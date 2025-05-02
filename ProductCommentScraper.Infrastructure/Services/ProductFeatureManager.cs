using AutoMapper;
using ProductCommentScraper.Domain.Application.Dtos;
using ProductCommentScraper.Domain.Entities;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Domain.Services;
using ProductCommentScraper.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Infrastructure.Services
{
    public class ProductFeatureManager : IProductFeatureService
    {
        private readonly IMapper _mapper;
        private readonly IProductFeatureRepository _repository;

        public ProductFeatureManager(IMapper mapper, IProductFeatureRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(ProductFeatureDto entity)
        {
            var productFeature = _mapper.Map<ProductFeature>(entity);
            await _repository.AddAsync(productFeature);
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _repository.DeleteAsync(id);
        }

        public async Task<List<ProductFeatureDto>> GetAllAsync()
        {
            var productFeatures = await _repository.GetAllAsync();
            var result = _mapper.Map<List<ProductFeatureDto>>(productFeatures);
            return result;
        }

        public async Task<ProductFeatureDto> GetByIdAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductFeatureDto>(product);
        }

        public Task UpdateAsync(string id, ProductFeatureDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
