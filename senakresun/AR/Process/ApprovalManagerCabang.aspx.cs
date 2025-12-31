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

public partial class AR_Process_ApprovalManagerCabang : System.Web.UI.Page
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
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovalManagerCabang.aspx");
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
    protected void btnNexIdentitas_Click(object sender, EventArgs e)
    {


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

        pnlPengajuan.Visible = true;
 

    }
    protected void btnBackJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        pnlPengajuan.Visible = false;


    }

    protected void btnNextPengajuan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
     
        //pnlLaba.Visible = false;
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
        //pnlKeanggotaan.Visible = false;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        //pnlUsaha.Visible = false;
    }


    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
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
          //pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        //pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = false;
        pnlAlasanTolak.Visible = true;
    }

    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        ProsesVerifikasi("5", SessLoginID);
    }

    protected void btnSetuju_Click(object sender, EventArgs e)
    {
        ProsesVerifikasi("6", SessLoginID);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BindGridEntity();
            //lbllabakotor.Font.Bold = true;
            //lbltotalbiaya.Font.Bold = true;
            //lbllabasebelumpajak.Font.Bold = true;
            //lbllababersih.Font.Bold = true;


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

          

            btnSetuju.Enabled = true;
            btnSimpanTolakan.Enabled = true;
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
                    pnlPengajuan.Visible = true;

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
                    txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                    ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                    txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                    txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                    ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                    txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                    txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    //data ktp

                    //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                    ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                    ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                    

                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();

                    
                    ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();

                    lblcadangansebelumnya.Text=string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));

                    lblNoAnggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    lblNama.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();


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
                    LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString())- decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                    LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                    lbluangmuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayauangmuka"].ToString()));
                    lblTotalPotongbiaya.Text = string.Format("{0:N2}", decimal.Parse(lbladministrasi.Text) + decimal.Parse(lblmaterai.Text) + decimal.Parse(LblPotonganangsuran.Text) + decimal.Parse(LblAnggota.Text) + decimal.Parse(lbluangmuka.Text));
                    lblygditerimaanggota.Text = string.Format("{0:N2}", decimal.Parse(lblbesarpinjaman.Text) - decimal.Parse(lblTotalPotongbiaya.Text));
                    ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                    lblrekeningperpanjangan.Text = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();
                    if (ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString() == "3C")
                    {
                        lblangsuran.Text = "Angsuran Per-Minggu";
                    }
                    else
                    {
                        lblangsuran.Text = "Angsuran Per-Bulan";
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
            var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVEMANAGER", typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[7] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));

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

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAPPROVALMANAGER", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
}