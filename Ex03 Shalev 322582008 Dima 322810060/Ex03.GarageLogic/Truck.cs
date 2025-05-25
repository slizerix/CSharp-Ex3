using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public bool CarriesHazardousMaterials { get; set; }
        public float CargoVolume { get; set; }
        public float CurrentAmount { get; set; }
        public float MaxAmount { get; set; }
        public FuelType FuelType { get; private set; }
        public void Refuel(float i_Amount, FuelType i_Type)
        {
            if (i_Type != FuelType)
                throw new ArgumentException("Incorrect fuel type.");
            if (CurrentAmount + i_Amount > MaxAmount)
                throw new ValueOutOfRangeException(0, MaxAmount - CurrentAmount, "Fuel exceeds tank capacity.");

            CurrentAmount += i_Amount;
        }
        public float EnergyPercentage()
        {
            return (CurrentAmount / MaxAmount) * 100;
        }
    }
}
