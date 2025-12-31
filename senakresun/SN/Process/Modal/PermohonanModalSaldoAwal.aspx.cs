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

public partial class SD_Process_Modal_PermohonanModalSaldoAwal : System.Web.UI.Page
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
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
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
    string kodesumber = "SM";
    string KodeKonter = "SM02";
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
        //ddlbunga.Enabled = false;
        ddlInstruktur.SelectedValue = "2";
        ddlKdBank.SelectedValue = "110191";
        pnlFilter.Visible = false;
        ddlpenarikanbunga.SelectedValue = "2";
        ddlpenarikanbunga_SelectedIndexChanged(sender, e);
        //ddlKdBank.SelectedValue = "110141";
        pnlFilter.Visible = false;
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
                pnlFilter.Visible = false;
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
                ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "nama_koperasi";
                ddlKoperasi.DataValueField = "kode_koperasi";
                ddlKoperasi.DataBind();
                ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlKoperasi.SelectedValue = SessKoperasiID;

                DefaultClass.Global.SqlHelper[] var9 = new DefaultClass.Global.SqlHelper[3];
                var9[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKoperasiID, typeof(string));
                var9[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var9[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var9, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "nama_cabang";
                ddlcabang.DataValueField = "kode_cabang";
                ddlcabang.DataBind();
                ddlcabang.SelectedValue = SessBranchID;

                //KODE Produk
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPRODUCT", typeof(string));
                var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var4, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
                ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlproduk.DataTextField = "nama_produk";
                ddlproduk.DataValueField = "kode_produk";
                ddlproduk.DataBind();
                ddlproduk.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlproduk.SelectedValue = "C1";
                ddljangka.Items.Insert(0, new ListItem("1", "1"));


                //KODE Produk
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[3];
                var5[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var5[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPAJAK", typeof(string));
                var5[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var5, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Pajak asc";
                ddlpajak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlpajak.DataTextField = "Pajak";
                ddlpajak.DataValueField = "Pajak";
                ddlpajak.DataBind();
                //ddlpajak.Items.Insert(0, new ListItem("Pilih Data", "-"));
                //ddlpajak.SelectedValue = "10";
                ddlpajak.Enabled = false;                

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";

                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "namabank";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();
                ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));

                var Produk = ddlproduk.SelectedValue;
                int jangka = int.Parse(ddljangka.SelectedValue);
                DefaultClass.Global.SqlHelper[] var7 = new DefaultClass.Global.SqlHelper[5];
                var7[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                var7[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
                var7[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var7[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var7[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var7, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
                ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlbunga.DataTextField = "BungaTahunan";
                ddlbunga.DataValueField = "BungaTahunan";
                ddlbunga.DataBind();
                //ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlbunga.Enabled = true;
                //txtnilaisaldo.Enabled = false;
                InsertCombo();
                ddlInstruktur.Enabled = false;
                ddlInstruktur.SelectedValue = "2";
                ddlpenarikanbunga.Enabled = true;
                ddlpenarikanbunga.SelectedValue = "1";
                ddlbunga_SelectedIndexChanged(sender, e);
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

    protected void ddlproduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Produk = ddlproduk.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
        ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangka.DataTextField = "Jangka";
        ddljangka.DataValueField = "Jangka";
        ddljangka.DataBind();
        //ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));

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
        ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
        ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbunga.DataTextField = "BungaTahunan";
        ddlbunga.DataValueField = "BungaTahunan";
        ddlbunga.DataBind();
        //ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }
    protected void ddlKoperasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Koperasi = ddlKoperasi.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
     
        ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "nama_cabang";
        ddlcabang.DataValueField = "kode_cabang";
        ddlcabang.DataBind();
        //ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

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
    protected void ddlpenarikanbungasss_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpenarikanbunga.SelectedValue == "1")
        {

            txtbankrekening.Enabled = true;
            txtnorekening.Enabled = true;
            txtatasnama.Enabled = true;
        }
        else
        {

            //if (ddlpenarikanbunga.SelectedValue == "3")
            //{
            //    ddlrollover.Enabled = true;
            //}
            txtbankrekening.Text = "-";
            txtbankrekening.Enabled = false;
            txtnorekening.Text = "-";
            txtnorekening.Enabled = false;
            txtatasnama.Text = "-";
            txtatasnama.Enabled = false;
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
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
            var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[5] = gb.CreateSPParameter("@typepermohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, "A", typeof(string));
            var[6] = gb.CreateSPParameter("@typemodal", ParameterDirection.Input, SqlDbType.VarChar, 1, "M", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "Idpermohonan Desc";


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
    public void BindGridEntityx()
    {
        try
        {
            string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";
            //string sign =  " like " ;

            //var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'%" + txtSearch.Text + "%'" : "");
            var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
            Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "PERMOHONANSIMPANAN", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("SM_GetSEARCH", Params, "constrmf");

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
                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");

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




                    ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
                    ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();
                    ddlrollover.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pencairanbunga"].ToString();
                    txtbankrekening.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBank"].ToString();
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["Atasnama"].ToString();

                    //path
                    txtpathktpwaris1.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris1")).Value;
                    txtpathktpwaris2.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris2")).Value;
                    txtpathbuktitransfer.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Value;

                   
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

                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", varx, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", varc, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", varj, "constrmf");
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
                        DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[5];
                        vart[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                        vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                        vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        vart[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        vart[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", vart, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows.Count > 0)


                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permohonan Tersebut Sudah ada transaksi!');", true);
                            btnSimpan.Visible = false;
                        }
                        else
                        {
                            btnSimpan.Visible = true;

                        }
                    }

                    //DefaultClass.Global.SqlHelper[] ParamPosting = new DefaultClass.Global.SqlHelper[4];
                    //ParamPosting[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    //ParamPosting[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    //ParamPosting[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    //ParamPosting[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKPOSTING", typeof(String));
                    //ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", ParamPosting, "constrmf");
                    //string pubposting = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                    //if (pubposting == "1")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting!');", true);
                    //    btnSimpan.Visible = false;
                    //}
                    //else
                    //{
                    //    btnSimpan.Visible = true;
                    //}


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
                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");

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

                    ddlrollover.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pencairanbunga"].ToString();

                    txtnobuktisetoran.Text = ge.ListDataset.Tables[0].Rows[0]["nobuktisetoran"].ToString();


                    //path
                    txtpathktpwaris1.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris1")).Value;
                    txtpathktpwaris2.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris2")).Value;
                    txtpathbuktitransfer.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Value;


                    ddlKoperasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();
                    ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["produk"].ToString();

                    ddlpajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString();


                    txtbungaperhari.Text = string.Format("{0:N6}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                    txtnilaisaldo.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaisaldo"].ToString()));

                    txtnilaibungapertahun.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungapertahun"].ToString()));
                    txtnilaibungaperhari.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungaperhari"].ToString()));


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
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["Atasnama"].ToString();
                    ddlsetor.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenissetoran"].ToString();
                    ddlKdBank.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodebank"].ToString();

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

                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", varx, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", varc, "constrmf");
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
                    ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", varj, "constrmf");
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
                        DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[5];
                        vart[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                        vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                        vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        vart[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        vart[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", vart, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows.Count > 0)


                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permohonan Tersebut Sudah ada transaksi!');", true);
                            btndelete.Visible = false;
                        }
                        else
                        {
                            btndelete.Visible = true;

                        }
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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SM_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

       
                            rprt.Load(Server.MapPath("~/SN/Process/Modal/ReportFormulirberjangkapajak.rpt"));
          
                    
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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SM_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (pajak == "0")
                        {
                            rprt.Load(Server.MapPath("~/SM/Process/Reportbillyednonpajak.rpt"));
                        }
                        else
                        {
                            rprt.Load(Server.MapPath("~/SM/Process/Reportbillyed.rpt"));
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
            case "JOURNAL":
                try
                {

                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

               
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("SM_Permohonan", Paramsc, "constrmf").Tables[0];



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


                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSI", typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SM_Permohonan", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AR/Process/Report/CLKwitansiTerima.rpt"));

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

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[8];
                Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
                Params[4] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[5] = gb.CreateSPParameter("@jenisetoran", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlsetor.SelectedValue), typeof(int));
                Params[6] = gb.CreateSPParameter("@nobuktisetoran", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobuktisetoran.Text, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", Params, "constrmf");
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
                    //coba apus

                    if (txtpathktpwaris1.Text != "")
                    {
                        string path = Server.MapPath(txtpathktpwaris1.Text);

                        FileInfo file = new FileInfo(path);
                        if (file.Exists)//check file exsit or not.
                        {
                            file.Delete();
                        }
                    }
                    if (txtpathktpwaris2.Text != "")
                    {
                        string path = Server.MapPath(txtpathktpwaris2.Text);

                        FileInfo file = new FileInfo(path);
                        if (file.Exists)//check file exsit or not.
                        {
                            file.Delete();
                        }
                    }
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
        try
        {
            decimal jlhbayartahun = 0;
            decimal jlhbayarhari = 0;
            if (ddlbunga.SelectedValue == "-")
            {
                ddlbunga.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Bunga % Pertahun dahulu.');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningALert('Silahkan Pilih Bunga % Pertahun dahulu.');", true);
            }
            else
            {
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
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
         {
            if(txtnilaisaldo.Text == "" || txtnilaisaldo.Text == "0")
            {
                txtnilaisaldo.Focus();
                txtnilaisaldo.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Masukkan Nilai Saldo terlbih dahulu dan Tidak Boleh 0');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningALert('Silahkan Masukkan Nilai Saldo terlbih dahulu dan Tidak Boleh 0.');", true);
            }
            else if (ddlKdBank.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank Masih Kosong);", true);

            }
            else
            {
                if (ddlbunga.SelectedValue == "-")
                {
                    ddlbunga.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Bunga % Pertahun dahulu.');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningALert('Silahkan Pilih Bunga % Pertahun dahulu.');", true);
                }
                else
                {
                    string State = "";
                    string namafile = "";
                    string fotopath = "";
                    string ambilnama = "";
                    if (txtnobukti.Text == "")
                    {
                        State = "INSERT";
                    }
                    else
                    {
                        State = "UPDATE";
                    }

                   
                        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[67];

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
                        Params[29] = gb.CreateSPParameter("@Atasnama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtatasnama.Text, typeof(string));
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
                        Params[43] = gb.CreateSPParameter("@pathktpWaris2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpathbuktitransfer.Text, typeof(string));
                        Params[44] = gb.CreateSPParameter("@jenisetoran", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlsetor.SelectedValue), typeof(int));
                        Params[45] = gb.CreateSPParameter("@Kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                        Params[46] = gb.CreateSPParameter("@PathFile", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpathbuktitransfer.Text, typeof(string));
                        Params[47] = gb.CreateSPParameter("@JenisKelamin", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenkel.SelectedValue, typeof(string));
                        Params[48] = gb.CreateSPParameter("@TempatLahir", ParameterDirection.Input, SqlDbType.VarChar, 30, txttempatlahir.Text, typeof(string));
                        Params[49] = gb.CreateSPParameter("@tanggal_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(DateTime));
                        Params[50] = gb.CreateSPParameter("@agama", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlAgama.SelectedValue, typeof(string));
                        Params[51] = gb.CreateSPParameter("@status_pernikahan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikah.SelectedValue, typeof(string));
                        Params[52] = gb.CreateSPParameter("@pendidikan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpendidikan.SelectedValue, typeof(string));
                        Params[53] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 20, txthp.Text, typeof(string));
                        Params[54] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 40, TxtEmail.Text, typeof(string));
                        Params[55] = gb.CreateSPParameter("@simpananwajib", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtsimpananwajib.Text), typeof(decimal));
                        Params[56] = gb.CreateSPParameter("@pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlPekerjaan.SelectedValue, typeof(string));
                        Params[57] = gb.CreateSPParameter("@sektorusaha", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlSektorUsaha.SelectedValue, typeof(string));
                        Params[58] = gb.CreateSPParameter("@jenisusaha", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenisusaha.Text, typeof(string));
                        Params[59] = gb.CreateSPParameter("@lamabekerja", ParameterDirection.Input, SqlDbType.VarChar, 2, ddllamabekerja.SelectedValue, typeof(string));
                        Params[60] = gb.CreateSPParameter("@penghasilan", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlpenghasilan.SelectedValue, typeof(string));
                        Params[61] = gb.CreateSPParameter("@sumberdana", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlsumberdana.SelectedValue, typeof(string));
                        Params[62] = gb.CreateSPParameter("@tujuantransaksi", ParameterDirection.Input, SqlDbType.VarChar, 4, ddltujuanTransaksi.SelectedValue, typeof(string));
                        Params[63] = gb.CreateSPParameter("@ketsumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, txtsumberdana.Text, typeof(string));
                        Params[64] = gb.CreateSPParameter("@kettujuantransaksi", ParameterDirection.Input, SqlDbType.VarChar, 50, txttujuantransaksi.Text, typeof(string));
                        Params[65] = gb.CreateSPParameter("@typepermohonan", ParameterDirection.Input, SqlDbType.VarChar, 50, "A", typeof(string));
                        Params[66] = gb.CreateSPParameter("@typemodal", ParameterDirection.Input, SqlDbType.VarChar, 50, "M", typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", Params, "constrmf");


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
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        //varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        //varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        //varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        //string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        //if (xstatus == "1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
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


    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SM_Permohonan", var, "constrmf");
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


            //ddlproduk.Enabled = true;
            //ddlKoperasi.Enabled = true;
            //ddlcabang.Enabled = true;
            //ddljangka.Enabled = true;
            //ddlbunga.Enabled = true;

            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
          
            RequiredFieldValidator6.Enabled = true;
            //RequiredFieldValidator7.Enabled = true;
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
            RequiredFieldValidator2.Enabled = false;
            RequiredFieldValidator3.Enabled = false;
           
            RequiredFieldValidator6.Enabled = false;
            //RequiredFieldValidator7.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
        }


     


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



    }
  
 
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessKoperasiIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKoperasiIDx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));


        ge.ListDataset = gb.GetDataJustOutPut("dbo.SM_getCustomer_Permohonan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    #region"Dimas"

    protected void UploadFileKTPwaris1(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/permohonanmodal/");
        DateTime now = DateTime.Now;
        string namafileKTP = "", fotopathktp;

        namafileKTP = "tes" + "_" + now.ToString("ddMMyyyy") + "_" + "KTP";

        Boolean FileOK = false;

        Boolean FileSaved = false;
        string tipefile = "";
        string cekfile = "";
        string pathfile = "";

        if (fileuploadktpwaris1.HasFile)
        {
            Session["WorkingImage"] = fileuploadktpwaris1.FileName;

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
                fileuploadktpwaris1.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
                System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
                img = resizeImage(img);
                img.Save(string.Concat(folderPath, txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
                //imgCroppedwaris1.ImageUrl = folderPath + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                pathfile = "~/Files/permohonanmodal/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                imgCroppedwaris1.Dispose();
                img.Dispose();
                //StreamReader.Close();

                //FileInfo file = new FileInfo(folderPath + Session["WorkingImage"]);
                //if (file.Exists)
                //{
                //    File.Delete(folderPath + Session["WorkingImage"]);
                //}
                //string folderPathapus = Server.MapPath("~\\Files\\SimpananBerjangka\\" + Session["WorkingImage"]);
                //File.Delete(folderPathapus);
                //img.de
                //File.Delete(fileuploadktpwaris1.FileBytes);
                FileSaved = true;
            }
            catch (System.Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                FileSaved = false;
            }
        }
        else if (fileuploadktpwaris1.FileName == "" && cekfile == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih File Terlebih dahulu.');", true);
        }
        else if (fileuploadktpwaris1.FileName != "" && cekfile == "masuk")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis File harus image');", true);
        }
        if (FileSaved)
        {
            txtpathktpwaris1.Text = pathfile;
            imgCroppedwaris1.ImageUrl = pathfile;
            //string path1 = Server.MapPath("~/Files/permohonanmodal/" + Session["WorkingImage"]);
            //FileAccess file1 = new FileAccess();
            ////file1.Delete();
            ////Bitmap.Dispose();
            ////pictureBox1.Image?.Dispose();

            //File.file1.
            //File.Delete(folderPath + Session["WorkingImage"].ToString());
            //fileuploadktpwaris1.(folderPath + Session["WorkingImage"]);
        }

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
        string folderPath = Server.MapPath("~/Files/permohonanmodal/");
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
                pathfile = "~/Files/permohonanmodal/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
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
    protected void UploadFileBuktitrans(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/permohonanmodal/");
        DateTime now = DateTime.Now;
        string namafileKTP = "", fotopathktp;

        namafileKTP = "tes" + "_" + now.ToString("ddMMyyyy") + "_" + "Bukti";

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
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                fileuploadktpwaris2.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
                System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
                img = resizeImage(img);
                img.Save(string.Concat(folderPath, txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
                imgCroppedBukti.ImageUrl = folderPath + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                pathfile = "~/Files/permohonan/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];

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
