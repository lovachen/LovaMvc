using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lova.Mapping
{
    [Serializable]
    public class Sys_ActivityLogMapping
    {
        [Column(TypeName = "varchar(145)")]
        public string comment { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? creation_time { get; set; }
        [Column(TypeName = "varchar(36)")]
        public string creator { get; set; }
        [Column(TypeName = "varchar(145)")]
        public string entity_name { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string method { get; set; }
        [Column(TypeName = "text")]
        public string newvalue { get; set; }
        [Column(TypeName = "text")]
        public string oldvalue { get; set; }
        [Column(TypeName = "varchar(64)")]
        public string primary_key { get; set; }






        #region
        public string user_name { get; set; }

        public string user_account { get; set; }



        #endregion
    }
}
