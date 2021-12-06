using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Base;
using Common.Filter;
using Common.Options;
using Microsoft.EntityFrameworkCore;

namespace Boards.BoardService.Database.Repositories.Base
{
    public class BaseRepository : IBaseRepository
    {
        private readonly AppDbContext _context;
        private readonly PagingOptions _pagingOptions;

        public BaseRepository
        (
            AppDbContext context, 
            PagingOptions pagingOptions
        )
        {
            _context = context;
            _pagingOptions = pagingOptions;
        }
        
        public TEntity GetOne<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseModel
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }
        
        public async Task<TEntity> Create<TEntity>(TEntity item) where TEntity : BaseModel
        {
            item.DateCreated = DateTime.Now;
            await _context.Set<TEntity>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public IQueryable<TEntity> Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseModel
        {
            return _context.Set<TEntity>().AsNoTracking().AsEnumerable().Where(predicate).AsQueryable();
        }

        public async Task<TEntity> GetById<TEntity>(Guid id) where TEntity : BaseModel
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Update<TEntity>(TEntity item) where TEntity : BaseModel
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TEntity> Remove<TEntity>(Guid id) where TEntity : BaseModel
        {
            var item = await _context.Set<TEntity>().FindAsync(id);
            if (item == null)
                return null;
            
            _context.Set<TEntity>().Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ICollection<TEntity>> GetFiltered<TEntity, TFilter>(IQueryable<TEntity> source, TFilter filter)
        where TFilter : BaseFilter
        where TEntity : BaseModel
        {
            var result = ApplyPaging(source, filter.Paging).AsEnumerable().ToList();
            return result;
        }


        private IQueryable<TEntity> ApplyPaging<TEntity>(IQueryable<TEntity> source, FilterPagingDto paging) 
            where TEntity : BaseModel
        {
            if (paging.PageSize < 1)
                paging.PageSize = _pagingOptions.DefaultPageSize;
            
            if (paging.PageNumber < 1)
                paging.PageNumber = _pagingOptions.DefaultPageNumber;
            
            return source
                .OrderByDescending(b => b.DateCreated)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);
        }
    }
}