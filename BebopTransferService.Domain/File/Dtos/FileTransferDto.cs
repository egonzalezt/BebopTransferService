namespace BebopTransferService.Domain.File.Dtos;

using Entities;
using System.Text.Json.Serialization;

public class FileTransferDto
{
    [JsonPropertyName("documentTitle")]
    public string DocumentTitle { get; set; }
    [JsonPropertyName("urlDocument")]
    public string UrlDocument { get; set; }

    public static FileTransferDto BuildFromFile(File file)
    {
        return new()
        {
            DocumentTitle = file.DocumentTitle,
            UrlDocument = file.UrlDocument,
        };
    }
}
