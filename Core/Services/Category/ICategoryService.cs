using System;
using System.Threading.Tasks;
using Common.Result;
using Core.Dto.Category;
using Core.Dto.Category.Create;
using Core.Dto.Category.Update;

namespace Core.Services.Category
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