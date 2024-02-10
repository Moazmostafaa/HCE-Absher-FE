using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Persistence.Configuration
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private readonly IConfiguration _configuration;
        private string AuthConnectionKey = "UserManagmentConnection";
        private string HCEDbContextConnectionKey = "HCEDbContextConnection";

        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetHCEDbContextConnectionString()
        {
            return _configuration.GetConnectionString(HCEDbContextConnectionKey);
        }

        public string GetAuthConnectionString()
        {
            return _configuration.GetConnectionString(AuthConnectionKey);
        }
    }
}
