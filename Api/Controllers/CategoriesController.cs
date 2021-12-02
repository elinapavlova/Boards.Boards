using System;
using System.Threading.Tasks;
using Common.Result;
using Core.Dto.Category;
using Core.Dto.Category.Create;
using Core.Dto.Category.Update;
using Core.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        /// <summary>
        /// Create a category
        /// </summary>
        /// <response code="200">Return created category</response>
        /// <response code="400">If the category already exists</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryModelDto>> Create(CreateCategoryModelDto category)
            => await ReturnResult<ResultContainer<CategoryModelDto>, CategoryModelDto>(_categoryService.Create(category)); 
        
        /// <summary>
        /// Get category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return category</response>
        /// <response code="404">If the category doesn't exist</response>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelDto>> GetById(Guid id)
            => await ReturnResult<ResultContainer<CategoryModelDto>, CategoryModelDto>(_categoryService.GetById(id)); 
        
        /// <summary>
        /// Get category by name
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Return category</response>
        /// <response code="404">If the category doesn't exist</response>
        [HttpGet("{name}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelDto>> GetByName(string name)
            => await ReturnResult<ResultContainer<CategoryModelDto>, CategoryModelDto>(_categoryService.GetByName(name)); 
        
        /// <summary>
        /// Update category
        /// </summary>
        /// <response code="200">Return created category</response>
        /// <response code="404">If the category doesn't exist</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelDto>> Update(UpdateCategoryRequestDto category)
            => await ReturnResult<ResultContainer<CategoryModelDto>, CategoryModelDto>(_categoryService.Update(category)); 
        
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return category</response>
        /// <response code="404">If the category doesn't exists</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelDto>> Delete(Guid id)
            => await ReturnResult<ResultContainer<CategoryModelDto>, CategoryModelDto>(_categoryService.Delete(id)); 
        
        /// <summary>
        /// Get category Id with boards
        /// </summary>
        /// <response code="200">Return boards list</response>
        /// <response code="404">If the category doesn't exist</response>
        [HttpGet("With-Boards/{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResponseDto>> GetByIdWithBoards(Guid id)
            => await ReturnResult<ResultContainer<CategoryResponseDto>, CategoryResponseDto>
                (_categoryService.GetByIdWithBoards(id));
    }
}