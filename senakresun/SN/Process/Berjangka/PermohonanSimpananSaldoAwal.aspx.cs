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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Activities.Statements;
using DocumentFormat.OpenXml.Spreadsheet;

public partial class Kresun_Process_PermohonanSimpananSaldoAwal : System.Web.UI.Page
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
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
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
    string kodesumber = "SN";
    string KodeKonter = "SN02";
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
        ddlInstruktur.SelectedValue = "1";
        txttgltrn.ReadOnly = false;
    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void UploadFile(object sender, EventArgs e)
    {




        string namafile;
        string fotopath;
        string folderPath = Server.MapPath("~/Files/Tarikan/");
        DateTime now = DateTime.Now;

        namafile = now.ToString("ddMMyyyyhhmmss") + "_" + "BuktiTarikan";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }
        fileupload.SaveAs(folderPath + namafile + ".jpg");


        fotopath = folderPath + namafile + ".jpg";
        txtpath.Text = fotopath;


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
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "nama_koperasi";
                ddlKoperasi.DataValueField = "kode_koperasi";
                ddlKoperasi.DataBind();


                ddlKoperasi.SelectedValue = SessKoperasiID;
                ddlKoperasi.Enabled = false;
                //ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));


                var Koperasi = ddlKoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
              
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                //ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlcabang.SelectedValue = SessBranchID;
                ddlcabang.Enabled = false;

                //KODE Produk
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPRODUCT", typeof(string));
                var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var4, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
                ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlproduk.DataTextField = "nama_produk";
                ddlproduk.DataValueField = "kode_produk";
                ddlproduk.DataBind();
                ddlproduk.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlproduk.Enabled = true;

                //KODE Produk
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[3];
                var5[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var5[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPAJAK", typeof(string));
                var5[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var5, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Pajak desc";
                ddlpajak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlpajak.DataTextField = "Pajak";
                ddlpajak.DataValueField = "Pajak";
                ddlpajak.DataBind();
                ddlpajak.Enabled = false;
                //ddlpajak.SelectedValue = "0";
                ////ddlpajak.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                Txttglsetoran.Text = xtanggal.ToString("dd-MM-yyyy");
                InsertCombo();
            }
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            fileuploadktpwaris1.Dispose();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void ddlsumberdana_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
         
            if (ddlsumberdana.SelectedValue == "5")
            {
                txtsumberdana.Text = "";
                txtsumberdana.Enabled = true;
            }
            else
            {
                txtsumberdana.Text = "-";
                txtsumberdana.Enabled = false;

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void ddltujuantransaksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddltujuanTransaksi.SelectedValue == "4")
            {
                txttujuantransaksi.Text = "";
                txttujuantransaksi.Enabled = true;
            }
            else
            {
                txttujuantransaksi.Text = "-";
                txttujuantransaksi.Enabled = false;

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlproduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Produk = ddlproduk.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
            ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljangka.DataTextField = "Jangka";
            ddljangka.DataValueField = "Jangka";
            ddljangka.DataBind();
            //ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddljangka_SelectedIndexChanged(sender, e);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddljangka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Produk = ddlproduk.SelectedValue;
            int jangka = int.Parse(ddljangka.SelectedValue);
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
            ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
            ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlbunga.DataTextField = "BungaTahunan";
            ddlbunga.DataValueField = "BungaTahunan";
            ddlbunga.DataBind();
            ddlbunga_SelectedIndexChanged(sender, e);
            //ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

   
    protected void ddlrollover_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrollover.SelectedValue == "1")
        {
            txtbankrekening.Text = "-";
            txtbankrekening.Enabled = false;
            txtnorekening.Text = "-";
            txtnorekening.Enabled = false;
            txtatasnama.Text = "-";           
        }
        else
        {
            txtatasnama.Enabled = false;
            txtbankrekening.Enabled = true;
            txtnorekening.Enabled = true;
            txtatasnama.Enabled = true;
        }

    }
    protected void ddlsetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsetor.SelectedValue == "3")
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SETORAN", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");

            txtbankcabang.Enabled = true;
            txtatasnamacabang.Enabled = true;
            txtrekeningbankcabang.Enabled = true;
            txtbankcabang.Text = ge.ListDataset.Tables[0].Rows[0]["bankrekeningcabang"].ToString();
            txtatasnamacabang.Text = ge.ListDataset.Tables[0].Rows[0]["atasnamacabang"].ToString();
            txtrekeningbankcabang.Text = ge.ListDataset.Tables[0].Rows[0]["norekeningcabang"].ToString();



        }
        else
        {

            txtbankcabang.Text = "-";
            txtbankcabang.Enabled = false;
            txtatasnamacabang.Text = "-";
            txtatasnamacabang.Enabled = false;
            txtrekeningbankcabang.Text = "-";
            txtrekeningbankcabang.Enabled = false;
        }

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
       
            if (ddlpenarikanbunga.SelectedValue == "3")
            {
                ddlrollover.Enabled = true;
            }    
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
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@typepermohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, "A", typeof(string));
        
        ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "Idpermohonan desc";


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
                    txttgltrn.ReadOnly = true;
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
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");

                    //HDNoNasabah.Value = no_nasabah;
                    txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    txtpath.Text = ge.ListDataset.Tables[0].Rows[0]["PathFile"].ToString();
                    txtfilename.Text = ge.ListDataset.Tables[0].Rows[0]["filename"].ToString();

                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();

                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();

                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    ddltypebunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["typebunga"].ToString();

                    ddljenkel.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    txttempatlahir.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                    txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse((ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())));

                    ddlAgama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                    ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                    ddlpendidikan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    TxtEmail.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();

                    ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                    ddlSektorUsaha.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                    txtjenisusaha.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();
                    ddllamabekerja.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["lamabekerja"].ToString();
                    ddlpenghasilan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["penghasilan"].ToString();

                    ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["penghasilan"].ToString();
                    txtsumberdana.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();


                    ddltujuanTransaksi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tujuantransaksi"].ToString();
                    txttujuantransaksi.Text = ge.ListDataset.Tables[0].Rows[0]["kettujuantransaksi"].ToString();


                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBayar")).Text;
                    

                    ddlsetor.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenissetoran"].ToString();
                    Txttglsetoran.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglsetoran")).Text;

                    txtatasnamacabang.Text = ge.ListDataset.Tables[0].Rows[0]["AtasNamacabang"].ToString();
                    txtbankcabang.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningcabang"].ToString();
                    txtrekeningbankcabang.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBankcabang"].ToString();

                    ddltanggal.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pilihandata"].ToString();
                    if (ddlsetor.SelectedValue == "3")
                    {

                      

                        txtbankcabang.Enabled = true;
                        txtatasnamacabang.Enabled = true;
                        txtrekeningbankcabang.Enabled = true;
                   



                    }
                    else
                    {

                        txtbankcabang.Text = "-";
                        txtbankcabang.Enabled = false;
                        txtatasnamacabang.Text = "-";
                        txtatasnamacabang.Enabled = false;
                        txtrekeningbankcabang.Text = "-";
                        txtrekeningbankcabang.Enabled = false;
                    }


                    ddlKoperasi.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();
                    ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["produk"].ToString();

                    ddlpajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString();


                    txtbungaperhari.Text = string.Format("{0:N6}",float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                    txtnilaisaldo.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaisaldo"].ToString()));
                    txtsimpananwajib.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["simpananwajib"].ToString()));

                    txtnilaibungapertahun.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungapertahun"].ToString()));
                    txtnilaibungaperhari.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungaperhari"].ToString()));


                    ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
                    ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();
                    ddlrollover.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pencairanbunga"].ToString();
                    txtbankrekening.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBank"].ToString();
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["AtasNama"].ToString();


                    //path
                    txtpathktpwaris1.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris1")).Value;
                    txtpathktpwaris2.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris2")).Value;

                    if (ddlpenarikanbunga.SelectedValue == "1")
        {
          
            txtbankrekening.Enabled = true;
             txtnorekening.Enabled = true;
             txtatasnama.Enabled = true;
        }
        else 
        {
       
            if (ddlpenarikanbunga.SelectedValue == "3")
            {
                ddlrollover.Enabled = true;
                            if (ddlrollover.SelectedValue == "1")
                            {
                                txtbankrekening.Text = "-";
                                txtbankrekening.Enabled = false;
                                txtnorekening.Text = "-";
                                txtnorekening.Enabled = false;
                                txtatasnama.Text = "-";
                            }
                            else
                            {
                                txtatasnama.Enabled = false;
                                txtbankrekening.Enabled = true;
                                txtnorekening.Enabled = true;
                                txtatasnama.Enabled = true;
                            }
                        }    
            txtbankrekening.Text = "-";
            txtbankrekening.Enabled = false;
            txtnorekening.Text = "-";
            txtnorekening.Enabled = false;
            txtatasnama.Text = "-";
            txtatasnama.Enabled = false;
        }




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

                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varx, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varc, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varj, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
                    ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlbunga.DataTextField = "BungaTahunan";
                    ddlbunga.DataValueField = "BungaTahunan";
                    ddlbunga.DataBind();
                    ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddlbunga.SelectedValue = bunga.ToString();

                    btndelete.Visible = false;




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
                    Pnlfooter4.Visible = true;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");

                    //HDNoNasabah.Value = no_nasabah;
                    txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();

                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBayar")).Text;
                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                    ddltypebunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["typebunga"].ToString();
                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;

                    ddlrollover.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pencairanbunga"].ToString();


                    ddltanggal.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["pilihandata"].ToString(); 

                    //path
                    txtpathktpwaris1.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris1")).Value;
                    txtpathktpwaris2.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris2")).Value;


                    ddlKoperasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();
                    ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["produk"].ToString();

                    ddlpajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString();


                    txtbungaperhari.Text = string.Format("{0:N6}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                    txtnilaisaldo.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaisaldo"].ToString()));

                    txtnilaibungapertahun.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungapertahun"].ToString()));
                    txtnilaibungaperhari.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungaperhari"].ToString()));


                    ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
                    ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();
                    if (ddlpenarikanbunga.SelectedValue == "1")
                    {

                        txtbankrekening.Enabled = true;
                        txtnorekening.Enabled = true;
                        txtatasnama.Enabled = true;
                    }
                    else
                    {

                        if (ddlpenarikanbunga.SelectedValue == "3")
                        {
                            ddlrollover.Enabled = true;
                            if (ddlrollover.SelectedValue == "1")
                            {
                                txtbankrekening.Text = "-";
                                txtbankrekening.Enabled = false;
                                txtnorekening.Text = "-";
                                txtnorekening.Enabled = false;
                                txtatasnama.Text = "-";
                            }
                            else
                            {
                                txtatasnama.Enabled = false;
                                txtbankrekening.Enabled = true;
                                txtnorekening.Enabled = true;
                                txtatasnama.Enabled = true;
                            }
                        }
                        txtbankrekening.Text = "-";
                        txtbankrekening.Enabled = false;
                        txtnorekening.Text = "-";
                        txtnorekening.Enabled = false;
                        txtatasnama.Text = "-";
                        txtatasnama.Enabled = false;
                    }

                    txtbankrekening.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBank"].ToString();
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["AtasNama"].ToString();


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

                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varx, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varc, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", varj, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
                    ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlbunga.DataTextField = "BungaTahunan";
                    ddlbunga.DataValueField = "BungaTahunan";
                    ddlbunga.DataBind();
                    ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddlbunga.SelectedValue = bunga.ToString();
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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SN_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (pajak == "0")
                        {
                            rprt.Load(Server.MapPath("~/SN/Process/Berjangka/ReportFormulir.rpt"));
                        }
                        else
                        {
                            rprt.Load(Server.MapPath("~/SN/Process/Berjangka/ReportFormulirberjangkapajak.rpt"));
                        }
                    
                        rprt.SetDataSource(ge.ListDataTable);
                        
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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SN_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (pajak == "0")
                        {
                            rprt.Load(Server.MapPath("~/SN/Process/Berjangka/Reportbillyednonpajaktesnew.rpt"));
                        }
                        else
                        {
                            rprt.Load(Server.MapPath("~/SN/Process/Berjangka/Reportbillyed.rpt"));
                        }

                        rprt.SetDataSource(ge.ListDataTable);
                       
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
            case "STATEMENT":

                try
                {
                    string nobukti;
                    string penarikanbunga;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    penarikanbunga = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPenarikanBunga")).Text;
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTSTATEMENTBERJANGKA", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SN_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (penarikanbunga == "3")
                        {
                            rprt.Load(Server.MapPath("~/SN/Process/Berjangka/PNROOLOVER.rpt"));
                        }
                        else
                        {
                            rprt.Load(Server.MapPath("~/SN/Process/Berjangka/PNTRANSFER.rpt"));
                        }

                        rprt.SetDataSource(ge.ListDataTable);
                       
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

            //invoce yg dibayarkan
            //string txtpath = txtpathktp.Text;
            string State = "";

            if (txtnobukti.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('File Upload Masih Kosong!');", true);
            }
            else
            {
               
                State = "DELETE";


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
                Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
                Params[4] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", Params, "constrmf");
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

                    //if (txtpathktpwaris1.Text != "")
                    //{
                    //    string path1 = Server.MapPath(txtpathktpwaris1.Text);
                    //    FileInfo file1 = new FileInfo(path1);
                    //    if (file1.Exists)
                    //    {
                    //        file1.Delete();
                    //    }
                    //}
                    //if (txtpathktpwaris2.Text != "")
                    //{
                    //    string path2 = Server.MapPath(txtpathktpwaris2.Text);
                    //    FileInfo file2 = new FileInfo(path2);
                    //    if (file2.Exists)
                    //    {
                    //        file2.Delete();
                    //    }
                    //}
                        //    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Delete Permohonan Done','top-right','success','1');", true);
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
            lbltotalPokok.Text = string.Format("{0:N0}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N0}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N0}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N0}", denda);



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
            txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
            //txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);

        }
        else
        {
            txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
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
            int filex = 0;
            string filename = "";
            string fotopath;
            decimal nilai = 0;
            nilai = decimal.Parse(txtnilaisaldo.Text);

           if (txtnobukti.Text == "")
                {
                    State = "INSERT";
    
            }
           else
                {
                    State = "UPDATE";
                }



        
            if (ddlproduk.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Produk Masih Kosong!');", true);
            }
            else if (ddljangka.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jangka Masih Kosong!');", true);
            }
            else if (ddlbunga.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bunga Masih Kosong!');", true);
            }
            else if (nilai <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nilai Saldo Masih Kosong!');", true);
            }

           else
            {
              

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[72];
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
                    Params[29] = gb.CreateSPParameter("@AtasNama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtatasnama.Text, typeof(string));
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
                    Params[41] = gb.CreateSPParameter("@rollover", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlrollover.SelectedValue), typeof(int));
                    Params[42] = gb.CreateSPParameter("@pathktpWaris1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpathktpwaris1.Text, typeof(string));
                    Params[43] = gb.CreateSPParameter("@pathktpWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpathktpwaris2.Text, typeof(string));
                    Params[44] = gb.CreateSPParameter("@BankRekeningcabang", ParameterDirection.Input, SqlDbType.VarChar, 100, txtbankcabang.Text, typeof(string));
                    Params[45] = gb.CreateSPParameter("@NoRekeningBankcabang", ParameterDirection.Input, SqlDbType.VarChar, 100, txtrekeningbankcabang.Text, typeof(string));
                    Params[46] = gb.CreateSPParameter("@AtasNamacabang", ParameterDirection.Input, SqlDbType.VarChar, 100, txtatasnamacabang.Text, typeof(string));
                    Params[47] = gb.CreateSPParameter("@jenissetoran", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlsetor.SelectedValue), typeof(int));
                    Params[48] = gb.CreateSPParameter("@tglsetoran", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[49] = gb.CreateSPParameter("@typebunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddltypebunga.SelectedValue), typeof(int));
                    Params[50] = gb.CreateSPParameter("@PathFile", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpath.Text, typeof(string));
                    Params[51] = gb.CreateSPParameter("@filename", ParameterDirection.Input, SqlDbType.VarChar, 80, filename, typeof(string));
                    Params[52] = gb.CreateSPParameter("@pilihantgl", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddltanggal.SelectedValue), typeof(string));
                    Params[53] = gb.CreateSPParameter("@JenisKelamin", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenkel.SelectedValue, typeof(string));
                    Params[54] = gb.CreateSPParameter("@TempatLahir", ParameterDirection.Input, SqlDbType.VarChar, 30, txttempatlahir.Text, typeof(string));
                    Params[55] = gb.CreateSPParameter("@tanggal_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(DateTime));
                    Params[56] = gb.CreateSPParameter("@agama", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlAgama.SelectedValue, typeof(string));
                    Params[57] = gb.CreateSPParameter("@status_pernikahan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikah.SelectedValue, typeof(string));
                    Params[58] = gb.CreateSPParameter("@pendidikan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpendidikan.SelectedValue, typeof(string));
                    Params[59] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 20, txthp.Text, typeof(string));
                    Params[60] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 40, TxtEmail.Text, typeof(string));
                    Params[61] = gb.CreateSPParameter("@simpananwajib", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtsimpananwajib.Text), typeof(decimal));
                    Params[62] = gb.CreateSPParameter("@pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlPekerjaan.SelectedValue, typeof(string));
                    Params[63] = gb.CreateSPParameter("@sektorusaha", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlSektorUsaha.SelectedValue, typeof(string));
                    Params[64] = gb.CreateSPParameter("@jenisusaha", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenisusaha.Text, typeof(string));
                    Params[65] = gb.CreateSPParameter("@lamabekerja", ParameterDirection.Input, SqlDbType.VarChar, 2, ddllamabekerja.SelectedValue, typeof(string));
                    Params[66] = gb.CreateSPParameter("@penghasilan", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlpenghasilan.SelectedValue, typeof(string));
                    Params[67] = gb.CreateSPParameter("@sumberdana", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlsumberdana.SelectedValue, typeof(string));
                    Params[68] = gb.CreateSPParameter("@tujuantransaksi", ParameterDirection.Input, SqlDbType.VarChar, 4, ddltujuanTransaksi.SelectedValue, typeof(string));
                    Params[69] = gb.CreateSPParameter("@ketsumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, txtsumberdana.Text, typeof(string));
                    Params[70] = gb.CreateSPParameter("@kettujuantransaksi", ParameterDirection.Input, SqlDbType.VarChar, 50, txttujuantransaksi.Text, typeof(string));
                    Params[71] = gb.CreateSPParameter("@typepermohonan", ParameterDirection.Input, SqlDbType.VarChar, 50, "A", typeof(string));
                
                    ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", Params, "constrmf");

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
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        //varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        //varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        //varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        //string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        //if (xstatus == "1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
        //    txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
       
        //}

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
    public void InsertCombo()
    {
        // --- Combo Jenis Kelamin
        ge.ListDataset = gb.GetDataFunction("dbo.GetJenisKelamin()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddljenkel.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljenkel.DataTextField = "Keterangan";
        ddljenkel.DataValueField = "id";
        ddljenkel.DataBind();
        //ddljenkel.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Status Pernikahan
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatusnikah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatusnikah.DataTextField = "Keterangan";
        ddlstatusnikah.DataValueField = "id";
        ddlstatusnikah.DataBind();
        // ddlstatusnikah.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Agama
        ge.ListDataset = gb.GetDataFunction("dbo.GetAgama()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlAgama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAgama.DataTextField = "Keterangan";
        ddlAgama.DataValueField = "id";
        ddlAgama.DataBind();




        //// --- Combo kewarganegaraan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetCitizen()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlkewarganegaraan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlkewarganegaraan.DataTextField = "keterangan";
        //ddlkewarganegaraan.DataValueField = "id";
        //ddlkewarganegaraan.DataBind();

        // --- Combo Pendidikan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpendidikan.DataTextField = "keterangan";
        ddlpendidikan.DataValueField = "id";
        ddlpendidikan.DataBind();
        //  ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlpendidikan.DataTextField = "keterangan";
        //ddlpendidikan.DataValueField = "id";
        //ddlpendidikan.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiktp.DataTextField = "nama_provinsi";
        ddlprovinsiktp.DataValueField = "kode_provinsi";
        ddlprovinsiktp.DataBind();
        // ddlprovinsiktp.Items.Insert(0, new ListItem("Pilih Data", "-"));


        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPekerjaan.DataTextField = "keterangan";
        ddlPekerjaan.DataValueField = "id";
        ddlPekerjaan.DataBind();


        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetSektorUsaha()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_sektor_usaha asc";

        ddlSektorUsaha.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlSektorUsaha.DataTextField = "keterangan";
        ddlSektorUsaha.DataValueField = "kode_sektor_usaha";
        ddlSektorUsaha.DataBind();



        // --Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetLamaKerja()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddllamabekerja.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddllamabekerja.DataTextField = "keterangan";
        ddllamabekerja.DataValueField = "id";
        ddllamabekerja.DataBind();

        // --Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPenghasilanKotor()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpenghasilan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpenghasilan.DataTextField = "keterangan";
        ddlpenghasilan.DataValueField = "id";
        ddlpenghasilan.DataBind();


        // --Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetSumberPenghasilan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "keterangan";
        ddlsumberdana.DataValueField = "id";
        ddlsumberdana.DataBind();


        // --Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetTujuanBukaRekening()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddltujuanTransaksi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltujuanTransaksi.DataTextField = "keterangan";
        ddltujuanTransaksi.DataValueField = "id";
        ddltujuanTransaksi.DataBind();

    }


    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {

        var kodelgn = hdncustomer.Value;
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var, "constrmf");
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
            ddljenkel.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
            txttempatlahir.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
            txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse((ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())));

            ddlAgama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
            ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
            ddlpendidikan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
            TxtEmail.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
            txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();

            ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
            ddlSektorUsaha.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
            txtjenisusaha.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();
            ddllamabekerja.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["lamabekerja"].ToString();
            ddlpenghasilan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["penghasilan"].ToString();

            btnSimpan.Visible = true;


            ddlproduk.Enabled = true;
            ddlKoperasi.Enabled = false;
            ddlcabang.Enabled = false;
            ddljangka.Enabled = true;
            ddlbunga.Enabled = true;

            //RequiredFieldValidator1.Enabled = true;
           
            RequiredFieldValidator6.Enabled = true;
            RequiredFieldValidator7.Enabled = true;
     
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
            //RequiredFieldValidator1.Enabled = false;
         
           
            RequiredFieldValidator6.Enabled = false;
            RequiredFieldValidator7.Enabled = false;
         
        }


     


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
  
 
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx,string SessKoperasiID)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@SessKoperasiID", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKoperasiID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SN_getCustomer_Permohonan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    #region"Dimas"

    protected void UploadFileKTPwaris1(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/SimpananBerjangka/");
        DateTime now = DateTime.Now;
        string namafile = "", fotopath;

        namafile = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AhliWaris1";
        //File cekfile1 = new FileInfo(folderPath + )
        //Check whether Directory (Folder) exists.
        //////FileInfo file = new FileInfo(folderPath + namafile + ".jpg");
        //////if (file.Exists)
        //////{
        //////    file.Delete();
        //////}
        //////if (!Directory.Exists(folderPath))
        //////{
        //////    //If Directory (Folder) does not exists. Create it.
        //////    Directory.CreateDirectory(folderPath);
        //////}

        //////fileuploadktpwaris1.SaveAs(folderPath + namafile + ".jpg");
        //////txtpathktpwaris1.Text = namafile + ".jpg";

        //fotopath = folderPath + namafile + ".jpg";
        //txtpathktp2.Text = fotopath;
        //Boolean FileOK = false;

        //Boolean FileSaved = false;
        //string tipefile = "";
        //string cekfile = "";
        //string pathfile = "";

        //if (fileuploadktpwaris1.HasFile)
        //{
        //    Session["WorkingImage"] = fileuploadktpwaris1.FileName;

        //    String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

        //    String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".png" };

        //    for (int i = 0; i < allowedExtensions.Length; i++)
        //    {
        //        if (FileExtension == allowedExtensions[i])
        //        {
        //            FileOK = true;
        //            tipefile = allowedExtensions[i].ToString();
        //            cekfile = "masuk";
        //        }
        //    }
        //}
        //if (FileOK)
        //{
        //    try
        //    {
        //        //Check whether Directory (Folder) exists.
        //        if (!Directory.Exists(folderPath))
        //        {
        //            //If Directory (Folder) does not exists. Create it.
        //            Directory.CreateDirectory(folderPath);
        //        }
        //        //fileuploadktpwaris1.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);

        //        FileInfo file = new FileInfo(folderPath + Session["WorkingImage"]);
        //        //string pathori = "~/Files/SimpananBerjangka/" + Session["WorkingImage"];
        //        //string pathori1 = folderPath + Session["WorkingImage"];
        //        if (file.Exists)
        //        {
        //            file.Delete();
        //            //var stream1 = new FileStream(pathori1, (FileMode)FileAccess.Read);
        //            //var reader1 = new StreamReader(stream1);
        //            //stream1.Close();
        //            //reader1.Close();
        //        }

        //        //var stream = new FileStream(pathori, (FileMode)FileAccess.Read);
        //        //var reader = new StreamReader(stream);
        //        //File.Delete(pathori); 

        //        fileuploadktpwaris1.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
        //        System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
        //        img = resizeImage(img);
        //        img.Save(string.Concat(folderPath, txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
        //        img.Dispose();
        //        //var stream = new FileStream(folderPath + Session["WorkingImage"], (FileMode)FileAccess.ReadWrite);
        //        //var reader = new StreamReader(stream);
        //        fileuploadktpwaris1.PostedFile.InputStream.Dispose();
        //        if (file.Exists)
        //        {
        //            file.Delete();
        //        }
        //        //imgCroppedwaris1.ImageUrl = folderPath + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
        //        pathfile = "~/Files/SimpananBerjangka/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
        //        imgCroppedwaris1.Dispose();
        //        img.Dispose();
        //        //StreamReader.Close();

        //        //FileInfo file = new FileInfo(folderPath + Session["WorkingImage"]);
        //        //if (file.Exists)
        //        //{
        //        //    File.Delete(folderPath + Session["WorkingImage"]);
        //        //}
        //        //string folderPathapus = Server.MapPath("~\\Files\\SimpananBerjangka\\" + Session["WorkingImage"]);
        //        //File.Delete(folderPathapus);
        //        //img.de
        //        //File.Delete(fileuploadktpwaris1.FileBytes);

        //        fileuploadktpwaris1.Dispose();
        //        //if (File.Exists(pathori1))
        //        //{
        //        //    File.Delete(pathori1);
        //        //}
        //        FileSaved = true;
        //    }
        //    catch (System.Exception X)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //        FileSaved = false;
        //    }
        //}
        //else if (fileuploadktpwaris1.FileName == "" && cekfile == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih File Terlebih dahulu.');", true);
        //}
        //else if (fileuploadktpwaris1.FileName != "" && cekfile == "masuk")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis File harus image');", true);
        //}
        //if (FileSaved)
        //{
        //    txtpathktpwaris1.Text = pathfile;
        //    imgCroppedwaris1.ImageUrl = pathfile;
        //    //string path1 = Server.MapPath("~/Files/SimpananBerjangka/" + Session["WorkingImage"]);
        //    //FileAccess file1 = new FileAccess();
        //    ////file1.Delete();
        //    ////Bitmap.Dispose();
        //    ////pictureBox1.Image?.Dispose();

        //    //File.file1.
        //    //File.Delete(folderPath + Session["WorkingImage"].ToString());
        //    //fileuploadktpwaris1.(folderPath + Session["WorkingImage"]);
        //}

    }
    private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        //Get the image current width
        int sourceWidth = imgToResize.Width;
        //Get the image current height
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;
        //Calulate  width with new desired size
        nPercentW = ((float)size.Width / (float)sourceWidth);
        //Calculate height with new desired size
        nPercentH = ((float)size.Height / (float)sourceHeight);


        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;
        //New Width
        int destWidth = (int)(sourceWidth * nPercent);
        //New Height
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(60, 60);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw image with new width and height
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (System.Drawing.Image)b;
    }
    /// <summary>
    /// Function for resize image
    /// </summary>
    /// <param name="img">Image</param>
    /// <returns></returns>
    private System.Drawing.Image resizeImage(System.Drawing.Image img)
    {
        Bitmap b = new Bitmap(img);
        System.Drawing.Image i = resizeImage(b, new Size(60, 60));
        b.Dispose();
        return i;
    }
    protected void UploadFileKTPwaris2(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/SimpananBerjangka/");
        DateTime now = DateTime.Now;
        string namafileKTP = "", fotopathktp;

        namafileKTP = "tes" + "_" + now.ToString("ddMMyyyy") + "_" + "KTP";

        Boolean FileOK = false;

        Boolean FileSaved = false;
        string tipefile = "";
        string cekfile = "";
        string pathfile = "";

        if (fileuploadktpwaris2.HasFile)
        {
            Session["WorkingImage"] = fileuploadktpwaris2.FileName;

            String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".png" };

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;
                    tipefile = allowedExtensions[i].ToString();
                    cekfile = "masuk";
                }
            }
        }
        if (FileOK)
        {
            try
            {
                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                fileuploadktpwaris2.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
                System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
                img = resizeImage(img);
                img.Save(string.Concat(folderPath, txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
                //imgCroppedwaris2.ImageUrl = folderPath + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                pathfile = "~/Files/SimpananBerjangka/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                FileSaved = true;
            }
            catch (System.Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                FileSaved = false;
            }
        }
        else if (fileuploadktpwaris2.FileName == "" && cekfile == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih File Terlebih dahulu.');", true);
        }
        else if (fileuploadktpwaris2.FileName != "" && cekfile == "masuk")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis File harus image');", true);
        }
        if (FileSaved)
        {
            txtpathktpwaris2.Text = pathfile;
            imgCroppedwaris2.ImageUrl = pathfile;
        }

    }
    protected void clearFileInputField1(object sender, EventArgs e)
    {
        try
        {
                //if (txtpathktpwaris1.Text != "")
                //{
                //    string path1 = Server.MapPath(txtpathktpwaris1.Text);
                //    FileInfo file1 = new FileInfo(path1);
                //    if (file1.Exists)
                //    {
                //        file1.Delete();
                        txtpathktpwaris1.Text = "";
            //        }
            //    }
            //}
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void clearFileInputField2(object sender, EventArgs e)
    {
        try
        {

            //if (fileuploadktpwaris2.HasFile)
            //{

            //    if (txtpathktpwaris2.Text != "")
            //    {
            //        string path2 = Server.MapPath(txtpathktpwaris2.Text);
            //        FileInfo file2 = new FileInfo(path2);
            //        if (file2.Exists)
            //        {
            //            file2.Delete();
                        txtpathktpwaris2.Text = "";
            //        }
            //    }
            //}
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion"Dimas"
}
