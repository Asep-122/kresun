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
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class CL_Report_Filter_DaftarBank : System.Web.UI.Page
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnSearch);

                BindGridEntity();

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
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTKODEBANK", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Input, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 100, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 100,SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ReportNKmasuk", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kodebank asc";
        ddlkdbankAwl.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdbankAwl.DataTextField = "kdnmbank";
        ddlkdbankAwl.DataValueField = "kodebank";
        ddlkdbankAwl.DataBind();
        ddlkdbankAwl.Items.Insert(0, new ListItem("Pilih Data", "-"));


        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
        var1[0] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTKODEBANK", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Input, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 100, SessKdpersh, typeof(string));
        var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 100, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ReportNKmasuk", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kodebank asc";
        ddlkdbankAkr.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdbankAkr.DataTextField = "kdnmbank";
        ddlkdbankAkr.DataValueField = "kodebank";
        ddlkdbankAkr.DataBind();
        ddlkdbankAkr.Items.Insert(0, new ListItem("Pilih Data", "-"));


        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@kdCrc", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTKDCRC", typeof(string));
        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Input, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_ReportNKmasuk", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";
        ddlkdcrc.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdcrc.DataTextField = "kdnm";
        ddlkdcrc.DataValueField = "KodeCrc";
        ddlkdcrc.DataBind();
        ddlkdcrc.Items.Insert(0, new ListItem("Pilih Data", "-"));

        if (SessSystemKurs == "False")
        {
            ddlkdcrc.Items.Insert(0, new ListItem("IDR"));
            ddlkdcrc.Enabled = false;  
        }
        else
        {
            ddlkdcrc.Enabled = true; 
        }
    }
    public void btnSearch_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        string totalval = ""; var title = ""; var path = "";
        if (ddljeniskas.SelectedValue == "1")
        {
            title = "Laporan Header Daftar Bank";
            path = "~/CL/Report/Report/CL/CL.STD.RPT.007.rpt"; 
        }
        else if (ddljeniskas.SelectedValue == "2")
        {
            title = "Laporan Detail Daftar Bank";
            path = "~/CL/Report/Report/CL/CL.STD.RPT.006.rpt";
        }
        try
        {
            //DateTime ambilawal = Convert.ToDateTime(txtStart.Text); 
            DateTime now = DateTime.Now;

           string tanggal = now.ToString("dd-MM-yyyy");


            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
            Paramsc[0] = gb.CreateSPParameter("@KodeGolonganawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdbankAwl.SelectedValue, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@KodeGolonganakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkdbankAkr.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@kdCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlkdcrc.SelectedValue, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTDAFTARBANK", typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_ReportNKmasuk", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {


                rprt.Load(Server.MapPath(path));
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("ReportNama", title);
                rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);
                //rprt.SetParameterValue("Alamat1", SessAlamatProfil1);
                //rprt.SetParameterValue("Alamat2", SessAlamatProfil2);
                //rprt.SetParameterValue("Alamat3", SessKotaProfil); 
                rprt.SetParameterValue("User", SessLoginID);
                //rprt.SetParameterValue("Crc", ddlkdcrc.SelectedValue);
                rprt.SetParameterValue("Periode", tanggal);
                //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
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
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldoword");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldopdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldoexcel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldocsv");
                        break;
                }
                Response.End();


            }
            else if (ge.ListDataTable.Rows.Count <= 0)

            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Rentang " + "" + "" + ddlkdbankAwl.SelectedValue + "" + "" + "  s/d  " + "" + "" + ddlkdbankAwl.SelectedValue + " Yang anda Pilih Tidak ada')", true);
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
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;

        //string nowx;
        //nowx = gb.ReadOrderData();
        //DateTime now = DateTime.Parse(nowx);
        //int tahun;
        //tahun = now.Year;
        //DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        //DateTime awaltahun = Convert.ToDateTime(date);
        //int ambilawaltahun = Convert.ToDateTime(date).Year;
        ////ddlStart.Text = awaltahun.ToShortDateString();

        //// txtEnd.Text = awaltahun.ToString();
        //DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        //int ambilakhirtahun = Convert.ToDateTime(xdate).Year;
        //xdate = date.AddMonths(1);
        //xdate = xdate.AddDays(-1);
        ////txtEnd.Text = xdate.ToString("dd/MM/yyyy");
        ////txtEnd.Text = ambilakhirtahun.ToString("yyyy");
        //Txtrptperiode.Text = "Periode : " + ddlSP.SelectedItem + ' ' + ddlStart.SelectedValue;

        //txtrpttitle.Text = "Report Daftar Penyusutan Per-Group";

    }
}