using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace PdfTemplateGen.DocumentTemplates;

public class StringItem : ITemplateItem
{
    /// <summary>
    /// Static | StaticRepeat | Dynamic
    /// </summary>
    public required ItemMode Mode { private get; set; }
    /// <summary>
    /// FormatString: 'pre{0}mid{1}next{0}post' for keyvalue from {0} to {n}<para/>
    /// KeyValue Pairs: Key[Value]: with index from {0} to {n} in FormatString
    /// </summary>
    public required string Content { private get; set; }
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
    /// max width from startposition before linebreak occurs
    /// </summary>
    public required double MaxWidth { private get; set; }
    /// <summary>
    /// index of used font in fontlist
    /// </summary>
    public required int FontIndex { private get; set; }
    /// <summary>
    /// index of used brush in brushlist
    /// </summary>
    public required int BrushIndex { private get; set; }
    /// <summary>
    /// index of used pen in penlist
    /// </summary>
    public required int PenIndex { private get; set; }
    /// <summary>
    /// true: text will be underlined
    /// </summary>
    public required bool Underline { private get; set; }

    private class Information
    {
        internal required string Text { get; set; }
        internal required Rect Rect { get; set; }
        internal required XFont Font { get; set; }
    }
    private Information? Info;

    internal override void CalcParameters(DocumentItem doc, CollectionInformation drawinfo)
    {
        var full = Content;
        var verticalstart = drawinfo.VerticalStart + DistanceTop;
        var horizontalstart = Mode is ItemMode.Dynamic ?
            drawinfo.LastHorizontalPosition + DistanceLeft :
            drawinfo.HorizontalStart + DistanceLeft;
        if (full is null || full.Length == 0) full = " ";
        var font = drawinfo.Fonts[FontIndex];

        var rect = new XRect(
            new XPoint(horizontalstart, 0),
            new XPoint(horizontalstart + MaxWidth, 800)
            );
        var ptm = new TextMeasurements(doc.GetCurrentXGraphics(), full, font, rect);
        var height = ptm.MeasureText();

        var width = doc.GetCurrentXGraphics().MeasureString(full, font).Width;
        if (width > MaxWidth) width = MaxWidth;

        drawinfo.NeededHeight = DistanceTop + height > drawinfo.NeededHeight ? DistanceTop + height : drawinfo.NeededHeight;

        Info = new()
        {
            Text = full,
            Font = font,
            Rect = new()
            {
                Bottom = verticalstart + height,
                Top = verticalstart,
                Left = horizontalstart,
                Right = horizontalstart + width
            }
        };

        if (Mode == ItemMode.Dynamic) drawinfo.LastHorizontalPosition = Info.Rect.Right;
    }

    internal override void AdjustNewPage(double verticalstart)
    {
        if (Info is null) return;

        double h = Info.Rect.Bottom - Info.Rect.Top;
        Info.Rect.Top = verticalstart + DistanceTop;
        Info.Rect.Bottom = verticalstart + h + DistanceTop;
    }

    internal override void Draw(DocumentItem doc, CollectionInformation drawinfo)
    {
        if (Info is null) return;

        var rect = new XRect(
            new XPoint(Info.Rect.Left, Info.Rect.Top),
            new XPoint(Info.Rect.Right, Info.Rect.Bottom)
            );
        var tf = new XTextFormatter(doc.GetCurrentXGraphics());
        tf.DrawString(Info.Text, Info.Font, drawinfo.Brushes[BrushIndex], rect, XStringFormats.TopLeft);

        if (Underline)
        {
            var p1 = new XPoint(Info.Rect.Left, Info.Rect.Bottom + 1);
            var p2 = new XPoint(Info.Rect.Right, Info.Rect.Bottom + 1);
            doc.GetCurrentXGraphics().DrawLine(drawinfo.Pens[PenIndex], p1, p2);
        }
        
    }
}