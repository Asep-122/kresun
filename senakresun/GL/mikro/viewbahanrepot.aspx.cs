using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ClosedXML.Excel;
public partial class Report_viewbahanrepot : System.Web.UI.Page
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


    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    GlobalEntities dt2 = new GlobalEntities();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //BindGrddatawilayahEntity();

                //DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                //var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KOPERASI", typeof(string));
                //var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var3[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var3, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                //ddlkoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlkoperasi.DataTextField = "nama_koperasi";
                //ddlkoperasi.DataValueField = "kode_koperasi";
                //ddlkoperasi.DataBind();
                //ddlkoperasi.Items.Insert(0, new ListItem("ALL", "ALL"));
                ddlkoperasi.Enabled = false;


                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KOPERASI", typeof(string));
                var3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var3[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlkoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkoperasi.DataTextField = "Text";
                ddlkoperasi.DataValueField = "value";
                ddlkoperasi.DataBind();
                //ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddlcabang.Enabled = true;
                var Koperasi = ddlkoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();



                
                DefaultClass.Global.SqlHelper[] vart = new DefaultClass.Global.SqlHelper[4];
                vart[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AREA", typeof(string));
                vart[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                vart[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                vart[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, "-", typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login", vart, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlarea.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlarea.DataTextField = "Text";
                ddlarea.DataValueField = "value";
                ddlarea.DataBind();


                ddlarea.Enabled = false;

                ambilTahun();
                ambilBulan();

                if (SessBranchID == "000")
                {
                    ddltypereport.SelectedValue = "1";
                    ddltypereport.Enabled = true;
                    ddlkoperasi.Enabled = true;
                    ddlarea.Enabled = false;
                    ddlcabang.Enabled = false;
                }
                else
                {
                    ddltypereport.SelectedValue = "3";
                    ddltypereport.Enabled = false;
                    ddlkoperasi.Enabled = false;
                    ddlarea.Enabled = false;
                    ddlcabang.Enabled = false;
                }


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
            jmlbaru += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
            jmltotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "terima"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotal = (Label)e.Row.FindControl("lblTotal");
            lbltotal.Text = Total.ToString();
            Label lbljmlbaru = (Label)e.Row.FindControl("lbljmlbaru");
            lbljmlbaru.Text = jmlbaru.ToString("#,##0.00");

            Label lblterimabaru = (Label)e.Row.FindControl("lblterimabaru");
            lblterimabaru.Text = jmltotal.ToString("#,##0.00");


        }
    }
    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 3; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
        ddlTahun.SelectedValue = currentYear.ToString();

            ddlTahun.Enabled = true;
       
    }

    public void ambilBulan()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length; i++)
        {
            ddlbulan.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
        }
        var currentmonth = DateTime.Today.Month - 1;
        ddlbulan.SelectedValue = currentmonth.ToString();

            ddlbulan.Enabled = true;
  
    }

   

    protected void ddltypereport_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddltypereport.SelectedValue == "1")

        {
            ddlkoperasi.Enabled = true;
            ddlarea.Enabled = false;
            ddlcabang.Enabled = false;
        }
        else if(ddltypereport.SelectedValue == "2")
        {
            ddlkoperasi.Enabled = false;
            ddlarea.Enabled = true;
            ddlcabang.Enabled = false;

        }
        else
        {
            ddlkoperasi.Enabled = false;
            ddlarea.Enabled = false;
            ddlcabang.Enabled = true;
        }



    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {

            if (DdlReport.SelectedValue == "6")
            {

                var TypeReport = ddltypereport.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@typereport", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltypereport.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkoperasi.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlarea.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {

                    XLWorkbook wb = new XLWorkbook();
                    for (int i = 0; i < ge.ListDataset.Tables[0].Rows.Count; i++)
                    {

                        string name;
                        //name=dt.Rows[i][0].;
                        name = ge.ListDataset.Tables[0].Rows[i][1].ToString();
                        var worksheet = wb.Worksheets.Add(name);
                        worksheet.RowHeight = 13;




                        string xnama;
                        string ku_mingguan;
                        string groupx;
                        string noperkiraan;


                        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[9];
                        varx[0] = gb.CreateSPParameter("@typereport", ParameterDirection.Input, SqlDbType.VarChar, 1, ddltypereport.SelectedValue, typeof(string));
                        varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIBAHANREPORT", typeof(string));
                        varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varx[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ge.ListDataset.Tables[0].Rows[i][0].ToString(), typeof(string));
                        varx[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkoperasi.SelectedValue, typeof(string));
                        varx[6] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlarea.SelectedValue, typeof(string));
                        varx[7] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                        varx[8] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));

                        dt2.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", varx, "constrmf");
                        if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                        {



                            //Add DataTable as Worksheet.
                            worksheet.Cell("A1").Value = dt2.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString().ToUpper() + " - " + dt2.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString().ToUpper() + " - " + dt2.ListDataset.Tables[0].Rows[0]["tahun"].ToString().ToUpper();
                            worksheet.Cell("A1").Style.Font.FontSize = 14;
                            worksheet.Cell("A1").Style.Font.FontName = "ARIAL";
                            worksheet.Cell("A1").Style.Font.Bold = true;

                            worksheet.Cell("A2").Value = "AKTIVA";
                            worksheet.Cell("A2").Style.Font.FontName = "ARIAL";
                            worksheet.Cell("A2").Style.Font.FontSize = 14;
                            worksheet.Cell("A2").Style.Font.Bold = true;


                            string tahun, tahunlalu;
                            tahun = ddlTahun.SelectedValue;
                            tahunlalu = (int.Parse(tahun) - 1).ToString();
                            tahun = tahun.Substring(2, 2);
                            tahunlalu = tahunlalu.Substring(2, 2);



                            if (ddlbulan.SelectedValue == "1")
                            {
                                worksheet.Cell("B3").Value = "'Jan-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "2")
                            {


                                worksheet.Cell("B3").Value = "'Feb-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "3")
                            {

                                worksheet.Cell("B3").Value = "'Mar-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "4")
                            {

                                worksheet.Cell("B3").Value = "'Apr-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "5")
                            {

                                worksheet.Cell("B3").Value = "'May-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "6")
                            {

                                worksheet.Cell("B3").Value = "'Jun-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "7")
                            {

                                worksheet.Cell("B3").Value = "'Jul-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "8")
                            {

                                worksheet.Cell("B3").Value = "'Agu-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;


                            }
                            else if (ddlbulan.SelectedValue == "9")
                            {

                                worksheet.Cell("B3").Value = "'Sep-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "10")
                            {

                                worksheet.Cell("B3").Value = "'Oct-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "11")
                            {

                                worksheet.Cell("B3").Value = "'Nov-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }
                            else if (ddlbulan.SelectedValue == "12")
                            {

                                worksheet.Cell("B3").Value = "'Dec-" + tahun;
                                worksheet.Cell("B3").Style.Font.FontName = "ARIAL";
                                worksheet.Cell("B3").Style.Font.Italic = true;
                                worksheet.Cell("B3").Style.Font.FontSize = 10;

                            }


                            worksheet.Range("B20:B20").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B27:B27").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B35:B35").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B41:B41").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B44:B44").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B63:B63").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B72:B72").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B85:B85").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B88:B88").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B97:B97").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B105:B105").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B114:B114").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B123:B123").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B126:B126").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B146:B146").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B154:B154").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B159:B159").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B162:B162").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Range("B164:B164").Style.Border.TopBorder = XLBorderStyleValues.Thin;




                            int b;
                            b = 4;


                            for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                            {

                                noperkiraan = dt2.ListDataset.Tables[0].Rows[x]["perkiraan_bahan"].ToString();
                                if (noperkiraan == "810000")
                                {
                                    b = 168;
                                }
                                if (noperkiraan == "311200")
                                {
                                    b = b + 1;
                                }

                                if (b == 48 || b == 90 || b == 128)
                                {


                                    if (ddlbulan.SelectedValue == "1")
                                    {
                                        worksheet.Cell(b, "B").Value = "'Jan-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "2")
                                    {


                                        worksheet.Cell(b, "B").Value = "'Feb-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "3")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Mar-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "4")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Apr-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "5")
                                    {

                                        worksheet.Cell(b, "B").Value = "'May-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "6")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Jun-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "7")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Jul-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "8")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Agu-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;


                                    }
                                    else if (ddlbulan.SelectedValue == "9")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Sep-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "10")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Oct-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "11")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Nov-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "12")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Dec-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }







                                }


                                if (b == 47)
                                {
                                    worksheet.Cell(b, "A").Value = "PASIVA";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 12;
                                    worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "ARIAL";


                                }
                                else if (b == 89)
                                {
                                    worksheet.Cell(b, "A").Value = "PROFIT  &  LOSS  CUMULATIVE";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 12;
                                    worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "ARIAL";
                                }

                                else if (b == 168)
                                {
                                    b = b + 1;
                                    worksheet.Cell(b, "A").Value = "DATA - DATA PENUNJANG";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 14;
                                    worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "ARIAL";

                                    b = b + 2;
                                    if (ddlbulan.SelectedValue == "1")
                                    {
                                        worksheet.Cell(b, "B").Value = "'Jan-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "2")
                                    {


                                        worksheet.Cell(b, "B").Value = "'Feb-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "3")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Mar-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "4")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Apr-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "5")
                                    {

                                        worksheet.Cell(b, "B").Value = "'May-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "6")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Jun-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "7")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Jul-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "8")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Agu-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;


                                    }
                                    else if (ddlbulan.SelectedValue == "9")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Sep-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "10")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Oct-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "11")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Nov-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }
                                    else if (ddlbulan.SelectedValue == "12")
                                    {

                                        worksheet.Cell(b, "B").Value = "'Dec-" + tahun;
                                        worksheet.Cell(b, "B").Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, "B").Style.Font.Italic = true;
                                        worksheet.Cell(b, "B").Style.Font.FontSize = 10;

                                    }



                                    b = b + 2;
                                    worksheet.Cell(b, "A").Value = "1. JUMLAH PETUGAS";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 12;
                                }

                                else
                                {


                                    if (dt2.ListDataset.Tables[0].Rows[x]["type_font"].ToString() == "B")
                                    {
                                        worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    }


                                    //worksheet.Cell(b, "A").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_bahan"].ToString();

                                    worksheet.Cell(b, "A").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_bahan"].ToString();
                                    worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 11;

                                    xnama = dt2.ListDataset.Tables[0].Rows[x]["kategori"].ToString();

                                    if (xnama == "M" || xnama == "U")
                                    {


                                        if (b == 21)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B5:B19)";
                                        }
                                        else if (b == 28)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B24:B27)";
                                        }
                                        else if (b == 36)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B31:B35)";
                                        }
                                        else if (b == 42)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B39:B41)";
                                        }
                                        else if (b == 44)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B21+B28+B36+B42";
                                        }
                                        else if (b == 64)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B50:B62)";
                                        }
                                        else if (b == 73)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B67:B72)";
                                        }
                                        else if (b == 86)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B76:B85)";
                                        }
                                        else if (b == 88)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B64+B73+B86";
                                        }
                                        else if (b == 97)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B93:B96)";
                                        }
                                        else if (b == 105)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B100:B104)";
                                        }
                                        else if (b == 106)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B97-B105";
                                        }
                                        else if (b == 114)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B110:B113)";
                                        }
                                        else if (b == 123)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B117:B122)";
                                        }
                                        else if (b == 125)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B114-B123";
                                        }
                                        else if (b == 126)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B106+B125";
                                        }
                                        else if (b == 146)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B130:B145)";
                                        }
                                        else if (b == 148)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B126-B146";
                                        }
                                        else if (b == 154)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B151:B153)";
                                        }
                                        else if (b == 155)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B148-B154";
                                        }
                                        else if (b == 159)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=SUM(B157:B158)";
                                        }
                                        else if (b == 160)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B155+B159";
                                        }
                                        else if (b == 162)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B160+B161";
                                        }
                                        else if (b == 164)
                                        {
                                            worksheet.Cell(b, "B").FormulaA1 = "=B162-B163";
                                        }

                                        else if (b == 187)
                                        {

                                            worksheet.Cell(b, "B").FormulaA1 = "= B184 / (B13 + B14) * 100";


                                        }
                                        else if (b == 190)
                                        {

                                            worksheet.Cell(b, "B").FormulaA1 = "=-B16/(B13+B14)*100";


                                        }
                                        else if (b == 193)
                                        {

                                            worksheet.Cell(b, "B").FormulaA1 = "=B187-B190";


                                        }
                                        else if (b == 196)
                                        {

                                            worksheet.Cell(b, "B").FormulaA1 = "=B193*(B13+B14)/((B13+B14)+B16)";


                                        }
                                        else if (b == 84)
                                        {

                                            worksheet.Cell(b, "B").FormulaA1 = "=B164";


                                        }



                                        else
                                        {

                                            ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["saldo_akhir"].ToString();

                                            worksheet.Cell(b, "B").Value = ku_mingguan.Replace(",", ".");
                                        }
                                    }

                                }

                                b = b + 1;
                                worksheet.Column(1).Width = 61.86;
                                worksheet.Column(2).Width = 14.29;
                                worksheet.Column(2).Style.NumberFormat.Format = "_(#,##0_)";
                                worksheet.Cell(187, "B").Style.NumberFormat.Format = "##.00";
                                worksheet.Cell(190, "B").Style.NumberFormat.Format = "##.00";
                                worksheet.Cell(193, "B").Style.NumberFormat.Format = "##.00";
                                worksheet.Cell(196, "B").Style.NumberFormat.Format = "##.00";

                            }




                        }



                       




                    }

                    string report = "";
                    if (ddltypereport.SelectedValue == "1")
                    {
                        report = "Report This Month Koperasi " + ddlbulan.SelectedValue + "-" + ddlTahun.SelectedValue + "-" + ddlkoperasi.SelectedItem.Text + ".xlsx";
                    }
                    else if (ddltypereport.SelectedValue == "2")
                    {
                        report = "Report This Month Area " + ddlbulan.SelectedValue + "-" + ddlTahun.SelectedValue + "-" + ddlarea.SelectedItem.Text + ".xlsx";
                    }
                    else if (ddltypereport.SelectedValue == "3")
                    {
                        report = "Report This Month Cabang " + ddlbulan.SelectedValue + "-" + ddlTahun.SelectedValue + "-" + ddlcabang.SelectedItem.Text + ".xlsx";
                    }

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
                else
                {


                }









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





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
   


}