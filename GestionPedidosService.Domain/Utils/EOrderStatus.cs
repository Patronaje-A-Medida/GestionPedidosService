using System.ComponentModel;

namespace GestionPedidosService.Domain.Utils
{
    public enum EOrderStatus
    {
        [Description("Sin Atender")]
        unattended = 0,
        [Description("En Progreso")]
        in_progress = 1,
        [Description("Atendido")]
        attended = 2
    }
}
