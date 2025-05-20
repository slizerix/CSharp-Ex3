using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Utils
    {
        // Enums
        public enum FuelType { Octan95, Octan96, Octan98, Soler }
        public enum LicenseType { A, A2, AB, B2 }
        public enum CarColor { Yellow, White, Black, Silver }
        public enum VehicleStatus { InRepair, Repaired, Paid }

        // Custom Exception
        public class ValueOutOfRangeException : Exception
        {
            public float MinValue { get; }
            public float MaxValue { get; }

            public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message)
                : base(i_Message)
            {
                MinValue = i_MinValue;
                MaxValue = i_MaxValue;
            }
        }
    }
}
