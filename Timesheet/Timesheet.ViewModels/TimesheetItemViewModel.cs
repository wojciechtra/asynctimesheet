using System;

namespace Timesheet.ViewModels
{
    public class TimesheetItemViewModel
    {
        public string UserName { get; set; }

        public string EntryType { get; set; }

        public string Comment { get; set; }
        
        public int DayPercent { get; set; }
        
        public string Day { get; set; }
    }
}
