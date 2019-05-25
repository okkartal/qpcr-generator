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
             Names = new string[,] {{"Sample-1", "Sample-2", "Sample-3"},{"Sample-1", "Sample-2", "Sample-3"}},
             NamesOfReagents = new string[,] {{"Pink", "Green"}},
             listOfIntegers = new int[] {3, 2},
             //MaximumNumberOfPlates = 1,
             PlateSize = 96
          };

       } 

        [TestCaseSource("GetQpcrModel")]
        public void CallMethod_WhenQpcrGenerated_ReturnsTrue(QpcrModel model)
        {
           var result = new QpcrGenerator().GenerateQpcr(model);
           
           Assert.IsNotNull(result);
        }
    }
}