using System.Collections.Generic;

namespace Core.Dto.File.Upload
{
    public class UploadFilesResultDto
    {
        public IList<FileResponseDto> Data { get; set; }
        public int? ErrorType { get; set; }
    }
}