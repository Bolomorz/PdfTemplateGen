using System.Dynamic;

namespace PdfTemplateGen.DocumentTemplates;

public class DocumentSettings
{
    public class A4Portrait
    {
        public const int Height = 842;
        public const int Width = 595;
        public double VerticalStart { get; set; } = 60;
        public double VerticalEnd { get; set; } = 782;
        public double HorizontalStart { get; set; } = 60;
        public double HorizontalEnd { get; set; } = 535;
    }
    public required A4Portrait A4PortraitSettings { get; set; }

    public class A4Landscape
    {
        public const int Height = 595;
        public const int Width = 842;
        public double VerticalStart { get; set; } = 60;
        public double VerticalEnd { get; set; } = 535;
        public double HorizontalStart { get; set; } = 60;
        public double HorizontalEnd { get; set; } = 782;
    }
    public required A4Landscape A4LandscapeSettings { get; set; }
}