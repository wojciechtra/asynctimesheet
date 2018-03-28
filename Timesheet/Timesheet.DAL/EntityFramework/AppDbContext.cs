using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timesheet.BLL.Models;

namespace Timesheet.DAL.EntityFramework
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<EntryType> EntryType { get; set; }
        public DbSet<TimesheetItem> Timesheets { get; set; }
    }
}
