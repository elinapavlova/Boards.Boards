﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.Thread;
using Boards.BoardService.Core.Dto.Thread.Create;
using Boards.Common.Filter;
using Boards.Common.Result;

namespace Boards.BoardService.Core.Services.Thread
{
    public interface IThreadService
    {
        Task<ResultContainer<CreateThreadResponseDto>> Create(CreateThreadRequestDto data);
        Task<ResultContainer<ThreadResponseDto>> GetByIdWithMessages(Guid id, FilterPagingDto filter);
        Task<ResultContainer<ThreadModelDto>> GetById(Guid id);
        Task<ResultContainer<ICollection<ThreadModelDto>>> GetByName(string name, FilterPagingDto filter);
        Task<ResultContainer<ThreadModelDto>> Delete(Guid id);
    }
}