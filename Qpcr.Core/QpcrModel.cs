namespace Qpcr.Core
{
    public class QpcrModel
    {
        public int PlateSize { get; set; }

        public int MaximumNumberOfPlages { get; set; }
        
        public  string[,] Names { get; set; }
        
        public  string[,] NamesOfReagents { get; set; }
        
        public  int[,] listOfIntegers { get; set; }
        
        
    }
}