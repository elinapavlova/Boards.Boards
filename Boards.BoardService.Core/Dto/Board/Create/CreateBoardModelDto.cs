using System;

namespace Boards.BoardService.Core.Dto.Board.Create
{
    public class CreateBoardModelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}