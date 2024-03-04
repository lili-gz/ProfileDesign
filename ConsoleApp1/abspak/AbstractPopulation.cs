using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.abspak
{
    internal abstract class AbstractPopulation
    {
        protected List<AbstractDesignLine> lines=new List<AbstractDesignLine>();
        protected int size;
        public abstract void InitPopulation(AbstractDesignLine line, int size);

        public List<AbstractDesignLine> GetLines()
        {
            return lines;
        }

        public void AddDesignLine(AbstractDesignLine line)
        {
            lines.Add(line);
        }

        public abstract void Update(AbstractPopulation abstractPopulation);
    }
}
