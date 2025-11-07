namespace Application.Models.ZaloPayModels;

public interface IBaseFormRequest
{
    public Dictionary<string, string> ToDictionary();
}