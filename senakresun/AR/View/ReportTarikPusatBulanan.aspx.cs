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

public partial class AR_Report_ReportTarikPusatBulanan : System.Web.UI.Page
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

                 BindGridEntity();
                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttglhariini.Text = xtanggal.ToString("yyyy-MM-dd");

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

     


        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
        var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KOPERASI", typeof(string));
        var3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
        var3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var3, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        ddlkoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkoperasi.DataTextField = "Text";
        ddlkoperasi.DataValueField = "value";
        ddlkoperasi.DataBind();
        //ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlcabang.Enabled = true;
        var Koperasi = ddlkoperasi.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
        var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "Text";
        ddlcabang.DataValueField = "value";
        ddlcabang.DataBind();

        if (SessBranchID == "000")
        {
            ddlcabang.Enabled = true;
            ddlkoperasi.Enabled = true;
            ddltypereport.SelectedValue = "A";
            ddltypereport.Enabled = true;
        }
        else
        {
            ddlcabang.Enabled = false;
            ddlkoperasi.Enabled = false;
            ddltypereport.SelectedValue = "A";
            ddltypereport.Enabled = false;
        }


    }



    public void btnsearchdata_Click(object sender, EventArgs e)
    {




        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[5];
        Paramsx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Paramsx[1] = gb.CreateSPParameter("@TypeReport", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypereport.SelectedValue, typeof(string));
        Paramsx[2] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglhariini.Text, typeof(DateTime));
        Paramsx[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
        Paramsx[4] = gb.CreateSPParameter("@Kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkoperasi.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_TARIK_PUSAT_BULAN", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kodecabang,noanggota asc";


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
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "simpananpokok"));
            xjlhkredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "simpananwajib"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbldebit = (Label)e.Row.FindControl("lbldebit");
            lbldebit.Text = string.Format("{0:N0}", xjlhdebit);

            Label lblkredit = (Label)e.Row.FindControl("lblkredit");
            lblkredit.Text = string.Format("{0:N0}", xjlhkredit);


        }
    }


}