using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;


public partial class SN_Process_Sukarela_CetakBuTab : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
    }

        protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnprint);

    }
    protected void btnprinttagih_Click(object sender, EventArgs e)
    {
    } 
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        { 
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];

            var[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, hdncustomer.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKBUTAB", typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[5] = gb.CreateSPParameter("@month", ParameterDirection.Input, SqlDbType.Int, 50, "-" + ddlbulan.SelectedValue, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("SS_BUKUTABUNGAN", var, "constrmf").Tables[0];

            rprt.Load(Server.MapPath("~/SN/Process/Sukarela/ReportBukuTabungan.rpt"));

            rprt.SetDataSource(ge.ListDataTable);


            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


            Response.End();


            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('test3!');", true);
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }
            else
            {
                rprt.Dispose();
                rprt.Close();
            }

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlGL.Visible = true;

        var kodelgn = hdncustomer.Value;
        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
        var3[0] = gb.CreateSPParameter("@noRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
        var3[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASI", typeof(string));
        var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var3[4] = gb.CreateSPParameter("@month", ParameterDirection.Input, SqlDbType.Int, 50, "-" +  ddlbulan.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_BUKUTABUNGAN", var3, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_transaksi asc";


        GGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GGL.DataBind();

        GGL.UseAccessibleHeader = true;

        GGL.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected string btnprint_Click()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessKoperasiIDx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();


        string no_nasabah = InputText;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchIDx, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah.Trim(), typeof(String));
        Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUTAB", typeof(string));
        Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SS_BUKUTABUNGAN", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

}