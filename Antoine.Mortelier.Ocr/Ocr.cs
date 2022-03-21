using System.Reflection;
using Tesseract;

namespace Antoine.Mortelier.Ocr;

public class Ocr
{
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
    public List<OcrResult> Read(IList<byte[]> images)
    {
        var tasks = new List<Task>();
        
        foreach (var image in images)
        {
            var task = Task.Run(() =>
            {
                var executingPath = GetExecutingPath();
                using var engine = new TesseractEngine(Path.Combine(executingPath, @"tessdata"), "fra", EngineMode.Default);
                using var pix = Pix.LoadFromMemory(image);
                var test = engine.Process(pix);
                var text = test.GetText();
                var confidence = test.GetMeanConfidence();
                return new OcrResult{Text = text, Confidence = confidence};
            });
            
            tasks.Add(task);
        }
        
        Task.WaitAll(tasks.ToArray());
        
        return tasks.Select(task => ((Task<OcrResult>) task).Result).ToList();
    }
}