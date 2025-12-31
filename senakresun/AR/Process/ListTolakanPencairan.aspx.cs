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

public partial class AR_Process_ListTolakanPencairan : System.Web.UI.Page
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
        Response.Redirect("ListTolakanPencairan.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListTolakanPencairan.aspx");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = true;
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
        pnlLaba.Visible = true;
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
        pnlUsaha.Visible = true;
        pnlJaminan.Visible = true;
    }
    protected void btnNextTolakan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        pnlKeanggotaan.Visible = false;
        pnlUsaha.Visible = false;
        pnlAlasanTolak.Visible = true;
    }
    protected void BtnBackTolakan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        pnlKeanggotaan.Visible = false;
        pnlUsaha.Visible = true;
        pnlAlasanTolak.Visible = false;
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
        pnlLaba.Visible = true;
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
        pnlUsaha.Visible = false;
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
        pnlUsaha.Visible = false;
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
        pnlUsaha.Visible = true;
        pnlJaminan.Visible = false;
        //ProsesVerifikasi("0", SessLoginID);
    }

    protected void btnTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        pnlKeanggotaan.Visible = false;
        pnlUsaha.Visible = false;
        pnlJaminan.Visible = true;
    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BindGridEntity();
            lbllabakotor.Font.Bold = true;
            lbltotalbiaya.Font.Bold = true;
            lbllabasebelumpajak.Font.Bold = true;
            lbllababersih.Font.Bold = true;
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

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

                    //Panel Data nasabah 1
                    txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                    txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal2"].ToString();
                    txtkecamatan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal3"].ToString();
                    txtkota.Text = ge.ListDataset.Tables[0].Rows[0]["kota"].ToString();
                    txttempatlahir.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                    txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir"].ToString();
                    txtnoidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    txtmasaberakhirKTP.Text = ge.ListDataset.Tables[0].Rows[0]["berlaku_ktp"].ToString();
                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                    txtAgama.Text = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                    txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();

                    //panel Data Pekerjaan Nasabah
                    txtkdinstansi.Text = ge.ListDataset.Tables[0].Rows[0]["kd_instansi"].ToString();
                    txtPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pekerjaan"].ToString();
                    txtnamaPerusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_perusahaan"].ToString();
                    txtalamatperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan1"].ToString();
                    txtkelurahanperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan2"].ToString();
                    txtkecamatanperusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan3"].ToString();
                    txtkotapekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_provinsi"].ToString();
                    txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kode_pos"].ToString();
                    txtBidangUsaha.Text = ge.ListDataset.Tables[0].Rows[0]["bidang_usaha"].ToString();
                    txtteleponpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["telepon"].ToString();
                    txtFaxPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["fax"].ToString();
                    txtlamabekerja.Text = ge.ListDataset.Tables[0].Rows[0]["lama_usaha"].ToString();
                    txtkepemilikantempatusaha.Text = ge.ListDataset.Tables[0].Rows[0]["kepemilikan_tempat_usaha"].ToString();
                    txtalamatsurat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_surat"].ToString();


                    //panel Data Tambahan Nasabah
                    txtstatusdalamkeluarga.Text = ge.ListDataset.Tables[0].Rows[0]["status_dalam_keluarga"].ToString();
                    txtkondisisakit.Text = ge.ListDataset.Tables[0].Rows[0]["kondisi_kesehatan"].ToString();
                    txtsakit.Text = ge.ListDataset.Tables[0].Rows[0]["sakit_yang_pernah_diderita"].ToString();
                    txttahunsakit.Text = ge.ListDataset.Tables[0].Rows[0]["tahun_sakit_yang_diderita"].ToString();

                    //panel Data Pendapatan Nasabah
                    txtpendapatanbersih.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_bersih_usaha"].ToString();
                    txtpendapatanselainusaha.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_selain_usaha"].ToString();
                    txttotalpendapatan.Text = ge.ListDataset.Tables[0].Rows[0]["total_pendapatan"].ToString();
                    txtpengeluaranrumahtangga.Text = ge.ListDataset.Tables[0].Rows[0]["pengeluaran_rumah_tangga"].ToString();
                    TextBox2.Text = ge.ListDataset.Tables[0].Rows[0]["tagihan_pln_pam"].ToString();
                    TextBox4.Text = ge.ListDataset.Tables[0].Rows[0]["pinjaman_kewajiban_lain"].ToString();
                    txtangsuran.Text = ge.ListDataset.Tables[0].Rows[0]["rekomendasi_angsuran"].ToString();
                    TextBox6.Text = ge.ListDataset.Tables[0].Rows[0]["total_angsuran"].ToString();
                    TextBox7.Text = ge.ListDataset.Tables[0].Rows[0]["sisa_penghasilan"].ToString();

                    //panel Data Laba Nasabah
                    txtpendapatanusaha.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_usaha"].ToString();
                    txthargapokok.Text = ge.ListDataset.Tables[0].Rows[0]["harga_pokok"].ToString();
                    txtlabakotor.Text = ge.ListDataset.Tables[0].Rows[0]["laba_kotor"].ToString();
                    txtbiayapegawai.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_pegawai"].ToString();
                    txtbiayalain.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_lain_lain"].ToString();
                    txttotalbiaya.Text = ge.ListDataset.Tables[0].Rows[0]["total_biaya"].ToString();
                    txtlababersihsebelumpajak.Text = ge.ListDataset.Tables[0].Rows[0]["laba_bersih_sebelum_pajak"].ToString();
                    txtpajakpenghasilan.Text = ge.ListDataset.Tables[0].Rows[0]["pajak_penghasilan"].ToString();
                    txtlababersihsetelahpajak.Text = ge.ListDataset.Tables[0].Rows[0]["laba_bersih_setelah_pajak"].ToString();

                    //Panel Data pengajuan kredit nasabah
                    txtnoreklama.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtjeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                    txtjenispenggunaan.Text = ge.ListDataset.Tables[0].Rows[0]["jns_pgn"].ToString();
                    txttglrealisasi.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_realisasi"].ToString();
                    txtjenispinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString(); ;
                    txtprogrampinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["status_sk"].ToString();
                    txtsumberdana.Text = ge.ListDataset.Tables[0].Rows[0]["kdpersh1"].ToString();
                    txtplafon.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_bendahara"].ToString();
                    txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka"].ToString();
                    txtbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtProvisi.Text = ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString();
                    txtnominalprovisi.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString();
                    txtangsuranperbulan.Text = ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString();
                    txtadminPencairan.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_administrasi"].ToString();
                    txtadminangsuran.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_angsuran1"].ToString();
                    txtsifat.Text = ge.ListDataset.Tables[0].Rows[0]["sifat"].ToString();
                    txtAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txtNonAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txttablain.Text = "-";
                    txtsektorekonomi.Text = ge.ListDataset.Tables[0].Rows[0]["sektor_ekonomi"].ToString();
                    txtgolongandebitur.Text = ge.ListDataset.Tables[0].Rows[0]["golongan_debitur"].ToString();
                    txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                    TextBox13.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                    txtgolonganpenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["golongan_penjamin"].ToString();
                    txtpendapatanperbulan.Text = ge.ListDataset.Tables[0].Rows[0]["pendapatan_bulan"].ToString();
                    txtfisikjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["fisik_jaminan"].ToString();
                    txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    TextBox1.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan_tambahan"].ToString();
                    TextBox3.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString();
                    TextBox5.Text = ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi"].ToString();
                    txtjttempo.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_jt"].ToString();
                    txtangsurandimuka.Text = ge.ListDataset.Tables[0].Rows[0]["angsuran_dimuka"].ToString();
                    txtbymaterai.Text = ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString();
                    txtbyasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString();
                    txtbypelunasan.Text = ge.ListDataset.Tables[0].Rows[0]["biaya_pelunasan"].ToString();
                    txtbynotaris.Text = ge.ListDataset.Tables[0].Rows[0]["notaris"].ToString();
                    TextBox14.Text = ge.ListDataset.Tables[0].Rows[0]["blokir_angsuran"].ToString();
                    TextBox15.Text = ge.ListDataset.Tables[0].Rows[0]["nominal_blokir_angsuran"].ToString();
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

                    txtsimpok.Text = ge.ListDataset.Tables[0].Rows[0]["simpok"].ToString();
                    txtsimwa.Text = ge.ListDataset.Tables[0].Rows[0]["simwa"].ToString();


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
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)); 
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLISTTOLAKANPINJAMAN", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
         
        ge.ListDataset = gb.GetDataJustOutPut("AR_LISTTOLAKAN_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    } 
}