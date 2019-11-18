using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class sys_user_role
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string role_id { get; set; }
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string user_id { get; set; }
    }
}
