using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.Message;
using Boards.Common.Filter;
using Boards.Common.Result;

namespace Boards.BoardService.Core.Services.Message
{
    public interface IMessageService
    {
        Task<ResultContainer<ICollection<MessageResponseDto>>> GetByThreadId(Guid id, FilterPagingDto filter);
    }
}