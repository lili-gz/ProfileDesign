using ConsoleApp1.abspak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.implpak
{
    internal interface BaseCostWithoutParams
    {
        public double Cost(AbstractDesignLine line, int start, int end);
    }

    internal interface BaseCostWithParams
    {
        public double Cost(AbstractDesignLine line, int start, int end, Dictionary<string, Object> keyValuePairs);
    }


}
