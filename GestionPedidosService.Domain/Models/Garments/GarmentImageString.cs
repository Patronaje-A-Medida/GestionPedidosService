using System.ComponentModel.DataAnnotations;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentImageString
    {
        [Required(ErrorMessage = "La imagen es requerida")]
        public string Image { get; set; }

        [Required(ErrorMessage = "El nombre del archivo es requerido")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "El folder de almacenamiento es requerido")]
        public string FolderPath { get; set; }
    }
}
