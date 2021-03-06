﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class sys_category
    {
        [Column(TypeName = "varchar(128)")]
        public string action { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string code { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string controller { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string father_code { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string icon_class { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        public bool is_menu { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string name { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string route_name { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string route_template { get; set; }
        [Column(TypeName = "int(11)")]
        public int sort { get; set; }
        [Column(TypeName = "varchar(2)")]
        public string target { get; set; }
        [Required]
        [Column(TypeName = "varchar(128)")]
        public string uid { get; set; }
    }
}
