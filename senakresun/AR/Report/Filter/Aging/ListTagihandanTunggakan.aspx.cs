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

public partial class Mikro_ListTagihandanTunggakan : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {


                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                //txtEnd.Text = now.ToString("yyyy-MM-dd");
                //DateKurs.Text = now.ToString("yyyy-MM-dd");
                //Txtkurs.Text = "0";
                BindGridEntity();

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

    //protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCurrency.SelectedValue == "IDR")
    //    {
    //        Txtkurs.Text = "1";
    //        Txtkurs.Enabled = false;
    //    }
    //    else
    //    {
    //        //Txtkurs.Text = "1";

    //        Txtkurs.Enabled = true;

    //        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
    //        Params[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlCurrency.SelectedValue, typeof(string));
    //        Params[1] = gb.CreateSPParameter("@TglKurs", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateKurs.Text, typeof(DateTime));
    //        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));
    //        ge.ListDataset = gb.GetDataJustOutPut("GL_GetKurs", Params, "constrmf");
    //        DataTable dt = new DataTable();
    //        dt = ge.ListDataset.Tables[0].DefaultView.ToTable();


    //        if (dt.Rows.Count > 0)
    //        {
    //            Txtkurs.Text = dt.Rows[0]["kursstd"].ToString();
    //        }
    //        else
    //        {
    //            Txtkurs.Text = "1";
    //        }
    //    }

    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    public void BindGridEntity()
    {

        //getproduct
        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[3];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        Paramsx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTPRODUK", typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.Report_New_Mikro", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");
        ddlproduk.Items.Insert(0, new ListItem("-", "-"));
        //ddlaccountakhir.Items.Insert(0, new ListItem("-", "-"));

        //getcabang
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTCABANG", typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.Report_New_Mikro", Params, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
        ddlcabang.Items.Insert(0, new ListItem("-", "-"));

        //getmarketing
        DefaultClass.Global.SqlHelper[] Params1 = new DefaultClass.Global.SqlHelper[3];
        Params1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params1[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        Params1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTKODESTAFF", typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.Report_New_Mikro", Params1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), DdlMarketing, "None", "");
        DdlMarketing.Items.Insert(0, new ListItem("-", "-"));

        txtrpttitle.Text = "Laporan Tagihan dan Tunggakan";
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
        //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        //txtEnd.Enabled = true;
        Txtrptperiode.Enabled = true;
        ddlSP.Enabled = true;
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {


        try
        {


            ReportDocument rprt = new ReportDocument();

            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
            Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, DdlMarketing.SelectedValue, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.SelectedValue, typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@datestart", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            Paramsc[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "LAPORANLISTTAGIHANTUNGGAKANMARKETING", typeof(string));
            //Paramsc[3] = gb.CreateSPParameter("@KodeDeptAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtawal.SelectedValue, typeof(string));
            //Paramsc[4] = gb.CreateSPParameter("@KodeDeptAkhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldprtakhir.SelectedValue, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.Report_New_Mikro", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {

                rprt.Load(Server.MapPath("~/Mikro/Report/ListTagihandanTunggakan.rpt"));
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);

                rprt.SetParameterValue("User", SessLoginID);
                rprt.SetParameterValue("ReportNama", txtrpttitle.Text);

                rprt.SetParameterValue("Periode", Txtrptperiode.Text);
                //rprt.SetParameterValue("Kurs", "Kurs :" + ddlCurrency.SelectedValue + "  Amount " + Txtkurs.Text);
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
                        rprt.ExportToHttpResponse(formatType, Response, true, "TagihanDanTunggakanword");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "TagihanDanTunggakanpdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "TagihanDanTunggakanexcel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "TagihanDanTunggakancsv");
                        break;
                }


                Response.End();


            }
            else

            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Daftar Account Empty');", true);
            }


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
        //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");



    }



}