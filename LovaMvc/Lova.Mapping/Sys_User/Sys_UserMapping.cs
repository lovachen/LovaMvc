using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{
    [Serializable]
    public class Sys_UserMapping
    {
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string account { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime creation_time { get; set; }
        [Column(TypeName = "varchar(36)")]
        public string creator { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? deleted_time { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        public bool is_admin { get; set; }
        public bool is_deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? last_activity_time { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string last_ipaddr { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string name { get; set; }
        [Required]
        [Column(TypeName = "varchar(512)")]
        public string password { get; set; }
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string salt { get; set; }
    }
}
