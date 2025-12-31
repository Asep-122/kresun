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

public partial class AE_Report_Reportfeebulanan : System.Web.UI.Page
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
                ddlsp_SelectedIndexChanged(sender, e);
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
        ambilBulan();
        ambilTahun();

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

            ge.ListDataset = gb.GetDataJustOutPut("AE_STD_FEE_REWARD_XLS", var, "constrmf");
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
                name = "Fee_Reward_" + ddlcabang.SelectedValue + "_" + ddlSP.SelectedItem.Text + "_" + ddltahun.SelectedValue;

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



                
                worksheet.Cell("A1").Value = "LAPORAN FEE & REWARD";
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


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
           
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Font.FontName = "Calibri";
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Font.FontSize = 11;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Fill.BackgroundColor = XLColor.PaleAqua;




                worksheet.Column(1).Width = 13.89;
                worksheet.Column(4).Width = 18.78;
                worksheet.Column(5).Width = 21.11;
                worksheet.Column(6).Width = 14.11;
                
                worksheet.Row(8).Height = 46.20;
                worksheet.Row(9).Height = 46.20;
                worksheet.Column(2).Width = 29.33;
                worksheet.Column(3).Width = 29.33;
                worksheet.Column(7).Width = 19.78;
                worksheet.Column(8).Width = 17.11;
                worksheet.Column(9).Width = 17.11;
                worksheet.Column(10).Width = 17.11;
                worksheet.Column(11).Width = 17.11;
                worksheet.Column(12).Width = 17.11;
                worksheet.Column(13).Width = 17.11;
                worksheet.Column(14).Width = 17.11;
                worksheet.Column(15).Width = 17.11;
                worksheet.Column(16).Width = 17.11;
                worksheet.Column(17).Width = 17.11;


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 1)).Merge();
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 1)).Value="NO.";

                worksheet.Range(worksheet.Cell(7, 2), worksheet.Cell(9, 2)).Merge();
                worksheet.Range(worksheet.Cell(7, 2), worksheet.Cell(9, 2)).Value = "NIK KARYAWAN";

                worksheet.Range(worksheet.Cell(7, 3), worksheet.Cell(9, 3)).Merge();
                worksheet.Range(worksheet.Cell(7, 3), worksheet.Cell(9, 3)).Value = "NAMA LENGKAP";
               

                
                worksheet.Range(worksheet.Cell(7, 4), worksheet.Cell(9, 4)).Merge();
                worksheet.Range(worksheet.Cell(7, 4), worksheet.Cell(9, 4)).Value = "JABATAN";


                worksheet.Range(worksheet.Cell(7, 5), worksheet.Cell(9, 5)).Merge();
                worksheet.Range(worksheet.Cell(7, 5), worksheet.Cell(9, 5)).Value = "TOTAL  AVERAGE COLLECTIONS & KPI MANAGEMENT";




                worksheet.Range(worksheet.Cell(7, 6), worksheet.Cell(9, 6)).Value = "FEE MARKETING (BM)";
                worksheet.Range(worksheet.Cell(7, 6), worksheet.Cell(9, 6)).Merge();

                worksheet.Range(worksheet.Cell(7, 7), worksheet.Cell(9, 7)).Merge();
                worksheet.Range(worksheet.Cell(7, 7), worksheet.Cell(9, 7)).Value = "TOTAL BM";

                worksheet.Range(worksheet.Cell(7, 8), worksheet.Cell(7, 12)).Merge();
                worksheet.Range(worksheet.Cell(7, 8), worksheet.Cell(7, 12)).Value = "REWARD";

                worksheet.Range(worksheet.Cell(8, 8), worksheet.Cell(9, 8)).Merge();
                worksheet.Range(worksheet.Cell(8, 8), worksheet.Cell(9, 8)).Value = "P&L  >100";

                worksheet.Range(worksheet.Cell(8, 9), worksheet.Cell(9, 9)).Merge();
                worksheet.Range(worksheet.Cell(8, 9), worksheet.Cell(9, 9)).Value = "NEW MEMBERS";

                worksheet.Range(worksheet.Cell(8, 10), worksheet.Cell(9, 10)).Merge();
                worksheet.Range(worksheet.Cell(8, 10), worksheet.Cell(9, 10)).Value = ">K M K";

                worksheet.Range(worksheet.Cell(8, 11), worksheet.Cell(9, 11)).Merge();
                worksheet.Range(worksheet.Cell(8, 11), worksheet.Cell(9, 11)).Value = "Tunggakan Pokok (TP)";

                worksheet.Range(worksheet.Cell(8, 12), worksheet.Cell(9, 12)).Merge();
                worksheet.Range(worksheet.Cell(8, 12), worksheet.Cell(9, 12)).Value = "SM/KU TERBAIK";



                worksheet.Range(worksheet.Cell(7, 13), worksheet.Cell(9, 13)).Merge();
                worksheet.Range(worksheet.Cell(7, 13), worksheet.Cell(9, 13)).Value = "TOTAL REWARD";

                worksheet.Range(worksheet.Cell(7, 14), worksheet.Cell(9, 14)).Merge();
                worksheet.Range(worksheet.Cell(7, 14), worksheet.Cell(9, 14)).Value = "Total BM + Reward yang diterima";

                worksheet.Range(worksheet.Cell(7, 15), worksheet.Cell(9, 15)).Merge();
                worksheet.Range(worksheet.Cell(7, 15), worksheet.Cell(9, 15)).Value = "Total New Members Harian yang diterima";

                worksheet.Range(worksheet.Cell(7, 16), worksheet.Cell(9, 16)).Merge();
                worksheet.Range(worksheet.Cell(7, 16), worksheet.Cell(9, 16)).Value = "Pengurangan Fee";


                worksheet.Range(worksheet.Cell(7, 17), worksheet.Cell(9, 17)).Merge();
                worksheet.Range(worksheet.Cell(7, 17), worksheet.Cell(9, 17)).Value = "Penambahan Fee";


                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(9, 17)).Style.Alignment.WrapText = true;


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
                    worksheet.Range(worksheet.Cell(b, 1), worksheet.Cell(b, 17)).Style.Fill.BackgroundColor = XLColor.AntiFlashWhite;
                    
                    
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                    varx[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                    varx[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                    varx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                    varx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    varx[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "FEEREWARD", typeof(string));
                    varx[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[6] = gb.CreateSPParameter("@bagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ge.ListDataset.Tables[0].Rows[i][0].ToString(), typeof(string));
                    dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_FEE_REWARD_XLS", varx, "constrmf");
                    if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                    {

                        for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                        {


                            b = b + 1;
                            p = p + 1;

                           
                            worksheet.Cell(b, "A").Value = p;


                            worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["nik"].ToString();
                            worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaKaryawan"].ToString();
                            worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KetJab"].ToString();
                            worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["averagekpi"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["feepersen"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["totalbm"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "H").Value = dt2.ListDataset.Tables[0].Rows[x]["pl"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["newmembers"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["kmk"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "K").Value = dt2.ListDataset.Tables[0].Rows[x]["Tunggakan"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["smkuterbaik"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "M").Value = dt2.ListDataset.Tables[0].Rows[x]["totalreward"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "N").Value = dt2.ListDataset.Tables[0].Rows[x]["totalbmreward"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "O").Value = dt2.ListDataset.Tables[0].Rows[x]["newmembersharian"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "P").Value = dt2.ListDataset.Tables[0].Rows[x]["penguranganfee"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "Q").Value = dt2.ListDataset.Tables[0].Rows[x]["penambahanfee"].ToString().Replace(",", ".");



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

                        worksheet.Range(worksheet.Cell(12, 5), worksheet.Cell(b, 5)).Style.NumberFormat.Format = "_(#,##0_)";
                        worksheet.Range(worksheet.Cell(12, 7), worksheet.Cell(b, 17)).Style.NumberFormat.Format = "_(#,##0_)";

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
                worksheet.Cell(pakhir, "C").Value = "Total";



                worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G12:G" + b + ")";
                worksheet.Cell(pakhir, "H").FormulaA1 = "=SUM(H12:H" + b + ")";
                worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I12:I" + b + ")";
                worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J12:J" + b + ")";
                worksheet.Cell(pakhir, "K").FormulaA1 = "=SUM(K12:K" + b + ")";
                worksheet.Cell(pakhir, "L").FormulaA1 = "=SUM(L12:L" + b + ")";
                worksheet.Cell(pakhir, "M").FormulaA1 = "=SUM(M12:M" + b + ")";
                worksheet.Cell(pakhir, "N").FormulaA1 = "=SUM(N12:N" + b + ")";
                worksheet.Cell(pakhir, "O").FormulaA1 = "=SUM(O12:O" + b + ")";
                worksheet.Cell(pakhir, "P").FormulaA1 = "=SUM(P12:P" + b + ")";
                worksheet.Cell(pakhir, "Q").FormulaA1 = "=SUM(Q12:Q" + b + ")";

                worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(11).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(12).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(13).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(14).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(15).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(16).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(17).Style.NumberFormat.Format = "_(#,##0_)";

                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 17)).Style.Font.FontName = "Calibri";
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 17)).Style.Font.FontSize = 11;
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 17)).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 17)).Style.Fill.BackgroundColor = XLColor.PaleAqua;



                string report = "";
                    //if (ddlproduk.SelectedValue == "3C")
                    //{
                        report = "FEE_REWARD_" + ddlcabang.SelectedValue + "_" + ddlSP.SelectedItem.Text + "_" + ddltahun.SelectedValue + ".xlsx";
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
    //protected void btnSerach_Clickx(object sender, EventArgs e)
    //{
    //    //ReportDocument rprt = new ReportDocument();

    //    try
    //    {




    //        XLWorkbook wb = new XLWorkbook();

    //        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
    //        var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
    //        var[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
    //        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
    //        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
    //        var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PENGGAJIAN", typeof(string));
    //        var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                     
    //        dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_PENGGAJIAN_XLS", var, "constrmf");
    //        if (dt2.ListDataset.Tables[0].Rows.Count > 0)
    //        {
    //            string huruf = "", deltax;
    //            string huruf2, huruf1;

    //            huruf = "";
    //            int abjadlanjut = 65;
    //            int abjadawal = 64;
    //            string strAlphaawal = "";
    //            string strAlphalanjut = "";
    //            string alpha1 = "";
    //            string alpha2 = "";
    //            int x1 = 1;


    //            string name;
    //            //name=dt.Rows[i][0].;
    //            name = "Gaji_" + ddlcabang.SelectedValue + "_" + ddlSP.SelectedItem.Text + "_" + ddltahun.SelectedValue;

    //            //DateTime now = DateTime.Parse(txttglhariini.Text);

    //            var worksheet = wb.Worksheets.Add(name);
    //            worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
    //            worksheet.PageSetup.Margins.Top = 0.4;
    //            worksheet.PageSetup.Margins.Left = 0.3;
    //            worksheet.PageSetup.Margins.Right = 0.3;
    //            worksheet.PageSetup.Margins.Bottom = 0.5;

    //            worksheet.PageSetup.Margins.Header = 0.3;
    //            worksheet.PageSetup.Margins.Footer = 0.3;
    //            worksheet.RowHeight = 13;
    //            string hurufx = "";
    //            int abjadlanjutx = 65;
    //            int abjadawalx = 64;
    //            string strAlphaawalx = "";
    //            string strAlphalanjutx = "";
    //            string alpha1x = "";
    //            string alpha2x = "";

    //            string xnama;
    //            string groupx;
    //            string noperkiraan;
    //            string ku_mingguan;
    //            //string tahunini



    //            worksheet.Cell(7, 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 4).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 5).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 7).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 9).Style.Border.TopBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 10).Style.Border.TopBorder = XLBorderStyleValues.Thin;

    //            worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

    //            worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

    //            worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(7, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;

    //            worksheet.Cell("A1").Value = "LAPORAN PENGGAJIAN";
    //            worksheet.Cell("A1").Style.Font.FontSize = 9;
    //            worksheet.Cell("A1").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("A2").Value = "Bulan/Tahun";
    //            worksheet.Cell("A2").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("A2").Style.Font.FontSize = 9;
    //            worksheet.Cell("A3").Value = "Koperasi";
    //            worksheet.Cell("A3").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("A3").Style.Font.FontSize = 9;
    //            worksheet.Cell("A4").Value = "Area";
    //            worksheet.Cell("A4").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("A4").Style.Font.FontSize = 9;
    //            worksheet.Cell("A5").Value = "Cabang";
    //            worksheet.Cell("A5").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("A5").Style.Font.FontSize = 9;

    //            worksheet.Cell("C2").Value = "'" + ddlSP.SelectedItem.Text + "/" + ddltahun.SelectedValue;

    //            worksheet.Cell("C2").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("C2").Style.Font.FontSize = 9;
    //            worksheet.Cell("C3").Value = dt2.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString();
    //            worksheet.Cell("C4").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("C4").Style.Font.FontSize = 9;
    //            worksheet.Cell("C4").Value = dt2.ListDataset.Tables[0].Rows[0]["nama_area"].ToString();
    //            worksheet.Cell("C5").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("C5").Style.Font.FontSize = 9;
    //            worksheet.Cell("C5").Value = dt2.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString();

    //            worksheet.Cell("A7").Value = "No.";
    //            worksheet.Cell("A7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("A7").Style.Font.FontSize = 9;

    //            worksheet.Cell("B7").Value = "Nobukti";
    //            worksheet.Cell("B7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("B7").Style.Font.FontSize = 9;


    //            worksheet.Cell("C7").Value = "Tgl Trn";
    //            worksheet.Cell("C7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("C7").Style.Font.FontSize = 9;

    //            worksheet.Cell("D7").Value = "Kode GL";
    //            worksheet.Cell("D7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("D7").Style.Font.FontSize = 9;

    //            worksheet.Cell("E7").Value = "Nama GL";
    //            worksheet.Cell("E7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("E7").Style.Font.FontSize = 9;

    //            worksheet.Cell("F7").Value = "Keterangan";
    //            worksheet.Cell("F7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("F7").Style.Font.FontSize = 9;

    //            worksheet.Cell("G7").Value = "Jlh Debit";
    //            worksheet.Cell("G7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("G7").Style.Font.FontSize = 9;

    //            worksheet.Cell("H7").Value = "Jlh Kredit";
    //            worksheet.Cell("H7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("H7").Style.Font.FontSize = 9;

    //            worksheet.Cell("I7").Value = "Tgl Entry";
    //            worksheet.Cell("I7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("I7").Style.Font.FontSize = 9;


    //            worksheet.Cell("J7").Value = "User Entry";
    //            worksheet.Cell("J7").Style.Font.FontName = "Calibri";
    //            worksheet.Cell("J7").Style.Font.FontSize = 9;


    //            int b;
    //            int p;
    //            b = 7;
    //            p = 0;


    //            for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
    //            {


    //                b = b + 1;
    //                p = p + 1;

    //                worksheet.Cell(b, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


    //                worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;

    //                worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //                worksheet.Cell(b, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

    //                worksheet.Cell(b, "A").Value = p;
    //                worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "A").Style.Font.FontSize = 9;


    //                worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoBukti"].ToString();
    //                worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "B").Style.Font.FontSize = 9;

    //                worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString();
    //                worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "C").Style.Font.FontSize = 9;


    //                worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeGl"].ToString();
    //                worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "D").Style.Font.FontSize = 9;

    //                worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["NamaGl"].ToString();
    //                worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "E").Style.Font.FontSize = 9;


    //                worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["Keterangan"].ToString();
    //                worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "F").Style.Font.FontSize = 9;



    //                ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JlhDebit"].ToString();
    //                worksheet.Cell(b, "G").Value = ku_mingguan.Replace(",", ".");
    //                worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "G").Style.Font.FontSize = 9;



    //                ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JlhKredit"].ToString();
    //                worksheet.Cell(b, "H").Value = ku_mingguan.Replace(",", ".");
    //                worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "H").Style.Font.FontSize = 9;




    //                worksheet.Cell(b, "I").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["TglEntry"].ToString();
    //                worksheet.Cell(b, "I").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "I").Style.Font.FontSize = 9;


    //                worksheet.Cell(b, "J").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeUser"].ToString();
    //                worksheet.Cell(b, "J").Style.Font.FontName = "Calibri";
    //                worksheet.Cell(b, "J").Style.Font.FontSize = 9;



    //                worksheet.Row(b).Height = 12;




    //            }

    //            int pakhir = 0;

    //            pakhir = 8 + p;

    //            worksheet.Cell(pakhir, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


    //            worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

    //            worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;

    //            worksheet.Cell(pakhir, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
    //            worksheet.Cell(pakhir, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;


    //            worksheet.Cell(pakhir, "B").Value = "Total";
    //            worksheet.Cell(pakhir, "B").Style.Font.FontName = "Calibri";
    //            worksheet.Cell(pakhir, "B").Style.Font.FontSize = 9;


    //            worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G8:G" + b + ")";
    //            worksheet.Cell(pakhir, "H").FormulaA1 = "=SUM(H8:H" + b + ")";





    //            worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";
    //            worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";

    //            //worksheet.Column(13).Style.NumberFormat.Format = "dd/mm/yyyy";

    //            //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
    //            //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

    //            //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



    //            worksheet.Column(1).Width = 4;
    //            worksheet.Column(2).Width = 19.43;
    //            worksheet.Column(3).Width = 10.57;
    //            worksheet.Column(4).Width = 10.86;
    //            worksheet.Column(5).Width = 10.86;
    //            worksheet.Column(6).Width = 25.14;
    //            worksheet.Column(7).Width = 15.14;
    //            worksheet.Column(8).Width = 15.14;
    //            worksheet.Column(9).Width = 15.57;
    //            worksheet.Column(10).Width = 10.57;







    //            worksheet.Row(1).Height = 12;
    //            worksheet.Row(2).Height = 12;

    //            worksheet.Row(3).Height = 12;
    //            worksheet.Row(4).Height = 12;

    //            worksheet.Row(5).Height = 12;
    //            worksheet.Row(6).Height = 12;
    //            worksheet.Row(7).Height = 12;
    //            worksheet.Row(8).Height = 12;
    //            worksheet.Row(9).Height = 12;
    //            worksheet.Row(10).Height = 12;

    //            worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
    //            worksheet.PageSetup.Margins.Top = 0.11811;
    //            worksheet.PageSetup.Margins.Left = 0.314961;
    //            worksheet.PageSetup.Margins.Right = 0.314961;
    //            worksheet.PageSetup.Margins.Bottom = 0.11811;

    //            worksheet.PageSetup.Margins.Header = 0;
    //            worksheet.PageSetup.Margins.Footer = 0;

    //            // Set each particular border for the cell.

    //            //int ro = 1;
    //            //int co = 8;
    //            ////worksheet.Cell(pakhir, "E").

    //            //worksheet.Cell(ro, co).Style.Border.BottomBorder = XLBorderStyleValues.Thin;













    //        }


    //        string report = "";

    //        report = "Report Transaksi Journal " + txttglhariini.Text + ".xlsx";

    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.Charset = "";
    //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //        Response.AddHeader("content-disposition", "attachment;filename=" + report + "");
    //        using (MemoryStream MyMemoryStream = new MemoryStream())
    //        {
    //            wb.SaveAs(MyMemoryStream);
    //            MyMemoryStream.WriteTo(Response.OutputStream);
    //            Response.Flush();
    //            Response.End();
    //        }








    //    }

    //    catch (Exception X)
    //    {
    //        if (X.Message != "Thread was being aborted.")
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        }
    //        else
    //        {

    //            //rprt.Close();
    //            //rprt.Dispose();
    //        }

    //    }



    //}





    public void btnSerach_Click(object sender, EventArgs e)
    {



        try
        {


            //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            //var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
            //var[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
            //var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
            //var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            //var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCLOSING", typeof(string));
            //var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("AE_Closing_Fee_Reward", var, "constrmf");
            //if (ge.ListDataset.Tables[0].Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Closing Fee dan Reward tidak pernah dilakukan di bulan dan periode ini');", true);

            //}

            //else
            //{
                lnkCari_Click(sender, e);
            //}


        }

        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }

        }

    }
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSP.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTANGGAL", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_Closing_Gaji", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {


                
                //txtStart.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglAbsAwal"].ToString()).ToString("dd-MM-yyyy");
                //txtEnd.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglAbsAkhir"].ToString()).ToString("dd-MM-yyyy");
            }
            else
            {
                //txtStart.Text = "";
                //txtEnd.Text = "";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), System.IO.Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        //DateTime now = DateTime.Now;

        //Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy") +  " s/d " + xdate.ToString("dd-MM-yyyy");
    }

}