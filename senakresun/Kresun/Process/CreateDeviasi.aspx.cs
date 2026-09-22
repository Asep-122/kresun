using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;



public partial class KP_Process_CreateDeviasi : System.Web.UI.Page
{
    
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gbx = new DefaultClass.Global();
    GlobalEntities gex = new GlobalEntities();


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

        DivReasonVisibility();

    }

    private void DivReasonVisibility() {
        switch (ddlPurpose.SelectedValue.ToLower())
        {
            case "deviasi":
                divReason.Visible = true;
                break;

            case "lain - lain":
                divReason.Visible = true;
                break;

            default:
                divReason.Visible = false;
            break;
        }
    }

    public void BindGridEntity()
    {


        DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[4];
        x[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CREATELISTDEVIASI", typeof(string));
        x[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_Deviasi", x, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "CreateDate desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnlData.Visible = true;
        btnDelete.Visible = false;
        pnlUploadDokument.Visible = true;
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
                    txtNamaDebitur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamadebitur")).Text;
                    txtNoPen.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopensiun")).Text;
                    txtTo.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblto")).Text;
                    txtTitle.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltitle")).Text;
                    ddlDept.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldept")).Text;

                    //txtPurpose.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpurpose")).Text;
                    var lbl = GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpurpose") as Label;

                    ddlPurpose.SelectedIndex = 0;

                    if (lbl != null && !string.IsNullOrWhiteSpace(lbl.Text) && ddlPurpose.Items.FindByValue(lbl.Text) != null)
                    {
                        ddlPurpose.SelectedValue = lbl.Text;
                    }
                    txtReason.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblreason")).Text;

                    txtNoBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txtPrihal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperihal")).Text;
                    hdnEditorContent.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbleditor")).Text;
                    string safeValue = hdnEditorContent.Value.Replace("'", "\\'");
                    string script = "setTimeout(function() { if(window.quill) quill.root.innerHTML = '" + safeValue + "'; }, 500);";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetQuillContent", script, true);

                    PnlFront.Visible = false;
                    pnlData.Visible = true;
                    pnlUploadDokument.Visible = true;
                    pnlHeader.Visible = false;

                    DivReasonVisibility();

                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "PDF":

                try
                {

                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTDEVIASI", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@Level", ParameterDirection.Input, SqlDbType.Int, 10, 0, typeof(int));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("KP_Deviasi", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/Kresun/Report/Deviasi.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


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
            case "Approve":
                try
                {



                    string NoBukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                    var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVECREATEDEVIASI", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[4] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, NoBukti, typeof(string));
                    var1[5] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_Deviasi", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','zzzzzz','top-right','success','true');", true);
                    }

                    GRData.UseAccessibleHeader = true;
                    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
            case "SETDELETE":
                try
                {

                    txtTo.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblto")).Text;
                    txtTitle.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltitle")).Text;
                    ddlDept.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldept")).Text;

                    //txtPurpose.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpurpose")).Text;
                    var lbl = GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpurpose") as Label;

                    ddlPurpose.SelectedIndex = 0;

                    if (lbl != null && !string.IsNullOrWhiteSpace(lbl.Text) && ddlPurpose.Items.FindByValue(lbl.Text) != null)
                    {
                        ddlPurpose.SelectedValue = lbl.Text;
                    }

                    txtReason.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblreason")).Text;

                    txtNoBukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txtPrihal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblperihal")).Text;
                    hdnEditorContent.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbleditor")).Text;
                    string safeValue = hdnEditorContent.Value.Replace("'", "\\'");
                    string script = "setTimeout(function() { if(window.quill) quill.root.innerHTML = '" + safeValue + "'; }, 500);";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SetQuillContent", script, true);

                    PnlFront.Visible = false;
                    pnlData.Visible = true;
                    pnlData.Enabled = false;
                    pnlUploadDokument.Visible = true;
                    pnlHeader.Visible = false;
                    btnSimpan.Visible = false;

                    DivReasonVisibility();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;

        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateDeviasi.aspx");

    }


    protected string btnafterDelete()
    {

        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = (string.IsNullOrEmpty(txtNoBukti.Text) ? "INSERTDEVIASI" : "UPDATEDEVIASI");
        string editorContent =  hdnEditorContent.Value;

        // Contoh: simpan ke database atau tampilkan


        try
        {
            string date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            string bukti = "DEV" + date.ToString();

            string nobukti = (string.IsNullOrEmpty(txtNoBukti.Text) ? bukti : txtNoBukti.Text);

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[15];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[4] = gb.CreateSPParameter("@To", ParameterDirection.Input, SqlDbType.VarChar, 50, txtTo.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@Title", ParameterDirection.Input, SqlDbType.VarChar, 50, txtTitle.Text, typeof(string));
            var1[6] = gb.CreateSPParameter("@Dept", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlDept.SelectedValue, typeof(string));
            //var1[7] = gb.CreateSPParameter("@Purpose", ParameterDirection.Input, SqlDbType.VarChar, 50, txtPurpose.Text, typeof(string));
            var1[7] = gb.CreateSPParameter("@Purpose", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlPurpose.SelectedValue.ToString(), typeof(string));
            var1[8] = gb.CreateSPParameter("@Perihal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtPrihal.Text, typeof(string));
            var1[9] = gb.CreateSPParameter("@Editor", ParameterDirection.Input, SqlDbType.Text, 0, editorContent, typeof(string));
            var1[10] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var1[11] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            var1[12] = gb.CreateSPParameter("@Reason", ParameterDirection.Input, SqlDbType.VarChar, 100, txtReason.Text, typeof(string));
            var1[13] = gb.CreateSPParameter("@NamaDebitur", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNamaDebitur.Text, typeof(string));
            var1[14] = gb.CreateSPParameter("@NoPensiun", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNoPen.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_Deviasi", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully simpan data','top-right','success','true');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string NoBukti = txtNoBukti.Text;

                    DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[5];
                    x[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEDEVIASI", typeof(string));
                    x[2] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, NoBukti, typeof(string));
                    x[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    x[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_DEVIASI", x, "constrmf");
        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully delete data','top-right','success','true');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "','success','true');", true);

        }
    }
}