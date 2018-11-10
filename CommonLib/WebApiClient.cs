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
                string msg = r.Result.Content.ReadAsStringAsync().Result;
                TCS.SetResult(msg);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            return TCS.Task;
        }

        public async void PostData_t(string url, HttpContent body)
        {
            var uri = new Uri(url);
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpResponseMessage response = await httpClient.PostAsync(uri, body);

            Console.WriteLine(response.StatusCode);
        }

        public string PostDataSync(string url, HttpContent body)
        {
            string res = null;

            var uri = new Uri(url);
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("begin request");
            System.Net.Http.HttpResponseMessage response = httpClient.PostAsync(uri, body).Result;
            Console.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                res = response.Content.ReadAsStringAsync().Result;
            }

            return res;

        }

        public Task<string> PostData(string url, HttpContent body)
        {
            TaskCompletionSource<string> TCS = new TaskCompletionSource<string>();

            var uri = new Uri(url);
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Task <System.Net.Http.HttpResponseMessage> response = httpClient.PostAsync(uri, body);

           
            Console.WriteLine("begin request");
            
            response.ContinueWith(r =>
            {
                var t = r.Result.Content.ReadAsStringAsync();

                Console.WriteLine(r.Result.StatusCode);

                TCS.SetResult(t.Result);

            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            
            response.ContinueWith(r =>
            {
                Console.WriteLine(r.Exception.ToString());

                TCS.SetResult(string.Empty);

                var ex = r.Exception;
            }, TaskContinuationOptions.OnlyOnFaulted);

            return TCS.Task;
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
