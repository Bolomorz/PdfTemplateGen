namespace PdfTemplateGen.GraphTemplates;

public static class GraphWriter
{
    public static TGraphData Write(GraphTemplate template) => template.FillItems();
}