using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Error;
using Common.Result;
using Core.Dto.Board;
using Core.Dto.Board.Create;
using Core.Dto.Board.Update;
using Database.Models;
using Database.Repositories.Board;
using Database.Repositories.Category;

namespace Core.Services.Board
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public BoardService 
        (
            IBoardRepository boardRepository, 
            IMapper mapper,
            ICategoryRepository categoryRepository
        )
        {
            _boardRepository = boardRepository;
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

        public async Task<ResultContainer<ICollection<BoardResponseDto>>> GetByCategoryId(Guid id)
        {
            var result = new ResultContainer<ICollection<BoardResponseDto>>();
            var category = await _categoryRepository.GetById<CategoryModel>(id);
            if (category == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }
            
            result = _mapper.Map<ResultContainer<ICollection<BoardResponseDto>>>(await _boardRepository.GetByCategoryId(id));
            return result;
        }
    }
}