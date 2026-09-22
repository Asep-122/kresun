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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
public partial class AS_Process_MasukTrnInvoicePengajuanPolis : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A02";
    string kodesumber = "AS";
    string KodeKonter = "AS03";
    public string nobuktidb;
    string namafileJaminan = "";
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        GRTP.Enabled = true;
      
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        pnltrn.Visible = true;
        txtKeterangan.Text = "-";
        Pnlfooter4.Visible = true;
    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
                txttgljthtmp.Text = txttgltrn.Text;

                //get tanggal server



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];             


               
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTASURANSI", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Pengajuan", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "NamaAsuransi";
                ddlKdBank.DataValueField = "KodeAsuransi";
                ddlKdBank.DataBind();
                ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));


            }


            //pwcabang system kurs single default IDR kurs 1
            if (SessSystemKurs == "False")
            {
                

                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = false;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = false;
            }
            else
            {
               

                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = true;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = true;
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

    protected void ddlkursBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkursbank.Text = ddlkursbank.SelectedValue;
    }
    protected void ddlKdBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIASURANSI", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Pengajuan", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaAsuransi"].ToString();
            string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            


            txtNmBank.Text = NamaBank;
            ddlkursbank.SelectedValue = kdcrc;
            txtkursbank.Text = "1";



            pnltrn.Visible = true;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var1[1] = gb.CreateSPParameter("@kodeasuransi", ParameterDirection.Input, SqlDbType.VarChar, 50, bank, typeof(String));

            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataTable = gb.GetDataJustOutPut("dbo.AS_Trn_Invoice_Pengajuan", var1, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {
                SetDetailBayar();
                ViewState["ListDetailBayar"] = ge.ListDataTable;

                GRTP.DataSource = ge.ListDataTable;
                GRTP.DataBind();

                GRTP.UseAccessibleHeader = true;
                GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridBayar();
                btnSimpan.Visible = true;
            }
            else
            {
                SetDetailBayar();
                btnSimpan.Visible = false;
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);






        }
        else
        {
            txtNmBank.Text = "";
            ddlkursbank.ClearSelection();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";
            

        }

    
    }

    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal jlhpengajuan; decimal jlhinvoice;
        jlhpengajuan = decimal.Parse(((Label)grow.FindControl("lblgrosspremi")).Text);
        jlhinvoice = decimal.Parse(((TextBox)grow.FindControl("lblbayar")).Text);
        if (jlhinvoice < jlhpengajuan)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Invoice tidak boleh kurang dari Jumlah Pengajuan');", true);
            ((TextBox)grow.FindControl("lblbayar")).Text = "0";
         
        }

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridbayar_Click();
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Invoice_Pengajuan", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti desc";


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

            case "SETDEL":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    GRTP.Enabled = false;
                    pnltrn.Visible = true;
                    btnSimpan.Visible = false;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    //ddlcabang.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcabangfilter")).Text;
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgljthtmp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgljthtmp")).Text;
                    txttgltrn.Enabled = false;
                    txttgljthtmp.Enabled = false;
                    ddlKdBank.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeAsuransi")).Text;
                    ddlKdBank.Enabled = false;

                    txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlPengajuan")).Text));
                    txtbayarinvoice.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtKeterangan.Enabled = false;
                    txtinvoice.Text=((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblReferenceInvoice")).Text;
                    txtinvoice.Enabled = false;
                     statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    //statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblhapusflag")).Text;
                    if (statusposting == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sudah ada Invoice!');", true);
                        btndelete.Enabled = false;
                    }
                    if (statusclosing == "True")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sudah Di posting Journal!');", true);
                        btndelete.Enabled = false;
                    }
                   
                    var bank = ddlKdBank.SelectedValue;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIASURANSI", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Invoice_Pengajuan", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaAsuransi"].ToString();
                        string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();



                        txtNmBank.Text = NamaBank;
                        ddlkursbank.SelectedValue = kdcrc;
                        txtkursbank.Text = "1";

                    }
                    else
                    {
                        txtNmBank.Text = "";
                        ddlkursbank.ClearSelection();
                        ddlkursbank.SelectedValue = "IDR";
                        txtkursbank.Text = "1";


                    }
                    
                   

                   
                    DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[6];
                    Paramst[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramst[1] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    Paramst[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTINVOICE", typeof(string));
                    Paramst[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramst[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramst[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AS_Trn_Invoice_Pengajuan", Paramst, "constrmf").Tables[0];

                        if (ge.ListDataTable.Rows.Count > 0)
                        {

                            ViewState["ListDetailBayar"] = ge.ListDataTable;

                            GRTP.DataSource = ge.ListDataTable;
                            GRTP.DataBind();

                            GRTP.UseAccessibleHeader = true;
                            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                            cssGridBayar();
                        }
                        else
                        {
                            SetDetailBayar();
                        }

                    GRTP.Enabled = false;


                    pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;



                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SET":

              
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
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AS_Trn_Invoice_Pengajuan", Paramsc, "constrmf").Tables[0];

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
                    typetrn = "K";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIP", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_TRANSFER", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashLain.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        if (typetrn == "B")
                        {
                            rprt.SetParameterValue("Reportname", "Terima Cash Bank");
                        }
                        else
                        {
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                        }


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
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {





            dtBayar.Columns.Clear();




            dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("nopolis", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("refpolis", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("No_Pengajuan", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("Tgl_Pengajuan", typeof(DateTime)));
            dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("nonasabah", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("namanasabah", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("Sex", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("pekerjaan", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("alamat", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("cabang", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("id", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("tptlahir", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("tgllahir", typeof(DateTime)));
            dtBayar.Columns.Add(new DataColumn("Usia", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("tglawal", typeof(DateTime)));
            dtBayar.Columns.Add(new DataColumn("tglakhir", typeof(DateTime)));
            dtBayar.Columns.Add(new DataColumn("Bulan", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("rateasuransi", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("GrossPremi", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("UnderWriting", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));




            dtBayar.Rows.Clear();
            int xi = 0;

            for (int i = 0; i < GRTP.Rows.Count; i++)
            {
                DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");


                if (ddlbayar.SelectedValue != "0")
                {
                    DataRow dr1 = dtBayar.NewRow();
                    xi = xi + 1;
                    int xUsia = int.Parse(((Label)GRTP.Rows[i].FindControl("lblusia")).Text);
                    int xBulan = int.Parse(((Label)GRTP.Rows[i].FindControl("lblbulan")).Text);

                    decimal xplafon = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblplafon")).Text);
                    decimal xrateasuransi = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblrateasuransi")).Text);
                    decimal xGrossPremi = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblGrossPremi")).Text);
                    decimal xbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);







                    DateTime Tgl_Pengajuan = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lblTgl_Pengajuan")).Text);
                    DateTime tgllahir = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgllahir")).Text);
                    DateTime tglawal = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltglawal")).Text);
                    DateTime tglakhir = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltglakhir")).Text);

                    dr1["No"] = xi;
                    dr1["nopolis"] = ((TextBox)GRTP.Rows[i].FindControl("lblpolis")).Text;
                    dr1["refpolis"] = ((TextBox)GRTP.Rows[i].FindControl("lblrefpolis")).Text;
                    dr1["No_Pengajuan"] = ((Label)GRTP.Rows[i].FindControl("lblNo_Pengajuan")).Text;
                    dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                    dr1["Tgl_Pengajuan"] = Tgl_Pengajuan;
                    dr1["nonasabah"] = ((Label)GRTP.Rows[i].FindControl("lblno_nasabah")).Text;
                    dr1["namanasabah"] = ((Label)GRTP.Rows[i].FindControl("lblnama_nasabah")).Text;
                    dr1["Sex"] = ((Label)GRTP.Rows[i].FindControl("lblSex")).Text;
                    dr1["pekerjaan"] = ((Label)GRTP.Rows[i].FindControl("lblpekerjaan")).Text;
                    dr1["alamat"] = ((Label)GRTP.Rows[i].FindControl("lblalamat")).Text;
                    dr1["cabang"] = ((Label)GRTP.Rows[i].FindControl("lblNamaCabang")).Text;
                    dr1["id"] = ((Label)GRTP.Rows[i].FindControl("lblid")).Text;
                    dr1["tptlahir"] = ((Label)GRTP.Rows[i].FindControl("lbltptlahir")).Text;
                    dr1["tgllahir"] = tgllahir;
                    dr1["Usia"] = xUsia;
                    dr1["tglawal"] = tglawal;
                    dr1["tglakhir"] = tglakhir;
                    dr1["Bulan"] = xBulan;
                    dr1["plafon"] = xplafon;
                    dr1["rateasuransi"] = xrateasuransi;
                    dr1["GrossPremi"] = xGrossPremi;
                    dr1["UnderWriting"] = ((Label)GRTP.Rows[i].FindControl("lblUnderWriting")).Text;
                    dr1["Bayar"] = xbayar;
                    dr1["TypeBayar"] = ddlbayar.SelectedValue;
                    dtBayar.Rows.Add(dr1);
                }
            }
            
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[17];

                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@Jumlahpengajuan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
                Params[3] = gb.CreateSPParameter("@JumlahInvoice", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayarinvoice.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[6] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[11] = gb.CreateSPParameter("@AS_Invoice_Pengajuan", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[12] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                Params[13] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[14] = gb.CreateSPParameter("@kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 30, "AS001", typeof(string));
                Params[15] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "DELETE", typeof(string));
                Params[16] = gb.CreateSPParameter("@ReferenceInvoice", ParameterDirection.Input, SqlDbType.VarChar, 100, txtinvoice.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Invoice_Pengajuan", Params, "constrmf");
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
                    btndelete.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Invoice Pengajuan Asuransi','top-right','success','1');", true);
                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

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
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "redirect",
            "window.location.href='MasukTrnInvoicePengajuanPolis.aspx';",
            true
        );

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
  

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            //bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "blokirtransfer"));
            angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "grosspremi"));
            //denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));

            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
            {


            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "grosspremi"));
            }

            string formatted = string.Format("{0:N2}", bayar);
            txtbayar.Text = formatted;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalplafon = (Label)e.Row.FindControl("lbltotalplafon");
            lbltotalplafon.Text = string.Format("{0:N2}", pokok);


            Label lbltotalgrosspremi = (Label)e.Row.FindControl("lbltotalgrosspremi");
            lbltotalgrosspremi.Text = string.Format("{0:N2}", angsuran);

         


        }
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtbayar.Text);

            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
            }
            else
            {

                dtBayar.Columns.Clear();




                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("nopolis", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("refpolis", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("No_Pengajuan", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Tgl_Pengajuan", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("nonasabah", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("namanasabah", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Sex", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("pekerjaan", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("alamat", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("cabang", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("id", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("tptlahir", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("tgllahir", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("Usia", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("tglawal", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("tglakhir", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("Bulan", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("rateasuransi", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("GrossPremi", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("UnderWriting", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));



                dtBayar.Rows.Clear();
                int xi = 0;

                for (int i = 0; i < GRTP.Rows.Count; i++)
                {
                    DropDownList ddlbayar = (DropDownList)GRTP.Rows[i].FindControl("ddlbayar");


                    if (ddlbayar.SelectedValue != "0")
                    {
                        DataRow dr1 = dtBayar.NewRow();
                        xi = xi + 1;
                        int xUsia = int.Parse(((Label)GRTP.Rows[i].FindControl("lblusia")).Text);
                        int xBulan = int.Parse(((Label)GRTP.Rows[i].FindControl("lblbulan")).Text);

                        decimal xplafon = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblplafon")).Text);
                        decimal xrateasuransi = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblrateasuransi")).Text);
                        decimal xGrossPremi = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblGrossPremi")).Text);
                        decimal xbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);






                       
                        DateTime Tgl_Pengajuan = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lblTgl_Pengajuan")).Text);
                        DateTime tgllahir = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgllahir")).Text);
                        DateTime tglawal = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltglawal")).Text);
                        DateTime tglakhir = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltglakhir")).Text);

                        dr1["No"] = xi;                    
                        dr1["nopolis"] = ((TextBox)GRTP.Rows[i].FindControl("lblpolis")).Text;
                        dr1["refpolis"] = ((TextBox)GRTP.Rows[i].FindControl("lblrefpolis")).Text;
                        dr1["No_Pengajuan"] = ((Label)GRTP.Rows[i].FindControl("lblNo_Pengajuan")).Text;
                        dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                        dr1["Tgl_Pengajuan"] = Tgl_Pengajuan;
                        dr1["nonasabah"] = ((Label)GRTP.Rows[i].FindControl("lblno_nasabah")).Text;
                        dr1["namanasabah"] = ((Label)GRTP.Rows[i].FindControl("lblnama_nasabah")).Text;
                        dr1["Sex"] = ((Label)GRTP.Rows[i].FindControl("lblSex")).Text;
                        dr1["pekerjaan"] = ((Label)GRTP.Rows[i].FindControl("lblpekerjaan")).Text;
                        dr1["alamat"] = ((Label)GRTP.Rows[i].FindControl("lblalamat")).Text;
                        dr1["cabang"] = ((Label)GRTP.Rows[i].FindControl("lblNamaCabang")).Text;
                        dr1["id"] = ((Label)GRTP.Rows[i].FindControl("lblid")).Text;
                        dr1["tptlahir"] = ((Label)GRTP.Rows[i].FindControl("lbltptlahir")).Text;
                        dr1["tgllahir"] = tgllahir;
                        dr1["Usia"] = xUsia;
                        dr1["tglawal"] = tglawal;
                        dr1["tglakhir"] = tglakhir;
                        dr1["Bulan"] = xBulan;
                        dr1["plafon"] = xplafon;
                        dr1["rateasuransi"] = xrateasuransi;
                        dr1["GrossPremi"] = xGrossPremi;
                        dr1["UnderWriting"] = ((Label)GRTP.Rows[i].FindControl("lblUnderWriting")).Text;
                        dr1["Bayar"] = xbayar;
                        dr1["TypeBayar"] = ddlbayar.SelectedValue;
                        dtBayar.Rows.Add(dr1);
                    }
                }


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[20];

                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@Jumlahpengajuan", ParameterDirection.Input, SqlDbType.Decimal, 0,txtbayar.Text, typeof(decimal));
                Params[3] = gb.CreateSPParameter("@JumlahInvoice", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayarinvoice.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@KodeAsuransi", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));               
                Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[6] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[11] = gb.CreateSPParameter("@AS_Invoice_Pengajuan", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[12] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                Params[13] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[14] = gb.CreateSPParameter("@kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 30, "AS001", typeof(string));
                Params[15] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "INSERT", typeof(string));
                Params[16] = gb.CreateSPParameter("@ReferenceInvoice", ParameterDirection.Input, SqlDbType.VarChar, 100, txtinvoice.Text, typeof(string));
                Params[17] = gb.CreateSPParameter("@tgljthtmp", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljthtmp.Text, typeof(DateTime));
                Params[18] = gb.CreateSPParameter("@file1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtfilename1.Text, typeof(string));
                Params[19] = gb.CreateSPParameter("@file_path1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtPdf1.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Invoice_Pengajuan", Params, "constrmf");
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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Invoice Pengajuan Asuransi','top-right','success','1');", true);
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
            txttgljthtmp.Text = txttgltrn.Text;

        }
        //txttgljthtmp.Text = txttgltrn.Text;

    }



    public void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        var Cabang = ddlcabang.SelectedValue;

        
       

  
     

    }
    private void cssGridBayar()
    {

        // Attribute to show the Plus Minus Button.
        GRTP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRTP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-class", "expand");
        cells[4].Attributes.Add("data-class", "expand");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand");
        cells[8].Attributes.Add("data-class", "expand");
        cells[9].Attributes.Add("data-class", "expand");
        cells[10].Attributes.Add("data-class", "expand");
        cells[11].Attributes.Add("data-class", "expand");
        cells[12].Attributes.Add("data-class", "expand");
        cells[13].Attributes.Add("data-class", "expand");
        cells[14].Attributes.Add("data-class", "expand");
        cells[15].Attributes.Add("data-class", "expand");
        cells[16].Attributes.Add("data-class", "expand");
        cells[17].Attributes.Add("data-class", "expand");
        cells[18].Attributes.Add("data-class", "expand");
        cells[19].Attributes.Add("data-class", "expand");
        cells[20].Attributes.Add("data-class", "expand");
        cells[21].Attributes.Add("data-class", "expand");
        cells[22].Attributes.Add("data-class", "expand");
        cells[23].Attributes.Add("data-class", "expand");
        cells[24].Attributes.Add("data-class", "expand");

    }

    private void SetDetailBayar()
    {

        //DataRow dr = null;     
        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("nopolis", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("refpolis", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("No_Pengajuan", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Tgl_Pengajuan", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("nonasabah", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("namanasabah", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Sex", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("pekerjaan", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("alamat", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("cabang", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("id", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("tptlahir", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("tgllahir", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("Usia", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("tglawal", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("tglakhir", typeof(DateTime)));
        dtBayar.Columns.Add(new DataColumn("Bulan", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("plafon", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("rateasuransi", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("GrossPremi", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("UnderWriting", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Bayar", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("TypeBayar", typeof(string)));


        ViewState["ListDetailBayar"] = dtBayar;
        GRTP.DataSource = dtBayar;
        GRTP.DataBind();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
    }

    protected void ddlbayar_SelectedIndexChanged(object sender, EventArgs e)
    {


        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "0")
        {
            ((TextBox)grow.FindControl("lblbayar")).Text = "0";
            ((TextBox)grow.FindControl("lblpolis")).Enabled = false;
            ((TextBox)grow.FindControl("lblrefpolis")).Enabled = false;
            ((TextBox)grow.FindControl("lblbayar")).Enabled = false;




        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "1")
        {
            ((TextBox)grow.FindControl("lblbayar")).Text = ((Label)grow.FindControl("lblgrosspremi")).Text;
            ((TextBox)grow.FindControl("lblpolis")).Enabled = true;
            ((TextBox)grow.FindControl("lblrefpolis")).Enabled = true;
            ((TextBox)grow.FindControl("lblbayar")).Enabled = true;
        }

        //else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "2")
        //{
        //    decimal angsuran = 0;
        //    decimal denda = 0;
        //    decimal total = 0;

        //    angsuran = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
        //    denda = decimal.Parse(((Label)grow.FindControl("lbldenda")).Text);
        //    total = angsuran + denda;
        //    ((Label)grow.FindControl("lblbayar")).Text = string.Format("{0:N2}", total);

        //}


        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridbayar_Click();

    }
    private void subgridbayar_Click()
    {

        int dgcount = GRTP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;

        decimal sumPengajuan = 0;
        decimal jlhPengajuan = 0;
        if (dgcount > 0)
        {


            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);
                sumjlhgl = sumjlhgl + jlhgl;


                jlhPengajuan = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblgrosspremi")).Text);
                sumPengajuan = sumPengajuan + jlhPengajuan;



            }



        }


        else
        {
            sumjlhgl = 0;
            sumPengajuan = 0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
        txtbayarinvoice.Text = formatted;

        txtbayar.Text = string.Format("{0:N2}", sumPengajuan);




    }

    protected void Uploadfoto1(object sender, EventArgs e)
    {
        try
        {
            string extension = Path.GetExtension(fileupload1.FileName).ToLower();
            if (extension != ".pdf")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya file PDF yang diperbolehkan!');", true);
                txtPdf1.Focus();
                return;
            }

            int maxSize = 4 * 1024 * 1024; // 5 MB
            if (fileupload1.PostedFile.ContentLength > maxSize)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript",
                    "javascript:SweetAlert('Ukuran file maksimal 4 MB!');", true);
                return;
            }

            string folderPath = Server.MapPath("~/Files/Kresun/FileAsuransi/");
            DateTime now = DateTime.Now;

            namafileJaminan = "InvAs" + "_" + SessBranchID + "_" + now.ToString("ddMMyyyyhhmmss");
            txtfilename1.Text = namafileJaminan + ".pdf";



            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtPdf1.Text = "";

            string uploadedFile = Path.Combine(folderPath, namafileJaminan + ".pdf");
            string compressedFile = Path.Combine(folderPath, namafileJaminan + "_tmp.pdf");

            fileupload1.SaveAs(uploadedFile);

            CompressPdf(uploadedFile, compressedFile);
            File.Delete(uploadedFile);
            File.Move(compressedFile, uploadedFile);

            txtPdf1.Text = "~/Files/Kresun/FileAsuransi/" + namafileJaminan + ".pdf";

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    private void CompressPdf(string inputPath, string outputPath)
    {
        using (var reader = new PdfReader(inputPath))
        {
            reader.RemoveUnusedObjects();

            using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                using (var document = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(1)))
                {
                    PdfCopy copy = new PdfCopy(document, fs);
                    copy.SetFullCompression();
                    document.Open();

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = copy.GetImportedPage(reader, i);
                        copy.AddPage(page);
                    }

                    document.Close();
                }
            }
        }
    }


}
