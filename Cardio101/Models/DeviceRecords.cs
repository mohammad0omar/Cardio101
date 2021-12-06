using System;
using System.ComponentModel.DataAnnotations;

namespace Cardio101.Models
{
    public class DeviceRecords
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Time is missing")]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Pleasee provide a BPM")]
        [Range(0, 480, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public uint Value { get; set; }

        public Study Study { get; set; }
    }
}
