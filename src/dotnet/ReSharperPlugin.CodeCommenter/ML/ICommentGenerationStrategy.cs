using System.Threading.Tasks;
using JetBrains.Lifetimes;
using ReSharperPlugin.CodeCommenter.Entities.Network;

namespace ReSharperPlugin.CodeCommenter.Common;

public interface ICommentGenerationStrategy
{
    Task<GenerationResult> Generate(string code, Lifetime lifetime);
}
