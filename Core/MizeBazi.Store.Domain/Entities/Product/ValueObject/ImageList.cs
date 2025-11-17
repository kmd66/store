using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class ProductImages : ValueObject
{
    private readonly List<string> _images = new();
    public IReadOnlyList<string> Images => _images.AsReadOnly();

    private const int MaxImages = 10;

    private ProductImages() { }

    private ProductImages(IEnumerable<string> urls)
    {
        foreach (var url in urls)
            Add(url);
    }

    public static ProductImages From(string value)
    {
        if (value.IsNullOrEmpty())
            throw new ValidatorException(ProductConstants.Error_Images);

        IEnumerable<string> urls;
        try
        {
            urls = value.JsonToObject<IEnumerable<string>>();
        }
        catch(Exception ex) { throw new ValidatorException(ex.Message); }

        return new ProductImages(urls);
    }

    public void Add(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ValidatorException("Image URL is invalid");

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            throw new ValidatorException("Invalid image URL format");

        if (_images.Count >= MaxImages)
            return;

        if (_images.Contains(url))
            return;

        _images.Add(url);
    }

    public void Remove(string url)
    {
        _images.Remove(url);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        foreach (var img in _images)
            yield return img;
    }
}




