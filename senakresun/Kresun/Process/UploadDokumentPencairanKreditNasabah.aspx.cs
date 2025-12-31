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
using System.Activities.Statements;
using iTextSharp.text.pdf;



public partial class AR_Process_UploadDokumentPencairanKreditNasabah : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridEntity();
        }


    }

    public void BindGridEntity()
    {


        DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[6];
        x[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        x[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWVERIFIKASI", typeof(string));
        x[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        x[4] = gb.CreateSPParameter("@inisial", ParameterDirection.Input, SqlDbType.VarChar, 4, '6', typeof(string));
        x[5] = gb.CreateSPParameter("@tolak", ParameterDirection.Input, SqlDbType.Int, 0, 0, typeof(int));
        ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "idpermohonan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":

                try
                {
                    //string register
                    txtIdPermohonan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblRegister")).Text;
                    txtNoAnggota.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtNoRekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    txtSumberDana.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasd")).Text;
                    txtPlafon.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text;
                    txtNama.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamanasabah")).Text;
                    // Combo MO dan REF Non MO
                    PnlFront.Visible = false;
                    pnlData.Visible = true;
                    pnlUploadDokument.Visible = true;



                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                    var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[1] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNoRekening.Text, typeof(string));
                    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETPATH", typeof(string));
                    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtPdf1.Text = ge.ListDataset.Tables[0].Rows[0]["file_path1"].ToString();
                        txtfilename1.Text = ge.ListDataset.Tables[0].Rows[0]["file1"].ToString();
                        txtPdf2.Text = ge.ListDataset.Tables[0].Rows[0]["file_path2"].ToString();
                        txtfilename2.Text = ge.ListDataset.Tables[0].Rows[0]["file2"].ToString();
                        txtPdf3.Text = ge.ListDataset.Tables[0].Rows[0]["file_path3"].ToString();
                        txtfilename3.Text = ge.ListDataset.Tables[0].Rows[0]["file3"].ToString();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                    
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

        }


    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("UploadDokumentPencairanKreditNasabah.aspx");

    }

    protected void btnPdf1_Click(object sender, EventArgs e)
    {
        try
        {
            string extension = Path.GetExtension(fileuploadpdf1.FileName).ToLower();
            if (extension != ".pdf")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya file PDF yang diperbolehkan!');", true);
                return;
            }

            int maxSize = 2 * 1024 * 1024; // 5 MB
            if (fileuploadpdf1.PostedFile.ContentLength > maxSize)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript",
                    "javascript:SweetAlert('Ukuran file maksimal 2 MB!');", true);
                return;
            }

            string folderPath = Server.MapPath("~/Files/Kresun/FilePencairan/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtNoRekening.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "UP1";
            txtfilename1.Text = namafileJaminan + ".pdf";



            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtPdf1.Text = "";

            string uploadedFile = Path.Combine(folderPath, namafileJaminan + ".pdf");
            string compressedFile = Path.Combine(folderPath, namafileJaminan + "_tmp.pdf");

            fileuploadpdf1.SaveAs(uploadedFile);

            CompressPdf(uploadedFile, compressedFile);
            File.Delete(uploadedFile);
            File.Move(compressedFile, uploadedFile);

            txtPdf1.Text = "~/Files/Kresun/FilePencairan/" + namafileJaminan + ".pdf";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + txtPdf1.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    
    protected void btnPdf2_Click(object sender, EventArgs e)
    {
        try
        {
            string extension = Path.GetExtension(fileuploadpdf2.FileName).ToLower();
            if (extension != ".pdf")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya file PDF yang diperbolehkan!');", true);
                return;
            }

            int maxSize = 2 * 1024 * 1024; // 5 MB
            if (fileuploadpdf2.PostedFile.ContentLength > maxSize)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript",
                    "javascript:SweetAlert('Ukuran file maksimal 2 MB!');", true);
                return;
            }

            string folderPath = Server.MapPath("~/Files/Kresun/FilePencairan/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtNoRekening.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "UP2";
            txtfilename2.Text = namafileJaminan + ".pdf";



            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtPdf2.Text = "";

            string uploadedFile = Path.Combine(folderPath, namafileJaminan + ".pdf");
            string compressedFile = Path.Combine(folderPath, namafileJaminan + "_tmp.pdf");

            fileuploadpdf2.SaveAs(uploadedFile);

            CompressPdf(uploadedFile, compressedFile);
            File.Delete(uploadedFile);
            File.Move(compressedFile, uploadedFile);

            txtPdf2.Text = "~/Files/Kresun/FilePencairan/" + namafileJaminan + ".pdf";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + txtPdf2.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    } 
    
    protected void btnPdf3_Click(object sender, EventArgs e)
    {
        try
        {
            string extension = Path.GetExtension(fileuploadpdf3.FileName).ToLower();
            if (extension != ".pdf")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya file PDF yang diperbolehkan!');", true);
                return;
            }

            int maxSize = 2 * 1024 * 1024; // 5 MB
            if (fileuploadpdf3.PostedFile.ContentLength > maxSize)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript",
                    "javascript:SweetAlert('Ukuran file maksimal 2 MB!');", true);
                return;
            }

            string folderPath = Server.MapPath("~/Files/Kresun/FilePencairan/");
            DateTime now = DateTime.Now;

            namafileJaminan = txtNoRekening.Text + "_" + now.ToString("ddMMyyyyhhmmss") + "_" + "UP3";
            txtfilename3.Text = namafileJaminan + ".pdf";



            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtPdf3.Text = "";

            string uploadedFile = Path.Combine(folderPath, namafileJaminan + ".pdf");
            string compressedFile = Path.Combine(folderPath, namafileJaminan + "_tmp.pdf");

            fileuploadpdf3.SaveAs(uploadedFile);

            CompressPdf(uploadedFile, compressedFile);
            File.Delete(uploadedFile);
            File.Move(compressedFile, uploadedFile);

            txtPdf3.Text = "~/Files/Kresun/FilePencairan/" + namafileJaminan + ".pdf";
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + txtPdf3.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
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



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[10];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 10, txtNoRekening.Text, typeof(string));
            var1[2] = gb.CreateSPParameter("@File1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfilename1.Text, typeof(string));
            var1[3] = gb.CreateSPParameter("@file_path1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtPdf1.Text, typeof(string));
            var1[4] = gb.CreateSPParameter("@File2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfilename2.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@file_path2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtPdf2.Text, typeof(string));
            var1[6] = gb.CreateSPParameter("@File3", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfilename3.Text, typeof(string));
            var1[7] = gb.CreateSPParameter("@file_path3", ParameterDirection.Input, SqlDbType.VarChar, 100, txtPdf3.Text, typeof(string));
            var1[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEFILE", typeof(string));
            var1[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_MASTER_PERMOHONAN_NEW", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','top-right','success','1');", true);
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected string btnafterDelete()
    {

        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

}