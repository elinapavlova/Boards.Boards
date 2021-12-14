using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Database.Models;

namespace Boards.BoardService.Database.Repositories.Message
{
    public interface IMessageRepository
    {
        Task<List<MessageModel>> GetByThreadId(Guid id, int pageNumber, int pageSize);
    }
}