using System;
using Microsoft.AspNetCore.Http;

namespace Boards.BoardService.Core.Dto.Thread.Create
{
    public class CreateThreadRequestDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BoardId { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}