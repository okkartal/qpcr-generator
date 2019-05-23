using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace Qpcr.Core.Tests
{
    public class QpcrCoreTests
    {

       private static IEnumerable<QpcrModel> GetQpcrModel()
       {
            yield return new  QpcrModel()
          {
             Names = new string[,] {{"Sam 1", "Sam 2", "Sam 3"}, {"Sam 1", "Sam 2", "Sam 3"}},
             NamesOfReagents = new string[,] {{"<Pink>", "<Green>"},},
             listOfIntegers = new int[,] {{1, 2},},
             MaximumNumberOfPlages = 1,
             PlateSize = 96
          };

       }
       
       

        [TestCaseSource("GetQpcrModel")]
        public void CallMethod_WhenQpcrGenerated_ReturnsTrue(QpcrModel model)
        {
           var result = new QpcrGenerator().GenerateQpcr(model);
           
           Assert.IsTrue(result);
        }
    }
}