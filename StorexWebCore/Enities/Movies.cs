using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorexWebCore.Enities
{
    public class Movies : BaseEntity
    {
        [Key]
        public long MovieID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Rate { get; set; }
        public string? Image { get; set; }        // This is Location For Image and in Front End Tag <img src= Image >
        public long? CategoryId { get; set; }
        public long? UserId { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }
    }
}
