using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorexWebCore.Enities
{
    public class Categories : BaseEntity
    {
        [Key]
        public long CategoryID { get; set; }
        public string? Title { get; set; }
        public long? UserId { get; set; }

    }
}
