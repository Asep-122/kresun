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

public partial class GL_Report_Reportsummary : System.Web.UI.Page
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
                DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[1];
                Paramx[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetUserJournal", Paramx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlteller, "None", "");
                ddlteller.Items.Insert(0, new ListItem("ALL", "ALL"));
                BindGridEntity();

                ddlSP.Enabled = true;
               

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
        ddlaccountawal.Items.Insert(0, new ListItem("ALL AKUN", "ALL"));

        DefaultClass.Global.SqlHelper[] Paramsy = new DefaultClass.Global.SqlHelper[2];
        Paramsy[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsy[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MFT_GetBRANCH", Paramsy, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlbranch, "None", "");
        ddlbranch.SelectedValue = SessBranchID;

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
        Txtrptperiode.Enabled = false;
        ddlSP.Enabled = true;


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

    public void btnsearchdata_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[8];
        Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Paramsc[1] = gb.CreateSPParameter("@KodeGlAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaccountawal.SelectedValue, typeof(string));
        Paramsc[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        Paramsc[3] = gb.CreateSPParameter("@DteTglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
        Paramsc[4] = gb.CreateSPParameter("@DteTglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
        Paramsc[5] = gb.CreateSPParameter("@typeposting", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlrpttype.SelectedValue, typeof(string));
        Paramsc[6] = gb.CreateSPParameter("@userteller", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(String));
        Paramsc[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GL_Neracapercobaan_NEW", Paramsc, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
      

    }
    protected void ddltgl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;

        Txtrptperiode.Text = "Periode : " + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");

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
    decimal xjlhdebit = 0, xjlhkredit = 0,xjlhsaldo=0,xjlhsaldoawal=0 ;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            xjlhsaldoawal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JawalBi"));
            xjlhdebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JDebitBi"));
            xjlhkredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JKreditBi"));
            xjlhsaldo += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldoakhir"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lblsaldoawal = (Label)e.Row.FindControl("lblsaldoawal");
            lblsaldoawal.Text = string.Format("{0:N0}", xjlhsaldoawal);

            Label lbljlhdebit = (Label)e.Row.FindControl("lbljlhdebit");
            lbljlhdebit.Text = string.Format("{0:N0}", xjlhdebit);

            Label lbljlhKredit = (Label)e.Row.FindControl("lbljlhKredit");
            lbljlhKredit.Text = string.Format("{0:N0}", xjlhkredit);

            Label lblsaldoakhir = (Label)e.Row.FindControl("lblsaldoakhir");
            lblsaldoakhir.Text = string.Format("{0:N0}", xjlhsaldo);


        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {




            XLWorkbook wb = new XLWorkbook();

            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[8];
            Paramsc[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@KodeGlAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaccountawal.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@DteTglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            Paramsc[4] = gb.CreateSPParameter("@DteTglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
            Paramsc[5] = gb.CreateSPParameter("@typeposting", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlrpttype.SelectedValue, typeof(string));
            Paramsc[6] = gb.CreateSPParameter("@userteller", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlteller.SelectedValue, typeof(String));
            Paramsc[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
             dt2.ListDataset = gb.GetDataJustOutPut("GL_Neracapercobaan_NEW", Paramsc, "constrmf");
            if (dt2.ListDataset.Tables[0].Rows.Count > 0)
            {
                string huruf = "", deltax;
                string huruf2, huruf1;

                huruf = "";
                int abjadlanjut = 65;
                int abjadawal = 64;
                string strAlphaawal = "";
                string strAlphalanjut = "";
                string alpha1 = "";
                string alpha2 = "";
                int x1 = 1;


                string name;

                //DateTime now = DateTime.Parse(txttglhariini.Text);
                var worksheet = wb.Worksheets.Add("SummaryReport");

                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                worksheet.PageSetup.Margins.Top = 0.4;
                worksheet.PageSetup.Margins.Left = 0.3;
                worksheet.PageSetup.Margins.Right = 0.3;
                worksheet.PageSetup.Margins.Bottom = 0.5;

                worksheet.PageSetup.Margins.Header = 0.3;
                worksheet.PageSetup.Margins.Footer = 0.3;
                worksheet.RowHeight = 13;
                string hurufx = "";
                int abjadlanjutx = 65;
                int abjadawalx = 64;
                string strAlphaawalx = "";
                string strAlphalanjutx = "";
                string alpha1x = "";
                string alpha2x = "";

                string xnama;
                string groupx;
                string noperkiraan;
                string ku_mingguan;
                //string tahunini



                worksheet.Cell(7, 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 4).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 5).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 7).Style.Border.TopBorder = XLBorderStyleValues.Thin;
             

                worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
               

                worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
               

                worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(7, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                
                if (ddlrpttype.SelectedValue=="A")
                {
                    worksheet.Cell("A1").Value = "SUMMARY TRIAL BALANCE REPORT SUDAH DIPOSTING";
                }
                else
                {
                    worksheet.Cell("A1").Value = "SUMMARY TRIAL BALANCE REPORT BLM DIPOSTING";
                }
               
                worksheet.Cell("A1").Style.Font.FontSize = 9;
                worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                worksheet.Cell("A1").Style.Font.Bold = true;
                worksheet.Cell("A2").Value = "Tanggal " + Txtrptperiode.Text; ;
                worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                worksheet.Cell("A2").Style.Font.FontSize = 9;
                worksheet.Cell("A2").Style.Font.Bold = true;
                worksheet.Cell("A3").Value = "Cabang : " + ddlbranch.SelectedItem.Text;
                worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                worksheet.Cell("A3").Style.Font.FontSize = 9;
                worksheet.Cell("A3").Style.Font.Bold = true;




                worksheet.Cell("A7").Value = "No.";
                worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                worksheet.Cell("A7").Style.Font.FontSize = 9;
                worksheet.Cell("A7").Style.Font.Bold = true;

                worksheet.Cell("B7").Value = "Account Code";
                worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                worksheet.Cell("B7").Style.Font.FontSize = 9;
                worksheet.Cell("B7").Style.Font.Bold = true;

                worksheet.Cell("C7").Value = "Description";
                worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                worksheet.Cell("C7").Style.Font.FontSize = 9;
                worksheet.Cell("C7").Style.Font.Bold = true;
                worksheet.Cell("D7").Value = "Saldo Awal";
                worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                worksheet.Cell("D7").Style.Font.FontSize = 9;
                worksheet.Cell("D7").Style.Font.Bold = true;
                worksheet.Cell("E7").Value = "Debit";
                worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                worksheet.Cell("E7").Style.Font.FontSize = 9;
                worksheet.Cell("E7").Style.Font.Bold = true;
                worksheet.Cell("F7").Value = "Kredit";
                worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                worksheet.Cell("F7").Style.Font.FontSize = 9;
                worksheet.Cell("F7").Style.Font.Bold = true;
                worksheet.Cell("G7").Value = "Saldo Akhir";
                worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                worksheet.Cell("G7").Style.Font.FontSize = 9;
                worksheet.Cell("G7").Style.Font.Bold = true;

                int b;
                int p;
                b = 7;
                p = 0;

                decimal jawalbidb;
                jawalbidb = 0;
                decimal jawal=0,jdebitbidb=0,jkreditbi = 0;
           


                decimal saldoendingdebit=0, saldoawalminus=0,saldoakhirminus=0;
                
                for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                {


                    b = b + 1;
                    p = p + 1;

                    worksheet.Cell(b, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                  

                    worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
               
                    worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
           
                    worksheet.Cell(b, "A").Value = p;
                    worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeGl"].ToString();
                    worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaGl"].ToString();
                    worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "C").Style.Font.FontSize = 9;


                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JawalBi"].ToString();
                    worksheet.Cell(b, "D").Value = ku_mingguan.Replace(",", ".");
                    worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "D").Style.Font.FontSize = 9;
            
                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JDebitBi"].ToString();
                    worksheet.Cell(b, "E").Value = ku_mingguan.Replace(",", ".");
                    worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["JKreditBi"].ToString();
                    worksheet.Cell(b, "F").Value = ku_mingguan.Replace(",", ".");
                    worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["saldoakhir"].ToString();
                    worksheet.Cell(b, "G").Value = ku_mingguan.Replace(",", ".");
                    worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "G").Style.Font.FontSize = 9;

                    jawalbidb = jawalbidb + decimal.Parse(dt2.ListDataset.Tables[0].Rows[x]["saldoawaldebit"].ToString());
                    saldoendingdebit = saldoendingdebit + decimal.Parse(dt2.ListDataset.Tables[0].Rows[x]["saldoakhirdebit"].ToString());

                    saldoawalminus = saldoawalminus + decimal.Parse(dt2.ListDataset.Tables[0].Rows[x]["saldoawalkredit"].ToString());
                    saldoakhirminus = saldoakhirminus + decimal.Parse(dt2.ListDataset.Tables[0].Rows[x]["saldoakhirKredit"].ToString());






                }

                int pakhir = 0;

                pakhir = 8 + p;

                worksheet.Cell(pakhir, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
               


                worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
               

                worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;









                worksheet.Cell(pakhir + 1, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;



                worksheet.Cell(pakhir + 1, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                worksheet.Cell(pakhir + 1, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 1, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;









                worksheet.Cell(pakhir + 2, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2,2 ).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;



                worksheet.Cell(pakhir + 2, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                worksheet.Cell(pakhir + 2, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir + 2, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                worksheet.Cell(pakhir, "B").Value = "Total Debit";
                worksheet.Cell(pakhir, "B").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir, "B").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir, "B").Style.Font.Bold = true;
                ku_mingguan = jawalbidb.ToString();
                worksheet.Cell(pakhir, "D").Value = ku_mingguan.Replace(",", ".");
                worksheet.Cell(pakhir, "D").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir, "D").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir, "D").Style.Font.Bold = true;

                worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + b + ")"; //debit
                worksheet.Cell(pakhir, "E").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir, "E").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir, "E").Style.Font.Bold = true;
                ku_mingguan = saldoendingdebit.ToString();
                worksheet.Cell(pakhir, "G").Value = ku_mingguan.Replace(",", ".");
                worksheet.Cell(pakhir, "G").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir, "G").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir, "G").Style.Font.Bold = true;

                worksheet.Cell(pakhir + 1, "B").Value = "Total Kredit";
                worksheet.Cell(pakhir + 1, "B").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 1, "B").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 1, "B").Style.Font.Bold = true;

                ku_mingguan = saldoawalminus.ToString();
                worksheet.Cell(pakhir+1, "D").Value = ku_mingguan.Replace(",", ".");
                worksheet.Cell(pakhir + 1, "D").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 1, "D").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 1, "D").Style.Font.Bold = true;

                worksheet.Cell(pakhir+1, "F").FormulaA1 = "=SUM(F8:F" + b + ")";//kredit
                worksheet.Cell(pakhir + 1, "F").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 1, "F").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 1, "F").Style.Font.Bold = true;

                ku_mingguan =  saldoakhirminus.ToString();
                worksheet.Cell(pakhir+1, "G").Value = ku_mingguan.Replace(",", ".");
                worksheet.Cell(pakhir + 1, "G").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 1, "G").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 1, "G").Style.Font.Bold = true;


                worksheet.Cell(pakhir + 2, "B").Value = "Total";
                worksheet.Cell(pakhir + 2, "B").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 2, "B").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 2, "B").Style.Font.Bold = true;



                ku_mingguan = (jawalbidb + saldoawalminus).ToString();
                worksheet.Cell(pakhir+2, "D").Value = ku_mingguan.Replace(",", ".");
                worksheet.Cell(pakhir + 2, "D").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 2, "D").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 2, "D").Style.Font.Bold = true;
                worksheet.Cell(pakhir+2, "E").FormulaA1 = "=SUM(E8:E" + b + ")"; //debit
                worksheet.Cell(pakhir + 2, "E").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 2, "E").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 2, "E").Style.Font.Bold = true;
                worksheet.Cell(pakhir+2, "F").FormulaA1 = "=SUM(F8:F" + b + ")";//kredit
                worksheet.Cell(pakhir + 2, "F").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 2, "F").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 2, "F").Style.Font.Bold = true;
                ku_mingguan = (saldoendingdebit + saldoakhirminus).ToString();
                worksheet.Cell(pakhir+2, "G").Value = ku_mingguan.Replace(",", ".");
                worksheet.Cell(pakhir + 2, "G").Style.Font.FontName = "Calibri";
                worksheet.Cell(pakhir + 2, "G").Style.Font.FontSize = 9;
                worksheet.Cell(pakhir + 2, "G").Style.Font.Bold = true;

                //worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G8:H" + b + ")";







                worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(5).Style.NumberFormat.Format = "_(#,##0_)";
                worksheet.Column(4).Style.NumberFormat.Format = "_(#,##0_)";
                //worksheet.Column(13).Style.NumberFormat.Format = "dd/mm/yyyy";

                //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                worksheet.Column(1).Width = 4;
                worksheet.Column(2).Width = 10.43;
                worksheet.Column(3).Width = 24.57;
                worksheet.Column(4).Width = 15.86;
                worksheet.Column(5).Width = 15.86;
                worksheet.Column(6).Width = 15.86;
                worksheet.Column(7).Width = 15.86;
              







                worksheet.Row(1).Height = 12;
                worksheet.Row(2).Height = 12;

                worksheet.Row(3).Height = 12;
                worksheet.Row(4).Height = 12;

                worksheet.Row(5).Height = 12;
                worksheet.Row(6).Height = 12;
                worksheet.Row(7).Height = 12;
              

                worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                worksheet.PageSetup.Margins.Top = 0.11811;
                worksheet.PageSetup.Margins.Left = 0.314961;
                worksheet.PageSetup.Margins.Right = 0.314961;
                worksheet.PageSetup.Margins.Bottom = 0.11811;

                worksheet.PageSetup.Margins.Header = 0;
                worksheet.PageSetup.Margins.Footer = 0;

                // Set each particular border for the cell.

                //int ro = 1;
                //int co = 8;
                ////worksheet.Cell(pakhir, "E").

                //worksheet.Cell(ro, co).Style.Border.BottomBorder = XLBorderStyleValues.Thin;













            }


            string report = "";

            report = "Report Summary Transaction " + Txtrptperiode.Text + ".xlsx";

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + report + "");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
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
        Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        Txtrptperiode.Enabled = false;
        ddlSP.Enabled = true;


    }



}