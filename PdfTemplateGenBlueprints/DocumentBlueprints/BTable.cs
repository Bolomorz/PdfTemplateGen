using System.ComponentModel;
using PdfTemplateGen.DocumentTemplates;

namespace PdfTemplateGenBlueprints;

internal class TableBlueprint : DocumentTemplate
{
    protected override DocumentItem Document { get; set; }
    protected override DocumentInformation Information { get; set; }
    protected override List<TemplateItemCollection> Items { get; set; }

    #region settings
    private readonly double CellPaddingTop = 5;
    private readonly double CellPaddingBot = 5;
    private readonly double CellPaddingLeft = 10;
    private readonly double CellPaddingRight = 10;

    private readonly double DistanceLeft = 0;
    private readonly double DistanceRight = 0;
    private readonly double DistanceTop = 0;


    #endregion

    internal TableBlueprint()
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

        Items = new();
    }

    protected override void FillItems()
    {
        
    }
}