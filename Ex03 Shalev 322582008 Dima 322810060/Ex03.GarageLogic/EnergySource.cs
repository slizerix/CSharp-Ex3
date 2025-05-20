using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public float CurrentAmount { get; set; }
        public float MaxAmount { get; set; }

        public float EnergyPercentage()
        {
            return (CurrentAmount / MaxAmount) * 100;
        }
        
    }

}
