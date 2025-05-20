using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        public CarColor Color { get; set; }
        public int NumOfDoors { get; set; }
    }
}
