using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class sys_user_login
    {
        [Column(TypeName = "varchar(145)")]
        public string comment { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string ip_addr { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? logged_time { get; set; }
        [Column(TypeName = "varchar(36)")]
        public string user_id { get; set; }
    }
}
