using Application.Common.Specifications;
using Application.Wrapper;
using Domain;
using Domain.Common.Contracts;
using Shared.Dtos;
using Shared.Dtos.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGenericRepository
    {
        Task<Guid> CreateAsync<T>(T entity) where T : BaseEntity;
        Task<Guid> UpdateAsync<T>(T entity, Guid id) where T : BaseEntity;
        Task<Guid> RemoveAsync<T>(Guid id);
        Task<Guid> GetByIdAsync<T,TDto>(Guid id) where T:BaseEntity where TDto:IDto;
        Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity;
        Task<List<T>> GetAllAsync<T>() where T : BaseEntity;
        Task<PaginatedResult<TDto>> GetSearchResult<T, TDto>(int pageNumber, int pageSize = int.MaxValue, string[] orderBy = null, Filters<T> filters = null, Expression<Func<T, bool>> expression = null, BaseSpecification<T> spec = null, CancellationToken token = default)
            where T : BaseEntity
            where TDto : IDto;
        Task<TDto> GetByIdAsync<T, TDto>(Guid entityId, BaseSpecification<T> specification, CancellationToken cancellationToken = default)
        where T : BaseEntity
        where TDto : IDto;
    }
}
