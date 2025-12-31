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
using DocumentFormat.OpenXml.Spreadsheet;

public partial class AE_Report_ReportPajak : System.Web.UI.Page
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
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[6];
                    varx[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                    varx[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PENGGAJIANPAJAK", typeof(string));
                    varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
                    varx[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
                    dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_PENGGAJIAN_XLS", varx, "constrmf");
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
                name = ddlSP.SelectedValue;

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

                worksheet.Row(1).Height = 20.3;
                worksheet.Row(4).Height = 16.5;
                worksheet.Row(5).Height = 15.5;
                worksheet.Row(6).Height = 10.5;
                worksheet.Cell("A1").Value = dt2.ListDataset.Tables[0].Rows[0]["nama"].ToString();
                worksheet.Cell("A1").Style.Font.FontSize = 14;
                worksheet.Cell("A1").Style.Font.FontName = "Microsoft Tai Le";
                worksheet.Cell("A1").Style.Font.Bold = true;
                worksheet.Cell("A2").Value = "PERHITUNGAN PPH PASAL 21 PEGAWAI TETAP (PP 58 Tahun 2023)";
                worksheet.Cell("A2").Style.Font.FontName = "Microsoft Tai Le";
                worksheet.Cell("A2").Style.Font.FontSize = 10;
                worksheet.Cell("A2").Style.Font.Bold = true;
                worksheet.Cell("A3").Value = "PERIODE : " + ddlSP.SelectedItem.Text + ' ' + ddltahun.SelectedValue;
                worksheet.Cell("A3").Style.Font.FontName = "Microsoft Tai Le";
                worksheet.Cell("A3").Style.Font.FontSize = 10;
                worksheet.Cell("A3").Style.Font.Bold = true;
              

                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 19)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(7, 19)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Alignment.WrapText = true;
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Fill.BackgroundColor = XLColor.DarkPink;

                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(50, 1)).Style.Fill.BackgroundColor = XLColor.PinkSherbet;

                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Font.FontName = "Microsoft Tai Le";
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Font.FontSize = 8;
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).Style.Font.FontColor = XLColor.White;
                //worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(7, 19)).FreezePanes();

                worksheet.Column(1).Width = 4.31;
                worksheet.Column(2).Width = 9.46;
                worksheet.Column(3).Width = 19.85;
                worksheet.Column(4).Width = 16.15;
                worksheet.Column(5).Width = 5.31;
                worksheet.Column(6).Width = 4.31;
                worksheet.Column(7).Width = 10;
                worksheet.Column(8).Width = 11.46;
                worksheet.Column(9).Width = 11.46;
                worksheet.Column(10).Width = 11.46;
                worksheet.Column(11).Width = 12.46;
                worksheet.Column(12).Width = 9.46;
                worksheet.Column(13).Width = 9.46;
                worksheet.Column(14).Width = 9.46;
                worksheet.Column(15).Width = 11.46;
                worksheet.Column(16).Width = 9.46;
                worksheet.Column(17).Width = 9.46;
                worksheet.Column(18).Width = 5.46;
                worksheet.Column(19).Width = 11.46;
               
                

                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(6, 1)).Merge();
                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(6, 1)).Value = "NO.";
                worksheet.Range(worksheet.Cell(4, 2), worksheet.Cell(6, 2)).Merge();
                worksheet.Range(worksheet.Cell(4, 2), worksheet.Cell(6, 2)).Value = "Metode Perhitungan";
                worksheet.Range(worksheet.Cell(4, 3), worksheet.Cell(6, 3)).Merge();
                worksheet.Range(worksheet.Cell(4, 3), worksheet.Cell(6, 3)).Value = "Nama Pegawai";
                worksheet.Range(worksheet.Cell(4, 4), worksheet.Cell(6, 4)).Merge();
                worksheet.Range(worksheet.Cell(4, 4), worksheet.Cell(6, 4)).Value = "NPWP / NIK";
                worksheet.Range(worksheet.Cell(4, 5), worksheet.Cell(6, 5)).Merge();
                worksheet.Range(worksheet.Cell(4, 5), worksheet.Cell(6, 5)).Value = "Status";
                worksheet.Range(worksheet.Cell(4, 6), worksheet.Cell(6, 6)).Merge();
                worksheet.Range(worksheet.Cell(4, 6), worksheet.Cell(6, 6)).Value = "Tarif";

                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(7, 6)).Merge();
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(7, 6)).Value = "TOTAL";
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(7, 6)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                worksheet.Range(worksheet.Cell(7, 7), worksheet.Cell(7, 7)).FormulaA1 = "=SUM(G8:G507)";

                worksheet.Range(worksheet.Cell(4, 7), worksheet.Cell(5, 7)).Merge();
                worksheet.Range(worksheet.Cell(4, 7), worksheet.Cell(5, 7)).Value = "Tunjangan PPh 21";
                worksheet.Range(worksheet.Cell(6, 7), worksheet.Cell(6, 7)).Value = "1";


                worksheet.Range(worksheet.Cell(4, 8), worksheet.Cell(5, 8)).Merge();

                worksheet.Range(worksheet.Cell(4, 8), worksheet.Cell(5, 8)).Value = "Gaji Pokok (Bulan)";
                worksheet.Range(worksheet.Cell(6, 8), worksheet.Cell(6, 8)).Value = "2";

                worksheet.Range(worksheet.Cell(7, 8), worksheet.Cell(7, 8)).FormulaA1 = "=SUM(H8:H507)";


                worksheet.Range(worksheet.Cell(4, 9), worksheet.Cell(5, 9)).Merge();
                worksheet.Range(worksheet.Cell(4, 9), worksheet.Cell(5, 9)).Value = "Tunjangan Tetap Lainnya, Reward, dsb";
                worksheet.Range(worksheet.Cell(6, 9), worksheet.Cell(6, 9)).Value = "3";
                worksheet.Range(worksheet.Cell(7, 9), worksheet.Cell(7, 9)).FormulaA1 = "=SUM(I8:I507)";

                worksheet.Range(worksheet.Cell(4, 10), worksheet.Cell(5, 10)).Merge();
                worksheet.Range(worksheet.Cell(4, 10), worksheet.Cell(5, 10)).Value = "Natura & Kenikmatan Lainnya";
                worksheet.Range(worksheet.Cell(6, 10), worksheet.Cell(6, 10)).Value = "4";

                worksheet.Range(worksheet.Cell(7, 10), worksheet.Cell(7, 10)).FormulaA1 = "=SUM(J8:J507)";


                worksheet.Range(worksheet.Cell(4, 11), worksheet.Cell(5, 11)).Merge();
                worksheet.Range(worksheet.Cell(4, 11), worksheet.Cell(5, 11)).Value = "Tantiem, Bonus, Gratifikasi, Jasa Produksi & THR";
                worksheet.Range(worksheet.Cell(6, 11), worksheet.Cell(6, 11)).Value = "5";
                worksheet.Range(worksheet.Cell(7, 11), worksheet.Cell(7, 11)).FormulaA1 = "=SUM(K8:K507)";


                worksheet.Range(worksheet.Cell(4, 12), worksheet.Cell(4, 14)).Merge();
                worksheet.Range(worksheet.Cell(4, 12), worksheet.Cell(4, 14)).Value = "(+) BPJS Dibayar Perusahaan";

                worksheet.Range(worksheet.Cell(5, 12), worksheet.Cell(5, 12)).Value = "JHT";
                worksheet.Range(worksheet.Cell(7, 12), worksheet.Cell(7, 12)).FormulaA1 = "=SUM(L8:L507)";
                worksheet.Range(worksheet.Cell(5, 13), worksheet.Cell(5, 13)).Value = "JKK+JKM";
                worksheet.Range(worksheet.Cell(7, 13), worksheet.Cell(7, 13)).FormulaA1 = "=SUM(M8:M507)";
                worksheet.Range(worksheet.Cell(5, 14), worksheet.Cell(5, 14)).Value = "BPJS KES";
                worksheet.Range(worksheet.Cell(7, 14), worksheet.Cell(7, 14)).FormulaA1 = "=SUM(N8:N507)";

                worksheet.Range(worksheet.Cell(6, 12), worksheet.Cell(6, 14)).Merge();
                worksheet.Range(worksheet.Cell(6, 12), worksheet.Cell(6, 14)).Value = "6";

                worksheet.Range(worksheet.Cell(4, 15), worksheet.Cell(5, 15)).Merge();
                worksheet.Range(worksheet.Cell(4, 15), worksheet.Cell(5, 15)).Value = "Total Penghasilan Bruto";
                worksheet.Range(worksheet.Cell(6, 15), worksheet.Cell(6, 15)).Value = "7 = 1+2+3+4+5+6";
                worksheet.Range(worksheet.Cell(7, 15), worksheet.Cell(7, 15)).FormulaA1 = "=SUM(O8:O507)";


                worksheet.Range(worksheet.Cell(4, 16), worksheet.Cell(4, 17)).Merge();
                worksheet.Range(worksheet.Cell(4, 16), worksheet.Cell(4, 17)).Value = "(-) Ditanggung Karyawan";


               
                worksheet.Range(worksheet.Cell(5, 16), worksheet.Cell(5, 16)).Value = "JHT, THT";
                worksheet.Range(worksheet.Cell(7, 16), worksheet.Cell(7, 16)).FormulaA1 = "=SUM(P8:P507)";
                worksheet.Range(worksheet.Cell(5, 17), worksheet.Cell(5, 17)).Value = "JP, IP";
                worksheet.Range(worksheet.Cell(7, 17), worksheet.Cell(7, 17)).FormulaA1 = "=SUM(Q8:Q507)";

                worksheet.Range(worksheet.Cell(6, 16), worksheet.Cell(6, 17)).Merge();
                worksheet.Range(worksheet.Cell(6, 17), worksheet.Cell(6, 17)).Value = "8";


                worksheet.Range(worksheet.Cell(4, 18), worksheet.Cell(5, 18)).Merge();
                worksheet.Range(worksheet.Cell(4, 18), worksheet.Cell(5, 18)).Value = "TARIF TER";
                worksheet.Range(worksheet.Cell(6, 18), worksheet.Cell(6, 18)).Value = "9";

                worksheet.Range(worksheet.Cell(4, 19), worksheet.Cell(5, 19)).Merge();
                worksheet.Range(worksheet.Cell(4, 19), worksheet.Cell(5, 19)).Value = "PPh Pasal 21 (Bulan)";
                worksheet.Range(worksheet.Cell(6, 19), worksheet.Cell(6, 19)).Value = "10 = 7x9";
                worksheet.Range(worksheet.Cell(7, 19), worksheet.Cell(7, 19)).FormulaA1 = "=SUM(S8:S507)";
                worksheet.Range(worksheet.Cell(6, 7), worksheet.Cell(6, 19)).Style.Font.FontSize = 7;

                worksheet.Range(worksheet.Cell(6, 6), worksheet.Cell(6, 19)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                int b;
                int p;
                b = 7;
                p = 0;


                        for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                        {


                            b = b + 1;
                            p = p + 1;
                    worksheet.Range(worksheet.Cell(b, 1), worksheet.Cell(b, 19)).Style.Font.FontName = "Microsoft Tai Le";
                    worksheet.Range(worksheet.Cell(b, 1), worksheet.Cell(b, 19)).Style.Font.FontSize = 8;

                    worksheet.Cell(b, "A").Value = p;
                            worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["metode"].ToString(); 
                            worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaKry"].ToString(); ;
                            worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NPWPKTP"].ToString();
                            worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["KodePtkp"].ToString();
                            worksheet.Cell(b, "F").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["tarif"].ToString();
                            worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["tunjanganpph"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "H").Value = dt2.ListDataset.Tables[0].Rows[x]["GajisebelumPajak"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["Tunjangan"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["kenikmatan"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "K").Value = dt2.ListDataset.Tables[0].Rows[x]["THR"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["JHTP"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "M").Value = dt2.ListDataset.Tables[0].Rows[x]["JKKJKMP"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "N").Value = dt2.ListDataset.Tables[0].Rows[x]["BPJSKESP"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "O").Value = dt2.ListDataset.Tables[0].Rows[x]["Totalpenghasilan"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "P").Value = dt2.ListDataset.Tables[0].Rows[x]["JHTTHT"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "Q").Value = dt2.ListDataset.Tables[0].Rows[x]["JPIP"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "R").Value = dt2.ListDataset.Tables[0].Rows[x]["persenpajak"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "S").Value = dt2.ListDataset.Tables[0].Rows[x]["Potgajibaru"].ToString().Replace(",", ".");


                }



                //worksheet.Range(worksheet.Cell(13, 13), worksheet.Cell(b, 13)).Style.NumberFormat.Format = "_(#,##0_)";
                //worksheet.Column(2).Style.NumberFormat.Format = "dd/mm/yyyy";

                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(507, 1)).Style.Fill.BackgroundColor = XLColor.PinkSherbet;
                worksheet.Range(worksheet.Cell(8, 3), worksheet.Cell(507, 7)).Style.Fill.BackgroundColor = XLColor.PinkSherbet;
                worksheet.Range(worksheet.Cell(8, 3), worksheet.Cell(507, 7)).Style.Fill.BackgroundColor = XLColor.PinkSherbet;
                worksheet.Range(worksheet.Cell(8, 15), worksheet.Cell(507, 15)).Style.Fill.BackgroundColor = XLColor.PinkSherbet;
                worksheet.Range(worksheet.Cell(8, 18), worksheet.Cell(507, 19)).Style.Fill.BackgroundColor = XLColor.PinkSherbet;

                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(6, 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range(worksheet.Cell(4, 2), worksheet.Cell(6, 2)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(507, 2)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Range(worksheet.Cell(7, 1), worksheet.Cell(7, 19)).Style.Font.FontColor = XLColor.TitaniumYellow;

                worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
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
                        //worksheet.Column(18).Style.NumberFormat.Format = "_(#,##0_)";
                        worksheet.Column(19).Style.NumberFormat.Format = "_(#,##0_)";






                worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                worksheet.PageSetup.Margins.Top = 0.11811;
                worksheet.PageSetup.Margins.Left = 0.314961;
                worksheet.PageSetup.Margins.Right = 0.314961;
                worksheet.PageSetup.Margins.Bottom = 0.11811;

                worksheet.PageSetup.Margins.Header = 0;
                worksheet.PageSetup.Margins.Footer = 0;





                //int pakhir = 0;
                //pakhir = b + 1;
                //worksheet.Cell(pakhir, "C").Value = "Total";



               
                //worksheet.Cell(pakhir, "L").FormulaA1 = "=SUM(L8:L" + b + ")";
              
                //worksheet.Column(12).Style.NumberFormat.Format = "_(#,##0_)";

                //worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontName = "Calibri";
                //worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.FontSize = 11;
                //worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Font.Bold = true;
                //worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 12)).Style.Fill.BackgroundColor = XLColor.PaleAqua;



                string report = "";
                //if (ddlproduk.SelectedValue == "3C")
                //{
                report = "Penggajian_Pajak" + ddlcabang.SelectedValue + "_" + ddlSP.SelectedValue + "_" + ddltahun.SelectedValue + ".xlsx";
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