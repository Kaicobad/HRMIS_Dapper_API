namespace HRMIS_Dapper_API.Services.Interfaces
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}
