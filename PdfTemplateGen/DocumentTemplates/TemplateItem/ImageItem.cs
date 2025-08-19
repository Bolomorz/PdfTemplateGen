using PdfSharp.Drawing;

namespace PdfTemplateGen.DocumentTemplates;

public class ImageItem : ITemplateItem
{
    /// <summary>
    /// Static | StaticRepeat | Dynamic
    /// </summary>
    public required ItemMode Mode { private get; set; }
    /// <summary>
    /// image data as byte[]
    /// </summary>
    public required byte[] ImageBytes { private get; set; }
    /// <summary>
    /// distance from TemplateItemCollection.(Static: HorizontalStart | Dynamic: LastHorizontalPosition):<para/> 
    /// startposition = horizontalstart + distanceleft
    /// </summary>
    public required double DistanceLeft { private get; set; }
    /// <summary>
    /// distance from TemplateItemCollection.VerticalStart:<para/>  
    /// startposition = verticalstart + distancetop
    /// </summary>
    public required double DistanceTop { private get; set; } 
    /// <summary>
    /// width from startposition: startposition + width
    /// </summary>
    public required double Width { private get; set; } 
    /// <summary>
    /// height from startposition: startposition + height
    /// </summary>
    public required double Height { private get; set; }

    private Information? Info;
    private class Information
    {
        internal required Rect Rect { get; set; }
    }

    internal override void CalcParameters(DocumentItem doc, CollectionInformation drawinfo)
    {
        var verticalstart = drawinfo.VerticalStart + DistanceTop;
        var horizontalstart = Mode is ItemMode.Dynamic ?
            drawinfo.LastHorizontalPosition + DistanceLeft :
            drawinfo.HorizontalStart + DistanceLeft;

        drawinfo.NeededHeight = Height > drawinfo.NeededHeight ? Height : drawinfo.NeededHeight;

        Info = new()
        {
            Rect = new()
            {
                Top = verticalstart,
                Bottom = verticalstart + Height,
                Left = horizontalstart,
                Right = horizontalstart + Width
            }
        };

        if(Mode == ItemMode.Dynamic) drawinfo.LastHorizontalPosition = Info.Rect.Right;
    }
    internal override void AdjustNewPage(double verticalstart)
    {
        if(Info is null) return;

        double h = Info.Rect.Bottom - Info.Rect.Top;
        Info.Rect.Top = verticalstart + DistanceTop;
        Info.Rect.Bottom = verticalstart + h + DistanceTop;
    }
    internal override void Draw(DocumentItem doc, CollectionInformation drawinfo)
    {
        if(Info is null) return;

        var rect = new XRect(
            new XPoint(Info.Rect.Left, Info.Rect.Top),
            new XPoint(Info.Rect.Right, Info.Rect.Bottom)
            );

        using(var stream = new MemoryStream(ImageBytes, 0, ImageBytes.Length, true, true))
        {
            var img = XImage.FromStream(stream);
            doc.GetCurrentXGraphics().DrawImage(img, rect);
        }
    }

    public static byte[] ImageFileToBytes(string path) => File.ReadAllBytes(path);
}