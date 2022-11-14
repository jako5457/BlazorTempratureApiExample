using Blazor_Radzen_Data_Example.Shared;

namespace Blazor_Radzen_Data_Example.Client.Services
{
    public class RandomTempratureService : ITempratureService
    {
        private Random _Random;
        int seed = 1009432120;

        public RandomTempratureService()
        {
            _Random = new Random(seed);
        }

        public Task<TempraureInfo> GetLatestTempratureAsync()
        {
            var info = new TempraureInfo() { TempratureC = _Random.NextDouble(), Date = DateTime.Now };

            return Task.FromResult(info);
        }

        public Task<List<TempraureInfo>> GetTempraturesAsync(DateTime start,DateTime end)
        {

            var timeSpan = start.Subtract(end);

            var temps = new List<TempraureInfo>().InfinityRandom(_Random,start);

            int segments = Convert.ToInt32(timeSpan.TotalMinutes / 30);

            return Task.FromResult(temps.Take(segments).ToList());
        }
    }

    static class RandomLinq
    {
        public static IEnumerable<TempraureInfo> InfinityRandom(this IEnumerable<TempraureInfo> queryable,Random random,DateTime startDate)
        {
            DateTime date = startDate;
            while (true)
            {
                date = date.AddMinutes(-30);
                yield return new TempraureInfo()
                {
                    TempratureC = random.NextDouble(),
                    Date = date,
                };
            }
        }
    }
}
