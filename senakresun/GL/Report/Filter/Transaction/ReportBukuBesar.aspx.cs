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

public partial class GL_Report_ReportBukuBesar : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {


            if (!Page.IsPostBack)
            {


                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");
             
                BindGridEntity();

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
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        Txtrptperiode.Enabled = true;
        ddlSP.Enabled = true;
        //DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[3];
        //Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramt[1] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramt[2] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference", Paramt, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), Ddlreferenceawal, "None", "");
        //Ddlreferenceawal.Items.Insert(0, new ListItem("-", "-"));
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), Ddlreferenceakhir, "None", "");
        //Ddlreferenceakhir.Items.Insert(0, new ListItem("-", "-"));

        //DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[3];
        //Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramsx[1] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramsx[2] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));

        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoaReportTrn", Paramsx, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountakhir, "None", "");
        //ddlaccountawal.Items.Insert(0, new ListItem("-", "-"));
        //ddlaccountakhir.Items.Insert(0, new ListItem("-", "-"));

        //DefaultClass.Global.SqlHelper[] Paramsl = new DefaultClass.Global.SqlHelper[3];
        //Paramsl[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramsl[1] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramsl[2] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference_Source", Paramsl, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlsourceawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlsourceakhir, "None", "");
        //ddlsourceawal.Items.Insert(0, new ListItem("-", "-"));
        //ddlsourceakhir.Items.Insert(0, new ListItem("-", "-"));

        //DefaultClass.Global.SqlHelper[] Paramsm = new DefaultClass.Global.SqlHelper[3];
        //Paramsm[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramsm[1] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramsm[2] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference_CostCenter", Paramsm, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcostcenterawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcostcenterakhir, "None", "");
        //ddlcostcenterawal.Items.Insert(0, new ListItem("-", "-"));
        //ddlcostcenterakhir.Items.Insert(0, new ListItem("-", "-"));


        //DefaultClass.Global.SqlHelper[] Paramss = new DefaultClass.Global.SqlHelper[3];
        //Paramss[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //Paramss[1] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramss[2] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference_TypeJournal", Paramss, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypejournalawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypejournalakhir, "None", "");
        //ddltypejournalawal.Items.Insert(0, new ListItem("-", "-"));
        //ddltypejournalakhir.Items.Insert(0, new ListItem("-", "-"));

        //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
        //Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetDeptReport", Params, "constrmf");
        //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddldprtawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddldprtakhir, "None", "");
        //ddldprtawal.Items.Insert(0, new ListItem("-", "-"));
        //ddldprtakhir.Items.Insert(0, new ListItem("-", "-"));

     

        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[2];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoaReportTrn", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountawal, "None", "");       
       



        DefaultClass.Global.SqlHelper[] Paramss = new DefaultClass.Global.SqlHelper[0];
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetNoreference_TypeJournal", Paramss, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddltypejournalawal, "None", "");
   
        ddltypejournalawal.Items.Insert(0, new ListItem("-", "ALL"));
        txtrpttitle.Text = "Buku Besar Report General Ledger";

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {


        ReportDocument rprt = new ReportDocument();
        try
                {


     
                

                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
            Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@KodeGlAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaccountawal.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@TypeTrnawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltypejournalawal.SelectedValue, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@TglTrnawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            Paramsc[4] = gb.CreateSPParameter("@TglTrnakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
            Paramsc[5] = gb.CreateSPParameter("@typeposting", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlrpttype.SelectedValue, typeof(string));
            Paramsc[6] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            //-- Paramsc[13] = gb.CreateSPParameter("@typeposting", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltype.SelectedValue, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.GL_STD_RPT_004_001", Paramsc, "constrmf").Tables[0];

                if (ge.ListDataTable.Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/GL/Report/Report/Transaction/GL.STD.RPT.004.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);
                    if (ddlrpttype.SelectedValue == "A")
                    {
                    rprt.SetParameterValue("ReportNama", txtrpttitle.Text + " Posting");
                    }
                    else
                    {
                    rprt.SetParameterValue("ReportNama", txtrpttitle.Text + " Un-Posting");
                    }
                rprt.SetParameterValue("User", SessLoginID);
                    rprt.SetParameterValue("Periode", Txtrptperiode.Text);
                Response.ClearContent();
                Response.ClearHeaders();

                ExportFormatType formatType = ExportFormatType.NoFormat;
                switch (DdlReport.SelectedItem.Value)
                {
                    case "1":
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        break;
                    case "2":
                        formatType = ExportFormatType.WordForWindows;
                        rprt.ExportToHttpResponse(formatType, Response, true, "reportbukubesarpdf");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "reportbukubesarpdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "reportbukubesarexcel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "reportbukubesarcsv");
                        break;
                }


                Response.End();

            }

                    


                }

                catch (Exception X)
                {
                   if (X.Message!="Thread was being aborted.")
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




    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
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
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");

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






    }



}