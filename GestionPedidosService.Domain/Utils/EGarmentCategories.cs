using System.ComponentModel;

namespace GestionPedidosService.Domain.Utils
{
    public enum EGarmentCategories
    {
        [Description("Blusas")]
        blouses = 1,

        [Description("Sacos/Gabardinas")]
        coats = 2,

        [Description("Vestidos")]
        dresses = 3,

        [Description("Faldas")]
        skirts = 4,

        [Description("Pantalones")]
        pants = 5,
    }
}
