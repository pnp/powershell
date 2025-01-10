using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using PnP.Core.Services;

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

        internal class BatchedPropertyResults
        {
            public Dictionary<string, string> Results;
            public List<Exception> Errors;
        }

        internal class BatchedObjectResults<T>
        {
            public Dictionary<string,T> Results;
            public List<Exception> Errors;
        }

        internal static BatchedPropertyResults GetPropertyBatched(ApiRequestHelper requestHelper, string[] lookupData, string urlTemplate, string property)
        {
            var returnValue = new Dictionary<string, string>();
            var errors = new List<Exception>();
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
                foreach (var response in result.Responses)
                {
                    var value = requests.First(r => r.Key == response.Id).Value;
                    if (response.Body.TryGetValue(property, out object webUrlObject))
                    {
                        var element = (JsonElement)webUrlObject;
                        returnValue.Add(value, element.GetString());
                    }
                    else if (response.Body.TryGetValue("error", out object errorObject))
                    {
                        var error = (JsonElement)errorObject;
                        var request = batch.Requests.First(r => r.Id == response.Id);
                        returnValue.Add(value,"");
                        errors.Add(new Exception($"An error occured for request id {request.Id}:{request.Url} => {error.ToString()}"));
                    }
                }

                // if (errors.Any())
                // {
                //     throw new AggregateException($"{errors.Count} error(s) occurred in a Graph batch request", errors);
                // }
            }
            return new BatchedPropertyResults { Results = returnValue, Errors = errors };
        }

        internal static BatchedObjectResults<IEnumerable<T>> GetObjectCollectionBatched<T>(ApiRequestHelper requestHelper, string[] lookupData, string urlTemplate)
        {
            Dictionary<string, IEnumerable<T>> returnValue = new Dictionary<string, IEnumerable<T>>();
            var errors = new List<Exception>();

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
                       else if (response.Body.TryGetValue("error", out object errorObject))
                    {
                        var error = (JsonElement)errorObject;
                        var request = batch.Requests.First(r => r.Id == response.Id);
                        returnValue.Add(itemId,default(T[]));
                        errors.Add(new Exception($"An error occured for request id {request.Id}:{request.Url} => {error.ToString()}"));
                    }
                }
            }
            return new BatchedObjectResults<IEnumerable<T>>{ Results = returnValue, Errors = errors};
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