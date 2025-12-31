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

public partial class Report_realisasimikro : System.Web.UI.Page
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
    // Global gb = new Global();
    //GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //BindGrddatawilayahEntity();
                DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");


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
                //DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[2];
                //varx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                //varx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
                ////var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("AR_Marketing_List", varx, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";
                //dd.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlmarketing.DataTextField = "Text";
                //ddlmarketing.DataValueField = "value";
                //ddlmarketing.DataBind();
                //ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));



                Lbltaspen.Text = "DATA REALISASI MIKRO SPSM / RESUME";
                txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtTglLto.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    //'int total = 0;
    decimal plafon = 0, nasabah = 0, baki = 0;
    decimal Total = 0, jmlbaru = 0, plafonbaru = 0, jmllama = 0, plafonlama=0,jmltotal=0, plafontotal=0;
    protected void Grdtagihank_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Total += 1;
            jmlbaru += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jml_baru"));
            plafonbaru += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon_baru"));
            jmllama += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jml_lama"));
            plafonlama += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon_lama"));
            jmltotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jml_total"));
            plafontotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon_total"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotal = (Label)e.Row.FindControl("lblTotal");
            lbltotal.Text = Total.ToString("N2");
            Label lbljmlbaru = (Label)e.Row.FindControl("lbljmlbaru");
            lbljmlbaru.Text = jmlbaru.ToString("N2");

            Label lblplafonbaru = (Label)e.Row.FindControl("lblplafonbaru");
            lblplafonbaru.Text = plafonbaru.ToString("N2");
            Label lbljmllama = (Label)e.Row.FindControl("lbljmllama");
            lbljmllama.Text = jmllama.ToString("N2");

            Label lblplafonlama = (Label)e.Row.FindControl("lblplafonlama");
            lblplafonlama.Text = plafonlama.ToString("N2");

            Label lbljmltotal = (Label)e.Row.FindControl("lbljmltotal");
            lbljmltotal.Text = jmltotal.ToString("N2");


            Label lblplafontotal = (Label)e.Row.FindControl("lblplafontotal");
            lblplafontotal.Text = plafontotal.ToString("N2");

        }
    }
    protected void Grdtagihanmrk_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

                nasabah +=1;
            plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
          




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblnasabah = (Label)e.Row.FindControl("lblnasabah");
            lblnasabah.Text = nasabah.ToString("N2");
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = plafon.ToString("N2");

            Label lblbaki = (Label)e.Row.FindControl("lblbaki");
            lblbaki.Text = baki.ToString("N2");

          



        }
    }

    public void BindGridEntityTagihan()
    {
      


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REALISASISPSM", typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");



        Grdtagihank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihank.DataBind();

        Grdtagihank.UseAccessibleHeader = true;

        Grdtagihank.HeaderRow.TableSection = TableRowSection.TableHeader;


    }
    public void BindGridEntityTagihanmrk()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REALISASIDROPPING", typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        Grdtagihanmrk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihanmrk.DataBind();

        Grdtagihanmrk.UseAccessibleHeader = true;

        Grdtagihanmrk.HeaderRow.TableSection = TableRowSection.TableHeader;


       


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
            if (Lbltaspen.Text == "DATA REALISASI MIKRO SPSM / RESUME")
            {
                Pnltagihan.Visible = true;
                Pnltagihanmrk.Visible = false;
                BindGridEntityTagihan();
            }

            else if (Lbltaspen.Text == "DATA KREDIT MIKRO BERDASARKAN TANGGAL DROPING")
            {
                Pnltagihan.Visible = false;
                Pnltagihanmrk.Visible = false;
                BindGridEntityTagihanmrk();

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        else
        {
            if (Lbltaspen.Text == "DATA REALISASI MIKRO SPSM / RESUME")
            {
               
                try
                {




                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                    var[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REALISASISPSM", typeof(string));
                    var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[5] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
                    
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AR/View/rptrealisasirekapao.rpt"));

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
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasiword");
                                break;
                            case "3":
                                formatType = ExportFormatType.PortableDocFormat;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasipdf");
                                break;
                            case "4":
                                formatType = ExportFormatType.Excel;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasiexcel");
                                break;
                            case "5":
                                formatType = ExportFormatType.CharacterSeparatedValues;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasicsv");
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

            else if (Lbltaspen.Text == "DATA KREDIT MIKRO BERDASARKAN TANGGAL DROPING")
            {
                try
                {




                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                    var[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REALISASIDROPPING", typeof(string));
                    var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[5] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglLto.Text, typeof(DateTime));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                    if (ge.ListDataTable.Rows.Count > 0)
                    {
                        rprt.Load(Server.MapPath("~/AR/View/rptrealisasiao.rpt"));

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
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasiword");
                                break;
                            case "3":
                                formatType = ExportFormatType.PortableDocFormat;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasipdf");
                                break;
                            case "4":
                                formatType = ExportFormatType.Excel;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasiexcel");
                                break;
                            case "5":
                                formatType = ExportFormatType.CharacterSeparatedValues;
                                rprt.ExportToHttpResponse(formatType, Response, true, "Reportrealisasicsv");
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
             
      
        //ddlmarketing.Enabled = false;
        
        txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtTglLto.Text = DateTime.Now.ToString("dd-MM-yyyy");
        //ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));

        Lbltaspen.Text = "DATA REALISASI MIKRO SPSM / RESUME";

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

        Lbltaspen.Text = "DATA KREDIT MIKRO BERDASARKAN TANGGAL DROPING";

        Pnltagihan.Visible = false;
        Pnltagihanmrk.Visible = false;
  
    }


}