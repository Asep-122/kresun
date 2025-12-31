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



public partial class AR_Process_PencairanKreditSumberDana: System.Web.UI.Page
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
            //BindGridEntity();
          
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
            txttgltransfer.Text = xtanggal.ToString("dd-MM-yyyy");

        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

      

    }

    //protected void ddlbycad_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlbycad.SelectedValue == "1")
    //    {
    //        txtkalicadangan.Text = "1";
    //        txtkalicadangan.Enabled = true;
    //        txtkalicadangan_TextChanged(sender, e);

    //    }
    //    else if (ddlbycad.SelectedValue == "0")
    //    {
    //        txtkalicadangan.Enabled = false;
    //        TextBox3.Text = "0";
    //        txtkalicadangan.Text = "0";
    //        txtkalicadangan_TextChanged(sender, e);
    //    }
    //    else if (ddlbycad.SelectedValue == "2")
    //    {
    //        txtkalicadangan.Text = "1";
    //        txtkalicadangan.Enabled = true;
    //        txtkalicadangan_TextChanged(sender, e);

    //    }

    //}


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {


            //if (string.IsNullOrEmpty(ddlKdBank.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kode Bank terlebih dahulu');", true);
            //    ddlKdBank.Focus();
            //    return;
            //}
            //else if (string.IsNullOrEmpty(txtbuktiTransfer.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bukti Transfer Masih Kosong');", true);
            //    txtbuktiTransfer.Focus();
            //    return;
            //}
            //else
            //{

           

            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[18];
            var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            var88[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, Hdnkodecabang.Value, typeof(string));
            var88[3] = gb.CreateSPParameter("@idPermohonan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
            var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(string));
            var88[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 30, "KP", typeof(string));
            var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[7] = gb.CreateSPParameter("@AngsuranPerbulanSena", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuran.Text, typeof(decimal));
            var88[8] = gb.CreateSPParameter("@AngsuranPerbulanBank", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranperbulanbank.Text, typeof(string));
            var88[9] = gb.CreateSPParameter("@BungaSena", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtratebungapinjaman.Text), typeof(float));
            var88[10] = gb.CreateSPParameter("@BungaBank", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtbungabank.Text), typeof(float));
            var88[11] = gb.CreateSPParameter("@jangkasena", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
            var88[12] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
            var88[13] = gb.CreateSPParameter("@tgltransfer", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltransfer.Text, typeof(DateTime));
            var88[14] = gb.CreateSPParameter("@no_rekening_kredit", ParameterDirection.Input, SqlDbType.VarChar, 30, Hdnrekeningkredit.Value, typeof(string));
            var88[15] = gb.CreateSPParameter("@buktitransfer", ParameterDirection.Input, SqlDbType.VarChar, 30, txtbuktiTransfer.Text, typeof(string));
            var88[16] = gb.CreateSPParameter("@no_rekening_kredit_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, Hdnrekeningnasabah.Value, typeof(string));
            var88[17] = gb.CreateSPParameter("@selisih", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtselisihrp.Text), typeof(float));



                //CL_Trn_PROSESS_DOKUMENT_Cair_SumberDana
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_PROSESS_DOKUMENT_Cair_SumberDana", var88, "constrmf");
               // ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KP_Insert_Cair_Kresun_SumberDana", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
                pnladd.Visible = false;
                pnlIdentitas.Visible = false;
                PnlFront.Visible = true;

                //pnlPengajuan.Visible = false;
                PNLKELUARGA.Visible = false;
                btnSimpan.Visible = false;
                    PNLKARTU.Visible = false;


                    pnlAlasanTolak.Visible = false;

                Pnlpenerima.Visible = false;
                Pnlsimulasi.Visible = false;
                PnlSKPensiun.Visible = false;
                pnlpinjaman.Visible = false;

                pnlUpdAsuransi.Visible = false;
                btnCancel.Visible = false;


                ddlstatus_SelectedIndexChanged(sender, e);
                ddlkodeasuransi.SelectedValue = "-";
                txtalasantolak.Text = "";
                txtpremiasuransi.Text = "0";
                txtrateasuransi.Text = "0";
                txtnamaasuransi.Text = "";
                txtselisihasuransi.Text = "0";
                txtfocus.Focus();



            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));



            }
            //}

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }




    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {


        //Session.Contents.Add("Statusses", ddlstatus.SelectedValue);

        BindGridEntity();
       
       


    }



    //protected void ddlbiayaanggota_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    //var keteranganJaminan = txtketeranganJaminan.Text;
    //    //var kondisiJaminan = txtkondisiJaminan.Text;
    //    //decimal taksasi = 0;
    //    //taksasi =  decimal.Parse(txttaksasi.Text);

    //    if (ddlbiayaanggota.SelectedValue == "0")
    //    {
    //        if (decimal.Parse(txtsimpok.Text) == 0 )
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota tidak boleh Simpanan Pokok 0');", true);
    //            ddlbiayaanggota.SelectedValue = "1";
    //            LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("20000"));
    //        }
    //        else
    //        {
    //            LblAnggota.Text = string.Format("{0:N2}", decimal.Parse(""));
    //        }                  

    //    }
    //    else if (ddlbiayaanggota.SelectedValue == "1")
    //    {
    //        LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("20000"));
    //    }
    //    else if (ddlbiayaanggota.SelectedValue == "2")
    //    {

    //            LblAnggota.Text = string.Format("{0:N2}", decimal.Parse("0"));


    //    }




    //}


    //protected void ddluangmuka_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    //var keteranganJaminan = txtketeranganJaminan.Text;
    //    //var kondisiJaminan = txtkondisiJaminan.Text;
    //    //decimal taksasi = 0;
    //    //taksasi =  decimal.Parse(txttaksasi.Text);

    //    if (ddluangmuka.SelectedIndex == 0)
    //    {
    //        txtkaliuangmuka.Text = "0";
    //        txtuangmuka.Text = "0";
    //        txtkaliuangmuka.Enabled = false;
    //        //txtpokokuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));

    //    }
    //    else if (ddluangmuka.SelectedIndex == 1)
    //    {
    //        txtkaliuangmuka.Enabled = true;
    //        txtkaliuangmuka.Text = "1";
    //        //txtpokokuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));
    //        //txtuangmuka.Text = string.Format("{0:N2}", decimal.Parse(txtkaliuangmuka.Text) * decimal.Parse(txtangsuranperbulan.Text)); 



    //    }

    //    //txtketeranganJaminan.Text = keteranganJaminan;
    //    //txtkondisiJaminan.Text = kondisiJaminan;
    //    //txttaksasi.Text = string.Format("{0:N2}", taksasi);


    //}

    //protected void txtkaliuangmuka_TextChanged(object sender, EventArgs e)
    //{
    //    Decimal biayauangmuka = 0, totuangmuka = 0;
    //    //txtpokokuangmuka.Text= string.Format("{0:N2}", decimal.Parse(txtangsuranperbulan.Text));
    //    //biayauangmuka = decimal.Parse(txtkaliuangmuka.Text) * decimal.Parse(txtangsuranperbulan.Text);
    //    txtuangmuka.Text = string.Format("{0:N2}", biayauangmuka);

    //}

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
        //pnlFilter.Visible = true;
        //pnlJaminan.Visible = false;
        //pnlPengajuan.Visible = false;
      
        //txtnonasabah.Text = "";
        Pnlpenerima.Visible = false;
        Pnlsimulasi.Visible = false;
        PnlSKPensiun.Visible = false;
        pnlpinjaman.Visible = false;
        //PnlFooter.Visible = false;
        pnlUpdAsuransi.Visible = false;
        PNLKELUARGA.Visible = false;

        pnlUpdAsuransi.Visible = false;
        btnCancel.Visible = false;
        pnlAlasanTolak.Visible = false;
        PNLKARTU.Visible = false;
        txtfocus.Focus();


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
    //protected void btnBackIdentitas_Click(object sender, EventArgs e)
    //{
    //    pnladd.Visible = true;
    //    pnlIdentitas.Visible = false;
    //}
    protected void btnNexIdentitas_Click(object sender, EventArgs e)
    {
       
        pnlIdentitas.Visible = false;
        //pnlPekerjaan.Visible = true;
    }
    
    protected void btnNextPekerjaan_Click(object sender, EventArgs e)
    {
        //if (ddlkodeinstansi.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Instansi belum di pilih!');", true);

        //}
        //else
        //{
            //pnlPekerjaan.Visible = false;
            pnlIdentitas.Visible = false;
            //pnlTambahan.Visible = true;
        //}

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
     

     
    }
    protected void btnNextPendapatan_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
     
        //pnlLaba.Visible = true;

        pnladd.Visible = false;
      
    }
    protected void btnNextPihakLain_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
      


    }





    protected void btnbackawal_Click(object sender, EventArgs e)
    {
        //pnlFilter.Visible = true;
        PnlFront.Visible = true;
        pnladd.Visible = false;
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

    
    // protected void btnSimpan_Click(object sender, EventArgs e)
    // {
    //    //try
    //    //{
    //    //    decimal taksas=0;
    //    //    //taksas = decimal.Parse(txttaksasi.Text);
    //    //    int blokbulanan = 0;
    //    //    btnSimpan.Enabled = false;
    //    //    int datamingguan = 0;

    //    //    DateTime tgllahirx = Convert.ToDateTime(txttgllahirktp.Text);
    //    //    DateTime tgldrop = Convert.ToDateTime(txttglrealisasi.Text);
    //    //    if (tgllahirx >= tgldrop)
    //    //    {
    //    //        usia = "0";
    //    //    }
    //    //    else
    //    //    {
    //    //        usia = gb.HitungTahunUsia(txttgllahirktp.Text);
    //    //    }



    //    //    blokbulanan = 0;





    //    //    if (ddlprodukpengajuan.SelectedValue == "3B")
    //    //    {

    //    //        if (ddlrekening.SelectedValue == "-")
    //    //        {
    //    //            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[3];
    //    //            varz[0] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "BULANBLOCK", typeof(string));
    //    //            varz[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //    //            varz[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
    //    //            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

    //    //            blokbulanan = int.Parse(ge.ListDataset.Tables[0].Rows[0]["banyak"].ToString());
    //    //            if (blokbulanan >= 50)
    //    //            {
    //    //                blokbulanan = 1;
    //    //            }
    //    //            else
    //    //            {
    //    //                blokbulanan = 0;

    //    //            }
    //    //        }
    //    //        else if (ddlrekening.SelectedValue == "B")
    //    //        {
    //    //            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[3];
    //    //            varz[0] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "BULANBLOCK", typeof(string));
    //    //            varz[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //    //            varz[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
    //    //            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

    //    //            blokbulanan = int.Parse(ge.ListDataset.Tables[0].Rows[0]["banyak"].ToString());
    //    //            if (blokbulanan >= 50)
    //    //            {
    //    //                blokbulanan = 1;
    //    //            }
    //    //            else
    //    //            {
    //    //                blokbulanan = 0;

    //    //            }

    //    //        }


    //    //    }


    //    //    else if (ddlprodukpengajuan.SelectedValue == "3C")
    //    //    {
    //    //        if (ddljangka.SelectedValue=="11")
    //    //        {

    //    //            //if (ddlrekening.SelectedValue == "-" || ddlrekening.SelectedValue == "B")
    //    //            //{

    //    //            //    if (decimal.Parse(txtplafon.Text) == 750000)
    //    //            //    {
    //    //            //        blokbulanan = 2;
    //    //            //    }

    //    //            //}
    //    //            //else
    //    //            //{
    //    //                blokbulanan = 0;

    //    //            //}



    //    //        }


    //    //        else if (ddljangka.SelectedValue == "12")
    //    //        {


    //    //            blokbulanan = 0;


    //    //            if (ddlrekening.SelectedValue == "-" || ddlrekening.SelectedValue == "B")
    //    //            {

    //    //                if (decimal.Parse(txtplafon.Text) <= 2000000)
    //    //                {

    //    //                    blokbulanan = 0;

    //    //                }

    //    //                else
    //    //                {

    //    //                    blokbulanan = 3;


    //    //                }




    //    //            }
    //    //            else
    //    //            {

    //    //                blokbulanan = 0;

    //    //            }




    //    //            }
    //    //        else if (ddljangka.SelectedValue == "36")
    //    //        {

    //    //            //if (ddlrekening.SelectedValue == "-" || ddlrekening.SelectedValue == "B")
    //    //            //{

    //    //            //    if (decimal.Parse(txtplafon.Text) == 750000)
    //    //            //    {
    //    //            //        blokbulanan = 2;
    //    //            //    }

    //    //            //}
    //    //            //else
    //    //            //{
    //    //            blokbulanan = 5;

    //    //            //}



    //    //        }
    //    //        else if (ddljangka.SelectedValue == "48")
    //    //        {

    //    //            //if (ddlrekening.SelectedValue == "-" || ddlrekening.SelectedValue == "B")
    //    //            //{

    //    //            //    if (decimal.Parse(txtplafon.Text) == 750000)
    //    //            //    {
    //    //            //        blokbulanan = 2;
    //    //            //    }

    //    //            //}
    //    //            //else
    //    //            //{
    //    //            blokbulanan = 6;

    //    //            //}



    //    //        }


    //    //        else if (ddljangka.SelectedValue == "24")
    //    //        {

    //    //            if (ddlrekening.SelectedValue == "-" || ddlrekening.SelectedValue == "B")
    //    //            {

    //    //                if (decimal.Parse(txtplafon.Text) <= 2000000)
    //    //                {

    //    //                    blokbulanan = 0;

    //    //                }

    //    //                else
    //    //                {

    //    //                    blokbulanan = 3;


    //    //                }




    //    //            }
    //    //            else 
    //    //            {
    //    //                if (decimal.Parse(txtplafon.Text) > 3000000)
    //    //                {

    //    //                    if (decimal.Parse(txtplafon.Text) > 3000000)
    //    //                    { 

    //    //                        DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[4];
    //    //                    varz[0] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "MINGGUANBLOCK", typeof(string));
    //    //                    varz[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //    //                    varz[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
    //    //                    varz[3] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggota.Text, typeof(String));
    //    //                    ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

    //    //                    datamingguan = int.Parse(ge.ListDataset.Tables[0].Rows[0]["banyak"].ToString());
    //    //                    if (datamingguan >0)
    //    //                    {
    //    //                        blokbulanan = 0;
    //    //                    }
    //    //                    else
    //    //                    {
    //    //                        blokbulanan = 4;

    //    //                    }



    //    //                    }
    //    //                    else
    //    //                    {
    //    //                        blokbulanan = 4;

    //    //                    }


    //    //                }
    //    //                else
    //    //                {
    //    //                    blokbulanan = 0;
    //    //                }


    //    //            }



    //    //        }





    //    //    }


    //    //    if (blokbulanan == 1)
    //    //    {


    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon Maaf Anggota Aktif pada Produk Bulanan tidak boleh melebihi dari 50 anggota');", true);
    //    //        btnSimpan.Enabled = true;

    //    //    }
    //    //    else if (blokbulanan == 2)
    //    //    {


    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon Maaf Anggota Baru Produk Mingguan minimal Plafon 1000000');", true);
    //    //        btnSimpan.Enabled = true;

    //    //    }
    //    //    else if (blokbulanan == 3)
    //    //    {


    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon Maaf Anggota Baru Produk Mingguan  Jangka waktu 24/12 Minggu Plafon 2.500.000 keatas khusus Anggota Perpanjangan');", true);
    //    //        btnSimpan.Enabled = true;

    //    //    }
    //    //    else if (blokbulanan == 4)
    //    //    {


    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon Maaf Anggota perpanjangan Produk Mingguan  Jangka waktu 24 Minggu Minimala Plafon 2000000 dan maksimal 3000000');", true);
    //    //        btnSimpan.Enabled = true;

    //    //    }
    //    //    else if (blokbulanan == 5)
    //    //    {


    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon Maaf Tenor 36 Belum dibuka');", true);
    //    //        btnSimpan.Enabled = true;

    //    //    }

    //    //    else if (blokbulanan == 6)
    //    //    {


    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon Maaf Tenor 48 Belum dibuka');", true);
    //    //        btnSimpan.Enabled = true;

    //    //    }

    //    //    else
    //    //    {




    //    //    DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
    //    //    varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglrealisasi.Text, typeof(DateTime));
    //    //    varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
    //    //    varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //    //    varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
    //    //    varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
    //    //    ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

    //    //    string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
    //    //    string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

    //    //    if (xsf != "0")
    //    //    {
    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
    //    //        btnSimpan.Enabled = true;
    //    //    }
    //    //    else
    //    //    {


    //    //        if (ddlpengajuan.SelectedValue == "X" && ddlrekening.SelectedValue == "-")
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masukkan Rekening Lama Perpanjangan');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddlrekening.Focus();

    //    //        }

    //    //        else if (ddluangmuka.SelectedValue == "1" && (ddlbycad.SelectedValue == "0" && decimal.Parse(txtcadsebelumnya.Text) > 0))
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Lakukan Pengembalian Cadangan di Teller');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddlrekening.Focus();

    //    //        }
    //    //        else if (ddlbycad.SelectedValue == "0" && decimal.Parse(txtcadsebelumnya.Text) > 0)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cadangan Sebelumnya ada, tidak boleh pilih Jenis Biaya Cadangan Tanpa Cadangan');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddlbycad.Focus();

    //    //        }


    //    //        else if (ddlbycad.SelectedValue == "1" && txtkalicadangan.Text == "0")
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis Biaya Cadangan Dengan Cadangan Tidak Boleh Biaya Cadangan 0');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddlbycad.Focus();

    //    //        }
    //    //        else if (ddlbiayaanggota.SelectedValue == "0" && decimal.Parse(txtsimpok.Text) == 0)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Saldo  Simpok Kosong tidak boleh memiih type biaya keanggotaan dengan sudah menjadi anggota');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddlrekening.Focus();

    //    //        }
    //    //        else if (ddlkelompokAgunan.SelectedValue == "Z")
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kelompok Agunan Pilihan Agunan tidak boleh dipilih');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddlkelompokAgunan.Focus();
    //    //        }
    //    //        else if (ddljangka.SelectedValue == "-")
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jangka Belum di dipilih');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddljangka.Focus();
    //    //        }
    //    //        else if (txtkelurahanktp.Text.Length == 0)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kelurahan KTP masing kosong, lengkapi data anggota terlebih dahulu');", true);
    //    //            btnSimpan.Enabled = true;

    //    //        }
    //    //        else if (txtkecamatanktp.Text.Length == 0)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kecamatan KTP masing kosong, lengkapi data anggota terlebih dahulu');", true);
    //    //            btnSimpan.Enabled = true;

    //    //        }
    //    //        else if (txtkotaktp.Text.Length == 0)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kota KTP masing kosong, lengkapi data anggota terlebih dahulu');", true);
    //    //            btnSimpan.Enabled = true;

    //    //        }
    //    //        else if (txtketeranganJaminan.Text.Length == 0)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Agunan Masih Kosong');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            txtketeranganJaminan.Focus();
    //    //        }
    //    //        else if (taksas <= 0)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Taksasi Agunan Masih Kosong');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            txttaksasi.Focus();
    //    //        }
    //    //        else if (txttglrealisasi.Text == "")
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi Belum di dipilih');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            txttglrealisasi.Focus();
    //    //        }
    //    //        else if (ddljaminan.SelectedValue == "-")
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jaminan Surat Belum di dipilih');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            ddljaminan.Focus();
    //    //        }
    //    //        else if (txtktppenjamin.Text.Length < 16)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Ktp penjamin tidak boleh kurang dari 16');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            txtktppenjamin.Focus();
    //    //        }
    //    //        else if (txtnamapenjamin.Text.Length < 3)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama penjamin tidak boleh kosong');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            txtnamapenjamin.Focus();
    //    //        }
    //    //        else if (txttgllahirpasangan.Text.Trim() == "" && ddlstatuskawinupload.SelectedValue == "2")
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Lahir Pasangan belum di isi');", true);
    //    //            btnSimpan.Enabled = true;
    //    //            txttgllahirpasangan.Focus();
    //    //        }
    //    //        else if (int.Parse(usia) < 17)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Usia Pemohon Kurang dari 17 Tahun tidak boleh');", true);
    //    //            btnSimpan.Enabled = true;

    //    //        }
    //    //        else if (txtjenisusahaKtp.Text.Length < 3)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis Usaha Pemohon tolong di detailkan');", true);

    //    //            btnSimpan.Enabled = true;
    //    //            txtjenisusahaKtp.Focus();

    //    //        }
    //    //        else if (txtjenisusahapenjamin.Text.Length < 3)
    //    //        {
    //    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis Usaha Penjamin tolong di detailkan');", true);

    //    //            btnSimpan.Enabled = true;
    //    //            txtjenisusahapenjamin.Focus();

    //    //        }
    //    //        else
    //    //        {
    //    //            //DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];
    //    //            //varx[0] = gb.CreateSPParameter("@ktp_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtktppenjamin.Text, typeof(string));
    //    //            //varx[1] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //    //            //varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPENJAMIN", typeof(string));
    //    //            //varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //    //            //ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varx, "constrmf");

    //    //            //if (ge.ListDataset.Tables[0].DefaultView.Count > 0 )
    //    //            //{


    //    //            //    string nasabah = ge.ListDataset.Tables[0].Rows[0]["nasabah"].ToString();
    //    //            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Penjamin masih terdapat di Anggota yg belum lunas, No. Anggota =  " + nasabah + ", Mohon Lunasi terlebih dahulu');", true);
    //    //            //    txtktppenjamin.Focus();
    //    //            //}
    //    //            //else
    //    //            //{

    //    //            //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[1];
    //    //            //varT[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //    //            //ge.ListDataset = gb.GetDataJustOutPut("gettglsekarangXYZ", varT, "constrmf");
    //    //            //string xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString()).ToString("dd/MM/yyyy");
    //    //            //string tanggalrealisasi = DateTime.Parse(txttglrealisasi.Text).ToString("dd/MM/yyyy");


    //    //            //if (tanggalrealisasi != xtanggal)
    //    //            //{
    //    //            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Realisasi (Kartu Angsuran) Berbeda dengan Tanggal Sekarang, Proses Penyimpanan Dibatalkan');", true);
    //    //            //    btnSimpan.Enabled = true;
    //    //            //    txttglrealisasi.Focus();
    //    //            //}
    //    //            //else
    //    //            //{






    //    //            string siklus = "";
    //    //            siklus = "2";

    //    //            DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
    //    //            var4[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
    //    //            var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAAN", typeof(string));
    //    //            var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //    //            //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
    //    //            ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf");
    //    //            if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
    //    //            {
    //    //                if (ge.ListDataset.Tables[0].Rows[0]["flagsalah"].ToString() == "1")
    //    //                {


    //    //                    HDStatusDebitur.Value = "L";



    //    //                }
    //    //                else
    //    //                {


    //    //                    if (ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString() == "0")
    //    //                    {

    //    //                        HDStatusDebitur.Value = "B";

    //    //                    }
    //    //                    else
    //    //                    {
    //    //                        HDStatusDebitur.Value = "L";
    //    //                    }

    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                HDStatusDebitur.Value = "B";
    //    //            }



    //    //            nonasabah = txtnoanggota.Text;



    //    //            string datetgldrop = tgldrop.ToString("yyyy/MM/dd");

    //    //            DateTime tgljt = Convert.ToDateTime(txtjttempo.Text);
    //    //            string datetgljt = tgljt.ToString("yyyy/MM/dd");



    //    //            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[83];
    //    //            var1[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
    //    //            var1[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
    //    //            var1[2] = gb.CreateSPParameter("@nama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
    //    //            var1[3] = gb.CreateSPParameter("@no_registrasi", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
    //    //            var1[4] = gb.CreateSPParameter("@pekerjaan", ParameterDirection.Input, SqlDbType.VarChar, 100, ddlPekerjaan.SelectedValue, typeof(string));
    //    //            var1[5] = gb.CreateSPParameter("@angsuran_bulanan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtangsuranperbulan.Text, typeof(decimal));
    //    //            var1[6] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal));
    //    //            var1[7] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljangka.SelectedValue, typeof(string));
    //    //            var1[8] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.VarChar, 10, txtbunga.Text, typeof(string));
    //    //            var1[9] = gb.CreateSPParameter("@kdpersh1", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlsumberdana.SelectedValue, typeof(string));
    //    //            var1[10] = gb.CreateSPParameter("@biaya_provisi", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnominalprovisi.Text, typeof(decimal));
    //    //            var1[11] = gb.CreateSPParameter("@persen_prov", ParameterDirection.Input, SqlDbType.VarChar, 10, txtProvisi.Text, typeof(string));
    //    //            var1[12] = gb.CreateSPParameter("@meterai", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbymaterai.Text, typeof(decimal));
    //    //            var1[13] = gb.CreateSPParameter("@base_rate", ParameterDirection.Input, SqlDbType.VarChar, 10, txtbunga.Text, typeof(string));
    //    //            var1[14] = gb.CreateSPParameter("@kode_produk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprodukpengajuan.SelectedValue, typeof(string));
    //    //            var1[15] = gb.CreateSPParameter("@jns_pinjaman", ParameterDirection.Input, SqlDbType.VarChar, 10, HDJenisPinjaman.Value, typeof(string));
    //    //            var1[16] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
    //    //            var1[17] = gb.CreateSPParameter("@usia", ParameterDirection.Input, SqlDbType.VarChar, 10, usia, typeof(decimal));
    //    //            var1[18] = gb.CreateSPParameter("@tgl_drop", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, datetgldrop, typeof(DateTime));
    //    //            var1[19] = gb.CreateSPParameter("@tgl_jt", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, datetgljt, typeof(DateTime));
    //    //            var1[20] = gb.CreateSPParameter("@filepathjaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
    //    //            var1[21] = gb.CreateSPParameter("@jaminan", ParameterDirection.Input, SqlDbType.VarChar, 250, txtketeranganJaminan.Text, typeof(string));
    //    //            var1[22] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
    //    //            var1[23] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
    //    //            var1[24] = gb.CreateSPParameter("@usrupd", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
    //    //            var1[25] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
    //    //            var1[26] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //    //            var1[27] = gb.CreateSPParameter("@ktp_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 30, txtktppenjamin.Text, typeof(string));
    //    //            var1[28] = gb.CreateSPParameter("@nama_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamapenjamin.Text, typeof(string));
    //    //            var1[29] = gb.CreateSPParameter("@file_pathktppasangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
    //    //            var1[30] = gb.CreateSPParameter("@nilai_taksasi", ParameterDirection.Input, SqlDbType.Decimal, 0, txttaksasi.Text, typeof(decimal));
    //    //            var1[31] = gb.CreateSPParameter("@jaminan_kondisi", ParameterDirection.Input, SqlDbType.VarChar, 250, txtkondisiJaminan.Text, typeof(string));
    //    //            var1[32] = gb.CreateSPParameter("@biaya_cadangan", ParameterDirection.Input, SqlDbType.Decimal, 0, TextBox3.Text, typeof(decimal));
    //    //            var1[33] = gb.CreateSPParameter("@status_dalam_keluarga", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatuskawinupload.SelectedValue, typeof(string));
    //    //            var1[34] = gb.CreateSPParameter("@Siklus", ParameterDirection.Input, SqlDbType.VarChar, 10, siklus, typeof(string));
    //    //            var1[35] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamaibukandung.Text, typeof(string));
    //    //            var1[36] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNPWP.Text, typeof(string));
    //    //            var1[37] = gb.CreateSPParameter("@no_ktp_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpasangan.Text, typeof(string));
    //    //            var1[38] = gb.CreateSPParameter("@nama_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamapasangan.Text, typeof(string));
    //    //            var1[39] = gb.CreateSPParameter("@tempat_lahir_pasangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txttempatlahirpasangan.Text, typeof(string));
    //    //            var1[40] = gb.CreateSPParameter("@tanggal_lahir_pasangan", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahirpasangan.Text, typeof(DateTime));
    //    //            var1[41] = gb.CreateSPParameter("@no_kk", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnomerkk.Text, typeof(string));
    //    //            var1[42] = gb.CreateSPParameter("@file_pathKK", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
    //    //            var1[43] = gb.CreateSPParameter("@file_pathnpwp", ParameterDirection.Input, SqlDbType.VarChar, 250, "", typeof(string));
    //    //            var1[44] = gb.CreateSPParameter("@jumlah_anak", ParameterDirection.Input, SqlDbType.VarChar, 5, txtjumlahanak.Text, typeof(string));
    //    //            var1[45] = gb.CreateSPParameter("@status_rumah", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusrumah.SelectedValue, typeof(string));
    //    //            var1[46] = gb.CreateSPParameter("@alamat_rumah_tinggal1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamatrumahtinggal1.Text, typeof(string));
    //    //            var1[47] = gb.CreateSPParameter("@RT", ParameterDirection.Input, SqlDbType.VarChar, 50, txtrttp.Text, typeof(string));
    //    //            var1[48] = gb.CreateSPParameter("@RW", ParameterDirection.Input, SqlDbType.VarChar, 50, txtrwtpt.Text, typeof(string));
    //    //            var1[49] = gb.CreateSPParameter("@provinsiTempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlprovinsitpt.SelectedValue, typeof(string));
    //    //            var1[50] = gb.CreateSPParameter("@kodeposTempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodepostpt.Text, typeof(string));
    //    //            var1[51] = gb.CreateSPParameter("@kelurahan_TempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkelurahantpt.Text, typeof(string));
    //    //            var1[52] = gb.CreateSPParameter("@kecamatan_TempatTinggal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkecamatantpt.Text, typeof(string));
    //    //            var1[53] = gb.CreateSPParameter("@majuhari", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlmajuhari.SelectedValue), typeof(int));
    //    //            var1[54] = gb.CreateSPParameter("@Jaminansurat", ParameterDirection.Input, SqlDbType.VarChar, 80, ddljaminan.SelectedValue, typeof(string));
    //    //            var1[55] = gb.CreateSPParameter("@status_debitur", ParameterDirection.Input, SqlDbType.VarChar, 1, HDStatusDebitur.Value, typeof(string));
    //    //            var1[56] = gb.CreateSPParameter("@biayaanggota", ParameterDirection.Input, SqlDbType.Decimal, 0, LblAnggota.Text, typeof(decimal));
    //    //            var1[57] = gb.CreateSPParameter("@qtyuangmuka", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtkaliuangmuka.Text), typeof(int));
    //    //            var1[58] = gb.CreateSPParameter("@biayauangmuka", ParameterDirection.Input, SqlDbType.Decimal, 0, txtuangmuka.Text, typeof(decimal));
    //    //            var1[59] = gb.CreateSPParameter("@cadangansebelumnya", ParameterDirection.Input, SqlDbType.Decimal, 0, txtcadsebelumnya.Text, typeof(decimal));
    //    //            var1[60] = gb.CreateSPParameter("@norekeningperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrekening.SelectedValue, typeof(string));
    //    //            var1[61] = gb.CreateSPParameter("@tipeperpanjangan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlpengajuan.SelectedValue, typeof(string));
    //    //            var1[62] = gb.CreateSPParameter("@cadangankembali", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkembalicadangan.Text, typeof(decimal));
    //    //            var1[63] = gb.CreateSPParameter("@qtycadangan", ParameterDirection.Input, SqlDbType.Int, 0, txtkalicadangan.Text, typeof(int));
    //    //            var1[64] = gb.CreateSPParameter("@pilihcadangan", ParameterDirection.Input, SqlDbType.Int, 0, ddlbycad.SelectedValue, typeof(int));
    //    //            var1[65] = gb.CreateSPParameter("@pilihuangmuka", ParameterDirection.Input, SqlDbType.Int, 0, ddluangmuka.SelectedValue, typeof(int));
    //    //            var1[66] = gb.CreateSPParameter("@idpermohonan", ParameterDirection.Input, SqlDbType.VarChar, 30, HDRegister.Value, typeof(string));
    //    //            var1[67] = gb.CreateSPParameter("@infomaterai", ParameterDirection.Input, SqlDbType.VarChar, 80, txtinfox.Text, typeof(string));
    //    //            var1[68] = gb.CreateSPParameter("@biayasimpananwajib", ParameterDirection.Input, SqlDbType.Decimal, 0, lblsimpananwajib.Text, typeof(decimal));
    //    //            var1[69] = gb.CreateSPParameter("@jenistagihan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenistagihan.SelectedValue, typeof(string));
    //    //            var1[70] = gb.CreateSPParameter("@Typebiayakeanggotaan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlbiayaanggota.SelectedValue, typeof(string));
    //    //            var1[71] = gb.CreateSPParameter("@Rekeninglamacadangan", ParameterDirection.Input, SqlDbType.VarChar, 30, txtcadanganRekening.Text, typeof(string));
    //    //            var1[72] = gb.CreateSPParameter("@tgltagihhari", ParameterDirection.Input, SqlDbType.VarChar, 40, txttgltagih.Text, typeof(string));
    //    //            var1[73] = gb.CreateSPParameter("@discountangsuran", ParameterDirection.Input, SqlDbType.Decimal, 0, txtdiscountangsuran.Text, typeof(decimal));
    //    //            var1[74] = gb.CreateSPParameter("@pekerjaan_penjamin", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlPekerjaan.SelectedValue, typeof(string));
    //    //            var1[75] = gb.CreateSPParameter("@sektorusaha_Penjamin", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlsektorusahapenjamin.SelectedValue, typeof(string));
    //    //            var1[76] = gb.CreateSPParameter("@jenisusaha_Penjamin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenisusahapenjamin.Text, typeof(string));
    //    //            var1[77] = gb.CreateSPParameter("@jenispermohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenispermohonan.SelectedValue, typeof(string));
    //    //            var1[78] = gb.CreateSPParameter("@kelompokagunan", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlkelompokAgunan.SelectedValue, typeof(string));
    //    //            var1[79] = gb.CreateSPParameter("@tujuanpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltujuanpinjaman.SelectedValue, typeof(string));
    //    //            var1[80] = gb.CreateSPParameter("@pekerjaanX", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlpekerjaanktp.SelectedValue, typeof(string));
    //    //            var1[81] = gb.CreateSPParameter("@sektorusaha", ParameterDirection.Input, SqlDbType.VarChar, 4, ddlSektorUsahaKtp.SelectedValue, typeof(string));
    //    //            var1[82] = gb.CreateSPParameter("@jenisusaha", ParameterDirection.Input, SqlDbType.VarChar, 50, txtjenisusahaKtp.Text, typeof(string));

    //    //            ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var1, "constrmf");





    //    //            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //    //            {
    //    //                //btnSimpan.Visible = false;
    //    //                //btnSave.Visible = false;
    //    //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);
    //    //                //txtnonasabah.Focus();
    //    //                pnladd.Visible = false;
    //    //                pnlIdentitas.Visible = false;
    //    //                PnlFront.Visible = true;
    //    //                pnlFilter.Visible = true;
    //    //                pnlJaminan.Visible = false;
    //    //                pnlPengajuan.Visible = false;
    //    //                btnSimpan.Visible = false;
    //    //                BtnApproveAntrian.Visible = false;
    //    //                BtnBatalkanmohon.Visible = false;
    //    //                BtnApproveAntrian.Visible = false;
    //    //                BtnBatalkanmohon.Visible = false;
    //    //                btnlanjutkan.Visible = false;
    //    //                btnTolakAntrian.Visible = false;
    //    //                Pnlupdatedata.Visible = false;
    //    //                pnlAlasanTolak.Visible = false;
    //    //                txtnonasabah.Text = "";
    //    //                txtketeranganJaminan.Text = "";
    //    //                txtkondisiJaminan.Text = "-";
    //    //                txttaksasi.Text = "0";
    //    //                txtktppenjamin.Text = "0";
    //    //                txtnamapenjamin.Text = "-";
    //    //                txtnamaibukandung.Text = "-";
    //    //                txtNPWP.Text = "0";
    //    //                txtnomerkk.Text = "0";
    //    //                txtcadsebelumnya.Text = "0";
    //    //                txtkembalicadangan.Text = "0";
    //    //                txtuangmuka.Text = "0";
    //    //                txtkaliuangmuka.Text = "0";
    //    //                TextBox2.Text = "0";
    //    //                txtcadanganRekening.Text = "";
    //    //                txtjenisusahapenjamin.Text = "";
    //    //                ddljangka.SelectedValue = "-";
    //    //                ddljenispermohonan.SelectedValue = "A";
    //    //                ddlstatus_SelectedIndexChanged(sender, e);
    //    //                ddlpengajuan.SelectedValue = "B";
    //    //                ddljenispermohonan_SelectedIndexChanged(sender, e);
    //    //                ddlrekening.SelectedValue = "-";
    //    //                txtnonasabah.Focus();

    //    //            }
    //    //            else
    //    //            {

    //    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //    //                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    //                btnSimpan.Enabled = true;
    //    //            }



    //    //            }










    //    //        //}


    //    //    }

    //    //    }
    //    //}


    //    //catch (Exception X)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
    //    //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //    //}


    //}

    protected string btnafterDelete()
    {
        
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermohonanPinjamanKreditMultigunaNew.aspx");
        //ddlcabang.Focus();
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
    //    try
    //    {


    //        btnSimpan.Enabled = true;
    //        string no_rekening = "";

    //        HDRegister.Value = "";
    //        string[] splitdata = txtnonasabah.Text.Split('-');
    //        string nonasabah = splitdata[0];
    //        string KTP = "";
    //        string KOPERASI = "";
    //        string WOSTATUS = "0";
    //        string KODEAO = "";
    //        string DATAKODEAOADA = "0";
    //        string flag_bayar = "0";
    //        int flagdata = 0;
    //        int Setoranx = 0;
    //        //ddlpengajuan.Enabled = false;
    //        //ddlrekening.Enabled = false;
    //        //ddlbiayaanggota.Enabled = false;
    //        //txtangsuranperbulan.Text = string.Format("{0:N0}", "0");
    //        //txtplafon.Text = string.Format("{0:N0}", "750000");
    //        ////ddlrekening.Items.Insert(0, new ListItem("Pilih Rekening ", "-"));
    //        //ddlbiayaanggota.Enabled = false;
    //        //txtcadanganRekening.Enabled = false;
    //        //txttgltagih.Enabled = false;



      
    //        txttglrealisasi.Enabled = true;
    //        //ddlprodukpengajuan.Enabled = true;
    //        //ddljaminan.Enabled = true;
    //        //txtketeranganJaminan.Enabled = true;
    //        //ddljangka.Enabled = true;
    //        //btntambah.Enabled = true;
    //        //btnminus.Enabled = true;
    //        btnSimpan.Enabled = true;
    //        //btncekcadangan.Enabled = true;
    //        //ddljenistagihan.Enabled = true;
    //        txtplafon.Text = string.Format("{0:N0}", "0");
    //        txtidpermohonan.Text = "";

    //        //ddljenispermohonan.SelectedValue = "A";


    //        pnlpinjaman.Visible = true;
    //        Pnlpenerima.Visible = true;
    //        Pnlsimulasi.Visible = true;
    //        PnlSKPensiun.Visible = true;
    //        pnlpinjaman.Visible = true;

    //        pnlUpdAsuransi.Visible = false;
    //        btnCancel.Visible = true;
    //        btndokumen.Visible = true;

    //        ddldatasimulasi.SelectedValue = "B";
    //        //ddllistsimulasi.SelectedValue = "-";
    //        ddldatasimulasi_SelectedIndexChanged(sender, e);
    //        //ddlmajuhari_SelectedIndexChanged(sender, e);

    //        if (txtnonasabah.Text == "")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Anggota belum di isi');", true);
    //            txtnonasabah.Focus();
    //        }
    //        else if (ddlcabang.SelectedValue != txtnonasabah.Text.Substring(0, 3))
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Anggota yang di input tidak terdapat di cabang Anda');", true);

    //        }

    //        else
    //        {


    //            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
    //            var3[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
    //            var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKEANGGOTAAN", typeof(string));
    //            var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3, "constrmf");

    //            if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
    //            {

    //                KTP = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
    //                KOPERASI = ge.ListDataset.Tables[0].Rows[0]["kode_koperasi"].ToString();
    //                WOSTATUS = ge.ListDataset.Tables[0].Rows[0]["status_wo"].ToString();
    //                KODEAO = ge.ListDataset.Tables[0].Rows[0]["kode_ref"].ToString();
    //                flagdata= int.Parse(ge.ListDataset.Tables[0].Rows[0]["flagdata"].ToString());
    //                Setoranx = int.Parse(ge.ListDataset.Tables[0].Rows[0]["Setoranx"].ToString()); 
    //                flag_bayar =ge.ListDataset.Tables[0].Rows[0]["flag_bayar"].ToString();
    //                if (flag_bayar == "0")
    //                {
    //                    flagdata = 1;
    //                }
    //                if (Setoranx == 1)
    //                {
    //                    flagdata = 1;
    //                }

    //                if (flagdata > 0)
    //                {


    //                    DefaultClass.Global.SqlHelper[] var2x = new DefaultClass.Global.SqlHelper[5];
    //                    var2x[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
    //                    var2x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKPERMOHONANADA", typeof(string));
    //                    var2x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //                    var2x[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //                    var2x[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //                    ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", var2x, "constrmf");

    //                if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
    //                {

    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota untuk Permohonan tersebut sudah ada dan belum di proses');", true);

    //                    txtnonasabah.Focus();
    //                }
    //                else
    //                {


    //                    DefaultClass.Global.SqlHelper[] var3x = new DefaultClass.Global.SqlHelper[5];
    //                    var3x[0] = gb.CreateSPParameter("@kode_AO", ParameterDirection.Input, SqlDbType.VarChar, 50, KODEAO, typeof(string));
    //                    var3x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKAOAKTIF", typeof(string));
    //                    var3x[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //                    var3x[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //                    var3x[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //                    ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var3x, "constrmf");

    //                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
    //                    {
    //                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode AO tidak ada dalam list AO yang aktif,rubah data anggota di register calon anggota');", true);

    //                        //txtnonasabah.Focus();
    //                        DATAKODEAOADA = "0";
    //                    }

    //                    else
    //                    {
    //                        DATAKODEAOADA = "1";
    //                    }

    //                    //else
    //                    //{

    //                        DefaultClass.Global.SqlHelper[] varkop1 = new DefaultClass.Global.SqlHelper[7];
    //                        varkop1[0] = gb.CreateSPParameter("@no_ktp", ParameterDirection.Input, SqlDbType.VarChar, 50, KTP, typeof(string));
    //                        varkop1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLUNASKTPKONSOL", typeof(string));
    //                        varkop1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //                        varkop1[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, "I", typeof(string));
    //                        varkop1[4] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //                        varkop1[5] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 30, nonasabah, typeof(string));
    //                        varkop1[6] = gb.CreateSPParameter("@kd_reff2", ParameterDirection.Input, SqlDbType.VarChar, 30, KODEAO, typeof(string));


    //                         ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varkop1, "constrmf");
    //                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
    //                        {
    //                            pnladd.Visible = true;
    //                            pnlIdentitas.Visible = true;
    //                            PnlFront.Visible = false;
    //                            //pnlFilter.Visible = false;
    //                            //pnlJaminan.Visible = true;
    //                            //pnlPengajuan.Visible = true;
    //                            btnSimpan.Visible = true;
    //                            BtnApproveAntrian.Visible = false;
    //                            BtnBatalkanmohon.Visible = false;
    //                            BtnApproveAntrian.Visible = false;
    //                            BtnBatalkanmohon.Visible = false;
    //                            btnlanjutkan.Visible = false;
    //                            btnTolakAntrian.Visible = false;
    //                            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
    //                            var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah, typeof(string));
    //                            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORPERMOHONAN", typeof(string));
    //                            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //                            ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");
    //                            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
    //                            {
    //                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);
    //                                txtnonasabah.Focus();
    //                            }
    //                            else
    //                            {
    //                                btnSimpan.Visible = true;
    //                                txtanggotacbs.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah_cbs"].ToString();
    //                                txtnoanggota.Text = ge.ListDataset.Tables[0].Rows[0]["no_anggota"].ToString();
    //                                txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
    //                                txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
    //                                txtalamatktp.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_ktp1"].ToString();
    //                                txtkelurahanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan_ktp"].ToString();
    //                                txtkodeposktp.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos_ktp"].ToString();
    //                                ddlpropinsiktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi_ktp"].ToString();
    //                                txtkecamatanktp.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan_ktp"].ToString();
    //                                txtkotaktp.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
    //                                txttempatlahirktp.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir"].ToString();
    //                                txttgllahirktp.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir"].ToString();
    //                                txtnoidentitasktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_identitas"].ToString();
    //                                txtmasaberakhirKTP.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();                           
    //                                ddlAgamaktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["agama"].ToString();
    //                                txthpktp.Text = ge.ListDataset.Tables[0].Rows[0]["no_hp"].ToString();
    //                                txtemailsktp.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
    //                                ddlpekerjaanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();
    //                                ddlSektorUsahaKtp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha"].ToString();
    //                                txtjenisusahaKtp.Text= ge.ListDataset.Tables[0].Rows[0]["jenisusaha"].ToString();


    //                                //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan_penjamin"].ToString();
    //                                //ddlsektorusahapenjamin.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["sektorusaha_penjamin"].ToString();
    //                                //txtjenisusahapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["jenisusaha_penjamin"].ToString();

    //                                ddlstatusnikahktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_pernikahan"].ToString();
    //                                txtrtktp.Text = ge.ListDataset.Tables[0].Rows[0]["RT_ktp"].ToString();
    //                                txtrwktp.Text = ge.ListDataset.Tables[0].Rows[0]["RW_ktp"].ToString();
    //                                //txtsimpok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
    //                                //txtsimwa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

    //                                txtnorektabungan.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
    //                                txttglbergabung.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
    //                                ddlkewarganegaraanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kewarganegaraan"].ToString();
                                    
    //                                ddlpendidikanktp.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pendidikan"].ToString();
    //                                //data ktp

    //                                //data tempat tinggal
    //                                txtalamatrumahtinggal1.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
    //                                txtrttp.Text = ge.ListDataset.Tables[0].Rows[0]["RT"].ToString();
    //                                txtrwtpt.Text = ge.ListDataset.Tables[0].Rows[0]["RW"].ToString();
    //                                ddlstatusrumah.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Status_rumah"].ToString();
    //                                txtkelurahantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kelurahan"].ToString();
    //                                txtkecamatantpt.Text = ge.ListDataset.Tables[0].Rows[0]["kecamatan"].ToString();
    //                                txtkodepostpt.Text = ge.ListDataset.Tables[0].Rows[0]["kodepos"].ToString();
    //                                ddlprovinsitpt.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["provinsi"].ToString();
    //                                if (DATAKODEAOADA == "1")
    //                                {
    //                                ddlAO.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kode_AO"].ToString();
    //                                }
                                
    //                                HDKodePos.Value = txtkodeposktp.Text;
    //                                //HDKodeArea.Value = ge.ListDataset.Tables[0].Rows[0]["kode_area"].ToString();
    //                                HDGender.Value = ge.ListDataset.Tables[0].Rows[0]["jenis_kelamin"].ToString();
    //                                ddljenkelktp.SelectedValue = HDGender.Value;

    //                                if (ddlstatusrumah.SelectedValue == "8")
    //                                {




    //                                    txtalamatrumahtinggal1.Enabled = false;
    //                                    txtrttp.Enabled = false;
    //                                    txtrwtpt.Enabled = false;
    //                                    txtkelurahantpt.Enabled = false;
    //                                    txtkecamatantpt.Enabled = false;
    //                                    ddlprovinsitpt.Enabled = false;
    //                                    txtkodepostpt.Enabled = false;


    //                                }
    //                                else
    //                                {

    //                                    txtalamatrumahtinggal1.Enabled = true;
    //                                    txtrttp.Enabled = true;
    //                                    txtrwtpt.Enabled = true;
    //                                    txtkelurahantpt.Enabled = true;
    //                                    txtkecamatantpt.Enabled = true;
    //                                    ddlprovinsitpt.Enabled = true;
    //                                    txtkodepostpt.Enabled = true;
    //                                }


    //                                //data upload
    //                                ddlstatuskawinupload.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["status_dalam_keluarga"].ToString();
    //                                if (ddlstatuskawinupload.SelectedIndex == 0)
    //                                {
    //                                    txtidpasangan.Enabled = false;
    //                                    txtidpasangan.Text = "-";
    //                                    txtnamapasangan.Enabled = false;
    //                                    txtnamapasangan.Text = "-";
    //                                    txttempatlahirpasangan.Enabled = false;
    //                                    txttempatlahirpasangan.Text = "-";
    //                                    txttgllahirpasangan.Enabled = false;
    //                                    txttgllahirpasangan.Text = "1900-01-01 00:00:00";


    //                                }
    //                                else
    //                                {

    //                                    txtidpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["no_ktp_pasangan"].ToString();
    //                                    txtnamapasangan.Text = ge.ListDataset.Tables[0].Rows[0]["nama_pasangan"].ToString();
    //                                    txttempatlahirpasangan.Text = ge.ListDataset.Tables[0].Rows[0]["tempat_lahir_pasangan"].ToString();
    //                                    txttgllahirpasangan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString())); //ge.ListDataset.Tables[0].Rows[0]["tanggal_lahir_pasangan"].ToString();

    //                                }

    //                                imgktp.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_pathktp"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
    //                                                                                                              //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

    //                                //txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["no_npwp"].ToString();

    //                                //txtnomerkk.Text = ge.ListDataset.Tables[0].Rows[0]["no_kk"].ToString();

                                 


    //                                //ddlPekerjaan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pekerjaan"].ToString();

    //                                //txtnamaibukandung.Text = ge.ListDataset.Tables[0].Rows[0]["nama_ibu_kandung"].ToString();

    //                                //txtktppenjamin.Text = "0";
    //                                //txtnamapenjamin.Text = "-";
    //                                //txtpendapatanperbulan.Text = "0";
    //                                //txtjaminantambahan.Text = "-";
    //                                //txtktppenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["ktp_penjamin"].ToString();
    //                                //txtnamapenjamin.Text = ge.ListDataset.Tables[0].Rows[0]["nama_penjamin"].ToString();

                               
    //                                DefaultClass.Global.SqlHelper[] varcad = new DefaultClass.Global.SqlHelper[3];
    //                                varcad[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah.Trim(), typeof(string));
    //                                varcad[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKCADANGAN", typeof(string));
    //                                varcad[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //                                ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varcad, "constrmf");

    //                                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
    //                                {
    //                                    //txtcadanganRekening.Text = "";
    //                                    //txtcadsebelumnya.Text = "0";
    //                                }
    //                                else
    //                                {


    //                                    //txtcadanganRekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
    //                                    //txtcadsebelumnya.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["sisacadangan"].ToString());
    //                                    //txtcadsebelumnya.Text = txtcadsebelumnya.Text.Replace("Rp", "");

    //                                }




    //                                  ddlstatusrumah_SelectedIndexChanged(sender, e);


    //                                var notifcatatatan = "";

    //                                DefaultClass.Global.SqlHelper[] varnotif = new DefaultClass.Global.SqlHelper[3];
    //                                varnotif[0] = gb.CreateSPParameter("@nip", ParameterDirection.Input, SqlDbType.VarChar, 50, nonasabah.Trim(), typeof(string));
    //                                varnotif[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKNOTIFCATAT", typeof(string));
    //                                varnotif[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

    //                                ge.ListDataset = gb.GetDataJustOutPut("AR_PERMOHONAN_KREDIT", varnotif, "constrmf");

    //                                if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
    //                                {
    //                                    notifcatatatan = ge.ListDataset.Tables[0].Rows[0]["alasan_ttp"].ToString();
    //                                    if (notifcatatatan.Length > 5)
    //                                    {
    //                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Notifikasi Anggota Tolong Dibaca... " + notifcatatatan + " ');", true);

    //                                    }
                                       
    //                                }
                                   




    //                            }


    //                        }

    //                        else
    //                        {
    //                            no_rekening = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();

    //                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + no_rekening + " belum Lunas, Mohon Lunasi terlebih dahulu');", true);
    //                            txtnonasabah.Focus();
    //                        }

    //                    //}
    //                }
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota tersebut adalah Anggota Full Up yang tidak ada transaksi pinjaman, lakukan Perubahan menjadi Anggota Baru /Calon Anggota process Full UP ');", true);
    //                    txtnonasabah.Focus();
    //                }




    //            }

    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tersebut Belum Menjadi Anggota Koperasi Atau Tidak Aktif/ sudah di Tutup! ');", true);
    //                txtnonasabah.Focus();
    //            }




    //        }

    //    }
         

    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
    //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //    }
       
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




    public void BindGridEntity()
    {


            DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[6];
        x[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        x[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWVERIFIKASISD", typeof(string));
        x[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        x[4] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 4, '6', typeof(string));
        x[5] = gb.CreateSPParameter("@tolak", ParameterDirection.Input, SqlDbType.Int,0,0, typeof(int));
        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

      

    }


    //protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    //{
    //    //ProsesVerifikasi("2B", SessLoginID);
    //    try
    //    {

    //    }

    //    catch (Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //    }
    //}


    private void ClearData()

    {

        try
        {


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
            //ddlstatusnikah.SelectedValue = "-";
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

    protected void bindGrdkartu(object sender, EventArgs e)
    {
        try
        {

        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

        var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdnrekeningkredit.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKARTUANGSURANSD", typeof(string));
            //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "angs_ke asc";


        Grdkartu.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdkartu.DataBind();

        Grdkartu.UseAccessibleHeader = true;

        Grdkartu.HeaderRow.TableSection = TableRowSection.TableHeader;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
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



            case "SET":

                try

                {

                    ClearData();
                    blockdata();


                    string DATAKODEAOADA = "";
                    string kodematerai = "";
                    string AngsuranDimukaAsuransi = "";
                    string kodecabangx = "";
                    //string register
                    HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;

                    HDRegister.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    kodecabangx = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    Hdnkodecabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    Hdnrekeningkredit.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    Hdnrekeningnasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekeningnasabah")).Text;
                    // Combo MO dan REF Non MO
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, Hdnkodecabang.Value, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_MO", var1, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "NIK asc";

                    ddlAO.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlAO.DataTextField = "namaAO";
                    ddlAO.DataValueField = "NIK";
                    ddlAO.DataBind();

                    txtidpermohonan.Text = HDRegister.Value;

                    pnladd.Visible = true;
                    pnlIdentitas.Visible = false;
                    PnlFront.Visible = false;

                    //pnlJaminan.Visible = true;
                    //pnlPengajuan.Visible = true;

                    pnlpinjaman.Visible = false;
                    Pnlpenerima.Visible = false;
                    Pnlsimulasi.Visible = false;
                    PnlSKPensiun.Visible = false;
                    pnlpinjaman.Visible = true;
                    pnlUpdAsuransi.Visible = false;
                    pnlAlasanTolak.Visible = true;
                    PNLKELUARGA.Visible = false;
                    //PnlFooter.Visible = true;
                    btnCancel.Visible = true;
                    PNLKARTU.Visible = true;
                    Button2.Visible = false;

                    //ddldatasimulasi.SelectedValue = "B";
                    //ddllistsimulasi.SelectedValue = "-";
                    //ddldatasimulasi_SelectedIndexChanged(sender, e);
                    //ddlmajuhari_SelectedIndexChanged(sender, e);

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoNasabah.Value, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CAIRSD", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, kodecabangx, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var[5] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, HDRegister.Value, typeof(string));
                    
                    ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak di temukan');", true);

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
                        
                        
                        txtprovisitfpsn.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["psnprovisisd"].ToString()));
                        txtadminbanktfpsn.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["adminbankpersensd"].ToString()));

                        txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranblokirkali"].ToString()));
                        txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaangsurankali"].ToString()));
                        txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsurandimukaasuransikali"].ToString()));

                        txttgljatuhtempo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_Jatuh_Tempo"].ToString()));// ge.ListDataset.Tables[0].Rows[0]["berlaku_sampai_dengan"].ToString();    

                        txtfoto1.Text = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();
                        txtfoto2.Text = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();
                        txtfoto3.Text = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();
                        txtfoto4.Text = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();
                        txtfoto5.Text = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();
                        txtfilename1.Text = ge.ListDataset.Tables[0].Rows[0]["file1"].ToString();
                        txtfilename2.Text = ge.ListDataset.Tables[0].Rows[0]["file2"].ToString();
                        txtfilename3.Text = ge.ListDataset.Tables[0].Rows[0]["file3"].ToString();
                        txtfilename4.Text = ge.ListDataset.Tables[0].Rows[0]["file4"].ToString();
                        txtfilename5.Text = ge.ListDataset.Tables[0].Rows[0]["file5"].ToString();
                        imgfoto1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                      //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                        imgfoto2.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                      //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();

                        imgfoto3.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                      //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                        imgfoto4.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path4"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();
                                                                                                      //txtppathktppasangan.Text = ge.ListDataset.Tables[0].Rows[0]["file_pathktppasangan"].ToString();
                        imgfoto5.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["file_path5"].ToString();// + "?r=" + DateTime.Now.Ticks.ToString();


                        string folderPath = ge.ListDataset.Tables[0].Rows[0]["file_path_sumberdana"].ToString();   

                        string encrypted = gb.Encrypt(folderPath);

                        string secureUrl = ResolveUrl("~/SecurePdf.ashx?f=" + encrypted);


                        // tampilkan nama file atau path di link
                        txtPdfSumberDana.Text = ge.ListDataset.Tables[0].Rows[0]["file_sumberdana"].ToString();
                        txtPdfSumberDana.NavigateUrl = "#";

                        // pakai window.open agar buka popup
                        txtPdfSumberDana.Attributes["onclick"] = string.Format(
                            "window.open('{0}', '_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,width=900,height=850');",
                            secureUrl
                        );


                        var kodeasuransi = ge.ListDataset.Tables[0].Rows[0]["kodeasuransi"].ToString();
                      
                        txtnamaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["namaasuransi"].ToString();
                        txtrateasuransipanggil.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransivrf"].ToString()));
                        txtpremiasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["premigrossasuransi"].ToString()));
                        txtselisihasuransi.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["selisihasuransivrf"].ToString()));

                        TxtNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningtrf"].ToString();
                        TxtNomorBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningtrf"].ToString();
                        TxtAtasNamaBankRekeningTransfer.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningtrf"].ToString();
                        txtnamaBankkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekeningKB"].ToString();
                        txtnorekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningKB"].ToString();
                        txtatasnamarekeningkantorbayar.Text = ge.ListDataset.Tables[0].Rows[0]["NamaRekeningKB"].ToString();


                        ddlkodeasuransi.Enabled = false;
                        txtnamaasuransi.Enabled = false;
                        txtrateasuransipanggil.Enabled = false;
                        txtpremiasuransi.Enabled = false;
                        txtselisihasuransi.Enabled = false;

                        DdlAngsurandimuka();
                        ddlmaterai.SelectedValue = kodematerai;
                        ddlprovisi.SelectedValue = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["provisi"].ToString()));
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

                        DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[3];
                        vart1[0] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodesimulasi.Text, typeof(string));
                        vart1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MASTERASURANSI", typeof(string));
                        vart1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", vart1, "constrmf");


                        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";

                        ddlkodeasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                        ddlkodeasuransi.DataTextField = "NamaAsuransi";
                        ddlkodeasuransi.DataValueField = "KodeAsuransi";
                        ddlkodeasuransi.DataBind();
                        ddlkodeasuransi.Items.Insert(0, new ListItem("Pilih Kode Asuransi", "-"));


                        ddlkodeasuransi.SelectedValue = kodeasuransi;

                        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");

                        DefaultClass.Global.SqlHelper[] varSD = new DefaultClass.Global.SqlHelper[9];
                        varSD[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        varSD[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
                        varSD[2] = gb.CreateSPParameter("@statuspensiun", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlpensiun.SelectedValue), typeof(int));
                        varSD[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlsumberdana.SelectedValue, typeof(string));
                        varSD[4] = gb.CreateSPParameter("@program_pinj", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlproduk.SelectedValue, typeof(string));
                        varSD[5] = gb.CreateSPParameter("@Kategori", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlkategori.SelectedValue, typeof(string));
                        varSD[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGASD", typeof(string));
                        varSD[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varSD[8] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Money, 0, txtplafon.Text, typeof(decimal));
                        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", varSD, "constrmf");
                        float bungabank = 0;
                        decimal angsuranperbulanbank = 0;


                        if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                        {
                            bungabank = 0;
                            angsuranperbulanbank = 0;
                    


                        }

                        else
                        {
                            bungabank = float.Parse(ge.ListDataset.Tables[0].Rows[0]["rate"].ToString());
                            //bungaEfektifOBS = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaefektif1"].ToString());
                            angsuranperbulanbank = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Angsuranperbulanbank"].ToString());
                          
                        

                        }

                        txtbungabank.Text = string.Format("{0:N2}", bungabank);
                        txtangsuranperbulanbank.Text = string.Format("{0:N0}", angsuranperbulanbank);
                        txtselisihangsuran.Text= string.Format("{0:N0}",decimal.Parse(txtangsuran.Text)- angsuranperbulanbank);

                        bindGrdkartu(sender, e);
                        txtprovisitf_TextChanged(sender, e);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


                        txtselisihrp.Text = string.Format("{0:N0}", decimal.Parse(txtprovisi.Text) - decimal.Parse(txtprovisitf.Text));

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

    protected void txtprovisitf_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal xprovisi = 0, xadminbank = 0, xterimabersih = 0;

            xprovisi = decimal.Parse(txtplafon.Text) * (decimal.Parse(txtprovisitfpsn.Text) / 100);
            xadminbank = decimal.Parse(txtplafon.Text) * (decimal.Parse(txtadminbanktfpsn.Text) / 100);
            xterimabersih = decimal.Parse(txtplafon.Text) - (xprovisi + xadminbank);
            txtprovisitf.Text = string.Format("{0:N0}", xprovisi);
            txtadminbanktf.Text = string.Format("{0:N0}", xadminbank);
            txtterimabersihtf.Text = string.Format("{0:N0}", xterimabersih);


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    public static double PMT(float yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
    {
        var rate = (double)yearlyInterestRate / 100 / 12;
        var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
        return (rate + (rate / denominator)) * loanAmount;
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
                    txtgajidisplay.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Gaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtplafon.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasan.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Pelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktuTahun"].ToString();
                    //                    txttunjangananak.Text = ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString() == "" ? "0,00" : string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["TunjAnak"].ToString()).ToString().Replace(",0000", "").Replace(",00", "")));
                    ddljangkawaktu.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString(), ge.ListDataset.Tables[0].Rows[0]["JangkaWaktu"].ToString()));
                    ddlangsurandimuka.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString(), ge.ListDataset.Tables[0].Rows[0]["AngsDimuka"].ToString()));
                    //                    txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));



                    //InsertCombo();
                    //output
                    txtnama.Text = ge.ListDataset.Tables[0].Rows[0]["StatusAnggota"].ToString();
                    txtstatuspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["StatusPinjaman"].ToString();
                    txtusiapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaPinjaman"].ToString();
                    txtusiaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAsuransi"].ToString();
                    txtusiaanak1.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAnak1"].ToString();
                    txtusiaanak2.Text = ge.ListDataset.Tables[0].Rows[0]["UsiaAnak2"].ToString();
                    txtplafonmax.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PlafondMax"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtjangkawaktumax.Text = ge.ListDataset.Tables[0].Rows[0]["JangkaWaktuMax"].ToString();
                    txtrateasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString();
                    txtratebungapinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["RateBungaPinjaman"].ToString();
                    txtjangkatahun.Text = ge.ListDataset.Tables[0].Rows[0]["JthTempoThn"].ToString();
                    //txtjangkawaktumax = ge.ListDataset.Tables[0].Rows[0]["JthTempoBln"].ToString();
                    txtangsuran.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtasuransi.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["CicilanAsuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txttotal.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Total"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtsisagaji.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["SisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtprovisi.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtkeanggotaan.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["Keanggotan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtmaterai.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputMaterai"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsuranblokir.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["AngsuranDiblokir"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtangsurandimuka.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputAngsuranDimuka"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtbiayaadmin.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["BiayaAdmin"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpelunasanRp.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["OutputPelunasan"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtterimabersihrp.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["TerimaBersih"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtadminbank.Text = string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["AdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpersenasuransi.Text= string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenAsuransi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpsnangsuran.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenAngsuran"].ToString()));
                    txtpersenprovisi.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenProvisi"].ToString())); //Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenProvisi"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txttotalpersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString())); // string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtpersenadminbank.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenAdminBank"].ToString())); //string.Format("{0:N2}", Convert.ToInt32((ge.ListDataset.Tables[0].Rows[0]["PersenAdminBank"].ToString()).ToString().Replace(".00", "").Replace(",0000", "").Replace(",00", "")));
                    txtsisagajipersen.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["PersenSisaGaji"].ToString()));

                    txtangsuranblokirkali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["AngsBlokirKali"].ToString()));
                    txtangsurandimukakali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["AngsDimukaKali"].ToString()));
                    txtangdimukaasuransikali.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsdimukaasuransi"].ToString()));

                    if (ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString()=="" || ge.ListDataset.Tables[0].Rows[0]["MutasiKantorBayar"].ToString()== "Tidak")
                    {
                        ddlmutasikantorbayar.SelectedValue = "0";

                    }
                    else
                    {

                        ddlmutasikantorbayar.SelectedValue = "1";
                    }
                    
                   

                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];
                    varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTPROVISI", typeof(string));
                    varx[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varx[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
                    varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SI_Request", varx, "constrmf");

                    ddlprovisi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlprovisi.DataTextField = "provisi";
                    ddlprovisi.DataValueField = "provisi";
                    ddlprovisi.DataBind();
                    ddlprovisi.Items.Insert(0, new ListItem("Pilih Provisi", "0"));
                    ddlprovisi.SelectedValue = txtpersenprovisi.Text;

                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
                    var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTMATERAI", typeof(string));
                    var2[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var2[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
                    var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2, "constrmf");

                    ddlmaterai.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    ddlmaterai.DataTextField = "totalmaterai";
                    ddlmaterai.DataValueField = "materai";
                    ddlmaterai.DataBind();
                    ddlmaterai.Items.Insert(0, new ListItem("Pilih Materai", "0"));

                    ddlmaterai.SelectedValue = "2";
                    ddlmaterai_SelectedIndexChanged(sender, e);
                    DdlAngsurandimuka();
                    ddlangsurandimuka.SelectedValue = txtangsurandimukakali.Text;
                    ddlangsurandimukablokir.SelectedValue = txtangsuranblokirkali.Text;
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

                    TerimaBersih();


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

            if (ddlKantorBayar.SelectedItem.Text == "Bank Capital")
            {
                ddlsumberdana.SelectedValue = ddlKantorBayar.SelectedValue;
                ddlsumberdana.Enabled = false;
                txtkodesimulasi.Text = "B";
                txtpersenadminbank.Text = "1,5";
                //fungsiAllB();
            }
            else
            {
                ddlsumberdana.SelectedValue = "";
                ddlsumberdana.Enabled = true;
                //DdlAngsurandimuka();
                txtkodesimulasi.Text = "A";
                txtpersenadminbank.Text = "0";
            }
            ddlmaterai.SelectedValue = "2";
            ddlmaterai_SelectedIndexChanged(sender, e);

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
            if (ddlmaterai.SelectedValue == "")
            {
                txtmaterai.Text = "0";
                txtmateraipcs.Text = "0";
            }
            else
            {
                txtmateraipcs.Text = ddlmaterai.SelectedValue;
                txtmaterai.Text= string.Format("{0:N0}",decimal.Parse(ddlmaterai.SelectedItem.Text));

            }
  

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
            if (ddlangsurandimukablokir.SelectedValue == "-")
            {
                txtangsuranblokir.Text = "0";
                txtangsuranblokirkali.Text = "0";
            }
            else
            {
                txtangsuranblokirkali.Text = ddlangsurandimukablokir.SelectedValue;
                txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimukablokir.SelectedValue) * decimal.Parse(txtangsuran.Text));

            }


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
            if (ddlangsurandimuka.SelectedValue == "-")
            {
                txtangsurandimukakali.Text = "0";
                txtangsurandimuka.Text = "0";
            }
            else
            {
                txtangsurandimukakali.Text = ddlangsurandimuka.SelectedValue;
                txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimuka.SelectedValue) * decimal.Parse(txtangsuran.Text));

            }


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
            if (ddlangsurandimukaasuransi.SelectedValue == "-")
            {
                txtangdimukaasuransi.Text = "0";
                txtangdimukaasuransikali.Text = "0";
            }
            else
            {
                txtangdimukaasuransikali.Text = ddlangsurandimukaasuransi.SelectedValue;
                txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimukaasuransi.SelectedValue) * decimal.Parse(txtasuransi.Text));

            }


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
            for (int i = 3; i <= 8; i++)
            {
                ddlprovisi.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
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

    protected void ddlkantorbayar_SelectedIndexChanged(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[5];
        var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
        var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlproduk.SelectedValue, typeof(string));
        var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LSTJANGKA", typeof(string));
        var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");

        /*        txtkodesimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["kodesimulasi"].ToString();*/

        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";


        ddljangkawaktu.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljangkawaktu.DataTextField = "jangka";
        ddljangkawaktu.DataValueField = "jangka";
        ddljangkawaktu.DataBind();

        ddljangkawaktu.Items.Insert(0, new ListItem("Pilih Jangka", "-"));



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

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTMATERAI", typeof(string));
        var2[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var2[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodesimulasi.Text, typeof(string));
        var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2, "constrmf");

        ddlmaterai.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

        ddlmaterai.DataTextField = "totalmaterai";
        ddlmaterai.DataValueField = "materai";
        ddlmaterai.DataBind();
        ddlmaterai.Items.Insert(0, new ListItem("Pilih Materai", "0"));


        DdlAngsurandimuka();
    }

    protected void ddlprogrampinjaman_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DdlKodeProduk();
        ddlkantorbayar_SelectedIndexChanged(ddlKantorBayar.SelectedValue, EventArgs.Empty);
        //fungsiAllB();
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



    public void InsertCombo()
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


        //KODEBANK
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CHANNELLING_SD", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


        ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKdBank.DataTextField = "namabank";
        ddlKdBank.DataValueField = "KodeBank";
        ddlKdBank.DataBind();
        ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", ""));

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
        ddlproduk.Enabled = false;


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




        //ddlAO.Items.Insert(0, new ListItem("Pilih Data", "-"));


        ge.ListDataset = gb.GetDataFunction("dbo.GetMasterAsuransi()", "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";

        ddlkodeasuransi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkodeasuransi.DataTextField = "asuransi";
        ddlkodeasuransi.DataValueField = "KodeAsuransi";
        ddlkodeasuransi.DataBind();
        ddlkodeasuransi.Items.Insert(0, new ListItem("Pilih Data", "-"));

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
    protected void ddlkodeasuransi_SelectedIndexChanged(object sender, EventArgs e)
    {

        double rate = 0;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@umur", ParameterDirection.Input, SqlDbType.VarChar, 50, txtusiaasuransi.Text, typeof(string));
        var[1] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 50,txtjangkatahun.Text, typeof(string));
        var[2] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodeasuransi.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETRATEASURANSI", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var, "constrmf");

        if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
        {
            if (ddlkodeasuransi.SelectedValue != "-")
            {

                txtnamaasuransi.Text = ge.ListDataset.Tables[0].Rows[0]["namaasuransi"].ToString();
                txtrateasuransipanggil.Text = ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString();
                txtpremiasuransi.Text = string.Format("{0:N2}", decimal.Parse(txtplafon.Text) * decimal.Parse(txtrateasuransi.Text)/100);
                
                txtselisihasuransi.Text = string.Format("{0:N2}", decimal.Parse(txtasuransi.Text) - decimal.Parse(txtpremiasuransi.Text) );
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan');", true);

                txtpremiasuransi.Text = "0";
                txtrateasuransi.Text = "0";
                txtnamaasuransi.Text = "";
                txtselisihasuransi.Text = "0";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak ditemukan, periksa kembali inputan anda');", true);

            txtpremiasuransi.Text = "0";
            txtrateasuransi.Text = "0";
            txtnamaasuransi.Text = "";
            txtselisihasuransi.Text = "0";
        }



    }
    protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpanTolakan.Enabled = false;
            DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[9];
            var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var88[1] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
            var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, Hdnkodecabang.Value, typeof(string));
            var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
            var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
            var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BATALCAIR", typeof(string));
            var88[6] = gb.CreateSPParameter("@norekeningkredit", ParameterDirection.Input, SqlDbType.VarChar, 30, Hdnrekeningkredit.Value, typeof(string));
            var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var88[8] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10,SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var88, "constrmf");



            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
                pnladd.Visible = false;
                pnlIdentitas.Visible = false;
                PnlFront.Visible = true;
               
                //pnlPengajuan.Visible = false;
                PNLKELUARGA.Visible = false;
                btnSimpan.Visible = false;
              

             
                pnlAlasanTolak.Visible = false;
           
                Pnlpenerima.Visible = false;
                Pnlsimulasi.Visible = false;
                PnlSKPensiun.Visible = false;
                pnlpinjaman.Visible = false;

                pnlUpdAsuransi.Visible = false;
                btnCancel.Visible = false;

                ddlstatus_SelectedIndexChanged(sender, e);
                ddlkodeasuransi.SelectedValue = "-";
                txtalasantolak.Text = "";
                txtpremiasuransi.Text = "0";
                txtrateasuransi.Text = "0";
                txtnamaasuransi.Text = "";
                txtselisihasuransi.Text = "0";
                txtfocus.Focus();


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


    protected void txtnopen_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //lblnopenlama.

            if (Convert.ToInt32(txtnopen.Text.Length) < 11)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Nomor Pensiun Anda kurang dari 11, silahkan di cek kembali');", true);
            }
            else if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@nopen", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnopen.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKNOPENKTP", typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtnama.Text = "Anggota Terdaftar";
                    txtstatuspinjaman.Text = "LAMA";
                    txtkeanggotaan.Text = string.Format("{0:N0}", "0");
                    //txtnamapensiun.Focus();
                }
                else
                {
                    txtnama.Text = "Calon Anggota";
                    txtstatuspinjaman.Text = "BARU";
                    txtkeanggotaan.Text = string.Format("{0:N0}", 20000);
                    //lblnopenbaru.Visible = true;
                }


                string instansi;
                if (txtnopen.Text.Length == 11 || txtnopen.Text.Length == 15)
                {
                    instansi = "A";
                }
                else if (txtnopen.Text.Length == 12 || txtnopen.Text.Length == 14)
                {
                    instansi = "B";
                }
                else
                {
                    instansi = "NOPEN SALAH";
                }

                ddlinstansi.SelectedValue = instansi;
                ddlinstansi.Enabled = false;
            }
            //fungsiAll();
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
            //if (string.IsNullOrEmpty(ddlKantorBayar.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kantor Bayar terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}
            //if (string.IsNullOrEmpty(ddlsumberdana.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Sumber Dana terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}
            //if (string.IsNullOrEmpty(ddlprogrampinjaman.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}
            //if (string.IsNullOrEmpty(ddlkategori.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Program Pinjam terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtnopen.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon di isi no pensiun terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}
            //if (string.IsNullOrEmpty(ddlkodejiwa.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Kode Jiwa terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}
            //if (string.IsNullOrEmpty(ddlstatusnikah.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Status Nikah terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}
            //if (string.IsNullOrEmpty(ddlpensiun.SelectedValue))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mohon pilih Pensiun terlebih dahulu');", true);
            //    txttgllahir.Text = string.Empty;
            //    return;
            //}
            ////lblnopenlama.

            if (txtnopen.Text != "")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@tgllahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgllahir.Text, typeof(DateTime));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKUSIAPENSIUN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usiapinjaman"].ToString());
                    txtusiaasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["usia"].ToString());

                    if (int.Parse(txtusiaasuransi.Text) < 50)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Usia minimal 50 tahun');", true);
                        txttgllahir.Text = string.Empty;
                        txtusiapinjaman.Text = string.Empty;
                        txtusiaasuransi.Text = string.Empty;
                        return;

                    }

                    if (txtkodesimulasi.Text == "B")
                    {
                        //= IF((80 - G12) * 12 > 300; 300; (80 - G12) * 12)
                        if ((int.Parse("80") - int.Parse(txtusiapinjaman.Text)) * int.Parse("12") > 300)
                        {
                            txtjangkawaktumax.Text = "300";
                        }
                        else
                        {
                            txtjangkawaktumax.Text = string.Format("{0:N0}", (80 - int.Parse(txtusiapinjaman.Text)) * 12);


                        }
                        //fungsiAllB();

                    }
                    else
                    {
                        string result;



                        var reguler = TipeReguler(txtusiapinjaman.Text);

                        var platinum = TipePlatinum(txtusiapinjaman.Text);

                        if (ddlproduk.SelectedValue == "")
                        {
                            result = "0";//K93;
                        }
                        else
                        {
                            if (int.Parse(ddlproduk.SelectedValue) == 1 || int.Parse(ddlproduk.SelectedValue) == 2)
                            {
                                result = reguler;
                            }
                            else if (int.Parse(ddlkategori.SelectedValue) == 3 || int.Parse(ddlkategori.SelectedValue) == 4)
                            {
                                result = platinum;
                            }
                            else
                            {
                                result = "0";//K93;
                            }
                        }
                        txtjangkawaktumax.Text = result;
                        //                        fungsiAll();

                    }
                }
                else
                {
                    txtusiapinjaman.Text = string.Format("{0:N0}", "0");
                    txtusiaasuransi.Text = string.Format("{0:N0}", "0");
                    txtjangkawaktumax.Text = "0";
                    //lblnopenbaru.Visible = true;
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }
    public string TipeReguler(string usia)
    {
        var J90 = (75 - decimal.Parse(usia)) * 12;

        var reguler = string.Empty;

        if ((ddlsumberdana.SelectedItem.Text == "BPR Menara mas" || ddlsumberdana.SelectedItem.Text == "BPR Dana Mitra" || ddlsumberdana.SelectedItem.Text == "BPR Ramaganda" || ddlsumberdana.SelectedItem.Text == "BPR CAR" || ddlsumberdana.SelectedItem.Text == "BPR Indomitra") && J90 > 60)
        {
            reguler = "60";
        }
        else if (int.Parse(ddlpensiun.SelectedValue) == 3 && J90 > 60)
        {
            reguler = "60";
        }
        else if (int.Parse(ddlpensiun.SelectedValue) == 2 && J90 > 120)
        {
            reguler = "120";
        }
        else if (J90 < 24 || decimal.Parse(usia) >= 75)
        {
            reguler = "0";
        }
        else
        {
            reguler = J90.ToString();
        }
        return reguler;
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



    public string TipePlatinum(string usia)
    {
        string platinum = string.Empty;

        if (ddljangkawaktu.SelectedValue == string.Empty || ddljangkawaktu.SelectedValue == "-")
        {
            platinum = "0";
            return platinum;
        }

        decimal C19 = Math.Ceiling(decimal.Parse(ddljangkawaktu.SelectedValue) / 12);
        decimal C18 = Math.Floor(decimal.Parse(ddljangkawaktu.SelectedValue) / 12);
        decimal E54 = int.Parse(usia) + (12 * C19) - (12 * C18) - (12 * C19 - decimal.Parse(ddljangkawaktu.SelectedValue));
        DateTime D15 = DateTime.Parse(txttgllahir.Text);
        DateTime F5 = DateTime.Now;

        decimal D50 = int.Parse(usia);
        decimal D54 = int.Parse(usia) + C18;
        decimal D53 = (E54 >= 12 ? (int.Parse(usia) + C18 + 1) : D54);
        decimal D75 = 79 * 12;
        decimal D78 = D50 * 12;
        decimal E75 = 9;
        decimal E50 = DateDifYM(D15, F5);
        decimal F50 = DateDifMD(D15, F5);
        Decimal F53 = F50;
        decimal E78 = E50;
        decimal F75 = (F53 > 0 ? D75 + E75 - 1 : D75 + E75);
        decimal F78 = D78 + E78;
        decimal F89 = F75 - F78;
        decimal H88 = (80 * int.Parse(usia)) * 12;

        string[] daftarBPR = {
            "BPR Dana Mitra",
            "BPR Menara mas",
            "BPR Ramaganda",
            "BPR CAR",
            "BPR Indomitra"
        };

        decimal I88 = 0;
        if (H88 > 60 && daftarBPR.Contains(ddlsumberdana.SelectedItem.Text))
        {
            I88 = 60;
        }
        else if (ddlsumberdana.SelectedItem.Text == "BANK MNC" && H88 > 36)
        {
            I88 = H88;
        }
        else if (H88 > 120)
        {
            I88 = 120;
        }
        else if (H88 > 84)
        {
            I88 = 84;
        }
        else
        {
            I88 = H88;
        }
        decimal G89 = (F89 >= I88 ? I88 : F89);
        decimal J89 = (G89 <= 0 ? 0 : G89);


        if (ddlsumberdana.SelectedItem.Text == "BANK MNC" && (int.Parse(ddlpensiun.SelectedValue) == 2 || int.Parse(ddlpensiun.SelectedValue) == 3))
        {
            platinum = (J89 > 120) ? "120" : J89.ToString();
        }
        else if ((ddlsumberdana.SelectedItem.Text == "BPR Menara mas" || ddlsumberdana.SelectedItem.Text == "BPR Dana Mitra") && J89 > 60)
        {
            platinum = "60";
        }
        else if ((int.Parse(ddlpensiun.SelectedValue) == 2 || int.Parse(ddlpensiun.SelectedValue) == 3) && J89 > 60)
        {
            platinum = "60";
        }
        else if (int.Parse(usia) <= 70 && (J89 < 24 || D53 < 75))
        {
            platinum = "0";
        }
        else
        {
            platinum = J89.ToString();
        }


        return platinum;
    }
    protected void ddljangkawaktu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtusia_OnChange(sender, e);

            if (txtkodesimulasi.Text == "B")
            {



                if (ddljangkawaktu.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Anda Belum Memilih Jangka, silahkan di cek kembali');", true);
                    return;
                }
                else if (ddlinstansi.SelectedValue == "B")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Maaf Instansi Asabri Belum bisa, silahkan di cek kembali');", true);
                    return;
                }
                else if (decimal.Parse(ddljangkawaktu.SelectedValue) > decimal.Parse(txtjangkawaktumax.Text))
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningDate('Jangka waktu tidak boleh melebihi jangka waktu max, silahkan di cek kembali');", true);
                    return;
                }

                else
                {
                    if (int.Parse(txtusiaasuransi.Text) < 66)
                    {
                        DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[6];
                        var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
                        var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 20, "1", typeof(string));
                        var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJANGKA", typeof(string));
                        var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var1xx[5] = gb.CreateSPParameter("@Jangkabulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
                        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");
                    }
                    else
                    {


                        DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[6];
                        var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
                        var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlproduk.SelectedValue, typeof(string));
                        var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJANGKA", typeof(string));
                        var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var1xx[5] = gb.CreateSPParameter("@Jangkabulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
                        ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");

                    }

                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtjangkatahun.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["Jangkatahun"].ToString());
                        txtratebungapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());

                    }
                    else
                    {


                        txtjangkatahun.Text = string.Format("{0:N0}", "0");
                        txtratebungapinjaman.Text = string.Format("{0:N0}", "0");


                        //lblnopenbaru.Visible = true;
                    }
                    DefaultClass.Global.SqlHelper[] var2xx = new DefaultClass.Global.SqlHelper[6];
                    var2xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodesimulasi.Text, typeof(string));
                    var2xx[1] = gb.CreateSPParameter("@UsiaAsuransi", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusiaasuransi.Text), typeof(int));

                    var2xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKASURANSI", typeof(string));
                    var2xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var2xx[5] = gb.CreateSPParameter("@Jangkatahun", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtjangkatahun.Text), typeof(int));
                    ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2xx, "constrmf");


                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtrateasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                    }
                    else
                    {
                        txtrateasuransi.Text = string.Format("{0:N0}", "0");
                    }

                    fungsiAllB();


                }



                

            }
            else
            {
                DefaultClass.Global.SqlHelper[] var1xx = new DefaultClass.Global.SqlHelper[6];
                var1xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlKantorBayar.SelectedValue, typeof(string));
                var1xx[1] = gb.CreateSPParameter("@KodeSumberDana", ParameterDirection.Input, SqlDbType.VarChar, 20, "2", typeof(string));
                var1xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKJANGKA", typeof(string));
                var1xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1xx[5] = gb.CreateSPParameter("@Jangkabulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangkawaktu.SelectedValue), typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var1xx, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtratebungapinjaman.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                }
                else
                {
                    txtratebungapinjaman.Text = string.Format("{0:N0}", "0");
                }

                txtjangkatahun.Text = string.Format("{0:N0}", Math.Floor(decimal.Parse(ddljangkawaktu.Text) / 12));

                DefaultClass.Global.SqlHelper[] var2xx = new DefaultClass.Global.SqlHelper[6];
                var2xx[0] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodesimulasi.Text, typeof(string));
                var2xx[1] = gb.CreateSPParameter("@UsiaAsuransi", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtusiaasuransi.Text), typeof(int));

                var2xx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKASURANSI", typeof(string));
                var2xx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2xx[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var2xx[5] = gb.CreateSPParameter("@Jangkatahun", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtjangkatahun.Text), typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("SI_Request", var2xx, "constrmf");


                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    txtrateasuransi.Text = string.Format("{0:N0}", ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                }
                else
                {
                    txtrateasuransi.Text = string.Format("{0:N0}", "0");
                }

                fungsiAll();
            }

            DateTime tgl_libur, tgl_realisasi;
            tgl_realisasi = DateTime.Parse(txttglrealisasi.Text);
            tgl_libur = tgl_realisasi.AddMonths(int.Parse(ddljangkawaktu.SelectedValue));
            txttgljatuhtempo.Text= tgl_libur.ToString();
            DateTime tgljt = Convert.ToDateTime(txttgljatuhtempo.Text);
            txttgljatuhtempo.Text = tgljt.ToString("dd/MM/yyyy");

           

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }


    }
    protected void txtplafon_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddljangkawaktu.Text == "-" || string.IsNullOrEmpty(ddljangkawaktu.Text))
            {
                txtkodesimulasi.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sebelum isi Plafon mohon pilih terlebih dahulu jangka waktu');", true);
                return;
            }

            if (txtkodesimulasi.Text == "A")
            {
                fungsiAll();
            }
            else
            {
                fungsiAllB();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    public bool fungsiAll()
    {
        try
        {
            asuransi();
            plafondmax();
            angsuran();
            Total();
            SisaGaji();
            Provisi();
            angsuranDiblokir();
            angsuranDimuka();
            BiayaAdmin();
            TerimaBersih();
            PersenAngsuran();
            PersenAsuransi();
            PersenTotal();
            PersenSisaGaji();
            PersenProvisi();
            Angsuranblokirkali();
            Angsurandimukakali();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return true;
    }

    public bool fungsiAllB()
    {
        try
        {
            //            jangkawaktuMaxB();
            asuransiB();
            PlafondmaxB();
            angsuranB();
            PersenAngsuran();
            AdminBankB();
            Total();
            SisaGaji();
            ProvisiB();
            angsuranDiblokirB();
            angsuranDimukaAsuransiB();
            BiayaAdminB();

            //BiayaAdminB();

            Materaidefault();


            TerimaBersih();
           

            PersenTotal();
            PersenSisaGaji();
            PersenAsuransi();

            //PersenProvisi();
            //Angsuranblokirkali();
            //Angsurandimukakali();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return true;
    }

    public string angsuran()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                return txtangsuran.Text;
            }

            decimal hasil = 0;
            hasil = (decimal.Parse(txtplafon.Text) / decimal.Parse(ddljangkawaktu.Text) + decimal.Parse(txtplafon.Text) * (decimal.Parse(txtratebungapinjaman.Text) / 100));
            txtangsuran.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsuran.Text;
    }

    public string angsuranB()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                return txtangsuran.Text;
            }

            string hari = DateTime.Now.DayOfWeek.ToString();
            string hasil = string.Empty;

            if (hari == "SABTU" || hari == "MINGGU" || decimal.Parse(txtplafon.Text) > decimal.Parse(txtplafonmax.Text) + 1)
            {
                hasil = "0";
            }
            else
            {
                hasil = string.Format("{0:N0}", Math.Ceiling(decimal.Parse(txtplafon.Text) * (decimal.Parse(txtratebungapinjaman.Text) / 100) + decimal.Parse(txtplafon.Text) / decimal.Parse(ddljangkawaktu.SelectedValue)));
            }
            txtangsuran.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsuran.Text;
    }

    public string jangkawaktuMaxB()
    {
        try
        {
            string a = string.Empty;
            string hasil = string.Empty;

            string usia = txtusiapinjaman.Text;
            // Perhitungan maksimal tenor dalam bulan: (80 - usia) * 12, maksimal 300
            decimal tenorMaks = (80 - decimal.Parse(usia)) * 12;
            a = (tenorMaks > 300 ? 300 : tenorMaks).ToString();


            if (ddlpensiun.SelectedItem.Text == "DUDA")
            {
                hasil = "0";
            }

            if (ddlpensiun.SelectedItem.Text == "sendiri")
            {
                hasil = a;

            }

            if (ddlpensiun.SelectedItem.Text == "Janda" && decimal.Parse(a) > 300)
            {
                hasil = "300";
            }

            txtjangkawaktumax.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtjangkawaktumax.Text;
    }

    public string asuransi()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                return txtasuransi.Text;
            }

            decimal hasil = 0;
            if (decimal.Parse(txtrateasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtplafon.Text) * (decimal.Parse(txtrateasuransi.Text) / 100);
            }

            txtasuransi.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtasuransi.Text;

    }

    public string asuransiB()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                return txtasuransi.Text;
            }
            decimal hasil = 0;
            hasil = decimal.Parse(txtplafon.Text) * (15m / 100);
            txtasuransi.Text = string.Format("{0:N0}", hasil);

            /*            
                decimal nilaiDasar = ((decimal.Parse(txtplafon.Text) * (decimal.Parse(txtrateasuransi.Text)/100) * (decimal.Parse(txtratebungapinjaman.Text)/100) * decimal.Parse(ddljangkawaktu.SelectedValue) / 12) + ((decimal.Parse(txtplafon.Text) * (decimal.Parse(txtrateasuransi.Text) / 100))) / decimal.Parse(ddljangkawaktu.SelectedValue));
                decimal dibulatkan = Math.Round(nilaiDasar, 0, MidpointRounding.AwayFromZero); // ROUND(..., 0)

                if (int.Parse(txtusiaasuransi.Text) < 66 && int.Parse(txtusiaasuransi.Text) + decimal.Parse(txtjangkatahun.Text) > 75)
                {
                    hasil =  dibulatkan * (67m/100);
                }
                else if (int.Parse(txtusiaasuransi.Text) < 66)
                {
                    hasil = dibulatkan / 2;
                }
                else
                {
                    hasil = dibulatkan;
                }
              */

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtasuransi.Text;

    }

    public string Total()
    {
        try
        {
            decimal hasil = 0;
            hasil = decimal.Parse(txtangsuran.Text) + decimal.Parse(txtasuransi.Text);
            txttotal.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txttotal.Text;
    }

    public string SisaGaji()
    {
        try
        {
            decimal hasil = 0;
            hasil = decimal.Parse(txtgajidisplay.Text) - decimal.Parse(txtangsuran.Text);
            txtsisagaji.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtsisagaji.Text;
    }
    public string SisaGajiB()
    {
        try
        {
            decimal hasil = 0;
            hasil = decimal.Parse(txtgajidisplay.Text) - decimal.Parse(txttotal.Text);
            txtsisagaji.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtsisagaji.Text;
    }

    public string Provisi()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtrateasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if (int.Parse(ddlprovisi.SelectedValue) == 0)
                {
                    hasil = 0;
                }
                else
                {
                    hasil = (decimal.Parse(ddlprovisi.SelectedValue) / 100) * decimal.Parse(txtplafon.Text);
                }
            }

            txtprovisi.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtprovisi.Text;
    }
    public string ProvisiB()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                ddlprovisi.SelectedValue = "-";
                txtprovisi.Text = "0";
                txtpersenprovisi.Text = "0";
                return txtprovisi.Text;
            }

            decimal hasil = 0;

            if (decimal.Parse(txtsisagaji.Text) == 0)
            {
                hasil = 0;
                ddlprovisi.SelectedValue = "-";
                txtprovisi.Text = "0";
                txtpersenprovisi.Text = "0";
            }
            else
            {
                if (int.Parse(txtusiapinjaman.Text) + int.Parse(txtjangkatahun.Text) > 75)
                {
                    ddlprovisi.SelectedValue = "6";

                    //hasil = decimal.Parse(txtplafon.Text) * (6m / 100);
                }
                else
                {
                    ddlprovisi.SelectedValue = "5";
                    //hasil = decimal.Parse(txtplafon.Text) * (5m / 100);
                }
            }

            if (ddlprovisi.SelectedValue == "-")
            {
                txtpersenprovisi.Text = "0";
                txtprovisi.Text = "0";
            }
            else
            {
                txtpersenprovisi.Text = ddlprovisi.SelectedValue;
                txtprovisi.Text = string.Format("{0:N0}", (decimal.Parse(ddlprovisi.SelectedValue) / 100) * decimal.Parse(txtplafon.Text));

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return ddlprovisi.SelectedValue;
    }
    public string AdminBankB()
    {
        try
        {
            if (decimal.Parse(txtplafon.Text) == 0)
            {
                return txtadminbank.Text;
            }

            decimal hasil = 0;

            if (decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtplafon.Text) * (decimal.Parse(txtpersenadminbank.Text) / 100);
            }

            txtadminbank.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtadminbank.Text;
    }
    protected void txtgajidisplay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddljangkawaktu.Text == "-" || string.IsNullOrEmpty(ddljangkawaktu.Text))
            //{
            //    txtgajidisplay.Text = "0";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sebelum isi Gaji mohon pilih terlebih dahulu jangka waktu');", true);
            //    return;
            //}

            if (txtkodesimulasi.Text != "")
            {

           

            if (txtkodesimulasi.Text == "A")
            {
                fungsiAll();
            }
            else
            {
                fungsiAllB();
            }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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
            txtgajibersih.Text=string.Format("{0:N0}", decimal.Parse(txtnetgaji.Text) + decimal.Parse(txttotalpotongan.Text));

           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    public string angsuranDiblokir()
    {
        try
        {
            decimal hasil = 0;

            if (ddlKantorBayar.SelectedValue == "1" || ddlKantorBayar.SelectedValue == "5" || ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3")
            {
                //hasil = 1 * decimal.Parse(txtangsuran.Text);
                ddlangsurandimukablokir.SelectedValue = "1";
            }
            else
            {
                hasil = 0;
                ddlangsurandimukablokir.SelectedValue = "-";
            }

            //txtangsuranblokir.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return ddlangsurandimukablokir.SelectedValue;
    }

    public string angsuranDiblokirB()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
                ddlangsurandimukablokir.SelectedValue = "-";
                ddlangsurandimuka.SelectedValue = "-";
            }
            else
            {
                if (ddlkategori.SelectedValue == "B")
                {
                    ddlangsurandimukablokir.SelectedValue = "3";
                    ddlangsurandimuka.SelectedValue = "3";
                    //hasil = decimal.Parse(txtangsuran.Text) * 3;
                }
                else
                {
                    ddlangsurandimukablokir.SelectedValue = "-";
                    ddlangsurandimuka.SelectedValue = "-";
                    //hasil = decimal.Parse(txtangsuran.Text);
                }

            }

            if (ddlangsurandimukablokir.SelectedValue == "-")
            {
                txtangsuranblokir.Text = "0";
                txtangsuranblokirkali.Text = "0";
            }
            else
            {
                txtangsuranblokirkali.Text = ddlangsurandimukablokir.SelectedValue;
                txtangsuranblokir.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimukablokir.SelectedValue) * decimal.Parse(txtangsuran.Text));

            }


            if (ddlangsurandimuka.SelectedValue == "-")
            {
                txtangsurandimukakali.Text = "0";
                txtangsurandimuka.Text = "0";
            }
            else
            {
                txtangsurandimukakali.Text = ddlangsurandimuka.SelectedValue;
                txtangsurandimuka.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimuka.SelectedValue) * decimal.Parse(txtangsuran.Text));

            }




            //txtangsuranblokir.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return ddlangsurandimukablokir.SelectedValue;
    }

    public string angsuranDimuka()
    {
        try
        {
            decimal hasil = 0;

            if (int.Parse(ddlangsurandimuka.Text) == 0)
            {
                hasil = 0;
                ddlangsurandimuka.SelectedValue = "-";
            }
            else
            {
                ddlangsurandimuka.SelectedValue = "1";
                hasil = decimal.Parse(txtangsuran.Text) * int.Parse(ddlangsurandimuka.Text);
            }

            txtangsurandimuka.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtangsurandimuka.Text;
    }

    public string angsuranDimukaAsuransiB()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtasuransi.Text) == 0)
            {
                hasil = 0;
                ddlangsurandimukaasuransi.SelectedValue = "-";
                txtangdimukaasuransi.Text = "0";
                txtangdimukaasuransikali.Text = "0";
            }
            else
            {
                if (ddlkategori.SelectedValue == "B")
                {
                    ddlangsurandimukaasuransi.SelectedValue = "3";
                  
                }
                else
                {
                    ddlangsurandimukaasuransi.SelectedValue = "-";
                  
                }
            }

            if (ddlangsurandimukaasuransi.SelectedValue == "-")
            {
                txtangdimukaasuransi.Text = "0";
                txtangdimukaasuransikali.Text = "0";
            }
            else
            {
                txtangdimukaasuransikali.Text = ddlangsurandimukaasuransi.SelectedValue;
                txtangdimukaasuransi.Text = string.Format("{0:N0}", decimal.Parse(ddlangsurandimukaasuransi.SelectedValue) * decimal.Parse(txtasuransi.Text));

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return ddlangsurandimukaasuransi.SelectedValue;
    }
    public string angsuranDimukaB()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if (int.Parse(ddlangsurandimuka.SelectedValue) < 1)
                {
                    hasil = 0;
                }
                else
                {
                    hasil = decimal.Parse(txtangsuran.Text) * int.Parse(ddlangsurandimuka.SelectedValue);

                }
            }

            txtangsurandimuka.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtangsurandimuka.Text;
    }

    public string BiayaAdminB()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if (int.Parse(ddljangkawaktu.SelectedValue) < 120)
                {
                    hasil = 1250000;
                }
                else
                {
                    if (int.Parse(ddljangkawaktu.SelectedValue) > 120 && int.Parse(ddljangkawaktu.SelectedValue) < 180)
                    {
                        hasil = 1450000;
                    }
                    else
                    {
                        if (int.Parse(ddljangkawaktu.SelectedValue) > 180)
                        {
                            hasil = 1750000;
                        }
                    }
                }
            }

            txtbiayaadmin.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtbiayaadmin.Text;
    }
    public string BiayaAdmin()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtrateasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                if (int.Parse(ddljangkawaktu.SelectedValue) > 120)
                {
                    hasil = 1200000;
                }
                else
                {
                    hasil = 1000000;
                }
            }

            txtbiayaadmin.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtbiayaadmin.Text;
    }

    public string TerimaBersih()
    {
        try
        {

            decimal hasil = 0;

            if (decimal.Parse(txtasuransi.Text) == 0 || decimal.Parse(txtplafon.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtplafon.Text) - decimal.Parse(txtasuransi.Text) - decimal.Parse(txtadminbank.Text) - decimal.Parse(txtprovisi.Text) - decimal.Parse(txtkeanggotaan.Text) - decimal.Parse(txtangsuranblokir.Text) - decimal.Parse(txtangsurandimuka.Text) - decimal.Parse(txtmaterai.Text) - decimal.Parse(txtbiayaadmin.Text) - decimal.Parse(txtpelunasanRp.Text);
            }

            txtterimabersihrp.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtterimabersihrp.Text;
    }

    public string PersenAngsuran()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtangsuran.Text) / decimal.Parse(txtgajidisplay.Text)) * 100;
            }
            txtpsnangsuran.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtpsnangsuran.Text;
    }

    public string Materaidefault()
    {
        try
        {

        
            if (ddlmaterai.SelectedValue == "" || ddlmaterai.SelectedValue == "0")
            {
                txtmaterai.Text = "0";
                txtmateraipcs.Text = "0";
            }
            else
            {
                txtmateraipcs.Text = ddlmaterai.SelectedValue;
                txtmaterai.Text = string.Format("{0:N0}", decimal.Parse(ddlmaterai.SelectedItem.Text));

            }

           

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return ddlmaterai.SelectedValue;
    }
    public string PersenAsuransi()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtasuransi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtasuransi.Text) / decimal.Parse(txtgajidisplay.Text)) * 100;
            }
            txtpersenasuransi.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtpersenasuransi.Text;
    }
    public string PersenTotal()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txttotal.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txttotal.Text) / decimal.Parse(txtgajidisplay.Text)) * 100;
            }
            txttotalpersen.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txttotalpersen.Text;
    }

    public string PersenSisaGaji()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtsisagaji.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtsisagaji.Text) / decimal.Parse(txtgajidisplay.Text)) * 100;
            }
            txtsisagajipersen.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtsisagajipersen.Text;
    }

    public string PersenProvisi()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtprovisi.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = (decimal.Parse(txtprovisi.Text) / decimal.Parse(txtplafon.Text)) * 100;
            }
            txtpersenprovisi.Text = string.Format("{0:N2}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtpersenprovisi.Text;
    }

    public string Angsuranblokirkali()
    {
        try
        {
            decimal hasil = 0;
            if (decimal.Parse(txtangsuranblokir.Text) == 0 || decimal.Parse(txtangsuran.Text) == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = decimal.Parse(txtangsuranblokir.Text) / decimal.Parse(txtangsuran.Text);
            }
            txtangsuranblokirkali.Text = string.Format("{0:N0}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsuranblokirkali.Text;
    }
    public string Angsurandimukakali()
    {
        try
        {

            txtangsurandimukakali.Text = ddlangsurandimuka.Text;
            txtangdimukaasuransikali.Text = ddlangsurandimuka.Text;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return txtangsurandimukakali.Text;
    }

    public string DdlAngsurandimukaB()
    {
        try
        {

            string hasil = string.Empty;
            ddlangsurandimuka.Items.Clear();

            if (ddlkategori.SelectedItem.Text == "Pra Pensiun")
            {
                DateTime tglTmPensiun = DateTime.Parse(txtmtpensiun.Text);
                DateTime sekarang = DateTime.Now;

                int selisihBulan = ((tglTmPensiun.Year - sekarang.Year) * 12) + (tglTmPensiun.Month - sekarang.Month);

                if (selisihBulan < 0) selisihBulan = 0;

                hasil = string.Format("{0:N0}", selisihBulan);

            }
            else
            {
                hasil = "1";
            }

            ddlangsurandimuka.Items.Add(new ListItem(hasil, hasil));
            fungsiAllB();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
        return ddlangsurandimuka.Text;
    }


    protected string plafondmax()
    {
        try
        {

            decimal plafon = decimal.Parse(txtplafon.Text);
            decimal hasil = 0;

            if (decimal.Parse(txtplafon.Text) == 0)
            {
                hasil = 0;
                txtplafonmax.Text = string.Format("{0:N0}", hasil);
                return string.Format("{0:N0}", hasil);
            }


            decimal temp_plafon = (decimal.Parse(txtgajidisplay.Text) * (decimal)double.Parse(ddljangkawaktu.SelectedValue)) / ((decimal.Parse(txtratebungapinjaman.Text) / 100) * decimal.Parse(ddljangkawaktu.SelectedValue) + 1);


            if (plafon < 500000)
            {
                hasil = 0;
            }
            else if ((ddlproduk.SelectedValue == "1" || ddlproduk.SelectedValue == "2") && int.Parse(ddljangkawaktu.SelectedValue) < 24)
            {
                hasil = 0;
            }
            else if ((ddlproduk.SelectedValue == "3" || ddlproduk.SelectedValue == "4") && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && temp_plafon > 50000000 && ddlsumberdana.SelectedItem.Text != "BANK MNC")
            {
                hasil = 50000000;
            }
            else if ((ddlsumberdana.SelectedItem.Text == "BPR Menara Mas" || ddlsumberdana.SelectedItem.Text == "BPR Dana Mitra" || ddlsumberdana.SelectedItem.Text == "BPR Ramaganda" || ddlsumberdana.SelectedItem.Text == "BPR Indomitra") && temp_plafon > 100000000)
            {
                hasil = 100000000;
            }
            else if ((ddlproduk.SelectedValue == "1" || ddlproduk.SelectedValue == "2") && (ddlpensiun.SelectedValue == "2" || ddlpensiun.SelectedValue == "3") && temp_plafon > 100000000)
            {
                hasil = 100000000;
            }
            else if ((ddlproduk.SelectedValue == "1" || ddlproduk.SelectedValue == "2") && temp_plafon > 350000000)
            {
                hasil = 350000000;
            }
            else if ((ddlproduk.SelectedValue == "3" || ddlproduk.SelectedValue == "4") && temp_plafon > 100000000 && int.Parse(txtusiapinjaman.Text) > 73)
            {
                hasil = 100000000;
            }
            else if ((ddlproduk.SelectedValue == "3" || ddlproduk.SelectedValue == "4") && temp_plafon > 150000000)
            {
                hasil = 150000000;
            }
            else if (ddlproduk.SelectedValue == "5" && temp_plafon > 150000000)
            {
                hasil = 150000000;
            }
            else
            {
                hasil = temp_plafon;
            }
            txtplafonmax.Text = string.Format("{0:N0}", hasil);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtplafonmax.Text;
    }

    protected string PlafondmaxB()
    {
        try
        {
            decimal hasil = 0;

            if (decimal.Parse(txtplafon.Text) == 0)
            {
                hasil = 0;
                txtplafonmax.Text = string.Format("{0:N0}", hasil);
                return string.Format("{0:N0}", hasil);
            }
            decimal J19 = decimal.Parse(txtasuransi.Text) * decimal.Parse(ddljangkawaktu.Text);
            decimal J18 = J19 / decimal.Parse(txtplafon.Text);
            decimal pembagi = ((decimal.Parse(txtratebungapinjaman.Text) / 100) * decimal.Parse(ddljangkawaktu.SelectedValue) + 1);
            if (ddlkategori.SelectedValue == "A" && ((decimal.Parse(txtgajidisplay.Text) * (95 / 100) * decimal.Parse(ddljangkawaktu.SelectedValue)) / pembagi) > 500000000)
            {
                hasil = 500000000;
            }
            else
            {
                if (decimal.Parse(txtgajidisplay.Text) * (5m / 100) < 100000)
                {
                    hasil = ((decimal.Parse(txtgajidisplay.Text) - 100000) * decimal.Parse(ddljangkawaktu.SelectedValue)) / pembagi;
                }
                else
                {
                    hasil = (decimal.Parse(txtgajidisplay.Text) * (95m / 100) * decimal.Parse(ddljangkawaktu.SelectedValue)) / pembagi;
                }
            }
            txtplafonmax.Text = string.Format("{0:N0}", hasil);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

        return txtplafonmax.Text;
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




    protected void btnBackIdentitas_Click(object sender, EventArgs e)
    {
        Pnlpenerima.Visible = true;
        Pnlsimulasi.Visible = true;
        PnlSKPensiun.Visible = true;
        pnlpinjaman.Visible = true;

        pnlUpdAsuransi.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    
    protected void btndokumen_Click(object sender, EventArgs e)
    {
        Pnlpenerima.Visible = false;
        Pnlsimulasi.Visible = false;
        PnlSKPensiun.Visible = false;
        pnlpinjaman.Visible = false;

        pnlUpdAsuransi.Visible = true;
        fileupload1.Focus();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void btnTolak_Click(object sender, EventArgs e)
    {




        //btnSimpanTolakan.Enabled = true;
        //btnSimpanTolakan.Visible = true;

        //pnladd.Visible = false;
        //pnlIdentitas.Visible = false;
      
        //btnSimpan.Visible = false;
        //BtnApproveAntrian.Visible = false;
        //BtnBatalkanmohon.Visible = false;
        //BtnApproveAntrian.Visible = false;
        //BtnBatalkanmohon.Visible = false;
        //btnlanjutkan.Visible = false;
        //btnTolakAntrian.Visible = false;
        //pnlAlasanTolak.Visible = false;
        //txtnonasabah.Text = "";
        //Pnlpenerima.Visible = false;
        //Pnlsimulasi.Visible = false;
        //PnlSKPensiun.Visible = false;
        //pnlpinjaman.Visible = false;

        //pnlUpdAsuransi.Visible = false;
        //btnCancel.Visible = false;
        //btndokumen.Visible = false;

        //pnlAlasanTolak.Visible = true;


        //txtalasantolak.Focus();

    }
    //protected void btnSimpanTolakan_Click(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    //    btnSimpanTolakan.Enabled = false;
    //    //    DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[7];
    //    //    var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
    //    //    var88[1] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
    //    //    var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //    //    var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
    //    //    var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
    //    //    var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REJECTSTATUSCAIR", typeof(string));
    //    //    var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //    //    ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var88, "constrmf");



    //    //    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //    //    {

    //    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
    //    //        pnladd.Visible = false;
    //    //        pnlIdentitas.Visible = false;
    //    //        PnlFront.Visible = true;
    //    //        //pnlFilter.Visible = true;
    //    //        //pnlJaminan.Visible = false;
    //    //        //pnlPengajuan.Visible = false;
    //    //        btnSimpan.Visible = false;
    //    //        BtnApproveAntrian.Visible = false;
    //    //        BtnBatalkanmohon.Visible = false;
    //    //        BtnApproveAntrian.Visible = false;
    //    //        BtnBatalkanmohon.Visible = false;
    //    //        btnlanjutkan.Visible = false;
    //    //        btnTolakAntrian.Visible = false;
    //    //        pnlAlasanTolak.Visible = false;
    //    //        txtnonasabah.Text = "";
    //    //        Pnlpenerima.Visible = false;
    //    //        Pnlsimulasi.Visible = false;
    //    //        PnlSKPensiun.Visible = false;
    //    //        pnlpinjaman.Visible = false;

    //    //        pnlUpdAsuransi.Visible = false;
    //    //        btnCancel.Visible = false;
    //    //        btndokumen.Visible = false;
    //    //        ddlstatus_SelectedIndexChanged(sender, e);
    //    //        txtnonasabah.Focus();
    //    //    }

    //    //    else
    //    //    {

    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //    //        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //    //    }

    //    //}
    //    //catch (Exception X)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //    //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //    //}



    //}
    protected void btnBackTolak_Click(object sender, EventArgs e)
    {
        //pnladd.Visible = true;
        //pnlIdentitas.Visible = true;
        //PnlFront.Visible = false;
        //pnlFilter.Visible = false;
        ////pnlJaminan.Visible = true;
        ////pnlPengajuan.Visible = true;
        //btnSimpan.Visible = true;
        //BtnApproveAntrian.Visible = false;
        //BtnBatalkanmohon.Visible = false;
        //BtnApproveAntrian.Visible = false;
        //BtnBatalkanmohon.Visible = false;
        //btnlanjutkan.Visible = false;
        //btnTolakAntrian.Visible = false;
        //pnlpinjaman.Visible = true;
        //Pnlpenerima.Visible = true;
        //Pnlsimulasi.Visible = true;
        //PnlSKPensiun.Visible = true;
        //pnlpinjaman.Visible = true;

        //pnlUpdAsuransi.Visible = false;
        //btnCancel.Visible = true;
        //btndokumen.Visible = true;
        //Button2.Visible = false;

        //btnSimpan.Visible = false;
        //BtnApproveAntrian.Visible = true;
        //BtnBatalkanmohon.Visible = true;
        //BtnApproveAntrian.Enabled = true;

        //pnlAlasanTolak.Visible = false;


        //btnlanjutkan.Visible = false;
        //btnTolakAntrian.Visible = false;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }


    protected void btnbatalkanantrian_Click(object sender, EventArgs e)
    {
        



        //try
        //{
        //    btnTolakAntrian.Enabled = false;
        //    DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[7];
        //    var88[0] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        //    var88[1] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalasantolak.Text, typeof(string));
        //    var88[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        //    var88[3] = gb.CreateSPParameter("@bukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidpermohonan.Text, typeof(string));
        //    var88[4] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
        //    var88[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BATALKANANTRIAN", typeof(string));
        //    var88[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //    ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var88, "constrmf");



        //    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //    {

        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Pembatalan Permohonan Done!','You successfully save this Project','top-right','success','1');", true);
        //        pnladd.Visible = false;
        //        pnlIdentitas.Visible = false;
        //        PnlFront.Visible = true;
        //        pnlFilter.Visible = true;
        //        //pnlJaminan.Visible = false;
        //        //pnlPengajuan.Visible = false;
        //        btnSimpan.Visible = false;
        //        BtnApproveAntrian.Visible = false;
        //        BtnBatalkanmohon.Visible = false;
        //        BtnApproveAntrian.Visible = false;
        //        BtnBatalkanmohon.Visible = false;
        //        btnlanjutkan.Visible = false;
        //        btnTolakAntrian.Visible = false;
        //        pnlAlasanTolak.Visible = false;
        //        txtnonasabah.Text = "";
        //        Pnlpenerima.Visible = false;
        //        Pnlsimulasi.Visible = false;
        //        PnlSKPensiun.Visible = false;
        //        pnlpinjaman.Visible = false;

        //        pnlUpdAsuransi.Visible = false;
        //        btnCancel.Visible = false;
        //        btndokumen.Visible = false;
        //        ddlstatus_SelectedIndexChanged(sender, e);
        //        txtnonasabah.Focus();
        //    }

        //    else
        //    {

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //    }

        //}
        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}



    }

    protected void Uploadfoto1(object sender, EventArgs e)
    {
        //try
        //{
        //    string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
        //    DateTime now = DateTime.Now;

        //    namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS1";


        //    //Check whether Directory (Folder) exists.
        //    if (!Directory.Exists(folderPath))
        //    {
        //        //If Directory (Folder) does not exists. Create it.
        //        Directory.CreateDirectory(folderPath);
        //    }

        //    txtfoto1.Text = "";


        //    fileupload1.SaveAs(folderPath + namafileJaminan + ".jpg");

        //    fotopathKTP1 = folderPath + namafileJaminan;

        //    txtfoto1.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

        //    //Display the Picture in Image control.
        //    imgfoto1.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();



        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
    
    }
    protected void Uploadfoto2(object sender, EventArgs e)
    {
        //try
        //{
        //    string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
        //    DateTime now = DateTime.Now;

        //    namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS2";


        //    //Check whether Directory (Folder) exists.
        //    if (!Directory.Exists(folderPath))
        //    {
        //        //If Directory (Folder) does not exists. Create it.
        //        Directory.CreateDirectory(folderPath);
        //    }


        //    txtfoto2.Text = "";

        //    fileupload2.SaveAs(folderPath + namafileJaminan + ".jpg");

        //    fotopathKTP2 = folderPath + namafileJaminan;

        //    txtfoto2.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

        //    //Display the Picture in Image control.
        //    imgfoto2.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
   
    
    }
    protected void Uploadfoto3(object sender, EventArgs e)
    {
        //try
        //{
        //    string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
        //    DateTime now = DateTime.Now;

        //    namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS3";


        //    //Check whether Directory (Folder) exists.
        //    if (!Directory.Exists(folderPath))
        //    {
        //        //If Directory (Folder) does not exists. Create it.
        //        Directory.CreateDirectory(folderPath);
        //    }


        //    txtfoto3.Text = "";

        //    fileupload3.SaveAs(folderPath + namafileJaminan + ".jpg");

        //    fotopathKTP3 = folderPath + namafileJaminan;

        //    txtfoto3.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

        //    //Display the Picture in Image control.
        //    imgfoto3.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
  
    
    }
    protected void Uploadfoto4(object sender, EventArgs e)
    {
        //try
        //{
        //    string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
        //    DateTime now = DateTime.Now;

        //    namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS4";


        //    //Check whether Directory (Folder) exists.
        //    if (!Directory.Exists(folderPath))
        //    {
        //        //If Directory (Folder) does not exists. Create it.
        //        Directory.CreateDirectory(folderPath);
        //    }

        //    txtfoto4.Text = "";


        //    fileupload4.SaveAs(folderPath + namafileJaminan + ".jpg");

        //    fotopathKTP4 = folderPath + namafileJaminan;

        //    txtfoto4.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

        //    //Display the Picture in Image control.
        //    imgfoto4.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
   
    
    }
    protected void Uploadfoto5(object sender, EventArgs e)
    {
        //try
        //{
        //    string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
        //    DateTime now = DateTime.Now;

        //    namafileJaminan = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AS5";


        //    //Check whether Directory (Folder) exists.
        //    if (!Directory.Exists(folderPath))
        //    {
        //        //If Directory (Folder) does not exists. Create it.
        //        Directory.CreateDirectory(folderPath);
        //    }

        //    txtfoto5.Text = "";


        //    fileupload5.SaveAs(folderPath + namafileJaminan + ".jpg");

        //    fotopathKTP5 = folderPath + namafileJaminan;

        //    txtfoto5.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg";

        //    //Display the Picture in Image control.
        //    imgfoto5.ImageUrl = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".jpg" + "?r=" + DateTime.Now.Ticks.ToString();

        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
  
    
    
    }

    protected void btnlanjutkan_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    btnlanjutkan.Enabled = false;

           

        //    DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
        //    varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttglrealisasi.Text, typeof(DateTime));
        //    varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        //    varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //    varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        //    ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

        //    string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //    string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

        //    if (xsf != "0")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
        //        btnlanjutkan.Enabled = true;
        //    }
        //    else
        //    {






        //        DefaultClass.Global.SqlHelper[] var88 = new DefaultClass.Global.SqlHelper[11];
        //        var88[0] = gb.CreateSPParameter("@proses", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
        //        var88[1] = gb.CreateSPParameter("@userid", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        //        var88[2] = gb.CreateSPParameter("@alasan_tolak", ParameterDirection.Input, SqlDbType.VarChar, 200, txtalasantolak.Text, typeof(string));
        //        var88[3] = gb.CreateSPParameter("@siklus", ParameterDirection.Input, SqlDbType.VarChar, 50, "10", typeof(string));
        //        var88[4] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 50, HDid.Value, typeof(string));
        //        var88[5] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnoanggota.Text, typeof(string));
        //        var88[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVETELLER", typeof(string));
        //        var88[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //        var88[8] = gb.CreateSPParameter("@register", ParameterDirection.Input, SqlDbType.VarChar, 50, HDRegister.Value, typeof(string));
        //        var88[9] = gb.CreateSPParameter("@notelpon", ParameterDirection.Input, SqlDbType.VarChar, 20, txthpktp.Text, typeof(string));
        //        var88[10] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 100, txtemailsktp.Text, typeof(string));
        //        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTERKREDIT", var88, "constrmf");



        //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //        {
        //            //btnlanjutkan.Visible = false;
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','APPROVE TELLER SUKSES','top-right','success','1');", true);
        //            pnladd.Visible = false;
        //            pnlIdentitas.Visible = false;
        //            PnlFront.Visible = true;
        //            pnlFilter.Visible = true;
        //            pnlJaminan.Visible = false;
        //            pnlPengajuan.Visible = false;
        //            btnSimpan.Visible = false;
        //            BtnApproveAntrian.Visible = false;
        //            BtnBatalkanmohon.Visible = false;
        //            BtnApproveAntrian.Visible = false;
        //            BtnBatalkanmohon.Visible = false;
        //            btnlanjutkan.Visible = false;
        //            btnTolakAntrian.Visible = false;
        //            txtnonasabah.Text = "";
        //            txtketeranganJaminan.Text = "";
        //            txtkondisiJaminan.Text = "-";
        //            txttaksasi.Text = "0";
        //            txtktppenjamin.Text = "0";
        //            txtnamapenjamin.Text = "-";
        //            txtnamaibukandung.Text = "-";
        //            txtNPWP.Text = "0";
        //            txtnomerkk.Text = "0";
        //            txtcadsebelumnya.Text = "0";
        //            txtkembalicadangan.Text = "0";
        //            txtuangmuka.Text = "0";
        //            txtkaliuangmuka.Text = "0";
        //            TextBox2.Text = "0";
        //            txtcadanganRekening.Text = "";
        //            ddljangka.SelectedValue = "-";
        //            ddlstatus_SelectedIndexChanged(sender, e);
        //            Pnlupdatedata.Visible = false;
        //            pnlAlasanTolak.Visible = false;
        //            txtnonasabah.Focus();
        //        }

        //        else
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //            btnlanjutkan.Enabled = true;
        //        }
        //    }
        //}
        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //    btnlanjutkan.Enabled = true;
        //}



    }

    protected void btnPdfSumberDana_Click(object sender, EventArgs e)
    {

    }
}