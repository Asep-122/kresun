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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Text;

public partial class SN_Process_Sukarela_MasukTrnTarikanAlokasi : System.Web.UI.Page
{
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
    DataTable dtBayar = new DataTable("TransactionBayar");

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A09";
    string kodesumber = "SS";
    string KodeKonter = "SS10";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter4.Visible = true;
        pnlbunga.Visible = true;
        PnlMutasi.Visible = true;
        pnltrn.Visible = true;

    }
    private void SetDetailRP()
    {

        DataRow dr = null;

        dtBayar.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("Koperasi", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Cabang", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Instruktur", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("Penarikan", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("Jangka", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Pajak", typeof(int)));

        dtBayar.Columns.Add(new DataColumn("Bunga", typeof(float)));
        dtBayar.Columns.Add(new DataColumn("Plafon", typeof(decimal)));

        dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));


        dr = dtBayar.NewRow();
        dr["nomor"] = 1;
        dr["Koperasi"] = string.Empty;
        dr["Cabang"] = string.Empty;
        dr["Instruktur"] = 1;
        dr["Penarikan"] =1;
        dr["Produk"] = string.Empty;        
        dr["Jangka"] = 0;
        dr["Pajak"] = 0;
        dr["Bunga"] = 0;
        dr["Plafon"] = decimal.Zero;
        dr["no_rekening"] = string.Empty;

        dtBayar.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtBayar;


        GRTP.DataSource = dtBayar;
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();






    }
    protected void lnkBT_Click(object sender, EventArgs e)
    {
        int dgcount = GRTP.Rows.Count;
        if (dgcount > 0)
        {
            dtBayar.Columns.Clear();

            dtBayar = (DataTable)ViewState["ListDetailRP"];

            dtBayar.Rows.Clear();

            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtBayar.NewRow();                
                dr["nomor"] = ((Label)GRTP.Rows[i].FindControl("lblNumber")).Text;
                dr["Koperasi"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue : (object)DBNull.Value;
                dr["Cabang"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlcabang")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlcabang")).SelectedValue : (object)DBNull.Value;
                dr["Instruktur"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue : (object)DBNull.Value;
                dr["Penarikan"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue : (object)DBNull.Value;
                dr["produk"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue : (object)DBNull.Value;
                dr["Jangka"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue : (object)DBNull.Value;
                dr["Pajak"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue : (object)DBNull.Value;
                dr["Bunga"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue : (object)DBNull.Value;
                dr["Plafon"] = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhPlafon")).Text); ;
                dr["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;


                dtBayar.Rows.Add(dr);
            }

            DataRow drdnew = dtBayar.NewRow();
            drdnew["nomor"] = dgcount + 1; 
            drdnew["Koperasi"] = string.Empty;
            drdnew["Cabang"] = string.Empty;
            drdnew["Instruktur"] =1;
            drdnew["Penarikan"] = 1;
            drdnew["Produk"] = string.Empty;
            drdnew["Jangka"] = 0;
            drdnew["Pajak"] = 0;
            drdnew["Bunga"] = 0;
            drdnew["Plafon"] = decimal.Zero;
            drdnew["no_rekening"] = string.Empty;
            dtBayar.Rows.Add(drdnew);
            ViewState["ListDetailRP"] = dtBayar;

            GRTP.DataSource = dtBayar;
            GRTP.DataBind();
           
            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
            cssGridRP();


        }


        else
        {
            SetDetailRP();
            
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    private void lnkAddRP2_Click()
    {

        int dgcount = GRTP.Rows.Count;
        if (dgcount > 0)
        {
            dtBayar.Columns.Clear();

            dtBayar = (DataTable)ViewState["ListDetailRP"];

            dtBayar.Rows.Clear();

            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtBayar.NewRow();


                dr = dtBayar.NewRow();
                dr["nomor"] = ((Label)GRTP.Rows[i].FindControl("lblNumber")).Text;
                dr["Koperasi"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue : (object)DBNull.Value;
                dr["Cabang"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlcabang")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlcabang")).SelectedValue : (object)DBNull.Value; 
                dr["Instruktur"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue : (object)DBNull.Value; 
                dr["Penarikan"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue : (object)DBNull.Value; 
                dr["Jangka"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue : (object)DBNull.Value;
                dr["Pajak"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue : (object)DBNull.Value;
                dr["Bunga"] = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblBunga")).Text); ;
                dr["Plafon"] = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblPlafon")).Text); ;
                dr["no_rekening"] = ((DropDownList)GRTP.Rows[i].FindControl("lblnofaktur")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("lblnofaktur")).SelectedValue : (object)DBNull.Value;
                dr["produk"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue : (object)DBNull.Value;

                dtBayar.Rows.Add(dr);
            }

            //DataRow drdnew = dtRP.NewRow();
            //drdnew["nomor"] = dgcount + 1;
            //drdnew["KodeGL"] = string.Empty;
            //drdnew["NamaGL"] = string.Empty;
            //drdnew["Description"] = string.Empty;
            //drdnew["Costcenter"] = string.Empty;
            //drdnew["jlhdebit"] = decimal.Zero;
            //drdnew["jlhkredit"] = decimal.Zero;

            //dtRP.Rows.Add(drdnew);

            ViewState["ListDetailRP"] = dtBayar;

            GRTP.DataSource = dtBayar;
            GRTP.DataBind();

            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridRP();

        }


        else
        {
            SetDetailRP();
            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }


    }


    private void cssGridRP()
    {

        // Attribute to show the Plus Minus Button.
        GRTP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRTP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand");
        cells[8].Attributes.Add("data-hide", "phone");


    }

 

    protected void btnAdddetail_Click(object sender, EventArgs e)
    {

    }
    //protected void lnkBT_Click(object sender, EventArgs e)
    //{
    //    int dgcount = GRRP.Rows.Count;

    //    dtRP.Columns.Clear();

    //    dtRP = (DataTable)ViewState["ListDetailRP"];

    //    dtRP.Rows.Clear();

    //    for (int i = 0; i < dgcount; i++)
    //    {
    //        DataRow dr = dtRP.NewRow();

    //        dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
    //        dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
    //        dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
    //        dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
    //        dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
    //        dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
    //        dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
    //        dtRP.Rows.Add(dr);
    //    }

    //    DataRow drdnew = dtRP.NewRow();
    //    drdnew["nomor"] = dgcount + 1;
    //    drdnew["KodeGL"] = string.Empty;
    //    drdnew["NamaGL"] = string.Empty;
    //    drdnew["Description"] = txtnote.Text;
    //    drdnew["Costcenter"] = string.Empty;
    //    drdnew["jlhdebit"] = decimal.Zero;
    //    drdnew["jlhkredit"] = decimal.Zero;

    //    dtRP.Rows.Add(drdnew);

    //    ViewState["ListDetailRP"] = dtRP;

    //    GRRP.DataSource = dtRP;
    //    GRRP.DataBind();

    //    GRRP.UseAccessibleHeader = true;
    //    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

    //    cssGridRP();
    //}


    protected void ddlnamaGL_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        ges.ListDataset = gbs.GetDataJustOutPut("dbo.GL_xSGetCoacode", Params, "constrmf");



        ges.ListDataset.Tables[0].DefaultView.Sort = "value asc";


        gbs.FillDDL(ges.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlKodeGL")), "-", "-");

        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        xParams[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue, typeof(String));
        xParams[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        xParams[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParams, "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count != 0)
        {
            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)grow.FindControl("ddlcostcenter")), "", "");
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataTextField = "Text";
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataValueField = "Value";
            ((DropDownList)grow.FindControl("ddlcostcenter")).DataBind();
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = true;
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlcostcenter")).Items.Insert(0, new ListItem("-", "-"));
            ((DropDownList)grow.FindControl("ddlcostcenter")).Enabled = false;
        }



        ((DropDownList)grow.FindControl("ddlKodeGL")).SelectedValue = ((DropDownList)grow.FindControl("ddlnamaGL")).SelectedValue;


        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        // UpdatePanel1.Update();
    }

    //protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
    //    decimal jlhdebit;
    //    jlhdebit = decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text);
    //    if (jlhdebit > 0)
    //    {

    //        ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
    //        ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
    //    }
    //    else
    //    {

    //        ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
    //        ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
    //    }
    //    GRRP.UseAccessibleHeader = true;
    //    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    //}

    //protected void XXXTxt02_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
    //    decimal jlhkredit;
    //    jlhkredit = decimal.Parse(((TextBox)grow.FindControl("jlhkredit")).Text);
    //    if (jlhkredit > 0)
    //    {

    //        ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
    //        ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
    //    }
    //    else
    //    {

    //        ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
    //        ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
    //    }
    //    GRRP.UseAccessibleHeader = true;
    //    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntityDatamutasi()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIMUTASI", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");



        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        Grmutasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grmutasi.DataBind();

        Grmutasi.UseAccessibleHeader = true;

        Grmutasi.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityDataAlokasi()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIALOKASI", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");



        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;

        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");


            }


         
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

   
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


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

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;

                    Pnlfooter4.Visible = true;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    txtsaldopokok.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldopokok")).Text));
                    txtsaldobunga.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldobunga")).Text));
                    txtsaldojalan.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldojalan")).Text));

                    txtjumlahtarikanpokok.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTarikanPokok")).Text));
                    txtjumlahtarikanbunga.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTarikanAlokasi")).Text));
                    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotaltarikan")).Text));

                    



                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    //
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btnSimpan.Visible = false;
        
                    btndelete.Visible = true;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;

                    txttgldrop.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgldrop")).Text;
                    txttgldrop.Enabled = false;

                    txtday.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDaybunga")).Text;
                    //txtday.Enabled = false;


                    txtbunga.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungakotor")).Text));
                    //txtbunga.Enabled = false;

                    txtnilaipajak.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnilaipajak")).Text));
                    //txtnilaipajak.Enabled = false;

                    txtbungabersih.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbungabersih")).Text));
                    //txtbungabersih.Enabled = false;
                    txttambahbunga.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbungabersih")).Text));


                    if (statusclosing == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                        btndelete.Enabled = false;
                    }
                    else
                    {
                        if (statusposting == "True")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting hubungi pusat!');", true);
                            btndelete.Enabled = false;
                        }

                    }
                    pnlbunga.Visible = true;

                    BindGridEntityDatamutasi();

                    BindGridEntityDataAlokasi();
                    Button1.Enabled = false;
                    GRTP.Enabled = false;
                    PnlMutasi.Visible = true;
                    pnltrn.Visible = true;


                    var kodelgn = hdncustomer.Value;
                    string norekening = HDNoRekening.Value;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)


                    {
                        txtkodelgn.Text = kodelgn;
                        txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();


                        //HDNoNasabah.Value = no_nasabah;
                        txttglpermohonan.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglpermohonan"].ToString()).ToString("dd-MM-yyyy");

                        txtnopermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["Idpermohonan"].ToString();





                        txtproduk.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();

                        txtpajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString()));
                        txtbungapertahun.Text = string.Format("{0:N2}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString()));

                        txtbungaperhari.Text = string.Format("{0:N6}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                        ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
                        ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();

                        //txtbankrekening.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                        //txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBank"].ToString();
                        //txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["Atasnama"].ToString();
                        txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

                        //btnSimpan.Visible = true;




                    }

                    else
                    {
                        txtkodelgn.Text = "";
                        txtNmSup.Text = "";


                        //HDNoNasabah.Value = no_nasabah;
                        txttglpermohonan.Text = "";

                        txtnopermohonan.Text = "";





                        txtproduk.Text = "";

                        txtpajak.Text = "";
                        txtbungapertahun.Text = "";

                        txtbungaperhari.Text = "";

                        //txtnilaisaldo.Text = "";

                        //txtnilaibungapertahun.Text = "";
                        //txtnilaibungaperhari.Text = "";


                        ddlInstruktur.SelectedValue = "";
                        ddlpenarikanbunga.SelectedValue = "";

                        //txtbankrekening.Text = "";
                        //txtnorekening.Text = "";
                        //txtatasnama.Text = "";
                        txtjangka.Text = "";



                        btnSimpan.Enabled = false;
                    }

                    //ddlsetor.Enabled = false;



                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

              
                break;
            case "PDF":

                try
                {

                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
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
            case "SLIP":

                try
                {
                     
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    typetrn = "B";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    
                    Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SS_TARIKAN_ALOKASI_BERJANGKA", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAP.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                      
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                      

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
        }
    }
    
          protected void ddlCabang_SelectedIndexChanged(object sender, EventArgs e)
    {

        


    }
    protected void XXXTxt02_TextChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        var data = ((DropDownList)grow.FindControl("ddlkoperasi")).SelectedValue;
        var cabang = ((DropDownList)grow.FindControl("ddlCabang")).SelectedValue;
        var produk = ((DropDownList)grow.FindControl("ddlproduk")).SelectedValue;
        var jangka = ((DropDownList)grow.FindControl("ddlJangka")).SelectedValue;
        var pajak = ((DropDownList)grow.FindControl("ddlpajak")).SelectedValue;
        var bunga = ((DropDownList)grow.FindControl("ddlbunga")).SelectedValue;
        if (data=="-")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Koperasi Masih Kosong');", true);

            ((TextBox)grow.FindControl("jlhPlafon")).Text = "0";
         
        }
        else if (cabang == "-")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong');", true);
            ((TextBox)grow.FindControl("jlhPlafon")).Text = "0";

        }
        else if (produk == "-")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Produk Masih Kosong');", true);
            ((TextBox)grow.FindControl("jlhPlafon")).Text = "0";

        }
        else if (jangka == "0")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('jangka Masih Kosong');", true);
            ((TextBox)grow.FindControl("jlhPlafon")).Text = "0";

        }
        else if (pajak == "-")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pajak Masih Kosong');", true);
            ((TextBox)grow.FindControl("jlhPlafon")).Text = "0";

        }
        else if (bunga == "0")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('bunga Masih Kosong');", true);
            ((TextBox)grow.FindControl("jlhPlafon")).Text = "0";

        }

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridbayar_Click();

    }
    private void subgridbayar_Click()
    {

        int dgcount = GRTP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {


            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhPlafon")).Text);
                sumjlhgl = sumjlhgl + jlhgl;
            }



        }


        else
        {
            sumjlhgl = 0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
        txtjumlahtarikanbunga.Text = formatted;

        decimal saldojalan = decimal.Parse(txtsaldojalan.Text);

        txtjumlahtarikanpokok.Text= string.Format("{0:N2}", saldojalan - sumjlhgl);


    }

    protected void ddlJangka_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;


        


        var produk = ((DropDownList)grow.FindControl("ddlproduk")).SelectedValue;
        var jangka = ((DropDownList)grow.FindControl("ddljangka")).SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@xProduk", ParameterDirection.Input, SqlDbType.VarChar, 15, produk, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 0, jangka, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            ((DropDownList)grow.FindControl("ddlbunga")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ((DropDownList)grow.FindControl("ddlbunga")).DataTextField = "BungaTahunan";
            ((DropDownList)grow.FindControl("ddlbunga")).DataValueField = "BungaTahunan";
            ((DropDownList)grow.FindControl("ddlbunga")).DataBind();
            ((DropDownList)grow.FindControl("ddlbunga")).Items.Insert(0, new ListItem("-", "0"));
            ((DropDownList)grow.FindControl("ddlbunga")).Enabled = true;
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlbunga")).Items.Insert(0, new ListItem("-", "0"));
            ((DropDownList)grow.FindControl("ddlbunga")).Enabled = false;
        }

        //((DropDownList)grow.FindControl("ddlkoperasi")).SelectedValue = ((HiddenField)grow.FindControl("hdnddlkoperasi")).Value;









        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;


    }
    protected void ddlproduk_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        var produk = ((DropDownList)grow.FindControl("ddlproduk")).SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@xproduk", ParameterDirection.Input, SqlDbType.VarChar, 15, produk, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            ((DropDownList)grow.FindControl("ddlJangka")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ((DropDownList)grow.FindControl("ddlJangka")).DataTextField = "jangka";
            ((DropDownList)grow.FindControl("ddlJangka")).DataValueField = "jangka";
            ((DropDownList)grow.FindControl("ddlJangka")).DataBind();
            ((DropDownList)grow.FindControl("ddlJangka")).Items.Insert(0, new ListItem("-", "0"));
            ((DropDownList)grow.FindControl("ddlJangka")).Enabled = true;
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlJangka")).Items.Insert(0, new ListItem("-", "0"));
            ((DropDownList)grow.FindControl("ddlJangka")).Enabled = false;
        }

        //((DropDownList)grow.FindControl("ddlkoperasi")).SelectedValue = ((HiddenField)grow.FindControl("hdnddlkoperasi")).Value;

        







        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;


    }
    protected void ddlkoperasix_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        var Koperasi = ((DropDownList)grow.FindControl("ddlkoperasi")).SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@xkoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";        
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            ((DropDownList)grow.FindControl("ddlCabang")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ((DropDownList)grow.FindControl("ddlCabang")).DataTextField = "nama_cabang";
            ((DropDownList)grow.FindControl("ddlCabang")).DataValueField = "kode_cabang";
            ((DropDownList)grow.FindControl("ddlCabang")).DataBind();
            ((DropDownList)grow.FindControl("ddlCabang")).Items.Insert(0, new ListItem("-", "-"));
            ((DropDownList)grow.FindControl("ddlCabang")).Enabled = true;
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlCabang")).Items.Insert(0, new ListItem("-", "-"));
            ((DropDownList)grow.FindControl("ddlCabang")).Enabled = false;
        }

        //((DropDownList)grow.FindControl("ddlkoperasi")).SelectedValue = ((HiddenField)grow.FindControl("hdnddlkoperasi")).Value;
      
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
      
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPRODUK", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            ((DropDownList)grow.FindControl("ddlproduk")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ((DropDownList)grow.FindControl("ddlproduk")).DataTextField = "nama_produk";
            ((DropDownList)grow.FindControl("ddlproduk")).DataValueField = "kode_produk";
            ((DropDownList)grow.FindControl("ddlproduk")).DataBind();
            ((DropDownList)grow.FindControl("ddlproduk")).Items.Insert(0, new ListItem("-", "-"));
            ((DropDownList)grow.FindControl("ddlproduk")).Enabled = true;
        }
        else
        {
            ((DropDownList)grow.FindControl("ddlproduk")).Items.Insert(0, new ListItem("-", "-"));
            ((DropDownList)grow.FindControl("ddlproduk")).Enabled = false;
        }




      



        //GRTP.UseAccessibleHeader = true;
        //GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;


    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtjumlahtarikanpokokbunga.Text);
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Tarikan Masih Kosong');", true);

            }
            else
            {




                dtBayar.Columns.Clear();
                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Koperasi", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Cabang", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Instruktur", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Penarikan", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Jangka", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Pajak", typeof(int)));

                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Plafon", typeof(decimal)));

                dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));

                dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {

                    DataRow dr1 = dtBayar.NewRow();
                    xi = xi + 1;
                    decimal xInstruktur = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue);
                    decimal xPenarikan = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue);
                    decimal xJangka = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue);
                    decimal xPajak = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue);
                    decimal xBunga = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue);
                    decimal xPlafon = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhPlafon")).Text);


                    dr1["No"] = xi;
                    dr1["Koperasi"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue;
                    dr1["Cabang"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlCabang")).SelectedValue;
                    dr1["Instruktur"] = xInstruktur;
                    dr1["Penarikan"] = xPenarikan;
                    dr1["Jangka"] = xJangka;
                    dr1["Produk"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue;
                    dr1["Pajak"] = xPajak;
                    dr1["Bunga"] = xBunga;
                    dr1["Plafon"] = xPlafon;
                    dr1["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;


                    dtBayar.Rows.Add(dr1);



                }


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[24];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                Params[3] = gb.CreateSPParameter("@TarikanPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokok.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@TarikanAlokasi", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanbunga.Text, typeof(decimal));
                Params[5] = gb.CreateSPParameter("@totaltarikan", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokokbunga.Text, typeof(decimal));
                Params[6] = gb.CreateSPParameter("@saldopokok", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldopokok.Text, typeof(decimal));
                Params[7] = gb.CreateSPParameter("@saldobunga", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldobunga.Text, typeof(decimal));
                Params[8] = gb.CreateSPParameter("@saldojalan", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldojalan.Text, typeof(decimal));
                Params[9] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                Params[10] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[12] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[13] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[15] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[16] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
                Params[17] = gb.CreateSPParameter("@tgldrop", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgldrop.Text, typeof(DateTime));
                Params[18] = gb.CreateSPParameter("@Bungakotor", ParameterDirection.Input, SqlDbType.Money, 0, txtbunga.Text, typeof(decimal));
                Params[19] = gb.CreateSPParameter("@Daybunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtday.Text), typeof(int));
                Params[20] = gb.CreateSPParameter("@nilaipajak", ParameterDirection.Input, SqlDbType.Money, 0, txtnilaipajak.Text, typeof(decimal));
                Params[21] = gb.CreateSPParameter("@bungabersih", ParameterDirection.Input, SqlDbType.Money, 0, txtbungabersih.Text, typeof(decimal));
                Params[22] = gb.CreateSPParameter("@SS_Alokasi_Berjangka", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[23] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA_DELETE", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

                }

                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                int dgcount = GRTP.Rows.Count;


                ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtBayar.Columns.Clear();

                dtBayar = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtBayar.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {

                    if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtBayar.NewRow();



                        drCurrentRow["nomor"] = j;
                        drCurrentRow["Koperasi"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Cabang"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlcabang")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlcabang")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Instruktur"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Penarikan"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["produk"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Jangka"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Pajak"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Bunga"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue != string.Empty ? ((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Plafon"] = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhPlafon")).Text); ;
                        drCurrentRow["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;




                        dtBayar.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                           


                    DataRow drdnew = dtBayar.NewRow();
                    drdnew["nomor"] = 1;
                    drdnew["Koperasi"] = string.Empty;
                    drdnew["Cabang"] = string.Empty;
                    drdnew["Instruktur"] = string.Empty;
                    drdnew["Penarikan"] = string.Empty;
                    drdnew["Produk"] = string.Empty;
                    drdnew["Jangka"] = string.Empty;
                    drdnew["Pajak"] = string.Empty;
                    drdnew["Bunga"] = decimal.Zero;
                    drdnew["Plafon"] = decimal.Zero;
                    drdnew["no_rekening"] = string.Empty;


                    dtBayar.Rows.Add(drdnew);

                }


                ViewState["ListDetailRP"] = dtBayar;

                GRTP.DataSource = dtBayar;
                GRTP.DataBind();

                GRTP.UseAccessibleHeader = true;
                GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();


                break;
        }
    }
    //protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    //switch (e.CommandName)
    //    //{
    //    //    case "DEL":
    //    //        int dgcount = GRRP.Rows.Count;


    //    //        ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

    //    //        dtRP.Columns.Clear();

    //    //        dtRP = (DataTable)ViewState["ListDetailRTP"];

    //    //        DataRow drCurrentRow = null;

    //    //        dtRP.Rows.Clear();

    //    //        int j = 1;

    //    //        for (int i = 0; i < GRTP.Rows.Count; i++)
    //    //        {

    //    //            if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
    //    //            {


    //    //                drCurrentRow = dtRP.NewRow();

    //    //                drCurrentRow["nomor"] = j;
    //    //                drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
    //    //                drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
    //    //                drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
    //    //                drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
    //    //                drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
    //    //                drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
    //    //                drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

    //    //                dtRP.Rows.Add(drCurrentRow);

    //    //                j++;
    //    //            }
    //    //        }

    //    //        if (dgcount == 1)
    //    //        {
    //    //            DataRow drdnew = dtRP.NewRow();

    //    //            drdnew["nomor"] = 1;
    //    //            drdnew["TglTrnFaktur"] = string.Empty;
    //    //            drdnew["NoFaktur"] = string.Empty;
    //    //            drdnew["TypeTrn"] = string.Empty;
    //    //            drdnew["TglJthTmp"] = string.Empty;
    //    //            drdnew["Keterangan"] = string.Empty;
    //    //            drdnew["JumlahTrn"] = decimal.Zero;
    //    //            drdnew["KursStd"] = decimal.Zero;
    //    //            dtRP.Rows.Add(drdnew);
    //    //        }


    //    //        ViewState["ListDetailRTP"] = dtRP;

    //    //        GRTP.DataSource = dtRP;
    //    //        GRTP.DataBind();

    //    //        GRTP.UseAccessibleHeader = true;
    //    //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

    //    //        cssGridBayar();



    //    //        break;


    //    //}
    //}

    decimal debet = 0, kredit = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("dbo.SS_GetKOPERASI", Params, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlkoperasi")), "-", "-");

            ((DropDownList)e.Row.FindControl("ddlkoperasi")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlkoperasi")).Value;


             
            DefaultClass.Global.SqlHelper[] varxx = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("SS_GetPAJAK", varxx, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "Pajak asc";
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)e.Row.FindControl("ddlpajak")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ((DropDownList)e.Row.FindControl("ddlpajak")).DataTextField = "Pajak";
                ((DropDownList)e.Row.FindControl("ddlpajak")).DataValueField = "Pajak";
                ((DropDownList)e.Row.FindControl("ddlpajak")).DataBind();

                ((DropDownList)e.Row.FindControl("ddlpajak")).Enabled = true;
            }
            else
            {
                ((DropDownList)e.Row.FindControl("ddlpajak")).Items.Insert(0, new ListItem("-","-"));
                ((DropDownList)e.Row.FindControl("ddlpajak")).Enabled = false;
            }

            ((DropDownList)e.Row.FindControl("ddlpajak")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlpajak")).Value;

            var Koperasi = ((DropDownList)e.Row.FindControl("ddlkoperasi")).SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@xkoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";

            


            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)e.Row.FindControl("ddlCabang")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ((DropDownList)e.Row.FindControl("ddlCabang")).DataTextField = "nama_cabang";
                ((DropDownList)e.Row.FindControl("ddlCabang")).DataValueField = "kode_cabang";
                ((DropDownList)e.Row.FindControl("ddlCabang")).DataBind();

                ((DropDownList)e.Row.FindControl("ddlCabang")).Enabled = true;
            }
            else
            {
                ((DropDownList)e.Row.FindControl("ddlCabang")).Items.Insert(0, new ListItem("-", "-"));
               ((DropDownList)e.Row.FindControl("ddlCabang")).Enabled = false;
            }

            ((DropDownList)e.Row.FindControl("ddlCabang")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlCabang")).Value;



            //if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
            //{
            //    ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

            //}

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];

            var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPRODUK", typeof(string));
            var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)e.Row.FindControl("ddlproduk")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ((DropDownList)e.Row.FindControl("ddlproduk")).DataTextField = "nama_produk";
                ((DropDownList)e.Row.FindControl("ddlproduk")).DataValueField = "kode_produk";
                ((DropDownList)e.Row.FindControl("ddlproduk")).DataBind();
                ((DropDownList)e.Row.FindControl("ddlproduk")).Items.Insert(0, new ListItem("-", "-"));
                ((DropDownList)e.Row.FindControl("ddlproduk")).Enabled = true;
            }
            else
            {
                ((DropDownList)e.Row.FindControl("ddlproduk")).Items.Insert(0, new ListItem("-", "-"));
                ((DropDownList)e.Row.FindControl("ddlproduk")).Enabled = false;
            }
             ((DropDownList)e.Row.FindControl("ddlproduk")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlproduk")).Value;




            var produk = ((DropDownList)e.Row.FindControl("ddlproduk")).SelectedValue;
            DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
            var2[0] = gb.CreateSPParameter("@xproduk", ParameterDirection.Input, SqlDbType.VarChar, 15, produk, typeof(string));
            var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
            var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var2, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "jangka asc";
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)e.Row.FindControl("ddlJangka")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ((DropDownList)e.Row.FindControl("ddlJangka")).DataTextField = "jangka";
                ((DropDownList)e.Row.FindControl("ddlJangka")).DataValueField = "jangka";
                ((DropDownList)e.Row.FindControl("ddlJangka")).DataBind();
                ((DropDownList)e.Row.FindControl("ddlJangka")).Items.Insert(0, new ListItem("-", "0"));
                ((DropDownList)e.Row.FindControl("ddlJangka")).Enabled = true;
            }
            else
            {
                ((DropDownList)e.Row.FindControl("ddlJangka")).Items.Insert(0, new ListItem("-", "0"));
                ((DropDownList)e.Row.FindControl("ddlJangka")).Enabled = false;
            }

            ((DropDownList)e.Row.FindControl("ddlJangka")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlJangka")).Value;

           


            var produkx = ((DropDownList)e.Row.FindControl("ddlproduk")).SelectedValue;
            var jangkax = ((DropDownList)e.Row.FindControl("ddljangka")).SelectedValue;
            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
            var3[0] = gb.CreateSPParameter("@xProduk", ParameterDirection.Input, SqlDbType.VarChar, 15, produkx, typeof(string));
            var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
            var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var3[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            var3[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 0, jangkax, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var3, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)e.Row.FindControl("ddlbunga")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ((DropDownList)e.Row.FindControl("ddlbunga")).DataTextField = "BungaTahunan";
                ((DropDownList)e.Row.FindControl("ddlbunga")).DataValueField = "BungaTahunan";
                ((DropDownList)e.Row.FindControl("ddlbunga")).DataBind();
                ((DropDownList)e.Row.FindControl("ddlbunga")).Items.Insert(0, new ListItem("-", "0"));
                ((DropDownList)e.Row.FindControl("ddlbunga")).Enabled = true;
            }
            else
            {
                ((DropDownList)e.Row.FindControl("ddlbunga")).Items.Insert(0, new ListItem("-", "0"));
                ((DropDownList)e.Row.FindControl("ddlbunga")).Enabled = false;
            }
             ((DropDownList)e.Row.FindControl("ddlbunga")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlbunga")).Value;
            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtjumlahtarikanpokokbunga.Text);
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Tarikan Masih Kosong');", true);

            }
            else
            {

                
               

                dtBayar.Columns.Clear();
                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Koperasi", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Cabang", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Instruktur", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Penarikan", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Jangka", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Pajak", typeof(int)));

                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(float)));
                dtBayar.Columns.Add(new DataColumn("Plafon", typeof(decimal)));

                dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));

                dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                  
                        DataRow dr1 = dtBayar.NewRow();
                        xi = xi + 1;
                        decimal xInstruktur = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue);
                        decimal xPenarikan = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue);
                        decimal xJangka = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue);
                        decimal xPajak = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue);
                        float xBunga = float.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue);
                        decimal xPlafon = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhPlafon")).Text);

                      
                            dr1["No"] = xi;
                            dr1["Koperasi"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue;
                            dr1["Cabang"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlCabang")).SelectedValue;
                            dr1["Instruktur"] = xInstruktur;
                            dr1["Penarikan"] = xPenarikan;
                            dr1["Jangka"] = xJangka;
                            dr1["Produk"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue;
                            dr1["Pajak"] = xPajak;
                            dr1["Bunga"] = xBunga;
                            dr1["Plafon"] = xPlafon;
                            dr1["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;


                        dtBayar.Rows.Add(dr1);
               


                }


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[23];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                Params[3] = gb.CreateSPParameter("@TarikanPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokok.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@TarikanAlokasi", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanbunga.Text, typeof(decimal));
                Params[5] = gb.CreateSPParameter("@totaltarikan", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokokbunga.Text, typeof(decimal));
                Params[6] = gb.CreateSPParameter("@saldopokok", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldopokok.Text, typeof(decimal));
                Params[7] = gb.CreateSPParameter("@saldobunga", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldobunga.Text, typeof(decimal));
                Params[8] = gb.CreateSPParameter("@saldojalan", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldojalan.Text, typeof(decimal));
                Params[9] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                Params[10] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[12] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[13] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[15] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[16] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
                Params[17] = gb.CreateSPParameter("@tgldrop", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgldrop.Text, typeof(DateTime));
                Params[18] = gb.CreateSPParameter("@Bungakotor", ParameterDirection.Input, SqlDbType.Money, 0, txtbunga.Text, typeof(decimal));
                Params[19] = gb.CreateSPParameter("@Daybunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtday.Text), typeof(int));
                Params[20] = gb.CreateSPParameter("@nilaipajak", ParameterDirection.Input, SqlDbType.Money, 0, txtnilaipajak.Text, typeof(decimal));
                Params[21] = gb.CreateSPParameter("@bungabersih", ParameterDirection.Input, SqlDbType.Money, 0, txtbungabersih.Text, typeof(decimal));
                Params[22] = gb.CreateSPParameter("@SS_Alokasi_Berjangka", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA_Save", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

                }

                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
            }
            
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

  
  
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
       
        }

    }

    protected void txtjumlahtarikanpokok_TextChanged(object sender, EventArgs e)
    {

        decimal saldoawal=0, saldopokok=0;
        saldoawal = decimal.Parse(txtjumlahtarikanpokok.Text);
        saldopokok = decimal.Parse(txtsaldopokok.Text);

        if (saldoawal> saldopokok) {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Pokok tidak boleh lebih dari saldo Pokok!');", true);
            txtjumlahtarikanpokok.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        }
        else
        {
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        }


        //txtbayar.Text = string.Format("{0:N2}", saldoawal);
    }

    protected void ddlkoperasi_SelectedIndexChanged(object sender, EventArgs e)
    {


        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        if (((DropDownList)grow.FindControl("ddlkoperasi")).SelectedValue != "-")
        {
           

        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "1")
        {
            ((Label)grow.FindControl("lblbayar")).Text = ((Label)grow.FindControl("lblAngsuran")).Text;
        }

        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "2")
        {
            decimal angsuran = 0;
            decimal denda = 0;
            decimal total = 0;

            angsuran = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            denda = decimal.Parse(((Label)grow.FindControl("lbldenda")).Text);
            total = angsuran + denda;
            ((Label)grow.FindControl("lblbayar")).Text = string.Format("{0:N2}", total);

        }


        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
       

    }
    protected void txtjumlahtarikanbunga_TextChanged(object sender, EventArgs e)
    {

        decimal saldoawal = 0, saldopokok = 0;
        saldoawal = decimal.Parse(txtjumlahtarikanbunga.Text);
        saldopokok = decimal.Parse(txtsaldobunga.Text);

        if (saldoawal > saldopokok)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Bunga tidak boleh lebih dari saldo Bunga!');", true);
            txtjumlahtarikanbunga.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        }
        else
        {
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        }


        //txtbayar.Text = string.Format("{0:N2}", saldoawal);
    }

  
    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;
        string norekening = HDNoRekening.Value;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SS_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
            txtkodelgn.Text = kodelgn;
            txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();


            //HDNoNasabah.Value = no_nasabah;
            txttglpermohonan.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglpermohonan"].ToString()).ToString("dd-MM-yyyy");

            txtnopermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["Idpermohonan"].ToString();





            txtproduk.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();

            txtpajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString()));
            txtbungapertahun.Text = string.Format("{0:N2}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString()));

            txtbungaperhari.Text = string.Format("{0:N6}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

            //txtnilaisaldo.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaisaldo"].ToString()));

            //txtnilaibungapertahun.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungapertahun"].ToString()));
            //txtnilaibungaperhari.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungaperhari"].ToString()));


            ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
            ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();


            txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

            btnSimpan.Visible = true;

            txttgldrop.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString()).ToString("dd-MM-yyyy");

            txtday.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["daybunga"].ToString()));

            txtbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungakotor"].ToString()));

            txtnilaipajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaipajak"].ToString()));
            txtbungabersih.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungabersih"].ToString()));


            txttambahbunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungabersih"].ToString()));

            txtsaldoawalpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoawal"].ToString()));
            txtsetoranpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["setoran"].ToString()));
            txttarikanpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tarikan"].ToString()));
            txtsaldopokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldopokok"].ToString()));
            txtbungatransfer.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungatransfer"].ToString()));
            txtbungarollon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungarollon"].ToString()));
            txtbungakeluar.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungakeluar"].ToString()));
            txtsaldobunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobunga"].ToString()));
            txtsaldojalan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldojalan"].ToString()) + decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungabersih"].ToString()));

            txtjumlahtarikanpokok.Text = string.Format("{0:N2}", decimal.Parse(txtsaldojalan.Text));
            txtjumlahtarikanbunga.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtsaldojalan.Text) + decimal.Parse("0"));
            txtjumlahtarikanpokok.ReadOnly = true;
            txtjumlahtarikanpokokbunga.ReadOnly = true;
            txtjumlahtarikanbunga.ReadOnly = true;
            txtjumlahtarikanpokok.ReadOnly = false;
          




            BindGridEntityDatamutasi();

        }

        else
        {
            txtkodelgn.Text ="";
            txtNmSup.Text = "";


            //HDNoNasabah.Value = no_nasabah;
            txttglpermohonan.Text = "";

            txtnopermohonan.Text = "";





            txtproduk.Text = "";

            txtpajak.Text = "";
            txtbungapertahun.Text = "";

            txtbungaperhari.Text = "";

            //txtnilaisaldo.Text = "";

            //txtnilaibungapertahun.Text = "";
            //txtnilaibungaperhari.Text = "";


            ddlInstruktur.SelectedValue = "";
            ddlpenarikanbunga.SelectedValue = "";

            //txtbankrekening.Text = "";
            //txtnorekening.Text = "";
            //txtatasnama.Text = "";
            txtjangka.Text = "";

            txtjumlahtarikanpokok.Text = string.Format("{0:N2}", decimal.Parse("0")); ;
            txtjumlahtarikanbunga.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtjumlahtarikanpokok.Enabled = false;
            txtjumlahtarikanpokokbunga.Enabled = false;
            txtjumlahtarikanbunga.Enabled = false;

            btnSimpan.Enabled = false;

            txttgldrop.Text = "";

            txtday.Text = string.Format("{0:N0}", decimal.Parse("0"));

            txtbunga.Text = string.Format("{0:N2}", decimal.Parse("0"));

            txtnilaipajak.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtbungabersih.Text = string.Format("{0:N2}", decimal.Parse("0"));
        }


     
        

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



    }
  

  

      
    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SS_getCustomer_tarikan_berjangka", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }






}
