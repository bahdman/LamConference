using Microsoft.EntityFrameworkCore;
using LamConference.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LamConference.Data{
    public class LamContext: IdentityDbContext{

        public LamContext(DbContextOptions<LamContext> options) : base(options){}

        public DbSet<StudentData> StudentData{get; set;}
        public DbSet<ReferenceID> ReferenceIDs{get; set;}
    }
}