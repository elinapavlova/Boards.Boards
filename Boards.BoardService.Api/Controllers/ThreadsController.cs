using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Auth.Common.Filter;
using Boards.Auth.Common.Result;
using Boards.BoardService.Core.Dto.Thread;
using Boards.BoardService.Core.Dto.Thread.Create;
using Boards.BoardService.Core.Services.Thread;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boards.BoardService.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class ThreadsController : BaseController
    {
        private readonly IThreadService _threadService;

        public ThreadsController(IThreadService threadService)
        {
            _threadService = threadService;
        }
        
        /// <summary>
        /// Create a thread
        /// </summary>
        /// <response code="200">Return created thread</response>
        /// <response code="400">If the thread name or text are empty</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateThreadResponseDto>> Create([FromForm] CreateThreadRequestDto thread)
            => await ReturnResult<ResultContainer<CreateThreadResponseDto>, CreateThreadResponseDto>
                (_threadService.Create(thread));

        /// <summary>
        /// Get thread by Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return thread</response>
        /// <response code="404">If the thread doesn't exist</response>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ThreadModelDto>> GetById(Guid id)
            => await ReturnResult<ResultContainer<ThreadModelDto>, ThreadModelDto>(_threadService.GetById(id));
        
        /// <summary>
        /// Get thread by Id with messages
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <response code="200">Return thread</response>
        /// <response code="404">If the thread or messages on page doesn't exist</response>
        [HttpGet("{id:guid}/messages")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ThreadResponseDto>> GetByIdWithMessages(Guid id, [FromQuery] FilterPagingDto filter)
            => await ReturnResult<ResultContainer<ThreadResponseDto>, ThreadResponseDto>
                (_threadService.GetByIdWithMessages(id, filter));

        /// <summary>
        /// Get threads by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        /// <response code="200">Return thread</response>
        /// <response code="404">If the thread doesn't exist</response>
        [HttpGet("{name}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<ThreadModelDto>>> GetByName(string name, [FromQuery] FilterPagingDto filter)
            => await ReturnResult<ResultContainer<ICollection<ThreadModelDto>>, ICollection<ThreadModelDto>>
                (_threadService.GetByName(name, filter)); 
        
        /// <summary>
        /// Delete thread
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return thread</response>
        /// <response code="404">If the thread doesn't exist</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ThreadModelDto>> Delete(Guid id)
            => await ReturnResult<ResultContainer<ThreadModelDto>, ThreadModelDto>(_threadService.Delete(id));
    }
}