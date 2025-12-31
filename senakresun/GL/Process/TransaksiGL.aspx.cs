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

public partial class GL_Process_TransaksiGL : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Reports rpt = new Reports();


    DataTable dtRP = new DataTable("RencanaPerjalanan");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                //txtNama.Attributes.Add("readonly", "true");
                //txtNamaS.Attributes.Add("readonly", "true");
                //txtJBTN.Attributes.Add("readonly", "true");
                //txtCBG.Attributes.Add("readonly", "true");
                //txtGRADE.Attributes.Add("readonly", "true");


                //txtJBTNS.Attributes.Add("readonly", "true");
                //txtBRS.Attributes.Add("readonly", "true");
                //txtGRDS.Attributes.Add("readonly", "true");

                callTahun();

                ddlMonth.SelectedValue = DateTime.Now.ToString("MM");
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();


                BindGridEntity();

               // strpathsp = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/attach/sign/";
            }
        }
        catch (System.Exception X)
        {
            ltlMsg.Text = " <div class=\"alert bg-green alert-dismissible\" role=\"alert\"> <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + X.Message.ToString() + "</div>";

            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    private void callTahun()
    {

        int curYear = DateTime.Now.Year - 4;

        for (int i = 1; i < 6; ++i)
        {

            ListItem tmp = new ListItem();

            tmp.Value = curYear.ToString();

            tmp.Text = curYear.ToString();

            ddlYear.Items.Add(tmp);

            curYear = DateTime.Now.AddYears(i).Year - 4;
        }

    }
    public void BindGridEntity()
    {

        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} {1} ", "where kdpersh = '" + SessKdpersh + "' and modul = 'GL' and month(TglTrn) = '" + ddlMonth.SelectedValue + "' and year(TglTrn) = '" + ddlYear.SelectedValue + "'", txtSearch.Text.Trim() != "" ? " and " + ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, -1, WhereCond, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GRIDTransaksiView", Params, "constrmf");

        GRData.DataSource = ge.ListDataset;
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void btnAdd_ServerClick(object sender, EventArgs e)
    {
        PnlFront.Visible = false;
        pnlSPJ.Visible = true;

       // txtNOSPJ.Enabled = false;



        SetDetailRP();

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {


            int dgcount = GRRP.Rows.Count;

            dtRP.Columns.Clear();

            dtRP = (DataTable)ViewState["ListDetailRP"];

            dtRP.Rows.Clear();


            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtRP.NewRow();

                dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue : (object)DBNull.Value;
                dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                dr["costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                dr["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                dr["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;
                dtRP.Rows.Add(dr);
            }


           

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[15];

            Params[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNobukti.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@Date", ParameterDirection.Input, SqlDbType.VarChar, 20,txtDate, typeof(string));
            Params[2] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 15, ddltypetrn.SelectedValue, typeof(string));
            Params[3] = gb.CreateSPParameter("@currency", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcurrency.SelectedValue, typeof(string));
            Params[4] = gb.CreateSPParameter("@kurs", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkurs.Text, typeof(decimal));
            Params[5] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar,150, txtnote.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("HCIS_SPJ_InsertDetail", Params, "constrhrms");

            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                txtNobukti.Text = ge.ListDataset.Tables[0].Rows[0]["Nobukti"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "showSuccessMessage();", true);

            }

            else
            {

                ltlMsg.Text = " <div class=\"alert bg-green alert-dismissible\" role=\"alert\"> <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "</div>";

                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }

        }

        catch (Exception X)
        {
            ltlMsg.Text = " <div class=\"alert bg-green alert-dismissible\" role=\"alert\"> <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + X.Message.ToString() + "</div>";

            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void ddlSaranaB_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;

        string value = ((DropDownList)grow.FindControl("ddlSaranaB")).SelectedValue;

        switch (value)
        {
            case "3":
                ((TextBox)grow.FindControl("txtBandaraB")).Attributes.Remove("readonly");
                ((TextBox)grow.FindControl("txtBandaraB")).Attributes.Add("required", "required");
                ((TextBox)grow.FindControl("txtBandaraT")).Attributes.Remove("readonly");
                ((TextBox)grow.FindControl("txtBandaraT")).Attributes.Add("required", "required");
                break;

            default:
                {
                    ((TextBox)grow.FindControl("txtBandaraB")).Attributes.Add("readonly", "true");
                    ((TextBox)grow.FindControl("txtBandaraB")).Attributes.Remove("required");
                    ((TextBox)grow.FindControl("txtBandaraT")).Attributes.Add("readonly", "true");
                    ((TextBox)grow.FindControl("txtBandaraT")).Attributes.Remove("required");
                    break;
                }
        }

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblAPVID1")).Text != "" && ((Label)e.Row.FindControl("lblStatus")).Text == "1")
            {
                ((LinkButton)e.Row.FindControl("btnedit")).Visible = false;
            }

            else if (((Label)e.Row.FindControl("lblStatus")).Text == "2" || ((Label)e.Row.FindControl("lblStatus")).Text == "3")
            {
                ((LinkButton)e.Row.FindControl("btnedit")).Visible = false;
            }

            else
            {
                ((LinkButton)e.Row.FindControl("btnedit")).Visible = true;
            }
        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "EDT":

                try

                {
                    PnlFront.Visible = false;
                    pnlSPJ.Visible = true;

               
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[0];



                    ge.ListDataset = gb.GetDataJustOutPut("dbo.HCIS_SPJ_GETJenisTugas", Params, "constrhrms");
                   

                   
                    DataTable dtchecklist = new DataTable();

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[1];
                    Paramsc[0] = gb.CreateSPParameter("@NoSPJ", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNobukti.Text, typeof(string));

                    dtchecklist = gb.GetDataJustOutPut("dbo.HCIS_Get_Tugas", Paramsc, "constrhrms").Tables[0];


                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.HCIS_SPJ_Get_RencanaPerjalanan", Paramsc, "constrhrms").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        ViewState["ListDetailRP"] = ge.ListDataTable;

                        GRRP.DataSource = ge.ListDataTable;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        cssGridRP();
                    }
                    else
                    {
                        SetDetailRP();
                    }
                }

                catch (Exception X)
                {
                    ltlMsg.Text = " <div class=\"alert bg-green alert-dismissible\" role=\"alert\"> <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + X.Message.ToString() + "</div>";

                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

       
    private void SetDetailRP()
    {

        DataRow dr = null;

        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));
       
        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


        dr = dtRP.NewRow();
        dr["nomor"] = 1;
        dr["KodeGL"] = string.Empty;
        dr["NamaGL"] = string.Empty;
        dr["Description"] = string.Empty;
        dr["Costcenter"] = string.Empty;
        dr["jlhdebit"] = decimal.Zero;
        dr["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtRP;


        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();


    }


    private void cssGridRP()
    {

        // Attribute to show the Plus Minus Button.
        GRRP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRRP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");

        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");

    }

  

   
 

  
    protected void btnBackEdit_Click(object sender, EventArgs e)
    {
        pnlSPJ.Visible = true;

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();


    }


    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                int dgcount = GRRP.Rows.Count;


                ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                        drCurrentRow["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                        drCurrentRow["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;

     
                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = string.Empty;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }


                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();


                break;
        }
    }

    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[0];

            ge.ListDataset = gb.GetDataJustOutPut("dbo.HCIS_SP_JustBranch", Params, "constrhrms");

            ge.ListDataset.Tables[0].DefaultView.Sort = "[Text] asc";

            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlBranch")), "none", "");
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlBranchtujuan")), "none", "");

            ((DropDownList)e.Row.FindControl("ddlBranch")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnBranch")).Value;
            ((DropDownList)e.Row.FindControl("ddlBranchtujuan")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlBranchtujuan")).Value;

            ((DropDownList)e.Row.FindControl("ddlSaranaB")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlddlSaranaB")).Value;

            ((TextBox)e.Row.FindControl("txtwaktuB")).Attributes.Add("readonly", "true");


            if (((DropDownList)e.Row.FindControl("ddlSaranaB")).SelectedValue == "3")
            {
                ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Remove("readonly");
                ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Add("required", "required");
                ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Remove("readonly");
                ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Add("required", "required");
            }

            else
            {
                ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Add("readonly", "true");
                ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Remove("required");
                ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Add("readonly", "true");
                ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Remove("required");
            }

            if (SessBranchID != "WPI" & (((HiddenField)e.Row.FindControl("hdnBranch")).Value != "" || ((HiddenField)e.Row.FindControl("hdnddlBranchtujuan")).Value != ""))
            {
                ((DropDownList)e.Row.FindControl("ddlBranch")).SelectedValue = SessBranchID;
                ((DropDownList)e.Row.FindControl("ddlBranch")).Enabled = false;

                ((DropDownList)e.Row.FindControl("ddlBranchtujuan")).SelectedValue = SessBranchID;
                ((DropDownList)e.Row.FindControl("ddlBranchtujuan")).Enabled = false;
            }

        }
    }

    protected void lnkAddRP_Click(object sender, EventArgs e)
    {
        int dgcount = GRRP.Rows.Count;

        dtRP.Columns.Clear();

        dtRP = (DataTable)ViewState["ListDetailRP"];

        dtRP.Rows.Clear();

        for (int i = 0; i < dgcount; i++)
        {
            DataRow dr = dtRP.NewRow();

            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
            dr["BranchID"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlBranch")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlBranch")).SelectedValue : (object)DBNull.Value;
            dr["BranchIDTujuan"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlBranchtujuan")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlBranchtujuan")).SelectedValue : (object)DBNull.Value;
            dr["KotaTujuan"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
            dr["RencanaStartdate"] = Convert.ToDateTime(((TextBox)GRRP.Rows[i].FindControl("txtStartdate")).Text);
            dr["RencanaEndate"] = Convert.ToDateTime(((TextBox)GRRP.Rows[i].FindControl("txtEndDate")).Text);
            dr["SaranaKeb"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlSaranaB")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlSaranaB")).SelectedValue : (object)DBNull.Value;
            dr["KetBandaraKeb"] = ((TextBox)GRRP.Rows[i].FindControl("txtBandaraB")).Text;
            dr["JamKeb"] = ((TextBox)GRRP.Rows[i].FindControl("txtwaktuB")).Text != string.Empty ? ((TextBox)GRRP.Rows[i].FindControl("txtwaktuB")).Text : DateTime.Now.ToString("HH:mm");

            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();

        drdnew["nomor"] = dgcount + 1;
        drdnew["BranchID"] = string.Empty;
        drdnew["BranchIDTujuan"] = string.Empty;
        drdnew["KotaTujuan"] = string.Empty;
        drdnew["RencanaStartdate"] = DateTime.Now;
        drdnew["RencanaEndate"] = DateTime.Now;
        drdnew["SaranaKeb"] = "1";
        drdnew["KetBandaraKeb"] = string.Empty;
        drdnew["JamKeb"] = DateTime.Now.ToString("hh:mm");

        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();


    }

   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridEntity();
    }

    
}