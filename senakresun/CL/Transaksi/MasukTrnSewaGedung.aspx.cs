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
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class CL_Process_MasukTrnSewaGedung : System.Web.UI.Page
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
    string form = "K1";
    string kodesumber = "CL";
    string KodeKonter = "CL07";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;     
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnlFilter.Visible = false;

        btndelete.Visible = false;

        pnldatapencairan.Visible = true;
        BindGridEntityDataCair();
        //btnSimpan.Visible = true;
        //txtnobukti.Text = "";
        //pnladd.Visible = true;
        //Pnlcustomer.Visible = true;
        //Pnlfooter4.Visible = true;
        //pnlbunga.Visible = true;
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
                //BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //KODEBANK
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMSTXY", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_BAYAR_BUNGA", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKdBank.DataTextField = "namabank";
                ddlKdBank.DataValueField = "KodeBank";
                ddlKdBank.DataBind();
                ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));


                //ddlKdBank.SelectedValue = "110191";


                txtNmBank.Text = "Kas Head Teller";

                txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
                Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTeller", Paramx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
                ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));

                string tempuser = (SessBranchID == "000" ? "000TL1" : SessLoginID);
                ddlteller.SelectedValue = tempuser;
                if (tempuser.Substring(3, 3) == "TL1")
                {
                    ddlteller.Enabled = true;
                }
                else
                {
                    ddlteller.Enabled = false;
                }
                BindGridEntity();

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

    protected void lnkCari_Click(object sender, EventArgs e)
    {

        if (txtSearch.Text == "")
        {
            BindGridEntity();
        }
        else
        {
            BindGridEntityx();
        }


    }

    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRXBAYARSEWAGEDUNG", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_GetSEARCH", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Sewa_Gedung", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void ddlteller_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrnx.Text, typeof(DateTime));
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
            txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
            BindGridEntity();
        }
        else
        {
            BindGridEntity();
        }
    }

    decimal total = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", total);

        }
    }
    public void BindGridEntityDataCair()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATACAIR", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Sewa_Gedung", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_Sewa asc";


        GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;

        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityDatamutasi()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kode_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoNasabah.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIMUTASI", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Sewa_Gedung", var, "constrmf");

      

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeSewa asc";


        Grmutasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grmutasi.DataBind();

        Grmutasi.UseAccessibleHeader = true;

        Grmutasi.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        pnlFilter.Visible = false;
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
                   
                    btnSimpan.Visible = false;
        
                    btndelete.Visible = true;
                    //statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    //statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;


                    //path
                    txtpathbuktitrn.Text = ((HiddenField)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblwaris1")).Value;





                    //if (statusclosing == "True")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                    //    btndelete.Enabled = false;
                    //}
                    //else
                    //{
                    //    if (statusposting == "True")
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diposting hubungi pusat!');", true);
                    //        btndelete.Enabled = false;
                    //    }

                    //}
                    

                    txtkode_Sewa.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoFaktur")).Text; 

                    BindGridEntityDatamutasi();
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Sewa_Gedung", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)


                    {

                        txtkode_Sewa.Text = ge.ListDataset.Tables[0].Rows[0]["kode_Sewa"].ToString();
                        TxtNama_Sewa.Text = ge.ListDataset.Tables[0].Rows[0]["Nama_Sewa"].ToString();
                        TxtbiayaSewa.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["hargasewa"].ToString()));
                        txtpajak.Text = string.Format("{0:N2}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString()));
                        txtNilaiPajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["NilaiPajak"].ToString()));
                        //txtperiodeawal.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["awalbulan"].ToString()).ToString("dd-MM-yyyy");
                        //txtperiodeakhir.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["akhirbulan"].ToString()).ToString("dd-MM-yyyy");
                        txtbulan.Text = ge.ListDataset.Tables[0].Rows[0]["bulan"].ToString();

                        txttotalygharusdibayar.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JumlahTrn"].ToString()));
                        txtnilaipajakh.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JumlahPajak"].ToString()));

                        //DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];
                        //varx[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        //varx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        //varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                        //varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        //ge.ListDataset = gb.GetDataJustOutPut("SN_Trn_BAYAR_BUNGA", varx, "constrmf");

                        //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                        //ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                        //ddlKdBank.DataTextField = "namabank";
                        //ddlKdBank.DataValueField = "KodeBank";
                        //ddlKdBank.DataBind();
                        //ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));
                        ddlKdBank.SelectedValue= ge.ListDataset.Tables[0].Rows[0]["KodeBank"].ToString();
                       txtNmBank.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                        ddlkursbank.SelectedValue = "IDR";
                        txtkursbank.Text="1";

                        txtbulan.Enabled = false;

                    }

                    else
                    {
                        txtkode_Sewa.Text = "";
                        TxtNama_Sewa.Text = "";
                        TxtbiayaSewa.Text = "0";
                        txtpajak.Text = "0";
                        txtNilaiPajak.Text = "0";
                        //txtperiodeawal.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["awalbulan"].ToString()).ToString("dd-MM-yyyy");
                        //txtperiodeakhir.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["akhirbulan"].ToString()).ToString("dd-MM-yyyy");
                        txtbulan.Text = "0";

                        txttotalygharusdibayar.Text = "0";
                        txtnilaipajakh.Text = "0";




                   


                    }


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


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);




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
                    ge.ListDataTable = gb.GetDataJustOutPut("SN_PENCAIRAN_BERJANGKA", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        //Response.Buffer = false;
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

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    
                    Paramsc[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Sewa_Gedung", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAP.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                      
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                      

                        //Response.Buffer = false;
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
        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txttotalygharusdibayar.Text);
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Tarikan Masih Kosong');", true);

            }
            else
            {


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[20];
                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                Params[3] = gb.CreateSPParameter("@NamaBank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmBank.Text, typeof(string));
                Params[4] = gb.CreateSPParameter("@kode_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkode_Sewa.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@Nama_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 100, TxtNama_Sewa.Text, typeof(string));
                //Params[6] = gb.CreateSPParameter("@Biaya_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 100, TxtNama_Sewa.Text, typeof(string));
                Params[6] = gb.CreateSPParameter("@Biaya_Sewa", ParameterDirection.Input, SqlDbType.Money, 0, TxtbiayaSewa.Text, typeof(decimal));
                Params[7] = gb.CreateSPParameter("@Pajak", ParameterDirection.Input, SqlDbType.Float, 0, txtpajak.Text, typeof(float));
                Params[8] = gb.CreateSPParameter("@NilaiPajak", ParameterDirection.Input, SqlDbType.Money, 0, txtNilaiPajak.Text, typeof(decimal));
                Params[9] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtbulan.Text), typeof(int));
                Params[10] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, txttotalygharusdibayar.Text, typeof(decimal));
                Params[11] = gb.CreateSPParameter("@JumlahPajak", ParameterDirection.Input, SqlDbType.Money, 0, txtnilaipajakh.Text, typeof(decimal));
                Params[12] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[13] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[14] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[15] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[17] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[18] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "DELETE", typeof(string));
                Params[19] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Sewa_Gedung", Params, "constrmf");
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
                    if (txtpathbuktitrn.Text != "")
                    {
                        string path1 = Server.MapPath(txtpathbuktitrn.Text);
                        FileInfo file1 = new FileInfo(path1);
                        if (file1.Exists)
                        {
                            file1.Delete();
                        }
                    }
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
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "redirect",
            "window.location.href='MasukTrnsSewaGedung.aspx';",
            true
        );

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        switch (e.CommandName)
        {

            case "SET":

                try
                {

                   var kodesewa= ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkode_Sewa")).Text;
                    HDNoNasabah.Value = ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkode_Sewa")).Text;
                    pnldatapencairan.Visible = false;

                    btnSimpan.Visible = true;
                    txtnobukti.Text = "";
                    pnladd.Visible = true;
                   
                    Pnlfooter4.Visible = true;
                    PnlMutasi.Visible = true;
                   
                   //var kodelgn = HDNoNasabah.Value;
                   // hdncustomer.Value = kodelgn;
                   // string norekening = HDNoRekening.Value;
                    BindGridEntityDatamutasi();
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@kode_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 30, kodesewa, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIPERMOHONAN", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Sewa_Gedung", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)


                    {

                        txtkode_Sewa.Text = ge.ListDataset.Tables[0].Rows[0]["kode_Sewa"].ToString();
                        TxtNama_Sewa.Text = ge.ListDataset.Tables[0].Rows[0]["Nama_Sewa"].ToString();
                        TxtbiayaSewa.Text= string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Biaya_Sewa"].ToString()));
                        txtpajak.Text = string.Format("{0:N2}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString()));
                        txtNilaiPajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["NilaiPajak"].ToString()));
                        //txtperiodeawal.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["awalbulan"].ToString()).ToString("dd-MM-yyyy");
                        //txtperiodeakhir.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["akhirbulan"].ToString()).ToString("dd-MM-yyyy");
                        txtbulan.Text = ge.ListDataset.Tables[0].Rows[0]["bulan"].ToString();

                        txttotalygharusdibayar.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Biaya_Sewa"].ToString()));
                        txtnilaipajakh.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["NilaiPajak"].ToString()));
                  
                        btnSimpan.Visible = true;

                     

                    }

                    else
                    {
                        txtkode_Sewa.Text = "";
                        TxtNama_Sewa.Text = "";
                        TxtbiayaSewa.Text = "0";
                        txtpajak.Text = "0";
                        txtNilaiPajak.Text = "0";
                        //txtperiodeawal.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["awalbulan"].ToString()).ToString("dd-MM-yyyy");
                        //txtperiodeakhir.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["akhirbulan"].ToString()).ToString("dd-MM-yyyy");
                        txtbulan.Text = "0";

                        txttotalygharusdibayar.Text = "0";
                        txtnilaipajakh.Text = "0";




                        btnSimpan.Enabled = false;

                     
                    }





                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


                }

                catch (Exception X)
                {
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                  
                }

                break;
        }
    }

    decimal debet = 0, kredit = 0;
    

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txttotalygharusdibayar.Text);
            btnSimpan.Enabled = false;

            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {
                if (jlhdibayar <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Tarikan Masih Kosong');", true);
                    btnSimpan.Enabled = true;
                }
                if (ddlKdBank.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank Masih Kosong');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {
                    if (fileuploadbuktitrn.HasFile)
                    {
                        string namafile = "";
                        string fotopath = "";
                        string ambilnama = "";
                        string folderPath = Server.MapPath("~/Files/bayarhutangsewa/");

                        DateTime now = DateTime.Now;

                        namafile = txtkode_Sewa.Text + "_" + now.ToString("ddMMyyyyhhmmss");
                        ambilnama = fileuploadbuktitrn.FileName;
                        String FileExtension = Path.GetExtension(ambilnama.ToString()).ToLower();

                        //Check whether Directory (Folder) exists.
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }

                        fileuploadbuktitrn.SaveAs(folderPath + namafile + FileExtension);
                        txtpathbuktitrn.Text = "~/Files/bayarhutangsewa/" + namafile + FileExtension;

                        fotopath = folderPath + namafile + FileExtension;
                    }
                    else
                    {
                        txtpathbuktitrn.Text = "";
                    }
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[21];
                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKdBank.SelectedValue, typeof(string));
                    Params[3] = gb.CreateSPParameter("@NamaBank", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmBank.Text, typeof(string));
                    Params[4] = gb.CreateSPParameter("@kode_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkode_Sewa.Text, typeof(string));
                    Params[5] = gb.CreateSPParameter("@Nama_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 100, TxtNama_Sewa.Text, typeof(string));
                    //Params[6] = gb.CreateSPParameter("@Biaya_Sewa", ParameterDirection.Input, SqlDbType.VarChar, 100, TxtNama_Sewa.Text, typeof(string));
                    Params[6] = gb.CreateSPParameter("@Biaya_Sewa", ParameterDirection.Input, SqlDbType.Money, 0, TxtbiayaSewa.Text, typeof(decimal));
                    Params[7] = gb.CreateSPParameter("@Pajak", ParameterDirection.Input, SqlDbType.Float, 0, txtpajak.Text, typeof(float));
                    Params[8] = gb.CreateSPParameter("@NilaiPajak", ParameterDirection.Input, SqlDbType.Money, 0, txtNilaiPajak.Text, typeof(decimal));
                    Params[9] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtbulan.Text), typeof(int));
                    Params[10] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, txttotalygharusdibayar.Text, typeof(decimal));
                    Params[11] = gb.CreateSPParameter("@JumlahPajak", ParameterDirection.Input, SqlDbType.Money, 0, txtnilaipajakh.Text, typeof(decimal));
                    Params[12] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[13] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[14] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[15] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[17] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[18] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, "INSERT", typeof(string));
                    Params[19] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Params[20] = gb.CreateSPParameter("@pathbuktitrn", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpathbuktitrn.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_Sewa_Gedung", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                        btnSimpan.Enabled = true;
                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);
                        btnSimpan.Enabled = true;
                    }

                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully save this Simulasi','top-right','success','true');", true);

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        btnSimpan.Enabled = true;
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
        }
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

        ge.ListDataset = gb.GetDataJustOutPut("SN_PENCAIRAN_BERJANGKA", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
            //txtkodelgn.Text = kodelgn;
            //txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();


            ////HDNoNasabah.Value = no_nasabah;
            //txttglpermohonan.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglpermohonan"].ToString()).ToString("dd-MM-yyyy");

            //txtnopermohonan.Text = ge.ListDataset.Tables[0].Rows[0]["Idpermohonan"].ToString();





            //txtproduk.Text = ge.ListDataset.Tables[0].Rows[0]["nama_produk"].ToString();

            //txtpajak.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Pajak"].ToString()));
            //txtbungapertahun.Text = string.Format("{0:N2}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungapertahun"].ToString()));

            //txtbungaperhari.Text = string.Format("{0:N6}", float.Parse(ge.ListDataset.Tables[0].Rows[0]["bungaperhari"].ToString()));

            ////txtnilaisaldo.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaisaldo"].ToString()));

            ////txtnilaibungapertahun.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungapertahun"].ToString()));
            ////txtnilaibungaperhari.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["nilaibungaperhari"].ToString()));


            //ddlInstruktur.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["InstrukturPromissory"].ToString();
            //ddlpenarikanbunga.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["PenarikanBunga"].ToString();


            //txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangkabulan"].ToString();

            btnSimpan.Visible = true;

           


            //txtsaldoawalpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoawal"].ToString()));
            //txtsetoranpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["setoran"].ToString()));
            //txttarikanpokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tarikan"].ToString()));
            //txtsaldopokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldopokok"].ToString()));
            //txtbungatransfer.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungatransfer"].ToString()));
            //txtbungarollon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungarollon"].ToString()));
            //txtbungakeluar.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bungakeluar"].ToString()));
            //txtsaldobunga.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobunga"].ToString()));
            //txtsaldojalan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldojalan"].ToString()));

            
        }

        else
        {
            //txtkodelgn.Text ="";
            //txtNmSup.Text = "";


            ////HDNoNasabah.Value = no_nasabah;
            //txttglpermohonan.Text = "";

            //txtnopermohonan.Text = "";





            //txtproduk.Text = "";

            //txtpajak.Text = "";
            //txtbungapertahun.Text = "";

            //txtbungaperhari.Text = "";

            ////txtnilaisaldo.Text = "";

            ////txtnilaibungapertahun.Text = "";
            ////txtnilaibungaperhari.Text = "";


            //ddlInstruktur.SelectedValue = "";
            //ddlpenarikanbunga.SelectedValue = "";

            //txtbankrekening.Text = "";
            //txtnorekening.Text = "";
            //txtatasnama.Text = "";
            //txtjangka.Text = "";

          

            btnSimpan.Enabled = false;

           
        }


     
        

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
       
        decimal bayar, jumlahPajak;
        bayar = decimal.Parse(txtbulan.Text) * decimal.Parse(TxtbiayaSewa.Text);
        jumlahPajak = (decimal.Parse(txtpajak.Text)/100) * bayar;
        if (decimal.Parse(txtbulan.Text) > 0)
        {

           txttotalygharusdibayar.Text = string.Format("{0:N2}", bayar);
            txtnilaipajakh.Text = string.Format("{0:N2}", jumlahPajak);

        }
        else
        {

            txttotalygharusdibayar.Text = string.Format("{0:N2}", 0);
            txtnilaipajakh.Text = string.Format("{0:N2}", 0);
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
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SN_Trn_BAYAR_BUNGA", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();

            //txtkodejournal.Text=ge.ListDataset.Tables[0].Rows[0]["kodejurnal"].ToString();

            txtNmBank.Text = NamaBank;
            ddlkursbank.SelectedValue = kdcrc;
            txtkursbank.Text = "1";

        }
        else
        {
            //txtkodejournal.Text = "";
            txtNmBank.Text = "";
            ddlkursbank.ClearSelection();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";


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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SN_getCustomer_tarikan_berjangka", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }


    #region"Dimas"
    protected void UploadFiletrn(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/Files/penutupanberjangka/");
        DateTime now = DateTime.Now;
        string namafileKTP = "", fotopathktp;

        //namafileKTP = "tes" + "_" + now.ToString("ddMMyyyy") + "_" + "KTP";

        Boolean FileOK = false;

        Boolean FileSaved = false;
        string tipefile = "";
        string cekfile = "";
        string pathfile = "";

        if (fileuploadbuktitrn.HasFile)
        {
            Session["WorkingImage"] = fileuploadbuktitrn.FileName;

            String FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".png" };

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;
                    tipefile = allowedExtensions[i].ToString();
                    cekfile = "masuk";
                }
            }
        }
        if (FileOK)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                fileuploadbuktitrn.PostedFile.SaveAs(folderPath + Session["WorkingImage"]);
                System.Drawing.Image img = System.Drawing.Image.FromFile(string.Concat(folderPath, Session["WorkingImage"]));
                img = resizeImage(img);
                img.Save(string.Concat(folderPath, txtnobukti.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"]));
                imgCroppedwaris1.ImageUrl = folderPath + txtnobukti.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                pathfile = "~/Files/penutupanberjangka/" + txtnobukti.Text + DateTime.Now.ToString("ddMMyyyyhhmmss") + Session["WorkingImage"];
                FileSaved = true;
            }
            catch (System.Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                FileSaved = false;
            }
        }
        else if (fileuploadbuktitrn.FileName == "" && cekfile == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih File Terlebih dahulu.');", true);
        }
        else if (fileuploadbuktitrn.FileName != "" && cekfile == "masuk")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jenis File harus image');", true);
        }
        if (FileSaved)
        {
            txtpathbuktitrn.Text = pathfile;
            imgCroppedwaris1.ImageUrl = pathfile;
        }

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
        return i;
    }
    protected void clearFileInputField1(object sender, EventArgs e)
    {
        try
        {
            txtpathbuktitrn.Text = "";
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion"Dimas"

}
