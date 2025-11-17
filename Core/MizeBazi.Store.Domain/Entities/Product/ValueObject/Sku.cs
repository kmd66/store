using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using System.Text.RegularExpressions;

namespace MizeBazi.Store.Domain;

public class Sku : ValueObject
{
    private const int Length = 8;

    public string Value { get; }

    private Sku() { }

    private Sku(string value) => Value = value;

    public static Sku From(string value)=> Validation(value);

    public static Sku From(long categoryCode, int code)
    {
        if (categoryCode == 0)
            throw new ValidatorException(ProductConstants.Error_SkuCategoryCode);
        if (code == 0 && code < 1000)
            throw new ValidatorException(ProductConstants.Error_SkuCode);

        var categoryPart = categoryCode.ToString("D2").Substring(0, 2);
        var codePart = code.ToString("D4").Substring(0, 4);

        var value = $"MZ:{categoryCode}-{code}";
        return Validation(value);
    }

    private static Sku Validation(string value)
    {
        if (value.IsNullOrEmpty())
            throw new ValidatorException(ProductConstants.Error_Sku);
        
        value = value.Replace(" ", "");

        if (value.Length != Length)
            throw new ValidatorException(ProductConstants.Error_SkuLength);

        if (!IsValidFormat(value))
            throw new ValidatorException(ProductConstants.Error_SkuFormat);

        value = value.Trim().ToUpper();
        return new Sku(value);
    }

    private static bool IsValidFormat(string sku)
    {
        var pattern = @"^[A-Z0-9\-_]+$";
        return Regex.IsMatch(sku, pattern);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}



