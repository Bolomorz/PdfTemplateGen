using PdfSharp.Drawing;
using PdfTemplateGen.DocumentTemplates;
using PdfTemplateGen.GraphTemplates;

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
        #region header

        #endregion

        #region info

        #endregion

        #region graph
        var graphItem = new TemplateItemCollection(ItemMode.Dynamic, 0, 0, 50);
        var data = GraphWriter.Write(new TGradeDistribution(0, 0, 400, 450, Exam, Grades));

        if (data is not null)
        {
            graphItem.AddItem(new GraphItem()
            {
                Mode = ItemMode.Static,
                Data = data,
                DistanceLeft = 0,
                DistanceTop = 0,
                Height = 400,
                Width = 450
            });
        }
        Items.Add(graphItem);
        #endregion

        #region table

        #endregion
    }
}