using BlazorApp.Shared;
using Flurl.Http;
using Flurl;
using Flurl.Util;

namespace BlazorApp.Services
{
    public class TempratureApiService
    {
        private readonly IFlurlClient _FlurlClient;

        public TempratureApiService(HttpClient client)
        {
            _FlurlClient = new FlurlClient(client);
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
