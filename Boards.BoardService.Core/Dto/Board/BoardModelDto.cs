using System;
using Boards.Common.Base;

namespace Boards.BoardService.Core.Dto.Board
{
    public class BoardModelDto : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}