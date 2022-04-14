using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Persistence.Managers
{
    public interface IConfigurationManager
    {
        string FirebaseApiKey { get; }
        string FirebaseBucket { get; }
        string FirebaseAuthEmail { get; }
        string FirebaseAuthPwd { get; }

        string MongoDbConnectionString { get; }
        string MongoDbName { get; }
    }
}
