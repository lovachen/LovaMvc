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
    public class Sys_RoleMapping
    {
        [Column(TypeName = "datetime")]
        public DateTime creation_time { get; set; }
        [Column(TypeName = "varchar(36)")]
        public string creator { get; set; }
        [Column(TypeName = "varchar(450)")]
        public string description { get; set; }
        [Key]
        [Column(TypeName = "varchar(36)")]
        public string id { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string name { get; set; }

        #region

        /// <summary>
        /// 
        /// </summary>
        public List<Sys_UserMapping> SysUsers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Sys_PermissionMapping> SysPermissions { get; set; }

        #endregion
    }
}
