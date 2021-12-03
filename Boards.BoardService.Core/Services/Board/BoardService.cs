﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Boards.BoardService.Core.Dto.Board;
using Boards.BoardService.Core.Dto.Board.Create;
using Boards.BoardService.Core.Dto.Board.Update;
using Boards.BoardService.Core.Services.Thread;
using Common.Error;
using Common.Result;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Board;
using Boards.BoardService.Database.Repositories.Category;

namespace Boards.BoardService.Core.Services.Board
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IThreadService _threadService;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public BoardService 
        (
            IBoardRepository boardRepository, 
            IThreadService threadService,
            IMapper mapper,
            ICategoryRepository categoryRepository
        )
        {
            _boardRepository = boardRepository;
            _threadService = threadService;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        
        public async Task<ResultContainer<BoardModelDto>> Create(CreateBoardModelDto data)
        {
            var result = new ResultContainer<BoardModelDto>();
            var category = await _categoryRepository.GetById<CategoryModel>(data.CategoryId);
            if (category == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            var board = _boardRepository.GetOne<BoardModel>(b => b.Name == data.Name);
            if (board != null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var newBoard = _mapper.Map<CreateBoardModelDto, BoardModel>(data);
            newBoard.DateCreated = DateTime.Now;

            result = _mapper.Map<ResultContainer<BoardModelDto>>(await _boardRepository.Create(newBoard));
            return result;
        }

        public async Task<ResultContainer<BoardModelDto>> GetByName(string name)
        {
            var result = new ResultContainer<BoardModelDto>();
            var boards = _boardRepository.GetOne<BoardModel>(b => b.Name == name);
            if (boards == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<BoardModelDto>>(boards);
            return result;
        }

        public async Task<ResultContainer<BoardModelDto>> GetById(Guid id)
        {
            var result = new ResultContainer<BoardModelDto>();
            var board = await _boardRepository.GetById<BoardModel>(id);
            if (board == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<BoardModelDto>>(board);
            return result;
        }

        public async Task<ResultContainer<BoardModelDto>> Delete(Guid id)
        {
            var result = new ResultContainer<BoardModelDto>();
            var board = await _boardRepository.Remove<BoardModel>(id);
            if (board == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<BoardModelDto>>(board);
            return result;
        }

        public async Task<ResultContainer<BoardModelDto>> Update(UpdateBoardRequestDto data)
        {
            var result = new ResultContainer<BoardModelDto>();
            var board = _boardRepository.GetOne<BoardModel>(b => b.Name == data.Name);
            if (board == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            board.Name = data.NewName;
            board.Description = data.Description;

            result = _mapper.Map<ResultContainer<BoardModelDto>>(await _boardRepository.Update(board));
            return result;
        }

        public async Task<ResultContainer<ICollection<BoardModelDto>>> GetByCategoryId(Guid id)
        {
            var result = new ResultContainer<ICollection<BoardModelDto>>();
            var category = await _categoryRepository.GetById<CategoryModel>(id);
            if (category == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }
            
            result = _mapper.Map<ResultContainer<ICollection<BoardModelDto>>>(await _boardRepository.GetByCategoryId(id));
            return result;
        }

        public async Task<ResultContainer<BoardResponseDto>> GetByIdWithThreads(Guid id)
        {
            var result = new ResultContainer<BoardResponseDto>
            {
                Data = new BoardResponseDto()
            };
            var board = await _boardRepository.GetById<BoardModel>(id);
            
            var threads = await _threadService.GetByBoardId(id);
            if ( threads.ErrorType.HasValue)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<BoardResponseDto>>(board);
            result.Data.Threads =  threads.Data;
            return result;
        }
    }
}