using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.DAL.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string CardNo { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }

    }
}
