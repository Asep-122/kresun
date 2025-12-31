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

public partial class AE_Report_ReportTHR : System.Web.UI.Page
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
                //ddlsp_SelectedIndexChanged(sender, e);
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
        //ddlcabang.Items.Insert(0, new ListItem("-", "-"));
        ddlcabang.SelectedValue = SessBranchID;
        ambilTahun();
        ambilBulan();
        if (SessBranchID == "000")
        {
            ddlcabang.Enabled = true;
        }
        else
        {
            ddlcabang.Enabled = false;
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


           


                //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                //ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BAGIAN", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@dept", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                var[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_STD_THR_XLS", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
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
                name = "THR_" + ddlcabang.SelectedValue + "_" + ddlSP.SelectedItem.Text + "_" + ddltahun.SelectedValue;

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



                
                worksheet.Cell("A1").Value = "LAPORAN TUNJANGAN HARI RAYA";
                worksheet.Cell("A1").Style.Font.FontSize = 11;
                worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                worksheet.Cell("A1").Style.Font.Bold = true;
                worksheet.Cell("A2").Value = "Bulan/Tahun";
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

                worksheet.Cell("B2").Value = "'" + ddlSP.SelectedItem.Text + "/" + ddltahun.SelectedValue;

                worksheet.Cell("B2").Style.Font.FontName = "Calibri";
                worksheet.Cell("B2").Style.Font.FontSize = 11;
                worksheet.Cell("B2").Style.Font.Bold = true;
                worksheet.Cell("B3").Style.Font.FontName = "Calibri";
                worksheet.Cell("B3").Style.Font.FontSize = 11;
                worksheet.Cell("B3").Style.Font.Bold = true;
                worksheet.Cell("B3").Value = ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();
                worksheet.Cell("B4").Style.Font.FontName = "Calibri";
                worksheet.Cell("B4").Style.Font.FontSize = 11;
                worksheet.Cell("B4").Value = ge.ListDataset.Tables[0].Rows[0]["Area"].ToString();
                worksheet.Cell("B4").Style.Font.Bold = true;
                worksheet.Cell("B5").Style.Font.FontName = "Calibri";
                worksheet.Cell("B5").Style.Font.FontSize = 11;
                worksheet.Cell("B5").Value = ge.ListDataset.Tables[0].Rows[0]["cabang"].ToString();
                worksheet.Cell("B5").Style.Font.Bold = true;


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
           
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Font.FontName = "Calibri";
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Font.FontSize = 11;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 10)).Style.Fill.BackgroundColor = XLColor.PaleAqua;




                worksheet.Column(1).Width = 13.89;
                worksheet.Row(8).Height = 46.20;
                worksheet.Row(9).Height = 46.20;
                worksheet.Column(2).Width = 29.33;
                worksheet.Column(3).Width = 29.33;
                worksheet.Column(7).Width = 19.78;
                worksheet.Column(8).Width = 17.11;
                worksheet.Column(9).Width = 17.11;
                worksheet.Column(10).Width = 17.11;
                

                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 1)).Merge();
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 1)).Value="NO.";

                worksheet.Range(worksheet.Cell(7, 2), worksheet.Cell(9, 2)).Merge();
                worksheet.Range(worksheet.Cell(7, 2), worksheet.Cell(9, 2)).Value = "NAMA LENGKAP";
               

                worksheet.Range(worksheet.Cell(7, 3), worksheet.Cell(9, 3)).Merge();
                worksheet.Range(worksheet.Cell(7, 3), worksheet.Cell(9, 3)).Value = "TANGGAL MASUK";

                worksheet.Cell("D7").Value = "MASA KERJA";
                worksheet.Range(worksheet.Cell(7, 4), worksheet.Cell(7, 6)).Merge();

                worksheet.Range(worksheet.Cell(8, 4), worksheet.Cell(9, 4)).Merge();
                worksheet.Range(worksheet.Cell(8, 4), worksheet.Cell(9, 4)).Value = "TAHUN";

                worksheet.Range(worksheet.Cell(8, 5), worksheet.Cell(9, 5)).Merge();
                worksheet.Range(worksheet.Cell(8, 5), worksheet.Cell(9, 5)).Value = "BULAN";

                worksheet.Range(worksheet.Cell(8, 6), worksheet.Cell(9, 6)).Merge();
                worksheet.Range(worksheet.Cell(8, 6), worksheet.Cell(9, 6)).Value = "HARI";



                worksheet.Range(worksheet.Cell(7, 7), worksheet.Cell(9, 7)).Merge();
                worksheet.Range(worksheet.Cell(7, 7), worksheet.Cell(9, 7)).Value = "JABATAN";


                worksheet.Range(worksheet.Cell(7, 8), worksheet.Cell(9, 8)).Merge();
                worksheet.Range(worksheet.Cell(7, 8), worksheet.Cell(9, 8)).Value = "THR";

                worksheet.Range(worksheet.Cell(7, 9), worksheet.Cell(9, 9)).Merge();
                worksheet.Range(worksheet.Cell(7, 9), worksheet.Cell(9, 9)).Value = "POTONGAN";
                worksheet.Range(worksheet.Cell(7, 9), worksheet.Cell(9, 9)).Style.Alignment.WrapText = true;

                worksheet.Range(worksheet.Cell(7, 10), worksheet.Cell(9, 10)).Merge();
                worksheet.Range(worksheet.Cell(7, 10), worksheet.Cell(9, 10)).Value = "TERIMA THR SETELAH POTONGAN";
                worksheet.Range(worksheet.Cell(7, 10), worksheet.Cell(9, 10)).Style.Alignment.WrapText = true;


                int b;
                int p;
                b =10;
                p = 0;

                for (int i = 0; i < ge.ListDataset.Tables[0].Rows.Count; i++)
                    {

                    b = b + 1;
                    worksheet.Cell(b, "A").Value = "'" + ge.ListDataset.Tables[0].Rows[i]["KetBagian"].ToString();
                    worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "A").Style.Font.FontSize = 11;
                    worksheet.Cell(b, "A").Style.Font.Bold =true;
                    worksheet.Range(worksheet.Cell(b, 1), worksheet.Cell(b, 2)).Merge();
                    worksheet.Range(worksheet.Cell(b, 1), worksheet.Cell(b, 10)).Style.Fill.BackgroundColor = XLColor.AntiFlashWhite;
                    
                    
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                    varx[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                    varx[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                    varx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                    varx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    varx[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "THR", typeof(string));
                    varx[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[6] = gb.CreateSPParameter("@bagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ge.ListDataset.Tables[0].Rows[i][0].ToString(), typeof(string));
                    dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_THR_XLS", varx, "constrmf");
                    if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                    {

                        for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                        {


                            b = b + 1;
                            p = p + 1;

                           
                            worksheet.Cell(b, "A").Value = p;


                            worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaKaryawan"].ToString();
                            worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["TglTerima"].ToString();
                            worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["masakerjatahun"].ToString();
                            worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["masakerjabulan"].ToString();
                            worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["masakerjahari"].ToString();
                            worksheet.Cell(b, "G").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KetJab"].ToString();
                            worksheet.Cell(b, "H").Value = dt2.ListDataset.Tables[0].Rows[x]["JlhTTHR"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["JlhPotongan"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["JlhGajiTerima"].ToString().Replace(",", ".");

                            


                        }

                        //int pakhir = 0;

                        //pakhir = 1 + p;

                       
                        //worksheet.Cell(pakhir, "D").Value = "Total";
                       


                        //worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + b + ")";
                        //worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + b + ")";
                        //worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I8:I" + b + ")";

                        //worksheet.Cell(pakhir, "K").FormulaA1 = "=SUM(K8:K" + b + ")";
                        //worksheet.Cell(pakhir, "L").FormulaA1 = "=SUM(L8:L" + b + ")";
                        //worksheet.Cell(pakhir, "M").FormulaA1 = "=SUM(M8:M" + b + ")";
                        //worksheet.Cell(pakhir, "N").FormulaA1 = "(K" + pakhir + "/f" + pakhir + ")*100";


                        //worksheet.Cell(pakhir, "Q").FormulaA1 = "=SUM(Q8:Q" + b + ")";
                        //worksheet.Cell(pakhir, "R").FormulaA1 = "=SUM(R8:R" + b + ")";
                        //worksheet.Cell(pakhir, "S").FormulaA1 = "=SUM(S8:S" + b + ")";
                        //worksheet.Cell(pakhir, "T").FormulaA1 = "(Q" + pakhir + "/f" + pakhir + ")*100";


                        worksheet.Range(worksheet.Cell(12, 8), worksheet.Cell(b, 10)).Style.NumberFormat.Format = "_(#,##0_)";



                        //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";


                        //worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(11).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(12).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(13).Style.NumberFormat.Format = "_(#,##0_)";

                        //worksheet.Column(14).Style.NumberFormat.Format = "#,##0.00";

                        //worksheet.Column(3).Style.NumberFormat.Format = "dd/mm/yyyy";

                        //worksheet.Column(16).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(17).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(18).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(19).Style.NumberFormat.Format = "_(#,##0_)";

                        //worksheet.Column(20).Style.NumberFormat.Format = "#,##0.00";


                        //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                        //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";











                        //worksheet.Row(1).Height = 12;
                        //worksheet.Row(2).Height = 12;

                        //worksheet.Row(3).Height = 12;
                        //worksheet.Row(4).Height = 12;

                        //worksheet.Row(5).Height = 12;
                        //worksheet.Row(6).Height = 12;
                        //worksheet.Row(7).Height = 36;
                        //worksheet.Row(8).Height = 12;
                        //worksheet.Row(9).Height = 12;


                       




                    }

                   

                    //worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + b + ")";
                    //worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I8:I" + b + ")";




                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;




                }
                int pakhir = 0;
                pakhir = b + 1;
                worksheet.Cell(pakhir, "G").Value = "Total";



                worksheet.Cell(pakhir, "H").FormulaA1 = "=SUM(H12:H" + b + ")";
                worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I12:I" + b + ")";
                worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J12:J" + b + ")";
              

                worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 10)).Style.Font.FontName = "Calibri";
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 10)).Style.Font.FontSize = 11;
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 10)).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 10)).Style.Fill.BackgroundColor = XLColor.PaleAqua;



                string report = "";
                    //if (ddlproduk.SelectedValue == "3C")
                    //{
                        report = "THR_" + ddlcabang.SelectedValue + "_" + ddlSP.SelectedItem.Text + "_" + ddltahun.SelectedValue + ".xlsx";
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
 


    public void btnSerach_Click(object sender, EventArgs e)
    {



        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCLOSING", typeof(string));
            var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_Closing_THR", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Closing THR tidak pernah dilakukan di periode ini');", true);

            }

            else
            {
                lnkCari_Click(sender, e);
            }


        }

        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }

        }

    }
   
}