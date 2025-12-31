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

public partial class AR_Process_VerifikasiPinjaman : System.Web.UI.Page
{
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiPinjaman.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifikasiPinjaman.aspx");
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
    protected void btnSetuju_Click(object sender, EventArgs e)
    {
        ProsesVerifikasi("4", SessLoginID);
    }
    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        ProsesVerifikasi("3", SessLoginID);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
             
            BindGridEntity(); 
            btnSetuju.Enabled = true;
            btnSimpanTolakan.Enabled = true;


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
            //ddlgolongandebitur.Items.Insert(0, new ListItem("Pilih Data", "-"));

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
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[5] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
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

                    //txtsimpok.Text = ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString();
                    //txtsimwa.Text = ge.ListDataset.Tables[0].Rows[0]["wajib"].ToString();
                    //txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()));
                    //txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["wajib"].ToString()));

                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();



                   // imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break; 
        }
    }

    public void ProsesVerifikasi(string flag, string user)
    {

        try

        {
            btnSetuju.Enabled = false;
            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, flag, typeof(string));
            var[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, user, typeof(string));
            var[2] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var[3] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VERIFIKASISTATUS", typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[7] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

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

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "1", typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLISTVERIFIKASI", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
}