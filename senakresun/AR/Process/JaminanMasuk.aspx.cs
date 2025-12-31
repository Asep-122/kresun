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
using System.Drawing;

public partial class AR_Process_JaminanMasuk : System.Web.UI.Page
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
    string form = "B10";
    string kodesumber = "AR";
    string KodeKonter = "AR08";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;

        btndelete.Visible = false;
        btnSimpan.Visible = false;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter4.Visible = true;
        //pnltrn.Visible = true;
        lnkAddRP2_Click();
        pnlFilter.Visible = false;
        pnltrn.Visible = false;
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
                pnlFilter.Visible = true;
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
             

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
                txtjmltax.Text = "0";
                txtjmltaxpermohonan.Text = "0";
                BindGridEntity();
            }
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Jaminan_Masuk", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrnx.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
            BindGridEntity();
        }
        else
        {
            BindGridEntity();
        }
    }
    public void BindGridEntityx()
    {
        try
        {
            var WhereCond = "";
            string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";
            WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETMIJAMINANMASUK", typeof(string));
            Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetSEARCH", Params, "constrmf");

            GRData.DataSource = ge.ListDataset.Tables[0];
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text == "")
        {
            BindGridEntity();
        }
        else
        {
            BindGridEntityx();
        }
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        pnlFilter.Visible = false;
        switch (e.CommandName)
        {
          
            case "SET":
                try
                {
                    Button1.Visible = true;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;
                    pnltrn.Visible = true;
                    blnadd = 1;
                    GRRP.Enabled = false;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //txtnobuktimanual.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobuktimanual")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtnorekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtJaminan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketjaminan")).Text;
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btndelete.Visible = false;

                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQtyMasuk")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQtykeluar")).Text;
                    txtkodeao.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeao")).Text;
                    txtnamaao.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasales")).Text;
                    btnSimpan.Enabled = true;
                    if (statusclosing == statusposting)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Sudah Keluar);", true);
                        btnSimpan.Enabled = false;
                    }
                    btnSimpan.Enabled = true;
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTJAMINAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_Jaminan_Masuk", Paramsc, "constrmf").Tables[0];
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_Jaminan_Masuk", Paramsc, "constrmf");

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        txtagunan.Text = ge.ListDataset.Tables[0].Rows[0]["agunan"].ToString();
                        txtketjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                        txtjmltaxpermohonan.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString());
                        txtjmltax.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["JmlTax"].ToString());
                        imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                        txtkondisiagunan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();

                        ViewState["ListDetailRP"] = ge.ListDataTable;

                        GRRP.DataSource = ge.ListDataTable;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        cssGridBayar();
                    }
                    else
                    {
                        SetDetailRP();
                    }

                    int jumlahmasuk = int.Parse(statusposting);
                    if (jumlahmasuk > 0)
                    {
                        btnSimpan.Enabled = false;
                        btndelete.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Anggota sudah mempunyai transaksi di Jaminan Keluar, data tidak bisa di hapus.!!');", true);
                    }

                    GRRP.Enabled = true;
                    pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIREKENING", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Jaminan_Masuk", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
       
                        txtdetailsurat.Text = ge.ListDataset.Tables[0].Rows[0]["jaminansuratdetail"].ToString();
                    }
                    else
                    {
                        txtdetailsurat.Text = "-";
                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
            case "SETDEL":
                try
                {
                    Button1.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;
                    pnltrn.Visible = true;
                    blnadd = 1;
                    GRRP.Enabled = false;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //txtnobuktimanual.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobuktimanual")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtnorekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtJaminan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketjaminan")).Text;
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btnSimpan.Visible = false;

                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQtyMasuk")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQtykeluar")).Text;
                    txtkodeao.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeao")).Text;
                    txtnamaao.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasales")).Text;

                    if (statusclosing == statusposting)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Sudah Keluar);", true);
                        btndelete.Enabled = false;
                    }

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTJAMINAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_Jaminan_Masuk", Paramsc, "constrmf").Tables[0];
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_Jaminan_Masuk", Paramsc, "constrmf");

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        txtagunan.Text = ge.ListDataset.Tables[0].Rows[0]["agunan"].ToString();
                        txtketjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                        txtjmltaxpermohonan.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString());
                        txtjmltax.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["JmlTax"].ToString());
                        imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                        txtkondisiagunan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();

                        ViewState["ListDetailBayar"] = ge.ListDataTable;

                        GRRP.DataSource = ge.ListDataTable;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        cssGridBayar();
                    }
                    else
                    {
                        SetDetailRP();
                    }

                    int jumlahmasuk = int.Parse(statusposting);
                    if (jumlahmasuk > 0)
                    {
                        btnSimpan.Enabled = false;
                        btndelete.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Anggota sudah mempunyai transaksi di Jaminan Keluar, data tidak bisa di hapus.!!');", true);
                    }

                    GRRP.Enabled = false;
                    pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
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
            case "JAMINAN":
                try
                {
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    txtnorekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtJaminan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketjaminan")).Text;
                    btnSimpan.Visible = false;

                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQtyMasuk")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQtykeluar")).Text;
                    txtkodeao.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeao")).Text;
                    txtnamaao.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasales")).Text;


                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKJAMINANMASUK", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.View_Cetak_Cabang", Paramsc, "constrmf").Tables[0];
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.View_Cetak_Cabang", Paramsc, "constrmf");

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        string koperasi = ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString();
                        string cabang = ge.ListDataset.Tables[0].Rows[0]["nama_cabang"].ToString().Remove(0, 7);
                        rprt.Load(Server.MapPath("~/AR/Process/Report/JaminanMasuk.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("koperasi", koperasi);
                        rprt.SetParameterValue("tanggal", Convert.ToDateTime(txttgltrn.Text).ToString("dd MMMM yyyy"));
                        rprt.SetParameterValue("usersearch", SessLoginID);
                        rprt.SetParameterValue("Judul", "");
                        rprt.SetParameterValue("cabang", cabang);
                        rprt.SetParameterValue("nobukti", txtnobukti.Text);
                        rprt.SetParameterValue("nonasabah", hdncustomer.Value);
                        rprt.SetParameterValue("nama", txtNmSup.Text);
                        rprt.SetParameterValue("norekening", txtnorekening.Text);
                        rprt.SetParameterValue("jaminan", txtJaminan.Text);
                        rprt.SetParameterValue("kodeao", txtkodeao.Text);
                        rprt.SetParameterValue("namaao", txtnamaao.Text);
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
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {
            dtBayar.Columns.Clear();
            dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("Jaminan", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("path", typeof(string)));

            dtBayar.Rows.Clear();
            int xi = 0;

            for (int i = 0; i < GRRP.Rows.Count; i++)
            {
                DataRow dr1 = dtBayar.NewRow();
                xi = xi + 1;
                dr1["No"] = xi;
                dr1["Jaminan"] = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;
                dr1["path"] = ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text;
                string ambilpath = ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text;
                if(ambilpath != "")
                {
                    File.Delete(Server.MapPath(ambilpath));

                }

                dtBayar.Rows.Add(dr1);
            }


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[12];

            Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@Qtymasuk", ParameterDirection.Input, SqlDbType.Int, 0, xi, typeof(int));
            Params[3] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnorekening.Text, typeof(string));
            Params[4] = gb.CreateSPParameter("@Nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));

            Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
            Params[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[10] = gb.CreateSPParameter("@Namanasabah", ParameterDirection.Input, SqlDbType.VarChar, 150, txtNmSup.Text, typeof(string));
            Params[11] = gb.CreateSPParameter("@Jaminan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtJaminan.Text, typeof(string));
            // Params[12] = gb.CreateSPParameter("@Jaminan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtJaminan.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Jaminan_Masuk_Delete", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','2');", true);

            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            //int dgcount = GRRP.Rows.Count;
            //GridViewRow grow = (GridViewRow)GRRP.Rows[dgcount - 1];
            //if (dgcount > 1 && (((DropDownList)grow.FindControl("ddlkategoriGRRP")).SelectedValue == "-" || ((DropDownList)grow.FindControl("ddlposisi")).SelectedValue == "-" ||
            //     ((TextBox)grow.FindControl("txtnilaitax")).Text == "" || ((TextBox)grow.FindControl("txtjaminanmasuk")).Text == "" ||
            //     ((FileUpload)grow.FindControl("fileuploadbuktitrn")).FileName.ToString() == "")
            //    )
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Lengkapi kolom yang masih kosong!');", true);
            //}
            //int dgcount = GRRP.Rows.Count;
            //for (int i = 0; i < GRRP.Rows.Count; i++)
            //{

            //}
            int dgcount = GRRP.Rows.Count;
            GridViewRow grow = (GridViewRow)GRRP.Rows[dgcount - 1];
            if (((TextBox)grow.FindControl("txtnilaitax")).Text == "" || ((TextBox)grow.FindControl("txtjaminanmasuk")).Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Lengkapi kolom yang masih kosong!');", true);
            }
            else if (decimal.Parse(txtjmltax.Text) != decimal.Parse(txtjmltaxpermohonan.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nilai Tax di Jumlah Tax Permohonan dan Jumlah Tax Harus sama!!');", true);
            }
            else
            {
                //invoce yg dibayarkan
                string ambilnama = "";

                dtBayar.Columns.Clear();
                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Jaminan", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("path", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("nilaitax", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("kategori", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("posisi", typeof(string)));


                dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    DataRow dr1 = dtBayar.NewRow();
                    xi = xi + 1;

                    bool masuk = ((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).HasFile;
                    if (((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).FileName.ToString() != "")
                    {
                        string namafile = "";
                        string namafilex = "";
                        //string fotopath = "";
                        //string folderPath = Server.MapPath("~/Files/jaminanmasuk/");

                        DateTime now = DateTime.Now;

                        namafile = i + ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedItem.Text + txtnorekening.Text +
                                     ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text +
                                     "_" + now.ToString("ddMMyyyyhhmmss");
                        string folderPath = Server.MapPath("~/Files/jaminanmasuk/" + namafile);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }


                        namafile = namafile + ".jpg";
                        namafilex = namafile + "OK.jpg";


                        //create temp
                        string path = Server.MapPath("~/Files/jaminanmasuk/" + namafile + ".jpg");
                        ((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).SaveAs(path);
                        //create temp

                        System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                        float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                        int newHeight = 200;
                        int newWidth = Convert.ToInt32(aspectRatio * newHeight);
                        System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
                        System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
                        thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imageRectangle);
                        thumbBitmap.Save(Server.MapPath("~/Files/jaminanmasuk/" + namafilex), System.Drawing.Imaging.ImageFormat.Jpeg);
                        thumbGraph.Dispose();
                        thumbBitmap.Dispose();
                        image.Dispose();
                        ambilnama = "~/Files/jaminanmasuk/" + namafilex;
                        ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text = ambilnama;
                        FileInfo file = new FileInfo(path);
                        if (file.Exists)//check file exsit or not.
                        {
                            file.Delete();
                        }

                        //namafile = i + ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedItem.Text + txtnorekening.Text +
                        //            ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text +
                        //            "_" + now.ToString("ddMMyyyyhhmmss");
                        //ambilnama = ((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).FileName;
                        //String FileExtension = Path.GetExtension(ambilnama.ToString()).ToLower();

                        //Check whether Directory (Folder) exists.
                        //if (!Directory.Exists(folderPath))
                        //{
                        //    If Directory (Folder) does not exists. Create it.
                        //    Directory.CreateDirectory(folderPath);
                        //}

                        //((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).SaveAs(folderPath + namafile + FileExtension);
                        //ambilnama = "~/Files/JaminanMasuk/" + namafile + FileExtension;
                        //((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text = ambilnama;

                    }
                    dr1["No"] = xi;
                    dr1["Jaminan"] = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;
                    dr1["path"] = ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text;
                    dr1["nilaitax"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text);
                    dr1["kategori"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue;
                    dr1["posisi"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlposisi")).SelectedValue;
                    dtBayar.Rows.Add(dr1);

                }


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[15];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@Qtymasuk", ParameterDirection.Input, SqlDbType.Int, 0, xi, typeof(int));
                Params[3] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnorekening.Text, typeof(string));
                Params[4] = gb.CreateSPParameter("@Nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[10] = gb.CreateSPParameter("@Namanasabah", ParameterDirection.Input, SqlDbType.VarChar, 150, txtNmSup.Text, typeof(string));
                Params[11] = gb.CreateSPParameter("@AR_LIST_JAMINAN_MASUK", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[12] = gb.CreateSPParameter("@Jaminan", ParameterDirection.Input, SqlDbType.VarChar, 300, txtJaminan.Text, typeof(string));
                Params[13] = gb.CreateSPParameter("@jmltax", ParameterDirection.Input, SqlDbType.Money, 0, txtjmltax.Text, typeof(string));
                Params[14] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Jaminan_Masuk_Save", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;

                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
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
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void txtjaminan_Onchange(object sender, EventArgs e)
    {
        try
        {
            int dgcount = GRRP.Rows.Count;
            string masuknamafile = "";
            for (int i = 0; i < dgcount; i++)
            {
                if (txtJaminan.Text == "" && dgcount == 1)
                {
                    masuknamafile = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;
                }
                else if (dgcount > 1)
                {
                    txtJaminan.Text = "";
                    if (dgcount - 1 == i)
                    {
                        masuknamafile += ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text + ".";
                    }
                    else
                    {
                        masuknamafile += ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text + ", ";
                    }
                }
                txtJaminan.Text = masuknamafile;
            }
            //btnSimpan.Focus();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void lnkBT_Click(object sender, EventArgs e)
    {
        try
        {
            int dgcount = GRRP.Rows.Count;
            string ambilnama = "";
            string ambilpath = "";

            dtBayar.Columns.Clear();

            dtBayar = (DataTable)ViewState["ListDetailRP"];

            dtBayar.Rows.Clear();

            GridViewRow grow = (GridViewRow)GRRP.Rows[dgcount - 1];
            if (((DropDownList)grow.FindControl("ddlkategori")).SelectedValue == "-" || ((DropDownList)grow.FindControl("ddlposisi")).SelectedValue == "-" ||
                 ((TextBox)grow.FindControl("txtnilaitax")).Text == "" || ((TextBox)grow.FindControl("txtjaminanmasuk")).Text == "" 
                )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Lengkapi kolom yang masih kosong!');", true);
            }
            else if (((FileUpload)GRRP.Rows[dgcount - 1].FindControl("fileuploadbuktitrn")).HasFile)
            {
                //Button Btn = sender as Button;
                //GridViewRow gvRow = Btn.NamingContainer as GridViewRow;
                //int rowId = gvRow.RowIndex;
                //Button btndelete = GRRP.Rows[rowId].FindControl("btndelete2") as Button;
                //btndelete.Visible = true;

                

                bool masuk = ((FileUpload)GRRP.Rows[dgcount - 1].FindControl("fileuploadbuktitrn")).HasFile;
                ambilnama = ((FileUpload)GRRP.Rows[dgcount - 1].FindControl("fileuploadbuktitrn")).FileName.ToString();
                ambilpath = ((TextBox)GRRP.Rows[dgcount - 1].FindControl("txtpath")).Text;
                if (((DropDownList)grow.FindControl("ddlkategori")).SelectedValue == "-" || ((DropDownList)grow.FindControl("ddlposisi")).SelectedValue == "-" ||
                 ((TextBox)grow.FindControl("txtnilaitax")).Text == "" || ((TextBox)grow.FindControl("txtjaminanmasuk")).Text == "" ||
                 ((FileUpload)grow.FindControl("fileuploadbuktitrn")).FileName.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Lengkapi kolom yang masih kosong!');", true);
                }
                else
                {
                    if (((FileUpload)GRRP.Rows[dgcount - 1].FindControl("fileuploadbuktitrn")).HasFile)
                    {
                        for (int i = 0; i < dgcount; i++)
                        {
                            if (((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).FileName.ToString() != "")
                            {
                                string namafile = "";
                                string namafilex = "";
                                string fotopath = "";
                                //string folderPath = Server.MapPath("~/Files/jaminanmasuk/");

                                DateTime now = DateTime.Now;


                                //ambilnama = ((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).FileName;
                                //String FileExtension = Path.GetExtension(ambilnama.ToString()).ToLower();

                                ////Check whether Directory (Folder) exists.
                                //if (!Directory.Exists(folderPath))
                                //{
                                //    //If Directory (Folder) does not exists. Create it.
                                //    Directory.CreateDirectory(folderPath);
                                //}

                                //((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).SaveAs(folderPath + namafile + FileExtension);
                                //ambilnama = "~/Files/JaminanMasuk/" + namafile + FileExtension;
                                //((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text = ambilnama;

                                namafile = dgcount + ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedItem.Text + txtnorekening.Text +
                                     ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text +
                                     "_" + now.ToString("ddMMyyyyhhmmss");
                                string folderPath = Server.MapPath("~/Files/jaminanmasuk/" + namafile);
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }


                                namafile = namafile + ".jpg";
                                namafilex = namafile + "OK.jpg";


                                //create temp
                                string path = Server.MapPath("~/Files/jaminanmasuk/" + namafile + ".jpg");
                                ((FileUpload)GRRP.Rows[i].FindControl("fileuploadbuktitrn")).SaveAs(path);
                                //create temp

                                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                                float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                                int newHeight = 200;
                                int newWidth = Convert.ToInt32(aspectRatio * newHeight);
                                System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
                                System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
                                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                thumbGraph.DrawImage(image, imageRectangle);
                                thumbBitmap.Save(Server.MapPath("~/Files/jaminanmasuk/" + namafilex), System.Drawing.Imaging.ImageFormat.Jpeg);
                                thumbGraph.Dispose();
                                thumbBitmap.Dispose();
                                image.Dispose();
                                ambilnama = "~/Files/jaminanmasuk/" + namafilex;
                                ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text = ambilnama;
                                FileInfo file = new FileInfo(path);
                                if (file.Exists)//check file exsit or not.
                                {
                                    file.Delete();
                                }
                            }
                        }

                        //string asdas = ((TextBox)GRRP.Rows[dgcount].FindControl("path")).Text;

                    }
                    else if (((TextBox)GRRP.Rows[dgcount - 1].FindControl("txtpath")).Text == "" && ambilnama == "")
                    {
                        ((TextBox)GRRP.Rows[dgcount - 1].FindControl("txtpath")).Text = "";
                    }
                    if (dgcount < 1 && ((DropDownList)GRRP.Rows[dgcount - 1].FindControl("ddlkategori")).SelectedValue == "1" &&
                        ((TextBox)GRRP.Rows[dgcount - 1].FindControl("txtnilaitax")).Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Lengkapi kolom yang masih kosong!');", true);
                    }
                    else
                    {
                        for (int i = 0; i < dgcount; i++)
                        {
                            DataRow dr = dtBayar.NewRow();
                            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                            dr["jaminan"] = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                            dr["path"] = ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text;
                            dr["nilaitax"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text);
                            dr["kategori"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue;
                            dr["posisi"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlposisi")).SelectedValue;
                            dtBayar.Rows.Add(dr);
                            if (((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue == "1")
                            {
                                ((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Enabled = true;
                            }
                        }

                        DataRow drdnew = dtBayar.NewRow();
                        drdnew["nomor"] = dgcount + 1;
                        drdnew["Jaminan"] = string.Empty;
                        drdnew["path"] = string.Empty;
                        drdnew["nilaitax"] = 0;
                        drdnew["kategori"] = 1;
                        drdnew["posisi"] = 1;


                        dtBayar.Rows.Add(drdnew);

                        ViewState["ListDetailRP"] = dtBayar;


                        //FileUpload fileUpload = GRRP.Rows[Convert.ToInt32(dgcount - 1)].FindControl("fileuploadbuktitrn") as FileUpload;

                        GRRP.DataSource = dtBayar;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        cssGridBayar();
                    }
                }
                
                txtcustomer.Text = txtcustomer.Text;
                GRRP.Focus();
            }
            else if (dgcount < 1 && ((DropDownList)GRRP.Rows[dgcount - 1].FindControl("ddlkategori")).SelectedValue == "-" ||
                ((TextBox)GRRP.Rows[dgcount - 1].FindControl("txtnilaitax")).Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Lengkapi kolom yang masih kosong!');", true);
            }
            else if (dgcount == 0)
            {
                //for (int i = 0; i < dgcount; i++)
                //{
                //    DataRow dr = dtBayar.NewRow();
                //    dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                //    dr["jaminan"] = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                //    dr["path"] = ambilnama;
                //    dr["nilaitax"] = ((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text;
                //    dr["kategori"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue;
                //    dr["posisi"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlposisi")).SelectedValue;
                //    dtBayar.Rows.Add(dr);
                //    if (((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue == "1")
                //    {
                //        ((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Enabled = true;
                //    }
                //}

                DataRow drdnew = dtBayar.NewRow();
                drdnew["nomor"] = dgcount + 1;
                drdnew["Jaminan"] = string.Empty;
                drdnew["path"] = string.Empty;
                drdnew["nilaitax"] = 0;
                drdnew["kategori"] = "1";
                drdnew["posisi"] = "1";

                //((DropDownList)GRRP.Rows[0].FindControl("ddlkategori")).Enabled = true;

                dtBayar.Rows.Add(drdnew);

                ViewState["ListDetailRP"] = dtBayar;
                //FileUpload fileUpload = GRRP.Rows[Convert.ToInt32(dgcount - 1)].FindControl("fileuploadbuktitrn") as FileUpload;

                GRRP.DataSource = dtBayar;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridBayar();
            }
            //else if (((TextBox)grow.FindControl("txtpath")).Text == "")
            //{
            //    GRRP.Focus();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Upload Document anda terlebih Dahulu!');", true);
            //}
            else
            {

                for (int i = 0; i < dgcount; i++)
                {
                    DataRow dr = dtBayar.NewRow();
                    dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                    dr["jaminan"] = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                    dr["path"] = ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text;
                    dr["nilaitax"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text);
                    dr["kategori"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue;
                    dr["posisi"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlposisi")).SelectedValue;
                    dtBayar.Rows.Add(dr);
                    if (((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue == "1")
                    {
                        ((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Enabled = true;
                    }
                }

                DataRow drdnew = dtBayar.NewRow();
                drdnew["nomor"] = dgcount + 1;
                drdnew["Jaminan"] = string.Empty;
                drdnew["path"] = string.Empty;
                drdnew["nilaitax"] = 0;
                drdnew["kategori"] = "1";
                drdnew["posisi"] = "1";


                dtBayar.Rows.Add(drdnew);

                ViewState["ListDetailRP"] = dtBayar;
                //FileUpload fileUpload = GRRP.Rows[Convert.ToInt32(dgcount - 1)].FindControl("fileuploadbuktitrn") as FileUpload;

                GRRP.DataSource = dtBayar;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridBayar();
            }
            for (int i = 0; i < dgcount; i++)
            {
                if (((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue == "1")
                {
                    //((DropDownList)GRRP.Rows[0].FindControl("ddlkategori")).Enabled = true;
                    ((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Enabled = true;
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }
    protected void txtnilai_TextChange(object sender, EventArgs e)
    {
        try
        {
             
            subgridbayar_Click();
            txtcustomer.Text = txtcustomer.Text;
            //btnSimpan.Focus();

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlkategori_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            if (((DropDownList)grow.FindControl("ddlkategori")).SelectedValue == "1")
            {
                ((TextBox)grow.FindControl("txtnilaitax")).Enabled = true;
            }
            else
            {
                ((TextBox)grow.FindControl("txtnilaitax")).Enabled = false;
                ((TextBox)grow.FindControl("txtnilaitax")).Text = "0";
            }
            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
            subgridbayar_Click();
            txtcustomer.Text = txtcustomer.Text;
            //btnSimpan.Focus();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            var kodelgn = hdncustomer.Value;

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIREKENING", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_Jaminan_Masuk", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtkodelgn.Text = hdncustomer.Value;
                txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                txtnorekening.Text = HDNoRekening.Value;
                txtagunan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                txtketjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminansurat"].ToString();
                //decimal masuk = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString());
                //Decimal masd = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString());
                //string asdas = string.Format("{0:N2}", masd);
                txtjmltaxpermohonan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                txtkondisiagunan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
                txtkodeao.Text = ge.ListDataset.Tables[0].Rows[0]["kd_staff"].ToString();
                txtnamaao.Text = ge.ListDataset.Tables[0].Rows[0]["NamaSales"].ToString();
                txtdetailsurat.Text = ge.ListDataset.Tables[0].Rows[0]["jaminansuratdetail"].ToString();
                

                btnSimpan.Visible = true;
                pnltrn.Visible = true;




                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                Paramsc[0] = gb.CreateSPParameter("@Agunan", ParameterDirection.Input, SqlDbType.VarChar,255, txtagunan.Text, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTJAMINANBARU", typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@jaminan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtketjaminan.Text, typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@nilaiagunan", ParameterDirection.Input, SqlDbType.Money, 0, txtjmltaxpermohonan.Text, typeof(decimal));
                Paramsc[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_Jaminan_Masuk", Paramsc, "constrmf").Tables[0];
             
                if (ge.ListDataTable.Rows.Count > 0)
                {
                    
                    ViewState["ListDetailRP"] = ge.ListDataTable;

                    GRRP.DataSource = ge.ListDataTable;
                    GRRP.DataBind();

                    GRRP.UseAccessibleHeader = true;
                    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                    cssGridBayar();
                    subgridbayar_Click();
                }
           



            }

            else
            {
                txtkodelgn.Text = "";
                txtNmSup.Text = "";
                txtnorekening.Text = "";
                txtJaminan.Text = "";

                txtdetailsurat.Text = "";
                btnSimpan.Enabled = false;
                SetDetailRP();

            }


            //SetDetailRP();


            pnltrn.Visible = true;

            //lnkAddRP2_Click();

            //lnkBT_Click();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    private void subgridbayar_Click()
    {
        int dgcount = GRRP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {
            for (int i = 0; i < dgcount; i++)
            {
                if (((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text != "")
                {
                    jlhgl = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text);
                    sumjlhgl = sumjlhgl + jlhgl;


                    string a = string.Format("{0:N2}", sumjlhgl); 
                    GRRP.FooterRow.Cells[4].Text = a.ToString();
                    btnSimpan.Focus();
                }
 
            }
            
        }
        else
        {
            sumjlhgl = 0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
        txtjmltax.Text = formatted;


         
    }
    private void cssGridBayar()
    {

        // Attribute to show the Plus Minus Button.
        GRRP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRRP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-class", "expand");
        cells[4].Attributes.Add("data-class", "expand");
        cells[5].Attributes.Add("data-class", "expand");

    }
    private void SetDetailRP()
    {
        DataRow dr = null;

        dtBayar.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("Jaminan", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("path", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("nilaitax", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("kategori", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("posisi", typeof(string)));

        dr = dtBayar.NewRow();
        dr["nomor"] = 1;
        dr["Jaminan"] = string.Empty;
        dr["path"] = string.Empty;
        dr["nilaitax"] = 0;
        dr["kategori"] = "1";
        dr["posisi"] = "1";


        dtBayar.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtBayar;


        GRRP.DataSource = dtBayar;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
    }
    private void lnkAddRP2_Click()
    {
        int dgcount = GRRP.Rows.Count;
        if (dgcount > 0)
        {
            dtBayar.Columns.Clear();

            dtBayar = (DataTable)ViewState["ListDetailRP"];

            dtBayar.Rows.Clear();

            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtBayar.NewRow();

                dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                dr["Jaminan"] = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;
                dr["nilaitax"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text);
                dr["kategori"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue;
                dr["posisi"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlposisi")).SelectedValue;
                dtBayar.Rows.Add(dr);
            }

            //DataRow drdnew = dtRP.NewRow();
            //drdnew["nomor"] = dgcount + 1;
            //drdnew["KodeGL"] = string.Empty;
            //drdnew["NamaGL"] = string.Empty;
            //drdnew["Description"] = string.Empty;
            //drdnew["Costcenter"] = string.Empty;
            //drdnew["jlhdebit"] = decimal.Zero;
            //drdnew["jlhkredit"] = decimal.Zero;

            //dtRP.Rows.Add(drdnew);

            ViewState["ListDetailRP"] = dtBayar;

            GRRP.DataSource = dtBayar;
            GRRP.DataBind();

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridBayar();

        }
        else
        {
            SetDetailRP();
        }
        //((TextBox)GRRP.Rows[dgcount - 1].FindControl("txtjaminanmasuk")).Focus();
        btnSimpan.Focus();
    }
    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                
                int dgcount = GRRP.Rows.Count;

                string txtjaminan = ((TextBox)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtjaminanmasuk")).Text;
                string txtnilaitax = ((TextBox)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtnilaitax")).Text;
                string path = ((TextBox)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtpath")).Text;
                
          
                
                    ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                    dtBayar.Columns.Clear();

                    dtBayar = (DataTable)ViewState["ListDetailRP"];

                    DataRow drCurrentRow = null;

                    dtBayar.Rows.Clear();

                    int j = 1;

                    for (int i = 0; i < GRRP.Rows.Count; i++)
                    {

                        if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                        {


                            drCurrentRow = dtBayar.NewRow();

                            drCurrentRow["nomor"] = j;
                            drCurrentRow["Jaminan"] = ((TextBox)GRRP.Rows[i].FindControl("txtjaminanmasuk")).Text;
                            drCurrentRow["path"] = ((TextBox)GRRP.Rows[i].FindControl("txtpath")).Text;
                            drCurrentRow["nilaitax"] = ((TextBox)GRRP.Rows[i].FindControl("txtnilaitax")).Text;
                            drCurrentRow["kategori"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlkategori")).SelectedValue;
                            drCurrentRow["posisi"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlposisi")).SelectedValue;
                            dtBayar.Rows.Add(drCurrentRow);

                            j++;
                        }
                    }

                    if (dgcount == 1)
                    {
                        DataRow drdnew = dtBayar.NewRow();

                        drdnew["nomor"] = 1;

                        drdnew["Jaminan"] = string.Empty;
                        drdnew["path"] = string.Empty;
                        drdnew["nilaitax"] = 0;
                        drdnew["kategori"] = "1";
                        drdnew["posisi"] = "1";

                        dtBayar.Rows.Add(drdnew);
                    }


                    ViewState["ListDetailRP"] = dtBayar;

                    GRRP.DataSource = dtBayar;
                    GRRP.DataBind();

                    GRRP.UseAccessibleHeader = true;
                    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                    cssGridBayar();
                    subgridbayar_Click();
                    if (dgcount == 1)
                    {
                        ((TextBox)GRRP.Rows[dgcount - 1].FindControl("txtjaminanmasuk")).Focus();
                    }
                    else if (dgcount <= 2)
                    {
                        ((TextBox)GRRP.Rows[dgcount - 2].FindControl("txtjaminanmasuk")).Focus();
                    }
                     
                break;
        }
    }
    decimal pokok = 0, bunga = 0, cadangan = 0;
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "nilaitax")); 

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalpokok = (Label)e.Row.FindControl("lbltotaltax");
            lbltotalpokok.Text = string.Format("{0:N2}", txtjmltax.Text);

           
        }
    }

    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        Global gb = new Global();
        GlobalEntities ge = new GlobalEntities();

        Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_getCustomer_Jaminan_Masuk", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    //private void SetDetailBayar()
    //{

    //    //DataRow dr = null;     
    //    dtBayar.Columns.Clear();
    //    dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
    //    dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
    //    dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(string)));
    //    dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
    //    dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));

    //    dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
    //    dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
    //    dtBayar.Columns.Add(new DataColumn("Angsuran", typeof(decimal)));
    //    dtBayar.Columns.Add(new DataColumn("Denda", typeof(decimal)));
    //    dtBayar.Columns.Add(new DataColumn("Bayar", typeof(string)));



    //    ViewState["ListDetailBayar"] = dtBayar;
    //    GRTP.DataSource = dtBayar;
    //    GRTP.DataBind();
    //    GRTP.UseAccessibleHeader = true;
    //    GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

    //    cssGridBayar();
    //}
}
