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
using System.Activities.Expressions;

public partial class AR_Process_PencairanPinjaman : System.Web.UI.Page
{
    String nosebelumnya = "", updateno = "", CRno_rekening = "", STATE = "", kdpersh = "", format_no_pk = "",
              formatnumber = "", formatpknumber = "", formatnumberbukti = "", nopksebelumnya = "", CRno_pk = "", updatepkno = "", pokok = "", bunga = "",
              typetrn = "", keterangantrn = "", no_bukti_sebelumnya = "", NoBuktiNew = "", DebitGL = "", KreditGL1 = "", KreditGL2 = "", 
              KodeJurnal = "",  KreditGL3 = "", KreditGL4 = "", kodebank = "";
    double ttlbunga = 0, sisapk = 0, ttlpokok = 0, pokokbunga = 0, ttlkeluarcl = 0;
    int counter_no_rekening = 0, i = 0, x = 0;

    long no_pk_sekarang = 0, no_pk = 0, nosekarang = 0, no_rekening = 0,  no_bukti_sekarang = 0, no_bukti = 0;

    DateTime tgl_libur, tgl_realisasi, tgldrop, tgl_angsuran1;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    string id = "", no_nasabah = "", flag = "";

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }

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
        Response.Redirect("PencairanPinjaman.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PencairanPinjaman.aspx");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

    }
    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {

        pnladd.Visible = false;



    }

    protected void btnNextTambahan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
     
    }
    protected void btnNextPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        //pnlLaba.Visible = true;

        pnladd.Visible = false;

    }
    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
      
    }

    protected void btnBackPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;


    }
    protected void btnNextPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;



    }
    protected void btnNextJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;


    }
    protected void btnBackJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
     

    }

    protected void btnNextPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        pnlPengajuan.Visible = false;

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
       
    }

    protected void btnBackTambahan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
  

    }
    protected void btnBackPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

    }

    protected void btnBackPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;


        pnlPengajuan.Visible = false;

    }
    protected void btnBatal_Click(object sender, EventArgs e)
    {
        try
        {
            btnBatal.Enabled = false;

            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[11];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "4", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, "Pembatalan Dari Teller", typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "4", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BATALKANTELLERX", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            var88[9] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
            var88[10] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pencairan Done!','You successfully save','top-right','success','1');", true);
                PnlFront.Visible = true;
                pnladd.Visible = false;
                pnlPengajuan.Visible = false;
                btnSetuju.Enabled = true;
                btnSetuju.Visible = true;
                BindGridEntity();
                txtnonasabahxt.Focus();

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
        try
        {
            int blokbulanan = 0;
            btnSetuju.Enabled = false;



            blokbulanan = 0;

            if (SessBranchID != "608")
            {
                if (HDKodeproduk.Value == "3B" && (lblrekeninglama.Text == "-" || lblrekeninglama.Text == "B"))
                {

                    DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[3];
                    varz[0] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "BULANBLOCK", typeof(string));
                    varz[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    varz[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

                    blokbulanan = int.Parse(ge.ListDataset.Tables[0].Rows[0]["banyak"].ToString());
                    if (blokbulanan >= 50)
                    {
                        blokbulanan = 1;
                    }
                    else
                    {
                        blokbulanan = 0;

                    }



                }
            }

            if (blokbulanan == 1)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota Aktif pada Produk Bulanan tidak boleh melebihi dari 50 anggota');", true);
                btnSetuju.Enabled = true;

            }


            else
            {




                DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
                varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglrealisasi.Text, typeof(DateTime));
                varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
                varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

                string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();
                string SessLoginIDx = SessLoginID.Substring(3, 2).ToUpper();
                if (xsf != "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                    btnSetuju.Enabled = true;
                }
                else if (SessLoginIDx != "TL")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Login bukan user teller / Head teller,logoff dan login kembali');", true);
                    btnSetuju.Enabled = true;
                }
                else
                {



                    //if (txtrekeningcbs.Text.Length <= 1)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masukkan Terlebih Dahulu Rekening CBS');", true);
                    //    btnSetuju.Enabled = true;
                    //    txtrekeningcbs.Focus();
                    //}
                    //else
                    //{

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



                    //btnSetuju.Visible = false;
                    ProsesPencairanKreditx("9", SessLoginID);
                    //}





                }

            }
    }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            btnSetuju.Enabled = true;

        }
    
   }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //GRTP.Enabled = true;     
        BindGridEntity();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            BindGridEntity();
            ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";




            ddlpekerjaanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpekerjaanktp.DataTextField = "keterangan";
            ddlpekerjaanktp.DataValueField = "id";
            ddlpekerjaanktp.DataBind();



     

            




           

            ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

            ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

            ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlpropinsiktp.DataTextField = "nama_provinsi";
            ddlpropinsiktp.DataValueField = "kode_provinsi";
            ddlpropinsiktp.DataBind();


            
           

           

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

            //get tanggal server
            ge.ListDataset = gb.GetDataFunction("dbo.GetSektorUsaha()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_sektor_usaha asc";

            ddlSektorUsahaKtp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlSektorUsahaKtp.DataTextField = "keterangan";
            ddlSektorUsahaKtp.DataValueField = "kode_sektor_usaha";
            ddlSektorUsahaKtp.DataBind();


        }


    }
    
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        pnlFilter.Visible = false;
        switch (e.CommandName)
        {
            case "SET":

                try

                {




                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlPengajuan.Visible = true;
                    txtrekeningcbs.Text = "";

                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDKodeproduk.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text;
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    txtnorekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    txtpkredit.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopk")).Text;

                    if (SessBranchID == "305")
                    {
                        discountmingguan.Visible = true;
                        txtdiscountangsuran.Enabled = true;
                        txtdiscountangsuran.Text = string.Format("{0:N0}", "0");
                    }
                    else
                    {
                        discountmingguan.Visible = false;
                        txtdiscountangsuran.Enabled = false;
                        txtdiscountangsuran.Text = string.Format("{0:N0}", "0");
                    }

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

                    //data ktp
                    ddljenispermohonan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenispermohonan"].ToString();
                    ddlkelompokAgunan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kelompokagunan"].ToString();
                    ddltujuanpinjaman.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["tujuanpinjaman"].ToString();

                    ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                    txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();


                    txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                    ddlbycad.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["cadangan"].ToString();
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
                    txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                    txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));
                    ddljenistagihan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenistagihan"].ToString();
                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    LblTypebiaya.Text = ge.ListDataset.Tables[0].Rows[0]["Typebiayakeanggotaan"].ToString();
                    LblrekeningCadangan.Text = ge.ListDataset.Tables[0].Rows[0]["Rekeninglamacadangan"].ToString();
                    LblTglhariTagih.Text = ge.ListDataset.Tables[0].Rows[0]["tgltagihhari"].ToString();
                    lblsaldosimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                    lblsaldoSimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));
                    if (LblTypebiaya.Text == "0")
                    {
                        LblnamaTypebiaya.Text = "Sudah Menjadi Anggota";
                    }
                    else if (LblTypebiaya.Text == "1")
                    {
                        LblnamaTypebiaya.Text = "Anggota Bayar";
                    }
                    else if (LblTypebiaya.Text == "2")
                    {
                        LblnamaTypebiaya.Text = "Cabang Bayar";
                    }

                    txtdiscountangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["discountangsuran"].ToString()));


                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();

                   

                  
                 
                    ddlPenggunaan.SelectedValue = "10";




                    //txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    //ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jaminan_tambahan"].ToString();
                    //txttaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi"].ToString();
                    txttaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                    ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                    //Panel Data pengajuan kredit nasabah
                    txtnoreklama.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    txtjeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                    //txtjenispenggunaan.Text = ge.ListDataset.Tables[0].Rows[0]["penggunaan"].ToString();
                    txttglrealisasi.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString();
                    txtjenispinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString(); ;
                    // = ge.ListDataset.Tables[0].Rows[0]["ketprogram"].ToString();
                    txtsumberdana.Text = ge.ListDataset.Tables[0].Rows[0]["kdpersh1"].ToString();
                    txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["permohonan_bendahara"].ToString();
                    txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                    txtbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    txtProvisi.Text = ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString();
                    txtnominalprovisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString();
                    txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString();
                    txtadminPencairan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_administrasi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_administrasi"].ToString();
                    txtadminangsuran.Text = string.Format("{0:N2}", decimal.Parse("0")); //ge.ListDataset.Tables[0].Rows[0]["biaya_angsuran1"].ToString();
                    txtsifat.Text = "06";
                    txtAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txtNonAO.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    txttablain.Text = "-";
                    txtsektorekonomi.Text = "6000";
                    ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                    lblrekeningperpanjangan.Text = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();
                    lbltipe.Text = ddlpengajuan.SelectedItem.Text;
                    lblrekeninglama.Text = lblrekeningperpanjangan.Text;
                    
                    lblcadangansebelumnya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                    TextBox3.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString();
                    TextBox5.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["nilai_taksasi"].ToString();
                    txtjttempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tgl_jatuh_tempo"].ToString();
                    txtangsurandimuka.Text = string.Format("{0:N2}", decimal.Parse("0")); //ge.ListDataset.Tables[0].Rows[0]["angsuran_dimuka"].ToString();
                    txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString();
                    txtbyasuransi.Text = string.Format("{0:N2}", decimal.Parse("0")); //ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString();
                    txtbypelunasan.Text = string.Format("{0:N2}", decimal.Parse("0")); //ge.ListDataset.Tables[0].Rows[0]["biaya_pelunasan"].ToString();
                    txtbynotaris.Text = string.Format("{0:N2}", decimal.Parse("0")); // ge.ListDataset.Tables[0].Rows[0]["notaris"].ToString();
                    TextBox14.Text = string.Format("{0:N2}", decimal.Parse("0")); //ge.ListDataset.Tables[0].Rows[0]["blokir_angsuran"].ToString();
                    TextBox15.Text = string.Format("{0:N2}", decimal.Parse("0")); //ge.ListDataset.Tables[0].Rows[0]["nominal_blokir_angsuran"].ToString();
                    txtjenisblokir.Text = "0";


                    HDKodePos.Value = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    HDKodeArea.Value = "I";
                    HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    //HDLamaKerja.Value = ge.ListDataset.Tables[0].Rows[0]["lama_bekerja"].ToString();

                  
                        txtanggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        txtsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayasimpananwajib"].ToString()));

                    lblNoAnggota.Text = txtnoanggota.Text;
                    lblNama.Text = txtnamanasabah.Text;
                    lblrekpinjaman.Text = txtnorekening.Text;
                    lblpkkredit.Text = txtpkredit.Text;
                    lblNama.Text = txtnamanasabah.Text;
                    
                    lbljeniskredit.Text = txtjeniskredit.Text;
                    lblao.Text = txtAO.Text;
                    lbltglRealisasi.Text = txttglrealisasi.Text;
                    lbljangkawaktu.Text = txtjangka.Text;
                    lbljthtempo.Text = txtjttempo.Text;
                    lblbunga.Text = txtbunga.Text;
                    lblmajuhari.Text = ddlmajuhari.SelectedItem.Text;
                    lblbesarpinjaman.Text = txtplafon.Text;
                    lblangsuranbulan.Text = txtangsuranperbulan.Text;
                    lbladministrasi.Text = txtnominalprovisi.Text;
                    lblmaterai.Text = txtbymaterai.Text;
                    LblPotonganangsuran.Text = TextBox3.Text;
                    LblAnggota.Text = txtanggota.Text;
                    lblsimpananwajib.Text = txtsimpananwajib.Text;
                    lbluangmuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayauangmuka"].ToString()));
                    lblTotalPotongbiaya.Text = string.Format("{0:N2}", decimal.Parse(txtnominalprovisi.Text) + decimal.Parse(txtbymaterai.Text) + decimal.Parse(LblPotonganangsuran.Text) + decimal.Parse(LblAnggota.Text) + decimal.Parse(lbluangmuka.Text) + decimal.Parse(txtsimpananwajib.Text));
                    lblygditerimaanggota.Text = string.Format("{0:N2}", decimal.Parse(txtplafon.Text) - decimal.Parse(lblTotalPotongbiaya.Text));
                    lblkembalicadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pengembaliancadangan"].ToString()));
                    if (HDKodeproduk.Value == "3C")
                    {
                        lblangsuran.Text = "Angsuran Per-Minggu";
                        txtprogrampinjaman.Text = "6";
                    }
                    else
                    {
                        lblangsuran.Text = "Angsuran Per-Bulan";
                        txtprogrampinjaman.Text = "5";
                    }


                    DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[1];
                    varT[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("gettglsekarangXYZ", varT, "constrmf");
                    string xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString()).ToString("dd/MM/yyyy");
                    string tanggalrealisasi = DateTime.Parse(txttglrealisasi.Text).ToString("dd/MM/yyyy");

                    btnSetuju.Enabled = true;
                    btnBatal.Enabled = true;
                    if (txttglrealisasi.Text != xtanggal)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi (Kartu Angsuran) Berbeda dengan Tanggal Pencairan (Tgl Sekarang), lakukan perubahan terlebih dahulu di admin, klik tombol batalkan antrian Teller untuk kembali ke posisi interview admin ');", true);
                        btnSetuju.Enabled = false;
                    }

                    ddljenispermohonan.Enabled = false;


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
        }
    }

    //public void ProsesPencairanKredit(string flag, string user)
    //{

    //    try

    //    {


    //        decimal cadangan = 0, cadangankedua = 0, bungamasterkredit = 0;
    //        string bng = "";

    //        DefaultClass.Global.SqlHelper[] varcad = new DefaultClass.Global.SqlHelper[3];
    //        varcad[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
    //        varcad[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCADANGAN", typeof(string));
    //        varcad[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varcad, "constrmf");


    //        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)

    //        {
    //            cadangan = 0;
    //            CRno_rekening = "-";
    //        }
    //        else
    //        {

    //            cadangan = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisacadangan"].ToString());
    //            CRno_rekening = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();

    //        }


    //        cadangankedua = cadangan + decimal.Parse(TextBox3.Text);

    //        //Select TypeTrn TO TABLE CL_AR_JURNAL
    //        DefaultClass.Global.SqlHelper[] varartrnget = new DefaultClass.Global.SqlHelper[4];
    //        varartrnget[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        varartrnget[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        varartrnget[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORARTRNINSERT", typeof(string));
    //        varartrnget[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", varartrnget, "constrmf");

    //        KodeJurnal = ge.ListDataset.Tables[0].Rows[0]["CL_AR_CAIR_JURNAL"].ToString();
    //        //-----------------------------------------------------------------------------INSERT ArMasterKredit----------------------------------------------------------------------------------

    //        btnSetuju.Enabled = false;
    //        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[72];
             
    //        bng = gb.GetNominalBunga(Double.Parse(txtbunga.Text.Trim().Replace(".", ",")), int.Parse(txtjangka.Text), double.Parse(txtplafon.Text), txtjeniskredit.Text);

    //        bungamasterkredit = int.Parse(bng) * int.Parse(txtjangka.Text);

    //        var1[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        var1[1] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
    //        var1[2] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjeniskredit.Text, typeof(string));
    //        var1[3] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlPenggunaan.SelectedItem.Text, typeof(string));
    //        var1[4] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        var1[5] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, HDnopk.Value, typeof(string));
    //        var1[6] = gb.CreateSPParameter("@tgl_drop", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        var1[7] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
    //        var1[8] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjangka.Text.Trim(), typeof(string));
    //        var1[9] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 250, txtplafon.Text.Trim(), typeof(string));
    //        var1[10] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbunga.Text.Trim(), typeof(string)); //
    //        var1[11] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.VarChar, 250, txtbymaterai.Text.Trim(), typeof(string));
    //        var1[12] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.Decimal, 250, txttaksasi.Text, typeof(string));
    //        var1[13] = gb.CreateSPParameter("@kd_area", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeArea.Value, typeof(string));
    //        var1[14] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
    //        var1[15] = gb.CreateSPParameter("@sifat", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsifat.Text, typeof(string));
    //        var1[16] = gb.CreateSPParameter("@sek_ekon", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsektorekonomi.Text, typeof(string));
    //        var1[17] = gb.CreateSPParameter("@kd_goldb", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlgolongandebitur.SelectedItem.Text, typeof(string));
    //        var1[18] = gb.CreateSPParameter("@nm_pnjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapenjamin.Text, typeof(string));
    //        var1[19] = gb.CreateSPParameter("@golpjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlgolonganpenjamin.SelectedItem.Text, typeof(string));
    //        var1[20] = gb.CreateSPParameter("@nl_jmn", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string));
    //        var1[21] = gb.CreateSPParameter("@kd_fisik", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[22] = gb.CreateSPParameter("@ttdjmn", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        var1[23] = gb.CreateSPParameter("@ket_jmn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketeranganJaminan.Text, typeof(string));
    //        var1[24] = gb.CreateSPParameter("@kd_ktr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string)); //
    //        var1[25] = gb.CreateSPParameter("@ttlpokok", ParameterDirection.Input, SqlDbType.Decimal, 250, txtplafon.Text.Trim(), typeof(string));
    //        var1[26] = gb.CreateSPParameter("@ttlbunga", ParameterDirection.Input, SqlDbType.Decimal, 250, bungamasterkredit, typeof(string));
    //        var1[27] = gb.CreateSPParameter("@angspokok", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string));
    //        var1[28] = gb.CreateSPParameter("@angsbunga", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string));
    //        var1[29] = gb.CreateSPParameter("@bakidb", ParameterDirection.Input, SqlDbType.Decimal, 250, txtplafon.Text.Trim(), typeof(string));
    //        var1[30] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 50, "0", typeof(string));
    //        var1[31] = gb.CreateSPParameter("@picb", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamanasabah.Text, typeof(string));
    //        var1[32] = gb.CreateSPParameter("@gender", ParameterDirection.Input, SqlDbType.VarChar, 250, HDGender.Value, typeof(string));
    //        var1[33] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsumberdana.Text, typeof(string));
    //        var1[34] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
    //        var1[35] = gb.CreateSPParameter("@bgc", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        var1[36] = gb.CreateSPParameter("@no_bukti", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[37] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtprogrampinjaman.Text, typeof(string));
    //        var1[38] = gb.CreateSPParameter("@kol", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string));
    //        var1[39] = gb.CreateSPParameter("@arrear", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        var1[40] = gb.CreateSPParameter("@cadangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        var1[41] = gb.CreateSPParameter("@nmkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNamaKeluarga.Value, typeof(string));
    //        var1[42] = gb.CreateSPParameter("@stskel", ParameterDirection.Input, SqlDbType.VarChar, 50, HDHubunganKeluarga.Value, typeof(string));
    //        var1[43] = gb.CreateSPParameter("@pkjkel", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[44] = gb.CreateSPParameter("@almkel", ParameterDirection.Input, SqlDbType.VarChar, 50, HDAlamatKeluarga.Value, typeof(string));
    //        var1[45] = gb.CreateSPParameter("@klrkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKelurahanKeluarga.Value, typeof(string));
    //        var1[46] = gb.CreateSPParameter("@keckel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKecamatanKeluarga.Value, typeof(string));
    //        var1[47] = gb.CreateSPParameter("@kotkel", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKotaKeluarga.Value.Trim(), typeof(string));
    //        var1[48] = gb.CreateSPParameter("@nmpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamapenjamin.Text, typeof(string));
    //        var1[49] = gb.CreateSPParameter("@almpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[50] = gb.CreateSPParameter("@klrpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[51] = gb.CreateSPParameter("@kecpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[52] = gb.CreateSPParameter("@kotpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[53] = gb.CreateSPParameter("@pkjpjmn1", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[54] = gb.CreateSPParameter("@tmp_lahir", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[55] = gb.CreateSPParameter("@tgl_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //        var1[56] = gb.CreateSPParameter("@tglproses", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //        var1[57] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        var1[58] = gb.CreateSPParameter("@status_pelunasan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        var1[59] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjenispinjaman.Text, typeof(string));// 
    //        var1[60] = gb.CreateSPParameter("@sandi_agunan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[61] = gb.CreateSPParameter("@dati2", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //        var1[62] = gb.CreateSPParameter("@input_pembiayaan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string)); 
    //        var1[63] = gb.CreateSPParameter("@jns_blokir", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        var1[64] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string)); 
    //        var1[65] = gb.CreateSPParameter("@id_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtktppenjamin.Text, typeof(string));
    //        var1[66] = gb.CreateSPParameter("@by_cadangan", ParameterDirection.Input, SqlDbType.Decimal, 50, cadangankedua, typeof(string));
    //        var1[67] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
    //        var1[68] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        var1[69] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        var1[70] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        var1[71] = gb.CreateSPParameter("@no_rek_tabungan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorektabungan.Text, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var1, "constrmf");


    //        for (i = 0; i < int.Parse(txtjangka.Text.Trim()); i++)
    //        {
    //            if (i == 0)
    //            {
    //                tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
    //                tgl_libur = tgl_realisasi.AddDays(7);
    //                tgl_angsuran1 = tgl_libur;
    //            }
    //            else
    //            {
    //                tgl_libur = tgldrop.AddDays(7);
    //            }

    //            //-----------------------------------------------------------------------------Cek Tanggal Libur----------------------------------------------------------------------------------

    //            DefaultClass.Global.SqlHelper[] var9 = new DefaultClass.Global.SqlHelper[3];
    //            var9[0] = gb.CreateSPParameter("@tglLibur", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tgl_libur, typeof(DateTime));
    //            var9[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
    //            var9[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //            ge.ListDataset = gb.GetDataJustOutPut("GetTglLibur", var9, "constrmf");

    //            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
    //            {
    //                tgldrop = tgl_libur;

    //            }
    //            else
    //            {
    //                tgldrop = tgl_libur.AddDays(7);

    //            }

    //            x = i + 1;

    //            pokok = gb.GetPokok(int.Parse(txtjangka.Text), double.Parse(txtplafon.Text));
    //            bunga = gb.GetNominalBunga(Double.Parse(txtbunga.Text.Trim().Replace(".", ",")), int.Parse(txtjangka.Text), double.Parse(txtplafon.Text), txtjeniskredit.Text);

    //            double pembulatanbunga = double.Parse(bunga);
    //            pembulatanbunga = Math.Round(pembulatanbunga);

    //            if (i == 0)
    //            {
    //                sisapk = double.Parse(txtplafon.Text);
    //            }
    //            else
    //            {
    //                sisapk = double.Parse(txtplafon.Text) - ((x - 1) * double.Parse(pokok));
    //            }
    //            //-----------------------------------------------------------------------------INSERT ArKartuPinjaman----------------------------------------------------------------------------------

    //            DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[55];
    //            var4[0] = gb.CreateSPParameter("@status_pembayaran", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //            var4[2] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //            var4[3] = gb.CreateSPParameter("@no_rekening_lama", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
    //            var4[4] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, txtjeniskredit.Text, typeof(string));
    //            var4[5] = gb.CreateSPParameter("@angs_ke", ParameterDirection.Input, SqlDbType.VarChar, 250, x, typeof(string));
    //            var4[6] = gb.CreateSPParameter("@tgl_jwl", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgldrop, typeof(string));
    //            var4[7] = gb.CreateSPParameter("@pokok", ParameterDirection.Input, SqlDbType.Decimal, 250, pokok, typeof(string));
    //            var4[8] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Decimal, 250, bunga, typeof(string));
    //            var4[9] = gb.CreateSPParameter("@tgl_angs", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //            var4[10] = gb.CreateSPParameter("@pkangs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[11] = gb.CreateSPParameter("@bng_angs", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[12] = gb.CreateSPParameter("@denda", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[13] = gb.CreateSPParameter("@tgl", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //            var4[14] = gb.CreateSPParameter("@tgl_prov", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //            var4[15] = gb.CreateSPParameter("@provisi", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[16] = gb.CreateSPParameter("@arrear", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[17] = gb.CreateSPParameter("@arrrate", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[18] = gb.CreateSPParameter("@trtpk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[19] = gb.CreateSPParameter("@trtbng", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[20] = gb.CreateSPParameter("@sisapk", ParameterDirection.Input, SqlDbType.Decimal, 250, sisapk, typeof(string));
    //            var4[21] = gb.CreateSPParameter("@sisabng", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[22] = gb.CreateSPParameter("@postgk", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[23] = gb.CreateSPParameter("@pkbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[24] = gb.CreateSPParameter("@bgbulanan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[25] = gb.CreateSPParameter("@pkbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[26] = gb.CreateSPParameter("@bngbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[27] = gb.CreateSPParameter("@pkbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[28] = gb.CreateSPParameter("@bngbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[29] = gb.CreateSPParameter("@angspbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[30] = gb.CreateSPParameter("@angsbbpr", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[31] = gb.CreateSPParameter("@angspbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[32] = gb.CreateSPParameter("@angsbbank", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[33] = gb.CreateSPParameter("@pcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[34] = gb.CreateSPParameter("@bcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[35] = gb.CreateSPParameter("@tgl_cof", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //            var4[36] = gb.CreateSPParameter("@angspcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[37] = gb.CreateSPParameter("@angsbcof", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[38] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[39] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[40] = gb.CreateSPParameter("@rekadm", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[41] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
    //            var4[42] = gb.CreateSPParameter("@tglupd", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgldrop, typeof(string));
    //            var4[43] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //            var4[44] = gb.CreateSPParameter("@tgl_jwl_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //            var4[45] = gb.CreateSPParameter("@tgl_angs_lama", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //            var4[46] = gb.CreateSPParameter("@arrear_lama ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[47] = gb.CreateSPParameter("@status_channeling ", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[48] = gb.CreateSPParameter("@status_pembayaran_channeling", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[49] = gb.CreateSPParameter("@tgl_angsuran_bank", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, "1900-01-01 00:00:00", typeof(string));
    //            var4[50] = gb.CreateSPParameter("@no_tagihan", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            var4[51] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTKARTUPINJAMAN", typeof(string));
    //            var4[52] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //            var4[53] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //            var4[54] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var4, "constrmf");

               

    //        }

    //        ttlbunga = double.Parse(txtjangka.Text) * double.Parse(bunga);
    //        ttlpokok = double.Parse(txtjangka.Text) * double.Parse(pokok); ;
    //        pokokbunga = ttlbunga + ttlpokok; 

    //        if (cadangan.ToString() != "0")
    //        { 
                 
    //            DefaultClass.Global.SqlHelper[] aa = new DefaultClass.Global.SqlHelper[5];
    //            aa[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string)); 
    //            aa[1] = gb.CreateSPParameter("@cadangan", ParameterDirection.Input, SqlDbType.Decimal, 50, cadangan, typeof(string));
    //            aa[2] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, CRno_rekening, typeof(string));
    //            aa[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEBYCADANGAN", typeof(string));
    //            aa[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", aa, "constrmf");

    //            //INSERT CADANGAN
    //            DefaultClass.Global.SqlHelper[] vartrn5 = new DefaultClass.Global.SqlHelper[17];
    //            vartrn5[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //            vartrn5[1] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgl_angsuran1, typeof(string));
    //            vartrn5[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "T", typeof(string));
    //            vartrn5[3] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
    //            vartrn5[4] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Cadangan", typeof(string));
    //            vartrn5[5] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //            vartrn5[6] = gb.CreateSPParameter("@kodecabang ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
    //            vartrn5[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //            vartrn5[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTJURNALBALIK", typeof(string));
    //            vartrn5[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //            vartrn5[10] = gb.CreateSPParameter("@TglJtpFaktur", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgl_angsuran1, typeof(string));
    //            vartrn5[11] = gb.CreateSPParameter("@pokok", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string));
    //            vartrn5[12] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string));
    //            vartrn5[13] = gb.CreateSPParameter("@UangMukaFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            vartrn5[14] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, KodeJurnal, typeof(string));
    //            vartrn5[15] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //            vartrn5[16] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeproduk.Value, typeof(string));

                
    //            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", vartrn5, "constrmf");


    //        }

    //        //-----------------------------------------------------------------------------UPDATE ArMasterKredit----------------------------------------------------------------------------------

    //        DefaultClass.Global.SqlHelper[] var00 = new DefaultClass.Global.SqlHelper[12];
    //        var00[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "9", typeof(string));
    //        var00[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
    //        var00[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
    //        var00[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "9", typeof(string));
    //        var00[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
    //        var00[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
    //        var00[6] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
    //        var00[7] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        var00[8] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, HDno_rekening.Value, typeof(string));
    //        var00[9] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh , typeof(string));
    //        var00[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVALSTATUS", typeof(string));
    //        var00[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var00, "constrmf");

    //        //-----------------------------------------------------------------------------UPDATE ARMST----------------------------------------------------------------------------------

    //        //UPDATE Total Pencairan dan cadangan ke ARMST
    //        DefaultClass.Global.SqlHelper[] varMst = new DefaultClass.Global.SqlHelper[7];
    //        varMst[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
    //        varMst[1] = gb.CreateSPParameter("@totalpokok", ParameterDirection.Input, SqlDbType.Decimal, 50, ttlpokok, typeof(string));
    //        varMst[2] = gb.CreateSPParameter("@totalbunga", ParameterDirection.Input, SqlDbType.Decimal, 250, ttlbunga, typeof(string));
    //        varMst[3] = gb.CreateSPParameter("@pokokbunga", ParameterDirection.Input, SqlDbType.Decimal, 250, pokokbunga, typeof(string));
    //        varMst[4] = gb.CreateSPParameter("@cadangan", ParameterDirection.Input, SqlDbType.Decimal, 250, TextBox3.Text, typeof(string));
    //        varMst[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEFORPENCAIRAN", typeof(string));
    //        varMst[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varMst, "constrmf");

    //        //-----------------------------------------------------------------------------INSERT ARTRN----------------------------------------------------------------------------------
            
        

    //        //INSERT PINJAMAN KREDIT
    //        DefaultClass.Global.SqlHelper[] vartrn2 = new DefaultClass.Global.SqlHelper[18];
    //        vartrn2[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        vartrn2[1] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgl_angsuran1, typeof(string));
    //        vartrn2[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //        vartrn2[3] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
    //        vartrn2[4] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pinjaman Kredit", typeof(string));
    //        vartrn2[5] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + + pokokbunga, typeof(string));
    //        vartrn2[6] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        vartrn2[7] = gb.CreateSPParameter("@kodecabang ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
    //        vartrn2[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        vartrn2[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPENCAIRAN", typeof(string));
    //        vartrn2[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        vartrn2[11] = gb.CreateSPParameter("@TglJtpFaktur", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgl_angsuran1, typeof(string));
    //        vartrn2[12] = gb.CreateSPParameter("@pokok", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + + ttlpokok, typeof(string));
    //        vartrn2[13] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + + ttlbunga, typeof(string));
    //        vartrn2[14] = gb.CreateSPParameter("@UangMukaFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        vartrn2[15] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, KodeJurnal, typeof(string));
    //        vartrn2[16] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "0", typeof(string));
    //        vartrn2[17] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeproduk.Value, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", vartrn2, "constrmf"); 

    //        //INSERT CADANGAN
    //        DefaultClass.Global.SqlHelper[] vartrn3 = new DefaultClass.Global.SqlHelper[18];
    //        vartrn3[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        vartrn3[1] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgl_angsuran1, typeof(string));
    //        vartrn3[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "U", typeof(string));
    //        vartrn3[3] = gb.CreateSPParameter("@KodeSales", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
    //        vartrn3[4] = gb.CreateSPParameter("@Keterangan ", ParameterDirection.Input, SqlDbType.VarChar, 250, "Cadangan", typeof(string));
    //        vartrn3[5] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Decimal, 250, cadangankedua, typeof(string));
    //        vartrn3[6] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        vartrn3[7] = gb.CreateSPParameter("@kodecabang ", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
    //        vartrn3[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        vartrn3[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPENCAIRAN", typeof(string));
    //        vartrn3[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        vartrn3[11] = gb.CreateSPParameter("@TglJtpFaktur", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, tgl_angsuran1, typeof(string)); 
    //        vartrn3[12] = gb.CreateSPParameter("@UangMukaFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "1", typeof(string));
    //        vartrn3[13] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, KodeJurnal, typeof(string));
    //        vartrn3[14] = gb.CreateSPParameter("@TFlag", ParameterDirection.Input, SqlDbType.VarChar, 250, "2", typeof(string));
    //        vartrn3[15] = gb.CreateSPParameter("@pokok", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + +ttlpokok, typeof(string));
    //        vartrn3[16] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + +ttlbunga, typeof(string));
    //        vartrn3[17] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 250, HDKodeproduk.Value, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", vartrn3, "constrmf");

           

    //        //-----------------------------------------------------------------------------INSERT GLTRNSM----------------------------------------------------------------------------------


    //        //Select TypeTrn TO TABLE suTypeMikro
    //        DefaultClass.Global.SqlHelper[] varsutype = new DefaultClass.Global.SqlHelper[2]; 
    //        varsutype[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORJURNALPENCAIRAN", typeof(string));
    //        varsutype[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varsutype, "constrmf");


    //        DebitGL = ge.ListDataset.Tables[0].Rows[0]["debit1"].ToString();
    //        KreditGL1 = ge.ListDataset.Tables[0].Rows[0]["kredit1"].ToString();
    //        KreditGL2 = ge.ListDataset.Tables[0].Rows[0]["kredit2"].ToString();
    //        KreditGL3 = ge.ListDataset.Tables[0].Rows[0]["kredit3"].ToString();
    //        KreditGL4 = ge.ListDataset.Tables[0].Rows[0]["kredit4"].ToString();


    //        //INSERT INTO gltrnsm DEBIT PLAFON
    //        DefaultClass.Global.SqlHelper[] vartrnsm = new DefaultClass.Global.SqlHelper[16];
    //        vartrnsm[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
    //        vartrnsm[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        vartrnsm[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, DebitGL, typeof(string));
    //        vartrnsm[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //        vartrnsm[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Nilai Plafon Pinjaman", typeof(string));
    //        vartrnsm[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Decimal, 250, txtplafon.Text.Trim(), typeof(string)); //jmldebit
    //        vartrnsm[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string)); //jmlkredit 
    //        vartrnsm[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        vartrnsm[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pencairan Pinjaman dengan Plafon : " + txtplafon.Text, typeof(string));
    //        vartrnsm[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
    //        vartrnsm[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        vartrnsm[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        vartrnsm[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        vartrnsm[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        vartrnsm[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        vartrnsm[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));


    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", vartrnsm, "constrmf");

    //        //INSERT INTO gltrnsm KREDIT Provisi
    //        DefaultClass.Global.SqlHelper[] varprovisi = new DefaultClass.Global.SqlHelper[16];
    //        varprovisi[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
    //        varprovisi[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        varprovisi[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, KreditGL1, typeof(string));
    //        varprovisi[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //        varprovisi[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Nilai Biaya Admin Provisi 3%", typeof(string));
    //        varprovisi[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string)); //jmldebit
    //        varprovisi[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Decimal, 250, txtnominalprovisi.Text.Trim(), typeof(string)); //jmlkredit 
    //        varprovisi[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        varprovisi[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pencairan Pinjaman dengan Plafon : " + txtplafon.Text, typeof(string));
    //        varprovisi[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
    //        varprovisi[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        varprovisi[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varprovisi[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        varprovisi[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varprovisi[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        varprovisi[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
             

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varprovisi, "constrmf");

    //        //INSERT INTO gltrnsm KREDIT Materai
    //        DefaultClass.Global.SqlHelper[] varmaterai = new DefaultClass.Global.SqlHelper[16];
    //        varmaterai[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
    //        varmaterai[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        varmaterai[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, KreditGL2, typeof(string));
    //        varmaterai[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //        varmaterai[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Biaya Materai", typeof(string));
    //        varmaterai[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string)); //jmldebit
    //        varmaterai[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Decimal, 250, txtbymaterai.Text.Trim(), typeof(string)); //jmlkredit 
    //        varmaterai[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        varmaterai[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pencairan Pinjaman dengan Plafon : " + txtplafon.Text, typeof(string));
    //        varmaterai[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
    //        varmaterai[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        varmaterai[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varmaterai[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        varmaterai[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varmaterai[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        varmaterai[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
             

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varmaterai, "constrmf");

    //        if(TextBox3.Text != "0")
    //        {
    //            //INSERT INTO gltrnsm KREDIT Biaya Cadangan Angsuran 

    //            DefaultClass.Global.SqlHelper[] varcadangan = new DefaultClass.Global.SqlHelper[16];
    //            varcadangan[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
    //            varcadangan[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //            varcadangan[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, KreditGL3, typeof(string));
    //            varcadangan[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //            varcadangan[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Cadangan Angsuran", typeof(string));
    //            varcadangan[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string)); //jmldebit
    //            varcadangan[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Decimal, 250, TextBox3.Text, typeof(string)); //jmlkredit 
    //            varcadangan[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //            varcadangan[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pencairan Pinjaman dengan Plafon : " + txtplafon.Text, typeof(string));
    //            varcadangan[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
    //            varcadangan[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //            varcadangan[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //            varcadangan[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //            varcadangan[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //            varcadangan[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //            varcadangan[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));


    //            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varcadangan, "constrmf");


    //        } 

    //        //INSERT INTO gltrnsm KREDIT KAS
    //        double kas = 0;
    //        kas = double.Parse(txtplafon.Text) - double.Parse(txtnominalprovisi.Text) - double.Parse(txtbymaterai.Text) - double.Parse(TextBox3.Text);

    //        DefaultClass.Global.SqlHelper[] varkas = new DefaultClass.Global.SqlHelper[16];
    //        varkas[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 250, HDNoBuktiSekarang.Value, typeof(string));
    //        varkas[1] = gb.CreateSPParameter("@TglTrns", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        varkas[2] = gb.CreateSPParameter("@kodeGL", ParameterDirection.Input, SqlDbType.VarChar, 250, KreditGL4, typeof(string));
    //        varkas[3] = gb.CreateSPParameter("@TypeTrans", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //        varkas[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "KAS", typeof(string));
    //        varkas[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Decimal, 250, "0", typeof(string)); //jmldebit
    //        varkas[6] = gb.CreateSPParameter("@JlhKredit", ParameterDirection.Input, SqlDbType.Decimal, 250, kas, typeof(string)); //jmlkredit 
    //        varkas[7] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        varkas[8] = gb.CreateSPParameter("@KeteranganMemorial", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pencairan Pinjaman dengan Plafon : " + txtplafon.Text, typeof(string));
    //        varkas[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "INSERTAPPROVALTOGLTRN", typeof(string));
    //        varkas[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        varkas[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varkas[12] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        varkas[13] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varkas[14] = gb.CreateSPParameter("@TglEntry", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, DateTime.Now, typeof(string));
    //        varkas[15] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
             
    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varkas, "constrmf");

    //        //-----------------------------------------------------------------------------INSERT CLTRN----------------------------------------------------------------------------------

    //        //Select KodeBank from Clmst
    //        DefaultClass.Global.SqlHelper[] varclmst = new DefaultClass.Global.SqlHelper[5];
    //        varclmst[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        varclmst[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        varclmst[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, KreditGL4, typeof(string));
    //        varclmst[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEJURNALCLMST", typeof(string));
    //        varclmst[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", varclmst, "constrmf");


    //        kodebank = ge.ListDataset.Tables[0].Rows[0]["KodeBank"].ToString();

    //        ttlkeluarcl = ttlpokok - double.Parse(TextBox3.Text.Replace(".", ""));
    //        //INSERT CLTRN KELUAR
    //        DefaultClass.Global.SqlHelper[] varclmst2 = new DefaultClass.Global.SqlHelper[21];
    //        varclmst2[0] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 250, kodebank, typeof(string));
    //        varclmst2[1] = gb.CreateSPParameter("@TglEfektif", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        varclmst2[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "K", typeof(string));
    //        varclmst2[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        varclmst2[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string)); 
    //        varclmst2[5] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        varclmst2[6] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        varclmst2[7] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
    //        varclmst2[8] = gb.CreateSPParameter("@Statusgiro", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //        varclmst2[9] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        varclmst2[10] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Pencairan Pinjaman", typeof(string));
    //        varclmst2[11] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + + ttlpokok, typeof(string));
    //        varclmst2[12] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 250, "MK", typeof(string));
    //        varclmst2[13] = gb.CreateSPParameter("@TypeBayar ", ParameterDirection.Input, SqlDbType.VarChar, 250, "AR", typeof(string));
    //        varclmst2[14] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, KodeJurnal, typeof(string));
    //        varclmst2[15] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varclmst2[16] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 250, "IDR", typeof(string));
    //        varclmst2[17] = gb.CreateSPParameter("@JumlahBayar", ParameterDirection.Input, SqlDbType.Decimal, 250, "-" + + ttlpokok, typeof(string));
    //        varclmst2[18] = gb.CreateSPParameter("@OptBayar", ParameterDirection.Input, SqlDbType.VarChar, 250, "2", typeof(string));
    //        varclmst2[19] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTCL", typeof(string));
    //        varclmst2[20] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varclmst2, "constrmf");

    //        //INSERT CLTRN TERIMA
    //        DefaultClass.Global.SqlHelper[] varclmst3 = new DefaultClass.Global.SqlHelper[21];
    //        varclmst3[0] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 250, kodebank, typeof(string));
    //        varclmst3[1] = gb.CreateSPParameter("@TglEfektif", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        varclmst3[2] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 250, "B", typeof(string));
    //        varclmst3[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        varclmst3[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
    //        varclmst3[5] = gb.CreateSPParameter("@NoFaktur", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        varclmst3[6] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txttglrealisasi.Text, typeof(string));
    //        varclmst3[7] = gb.CreateSPParameter("@TglJthTmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
    //        varclmst3[8] = gb.CreateSPParameter("@Statusgiro", ParameterDirection.Input, SqlDbType.VarChar, 250, "C", typeof(string));
    //        varclmst3[9] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnoanggota.Text, typeof(string));
    //        varclmst3[10] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "Terima Cadangan", typeof(string));
    //        varclmst3[11] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Decimal, 250,  TextBox3.Text, typeof(string));
    //        varclmst3[12] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 250, "MK", typeof(string));
    //        varclmst3[13] = gb.CreateSPParameter("@TypeBayar ", ParameterDirection.Input, SqlDbType.VarChar, 250, "AR", typeof(string));
    //        varclmst3[14] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 250, KodeJurnal, typeof(string));
    //        varclmst3[15] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
    //        varclmst3[16] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 250, "IDR", typeof(string));
    //        varclmst3[17] = gb.CreateSPParameter("@JumlahBayar", ParameterDirection.Input, SqlDbType.Decimal, 250, TextBox3.Text, typeof(string));
    //        varclmst3[18] = gb.CreateSPParameter("@OptBayar", ParameterDirection.Input, SqlDbType.VarChar, 250, "2", typeof(string));
    //        varclmst3[19] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTCL", typeof(string));
    //        varclmst3[20] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varclmst3, "constrmf");

    //        //-----------------------------------------------------------------------------UPDATE CLMST----------------------------------------------------------------------------------

    //        //Select KodeBank from Clmst
    //        DefaultClass.Global.SqlHelper[] varclmst4 = new DefaultClass.Global.SqlHelper[7];
    //        varclmst4[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        varclmst4[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        varclmst4[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, KreditGL4, typeof(string));
    //        varclmst4[3] = gb.CreateSPParameter("@cadangan", ParameterDirection.Input, SqlDbType.Decimal, 50, TextBox3.Text, typeof(string));
    //        varclmst4[4] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 50, ttlpokok, typeof(string));
    //        varclmst4[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATECLMST", typeof(string));
    //        varclmst4[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", varclmst4, "constrmf");

    //        //-----------------------------------------------------------------------------UPDATE SUKONTER----------------------------------------------------------------------------------

    //        DefaultClass.Global.SqlHelper[] varSTATE = new DefaultClass.Global.SqlHelper[4];
    //        varSTATE[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
    //        varSTATE[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
    //        varSTATE[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "UPDATEALLPENCAIRAN", typeof(string));
    //        varSTATE[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
             
    //        ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", varSTATE, "constrmf");


    //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

    //        }

    //        else
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //        }



    //    }

    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //        btnSetuju.Enabled = true;
    //    }


    //}


    public void ProsesPencairanKreditx(string flag, string user)
    {

        try

        {


            //decimal cadangan = 0, cadangankedua = 0, bungamasterkredit = 0;
            //string bng = "";
           // TextBox3.Text= string.Format("{0:N2}", decimal.Parse(TextBox3.Text));
            //btnSetuju.Enabled = false;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[39];          
            var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 31, txtnoanggota.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglrealisasi.Text, typeof(string));
            var1[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 250, txtplafon.Text, typeof(decimal));
            var1[3] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjeniskredit.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlPenggunaan.SelectedValue, typeof(string));
            var1[6] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtpkredit.Text, typeof(string));
            var1[7] = gb.CreateSPParameter("@tgl_dropx", ParameterDirection.Input, SqlDbType.SmallDateTime, 15, txttglrealisasi.Text, typeof(DateTime));
            var1[8] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 15, txtjttempo.Text, typeof(DateTime));
            var1[9] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 8, txtjangka.Text, typeof(int));
            var1[10] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.Decimal, 250, txtbymaterai.Text, typeof(decimal));
            var1[11] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.Decimal, 250, txttaksasi.Text, typeof(decimal));
            var1[12] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, txtAO.Text, typeof(string));
            var1[13] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, txtsumberdana.Text, typeof(string));
            var1[14] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
            var1[15] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, txtprogrampinjaman.Text, typeof(string));
            var1[16] = gb.CreateSPParameter("@cadanganrek", ParameterDirection.Input, SqlDbType.Decimal, 50, TextBox3.Text, typeof(decimal));
            var1[17] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            var1[18] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[19] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[20] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[21] = gb.CreateSPParameter("@nominalprovisi", ParameterDirection.Input, SqlDbType.Decimal, 250, txtnominalprovisi.Text, typeof(decimal));
            var1[22] = gb.CreateSPParameter("@psnprovisi", ParameterDirection.Input, SqlDbType.Decimal, 250, txtProvisi.Text, typeof(decimal));
            var1[23] = gb.CreateSPParameter("@angsuranset", ParameterDirection.Input, SqlDbType.Decimal, 250, txtangsuranperbulan.Text, typeof(decimal));
            var1[24] = gb.CreateSPParameter("@XID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var1[25] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            var1[26] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
            var1[27] = gb.CreateSPParameter("@Uangmuka", ParameterDirection.Input, SqlDbType.Decimal, 0, lbluangmuka.Text, typeof(decimal));
            var1[28] = gb.CreateSPParameter("@cadangansebelumnya", ParameterDirection.Input, SqlDbType.Decimal, 0, lblcadangansebelumnya.Text, typeof(decimal));
            var1[29] = gb.CreateSPParameter("@norekeningperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 30, lblrekeningperpanjangan.Text, typeof(string));
            var1[30] = gb.CreateSPParameter("@tipeperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
            var1[31] = gb.CreateSPParameter("@cadangankembali", ParameterDirection.Input, SqlDbType.Decimal, 0, lblkembalicadangan.Text, typeof(decimal));
            var1[32] = gb.CreateSPParameter("@norekeningcbs", ParameterDirection.Input, SqlDbType.VarChar, 30, txtrekeningcbs.Text, typeof(string));
            var1[33] = gb.CreateSPParameter("@jenistagihan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenistagihan.SelectedValue, typeof(string));
            var1[34] = gb.CreateSPParameter("@Typebiayakeanggotaan", ParameterDirection.Input, SqlDbType.VarChar, 1, LblTypebiaya.Text, typeof(string));
            var1[35] = gb.CreateSPParameter("@Rekeninglamacadangan", ParameterDirection.Input, SqlDbType.VarChar, 30, LblrekeningCadangan.Text, typeof(string));
            var1[36] = gb.CreateSPParameter("@tgltagihhari", ParameterDirection.Input, SqlDbType.VarChar, 40, LblTglhariTagih.Text, typeof(string));
            var1[37] = gb.CreateSPParameter("@pilihcadangan", ParameterDirection.Input, SqlDbType.Int, 0, ddlbycad.SelectedValue, typeof(int));
            var1[38] = gb.CreateSPParameter("@discountangsuran", ParameterDirection.Input, SqlDbType.Decimal, 0, txtdiscountangsuran.Text, typeof(decimal));

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Insert_Cair_Mikro_2024_008", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                //DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                //var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var2[1] = gb.CreateSPParameter("@NoSekarang", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                //var2[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEPERMOHONAN", typeof(string));
                //var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", var2, "constrmf");

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pencairan Done!','You successfully save','top-right','success','1');", true);
                PnlFront.Visible = true;
                pnladd.Visible = false;
                pnlPengajuan.Visible = false;
                btnSetuju.Enabled = true;
                btnSetuju.Visible = true;
                BindGridEntity();
                txtnonasabahxt.Focus();
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                btnSetuju.Visible = true;
            }



        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            btnSetuju.Enabled = true;
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
        Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRXPENCAIRANPINJAMAN", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_GetSEARCH", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPENCAIRAN", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GRData_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}