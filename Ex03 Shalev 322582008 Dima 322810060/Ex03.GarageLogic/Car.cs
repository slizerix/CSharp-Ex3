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
        protected Car(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
        }
        public CarColor Color { get; set; }
        public int NumOfDoors { get; set; }
    }
}
