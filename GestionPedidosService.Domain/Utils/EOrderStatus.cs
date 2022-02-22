using System.ComponentModel;

namespace GestionPedidosService.Domain.Utils
{
    public enum EOrderStatus
    {
        [Description("Sin Atender")]
        Unattended = 0,
        [Description("En Progreso")]
        InProgress = 1,
        [Description("Atendido")]
        Attended = 2
    }
}
