using System;

namespace Boards.BoardService.Core.Dto.File
{
    public class FileMessageResponseDto : FileResponseDto
    {
        public Guid MessageId { get; set; }
    }
}