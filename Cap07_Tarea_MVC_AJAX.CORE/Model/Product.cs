using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cap07_Tarea_MVC_AJAX.CORE.Model
{
    [Table("Product")]

    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public double UnitPrice { get; set; }
        public string Package { get; set; }
        [Required]
        public bool IsDiscontinued { get; set; }

    }
}
