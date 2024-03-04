using ConsoleApp1.abspak;
using ConsoleApp1.pojo;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.common
{
    internal class BaseContext
    {
        public static List<double[]> groundLine;
        public static AbstractChangePoint[] continueGroundLine;
        private static int length=-1;

        public static void GenerateContinueGroundLine()
        {
            
            int end = (int)Math.Round(groundLine[groundLine.Count - 1][0]);
            length = end+1;
            continueGroundLine = GenerateEmptyArrays();
            foreach (var item in groundLine)
            {
                int position = (int)Math.Round(item[0]);
                continueGroundLine[position].SetElevation(item[1]);
            }

            GenerateContinueLine(continueGroundLine, 0, end);
        }


        //生成连续线
        public static void GenerateContinueLine(AbstractChangePoint[] line,int start, int end,int judgeType=1)
        {
            int flag = start;
            int nextPoint;
            while (true)
            {
                if(judgeType==1)
                {
                    while (true)
                    {
                        if (line[flag].Elevation() != -1) break;
                        flag++;
                    }
                    nextPoint = flag + 1;
                    while (true)
                    {
                        if (line[nextPoint].Elevation() != -1) break;
                        nextPoint++;
                    }
                }
                else
                {
                    while (true)
                    {
                        if (line[flag].changeable==true) break;
                        flag++;
                    }
                    nextPoint = flag + 1;
                    while (true)
                    {
                        if (line[nextPoint].changeable==true) break;
                        nextPoint++;
                    }
                }
               
                double stepValue = (line[nextPoint].Elevation() - line[flag].Elevation()) / (nextPoint - flag);
                for (int i = flag + 1; i < nextPoint; i++)
                {
                    double newElevation = line[flag].Elevation() + stepValue * (i - flag);
                    line[i].SetElevation(newElevation);
                }
                flag = nextPoint;
                if(flag==end) break;
            }

            IterateOverIntersectionPoints(line, start, end);
        }

        public static void IterateOverIntersectionPoints(AbstractChangePoint[] line, int start, int end)
        {
            bool preSymbol = true;
            bool posSymbol = true;
            if (line[start].Elevation() == continueGroundLine[start].Elevation())
            {
                line[start].intersectionPoint = true;
                start++;
            }
            preSymbol = (line[start].elevation - continueGroundLine[start].elevation) >= 0;
            posSymbol = preSymbol;

            while (start<=end)
            {
                if (line[start].Elevation() == continueGroundLine[start].Elevation())
                {
                    line[start].intersectionPoint = true;
                    if(start<end) preSymbol = (line[start+1].elevation - continueGroundLine[start+1].elevation) > 0;
                    posSymbol = preSymbol;
                }
                else
                {
                    posSymbol = (line[start + 1].elevation - continueGroundLine[start + 1].elevation) > 0;
                }
                if (preSymbol != posSymbol)
                {
                    line[start].intersectionPoint = true;
                    line[start + 1].intersectionPoint = true;
                    preSymbol = posSymbol;
                }
                start++;
            }

        }


        public static AbstractChangePoint[] GenerateEmptyArrays()
        {
            if (length == -1) return null;
            AbstractChangePoint[] points = new AbstractChangePoint[length];
            for (int i = 0; i < length; i++)
            {
                points[i] = new BaseChangePoint();
                points[i].mileage = i;
            }

            return points;
        }
    }
}
