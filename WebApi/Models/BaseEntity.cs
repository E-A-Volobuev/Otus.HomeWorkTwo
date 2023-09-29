using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {

        }
        public long Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }
    }
}
