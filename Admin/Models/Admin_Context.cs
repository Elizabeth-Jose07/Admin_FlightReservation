using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Configuration;

namespace Admin.Models
{
    public class Admin_Context : DbContext
    {

        public Admin_Context(DbContextOptions<Admin_Context> options)
       : base(options)
        {

        }

        

       
        //public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<FlightInfo> FlightInfos { get; set; }
        public virtual DbSet<FlightDetail> Flightdetails { get; set; }
        public virtual DbSet<User>Users { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }

      

    }

   
}
