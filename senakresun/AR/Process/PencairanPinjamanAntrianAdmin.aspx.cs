using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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

public partial class AR_Process_PencairanPinjamanAntrianAdmin : System.Web.UI.Page
{

    String nosebelumnya = "", updateno = "", CRno_rekening = "", STATE = "", kdpersh = "", format_no_pk = "",
              formatnumber = "", formatpknumber = "", formatnumberbukti = "", nopksebelumnya = "", CRno_pk = "", updatepkno = "", pokok = "", bunga = "",
              typetrn = "", keterangantrn = "", no_bukti_sebelumnya = "", NoBuktiNew = "";
    double ttlbunga = 0;
    int counter_no_rekening = 0, i = 0, x = 0, sisapk = 0;

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
        Response.Redirect("PencairanPinjamanAntrianAdmin.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PencairanPinjamanAntrianAdmin.aspx");
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
        //pnlKeanggotaan.Visible = false;
        //pnlUsaha.Visible = true;

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

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;

        //pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        //pnlUsaha.Visible = false;
    }



    protected void btnlanjutkan_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[11];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalasantolak.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVETELLER", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            var88[9] = gb.CreateSPParameter("@notelpon", ParameterDirection.Input, SqlDbType.VarChar, 20, txthpktp.Text, typeof(string));
            var88[10] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 100,txtemailsktp.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnlanjutkan.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','APPROVE TELLER SUKSES','top-right','success','1');", true);

            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                btnlanjutkan.Visible = true;
            }

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }


    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            //btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[11];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@jaminandetail", ParameterDirection.Input, SqlDbType.VarChar, 250, txtketeranganJaminan.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, lblanggotax.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEDATA", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            var88[9] = gb.CreateSPParameter("@notelpon", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnohpx.Text, typeof(string));
            var88[10] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 100, txtemailx.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                Button2.Visible = false;
                Response.Redirect("PencairanPinjamanAntrianAdmin.aspx");

            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                Button2.Visible = true;
            }

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }
    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[10];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalasantolak.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BATALKANTELLER", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            var88[9] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, lblrekpinjaman.Text, typeof(string));
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

    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
      
        pnlPengajuan.Visible = true;
      
        pnlAlasanTolak.Visible = false;
    }

    protected void btnTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
    
        pnlPengajuan.Visible = false;
        pnlAlasanTolak.Visible = true;
      

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            BindGridEntity();
            
            btnSimpanTolakan.Enabled = true;
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

        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlPengajuan.Visible = true;
                    lblrekpinjaman.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    lblpkkredit.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopk")).Text;
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
                    LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString()));

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

                    // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                    // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    if (ge.ListDataset.Tables[0].Rows[0]["statusantrian"].ToString() == "2")
                    {
                        btnlanjutkan.Visible = false;
                    }
                    else
                    {
                        btnlanjutkan.Visible = true;
                    }
                    

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

            case "UPDATEX":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = false;
                    pnlPengajuan.Visible = false;
                    Pnlupdatedata.Visible = true;
                    lblrekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    lblpkkredit.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopk")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");




                    txtnohpx.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                    txtemailx.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    lblanggotax.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                    lblnamaanggota.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                    lbljaminansuratx.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                    txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminansuratdetail"].ToString();
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "Bukti":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKBUKTI", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf");
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLBuktiAntrian.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("Periode", DateTime.Now.ToString("dd MMMM yyyy"));
                        rprt.SetParameterValue("usersearch", SessLoginID);
                        rprt.SetParameterValue("Judul", "Bukti Pencairan");
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
                    }
                }
                break;
            case "Kwitansipinjaman":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                   
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIPINJAMAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLKwitansiPencairan.rpt"));

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
                    }
                }
                break;
            case "Angsuranpinjaman":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

                    var[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, norek, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUNEW", typeof(string));
                    var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf").Tables[0];

                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));

                    rprt.SetDataSource(ge.ListDataTable);


                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.ContentType = "application/pdf";
                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    Response.End();
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
                    }
                }
                break;
            case "PerjanjianKredit":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;

                    string kode_produk, file_report;


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                        var[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPK", typeof(string));
                        
                        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf");

                        kode_produk = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                        file_report = ge.ListDataset.Tables[0].Rows[0]["path_filereport"].ToString();
                        string status = "1";

                        if (kode_produk == "3C") //Pembiayaan sendiri mingguan
                        {
                            rprt.Load(Server.MapPath(file_report));
                            rprt.SetParameterValue("kepala_cabang", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());
                            rprt.SetParameterValue("alamat_cabang", ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString());
                            rprt.SetParameterValue("nama_nasabah", ge.ListDataset.Tables[0].Rows[0]["picb"].ToString());
                            rprt.SetParameterValue("alamat_nasabah", ge.ListDataset.Tables[0].Rows[0]["alamat_nasabah"].ToString());
                            rprt.SetParameterValue("nm_penjamin", ge.ListDataset.Tables[0].Rows[0]["nmpjmn2"].ToString());
                            rprt.SetParameterValue("plafon", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("jangka", ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString());
                            rprt.SetParameterValue("tgl_drop", ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString());
                            rprt.SetParameterValue("tgl_due", ge.ListDataset.Tables[0].Rows[0]["tgl_due"].ToString());
                            rprt.SetParameterValue("pokok----------10", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["pokok"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("bunga", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("angsuran_perbulan", string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbrx"].ToString())));
                            rprt.SetParameterValue("persen_denda", ge.ListDataset.Tables[0].Rows[0]["persen_denda_pelunasan"].ToString());
                            rprt.SetParameterValue("jaminan", ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString());
                            rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            rprt.SetParameterValue("terbilangPlafon", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString())));
                            rprt.SetParameterValue("terbilang_angsuran_perbulan", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbrx"].ToString())));
                            rprt.SetParameterValue("no_pk", ge.ListDataset.Tables[0].Rows[0]["no_spk"].ToString());
                            rprt.SetParameterValue("no_rekening-----20", lblrekpinjaman.Text);
                            rprt.SetParameterValue("nama_koperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("tanggal_print", DateTime.Now.ToString("dd-MMMM-yyyy"));
                            rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                            rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                            rprt.SetParameterValue("alamatpusat", "Kantor Pusat : " + ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            rprt.SetParameterValue("statuskop", status);
                            rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                            rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());

                            Response.ClearContent();
                            Response.ClearHeaders();

                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                            Response.End();
                        }
                        else if (kode_produk == "3B") //pembiayaan sendiri bulanan
                        {
                            //string bunga_terbilang = gb.terbilang("constrmf", ge.ListDataset.Tables[0].Rows[0]["persen_bunga"].ToString().Trim());
                            //string SanksiAdministrasi_terbilang = gb.terbilang("constrmf", ge.ListDataset.Tables[0].Rows[0]["sanksi_administrasi_perbulan"].ToString().Trim().Replace(",", "."));
                            //string potonganbunga_terbilang = gb.terbilang("constrmf", ge.ListDataset.Tables[0].Rows[0]["PotonganBunga"].ToString().Trim().Replace(",", "."));
                            string bunga_terbilang = "";
                            string SanksiAdministrasi_terbilang = "";
                            string potonganbunga_terbilang = "";

                            //ReportDocument rprt = new ReportDocument();
                            rprt.Load(Server.MapPath(file_report));
                            rprt.SetParameterValue("No.PK", ge.ListDataset.Tables[0].Rows[0]["no_spk"].ToString());
                            rprt.SetParameterValue("NamaPejabat", ge.ListDataset.Tables[0].Rows[0]["Kepala_koperasi"].ToString());
                            rprt.SetParameterValue("NamaAnggota", ge.ListDataset.Tables[0].Rows[0]["picb"].ToString().Trim());
                            rprt.SetParameterValue("NamaAnggotaBesar", ge.ListDataset.Tables[0].Rows[0]["namaanggota"].ToString().Trim());
                            rprt.SetParameterValue("NamaPenjamin", ge.ListDataset.Tables[0].Rows[0]["nmpjmn2"].ToString());
                            rprt.SetParameterValue("AlamatAnggota---------05", ge.ListDataset.Tables[0].Rows[0]["alamat_nasabah"].ToString());
                            rprt.SetParameterValue("Plafon", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("PlafonTerbilang", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString())));
                            rprt.SetParameterValue("AngsTotal", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["rek_tabbr"].ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("AngsTotalTerbilang", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbr"].ToString())));
                            rprt.SetParameterValue("JangkaWaktu-----------10", ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString());
                            rprt.SetParameterValue("JangkaWaktuTerbilang", gb.TerbilangAngka(int.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString())));
                            rprt.SetParameterValue("TanggalDrop", ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString());
                            rprt.SetParameterValue("TanggalDue", ge.ListDataset.Tables[0].Rows[0]["tgl_due"].ToString());
                            rprt.SetParameterValue("Jaminan1", ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString());
                            rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("Jabatan", ge.ListDataset.Tables[0].Rows[0]["jabatan"].ToString());
                            rprt.SetParameterValue("Perwakilan/Sendiri", ge.ListDataset.Tables[0].Rows[0]["Perwakilan"].ToString());
                            rprt.SetParameterValue("NoRekening", lblrekpinjaman.Text);
                            rprt.SetParameterValue("Keperluan", ge.ListDataset.Tables[0].Rows[0]["keterangan"].ToString());
                            //string asda = string.Format("{0:N2}",decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_bunga"].ToString()));
                            rprt.SetParameterValue("Bunga%---------------20", string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_bunga"].ToString())));
                            //rprt.SetParameterValue("BungaTerbilang", bunga_terbilang);
                            rprt.SetParameterValue("SanksiAdministrasi%", ge.ListDataset.Tables[0].Rows[0]["persen_denda_tunggakan"].ToString());
                            rprt.SetParameterValue("SanksiAdministrasi/bulan", ge.ListDataset.Tables[0].Rows[0]["sanksi_administrasi_perbulan"].ToString());//blm ada
                            rprt.SetParameterValue("SanksiAdministrasi/bulanTerbilang", ge.ListDataset.Tables[0].Rows[0]["SanksiAdministrasi_terbilang"].ToString());  //SanksiAdministrasi_terbilang + " Persen");//blm ada
                            rprt.SetParameterValue("Provisi%", ge.ListDataset.Tables[0].Rows[0]["PsnProvisi"].ToString());
                            rprt.SetParameterValue("PotonganBunga%", ge.ListDataset.Tables[0].Rows[0]["PotonganBunga"].ToString());
                            rprt.SetParameterValue("PotonganBungaTerbilang", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PotonganBunga"].ToString())));
                            rprt.SetParameterValue("PekerjaanPenjamin", "-");
                            rprt.SetParameterValue("PekerjaanAnggota", ge.ListDataset.Tables[0].Rows[0]["pekerjaan_anggota"].ToString());
                            rprt.SetParameterValue("AlamatPenjamin-------30", ge.ListDataset.Tables[0].Rows[0]["alamat_penjamin"].ToString());
                            rprt.SetParameterValue("HubunganPenjamin", ge.ListDataset.Tables[0].Rows[0]["keterangan"].ToString());
                            rprt.SetParameterValue("DendaJaminan%", ge.ListDataset.Tables[0].Rows[0]["dendajaminan"].ToString());
                            rprt.SetParameterValue("Satuan", ge.ListDataset.Tables[0].Rows[0]["satuan"].ToString());
                            rprt.SetParameterValue("Nama Cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            rprt.SetParameterValue("Tanggal Hari Ini", DateTime.Now.ToString("dd MMMM yyyy"));
                            rprt.SetParameterValue("Agunan", ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString());
                            rprt.SetParameterValue("pengadilan", ge.ListDataset.Tables[0].Rows[0]["pengadilan"].ToString());
                            rprt.SetParameterValue("Tandatangannama", "");
                            rprt.SetParameterValue("Tandatanganjab", "");
                            rprt.SetParameterValue("BungaTerbilang", ge.ListDataset.Tables[0].Rows[0]["BungaTerbilang"].ToString());
                            rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                            rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                            rprt.SetParameterValue("alamatpusat", "Kantor Pusat : " + ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            rprt.SetParameterValue("statuskop", status);
                            rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());

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
                    }
                }
                break;
            case "TandaTerimaAnggota":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                    var[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCETAKKEANGGOTAAN", typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString() == "0")
                    {
                        rprt.Load(Server.MapPath("~/AR/Report/rptCetakAnggota.rpt"));
                        rprt.SetParameterValue("noAnggota", ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString());
                        rprt.SetParameterValue("nama", ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString());
                        rprt.SetParameterValue("alamat", ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString() + " " + ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString());
                        rprt.SetParameterValue("kabupaten", ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString() + "," + ge.ListDataset.Tables[0].Rows[0]["kabupaten1"].ToString() + "," + ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString() + " " + ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString());


                        rprt.SetParameterValue("pokok", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["simpanan_pokok"].ToString().Replace(",", ".")));
                        rprt.SetParameterValue("wajib", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["simpanan_smk"].ToString().Replace(",", ".")));
                        rprt.SetParameterValue("tanggal", ge.ListDataset.Tables[0].Rows[0]["tgl"].ToString());
                        rprt.SetParameterValue("kepalaPos", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());
                        rprt.SetParameterValue("cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                        rprt.SetParameterValue("nomPokok", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["simpanan_pokok"].ToString().Replace(",", "."))));
                        rprt.SetParameterValue("nomWajib", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["simpanan_smk"].ToString().Replace(",", "."))));
                        rprt.SetParameterValue("nama kantor", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());

                        rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                        rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                        rprt.SetParameterValue("alamatpusat", "Kantor Pusat : " + ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                        rprt.SetParameterValue("statuskop", "1");
                        rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                        rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                        Response.End();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sudah menjadi Anggota');", true);
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
                    }
                }
                break;
            case "KwitansiAnggota":
                try
                {
                   
                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIANGGOTA", typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf");
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf").Tables[0];




                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        if (ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString() == "0")
                        {
                            rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLKWITANSIANGGOTA.rpt"));
                            rprt.SetDataSource(ge.ListDataTable);
                            rprt.SetParameterValue("Periode", DateTime.Now.ToString("dd MMMM yyyy"));
                            rprt.SetParameterValue("usersearch", SessLoginID);
                            rprt.SetParameterValue("Judul", "Kwitansi");
                            //Response.Buffer = false;
                            // Clear the response content and headers
                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            Response.End();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sudah menjadi Anggota');", true);
                        }
                       
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
                    }
                }
                break;
            case "TandaTerimaJaminan":
                try
                {
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKSURATJAMINAN", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLTandaTerimaJaminan.rpt"));

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
                    }
                }
                break;
        }
    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTANTRIANADMIN", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "selisih asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
}