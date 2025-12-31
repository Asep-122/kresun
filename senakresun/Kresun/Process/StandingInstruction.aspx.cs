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



public partial class KP_Process_StandingInstruction : System.Web.UI.Page
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


    }

    public void BindGridEntity()
    {


        DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[3];
        x[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        x[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        x[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTSTANDINGINSTRUCTION", typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_StandingInstruction", x, "constrmf");



        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        if (SessBranchID == "000")
        {
            btnAdd.Visible = false;
        }
        else
        {
            btnAdd.Visible = true;

        }

        for (int i = 0; i < GRData.Rows.Count; i++)
            {
                GridViewRow row = GRData.Rows[i];
                LinkButton btnApprove = (LinkButton)row.FindControl("btnApproveAntrian");
                LinkButton btnedit = (LinkButton)row.FindControl("btnedit");
                LinkButton btnDelete = (LinkButton)row.FindControl("btnDelete");
                if (SessBranchID == "000")
                {
                    if (btnApprove != null)
                        btnApprove.Visible = false;

                    if (btnedit != null)
                        btnedit.Visible = false;

                    if (btnDelete != null)
                        btnDelete.Visible = false;
                }
                else
                {
                    if (btnApprove != null)
                        btnApprove.Visible = true;

                    if (btnedit != null)
                        btnedit.Visible = true;

                    if (btnDelete != null)
                        btnDelete.Visible = true;
                }

            }

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
                    txtNoBukti.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txtNoRekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    txtNoRekening2.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
                    txtNama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                    txtNamaBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                    txtNoPinjaman.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopinjaman")).Text;
                    txtNoPk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopk")).Text;
                    txtNoPensiun.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnopensiun")).Text;
                    ddlJnsInstruction.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljenisinstruction")).Text;
                    txtKoperasi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkoperasiasal")).Text;
                    txtNoKtp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoktp")).Text;
                    txtBankRekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbankrekening")).Text;
                    txtTerimaBersih.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblterimabersih")).Text;


                    txtNoRekening.Enabled = false;
                    PnlFront.Visible = false;
                    pnlData.Visible = true;
                    pnlUploadDokument.Visible = true;
                    pnlHeader.Visible = false;
                    btnDelete.Visible = false;


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

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[4];
                    Paramsc[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTSTANDINGINSTRUCTION", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("KP_StandingInstruction", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/Kresun/Report/ReportStandingInstruction.rpt"));
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
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVESTANDINGINSTRUCTION", typeof(string));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[3] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, NoBukti, typeof(string));
                    var1[4] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_StandingInstruction", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','zzzzzz','top-right','success','1');", true);
                    }

                    GRData.UseAccessibleHeader = true;
                    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('" + ex.Message.ToString().Replace("'", "") + "');", true);
                    gb.SaveErroLog(ex.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
            case "SETDELETE":
                try
                {




                    PnlFront.Visible = false;
                    pnlData.Visible = true;
                    pnlData.Enabled = false;
                    pnlUploadDokument.Visible = true;
                    pnlHeader.Visible = false;
                    btnSimpan.Visible = false;


                    
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
        Response.Redirect("StandingInstruction.aspx");

    }


    protected string btnafterDelete()
    {

        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
//        string STATE = (string.IsNullOrEmpty(txtNoBukti.Text) ? "INSERTDEVIASI" : "UPDATEDEVIASI");


        try
        {
            if(ddlJnsInstruction.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Error!','Mohon pilih Jenis Instruction terlebih dahulu','success','true');", true);

                return;
            }
            string date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            string nobukti = "SI" + date.ToString();


            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[10];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTSTANDINGINSTRUCTION", typeof(string));
            var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[3] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 50, NoRekeningHiden.Value, typeof(string));
            var1[4] = gb.CreateSPParameter("@JenisInstruction", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJnsInstruction.SelectedValue, typeof(string));
            var1[5] = gb.CreateSPParameter("@koperasiasal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKoperasi.Text, typeof(string));
            var1[6] = gb.CreateSPParameter("@nilai", ParameterDirection.Input, SqlDbType.Decimal, 50, Convert.ToDecimal(txtNilai.Text.Replace(",",".")), typeof(decimal));
            var1[7] = gb.CreateSPParameter("@IdPermohonan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtIdPermohonan.Value, typeof(string));
            var1[8] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var1[9] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_StandingInstruction", var1, "constrmf");
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
        string NoBukti = txtNoBukti.Value;

                    DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[5];
                    x[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETESTANDINGINSTRUCTION", typeof(string));
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

    [WebMethod]
    public static string GetNoRekening(string InputText)
    {
        List<string[]> listResult = new List<string[]>();

        string no_Bukti = InputText;
        string STATE = "SEARCHNOREKENING";

        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();


        ge.ListDataset = GetData(STATE, InputText).ListDataset;
        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;
    }

    protected static GlobalEntities GetData(string STATE, string NoRekening)
    {
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
        Params[1] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 50, NoRekening, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_StandingInstruction", Params, "constrmf");

        return ge;
    }

    protected void ddlJnsInstruction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJnsInstruction.SelectedValue == "Dantal")
        {
            divKoperasi.Visible = true;
        }
        else
        {
            divKoperasi.Visible = false;
        }
    }
}