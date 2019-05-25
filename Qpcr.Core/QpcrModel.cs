namespace Qpcr.Core
{
    public class QpcrModel
    {
        public int PlateSize { get; set; }

        //an integer defining the maximum number of plates that can be used
        public int MaximumNumberOfPlates { get; set; }  //If =0,limit for maximum row-count combination
        
        public  string[,] Names { get; set; }
        
        public  string[,] NamesOfReagents { get; set; }
        
        public  int[] listOfIntegers { get; set; }
            
    }
}