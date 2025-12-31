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
using System.Text.RegularExpressions;

public partial class FA_Master_RegistrasiNasabah : System.Web.UI.Page
{
    String no_nasabah = "", nosebelumnya = "", updateno = "", CRNonasabah = "", STATE = "", kdpersh = "",
           formatnumber = "", msg1 = "", msg2 = "", msg3 = "", msg4 = "", msg5 = "", msg6 = "", msg7 = "", namafileKTP = "", namafileKK = "",
           namafileNPWP = "", namafileLainLain1 = "", namafileLainLain2 = "", namafilektppasangan = "";
    int counter_nonasabah = 0;
    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";
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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnBack, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void txtidentitas_TextChanged(object sender, EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //var[0] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
        //var[1] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaibukandung.Text, typeof(string));
        //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTP", typeof(string));
        //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
        //ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");

        //if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah sudah terdaftar');", true);

        //}

    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlIdentitas.Visible = true;
    }
    protected void btnNexIdentitas_Click(object sender, EventArgs e)
    {
        pnlIdentitas.Visible = false;
        pnlPekerjaan.Visible = true;

    }

    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {
        pnlPekerjaan.Visible = false;
        PnlPendapatan.Visible = true;
    }

    protected void btnNextPendapatan_Click(object sender, EventArgs e)
    {
        PnlPendapatan.Visible = false;
        pnlLainnya.Visible = true;
    }

    protected void btnNextLain_Click(object sender, EventArgs e)
    {
      
        //if(ddlAO.SelectedIndex == 0)
        //{

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('AO belum di pilih');", true);
        //}
        //else
        //{
            pnlLainnya.Visible = false;
            pnlPihakYgDapatDiHubungi.Visible = true;

        ////}
    }
    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        pnlIdentitas.Visible = false;
    }

    protected void btnBackPekerjaan_Click(object sender, EventArgs e)
    {
        pnlIdentitas.Visible = true;
        pnlPekerjaan.Visible = false;
    }
    protected void btnBackPendapatan_Click(object sender, EventArgs e)
    {
        pnlPekerjaan.Visible = true;
        PnlPendapatan.Visible = false;
    }
    protected void btnBackLain_Click(object sender, EventArgs e)
    {
        PnlPendapatan.Visible = true;
        pnlLainnya.Visible = false;
    }
    protected void btnBackPihakLain_Click(object sender, EventArgs e)
    {
        pnlLainnya.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RegistrasiNasabah.aspx");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistrasiNasabah.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                txtalamatPihakLain.Attributes.Add("maxlength", txtalamatPihakLain.MaxLength.ToString());
                BindGridEntity();

                InsertCombo();
                //txttgllahirasuransi.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
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
    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AR_GetNasabah", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":
                HDState.Value = e.CommandName;

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;
                    pnlFilter.Visible = false;
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");
 
                    HDNoNasabah.Value = no_nasabah;
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtnamapanggilan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_panggilan"].ToString();
                    ddljenkel.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    txttempatlahir.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                    txttgllahir.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir"].ToString();
                    //txttgllahirasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_asuransi"].ToString();
                    ddlAgama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                    ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                    ddlpendidikan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    ddlkewarganegaraan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    txtkewarganegaraan.Text = ge.ListDataset.Tables[0].Rows[0]["kenegaraan_asing"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["jenis_identitas"].ToString();
                    txtidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    txtkotaidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["kota_terbit_identitas"].ToString();
                    txttglterbitidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_terbit_identitas"].ToString();
                    txtberlakuidentitas.Text = ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dgn"].ToString();
                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["npwp"].ToString();
                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                    //txtemails.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    txthp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                    txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                    txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                    txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                    txttgllahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_lahir_pasangan"].ToString();
                    ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_rumah"].ToString();
                    //txttglmulaimenempati.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_mulai_menempati"].ToString();
                    //ddlsandikota.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sandi_kota"].ToString();
                    txtalamatsesuaiktp1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
                    //txtalamatsesuaiktp2.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp2"].ToString();
                    //txtalamatsesuaiktp3.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp3"].ToString();
                    txtkabupaten.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                    //ddlkdarea.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                    ddlprovinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                    txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                    //txtalamatrumahtinggal2.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal2"].ToString();
                    //txtalamatrumahtinggal3.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal3"].ToString();
                    txtrt.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrw.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    ddlprovinsi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                    txtkodepos.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    txtpathktp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();
                    txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                    txtpathKK.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();
                    txtpathlainlain1.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathlainlain1"].ToString();
                    txtlainlain1.Text = ge.ListDataset.Tables[0].Rows[0]["lainlain1"].ToString();
                    txtlainlain2.Text = ge.ListDataset.Tables[0].Rows[0]["lainlain2"].ToString();
                    txtpathlainlain2.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathlainlain2"].ToString();
                    txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();
                    txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                    //txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_pasangan"].ToString();
                    txtjumlahanak.Text = ge.ListDataset.Tables[0].Rows[0]["jumlah_anak"].ToString();


                    ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_pekerjaan"].ToString();
                    txtPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pekerjaan"].ToString();
                    txtjabatan.Text = ge.ListDataset.Tables[0].Rows[0]["jabatan"].ToString();
                    txtNamaPerusahaan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_perusahaan"].ToString();
                    txtBidangUsaha.Text = ge.ListDataset.Tables[0].Rows[0]["bidang_usaha"].ToString();
                    ddlLamaBekerja.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["lama_bekerja"].ToString();
                    ddlkepemilikantempatusaha.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kepemilikan_tempat_usaha"].ToString();
                    txtkepemilikantempatusaha.Text = ge.ListDataset.Tables[0].Rows[0]["tempatusahalain"].ToString();
                    ddlAlamatSurat.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["alamat_surat"].ToString();
                    //txtNIP.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString();
                    //txtnopensiun.Text = ge.ListDataset.Tables[0].Rows[0]["no_pensiun"].ToString();
                    txtalamatperusahaan1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan1"].ToString();
                    //txtalamatperusahaan2.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan2"].ToString();
                    //txtalamatperusahaan3.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan3"].ToString();
                    txtkabupatenpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenpt"].ToString();
                    txtkelurahanpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanpt"].ToString();
                    txtkecamatanpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanpt"].ToString();
                    ddlprovinsipekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsipt"].ToString();
                    txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kode_pospt"].ToString();
                    txtteleponpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["Telepon"].ToString();
                    txtFaxPekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["fax"].ToString();


                    ddlgajikotor.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["gaji_kotor"].ToString();
                    ddlsumberpenghasilan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sumber_penghasilan"].ToString();
                    ddltransaksiperhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["transaksi_perhari"].ToString();
                    txtsumberpenghasilan.Text = ge.ListDataset.Tables[0].Rows[0]["sumber_penghasilan_lain"].ToString();

                 
                    ddlTujuanbukaRekening.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tujuan_pembukaan_rekening"].ToString();
                    txttujuanbukarekening.Text = ge.ListDataset.Tables[0].Rows[0]["tujuan_pembukaan_rekening_lain"].ToString();
                    ddldalamhalbertindak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["dalam_hal_bertindak"].ToString();
                    txtdalamhalbertindak.Text = ge.ListDataset.Tables[0].Rows[0]["dalam_hal_bertindak_lain"].ToString();
                    ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                    txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                    txtkodepetugas.Text = ge.ListDataset.Tables[0].Rows[0]["inputed_by"].ToString();
                    ddlKenaPajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kena_pajak"].ToString();

            

                    txtnamapihaklain.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pihaklain"].ToString();
                    ddlHubunganPihakLain.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["hubungan"].ToString();
                    txtalamatPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat"].ToString();
                    txtRTPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["RtLainlain"].ToString();
                    txtRWPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["rwlainlain"].ToString();
                    txtKelurahanPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kelurahanlainlain"].ToString();
                    txtKecamatanPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kecamatanlainlain"].ToString();
                    txtKotaPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kota"].ToString();
                    ddlProvinsiPihakLain.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Provinsilainlain"].ToString();
                    txtKodePosPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["kodeposlainlain"].ToString();
                    txtTlpPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Tlp"].ToString();


                    HDNoNasabah.Value = "";
                    HDNoNasabah.Value = no_nasabah;
                    txtkodepetugas.Text = SessLoginID;

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }


                break;

        }
    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        pnlFilter.Visible=false;


        //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        //var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERNASABAH", typeof(string));
        //var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", var1, "constrmf");

        //nosekarang = long.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
        //formatnumber = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
        //nosebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();

        //nonasabah = nosekarang + 1;

        //CRNonasabah = SessBranchID + String.Format("{0:" + formatnumber + "}", nonasabah);

        //updateno = String.Format("{0:" + formatnumber + "}", nonasabah);

        //HDNoNasabah.Value = CRNonasabah;
        //HDNoSebelumnya.Value = nosebelumnya;

        txtkodepetugas.Text = SessLoginID;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        try
        {
            if (txtnamapihaklain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama pihak lain tidak boleh kosong');", true);
                txtnamapihaklain.Focus();
            }
            else if (txtalamatPihakLain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Alamat pihak lain tidak boleh kosong');", true);
                txtalamatPihakLain.Focus();
            }
            else if (txtRTPihakLain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('RT tidak boleh kosong');", true);
                txtRTPihakLain.Focus();
            }
            else if (txtRWPihakLain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('RW tidak boleh kosong');", true);
                txtRWPihakLain.Focus();
            }
            else if (txtKelurahanPihakLain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kelurahan tidak boleh kosong');", true);
                txtKelurahanPihakLain.Focus();
            }
            else if (txtKecamatanPihakLain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kecamatan tidak boleh kosong');", true);
                txtKecamatanPihakLain.Focus();
            }
            else if (txtKotaPihakLain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kota tidak boleh kosong');", true);
                txtKotaPihakLain.Focus();
            }

            else if (txtTlpPihakLain.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tlp pihak lain tidak boleh kosong');", true);
                txtTlpPihakLain.Focus();
            }
            else if (txtkotaidentitas.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kota Terbit Identitas tidak boleh kosong');", true);
                txtTlpPihakLain.Focus();
            }
            else if (txttglterbitidentitas.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Terbit tidak boleh kosong');", true);
                txtTlpPihakLain.Focus();
            }


            else
            {
                string nikditemukan = "-";
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[5];
                var4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitas.Text, typeof(string));
                var4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro", var4, "constrmf");



                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('NIK sudah ada pada koperasi yg sama');", true);


                }

                else
                { 







                if (HDState.Value == "SET")
                {
                    STATE = "UPDATE";
                }
                else
                {
                    STATE = "INSERT";
                    HDNoNasabah.Value = "-";
                }

                if (txttgllahirpasangan.Text == "")
                {
                    txttgllahirpasangan.Text = "1900-01-01";
                }
                //if(txttgllahirasuransi.Text == "")
                //{
                //    txttgllahirasuransi.Text = "1900-01-01"; 
                //}

                //INSERT INTO ARNASABAH
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[64];

                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                var[1] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@nama_panggilan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapanggilan.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@jenis_kelamin", ParameterDirection.Input, SqlDbType.VarChar, 250, ddljenkel.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@tempat_lahir", ParameterDirection.Input, SqlDbType.VarChar, 250, txttempatlahir.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@tanggal_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttgllahir.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@tanggal_asuransi", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01", typeof(string));
                var[7] = gb.CreateSPParameter("@agama", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlAgama.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@status_pernikahan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlstatusnikah.SelectedValue, typeof(string));
                var[9] = gb.CreateSPParameter("@pendidikan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlpendidikan.SelectedValue, typeof(string));
                var[10] = gb.CreateSPParameter("@kewarganegaraan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlkewarganegaraan.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@kenegaraan_asing", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkewarganegaraan.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@jenis_identitas", ParameterDirection.Input, SqlDbType.VarChar, 250, txtidentitas.Text, typeof(string));
                var[13] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 250, txtidentitas.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@kota_terbit_identitas", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkotaidentitas.Text, typeof(string));
                var[15] = gb.CreateSPParameter("@tanggal_terbit_identitas", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglterbitidentitas.Text, typeof(string));
                var[16] = gb.CreateSPParameter("@berlaku_sampai_dengan", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglterbitidentitas.Text, typeof(string));
                var[17] = gb.CreateSPParameter("@npwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNPWP.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNPWP.Text, typeof(string));
                var[19] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                var[20] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 250, txthp.Text, typeof(string));
                var[21] = gb.CreateSPParameter("@no_ktp_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtidpasangan.Text, typeof(string));
                var[22] = gb.CreateSPParameter("@nama_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapasangan.Text, typeof(string));
                var[23] = gb.CreateSPParameter("@tempat_lahir_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txttempatlahirpasangan.Text, typeof(string));
                var[24] = gb.CreateSPParameter("@tanggal_lahir_pasangan", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttgllahirpasangan.Text, typeof(string));
                var[25] = gb.CreateSPParameter("@status_rumah", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlstatusrumah.SelectedValue, typeof(string));
                var[26] = gb.CreateSPParameter("@tanggal_mulai_menempati", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01", typeof(string));
                var[27] = gb.CreateSPParameter("@sandi_kota", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                var[28] = gb.CreateSPParameter("@alamat_rumah_ktp1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatsesuaiktp1.Text, typeof(string));
                var[29] = gb.CreateSPParameter("@alamat_rumah_ktp2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                var[30] = gb.CreateSPParameter("@alamat_rumah_ktp3", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                var[31] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkabupaten.Text, typeof(string));
                var[32] = gb.CreateSPParameter("@kode_area", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                var[33] = gb.CreateSPParameter("@RT_ktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrtktp.Text, typeof(string));
                var[34] = gb.CreateSPParameter("@RW_ktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrwktp.Text, typeof(string));
                var[35] = gb.CreateSPParameter("@kelurahan_ktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahanktp.Text, typeof(string));
                var[36] = gb.CreateSPParameter("@kecamatan_ktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatanktp.Text, typeof(string));
                var[37] = gb.CreateSPParameter("@provinsi_ktp", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlprovinsiktp.SelectedValue, typeof(string));
                var[38] = gb.CreateSPParameter("@kodepos_ktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodeposktp.Text, typeof(string));
                var[39] = gb.CreateSPParameter("@alamat_rumah_tinggal1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatrumahtinggal1.Text, typeof(string));
                var[40] = gb.CreateSPParameter("@alamat_rumah_tinggal2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                var[41] = gb.CreateSPParameter("@alamat_rumah_tinggal3", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                var[42] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrt.Text, typeof(string));
                var[43] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrw.Text, typeof(string));
                var[44] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahan.Text, typeof(string));
                var[45] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatan.Text, typeof(string));
                var[46] = gb.CreateSPParameter("@provinsi", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlprovinsi.SelectedValue, typeof(string));
                var[47] = gb.CreateSPParameter("@kodepos", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodepos.Text, typeof(string));
                var[48] = gb.CreateSPParameter("@file_pathktppasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtppathktppasangan.Text, typeof(string));
                var[49] = gb.CreateSPParameter("@no_kk", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnomerkk.Text, typeof(string));
                var[50] = gb.CreateSPParameter("@file_pathKK", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathKK.Text, typeof(string));
                var[51] = gb.CreateSPParameter("@lainlain1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtlainlain1.Text, typeof(string));
                var[52] = gb.CreateSPParameter("@file_pathlainlain1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathlainlain1.Text, typeof(string));
                var[53] = gb.CreateSPParameter("@lainlain2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtlainlain2.Text, typeof(string));
                var[54] = gb.CreateSPParameter("@file_pathlainlain2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathlainlain2.Text, typeof(string));
                var[55] = gb.CreateSPParameter("@file_pathktp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathktp.Text, typeof(string));
                var[56] = gb.CreateSPParameter("@file_pathnpwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathnpwp.Text, typeof(string));
                var[57] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[58] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[59] = gb.CreateSPParameter("@jumlah_anak", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjumlahanak.Text, typeof(string));
                var[60] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                var[61] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                var[62] = gb.CreateSPParameter("@Userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                var[63] = gb.CreateSPParameter("@kode_AO", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlAO.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");

                msg1 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();

                ////INSERT TO TABLE ARMST

                //DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
                //var3[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                //var3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, kdpersh, typeof(string));
                //var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                //var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("AR_MST", var3, "constrmf");

                //msg2 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();

                //INSERT INTO ARNASABAHPEKERJAAN
                DefaultClass.Global.SqlHelper[] varpekerjaan = new DefaultClass.Global.SqlHelper[26];
                varpekerjaan[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                varpekerjaan[1] = gb.CreateSPParameter("@nama_pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtPekerjaan.Text, typeof(string));
                varpekerjaan[2] = gb.CreateSPParameter("@jabatan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjabatan.Text, typeof(string));
                varpekerjaan[3] = gb.CreateSPParameter("@nama_perusahaan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNamaPerusahaan.Text, typeof(string));
                varpekerjaan[4] = gb.CreateSPParameter("@bidang_usaha", ParameterDirection.Input, SqlDbType.VarChar, 250, txtBidangUsaha.Text, typeof(string));
                varpekerjaan[5] = gb.CreateSPParameter("@lama_bekerja", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlLamaBekerja.SelectedValue, typeof(string));
                varpekerjaan[6] = gb.CreateSPParameter("@kepemilikan_tempat_usaha", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlkepemilikantempatusaha.SelectedValue, typeof(string));
                varpekerjaan[7] = gb.CreateSPParameter("@alamat_surat", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlAlamatSurat.SelectedValue, typeof(string));
                varpekerjaan[8] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                varpekerjaan[9] = gb.CreateSPParameter("@no_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                varpekerjaan[10] = gb.CreateSPParameter("@alamat_perusahaan1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatperusahaan1.Text, typeof(string));
                varpekerjaan[11] = gb.CreateSPParameter("@alamat_perusahaan2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                varpekerjaan[12] = gb.CreateSPParameter("@alamat_perusahaan3", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                varpekerjaan[13] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkabupatenpekerjaan.Text, typeof(string));
                varpekerjaan[14] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahanpekerjaan.Text, typeof(string));
                varpekerjaan[15] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatanpekerjaan.Text, typeof(string));
                varpekerjaan[16] = gb.CreateSPParameter("@provinsi", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlprovinsipekerjaan.SelectedValue, typeof(string));
                varpekerjaan[17] = gb.CreateSPParameter("@kode_pos", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodepospekerjaan.Text, typeof(string));
                varpekerjaan[18] = gb.CreateSPParameter("@fax", ParameterDirection.Input, SqlDbType.VarChar, 250, txtFaxPekerjaan.Text, typeof(string));
                varpekerjaan[19] = gb.CreateSPParameter("@kode_pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlPekerjaan.SelectedValue, typeof(string));
                varpekerjaan[20] = gb.CreateSPParameter("@tempatusahalain", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkepemilikantempatusaha.Text, typeof(string));
                varpekerjaan[21] = gb.CreateSPParameter("@Telepon", ParameterDirection.Input, SqlDbType.VarChar, 250, txtteleponpekerjaan.Text, typeof(string));
                varpekerjaan[22] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                varpekerjaan[23] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varpekerjaan[24] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                varpekerjaan[25] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAHPEKERJAAN", varpekerjaan, "constrmf");

                msg3 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();

                //INSERT INTO ARNASABAHPENDAPATAN
                DefaultClass.Global.SqlHelper[] varpendapatan = new DefaultClass.Global.SqlHelper[9];
                varpendapatan[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                varpendapatan[1] = gb.CreateSPParameter("@gaji_kotor", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlgajikotor.SelectedValue, typeof(string));
                varpendapatan[2] = gb.CreateSPParameter("@sumber_penghasilan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlsumberpenghasilan.SelectedValue, typeof(string));
                varpendapatan[3] = gb.CreateSPParameter("@transaksi_perhari", ParameterDirection.Input, SqlDbType.VarChar, 250, ddltransaksiperhari.SelectedValue, typeof(string));
                varpendapatan[4] = gb.CreateSPParameter("@sumber_penghasilan_lain", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsumberpenghasilan.Text, typeof(string));
                varpendapatan[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                varpendapatan[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varpendapatan[7] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                varpendapatan[8] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAHPENDAPATAN", varpendapatan, "constrmf");

                msg4 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();

                //INSERT INTO ARNASABAHLAINNYA
                DefaultClass.Global.SqlHelper[] varlainnya = new DefaultClass.Global.SqlHelper[13];
                varlainnya[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                varlainnya[1] = gb.CreateSPParameter("@tujuan_pembukaan_rekening", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlTujuanbukaRekening.SelectedValue, typeof(string));
                varlainnya[2] = gb.CreateSPParameter("@tujuan_pembukaan_rekening_lain", ParameterDirection.Input, SqlDbType.VarChar, 250, txttujuanbukarekening.Text, typeof(string));
                varlainnya[3] = gb.CreateSPParameter("@dalam_hal_bertindak", ParameterDirection.Input, SqlDbType.VarChar, 250, ddldalamhalbertindak.SelectedValue, typeof(string));
                varlainnya[4] = gb.CreateSPParameter("@dalam_hal_bertindak_lain", ParameterDirection.Input, SqlDbType.VarChar, 250, txtdalamhalbertindak.Text, typeof(string));
                varlainnya[5] = gb.CreateSPParameter("@kode_AO", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlAO.SelectedValue, typeof(string));
                varlainnya[6] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamaibukandung.Text, typeof(string));
                varlainnya[7] = gb.CreateSPParameter("@inputed_by", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                varlainnya[8] = gb.CreateSPParameter("@kena_pajak", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlKenaPajak.SelectedValue, typeof(string));
                varlainnya[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                varlainnya[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varlainnya[11] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                varlainnya[12] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAHLAINNYA", varlainnya, "constrmf");

                msg5 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();

                //INSERT INTO AR_NASABAHPIHAKLAIN
                DefaultClass.Global.SqlHelper[] varpihaklain = new DefaultClass.Global.SqlHelper[16];
                varpihaklain[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                varpihaklain[1] = gb.CreateSPParameter("@hubungan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlHubunganPihakLain.SelectedValue, typeof(string));
                varpihaklain[2] = gb.CreateSPParameter("@Alamat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatPihakLain.Text, typeof(string));
                varpihaklain[3] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 250, txtRTPihakLain.Text, typeof(string));
                varpihaklain[4] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 250, txtRWPihakLain.Text, typeof(string));
                varpihaklain[5] = gb.CreateSPParameter("@Kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKelurahanPihakLain.Text, typeof(string));
                varpihaklain[6] = gb.CreateSPParameter("@Kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKecamatanPihakLain.Text, typeof(string));
                varpihaklain[7] = gb.CreateSPParameter("@Kota", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKotaPihakLain.Text, typeof(string));
                varpihaklain[8] = gb.CreateSPParameter("@Provinsi", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlProvinsiPihakLain.SelectedValue, typeof(string));
                varpihaklain[9] = gb.CreateSPParameter("@kode_pos", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKodePosPihakLain.Text, typeof(string));
                varpihaklain[10] = gb.CreateSPParameter("@Tlp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtTlpPihakLain.Text, typeof(string));
                varpihaklain[11] = gb.CreateSPParameter("@nama_pihaklain", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapihaklain.Text, typeof(string));
                varpihaklain[12] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                varpihaklain[13] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varpihaklain[14] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                varpihaklain[15] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAHPIHAKLAIN", varpihaklain, "constrmf");

                msg6 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();

                ////INSERT INTO ArMst
                //DefaultClass.Global.SqlHelper[] var10 = new DefaultClass.Global.SqlHelper[57];

                //var10[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                //var10[1] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 250, "3", typeof(string));
                //var10[2] = gb.CreateSPParameter("@NamaLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
                //var10[3] = gb.CreateSPParameter("@NamaLgn2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapanggilan.Text, typeof(string));
                //var10[4] = gb.CreateSPParameter("@KodeWil", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                //var10[5] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatsesuaiktp1.Text, typeof(string));
                //var10[6] = gb.CreateSPParameter("@Alamat2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatsesuaiktp2.Text, typeof(string));
                //var10[7] = gb.CreateSPParameter("@Alamat3", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatsesuaiktp3.Text, typeof(string));
                //var10[8] = gb.CreateSPParameter("@Telepon", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[9] = gb.CreateSPParameter("@Nofax", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[10] = gb.CreateSPParameter("@Email", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                //var10[11] = gb.CreateSPParameter("@NoHp", ParameterDirection.Input, SqlDbType.VarChar, 250, txthp.Text, typeof(string));
                //var10[12] = gb.CreateSPParameter("@Website", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                //var10[13] = gb.CreateSPParameter("@Npwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNPWP.Text, typeof(string));
                //var10[14] = gb.CreateSPParameter("@Nppkp", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                //var10[15] = gb.CreateSPParameter("@SyaratBayar", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                //var10[16] = gb.CreateSPParameter("@BatasKredit", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[17] = gb.CreateSPParameter("@JJualThn", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[18] = gb.CreateSPParameter("@JJualAkm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[19] = gb.CreateSPParameter("@JBayarBln", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[20] = gb.CreateSPParameter("@JBayarThn", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[21] = gb.CreateSPParameter("@JlainThn", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[22] = gb.CreateSPParameter("@SaldoAwal", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[23] = gb.CreateSPParameter("@SaldoAwalGr  ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[24] = gb.CreateSPParameter("@SaldoGiro", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[25] = gb.CreateSPParameter("@SaldoGiroBln ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[26] = gb.CreateSPParameter("@SaldoUmkAwal ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[27] = gb.CreateSPParameter("@SaldoUmk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[28] = gb.CreateSPParameter("@SaldoUmkBln", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[29] = gb.CreateSPParameter("@HubungDengan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlHubunganPihakLain.SelectedValue, typeof(string));
                ////v10r236] = gb.CreateSPParameter("@Jabatan", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
                //var10[30] = gb.CreateSPParameter("@BidangUsaha", ParameterDirection.Input, SqlDbType.VarChar, 250, txtBidangUsaha.Text, typeof(string));
                //var10[31] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                //var10[32] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now.ToString("yyyy/MM/dd"), typeof(string));
                //var10[33] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                //var10[34] = gb.CreateSPParameter("@KodeCrc ", ParameterDirection.Input, SqlDbType.VarChar, 250, "IDR", typeof(string));
                //var10[35] = gb.CreateSPParameter("@kurs ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[36] = gb.CreateSPParameter("@JAkhir", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[37] = gb.CreateSPParameter("@JAkhirRp", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[38] = gb.CreateSPParameter("@KodePos", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodeposktp.Text, typeof(string));
                //var10[39] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                //var10[40] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
                //var10[41] = gb.CreateSPParameter("@DateModified", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
                //var10[42] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                //var10[43] = gb.CreateSPParameter("@NamaKirim", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapihaklain.Text, typeof(string));
                //var10[44] = gb.CreateSPParameter("@NamaKirim2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapihaklain.Text, typeof(string));
                //var10[45] = gb.CreateSPParameter("@AlamatKirim1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatPihakLain.Text, typeof(string));    //Penjamin
                //var10[46] = gb.CreateSPParameter("@AlamatKirim2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKelurahanPihakLain.Text, typeof(string)); //Penjamin
                //var10[47] = gb.CreateSPParameter("@AlamatKirim3", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKotaPihakLain.Text, typeof(string));      //Penjamin
                //var10[48] = gb.CreateSPParameter("@TelponKirim", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));        //Penjamin            
                //var10[49] = gb.CreateSPParameter("@JabatanKirim", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlHubunganPihakLain.SelectedValue, typeof(string));
                //var10[50] = gb.CreateSPParameter("@KodeposKirim", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKodePosPihakLain.Text, typeof(string));
                //var10[51] = gb.CreateSPParameter("@HpKirim", ParameterDirection.Input, SqlDbType.VarChar, 250, txtTlpPihakLain.Text, typeof(string));
                //var10[52] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                //var10[53] = gb.CreateSPParameter("@KursAkhir ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
                //var10[54] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                //var10[55] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var10[56] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("AR_ArMst", var10, "constrmf");




                msg7 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();


                if (msg1 == true.ToString() && msg3 == true.ToString() && msg4 == true.ToString() && msg5 == true.ToString() && msg6 == true.ToString() && msg7 == true.ToString())
                {

                    if (STATE == "INSERT")
                    {

                        DefaultClass.Global.SqlHelper[] varSTATE = new DefaultClass.Global.SqlHelper[4];
                        varSTATE[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                        varSTATE[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                        varSTATE[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "UPDATENONASABAH", typeof(string));
                        varSTATE[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", varSTATE, "constrmf");

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Update Done!','You successfully save this Project','top-right','success','2');", true);
                        //Response.Redirect("RegistrasiNasabah.aspx");
                    }

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));


                }



            }

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            DefaultClass.Global.SqlHelper[] delete = new DefaultClass.Global.SqlHelper[13];
            delete[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoNasabah.Value, typeof(string));
            delete[11] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
            delete[12] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", delete, "constrmf");
        }


    }

    protected void txtlainlain1_TextChanged(object sender, EventArgs e)
    {
        if (txtlainlain1.Text == "0" || txtlainlain1.Text == "")
        {
            fileuploadlainlain1.Enabled = false;
            btnlainlain1.Enabled = false;
            txtpathlainlain1.Enabled = false;
            txtpathlainlain1.Text = "-";
        }
        else
        {
            fileuploadlainlain1.Enabled = true;
            btnlainlain1.Enabled = true;
            txtpathlainlain1.Text = "";
        }
    }

    protected void txtlainlain2_TextChanged(object sender, EventArgs e)
    {
        if (txtlainlain2.Text == "0" || txtlainlain2.Text == "")
        {
            fileuploadlainlain2.Enabled = false;
            btnlainlain2.Enabled = false;
            txtpathlainlain2.Enabled = false;
            txtpathlainlain2.Text = "-";
        }
        else
        {
            fileuploadlainlain2.Enabled = true;
            btnlainlain2.Enabled = true;
            txtpathlainlain2.Text = "";
        }
    }

    protected void txtidpasangan_TextChanged(object sender, EventArgs e)
    {
        if (txtidpasangan.Text == "0" || txtidpasangan.Text == "")
        {
            txtnamapasangan.Enabled = false;
            txttempatlahirpasangan.Enabled = false;
            txttgllahirpasangan.Enabled = false;
            fileuploadktppasangan.Enabled = false;
            btnuploadktppasangan.Enabled = false;
            txtppathktppasangan.Enabled = false;
            txtppathktppasangan.Text = "-";
            txttgllahirpasangan.Text = "";

            txttempatlahirpasangan.Text = "-";
            txttgllahirpasangan.Text = "1900-01-01 00:00:00";
            txtnamapasangan.Text = "-";
        }
        else
        {
            txtnamapasangan.Enabled = true;
            txttempatlahirpasangan.Enabled = true;
            txttgllahirpasangan.Enabled = true;
            fileuploadktppasangan.Enabled = true;
            btnuploadktppasangan.Enabled = true;
            txttgllahirpasangan.Text = "";
        }
    }
    protected void ddlsumberpenghasilan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsumberpenghasilan.SelectedValue == "5")
        {
            txtsumberpenghasilan.Enabled = true;
            txtsumberpenghasilan.Text = "";

        }
        else
        {
            txtsumberpenghasilan.Enabled = false;
            txtsumberpenghasilan.Text = "-";
        }

    }

    protected void ddlstatusrumah_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatusrumah.SelectedValue == "8")
        {
            txtalamatrumahtinggal1.Text = txtalamatsesuaiktp1.Text;
            txtrt.Text = txtrtktp.Text;
            txtrw.Text = txtrwktp.Text;
            txtkelurahan.Text =txtkelurahanktp.Text;
            txtkecamatan.Text = txtkecamatanktp.Text;
            ddlprovinsi.SelectedValue = ddlprovinsiktp.SelectedValue;
            txtkodepos.Text = txtkodeposktp.Text;


            txtalamatrumahtinggal1.Enabled=false;
            txtrt.Enabled = false;
            txtrw.Enabled = false;
            txtkelurahan.Enabled = false;
            txtkecamatan.Enabled = false;
            ddlprovinsi.Enabled = false;
            txtkodepos.Enabled = false;


        }
        else
        {
            txtalamatrumahtinggal1.Text = "-";
            txtrt.Text = "0";
            txtrw.Text = "0";
            txtkelurahan.Text = "-";
            txtkecamatan.Text = "-";
            //ddlprovinsiktp.SelectedValue = "-";
            txtkodepos.Text = "0";


            txtalamatrumahtinggal1.Enabled = true;
            txtrt.Enabled = true;
            txtrw.Enabled = true;
            txtkelurahan.Enabled = true;
            txtkecamatan.Enabled = true;
            ddlprovinsi.Enabled = true;
            txtkodepos.Enabled = true;
        }

    }



    //protected void ddlnpwp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   if(txtNPWP.SelectedValue == "1")
    //   {
    //           txtNPWP.Enabled = false;
    //           txtNPWP.Text = "-";
    //   }else
    //   {
    //           txtNPWP.Enabled = true;
    //           txtNPWP.Text = "";
    //   }

    //}

    protected void ddlTujuanbukaRekening_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlTujuanbukaRekening.SelectedValue == "4")
        {
            txttujuanbukarekening.Enabled = true;
            txttujuanbukarekening.Text = "";
        }
        else
        {
            txttujuanbukarekening.Enabled = false;
            txttujuanbukarekening.Text = "-";
        }


    }
    protected void ddldalamhalbertindak_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddldalamhalbertindak.SelectedValue == "4")
        {
            txtdalamhalbertindak.Enabled = true;
            txtdalamhalbertindak.Text = "";
        }
        else
        {
            txtdalamhalbertindak.Enabled = false;
            txtdalamhalbertindak.Text = "-";
        }


    }

    protected void ddlkewarganegaraan_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlkewarganegaraan.SelectedValue == "2")
        {
            txtkewarganegaraan.Enabled = true;
            txtkewarganegaraan.Text = "";
        }
        else
        {
            txtkewarganegaraan.Enabled = false;
            txtkewarganegaraan.Text = "-";
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
     //   ddlAgama.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Sandi Kota
        //ge.ListDataset = gb.GetDataFunction("dbo.GetSandiKota()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "nama_kota asc";

        //ddlsandikota.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlsandikota.DataTextField = "nama_kota";
        //ddlsandikota.DataValueField = "sandi_kota";
        //ddlsandikota.DataBind();
        //ddlsandikota.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Provinsi
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsi.DataTextField = "nama_provinsi";
        ddlprovinsi.DataValueField = "kode_provinsi";
        ddlprovinsi.DataBind();
       // ddlprovinsi.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Status Rumah
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusRumah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatusrumah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatusrumah.DataTextField = "keterangan";
        ddlstatusrumah.DataValueField = "id";
        ddlstatusrumah.DataBind();
      //  ddlstatusrumah.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo kewarganegaraan
        ge.ListDataset = gb.GetDataFunction("dbo.GetCitizen()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlkewarganegaraan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkewarganegaraan.DataTextField = "keterangan";
        ddlkewarganegaraan.DataValueField = "id";
        ddlkewarganegaraan.DataBind();
     //   ddlkewarganegaraan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Jenis Identitas
        //ge.ListDataset = gb.GetDataFunction("dbo.GetIdentityCard()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlidentitas.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlidentitas.DataTextField = "keterangan";
        //ddlidentitas.DataValueField = "id";
        //ddlidentitas.DataBind();
        //ddlidentitas.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Pendidikan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpendidikan.DataTextField = "keterangan";
        ddlpendidikan.DataValueField = "id";
        ddlpendidikan.DataBind();
      //  ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Pendidikan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPendidikan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlpendidikan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpendidikan.DataTextField = "keterangan";
        ddlpendidikan.DataValueField = "id";
        ddlpendidikan.DataBind();
       // ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetArea()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";

        //ddlkdarea.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlkdarea.DataTextField = "nama_area";
        //ddlkdarea.DataValueField = "kode_area";
        //ddlkdarea.DataBind();
        //ddlkdarea.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetNPWP()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlNPWP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlNPWP.DataTextField = "keterangan";
        //ddlNPWP.DataValueField = "id";
        //ddlNPWP.DataBind();
        //ddlNPWP.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Provinsi KTP
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiktp.DataTextField = "nama_provinsi";
        ddlprovinsiktp.DataValueField = "kode_provinsi";
        ddlprovinsiktp.DataBind();
       // ddlprovinsiktp.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Provinsi Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsipekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsipekerjaan.DataTextField = "nama_provinsi";
        ddlprovinsipekerjaan.DataValueField = "kode_provinsi";
        ddlprovinsipekerjaan.DataBind();
     //   ddlprovinsipekerjaan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPekerjaan.DataTextField = "keterangan";
        ddlPekerjaan.DataValueField = "id";
        ddlPekerjaan.DataBind();
   //     ddlPekerjaan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Kepemilikan Tempat Usaha
        ge.ListDataset = gb.GetDataFunction("dbo.GetKepemilikanTempatUsaha()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlkepemilikantempatusaha.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkepemilikantempatusaha.DataTextField = "keterangan";
        ddlkepemilikantempatusaha.DataValueField = "id";
        ddlkepemilikantempatusaha.DataBind();
    //    ddlkepemilikantempatusaha.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Alamat Surat
        ge.ListDataset = gb.GetDataFunction("dbo.GetAlamatSurat()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlAlamatSurat.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAlamatSurat.DataTextField = "keterangan";
        ddlAlamatSurat.DataValueField = "id";
        ddlAlamatSurat.DataBind();
   //     ddlAlamatSurat.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Lama Bekerja
        ge.ListDataset = gb.GetDataFunction("dbo.GetLamaKerja()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlLamaBekerja.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlLamaBekerja.DataTextField = "keterangan";
        ddlLamaBekerja.DataValueField = "id";
        ddlLamaBekerja.DataBind();
     //   ddlLamaBekerja.Items.Insert(0, new ListItem("Pilih Data", "-"));


        // -- Combo Penghasil Gaji Kotor Per Tahun
        ge.ListDataset = gb.GetDataFunction("dbo.GetPenghasilanKotor()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlgajikotor.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgajikotor.DataTextField = "keterangan";
        ddlgajikotor.DataValueField = "id";
        ddlgajikotor.DataBind();
       // ddlgajikotor.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Sumber Penghasilan
        ge.ListDataset = gb.GetDataFunction("dbo.GetSumberPenghasilan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlsumberpenghasilan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberpenghasilan.DataTextField = "keterangan";
        ddlsumberpenghasilan.DataValueField = "id";
        ddlsumberpenghasilan.DataBind();
      //  ddlsumberpenghasilan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Aktifitas Transaksi Per Hari
        ge.ListDataset = gb.GetDataFunction("dbo.GetTransaksiNormal()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddltransaksiperhari.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltransaksiperhari.DataTextField = "keterangan";
        ddltransaksiperhari.DataValueField = "id";
        ddltransaksiperhari.DataBind();
      //  ddltransaksiperhari.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Tujuan Pembukaan Rekening
        ge.ListDataset = gb.GetDataFunction("dbo.GetTujuanBukaRekening()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlTujuanbukaRekening.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlTujuanbukaRekening.DataTextField = "keterangan";
        ddlTujuanbukaRekening.DataValueField = "id";
        ddlTujuanbukaRekening.DataBind();
      //  ddlTujuanbukaRekening.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Dalam Hal Bertindak
        ge.ListDataset = gb.GetDataFunction("dbo.GetTindakan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddldalamhalbertindak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddldalamhalbertindak.DataTextField = "keterangan";
        ddldalamhalbertindak.DataValueField = "id";
        ddldalamhalbertindak.DataBind();
     //   ddldalamhalbertindak.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Kena Pajak
        ge.ListDataset = gb.GetDataFunction("dbo.GetKenaPajak()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlKenaPajak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKenaPajak.DataTextField = "keterangan";
        ddlKenaPajak.DataValueField = "id";
        ddlKenaPajak.DataBind();
    //    ddlKenaPajak.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // COMBO MASTER MO
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_MO", var1, "constrmf");

        ddlAO.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAO.DataTextField = "namaAO";
        ddlAO.DataValueField = "NIK";
        ddlAO.DataBind();
    //    ddlAO.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // COMBO MASTER HUBUNGAN 
        ge.ListDataset = gb.GetDataFunction("dbo.GetHubungan()", "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlHubunganPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

        ddlHubunganPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlHubunganPihakLain.DataTextField = "keterangan";
        ddlHubunganPihakLain.DataValueField = "id";
        ddlHubunganPihakLain.DataBind();
     //   ddlHubunganPihakLain.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // COMBO PROVINSI 
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlProvinsiPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

        ddlProvinsiPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlProvinsiPihakLain.DataTextField = "nama_provinsi";
        ddlProvinsiPihakLain.DataValueField = "kode_provinsi";
        ddlProvinsiPihakLain.DataBind();
        //   ddlProvinsiPihakLain.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlkewarganegaraan.SelectedValue = "1";
        txtkewarganegaraan.Enabled = false;
        txtkewarganegaraan.Text = "-";
    }

    protected void UploadFileKTP(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/KTP/");
        DateTime now = DateTime.Now;

        namafileKTP = txtidentitas.Text + "_" + now.ToString("ddMMyyyy") + "_" + "KTP";


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
        fileuploadktp.SaveAs(folderPath + namafileKTP + ".jpg");

        ////Display the Picture in Image control.
        //imagektp.ImageUrl = "~/Files/KTP/" + Path.GetFileName(fileuploadktp.FileName);

        fotopathktp = folderPath + Path.GetFileName(fileuploadktp.FileName);
        txtpathktp.Text = "~/Files/KTP/" + namafileKTP + ".jpg";

        //}


    }

    protected void UploadFileNPWP(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/NPWP/");
        DateTime now = DateTime.Now;

        namafileNPWP = txtNPWP.Text + "_" + now.ToString("ddMMyyyy") + "_" + "NPWP";


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
        fileuploadnpwp.SaveAs(folderPath + namafileNPWP + ".jpg");

        ////Display the Picture in Image control.
        //imagenpwp.ImageUrl = "~/Files/NPWP/" + Path.GetFileName(fileuploadnpwp.FileName);

        fotopathnpwp = folderPath + Path.GetFileName(fileuploadnpwp.FileName);
        txtpathnpwp.Text = "~/Files/NPWP/" + namafileNPWP + ".jpg";


        //}
    }


    protected void UploadFileKK(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/KK/");
        DateTime now = DateTime.Now;

        namafileKK = txtnomerkk.Text + "_" + now.ToString("ddMMyyyy") + "_" + "KK";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadKK.FileName) != namafileKK)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileKK + "');", true);

        //}
        //else
        //{

        //Save the File to the Directory (Folder).
        fileuploadKK.SaveAs(folderPath + namafileKK + ".jpg");

        ////Display the Picture in Image control.
        //imageKK.ImageUrl = "~/Files/KK" + Path.GetFileName(fileuploadKK.FileName);

        txtpathKK.Text = "~/Files/KK/" + namafileKK + ".jpg";

        //}




    }

    protected void UploadFilelainlain(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/LainLain/");
        DateTime now = DateTime.Now;

        namafileLainLain1 = txtlainlain1.Text + "_" + now.ToString("ddMMyyyy") + "_" + "LainLain";

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadlainlain1.FileName) != namafileLainLain1)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileLainLain1 + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        fileuploadlainlain1.SaveAs(folderPath + namafileLainLain1 + ".jpg");

        //Display the Picture in Image control.
        //imagelainlain.ImageUrl = "~/Files/LainLain" + Path.GetFileName(fileuploadlainlain1.FileName);

        txtpathlainlain1.Text = "~/Files/LainLain/" + namafileLainLain1 + ".jpg";
        ///}


    }

    protected void UploadFilelainlain2(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/LainLain2/");
        DateTime now = DateTime.Now;

        namafileLainLain2 = txtlainlain2.Text + "_" + now.ToString("ddMMyyyy") + "_" + "LainLain2";

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadlainlain2.FileName) != namafileLainLain2)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileLainLain2 + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        fileuploadlainlain2.SaveAs(folderPath + namafileLainLain2 + ".jpg");

        //Display the Picture in Image control.
        //imagelainlain2.ImageUrl = "~/Files/LainLain2" + Path.GetFileName(fileuploadlainlain2.FileName);

        txtpathlainlain2.Text = "~/Files/LainLain2/" + namafileLainLain2 + ".jpg";
        //}


    }

    protected void UploadFilektppasangan(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/KTP_Pasangan/");
        DateTime now = DateTime.Now;

        namafilektppasangan = txtidpasangan.Text + "_" + now.ToString("ddMMyyyy") + "_" + "KTP_Pasangan";

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if (Path.GetFileNameWithoutExtension(fileuploadktppasangan.FileName) != namafilektppasangan)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafilektppasangan + "');", true);

        //}
        //else
        //{

        //Save the File to the Directory (Folder).
        fileuploadktppasangan.SaveAs(folderPath + namafilektppasangan + ".jpg");

        //Display the Picture in Image control.
        //imagepasangan.ImageUrl = "~/Files/KTP_Pasangan" + Path.GetFileName(fileuploadktppasangan.FileName);

        txtppathktppasangan.Text = "~/Files/KTP_Pasangan/" + namafilektppasangan + ".jpg";
        //txtppathktppasangan.Text = imagepasangan.ImageUrl;
        //}

    }
    //protected void txtemails_TextChanged(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    //    if (txtemails.Text != "-")
    //    //    {
    //    //        var eMailValidator = new System.Net.Mail.MailAddress(txtemails.Text);
    //    //    } 
    //    //}
    //    //catch (FormatException ex)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Format Email yang anda masukan salah');", true);

    //    //}
    //    Regex mRegxExpression;
    //    if (txtemails.Text.Trim() != string.Empty)
    //    {
    //        mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

    //        if (!mRegxExpression.IsMatch(txtemails.Text.Trim()))
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Format Email yang anda masukan salah');", true);

    //            txtemails.Focus();
    //        }
    //    }
    //}
        
}