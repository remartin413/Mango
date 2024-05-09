using System.Net;
using System.Text;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using static Mango.Web.Utility.SD;
using Newtonsoft.Json;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _clientFactory;

        public BaseService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            HttpClient client = _clientFactory.CreateClient("MangoAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");

            // Token

            message.RequestUri = new Uri(requestDto.Url);
            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;
            switch (requestDto.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            try
            {
                apiResponse = await client.SendAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            try
            {
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return responseDto;
                }
            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    Message = e.Message.ToString(),
                    IsSuccess = false
                };

                return dto;
            }
        }
    }
}
