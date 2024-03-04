using ConsoleApp1.common;
using ConsoleApp1.pojo;
using ConsoleApp1.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.abspak
{
    internal class AbstractInitialPlanGeneration
    {
        protected AbstracMutation mutation = null;
        public AbstractDesignLine abstractDesignLine { get; set; }

        public AbstractInitialPlanGeneration()
        {
            abstractDesignLine = new BaseDesignLine();
        }
        public void SetMutation(AbstracMutation mutation)
        {
            this.mutation = mutation.Clone();
        }
        public void PlanGeneration()
        {
            if (mutation == null || abstractDesignLine == null)
            {
                throw new InvalidOperationException("请初始化mutation或abstractDesignLine");
            }
            abstractDesignLine.designLine.Clear();
            //设置初始状态
            AbstractChangePoint point1 = BaseContext.continueGroundLine[0].Clone();
            point1.changeable = false;
            AbstractChangePoint point2 = point1.Clone();
            AbstractChangePoint point3;
            AbstractChangePoint point4 = BaseContext.continueGroundLine[BaseContext.continueGroundLine.Length-1].Clone();
            point2.changeable = true;
            abstractDesignLine.AddChangePoint(point2);
            abstractDesignLine.UpdateWorkbench(point2, point4);
            point3 = FindFirstIntersectionPointInWorkbenchTowardRightWithLengthConstraint(point2.mileage, point4.mileage, (int)(AbstractDesignLine.minSlopeLength * 4));
            if (point3 == null)
            {
                abstractDesignLine.AddChangePoint(point4);
                return;//结束判断
            }
            point3.changeable = false;
            AbstractChangePoint mutationPoint = abstractDesignLine.workbench[ BaseRandomOpt.MultipleOf5((int)(point2.mileage + 1.5 * AbstractDesignLine.minSlopeLength), (int)(point2.mileage + 2.5 * AbstractDesignLine.minSlopeLength))].Clone();
            mutationPoint.changeable = false;
            //设置检查点
            double slopeAngleCheckPoint = (point3.elevation - point2.elevation) / (point3.mileage - point2.mileage) / 2;
            int lengthCheckPoint = (int)(point2.mileage + 3 * AbstractDesignLine.minSlopeLength);

            while (true)
            {
                if (point3 == null)
                {
                    abstractDesignLine.AddChangePoint(point4);
                    return;//结束判断
                }
                //设置变异初始环境（优化区间）
                mutation.designLine.designLine.Clear();
                mutation.designLine.designLine.Add(point1);
                mutation.designLine.designLine.Add(point2);
                mutation.designLine.designLine.Add(point3);
                //开始变异
                int mutationRes = mutation.SinglePointMutateionTowardsMinCost(mutationPoint,lengthCheckPoint,slopeAngleCheckPoint);
                if (mutationRes == 0)
                {
                    //状态切换
                    point2 = mutation.mutationPoint;
                    abstractDesignLine.AddChangePoint(point2);
                    point1 = FindPreChangePointIntersectionPoint(point2.mileage);
                    abstractDesignLine.UpdateWorkbench(point2, point4);
                    point3 = FindFirstIntersectionPointInWorkbenchTowardRightWithLengthConstraint(point2.mileage, point4.mileage, (int)(AbstractDesignLine.minSlopeLength * 4));
                    mutationPoint = abstractDesignLine.workbench[BaseRandomOpt.MultipleOf5((int)(point2.mileage + 1.5 * AbstractDesignLine.minSlopeLength), (int)(point2.mileage + 2.5 * AbstractDesignLine.minSlopeLength))].Clone();
                    lengthCheckPoint = (int)(point2.mileage + 3 * AbstractDesignLine.minSlopeLength);
                }
                else if(mutationRes == 1)
                {
                    mutationPoint = mutation.mutationPoint;
                    abstractDesignLine.UpdateWorkbench(mutationPoint, point4);
                    point3 = FindFirstIntersectionPointInWorkbenchTowardRightWithLengthConstraint(mutationPoint.mileage, point4.mileage, (int)(AbstractDesignLine.minSlopeLength * 2));
                    lengthCheckPoint = (int)(mutationPoint.mileage + AbstractDesignLine.minSlopeLength);
                }else if(mutationRes == 2)
                {
                    mutationPoint= mutation.mutationPoint;
                    double currentAngle = (mutationPoint.elevation - point2.elevation) / (mutationPoint.mileage - point2.mileage);
                    double endElevation = (point4.mileage-point2.mileage)*currentAngle+point2.elevation;
                    AbstractChangePoint temp = new BaseChangePoint();
                    temp.mileage=point4.mileage;
                    temp.elevation=endElevation;
                    abstractDesignLine.UpdateWorkbench(mutationPoint, temp);
                    point3 = FindFirstIntersectionPointInWorkbenchTowardRightWithLengthConstraint(mutationPoint.mileage, temp.mileage, (int)(AbstractDesignLine.minSlopeLength * 2));
                    if (point3 == null)
                    {
                        point2 = mutation.mutationPoint;
                        abstractDesignLine.AddChangePoint(point2);
                        point1 = FindPreChangePointIntersectionPoint(point2.mileage);
                        abstractDesignLine.UpdateWorkbench(point2, point4);
                        point3 = FindFirstIntersectionPointInWorkbenchTowardRightWithLengthConstraint(point2.mileage, point4.mileage, (int)(AbstractDesignLine.minSlopeLength * 4));
                        mutationPoint = abstractDesignLine.workbench[BaseRandomOpt.MultipleOf5((int)(point2.mileage + 1.5 * AbstractDesignLine.minSlopeLength), (int)(point2.mileage + 2.5 * AbstractDesignLine.minSlopeLength))].Clone();
                        lengthCheckPoint = (int)(point2.mileage + 3 * AbstractDesignLine.minSlopeLength);
                    }
                    else
                    {
                        lengthCheckPoint = (int)(mutationPoint.mileage + AbstractDesignLine.minSlopeLength);
                    }
                }
            }

        }

        public AbstractChangePoint FindFirstIntersectionPointInWorkbenchTowardRightWithLengthConstraint(int start, int end ,int lengthConstraint)
        {
            start  = start+lengthConstraint;
            if(start>end) { return null; }
            while (true) 
            {
                if (abstractDesignLine.workbench[start].intersectionPoint == true)
                {
                    if(start<end&&abstractDesignLine.workbench[start+1].intersectionPoint == true&&new Random().Next(1,2)==1)return abstractDesignLine.workbench[start + 1].Clone();
                    return abstractDesignLine.workbench[start + 1].Clone() ;
                }
                start++;
            }
        }

        public AbstractChangePoint FindPreChangePointIntersectionPoint(int position)
        {
            while (true)
            {
                if (abstractDesignLine.continueDesignLine[position].intersectionPoint == true)
                {
                    if (position >0 && abstractDesignLine.continueDesignLine[position - 1].intersectionPoint == true) return abstractDesignLine.continueDesignLine[position - 1].Clone();
                    return abstractDesignLine.continueDesignLine[position - 1].Clone();
                }
                position--;
            }
        }


    }
}
