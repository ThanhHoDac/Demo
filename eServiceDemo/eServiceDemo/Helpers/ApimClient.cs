using eServiceDemo.Configurations;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eServiceDemo.Helpers
{
    public class ApimClient
    {
        private readonly ClientOptions _options;
        private HttpClient _httpClient;

        public ApimClient(ClientOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            InitClient();
        }

        private void InitClient()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_options.BaseAddress)
            };

            foreach (KeyValuePair<string, string> header in _options.DefaultRequestHeaders)
            {
                if (!string.IsNullOrEmpty(header.Key))
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        protected HttpContent CreateContent<T>(T content)
        {
            string serialisedContent = JsonConvert.SerializeObject(content, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                });
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(serialisedContent);

            ByteArrayContent httpContent = new ByteArrayContent(buffer);
            if (!string.IsNullOrEmpty(_options.RequestContentType))
            {
                httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(_options.RequestContentType);
            }
            return httpContent;
        }

        protected string Send<TContent>(string path, HttpMethod httpMethod, TContent? content)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("SendRequestException : " + nameof(path));
            }
            string responseContent;
            using (HttpRequestMessage request = new HttpRequestMessage(httpMethod, path))
            {
                if (content != null)
                {
                    HttpContent httpContent = CreateContent(content);
                    request.Content = httpContent;
                }
                try
                {
                    HttpResponseMessage response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
                    responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return responseContent;
        }

        protected TResponse Send<TContent, TResponse>(string path, HttpMethod httpMethod, TContent? content)
        {
            return JsonConvert.DeserializeObject<TResponse>(Send(path, httpMethod, content));
        }

        protected void SendWithoutResponse<TContent>(string path, HttpMethod httpMethod, TContent content)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, path);
            if (content != null)
            {
                HttpContent httpContent = CreateContent(content);
                request.Content = httpContent;
            }
            _httpClient.SendAsync(request);
        }

        protected TResponse Send<TResponse>(string path, HttpMethod httpMethod)
        {
            return Send<object, TResponse>(path, httpMethod, null);
        }
    }
}
