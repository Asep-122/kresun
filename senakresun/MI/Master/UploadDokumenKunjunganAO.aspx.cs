using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MI_Master_UploadDokumenKunjunganAO : System.Web.UI.Page
{
    private readonly Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    private const int MaxSize = 2 * 1024 * 1024; // 2 MB
    private string finalPathDokumenNasabah = string.Empty;
    private string finalPathAplikasiNasabah = string.Empty;

    #region Session Properties
    private string SessLoginID
    {
        get { return Session["SessLoginID"] != null ? Session["SessLoginID"].ToString() : string.Empty; }
        set { Session["SessLoginID"] = value; }
    }

    private string SessBranchID
    {
        get { return Session["SessBranchID"] != null ? Session["SessBranchID"].ToString() : string.Empty; }
        set { Session["SessBranchID"] = value; }
    }

    private string SessKdpersh
    {
        get { return Session["SessKdpersh"] != null ? Session["SessKdpersh"].ToString() : string.Empty; }
        set { Session["SessKdpersh"] = value; }
    }

    private string SessKoperasiID
    {
        get { return Session["SessKoperasiID"] != null ? Session["SessKoperasiID"].ToString() : string.Empty; }
        set { Session["SessKoperasiID"] = value; }
    }
    #endregion

    #region ViewState Properties
    public string CultureName
    {
        get { return ViewState["CultureName"] != null ? (string)ViewState["CultureName"] : string.Empty; }
        set { ViewState["CultureName"] = value; }
    }

    public string no_simulasi
    {
        get { return ViewState["no_simulasi"] != null ? (string)ViewState["no_simulasi"] : string.Empty; }
        set { ViewState["no_simulasi"] = value; }
    }

    public string no_pensiun
    {
        get { return ViewState["no_pensiun"] != null ? (string)ViewState["no_pensiun"] : string.Empty; }
        set { ViewState["no_pensiun"] = value; }
    }

    public string nama_nasabah
    {
        get { return ViewState["nama_nasabah"] != null ? (string)ViewState["nama_nasabah"] : string.Empty; }
        set { ViewState["nama_nasabah"] = value; }
    }

    public string plafon
    {
        get { return ViewState["plafon"] != null ? (string)ViewState["plafon"] : string.Empty; }
        set { ViewState["plafon"] = value; }
    }

    public int approval_status
    {
        get { return ViewState["approval_status"] != null ? (int)ViewState["approval_status"] : 0; }
        set { ViewState["approval_status"] = value; }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessLoginID == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!Page.IsPostBack)
        {
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnUpload);

            if (!string.IsNullOrEmpty(Request.QueryString["no_simulasi"]))
            {
                no_simulasi = Request.QueryString["no_simulasi"];
                no_pensiun = Request.QueryString["no_pensiun"];
                nama_nasabah = Request.QueryString["nama_nasabah"];
                plafon = Request.QueryString["plafon"];

                BindData(no_simulasi);
            }
        }
    }

    private void BindForm() {
        txtNoSimulasi.Text = no_simulasi;
        txtPlafon.Text = plafon;
        txtNama.Text = nama_nasabah;
        txtNopen.Text = no_pensiun;
    }

    protected void BindData(string _no_simulasi)
    {

        var paramList = new List<DefaultClass.Global.SqlHelper>
        {   
            gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "SHOW_HISTORY_DOKUMEN_NASABAH", typeof(string)),
            gb.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, _no_simulasi, typeof(string)),
            gb.CreateSPParameter("@SessionId", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
            gb.CreateSPParameter("@KodePerusahaan", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string)),
            gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string))
        };

        ge.ListDataset = gb.GetDataJustOutPut("SI_SP_APPROVAL_KUNJUNGAN_AO", paramList.ToArray(), "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            txtNoSimulasi.Text = ge.ListDataset.Tables[0].Rows[0]["NoSimulasi"].ToString();
            txtPlafon.Text = ge.ListDataset.Tables[0].Rows[0]["Plafon"].ToString();
            txtNama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaPensiun"].ToString();
            txtNopen.Text = ge.ListDataset.Tables[0].Rows[0]["NomorPensiun"].ToString();
            txtPdfDokumenNasabah.Text = ge.ListDataset.Tables[0].Rows[0]["PathFileKunjunganDokumenNasabah"].ToString();
            txtPdfDokumenApliaksi.Text = ge.ListDataset.Tables[0].Rows[0]["PathFileKunjunganAplikasiNasabah"].ToString();

            btnUpload.Visible = false;

            object val = ge.ListDataset.Tables[0].Rows[0]["ApprovalStatus"];

            //Response.Write(val); Response.End(); //debug

            int? approvalStatus = (val == null || val == DBNull.Value)
                ? (int?)null
                : Convert.ToInt32(val);

            approval_status = approvalStatus ?? 0;

            bool canUpload = (!approvalStatus.HasValue || approvalStatus == 3);

            btnUpload.Visible = canUpload;
            fileuploadpdf_dokumen_nasabah.Enabled = canUpload;
            fileuploadpdf_dokumen_aplikasi.Enabled = canUpload;

            GRDataUploadHistory.DataSource = ge.ListDataset.Tables[1];
            GRDataUploadHistory.DataBind();
        }
        else 
        {
            BindForm();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fileuploadpdf_dokumen_nasabah.HasFile)
                finalPathDokumenNasabah = UploadPdf(fileuploadpdf_dokumen_nasabah,
                    "~/Files/Kresun/FileKunjunganMarketing/DokumenNasabah/", "Dokumen");

            if (fileuploadpdf_dokumen_aplikasi.HasFile)
                finalPathAplikasiNasabah = UploadPdf(fileuploadpdf_dokumen_aplikasi,
                    "~/Files/Kresun/FileKunjunganMarketing/AplikasiNasabah/", "Aplikasi");

            SaveData();

            SweetAlert("File berhasil disimpan.");
        }
        catch (Exception ex)
        {
            SweetAlert(ex.Message.ToString());
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        finally
        {
            //Response.Redirect("CreateSimulasi.aspx");
            BindData(no_simulasi);
        }
    }

    private string UploadPdf(FileUpload uploader, string relativeFolder, string suffix)
    {
        try
        {
            if (!IsPdf(uploader))
            {
                SweetAlert("File harus PDF!");
                return null;
            }

            if (uploader.PostedFile.ContentLength > MaxSize)
            {
                SweetAlert("Ukuran file maksimal 2 MB!");
                return null;
            }

            string folderPath = Server.MapPath(relativeFolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileBaseName = no_pensiun + "_" + no_simulasi + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + suffix;
            string finalFileName = fileBaseName + ".pdf";
            string uploadedFile = Path.Combine(folderPath, finalFileName);
            string compressedFile = Path.Combine(folderPath, fileBaseName + "_tmp.pdf");

            uploader.SaveAs(uploadedFile);
            PdfHelper.CompressPdf(uploadedFile, compressedFile);
            File.Delete(uploadedFile);
            File.Move(compressedFile, uploadedFile);

            return relativeFolder + finalFileName;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveData()
    {
        gb.BeginTran("constrmf");
        try
        {
			string action = (int)approval_status == 3 ? "REVISE" : "SUBMIT";
            var paramList = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, action, typeof(string)),
                gb.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 50, no_simulasi, typeof(string)),
                gb.CreateSPParameter("@PathFileKunjunganDokumenNasabah", ParameterDirection.Input, SqlDbType.VarChar, 500, finalPathDokumenNasabah ?? string.Empty, typeof(string)),
                gb.CreateSPParameter("@PathFileKunjunganAplikasiNasabah", ParameterDirection.Input, SqlDbType.VarChar, 500, finalPathAplikasiNasabah ?? string.Empty, typeof(string)),
                gb.CreateSPParameter("@CreatedBy", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
                gb.CreateSPParameter("@KodePerusahaan", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string)),
                gb.CreateSPParameter("@BranchID", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string))
            };

            gb.GetDataJustOutPut("SI_INSERT_DOKUMEN_KUNJUNGAN_AO", paramList.ToArray(), "constrmf");
            gb.CommitTran();
        }
        catch(Exception ex)
        {
            gb.RollBackTran();
            throw ex;
        }
    }

    private bool IsPdf(FileUpload fileUpload)
    {
        string ext = Path.GetExtension(fileUpload.FileName).ToLower();
        string contentType = fileUpload.PostedFile.ContentType.ToLower();
        return ext == ".pdf" && contentType == "application/pdf";
    }

    private void SweetAlert(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "javascript:SweetAlert('" + message + "');", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateSimulasi.aspx");
    }
}
