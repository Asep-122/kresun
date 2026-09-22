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

public partial class GL_Report_ReportDaftarPenyusutanGolongan : System.Web.UI.Page
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
                DateKurs.Text = now.ToString("yyyy-MM-dd");
                Txtkurs.Text = "0";
                int year = DateTime.Now.Year;
                for (int i = year - 100; i <= year + 100; i++)
                {
                    ListItem li = new ListItem(i.ToString());
                    ddlStart.Items.Add(li);
                }
                ddlStart.Items.FindByText(year.ToString()).Selected = true;
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
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

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEGOLONGAN", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("FA_report_bulan", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgolonganawal, "None", "");
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlgolonganakhir, "None", "");
        ddlgolonganawal.Items.Insert(0, new ListItem("-", "-"));
        ddlgolonganakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[2];
        Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramt[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETCURRENCY", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.FA_report_bulan", Paramt, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCurrency, "None", "");
        ddlCurrency.Items.Insert(0, new ListItem("-", "-"));
        txtrpttitle.Text = "Daftar Penyusutan Golongan Bulanan";
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
        Txtrptperiode.Text = "Periode : " + ddlSP.SelectedItem + ' ' + ddlStart.SelectedValue;
        ddlCurrency.SelectedValue = "IDR";
        Txtkurs.Text = "1";
        ddlgolonganawal.SelectedValue = SessKdpersh;
        ddlgolonganakhir.SelectedValue = SessKdpersh;
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlStart.SelectedItem == null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Pilih Bulan Awal')", true);
        }
        else
        {
            string ATahun = ddlStart.SelectedValue;
            string ATanggal = "28";
            string ABulan = ddlSP.SelectedValue;
            string iDate = ATahun + "-" + ABulan + "-" + ATanggal;
            string tahunbulan = ATahun + "-" + ABulan + "-" + ATanggal;

            DateTime oDate = Convert.ToDateTime(iDate);

            DateTimeFormatInfo dateTimeInfo = new DateTimeFormatInfo();
            int bulanlalu = DateTime.Now.Month - 1;

            String kurangbulan = dateTimeInfo.GetAbbreviatedMonthName(bulanlalu);

            int rubahtahun = Int32.Parse(ddlStart.SelectedValue);
            int rubahbulan = Int32.Parse(ddlSP.SelectedValue);
            DateTime.DaysInMonth(rubahtahun, rubahbulan);
            String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
            string rubahtanggal = ATahun + "-" + ABulan + "-" + rubahhari;
            string striptanggal = rubahhari + "-" + ABulan + "-" + ATahun;
            string slastanggal = rubahhari + "/" + ABulan + "/" + ATahun;
            ReportDocument rprt = new ReportDocument();
            try
            {
                if (ddlselectbulan.SelectedValue == "1")
                {
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[8];
                    Paramsc[0] = gb.CreateSPParameter("@KodeGolonganawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgolonganawal.SelectedValue, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@KodeGolonganakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgolonganakhir.SelectedValue, typeof(string));

                    //Paramsc[2] = gb.CreateSPParameter("@KodeAktivaawal", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlaktivaawal.SelectedValue, typeof(string));
                    //Paramsc[3] = gb.CreateSPParameter("@KodeAktivaakhir", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlaktivaakhir.SelectedValue, typeof(string));

                    Paramsc[2] = gb.CreateSPParameter("@KodeAktivaawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaktivaawal.SelectedValue, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeAktivaakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaktivaakhir.SelectedValue, typeof(string));

                    Paramsc[4] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.VarChar, 50, rubahtanggal, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[6] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTREPORTGOL", typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.FA_report_bulan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/FA/Report/Report/Fa/GL.STD.RPT.GOLBULANINI.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);
                        rprt.SetParameterValue("User", SessLoginID);
                        rprt.SetParameterValue("ReportNama", txtrpttitle.Text);
                        rprt.SetParameterValue("DataBulan", Int32.Parse(ddlSP.SelectedValue));
                        rprt.SetParameterValue("DataTahun", Int32.Parse(ddlStart.SelectedValue));
                        rprt.SetParameterValue("striptanggal", striptanggal);
                        rprt.SetParameterValue("slastanggal", slastanggal);

                        rprt.SetParameterValue("Periode", Txtrptperiode.Text);
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
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan word");
                                break;
                            case "3":
                                formatType = ExportFormatType.PortableDocFormat;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan pdf");
                                break;
                            case "4":
                                formatType = ExportFormatType.Excel;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan excel");
                                break;
                            case "5":
                                formatType = ExportFormatType.CharacterSeparatedValues;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan csv");
                                break;
                        }

                        Response.End();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Bulan Atau Tahun Yang anda Pilih Tidak ada')", true);
                    }
                }
                else if (ddlselectbulan.SelectedValue == "2")
                {
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[8];
                    Paramsc[0] = gb.CreateSPParameter("@KodeGolonganawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgolonganawal.SelectedValue, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@KodeGolonganakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgolonganakhir.SelectedValue, typeof(string));

                    //Paramsc[2] = gb.CreateSPParameter("@KodeAktivaawal", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlaktivaawal.SelectedValue, typeof(string));
                    //Paramsc[3] = gb.CreateSPParameter("@KodeAktivaakhir", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlaktivaakhir.SelectedValue, typeof(string));

                    Paramsc[2] = gb.CreateSPParameter("@KodeAktivaawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaktivaawal.SelectedValue, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeAktivaakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaktivaakhir.SelectedValue, typeof(string));

                    Paramsc[4] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.VarChar, 50, rubahtanggal, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[6] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTREPORTGOL", typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.FA_report_bulan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/FA/Report/Report/Fa/GL.STD.RPT.GOLBULANLALU.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);
                        rprt.SetParameterValue("User", SessLoginID);
                        rprt.SetParameterValue("ReportNama", txtrpttitle.Text);
                        rprt.SetParameterValue("DataBulan", Int32.Parse(ddlSP.SelectedValue));
                        rprt.SetParameterValue("DataTahun", Int32.Parse(ddlStart.SelectedValue));
                        rprt.SetParameterValue("striptanggal", striptanggal);
                        rprt.SetParameterValue("slastanggal", slastanggal);

                        rprt.SetParameterValue("Periode", Txtrptperiode.Text);
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
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan word");
                                break;
                            case "3":
                                formatType = ExportFormatType.PortableDocFormat;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan pdf");
                                break;
                            case "4":
                                formatType = ExportFormatType.Excel;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan excel");
                                break;
                            case "5":
                                formatType = ExportFormatType.CharacterSeparatedValues;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Report Daftar Penyusutan Per-Golongan csv");
                                break;
                        }

                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Bulan Atau Tahun Yang anda Pilih Tidak ada')", true);
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
                    rprt.Close();
                    rprt.Dispose();
                }
            }
        }
    }
    protected void ddlstart_SelectedChange(object sender, EventArgs e)
    {
        Txtrptperiode.Text = "Periode : " + ddlSP.SelectedItem + ' ' + ddlStart.SelectedValue;
    }
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Txtrptperiode.Text = "Periode : " + ddlSP.SelectedItem + ' ' + ddlStart.SelectedValue;
    }



}