using System;

namespace Core.Dto
{
    public class BoardResponseDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
    }
}