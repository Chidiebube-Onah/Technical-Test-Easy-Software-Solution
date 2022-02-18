using System;
using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Models.ViewModels.Visit
{
    public class UpdateVisitViewModel
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string CameToSee { get; set; }
        public DateTime? SignOut { get; set; }
    }
}