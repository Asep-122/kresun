using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ClosedXML.Excel;

public partial class Report_viewlisttagihan : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    GlobalEntities dt2 = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                datagrid.Visible = false;
                divlist.Visible = false;
                divrekap.Visible = false;
                GRDataRekap.Visible = false;
                GRDataList.Visible = false;

                DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");

                ddlproduk.SelectedValue = "3C";
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_X", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();
                ddlcabang.SelectedValue = SessBranchID;

                // COMBO MASTER MO
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_REPORT_KODEMARKETING", var1, "constrmf");

                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "namaAO";
                ddlmarketing.DataValueField = "NIK";
                ddlmarketing.DataBind();

                txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlmarketing_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlreport.SelectedValue == "1")
        {
            ddlmarketing.Enabled = true;
            datagrid.Visible = false;
            divlist.Visible = false;
            divrekap.Visible = false;

            GRDataList.Visible = false;
            GRDataRekap.Visible = false;

            GRDataList.DataSource = null;
            GRDataList.DataBind();
            GRDataRekap.DataSource = null;
            GRDataRekap.DataBind();
        }
        else
        {
            ddlmarketing.Enabled = false;
            datagrid.Visible = false;
            divlist.Visible = false;
            divrekap.Visible = false;

            GRDataList.Visible = false;
            GRDataRekap.Visible = false;

            GRDataList.DataSource = null;
            GRDataList.DataBind();
            GRDataRekap.DataSource = null;
            GRDataRekap.DataBind();
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlreport.SelectedValue == "1")
            {
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTTAGIHANDETAIL", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AR_TAGIHAN", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {
                    XLWorkbook wb = new XLWorkbook();
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
                    name = ddlmarketing.SelectedItem.Text;


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
                    worksheet.Cell(7, 11).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 12).Style.Border.TopBorder = XLBorderStyleValues.Thin;

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
                    worksheet.Cell(7, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 12).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

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
                    worksheet.Cell(7, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 12).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


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
                    worksheet.Cell(7, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 12).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell("A1").Value = "LAPORAN LIST TAGIHAN MARKETING ";
                    worksheet.Cell("A1").Style.Font.FontSize = 9;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Value = "Periode";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 9;
                    worksheet.Cell("A3").Value = "Produk";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 9;
                    worksheet.Cell("A4").Value = "Cabang";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 9;
                    worksheet.Cell("A5").Value = "Marketing";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 9;

                    worksheet.Cell("C2").Value = txtTglL.Text;
                    worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C2").Style.Font.FontSize = 9;
                    worksheet.Cell("C3").Value = ddlproduk.SelectedItem.Text;
                    worksheet.Cell("C3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C3").Style.Font.FontSize = 9;
                    worksheet.Cell("C4").Value = ddlcabang.SelectedItem.Text;
                    worksheet.Cell("C4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C4").Style.Font.FontSize = 9;
                    worksheet.Cell("C5").Value = ddlmarketing.SelectedItem.Text;
                    worksheet.Cell("C5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C5").Style.Font.FontSize = 9;


                    worksheet.Cell("A7").Value = "No.";
                    worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A7").Style.Font.FontSize = 9;

                    worksheet.Cell("B7").Value = "No. Rekening";
                    worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B7").Style.Font.FontSize = 9;


                    worksheet.Cell("C7").Value = "Nama Nasabah";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;

                    worksheet.Cell("D7").Value = "Hari";
                    worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("D7").Style.Font.FontSize = 9;

                    worksheet.Cell("E7").Value = "Plafon";
                    worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("E7").Style.Font.FontSize = 9;

                    worksheet.Cell("F7").Value = "Saldo Akhir";
                    worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("F7").Style.Font.FontSize = 9;

                    worksheet.Cell("G7").Value = "Terbayar";
                    worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("G7").Style.Font.FontSize = 9;

                    worksheet.Cell("H7").Value = "Tunggakan";
                    worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("H7").Style.Font.FontSize = 9;

                    worksheet.Cell("I7").Value = "Tung.Pokok";
                    worksheet.Cell("I7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("I7").Style.Font.FontSize = 9;

                    worksheet.Cell("J7").Value = "Tung.Bunga";
                    worksheet.Cell("J7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("J7").Style.Font.FontSize = 9;

                    worksheet.Cell("K7").Value = "Total Tung";
                    worksheet.Cell("K7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("K7").Style.Font.FontSize = 9;

                    worksheet.Cell("L7").Value = "Tgl JTH";
                    worksheet.Cell("L7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("L7").Style.Font.FontSize = 9;


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
                        worksheet.Cell(b, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 12).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


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
                        worksheet.Cell(b, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 12).Style.Border.RightBorder = XLBorderStyleValues.Thin;

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
                        worksheet.Cell(b, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 12).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_rekening"].ToString();
                        worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                        worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["hari_tagih"].ToString();
                        worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "D").Style.Font.FontSize = 9;



                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["plafon"].ToString();
                        worksheet.Cell(b, "E").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;


                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["bakidb"].ToString();
                        worksheet.Cell(b, "F").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["terbayar"].ToString();
                        worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "G").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "H").Value = dt2.ListDataset.Tables[0].Rows[x]["tunggakan"].ToString();
                        worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "H").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_pokok"].ToString();
                        worksheet.Cell(b, "I").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "I").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "I").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_bunga"].ToString();
                        worksheet.Cell(b, "J").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "J").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "J").Style.Font.FontSize = 9;


                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["total_tung"].ToString();
                        worksheet.Cell(b, "K").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "K").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "K").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["tgl_due"].ToString();
                        worksheet.Cell(b, "L").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "L").Style.Font.FontSize = 9;

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
                    worksheet.Cell(pakhir, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 12).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


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
                    worksheet.Cell(pakhir, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 12).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

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
                    worksheet.Cell(pakhir, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 12).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(pakhir, "D").Value = "Total";
                    worksheet.Cell(pakhir, "D").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "D").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "E").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "E").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "F").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "F").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "I").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "I").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "J").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "J").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "K").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "K").Style.Font.FontSize = 9;


                    worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + b + ")";
                    worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + b + ")";
                    worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I8:I" + b + ")";
                    worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J8:J" + b + ")";
                    worksheet.Cell(pakhir, "K").FormulaA1 = "=SUM(K8:K" + b + ")";





                    worksheet.Column(5).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(11).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(12).Style.NumberFormat.Format = "dd/mm/yyyy";

                    //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                    worksheet.Column(1).Width = 4;
                    worksheet.Column(2).Width = 11;
                    worksheet.Column(3).Width = 8;
                    worksheet.Column(4).Width = 5.71;
                    worksheet.Column(5).Width = 9.43;
                    worksheet.Column(6).Width = 9.71;
                    worksheet.Column(7).Width = 2.14;
                    worksheet.Column(8).Width = 2.57;
                    worksheet.Column(9).Width = 8.43;
                    worksheet.Column(10).Width = 8.14;
                    worksheet.Column(11).Width = 8.14;
                    worksheet.Column(12).Width = 8.14;







                    worksheet.Row(1).Height = 12;
                    worksheet.Row(2).Height = 12;

                    worksheet.Row(3).Height = 12;
                    worksheet.Row(4).Height = 12;

                    worksheet.Row(5).Height = 12;
                    worksheet.Row(6).Height = 12;
                    worksheet.Row(7).Height = 12;
                    worksheet.Row(8).Height = 12;
                    worksheet.Row(9).Height = 12;


                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;




                    string report = "";
                    if (ddlproduk.SelectedValue == "3C")
                    {
                        report = "Report List Tagihan Mingguan " + ddlmarketing.SelectedItem.Text.ToString() + ".xlsx";
                    }
                    else if (ddlproduk.SelectedValue == "3B")
                    {
                        report = "Report List Tagihan Bulanan " + ddlmarketing.SelectedItem.Text.ToString() + ".xlsx";
                    }



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




                    // Set each particular border for the cell.

                    //int ro = 1;
                    //int co = 8;
                    ////worksheet.Cell(pakhir, "E").

                    //worksheet.Cell(ro, co).Style.Border.BottomBorder = XLBorderStyleValues.Thin;





                }
            }
            else
            {
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());


                XLWorkbook wb = new XLWorkbook();
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

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, "ALL", typeof(string));
                varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAGIHANREKAP", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, "ALL", typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AR_TAGIHAN", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {
                    string name;
                    //name=dt.Rows[i][0].;
                    name = "REKAP TAGIHAN";


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



                    worksheet.Cell(7, 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell("A1").Value = "LAPORAN LIST REKAP TAGIHAN MARKETING ";
                    worksheet.Cell("A1").Style.Font.FontSize = 9;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Value = "Periode";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 9;
                    worksheet.Cell("A3").Value = "Produk";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 9;
                    worksheet.Cell("A4").Value = "Cabang";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 9;
                    worksheet.Cell("A5").Value = "Marketing";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 9;



                    worksheet.Cell("C2").Value = "'" + txtTglL.Text;

                    worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C2").Style.Font.FontSize = 9;
                    worksheet.Cell("C3").Value = "All - All";
                    worksheet.Cell("C3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C3").Style.Font.FontSize = 9;
                    worksheet.Cell("C4").Value = ddlcabang.SelectedItem.Text;
                    worksheet.Cell("C4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C4").Style.Font.FontSize = 9;
                    worksheet.Cell("C5").Value = "All - All";
                    worksheet.Cell("C5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C5").Style.Font.FontSize = 9;


                    worksheet.Cell("A7").Value = "No.";
                    worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A7").Style.Font.FontSize = 9;

                    worksheet.Cell("B7").Value = "Marketing";
                    worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B7").Style.Font.FontSize = 9;


                    worksheet.Cell("C7").Value = "Jml Nasabah";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;

                    worksheet.Cell("D7").Value = "Plafon";
                    worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("D7").Style.Font.FontSize = 9;

                    worksheet.Cell("E7").Value = "Saldo Akhir";
                    worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("E7").Style.Font.FontSize = 9;

                    worksheet.Cell("F7").Value = "Tung.Pokok";
                    worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("F7").Style.Font.FontSize = 9;

                    worksheet.Cell("G7").Value = "Tung.Bunga";
                    worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("G7").Style.Font.FontSize = 9;

                    worksheet.Cell("H7").Value = "Total Tunggakan";
                    worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("H7").Style.Font.FontSize = 9;




                    int b;
                    int p;
                    b = 7;
                    p = 0;
                    int XYZ;
                    XYZ = 0;


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


                        worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                        worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["ao"].ToString();
                        worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["jumlah_nasabah"].ToString();
                        worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["plafon"].ToString();
                        worksheet.Cell(b, "D").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "D").Style.Font.FontSize = 9;



                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["bakidb"].ToString();
                        worksheet.Cell(b, "E").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;




                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_pokok"].ToString();
                        worksheet.Cell(b, "F").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_bunga"].ToString();
                        worksheet.Cell(b, "G").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "G").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["total_tung"].ToString();
                        worksheet.Cell(b, "H").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "H").Style.Font.FontSize = 9;

                        if (XYZ == 0)
                        {
                            if (dt2.ListDataset.Tables[0].Rows[x]["xGroup"].ToString() == "1")
                            {
                                XYZ = b - 1;
                            }


                        }



                        worksheet.Row(b).Height = 12;




                    }
                    if (XYZ == 0)
                    {

                        XYZ = b;


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

                    worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell(pakhir, "B").Value = "Total";
                    worksheet.Cell(pakhir, "B").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "B").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "C").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "C").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "D").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "D").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "E").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "E").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "F").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "F").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "G").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "G").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "H").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "H").Style.Font.FontSize = 9;



                    worksheet.Cell(pakhir, "C").FormulaA1 = "=SUM(C8:C" + XYZ + ")";
                    worksheet.Cell(pakhir, "D").FormulaA1 = "=SUM(D8:D" + XYZ + ")";
                    worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + XYZ + ")";
                    worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + XYZ + ")";

                    worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G8:G" + XYZ + ")";
                    worksheet.Cell(pakhir, "H").FormulaA1 = "=SUM(H8:H" + XYZ + ")";






                    worksheet.Column(3).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(4).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(5).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";

                    //worksheet.Column(13).Style.NumberFormat.Format = "dd/mm/yyyy";

                    //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                    worksheet.Column(1).Width = 4;
                    worksheet.Column(2).Width = 19.43;
                    worksheet.Column(3).Width = 4.57;
                    worksheet.Column(4).Width = 10.86;
                    worksheet.Column(5).Width = 10.86;
                    worksheet.Column(6).Width = 9.14;
                    worksheet.Column(7).Width = 9.14;
                    worksheet.Column(8).Width = 14.29;








                    worksheet.Row(1).Height = 12;
                    worksheet.Row(2).Height = 12;

                    worksheet.Row(3).Height = 12;
                    worksheet.Row(4).Height = 12;

                    worksheet.Row(5).Height = 12;
                    worksheet.Row(6).Height = 12;
                    worksheet.Row(7).Height = 12;
                    worksheet.Row(8).Height = 12;



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

                report = "Report Rekap Tagihan" + xtanggal.ToString() + ".xlsx";

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
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlreport.SelectedValue == "1")
            {
                datagrid.Visible = true;
                divlist.Visible = true;
                divrekap.Visible = false;

                GRDataList.Visible = true;
                GRDataRekap.Visible = false;

                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTTAGIHANDETAIL", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_TAGIHAN", varx, "constrmf");


                GRDataList.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRDataList.DataBind();

                GRDataList.UseAccessibleHeader = true;
                GRDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


            }
            else    //REKAP
            {
                datagrid.Visible = true;
                divlist.Visible = false;
                divrekap.Visible = true;

                GRDataList.Visible = false;
                GRDataRekap.Visible = true;

                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, "ALL", typeof(string));
                varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAGIHANREKAP", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, "ALL", typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_TAGIHAN", varx, "constrmf");

                GRDataRekap.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRDataRekap.DataBind();

                GRDataRekap.UseAccessibleHeader = true;
                GRDataRekap.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
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
    decimal xtotalplafon = 0, xtotalsaldoakhir = 0, xtotaltunpokok = 0, xtotaltungbunga = 0, xtotaltungpjt = 0, xtotalcadangan = 0, xtotalrasio = 0, xtotaljmlanggota = 0,
            xtotalterbayar = 0, xtotaltunggakan = 0, xtotaltung = 0;
    protected void GRDataRekap_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "status_wo")) == "0")
                {

                    xtotaljmlanggota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlah_nasabah"));
                    xtotalplafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
                    xtotalsaldoakhir += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
                    xtotaltunpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok"));
                    xtotaltungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_bunga"));
                    xtotaltung += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_tung"));
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                Label lbltotalanggota = (Label)e.Row.FindControl("lbltotalanggota");
                lbltotalanggota.Text = string.Format("{0:N0}", xtotaljmlanggota);

                Label lbltotalplafon = (Label)e.Row.FindControl("lbltotalplafon");
                lbltotalplafon.Text = string.Format("{0:N0}", xtotalplafon);

                Label lbltotalsaldolakhir = (Label)e.Row.FindControl("lbltotalsaldolakhir");
                lbltotalsaldolakhir.Text = string.Format("{0:N0}", xtotalsaldoakhir);

                Label lbltotaltungpokok = (Label)e.Row.FindControl("lbltotaltungpokok");
                lbltotaltungpokok.Text = string.Format("{0:N0}", xtotaltunpokok);

                Label lbltotaltungbunga = (Label)e.Row.FindControl("lbltotaltungbunga");
                lbltotaltungbunga.Text = string.Format("{0:N0}", xtotaltungbunga);

                Label lbltotaltung = (Label)e.Row.FindControl("lbltotaltung");
                lbltotaltung.Text = string.Format("{0:N2}", xtotaltung);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }

    }
    protected void GRDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                xtotalplafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
                xtotalsaldoakhir += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
                xtotalterbayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "terbayar"));
                xtotaltunggakan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tunggakan"));
                xtotaltunpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok"));
                xtotaltungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_bunga"));
                xtotaltung += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_tung"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotalplafon1 = (Label)e.Row.FindControl("lbltotalplafon1");
                lbltotalplafon1.Text = string.Format("{0:N0}", xtotalplafon);

                Label lbltotalsaldolakhir1 = (Label)e.Row.FindControl("lbltotalsaldolakhir1");
                lbltotalsaldolakhir1.Text = string.Format("{0:N0}", xtotalsaldoakhir);

                Label lbltotalterbayar = (Label)e.Row.FindControl("lbltotalterbayar");
                lbltotalterbayar.Text = string.Format("{0:N0}", xtotalterbayar);

                Label lbltotaltunggakan = (Label)e.Row.FindControl("lbltotaltunggakan");
                lbltotaltunggakan.Text = string.Format("{0:N0}", xtotaltunggakan);

                Label lbltotaltungpokok1 = (Label)e.Row.FindControl("lbltotaltungpokok1");
                lbltotaltungpokok1.Text = string.Format("{0:N0}", xtotaltunpokok);

                Label lbltotaltungbunga1 = (Label)e.Row.FindControl("lbltotaltungbunga1");
                lbltotaltungbunga1.Text = string.Format("{0:N0}", xtotaltungbunga);

                Label lbltotaltung1 = (Label)e.Row.FindControl("lbltotaltung1");
                lbltotaltung1.Text = string.Format("{0:N2}", xtotaltung);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}