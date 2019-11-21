using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{
    [Serializable]
    public class Sys_ActivityLogCommentMapping
    {
        [Column(TypeName = "varchar(145)")]
        public string comment { get; set; }
        [Key]
        [Column(TypeName = "varchar(100)")]
        public string entity_name { get; set; }
    }
}
