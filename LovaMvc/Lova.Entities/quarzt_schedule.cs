using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lova.Entities
{
    public partial class quarzt_schedule
    {
        [Column(TypeName = "varchar(64)")]
        public string cron_express { get; set; }
        [Column(TypeName = "int(11)")]
        public int? data_status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? end_run_time { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string job_group { get; set; }
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string job_name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? job_run_time { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? nex_run_time { get; set; }
        [Column(TypeName = "int(11)")]
        public int run_status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? start_run_time { get; set; }
        [Column(TypeName = "varchar(450)")]
        public string task_description { get; set; }
    }
}
