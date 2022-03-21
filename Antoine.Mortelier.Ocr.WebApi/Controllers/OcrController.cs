using Microsoft.AspNetCore.Mvc;

namespace Antoine.Mortelier.Ocr.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OcrController : ControllerBase
{
    private readonly Ocr _ocr;
    public OcrController(Ocr ocr)
    {
        _ocr = ocr;
    }
    [HttpPost]
    public async Task<IList<OcrResult>>
        OnPostUploadAsync([FromForm(Name = "files")] IList<IFormFile> files)
    {
        var images = new List<byte[]>();
        foreach (var formFile in files)
        {
            using var sourceStream = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            sourceStream.CopyTo(memoryStream);
            images.Add(memoryStream.ToArray());
        }
        
        var ocrResults = new Ocr().Read(images);

        return ocrResults;
    }
} 