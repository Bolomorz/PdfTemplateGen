using PdfSharp.Drawing;
using PdfTemplateGen.DocumentTemplates;
using PdfTemplateGen.GraphTemplates;
using PdfTemplateGen;

namespace PdfTemplateGenExample.ExamOverview;

internal class TExamOverview : DocumentTemplate
{
    protected override DocumentItem Document { get; set; }
    protected override DocumentInformation Information { get; set; }
    protected override List<TemplateItemCollection> Items { get; set; }

    private Exam Exam;
    private List<Grade> Grades;

    internal TExamOverview(Exam exam, List<Grade> grades)
    {
        Document = new(
            "ExamOverview",
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

        Exam = exam;
        Grades = grades;
    }

    protected override void FillItems()
    {
        var info = Exam.GetExamInfo(Grades);

        #region header
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
        var infoItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 20);

        var infoh1 = 10; var infoh2 = 80; var infoh3 = 200; var infoh4 = 270;
        var nextv = 0;

        infoItem.AddItem(new StringItem()
        {
            Mode = ItemMode.Static,
            Content = "Name:",
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
        nextv += 10;

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
        nextv += 10;

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
        var overviewItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 50);
        var overviewh1 = 10; var overviewh2 = 160; var overviewh3 = 310; var overviewh4 = 460;

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
            var gradeItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 0);

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
            nextv += 10;

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
        var graphItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 50);
        var data = GraphWriter.Write(new TGradeDistribution(0, 0, 200, 450, Exam, Grades));

        if (data is not null)
        {
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
        var distributionItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 50);
        var distributionh1 = 10; var distributionh2 = 60; var distributionh3 = 410; var distributionh4 = 460;
        var distributionv1 = 5; var distributionv2 = 20;

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
            var distributionItemI = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 0);

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
            nextv += 10;

            distributionItemI.AddItem(new StringItem()
            {
                Mode = ItemMode.Static,
                Content = $"{i+1}",
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