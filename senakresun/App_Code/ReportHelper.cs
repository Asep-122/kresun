using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public static class ReportHelper
{
    public static void ExportPdfInline(ReportDocument report, string exportFileName, HttpResponse response)
    {
        using (var stream = report.ExportToStream(ExportFormatType.PortableDocFormat))
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("content-disposition", "inline;filename=" + exportFileName + ".pdf");
            response.BinaryWrite(buffer);
            response.Flush();

            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
