using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)//To pass configuration info to DbContext use DbContext options instance
            :base(options)
        {
        }
        public DbSet<Notes> NotesTable { get; set; }//table name on DB holds the result from DB
        public DbSet<User> UserTables { get; set; }//table name on DB holds the result from DB

        public DbSet<Collaborator> CollabTable { get; set; }

        public DbSet<Label> LabelsTable { get; set; }

    }
}
