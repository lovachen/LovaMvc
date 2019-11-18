using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class sys_activitylog_comment
    {
        [Column(TypeName = "varchar(145)")]
        public string comment { get; set; }
        [Key]
        [Column(TypeName = "varchar(100)")]
        public string entity_name { get; set; }
    }
}
