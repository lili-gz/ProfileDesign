using ConsoleApp1.implpak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.abspak
{
    internal abstract class AbstractConstraint
    {
        protected List<BaseConstraintWithControlPoint> constraintWithControlPoints;
        protected List<BaseConstraintWithoutParams> constraintWithoutParams;
        protected List<BaseConstraintWithParams> constraintWithParams;
        protected AbstractConstraint(List<BaseConstraintWithControlPoint> constraintWithControlPoints, List<BaseConstraintWithoutParams> constraintWithoutParams, List<BaseConstraintWithParams> constraintWithParams)
        {
            this.constraintWithControlPoints = constraintWithControlPoints;
            this.constraintWithoutParams = constraintWithoutParams;
            this.constraintWithParams = constraintWithParams;
        }

        public abstract bool JudgeWithoutControlPoint(AbstractDesignLine line, int start, int end);
        public abstract bool JudgeWithControlPoint(AbstractDesignLine line, int start, int end);
        public abstract bool ConstraintFix(AbstractDesignLine line, int start, int end);
    }
}
