namespace PdfTemplateGen.DocumentTemplates;

public static class DocumentWriter
{
    public static PdfFile Write(DocumentTemplate template) => template.Write();
}