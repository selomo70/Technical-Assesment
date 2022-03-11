using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ResponseRegister
    {
        public bool status { get; set; }
        public int errors { get; set; }
        public string message { get; set; }
    }
}
