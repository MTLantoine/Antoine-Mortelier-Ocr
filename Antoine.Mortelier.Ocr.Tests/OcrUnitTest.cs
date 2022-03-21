using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Antoine.Mortelier.Ocr.Tests;

public class OcrUnitTest
{
    [Fact]
    public async Task ImagesShouldBeReadCorrectly()
    {
        var executingPath = GetExecutingPath();
        var images = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "images")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            images.Add(imageBytes);
        }

        var ocrResults = new Ocr().Read(images);

        Assert.Equal(ocrResults[0].Text, "ampleur. La pénurie n'est pas générale, il\nfaut le rappeler. Certains profils sont plus\nrecherchés que d'autres.\n");
        Assert.InRange(ocrResults[0].Confidence, 0.85, 0.95);
        Assert.Equal(ocrResults[1].Text, "Dans de nombreuses technologies, il\nexiste des certifications. Le monde\nMicrosoft en propose de nombreuses pour\n");
        Assert.InRange(ocrResults[1].Confidence, 0.85, 0.95);
        Assert.Equal(ocrResults[2].Text, "Certaines le sont depuis cet été, d'autres\nprendront fin en janvier 2021\n\nListe complète des certifications et\nexamens retirés :\n");
        Assert.InRange(ocrResults[2].Confidence, 0.85, 0.95);
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
} 