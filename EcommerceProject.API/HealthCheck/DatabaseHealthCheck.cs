using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceProject.API.HealthCheck
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public DatabaseHealthCheck(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool canConnect = IsDBOnline();
            if (canConnect)
                return HealthCheckResult.Healthy("passed");
            return HealthCheckResult.Unhealthy("failed");
        }
        private bool IsDBOnline()
        {
            string connectionString = Config["ConnectionStrings:SqlConStr"].ToString();
            try
            {
                using (SqlConnection connection = new
                SqlConnection(connectionString))
                {
                    if (connection.State !=
                       System.Data.ConnectionState.Open)
                        connection.Open();
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
