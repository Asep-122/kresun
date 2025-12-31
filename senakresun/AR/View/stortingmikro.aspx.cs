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

public partial class Report_stortingmikro : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    private Boolean SessIsSuperAdmin
    {
        get { return Boolean.Parse(Session["SessIsSuperAdmin"].ToString()); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }

    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessIsLevel
    {
        get { return Session["SessIsLevel"].ToString(); }
        set { ViewState["SessIsLevel"] = value; }

    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    DataTable dtBayar = new DataTable("TransactionBayar");

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
                //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_X", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();
                ddlcabang.SelectedValue = SessBranchID;

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[2];
                varx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                varx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_Marketing_List", varx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";
                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "Text";
                ddlmarketing.DataValueField = "value";
                ddlmarketing.DataBind();
                ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));


                Lbltaspen.Text = "STORTING - DATA ACCOUNT OFFICER MIKRO";
               

                txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
                //txtTgljwl.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtTglLto.Text = DateTime.Now.ToString("dd-MM-yyyy");
                //Pnltagihan.Visible = true;

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }
    }



    public void BindGridEntityTagihan()
    {
        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STORTINGTAGIHAN", typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[6] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


        Grdtagihank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihank.DataBind();

        Grdtagihank.UseAccessibleHeader = true;

        Grdtagihank.HeaderRow.TableSection = TableRowSection.TableHeader;



    }
    public void BindGridEntityTagihanmrk()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STORTINGMARKETING", typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[6] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


        Grdtagihanmrk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihanmrk.DataBind();

        Grdtagihanmrk.UseAccessibleHeader = true;

        Grdtagihanmrk.HeaderRow.TableSection = TableRowSection.TableHeader;



            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }
    }

   

   
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();

        if (ddldata.SelectedValue == "0")
        {
            DataTable ds = new DataTable();
            ds = null;
            Grdtagihank.DataSource = ds;
            Grdtagihank.DataBind();
            Grdtagihanmrk.DataSource = ds;
            Grdtagihanmrk.DataBind();
            System.Threading.Thread.Sleep(5000);
            if (Lbltaspen.Text == "STORTING - DATA ACCOUNT OFFICER MIKRO")
            {
                Pnltagihan.Visible = true;
                Pnltagihanmrk.Visible = false;
                BindGridEntityTagihan();
            }

            else if (Lbltaspen.Text == "STORTING - DATA SP/SM MIKRO")
            {
                Pnltagihan.Visible = false;
                Pnltagihanmrk.Visible = true;
                BindGridEntityTagihanmrk();

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        else
        {
            if (Lbltaspen.Text == "STORTING - DATA ACCOUNT OFFICER MIKRO")
            {
                try
                {




                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                    var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                    var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STORTINGTAGIHAN", typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[6] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
                    //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AR/View/rptstortingao.rpt"));

                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("Userprint", SessLoginID);
                        rprt.SetParameterValue("xPeriode", txtTglL.Text + " to " + txtTglLto.Text);
                        rprt.SetParameterValue("Produk", "ALL PRODUK");
                        rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                        //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                        Response.ClearContent();
                        Response.ClearHeaders();
                        ////ExportFormatType formatType = ExportFormatType.NoFormat;
                        //Response.ContentType = "application/pdf";
                        //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        ExportFormatType formatType = ExportFormatType.NoFormat;
                        switch (ddldata.SelectedItem.Value)
                        {
                            case "1":
                                Response.ContentType = "application/pdf";
                                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                break;
                            case "2":
                                formatType = ExportFormatType.WordForWindows;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingword");
                                break;
                            case "3":
                                formatType = ExportFormatType.PortableDocFormat;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingpdf");
                                break;
                            case "4":
                                formatType = ExportFormatType.Excel;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingexcel");
                                break;
                            case "5":
                                formatType = ExportFormatType.CharacterSeparatedValues;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingcsv");
                                break;
                        }


                        Response.End();


                    }
                    else if (ge.ListDataTable.Rows.Count <= 0)

                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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
            }
            else if (Lbltaspen.Text == "STORTING - DATA SP/SM MIKRO")
            {
                try
                {




                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                    var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                    var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STORTINGMARKETING", typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[6] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
                    //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AR/View/rptstortingrekapao.rpt"));

                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("Userprint", SessLoginID);
                        rprt.SetParameterValue("xPeriode", txtTglL.Text + " to " + txtTglLto.Text);
                        rprt.SetParameterValue("Produk", "ALL PRODUK");
                        rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                        //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                        Response.ClearContent();
                        Response.ClearHeaders();
                        //ExportFormatType formatType = ExportFormatType.NoFormat;
                        //Response.ContentType = "application/pdf";
                        //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        ExportFormatType formatType = ExportFormatType.NoFormat;
                        switch (ddldata.SelectedItem.Value)
                        {
                            case "1":
                                Response.ContentType = "application/pdf";
                                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                break;
                            case "2":
                                formatType = ExportFormatType.WordForWindows;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingword");
                                break;
                            case "3":
                                formatType = ExportFormatType.PortableDocFormat;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingpdf");
                                break;
                            case "4":
                                formatType = ExportFormatType.Excel;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingexcel");
                                break;
                            case "5":
                                formatType = ExportFormatType.CharacterSeparatedValues;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportstortingcsv");
                                break;
                        }



                        Response.End();


                    }
                    else if (ge.ListDataTable.Rows.Count <= 0)

                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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
            }

        }
    }





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btntagihan_Click(object sender, EventArgs e)
    {
        //DataTable ds = new DataTable();
        //ds = null;
        //Grdtagihank.DataSource = ds;
        //Grdtagihank.DataBind();
        Pnlsearch.Visible = true;
        txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtTglLto.Text = DateTime.Now.ToString("dd-MM-yyyy");
        //txtTgljwl.Text = DateTime.Now.ToString("dd-MM-yyyy");

        //ddlmarketing.Enabled = false;

        txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");

        Lbltaspen.Text = "STORTING - DATA ACCOUNT OFFICER MIKRO";

        Pnltagihan.Visible = false;
        Pnltagihanmrk.Visible = false;


    }
    protected void btntunggakan_Click(object sender, EventArgs e)
    {
        //DataTable ds = new DataTable();
        //ds = null;
        //Grdtagihanmrk.DataSource = ds;
        //Grdtagihanmrk.DataBind();
        Pnlsearch.Visible = true;
        txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtTglLto.Text = DateTime.Now.ToString("dd-MM-yyyy");
        //txtTgljwl.Text = DateTime.Now.ToString("dd-MM-yyyy");

        Lbltaspen.Text = "STORTING - DATA SP/SM MIKRO";

        Pnltagihan.Visible = false;
        Pnltagihanmrk.Visible = false;

    }

    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {


        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[2];
        varx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        varx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
        //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Marketing_List", varx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";
        ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlmarketing.DataTextField = "Text";
        ddlmarketing.DataValueField = "value";
        ddlmarketing.DataBind();
        ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));


    }
    decimal plafon = 0, targetpokok = 0, targetbunga = 0, totaltarget = 0, stortingpokok = 0, stortingbunga = 0, rasiopokok = 0, rasiobunga = 0, totalstorting = 0;
    protected void Grdtagihanmrk_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            var outputParam = DataBinder.Eval(e.Row.DataItem, "target_pokok");
            if (!(outputParam is DBNull)) targetpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "target_pokok"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "target_bunga");
            if (!(outputParam is DBNull)) targetbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "target_bunga"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "total_target");
            if (!(outputParam is DBNull)) totaltarget += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_target"));

            stortingpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "storting_pokok"));
            stortingbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "storting_bunga"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "rasio_pokok");
            if (!(outputParam is DBNull)) rasiopokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "rasio_pokok"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "rasio_bunga");
            if (!(outputParam is DBNull)) rasiobunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "rasio_bunga"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "total_storting");
            if (!(outputParam is DBNull)) totalstorting += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_storting"));




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = plafon.ToString("N2");

            Label lbltargetpokok = (Label)e.Row.FindControl("lbltargetpokok");
            lbltargetpokok.Text = targetpokok.ToString("N2");

            Label lbltargetbunga = (Label)e.Row.FindControl("lbltargetbunga");
            lbltargetbunga.Text = targetbunga.ToString("N2");

            Label lbltotaltarget = (Label)e.Row.FindControl("lbltotaltarget");
            lbltotaltarget.Text = totaltarget.ToString("N2");

            Label lblstortingpokok = (Label)e.Row.FindControl("lblstortingpokok");
            lblstortingpokok.Text = stortingpokok.ToString("N2");

            Label lblstortingbunga = (Label)e.Row.FindControl("lblstortingbunga");
            lblstortingbunga.Text = stortingbunga.ToString("N2");

            Label lblrasiopokok = (Label)e.Row.FindControl("lblrasiopokok");
            lblrasiopokok.Text = rasiopokok.ToString("N2");

            Label lblrasiobunga = (Label)e.Row.FindControl("lblrasiobunga");
            lblrasiobunga.Text = rasiobunga.ToString("N2");

            Label lbltotalstorting = (Label)e.Row.FindControl("lbltotalstorting");
            lbltotalstorting.Text = totalstorting.ToString("N2");


        }
    }

    protected void Grdtagihank_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            var outputParam = DataBinder.Eval(e.Row.DataItem, "target_pokok");
            if (!(outputParam is DBNull)) targetpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "target_pokok"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "target_bunga");
            if (!(outputParam is DBNull)) targetbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "target_bunga"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "total_target");
            if (!(outputParam is DBNull)) totaltarget += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_target"));
            
            stortingpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "storting_pokok"));
            stortingbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "storting_bunga"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "rasio_pokok");
            if (!(outputParam is DBNull)) rasiopokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "rasio_pokok"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "rasio_bunga");
            if (!(outputParam is DBNull)) rasiobunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "rasio_bunga"));
            outputParam = DataBinder.Eval(e.Row.DataItem, "total_storting");
            if (!(outputParam is DBNull)) totalstorting += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_storting"));



        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = plafon.ToString("N2");

            Label lbltargetpokok = (Label)e.Row.FindControl("lbltargetpokok");
            lbltargetpokok.Text = targetpokok.ToString("N2");

            Label lbltargetbunga = (Label)e.Row.FindControl("lbltargetbunga");
            lbltargetbunga.Text = targetbunga.ToString("N2");

            Label lbltotaltarget = (Label)e.Row.FindControl("lbltotaltarget");
            lbltotaltarget.Text = totaltarget.ToString("N2");

            Label lblstortingpokok = (Label)e.Row.FindControl("lblstortingpokok");
            lblstortingpokok.Text = stortingpokok.ToString("N2");

            Label lblstortingbunga = (Label)e.Row.FindControl("lblstortingbunga");
            lblstortingbunga.Text = stortingbunga.ToString("N2");

            Label lblrasiopokok = (Label)e.Row.FindControl("lblrasiopokok");
            lblrasiopokok.Text = rasiopokok.ToString("N2");

            Label lblrasiobunga = (Label)e.Row.FindControl("lblrasiobunga");
            lblrasiobunga.Text = rasiobunga.ToString("N2");

            Label lbltotalstorting = (Label)e.Row.FindControl("lbltotalstorting");
            lbltotalstorting.Text = totalstorting.ToString("N2");


        }
    }
}