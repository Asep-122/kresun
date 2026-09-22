using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Globalization;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Runtime.InteropServices.ComTypes;
using System.IO.Compression;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Runtime.InteropServices;
using System.IdentityModel.Tokens;
using CrystalDecisions.ReportAppServer.DataDefModel;



public partial class SD_REPORT_FORM_SuratRekapDropping : System.Web.UI.Page
{
    String nosebelumnya = "", updateno = "", CRno_rekening = "", STATE = "", kdpersh = "", format_no_pk = "",
             formatnumber = "", formatpknumber = "", formatnumberbukti = "", nopksebelumnya = "", CRno_pk = "", updatepkno = "", pokok = "", bunga = "",
             typetrn = "", keterangantrn = "", no_bukti_sebelumnya = "", NoBuktiNew = "", DebitGL = "", KreditGL1 = "", KreditGL2 = "",
             KodeJurnal = "", KreditGL3 = "", KreditGL4 = "", kodebank = "",lampiran = "";
    double ttlbunga = 0, sisapk = 0, ttlpokok = 0, pokokbunga = 0, ttlkeluarcl = 0;
    int counter_no_rekening = 0, i = 0, x = 0;

    long no_pk_sekarang = 0, no_pk = 0, nosekarang = 0, no_rekening = 0, no_bukti_sekarang = 0, no_bukti = 0;

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gbx = new DefaultClass.Global();
    GlobalEntities gex = new GlobalEntities();
    DataTable dtBayar = new DataTable("Transaction");

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    double angsuranperbulan = 0;
    string nonasabah = "", agama = "", usia = "", namafileJaminan = "", fotopathJaminan = "";
    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";

    string fotopathKTP1 = "",
       fotopathKTP2 = "", fotopathKTP3 = "", fotopathKTP4 = "", fotopathKTP5 = "";

    public string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ddlsumberdana_SelectedIndexChanged(sender, e);
            BindGridEntity();
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";

            //txttglrealisasi.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            //get tanggal server

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[1];
            varT[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarangXYZ", varT, "constrmf");

        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

      

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SuratRekapDropping.aspx");
        //ddlcabang.Focus();
    }
    protected string btnafterDelete()
    {

        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var22 = new DefaultClass.Global.SqlHelper[2];
        var22[0] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSUMBERDANA", typeof(string));
        var22[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SuratRekapDropping", var22, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank";

        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "NamaBank";
        ddlsumberdana.DataValueField = "KodeBank";
        ddlsumberdana.DataBind();
        ddlsumberdana.Items.Insert(0, new ListItem("Pilih Sumber Dana", "-"));



    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            dtBayar.Columns.Clear();

            dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("kodecabang", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("namacabang", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("norekening", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("nonasabah", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("namanasabah", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("jangkawaktu", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("adminrp", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("provisirp", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("tglpk", typeof(DateTime)));
            dtBayar.Columns.Add(new DataColumn("jenispinjaman", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("kodesumberdana", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("namasumberdana", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("nopk", typeof(string)));

            dtBayar.Rows.Clear();
            int xi = 0;


            for (int i = 0; i < GRData.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)GRData.Rows[i].FindControl("chkPilih");

                if (chk != null && chk.Checked)
                {
                    DataRow dr1 = dtBayar.NewRow();

                    xi = xi + 1;


                    dr1["No"] = xi;
                    dr1["kodecabang"] = ((Label)GRData.Rows[i].FindControl("lblkodecabang")).Text;
                    dr1["namacabang"] = ((Label)GRData.Rows[i].FindControl("lblnamacabang")).Text;
                    dr1["norekening"] = ((Label)GRData.Rows[i].FindControl("lblnorekeningnasabah")).Text;
                    dr1["nonasabah"] = ((Label)GRData.Rows[i].FindControl("lblnonasabah")).Text;
                    dr1["namanasabah"] = ((Label)GRData.Rows[i].FindControl("lblnamanasabah")).Text;
                    dr1["plafon"] = ((Label)GRData.Rows[i].FindControl("lblplafon")).Text;
                    dr1["jangkawaktu"] = ((Label)GRData.Rows[i].FindControl("lbljangka")).Text;
                    dr1["adminrp"] = ((Label)GRData.Rows[i].FindControl("lbladminrp")).Text;
                    dr1["provisirp"] = ((Label)GRData.Rows[i].FindControl("lblprovisirp")).Text;
                    dr1["tglpk"] = ((Label)GRData.Rows[i].FindControl("lbltanggalpk")).Text;
                    dr1["jenispinjaman"] = ((Label)GRData.Rows[i].FindControl("lbljenispinjaman")).Text;
                    dr1["kodesumberdana"] = ((Label)GRData.Rows[i].FindControl("lblsumberdana")).Text;
                    dr1["namasumberdana"] = ((Label)GRData.Rows[i].FindControl("lblnamasd")).Text;
                    dr1["nopk"] = ((Label)GRData.Rows[i].FindControl("lblnopk")).Text;

                    dtBayar.Rows.Add(dr1);
                }
            }



            ge.ListDataset = GetData("EXPORT", ddlsumberdana.SelectedValue, dtBayar).ListDataset;
            DataTable table = ge.ListDataset.Tables[0];
            DataTable table2 = ge.ListDataset.Tables[1];

            lampiran = (table.Rows[0]["kodesumberdana"].ToString() == "A018" ? "LAMPIRAN SURAT KSPPS SENA PUTRA MANDIRI" : "LAMPIRAN SURAT KSP SERAMBI DANA");

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "Javascript",
                "AlertSuccess('Download Done!','You successfully Download Zip','top-right','success','true');",
                true
            );

            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive zip =
                    new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    ExportDetailToExcel(table, zip);
                    ExportRekapToExcel(table2, zip);
                    ExportRekapTransferPembiayaanToWord(table2, zip);
                }



                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + table.Rows[0]["NoBukti"].ToString() + ".zip");

                // --- TAMBAHKAN COOKIE INI SEBAGAI SIGNAL KE JAVASCRIPT ---
                HttpCookie downloadCookie = new HttpCookie("DownloadComplete", "true");
                downloadCookie.HttpOnly = false; // Harus false agar bisa dibaca JavaScript
                downloadCookie.Path = "/";
                Response.Cookies.Add(downloadCookie);
                // --------------------------------------------------------

                // Kembalikan posisi stream ke 0 sebelum di-array-kan (mencegah file korup)
                zipStream.Position = 0;
                Response.BinaryWrite(zipStream.ToArray());
                Response.End();


            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    void ExportDetailToExcel(DataTable table,ZipArchive zip)
    {
        try
        {

            XLWorkbook wb = new XLWorkbook();

            string name = "Lampiran Detail Dropping";
            var worksheet = wb.Worksheets.Add(name);
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.FitToPages(1, 0);
            worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
            worksheet.Style.Font.FontName = "Tahoma";
            worksheet.Style.Font.FontSize = 10;
            worksheet.Columns().AdjustToContents();

            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 3).Style.Font.Bold = true;
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(2, 3).Style.Font.Bold = true;
            worksheet.Cell(2, 4).Style.Font.Bold = true;
            worksheet.Cell(3, 3).Style.Font.Bold = true;
            worksheet.Cell(3, 4).Style.Font.Bold = true;
            worksheet.Cell(4, 3).Style.Font.Bold = true;
            worksheet.Cell(4, 4).Style.Font.Bold = true;
            worksheet.Cell(5, 1).Style.Font.Bold = true;
            worksheet.Cell(5, 2).Style.Font.Bold = true;

            worksheet.Cell(1, 3).Value = lampiran;
            worksheet.Cell(2, 3).Value = "No";
            worksheet.Cell(2, 4).Value = ": " + table.Rows[0]["NoBukti"].ToString();

            worksheet.Cell(3, 3).Value = "Perihal";
            worksheet.Cell(3, 4).Value = ":   Permohonan Transfer Pencairan Pembiayaan";


            worksheet.Cell(4, 3).Value = "Tanggal";
            worksheet.Cell(4, 4).Value = ": " + DateTime.Now.ToString("dd MMMM yyyy");

            int row = 7;
            int endRow = row + table.Rows.Count - 1;
            decimal plafond = 0;
            decimal biayaadmin = 0;
            decimal biayaProvisi = 0;
            decimal rekeningtabungan = 0;
            decimal totaltransfer = 0;

            int cell = 0;
            if (table.Rows[0]["kodesumberdana"].ToString() == "A005")
            {
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Kantor Pelayanan";
                worksheet.Cell(6, 3).Value = "Nama Debitur";
                worksheet.Cell(6, 4).Value = "No PK";
                worksheet.Cell(6, 5).Value = "Nopen";
                worksheet.Cell(6, 6).Value = "Plafond Kredit";
                worksheet.Cell(6, 7).Value = "Jangka Waktu";
                worksheet.Cell(6, 8).Value = "Biaya Adm Bank";
                worksheet.Cell(6, 9).Value = "Biaya Provisi";
                worksheet.Cell(6, 10).Value = "Tanggal PK";
                worksheet.Cell(6, 11).Value = "Jenis Pinjaman";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["namacabang"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["namanasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["nopk"];
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["nopensiun"];
                    worksheet.Cell(i + row, 6).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 7).Value = table.Rows[i]["jangkawaktu"];
                    worksheet.Cell(i + row, 8).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 9).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 9).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 10).Value = table.Rows[i]["tglpk"];
                    worksheet.Cell(i + row, 11).Value = table.Rows[i]["jenispinjaman"];

                    plafond += decimal.Parse(table.Rows[i]["plafon"].ToString());
                    biayaadmin += decimal.Parse(table.Rows[i]["provisirp"].ToString());
                    biayaProvisi += decimal.Parse(table.Rows[i]["provisirp"].ToString());
                }

                worksheet.Cell(endRow + 1, 8).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 8).Value = biayaadmin;
                worksheet.Cell(endRow + 1, 8 ).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(endRow + 1, 9).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 9).Value = biayaProvisi;
                worksheet.Cell(endRow + 1, 9).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 9).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 9).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                cell = 11;
            }
            else if (table.Rows[0]["kodesumberdana"].ToString() == "A018")
            {
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Kantor Pelayanan";
                worksheet.Cell(6, 3).Value = "Nama Debitur";
                worksheet.Cell(6, 4).Value = "No PK";
                worksheet.Cell(6, 5).Value = "Nopen";
                worksheet.Cell(6, 6).Value = "Plafond Kredit";
                worksheet.Cell(6, 7).Value = "Jangka Waktu";
                worksheet.Cell(6, 8).Value = "Biaya Adm Bank";
                worksheet.Cell(6, 9).Value = "Tanggal PK";
                worksheet.Cell(6, 10).Value = "Jenis Pinjaman";
                worksheet.Cell(6, 11).Value = "Total Transfer";

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["namacabang"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["namanasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["nopk"];
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["nopensiun"];
                    worksheet.Cell(i + row, 6).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 7).Value = table.Rows[i]["jangkawaktu"];
                    worksheet.Cell(i + row, 8).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 9).Value = table.Rows[i]["tglpk"];
                    worksheet.Cell(i + row, 10).Value = table.Rows[i]["jenispinjaman"];
                    worksheet.Cell(i + row, 11).Value = decimal.Parse(table.Rows[i]["plafon"].ToString()) - decimal.Parse(table.Rows[i]["provisirp"].ToString());
                    worksheet.Cell(i + row, 11).Style.NumberFormat.Format = "#,##0";

                    plafond += decimal.Parse(table.Rows[i]["plafon"].ToString());
                    biayaadmin += decimal.Parse(table.Rows[i]["provisirp"].ToString());
                    biayaProvisi += decimal.Parse(table.Rows[i]["provisirp"].ToString());
                    totaltransfer += decimal.Parse(table.Rows[i]["plafon"].ToString()) - decimal.Parse(table.Rows[i]["provisirp"].ToString());

                }

                worksheet.Cell(endRow + 1, 6).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 6).Value = biayaadmin;
                worksheet.Cell(endRow + 1, 6).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(endRow + 1, 8).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 8).Value = biayaProvisi;
                worksheet.Cell(endRow + 1, 8).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; 
                
                worksheet.Cell(endRow + 1, 11).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 11).Value =  totaltransfer;
                worksheet.Cell(endRow + 1, 11).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 11).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 11).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                cell = 11;
            }
            else if (table.Rows[0]["kodesumberdana"].ToString() == "A004")
            {
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Kantor Pelayanan";
                worksheet.Cell(6, 3).Value = "Nama Debitur";
                worksheet.Cell(6, 4).Value = "No PK";
                worksheet.Cell(6, 5).Value = "Nopen";
                worksheet.Cell(6, 6).Value = "Plafond Kredit";
                worksheet.Cell(6, 7).Value = "Jangka Waktu";
                worksheet.Cell(6, 8).Value = "Biaya Adm Bank";
                worksheet.Cell(6, 9).Value = "Biaya Rek Tab Debitur";
                worksheet.Cell(6, 10).Value = "Tanggal PK";
                worksheet.Cell(6, 11).Value = "Jenis Pinjaman";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    rekeningtabungan = 10000;
                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["namacabang"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["namanasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["nopk"];
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["nopensiun"];
                    worksheet.Cell(i + row, 6).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 7).Value = table.Rows[i]["jangkawaktu"];
                    worksheet.Cell(i + row, 8).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 9).Value = rekeningtabungan;
                    worksheet.Cell(i + row, 9).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 10).Value = table.Rows[i]["tglpk"];
                    worksheet.Cell(i + row, 11).Value = table.Rows[i]["jenispinjaman"];

                    plafond += decimal.Parse(table.Rows[i]["plafon"].ToString());
                    biayaadmin += decimal.Parse(table.Rows[i]["provisirp"].ToString());
                    biayaProvisi += decimal.Parse(table.Rows[i]["provisirp"].ToString());

                    worksheet.Cell(endRow + 1, 8).Style.Font.Bold = true;
                    worksheet.Cell(endRow + 1, 8).Value = biayaadmin;
                    worksheet.Cell(endRow + 1, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(endRow + 1, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(endRow + 1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    worksheet.Cell(endRow + 1, 9).Style.Font.Bold = true;
                    worksheet.Cell(endRow + 1, 9).Value = rekeningtabungan;
                    worksheet.Cell(endRow + 1, 9).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(endRow + 1, 9).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(endRow + 1, 9).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                }
                cell = 11;
            }
            else if (table.Rows[0]["kodesumberdana"].ToString() == "A001")
            {
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Kantor Pelayanan";
                worksheet.Cell(6, 3).Value = "Nama Debitur";
                worksheet.Cell(6, 4).Value = "No Akad";
                worksheet.Cell(6, 5).Value = "Nopen";
                worksheet.Cell(6, 6).Value = "Plafond Pembiayaan";
                worksheet.Cell(6, 7).Value = "Jangka Waktu";
                worksheet.Cell(6, 8).Value = "Biaya Adm Bank";
                //               worksheet.Cell(6, 9).Value = "Provisi";
                worksheet.Cell(6, 9).Value = "Tanggal Akad";
                worksheet.Cell(6, 10).Value = "Jenis Pinjaman";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["namacabang"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["namanasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["nopk"];
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["nopensiun"];
                    worksheet.Cell(i + row, 6).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 7).Value = table.Rows[i]["jangkawaktu"];
                    worksheet.Cell(i + row, 8).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 9).Value = table.Rows[i]["tglpk"];
                    worksheet.Cell(i + row, 10).Value = table.Rows[i]["jenispinjaman"];

                    plafond += decimal.Parse(table.Rows[i]["plafon"].ToString());
                    biayaadmin += decimal.Parse(table.Rows[i]["adminrp"].ToString());
                    biayaProvisi += decimal.Parse(table.Rows[i]["provisirp"].ToString());

                    worksheet.Cell(endRow + 1, 8).Style.Font.Bold = true;
                    worksheet.Cell(endRow + 1, 8).Value = biayaProvisi;
                    worksheet.Cell(endRow + 1, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(endRow + 1, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(endRow + 1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                }
                cell = 10;
            }            
            else
            {
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Kantor Pelayanan";
                worksheet.Cell(6, 3).Value = "Nama Debitur";
                worksheet.Cell(6, 4).Value = "No PK";
                worksheet.Cell(6, 5).Value = "Nopen";
                worksheet.Cell(6, 6).Value = "Plafond Kredit";
                worksheet.Cell(6, 7).Value = "Jangka Waktu";
                worksheet.Cell(6, 8).Value = "Biaya Adm Bank";
                //               worksheet.Cell(6, 9).Value = "Provisi";
                worksheet.Cell(6, 9).Value = "Tanggal PK";
                worksheet.Cell(6, 10).Value = "Jenis Pinjaman";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["namacabang"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["namanasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["nopk"];
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["nopensiun"];
                    worksheet.Cell(i + row, 6).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 7).Value = table.Rows[i]["jangkawaktu"];
                    worksheet.Cell(i + row, 8).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 9).Value = table.Rows[i]["tglpk"];
                    worksheet.Cell(i + row, 10).Value = table.Rows[i]["jenispinjaman"];

                    plafond += decimal.Parse(table.Rows[i]["plafon"].ToString());
                    biayaadmin += decimal.Parse(table.Rows[i]["adminrp"].ToString());
                    biayaProvisi += decimal.Parse(table.Rows[i]["provisirp"].ToString());

                    worksheet.Cell(endRow + 1, 8).Style.Font.Bold = true;
                    worksheet.Cell(endRow + 1, 8).Value = biayaProvisi;
                    worksheet.Cell(endRow + 1, 8).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(endRow + 1, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(endRow + 1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(endRow + 1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                }
                cell = 10;
            }

            int DetailRow = endRow + 1;

            worksheet.Cell(endRow + 1, 6).Style.Font.Bold = true;
            worksheet.Cell(endRow + 1, 6).Value = plafond;
            worksheet.Cell(endRow + 1, 6).Style.NumberFormat.Format = "#,##0";
            worksheet.Cell(endRow + 1, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(endRow + 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(endRow + 1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(endRow + 1, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            worksheet.Cell(endRow + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(endRow + 1, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Cell(DetailRow + 2, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 2, 2).Value = "Jakarta," + DateTime.Now.ToString("dd MMMM yyyy");

            worksheet.Cell(DetailRow + 3, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 3, 2).Value = "KSP SERAMBI DANA";

            worksheet.Cell(DetailRow + 8, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 8, 2).Value = "(LUCKY NORMANSYAH)";

            worksheet.Cell(DetailRow + 9, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 9, 2).Value = "DIREKTUR UTAMA";

            for (int i = 1; i <= cell; i++)
            {
                worksheet.Cell(6, i).Style.Font.Bold = true;

                worksheet.Cell(6, i).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(6, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(7, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(7, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Columns().AdjustToContents();
                //                worksheet.Column(i + 1).Width = 25;

            }

            var dataRange = worksheet.Range(row, 1, endRow, cell);

            dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            string namefile = "Detail_" +  table.Rows[0]["NoBukti"].ToString().Replace("/","_") + ".xlsx";

            ZipArchiveEntry entry =
                   zip.CreateEntry(namefile);

            using (Stream entryStream = entry.Open())
            using (MemoryStream ms = new MemoryStream())
            {
                wb.SaveAs(ms);

                ms.Position = 0;

                ms.CopyTo(entryStream);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    void ExportRekapToExcel(DataTable table, ZipArchive zip)
    {
        try
        {

            XLWorkbook wb = new XLWorkbook();

            string name = "Lampiran Rekap Dropping";
            var worksheet = wb.Worksheets.Add(name);
            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            worksheet.PageSetup.FitToPages(1, 0);
            worksheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
            worksheet.Style.Font.FontName = "Tahoma";
            worksheet.Style.Font.FontSize = 10;

            worksheet.Column(1).Width = 10;
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 3).Style.Font.Bold = true;
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(2, 3).Style.Font.Bold = true;
            worksheet.Cell(2, 4).Style.Font.Bold = true;
            worksheet.Cell(3, 3).Style.Font.Bold = true;
            worksheet.Cell(3, 4).Style.Font.Bold = true;
            worksheet.Cell(4, 3).Style.Font.Bold = true;
            worksheet.Cell(4, 4).Style.Font.Bold = true;
            worksheet.Cell(5, 1).Style.Font.Bold = true;
            worksheet.Cell(5, 2).Style.Font.Bold = true;




            worksheet.Cell(1, 3).Value = lampiran;
            worksheet.Cell(2, 3).Value = "No";
            worksheet.Cell(2, 4).Value = ": " + table.Rows[0]["NoBukti"].ToString();

            worksheet.Cell(3, 3).Value = "Perihal";
            worksheet.Cell(3, 4).Value = ":   Permohonan Transfer Pencairan Pembiayaan";


            worksheet.Cell(4, 3).Value = "Tanggal";
            worksheet.Cell(4, 4).Value = ": " + DateTime.Now.ToString("dd MMMM yyyy");




            int row = 7;
            int endRow = row + table.Rows.Count - 1;
            decimal plafond = 0;
            decimal biayaProvisi = 0;
            int cell = 0;
            if (table.Rows[0]["kodesumberdana"].ToString() == "A005")
            {
                cell = 7;
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Jenis Produk";
                worksheet.Cell(6, 3).Value = "Jumlah Debitur";
                worksheet.Cell(6, 4).Value = "Plafond Kredit";
                worksheet.Cell(6, 5).Value = "Biaya Adm Bank";
                worksheet.Cell(6, 6).Value = "Biaya Provisi";
                worksheet.Cell(6, 7).Value = "Total Transfer";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["jenis_produk"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["jumlah_nasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 4).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["adminrp"];
                    worksheet.Cell(i + row, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 6).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 7).Value = table.Rows[i]["total_transfer"];
                    worksheet.Cell(i + row, 7).Style.NumberFormat.Format = "#,##0";
                }
            }
            else if (table.Rows[0]["kodesumberdana"].ToString() == "A004")
            {
                cell = 7;
                decimal total_transfer = 0;
                decimal AdmBank = 0;
                decimal total_nasabah = 0;
                decimal totalrekeningtabungan = 0;
                decimal rekeningtabungan = 10000;

                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Jenis Produk";
                worksheet.Cell(6, 3).Value = "Jumlah Debitur";
                worksheet.Cell(6, 4).Value = "Plafond Kredit";
                worksheet.Cell(6, 5).Value = "Biaya Adm Bank";
                worksheet.Cell(6, 6).Value = "Biaya Buka Tabungan";
                worksheet.Cell(6, 7).Value = "Total Transfer";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["jenis_produk"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["jumlah_nasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 4).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 6).Value = rekeningtabungan * decimal.Parse(table.Rows[i]["jumlah_nasabah"].ToString());
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 7).Value = decimal.Parse(table.Rows[i]["plafon"].ToString()) - decimal.Parse(table.Rows[i]["provisirp"].ToString()) - rekeningtabungan * decimal.Parse(table.Rows[i]["jumlah_nasabah"].ToString());
                    worksheet.Cell(i + row, 7).Style.NumberFormat.Format = "#,##0";

                    total_transfer += decimal.Parse(table.Rows[i]["plafon"].ToString()) - decimal.Parse(table.Rows[i]["provisirp"].ToString()) - rekeningtabungan;
                    totalrekeningtabungan += rekeningtabungan * decimal.Parse(table.Rows[i]["jumlah_nasabah"].ToString());
                    AdmBank += decimal.Parse(table.Rows[i]["provisirp"].ToString());
                    total_nasabah += decimal.Parse(table.Rows[i]["jumlah_nasabah"].ToString());
                    plafond += decimal.Parse(table.Rows[i]["plafon"].ToString());
                }

                worksheet.Cell(endRow + 1, 3).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 3).Value = total_nasabah;
                worksheet.Cell(endRow + 1, 3).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(endRow + 1, 4).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 4).Value = plafond;
                worksheet.Cell(endRow + 1, 4).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 4).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;               
                
                worksheet.Cell(endRow + 1, 5).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 5).Value = AdmBank;
                worksheet.Cell(endRow + 1, 5).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 5).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(endRow + 1, 6).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 6).Value = totalrekeningtabungan;
                worksheet.Cell(endRow + 1, 6).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;               
                
                worksheet.Cell(endRow + 1, 7).Style.Font.Bold = true;
                worksheet.Cell(endRow + 1, 7).Value = total_transfer;
                worksheet.Cell(endRow + 1, 7).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(endRow + 1, 7).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(endRow + 1, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(endRow + 1, 7).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


            }
            else if (table.Rows[0]["kodesumberdana"].ToString() == "A018")
            {
                decimal total_transfer = 0;
                cell = 6;
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Jenis Produk";
                worksheet.Cell(6, 3).Value = "Jumlah Debitur";
                worksheet.Cell(6, 4).Value = "Plafond Kredit";
                worksheet.Cell(6, 5).Value = "Biaya Provisi";
                worksheet.Cell(6, 6).Value = "Total Transfer";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["jenis_produk"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["jumlah_nasabah"];
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 4).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 6).Value =  decimal.Parse(table.Rows[i]["plafon"].ToString()) - decimal.Parse(table.Rows[i]["provisirp"].ToString());
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";
                }
            }            
            else if (table.Rows[0]["kodesumberdana"].ToString() == "A001")
            {
                decimal total_transfer = 0;
                cell = 5;
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Jenis Produk";
                worksheet.Cell(6, 3).Value = "Jumlah Debitur";
                worksheet.Cell(6, 4).Value = "Plafond Pembiayaan";
                worksheet.Cell(6, 5).Value = "Keterangan";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["jenis_produk"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["jumlah_nasabah"];
                    worksheet.Cell(i + row, 3).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 4).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 5).Value = "Rincian Data Nasabah Terlampir";

                }
            }
            else
            {
                cell = 5;
                worksheet.Cell(6, 1).Value = "No";
                worksheet.Cell(6, 2).Value = "Jenis Produk";
                worksheet.Cell(6, 3).Value = "Jumlah Debitur";
                worksheet.Cell(6, 4).Value = "Plafond Kredit";
                worksheet.Cell(6, 5).Value = "Biaya Provisi";
                worksheet.Cell(6, 6).Value = "Total Transfer";

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    worksheet.Cell(i + row, 1).Value = i + 1;
                    worksheet.Cell(i + row, 2).Value = table.Rows[i]["jenis_produk"];
                    worksheet.Cell(i + row, 3).Value = table.Rows[i]["jumlah_nasabah"];
                    worksheet.Cell(i + row, 3).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 4).Value = table.Rows[i]["plafon"];
                    worksheet.Cell(i + row, 4).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 5).Value = table.Rows[i]["provisirp"];
                    worksheet.Cell(i + row, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(i + row, 6).Value = table.Rows[i]["total_transfer"];
                    worksheet.Cell(i + row, 6).Style.NumberFormat.Format = "#,##0";

                }
            }

                int DetailRow = endRow + 1;

            worksheet.Cell(DetailRow + 2, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 2, 2).Value = "Jakarta," + DateTime.Now.ToString("dd MMMM yyyy");

            worksheet.Cell(DetailRow + 3, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 3, 2).Value = "KSP SERAMBI DANA";

            worksheet.Cell(DetailRow + 8, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 8, 2).Value = "(LUCKY NORMANSYAH)";

            worksheet.Cell(DetailRow + 9, 2).Style.Font.Bold = true;
            worksheet.Cell(DetailRow + 9, 2).Value = "DIREKTUR UTAMA";

            for (int i = 1; i <= cell; i++)
            {
                worksheet.Cell(6, i).Style.Font.Bold = true;

                worksheet.Cell(6, i).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, i).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(6, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(6, i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                /*                worksheet.Column(i + 1).Width = 25;*/
                worksheet.Columns().AdjustToContents();
            }

            var dataRange = worksheet.Range(row, 1, endRow, cell);

            dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            string namefile = "Rekap_" + table.Rows[0]["NoBukti"].ToString().Replace("/", "_") + ".xlsx";
            ZipArchiveEntry entry =
                    zip.CreateEntry(namefile);

            using (Stream entryStream = entry.Open())
            using (MemoryStream ms = new MemoryStream())
            {
                wb.SaveAs(ms);

                ms.Position = 0;

                ms.CopyTo(entryStream);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    void ExportRekapTransferPembiayaanToWord(DataTable table, ZipArchive zip)
    {
        try
        {
            ReportDocument rprt = new ReportDocument();

            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
            Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, table.Rows[0]["NoBukti"].ToString(), typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTPERMOHONANTRANSFERPEMBIAYAAN", typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@kode_sumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.ReportSuratRekapDropping", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {

                if(ddlsumberdana.SelectedValue == "A001")
                {
                    rprt.Load(Server.MapPath("~/SD/Report/Form/SD_SuratPermohonanTransferPembiayaan_HIK.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);

                }                     
                else if(ddlsumberdana.SelectedValue == "A005")
                {
                    rprt.Load(Server.MapPath("~/SD/Report/Form/SD_SuratPermohonanTransferPembiayaan_RamaGanda.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);

                }                
                else if(ddlsumberdana.SelectedValue == "A022")
                {
                    rprt.Load(Server.MapPath("~/SD/Report/Form/SD_SuratPermohonanTransferPembiayaan_NBP.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);

                }               
                else if(ddlsumberdana.SelectedValue == "A018")
                {
                    rprt.Load(Server.MapPath("~/SD/Report/Form/SD_SuratPermohonanTransferPembiayaan_DanaLestari.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);

                }                
                else if(ddlsumberdana.SelectedValue == "A004")
                {
                    rprt.Load(Server.MapPath("~/SD/Report/Form/SD_SuratPermohonanTransferPembiayaan_CAR.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);

                }                
                else if(ddlsumberdana.SelectedValue == "A023")
                {
                    rprt.Load(Server.MapPath("~/SD/Report/Form/SD_SuratPermohonanTransferPembiayaan_CahayaArthaSejati.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);

                }
                else
                {
                    rprt.Load(Server.MapPath("~/SD/Report/Form/SD_SuratPermohonanTransferPembiayaan.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);

                }

            }

            string namefile = "SuratTransferPembiayaan_" + table.Rows[0]["NoBukti"].ToString().Replace("/", "_") + ".doc";
            using (Stream rptStream =
                   rprt.ExportToStream(
                       ExportFormatType.WordForWindows))
            {
                ZipArchiveEntry entry =
                    zip.CreateEntry(namefile);

                using (Stream entryStream = entry.Open())
                {
                    rptStream.CopyTo(entryStream);
                }
            }

            rprt.Close();
            rprt.Dispose();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    protected static GlobalEntities GetData(string STATE, string sumberdana,DataTable data)
    {
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
        Params[0] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
        Params[1] = gb.CreateSPParameter("@kode_sumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, sumberdana, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, "01", typeof(string));
        Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        Params[4] = gb.CreateSPParameter("@SD_LIST_REKAPDROPPING", ParameterDirection.Input, SqlDbType.Structured, 0, data, typeof(DataTable));

        ge.ListDataset = gb.GetDataJustOutPut("SuratRekapDropping", Params, "constrmf");

        return ge;
    }

    protected void ddlsumberdana_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[5];
        x[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        x[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTREKAPDROPPING", typeof(string));
        x[3] = gb.CreateSPParameter("@kode_sumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
        x[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SuratRekapDropping", x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}