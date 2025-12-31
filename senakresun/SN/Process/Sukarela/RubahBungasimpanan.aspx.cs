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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Activities.Statements;
using DocumentFormat.OpenXml.Spreadsheet;

public partial class Kresun_Process_RubahBungasimpananSukarela : System.Web.UI.Page
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
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
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
    string form = "S2";
    string kodesumber = "SN";
    string KodeKonter = "SN02";
    public string nobuktidb;

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        //btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter4.Visible = true;
        btnSimpan.Visible = false;
        ddlproduk.Enabled = false;
        ddlKoperasi.Enabled = false;
        ddlcabang.Enabled = false;
        ddljangka.Enabled = false;
        ddlbunga.Enabled = false;

        txttgltrn.ReadOnly = false;
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
           


                //KODE LANGGANAN
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKOPERASI", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SN_Permohonan", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "nama_koperasi";
                ddlKoperasi.DataValueField = "kode_koperasi";
                ddlKoperasi.DataBind();


                ddlKoperasi.SelectedValue = SessKoperasiID;
                ddlKoperasi.Enabled = false;
                //ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));


                var Koperasi = ddlKoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
              
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                //ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlcabang.SelectedValue = SessBranchID;
                ddlcabang.Enabled = false;

                //KODE Produk
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPRODUCT", typeof(string));
                var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var4, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_produk asc";
                ddlproduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlproduk.DataTextField = "nama_produk";
                ddlproduk.DataValueField = "kode_produk";
                ddlproduk.DataBind();
                ddlproduk.Items.Insert(0, new ListItem("Pilih Data", "-"));
      


                //ddlpajak.SelectedValue = "0";
                ////ddlpajak.Items.Insert(0, new ListItem("Pilih Data", "-"));

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

  protected void ddlproduk_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Produk = ddlproduk.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
            ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljangka.DataTextField = "Jangka";
            ddljangka.DataValueField = "Jangka";
            ddljangka.DataBind();
            //ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddljangka_SelectedIndexChanged(sender, e);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddljangka_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Produk = ddlproduk.SelectedValue;
            int jangka = int.Parse(ddljangka.SelectedValue);
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
            ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
            ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlbunga.DataTextField = "BungaTahunan";
            ddlbunga.DataValueField = "BungaTahunan";
            ddlbunga.DataBind();
            ddlbunga_SelectedIndexChanged(sender, e);
            //ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

   
 public void BindGridEntity()
    {
        try
        {

        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBUNGAX", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@typepermohonan", ParameterDirection.Input, SqlDbType.VarChar, 1, "B", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening,tgl_due asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    protected void ddlpenarikanbunga_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpenarikanbunga.SelectedValue == "1")
        {

            ddlrollover.Enabled = false;
        }
        else
        {

            if (ddlpenarikanbunga.SelectedValue == "3")
            {
                ddlrollover.Enabled = true;
            }
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
                    txttgltrn.ReadOnly = true;
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                    Pnlfooter4.Visible = true;
                    btnSimpan.Visible = true;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;


                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFOREDITXX", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");

                    //HDNoNasabah.Value = no_nasabah;
                    txtnonasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                    txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

                    txtbankrekening.Text = ge.ListDataset.Tables[0].Rows[0]["BankRekening"].ToString();
                    txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["NoRekeningBank"].ToString();
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["AtasNama"].ToString();

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrndrop")).Text;

                    

             

                    ddlKoperasi.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["koperasi"].ToString();
                    ddlproduk.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["produk"].ToString();


                    ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();
                    ddlrollover.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["pencairanbunga"].ToString();

                    txtbungaperhari.Text = string.Format("{0:N6}",float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

                    txtnilaisaldo.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldojalan"].ToString()));
                                  txtnilaibungapertahun.Text = "0";
                    txtnilaibungaperhari.Text = "0";



                    if (ddlpenarikanbunga.SelectedValue == "1")
                    {

                        ddlrollover.Enabled = false;
                    }
                    else
                    {

                        if (ddlpenarikanbunga.SelectedValue == "3")
                        {
                            ddlrollover.Enabled = true;
                        }
                    }

                    var cabang= ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                    var jangkabulan= ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

                    var norekening = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();

                    float bunga = float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString());


                    var Koperasi = ddlKoperasi.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[3];
                    varx[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANGXXX", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varx, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                    ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlcabang.DataTextField = "nama_cabang";
                    ddlcabang.DataValueField = "kode_cabang";
                    ddlcabang.DataBind();
                    ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));





                    ddlcabang.SelectedValue = cabang;




                    var Produk = ddlproduk.SelectedValue;
                    DefaultClass.Global.SqlHelper[] varc = new DefaultClass.Global.SqlHelper[4];
                    varc[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                    varc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIJANGKA", typeof(string));
                    varc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varc[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varc, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "Jangka asc";
                    ddljangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljangka.DataTextField = "Jangka";
                    ddljangka.DataValueField = "Jangka";
                    ddljangka.DataBind();
                    ddljangka.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddljangka.SelectedValue = jangkabulan;


                    int jangka = int.Parse(ddljangka.SelectedValue);
                    DefaultClass.Global.SqlHelper[] varj = new DefaultClass.Global.SqlHelper[5];
                    varj[0] = gb.CreateSPParameter("@Produk", ParameterDirection.Input, SqlDbType.VarChar, 15, Produk, typeof(string));
                    varj[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBUNGA", typeof(string));
                    varj[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varj[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                    varj[4] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, jangka, typeof(int));
                    ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", varj, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "BungaTahunan asc";
                    ddlbunga.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlbunga.DataTextField = "BungaTahunan";
                    ddlbunga.DataValueField = "BungaTahunan";
                    ddlbunga.DataBind();
                    ddlbunga.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddlbunga.SelectedValue = bunga.ToString();




                    decimal jlhbayar = 0;
                    if (decimal.Parse(ddlbunga.SelectedValue) >= 0)
                    {


                        jlhbayar = ((decimal.Parse(ddlbunga.SelectedValue) / 100) / 365) * 100;
                        txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
                    }
                    else
                    {
                        txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
                    }


                    decimal jlhbayartahun = 0;
                    decimal jlhbayarhari = 0;
                    if (decimal.Parse(txtnilaisaldo.Text) >= 0)
                    {


                        jlhbayartahun = (decimal.Parse(ddlbunga.SelectedValue) / 100) * decimal.Parse(txtnilaisaldo.Text);
                        jlhbayarhari = (decimal.Parse(txtbungaperhari.Text) / 100) * decimal.Parse(txtnilaisaldo.Text);
                        txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
                        txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
                        //txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);

                    }
                    else
                    {
                        txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
                        txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
                    }


                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
          

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
            lbltotalPokok.Text = string.Format("{0:N0}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N0}", bunga);

            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N0}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N0}", denda);



        }
    }

    

         protected void txtnilaisaldo_TextChanged(object sender, EventArgs e)
    {
        decimal jlhbayartahun = 0;
        decimal jlhbayarhari = 0;
        if (decimal.Parse(txtnilaisaldo.Text) >= 0)
        {


            jlhbayartahun = (decimal.Parse(ddlbunga.SelectedValue) / 100) * decimal.Parse(txtnilaisaldo.Text);
            jlhbayarhari = (decimal.Parse(txtbungaperhari.Text) / 100) * decimal.Parse(txtnilaisaldo.Text);
            txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
            //txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);

        }
        else
        {
            txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
        }


    }

    protected void ddlbunga_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal jlhbayar = 0;
        if (decimal.Parse(ddlbunga.SelectedValue) >= 0)
        {


            jlhbayar = ((decimal.Parse(ddlbunga.SelectedValue) / 100) / 365) * 100;
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
        }
        else
        {
            txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);
        }

        decimal jlhbayartahun = 0;
        decimal jlhbayarhari = 0;
        if (decimal.Parse(txtnilaisaldo.Text) >= 0)
        {


            jlhbayartahun = (decimal.Parse(ddlbunga.SelectedValue) / 100) * decimal.Parse(txtnilaisaldo.Text);
            jlhbayarhari = (decimal.Parse(txtbungaperhari.Text) / 100) * decimal.Parse(txtnilaisaldo.Text);
            txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
            //txtbungaperhari.Text = string.Format("{0:N6}", jlhbayar);

        }
        else
        {
            txtnilaibungapertahun.Text = string.Format("{0:N0}", jlhbayartahun);
            txtnilaibungaperhari.Text = string.Format("{0:N0}", jlhbayarhari);
        }
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {
                        //invoce yg dibayarkan
            //string txtpath = txtpathktp.Text;
            string State = "";
            int filex = 0;
            string filename = "";
            string fotopath;
            decimal nilai = 0;
            nilai = decimal.Parse(txtnilaisaldo.Text);
           
            if (txtnobukti.Text == "")
                {
                    State = "INSERT";
    
            }
           else
                {
                    State = "UPDATE";
                }



        
            if (ddlproduk.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Produk Masih Kosong!');", true);
            }
            else if (ddljangka.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jangka Masih Kosong!');", true);
            }
            else if (ddlbunga.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bunga Masih Kosong!');", true);
            }
            else if (nilai <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nilai Saldo Masih Kosong!');", true);
            }

           else
            {
              

                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[18];
                    Params[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    Params[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                    Params[3] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnonasabah.Text, typeof(string));
                    Params[4] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamanasabah.Text, typeof(string));
                
                    Params[5] = gb.CreateSPParameter("@produk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                    Params[6] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddljangka.SelectedValue), typeof(int));
                    Params[7] = gb.CreateSPParameter("@bungapertahun", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(ddlbunga.SelectedValue), typeof(float));
                    Params[8] = gb.CreateSPParameter("@bungaperhari", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtbungaperhari.Text), typeof(float));
                    
                    Params[9] = gb.CreateSPParameter("@nilaibungapertahun", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtnilaibungapertahun.Text), typeof(decimal));
                    Params[10] = gb.CreateSPParameter("@nilaibungaperhari", ParameterDirection.Input, SqlDbType.Decimal, 0, decimal.Parse(txtnilaibungaperhari.Text), typeof(decimal));
                    Params[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[12] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEBUNGA", typeof(string));
                    Params[13] = gb.CreateSPParameter("@rollover", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlrollover.SelectedValue), typeof(int));
                    Params[14] = gb.CreateSPParameter("@PenarikanBunga", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlpenarikanbunga.SelectedValue), typeof(int));
                    Params[15] = gb.CreateSPParameter("@BankRekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtbankrekening.Text, typeof(string));
                    Params[16] = gb.CreateSPParameter("@NoRekeningBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
                    Params[17] = gb.CreateSPParameter("@AtasNama", ParameterDirection.Input, SqlDbType.VarChar, 100, txtatasnama.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", Params, "constrmf");

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


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Update Done!','bunga dan jangka','top-right','success','1');", true);

                    pnlHeader.Visible = true;
                    PnlFront.Visible = true;

      
                    txtnobukti.Text = "";
                    pnladd.Visible = false;
                    Pnlcustomer.Visible = false;
                    Pnlfooter4.Visible = false;
                    BindGridEntity();
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
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
            //txttgljthtmp.Text = txttgltrn.Text;
        }

    }
    protected void UploadFileKTP(object sender, EventArgs e)
    {

        string namafileKTP;
        string fotopathktp;
        string folderPath = Server.MapPath("~/Files/MENINGGAL/");
        DateTime now = DateTime.Now;

        namafileKTP = HDNoRekening.Value + "_" + now.ToString("ddMMyyyy") + "_" + "DEAD";


        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //if(Path.GetFileNameWithoutExtension(fileuploadktp.FileName) != namafileKTP)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama File harus berformat nonasabah_tanggal hari ini tanpa strip_File yg di upload, contoh : " + namafileKTP + "');", true);

        //}
        //else
        //{
        //Save the File to the Directory (Folder).
        //fileuploadktp.SaveAs(folderPath + namafileKTP + Path.GetFileName(fileuploadktp.FileName));

        ////Display the Picture in Image control.
        //imagektp.ImageUrl = "~/Files/Kresun/KTP/" + Path.GetFileName(fileuploadktp.FileName);

        //fotopathktp = folderPath + Path.GetFileName(fileuploadktp.FileName);
        //txtpathktp.Text = "~/Files/MENINGGAL/" + namafileKTP + Path.GetFileName(fileuploadktp.FileName);
        //txtlink.Text = "View";
        //txtlink.NavigateUrl= txtpathktp.Text;
        //}


    }

    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {

        var kodelgn = hdncustomer.Value;
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SS_Permohonan", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
            //txtkodelgn.Text = kodelgn;
            //txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            //txttglsk.Text = ge.ListDataset.Tables[0].Rows[0]["tglsk"].ToString();

            //Txtnorekening.Text = HDNoRekening.Value;
            txtnonasabah.Text = kodelgn;
            txtnamanasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

          
           

            btnSimpan.Visible = true;



            ddlKoperasi.Enabled = false;
            ddlcabang.Enabled = false;
            ddljangka.Enabled = true;
            ddlbunga.Enabled = true;

            //RequiredFieldValidator1.Enabled = true;

     
            ////txtNmSup.Enabled = true;
            //txttglsk.Enabled = true;

            //txttgltrn.Enabled = true;
            //txtKeterangan.Enabled = true;
            //fileuploadktp.Enabled = true;
            //btnUploadktp.Enabled = true;

        }

        else
        {
            txtnonasabah.Text = "";
            txtnamanasabah.Text = "";


       
            ddlproduk.Enabled = false;
            ddlKoperasi.Enabled = false;
            ddlcabang.Enabled = false;
            ddljangka.Enabled = false;
            ddlbunga.Enabled = false;
            btnSimpan.Enabled = false;
            //RequiredFieldValidator1.Enabled = false;
         
           
 
         
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
    public static string GetCustomer(string InputText, string SessKdpershx,string SessKoperasiID)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@SessKoperasiID", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKoperasiID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SN_getCustomer_Permohonan", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    #region"Dimas"

    protected void UploadFileKTPwaris1(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/SimpananBerjangka/");
        DateTime now = DateTime.Now;
        string namafile = "", fotopath;

        namafile = txtnonasabah.Text + "_" + now.ToString("ddMMyyyy") + "_" + "AhliWaris1";
        //File cekfile1 = new FileInfo(folderPath + )
        //Check whether Directory (Folder) exists.
        //////FileInfo file = new FileInfo(folderPath + namafile + ".jpg");
        //////if (file.Exists)
        //////{
        //////    file.Delete();
        //////}
        //////if (!Directory.Exists(folderPath))
        //////{
        //////    //If Directory (Folder) does not exists. Create it.
        //////    Directory.CreateDirectory(folderPath);
        //////}

        //////fileuploadktpwaris1.SaveAs(folderPath + namafile + ".jpg");
        //////txtpathktpwaris1.Text = namafile + ".jpg";

        //fotopath = folderPath + namafile + ".jpg";
        //txtpathktp2.Text = fotopath;
        //Boolean FileOK = false;

        //Boolean FileSaved = false;
        //string tipefile = "";
        //string cekfile = "";
        //string pathfile = "";

        //if (fileuploadktpwaris1.HasFile)
        //{
        //    Session["WorkingImage"] = fileuploadktpwaris1.FileName;

        //    String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

        //    String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".png" };

        //    for (int i = 0; i < allowedExtensions.Length; i++)
        //    {
        //        if (FileExtension == allowedExtensions[i])
        //        {
        //            FileOK = true;
        //            tipefile = allowedExtensions[i].ToString();
        //            cekfile = "masuk";
        //        }
        //    }
        //}
        //if (FileOK)
        //{
        //    try
        //    {
        //        //Check whether Directory (Folder) exists.
        //        if (!Directory.Exists(folderPath))
        //        {
        //            //If Directory (Folder) does not exists. Create it.
        //            Directory.CreateDirectory(folderPath);
        //        }
        //        //fileuploadktpwaris1.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);

        //        FileInfo file = new FileInfo(folderPath + Session["WorkingImage"]);
        //        //string pathori = "~/Files/SimpananBerjangka/" + Session["WorkingImage"];
        //        //string pathori1 = folderPath + Session["WorkingImage"];
        //        if (file.Exists)
        //        {
        //            file.Delete();
        //            //var stream1 = new FileStream(pathori1, (FileMode)FileAccess.Read);
        //            //var reader1 = new StreamReader(stream1);
        //            //stream1.Close();
        //            //reader1.Close();
        //        }

        //        //var stream = new FileStream(pathori, (FileMode)FileAccess.Read);
        //        //var reader = new StreamReader(stream);
        //        //File.Delete(pathori); 

        //        fileuploadktpwaris1.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
        //        System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
        //        img = resizeImage(img);
        //        img.Save(string.Concat(folderPath, txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
        //        img.Dispose();
        //        //var stream = new FileStream(folderPath + Session["WorkingImage"], (FileMode)FileAccess.ReadWrite);
        //        //var reader = new StreamReader(stream);
        //        fileuploadktpwaris1.PostedFile.InputStream.Dispose();
        //        if (file.Exists)
        //        {
        //            file.Delete();
        //        }
        //        //imgCroppedwaris1.ImageUrl = folderPath + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
        //        pathfile = "~/Files/SimpananBerjangka/" + txtnonasabah.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
        //        imgCroppedwaris1.Dispose();
        //        img.Dispose();
        //        //StreamReader.Close();

        //        //FileInfo file = new FileInfo(folderPath + Session["WorkingImage"]);
        //        //if (file.Exists)
        //        //{
        //        //    File.Delete(folderPath + Session["WorkingImage"]);
        //        //}
        //        //string folderPathapus = Server.MapPath("~\\Files\\SimpananBerjangka\\" + Session["WorkingImage"]);
        //        //File.Delete(folderPathapus);
        //        //img.de
        //        //File.Delete(fileuploadktpwaris1.FileBytes);

        //        fileuploadktpwaris1.Dispose();
        //        //if (File.Exists(pathori1))
        //        //{
        //        //    File.Delete(pathori1);
        //        //}
        //        FileSaved = true;
        //    }
        //    catch (System.Exception X)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        //        FileSaved = false;
        //    }
        //}
        //else if (fileuploadktpwaris1.FileName == "" && cekfile == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih File Terlebih dahulu.');", true);
        //}
        //else if (fileuploadktpwaris1.FileName != "" && cekfile == "masuk")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis File harus image');", true);
        //}
        //if (FileSaved)
        //{
        //    txtpathktpwaris1.Text = pathfile;
        //    imgCroppedwaris1.ImageUrl = pathfile;
        //    //string path1 = Server.MapPath("~/Files/SimpananBerjangka/" + Session["WorkingImage"]);
        //    //FileAccess file1 = new FileAccess();
        //    ////file1.Delete();
        //    ////Bitmap.Dispose();
        //    ////pictureBox1.Image?.Dispose();

        //    //File.file1.
        //    //File.Delete(folderPath + Session["WorkingImage"].ToString());
        //    //fileuploadktpwaris1.(folderPath + Session["WorkingImage"]);
        //}

    }
    private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        //Get the image current width
        int sourceWidth = imgToResize.Width;
        //Get the image current height
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;
        //Calulate  width with new desired size
        nPercentW = ((float)size.Width / (float)sourceWidth);
        //Calculate height with new desired size
        nPercentH = ((float)size.Height / (float)sourceHeight);


        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;
        //New Width
        int destWidth = (int)(sourceWidth * nPercent);
        //New Height
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(60, 60);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw image with new width and height
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (System.Drawing.Image)b;
    }
    /// <summary>
    /// Function for resize image
    /// </summary>
    /// <param name="img">Image</param>
    /// <returns></returns>
    private System.Drawing.Image resizeImage(System.Drawing.Image img)
    {
        Bitmap b = new Bitmap(img);
        System.Drawing.Image i = resizeImage(b, new Size(60, 60));
        b.Dispose();
        return i;
    }

    #endregion"Dimas"
}
