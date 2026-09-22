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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text.pdf;
using DocumentFormat.OpenXml.Office2010.PowerPoint;


public partial class AR_Process_PermohonanPinjamanKreditMultigunaNew_pk2 : System.Web.UI.Page
{
    String nosebelumnya = "", updateno = "", CRno_rekening = "", STATE = "", kdpersh = "", format_no_pk = "",
             formatnumber = "", formatpknumber = "", formatnumberbukti = "", nopksebelumnya = "", CRno_pk = "", updatepkno = "", pokok = "", bunga = "",
             typetrn = "", keterangantrn = "", no_bukti_sebelumnya = "", NoBuktiNew = "", DebitGL = "", KreditGL1 = "", KreditGL2 = "",
             KodeJurnal = "", KreditGL3 = "", KreditGL4 = "", kodebank = "";
    double ttlbunga = 0, sisapk = 0, ttlpokok = 0, pokokbunga = 0, ttlkeluarcl = 0;
    int counter_no_rekening = 0, i = 0, x = 0;

    long no_pk_sekarang = 0, no_pk = 0, nosekarang = 0, no_rekening = 0, no_bukti_sekarang = 0, no_bukti = 0;

    DataTable FormSimulasiPk2 = new DataTable("FormSimulasiPK2");
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gbx = new DefaultClass.Global();
    GlobalEntities gex = new GlobalEntities();

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    double angsuranperbulan = 0;
    string nonasabah = "", agama = "", usia = "", namafileJaminan = "", fotopathJaminan = "";
    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";

    string fotopathKTP1 = "",
       fotopathKTP2 = "", fotopathKTP3 = "", fotopathKTP4 = "", fotopathKTP5 = "";

    public string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }

    protected void ddlpenerimapensiun_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlpenerimapensiun.SelectedValue == "0")
        {
            txtidktppp.Text = txtnoidentitasktp.Text;
            txtnamapp.Text = txtnamanasabah.Text;
            ddljeniskelaminpp.SelectedValue = ddljenkelktp.SelectedValue;
            txttempatlahirpp.Text = txttempatlahirktp.Text;
            txttgllahirpp.Text = txttgllahirktp.Text;
            ddlagamapp.SelectedValue = ddlAgamaktp.SelectedValue;
            txtnohppp.Text = txthpktp.Text;
            txtemailpp.Text = txtemailsktp.Text;
            txtalamatpp.Text = txtalamatktp.Text;
            txtrtpp.Text = txtrtktp.Text;
            txtrwpp.Text = txtrwktp.Text;
            txtkabupatenpp.Text = txtkabupatenktp.Text;
            txtkelurahanpp.Text = txtkelurahanktp.Text;
            txtkecamatanpp.Text = txtkecamatanktp.Text;
            ddlprovinsipp.SelectedValue = ddlpropinsiktp.SelectedValue;
            txtkodepospp.Text = txtkodeposktp.Text;
        }
        else
        {
            txtidktppp.Text="0";
            txtnamapp.Text = "";
            txttempatlahirpp.Text = "";
            txttgllahirpp.Text = "";
            txtnohppp.Text = "0";
            txtemailpp.Text = "";
            txtalamatpp.Text = "";
            txtrtpp.Text = "0";
            txtrwpp.Text = "0";
            txtkabupatenpp.Text = "";
            txtkelurahanpp.Text = "";
            txtkecamatanpp.Text = "";
            txtkodepospp.Text = "0";
        }


    }

    
    protected void ddlkewarganegaraan_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlkewarganegaraanktp.SelectedValue == "2")
        {
            txtkewarganegaraanktp.Enabled = true;
            txtkewarganegaraanktp.Text = "";
        }
        else
        {
            txtkewarganegaraanktp.Enabled = false;
            txtkewarganegaraanktp.Text = "-";
        }


    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            InsertCombo();

            ddlstatus_SelectedIndexChanged(sender, e);
           
            txtkodecabang.Text = SessBranchID;
         

            //ddlSurvey.Items.Add("Pilih Data");
            //ddlSurvey.Items.Add("Belum Dilakukan Survey");
            ddlSurvey.Items.Add("Sudah Dilakukan Survey");
            ddlSurvey.Visible = false;
            ddlSurvey.SelectedValue = "Sudah Dilakukan Survey";

            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";

            //txttglrealisasi.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            //get tanggal server

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[1];
            varT[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarangXYZ", varT, "constrmf");

            //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            //ge.ListDataset = gb.GetDataJustOutPut("gettglsekarangXYZ", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
            txttglrealisasi.Text = xtanggal.ToString("dd-MM-yyyy");
            txtmtpensiun.Text = xtanggal.ToString("dd-MM-yyyy");
            txtglskpensiun.Text = xtanggal.ToString("dd-MM-yyyy");
            ddlstatus.Focus();


        }
        else
        {
            string activeTab = hfActiveTab.Value;

            if (!string.IsNullOrEmpty(activeTab))
            {
                string script = "setTimeout(function(){ $('a[href=\"" + activeTab + "\"]').tab('show'); }, 0);";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "activateTab",
                    script,
                    true
                );
            }

        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
      


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

    protected void gajipokok_ontextchanged(object sender, EventArgs e)
    {
        try
        {

            decimal a, b, c, d, f, g, h, i, j, k, x, y, z;
            a = decimal.Parse(txtgapok.Text);
            b = decimal.Parse(txttunjistri.Text);
            c = decimal.Parse(txttunjangananak.Text);
            d = decimal.Parse(txttunjanganberas.Text);
            f = decimal.Parse(txttunjanganlain.Text);

            g = decimal.Parse(txtpotonganpajak.Text);
            h = decimal.Parse(txtpotonganaskes.Text);
            i = decimal.Parse(txtpotonganlain.Text);

            txtnetgaji.Text = string.Format("{0:N0}", a + b + c + d + f);
            txttotalpotongan.Text = string.Format("{0:N0}", g + h + i);
            txtgajibersih.Text = string.Format("{0:N0}", decimal.Parse(txtnetgaji.Text) - decimal.Parse(txttotalpotongan.Text));


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }



    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {


        //Session.Contents.Add("Statusses", ddlstatus.SelectedValue);

        BindGridEntity();
        GRData.Columns[8].Visible = false;
        GRData.Columns[12].Visible = false;
        GRData.Columns[13].Visible = false;
        GRData.Columns[14].Visible = false;
        GRData.Columns[15].Visible = true;
        if (ddlstatus.SelectedValue == "8" )

        {

            GRData.Columns[14].Visible = true;





        }
        else if (ddlstatus.SelectedValue == "9" || ddlstatus.SelectedValue == "6")

        {


            GRData.Columns[8].Visible = true;
            GRData.Columns[15].Visible = false;

            GRData.Columns[14].Visible = true;

        }
        else if (ddlstatus.SelectedValue == "11")

        {

            GRData.Columns[12].Visible = true;
            GRData.Columns[13].Visible = true;


        }
        else
        {
            GRData.Columns[8].Visible = false;
            GRData.Columns[12].Visible = false;
            GRData.Columns[13].Visible = false;
            GRData.Columns[14].Visible = false;

        }
       


    }



    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("PermohonanPinjamanKreditMultigunaNew.aspx");
        pnladd.Visible = false;
        pnlIdentitas.Visible = false;
        PnlFront.Visible = true;
        pnlFilter.Visible = true;
        PNLKELUARGA.Visible = false;
        //pnlJaminan.Visible = false;
        //pnlPengajuan.Visible = false;
        btnSimpan.Visible = false;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;
        btnTolakAntrian.Visible = false;
        pnlAlasanTolak.Visible = false;
        txtnonasabah.Text = "";
        Pnlpenerima.Visible = false;
        Pnlsimulasi.Visible = false;
        PnlSKPensiun.Visible = false;
        pnlpinjaman.Visible = false;

        pnlUpdAsuransi.Visible = false;
        btnCancel.Visible = false;
        //btndokumen.Visible = false;
        //ddlstatus_SelectedIndexChanged(sender, e);
        PnlRekeningBank.Visible = false;
        nav.Visible = false;
        BackNextAnggota.Visible = false;
        BackNextAhliWaris.Visible = false;
        BackNextSimulasi.Visible = false;
        btnBackSimulasi.Visible = false;
        ClearData();
        txtnonasabah.Focus();
        ddlstatus_SelectedIndexChanged(sender,e);
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


    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlIdentitas.Visible = true;
    }


   
  
        
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
            txtalamatrumahtinggal1.Enabled = false;
            txtrttp.Enabled = false;
            txtrwtpt.Enabled = false;
            txtkelurahantpt.Enabled = false;
            txtkecamatantpt.Enabled = false;
            ddlprovinsitpt.Enabled = false;
            txtkodepostpt.Enabled = false;

            //txtalamatrumahtinggal1.Text = "-";
            //txtkelurahantpt.Text = "-";
            //txtrttp.Text = "0";
            //txtrwtpt.Text = "0";
            //txtkodepostpt.Text = "0";
            //txtkecamatantpt.Text = "-";
            //txtalamatrumahtinggal1.Enabled = true;
            //txtrttp.Enabled = true;
            //txtrwtpt.Enabled = true;
            //txtkelurahantpt.Enabled = true;
            //txtkecamatantpt.Enabled = true;
            //ddlprovinsitpt.Enabled = true;
            //txtkodepostpt.Enabled = true;
        }

    }
  
    protected void txtemails_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (txtemails.Text != "-")
        //    {
        //        var eMailValidator = new System.Net.Mail.MailAddress(txtemails.Text);
        //    } 
        //}
        //catch (FormatException ex)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Format Email yang anda masukan salah');", true);

        //}
        Regex mRegxExpression;
        if (txtemailsktp.Text.Trim() != string.Empty)
        {
            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

            if (!mRegxExpression.IsMatch(txtemailsktp.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Format Email yang anda masukan salah');", true);

                txtemailsktp.Focus();
            }
        }
    }
    public object CheckTglNull(string ParamTanggal)
    {
        object Tanggal;
        if (string.IsNullOrWhiteSpace(ParamTanggal))
        {
            Tanggal = DBNull.Value;
        }
        else
        {
            DateTime parsedDate;
            if (DateTime.TryParse(ParamTanggal, out parsedDate))
            {
                Tanggal = parsedDate;
            }
            else
            {
                Tanggal = DBNull.Value;
            }
        }

        return Tanggal;
    }
    public object ChecknumberNull(string ParamTanggal)
    {
        object number;
        if (string.IsNullOrWhiteSpace(ParamTanggal))
        {
            number = 0;
        }
        else
        {
            number = ParamTanggal;
        }

        return number;
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        try
        {


            //btnSimpan.Enabled = false;

            //INSERT INTO KPPermohonanKredit

            if (string.IsNullOrEmpty(ddlKantorBayar.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
                return;
            }
            else if (string.IsNullOrEmpty(ddlsumberdana.SelectedValue) || ddlsumberdana.SelectedValue =="-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Sumber Dana terlebih dahulu');", true);
               
                return;
            }
            else if (string.IsNullOrEmpty(ddlprogrampinjaman.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
               
                return;
            }
            else if (string.IsNullOrEmpty(ddlkategori.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
               
                return;
            }

            else if (string.IsNullOrEmpty(txtnopen.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi no pensiun terlebih dahulu');", true);
                
                return;
            }
            else if (string.IsNullOrEmpty(ddlkodejiwa.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kode Jiwa terlebih dahulu');", true);
               
                return;
            }
            else if (string.IsNullOrEmpty(ddlstatusnikah.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Status Nikah terlebih dahulu');", true);
                
                return;
            }
            else if (string.IsNullOrEmpty(ddlpensiun.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Pensiun terlebih dahulu');", true);
               
                return;
            }
            else if (string.IsNullOrEmpty(txtidktppp.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Id KTP Penerima Pensiun terlebih dahulu');", true);
                txtidktppp.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtnamapp.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Nama Penerima Pensiun terlebih dahulu');", true);
                txtnamapp.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txttempatlahirpp.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Tempat Lahir Penerima Pensiun terlebih dahulu');", true);
                txttempatlahirpp.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtalamatpp.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Alamat Penerima Pensiun terlebih dahulu');", true);
                txtalamatpp.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtnamaaw.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Nama Ahli Waris terlebih dahulu');", true);
                txtnamaaw.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtnamaibukandungaw.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Nama Ibu Kandung terlebih dahulu');", true);
                txtnamaibukandungaw.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txthubunganaw.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Hubungan Ahli Waris terlebih dahulu');", true);
                txthubunganaw.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txttempatlahiraw.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Tempat Tanggal Lahir Ahli Waris terlebih dahulu');", true);
                txthubunganaw.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtalamataw.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Tempat Alamat Ahli Waris terlebih dahulu');", true);
                txthubunganaw.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtnip.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi nip terlebih dahulu');", true);
                txtnip.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtnamasesuaisk.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi nama sesuai sk terlebih dahulu');", true);
                txtnamasesuaisk.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtnosk.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi No sk terlebih dahulu');", true);
                txtnosk.Focus();
                return;
            }
            else if (decimal.Parse(txtgajidisplay.Text)==0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Gaji Display terlebih dahulu');", true);
                txtgajidisplay.Focus();
                return;
            }
            else if (decimal.Parse(txtplafon.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Plafon terlebih dahulu');", true);
                txtplafon.Focus();
                return;
            }
            else if (ddljangkawaktu.SelectedValue=="0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Jangka Waktu terlebih dahulu');", true);
                ddljangkawaktu.Focus();
                return;
            }
            else if (ddlmutasikantorbayar.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi Mutasi Kantor terlebih dahulu');", true);
                ddlmutasikantorbayar.Focus();
                return;
            }
            else if (decimal.Parse(txtplafon.Text) > decimal.Parse(txtplafonmax.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Plafon tidak boleh lebih dari plafon max');", true);
                txtplafon.Focus();
                return;
            }
            else if (int.Parse(ddljangkawaktu.SelectedValue) > int.Parse(txtjangkawaktumax.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jangka Waktu tidak boleh lebih dari Jangka Waktu max');", true);
                ddljangkawaktu.Focus();
                return;
            }
            else
            {

                setPK2();

                object tglLahirAnak1Value = CheckTglNull(txttgllahiranak1.Text);
                object tglLahirAnak2Value = CheckTglNull(txttgllahiranak2.Text);
                object tglLahirAnak3Value = CheckTglNull(txttgllahiranak3.Text);
                object txttgllahirawValue = CheckTglNull(txttgllahiraw.Text);
                object txttgllahirppValue = CheckTglNull(txttgllahirpp.Text);
                object usia1 = ChecknumberNull(txtusia1.Text);
                object usia2 = ChecknumberNull(txtusia2.Text);
                object usia3 = ChecknumberNull(txtusia3.Text);
                object usiajatuhtempo = ChecknumberNull(txtusiajthtempo.Text);

                nav.Visible = false;



                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[165];
            var1[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            var1[3] = gb.CreateSPParameter("@Jenis_produk", ParameterDirection.Input, SqlDbType.VarChar, 10, "A", typeof(string));
            var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[6] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
            var1[7] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
            var1[8] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
            var1[9] = gb.CreateSPParameter("@siklus_transaksi", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
            var1[10] = gb.CreateSPParameter("@namanasabah", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamanasabah.Text, typeof(string));
            var1[11] = gb.CreateSPParameter("@simulasi", ParameterDirection.Input, SqlDbType.VarChar, 1, ddldatasimulasi.SelectedValue, typeof(string));
            var1[12] = gb.CreateSPParameter("@nosimulasi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddllistsimulasi.SelectedValue, typeof(string));
            var1[13] = gb.CreateSPParameter("@instansi", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlinstansi.SelectedValue, typeof(string));
            var1[14] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnip.Text, typeof(string));
            var1[15] = gb.CreateSPParameter("@nopensiun", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnopen.Text, typeof(string));
            var1[16] = gb.CreateSPParameter("@namask", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamasesuaisk.Text, typeof(string));
            var1[17] = gb.CreateSPParameter("@nosk", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnosk.Text, typeof(string));
            var1[18] = gb.CreateSPParameter("@tglskpensiun", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtglskpensiun.Text, typeof(string));
            var1[19] = gb.CreateSPParameter("@statusnikah", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikah.SelectedValue, typeof(string));
            var1[20] = gb.CreateSPParameter("@jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlpensiun.SelectedValue, typeof(string));
            var1[21] = gb.CreateSPParameter("@kodejiwa", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkodejiwa.SelectedValue, typeof(string));
            var1[22] = gb.CreateSPParameter("@tmtpensiun", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtmtpensiun.Text, typeof(string));
            var1[23] = gb.CreateSPParameter("@nonpwp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnonpwp.Text, typeof(string));
            var1[24] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(string));
            var1[25] = gb.CreateSPParameter("@tgllahiranak1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglLahirAnak1Value, typeof(string));
            var1[26] = gb.CreateSPParameter("@tgllahiranak2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglLahirAnak2Value, typeof(string));
            var1[27] = gb.CreateSPParameter("@gajidisplay", ParameterDirection.Input, SqlDbType.Money, 0, txtgajidisplay.Text, typeof(decimal));
            var1[28] = gb.CreateSPParameter("@gajipokok", ParameterDirection.Input, SqlDbType.Money, 0, txtgapok.Text, typeof(decimal));
            var1[29] = gb.CreateSPParameter("@tunjanganistri", ParameterDirection.Input, SqlDbType.Money, 0, txttunjistri.Text, typeof(decimal));
            var1[30] = gb.CreateSPParameter("@tunjangananak", ParameterDirection.Input, SqlDbType.Money, 0, txttunjangananak.Text, typeof(decimal));
            var1[31] = gb.CreateSPParameter("@tunjanganberas", ParameterDirection.Input, SqlDbType.Money, 0, txttunjanganberas.Text, typeof(decimal));
            var1[32] = gb.CreateSPParameter("@tunjanganlainlain", ParameterDirection.Input, SqlDbType.Money, 0, txttunjanganlain.Text, typeof(decimal));
            var1[33] = gb.CreateSPParameter("@gajikotor", ParameterDirection.Input, SqlDbType.Money, 0, txtnetgaji.Text, typeof(decimal));
            var1[34] = gb.CreateSPParameter("@potonganpajak", ParameterDirection.Input, SqlDbType.Money, 0, txtpotonganpajak.Text, typeof(decimal));
            var1[35] = gb.CreateSPParameter("@potonganaskes", ParameterDirection.Input, SqlDbType.Money, 0, txtpotonganaskes.Text, typeof(decimal));
            var1[36] = gb.CreateSPParameter("@potonganlainlain", ParameterDirection.Input, SqlDbType.Money, 0, txtpotonganlain.Text, typeof(decimal));
            var1[37] = gb.CreateSPParameter("@totalpotongan", ParameterDirection.Input, SqlDbType.Money, 0, txttotalpotongan.Text, typeof(decimal));
            var1[38] = gb.CreateSPParameter("@gajibersih", ParameterDirection.Input, SqlDbType.Money, 0, txtgajibersih.Text, typeof(decimal));
            var1[39] = gb.CreateSPParameter("@penerima_pensiun", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlpenerimapensiun.SelectedValue, typeof(string));
            var1[40] = gb.CreateSPParameter("@id_KTP", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidktppp.Text, typeof(string));
            var1[41] = gb.CreateSPParameter("@namaPP", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamapp.Text, typeof(string));
            var1[42] = gb.CreateSPParameter("@jenis_kelamin", ParameterDirection.Input, SqlDbType.VarChar, 2, ddljeniskelaminpp.SelectedValue, typeof(string));
            var1[43] = gb.CreateSPParameter("@tempatlahirpp", ParameterDirection.Input, SqlDbType.VarChar, 30, txttempatlahirpp.Text, typeof(string));
            var1[44] = gb.CreateSPParameter("@tgllahirpp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahirppValue, typeof(string));
            var1[45] = gb.CreateSPParameter("@agamapp", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlagamapp.SelectedValue, typeof(string));
            var1[46] = gb.CreateSPParameter("@nohppp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnohppp.Text, typeof(string));
            var1[47] = gb.CreateSPParameter("@emailpp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtemailpp.Text, typeof(string));
            var1[48] = gb.CreateSPParameter("@alamatpp", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatpp.Text, typeof(string));
            var1[49] = gb.CreateSPParameter("@rtpp", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrtpp.Text, typeof(string));
            var1[50] = gb.CreateSPParameter("@rwpp", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrwpp.Text, typeof(string));
            var1[51] = gb.CreateSPParameter("@kabupatenpp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkabupatenpp.Text, typeof(string));
            var1[52] = gb.CreateSPParameter("@kelurahanpp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkelurahanpp.Text, typeof(string));
            var1[53] = gb.CreateSPParameter("@kecamatanpp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkecamatanpp.Text, typeof(string));
            var1[54] = gb.CreateSPParameter("@provinsipp", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlprovinsipp.SelectedValue, typeof(string));
            var1[55] = gb.CreateSPParameter("@kodepospp", ParameterDirection.Input, SqlDbType.VarChar, 6, txtkodepospp.Text, typeof(string));
            var1[56] = gb.CreateSPParameter("@namaibukandungaw", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamaibukandungaw.Text, typeof(string));
            var1[57] = gb.CreateSPParameter("@hubunganaw", ParameterDirection.Input, SqlDbType.VarChar, 30, txthubunganaw.Text, typeof(string));
            var1[58] = gb.CreateSPParameter("@namaaw", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamaaw.Text, typeof(string));
            var1[59] = gb.CreateSPParameter("@jenis_kelaminaw", ParameterDirection.Input, SqlDbType.VarChar, 2, ddljeniskelaminaw.SelectedValue, typeof(string));
            var1[60] = gb.CreateSPParameter("@tempatlahiraw", ParameterDirection.Input, SqlDbType.VarChar, 30, txttempatlahiraw.Text, typeof(string));
            var1[61] = gb.CreateSPParameter("@tgllahiraw", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahirawValue, typeof(string));
            var1[62] = gb.CreateSPParameter("@nohpaw", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnohpaw.Text, typeof(string));
            var1[63] = gb.CreateSPParameter("@emailaw", ParameterDirection.Input, SqlDbType.VarChar, 50, txtemailaw.Text, typeof(string));
            var1[64] = gb.CreateSPParameter("@alamataw", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamataw.Text, typeof(string));
            var1[65] = gb.CreateSPParameter("@rtaw", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrtaw.Text, typeof(string));
            var1[66] = gb.CreateSPParameter("@rwaw", ParameterDirection.Input, SqlDbType.VarChar, 5, txtrwaw.Text, typeof(string));
            var1[67] = gb.CreateSPParameter("@kabupatenaw", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkabupatenaw.Text, typeof(string));
            var1[68] = gb.CreateSPParameter("@kelurahanaw", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkelurahanaw.Text, typeof(string));
            var1[69] = gb.CreateSPParameter("@kecamatanaw", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkecamatanaw.Text, typeof(string));
            var1[70] = gb.CreateSPParameter("@provinsiaw", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlprovinsiaw.SelectedValue, typeof(string));
            var1[71] = gb.CreateSPParameter("@kodeposaw", ParameterDirection.Input, SqlDbType.VarChar, 6, txtkodeposaw.Text, typeof(string));
            var1[72] = gb.CreateSPParameter("@pathfoto1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto1.Text, typeof(string));
            var1[73] = gb.CreateSPParameter("@filefoto1", ParameterDirection.Input, SqlDbType.VarChar, 60, txtfilename1.Text, typeof(string));
            var1[74] = gb.CreateSPParameter("@pathfoto2", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto2.Text, typeof(string));
            var1[75] = gb.CreateSPParameter("@filefoto2", ParameterDirection.Input, SqlDbType.VarChar, 60, txtfilename2.Text, typeof(string));
            var1[76] = gb.CreateSPParameter("@pathfoto3", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto3.Text, typeof(string));
            var1[77] = gb.CreateSPParameter("@filefoto3", ParameterDirection.Input, SqlDbType.VarChar, 60, txtfilename3.Text, typeof(string));
            var1[78] = gb.CreateSPParameter("@pathfoto4", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto4.Text, typeof(string));
            var1[79] = gb.CreateSPParameter("@filefoto4", ParameterDirection.Input, SqlDbType.VarChar, 60, txtfilename4.Text, typeof(string));
            var1[80] = gb.CreateSPParameter("@pathfoto5", ParameterDirection.Input, SqlDbType.VarChar, 250, txtfoto5.Text, typeof(string));
            var1[81] = gb.CreateSPParameter("@filefoto5", ParameterDirection.Input, SqlDbType.VarChar, 60, txtfilename5.Text, typeof(string));
            var1[82] = gb.CreateSPParameter("@tanggal_pengajuan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglrealisasi.Text, typeof(DateTime));
            var1[83] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));
            var1[84] = gb.CreateSPParameter("@Kodesimulasi", ParameterDirection.Input, SqlDbType.VarChar, 2, txtkodesimulasi.Text, typeof(string));
            var1[85] = gb.CreateSPParameter("@sumberdana", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsumberdana.SelectedValue, typeof(string));
            var1[86] = gb.CreateSPParameter("@ProgramPinjaman", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlprogrampinjaman.SelectedValue, typeof(string));
            var1[87] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlproduk.SelectedValue, typeof(string));
            var1[88] = gb.CreateSPParameter("@kode_kategori", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkategori.SelectedValue, typeof(string));
            var1[89] = gb.CreateSPParameter("@permohonan_jangka_bulan", ParameterDirection.Input, SqlDbType.Int, 0, ddljangkawaktu.SelectedValue, typeof(string));
            var1[90] = gb.CreateSPParameter("@permohonan_jangka_tahun", ParameterDirection.Input, SqlDbType.Int, 0, txtjangkatahun.Text, typeof(string));
            var1[91] = gb.CreateSPParameter("@tgl_Jatuh_Tempo", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljatuhtempo.Text, typeof(DateTime));
            var1[92] = gb.CreateSPParameter("@permohonan_plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
            var1[93] = gb.CreateSPParameter("@permohonan_pelunasan", ParameterDirection.Input, SqlDbType.Money, 0, txtpelunasan.Text, typeof(decimal));
            
                var1[94] = gb.CreateSPParameter("@angsuranblokir", ParameterDirection.Input, SqlDbType.Int, 0,int.Parse(ddlangsurandimukablokir.SelectedValue), typeof(int));
            var1[95] = gb.CreateSPParameter("@angsuranmukaangsuran", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlangsurandimuka.SelectedValue), typeof(int));
            var1[96] = gb.CreateSPParameter("@angsuranmukaasuransi", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlangsurandimukaasuransi.SelectedValue), typeof(int));
            var1[97] = gb.CreateSPParameter("@materai", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlmaterai.SelectedValue), typeof(int));
            var1[98] = gb.CreateSPParameter("@provisi", ParameterDirection.Input, SqlDbType.Float, 0, decimal.Parse(ddlprovisi.SelectedValue), typeof(decimal));
            var1[99] = gb.CreateSPParameter("@mutasikantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlmutasikantorbayar.SelectedValue, typeof(string));
            var1[100] = gb.CreateSPParameter("@kodeao",ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
            var1[101] = gb.CreateSPParameter("@kodenonao", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlnonao.SelectedValue, typeof(string));
            var1[102] = gb.CreateSPParameter("@namaoutput", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnama.Text, typeof(string));
            var1[103] = gb.CreateSPParameter("@statuspinjamanoutput", ParameterDirection.Input, SqlDbType.VarChar, 30, txtstatuspinjaman.Text, typeof(string));
            var1[104] = gb.CreateSPParameter("@usiapeminjam", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusiapinjaman.Text), typeof(int));

            var1[105] = gb.CreateSPParameter("@usiaasuransi", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusiaasuransi.Text), typeof(int));
            var1[106] = gb.CreateSPParameter("@usiaanak1", ParameterDirection.Input, SqlDbType.Int, 0, usia1, typeof(int));
            var1[107] = gb.CreateSPParameter("@usiaanak2", ParameterDirection.Input, SqlDbType.Int, 0, usia2, typeof(int));
            var1[108] = gb.CreateSPParameter("@plafonmax", ParameterDirection.Input, SqlDbType.Money, 0, txtplafonmax.Text, typeof(decimal));
            var1[109] = gb.CreateSPParameter("@jangkawaktumax", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtjangkawaktumax.Text), typeof(int));
            var1[110] = gb.CreateSPParameter("@rateasuransi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateasuransi.Text, typeof(decimal));
            var1[111] = gb.CreateSPParameter("@ratebungapinjaman", ParameterDirection.Input, SqlDbType.Decimal, 0, txtratebungapinjaman.Text, typeof(decimal));

            var1[112] = gb.CreateSPParameter("@angsuranrp", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuran.Text, typeof(decimal));
            var1[113] = gb.CreateSPParameter("@angsuranpersen", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpsnangsuran.Text, typeof(decimal));

            var1[114] = gb.CreateSPParameter("@asuransirp", ParameterDirection.Input, SqlDbType.Money, 0, txtasuransi.Text, typeof(decimal));
            var1[115] = gb.CreateSPParameter("@asuransipersen", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpersenasuransi.Text, typeof(decimal));

            var1[116] = gb.CreateSPParameter("@totalrp", ParameterDirection.Input, SqlDbType.Money, 0, txttotal.Text, typeof(decimal));
            var1[117] = gb.CreateSPParameter("@totalpersen", ParameterDirection.Input, SqlDbType.Decimal, 0, txttotalpersen.Text, typeof(decimal));

            var1[118] = gb.CreateSPParameter("@sisagajirp", ParameterDirection.Input, SqlDbType.Money, 0, txtsisagaji.Text, typeof(decimal));
            var1[119] = gb.CreateSPParameter("@sisagajipersen", ParameterDirection.Input, SqlDbType.Decimal, 0, txtsisagajipersen.Text, typeof(decimal));


            var1[120] = gb.CreateSPParameter("@provisirp", ParameterDirection.Input, SqlDbType.Money, 0, txtprovisi.Text, typeof(decimal));
            var1[121] = gb.CreateSPParameter("@provisipersen", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpersenprovisi.Text, typeof(decimal));

            var1[122] = gb.CreateSPParameter("@adminbankrp", ParameterDirection.Input, SqlDbType.Money, 0, txtadminbank.Text, typeof(decimal));
            var1[123] = gb.CreateSPParameter("@adminbankpersen", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpersenadminbank.Text, typeof(decimal));

            var1[124] = gb.CreateSPParameter("@keanggotaan", ParameterDirection.Input, SqlDbType.Money, 0, txtkeanggotaan.Text, typeof(decimal));

            var1[125] = gb.CreateSPParameter("@materairp", ParameterDirection.Input, SqlDbType.Money, 0, txtmaterai.Text, typeof(decimal));
            var1[126] = gb.CreateSPParameter("@materaikali", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtmateraipcs.Text), typeof(int));

            var1[127] = gb.CreateSPParameter("@angsuranblokirrp", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranblokir.Text, typeof(decimal));
            var1[128] = gb.CreateSPParameter("@angsuranblokirkali", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtangsuranblokirkali.Text), typeof(int));

            var1[129] = gb.CreateSPParameter("@angsurandimukaangsuranrp", ParameterDirection.Input, SqlDbType.Money, 0, txtangsurandimuka.Text, typeof(decimal));
            var1[130] = gb.CreateSPParameter("@angsurandimukaangsurankali", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtangsurandimukakali.Text), typeof(int));


            var1[131] = gb.CreateSPParameter("@angsurandimukaasuransirp", ParameterDirection.Input, SqlDbType.Money, 0, txtangdimukaasuransi.Text, typeof(decimal));
            var1[132] = gb.CreateSPParameter("@angsurandimukaasuransikali", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtangdimukaasuransikali.Text), typeof(int));

            var1[133] = gb.CreateSPParameter("@biayaadminrp", ParameterDirection.Input, SqlDbType.Money, 0, txtbiayaadmin.Text, typeof(decimal));
            var1[134] = gb.CreateSPParameter("@pelunasanrp", ParameterDirection.Input, SqlDbType.Money, 0, txtpelunasanRp.Text, typeof(decimal));
            var1[135] = gb.CreateSPParameter("@terimabersihrp", ParameterDirection.Input, SqlDbType.Money, 0, txtterimabersihrp.Text, typeof(decimal));
            var1[136] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
                var1[137] = gb.CreateSPParameter("@jeniskelaminpensiun", ParameterDirection.Input, SqlDbType.VarChar, 2, ddljeniskelaminpensiun.SelectedValue, typeof(string));
                var1[138] = gb.CreateSPParameter("@nodapem", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnodapem.Text, typeof(string));

                var1[139] = gb.CreateSPParameter("@tgllahiranak3", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglLahirAnak3Value, typeof(string));

                var1[140] = gb.CreateSPParameter("@usiaanak3", ParameterDirection.Input, SqlDbType.Int, 0, usia3, typeof(int));
                var1[141] = gb.CreateSPParameter("@namaanak1", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamaanak1.Text, typeof(string));
                var1[142] = gb.CreateSPParameter("@namaanak2", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamaanak2.Text, typeof(string));
                var1[143] = gb.CreateSPParameter("@namaanak3", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamaanak3.Text, typeof(string));
                var1[144] = gb.CreateSPParameter("@jenis_kelaminanak1", ParameterDirection.Input, SqlDbType.VarChar, 2, ddljeniskelaminanak1.SelectedValue, typeof(string));
                var1[145] = gb.CreateSPParameter("@jenis_kelaminanak2", ParameterDirection.Input, SqlDbType.VarChar, 2, ddljeniskelaminanak2.SelectedValue, typeof(string));
                var1[146] = gb.CreateSPParameter("@jenis_kelaminanak3", ParameterDirection.Input, SqlDbType.VarChar, 2, ddljeniskelaminanak3.SelectedValue, typeof(string));
                var1[147] = gb.CreateSPParameter("@statusnikahanak1", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikahanak1.SelectedValue, typeof(string));
                var1[148] = gb.CreateSPParameter("@statusnikahanak2", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikahanak2.SelectedValue, typeof(string));
                var1[149] = gb.CreateSPParameter("@statusnikahanak3", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikahanak3.SelectedValue, typeof(string));
                var1[150] = gb.CreateSPParameter("@usiajatuhtempo", ParameterDirection.Input, SqlDbType.Int, 0, usiajatuhtempo, typeof(int));
                var1[151] = gb.CreateSPParameter("@BankRekeningtrf", ParameterDirection.Input, SqlDbType.VarChar, 50, TxtNamaBankRekeningTransfer.Text, typeof(string));
                var1[152] = gb.CreateSPParameter("@NoRekeningtrf", ParameterDirection.Input, SqlDbType.VarChar, 50, TxtNomorBankRekeningTransfer.Text, typeof(string));
                var1[153] = gb.CreateSPParameter("@NamaRekeningtrf", ParameterDirection.Input, SqlDbType.VarChar, 80, TxtAtasNamaBankRekeningTransfer.Text, typeof(string));
                var1[154] = gb.CreateSPParameter("@BankRekeningKB", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaBankkantorbayar.Text, typeof(string));
                var1[155] = gb.CreateSPParameter("@NoRekeningKB", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekeningkantorbayar.Text, typeof(string));
                var1[156] = gb.CreateSPParameter("@NamaRekeningKB", ParameterDirection.Input, SqlDbType.VarChar, 80, txtatasnamarekeningkantorbayar.Text, typeof(string));
                var1[157] = gb.CreateSPParameter("@ratebungapinjamanefektif", ParameterDirection.Input, SqlDbType.Decimal, 0, txtratebungaefektif.Text, typeof(decimal));
                var1[158] = gb.CreateSPParameter("@pathpdf6", ParameterDirection.Input, SqlDbType.VarChar, 250, txtPdf6.Text, typeof(string));
                var1[159] = gb.CreateSPParameter("@filepdf6", ParameterDirection.Input, SqlDbType.VarChar, 60, txtfilename6.Text, typeof(string));
                var1[160] = gb.CreateSPParameter("@nik_a", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnikA.Text, typeof(string));
                var1[161] = gb.CreateSPParameter("@graceperiode", ParameterDirection.Input, SqlDbType.Money, 30, txtgraceperiode.Text, typeof(decimal));
                var1[162] = gb.CreateSPParameter("@PK", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlPk.Text, typeof(string));
                var1[163] = gb.CreateSPParameter("@statustunjanganistrianak", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlTunjanganIstriAnak.SelectedValue, typeof(string));
                var1[164] = gb.CreateSPParameter("@Form_Simulasi_PK2", ParameterDirection.Input, SqlDbType.Structured, 0, FormSimulasiPk2, typeof(DataTable));

                ge.ListDataset = gb.GetDataJustOutPut("KP_PERMOHONAN_KREDIT_NEW_TEST", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);

                    pnladd.Visible = false;
                    pnlIdentitas.Visible = false;
                    PnlFront.Visible = true;
                    pnlFilter.Visible = true;
                    //pnlJaminan.Visible = false;
                    //pnlPengajuan.Visible = false;
                    btnSimpan.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                    pnlUpdAsuransi.Visible = false;

                    btnTolakAntrian.Visible = false;
                    PNLKELUARGA.Visible = false;
                    pnlAlasanTolak.Visible = false;
                    txtnonasabah.Text = "";
                    Pnlpenerima.Visible = false;
                    Pnlsimulasi.Visible = false;
                    PnlSKPensiun.Visible = false;
                    pnlpinjaman.Visible = false;
                    nav.Visible = false;
                    BackNextAnggota.Visible = false;
                    BackNextAhliWaris.Visible = false;
                    BackNextSimulasi.Visible = false;
                    btnBackSimulasi.Visible = false;

                    pnlUpdAsuransi.Visible = false;
                    btnCancel.Visible = false;
                    PnlRekeningBank.Visible = false;
                    //btndokumen.Visible = false;
                  
                  
                    ddlstatus_SelectedIndexChanged(sender, e);
                    txtnonasabah.Focus();
                }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                btnSimpan.Enabled = true;
            }

            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    private void setPK2()
    {

        FormSimulasiPk2.Columns.Clear();
        FormSimulasiPk2.Columns.Add(new DataColumn("No", typeof(int)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsuranblokir", typeof(string)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsuranmukaangsuran", typeof(string)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsuranmukaasuransi", typeof(string)));
        FormSimulasiPk2.Columns.Add(new DataColumn("provisi", typeof(string)));

        FormSimulasiPk2.Columns.Add(new DataColumn("plafondmax", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("rateasuransi", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("ratebungapinjaman", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("ratebungapinjamanefektif", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsuranrp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsuranpersen", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("plafond", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("asuransirp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("asuransipersen", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("totalrp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("totalpersen", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("sisagajirp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("sisagajipersen", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("provisirp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("provisipersen", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("adminbankrp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("adminbankpersen", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("keanggotaan", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("materairp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("materaikali", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsuranblokirrp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsuranblokirkali", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsurandimukaangsuranrp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsurandimukaangsurankali", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsurandimukaasuransirp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("angsurandimukaasuransikali", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("biayaadminrp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("pelunasanrp", typeof(decimal)));
        FormSimulasiPk2.Columns.Add(new DataColumn("terimabersihrp", typeof(decimal)));


        FormSimulasiPk2.Rows.Clear();
        DataRow RowPk2 = FormSimulasiPk2.NewRow();

        if(ddlPkChild.SelectedValue == "2" && Convert.ToDecimal(txtterimabersihrpChild.Text) > 0)
        {

            decimal plafondmax = Convert.ToDecimal(txtplafonmaxChild.Text);
            decimal rateasuransi = Convert.ToDecimal(txtrateasuransiChild.Text);
            decimal ratebungapinjaman = Convert.ToDecimal(txtratebungapinjamanChild.Text);
            decimal ratebungapinjamanefektif = Convert.ToDecimal(txtratebungaefektifChild.Text);
            decimal angsuranrp = Convert.ToDecimal(txtangsuranChild.Text);
            decimal angsuranpersen = Convert.ToDecimal(txtpsnangsuranChild.Text);
            decimal plafond = Convert.ToDecimal(txtplafonChild.Text);
            decimal asuransirp = Convert.ToDecimal(txtasuransiChild.Text);
            decimal asuransipersen = Convert.ToDecimal(txtpersenasuransi.Text);
            decimal totalrp = Convert.ToDecimal(txttotalChild.Text);
            decimal totalpersen = Convert.ToDecimal(txttotalpersenChild.Text);
            decimal sisagajirp = Convert.ToDecimal(txtsisagajiChild.Text);
            decimal sisagajipersen = Convert.ToDecimal(txtsisagajipersenChild.Text);
            decimal provisirp = Convert.ToDecimal(txtprovisiChild.Text);
            decimal provisipersen = Convert.ToDecimal(txtpersenprovisiChild.Text);
            decimal adminbankrp = Convert.ToDecimal(txtadminbankChild.Text);
            decimal adminbankpersen = Convert.ToDecimal(txtpersenadminbankChild.Text);
            decimal keanggotaan = Convert.ToDecimal(txtkeanggotaanChild.Text);
            decimal materairp = Convert.ToDecimal(txtmateraiChild.Text);
            decimal materaikali = Convert.ToDecimal(txtmateraipcsChild.Text);
            decimal angsuranblokirrp = Convert.ToDecimal(txtangsuranblokirChild.Text);
            decimal angsuranblokirkali = Convert.ToDecimal(txtangsuranblokirkaliChild.Text);
            decimal angsurandimukaangsuranrp = Convert.ToDecimal(txtangsurandimukaChild.Text);
            decimal angsurandimukaangsurankali = Convert.ToDecimal(txtangsurandimukakaliChild .Text);
            decimal angsurandimukaasuransirp = Convert.ToDecimal(txtangdimukaasuransiChild.Text);
            decimal angsurandimukaasuransikali = Convert.ToDecimal(txtangdimukaasuransikaliChild.Text);
            decimal biayaadminrp = Convert.ToDecimal(txtbiayaadminChild.Text);
            decimal pelunasanrp = Convert.ToDecimal(txtpelunasanChild.Text);
            decimal terimabersihrp = Convert.ToDecimal(txtterimabersihrpChild.Text);


            RowPk2["No"] = 2;
            RowPk2["angsuranblokir"] = ddlangsurandimukablokirChild.Text;
            RowPk2["angsuranmukaangsuran"] = ddlangsurandimukaasuransi.Text;
            RowPk2["angsuranmukaasuransi"] = ddlangsurandimukaasuransiChild.Text;
            RowPk2["provisi"] = ddlprovisiChild.Text;
            RowPk2["plafondmax"] = plafondmax;
            RowPk2["rateasuransi"] = rateasuransi;
            RowPk2["ratebungapinjaman"] = ratebungapinjaman;
            RowPk2["ratebungapinjamanefektif"] = ratebungapinjamanefektif;
            RowPk2["angsuranrp"] = angsuranrp;
            RowPk2["angsuranpersen"] = angsuranpersen;
            RowPk2["plafond"] = plafond;
            RowPk2["asuransirp"] = asuransirp;
            RowPk2["asuransipersen"] = asuransipersen;
            RowPk2["totalrp"] = totalrp;
            RowPk2["totalpersen"] = totalpersen;
            RowPk2["sisagajirp"] = sisagajirp;
            RowPk2["sisagajipersen"] = sisagajipersen;
            RowPk2["provisirp"] = provisirp;
            RowPk2["provisipersen"] = provisipersen;
            RowPk2["adminbankrp"] = adminbankrp;
            RowPk2["adminbankpersen"] = adminbankpersen;
            RowPk2["keanggotaan"] = keanggotaan;
            RowPk2["materairp"] = materairp;
            RowPk2["materaikali"] = materaikali;
            RowPk2["angsuranblokirrp"] = angsuranblokirrp;
            RowPk2["angsuranblokirkali"] = angsuranblokirkali;
            RowPk2["angsurandimukaangsuranrp"] = angsurandimukaangsuranrp;
            RowPk2["angsurandimukaangsurankali"] = angsurandimukaangsurankali;
            RowPk2["angsurandimukaasuransirp"] = angsurandimukaasuransirp;
            RowPk2["angsurandimukaasuransikali"] = angsurandimukaasuransikali;
            RowPk2["biayaadminrp"] = biayaadminrp;
            RowPk2["pelunasanrp"] = pelunasanrp;
            RowPk2["terimabersihrp"] = terimabersihrp;
            FormSimulasiPk2.Rows.Add(RowPk2);
        }

    }


    protected string btnafterDelete()
    {
        
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermohonanPinjamanKreditMultigunaNew.aspx");
        ddlcabang.Focus();
    }
    //protected void txttglrealisasi_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
    //    DateTime ToDat1e = DateTime.ParseExact(txttglrealisasi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

    //    if (ToDat1e < currentdate)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi tidak boleh kurang dari Tanggal hari ini');", true);
    //        txttglrealisasi.Focus();
    //    }
    //    else
    //    { 

    //    }


    //}


  


    protected void btnSearchx_Click(object sender, EventArgs e)
    {
        try
        {


            btnSimpan.Enabled = true;
            string no_rekening = "";
            
            HDRegister.Value = "";
            string[] splitdata = txtnonasabah.Text.Split('-');
            string nonasabah = splitdata[0];
            string KTP = "";
            string KOPERASI = "";
            string WOSTATUS = "0";
            string KODEAO = "";
            string DATAKODEAOADA = "0";
            string flag_bayar = "0";
            int flagdata = 0;
            int Setoranx = 0;


            ClearData();
            Opendata();

            txttglrealisasi.Enabled = true;
            //ddlprodukpengajuan.Enabled = true;
            //ddljaminan.Enabled = true;
            //txtketeranganJaminan.Enabled = true;
            //ddljangka.Enabled = true;
            //btntambah.Enabled = true;
            //btnminus.Enabled = true;
            btnSimpan.Enabled = true;
            //btncekcadangan.Enabled = true;
            //ddljenistagihan.Enabled = true;
            txtplafon.Text = string.Format("{0:N0}", "0");
            txtidpermohonan.Text = "";

            //ddljenispermohonan.SelectedValue = "A";



            ddldatasimulasi.SelectedValue = "0";
            //ddllistsimulasi.SelectedValue = "-";
            ddldatasimulasi_SelectedIndexChanged(sender, e);
            ddlprogrampinjaman.SelectedValue = "A";
            ddlprogrampinjaman_SelectedIndexChanged(sender, e);
            //ddlmajuhari_SelectedIndexChanged(sender, e);

            if (txtnonasabah.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Anggota belum di isi');", true);
                txtnonasabah.Focus();
            }
            else if (ddlcabang.SelectedValue != txtnonasabah.Text.Substring(0, 3))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Anggota yang di input tidak terdapat di cabang Anda');", true);

            }

            else
            {


                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAAN", typeof(string));
                var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                {

                    KTP = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                    KOPERASI = ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString();
                    WOSTATUS = ge.ListDataset.Tables[0].Rows[0]["status_wo"].ToString();
                    KODEAO = ge.ListDataset.Tables[0].Rows[0]["kode_ref"].ToString();
                    flagdata= int.Parse(ge.ListDataset.Tables[0].Rows[0]["flagdata"].ToString());
                    Setoranx = int.Parse(ge.ListDataset.Tables[0].Rows[0]["Setoranx"].ToString()); 
                    flag_bayar =ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString();
                    if (flag_bayar == "0")
                    {
                        flagdata = 1;
                    }
                    if (Setoranx == 1)
                    {
                        flagdata = 1;
                    }



                    if (flagdata > 0)
                    {


                        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
                        var2x[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                        var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
                        var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var2x[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var2x, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota untuk Permohonan tersebut sudah ada dan belum di proses');", true);

                        txtnonasabah.Focus();
                    }
                    else
                    {


                        DefaultClass.Global.SqlHelper[] var3x = new DefaultClass.Global.SqlHelper[5];
                        var3x[0] = gb.CreateSPParameter("@kode_AO", ParameterDirection.Input, SqlDbType.VarChar, 50, KODEAO, typeof(string));
                        var3x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKAOAKTIF", typeof(string));
                        var3x[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var3x[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var3x[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3x, "constrmf");

                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode AO tidak ada dalam list AO yang aktif,rubah data anggota di register calon anggota');", true);

                            //txtnonasabah.Focus();
                            DATAKODEAOADA = "0";
                        }

                        else
                        {
                            DATAKODEAOADA = "1";
                        }

                        //else
                        //{

                            DefaultClass.Global.SqlHelper[] varkop1 = new DefaultClass.Global.SqlHelper[7];
                            varkop1[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, KTP, typeof(string));
                            varkop1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLUNASKTPKONSOL", typeof(string));
                            varkop1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            varkop1[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, "I", typeof(string));
                            varkop1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                            varkop1[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
                            varkop1[6] = gb.CreateSPParameter("@Kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30, KODEAO, typeof(string));


                             ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", varkop1, "constrmf");
                            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                            {


                                pnlpinjaman.Visible = true;
                                Pnlpenerima.Visible = true;
                                Pnlsimulasi.Visible = true;
                                PnlSKPensiun.Visible = true;
                                pnlpinjaman.Visible = true;
                                PnlRekeningBank.Visible = true;
                                BackNextAnggota.Visible = true;
                                BackNextAhliWaris.Visible = true;
                                BackNextSimulasi.Visible = true;
                                btnBackSimulasi.Visible = true;
                                pnlUpdAsuransi.Visible = false;
                                btnCancel.Visible = true;
                                //btndokumen.Visible = true;
                                PNLKELUARGA.Visible = true;
                                pnlUpdAsuransi.Visible = true;
                                pnladd.Visible = true;
                                pnlIdentitas.Visible = true;
                                PnlFront.Visible = false;
                                pnlFilter.Visible = false;
                                //pnlJaminan.Visible = true;
                                //pnlPengajuan.Visible = true;
                                btnSimpan.Visible = true;
                                BtnApproveAntrian.Visible = false;
                                BtnBatalkanmohon.Visible = false;
                                BtnApproveAntrian.Visible = false;
                                BtnBatalkanmohon.Visible = false;

                                btnTolakAntrian.Visible = false;
                                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
                                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");
                                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);
                                    txtnonasabah.Focus();
                                }
                                else
                                {
                                    nav.Visible = true;
                                    btnSimpan.Visible = true;
                                    txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
                                    txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                                    txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
                                    txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                                    txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                                    ddlpropinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                                    txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                                    txtkotaktp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                                    txtkabupatenktp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                                    txttempatlahirktp.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                                    txttgllahirktp.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                                    txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                                    txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                                    txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                                    ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                                    txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                                    txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                                    ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                                    ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                                    txtjenisusahaKtp.Text= ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();
                                    ddljeniskelaminpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();

                                    //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                                    //ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                                    //txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

                                    ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                                    //txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                                    //txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

                                    txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                                    txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                                    ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                                    
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
                                    if (DATAKODEAOADA == "1")
                                    {
                                    ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                                    }
                                
                                    HDKodePos.Value = txtkodeposktp.Text;
                                    //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                                    HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                                    ddljenkelktp.SelectedValue = HDGender.Value;

                                    if (ddlstatusrumah.SelectedValue == "8")
                                    {




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

                                        txtalamatrumahtinggal1.Enabled = true;
                                        txtrttp.Enabled = true;
                                        txtrwtpt.Enabled = true;
                                        txtkelurahantpt.Enabled = true;
                                        txtkecamatantpt.Enabled = true;
                                        ddlprovinsitpt.Enabled = true;
                                        txtkodepostpt.Enabled = true;
                                    }


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


                                    }
                                    else
                                    {

                                        txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                                        txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                                        txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                                        var row = ge.ListDataset.Tables[0].Rows[0];
                                        string tgllahirpasangan = null;

                                        if (row["tanggal_lahir_pasangan"] != DBNull.Value)
                                        {
                                            DateTime tgl;
                                            if (DateTime.TryParse(row["tanggal_lahir_pasangan"].ToString(), out tgl))
                                            {
                                                tgllahirpasangan = tgl.ToString("dd/MM/yyyy");
                                            }
                                        }

                                        txttgllahirpasangan.Text = tgllahirpasangan ?? string.Empty;
                                    }

                                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                                  //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                                    //txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

                                    //txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();

                                 


                                    //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();

                                    //txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();

                                    //txtktppenjamin.Text = "0";
                                    //txtnamapenjamin.Text = "-";
                                    //txtpendapatanperbulan.Text = "0";
                                    //txtjaminantambahan.Text = "-";
                                    //txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                                    //txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();

                               
                                    DefaultClass.Global.SqlHelper[] varcad = new DefaultClass.Global.SqlHelper[3];
                                    varcad[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah.Trim(), typeof(string));
                                    varcad[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCADANGAN", typeof(string));
                                    varcad[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varcad, "constrmf");

                                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                                    {
                                        //txtcadanganRekening.Text = "";
                                        //txtcadsebelumnya.Text = "0";
                                    }
                                    else
                                    {


                                        //txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                                        //txtcadsebelumnya.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["sisacadangan"].ToString());
                                        //txtcadsebelumnya.Text = txtcadsebelumnya.Text.Replace("Rp", "");

                                    }




                                      ddlstatusrumah_SelectedIndexChanged(sender, e);


                                    ddlpenerimapensiun_SelectedIndexChanged(sender, e);

                                    //var notifcatatatan = "";

                                    //DefaultClass.Global.SqlHelper[] varnotif = new DefaultClass.Global.SqlHelper[3];
                                    //varnotif[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah.Trim(), typeof(string));
                                    //varnotif[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKNOTIFCATAT", typeof(string));
                                    //varnotif[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                    //ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varnotif, "constrmf");

                                    //if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                                    //{
                                    //    notifcatatatan = ge.ListDataset.Tables[0].Rows[0]["alasan_ttp"].ToString();
                                    //    if (notifcatatatan.Length > 5)
                                    //    {
                                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Notifikasi Anggota Tolong Dibaca... " + notifcatatatan + " ');", true);

                                    //    }
                                       
                                    //}
                                   




                                }


                            }

                            else
                            {
                                no_rekening = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + no_rekening + " belum Lunas, Mohon Lunasi terlebih dahulu');", true);
                                txtnonasabah.Focus();
                            }



                        //}
                    }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota tersebut adalah Anggota Full Up yang tidak ada transaksi pinjaman, lakukan Perubahan menjadi Anggota Baru /Calon Anggota process Full UP ');", true);
                        txtnonasabah.Focus();
                    }




                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tersebut Belum Menjadi Anggota Koperasi Atau Tidak Aktif/ sudah di Tutup! ');", true);
                    txtnonasabah.Focus();
                }




            }

        }
         

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
       
    }

    protected void UploadFileKTP(object sender, EventArgs e)
    {



        if (fileuploadktp.HasFile)
        {


            var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

            if (fileuploadktp.PostedFile.ContentLength > maxFileSize)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

            }

            else
            {

                string namafile;
                string filename;
                string filenamex;

                DateTime now = DateTime.Now;
                namafile = txtnoidentitasktp.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "KTP";
                string folderPath = Server.MapPath("~/Files/Anggota/thumb");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                filename = namafile + ".jpg";
                filenamex = namafile + "OK.jpg";


                //create temp
                string path = Server.MapPath("~/Files/Anggota/thumb/" + namafile + ".jpg");
                fileuploadktp.SaveAs(path);
                //create temp

                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                int newHeight = 200;
                int newWidth = Convert.ToInt32(aspectRatio * newHeight);
                System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
                System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbBitmap.Save(Server.MapPath("~/Files/Anggota/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();
                fotopathktp = "~/Files/Anggota/" + filenamex;
                txtpathktp.Text = fotopathktp;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not.
                {
                    file.Delete();
                }

                imgktp.ImageUrl = "~/Files/Anggota/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();
            }

        }



    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

        
        btnSimpan.Enabled = true;
        string no_rekening = "";

        string[] splitdata = txtnonasabah.Text.Split('-');
        string nonasabah = splitdata[0];

       
        if (txtnonasabah.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah belum di isi');", true);

        }
        
        else
        {

         
            pnladd.Visible = true;
            pnlIdentitas.Visible = true;
            PnlFront.Visible = false;
            pnlFilter.Visible = false;
            //pnlJaminan.Visible = true;
            //pnlPengajuan.Visible = true;
            btnSimpan.Visible = true;
            btnSimpan.Enabled = true;
            BtnApproveAntrian.Visible = false;
            BtnBatalkanmohon.Visible = false;
            BtnApproveAntrian.Visible = false;
            BtnBatalkanmohon.Visible = false;

            btnTolakAntrian.Visible = false;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);

            }
            else
            {
                btnSimpan.Visible = true;
                //if (ddlSurvey.SelectedIndex == 1)
                //{
                //    btnNextKriteria.Visible = false;

                //}
                //else if (ddlSurvey.SelectedIndex == 2)
                //{

                //    btnSimpan.Visible = false;
                //    btnNextKriteria.Visible = true;
                //}

                //Panel Data nasabah 1

                //data ktp
                txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
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
                txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
            
                txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
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


                }
                else
                {

                    txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                    txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                    txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                        var row = ge.ListDataset.Tables[0].Rows[0];
                        string tgllahirpasangan = null;

                        if (row["tanggal_lahir_pasangan"] != DBNull.Value)
                        {
                            DateTime tgl;
                            if (DateTime.TryParse(row["tanggal_lahir_pasangan"].ToString(), out tgl))
                            {
                                tgllahirpasangan = tgl.ToString("dd/MM/yyyy");
                            }
                        }

                        txttgllahirpasangan.Text = tgllahirpasangan ?? string.Empty;
                    }

                imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                              //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                //txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                //txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString(); // "?r=" + DateTime.Now.Ticks.ToString();

                //txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                //txtpathKK.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();

                //Imagenpwp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                //ImageKtppasangan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                //Imagekk.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();

                //data pekerjaan



                //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();

                //txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();

                //txtktppenjamin.Text = "0";
                //txtnamapenjamin.Text = "-";
                //txtpendapatanperbulan.Text = "0";
                //txtjaminantambahan.Text = "-";
                //txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                //txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();

        


                HDKodePos.Value = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();


                ddlpenerimapensiun_SelectedIndexChanged(sender, e);


            }








        }
        }


        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    [WebMethod]
    public static string GetNoNasabah(string InputText, string SessionBranchID)
    {
        List<string[]> listResult = new List<string[]>();

        string no_nasabah = InputText;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_GetNoNasabahForPermohonan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

    public void BindGridEntity()
    {

        int tolak = 0;
        if (ddlstatus.SelectedValue == "11")
        {
            tolak = 1;
        }
        else
        {
            tolak = 0;
        }

        DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[6];
        x[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        x[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWSTATUSTRX", typeof(string));
        x[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        x[4] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlstatus.SelectedValue, typeof(string));
        x[5] = gb.CreateSPParameter("@tolak", ParameterDirection.Input, SqlDbType.Int,0,tolak, typeof(int));
        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

      

    }





    
    private void ClearData()

    {

        try
        {

            InsertCombo();


            ddlpenerimapensiun.SelectedValue = "0";
        txtidktppp.Text = "";
        txtnamapp.Text = "";
        txttempatlahirpp.Text = "";
        txtalamatpp.Text = "";
        txtnamaaw.Text = "";
        txtnamaibukandungaw.Text = "";
        txthubunganaw.Text = "";
        txttempatlahiraw.Text = "";
        txtalamataw.Text = "";
        txtnopen.Text = "";
        txtnip.Text = "";
        txtnamasesuaisk.Text = "";
        txtnosk.Text = "";
        txtnodapem.Text = "";
        txtgajidisplay.Text = "0";
        txtgapok.Text = "0";
        ddljangkawaktu.Items.Insert(0, new ListItem("0", "0"));
        ddljangkawaktu.SelectedValue = "0";
        txtplafon.Text = "0";

        ddlinstansi.SelectedValue = "";
        txtnopen.Text = "";
        txtnamasesuaisk.Text = "";
        ddlstatusnikah.SelectedValue = "-";
        ddlpensiun.SelectedValue = "";
        ddlkodejiwa.SelectedValue = "";
        txttgllahir.Text = "";
        txtgajidisplay.Text = "0";
        txttgllahiranak1.Text = "";
        txttgllahiranak2.Text = "";

        ddlKantorBayar.SelectedValue = "";
        ddlsumberdana.SelectedValue = "";
        ddlprogrampinjaman.SelectedValue = "";
        ddlproduk.SelectedValue = "";
        ddlkategori.SelectedValue = "";
        txtplafon.Text = "0";
        ddlangsurandimukablokir.SelectedValue = "0";
        txtpelunasan.Text = "0";
        ddlangsurandimuka.SelectedValue = "0";
        ddlangsurandimukaasuransi.SelectedValue = "0";
        ddlmaterai.SelectedValue = "0";


        //ddljeniskelaminpp.SelectedValue = "-";
        txttgllahirpp.Text = "";
        //ddlagamapp.SelectedValue = "";
        txtnohppp.Text = "0";
        txtemailpp.Text = "";
        txtrtpp.Text = "0";
        txtrwpp.Text = "0"; ;
        txtkabupatenpp.Text = "-";
        txtkelurahanpp.Text = "-";
        txtkecamatanpp.Text = "-";
        //ddlprovinsipp.SelectedValue = "";
        txtkodepospp.Text = "0";
        //ddljeniskelaminaw.SelectedValue = "";
        txttgllahiraw.Text = "";
        txtnohpaw.Text = "0";
        txtemailaw.Text = "";
        txtrtaw.Text = "0";
        txtrwaw.Text = "0";
        txtkabupatenaw.Text = "-";
        txtkelurahanaw.Text = "-";
        txtkecamatanaw.Text = "-";
        //ddlprovinsiaw.SelectedValue = "";
        txtkodeposaw.Text = "0";
        ddldatasimulasi.SelectedValue = "0";
        txtglskpensiun.Text = "";
        txtnonpwp.Text = "0";
        txttunjistri.Text = "0";
        txttunjangananak.Text = "0";
        txttunjanganberas.Text = "0";
        txttunjanganlain.Text = "0";
        txtpotonganpajak.Text = "0";
        txtpotonganaskes.Text = "0";
        txtpotonganlain.Text = "0";
        txtnamaanak1.Text = "-";
        txtnamaanak2.Text = "-";
        txtnamaanak3.Text = "-";
        txttgllahiranak3.Text = "";
        ddljeniskelaminanak1.SelectedValue = "-";
        ddljeniskelaminanak2.SelectedValue = "-";
        ddljeniskelaminanak3.SelectedValue = "-";
        ddlstatusnikahanak1.SelectedValue = "-";
        ddlstatusnikahanak2.SelectedValue = "-";
        ddlstatusnikahanak3.SelectedValue = "-";
        ddlmutasikantorbayar.SelectedValue = "-";

        TxtAtasNamaBankRekeningTransfer.Text = "-";
        TxtNamaBankRekeningTransfer.Text = "-";
        TxtNomorBankRekeningTransfer.Text = "-";

        txtnamaBankkantorbayar.Text = "-";
        txtnorekeningkantorbayar.Text = "-";
        txtatasnamarekeningkantorbayar.Text = "-";

            ddlnonao.SelectedValue = "-";
    

        imgfoto1.ImageUrl = "";
        imgfoto2.ImageUrl = "";
        imgfoto3.ImageUrl = "";
        imgfoto4.ImageUrl = "";
        imgfoto5.ImageUrl = "";
        txtfoto1.Text = "";
        txtfoto2.Text = "";
        txtfoto3.Text = "";
          txtfoto4.Text = "";  
        txtfoto5.Text = "";
        txtPdf6.Text = "";
        txtfilename1.Text = "";
        txtfilename2.Text = "";
         txtfilename3.Text = ""; 
        txtfilename4.Text = "";
         txtfilename5.Text = "";
        txtnama.Text = "";
        txtstatuspinjaman.Text = "";
        txtusiapinjaman.Text = "0";
        txtusiaasuransi.Text = "0";
        txtusiaanak1.Text = "0";
        txtusiaanak2.Text = "0";
        txtplafonmax.Text = "0";
        txtjangkawaktumax.Text = "0";
        txtusiajthtempo.Text = "0";
        txtrateasuransi.Text = "0";
        txtratebungapinjaman.Text = "0";
        txtangsuran.Text = "0";
        txtpsnangsuran.Text = "0";
        txtasuransi.Text = "0";
        txtpersenasuransi.Text = "0";
        txttotal.Text = "0";
        txttotalpersen.Text = "0";
        txtsisagaji.Text = "0";
        txtsisagajipersen.Text = "0";
        txtprovisi.Text = "0";
        txtpersenprovisi.Text = "0";
        txtadminbank.Text = "0";
        txtpersenadminbank.Text = "0";
        txtkeanggotaan.Text = "0";
        txtmaterai.Text = "0";
        ddljangkawaktu.SelectedValue = "0";
        txtmateraipcs.Text = "0";
        txtangsuranblokir.Text = "0";
        txtangsuranblokirkali.Text = "0";
        txtangsurandimuka.Text = "0";
        txtangsurandimukakali.Text = "0";
        txtangdimukaasuransi.Text = "0";
        txtangdimukaasuransikali.Text = "0";
        txtbiayaadmin.Text = "0";
        txtpelunasanRp.Text = "0";
        txtterimabersihrp.Text = "0";
            txttgljatuhtempo.Text = "";
            txtjangkatahun.Text = "0";

    }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
       
    }

    private void Opendata()

    {
        ddlpenerimapensiun.Enabled = true;
        txtidktppp.Enabled = true;
        txtnamapp.Enabled = true;
        txttempatlahirpp.Enabled = true;
        txtalamatpp.Enabled = true;
        txtnamaaw.Enabled = true;
        txtnamaibukandungaw.Enabled = true;
        txthubunganaw.Enabled = true;
        txttempatlahiraw.Enabled = true;
        txtalamataw.Enabled = true;
        txtnopen.Enabled = true;
        txtnip.Enabled = true;
        txtnamasesuaisk.Enabled = true;
        txtnosk.Enabled = true;
        txtnodapem.Enabled = true;
        txtgajidisplay.Enabled = true;
        txtgapok.Enabled = true;
        ddljangkawaktu.Enabled = true;
        txtplafon.Enabled = true;


        ddlinstansi.Enabled = true;
        txtnopen.Enabled = true;
        txtnamasesuaisk.Enabled = true;
        ddlstatusnikah.Enabled = true;
        ddlpensiun.Enabled = true;
        ddlkodejiwa.Enabled = true;
        txtmtpensiun.Enabled = true;
        txttgllahir.Enabled = true;
        txtgajidisplay.Enabled = true;
        txttgllahiranak1.Enabled = true;
        txttgllahiranak2.Enabled = true;
        txttglrealisasi.Enabled = true;
        ddlKantorBayar.Enabled = true;
        ddlsumberdana.Enabled = true;
        ddlprogrampinjaman.Enabled = true;
        ddlproduk.Enabled = true;
        ddlkategori.Enabled = true;
        ddljangkawaktu.Enabled = true;
        txtplafon.Enabled = true;
        ddlangsurandimukablokir.Enabled = true;
        txtpelunasan.Enabled = true;
        ddlangsurandimuka.Enabled = true;
        ddlangsurandimukaasuransi.Enabled = true;
        ddlmaterai.Enabled = true;
        ddlprovisi.Enabled = true;

        ddljeniskelaminpp.Enabled = true;
        txttgllahirpp.Enabled = true;
        ddlagamapp.Enabled = true;
        txtnohppp.Enabled = true;
        txtemailpp.Enabled = true;
        txtrtpp.Enabled = true;
        txtrwpp.Enabled = true;
        txtkabupatenpp.Enabled = true;
        txtkelurahanpp.Enabled = true;
        txtkecamatanpp.Enabled = true;
        ddlprovinsipp.Enabled = true;
        txtkodepospp.Enabled = true;
        ddljeniskelaminaw.Enabled = true;
        txttgllahiraw.Enabled = true;
        txtnohpaw.Enabled = true;
        txtemailaw.Enabled = true;
        txtrtaw.Enabled = true;
        txtrwaw.Enabled = true;
        txtkabupatenaw.Enabled = true;
        txtkelurahanaw.Enabled = true;
        txtkecamatanaw.Enabled = true;
        ddlprovinsiaw.Enabled = true;
        txtkodeposaw.Enabled = true;
        ddldatasimulasi.Enabled = true;
        txtglskpensiun.Enabled = true;
        txtnonpwp.Enabled = true;
        txttunjistri.Enabled = true;
        txttunjangananak.Enabled = true;
        txttunjanganberas.Enabled = true;
        txttunjanganlain.Enabled = true;
        txtpotonganpajak.Enabled = true;
        txtpotonganaskes.Enabled = true;
        txtpotonganlain.Enabled = true;
        txtnamaanak1.Enabled = true;
        txtnamaanak2.Enabled = true;
        txtnamaanak3.Enabled = true;
        txttgllahiranak3.Enabled = true;
        ddljeniskelaminanak1.Enabled = true;
        ddljeniskelaminanak2.Enabled = true;
        ddljeniskelaminanak3.Enabled = true;
        ddlstatusnikahanak1.Enabled = true;
        ddlstatusnikahanak2.Enabled = true;
        ddlstatusnikahanak3.Enabled = true;
        ddlmutasikantorbayar.Enabled = true;
        ddlAO.Enabled = true;
        ddlnonao.Enabled = true;
        fileupload1.Enabled = true;
        btnfoto1.Enabled = true;
        fileupload2.Enabled = true;
        btnfoto2.Enabled = true;
        fileupload3.Enabled = true;
        btnfoto3.Enabled = true;
        fileupload4.Enabled = true;
        btnfoto4.Enabled = true;
        ddljeniskelaminpensiun.Enabled = true;
        fileupload5.Enabled = true;
        btnfoto5.Enabled = true;
    }
  
    
    
    
    private void blockdata()

    {
        ddlpenerimapensiun.Enabled = false;
        txtidktppp.Enabled = false;
        txtnamapp.Enabled = false;
        txttempatlahirpp.Enabled = false;
        txtalamatpp.Enabled = false;
        txtnamaaw.Enabled = false;
        txtnamaibukandungaw.Enabled = false;
        txthubunganaw.Enabled = false;
        txttempatlahiraw.Enabled = false;
        txtalamataw.Enabled = false;
        txtnopen.Enabled = false;
        txtnip.Enabled = false;
        txtnamasesuaisk.Enabled = false;
        txtnosk.Enabled = false;
        txtnodapem.Enabled = false;
        txtgajidisplay.Enabled = false;
        txtgapok.Enabled = false;
        ddljangkawaktu.Enabled = false;
        txtplafon.Enabled = false;


        ddlinstansi.Enabled = false;
        txtnopen.Enabled = false;
        txtnamasesuaisk.Enabled = false;
        ddlstatusnikah.Enabled = false;
        ddlpensiun.Enabled = false;
        ddlkodejiwa.Enabled = false;
        txtmtpensiun.Enabled = false;
        txttgllahir.Enabled = false;
        txtgajidisplay.Enabled = false;
        txttgllahiranak1.Enabled = false;
        txttgllahiranak2.Enabled = false;
        txttglrealisasi.Enabled = false;
        ddlKantorBayar.Enabled = false;
        ddlsumberdana.Enabled = false;
        ddlprogrampinjaman.Enabled = false;
        ddlproduk.Enabled = false;
        ddlkategori.Enabled = false;
        ddljangkawaktu.Enabled = false;
        txtplafon.Enabled = false;
        ddlangsurandimukablokir.Enabled = false;
        txtpelunasan.Enabled = false;
        ddlangsurandimuka.Enabled = false;
        ddlangsurandimukaasuransi.Enabled = false;
        ddlmaterai.Enabled = false;
        ddlprovisi.Enabled = false;

        ddljeniskelaminpp.Enabled = false;
        txttgllahirpp.Enabled = false;
        ddlagamapp.Enabled = false;
        txtnohppp.Enabled = false;
        txtemailpp.Enabled = false;
        txtrtpp.Enabled = false;
        txtrwpp.Enabled = false;
        txtkabupatenpp.Enabled = false;
        txtkelurahanpp.Enabled = false;
        txtkecamatanpp.Enabled = false;
        ddlprovinsipp.Enabled = false;
        txtkodepospp.Enabled = false;
        ddljeniskelaminaw.Enabled = false;
        txttgllahiraw.Enabled = false;
        txtnohpaw.Enabled = false;
        txtemailaw.Enabled = false;
        txtrtaw.Enabled = false;
        txtrwaw.Enabled = false;
        txtkabupatenaw.Enabled = false;
        txtkelurahanaw.Enabled = false;
        txtkecamatanaw.Enabled = false;
        ddlprovinsiaw.Enabled = false;
        txtkodeposaw.Enabled = false;
        ddldatasimulasi.Enabled = false;
        txtglskpensiun.Enabled = false;
        txtnonpwp.Enabled = false;
        txttunjistri.Enabled = false;
        txttunjangananak.Enabled = false;
        txttunjanganberas.Enabled = false;
        txttunjanganlain.Enabled = false;
        txtpotonganpajak.Enabled = false;
        txtpotonganaskes.Enabled = false;
        txtpotonganlain.Enabled = false;
        txtnamaanak1.Enabled = false;
        txtnamaanak2.Enabled = false;
        txtnamaanak3.Enabled = false;
        txttgllahiranak3.Enabled = false;
        ddljeniskelaminanak1.Enabled = false;
        ddljeniskelaminanak2.Enabled = false;
        ddljeniskelaminanak3.Enabled = false;
        ddlstatusnikahanak1.Enabled = false;
        ddlstatusnikahanak2.Enabled = false;
        ddlstatusnikahanak3.Enabled = false;
        ddlmutasikantorbayar.Enabled = false;
        ddlAO.Enabled = false;
        ddlnonao.Enabled = false;
        fileupload1.Enabled = false;
        btnfoto1.Enabled = false;
        fileupload2.Enabled = false;
        btnfoto2.Enabled = false;
        fileupload3.Enabled = false;
        btnfoto3.Enabled = false;
        fileupload4.Enabled = false;
        btnfoto4.Enabled = false;
        ddljeniskelaminpensiun.Enabled = false;
        fileupload5.Enabled = false;
        btnfoto5.Enabled = false;
    }


   
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {



            case "SET":

                try

                {
                    if (ddlstatus.SelectedValue != "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Edit Data Permohonan Hanya dapat dilakukan di status Permohonan Kredit');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        nav.Visible = true;
                        BackNextAnggota.Visible = true;
                        BackNextAhliWaris.Visible = true;
                        BackNextSimulasi.Visible = true;
                        btnBackSimulasi.Visible = true;
                        ShowPK2.Enabled = false;
                        ClearData();
                        Opendata();
                        ClearPanel2();
                        pnlpinjaman2.Visible = false;
                        string DATAKODEAOADA = "";
                        string kodematerai = "";
                        string AngsuranDimukaAsuransi = "";
                        //string register
                        HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;
                      
                        HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;


                        txtidpermohonan.Text = HDRegister.Value;

                        pnladd.Visible = true;
                        pnlIdentitas.Visible = true;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        //pnlJaminan.Visible = true;
                        //pnlPengajuan.Visible = true;
                        btnSimpan.Visible = true;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;

                        btnTolakAntrian.Visible = false;
                        pnlpinjaman.Visible = true;
                        Pnlpenerima.Visible = true;
                        Pnlsimulasi.Visible = true;
                        PnlSKPensiun.Visible = true;
                        pnlpinjaman.Visible = true;
                        PNLKELUARGA.Visible = true;
                        pnlUpdAsuransi.Visible = true;

                        PnlRekeningBank.Visible = true;

                        btnCancel.Visible = true;
                        //btndokumen.Visible = true;
                        Button2.Visible = true;

                        //ddldatasimulasi.SelectedValue = "B";
                        //ddllistsimulasi.SelectedValue = "-";
                      
                        //ddlmajuhari_SelectedIndexChanged(sender, e);

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var[5] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, HDRegister.Value, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);
                            txtnonasabah.Focus();
                        }
                        else
                        {
                            btnSimpan.Visible = true;
                            ddldatasimulasi.Enabled = false;
                            txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
                            txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                            txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                            txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
                            txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                            txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                            ddlpropinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
                            txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
                            txtkotaktp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                            txtkabupatenktp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
                            txttempatlahirktp.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
                            txttgllahirktp.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                            txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                            txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                            txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                            ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                            txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                            txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                            ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                            ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                            txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();

                            txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();

                            txttglterbitidentitasktp.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse((ge.ListDataset.Tables[0].Rows[0]["tanggal_terbit_identitas"].ToString())));
                            //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                            //ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                            //txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

                            ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                            txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                            txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                            //txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                            //txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

                            txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                            txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                            ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();

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
                            if (DATAKODEAOADA == "1")
                            {
                                ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                            }

                            HDKodePos.Value = txtkodeposktp.Text;
                            //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                            HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                            ddljenkelktp.SelectedValue = HDGender.Value;

                            if (ddlstatusrumah.SelectedValue == "8")
                            {




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

                                txtalamatrumahtinggal1.Enabled = false;
                                txtrttp.Enabled = false;
                                txtrwtpt.Enabled = false;
                                txtkelurahantpt.Enabled = false;
                                txtkecamatantpt.Enabled = false;
                                ddlprovinsitpt.Enabled = false;
                                txtkodepostpt.Enabled = false;
                            }


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


                            }
                            else
                            {

                                txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                                txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                                txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                                var row = ge.ListDataset.Tables[0].Rows[0];
                                string tgllahirpasangan = null;

                                if (row["tanggal_lahir_pasangan"] != DBNull.Value)
                                {
                                    DateTime tgl;
                                    if (DateTime.TryParse(row["tanggal_lahir_pasangan"].ToString(), out tgl))
                                    {
                                        tgllahirpasangan = tgl.ToString("dd/MM/yyyy");
                                    }
                                }

                                txttgllahirpasangan.Text = tgllahirpasangan ?? string.Empty;

                            }

                            imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            ddldatasimulasi.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["simulasi"].ToString();
                            ddllistsimulasi.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString(), ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString()));
                            ddllistsimulasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString();



                            txtnamaaw.Text = ge.ListDataset.Tables[0].Rows[0]["namaahliwaris"].ToString();
                            txtnamaibukandungaw.Text = ge.ListDataset.Tables[0].Rows[0]["namaibukandung"].ToString();
                            txthubunganaw.Text = ge.ListDataset.Tables[0].Rows[0]["hubunganahliwaris"].ToString();
                            ddljeniskelaminaw.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelaminaw"].ToString();
                            txttempatlahiraw.Text = ge.ListDataset.Tables[0].Rows[0]["tempatlahir"].ToString();
                            txttgllahiraw.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);//Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()).ToString("dd-MMMM-yyyy");
                            txtnohpaw.Text = ge.ListDataset.Tables[0].Rows[0]["nohp"].ToString();
                            txtemailaw.Text = ge.ListDataset.Tables[0].Rows[0]["emailaw"].ToString();
                            txtalamataw.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                            txtrtaw.Text = ge.ListDataset.Tables[0].Rows[0]["rtaw"].ToString();
                            txtrwaw.Text = ge.ListDataset.Tables[0].Rows[0]["rwaw"].ToString();
                            txtkabupatenaw.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenaw"].ToString();
                            txtkelurahanaw.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanaw"].ToString();
                            txtkecamatanaw.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanaw"].ToString();
                            ddlprovinsiaw.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsiaw"].ToString();
                            txtkodeposaw.Text = ge.ListDataset.Tables[0].Rows[0]["kodeposaw"].ToString();

                            ddlpenerimapensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["penerima_pensiun"].ToString();
                            txtidktppp.Text = ge.ListDataset.Tables[0].Rows[0]["id_KTP"].ToString();
                            txtnamapp.Text = ge.ListDataset.Tables[0].Rows[0]["nama"].ToString();
                            ddljeniskelaminpp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelaminpp"].ToString();
                            txttempatlahirpp.Text = ge.ListDataset.Tables[0].Rows[0]["tempatlahirpp"].ToString();
                            txttgllahirpp.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty); //Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()).ToString("dd-MMMM-yyyy");
                            ddlagamapp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agamapp"].ToString();
                            txtnohppp.Text = ge.ListDataset.Tables[0].Rows[0]["nohppp"].ToString();
                            txtemailpp.Text = ge.ListDataset.Tables[0].Rows[0]["emailpp"].ToString();
                            txtalamatpp.Text = ge.ListDataset.Tables[0].Rows[0]["alamatpp"].ToString();
                            txtrtpp.Text = ge.ListDataset.Tables[0].Rows[0]["RTpp"].ToString();
                            txtrwpp.Text = ge.ListDataset.Tables[0].Rows[0]["rwpp"].ToString();
                            txtkabupatenpp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenpp"].ToString();
                            txtkelurahanpp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanpp"].ToString();
                            txtkecamatanpp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanpp"].ToString();
                            ddlprovinsipp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsipp"].ToString();
                            txtkodepospp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepospp"].ToString();

                            ddlinstansi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();
                            txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString();

                            if(ge.ListDataset.Tables[0].Rows[0]["Instansi"].ToString() == "B")
                            {
                                divNik.Visible = true;
                            }
                            txtnikA.Text = ge.ListDataset.Tables[0].Rows[0]["nik_a"].ToString();
                            txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["nopensiun"].ToString();
                            ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_Produk"].ToString();
                            txtnamasesuaisk.Text = ge.ListDataset.Tables[0].Rows[0]["namask"].ToString();
                            txtnodapem.Text = ge.ListDataset.Tables[0].Rows[0]["nodapem"].ToString();
                            ddljeniskelaminpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminpensiun"].ToString();
                            txtnosk.Text = ge.ListDataset.Tables[0].Rows[0]["nosk"].ToString();
                            txtglskpensiun.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tglskpensiun"].ToString()).ToString("dd-MMMM-yyyy");
                            ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusNikah"].ToString();
                            ddlpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
                            ddlkodejiwa.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
                            txtmtpensiun.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tmtpensiun"].ToString()).ToString("dd-MMMM-yyyy");
                            txtnonpwp.Text = ge.ListDataset.Tables[0].Rows[0]["nonpwp"].ToString();
                            txttgllahir.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");

                            txtnamaanak1.Text = ge.ListDataset.Tables[0].Rows[0]["namaanak1"].ToString();
                            txtnamaanak2.Text = ge.ListDataset.Tables[0].Rows[0]["namaanak2"].ToString();
                            txtnamaanak3.Text = ge.ListDataset.Tables[0].Rows[0]["namaanak3"].ToString();

                            txttgllahiranak1.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txttgllahiranak2.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txttgllahiranak3.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak3"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak3"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txtgajidisplay.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajidisplay"].ToString()));
                            txtgapok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajipokok"].ToString()));
                            ddlTunjanganIstriAnak.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statustunjanganistrianak"].ToString();
                            if(ddlTunjanganIstriAnak.SelectedValue == "Tidak")
                            {
                                txttunjistri.Enabled = false;
                                txttunjangananak.Enabled = false;
                            }
                            txttunjistri.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganistri"].ToString()));
                            txttunjangananak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjangananak"].ToString()));
                            txttunjanganberas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganberas"].ToString()));
                            txttunjanganlain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganlainlain"].ToString()));
                            txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajikotor"].ToString()));
                            txtpotonganpajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganpajak"].ToString()));
                            txtpotonganaskes.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganaskes"].ToString()));
                            txtpotonganlain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganlainlain"].ToString()));
                            txttotalpotongan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpotongan"].ToString()));
                            txtgajibersih.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajibersih"].ToString()));








                            txttglrealisasi.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tanggal_pengajuan"].ToString()).ToString("dd-MMMM-yyyy");

                            txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                            ddlKantorBayar.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                            ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["SumberDana"].ToString();
                            ddlprogrampinjaman.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["ProgramPinjaman"].ToString();
                            ddlkategori.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_kategori"].ToString();
                            ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                            ddlPk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pk"].ToString();

                            txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpelunasan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["permohonan_pelunasan"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["permohonan_pelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka_tahun"].ToString();
                            //                    txttunjangananak.Text = ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
                            var rtjangkawaktu = "0";
                            rtjangkawaktu = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka_bulan"].ToString();
                                                       kodematerai = ge.ListDataset.Tables[0].Rows[0]["materai"].ToString();
                            AngsuranDimukaAsuransi = ge.ListDataset.Tables[0].Rows[0]["angsuranmukaasuransi"].ToString();
                            if (ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString() == "" || ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString() == "Tidak")
                            {
                                ddlmutasikantorbayar.SelectedValue = "0";

                            }
                            else
                            {

                                ddlmutasikantorbayar.SelectedValue = "1";
                            }
                            ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodeao"].ToString();
                            ddlnonao.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodenonao"].ToString();
                            //InsertCombo();
                            //output
                            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["namaoutput"].ToString();
                            txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["statuspinjamanoutput"].ToString();
                            txtusiapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["usiapeminjam"].ToString();
                            txtusiaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["usiaasuransi"].ToString();
                            txtusiaanak1.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak1"].ToString();
                            txtusiaanak2.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak2"].ToString();
                            txtusia3.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak3"].ToString();
                            ddljeniskelaminanak1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak1"].ToString();
                            ddljeniskelaminanak2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak2"].ToString();
                            ddljeniskelaminanak3.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak3"].ToString();
                            ddlstatusnikahanak1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak1"].ToString();
                            ddlstatusnikahanak2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak2"].ToString();
                            ddlstatusnikahanak3.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak3"].ToString();

                            txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonmax"].ToString()));// string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtjangkawaktumax.Text = ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString();
                            txtrateasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                            txtratebungapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["RateBungaPinjaman"].ToString();
                            txtratebungaefektif.Text = ge.ListDataset.Tables[0].Rows[0]["ratebungapinjamanefektif"].ToString();

                            //txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["JthTempoThn"].ToString();
                            //txtjangkawaktumax = ge.ListDataset.Tables[0].Rows[0]["JthTempoBln"].ToString();
                            txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsuranrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["asuransirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["totalrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["sisagajirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["provisirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materairp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["materairp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokirrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsuranblokirrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsuranrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsuranrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaadminrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["biayaadminrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanrp"].ToString()));// Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["pelunasanrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["terimabersihrp"].ToString()));// Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["terimabersihrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbankrp"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["adminbankrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransipersen"].ToString()));
                            txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranpersen"].ToString()));
                            txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisipersen"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txttotalpersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajipersen"].ToString())); // string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbankpersen"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenAdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajipersen"].ToString()));

                            txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokirkali"].ToString()));
                            txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsurankali"].ToString()));
                            txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaasuransikali"].ToString()));

                            txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_Jatuh_Tempo"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();    



                            txtfoto1.Text = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();
                            txtfoto2.Text = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();
                            txtfoto3.Text = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();
                            txtfoto4.Text = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();
                            txtfoto5.Text = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();
                            txtPdf6.Text = ge.ListDataset.Tables[0].Rows[0]["file_path6"].ToString();

                            txtfilename1.Text = ge.ListDataset.Tables[0].Rows[0]["file1"].ToString();
                            txtfilename2.Text = ge.ListDataset.Tables[0].Rows[0]["file2"].ToString();
                            txtfilename3.Text = ge.ListDataset.Tables[0].Rows[0]["file3"].ToString();
                            txtfilename4.Text = ge.ListDataset.Tables[0].Rows[0]["file4"].ToString();
                            txtfilename5.Text = ge.ListDataset.Tables[0].Rows[0]["file5"].ToString();
                            txtfilename6.Text = ge.ListDataset.Tables[0].Rows[0]["file6"].ToString();
                            imgfoto1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                            imgfoto2.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                            imgfoto3.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            imgfoto4.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            imgfoto5.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();


                            TxtNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningtrf"].ToString();
                            TxtNomorBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningtrf"].ToString();
                            TxtAtasNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningtrf"].ToString();
                            txtnamaBankkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningKB"].ToString();
                            txtnorekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningKB"].ToString();
                            txtatasnamarekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningKB"].ToString();

                            ddljangkawaktu.Items.Clear();
                            ddljangkawaktu.SelectedValue = null;
                            ddljangkawaktu.Text = null;


                            DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[4];
                            var1xx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJANGKA", typeof(string));
                            var1xx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var1xx[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                            var1xx[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));

                            gex.ListDataset = gbx.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var1xx, "constrmf");

                            gex.ListDataset.Tables[0].DefaultView.Sort = "jangkabulan asc";

                            ddljangkawaktu.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                            ddljangkawaktu.DataTextField = "jangkabulan";
                            ddljangkawaktu.DataValueField = "jangkabulan";
                            ddljangkawaktu.DataBind();

                            ddljangkawaktu.SelectedValue = rtjangkawaktu;


                            DdlAngsurandimuka();
                            ddlmaterai.SelectedValue = kodematerai;
                            ddlprovisi.SelectedValue = txtpersenprovisi.Text;
                            ddlangsurandimuka.SelectedValue = txtangsurandimukakali.Text;
                            ddlangsurandimukablokir.SelectedValue = txtangsuranblokirkali.Text;
                            txtmateraipcs.Text = kodematerai;
                          
                            if (AngsuranDimukaAsuransi == "0" || AngsuranDimukaAsuransi=="" || AngsuranDimukaAsuransi=="-")
                            {
                                ddlangsurandimukaasuransi.SelectedValue = "0";
                            }
                            else
                            {
                                ddlangsurandimukaasuransi.SelectedValue = AngsuranDimukaAsuransi;

                            }
                            if (ddldatasimulasi.SelectedValue == "0")
                            {

                                simulasi.Visible = false;



                            }
                            else
                            {
                                simulasi.Visible = true;
                            }




                        }
                       
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

                    }
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "Approve":

                try

                {

                    if (ddlstatus.SelectedValue != "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Approve Antrian Hanya dapat dilakukan di status Permohonan Kredit');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {


                        ClearData();
                        blockdata();


                        string DATAKODEAOADA = "";
                        string kodematerai = "";
                        string AngsuranDimukaAsuransi = "";
                        //string register
                        HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;

                        HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;


                        txtidpermohonan.Text = HDRegister.Value;

                        pnladd.Visible = true;
                        pnlIdentitas.Visible = true;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        //pnlJaminan.Visible = true;
                        //pnlPengajuan.Visible = true;
                        btnSimpan.Visible = true;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;

                        btnTolakAntrian.Visible = false;
                        pnlpinjaman.Visible = true;
                        Pnlpenerima.Visible = true;
                        Pnlsimulasi.Visible = true;
                        PnlSKPensiun.Visible = true;
                        pnlpinjaman.Visible = true;
                        PNLKELUARGA.Visible = true;
                        PnlRekeningBank.Visible = true;
                        pnlUpdAsuransi.Visible = false;
                        btnCancel.Visible = true;
                        //btndokumen.Visible = true;
                        Button2.Visible = false;

                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = true;
                        BtnBatalkanmohon.Visible = true;
                        BtnApproveAntrian.Enabled = true;
                        nav.Visible = true;
                        BackNextAnggota.Visible = true;
                        BackNextAhliWaris.Visible = true;
                        BackNextSimulasi.Visible = true;
                        btnBackSimulasi.Visible = true;

                        btnTolakAntrian.Visible = false;


                        pnlUpdAsuransi.Visible = true;

                        //ddldatasimulasi.SelectedValue = "B";
                        //ddllistsimulasi.SelectedValue = "-";

                        //ddlmajuhari_SelectedIndexChanged(sender, e);

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var[5] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, HDRegister.Value, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);
                            txtnonasabah.Focus();
                        }
                        else
                        {
                           
                            txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
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
                            txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                            txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                            txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                            ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                            txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                            txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                            ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                            ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                            txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();

                            txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();


                            //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                            //ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                            //txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

                            ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                            txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                            txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                            //txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                            //txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

                            txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                            txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                            ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();

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
                            if (DATAKODEAOADA == "1")
                            {
                                ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                            }

                            HDKodePos.Value = txtkodeposktp.Text;
                            //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                            HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                            ddljenkelktp.SelectedValue = HDGender.Value;

                            if (ddlstatusrumah.SelectedValue == "8")
                            {




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

                                txtalamatrumahtinggal1.Enabled = false;
                                txtrttp.Enabled = false;
                                txtrwtpt.Enabled = false;
                                txtkelurahantpt.Enabled = false;
                                txtkecamatantpt.Enabled = false;
                                ddlprovinsitpt.Enabled = false;
                                txtkodepostpt.Enabled = false;
                            }


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


                            }
                            else
                            {

                                txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                                txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                                txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                                var row = ge.ListDataset.Tables[0].Rows[0];
                                string tgllahirpasangan = null;

                                if (row["tanggal_lahir_pasangan"] != DBNull.Value)
                                {
                                    DateTime tgl;
                                    if (DateTime.TryParse(row["tanggal_lahir_pasangan"].ToString(), out tgl))
                                    {
                                        tgllahirpasangan = tgl.ToString("dd/MM/yyyy");
                                    }
                                }

                                txttgllahirpasangan.Text = tgllahirpasangan ?? string.Empty;
                            }

                            imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            ddldatasimulasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["simulasi"].ToString();
                            ddllistsimulasi.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString(), ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString()));
                            ddllistsimulasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString();



                            txtnamaaw.Text = ge.ListDataset.Tables[0].Rows[0]["namaahliwaris"].ToString();
                            txtnamaibukandungaw.Text = ge.ListDataset.Tables[0].Rows[0]["namaibukandung"].ToString();
                            txthubunganaw.Text = ge.ListDataset.Tables[0].Rows[0]["hubunganahliwaris"].ToString();
                            ddljeniskelaminaw.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelaminaw"].ToString();
                            txttempatlahiraw.Text = ge.ListDataset.Tables[0].Rows[0]["tempatlahir"].ToString();
                            txttgllahiraw.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);//Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()).ToString("dd-MMMM-yyyy");
                            txtnohpaw.Text = ge.ListDataset.Tables[0].Rows[0]["nohp"].ToString();
                            txtemailaw.Text = ge.ListDataset.Tables[0].Rows[0]["emailaw"].ToString();
                            txtalamataw.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                            txtrtaw.Text = ge.ListDataset.Tables[0].Rows[0]["rtaw"].ToString();
                            txtrwaw.Text = ge.ListDataset.Tables[0].Rows[0]["rwaw"].ToString();
                            txtkabupatenaw.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenaw"].ToString();
                            txtkelurahanaw.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanaw"].ToString();
                            txtkecamatanaw.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanaw"].ToString();
                            ddlprovinsiaw.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsiaw"].ToString();
                            txtkodeposaw.Text = ge.ListDataset.Tables[0].Rows[0]["kodeposaw"].ToString();

                            ddlpenerimapensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["penerima_pensiun"].ToString();
                            txtidktppp.Text = ge.ListDataset.Tables[0].Rows[0]["id_KTP"].ToString();
                            txtnamapp.Text = ge.ListDataset.Tables[0].Rows[0]["nama"].ToString();
                            ddljeniskelaminpp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelaminpp"].ToString();
                            txttempatlahirpp.Text = ge.ListDataset.Tables[0].Rows[0]["tempatlahirpp"].ToString();
                            txttgllahirpp.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty); //Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()).ToString("dd-MMMM-yyyy");
                            ddlagamapp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agamapp"].ToString();
                            txtnohppp.Text = ge.ListDataset.Tables[0].Rows[0]["nohppp"].ToString();
                            txtemailpp.Text = ge.ListDataset.Tables[0].Rows[0]["emailpp"].ToString();
                            txtalamatpp.Text = ge.ListDataset.Tables[0].Rows[0]["alamatpp"].ToString();
                            txtrtpp.Text = ge.ListDataset.Tables[0].Rows[0]["RTpp"].ToString();
                            txtrwpp.Text = ge.ListDataset.Tables[0].Rows[0]["rwpp"].ToString();
                            txtkabupatenpp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenpp"].ToString();
                            txtkelurahanpp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanpp"].ToString();
                            txtkecamatanpp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanpp"].ToString();
                            ddlprovinsipp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsipp"].ToString();
                            txtkodepospp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepospp"].ToString();

                            ddlinstansi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();
                            txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString();
                            txtnikA.Text = ge.ListDataset.Tables[0].Rows[0]["nik_a"].ToString();
                            txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["nopensiun"].ToString();
                            ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_Produk"].ToString();
                            txtnamasesuaisk.Text = ge.ListDataset.Tables[0].Rows[0]["namask"].ToString();
                            txtnodapem.Text = ge.ListDataset.Tables[0].Rows[0]["nodapem"].ToString();
                            ddljeniskelaminpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminpensiun"].ToString();
                            txtnosk.Text = ge.ListDataset.Tables[0].Rows[0]["nosk"].ToString();
                            txtglskpensiun.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tglskpensiun"].ToString()).ToString("dd-MMMM-yyyy");
                            ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusNikah"].ToString();
                            ddlpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
                            ddlkodejiwa.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
                            txtmtpensiun.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tmtpensiun"].ToString()).ToString("dd-MMMM-yyyy");
                            txtnonpwp.Text = ge.ListDataset.Tables[0].Rows[0]["nonpwp"].ToString();
                            txttgllahir.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");
                            txttgllahiranak1.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txttgllahiranak2.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txttgllahiranak3.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak3"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak3"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txtgajidisplay.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajidisplay"].ToString()));
                            txtgapok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajipokok"].ToString()));
                            ddlTunjanganIstriAnak.Text = ge.ListDataset.Tables[0].Rows[0]["statustunjanganistrianak"].ToString();
                            txttunjistri.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganistri"].ToString()));
                            txttunjangananak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjangananak"].ToString()));
                            txttunjanganberas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganberas"].ToString()));
                            txttunjanganlain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganlainlain"].ToString()));
                            txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajikotor"].ToString()));
                            txtpotonganpajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganpajak"].ToString()));
                            txtpotonganaskes.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganaskes"].ToString()));
                            txtpotonganlain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganlainlain"].ToString()));
                            txttotalpotongan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpotongan"].ToString()));
                            txtgajibersih.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajibersih"].ToString()));








                            txttglrealisasi.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tanggal_pengajuan"].ToString()).ToString("dd-MMMM-yyyy");

                            txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                            ddlKantorBayar.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                            ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["SumberDana"].ToString();
                            ddlprogrampinjaman.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["ProgramPinjaman"].ToString();
                            ddlkategori.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_kategori"].ToString();
                            ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();

                            txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpelunasan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["permohonan_pelunasan"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["permohonan_pelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka_tahun"].ToString();
                            //                    txttunjangananak.Text = ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
                            var rtjangkawaktu = "0";
                            rtjangkawaktu = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka_bulan"].ToString();
                            kodematerai = ge.ListDataset.Tables[0].Rows[0]["materai"].ToString();
                            AngsuranDimukaAsuransi = ge.ListDataset.Tables[0].Rows[0]["angsuranmukaasuransi"].ToString();
                            if (ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString() == "" || ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString() == "Tidak")
                            {
                                ddlmutasikantorbayar.SelectedValue = "0";

                            }
                            else
                            {

                                ddlmutasikantorbayar.SelectedValue = "1";
                            }
                            ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodeao"].ToString();
                            ddlnonao.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodenonao"].ToString();
                            //InsertCombo();
                            //output
                            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["namaoutput"].ToString();
                            txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["statuspinjamanoutput"].ToString();
                            txtusiapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["usiapeminjam"].ToString();
                            txtusiaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["usiaasuransi"].ToString();
                            txtusiaanak1.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak1"].ToString();
                            txtusiaanak2.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak2"].ToString();
                            txtusia3.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak3"].ToString();
                            ddljeniskelaminanak1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak1"].ToString();
                            ddljeniskelaminanak2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak2"].ToString();
                            ddljeniskelaminanak3.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak3"].ToString();
                            ddlstatusnikahanak1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak1"].ToString();
                            ddlstatusnikahanak2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak2"].ToString();
                            ddlstatusnikahanak3.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak3"].ToString();

                            TxtNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningtrf"].ToString();
                            TxtNomorBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningtrf"].ToString();
                            TxtAtasNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningtrf"].ToString();
                            txtnamaBankkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningKB"].ToString();
                            txtnorekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningKB"].ToString();
                            txtatasnamarekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningKB"].ToString();


                            txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonmax"].ToString()));// string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtjangkawaktumax.Text = ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString();
                            txtrateasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                            txtratebungapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["RateBungaPinjaman"].ToString();
                            txtratebungaefektif.Text = ge.ListDataset.Tables[0].Rows[0]["ratebungapinjamanefektif"].ToString();


                            //txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["JthTempoThn"].ToString();
                            //txtjangkawaktumax = ge.ListDataset.Tables[0].Rows[0]["JthTempoBln"].ToString();
                            txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsuranrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["asuransirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["totalrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["sisagajirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["provisirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materairp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["materairp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokirrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsuranblokirrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsuranrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsuranrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaadminrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["biayaadminrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanrp"].ToString()));// Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["pelunasanrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["terimabersihrp"].ToString()));// Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["terimabersihrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbankrp"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["adminbankrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransipersen"].ToString()));
                            txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranpersen"].ToString()));
                            txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisipersen"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txttotalpersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajipersen"].ToString())); // string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbankpersen"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenAdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajipersen"].ToString()));

                            txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokirkali"].ToString()));
                            txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsurankali"].ToString()));
                            txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaasuransikali"].ToString()));

                            txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_Jatuh_Tempo"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();    

                            txtfoto1.Text = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();
                            txtfoto2.Text = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();
                            txtfoto3.Text = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();
                            txtfoto4.Text = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();
                            txtfoto5.Text = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();
                            txtPdf6.Text = ge.ListDataset.Tables[0].Rows[0]["file_path6"].ToString();

                            txtfilename1.Text = ge.ListDataset.Tables[0].Rows[0]["file1"].ToString();
                            txtfilename2.Text = ge.ListDataset.Tables[0].Rows[0]["file2"].ToString();
                            txtfilename3.Text = ge.ListDataset.Tables[0].Rows[0]["file3"].ToString();
                            txtfilename4.Text = ge.ListDataset.Tables[0].Rows[0]["file4"].ToString();
                            txtfilename5.Text = ge.ListDataset.Tables[0].Rows[0]["file5"].ToString();
                            txtfilename6.Text = ge.ListDataset.Tables[0].Rows[0]["file6"].ToString();

                            imgfoto1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                            imgfoto2.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                            imgfoto3.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            imgfoto4.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            imgfoto5.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();





                            DdlAngsurandimuka();
                            ddlmaterai.SelectedValue = kodematerai;
                            ddlprovisi.SelectedValue = txtpersenprovisi.Text;
                            ddlangsurandimuka.SelectedValue = txtangsurandimukakali.Text;
                            ddlangsurandimukablokir.SelectedValue = txtangsuranblokirkali.Text;
                            txtmateraipcs.Text = kodematerai;

                            if (AngsuranDimukaAsuransi == "0" || AngsuranDimukaAsuransi == "" || AngsuranDimukaAsuransi == "-")
                            {
                                ddlangsurandimukaasuransi.SelectedValue = "0";
                            }
                            else
                            {
                                ddlangsurandimukaasuransi.SelectedValue = AngsuranDimukaAsuransi;

                            }
                            if (ddldatasimulasi.SelectedValue == "0")
                            {

                                simulasi.Visible = false;



                            }
                            else
                            {
                                simulasi.Visible = true;
                            }

                            ddljangkawaktu.Items.Clear();
                            ddljangkawaktu.SelectedValue = null;
                            ddljangkawaktu.Text = null;


                            DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[4];
                            var1xx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJANGKA", typeof(string));
                            var1xx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var1xx[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                            var1xx[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));

                            gex.ListDataset = gbx.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var1xx, "constrmf");

                            gex.ListDataset.Tables[0].DefaultView.Sort = "jangkabulan asc";

                            ddljangkawaktu.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                            ddljangkawaktu.DataTextField = "jangkabulan";
                            ddljangkawaktu.DataValueField = "jangkabulan";
                            ddljangkawaktu.DataBind();

                            ddljangkawaktu.SelectedValue = rtjangkawaktu;


                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
           
            case "Batalkan":

                try

                {
               
                    if (ddlstatus.SelectedValue != "11")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Batalkan Antrian Hanya dapat dilakukan di status Tolakan Pusat');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        ClearData();
                        blockdata();
                        string DATAKODEAOADA = "";
                        string kodematerai = "";
                        string AngsuranDimukaAsuransi = "";
                        //string register
                        HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;

                        HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;


                        txtidpermohonan.Text = HDRegister.Value;

                        pnladd.Visible = true;
                        pnlIdentitas.Visible = true;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        //pnlJaminan.Visible = true;
                        //pnlPengajuan.Visible = true;
                        nav.Visible = true;
                        BackNextAnggota.Visible = true;
                        BackNextAhliWaris.Visible = true;
                        BackNextSimulasi.Visible = true;
                        btnBackSimulasi.Visible = true;

                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;

                        btnTolakAntrian.Visible = true;
                        pnlpinjaman.Visible = true;
                        Pnlpenerima.Visible = true;
                        Pnlsimulasi.Visible = true;
                        PnlSKPensiun.Visible = true;
                        pnlpinjaman.Visible = true;
                        PNLKELUARGA.Visible = true;
                        PnlRekeningBank.Visible = true;

                        pnlUpdAsuransi.Visible = false;
                        btnCancel.Visible = true;
                        //btndokumen.Visible = true;
                        Button2.Visible = false;

                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
     
                        btnTolakAntrian.Visible = true;
                        pnlUpdAsuransi.Visible = true;
                        //ddldatasimulasi.SelectedValue = "B";
                        ////ddllistsimulasi.SelectedValue = "-";
                        //ddldatasimulasi_SelectedIndexChanged(sender, e);
                        ////ddlmajuhari_SelectedIndexChanged(sender, e);
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var[5] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, HDRegister.Value, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);
                            txtnonasabah.Focus();
                        }
                        else
                        {
                            btnSimpan.Visible = true;
                            txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
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
                            txttgllahir.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
                            txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                            txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                            ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                            txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                            txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                            ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                            ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                            txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();

                            txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();


                            //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                            //ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                            //txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

                            ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                            txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                            txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                            //txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                            //txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

                            txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                            txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                            ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();

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
                            if (DATAKODEAOADA == "1")
                            {
                                ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
                            }

                            HDKodePos.Value = txtkodeposktp.Text;
                            //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                            HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                            ddljenkelktp.SelectedValue = HDGender.Value;

                            if (ddlstatusrumah.SelectedValue == "8")
                            {




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

                                txtalamatrumahtinggal1.Enabled = false;
                                txtrttp.Enabled = false;
                                txtrwtpt.Enabled = false;
                                txtkelurahantpt.Enabled = false;
                                txtkecamatantpt.Enabled = false;
                                ddlprovinsitpt.Enabled = false;
                                txtkodepostpt.Enabled = false;
                            }


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


                            }
                            else
                            {

                                txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
                                txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
                                txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
                                var row = ge.ListDataset.Tables[0].Rows[0];
                                string tgllahirpasangan = null;

                                if (row["tanggal_lahir_pasangan"] != DBNull.Value)
                                {
                                    DateTime tgl;
                                    if (DateTime.TryParse(row["tanggal_lahir_pasangan"].ToString(), out tgl))
                                    {
                                        tgllahirpasangan = tgl.ToString("dd/MM/yyyy");
                                    }
                                }

                                txttgllahirpasangan.Text = tgllahirpasangan ?? string.Empty;
                            }

                            imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            ddldatasimulasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["simulasi"].ToString();
                            ddllistsimulasi.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString(), ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString()));
                            ddllistsimulasi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["nosimulasi"].ToString();



                            txtnamaaw.Text = ge.ListDataset.Tables[0].Rows[0]["namaahliwaris"].ToString();
                            txtnamaibukandungaw.Text = ge.ListDataset.Tables[0].Rows[0]["namaibukandung"].ToString();
                            txthubunganaw.Text = ge.ListDataset.Tables[0].Rows[0]["hubunganahliwaris"].ToString();
                            ddljeniskelaminaw.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelaminaw"].ToString();
                            txttempatlahiraw.Text = ge.ListDataset.Tables[0].Rows[0]["tempatlahir"].ToString();
                            txttgllahiraw.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);//Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahiraw"].ToString()).ToString("dd-MMMM-yyyy");
                            txtnohpaw.Text = ge.ListDataset.Tables[0].Rows[0]["nohp"].ToString();
                            txtemailaw.Text = ge.ListDataset.Tables[0].Rows[0]["emailaw"].ToString();
                            txtalamataw.Text = ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString();
                            txtrtaw.Text = ge.ListDataset.Tables[0].Rows[0]["rtaw"].ToString();
                            txtrwaw.Text = ge.ListDataset.Tables[0].Rows[0]["rwaw"].ToString();
                            txtkabupatenaw.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenaw"].ToString();
                            txtkelurahanaw.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanaw"].ToString();
                            txtkecamatanaw.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanaw"].ToString();
                            ddlprovinsiaw.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsiaw"].ToString();
                            txtkodeposaw.Text = ge.ListDataset.Tables[0].Rows[0]["kodeposaw"].ToString();

                            ddlpenerimapensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["penerima_pensiun"].ToString();
                            txtidktppp.Text = ge.ListDataset.Tables[0].Rows[0]["id_KTP"].ToString();
                            txtnamapp.Text = ge.ListDataset.Tables[0].Rows[0]["nama"].ToString();
                            ddljeniskelaminpp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelaminpp"].ToString();
                            txttempatlahirpp.Text = ge.ListDataset.Tables[0].Rows[0]["tempatlahirpp"].ToString();
                            txttgllahirpp.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty); //Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tgllahirpp"].ToString()).ToString("dd-MMMM-yyyy");
                            ddlagamapp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agamapp"].ToString();
                            txtnohppp.Text = ge.ListDataset.Tables[0].Rows[0]["nohppp"].ToString();
                            txtemailpp.Text = ge.ListDataset.Tables[0].Rows[0]["emailpp"].ToString();
                            txtalamatpp.Text = ge.ListDataset.Tables[0].Rows[0]["alamatpp"].ToString();
                            txtrtpp.Text = ge.ListDataset.Tables[0].Rows[0]["RTpp"].ToString();
                            txtrwpp.Text = ge.ListDataset.Tables[0].Rows[0]["rwpp"].ToString();
                            txtkabupatenpp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupatenpp"].ToString();
                            txtkelurahanpp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahanpp"].ToString();
                            txtkecamatanpp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatanpp"].ToString();
                            ddlprovinsipp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsipp"].ToString();
                            txtkodepospp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepospp"].ToString();

                            ddlinstansi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();
                            txtnip.Text = ge.ListDataset.Tables[0].Rows[0]["nip"].ToString();
                            txtnikA.Text = ge.ListDataset.Tables[0].Rows[0]["nik_a"].ToString();
                            txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["nopensiun"].ToString();
                            ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_Produk"].ToString();
                            txtnamasesuaisk.Text = ge.ListDataset.Tables[0].Rows[0]["namask"].ToString();
                            txtnodapem.Text = ge.ListDataset.Tables[0].Rows[0]["nodapem"].ToString();
                            ddljeniskelaminpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminpensiun"].ToString();
                            txtnosk.Text = ge.ListDataset.Tables[0].Rows[0]["nosk"].ToString();
                            txtglskpensiun.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tglskpensiun"].ToString()).ToString("dd-MMMM-yyyy");
                            ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusNikah"].ToString();
                            ddlpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenispensiun"].ToString();
                            ddlkodejiwa.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
                            txtmtpensiun.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tmtpensiun"].ToString()).ToString("dd-MMMM-yyyy");
                            txtnonpwp.Text = ge.ListDataset.Tables[0].Rows[0]["nonpwp"].ToString();
                            txttgllahir.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");
                            txttgllahiranak1.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txttgllahiranak2.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txttgllahiranak3.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak3"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak3"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                            txtgajidisplay.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajidisplay"].ToString()));
                            txtgapok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajipokok"].ToString()));
                            ddlTunjanganIstriAnak.Text = ge.ListDataset.Tables[0].Rows[0]["statustunjanganistrianak"].ToString();
                            txttunjistri.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganistri"].ToString()));
                            txttunjangananak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjangananak"].ToString()));
                            txttunjanganberas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganberas"].ToString()));
                            txttunjanganlain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tunjanganlainlain"].ToString()));
                            txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajikotor"].ToString()));
                            txtpotonganpajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganpajak"].ToString()));
                            txtpotonganaskes.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganaskes"].ToString()));
                            txtpotonganlain.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["potonganlainlain"].ToString()));
                            txttotalpotongan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpotongan"].ToString()));
                            txtgajibersih.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gajibersih"].ToString()));








                            txttglrealisasi.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tanggal_pengajuan"].ToString()).ToString("dd-MMMM-yyyy");

                            txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                            ddlKantorBayar.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                            ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["SumberDana"].ToString();
                            ddlprogrampinjaman.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["ProgramPinjaman"].ToString();
                            ddlkategori.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_kategori"].ToString();
                            ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();

                            txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["permohonan_plafon"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpelunasan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["permohonan_pelunasan"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["permohonan_pelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka_tahun"].ToString();
                            //                    txttunjangananak.Text = ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
                            var rtjangkawaktu = "0";
                            rtjangkawaktu = ge.ListDataset.Tables[0].Rows[0]["permohonan_jangka_bulan"].ToString();
                            kodematerai = ge.ListDataset.Tables[0].Rows[0]["materai"].ToString();
                            AngsuranDimukaAsuransi = ge.ListDataset.Tables[0].Rows[0]["angsuranmukaasuransi"].ToString();
                            if (ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString() == "" || ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString() == "Tidak")
                            {
                                ddlmutasikantorbayar.SelectedValue = "0";

                            }
                            else
                            {

                                ddlmutasikantorbayar.SelectedValue = "1";
                            }
                            ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodeao"].ToString();
                            ddlnonao.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodenonao"].ToString();
                            //InsertCombo();
                            //output
                            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["namaoutput"].ToString();
                            txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["statuspinjamanoutput"].ToString();
                            txtusiapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["usiapeminjam"].ToString();
                            txtusiaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["usiaasuransi"].ToString();
                            txtusiaanak1.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak1"].ToString();
                            txtusiaanak2.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak2"].ToString();
                            txtusia3.Text = ge.ListDataset.Tables[0].Rows[0]["usiaanak3"].ToString();
                            ddljeniskelaminanak1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak1"].ToString();
                            ddljeniskelaminanak2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak2"].ToString();
                            ddljeniskelaminanak3.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jeniskelaminanak3"].ToString();
                            ddlstatusnikahanak1.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak1"].ToString();
                            ddlstatusnikahanak2.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak2"].ToString();
                            ddlstatusnikahanak3.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statusnikahanak3"].ToString();

                            txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonmax"].ToString()));// string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtjangkawaktumax.Text = ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString();
                            txtrateasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                            txtratebungapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["RateBungaPinjaman"].ToString();
                            txtratebungaefektif.Text = ge.ListDataset.Tables[0].Rows[0]["ratebungapinjamanefektif"].ToString();

                            //txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["JthTempoThn"].ToString();
                            //txtjangkawaktumax = ge.ListDataset.Tables[0].Rows[0]["JthTempoBln"].ToString();
                            txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsuranrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["asuransirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["totalrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["sisagajirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisirp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["provisirp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materairp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["materairp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokirrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsuranblokirrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsuranrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsuranrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaadminrp"].ToString()));//Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["biayaadminrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanrp"].ToString()));// Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["pelunasanrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["terimabersihrp"].ToString()));// Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["terimabersihrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbankrp"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["adminbankrp"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransipersen"].ToString()));
                            txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranpersen"].ToString()));
                            txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisipersen"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txttotalpersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajipersen"].ToString())); // string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtpersenadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbankpersen"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenAdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                            txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagajipersen"].ToString()));

                            txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokirkali"].ToString()));
                            txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsurankali"].ToString()));
                            txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaasuransikali"].ToString()));

                            txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_Jatuh_Tempo"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();    

                            txtfoto1.Text = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();
                            txtfoto2.Text = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();
                            txtfoto3.Text = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();
                            txtfoto4.Text = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();
                            txtfoto5.Text = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();
                            txtPdf6.Text = ge.ListDataset.Tables[0].Rows[0]["file_path6"].ToString();

                            txtfilename1.Text = ge.ListDataset.Tables[0].Rows[0]["file1"].ToString();
                            txtfilename2.Text = ge.ListDataset.Tables[0].Rows[0]["file2"].ToString();
                            txtfilename3.Text = ge.ListDataset.Tables[0].Rows[0]["file3"].ToString();
                            txtfilename4.Text = ge.ListDataset.Tables[0].Rows[0]["file4"].ToString();
                            txtfilename5.Text = ge.ListDataset.Tables[0].Rows[0]["file5"].ToString();
                            txtfilename6.Text = ge.ListDataset.Tables[0].Rows[0]["file6"].ToString();

                            imgfoto1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                            imgfoto2.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                            imgfoto3.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            imgfoto4.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                          //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                            imgfoto5.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();


                            TxtNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningtrf"].ToString();
                            TxtNomorBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningtrf"].ToString();
                            TxtAtasNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningtrf"].ToString();
                            txtnamaBankkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningKB"].ToString();
                            txtnorekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningKB"].ToString();
                            txtatasnamarekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningKB"].ToString();



                            DdlAngsurandimuka();
                            ddlmaterai.SelectedValue = kodematerai;
                            ddlprovisi.SelectedValue = txtpersenprovisi.Text;
                            ddlangsurandimuka.SelectedValue = txtangsurandimukakali.Text;
                            ddlangsurandimukablokir.SelectedValue = txtangsuranblokirkali.Text;
                            txtmateraipcs.Text = kodematerai;

                            if (AngsuranDimukaAsuransi == "0" || AngsuranDimukaAsuransi == "" || AngsuranDimukaAsuransi == "-")
                            {
                                ddlangsurandimukaasuransi.SelectedValue = "0";
                            }
                            else
                            {
                                ddlangsurandimukaasuransi.SelectedValue = AngsuranDimukaAsuransi;

                            }
                            if (ddldatasimulasi.SelectedValue == "0")
                            {

                                simulasi.Visible = false;



                            }
                            else
                            {
                                simulasi.Visible = true;
                            }

                            ddljangkawaktu.Items.Clear();
                            ddljangkawaktu.SelectedValue = null;
                            ddljangkawaktu.Text = null;


                            DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[4];
                            var1xx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJANGKA", typeof(string));
                            var1xx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var1xx[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                            var1xx[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));

                            gex.ListDataset = gbx.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var1xx, "constrmf");

                            gex.ListDataset.Tables[0].DefaultView.Sort = "jangkabulan asc";

                            ddljangkawaktu.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                            ddljangkawaktu.DataTextField = "jangkabulan";
                            ddljangkawaktu.DataValueField = "jangkabulan";
                            ddljangkawaktu.DataBind();

                            ddljangkawaktu.SelectedValue = rtjangkawaktu;


                        }




                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


                    }
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
                    if (ddlstatus.SelectedValue !=  "9" && ddlstatus.SelectedValue != "6")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Close Permohonan');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRekening")).Text;

                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                        Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        if (ddlstatus.SelectedValue=="6")
                        {
                            Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKBUKTIDRAFT", typeof(string));
                        }
                        else
                        {
                            Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKBUKTI", typeof(string));
                        }
                        Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                   
                        //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf");
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_KP_CETAK_KRESUN_PENCAIRAN", Paramsc, "constrmf").Tables[0];

                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            rprt.Load(Server.MapPath("~/CL/Transaksi/Report/CLBuktiAntrianKreseun.rpt"));
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
                        GC.Collect();


                    }
                }
                break;
            case "Kwitansipinjaman":
                try
                {
                    if (ddlstatus.SelectedValue != "9" && ddlstatus.SelectedValue != "6")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Close Permohonan');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRekening")).Text;

                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                        Paramsc[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        //Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIPINJAMAN", typeof(string));
                        if (ddlstatus.SelectedValue == "6")
                        {
                            Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIPINJAMANDRAFT", typeof(string));
                        }
                        else
                        {
                            Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIPINJAMAN", typeof(string));
                        }


                        Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramsc[4] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_KP_CETAK_KRESUN_PENCAIRAN", Paramsc, "constrmf").Tables[0];

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
                        GC.Collect();

                    }
                }
                break;
            case "Angsuranpinjaman":
                try
                {
                    if (ddlstatus.SelectedValue != "9" && ddlstatus.SelectedValue != "6")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Close Permohonan');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRekening")).Text;
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

                        var[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, norek, typeof(string));
                        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        if (ddlstatus.SelectedValue == "6")
                        {
                            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUNEWDRAFT", typeof(string));
                        }
                        else
                        {
                            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUNEW", typeof(string));
                        }
                        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataTable = gb.GetDataJustOutPut("AR_Trn_KP_CETAK_KRESUN_PENCAIRAN", var, "constrmf").Tables[0];
                        //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", var, "constrmf");

                        //rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));

                        if (ge.ListDataTable.Rows.Count > 0)
                        {

                           
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));
                          

                         
                            string notransferhp = ge.ListDataTable.Rows[0]["datatransfer"].ToString() == "" ? "" : "Bukti Transfer di kirim ke No : " + ge.ListDataTable.Rows[0]["datatransfer"].ToString() + ".";
                            rprt.SetDataSource(ge.ListDataTable);

                            rprt.SetParameterValue("Notes", ge.ListDataTable.Rows[0]["notes"].ToString());
                            rprt.SetParameterValue("bagian", ge.ListDataTable.Rows[0]["bagian"].ToString());
                            //rprt.SetParameterValue("Notes1", notransferhp);

                            Response.ClearContent();
                            Response.ClearHeaders();

                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                            Response.End();
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
                        GC.Collect();
                    }
                }
                break;
            case "PerjanjianKredit":
                try
                {
                    if (ddlstatus.SelectedValue != "9" && ddlstatus.SelectedValue != "6")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Close Permohonan');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        string plafon = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat1")).Text.Replace(".", "");
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekening")).Text;

                        ge.ListDataset = gb.GetDataFunction("dbo.GetDayName()", "constrmf");

                        var namahari = ge.ListDataset.Tables[0].Rows[0]["hari"].ToString();
                        var today = ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString();

                        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
                        var1[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                        //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPKKRESUN", typeof(string));
                        if (ddlstatus.SelectedValue == "6")
                        {
                            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPKKRESUNDRAFT", typeof(string));
                        }
                        else
                        {
                            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPKKRESUN", typeof(string));
                        }


                        if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                        {


                            ge.ListDataTable = gb.GetDataJustOutPut("AR_Trn_KP_CETAK_KRESUN_PENCAIRAN", var1, "constrmf").Tables[0];


                            rprt.Load(Server.MapPath("~/Kresun/Report/CetakPKPembiayaanSendiri.rpt"));

                            rprt.SetDataSource(ge.ListDataTable);

    
                            rprt.SetParameterValue("hari", namahari);
                            rprt.SetParameterValue("terbilangplafon", gb.Terbilang(decimal.Parse(plafon)));
                            rprt.SetParameterValue("terbilangbunga", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][3].ToString())));
                            rprt.SetParameterValue("terbilangprovisi", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][18].ToString())));
                            rprt.SetParameterValue("terbilangasuransi", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][19].ToString())));
                            rprt.SetParameterValue("terbilangjangka", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][4].ToString())));
                            rprt.SetParameterValue("terbilangangsuran", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][7].ToString())));
                            rprt.SetParameterValue("terbilangdenda", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][9].ToString())));

                       

                      

                                Response.ClearContent();
                                Response.ClearHeaders();

                                Response.ContentType = "application/pdf";
                                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");





                            Response.End();
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
                        GC.Collect();
                    }
                }
                break;
            case "TandaTerimaAnggota":
                try
                {
                    if (ddlstatus.SelectedValue != "9" && ddlstatus.SelectedValue != "6")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Permohonan Close');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;
                     
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                        var[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCETAKKEANGGOTAAN", typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_KP_CETAK_KRESUN_PENCAIRAN", var, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
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
                                rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                                rprt.SetParameterValue("statuskop", "1");
                                rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                                rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                                rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                                rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                                rprt.SetParameterValue("namacabang", ge.ListDataset.Tables[0].Rows[0]["namacabang"].ToString());
                                rprt.SetParameterValue("Bagian", ge.ListDataset.Tables[0].Rows[0]["ttknamabgn"].ToString());
                                Response.ClearContent();
                                Response.ClearHeaders();

                                Response.ContentType = "application/pdf";
                                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                                Response.End();

                         
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
                        GC.Collect();
                    }
                }
                break;
             
        }


    }




    

    protected void ddldatasimulasipilih_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddllistsimulasi.SelectedValue == "-")
            {

                simulasi.Visible = false;

            }
            else
            {

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                var[0] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddllistsimulasi.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVIEWSIMULASI", typeof(string));
                var[2] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[4] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, ddldatasimulasi.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");
                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);

                }
                else
                { 
                    txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["KodeSimulasi"].ToString();
                    ddlKantorBayar.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KantorBayar"].ToString();
                    ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["SumberDana"].ToString();
                    ddlprogrampinjaman.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["ProgramPinjaman"].ToString();
                    ddlkategori.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JenisKategori"].ToString();
                    ddlinstansi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Instansi"].ToString();
                    txtnopen.Text = ge.ListDataset.Tables[0].Rows[0]["NomorPensiun"].ToString();
                    ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JenisProduk"].ToString();
                    txtnamasesuaisk.Text = ge.ListDataset.Tables[0].Rows[0]["NamaPensiun"].ToString();
                    ddlkodejiwa.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeJiwa"].ToString();
                    ddlstatusnikah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusNikah"].ToString();
                    ddlpensiun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Pensiunan"].ToString();
                    txttgllahir.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahir"].ToString()).ToString("dd-MMMM-yyyy");
                    txttglrealisasi.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["tglsimulasi"].ToString()).ToString("dd-MMMM-yyyy");
                    txttgllahiranak1.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak1"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                    txttgllahiranak2.Text = (!string.IsNullOrEmpty(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()) ? Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglLahirAnak2"].ToString()).ToString("dd-MMMM-yyyy") : string.Empty);
                    txtmtpensiun.Text = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TmtPensiun"].ToString()).ToString("dd-MMMM-yyyy");
                    txtgajidisplay.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Gajidisplay"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Gaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Pelunasan"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Pelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktuTahun"].ToString();                    
                    ddljangkawaktu.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString(), ge.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()));
                    //ddlangsurandimuka.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["AngsDimukaKali"].ToString(), ge.ListDataset.Tables[0].Rows[0]["AngsDimukaKali"].ToString()));
                    ddljangkawaktu.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString();


                    //InsertCombo();
                    //output
                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["StatusAnggota"].ToString();
                    txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["StatusPinjaman"].ToString();
                    txtusiapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaPinjaman"].ToString();
                    txtusiaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAsuransi"].ToString();
                    txtusiaanak1.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAnak1"].ToString();
                    txtusiaanak2.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAnak2"].ToString();
                    txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["JthTempoThn"].ToString();
                    
                    txtusia1.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAnak1"].ToString();
                    txtusia2.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAnak1"].ToString();
                    txtplafonmax.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkawaktumax.Text = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktuMax"].ToString();
                    txtrateasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                    txtratebungapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["RateBungaPinjaman"].ToString();
                    txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktuTahun"].ToString();
                    //txtjangkawaktumax = ge.ListDataset.Tables[0].Rows[0]["JthTempoBln"].ToString();
                    txtangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["CicilanAsuransi"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["CicilanAsuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txttotal.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Total"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Total"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtsisagaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SisaGaji"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["SisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtprovisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["OutputProvisi"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtkeanggotaan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Keanggotan"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Keanggotan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtmaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["OutputMaterai"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputMaterai"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsuranblokir.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["AngsuranDiblokir"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["AngsuranDiblokir"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsurandimuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["OutputAngsuranDimuka"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputAngsuranDimuka"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtbiayaadmin.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["BiayaAdmin"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["BiayaAdmin"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasanRp.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["OutputPelunasan"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputPelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtterimabersihrp.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["TerimaBersih"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["TerimaBersih"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["AdminBank"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["AdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpersenasuransi.Text= string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenAsuransi"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenAsuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenAngsuran"].ToString()));
                    txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenProvisi"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txttotalpersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString())); // string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenAdminBank"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenAdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtsisagajipersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString()));
                    txtangsurandimukakali.Text = string.Format("{0:N0}", int.Parse(ge.ListDataset.Tables[0].Rows[0]["angsdimukabanyak"].ToString()));
                    txtangsuranblokirkali.Text = string.Format("{0:N0}", int.Parse(ge.ListDataset.Tables[0].Rows[0]["AngsBlokirKali"].ToString()));
                  
                    txtangdimukaasuransikali.Text = string.Format("{0:N0}", int.Parse(ge.ListDataset.Tables[0].Rows[0]["angsdimukaasuransi"].ToString()));

                    if (ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString()=="" || ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString()== "Tidak")
                    {
                        ddlmutasikantorbayar.SelectedValue = "0";

                    }
                    else
                    {

                        ddlmutasikantorbayar.SelectedValue = "1";
                    }
                    
                   


                    DdlAngsurandimuka();
                    ddlmaterai.SelectedValue = "2";
                    ddlangsurandimuka.SelectedValue = txtangsurandimukakali.Text;
                    ddlangsurandimukablokir.SelectedValue = txtangsuranblokirkali.Text;
                    ddlprovisi.SelectedValue = txtpersenprovisi.Text;
                    //PersenAngsuran();
                    //PersenSisaGaji();
                    //PersenProvisi();
                    //Angsuranblokirkali();
                    //Angsurandimukakali();
                    DateTime tgl_libur, tgl_realisasi;
                    tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
                    tgl_libur = tgl_realisasi.AddMonths(int.Parse(ddljangkawaktu.SelectedValue));
                    txttgljatuhtempo.Text = tgl_libur.ToString();
                    DateTime tgljt = Convert.ToDateTime(txttgljatuhtempo.Text);
                    txttgljatuhtempo.Text = tgljt.ToString("dd/MM/yyyy");

                    //TerimaBersih();


                }
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    protected void ddldatasimulasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddldatasimulasi.SelectedValue == "0")
            {

                simulasi.Visible = false;

                ddlinstansi.Enabled = true;
                txtnopen.Enabled = true;
                txtnamasesuaisk.Enabled = true;
                ddlstatusnikah.Enabled = true;
                ddlpensiun.Enabled = true;
                ddlkodejiwa.Enabled = true;
                txtmtpensiun.Enabled = true;
                txttgllahir.Enabled = true;
                txtgajidisplay.Enabled = true;
                txttgllahiranak1.Enabled = true;
                txttgllahiranak2.Enabled = true;
                txttglrealisasi.Enabled = true;
                ddlKantorBayar.Enabled = true;
                ddlsumberdana.Enabled = true;
                ddlprogrampinjaman.Enabled = true;
                ddlproduk.Enabled = true;
                ddlkategori.Enabled = true;
                ddljangkawaktu.Enabled = true;
                txtplafon.Enabled = true;
                ddlangsurandimukablokir.Enabled = true;
                txtpelunasan.Enabled = true;
                ddlangsurandimuka.Enabled = true;
                ddlangsurandimukaasuransi.Enabled = true;
                ddlmaterai.Enabled = true;
                ddlprovisi.Enabled = true;

                ddlinstansi.SelectedValue = "";
                txtnopen.Text = "";
                txtnamasesuaisk.Text = "";
                ddlstatusnikah.SelectedValue = "-";
                ddlpensiun.SelectedValue = "";
                ddlkodejiwa.SelectedValue = "";
                
                txttgllahir.Text = "";
                txtgajidisplay.Text = "0";
                txttgllahiranak1.Text = "";
                txttgllahiranak2.Text = "";
                
                txttglrealisasi.Enabled = true;
                ddlKantorBayar.SelectedValue = "";
                ddlsumberdana.SelectedValue = "";
                ddlprogrampinjaman.SelectedValue = "";
                ddlproduk.SelectedValue = "";
                ddlkategori.SelectedValue = "";
                ddljangkawaktu.SelectedValue = "0";
                txtplafon.Text = "0";
                ddlangsurandimukablokir.SelectedValue = "0";
                txtpelunasan.Text = "0";
                ddlangsurandimuka.SelectedValue = "0"; ;
                ddlangsurandimukaasuransi.SelectedValue = "0";
                ddlmaterai.SelectedValue = "0";
                ddlprovisi.SelectedValue = "0";

            }
            else
            {
                
                
                DefaultClass.Global.SqlHelper[] var22 = new DefaultClass.Global.SqlHelper[5];
                var22[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                var22[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var22[2] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, ddldatasimulasi.SelectedValue, typeof(string));
                var22[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSIMULASI", typeof(string));
                var22[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var22, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "NoSimulasi asc";

                ddllistsimulasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddllistsimulasi.DataTextField = "ketdata";
                ddllistsimulasi.DataValueField = "NoSimulasi";
                ddllistsimulasi.DataBind();
                ddllistsimulasi.Items.Insert(0, new ListItem("Pilih No Simulasi", "-"));

                simulasi.Visible = true;
                ddlinstansi.Enabled = false;
                txtnopen.Enabled = false;
                txtnamasesuaisk.Enabled = false;
                ddlstatusnikah.Enabled = false;
                ddlpensiun.Enabled = false;
                ddlkodejiwa.Enabled = false;
                txtmtpensiun.Enabled = false;
                txttgllahir.Enabled = false;
                txtgajidisplay.Enabled = false;
                txttgllahiranak1.Enabled = false;
                txttgllahiranak2.Enabled = false;
                txttglrealisasi.Enabled = false;
                ddlKantorBayar.Enabled = false;
                ddlsumberdana.Enabled = false;
                ddlprogrampinjaman.Enabled = false;
                ddlproduk.Enabled = false;
                ddlkategori.Enabled = false;
                ddljangkawaktu.Enabled = false;
                txtplafon.Enabled = false;
                ddlangsurandimukablokir.Enabled = false;
                txtpelunasan.Enabled = false;
                ddlangsurandimuka.Enabled = false;
                ddlangsurandimukaasuransi.Enabled = false;
                ddlmaterai.Enabled = false;
                ddlprovisi.Enabled = false;


                ddlinstansi.SelectedValue = "";
                txtnopen.Text="";
                txtnamasesuaisk.Text = "";
                ddlstatusnikah.SelectedValue = "-";
                ddlpensiun.SelectedValue = "";
                ddlkodejiwa.SelectedValue = "";
               
                txttgllahir.Text = "";
                txtgajidisplay.Text = "0";
                txttgllahiranak1.Text = "";
                txttgllahiranak2.Text = "";
                txttglrealisasi.Enabled = false;
                ddlKantorBayar.SelectedValue = "";
                ddlsumberdana.SelectedValue = "";
                ddlprogrampinjaman.SelectedValue = "";
                ddlproduk.SelectedValue = "";
                ddlkategori.SelectedValue = "";
                ddljangkawaktu.SelectedValue = "0";
                txtplafon.Text = "0";
                ddlangsurandimukablokir.SelectedValue = "0"; 
                txtpelunasan.Text = "0";
                ddlangsurandimuka.SelectedValue = "0"; ;
                ddlangsurandimukaasuransi.SelectedValue = "0";
                ddlmaterai.SelectedValue = "0";
                ddlprovisi.SelectedValue = "0";



            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }




    protected void ddlkantor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var22s = new DefaultClass.Global.SqlHelper[3];
            var22s[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SUMBERDANA", typeof(string));
            var22s[1] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));
            var22s[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var22s, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodesumberDana asc";

            ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlsumberdana.DataTextField = "NamaSumberDana";
            ddlsumberdana.DataValueField = "KodesumberDana";
            ddlsumberdana.DataBind();
            ddlsumberdana.Items.Insert(0, new ListItem("Pilih Sumber Dana", "-"));




            DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[4];
            var1xx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJANGKA", typeof(string));
            var1xx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1xx[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var1xx[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var1xx, "constrmf");

            /*        txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["kodesimulasi"].ToString();*/

            ge.ListDataset.Tables[0].DefaultView.Sort = "jangkabulan asc";


            ddljangkawaktu.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljangkawaktu.DataTextField = "jangkabulan";
            ddljangkawaktu.DataValueField = "jangkabulan";
            ddljangkawaktu.DataBind();
            ddljangkawaktu.Items.Insert(0, new ListItem("0", "0"));
            ddljangkawaktu.SelectedValue = "0";


            if (ddlKantorBayar.SelectedValue == "A009")
            {
                ddlsumberdana.SelectedValue = ddlKantorBayar.SelectedValue;
                ddlsumberdana.Enabled = false;
                txtkodesimulasi.Text = "B";
          
            }
            else
            {
                ddlsumberdana.SelectedValue = "-";
                ddlsumberdana.Enabled = true;
                //DdlAngsurandimuka();
                txtkodesimulasi.Text = "A";
                //txtpersenadminbank.Text = "0";



            }



            //DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
            //var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTMATERAI", typeof(string));
            //var2[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //var2[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
            //var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2, "constrmf");

            //ddlmaterai.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

            //ddlmaterai.DataTextField = "totalmaterai";
            //ddlmaterai.DataValueField = "materai";
            //ddlmaterai.DataBind();
            //ddlmaterai.Items.Insert(0, new ListItem("Pilih Materai", "0"));


            DdlAngsurandimuka();




            ddlmaterai.SelectedValue = "2";
            ddlmaterai_SelectedIndexChanged(sender, e);

            if(txtkodesimulasi.Text == "A")
            {
                ddlangsurandimukablokir.Enabled = false;
                ddlPk.Enabled = false;
                ddlkategori.SelectedValue = "A";
                ddlkategori.Enabled = false;
            }
            else
            {
                ddlangsurandimukablokir.Enabled = true;
                ddlPk.Enabled = true;
                ddlkategori.Enabled = true;


            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }



    protected void ddlmaterai_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmaterai.SelectedValue == "0")
            {
                txtmaterai.Text = "0";
                txtmateraipcs.Text = "0";
            }
            else
            {
                txtmateraipcs.Text = ddlmaterai.SelectedValue;
                string materai = (ddlmaterai.SelectedValue == "0" ? "0" : ddlmaterai.SelectedItem.Text);
                txtmaterai.Text= string.Format("{0:N0}",decimal.Parse(materai));

            }
            Data_Formula(sender, e);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void ddlangsurandimukablokir_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlangsurandimukablokir.SelectedValue == "0")
            {
                txtangsuranblokir.Text = "0";
                txtangsuranblokirkali.Text = "0";
            }
            else
            {
                txtangsuranblokirkali.Text = ddlangsurandimukablokir.SelectedValue;
                txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimukablokir.SelectedValue) * decimal.Parse(txtangsuran.Text));

            }
            Data_Formula(sender, e);


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }


    protected void ddlangsurandimuka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlangsurandimuka.SelectedValue == "0")
            {
                txtangsurandimukakali.Text = "0";
                txtangsurandimuka.Text = "0";
            }
            else
            {
                txtangsurandimukakali.Text = ddlangsurandimuka.SelectedValue;
                txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimuka.SelectedValue) * decimal.Parse(txtangsuran.Text));

            }

            Data_Formula(sender, e);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void ddlangsurandimukaasuransi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlangsurandimukaasuransi.SelectedValue == "0")
            {
                txtangdimukaasuransi.Text = "0";
                txtangdimukaasuransikali.Text = "0";
            }
            else
            {
                txtangdimukaasuransikali.Text = ddlangsurandimukaasuransi.SelectedValue;
                txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimukaasuransi.SelectedValue) * decimal.Parse(txtasuransi.Text));

            }

            Data_Formula(sender, e);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void ddlprovisi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlprovisi.SelectedValue == "-")
            {
                txtpersenprovisi.Text = "0";
                txtprovisi.Text = "0";
            }
            else
            {
                txtpersenprovisi.Text = ddlprovisi.SelectedValue;
                txtprovisi.Text = string.Format("{0:N0}", (decimal.Parse(ddlprovisi.SelectedValue)/100) * decimal.Parse(txtplafon.Text));

            }

            Data_Formula(sender, e);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ex.Message.ToString() + "','success','true');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }


    public string DdlAngsurandimuka()
    {
        try
        {

            decimal hitung = 10000;
            decimal x = 0;
            ddlmaterai.Items.Clear();
            for (int i = 1; i <= 5; i++)
            {
                x = i * hitung;
                ddlmaterai.Items.Add(new ListItem(x.ToString(), i.ToString()));

            }
            ddlmaterai.Items.Insert(0, new ListItem("Pilih Materai", "0"));

            ddlprovisi.Items.Clear();

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTPROVISI", typeof(string));
            var[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");


            ddlprovisi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlprovisi.DataTextField = "provisi";
            ddlprovisi.DataValueField = "provisi";
            ddlprovisi.DataBind();
            ddlprovisi.Items.Insert(0, new ListItem("Pilih Provisi", "0"));


            ddlangsurandimuka.Items.Clear();
            for (int i = 1; i <= 30; i++)
            {
                ddlangsurandimuka.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
            ddlangsurandimuka.Items.Insert(0, new ListItem("Pilih Angsuran Dimuka Angsuran", "0"));

            ddlangsurandimukablokir.Items.Clear();
            for (int i = 1; i <= 30; i++)
            {
                ddlangsurandimukablokir.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
            ddlangsurandimukablokir.Items.Insert(0, new ListItem("Pilih Angsuran Blokir", "0"));

            ddlangsurandimukaasuransi.Items.Clear();
            for (int i = 1; i <= 30; i++)
            {
                ddlangsurandimukaasuransi.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
            ddlangsurandimukaasuransi.Items.Insert(0, new ListItem("Pilih Angsuran Dimuka Asuransi", "0"));
            ddlangsurandimukaasuransi.Enabled = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        return ddlangsurandimuka.Text;
    }


    protected void ddlprogrampinjaman_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DdlKodeProduk();
        //ddlkantorbayar_SelectedIndexChanged(ddlKantorBayar.SelectedValue, EventArgs.Empty);
        //fungsiAllB();
        if (ddlprogrampinjaman.SelectedValue =="B")
        {
            txtpelunasan.Text = "0";
            txtpelunasan.Enabled = true;

        }
        else
        {
            txtpelunasan.Text = "0";
            txtpelunasan.Enabled = false;

        }
        Data_Formula(sender, e);

    }

    public void InsertCombo()
    {
        try
        {

       
        ge.ListDataset = gb.GetDataFunction("dbo.Getmasterkodejiwa()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_jiwa asc";

        ddlkodejiwa.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkodejiwa.DataTextField = "kdjiwa";
        ddlkodejiwa.DataValueField = "kode_jiwa";
        ddlkodejiwa.DataBind();
        ddlkodejiwa.Items.Insert(0, new ListItem("Pilih Kode Jiwa", ""));

        DefaultClass.Global.SqlHelper[] var22 = new DefaultClass.Global.SqlHelper[3];
        var22[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var22[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDNONSALES", typeof(string));
        var22[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_MO", var22, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NIK asc";

        ddlnonao.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlnonao.DataTextField = "namaAO";
        ddlnonao.DataValueField = "NIK";
        ddlnonao.DataBind();
        ddlnonao.Items.Insert(0, new ListItem("Pilih Non Salesman", "-"));

            ddljangkawaktu.Items.Insert(0, new ListItem("0", "0"));



            DefaultClass.Global.SqlHelper[] var22s = new DefaultClass.Global.SqlHelper[2];
        var22s[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSTATUS", typeof(string));
        var22s[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var22s, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "urut asc";

        ddlstatus.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatus.DataTextField = "Keterangan";
        ddlstatus.DataValueField = "id";
        ddlstatus.DataBind();
        ddlstatus.Items.Insert(0, new ListItem("Pilih Status Permohonan", "-"));
        ddlstatus.Items.Insert(9, new ListItem("Tolak Approval", "11"));

        //// --- Combo Jenis Deviasi
        //ge.ListDataset = gb.GetDataFunction("dbo.GetJenisDeviasi()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJenis asc";

        //ddljenisdeviasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddljenisdeviasi.DataTextField = "NamaJenis";
        //ddljenisdeviasi.DataValueField = "KodeJenis";
        //ddljenisdeviasi.DataBind();
        //ddljenisdeviasi.Items.Insert(0, new ListItem("Pilih Type", ""));

        // --- Combo Sumberdana
        ge.ListDataset = gb.GetDataFunction("dbo.getsumberdana()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc";

        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "NamaBank";
        ddlsumberdana.DataValueField = "KodeBank";
        ddlsumberdana.DataBind();
        ddlsumberdana.Items.Insert(0, new ListItem("Pilih Sumber Dana", ""));


        ge.ListDataset = gb.GetDataFunction("dbo.getkantorbayar()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NamaBank asc";

        ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKantorBayar.DataTextField = "NamaBank";
        ddlKantorBayar.DataValueField = "KodeBank";
        ddlKantorBayar.DataBind();
        ddlKantorBayar.Items.Insert(0, new ListItem("Pilih Kantor Bayar", ""));




        ge.ListDataset = gb.GetDataFunction("dbo.getProdukGroup()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        ddlprogrampinjaman.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprogrampinjaman.DataTextField = "namagroup";
        ddlprogrampinjaman.DataValueField = "kode";
        ddlprogrampinjaman.DataBind();
        ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Program Pinjaman", ""));


        ge.ListDataset = gb.GetDataFunction("dbo.getprogrampinjam()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlproduk.DataTextField = "keterangan";
        ddlproduk.DataValueField = "kode";
        ddlproduk.DataBind();
        ddlproduk.Items.Insert(0, new ListItem("Pilih Produk Pinjaman", ""));

        //ge.ListDataset = gb.GetDataFunction("dbo.getprogrampinjam()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        //ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlproduk.DataTextField = "keterangan";
        //ddlproduk.DataValueField = "kode";
        //ddlproduk.DataBind();
        //ddlproduk.Items.Insert(0, new ListItem("Pilih Produk Pinjaman", ""));
        ddlproduk.SelectedValue = "1";
        //ddlproduk.Enabled = false;


        ge.ListDataset = gb.GetDataFunction("dbo.getprodukkategori()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        ddlkategori.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkategori.DataTextField = "nama";
        ddlkategori.DataValueField = "kode";
        ddlkategori.DataBind();
        ddlkategori.Items.Insert(0, new ListItem("Pilih Kategori", ""));


        ge.ListDataset = gb.GetDataFunction("dbo.getprodukinstansi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        ddlinstansi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlinstansi.DataTextField = "namajenis";
        ddlinstansi.DataValueField = "kode";
        ddlinstansi.DataBind();
        ddlinstansi.Items.Insert(0, new ListItem("Pilih Instansi", ""));

        // --- Combo Status Pernikahan
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatuskawinupload.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatuskawinupload.DataTextField = "Keterangan";
        ddlstatuskawinupload.DataValueField = "id";
        ddlstatuskawinupload.DataBind();
     

        // --- Combo  Jenis Kredit
        ge.ListDataset = gb.GetDataFunction("dbo.GetProduk()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";

        //ddlprodukpengajuan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlprodukpengajuan.DataTextField = "produk";
        //ddlprodukpengajuan.DataValueField = "kode_produk";
        //ddlprodukpengajuan.DataBind();
        //ddlprodukpengajuan.SelectedValue = "3C";
        ////ddlprodukpengajuan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo  Jenis Penggunaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPenggunaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penggunaan asc";

        //ddlPenggunaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlPenggunaan.DataTextField = "penggunaan";
        //ddlPenggunaan.DataValueField = "kode_penggunaan";
        //ddlPenggunaan.DataBind();
        ////ddlPenggunaan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Jenis Pinjaman

        ////ddljenispinjaman.Items.Add("Pilih Data");
        //ddljenispinjaman.Items.Add("MIKRO");

       

        // Combo MO dan REF Non MO
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_MO", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NIK asc";

        ddlAO.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlAO.DataTextField = "namaAO";
        ddlAO.DataValueField = "NIK";
        ddlAO.DataBind();
            ddlAO.Items.Insert(0, new ListItem("Pilih Salesman", ""));
            //ddlAO.Items.Insert(0, new ListItem("Pilih Data", "-"));




            //DefaultClass.Global.SqlHelper[] var1x = new DefaultClass.Global.SqlHelper[3];
            //var1x[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
            //var1x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            //var1x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_PROGRAMPINJAMAN", var1x, "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

            //ddlprogrampinjaman.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddlprogrampinjaman.DataTextField = "keterangan";
            //ddlprogrampinjaman.DataValueField = "kode";
            //ddlprogrampinjaman.DataBind();







            //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));


            //HDJenisPinjaman.Value = ge.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString();



            DefaultClass.Global.SqlHelper[] var1x1 = new DefaultClass.Global.SqlHelper[3];
        var1x1[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1x1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1x1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_cabangkoperasi", var1x1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";

        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "nama";
        ddlcabang.DataValueField = "kode_cabang";
        ddlcabang.DataBind();

        ddlcabang.SelectedValue = SessBranchID;



        //DefaultClass.Global.SqlHelper[] var1z = new DefaultClass.Global.SqlHelper[6];
        //var1z[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //var1z[1] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, HDJenisPinjaman.Value, typeof(string));
        //var1z[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        //var1z[3] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        //var1z[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        //var1z[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("GetSumberDanaCabangBank", var1z, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "kodebank asc";

        //ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlsumberdana.DataTextField = "kode_sumberdana";
        //ddlsumberdana.DataValueField = "Kodebank";
        //ddlsumberdana.DataBind();
        ////ddlsumberdana.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[3];
        //var1xx[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        //var1xx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        //var1xx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("AR_Jangka", var1xx, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

        //ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddljangka.DataTextField = "jangka";
        //ddljangka.DataValueField = "jangka";
        //ddljangka.DataBind();

        //ddljangka.Items.Insert(0, new ListItem("Pilih Jangka", "-"));


        ge.ListDataset = gb.GetDataFunction("dbo.GetPekerjaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlPekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlPekerjaan.DataTextField = "keterangan";
        //ddlPekerjaan.DataValueField = "id";
        //ddlPekerjaan.DataBind();


        ddlpekerjaanktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpekerjaanktp.DataTextField = "keterangan";
        ddlpekerjaanktp.DataValueField = "id";
        ddlpekerjaanktp.DataBind();


        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetSektorUsaha()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_sektor_usaha asc";

        ddlSektorUsahaKtp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlSektorUsahaKtp.DataTextField = "keterangan";
        ddlSektorUsahaKtp.DataValueField = "kode_sektor_usaha";
        ddlSektorUsahaKtp.DataBind();


        //ddlsektorusahapenjamin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlsektorusahapenjamin.DataTextField = "keterangan";
        //ddlsektorusahapenjamin.DataValueField = "kode_sektor_usaha";
        //ddlsektorusahapenjamin.DataBind();



        //// -- Combo Provinsi Pekerjaan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        //ddlprovinsipekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlprovinsipekerjaan.DataTextField = "nama_provinsi";
        //ddlprovinsipekerjaan.DataValueField = "kode_provinsi";
        //ddlprovinsipekerjaan.DataBind();



        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";
        ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpropinsiktp.DataTextField = "nama_provinsi";
        ddlpropinsiktp.DataValueField = "kode_provinsi";
        ddlpropinsiktp.DataBind();


        ddlprovinsipp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsipp.DataTextField = "nama_provinsi";
        ddlprovinsipp.DataValueField = "kode_provinsi";
        ddlprovinsipp.DataBind();

        ddlprovinsiaw.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsiaw.DataTextField = "nama_provinsi";
        ddlprovinsiaw.DataValueField = "kode_provinsi";
        ddlprovinsiaw.DataBind();

        ddlprovinsitpt.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsitpt.DataTextField = "nama_provinsi";
        ddlprovinsitpt.DataValueField = "kode_provinsi";
        ddlprovinsitpt.DataBind();

        // --- Combo Status Rumah
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusRumah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatusrumah.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatusrumah.DataTextField = "keterangan";
        ddlstatusrumah.DataValueField = "id";
        ddlstatusrumah.DataBind();



        // --- Combo Jenis Kelamin
        ge.ListDataset = gb.GetDataFunction("dbo.GetJenisKelamin()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddljenkelktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljenkelktp.DataTextField = "Keterangan";
        ddljenkelktp.DataValueField = "id";
        ddljenkelktp.DataBind();

        ddljeniskelaminpp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljeniskelaminpp.DataTextField = "Keterangan";
        ddljeniskelaminpp.DataValueField = "id";
        ddljeniskelaminpp.DataBind();


        ddljeniskelaminaw.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljeniskelaminaw.DataTextField = "Keterangan";
        ddljeniskelaminaw.DataValueField = "id";
        ddljeniskelaminaw.DataBind();

        ddljeniskelaminanak1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljeniskelaminanak1.DataTextField = "Keterangan";
        ddljeniskelaminanak1.DataValueField = "id";
        ddljeniskelaminanak1.DataBind();

        ddljeniskelaminanak2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljeniskelaminanak2.DataTextField = "Keterangan";
        ddljeniskelaminanak2.DataValueField = "id";
        ddljeniskelaminanak2.DataBind();

        ddljeniskelaminanak3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljeniskelaminanak3.DataTextField = "Keterangan";
        ddljeniskelaminanak3.DataValueField = "id";
        ddljeniskelaminanak3.DataBind();

        ddljeniskelaminpensiun.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljeniskelaminpensiun.DataTextField = "Keterangan";
        ddljeniskelaminpensiun.DataValueField = "id";
        ddljeniskelaminpensiun.DataBind();


            ddljeniskelaminanak1.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddljeniskelaminanak1.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddljeniskelaminanak2.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddljeniskelaminanak3.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ddljeniskelaminpensiun.Items.Insert(0, new ListItem("Pilih Data", "-"));



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


        ddlagamapp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlagamapp.DataTextField = "Keterangan";
        ddlagamapp.DataValueField = "id";
        ddlagamapp.DataBind();



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

            //ddlrekening.Enabled = false;
            //ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
            //ddlrekening.SelectedValue = "-";
            //  ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));


            //// --- Combo Pendidikan
            //ge.ListDataset = gb.GetDataFunction("dbo.GetJenisJaminan()", "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

            //ddljaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddljaminan.DataTextField = "namajaminan";
            //ddljaminan.DataValueField = "namajaminan";
            //ddljaminan.DataBind();


            //DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[3];
            //var2x[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
            //var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            //var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_listJaminan", var2x, "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

            //ddljaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            //ddljaminan.DataTextField = "namajaminan";
            //ddljaminan.DataValueField = "namajaminan";
            //ddljaminan.DataBind();

            //ddljaminan.Items.Insert(0, new ListItem("Pilih Jaminan", "-"));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

  
    protected void Data_Formula(object sender, EventArgs e)
    {
        try
        {


            if (ddlinstansi.SelectedValue != "" && ddlkategori.SelectedValue != "" && ddlKantorBayar.SelectedValue != "" && ddlsumberdana.SelectedValue != "" && ddlpensiun.SelectedValue != "" && ddlprogrampinjaman.SelectedValue != "" && txttgllahir.Text != "" && ddljangkawaktu.SelectedValue!="0")
            {

                if(Convert.ToDecimal(txtgajidisplay.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon isi gaji display terlebih dahulu');", true);
                    txtplafon.Text = "0";
                    return;
                }

                object tgltmt = CheckTglNull(txtmtpensiun.Text);
                object tglrealisasi = CheckTglNull(txttglrealisasi.Text);
                object tgllahir = CheckTglNull(txttgllahir.Text);


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[21];
                var[0] = gb.CreateSPParameter("@instansix", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlinstansi.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@Tglrealisasi", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglrealisasi, typeof(DateTime));
                var[2] = gb.CreateSPParameter("@C13Kategori", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlkategori.SelectedValue, typeof(string));
                var[3] = gb.CreateSPParameter("@C11KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@F9SUMBERDANA", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsumberdana.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@C17jangkawaktubulan", ParameterDirection.Input, SqlDbType.Int, 0, ddljangkawaktu.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@C21gajidiplay", ParameterDirection.Input, SqlDbType.Money, 0, txtgajidisplay.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@C19jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpensiun.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@C12ProdukGroup", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlprogrampinjaman.SelectedValue, typeof(string));
                var[9] = gb.CreateSPParameter("@G14Programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlproduk.SelectedValue, typeof(string));
                var[10] = gb.CreateSPParameter("@C20Statusnikah", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikah.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@C24tmtpensiun", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tgltmt, typeof(DateTime));
                var[12] = gb.CreateSPParameter("@C25Plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(string));
                var[13] = gb.CreateSPParameter("@kodesimulasi", ParameterDirection.Input, SqlDbType.VarChar, 2, txtkodesimulasi.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@TglLahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tgllahir, typeof(DateTime));
                var[15] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                //var[16] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KP_HITUNG_KANTOR_BAYAR_POS", typeof(string));
                var[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[17] = gb.CreateSPParameter("@C25Pelunasan", ParameterDirection.Input, SqlDbType.Money, 0, txtpelunasan.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
                var[19] = gb.CreateSPParameter("@PK", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlPk.SelectedValue, typeof(string));
                var[20] = gb.CreateSPParameter("@TunjanganIstriAnak", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlTunjanganIstriAnak.SelectedValue, typeof(string));
                if (ddlKantorBayar.SelectedValue == "A014" && ddlpensiun.SelectedValue=="1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_POS", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A009" && ddlsumberdana.SelectedValue== "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_CAPITAL", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A015" && ddlsumberdana.SelectedValue != "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_Capital_A", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A013" && ddlsumberdana.SelectedValue != "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_MANTAP", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A014" && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A015" && ddlsumberdana.SelectedValue != "A009" && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }
                else    if (ddlKantorBayar.SelectedValue == "A013"  && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    if (ge.ListDataset.Tables[0].Rows[0]["statusmsg"].ToString() == "1")
                    {
                        txtplafon.Text = "0";
                        var datamsg = ge.ListDataset.Tables[0].Rows[0]["msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + datamsg + "');", true);
                        return;
                    }
/*                    if(ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString() == "0")
                    {
                        txtplafon.Text = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rate Bunga Efektif tidak boleh 0 mohon di isi Master terlebih dahulu ');", true);
                        return;
                    }*/
                    if(ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString() == "0")
                    {
                        txtplafon.Text = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rate Bunga Flat tidak boleh 0 mohon di isi Master terlebih dahulu ');", true);
                        return;
                    }

                    if (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()) > decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()))
                    {

                        txtplafon.Text = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Plafon tidak boleh lebih besar dari Palfon Max');", true);
                        return;
                    }

                    else
                    {
                        if(txtkodesimulasi.Text == "A")
                        {
                            txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["UsiaPeminjam"].ToString());
                            txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Usiaasuransi"].ToString());
                            txtjangkatahun.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktutahun"].ToString());
                            txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljatuhtempo"].ToString()));
                            ddlangsurandimukablokir.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString());
                            ddlangsurandimuka.Enabled = true;
                            ddlmaterai.Enabled = true;
                            
                            //                        ddlangsurandimuka.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString());
                            //                        ddlangsurandimukaasuransi.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString());
                            //                        ddlmaterai.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString());
                            //                        ddlprovisi.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["persenProvisi"].ToString());
                            //ddlmutasikantorbayar.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["@@I23persenProvisi"].ToString());
                            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["ketanggota"].ToString();
                            txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["ketrekening"].ToString();
                            txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();
                            txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()));
                            txtjangkawaktumax.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString());
                            txtratebungapinjaman.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString());
                            txtratebungaefektif.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString());
                            txtrateasuransi.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString());
                            txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran"].ToString()));
                            txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenangsuran"].ToString()));
                            txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString()));
                            txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
                            txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total"].ToString()));
                            txttotalpersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpersen"].ToString()));
                            txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagaji"].ToString()));
                            txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Persensisagaji"].ToString()));
                            //                        txtprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString()));
                            txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenprovisi"].ToString()));
                            txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbank"].ToString()));
                            txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenadminbank"].ToString()));
                            txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));
                            //                        txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materai"].ToString()));
                            //txtmateraipcs.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString()));
                            txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokir"].ToString()));
                            txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString()));
                            //                        txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimuka"].ToString()));
                            //txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString()));
                            txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranAsuransidiblokir"].ToString()));
                            //txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString()));
                            txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tatalaksana"].ToString()));
                            txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanRp"].ToString()));
                            //                        txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["terimabersih"].ToString()));
                            decimal terimabersih = decimal.Parse(txtplafon.Text) - decimal.Parse(txtprovisi.Text) - decimal.Parse(txtadminbank.Text) - decimal.Parse(txtasuransi.Text) - decimal.Parse(txtkeanggotaan.Text) - decimal.Parse(txtangsuranblokir.Text) - decimal.Parse(txtangsurandimuka.Text) - decimal.Parse(txtangdimukaasuransi.Text) - decimal.Parse(txtmaterai.Text) - decimal.Parse(txtbiayaadmin.Text);
                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(terimabersih.ToString()));

                        }
                        else
                        {
                            ddlangsurandimuka.Items.Clear();
                            for (int i = 1; i <= 30; i++)
                            {
                                ddlangsurandimuka.Items.Add(new ListItem(i.ToString(), i.ToString()));

                            }
                            ddlangsurandimuka.Items.Insert(0, new ListItem("Pilih Angsuran Dimuka Angsuran", "0"));


                            txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["UsiaPeminjam"].ToString());
                            txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Usiaasuransi"].ToString());
                            txtjangkatahun.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktutahun"].ToString());
                            txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljatuhtempo"].ToString()));
                            ddlangsurandimukablokir.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString());
                            ddlangsurandimukablokir.Enabled = false;
                            ddlangsurandimuka.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["ddlangsurandimuka"].ToString());
                            ddlangsurandimuka.Enabled = false;
//                            ddlangsurandimukaasuransi.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString());
                            ddlmaterai.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString());
                            ddlmaterai.Enabled = false;
                            string persenprovisi = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["persenProvisi"].ToString());
                            ddlprovisi.Items.Clear();
                            ddlprovisi.Items.Insert(0, new ListItem(persenprovisi, persenprovisi));
                                
                            ddlprovisi.SelectedValue = persenprovisi;
                            txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["ketanggota"].ToString();
                            txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["ketrekening"].ToString();
                            txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();
                            txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()));
                            txtjangkawaktumax.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString());
                            txtratebungapinjaman.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString());
                            txtratebungaefektif.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString());
                            txtrateasuransi.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString());
                            txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran"].ToString()));
                            txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenangsuran"].ToString()));
                            txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString()));
                            txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
                            txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total"].ToString()));
                            txttotalpersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpersen"].ToString()));
                            txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagaji"].ToString()));
                            txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Persensisagaji"].ToString()));
                            txtprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString()));
                            txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenprovisi"].ToString()));
                            txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbank"].ToString()));
                            txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenadminbank"].ToString()));
                            txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));
                            txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materai"].ToString()));
                            txtmateraipcs.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString()));
                            txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokir"].ToString()));
                            txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString()));
                            txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimuka"].ToString()));
                            txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString()));
                            txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranAsuransidiblokir"].ToString()));
                            txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString()));
                            txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tatalaksana"].ToString()));
                            txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanRp"].ToString()));
                            txtgraceperiode.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gracePeriode"].ToString()));
//                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["terimabersih"].ToString()));
                            decimal terimabersih = decimal.Parse(txtplafon.Text) - decimal.Parse(txtasuransi.Text) - decimal.Parse(txtprovisi.Text) - decimal.Parse(txtadminbank.Text)  - decimal.Parse(txtkeanggotaan.Text) - 
                                decimal.Parse(txtangsuranblokir.Text) - decimal.Parse(txtangsurandimuka.Text)  - decimal.Parse(txtmaterai.Text) - decimal.Parse(txtbiayaadmin.Text) - decimal.Parse(txtgraceperiode.Text);
                            txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(terimabersih.ToString()));

                            if(ddlPk.SelectedValue == "2" && terimabersih > 0)
                            {
                                ShowPK2.Enabled = true;
                            }
                            else
                            {
                                ClearPanel2();
                                ShowPK2.Enabled = false;
                                pnlpinjaman2.Visible = false;
                            }

                        }



                    }



                }
                else
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", "0");
                    txtusiaasuransi.Text = string.Format("{0:N0}", "0");
                    ddlangsurandimukablokir.SelectedValue = "0";
                    ddlangsurandimuka.SelectedValue = "0";
                    ddlangsurandimukaasuransi.SelectedValue = "0";
                    ddlmaterai.SelectedValue = "0";
                    txtnama.Text = "";
                    txtstatuspinjaman.Text = "";
                    txtusiapinjaman.Text = "0";
                    txtusiaasuransi.Text = "0";
                    txtusiaanak1.Text = "0";
                    txtusiaanak2.Text = "0";
                    txtplafonmax.Text = "0";
                    txtjangkawaktumax.Text = "0";
                    txtusiajthtempo.Text = "0";
                    txtrateasuransi.Text = "0";
                    txtratebungapinjaman.Text = "0";
                    txtangsuran.Text = "0";
                    txtpsnangsuran.Text = "0";
                    txtasuransi.Text = "0";
                    txtpersenasuransi.Text = "0";
                    txttotal.Text = "0";
                    txttotalpersen.Text = "0";
                    txtsisagaji.Text = "0";
                    txtsisagajipersen.Text = "0";
                    txtprovisi.Text = "0";
                    txtpersenprovisi.Text = "0";
                    txtadminbank.Text = "0";
                    txtpersenadminbank.Text = "0";
                    txtkeanggotaan.Text = "0";
                    txtmaterai.Text = "0";
                    txtmateraipcs.Text = "0";
                    txtangsuranblokir.Text = "0";
                    txtangsuranblokirkali.Text = "0";
                    txtangsurandimuka.Text = "0";
                    txtangsurandimukakali.Text = "0";
                    txtangdimukaasuransi.Text = "0";
                    txtangdimukaasuransikali.Text = "0";
                    txtbiayaadmin.Text = "0";
                    txtpelunasanRp.Text = "0";
                    txtterimabersihrp.Text = "0";


                }



            }

            selectedprioritassumberdana();

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
    protected void Data_Formula_Child(object sender, EventArgs e)
    {
        try
        {


            if (ddlinstansi.SelectedValue != "" && ddlkategoriChild.SelectedValue != "" && ddlKantorBayarChild.SelectedValue != "" && ddlsumberdanaChild.SelectedValue != "" && ddlpensiun.SelectedValue != "" && ddlprogrampinjamanChild.SelectedValue != "" && txttgllahir.Text != "" && ddljangkawaktuChild.SelectedValue!="0")
            {

                if(Convert.ToDecimal(txtgajidisplay.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon isi gaji display terlebih dahulu');", true);
                    txtplafonChild.Text = "0";
                    return;
                }

                object tgltmt = CheckTglNull(txtmtpensiun.Text);
                object tglrealisasi = CheckTglNull(txttglrealisasiChild.Text);
                object tgllahir = CheckTglNull(txttgllahir.Text);


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[21];
                var[0] = gb.CreateSPParameter("@instansix", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlinstansi.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@Tglrealisasi", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglrealisasi, typeof(DateTime));
                var[2] = gb.CreateSPParameter("@C13Kategori", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlkategoriChild.SelectedValue, typeof(string));
                var[3] = gb.CreateSPParameter("@C11KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayarChild.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@F9SUMBERDANA", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsumberdanaChild.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@C17jangkawaktubulan", ParameterDirection.Input, SqlDbType.Int, 0, ddljangkawaktuChild.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@C21gajidiplay", ParameterDirection.Input, SqlDbType.Money, 0, txtgajidisplay.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@C19jenispensiun", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpensiun.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@C12ProdukGroup", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlprogrampinjamanChild.SelectedValue, typeof(string));
                var[9] = gb.CreateSPParameter("@G14Programpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlprodukChild.SelectedValue, typeof(string));
                var[10] = gb.CreateSPParameter("@C20Statusnikah", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlstatusnikah.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@C24tmtpensiun", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tgltmt, typeof(DateTime));
                var[12] = gb.CreateSPParameter("@C25Plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafonChild .Text, typeof(string));
                var[13] = gb.CreateSPParameter("@kodesimulasi", ParameterDirection.Input, SqlDbType.VarChar, 2, txtkodesimulasiChild.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@TglLahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tgllahir, typeof(DateTime));
                var[15] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                //var[16] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KP_HITUNG_KANTOR_BAYAR_POS", typeof(string));
                var[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[17] = gb.CreateSPParameter("@C25Pelunasan", ParameterDirection.Input, SqlDbType.Money, 0, txtpelunasanChild.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
                var[19] = gb.CreateSPParameter("@PK", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlPkChild.SelectedValue, typeof(string));
                var[20] = gb.CreateSPParameter("@TunjanganIstriAnak", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlTunjanganIstriAnak.SelectedValue, typeof(string));
                if (ddlKantorBayar.SelectedValue == "A014" && ddlpensiun.SelectedValue=="1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_POS", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A009" && ddlsumberdana.SelectedValue== "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_CAPITAL_PS", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A015" && ddlsumberdana.SelectedValue != "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_Capital_A", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A013" && ddlsumberdana.SelectedValue != "A009" && ddlpensiun.SelectedValue == "1")
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_MANTAP", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A014" && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }
                else if (ddlKantorBayar.SelectedValue == "A015" && ddlsumberdana.SelectedValue != "A009" && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }
                else    if (ddlKantorBayar.SelectedValue == "A013"  && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3"))
                {
                    ge.ListDataset = gb.GetDataJustOutPut("KP_HITUNG_KANTOR_BAYAR_JANDA_DUDA", var, "constrmf");
                }

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    if (ge.ListDataset.Tables[0].Rows[0]["statusmsg"].ToString() == "1")
                    {
                        txtplafon.Text = "0";
                        var datamsg = ge.ListDataset.Tables[0].Rows[0]["msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + datamsg + "');", true);
                        return;
                    }
                    if(ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString() == "0")
                    {
                        txtplafon.Text = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rate Bunga Flat tidak boleh 0 mohon di isi Master terlebih dahulu ');", true);
                        return;
                    }

                    if (decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()) > decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()))
                    {

                        txtplafon.Text = "0";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Plafon tidak boleh lebih besar dari Palfon Max');", true);
                        return;
                    }

                    else
                    {
                        if(txtkodesimulasi.Text == "A")
                        {
                            //txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["UsiaPeminjam"].ToString());
                            //txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Usiaasuransi"].ToString());
                            //txtjangkatahun.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktutahun"].ToString());
                            //txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljatuhtempo"].ToString()));
                            //ddlangsurandimukablokir.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString());
                            //ddlangsurandimuka.Enabled = true;
                            //ddlmaterai.Enabled = true;
                            //txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["ketanggota"].ToString();
                            //txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["ketrekening"].ToString();
                            //txtusiajthtempo.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();
                            //txtplafonmax.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()));
                            //txtjangkawaktumax.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString());
                            //txtratebungapinjaman.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString());
                            //txtratebungaefektif.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString());
                            //txtrateasuransi.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString());
                            //txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran"].ToString()));
                            //txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenangsuran"].ToString()));
                            //txtasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString()));
                            //txtpersenasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
                            //txttotal.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total"].ToString()));
                            //txttotalpersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpersen"].ToString()));
                            //txtsisagaji.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagaji"].ToString()));
                            //txtsisagajipersen.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Persensisagaji"].ToString()));
                            //txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenprovisi"].ToString()));
                            //txtadminbank.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbank"].ToString()));
                            //txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenadminbank"].ToString()));
                            //txtkeanggotaan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));
                            //txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokir"].ToString()));
                            //txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString()));
                            //txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranAsuransidiblokir"].ToString()));
                            //txtbiayaadmin.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tatalaksana"].ToString()));
                            //txtpelunasanRp.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanRp"].ToString()));
                            //decimal terimabersih = decimal.Parse(txtplafon.Text) - decimal.Parse(txtprovisi.Text) - decimal.Parse(txtadminbank.Text) - decimal.Parse(txtasuransi.Text) - decimal.Parse(txtkeanggotaan.Text) - decimal.Parse(txtangsuranblokir.Text) - decimal.Parse(txtangsurandimuka.Text) - decimal.Parse(txtangdimukaasuransi.Text) - decimal.Parse(txtmaterai.Text) - decimal.Parse(txtbiayaadmin.Text);
                            //txtterimabersihrp.Text = string.Format("{0:N0}", decimal.Parse(terimabersih.ToString()));

                        }
                        else
                        {
                            ddlangsurandimukaChild.Items.Clear();
                            for (int i = 1; i <= 30; i++)
                            {
                                ddlangsurandimukaChild.Items.Add(new ListItem(i.ToString(), i.ToString()));

                            }
                            ddlangsurandimukaChild.Items.Insert(0, new ListItem("Pilih Angsuran Dimuka Angsuran", "0"));


                            txtusiapinjamanChild.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["UsiaPeminjam"].ToString());
                            txtusiaasuransiChild.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Usiaasuransi"].ToString());
                            txtjangkatahunChild.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktutahun"].ToString());
                            txttgljatuhtempoChild.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljatuhtempo"].ToString()));
                            ddlangsurandimukablokirChild.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString());
                            ddlangsurandimukablokirChild.Enabled = false;
                            ddlangsurandimukaChild.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["ddlangsurandimuka"].ToString());
                            ddlangsurandimukaChild.Enabled = false;
                            ddlangsurandimukaasuransiChild.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString());
                            ddlmateraiChild.SelectedValue = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString());
                            ddlmateraiChild.Enabled = false;
                            string persenprovisi = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["persenProvisi"].ToString());
                            ddlprovisiChild.Items.Clear();
                            ddlprovisiChild.Items.Insert(0, new ListItem(persenprovisi, persenprovisi));

                            ddlprovisiChild.SelectedValue = persenprovisi;
                            txtnamaChild.Text = ge.ListDataset.Tables[0].Rows[0]["ketanggota"].ToString();
                            txtstatuspinjamanChild.Text = ge.ListDataset.Tables[0].Rows[0]["ketrekening"].ToString();
                            txtusiajthtempoChild.Text = ge.ListDataset.Tables[0].Rows[0]["usiajatuhtempo"].ToString();
                            txtplafonmaxChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonmax"].ToString()));
                            txtjangkawaktumaxChild.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["jangkawaktumax"].ToString());
                            txtratebungapinjamanChild.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaflat"].ToString());
                            txtratebungaefektifChild.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["bungasenaefektif"].ToString());
                            txtrateasuransiChild.Text = string.Format("{0:N2}", ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString());
                            txtangsuranChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran"].ToString()));
                            txtpsnangsuranChild.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenangsuran"].ToString()));
                            txtasuransiChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["asuransi"].ToString()));
                            txtpersenasuransiChild.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransi"].ToString()));
                            txttotalChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["total"].ToString()));
                            txttotalpersenChild.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalpersen"].ToString()));
                            txtsisagajiChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisagaji"].ToString()));
                            txtsisagajipersenChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Persensisagaji"].ToString()));
                            txtprovisiChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString()));
                            txtpersenprovisiChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenprovisi"].ToString()));
                            txtadminbankChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbank"].ToString()));
                            txtpersenadminbankChild.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persenadminbank"].ToString()));
                            txtkeanggotaanChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["keanggotaan"].ToString()));
                            txtmateraiChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materai"].ToString()));
                            txtmateraipcsChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kalimaterai"].ToString()));
                            txtangsuranblokirChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokir"].ToString()));
                            txtangsuranblokirkaliChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandiblokir"].ToString()));
                            txtangsurandimukaChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimuka"].ToString()));
                            txtangsurandimukakaliChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsurandimuka"].ToString()));
                            txtangdimukaasuransiChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranAsuransidiblokir"].ToString()));
                            txtangdimukaasuransikaliChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kaliangsuranAsuransidiblokir"].ToString()));
                            txtbiayaadminChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tatalaksana"].ToString()));
                            txtpelunasanRpChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pelunasanRp"].ToString()));
                            txtgraceperiodeChild.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["gracePeriode"].ToString()));
                            decimal terimabersih = decimal.Parse(txtplafonChild.Text) - decimal.Parse(txtasuransiChild.Text) - decimal.Parse(txtprovisiChild.Text) - decimal.Parse(txtadminbankChild.Text)  - decimal.Parse(txtkeanggotaanChild.Text) - 
                                decimal.Parse(txtangsuranblokirChild.Text) - decimal.Parse(txtangsurandimukaChild.Text)  - decimal.Parse(txtmateraiChild.Text) - decimal.Parse(txtbiayaadminChild.Text) - decimal.Parse(txtgraceperiodeChild.Text);
                            txtterimabersihrpChild.Text = string.Format("{0:N0}", decimal.Parse(terimabersih.ToString()));

                        }



                    }



                }
                else
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", "0");
                    txtusiaasuransi.Text = string.Format("{0:N0}", "0");
                    ddlangsurandimukablokir.SelectedValue = "0";
                    ddlangsurandimuka.SelectedValue = "0";
                    ddlangsurandimukaasuransi.SelectedValue = "0";
                    ddlmaterai.SelectedValue = "0";
                    txtnama.Text = "";
                    txtstatuspinjaman.Text = "";
                    txtusiapinjaman.Text = "0";
                    txtusiaasuransi.Text = "0";
                    txtusiaanak1.Text = "0";
                    txtusiaanak2.Text = "0";
                    txtplafonmax.Text = "0";
                    txtjangkawaktumax.Text = "0";
                    txtusiajthtempo.Text = "0";
                    txtrateasuransi.Text = "0";
                    txtratebungapinjaman.Text = "0";
                    txtangsuran.Text = "0";
                    txtpsnangsuran.Text = "0";
                    txtasuransi.Text = "0";
                    txtpersenasuransi.Text = "0";
                    txttotal.Text = "0";
                    txttotalpersen.Text = "0";
                    txtsisagaji.Text = "0";
                    txtsisagajipersen.Text = "0";
                    txtprovisi.Text = "0";
                    txtpersenprovisi.Text = "0";
                    txtadminbank.Text = "0";
                    txtpersenadminbank.Text = "0";
                    txtkeanggotaan.Text = "0";
                    txtmaterai.Text = "0";
                    txtmateraipcs.Text = "0";
                    txtangsuranblokir.Text = "0";
                    txtangsuranblokirkali.Text = "0";
                    txtangsurandimuka.Text = "0";
                    txtangsurandimukakali.Text = "0";
                    txtangdimukaasuransi.Text = "0";
                    txtangdimukaasuransikali.Text = "0";
                    txtbiayaadmin.Text = "0";
                    txtpelunasanRp.Text = "0";
                    txtterimabersihrp.Text = "0";


                }



            }

            selectedprioritassumberdana();

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    public void ClearPanel2()
    {
        txttglrealisasiChild.Text = string.Empty;
        txtkodesimulasiChild.Text = string.Empty;
        ddlKantorBayarChild.Items.Clear();
        ddlsumberdanaChild.Items.Clear();
        ddlprogrampinjamanChild.Items.Clear();
        ddlprodukChild.Items.Clear();
        ddlkategoriChild.Items.Clear();
        ddlPkChild.Items.Clear();
        ddljangkawaktuChild.Items.Clear();
        txtjangkatahunChild.Text = string.Empty;
        txttgljatuhtempoChild.Text = string.Empty;
        ddlangsurandimukablokirChild.Items.Clear();
        ddlangsurandimukaasuransiChild.Items.Clear();
        ddlmateraiChild.Items.Clear();
        ddlmutasikantorbayarChild.Items.Clear();
        ddlAOChild.Items.Clear();
        ddlnonaoChild.Items.Clear();
        ddlangsurandimukaChild.Items.Clear();
        ddljangkawaktuChild.Items.Clear();
        txtnamaChild.Text = string.Empty;
        txtstatuspinjamanChild.Text = string.Empty;
        txtusiapinjamanChild.Text = string.Empty;
        txtusiaasuransiChild.Text = string.Empty;
        txtusiaanak1Child.Text = string.Empty;
        txtusiaanak2Child.Text = string.Empty;
        txtplafonChild.Text = "0";
        txtplafonmaxChild.Text = "0";
        txtjangkawaktumaxChild.Text = "0";
        txtusiajthtempoChild.Text = "0";
        txtrateasuransiChild.Text = "0";
        txtratebungapinjamanChild.Text = "0";
        txtratebungaefektifChild.Text = "0";
        txtangsuranChild.Text = "0";
        txtasuransiChild.Text = "0";
        txttotalChild.Text = "0";
        txtsisagajiChild.Text = "0";
        txtprovisiChild.Text = "0";
        txtadminbankChild.Text = "0";
        txtkeanggotaanChild.Text = "0";
        txtmateraiChild.Text = "0";
        txtmateraipcsChild.Text = "0";
        txtangsuranblokirChild.Text = "0";
        txtangsurandimukaChild.Text = "0";
        txtangdimukaasuransiChild.Text = "0";
        txtbiayaadminChild.Text = "0";
        txtgraceperiodeChild.Text = "0";
        txtpelunasanRpChild.Text = "0";
        txtterimabersihrpChild.Text = "0";
    }

    protected void ShowPk2_Click(object sender, EventArgs e)
    {
        try
        {
            ClearPanel2();
            pnlpinjaman2.Visible = true;
            txttglrealisasiChild.Text = txttglrealisasi.Text;
            ddlKantorBayarChild.Items.Insert(0, new ListItem(ddlKantorBayar.SelectedItem.Text, ddlKantorBayar.SelectedValue));
            ddlKantorBayarChild.SelectedIndex = 0;
            txtkodesimulasiChild.Text = txtkodesimulasi.Text;
            ddlsumberdanaChild.Items.Insert(0, new ListItem(ddlsumberdana.SelectedItem.Text, ddlsumberdana.SelectedValue));
            ddlsumberdanaChild.SelectedIndex = 0;            
            ddlprogrampinjamanChild.Items.Insert(0, new ListItem(ddlprogrampinjaman.SelectedItem.Text, ddlprogrampinjaman.SelectedValue));
            ddlprogrampinjamanChild.SelectedIndex = 0;
            ddlprodukChild.Items.Insert(0, new ListItem(ddlproduk.SelectedItem.Text, ddlproduk.SelectedValue));
            ddlprodukChild.SelectedIndex = 0;            
            ddlkategoriChild.Items.Insert(0, new ListItem(ddlkategori.SelectedItem.Text, ddlkategori.SelectedValue));
            ddlkategoriChild.SelectedIndex = 0;            
            ddlPkChild.Items.Insert(0, new ListItem(ddlPk.SelectedItem.Text, ddlPk.SelectedValue));
            ddlPkChild.SelectedIndex = 0;
            ddljangkawaktuChild.Items.Insert(0, new ListItem(ddljangkawaktu.SelectedItem.Text, ddljangkawaktu.SelectedValue));
            ddljangkawaktuChild.SelectedIndex = 0;
            txtjangkatahunChild.Text = txtjangkatahun.Text;
            txttgljatuhtempoChild.Text = txttgljatuhtempo.Text;

            ddlangsurandimukablokirChild.Items.Clear();
            for (int i = 1; i <= 30; i++)
            {
                ddlangsurandimukablokirChild.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
            ddlangsurandimukablokirChild.Items.Insert(0, new ListItem("Pilih Angsuran Blokir", "0"));

            ddlangsurandimukaasuransi.Items.Clear();
            for (int i = 1; i <= 30; i++)
            {
                ddlangsurandimukaasuransi.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
            ddlangsurandimukaasuransiChild.Items.Insert(0, new ListItem("Pilih Angsuran Dimuka Asuransi", "0"));
            ddlangsurandimukaasuransiChild.Enabled = false;

            ddlmateraiChild.Items.Insert(0, new ListItem(ddlmaterai.SelectedItem.Text, ddlmaterai.SelectedValue));
            ddlmateraiChild.SelectedIndex = 0;
            ddlprovisiChild.Items.Insert(0, new ListItem(ddlprovisi.SelectedItem.Text, ddlprovisi.SelectedValue));
            ddlprovisiChild.SelectedIndex = 0;
            ddlmutasikantorbayarChild.Items.Insert(0, new ListItem(ddlmutasikantorbayar.SelectedItem.Text, ddlmutasikantorbayar.SelectedValue));
            ddlmutasikantorbayarChild.SelectedIndex = 0;
            ddlAOChild.Items.Insert(0, new ListItem(ddlAO.SelectedItem.Text, ddlAO.SelectedValue));
            ddlAOChild.SelectedIndex = 0;
            ddlnonaoChild.Items.Insert(0, new ListItem(ddlnonao.SelectedItem.Text, ddlnonao.SelectedValue));
            ddlnonaoChild.SelectedIndex = 0;
            ddlangsurandimukaChild.Items.Insert(0, new ListItem(ddlangsurandimuka.SelectedItem.Text, ddlangsurandimuka.SelectedValue));
            ddljangkawaktuChild.SelectedIndex = 0;
            txtnamaChild.Text = txtnama.Text;
            txtstatuspinjamanChild.Text = txtstatuspinjaman.Text;
            txtusiapinjamanChild.Text = txtusiapinjaman.Text;
            txtusiaasuransiChild.Text = txtusiaasuransi.Text;
            txtusiaanak1Child.Text = txtusiaanak1.Text;
            txtusiaanak2Child.Text = txtusiaanak2.Text;
            Data_Formula_Child(sender,e);

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void txtusia_OnChange(object sender, EventArgs e)
    {
        try
        {

            Data_Formula(sender, e);







        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    public static int DateDifMD(DateTime startDate, DateTime endDate)
    {
        int dayDiff = endDate.Day - startDate.Day;

        if (dayDiff >= 0)
        {
            return dayDiff;
        }
        else
        {
            // Ambil jumlah hari dari bulan sebelum endDate
            DateTime prevMonth = endDate.AddMonths(-1);
            int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            return daysInPrevMonth + dayDiff; // dayDiff bernilai negatif
        }
    }

    public static int DateDifY(DateTime startDate, DateTime endDate)
    {
        int dayDiff = endDate.Day - startDate.Day;

        if (dayDiff >= 0)
        {
            return dayDiff;
        }
        else
        {
            DateTime prevMonth = endDate.AddMonths(-1);
            int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            return daysInPrevMonth + dayDiff;
        }
    }

    public static int DateDifYM(DateTime startDate, DateTime endDate)
    {
        int monthDiff = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);

        if (endDate.Day < startDate.Day)
        {
            monthDiff--;
        }

        return monthDiff % 12;
    }



 
    protected void ddljangkawaktu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

           decimal AmountSpent1 = decimal.Parse(ddljangkawaktu.SelectedValue)/12;
            decimal d = Math.Round(AmountSpent1, 0);
            txtjangkatahun.Text = d.ToString();
            

            Data_Formula(sender, e);
           



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }


    }
   

    protected void txtusia_anak1_OnChange(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.

            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak1.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusiaanak1.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());
                    txtusia1.Text = txtusiaanak1.Text;
                }
                else
                {
                    txtusiaanak1.Text = string.Format("{0:N0}", "0");
                    txtusia1.Text = txtusiaanak1.Text;
                    //lblnopenbaru.Visible = true;
                }
            }
            //fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    protected void txtusia_anak2_OnChange(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.



            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak2.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusiaanak2.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());
                    txtusia2.Text = txtusiaanak2.Text;
                }
                else
                {
                    txtusiaanak2.Text = string.Format("{0:N0}", "0");
                    txtusia2.Text = txtusiaanak2.Text;


                }
            }
            //fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    protected void txtusia_anak3_OnChange(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.



            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahiranak3.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusia3.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());

                }
                else
                {
                    txtusia3.Text = string.Format("{0:N0}", "0");


                }
            }
            //fungsiAll();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    public void BtnApproveAntrian_Click(object sender, EventArgs e)
    {

        try

        {
            BtnApproveAntrian.Enabled = false;

            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglrealisasi.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();
            xsf = "0";
            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                BtnApproveAntrian.Enabled = true;
            }
            else
            {

                btnTolakAntrian.Enabled = false;
                DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[7];
                var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var88[1] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
                var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
                var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVEANTRIAN", typeof(string));
                var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var88, "constrmf");



                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {

                    
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
                    pnladd.Visible = false;
                    pnlIdentitas.Visible = false;
                    PnlFront.Visible = true;
                    pnlFilter.Visible = true;
                    //pnlJaminan.Visible = false;
                    //pnlPengajuan.Visible = false;
                    btnSimpan.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;

                    btnTolakAntrian.Visible = false;
                    PNLKELUARGA.Visible = false;
                    pnlAlasanTolak.Visible = false;
                    txtnonasabah.Text = "";
                    Pnlpenerima.Visible = false;
                    Pnlsimulasi.Visible = false;
                    PnlSKPensiun.Visible = false;
                    pnlpinjaman.Visible = false;
                    PnlRekeningBank.Visible = false;

                    pnlUpdAsuransi.Visible = false;
                    btnCancel.Visible = false;
                    nav.Visible = false;
                    BackNextAnggota.Visible = false;
                    BackNextAhliWaris.Visible = false;
                    BackNextSimulasi.Visible = false;
                    btnBackSimulasi.Visible = false;
                    //btndokumen.Visible = false;
                    ddlstatus_SelectedIndexChanged(sender, e);
                    txtnonasabah.Focus();
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
            BtnApproveAntrian.Enabled = true;
        }


    }

    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        Pnlpenerima.Visible = true;
        Pnlsimulasi.Visible = true;
        PnlSKPensiun.Visible = true;
        pnlpinjaman.Visible = true;
        PNLKELUARGA.Visible = true;

        pnlUpdAsuransi.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    
 
    protected void btnTolak_Click(object sender, EventArgs e)
    {




        btnSimpanTolakan.Enabled = true;
        btnSimpanTolakan.Visible = true;

        pnladd.Visible = false;
        pnlIdentitas.Visible = false;
      
        btnSimpan.Visible = false;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;

        btnTolakAntrian.Visible = false;
        pnlAlasanTolak.Visible = false;
        txtnonasabah.Text = "";
        Pnlpenerima.Visible = false;
        Pnlsimulasi.Visible = false;
        PnlSKPensiun.Visible = false;
        pnlpinjaman.Visible = false;
        PNLKELUARGA.Visible = false;
        pnlUpdAsuransi.Visible = false;
        btnCancel.Visible = false;
        //btndokumen.Visible = false;
        PnlRekeningBank.Visible = false;

        pnlAlasanTolak.Visible = true;
        nav.Visible = false;
        BackNextAnggota.Visible = false;
        BackNextAhliWaris.Visible = false;
        BackNextSimulasi.Visible = false;
        btnBackSimulasi.Visible = false;

        txtalasantolak.Focus();

    }
    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[7];
            var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[1] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
            var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REJECTSTATUSCAIR", typeof(string));
            var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
                pnladd.Visible = false;
                pnlIdentitas.Visible = false;
                PnlFront.Visible = true;
                pnlFilter.Visible = true;
                //pnlJaminan.Visible = false;
                //pnlPengajuan.Visible = false;
                PNLKELUARGA.Visible = false;
                btnSimpan.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
   
                btnTolakAntrian.Visible = false;
                pnlAlasanTolak.Visible = false;
                txtnonasabah.Text = "";
                Pnlpenerima.Visible = false;
                Pnlsimulasi.Visible = false;
                PnlSKPensiun.Visible = false;
                pnlpinjaman.Visible = false;

                pnlUpdAsuransi.Visible = false;
                btnCancel.Visible = false;
                //btndokumen.Visible = false;
                ddlstatus_SelectedIndexChanged(sender, e);
                txtnonasabah.Focus();
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
        pnlIdentitas.Visible = true;
        PnlFront.Visible = false;
        pnlFilter.Visible = false;
        //pnlJaminan.Visible = true;
        //pnlPengajuan.Visible = true;
        btnSimpan.Visible = true;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;

        btnTolakAntrian.Visible = false;
        pnlpinjaman.Visible = true;
        Pnlpenerima.Visible = true;
        Pnlsimulasi.Visible = true;
        PnlSKPensiun.Visible = true;
        pnlpinjaman.Visible = true;
        PNLKELUARGA.Visible = true;

        pnlUpdAsuransi.Visible = true;
        btnCancel.Visible = true;
        //btndokumen.Visible = true;
        Button2.Visible = false;

        btnSimpan.Visible = false;
        BtnApproveAntrian.Visible = true;
        BtnBatalkanmohon.Visible = true;
        BtnApproveAntrian.Enabled = true;

        pnlAlasanTolak.Visible = false;


   
        btnTolakAntrian.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }


    protected void btnbatalkanantrian_Click(object sender, EventArgs e)
    {
        



        try
        {
            btnTolakAntrian.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[7];
            var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[1] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
            var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BATALKANANTRIAN", typeof(string));
            var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
                pnladd.Visible = false;
                pnlIdentitas.Visible = false;
                PnlFront.Visible = true;
                pnlFilter.Visible = true;
                //pnlJaminan.Visible = false;
                //pnlPengajuan.Visible = false;
                btnSimpan.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
        
                btnTolakAntrian.Visible = false;
                pnlAlasanTolak.Visible = false;
                txtnonasabah.Text = "";
                Pnlpenerima.Visible = false;
                Pnlsimulasi.Visible = false;
                PnlSKPensiun.Visible = false;
                pnlpinjaman.Visible = false;
                PNLKELUARGA.Visible = false;
                pnlUpdAsuransi.Visible = false;
                btnCancel.Visible = false;
                PnlRekeningBank.Visible = false;
                //btndokumen.Visible = false;

                ddlstatus_SelectedIndexChanged(sender, e);
                txtnonasabah.Focus();
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

    protected void Uploadfoto1(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "AS1";
            txtfilename1.Text = namafileJaminan + ".jpg";

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }


            txtfoto1.Text = "";


            fileupload1.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP1 = folderPath + namafileJaminan;

            txtfoto1.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto1.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

            txtfoto1.Focus();

            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + imgfoto1.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);



        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto2(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "AS2";
            txtfilename2.Text = namafileJaminan + ".jpg";

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }


            txtfoto2.Text = "";

            fileupload2.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP2 = folderPath + namafileJaminan;

            txtfoto2.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto2.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + imgfoto2.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto3(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "AS3";
            txtfilename3.Text = namafileJaminan + ".jpg";

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }


            txtfoto3.Text = "";

            fileupload3.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP3 = folderPath + namafileJaminan;

            txtfoto3.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto3.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + imgfoto3.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto4(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "AS4";
            txtfilename4.Text = namafileJaminan + ".jpg";

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtfoto4.Text = "";


            fileupload4.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP4 = folderPath + namafileJaminan;

            txtfoto4.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";
            txtfoto4.Focus();
            //Display the Picture in Image control.
            imgfoto4.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + imgfoto4.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void Uploadfoto5(object sender, EventArgs e)
    {
        try
        {
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "AS5";
            txtfilename5.Text = namafileJaminan + ".jpg";

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtfoto5.Text = "";


            fileupload5.SaveAs(folderPath + namafileJaminan + ".jpg");

            fotopathKTP5 = folderPath + namafileJaminan;

            txtfoto5.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            imgfoto5.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + imgfoto5.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void Uploadfoto6(object sender, EventArgs e)
    {
        try
        {
            string extension = Path.GetExtension(fileupload6.FileName).ToLower();
            if (extension != ".pdf")
            {
				ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya file PDF yang diperbolehkan!');", true);
                txtPdf6.Focus();
                return;
            }


            int maxSize = 4 * 1024 * 1024; // 2 MB
            if (fileupload6.PostedFile.ContentLength > maxSize)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript",
                    "javascript:SweetAlert('Ukuran file maksimal 4 MB!');", true);
                return;
            }
			
			
            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "AS5";
            txtfilename6.Text = namafileJaminan + ".pdf";



            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtPdf6.Text = "";


            string uploadedFile = Path.Combine(folderPath, namafileJaminan + ".pdf");
            string compressedFile = Path.Combine(folderPath, namafileJaminan + "_tmp.pdf");

            fileupload6.SaveAs(uploadedFile);

            CompressPdf(uploadedFile, compressedFile);
            File.Delete(uploadedFile);
            File.Move(compressedFile, uploadedFile);



            txtPdf6.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".pdf";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + txtPdf6.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
	
	private void CompressPdf(string inputPath, string outputPath)
	{
		using (var reader = new PdfReader(inputPath))
		{
			reader.RemoveUnusedObjects();

			using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
			{
				using (var document = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(1)))
				{
					PdfCopy copy = new PdfCopy(document, fs);
					copy.SetFullCompression();
					document.Open();

					for (int i = 1; i <= reader.NumberOfPages; i++)
					{
						PdfImportedPage page = copy.GetImportedPage(reader, i);
						copy.AddPage(page);
					}

					document.Close();
				}
			}
		}
	}



    protected void ddlinstansi_selected_index(object sender, EventArgs e)
    {
        if(ddlinstansi.SelectedItem.Text == "Asabri")
        {
            divNik.Visible = true;
        }
        else
        {
            divNik.Visible = false;

        }
    }

    public void selectedprioritassumberdana()
    {
        try
        {
            if(Convert.ToDecimal(txtplafonmax.Text) > 0 && Convert.ToInt32(ddljangkawaktu.SelectedValue) > 0 && Convert.ToInt32(txtusiaasuransi.Text) >0)
            {
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[10];
                Params[0] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
                Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PRIORITASKRITERIA", typeof(string));       
                Params[3] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafonmax.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 0, SessBranchID, typeof(string));
                Params[5] = gb.CreateSPParameter("@jangkawaktu", ParameterDirection.Input, SqlDbType.Int, 0, txtjangkatahun.Text, typeof(int));
                Params[6] = gb.CreateSPParameter("@rate", ParameterDirection.Input, SqlDbType.Float, 0, txtratebungapinjaman.Text, typeof(float));
                Params[7] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.Int, 0,txtusiaasuransi.Text, typeof(int));
                Params[8] = gb.CreateSPParameter("@kantorbayar", ParameterDirection.Input, SqlDbType.VarChar, 0,ddlKantorBayar.SelectedValue, typeof(string));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_Mstr_Kriteria_SumberDana", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    ddlsumberdana.SelectedValue = ge.ListDataset.Tables[0].Rows[0][0].ToString();
                }
            }


        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void ddlTunjanganIStriAnak_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlTunjanganIstriAnak.SelectedValue == "Ya")
        {
            txttunjistri.Enabled = true;
            txttunjangananak.Enabled = true;
        }
        else
        {
            txttunjistri.Enabled = false;
            txttunjangananak.Enabled = false;
            txttunjistri.Text = "0";
            txttunjangananak.Text = "0";

        }
    }
}