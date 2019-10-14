using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JoinRpg.Web.XGameApi.Contract.Schedule;
using Newtonsoft.Json;

namespace Joinrpg.Web.XGameApi.Client
{
    public class ProjectScheduleWebClient : IProjectScheduleWebClient
    {
        private readonly HttpClient _client;

        // inject from config
        private const string Host = "https://joinrpg.ru";

        public ProjectScheduleWebClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<ProgramItemInfoApi>> GetSchedule(int projectId)
        {
            var response = await _client.GetAsync(new Uri($"{Host}/x-game-api/{projectId}/schedule/all"));
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProgramItemInfoApi>>(body);
        }
    }
}
