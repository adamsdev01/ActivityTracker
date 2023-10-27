using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorActivityTracker.Data.Models;

public partial class ActivityTrackerContext : DbContext
{
    public ActivityTrackerContext()
    {
    }

    public ActivityTrackerContext(DbContextOptions<ActivityTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC07EA02D99D");

            entity.ToTable("Activity");

            entity.Property(e => e.ActivityDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
