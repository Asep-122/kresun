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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DocumentFormat.OpenXml.EMMA;
using System.Activities.Statements;

public partial class AR_View_ViewRealisasiArea : System.Web.UI.Page
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
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A2";
    string kodesumber = "MK";
    string KodeKonter = "AP03";
    public string nobuktidb;
    string no_bukti;
    decimal tambahteller = 0;
    decimal kurangteller = 0;
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       // btncetak_Click(sender, e);
        try
        {
            
            if (!Page.IsPostBack)
            {
                

                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";


                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());


                //txtStart.Text = xtanggal.ToString("dd-MM-yyyy");
                txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
                txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
               // DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[2];
               // Paramx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AREA", typeof(string));
               // Paramx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
               // //Paramx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
               //// Paramx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
               // ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Realisasi_AREA", Paramx, "constrmf");
               // //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
               // gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddluser, "None", "");



                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AREA", typeof(string));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_Realisasi_AREA", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "kodearw asc";
                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddluser, "None", "");
                ddluser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddluser.DataTextField = "ket";
                ddluser.DataValueField = "kodearw";
                ddluser.DataBind();

                //ddluser.Items.Insert(0, new ListItem("ALL", "-"));

                //BindGridEntity();


                //GRData.UseAccessibleHeader = true;
                //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    decimal Plafon = 0, Potongan = 0, diterimaAnggota = 0, cadangan = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            Potongan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "potongan"));
            diterimaAnggota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jlhditerimaanggota"));
            cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalplafon = (Label)e.Row.FindControl("lbltotalplafon");
            lbltotalplafon.Text = string.Format("{0:N2}", Plafon);
            Label lbltotalPotongan = (Label)e.Row.FindControl("lbltotalPotongan");
            lbltotalPotongan.Text = string.Format("{0:N2}", Potongan);
            Label lbltotalditerimaanggota = (Label)e.Row.FindControl("lbltotalditerimaanggota");
            lbltotalditerimaanggota.Text = string.Format("{0:N2}", diterimaAnggota);
            Label lbltotalcadangan = (Label)e.Row.FindControl("lbltotalcadangan");
            lbltotalcadangan.Text = string.Format("{0:N2}", cadangan);
        }
    }

    public void btncetak_Click(object sender, EventArgs e)
    {


        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



    }

    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RLSAREA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, ddluser.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            var[6] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            var[7] = gb.CreateSPParameter("@KodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddluser.SelectedValue, typeof(string));

            //var[7] = gb.CreateSPParameter("@kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_Realisasi_AREA", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "plafon_total desc";
            GRDataList.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDataList.DataBind();

            GRDataList.UseAccessibleHeader = true;
            GRDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
            buttonprint.Visible = true;
            //}
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void ddlbulan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlbulan.SelectedValue), 1);
        txtStart.Text = date.ToString("dd-MM-yyyy");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlbulan.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        ddlbulan.Enabled = true;
    }
    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        if (DateTime.Parse(txtStart.Text) > DateTime.Parse(txtEnd.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Awal tidak boleh lebih dari Tanggal Akhir');", true);

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

            txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
            txtStart.Focus();
        }
        else if (DateTime.Parse(txtEnd.Text) < DateTime.Parse(txtEnd.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Akhir tidak boleh kurang dari Tanggal Awal');", true);

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

            txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
            txtEnd.Focus();
        }
        //else
        //{
        //    BindGridEntity();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        //}
    }
  
    public void ambiltanggal()
    {

        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
        ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

        txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
        txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
    }
    protected void ddluser_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGridEntity();
        //\ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
}
