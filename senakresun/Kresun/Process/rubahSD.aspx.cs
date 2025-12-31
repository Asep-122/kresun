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
public partial class Kresun_Process_rubahAsuransi : System.Web.UI.Page
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
    string form = "G1";
    string kodesumber = "KP";
    string KodeKonter = "KP12";
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
        btnSimpan.Visible = false;
        pnlnasabah.Visible = true;
        PnlEntry.Visible = true;
        txtKeterangan.Text = "-";
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

                //KODE Asuransi
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
                var3[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTASMST", typeof(string));
                var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_HUTANG_AS", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAsuransi asc";
                ddlasuransibaru.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlasuransibaru.DataTextField = "namaasuransi";
                ddlasuransibaru.DataValueField = "KodeAsuransi";
                ddlasuransibaru.DataBind();
                ddlasuransibaru.Items.Insert(0, new ListItem("Pilih Data", "-"));
            }


            if (SessSystemKurs == "False")
            {
                ddlkodecrcasuransilama.Items.Insert(0, new ListItem("IDR"));
                ddlkodecrcasuransilama.SelectedValue = "IDR";
                ddlkodecrcasuransilama.Enabled = false;
                txtkursasuransilama.Text = "1";
                txtkursasuransilama.Enabled = false;

                ddlcrcasuransibaru.Items.Insert(0, new ListItem("IDR"));
                ddlcrcasuransibaru.SelectedValue = "IDR";
                ddlcrcasuransibaru.Enabled = false;
                txtkursasuransibaru.Text = "1";
                txtkursasuransibaru.Enabled = false;
            }
            else
            {
                ddlkodecrcasuransilama.Items.Insert(0, new ListItem("IDR"));
                ddlkodecrcasuransilama.SelectedValue = "IDR";
                ddlkodecrcasuransilama.Enabled = true;
                txtkursasuransilama.Text = "1";
                txtkursasuransilama.Enabled = true;

                ddlcrcasuransibaru.Items.Insert(0, new ListItem("IDR"));
                ddlcrcasuransibaru.SelectedValue = "IDR";
                ddlcrcasuransibaru.Enabled = true;
                txtkursasuransibaru.Text = "1";
                txtkursasuransibaru.Enabled = true;
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
 
        ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_ASURANSI", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ddlasuransibaru_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlasuransibaru.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
        var[0] = gb.CreateSPParameter("@KodeAsuransiBaru", ParameterDirection.Input, SqlDbType.VarChar, 30, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@Plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal));
        var[2] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, txtjangka.Text, typeof(int));
        var[3] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.Int, 0, txtumur.Text, typeof(int));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIASURANSIBARU", typeof(string));
        var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_ASURANSI", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {

            txtnamaasuransibaru.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAsuransi"].ToString();
            ddlcrcasuransibaru.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            txtrateasuransiBaru.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rate"].ToString()));
            txtjumlahasuransiBaru.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jumlah"].ToString()));
            txtkursasuransibaru.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["kurs"].ToString()));


           

        }
        else
        {
            txtnamaasuransibaru.Text = "";
            ddlcrcasuransibaru.SelectedValue = "-";
            txtrateasuransiBaru.Text = "";
            txtjumlahasuransiBaru.Text = "";
            txtkursasuransibaru.Text = "1";


        }


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
                   
                    //txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    //txtkodelgn.Enabled = false;
                    //txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    //txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    //Txtnorekening.Text = HDNoRekening.Value;
                    //txtnamask.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaSK")).Text;
                    //txtnosk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoSK")).Text;
                    //txttglsk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglSK")).Text;
                    //txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    //txtpathktp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;
                    //txtlink.Text = "View";
                    //txtlink.NavigateUrl = txtpathktp.Text;
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btnSimpan.Visible = true;
                    
                    //txttglsk.Enabled = true;
                    //txtnamask.Enabled = true;
                    //txtnosk.Enabled = true;
                    txttgltrn.Enabled = true;
                    txtKeterangan.Enabled = true;
                    //fileuploadktp.Enabled = true;
                    //btnUploadktp.Enabled = true;

                    btndelete.Visible = false;


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":
                try

                {





                    pnlnasabah.Visible = true;
                    PnlEntry.Visible = true;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    ddlasuransibaru.Enabled = false;

                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;

                    Pnlfooter4.Visible = true;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    txtnorekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    txtnasabah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                    txtnamanasabah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaNasabah")).Text;
                    txtplafon.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPlafon")).Text;
                    
                    txtrateuniversal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrateasuransiuniversal")).Text;
                    txtjumlahuniversal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblJumlahrateasuransiuniversal")).Text;
                    txtumur.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblUmur")).Text;
                    txtjangka.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljangka")).Text;

                    txtkodeasuransilama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeAsuransiLama")).Text;
                    txtnamaasuransilama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaAsuransiLama")).Text;
                    ddlkodecrcasuransilama.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrcLama")).Text;
                    txtkursasuransilama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKursLama")).Text;
                    txtrateasuransilama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrateLama")).Text;
                    txtjumlahasuransilama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljumlahLama")).Text;

                    ddlasuransibaru.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeAsuransiBaru")).Text;
                    txtnamaasuransibaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaAsuransiBaru")).Text;
                    ddlcrcasuransibaru.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrcbaru")).Text;
                    txtkursasuransibaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKursbaru")).Text;
                    txtrateasuransiBaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblratebaru")).Text;
                    txtjumlahasuransiBaru.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljumlahbaru")).Text;

                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;


                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;
                    btnSimpan.Visible = false;
                    btndelete.Visible = true;

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

                    //txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    //txtkodelgn.Enabled = false;
                    //txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    //txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));


                    //hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    //Txtnorekening.Text = HDNoRekening.Value;
                    //txtnamask.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaSK")).Text;
                    //txtnosk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoSK")).Text;
                    //txttglsk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglSK")).Text;
                    //txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    //txtpathktp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;
                    //txtlink.Text = "View";
                    //txtlink.NavigateUrl = txtpathktp.Text;
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
            




                   




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
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR", Paramsc, "constrmf").Tables[0];

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
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAR.rpt"));
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
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            //string txtpath = txtpathktp.Text;
            string State = "";

            if (ddlasuransibaru.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Asuransi Baru Masih Kosong!');", true);
            }
            else
            {



                    State = "DELETE";
             

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[31];



                Params[0] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[1] = gb.CreateSPParameter("@kodekonter", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string));
                Params[2] = gb.CreateSPParameter("@KodeAsuransiBaru", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlasuransibaru.SelectedValue, typeof(string));
                Params[3] = gb.CreateSPParameter("@NamaAsuransiBaru", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamaasuransibaru.Text, typeof(string));
                Params[4] = gb.CreateSPParameter("@kodecrcbaru", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcrcasuransibaru.SelectedValue, typeof(string));
                Params[5] = gb.CreateSPParameter("@Kursbaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkursasuransibaru.Text, typeof(decimal));
                Params[6] = gb.CreateSPParameter("@ratebaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateasuransiBaru.Text, typeof(decimal));
                Params[7] = gb.CreateSPParameter("@jumlahbaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahasuransiBaru.Text, typeof(decimal));
                Params[8] = gb.CreateSPParameter("@KodeAsuransiLama", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkodecrcasuransilama.SelectedValue, typeof(string));
                Params[9] = gb.CreateSPParameter("@NamaAsuransiLama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamaasuransilama.Text, typeof(string));
                Params[10] = gb.CreateSPParameter("@kodecrcLama", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkodecrcasuransilama.SelectedValue, typeof(string));
                Params[11] = gb.CreateSPParameter("@KursLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkursasuransilama.Text, typeof(decimal));
                Params[12] = gb.CreateSPParameter("@rateLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateasuransilama.Text, typeof(decimal));
                Params[13] = gb.CreateSPParameter("@jumlahLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahasuransilama.Text, typeof(decimal));
                Params[14] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text, typeof(string));
                Params[15] = gb.CreateSPParameter("@NamaNasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
                Params[16] = gb.CreateSPParameter("@Plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal));
                Params[17] = gb.CreateSPParameter("@rateasuransiuniversal", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateuniversal.Text, typeof(decimal));
                Params[18] = gb.CreateSPParameter("@Jumlahrateasuransiuniversal", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahuniversal.Text, typeof(decimal));
                Params[19] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, txtjangka.Text, typeof(int));
                Params[20] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.Int, 0, txtumur.Text, typeof(int));
                Params[21] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnorekening.Text, typeof(string));
                Params[22] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 30, kodesumber, typeof(string));
                Params[23] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[24] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[25] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[26] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[27] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[28] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
                Params[29] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                Params[30] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_ASURANSI", Params, "constrmf");
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

                    //    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Change Asuransi','top-right','success','1');", true);
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
      
    }

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok+= Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
            bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bunga"));
            angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
            {
                

            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            }
            else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 2)
            {
                bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            }
            
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
            lbltotalPokok.Text = string.Format("{0:N2}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N2}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N2}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N2}", denda);



        }
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

            //invoce yg dibayarkan
            //string txtpath = txtpathktp.Text;
            string State = "";

            if (ddlasuransibaru.SelectedValue =="-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Asuransi Baru Masih Kosong!');", true);
            }
            else
            {

           

           if (txtnobukti.Text == "")
            {
                    State = "INSERT";
            }
           else
                {
                    State = "UPDATE";
                }

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[31];



                    Params[0] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[1] = gb.CreateSPParameter("@kodekonter", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string));
                    Params[2] = gb.CreateSPParameter("@KodeAsuransiBaru", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlasuransibaru.SelectedValue, typeof(string));
                    Params[3] = gb.CreateSPParameter("@NamaAsuransiBaru", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamaasuransibaru.Text, typeof(string));
                    Params[4] = gb.CreateSPParameter("@kodecrcbaru", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcrcasuransibaru.SelectedValue, typeof(string));
                    Params[5] = gb.CreateSPParameter("@Kursbaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkursasuransibaru.Text, typeof(decimal));
                    Params[6] = gb.CreateSPParameter("@ratebaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateasuransiBaru.Text, typeof(decimal));
                    Params[7] = gb.CreateSPParameter("@jumlahbaru", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahasuransiBaru.Text, typeof(decimal));
                    Params[8] = gb.CreateSPParameter("@KodeAsuransiLama", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkodecrcasuransilama.SelectedValue, typeof(string));
                    Params[9] = gb.CreateSPParameter("@NamaAsuransiLama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamaasuransilama.Text, typeof(string));
                    Params[10] = gb.CreateSPParameter("@kodecrcLama", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkodecrcasuransilama.SelectedValue, typeof(string));
                    Params[11] = gb.CreateSPParameter("@KursLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtkursasuransilama.Text, typeof(decimal));
                    Params[12] = gb.CreateSPParameter("@rateLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateasuransilama.Text, typeof(decimal));
                    Params[13] = gb.CreateSPParameter("@jumlahLama", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahasuransilama.Text, typeof(decimal));
                    Params[14] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnasabah.Text, typeof(string));
                    Params[15] = gb.CreateSPParameter("@NamaNasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
                    Params[16] = gb.CreateSPParameter("@Plafon", ParameterDirection.Input, SqlDbType.Decimal, 0, txtplafon.Text, typeof(decimal));
                    Params[17] = gb.CreateSPParameter("@rateasuransiuniversal", ParameterDirection.Input, SqlDbType.Decimal, 0, txtrateuniversal.Text, typeof(decimal));
                    Params[18] = gb.CreateSPParameter("@Jumlahrateasuransiuniversal", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjumlahuniversal.Text, typeof(decimal));
                    Params[19] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, txtjangka.Text, typeof(int));
                    Params[20] = gb.CreateSPParameter("@Umur", ParameterDirection.Input, SqlDbType.Int, 0, txtumur.Text, typeof(int));
                    Params[21] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnorekening.Text, typeof(string));
                    Params[22] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 30,kodesumber, typeof(string));
                    Params[23] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[24] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[25] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));               
                    Params[26] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[27] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[28] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
                    Params[29] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                    Params[30] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_ASURANSI", Params, "constrmf");
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
                       
                    //    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Change Asuransi','top-right','success','1');", true);
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
   
    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIASURANSI", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_CHANGE_ASURANSI", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
   //         select x.no_nasabah,x.nama_nasabah, x.no_rekening,x.jangka,x.plafon,x.rateasuransiuniversal,x.usia,(x.rateasuransiuniversal * x.plafon) / 100 as jumlahrate
			//,y.kodeasuransi,y.RateAsuransi,(y.RateAsuransi * x.plafon) / 100 as jumlahrateasuransi,
			//a.NamaAsuransi,a.KodeCrc,1 as kurs


            txtnasabah.Text= ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
            txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
            txtumur.Text = ge.ListDataset.Tables[0].Rows[0]["usia"].ToString();

            txtplafon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString()));
            txtrateuniversal.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["rateasuransiuniversal"].ToString()));
            txtjumlahuniversal.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jumlahrate"].ToString()));


            txtkodeasuransilama.Text = ge.ListDataset.Tables[0].Rows[0]["kodeasuransi"].ToString();
            txtrateasuransilama.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["RateAsuransi"].ToString())); 
            txtjumlahasuransilama.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["jumlahrateasuransi"].ToString()));
            txtnamaasuransilama.Text = ge.ListDataset.Tables[0].Rows[0]["NamaAsuransi"].ToString();
            ddlkodecrcasuransilama.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            txtkursasuransilama.Text = ge.ListDataset.Tables[0].Rows[0]["kurs"].ToString();



           
            btnSimpan.Visible = true;

         
            txttgltrn.Enabled = true;
            txtKeterangan.Enabled = true;
      

        }

        else
        {
            txtnasabah.Text = "";
            txtnamanasabah.Text = "";
            txtnorekening.Text = "";
            txtjangka.Text = "";
            txtumur.Text = "";

            txtplafon.Text = "";
            txtrateuniversal.Text = "";
            txtjumlahuniversal.Text = "";


            txtkodeasuransilama.Text = "";
            txtrateasuransilama.Text = "";
            txtjumlahasuransilama.Text = "";
            txtnamaasuransilama.Text = "";
            ddlkodecrcasuransilama.SelectedValue = "IDR";
            txtkursasuransilama.Text = "1";

            btnSimpan.Enabled = false;
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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.KP_getAsuransi_Baru", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }








}
