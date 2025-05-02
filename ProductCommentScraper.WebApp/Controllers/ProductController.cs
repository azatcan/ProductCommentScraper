using Microsoft.AspNetCore.Mvc;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Domain.Services;

namespace ProductCommentScraper.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductScraperService _productScraperService;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductController(IProductScraperService productScraperService, IProductRepository productRepository, IProductService productService)
        {
            _productScraperService = productScraperService;
            _productRepository = productRepository;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);  
        }

        public async Task<IActionResult> Detail(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Scrape()
        {
            await _productScraperService.ScrapeAndSaveTrendyolAsync();
            return RedirectToAction("Index");
        }
    }
}
