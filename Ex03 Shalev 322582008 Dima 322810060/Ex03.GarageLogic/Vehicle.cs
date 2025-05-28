using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public string ModelName { get; set; }
        public string LicenseNumber { get; set; }
        public float CurrentAmount { get; set; }
        public virtual float MaxAmount { get; set; } = 0;

        public List<Wheel> Wheels = new List<Wheel>();

        public Vehicle(string i_LicenseID, string i_ModelName)
        {
            LicenseNumber = i_LicenseID;
            ModelName = i_ModelName;
        }
        public abstract string GetVhicleInfo();
        
    }
}