using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Haldan.Logic.BusinessLogic
{
    public  interface IApplicationUserLogic
    {
       bool SaveUser(ApplicationUser user);
   
        public  Task<ResponseRegister> PostRegUser(ApplicationUser requestObj);
    }
}
