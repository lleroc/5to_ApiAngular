namespace ApiAngular.Models
{
    using System.ComponentModel.DataAnnotations;
    public class cantonModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string nombre { get; set; }
        //relacion con provincia
        public int provinciaId { get; set; }
        public provinciaModel Provincia { get; set; }
    }
}
