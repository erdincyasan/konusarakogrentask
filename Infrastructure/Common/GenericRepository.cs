using Application.Common.Interfaces;
using Application.Common.Specifications;
using Application.Wrapper;
using Domain;
using Domain.Common.Contracts;
using Infrastructure.Common.Extensions;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync<T>(T entity) where T : BaseEntity
        {
            var query = _dbContext.Set<T>();
            query.Add(entity);
            return entity.Id;
        }

        public Task<List<T>> GetAllAsync<T>() where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<Guid> GetByIdAsync<T, TDto>(Guid id) where T : BaseEntity where TDto : IDto
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<TDto>> GetSearchResult<T, TDto>(int pageNumber, int pageSize = int.MaxValue, string[] orderBy = null, Filters<T> filters = null, Expression<Func<T, bool>> expression = null, BaseSpecification<T> spec = null, CancellationToken token = default)
            where T : BaseEntity
            where TDto : IDto
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (spec != null)
            {
                query.Specify(spec);
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            query.ApplySort(orderBy);
            return await query.ToMappedPaginatedResultAsync<T, TDto>(pageNumber, pageSize);
        }
        public async Task<TDto> GetByIdAsync<T, TDto>(Guid entityId, BaseSpecification<T> specification, CancellationToken cancellationToken = default)
        where T : BaseEntity
        where TDto : IDto
        {

            IQueryable<T> query = _dbContext.Set<T>();
            if (specification != null)
                query = query.Specify(specification);
            var entity = await query.Where(a => a.Id == entityId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            var dto = entity.Adapt<TDto>();
            return dto;

        }

        public Task<Guid> RemoveAsync<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateAsync<T>(T entity, Guid id) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            var query = _dbContext.Set<T>();
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
