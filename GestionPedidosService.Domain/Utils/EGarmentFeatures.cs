using System.ComponentModel;

namespace GestionPedidosService.Domain.Utils
{
    public enum EGarmentFeatures
    {
        [Description("Colores")]
        color = 1,

        [Description("Telas")]
        fabric = 2,

        [Description("Ocasión/Eventos")]
        occasion = 3,

        [Description("Imágenes")]
        images = 4,
    }
}
