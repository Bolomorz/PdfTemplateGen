using PdfSharp.Drawing;

namespace PdfTemplateGen.DocumentTemplates;

public class DocumentInformation
{
    internal double LastVerticalPosition { get; set; }
    internal double VerticalStart { get; set; }
    internal double VerticalEnd { get; set; }
    internal double HorizontalStart { get; set; }
    internal double HorizontalEnd { get; set; }
    public List<XBrush> Brushes { get; set; }
    public List<XFont> Fonts { get; set; }
    public List<XPen> Pens { get; set; }

    public DocumentInformation(
        double verticalStart,
        double verticalEnd,
        double horizontalStart,
        double horizontalEnd,
        List<XBrush> brushes,
        List<XFont> fonts,
        List<XPen> pens
    )
    {
        LastVerticalPosition = verticalStart;
        VerticalStart = verticalStart;
        VerticalEnd = verticalEnd;
        HorizontalStart = horizontalStart;
        HorizontalEnd = horizontalEnd;
        Brushes = brushes;
        Fonts = fonts;
        Pens = pens;
    }
}

internal class CollectionInformation
{
    internal required double LastHorizontalPosition { get; set; }
    internal required double VerticalStart { get; set; }
    internal required double HorizontalStart { get; set; }
    internal required double HorizontalEnd { get; set; }
    internal required List<XBrush> Brushes { get; set; }
    internal required List<XFont> Fonts { get; set; }
    internal required List<XPen> Pens { get; set; }
    internal required double NeededHeight { get; set; }

}