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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using ClosedXML.Excel;
using System.Activities.Statements;

public partial class GL_Report_ReportKasHeadTeller : System.Web.UI.Page
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
    private string SessNamaProfil
    {
        get { return Session["SessNamaProfil"].ToString(); }
        set { ViewState["SessNamaProfil"] = value; }
    }
    private string SessAlamatProfil1
    {
        get { return Session["SessAlamatProfil1"].ToString(); }
        set { ViewState["SessAlamatProfil1"] = value; }
    }
    private string SessAlamatProfil2
    {
        get { return Session["SessAlamatProfil2"].ToString(); }
        set { ViewState["SessAlamatProfil2"] = value; }
    }
    private string SessKotaProfil
    {
        get { return Session["SessKotaProfil"].ToString(); }
        set { ViewState["SessKotaProfil"] = value; }
    }

    private string SessNegaraProfil
    {
        get { return Session["SessNegaraProfil"].ToString(); }
        set { ViewState["SessNegaraProfil"] = value; }
    }

    private string SessTeleponProfil
    {
        get { return Session["SessTeleponProfil"].ToString(); }
        set { ViewState["SessTeleponProfil"] = value; }
    }
    private string SessFaxProfil
    {
        get { return Session["SessFaxProfil"].ToString(); }
        set { ViewState["SessFaxProfil"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    GlobalEntities dt2 = new GlobalEntities();
    GlobalEntities dt3 = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {


                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");

                //BindGridEntity();
                ddlSP.SelectedValue = SessFiskalBi;
                ddlsp_SelectedIndexChanged(sender, e);


                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTBIAYAX", typeof(string));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_VIEW_HT", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "Namabiaya asc";

                ddlbiayaapproval.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlbiayaapproval.DataTextField = "Namabiaya";
                ddlbiayaapproval.DataValueField = "tipebiaya";
                ddlbiayaapproval.DataBind();
                ddlbiayaapproval.Items.Insert(0, new ListItem("ALL DATA", "ALL"));

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTBIAYANON", typeof(string));
                var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_VIEW_HT", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "Namabiaya asc";

                ddlbiayanon.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlbiayanon.DataTextField = "Namabiaya";
                ddlbiayanon.DataValueField = "tipebiaya";
                ddlbiayanon.DataBind();
                ddlbiayanon.Items.Insert(0, new ListItem("ALL DATA", "ALL"));

                ddltypetransaksi_SelectedIndexChanged(sender, e);
                ddlrpttype_SelectedIndexChanged(sender, e);

                

                


            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {


     
        
        //txtrpttitle.Text = "Transaction Buku Besar Report";

        //DateTime now = DateTime.Now;
        ddlSP.SelectedValue = SessFiskalBi;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        //Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        //Txtrptperiode.Enabled = true;
        ddlSP.Enabled = true;


        //DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[2];
        //Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramsx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        //ge.ListDataset = gb.GetDataJustOutPut("dbo.MFT_GetBRANCH", Paramsx, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountawal, "None", "");
        //ddlaccountawal.SelectedValue = SessBranchID;

        //DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
        //Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetUserJournal", Paramx, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
        //ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));

        //DefaultClass.Global.SqlHelper[] Paramss = new DefaultClass.Global.SqlHelper[0];
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference_TypeJournal", Paramss, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypejournalawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypejournalakhir, "None", "");
        //ddltypejournalawal.Items.Insert(0, new ListItem("ALL TYPE", "ALL"));
        //ddltypejournalakhir.Items.Insert(0, new ListItem("-", "-"));

        //DefaultClass.Global.SqlHelper[] Paramsr = new DefaultClass.Global.SqlHelper[2];
        //Paramsr[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramsr[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoaReportTrn", Paramsx, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountakhir, "None", "");

    }

    decimal xjlhdebit = 0, xjlhkredit = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Tambah"));
            xjlhkredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Kurang"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbldebit = (Label)e.Row.FindControl("lbldebit");
            lbldebit.Text = string.Format("{0:N0}", xjlhdebit);

            Label lblkredit = (Label)e.Row.FindControl("lblkredit");
            lblkredit.Text = string.Format("{0:N0}", xjlhkredit);
           

        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Tambah"));
            xjlhkredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Kurang"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbldebit = (Label)e.Row.FindControl("lbldebit");
            lbldebit.Text = string.Format("{0:N0}", xjlhdebit);

            Label lblkredit = (Label)e.Row.FindControl("lblkredit");
            lblkredit.Text = string.Format("{0:N0}", xjlhkredit);


        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Tambah"));
            xjlhkredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Kurang"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbldebit = (Label)e.Row.FindControl("lbldebit");
            lbldebit.Text = string.Format("{0:N0}", xjlhdebit);

            Label lblkredit = (Label)e.Row.FindControl("lblkredit");
            lblkredit.Text = string.Format("{0:N0}", xjlhkredit);


        }
    }
    public void btnsearchdata_Click(object sender, EventArgs e)
    {
        try
        {

       
       if (ddlrpttype.SelectedValue == "A")
        {
            DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[8];
            Paramst[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramst[1] = gb.CreateSPParameter("@Typelain", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypetransaksi.SelectedValue, typeof(string));
            Paramst[2] = gb.CreateSPParameter("@kodejurnal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenistransaksi.SelectedValue, typeof(string));
            Paramst[3] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            Paramst[4] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
            Paramst[5] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            Paramst[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramst[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRANSAKSILAIN", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_VIEW_HT", Paramst, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn asc";


            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

            txtmutasidebit.Text = string.Format("{0:N2}", xjlhdebit);
            txtmutasikredit.Text = string.Format("{0:N2}", xjlhkredit);
            txtsaldoakhir.Text = string.Format("{0:N2}", (decimal.Parse(txtmutasidebit.Text) - decimal.Parse(txtmutasikredit.Text)));

        }
       
            if (ddlrpttype.SelectedValue == "B")
        {
            DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[8];
            Paramst[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramst[1] = gb.CreateSPParameter("@Typelain", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
            Paramst[2] = gb.CreateSPParameter("@kodejurnal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbiayaapproval.SelectedValue, typeof(string));
            Paramst[3] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            Paramst[4] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
            Paramst[5] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            Paramst[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramst[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRANSAKSIBIAYAAPP", typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_VIEW_HT", Paramst, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn asc";


                GridView1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GridView1.DataBind();

                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            txtmutasidebit.Text = string.Format("{0:N2}", xjlhdebit);
            txtmutasikredit.Text = string.Format("{0:N2}", xjlhkredit);
            txtsaldoakhir.Text = string.Format("{0:N2}", (decimal.Parse(txtmutasidebit.Text) - decimal.Parse(txtmutasikredit.Text)));

        }
            if (ddlrpttype.SelectedValue == "C")
            {
                DefaultClass.Global.SqlHelper[] Paramst = new DefaultClass.Global.SqlHelper[8];
                Paramst[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramst[1] = gb.CreateSPParameter("@Typelain", ParameterDirection.Input, SqlDbType.VarChar, 10, "", typeof(string));
                Paramst[2] = gb.CreateSPParameter("@kodejurnal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbiayanon.SelectedValue, typeof(string));
                Paramst[3] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
                Paramst[4] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
                Paramst[5] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                Paramst[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Paramst[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRANSAKSIBIAYANON", typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_VIEW_HT", Paramst, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn asc";


                GridView2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GridView2.DataBind();

                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;

                txtmutasidebit.Text = string.Format("{0:N2}", xjlhdebit);
                txtmutasikredit.Text = string.Format("{0:N2}", xjlhkredit);
                txtsaldoakhir.Text = string.Format("{0:N2}", (decimal.Parse(txtmutasidebit.Text) - decimal.Parse(txtmutasikredit.Text)));

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now =DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue),1 );
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        //Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy") +  " s/d " + xdate.ToString("dd-MM-yyyy");

        //DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[4];
        //Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramt[1] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramt[2] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //Paramt[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference", Paramt, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), Ddlreferenceawal, "None", "");
        //Ddlreferenceawal.Items.Insert(0, new ListItem("-", "-"));
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), Ddlreferenceakhir, "None", "");
        //Ddlreferenceakhir.Items.Insert(0, new ListItem("-", "-"));


        GRData.DataSource = null;
        GRData.DataBind();



    }

    protected void ddltgl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;

        //Txtrptperiode.Text = "Periode : " + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");

        //DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[4];
        //Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramt[1] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramt[2] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //Paramt[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference", Paramt, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), Ddlreferenceawal, "None", "");
        //Ddlreferenceawal.Items.Insert(0, new ListItem("-", "-"));
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), Ddlreferenceakhir, "None", "");
        //Ddlreferenceakhir.Items.Insert(0, new ListItem("-", "-"));



        GRData.DataSource = null;
        GRData.DataBind();


    }


    protected void ddlrpttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrpttype.SelectedValue == "A")
        {
            PnlTransaksiLain.Visible = true;
            PNLBIAYANON.Visible = false;
            PNLBIAYAAPROVAL.Visible = false;
            ddltypetransaksi.SelectedValue = "ALL";

            ddljenistransaksi.SelectedValue = "ALL";
            txtmutasidebit.Text = string.Format("{0:N2}", "0"); 
            txtmutasikredit.Text = string.Format("{0:N2}", "0");
            txtsaldoakhir.Text = string.Format("{0:N2}", "0");
            GRData.DataSource = null;
            GRData.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            PNLBIAYAAPROVAL.Visible = false;
            ddlbiayaapproval.SelectedValue = "ALL";
        }
        else if (ddlrpttype.SelectedValue == "B")
        {
            PnlTransaksiLain.Visible = false;
            PNLBIAYANON.Visible = false;
            PNLBIAYAAPROVAL.Visible = true;
            ddlbiayaapproval.SelectedValue = "ALL";
            ddltypetransaksi.SelectedValue = "ALL";
            ddljenistransaksi.SelectedValue = "ALL";
            txtmutasidebit.Text = string.Format("{0:N2}", "0");
            txtmutasikredit.Text = string.Format("{0:N2}", "0");
            txtsaldoakhir.Text = string.Format("{0:N2}", "0");
            GRData.DataSource = null;
            GRData.DataBind();

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        else if (ddlrpttype.SelectedValue == "C")
        {
            PnlTransaksiLain.Visible = false;
            PNLBIAYAAPROVAL.Visible = false;
            PNLBIAYANON.Visible = true;
            ddlbiayaapproval.SelectedValue = "ALL";
            ddltypetransaksi.SelectedValue = "ALL";
            ddljenistransaksi.SelectedValue = "ALL";
            ddlbiayanon.SelectedValue = "ALL";
            txtmutasidebit.Text = string.Format("{0:N2}", "0");
            txtmutasikredit.Text = string.Format("{0:N2}", "0");
            txtsaldoakhir.Text = string.Format("{0:N2}", "0");
            GRData.DataSource = null;
            GRData.DataBind();

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        else
        {
            PnlTransaksiLain.Visible = false;
        }



    }
    protected void ddltypetransaksi_SelectedIndexChanged(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
        var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var1[1] = gb.CreateSPParameter("@Typelain", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypetransaksi.SelectedValue , typeof(string));
        var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTTRNLAIN", typeof(string));
        var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_VIEW_HT", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";

        ddljenistransaksi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljenistransaksi.DataTextField = "KetJurnal";
        ddljenistransaksi.DataValueField = "KodeJurnal";
        ddljenistransaksi.DataBind();
        ddljenistransaksi.Items.Insert(0, new ListItem("ALL DATA","ALL"));
        btnsearchdata_Click(sender, e);

    }
    protected void ddljenistransaksi_SelectedIndexChanged(object sender, EventArgs e)
    {

        btnsearchdata_Click(sender, e);
   }
    protected void ddlbiayaapproval_SelectedIndexChanged(object sender, EventArgs e)
    {

        btnsearchdata_Click(sender, e);
    }

    protected void ddlbiayanon_SelectedIndexChanged(object sender, EventArgs e)
    {

        btnsearchdata_Click(sender, e);
    }
}