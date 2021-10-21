using Autransoft.Fluent.HttpClient.Lib.Enums;

namespace Autransoft.Fluent.HttpClient.Lib.Extensions
{
    internal static class VerbsExtension
    {
        internal static string GetDescription(this Verbs verb)
        {
            var description = string.Empty;

            switch (verb)
            {
                case Verbs.Get:
                    description = "Get";
                    break;
                case Verbs.Post:
                    description = "Post";
                    break;
                case Verbs.Put:
                    description = "Put";
                    break;
                case Verbs.Delete:
                    description = "Delete";
                    break;
                case Verbs.Patch:
                    description = "Patch";
                    break;
            }

            return description;
        }
    }
}