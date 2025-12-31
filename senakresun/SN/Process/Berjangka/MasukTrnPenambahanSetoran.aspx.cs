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

public partial class SN_Process_MasukTrnPenambahanSetoran : System.Web.UI.Page
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
    string form = "A05";
    string kodesumber = "SN";
    string KodeKonter = "SN06";
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
        txtKeterangan.Text = "Penambahan Setoran Berjangka";
    }
  
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasukTrnPenambahanSetoran.aspx");
    }

    public void BindGridEntityDatamutasi()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIMUTASI", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("SN_SETORAN_BERJANGKA", var, "constrmf");



        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening asc";


        Grmutasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grmutasi.DataBind();

        Grmutasi.UseAccessibleHeader = true;

        Grmutasi.HeaderRow.TableSection = TableRowSection.TableHeader;

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

                txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse(txtsaldopokok.Text));
                txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(txtsaldopokok.Text));
                txtjumlahtarikanpokok.ReadOnly = true;
                txtjumlahtarikanpokokbunga.ReadOnly = true;
                txtjumlahtarikanbunga.ReadOnly = true;
                txtjumlahtarikanpokok.ReadOnly = false;
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
 
        ge.ListDataset = gb.GetDataJustOutPut("SN_SETORAN_BERJANGKA", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti Desc";


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
                    ddljenistarikan.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txttypetarikan")).Text;
                    ddljenistarikan.Enabled = false;
                    txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldopokok")).Text));
                    txtjumlahtarikanpokok.Enabled = false;
                    txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldobunga")).Text));
                    txtjumlahtarikanbunga.Enabled = false;

                    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldopokokbunga")).Text));
                    txtjumlahtarikanpokokbunga.Enabled = false;

                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketerangan")).Text;
                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    
                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    //
                    //txtbayar.Text = string.Format("{0:N0}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btnSimpan.Visible = false;
        
                    btndelete.Visible = true;
                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;

                    txttgldrop.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgldrop")).Text;
                    txttgldrop.Enabled = false;

                    txtday.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDaybunga")).Text;
                    //txtday.Enabled = false;


                    txtbunga.Text = string.Format("{0:N0}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBungakotor")).Text));
                    //txtbunga.Enabled = false;

                    txtnilaipajak.Text = string.Format("{0:N0}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnilaipajak")).Text));
                    //txtnilaipajak.Enabled = false;

                    txtbungabersih.Text = string.Format("{0:N0}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbungabersih")).Text));
                    //txtbungabersih.Enabled = false;
                    txtpath.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpath")).Text;
                    txtfilename.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfilename")).Text;


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




                    var kodelgn = hdncustomer.Value;
                    string norekening = HDNoRekening.Value;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SN_SETORAN_BERJANGKA", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)


                    {
                        txtkodelgn.Text = kodelgn;
                        txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();


                        //HDNoNasabah.Value = no_nasabah;
                        txttglpermohonan.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglpermohonan"].ToString()).ToString("dd-MM-yyyy");

                        txtnopermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["Idpermohonan"].ToString();





                        txtproduk.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();

                        txtpajak.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString()));
                        txtbungapertahun.Text = string.Format("{0:N0}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString()));

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



                        btnSimpan.Enabled = false;
                    }

                    //ddlsetor.Enabled = false;
                    DefaultClass.Global.SqlHelper[] ParamPosting = new DefaultClass.Global.SqlHelper[4];
                    ParamPosting[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    ParamPosting[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                    ParamPosting[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    ParamPosting[3] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKPOSTING", typeof(String));
                    ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", ParamPosting, "constrmf");
                    string pubposting = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
                    if (pubposting == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting!');", true);
                        btndelete.Visible = false;
                    }
                    else
                    {
                        btndelete.Visible = true;



                    }


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
                    ge.ListDataTable = gb.GetDataJustOutPut("SN_SETORAN_BERJANGKA", Paramsc, "constrmf").Tables[0];





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
                        Response.End();

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
                        GC.Collect();
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

                   
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKWITANSI", typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.SN_SETORAN_BERJANGKA", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AR/Process/Report/CLKwitansiTerima.rpt"));

                        rprt.SetDataSource(ge.ListDataTable);

                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        Response.End();
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
                        GC.Collect();
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
            decimal jlhdibayar = decimal.Parse(txtjumlahtarikanpokokbunga.Text);


            btndelete.Enabled = false;
            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[6];
            varz[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnopermohonan.Text, typeof(string));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKDELJANGKA", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            varz[5] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckSimpanan", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btndelete.Enabled = true;
            }
            else
            {


                if (jlhdibayar <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Setoran Masih Kosong');", true);

                }
                else
                {



                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[16];
                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                    Params[3] = gb.CreateSPParameter("@TarikanPokok", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokok.Text, typeof(decimal));
                    Params[4] = gb.CreateSPParameter("@TarikanBunga", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanbunga.Text, typeof(decimal));
                    Params[5] = gb.CreateSPParameter("@totaltarikan", ParameterDirection.Input, SqlDbType.Money, 0, txtjumlahtarikanpokokbunga.Text, typeof(decimal));
                    Params[6] = gb.CreateSPParameter("@TypeTarikan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljenistarikan.SelectedValue), typeof(int));
                    Params[7] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    Params[8] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[9] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[10] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[11] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[12] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[13] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "DELETE", typeof(string));
                    Params[15] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_SETORAN_BERJANGKA", Params, "constrmf");
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
                        DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[6];
                        Paramx[0] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                        Paramx[1] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                        Paramx[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        Paramx[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        Paramx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        Paramx[5] = gb.CreateSPParameter("@Pajak", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtpajak.Text), typeof(float));
                        ge.ListDataset = gb.GetDataJustOutPut("SN_KARTU_PINJAMAN_DTL", Paramx, "constrmf");

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }

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
        Response.Redirect("MasukTrnPenambahanSetoran.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasukTrnPenambahanSetoran.aspx");

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //switch (e.CommandName)
        //{
        //    case "DEL":
        //        int dgcount = GRRP.Rows.Count;


        //        ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

        //        dtRP.Columns.Clear();

        //        dtRP = (DataTable)ViewState["ListDetailRTP"];

        //        DataRow drCurrentRow = null;

        //        dtRP.Rows.Clear();

        //        int j = 1;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {

        //            if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
        //            {


        //                drCurrentRow = dtRP.NewRow();

        //                drCurrentRow["nomor"] = j;
        //                drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
        //                drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
        //                drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
        //                drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
        //                drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
        //                drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
        //                drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

        //                dtRP.Rows.Add(drCurrentRow);

        //                j++;
        //            }
        //        }

        //        if (dgcount == 1)
        //        {
        //            DataRow drdnew = dtRP.NewRow();

        //            drdnew["nomor"] = 1;
        //            drdnew["TglTrnFaktur"] = string.Empty;
        //            drdnew["NoFaktur"] = string.Empty;
        //            drdnew["TypeTrn"] = string.Empty;
        //            drdnew["TglJthTmp"] = string.Empty;
        //            drdnew["Keterangan"] = string.Empty;
        //            drdnew["JumlahTrn"] = decimal.Zero;
        //            drdnew["KursStd"] = decimal.Zero;
        //            dtRP.Rows.Add(drdnew);
        //        }


        //        ViewState["ListDetailRTP"] = dtRP;

        //        GRTP.DataSource = dtRP;
        //        GRTP.DataBind();

        //        GRTP.UseAccessibleHeader = true;
        //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        cssGridBayar();



        //        break;


        //}
    }

    decimal debet = 0, kredit = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            debet += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "mutasidebet"));
            kredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "mutasikredit"));
       
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldebet = (Label)e.Row.FindControl("lbltotaldebet");
            lbltotaldebet.Text = string.Format("{0:N0}", debet);
            Label lbltotalkredit = (Label)e.Row.FindControl("lbltotalkredit");
            lbltotalkredit.Text = string.Format("{0:N0}", kredit);
        }
    }
    protected void UploadFile(object sender, EventArgs e)
    {




        string namafile;
        string fotopath;
        string folderPath = Server.MapPath("~/Files/TambahSetor/");
        DateTime now = DateTime.Now;

        namafile = now.ToString("ddMMyyyyhhmmss") + "_" + "BuktiTambahSetor";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }
        fileupload.SaveAs(folderPath + namafile + ".jpg");


        fotopath = folderPath + namafile + ".jpg";
        txtpath.Text = fotopath;


    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtjumlahtarikanpokokbunga.Text);
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Setoran Masih Kosong');", true);
            }
            else
            {

         

                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[24];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                Params[3] = gb.CreateSPParameter("@TarikanPokok", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(txtjumlahtarikanpokok.Text), typeof(decimal));
                Params[4] = gb.CreateSPParameter("@TarikanBunga", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(txtjumlahtarikanbunga.Text), typeof(decimal));
                Params[5] = gb.CreateSPParameter("@totaltarikan", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(txtjumlahtarikanpokokbunga.Text), typeof(decimal));
                Params[6] = gb.CreateSPParameter("@TypeTarikan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljenistarikan.SelectedValue), typeof(int));
                Params[7] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[9] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[10] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[11] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[12] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[13] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
                Params[15] = gb.CreateSPParameter("@tgldrop", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgldrop.Text, typeof(DateTime));
                Params[16] = gb.CreateSPParameter("@Bungakotor", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(txtbunga.Text), typeof(decimal));
                Params[17] = gb.CreateSPParameter("@Daybunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtday.Text), typeof(int));
                Params[18] = gb.CreateSPParameter("@nilaipajak", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(txtnilaipajak.Text), typeof(decimal));
                Params[19] = gb.CreateSPParameter("@bungabersih", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(txtbungabersih.Text), typeof(decimal));
                Params[20] = gb.CreateSPParameter("@PathFile", ParameterDirection.Input, SqlDbType.VarChar, 250, txtpath.Text, typeof(string));
                Params[21] = gb.CreateSPParameter("@filename", ParameterDirection.Input, SqlDbType.VarChar, 80, "-", typeof(string));
                Params[22] = gb.CreateSPParameter("@pathbuktitrn", ParameterDirection.Input, SqlDbType.VarChar, 100, "-", typeof(string));
                Params[23] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("SN_SETORAN_BERJANGKA", Params, "constrmf");
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
                    DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[6];
                    Paramx[0] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    Paramx[1] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Paramx[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramx[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramx[5] = gb.CreateSPParameter("@Pajak", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtpajak.Text), typeof(float));
                    ge.ListDataset = gb.GetDataJustOutPut("SN_KARTU_PINJAMAN_DTL", Paramx, "constrmf");

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
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        //varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        //varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        //varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        //string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        //if (xstatus == "1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
        //    txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        //    //txttgljthtmp.Text = txttgltrn.Text;
        //}
    }
    protected void txtjumlahtarikanpokok_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        decimal saldoawal=0, saldopokok=0;
        saldoawal = decimal.Parse(txtjumlahtarikanpokok.Text);
        saldopokok = decimal.Parse(txtsaldopokok.Text);

        txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
      
        //txtbayar.Text = string.Format("{0:N0}", saldoawal);
    }
    protected void txtjumlahtarikanbunga_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        decimal saldoawal = 0, saldopokok = 0;
        saldoawal = decimal.Parse(txtjumlahtarikanbunga.Text);
        saldopokok = decimal.Parse(txtsaldobunga.Text);

        if (saldoawal > saldopokok)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Bunga tidak boleh lebih dari saldo Bunga!');", true);
            txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        }
        else
        {
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(txtjumlahtarikanpokok.Text) + decimal.Parse(txtjumlahtarikanbunga.Text));
        }


        //txtbayar.Text = string.Format("{0:N0}", saldoawal);
    }

    protected void ddljenistarikan_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        if (ddljenistarikan.SelectedValue == "1")
        {
            txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse(txtsaldopokok.Text));
            txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse(txtsaldobunga.Text));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(txtsaldopokok.Text) + decimal.Parse(txtsaldobunga.Text));
            txtjumlahtarikanpokok.ReadOnly = true;
            txtjumlahtarikanpokokbunga.ReadOnly = true;
            txtjumlahtarikanbunga.ReadOnly = true;
            txtjumlahtarikanpokok.ReadOnly = false;
            txtjumlahtarikanbunga.ReadOnly = false;
        }
        else if (ddljenistarikan.SelectedValue == "2")
        {
            txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse(txtsaldopokok.Text));
            txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(txtsaldopokok.Text));
            txtjumlahtarikanpokok.ReadOnly = true;
            txtjumlahtarikanpokokbunga.ReadOnly = true;
            txtjumlahtarikanbunga.ReadOnly = true;
            txtjumlahtarikanpokok.ReadOnly = false;
        }
        else if (ddljenistarikan.SelectedValue == "3")
        {
            txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse("0"));
            txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse(txtsaldobunga.Text));
            txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(txtsaldobunga.Text));
            txtjumlahtarikanpokok.ReadOnly = true; 
            txtjumlahtarikanpokokbunga.ReadOnly = true;
            txtjumlahtarikanbunga.ReadOnly = true;
            txtjumlahtarikanbunga.ReadOnly = false;
        }

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            var kodelgn = hdncustomer.Value;
            string norekening = HDNoRekening.Value;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@No_Rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("SN_SETORAN_BERJANGKA", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                txtkodelgn.Text = kodelgn;
                txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

                txttglpermohonan.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglpermohonan"].ToString()).ToString("dd-MM-yyyy");
                txtnopermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["Idpermohonan"].ToString();
                txtproduk.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();

                txtpajak.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString()));
                txtbungapertahun.Text = string.Format("{0:N0}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString()));
                txtbungaperhari.Text = string.Format("{0:N6}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
                ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();
                txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();
                btnSimpan.Visible = true;

                txttgldrop.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString()).ToString("dd-MM-yyyy");
                txtday.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["daybunga"].ToString()));
                txtbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungakotor"].ToString()));
                txtnilaipajak.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaipajak"].ToString()));
                txtbungabersih.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungabersih"].ToString()));

                txtsaldoawalpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoawal"].ToString()));
                txtsetoranpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["setoran"].ToString()));
                txttarikanpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tarikan"].ToString()));
                txtsaldopokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldopokok"].ToString()));
                txtbungatransfer.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungatransfer"].ToString()));
                txtbungarollon.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungarollon"].ToString()));
                txtbungakeluar.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungakeluar"].ToString()));
                txtsaldobunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobunga"].ToString()));
                txtsaldojalan.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldojalan"].ToString()));
                if (ddljenistarikan.SelectedValue == "1")
                {
                    txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldopokok"].ToString()));
                    txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobunga"].ToString()));
                    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldopokok"].ToString()) + decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobunga"].ToString()));
                    txtjumlahtarikanpokok.ReadOnly = true;
                    txtjumlahtarikanpokokbunga.ReadOnly = true;
                    txtjumlahtarikanbunga.ReadOnly = true;
                    txtjumlahtarikanpokok.ReadOnly = false;
                    txtjumlahtarikanbunga.ReadOnly = false;
                }
                else if (ddljenistarikan.SelectedValue == "2")
                {
                    txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldopokok"].ToString()));
                    txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldopokok"].ToString()));
                    txtjumlahtarikanpokok.ReadOnly = true;
                    txtjumlahtarikanpokokbunga.ReadOnly = true;
                    txtjumlahtarikanbunga.ReadOnly = true;
                    txtjumlahtarikanpokok.ReadOnly = false;
                }
                else if (ddljenistarikan.SelectedValue == "3")
                {
                    txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse("0"));
                    txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobunga"].ToString()));
                    txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobunga"].ToString()));
                    txtjumlahtarikanpokok.ReadOnly = true;
                    txtjumlahtarikanpokokbunga.ReadOnly = true;
                    txtjumlahtarikanbunga.ReadOnly = true;
                    txtjumlahtarikanbunga.ReadOnly = false;
                }
                BindGridEntityDatamutasi();
            }
            else
            {
                txtkodelgn.Text = "";
                txtNmSup.Text = "";
                txttglpermohonan.Text = "";
                txtnopermohonan.Text = "";
                txtproduk.Text = "";

                txtpajak.Text = "";
                txtbungapertahun.Text = "";

                txtbungaperhari.Text = "";
                ddlInstruktur.SelectedValue = "";
                ddlpenarikanbunga.SelectedValue = "";

                txtjangka.Text = "";

                txtjumlahtarikanpokok.Text = string.Format("{0:N0}", decimal.Parse("0")); ;
                txtjumlahtarikanbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                txtjumlahtarikanpokokbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));
                txtjumlahtarikanpokok.Enabled = false;
                txtjumlahtarikanpokokbunga.Enabled = false;
                txtjumlahtarikanbunga.Enabled = false;

                btnSimpan.Enabled = false;

                txttgldrop.Text = "";

                txtday.Text = string.Format("{0:N0}", decimal.Parse("0"));

                txtbunga.Text = string.Format("{0:N0}", decimal.Parse("0"));

                txtnilaipajak.Text = string.Format("{0:N0}", decimal.Parse("0"));
                txtbungabersih.Text = string.Format("{0:N0}", decimal.Parse("0"));
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SN_getCustomer_setoran_berjangka", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
}
