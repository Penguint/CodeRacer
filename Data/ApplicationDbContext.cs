using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodeRacer.Models;

namespace CodeRacer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<File> Files {get; set;}
        public DbSet<CompetitionFile> CompetitionFiles { get; set; }
        public DbSet<SubmissionFile> SubmissionFiles { get; set; }
    }
}
