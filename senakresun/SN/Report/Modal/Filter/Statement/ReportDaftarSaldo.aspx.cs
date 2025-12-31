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

public partial class AR_Report_ReportDaftarSaldo : System.Web.UI.Page
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
                ambilTahun();
                ddlTahun.Enabled = true;
                DateKurs.Text = now.ToString("yyyy-MM-dd");
                Txtkurs.Text = "0";
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
    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 3; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
        ddlTahun.SelectedValue = currentYear.ToString();

            ddlTahun.Enabled = true;
      
    }
    protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == "IDR")
        {
            Txtkurs.Text = "1";
            Txtkurs.Enabled = false;
        }
        else
        {
            //Txtkurs.Text = "1";

            Txtkurs.Enabled = true;

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlCurrency.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@TglKurs", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateKurs.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("GL_GetKurs", Params, "constrmf");
            DataTable dt = new DataTable();
            dt = ge.ListDataset.Tables[0].DefaultView.ToTable();


            if (dt.Rows.Count > 0)
            {
                Txtkurs.Text = dt.Rows[0]["kursstd"].ToString();
            }
            else
            {
                Txtkurs.Text = "1";
            }
        }

    }

    public void BindGridEntity()
    {
   
      
        DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[1];
        Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
     
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCurrencyReport", Paramt, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCurrency, "None", "");
        ddlCurrency.Items.Insert(0, new ListItem("-", "-"));

        if (SessSystemKurs == "False")
        {
            ddlCurrency.SelectedValue = "IDR";
            ddlCurrency.Enabled = false;
            Txtkurs.Text = "1";
            Txtkurs.Enabled = false;
            DateKurs.Enabled = false;
        }
        else
        {
            ddlCurrency.SelectedValue = "IDR";
            ddlCurrency.Enabled = true;
            Txtkurs.Text = "1";
            Txtkurs.Enabled = true;
            DateKurs.Enabled = true;
        }

        txtrpttitle.Text = "Report Bulan Ini";
        ddlSP.SelectedValue = SessFiskalBi;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
   
        Txtrptperiode.Text = "Bulan : " + ddlSP.SelectedItem.Text + " Tahun : " + ddlTahun.SelectedValue;
        Txtrptperiode.Enabled = false;
        
        ddlCurrency.SelectedValue = "IDR";
        Txtkurs.Text = "1";


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
        //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "Text";
        ddlcabang.DataValueField = "value";
        ddlcabang.DataBind();

        if (SessBranchID == "000")
        {
            ddlcabang.Items.Insert(0, new ListItem("ALL CABANG", "ALL"));
        }
   

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {

        ReportDocument rprt = new ReportDocument();

        try
                {


               

                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
                Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@pajak", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "REKAPBULANINIMODAL", typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlSP.SelectedValue), typeof(int));
                Paramsc[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar,4, ddlTahun.SelectedValue, typeof(string));
                Paramsc[6] = gb.CreateSPParameter("@report", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlstatus.SelectedValue), typeof(int));

                ge.ListDataTable = gb.GetDataJustOutPut("dbo.SN_STD_RPT_Statement", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {

                Txtrptperiode.Text = "Bulan : " + ddlSP.SelectedItem.Text + " Tahun : " + ddlTahun.SelectedValue;



                string xdateawal, xdateakhir;
                    if (ddlproduk.SelectedValue == "0") {
                    rprt.Load(Server.MapPath("~/SN/Report/Berjangka/Report/TRN/SN.STD.ROLLOVER.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    rprt.SetParameterValue("NamaPrsh", SessNamaProfil);
                    rprt.SetParameterValue("User", SessLoginID);
                    rprt.SetParameterValue("Judul", "Report Bunga RollOver Bulan Ini");
                    rprt.SetParameterValue("Periode", Txtrptperiode.Text);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/SN/Report/Modal/Report/TRN/SN.STD.TRANSFER.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    rprt.SetParameterValue("NamaPrsh", SessNamaProfil);
                    rprt.SetParameterValue("User", SessLoginID);
                    rprt.SetParameterValue("Judul", "Report Bunga Transfer Bulan Ini");
                    rprt.SetParameterValue("Periode", Txtrptperiode.Text);
                }
               
   
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
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanapasaldoword");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldopdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldoexcel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldocsv");
                        break;
                }


                Response.End();


            }
            else

            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningData('  Data Pada " + Txtrptperiode.Text + " tidak ada!!');", true);
            }


            }

                catch (Exception X)
                {
                   if (X.Message!="Thread was being aborted.")
                    {

                         ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                         }
            else
            {
                rprt.Close();
                rprt.Dispose();
                GC.Collect();
            }

        }

    }

   




}