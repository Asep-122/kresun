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
using System.Drawing.Drawing2D;
public partial class CL_Process_MasukBayarBiayaCrude : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "L4";
    string kodesumber = "CL";
    string KodeKonter = "CL13";
    //string xkodejournal = "HTGAS01";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        GRTP.Enabled = true;
      
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnlFilter.Visible = false;
        btndelete.Visible = false;
        btnSimpan.Visible = false;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        //Pnlfilterx.Visible = true;


        txtKeterangan.Text = "Pengembalian Kasbon Operational";
        Pnlfooter4.Visible = true;
        ddlpenarikanbunga_SelectedIndexChanged(sender, e);
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

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
               
                //DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                //var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                //var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPRODUCT", typeof(string));
                //var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("SN_Trn_BAYAR_BUNGA", var4, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
                //ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlproduk.DataTextField = "nama_produk";
                //ddlproduk.DataValueField = "kode_produk";
                //ddlproduk.DataBind();
                //ddlproduk.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMSTXY", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_BAYAR_BIAYA_CRUD", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "namabank";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();
                //ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlKdBank.SelectedValue = "110191";
                ddlKdBank_SelectedIndexChanged(ddlKdBank, e);

                txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
                Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTeller", Paramx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
                ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));

                string tempuser = (SessBranchID == "000" ? "000TL1" : SessLoginID);
                ddlteller.SelectedValue = tempuser;

                if (tempuser.Substring(3, 3) == "TL1")
                {
                    ddlteller.Enabled = true;
                }
                else
                {
                    ddlteller.Enabled = false;
                }
                BindGridEntity();
            }
            //pwcabang system kurs single default IDR kurs 1
            if (SessSystemKurs == "False")
            {
                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = false;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = false;
            }
            else
            {
                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = true;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = true;
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
    protected void ddlkursBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkursbank.Text = ddlkursbank.SelectedValue;
    }
    protected void ddlKdBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_BAYAR_BIAYA_CRUD", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

            //txtkodejournal.Text=ge.ListDataset.Tables[0].Rows[0]["kodejurnal"].ToString();

            txtNmBank.Text = NamaBank;
            ddlkursbank.SelectedValue = kdcrc;
            txtkursbank.Text = "1";

        }
        else
        {
            //txtkodejournal.Text = "";
            txtNmBank.Text = "";
            ddlkursbank.ClearSelection();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";
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

    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRXBAYARCRUD", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_GetSEARCH", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNON", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_BAYAR_KASBON_NON_BIAYA", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti Desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
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

    decimal total = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltxttotals");
            lbltotal.Text = string.Format("{0:N2}", total);

        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        pnlFilter.Visible = false;
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
                    GRTP.Enabled = false;
                     
                   
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                   
                    //ddlkdsup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeKantorBayar")).Text;
                    //ddlkdsup.Enabled = false;
                    //txtnamakantorbayar.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaKantorBayar")).Text;
                    //txtnamakantorbayar.Enabled = false;
                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlKdBank.Enabled = false;
                 
                    txttransAP.Enabled = false;
                    //txtjmlhGL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhGL")).Text;
                    txttransAP.Text = string.Format("{0:N2}",decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtjmlhGL.Text = string.Format("{0:N2}",  decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    //txtkodejournal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejurnal")).Text;
                    //txtkodejournal.Enabled = false;
                    ////txtjmlhGL.Enabled = false;

                    //txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtjmlhCL.Enabled = false;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtKeterangan.Enabled = false;
                

                    //statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    //statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;

                    //if (statusclosing == "True")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                    //    btndelete.Enabled = false;
                    //}
                    //else
                    //{
                    //    if (statusposting == "True")
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting hubungi pusat!');", true);
                    //        btndelete.Enabled = false;
                    //    }
                    //}
                    var bank = ddlKdBank.SelectedValue;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_BAYAR_BIAYA_CRUD", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                        string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

                        txtNmBank.Text = NamaBank;
                        ddlkursbank.SelectedValue = kdcrc;
                        txtkursbank.Text = "1";
                    }
                    else
                    {
                        txtNmBank.Text = "";
                        ddlkursbank.ClearSelection();
                        ddlkursbank.SelectedValue = "IDR";
                        txtkursbank.Text = "1";
                    }
                    pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;
                    btndelete.Visible = true;
                    btnSimpan.Visible = false;
					
					
					
					  DefaultClass.Global.SqlHelper[] ParamPosting = new DefaultClass.Global.SqlHelper[4];
                    ParamPosting[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    ParamPosting[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    ParamPosting[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    ParamPosting[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKPOSTING", typeof(String));
                    ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", ParamPosting, "constrmf");
                    string pubposting = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                    if (pubposting == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting!');", true);
                        btndelete.Visible = false;
                    }
                    else
                    {
                        btndelete.Visible = true;
                    }
                   

                    DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[6];
                    Paramst[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramst[1] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    Paramst[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTINVOICE", typeof(string));
                    Paramst[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramst[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramst[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_BAYAR_KASBON_NON_BIAYA", Paramst, "constrmf").Tables[0];

                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            ViewState["ListDetailBayar"] = ge.ListDataTable;

                            GRTP.DataSource = ge.ListDataTable;
                            GRTP.DataBind();

                            GRTP.UseAccessibleHeader = true;
                            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                            cssGridBayar();
                        }
                        else
                        {
                            SetDetailBayar();
                        }
                     GRTP.Enabled = false;

                  

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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SN_Trn_BAYAR_BUNGA", Paramsc, "constrmf").Tables[0];

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
                    typetrn = "K";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAS", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SN_Trn_BAYAR_BUNGA", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAP.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        if (typetrn == "B")
                        {
                            rprt.SetParameterValue("Reportname", "Terima Cash Bank");
                        }
                        else
                        {
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                        }

                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
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
                break;
        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {
            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txttransAP.Text);

            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
            }
            else
            {
               
                dtBayar.Columns.Clear();

                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("tgl_drop", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("tgl_due", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("bungake", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("nama_nasabah", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("BankRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("NoRekeningBank", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("AtasNama", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("urut", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("nobuktibunga", typeof(string)));

                dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");

                    if (ddlbayar.SelectedValue == "1")
                    {
                        DataRow dr1 = dtBayar.NewRow();
                        xi = xi + 1;
                        int bungake = 1;

                        decimal xBayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhdibayar")).Text);
                        decimal xBunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblangsuran")).Text); ;

                        int urut = xi;

                        DateTime tgl_drop = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lblTglTrn")).Text);
                        DateTime tgl_due = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lblTglTrn")).Text);

                        dr1["No"] = xi;
                        dr1["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblno_rekening")).Text;
                        dr1["tgl_drop"] = tgl_drop;
                        dr1["tgl_due"] = tgl_due;
                        dr1["bungake"] = bungake;
                        dr1["nama_nasabah"] = ((Label)GRTP.Rows[i].FindControl("lblnama_Biaya")).Text;
                        dr1["BankRekening"] = "-";
                        dr1["NoRekeningBank"] = "-";
                        dr1["AtasNama"] = "-";
                        dr1["Bayar"] = xBayar;
                        dr1["TypeBayar"] = 1;
                        dr1["Bunga"] = xBunga;
                        dr1["urut"] = urut;
                        dr1["nobuktibunga"] = ((Label)GRTP.Rows[i].FindControl("lblnobuktibunga")).Text;
                        dtBayar.Rows.Add(dr1);
                    }
                }

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[14];

                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[3] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[4] = gb.CreateSPParameter("@NmBank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmBank.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[11] = gb.CreateSPParameter("@SN_LIST_BUNGA_AR", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[12] = gb.CreateSPParameter("@Kettransfer", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                Params[13] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_BAYAR_BIAYA_KASBON_NON_Delete", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);
                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','Data Berhasil Terhapus','bottom-center','success','true');", true);
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
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "redirect",
            "window.location.href='ReturMasukTrnCashAdvanceNonBiaya.aspx';",
            true
        );
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void ddlpenarikanbunga_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        { 
            
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBIAYA", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


            ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_BAYAR_KASBON_NON_BIAYA", var, "constrmf").Tables[0];
            pnltrn.Visible = true;
            btnSimpan.Visible = true;
            //SetDetailBayar();
            if (ge.ListDataTable.Rows.Count > 0)
            {

                ViewState["ListDetailBayar"] = ge.ListDataTable;

                GRTP.DataSource = ge.ListDataTable;
                GRTP.DataBind();

                GRTP.UseAccessibleHeader = true;
                GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridBayar();
            }
            else
            {
                //SetDetailBayar();
            }
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txttransAP.Text);
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

                if (jlhdibayar <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
                    btnSimpan.Enabled = true;
                }
                else if (ddlKdBank.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank Masih Kosong);", true);
                    btnSimpan.Enabled = true;
                }
                else
                {

                    dtBayar.Columns.Clear();

                    dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                    dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("tgl_drop", typeof(DateTime)));
                    dtBayar.Columns.Add(new DataColumn("tgl_due", typeof(DateTime)));
                    dtBayar.Columns.Add(new DataColumn("bungake", typeof(int)));
                    dtBayar.Columns.Add(new DataColumn("nama_nasabah", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("BankRekening", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("NoRekeningBank", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("AtasNama", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
                    dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));
                    dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
                    dtBayar.Columns.Add(new DataColumn("urut", typeof(int)));
                    dtBayar.Columns.Add(new DataColumn("nobuktibunga", typeof(string)));

                    dtBayar.Rows.Clear();
                    int xi = 0;

                    for (int i = 0; i < GRTP.Rows.Count; i++)
                    {
                        DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");

                        if (ddlbayar.SelectedValue == "1")
                        {
                            DataRow dr1 = dtBayar.NewRow();
                            xi = xi + 1;
                            int bungake = 1;

                            decimal xBayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhdibayar")).Text);
                            decimal xBunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblangsuran")).Text); ;

                            int urut = xi;

                            DateTime tgl_drop = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lblTglTrn")).Text);
                            DateTime tgl_due = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lblTglTrn")).Text);

                            dr1["No"] = xi;
                            dr1["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblno_rekening")).Text;
                            dr1["tgl_drop"] = tgl_drop;
                            dr1["tgl_due"] = tgl_due;
                            dr1["bungake"] = bungake;
                            dr1["nama_nasabah"] = ((Label)GRTP.Rows[i].FindControl("lblnama_Biaya")).Text;
                            dr1["BankRekening"] = "-";
                            dr1["NoRekeningBank"] = "-";
                            dr1["AtasNama"] = "-";
                            dr1["Bayar"] = xBayar;
                            dr1["TypeBayar"] = 1;
                            dr1["Bunga"] = xBunga;
                            dr1["urut"] = urut;
                            dr1["nobuktibunga"] = ((Label)GRTP.Rows[i].FindControl("lblnobuktibunga")).Text;
                            dtBayar.Rows.Add(dr1);
                        }
                    }
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[17];

                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                    Params[3] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
                    Params[4] = gb.CreateSPParameter("@penarikan", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
                    Params[5] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                    Params[6] = gb.CreateSPParameter("@NmBank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmBank.Text, typeof(string));
                    Params[7] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[8] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[9] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[10] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[12] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[13] = gb.CreateSPParameter("@SN_LIST_BUNGA_AR", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                    Params[14] = gb.CreateSPParameter("@Kettransfer", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                    Params[15] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    Params[16] = gb.CreateSPParameter("@pathbuktitrn", ParameterDirection.Input, SqlDbType.VarChar, 100, "", typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_BAYAR_BIAYA_KASBON_NON_Save", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                        btnSimpan.Enabled = true;
                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);
                        btnSimpan.Enabled = true;
                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;
                        //nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','Data Berhasil Tersimpan','bottom-center','success','true');", true);

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
    protected void ddlKdSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        

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
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DEL":
                int dgcount = GRTP.Rows.Count;


                ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRTP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
                        drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                        drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
                        drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
                        drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
                        drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
                        drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }
                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["TglTrnFaktur"] = string.Empty;
                    drdnew["NoFaktur"] = string.Empty;
                    drdnew["TypeTrn"] = string.Empty;
                    drdnew["TglJthTmp"] = string.Empty;
                    drdnew["Keterangan"] = string.Empty;
                    drdnew["JumlahTrn"] = decimal.Zero;
                    drdnew["KursStd"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }
                ViewState["ListDetailRTP"] = dtRP;
                 
                GRTP.DataSource = dtRP;
                GRTP.DataBind();

                GRTP.UseAccessibleHeader = true;
                GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
                cssGridBayar();
                break;
        }
    }
    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (blnadd == 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (DataBinder.Eval(e.Row.DataItem, "Tipebayar").ToString() == "1")
                {
                    bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));
                string formatted = string.Format("{0:N2}", bayar);
                txttransAP.Text = formatted;
                }
            }
        }
    }
    private void cssGridBayar()
    {
        // Attribute to show the Plus Minus Button.
        GRTP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRTP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-class", "expand");
        cells[4].Attributes.Add("data-class", "expand");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand");


    }
    private void SetDetailBayar()
    {
        //DataRow dr = null;
        dtBayar.Clear();
        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("tgl_drop", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("tgl_due", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("bungake", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("nama_nasabah", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("BankRekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("NoRekeningBank", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("AtasNama", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("urut", typeof(int)));

       

        ViewState["ListDetailBayar"] = dtBayar;
        GRTP.DataSource = dtBayar;
        GRTP.DataBind();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
    }
    protected void GetKurs_textchanged(object sender, EventArgs e)
    {

        //get kurs SUPLLIER for dropdown and textbox
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
            //txttgljthtmp.Text = txttgltrn.Text;
        }
        //txttgljthtmp.Text = txttgltrn.Text;
    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal bayar, jumlahtrn;
        bayar = decimal.Parse(((TextBox)grow.FindControl("jlhdibayar")).Text);
        jumlahtrn = decimal.Parse(((Label)grow.FindControl("lblangsuran")).Text);
        if (bayar > 0)
        {          

            if (bayar > jumlahtrn)
            {
                ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lblangsuran")).Text;
            }
            subgridAP_Click();

        }
        else
        {
            
            ((TextBox)grow.FindControl("jlhdibayar")).Text = "0";
            subgridAP_Click();
            //((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            //((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
        }
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void ddlbayar_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "0")
        {
            ((TextBox)grow.FindControl("jlhdibayar")).Text = "0";
            ((TextBox)grow.FindControl("jlhdibayar")).Enabled = false;

        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "1")
        {
            ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lblangsuran")).Text;
            ((TextBox)grow.FindControl("jlhdibayar")).Enabled = true;
        }

        

        subgridbayar_Click();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    private void subgridbayar_Click()
    {

        int dgcount = GRTP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {


            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhdibayar")).Text);
                sumjlhgl = sumjlhgl + jlhgl;
            }



        }


        else
        {
            sumjlhgl = 0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
        txttransAP.Text = formatted;




    }
    protected void Check_Clicked(object sender,EventArgs e)
    { 
        if (blnadd == 0)
        {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (((CheckBox)grow.FindControl("CheckBox1")).Checked)
        {
            ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lbljmlhtrn")).Text;
                ((TextBox)grow.FindControl("jlhdibayar")).Enabled = true;
        }
        else
        {
            ((TextBox)grow.FindControl("jlhdibayar")).Text = "0";
                ((TextBox)grow.FindControl("jlhdibayar")).Enabled = false;
            }

        subgridAP_Click();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }
    private void subgridAP_Click()
    {
        int dgcount = GRTP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {


            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhdibayar")).Text);
                sumjlhgl = sumjlhgl + jlhgl;
            }



        }


        else
        {
            sumjlhgl = 0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
       
        txttransAP.Text = formatted;
        //txtjmlhCL.Text = formatted;

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;


    }
   
}
