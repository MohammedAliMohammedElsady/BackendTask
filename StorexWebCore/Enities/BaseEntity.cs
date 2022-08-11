using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorexWebCore.Enities
{
    public abstract class BaseEntity
    {
        [NotMapped]
        public int jtStartIndex { get; set; }
        [NotMapped]
        public int jtPageSize { get; set; }
        [NotMapped]
        public string jtSorting { get; set; }
        [NotMapped]
        public int TotalRecordCount { get; set; }
        [NotMapped]
        public string OrderBy { get; set; }
        [NotMapped]
        public bool OrderByReversed { get; set; }
    }
}
