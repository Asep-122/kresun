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
public partial class AR_Process_PencairanPinjamanAntrian : System.Web.UI.Page
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
        Response.Redirect("PencairanPinjamanAntrian.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PencairanPinjamanAntrian.aspx");
    }


    
    protected void btnbackawal_Click(object sender, EventArgs e)
    {
        pnlFilter.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
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

        CRno_pk = String.Format("{0:" + formatpknumber + "}", no_pk) + "/" + format_no_pk + SessBranchID + "/" + lbltglRealisasi.Text.Substring(0, 5).Replace("/", "-") + "/" + lbltglRealisasi.Text.Substring(6, 4);

        updatepkno = String.Format("{0:" + formatnumber + "}", no_pk);

        HDnopk.Value = CRno_pk;
        HDnopksekarang.Value = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();
        HDNoPKSebelumnya.Value = nopksebelumnya;



        btnSetuju.Visible = false;
        ProsesPencairanKreditx("9", SessLoginID);
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
                    txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
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
                    txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                    txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                   
                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                    //ddlPenggunaan.SelectedValue = "2";

                    ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();


                    ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                    lblrekeningperpanjangan.Text = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();

                    lblNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    lblNama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

                    lblcadangansebelumnya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                    lblnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                    lblktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                    lblnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                    lbljaminansurat.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                    lblagunan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    lblkondisiagunan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
                    Lbltaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                    lbljeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                    lblao.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                    lbltglRealisasi.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString()));
                    lbljangkawaktu.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                    lbljthtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                    lblbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                    lblmajuhari.Text = ddlmajuhari.SelectedItem.Text;
                    lblbesarpinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()));
                    lblangsuranbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                    lbladministrasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                    lblmaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                    //LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString())- decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                    decimal data1 = 0;
                    data1 = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()) - decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString());
                    if (data1 < 0)
                    {
                        data1 = 0;
                    }

                    LblPotonganangsuran.Text = string.Format("{0:N2}", data1);
                    LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString()));
                    Lblcadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString()));
                    LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                    lbluangmuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayauangmuka"].ToString()));

                    lblTotalPotongbiaya.Text = string.Format("{0:N2}", decimal.Parse(lbladministrasi.Text) + decimal.Parse(lblmaterai.Text) + decimal.Parse(LblPotonganangsuran.Text) + decimal.Parse(LblAnggota.Text) + decimal.Parse(lbluangmuka.Text));

                    lblygditerimaanggota.Text = string.Format("{0:N2}", decimal.Parse(lblbesarpinjaman.Text) - decimal.Parse(lblTotalPotongbiaya.Text));
                    lblkembalicadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pengembaliancadangan"].ToString()));
                    if (ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString() == "3C")
                    {
                        lblangsuran.Text = "Angsuran Per-Minggu";
                    }
                    else
                    {
                        lblangsuran.Text = "Angsuran Per-Bulan";
                    }

                    HDKodePos.Value = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                    HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    //HDLamaKerja.Value = ge.ListDataset.Tables[0].Rows[0]["lama_bekerja"].ToString();

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
        }
    }

    

    public void ProsesPencairanKreditx(string flag, string user)
    {

        try

        {


            //decimal cadangan = 0, cadangankedua = 0, bungamasterkredit = 0;
            //string bng = "";
           // TextBox3.Text= string.Format("{0:N2}", decimal.Parse(TextBox3.Text));
            btnSetuju.Enabled = false;
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[31];

          
            var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, lbltglRealisasi.Text, typeof(string));
            var1[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 250, lblbesarpinjaman.Text, typeof(decimal));
            var1[3] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, lbljeniskredit.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlPenggunaan.SelectedItem.Text, typeof(string));
            var1[6] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, HDnopk.Value, typeof(string));
            var1[7] = gb.CreateSPParameter("@tgl_dropx", ParameterDirection.Input, SqlDbType.SmallDateTime, 15, lbltglRealisasi.Text, typeof(DateTime));
            var1[8] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 15, lbljthtempo.Text, typeof(DateTime));
            var1[9] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 8, lbljangkawaktu.Text, typeof(int));
            var1[10] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.Decimal, 250, lblmaterai.Text, typeof(decimal));
            var1[11] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.Decimal, 250, Lbltaksasi.Text, typeof(decimal));
            var1[12] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, lblao.Text, typeof(string));
            var1[13] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, "999", typeof(string));
            var1[14] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250,lbljthtempo.Text, typeof(string));
            var1[15] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, "2", typeof(string));
            var1[16] = gb.CreateSPParameter("@cadanganrek", ParameterDirection.Input, SqlDbType.Decimal, 50, Lblcadangan.Text, typeof(decimal));
            var1[17] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            var1[18] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[19] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[20] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[21] = gb.CreateSPParameter("@nominalprovisi", ParameterDirection.Input, SqlDbType.Decimal, 250, lbladministrasi.Text, typeof(decimal));
            var1[22] = gb.CreateSPParameter("@psnprovisi", ParameterDirection.Input, SqlDbType.Decimal, 250, 3, typeof(decimal));
            var1[23] = gb.CreateSPParameter("@angsuranset", ParameterDirection.Input, SqlDbType.Decimal, 250, lblangsuranbulan.Text, typeof(decimal));
            var1[24] = gb.CreateSPParameter("@XID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var1[25] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            var1[26] = gb.CreateSPParameter("@Uangmuka", ParameterDirection.Input, SqlDbType.Decimal, 0, lbluangmuka.Text, typeof(decimal));
            var1[27] = gb.CreateSPParameter("@cadangansebelumnya", ParameterDirection.Input, SqlDbType.Decimal, 0, lblcadangansebelumnya.Text, typeof(decimal));
            var1[28] = gb.CreateSPParameter("@norekeningperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 30, lblrekeningperpanjangan.Text, typeof(string));
            var1[29] = gb.CreateSPParameter("@tipeperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
            var1[30] = gb.CreateSPParameter("@cadangankembali", ParameterDirection.Input, SqlDbType.Decimal, 0, lblkembalicadangan.Text, typeof(decimal));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Insert_Cair_Antrian", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pencairan Done!','You successfully save','top-right','success','1');", true);

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
        Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRXPENCAIRANPINJAMANANTRIAN", typeof(string));
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
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPENCAIRANANTRIAN", typeof(string));
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