using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class sys_setting
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
