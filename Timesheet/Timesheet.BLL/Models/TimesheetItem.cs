using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.BLL.Models
{
    public class TimesheetItem
    {
        [Key]
        public int Id { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public EntryType EntryType { get; set; }

        [Range(1, 150, ErrorMessage = "Liczba musi być z przedziału 1 -150")]
        public int DayPercent { get; set; }

        public DateTime Day { get; set; }

        public bool IsSettled { get; set; }
    }
}
