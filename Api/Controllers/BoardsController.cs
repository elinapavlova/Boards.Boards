using System;
using System.Threading.Tasks;
using Common.Result;
using Core.Dto.Board;
using Core.Dto.Board.Create;
using Core.Dto.Board.Update;
using Core.Services.Board;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class BoardsController : BaseController
    {
        private readonly IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        /// <summary>
        /// Create a board
        /// </summary>
        /// <response code="200">Return created board</response>
        /// <response code="400">If the board already exists</response>
        /// <response code="404">If category doesn't exist</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpPost]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BoardModelDto>> Create(CreateBoardModelDto board)
            => await ReturnResult<ResultContainer<BoardModelDto>, BoardModelDto>(_boardService.Create(board));

        /// <summary>
        /// Get board by id
        /// </summary>
        /// <response code="200">Return the board</response>
        /// <response code="404">If the board doesn't exist</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpGet("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BoardModelDto>> GetById(Guid id)
            => await ReturnResult<ResultContainer<BoardModelDto>, BoardModelDto>
                (_boardService.GetById(id));
        
        /// <summary>
        /// Get board by name
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Return the board</response>
        /// <response code="404">If the board doesn't exist</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpGet("{name}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BoardModelDto>> GetByName(string name)
            => await ReturnResult<ResultContainer<BoardModelDto>, BoardModelDto>
                (_boardService.GetByName(name));
        
        /// <summary>
        /// Delete a board
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return deleted board</response>
        /// <response code="404">If the board doesn't exist</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BoardModelDto>> Delete(Guid id)
            => await ReturnResult<ResultContainer<BoardModelDto>, BoardModelDto>
                (_boardService.Delete(id));
        
        /// <summary>
        /// Update a board
        /// </summary>
        /// <response code="200">Return updated board</response>
        /// <response code="404">If the board doesn't exist</response>
        /// <response code="401">If the User wasn't authorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BoardModelDto>> Update(UpdateBoardRequestDto data)
            => await ReturnResult<ResultContainer<BoardModelDto>, BoardModelDto>
                (_boardService.Update(data));
    }
}