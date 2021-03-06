﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{
    [Serializable]
    public class Sys_PermissionMapping
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
