using PdfSharp.Drawing;

namespace PdfTemplateGen.DocumentTemplates;

public static class GeometryHelper
{
    public static GeometryItem Arc(
        ItemMode mode,
        double distanceLeft,
        double distanceTop,
        double height,
        double width,
        double startAngle,
        double sweepAngle,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Arc,
            Points = new(),
            FillMode = XFillMode.Alternate,
            DistanceLeft = distanceLeft,
            DistanceTop = distanceTop,
            Height = height,
            Width = width,
            StartAngle = startAngle,
            SweepAngle = sweepAngle,
            Tension = 0,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem Bezier(
        ItemMode mode,
        List<Point> points,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Bezier,
            Points = points,
            FillMode = XFillMode.Alternate,
            DistanceLeft = 0,
            DistanceTop = 0,
            Height = 0,
            Width = 0,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = 0,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem ClosedCurve(
        ItemMode mode,
        XFillMode fillMode,
        double distanceLeft,
        double distanceTop,
        double tension,
        List<Point> points,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.ClosedCurve,
            Points = points,
            FillMode = fillMode,
            DistanceLeft = distanceLeft,
            DistanceTop = distanceTop,
            Height = 0,
            Width = 0,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = tension,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem Curve(
        ItemMode mode,
        double distanceLeft,
        double distanceTop,
        double tension,
        List<Point> points,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Curve,
            Points = points,
            FillMode = XFillMode.Alternate,
            DistanceLeft = distanceLeft,
            DistanceTop = distanceTop,
            Height = 0,
            Width = 0,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = tension,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem Ellipse(
        ItemMode mode,
        double distanceLeft,
        double distanceTop,
        double height,
        double width,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Ellipse,
            Points = new(),
            FillMode = XFillMode.Alternate,
            DistanceLeft = distanceLeft,
            DistanceTop = distanceTop,
            Height = height,
            Width = width,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = 0,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem Line(
        ItemMode mode,
        List<Point> points,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Line,
            Points = points,
            FillMode = XFillMode.Alternate,
            DistanceLeft = 0,
            DistanceTop = 0,
            Height = 0,
            Width = 0,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = 0,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem Pie(
        ItemMode mode,
        double distanceLeft,
        double distanceTop,
        double height,
        double width,
        double startAngle,
        double sweepAngle,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Pie,
            Points = [],
            FillMode = XFillMode.Alternate,
            DistanceLeft = distanceLeft,
            DistanceTop = distanceTop,
            Height = height,
            Width = width,
            StartAngle = startAngle,
            SweepAngle = sweepAngle,
            Tension = 0,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem Polygon(
        ItemMode mode,
        XFillMode fillMode,
        List<Point> points,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Polygon,
            Points = points,
            FillMode = fillMode,
            DistanceLeft = 0,
            DistanceTop = 0,
            Height = 0,
            Width = 0,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = 0,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem Rectangle(
        ItemMode mode,
        double distanceLeft,
        double distanceTop,
        double height,
        double width,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.Rectangle,
            Points = [],
            FillMode = XFillMode.Alternate,
            DistanceLeft = distanceLeft,
            DistanceTop = distanceTop,
            Height = height,
            Width = width,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = 0,
            EllipseWidth = 0,
            EllipseHeight = 0,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }

    public static GeometryItem RoundedRectangle(
        ItemMode mode,
        double distanceLeft,
        double distanceTop,
        double height,
        double width,
        double ellipseWidth,
        double ellipseHeight,
        int brushIndex,
        int penIndex
    )
    {
        return new()
        {
            Mode = mode,
            Type = GeometryType.RoundedRectangle,
            Points = [],
            FillMode = XFillMode.Alternate,
            DistanceLeft = distanceLeft,
            DistanceTop = distanceTop,
            Height = height,
            Width = width,
            StartAngle = 0,
            SweepAngle = 0,
            Tension = 0,
            EllipseWidth = ellipseWidth,
            EllipseHeight = ellipseHeight,
            BrushIndex = brushIndex,
            PenIndex = penIndex
        };
    }
}

public class GeometryItem : ITemplateItem
{
    /// <summary>
    /// Static | StaticRepeat | Dynamic
    /// </summary>
    public required ItemMode Mode { private get; set; }
    /// <summary>
    /// Arc, Bezier, ClosedCurve, Curve, Ellipse, Line, Pie, Polygon, Rectangle, RoundedRectangle
    /// </summary>
    public required GeometryType Type { private get; set; }
    /// <summary>
    /// verticalposition: relative to startposition: startposition + verticalposition<para/>
    /// horizontalposition: relative to startposition: startposition + horizontalposition<para/>
    /// Bezier: 4 Points | ClosedCurve: 2 Points or more | Curve: 2 Points or more | Line: 2 Points | Polygon: 3 Points or more
    /// </summary>
    public required List<Point> Points { private get; set; }
    /// <summary>
    /// ClosedCurve | Polygon
    /// </summary>
    public required XFillMode FillMode { private get; set; }
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
    /// height from startposition: startposition + height<para/>
    /// Rectangle | RoundedRectangle | Pie | Ellipse | Arc
    /// </summary>
    public required double Height { private get; set; }
    /// <summary>
    /// width from startposition: startposition + width<para/>
    /// Rectangle | RoundedRectangle | Pie | Ellipse | Arc
    /// </summary>
    public required double Width { private get; set; }
    /// <summary>
    /// Arc | Pie
    /// </summary>
    public required double StartAngle { private get; set; }
    /// <summary>
    /// Arc | Pie
    /// </summary>
    public required double SweepAngle { private get; set; }
    /// <summary>
    /// ClosedCurve | Curve
    /// </summary>
    public required double Tension { private get; set; }
    /// <summary>
    /// RoundedRectangle
    /// </summary>
    public required double EllipseWidth { private get; set; }
    /// <summary>
    /// RoundedRectangle
    /// </summary>
    public required double EllipseHeight { private get; set; }
    /// <summary>
    /// index of brush in brushlist
    /// </summary>
    public required int BrushIndex { private get; set; }
    /// <summary>
    /// index of pen in penlist
    /// </summary>
    public required int PenIndex { private get; set; }

    private Information? Info;
    private class Information
    {
        internal required Rect Rect { get; set; }
    }

    internal override void CalcParameters(DocumentItem doc, CollectionInformation drawinfo)
    {
        if (!TestPointCount()) throw new Exception("geometry doesnt have required amount of points");

        var verticalstart = drawinfo.VerticalStart + DistanceTop;
        var horizontalstart = Mode is ItemMode.Dynamic ?
            drawinfo.LastHorizontalPosition + DistanceLeft :
            drawinfo.HorizontalStart + DistanceLeft;

        var height = DistanceTop + CalculateHeight();
        var width = CalculateWidth();

        drawinfo.NeededHeight = height > drawinfo.NeededHeight ? height : drawinfo.NeededHeight;

        Info = new()
        {
            Rect = new()
            {
                Top = verticalstart,
                Bottom = verticalstart + height,
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
        if (!TestPointCount()) throw new Exception("geometry doesnt have required amount of points");

        DrawGeometry(doc.GetCurrentXGraphics(), drawinfo.Pens[PenIndex], drawinfo.Brushes[BrushIndex]);
    }

    private void DrawGeometry(XGraphics gfx, XPen pen, XBrush brush)
    {
        if (Info is null) return;
        XPoint[] ps;
        XRect rect;
        switch (Type)
        {
            case GeometryType.Arc:
                rect = new(new XPoint(Info.Rect.Left, Info.Rect.Top), new XPoint(Info.Rect.Right, Info.Rect.Bottom));
                gfx.DrawArc(pen, rect, StartAngle, SweepAngle);
                break;
            case GeometryType.Bezier:
                ps = new XPoint[4];
                for (int i = 0; i < 4; i++) ps[i] = new(Points[i].HorizontalPosition + Info.Rect.Left, Points[i].VerticalPosition + Info.Rect.Top);
                gfx.DrawBezier(pen, ps[0], ps[1], ps[2], ps[3]);
                break;
            case GeometryType.ClosedCurve:
                ps = new XPoint[Points.Count];
                for (int i = 0; i < Points.Count; i++) ps[i] = new(Points[i].HorizontalPosition + Info.Rect.Left, Points[i].VerticalPosition + Info.Rect.Top);
                gfx.DrawClosedCurve(pen, brush, ps, FillMode, Tension);
                break;
            case GeometryType.Curve:
                ps = new XPoint[Points.Count];
                for (int i = 0; i < Points.Count; i++) ps[i] = new(Points[i].HorizontalPosition + Info.Rect.Left, Points[i].VerticalPosition + Info.Rect.Top);
                gfx.DrawCurve(pen, ps, Tension);
                break;
            case GeometryType.Ellipse:
                rect = new(new XPoint(Info.Rect.Left, Info.Rect.Top), new XPoint(Info.Rect.Right, Info.Rect.Bottom));
                gfx.DrawEllipse(pen, rect);
                break;
            case GeometryType.Line:
                ps = new XPoint[2];
                for (int i = 0; i < 2; i++) ps[i] = new(Points[i].HorizontalPosition + Info.Rect.Left, Points[i].VerticalPosition + Info.Rect.Top);
                gfx.DrawLine(pen, ps[0], ps[1]);
                break;
            case GeometryType.Pie:
                rect = new(new XPoint(Info.Rect.Left, Info.Rect.Top), new XPoint(Info.Rect.Right, Info.Rect.Bottom));
                gfx.DrawPie(pen, rect, StartAngle, SweepAngle);
                break;
            case GeometryType.Polygon:
                ps = new XPoint[Points.Count];
                for (int i = 0; i < Points.Count; i++) ps[i] = new(Points[i].HorizontalPosition + Info.Rect.Left, Points[i].VerticalPosition + Info.Rect.Top);
                gfx.DrawPolygon(brush, ps, FillMode);
                break;
            case GeometryType.Rectangle:
                rect = new(new XPoint(Info.Rect.Left, Info.Rect.Top), new XPoint(Info.Rect.Right, Info.Rect.Bottom));
                gfx.DrawRectangle(pen, brush, rect);
                break;
            case GeometryType.RoundedRectangle:
                rect = new(new XPoint(Info.Rect.Left, Info.Rect.Top), new XPoint(Info.Rect.Right, Info.Rect.Bottom));
                gfx.DrawRoundedRectangle(pen, rect, new XSize(EllipseWidth, EllipseHeight));
                break;
        }
    }
    private bool TestPointCount()
    {
        switch (Type)
        {
            case GeometryType.Bezier: return Points.Count == 4;
            case GeometryType.ClosedCurve: return Points.Count >= 2;
            case GeometryType.Curve: return Points.Count >= 2;
            case GeometryType.Line: return Points.Count == 2;
            case GeometryType.Polygon: return Points.Count >= 3;
            default: return true;
        }
    }
    private double CalculateHeight()
    {
        double max = 0;
        switch (Type)
        {
            case GeometryType.Arc: return Height;
            case GeometryType.Bezier: foreach (var p in Points) if (p.VerticalPosition > max) max = p.VerticalPosition; return max;
            case GeometryType.ClosedCurve: foreach (var p in Points) if (p.VerticalPosition > max) max = p.VerticalPosition; return max;
            case GeometryType.Curve: foreach (var p in Points) if (p.VerticalPosition > max) max = p.VerticalPosition; return max;
            case GeometryType.Ellipse: return Height;
            case GeometryType.Line: foreach (var p in Points) if (p.VerticalPosition > max) max = p.VerticalPosition; return max;
            case GeometryType.Pie: return Height;
            case GeometryType.Polygon: foreach (var p in Points) if (p.VerticalPosition > max) max = p.VerticalPosition; return max;
            case GeometryType.Rectangle: return Height;
            case GeometryType.RoundedRectangle: return Height;
            default: return 0;
        }
    }
    private double CalculateWidth()
    {
        double max = 0;
        switch (Type)
        {
            case GeometryType.Arc: return Width;
            case GeometryType.Bezier: foreach (var p in Points) if (p.HorizontalPosition > max) max = p.HorizontalPosition; return max;
            case GeometryType.ClosedCurve: foreach (var p in Points) if (p.HorizontalPosition > max) max = p.HorizontalPosition; return max;
            case GeometryType.Curve: foreach (var p in Points) if (p.HorizontalPosition > max) max = p.HorizontalPosition; return max;
            case GeometryType.Ellipse: return Width;
            case GeometryType.Line: foreach (var p in Points) if (p.HorizontalPosition > max) max = p.HorizontalPosition; return max;
            case GeometryType.Pie: return Width;
            case GeometryType.Polygon: foreach (var p in Points) if (p.HorizontalPosition > max) max = p.HorizontalPosition; return max;
            case GeometryType.Rectangle: return Width;
            case GeometryType.RoundedRectangle: return Width;
            default: return 0;
        }
    }
}