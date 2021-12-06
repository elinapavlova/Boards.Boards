using System;
using System.Threading.Tasks;
using Boards.BoardService.Core.Dto.Category;
using Boards.BoardService.Core.Dto.Category.Create;
using Boards.BoardService.Core.Dto.Category.Update;
using Boards.Common.Result;

namespace Boards.BoardService.Core.Services.Category
{
    public interface ICategoryService
    {
        Task<ResultContainer<CategoryModelDto>> Create(CreateCategoryModelDto category);
        Task<ResultContainer<CategoryModelDto>> GetByName(string name);
        Task<ResultContainer<CategoryModelDto>> GetById(Guid id);
        Task<ResultContainer<CategoryModelDto>> Delete(Guid id);
        Task<ResultContainer<CategoryModelDto>> Update(UpdateCategoryRequestDto data);
        Task<ResultContainer<CategoryResponseDto>> GetByIdWithBoards(Guid id);
    }
}