using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        public ElectricMotorcycle() => EnergySource = new ElectricEnergy(3.2f);
        public override string GetSpecificDetails() => $"Electric Motorcycle: {LicenseType}, {EngineVolume}cc";
    }
}
