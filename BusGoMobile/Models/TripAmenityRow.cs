using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusGoMobile.Models
{
    // JOIN sonucunu taşımak için yardımcı sınıf
    public class TripAmenityRow
    {
        public int TripId { get; set; }
        public string Name { get; set; }
        public string IconCode { get; set; }
    }
}
