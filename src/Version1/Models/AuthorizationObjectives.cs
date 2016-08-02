using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class Authorization_Object
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Authorization Name")]
        [Required,MaxLength(200, ErrorMessage = "Name of Object Authorized must be 200 characters or less")]
        public string ObjectName { get; set; }

        [Display(Name = "Authorization Description")]
        public string ObjectDescription { get; set; }

        [Display(Name = "Authorization Type")]
        [Required, MaxLength(50, ErrorMessage = "Type of Object Authorized must be 50 characters or less")]
        public string ObjectType { get; set; }

        [Display(Name = "Controller")]
        [  MaxLength(150, ErrorMessage = "Function Controller-name must be 150 characters or less")]
        public string FullControllerName { get; set; }  //the Fullcontrollername includes Area name

        [Display(Name = "Action")]
        [  MaxLength(150, ErrorMessage = "Function Action-name must be 50 characters or less")]
        public string ActionName { get; set; }

    //    public List<Authorization_Object_Role> Authorization_Object_Roles { get; set; }
     //   public Authorization_Object()
      //  {
       //     Authorization_Object_Roles = new List<Authorization_Object_Role>();
       // }

    }
    public class Authorization_Object_Role
    {
        //[Key]
        //  public int ID { get; set; }
        [Required]
        public  string RoleID { get; set; }

        [Required]
        public int Authorization_Object_ID { get; set; }  
         
    }

}
