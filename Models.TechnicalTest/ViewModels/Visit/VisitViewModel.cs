using System;

namespace TechnicalTest.Models.ViewModels.Visit
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        public string PatientFullname { get; set; }
        public string PatientCardNo { get; set; }
        public int PatientId { get; set; }
        public string Reason { get; set; }
        public string CameToSee { get; set; }
        public string SignIn { get; set; }
        public string SignOut { get; set; }
    }
}