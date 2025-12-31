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
public partial class SN_Process_Sukarela_Cetakbillyet : System.Web.UI.Page
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
    string form = "S2";
    string kodesumber = "SS";
    string KodeKonter = "SS02";
    public string nobuktidb;

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter4.Visible = true;
        btnSimpan.Visible = false;
        ddlproduk.Enabled = false;
        ddlKoperasi.Enabled = false;
        ddlcabang.Enabled = false;
        ddljangka.Enabled = false;
        ddlbunga.Enabled = false;
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
               
                ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";
                ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlprovinsiktp.DataTextField = "nama_provinsi";
                ddlprovinsiktp.DataValueField = "kode_provinsi";
                ddlprovinsiktp.DataBind();


                //KODE LANGGANAN
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKOPERASI", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "nama_koperasi";
                ddlKoperasi.DataValueField = "kode_koperasi";
                ddlKoperasi.DataBind();
                ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlKoperasi.SelectedValue = "I";

                var Koperasi = ddlKoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "nama_cabang";
                ddlcabang.DataValueField = "kode_cabang";
                ddlcabang.DataBind();
                ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                BindGridEntity();

                //KODE Produk
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPRODUCT", typeof(string));
                var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var4, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
                ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlproduk.DataTextField = "nama_produk";
                ddlproduk.DataValueField = "kode_produk";
                ddlproduk.DataBind();
                ddlproduk.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //KODE Produk
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[3];
                var5[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var5[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPAJAK", typeof(string));
                var5[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var5, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Pajak asc";
                ddlpajak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlpajak.DataTextField = "Pajak";
                ddlpajak.DataValueField = "Pajak";
                ddlpajak.DataBind();
                ddlpajak.Items.Insert(0, new ListItem("Pilih Data", "-"));

                

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
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
    protected void ddlproduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Produk = ddlproduk.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
        ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangka.DataTextField = "Jangka";
        ddljangka.DataValueField = "Jangka";
        ddljangka.DataBind();
        ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }
    protected void ddljangka_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Produk = ddlproduk.SelectedValue;
        int jangka =int.Parse(ddljangka.SelectedValue);
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
        ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
        ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbunga.DataTextField = "BungaTahunan";
        ddlbunga.DataValueField = "BungaTahunan";
        ddlbunga.DataBind();
        ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }
    
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        BindGridEntity();

    }
    protected void ddlKoperasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Koperasi = ddlKoperasi.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
     
        ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "nama_cabang";
        ddlcabang.DataValueField = "kode_cabang";
        ddlcabang.DataBind();
        ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));
        BindGridEntity();

    }

    protected void ddlpenarikanbunga_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpenarikanbunga.SelectedValue == "1")
        {
          
            txtbankrekening.Enabled = true;
             txtnorekening.Enabled = true;
             txtatasnama.Enabled = true;
        }
        else
        {
       
            txtbankrekening.Text = "-";
            txtbankrekening.Enabled = false;
            txtnorekening.Text = "-";
            txtnorekening.Enabled = false;
            txtatasnama.Text = "-";
            txtatasnama.Enabled = false;
        }

    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBILLYED", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKoperasi.SelectedValue, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "Idpermohonan asc";


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
                    //GRTP.Enabled = false;
                    Pnlfooter4.Visible = true;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");

                    //HDNoNasabah.Value = no_nasabah;
                    txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();

                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();

                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();

                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBayar")).Text;
                   

        

                    ddlKoperasi.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();
                    ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["produk"].ToString();

                    ddlpajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString();


                    txtbungaperhari.Text = string.Format("{0:N6}",float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                    txtnilaisaldo.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaisaldo"].ToString()));

                    txtnilaibungapertahun.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungapertahun"].ToString()));
                    txtnilaibungaperhari.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungaperhari"].ToString()));


                    ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
                    ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();

                    txtbankrekening.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBank"].ToString();
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["atasnama"].ToString();


                    txtnamawaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris1"].ToString();
                    txtktpwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris1"].ToString();
                    txtalamatwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris1"].ToString();

                    txtnamawaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris2"].ToString();
                    txtktpwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris2"].ToString();
                    txtalamatwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris2"].ToString();

                    var cabang= ge.ListDataset.Tables[0].Rows[0]["kode_cabang"].ToString();
                    var jangkabulan= ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

                    var norekening = ge.ListDataset.Tables[0].Rows[0]["norekening"].ToString();

                    float bunga = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString());


                    var Koperasi = ddlKoperasi.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                    varx[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varx, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                    ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));





                    ddlcabang.SelectedValue = cabang;




                    var Produk = ddlproduk.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varc = new DefaultClass.Global.SqlHelper[4];
                    varc[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                    varc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
                    varc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varc[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varc, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
                    ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljangka.DataTextField = "Jangka";
                    ddljangka.DataValueField = "Jangka";
                    ddljangka.DataBind();
                    ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddljangka.SelectedValue = jangkabulan;


                    int jangka = int.Parse(ddljangka.SelectedValue);
                    DefaultClass.Global.SqlHelper[] varj = new DefaultClass.Global.SqlHelper[5];
                    varj[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                    varj[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
                    varj[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varj[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    varj[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varj, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
                    ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlbunga.DataTextField = "BungaTahunan";
                    ddlbunga.DataValueField = "BungaTahunan";
                    ddlbunga.DataBind();
                    ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddlbunga.SelectedValue = bunga.ToString();

                    if (statusclosing == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah dibayar!');", true);
                        btnSimpan.Visible = false;
                    }
                    else
                    {
                        btnSimpan.Visible = true;
                    }

                   
                
                    btndelete.Visible = false;


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
                    Pnlfooter4.Visible = true;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");

                    //HDNoNasabah.Value = no_nasabah;
                    txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();

                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBayar")).Text;
                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();

                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;



                    ddlKoperasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();
                    ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["produk"].ToString();

                    ddlpajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString();


                    txtbungaperhari.Text = string.Format("{0:N6}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                    txtnilaisaldo.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaisaldo"].ToString()));

                    txtnilaibungapertahun.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungapertahun"].ToString()));
                    txtnilaibungaperhari.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungaperhari"].ToString()));


                    ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
                    ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();

                    txtbankrekening.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBank"].ToString();
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["atasnama"].ToString();


                    txtnamawaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris1"].ToString();
                    txtktpwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris1"].ToString();
                    txtalamatwaris1.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris1"].ToString();

                    txtnamawaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAhliWaris2"].ToString();
                    txtktpwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["NoKTPAhliWaris2"].ToString();
                    txtalamatwaris2.Text = ge.ListDataset.Tables[0].Rows[0]["AlamatAhliWaris2"].ToString();

                    var cabang = ge.ListDataset.Tables[0].Rows[0]["kode_cabang"].ToString();
                    var jangkabulan = ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

                    var norekening = ge.ListDataset.Tables[0].Rows[0]["norekening"].ToString();

                    float bunga = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString());


                    var Koperasi = ddlKoperasi.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                    varx[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varx, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                    ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));





                    ddlcabang.SelectedValue = cabang;




                    var Produk = ddlproduk.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varc = new DefaultClass.Global.SqlHelper[4];
                    varc[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                    varc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
                    varc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varc[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varc, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
                    ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljangka.DataTextField = "Jangka";
                    ddljangka.DataValueField = "Jangka";
                    ddljangka.DataBind();
                    ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddljangka.SelectedValue = jangkabulan;


                    int jangka = int.Parse(ddljangka.SelectedValue);
                    DefaultClass.Global.SqlHelper[] varj = new DefaultClass.Global.SqlHelper[5];
                    varj[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                    varj[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
                    varj[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varj[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    varj[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varj, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
                    ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlbunga.DataTextField = "BungaTahunan";
                    ddlbunga.DataValueField = "BungaTahunan";
                    ddlbunga.DataBind();
                    ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddlbunga.SelectedValue = bunga.ToString();

                   
                        btndelete.Visible = true;
                    




                    btnSimpan.Visible = false;
                   






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
                    string pajak;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    pajak = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPajak")).Text;
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTFORMULIR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SS_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (pajak == "0")
                        {
                            rprt.Load(Server.MapPath("~/SS/Process/ReportFormulir.rpt"));
                        }
                        else
                        {
                            rprt.Load(Server.MapPath("~/SS/Process/ReportFormulirberjangkapajak.rpt"));
                        }
                    
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
            case "Bilyed":

                try
                {
                    string nobukti;
                    string pajak;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    pajak = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPajak")).Text;
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTSLIP", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SS_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (pajak == "0")
                        {
                            rprt.Load(Server.MapPath("~/SS/Process/Reportbillyednonpajak.rpt"));
                        }
                        else
                        {
                            rprt.Load(Server.MapPath("~/SS/Process/Reportbillyed.rpt"));
                        }

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

        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
           
            string nobukti;
            string pajak;
            nobukti = txtnobukti.Text;
            //pajak = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPajak")).Text;
            //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

            
            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
            Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@nobillyed", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobillyeed.Text, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATESLIP", typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsc[5] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.SS_Permohonan", Paramsc, "constrmf").Tables[0];



            DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[6];
            Paramsx[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            Paramsx[1] = gb.CreateSPParameter("@nobillyed", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobillyeed.Text, typeof(string));
            Paramsx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTSLIP", typeof(string));
            Paramsx[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Paramsx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsx[5] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.SS_Permohonan", Paramsx, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {

                txtnobillyeed.Enabled = false;
                btndelete.Enabled = false;
                rprt.Load(Server.MapPath("~/SS/Process/Reportbillyed.rpt"));
               
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
      
    }

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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

    

         protected void txtnilaisaldo_TextChanged(object sender, EventArgs e)
    {
        decimal jlhbayartahun = 0;
        decimal jlhbayarhari = 0;
        if (decimal.Parse(txtnilaisaldo.Text) >= 0)
        {


            jlhbayartahun = (decimal.Parse(ddlbunga.SelectedValue) / 100) * decimal.Parse(txtnilaisaldo.Text);
            jlhbayarhari = (decimal.Parse(txtbungaperhari.Text) / 100) * decimal.Parse(txtnilaisaldo.Text);
            txtnilaibungapertahun.Text = string.Format("{0:N2}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N2}", jlhbayarhari);
            //txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);

        }
        else
        {
            txtnilaibungapertahun.Text = string.Format("{0:N2}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N2}", jlhbayarhari);
        }


    }

    protected void ddlbunga_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal jlhbayar = 0;
        if (decimal.Parse(ddlbunga.SelectedValue) >= 0)
        {


            jlhbayar = ((decimal.Parse(ddlbunga.SelectedValue) / 100) / 365) * 100;
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
        }
        else
        {
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
        }


    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {
                        //invoce yg dibayarkan
            //string txtpath = txtpathktp.Text;
            string State = "";

 
           

           if (txtnobukti.Text == "")
                {
                    State = "INSERT";
                }
           else
                {
                    State = "UPDATE";
                }
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[41];

	
		        Params[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[1] = gb.CreateSPParameter("@kodekonter", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string));
                Params[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[3] = gb.CreateSPParameter("@tglpermohonan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[4] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlKoperasi.SelectedValue, typeof(string));
                Params[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                Params[6] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnonasabah.Text, typeof(string));
                Params[7] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
                Params[8] = gb.CreateSPParameter("@alamat", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamat.Text, typeof(string));
                Params[9] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkabupaten.Text, typeof(string));
                Params[10] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrtktp.Text, typeof(string));
                Params[11] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrwktp.Text, typeof(string));
                Params[12] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkelurahanktp.Text, typeof(string));
                Params[13] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkecamatanktp.Text, typeof(string));
                Params[14] = gb.CreateSPParameter("@provinsi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprovinsiktp.SelectedValue, typeof(string));

            Params[15] = gb.CreateSPParameter("@kodepos", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodeposktp.Text, typeof(string));
            Params[16] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitas.Text, typeof(string));
            Params[17] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNPWP.Text, typeof(string));
            Params[18] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
            Params[19] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangka.SelectedValue), typeof(int));
            Params[20] = gb.CreateSPParameter("@bungapertahun", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(ddlbunga.SelectedValue), typeof(float));
            Params[21] = gb.CreateSPParameter("@bungaperhari", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtbungaperhari.Text), typeof(float));
            Params[22] = gb.CreateSPParameter("@Pajak", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(ddlpajak.SelectedValue), typeof(float));
            Params[23] = gb.CreateSPParameter("@nilaibungapertahun", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtnilaibungapertahun.Text), typeof(decimal));
            Params[24] = gb.CreateSPParameter("@nilaibungaperhari", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtnilaibungaperhari.Text), typeof(decimal));
            Params[25] = gb.CreateSPParameter("@InstrukturPromissory", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlInstruktur.SelectedValue), typeof(int));
            Params[26] = gb.CreateSPParameter("@PenarikanBunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlpenarikanbunga.SelectedValue), typeof(int));
            Params[27] = gb.CreateSPParameter("@BankRekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtbankrekening.Text, typeof(string));
            Params[28] = gb.CreateSPParameter("@NoRekeningBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
            Params[29] = gb.CreateSPParameter("@atasnama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtatasnama.Text, typeof(string));
            Params[30] = gb.CreateSPParameter("@NamaAhliWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamawaris1.Text, typeof(string));
            Params[31] = gb.CreateSPParameter("@NoKTPAhliWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtktpwaris1.Text, typeof(string));
            Params[32] = gb.CreateSPParameter("@AlamatAhliWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatwaris1.Text, typeof(string));
            Params[33] = gb.CreateSPParameter("@NamaAhliWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamawaris2.Text, typeof(string));
            Params[34] = gb.CreateSPParameter("@NoKTPAhliWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtktpwaris2.Text, typeof(string));
            Params[35] = gb.CreateSPParameter("@AlamatAhliWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatwaris2.Text, typeof(string));
            Params[36] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[37] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[38] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
            Params[39] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
            Params[40] = gb.CreateSPParameter("@nilaisaldo", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtnilaisaldo.Text), typeof(decimal));
            ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", Params, "constrmf");


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
                       
                    //    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Data Permohonan','top-right','success','1');", true);
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
    protected void UploadFileKTP(object sender, EventArgs e)
    {

        string namafileKTP;
        string fotopathktp;
        string folderPath = Server.MapPath("~/Files/MENINGGAL/");
        DateTime now = DateTime.Now;

        namafileKTP = HDNoRekening.Value + "_" + now.ToString("ddMMyyyy") + "_" + "DEAD";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if(Path.GetFileNameWithoutExtension(fileuploadktp.FileName) != namafileKTP)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileKTP + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        //fileuploadktp.SaveAs(folderPath + namafileKTP + Path.GetFileName(fileuploadktp.FileName));

        ////Display the Picture in Image control.
        //imagektp.ImageUrl = "~/Files/Kresun/KTP/" + Path.GetFileName(fileuploadktp.FileName);

        //fotopathktp = folderPath + Path.GetFileName(fileuploadktp.FileName);
        //txtpathktp.Text = "~/Files/MENINGGAL/" + namafileKTP + Path.GetFileName(fileuploadktp.FileName);
        //txtlink.Text = "View";
        //txtlink.NavigateUrl= txtpathktp.Text;
        //}


    }


    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
            //txtkodelgn.Text = kodelgn;
            //txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            //txttglsk.Text = ge.ListDataset.Tables[0].Rows[0]["tglsk"].ToString();

            //Txtnorekening.Text = HDNoRekening.Value;
            txtnonasabah.Text = kodelgn;
            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

          
            txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();

            txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

            txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();

            txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();

            txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
            txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
            txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
            txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
            ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
            txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
       

            btnSimpan.Visible = true;


            ddlproduk.Enabled = true;
            ddlKoperasi.Enabled = true;
            ddlcabang.Enabled = true;
            ddljangka.Enabled = true;
            ddlbunga.Enabled = true;

           
            //RequiredFieldValidator3.Enabled = true;
          
            RequiredFieldValidator6.Enabled = true;
            RequiredFieldValidator7.Enabled = true;
            RequiredFieldValidator8.Enabled = true;
            ////txtNmSup.Enabled = true;
            //txttglsk.Enabled = true;

            //txttgltrn.Enabled = true;
            //txtKeterangan.Enabled = true;
            //fileuploadktp.Enabled = true;
            //btnUploadktp.Enabled = true;

        }

        else
        {
            txtnonasabah.Text = "";
            txtnamanasabah.Text = "";


            txtidentitas.Text = "";

            txtNPWP.Text = "";

            txtalamat.Text = "";

            txtkabupaten.Text = "";

            txtrtktp.Text = "";
            txtrwktp.Text = "";
            txtkelurahanktp.Text = "";
            txtkecamatanktp.Text = "";
            txtkodeposktp.Text = "";
            ddlproduk.Enabled = false;
            ddlKoperasi.Enabled = false;
            ddlcabang.Enabled = false;
            ddljangka.Enabled = false;
            ddlbunga.Enabled = false;
            btnSimpan.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
           
            //RequiredFieldValidator3.Enabled = false;
           
            RequiredFieldValidator6.Enabled = false;
            RequiredFieldValidator7.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
        }


     


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



    }
  
 
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
  

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SS_getCustomer_Permohonan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }








}
