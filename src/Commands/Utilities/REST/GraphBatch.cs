using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Utilities.REST
{


    internal static class BatchUtility
    {
        internal static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }

        internal static Dictionary<string, string> GetPropertyBatched(ApiRequestHelper requestHelper, string[] lookupData, string urlTemplate, string property)
        {
            Dictionary<string, string> returnValue = new Dictionary<string, string>();

            Dictionary<string, string> requests = new Dictionary<string, string>();
            var batch = new GraphBatch();
            int id = 0;
            foreach (var item in lookupData)
            {
                id++;
                var url = string.Format(urlTemplate, item);
                batch.Requests.Add(new GraphBatchRequest() { Id = id.ToString(), Method = "GET", Url = $"{url}?$select={property}" });
                requests.Add(id.ToString(), item);
            }
            var stringContent = new StringContent(JsonSerializer.Serialize(batch));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = requestHelper.Post<GraphBatchResponse>("v1.0/$batch", stringContent);
            if (result.Responses != null && result.Responses.Any())
            {
                var errors = new List<Exception>();

                foreach (var response in result.Responses)
                {
                    var userId = requests.First(r => r.Key == response.Id).Value;
                    if (response.Body.TryGetValue(property, out object webUrlObject))
                    {
                        var element = (JsonElement)webUrlObject;
                        returnValue.Add(userId, element.GetString());
                    }
                    else if (response.Body.TryGetValue("error", out object errorObject))
                    {
                        var error = (JsonElement)errorObject;
                        errors.Add(new Exception(error.ToString()));
                    }
                }

                if (errors.Any())
                {
                    throw new AggregateException($"{errors.Count} error(s) occurred in a Graph batch request", errors);
                }
            }
            return returnValue;
        }

        internal static Dictionary<string, IEnumerable<T>> GetObjectCollectionBatched<T>(ApiRequestHelper requestHelper, string[] lookupData, string urlTemplate)
        {
            Dictionary<string, IEnumerable<T>> returnValue = new Dictionary<string, IEnumerable<T>>();

            Dictionary<string, string> requests = new Dictionary<string, string>();
            var batch = new GraphBatch();
            int id = 0;
            foreach (var item in lookupData)
            {
                id++;
                var url = string.Format(urlTemplate, item);
                batch.Requests.Add(new GraphBatchRequest() { Id = id.ToString(), Method = "GET", Url = $"{url}" });
                requests.Add(id.ToString(), item);
            }
            var stringContent = new StringContent(JsonSerializer.Serialize(batch));
            stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = requestHelper.Post<GraphBatchResponse>("v1.0/$batch", stringContent);
            if (result.Responses != null && result.Responses.Any())
            {
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                foreach (var response in result.Responses)
                {
                    var itemId = requests.First(r => r.Key == response.Id).Value;
                    if (response.Body.TryGetValue("value", out object resultObject))
                    {
                        var objectElement = (JsonElement)resultObject;
                        returnValue.Add(itemId, JsonSerializer.Deserialize<T[]>(objectElement.ToString(), options));
                    }
                }
            }
            return returnValue;
        }
    }

    internal class GraphBatch
    {
        [JsonPropertyName("requests")]
        public List<GraphBatchRequest> Requests { get; set; } = new List<GraphBatchRequest>();


    }

    internal class GraphBatchRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    internal class GraphBatchResponse
    {
        public List<GraphBatchResponseItem> Responses { get; set; }
    }

    internal class GraphBatchResponseItem
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, object> Body { get; set; }
    }
}