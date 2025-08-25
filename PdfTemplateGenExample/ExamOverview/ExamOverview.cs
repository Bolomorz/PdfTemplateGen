using PdfTemplateGen.DocumentTemplates;

namespace PdfTemplateGenExample.ExamOverview;

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