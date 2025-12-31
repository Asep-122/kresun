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
public partial class AR_Process_ViewBiayaApprovePusat : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "M1";
    string kodesumber = "AR";
    string KodeKonter = "AR06";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindGridEntityHEADER(); 
    }
  
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



                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[1];
                varT[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarangXYZ", varT, "constrmf");

                //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                //ge.ListDataset = gb.GetDataJustOutPut("gettglsekarangXYZ", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GetArbiayatypeApproval", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "tipebiaya asc";

                ddltypememo.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddltypememo.DataTextField = "nama";
                ddltypememo.DataValueField = "tipebiaya";
                ddltypememo.DataBind();


                //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
                //var1[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                //var1[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, "PUSAT01", typeof(string));
                ////var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang", var1, "constrmf");
                ////ge.ListDataTable = gb.GetDataJustOutPut("GG_GetCabang", var, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
                //ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlcabang.DataTextField = "Text";
                //ddlcabang.DataValueField = "value";
                //ddlcabang.DataBind();

                //ddlcabang.Items.Insert(0, new ListItem("ALL CABANG", "ALL"));

                BindGridEntityHEADER();

            }



            GRHEADER.UseAccessibleHeader = true;
            GRHEADER.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void ckbx_checked(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GRData.HeaderRow.FindControl("chkHeader");
        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxapprove");
            CheckBox ChkBoxtolakRows = (CheckBox)row.FindControl("ckbxtolak");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                ChkBoxtolakRows.Checked = false;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)


    {


        GRHEADER.DataSource = null;
        GRHEADER.DataBind();
        GRData.DataSource = null;
        GRData.DataBind();
        BindGridEntityHEADER();
        panelPosting.Visible = false;

        PnlFront.Visible = false;



    }


    
    protected void chkHeaderTolak_checked(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GRData.HeaderRow.FindControl("chkHeaderTolak");
        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxtolak");
            CheckBox ChkBoxtolakRows = (CheckBox)row.FindControl("ckbxapprove");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                ChkBoxtolakRows.Checked = false;
            }
            else
            {
                ChkBoxRows.Checked = false;

            }
        }
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    decimal debetpilihx = 0, kreditpilihx = 0, pilihxt = 0,  pilihx = 0;
    protected void chkdtlapprove_checked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxapprove");
            CheckBox ChkBoxtolak = (CheckBox)row.FindControl("ckbxtolak");
            if (ChkBoxRows.Checked == true)
            {

                pilihx += 1;
                debetpilihx += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxtolak.Checked = false;
            }

            if (ChkBoxtolak.Checked == true)
            {

                pilihxt += 1;
                kreditpilihx += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxRows.Checked = false;
            }

        }
        txtqtyapprove.Text = string.Format("{0:N0}", pilihx);
        txtjumlahapprove.Text = string.Format("{0:N2}", debetpilihx);

        txtqtytolak.Text = string.Format("{0:N0}", pilihxt);
        txtjumlahtolak.Text = string.Format("{0:N2}", kreditpilihx);

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    decimal debetpilih = 0, pilih = 0, debetpilihs = 0, pilihs = 0;
    protected void chkdtltolak_checked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxapprove");
            CheckBox ChkBoxtolak = (CheckBox)row.FindControl("ckbxtolak");
            if (ChkBoxtolak.Checked == true)
            {

                pilih += 1;
                debetpilih += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxRows.Checked = false;
            }

            if (ChkBoxRows.Checked == true)
            {

                pilihs += 1;
                debetpilihs += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxtolak.Checked = false;
            }

        }
        txtqtytolak.Text = string.Format("{0:N0}", pilih);
        txtjumlahtolak.Text = string.Format("{0:N2}", debetpilih);
        txtqtyapprove.Text = string.Format("{0:N0}", pilihs);
        txtjumlahapprove.Text = string.Format("{0:N2}", debetpilihs);
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    decimal debetpilihx1 = 0, kreditpilihx1 = 0, pilihxt1 = 0, pilihx1 = 0;
    protected void btnapprove_checked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxapprove");
            CheckBox ChkBoxtolak = (CheckBox)row.FindControl("ckbxtolak");
            ChkBoxRows.Checked = true;
            if (ChkBoxRows.Checked == true)
            {

                pilihx1 += 1;
                debetpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxtolak.Checked = false;
            }

            if (ChkBoxtolak.Checked == true)
            {

                pilihxt1 += 1;
                kreditpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxRows.Checked = false;
            }

        }
        txtqtyapprove.Text = string.Format("{0:N0}", pilihx1);
        txtjumlahapprove.Text = string.Format("{0:N2}", debetpilihx1);

        txtqtytolak.Text = string.Format("{0:N0}", pilihxt1);
        txtjumlahtolak.Text = string.Format("{0:N2}", kreditpilihx1);

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

       
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }



    decimal debetpilihx11= 0, kreditpilihx11 = 0, pilihxt11 = 0, pilihx11 = 0;
    protected void btnapprove_unchecked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxapprove");
            CheckBox ChkBoxtolak = (CheckBox)row.FindControl("ckbxtolak");
            ChkBoxRows.Checked = false;
            if (ChkBoxRows.Checked == true)
            {

                pilihx1 += 1;
                debetpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxtolak.Checked = false;
            }

            if (ChkBoxtolak.Checked == true)
            {

                pilihxt1 += 1;
                kreditpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                //ChkBoxRows.Checked = false;
            }

        }
        txtqtyapprove.Text = string.Format("{0:N0}", pilihx11);
        txtjumlahapprove.Text = string.Format("{0:N2}", debetpilihx11);

        txtqtytolak.Text = string.Format("{0:N0}", pilihxt11);
        txtjumlahtolak.Text = string.Format("{0:N2}", kreditpilihx11);

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }



    decimal xdebetpilihx1 = 0, xkreditpilihx1 = 0, xpilihxt1 = 0, xpilihx1 = 0;
    protected void btntolak_checked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxapprove");
            CheckBox ChkBoxtolak = (CheckBox)row.FindControl("ckbxtolak");
            ChkBoxtolak.Checked = true;

            if (ChkBoxtolak.Checked == true)
            {

                xpilihxt1 += 1;
                xkreditpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxRows.Checked = false;
            }


            if (ChkBoxRows.Checked == true)
            {

                xpilihx1 += 1;
                xdebetpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                //ChkBoxtolak.Checked = false;
            }

     

        }
        txtqtyapprove.Text = string.Format("{0:N0}", xpilihx1);
        txtjumlahapprove.Text = string.Format("{0:N2}", xdebetpilihx1);

        txtqtytolak.Text = string.Format("{0:N0}", xpilihxt1);
        txtjumlahtolak.Text = string.Format("{0:N2}", xkreditpilihx1);

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    decimal xxdebetpilihx1 = 0, xxkreditpilihx1 = 0, xxpilihxt1 = 0, xxpilihx1 = 0;
    protected void btntolak_unchecked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbxapprove");
            CheckBox ChkBoxtolak = (CheckBox)row.FindControl("ckbxtolak");
            ChkBoxtolak.Checked = false;

            if (ChkBoxtolak.Checked == true)
            {

                xxpilihxt1 += 1;
                xxkreditpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                ChkBoxRows.Checked = false;
            }


            if (ChkBoxRows.Checked == true)
            {

                xxpilihx1 += 1;
                xxdebetpilihx1 += decimal.Parse(((Label)row.FindControl("lblddlpembayaran")).Text);
                //ChkBoxtolak.Checked = false;
            }



        }
        txtqtyapprove.Text = string.Format("{0:N0}", xxpilihx1);
        txtjumlahapprove.Text = string.Format("{0:N2}", xxdebetpilihx1);

        txtqtytolak.Text = string.Format("{0:N0}", xxpilihxt1);
        txtjumlahtolak.Text = string.Format("{0:N2}", xxkreditpilihx1);

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }

    public void BindGridEntityHEADER()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlstatus.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVIEW", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@tglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Biaya_APPROVE", var, "constrmf");


        ge.ListDataset.Tables[0].DefaultView.Sort = "namatype Asc";


        GRHEADER.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRHEADER.DataBind();

        GRHEADER.UseAccessibleHeader = true;

        GRHEADER.HeaderRow.TableSection = TableRowSection.TableHeader;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }
    protected void GRHEADER_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    //PnlFront.Visible = false;
                    //pnladd.Visible = true;

                    //pnlHeader.Visible = false;
                    //blnadd = 1;
                    GRData.DataSource = null;
                    GRData.DataBind();

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                    var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMCHDVW", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ((Label)GRHEADER.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[5] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 20, ((Label)GRHEADER.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodetype")).Text, typeof(string));
                    var[6] = gb.CreateSPParameter("@Kodegldebit", ParameterDirection.Input, SqlDbType.VarChar, 20, ((Label)GRHEADER.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBulan")).Text, typeof(string));
                    var[7] = gb.CreateSPParameter("@KodeglKredit", ParameterDirection.Input, SqlDbType.VarChar, 20, ((Label)GRHEADER.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltahun")).Text, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Biaya_APPROVE", var, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo desc";


                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    GRData.DataBind();

                    GRData.UseAccessibleHeader = true;

                    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

                    panelPosting.Visible = true;

                    PnlFront.Visible = true;
                    txtqtyapprove.Text = string.Format("{0:N0}", 0);
                    txtjumlahapprove.Text = string.Format("{0:N2}", 0);
                    txtqtytolak.Text = string.Format("{0:N2}", 0);
                    txtjumlahtolak.Text = string.Format("{0:N2}", 0);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":


                break;
      
        }
    }

    decimal xtotal = 0;

    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                


                    xtotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahmemo"));
               


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                

                Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                lbltotal.Text = string.Format("{0:N0}", xtotal);

              
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }

    }

    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlstatus.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVIEW", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@tglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Biaya_APPROVE", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;

                    HDNKODECABANG.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    txtmasamanfaat.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblmasamanfaat")).Text;
                    txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtbayar.Enabled = false;
                    txtmasamanfaat.Enabled=false;
                    txtbayar.Enabled = false;
                    btnSimpan.Visible = true;
                    txtnote.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotememo")).Text;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    ddltypememo.SelectedValue=((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkode")).Text;
                    txtnote.Enabled = false;
                    ddltypememo.Enabled = false;
                    txtnoteApproval.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotepusat")).Text;
                    
                    if (statusclosing != "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Memo Internal Sudah diapprove manager cabang!');", true);
                        btndelete.Enabled = false;
                    }
                  
                   
                   
                    Pnlfooter4.Visible = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

              
                break;
            case "PDF":

                try
                {

                 
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        //Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        Response.End();

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
                        GC.Collect();


                    }

                }

                break;
            case "SLIP":

                try
                {

                    
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    typetrn = "B";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAR.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        if (typetrn == "B")
                        {
                            rprt.SetParameterValue("Reportname", "Terima Cash Bank");
                        }
                        else
                        {
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                        }

                        //Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                        Response.End();
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
                        GC.Collect();

                    }


                }

                break;

        }
    }
   
    
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            btndelete.Enabled = false;
            if (txtnoteApproval.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Note Tolakan Masih Kosong!');", true);
                btndelete.Enabled = true;
            }
            else
            {


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];


                Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                Params[1] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HDNKODECABANG.Value, typeof(string));
                Params[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[5] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnoteApproval.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Biaya_Tolakan_MC_X", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;

                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Tolak Done!','zzzzzz','bottom-center','success','1');", true);

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //switch (e.CommandName)
        //{
        //    case "DEL":
        //        int dgcount = GRRP.Rows.Count;


        //        ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

        //        dtRP.Columns.Clear();

        //        dtRP = (DataTable)ViewState["ListDetailRTP"];

        //        DataRow drCurrentRow = null;

        //        dtRP.Rows.Clear();

        //        int j = 1;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {

        //            if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
        //            {


        //                drCurrentRow = dtRP.NewRow();

        //                drCurrentRow["nomor"] = j;
        //                drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
        //                drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
        //                drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
        //                drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
        //                drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
        //                drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
        //                drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

        //                dtRP.Rows.Add(drCurrentRow);

        //                j++;
        //            }
        //        }

        //        if (dgcount == 1)
        //        {
        //            DataRow drdnew = dtRP.NewRow();

        //            drdnew["nomor"] = 1;
        //            drdnew["TglTrnFaktur"] = string.Empty;
        //            drdnew["NoFaktur"] = string.Empty;
        //            drdnew["TypeTrn"] = string.Empty;
        //            drdnew["TglJthTmp"] = string.Empty;
        //            drdnew["Keterangan"] = string.Empty;
        //            drdnew["JumlahTrn"] = decimal.Zero;
        //            drdnew["KursStd"] = decimal.Zero;
        //            dtRP.Rows.Add(drdnew);
        //        }


        //        ViewState["ListDetailRTP"] = dtRP;

        //        GRTP.DataSource = dtRP;
        //        GRTP.DataBind();

        //        GRTP.UseAccessibleHeader = true;
        //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        cssGridBayar();



        //        break;


        //}
    }

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((ddltypememo.SelectedValue=="C1")|| (ddltypememo.SelectedValue == "E1"))
        {
            e.Row.Cells[3].Visible = false;
            //e.Row.Cells[7].Visible = false;
           
        }


        


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok+= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
            bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bunga"));
            angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
            {
                

            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 2)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            }
            
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
            lbltotalPokok.Text = string.Format("{0:N2}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N2}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N2}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N2}", denda);



        }
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpan.Enabled = false;

            if (txtqtyapprove.Text == "0" && txtqtytolak.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Belum ada Pengajuan yang di approve dan ditolak');", true);
                btnSimpan.Enabled = true;
            }
            else
            {


                dtRP.Columns.Clear();
                dtRP.Columns.Add(new DataColumn("No", typeof(int)));
                dtRP.Columns.Add(new DataColumn("Nobukti", typeof(string)));
                dtRP.Columns.Add(new DataColumn("kodecabang", typeof(string)));
                dtRP.Columns.Add(new DataColumn("Jenis", typeof(int)));

                dtRP.Rows.Clear();
                int xi = 0;

                int blndata = 0;



                foreach (GridViewRow row in GRData.Rows)
                {
                    CheckBox ChkApprove = (CheckBox)row.FindControl("ckbxapprove");
                    CheckBox Chktolak = (CheckBox)row.FindControl("ckbxtolak");


                    if (ChkApprove.Checked == true)
                    {
                        blndata = 1;
                    }
                    else if (Chktolak.Checked == true)
                    {
                        blndata = 1;
                    }




                    if (blndata == 1) 
                    { 
                    

                    DataRow dr1 = dtRP.NewRow();
                        xi = xi + 1;

                        dr1["No"] = xi;
                        dr1["Nobukti"] = ((Label)row.FindControl("lblnobukti")).Text;
                        dr1["kodecabang"] = ((Label)row.FindControl("lblkodecabang")).Text;

                        if (ChkApprove.Checked == true)
                        {
                            dr1["Jenis"] = 1;
                        }
                        else
                        {
                            dr1["Jenis"] = 2;
                        }
                       
                        dtRP.Rows.Add(dr1);


                    }

                    blndata = 0;


                }
                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                //Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                Paramsc[0] = gb.CreateSPParameter("@AR_Pusat_Approve1", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Biaya_Approve_TMP_1", Paramsc, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {

                    txtqtyapprove.Text = string.Format("{0:N0}", 0);
                    txtjumlahapprove.Text = string.Format("{0:N2}", 0);
                    txtqtytolak.Text = string.Format("{0:N2}", 0);
                    txtjumlahtolak.Text = string.Format("{0:N2}", 0);


                    GRHEADER.DataSource = null;
                    GRHEADER.DataBind();
                    GRData.DataSource = null;
                    GRData.DataBind();
                    BindGridEntityHEADER();
                    panelPosting.Visible = false;

                    PnlFront.Visible = false;
                    btnSimpan.Enabled = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Process Done');", true);
                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }





            }

       
        
        
        
        
        
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }



    protected void btnSimpan_lama_Click(object sender, EventArgs e)
    {
         try
        {

            ////invoce yg dibayarkan
            //decimal jlhdibayar = decimal.Parse(txtbayar.Text);
            //decimal jlhcadangan = decimal.Parse(txtcadangan.Text);
            btnSimpan.Enabled = false;

            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {
                if (txtnoteApproval.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Note Approval Masih Kosong!');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {


                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];


                    Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Params[1] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, HDNKODECABANG.Value, typeof(string));
                    Params[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[5] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnoteApproval.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Biaya_Approve_MC_X", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;

                        nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','zzzzzz','bottom-center','success','1');", true);

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }
                }
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

  
  
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
       
        }

    }

  




}
