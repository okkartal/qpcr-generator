using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using env = System.Environment;

namespace Qpcr.Core
{
    public class QpcrGenerator
    {
        public Cell[,] GenerateQpcr(QpcrModel model, bool generateVisualizeTable = true)
        {
            ValidateQpcrModel(model);

            int sumListOfIntegers = model.listOfIntegers.AsQueryable().Sum();

            //combine all reagents in single list
            var reAgentList = new List<string>();
            for (int i = 0; i < model.NamesOfReagents.Length; i++)
                reAgentList.Add(model.NamesOfReagents[0, i]);


            var cellItems = new List<CellItem>();

            Cell[,] cells = new Cell[model.Names.GetLength(0) + 1, sumListOfIntegers];

            //Get dimension count from Names array
            for (int dimensionIndex = 0; dimensionIndex < model.Names.Rank; dimensionIndex++)
            {
                //Get each element count from given dimension
                for (int itemIndex = 0; itemIndex < model.Names.GetLength(dimensionIndex); itemIndex++)
                {
                    //this variable iterates from  zero to sum(listOfInteger.values)
                    int listOfIntegerCounter = 0;

                    for (int listOfIntegerLen = 0; listOfIntegerLen < model.listOfIntegers.Length; listOfIntegerLen++)
                    {
                        for (int listOfIntegerItem = 0;
                            listOfIntegerItem < model.listOfIntegers[listOfIntegerLen];
                            listOfIntegerItem++)
                        {
                            //if maximum number of plates not exceeded
                            if (model.MaximumNumberOfPlates == 0 || cellItems.Count < model.MaximumNumberOfPlates)
                            {
                                cellItems.Add(new CellItem()
                                {
                                    Name = model.Names[dimensionIndex, itemIndex],
                                    ReAgent = reAgentList[listOfIntegerLen],
                                    Row_Coord = itemIndex,
                                    Col_Coord = listOfIntegerCounter
                                });

                                cells[itemIndex, listOfIntegerCounter] = new Cell()
                                {
                                    Name = model.Names[dimensionIndex, itemIndex],
                                    ReAgent = reAgentList[listOfIntegerLen]
                                };

                                ++listOfIntegerCounter;
                            }
                        }
                    }
                }
            }

            if (generateVisualizeTable)
            {
                var boardSize = GetBoardSize(model.PlateSize);

                GenerateVisualizeTable(cellItems, boardSize.Item1, boardSize.Item2);
            }

            return cells;
        }


        private (int, int) GetBoardSize(int plateSize)
        {
            return (plateSize == Constants.PLATESIZE_96) ? (8, 12) : (16, 24);
        }

        private void ValidateQpcrModel(QpcrModel model)
        {
            if (!PlateSizeIsValid(model.PlateSize))
                throw new Exception("Plate size is valid");

            if (!AllReagentsNamesAreUnique(model.NamesOfReagents))
                throw new Exception("All reagents names are not unique");

            if (model.listOfIntegers.Length != model.NamesOfReagents.Length)
                throw new Exception("List of integer can not cover reagents length");
        }

        private bool PlateSizeIsValid(int plateSize)
        {
            return (plateSize == Constants.PLATESIZE_96 ||
                    plateSize == Constants.PLATESIZE_384);
        }

        private bool AllReagentsNamesAreUnique(string[,] reAgentsList)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < reAgentsList.Rank; i++)
            {
                for (int j = 0; j < reAgentsList.GetLength(i); j++)
                {
                    try
                    {
                        if (list.Contains(reAgentsList[i, j]))
                            return false;
                        list.Add(reAgentsList[i, j]);
                    }
                    catch
                    {
                    }
                }

                list.Clear();
            }

            return true;
        }


        private void GenerateVisualizeTable(IReadOnlyCollection<CellItem> cellItems, int rows, int columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{env.NewLine}<table  border='1'>");

            for (int i = 0; i < rows; i++)
            {
                sb.Append($"{env.NewLine}<tr style='height:30px;'>");
                for (int j = 0; j < columns; j++)
                {
                    var item = cellItems.Where(x => x.Row_Coord == i).FirstOrDefault(y => y.Col_Coord == j);
                    sb.Append($"{env.NewLine}<td style='width:10px;background-color:{item?.ReAgent};'>");
                    sb.Append(item?.Name);
                    sb.Append("</td>");
                }

                sb.Append($"{env.NewLine}</tr>");
            }

            sb.Append($"{env.NewLine}</table>");

            File.WriteAllText("../../../report.html", $"<html>{env.NewLine}<body>" +
                                                      $"{env.NewLine}{sb.ToString()}" +
                                                      $"{env.NewLine}</body>" +
                                                      $"{env.NewLine}</html>");
        }
    }
}