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
public partial class AR_Process_ApprovalPinjaman : System.Web.UI.Page
{
    String nosebelumnya = "", updateno = "", CRno_rekening = "", STATE = "", kdpersh = "", format_no_pk = "",
            formatnumber = "", formatpknumber = "", formatnumberbukti = "", nopksebelumnya = "", CRno_pk = "", updatepkno  = "" , pokok = "", bunga = "",
            typetrn = "", keterangantrn = "", no_bukti_sebelumnya = "", NoBuktiNew = "";
    double ttlbunga = 0;
    int counter_no_rekening = 0,   i = 0, x = 0, sisapk = 0;

    long no_pk_sekarang = 0, no_pk = 0, nosekarang = 0, no_rekening = 0, nonasabah = 0, no_bukti_sekarang = 0, no_bukti = 0;

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string id = "", no_nasabah = "", flag = "";

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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovalPinjaman.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovalPinjaman.aspx");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlIdentitas.Visible = true;
    }
    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {

        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = true;


    }

    protected void btnNextTambahan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = true;

        lbltotalPengeluaran.Text = "Total Pengeluaran";
        lbltotalPengeluaran.Font.Bold = true;

        lbltotalpendapatan.Text = "Total Pendapatan";
        lbltotalpendapatan.Font.Bold = true;

        lblsisapenghasilan.Text = "Sisa Penghasilan";
        lblsisapenghasilan.Font.Bold = true;
    }
    protected void btnNextPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        //pnlLaba.Visible = true;

        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPihakYgDapatDiHubungi.Visible = true;
    }
    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        pnlIdentitas.Visible = false;
    }
    protected void btnNexIdentitas_Click(object sender, EventArgs e)
    {

        pnlIdentitas.Visible = false;
        pnlPekerjaan.Visible = true;
    }
    protected void btnBackPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = false;

    }
    protected void btnNextPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        //pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = false;
        pnlJaminan.Visible = true;


    }
    protected void btnNextJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPihakYgDapatDiHubungi.Visible = false;
        pnlPengajuan.Visible = true;
        pnlJaminan.Visible = false;

    }
    protected void btnBackJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPihakYgDapatDiHubungi.Visible = true;
        pnlPengajuan.Visible = false;
        pnlJaminan.Visible = false;

    }

    protected void btnNextPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        //pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = true;
        pnlJaminan.Visible = true;
    }

    protected void btnbackawal_Click(object sender, EventArgs e)
    {
        pnlFilter.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
    }
    protected void btnBackPekerjaan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        pnlPekerjaan.Visible = false;
    }

    protected void btnBackTambahan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = true;
        pnlTambahan.Visible = false;

    }
    protected void btnBackPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = true;
        pnlPendapatan.Visible = false;
    }

    protected void btnBackPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;

        pnlPengajuan.Visible = false;
        //pnlKeanggotaan.Visible = false;
        pnlPihakYgDapatDiHubungi.Visible = false;
        pnlJaminan.Visible = true;
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        //pnlUsaha.Visible = false;
    }


    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = true;
        pnlAlasanTolak.Visible = false;
        //ProsesVerifikasi("0", SessLoginID);
    }

    protected void btnTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        //pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = false;
        pnlAlasanTolak.Visible = true;
    }


    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        try
        {

            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[9];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "7", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "7", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REJECTSTATUS", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");


            //DefaultClass.Global.SqlHelper[] var77 = new DefaultClass.Global.SqlHelper[5];
            //var77[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            //var77[1] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //var77[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //var77[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEPEMBATALAN", typeof(string));
            //var77[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var77, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }
    protected void btnSetuju_Click(object sender, EventArgs e)
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        //GET NOMER PK

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
        var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var2[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERNOPK", typeof(string));
        var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", var2, "constrmf");

        no_pk_sekarang = long.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
        formatpknumber = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
        nopksebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();
        format_no_pk = ge.ListDataset.Tables[0].Rows[0]["format_no_pk"].ToString();

        no_pk = no_pk_sekarang + 1;

        CRno_pk = String.Format("{0:" + formatpknumber + "}", no_pk) + "/" + format_no_pk + SessBranchID + "/" + txttglrealisasi.Text.Substring(0, 5).Replace("/", "-") + "/" + txttglrealisasi.Text.Substring(6, 4);

        updatepkno = String.Format("{0:" + formatnumber + "}", no_pk);

        HDnopk.Value = CRno_pk;
        HDnopksekarang.Value = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();
        HDNoPKSebelumnya.Value = nopksebelumnya;

        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
        var4[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
        var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf");



        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
        {
            HDStatusDebitur.Value = "B";
        }
        else
        {
            HDStatusDebitur.Value = "-";
        }


        ProsesApprovalKredit("6", SessLoginID);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BindGridEntity();
            ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlPekerjaan.DataTextField = "keterangan";
            ddlPekerjaan.DataValueField = "id";
            ddlPekerjaan.DataBind();


            ddlpekerjaanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpekerjaanktp.DataTextField = "keterangan";
            ddlpekerjaanktp.DataValueField = "id";
            ddlpekerjaanktp.DataBind();



            ge.ListDataset = gb.GetDataFunction("dbo.GetLamaKerja()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlLamaBekerja.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlLamaBekerja.DataTextField = "keterangan";
            ddlLamaBekerja.DataValueField = "id";
            ddlLamaBekerja.DataBind();

            ge.ListDataset = gb.GetDataFunction("dbo.GetKepemilikanTempatUsaha()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlkepemilikantempatusaha.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkepemilikantempatusaha.DataTextField = "keterangan";
            ddlkepemilikantempatusaha.DataValueField = "id";
            ddlkepemilikantempatusaha.DataBind();
            // -- Combo Alamat Surat
            ge.ListDataset = gb.GetDataFunction("dbo.GetAlamatSurat()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlAlamatSurat.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlAlamatSurat.DataTextField = "keterangan";
            ddlAlamatSurat.DataValueField = "id";
            ddlAlamatSurat.DataBind();
            // -- Combo Provinsi Pekerjaan
            ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

            ddlprovinsipekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlprovinsipekerjaan.DataTextField = "nama_provinsi";
            ddlprovinsipekerjaan.DataValueField = "kode_provinsi";
            ddlprovinsipekerjaan.DataBind();




            // COMBO PROVINSI 
            ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

            ddlProvinsiPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

            ddlProvinsiPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlProvinsiPihakLain.DataTextField = "nama_provinsi";
            ddlProvinsiPihakLain.DataValueField = "kode_provinsi";
            ddlProvinsiPihakLain.DataBind();

            ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

            ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

            ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpropinsiktp.DataTextField = "nama_provinsi";
            ddlpropinsiktp.DataValueField = "kode_provinsi";
            ddlpropinsiktp.DataBind();


            // COMBO MASTER HUBUNGAN 
            ge.ListDataset = gb.GetDataFunction("dbo.GetHubungan()", "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlHubunganPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

            ddlHubunganPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlHubunganPihakLain.DataTextField = "keterangan";
            ddlHubunganPihakLain.DataValueField = "id";
            ddlHubunganPihakLain.DataBind();
            // --- Combo Status Rumah
            ge.ListDataset = gb.GetDataFunction("dbo.GetStatusRumah()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlstatusrumah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlstatusrumah.DataTextField = "keterangan";
            ddlstatusrumah.DataValueField = "id";
            ddlstatusrumah.DataBind();

            ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

            ddlprovinsitpt.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlprovinsitpt.DataTextField = "nama_provinsi";
            ddlprovinsitpt.DataValueField = "kode_provinsi";
            ddlprovinsitpt.DataBind();

            // --- Combo Jenis Kelamin
            ge.ListDataset = gb.GetDataFunction("dbo.GetJenisKelamin()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddljenkelktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljenkelktp.DataTextField = "Keterangan";
            ddljenkelktp.DataValueField = "id";
            ddljenkelktp.DataBind();
            //ddljenkel.Items.Insert(0, new ListItem("Pilih Data", "-"));

            // --- Combo Status Pernikahan
            ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlstatusnikahktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlstatusnikahktp.DataTextField = "Keterangan";
            ddlstatusnikahktp.DataValueField = "id";
            ddlstatusnikahktp.DataBind();
            ddlstatuskawinupload.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlstatuskawinupload.DataTextField = "Keterangan";
            ddlstatuskawinupload.DataValueField = "id";
            ddlstatuskawinupload.DataBind();
            // ddlstatusnikah.Items.Insert(0, new ListItem("Pilih Data", "-"));

            // --- Combo Agama
            ge.ListDataset = gb.GetDataFunction("dbo.GetAgama()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlAgamaktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlAgamaktp.DataTextField = "Keterangan";
            ddlAgamaktp.DataValueField = "id";
            ddlAgamaktp.DataBind();




            // --- Combo kewarganegaraan
            ge.ListDataset = gb.GetDataFunction("dbo.GetCitizen()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlkewarganegaraanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkewarganegaraanktp.DataTextField = "keterangan";
            ddlkewarganegaraanktp.DataValueField = "id";
            ddlkewarganegaraanktp.DataBind();

            // --- Combo Pendidikan
            ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

            ddlpendidikanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpendidikanktp.DataTextField = "keterangan";
            ddlpendidikanktp.DataValueField = "id";
            ddlpendidikanktp.DataBind();

            // --- Combo  Jenis Penggunaan
            ge.ListDataset = gb.GetDataFunction("dbo.GetPenggunaan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penggunaan asc";

            ddlPenggunaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlPenggunaan.DataTextField = "penggunaan";
            ddlPenggunaan.DataValueField = "kode_penggunaan";
            ddlPenggunaan.DataBind();
            //ddlPenggunaan.Items.Insert(0, new ListItem("Pilih Data", "-"));
            // --- Combo Golongan penjamin
            ge.ListDataset = gb.GetDataFunction("dbo.GetGolonganPenjamin()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penjamin asc";

            ddlgolonganpenjamin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgolonganpenjamin.DataTextField = "keterangan";
            ddlgolonganpenjamin.DataValueField = "kode_penjamin";
            ddlgolonganpenjamin.DataBind();
            //ddlgolonganpenjamin.Items.Insert(0, new ListItem("Pilih Data", "-"));

            // --- Combo Golongan Debitur
            ge.ListDataset = gb.GetDataFunction("dbo.GetGolonganDebitur()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_gol_debitur asc";

            ddlgolongandebitur.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgolongandebitur.DataTextField = "keterangan";
            ddlgolongandebitur.DataValueField = "kode_gol_debitur";
            ddlgolongandebitur.DataBind();



            btnSimpanTolakan.Enabled = true;
            btnSetuju.Enabled = true;
        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDKodeproduk.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text;
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");
                    //data ktp
                    txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                    ddlpropinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                    txtkotaktp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                    txttempatlahirktp.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                    txttgllahirktp.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                    txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();
                    ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                    txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                    txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                    ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                    txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["simpananpokok"].ToString()));
                    txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["simpananwajib"].ToString()));

                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    //data ktp

                    //data tempat tinggal
                    txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                    txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();
                    txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    ddlprovinsitpt.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();

                    //data upload
                    ddlstatuskawinupload.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_dalam_keluarga"].ToString();
                    if (ddlstatuskawinupload.SelectedIndex == 0)
                    {
                        txtidpasangan.Enabled = false;
                        txtidpasangan.Text = "-";
                        txtnamapasangan.Enabled = false;
                        txtnamapasangan.Text = "-";
                        txttempatlahirpasangan.Enabled = false;
                        txttempatlahirpasangan.Text = "-";
                        txttgllahirpasangan.Enabled = false;
                        txttgllahirpasangan.Text = "1900-01-01 00:00:00";
                        //txtkalicadangan.Focus();
                        //btnuploadktppasangan.Enabled = false;
                    }
                    else
                    {

                        txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                        txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                        txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                        txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();
                        //btnuploadktppasangan.Enabled = true;
                    }

                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();

                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                    Imagenpwp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                    ImageKtppasangan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                    Imagekk.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                    //txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString(); // "?r=" + DateTime.Now.Ticks.ToString();
                    //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                    //txtpathKK.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();

                    txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();


                    //data pekerjaan



                    ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_pekerjaan"].ToString();
                    txtPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pekerjaan"].ToString();
                    txtjabatan.Text = ge.ListDataset.Tables[0].Rows[0]["jabatan"].ToString();
                    txtNamaPerusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_perusahaan"].ToString();
                    txtBidangUsaha.Text = ge.ListDataset.Tables[0].Rows[0]["bidang_usaha"].ToString();
                    ddlLamaBekerja.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["lama_bekerja"].ToString();
                    ddlkepemilikantempatusaha.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kepemilikan_tempat_usaha"].ToString();
                    txtkepemilikantempatusaha.Text = ge.ListDataset.Tables[0].Rows[0]["tempatusahalain"].ToString();
                    txtalamatpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan1"].ToString();
                    txtkabupatenpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenpt"].ToString();
                    txtkelurahanpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanpt"].ToString();
                    txtkecamatanpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanpt"].ToString();
                    ddlprovinsipekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsipt"].ToString();
                    txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kode_pospt"].ToString();
                    txtteleponpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["teleponpekerjaan"].ToString();
                    txtFaxPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["fax"].ToString();


                    //panel Data tambahan
                    ddlAlamatSurat.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["alamat_surat"].ToString();
                    ddlPenggunaan.SelectedValue = "10";
                    ddlgolongandebitur.SelectedValue = "874";
                    txtjumlahanak.Text = ge.ListDataset.Tables[0].Rows[0]["jumlah_anak"].ToString();
                    txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();

                    //txtktppenjamin.Text = "0";
                    //txtnamapenjamin.Text = "-";
                    //ddlgolonganpenjamin.SelectedValue = "874";
                    //txtpendapatanperbulan.Text = "0";
                    //ddlfisikjaminan.SelectedValue = "999";
                    //txtjaminantambahan.Text = "-";
                    ddlPenggunaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jns_pgn"].ToString();
                    ddlgolongandebitur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan_debitur"].ToString();
                    txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                    txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                    ddlgolonganpenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan_penjamin"].ToString();
                    txtpendapatanperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pendapatan_bulan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["pendapatan_bulan"].ToString();


                    //panel Data jaminan
                    txtnamapihaklain.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pihaklain"].ToString();
                    ddlHubunganPihakLain.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["hubungan"].ToString();
                    txtalamatPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat"].ToString();
                    txtRTPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["rt_pihak"].ToString();
                    txtRWPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["rw_pihak"].ToString();
                    txtKelurahanPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kelurahan_pihak"].ToString();
                    txtKecamatanPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kecamatan_pihak"].ToString();
                    txtKotaPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["kota_pihak"].ToString();
                    ddlProvinsiPihakLain.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Provinsi_pihak"].ToString();
                    txtKodePosPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["kode_pos_pihaklain"].ToString();
                    txtTlpPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["tlp_pihaklain"].ToString();

                    //panel Data Pendapatan Nasabah
                    txtpendapatanbersih.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pendapatan_bersih_usaha"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["pendapatan_bersih_usaha"].ToString();
                    txtpendapatanselain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pendapatan_selain_usaha"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["pendapatan_selain_usaha"].ToString();
                    txttotalpendapatan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total_pendapatan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["total_pendapatan"].ToString();
                    txtpengeluaranrumahtangga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pengeluaran_rumah_tangga"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["pengeluaran_rumah_tangga"].ToString();
                    txttagihanlistrik.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tagihan_pln_pam"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tagihan_pln_pam"].ToString();
                    txtpinjamanlain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pinjaman_kewajiban_lain"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["pinjaman_kewajiban_lain"].ToString();
                    txtangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rekomendasi_angsuran"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["rekomendasi_angsuran"].ToString();
                    txttotal.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total_angsuran"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["total_angsuran"].ToString();
                    txtSisaPenghasilan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisa_penghasilan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["sisa_penghasilan"].ToString();

                    //panel Data Kondisi Usaha Nasabah
                    txtkondisiusaha.Text = ge.ListDataset.Tables[0].Rows[0]["kondisi_usaha"].ToString();
                    txtKarakterAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["karakter_anggota"].ToString();
                    txtinfo.Text = ge.ListDataset.Tables[0].Rows[0]["info_anggota_dari_pihak3"].ToString();
                    txtkondisipesaing.Text = ge.ListDataset.Tables[0].Rows[0]["kondisi_pesaing"].ToString();
                    txtresikousaha.Text = ge.ListDataset.Tables[0].Rows[0]["resiko_usaha"].ToString();
                    txtatasiresiko.Text = ge.ListDataset.Tables[0].Rows[0]["upaya_mengatasi_resiko_usaha"].ToString();

                    txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                    txttaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi"].ToString();
                    txtkondisiJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();

                    imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    txtpathjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                    ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                    //Panel Data pengajuan kredit nasabah
                    txtnoreklama.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtjeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                    //txtjenispenggunaan.Text = ge.ListDataset.Tables[0].Rows[0]["penggunaan"].ToString();
                    txttglrealisasi.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString();
                    txtjenispinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString(); ;
                    txtprogrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["ketprogram"].ToString();
                    txtsumberdana.Text = ge.ListDataset.Tables[0].Rows[0]["kdpersh1"].ToString();
                    txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["permohonan_bendahara"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["permohonan_bendahara"].ToString();
                    txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka"].ToString();
                    txtbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtProvisi.Text = ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString();
                    txtnominalprovisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString();
                    txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString();
                    txtadminPencairan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_administrasi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_administrasi"].ToString();
                    txtadminangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_angsuran1"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_angsuran1"].ToString();
                    txtsifat.Text = ge.ListDataset.Tables[0].Rows[0]["sifat"].ToString();
                    txtAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txtNonAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txttablain.Text = "-";
                    txtsektorekonomi.Text = ge.ListDataset.Tables[0].Rows[0]["sektor_ekonomi"].ToString();


                    TextBox3.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString();
                    TextBox5.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi"].ToString();
                    txtjttempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_jatuh_tempo"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tgl_jatuh_tempo"].ToString();
                    txtangsurandimuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_dimuka"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["angsuran_dimuka"].ToString();
                    txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString();
                    txtbyasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString();
                    txtbypelunasan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_pelunasan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_pelunasan"].ToString();
                    txtbynotaris.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["notaris"].ToString())); // ge.ListDataset.Tables[0].Rows[0]["notaris"].ToString();
                    TextBox14.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blokir_angsuran"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["blokir_angsuran"].ToString();
                    TextBox15.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nominal_blokir_angsuran"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["nominal_blokir_angsuran"].ToString();
                    txtjenisblokir.Text = ge.ListDataset.Tables[0].Rows[0]["jenis_blokir"].ToString();


                    HDKodePos.Value = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                    HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    HDLamaKerja.Value = ge.ListDataset.Tables[0].Rows[0]["lama_bekerja"].ToString();

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
        }
    }

    public void ProsesApprovalKredit(string flag, string user)
    {

        try

        {

            btnSetuju.Enabled = false;
            //bunga = gb.GetNominalBunga(Double.Parse(txtbunga.Text.Trim().Replace(".", ",")), int.Parse(txtjangka.Text), double.Parse(txtplafon.Text), txtjeniskredit.Text);

            //ttlbunga = double.Parse(bunga) * double.Parse(txtjangka.Text);

            //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[172];

            //var1[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //var1[1] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[2] = gb.CreateSPParameter("@no_rekening_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "-",typeof(string));
            //var1[3] = gb.CreateSPParameter("@no_rek_tabungan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnorektabungan.Text, typeof(string));
            //var1[4] = gb.CreateSPParameter("@no_rek_tabungan_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[5] = gb.CreateSPParameter("@no_rek_giro", ParameterDirection.Input, SqlDbType.VarChar, 250 , "-", typeof(string));
            //var1[6] = gb.CreateSPParameter("@no_karip", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[7] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
            //var1[8] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjeniskredit.Text, typeof(string));
            //var1[9] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenispenggunaan.Text.Trim(), typeof(string));
            //var1[10] = gb.CreateSPParameter("@rek_lama", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[11] = gb.CreateSPParameter("@rek_baru", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[12] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //var1[13] = gb.CreateSPParameter("@kddapem", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[14] = gb.CreateSPParameter("@jns_pen", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[15] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, HDnopk.Value, typeof(string));
            //var1[16] = gb.CreateSPParameter("@no_aksep", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[17] = gb.CreateSPParameter("@rek_tablm", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[18] = gb.CreateSPParameter("@rek_tabbr", ParameterDirection.Input, SqlDbType.VarChar, 50, txtangsuranperbulan.Text.Trim(), typeof(string));
            //var1[19] = gb.CreateSPParameter("@prblkr", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox7.Text.Trim(), typeof(string)); //
            //var1[20] = gb.CreateSPParameter("@tgl_drop", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
            //var1[21] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
            //var1[22] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjangka.Text.Trim(), typeof(string));
            //var1[23] = gb.CreateSPParameter("@satuan", ParameterDirection.Input, SqlDbType.VarChar, 250, flag, typeof(string)); //
            //var1[24] = gb.CreateSPParameter("@seqno", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[25] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text.Trim(), typeof(string));
            //var1[26] = gb.CreateSPParameter("@sld_awl", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[27] = gb.CreateSPParameter("@debet", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[28] = gb.CreateSPParameter("@kredit", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[29] = gb.CreateSPParameter("@bunga_flat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbunga.Text.Trim(), typeof(string));
            //var1[30] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbunga.Text.Trim(), typeof(string)); //
            //var1[31] = gb.CreateSPParameter("@jns_bunga", ParameterDirection.Input, SqlDbType.VarChar, 250, flag, typeof(string)); //
            //var1[32] = gb.CreateSPParameter("@byprov", ParameterDirection.Input, SqlDbType.VarChar, 250, txtProvisi.Text.Trim(), typeof(string));
            //var1[33] = gb.CreateSPParameter("@oscprov", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnominalprovisi.Text.Trim(), typeof(string));
            //var1[34] = gb.CreateSPParameter("@byadm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[35] = gb.CreateSPParameter("@denda", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[36] = gb.CreateSPParameter("@ngkdenda", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[37] = gb.CreateSPParameter("@bytab", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[38] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbymaterai.Text.Trim(), typeof(string));
            //var1[39] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.VarChar, 250, txttotalbiaya.Text.Trim(), typeof(string));
            //var1[40] = gb.CreateSPParameter("@kd_area", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeArea.Value, typeof(string));
            //var1[41] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
            //var1[42] = gb.CreateSPParameter("@tab_drop", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[43] = gb.CreateSPParameter("@tab_angs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[44] = gb.CreateSPParameter("@sifat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsifat.Text, typeof(string));
            //var1[45] = gb.CreateSPParameter("@sek_ekon", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsektorekonomi.Text, typeof(string));
            //var1[46] = gb.CreateSPParameter("@kd_goldb", ParameterDirection.Input, SqlDbType.VarChar, 250, txtgolongandebitur.Text, typeof(string));
            //var1[47] = gb.CreateSPParameter("@nm_pnjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox13.Text, typeof(string));
            //var1[48] = gb.CreateSPParameter("@golpjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtgolonganpenjamin.Text, typeof(string));
            //var1[49] = gb.CreateSPParameter("@nl_jmn", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox7.Text, typeof(string));
            //var1[50] = gb.CreateSPParameter("@kd_fisik", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfisikjaminan.Text, typeof(string));
            //var1[51] = gb.CreateSPParameter("@ttdjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[52] = gb.CreateSPParameter("@ket_jmn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketeranganJaminan.Text, typeof(string));
            //var1[53] = gb.CreateSPParameter("@kd_ktr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); //
            //var1[54] = gb.CreateSPParameter("@ttlpokok", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text.Trim(), typeof(string));
            //var1[55] = gb.CreateSPParameter("@ttlbunga", ParameterDirection.Input, SqlDbType.VarChar, 250, ttlbunga, typeof(string));
            //var1[56] = gb.CreateSPParameter("@angspokok", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[57] = gb.CreateSPParameter("@angsbunga", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[58] = gb.CreateSPParameter("@bakidb", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text.Trim(), typeof(string));
            //var1[59] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 250, HDStatusDebitur.Value, typeof(string));
            //var1[60] = gb.CreateSPParameter("@droping", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); //
            //var1[61] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
            //var1[62] = gb.CreateSPParameter("@sklsppk", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); // 
            //var1[63] = gb.CreateSPParameter("@byrbunga", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string)); //
            //var1[64] = gb.CreateSPParameter("@picb", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
            //var1[65] = gb.CreateSPParameter("@gender", ParameterDirection.Input, SqlDbType.VarChar, 250, HDGender.Value, typeof(string)); 
            //var1[66] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsumberdana.Text, typeof(string));
            //var1[67] = gb.CreateSPParameter("@kcab", ParameterDirection.Input, SqlDbType.VarChar, 250, "000", typeof(string));
            //var1[68] = gb.CreateSPParameter("@channeling", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[69] = gb.CreateSPParameter("@nmlbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[70] = gb.CreateSPParameter("@nmlbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[71] = gb.CreateSPParameter("@oscbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[72] = gb.CreateSPParameter("@oscbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[73] = gb.CreateSPParameter("@prbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[74] = gb.CreateSPParameter("@prbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[75] = gb.CreateSPParameter("@kdbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[76] = gb.CreateSPParameter("@tgl_sale", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[77] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
            //var1[78] = gb.CreateSPParameter("@act_rra", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[79] = gb.CreateSPParameter("@act_rrp", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[80] = gb.CreateSPParameter("@jkc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[81] = gb.CreateSPParameter("@bgc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[82] = gb.CreateSPParameter("@jbgc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[83] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[84] = gb.CreateSPParameter("@no_acc", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[85] = gb.CreateSPParameter("@angspbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[86] = gb.CreateSPParameter("@angspbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[87] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtprogrampinjaman.Text, typeof(string));
            //var1[88] = gb.CreateSPParameter("@kol", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string));
            //var1[89] = gb.CreateSPParameter("@arrear", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[90] = gb.CreateSPParameter("@ttypokok", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[91] = gb.CreateSPParameter("@ttybunga", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[92] = gb.CreateSPParameter("@tgkhari", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[93] = gb.CreateSPParameter("@postgk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[94] = gb.CreateSPParameter("@pkbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[95] = gb.CreateSPParameter("@bgbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[96] = gb.CreateSPParameter("@cadangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[97] = gb.CreateSPParameter("@rekadm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[98] = gb.CreateSPParameter("@spv", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string));
            //var1[99] = gb.CreateSPParameter("@prkomisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[100] = gb.CreateSPParameter("@rekkomisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[101] = gb.CreateSPParameter("@actkomisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[102] = gb.CreateSPParameter("@actntrs", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[103] = gb.CreateSPParameter("@ntrs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[104] = gb.CreateSPParameter("@actasrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[105] = gb.CreateSPParameter("@asrn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbyasuransi.Text.Trim(), typeof(string)); //
            //var1[106] = gb.CreateSPParameter("@prjrbyr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[107] = gb.CreateSPParameter("@rekjrbyr", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[108] = gb.CreateSPParameter("@actjrbyr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); 
            //var1[109] = gb.CreateSPParameter("@nmkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNamaKeluarga.Value, typeof(string));
            //var1[110] = gb.CreateSPParameter("@stskel", ParameterDirection.Input, SqlDbType.VarChar, 50, HDHubunganKeluarga.Value, typeof(string));
            //var1[111] = gb.CreateSPParameter("@pkjkel", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[112] = gb.CreateSPParameter("@almkel", ParameterDirection.Input, SqlDbType.VarChar, 50, HDAlamatKeluarga.Value, typeof(string));
            //var1[113] = gb.CreateSPParameter("@klrkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKelurahanKeluarga.Value, typeof(string));
            //var1[114] = gb.CreateSPParameter("@keckel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKecamatanKeluarga.Value, typeof(string));
            //var1[115] = gb.CreateSPParameter("@kotkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKotaKeluarga.Value.Trim(), typeof(string));
            //var1[116] = gb.CreateSPParameter("@nmpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[117] = gb.CreateSPParameter("@almpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[118] = gb.CreateSPParameter("@klrpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[119] = gb.CreateSPParameter("@kecpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[120] = gb.CreateSPParameter("@kotpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[121] = gb.CreateSPParameter("@pkjpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[122] = gb.CreateSPParameter("@nmpjmn2", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox13.Text, typeof(string));
            //var1[123] = gb.CreateSPParameter("@stspjmn2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[124] = gb.CreateSPParameter("@pkjpjmn2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[125] = gb.CreateSPParameter("@tmp_lahir", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[126] = gb.CreateSPParameter("@tgl_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[127] = gb.CreateSPParameter("@tglproses", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[128] = gb.CreateSPParameter("@tglupd", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
            //var1[129] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //var1[130] = gb.CreateSPParameter("@status_pelunasan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[131] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjenispinjaman.Text, typeof(string));// 
            //var1[132] = gb.CreateSPParameter("@tgl_drop_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[133] = gb.CreateSPParameter("@tgl_due_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[134] = gb.CreateSPParameter("@kol_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[135] = gb.CreateSPParameter("@arrear_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[136] = gb.CreateSPParameter("@sandi_agunan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[137] = gb.CreateSPParameter("@dati2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[138] = gb.CreateSPParameter("@bagian_dijamin", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[139] = gb.CreateSPParameter("@no_rek_awal", ParameterDirection.Input, SqlDbType.VarChar, 250, HDno_rekening.Value, typeof(string));
            //var1[140] = gb.CreateSPParameter("@flag_proses_aba", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[141] = gb.CreateSPParameter("@noakad_ijarok", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[142] = gb.CreateSPParameter("@noakad_murabahah", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[143] = gb.CreateSPParameter("@noakad_qard", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[144] = gb.CreateSPParameter("@m_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[145] = gb.CreateSPParameter("@flag_cetak_anggota", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[146] = gb.CreateSPParameter("@report_dod", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[147] = gb.CreateSPParameter("@dod", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //var1[148] = gb.CreateSPParameter("@user_dod", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[149] = gb.CreateSPParameter("@flag_dod", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[150] = gb.CreateSPParameter("@no_fidusia", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[151] = gb.CreateSPParameter("@input_pembiayaan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[152] = gb.CreateSPParameter("@no_adendum", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[153] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[154] = gb.CreateSPParameter("@asuransi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[155] = gb.CreateSPParameter("@no_asuransi", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[156] = gb.CreateSPParameter("@asuransi_sebelum", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[157] = gb.CreateSPParameter("@no_asuransi_sebelum", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //var1[158] = gb.CreateSPParameter("@keterangan1", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[159] = gb.CreateSPParameter("@kode_travel", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[160] = gb.CreateSPParameter("@jns_blokir", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[161] = gb.CreateSPParameter("@rek_lain", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[162] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var1[163] = gb.CreateSPParameter("@kode_siskohat", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //var1[164] = gb.CreateSPParameter("@id_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtktppenjamin.Text, typeof(string));
            //var1[165] = gb.CreateSPParameter("@by_cadangan", ParameterDirection.Input, SqlDbType.VarChar, 50, TextBox3.Text, typeof(string));
            //var1[166] = gb.CreateSPParameter("@angsbbpr", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
            //var1[167] = gb.CreateSPParameter("@angsbbank", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string)); 
            //var1[168] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            //var1[169] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[170] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID , typeof(string));
            //var1[171] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh , typeof(string));

            ////save ke table ArMasterKredit
            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var1, "constrmf");


            //for (i = 0; i < int.Parse(txtjangka.Text.Trim()); i++)
            //{
            //    if (i == 0)
            //    {
            //        tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
            //        tgl_libur = tgl_realisasi.AddDays(7);
            //    }
            //    else
            //    {
            //        tgl_libur = tgldrop.AddDays(7);
            //    }

            //    DefaultClass.Global.SqlHelper[] var9 = new DefaultClass.Global.SqlHelper[3];
            //    var9[0] = gb.CreateSPParameter("@tglLibur", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tgl_libur, typeof(DateTime));
            //    var9[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            //    var9[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //    ge.ListDataset = gb.GetDataJustOutPut("GetTglLibur", var9, "constrmf");

            //    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            //    {
            //        tgldrop = tgl_libur;

            //    }
            //    else
            //    {
            //        tgldrop = tgl_libur.AddDays(7);

            //    }

            //    x = i + 1;

            //    pokok = gb.GetPokok(int.Parse(txtjangka.Text), double.Parse(txtplafon.Text));
            //    bunga = gb.GetNominalBunga(Double.Parse(txtbunga.Text.Trim().Replace(".", ",")), int.Parse(txtjangka.Text), double.Parse(txtplafon.Text), txtjeniskredit.Text);

            //    double pembulatanbunga = double.Parse(bunga);
            //    pembulatanbunga = Math.Round(pembulatanbunga);

            //    if(i == 0)
            //    {
            //        sisapk = int.Parse(txtplafon.Text);
            //    }
            //    else
            //    {
            //        sisapk = int.Parse(txtplafon.Text) - ((x - 1) * int.Parse(pokok));
            //    }


            //    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[55];
            //    var4[0] = gb.CreateSPParameter("@status_pembayaran", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //    var4[2] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //    var4[3] = gb.CreateSPParameter("@no_rekening_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
            //    var4[4] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjeniskredit.Text, typeof(string));
            //    var4[5] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 250, x, typeof(string));
            //    var4[6] = gb.CreateSPParameter("@tgl_jwl", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgldrop, typeof(string));
            //    var4[7] = gb.CreateSPParameter("@pokok", ParameterDirection.Input, SqlDbType.VarChar, 250, pokok, typeof(string));
            //    var4[8] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.VarChar, 250, bunga, typeof(string));
            //    var4[9] = gb.CreateSPParameter("@tgl_angs", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[10] = gb.CreateSPParameter("@pkangs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[11] = gb.CreateSPParameter("@bng_angs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[12] = gb.CreateSPParameter("@denda", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[13] = gb.CreateSPParameter("@tgl", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[14] = gb.CreateSPParameter("@tgl_prov", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[15] = gb.CreateSPParameter("@provisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[16] = gb.CreateSPParameter("@arrear", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[17] = gb.CreateSPParameter("@arrrate", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[18] = gb.CreateSPParameter("@trtpk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[19] = gb.CreateSPParameter("@trtbng", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[20] = gb.CreateSPParameter("@sisapk", ParameterDirection.Input, SqlDbType.VarChar, 250, sisapk, typeof(string));
            //    var4[21] = gb.CreateSPParameter("@sisabng", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[22] = gb.CreateSPParameter("@postgk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[23] = gb.CreateSPParameter("@pkbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[24] = gb.CreateSPParameter("@bgbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[25] = gb.CreateSPParameter("@pkbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[26] = gb.CreateSPParameter("@bngbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[27] = gb.CreateSPParameter("@pkbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[28] = gb.CreateSPParameter("@bngbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[29] = gb.CreateSPParameter("@angspbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[30] = gb.CreateSPParameter("@angsbbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[31] = gb.CreateSPParameter("@angspbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[32] = gb.CreateSPParameter("@angsbbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[33] = gb.CreateSPParameter("@pcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[34] = gb.CreateSPParameter("@bcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[35] = gb.CreateSPParameter("@tgl_cof", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[36] = gb.CreateSPParameter("@angspcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[37] = gb.CreateSPParameter("@angsbcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[38] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[39] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[40] = gb.CreateSPParameter("@rekadm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[41] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
            //    var4[42] = gb.CreateSPParameter("@tglupd", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgldrop, typeof(string));
            //    var4[43] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //    var4[44] = gb.CreateSPParameter("@tgl_jwl_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[45] = gb.CreateSPParameter("@tgl_angs_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[46] = gb.CreateSPParameter("@arrear_lama ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[47] = gb.CreateSPParameter("@status_channeling ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[48] = gb.CreateSPParameter("@status_pembayaran_channeling", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[49] = gb.CreateSPParameter("@tgl_angsuran_bank", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
            //    var4[50] = gb.CreateSPParameter("@no_tagihan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //    var4[51] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            //    var4[52] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //    var4[53] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //    var4[54] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //    ge.ListDataset = gb.GetDataJustOutPut("AR_KARTUPINJAMAN", var4, "constrmf");


            //}

            //DefaultClass.Global.SqlHelper[] var00 = new DefaultClass.Global.SqlHelper[8];
            //var00[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "6", typeof(string));
            //var00[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            //var00[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var00[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "6", typeof(string));
            //var00[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            //var00[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //var00[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVALSTATUS", typeof(string));
            //var00[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var00, "constrmf");

            //DefaultClass.Global.SqlHelper[] var99 = new DefaultClass.Global.SqlHelper[6];
            //var99[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "6", typeof(string));
            //var99[1] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //var99[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            //var99[3] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, HDno_rekening.Value, typeof(string));
            //var99[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEPERMOHONAN", typeof(string));
            //var99[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var99, "constrmf");




            ////UPDATE NO NASABAH COUNTER TO TABLE ARMst
            //DefaultClass.Global.SqlHelper[] varMst = new DefaultClass.Global.SqlHelper[6];
            //varMst[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            //varMst[1] = gb.CreateSPParameter("@JJualBln", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text, typeof(string));
            //varMst[2] = gb.CreateSPParameter("@JBayarThn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtplafon.Text, typeof(string));
            //varMst[3] = gb.CreateSPParameter("@SaldoJalan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text, typeof(string));
            //varMst[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEFORAPPROVAL", typeof(string));
            //varMst[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_ArMst", varMst, "constrmf");

            ////Select TypeTrn TO TABLE suTypeAr
            //DefaultClass.Global.SqlHelper[] varsutype = new DefaultClass.Global.SqlHelper[3];
            //varsutype[0] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 50, "C", typeof(string));
            //varsutype[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            //varsutype[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_SuTypeAr", varsutype, "constrmf");


            //typetrn  = ge.ListDataset.Tables[0].Rows[0]["TypeTrn"].ToString();
            //keterangantrn = ge.ListDataset.Tables[0].Rows[0]["KetType"].ToString();

            ////INSERT INTO ArTrn
            //DefaultClass.Global.SqlHelper[] vartrn = new DefaultClass.Global.SqlHelper[35];
            //vartrn[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[1] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 250, HDno_rekening.Value, typeof(string));
            //vartrn[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            //vartrn[3] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
            //vartrn[4] = gb.CreateSPParameter("@TglTrn ", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
            //vartrn[5] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
            ////vartrn[6] = gb.CreateSPParameter("@TglTrnFaktur ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[7] = gb.CreateSPParameter("@TglJtpFaktur ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[8] = gb.CreateSPParameter("@BankGiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[9] = gb.CreateSPParameter("@Nogiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[10] = gb.CreateSPParameter("@Statusgiro ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[6] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
            //vartrn[7] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Approval Pinjaman", typeof(string));
            //vartrn[8] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtplafon.Text, typeof(string));
            //vartrn[9] = gb.CreateSPParameter("@TglEntry ", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now.ToString("yyyy-MM-dd"), typeof(string));
            //vartrn[10] = gb.CreateSPParameter("@Kodeuser ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrn[11] = gb.CreateSPParameter("@Kodesumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, "MK", typeof(string));
            //vartrn[12] = gb.CreateSPParameter("@KodeJurnal ", ParameterDirection.Input, SqlDbType.VarChar, 250, "ARMK01", typeof(string));
            ////vartrn[18] = gb.CreateSPParameter("@DelIndex ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[13] = gb.CreateSPParameter("@TrfFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[14] = gb.CreateSPParameter("@BayarFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[15] = gb.CreateSPParameter("@HapusFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[16] = gb.CreateSPParameter("@TutupBukuFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[17] = gb.CreateSPParameter("@UangMukaFlag ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[18] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[19] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[20] = gb.CreateSPParameter("@KursAkhir", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[21] = gb.CreateSPParameter("@KursLama ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[22] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrn[23] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now.ToString("yyyy-MM-dd"), typeof(string));
            ////vartrn[30] = gb.CreateSPParameter("@DateModified ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[31] = gb.CreateSPParameter("@LastUserFK ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[24] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            //vartrn[25] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[26] = gb.CreateSPParameter("@Pk ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            //vartrn[27] = gb.CreateSPParameter("@OverDue", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[28] = gb.CreateSPParameter("@RencanaPembayaran", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeproduk.Value, typeof(string));
            ////vartrn[37] = gb.CreateSPParameter("@REMARK ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[38] = gb.CreateSPParameter("@TglUpdate", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[39] = gb.CreateSPParameter("@PKPort ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[40] = gb.CreateSPParameter("@@TypeTrnRegister ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[41] = gb.CreateSPParameter("@PKVessel ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[42] = gb.CreateSPParameter("@IdAgency ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[43] = gb.CreateSPParameter("@Timearrival", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[44] = gb.CreateSPParameter("@timedeparture", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[45] = gb.CreateSPParameter("@voyagenumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[46] = gb.CreateSPParameter("@Commodity", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[29] = gb.CreateSPParameter("@Signatory", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
            ////vartrn[48] = gb.CreateSPParameter("@RegisterNumber ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            ////vartrn[49] = gb.CreateSPParameter("@IdPrincipal", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
            //vartrn[30] = gb.CreateSPParameter("@RegisterName ", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
            //vartrn[31] = gb.CreateSPParameter("@TypeAR ", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string)); 
            //vartrn[32] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            //vartrn[33] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //vartrn[34] = gb.CreateSPParameter("@kodecabang ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));


            //ge.ListDataset = gb.GetDataJustOutPut("AR_TRN", vartrn, "constrmf");

            ////INSERT INTO gltrnsm
            //DefaultClass.Global.SqlHelper[] vartrnsm = new DefaultClass.Global.SqlHelper[17];
            //vartrnsm[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string)); 
            //vartrnsm[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string)); 
            //vartrnsm[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, "ARMK01", typeof(string)); 
            //vartrnsm[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, typetrn, typeof(string));
            //vartrnsm[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Approval Pinjaman", typeof(string));
            //vartrnsm[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 250, txtplafon.Text.Trim(), typeof(string)); //jmldebit
            //vartrnsm[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Money, 250, txtplafon.Text.Trim(), typeof(string)); //jmlkredit
            //vartrnsm[7] = gb.CreateSPParameter("@KodeSupplier", ParameterDirection.Input, SqlDbType.VarChar, 250, HDno_rekening.Value, typeof(string));
            //vartrnsm[8] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            //vartrnsm[9] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Approval Pinjaman dengan Plafon : " + txtplafon.Text, typeof(string)); 
            //vartrnsm[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
            //vartrnsm[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //vartrnsm[12] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrnsm[13] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            //vartrnsm[14] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            //vartrnsm[15] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
            //vartrnsm[16] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));



            //ge.ListDataset = gb.GetDataJustOutPut("AP_Transaksi", vartrnsm, "constrmf");

            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[9];
                    var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "8", typeof(string));
                    var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
                    var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "8", typeof(string));
                    var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                    var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVALSTATUS", typeof(string));
                    var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                     var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                     ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }

                 
 
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAPPROVALPINJAMAN", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
}