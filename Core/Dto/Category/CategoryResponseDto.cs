using System.Collections.Generic;

namespace Core.Dto.Category
{
    public class CategoryResponseDto
    {
        public string Name { get; set; }
        public ICollection<BoardResponseDto> Boards { get; set; }
    }
}