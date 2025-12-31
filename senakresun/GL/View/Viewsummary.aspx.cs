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
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Security.AccessControl;

public partial class GL_Report_Viewsummary_001 : System.Web.UI.Page
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
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
                Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetTeller", Paramx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
                ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));

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
        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[2];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoaReport", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlaccountawal, "None", "");
   

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
       
        //Txtrptperiode.Enabled = false;
        ddlSP.Enabled = false;


    }
    //public void btnSerach_Click(object sender, EventArgs e)
    //{


    //    ReportDocument rprt = new ReportDocument();
    //    try
    //            {




    //            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
    //        Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        Paramsc[1] = gb.CreateSPParameter("@KodeGlAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaccountawal.SelectedValue, typeof(string));
    //        Paramsc[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
    //        Paramsc[3] = gb.CreateSPParameter("@DteTglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
    //        Paramsc[4] = gb.CreateSPParameter("@DteTglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
    //        Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        ge.ListDataTable = gb.GetDataJustOutPut("dbo.GL_Neracapercobaan", Paramsc, "constrmf").Tables[0];

    //        if (ge.ListDataTable.Rows.Count > 0)
    //        {




    //            rprt.Load(Server.MapPath("~/GL/Report/Report/NeracaPercobaan/RptGL_NPRekap.rpt"));
    //            rprt.SetDataSource(ge.ListDataTable);
    //            rprt.SetParameterValue("NamaPerusahaan", SessNamaProfil);

    //            rprt.SetParameterValue("User", SessLoginID);
    //            rprt.SetParameterValue("ReportNama", txtrpttitle.Text);

    //            rprt.SetParameterValue("Periode", Txtrptperiode.Text);
    //            //rprt.SetParameterValue("Kurs", "Kurs :" + ddlCurrency.SelectedValue + "  Amount " + Txtkurs.Text);
    //            Response.ClearContent();
    //            Response.ClearHeaders();

    //            ExportFormatType formatType = ExportFormatType.NoFormat;
    //            switch (DdlReport.SelectedItem.Value)
    //            {
    //                case "1":
    //                    Response.ContentType = "application/pdf";
    //                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
    //                    break;
    //                case "2":
    //                    formatType = ExportFormatType.WordForWindows;
    //                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanapasaldoword");
    //                    break;
    //                case "3":
    //                    formatType = ExportFormatType.PortableDocFormat;
    //                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldopdf");
    //                    break;
    //                case "4":
    //                    formatType = ExportFormatType.Excel;
    //                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldoexcel");
    //                    break;
    //                case "5":
    //                    formatType = ExportFormatType.CharacterSeparatedValues;
    //                    rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakuntanpasaldocsv");
    //                    break;
    //            }


    //            Response.End();


    //        }
    //        else

    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Daftar Account Empty');", true);
    //        }


    //        }

    //            catch (Exception X)
    //            {
    //               if (X.Message!="Thread was being aborted.")
    //                {

    //                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //                 }
    //                else
    //                {

    //                    rprt.Close();
    //                    rprt.Dispose();
    //                }

    //    }

    //}


    protected void btnSerach_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {





            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
            Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@KodeGlAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaccountawal.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@DteTglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            Paramsc[4] = gb.CreateSPParameter("@DteTglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
            Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsc[6] = gb.CreateSPParameter("@userteller", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(string));
            dt2.ListDataset = gb.GetDataJustOutPut("GL_Neracapercobaan_Search", Paramsc, "constrmf");
            if (dt2.ListDataset.Tables[0].Rows.Count > 0)
            {
                decimal jawal = decimal.Parse(dt2.ListDataset.Tables[0].Rows[0]["JawalBi"].ToString());
                decimal jdebit = decimal.Parse(dt2.ListDataset.Tables[0].Rows[0]["JdebitBI"].ToString());
                decimal jkredit = decimal.Parse(dt2.ListDataset.Tables[0].Rows[0]["JKREDITBI"].ToString());
                decimal jakhir = jawal + (jdebit - jkredit);

                decimal jdebitsm = decimal.Parse(dt2.ListDataset.Tables[0].Rows[0]["JDebitBism"].ToString());
                decimal jkreditsm = decimal.Parse(dt2.ListDataset.Tables[0].Rows[0]["JKREDITBISM"].ToString());
                decimal saldoakhirsm = decimal.Parse(dt2.ListDataset.Tables[0].Rows[0]["saldoakhirbp"].ToString());
                Txtsaldoawal.Text = string.Format("{0:N0}", jawal);
                txtsaldodebit.Text = string.Format("{0:N0}", jdebit); 
                txtsaldoKredit.Text = string.Format("{0:N0}", jkredit); 
                txtsaldoakhir.Text = string.Format("{0:N0}", jakhir);

                txtdebitblm.Text = string.Format("{0:N0}", jdebitsm);
                txtkreditBlm.Text = string.Format("{0:N0}", jkreditsm);
                txtsaldoakhirblm.Text = string.Format("{0:N0}", saldoakhirsm);


                txtsaldoakhirreal.Text = string.Format("{0:N0}", jakhir + saldoakhirsm);

            }

            else
            {
                Txtsaldoawal.Text = string.Format("{0:N2}", 0);
                txtsaldodebit.Text = string.Format("{0:N2}", 0);
                txtsaldoKredit.Text = string.Format("{0:N2}", 0);
                txtsaldoakhir.Text = string.Format("{0:N2}", 0);
                txtdebitblm.Text = string.Format("{0:N0}", 0);
                txtkreditBlm.Text = string.Format("{0:N0}", 0);
                txtsaldoakhirblm.Text = string.Format("{0:N0}", 0);
                txtsaldoakhirreal.Text = string.Format("{0:N0}", 0);
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }



    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;
       



        //ddlSP.SelectedValue = SessFiskalBi;
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
      
        //Txtrptperiode.Enabled = false;
        ddlSP.Enabled = true;


    }



}