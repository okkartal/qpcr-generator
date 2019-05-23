using System;
using System.Collections.Generic;

namespace Qpcr.Core
{
    public class QpcrGenerator
    {    
        public bool GenerateQpcr(QpcrModel model)
        {
            if (!PlageSizeIsValid(model.PlateSize))
                return false;
             
            return true;
        }

        private bool PlageSizeIsValid(int plateSize)
        {
            return (plateSize == Constants.PLATESIZE_96 ||
                    plateSize == Constants.PLATESIZE_384);
        }
    }
}