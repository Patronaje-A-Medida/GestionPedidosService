using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Domain.Base
{
    public class UserBase
    {
        public string Id { get; set; }
        public string NameUser { get; set; }
        public string LastNameUser { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string Status { get; set; }
    }
}
