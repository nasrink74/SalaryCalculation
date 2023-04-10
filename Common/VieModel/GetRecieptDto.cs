using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.VieModel
{
    public class GetRecieptDto
    {
        public long BasicSalary { get; set; }       //حقوقو پایه
        public long Allowance { get; set; }         //حق جذب
        public long Transportation { get; set; }    //حق ایاب و ذهاب
        public string OverTimeCalculator { get; set; }
    }
}
