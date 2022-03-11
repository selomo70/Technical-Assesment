using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Communication
    {
        public int Id { get; set; }
        public string Action { get; set; }

        public string Result { get; set; }

        public DateTime? CreatedAt { get; set; }
        public Communication() : base()
        {
            this.Action = string.Empty;
            this.CreatedAt = DateTime.Now;
            this.Result = string.Empty;
        }
    }
}
