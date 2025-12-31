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
public partial class AR_View_ViewMasukTrnCair : System.Web.UI.Page
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
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A2";
    string kodesumber = "AP";
    string KodeKonter = "AP03";
    public string nobuktidb;
    string no_bukti;

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
                pnlFilter.Visible = true;
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text;
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
            //GRData.UseAccessibleHeader = true;
            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            //string nowx;
            //nowx = gb.ReadOrderData();
            //DateTime now = DateTime.Parse(nowx);
            //int tahun;
            //tahun = now.Year;
            //DateTime date = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
            //txtStart.Text = date.ToString("yyyy-MM-dd");
            //DateTime xdate = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
            //xdate = date.AddMonths(1);
            //xdate = xdate.AddDays(-1);
            //txtEnd.Text = xdate.ToString("yyyy-MM-dd");

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    decimal pokok = 0, bunga = 0, cadangan = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            pokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ttlpokok"));
            bunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ttlbunga"));
            cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalpokok = (Label)e.Row.FindControl("lbltotalpokok");
            lbltotalpokok.Text = string.Format("{0:N2}", pokok);
            Label lbltotalbunga = (Label)e.Row.FindControl("lbltotalbunga");
            lbltotalbunga.Text = string.Format("{0:N2}", bunga);
            Label lbltotalcadangan = (Label)e.Row.FindControl("lbltotalcadangan");
            lbltotalcadangan.Text = string.Format("{0:N2}", cadangan);
        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    Pnlfooter1.Visible = true;
                    pnlar.Visible = true;
                    Pnlcl.Visible = true;
                    pnlGL.Visible = true;
                    pnlFilter.Visible = false;

                    


                    no_bukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_bukti")).Text;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_rekening")).Text;
                    HDNoNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;

                    btnSerach_Click();

                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":


                break;
            //case "PDF":

            //    try
            //    {

            //        ReportDocument rprt = new ReportDocument();
            //        string nobukti;
            //        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

            //        //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

            //        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
            //        Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            //        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
            //        Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumber, typeof(string));
            //        Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            //        Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            //        Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //        ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AP", Paramsc, "constrmf").Tables[0];

            //        if (ge.ListDataTable.Rows.Count > 0)
            //        {


            //            rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
            //            rprt.SetDataSource(ge.ListDataTable);
            //            Response.Buffer = false;
            //            // Clear the response content and headers
            //            Response.ClearContent();
            //            Response.ClearHeaders();
            //            Response.ContentType = "application/pdf";
            //            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


            //        }


            //    }

            //    catch (Exception X)
            //    {

            //        if (X.Message != "Thread was being aborted.")
            //        {

            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //        }


            //    }

            //    break;

            //case "SLIP":

            //    try
            //    {

            //        ReportDocument rprt = new ReportDocument();
            //        string nobukti;
            //        string typetrn;
            //        nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
            //        typetrn = "K";

            //        //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

            //        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
            //        Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
            //        Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAP", typeof(string));
            //        Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            //        Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            //        Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

            //        ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

            //        if (ge.ListDataTable.Rows.Count > 0)
            //        {


            //            rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAP.rpt"));
            //            rprt.SetDataSource(ge.ListDataTable);
            //            if (typetrn == "B")
            //            {
            //                rprt.SetParameterValue("Reportname", "Terima Cash Bank");
            //            }
            //            else
            //            {
            //                rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
            //            }

            //            Response.Buffer = false;
            //            // Clear the response content and headers
            //            Response.ClearContent();
            //            Response.ClearHeaders();
            //            Response.ContentType = "application/pdf";
            //            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


            //        }


            //    }

            //    catch (Exception X)
            //    {

            //        if (X.Message != "Thread was being aborted.")
            //        {

            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //        }


            //    }

            //    break;

        }
    }

    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "rownumber asc";

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
    public void BindGridEntityx()
    {
        try
        {
            var WhereCond = "";
            string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";
            WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETVIEWPENCAIRAN", typeof(string));
            Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetSEARCH", Params, "constrmf");

            GRData.DataSource = ge.ListDataset.Tables[0];
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
    public void btnSerach_Click()
    {
        var kodelgn = HDNoNasabah.Value;
        string norekening = txtnobukti.Text;
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, no_bukti, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVIEW", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)


        {
                txtnobukti.Text = ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                txtnofaktur.Text = ge.ListDataset.Tables[0].Rows[0]["no_rek_tabungan"].ToString();
                txttgltrn.Text = ge.ListDataset.Tables[0].Rows[0]["tgl_drop"].ToString();
                ddlKdBank.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
                txttransAP.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["ttlpokok"].ToString()));
                txtjmlhGL.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["ttlbunga"].ToString()));
                
                txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["by_cadangan"].ToString()));
            
            txtNmBank.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";


               



        }

        else
        {
            
        }

        pnlGL.Visible = true;
        pnlar.Visible = true;
        Pnlcl.Visible = true;

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
        var2[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, no_bukti, typeof(string));
        var2[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        var2[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTAR", typeof(string));
        var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var2, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GCL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GCL.DataBind();

        GCL.UseAccessibleHeader = true;

        GCL.HeaderRow.TableSection = TableRowSection.TableHeader;


        DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[5];
        var5[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, no_bukti, typeof(string));
        var5[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        var5[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        var5[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTCL", typeof(string));
        var5[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var5, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GrdCL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GrdCL.DataBind();

        GrdCL.UseAccessibleHeader = true;

        GrdCL.HeaderRow.TableSection = TableRowSection.TableHeader;


        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
            var3[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, no_bukti, typeof(string));
            var3[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            var3[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            var3[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTGL", typeof(string));
            var3[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_Trn_AR_View", var3, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kodegl asc";


            GGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GGL.DataBind();

            GGL.UseAccessibleHeader = true;

            GGL.HeaderRow.TableSection = TableRowSection.TableHeader;

      
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
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
            //txttgljthtmp.Text = txttgltrn.Text;
        }
        //txttgljthtmp.Text = txttgltrn.Text;
    }

   
    [WebMethod]
    public static string GetBukti(string InputText, string SessKdpershx, string SessBranchIDx, string tglawal, string tglakhir)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));
        Params[3] = gb.CreateSPParameter("@tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglawal, typeof(DateTime));
        Params[4] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tglakhir, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AP_get_AP", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }


}
