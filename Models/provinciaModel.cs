using System.ComponentModel.DataAnnotations;

namespace ApiAngular.Models
{
    public class provinciaModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string nombre { get; set; }
        //relacion con pais

        public int paisId { get; set; }
        public paisModel Pais { get; set; }
    }
}
