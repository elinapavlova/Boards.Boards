using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Auth.Common.Filter;
using Boards.Auth.Common.Result;
using Boards.BoardService.Core.Dto.Message;

namespace Boards.BoardService.Core.Services.Message
{
    public interface IMessageService
    {
        Task<ResultContainer<ICollection<MessageResponseDto>>> GetByThreadId(Guid id, FilterPagingDto filter);
    }
}