using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class sys_permission
    {
        [Column(TypeName = "varchar(36)")]
        public string category_id { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Column(TypeName = "varchar(36)")]
        public string role_id { get; set; }
    }
}
