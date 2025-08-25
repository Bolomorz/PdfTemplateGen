// See https://aka.ms/new-console-template for more information
using PdfTemplateGenExample.ExamOverview;

PdfSharp.Fonts.GlobalFontSettings.FontResolver = new PdfSharp.Snippets.Font.FailsafeFontResolver();

ExamOverviewExample.Print();
