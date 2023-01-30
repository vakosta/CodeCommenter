namespace ReSharperPlugin.CodeCommenter.Common;

public interface ICommentGenerationStrategy
{
    string Generate(string code);
}
