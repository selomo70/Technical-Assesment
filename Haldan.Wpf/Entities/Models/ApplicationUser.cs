using DTO.BusinessObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    
    public class ApplicationUser 
    {

        public int Id { get; set; }
        public virtual string Password
        {
            get;
            set;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public bool Registered { get{ return !string.IsNullOrEmpty(Username); } }
        public DateTime LastLogin { get; set; }
        public AccessType AccessLevelId { get; set; }



        public ApplicationUser() : base()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Password = string.Empty;
            this.Username = string.Empty;
            this.LastLogin = DateTime.Now;
        }
    }
}
