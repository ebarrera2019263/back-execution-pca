using System.Linq;

namespace ExecutionPca.Api.Security
{
    public static class CifradoHelper
    {
        /// <summary>
        /// Reverses and trims prefix/suffix (2 at start, 2 at end).
        /// </summary>
        public static string ReversePassword(string encrypted)
        {
            if (string.IsNullOrWhiteSpace(encrypted) || encrypted.Length <= 4)
                return string.Empty;

            var trimmed = encrypted.Substring(2, encrypted.Length - 4);
            return new string(trimmed.Reverse().ToArray());
        }
    }
}
