using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Base;

namespace Boards.BoardService.Database.Repositories.Base
{
    public interface IBaseRepository
    {
        TModel GetOne<TModel>(Func<TModel, bool> predicate) where TModel : BaseModel;
        Task<TModel> Create<TModel>(TModel item) where TModel : BaseModel;
        List<TEntity> Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseModel;
        Task<TEntity> GetById<TEntity>(Guid id) where TEntity : BaseModel;
        Task<TEntity> Update<TEntity>(TEntity item) where TEntity : BaseModel;
        Task<TEntity> Remove<TEntity>(Guid id) where TEntity : BaseModel;
    }
}