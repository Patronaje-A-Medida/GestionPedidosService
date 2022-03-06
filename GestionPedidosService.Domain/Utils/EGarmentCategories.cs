using System.ComponentModel;

namespace GestionPedidosService.Domain.Utils
{
    public enum EGarmentCategories
    {
        [Description("blusas")]
        Blouses = 20,

        [Description("sacos")]
        Coats = 21,

        [Description("vestidos")]
        Dresses = 22,

        [Description("faldas")]
        Skirts = 23,

        [Description("pantalones")]
        Pants = 24,
    }
}
