using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(float i_MaxHours)
        {
            MaxAmount = i_MaxHours;
            CurrentAmount = 0;
        }

        public void Recharge(float i_Hours)
        {
            if (CurrentAmount + i_Hours > MaxAmount)
                throw new ValueOutOfRangeException(0, MaxAmount - CurrentAmount, "Charge exceeds capacity.");

            CurrentAmount += i_Hours;
        }
    }
}
