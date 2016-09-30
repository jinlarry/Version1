using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Version1.Models
{
    public class ZeroRabbishRoute
    {   [Key]
        [MaxLength(50)]
        public string RouteID { get; set; }
        [MaxLength(50)]
        public string PathColor { get; set; }
        public DateTime Createdate { get; set; }
         [Required]
        public string CreateUserID { get; set; }
        public string RouteNote { get; set; }       
        public List<ZeroRabbishRoutePoint> Points { get; set; }      
        public ApplicationUser user { get; set; }
    }
    public class ZeroRabbishRoutePoint
    {
        [Key]
        [MaxLength(50)]
        public string PointID { get; set; }
        public string PostalAddress { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [MaxLength(50)]
        public string RouteID { get; set; }
        public ZeroRabbishRoute ZRoute { get; set; }

    }
}
