namespace Boards.BoardService.Core.Dto.Board.Update
{
    public class UpdateBoardRequestDto
    {
        public string NewName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}