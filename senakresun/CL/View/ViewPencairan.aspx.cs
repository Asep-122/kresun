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

public partial class CL_View_Pencairan : System.Web.UI.Page
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
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[4];
                Paramx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTADMIN", typeof(string));
                Paramx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Paramx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_VIEWPENCAIRAN", Paramx, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddluser, "None", "");
                //ddluser.Items.Insert(0, new ListItem("ALL", "-"));
                ddluser.Items.Insert(0, new ListItem("ALL", "-"));

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAO", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_VIEWPENCAIRAN", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "NIK asc";
                ddlAO.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlAO.DataTextField = "namaAO";
                ddlAO.DataValueField = "NIK";
                ddlAO.DataBind();
                ddlAO.Items.Insert(0, new ListItem("ALL", "-"));

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
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPENCAIRANNEW", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@userentry", ParameterDirection.Input, SqlDbType.VarChar, 30, ddluser.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@tglstart", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            var[6] = gb.CreateSPParameter("@tglend", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            var[7] = gb.CreateSPParameter("@kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_VIEWPENCAIRAN", var, "constrmf");
            ////ge.ListDataTable = gb.GetDataJustOutPut("AR_VIEWPENCAIRAN", var, "constrmf").Tables[0];
            //if (ge.ListDataset.Tables[0].Rows.Count == 0 && ddluser.SelectedValue != "-" && ddlAO.SelectedValue == "-")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Admin yang Anda Pilih belum melakukan inputan');", true);
            //    ambiltanggal(); 
            //    ddlAO.Focus();
            //    BindGridEntity();
            //}
            //else if(ge.ListDataset.Tables[0].Rows.Count == 0 && ddluser.SelectedValue == "-" && ddlAO.SelectedValue != "-")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Ao yang Anda Pilih belum mempunyai Anggota');", true);
            //    ambiltanggal();
            //    BindGridEntity();
            //}
            //else if (ge.ListDataset.Tables[0].Rows.Count == 0 && ddluser.SelectedValue == "-" && ddlAO.SelectedValue == "-")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Anda masih kosong, coba pilih periode lain');", true);
            //    ambiltanggal();
            //    txtStart.Focus();
            //    BindGridEntity();
            //}
            //else if(ge.ListDataset.Tables[0].Rows.Count != 0)
            //{
                ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop desc";
                GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRData.DataBind();

                GRData.UseAccessibleHeader = true;
                GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        if(DateTime.Parse(txtStart.Text) > DateTime.Parse(txtEnd.Text))
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
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            Global.SqlHelper[] var = new Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPENCAIRAN", typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@userentry", ParameterDirection.Input, SqlDbType.VarChar, 30, ddluser.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@tglstart", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            var[5] = gb.CreateSPParameter("@tglend", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            var[6] = gb.CreateSPParameter("@kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("AR_VIEWPENCAIRAN_CETAK", var, "constrmf");

            ge.ListDataTable = gb.GetDataJustOutPut("AR_VIEWPENCAIRAN_CETAK", var, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {

           
                rprt.Load(Server.MapPath("~/AR/Report/rptCetakPencairan.rpt"));
            
            string tanggalakhir = " / " + txtEnd.Text;
            rprt.SetDataSource(ge.ListDataTable);
            rprt.SetParameterValue("Periode", txtStart.Text + (txtEnd.Text == txtStart.Text ? "" : tanggalakhir));
            //rprt.SetParameterValue("bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());

            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


            Response.End();
            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }
            else
            {
                rprt.Dispose();
                rprt.Close();
                GC.Collect();
            }

        }


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
