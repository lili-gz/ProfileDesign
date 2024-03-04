using ConsoleApp1.implpak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.abspak
{
    internal abstract class AbstractCost
    {
        protected List<BaseCostWithParams> costWithParams;
        protected List<BaseCostWithoutParams> costWithoutParams;

        protected AbstractCost(List<BaseCostWithParams> costWithParams, List<BaseCostWithoutParams> costWithoutParams)
        {
            this.costWithParams = costWithParams;
            this.costWithoutParams = costWithoutParams;
        }

        public abstract double Calculate(AbstractDesignLine line,int start, int end);

    }
}
