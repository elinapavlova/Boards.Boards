using System;
using System.Threading.Tasks;
using AutoMapper;
using Boards.Auth.Common.Error;
using Boards.Auth.Common.Result;
using Boards.BoardService.Core.Dto.Category;
using Boards.BoardService.Core.Dto.Category.Create;
using Boards.BoardService.Core.Dto.Category.Update;
using Boards.BoardService.Core.Services.Board;
using Boards.BoardService.Database.Models;
using Boards.BoardService.Database.Repositories.Category;

namespace Boards.BoardService.Core.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBoardService _boardService;
        private readonly IMapper _mapper;

        public CategoryService 
        (
            ICategoryRepository categoryRepository, 
            IMapper mapper,
            IBoardService boardService
        )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _boardService = boardService;
        }
        
        public async Task<ResultContainer<CategoryModelDto>> Create(CreateCategoryModelDto data)
        {
            var result = new ResultContainer<CategoryModelDto>();
            var category = _categoryRepository.GetOne<CategoryModel>(b => b.Name == data.Name);

            if (category != null)
            {
                result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var newCategory = _mapper.Map<CreateCategoryModelDto, CategoryModel>(data);
            newCategory.DateCreated = DateTime.Now;

            result = _mapper.Map<ResultContainer<CategoryModelDto>>(await _categoryRepository.Create(newCategory));
            return result;
        }

        public async Task<ResultContainer<CategoryModelDto>> GetByName(string name)
        {
            var result = new ResultContainer<CategoryModelDto>();
            var category = _categoryRepository.GetOne<CategoryModel>(b => b.Name == name);
            if (category == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<CategoryModelDto>>(category);
            return result;
        }

        public async Task<ResultContainer<CategoryModelDto>> GetById(Guid id)
        {
            var result = new ResultContainer<CategoryModelDto>();
            var category = await _categoryRepository.GetById<CategoryModel>(id);
            if (category == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<CategoryModelDto>>(category);
            return result;
        }

        public async Task<ResultContainer<CategoryModelDto>> Delete(Guid id)
        {
            var result = new ResultContainer<CategoryModelDto>();
            var category = await _categoryRepository.Remove<CategoryModel>(id);
            if (category == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result = _mapper.Map<ResultContainer<CategoryModelDto>>(category);
            return result;
        }

        public async Task<ResultContainer<CategoryModelDto>> Update(UpdateCategoryRequestDto data)
        {
            var result = new ResultContainer<CategoryModelDto>();
            var category = _categoryRepository.GetOne<CategoryModel>(b => b.Name == data.Name);
            if (category == null)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            category.Name = data.NewName;
            result = _mapper.Map<ResultContainer<CategoryModelDto>>(await _categoryRepository.Update(category));
            return result;
        }

        public async Task<ResultContainer<CategoryResponseDto>> GetByIdWithBoards(Guid id)
        {
            var result = new ResultContainer<CategoryResponseDto>
            {
                Data = new CategoryResponseDto()
            };
            var category = await _categoryRepository.GetById<CategoryModel>(id);
            
            var boards = await _boardService.GetByCategoryId(id);
            if (boards.ErrorType.HasValue)
            {
                result.ErrorType = ErrorType.NotFound;
                return result;
            }

            result.Data.Boards = boards.Data;
            result.Data.Name = category.Name;
            return result;
        }
    }
}