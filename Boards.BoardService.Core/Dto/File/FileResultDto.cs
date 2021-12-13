using System;
using System.IO;

namespace Boards.BoardService.Core.Dto.File
{
    public class FileResultDto
    {
        public FileStream Stream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Uri Url { get; set; }
    }
}