using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_2._1.Models
{
    internal class Conversor
    {
        public string TimeLastUpdateOne { get; set; }
        public string TimeLastUpdateSecond { get; set; }
        public string TimeNextUpdateOne { get; set; }
        public string TimeNextUpdateSecond { get; set; }
        public string BaseCode { get; set; }
        public string TargetCode { get; set; }
        public double ConversionRate { get; set; }
        public bool Concrete { get; set; }

        public Conversor()
        {
           Concrete = false; 
        }

        public static implicit operator Conversion(Conversor v)
        {
            throw new NotImplementedException();
        }
    }
}
