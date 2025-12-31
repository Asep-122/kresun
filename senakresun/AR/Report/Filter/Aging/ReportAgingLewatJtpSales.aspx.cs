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
using System.Text.RegularExpressions;
using System.Globalization;

public partial class AR_Report_ReportAgingLewatJtpSales : System.Web.UI.Page
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
                txtlimitinternal.Text = "0";
                DateKurs.Text = now.ToString("yyyy-MM-dd");
                Txtkurs.Text = "0";
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

    protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedValue == "IDR")
        {
            Txtkurs.Text = "1";
            Txtkurs.Enabled = false;
        }
        else
        {
            //Txtkurs.Text = "1";

            Txtkurs.Enabled = true;

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlCurrency.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@TglKurs", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateKurs.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("GL_GetKurs", Params, "constrmf");
            DataTable dt = new DataTable();
            dt = ge.ListDataset.Tables[0].DefaultView.ToTable();


            if (dt.Rows.Count > 0)
            {
                Txtkurs.Text = dt.Rows[0]["kursstd"].ToString();
            }
            else
            {
                Txtkurs.Text = "1";
            }
        }

    }

    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[2];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_GetSalesmanReport", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountakhir, "None", "");
        ddlaccountawal.Items.Insert(0, new ListItem("-", "-"));
        ddlaccountakhir.Items.Insert(0, new ListItem("-", "-"));

        DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduct, "None", "");

        DefaultClass.Global.SqlHelper[] Paramt = new DefaultClass.Global.SqlHelper[1];
        Paramt[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
     
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCurrencyReport", Paramt, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCurrency, "None", "");
        ddlCurrency.Items.Insert(0, new ListItem("-", "-"));

        if (SessSystemKurs == "False")
        {
            ddlCurrency.SelectedValue = "IDR";
            ddlCurrency.Enabled = false;
            Txtkurs.Text = "1";
            Txtkurs.Enabled = false;
            DateKurs.Enabled = false;
        }
        else
        {
            ddlCurrency.SelectedValue = "IDR";
            ddlCurrency.Enabled = true;
            Txtkurs.Text = "1";
            Txtkurs.Enabled = true;
            DateKurs.Enabled = true;
        }

        txtrpttitle.Text = "Aging Will be Due Detail Customer Report Based Customer";
       
        
        DateTime date = DateTime.Now;
        txtStart.Text = date.ToString("yyyy-MM-dd");
            
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") ;
        Txtrptperiode.Enabled = false;
      
        ddlCurrency.SelectedValue = "IDR";
        Txtkurs.Text = "1";
        ddlaccountawal.SelectedValue = "                              ";
        ddlaccountakhir.SelectedValue = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ";
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {

        ReportDocument rprt = new ReportDocument();

        try
                {


                

                DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[9];
                Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsc[1] = gb.CreateSPParameter("@Kodesalesawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaccountawal.SelectedValue, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@Kodesalesakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaccountakhir.SelectedValue, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@Kurs", ParameterDirection.Input, SqlDbType.Money, 10, Txtkurs.Text, typeof(decimal));
                Paramsc[5] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "Sales", typeof(string));
                Paramsc[6] = gb.CreateSPParameter("@TglUmur", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
                Paramsc[7] = gb.CreateSPParameter("@BatasInterval", ParameterDirection.Input, SqlDbType.Int, 0, txtlimitinternal.Text, typeof(int));
                Paramsc[8] = gb.CreateSPParameter("@product", ParameterDirection.Input, SqlDbType.VarChar, 50,ddlproduct.SelectedValue, typeof(string));
               ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_AGING_CUSTOMER_LEWATJTP", Paramsc, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {



                string xdateawal, xdateakhir;
                DateTime date = DateTime.Parse(txtStart.Text);
                xdateawal = date.ToString("dd-MM-yyyy");
                if (ddltypereport.SelectedValue == "1")
                {
                    rprt.Load(Server.MapPath("~/AR/Report/Report/Aging/AR.STD.RPT.AKANJTPSALES.rpt"));
                    txtrpttitle.Text = "Laporan Detail Aging Lewat Jatuh Tempo Base Sales";
                }
                else
                {
                    rprt.Load(Server.MapPath("~/AR/Report/Report/Aging/AR.STD.RPT.AKANJTPSLSRKP.rpt"));
                    txtrpttitle.Text = "Laporan Rekap Aging Lewat Jatuh Tempo Base Sales";
                }
                
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("Nama", SessNamaProfil);
                rprt.SetParameterValue("User", SessLoginID);

                rprt.SetParameterValue("Judul", txtrpttitle.Text);


                rprt.SetParameterValue("Periode", Txtrptperiode.Text);
                rprt.SetParameterValue("product", ddlproduct.SelectedItem.Text);
                //rprt.SetParameterValue("Kurs", "Kurs :" + ddlCurrency.SelectedValue + "  Amount " + Txtkurs.Text);
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
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanapasaldoword");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldopdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldoexcel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldocsv");
                        break;
                }


                Response.End();


            }
            else

            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Daftar Account Empty');", true);
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
        
        DateTime date = DateTime.Now;
        txtStart.Text = date.ToString("yyyy-MM-dd");
        Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy");


    }

  

}