using ConsoleApp1.abspak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.implpak
{
    internal interface BaseExchanging
    {
        public AbstractDesignLine Exchanging(AbstractDesignLine line1, AbstractDesignLine line2, double rate, BaseConstraintFix baseConstraint);
    }
}
