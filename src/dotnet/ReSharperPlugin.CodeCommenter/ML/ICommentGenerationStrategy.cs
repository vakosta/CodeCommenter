using System.Threading.Tasks;
using JetBrains.Annotations;
using JetBrains.Lifetimes;
using ReSharperPlugin.CodeCommenter.Entities.Network;

namespace ReSharperPlugin.CodeCommenter.Common;

public interface ICommentGenerationStrategy
{
    [NotNull]
    Task<GenerationResult> Generate([NotNull] string code, Lifetime lifetime);
}
