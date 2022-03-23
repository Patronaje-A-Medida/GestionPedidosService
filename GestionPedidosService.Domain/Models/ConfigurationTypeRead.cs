using System.Collections.Generic;

namespace GestionPedidosService.Domain.Models
{
    public class ConfigurationTypeRead
    {
        public IEnumerable<TypeRead> Categories { get; set; }
        public IEnumerable<TypeRead> OrderStatus { get; set; }
        public IEnumerable<TypeRead> Fabrics { get; set; }
        public IEnumerable<TypeRead> Occasions { get; set; }
    }
}
