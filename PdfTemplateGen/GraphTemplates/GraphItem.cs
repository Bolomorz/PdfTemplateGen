using PdfSharp.Drawing;

namespace PdfTemplateGen.GraphTemplates;

/// <summary>
/// template point as circle with radius
/// </summary>
public class TPoint
{
    /// <summary>
    /// vertical position of circle on graph | pos.y | relative to grapharea.topleft
    /// </summary>
    public required double VerticalPosition;
    /// <summary>
    /// horizontal position of circle on graph | pos.x | relative to grapharea.topleft
    /// </summary>
    public required double HorizontalPosition;
    /// <summary>
    /// radius of circle 
    /// </summary>
    public required double Radius;
    /// <summary>
    /// color of pen for drawing border
    /// </summary>
    public required XPen Pen;
    /// <summary>
    /// color of brush for drawing surface area
    /// </summary>
    public required XBrush Brush;
}

/// <summary>
/// template line
/// </summary>
public class TLine
{
    /// <summary>
    /// vertical start position | start.y | relative to grapharea.topleft
    /// </summary>
    public required double VerticalStart;
    /// <summary>
    /// horizontal start position | start.x | relative to grapharea.topleft
    /// </summary>
    public required double HorizontalStart;
    /// <summary>
    /// vertical end position | end.y | relative to grapharea.topleft
    /// </summary>
    public required double VerticalEnd;
    /// <summary>
    /// horizontal end position | end.x | relative to grapharea.topleft
    /// </summary>
    public required double HorizontalEnd;
    /// <summary>
    /// color of pen drawing line
    /// </summary>
    public required XPen Pen;
}

/// <summary>
/// template text
/// </summary>
public class TText
{
    /// <summary>
    /// vertical position of text on graph - center alignment | pos.y | relative to grapharea.topleft
    /// </summary>
    public required double VerticalPosition;
    /// <summary>
    /// horizontal position of text on graph - center alignment | pos.x | relative to grapharea.topleft
    /// </summary>
    public required double HorizontalPosition;
    /// <summary>
    /// content of text
    /// </summary>
    public required string Content;
    /// <summary>
    /// font of text
    /// </summary>
    public required XFont Font;
    /// <summary>
    /// color of brush drawing text
    /// </summary>
    public required XBrush Brush;
    /// <summary>
    /// rotate text with angle in degrees
    /// </summary>
    public required double? Rotate;
}

/// <summary>
/// template rectangle
/// </summary>
public class TRectangle
{
    /// <summary>
    /// vertical start position | rect.y1 | relative to grapharea.topleft
    /// </summary>
    public required double VerticalStart;
    /// <summary>
    /// horizontal start position | rect.x1 | relative to grapharea.topleft
    /// </summary>
    public required double HorizontalStart;
    /// <summary>
    /// vertical end position | rect.y2 | relative to grapharea.topleft
    /// </summary>
    public required double VerticalEnd;
    /// <summary>
    /// horizontal end position | rect.x2 | relative to grapharea.topleft
    /// </summary>
    public required double HorizontalEnd;
    /// <summary>
    /// color of pen drawing border
    /// </summary>
    public required XPen Pen;
    /// <summary>
    /// color of brush drawing surface
    /// </summary>
    public required XBrush Brush;
    internal double Width() => HorizontalEnd - HorizontalStart;
    internal double Heigth() => VerticalEnd - VerticalStart;
}

/// <summary>
/// collection of graph-template items
/// </summary>
public class TGraphData
{
    /// <summary>
    /// area of graph relative to pdf
    /// </summary>
    public required TRectangle TGraphArea;
    public List<TLine> Lines = new();
    public List<TPoint> Points = new();
    public List<TText> Texts = new();
    public List<TRectangle> Rectangles = new();
}