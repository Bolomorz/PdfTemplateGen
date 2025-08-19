using PdfSharp.Drawing;

namespace PdfTemplateGen.GraphTemplates;

internal class ExampleTemplate : GraphTemplate
{
    protected override TRectangle GraphArea { get; set; }
    protected override TGraphData GraphData { get; set; }
    protected override GraphSettings Settings { get; set; }

    private object? ObjectToDisplay;

    internal ExampleTemplate(double verticalPosition, double horizontalPosition, double verticalLength, double horizontalLength, object? objectToDisplay)
    {
        Settings = new()
        {
            AxisAreaLength = 20
        };
        GraphArea = new()
        {
            VerticalStart = verticalPosition,
            VerticalEnd = verticalPosition + verticalLength - Settings.AxisAreaLength,
            HorizontalStart = horizontalPosition + Settings.AxisAreaLength,
            HorizontalEnd = horizontalPosition + horizontalLength,
            Pen = XPens.AliceBlue,
            Brush = XBrushes.AliceBlue
        };
        GraphData = new() { TGraphArea = GraphArea };
        ObjectToDisplay = objectToDisplay;
    }

    internal override TGraphData FillItems()
    {
        /*
        => fill GraphData.ItemCollections with items according to ObjectToDisplay here
        */

        return GraphData;
    }
}