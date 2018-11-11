using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CommonLib
{
    public class WebApiClient
    {
        public Task<string> GetData(string url)
        {
            TaskCompletionSource<string> TCS = new TaskCompletionSource<string>();

            var uri = new Uri(url);
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();


            Task<System.Net.Http.HttpResponseMessage> response = httpClient.GetAsync(uri);

            response.ContinueWith(r =>
            {
                Console.WriteLine(response.Result.StatusCode);
                string msg = r.Result.Content.ReadAsStringAsync().Result;
                TCS.SetResult(msg);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            response.ContinueWith(r =>
            {
                Console.WriteLine(r.Exception.ToString());
                TCS.SetResult("err");

            }, TaskContinuationOptions.OnlyOnFaulted);

            return TCS.Task;
        }

        public async Task<string> PostDataAsync(string url, HttpContent body)
        {
            string responseBody = null;

            var uri = new Uri(url);
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpResponseMessage response = await httpClient.PostAsync(uri, body);


            if (response.IsSuccessStatusCode)
            {
                responseBody = response.Content.ReadAsStringAsync().Result;
            }

            return responseBody;
        }

        public Task<bool> PutData(string url, List<KeyValuePair<string, string>> data)
        {
            TaskCompletionSource<bool> TCS = new TaskCompletionSource<bool>();

            var uri = new Uri(url);
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpContent content = new System.Net.Http.FormUrlEncodedContent(data);

            string text = string.Empty;


            Task<System.Net.Http.HttpResponseMessage> response = httpClient.PutAsync(uri, content);

            response.ContinueWith(r =>
            {
                TCS.SetResult(r.Result.IsSuccessStatusCode);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            return TCS.Task;
        }
    }
}
