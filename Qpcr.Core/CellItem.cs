namespace Qpcr.Core
{
    public class CellItem : Cell
    {
        public int Row_Coord { get; set; }

        public int Col_Coord { get; set; }
    }

    public class Cell
    {
        public string Name { get; set; }

        public string ReAgent { get; set; }
    }
}