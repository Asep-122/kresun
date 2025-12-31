using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
public partial class SD_Report_ReportPembayaran : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessNamaProfil
    {
        get { return Session["SessNamaProfil"].ToString(); }
        set { ViewState["SessNamaProfil"] = value; }
    }
    private string SessAlamatProfil1
    {
        get { return Session["SessAlamatProfil1"].ToString(); }
        set { ViewState["SessAlamatProfil1"] = value; }
    }
    private string SessAlamatProfil2
    {
        get { return Session["SessAlamatProfil2"].ToString(); }
        set { ViewState["SessAlamatProfil2"] = value; }
    }
    private string SessKotaProfil
    {
        get { return Session["SessKotaProfil"].ToString(); }
        set { ViewState["SessKotaProfil"] = value; }
    }

    private string SessNegaraProfil
    {
        get { return Session["SessNegaraProfil"].ToString(); }
        set { ViewState["SessNegaraProfil"] = value; }
    }

    private string SessTeleponProfil
    {
        get { return Session["SessTeleponProfil"].ToString(); }
        set { ViewState["SessTeleponProfil"] = value; }
    }
    private string SessFaxProfil
    {
        get { return Session["SessFaxProfil"].ToString(); }
        set { ViewState["SessFaxProfil"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            if (!Page.IsPostBack)
            {
                BindEntity();
                StartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                EndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    void BindEntity()
    {
        DefaultClass.Global.SqlHelper[] var22 = new DefaultClass.Global.SqlHelper[2];
        var22[0] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSUMBERDANA", typeof(string));
        var22[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("Report_SumberDana", var22, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank";

        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "NamaBank";
        ddlsumberdana.DataValueField = "KodeBank";
        ddlsumberdana.DataBind();
        ddlsumberdana.Items.Insert(0, new ListItem("Pilih Sumber Dana", "-"));

    }

/*    [WebMethod]
    public static string GetNoRekening(string InputText, string sumberdana )
    {
        List<string[]> listResult = new List<string[]>();

        string no_Bukti = InputText;
        string STATE = "SEARCHNOREKENING";

        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();


        ge.ListDataset = GetData(STATE, InputText,sumberdana).ListDataset;
        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;
    }
*/
    protected static GlobalEntities GetData(string STATE, string sumberdana,DateTime ? StartDate,DateTime ? EndDate)
    {
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
        Params[0] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
        Params[1] = gb.CreateSPParameter("@kode_sumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, sumberdana, typeof(string));
        Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        Params[3] = gb.CreateSPParameter("@StartDate", ParameterDirection.Input, SqlDbType.DateTime, 8000, StartDate, typeof(DateTime));
        Params[4] = gb.CreateSPParameter("@EndDate", ParameterDirection.Input, SqlDbType.DateTime, 8000,EndDate , typeof(DateTime));

        ge.ListDataset = gb.GetDataJustOutPut("Report_SumberDana", Params, "constrmf");

        return ge;
    }

    public void ExportToExcel(DataTable dt)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Data");

            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            context.Response.AddHeader("content-disposition", "attachment;filename=PencairanBank.xlsx");

            using (MemoryStream ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                ms.WriteTo(context.Response.OutputStream);
                context.Response.Flush();
                context.Response.End();
            }
        }
    }

    public void ExportToTxt(DataTable dt)
    {
        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        context.Response.Buffer = true;
        context.Response.ContentType = "text/plain";
        context.Response.AddHeader("content-disposition", "attachment;filename=PencairanBank.txt");

        using (StringWriter sw = new StringWriter())
        {


            // --- tulis data ---
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(row[i].ToString());
                    if (i < dt.Columns.Count - 1) sw.Write("|");
                }
                sw.Write(sw.NewLine);
            }

            context.Response.Write(sw.ToString());
            context.Response.Flush();
            context.Response.End();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ge.ListDataset = GetData("EXPORT", ddlsumberdana.SelectedValue,Convert.ToDateTime(StartDate.Text),Convert.ToDateTime(EndDate.Text)).ListDataset;
            DataTable table = ge.ListDataset.Tables[0];

            if (ddlFile.SelectedValue == "xlsx")
            {
                if(ddlsumberdana.SelectedValue == "A012")
                {
                    ExportMNC(table);
                    return;
                }

                ExportToExcel(table);
            }
            else
            {
                ExportToTxt(table);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    void ExportMNC(DataTable table)
    {
        try
        {

            XLWorkbook wb = new XLWorkbook();

            string name = "Dafnom Bank MNC Sena";
            var worksheet = wb.Worksheets.Add(name);
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.Margins.Top = 0.4;
            worksheet.PageSetup.Margins.Left = 0.3;
            worksheet.PageSetup.Margins.Right = 0.3;
            worksheet.PageSetup.Margins.Bottom = 0.5;   

            worksheet.PageSetup.Margins.Header = 0.3;
            worksheet.PageSetup.Margins.Footer = 0.3;
            worksheet.RowHeight = 13;


            worksheet.Column(1).Width = 10;
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(5, 1).Style.Font.Bold = true;
            worksheet.Cell(5, 2).Style.Font.Bold = true;
            worksheet.Cell(6, 1).Style.Font.Bold = true;
            worksheet.Cell(6, 2).Style.Font.Bold = true;

            for (int i = 1; i <= 25; i++)
            {
                worksheet.Cell(9, i).Style.Font.Bold = true;
                worksheet.Cell(10, i).Style.Font.Bold = true;
        
                worksheet.Cell(9, i).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(10, i).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(9, i).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(10, i).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(9, i).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(10, i).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(9, i).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(10, i).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(9, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(9, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(10, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(10, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Column(i+1).Width = 25;

            }


            worksheet.Cell(1, 1).Value = "DAFTAR NOMINATIF PENARIKAN PEMBIAYAAN";
            worksheet.Cell(2, 1).Value = "KOPERASI SIMPAN PINJAM SERAMBI DANA DENGAN BANK MNC INTERNASIONAL";

            worksheet.Cell(5, 1).Value = "Batch";
            worksheet.Cell(5, 2).Value = ": 8";
            worksheet.Cell(6, 1).Value = "Bulan";
            worksheet.Cell(6, 2).Value = ": APRIL 2025";

            worksheet.Cell(9, 1).Value = "No";
            worksheet.Range(worksheet.Cell(9, 1), worksheet.Cell(10, 1)).Merge();

            worksheet.Cell(9, 2).Value = "NAMA LENGKAP";
            worksheet.Range(worksheet.Cell(9, 2), worksheet.Cell(10, 2)).Merge();
        
            worksheet.Cell(9, 3).Value = "NO ID";
            worksheet.Range(worksheet.Cell(9, 3), worksheet.Cell(10, 3)).Merge();

            worksheet.Cell(9, 4).Value = "PRODUK";
            worksheet.Range(worksheet.Cell(9, 4), worksheet.Cell(10, 4)).Merge();

            worksheet.Cell(9, 5).Value = "KETERANGAN";
            worksheet.Range(worksheet.Cell(9, 5), worksheet.Cell(10, 5)).Merge();

            worksheet.Cell(9, 6).Value = "NO PK";
            worksheet.Range(worksheet.Cell(9, 6), worksheet.Cell(10, 6)).Merge();

            worksheet.Cell(9, 7).Value = "NO SK";
            worksheet.Range(worksheet.Cell(9, 7), worksheet.Cell(10, 7)).Merge();

    
            worksheet.Cell(9, 8).Value = "USIA";
            worksheet.Range(worksheet.Cell(9, 8), worksheet.Cell(9, 9)).Merge();
            worksheet.Cell(10, 8).Value = "SAAT INI";
            worksheet.Cell(10, 9).Value = "JT TEMPO";

            worksheet.Cell(9, 10).Value = "JT Tempo";
            worksheet.Range(worksheet.Cell(9, 10), worksheet.Cell(10, 10)).Merge();

            worksheet.Cell(9, 11).Value = "PENGAJUAN";
            worksheet.Range(worksheet.Cell(9, 11), worksheet.Cell(9, 13)).Merge();
            worksheet.Cell(10, 11).Value = "PLAFOND";
            worksheet.Cell(10, 12).Value = "TENOR (BULAN)";
            worksheet.Cell(10, 13).Value = "SUKU BUNGA";

            worksheet.Cell(9, 14).Value = "NO";
            worksheet.Range(worksheet.Cell(9, 14), worksheet.Cell(10, 14)).Merge();

            worksheet.Cell(9, 15).Value = "NAMA LENGKAP";
            worksheet.Range(worksheet.Cell(9, 15), worksheet.Cell(10, 15)).Merge();            
        
            worksheet.Cell(9, 16).Value = "BIAYA PROVISI (1.0%)";
            worksheet.Range(worksheet.Cell(9, 16), worksheet.Cell(10, 16)).Merge();

            worksheet.Cell(9, 17).Value = "POTONGAN BIAYA KOPERASI";
            worksheet.Range(worksheet.Cell(9, 17), worksheet.Cell(9, 21)).Merge();
            worksheet.Cell(10, 17).Value = "NOMINAL POTONGAN";
            worksheet.Cell(10, 18).Value = "NOMINAL TALANGAN";
            worksheet.Cell(10, 19).Value = "TOTAL POTONGAN KOPERASI";
            worksheet.Cell(10, 20).Value = "GIRO";
            worksheet.Cell(10, 21).Value = "ATAS NAMA";

            worksheet.Cell(9, 22).Value = "TOTAL POTONGAN BIAYA";
            worksheet.Range(worksheet.Cell(9, 22), worksheet.Cell(10, 22)).Merge();            
        
            worksheet.Cell(9, 23).Value = "BLOKIR 2X ANGSURAN";
            worksheet.Range(worksheet.Cell(9, 23), worksheet.Cell(10, 23)).Merge();

            worksheet.Cell(9, 24).Value = "PENCAIRAN END USER";
            worksheet.Range(worksheet.Cell(9, 24), worksheet.Cell(9, 25)).Merge();
            worksheet.Cell(10, 24).Value = "Nominal";
            worksheet.Cell(10, 25).Value = "No Rekening Motion Bank";

            int row = 11;
            int endRow = row + table.Rows.Count - 1;
            decimal plafond = 0;
            decimal biayaProvisi = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {

                worksheet.Cell(i + row, 1).Value = i+1;
                worksheet.Cell(i + row, 2).Value = table.Rows[i]["Nama_Lengkap"];
                worksheet.Cell(i + row, 3).Value = table.Rows[i]["No_Id"];
                worksheet.Cell(i + row, 4).Value = table.Rows[i]["produk"];
                worksheet.Cell(i + row, 5).Value = "";
                worksheet.Cell(i + row, 6).Value = table.Rows[i]["NoPk"];
                worksheet.Cell(i + row, 7).Value = table.Rows[i]["NoSk"];
                worksheet.Cell(i + row, 8).Value = table.Rows[i]["Usia"];
                worksheet.Cell(i + row, 9).Value = table.Rows[i]["JtTempo"];
                worksheet.Cell(i + row, 10).Value = table.Rows[i]["TglJthTempo"];
                worksheet.Cell(i + row, 11).Value = table.Rows[i]["Plafond"];
                worksheet.Cell(i + row, 12).Value = table.Rows[i]["Tenor"];
                worksheet.Cell(i + row, 13).Value = table.Rows[i]["SukuBunga"];
                worksheet.Cell(i + row, 14).Value = table.Rows[i]["No"];
                worksheet.Cell(i + row, 15).Value = table.Rows[i]["NamaLengkap"];
                worksheet.Cell(i + row, 16).Value = table.Rows[i]["BiayaLengkap"];
                worksheet.Cell(i + row, 17).Value = table.Rows[i]["NominalPotongan"];
                worksheet.Cell(i + row, 18).Value = table.Rows[i]["NominalTalangan"];
                worksheet.Cell(i + row, 19).Value = table.Rows[i]["TotalPotonganKoperasi"];
                worksheet.Cell(i + row, 20).Value = table.Rows[i]["Giro"];
                worksheet.Cell(i + row, 21).Value = table.Rows[i]["AtasNama"];
                worksheet.Cell(i + row, 22).Value = table.Rows[i]["TotalPotonganBiaya"];
                worksheet.Cell(i + row, 23).Value = table.Rows[i]["BiayaAngsuran"];
                worksheet.Cell(i + row, 24).Value = table.Rows[i]["Nominal"];
                worksheet.Cell(i + row, 25).Value = table.Rows[i]["NoRekeningBank"];

                plafond += Convert.ToDecimal(table.Rows[i]["Plafond"]);
                biayaProvisi += Convert.ToDecimal(table.Rows[i]["BiayaLengkap"]);

            }
            int DetailRow = endRow + 1;

            worksheet.Cell(DetailRow, 11).Style.Font.Bold = true;
            worksheet.Cell(DetailRow, 11).Value = plafond.ToString();

            worksheet.Cell(DetailRow, 16).Style.Font.Bold = true;
            worksheet.Cell(DetailRow, 16).Value = biayaProvisi.ToString();

            worksheet.Cell(DetailRow, 17).Style.Font.Bold = true;
            worksheet.Cell(DetailRow, 17).Value = biayaProvisi.ToString();

            worksheet.Cell(DetailRow, 18).Style.Font.Bold = true;
            worksheet.Cell(DetailRow, 18).Value = biayaProvisi.ToString();

            worksheet.Cell(DetailRow, 19).Style.Font.Bold = true;
            worksheet.Cell(DetailRow, 19).Value = biayaProvisi.ToString();

            worksheet.Cell(DetailRow, 22).Style.Font.Bold = true;
            worksheet.Cell(DetailRow, 22).Value = biayaProvisi.ToString();

            worksheet.Cell(DetailRow, 23).Style.Font.Bold = true;
            worksheet.Cell(DetailRow, 23).Value = biayaProvisi.ToString();

            worksheet.Cell(DetailRow + 2, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 2, 2).Value = "KSP SERAMBI DANA";    

            worksheet.Cell(DetailRow + 7, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 7, 2).Value = "(LUCKY NORMANSYAH)";

            worksheet.Cell(DetailRow + 8, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 8, 2).Value = "DIREKTUR UTAMA";


            var dataRange = worksheet.Range(row, 1, endRow + 1, 25);

            dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + name + ".xlsx");

            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                stream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
}