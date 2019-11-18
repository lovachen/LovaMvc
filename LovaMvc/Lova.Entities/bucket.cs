using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class bucket
    {
        [Column(TypeName = "varchar(45)")]
        public string bucketcol { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime creation_time { get; set; }
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string creator { get; set; }
        [Column(TypeName = "varchar(450)")]
        public string description { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        public bool is_compress { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string name { get; set; }
    }
}
