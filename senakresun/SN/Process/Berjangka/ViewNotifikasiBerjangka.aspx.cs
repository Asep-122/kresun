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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Text;

public partial class SN_Process_ViewNotifikasiBerjangka : System.Web.UI.Page
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
    DataTable dtBayar = new DataTable("TransactionBayar");

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A07";
    string kodesumber = "SN";
    string KodeKonter = "SN08";
    public string nobuktidb;

  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                //BindGridEntity();

        
            }


            blnadd = 0;

            BindGridEntityBungaBerjangka();
            BindGridEntityPajakBerjangka();
            BindGridEntityPencairanBerjangka();
            //GRData.UseAccessibleHeader = true;
            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void BindGridEntityBungaBerjangka()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATABUNGABERJANGKA", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("DSH_NOTIF", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GPBungaBerjangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GPBungaBerjangka.DataBind();

        GPBungaBerjangka.UseAccessibleHeader = true;

        GPBungaBerjangka.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityPajakBerjangka()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAPAJAKBERJANGKA", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("DSH_NOTIF", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GPPajakBerjangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GPPajakBerjangka.DataBind();

        GPPajakBerjangka.UseAccessibleHeader = true;

        GPPajakBerjangka.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityPencairanBerjangka()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAPENCAIRAN", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("DSH_NOTIF", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;

        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
 
  
    

   





}
