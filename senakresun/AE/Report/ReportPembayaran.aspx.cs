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
using System.Text.RegularExpressions;
using System.Globalization;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Drawing.Diagrams;

public partial class AE_Report_ReportPembayaran : System.Web.UI.Page
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
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    GlobalEntities dt2 = new GlobalEntities();
    GlobalEntities dt3 = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {


                DateTime now = DateTime.Now;
               
                BindGridEntity();
               
               
                
                
                
            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), System.IO.Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetCabangReport", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
        ddlcabang.Items.Insert(0, new ListItem("ALL CABANG", "ALL"));
        ddlcabang.SelectedValue = SessBranchID;
        ambilTahun();
        ambilBulan();
        //if (SessBranchID == "000")
        //{
        //    ddlcabang.Enabled = true;
        //}
        //else
        //{
        //    ddlcabang.Enabled = false;
        //}
        DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[1];
        Paramst[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));


        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetKoperasiReport", Paramst, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlkoperasi, "None", "");
        ddlkoperasi.Items.Insert(0, new ListItem("ALL KOPERASI", "ALL"));



        DefaultClass.Global.SqlHelper[] Paramsr = new DefaultClass.Global.SqlHelper[1];
        Paramsr[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));


        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetAreaReport", Paramsr, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlarea, "None", "");
        ddlarea.Items.Insert(0, new ListItem("ALL AREA", "ALL"));



        ddlcabang.SelectedValue = SessBranchID;



        if (SessBranchID == "000")
        {
            //ddlcabang.Enabled = true;
            ddltypereport.Enabled = true;
            ddltypereport.SelectedValue = "1";
            if (ddltypereport.SelectedValue == "1")

            {
                ddlkoperasi.Enabled = true;
                ddlarea.Enabled = false;
                ddlcabang.Enabled = false;
                ddlarea.SelectedValue = "ALL";
                ddlkoperasi.SelectedValue = "ALL";
                ddlcabang.SelectedValue = "ALL";
            }
            else if (ddltypereport.SelectedValue == "2")
            {
                ddlkoperasi.Enabled = false;
                ddlarea.Enabled = true;
                ddlcabang.Enabled = false;
                ddlarea.SelectedValue = "ALL";
                ddlkoperasi.SelectedValue = "ALL";
                ddlcabang.SelectedValue = "ALL";

            }
            else
            {
                ddlkoperasi.Enabled = false;
                ddlarea.Enabled = false;
                ddlcabang.Enabled = true;
                ddlarea.SelectedValue = "ALL";
                ddlkoperasi.SelectedValue = "ALL";
                ddlcabang.SelectedValue = "ALL";
            }
        }
        else
        {
            ddltypereport.Enabled = false;
            ddltypereport.SelectedValue = "3";
            ddlkoperasi.Enabled = false;
            ddlarea.Enabled = false;
            ddlcabang.Enabled = false;
            ddlarea.SelectedValue = "ALL";
            ddlkoperasi.SelectedValue = "ALL";

            ddlcabang.SelectedValue = SessBranchID;
        }

    }
    protected void ddltypereport_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddltypereport.SelectedValue == "1")

        {
            ddlkoperasi.Enabled = true;
            ddlarea.Enabled = false;
            ddlcabang.Enabled = false;
            ddlarea.SelectedValue = "ALL";
            ddlkoperasi.SelectedValue = "ALL";
            ddlcabang.SelectedValue = "ALL";
        }
        else if (ddltypereport.SelectedValue == "2")
        {
            ddlkoperasi.Enabled = false;
            ddlarea.Enabled = true;
            ddlcabang.Enabled = false;
            ddlarea.SelectedValue = "ALL";
            ddlkoperasi.SelectedValue = "ALL";
            ddlcabang.SelectedValue = "ALL";

        }
        else
        {
            ddlkoperasi.Enabled = false;
            ddlarea.Enabled = false;
            ddlcabang.Enabled = true;
            ddlarea.SelectedValue = "ALL";
            ddlkoperasi.SelectedValue = "ALL";
            ddlcabang.SelectedValue = "ALL";
        }



    }

    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 4; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddltahun.Items.Add((currentYear - i).ToString());
        }
        ddltahun.SelectedValue = currentYear.ToString();

    }

    public void ambilBulan()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length; i++)
        {
            ddlSP.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
        }
        var currentmonth = DateTime.Today.Month;
        ddlSP.SelectedValue = currentmonth.ToString();

    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {


            if (ddltypereport.SelectedValue == "3")
            {

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BAYARCABANG", typeof(string));
                varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                varx[5] = gb.CreateSPParameter("@statusbyr", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusByr.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_KASBON_XLS", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {

                    string huruf = "", deltax;
                    string huruf2, huruf1;

                    huruf = "";
                    int abjadlanjut = 65;
                    int abjadawal = 64;
                    string strAlphaawal = "";
                    string strAlphalanjut = "";
                    string alpha1 = "";
                    string alpha2 = "";
                    int x1 = 1;


                    string name;
                    //name=dt.Rows[i][0].;
                    name = "Bayar_Cabang_" + ddlcabang.SelectedValue + "_" + ddlstatusByr.SelectedValue;

                    //DateTime now = DateTime.Parse(txttglhariini.Text);
                    XLWorkbook wb = new XLWorkbook();
                    var worksheet = wb.Worksheets.Add(name);
                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.Margins.Top = 0.4;
                    worksheet.PageSetup.Margins.Left = 0.3;
                    worksheet.PageSetup.Margins.Right = 0.3;
                    worksheet.PageSetup.Margins.Bottom = 0.5;

                    worksheet.PageSetup.Margins.Header = 0.3;
                    worksheet.PageSetup.Margins.Footer = 0.3;
                    worksheet.RowHeight = 13;
                    string hurufx = "";
                    int abjadlanjutx = 65;
                    int abjadawalx = 64;
                    string strAlphaawalx = "";
                    string strAlphalanjutx = "";
                    string alpha1x = "";
                    string alpha2x = "";

                    string xnama;
                    string groupx;
                    string noperkiraan;
                    string ku_mingguan;
                    //string tahunini




                    worksheet.Cell("A1").Value = "LAPORAN PEMBAYARAN KASBON";
                    worksheet.Cell("A1").Style.Font.FontSize = 11;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("A2").Value = "STATUS";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 11;
                    worksheet.Cell("A2").Style.Font.Bold = true;
                    worksheet.Cell("A3").Value = "Koperasi";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 11;
                    worksheet.Cell("A3").Style.Font.Bold = true;
                    worksheet.Cell("A4").Value = "Area";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 11;
                    worksheet.Cell("A4").Style.Font.Bold = true;
                    worksheet.Cell("A5").Value = "Cabang";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 11;
                    worksheet.Cell("A5").Style.Font.Bold = true;

                    worksheet.Cell("A6").Value = "Bulan/Tahun";
                    worksheet.Cell("A6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A6").Style.Font.FontSize = 11;
                    worksheet.Cell("A6").Style.Font.Bold = true;

                    worksheet.Cell("B2").Value = "'" + ddlstatusByr.SelectedItem.Text;

                    worksheet.Cell("B2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B2").Style.Font.FontSize = 11;
                    worksheet.Cell("B2").Style.Font.Bold = true;
                    worksheet.Cell("B3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B3").Style.Font.FontSize = 11;
                    worksheet.Cell("B3").Style.Font.Bold = true;
                    worksheet.Cell("B3").Value = "'" + ddlkoperasi.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B4").Style.Font.FontSize = 11;
                    worksheet.Cell("B4").Value = "'" + ddlarea.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.Bold = true;
                    worksheet.Cell("B5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B5").Style.Font.FontSize = 11;
                    worksheet.Cell("B5").Value = "'" + ddlcabang.SelectedItem.Text;
                    worksheet.Cell("B5").Style.Font.Bold = true;
                    worksheet.Cell("B6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B6").Style.Font.FontSize = 11;
                    worksheet.Cell("B6").Value = "'" + ddlSP.SelectedItem.Text + '/' + ddltahun.SelectedValue;
                    worksheet.Cell("B6").Style.Font.Bold = true;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.WrapText = true;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.FontName = "Calibri";
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.FontSize = 11;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Fill.BackgroundColor = XLColor.PaleAqua;

                    worksheet.Column(1).Width = 10.89;
                    worksheet.Column(2).Width = 18.11;
                    worksheet.Column(3).Width = 17.11;
                    worksheet.Column(4).Width = 17.11;
                    worksheet.Column(5).Width = 4.33;
                    worksheet.Column(6).Width = 17.11;
                    worksheet.Column(7).Width = 18.67;
                    worksheet.Column(8).Width = 17.11;
                    worksheet.Column(9).Width = 9.11;
                    worksheet.Column(10).Width = 17.11;
                    worksheet.Column(11).Width = 17.11;
                    worksheet.Column(12).Width = 17.11;
                    




                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 1)).Value = "NO.";
                    worksheet.Range(worksheet.Cell(8, 2), worksheet.Cell(8, 2)).Value = "Cabang";
                    worksheet.Range(worksheet.Cell(8, 3), worksheet.Cell(8, 3)).Value = "Tgl Bayar";
                    worksheet.Range(worksheet.Cell(8, 4), worksheet.Cell(8, 4)).Value = "No. Bayar";
                    worksheet.Range(worksheet.Cell(8, 5), worksheet.Cell(8, 5)).Value = "Type";
                    worksheet.Range(worksheet.Cell(8, 6), worksheet.Cell(8, 6)).Value = "Keterangan";
                    worksheet.Range(worksheet.Cell(8, 7), worksheet.Cell(8, 7)).Value = "Kode Nik";
                    worksheet.Range(worksheet.Cell(8, 8), worksheet.Cell(8, 8)).Value = "Nama Karyawan";
                    worksheet.Range(worksheet.Cell(8, 9), worksheet.Cell(8, 9)).Value = "Angs Ke";
                    worksheet.Range(worksheet.Cell(8, 10), worksheet.Cell(8, 10)).Value = "Jumlah Bayar";
                    worksheet.Range(worksheet.Cell(8, 11), worksheet.Cell(8, 11)).Value = "No Tagihan";
                    worksheet.Range(worksheet.Cell(8, 12), worksheet.Cell(8, 12)).Value = "Tgl Tagihan";




                    int b;
                    int p;
                    b = 8;
                    p = 0;


                    for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                    {


                        b = b + 1;
                        p = p + 1;


                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "B").Value = dt2.ListDataset.Tables[0].Rows[x]["cabang_nama"].ToString();
                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString();
                        worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoBukti"].ToString(); 
                        worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["typetrn"].ToString();
                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["tppot"].ToString();
                        worksheet.Cell(b, "G").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeNik"].ToString();
                        worksheet.Cell(b, "H").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaKry"].ToString();
                        worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["angs_ke"].ToString();
                        worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["JumlahTrn"].ToString().Replace(",", ".");
                        worksheet.Cell(b, "K").Value = dt2.ListDataset.Tables[0].Rows[x]["Nofaktur"].ToString();
                        worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["TglJthTmp"].ToString();

                    }



                    //worksheet.Range(worksheet.Cell(13, 13), worksheet.Cell(b, 13)).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(3).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(12).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";








                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;





                    int pakhir = 0;
                    pakhir = b + 1;
                    worksheet.Cell(pakhir, "D").Value = "Total";



                    worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J8:J" + b + ")";

                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";


                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontName = "Calibri";
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontSize = 11;
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Fill.BackgroundColor = XLColor.PaleAqua;



                    string report = "";
                    //if (ddlproduk.SelectedValue == "3C")
                    //{
                    report = "Bayar_cabang" + ddlcabang.SelectedValue + "_" + ddlstatusByr.SelectedItem.Text + ".xlsx";
                    //}
                    //else if (ddlproduk.SelectedValue == "3B")
                    //{
                    //    report = "Report Listtagihan Bulanan " + xtanggal.ToString() + ".xlsx";
                    //}



                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + report + "");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }

                }




                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak ditemukan!');", true);
                }

            }

            else if (ddltypereport.SelectedValue == "2")
            {

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlarea.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BAYARAREA", typeof(string));
                varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                varx[5] = gb.CreateSPParameter("@statusbyr", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusByr.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_KASBON_XLS", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {

                    string huruf = "", deltax;
                    string huruf2, huruf1;

                    huruf = "";
                    int abjadlanjut = 65;
                    int abjadawal = 64;
                    string strAlphaawal = "";
                    string strAlphalanjut = "";
                    string alpha1 = "";
                    string alpha2 = "";
                    int x1 = 1;


                    string name;
                    //name=dt.Rows[i][0].;
                    name = "Bayar_Area_" + ddlarea.SelectedValue + "_" + ddlstatusByr.SelectedValue;

                    //DateTime now = DateTime.Parse(txttglhariini.Text);
                    XLWorkbook wb = new XLWorkbook();
                    var worksheet = wb.Worksheets.Add(name);
                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.Margins.Top = 0.4;
                    worksheet.PageSetup.Margins.Left = 0.3;
                    worksheet.PageSetup.Margins.Right = 0.3;
                    worksheet.PageSetup.Margins.Bottom = 0.5;

                    worksheet.PageSetup.Margins.Header = 0.3;
                    worksheet.PageSetup.Margins.Footer = 0.3;
                    worksheet.RowHeight = 13;
                    string hurufx = "";
                    int abjadlanjutx = 65;
                    int abjadawalx = 64;
                    string strAlphaawalx = "";
                    string strAlphalanjutx = "";
                    string alpha1x = "";
                    string alpha2x = "";

                    string xnama;
                    string groupx;
                    string noperkiraan;
                    string ku_mingguan;
                    //string tahunini




                    worksheet.Cell("A1").Value = "LAPORAN PEMBAYARAN KASBON";
                    worksheet.Cell("A1").Style.Font.FontSize = 11;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("A2").Value = "STATUS";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 11;
                    worksheet.Cell("A2").Style.Font.Bold = true;
                    worksheet.Cell("A3").Value = "Koperasi";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 11;
                    worksheet.Cell("A3").Style.Font.Bold = true;
                    worksheet.Cell("A4").Value = "Area";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 11;
                    worksheet.Cell("A4").Style.Font.Bold = true;
                    worksheet.Cell("A5").Value = "Cabang";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 11;
                    worksheet.Cell("A5").Style.Font.Bold = true;

                    worksheet.Cell("A6").Value = "Bulan/Tahun";
                    worksheet.Cell("A6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A6").Style.Font.FontSize = 11;
                    worksheet.Cell("A6").Style.Font.Bold = true;

                    worksheet.Cell("B2").Value = "'" + ddlstatusByr.SelectedItem.Text;

                    worksheet.Cell("B2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B2").Style.Font.FontSize = 11;
                    worksheet.Cell("B2").Style.Font.Bold = true;
                    worksheet.Cell("B3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B3").Style.Font.FontSize = 11;
                    worksheet.Cell("B3").Style.Font.Bold = true;
                    worksheet.Cell("B3").Value = "'" + ddlkoperasi.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B4").Style.Font.FontSize = 11;
                    worksheet.Cell("B4").Value = "'" + ddlarea.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.Bold = true;
                    worksheet.Cell("B5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B5").Style.Font.FontSize = 11;
                    worksheet.Cell("B5").Value = "'" + ddlcabang.SelectedItem.Text;
                    worksheet.Cell("B5").Style.Font.Bold = true;
                    worksheet.Cell("B6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B6").Style.Font.FontSize = 11;
                    worksheet.Cell("B6").Value = "'" + ddlSP.SelectedItem.Text + '/' + ddltahun.SelectedValue;
                    worksheet.Cell("B6").Style.Font.Bold = true;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.WrapText = true;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.FontName = "Calibri";
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.FontSize = 11;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Fill.BackgroundColor = XLColor.PaleAqua;

                    worksheet.Column(1).Width = 10.89;
                    worksheet.Column(2).Width = 18.11;
                    worksheet.Column(3).Width = 17.11;
                    worksheet.Column(4).Width = 17.11;
                    worksheet.Column(5).Width = 4.33;
                    worksheet.Column(6).Width = 17.11;
                    worksheet.Column(7).Width = 18.67;
                    worksheet.Column(8).Width = 17.11;
                    worksheet.Column(9).Width = 9.11;
                    worksheet.Column(10).Width = 17.11;
                    worksheet.Column(11).Width = 17.11;
                    worksheet.Column(12).Width = 17.11;





                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 1)).Value = "NO.";
                    worksheet.Range(worksheet.Cell(8, 2), worksheet.Cell(8, 2)).Value = "Cabang";
                    worksheet.Range(worksheet.Cell(8, 3), worksheet.Cell(8, 3)).Value = "Tgl Bayar";
                    worksheet.Range(worksheet.Cell(8, 4), worksheet.Cell(8, 4)).Value = "No. Bayar";
                    worksheet.Range(worksheet.Cell(8, 5), worksheet.Cell(8, 5)).Value = "Type";
                    worksheet.Range(worksheet.Cell(8, 6), worksheet.Cell(8, 6)).Value = "Keterangan";
                    worksheet.Range(worksheet.Cell(8, 7), worksheet.Cell(8, 7)).Value = "Kode Nik";
                    worksheet.Range(worksheet.Cell(8, 8), worksheet.Cell(8, 8)).Value = "Nama Karyawan";
                    worksheet.Range(worksheet.Cell(8, 9), worksheet.Cell(8, 9)).Value = "Angs Ke";
                    worksheet.Range(worksheet.Cell(8, 10), worksheet.Cell(8, 10)).Value = "Jumlah Bayar";
                    worksheet.Range(worksheet.Cell(8, 11), worksheet.Cell(8, 11)).Value = "No Tagihan";
                    worksheet.Range(worksheet.Cell(8, 12), worksheet.Cell(8, 12)).Value = "Tgl Tagihan";




                    int b;
                    int p;
                    b = 8;
                    p = 0;


                    for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                    {


                        b = b + 1;
                        p = p + 1;


                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "B").Value = dt2.ListDataset.Tables[0].Rows[x]["cabang_nama"].ToString();
                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString();
                        worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoBukti"].ToString();
                        worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["typetrn"].ToString();
                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["tppot"].ToString();
                        worksheet.Cell(b, "G").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeNik"].ToString();
                        worksheet.Cell(b, "H").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaKry"].ToString();
                        worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["angs_ke"].ToString();
                        worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["JumlahTrn"].ToString().Replace(",", ".");
                        worksheet.Cell(b, "K").Value = dt2.ListDataset.Tables[0].Rows[x]["Nofaktur"].ToString();
                        worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["TglJthTmp"].ToString();

                    }



                    //worksheet.Range(worksheet.Cell(13, 13), worksheet.Cell(b, 13)).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(3).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(12).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";








                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;





                    int pakhir = 0;
                    pakhir = b + 1;
                    worksheet.Cell(pakhir, "D").Value = "Total";



                    worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J8:J" + b + ")";

                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";


                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontName = "Calibri";
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontSize = 11;
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Fill.BackgroundColor = XLColor.PaleAqua;



                    string report = "";
                    //if (ddlproduk.SelectedValue == "3C")
                    //{
                    report = "Bayar_Area" + ddlarea.SelectedValue + "_" + ddlstatusByr.SelectedItem.Text + ".xlsx";
                    //}
                    //else if (ddlproduk.SelectedValue == "3B")
                    //{
                    //    report = "Report Listtagihan Bulanan " + xtanggal.ToString() + ".xlsx";
                    //}



                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + report + "");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }

                }




                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak ditemukan!');", true);
                }

            }

            else if (ddltypereport.SelectedValue == "1")
            {

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkoperasi.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BAYARKOPERASI", typeof(string));
                varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                varx[5] = gb.CreateSPParameter("@statusbyr", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusByr.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_KASBON_XLS", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {

                    string huruf = "", deltax;
                    string huruf2, huruf1;

                    huruf = "";
                    int abjadlanjut = 65;
                    int abjadawal = 64;
                    string strAlphaawal = "";
                    string strAlphalanjut = "";
                    string alpha1 = "";
                    string alpha2 = "";
                    int x1 = 1;


                    string name;
                    //name=dt.Rows[i][0].;
                    name = "Bayar_Koperasi_" + ddlkoperasi.SelectedValue + "_" + ddlstatusByr.SelectedValue;

                    //DateTime now = DateTime.Parse(txttglhariini.Text);
                    XLWorkbook wb = new XLWorkbook();
                    var worksheet = wb.Worksheets.Add(name);
                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.Margins.Top = 0.4;
                    worksheet.PageSetup.Margins.Left = 0.3;
                    worksheet.PageSetup.Margins.Right = 0.3;
                    worksheet.PageSetup.Margins.Bottom = 0.5;

                    worksheet.PageSetup.Margins.Header = 0.3;
                    worksheet.PageSetup.Margins.Footer = 0.3;
                    worksheet.RowHeight = 13;
                    string hurufx = "";
                    int abjadlanjutx = 65;
                    int abjadawalx = 64;
                    string strAlphaawalx = "";
                    string strAlphalanjutx = "";
                    string alpha1x = "";
                    string alpha2x = "";

                    string xnama;
                    string groupx;
                    string noperkiraan;
                    string ku_mingguan;
                    //string tahunini




                    worksheet.Cell("A1").Value = "LAPORAN PEMBAYARAN KASBON";
                    worksheet.Cell("A1").Style.Font.FontSize = 11;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("A2").Value = "STATUS";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 11;
                    worksheet.Cell("A2").Style.Font.Bold = true;
                    worksheet.Cell("A3").Value = "Koperasi";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 11;
                    worksheet.Cell("A3").Style.Font.Bold = true;
                    worksheet.Cell("A4").Value = "Area";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 11;
                    worksheet.Cell("A4").Style.Font.Bold = true;
                    worksheet.Cell("A5").Value = "Cabang";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 11;
                    worksheet.Cell("A5").Style.Font.Bold = true;

                    worksheet.Cell("A6").Value = "Bulan/Tahun";
                    worksheet.Cell("A6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A6").Style.Font.FontSize = 11;
                    worksheet.Cell("A6").Style.Font.Bold = true;

                    worksheet.Cell("B2").Value = "'" + ddlstatusByr.SelectedItem.Text;

                    worksheet.Cell("B2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B2").Style.Font.FontSize = 11;
                    worksheet.Cell("B2").Style.Font.Bold = true;
                    worksheet.Cell("B3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B3").Style.Font.FontSize = 11;
                    worksheet.Cell("B3").Style.Font.Bold = true;
                    worksheet.Cell("B3").Value = "'" + ddlkoperasi.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B4").Style.Font.FontSize = 11;
                    worksheet.Cell("B4").Value = "'" + ddlarea.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.Bold = true;
                    worksheet.Cell("B5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B5").Style.Font.FontSize = 11;
                    worksheet.Cell("B5").Value = "'" + ddlcabang.SelectedItem.Text;
                    worksheet.Cell("B5").Style.Font.Bold = true;
                    worksheet.Cell("B6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B6").Style.Font.FontSize = 11;
                    worksheet.Cell("B6").Value = "'" + ddlSP.SelectedItem.Text + '/' + ddltahun.SelectedValue;
                    worksheet.Cell("B6").Style.Font.Bold = true;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Alignment.WrapText = true;

                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.FontName = "Calibri";
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.FontSize = 11;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 12)).Style.Fill.BackgroundColor = XLColor.PaleAqua;

                    worksheet.Column(1).Width = 10.89;
                    worksheet.Column(2).Width = 18.11;
                    worksheet.Column(3).Width = 17.11;
                    worksheet.Column(4).Width = 17.11;
                    worksheet.Column(5).Width = 4.33;
                    worksheet.Column(6).Width = 17.11;
                    worksheet.Column(7).Width = 18.67;
                    worksheet.Column(8).Width = 17.11;
                    worksheet.Column(9).Width = 9.11;
                    worksheet.Column(10).Width = 17.11;
                    worksheet.Column(11).Width = 17.11;
                    worksheet.Column(12).Width = 17.11;





                    worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 1)).Value = "NO.";
                    worksheet.Range(worksheet.Cell(8, 2), worksheet.Cell(8, 2)).Value = "Cabang";
                    worksheet.Range(worksheet.Cell(8, 3), worksheet.Cell(8, 3)).Value = "Tgl Bayar";
                    worksheet.Range(worksheet.Cell(8, 4), worksheet.Cell(8, 4)).Value = "No. Bayar";
                    worksheet.Range(worksheet.Cell(8, 5), worksheet.Cell(8, 5)).Value = "Type";
                    worksheet.Range(worksheet.Cell(8, 6), worksheet.Cell(8, 6)).Value = "Keterangan";
                    worksheet.Range(worksheet.Cell(8, 7), worksheet.Cell(8, 7)).Value = "Kode Nik";
                    worksheet.Range(worksheet.Cell(8, 8), worksheet.Cell(8, 8)).Value = "Nama Karyawan";
                    worksheet.Range(worksheet.Cell(8, 9), worksheet.Cell(8, 9)).Value = "Angs Ke";
                    worksheet.Range(worksheet.Cell(8, 10), worksheet.Cell(8, 10)).Value = "Jumlah Bayar";
                    worksheet.Range(worksheet.Cell(8, 11), worksheet.Cell(8, 11)).Value = "No Tagihan";
                    worksheet.Range(worksheet.Cell(8, 12), worksheet.Cell(8, 12)).Value = "Tgl Tagihan";




                    int b;
                    int p;
                    b = 8;
                    p = 0;


                    for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                    {


                        b = b + 1;
                        p = p + 1;


                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "B").Value = dt2.ListDataset.Tables[0].Rows[x]["cabang_nama"].ToString();
                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString();
                        worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoBukti"].ToString();
                        worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["typetrn"].ToString();
                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["tppot"].ToString();
                        worksheet.Cell(b, "G").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeNik"].ToString();
                        worksheet.Cell(b, "H").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaKry"].ToString();
                        worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["angs_ke"].ToString();
                        worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["JumlahTrn"].ToString().Replace(",", ".");
                        worksheet.Cell(b, "K").Value = dt2.ListDataset.Tables[0].Rows[x]["Nofaktur"].ToString();
                        worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["TglJthTmp"].ToString();

                    }



                    //worksheet.Range(worksheet.Cell(13, 13), worksheet.Cell(b, 13)).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(3).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(12).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";








                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;





                    int pakhir = 0;
                    pakhir = b + 1;
                    worksheet.Cell(pakhir, "D").Value = "Total";



                    worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J8:J" + b + ")";

                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";


                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontName = "Calibri";
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontSize = 11;
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Fill.BackgroundColor = XLColor.PaleAqua;



                    string report = "";
                    //if (ddlproduk.SelectedValue == "3C")
                    //{
                    report = "Bayar_Koperasi" + ddlkoperasi.SelectedValue + "_" + ddlstatusByr.SelectedItem.Text + ".xlsx";
                    //}
                    //else if (ddlproduk.SelectedValue == "3B")
                    //{
                    //    report = "Report Listtagihan Bulanan " + xtanggal.ToString() + ".xlsx";
                    //}



                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + report + "");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }

                }




                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak ditemukan!');", true);
                }

            }







        }

        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }
            else
            {

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }

    



}