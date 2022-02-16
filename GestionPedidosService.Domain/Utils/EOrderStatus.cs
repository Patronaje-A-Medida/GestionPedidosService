using System.ComponentModel;

namespace GestionPedidosService.Domain.Utils
{
    public enum EOrderStatus
    {
        [Description("Unattended")]
        Unattended = 0,
        [Description("In Progress")]
        InProgress = 1,
        [Description("Attended")]
        Attended = 2
    }
}
