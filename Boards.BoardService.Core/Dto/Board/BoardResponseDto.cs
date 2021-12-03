using System;
using System.Collections.Generic;
using Boards.BoardService.Core.Dto.Thread;

namespace Boards.BoardService.Core.Dto.Board
{
    public class BoardResponseDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public ICollection<ThreadModelDto> Threads { get; set; }
    }
}