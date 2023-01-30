using System;
using System.Net.Http;
using System.Text;
using JetBrains.ProjectModel;
using JetBrains.Rider.Model;
using Newtonsoft.Json;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class HuggingFaceCommentGenerationStrategy : ICommentGenerationStrategy
{
    private const string URL = "https://vakosta-code2comment.hf.space/run/predict";

    private static readonly HttpClient client = new()
    {
        Timeout = new TimeSpan(1, 0, 0)
    };

    public string Generate(string code)
    {
        return Post(code);
    }

    private static string Post(string code)
    {
        var payload = new HuggingFacePayload
        {
            data = new[] { code }
        };

        // TODO: Rewrite this painful code.
        var stringPayload = JsonConvert.SerializeObject(payload);
        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        var httpResponse = client.PostAsync(URL, httpContent).Result.Content;
        var stringResult = httpResponse?.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<HuggingFaceResponse>(stringResult).data[0];
    }
}
