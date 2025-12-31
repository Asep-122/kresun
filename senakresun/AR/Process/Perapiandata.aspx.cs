using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
 
public partial class GL_Perapiandata : System.Web.UI.Page
{
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

    DataTable dtRP = new DataTable("Transaction");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
               

                panelPosting.Visible = true;
                //pnlFilter.Visible = true;
                //BindGridEntity();
                BindGridEntity();
            }
           
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }
 


    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] param = new DefaultClass.Global.SqlHelper[4];
        //param[0] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlaolama.SelectedValue, typeof(string));
        param[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAP", typeof(string));
        param[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        param[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        param[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_Over_Cup", param, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    decimal debet = 0, kredit = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            debet += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_awal_pokok"));
            kredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_awal_wajib"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalsaldo_awal_pokok = (Label)e.Row.FindControl("lbltotalsaldo_awal_pokok");
            lbltotalsaldo_awal_pokok.Text = string.Format("{0:N2}", debet);
            Label lblsaldo_awal_wajib = (Label)e.Row.FindControl("lblsaldo_awal_wajib");
            lblsaldo_awal_wajib.Text = string.Format("{0:N2}", kredit);
        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
    }
    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = true;
      
        Pnlfooter.Visible = false;
        Response.Redirect("MasuktrnChangeRekening.aspx");

    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {




            foreach (GridViewRow row in GRData.Rows)
            {
                CheckBox ChkBoxHeader = (CheckBox)row.FindControl("ckbx");

                string nobukti = ((Label)row.FindControl("lblnoanggota")).Text;

                if (ChkBoxHeader.Checked == true)
                {

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RAPIHDATA", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Over_Cup", Paramsc, "constrmf");


                }
            }
            BindGridEntity();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Process Done');", true);

        
        }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

  
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{


        //    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //    Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    Params[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNMGL", typeof(String));
        //    Params[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        //    ge.ListDataset = gb.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", Params, "constrmf");
        //    ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



        //    DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        //    xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    xParams[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKDGL", typeof(String));
        //    xParams[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //    gex.ListDataset = gbx.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", xParams, "constrmf");
        //    gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");


        //    DefaultClass.Global.SqlHelper[] xParamsCC = new DefaultClass.Global.SqlHelper[3];
        //    xParamsCC[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    xParamsCC[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));
        //    xParamsCC[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //    gex.ListDataset = gbx.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", xParamsCC, "constrmf");
        //    gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");




        //    ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
        //    ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;

        //    ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;



        //    if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
        //    {
        //        ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

        //    }

        //    GRRP.UseAccessibleHeader = true;
        //    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //}
    }

  
    protected void ckbx_checked(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GRData.HeaderRow.FindControl("chkHeader");
        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
        GRData.UseAccessibleHeader = true; 
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}
