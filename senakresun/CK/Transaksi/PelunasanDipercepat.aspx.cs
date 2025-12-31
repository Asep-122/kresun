using System;
using System.Collections.Generic;
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
public partial class CL_Transaksi_PelunasanDipercepat : System.Web.UI.Page
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
    string form = "R4";
    string kodesumber = "CL";
    string KodeKonter = "CL16";
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
        //Pnlentry.Visible = true;
        Pnlsaldo.Visible = true;
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

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_PelunasanDipercepat_AS", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";


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
                    GRTP.Enabled = false;


                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdsplr")).Text;
                    txtbpenalti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbpinalti")).Text;
                    ddlpenalti.Visible = false;
                    lblpnlty.Visible = false;
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;

                    //txtsaldotitipan.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblSaldoTitipan")).Text));
                    //txtsaldotitipan.Enabled = false;
                    Pnlsaldo.Visible = true;

                    txtkodecabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabangf")).Text;
                    txtkodecabang.Enabled = false;
                    btnSimpan.Visible = false;

                    statusclosing = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTutupBukuFlag")).Text;
                    statusposting = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHapusFlag")).Text;

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


                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTINVOICE", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 50, form, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_PelunasanDipercepat_AS", Paramsc, "constrmf").Tables[0];

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


                    pnltrn.Visible = true;
                    Pnlfooter4.Visible = true;



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
                    string nobukti, kodecabangf;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    kodecabangf = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabangf")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_PelunasanDipercepat_AS", Paramsc, "constrmf").Tables[0];

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

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_PelunasanDipercepat_AS", Paramsc, "constrmf").Tables[0];

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
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            //decimal jlhdibayar = decimal.Parse(txtbayar.Text);

            //if (jlhdibayar <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
            //}
            //else
            //{

                dtBayar.Columns.Clear();
                dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(DateTime)));
                dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
                dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));
                dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Angsuran", typeof(decimal)));
                dtBayar.Columns.Add(new DataColumn("Denda", typeof(decimal)));
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
                        decimal xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs")).Text);
                        decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblPokok")).Text);
                        decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblBunga")).Text);
                        decimal xangsuran = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text);
                        decimal xdenda = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lbldenda")).Text);
                        decimal xbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);

                        DateTime tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                        dr1["No"] = xi;
                        dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                        dr1["TglJthTmp"] = tgljthtempo;
                        dr1["Produk"] = ((Label)GRTP.Rows[i].FindControl("lblproduk")).Text;
                        dr1["Angske"] = xangske;
                        dr1["Pokok"] = xpokok;
                        dr1["Bunga"] = xbunga;
                        dr1["Angsuran"] = xangsuran;
                        dr1["Denda"] = xdenda;
                        dr1["Bayar"] = xbayar;
                        dr1["TypeBayar"] = ddlbayar.SelectedValue;
                        dtBayar.Rows.Add(dr1);
                    }
                }
                 

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[15];


                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                //Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
                Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));
                Params[3] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                Params[4] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[6] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[9] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[10] = gb.CreateSPParameter("@CL_AR_Kartu_Pinjaman", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
                Params[11] = gb.CreateSPParameter("@Kodecabangf", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodecabang.Text, typeof(string));
                //Params[13] = gb.CreateSPParameter("@SaldoTitipan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtsaldotitipan.Text, typeof(decimal));
                Params[12] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
                Params[13] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                Params[14] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjmlhangs.Text, typeof(decimal));

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_PelunasanDipercepat_AS_Delete_Bayar_Angsuran", Params, "constrmf");
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

                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }
            //}
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

    decimal pokok = 0, bunga = 0, angsuran = 0, denda = 0, bayar = 0, fee = 0, total = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
            bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bunga"));
            angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
            denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            fee += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "feechannelling"));



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


            decimal tottrans = 0;
            Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
            lbltotalPokok.Text = string.Format("{0:N2}", pokok);

            Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
            lbltotalBunga.Text = string.Format("{0:N2}", bunga);


            Label lbltotbay = (Label)e.Row.FindControl("lbltotbay");
            lbltotbay.Text = string.Format("{0:N2}", angsuran);


            Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
            lbltotalAngsuran.Text = string.Format("{0:N2}", angsuran);

            Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
            lbltotaldenda.Text = string.Format("{0:N2}", denda);

            Label lbltotalfeec = (Label)e.Row.FindControl("lbltotalfeec");
            lbltotalfeec.Text = string.Format("{0:N2}", fee);

            total = pokok + bunga + fee;
            HDBUnga.Value = lbltotalBunga.Text;
            HDPokok.Value = lbltotalPokok.Text;
            HDFee.Value = lbltotalfeec.Text;

            HDtotal.Value = total.ToString();
            txtpiutang.Text = gb.ToRupiah(lbltotalAngsuran.Text).Replace("Rp","");
            

            tottrans = decimal.Parse(txtpiutang.Text) + decimal.Parse(txtbpenalti.Text);
            txtjmlhangs.Text = gb.ToRupiah(tottrans.ToString()).Replace("Rp", "");
        }


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
        //    bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bunga"));
        //    angsuran += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
        //    denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));

        //    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 0)
        //    {


        //    }
        //    else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 1)
        //    {
        //        bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran"));
        //    }
        //    else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Tipebayar")) == 2)
        //    {
        //        bayar += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Angsuran")) + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
        //    }

        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{

        //    Label lbltotalPokok = (Label)e.Row.FindControl("lbltotalPokok");
        //    lbltotalPokok.Text = string.Format("{0:N2}", pokok);

        //    Label lbltotalBunga = (Label)e.Row.FindControl("lbltotalBunga");
        //    lbltotalBunga.Text = string.Format("{0:N2}", bunga);

        //    Label lbltotalAngsuran = (Label)e.Row.FindControl("lbltotalAngsuran");
        //    lbltotalAngsuran.Text = string.Format("{0:N2}", angsuran);

        //    Label lbltotaldenda = (Label)e.Row.FindControl("lbltotaldenda");
        //    lbltotaldenda.Text = string.Format("{0:N2}", denda);



        //}
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            //invoce yg dibayarkan
            //decimal jlhdibayar = decimal.Parse(txtbayar.Text);

            //if (jlhdibayar <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Bayar Masih Kosong!');", true);
            //}
            //else
            //{

            dtBayar.Columns.Clear();
            dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(DateTime)));
            dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
            dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));
            dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("Angsuran", typeof(decimal)));
            dtBayar.Columns.Add(new DataColumn("Denda", typeof(decimal)));
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
                    decimal xangske = int.Parse(((Label)GRTP.Rows[i].FindControl("lblangs")).Text);
                    decimal xpokok = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblPokok")).Text);
                    decimal xbunga = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblBunga")).Text);
                    decimal xangsuran = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lblAngsuran")).Text);
                    decimal xdenda = decimal.Parse(((Label)GRTP.Rows[i].FindControl("lbldenda")).Text);
                    decimal xbayar = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);

                    DateTime tgljthtempo = DateTime.Parse(((Label)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text);
                    dr1["No"] = xi;
                    dr1["NomorRekening"] = ((Label)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
                    dr1["TglJthTmp"] = tgljthtempo;
                    dr1["Produk"] = ((Label)GRTP.Rows[i].FindControl("lblproduk")).Text;
                    dr1["Angske"] = xangske;
                    dr1["Pokok"] = xpokok;
                    dr1["Bunga"] = xbunga;
                    dr1["Angsuran"] = xangsuran;
                    dr1["Denda"] = xdenda;
                    dr1["Bayar"] = xbayar;
                    dr1["TypeBayar"] = ddlbayar.SelectedValue;
                    dtBayar.Rows.Add(dr1);
                }
            }


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[17];


            Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
            Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtjmlhangs.Text, typeof(decimal));
            Params[3] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodelgn.Text, typeof(string));
            Params[4] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
            Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
            Params[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
            Params[11] = gb.CreateSPParameter("@CL_AR_Kartu_Pinjaman", ParameterDirection.Input, SqlDbType.Structured, 0, dtBayar, typeof(DataTable));
            Params[12] = gb.CreateSPParameter("@Kodecabangf", ParameterDirection.Input, SqlDbType.VarChar, 10, HDNKodeCabang.Value, typeof(string));
            Params[13] = gb.CreateSPParameter("@SaldoTitipan", ParameterDirection.Input, SqlDbType.Decimal, 0, txtpiutang.Text, typeof(decimal));
            Params[14] = gb.CreateSPParameter("@KodeBank", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            Params[15] = gb.CreateSPParameter("@kdpenalti", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlpenalti.SelectedValue, typeof(string));
            Params[16] = gb.CreateSPParameter("@biayapenalti", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbpenalti.Text, typeof(decimal)); 

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_PelunasanDipercepat_AS_Insert_Bayar_Pelunasan", Params, "constrmf");
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

                nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }
            //}
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
        //var kodecabang = HDNKodeCabang.Value;


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIASCLAIM", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNoRekening.Value, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_PelunasanDipercepat_AS", var, "constrmf");


        SetDetailBayar();

        if (ge.ListDataset.Tables[0].Rows.Count > 0)


        {
            txtkodelgn.Text = ge.ListDataset.Tables[0].Rows[0]["KodeAsuransi"].ToString();
            txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtpiutang.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["piutang"].ToString()));
            txtkodecabang.Text =  ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString(); 
            btnSimpan.Visible = true;


            var cust = hdncustomer.Value;
            //string norekening = HDNoRekening.Value;
            pnltrn.Visible = true;


            DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[6];
            var2[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, cust, typeof(string));
            var2[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
            var2[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            var2[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTRANSAKSIPINJAMAN", typeof(string));
            var2[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_PelunasanDipercepat_AS", var2, "constrmf");



            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {

                //ViewState["ListDetailBayar"] = ge.ListDataTable;

                ge.ListDataset.Tables[0].DefaultView.Sort = "angs_ke asc";
                GRTP.DataSource = ge.ListDataset.Tables[0];
                GRTP.DataBind();

                GRTP.UseAccessibleHeader = true;
                GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridBayar();
            }
            else
            {
                SetDetailBayar();
            }
        }

        else
        {
            txtkodelgn.Text = "";
            txtNmSup.Text = "";
            //txtsaldotitipan.Text = "0";
            txtkodecabang.Text = "";

            btnSimpan.Enabled = false;
        }



        //string customer = hdncustomer.Value;
        //string norekening = HDNoRekening.Value;
        //pnltrn.Visible = true;

        //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
        //var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, customer, typeof(string));
        //var1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
        //var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNKodeCabang.Value, typeof(String));

        //var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTRANSAKSIPINJAMAN", typeof(string));
        //var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        //ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_PelunasanDipercepat_AS", var1, "constrmf").Tables[0];

        //if (ge.ListDataTable.Rows.Count > 0)
        //{

        //    ViewState["ListDetailBayar"] = ge.ListDataTable;

        //    GRTP.DataSource = ge.ListDataTable;
        //    GRTP.DataBind();

        //    GRTP.UseAccessibleHeader = true;
        //    GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    cssGridBayar();
        //}
        //else
        //{
        //    SetDetailBayar();
        //}


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



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
        //cells[6].Attributes.Add("data-class", "expand");
        //cells[7].Attributes.Add("data-class", "expand");
        //cells[8].Attributes.Add("data-class", "expand");
        //cells[9].Attributes.Add("data-class", "expand");

    }

    private void SetDetailBayar()
    {

        //DataRow dr = null;     
        
        dtBayar.Columns.Clear();
        dtBayar.Columns.Add(new DataColumn("No", typeof(int)));
        dtBayar.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("TglJthTmp", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Produk", typeof(string)));
        dtBayar.Columns.Add(new DataColumn("Angske", typeof(int)));

        dtBayar.Columns.Add(new DataColumn("Pokok", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Bunga", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Angsuran", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Denda", typeof(decimal)));
        dtBayar.Columns.Add(new DataColumn("Bayar", typeof(string)));


        ViewState["ListDetailBayar"] = dtBayar;
        GRTP.DataSource = dtBayar;
        GRTP.DataBind();
        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
    }

    protected void ddlbayar_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal a, b, c = 0;

        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;



        if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "0")
        {
            ((TextBox)grow.FindControl("lblbayar")).Text = "0";
            ((TextBox)grow.FindControl("lblbayar")).Enabled = false;

            a = decimal.Parse(txtjmlhangs.Text);
            b = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            c = a - b;
            txtjmlhangs.Text = gb.ToRupiah(c.ToString()).Replace("Rp", "");

        }
        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "1")
        {
            //decimal angsuran = decimal.Parse(txtsaldotitipan.Text) - decimal.Parse(txtbayar.Text);
            //decimal declbljmlhtrn = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            //if (angsuran < declbljmlhtrn)
            //{
            //    ((DropDownList)grow.FindControl("ddlbayar")).SelectedValue = "0";
            //    ((TextBox)grow.FindControl("lblbayar")).Text = "0";
            //    ((TextBox)grow.FindControl("lblbayar")).Enabled = false;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Saldo Titipan kurang dari Angsuran!');", true);

            //}
            //else
            //{
            //    ((TextBox)grow.FindControl("lblbayar")).Text = ((Label)grow.FindControl("lblAngsuran")).Text;
            //    ((TextBox)grow.FindControl("lblbayar")).Enabled = false;
            //}
            //decimal angsuran = decimal.Parse(txtsaldotitipan.Text) - decimal.Parse(txtbayar.Text);
            decimal declbljmlhtrn = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            string jmltrn = ((Label)grow.FindControl("lblAngsuran")).Text.Replace("-", "");
            //if (angsuran < declbljmlhtrn)
            //{
            //    ((DropDownList)grow.FindControl("ddlbayar")).SelectedValue = "0";
            //    ((TextBox)grow.FindControl("lblbayar")).Text = "0";
            //    ((TextBox)grow.FindControl("lblbayar")).Enabled = false;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Saldo Titipan kurang dari Angsuran!');", true);

            //}
            //else
            //{

            declbljmlhtrn = decimal.Parse(jmltrn);
            ((TextBox)grow.FindControl("lblbayar")).Text = string.Format("{0:N2}", jmltrn);
            ((TextBox)grow.FindControl("lblbayar")).Enabled = false;
             

            a = decimal.Parse(txtjmlhangs.Text);
            b = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            c = a + b;
            txtjmlhangs.Text = gb.ToRupiah(c.ToString()).Replace("Rp","");
        }

        else if (((DropDownList)grow.FindControl("ddlbayar")).SelectedValue == "2")
        {
            decimal angsuran = 0;
            decimal denda = 0;
            decimal total = 0;

            angsuran = decimal.Parse(((Label)grow.FindControl("lblAngsuran")).Text);
            denda = decimal.Parse(((Label)grow.FindControl("lbldenda")).Text);
            total = angsuran + denda;
            ((TextBox)grow.FindControl("lblbayar")).Text = string.Format("{0:N2}", total);

        } 

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridbayar_Click();

         
    }
    public void subgridbayar_Click()
    { 
        int dgcount = GRTP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        
                
        if (dgcount > 0)
        {


            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((TextBox)GRTP.Rows[i].FindControl("lblbayar")).Text);
                sumjlhgl = sumjlhgl + jlhgl;

            
            }

            

        } 
        else
        {
            sumjlhgl = 0;
        } 
        
        //string formatted = string.Format("{0:N2}", sumjlhgl);
        //txtjmlhangs.Text = formatted;


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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_getCustomer_Pelunasan_Dipercepat", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    } 

    protected void ddlPenalty_SelectedIndexChanged(object sender,EventArgs e)
    {
        decimal angsperbulan = 0, hasil = 0, jmltransaksi = 0;
        string[] split = txtcustomer.Text.Split(':');
        string noRekening = split[3];


        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
        var3[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, noRekening.Replace(" ", ""), typeof(string));
        var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTANGS", typeof(string));
        var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_PelunasanDipercepat_AS", var3, "constrmf");


        angsperbulan = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuran_perbulan"].ToString());

        decimal val = decimal.Parse(ddlpenalti.SelectedValue);

        hasil = angsperbulan * val;
        txtbpenalti.Text = gb.ToRupiah(hasil.ToString()).Replace("Rp", "");

        jmltransaksi = decimal.Parse(txtpiutang.Text) + hasil;



        txtjmlhangs.Text = gb.ToRupiah(jmltransaksi.ToString()).Replace("Rp", "");
        //if(ddlpenalti.SelectedValue == "0" || ddlpenalti.SelectedValue =="1")
        //{ 
        //decimal angs = decimal.Parse(txtpiutang.Text);
        //decimal val = decimal.Parse(ddlpenalti.SelectedValue);
        //decimal result = 0;
        //result = angs * val;

        //string formatted = string.Format("{0:N2}", result);
        //txtbpenalti.Text = formatted;
        //}
        //else
        //{
        //    decimal angs = decimal.Parse(txtjmlhangs.Text);
        //    decimal val = decimal.Parse(ddlpenalti.SelectedValue);
        //    decimal result = 0;
        //    result = angs * 0;

        //    string formatted = string.Format("{0:N2}", result);
        //    txtbpenalti.Text = formatted;
        //}
        //if (ddlpenalti.SelectedValue == "0")
        //{
        //    int angs = Int32.Parse(txtjmlhangs.Text);
        //    int val = Int32.Parse(ddlpenalti.SelectedValue);
        //    int result = 0; 
        //    result = angs * val;

        //    string formatted = string.Format("{0:N2}", result);
        //    txtbpenalti.Text = formatted;
        //}
        //else if (ddlpenalti.SelectedValue == "1")
        //{ 
        //    int angs = Int32.Parse(txtjmlhangs.Text);
        //    int val = Int32.Parse(ddlpenalti.SelectedValue);
        //    int result = 0;
        //    result = angs * val;

        //    string formatted = string.Format("{0:N2}", result);
        //    txtbpenalti.Text = formatted;
        //}
    }
}
