using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Shared.Json;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MI_Master_Approval_Dokumen_Kunjungan : System.Web.UI.Page
{

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "", fotopathKK = "";
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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            if (!Page.IsPostBack)
            {
                BindGridEntityx();
                loadVisibilityInit();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    public void BindGridEntityx()
    {
        try
        {
            var paramList = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "LIST_DATA_KUNJUNGAN", typeof(string)),
                gb.CreateSPParameter("@KodePerusahaan", ParameterDirection.Input, SqlDbType.VarChar, 500, SessKdpersh, typeof(string)),
                gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 500, SessBranchID, typeof(string)),
                gb.CreateSPParameter("@SessionId", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string))
            };

            ge.ListDataset = gb.GetDataJustOutPut("SI_SP_APPROVAL_KUNJUNGAN_AO", paramList.ToArray(), "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TglSimulasi ASC";

            GRData.DataSource = ge.ListDataset.Tables[0];
            GRData.DataBind();

            reDraw_Table_GRData();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "DOKUMEN":

                    string no_simulasi = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrequest")).Text;
                    string no_pensiun = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopen")).Text;
                    string nama_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                    string plafon = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafond")).Text;

                    txtNoSimulasi.Text = no_simulasi;
                    txtNopen.Text = no_pensiun;
                    txtNama.Text = nama_nasabah;
                    txtPlafon.Text = plafon;

                    BindData(no_simulasi);
                    dokumenVisibility();

                    break;
                //case "APLIKASI":
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Mau ngapain bang?');", true);
                //break;
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        finally
        {
            reDraw_Table_GRData();
        }
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
            //txtPdfDokumenNasabah.Text = ge.ListDataset.Tables[0].Rows[0]["PathFileKunjunganDokumenNasabah"].ToString();
            //txtPdfDokumenApliaksi.Text = ge.ListDataset.Tables[0].Rows[0]["PathFileKunjunganAplikasiNasabah"].ToString();

            //btnUpload.Visible = false;

            var pdfPathDokumenNasabah = ResolveUrl(ge.ListDataset.Tables[0].Rows[0]["PathFileKunjunganDokumenNasabah"].ToString());
            var pdfPathAplikasiNasabah = ResolveUrl(ge.ListDataset.Tables[0].Rows[0]["PathFileKunjunganAplikasiNasabah"].ToString());
            iframePDFDokumenNasabah.Attributes["src"] = pdfPathDokumenNasabah;
            iframePDFAplikasiNasabah.Attributes["src"] = pdfPathAplikasiNasabah;

            if (string.IsNullOrEmpty(pdfPathDokumenNasabah.Trim()))
            {
                btnViewDokumenNasabah.Enabled = false;
            }
            if (string.IsNullOrEmpty(pdfPathAplikasiNasabah.Trim()))
            {
                btnViewAplikasiNasabah.Enabled = false;
            }
            if (string.IsNullOrEmpty(pdfPathDokumenNasabah.Trim()) && string.IsNullOrEmpty(pdfPathAplikasiNasabah.Trim()))
            {
                btnHide.Enabled = false;
            }

            object val = ge.ListDataset.Tables[0].Rows[0]["ApprovalStatus"];
            int? approvalStatus = val == DBNull.Value ? (int?)null : Convert.ToInt32(val);

            bool canDoAction = approvalStatus.HasValue && (approvalStatus == 1 || approvalStatus == 4);

            txtNote.Visible = canDoAction;
            btnApprove.Visible = canDoAction;
            btnRevise.Visible = canDoAction;
            btnReject.Visible = canDoAction;

            GRDataUploadHistory.DataSource = ge.ListDataset.Tables[1];
            GRDataUploadHistory.DataBind();
        }
        else
        {
            SweetAlert("Data Not Found!");
        }
    }

    private void reDraw_Table_GRData()
    {
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    private void loadVisibilityInit() {
        PnlFront.Visible = true;
        pnlData.Visible = false ;
    }

    private void dokumenVisibility()
    {
        PnlFront.Visible = false;
        pnlData.Visible = true;
        pnlApproval.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovalDokumenKunjungan.aspx");
    }

    protected void btnViewDokumenNasabah_Click(object sender, EventArgs e)
    {

        //string pdfPath = ResolveUrl("~/Files/Kresun/FileKunjunganMarketing/AplikasiNasabah/454654654654654_AR0701101000011_24092025150711_Aplikasi.pdf");
        //iframePDFDokumenNasabah.Attributes["src"] = pdfPath;
        PanelPDFDokumen.Visible = true;
        iframePDFDokumenNasabah.Visible = true;

        iframePDFAplikasiNasabah.Visible = false;
        //iframePDFAplikasiNasabah.Attributes.Remove("src");
    }

    protected void btnViewAplikasiNasabah_Click(object sender, EventArgs e)
    {
        //string pdfPath = ResolveUrl("~/Files/Kresun/FileKunjunganMarketing/AplikasiNasabah/454654654654654_AR0701101000011_24092025150711_Aplikasi.pdf");
        //iframePDFAplikasiNasabah.Attributes["src"] = pdfPath;
        PanelPDFDokumen.Visible = true;
        iframePDFAplikasiNasabah.Visible = true;

        iframePDFDokumenNasabah.Visible = false;
        //iframePDFDokumenNasabah.Attributes.Remove("src");
    }

    private void ProcessDokumenApproval(string action)
    {
        gb.BeginTran("constrmf");
        try
        {
            var paramList = new List<DefaultClass.Global.SqlHelper>
        {
            gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, action, typeof(string)),
            gb.CreateSPParameter("@KodePerusahaan", ParameterDirection.Input, SqlDbType.VarChar, 6, SessKdpersh, typeof(string)),
            gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 6, SessBranchID, typeof(string)),
            gb.CreateSPParameter("@SessionId", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
            gb.CreateSPParameter("@NoSimulasi", ParameterDirection.Input, SqlDbType.VarChar, 20, txtNoSimulasi.Text.Trim(), typeof(string)),
            gb.CreateSPParameter("@AdminNote", ParameterDirection.Input, SqlDbType.VarChar, 255, txtNote.Text.Trim(), typeof(string))
        };

            gb.GetDataJustOutPut("SI_SP_APPROVAL_KUNJUNGAN_AO", paramList.ToArray(), "constrmf");
            gb.CommitTran();

            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{action.Replace("_", " ")} berhasil!');", true);
            SweetAlert(action.Replace("_", " ") + " berhasil!");

            BindData(txtNoSimulasi.Text.Trim());
        }
        catch (Exception ex)
        {
            gb.RollBackTran();
            SweetAlert(ex.Message);
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Terjadi error: {ex.Message}');", true);
        }
    }

    // Event handlers
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        ProcessDokumenApproval("APPROVE_DOKUMEN_KUNJUNGAN");
    }

    protected void btnRevise_Click(object sender, EventArgs e)
    {
        ProcessDokumenApproval("REVISE_DOKUMEN_KUNJUNGAN");
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        ProcessDokumenApproval("REJECT_DOKUMEN_KUNJUNGAN");
    }


    protected void btnHide_Click(object sender, EventArgs e)
    {
        PanelPDFDokumen.Visible = false;
    }

    private void SweetAlert(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "javascript:SweetAlert('" + message + "');", true);
    }
}