using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Models
{
    public class UserClientMin
    {
        public int Id { get; set; }
        public string NameClient { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
