namespace LnzSoftware.Swashbuckle.AspNetCore.CommandQueryBinder.Common;

public static class StringExtensions
{
    public static string ToCamelCase(this string str)
    {
        var words = str.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
        var leadWord = words[0].ToLower();
        var tailWords = words.Skip(1)
            .Select(word => char.ToUpper(word[0]) + word.Substring(1))
            .ToArray();
        return $"{leadWord}{string.Join(string.Empty, tailWords)}";
    }

    public static string ToLowerFirstChar(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return str;
        if (str.Count() == 1) return str.ToLowerInvariant();

        return $"{char.ToLowerInvariant(str[0])}{str.Substring(1)}";
    }
}
