using System;

namespace Boards.BoardService.Core.Dto.File
{
    public class FileResultDto
    {
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public Uri Url { get; set; }
    }
}