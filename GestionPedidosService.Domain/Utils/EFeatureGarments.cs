using System.ComponentModel;

namespace GestionPedidosService.Domain.Utils
{
    public enum EFeatureGarments
    {
        [Description("color")]
        Color = 5,

        [Description("fabric")]
        Fabric = 6,

        [Description("occasion")]
        Occasion = 7,

        [Description("images")]
        Images = 8,
    }
}
