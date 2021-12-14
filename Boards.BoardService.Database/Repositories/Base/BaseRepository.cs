using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Auth.Common.Base;
using Boards.Auth.Common.Options;
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

        public async Task<TEntity> Create<TEntity>(TEntity item) where TEntity : BaseModel
        {
            item.DateCreated = DateTime.Now;
            await _context.Set<TEntity>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
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

        public async Task<ICollection<TEntity>> GetFiltered<TEntity>
            (Func<TEntity, bool> predicate,int pageNumber, int pageSize)
            where TEntity : BaseModel
        {
            if (pageSize < _pagingOptions.DefaultPageSize)
                pageSize = _pagingOptions.DefaultPageSize;
            
            if (pageNumber < _pagingOptions.DefaultPageNumber)
                pageSize = _pagingOptions.DefaultPageNumber;
            
            var result = _context.Set<TEntity>()
                .AsNoTracking()
                .OrderByDescending(b => b.DateCreated)
                .Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            return result;
        }
    }
}