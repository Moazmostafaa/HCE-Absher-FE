using Hangfire.Dashboard;

namespace HCE.WebAPI.Filters
{
    public class HangfireDashboardNoAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext dashboardContext)
        {
            return true;
        }
    }
}
