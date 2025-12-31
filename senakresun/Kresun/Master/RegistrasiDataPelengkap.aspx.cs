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
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Activities.Statements;

public partial class FA_Master_RegistrasiDataPelengkap : System.Web.UI.Page
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
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        if (txtSearch.Text == "")
        {
            //BindGridEntity();
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
        ge.ListDataset = gb.GetDataJustOutPut("KP_GetNasabah", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }
    protected void txtidentitas_TextChanged(object sender, EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //var[0] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
        //var[1] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaibukandung.Text, typeof(string));
        //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTP", typeof(string));
        //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
        //ge.ListDataset = gb.GetDataJustOutPut("KP_NASABAH", var, "constrmf");

        //if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah sudah terdaftar');", true);

        //}

    }



   
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RegistrasiDataPelengkap.aspx");
    }
    //protected void txttgllahir_TextChanged(object sender, EventArgs e)
    //{

    //    DateTime date = Convert.ToDateTime(t.Text.Trim());
    //    string dateString = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);


    //    DateTime date1 = Convert.ToDateTime(DateTime.Now);
    //    string dateString1 = date1.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

    //    if (int.Parse(dateString) > int.Parse(dateString1))
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Lahir debitur tidak boleh melebihi tanggal hari ini');", true);

    //    }
    //    else
    //    {

        

    //        string usia = gb.HitungTahunUsia(txttgllahir.Text);

    //        decimal umur = decimal.Parse(usia);

    //       //string sDate = DateTime.ParseExact(txttgllahir.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
    //        //DateTime date = DateTime.ParseExact(txttgllahir.Text, "yyyy/MM/dd", null);
    //        //DateTime dob = date; 
    //        //int age = CalculateAge(dob);

    //        if (umur < 40 || umur > 80)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Usia Debitur tidak masuk kriteria peminjaman');", true);

    //        }
    //    }
    //}
    private static int CalculateAge(DateTime dateOfBirth)
    {
        int age = 0;
        age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age = age - 1;

        return age;
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistrasiDataPelengkap.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                txtalamatPihakLain.Attributes.Add("maxlength", txtalamatPihakLain.MaxLength.ToString());
                //BindGridEntity();
               
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
    //public void BindGridEntity()
    //{

    //    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
    //    var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
    //    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


    //    ge.ListDataset = gb.GetDataJustOutPut("KP_NASABAH", var, "constrmf");

    //    ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah desc";

    //    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
    //    GRData.DataBind();

    //    GRData.UseAccessibleHeader = true;

    //    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    //}
 
    protected void ddlstatusrumah_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatusrumah.SelectedValue == "8")
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");


            //Panel Data nasabah 1
            txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["rt_ktp"].ToString();
            txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["rw_ktp"].ToString();
            txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
            txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
            ddlprovinsitpt.Text = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();

            txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
            txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();

            txtalamatrumahtinggal1.Enabled = false;
            txtrttp.Enabled = false;
            txtrwtpt.Enabled = false;
            txtkelurahantpt.Enabled = false;
            txtkecamatantpt.Enabled = false;
            ddlprovinsitpt.Enabled = false;
            txtkodepostpt.Enabled = false;


        }
        else
        {
            txtalamatrumahtinggal1.Text = "-";
            txtkelurahantpt.Text = "-";
            txtrttp.Text = "0";
            txtrwtpt.Text = "0";
            txtkodepostpt.Text = "0";
            txtkecamatantpt.Text = "-";
            txtalamatrumahtinggal1.Enabled = true;
            txtrttp.Enabled = true;
            txtrwtpt.Enabled = true;
            txtkelurahantpt.Enabled = true;
            txtkecamatantpt.Enabled = true;
            ddlprovinsitpt.Enabled = true;
            txtkodepostpt.Enabled = true;
        }

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":
                //HDState.Value = e.CommandName;

                try

                {

                    PnlFront.Visible = false;
                    //pnladd.Visible = false;
                    pnlHeader.Visible = false;
                    Panel1.Visible = false;
                    pnldataanggota.Visible = true;
                    Pnl1.Visible = true;
                    pnlPekerjaan.Visible = true;
                    //PnlPendapatan.Visible = true;
                    //pnlLainnya.Visible = true;
                    pnlPihakYgDapatDiHubungi.Visible = true;
                    Pnlfooter4.Visible = true;
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtnoanggota.Text = no_nasabah;
                    txtnamaanggota.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                    txttanggalbergabung.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDIT", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_UPDATEDATA_NASABAH", var, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                    {

                        //txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_pasangan"].ToString();


                        ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_rumah"].ToString();



                        txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamattp"].ToString();
                        txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["rttp"].ToString();
                        txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["rwtp"].ToString();
                        txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahantp"].ToString();
                        txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatantp"].ToString();
                        ddlprovinsitpt.Text = ge.ListDataset.Tables[0].Rows[0]["propinsitp"].ToString();
                        txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["Kodepostp"].ToString();
                        txtkabupatenpt.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatantp"].ToString();


                        ddlstatuskawinupload.Text = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                        txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                        txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                        txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                        txttgllahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();


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

                        txtalamatperusahaan1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_perusahaan1"].ToString();
                        txtkabupatenpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["Kabpekerjaan"].ToString();
                        txtkelurahanpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                        txtkecamatanpekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                        ddlprovinsipekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                        txtkodepospekerjaan.Text = ge.ListDataset.Tables[0].Rows[0]["kode_pos"].ToString();
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
                        //ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                        txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                        //txtkodepetugas.Text = ge.ListDataset.Tables[0].Rows[0]["inputed_by"].ToString();
                        ddlKenaPajak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kena_pajak"].ToString();
                        txtjumlahanak.Text = ge.ListDataset.Tables[0].Rows[0]["jumlah_anak"].ToString();


                        txtnamapihaklain.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pihaklain"].ToString();
                        ddlHubunganPihakLain.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["hubungan"].ToString();
                        txtalamatPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat"].ToString();
                        txtRTPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                        txtRWPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                        txtKelurahanPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kelurahanlain"].ToString();
                        txtKecamatanPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kecamatanlain"].ToString();
                        txtKotaPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Kota"].ToString();
                        ddlProvinsiPihakLain.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Propinsilain"].ToString();
                        txtKodePosPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["kodeposlain"].ToString();
                        txtTlpPihakLain.Text = ge.ListDataset.Tables[0].Rows[0]["Tlp"].ToString();


                        //txtkodepetugas.Text = SessLoginID;


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);




                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data nasabah Tidak ditemukan');", true);
                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }


                break;

       
        }
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
            else
            {



                STATE = "UPDATE";


                if (txttgllahirpasangan.Text == "")
                {
                    txttgllahirpasangan.Text = "1900-01-01";
                }


                btnSimpan.Enabled = false;

                //INSERT INTO ARNASABAH
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[57];

                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@status_pernikahan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatuskawinupload.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@no_ktp_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtidpasangan.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@nama_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapasangan.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@tempat_lahir_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txttempatlahirpasangan.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@tanggal_lahir_pasangan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahirpasangan.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@status_rumah", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlstatusrumah.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@alamat_rumah_tinggal1", ParameterDirection.Input, SqlDbType.VarChar, 150, txtalamatrumahtinggal1.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 10, txtrttp.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 10, txtrwtpt.Text, typeof(string));
                var[10] = gb.CreateSPParameter("@kelurahan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkelurahantpt.Text, typeof(string));
                var[11] = gb.CreateSPParameter("@kecamatan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkecamatantpt.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@provinsi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprovinsitpt.SelectedValue, typeof(string));
                var[13] = gb.CreateSPParameter("@kodepos", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodepostpt.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@kabupaten", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkabupatenpt.Text, typeof(string));
                var[15] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEDATA", typeof(string));
                var[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[17] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                 var[18] = gb.CreateSPParameter("@nama_pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 70, txtPekerjaan.Text, typeof(string));
               var[19] = gb.CreateSPParameter("@jabatan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjabatan.Text, typeof(string));
               var[20] = gb.CreateSPParameter("@nama_perusahaan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNamaPerusahaan.Text, typeof(string));
               var[21] = gb.CreateSPParameter("@bidang_usaha", ParameterDirection.Input, SqlDbType.VarChar, 100, txtBidangUsaha.Text, typeof(string));
               var[22] = gb.CreateSPParameter("@lama_bekerja", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlLamaBekerja.SelectedValue, typeof(string));
               var[23] = gb.CreateSPParameter("@kepemilikan_tempat_usaha", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkepemilikantempatusaha.SelectedValue, typeof(string));
               var[24] = gb.CreateSPParameter("@alamat_perusahaan1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatperusahaan1.Text, typeof(string));
               var[25] = gb.CreateSPParameter("@kabupatenpt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkabupatenpekerjaan.Text, typeof(string));
               var[26] = gb.CreateSPParameter("@kelurahanpt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkelurahanpekerjaan.Text, typeof(string));
               var[27] = gb.CreateSPParameter("@kecamatanpt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkecamatanpekerjaan.Text, typeof(string));
               var[28] = gb.CreateSPParameter("@provinsipt", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlprovinsipekerjaan.SelectedValue, typeof(string));
               var[29] = gb.CreateSPParameter("@kode_pospt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkodepospekerjaan.Text, typeof(string));
               var[30] = gb.CreateSPParameter("@fax", ParameterDirection.Input, SqlDbType.VarChar, 250, txtFaxPekerjaan.Text, typeof(string));
               var[31] = gb.CreateSPParameter("@kode_pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlPekerjaan.SelectedValue, typeof(string));
               var[32] = gb.CreateSPParameter("@tempatusahalain", ParameterDirection.Input, SqlDbType.VarChar, 100, txtkepemilikantempatusaha.Text, typeof(string));
               var[33] = gb.CreateSPParameter("@Teleponpt", ParameterDirection.Input, SqlDbType.VarChar, 30, txtteleponpekerjaan.Text, typeof(string));
                var[34] = gb.CreateSPParameter("@gaji_kotor", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlgajikotor.SelectedValue, typeof(string));
                var[35] = gb.CreateSPParameter("@sumber_penghasilan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlsumberpenghasilan.SelectedValue, typeof(string));
                var[36] = gb.CreateSPParameter("@transaksi_perhari", ParameterDirection.Input, SqlDbType.VarChar, 250, ddltransaksiperhari.SelectedValue, typeof(string));
                var[37] = gb.CreateSPParameter("@sumber_penghasilan_lain", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsumberpenghasilan.Text, typeof(string));

                var[38] = gb.CreateSPParameter("@nama_pihaklain", ParameterDirection.Input, SqlDbType.VarChar, 70, txtnamapihaklain.Text, typeof(string));
                var[39] = gb.CreateSPParameter("@hubungan", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlHubunganPihakLain.SelectedValue, typeof(string));
                var[40] = gb.CreateSPParameter("@Alamatpl", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamatPihakLain.Text, typeof(string));
                var[41] = gb.CreateSPParameter("@RTpl", ParameterDirection.Input, SqlDbType.VarChar, 250, txtRTPihakLain.Text, typeof(string));
                var[42] = gb.CreateSPParameter("@RWpl", ParameterDirection.Input, SqlDbType.VarChar, 250, txtRWPihakLain.Text, typeof(string));
                var[43] = gb.CreateSPParameter("@Kelurahanpl", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKelurahanPihakLain.Text, typeof(string));
                var[44] = gb.CreateSPParameter("@Kecamatanpl", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKecamatanPihakLain.Text, typeof(string));
                var[45] = gb.CreateSPParameter("@Kotapl", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKotaPihakLain.Text, typeof(string));
                var[46] = gb.CreateSPParameter("@Provinsipl", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlProvinsiPihakLain.SelectedValue, typeof(string));
                var[47] = gb.CreateSPParameter("@kode_pospl", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKodePosPihakLain.Text, typeof(string));
                var[48] = gb.CreateSPParameter("@Tlp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtTlpPihakLain.Text, typeof(string));

                var[49] = gb.CreateSPParameter("@tujuan_pembukaan_rekening", ParameterDirection.Input, SqlDbType.VarChar, 100, ddlTujuanbukaRekening.SelectedValue, typeof(string));
                var[50] = gb.CreateSPParameter("@tujuan_pembukaan_rekening_lain", ParameterDirection.Input, SqlDbType.VarChar, 100, txttujuanbukarekening.Text, typeof(string));
                var[51] = gb.CreateSPParameter("@dalam_hal_bertindak", ParameterDirection.Input, SqlDbType.VarChar, 100, ddldalamhalbertindak.SelectedValue, typeof(string));
                var[52] = gb.CreateSPParameter("@dalam_hal_bertindak_lain", ParameterDirection.Input, SqlDbType.VarChar, 100, txtdalamhalbertindak.Text, typeof(string));
            
                var[53] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 70, txtnamaibukandung.Text, typeof(string));
                var[54] = gb.CreateSPParameter("@jumlah_anak", ParameterDirection.Input, SqlDbType.VarChar, 10, txtjumlahanak.Text, typeof(string));
                var[55] = gb.CreateSPParameter("@kena_pajak", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlKenaPajak.SelectedValue, typeof(string));
                var[56] = gb.CreateSPParameter("@alamat_surat", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlAlamatSurat.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("KP_UPDATEDATA_NASABAH", var, "constrmf");

                //ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH_NEW_UPGRADE", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Anggota','top-right','success','2');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
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

    protected void ddlkepemilikantempatusaha_SelectedIndexChanged(object sender, EventArgs e)
{

    if (ddlkepemilikantempatusaha.SelectedValue == "3")
    {
        txtkepemilikantempatusaha.Enabled = true;
            txtkepemilikantempatusaha.Text = "-";
    }
    else
    {
            txtkepemilikantempatusaha.Enabled = false;
            txtkepemilikantempatusaha.Text = "-";
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

  
    public void InsertCombo()
    {
        // --- Combo Status Pernikahan
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatuskawinupload.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatuskawinupload.DataTextField = "Keterangan";
        ddlstatuskawinupload.DataValueField = "id";
        ddlstatuskawinupload.DataBind();






        // --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetNPWP()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlNPWP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlNPWP.DataTextField = "keterangan";
        //ddlNPWP.DataValueField = "id";
        //ddlNPWP.DataBind();
        //ddlNPWP.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Provinsi KTP
        //ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        //ddlprovinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlprovinsiktp.DataTextField = "nama_provinsi";
        //ddlprovinsiktp.DataValueField = "kode_provinsi";
        //ddlprovinsiktp.DataBind();
        ////ddlprovinsiktp.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Provinsi Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsipekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsipekerjaan.DataTextField = "nama_provinsi";
        ddlprovinsipekerjaan.DataValueField = "kode_provinsi";
        ddlprovinsipekerjaan.DataBind();
        ddlprovinsipekerjaan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPekerjaan.DataTextField = "keterangan";
        ddlPekerjaan.DataValueField = "id";
        ddlPekerjaan.DataBind();
        ddlPekerjaan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Kepemilikan Tempat Usaha
        ge.ListDataset = gb.GetDataFunction("dbo.GetKepemilikanTempatUsaha()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlkepemilikantempatusaha.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkepemilikantempatusaha.DataTextField = "keterangan";
        ddlkepemilikantempatusaha.DataValueField = "id";
        ddlkepemilikantempatusaha.DataBind();
        ddlkepemilikantempatusaha.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Alamat Surat
        ge.ListDataset = gb.GetDataFunction("dbo.GetAlamatSurat()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlAlamatSurat.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAlamatSurat.DataTextField = "keterangan";
        ddlAlamatSurat.DataValueField = "id";
        ddlAlamatSurat.DataBind();
        ddlAlamatSurat.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Lama Bekerja
        ge.ListDataset = gb.GetDataFunction("dbo.GetLamaKerja()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlLamaBekerja.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlLamaBekerja.DataTextField = "keterangan";
        ddlLamaBekerja.DataValueField = "id";
        ddlLamaBekerja.DataBind();
        ddlLamaBekerja.Items.Insert(0, new ListItem("Pilih Data", "-"));


        // -- Combo Penghasil Gaji Kotor Per Tahun
        ge.ListDataset = gb.GetDataFunction("dbo.GetPenghasilanKotor()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlgajikotor.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgajikotor.DataTextField = "keterangan";
        ddlgajikotor.DataValueField = "id";
        ddlgajikotor.DataBind();
        ddlgajikotor.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Sumber Penghasilan
        ge.ListDataset = gb.GetDataFunction("dbo.GetSumberPenghasilan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlsumberpenghasilan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberpenghasilan.DataTextField = "keterangan";
        ddlsumberpenghasilan.DataValueField = "id";
        ddlsumberpenghasilan.DataBind();
        ddlsumberpenghasilan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Aktifitas Transaksi Per Hari
        ge.ListDataset = gb.GetDataFunction("dbo.GetTransaksiNormal()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddltransaksiperhari.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltransaksiperhari.DataTextField = "keterangan";
        ddltransaksiperhari.DataValueField = "id";
        ddltransaksiperhari.DataBind();
        ddltransaksiperhari.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Tujuan Pembukaan Rekening
        ge.ListDataset = gb.GetDataFunction("dbo.GetTujuanBukaRekening()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlTujuanbukaRekening.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlTujuanbukaRekening.DataTextField = "keterangan";
        ddlTujuanbukaRekening.DataValueField = "id";
        ddlTujuanbukaRekening.DataBind();
        ddlTujuanbukaRekening.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Dalam Hal Bertindak
        ge.ListDataset = gb.GetDataFunction("dbo.GetTindakan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddldalamhalbertindak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddldalamhalbertindak.DataTextField = "keterangan";
        ddldalamhalbertindak.DataValueField = "id";
        ddldalamhalbertindak.DataBind();
        ddldalamhalbertindak.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // -- Combo Kena Pajak
        ge.ListDataset = gb.GetDataFunction("dbo.GetKenaPajak()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlKenaPajak.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKenaPajak.DataTextField = "keterangan";
        ddlKenaPajak.DataValueField = "id";
        ddlKenaPajak.DataBind();
        ddlKenaPajak.Items.Insert(0, new ListItem("Pilih Data", "-"));


        // COMBO MASTER HUBUNGAN 
        ge.ListDataset = gb.GetDataFunction("dbo.GetHubungan()", "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlHubunganPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

        ddlHubunganPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlHubunganPihakLain.DataTextField = "keterangan";
        ddlHubunganPihakLain.DataValueField = "id";
        ddlHubunganPihakLain.DataBind();
        ddlHubunganPihakLain.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // COMBO PROVINSI 
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlProvinsiPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

        ddlProvinsiPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlProvinsiPihakLain.DataTextField = "nama_provinsi";
        ddlProvinsiPihakLain.DataValueField = "kode_provinsi";
        ddlProvinsiPihakLain.DataBind();
        ddlProvinsiPihakLain.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ddlProvinsiPihakLain.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

        ddlprovinsitpt.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsitpt.DataTextField = "nama_provinsi";
        ddlprovinsitpt.DataValueField = "kode_provinsi";
        ddlprovinsitpt.DataBind();
        ddlprovinsitpt.Items.Insert(0, new ListItem("Pilih Data", "-"));
        


        // --- Combo Status Rumah
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusRumah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatusrumah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatusrumah.DataTextField = "keterangan";
        ddlstatusrumah.DataValueField = "id";
        ddlstatusrumah.DataBind();

        ddlstatusrumah.Items.Insert(0, new ListItem("Pilih Data", "-"));


    }

    protected void ddlstatuskawin_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlstatuskawinupload.SelectedIndex == 0 || ddlstatuskawinupload.SelectedIndex == 2)
        {
            txtidpasangan.Enabled = false;
            txtidpasangan.Text = "-";
            txtnamapasangan.Enabled = false;
            txtnamapasangan.Text = "-";
            txttempatlahirpasangan.Enabled = false;
            txttempatlahirpasangan.Text = "-";
            txttgllahirpasangan.Enabled = false;
            txttgllahirpasangan.Text = "1900-01-01 00:00:00";

        }
        else
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");



            txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
            txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
            txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
            txttgllahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {

                txtidpasangan.Enabled = true;
                txtidpasangan.Text = "-";
                txtnamapasangan.Enabled = true;
                txtnamapasangan.Text = "-";
                txttempatlahirpasangan.Enabled = true;
                txttempatlahirpasangan.Text = "-";
                txttgllahirpasangan.Enabled = true;
                txttgllahirpasangan.Text = "1900-01-01 00:00:00";

            }
            else
            {

                txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                txttgllahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();


                txtidpasangan.Enabled = true;
                txtnamapasangan.Enabled = true;
                txttempatlahirpasangan.Enabled = true;
                txttgllahirpasangan.Enabled = true;

            }

        }

    }
    protected void txtidpasangan_TextChanged(object sender, EventArgs e)
    {
        if (txtidpasangan.Text == "0" || txtidpasangan.Text == "")
        {
            txtnamapasangan.Enabled = false;
            txttempatlahirpasangan.Enabled = false;
            txttgllahirpasangan.Enabled = false;
            //fileuploadktppasangan.Enabled = false;
            //btnuploadktppasangan.Enabled = false;
            //txtppathktppasangan.Enabled = false;
            //txtppathktppasangan.Text = "-";
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
            //fileuploadktppasangan.Enabled = true;
            //btnuploadktppasangan.Enabled = true;
            txttgllahirpasangan.Text = "";
        }
    }


}