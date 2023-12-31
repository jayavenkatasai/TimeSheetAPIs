﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TIMESHEETAPI.DataModels;

namespace TIMESHEETAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options) 
        {
            
        }
        public DbSet<SuperHero> superHeroes { get; set; }
        public DbSet<Registeration> registerations { get; set; }

        public DbSet<NewRegisterationEmployee> NewRegisterationEmployees { get; set; }
    }
}
