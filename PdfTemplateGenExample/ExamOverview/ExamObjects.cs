namespace PdfTemplateGenExample.ExamOverview;

internal class Exam
{
    internal required string Description { get; set; }
    internal required DateTime Date { get; set; }
    internal required string Subject { get; set; }
    internal required double TotalPoints { get; set; }

    internal int GetGrade(Grade grade)
    {
        var percent = (grade.Points / TotalPoints) * 100;
        if (percent < 25) return 6;
        else if (percent < 50) return 5;
        else if (percent < 70) return 4;
        else if (percent < 81) return 3;
        else if (percent < 91) return 2;
        else return 1;
    }

    internal ExamInfo GetExamInfo(List<Grade> grades)
    {
        var info = new ExamInfo();

        var sum = 0;

        foreach (var grade in grades)
        {
            switch (GetGrade(grade))
            {
                case 1: info.Distributions[0].Amount++; info.AmountTotal++; sum += 1; info.Distributions[0].PupilString += $" {grade.Pupil} |"; break;
                case 2: info.Distributions[1].Amount++; info.AmountTotal++; sum += 2; info.Distributions[1].PupilString  += $" {grade.Pupil} |"; break;
                case 3: info.Distributions[2].Amount++; info.AmountTotal++; sum += 3; info.Distributions[2].PupilString  += $" {grade.Pupil} |"; break;
                case 4: info.Distributions[3].Amount++; info.AmountTotal++; sum += 4; info.Distributions[3].PupilString  += $" {grade.Pupil} |"; break;
                case 5: info.Distributions[4].Amount++; info.AmountTotal++; sum += 5; info.Distributions[4].PupilString  += $" {grade.Pupil} |"; break;
                case 6: info.Distributions[5].Amount++; info.AmountTotal++; sum += 6; info.Distributions[5].PupilString  += $" {grade.Pupil} |"; break;
            }
        }

        info.AverageGrade = sum / info.AmountTotal;

        return info;
    }
}

internal class Grade
{
    internal required string Pupil { get; set; }
    internal required double Points { get; set; }
}

internal class Distribution
{
    internal double Amount = 0;
    internal string PupilString = "|";
}

internal class ExamInfo
{
    internal double AmountTotal = 0;
    internal double AverageGrade = 0;

    internal Distribution[] Distributions = [new(), new(), new(), new(), new(), new()];
}