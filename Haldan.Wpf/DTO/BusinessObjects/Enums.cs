using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BusinessObjects
{
    public enum AccessType
    {
        
        Admin = 1,
        User = 2,
        ReadOnly= 3,
    }
    public enum Status
    {
        Edit = 0,
        Idle = 1,
        Testing = 2,
        Busy = 3
    }
}
