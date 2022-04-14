using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionPedidosService.Persistence.Managers
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string FirebaseApiKey
        {
            get
            {
                return _configuration["FirebaseSettings:ApiKey"];
            }
        }

        public string FirebaseBucket => _configuration["FirebaseSettings:Bucket"];

        public string FirebaseAuthEmail => _configuration["FirebaseSettings:AuthEmail"];

        public string FirebaseAuthPwd => _configuration["FirebaseSettings:AuthPwd"];

        public string MongoDbConnectionString => _configuration["MongoDBSettings:ServerConnection"];

        public string MongoDbName => _configuration["MongoDBSettings:DatabaseName"];
    }
}
