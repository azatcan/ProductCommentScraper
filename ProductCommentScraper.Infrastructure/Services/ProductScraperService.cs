using AutoMapper;
using Microsoft.Playwright;
using ProductCommentScraper.Domain.Entities;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductCommentScraper.Infrastructure.Services
{
    public class ProductScraperService : IProductScraperService
    {

        private readonly IProductRepository _productRepository;

        public ProductScraperService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ScrapeAndSaveTrendyolAsync()
        {
            string homeUrl = "https://www.trendyol.com/";
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true,
                Timeout = 60000,
            });

            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
                Locale = "tr-TR"
            });

            var page = await context.NewPageAsync();

            await page.GotoAsync(homeUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
            await page.WaitForTimeoutAsync(2500);

            for (int i = 0; i < 3; i++)
            {
                await page.EvaluateAsync("window.scrollBy(0, document.body.scrollHeight)");
                await page.WaitForTimeoutAsync(1500);
            }

            var productLinks = await page.QuerySelectorAllAsync(".widget-product a");
            var productUrls = new List<string>();
            foreach (var productLink in productLinks)
            {
                var productUrl = await productLink.GetAttributeAsync("href");
                if (productUrl != null)
                {
                    productUrls.Add("https://www.trendyol.com" + productUrl); 
                }
            }

            foreach (var productUrl in productUrls)
            {
                await page.GotoAsync(productUrl, new PageGotoOptions { WaitUntil = WaitUntilState.Load, Timeout = 120000 });
                await page.WaitForTimeoutAsync(5500);

                var h1Element = await page.QuerySelectorAsync("h1.pr-new-br");
                if (h1Element == null)
                {
                    continue;
                }

                var brandElement = await h1Element.QuerySelectorAsync("a");
                string brand = brandElement != null ? await brandElement.InnerTextAsync() : "Bilinmeyen Marka";

                var modelElement = await h1Element.QuerySelectorAsync("span");
                string modelName = modelElement != null ? await modelElement.InnerTextAsync() : "Bilinmeyen Model";

                var imageUrl = await page.GetAttributeAsync(".gallery-container img", "src");
                var priceText = await page.InnerTextAsync(".prc-dsc");

                decimal price = 0;
                if (!string.IsNullOrEmpty(priceText))
                {
                    priceText = priceText.Replace("TL", "")
                                         .Replace("₺", "")
                                         .Replace(".", "")
                                         .Replace(",", ".")
                                         .Trim();

                    decimal.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out price);
                }

                var product = new Product
                {
                    Brand = brand,
                    ModelName = modelName,
                    ModelNo = "", 
                    Description = "description",
                    ImageUrl = imageUrl ?? "",
                    Price = price,
                    Reviews = new List<Review>(),
                    Properties = new List<ProductFeature>(),
                    SellSources = new List<SellSource>()
                };

                var featureElements = await page.QuerySelectorAllAsync("li.detail-attr-item");
                foreach (var feature in featureElements)
                {
                    var name = await feature.QuerySelectorAsync(".attr-key-name-w");
                    var value = await feature.QuerySelectorAsync(".attr-value-name-w");

                    if (name != null && value != null)
                    {
                        product.Properties.Add(new ProductFeature
                        {
                            FeatureName = await name.InnerTextAsync(),
                            FeatureValue = await value.InnerTextAsync()
                        });
                    }
                }

                string yorumUrl = productUrl.Contains('?')
                    ? productUrl.Insert(productUrl.IndexOf('?'), "/yorumlar")
                    : productUrl + "/yorumlar";

                await page.GotoAsync(yorumUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle, Timeout = 60000 });
                await page.WaitForTimeoutAsync(2500);

                var reviewElements = await page.QuerySelectorAllAsync(".comment");
                foreach (var element in reviewElements)
                {
                    var usernameElement = await element.QuerySelectorAsync(".comment-info-item");
                    var commentElement = await element.QuerySelectorAsync(".comment-text p");

                    if (usernameElement == null || commentElement == null)
                        continue;

                    var username = await usernameElement.InnerTextAsync();
                    var comment = await commentElement.InnerTextAsync();

                    int rating = 0;
                    var starWrappers = await element.QuerySelectorAllAsync(".star-w");

                    foreach (var starWrapper in starWrappers)
                    {
                        var fullDiv = await starWrapper.QuerySelectorAsync(".full");
                        if (fullDiv != null)
                        {
                            var style = await fullDiv.GetAttributeAsync("style");

                            if (!string.IsNullOrEmpty(style) && style.Contains("width:"))
                            {
                                var match = Regex.Match(style, @"width:\s*(\d+)");
                                if (match.Success && int.TryParse(match.Groups[1].Value, out int widthValue))
                                {
                                    if (widthValue >= 50) 
                                        rating++;
                                }
                            }
                        }
                    }

                    product.Reviews.Add(new Review
                    {
                        Username = username,
                        Comment = comment,
                        Rating = rating,
                    });
                }
                product.SellSources.Add(new SellSource
                {
                    Site = "Trendyol",
                    ProductUrl = productUrl
                });

                // Veriyi kaydet
                await _productRepository.AddAsync(product);
            }

            await browser.CloseAsync();
        }

    }
}
