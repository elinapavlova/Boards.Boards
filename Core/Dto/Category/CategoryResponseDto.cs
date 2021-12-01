using System.Collections.Generic;
using Core.Dto.Board;

namespace Core.Dto.Category
{
    public class CategoryResponseDto
    {
        public string Name { get; set; }
        public ICollection<BoardResponseDto> Boards { get; set; }
    }
}