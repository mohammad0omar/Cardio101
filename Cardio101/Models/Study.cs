using System.ComponentModel.DataAnnotations;

namespace Cardio101.Models
{
    public class Study
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Time"), Required]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime StartTime { get; set; }

        [Display(Name = "Duration (in days)"), Required]
        public uint Duration { get; set;}

        //[Display(Name = "Patient Name"), Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        //[Display(Name = "Device Serial Number"), Required]
        public string DeviceSerialNumber { get; set; }
        public Device Device { get; set; }

    }
}
