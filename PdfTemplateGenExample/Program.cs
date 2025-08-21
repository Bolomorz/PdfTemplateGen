// See https://aka.ms/new-console-template for more information
using PdfTemplateGenExample.ExamOverview;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;

GlobalFontSettings.FontResolver = new FailsafeFontResolver();

ExamOverviewExample.Print();
