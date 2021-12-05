using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cardio101.Models
{
    // [Index(nameof(SerialNumber), IsUnique = true)] introduced in 5.0
    public class Device
    {
        public int Id { get; set; }

        [Display(Name = "Device Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Serial Number"), Required]
        public string SerialNumber { get; set; }
        public string Description { get; set; }

        public List<Study> Studies { get; set; }

    }
}
