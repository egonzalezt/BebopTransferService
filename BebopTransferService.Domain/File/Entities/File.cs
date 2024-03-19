namespace BebopTransferService.Domain.File.Entities;

using Domain.File.Dtos;
using Domain.SharedKernel;

public class File : Entity
{
    private File(Guid id, string documentTitle, string urlDocument) : base(id)
    {
        DocumentTitle = documentTitle;
        UrlDocument = urlDocument;
    }

    private File() { }

    public string DocumentTitle { get; private set; }
    public string UrlDocument {  get; private set; }

    public File BuildFromDto(FileDto fileDto)
    {
        return new(fileDto.Id, fileDto.DocumentTitle, fileDto.UrlDocument);
    }
}
