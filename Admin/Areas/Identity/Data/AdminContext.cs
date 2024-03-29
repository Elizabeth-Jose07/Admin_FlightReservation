﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Areas.Identity.Data;
using Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Admin.Data
{
    public class AdminContext : IdentityDbContext<AdminUser>
    {
        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //public virtual DbSet<FlightInfo> FlightInfos { get; set; }
        //public virtual DbSet<FlightDetail> Flightdetails { get; set; }
        //public virtual DbSet<User> User { get; set; }
        //public virtual DbSet<Ticket> Tickets { get; set; }
        //public virtual DbSet<Booking> Bookings { get; set; }


    }
}
