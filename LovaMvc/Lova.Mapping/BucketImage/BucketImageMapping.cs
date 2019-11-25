using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{
    [Serializable]
    public class BucketImageMapping
    {
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ext_name { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Required]
        [Column(TypeName = "varchar(1450)")]
        public string io_path { get; set; }
        [Column(TypeName = "varchar(512)")]
        public string sha1 { get; set; }
        [Required]
        [Column(TypeName = "varchar(1450)")]
        public string visiturl { get; set; }
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string bucket_id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime creation_time { get; set; }

        #region


        public string creation_time_format => creation_time.ToString("F");

        #endregion
    }
}
