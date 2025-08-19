using System.Runtime.InteropServices;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PdfTemplateGen;

public class PdfFile
{
    public required bool Success { get; set; }
    internal byte[]? Data { get; set; }
    private const string FilePathLinux = "Resources/Temp.pdf";
    private const string FilePathWindows = @"Resources\Temp.pdf";

    public void OpenDocument(string? filepath)
    {
        if (Data is not null)
        {
            PdfDocument document;
            using (MemoryStream stream = new(Data))
            {
                document = PdfReader.Open(stream);
                string path = filepath is not null ? filepath : RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? FilePathWindows : FilePathLinux;
                document.Save(path);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
            }
        }
    }
}