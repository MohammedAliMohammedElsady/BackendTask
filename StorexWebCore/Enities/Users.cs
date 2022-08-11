using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorexWebCore.Enities
{
    public class Users : BaseEntity
    {
        [Key]
        public long UserID { get; set; }
        public string? Name { get; set; }
        public string? Birthdate { get; set; }
        public string? Email { get; set; }

    }
}
