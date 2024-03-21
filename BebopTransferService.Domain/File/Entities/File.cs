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

    private File(Guid id, Guid transferId, string documentTitle, string urlDocument) : base(id)
    {
        DocumentTitle = documentTitle;
        UrlDocument = urlDocument;
        TransferId = transferId;
    }

    private File() { }

    public string DocumentTitle { get; private set; }
    public string UrlDocument {  get; private set; }
    public Guid TransferId { get; private set; }

    public static File BuildFromDto(FileDto fileDto)
    {
        return new(fileDto.Id, fileDto.DocumentTitle, fileDto.UrlDocument);
    }

    public static File BuildFromDto(FileDto fileDto, Guid transferId)
    {
        return new(fileDto.Id, transferId, fileDto.DocumentTitle, fileDto.UrlDocument);
    }
}
