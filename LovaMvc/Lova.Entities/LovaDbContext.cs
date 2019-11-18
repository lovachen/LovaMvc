using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lova.Entities
{
    public partial class LovaDbContext : DbContext
    {
        public LovaDbContext()
        {
        }

        public LovaDbContext(DbContextOptions<LovaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<bucket> bucket { get; set; }
        public virtual DbSet<bucket_cut> bucket_cut { get; set; }
        public virtual DbSet<bucket_image> bucket_image { get; set; }
        public virtual DbSet<quarzt_schedule> quarzt_schedule { get; set; }
        public virtual DbSet<sys_activitylog> sys_activitylog { get; set; }
        public virtual DbSet<sys_activitylog_comment> sys_activitylog_comment { get; set; }
        public virtual DbSet<sys_category> sys_category { get; set; }
        public virtual DbSet<sys_nlog> sys_nlog { get; set; }
        public virtual DbSet<sys_permission> sys_permission { get; set; }
        public virtual DbSet<sys_role> sys_role { get; set; }
        public virtual DbSet<sys_setting> sys_setting { get; set; }
        public virtual DbSet<sys_user> sys_user { get; set; }
        public virtual DbSet<sys_user_login> sys_user_login { get; set; }
        public virtual DbSet<sys_user_role> sys_user_role { get; set; }
         

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bucket>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.bucketcol).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.creator).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.description).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.is_compress).HasDefaultValueSql("'b''0'''");

                entity.Property(e => e.name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<bucket_cut>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.bucket_id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.creator).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.value).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<bucket_image>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.ext_name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.io_path).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.sha1).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.visiturl).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<quarzt_schedule>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.cron_express).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.job_group).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.job_name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.task_description).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<sys_activitylog>(entity =>
            {
                entity.HasComment("用户操作日志");

                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.comment).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.creator).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.entity_name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.method).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.newvalue).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.oldvalue).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.primary_key).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<sys_activitylog_comment>(entity =>
            {
                entity.HasKey(e => e.entity_name)
                    .HasName("PRIMARY");

                entity.Property(e => e.entity_name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.comment).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);
            });

            modelBuilder.Entity<sys_category>(entity =>
            {
                entity.HasComment("菜单表");

                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.action).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.code).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.controller).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.father_code).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.icon_class).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.is_menu).HasDefaultValueSql("'b''0'''");

                entity.Property(e => e.name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.route_name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.route_template).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.target).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.uid).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<sys_nlog>(entity =>
            {
                entity.HasComment("错误日志");

                entity.Property(e => e.callsite).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.category).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.clientip).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.eventid).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.exception).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.level).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.logger).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.message).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.properties).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.traceid).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.user).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<sys_permission>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.category_id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.role_id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);
            });

            modelBuilder.Entity<sys_role>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.creator).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.description).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<sys_setting>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.value).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<sys_user>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.account).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.creator).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.is_admin).HasDefaultValueSql("'b''0'''");

                entity.Property(e => e.is_deleted).HasDefaultValueSql("'b''0'''");

                entity.Property(e => e.last_ipaddr).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.name).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.password).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.salt).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);
            });

            modelBuilder.Entity<sys_user_login>(entity =>
            {
                entity.HasComment("		");

                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.comment).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.ip_addr).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8);

                entity.Property(e => e.user_id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);
            });

            modelBuilder.Entity<sys_user_role>(entity =>
            {
                entity.Property(e => e.id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.role_id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);

                entity.Property(e => e.user_id).HasCharSet(Pomelo.EntityFrameworkCore.MySql.Storage.CharSet.Utf8Mb4);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
