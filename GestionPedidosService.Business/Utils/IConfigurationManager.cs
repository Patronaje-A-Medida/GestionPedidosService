using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Business.Utils
{
    public interface IConfigurationManager
    {
        string FirebaseApiKey { get; }
        string FirebaseBucket { get; }
        string FirebaseAuthEmail { get; }
        string FirebaseAuthPwd { get; }
    }
}
