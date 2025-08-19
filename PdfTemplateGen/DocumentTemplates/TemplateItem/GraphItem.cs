using PdfSharp.Drawing;
using PdfTemplateGen.GraphTemplates;

namespace PdfTemplateGen.DocumentTemplates;

public class GraphItem : ITemplateItem
{
    /// <summary>
    /// Static | StaticRepeat | Dynamic
    /// </summary>
    public required ItemMode Mode { private get; set; }
    /// <summary>
    /// items of chart from Graphing.GrahpWriter.Write(Template ts);
    /// </summary>
    public required TGraphData Data { private get; set; }
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
    /// width from startposition
    /// </summary>
    public required double Width { private get; set; } 
    /// <summary>
    /// height from startposition
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
        if (Info is null) return;

        var gfx = doc.GetCurrentXGraphics();

        var xrect = new XRect(
            new XPoint(Info.Rect.Left, Info.Rect.Top),
            new XPoint(Info.Rect.Right, Info.Rect.Bottom));
        gfx.DrawRectangle(Data.TGraphArea.Pen, Data.TGraphArea.Brush, xrect);

        foreach (var rect in Data.Rectangles)
        {
            xrect = new(
                new XPoint(Info.Rect.Left + rect.HorizontalStart, Info.Rect.Top + rect.VerticalStart),
                new XPoint(Info.Rect.Left + rect.HorizontalEnd, Info.Rect.Top + rect.VerticalEnd));
            gfx.DrawRectangle(rect.Pen, rect.Brush, xrect);
        }
        foreach (var line in Data.Lines)
        {
            var start = new XPoint(Info.Rect.Left + line.HorizontalStart, Info.Rect.Top + line.VerticalStart);
            var end = new XPoint(Info.Rect.Left + line.HorizontalEnd, Info.Rect.Top + line.VerticalEnd);
            gfx.DrawLine(line.Pen, start, end);
        }
        foreach (var point in Data.Points)
        {
            xrect = new(
                new XPoint(Info.Rect.Left + point.HorizontalPosition - 3, Info.Rect.Top + point.VerticalPosition - 3),
                new XPoint(Info.Rect.Left + point.HorizontalPosition + 3, Info.Rect.Top + point.VerticalPosition + 3));
            gfx.DrawEllipse(point.Pen, point.Brush, xrect);
        }
        foreach (var text in Data.Texts)
        {
            if (text.Rotate is not null)
            {
                var state = gfx.Save();

                gfx.TranslateTransform(Info.Rect.Left + text.HorizontalPosition, Info.Rect.Top + text.VerticalPosition);
                gfx.RotateTransform((double)text.Rotate);

                gfx.DrawString(text.Content, text.Font, text.Brush, new XPoint(0, 0), new() { Alignment = XStringAlignment.Center });

                gfx.Restore(state);
            }
            else
            {
                var pos = new XPoint(Info.Rect.Left + text.HorizontalPosition, Info.Rect.Top + text.VerticalPosition);
                gfx.DrawString(text.Content, text.Font, text.Brush, pos, new() { Alignment = XStringAlignment.Center });
            }
        }
    }
}