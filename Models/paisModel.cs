using System.ComponentModel.DataAnnotations;

namespace ApiAngular.Models
{
    public class paisModel
    {
        public int id { get; set; }
        [Required(ErrorMessage ="El campo es requerido")]
        public string nombre { get; set; }

    }
}
