namespace FastPatterns.Mapper.ConsoleApp.Extensions;
internal static class StringExtensions
{
  internal static string ReverseString(this string input)
  {
    if (input == null) return string.Empty;

    char[] charArray = input.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
  }
}
