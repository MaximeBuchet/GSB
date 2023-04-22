using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("user")]
    public partial class user
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(20)]
        public string user_pseudo { get; set; }

        [Required]
        [StringLength(20)]
        public string user_mdp { get; set; }

    }
}
