using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public string ModelName { get; set; }
        public string LicenseNumber { get; set; }
        public List<Wheel> Wheels = new List<Wheel>();

        public Vehicle(string i_LicenseID, string i_ModelName)
        {
            LicenseNumber = i_LicenseID;
            ModelName = i_ModelName;
        }
    }
}
