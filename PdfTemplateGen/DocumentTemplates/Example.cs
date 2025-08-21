using PdfSharp.Drawing;

namespace PdfTemplateGen.DocumentTemplates;

internal class ExampleTemplate : DocumentTemplate
{
    protected override DocumentItem Document { get; set; }
    protected override DocumentInformation Information { get; set; }
    protected override List<TemplateItemCollection> Items { get; set; }

    object? ObjectToDisplay;

    internal ExampleTemplate(object? objectToDisplay)
    {
        Document = new(
            "ExampleTemplate",
            FileType.Document,
            PdfSharp.PageOrientation.Portrait,
            new() { A4PortraitSettings = new(), A4LandscapeSettings = new() },
            null
        );

        Information = new(
            Document.Settings.A4PortraitSettings.VerticalStart,
            Document.Settings.A4PortraitSettings.VerticalEnd,
            Document.Settings.A4PortraitSettings.HorizontalStart,
            Document.Settings.A4PortraitSettings.HorizontalEnd,
            [XBrushes.Black],
            [new XFont("Arial", 10), new XFont("Arial", 15), new XFont("Arial", 8)],
            [XPens.Black]
        );

        Items = new();

        ObjectToDisplay = objectToDisplay;
    }

    protected override void FillItems()
    {
        /*
        => fill Items with items according to ObjectToDisplay here
        */
        var item = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 0);
        item.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Example",
            DistanceLeft = 0,
            DistanceTop = 0,
            MaxWidth = 100,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        Items.Add(item);
    }
}