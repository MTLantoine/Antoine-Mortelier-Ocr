using Antoine.Mortelier.Ocr;

var imagePaths = args;
var images = new List<byte[]>();

foreach (var imagePath in imagePaths)
{
    var imageBytes = await File.ReadAllBytesAsync(imagePath);
    images.Add(imageBytes);
}

var ocrResults = new Ocr().Read(images);

foreach (var ocrResult in ocrResults)
{
    System.Console.WriteLine($"Confidence :{ocrResult.Confidence}");
    System.Console.WriteLine($"Text :{ocrResult.Text}");
} 
