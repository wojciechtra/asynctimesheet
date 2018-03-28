using System.Linq;
using Timesheet.BLL.Models;

namespace Timesheet.DAL.EntityFramework
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.EntryType.Any())
            {
                EntryType[] entryTypes = new EntryType[]
                {
                    new EntryType
                    {
                        Name = "Office"
                    },
                    new EntryType
                    {
                        Name = "Remote"
                    },
                    new EntryType
                    {
                        Name = "Delegation"
                    },
                    new EntryType
                    {
                        Name = "Training"
                    },
                    new EntryType
                    {
                        Name = "Vacation"
                    }
                };

                context.EntryType.AddRange(entryTypes);
                context.SaveChanges();
            }
        }
    }
}
