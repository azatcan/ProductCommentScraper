using AutoMapper;
using ProductCommentScraper.Domain.Application.Dtos;
using ProductCommentScraper.Domain.Entities;
using ProductCommentScraper.Domain.Repositories;
using ProductCommentScraper.Domain.Services;
using ProductCommentScraper.Infrastructure.Repositories;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Infrastructure.Services
{
    public class ReviewManager : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _repository;

        public ReviewManager(IMapper mapper, IReviewRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(ReviewDto entity)
        {
            var review = _mapper.Map<Review>(entity);
            await _repository.AddAsync(review);
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _repository.DeleteAsync(id);
        }

        public async Task<List<ReviewDto>> GetAllAsync()
        {
            var review = await _repository.GetAllAsync();
            var result = _mapper.Map<List<ReviewDto>>(review);
            return result;
        }

        public async Task<ReviewDto> GetByIdAsync(string id)
        {
            var review = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReviewDto>(review);
        }

        public Task UpdateAsync(string id, ReviewDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
