using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApparts.Classes
{
    public partial class RegionApps : Appartment
    {
        protected string Region { get; set; }
        public string RegionName
        {
            get { return Region; }
            set { Region = value; }
        }
    }
}
