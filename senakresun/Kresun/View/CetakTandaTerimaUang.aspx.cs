using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Kresun_View_CetakTandaTerimaUang : System.Web.UI.Page
{

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    string noRekening = "";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridEntity();
        }

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.btnSimpan);
        //Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakTandaTerimaUang.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakTandaTerimaUang.aspx");
    }

    public void BindGridEntity()
    {
        var paramList = new List<DefaultClass.Global.SqlHelper>
        {
            gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "LIST", typeof(string)),
            gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
            gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string))
        };

        ge.ListDataTable = gb.GetDataJustOutPut("KP_CETAK_TANDA_TERIMA_UANG_NASABAH", paramList.ToArray(), "constrmf").Tables[0];

        GRData.DataSource = ge.ListDataTable;
        GRData.DataBind();

        GRData_Draw_Header_DataTable();

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            noRekening = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
            CetakCategoryTandaTerima(noRekening);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message + "');", true);
            GRData_Draw_Header_DataTable();
        }
    }

    private void CetakCategoryTandaTerima(string _noRekening)
    {
        string reportPath = Server.MapPath("~/Kresun/Report/TandaTerimaUang/KP_TANDA_TERIMA_UANG_NASABAH.rpt");
        using (ReportDocument rprt = new ReportDocument())
        {

            var paramList = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAK", typeof(string)),
                gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, _noRekening, typeof(string)),
                gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
                gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string))
            };

            ge.ListDataTable = gb.GetDataJustOutPut("KP_CETAK_TANDA_TERIMA_UANG_NASABAH", paramList.ToArray(), "constrmf").Tables[0];

            if (!System.IO.File.Exists(reportPath))
                throw new FileNotFoundException("Report file not found", reportPath);

            rprt.Load(reportPath);
            rprt.SetDataSource(ge.ListDataTable);

            Response.ClearContent();
            Response.ClearHeaders();

            ReportHelper.ExportPdfInline(rprt, "TANDA_TERIMA_UANG_" + _noRekening, Response);
        }
    }

    private void GRData_Draw_Header_DataTable()
    {
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}