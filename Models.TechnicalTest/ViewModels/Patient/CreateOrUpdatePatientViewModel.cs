using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Models.ViewModels.Patient
{
    public class CreateOrUpdatePatientViewModel
    {

        public int? Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string CardNo { get; set; }
    }
}