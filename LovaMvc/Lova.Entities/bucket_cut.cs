using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class bucket_cut
    {
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string bucket_id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime creation_time { get; set; }
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string creator { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Required]
        [Column(TypeName = "varchar(145)")]
        public string value { get; set; }
    }
}
