using PdfTemplateGen.DocumentTemplates;

namespace PdfTemplateGenBlueprints;

internal class TableBlueprint : DocumentTemplate
{
    protected override DocumentItem Document { get; set; }
    protected override DocumentInformation Information { get; set; }
    protected override List<TemplateItemCollection> Items { get; set; }

    private readonly object[] Objects;

    #region settings
    private readonly double CellPaddingTop = 5;
    private readonly double CellPaddingBot = 5;
    private readonly double CellPaddingLeft = 10;
    private readonly double CellPaddingRight = 10;

    private readonly double DistanceLeft = 0;
    private readonly double DistanceRight = 0;
    private readonly double DistanceTop = 0;

    private readonly double[] HorizontalGridLayout = [1, 2, 1];
    #endregion

    #region utility
    private static double Max(double[] doubles)
    {
        var max = double.MinValue;
        foreach (var d in doubles) max = double.Max(d, max);
        return max;
    }
    private static double Sum(double[] doubles)
    {
        var sum = 0.0;
        foreach (var d in doubles) sum += d;
        return sum;
    }
    #endregion

    internal TableBlueprint(object[] objects)
    {
        Document = new(
            "Table",
            FileType.Document,
            PdfSharp.PageOrientation.Portrait,
            new()
            {
                A4PortraitSettings = new(),     /// DefaultSettings
                A4LandscapeSettings = new()     /// DefaultSettings
            },
            null
        );

        Information = new(
            Document.Settings.A4PortraitSettings.VerticalStart,
            Document.Settings.A4PortraitSettings.VerticalEnd,
            Document.Settings.A4PortraitSettings.HorizontalStart,
            Document.Settings.A4PortraitSettings.HorizontalEnd,
            [PdfSharp.Drawing.XBrushes.Black],
            [new PdfSharp.Drawing.XFont("Arial", 10)],
            [PdfSharp.Drawing.XPens.Black]
        );

        Objects = objects;

        Items = new();
    }

    protected override void FillItems()
    {

        #region calculate grid layout
        var tableWidth = (Document.Settings.A4PortraitSettings.HorizontalStart + DistanceLeft) - (Document.Settings.A4PortraitSettings.HorizontalEnd - DistanceRight);
        var gridCount = Sum(HorizontalGridLayout);
        var gridWidth = tableWidth / gridCount;

        var cellWidths = new List<double>();
        foreach (var layoutElement in HorizontalGridLayout) cellWidths.Add(layoutElement * gridWidth);
        #endregion

        #region header

        #endregion

        #region body

        #endregion

    }
}