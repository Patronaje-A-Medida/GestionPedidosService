using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models.Garments
{
    public class GarmentUpdateFile
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FileName { get; set; }
        public string FolderPath { get; set; }
    }
}
