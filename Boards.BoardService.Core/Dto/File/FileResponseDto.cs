using System;
using Boards.Common.Base;

namespace Boards.BoardService.Core.Dto.File
{
    public class FileResponseDto : BaseModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }

        public Guid ThreadId { get; set; }
    }
}