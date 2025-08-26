using PdfTemplateGen.DocumentTemplates;

namespace PdfTemplateGenExample.ExamOverview;

/// <summary>
/// <para>exam overview</para>
/// 
/// <para>for this exam overview, independent (independent from PdfTemplateGen) types ('ExamObjects.cs') have been created to simulate an exam with grades</para>
/// 
/// <para>the template defined in 'TExamOverview.cs' takes objects of those independent types as parameters</para>
/// 
/// <para>in the print method, objects of those types are dynamically created to showcase:</para>
/// <para>-   the types themself are independent from the generator</para>
/// <para>-   the objects themself can be dynamic</para>
/// <para>-   the template can be defined to take any independent type and display its data dynamically</para>
/// <para>-   the created file for this example is 'ExamOverviewExample.pdf'</para>
/// </summary>
internal static class ExamOverviewExample
{
    internal static void Print()
    {
        var exam = new Exam()
        {
            Description = "Reading 1",
            Date = DateTime.Now,
            Subject = "English",
            TotalPoints = 100
        };
        var i = 0;
        List<Grade> grades = [
            new(){ Pupil = $"Pupil{i++}", Points = 66},
            new(){ Pupil = $"Pupil{i++}", Points = 55},
            new(){ Pupil = $"Pupil{i++}", Points = 43},
            new(){ Pupil = $"Pupil{i++}", Points = 77},
            new(){ Pupil = $"Pupil{i++}", Points = 95},
            new(){ Pupil = $"Pupil{i++}", Points = 88},
            new(){ Pupil = $"Pupil{i++}", Points = 99},
            new(){ Pupil = $"Pupil{i++}", Points = 56},
            new(){ Pupil = $"Pupil{i++}", Points = 63},
            new(){ Pupil = $"Pupil{i++}", Points = 68},
            new(){ Pupil = $"Pupil{i++}", Points = 72},
            new(){ Pupil = $"Pupil{i++}", Points = 78},
            new(){ Pupil = $"Pupil{i++}", Points = 73},
            new(){ Pupil = $"Pupil{i++}", Points = 59},
            new(){ Pupil = $"Pupil{i++}", Points = 20},
            new(){ Pupil = $"Pupil{i++}", Points = 56},
            new(){ Pupil = $"Pupil{i++}", Points = 56},
            new(){ Pupil = $"Pupil{i++}", Points = 56},
            new(){ Pupil = $"Pupil{i++}", Points = 56},
            new(){ Pupil = $"Pupil{i++}", Points = 56},
            new(){ Pupil = $"Pupil{i++}", Points = 56},
            new(){ Pupil = $"Pupil{i++}", Points = 56}
        ];

        var file = DocumentWriter.Write(new TExamOverview(exam, grades));

        if (file is not null && file.Success)
        {
            Console.WriteLine("Success");
            file.OpenDocument(null);
        }
        else
        {
            Console.WriteLine("Fail");
        }
    }
}