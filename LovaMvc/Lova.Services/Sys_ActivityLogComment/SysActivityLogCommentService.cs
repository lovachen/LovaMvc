using Lova.Entities;
using Lova.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Lova.Services
{

    public class SysActivityLogCommentService
    {
        LovaDbContext _dbContext;

        public SysActivityLogCommentService(LovaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<Sys_ActivityLogCommentMapping> GetActivityLogComments()
        {
            return _dbContext.sys_activitylog_comment.Select(item => new Sys_ActivityLogCommentMapping()
            {
                entity_name = item.entity_name,
                comment = item.comment
            }).ToList();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                connection.Open();
                var table = connection.GetSchema("Tables");
                connection.Close(); 
                List<string> tables = new List<string>();
                foreach (DataRow row in table.Rows)
                {
                   var sc = row["TABLE_SCHEMA"].ToString();

                    tables.Add(row["TABLE_NAME"].ToString());
                }
                var ac_comments = _dbContext.sys_activitylog_comment.ToList();
                ac_comments.ForEach(del =>
                {
                    if (!tables.Any(o => o == del.entity_name))
                    {
                        _dbContext.sys_activitylog_comment.Remove(del);
                    }
                });
                tables.ForEach(name =>
                {
                    if (!ac_comments.Any(o => o.entity_name == name))
                    {
                        _dbContext.sys_activitylog_comment.Add(new sys_activitylog_comment()
                        {
                            entity_name = name
                        });
                    }
                });
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Update(string name, string value)
        {
            _dbContext.Database.ExecuteSqlRaw($"UPDATE sys_activitylog_comment SET Comment='{value}' WHERE entity_name ='{name}'");
        }

    }
}
