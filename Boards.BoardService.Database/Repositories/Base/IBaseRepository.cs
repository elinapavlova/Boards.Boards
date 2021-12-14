using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Auth.Common.Base;

namespace Boards.BoardService.Database.Repositories.Base
{
    public interface IBaseRepository
    {
        Task<TModel> Create<TModel>(TModel item) where TModel : BaseModel;
        Task<ICollection<TEntity>> GetFiltered<TEntity>(Func<TEntity, bool> predicate,int pageNumber, int pageSize) 
            where TEntity : BaseModel;
        Task<TEntity> GetById<TEntity>(Guid id) where TEntity : BaseModel;
        Task<TEntity> Update<TEntity>(TEntity item) where TEntity : BaseModel;
        Task<TEntity> Remove<TEntity>(Guid id) where TEntity : BaseModel;
    }
}