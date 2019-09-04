using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasMany(d => d.Tasks)
                    .WithOne(d => d.Member)
                    .HasForeignKey(d => d.IdMember);

                entity.HasMany(d => d.Creations)
                    .WithOne(d => d.Creator)
                    .HasForeignKey(d => d.IdCreator);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.IdMember);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Creations)
                    .HasForeignKey(d => d.IdCreator);

                entity.HasOne(d => d.Project)
                    .WithMany(d => d.Tasks)
                    .HasForeignKey(d => d.IdProject);
            });   

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");

                entity.HasMany(d => d.Tasks)
                    .WithOne(d => d.Project)
                    .HasForeignKey(d => d.IdProject);

                entity.HasOne(d => d.Team)
                    .WithMany(d => d.Projects)
                    .HasForeignKey(d => d.IdTeam);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Teams");

                entity.HasMany(d => d.Members)
                    .WithOne(d => d.Team)
                    .HasForeignKey(d => d.IdTeam);

                entity.HasMany(d => d.Projects)
                    .WithOne(d => d.Team)
                    .HasForeignKey(d => d.IdTeam);
            });

            // modelBuilder.ApplyConfiguration(new TaskConfiguration());
            // modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        }
    }
}
