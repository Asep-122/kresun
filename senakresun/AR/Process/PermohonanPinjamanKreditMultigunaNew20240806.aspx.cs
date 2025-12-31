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



public partial class AR_Process_PermohonanPinjamanKreditMultigunaNew20240806 : System.Web.UI.Page
{
    String nosebelumnya = "", updateno = "", CRno_rekening = "", STATE = "", kdpersh = "", format_no_pk = "",
             formatnumber = "", formatpknumber = "", formatnumberbukti = "", nopksebelumnya = "", CRno_pk = "", updatepkno = "", pokok = "", bunga = "",
             typetrn = "", keterangantrn = "", no_bukti_sebelumnya = "", NoBuktiNew = "", DebitGL = "", KreditGL1 = "", KreditGL2 = "",
             KodeJurnal = "", KreditGL3 = "", KreditGL4 = "", kodebank = "";
    double ttlbunga = 0, sisapk = 0, ttlpokok = 0, pokokbunga = 0, ttlkeluarcl = 0;
    int counter_no_rekening = 0, i = 0, x = 0;

    long no_pk_sekarang = 0, no_pk = 0, nosekarang = 0, no_rekening = 0, no_bukti_sekarang = 0, no_bukti = 0;

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gbx = new DefaultClass.Global();
    GlobalEntities gex = new GlobalEntities();

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    double angsuranperbulan = 0;
    string nonasabah = "", agama = "", usia = "", namafileJaminan = "", fotopathJaminan = "";

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


    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            InsertCombo();
           
            ddlstatus_SelectedIndexChanged(sender, e);
            txtkodecabang.Text = SessBranchID;
            lbllabakotor.Font.Bold = true;
            lbltotalbiaya.Font.Bold = true;
            lbllabasebelumpajak.Font.Bold = true;
            lbllababersih.Font.Bold = true;

            //ddlSurvey.Items.Add("Pilih Data");
            //ddlSurvey.Items.Add("Belum Dilakukan Survey");
            ddlSurvey.Items.Add("Sudah Dilakukan Survey");
            ddlSurvey.Visible = false;
            ddlSurvey.SelectedValue = "Sudah Dilakukan Survey";

            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";

            //txttglrealisasi.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            //get tanggal server
            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
            txttglrealisasi.Text = xtanggal.ToString("dd-MM-yyyy");

            ddlstatus.Focus();

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
    protected void ddljenispinjaman_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlbycad.SelectedValue == "1")
        {
            txtkalicadangan.Text = "1";
            txtkalicadangan.Enabled = true;
            txtkalicadangan_TextChanged(sender, e);

        }
        else if (ddlbycad.SelectedValue == "0")
        {
            txtkalicadangan.Enabled = false;
            TextBox3.Text = "0";
            txtkalicadangan.Text = "0";
            txtkalicadangan_TextChanged(sender, e);
        }
        else if (ddlbycad.SelectedValue == "2")
        {
            txtkalicadangan.Text = "1";
            txtkalicadangan.Enabled = true;
            txtkalicadangan_TextChanged(sender, e);

        }

    }
    protected void ddlbycad_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlbycad.SelectedValue == "1")
        {
            txtkalicadangan.Text = "1";
            txtkalicadangan.Enabled = true;
            txtkalicadangan_TextChanged(sender, e);

        }
        else if (ddlbycad.SelectedValue == "0")
        {
            txtkalicadangan.Enabled = false;
            TextBox3.Text = "0";
            txtkalicadangan.Text = "0";
            txtkalicadangan_TextChanged(sender, e);
        }
        else if (ddlbycad.SelectedValue == "2")
        {
            txtkalicadangan.Text = "1";
            txtkalicadangan.Enabled = true;
            txtkalicadangan_TextChanged(sender, e);

        }

    }

    
    protected void ddlmajuhari_SelectedIndexChanged(object sender, EventArgs e)


    {
        
        int x = int.Parse(ddlmajuhari.SelectedValue);
        x = -x;

        if (ddlprodukpengajuan.SelectedValue == "3C")
        {
            txttgltagih.Text = DateTime.Parse(txttglrealisasi.Text).AddDays(x).ToString("dddd");
        }
        else
        {
            txttgltagih.Text = DateTime.Parse(txttglrealisasi.Text).AddDays(x).ToString("dd");
        }


       

    }


    protected void ddljenispermohonan_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlrekening.Items.Clear();
        if (ddlpengajuan.SelectedValue=="B")
        {


            ddlrekening.Enabled = false;
            //ddlrekening.Text = "-";
            ddlrekening.Items.Insert(0, new ListItem("Pilih Rekening ", "-"));
            txtketeranganJaminan.Text = "";

            txtkondisiJaminan.Text = "";
            txttaksasi.Text = "0";
            lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse("0"));

        }
        else if (ddlpengajuan.SelectedValue== "P")
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[1] = gb.CreateSPParameter("@jns_permohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("GetRekeningPerpanjangan", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";
            ddlrekening.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlrekening.DataTextField = "no_rekening";
            ddlrekening.DataValueField = "no_rekening";
            ddlrekening.DataBind();
            ddlrekening.Enabled = true;
            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddlrekening.Items.Insert(1, new ListItem("Rekening Lama Tidak Ada", "B"));
            ddlrekening.Enabled = false;
            ddlrekening.SelectedValue = "-";

            lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse("0"));
        }
        else if (ddlpengajuan.SelectedValue == "X")
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[1] = gb.CreateSPParameter("@jns_permohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAMINAN", typeof(string));
            var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("GetRekeningPerpanjangan", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";
            ddlrekening.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlrekening.DataTextField = "no_rekening";
            ddlrekening.DataValueField = "no_rekening";
            ddlrekening.DataBind();
            ddlrekening.Enabled = true;
            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddlrekening.Items.Insert(1, new ListItem("Rekening Lama Tidak Ada", "B"));
            ddlrekening.Enabled = true;
            ddlrekening.SelectedValue = "-";

            lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse("10000"));
            //if (decimal.Parse(txtsimpok.Text) > 0)
            //{
            //    lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse("10000"));
            //}
            //else
            //{
            //    lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse("0"));
            //}


        }

    }

    protected void ddlrekening_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
        var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrekening.SelectedValue , typeof(string));
        var1[2] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
        var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REKENINGLUNAS", typeof(string));
        var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GetRekeningPerpanjangan", var1, "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {

        
            txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminan"].ToString();
            //txtpathjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
            txtkondisiJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
            txttaksasi.Text=string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
            //imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();// + "?r=" + DateTime.Now
           if (ddlprodukpengajuan.SelectedValue== ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString())
            {
                ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
            }
           else
            {
                ddljaminan.SelectedValue = "-";
            }
            
        }
        else
        {
            //txtketeranganJaminan.Text = "";
            ////txtpathjaminan.Text = "";
            //txtkondisiJaminan.Text = "";
            //txttaksasi.Text = "0";
        }
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    
        //Session.Contents.Add("Statusses", ddlstatus.SelectedValue);
       
        BindGridEntity();


    }

    

    protected void ddlbiayaanggota_SelectedIndexChanged(object sender, EventArgs e)
    {

        //var keteranganJaminan = txtketeranganJaminan.Text;
        //var kondisiJaminan = txtkondisiJaminan.Text;
        //decimal taksasi = 0;
        //taksasi =  decimal.Parse(txttaksasi.Text);

        if (ddlbiayaanggota.SelectedValue == "0")
        {
            if (decimal.Parse(txtsimpok.Text) == 0 ||  ddlpengajuan.SelectedValue=="X")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota tidak boleh Simpanan Pokok 0');", true);
                ddlbiayaanggota.SelectedValue = "1";
                LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("20000"));
            }
            else
            {
                LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(""));
            }                  

        }
        else if (ddlbiayaanggota.SelectedValue == "1")
        {
            LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("20000"));
        }
        else if (ddlbiayaanggota.SelectedValue == "2")
        {
    
                LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("0"));


        }
     
 


    }


    protected void ddluangmuka_SelectedIndexChanged(object sender, EventArgs e)
    {

        //var keteranganJaminan = txtketeranganJaminan.Text;
        //var kondisiJaminan = txtkondisiJaminan.Text;
        //decimal taksasi = 0;
        //taksasi =  decimal.Parse(txttaksasi.Text);

        if (ddluangmuka.SelectedIndex == 0)
        {
            txtkaliuangmuka.Text = "0";
            txtuangmuka.Text = "0";
            txtkaliuangmuka.Enabled = false;
            txtpokokuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));

        }
        else if (ddluangmuka.SelectedIndex == 1)
        {
            txtkaliuangmuka.Enabled = true;
            txtkaliuangmuka.Text = "1";
            txtpokokuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));
            txtuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtkaliuangmuka.Text) * decimal.Parse(txtangsuranperbulan.Text)); 
   
           

        }

        //txtketeranganJaminan.Text = keteranganJaminan;
        //txtkondisiJaminan.Text = kondisiJaminan;
        //txttaksasi.Text = string.Format("{0:N2}", taksasi);


    }

    protected void txtkaliuangmuka_TextChanged(object sender, EventArgs e)
    {
        Decimal biayauangmuka = 0, totuangmuka = 0;
        txtpokokuangmuka.Text= string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));
        biayauangmuka = decimal.Parse(txtkaliuangmuka.Text) * decimal.Parse(txtangsuranperbulan.Text);
        txtuangmuka.Text = string.Format("{0:N2}", biayauangmuka);

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
        pnlJaminan.Visible = false;
        pnlPengajuan.Visible = false;
        btnSimpan.Visible = false;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;
        BtnApproveAntrian.Visible = false;
        BtnBatalkanmohon.Visible = false;
        btnlanjutkan.Visible = false;
        btnTolakAntrian.Visible = false;
        pnlAlasanTolak.Visible = false;
        txtnonasabah.Text = "";
        txtketeranganJaminan.Text = "";
        txtkondisiJaminan.Text = "-";
        txttaksasi.Text = "0";
        txtktppenjamin.Text = "0";
        txtnamapenjamin.Text = "-";
        txtnamaibukandung.Text = "-";
        txtNPWP.Text = "0";
        txtnomerkk.Text = "0";
        txtcadsebelumnya.Text = "0";
        txtkembalicadangan.Text = "0";
        txtuangmuka.Text = "0";
        txtkaliuangmuka.Text = "0";
        TextBox2.Text = "0";
        ddljangka.SelectedValue = "-";
        Pnlupdatedata.Visible = false;
        ddlstatus_SelectedIndexChanged(sender, e);
        txtnonasabah.Focus();
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

    protected void UploadFileKK(object sender, EventArgs e)
    {

        string fotopathkk;

        //if (fileuploadKK.HasFile)
        //{


        //    var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

        //    if (fileuploadKK.PostedFile.ContentLength > maxFileSize)
        //    {


        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

        //    }

        //    else
        //    {

        //        string namafile;
        //        string filename;
        //        string filenamex;

        //        DateTime now = DateTime.Now;
        //        namafile = txtnomerkk.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "KK";
        //        string folderPath = Server.MapPath("~/Files/KK/thumb");
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }


        //        filename = namafile + ".jpg";
        //        filenamex = namafile + "OK.jpg";


        //        //create temp
        //        string path = Server.MapPath("~/Files/KK/thumb/" + namafile + ".jpg");
        //        fileuploadKK.SaveAs(path);
        //        //create temp

        //        System.Drawing.Image image = System.Drawing.Image.FromFile(path);
        //        float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
        //        int newHeight = 200;
        //        int newWidth = Convert.ToInt32(aspectRatio * newHeight);
        //        System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
        //        System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
        //        thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        //        thumbGraph.DrawImage(image, imageRectangle);
        //        thumbBitmap.Save(Server.MapPath("~/Files/KK/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
        //        thumbGraph.Dispose();
        //        thumbBitmap.Dispose();
        //        image.Dispose();
        //        fotopathkk = "~/Files/KK/" + filenamex;
        //        txtpathKK.Text = fotopathkk;
        //        FileInfo file = new FileInfo(path);
        //        if (file.Exists)//check file exsit or not.
        //        {
        //            file.Delete();
        //        }

        //        Imagekk.ImageUrl = "http://gigaku.kspserambidana.com/Files/KK/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();
        //    }

        //}



    }

    protected void UploadFileNPWP(object sender, EventArgs e)
    {

        string fotopathnpwp;

        //if (fileuploadnpwp.HasFile)
        //{


        //    var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

        //    if (fileuploadnpwp.PostedFile.ContentLength > maxFileSize)
        //    {


        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

        //    }

        //    else
        //    {

        //        string namafile;
        //        string filename;
        //        string filenamex;

        //        DateTime now = DateTime.Now;
        //        namafile = txtNPWP.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "NPWP";
        //        string folderPath = Server.MapPath("~/Files/NPWP/thumb");
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }


        //        filename = namafile + ".jpg";
        //        filenamex = namafile + "OK.jpg";


        //        //create temp
        //        string path = Server.MapPath("~/Files/NPWP/thumb/" + namafile + ".jpg");
        //        fileuploadnpwp.SaveAs(path);
        //        //create temp

        //        System.Drawing.Image image = System.Drawing.Image.FromFile(path);
        //        float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
        //        int newHeight = 200;
        //        int newWidth = Convert.ToInt32(aspectRatio * newHeight);
        //        System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
        //        System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
        //        thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        //        thumbGraph.DrawImage(image, imageRectangle);
        //        thumbBitmap.Save(Server.MapPath("~/Files/NPWP/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
        //        thumbGraph.Dispose();
        //        thumbBitmap.Dispose();
        //        image.Dispose();
        //        fotopathnpwp = "~/Files/NPWP/" + filenamex;
        //        txtpathnpwp.Text = fotopathnpwp;
        //        FileInfo file = new FileInfo(path);
        //        if (file.Exists)//check file exsit or not.
        //        {
        //            file.Delete();
        //        }
        //        Imagenpwp.ImageUrl = "http://gigaku.kspserambidana.com/Files/NPWP/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();

        //    }

        //}



    }

    protected void UploadFilektppasangan(object sender, EventArgs e)
    {

        string fotopathpasangan;

        //if (fileuploadktppasangan.HasFile)
        //{


        //    var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

        //    if (fileuploadktppasangan.PostedFile.ContentLength > maxFileSize)
        //    {


        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

        //    }

        //    else
        //    {

        //        string namafile;
        //        string filename;
        //        string filenamex;

        //        DateTime now = DateTime.Now;
        //        namafile = txtidpasangan.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "KTP_Pasangan";
        //        string folderPath = Server.MapPath("~/Files/KTP_Pasangan/thumb");
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }


        //        filename = namafile + ".jpg";
        //        filenamex = namafile + "OK.jpg";


        //        //create temp
        //        string path = Server.MapPath("~/Files/KTP_Pasangan/thumb/" + namafile + ".jpg");
        //        fileuploadktppasangan.SaveAs(path);
        //        //create temp

        //        System.Drawing.Image image = System.Drawing.Image.FromFile(path);
        //        float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
        //        int newHeight = 200;
        //        int newWidth = Convert.ToInt32(aspectRatio * newHeight);
        //        System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
        //        System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
        //        thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        //        thumbGraph.DrawImage(image, imageRectangle);
        //        thumbBitmap.Save(Server.MapPath("~/Files/KTP_Pasangan/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
        //        thumbGraph.Dispose();
        //        thumbBitmap.Dispose();
        //        image.Dispose();
        //        fotopathpasangan = "~/Files/KTP_Pasangan/" + filenamex;
        //        txtppathktppasangan.Text = fotopathpasangan;
        //        FileInfo file = new FileInfo(path);
        //        if (file.Exists)//check file exsit or not.
        //        {
        //            file.Delete();
        //        }
        //        ImageKtppasangan.ImageUrl = "http://gigaku.kspserambidana.com/Files/KTP_Pasangan/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();

        //    }

        //}



    }
    protected void tambahplafon(object sender, EventArgs e)
    {
        try
        {

           

            decimal plafonnilai = 0;

            if (ddljaminan.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('hayo gabisa diklik ya? cek dulu Jaminan belum diisi tuh');", true);
                ddljaminan.Focus();
            }

            else if (ddljangka.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('hayo gabisa diklik ya? cek dulu Jangka belum diisi tuh');", true);
                ddljangka.Focus();
            }
            else
            { 
            plafonnilai = decimal.Parse(txtplafon.Text);
        if (plafonnilai < 50000000)
        {
            plafonnilai = plafonnilai + 250000;
            txtplafon.Text = string.Format("{0:N2}", plafonnilai);
        }

        if (ddljangka.SelectedValue != "-")
        {
            ddljangka_SelectedIndexChanged(sender, e);
        }
      }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void kurangplafon(object sender, EventArgs e)
    {
        try
        {


            decimal plafonnilai = 0;


            if (ddljaminan.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('hayo gabisa diklik ya? cek dulu Jaminan belum diisi tuh');", true);
                ddljaminan.Focus();
            }
            else if (ddljangka.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('hayo gabisa diklik ya? cek dulu Jangka belum diisi tuh');", true);
                ddljangka.Focus();
            }
            else
            {

           

                        plafonnilai = decimal.Parse(txtplafon.Text);
                    if (plafonnilai > 750000)
                    {
                        plafonnilai = plafonnilai - 250000;
                        txtplafon.Text = string.Format("{0:N2}", plafonnilai);
                    }
                    if (ddljangka.SelectedValue != "-")
                    {
                        ddljangka_SelectedIndexChanged(sender, e);
                    }


            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
    protected void UploadFileJaminan(object sender, EventArgs e)
    {

        string fotopathjaminan;

        //if (fileuploadjaminan.HasFile)
        //{


        //    var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

        //    if (fileuploadjaminan.PostedFile.ContentLength > maxFileSize)
        //    {


        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

        //    }

        //    else
        //    {

        //        string namafile;
        //        string filename;
        //        string filenamex;

        //        DateTime now = DateTime.Now;
        //        namafile = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "Jaminan";
        //        string folderPath = Server.MapPath("~/Files/Jaminan/thumb");
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }


        //        filename = namafile + ".jpg";
        //        filenamex = namafile + "OK.jpg";


        //        //create temp
        //        string path = Server.MapPath("~/Files/Jaminan/thumb/" + namafile + ".jpg");
        //        fileuploadjaminan.SaveAs(path);
        //        //create temp

        //        System.Drawing.Image image = System.Drawing.Image.FromFile(path);
        //        float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
        //        int newHeight = 200;
        //        int newWidth = Convert.ToInt32(aspectRatio * newHeight);
        //        System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
        //        System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
        //        thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        //        thumbGraph.DrawImage(image, imageRectangle);
        //        thumbBitmap.Save(Server.MapPath("~/Files/Jaminan/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
        //        thumbGraph.Dispose();
        //        thumbBitmap.Dispose();
        //        image.Dispose();
        //        fotopathjaminan = "~/Files/Jaminan/" + filenamex;
        //        txtpathjaminan.Text = fotopathjaminan;
        //        FileInfo file = new FileInfo(path);
        //        if (file.Exists)//check file exsit or not.
        //        {
        //            file.Delete();
        //        }
        //        imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();

        //    }

        //}



    }
    protected void UploadFileJaminan_X(object sender, EventArgs e)
    {
        try
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

            //fileuploadjaminan.SaveAs(folderPath + namafileJaminan + ".jpg");

            ////Display the Picture in Image control.
            //imagenpwp.ImageUrl = "~/Files/NPWP/" + Path.GetFileName(fileuploadnpwp.FileName);

            fotopathJaminan = folderPath + namafileJaminan;

            //txtpathjaminan.Text = "~/Files/jaminan/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            //imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            //imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            // PathFile
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlIdentitas.Visible = true;
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
    
    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {
        //if (ddlkodeinstansi.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Instansi belum di pilih!');", true);

        //}
        //else
        //{
            pnlPekerjaan.Visible = false;
            pnlIdentitas.Visible = false;
            pnlTambahan.Visible = true;
        //}

    }
    protected void ddlkepemilikantempatusaha_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlkepemilikantempatusaha.SelectedIndex == 0 || ddlkepemilikantempatusaha.SelectedIndex == 1)
        {
            txtkepemilikantempatusaha.Enabled = false;
            txtkepemilikantempatusaha.Text = "-";
        }else
        {

            txtkepemilikantempatusaha.Enabled = true;
            txtkepemilikantempatusaha.Text = "-";
        }
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
        pnlLaba.Visible = false;
        //pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = true;
    }
    protected void btnNextPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        //pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = false;
        pnlJaminan.Visible = true;


    }
    protected void btnBackPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false; 
        pnlPendapatan.Visible = true; 
        pnlPihakYgDapatDiHubungi.Visible = false;

    }


    
          protected void btnNextJaminan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        //pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
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
        pnlLaba.Visible = false;
        //pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = true;
        pnlPengajuan.Visible = false;
        pnlJaminan.Visible = false;

    }
    protected void btnNextLaba_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        //pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        pnlPihakYgDapatDiHubungi.Visible = true;

    }
    protected void btnBackKriteria_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        //pnlKriteria.Visible = false;
        //pnlUsaha.Visible = true;
         

    }



    protected void btnNextKriteria_Click(object sender, EventArgs e)
    {

    
        //if (ddlSurvey.SelectedIndex == 1)
        //{
            //pnlJaminan.Visible = false;
            //btnNextKriteria.Visible = false;
            //btnSimpan.Visible = true;
            //pnlPengajuan.Visible = true;
            //pnlKeanggotaan.Visible = true;
        //}
        //else if (ddlSurvey.SelectedIndex == 2)
        //{
        pnlJaminan.Visible = true;
        //btnNextKriteria.Visible = false;
        //pnlKriteria.Visible = false;
        //}



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
        //pnlKeanggotaan.Visible = false;
        //pnlKriteria.Visible = true;
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
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = false;
        //pnlKeanggotaan.Visible = false;
        pnlPihakYgDapatDiHubungi.Visible = false;
        pnlJaminan.Visible = true;
    }
    protected void btnBackUsaha_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;
        pnlLaba.Visible = false;
        pnlPengajuan.Visible = true;
        //pnlKeanggotaan.Visible = true;
        //pnlUsaha.Visible = false;
        pnlJaminan.Visible = false;
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

    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        //varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglrealisasi.Text, typeof(DateTime));
        //varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        //varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        //string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        //if (xstatus == "1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
        //    txttglrealisasi.Text = xtanggal.ToString("dd-MM-yyyy");
        //}
        int x = int.Parse(ddlmajuhari.SelectedValue);
        x = -x;

        if (ddlprodukpengajuan.SelectedValue == "3C")
        {
            txttgltagih.Text = DateTime.Parse(txttglrealisasi.Text).AddDays(x).ToString("dddd");
        }
        else
        {
            txttgltagih.Text = DateTime.Parse(txttglrealisasi.Text).AddDays(x).ToString("dddd dd/MM/yyyy");
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


    protected void btnCekcadangan_Click(object sender, EventArgs e)
    {
        try
        {
            
            DefaultClass.Global.SqlHelper[] varcad = new DefaultClass.Global.SqlHelper[3];
            varcad[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            varcad[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCADANGAN", typeof(string));
            varcad[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varcad, "constrmf");

            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                txtcadanganRekening.Text = "";
                txtcadsebelumnya.Text = "0";
            }
            else
            {


                txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                txtcadsebelumnya.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["sisacadangan"].ToString());
                txtcadsebelumnya.Text = txtcadsebelumnya.Text.Replace("Rp", "");

            }

            if (ddlbycad.SelectedValue == "1")
            {
                txtkalicadangan.Text = "1";
                txtkalicadangan.Enabled = true;
                txtkalicadangan_TextChanged(sender, e);

            }
            else if (ddlbycad.SelectedValue == "0")
            {
                txtkalicadangan.Enabled = false;
                TextBox3.Text = "0";
                txtkalicadangan.Text = "0";
                txtkalicadangan_TextChanged(sender, e);
            }
            else if (ddlbycad.SelectedValue == "2")
            {
                txtkalicadangan.Text = "1";
                txtkalicadangan.Enabled = true;
                txtkalicadangan_TextChanged(sender, e);

            }


        }


        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
     protected void btnSimpan_Click(object sender, EventArgs e)
     {
        try
        {
            decimal taksas=0;
            taksas = decimal.Parse(txttaksasi.Text);

            btnSimpan.Enabled = false;

            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglrealisasi.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {


                if (ddlpengajuan.SelectedValue == "X" && ddlrekening.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masukkan Rekening Lama Perpanjangan');", true);
                    btnSimpan.Enabled = true;
                    ddlrekening.Focus();

                }

                else if (ddluangmuka.SelectedValue == "1" && (ddlbycad.SelectedValue == "0" && decimal.Parse(txtcadsebelumnya.Text) > 0))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Lakukan Pengembalian Cadangan di Teller');", true);
                    btnSimpan.Enabled = true;
                    ddlrekening.Focus();

                }
                else if (ddlbycad.SelectedValue == "0" && decimal.Parse(txtcadsebelumnya.Text) > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cadangan Sebelumnya ada, tidak boleh pilih Jenis Biaya Cadangan Tanpa Cadangan');", true);
                    btnSimpan.Enabled = true;
                    ddlbycad.Focus();

                }


                else if (ddlbycad.SelectedValue == "1" && txtkalicadangan.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis Biaya Cadangan Dengan Cadangan Tidak Boleh Biaya Cadangan 0');", true);
                    btnSimpan.Enabled = true;
                    ddlbycad.Focus();

                }
                else if (ddlbiayaanggota.SelectedValue == "0" && decimal.Parse(txtsimpok.Text) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Saldo  Simpok Kosong tidak boleh memiih type biaya keanggotaan dengan sudah menjadi anggota');", true);
                    btnSimpan.Enabled = true;
                    ddlrekening.Focus();

                }
                else if (ddljangka.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jangka Belum di dipilih');", true);
                    btnSimpan.Enabled = true;
                    ddljangka.Focus();
                }
                else if (txtketeranganJaminan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Agunan Masih Kosong');", true);
                    btnSimpan.Enabled = true;
                    txtketeranganJaminan.Focus();
                }
                else if (taksas <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Taksasi Agunan Masih Kosong');", true);
                    btnSimpan.Enabled = true;
                    txttaksasi.Focus();
                }
                else if (txttglrealisasi.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi Belum di dipilih');", true);
                    btnSimpan.Enabled = true;
                    txttglrealisasi.Focus();
                }
                else if (ddljaminan.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Surat Belum di dipilih');", true);
                    btnSimpan.Enabled = true;
                    ddljaminan.Focus();
                }
                else if (txtktppenjamin.Text.Length < 16)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Ktp penjamin tidak boleh kurang dari 16');", true);
                    btnSimpan.Enabled = true;
                    txtktppenjamin.Focus();
                }
                else if (txttgllahirpasangan.Text.Trim() == "" && ddlstatuskawinupload.SelectedValue == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Lahir Pasangan belum di isi');", true);
                    btnSimpan.Enabled = true;
                    txttgllahirpasangan.Focus();
                }
                else
                {
                    //DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];
                    //varx[0] = gb.CreateSPParameter("@ktp_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtktppenjamin.Text, typeof(string));
                    //varx[1] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    //varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPENJAMIN", typeof(string));
                    //varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varx, "constrmf");

                    //if (ge.ListDataset.Tables[0].DefaultView.Count > 0 )
                    //{


                    //    string nasabah = ge.ListDataset.Tables[0].Rows[0]["nasabah"].ToString();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Penjamin masih terdapat di Anggota yg belum lunas, No. Anggota =  " + nasabah + ", Mohon Lunasi terlebih dahulu');", true);
                    //    txtktppenjamin.Focus();
                    //}
                    //else
                    //{



                    string siklus = "";
                    siklus = "2";

                    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                    var4[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                    var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAAN", typeof(string));
                    var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
                    ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                    {
                        if (ge.ListDataset.Tables[0].Rows[0]["flagsalah"].ToString() == "1")
                        {


                            HDStatusDebitur.Value = "L";



                        }
                        else
                        {


                            if (ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString() == "0")
                            {

                                HDStatusDebitur.Value = "B";

                            }
                            else
                            {
                                HDStatusDebitur.Value = "L";
                            }

                        }
                    }
                    else
                    {
                        HDStatusDebitur.Value = "B";
                    }



                    nonasabah = txtnoanggota.Text;

                    //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    //var[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
                    //var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNONASABAH", typeof(string));
                    //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var, "constrmf");


                    ////string[] splitdata1 = txtAgama.Text.Split('-');

                    ////decimal angs_bulanan = txtangsuranperbulan.Text;




                    DateTime tgllahirx = Convert.ToDateTime(txttgllahirktp.Text);
                    DateTime tgldrop = Convert.ToDateTime(txttglrealisasi.Text);
                    if (tgllahirx >= tgldrop)
                    {
                        usia = "0";
                    }
                    else
                    {
                        usia = gb.HitungTahunUsia(txttgllahirktp.Text);
                    }


                    string datetgldrop = tgldrop.ToString("yyyy/MM/dd");

                    DateTime tgljt = Convert.ToDateTime(txtjttempo.Text);
                    string datetgljt = tgljt.ToString("yyyy/MM/dd");



                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[77];
                    var1[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                    var1[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
                    var1[2] = gb.CreateSPParameter("@nama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
                    var1[3] = gb.CreateSPParameter("@no_registrasi", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
                    var1[4] = gb.CreateSPParameter("@pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 100, ddlPekerjaan.SelectedValue, typeof(string));
                    var1[5] = gb.CreateSPParameter("@angsuran_bulanan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtangsuranperbulan.Text, typeof(decimal));
                    var1[6] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal));
                    var1[7] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljangka.SelectedValue, typeof(string));
                    var1[8] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.VarChar, 10, txtbunga.Text, typeof(string));
                    var1[9] = gb.CreateSPParameter("@kdpersh1", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlsumberdana.SelectedValue, typeof(string));
                    var1[10] = gb.CreateSPParameter("@biaya_provisi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnominalprovisi.Text, typeof(decimal));
                    var1[11] = gb.CreateSPParameter("@persen_prov", ParameterDirection.Input, SqlDbType.VarChar, 10, txtProvisi.Text, typeof(string));
                    var1[12] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbymaterai.Text, typeof(decimal));
                    var1[13] = gb.CreateSPParameter("@base_rate", ParameterDirection.Input, SqlDbType.VarChar, 10, txtbunga.Text, typeof(string));
                    var1[14] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprodukpengajuan.SelectedValue, typeof(string));
                    var1[15] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 10, HDJenisPinjaman.Value, typeof(string));
                    var1[16] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
                    var1[17] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.VarChar, 10, usia, typeof(decimal));
                    var1[18] = gb.CreateSPParameter("@tgl_drop", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, datetgldrop, typeof(DateTime));
                    var1[19] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, datetgljt, typeof(DateTime));
                    var1[20] = gb.CreateSPParameter("@filepathjaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
                    var1[21] = gb.CreateSPParameter("@jaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtketeranganJaminan.Text, typeof(string));
                    var1[22] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[23] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    var1[24] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    var1[25] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                    var1[26] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[27] = gb.CreateSPParameter("@ktp_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 30, txtktppenjamin.Text, typeof(string));
                    var1[28] = gb.CreateSPParameter("@nama_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamapenjamin.Text, typeof(string));
                    var1[29] = gb.CreateSPParameter("@file_pathktppasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
                    var1[30] = gb.CreateSPParameter("@nilai_taksasi", ParameterDirection.Input, SqlDbType.Decimal, 0, txttaksasi.Text, typeof(decimal));
                    var1[31] = gb.CreateSPParameter("@jaminan_kondisi", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkondisiJaminan.Text, typeof(string));
                    var1[32] = gb.CreateSPParameter("@biaya_cadangan", ParameterDirection.Input, SqlDbType.Decimal, 0, TextBox3.Text, typeof(decimal));
                    var1[33] = gb.CreateSPParameter("@status_dalam_keluarga", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatuskawinupload.SelectedValue, typeof(string));
                    var1[34] = gb.CreateSPParameter("@Siklus", ParameterDirection.Input, SqlDbType.VarChar, 10, siklus, typeof(string));
                    var1[35] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamaibukandung.Text, typeof(string));
                    var1[36] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNPWP.Text, typeof(string));
                    var1[37] = gb.CreateSPParameter("@no_ktp_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpasangan.Text, typeof(string));
                    var1[38] = gb.CreateSPParameter("@nama_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamapasangan.Text, typeof(string));
                    var1[39] = gb.CreateSPParameter("@tempat_lahir_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txttempatlahirpasangan.Text, typeof(string));
                    var1[40] = gb.CreateSPParameter("@tanggal_lahir_pasangan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahirpasangan.Text, typeof(DateTime));
                    var1[41] = gb.CreateSPParameter("@no_kk", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnomerkk.Text, typeof(string));
                    var1[42] = gb.CreateSPParameter("@file_pathKK", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
                    var1[43] = gb.CreateSPParameter("@file_pathnpwp", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
                    var1[44] = gb.CreateSPParameter("@jumlah_anak", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjumlahanak.Text, typeof(string));
                    var1[45] = gb.CreateSPParameter("@status_rumah", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusrumah.SelectedValue, typeof(string));
                    var1[46] = gb.CreateSPParameter("@alamat_rumah_tinggal1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatrumahtinggal1.Text, typeof(string));
                    var1[47] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 50, txtrttp.Text, typeof(string));
                    var1[48] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 50, txtrwtpt.Text, typeof(string));
                    var1[49] = gb.CreateSPParameter("@provinsiTempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprovinsitpt.SelectedValue, typeof(string));
                    var1[50] = gb.CreateSPParameter("@kodeposTempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodepostpt.Text, typeof(string));
                    var1[51] = gb.CreateSPParameter("@kelurahan_TempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkelurahantpt.Text, typeof(string));
                    var1[52] = gb.CreateSPParameter("@kecamatan_TempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkecamatantpt.Text, typeof(string));
                    var1[53] = gb.CreateSPParameter("@majuhari", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlmajuhari.SelectedValue), typeof(int));
                    var1[54] = gb.CreateSPParameter("@Jaminansurat", ParameterDirection.Input, SqlDbType.VarChar, 80, ddljaminan.SelectedValue, typeof(string));
                    var1[55] = gb.CreateSPParameter("@status_debitur", ParameterDirection.Input, SqlDbType.VarChar, 1, HDStatusDebitur.Value, typeof(string));
                    var1[56] = gb.CreateSPParameter("@biayaanggota", ParameterDirection.Input, SqlDbType.Decimal, 0, LblAnggota.Text, typeof(decimal));
                    var1[57] = gb.CreateSPParameter("@qtyuangmuka", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtkaliuangmuka.Text), typeof(int));
                    var1[58] = gb.CreateSPParameter("@biayauangmuka", ParameterDirection.Input, SqlDbType.Decimal, 0, txtuangmuka.Text, typeof(decimal));
                    var1[59] = gb.CreateSPParameter("@cadangansebelumnya", ParameterDirection.Input, SqlDbType.Decimal, 0, txtcadsebelumnya.Text, typeof(decimal));
                    var1[60] = gb.CreateSPParameter("@norekeningperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrekening.SelectedValue, typeof(string));
                    var1[61] = gb.CreateSPParameter("@tipeperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
                    var1[62] = gb.CreateSPParameter("@cadangankembali", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkembalicadangan.Text, typeof(decimal));
                    var1[63] = gb.CreateSPParameter("@qtycadangan", ParameterDirection.Input, SqlDbType.Int, 0, txtkalicadangan.Text, typeof(int));
                    var1[64] = gb.CreateSPParameter("@pilihcadangan", ParameterDirection.Input, SqlDbType.Int, 0, ddlbycad.SelectedValue, typeof(int));
                    var1[65] = gb.CreateSPParameter("@pilihuangmuka", ParameterDirection.Input, SqlDbType.Int, 0, ddluangmuka.SelectedValue, typeof(int));
                    var1[66] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 30, HDRegister.Value, typeof(string));
                    var1[67] = gb.CreateSPParameter("@infomaterai", ParameterDirection.Input, SqlDbType.VarChar, 80, txtinfox.Text, typeof(string));
                    var1[68] = gb.CreateSPParameter("@biayasimpananwajib", ParameterDirection.Input, SqlDbType.Decimal, 0, lblsimpananwajib.Text, typeof(decimal));
                    var1[69] = gb.CreateSPParameter("@jenistagihan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenistagihan.SelectedValue, typeof(string));
                    var1[70] = gb.CreateSPParameter("@Typebiayakeanggotaan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlbiayaanggota.SelectedValue, typeof(string));
                    var1[71] = gb.CreateSPParameter("@Rekeninglamacadangan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtcadanganRekening.Text, typeof(string));
                    var1[72] = gb.CreateSPParameter("@tgltagihhari", ParameterDirection.Input, SqlDbType.VarChar, 40, txttgltagih.Text, typeof(string));
                    var1[73] = gb.CreateSPParameter("@discountangsuran", ParameterDirection.Input, SqlDbType.Decimal, 0, txtdiscountangsuran.Text, typeof(decimal));
                    var1[74] = gb.CreateSPParameter("@pekerjaan_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlPekerjaan.SelectedValue, typeof(string));
                    var1[75] = gb.CreateSPParameter("@sektorusaha_Penjamin", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlsektorusahapenjamin.SelectedValue, typeof(string));
                    var1[76] = gb.CreateSPParameter("@jenisusaha_Penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenisusahapenjamin.Text, typeof(string));
         
                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var1, "constrmf");

                  



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        //btnSimpan.Visible = false;
                        //btnSave.Visible = false;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                        //txtnonasabah.Focus();
                        pnladd.Visible = false;
                        pnlIdentitas.Visible = false;
                        PnlFront.Visible = true;
                        pnlFilter.Visible = true;
                        pnlJaminan.Visible = false;
                        pnlPengajuan.Visible = false;
                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        btnlanjutkan.Visible = false;
                        btnTolakAntrian.Visible = false;
                        Pnlupdatedata.Visible = false;
                        pnlAlasanTolak.Visible = false;
                        txtnonasabah.Text = "";
                        txtketeranganJaminan.Text = "";
                        txtkondisiJaminan.Text = "-";
                        txttaksasi.Text = "0";
                        txtktppenjamin.Text = "0";
                        txtnamapenjamin.Text = "-";
                        txtnamaibukandung.Text = "-";
                        txtNPWP.Text = "0";
                        txtnomerkk.Text = "0";
                        txtcadsebelumnya.Text = "0";
                        txtkembalicadangan.Text = "0";
                        txtuangmuka.Text = "0";
                        txtkaliuangmuka.Text = "0";
                        TextBox2.Text = "0";
                        txtcadanganRekening.Text = "";
                        txtjenisusahapenjamin.Text = "";
                        ddljangka.SelectedValue = "-";
                        ddlstatus_SelectedIndexChanged(sender, e);
                        ddlpengajuan.SelectedValue = "B";
                        ddljenispermohonan_SelectedIndexChanged(sender, e);
                        ddlrekening.SelectedValue = "-";
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
            
            //}
        }
           

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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

        
    protected void ddljangka_SelectedIndexChanged(object sender, EventArgs e)

    {
        try
        {

            if (ddljaminan.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('hayo gabisa diklik ya? cek dulu Jaminan belum diisi tuh');", true);
                ddljaminan.Focus();
            }
            else
            {
                if (txttglrealisasi.Text != "")
                {


                    if (ddljangka.SelectedValue != "-")
                    {


                        Decimal nominal_provisi = 0;
                        Decimal plafon = 0;
                        int i = 0;
                        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                        var1[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnonasabah.Text, typeof(string));
                        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var1, "constrmf");

                        ddlprogrampinjaman.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {
                            HDStatusDebitur.Value = "B";
                        }
                        else
                        {
                            HDStatusDebitur.Value = "-";
                        }

                        string kode_wilayah = "";

                        if (SessKdpersh == "01")
                        {
                            kode_wilayah = "I";
                        }
                        else if (SessKdpersh == "02")
                        {
                            kode_wilayah = "II";
                        }
                        else if (SessKdpersh == "03")
                        {
                            kode_wilayah = "III";
                        }
                        else if (SessKdpersh == "04")
                        {
                            kode_wilayah = "IV";
                        }
                        else if (SessKdpersh == "05")
                        {
                            kode_wilayah = "V";
                        }

                        DefaultClass.Global.SqlHelper[] varm3 = new DefaultClass.Global.SqlHelper[5];
                        varm3[0] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
                        varm3[1] = gb.CreateSPParameter("@jaminan", ParameterDirection.Input, SqlDbType.VarChar, 100, ddljaminan.SelectedValue, typeof(string));
                        varm3[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varm3[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSUKUBUNGA", typeof(string));
                        varm3[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("GetmateraiProdukMikro", varm3, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tidak ada sumber dana memenuhi kriteria pinjaman!');", true);
                            txtbymaterai.Text = "0";
                            txtinfox.Text = "";
                            ddljangka.SelectedValue = "-";
                        }
                        else
                        {
                            txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["totalm"].ToString()));
                            txtinfox.Text = ge.ListDataset.Tables[0].Rows[0]["detail"].ToString();
                        }




                        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[6];
                        var2[0] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
                        var2[1] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var2[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSUKUBUNGA", typeof(string));
                        var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var2[5] = gb.CreateSPParameter("@jenistagihan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenistagihan.SelectedValue, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("GetBungaProdukMikro", var2, "constrmf");

                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0 || ddljaminan.SelectedValue == "-")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tidak ada sumber dana memenuhi kriteria pinjaman!');", true);
                            //txtjangka.Text = "";
                            //txtjangka.Focus();
                            ddljangka.SelectedValue = "-";
                            ddlprodukpengajuan.Focus();
                        }
                        else
                        {
                            txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaproduk"].ToString()));
                            txtProvisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["psnprovisi"].ToString()));



                            nominal_provisi = decimal.Parse(txtplafon.Text) * (decimal.Parse(txtProvisi.Text) / 100);
                            string hitbunga = ge.ListDataset.Tables[0].Rows[0]["hitbunga"].ToString();
                            decimal pokok = 0;
                            decimal bunga = 0;
                            txtnominalprovisi.Text = string.Format("{0:N2}", nominal_provisi);
                            plafon = decimal.Parse(txtplafon.Text);
                            pokok = plafon / int.Parse(ddljangka.SelectedValue);

                            if (ddlprodukpengajuan.SelectedValue == "3C")
                            {


                                if (ddljenistagihan.SelectedValue == "3")

                                {

                                    bunga = decimal.Parse("2") / decimal.Parse("100");
                                    txtbunga.Text = string.Format("{0:N2}", bunga);
                                    bunga = decimal.Parse(txtplafon.Text) * (bunga) / decimal.Parse("4");


                                }

                                else
                                {

                                    bunga = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaproduk"].ToString());
                                    bunga = ((bunga / 100) * int.Parse(ddljangka.SelectedValue)) * pokok;



                                }

                                if (decimal.Parse(txtplafon.Text) >= 5500000)
                                {
                                    txtangsuranperbulan.Text = string.Format("{0:N0}", (pokok + bunga) - 1);
                                }
                                else
                                {
                                    txtangsuranperbulan.Text = string.Format("{0:N0}", pokok + bunga);
                                }
                            }




                            else
                            {

                                bunga = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaproduk"].ToString());
                                bunga = ((bunga / 100) * int.Parse(ddljangka.SelectedValue)) * pokok;

                                txtangsuranperbulan.Text = string.Format("{0:N0}", pokok + bunga);
                            }

                            txtangsuranperbulan.Text = string.Format("{0:N0}", decimal.Parse(txtangsuranperbulan.Text) - decimal.Parse(txtdiscountangsuran.Text));


                            if (hitbunga == "M")
                            {
                                //for (i = 0; i < int.Parse(ddljangka.SelectedValue); i++)
                                //{
                                //    if (i == 0)
                                //    {
                                //        tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
                                //        tgl_libur = tgl_realisasi.AddDays(7);
                                //    }
                                //    else
                                //    {

                                //}


                                tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
                                tgl_libur = tgl_realisasi.AddDays(7 * int.Parse(ddljangka.SelectedValue));

                                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                                var3[0] = gb.CreateSPParameter("@tglLibur", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tgl_libur, typeof(string));
                                var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                                var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                ge.ListDataset = gb.GetDataJustOutPut("GetTglLiburBulanan", var3, "constrmf");



                                tgl_libur = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_libur"].ToString());

                                tgldrop = tgl_libur;

                                //    txtjttempo.Text = tgl_libur.ToString("dd-MMM-yyyy");



                                //    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                                //{
                                //    tgldrop = tgl_libur;
                                //}
                                //else
                                //{
                                //    tgldrop = tgl_libur.AddDays(7);
                                //}

                                //}

                                txtjttempo.Text = tgldrop.ToString("dd-MMM-yyyy");


                            }
                            else if (hitbunga == "B")
                            {
                                tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
                                tgl_libur = tgl_realisasi.AddMonths(int.Parse(ddljangka.SelectedValue));


                                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                                var3[0] = gb.CreateSPParameter("@tglLibur", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tgl_libur, typeof(string));
                                var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                                var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                ge.ListDataset = gb.GetDataJustOutPut("GetTglLiburBulanan", var3, "constrmf");


                                tgl_libur = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_libur"].ToString());

                                txtjttempo.Text = tgl_libur.ToString("dd-MMM-yyyy");

                            }
                        }

                        Decimal biayacadangan = 0, totbycad = 0;

                        if (ddlbycad.SelectedValue == "1")
                        {
                            txtkalicadangan.Text = "1";
                            txtkalicadangan.Enabled = true;
                            txtkalicadangan_TextChanged(sender, e);

                        }
                        else if (ddlbycad.SelectedValue == "0")
                        {
                            txtkalicadangan.Enabled = false;
                            TextBox3.Text = "0";
                            txtkalicadangan.Text = "0";

                        }
                        else if (ddlbycad.SelectedValue == "2")
                        {
                            txtkalicadangan.Text = "1";
                            txtkalicadangan.Enabled = true;
                            txtkalicadangan_TextChanged(sender, e);

                        }



                        //        ddlbycad.SelectedValue = "Dengan Biaya Cadangan";
                        //txtkalicadangan.Text="1";
                        biayacadangan = decimal.Parse(txtkalicadangan.Text) * decimal.Parse(txtangsuranperbulan.Text);
                        //TextBox2.Text = gb.ToRupiah(biayacadangan.ToString());
                        TextBox2.Text = string.Format("{0:N2}", biayacadangan);

                        if (ddlbycad.SelectedValue == "2")
                        {
                            totbycad = biayacadangan;
                        }
                        else
                        {
                            totbycad = biayacadangan - decimal.Parse(txtcadsebelumnya.Text);
                        }

                        if (totbycad < 0)
                        {
                            txtkembalicadangan.Text = string.Format("{0:N2}", Math.Abs(totbycad));
                            totbycad = 0;

                        }
                        else
                        {

                            txtkembalicadangan.Text = string.Format("{0:N2}", 0);


                        }





                        // TextBox3.Text = gb.ToRupiah(totbycad.ToString());
                        TextBox3.Text = string.Format("{0:N2}", totbycad);

                        ddluangmuka.SelectedValue = "0";
                        txtkaliuangmuka.Text = "0";
                        txtpokokuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));
                        txtuangmuka.Text = "0";
                        txtkaliuangmuka.Enabled = false;

                    }



                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi masih kosong!');", true);
                    txttglrealisasi.Focus();
                }
            }
        }
 
           

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void txtkalicadangan_TextChanged(object sender, EventArgs e)
    {
        Decimal biayacadangan = 0, totbycad = 0;
        //ddlbycad.SelectedValue = "Dengan Biaya Cadangan";
        //txtkalicadangan.Text = "1";
        biayacadangan = decimal.Parse(txtkalicadangan.Text) * decimal.Parse(txtangsuranperbulan.Text);
        //TextBox2.Text = gb.ToRupiah(biayacadangan.ToString());
        TextBox2.Text = string.Format("{0:N2}", biayacadangan);
        if (ddlbycad.SelectedValue=="2")
        {
            totbycad = biayacadangan ;
        }
        else
        {

            totbycad = biayacadangan - decimal.Parse(txtcadsebelumnya.Text);
        }

       
        // TextBox3.Text = gb.ToRupiah(totbycad.ToString());
        if (totbycad < 0)
        {
            txtkembalicadangan.Text = string.Format("{0:N2}", decimal.Parse(txtcadsebelumnya.Text) - biayacadangan); 
            totbycad = 0;
            TextBox3.Text = string.Format("{0:N2}", totbycad);

        }
        else
        {
            txtkembalicadangan.Text = string.Format("{0:N2}", 0);
            TextBox3.Text = string.Format("{0:N2}", totbycad);
        }
        

    }


    protected void txtdiscountangsuran_TextChanged(object sender, EventArgs e)
    {
        ddljangka_SelectedIndexChanged(sender,e);
    }

    protected void ddlprodukpengajuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_PROGRAMPINJAMAN", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        ddlprogrampinjaman.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprogrampinjaman.DataTextField = "keterangan";
        ddlprogrampinjaman.DataValueField = "kode";
        ddlprogrampinjaman.DataBind();
        //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));


        HDJenisPinjaman.Value = ge.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString();



        DefaultClass.Global.SqlHelper[] var1x = new DefaultClass.Global.SqlHelper[3];
        var1x[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var1x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Jangka", var1x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

        ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangka.DataTextField = "jangka";
        ddljangka.DataValueField = "jangka";
        ddljangka.DataBind();

        ddljangka.Items.Insert(0, new ListItem("Pilih Jangka", "-"));


        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[3];
        var2x[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_listJaminan", var2x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

        ddljaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljaminan.DataTextField = "namajaminan";
        ddljaminan.DataValueField = "namajaminan";
        ddljaminan.DataBind();

        ddljaminan.Items.Insert(0, new ListItem("Pilih Jaminan", "-"));


        if (ddlprodukpengajuan.SelectedValue == "3C")
        {
            ddljenistagihan.Enabled = true;
        }
        else
        {
            ddljenistagihan.SelectedValue = "0";
            ddljenistagihan.Enabled = false;
        }

    }

    protected void ddlprogrampinjaman_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
        var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, HDJenisPinjaman.Value, typeof(string));
        var1[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1[3] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GetSumberDanaCabangBank", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kodebank asc";

        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "kode_sumberdana";
        ddlsumberdana.DataValueField = "kodebank";
        ddlsumberdana.DataBind();
        //ddlsumberdana.Items.Insert(0, new ListItem("Pilih Data", "-"));


    }

    protected void txtbiayalain_TextChanged(object sender, EventArgs e)
    {
        decimal labakotor = 0, totalbiaya = 0, lababersih = 0;
        labakotor = decimal.Parse(txtpendapatanusaha.Text) - decimal.Parse(txthargapokok.Text);
        totalbiaya = decimal.Parse(txtbiayapegawai.Text) + decimal.Parse(txtbiayalain.Text);
        lababersih = labakotor - totalbiaya;

        txtlabakotor.Text = string.Format("{0:N0}", labakotor); //gb.ToRupiah(labakotor.ToString());
                                                                //txtlabakotor.Text = txtlabakotor.Text.Replace("Rp", "");

        txttotalbiaya.Text = string.Format("{0:N0}", totalbiaya);//gb.ToRupiah(totalbiaya.ToString());
      //  txttotalbiaya.Text = txttotalbiaya.Text.Replace("Rp", "");
         
        txtlababersihsebelumpajak.Text = string.Format("{0:N0}", lababersih);  //gb.ToRupiah(lababersih.ToString());
        //txtlababersihsebelumpajak.Text = txtlababersihsebelumpajak.Text.Replace("Rp", "");
        txtpajakpenghasilan.Focus();
    }

    protected void txtbiayapegawai_TextChanged(object sender, EventArgs e)
    {
        decimal labakotor = 0, totalbiaya = 0, lababersih = 0;
        labakotor = decimal.Parse(txtpendapatanusaha.Text) - decimal.Parse(txthargapokok.Text);
        totalbiaya = decimal.Parse(txtbiayapegawai.Text) + decimal.Parse(txtbiayalain.Text);
        lababersih = labakotor - totalbiaya;

        txtlabakotor.Text = string.Format("{0:N0}", labakotor); //gb.ToRupiah(labakotor.ToString());
       // txtlabakotor.Text = txtlabakotor.Text.Replace("Rp", "");

        txttotalbiaya.Text = string.Format("{0:N0}", totalbiaya); //gb.ToRupiah(totalbiaya.ToString());
      //  txttotalbiaya.Text = txttotalbiaya.Text.Replace("Rp", "");

        txtlababersihsebelumpajak.Text = string.Format("{0:N0}", lababersih); //gb.ToRupiah(lababersih.ToString());
        txtbiayalain.Focus();
    }

    protected void txtpendapatanusaha_TextChanged(object sender, EventArgs e)
    {
        decimal labakotor = 0, totalbiaya = 0, lababersih = 0;
        labakotor = decimal.Parse(txtpendapatanusaha.Text) - decimal.Parse(txthargapokok.Text);
        totalbiaya = decimal.Parse(txtbiayapegawai.Text) + decimal.Parse(txtbiayalain.Text);
        lababersih = labakotor - totalbiaya;

        txtlabakotor.Text = string.Format("{0:N0}", labakotor); //gb.ToRupiah(labakotor.ToString());
       // txtlabakotor.Text = txtlabakotor.Text.Replace("Rp", "");

        txttotalbiaya.Text = string.Format("{0:N0}", totalbiaya); //;

        txtlababersihsebelumpajak.Text = string.Format("{0:N0}", lababersih);
        txthargapokok.Focus();
    }

    protected void txthargapokok_TextChanged(object sender, EventArgs e)
    {
        decimal labakotor = 0, totalbiaya = 0, lababersih = 0;
        labakotor = decimal.Parse(txtpendapatanusaha.Text) - decimal.Parse(txthargapokok.Text);
        totalbiaya = decimal.Parse(txtbiayapegawai.Text) + decimal.Parse(txtbiayalain.Text);
        lababersih = labakotor - totalbiaya;

        txtlabakotor.Text = string.Format("{0:N0}", labakotor); //gb.ToRupiah(labakotor.ToString());
                                                                // txtlabakotor.Text = txtlabakotor.Text.Replace("Rp", "");

        txttotalbiaya.Text = string.Format("{0:N0}", totalbiaya); //;

        txtlababersihsebelumpajak.Text = string.Format("{0:N0}", lababersih);
        //txthargapokok.Focus();
    }

    protected void txtpendapatanselain_TextChanged(object sender, EventArgs e)
    {
        decimal pendapatan = 0;
        pendapatan = decimal.Parse(txtpendapatanbersih.Text) + decimal.Parse(txtpendapatanselain.Text);

        txttotalpendapatan.Text = string.Format("{0:N0}", pendapatan); //gb.ToRupiah(pendapatan.ToString());
        //txttotalpendapatan.Text = txttotalpendapatan.Text.Replace("Rp", "");

        //HDNoNasabah.Value = pendapatan.ToString();
        txtpengeluaranrumahtangga.Focus();
    }

    protected void txtpendapatanbersih_TextChanged(object sender, EventArgs e)
    {
        decimal pendapatan = 0;
        pendapatan = decimal.Parse(txtpendapatanbersih.Text) + decimal.Parse(txtpendapatanselain.Text);

        txttotalpendapatan.Text = string.Format("{0:N0}", pendapatan);// gb.ToRupiah(pendapatan.ToString());
        //txttotalpendapatan.Text = txttotalpendapatan.Text.Replace("Rp", "");

        //HDNoNasabah.Value = pendapatan.ToString();
        txtpendapatanselain.Focus();
    }


    protected void txtpajakpenghasilan_TextChanged(object sender, EventArgs e)
    {
        decimal lababersih = 0;
        lababersih = decimal.Parse(txtlababersihsebelumpajak.Text) - decimal.Parse(txtpajakpenghasilan.Text);
      
        txtlababersihsetelahpajak.Text = string.Format("{0:N0}", lababersih);

    }
    protected void txtangsuran_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtpengeluaranrumahtangga.Text) + decimal.Parse(txttagihanlistrik.Text) + decimal.Parse(txtpinjamanlain.Text) + decimal.Parse(txtangsuran.Text);

        txttotal.Text = string.Format("{0:N0}", pengeluaran); //gb.ToRupiah(pengeluaran.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = decimal.Parse(txttotalpendapatan.Text) - pengeluaran;
        TextBox7.Text = string.Format("{0:N0}", grandtotal); //gb.ToRupiah(grandtotal.ToString());
        //TextBox7.Text = TextBox7.Text.Replace("Rp", "");
    }

    protected void txtpinjamanlain_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtpengeluaranrumahtangga.Text) + decimal.Parse(txttagihanlistrik.Text) + decimal.Parse(txtpinjamanlain.Text) + decimal.Parse(txtangsuran.Text);

        txttotal.Text = string.Format("{0:N0}", pengeluaran); //gb.ToRupiah(pengeluaran.ToString());
        //txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = decimal.Parse(txttotalpendapatan.Text) - pengeluaran;
        TextBox7.Text = string.Format("{0:N0}", grandtotal); //gb.ToRupiah(grandtotal.ToString());
       // TextBox7.Text = TextBox7.Text.Replace("Rp", "");
        txtangsuran.Focus();
    }

    protected void txttagihanlistrik_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtpengeluaranrumahtangga.Text) + decimal.Parse(txttagihanlistrik.Text) + decimal.Parse(txtpinjamanlain.Text) + decimal.Parse(txtangsuran.Text);

        txttotal.Text = string.Format("{0:N0}", pengeluaran); //gb.ToRupiah(pengeluaran.ToString());
                                                              //txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = decimal.Parse(txttotalpendapatan.Text) - pengeluaran;
        TextBox7.Text = string.Format("{0:N0}", grandtotal); //gb.ToRupiah(grandtotal.ToString());
        txtpinjamanlain.Focus();
    }

    protected void txtpengeluaranrumahtangga_TextChanged(object sender, EventArgs e)
    {
        decimal pengeluaran = 0, grandtotal = 0;
        pengeluaran = decimal.Parse(txtpengeluaranrumahtangga.Text) + decimal.Parse(txttagihanlistrik.Text) + decimal.Parse(txtpinjamanlain.Text) + decimal.Parse(txtangsuran.Text);

        txttotal.Text = string.Format("{0:N0}", pengeluaran); //gb.ToRupiah(pengeluaran.ToString());
                                                              //txttotal.Text = txttotal.Text.Replace("Rp", "");

        grandtotal = decimal.Parse(txttotalpendapatan.Text) - pengeluaran;
        TextBox7.Text = string.Format("{0:N0}", grandtotal); //gb.ToRupiah(grandtotal.ToString());
        txtpinjamanlain.Focus();

        txttagihanlistrik.Focus();
    }





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
            ddlpengajuan.Enabled = false;
            ddlrekening.Enabled = false;
            ddlbiayaanggota.Enabled = false;
            txtangsuranperbulan.Text = string.Format("{0:N0}", "0");
            txtplafon.Text = string.Format("{0:N0}", "750000");
            //ddlrekening.Items.Insert(0, new ListItem("Pilih Rekening ", "-"));
            ddlbiayaanggota.Enabled = false;
            txtcadanganRekening.Enabled = false;
            txttgltagih.Enabled = false;



            txtbymaterai.Enabled = false;
            ddlbiayaanggota.Enabled = false;
            lblsimpananwajib.Enabled = true;
            ddlmajuhari.Enabled = true;
            ddlAO.Enabled = true;
            ddluangmuka.Enabled = true;
            ddlbycad.Enabled = true;
            txtkalicadangan.Enabled = true;
            txttglrealisasi.Enabled = true;
            ddlprodukpengajuan.Enabled = true;
            ddljaminan.Enabled = true;
            txtketeranganJaminan.Enabled = true;
            ddljangka.Enabled = true;
            btntambah.Enabled = true;
            btnminus.Enabled = true;
            btnSimpan.Enabled = true;
            btncekcadangan.Enabled = true;
            ddljenistagihan.Enabled = true;
            txtplafon.Text = string.Format("{0:N0}", "750000");
            ddlrekening.Items.Clear();
            ddlrekening.Items.Insert(0, new ListItem("Pilih Rekening", "-"));
            if (SessBranchID == "000")
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

            txtdiscountangsuran.Enabled = true;
            btncekcadangan.Enabled = true;
            ddlPekerjaan.Enabled = true;
            ddlrekening.Enabled = true;
            ddlstatusrumah.Enabled = true;
            ddlstatuskawinupload.Enabled = true;
            txtktppenjamin.Enabled = true;
            txtnamapenjamin.Enabled = true;
            txtkondisiJaminan.Enabled = true;
            txttaksasi.Enabled = true;
            txtnamaibukandung.Enabled = true;
            txtNPWP.Enabled = true;
            txtnomerkk.Enabled = true;
            txtuangmuka.Enabled = false;
            txtkaliuangmuka.Enabled = true;
            ddlmajuhari_SelectedIndexChanged(sender, e);


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

                    flag_bayar=ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString();
                    if (flag_bayar == "0")
                    {
                        flagdata = 1;
                    }
                    if (flagdata > 0)
                    {


                        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
                        var2x[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                        var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
                        var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var2x[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2x, "constrmf");

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
                            varkop1[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                            varkop1[5] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
                            varkop1[6] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 30, KODEAO, typeof(string));


                             ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varkop1, "constrmf");
                            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                            {
                                pnladd.Visible = true;
                                pnlIdentitas.Visible = true;
                                PnlFront.Visible = false;
                                pnlFilter.Visible = false;
                                pnlJaminan.Visible = true;
                                pnlPengajuan.Visible = true;
                                btnSimpan.Visible = true;
                                BtnApproveAntrian.Visible = false;
                                BtnBatalkanmohon.Visible = false;
                                BtnApproveAntrian.Visible = false;
                                BtnBatalkanmohon.Visible = false;
                                btnlanjutkan.Visible = false;
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
                                    txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                                    txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
                                    ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
                                    txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                                    txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                                    ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                                    ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                                    txtjenisusahaKtp.Text= ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();


                                    ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                                    ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                                    txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

                                    ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
                                    txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
                                    txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
                                    txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
                                    txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

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
                                        txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

                                    }

                                    imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                                  //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

                                    txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();




                                    //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();

                                    txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();

                                    txtktppenjamin.Text = "0";
                                    txtnamapenjamin.Text = "-";
                                    txtpendapatanperbulan.Text = "0";
                                    txtjaminantambahan.Text = "-";
                                    txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                                    txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();

                                    if (ge.ListDataset.Tables[0].Rows[0]["flagsalah"].ToString() == "0")
                                    {
                                        if (ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString() == "0")
                                        {
                                            LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("20000"));
                                            ddlpengajuan.SelectedValue= "B";
                                            ddljenispermohonan_SelectedIndexChanged(sender, e);
                                            ddlbiayaanggota.SelectedValue = "1";
                                            //ddlbiayaanggota_SelectedIndexChanged(sender, e);
                                            ddlbiayaanggota.Enabled = false;
                                        }
                                        else
                                        {
                                            ddlbiayaanggota.SelectedValue = "0";
                                            LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("0"));
                                            ddlpengajuan.SelectedValue = "X";
                                            ddljenispermohonan_SelectedIndexChanged(sender, e);
                                            ddlrekening.SelectedValue = "-";
                                            ddlbiayaanggota.Enabled = false;
                                        }

                                    }
                                    else
                                    {
                                        ddlbiayaanggota.SelectedValue = "2";                                       
                                        LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("0"));
                                        ddlpengajuan.SelectedValue = "X";
                                        ddlpengajuan.Enabled = false;
                                        ddljenispermohonan_SelectedIndexChanged(sender, e);                                       
                                        ddlrekening.SelectedValue = "-";
                                        ddlbiayaanggota.Enabled = false;
                                    }





                                   

                                    txtkaliuangmuka.Text = "1";
                                    txtkalicadangan.Text = "1";
                                    DefaultClass.Global.SqlHelper[] varcad = new DefaultClass.Global.SqlHelper[3];
                                    varcad[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah.Trim(), typeof(string));
                                    varcad[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCADANGAN", typeof(string));
                                    varcad[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varcad, "constrmf");

                                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                                    {
                                        txtcadanganRekening.Text = "";
                                        txtcadsebelumnya.Text = "0";
                                    }
                                    else
                                    {


                                        txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                                        txtcadsebelumnya.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["sisacadangan"].ToString());
                                        txtcadsebelumnya.Text = txtcadsebelumnya.Text.Replace("Rp", "");

                                    }




                                      ddlstatusrumah_SelectedIndexChanged(sender, e);


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


    protected void btnSearch_Click(object sender, EventArgs e)
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
            pnlJaminan.Visible = true;
            pnlPengajuan.Visible = true;
            btnSimpan.Visible = true;
            btnSimpan.Enabled = true;
            BtnApproveAntrian.Visible = false;
            BtnBatalkanmohon.Visible = false;
            BtnApproveAntrian.Visible = false;
            BtnBatalkanmohon.Visible = false;
            btnlanjutkan.Visible = false;
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
                    txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

                }

                imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                              //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                //txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString(); // "?r=" + DateTime.Now.Ticks.ToString();

                txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                //txtpathKK.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();

                //Imagenpwp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                //ImageKtppasangan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                //Imagekk.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();

                //data pekerjaan



                ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();

                txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();

                txtktppenjamin.Text = "0";
                txtnamapenjamin.Text = "-";
                txtpendapatanperbulan.Text = "0";
                txtjaminantambahan.Text = "-";
                txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();

                if (ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString() == "0")
                {
                    LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("20000"));
                }
                else
                {
                    LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("0"));
                }


                HDKodePos.Value = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
                HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();

                txtkaliuangmuka.Text = "1";
                txtkalicadangan.Text = "1";
                DefaultClass.Global.SqlHelper[] varcad = new DefaultClass.Global.SqlHelper[3];
                varcad[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah.Trim(), typeof(string));
                varcad[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCADANGAN", typeof(string));
                varcad[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varcad, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {
                    txtcadsebelumnya.Text = "0";
                }
                else
                {

                    txtcadsebelumnya.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["sisacadangan"].ToString());
                    txtcadsebelumnya.Text = txtcadsebelumnya.Text.Replace("Rp", "");

                }


            }








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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLISTNASABAH", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.Int, 0, ddlstatus.SelectedValue, typeof(int));


        ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "Idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void btntarik_Click(object sender, EventArgs e)
    {
        try
        {
            string[] splitdata = txtnonasabah.Text.Split('-');
            string nonasabah = splitdata[0];
            string BAYARSTATUS = "0";
            string TUTUPSTATUS = "0";
            int banyakpinjaman = 0;


            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
    var3[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
    var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAANTP", typeof(string));
    var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3, "constrmf");
    if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
    {


                //BAYARSTATUS = "0";
                BAYARSTATUS = ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString();
                TUTUPSTATUS= ge.ListDataset.Tables[0].Rows[0]["flag_tutup"].ToString();
                banyakpinjaman = int.Parse(ge.ListDataset.Tables[0].Rows[0]["flagdata"].ToString()); 
                if (BAYARSTATUS == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('FULL UP Hanya bisa di gunakan untuk Anggota Full Up bukan Calon Anggota!!!');", true);
                    txtnonasabah.Focus();
                }
                else if (TUTUPSTATUS == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('FULL UP Hanya bisa di gunakan untuk Anggota Full Up bukan Anggota Penutupan!!!');", true);
                    txtnonasabah.Focus();
                }
                else if (banyakpinjaman> 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('FULL UP Hanya bisa di gunakan untuk Anggota Full Up yang tidak ada transaksi Pinjaman!!!');", true);
                    txtnonasabah.Focus();
                }
                else
                {

               


                DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
            var2x[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
            var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
            var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var2x[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2x, "constrmf");

            if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota untuk Permohonan tersebut sudah ada dan belum di proses');", true);

                txtnonasabah.Focus();
            }
            else
            {



                DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[6];
                var88[0] = gb.CreateSPParameter("@Nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                var88[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEFULLUP", typeof(string));
                var88[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var88[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var88[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var88[5] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_Nasabah_Update", var88, "constrmf");



                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota sudah di update menjadi Anggota Baru / Calon Anggota');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                    txtnonasabah.Text = "";
                    txtnonasabah.Focus();

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
            }

                }

            }
      else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tersebut Belum Menjadi Anggota Koperasi ! ');", true);
            txtnonasabah.Focus();
        }

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }


    protected void btntarikrlb_Click(object sender, EventArgs e)
    {
        try
        {
            string[] splitdata = txtnonasabah.Text.Split('-');
            string nonasabah = splitdata[0];
            string BAYARSTATUS = "0";


            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
            var3[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
            var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAAN", typeof(string));
            var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
            {


                //BAYARSTATUS = "0";
                BAYARSTATUS = ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString();

                if (BAYARSTATUS == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Roll Back FULL UP Hanya bisa di gunakan untuk Anggota FULL UP!!!');", true);
                    txtnonasabah.Focus();
                }
                else
                {




                    DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
                    var2x[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                    var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
                    var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var2x[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2x, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota untuk Permohonan tersebut sudah ada dan belum di proses');", true);

                        txtnonasabah.Focus();
                    }
                    else
                    {



                        DefaultClass.Global.SqlHelper[] var21x = new DefaultClass.Global.SqlHelper[3];
                        var21x[0] = gb.CreateSPParameter("@Nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                        var21x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKDATA", typeof(string));
                        var21x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("AR_Tarik_Pusat_RL_001", var21x, "constrmf");

                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota tersebut tidak pernah di FULL UP dalam permohonan');", true);

                            txtnonasabah.Focus();
                        }
                        else
                        {



                            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[3];
                            var88[0] = gb.CreateSPParameter("@Nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                            var88[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEFULLUP", typeof(string));
                            var88[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            ge.ListDataset = gb.GetDataJustOutPut("AR_Tarik_Pusat_RL_001", var88, "constrmf");

                            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota sudah di update');", true);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                                txtnonasabah.Text = "";
                                txtnonasabah.Focus();

                            }
                            else
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                            }



                        }



                    }

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tersebut Belum Menjadi Anggota Koperasi Atau Tidak Aktif/ sudah di Tutup! ');", true);
                txtnonasabah.Focus();
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
            if (txtidentitasx.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Id KTP Masih Kosong');", true);
                txtidentitasx.Focus();
            }
            else if (txtidentitasx.Text.Length < 16)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No KTP tidak boleh kurang dari 16 Digit');", true);
                txtidentitasx.Focus();
            }
            else
            {
                //DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[5];
                //var4[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, txtidentitasx.Text, typeof(string));
                //var4[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var4[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, lblanggotax.Text, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("GetValidateRegisterMikro", var4, "constrmf");



                //if (ge.ListDataset.Tables[0].Rows.Count > 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('NIK KTP sudah ada pada koperasi yg sama');", true);
                //    txtidentitasx.Focus();

                //}

                //else
                //{

                    //btnSimpanTolakan.Enabled = false;
                    DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[13];
                    var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
                    var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var88[2] = gb.CreateSPParameter("@jaminandetail", ParameterDirection.Input, SqlDbType.VarChar, 250, txtketeranganJaminanupdate.Text, typeof(string));
                    var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
                    var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, lblanggotax.Text, typeof(string));
                    var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEDATA", typeof(string));
                    var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    var88[9] = gb.CreateSPParameter("@notelpon", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnohpx.Text, typeof(string));
                    var88[10] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 100, txtemailx.Text, typeof(string));
                    var88[11] = gb.CreateSPParameter("@ktp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitasx.Text, typeof(string));
                    var88[12] = gb.CreateSPParameter("@catatan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtcatatan.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        // btnSimpan.Visible = false;
                        //btnSave.Visible = false;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                        pnladd.Visible = false;
                        pnlIdentitas.Visible = false;
                        PnlFront.Visible = true;
                        pnlFilter.Visible = true;
                        pnlJaminan.Visible = false;
                        pnlPengajuan.Visible = false;
                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        btnlanjutkan.Visible = false;
                        btnTolakAntrian.Visible = false;
                        Pnlupdatedata.Visible = false;
                        pnlAlasanTolak.Visible = false;
                        txtnonasabah.Text = "";
                        txtketeranganJaminan.Text = "";
                        txtcatatan.Text = "";
                        txtkondisiJaminan.Text = "-";
                        txttaksasi.Text = "0";
                        txtktppenjamin.Text = "0";
                        txtnamapenjamin.Text = "-";
                        txtnamaibukandung.Text = "-";
                        txtNPWP.Text = "0";
                        txtnomerkk.Text = "0";
                        txtcadsebelumnya.Text = "0";
                        txtkembalicadangan.Text = "0";
                        txtuangmuka.Text = "0";
                        txtkaliuangmuka.Text = "0";
                        TextBox2.Text = "0";
                        ddljangka.SelectedValue = "-";
                        ddlstatus_SelectedIndexChanged(sender, e);
                        txtnonasabah.Focus();

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }
                }
            //}
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }



    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

            case "UPDATEX":

                try

                {
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Update Data Tambahan Detail Jaminan,Nohp dan email Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        pnladd.Visible = false;
                        pnlIdentitas.Visible = false;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        pnlJaminan.Visible = false;
                        pnlPengajuan.Visible = false;
                        Pnlupdatedata.Visible = true;
                        string no_nasabah;
                        lblrekening.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;
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



                        txtidentitasx.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
                        txtnohpx.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
                        txtemailx.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                        lblanggotax.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                        lblnamaanggota.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                        lbljaminansuratx.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                        txtketeranganJaminanupdate.Text = ge.ListDataset.Tables[0].Rows[0]["jaminansuratdetail"].ToString();
                        txtcatatan.Text = ge.ListDataset.Tables[0].Rows[0]["catatan"].ToString();

                        if (txtidentitasx.Text.Length < 16)
                        {
                            txtidentitasx.Enabled = true;
                        }
                       else
                        {
                            txtidentitasx.Enabled = false;
                        }

                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

            case "SET":

                try

                {
                    if (ddlstatus.SelectedValue!="1" && ddlstatus.SelectedValue != "3")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Edit Data Permohonan Hanya dapat dilakukan di status Approval Pusat dan Approve Antrian');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                  
                    pnladd.Visible = true;
                    pnlIdentitas.Visible = true;
                    PnlFront.Visible = false;
                    pnlFilter.Visible = false;
                    pnlJaminan.Visible = true;
                    pnlPengajuan.Visible = true;
                    btnSimpan.Visible = true;
                        btnSimpan.Enabled = true;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        btnlanjutkan.Visible = false;
                        btnTolakAntrian.Visible = false;
                        //string register
                        HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    ddlbiayaanggota.Enabled = false;
                    txtcadanganRekening.Enabled = false;
                    txttgltagih.Enabled = false;
                        txtbymaterai.Enabled = false;


                        ddlpengajuan.Enabled = true;
                        ddlbiayaanggota.Enabled = false;
                        lblsimpananwajib.Enabled = true;
                        ddlmajuhari.Enabled = true;
                        ddlAO.Enabled = true;
                        ddluangmuka.Enabled = true;
                        ddlbycad.Enabled = true;
                        txtkalicadangan.Enabled = true;
                        txttglrealisasi.Enabled = true;
                        ddlprodukpengajuan.Enabled = true;
                        ddljaminan.Enabled = true;
                        txtketeranganJaminan.Enabled = true;
                        ddljangka.Enabled = true;
                        btntambah.Enabled = true;
                        btnminus.Enabled = true;
                        btncekcadangan.Enabled = true;
                        txtplafon.Text = string.Format("{0:N0}", "0");

                        //ddlrekening.Items.RemoveAt(0);
                        //ddlrekening.Items.RemoveAt(1);
                        //ddlrekening.Items.RemoveAt(2);
                        ddlrekening.Items.Clear();
                        //ddlrekening.Items.Insert(0, new ListItem("Pilih Rekening ", "-"));
                        txtdiscountangsuran.Enabled = true;
                        btncekcadangan.Enabled = true;
                        ddlPekerjaan.Enabled = true;
                        ddlrekening.Enabled = true;
                        ddlstatusrumah.Enabled = true;
                        ddlstatuskawinupload.Enabled = true;
                        txtktppenjamin.Enabled = true;
                        txtnamapenjamin.Enabled = true;
                        txtkondisiJaminan.Enabled = true;
                        txttaksasi.Enabled = true;
                        txtnamaibukandung.Enabled = true;
                        txtNPWP.Enabled = true;
                        txtnomerkk.Enabled = true;
                        btnSimpan.Enabled = true;

                        ddlPekerjaan.Enabled=true;
                        ddlsektorusahapenjamin.Enabled = true;
                        txtjenisusahapenjamin.Enabled = true;



                        if (SessBranchID == "000")
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


                        ddlmajuhari_SelectedIndexChanged(sender, e);

                      

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

                        //data ktp

                        ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                        ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                        txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

                        ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                        ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                        txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();




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
                    txtinfox.Text = ge.ListDataset.Tables[0].Rows[0]["infomaterai"].ToString();
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

                    ddlbiayaanggota.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Typebiayakeanggotaan"].ToString();
                    txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["Rekeninglamacadangan"].ToString();
                    txttgltagih.Text = ge.ListDataset.Tables[0].Rows[0]["tgltagihhari"].ToString();
                    txtdiscountangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["discountangsuran"].ToString()));





                        //imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();


                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                    txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                    txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                    txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                        //txtkat.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                        txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                    txtkondisiJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
                    txttaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                    ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                    txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                    txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                    txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                    txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                    txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                    txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                    txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                    ddlprovinsitpt.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();

                        ddljenistagihan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenistagihan"].ToString();

                        ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();



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



                    
                    //data tempat tinggal
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
                        txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

                    }

                    
                    ddlprodukpengajuan.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                        //ddlprodukpengajuan_SelectedIndexChanged(sender,e);

                        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[3];
                        var2x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var2x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var2x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_listJaminan", var2x, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

                        ddljaminan.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljaminan.DataTextField = "namajaminan";
                        ddljaminan.DataValueField = "namajaminan";
                        ddljaminan.DataBind();

                        ddljaminan.Items.Insert(0, new ListItem("Pilih Jaminan", "-"));
                        ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();


                        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                    var1[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                    var1[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    gex.ListDataset = gbx.GetDataJustOutPut("AR_PROGRAMPINJAMAN", var1, "constrmf");

                    gex.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

                    ddlprogrampinjaman.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlprogrampinjaman.DataTextField = "keterangan";
                    ddlprogrampinjaman.DataValueField = "kode";
                    ddlprogrampinjaman.DataBind();
                    //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    HDJenisPinjaman.Value = gex.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString();



                    DefaultClass.Global.SqlHelper[] var1x = new DefaultClass.Global.SqlHelper[3];
                    var1x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                    var1x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                    var1x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    gex.ListDataset = gbx.GetDataJustOutPut("AR_Jangka", var1x, "constrmf");

                    gex.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

                    ddljangka.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljangka.DataTextField = "jangka";
                    ddljangka.DataValueField = "jangka";
                    ddljangka.DataBind();

                    ddljangka.Items.Insert(0, new ListItem("Pilih Jangka", "-"));

                        txttglrealisasi.Text= string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString())); ;
                        txtjttempo.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                        txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
                        ddljangka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        ddlbycad.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["cadangan"].ToString();
                        txtkalicadangan.Text = ge.ListDataset.Tables[0].Rows[0]["qtycadangan"].ToString();
                        ddluangmuka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["uangmuka"].ToString();
                        txtkaliuangmuka.Text = ge.ListDataset.Tables[0].Rows[0]["qtyuangmuka"].ToString();
                        ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                        txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()));
                        txtProvisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString()));
                        txtnominalprovisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                        txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                        LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        txtcadsebelumnya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                        lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayasimpananwajib"].ToString()));
                        //ddlbycad_SelectedIndexChanged(sender, e);


                        txtkalicadangan_TextChanged(sender, e);
                        txtkaliuangmuka_TextChanged(sender, e);



                    if (ddlpengajuan.SelectedValue == "B")
                    {

                        ddlpengajuan.Enabled = false;
                        ddlrekening.Enabled = false;       
                            

                        ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
                       
                        
                        }
                    else if (ddlpengajuan.SelectedValue == "X")
                    {
                            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[6];
                            vart1[0] = gbx.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            vart1[1] = gbx.CreateSPParameter("@jns_permohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
                            vart1[2] = gbx.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                            vart1[3] = gbx.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            vart1[4] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAMINAN", typeof(string));
                            vart1[5] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                            gex.ListDataset = gbx.GetDataJustOutPut("GetRekeningPerpanjangan", vart1, "constrmf");
                            gex.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";
                            ddlrekening.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                            ddlrekening.DataTextField = "no_rekening";
                            ddlrekening.DataValueField = "no_rekening";
                            ddlrekening.DataBind();
                            ddlrekening.Enabled = true;
                            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
                            ddlrekening.Items.Insert(1, new ListItem("Rekening Lama Tidak Ada", "B"));

                            ddlrekening.Enabled = true;
                            ddlrekening.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();
                            ddlpengajuan.Enabled = false;
                    }

                        if (ddlbiayaanggota.SelectedValue == "0")
                        {
                            ddlbiayaanggota.Enabled = false;
                        }
                        else if (ddlbiayaanggota.SelectedValue == "1")
                        {
                            if (ddlpengajuan.SelectedValue == "B")
                            {
                                ddlbiayaanggota.Enabled = false;
                            }
                            else
                            {
                                ddlbiayaanggota.Enabled = false;
                            }
                           
                        }
                        else if (ddlbiayaanggota.SelectedValue == "2")
                        {
                            ddlbiayaanggota.Enabled = false;
                        }


                        if (ddlprodukpengajuan.SelectedValue == "3C")
                        {
                            ddljenistagihan.Enabled = true;
                        }
                        else
                        {
                            ddljenistagihan.SelectedValue = "0";
                            ddljenistagihan.Enabled = false;
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

                    if (ddlstatus.SelectedValue != "3")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Approve Antrian Hanya dapat dilakukan di status Approve Antrian');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = true;
                        BtnBatalkanmohon.Visible = true;
                        BtnApproveAntrian.Enabled = true;


                        btnlanjutkan.Visible = false;
                        btnTolakAntrian.Visible = false;


                        pnladd.Visible = true;
                        pnlIdentitas.Visible = true;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        pnlJaminan.Visible = true;
                        pnlPengajuan.Visible = true;

                        if (SessBranchID == "000")
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


                        //string register
                        HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                        HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                        var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                        var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                        var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                        var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");


                        ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                        ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                        txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();


                        ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                        ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                        txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();

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
                        lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayasimpananwajib"].ToString())); 
                        txtinfox.Text = ge.ListDataset.Tables[0].Rows[0]["infomaterai"].ToString();
                        txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                        txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                        ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                        ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                        ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                        txtdiscountangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["discountangsuran"].ToString()));
                        //data ktp

                        //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                        txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                        ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                        ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                        ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();

                        ddljenistagihan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenistagihan"].ToString();

                        //imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();


                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                        txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                        txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                        txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                        //ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                        txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                        txtkondisiJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
                        txttaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                        ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                        txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                        txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                        txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                        txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                        txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                        txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                        txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                        txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                        ddlprovinsitpt.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                        ddlbiayaanggota.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Typebiayakeanggotaan"].ToString();
                        txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["Rekeninglamacadangan"].ToString();
                        txttgltagih.Text = ge.ListDataset.Tables[0].Rows[0]["tgltagihhari"].ToString();
                  

                        ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();


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




                        //data tempat tinggal
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
                            txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

                        }


                        ddlprodukpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                        //ddlprodukpengajuan_SelectedIndexChanged(sender,e);

                        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[3];
                        var2x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var2x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var2x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_listJaminan", var2x, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

                        ddljaminan.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljaminan.DataTextField = "namajaminan";
                        ddljaminan.DataValueField = "namajaminan";
                        ddljaminan.DataBind();

                        ddljaminan.Items.Insert(0, new ListItem("Pilih Jaminan", "-"));
                        ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();


                        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                        var1[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var1[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var1[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_PROGRAMPINJAMAN", var1, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

                        ddlprogrampinjaman.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddlprogrampinjaman.DataTextField = "keterangan";
                        ddlprogrampinjaman.DataValueField = "kode";
                        ddlprogrampinjaman.DataBind();
                        //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));


                        HDJenisPinjaman.Value = gex.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString();



                        DefaultClass.Global.SqlHelper[] var1x = new DefaultClass.Global.SqlHelper[3];
                        var1x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var1x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var1x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_Jangka", var1x, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

                        ddljangka.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljangka.DataTextField = "jangka";
                        ddljangka.DataValueField = "jangka";
                        ddljangka.DataBind();

                        ddljangka.Items.Insert(0, new ListItem("Pilih Jangka", "-"));

                        txttglrealisasi.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString())); ;
                        txtjttempo.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                        txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
                        ddljangka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        ddlbycad.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["cadangan"].ToString();
                        txtkalicadangan.Text = ge.ListDataset.Tables[0].Rows[0]["qtycadangan"].ToString();
                        ddluangmuka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["uangmuka"].ToString();
                        txtkaliuangmuka.Text = ge.ListDataset.Tables[0].Rows[0]["qtyuangmuka"].ToString();
                        ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                        txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()));
                        txtProvisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString()));
                        txtnominalprovisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                        txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                        LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        txtcadsebelumnya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                        lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayasimpananwajib"].ToString()));

                        txtkalicadangan_TextChanged(sender, e);
                        //ddlbycad_SelectedIndexChanged(sender, e);
                        //ddluangmuka_SelectedIndexChanged(sender, e);
                        txtkaliuangmuka_TextChanged(sender, e);
                        ddlrekening.Items.Clear();
                        if (ddlpengajuan.SelectedValue == "B")
                        {


                            ddlrekening.Enabled = false;
                            //ddlrekening.Text = "-";
                            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
                            ddlpengajuan.Enabled = false;

                        }
                        else if (ddlpengajuan.SelectedValue == "X")
                        {
                            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[6];
                            vart1[0] = gbx.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            vart1[1] = gbx.CreateSPParameter("@jns_permohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
                            vart1[2] = gbx.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                            vart1[3] = gbx.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            vart1[4] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAMINAN", typeof(string));
                            vart1[5] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                            gex.ListDataset = gbx.GetDataJustOutPut("GetRekeningPerpanjangan", vart1, "constrmf");
                            gex.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";
                            ddlrekening.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                            ddlrekening.DataTextField = "no_rekening";
                            ddlrekening.DataValueField = "no_rekening";
                            ddlrekening.DataBind();
                            ddlrekening.Enabled = true;
                            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
                            ddlrekening.Items.Insert(1, new ListItem("Rekening Lama Tidak Ada", "B"));

                            ddlrekening.Enabled = true;
                            ddlrekening.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();
                            ddlpengajuan.Enabled = false;
                        }
                        if (ddlbiayaanggota.SelectedValue == "0")
                        {
                            ddlbiayaanggota.Enabled = false;
                        }
                        else if (ddlbiayaanggota.SelectedValue == "1")
                        {
                            if (ddlpengajuan.SelectedValue == "B")
                            {
                                ddlbiayaanggota.Enabled = false;
                            }
                            else
                            {
                                //ddlbiayaanggota.Enabled = true
                                     ddlbiayaanggota.Enabled = false;
                            }

                        }
                        else if (ddlbiayaanggota.SelectedValue == "2")
                        {
                            ddlbiayaanggota.Enabled = false;
                        }



                        ddlbiayaanggota.Enabled = false;
                        txtcadanganRekening.Enabled = false;
                        txttgltagih.Enabled = false;

                        ddlpengajuan.Enabled = false;
                        ddlbiayaanggota.Enabled = false;
                        lblsimpananwajib.Enabled = false;
                        ddlmajuhari.Enabled = false;
                        ddlAO.Enabled = false;
                        ddluangmuka.Enabled = false;
                        ddlbycad.Enabled = false;
                        txtkalicadangan.Enabled = false;
                        txttglrealisasi.Enabled = false;
                        ddlprodukpengajuan.Enabled = false;
                        ddljaminan.Enabled = false;
                        txtketeranganJaminan.Enabled = false;
                        ddljangka.Enabled = false;
                        btntambah.Enabled = false;
                        btnminus.Enabled = false;
                        txtdiscountangsuran.Enabled = false;
                        btncekcadangan.Enabled = false;
                        ddlPekerjaan.Enabled = false;
                        ddlrekening.Enabled = false;
                        ddlstatusrumah.Enabled = false;
                        ddlstatuskawinupload.Enabled = false;
                        txtktppenjamin.Enabled = false;
                        txtnamapenjamin.Enabled = false;
                        txtkondisiJaminan.Enabled = false;
                        txttaksasi.Enabled = false;
                        txtnamaibukandung.Enabled = false;
                        txtNPWP.Enabled = false;
                        txtnomerkk.Enabled = false;
                        txtkaliuangmuka.Enabled = false;
                        txtbymaterai.Enabled = false;
                        ddlPekerjaan.Enabled = false;
                        ddlsektorusahapenjamin.Enabled = false;
                        txtjenisusahapenjamin.Enabled = false;
                        ddljenistagihan.Enabled = false;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



                        //lbljeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                        //lblao.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        //lbltglRealisasi.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString()));
                        //lbljangkawaktu.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        //lbljthtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                        //lblbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                        //lblmajuhari.Text = ddlmajuhari.SelectedItem.Text;
                        //lblbesarpinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()));
                        //lblangsuranbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                        //lbladministrasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        //lblmaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                        //LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString()));
                        //LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        //lblTotalPotongbiaya.Text = string.Format("{0:N2}", decimal.Parse(lbladministrasi.Text) + decimal.Parse(lblmaterai.Text) + decimal.Parse(LblPotonganangsuran.Text) + decimal.Parse(LblAnggota.Text));
                        //lblygditerimaanggota.Text = string.Format("{0:N2}", decimal.Parse(lblbesarpinjaman.Text) - decimal.Parse(lblTotalPotongbiaya.Text));



                        // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                        // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "Lanjutkan":

                try

                {
                    if (SessBranchID == "000")
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
                    string identitas;
                    HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    HDNrekeningkreditbo.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;
                    DefaultClass.Global.SqlHelper[] varc = new DefaultClass.Global.SqlHelper[6];
                    varc[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                    varc[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                    varc[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                    varc[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    varc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                    varc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", varc, "constrmf");

                    identitas = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();

                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Lanjutkan Ke Teller Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else if (identitas.Length < 16){
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Id KTP Kurang dari 16 silahkan update data KTP');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        btnlanjutkan.Visible = true;
                        btnlanjutkan.Enabled = true;
                        btnTolakAntrian.Visible = false;

                        pnladd.Visible = true;
                        pnlIdentitas.Visible = true;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        pnlJaminan.Visible = true;
                        pnlPengajuan.Visible = true;

                        ddlbiayaanggota.Enabled = false;
                        txtcadanganRekening.Enabled = false;
                        txttgltagih.Enabled = false;

                        //string register
                        //HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        //HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                        //HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                        //HDNrekeningkreditbo.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                        var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                        var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                        var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                        var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

                        //data ktp

                        ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                        ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                        txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();


                        ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                        ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                        txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();


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
                        txtinfox.Text = ge.ListDataset.Tables[0].Rows[0]["infomaterai"].ToString();
                        txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                        txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                        ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                        ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                        ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                        txtdiscountangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["discountangsuran"].ToString()));
                        //data ktp
                        ddljenistagihan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenistagihan"].ToString();
                        //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                        txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                        ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                        ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                        ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();


                        //imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();


                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                        txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                        txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                        txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                        //ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                        txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                        txtkondisiJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
                        txttaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                        ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                        txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                        txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                        txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                        txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                        txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                        txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                        txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                        txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                        ddlprovinsitpt.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();

                        ddlbiayaanggota.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Typebiayakeanggotaan"].ToString();
                        txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["Rekeninglamacadangan"].ToString();
                        txttgltagih.Text = ge.ListDataset.Tables[0].Rows[0]["tgltagihhari"].ToString();

                      
                        ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();

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




                        //data tempat tinggal
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
                            txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

                        }


                        ddlprodukpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                        //ddlprodukpengajuan_SelectedIndexChanged(sender,e);
                        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[3];
                        var2x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var2x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var2x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_listJaminan", var2x, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

                        ddljaminan.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljaminan.DataTextField = "namajaminan";
                        ddljaminan.DataValueField = "namajaminan";
                        ddljaminan.DataBind();

                        ddljaminan.Items.Insert(0, new ListItem("Pilih Jaminan", "-"));
                        ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();

                        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                        var1[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var1[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var1[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_PROGRAMPINJAMAN", var1, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

                        ddlprogrampinjaman.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddlprogrampinjaman.DataTextField = "keterangan";
                        ddlprogrampinjaman.DataValueField = "kode";
                        ddlprogrampinjaman.DataBind();
                        //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));


                        HDJenisPinjaman.Value = gex.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString();



                        DefaultClass.Global.SqlHelper[] var1x = new DefaultClass.Global.SqlHelper[3];
                        var1x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var1x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var1x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_Jangka", var1x, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

                        ddljangka.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljangka.DataTextField = "jangka";
                        ddljangka.DataValueField = "jangka";
                        ddljangka.DataBind();

                        ddljangka.Items.Insert(0, new ListItem("Pilih Jangka", "-"));

                        txttglrealisasi.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString())); ;
                        txtjttempo.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                        txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
                        ddljangka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        ddlbycad.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["cadangan"].ToString();
                        txtkalicadangan.Text = ge.ListDataset.Tables[0].Rows[0]["qtycadangan"].ToString();
                        ddluangmuka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["uangmuka"].ToString();
                        txtkaliuangmuka.Text = ge.ListDataset.Tables[0].Rows[0]["qtyuangmuka"].ToString();
                        ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                        txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()));
                        txtProvisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString()));
                        txtnominalprovisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                        txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                        LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        txtcadsebelumnya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                        lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayasimpananwajib"].ToString()));
                        //ddlbycad_SelectedIndexChanged(sender, e);
                        txtkalicadangan_TextChanged(sender, e);
                        //ddluangmuka_SelectedIndexChanged(sender, e);
                        txtkaliuangmuka_TextChanged(sender, e);
                        ddlrekening.Items.Clear();
                        if (ddlpengajuan.SelectedValue == "B")
                        {


                            ddlrekening.Enabled = false;
                            //ddlrekening.Text = "-";
                            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));

                        }
                        else if (ddlpengajuan.SelectedValue == "X")
                        {
                            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[6];
                            vart1[0] = gbx.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            vart1[1] = gbx.CreateSPParameter("@jns_permohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
                            vart1[2] = gbx.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                            vart1[3] = gbx.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            vart1[4] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAMINAN", typeof(string));
                            vart1[5] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                            gex.ListDataset = gbx.GetDataJustOutPut("GetRekeningPerpanjangan", vart1, "constrmf");
                            gex.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";
                            ddlrekening.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                            ddlrekening.DataTextField = "no_rekening";
                            ddlrekening.DataValueField = "no_rekening";
                            ddlrekening.DataBind();
                            ddlrekening.Enabled = true;
                            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
                            ddlrekening.Items.Insert(1, new ListItem("Rekening Lama Tidak Ada", "B"));
                            ddlrekening.Enabled = true;
                            ddlrekening.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();
                        }

                        ddlbiayaanggota.Enabled = false;
                        txtcadanganRekening.Enabled = false;
                        txttgltagih.Enabled = false;

                        ddlpengajuan.Enabled = false;
                        ddlbiayaanggota.Enabled = false;
                        lblsimpananwajib.Enabled = false;
                        ddlmajuhari.Enabled = false;
                        ddlAO.Enabled = false;
                        ddluangmuka.Enabled = false;
                        ddlbycad.Enabled = false;
                        txtkalicadangan.Enabled = false;
                        txttglrealisasi.Enabled = false;
                        ddlprodukpengajuan.Enabled = false;
                        ddljaminan.Enabled = false;
                        txtketeranganJaminan.Enabled = false;
                        ddljangka.Enabled = false;
                        btntambah.Enabled = false;
                        btnminus.Enabled = false;
                        txtdiscountangsuran.Enabled = false;
                        btncekcadangan.Enabled = false;
                        ddlPekerjaan.Enabled = false;
                        ddlrekening.Enabled = false;
                        ddlstatusrumah.Enabled = false;
                        ddlstatuskawinupload.Enabled = false;
                        txtktppenjamin.Enabled = false;
                        txtnamapenjamin.Enabled = false;
                        txtkondisiJaminan.Enabled = false;
                        txttaksasi.Enabled = false;
                        txtnamaibukandung.Enabled = false;
                        txtNPWP.Enabled = false;
                        txtnomerkk.Enabled = false;
                        txtuangmuka.Enabled = false;
                        txtkaliuangmuka.Enabled = false;
                        txtbymaterai.Enabled = false;
                        ddlPekerjaan.Enabled = false;
                        ddlsektorusahapenjamin.Enabled = false;
                        txtjenisusahapenjamin.Enabled = false;
                        ddljenistagihan.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



                        //lbljeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                        //lblao.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        //lbltglRealisasi.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString()));
                        //lbljangkawaktu.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        //lbljthtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                        //lblbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                        //lblmajuhari.Text = ddlmajuhari.SelectedItem.Text;
                        //lblbesarpinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()));
                        //lblangsuranbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                        //lbladministrasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        //lblmaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                        //LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString()));
                        //LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        //lblTotalPotongbiaya.Text = string.Format("{0:N2}", decimal.Parse(lbladministrasi.Text) + decimal.Parse(lblmaterai.Text) + decimal.Parse(LblPotonganangsuran.Text) + decimal.Parse(LblAnggota.Text));
                        //lblygditerimaanggota.Text = string.Format("{0:N2}", decimal.Parse(lblbesarpinjaman.Text) - decimal.Parse(lblTotalPotongbiaya.Text));



                        // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                        // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
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
                    if (SessBranchID == "000")
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
                    if (ddlstatus.SelectedValue != "4" && ddlstatus.SelectedValue != "5")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Batalkan Antrian Hanya dapat dilakukan di status Interview Admin dan Teller');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        btnSimpan.Visible = false;
                        BtnApproveAntrian.Visible = false;
                        BtnBatalkanmohon.Visible = false;
                        btnlanjutkan.Visible = false;
                        btnTolakAntrian.Visible = true;

                        pnladd.Visible = true;
                        pnlIdentitas.Visible = true;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        pnlJaminan.Visible = true;
                        pnlPengajuan.Visible = true;
                        ddlbiayaanggota.Enabled = false;
                        txtcadanganRekening.Enabled = false;
                        txttgltagih.Enabled = false;
                        ddlPekerjaan.Enabled = false;
                        ddlsektorusahapenjamin.Enabled = false;
                        txtjenisusahapenjamin.Enabled = false;

                        //string register
                        HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        HDid.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                        HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                        HDNrekeningkreditbo.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                        var[1] = gb.CreateSPParameter("@id", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
                        var[2] = gb.CreateSPParameter("@Register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
                        var[3] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVERIFIKASIPINJAMAN", typeof(string));
                        var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_VERIFIKASI_PINJAMAN", var, "constrmf");

                        ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
                        ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
                        txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

                        ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                        ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
                        txtjenisusahaKtp.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();

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
                        txtinfox.Text= ge.ListDataset.Tables[0].Rows[0]["infomaterai"].ToString();

                        txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                        txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                        ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                        ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                        ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
                        //data ktp
                        ddljenistagihan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenistagihan"].ToString();
                        //txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                        txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                        ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                        ddljenkelktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
                        ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();


                        //imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();


                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                        txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();
                        txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
                        txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();
                        //ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
                        txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["jaminan"].ToString();
                        txtkondisiJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
                        txttaksasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
                        ddlpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["tipeperpanjangan"].ToString();
                        txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                        txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                        txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
                        txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
                        txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
                        txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
                        txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
                        txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
                        ddlprovinsitpt.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
                        txtdiscountangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["discountangsuran"].ToString()));

                        ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();
                        ddlbiayaanggota.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Typebiayakeanggotaan"].ToString();
                        txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["Rekeninglamacadangan"].ToString();
                        txttgltagih.Text = ge.ListDataset.Tables[0].Rows[0]["tgltagihhari"].ToString();
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




                        //data tempat tinggal
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
                            txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

                        }


                        ddlprodukpengajuan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                        //ddlprodukpengajuan_SelectedIndexChanged(sender,e);
                        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[3];
                        var2x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var2x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var2x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_listJaminan", var2x, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

                        ddljaminan.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljaminan.DataTextField = "namajaminan";
                        ddljaminan.DataValueField = "namajaminan";
                        ddljaminan.DataBind();

                        ddljaminan.Items.Insert(0, new ListItem("Pilih Jaminan", "-"));
                        ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();


                        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                        var1[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var1[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var1[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_PROGRAMPINJAMAN", var1, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

                        ddlprogrampinjaman.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddlprogrampinjaman.DataTextField = "keterangan";
                        ddlprogrampinjaman.DataValueField = "kode";
                        ddlprogrampinjaman.DataBind();
                        //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));


                        HDJenisPinjaman.Value = gex.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString();



                        DefaultClass.Global.SqlHelper[] var1x = new DefaultClass.Global.SqlHelper[3];
                        var1x[0] = gbx.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                        var1x[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                        var1x[2] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        gex.ListDataset = gbx.GetDataJustOutPut("AR_Jangka", var1x, "constrmf");

                        gex.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

                        ddljangka.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljangka.DataTextField = "jangka";
                        ddljangka.DataValueField = "jangka";
                        ddljangka.DataBind();

                        ddljangka.Items.Insert(0, new ListItem("Pilih Jangka", "-"));

                        txttglrealisasi.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString())); ;
                        txtjttempo.Text = string.Format("{0:dd-MM-yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                        txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
                        ddljangka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        ddlbycad.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["cadangan"].ToString();
                        txtkalicadangan.Text = ge.ListDataset.Tables[0].Rows[0]["qtycadangan"].ToString();
                        ddluangmuka.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["uangmuka"].ToString();
                        txtkaliuangmuka.Text = ge.ListDataset.Tables[0].Rows[0]["qtyuangmuka"].ToString();
                        ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        ddlmajuhari.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["maju_hari"].ToString();
                        txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()));
                        txtProvisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["persen_prov"].ToString()));
                        txtnominalprovisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                        txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                        LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        txtcadsebelumnya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["cadangansebelumnya"].ToString()));
                        lblsimpananwajib.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayasimpananwajib"].ToString()));
                        //ddlbycad_SelectedIndexChanged(sender, e);
                        txtkalicadangan_TextChanged(sender, e);
                        txtkaliuangmuka_TextChanged(sender, e);
                        ddlrekening.Items.Clear();
                        if (ddlpengajuan.SelectedValue == "B")
                        {


                            ddlrekening.Enabled = false;
                            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
                          
                        }
                        else if (ddlpengajuan.SelectedValue == "X")
                        {
                            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[6];
                            vart1[0] = gbx.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                            vart1[1] = gbx.CreateSPParameter("@jns_permohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
                            vart1[2] = gbx.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                            vart1[3] = gbx.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            vart1[4] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAMINAN", typeof(string));
                            vart1[5] = gbx.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                            gex.ListDataset = gbx.GetDataJustOutPut("GetRekeningPerpanjangan", vart1, "constrmf");
                            gex.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";
                            ddlrekening.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                            ddlrekening.DataTextField = "no_rekening";
                            ddlrekening.DataValueField = "no_rekening";
                            ddlrekening.DataBind();
                            ddlrekening.Enabled = true;
                            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
                            ddlrekening.Items.Insert(1, new ListItem("Rekening Lama Tidak Ada", "B"));
                            ddlrekening.Enabled = true;
                            ddlrekening.SelectedValue  = ge.ListDataset.Tables[0].Rows[0]["norekeningperpanjangan"].ToString();
                        }



                        ddlbiayaanggota.Enabled = false;
                        txtcadanganRekening.Enabled = false;
                        txttgltagih.Enabled = false;

                        ddlpengajuan.Enabled = false;
                        ddlbiayaanggota.Enabled = false;
                        lblsimpananwajib.Enabled = false;
                        ddlmajuhari.Enabled = false;
                        ddlAO.Enabled = false;
                        ddluangmuka.Enabled = false;
                        ddlbycad.Enabled = false;
                        txtkalicadangan.Enabled = false;
                        txttglrealisasi.Enabled = false;
                        ddlprodukpengajuan.Enabled = false;
                        ddljaminan.Enabled = false;
                        txtketeranganJaminan.Enabled = false;
                        ddljangka.Enabled = false;
                        btntambah.Enabled = false;
                        btnminus.Enabled = false;
                        txtdiscountangsuran.Enabled = false;
                        btncekcadangan.Enabled = false;
                        ddlPekerjaan.Enabled = false;
                        ddlrekening.Enabled = false;
                        ddlstatusrumah.Enabled = false;
                        ddlstatuskawinupload.Enabled = false;
                        txtktppenjamin.Enabled = false;
                        txtnamapenjamin.Enabled = false;
                        txtkondisiJaminan.Enabled = false;
                        txttaksasi.Enabled = false;
                        txtnamaibukandung.Enabled = false;
                        txtNPWP.Enabled = false;
                        txtnomerkk.Enabled = false;
                        txtuangmuka.Enabled = false;
                        txtkaliuangmuka.Enabled = false;
                        txtbymaterai.Enabled = false;
                        ddljenistagihan.Enabled = false;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



                        //lbljeniskredit.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();
                        //lblao.Text = ge.ListDataset.Tables[0].Rows[0]["kd_reff2"].ToString();
                        //lbltglRealisasi.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgldrop"].ToString()));
                        //lbljangkawaktu.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                        //lbljthtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgljthtempo"].ToString()));
                        //lblbunga.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                        //lblmajuhari.Text = ddlmajuhari.SelectedItem.Text;
                        //lblbesarpinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()));
                        //lblangsuranbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_bulanan"].ToString()));
                        //lbladministrasi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_provisi"].ToString()));
                        //lblmaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["meterai"].ToString()));
                        //LblPotonganangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biaya_cadangan"].ToString()));
                        //LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["biayaanggota"].ToString()));
                        //lblTotalPotongbiaya.Text = string.Format("{0:N2}", decimal.Parse(lbladministrasi.Text) + decimal.Parse(lblmaterai.Text) + decimal.Parse(LblPotonganangsuran.Text) + decimal.Parse(LblAnggota.Text));
                        //lblygditerimaanggota.Text = string.Format("{0:N2}", decimal.Parse(lblbesarpinjaman.Text) - decimal.Parse(lblTotalPotongbiaya.Text));



                        // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
                        // imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
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
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;

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
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;

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
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;
                        string lblProduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblProduk")).Text;
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                        
                        var[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, norek, typeof(string));
                        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTUNEW", typeof(string));
                        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataTable = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf").Tables[0];
                        //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", var, "constrmf");

                        //rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));

                        if (ge.ListDataTable.Rows.Count > 0)
                        {

                            if (SessKoperasiID == "I")
                            {
                                if (lblProduk == "3C")
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih3C.rpt"));
                                }
                                else
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih.rpt"));
                                }
                               
                            }
                            else if (SessKoperasiID == "II")
                            {
                                if (lblProduk == "3C")
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih23C.rpt"));
                                }
                                else
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih2.rpt"));
                                }

                                
                            }
                            else if (SessKoperasiID == "III")
                            {
                               
                                if (lblProduk == "3C")
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih33C.rpt"));
                                }
                                else
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih3.rpt"));
                                }
                            }
                            else if (SessKoperasiID == "IV")
                            {
                                
                                if (lblProduk == "3C")
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih43C.rpt"));
                                }
                                else
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih4.rpt"));
                                }
                            }
                            else if (SessKoperasiID == "V")
                            {
                                
                                if (lblProduk == "3C")
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih53C.rpt"));
                                }
                                else
                                {
                                    rprt.Load(Server.MapPath("~/AR/Report/rptCetakKartuAngsuranTagih5.rpt"));
                                }
                            }

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
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;

                        string kode_produk, file_report;


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                        var[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPK", typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                        {

                      

                            kode_produk = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
                        file_report = ge.ListDataset.Tables[0].Rows[0]["path_filereport"].ToString();
                        string status = "1";
                        decimal biayacollection = 0;
                        decimal bungaxr = 0;
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
                            rprt.SetParameterValue("jaminan", ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString().Replace("\r\n", ""+ " /tidak terlampir" + " \r\n").Remove(ge.ListDataset.Tables[0].Rows[0]["ket_jmn"].ToString().Replace("\r\n", "" + " /tidak terlampir" + " \r\n").Length - 1, 0) + " /tidak terlampir");
                            rprt.SetParameterValue("nama_cabang", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString());
                            rprt.SetParameterValue("terbilangPlafon", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString())));
                            rprt.SetParameterValue("terbilang_angsuran_perbulan", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rek_tabbrx"].ToString())));
                            rprt.SetParameterValue("no_pk", ge.ListDataset.Tables[0].Rows[0]["no_spk"].ToString());
                            rprt.SetParameterValue("no_rekening-----20", norek);
                            rprt.SetParameterValue("nama_koperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("tanggal_print", DateTime.Now.ToString("dd-MMMM-yyyy"));
                            rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());

                                //if (SessBranchID != "301")
                                //{
                                //    rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                //}
                                //else
                                //{

                                //    rprt.SetParameterValue("pad", "");
                                //}

                                rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                if (SessBranchID != "301")
                                {
                                    rprt.SetParameterValue("izin_cabang", "Izin Cabang Nomor : " +  ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                                    //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                }
                                else
                                {
                                    rprt.SetParameterValue("izin_cabang", "");
                                }

                                rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            rprt.SetParameterValue("statuskop", status);
                            rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                            rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            //rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            rprt.SetParameterValue("cabang", ge.ListDataset.Tables[0].Rows[0]["cabang"].ToString());
                            rprt.SetParameterValue("bagian", ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString());
                            rprt.SetParameterValue("tanggal_print", ge.ListDataset.Tables[0].Rows[0]["tgl_realisasi"].ToString());
                            bungaxr = decimal.Parse("0,02") * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString()) * decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString())/ decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString())/ decimal.Parse("4");
                            biayacollection =decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) - bungaxr;
                            //bungaxr = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString()) * decimal.Parse("0,7");
                            rprt.SetParameterValue("collection", gb.ToRupiah(string.Format("{0:N0}", biayacollection).ToString()).Replace("Rp", ""));
                            rprt.SetParameterValue("bungabaru", gb.ToRupiah(string.Format("{0:N0}", bungaxr).ToString()).Replace("Rp", ""));

        
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
                            rprt.SetParameterValue("nama_koperasi", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                            rprt.SetParameterValue("kepala_cabang", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());
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
                            rprt.SetParameterValue("NoRekening", norek);
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
                                //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                //if (SessBranchID != "301")
                                //{
                                //    rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                //}
                                //else
                                //{

                                //    rprt.SetParameterValue("pad", "");
                                //}

                                rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                if (SessBranchID != "301")
                                {
                                    rprt.SetParameterValue("izin_cabang", "Izin Cabang Nomor : " +  ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                                    //rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                }
                                else
                                {
                                    rprt.SetParameterValue("izin_cabang", "");
                                }

                                rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                            rprt.SetParameterValue("statuskop", status);
                            rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());

                            //rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                            rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                            rprt.SetParameterValue("cabang", ge.ListDataset.Tables[0].Rows[0]["cabang"].ToString());
                            rprt.SetParameterValue("kepala_cabang", ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString());


                            Response.ClearContent();
                            Response.ClearHeaders();

                            Response.ContentType = "application/pdf";
                            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                            Response.End();
                        }

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
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        string Typebiayakeanggotaan;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;
                        Typebiayakeanggotaan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTypebiayakeanggotaan")).Text;
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                        var[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCETAKKEANGGOTAAN", typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                        {

                            if (Typebiayakeanggotaan == "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sudah menjadi Anggota');", true);

                            }
                            else if (Typebiayakeanggotaan == "2")
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Biaya dibebankan Ke Cabang');", true);

                            }
                            else if (Typebiayakeanggotaan == "1")
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
            case "TandaPenjamin":
                try
                {
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                        var[0] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, norek, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPENJAMIN", typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_PENCAIRAN", var, "constrmf");
                        if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                        {

                            if (ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString() == "3B")
                            {
                                rprt.Load(Server.MapPath("~/AR/Report/RptSuratPenjamin.rpt"));
                                rprt.SetParameterValue("nikpenjamin", ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString());
                                rprt.SetParameterValue("namapenjamin", ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString());
                                rprt.SetParameterValue("alamat", ge.ListDataset.Tables[0].Rows[0]["alamat"].ToString());

                                rprt.SetParameterValue("NikPeminjam", ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString());
                                rprt.SetParameterValue("namapeminjam", ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString());

                                rprt.SetParameterValue("Plafon", gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString().Replace(",", ".")));
                                rprt.SetParameterValue("tanggal", ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString());
                                rprt.SetParameterValue("cabang", ge.ListDataset.Tables[0].Rows[0]["Nama_Cabang"].ToString());
                                rprt.SetParameterValue("terbilang", gb.Terbilang(decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString().Replace(",", "."))));
                                rprt.SetParameterValue("badanhukum", "Badan Hukum Nomor : " + ge.ListDataset.Tables[0].Rows[0]["badan_hukum"].ToString());
                                rprt.SetParameterValue("pad", "PAD No : " + ge.ListDataset.Tables[0].Rows[0]["Pad_no"].ToString());
                                rprt.SetParameterValue("alamatpusat", ge.ListDataset.Tables[0].Rows[0]["alamatpusat"].ToString());
                                rprt.SetParameterValue("statuskop", "1");
                                rprt.SetParameterValue("kode_koperasi", ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString());
                                rprt.SetParameterValue("nama_perusahaan", ge.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString());
                                rprt.SetParameterValue("izin_cabang", ge.ListDataset.Tables[0].Rows[0]["izin_cabang"].ToString());
                                rprt.SetParameterValue("alamat2", ge.ListDataset.Tables[0].Rows[0]["alamat2"].ToString());
                                rprt.SetParameterValue("namacabang", ge.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString());
                                rprt.SetParameterValue("norek", ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString());
                                Response.ClearContent();
                                Response.ClearHeaders();

                                Response.ContentType = "application/pdf";
                                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                                Response.End();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Khusus Produk Bulanan');", true);
                            }
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
            case "KwitansiAnggota":
                try
                {
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {

                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                        Paramsc[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSIANGGOTA", typeof(string));
                        Paramsc[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramsc[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramsc[3] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                        //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf");
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_Trn_AR_PENCAIRAN", Paramsc, "constrmf").Tables[0];


                        if (ge.ListDataTable.Rows.Count > 0)
                        {

                            if (ge.ListDataTable.Rows[0]["flag_bayar"].ToString() == "0")
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
            case "TandaTerimaJaminan":
                try
                {
                    if (ddlstatus.SelectedValue != "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Print Report Hanya dapat dilakukan di status Interview Admin');", true);
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        txtnonasabah.Focus();
                    }
                    else
                    {
                        string nobukti;
                        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        string norek = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrekeningkreditbo")).Text;

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
    public void InsertCombo()
    {
        

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

        ddlprodukpengajuan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprodukpengajuan.DataTextField = "produk";
        ddlprodukpengajuan.DataValueField = "kode_produk";
        ddlprodukpengajuan.DataBind();
        ddlprodukpengajuan.SelectedValue = "3C";
        //ddlprodukpengajuan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo  Jenis Penggunaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetPenggunaan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penggunaan asc";

        ddlPenggunaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlPenggunaan.DataTextField = "penggunaan";
        ddlPenggunaan.DataValueField = "kode_penggunaan";
        ddlPenggunaan.DataBind();
        //ddlPenggunaan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Jenis Pinjaman

        //ddljenispinjaman.Items.Add("Pilih Data");
        ddljenispinjaman.Items.Add("MIKRO");

       

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
        //ddlAO.Items.Insert(0, new ListItem("Pilih Data", "-"));

      

       
        DefaultClass.Global.SqlHelper[] var1x = new DefaultClass.Global.SqlHelper[3];
        var1x[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var1x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_PROGRAMPINJAMAN", var1x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode asc";

        ddlprogrampinjaman.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprogrampinjaman.DataTextField = "keterangan";
        ddlprogrampinjaman.DataValueField = "kode";
        ddlprogrampinjaman.DataBind();


       

        


        //ddlprogrampinjaman.Items.Insert(0, new ListItem("Pilih Data", "-"));


        HDJenisPinjaman.Value = ge.ListDataset.Tables[0].Rows[0]["jns_pinjaman"].ToString();



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



        DefaultClass.Global.SqlHelper[] var1z = new DefaultClass.Global.SqlHelper[6];
        var1z[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1z[1] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, HDJenisPinjaman.Value, typeof(string));
        var1z[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1z[3] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var1z[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1z[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GetSumberDanaCabangBank", var1z, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kodebank asc";

        ddlsumberdana.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsumberdana.DataTextField = "kode_sumberdana";
        ddlsumberdana.DataValueField = "Kodebank";
        ddlsumberdana.DataBind();
        //ddlsumberdana.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[3];
        var1xx[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var1xx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var1xx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Jangka", var1xx, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";

        ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangka.DataTextField = "jangka";
        ddljangka.DataValueField = "jangka";
        ddljangka.DataBind();

        ddljangka.Items.Insert(0, new ListItem("Pilih Jangka", "-"));


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


        // -- Combo Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetSektorUsaha()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_sektor_usaha asc";

        ddlSektorUsahaKtp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlSektorUsahaKtp.DataTextField = "keterangan";
        ddlSektorUsahaKtp.DataValueField = "kode_sektor_usaha";
        ddlSektorUsahaKtp.DataBind();


        ddlsektorusahapenjamin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsektorusahapenjamin.DataTextField = "keterangan";
        ddlsektorusahapenjamin.DataValueField = "kode_sektor_usaha";
        ddlsektorusahapenjamin.DataBind();



        // -- Combo Provinsi Pekerjaan
        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";

        ddlprovinsipekerjaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprovinsipekerjaan.DataTextField = "nama_provinsi";
        ddlprovinsipekerjaan.DataValueField = "kode_provinsi";
        ddlprovinsipekerjaan.DataBind();



        ge.ListDataset = gb.GetDataFunction("dbo.GetProvinsi()", "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "nama_provinsi asc";
        ddlpropinsiktp.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlpropinsiktp.DataTextField = "nama_provinsi";
        ddlpropinsiktp.DataValueField = "kode_provinsi";
        ddlpropinsiktp.DataBind();

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

        ddlrekening.Enabled = false;
        ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlrekening.SelectedValue = "-";
        //  ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));


        //// --- Combo Pendidikan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetJenisJaminan()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

        //ddljaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddljaminan.DataTextField = "namajaminan";
        //ddljaminan.DataValueField = "namajaminan";
        //ddljaminan.DataBind();


        DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[3];
        var2x[0] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_listJaminan", var2x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

        ddljaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljaminan.DataTextField = "namajaminan";
        ddljaminan.DataValueField = "namajaminan";
        ddljaminan.DataBind();

        ddljaminan.Items.Insert(0, new ListItem("Pilih Jaminan", "-"));

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

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                BtnApproveAntrian.Enabled = true;
            }
            else
            {



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

                string lbltglrealisasi;

                lbltglrealisasi = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(txttglrealisasi.Text)); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();


                no_pk = no_pk_sekarang + 1;

                CRno_pk = String.Format("{0:" + formatpknumber + "}", no_pk) + "/" + format_no_pk + SessBranchID + "/" + lbltglrealisasi.Substring(0, 5).Replace("/", "-") + "/" + lbltglrealisasi.Substring(6, 4);

                updatepkno = String.Format("{0:" + formatnumber + "}", no_pk);

                HDnopk.Value = CRno_pk;
                HDnopksekarang.Value = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();
                HDNoPKSebelumnya.Value = nopksebelumnya;

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[37];


                var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
                var1[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglrealisasi.Text, typeof(string));
                var1[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 250, txtplafon.Text, typeof(decimal));
                var1[3] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
                var1[4] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
                var1[5] = gb.CreateSPParameter("@kode_pengguna", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlPenggunaan.SelectedValue, typeof(string));
                var1[6] = gb.CreateSPParameter("@no_spk", ParameterDirection.Input, SqlDbType.VarChar, 50, HDnopk.Value, typeof(string));
                var1[7] = gb.CreateSPParameter("@tgl_dropx", ParameterDirection.Input, SqlDbType.SmallDateTime, 15, txttglrealisasi.Text, typeof(DateTime));
                var1[8] = gb.CreateSPParameter("@tgl_due", ParameterDirection.Input, SqlDbType.SmallDateTime, 15, txtjttempo.Text, typeof(DateTime));
                var1[9] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 8, ddljangka.SelectedValue, typeof(int));
                var1[10] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.Decimal, 250, txtbymaterai.Text, typeof(decimal));
                var1[11] = gb.CreateSPParameter("@taksasi", ParameterDirection.Input, SqlDbType.Decimal, 250, txttaksasi.Text, typeof(decimal));
                var1[12] = gb.CreateSPParameter("@kd_staff", ParameterDirection.Input, SqlDbType.VarChar, 250, ddlAO.SelectedValue, typeof(string));
                var1[13] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 250, "999", typeof(string));
                var1[14] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 250, txtjttempo.Text, typeof(string));
                var1[15] = gb.CreateSPParameter("@projt", ParameterDirection.Input, SqlDbType.VarChar, 250, "2", typeof(string));
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
                var1[26] = gb.CreateSPParameter("@Uangmuka", ParameterDirection.Input, SqlDbType.Decimal, 0, txtuangmuka.Text, typeof(decimal));
                var1[27] = gb.CreateSPParameter("@cadangansebelumnya", ParameterDirection.Input, SqlDbType.Decimal, 0, txtcadsebelumnya.Text, typeof(decimal));
                var1[28] = gb.CreateSPParameter("@norekeningperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrekening.SelectedValue, typeof(string));
                var1[29] = gb.CreateSPParameter("@tipeperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
                var1[30] = gb.CreateSPParameter("@cadangankembali", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkembalicadangan.Text, typeof(decimal));
                var1[31] = gb.CreateSPParameter("@jenistagihan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenistagihan.SelectedValue, typeof(string));
                var1[32] = gb.CreateSPParameter("@Typebiayakeanggotaan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlbiayaanggota.SelectedValue, typeof(string));
                var1[33] = gb.CreateSPParameter("@Rekeninglamacadangan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtcadanganRekening.Text, typeof(string));
                var1[34] = gb.CreateSPParameter("@tgltagihhari", ParameterDirection.Input, SqlDbType.VarChar, 40, txttgltagih.Text, typeof(string));
                var1[35] = gb.CreateSPParameter("@ddlbycad", ParameterDirection.Input, SqlDbType.Int, 8, ddlbycad.SelectedValue, typeof(int));
                var1[36] = gb.CreateSPParameter("@discountangsuran", ParameterDirection.Input, SqlDbType.Decimal, 250, txtdiscountangsuran.Text, typeof(decimal));



                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_Insert_Cair_Antrian_NEW_2024_004", var1, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {


                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Antrian Sukses!','You successfully save','top-right','success','1');", true);
                    //pnladd.Visible = false;
                    //pnlIdentitas.Visible = false;
                    //PnlFront.Visible = true;
                    //pnlFilter.Visible = true;
                    //pnlJaminan.Visible = false;
                    //pnlPengajuan.Visible = false;
                    //btnSimpan.Visible = false;
                    //BtnApproveAntrian.Visible = false;
                    //BtnBatalkanmohon.Visible = false;
                    //BtnApproveAntrian.Visible = false;
                    //BtnBatalkanmohon.Visible = false;
                    //btnlanjutkan.Visible = false;
                    //btnTolakAntrian.Visible = false;
                    //Pnlupdatedata.Visible = false;
                    //pnlAlasanTolak.Visible = false;
                    //txtnonasabah.Text = "";
                    //txtketeranganJaminan.Text = "";
                    //txtkondisiJaminan.Text = "-";
                    //txttaksasi.Text = "0";
                    //txtktppenjamin.Text = "0";
                    //txtnamapenjamin.Text = "-";
                    //txtnamaibukandung.Text = "-";
                    //txtNPWP.Text = "0";
                    //txtnomerkk.Text = "0";
                    //txtcadsebelumnya.Text = "0";
                    //txtkembalicadangan.Text = "0";
                    //txtuangmuka.Text = "0";
                    //txtkaliuangmuka.Text = "0";
                    //ddlbycad.SelectedValue = "2";
                    //TextBox2.Text = "0";
                    //lblsisapenghasilan.Text = "0";
                    //txtdiscountangsuran.Text = "0";
                    //ddljangka.SelectedValue = "-";
                    //ddlstatus_SelectedIndexChanged(sender, e);
                    //txtnonasabah.Focus();
                    pnladd.Visible = false;
                    pnlIdentitas.Visible = false;
                    PnlFront.Visible = true;
                    pnlFilter.Visible = true;
                    pnlJaminan.Visible = false;
                    pnlPengajuan.Visible = false;
                    btnSimpan.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                    btnlanjutkan.Visible = false;
                    btnTolakAntrian.Visible = false;
                    Pnlupdatedata.Visible = false;
                    pnlAlasanTolak.Visible = false;
                    txtnonasabah.Text = "";
                    txtketeranganJaminan.Text = "";
                    txtkondisiJaminan.Text = "-";
                    txttaksasi.Text = "0";
                    txtktppenjamin.Text = "0";
                    txtnamapenjamin.Text = "-";
                    txtnamaibukandung.Text = "-";
                    txtNPWP.Text = "0";
                    txtnomerkk.Text = "0";
                    txtcadsebelumnya.Text = "0";
                    txtkembalicadangan.Text = "0";
                    txtuangmuka.Text = "0";
                    txtkaliuangmuka.Text = "0";
                    TextBox2.Text = "0";
                    txtcadanganRekening.Text = "";
                    ddljangka.SelectedValue = "-";
                    ddlstatus_SelectedIndexChanged(sender, e);
                    ddlpengajuan.SelectedValue = "B";
                    ddljenispermohonan_SelectedIndexChanged(sender, e);
                    ddlrekening.SelectedValue = "-";
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

    protected void btnTolak_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;

        pnlPengajuan.Visible = false;

        pnlAlasanTolak.Visible = true;
        pnlJaminan.Visible = false;
        pnlIdentitas.Visible = false;
        txtalasantolak.Focus();
        
        btnSimpanTolakan.Enabled = true;
        btnSimpanTolakan.Visible = true;
    }
    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[9];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalasantolak.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REJECTSTATUSCAIR", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
                pnladd.Visible = false;
                pnlIdentitas.Visible = false;
                PnlFront.Visible = true;
                pnlFilter.Visible = true;
                pnlJaminan.Visible = false;
                pnlPengajuan.Visible = false;
                btnSimpan.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
                btnlanjutkan.Visible = false;
                btnTolakAntrian.Visible = false;
                Pnlupdatedata.Visible = false;
                pnlAlasanTolak.Visible = false;
                txtnonasabah.Text = "";
                txtketeranganJaminan.Text = "";
                txtkondisiJaminan.Text = "-";
                txttaksasi.Text = "0";
                txtktppenjamin.Text = "0";
                txtnamapenjamin.Text = "-";
                txtnamaibukandung.Text = "-";
                txtNPWP.Text = "0";
                txtnomerkk.Text = "0";
                txtcadsebelumnya.Text = "0";
                txtkembalicadangan.Text = "0";
                txtuangmuka.Text = "0";
                txtkaliuangmuka.Text = "0";
                TextBox2.Text = "0";
                ddljangka.SelectedValue = "-";
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
        pnlPekerjaan.Visible = false;
        pnlTambahan.Visible = false;
        pnlPendapatan.Visible = false;

        pnlPengajuan.Visible = true;

        pnlAlasanTolak.Visible = false;
        pnlIdentitas.Visible = true;
        pnlJaminan.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }


    protected void btnbatalkanantrian_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpanTolakan.Enabled = false;

            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[10];
            var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "3", typeof(string));
            var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalasantolak.Text, typeof(string));
            var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "3", typeof(string));
            var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
            var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BATALKANTELLER", typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
            var88[9] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNrekeningkreditbo.Value, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                pnladd.Visible = false;
                pnlIdentitas.Visible = false;
                PnlFront.Visible = true;
                pnlFilter.Visible = true;
                pnlJaminan.Visible = false;
                pnlPengajuan.Visible = false;
                btnSimpan.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
                BtnApproveAntrian.Visible = false;
                BtnBatalkanmohon.Visible = false;
                btnlanjutkan.Visible = false;
                btnTolakAntrian.Visible = false;
                Pnlupdatedata.Visible = false;
                pnlAlasanTolak.Visible = false;
                txtnonasabah.Text = "";
                txtketeranganJaminan.Text = "";
                txtkondisiJaminan.Text = "-";
                txttaksasi.Text = "0";
                txtktppenjamin.Text = "0";
                txtnamapenjamin.Text = "-";
                txtnamaibukandung.Text = "-";
                txtNPWP.Text = "0";
                txtnomerkk.Text = "0";
                txtcadsebelumnya.Text = "0";
                txtkembalicadangan.Text = "0";
                txtuangmuka.Text = "0";
                txtkaliuangmuka.Text = "0";
                TextBox2.Text = "0";
                ddljangka.SelectedValue = "-";
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

    protected void btnlanjutkan_Click(object sender, EventArgs e)
    {
        try
        {
            btnlanjutkan.Enabled = false;

           

            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglrealisasi.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnlanjutkan.Enabled = true;
            }
            else
            {



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
                var88[10] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 100, txtemailsktp.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    //btnlanjutkan.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','APPROVE TELLER SUKSES','top-right','success','1');", true);
                    pnladd.Visible = false;
                    pnlIdentitas.Visible = false;
                    PnlFront.Visible = true;
                    pnlFilter.Visible = true;
                    pnlJaminan.Visible = false;
                    pnlPengajuan.Visible = false;
                    btnSimpan.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                    BtnApproveAntrian.Visible = false;
                    BtnBatalkanmohon.Visible = false;
                    btnlanjutkan.Visible = false;
                    btnTolakAntrian.Visible = false;
                    txtnonasabah.Text = "";
                    txtketeranganJaminan.Text = "";
                    txtkondisiJaminan.Text = "-";
                    txttaksasi.Text = "0";
                    txtktppenjamin.Text = "0";
                    txtnamapenjamin.Text = "-";
                    txtnamaibukandung.Text = "-";
                    txtNPWP.Text = "0";
                    txtnomerkk.Text = "0";
                    txtcadsebelumnya.Text = "0";
                    txtkembalicadangan.Text = "0";
                    txtuangmuka.Text = "0";
                    txtkaliuangmuka.Text = "0";
                    TextBox2.Text = "0";
                    txtcadanganRekening.Text = "";
                    ddljangka.SelectedValue = "-";
                    ddlstatus_SelectedIndexChanged(sender, e);
                    Pnlupdatedata.Visible = false;
                    pnlAlasanTolak.Visible = false;
                    txtnonasabah.Focus();
                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    btnlanjutkan.Enabled = true;
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            btnlanjutkan.Enabled = true;
        }



    }

}