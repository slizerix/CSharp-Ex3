using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        public Vehicle Vehicle { get; }
        public string OwnerName { get; }
        public string OwnerPhone { get; }
        public VehicleStatus Status { get; set; }

        public VehicleInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            Vehicle = i_Vehicle;
            OwnerName = i_OwnerName;
            OwnerPhone = i_OwnerPhone;
            Status = VehicleStatus.InRepair;
        }
    }
}
