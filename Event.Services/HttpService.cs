using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Event.Services
{
    public class HttpService : IHttpService
    {
        public async Task<T> GetAsync<T>(string uri)
        {
            var response = await new HttpClient().GetAsync(uri);            

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();            
            
            return JsonConvert.DeserializeObject<T>(responseString);            
        }
        
    }

    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri);
    }
}