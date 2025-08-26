using PdfSharp.Drawing;
using PdfTemplateGen.DocumentTemplates;
using PdfTemplateGen.GraphTemplates;
using PdfTemplateGen;

namespace PdfTemplateGenExample.ExamOverview;


/// 
/// this file defines an example template depicting an exam overview
/// the types for exam and grades are defined in 'ExamObjects.cs'
/// 

/// <summary>
/// overview of an exam:
/// <para>- info about exam</para>
/// <para>- grades as table</para>
/// <para>- grade distribution as bar graph</para>
/// <para>- grade distribution as table</para>
/// </summary>
internal class TExamOverview : DocumentTemplate
{
    protected override DocumentItem Document { get; set; }
    protected override DocumentInformation Information { get; set; }
    protected override List<TemplateItemCollection> Items { get; set; }

    private readonly Exam Exam;
    private readonly List<Grade> Grades;

    /// <summary>
    /// overview of an exam:
    /// <para>- info about exam</para>
    /// <para>- grades as table</para>
    /// <para>- grade distribution as bar graph</para>
    /// <para>- grade distribution as table</para>
    /// </summary>
    /// <param name="exam">exam description</param>
    /// <param name="grades">grades of exam</param>
    internal TExamOverview(
        Exam exam,
        List<Grade> grades
    )
    {
        Document = new(
            "ExamOverview",
            FileType.Document,
            PdfSharp.PageOrientation.Portrait,
            new()
            {
                A4PortraitSettings = new(),
                A4LandscapeSettings = new()
            },
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

        Exam = exam;
        Grades = grades;
    }

    protected override void FillItems()
    {
        var info = Exam.GetExamInfo(Grades);

        #region header
        /// add header 'ExamOverview' on top of document
        /// first dynamic item of document
        var headerItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 0);

        headerItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "ExamOverview",
            DistanceLeft = 100,
            DistanceTop = 0,
            MaxWidth = 200,
            FontIndex = 1,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });

        Items.Add(headerItem);
        #endregion

        #region info
        /// add info box about exam
        /// distance to header is 20 pt
        var infoItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 20);

        var infoh1 = 10; var infoh2 = 80; var infoh3 = 200; var infoh4 = 270;
        var nextv = 0;

        /// add description and date
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Description:",
            DistanceLeft = infoh1,
            DistanceTop = nextv,
            MaxWidth = 50,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = $"{Exam.Description}",
            DistanceLeft = infoh2,
            DistanceTop = nextv,
            MaxWidth = 110,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Date:",
            DistanceLeft = infoh3,
            DistanceTop = nextv,
            MaxWidth = 50,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = $"{Exam.Date}",
            DistanceLeft = infoh4,
            DistanceTop = nextv,
            MaxWidth = 110,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });

        /// calculate height of description and date
        nextv += Extensions.RoundToNextTen(
        [
            new TextMeasurements(
                Document.GetCurrentXGraphics(),
                $"{Exam.Description}",
                Information.Fonts[0],
                new(
                    new XPoint(0, 0),
                    new XPoint(110, 800)
            )).MeasureText(),
            new TextMeasurements(
                Document.GetCurrentXGraphics(),
                $"{Exam.Date}",
                Information.Fonts[0],
                new(
                    new XPoint(0, 0),
                    new XPoint(110, 800)
            )).MeasureText(),
        ]);
        /// add another 10 pt as distance
        nextv += 10;

        /// add subject and calculate average grade
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Subject:",
            DistanceLeft = infoh1,
            DistanceTop = nextv,
            MaxWidth = 50,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = $"{Exam.Subject}",
            DistanceLeft = infoh2,
            DistanceTop = nextv,
            MaxWidth = 110,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Average:",
            DistanceLeft = infoh3,
            DistanceTop = nextv,
            MaxWidth = 50,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = $"{Math.Round(info.AverageGrade, 2)}",
            DistanceLeft = infoh4,
            DistanceTop = nextv,
            MaxWidth = 110,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });

        /// calculate height of subject and average grade
        nextv += Extensions.RoundToNextTen(
        [
            new TextMeasurements(
                Document.GetCurrentXGraphics(),
                $"{Exam.Subject}",
                Information.Fonts[0],
                new(
                    new XPoint(0, 0),
                    new XPoint(110, 800)
            )).MeasureText(),
            new TextMeasurements(
                Document.GetCurrentXGraphics(),
                $"{Math.Round(info.AverageGrade, 2)}",
                Information.Fonts[0],
                new(
                    new XPoint(0, 0),
                    new XPoint(110, 800)
            )).MeasureText(),
        ]);
        /// add another 10 pt as distance
        nextv += 10;

        /// add total points
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "TotalPoints:",
            DistanceLeft = infoh1,
            DistanceTop = nextv,
            MaxWidth = 50,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = $"{Exam.TotalPoints}",
            DistanceLeft = infoh2,
            DistanceTop = nextv,
            MaxWidth = 110,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });

        Items.Add(infoItem);
        #endregion

        #region overview
        /// add overview of exam as table
        /// distance to info box is 50 pt
        var overviewItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 50);
        /// set table width of cells
        var overviewh1 = 10; var overviewh2 = 160; var overviewh3 = 310; var overviewh4 = 460;

        /// add table header | cell values and cell borders
        overviewItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Pupil",
            DistanceLeft = overviewh1 + 10,
            DistanceTop = 0,
            MaxWidth = 130,
            FontIndex = 2,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        overviewItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Points",
            DistanceLeft = overviewh2 + 10,
            DistanceTop = 0,
            MaxWidth = 130,
            FontIndex = 2,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        overviewItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Grade",
            DistanceLeft = overviewh3 + 10,
            DistanceTop = 0,
            MaxWidth = 130,
            FontIndex = 2,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        overviewItem.AddItem(GeometryHelper.Line(
            ItemMode.Static,
            [
                new Point() { VerticalPosition = 20, HorizontalPosition = 0 },
                new Point() { VerticalPosition = 20, HorizontalPosition = overviewh4 }
            ],
            0,
            0
        ));
        overviewItem.AddItem(GeometryHelper.Line(
            ItemMode.Static,
            [
                new Point() { VerticalPosition = 0, HorizontalPosition = overviewh2 },
                new Point() { VerticalPosition = 20, HorizontalPosition = overviewh2 }
            ],
            0,
            0
        ));
        overviewItem.AddItem(GeometryHelper.Line(
            ItemMode.Static,
            [
                new Point() { VerticalPosition = 0, HorizontalPosition = overviewh3 },
                new Point() { VerticalPosition = 20, HorizontalPosition = overviewh3 }
            ],
            0,
            0
        ));
        Items.Add(overviewItem);

        foreach (var grade in Grades)
        {
            /// add table row
            var gradeItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 0);

            /// calculate height of each cell
            nextv = Extensions.RoundToNextTen(
            [
                new TextMeasurements(
                    Document.GetCurrentXGraphics(),
                    $"{grade.Pupil}",
                    Information.Fonts[2],
                    new(
                        new XPoint(0, 0),
                        new XPoint(130, 800)
                )).MeasureText(),
                new TextMeasurements(
                    Document.GetCurrentXGraphics(),
                    $"{grade.Points}",
                    Information.Fonts[2],
                    new(
                        new XPoint(0, 0),
                        new XPoint(130, 800)
                )).MeasureText(),
                new TextMeasurements(
                    Document.GetCurrentXGraphics(),
                    $"{Exam.GetGrade(grade)}",
                    Information.Fonts[2],
                    new(
                        new XPoint(0, 0),
                        new XPoint(130, 800)
                )).MeasureText()
            ]);
            /// add another 10 pt as distance
            nextv += 10;

            /// add table cell values
            gradeItem.AddItem(new StringItem()
            {
                Mode = ItemMode.Static,
                Content = $"{grade.Pupil}",
                DistanceLeft = overviewh1 + 10,
                DistanceTop = 5,
                MaxWidth = 130,
                FontIndex = 2,
                BrushIndex = 0,
                PenIndex = 0,
                Underline = false
            });
            gradeItem.AddItem(new StringItem()
            {
                Mode = ItemMode.Static,
                Content = $"{grade.Points}",
                DistanceLeft = overviewh2 + 10,
                DistanceTop = 5,
                MaxWidth = 130,
                FontIndex = 2,
                BrushIndex = 0,
                PenIndex = 0,
                Underline = false
            });
            gradeItem.AddItem(new StringItem()
            {
                Mode = ItemMode.Static,
                Content = $"{Exam.GetGrade(grade)}",
                DistanceLeft = overviewh3 + 10,
                DistanceTop = 5,
                MaxWidth = 130,
                FontIndex = 2,
                BrushIndex = 0,
                PenIndex = 0,
                Underline = false
            });
            /// add layout of table | cell borders
            gradeItem.AddItem(GeometryHelper.Line(
                ItemMode.Static,
                [
                    new Point() { VerticalPosition = nextv, HorizontalPosition = 0 },
                    new Point() { VerticalPosition = nextv, HorizontalPosition = overviewh4 }
                ],
                0,
                0
            ));
            gradeItem.AddItem(GeometryHelper.Line(
                ItemMode.Static,
                [
                    new Point() { VerticalPosition = 0, HorizontalPosition = overviewh2 },
                    new Point() { VerticalPosition = nextv, HorizontalPosition = overviewh2 }
                ],
                0,
                0
            ));
            gradeItem.AddItem(GeometryHelper.Line(
                ItemMode.Static,
                [
                    new Point() { VerticalPosition = 0, HorizontalPosition = overviewh3 },
                    new Point() { VerticalPosition = nextv, HorizontalPosition = overviewh3 }
                ],
                0,
                0
            ));

            Items.Add(gradeItem);
        }
        #endregion

        #region graph
        /// add grade distribution as bar graph
        /// distance to overview table is 50 pt
        var graphItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 50);
        /// get bar graph
        var data = GraphWriter.Write(new TGradeDistribution(0, 0, 200, 450, Exam, Grades));

        if (data is not null)
        {
            /// add bar graph | use same width and height values as calculated bar graph [200|450]
            graphItem.AddItem(new GraphItem()
            {
                Mode = ItemMode.Static,
                Data = data,
                DistanceLeft = 0,
                DistanceTop = 0,
                Height = 200,
                Width = 450
            });
        }
        Items.Add(graphItem);
        #endregion

        #region distribution
        /// add grade distribution as table
        /// distance to bar graph is 50 pt
        var distributionItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 50);
        /// set width of cells
        var distributionh1 = 10; var distributionh2 = 60; var distributionh3 = 410; var distributionh4 = 460;
        /// set distance to top of cell | and height of table head
        var distributionv1 = 5; var distributionv2 = 20;

        /// add table head and cell borders
        distributionItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Grade",
            DistanceLeft = distributionh1 + 10,
            DistanceTop = distributionv1,
            MaxWidth = 100,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        distributionItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = $"Pupils",
            DistanceLeft = distributionh2 + 10,
            DistanceTop = distributionv1,
            MaxWidth = 330,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        distributionItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = $"Amount",
            DistanceLeft = distributionh3 + 10,
            DistanceTop = distributionv1,
            MaxWidth = 100,
            FontIndex = 0,
            BrushIndex = 0,
            PenIndex = 0,
            Underline = false
        });
        distributionItem.AddItem(GeometryHelper.Line(
            ItemMode.Static,
            [
                new Point() { VerticalPosition = distributionv2, HorizontalPosition = distributionh1 },
                new Point() { VerticalPosition = distributionv2, HorizontalPosition = distributionh4 }
            ],
            0,
            0
        ));
        distributionItem.AddItem(GeometryHelper.Line(
            ItemMode.Static,
            [
                new Point() { VerticalPosition = 0, HorizontalPosition = distributionh2 },
                new Point() { VerticalPosition = distributionv2, HorizontalPosition = distributionh2 }
            ],
            0,
            0
        ));
        distributionItem.AddItem(GeometryHelper.Line(
            ItemMode.Static,
            [
                new Point() { VerticalPosition = 0, HorizontalPosition = distributionh3 },
                new Point() { VerticalPosition = distributionv2, HorizontalPosition = distributionh3 }
            ],
            0,
            0
        ));
        Items.Add(distributionItem);

        for (var i = 0; i < info.Distributions.Length; i++)
        {
            /// add table row for each grade value
            var distributionItemI = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 0);

            /// calculate height of pupil-string cell
            nextv = Extensions.RoundToNextTen(
            [
                new TextMeasurements(
                    Document.GetCurrentXGraphics(),
                    $"{info.Distributions[i].PupilString}",
                    Information.Fonts[0],
                    new(
                        new XPoint(0, 0),
                        new XPoint(330, 800)
                )).MeasureText()
            ]);
            /// add another 10 pt min distance
            nextv += 10;

            /// add cell values for this row
            distributionItemI.AddItem(new StringItem()
            {
                Mode = ItemMode.Static,
                Content = $"{i + 1}",
                DistanceLeft = distributionh1 + 25,
                DistanceTop = distributionv1,
                MaxWidth = 100,
                FontIndex = 0,
                BrushIndex = 0,
                PenIndex = 0,
                Underline = false
            });
            distributionItemI.AddItem(new StringItem()
            {
                Mode = ItemMode.Static,
                Content = $"{info.Distributions[i].PupilString}",
                DistanceLeft = distributionh2 + 10,
                DistanceTop = distributionv1,
                MaxWidth = 330,
                FontIndex = 0,
                BrushIndex = 0,
                PenIndex = 0,
                Underline = false
            });
            distributionItemI.AddItem(new StringItem()
            {
                Mode = ItemMode.Static,
                Content = $"{info.Distributions[i].Amount} x",
                DistanceLeft = distributionh3 + 25,
                DistanceTop = distributionv1,
                MaxWidth = 100,
                FontIndex = 0,
                BrushIndex = 0,
                PenIndex = 0,
                Underline = false
            });

            /// add layout of row | cell borders
            distributionItemI.AddItem(GeometryHelper.Line(
                ItemMode.Static,
                [
                    new Point() { VerticalPosition = nextv, HorizontalPosition = distributionh1 },
                    new Point() { VerticalPosition = nextv, HorizontalPosition = distributionh4 }
                ],
                0,
                0
            ));
            distributionItemI.AddItem(GeometryHelper.Line(
                ItemMode.Static,
                [
                    new Point() { VerticalPosition = 0, HorizontalPosition = distributionh2 },
                    new Point() { VerticalPosition = nextv, HorizontalPosition = distributionh2 }
                ],
                0,
                0
            ));
            distributionItemI.AddItem(GeometryHelper.Line(
                ItemMode.Static,
                [
                    new Point() { VerticalPosition = 0, HorizontalPosition = distributionh3 },
                    new Point() { VerticalPosition = nextv, HorizontalPosition = distributionh3 }
                ],
                0,
                0
            ));

            Items.Add(distributionItemI);
        }
        #endregion
    }
}