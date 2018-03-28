using System.ComponentModel.DataAnnotations;

namespace Timesheet.BLL.Models
{
    public class EntryType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
