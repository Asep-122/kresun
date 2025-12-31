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

public partial class AR_Process_PermohonanPinjamanKreditMultiguna : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    DateTime tgl_libur, tgl_realisasi, tgldrop;
    double angsuranperbulan = 0;
    string nonasabah = "", agama = "", usia = "", pokok = "", bunga = "", namafileJaminan = "", fotopathJaminan = "";

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
            BindGridEntity();
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
            ddlbycad.Items.Add("Pilih Data");
            ddlbycad.Items.Add("Dengan Biaya Cadangan");
            ddlbycad.Items.Add("Tanpa Biaya Cadangan");

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
            //  ddlpendidikan.Items.Insert(0, new ListItem("Pilih Data", "-"));


            // --- Combo Pendidikan
            ge.ListDataset = gb.GetDataFunction("dbo.GetJenisJaminan()", "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "namajaminan asc";

            ddljaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljaminan.DataTextField = "namajaminan";
            ddljaminan.DataValueField = "namajaminan";
            ddljaminan.DataBind();

            
                
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
        CultureName = "id-ID";

        txttglrealisasi.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
        //get tanggal server
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
        ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        txttglrealisasi.Text = xtanggal.ToString("yyyy-MM-dd");

        //txtjangka.Attributes["onchange"] = "if (IsValid(this) == false ) return;";

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
    
    protected void ddlbycad_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlbycad.SelectedIndex == 1)
        {
            txtkalicadangan.Text = "1";
            txtkalicadangan.Enabled = true;
            txtkalicadangan_TextChanged(sender, e);

        }
        else if (ddlbycad.SelectedIndex == 2)
        {
            txtkalicadangan.Enabled = false;
            TextBox3.Text = "0";
            txtkalicadangan.Text = "0";

        }

    }
    protected void ddljenispermohonan_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlpengajuan.SelectedValue=="B")
        {


            ddlrekening.Enabled = false;
            ddlrekening.Items.Insert(0, new ListItem("Pilih Data", "-"));
            txtketeranganJaminan.Text = "";
            txtpathjaminan.Text = "";
            txtkondisiJaminan.Text = "";
            txttaksasi.Text = "0";

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
          

        }

    }

    protected void ddlrekening_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
        var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrekening.SelectedValue, typeof(string));
        var1[2] = gb.CreateSPParameter("@no_anggota", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
        var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REKENINGLUNAS", typeof(string));
        var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GetRekeningPerpanjangan", var1, "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {

            ddljaminan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jaminansurat"].ToString();
            txtketeranganJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["Jaminan"].ToString();
            txtpathjaminan.Text = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();
            txtkondisiJaminan.Text = ge.ListDataset.Tables[0].Rows[0]["kondisijaminan"].ToString();
            txttaksasi.Text=string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["taksasi"].ToString()));
            imgjaminan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepath_jaminan"].ToString();// + "?r=" + DateTime.Now
        }
        else
        {
            txtketeranganJaminan.Text = "";
            txtpathjaminan.Text = "";
            txtkondisiJaminan.Text = "";
            txttaksasi.Text = "0";
        }
    }


    protected void ddluangmuka_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddluangmuka.SelectedIndex == 0)
        {
            txtkaliuangmuka.Text = "0";
            txtuangmuka.Text = "0";
            txtkaliuangmuka.Enabled = false;
            txtpokokuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));

        }
        else if (ddluangmuka.SelectedIndex == 1)
        {
            txtkaliuangmuka.Enabled = false;
            txtkaliuangmuka.Text = "1";
            txtpokokuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));
            txtuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtkaliuangmuka.Text) * decimal.Parse(txtangsuranperbulan.Text)); 
   
           

        }

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
        Response.Redirect("PermohonanPinjamanKreditMultiguna.aspx");
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

    protected void UploadFileKK(object sender, EventArgs e)
    {

        string fotopathkk;

        if (fileuploadKK.HasFile)
        {


            var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

            if (fileuploadKK.PostedFile.ContentLength > maxFileSize)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

            }

            else
            {

                string namafile;
                string filename;
                string filenamex;

                DateTime now = DateTime.Now;
                namafile = txtnomerkk.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "KK";
                string folderPath = Server.MapPath("~/Files/KK/thumb");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                filename = namafile + ".jpg";
                filenamex = namafile + "OK.jpg";


                //create temp
                string path = Server.MapPath("~/Files/KK/thumb/" + namafile + ".jpg");
                fileuploadKK.SaveAs(path);
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
                thumbBitmap.Save(Server.MapPath("~/Files/KK/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();
                fotopathkk = "~/Files/KK/" + filenamex;
                txtpathKK.Text = fotopathkk;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not.
                {
                    file.Delete();
                }

                Imagekk.ImageUrl = "http://gigaku.kspserambidana.com/Files/KK/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();
            }

        }



    }

    protected void UploadFileNPWP(object sender, EventArgs e)
    {

        string fotopathnpwp;

        if (fileuploadnpwp.HasFile)
        {


            var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

            if (fileuploadnpwp.PostedFile.ContentLength > maxFileSize)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

            }

            else
            {

                string namafile;
                string filename;
                string filenamex;

                DateTime now = DateTime.Now;
                namafile = txtNPWP.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "NPWP";
                string folderPath = Server.MapPath("~/Files/NPWP/thumb");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                filename = namafile + ".jpg";
                filenamex = namafile + "OK.jpg";


                //create temp
                string path = Server.MapPath("~/Files/NPWP/thumb/" + namafile + ".jpg");
                fileuploadnpwp.SaveAs(path);
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
                thumbBitmap.Save(Server.MapPath("~/Files/NPWP/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();
                fotopathnpwp = "~/Files/NPWP/" + filenamex;
                txtpathnpwp.Text = fotopathnpwp;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not.
                {
                    file.Delete();
                }
                Imagenpwp.ImageUrl = "http://gigaku.kspserambidana.com/Files/NPWP/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();

            }

        }



    }

    protected void UploadFilektppasangan(object sender, EventArgs e)
    {

        string fotopathpasangan;

        if (fileuploadktppasangan.HasFile)
        {


            var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

            if (fileuploadktppasangan.PostedFile.ContentLength > maxFileSize)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

            }

            else
            {

                string namafile;
                string filename;
                string filenamex;

                DateTime now = DateTime.Now;
                namafile = txtidpasangan.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "KTP_Pasangan";
                string folderPath = Server.MapPath("~/Files/KTP_Pasangan/thumb");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                filename = namafile + ".jpg";
                filenamex = namafile + "OK.jpg";


                //create temp
                string path = Server.MapPath("~/Files/KTP_Pasangan/thumb/" + namafile + ".jpg");
                fileuploadktppasangan.SaveAs(path);
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
                thumbBitmap.Save(Server.MapPath("~/Files/KTP_Pasangan/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();
                fotopathpasangan = "~/Files/KTP_Pasangan/" + filenamex;
                txtppathktppasangan.Text = fotopathpasangan;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not.
                {
                    file.Delete();
                }
                ImageKtppasangan.ImageUrl = "http://gigaku.kspserambidana.com/Files/KTP_Pasangan/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();

            }

        }



    }
    protected void tambahplafon(object sender, EventArgs e)
    {
        decimal plafonnilai = 0;

        plafonnilai = decimal.Parse(txtplafon.Text);
        if (plafonnilai < 10000000)
        {
            plafonnilai = plafonnilai + 250000;
            txtplafon.Text = string.Format("{0:N2}", plafonnilai);
        }

        if (ddljangka.SelectedValue != "-")
        {
            ddljangka_SelectedIndexChanged(sender, e);
        }
   
    }
    protected void kurangplafon(object sender, EventArgs e)
    {
        decimal plafonnilai = 0;

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
   
    
    protected void UploadFileJaminan(object sender, EventArgs e)
    {

        string fotopathjaminan;

        if (fileuploadjaminan.HasFile)
        {


            var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

            if (fileuploadjaminan.PostedFile.ContentLength > maxFileSize)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

            }

            else
            {

                string namafile;
                string filename;
                string filenamex;

                DateTime now = DateTime.Now;
                namafile = txtnoanggota.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "Jaminan";
                string folderPath = Server.MapPath("~/Files/Jaminan/thumb");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                filename = namafile + ".jpg";
                filenamex = namafile + "OK.jpg";


                //create temp
                string path = Server.MapPath("~/Files/Jaminan/thumb/" + namafile + ".jpg");
                fileuploadjaminan.SaveAs(path);
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
                thumbBitmap.Save(Server.MapPath("~/Files/Jaminan/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();
                fotopathjaminan = "~/Files/Jaminan/" + filenamex;
                txtpathjaminan.Text = fotopathjaminan;
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not.
                {
                    file.Delete();
                }
                imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + filenamex + "?r=" + DateTime.Now.Ticks.ToString();

            }

        }



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

            fileuploadjaminan.SaveAs(folderPath + namafileJaminan + ".jpg");

            ////Display the Picture in Image control.
            //imagenpwp.ImageUrl = "~/Files/NPWP/" + Path.GetFileName(fileuploadnpwp.FileName);

            fotopathJaminan = folderPath + namafileJaminan;

            txtpathjaminan.Text = "~/Files/jaminan/" + namafileJaminan + ".jpg";

            //Display the Picture in Image control.
            //imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
            imgjaminan.ImageUrl = "http://gigaku.kspserambidana.com/Files/Jaminan/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();
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
   

    protected void btnSimpan_Click(object sender, EventArgs e)
     {
        try
        {
            
            if (txtpathjaminan.Text == "" && ddlSurvey.SelectedIndex == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Belum di upload');", true);

            }
            else if (ddlpengajuan.SelectedValue == "X" && ddlrekening.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masukkan Rekening Lama Perpanjangan');", true);

            }
            else if (ddljangka.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jangka Belum di dipilih');", true);
            }
            else if (txttglrealisasi.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi Belum di dipilih');", true);
            }
            else if (ddlbycad.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cadangan Belum di dipilih');", true);
            }
            
            else
            {
                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                varx[0] = gb.CreateSPParameter("@ktp_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtktppenjamin.Text, typeof(string));
                varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPENJAMIN", typeof(string));
                varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varx, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                {
                   
                      
                    string nasabah = ge.ListDataset.Tables[0].Rows[0]["nasabah"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Penjamin masih terdapat di Anggota yg belum lunas, No. Anggota =  " + nasabah + ", Mohon Lunasi terlebih dahulu');", true);

                }
                else
                {



                    string siklus = "";
                    siklus = "2";
                    btnSimpan.Enabled = false;
                    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                    var4[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
                    var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                    var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        HDStatusDebitur.Value = "L";
                    }
                    else
                    {
                        HDStatusDebitur.Value = "B";
                    }


                    string[] splitdata = txtnonasabah.Text.Split('-');
                    nonasabah = splitdata[0];

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNONASABAH", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var, "constrmf");


                    //string[] splitdata1 = txtAgama.Text.Split('-');

                    //decimal angs_bulanan = txtangsuranperbulan.Text;


                    usia = gb.HitungTahunUsia(txttgllahirktp.Text);


                    DateTime tgldrop = Convert.ToDateTime(txttglrealisasi.Text);
                    string datetgldrop = tgldrop.ToString("yyyy/MM/dd");

                    DateTime tgljt = Convert.ToDateTime(txtjttempo.Text);
                    string datetgljt = tgljt.ToString("yyyy/MM/dd");



                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[63];
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
                    var1[20] = gb.CreateSPParameter("@filepathjaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathjaminan.Text, typeof(string));
                    var1[21] = gb.CreateSPParameter("@jaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtketeranganJaminan.Text, typeof(string));
                    var1[22] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[23] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    var1[24] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                    var1[25] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                    var1[26] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[27] = gb.CreateSPParameter("@ktp_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 30, txtktppenjamin.Text, typeof(string));
                    var1[28] = gb.CreateSPParameter("@nama_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamapenjamin.Text, typeof(string));
                    var1[29] = gb.CreateSPParameter("@file_pathktppasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtppathktppasangan.Text, typeof(string));
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
                    var1[42] = gb.CreateSPParameter("@file_pathKK", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathKK.Text, typeof(string));
                    var1[43] = gb.CreateSPParameter("@file_pathnpwp", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpathnpwp.Text, typeof(string));
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
                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var1, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;
                        //btnSave.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);


                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }
                }
            }
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
        Response.Redirect("PermohonanPinjamanKreditMultiguna.aspx");
    }
    protected void txttglrealisasi_TextChanged(object sender, EventArgs e)
    {
        DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        DateTime ToDat1e = DateTime.ParseExact(txttglrealisasi.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        
        if (ToDat1e < currentdate)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi tidak boleh kurang dari Tanggal hari ini');", true);

        }
        else
        { 

        }
 

    }

        
    protected void ddljangka_SelectedIndexChanged(object sender, EventArgs e)

    {

        if (txttglrealisasi.Text != "")
        {

        
        if (ddljangka.SelectedValue!="-")
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

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
        var2[0] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlsumberdana.SelectedValue, typeof(string));
        var2[1] = gb.CreateSPParameter("@KodeProduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlprodukpengajuan.SelectedValue, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSUKUBUNGA", typeof(string));
        var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GetBungaProdukMikro", var2, "constrmf");

        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tidak ada sumber dana memenuhi kriteria pinjaman!');", true);
            //txtjangka.Text = "";
            //txtjangka.Focus();
        }
        else
        {
            txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaproduk"].ToString())); 
            txtProvisi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["psnprovisi"].ToString()));
            txtbymaterai.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["materai"].ToString()));
            nominal_provisi = decimal.Parse(txtplafon.Text) * (decimal.Parse(txtProvisi.Text) / 100);
            string hitbunga= ge.ListDataset.Tables[0].Rows[0]["hitbunga"].ToString();
            decimal pokok = 0;
            decimal bunga = 0;
            txtnominalprovisi.Text = string.Format("{0:N2}", nominal_provisi);
            plafon = decimal.Parse(txtplafon.Text);
            pokok = plafon/int.Parse(ddljangka.SelectedValue);
            bunga = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaproduk"].ToString());
            bunga = ((bunga / 100) * int.Parse(ddljangka.SelectedValue)) * pokok ;
        
            txtangsuranperbulan.Text = string.Format("{0:N0}", pokok + bunga);
            //txtangsuranperbulan.Text = txtangsuranperbulan.Text.Replace("Rp", "");

            //HITUNG JATUH TEMPO BERDASARKAN JANGKA DAN HARI LIBUR

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


                    tgl_libur= DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_libur"].ToString());

                     txtjttempo.Text = tgl_libur.ToString("dd-MMM-yyyy");

            }
        }

        Decimal biayacadangan = 0, totbycad = 0;
        ddlbycad.SelectedValue = "Dengan Biaya Cadangan";
        txtkalicadangan.Text="1";
        biayacadangan = decimal.Parse(txtkalicadangan.Text) * decimal.Parse(txtangsuranperbulan.Text);
        //TextBox2.Text = gb.ToRupiah(biayacadangan.ToString());
        TextBox2.Text = string.Format("{0:N2}", biayacadangan);

            totbycad = biayacadangan - decimal.Parse(txtcadsebelumnya.Text);
            if (totbycad< 0)
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
                txtpokokuangmuka.Text= string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));
                txtuangmuka.Text = "0";
                txtkaliuangmuka.Enabled = false;

            }



        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi masih kosong!');", true);
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

        totbycad = biayacadangan - decimal.Parse(txtcadsebelumnya.Text);
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
        //else if (txtkodecabang.Text != txtnonasabah.Text.Substring(0, 3))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah yang di input tidak terdapat di cabang Anda');", true);

        //}
        //else if (ddlSurvey.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis Survey belum di pilih');", true);

        //}
        else
        {

            


                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAAN", typeof(string));
                var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                {
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                    var2[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                    var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLUNAS", typeof(string));
                    var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {

                    DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
                    var2x[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
                    var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
                    var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var2x[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2x, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {

                        pnladd.Visible = true;
                        pnlIdentitas.Visible = true;
                        PnlFront.Visible = false;
                        pnlFilter.Visible = false;
                        pnlJaminan.Visible = true;
                        pnlPengajuan.Visible = true;
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
                            txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
                            txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                            txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
                            txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
                            txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
                            ddlpropinsiktp.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
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
                            ddlstatusrumah.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();
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
                            txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                            txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();
                            txtpathnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString(); // "?r=" + DateTime.Now.Ticks.ToString();

                            txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();
                            txtpathKK.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();

                            Imagenpwp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathnpwp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                            ImageKtppasangan.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                            Imagekk.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathKK"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();

                            //data pekerjaan



                            ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
                          
                            txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();

                            txtktppenjamin.Text = "0";
                            txtnamapenjamin.Text = "-";                           
                            txtpendapatanperbulan.Text = "0";                            
                            txtjaminantambahan.Text = "-";
                            txtktppenjamin.Text= ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
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
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah untuk Permohonan tersebut sudah ada dan belum di proses');", true);

                    }


                }
                    else
                    {

                        no_rekening = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No.Rekening =  " + no_rekening + " belum Lunas, Mohon Lunasi terlebih dahulu');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah Belum Menjadi Anggota Koperasi Atau Tidak Aktif/Tutup!');", true);

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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLISTNASABAH", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "Idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void InsertCombo()
    {
        // --- Combo Jenis Kelamin
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("GetArInstansi", var, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "kd_instansi asc";

        //ddlkodeinstansi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlkodeinstansi.DataTextField = "instansi";
        //ddlkodeinstansi.DataValueField = "kd_instansi";
        //ddlkodeinstansi.DataBind();
        //ddlkodeinstansi.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Status Pernikahan
        ge.ListDataset = gb.GetDataFunction("dbo.GetStatusNikah()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddlstatuskawinupload.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlstatuskawinupload.DataTextField = "Keterangan";
        ddlstatuskawinupload.DataValueField = "id";
        ddlstatuskawinupload.DataBind();
        //ddlstatuskawin.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //// --- Combo Status Kesehatan
        //ge.ListDataset = gb.GetDataFunction("dbo.GetArKesehatan()", "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        //ddlkondisisehat.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //ddlkondisisehat.DataTextField = "Keterangan";
        //ddlkondisisehat.DataValueField = "id";
        //ddlkondisisehat.DataBind();
        //ddlkondisisehat.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo  Jenis Kredit
        ge.ListDataset = gb.GetDataFunction("dbo.GetProduk()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";

        ddlprodukpengajuan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlprodukpengajuan.DataTextField = "produk";
        ddlprodukpengajuan.DataValueField = "kode_produk";
        ddlprodukpengajuan.DataBind();
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

        // --- Combo Program Pinjaman
        ge.ListDataset = gb.GetDataFunction("dbo.GetFisikJaminan()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kd_fisik asc";

        ddlfisikjaminan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlfisikjaminan.DataTextField = "ket_fisik";
        ddlfisikjaminan.DataValueField = "kd_fisik";
        ddlfisikjaminan.DataBind();
        //ddlfisikjaminan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Golongan penjamin
        ge.ListDataset = gb.GetDataFunction("dbo.GetGolonganPenjamin()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_penjamin asc";

        ddlgolonganpenjamin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgolonganpenjamin.DataTextField = "keterangan";
        ddlgolonganpenjamin.DataValueField = "kode_penjamin";
        ddlgolonganpenjamin.DataBind();
        //ddlgolonganpenjamin.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Golongan Debitur
        ge.ListDataset = gb.GetDataFunction("dbo.GetGolonganDebitur()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_gol_debitur asc";

        ddlgolongandebitur.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgolongandebitur.DataTextField = "keterangan";
        ddlgolongandebitur.DataValueField = "kode_gol_debitur";
        ddlgolongandebitur.DataBind();
        //ddlgolongandebitur.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Golongan Sifat
        ge.ListDataset = gb.GetDataFunction("dbo.GetSifat()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_sifat asc";

        ddlSifat.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlSifat.DataTextField = "keterangan";
        ddlSifat.DataValueField = "kode_sifat";
        ddlSifat.DataBind();
        //ddlSifat.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Sektor Ekonomi
        ge.ListDataset = gb.GetDataFunction("dbo.GetSektorUsaha()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_sektor_usaha asc";

        ddlsektorekonomi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsektorekonomi.DataTextField = "keterangan";
        ddlsektorekonomi.DataValueField = "kode_sektor_usaha";
        ddlsektorekonomi.DataBind();
        //ddlsektorekonomi.Items.Insert(0, new ListItem("Pilih Data", "-"));

        // --- Combo Jenis Blokir
        ge.ListDataset = gb.GetDataFunction("dbo.GetJenisBlokir()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "id asc";

        ddljenisblokir.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljenisblokir.DataTextField = "keterangan";
        ddljenisblokir.DataValueField = "id";
        ddljenisblokir.DataBind();
        //ddljenisblokir.Items.Insert(0, new ListItem("Pilih Data", "-"));

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

        // Combo MO dan REF Non MO
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDNONSALES", typeof(string));
        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_MO", var2, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NIK asc";

        ddlnonao.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlnonao.DataTextField = "namaAO";
        ddlnonao.DataValueField = "NIK";
        ddlnonao.DataBind();
        ddlnonao.Items.Insert(0, new ListItem("Pilih Data", "-"));

       
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

    }

}