using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

public static class SlugHelper
{
    public static string ToSlug(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // 1. lowercase
        string str = input.ToLowerInvariant();

        // 2. riêng cho tiếng Việt: đ -> d
        str = str.Replace('đ', 'd').Replace('Đ', 'd');

        // 3. bỏ dấu (dùng Unicode normalization)
        str = str.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in str)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark) // bỏ dấu
            {
                sb.Append(c);
            }
        }

        str = sb.ToString().Normalize(NormalizationForm.FormC);

        // 4. bỏ ký tự không phải chữ, số, khoảng trắng, dấu gạch ngang
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

        // 5. cắt khoảng trắng đầu/cuối
        str = str.Trim();

        // 6. khoảng trắng -> dấu gạch ngang
        str = Regex.Replace(str, @"\s+", "-");

        // 7. gộp nhiều dấu gạch ngang liên tiếp thành 1
        str = Regex.Replace(str, "-+", "-");

        return str;
    }
}
