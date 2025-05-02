using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCommentScraper.Domain.Services
{
    public interface IGenericService<TDto> where TDto : class, new()
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(string id);
        Task AddAsync(TDto entity);
        Task DeleteAsync(string id);
        Task UpdateAsync(string id, TDto entity);
    }
}
