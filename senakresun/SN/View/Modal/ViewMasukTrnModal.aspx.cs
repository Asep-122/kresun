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

public partial class SM_View_ViewMasukTrnModal : System.Web.UI.Page
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
    string kodesumber = "SN";
    string KodeKonter = "SN10";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
     
        txtnobukti.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter4.Visible = true;
        pnlbunga.Visible = true;
        PnlMutasi.Visible = true;
       

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

        ge.ListDataset = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");



        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        Grmutasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grmutasi.DataBind();

        Grmutasi.UseAccessibleHeader = true;

        Grmutasi.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityDatamutasitrn()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIMUTASITRN", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");



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

                blnadd = 0;
                //GRTP.Enabled = true;     
                pnlHeader.Visible = false;
                PnlFront.Visible = false;

               
                txtnobukti.Text = "";
                pnladd.Visible = true;
                Pnlcustomer.Visible = true;
                Pnlfooter4.Visible = true;
                pnlbunga.Visible = true;
                PnlMutasi.Visible = true;
              
              
                pnlmutasitrn.Visible = true;
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
 
        ge.ListDataset = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");

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

                    //txtjumlahtarikanpokok.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTarikanPokok")).Text));
                    //txtjumlahtarikanbunga.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTarikanAlokasi")).Text));
                    //txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotaltarikan")).Text));

                    



                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    //
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                  
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
                        //btndelete.Enabled = false;
                    }
                    else
                    {
                        if (statusposting == "True")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting hubungi pusat!');", true);
                            //btndelete.Enabled = false;
                        }

                    }
                    pnlbunga.Visible = true;

                    BindGridEntityDatamutasi();

                   
                    PnlMutasi.Visible = true;
                  

                    var kodelgn = hdncustomer.Value;
                    string norekening = HDNoRekening.Value;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
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
                        //txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["AtasNama"].ToString();
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



                        //btnSimpan.Enabled = false;
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
                    ge.ListDataTable = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA", Paramsc, "constrmf").Tables[0];

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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SM_TARIKAN_ALOKASI_BERJANGKA", Paramsc, "constrmf").Tables[0];

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
  
 

    protected void btndelete_click(object sender,EventArgs e)
    {
        //try
        //{

        //    //invoce yg dibayarkan
        //    decimal jlhdibayar = decimal.Parse(txtjumlahtarikanpokokbunga.Text);
        //    if (jlhdibayar <= 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Tarikan Masih Kosong');", true);

        //    }
        //    else
        //    {




        //        dtBayar.Columns.Clear();
        //        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Koperasi", typeof(string)));
        //        dtBayar.Columns.Add(new DataColumn("Cabang", typeof(string)));
        //        dtBayar.Columns.Add(new DataColumn("Instruktur", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Penarikan", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Jangka", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
        //        dtBayar.Columns.Add(new DataColumn("Pajak", typeof(int)));

        //        dtBayar.Columns.Add(new DataColumn("Bunga", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Plafon", typeof(decimal)));

        //        dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));

        //        dtBayar.Rows.Clear();
        //        int xi = 0;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {

        //            DataRow dr1 = dtBayar.NewRow();
        //            xi = xi + 1;
        //            decimal xInstruktur = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue);
        //            decimal xPenarikan = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue);
        //            decimal xJangka = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue);
        //            decimal xPajak = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue);
        //            decimal xBunga = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue);
        //            decimal xPlafon = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhPlafon")).Text);


        //            dr1["No"] = xi;
        //            dr1["Koperasi"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue;
        //            dr1["Cabang"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlCabang")).SelectedValue;
        //            dr1["Instruktur"] = xInstruktur;
        //            dr1["Penarikan"] = xPenarikan;
        //            dr1["Jangka"] = xJangka;
        //            dr1["Produk"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue;
        //            dr1["Pajak"] = xPajak;
        //            dr1["Bunga"] = xBunga;
        //            dr1["Plafon"] = xPlafon;
        //            dr1["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;


        //            dtBayar.Rows.Add(dr1);



        //        }


        //        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[24];
        //        Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
        //        Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
        //        Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
        //        Params[3] = gb.CreateSPParameter("@TarikanPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokok.Text, typeof(decimal));
        //        Params[4] = gb.CreateSPParameter("@TarikanAlokasi", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanbunga.Text, typeof(decimal));
        //        Params[5] = gb.CreateSPParameter("@totaltarikan", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokokbunga.Text, typeof(decimal));
        //        Params[6] = gb.CreateSPParameter("@saldopokok", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldopokok.Text, typeof(decimal));
        //        Params[7] = gb.CreateSPParameter("@saldobunga", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldobunga.Text, typeof(decimal));
        //        Params[8] = gb.CreateSPParameter("@saldojalan", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldojalan.Text, typeof(decimal));
        //        Params[9] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        //        Params[10] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
        //        Params[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
        //        Params[12] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        //        Params[13] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
        //        Params[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //        Params[15] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
        //        Params[16] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
        //        Params[17] = gb.CreateSPParameter("@tgldrop", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgldrop.Text, typeof(DateTime));
        //        Params[18] = gb.CreateSPParameter("@Bungakotor", ParameterDirection.Input, SqlDbType.Money, 0, txtbunga.Text, typeof(decimal));
        //        Params[19] = gb.CreateSPParameter("@Daybunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtday.Text), typeof(int));
        //        Params[20] = gb.CreateSPParameter("@nilaipajak", ParameterDirection.Input, SqlDbType.Money, 0, txtnilaipajak.Text, typeof(decimal));
        //        Params[21] = gb.CreateSPParameter("@bungabersih", ParameterDirection.Input, SqlDbType.Money, 0, txtbungabersih.Text, typeof(decimal));
        //        Params[22] = gb.CreateSPParameter("@SM_Alokasi_Berjangka", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
        //        Params[23] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
        //        ge.ListDataset = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA_DELETE", Params, "constrmf");
        //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

        //        }
        //        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

        //        }

        //        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //        {
        //            btnSimpan.Visible = false;


        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

        //        }

        //        else
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //        }
        //    }

        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
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

    decimal debet = 0, kredit = 0;


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        // try
        //{

        //    //invoce yg dibayarkan
        //    decimal jlhdibayar = decimal.Parse(txtjumlahtarikanpokokbunga.Text);
        //    if (jlhdibayar <= 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Tarikan Masih Kosong');", true);

        //    }
        //    else
        //    {

                
               

        //        dtBayar.Columns.Clear();
        //        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Koperasi", typeof(string)));
        //        dtBayar.Columns.Add(new DataColumn("Cabang", typeof(string)));
        //        dtBayar.Columns.Add(new DataColumn("Instruktur", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Penarikan", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Jangka", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
        //        dtBayar.Columns.Add(new DataColumn("Pajak", typeof(int)));

        //        dtBayar.Columns.Add(new DataColumn("Bunga", typeof(int)));
        //        dtBayar.Columns.Add(new DataColumn("Plafon", typeof(decimal)));

        //        dtBayar.Columns.Add(new DataColumn("no_rekening", typeof(string)));

        //        dtBayar.Rows.Clear();
        //        int xi = 0;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {
                  
        //                DataRow dr1 = dtBayar.NewRow();
        //                xi = xi + 1;
        //                decimal xInstruktur = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlInstruktur")).SelectedValue);
        //                decimal xPenarikan = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlpenarikanbunga")).SelectedValue);
        //                decimal xJangka = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlJangka")).SelectedValue);
        //                decimal xPajak = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlPajak")).SelectedValue);
        //                decimal xBunga = int.Parse(((DropDownList)GRTP.Rows[i].FindControl("ddlbunga")).SelectedValue);
        //                decimal xPlafon = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("jlhPlafon")).Text);

                      
        //                    dr1["No"] = xi;
        //                    dr1["Koperasi"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlkoperasi")).SelectedValue;
        //                    dr1["Cabang"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlCabang")).SelectedValue;
        //                    dr1["Instruktur"] = xInstruktur;
        //                    dr1["Penarikan"] = xPenarikan;
        //                    dr1["Jangka"] = xJangka;
        //                    dr1["Produk"] = ((DropDownList)GRTP.Rows[i].FindControl("ddlproduk")).SelectedValue;
        //                    dr1["Pajak"] = xPajak;
        //                    dr1["Bunga"] = xBunga;
        //                    dr1["Plafon"] = xPlafon;
        //                    dr1["no_rekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;


        //                dtBayar.Rows.Add(dr1);
               


        //        }


        //        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[23];
        //        Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
        //        Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
        //        Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
        //        Params[3] = gb.CreateSPParameter("@TarikanPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokok.Text, typeof(decimal));
        //        Params[4] = gb.CreateSPParameter("@TarikanAlokasi", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanbunga.Text, typeof(decimal));
        //        Params[5] = gb.CreateSPParameter("@totaltarikan", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokokbunga.Text, typeof(decimal));
        //        Params[6] = gb.CreateSPParameter("@saldopokok", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldopokok.Text, typeof(decimal));
        //        Params[7] = gb.CreateSPParameter("@saldobunga", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldobunga.Text, typeof(decimal));
        //        Params[8] = gb.CreateSPParameter("@saldojalan", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldojalan.Text, typeof(decimal));
        //        Params[9] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        //        Params[10] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
        //        Params[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
        //        Params[12] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        //        Params[13] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
        //        Params[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //        Params[15] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
        //        Params[16] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
        //        Params[17] = gb.CreateSPParameter("@tgldrop", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgldrop.Text, typeof(DateTime));
        //        Params[18] = gb.CreateSPParameter("@Bungakotor", ParameterDirection.Input, SqlDbType.Money, 0, txtbunga.Text, typeof(decimal));
        //        Params[19] = gb.CreateSPParameter("@Daybunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtday.Text), typeof(int));
        //        Params[20] = gb.CreateSPParameter("@nilaipajak", ParameterDirection.Input, SqlDbType.Money, 0, txtnilaipajak.Text, typeof(decimal));
        //        Params[21] = gb.CreateSPParameter("@bungabersih", ParameterDirection.Input, SqlDbType.Money, 0, txtbungabersih.Text, typeof(decimal));
        //        Params[22] = gb.CreateSPParameter("@SM_Alokasi_Berjangka", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
        //        ge.ListDataset = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA_Save", Params, "constrmf");
        //        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

        //        }
        //        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

        //        }

        //        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        //        {
        //            btnSimpan.Visible = false;


        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

        //        }

        //        else
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
        //            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //        }
        //    }
            
        //}

        //catch (Exception X)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
        //    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        //}
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

        //decimal saldoawal=0, saldopokok=0;
        //saldoawal = decimal.Parse(txtjumlahtarikanpokok.Text);
        //saldopokok = decimal.Parse(txtsaldopokok.Text);

        //if (saldoawal> saldopokok) {
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Pokok tidak boleh lebih dari saldo Pokok!');", true);
        //    txtjumlahtarikanpokok.Text = string.Format("{0:N2}", decimal.Parse("0"));
        //    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        //}
        //else
        //{
        //    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        //}


        //txtbayar.Text = string.Format("{0:N2}", saldoawal);
    }

   
    protected void txtjumlahtarikanbunga_TextChanged(object sender, EventArgs e)
    {

        //decimal saldoawal = 0, saldopokok = 0;
        //saldoawal = decimal.Parse(txtjumlahtarikanbunga.Text);
        //saldopokok = decimal.Parse(txtsaldobunga.Text);

        //if (saldoawal > saldopokok)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Bunga tidak boleh lebih dari saldo Bunga!');", true);
        //    txtjumlahtarikanbunga.Text = string.Format("{0:N2}", decimal.Parse("0"));
        //    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        //}
        //else
        //{
        //    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        //}


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

        ge.ListDataset = gb.GetDataJustOutPut("SM_TARIKAN_ALOKASI_BERJANGKA", var, "constrmf");
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

            //btnSimpan.Visible = true;

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

            //txtjumlahtarikanpokok.Text = string.Format("{0:N2}", decimal.Parse(txtsaldojalan.Text));
            //txtjumlahtarikanbunga.Text = string.Format("{0:N2}", decimal.Parse("0"));
            //txtjumlahtarikanpokokbunga.Text = string.Format("{0:N2}", decimal.Parse(txtsaldojalan.Text) + decimal.Parse("0"));
            //txtjumlahtarikanpokok.ReadOnly = true;
            //txtjumlahtarikanpokokbunga.ReadOnly = true;
            //txtjumlahtarikanbunga.ReadOnly = true;
            //txtjumlahtarikanpokok.ReadOnly = false;
          




            BindGridEntityDatamutasi();
            BindGridEntityDatamutasitrn();
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

         

            //btnSimpan.Enabled = false;

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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SM_getCustomer_View", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }






}
