using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Auth.Common.Options;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Base;

namespace Boards.BoardService.Database.Repositories.Message
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context, PagingOptions options) : base(context, options)
        { }

        public async Task<ICollection<MessageModel>> GetByThreadId(Guid id, int pageNumber, int pageSize)
        {
            var messages = await GetFiltered<MessageModel>
                (m => m.ThreadId == id, pageNumber, pageSize);
            return messages;
        }
    }
}