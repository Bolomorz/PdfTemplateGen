namespace PdfTemplateGen.DocumentTemplates;

public abstract class DocumentTemplate
{
    protected abstract DocumentItem Document { get; set; }
    protected abstract DocumentInformation Information { get; set; }
    protected abstract List<TemplateItemCollection> Items { get; set; }
    protected abstract void FillItems();
    internal PdfFile Write()
    {
        FillItems();
        foreach(var item in Items.Where(x => x.IsStatic())) item.DrawCollection(Document, Information);
        foreach(var item in Items.Where(x => x.IsDynamic()))
            if(item.DrawCollection(Document, Information)) 
                foreach(var repeat in Items.Where(x => x.IsStaticRepeat())) repeat.DrawCollection(Document, Information);
        var file = Document.GetFile();
        return file is not null ? new PdfFile(){ Success = true, Data = file } : new PdfFile(){ Success = false };
    }
}