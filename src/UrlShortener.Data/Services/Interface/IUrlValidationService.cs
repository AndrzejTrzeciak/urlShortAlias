namespace UrlShortener.Data.Services.Interface
{
    public interface IUrlValidationService
    {
        bool ValidateUrl(string url);
    }
}
