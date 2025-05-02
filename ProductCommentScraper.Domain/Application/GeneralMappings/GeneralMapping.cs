using AutoMapper;
using MongoDB.Driver.Core.Misc;
using ProductCommentScraper.Domain.Application.Dtos;
using ProductCommentScraper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Application.GeneralMappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<SellSource, SellSourceDto>().ReverseMap();
        }
    }
}
