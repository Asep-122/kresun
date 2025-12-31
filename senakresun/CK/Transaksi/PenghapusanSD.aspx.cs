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
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
public partial class CL_Process_PenghapusanSD : System.Web.UI.Page
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
    string form = "S1";
    string kodesumber = "CL";
    string KodeKonter = "CL10";
    //string xkodejournal = "HTGAS01";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        GRTP.Enabled = true;
      
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        btndelete.Visible = false;
        btnSimpan.Visible = false;
        txtnobukti.Text = "";
        pnladd.Visible = true;
     
        txtBankGiro.Text = "-";
        txtnoGiro.Text = "-";
        txtKeterangan.Text = "-";
        Pnlfooter4.Visible = true;
        Pnlcustomer.Visible = true;

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
                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                txttgljthtmp.Text = txttgltrn.Text;

                //get tanggal server

                //KODE Sumberdana
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
                var3[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSDMSTX", typeof(string));
                var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
                ddlkdsup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkdsup.DataTextField = "NamaBank";
                ddlkdsup.DataValueField = "KodeBank";
                ddlkdsup.DataBind();
                ddlkdsup.Items.Insert(0, new ListItem("Pilih Data", "-"));


                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";

                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "namabank";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();
                ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));
            }

            //pwcabang system kurs single default IDR kurs 1
            if (SessSystemKurs == "False")
            {
                ddlkursSup.Items.Insert(0, new ListItem("IDR"));
                ddlkursSup.SelectedValue = "IDR";
                ddlkursSup.Enabled = false;
                txtkursstd.Text = "1";
                txtkursstd.Enabled = false;

                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = false;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = false;
            }
            else
            {
                ddlkursSup.Items.Insert(0, new ListItem("IDR"));
                ddlkursSup.SelectedValue = "IDR";
                ddlkursSup.Enabled = true;
                txtkursstd.Text = "1";
                txtkursstd.Enabled = true;

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
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

            txtkodejournal.Text=ge.ListDataset.Tables[0].Rows[0]["kodejurnal"].ToString();

            txtNmBank.Text = NamaBank;
            ddlkursbank.SelectedValue = kdcrc;
            txtkursbank.Text = "1";
        }
        else
        {
            txtkodejournal.Text = "";
            txtNmBank.Text = "";
            ddlkursbank.ClearSelection();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";
        }
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBUkti desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void insertcombo()
    {
        //KODE Sumberdana
        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
        var3[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var3[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSDMST", typeof(string));
        var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var3, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
        ddlkdsup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdsup.DataTextField = "NamaBank";
        ddlkdsup.DataValueField = "KodeBank";
        ddlkdsup.DataBind();
        ddlkdsup.Items.Insert(0, new ListItem("Pilih Data", "-"));
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sup = "";
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":

                try
                {
                    insertcombo();
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    GRTP.Enabled = false;
                     
                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbluserentry")).Text;
                    txtnamanasabah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmnasabah")).Text;
                    txtnobukti.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti1")).Value;
                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    HDSup.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                  //  ddlkdsup.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    ddlKdBank.SelectedValue = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Value;
                    ddlKdBank.Enabled = false;
                 
                    txtkursstd.Enabled = false;
                    ddlkursSup.Enabled = false;
                   
                  
                    txttransAP.Enabled = false;
                    //txtjmlhGL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhGL")).Text;
                    txttransAP.Text = string.Format("{0:N2}",decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtjmlhGL.Text = string.Format("{0:N2}",  decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    txtkodejournal.Text = "";//((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejurnal")).Text == "";
                    txtkodejournal.Enabled = false;
                    //txtjmlhGL.Enabled = false;

                    //txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtjmlhCL.Enabled = false;
                    txtKeterangan.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Value;
                    txtKeterangan.Enabled = false;

                    txtpenalty.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpenalty")).Text));
                    txtpenalty.Enabled = false;
                    //txtBankGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbankgiro")).Text;
                    //txtnoGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnogiro")).Text;


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
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var, "constrmf");
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
                    btnCancel.Visible = true;
                    btndelete.Visible = true;
                    btnSimpan.Visible = false;
                    var getkdsupplier = sup;
                    //string ambilnasabah = txtnasabah.Text.Substring(11, 14);
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];
                    var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, HDSup.Value, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTRANSAKSISDEDIT", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));

                    var1[6] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", var1, "constrmf").Tables[0];
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", var1, "constrmf");
                    ddlkursSup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
                    txtkodelgn.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jumlahtagihan"].ToString()));

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        ViewState["ListDetailBayar"] = ge.ListDataTable;

                        GRTP.DataSource = ge.ListDataTable;
                        GRTP.DataBind();

                        GRTP.UseAccessibleHeader = true;
                        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        cssGridBayar();
                        pnltrn.Visible = true;
                    }
                    else
                    {
                        //SetDetailBayar();
                    }
                    //DefaultClass.Global.SqlHelper[] varlgn = new DefaultClass.Global.SqlHelper[5];
                    //varlgn[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdsup.SelectedValue, typeof(string));
                    //varlgn[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARISDMST", typeof(string));
                    //varlgn[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //varlgn[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    //varlgn[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", varlgn, "constrmf");

                    //txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                    //ddlkursSup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
                    //txtkursstd.Text = "1";


                    //DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[6];
                    //Paramst[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    //Paramst[1] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    //Paramst[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTINVOICE", typeof(string));
                    //Paramst[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //Paramst[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //Paramst[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                    //ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", Paramst, "constrmf").Tables[0];

                    //    if (ge.ListDataTable.Rows.Count > 0)
                    //    {

                    //        ViewState["ListDetailBayar"] = ge.ListDataTable;

                    //        GRTP.DataSource = ge.ListDataTable;
                    //        GRTP.DataBind();

                    //        GRTP.UseAccessibleHeader = true;
                    //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                    //        cssGridBayar();
                    //    }
                    //    else
                    //    {
                    //        SetDetailBayar();
                    //    }

                    // GRTP.Enabled = false;





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
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
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
            case "SLIP":

                try
                { 
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti1")).Text;
                    typetrn = "B";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPSD", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", Paramsc, "constrmf").Tables[0];

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
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            var bank = ddlkdsup.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARISDMST", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var, "constrmf");

            //txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            ddlkursSup.SelectedValue = "IDR";// ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            txtkursstd.Text = "1";
            pnltrn.Visible = true;
            btnSimpan.Visible = true;
            SetDetailBayar();

            var getkdsupplier = ddlkdsup.SelectedValue;
            string ambilnasabah = txtnasabah.Text.Substring(11, 12);
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, getkdsupplier, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTRANSAKSISD", typeof(string));
            var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, ambilnasabah, typeof(string));


            ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", var1, "constrmf").Tables[0];
            ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", var1, "constrmf"); 
            ddlkursSup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            txtkodelgn.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();

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
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {


            decimal jumlahAP = decimal.Parse(txttransAP.Text);



            if (jumlahAP <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
            }


            else
            {
                dtBayar.Columns.Clear();

                dtBayar.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("NoFaktur", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("TglTrnFaktur", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("TypeTrn", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Keterangan", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("jmlhtrn", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("jmlbayar", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("KursStd", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("denda", typeof(decimal)));


                dtBayar.Rows.Clear();
                int xi = 0;
                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    //CheckBox ChkBoxHeader = (CheckBox)GRTP.Rows[i].FindControl("CheckBox1");
                    //if (ChkBoxHeader.Checked == true)
                    //{
                    xi = xi + 1;
                    DataRow dr1 = dtBayar.NewRow();
                    decimal xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs")).Text);
                    decimal xbayar = -1 * decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text); //decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhdibayar")).Text);
                    decimal xkurs = decimal.Parse(txtkursbank.Text);//decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text);
                    decimal xtrans = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text);

                    decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblPokok")).Text);
                    decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblBunga")).Text);
                    decimal xdenda = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lbldenda")).Text);
                    DateTime tgltrn = DateTime.Parse(txttgltrn.Text);//DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltglfaktur")).Text);
                    DateTime tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                    dr1["nomor"] = xi;
                    dr1["NoFaktur"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                    dr1["TglTrnFaktur"] = tgltrn;
                    dr1["TglJthTmp"] = tgljthtempo;
                    dr1["TypeTrn"] = "1"; //((Label)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
                    dr1["Keterangan"] = txtKeterangan.Text;//((Label)GRTP.Rows[i].FindControl("lblketJPF")).Text;
                    dr1["jmlhtrn"] = xtrans;
                    dr1["jmlbayar"] = xbayar;
                    dr1["KursStd"] = xkurs;
                    dr1["pokok"] = xpokok;
                    dr1["bunga"] = xbunga;
                    dr1["denda"] = xdenda;
                    dr1["angske"] = xangske;
                    dtBayar.Rows.Add(dr1);
                    // }
                }

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[27];

                Params[0] = gb.CreateSPParameter("@Typejournal", ParameterDirection.Input, SqlDbType.VarChar, 50, '1', typeof(string));
                Params[1] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[2] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkursSup.SelectedValue, typeof(string));
                Params[3] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkursstd.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[6] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(DateTime));
                Params[7] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[8] = gb.CreateSPParameter("@Kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodejournal.Text, typeof(string));
                Params[9] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdsup.SelectedValue, typeof(string));
                Params[10] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[12] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[13] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[14] = gb.CreateSPParameter("@CL_Transaction_DeleteSD", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[15] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[16] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[17] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[18] = gb.CreateSPParameter("@bankgiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtBankGiro.Text, typeof(string));
                Params[19] = gb.CreateSPParameter("@nogiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoGiro.Text, typeof(string));
                Params[20] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));
                Params[21] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNmSup.Text, typeof(string));
                Params[22] = gb.CreateSPParameter("@Kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 30, HDSup.Value, typeof(string));
                Params[23] = gb.CreateSPParameter("@namanasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnamanasabah.Text, typeof(string));
                Params[24] = gb.CreateSPParameter("@KursAkhir", ParameterDirection.Input, SqlDbType.Money, 0, txtkursbank.Text, typeof(decimal));
                Params[25] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[26] = gb.CreateSPParameter("@penalty", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpenalty.Text, typeof(decimal));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_SD_Delete_Penghapusan_SD", Params, "constrmf");
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
                    btnCancel.Visible = false;
                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','zzzzzz','bottom-center','success','true');", true);

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
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            decimal jumlahAP = decimal.Parse(txttransAP.Text);

            if (jumlahAP<=0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
            }
            else if(ddlKdBank.SelectedValue == "-")
            {
                ddlKdBank.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Kode Bank Terlebih dahulu');", true);
            }
            else if(ddlKdBank.SelectedValue != "-" && jumlahAP > 0)
            {
                dtBayar.Columns.Clear();

                dtBayar.Columns.Add(new DataColumn("nomor", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("NoFaktur", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("TglTrnFaktur", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("TypeTrn", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Keterangan", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("jmlhtrn", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("jmlbayar", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("KursStd", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("denda", typeof(decimal)));


                dtBayar.Rows.Clear();
                int xi = 0;
                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    //CheckBox ChkBoxHeader = (CheckBox)GRTP.Rows[i].FindControl("CheckBox1");
                    //if (ChkBoxHeader.Checked == true)
                    //{
                    xi = xi + 1;
                    DataRow dr1 = dtBayar.NewRow();
                    decimal xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs")).Text);
                    decimal xbayar = -1 * decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text); //decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhdibayar")).Text);
                    decimal xkurs = decimal.Parse(txtkursbank.Text);//decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text);
                    decimal xtrans = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text);

                    decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblPokok")).Text);
                    decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblBunga")).Text);
                    decimal xdenda = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lbldenda")).Text);
                    DateTime tgltrn = DateTime.Parse(txttgltrn.Text);//DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltglfaktur")).Text);
                    DateTime tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                    dr1["nomor"] = xi;
                    dr1["NoFaktur"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                    dr1["TglTrnFaktur"] = tgltrn;
                    dr1["TglJthTmp"] = tgljthtempo;
                    dr1["TypeTrn"] = "1"; //((Label)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
                    dr1["Keterangan"] = txtKeterangan.Text;//((Label)GRTP.Rows[i].FindControl("lblketJPF")).Text;
                    dr1["jmlhtrn"] = xtrans;
                    dr1["jmlbayar"] = xbayar;
                    dr1["KursStd"] = xkurs;
                    dr1["pokok"] = xpokok;
                    dr1["bunga"] = xbunga;
                    dr1["denda"] = xdenda;
                    dr1["angske"] = xangske;
                    dtBayar.Rows.Add(dr1);
                   // }
                }

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[27];

                Params[0] = gb.CreateSPParameter("@Typejournal", ParameterDirection.Input, SqlDbType.VarChar, 50, '1', typeof(string));
                Params[1] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[2] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkursSup.SelectedValue, typeof(string));
                Params[3] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkursstd.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[6] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(DateTime));
                Params[7] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[8] = gb.CreateSPParameter("@Kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodejournal.Text, typeof(string));
                Params[9] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdsup.SelectedValue, typeof(string));
                Params[10] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[12] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[13] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[14] = gb.CreateSPParameter("@CL_Transaction_DeleteSD", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[15] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[16] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[17] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[18] = gb.CreateSPParameter("@bankgiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtBankGiro.Text, typeof(string));
                Params[19] = gb.CreateSPParameter("@nogiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoGiro.Text, typeof(string));
                Params[20] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));
                Params[21] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNmSup.Text, typeof(string));
                Params[22] = gb.CreateSPParameter("@Kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdsup.SelectedValue, typeof(string));
                Params[23] = gb.CreateSPParameter("@namanasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnamanasabah.Text, typeof(string));
                Params[24] = gb.CreateSPParameter("@KursAkhir", ParameterDirection.Input, SqlDbType.Money, 0, txtkursbank.Text, typeof(decimal));
                Params[25] = gb.CreateSPParameter("@NamaBank", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNmBank.Text, typeof(string));
                Params[26] = gb.CreateSPParameter("@penalty", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtpenalty.Text), typeof(decimal));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Delete_SD_Insert", Params, "constrmf");
                //if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

                //}
                //else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

                //}

                //else 
                if (ge.ListDataset.Tables[1].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    btnCancel.Visible = false;
                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','zzzzzz','bottom-center','success','true');", true);

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
    protected void ddlKdSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //var bank = ddlkdsup.SelectedValue;
            //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            //var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
            //var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARISDMST", typeof(string));
            //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_DELETE_SD", var, "constrmf");

            //txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            //ddlkursSup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            //txtkursstd.Text = "1";
            //pnltrn.Visible = true;
            //btnSimpan.Visible = true;
            //SetDetailBayar();

            //var getkdsupplier = ddlkdsup.SelectedValue;
            //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            //var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, getkdsupplier, typeof(string));
            //var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            //var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            //var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTRANSAKSISD", typeof(string));
            //var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            //ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_DELETE_SD", var1, "constrmf").Tables[0];

            //if (ge.ListDataTable.Rows.Count > 0)
            //{

            //    ViewState["ListDetailBayar"] = ge.ListDataTable;

            //    GRTP.DataSource = ge.ListDataTable;
            //    GRTP.DataBind();

            //    GRTP.UseAccessibleHeader = true;
            //    GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

            //    cssGridBayar();
            //}
            //else
            //{
            //    //SetDetailBayar();
            //}

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlbayar_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;

        if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "0")
        {
            ((TextBox)grow.FindControl("lblbayar")).Text = "0";
            ((TextBox)grow.FindControl("lblbayar")).Enabled = false;
        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "1")
        {
            decimal angsuran = 0;//= decimal.Parse(txtsaldotitipan.Text) - decimal.Parse(txtbayar.Text);
            decimal declbljmlhtrn = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            if (angsuran < declbljmlhtrn)
            {
                ((DropDownList)grow.FindControl("ddlbayar")).SelectedValue = "0";
                ((TextBox)grow.FindControl("lblbayar")).Text = "0";
                ((TextBox)grow.FindControl("lblbayar")).Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Saldo Titipan kurang dari Angsuran!');", true);
            }
            else
            {
                ((TextBox)grow.FindControl("lblbayar")).Text = ((Label)grow.FindControl("lblAngsuran")).Text;
                ((TextBox)grow.FindControl("lblbayar")).Enabled = false;
            }
        }

        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "2")
        {
            decimal angsuran = 0;
            decimal denda = 0;
            decimal total = 0;

            angsuran = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            denda = decimal.Parse(((Label)grow.FindControl("lbldenda")).Text);
            total = angsuran + denda;
            ((TextBox)grow.FindControl("lblbayar")).Text = string.Format("{0:N2}", total);
        }

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            txttgljthtmp.Text = txttgltrn.Text;
        }
        txttgljthtmp.Text = txttgltrn.Text;
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
            case "rubah":
                try
                {
                    txttransAP.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAngsuran")).Text;
                }
                catch (Exception X)
                {
                    if (X.Message != "Thread was being aborted.")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }
                }

                break;

        }
    }
    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, fee = 0, bayar = 0, total = 0;

    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pcof"));
                bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bcof"));
                angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
                denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda")); 


              
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
                lbltotalPokok.Text = string.Format("{0:N2}", pokok);

                Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
                lbltotalBunga.Text = string.Format("{0:N2}", bunga);

                Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
                lbltotalAngsuran.Text = string.Format("{0:N2}", angsuran);


                txttransAP.Text = string.Format("{0:N2}", decimal.Parse(lbltotalAngsuran.Text));

                //Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
                //lbltotaldenda.Text = string.Format("{0:N2}", denda); 
                //total = pokok + bunga + fee;
                //HDBUnga.Value = lbltotalBunga.Text;
                //HDPokok.Value = lbltotalPokok.Text;
                //HDFee.Value = lbltotalfeec.Text;
                //txtTotTO1.Text = lbltotalAngsuran.Text;
                //txtTotTO.Text = lbltotalAngsuran.Text;
                //txtJlhPenerimaan.Text = lbltotalAngsuran.Text;
                //txtselisih.Text = "0";
                //HDtotal.Value = total.ToString();
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
                //CheckBox ChkBoxHeader = (CheckBox)e.Row.FindControl("CheckBox1");
                ////GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
                //if (ChkBoxHeader.Checked == true)
                //{
                //    ((TextBox)e.Row.FindControl("status_pembayaran")).Text = ((Label)e.Row.FindControl("lbljmlhtrn")).Text;
                //        ((TextBox)e.Row.FindControl("status_pembayaran")).Enabled = true;
                //}
                //else
                //{
                //    ((TextBox)e.Row.FindControl("status_pembayaran")).Text = "0";
                //     ((TextBox)e.Row.FindControl("status_pembayaran")).Enabled = false;
                // }

                //decimal decjlhdibayar = decimal.Parse(((TextBox)e.Row.FindControl("status_pembayaran")).Text);
                //decimal declbljmlhtrn = decimal.Parse(((Label)e.Row.FindControl("lbljmlhtrn")).Text);

                //if  (decjlhdibayar > declbljmlhtrn)
                //{
                //    ((TextBox)e.Row.FindControl("status_pembayaran")).Text = ((Label)e.Row.FindControl("lbljmlhtrn")).Text;
                //}

                //GRTP.UseAccessibleHeader = true;
                //GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

            //} 
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
        cells[8].Attributes.Add("data-class", "expand");
        //cells[9].Attributes.Add("data-class", "expand");

    }
    private void SetDetailBayar()
    {

        //DataRow dr = null;
        dtBayar.Clear();
        dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("angs-ke", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("tgl_jwl", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("kode_produk", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("pcof", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("bcof", typeof(decimal)));

        //dtBayar.Columns.Add(new DataColumn("denda", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("denda", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));


        //dr = dtBayar.NewRow();
        //dr["rownumber"] = 1;
        //dr["TglTrnFaktur"] = string.Empty;
        //dr["NoFaktur"] = string.Empty;
        //dr["TypeTrn"] = string.Empty;
        //dr["TglJthTmp"] = string.Empty;
        //dr["Keterangan"] = string.Empty;
        //dr["jmlhtrn"] = decimal.Zero;
        //dr["flagbyr"] = bool.FalseString;
        //dr["jmlbayar"] = decimal.Zero;
        //dr["KursStd"] = decimal.Zero;

        //dtBayar.Rows.Add(dr);

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
            txttgljthtmp.Text = txttgltrn.Text;
        }
        //txttgljthtmp.Text = txttgltrn.Text;
    }
    protected void ddlkursSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkursstd.Text = ddlkursSup.SelectedValue;
    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal bayar, jumlahtrn;
        bayar = decimal.Parse(((TextBox)grow.FindControl("jlhdibayar")).Text);
        jumlahtrn = decimal.Parse(((Label)grow.FindControl("lbljmlhtrn")).Text);
        if (bayar > 0)
        {          

            if (bayar > jumlahtrn)
            {
                ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lbljmlhtrn")).Text;
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
    protected void idcekaction_Change(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox cek = (CheckBox)GRTP.Rows[Convert.ToInt32(index)].FindControl("idcekaction");
            string masuk = ((Label)GRTP.Rows[Convert.ToInt32(index)].FindControl("lblangsuran")).Text;
            //bool cek = Convert.ToBoolean(asm);
            //txttransAP.Text = ((Label)GRTP.Rows[Convert.ToInt32(index)].FindControl("lblangsuran")).Text;
            if (cek.Checked)
            {
                txttransAP.Text = string.Format("{0:N2}", (decimal.Parse(masuk) + decimal.Parse(txttransAP.Text)));
            }
            else
            {
                txttransAP.Text = string.Format("{0:N2}", (decimal.Parse(txttransAP.Text) - decimal.Parse(masuk)));
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    [WebMethod]
    public static string GetNasabah(string InputText, string SessKdpershx, string SessBranchIDx, string kodesumber)
    {
        if (kodesumber == "-")
        {
            //System.Console.WriteLine("Pilih Kode Sumberdana Terlebih Dahulu");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
            string alert = "Pilih Kode Sumberdana Terlebih Dahulu";
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert(" + alert + ")</SCRIPT>");
        }
        else
        {
            List<string[]> listResult = new List<string[]>();

            // empResult.Add(KDBRG);
            DefaultClass.Global gb = new DefaultClass.Global();
            EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
            Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
            Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));
            Params[3] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 30, kodesumber, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_getNasabah_penghapusan", Params, "constrmf");

            //if(ge.ListDataset.Tables[0].Rows.Count < 0)
            //{
            //    string alert = "Nasabah Dengan Kodesumber Dana "+kodesumber+" tersebut sudah Lunas Semua";
            //    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert(" + alert + ")</SCRIPT>");
            //}
            //else
            //{
                listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(listResult);

                return json;
            //}

        }

        return InputText;
    }
    //protected void chkview_CheckedChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
    //    int index = row.RowIndex;
    //    CheckBox cb1 = (CheckBox)GRTP.Rows[index].FindControl("chkview");
    //    string yourvalue = cb1.Text;
    //    //here you can find your control and get value(Id).

    //}
    //public void dataGridView1_CellValueChanged(object sender,  DataGridViewCellEventArgs e)
    //{
    //    if (dataGridView1.Columns[e.ColumnIndex].Name == "CheckBoxes")
    //    {
    //        DataGridViewDisableButtonCell buttonCell =
    //            (DataGridViewDisableButtonCell)dataGridView1.
    //            Rows[e.RowIndex].Cells["Buttons"];

    //        DataGridViewCheckBoxCell checkCell =
    //            (DataGridViewCheckBoxCell)dataGridView1.
    //            Rows[e.RowIndex].Cells["CheckBoxes"];
    //        buttonCell.Enabled = !(Boolean)checkCell.Value;

    //        dataGridView1.Invalidate();
    //    }
    //}

}
