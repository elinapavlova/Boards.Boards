using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Result;
using Core.Dto.Thread;
using Core.Dto.Thread.Create;

namespace Core.Services.Thread
{
    public interface IThreadService
    {
        Task<ResultContainer<CreateThreadResponseDto>> Create(CreateThreadRequestDto data);
        Task<ResultContainer<ThreadResponseDto>> GetById(Guid id);
        Task<ResultContainer<ICollection<ThreadModelDto>>> GetByName(string name);
        Task<ResultContainer<ThreadModelDto>> Delete(Guid id);
    }
}