using System;

namespace TechnicalTest.DAL.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Reason { get; set; }
        public string CameToSee { get; set; }
        public DateTime VisitedAt { get; set; }
        public DateTime? SignOut { get; set; }
        public virtual Patient Patient { get; set; }
    }
}