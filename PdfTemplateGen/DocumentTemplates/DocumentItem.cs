using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Runtime.InteropServices;


namespace PdfTemplateGen.DocumentTemplates;

public class DocumentItem
{
    private PdfDocument Document;
    public DocumentSettings Settings { get; private set; }
    private List<PdfPage> Pages;
    private List<XGraphics> Gfxs;
    private int Index;
    private PageSize PageSize;
    private PageOrientation PageOrientation;
    private FileType Type;
    private string Title;
    private string? PageLayoutLinux;
    private string? PageLayoutWindows;

    public DocumentItem(string title, FileType type, PageOrientation orientation, DocumentSettings settings, string? pagelayoutpng)
    {
        Title = title;
        PageLayoutLinux = pagelayoutpng is not null ? $"Resources/{pagelayoutpng}" : null;
        PageLayoutWindows = pagelayoutpng is not null ? $@"Resources\{pagelayoutpng}" : null;
        Type = type;
        PageSize = PageSize.A4;
        PageOrientation = orientation;
        Index = 0;
        Document = new();
        Document.Info.Title = Title;
        Settings = settings;
        var firstpage = Document.AddPage();
        firstpage.Size = PageSize;
        firstpage.Orientation = PageOrientation;
        var firstgfx = XGraphics.FromPdfPage(firstpage);
        Pages = new() { firstpage };
        Gfxs = new() { firstgfx };
        DrawLayout(firstgfx);
    }

    internal void AddPage()
    {
        var page = Document.AddPage();
        page.Size = PageSize;
        page.Orientation = PageOrientation;
        var gfx = XGraphics.FromPdfPage(page);
        Pages.Add(page);
        Gfxs.Add(gfx);
        Index++;
        DrawLayout(gfx);
    }
    internal FileType GetCurrentType() => Type;
    internal PdfPage GetCurrentPage() => Pages[Index];
    public XGraphics GetCurrentXGraphics() => Gfxs[Index];
    internal byte[]? GetFile()
    {
        byte[]? pdf = null;
        using (MemoryStream stream = new())
        {
            Document.Save(stream, false);
            pdf = stream.ToArray();
        }
        return pdf;
    }
    

    private void DrawLayout(XGraphics gfx)
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && PageLayoutLinux is not null) DrawImage(gfx, PageLayoutLinux);
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && PageLayoutWindows is not null) DrawImage(gfx, PageLayoutWindows);
        DrawTitle(gfx, Title);
        DrawNumber(gfx, (Index + 1).ToString());
    }
    private void DrawImage(XGraphics gfx, string file)
    {
        var img = XImage.FromFile(file);
        var rect = new XRect(new XPoint(0,0), 
            PageOrientation == PageOrientation.Portrait ?
                new XPoint(DocumentSettings.A4Portrait.Width, DocumentSettings.A4Portrait.Height) :
                new XPoint(DocumentSettings.A4Landscape.Width, DocumentSettings.A4Landscape.Height));
        gfx.DrawImage(img, rect);
    }
    private void DrawTitle(XGraphics gfx, string title)
    {
        var rect = PageOrientation is PageOrientation.Portrait ? 
        new XRect(
            new XPoint(Settings.A4PortraitSettings.HorizontalStart, DocumentSettings.A4Portrait.Height - 15),
            new XPoint(Settings.A4PortraitSettings.HorizontalEnd, DocumentSettings.A4Portrait.Height - 5)
        ) :
        new XRect(
            new XPoint(Settings.A4LandscapeSettings.HorizontalStart, DocumentSettings.A4Landscape.Height - 15),
            new XPoint(Settings.A4LandscapeSettings.HorizontalEnd, DocumentSettings.A4Landscape.Height - 5)
        );
        var format = new XStringFormat(){LineAlignment = XLineAlignment.Center, Alignment = XStringAlignment.Near};
        var font = new XFont("Arial", 8);
        gfx.DrawString(title??" ", font, XBrushes.Black, rect, format);
    }
    private void DrawNumber(XGraphics gfx, string nr)
    {
        var rect = PageOrientation is PageOrientation.Portrait ?
        new XRect(
            new XPoint(DocumentSettings.A4Portrait.Width - 50, DocumentSettings.A4Portrait.Height - 50),
            new XPoint(DocumentSettings.A4Portrait.Width, DocumentSettings.A4Portrait.Height)
        ) :
        new XRect(
            new XPoint(DocumentSettings.A4Landscape.Width - 50, DocumentSettings.A4Landscape.Height - 5 - 50),
            new XPoint(DocumentSettings.A4Landscape.Width, DocumentSettings.A4Landscape.Height - 5)
        );
        var format = new XStringFormat(){LineAlignment = XLineAlignment.Center, Alignment = XStringAlignment.Center};
        var font = new XFont("Arial", 30);
        gfx.DrawString(nr, font, XBrushes.Black, rect, format);
    }
}