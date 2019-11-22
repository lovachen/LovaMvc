using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{

    [Serializable]
    public class Sys_NLogMapping
    {
        [Column(TypeName = "text")]
        public string callsite { get; set; }
        [Column(TypeName = "varchar(450)")]
        public string category { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string clientip { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string eventid { get; set; }
        [Column(TypeName = "text")]
        public string exception { get; set; }
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string level { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? logged { get; set; }
        [Column(TypeName = "text")]
        public string logger { get; set; }
        [Column(TypeName = "text")]
        public string message { get; set; }
        [Column(TypeName = "text")]
        public string properties { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string traceid { get; set; }
        [Column(TypeName = "varchar(450)")]
        public string user { get; set; }


        #region

        /// <summary>
        /// 
        /// </summary>
        public string logged_format => logged?.ToString("F");

        #endregion
    }
}
