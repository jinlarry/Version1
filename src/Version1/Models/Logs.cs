using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Version1.Models
{
    public class OperationLogs
    {
        [Required][MaxLength(50)] 
        public string ID { get; set; }
        [MaxLength(50)]
        public string UserID { get; set; }
        [MaxLength(200)]
        public string Controller { get; set; }
        [MaxLength(200)]
        public string Action { get; set; }        
        [MaxLength(450)]
        public string Operation { get; set; }
        public DateTime OperationTime { get; set; }
        [MaxLength(100)]
        public string OperationType { get; set; }
        [MaxLength(450)]
        public string OperationResult { get; set; }
        [Timestamp]
        public Byte[] LogTimeStamp { get; set; }
    }
}



