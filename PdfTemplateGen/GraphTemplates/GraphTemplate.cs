namespace PdfTemplateGen.GraphTemplates;

public abstract class GraphTemplate
{
    protected abstract TRectangle GraphArea { get; set; }
    protected abstract TGraphData GraphData { get; set; }
    protected abstract GraphSettings Settings { get; set; }

    internal abstract TGraphData FillItems();

    /// <summary>
    /// translate a point relative to series to a point relative to position on graph
    /// </summary>
    /// <param name="seriesVerticalPosition">vertical value | y value</param>
    /// <param name="seriesHorizontalPosition">horizontal value | x value</param>
    /// <param name="seriesVerticalStart">start value of y-axis | point closer to x-axis</param>
    /// <param name="seriesVerticalEnd">end value of y-axis | point farer from x-axis</param>
    /// <param name="seriesHorizontalStart">start value of x-axis | point closer to y-axis</param>
    /// <param name="seriesHorizontalEnd">end value of x-axis | point farer from y-axis</param>
    /// <param name="verticalDirection">direction of y-axis | FromTopToBot: x-axis is on top | FromBotToTop: x-axis is bottom</param>
    /// <param name="horizontalDirection">direction of x-axis | FromLeftToRight: y-axis is left | FromRightToLeft: y-axis is right</param>
    /// <returns>position on graph | returns null if position outside of graph-area</returns>
    protected (double VerticalPosition, double HorizontalPosition)? TranslateSeriesPointToChartPoint
    (
        double seriesVerticalPosition,
        double seriesHorizontalPosition,

        double seriesVerticalStart,
        double seriesVerticalEnd,
        VerticalDirection verticalDirection,

        double seriesHorizontalStart,
        double seriesHorizontalEnd,
        HorizontalDirection horizontalDirection
    )
    {
        bool positiveVerticalDirection = seriesVerticalEnd > seriesVerticalStart;
        bool positiveHorizontalDirection = seriesHorizontalEnd > seriesHorizontalStart;

        if (positiveVerticalDirection ? seriesVerticalPosition < seriesVerticalStart : seriesVerticalPosition > seriesVerticalStart) return null;
        if (positiveVerticalDirection ? seriesVerticalPosition > seriesVerticalEnd : seriesVerticalPosition < seriesVerticalEnd) return null;

        if (positiveHorizontalDirection ? seriesHorizontalPosition < seriesHorizontalStart : seriesHorizontalPosition > seriesHorizontalStart) return null;
        if (positiveHorizontalDirection ? seriesHorizontalPosition > seriesHorizontalEnd : seriesHorizontalPosition < seriesHorizontalEnd) return null;

        var verticalPercent = (seriesVerticalPosition - seriesVerticalStart) / (seriesVerticalEnd - seriesVerticalStart);
        var verticalOffset = (GraphArea.VerticalEnd - GraphArea.VerticalStart) * verticalPercent;
        var chartVerticalPosition = verticalDirection == VerticalDirection.FromBotToTop ?
            (positiveVerticalDirection ? GraphArea.VerticalEnd - verticalOffset : GraphArea.VerticalStart + verticalOffset) :
            (positiveVerticalDirection ? GraphArea.VerticalStart + verticalOffset : GraphArea.VerticalEnd - verticalOffset);

        var horizontalPercent = (seriesHorizontalPosition - seriesHorizontalStart) / (seriesHorizontalEnd - seriesHorizontalStart);
        var horizontalOffset = (GraphArea.HorizontalEnd - GraphArea.HorizontalStart) * horizontalPercent;
        var chartHorizontalPosition = horizontalDirection == HorizontalDirection.FromLeftToRight ?
            (positiveHorizontalDirection ? GraphArea.HorizontalStart + horizontalOffset : GraphArea.HorizontalEnd - horizontalOffset) :
            (positiveHorizontalDirection ? GraphArea.HorizontalEnd - horizontalOffset : GraphArea.HorizontalStart + horizontalOffset);

        return (chartVerticalPosition, chartHorizontalPosition);
    }
}