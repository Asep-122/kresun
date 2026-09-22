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

public partial class Report_viewAktivaBeli: System.Web.UI.Page
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
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
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
                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");

                ddlSP.SelectedValue = SessFiskalBi;
                string nowx;
                nowx = gb.ReadOrderData();
               
                int tahun;
                tahun = now.Year;
                DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
                txtStart.Text = date.ToString("yyyy-MM-dd");
                DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
                xdate = date.AddMonths(1);
                xdate = xdate.AddDays(-1);
                txtEnd.Text = xdate.ToString("yyyy-MM-dd");
                //Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
                txtStart.Enabled = true;
                txtEnd.Enabled = true;
                //Txtrptperiode.Enabled = true;
                ddlSP.Enabled = true;

                //DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
                //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");

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

                ddlcabang.Enabled = false;
                //// COMBO MASTER MO
                //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                //var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                //var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("AR_REPORT_KODEMARKETING", var1, "constrmf");

                //ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlmarketing.DataTextField = "namaAO";
                //ddlmarketing.DataValueField = "NIK";
                //ddlmarketing.DataBind();

                //ddlmarketing.Items.Insert(0, new ListItem("Semua Marketing", "ALL"));








            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        //Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");


    }
    //'int total = 0;
    decimal plafon = 0, nasabah = 0, baki = 0;
    decimal Total = 0, jmlbaru = 0, plafonbaru = 0, jmllama = 0, plafonlama = 0, jmltotal = 0, plafontotal = 0;
    protected void Grdtagihank_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Total += 1;
            jmlbaru += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
            jmltotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "terima"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotal = (Label)e.Row.FindControl("lblTotal");
            lbltotal.Text = Total.ToString();
            Label lbljmlbaru = (Label)e.Row.FindControl("lbljmlbaru");
            lbljmlbaru.Text = jmlbaru.ToString("#,##0.00");

            Label lblterimabaru = (Label)e.Row.FindControl("lblterimabaru");
            lblterimabaru.Text = jmltotal.ToString("#,##0.00");


        }
    }

    public void btnsearchdata_Click(object sender, EventArgs e)
    {


        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[9];
        varx[0] = gb.CreateSPParameter("@golongan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlgolongan.SelectedValue, typeof(string));
        varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AKTIVAJUAL", typeof(string));
        varx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        varx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varx[5] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroup.SelectedValue, typeof(string));
        varx[6] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
        varx[7] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
        varx[8] = gb.CreateSPParameter("@type", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltype.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("FA_DAFTAR_ASSET_01", varx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva desc";

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
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JlhPerolehan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbljumlah = (Label)e.Row.FindControl("lbljumlah");
            lbljumlah.Text = string.Format("{0:N0}", xjlhdebit);




        }
    }



    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
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

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[9];
                varx[0] = gb.CreateSPParameter("@golongan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlgolongan.SelectedValue, typeof(string));
                varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AKTIVAJUAL", typeof(string));
                varx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx[5] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroup.SelectedValue, typeof(string));
                varx[6] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
                varx[7] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
                varx[8] = gb.CreateSPParameter("@type", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltype.SelectedValue, typeof(string));

            dt2.ListDataset = gb.GetDataJustOutPut("FA_DAFTAR_ASSET_01", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {



                    string name;
                    //name=dt.Rows[i][0].;
                    name = "AKTIVA";
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
                    worksheet.Cell(7, 13).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 14).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 15).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 16).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 17).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 18).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 19).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 20).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 21).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 22).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 23).Style.Border.TopBorder = XLBorderStyleValues.Thin;



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
                    worksheet.Cell(7, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 14).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 15).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 16).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 19).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 20).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 21).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 22).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 23).Style.Border.LeftBorder = XLBorderStyleValues.Thin;




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
                    worksheet.Cell(7, 13).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 14).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 15).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 16).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 18).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 19).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 20).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 22).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 23).Style.Border.RightBorder = XLBorderStyleValues.Thin;


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
                    worksheet.Cell(7, 13).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 14).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 15).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 16).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 17).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 18).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 19).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 20).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 21).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 22).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 23).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                worksheet.Cell("A1").Value = "LAPORAN PENJUALAN & PENGHAPUSAN AKTIVA";
                    worksheet.Cell("A1").Style.Font.FontSize = 9;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Value = "Periode";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 9;
                    worksheet.Cell("A3").Value = "Golongan / Group";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 9;
                    worksheet.Cell("A4").Value = "Cabang";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 9;
                    worksheet.Cell("A5").Value = "Type";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 9;



                    worksheet.Cell("C2").Value = "'" + xtanggal.ToString("dd-MM-yyyy"); ;

                    worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C2").Style.Font.FontSize = 9;
                    worksheet.Cell("C3").Value = ddlgolongan.SelectedItem.Text + "/" + ddlgroup.SelectedItem.Text;
                    worksheet.Cell("C3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C3").Style.Font.FontSize = 9;
                    worksheet.Cell("C4").Value = ddlcabang.SelectedItem.Text;
                    worksheet.Cell("C4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C4").Style.Font.FontSize = 9;
                    worksheet.Cell("C5").Value = ddltype.SelectedItem.Text;
                    worksheet.Cell("C5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C5").Style.Font.FontSize = 9;


                    worksheet.Cell("A7").Value = "No.";
                    worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A7").Style.Font.FontSize = 9;


                    worksheet.Cell("B7").Value = "Nobukti";
                    worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B7").Style.Font.FontSize = 9;


                    worksheet.Cell("C7").Value = "Tgl Trn";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;

                worksheet.Cell("D7").Value = "Type";
                worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                worksheet.Cell("D7").Style.Font.FontSize = 9;

                worksheet.Cell("E7").Value = "DiJual/Dihapus";
                worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                worksheet.Cell("E7").Style.Font.FontSize = 9;

                worksheet.Cell("F7").Value = "Tgl Jual/Hapus";
                worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                worksheet.Cell("F7").Style.Font.FontSize = 9;


                worksheet.Cell("G7").Value = "Keterangan";
                worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                worksheet.Cell("G7").Style.Font.FontSize = 9;

                worksheet.Cell("H7").Value = "Kode Aktiva";
                    worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("H7").Style.Font.FontSize = 9;


                    worksheet.Cell("I7").Value = "Nama Aktiva";
                    worksheet.Cell("I7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("I7").Style.Font.FontSize = 9;


                worksheet.Cell("J7").Value = "No Aktiva";
                worksheet.Cell("J7").Style.Font.FontName = "Calibri";
                worksheet.Cell("J7").Style.Font.FontSize = 9;

                     worksheet.Cell("K7").Value = "Golongan";
                    worksheet.Cell("K7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("K7").Style.Font.FontSize = 9;

                    worksheet.Cell("L7").Value = "Group";
                    worksheet.Cell("L7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("L7").Style.Font.FontSize = 9;

                    worksheet.Cell("M7").Value = "Lokasi";
                    worksheet.Cell("M7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("M7").Style.Font.FontSize = 9;

                    worksheet.Cell("N7").Value = "Kelompok";
                    worksheet.Cell("N7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("N7").Style.Font.FontSize = 9;



                    worksheet.Cell("O7").Value = "Masa Susut";
                    worksheet.Cell("O7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("O7").Style.Font.FontSize = 9;


           

                worksheet.Cell("P7").Value = "Tgl mulai Susut";
                    worksheet.Cell("P7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("P7").Style.Font.FontSize = 9;


                worksheet.Cell("Q7").Value = "Tgl Akhir Susut";
                worksheet.Cell("Q7").Style.Font.FontName = "Calibri";
                worksheet.Cell("Q7").Style.Font.FontSize = 9;


                worksheet.Cell("R7").Value = "Jumlah Perolehan";
                    worksheet.Cell("R7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("R7").Style.Font.FontSize = 9;

                worksheet.Cell("S7").Value = "Akumulasi Penyusutan";
                worksheet.Cell("S7").Style.Font.FontName = "Calibri";
                worksheet.Cell("S7").Style.Font.FontSize = 9;


                worksheet.Cell("T7").Value = "Nilai Buku";
                worksheet.Cell("T7").Style.Font.FontName = "Calibri";
                worksheet.Cell("T7").Style.Font.FontSize = 9;

                worksheet.Cell("U7").Value = "Nilai Susut";
                worksheet.Cell("U7").Style.Font.FontName = "Calibri";
                worksheet.Cell("U7").Style.Font.FontSize = 9;


                worksheet.Cell("V7").Value = "Tgl Susut";
                worksheet.Cell("V7").Style.Font.FontName = "Calibri";
                worksheet.Cell("V7").Style.Font.FontSize = 9;

                worksheet.Cell("W7").Value = "Jumlah";
                worksheet.Cell("W7").Style.Font.FontName = "Calibri";
                worksheet.Cell("W7").Style.Font.FontSize = 9;




                int b;
                    int p;
                    b = 7;
                    p = 0;


                    for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                    {


                        b = b + 1;
                        p = p + 1;

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
                    worksheet.Cell(b, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 14).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 15).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 16).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 19).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 20).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 21).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 22).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 23).Style.Border.LeftBorder = XLBorderStyleValues.Thin;





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
                    worksheet.Cell(b, 13).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 14).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 15).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 16).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 18).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 19).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 20).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 22).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 23).Style.Border.RightBorder = XLBorderStyleValues.Thin;


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
                    worksheet.Cell(b, 13).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 14).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 15).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 16).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 17).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 18).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 19).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 20).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 21).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 22).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 23).Style.Border.BottomBorder = XLBorderStyleValues.Thin;



                    worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoBukti"].ToString();
                        worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString();
                        worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KetType"].ToString();
                        worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "D").Style.Font.FontSize = 9;


                        

                        worksheet.Cell(b, "E").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaLgn"].ToString();
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString();
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["Keterangan"].ToString();
                        worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "G").Style.Font.FontSize = 9;


          

                    worksheet.Cell(b, "H").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeAktiva"].ToString();
                    worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "H").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["NamaAktiva"].ToString();
                    worksheet.Cell(b, "I").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "I").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "J").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoAktiva"].ToString();
                    worksheet.Cell(b, "J").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "J").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "K").Value = dt2.ListDataset.Tables[0].Rows[x]["ketjenisgol"].ToString();
                    worksheet.Cell(b, "K").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "K").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_group"].ToString();
                    worksheet.Cell(b, "L").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "L").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "M").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaLokasi"].ToString();
                    worksheet.Cell(b, "M").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "M").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "N").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["ketgolongan"].ToString();
                    worksheet.Cell(b, "N").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "N").Style.Font.FontSize = 9;


     


                    worksheet.Cell(b, "O").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["MasaSusut"].ToString();
                    worksheet.Cell(b, "O").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "O").Style.Font.FontSize = 9;


  



       
                    worksheet.Cell(b, "P").Value = dt2.ListDataset.Tables[0].Rows[x]["TglMulaiSusut"].ToString();
                    worksheet.Cell(b, "P").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "P").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "Q").Value = dt2.ListDataset.Tables[0].Rows[x]["TglAkhirSusut"].ToString();
                    worksheet.Cell(b, "Q").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "Q").Style.Font.FontSize = 9;




                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JlhPerolehan"].ToString();
                        worksheet.Cell(b, "R").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "R").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "R").Style.Font.FontSize = 9;


                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["HargaAkumulasi"].ToString();
                        worksheet.Cell(b, "S").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "S").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "S").Style.Font.FontSize = 9;



                        ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JlhResidu"].ToString();
                        worksheet.Cell(b, "T").Value = ku_mingguan.Replace(",", ".");
                        worksheet.Cell(b, "T").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "T").Style.Font.FontSize = 9;


                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["nilaisusut"].ToString();
                    worksheet.Cell(b, "U").Value = ku_mingguan.Replace(",", ".");
                    worksheet.Cell(b, "U").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "U").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "V").Value = dt2.ListDataset.Tables[0].Rows[x]["Tglsusut"].ToString();
                        worksheet.Cell(b, "V").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "V").Style.Font.FontSize = 9;

                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["Jumlah"].ToString();
                    worksheet.Cell(b, "W").Value = ku_mingguan.Replace(",", ".");
                    worksheet.Cell(b, "W").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "W").Style.Font.FontSize = 9;


                    worksheet.Row(b).Height = 12;




                    }

                    int pakhir = 0;

                    pakhir = 8 + p;


                    worksheet.Column(17).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(16).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(6).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(22).Style.NumberFormat.Format = "dd/mm/yyyy";
                worksheet.Column(3).Style.NumberFormat.Format = "dd/mm/yyyy";
                //    //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                worksheet.Column(1).Width = 4;
                    worksheet.Column(2).Width = 15.3;
                    worksheet.Column(3).Width = 13.75;
                    worksheet.Column(4).Width = 16.29;
                    worksheet.Column(5).Width = 10.71;
                    worksheet.Column(6).Width = 9.65;
                    worksheet.Column(7).Width = 22.05;
                    worksheet.Column(8).Width = 9.42;
                    worksheet.Column(9).Width = 13.33;
                    worksheet.Column(10).Width = 14.29;
                    worksheet.Column(11).Width = 14.29;
                    worksheet.Column(12).Width = 29.01;
                    worksheet.Column(13).Width = 8.51;
                    worksheet.Column(14).Width = 8.51;
                    worksheet.Column(15).Width = 9.18;
                    worksheet.Column(16).Width = 14.29;
                    worksheet.Column(17).Width = 11.52;
                worksheet.Column(18).Width = 14.29;
                worksheet.Column(19).Width = 14.29;
                worksheet.Column(20).Width = 14.29;
                worksheet.Column(21).Width = 14.29;
                worksheet.Column(22).Width = 11.94;
                worksheet.Column(23).Width = 14.29;




                worksheet.Row(1).Height = 12;
                    worksheet.Row(2).Height = 12;

                    worksheet.Row(3).Height = 12;
                    worksheet.Row(4).Height = 12;

                    worksheet.Row(5).Height = 12;
                    worksheet.Row(6).Height = 12;
                  


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

                    //worksheet.Cell(ro, co).Style.Border.LeftBorder = XLBorderStyleValues.Thin;





                }







                string report = "";

                
                    report = "Report AKTIVA JUAL dan Hapus" + xtanggal.ToString() + ".xlsx";
               


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










    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



}