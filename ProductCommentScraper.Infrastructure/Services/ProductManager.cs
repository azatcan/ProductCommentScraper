using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductCommentScraper.Domain.Application.Dtos;
using ProductCommentScraper.Domain.Entities;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Infrastructure.Services
{
    public class ProductManager : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductManager(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task AddAsync(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _productRepository.DeleteAsync(id);
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }

        public async Task<ProductDto> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(string id, ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            var result = await _productRepository.UpdateAsync(product.Id,product);
        }
    }
}
