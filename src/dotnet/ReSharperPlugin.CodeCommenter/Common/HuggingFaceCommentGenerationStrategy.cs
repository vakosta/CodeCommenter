using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rider.Model;
using Newtonsoft.Json;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class HuggingFaceCommentGenerationStrategy : ICommentGenerationStrategy
{
    // TODO: Rewrite to JetBrains.Util.Threading.Tasks.TaskSemaphore
    private static SemaphoreSlim mySemaphore = new(4, 4);

    private const string Url = "https://vakosta-code2comment.hf.space/run/predict";
    private const string MediaType = "application/json";

    public async Task<string> Generate(string code, Lifetime lifetime)
    {
        await mySemaphore.WaitAsync(lifetime);
        try
        {
            return await Post(code, lifetime);
        }
        finally
        {
            mySemaphore.Release();
        }
    }

    /// <summary>
    /// Executes post request synchronously to convert a code text into a docstring text.
    /// </summary>
    /// <param name="code">A code text to convert.</param>
    /// <param name="lifetime">A lifetime to receive info about cancellation.</param>
    /// <returns>A docstring text after convert.</returns>
    private static async Task<string> Post(string code, Lifetime lifetime)
    {
        if (!lifetime.IsAlive) return string.Empty;
        using var client = new HttpClient { Timeout = new TimeSpan(0, 0, 20) };

        var payload = new HuggingFacePayload { data = new[] { code } };
        var stringPayload = JsonConvert.SerializeObject(payload);

        var httpContent = new StringContent(stringPayload, Encoding.UTF8, MediaType);
        var httpResponse = await client.PostAsync(Url, httpContent, lifetime);

        var stringResult = await httpResponse.Content.ReadAsStringAsync();
        return stringResult != null
            ? JsonConvert.DeserializeObject<HuggingFaceResponse>(stringResult).data[0]
            : null;
    }
}
