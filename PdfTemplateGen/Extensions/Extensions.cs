namespace PdfTemplateGen;

public static class Extensions
{
    public static int RoundToNextTen(double[] numbers)
    {
        var max = FindMax(numbers);
        return (int)(Math.Round(max / 10.0) * 10.0);
    }

    private static double FindMax(double[] numbers)
    {
        double max = double.MinValue;
        foreach (var number in numbers) max = double.Max(max, number);
        return max;
    }
}