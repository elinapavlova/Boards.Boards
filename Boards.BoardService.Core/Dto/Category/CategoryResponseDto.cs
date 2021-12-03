using System.Collections.Generic;
using Boards.BoardService.Core.Dto.Board;

namespace Boards.BoardService.Core.Dto.Category
{
    public class CategoryResponseDto
    {
        public string Name { get; set; }
        public ICollection<BoardResponseDto> Boards { get; set; }
    }
}