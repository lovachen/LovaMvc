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
                var com = connection.CreateCommand();
                com.CommandText = $"select table_name from information_schema.tables where table_schema='{connection.Database}';";
                var dr = com.ExecuteReader();
                List<string> tables = new List<string>();

                while (dr.Read())
                {
                    tables.Add(dr["table_name"].ToString());
                }
                dr.Close();
                connection.Close(); 
                 
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
