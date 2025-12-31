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

public partial class AS_Report_Form_ChangeAsuransi : System.Web.UI.Page
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

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnSearch);

                DateTime now = DateTime.Now;
                //txtStart.Text = now.ToString("yyyy-MM-dd");
                //txtEnd.Text = now.ToString("yyyy-MM-dd");

                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[3];
                Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Paramsx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Paramsx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETVALUECHANGASURANSI", typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.KP_REPORTING_ASURANSI", Paramsx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kd asc";
                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
                ddlkdasaw.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdasaw.DataTextField = "kd";
                ddlkdasaw.DataValueField = "kd";
                ddlkdasaw.DataBind();
                ddlkdasaw.Items.Insert(0, new ListItem("ALL", "-"));

                ddlkdasak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdasak.DataTextField = "kd";
                ddlkdasak.DataValueField = "kd";
                ddlkdasak.DataBind();
                ddlkdasak.Items.Insert(0, new ListItem("ALL", "-"));

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
        //ddlSP.SelectedValue = SessFiskalBi;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        //tahun = now.Year;
        //DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        //txtStart.Text = date.ToString("yyyy-MM-dd");
        //DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        //xdate = date.AddMonths(1);
        //xdate = xdate.AddDays(-1);
        //txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        //Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
        //txtStart.Enabled = true;
        //txtEnd.Enabled = true;
        //Txtrptperiode.Enabled = true;
        //ddlSP.Enabled = false;


    }
    public void btnSearch_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        string totalval = ""; var title = ""; var path = "";
        string cabang = "";
        //if (ddljeniskas.SelectedValue == "1")
        //{
        //    title = "Laporan Change Bank tanpa Saldo";
        //    path = "~/CL/Report/Report/CL/CL.STD.RPT.007.rpt";
        //}
        //else if (ddljeniskas.SelectedValue == "2")
        //{
        //} 
        try
        {
            //DateTime ambilawal = Convert.ToDateTime(txtStart.Text); 
            DateTime now = DateTime.Now;

            string tanggal = now.ToString("dd-MM-yyyy");

            title = "Laporan Change Asuransi";
            path = "~/AS/Report/CR/AS.STD.RPT.002.rpt";

            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
            //Paramsc[0] = gb.CreateSPParameter("@Dtetglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            //Paramsc[1] = gb.CreateSPParameter("@Dtetglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            Paramsc[0] = gb.CreateSPParameter("@kodeAsuransiAwal", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdasaw.SelectedValue, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@kodeAsuransiAkhir", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdasak.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTCHANGEDEBITUR", typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.KP_REPORTING_ASURANSI", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {
                rprt.Load(Server.MapPath(path));
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("ReportNama", title);
                rprt.SetParameterValue("NamaPerusahaan", cabang);
                rprt.SetParameterValue("User", SessLoginID);
                //rprt.SetParameterValue("Periode", Txtrptperiode.Text);
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
                        rprt.ExportToHttpResponse(formatType, Response, true, "ChangeAsuransiword");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "ChangeAsuransipdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "ChangeAsuransiexcel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "ChangeAsuransicsv");
                        break;
                }
                Response.End();


            }
            else if (ge.ListDataTable.Rows.Count <= 0)

            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Rentang " + "" + "" + ddlkdasaw.SelectedValue + "" + "" + "  s/d  " + "" + "" + ddlkdasak.SelectedValue + " Yang anda Pilih Tidak ada')", true);
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
    //protected void txtdate_TextChanged(object sender, EventArgs e)
    //{
    //    //DateTime now = DateTime.Now;

    //    DateTime date = DateTime.Parse(txtStart.Text);
    //    DateTime xdate = DateTime.Parse(txtEnd.Text);

    //    Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");

    //}

    //protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //DateTime now = DateTime.Now;
    //    string nowx;
    //    nowx = gb.ReadOrderData();
    //    DateTime now = DateTime.Parse(nowx);
    //    int tahun;
    //    tahun = now.Year;
    //    DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
    //    txtStart.Text = date.ToString("yyyy-MM-dd");
    //    DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
    //    xdate = date.AddMonths(1);
    //    xdate = xdate.AddDays(-1);
    //    txtEnd.Text = xdate.ToString("yyyy-MM-dd");
    //    Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");

    //}
}
