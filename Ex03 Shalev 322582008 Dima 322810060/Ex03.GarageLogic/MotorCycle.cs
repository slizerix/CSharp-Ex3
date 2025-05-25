using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        protected Motorcycle(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
        }
        public LicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }

    }
}
