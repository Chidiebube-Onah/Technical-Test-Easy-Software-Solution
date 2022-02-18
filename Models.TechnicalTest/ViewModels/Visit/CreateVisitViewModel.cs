using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Models.ViewModels.Visit
{
    public class CreateVisitViewModel
    {

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string CameToSee { get; set; }
        
    }
}