using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionPedidosService.Domain.Utils
{
    public enum ETypes
    {
        [Description("garment_categories")]
        garment_category = 2,

        [Description("order_status")]
        order_status = 1,
    }
}
