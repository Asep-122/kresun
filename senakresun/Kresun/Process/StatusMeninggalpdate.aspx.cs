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
using iTextSharp.text.pdf;

public partial class Kresun_Process_StatusMeninggalpdate : System.Web.UI.Page
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
    string form = "B3";
    string kodesumber = "KP";
    string KodeKonter = "KP09";
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
        try
        {

        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "", typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_MENINGGAL", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn desc";


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
                   
                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    Txtnorekening.Text = HDNoRekening.Value;
                    //txtnamask.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaSK")).Text;
                    //txtnosk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoSK")).Text;
                    txttglsk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglSK")).Text;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
//                    txtpathktp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;
                    txtlink.Text = "View";
//                    txtlink.NavigateUrl = txtpathktp.Text + "?r=" + DateTime.Now.Ticks.ToString();


                    string folderPath = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;

                    string encrypted = gb.Encrypt(folderPath + ".pdf");

                    string secureUrl = ResolveUrl("~/SecurePdf.ashx?f=" + encrypted);
                    txtpathktp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;
                    txtlink.NavigateUrl = secureUrl;

                    txtlink.Attributes["onclick"] =
                        "window.open(this.href, 'pdfWindow', 'toolbar=yes,scrollbars=yes,resizable=yes,width=900,height=850'); return false;";
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btnSimpan.Visible = true;
                    
                    txttglsk.Enabled = true;
                    //txtnamask.Enabled = true;
                    //txtnosk.Enabled = true;
                    txttgltrn.Enabled = true;
                    txtKeterangan.Enabled = true;
                    fileuploadktp.Enabled = true;
                    btnUploadktp.Enabled = true;

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

                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;

                    Pnlfooter4.Visible = true;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;

                    txtkodelgn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    txtkodelgn.Enabled = false;
                    txtNmSup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmLgn")).Text;
                    txtNmSup.Enabled = false;
                    //txtkodelgn.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    HDNoRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfaktur")).Text;
                    hdncustomer.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodelgn")).Text;
                    Txtnorekening.Text = HDNoRekening.Value;
                    //txtnamask.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaSK")).Text;
                    //txtnosk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoSK")).Text;
                    txttglsk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglSK")).Text;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtlink.Text = "View";
                    string folderPath = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;

                    string encrypted = gb.Encrypt(folderPath + ".pdf");

                    string secureUrl = ResolveUrl("~/SecurePdf.ashx?f=" + encrypted);
                    txtpathktp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPathFile")).Text;
                    txtlink.NavigateUrl = secureUrl;

                    txtlink.Attributes["onclick"] =
                        "window.open(this.href, 'pdfWindow', 'toolbar=yes,scrollbars=yes,resizable=yes,width=900,height=850'); return false;";
                    //txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    //txtbayar.Enabled = false;
                    btnSimpan.Visible =false;
                    btndelete.Visible = true;

                   




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
            string txtpath = txtpathktp.Text;
            string State = "";

            if (txtpathktp.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('File Upload Masih Kosong!');", true);
            }
            else
            {



               
                State = "DELETE";

                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[5];

                Paramx[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                Paramx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 4, SessBranchID, typeof(string));
                Paramx[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));
                Paramx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "CARIDATA", typeof(string));
                Paramx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_MENINGGAL", Paramx, "constrmf");


                if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tidak boleh di hapus karena sudah melakukan transaksi');", true);

                }
                else
                {
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[18];



                    Params[0] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[1] = gb.CreateSPParameter("@kodekonter", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string));
                    Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                    Params[3] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    Params[4] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[5] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[6] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[9] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
                    Params[11] = gb.CreateSPParameter("@NoSK", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                    Params[12] = gb.CreateSPParameter("@NamaSK", ParameterDirection.Input, SqlDbType.VarChar, 100, "", typeof(string));
                    Params[13] = gb.CreateSPParameter("@PathFile", ParameterDirection.Input, SqlDbType.VarChar, 150, txtpathktp.Text, typeof(string));
                    Params[14] = gb.CreateSPParameter("@TglSK", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglsk.Text, typeof(DateTime));
                    Params[15] = gb.CreateSPParameter("@namalgn", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmSup.Text, typeof(string));
                    Params[16] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                    Params[17] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_MENINGGAL", Params, "constrmf");
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Delete Done!','Data Berhasil Terhapus','bottom-center','success','true');", true);

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
            string txtpath = txtpathktp.Text;
            string State = "";

            if (txtpathktp.Text =="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('File Upload Masih Kosong!');", true);
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

                var norek = HDNoRekening.Value;



                DefaultClass.Global.SqlHelper[] bungajangka = new DefaultClass.Global.SqlHelper[4];
                bungajangka[0] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norek, typeof(string));
                bungajangka[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBUNGA", typeof(string));
                bungajangka[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                bungajangka[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AS_Trn_Claim", bungajangka, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alertfailed('Eror','Asuransi Tidak Ada');", true);
                    return;
                }
                decimal bunga = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bunga"].ToString());
                var jangka = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString();
                var palfon = ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString();


                double plafon = double.Parse(palfon.Replace(".", ""));
                double angsuranperbulanefektif = PMT(double.Parse(bunga.ToString()), int.Parse(jangka.ToString()), plafon);
                HdnAngka.Value =  angsuranperbulanefektif.ToString();
                HdnAngka.Value = Math.Abs(decimal.Parse(HdnAngka.Value)).ToString();


                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[22];


                Params[0] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[1] = gb.CreateSPParameter("@kodekonter", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string));
                    Params[2] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
                    Params[3] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
                    Params[4] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[5] = gb.CreateSPParameter("@addBy", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[6] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));               
                    Params[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[9] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 10, State, typeof(string));
                    Params[11] = gb.CreateSPParameter("@NoSK", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                    Params[12] = gb.CreateSPParameter("@NamaSK", ParameterDirection.Input, SqlDbType.VarChar, 100, "", typeof(string));
                    Params[13] = gb.CreateSPParameter("@PathFile", ParameterDirection.Input, SqlDbType.VarChar, 200, txtpathktp.Text, typeof(string));
                    Params[14] = gb.CreateSPParameter("@TglSK", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglsk.Text, typeof(DateTime));
                    Params[15] = gb.CreateSPParameter("@namalgn", ParameterDirection.Input, SqlDbType.VarChar, 100, txtNmSup.Text, typeof(string));
                    Params[16] = gb.CreateSPParameter("@keterangan", ParameterDirection.Input, SqlDbType.VarChar, 150, txtKeterangan.Text, typeof(string));
                    Params[17] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                    Params[18] = gb.CreateSPParameter("@AngsuranPerbulanEfektif", ParameterDirection.Input, SqlDbType.Money, 250, angsuranperbulanefektif, typeof(decimal));
                Params[19] = gb.CreateSPParameter("@bungaEfektif", ParameterDirection.Input, SqlDbType.Float, 250, bunga, typeof(decimal));
                Params[20] = gb.CreateSPParameter("@jangka", ParameterDirection.Input, SqlDbType.VarChar, 250, jangka, typeof(string));
                Params[21] = gb.CreateSPParameter("@plafon", ParameterDirection.Input, SqlDbType.Money, 250, plafon, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_MENINGGAL", Params, "constrmf");
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

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','Update Status Meninggal','bottom-center','success','true');", true);

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
    protected void UploadFileKTPjpg(object sender, EventArgs e)
    {
        try
        {
            if (fileuploadktp.HasFile)
            {


                var maxFileSize = 2194304; // 4MB -> 4 * 1024 * 1024

                if (fileuploadktp.PostedFile.ContentLength > maxFileSize)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Size File tidak boleh lebih dari 2 MB');", true);

                }

                else
                {

                    string namafile;
                    string filename;
                    string filenamex;
                    string fotopathktp;
                    DateTime now = DateTime.Now;
                    namafile = now.ToString("ddMMyyyyhhmmss") + "_" + "RIP";
                    string folderPath = Server.MapPath("~/Files/MENINGGAL/thumb");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }


                    filename = namafile + ".jpg";
                    filenamex = namafile + "OK.jpg";


                    //create temp
                    string path = Server.MapPath("~/Files/MENINGGAL/thumb/" + namafile + ".jpg");
                    fileuploadktp.SaveAs(path);
                    //create temp

                    System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                    float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                    int newHeight = 700;
                    int newWidth = Convert.ToInt32(aspectRatio * newHeight);
                    System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
                    System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);
                    thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                    thumbGraph.DrawImage(image, imageRectangle);
                    thumbBitmap.Save(Server.MapPath("~/Files/MENINGGAL/" + filenamex), System.Drawing.Imaging.ImageFormat.Jpeg);
                    thumbGraph.Dispose();
                    thumbBitmap.Dispose();
                    image.Dispose();
                    fotopathktp = "~/Files/MENINGGAL/" + filenamex;
                    txtpathktp.Text = fotopathktp;
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not.
                    {
                        file.Delete();
                    }

                    txtpathktp.Text = "~/Files/MENINGGAL/" + filenamex;
                    txtlink.Text = "View";
                    txtlink.NavigateUrl = txtpathktp.Text + "?r=" + DateTime.Now.Ticks.ToString();
                }

            }

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void UploadFileKTP(object sender, EventArgs e)
    {
        try
        {
            string namafile;
            string filename;
            string filenamex;
            string fotopathktp;

            string extension = Path.GetExtension(fileuploadktp.FileName).ToLower();
            if (extension != ".pdf")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya file PDF yang diperbolehkan!');", true);
                return;
            }

            int maxSize = 4 * 1024 * 1024; // 5 MB
            if (fileuploadktp.PostedFile.ContentLength > maxSize)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript",
                    "javascript:SweetAlert('Ukuran file maksimal 4 MB!');", true);
                return;
            }

            string folderPath = Server.MapPath("~/Files/MENINGGAL/thumb");
            DateTime now = DateTime.Now;

            namafile = now.ToString("ddMMyyyyhhmmss") + "_" + "RIP";
            filenamex = namafile + "OK";



            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            txtpathktp.Text = "";

            string uploadedFile = Path.Combine(folderPath, filenamex + ".pdf");
            string compressedFile = Path.Combine(folderPath, filenamex + "_tmp.pdf");

            fileuploadktp.SaveAs(uploadedFile);

            CompressPdf(uploadedFile, compressedFile);
            File.Delete(uploadedFile);
            File.Move(compressedFile, uploadedFile);

            txtpathktp.Text = "~/Files/MENINGGAL/thumb/" + filenamex;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "scrollToImage",
            "setTimeout(function(){ document.getElementById('" + txtpathktp.ClientID + "').scrollIntoView({behavior: 'auto'}); }, 150);",
            true);

            txtlink.Text = "View";
            txtlink.NavigateUrl = txtpathktp.Text + ".pdf";
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    public static double PMT(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
    {
        var rate = (double)yearlyInterestRate / 100 / 12;
        var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
        return (rate + (rate / denominator)) * loanAmount;
    }

    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;



        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("KP_Trn_MENINGGAL", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
            txtkodelgn.Text = kodelgn;
            txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txttglsk.Text = ge.ListDataset.Tables[0].Rows[0]["tglsk"].ToString();
      
            Txtnorekening.Text = HDNoRekening.Value;
            btnSimpan.Visible = true;

            //txtNmSup.Enabled = true;
            txttglsk.Enabled = true;
   
            txttgltrn.Enabled = true;
            txtKeterangan.Enabled = true;
            fileuploadktp.Enabled = true;
            btnUploadktp.Enabled = true;

        }

        else
        {
            txtkodelgn.Text = "";
            txtNmSup.Text = "";
            txttglsk.Text ="";
          
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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_getCustomer_Meninggal", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

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
