using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public partial class TaskManagerContext : DbContext
{
    public TaskManagerContext()
    {
    }

    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<TakesTask> TakesTasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskSkill> TaskSkills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=ConnectionStrings:TaskManagerConnStr");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log__3214EC07AB5FC730");

            entity.ToTable("Log");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manager__3214EC075FCC3C2A");

            entity.ToTable("Manager");

            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skill__3214EC07F3FAF0C8");

            entity.ToTable("Skill");

            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<TakesTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TakesTas__3214EC075D21EFF9");

            entity.ToTable("TakesTask");

            entity.HasOne(d => d.Task).WithMany(p => p.TakesTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TakesTask_Task");

            entity.HasOne(d => d.User).WithMany(p => p.TakesTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TakesTask_User");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Task__3214EC07A05DB91A");

            entity.ToTable("Task");

            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.Manager).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Manager");
        });

        modelBuilder.Entity<TaskSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaskSkil__3214EC07E56C5A9D");

            entity.ToTable("TaskSkill");

            entity.HasOne(d => d.Skill).WithMany(p => p.TaskSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskSkill_Skill");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskSkills)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskSkill_Task");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07B1B56A08");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(256);
            entity.Property(e => e.PwdHash).HasMaxLength(256);
            entity.Property(e => e.PwdSalt).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
