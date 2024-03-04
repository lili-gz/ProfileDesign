using ConsoleApp1.pojo;
using ConsoleApp1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.abspak
{
    [Serializable]
    internal abstract class AbstracMutation
    {
        public AbstractCost costCalculate;
        public AbstractDesignLine designLine;
        public AbstractConstraint constraint;
        public static bool containIntersectionPoint = true;
        public AbstractChangePoint mutationPoint;
        public AbstracMutation(AbstractCost costCalculate, AbstractDesignLine designLine, AbstractConstraint constraint)
        {
            this.costCalculate = costCalculate;
            this.designLine = designLine.Clone();
            this.constraint = constraint;
        }

        //成本最低变异
        //public abstract void MutateWithoutControl(); 

        //public abstract void MutateWithControl();

        public int SinglePointMutateionTowardsMinCost(AbstractChangePoint mutationPoint, int lenthCheckPoint, double slopeAnglePoint)
        {
            this.mutationPoint = mutationPoint.Clone();
            return 0;//0：正常变异结束；1：超过长度检查点；2：超过坡度检查点
        }

        public static AbstractChangePoint SinglePoinMutation(int index)
        {
            AbstractChangePoint temp = designLine.designLine[index].Clone();
            double preSlopeAngle = (designLine.designLine[index].Elevation()- designLine.designLine[index-1].Elevation())/(designLine.designLine[index].Mileage()- designLine.designLine[index-1].Mileage());
            int flag = new Random().Next(0, 2);
            if (flag == 1)
            {
                temp.SetMileage(designLine.designLine[index].Mileage()-5);
            }else if (flag == 2)
            {
                temp.SetMileage(designLine.designLine[index].Mileage() - 5);
            }
            else
            {
                double newSlopAngle = BaseRandomOpt.GenerateRandomDoubleInRange(-preSlopeAngle/2, preSlopeAngle/2);
                double newElevation = newSlopAngle * (designLine.designLine[index].Mileage() - designLine.designLine[index - 1].Mileage());
                temp.SetElevation(newElevation);
            }

            return temp;
        }


        public abstract AbstracMutation Clone();

    }
}
