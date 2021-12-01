using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Common.Error;
using Common.Result;
using Core.Dto.File.Upload;
using Core.Dto.Thread;
using Core.Dto.Thread.Create;
using Core.Services.FileStorage;
using Database.Models;
using Database.Repositories.Board;
using Database.Repositories.Thread;

namespace Core.Services.Thread
{
    public class ThreadService : IThreadService
    {
        private readonly IThreadRepository _threadRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public ThreadService
        (
            IThreadRepository threadRepository, 
            IBoardRepository boardRepository,
            IFileStorageService fileStorageService,
            IMapper mapper
        )
        {
            _threadRepository = threadRepository;
            _boardRepository = boardRepository;
            _fileStorageService = fileStorageService;
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
            thread.DateCreated = DateTime.Now;
            
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

            // Получить сообщения
            
            result = _mapper.Map<ResultContainer<ThreadResponseDto>>(thread);
            return result;
        }

        public async Task<ResultContainer<ICollection<ThreadModelDto>>> GetByName(string name)
        {
            var result = new ResultContainer<ICollection<ThreadModelDto>>();
            
            var thread = _threadRepository.Get<ThreadModel>
                (t => t.Name.Contains(name))
                .AsEnumerable().ToList();
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
    }
}