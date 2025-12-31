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

public partial class Report_viewlistAnggota : System.Web.UI.Page
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



                //DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
                //ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");

                //ddlproduk.SelectedValue = "3C";
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


                // COMBO MASTER MO
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_REPORT_KODEMARKETING", var1, "constrmf");

                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "namaAO";
                ddlmarketing.DataValueField = "NIK";
                ddlmarketing.DataBind();




                txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");

               




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
    decimal Total = 0, jmlbaru = 0, plafonbaru = 0, jmllama = 0, plafonlama = 0, jmltotal = 0, plafontotal = 0;
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


    protected void ddlmarketing_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlreport.SelectedValue == "1")
        {
            ddlmarketing.Enabled = true;
        }
        else
        {
            ddlmarketing.Enabled = false;
        }

    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {


            if (ddlreport.SelectedValue=="1")
            {


                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

                //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                //var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MARKETING", typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                
                //ge.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", var, "constrmf");
                //if (ge.ListDataset.Tables[0].Rows.Count > 0)
                //{

                //    XLWorkbook wb = new XLWorkbook();
                //    for (int i = 0; i < ge.ListDataset.Tables[0].Rows.Count; i++)
                //    {

                      


                        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[6];
                        varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                        varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        if (ddlstatus.SelectedValue == "1")
                        {
                            varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTANGGOTAAKTIFDETAIL", typeof(string));
                        }
                        else if(ddlstatus.SelectedValue == "2")
                        {
                            varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTANGGOTANONAKTIFDETAIL", typeof(string));

                        }
                        else if (ddlstatus.SelectedValue == "3")
                        {
                            varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTANGGOTAWODETAIL", typeof(string));

                        }

                        varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varx[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        dt2.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", varx, "constrmf");
                        if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                        {
                            string huruf = "", deltax;
                            string huruf2, huruf1;
                            XLWorkbook wb = new XLWorkbook();
                            huruf = "";
                            int abjadlanjut = 65;
                            int abjadawal = 64;
                            string strAlphaawal = "";
                            string strAlphalanjut = "";
                            string alpha1 = "";
                            string alpha2 = "";
                            int x1 = 1;


                            string name;
                            //name=dt.Rows[i][0].;
                            name = ddlmarketing.SelectedItem.Text;


                            var worksheet = wb.Worksheets.Add(name);
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
                      

                            worksheet.Cell("A1").Value = "LAPORAN LIST ANGGOTA MARKETING ";
                                worksheet.Cell("A1").Style.Font.FontSize = 9;
                                worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                                worksheet.Cell("A2").Value = "Periode";
                                worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                                worksheet.Cell("A2").Style.Font.FontSize = 9;
                                worksheet.Cell("A3").Value = "Status Anggota"; 
                                worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                                worksheet.Cell("A3").Style.Font.FontSize = 9;
                                worksheet.Cell("A4").Value = "Cabang";
                                worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                                worksheet.Cell("A4").Style.Font.FontSize = 9;
                                worksheet.Cell("A5").Value = "Marketing";
                                worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                                worksheet.Cell("A5").Style.Font.FontSize = 9;

                            worksheet.Cell("C2").Value = txtTglL.Text;
                            worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                            worksheet.Cell("C2").Style.Font.FontSize = 9;
                            worksheet.Cell("A3").Value = ddlstatus.SelectedItem.Text;
                            worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A3").Style.Font.FontSize = 9;
                            worksheet.Cell("A4").Value = ddlcabang.SelectedItem.Text;
                            worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A4").Style.Font.FontSize = 9;
                            worksheet.Cell("A5").Value = ddlmarketing.SelectedItem.Text;
                            worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A5").Style.Font.FontSize = 9;


                            worksheet.Cell("A7").Value = "No.";
                            worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A7").Style.Font.FontSize = 9;

                            worksheet.Cell("B7").Value = "No. Anggota";
                            worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("B7").Style.Font.FontSize = 9;


                            worksheet.Cell("C7").Value = "Nama Anggota";
                            worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("C7").Style.Font.FontSize = 9;

                            worksheet.Cell("D7").Value = "Tgl Anggota";
                            worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("D7").Style.Font.FontSize = 9;

                            worksheet.Cell("E7").Value = "No. Identitas";
                            worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("E7").Style.Font.FontSize = 9;

                            worksheet.Cell("F7").Value = "Alamat";
                            worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("F7").Style.Font.FontSize = 9;

                            worksheet.Cell("G7").Value = "No. HP";
                            worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("G7").Style.Font.FontSize = 9;

                           

                          


                            int b;
                                int p;
                                b = 7;
                                p = 0;


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


                                    worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_nasabah"].ToString();
                                    worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                                    worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                                    worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                                    worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["register_date"].ToString();
                                    worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "D").Style.Font.FontSize = 9;

                                worksheet.Cell(b, "E").Value = "'" +dt2.ListDataset.Tables[0].Rows[x]["no_identitas"].ToString();
                                worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                                worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["alamat"].ToString();
                                worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                                worksheet.Cell(b, "G").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_hp"].ToString();
                                worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "G").Style.Font.FontSize = 9;

                              



                                    worksheet.Row(b).Height = 12;
                                  



                            }

                                int pakhir = 0;

                                pakhir = 8 + p;

                        //worksheet.Cell(pakhir, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                       


                        //    worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                      

                        //    worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        //worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    

                        //        worksheet.Cell(pakhir, "D").Value = "Total";
                        //        worksheet.Cell(pakhir, "D").Style.Font.FontName = "Calibri";
                        //        worksheet.Cell(pakhir, "D").Style.Font.FontSize = 9;
                        //        worksheet.Cell(pakhir, "I").Style.Font.FontName = "Calibri";
                        //        worksheet.Cell(pakhir, "I").Style.Font.FontSize = 9;
                        //        worksheet.Cell(pakhir, "J").Style.Font.FontName = "Calibri";
                        //        worksheet.Cell(pakhir, "J").Style.Font.FontSize = 9;
                        //        worksheet.Cell(pakhir, "L").Style.Font.FontName = "Calibri";
                        //        worksheet.Cell(pakhir, "L").Style.Font.FontSize = 9;
                        //        worksheet.Cell(pakhir, "K").Style.Font.FontName = "Calibri";
                        //        worksheet.Cell(pakhir, "K").Style.Font.FontSize = 9;


                        //        worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I8:I" + b + ")";
                        //        worksheet.Cell(pakhir, "J").FormulaA1 = "=SUM(J8:J" + b + ")";
                        //        worksheet.Cell(pakhir, "K").FormulaA1 = "=SUM(K8:K" + b + ")";
                        //        worksheet.Cell(pakhir, "L").FormulaA1 = "=SUM(L8:L" + b + ")";




                        //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(11).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(12).Style.NumberFormat.Format = "_(#,##0_)";
                        //worksheet.Column(7).Style.NumberFormat.Format = "dd/mm/yyyy";
                         worksheet.Column(4).Style.NumberFormat.Format = "dd/mm/yyyy";
                        //    //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                        //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                        //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                                worksheet.Column(1).Width = 4;
                                worksheet.Column(2).Width = 10.43;
                                worksheet.Column(3).Width = 19.86;
                                worksheet.Column(4).Width = 8.71;
                                worksheet.Column(5).Width = 14.29;
                                worksheet.Column(6).Width = 24.57;
                                worksheet.Column(7).Width = 13.29;
                               






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

                    string report = "";
                    if (ddlstatus.SelectedValue == "1")
                    {
                        report = "Report Anggota Aktif " + ddlmarketing.SelectedItem.Text.ToString() + ".xlsx";
                    }
                    else if (ddlstatus.SelectedValue == "2")
                    {
                        report = "Report Anggota Non Aktif " + ddlmarketing.SelectedItem.Text.ToString() + ".xlsx";
                    }
                    else if (ddlstatus.SelectedValue == "3")
                    {
                        report = "Report Anggota WO " + ddlmarketing.SelectedItem.Text.ToString() + ".xlsx";
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



            }
            else if(ddlreport.SelectedValue == "3")
            {


                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());


                XLWorkbook wb = new XLWorkbook();
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

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[5];
                varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, "ALL", typeof(string));
                varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ANGGOTAAKTIFREKAPB", typeof(string));
                varx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                varx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                dt2.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", varx, "constrmf");
                if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                {



                    string name;
                    //name=dt.Rows[i][0].;
                    name = "REKAP PRODUK BULANAN";
                    var worksheet = wb.Worksheets.Add(name);
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
               


                    worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
               

                    worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                



                    worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                  

                    worksheet.Cell("A1").Value = "LAPORAN LIST REKAP PRODUK BULANAN ";
                    worksheet.Cell("A1").Style.Font.FontSize = 9;
                    worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Value = "Periode";
                    worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A2").Style.Font.FontSize = 9;
                    worksheet.Cell("A3").Value = "Status Anggota";
                    worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A3").Style.Font.FontSize = 9;
                    worksheet.Cell("A4").Value = "Cabang";
                    worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A4").Style.Font.FontSize = 9;
                    worksheet.Cell("A5").Value = "Marketing";
                    worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A5").Style.Font.FontSize = 9;



                    worksheet.Cell("C2").Value = "'" + txtTglL.Text;

                    worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C2").Style.Font.FontSize = 9;
                    worksheet.Cell("C3").Value = ddlstatus.SelectedItem.Text;
                    worksheet.Cell("C3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C3").Style.Font.FontSize = 9;
                    worksheet.Cell("C4").Value = ddlcabang.SelectedItem.Text;
                    worksheet.Cell("C4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C4").Style.Font.FontSize = 9;
                    worksheet.Cell("C5").Value = "All - All";
                    worksheet.Cell("C5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C5").Style.Font.FontSize = 9;


                    worksheet.Cell("A7").Value = "No.";
                    worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("A7").Style.Font.FontSize = 9;

                    worksheet.Cell("B7").Value = "Marketing";
                    worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B7").Style.Font.FontSize = 9;


                    worksheet.Cell("C7").Value = "No. Anggota";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;


                    worksheet.Cell("D7").Value = "Nama Anggota";
                    worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("D7").Style.Font.FontSize = 9;


                    worksheet.Cell("F7").Value = "Tgl Rekening";
                    worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("F7").Style.Font.FontSize = 9;

                    worksheet.Cell("E7").Value = "No. Rekening";
                    worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("E7").Style.Font.FontSize = 9;

               

                    int b;
                    int p;
                    b = 7;
                    p = 0;


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
                     


                        worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                   

                        worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                       



                        worksheet.Cell(b, "A").Value = p;
                        worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["kd_staff"].ToString();
                        worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_nasabah"].ToString();
                        worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                        worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "D").Style.Font.FontSize = 9;


                        

                        worksheet.Cell(b, "E").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_rekening"].ToString();
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "F").Value = dt2.ListDataset.Tables[0].Rows[x]["tgl_drop"].ToString();
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;

       



                        worksheet.Row(b).Height = 12;




                    }

                    int pakhir = 0;

                    pakhir = 8 + p;

                    //    worksheet.Cell(pakhir, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(pakhir, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    //    worksheet.Cell(pakhir, "B").Value = "Total";
                    //    worksheet.Cell(pakhir, "B").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "B").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "C").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "C").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "D").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "D").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "E").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "E").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "F").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "F").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "G").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "G").Style.Font.FontSize = 9;





                    //    worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + b + ")";
                    //    worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + b + ")";
                    //    worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G8:G" + b + ")";





                    //    worksheet.Column(5).Style.NumberFormat.Format = "_(#,##0_)";
                    //    worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                    //    worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";

                    ////worksheet.Column(13).Style.NumberFormat.Format = "dd/mm/yyyy";

                    ////worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                    ////worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                    ////worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                    //worksheet.Column(1).Width = 4;
                    //worksheet.Column(2).Width = 19.43;
                    //worksheet.Column(3).Width = 14.71;
                    //worksheet.Column(4).Width = 24.14;
                    //worksheet.Column(5).Width = 10.14;
                    //worksheet.Column(6).Width = 9.57;
                    //worksheet.Column(7).Width = 12.71;

                    worksheet.Column(6).Style.NumberFormat.Format = "dd/mm/yyyy";
                    //    //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                    //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                    //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                    worksheet.Column(1).Width = 4;
                    worksheet.Column(3).Width = 8.43;
                    worksheet.Column(3).Width = 10.43;
                    worksheet.Column(4).Width = 16.29;
                    worksheet.Column(5).Width = 8.71;
                    worksheet.Column(6).Width = 14.29;
                  






                    worksheet.Row(1).Height = 12;
                    worksheet.Row(2).Height = 12;

                    worksheet.Row(3).Height = 12;
                    worksheet.Row(4).Height = 12;

                    worksheet.Row(5).Height = 12;
                    worksheet.Row(6).Height = 12;
                  


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

                
                    report = "Report Rekap Produk Bulanan" + xtanggal.ToString() + ".xlsx";
               


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


                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());


                XLWorkbook wb = new XLWorkbook();
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

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[6];
                    varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, "ALL", typeof(string));
                    varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));

                if (ddlstatus.SelectedValue == "1")
                {
                    varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ANGGOTAAKTIFREKAP", typeof(string));
                }
                else if (ddlstatus.SelectedValue == "2")
                {
                    varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ANGGOTANONAKTIFREKAP", typeof(string));

                }
                else if (ddlstatus.SelectedValue == "3")
                {
                    varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ANGGOTAWOREKAP", typeof(string));

                }

                //varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CADANGANREKAP", typeof(string));
                varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varx[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    dt2.ListDataset = gb.GetDataJustOutPut("AR_ANGGOTA", varx, "constrmf");
                    if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        


                        string name;
                        //name=dt.Rows[i][0].;
                        name = "REKAP ANGGOTA";


                        var worksheet = wb.Worksheets.Add(name);
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
                        worksheet.Cell(7,8).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(7, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell("A1").Value = "LAPORAN LIST REKAP ANGGOTA MARKETING ";
                        worksheet.Cell("A1").Style.Font.FontSize = 9;
                        worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                        worksheet.Cell("A2").Value = "Periode";
                        worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                        worksheet.Cell("A2").Style.Font.FontSize = 9;
                        worksheet.Cell("A3").Value = "Status Anggota";
                        worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                        worksheet.Cell("A3").Style.Font.FontSize = 9;
                        worksheet.Cell("A4").Value = "Cabang";
                        worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                        worksheet.Cell("A4").Style.Font.FontSize = 9;
                        worksheet.Cell("A5").Value = "Marketing";
                        worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                        worksheet.Cell("A5").Style.Font.FontSize = 9;


                        
                         worksheet.Cell("C2").Value = "'" + txtTglL.Text;

                        worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                        worksheet.Cell("C2").Style.Font.FontSize = 9;
                        worksheet.Cell("C3").Value = ddlstatus.SelectedItem.Text;
                        worksheet.Cell("C3").Style.Font.FontName = "Calibri";
                        worksheet.Cell("C3").Style.Font.FontSize = 9;
                        worksheet.Cell("C4").Value = ddlcabang.SelectedItem.Text;
                        worksheet.Cell("C4").Style.Font.FontName = "Calibri";
                        worksheet.Cell("C4").Style.Font.FontSize = 9;
                        worksheet.Cell("C5").Value = "All - All";
                        worksheet.Cell("C5").Style.Font.FontName = "Calibri";
                        worksheet.Cell("C5").Style.Font.FontSize = 9;


                        worksheet.Cell("A7").Value = "No.";
                        worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                        worksheet.Cell("A7").Style.Font.FontSize = 9;

                        worksheet.Cell("B7").Value = "Marketing";
                        worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                        worksheet.Cell("B7").Style.Font.FontSize = 9;


                    worksheet.Cell("C7").Value = "No. Anggota";
                    worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("C7").Style.Font.FontSize = 9;


                    worksheet.Cell("D7").Value = "Nama Anggota";
                    worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("D7").Style.Font.FontSize = 9;

                    worksheet.Cell("E7").Value = "Tgl Anggota";
                    worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("E7").Style.Font.FontSize = 9;

                    worksheet.Cell("F7").Value = "No. Identitas";
                    worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("F7").Style.Font.FontSize = 9;

                    worksheet.Cell("G7").Value = "Alamat";
                    worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("G7").Style.Font.FontSize = 9;

                    worksheet.Cell("H7").Value = "No. HP";
                    worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                    worksheet.Cell("H7").Style.Font.FontSize = 9;




                    int b;
                        int p;
                        b = 7;
                        p = 0;


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
                        worksheet.Cell(b, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                        worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                             worksheet.Cell(b, "A").Value = p;
                            worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                            worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                            worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["marketing"].ToString();
                            worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                            worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                            worksheet.Cell(b, "C").Value ="'" + dt2.ListDataset.Tables[0].Rows[x]["no_nasabah"].ToString();
                            worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                            worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                            worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                            worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                            worksheet.Cell(b, "D").Style.Font.FontSize = 9;


                        worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["register_date"].ToString();
                        worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "F").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_identitas"].ToString();
                        worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["alamat"].ToString();
                        worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "G").Style.Font.FontSize = 9;

                        worksheet.Cell(b, "H").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_hp"].ToString();
                        worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                        worksheet.Cell(b, "H").Style.Font.FontSize = 9;




                        worksheet.Row(b).Height = 12;




                        }

                        int pakhir = 0;

                        pakhir = 8 + p;

                    //    worksheet.Cell(pakhir, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(pakhir, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    //worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    //    worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                      
                    //    worksheet.Cell(pakhir, "B").Value = "Total";
                    //    worksheet.Cell(pakhir, "B").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "B").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "C").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "C").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "D").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "D").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "E").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "E").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "F").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "F").Style.Font.FontSize = 9;
                    //    worksheet.Cell(pakhir, "G").Style.Font.FontName = "Calibri";
                    //    worksheet.Cell(pakhir, "G").Style.Font.FontSize = 9;
                       



                       
                    //    worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + b + ")";
                    //    worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + b + ")";
                    //    worksheet.Cell(pakhir, "G").FormulaA1 = "=SUM(G8:G" + b + ")";
                      




                    //    worksheet.Column(5).Style.NumberFormat.Format = "_(#,##0_)";
                    //    worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                    //    worksheet.Column(7).Style.NumberFormat.Format = "_(#,##0_)";
                  
                    ////worksheet.Column(13).Style.NumberFormat.Format = "dd/mm/yyyy";

                    ////worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                    ////worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                    ////worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                        //worksheet.Column(1).Width = 4;
                        //worksheet.Column(2).Width = 19.43;
                        //worksheet.Column(3).Width = 14.71;
                        //worksheet.Column(4).Width = 24.14;
                        //worksheet.Column(5).Width = 10.14;
                        //worksheet.Column(6).Width = 9.57;
                        //worksheet.Column(7).Width = 12.71;

                    worksheet.Column(5).Style.NumberFormat.Format = "dd/mm/yyyy";
                    //    //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                    //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                    //    //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                    worksheet.Column(1).Width = 4;
                    worksheet.Column(3).Width = 8.43;
                    worksheet.Column(3).Width = 10.43;
                    worksheet.Column(4).Width = 16.29;
                    worksheet.Column(5).Width = 8.71;
                    worksheet.Column(6).Width = 14.29;
                    worksheet.Column(7).Width = 20.43;
                    worksheet.Column(8).Width = 11.86;






                    worksheet.Row(1).Height = 12;
                        worksheet.Row(2).Height = 12;

                        worksheet.Row(3).Height = 12;
                        worksheet.Row(4).Height = 12;

                        worksheet.Row(5).Height = 12;
                        worksheet.Row(6).Height = 12;
                        worksheet.Row(7).Height = 12;
                    worksheet.Row(8).Height = 12;


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
               
                if (ddlstatus.SelectedValue=="1")
                {
                    report = "Report Rekap Anggota Aktif" + xtanggal.ToString() + ".xlsx";
                }
                else if (ddlstatus.SelectedValue == "2")
                {
                    report = "Report Rekap Anggota NON Aktif" + xtanggal.ToString() + ".xlsx";
                }
                else if (ddlstatus.SelectedValue == "3")
                {
                    report = "Report Rekap Anggota WO" + xtanggal.ToString() + ".xlsx";
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