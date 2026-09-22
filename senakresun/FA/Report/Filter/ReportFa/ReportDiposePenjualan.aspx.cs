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
using System.Globalization;

public partial class GL_Report_ReportDiposePenjualan : System.Web.UI.Page
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");
                DateKurs.Text = now.ToString("yyyy-MM-dd");
                Txtkurs.Text = "0";
                BindGridEntity();
                Txtrptperiode.Enabled = false;
                Txtkurs.Enabled = false;
                txtrpttitle.Enabled = false;
                DateKurs.Enabled = false;
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
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEGROUP", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_report_bulan", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgroupawal, "None", "");
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgroupakhir, "None", "");
            ddlgroupawal.Items.Insert(0, new ListItem("-", "-"));
            ddlgroupakhir.Items.Insert(0, new ListItem("-", "-"));

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEAKTIVA", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_report_bulan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaktivaawal, "None", "");
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaktivaakhir, "None", "");
            ddlaktivaawal.Items.Insert(0, new ListItem("-", "-"));
            ddlaktivaakhir.Items.Insert(0, new ListItem("-", "-"));

            DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[1];
            var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODETRN", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_report_bulan", var2, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypeawal, "None", "");
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypeakhir, "None", "");
            ddltypeawal.Items.Insert(0, new ListItem("-", "-"));
            ddltypeakhir.Items.Insert(0, new ListItem("-", "-"));

            DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[1];
            Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCurrencyReport", Paramt, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCurrency, "None", "");
            ddlCurrency.Items.Insert(0, new ListItem("-", "-"));
            txtrpttitle.Text = "Daftar Dipose dan Penjualan";
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
            ddlCurrency.SelectedValue = "IDR";
            Txtkurs.Text = "1";
            ddlaktivaawal.SelectedValue = SessKdpersh;
            ddlaktivaakhir.SelectedValue = SessKdpersh;
            ddltypeawal.SelectedValue = SessKdpersh;
            ddltypeakhir.SelectedValue = SessKdpersh;
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
    public void btnSerach_Click(object sender, EventArgs e)
    {
        Int32 ambily = Convert.ToDateTime(txtEnd.Text).Year;
        Int32 ambilb = Convert.ToDateTime(txtStart.Text).Month;
        Int32 ambiltanggal = Convert.ToDateTime(DateTime.Now).Day ;
        Int32 ambilbulan = Convert.ToDateTime(DateTime.Now).Month;
        Int32 ambiltahun = Convert.ToDateTime(DateTime.Now).Year;
        String rubahtanggal = ambiltahun + "/" + ambilbulan + "/" + ambiltanggal;
        string ATahun = ambily.ToString();
        string ATanggal = "28";
        string ABulan = ddlSP.SelectedValue;
        string iDate = ATahun + "-" + ABulan + "-" + ATanggal;
        string tahunbulan = ATahun + "-" + ABulan + "-" + ATanggal;

        DateTime oDate = Convert.ToDateTime(iDate);

        DateTimeFormatInfo dateTimeInfo = new DateTimeFormatInfo();
        int bulanlalu = DateTime.Now.Month - 1;

        String kurangbulan = dateTimeInfo.GetAbbreviatedMonthName(bulanlalu);

        int rubahtahun = ambily;
        int rubahbulan = Int32.Parse(ddlSP.SelectedValue);
        DateTime.DaysInMonth(rubahtahun, rubahbulan);
        String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
        string striptanggal = rubahhari + "-" + ABulan + "-" + ATahun;
        string slastanggal = rubahhari + "/" + ABulan + "/" + ATahun;
        string Periode = "Periode :" + txtStart.Text + " " + "s/d" + " " + txtEnd.Text;
        ReportDocument rprt = new ReportDocument();
        try
        {
            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[12];
            Paramsc[0] = gb.CreateSPParameter("@KodeGroupawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgroupawal.SelectedValue, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@KodeGroupakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgroupakhir.SelectedValue, typeof(string));

            //Paramsc[2] = gb.CreateSPParameter("@KodeAktivaawal", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlaktivaawal.SelectedValue, typeof(string));
            //Paramsc[3] = gb.CreateSPParameter("@KodeAktivaakhir", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlaktivaakhir.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@KodeAktivaawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaktivaawal.SelectedValue, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@KodeAktivaakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaktivaakhir.SelectedValue, typeof(string));

            Paramsc[4] = gb.CreateSPParameter("@TypeTrnawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypeawal.SelectedValue, typeof(string));
            Paramsc[5] = gb.CreateSPParameter("@TypeTrnakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypeakhir.SelectedValue, typeof(string));
            Paramsc[6] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtStart.Text, typeof(String));
            Paramsc[7] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, txtEnd.Text, typeof(String));
            Paramsc[8] = gb.CreateSPParameter("@Tglhitungbulan", ParameterDirection.Input, SqlDbType.VarChar, 50,rubahtanggal, typeof(string));
            Paramsc[9] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[10] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Paramsc[11] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTREPORTDISPOSE", typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.FA_report_bulan", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {
                rprt.Load(Server.MapPath("~/FA/Report/Report/Fa/GL.STD.RPT.DISPOSE.rpt"));
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);
                rprt.SetParameterValue("User", SessLoginID);
                rprt.SetParameterValue("ReportNama", txtrpttitle.Text);
                rprt.SetParameterValue("DataBulan", Int32.Parse(ddlSP.SelectedValue));
                rprt.SetParameterValue("DataTahun", ambily);
                rprt.SetParameterValue("RubahBulan", ambilbulan);
                rprt.SetParameterValue("RubahTahun", ambiltahun);

                rprt.SetParameterValue("striptanggal", striptanggal);
                rprt.SetParameterValue("slastanggal", slastanggal);

                rprt.SetParameterValue("Periode", Periode);
                rprt.SetParameterValue("Kurs", "Kurs :" + ddlCurrency.SelectedValue + "  Amount " + Txtkurs.Text);
                Response.ClearContent();
                Response.ClearHeaders();
                ExportFormatType formatType = ExportFormatType.NoFormat;
                switch (DdlReport.SelectedItem.Value)
                {
                    case "1":
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        break;
                    case "2":
                        formatType = ExportFormatType.WordForWindows;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Dispose Dan Penjualan word");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Dispose Dan Penjualan pdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Dispose Dan Penjualan excel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Dispose Dan Penjualan csv");
                        break;
                }

                Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Bulan Atau Tahun Yang anda Pilih Tidak ada')", true);
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
                rprt.Close();
                rprt.Dispose();
            }
        }
    }
    protected void txtend_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string a = txtStart.Text;
            string tanggalstart = DateTime.ParseExact(txtStart.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string tanggalend = DateTime.ParseExact(txtEnd.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            Txtrptperiode.Text = "Periode : " + tanggalstart + ' ' + "s/d" + ' ' + tanggalend;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtstart_SelectedIndexChanged(object sender, EventArgs e) {
        try
        {
            string a = txtStart.Text;
            string tanggalstart = DateTime.ParseExact(txtStart.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string tanggalend = DateTime.ParseExact(txtEnd.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            .ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            Txtrptperiode.Text = "Periode : " + tanggalstart + ' ' + "s/d" + ' ' + tanggalend;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string nowx;
            nowx = gb.ReadOrderData();
            DateTime now = DateTime.Parse(nowx);
            int tahun;
            tahun = now.Year;
            DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
            DateTime awaltahun = Convert.ToDateTime(date);
            int ambilawaltahun = Convert.ToDateTime(date).Year;
            DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
            int ambilakhirtahun = Convert.ToDateTime(xdate).Year;
            xdate = date.AddMonths(1);
            xdate = xdate.AddDays(-1);
            Txtrptperiode.Text = "Periode : " + txtStart.Text + ' ' + "s/d" + ' ' + txtEnd.Text;
            txtrpttitle.Text = "Laporan Daftar Dispose Dan Penjualan";
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    //endclose
}







