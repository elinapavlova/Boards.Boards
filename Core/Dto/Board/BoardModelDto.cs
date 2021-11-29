using System;
using Common.Base;

namespace Core.Dto.Board
{
    public class BoardModelDto : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}