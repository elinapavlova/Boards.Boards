using System;

namespace Core.Dto.File
{
    public class FileMessageResponseDto : FileResponseDto
    {
        public Guid MessageId { get; set; }
    }
}