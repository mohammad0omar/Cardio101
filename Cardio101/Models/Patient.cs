using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cardio101.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Display(Name = "Patient Name")]
        public string Name { get; set; }
    }
}
