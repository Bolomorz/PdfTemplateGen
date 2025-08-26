using PdfSharp.Drawing;
using PdfTemplateGen.GraphTemplates;

namespace PdfTemplateGenExample.ExamOverview;

/// 
/// this file defines an example template depicting a grade distribution
/// the types for exam and grades are defined in 'ExamObjects.cs'
/// 

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
        /// defining settings
        /// 
        /// width and height of axis area | distance of y/x-axes to border
        /// these definitions are non-commital, they are to be used in FillItems()
        Settings = new()
        {
            AxisAreaLength = 20    
        };

        /// defining the graph area
        /// 
        /// positional definitions are non-commital, they are to be used in FillItems() as borders
        /// 
        /// color definitions define the whole graph area, as background/border color
        /// 
        /// positional definitions should be used to define location of y/x-axes or borders of graph:
        ///     -   x-axes: top or bottom
        ///     -   y-axes: left or right
        /// here the following is defined
        ///     vertical:   - start from the very top
        ///                 - goes to bottom with distanceToBot=Settings.AxisAreaLength
        ///                 -> x-axis is at the bottom
        ///     horizontal: - start from the left with distanceToLeft=Settings.AxisAreaLength
        ///                 - goes to the very right
        ///                 -> y-axis is on the left
        GraphArea = new()
        {
            VerticalStart = verticalPosition,                                               
            VerticalEnd = verticalPosition + verticalLength - Settings.AxisAreaLength,      
            HorizontalStart = horizontalPosition + Settings.AxisAreaLength,                 
            HorizontalEnd = horizontalPosition + horizontalLength,                            
            Pen = XPens.AliceBlue,                                                          
            Brush = XBrushes.AliceBlue                                                     
        };

        /// defining the graph data
        /// 
        /// lists for use in FillItems() are automatically created
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
        /// congruent with definition for GraphArea, y-axis is on the left of graph area
        GraphData.Lines.Add(new()
        {
            VerticalStart = GraphArea.VerticalStart,
            VerticalEnd = GraphArea.VerticalEnd,
            HorizontalStart = GraphArea.HorizontalStart,
            HorizontalEnd = GraphArea.HorizontalStart,
            Pen = XPens.Black
        });

        /// add line for x-axis: grade [1, 6]
        /// congruent with definition for GraphArea, x-axis is at the bottom of graph area
        GraphData.Lines.Add(new()
        {
            VerticalStart = GraphArea.VerticalEnd,
            VerticalEnd = GraphArea.VerticalEnd,
            HorizontalStart = GraphArea.HorizontalStart,
            HorizontalEnd = GraphArea.HorizontalEnd,
            Pen = XPens.Black
        });
        #endregion

        #region bar graph
        for (int i = 0; i < info.Distributions.Length; i++)
        {
            /// translate (i, percenti) to chartpoint | calculate position of y-value
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
            /// the translated point is positioned relative to GraphArea and needs to be further modified 
            /// VerticalPosition = GraphArea.VerticalStart + point.Value.VerticalMagnitude
            /// HorizontalPosition = GraphArea.HorizontalStart + point.Value.HorizontalMagnitude

            /// add bar graph according to position of y-value
            if (point is not null)
            {
                /// y-value as text in percent | at the top of bar graph
                GraphData.Texts.Add(new()
                {
                    VerticalPosition = GraphArea.VerticalStart + point.Value.VerticalMagnitude,
                    HorizontalPosition = GraphArea.HorizontalStart + point.Value.HorizontalMagnitude,
                    Content = $"{Math.Round(percent, 2)} %",
                    Font = ChartFont,
                    Brush = XBrushes.Black,
                    Rotate = null
                });

                /// bar graph as rectangle | from translated point-position to x-axis
                GraphData.Rectangles.Add(new() 
                {
                    VerticalStart = GraphArea.VerticalStart + point.Value.VerticalMagnitude,
                    VerticalEnd = GraphArea.VerticalEnd,
                    HorizontalStart = GraphArea.HorizontalStart + point.Value.HorizontalMagnitude - 15,
                    HorizontalEnd = GraphArea.HorizontalStart + point.Value.HorizontalMagnitude + 15,
                    Pen = XPens.LightGreen,
                    Brush = XBrushes.LightGreen
                });

                /// x-axis description | grade value | below x-axis
                GraphData.Texts.Add(new() 
                {
                    VerticalPosition = GraphArea.VerticalEnd + Settings.AxisAreaLength / 2,
                    HorizontalPosition = GraphArea.HorizontalStart + point.Value.HorizontalMagnitude,
                    Content = $"{i + 1}",
                    Font = ChartFont,
                    Brush = XBrushes.Black,
                    Rotate = null
                });
            }
        }
        #endregion

        return GraphData;
    }
}