using System.ComponentModel.DataAnnotations;

namespace StorexWebCore.Enities
{
    public class CurrentUser : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public long? UserID { get; set; }
    }
}
