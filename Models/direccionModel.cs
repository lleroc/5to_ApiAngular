namespace ApiAngular.Models
{
    using System.ComponentModel.DataAnnotations;

    public class direccionModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string callePrincipal { get; set; }
        public string calleSecundaria { get; set; }
        public string numero { get; set; }
        //relacion con canton
        public int cantonId { get; set; }
        public cantonModel Canton { get; set; }

    }
}
