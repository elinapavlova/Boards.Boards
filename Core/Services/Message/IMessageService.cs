using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Result;
using Core.Dto.Message;

namespace Core.Services.Message
{
    public interface IMessageService
    {
        Task<ResultContainer<ICollection<MessageResponseDto>>> GetByThreadId(Guid id);
    }
}