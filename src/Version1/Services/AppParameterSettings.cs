using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
namespace Version1.Services
{
    public   class AppParameterSettings
    {
        public List<string> CensorUserRole { get; set; } //define which user role can access news censor function
        public List<string> ManagementRoles { get; set; }//define what kind of user role can access back-end management function   
        public Boolean NewsAutoCensor { get; set; }//if value=true then news will be automatically censored when news be created
    }
     
}
