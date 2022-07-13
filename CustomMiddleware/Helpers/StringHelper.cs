namespace CustomMiddleware.Helpers;
public static class StringHelper
{
    public static string RemoveLineBreak(this string input) => input.Replace("\n", "");
}
