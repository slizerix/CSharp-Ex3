using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar() => EnergySource = new ElectricEnergy(4.8f);
        public override string GetSpecificDetails() => $"Electric Car: {Color}, {NumOfDoors} doors";
    }
}
