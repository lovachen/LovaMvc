using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class sys_user_jwt
    {
        [Column(TypeName = "datetime")]
        public DateTime expiration { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Column(TypeName = "int(11)")]
        public int platform { get; set; }
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string user_id { get; set; }
    }
}
