using System.Threading.Tasks;
using JetBrains.Lifetimes;

namespace ReSharperPlugin.CodeCommenter.Common;

public interface ICommentGenerationStrategy
{
    Task<string> Generate(string code, Lifetime lifetime);
}
