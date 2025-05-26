using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            Manufacturer = i_Manufacturer;
            CurrentAirPressure = i_CurrentAirPressure;
            MaxAirPressure = i_MaxAirPressure;
        }
        public string Manufacturer { get; set; }
        public float CurrentAirPressure { get; set; }
        public float MaxAirPressure { get; set; }
        public void Inflate(float i_AirToAdd)
        {
            if (CurrentAirPressure + i_AirToAdd > MaxAirPressure)
                throw new ValueOutOfRangeException(0, MaxAirPressure - CurrentAirPressure, "Air exceeds max pressure.");

            CurrentAirPressure += i_AirToAdd;
        }
    }
}
