using GestionPedidosService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Entities
{
    public class UserClient : Auditable
    {
        public int Id { get; set; }
        public decimal Height { get; set; }
        public string Phone { get; set; }

        // relations
        public string UserId { get; set; }
        public UserBase User { get; set; }

        //
        public IEnumerable<Order> Orders { get; set; }

    }
}
