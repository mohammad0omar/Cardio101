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
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        [Range(0, 480, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public object NormalHeartRate;

        [Range(0, 480, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public object LowHeartRate = 50;

        [Range(0, 480, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public object HighHeartRate = 100;

    }
}
