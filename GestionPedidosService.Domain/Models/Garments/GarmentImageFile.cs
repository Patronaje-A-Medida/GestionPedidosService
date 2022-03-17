using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentImageFile
    {
        [Required(ErrorMessage = "La imagen es requerida")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "El nombre del archivo es requerido")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "El folder de almacenamiento es requerido")]
        public string FolderPath { get; set; }
    }
}
