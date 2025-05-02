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
    public class SellSourceManager : ISellSourceService
    {
        private readonly IMapper _mapper;
        private readonly ISellSourceRepository _sellSourceRepository;

        public SellSourceManager(IMapper mapper, ISellSourceRepository sellSourceRepository)
        {
            _mapper = mapper;
            _sellSourceRepository = sellSourceRepository;
        }

        public async Task AddAsync(SellSourceDto entity)
        {
            var sellSource = _mapper.Map<SellSource>(entity);
            await _sellSourceRepository.AddAsync(sellSource);
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _sellSourceRepository.DeleteAsync(id);
        }

        public async Task<List<SellSourceDto>> GetAllAsync()
        {
            var sellSources = await _sellSourceRepository.GetAllAsync();
            var result = _mapper.Map<List<SellSourceDto>>(sellSources);
            return result;
        }

        public async Task<SellSourceDto> GetByIdAsync(string id)
        {
            var sellSource = await _sellSourceRepository.GetByIdAsync(id);
            return _mapper.Map<SellSourceDto>(sellSource);
        }

        public Task UpdateAsync(string id, SellSourceDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
