using System.Collections.Generic;
using Boards.Auth.Common.Base;
using Boards.BoardService.Core.Dto.Board;

namespace Boards.BoardService.Core.Dto.Category
{
    public class CategoryResponseDto : BaseModel
    {
        public string Name { get; set; }
        public ICollection<BoardModelDto> Boards { get; set; }
    }
}