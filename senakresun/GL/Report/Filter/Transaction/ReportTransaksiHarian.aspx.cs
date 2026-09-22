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
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using ClosedXML.Excel;

public partial class GL_Report_ReportTransaksiHarian : System.Web.UI.Page
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

                 BindGridEntity();
                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttglhariini.Text = xtanggal.ToString("yyyy-MM-dd");

            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
        Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetUserJournal", Paramx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
        ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));


        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[2];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MFT_GetBRANCH", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountawal, "None", "");
        ddlaccountawal.SelectedValue = SessBranchID;


        DefaultClass.Global.SqlHelper[] Paramss = new DefaultClass.Global.SqlHelper[0];
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference_TypeJournal", Paramss, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypejournalawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypejournalakhir, "None", "");
        ddltypejournalawal.Items.Insert(0, new ListItem("ALL TYPE", "-"));
        ddltypejournalakhir.Items.Insert(0, new ListItem("-", "-"));


        DefaultClass.Global.SqlHelper[] Paramsd = new DefaultClass.Global.SqlHelper[2];
        Paramsd[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
        Paramsd[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKODECABANG", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.KP_REPORTING_CL", Paramsd, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kodecabang asc";
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "namacabang";
        ddlcabang.DataValueField = "kodecabang";
        ddlcabang.DataBind();
        ddlcabang.Items.Insert(0, new ListItem("ALL", "ALL"));



    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {




                XLWorkbook wb = new XLWorkbook();

                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[7];
                Paramsx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsx[1] = gb.CreateSPParameter("@TypeTrnawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypejournalawal.SelectedValue, typeof(string));
                Paramsx[2] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglhariini.Text, typeof(DateTime));
                Paramsx[3] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglhariini.Text, typeof(DateTime));
                Paramsx[4] = gb.CreateSPParameter("@typeposting", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlrpttype.SelectedValue, typeof(string));
                Paramsx[5] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(String));
                Paramsx[6] = gb.CreateSPParameter("@userteller", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(String));
               
                dt2.ListDataset = gb.GetDataJustOutPut("GL_STD_RPT_003_TglHarian_X", Paramsx, "constrmf");
                dt2.ListDataset.Tables[0].DefaultView.Sort = " TglEntry ASC ";

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
                //name = txtEnd.Text;

                    DateTime now = DateTime.Parse(txttglhariini.Text);

                var worksheet = wb.Worksheets.Add(txttglhariini.Text);
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



                    worksheet.Cell(7, 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 9).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 10).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell("A1").Value = "LAPORAN TRANSAKSI JOURNAL HARIAN ";
                    worksheet.Cell("A1").Style.Font.FontSize = 9;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Value = "Tanggal";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 9;
                    worksheet.Cell("A3").Value = "Cabang";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 9;


                    worksheet.Cell("C2").Value = "'" + now.ToString("dd-MM-yyyy"); 

                    worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C2").Style.Font.FontSize = 9;
                    worksheet.Cell("C3").Value = ddlaccountawal.SelectedItem.Text;
                    worksheet.Cell("C3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C3").Style.Font.FontSize = 9;


                    worksheet.Cell("A7").Value = "No.";
                    worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A7").Style.Font.FontSize = 9;

                    worksheet.Cell("B7").Value = "Nobukti";
                    worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B7").Style.Font.FontSize = 9;


                    worksheet.Cell("C7").Value = "Tgl Trn";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;

                    worksheet.Cell("D7").Value = "Kode GL";
                    worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("D7").Style.Font.FontSize = 9;

                    worksheet.Cell("E7").Value = "Nama GL";
                    worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("E7").Style.Font.FontSize = 9;

                    worksheet.Cell("F7").Value = "Keterangan";
                    worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("F7").Style.Font.FontSize = 9;

                    worksheet.Cell("G7").Value = "Jlh Debit";
                    worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("G7").Style.Font.FontSize = 9;

                    worksheet.Cell("H7").Value = "Jlh Kredit";
                    worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("H7").Style.Font.FontSize = 9;

                    worksheet.Cell("I7").Value = "Tgl Entry";
                    worksheet.Cell("I7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("I7").Style.Font.FontSize = 9;


                    worksheet.Cell("J7").Value = "User Entry";
                    worksheet.Cell("J7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("J7").Style.Font.FontSize = 9;


                    int b;
                    int p;
                    b = 7;
                    p = 0;


                    for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                    {


                        b = b + 1;
                        p = p + 1;

                        worksheet.Cell(b, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                        worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoBukti"].ToString();
                        worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "C").Value =DateTime.Parse(dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                        worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "C").Style.Font.FontSize = 9;
                        //worksheet.Cell("C2").Value = "'" + now.ToString("dd-MM-yyyy");

                        worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeGl"].ToString();
                        worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "D").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["NamaGl"].ToString();
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["Keterangan"].ToString();
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;



                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JlhDebit"].ToString();
                        worksheet.Cell(b, "G").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "G").Style.Font.FontSize = 9;



                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JlhKredit"].ToString();
                        worksheet.Cell(b, "H").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "H").Style.Font.FontSize = 9;




                        worksheet.Cell(b, "I").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["TglEntry"].ToString();
                        worksheet.Cell(b, "I").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "I").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "J").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeUser"].ToString();
                        worksheet.Cell(b, "J").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "J").Style.Font.FontSize = 9;



                        worksheet.Row(b).Height = 12;




                    }

                    int pakhir = 0;

                    pakhir = 8 + p;

                    worksheet.Cell(pakhir, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(pakhir, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell(pakhir, "B").Value = "Total";
                    worksheet.Cell(pakhir, "B").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "B").Style.Font.FontSize = 9;


                    worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G8:G" + b + ")";
                    worksheet.Cell(pakhir, "H").FormulaA1 = "=SUM(H8:H" + b + ")";





                    worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";

                    //worksheet.Column(13).Style.NumberFormat.Format = "dd/mm/yyyy";

                    //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                    worksheet.Column(1).Width = 4;
                    worksheet.Column(2).Width = 19.43;
                    worksheet.Column(3).Width = 10.57;
                    worksheet.Column(4).Width = 10.86;
                    worksheet.Column(5).Width = 10.86;
                    worksheet.Column(6).Width = 25.14;
                    worksheet.Column(7).Width = 15.14;
                    worksheet.Column(8).Width = 15.14;
                    worksheet.Column(9).Width = 15.57;
                    worksheet.Column(10).Width = 10.57;







                    worksheet.Row(1).Height = 12;
                    worksheet.Row(2).Height = 12;

                    worksheet.Row(3).Height = 12;
                    worksheet.Row(4).Height = 12;

                    worksheet.Row(5).Height = 12;
                    worksheet.Row(6).Height = 12;
                    worksheet.Row(7).Height = 12;
                    worksheet.Row(8).Height = 12;
                    worksheet.Row(9).Height = 12;
                    worksheet.Row(10).Height = 12;

                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;

                    // Set each particular border for the cell.

                    //int ro = 1;
                    //int co = 8;
                    ////worksheet.Cell(pakhir, "E").

                    //worksheet.Cell(ro, co).Style.Border.BottomBorder = XLBorderStyleValues.Thin;













                }

          
                string report = "";

                report = "Report Transaksi Journal " + txttglhariini.Text + ".xlsx";

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


    public void btnsearchdata_Click(object sender, EventArgs e)
    {




        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[7];
        Paramsx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Paramsx[1] = gb.CreateSPParameter("@TypeTrnawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypejournalawal.SelectedValue, typeof(string));
        Paramsx[2] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglhariini.Text, typeof(DateTime));
        Paramsx[3] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglhariini.Text, typeof(DateTime));
        Paramsx[4] = gb.CreateSPParameter("@typeposting", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlrpttype.SelectedValue, typeof(string));
        Paramsx[5] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(String));
        Paramsx[6] = gb.CreateSPParameter("@userteller", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("GL_STD_RPT_003_TglHarian_X", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TglEntry desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
     
    }
    decimal xjlhdebit = 0, xjlhkredit = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JlhDebit"));
            xjlhkredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JlhKredit"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbldebit = (Label)e.Row.FindControl("lbldebit");
            lbldebit.Text = string.Format("{0:N0}", xjlhdebit);

            Label lblkredit = (Label)e.Row.FindControl("lblkredit");
            lblkredit.Text = string.Format("{0:N0}", xjlhkredit);


        }
    }


}