using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{
    [Serializable]
    public class Sys_SettingMapping
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Required]
        [Column(TypeName = "varchar(64)")]
        public string name { get; set; }
        [Required]
        [Column(TypeName = "varchar(450)")]
        public string value { get; set; }
    }
}
