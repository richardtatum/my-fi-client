namespace MyFi.TheBadlands.Extensions;

public static class MultipartFormDataExtensions
{
    public static MultipartFormDataContent AddFormData(this MultipartFormDataContent content, string name, object value)
    {
        content.Add(new StringContent(value.ToString()), name);
        return content;
    }

}