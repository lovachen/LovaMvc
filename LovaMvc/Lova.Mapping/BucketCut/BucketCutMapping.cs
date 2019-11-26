using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BucketCutMapping
    {
        [Required]
        [Column(TypeName = "varchar(36)")]
        public string bucket_id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime creation_time { get; set; }
        [Column(TypeName = "varchar(36)")]
        public string creator { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Required]
        [Column(TypeName = "varchar(145)")]
        public string value { get; set; }

        #region

        /// <summary>
        /// 
        /// </summary>
        public string bucket_name { get; set; }

        #endregion
    }
}
