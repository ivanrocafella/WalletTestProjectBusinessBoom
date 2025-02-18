using System.Text.RegularExpressions;

namespace WalletTestProjectBusinessBoom.API.Extensions
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null) return null;

            // Remove "Controller" from the end and convert to lowercase
            var transformed = Regex.Replace(value.ToString(), "Controller$", "", RegexOptions.IgnoreCase);
            return transformed.ToLowerInvariant();
        }
    }
}
