using ConsoleApp1.abspak;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.pojo
{

    [Serializable]
    internal class BaseChangePoint:AbstractChangePoint
    {
        public BaseChangePoint() 
        {
            changeable = false;
            intersectionPoint = false;
            elevation = -1;
            mileage = 0;
        }

        public BaseChangePoint(bool changeble,bool intersectionPoint,double elevation,int mileage)
        {
            this.changeable = changeble;
            this.intersectionPoint = intersectionPoint;
            this.elevation =elevation;
            this.mileage =mileage;
        }

        public override AbstractChangePoint Clone()
        {
            string json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<BaseChangePoint>(json);
        }
    }

    [Serializable]
    internal class BaseDesignLine : AbstractDesignLine
    {
        public BaseDesignLine() :base() { }

        public override AbstractDesignLine Clone()
        {
            string json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<BaseDesignLine>(json);
        }
    }
}
