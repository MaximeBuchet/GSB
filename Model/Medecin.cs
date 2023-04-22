using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Medecin")]
    public partial class Medecin
    {
        [Key]
        public int med_id { get; set; }

        [Required]
        [StringLength(50)]
        public string med_nom { get; set; }

        [Required]
        [StringLength(50)]
        public string med_prenom { get; set; }

        [Required]
        [StringLength(250)]
        public string med_addresse { get; set; }

        [StringLength(50)]
        public string med_telephone { get; set; }

        public bool? med_specialite { get; set; }

        public int dep_numero { get; set; }

        public virtual Departement Departement { get; set; }
    }
}
