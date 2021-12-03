﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Boards.BoardService.Core.Dto.File;
using Boards.BoardService.Core.Dto.File.Upload;
using Boards.BoardService.Core.Dto.Thread;
using Boards.BoardService.Core.Dto.Thread.Create;
using Boards.BoardService.Core.Services.FileStorage;
using Boards.BoardService.Core.Services.Message;
using Common.Error;
using Common.Result;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Board;
using Boards.BoardService.Database.Repositories.Thread;

namespace Boards.BoardService.Core.Services.Thread
{
    public class ThreadService : IThreadService
    {
        private readonly IThreadRepository _threadRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ThreadService
        (
            IThreadRepository threadRepository, 
            IBoardRepository boardRepository,
            IFileStorageService fileStorageService,
            IMessageService messageService,
            IMapper mapper
        )
        {
            _threadRepository = threadRepository;
            _boardRepository = boardRepository;
            _fileStorageService = fileStorageService;
            _messageService = messageService;
            _mapper = mapper;
        }

        public async Task<ResultContainer<CreateThreadResponseDto>> Create(CreateThreadRequestDto data)
        {
            var result = new ResultContainer<CreateThreadResponseDto>();
            var resultUpload = new ResultContainer<UploadFilesResponseDto>();

            // Если название или текст пустые
            if (string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Text))
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }
            
            var board = await _boardRepository.GetById<BoardModel>(data.BoardId);
            if (board == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }
            
            var thread = _mapper.Map<ThreadModel>(data);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            result = _mapper.Map<ResultContainer<CreateThreadResponseDto>>(await _threadRepository.Create(thread));

            if (data.Files != null)
                resultUpload = await _fileStorageService.Upload(data.Files, result.Data.Id);

            if (resultUpload.Data != null)
                result.Data.Files = resultUpload.Data.Files;

            if (resultUpload.ErrorType.HasValue)
                result.ErrorType = ErrorType.BadRequest;
            else 
                scope.Complete();
            
            return result;
        }

        public async Task<ResultContainer<ThreadResponseDto>> GetById(Guid id)
        {
            var result = new ResultContainer<ThreadResponseDto>();
            var thread = await _threadRepository.GetById<ThreadModel>(id);
            if (thread == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<ThreadResponseDto>>(thread);
            result.Data.Files = _mapper.Map<ICollection<FileResponseDto>>(await _fileStorageService.GetByThreadId(id));
            result.Data.Messages = (await _messageService.GetByThreadId(id)).Data;
            return result;
        }

        public async Task<ResultContainer<ICollection<ThreadModelDto>>> GetByName(string name)
        {
            var result = new ResultContainer<ICollection<ThreadModelDto>>();
            var thread = _threadRepository.Get<ThreadModel>(t => t.Name.Contains(name));
            if (thread.Count == 0)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<ICollection<ThreadModelDto>>>(thread);
            return result;
        }

        public async Task<ResultContainer<ThreadModelDto>> Delete(Guid id)
        {
            var result = new ResultContainer<ThreadModelDto>();
            var thread = await _threadRepository.Remove<ThreadModel>(id);
            if (thread == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<ThreadModelDto>>(thread);
            return result;
        }

        public async Task<ResultContainer<ICollection<ThreadModelDto>>> GetByBoardId(Guid id)
        {
            var result = new ResultContainer<ICollection<ThreadModelDto>>();
            var board = await _boardRepository.GetById<BoardModel>(id);
            if (board == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }
            result = _mapper.Map<ResultContainer<ICollection<ThreadModelDto>>>(await _threadRepository.GetByBoardId(id));
            return result;
        }
    }
}