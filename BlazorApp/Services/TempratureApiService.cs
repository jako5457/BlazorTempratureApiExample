using BlazorApp.Shared;
using Flurl.Http;
using Flurl;
using Flurl.Util;

namespace BlazorApp.Services
{
    public class TempratureApiService
    {
        public static bool IgnoreCertErrors = false;

        private readonly IFlurlClient _FlurlClient;

        public TempratureApiService(HttpClient client,ILogger<TempratureApiService> logger)
        {
            _FlurlClient = new FlurlClient(client);
            if (IgnoreCertErrors)
            {
              _FlurlClient = _FlurlClient.Configure(config => config.HttpClientFactory = new UntrustedCertClientFactory());
                logger.LogInformation("Cert errors will be ignored.");
            }
            
        }

        public async Task<List<TempraureInfo>> GetTempraturesAsync(DateTime start, DateTime end)
        {
            var query = _FlurlClient.Request("api","temprature")
                                    .AppendPathSegment("range")
                                    .SetQueryParam("start", start)
                                    .SetQueryParam("end", end);

            return await query.GetJsonAsync<List<TempraureInfo>>();
        }

        public async Task<TempraureInfo> GetCurrentAsync()
        {
            var query = _FlurlClient.Request("api", "temprature")
                                    .AppendPathSegment("current");

            return await query.GetJsonAsync<TempraureInfo>();
        }
        
    }
}
