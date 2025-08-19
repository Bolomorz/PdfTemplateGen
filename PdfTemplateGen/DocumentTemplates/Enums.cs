namespace PdfTemplateGen.DocumentTemplates;

public enum ItemMode
{
    /// <summary>
    /// item with fixed position on page, does not repeat for each new page
    /// </summary>
    Static,
    /// <summary>
    /// item with fixed position on page, also repeats for each new page
    /// </summary>
    StaticRepeat,
    /// <summary>
    /// item with dynamic position on page, relative to last drawn dynamic item
    /// </summary>
    Dynamic
}
public enum FileType { Document, Template }
public enum GeometryType {Arc, Bezier, ClosedCurve, Curve, Ellipse, Line, Pie, Polygon, Rectangle, RoundedRectangle}