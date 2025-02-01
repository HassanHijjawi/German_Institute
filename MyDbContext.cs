﻿using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<Applicant> Applicants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>()
        .Property(a => a.PassportScan)
        .HasColumnType("LONGBLOB");

        modelBuilder.Entity<Applicant>()
            .Property(a => a.CV)
            .HasColumnType("LONGBLOB");
    }
}

