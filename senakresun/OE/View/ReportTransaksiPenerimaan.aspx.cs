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
using Org.BouncyCastle.Utilities.Encoders;

public partial class GL_Report_ReportTransaksiPenerimaan: System.Web.UI.Page
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
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");
              
                BindGridEntity();
                ddlSP.Enabled = true;
                txtStart.Enabled = true;
                txtEnd.Enabled = true;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.VarChar, 1, "T", typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Kodegroup asc";

                ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgroup.DataTextField = "Namagroup";
                ddlgroup.DataValueField = "Kodegroup";
                ddlgroup.DataBind();

                ddlgroup.Items.Insert(0, new ListItem("Pilih Group Pendapatan", "-"));


                DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTKARYAWAN", typeof(string));
                varx1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", varx1, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";

                ddlKodenik.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKodenik.DataTextField = "NamaKry";
                ddlKodenik.DataValueField = "KodeNik";
                ddlKodenik.DataBind();

                ddlKodenik.Items.Insert(0, new ListItem("ALL Karyawan", "ALL"));

                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
                Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));


                ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetCabangReport", Paramsx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
                ddlcabang.Items.Insert(0, new ListItem("ALL CABANG", "ALL"));
                ddlcabang.SelectedValue = SessBranchID;
                ddlcabang.Enabled = false;
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

    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
       

            if (ddlgroup.SelectedValue == "A0004" || ddlgroup.SelectedValue == "T0004")
            {
                ddlKodenik.SelectedValue = "ALL";
                txtnamakaryawan.Text = "-";
                ddlKodenik.Enabled = true;
                txtnamakaryawan.Enabled = false;

            }
            else
            {
                ddlKodenik.SelectedValue = "ALL";
                txtnamakaryawan.Text = "-";
                ddlKodenik.Enabled = false;
                txtnamakaryawan.Enabled = false;



            }





            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[3];
            //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            vart1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTBIAYAGROUPNONX", typeof(string));
            vart1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vart1[2] = gb.CreateSPParameter("@kodegroup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroup.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", vart1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kodebiaya asc";

            ddlgroupbiaya.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgroupbiaya.DataTextField = "Namagroup";
            ddlgroupbiaya.DataValueField = "kodebiaya";
            ddlgroupbiaya.DataBind();

            ddlgroupbiaya.Items.Insert(0, new ListItem("All Pendapatan", "ALL"));

            ddlgroupbiaya.Enabled = true;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }




    protected void ddlkodenik_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            var bank = ddlgroup.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKodenik.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKRY", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {

                txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
             

            }
            else
            {
                txtnamakaryawan.Text = "";
               

            }


          




        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    public void BindGridEntity()
    {


     
        
        txtrpttitle.Text = "Transaction Buku Besar Report";

        //DateTime now = DateTime.Now;
        ddlSP.SelectedValue = SessFiskalBi;
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
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        Txtrptperiode.Enabled = true;
        ddlSP.Enabled = true;



    }

    decimal xjlhdebit = 0, xjlhkredit = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlah"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbljumlah = (Label)e.Row.FindControl("lbljumlah");
            lbljumlah.Text = string.Format("{0:N0}", xjlhdebit);


           

        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {




            DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[9];
            Paramst[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramst[1] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgroup.SelectedValue, typeof(string));
            Paramst[2] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            Paramst[3] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
            Paramst[4] = gb.CreateSPParameter("@Biaya", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroupbiaya.SelectedValue, typeof(string));
            Paramst[5] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            Paramst[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramst[7] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKodenik.SelectedValue, typeof(String));
            Paramst[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PENDAPATAN", typeof(string));
            dt2.ListDataset = gb.GetDataJustOutPut("OE_REPORT", Paramst, "constrmf");

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
                    name = "Report Biaya";

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




                    worksheet.Cell("A1").Value = "LAPORAN PENDAPATAN";
                    worksheet.Cell("A1").Style.Font.FontSize = 11;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A1").Style.Font.Bold = true;
                    worksheet.Cell("A2").Value = "Group";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 11;
                    worksheet.Cell("A2").Style.Font.Bold = true;
                    worksheet.Cell("A3").Value = "Pendapatan";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 11;
                    worksheet.Cell("A3").Style.Font.Bold = true;
                    worksheet.Cell("A4").Value = "Karyawan";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 11;
                    worksheet.Cell("A4").Style.Font.Bold = true;
                    worksheet.Cell("A5").Value = "Cabang";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 11;
                    worksheet.Cell("A5").Style.Font.Bold = true;

                    worksheet.Cell("A6").Value = "Periode";
                    worksheet.Cell("A6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A6").Style.Font.FontSize = 11;
                    worksheet.Cell("A6").Style.Font.Bold = true;

                    worksheet.Cell("B2").Value = "'" + ddlgroup.SelectedItem.Text;

                    worksheet.Cell("B2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B2").Style.Font.FontSize = 11;
                    worksheet.Cell("B2").Style.Font.Bold = true;
                    worksheet.Cell("B3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B3").Style.Font.FontSize = 11;
                    worksheet.Cell("B3").Style.Font.Bold = true;
                    worksheet.Cell("B3").Value = "'" + ddlgroupbiaya.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B4").Style.Font.FontSize = 11;
                    worksheet.Cell("B4").Value = "'" + ddlKodenik.SelectedItem.Text;
                    worksheet.Cell("B4").Style.Font.Bold = true;
                    worksheet.Cell("B5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B5").Style.Font.FontSize = 11;
                    worksheet.Cell("B5").Value = "'" + ddlcabang.SelectedItem.Text;
                    worksheet.Cell("B5").Style.Font.Bold = true;
                    worksheet.Cell("B6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B6").Style.Font.FontSize = 11;
                    worksheet.Cell("B6").Value =Txtrptperiode.Text;
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
                    worksheet.Column(3).Width = 12.58;
                    worksheet.Column(4).Width = 12.58;
                    worksheet.Column(5).Width = 18.03;
                    worksheet.Column(6).Width = 25.11;
                    worksheet.Column(7).Width = 34.96;
                    worksheet.Column(8).Width = 17.11;
                    worksheet.Column(9).Width = 18.11;
                    worksheet.Column(10).Width = 17.11;
                    worksheet.Column(11).Width = 17.11;
                    worksheet.Column(12).Width = 17.11;
                worksheet.Column(13).Width = 17.11;




                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 1)).Value = "NO.";
                    worksheet.Range(worksheet.Cell(8, 2), worksheet.Cell(8, 2)).Value = "Cabang";
                    worksheet.Range(worksheet.Cell(8, 3), worksheet.Cell(8, 3)).Value = "Tgl Pengajuan";
                    worksheet.Range(worksheet.Cell(8, 4), worksheet.Cell(8, 4)).Value = "Tgl Bayar";
                    worksheet.Range(worksheet.Cell(8, 5), worksheet.Cell(8, 5)).Value = "No. Bukti";
                    worksheet.Range(worksheet.Cell(8, 6), worksheet.Cell(8, 6)).Value = "Group";
                    worksheet.Range(worksheet.Cell(8, 7), worksheet.Cell(8, 7)).Value = "Biaya";
                    worksheet.Range(worksheet.Cell(8, 8), worksheet.Cell(8, 8)).Value = "Nama Karyawan";
                    worksheet.Range(worksheet.Cell(8, 9), worksheet.Cell(8, 9)).Value = "Keterangan";
                    worksheet.Range(worksheet.Cell(8, 10), worksheet.Cell(8, 10)).Value = "Jumlah";
                    worksheet.Range(worksheet.Cell(8, 11), worksheet.Cell(8, 11)).Value = "Kas & Bank";
                    worksheet.Range(worksheet.Cell(8, 12), worksheet.Cell(8, 11)).Value = "Status";
                    worksheet.Range(worksheet.Cell(8, 13), worksheet.Cell(8, 12)).Value = "Tgl Entry";




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
                        worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["tgl_pengajuan"].ToString();
                        worksheet.Cell(b, "D").Value =  dt2.ListDataset.Tables[0].Rows[x]["tglbayar"].ToString();
                        worksheet.Cell(b, "E").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_memo"].ToString();
                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["Namagroup"].ToString();
                        worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["namabaiaya"].ToString();
                        worksheet.Cell(b, "H").Value =  dt2.ListDataset.Tables[0].Rows[x]["namakaryawan"].ToString();
                        worksheet.Cell(b, "I").Value = dt2.ListDataset.Tables[0].Rows[x]["notememo"].ToString();
                        worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["jumlah"].ToString().Replace(",", ".");
                        worksheet.Cell(b, "K").Value = dt2.ListDataset.Tables[0].Rows[x]["NamaBank"].ToString();
                        worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["datax"].ToString();
                        worksheet.Cell(b, "M").Value = dt2.ListDataset.Tables[0].Rows[x]["tglentry"].ToString();

                    }



                    //worksheet.Range(worksheet.Cell(13, 13), worksheet.Cell(b, 13)).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Column(3).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(4).Style.NumberFormat.Format = "dd/mm/yyyy";
                    worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(13).Style.NumberFormat.Format = "dd/mm/yyyy";







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
                    report = "Laporan Biaya" + ddlcabang.SelectedValue + ".xlsx";
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




    public void btnsearchdata_Click(object sender, EventArgs e)
    {
       

        DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[9];
        Paramst[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Paramst[1] = gb.CreateSPParameter("@Group", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgroup.SelectedValue, typeof(string));
        Paramst[2] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
        Paramst[3] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
        Paramst[4] = gb.CreateSPParameter("@Biaya", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroupbiaya.SelectedValue, typeof(string));
        Paramst[5] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        Paramst[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        Paramst[7] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKodenik.SelectedValue, typeof(String));
        Paramst[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PENDAPATAN", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("OE_REPORT", Paramst, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tglbayar desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void btntampildata(object sender, EventArgs e)
    {
        GRData.Visible = true;
    }

    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now =DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue),1 );
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy") +  " s/d " + xdate.ToString("dd-MM-yyyy");

 

        GRData.DataSource = null;
        GRData.DataBind();
        GRData.Visible = false;
        
    }

    protected void ddltgl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;

        Txtrptperiode.Text = "Periode : " + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");



        GRData.DataSource = null;
        GRData.DataBind();
        //txtsaldoAwal.Text = "0";
        //txtmutasidebit.Text = "0";
        //txtmutasikredit.Text = "0";
        //txtsaldoakhir.Text = "0";
        GRData.Visible = false;
        //btndata.Visible = false;
    }


}