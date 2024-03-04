using ConsoleApp1.abspak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.implpak
{
    internal interface BaseConstraintWithoutParams
    {
        public bool Judge(AbstractDesignLine designLine);
    }

    internal interface BaseConstraintWithParams
    {
        public bool Judge(AbstractDesignLine designLine,Dictionary<string, Object> keyValuePairs);
    }

    internal interface BaseConstraintWithControlPoint
    {
        public bool judge(AbstractDesignLine designLine,Dictionary<string, Object> keyValuePairs);
    }

    //修改变坡点越少越好
    internal interface BaseConstraintFix
    {
        public AbstractDesignLine Fix(AbstractDesignLine designLine, BaseConstraintWithoutParams baseConstraintWithoutParams, BaseConstraintWithParams baseConstraintWithParams, BaseConstraintWithControlPoint baseConstraintWithControlPoint);
    }
}
