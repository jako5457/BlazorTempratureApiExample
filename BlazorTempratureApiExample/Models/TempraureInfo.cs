using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blazor_Radzen_Data_Example.Shared
{
    public class TempraureInfo
    {
        public double TempratureC { get; set; }

        public DateTime Date { get; set; }

        [JsonIgnore]
        public double TempratureF 
        { 
            get { return (TempratureC * 1.8) + 32; } 
            set { TempratureC = (value - 32) / 1.8; } 
        }

        [JsonIgnore]
        public double TempratureK 
        { 
            get { return TempratureC + 273.15; }
            set { TempratureC = value - 273.15; } 
        }
    }
}
