using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Auth.Common.Base;
using Boards.Auth.Common.Filter;

namespace Boards.BoardService.Database.Repositories.Base
{
    public interface IBaseRepository
    {
        TModel GetOne<TModel>(Func<TModel, bool> predicate) where TModel : BaseModel;
        Task<TModel> Create<TModel>(TModel item) where TModel : BaseModel;
        IQueryable<TEntity> Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseModel;
        Task<ICollection<TEntity>> GetFiltered<TEntity, TFilter>(IQueryable<TEntity> source,TFilter filter) 
            where TFilter : BaseFilter where TEntity : BaseModel;
        Task<TEntity> GetById<TEntity>(Guid id) where TEntity : BaseModel;
        Task<TEntity> Update<TEntity>(TEntity item) where TEntity : BaseModel;
        Task<TEntity> Remove<TEntity>(Guid id) where TEntity : BaseModel;
    }
}