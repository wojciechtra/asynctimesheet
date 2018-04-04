using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Timesheet.BLL.Models;

namespace Timesheet.ViewModels
{
    public class AddTimesheetItemViewModel
    {
        [Required]
        public string UserId { get; set; }

        public IEnumerable<IdentityUser> Users { get; set; }

        [Required]
        public int EntryTypeId { get; set; }

        public IEnumerable<EntryType> EntryTypes { get; set; }

        [Required]
        public string Comment { get; set; }

        [Range(1, 150, ErrorMessage = "Liczba musi być z przedziału 1 -150")]
        public int DayPercent { get; set; }

        [DataType(DataType.Date)]
        public DateTime Day { get; set; }

    }
}
