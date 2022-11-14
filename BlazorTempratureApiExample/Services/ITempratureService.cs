using Blazor_Radzen_Data_Example.Shared;

namespace Blazor_Radzen_Data_Example.Client.Services
{
    public interface ITempratureService
    {

        public Task<List<TempraureInfo>> GetTempraturesAsync(DateTime start,DateTime end);

        public Task<TempraureInfo> GetLatestTempratureAsync();

    }
}
