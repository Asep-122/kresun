using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PdfHelper
/// </summary>
public class PdfHelper
{
    public PdfHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void CompressPdf(string inputPath, string outputPath)
    {
        using (var reader = new PdfReader(inputPath))
        {
            reader.RemoveUnusedObjects();

            using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                using (var document = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(1)))
                {
                    PdfCopy copy = new PdfCopy(document, fs);
                    copy.SetFullCompression();
                    document.Open();

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = copy.GetImportedPage(reader, i);
                        copy.AddPage(page);
                    }

                    document.Close();
                }
            }
        }
    }
}