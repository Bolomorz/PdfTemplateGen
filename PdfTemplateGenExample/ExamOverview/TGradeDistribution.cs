using PdfSharp.Drawing;
using PdfTemplateGen.GraphTemplates;

namespace PdfTemplateGenExample.ExamOverview;

/// <summary>
/// graph-template depicting a grade distribution of exam as bar graph
/// </summary>
internal class TGradeDistribution : GraphTemplate
{
    protected override TRectangle GraphArea { get; set; }
    protected override TGraphData GraphData { get; set; }
    protected override GraphSettings Settings { get; set; }

    private readonly Exam Exam;
    private readonly List<Grade> Grades;

    private readonly XFont ChartFont = new("Arial", 8);

    /// <summary>
    /// graph-template depicting a grade distribution of exam as bar graph
    /// </summary>
    /// 
    /// <param name="verticalPosition">
    /// vertical position relative to template-item on pdf page
    /// <para>this can be 0 in most cases (where graph-area.top == template-item.top)</para>
    /// </param>
    /// 
    /// <param name="horizontalPosition">
    /// horizontal position relative to template-item on pdf page
    /// <para>this can be 0 in most cases (where graph-area.left == template-item.left)</para>
    /// </param>
    /// 
    /// <param name="verticalLength">
    /// vertical height of graph-area
    /// <para>this should be the same as template-item.height in most cases (where graph-area.bot == template-item.bot)</para>
    /// </param>
    /// 
    /// <param name="horizontalLength">
    /// horizontal width of graph-area
    /// <para>this should be the same as template-item.width in most cases (where graph-area.right == template-item.right)</para>
    /// </param>
    /// 
    /// <param name="exam"></param>
    /// <param name="grades"></param>
    internal TGradeDistribution(
        double verticalPosition,
        double horizontalPosition,
        double verticalLength,
        double horizontalLength,
        Exam exam,
        List<Grade> grades
    )
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
        GraphData = new()
        {
            TGraphArea = GraphArea
        };
        Grades = grades;
        Exam = exam;
    }

    public override TGraphData FillItems()
    {
        var info = Exam.GetExamInfo(Grades);

        #region axes
        /// add line for y-axis: percent distribution per grade [0, 100]
        GraphData.Lines.Add(new()
        {
            VerticalStart = GraphArea.VerticalStart,
            VerticalEnd = GraphArea.VerticalEnd,
            HorizontalStart = Settings.AxisAreaLength,
            HorizontalEnd = Settings.AxisAreaLength,
            Pen = XPens.Black
        });

        /// add line for x-axis: grade [1, 6]
        GraphData.Lines.Add(new()
        {
            VerticalStart = GraphArea.VerticalEnd,
            VerticalEnd = GraphArea.VerticalEnd,
            HorizontalStart = Settings.AxisAreaLength,
            HorizontalEnd = GraphArea.HorizontalEnd,
            Pen = XPens.Black
        });
        #endregion

        #region calc dimensions
        /// calc dimensions for x-interval
        /// width-interval are known, as there are 6 Grades, each with the same distance
        var totalWidth = GraphArea.HorizontalEnd - GraphArea.HorizontalStart;
        var intervalWidth = totalWidth / (info.Distributions.Length + 1);       /// increment in horizontal direction

        var horizontalPosition = Settings.AxisAreaLength + intervalWidth;       /// horizontal startposition

        /// because the magnitudes of y-values is unknown (anything between [0, 100]), the y-value of each distribution has to be translated/calculated
        #endregion

        #region bar graph
        for (int i = 0; i < info.Distributions.Length; i++)
        {
            /// translate (i, percenti) to chartpoint | calculate magnitude of y-value
            var percent = (info.Distributions[i].Amount / info.AmountTotal) * 100;
            var point = TranslateSeriesPointToChartPoint(
                percent,                            /// | y-value
                i + 1,                              /// | x-value

                0,                                  /// |
                100,                                /// | y-axis | from 0 to 100 | with direction from bottom towards top
                VerticalDirection.FromBotToTop,     /// |

                0,                                  /// | 
                (info.Distributions.Length + 1),    /// | x-axis | from 0 to 7 | with direction from left towards right
                HorizontalDirection.FromLeftToRight /// |
            );

            /// add bar graph according to magnitude of y-value
            if (point is not null)
            {
                GraphData.Texts.Add(new()           /// y-value in percent
                {
                    VerticalPosition = point.Value.VerticalPosition,
                    HorizontalPosition = horizontalPosition,
                    Content = $"{Math.Round(percent, 2)} %",
                    Font = ChartFont,
                    Brush = XBrushes.Black,
                    Rotate = null
                });

                GraphData.Rectangles.Add(new()      /// bar graph 
                {
                    VerticalStart = point.Value.VerticalPosition,
                    VerticalEnd = GraphArea.VerticalEnd,
                    HorizontalStart = horizontalPosition - 15,
                    HorizontalEnd = horizontalPosition + 15,
                    Pen = XPens.LightGreen,
                    Brush = XBrushes.LightGreen
                });

                GraphData.Texts.Add(new()           /// x-axis description | grade value
                {
                    VerticalPosition = GraphArea.VerticalEnd + Settings.AxisAreaLength / 2,
                    HorizontalPosition = horizontalPosition,
                    Content = $"{i+1}",
                    Font = ChartFont,
                    Brush = XBrushes.Black,
                    Rotate = null
                });
            }

            horizontalPosition += intervalWidth;
        }
        #endregion

        return GraphData;
    }
}