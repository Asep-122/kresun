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
using System.Activities.Statements;

public partial class Report_viewlistcadangan : System.Web.UI.Page
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
                divlist1.Visible = false;
                divrekap1.Visible = false;
                divlist2.Visible = false;
                divrekap2.Visible = false;
                GRDataRekap.Visible = false;
                GRDataList.Visible = false;

                //ddlproduk.SelectedValue = "3C";
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

                txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");

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
            divlist.Visible = false;
            divrekap.Visible = false;
            divlist1.Visible = false;
            divrekap1.Visible = false;
            divlist2.Visible = false;
            divrekap2.Visible = false;

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
            divlist2.Visible = false;
            divrekap2.Visible = false;

            GRDataList.Visible = false;
            GRDataRekap.Visible = false;

            GRDataList.DataSource = null;
            GRDataList.DataBind();
            GRDataRekap.DataSource = null;
            GRDataRekap.DataBind();
        }
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlreport.SelectedValue == "1")
        {
            ddlmarketing.Enabled = true;

            divlist.Visible = false;
            divrekap.Visible = false;
            divlist1.Visible = false;
            divrekap1.Visible = false;
            divlist2.Visible = false;
            divrekap2.Visible = false;

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
            divlist2.Visible = false;
            divrekap2.Visible = false;

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

                XLWorkbook wb = new XLWorkbook();

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTCADANGANDETAIL", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@statuspelunasan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AR_CADANGAN", varx, "constrmf");
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

                    worksheet.Cell("A1").Value = "LAPORAN LIST CADANGAN MARKETING ";
                    worksheet.Cell("A1").Style.Font.FontSize = 9;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Value = "Periode";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 9;
                    worksheet.Cell("A3").Value = "Status Pelunasan";
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
                    worksheet.Cell("A3").Value = ddlstatus.SelectedItem.Text;
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 9;
                    worksheet.Cell("A4").Value = ddlcabang.SelectedItem.Text;
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 9;
                    worksheet.Cell("A5").Value = name;
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 9;

                    worksheet.Cell("A7").Value = "No.";
                    worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A7").Style.Font.FontSize = 9;

                    worksheet.Cell("B7").Value = "No. Anggota";
                    worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B7").Style.Font.FontSize = 9;

                    worksheet.Cell("C7").Value = "Nama Anggota";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;

                    worksheet.Cell("D7").Value = "Norekening";
                    worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("D7").Style.Font.FontSize = 9;

                    worksheet.Cell("E7").Value = "Status";
                    worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("E7").Style.Font.FontSize = 9;

                    worksheet.Cell("F7").Value = "Panjang";
                    worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("F7").Style.Font.FontSize = 9;

                    worksheet.Cell("G7").Value = "Tgl Drop";
                    worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("G7").Style.Font.FontSize = 9;

                    worksheet.Cell("H7").Value = "Tgl Due";
                    worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("H7").Style.Font.FontSize = 9;

                    worksheet.Cell("I7").Value = "Plafon";
                    worksheet.Cell("I7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("I7").Style.Font.FontSize = 9;

                    worksheet.Cell("J7").Value = "Cadangan";
                    worksheet.Cell("J7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("J7").Style.Font.FontSize = 9;

                    worksheet.Cell("K7").Value = "Mutasi";
                    worksheet.Cell("K7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("K7").Style.Font.FontSize = 9;

                    worksheet.Cell("L7").Value = "Sisa";
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

                        worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_nasabah"].ToString();
                        worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                        worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_rekening"].ToString();
                        worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "D").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["status_pelunasan"].ToString();
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["perpanjanganke"].ToString();
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["tgl_drop"].ToString();
                        worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "G").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "H").Value = dt2.ListDataset.Tables[0].Rows[x]["tgl_due"].ToString();
                        worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "H").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["plafon"].ToString();
                        worksheet.Cell(b, "I").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "I").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "I").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["by_cadangan"].ToString();
                        worksheet.Cell(b, "J").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "J").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "J").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["mutasi_cadangan"].ToString();
                        worksheet.Cell(b, "K").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "K").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "K").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["sisacadangan"].ToString();
                        worksheet.Cell(b, "L").Value = ku_mingguan.Replace(",", ".");
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
                    worksheet.Cell(pakhir, "I").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "I").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "J").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "J").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "L").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "L").Style.Font.FontSize = 9;
                    worksheet.Cell(pakhir, "K").Style.Font.FontName = "Calibri";
                    worksheet.Cell(pakhir, "K").Style.Font.FontSize = 9;

                    worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I8:I" + b + ")";
                    worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J8:J" + b + ")";
                    worksheet.Cell(pakhir, "K").FormulaA1 = "=SUM(K8:K" + b + ")";
                    worksheet.Cell(pakhir, "L").FormulaA1 = "=SUM(L8:L" + b + ")";

                    worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(11).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(12).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(7).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(8).Style.NumberFormat.Format = "dd/mm/yyyy";

                    worksheet.Column(1).Width = 4;
                    worksheet.Column(2).Width = 9.57;
                    worksheet.Column(3).Width = 9.71;
                    worksheet.Column(4).Width = 11.71;
                    worksheet.Column(5).Width = 2.71;
                    worksheet.Column(6).Width = 2.43;
                    worksheet.Column(7).Width = 8.43;
                    worksheet.Column(8).Width = 8.57;
                    worksheet.Column(9).Width = 9.43;
                    worksheet.Column(10).Width = 8.57;
                    worksheet.Column(11).Width = 8;
                    worksheet.Column(12).Width = 8.43;

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
                    worksheet.Row(11).Height = 12;
                    worksheet.Row(12).Height = 12;

                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;

                    string report = "";
                    report = "Report Cadangan " + ddlmarketing.SelectedItem.Text.ToString() + ".xlsx";

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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Masih Kosong, silahkan hubungi Pusat jika ada!!');", true);
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
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CADANGANREKAP", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@statuspelunasan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AR_CADANGAN", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {
                    string name;
                    //name=dt.Rows[i][0].;
                    name = "REKAP CADANGAN";

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

                    worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell("A1").Value = "LAPORAN LIST REKAP CADANGAN MARKETING ";
                    worksheet.Cell("A1").Style.Font.FontSize = 9;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Value = "Periode";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 9;
                    worksheet.Cell("A3").Value = "Status Pelunasan";
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
                    worksheet.Cell("C3").Value = ddlstatus.SelectedItem.Text;
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

                    worksheet.Cell("C7").Value = "No Anggota";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;

                    worksheet.Cell("D7").Value = "Nama Anggota";
                    worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("D7").Style.Font.FontSize = 9;

                    worksheet.Cell("E7").Value = "Cadangan";
                    worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("E7").Style.Font.FontSize = 9;

                    worksheet.Cell("F7").Value = "Mutasi Cadangan";
                    worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("F7").Style.Font.FontSize = 9;

                    worksheet.Cell("G7").Value = "Sisa Cadangan";
                    worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("G7").Style.Font.FontSize = 9;

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

                        worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "A").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["namamarketing"].ToString();
                        worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_nasabah"].ToString();
                        worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                        worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "D").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["by_cadangan"].ToString();
                        worksheet.Cell(b, "E").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["mutasi_cadangan"].ToString();
                        worksheet.Cell(b, "F").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["sisacadangan"].ToString();
                        worksheet.Cell(b, "G").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "G").Style.Font.FontSize = 9;

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

                    worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;

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

                    worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + b + ")";
                    worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + b + ")";
                    worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G8:G" + b + ")";

                    worksheet.Column(5).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";

                    worksheet.Column(1).Width = 4;
                    worksheet.Column(2).Width = 19.43;
                    worksheet.Column(3).Width = 14.71;
                    worksheet.Column(4).Width = 24.14;
                    worksheet.Column(5).Width = 10.14;
                    worksheet.Column(6).Width = 9.57;
                    worksheet.Column(7).Width = 12.71;

                    worksheet.Row(1).Height = 12;
                    worksheet.Row(2).Height = 12;
                    worksheet.Row(3).Height = 12;
                    worksheet.Row(4).Height = 12;
                    worksheet.Row(5).Height = 12;
                    worksheet.Row(6).Height = 12;
                    worksheet.Row(7).Height = 12;

                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                    worksheet.PageSetup.Margins.Top = 0.11811;
                    worksheet.PageSetup.Margins.Left = 0.314961;
                    worksheet.PageSetup.Margins.Right = 0.314961;
                    worksheet.PageSetup.Margins.Bottom = 0.11811;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;

                    string report = "";

                    report = "Report Rekap Cadangan" + xtanggal.ToString() + ".xlsx";

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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Masih Kosong, silahkan hubungi Pusat jika ada!!');", true);
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
            string State = "";
            if (ddlreport.SelectedValue == "1")
            {
                rubahjudul();
                datagrid.Visible = true;

                GRDataList.Visible = true;
                GRDataRekap.Visible = false;

                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTCADANGANDETAIL", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@statuspelunasan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_CADANGAN", varx, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    GRDataList.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    GRDataList.AllowPaging = false;
                    GRDataList.DataBind();

                    GRDataList.UseAccessibleHeader = true;
                    GRDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GRDataList.SetPageIndex(1);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Masih Kosong, silahkan hubungi Pusat jika ada!!');", true);
                }
            }
            else    //REKAP
            {
                rubahjudul();
                datagrid.Visible = true;

                GRDataList.Visible = false;
                GRDataRekap.Visible = true;

                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, "ALL", typeof(string));
                varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CADANGANREKAP", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[5] = gb.CreateSPParameter("@statuspelunasan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));                
                ge.ListDataset = gb.GetDataJustOutPut("AR_CADANGAN", varx, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    GRDataRekap.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    GRDataRekap.AllowPaging = false;
                    GRDataRekap.DataBind();

                    GRDataRekap.UseAccessibleHeader = true;
                    GRDataRekap.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GRDataRekap.SetPageIndex(1);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data belum ada, jika ada kesalahan hubungi pusat.');", true);
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
    decimal xtotalplafon = 0, xtotalcadangan = 0, xtotalrasio = 0, xtotaljmlanggota = 0,
            xtotalterbayar = 0, xtotaltunggakan = 0, xtotalsisa = 0, xtotalmutasi = 0;
    protected void GRDataRekap_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                xtotalcadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));
                xtotalmutasi += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "mutasi_cadangan"));
                xtotalsisa += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "sisacadangan"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotalcadangan = (Label)e.Row.FindControl("lbltotalcadangan");
                lbltotalcadangan.Text = string.Format("{0:N0}", xtotalcadangan);

                Label lbltotalmutasi = (Label)e.Row.FindControl("lbltotalmutasi");
                lbltotalmutasi.Text = string.Format("{0:N0}", xtotalmutasi);

                Label lbltotalsisa = (Label)e.Row.FindControl("lbltotalsisa");
                lbltotalsisa.Text = string.Format("{0:N0}", xtotalsisa);
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
                xtotalcadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));
                xtotalmutasi += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "mutasi_cadangan"));
                xtotalsisa += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "sisacadangan"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotalplafon1 = (Label)e.Row.FindControl("lbltotalplafon1");
                lbltotalplafon1.Text = string.Format("{0:N0}", xtotalplafon);

                Label lbltotalcadangan1 = (Label)e.Row.FindControl("lbltotalcadangan1");
                lbltotalcadangan1.Text = string.Format("{0:N0}", xtotalcadangan);

                Label lbltotalmutasi1 = (Label)e.Row.FindControl("lbltotalmutasi1");
                lbltotalmutasi1.Text = string.Format("{0:N0}", xtotalmutasi);

                Label lbltotalsisa1 = (Label)e.Row.FindControl("lbltotalsisa1");
                lbltotalsisa1.Text = string.Format("{0:N0}", xtotalsisa);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }
    }
    public void rubahjudul()
    {
        if (ddlstatus.SelectedValue == "ALL" && ddlreport.SelectedValue == "1")
        {
            divlist.Visible = true;
            divlist1.Visible = false;
            divrekap.Visible = false;
            divrekap1.Visible = false;
            divlist2.Visible = false;
            divrekap2.Visible = false;
        }
        else if (ddlstatus.SelectedValue == "ALL" && ddlreport.SelectedValue == "2")
        {
            divlist.Visible = false;
            divlist1.Visible = false;
            divrekap.Visible = true;
            divrekap1.Visible = false;
            divlist2.Visible = false;
            divrekap2.Visible = false;
        }
        else if (ddlstatus.SelectedValue == "0" && ddlreport.SelectedValue == "1")
        {
            divlist.Visible = false;
            divlist1.Visible = true;
            divrekap.Visible = false;
            divrekap1.Visible = false;
            divlist2.Visible = false;
            divrekap2.Visible = false;
        }
        else if (ddlstatus.SelectedValue == "0" && ddlreport.SelectedValue == "2")
        {
            divlist.Visible = false;
            divlist1.Visible = false;
            divrekap.Visible = false;
            divrekap1.Visible = true;
            divlist2.Visible = false;
            divrekap2.Visible = false;
        }
        else if (ddlstatus.SelectedValue == "1" && ddlreport.SelectedValue == "1")
        {
            divlist.Visible = false;
            divlist1.Visible = false;
            divrekap.Visible = false;
            divrekap1.Visible = false;
            divlist2.Visible = true;
            divrekap2.Visible = false;
        }
        else if (ddlstatus.SelectedValue == "1" && ddlreport.SelectedValue == "2")
        {
            divlist.Visible = false;
            divlist1.Visible = false;
            divrekap.Visible = false;
            divrekap1.Visible = false;
            divlist2.Visible = false;
            divrekap2.Visible = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}