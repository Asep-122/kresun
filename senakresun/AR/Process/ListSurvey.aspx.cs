using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class AR_Process_ListSurvey : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string id = "", no_nasabah = "", flag = "", namafileJaminan = "", fotopathJaminan = "";

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
        Response.Redirect("ListSurvey.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListSurvey.aspx");
    }
    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        pnlIdentitas.Visible = false;
    }
    protected void btnNexIdentitas_Click(object sender, EventArgs e)
    {
        pnlPekerjaan.Visible = true;
        pnlIdentitas.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlIdentitas.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }
    protected void btnNextPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        pnlKeanggotaan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = false;

    }
    protected void btnBackPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = false;

    }
    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {

        pnlIdentitas.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


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
        pnlLaba.Visible = false;

        pnlPihakYgDapatDiHubungi.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected void btnNextLaba_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        pnlKeanggotaan.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }
    protected void btnNextPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = true;
        pnlJaminan.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected void btnbackawal_Click(object sender, EventArgs e)
    {
        pnlFilter.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
    }
    protected void btnBackPekerjaan_Click(object sender, EventArgs e)
    {
        pnlIdentitas.Visible = true;
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
    protected void btnBackLaba_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = true;
        pnlLaba.Visible = false;
    }
    protected void btnBackPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPihakYgDapatDiHubungi.Visible = true;
        pnlPengajuan.Visible = false;
        pnlKeanggotaan.Visible = false;
    }
    protected void btnBackUsaha_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        pnlKeanggotaan.Visible = true;
        //pnlUsaha.Visible = false;
        pnlJaminan.Visible = false;
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        pnlKeanggotaan.Visible = true;
        //pnlUsaha.Visible = false;
    }

    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = true;
        pnlJaminan.Visible = false;
        //ProsesVerifikasi("0", SessLoginID);
    }
     
    protected void btnSetuju_Click(object sender, EventArgs e)
    {

        try

        {
            if (txtpathjaminan.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan belum di upload');", true);

            }
            else
            {
                btnSetuju.Enabled = false;

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@jenis_survey", ParameterDirection.Input, SqlDbType.VarChar, 50, "1", typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATELISTSURVEY", typeof(string));
                var[3] = gb.CreateSPParameter("@filepathjaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathjaminan.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[5] = gb.CreateSPParameter("@jaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, TextBox8.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));

                
                ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var, "constrmf");

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            insertcombo();
            BindGridEntity();
            lbllabakotor.Font.Bold = true;
            lbltotalbiaya.Font.Bold = true;
            lbllabasebelumpajak.Font.Bold = true;
            lbllababersih.Font.Bold = true;
            btnSetuju.Enabled = true;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

          
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
                    var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");
                    //Panel Data nasabah 1
                    txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                    txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();
                    txtkecamatan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                    txtkota.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                    txttempatlahir.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                    txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir"].ToString();
                    txtnoidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    txtmasaberakhirKTP.Text = ge.ListDataset.Tables[0].Rows[0]["berlaku_ktp"].ToString();
                    //txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                    ddlAgama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama3"].ToString();
                    txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                    txtjumlahanak.Text = ge.ListDataset.Tables[0].Rows[0]["jumlah_anak"].ToString();
                    txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                    txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                    txtrt.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrw.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    //panel Data Pekerjaan Nasabah
                    ddlwarga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddlstatuskawinktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_dalam_keluarga"].ToString();
                    ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_pekerjaan"].ToString();
                    txtPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pekerjaan"].ToString();
                    txtnamaPerusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_perusahaan"].ToString();
                    txtalamatperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan1"].ToString();
                    //txtprovinsiKTP.Text = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                    txtkodeposKTP.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                    //txtkelurahanperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan2"].ToString();
                    //txtkecamatanperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan3"].ToString();
                    //txtkotapekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_provinsi"].ToString();
                    ddljeniskelamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kode_pos"].ToString();
                    ddlpendidikan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    ddlalamatsurat.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["alamat_surat"].ToString();
                    txtkelurahanperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanpt"].ToString();
                    txtkecamatanperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanpt"].ToString();
                    txtkotapekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenpt"].ToString();
                    //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                    imagektppasangan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    TextBox4.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    TextBox6.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    txtkodepos.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                    imagekk.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    imageNPWP.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    //txtpathKK.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();
                    ddlstatuskawin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_dalam_keluarga"].ToString();
                    //ddlstskawin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_dalam_keluarga"].ToString(); 
                    txtnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                    txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                    ddlpendidikan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    TextBox12.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    ddlprovinsiKTP.Text = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                    ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_pekerjaan"].ToString();
                    ddlprovinsiperusahaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsipt"].ToString();
                    //txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kode_pospt"].ToString();
                    if (ddlstatuskawin.SelectedValue == "1")
                    {
                        txtidpasangan.Enabled = false;
                        txtidpasangan.Text = "-";
                        txtnamapasangan.Enabled = false;
                        txtnamapasangan.Text = "-";
                        txttempatlahirpasangan.Enabled = false;
                        txttempatlahirpasangan.Text = "-";
                        txttgllahirpasangan.Enabled = false;
                        //txtppathktppasangan.Text = "-";
                        //txtpathKK.Text = "-";
                        //txtpathnpwp.Text = "-";
                        txttgllahirpasangan.Text = "1900-01-01 00:00:00";  
                    }
                    else
                    {

                        txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                        txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                        txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                        txttgllahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

                    }
                    txtBidangUsaha.Text = ge.ListDataset.Tables[0].Rows[0]["bidang_usaha"].ToString();
                    txtteleponpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["telepon"].ToString();
                    txtFaxPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["fax"].ToString();
                    txtlamabekerja.Text = ge.ListDataset.Tables[0].Rows[0]["lama_usaha"].ToString();
                    txtkepemilikantempatusaha.Text = ge.ListDataset.Tables[0].Rows[0]["tempatusaha"].ToString();
                    //.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_surat"].ToString();

                    ddlgolongandebitur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan_debitur"].ToString();
                    ddlgolonganpenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan_penjamin"].ToString();
                    ddlfisikjaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["fisik_jaminan"].ToString();

                    ddlprovinsi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();


                    //panel Data Tambahan Nasabah
                    //txtkondisisakit.Text = ge.ListDataset.Tables[0].Rows[0]["kondisikesehatan"].ToString();
                    //txtsakit.Text = ge.ListDataset.Tables[0].Rows[0]["sakit_yang_pernah_diderita"].ToString();
                    //txttahunsakit.Text = ge.ListDataset.Tables[0].Rows[0]["tahun_sakit_yang_diderita"].ToString();

                    //panel Data Pendapatan Nasabah
                    txtpendapatanbersih.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_bersih_usaha1"].ToString();
                    txtpendapatanselainusaha.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_selain_usaha1"].ToString();
                    txttotalpendapatan.Text = ge.ListDataset.Tables[0].Rows[0]["total_pendapatan1"].ToString();
                    txtpengeluaranrumahtangga.Text = ge.ListDataset.Tables[0].Rows[0]["pengeluaran_rumah_tangga1"].ToString();
                    TextBox2.Text = ge.ListDataset.Tables[0].Rows[0]["tagihan_pln_pam1"].ToString();
                    TextBox4.Text = ge.ListDataset.Tables[0].Rows[0]["pinjaman_kewajiban_lain1"].ToString();
                    txtangsuran.Text = ge.ListDataset.Tables[0].Rows[0]["rekomendasi_angsuran1"].ToString();
                    TextBox6.Text = ge.ListDataset.Tables[0].Rows[0]["total_angsuran1"].ToString();
                    TextBox7.Text = ge.ListDataset.Tables[0].Rows[0]["sisa_penghasilan1"].ToString();

                    //panel Data Laba Nasabah
                    txtpendapatanusaha.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_usaha1"].ToString();
                    txthargapokok.Text = ge.ListDataset.Tables[0].Rows[0]["harga_pokok1"].ToString();
                    txtlabakotor.Text = ge.ListDataset.Tables[0].Rows[0]["laba_kotor1"].ToString();
                    txtbiayapegawai.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_pegawai1"].ToString();
                    txtbiayalain.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_lain_lain1"].ToString();
                    txttotalbiaya.Text = ge.ListDataset.Tables[0].Rows[0]["total_biaya1"].ToString();
                    txtlababersihsebelumpajak.Text = ge.ListDataset.Tables[0].Rows[0]["laba_bersih_sebelum_pajak1"].ToString();
                    txtpajakpenghasilan.Text = ge.ListDataset.Tables[0].Rows[0]["pajak_penghasilan1"].ToString();
                    txtlababersihsetelahpajak.Text = ge.ListDataset.Tables[0].Rows[0]["laba_bersih_setelah_pajak1"].ToString();

                    //Panel Data pengajuan kredit nasabah
                    txtnoreklama.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtjeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                    //txtjenispenggunaan.Text = ge.ListDataset.Tables[0].Rows[0]["penggunaan"].ToString();
                    txttglrealisasi.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_realisasi"].ToString();
                    txtjenispinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString(); ;
                    txtprogrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["ketprogram"].ToString();
                    txtsumberdana.Text = ge.ListDataset.Tables[0].Rows[0]["kdpersh1"].ToString();
                    txtplafon.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_bendahara1"].ToString();
                    txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka"].ToString();
                    txtbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtProvisi.Text = ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString();
                    txtnominalprovisi.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_provisi1"].ToString();
                    txtangsuranperbulan.Text = ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan1"].ToString();
                    txtadminPencairan.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_administrasi"].ToString();
                    txtadminangsuran.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_angsuran1"].ToString();
                    txtsifat.Text = ge.ListDataset.Tables[0].Rows[0]["sifat"].ToString();
                    txtAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txtNonAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txttablain.Text = "-";
                    txtsektorekonomi.Text = ge.ListDataset.Tables[0].Rows[0]["sektor_ekonomi"].ToString();
                    txtharitagih.Text = ge.ListDataset.Tables[0].Rows[0]["hari_tagih"].ToString(); 


                    txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                    TextBox13.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                     
                    txtpendapatanperbulan.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_bulan1"].ToString(); 
                    //txtJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    txtjaminan_tambahan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan_tambahan"].ToString();
                    TextBox3.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan1"].ToString();
                    TextBox5.Text = ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi1"].ToString();
                    txtjttempo.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_jt"].ToString();
                    txtangsurandimuka.Text = ge.ListDataset.Tables[0].Rows[0]["angsuran_dimuka1"].ToString();
                    txtbymaterai.Text = ge.ListDataset.Tables[0].Rows[0]["meterai1"].ToString();
                    txtbyasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["asuransi1"].ToString();
                    txtbypelunasan.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_pelunasan1"].ToString();
                    txtbynotaris.Text = ge.ListDataset.Tables[0].Rows[0]["notaris1"].ToString();
                    TextBox14.Text = ge.ListDataset.Tables[0].Rows[0]["blokir_angsuran1"].ToString();
                    TextBox15.Text = ge.ListDataset.Tables[0].Rows[0]["nominal_blokir_angsuran1"].ToString();
                    txtjenisblokir.Text = ge.ListDataset.Tables[0].Rows[0]["jenis_blokir"].ToString();

                    //panel Data Kondisi Usaha Nasabah
                    txtkondisiusaha.Text = ge.ListDataset.Tables[0].Rows[0]["kondisi_usaha"].ToString();
                    txtKarakterAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["karakter_anggota"].ToString();
                    txtinfo.Text = ge.ListDataset.Tables[0].Rows[0]["info_anggota_dari_pihak3"].ToString();
                    txtkondisipesaing.Text = ge.ListDataset.Tables[0].Rows[0]["kondisi_pesaing"].ToString();
                    txtresikousaha.Text = ge.ListDataset.Tables[0].Rows[0]["resiko_usaha"].ToString();
                    txtatasiresiko.Text = ge.ListDataset.Tables[0].Rows[0]["upaya_mengatasi_resiko_usaha"].ToString();

                    HDKodePos.Value = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                    HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    HDLamaKerja.Value = ge.ListDataset.Tables[0].Rows[0]["lama_kerja"].ToString();

                    //txtsimpok.Text = ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString();
                    //txtsimwa.Text = ge.ListDataset.Tables[0].Rows[0]["wajib"].ToString();
                    txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()));
                    txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["wajib"].ToString()));

                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
        }
    }


    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@flag_proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "1", typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBELUMSURVEY", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void UploadFileJaminan(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/Jaminan/");
        DateTime now = DateTime.Now;

        namafileJaminan = txtnoanggota.Text + "_" + now.ToString("ddMMyyyy") + "_" + "Jaminan";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadnpwp.FileName) != namafileNPWP)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileNPWP + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        fileuploadjaminan.SaveAs(folderPath + namafileJaminan + ".jpg");

        ////Display the Picture in Image control.
        //imagenpwp.ImageUrl = "~/Files/NPWP/" + Path.GetFileName(fileuploadnpwp.FileName);

        fotopathJaminan = folderPath + namafileJaminan;
        txtpathjaminan.Text = "~/Files/Jaminan/" + namafileJaminan + ".jpg";


        //Display the Picture in Image control.
        imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
        //}
    }
    public void insertcombo()
    {
        ge.ListDataset = gb.GetDataFunction("dbo.GetAgama()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlAgama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAgama.DataTextField = "Keterangan";
        ddlAgama.DataValueField = "id";
        ddlAgama.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusRumah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatusrumah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatusrumah.DataTextField = "keterangan";
        ddlstatusrumah.DataValueField = "id";
        ddlstatusrumah.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatuskawin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatuskawin.DataTextField = "Keterangan";
        ddlstatuskawin.DataValueField = "id";
        ddlstatuskawin.DataBind();


        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlstskawin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlstskawin.DataTextField = "Keterangan";
        //ddlstskawin.DataValueField = "id";
        //ddlstskawin.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsi.DataTextField = "nama_provinsi";
        ddlprovinsi.DataValueField = "kode_provinsi";
        ddlprovinsi.DataBind();


        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsiKTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiKTP.DataTextField = "nama_provinsi";
        ddlprovinsiKTP.DataValueField = "kode_provinsi";
        ddlprovinsiKTP.DataBind();


        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpekerjaanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpekerjaanktp.DataTextField = "keterangan";
        ddlpekerjaanktp.DataValueField = "id";
        ddlpekerjaanktp.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetGolonganPenjamin()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penjamin asc";

        ddlgolonganpenjamin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgolonganpenjamin.DataTextField = "keterangan";
        ddlgolonganpenjamin.DataValueField = "kode_penjamin";
        ddlgolonganpenjamin.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetGolonganDebitur()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_gol_debitur asc";

        ddlgolongandebitur.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgolongandebitur.DataTextField = "keterangan";
        ddlgolongandebitur.DataValueField = "kode_gol_debitur";
        ddlgolongandebitur.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetFisikJaminan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kd_fisik asc";

        ddlfisikjaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlfisikjaminan.DataTextField = "ket_fisik";
        ddlfisikjaminan.DataValueField = "kd_fisik";
        ddlfisikjaminan.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpendidikan.DataTextField = "keterangan";
        ddlpendidikan.DataValueField = "id";
        ddlpendidikan.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPekerjaan.DataTextField = "keterangan";
        ddlPekerjaan.DataValueField = "id";
        ddlPekerjaan.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatuskawin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatuskawin.DataTextField = "Keterangan";
        ddlstatuskawin.DataValueField = "id";
        ddlstatuskawin.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetCitizen()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlwarga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlwarga.DataTextField = "keterangan";
        ddlwarga.DataValueField = "id";
        ddlwarga.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetJenisKelamin()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddljeniskelamin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljeniskelamin.DataTextField = "Keterangan";
        ddljeniskelamin.DataValueField = "id";
        ddljeniskelamin.DataBind();

        ge.ListDataset = gb.GetDataFunction("dbo.GetAlamatSurat()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlalamatsurat.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlalamatsurat.DataTextField = "keterangan";
        ddlalamatsurat.DataValueField = "id";
        ddlalamatsurat.DataBind();
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatuskawinktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatuskawinktp.DataTextField = "Keterangan";
        ddlstatuskawinktp.DataValueField = "id";
        ddlstatuskawinktp.DataBind();


        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsiperusahaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiperusahaan.DataTextField = "nama_provinsi";
        ddlprovinsiperusahaan.DataValueField = "kode_provinsi";
        ddlprovinsiperusahaan.DataBind();
        
    }
}