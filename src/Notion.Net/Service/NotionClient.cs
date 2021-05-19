using Notion.Net.Exceptions;
using Notion.Net.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Notion.Net.Service
{
    // TODO: Token could/should expire, what should one do ?
    public class NotionClient
    {
        private readonly HttpClient _httpClient;

        private readonly string NotionBaseUrl = "https://api.notion.com/v1/";
        private readonly string PageRoute = "pages";
        private readonly string DatabaseRoute = "database";
        private readonly string UserRoute = "users";
        private readonly string BlockRoute = "blocks";
        private readonly string SearchRoute = "search";

        // TODO: Putting token in ctor as injected is not usually a good idea
        public NotionClient(HttpClient httpClient, string bearerToken)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(NotionBaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        public async Task<NotionDatabase> GetDatabaseAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                string requestUrl = $"databases/{id}";
                HttpRequestMessage httpMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                var response = await _httpClient.SendAsync(httpMessage);

                if(response.IsSuccessStatusCode)
                {
                    using var responseContent = await response.Content.ReadAsStreamAsync(cancellationToken);
                    var responseObject = await JsonSerializer.DeserializeAsync<NotionDatabase>(responseContent, null, cancellationToken);

                    return responseObject;
                }
                else
                {
                    throw new Exception($"Failure in Notion response {response.Content.ReadAsStringAsync().Result}");
                }
            }
            // TODO: Validation, Cancelled, Request failure
            catch (Exception ex)
            {
                throw new NotionException($"Failure in {nameof(QueryDatabaseAsync)} ", ex);
            }
        }

        public async Task<NotionList> QueryDatabaseAsync(Guid id, object queryObject, CancellationToken cancellationToken)
        {
            try
            {
                string requestUrl = $"{DatabaseRoute}/{id}";
                HttpRequestMessage httpMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);

                httpMessage.Content = new StringContent(JsonSerializer.Serialize(queryObject));

                var response = await _httpClient.SendAsync(httpMessage);

                if (response.IsSuccessStatusCode)
                {
                    using var responseContent = await response.Content.ReadAsStreamAsync(cancellationToken);
                    var responseObject = await JsonSerializer.DeserializeAsync<NotionList>(responseContent, null, cancellationToken);

                    return responseObject;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorResponse);
                }
            }
            // TODO: Validation, Cancelled, Request failure
            catch (Exception ex)
            {
                throw new NotionException($"Failure in {nameof(QueryDatabaseAsync)} ", ex);
            }
        }

        public async Task<NotionPage> GetPageAsync(Guid pageId, CancellationToken cancellationToken)
        {
            try
            {
                string requestUrl = $"{PageRoute}/{pageId}";
                HttpRequestMessage httpMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                var response = await _httpClient.SendAsync(httpMessage);

                if (response.IsSuccessStatusCode)
                {
                    using var responseContent = await response.Content.ReadAsStreamAsync(cancellationToken);
                    var responseObject = await JsonSerializer.DeserializeAsync<NotionPage>(responseContent, null, cancellationToken);

                    return responseObject;
                }
                else
                {
                    throw new Exception("Failure in Notion response");
                }
            }
            // TODO: Validation, Cancelled, Request failure
            catch (Exception ex)
            {
                throw new NotionException($"Failure in {nameof(QueryDatabaseAsync)} ", ex);
            }
        }
    }
}
