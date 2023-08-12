using Microsoft.EntityFrameworkCore;
using LamConference.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LamConference.Data{
    public class AppContext: IdentityDbContext{

        public AppContext(DbContextOptions<AppContext> options) : base(options){}

        public DbSet<StudentData> StudentData{get; set;}
        public DbSet<ReferenceID> ReferenceIDs{get; set;}
    }
}