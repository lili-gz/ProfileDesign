using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.utils
{
    internal class BaseFileOpt
    {
        public static List<double[]> ReadCsvFile(string filePath)
        {
            List<double[]> dataList = new List<double[]>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // 跳过第一行
                    sr.ReadLine();

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] columns = line.Split(',');

                        if (columns.Length >= 2)
                        {
                            // 尝试解析第一列和第二列为 double
                            if (double.TryParse(columns[0], out double column1) &&
                                double.TryParse(columns[1], out double column2))
                            {
                                dataList.Add(new double[] { column1, column2 });
                            }
                            else
                            {
                                Console.WriteLine($"Skipping invalid line: {line}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Skipping invalid line: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return dataList;
        }
    }

    class BaseRandomOpt
    {
        public static double GenerateRandomDoubleInRange(double minValue, double maxValue)
        {
            Random random = new Random();
            // 通过数学运算生成指定范围内的随机浮点数
            return minValue + (random.NextDouble() * (maxValue - minValue));
        }

        public static int MultipleOf5(int start, int end)
        {
            Random random = new Random(); ;
            int randomNumber = random.Next(start, end + 1);
            return (randomNumber / 5) * 5;
        }
    }

}
