using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Message
{
    public interface IMessageRepository : IBaseRepository
    {
        Task<ICollection<MessageModel>> GetByThreadId(Guid id, int pageNumber, int pageSize);
    }
}