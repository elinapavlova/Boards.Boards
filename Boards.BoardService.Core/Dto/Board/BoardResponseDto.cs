using System;

namespace Boards.BoardService.Core.Dto.Board
{
    public class BoardResponseDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
    }
}