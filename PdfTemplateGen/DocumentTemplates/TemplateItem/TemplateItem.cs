namespace PdfTemplateGen.DocumentTemplates;

internal class Rect
{
    internal required double Top { get; set; }
    internal required double Left { get; set; }
    internal required double Bottom { get; set; }
    internal required double Right { get; set; }
}
public class Point
{
    public required double VerticalPosition { get; set; }
    public required double HorizontalPosition { get; set; }
}

public abstract class ITemplateItem
{
    internal abstract void CalcParameters(DocumentItem doc, CollectionInformation drawinfo);
    internal abstract void AdjustNewPage(double verticalstart);
    internal abstract void Draw(DocumentItem doc, CollectionInformation drawinfo);
}

public class TemplateItemCollection
{
    private ItemMode Mode;
    private List<ITemplateItem> Items;
    private double DistanceLeft, DistanceRight, DistanceTop;
    private CollectionInformation? DrawInfo;

    /// <summary>
    /// create item collection TIC
    /// </summary>
    /// <param name="mode">Static | StaticRepeat | Dynamic</param>
    /// <param name="distanceleft">distance from Settings.HorizontalStart:<para/>
    /// TIC.HorizontalStart = Settings.HorizontalStart + DistanceLeft</param>
    /// <param name="distanceright">distance from Settings.HorizontalEnd:<para/>
    /// TIC.HorizontalEnd = Settings.HorizontalEnd - DistanceRight</param>
    /// <param name="distancetop">distance from Settings.(Static: VerticalStart | Dynamic: LastVerticalPosition)<para/>
    /// TIC.VerticalStart = Settings.VerticalStart + distancetop</param>
    public TemplateItemCollection(ItemMode mode, double distanceleft, double distanceright, double distancetop)
    {
        Mode = mode;
        DistanceLeft = distanceleft;
        DistanceRight = distanceright;
        DistanceTop = distancetop;
        Items = new();
    }

    internal bool DrawCollection(DocumentItem doc, DocumentInformation drawinfo)
    {
        DrawInfo = new()
        {
            VerticalStart = IsDynamic() ? drawinfo.LastVerticalPosition + DistanceTop : drawinfo.VerticalStart + DistanceTop,
            HorizontalStart = drawinfo.HorizontalStart + DistanceLeft,
            LastHorizontalPosition = drawinfo.HorizontalStart + DistanceLeft,
            HorizontalEnd = drawinfo.HorizontalEnd - DistanceRight,
            Brushes = drawinfo.Brushes,
            Pens = drawinfo.Pens,
            Fonts = drawinfo.Fonts,
            NeededHeight = 0
        };

        foreach(var item in Items) item.CalcParameters(doc, DrawInfo);
        var newpage = AdjustNewPage(doc, drawinfo);
        foreach(var item in Items) item.Draw(doc, DrawInfo);

        if(IsDynamic()) drawinfo.LastVerticalPosition += newpage ? DrawInfo.NeededHeight : DistanceTop + DrawInfo.NeededHeight;

        return newpage;
    }

    public void AddItem(ITemplateItem item) => Items.Add(item);
    internal bool IsStatic() => Mode is ItemMode.Static || Mode is ItemMode.StaticRepeat;
    internal bool IsStaticRepeat() => Mode is ItemMode.StaticRepeat;
    internal bool IsDynamic() => Mode is ItemMode.Dynamic;

    private bool AdjustNewPage(DocumentItem doc, DocumentInformation drawinfo)
    {
        if(IsStatic() || DrawInfo is null) return false;

        if(DrawInfo.VerticalStart + DrawInfo.NeededHeight > drawinfo.VerticalEnd)
        {
            doc.AddPage();
            foreach(var item in Items) item.AdjustNewPage(drawinfo.VerticalStart);
            drawinfo.LastVerticalPosition = drawinfo.VerticalStart;
            return true;
        }
        return false;
    }
}