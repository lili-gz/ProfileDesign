using ConsoleApp1.common;
using ConsoleApp1.utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.abspak
{
    [Serializable]
    internal abstract class AbstractDesignLine
    {
        public List<AbstractChangePoint> designLine = new List<AbstractChangePoint>();
        public AbstractChangePoint[] continueDesignLine;
        public AbstractChangePoint[] workbench;
        public static double minSlopeLength = 0;
        public static double maxSlopeAngle = 0;
        public static int accuracy = 2;//取小数点后几位
        public double cost = 0;

        public AbstractDesignLine()
        {
            continueDesignLine = BaseContext.GenerateEmptyArrays();
            workbench = BaseContext.GenerateEmptyArrays();
        }

        public void AccuracyFix()
        {
            //TODO 对坡度进行精度修整 ,删除前后坡度过小的点
        }

        

        public void UpdateWorkbench()
        {
            foreach (var item in designLine)
            {
                workbench[item.mileage].elevation = item.elevation;
            }
            BaseContext.GenerateContinueLine(workbench, designLine[0].mileage, designLine[designLine.Count-1].mileage);
        }

        public void UpdateWorkbench(AbstractChangePoint start, AbstractChangePoint end)
        {
            workbench[start.mileage].elevation=start.elevation;
            workbench[end.mileage].elevation = end.elevation;
            BaseContext.GenerateContinueLine(workbench, start.mileage, end.mileage);
        }

        public void AddChangePoint(AbstractChangePoint point)
        {
            int start = 0;
            if(designLine.Count>0)start = designLine[designLine.Count - 1].mileage;
            designLine.Add(point.Clone());
            BaseContext.GenerateContinueLine(continueDesignLine, start, point.mileage);
        }

        public void InitStartAndEnd()
        {
            designLine.Add(BaseContext.continueGroundLine[0].Clone());
            designLine.Add(BaseContext.continueGroundLine[BaseContext.continueGroundLine.Length-1].Clone());
            UpdateWorkbench(0, BaseContext.continueGroundLine.Length - 1);
        }
        public void UpdateWorkbench(int start, int end)
        {
            foreach (var item in designLine)
            {
                if(item.mileage>start)workbench[item.mileage].elevation = item.elevation;
            }
            BaseContext.GenerateContinueLine(workbench, start, end);
        }


        public void SetCost(double cost) 
        {
            this.cost = cost;
        }

        public double GetCost()
        {
            return cost;
        }

        public void SetdesignLine(List<AbstractChangePoint> designLine)
        {
            this.designLine = designLine; 
        }

        public int GetDesignLineLength()
        {
            return designLine.Count;
        }

        public AbstractChangePoint GetChangePoint(int index)
        {
            return (AbstractChangePoint)designLine[index];
        }

        public void UpdateChangePoint(int index, AbstractChangePoint changePoint)
        {
            designLine[index] = changePoint.Clone();
        }

        public abstract AbstractDesignLine Clone();
        //{
        //    string json = JsonConvert.SerializeObject(this);
        //    return JsonConvert.DeserializeObject<AbstractDesignLine>(json);
        //}
    }

    [Serializable]
    internal abstract class AbstractChangePoint
    {
        public bool changeable { get; set; }
        public bool intersectionPoint { get; set; }
        public double elevation { get; set; }
        public int mileage { get; set; }
        public AbstractChangePoint() 
        {
        }

        public bool Changeable()
        {
            return changeable;
        }

        public void SetChangeable(bool changeable)
        {
            this.changeable = changeable;
        }

        public double Elevation()
        {
            return elevation;
        }

        public void SetElevation(double elevation)
        {
            this.elevation = elevation;
        }

        public int Mileage()
        {
            return mileage;
        }

        public void SetMileage(int mileage)
        {
            this.mileage = mileage;
        }

        public bool IntersectionPoint()
        {
            return intersectionPoint;
        }

        public void SetIntersectionPoint(bool intersectionPoint)
        {
            this.intersectionPoint = intersectionPoint;
        }

        public  abstract AbstractChangePoint Clone();
        //{
        //    string json = JsonConvert.SerializeObject(this);
        //    return JsonConvert.DeserializeObject<AbstractChangePoint>(json);
        //}
    }
}
