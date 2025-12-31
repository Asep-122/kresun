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


public partial class CL_Process_MasukTrnlain : System.Web.UI.Page
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
    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "C1";
    string kodesumber = "CL";
    string KodeKonter = "CL05";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;
        //GRRP.Enabled = true;
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnlFilter.Visible = false;
        
        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        
        //pnltypejurnal.Visible = true;
        ddltypejur_SelectedIndexChanged(sender, e);
        //txtKeterangan.Text = "-";
        Pnlfooter4.Visible = true;
        btnclose.Visible = true;
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
                txttgljthtmp.Text = txttgltrn.Text;

                //get tanggal server

               

                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "namabank";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();
                ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlKdBank.SelectedValue = "110191";

               

                txtNmBank.Text = "Kas Head Teller";


                //TYPETRANSAKSI tidak perlu pake kode company dan kode cabang
                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[2];
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSUTYPECL", typeof(string));
                var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn asc";

                ddlTypeTrn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlTypeTrn.DataTextField = "TypeTrans";
                ddlTypeTrn.DataValueField = "TypeTrn";
                ddlTypeTrn.DataBind();
                ddlTypeTrn.Items.Insert(0, new ListItem("Pilih Data", "-"));

                DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[5];
                var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLJURNALLAIN", typeof(string));
                var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var6[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var6[4] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTypeTrn.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var6, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";
                ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlJurnal.DataTextField = "NamaJurnal";
                ddlJurnal.DataValueField = "KodeJurnal";
                ddlJurnal.DataBind();
                ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlTypeTrn.SelectedValue = "B";
                ddljurnal_SelectedIndexChanged(sender, e);
               
                if (ddlTypeTrn.SelectedValue == "B")
                {
                    txtKeterangan.Text = "Terima Lain-Lain";
                }
                else
                {
                    txtKeterangan.Text = "Keluar Lain-Lain";
                }
                //ddljenisTrn_SelectedIndexChanged(sender, e);
                txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
                Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTeller", Paramx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
                ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));

                ddlteller.SelectedValue = SessLoginID;
                if (SessLoginID.Substring(3, 3) == "TL1")
                {
                    ddlteller.Enabled = true;
                }
                else
                {
                    ddlteller.Enabled = false;
                }
                BindGridEntity();




            }

            txtBankGiro.Text = "-";
            txtnoGiro.Text = "-";
            ddlkursbank.Items.Insert(0, new ListItem("IDR"));
            ddlkursbank.SelectedValue = "IDR";
            ddlkursbank.Enabled = false;
            txtkursbank.Text = "1";
            txtkursbank.Enabled = false;
            //pwcabang system kurs single default IDR kurs 1
          
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

    protected void ddljurnal_SelectedIndexChanged(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[5];
        var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLJURNALLAIN", typeof(string));
        var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var6[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var6[4] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTypeTrn.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var6, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";
        ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlJurnal.DataTextField = "NamaJurnal";
        ddlJurnal.DataValueField = "KodeJurnal";
        ddlJurnal.DataBind();
        ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));       

        if (ddlTypeTrn.SelectedValue == "B")
        {
            txtKeterangan.Text = "Terima Lain-Lain";
        }
        else
        {
            txtKeterangan.Text = "Keluar Lain-Lain";
        }
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
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var, "constrmf");
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

        if (ddlTypeTrn.SelectedValue == "B")
        {
            txtDebit1.Text = ddlKdBank.SelectedValue;
            txtDebit3.Text = ddlKdBank.SelectedItem.Text;
        }
        else
        {
            txtKredit1.Text = ddlKdBank.SelectedValue;
            txtKredit3.Text = ddlKdBank.SelectedItem.Text;

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
        Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRXLAINLAIN", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_GetSEARCH", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    public void BindGridEntity()
    {
        try

        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

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
                    //GRTP.Enabled = false;
                   
                    btnSimpan.Visible = true;
                    btndelete.Visible = false;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    // txttgltrn.Enabled = false;
                    ddlTypeTrn.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTypeTrn")).Text;

                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    //ddlKdBank.Enabled = false;
               
                   
                  
                    //txttransAP.Enabled = true;
                    //txtjmlhGL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhGL")).Text;
                    txttransAP.Text = string.Format("{0:N2}",decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    //txtKeterangan.Enabled = false;
                    txtBankGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbankgiro")).Text;
                    txtnoGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnogiro")).Text;

                    var bank = ddlKdBank.SelectedValue;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var, "constrmf");
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
                    
                   



                    ddltypejur.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypejurnal")).Text;
                 //   ddltypejur.Enabled = false;
                    Pnlfooter4.Visible = true;
                    //pnltypejurnal.Visible = true;
                    string datajurnal=((Label) GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejurnal")).Text;
                    DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[5];
                    var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLJURNALLAIN", typeof(string));
                    var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var6[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var6[4] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTypeTrn.SelectedValue, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var6, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";
                    ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlJurnal.DataTextField = "NamaJurnal";
                    ddlJurnal.DataValueField = "KodeJurnal";
                    ddlJurnal.DataBind();
                    ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddlJurnal.SelectedValue = datajurnal;
                    if (ddltypejur.SelectedValue == "1")
                    { 

                        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
                        
                        varT[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlJurnal.SelectedValue, typeof(string));
                        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIClJURNAL", typeof(string));
                        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varT[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varT, "constrmf");

                        string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
                        string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
                        string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
                        string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
                        string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
                        txtJurnal.Text = KetJurnal;
                        txtDebit1.Text = DebitGL;
                        txtDebit2.Text = DebitCC;
                        txtKredit1.Text = KreditGL;
                        txtKredit2.Text = KreditCC;

                        //GET KETERANGAN GL
                        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[5];
                        varGL[0] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 40, DebitGL, typeof(string));
                        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
                        varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varGL[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8000, SessBranchID, typeof(string));
                        varGL[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
               
                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varGL, "constrmf");
                        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
                        txtDebit3.Text = DCC;

                        //GET KETERANGAN CC
                        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[5];
                        varCC[0] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 40, KreditGL, typeof(string));
                        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
                        varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varCC[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8000, SessBranchID, typeof(string));
                        varCC[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varCC, "constrmf");
                        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
                        txtKredit3.Text = KGL;
                    }


                    if (ddlTypeTrn.SelectedValue == "B")
                    {
                        txtDebit1.Text = ddlKdBank.SelectedValue;
                        txtDebit3.Text = ddlKdBank.SelectedItem.Text;

                    }
                    else
                    {
                        txtKredit1.Text = ddlKdBank.SelectedValue;
                        txtKredit3.Text = ddlKdBank.SelectedItem.Text;

                    }

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
                        btnSimpan.Visible = false;
                    }
                    else
                    {
                        btnSimpan.Visible = true;
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

                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;

                    btnSimpan.Visible = false;
                    btndelete.Visible = true;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;


                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdbank")).Text;
                    ddlKdBank.Enabled = false;

                    ddlTypeTrn.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTypeTrn")).Text;

                    txttransAP.Enabled = true;
                    //txtjmlhGL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljmlhGL")).Text;
                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtKeterangan.Enabled = false;
                    txtBankGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbankgiro")).Text;
                    txtnoGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnogiro")).Text;

                   
                    var bank = ddlKdBank.SelectedValue;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var, "constrmf");
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





                    ddltypejur.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypejurnal")).Text;
                    ddltypejur.Enabled = false;
                    Pnlfooter4.Visible = true;
                    //pnltypejurnal.Visible = true;

                    ddltypejur.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltypejurnal")).Text;
                    //   ddltypejur.Enabled = false;
                    //Pnlfooter1.Visible = true;
                    string datajurnal = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejurnal")).Text;
                    DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[5];
                    var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLJURNALLAIN", typeof(string));
                    var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var6[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var6[4] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTypeTrn.SelectedValue, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", var6, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";
                    ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlJurnal.DataTextField = "NamaJurnal";
                    ddlJurnal.DataValueField = "KodeJurnal";
                    ddlJurnal.DataBind();
                    ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));
                    ddlJurnal.SelectedValue = datajurnal;

                    if (ddltypejur.SelectedValue == "1")
                    {

                        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
                        ddlJurnal.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejurnal")).Text;
                        varT[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlJurnal.SelectedValue, typeof(string));
                        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIClJURNAL", typeof(string));
                        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varT[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varT, "constrmf");

                        string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
                        string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
                        string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
                        string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
                        string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
                        txtJurnal.Text = KetJurnal;
                        txtDebit1.Text = DebitGL;
                        txtDebit2.Text = DebitCC;
                        txtKredit1.Text = KreditGL;
                        txtKredit2.Text = KreditCC;

                        //GET KETERANGAN GL
                        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[5];
                        varGL[0] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 40, DebitGL, typeof(string));
                        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
                        varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varGL[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8000, SessBranchID, typeof(string));
                        varGL[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varGL, "constrmf");
                        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
                        txtDebit3.Text = DCC;

                        //GET KETERANGAN CC
                        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[5];
                        varCC[0] = gb.CreateSPParameter("@KodeGL", ParameterDirection.Input, SqlDbType.VarChar, 40, KreditGL, typeof(string));
                        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
                        varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varCC[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8000, SessBranchID, typeof(string));
                        varCC[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varCC, "constrmf");
                        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
                        txtKredit3.Text = KGL;
                    }

                    if (ddlTypeTrn.SelectedValue == "B")
                    {
                        txtDebit1.Text = ddlKdBank.SelectedValue;
                        txtDebit3.Text = ddlKdBank.SelectedItem.Text;

                    }
                    else
                    {
                        txtKredit1.Text = ddlKdBank.SelectedValue;
                        txtKredit3.Text = ddlKdBank.SelectedItem.Text;

                    }

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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL", Paramsc, "constrmf").Tables[0];

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
                    typetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTypeTrn")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIP", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashLain.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        if (typetrn=="B")
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
            case "Kwitansi":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    string tgltrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    string nama = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbluserentry")).Text;
                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    string nonasabah = HDNoNasabah.Value;
                    string buktipencairan = "No. " + ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    string typetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTypeTrn")).Text;
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSITLL", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, norek, typeof(string));
                    //Paramsc[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf");
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (typetrn == "B")
                        {
                            rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLKwitansiMasuk.rpt"));
                        }
                        else
                        {
                            rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLKwitansiKeluar.rpt"));
                        }
                     
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
        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            //decimal jumlahCL = decimal.Parse(txtjmlhCL.Text);
            decimal jumlahAP = decimal.Parse(txttransAP.Text);
            //decimal jumlahGL = decimal.Parse(txtjmlhGL.Text);


           




                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[15];

                Params[0] = gb.CreateSPParameter("@Typejournal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypejur.SelectedValue, typeof(string));
                Params[1] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[2] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[3] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[4] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[5] = gb.CreateSPParameter("@Kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJurnal.SelectedValue, typeof(string));
                Params[6] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[7] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[8] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[9] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[10] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                Params[11] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKdBank.SelectedValue, typeof(string));
                Params[12] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                Params[13] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[14] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTypeTrn.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Delete_Lain", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btndelete.Visible = false;
                    
                    nobuktidb = txtnobukti.Text;
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

               
            decimal jumlahAP = decimal.Parse(txttransAP.Text);

            string typetrn = ddlTypeTrn.SelectedValue;

            int temuan = 0,xtemuan=0;
            decimal jlhdebit = 0;
            decimal jlhkredit = 0;
            string kodebank;
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


                if (jumlahAP == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi tidak boleh 0!');", true);
                    btnSimpan.Enabled = true;
                }
                else if (typetrn == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Type Trn Masih Kosong');", true);
                    btnSimpan.Enabled = true;

                }
                else if (ddlKdBank.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank Masih Kosong');", true);
                    btnSimpan.Enabled = true;

                }
                //else if (ddlJurnal.SelectedValue == "C0000013" && jumlahAP>1000000)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kasbon Karyawan tidak boleh lebih dari 1 JT');", true);


                //}


                else if ((ddltypejur.SelectedValue == "1") && (ddlJurnal.SelectedValue == "-"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis Transaksi Masih Kosong');", true);
                    btnSimpan.Enabled = true;

                }
                else if (ddltypejur.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anda Belum Memilih Type Journal');", true);
                    btnSimpan.Enabled = true;

                }

                else
                {


                    if (ddltypejur.SelectedValue == "1")
                    {
                        dtRP.Columns.Clear();

                        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
                        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
                        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
                        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
                        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
                        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
                        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));

                        dtRP.Rows.Clear();


                        DataRow dr = dtRP.NewRow();
                        dr["nomor"] = 1;
                        dr["KodeGL"] = txtDebit1.Text;
                        dr["NamaGL"] = txtDebit3.Text;
                        dr["Description"] = txtKeterangan.Text;
                        dr["Costcenter"] = txtDebit2.Text;
                        dr["jlhdebit"] = jumlahAP;
                        dr["jlhkredit"] = 0;
                        dtRP.Rows.Add(dr);

                        dr = dtRP.NewRow();
                        dr["nomor"] = 2;
                        dr["KodeGL"] = txtKredit1.Text;
                        dr["NamaGL"] = txtKredit3.Text;
                        dr["Description"] = txtKeterangan.Text;
                        dr["Costcenter"] = txtKredit2.Text;
                        dr["jlhdebit"] = 0;
                        dr["jlhkredit"] = jumlahAP;
                        dtRP.Rows.Add(dr);

                        if (typetrn == "B")
                        {
                            if (txtDebit1.Text != ddlKdBank.SelectedValue)
                            {
                                temuan = 1;
                            }

                        }
                        else
                        {
                            if (txtKredit1.Text != ddlKdBank.SelectedValue)
                            {
                                temuan = 1;
                            }

                        }


                    }

                    if (temuan == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('CEK Kembali Kode Journal Dengan Kode Bank!');", true);
                        btnSimpan.Enabled = true;
                    }
                    else
                    {





                        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[20];

                        Params[0] = gb.CreateSPParameter("@Typejournal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypejur.SelectedValue, typeof(string));
                        Params[1] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                        Params[2] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                        Params[3] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                        Params[4] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(DateTime));
                        Params[5] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                        Params[6] = gb.CreateSPParameter("@Kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlJurnal.SelectedValue, typeof(string));
                        Params[7] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                        Params[8] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                        Params[9] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Params[10] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Params[11] = gb.CreateSPParameter("@GL_Transaction_Detail", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                        Params[12] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        Params[13] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKdBank.SelectedValue, typeof(string));
                        Params[14] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                        Params[15] = gb.CreateSPParameter("@bankgiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtBankGiro.Text, typeof(string));
                        Params[16] = gb.CreateSPParameter("@nogiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoGiro.Text, typeof(string));
                        Params[17] = gb.CreateSPParameter("@xnobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                        Params[18] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlTypeTrn.SelectedValue, typeof(string));
                        Params[19] = gb.CreateSPParameter("@kodecrc", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkursbank.SelectedValue, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Insert_Lain", Params, "constrmf");
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

  
    protected void ddltypejur_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddltypejur.SelectedValue == "3")
        {
            //Pnlfooter1.Visible = false;
            Pnlfooter4.Visible = true;
            btnSimpan.Visible = false;
            btndelete.Visible = true;
            //btnback4.Visible = false;
            btnclose.Visible = true;
            if (txtnobukti.Text == "")
            {
                btnSimpan.Visible = true;
                btndelete.Visible = false;
            }
            else
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
        }
        else
        {
            //Pnlfooter1.Visible = true;
            Pnlfooter4.Visible = false;
            if (txtnobukti.Text == "")
            {
                btnSimpan.Visible = true;
                btndelete.Visible = false;
            }
            else
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
           
            //btnback4.Visible = true;
            btnclose.Visible = false;
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
            txttgljthtmp.Text = txttgltrn.Text;
        }
        txttgljthtmp.Text = txttgltrn.Text;
    }

   
   
    protected void ddljenisTrn_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Jur = ddlJurnal.SelectedValue;
        //TEXTBOX AUTO CURRENCY
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@kodejurnal", ParameterDirection.Input, SqlDbType.VarChar, 30, Jur, typeof(string));
        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIClJURNAL", typeof(string));
        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varT[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varT, "constrmf");

        string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
        string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
        string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
        string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
        string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
        txtJurnal.Text = KetJurnal;
        txtDebit1.Text = DebitGL;
        txtDebit2.Text = DebitCC;
        txtKredit1.Text = KreditGL;
        txtKredit2.Text = KreditCC;

        //GET KETERANGAN GL
        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[5];
        varGL[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, DebitGL, typeof(string));
        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
        varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varGL[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varGL[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varGL, "constrmf");
        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
        txtDebit3.Text = DCC;

        //GET KETERANGAN GL
        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[5];
        varCC[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, KreditGL, typeof(string));
        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
        varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varCC[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varCC[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL", varCC, "constrmf");
        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
        txtKredit3.Text = KGL;
        txtKeterangan.Text = txtJurnal.Text;
        if (ddlTypeTrn.SelectedValue == "B")
        {
            txtDebit1.Text = ddlKdBank.SelectedValue;
            txtDebit3.Text = ddlKdBank.SelectedItem.Text;
            
        }
        else
        {
            txtKredit1.Text = ddlKdBank.SelectedValue;
            txtKredit3.Text = ddlKdBank.SelectedItem.Text;

        }


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




   
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {

        decimal bayar;
        bayar = decimal.Parse(txttransAP.Text);
        //txtjmlhCL.Text = string.Format("{0:N2}", bayar);
        //txtjmlhGL.Text = string.Format("{0:N2}", bayar);

    }



}
