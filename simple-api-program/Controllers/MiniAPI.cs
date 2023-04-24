using System.Text.Json;
using System.Reflection;
using System;
using System.Net.NetworkInformation;

// this class won't use attributes & ControllerBase to show what's the diff
// this's a static class -> use a static general-purpose util class -> Results
namespace simple_api_program.Controllers
{
    public class ResultBodyExample
    {
        public string Msg { get; set; }
        public int StatusCode { get; set; }
        public string Origin { get; set; }
    }

    public static class MiniAPI
    {
        // similar to axios
        private static readonly HttpClient httpClient = new HttpClient();

        // Not derived from ControllerBase, an instance class
        // -> IResult instead of IActionResult
        public static IResult HealthChecking()
        {
            var resBody = new ResultBodyExample
            {
                Msg = "Health check OK!",
                StatusCode = 200,
                Origin = MethodBase.GetCurrentMethod().Name
            };
            // aware that this returns with escape chars
            string jsonResBody = JsonSerializer.Serialize(resBody);
            return Results.Ok(jsonResBody);
        }

        public static async Task<IResult> GetOneUserById(int randomIdParamNameHere)
        {
            if (randomIdParamNameHere < 1 || randomIdParamNameHere > 10)
            {
                return Results.NotFound<ResultBodyExample>(
                    new ResultBodyExample
                    {
                        Msg = "ID outside of existance range!",
                        StatusCode = 404,
                        Origin = MethodBase.GetCurrentMethod().Name
                    }
                );
            }

            // string intepolation
            HttpResponseMessage fetchRes = await httpClient.GetAsync(
                $"https://jsonplaceholder.typicode.com/users/{randomIdParamNameHere}"
            );
            fetchRes.EnsureSuccessStatusCode();
            var tmp = await fetchRes.Content.ReadAsStringAsync();
            var jsonResBody = JsonSerializer.Serialize(tmp);
            return Results.Ok(jsonResBody);
        }

        // TODO: these examples do not work, fix it
        // Code using Task.Results
        public static HttpResponseMessage CustomResult()
        {
            var fetchRes = httpClient.GetAsync(
                $"https://jsonplaceholder.typicode.com/users/5"
            );

            Console.WriteLine(fetchRes.Result);
            Console.WriteLine("Running async op using Task.Result...");
            return fetchRes.Result;
        }

        // TODO: these examples do not work, fix it
        public static async Task<HttpResponseMessage> CustomAsync()
        {
            var fetchRes = await httpClient.GetAsync(
                $"https://jsonplaceholder.typicode.com/users/5"
            );

            Console.WriteLine(fetchRes);
            Console.WriteLine("Running async op using async-await...");
            return fetchRes;
        }
    }
}
