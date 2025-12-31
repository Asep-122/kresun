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

public partial class Report_viewlisttunggakantagih : System.Web.UI.Page
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



                DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");

                ddlproduk.SelectedValue = "3C";
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

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MARKETING", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                
                ge.ListDataset = gb.GetDataJustOutPut("AR_TUNGGAKAN_Tgltagih", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {

                    XLWorkbook wb = new XLWorkbook();
                    for (int i = 0; i < ge.ListDataset.Tables[0].Rows.Count; i++)
                    {

                      


                        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                        varx[0] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ge.ListDataset.Tables[0].Rows[i][0].ToString(), typeof(string));
                        varx[1] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTTUNGGAKANDETAIL", typeof(string));
                        varx[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varx[5] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlproduk.SelectedValue, typeof(string));
                        varx[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        dt2.ListDataset = gb.GetDataJustOutPut("AR_TUNGGAKAN_Tgltagih", varx, "constrmf");
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
                            //name=dt.Rows[i][0].;
                            name = ge.ListDataset.Tables[0].Rows[i][1].ToString();


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
                        worksheet.Cell(7, 8).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 9).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 10).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 11).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 12).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 13).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 14).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 15).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 16).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 17).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 18).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 19).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 20).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 21).Style.Border.TopBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell(7, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 12).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 13).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 14).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 15).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 16).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 17).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 18).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 19).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 20).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 21).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 12).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 14).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 15).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 16).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 19).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 20).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 21).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell(7, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 12).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 13).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(7, 14).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 15).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 16).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 18).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 19).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 20).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(7, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                            worksheet.Cell("A1").Value = "LAPORAN LIST TUNGGAKAN MARKETING TGL JWL VS TGL TAGIH";
                                worksheet.Cell("A1").Style.Font.FontSize = 9;
                                worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                                worksheet.Cell("A2").Value = "Periode";
                                worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                                worksheet.Cell("A2").Style.Font.FontSize = 9;
                                worksheet.Cell("A3").Value = "Produk"; 
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
                            worksheet.Cell("A3").Value = ddlproduk.SelectedItem.Text;
                            worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A3").Style.Font.FontSize = 9;
                            worksheet.Cell("A4").Value = ddlcabang.SelectedItem.Text;
                            worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A4").Style.Font.FontSize = 9;
                            worksheet.Cell("A5").Value = ge.ListDataset.Tables[0].Rows[i][1].ToString();
                            worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A5").Style.Font.FontSize = 9;


                            worksheet.Cell("A7").Value = "No.";
                            worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("A7").Style.Font.FontSize = 9;

                            worksheet.Cell("B7").Value = "No. Rekening";
                            worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("B7").Style.Font.FontSize = 9;


                            worksheet.Cell("C7").Value = "Nama Nasabah";
                            worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("C7").Style.Font.FontSize = 9;

                            worksheet.Cell("D7").Value = "Hari";
                            worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("D7").Style.Font.FontSize = 9;

                            worksheet.Cell("E7").Value = "Plafon";
                            worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("E7").Style.Font.FontSize = 9;

                            worksheet.Cell("F7").Value = "Saldo Akhir";
                            worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("F7").Style.Font.FontSize = 9;

                            worksheet.Cell("G7").Value = "Jangka";
                            worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("G7").Style.Font.FontSize = 9;

                            worksheet.Cell("H7").Value = "Terbayar";
                            worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("H7").Style.Font.FontSize = 9;

                            worksheet.Cell("I7").Value = "Cadangan";
                            worksheet.Cell("I7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("I7").Style.Font.FontSize = 9;
                            worksheet.Cell("J6").Value = "Data Berdasarkan Tgl Jatuh Tempo";
                                 worksheet.Range(worksheet.Cell(6, 10), worksheet.Cell(6, 15)).Merge();
                            worksheet.Cell("J6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(6, 10).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 11).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 12).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 13).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 14).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 15).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 15).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell("J7").Value = "Tunggakan";
                            worksheet.Cell("J7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("J7").Style.Font.FontSize = 9;
                            worksheet.Cell("J7").Style.Alignment.WrapText = true;

                            worksheet.Cell("K7").Value = "Tung.Pokok";
                            worksheet.Cell("K7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("K7").Style.Font.FontSize = 9;
                            worksheet.Cell("K7").Style.Alignment.WrapText = true;

                            worksheet.Cell("L7").Value = "Tung.Bunga";
                            worksheet.Cell("L7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("L7").Style.Font.FontSize = 9;
                            worksheet.Cell("L7").Style.Alignment.WrapText = true;

                            worksheet.Cell("M7").Value = "Setoran Yang Belum Tertarik";
                            worksheet.Cell("M7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("M7").Style.Font.FontSize = 9;
                            worksheet.Cell("M7").Style.Alignment.WrapText = true;

                            worksheet.Cell("N7").Value = "Rasio";
                            worksheet.Cell("N7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("N7").Style.Font.FontSize = 9;
                            worksheet.Cell("N7").Style.Alignment.WrapText = true;

                            worksheet.Cell("O7").Value = "Tgl";
                            worksheet.Cell("O7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("O7").Style.Font.FontSize = 9;
                            worksheet.Cell("O7").Style.Alignment.WrapText = true;

                            worksheet.Cell("P6").Value = "Data Berdasarkan Tgl Tagih";
                            worksheet.Range(worksheet.Cell(6, 16), worksheet.Cell(6, 21)).Merge();
                            worksheet.Cell("P6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(6, 16).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 17).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 18).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 19).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 20).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 21).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(6, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                            worksheet.Cell("P7").Value = "Tunggakan";
                            worksheet.Cell("P7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("P7").Style.Font.FontSize = 9;
                            worksheet.Cell("P7").Style.Alignment.WrapText = true;

                            worksheet.Cell("Q7").Value = "Tung.Pokok";
                            worksheet.Cell("Q7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("Q7").Style.Font.FontSize = 9;
                            worksheet.Cell("Q7").Style.Alignment.WrapText = true;

                            worksheet.Cell("R7").Value = "Tung.Bunga";
                            worksheet.Cell("R7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("R7").Style.Font.FontSize = 9;
                            worksheet.Cell("R7").Style.Alignment.WrapText = true;

                            worksheet.Cell("S7").Value = "Setoran Yang Belum Tertarik";
                            worksheet.Cell("S7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("S7").Style.Font.FontSize = 9;
                            worksheet.Cell("S7").Style.Alignment.WrapText = true;
                     
                            worksheet.Cell("T7").Value = "Rasio";
                            worksheet.Cell("T7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("T7").Style.Font.FontSize = 9;
                            worksheet.Cell("T7").Style.Alignment.WrapText = true;

                            worksheet.Cell("U7").Value = "Tgl";
                            worksheet.Cell("U7").Style.Font.FontName = "Calibri";
                            worksheet.Cell("U7").Style.Font.FontSize = 9;
                            worksheet.Cell("U7").Style.Alignment.WrapText = true;



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
                            worksheet.Cell(b, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 12).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 13).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 14).Style.Border.BottomBorder = XLBorderStyleValues.Thin;


                                worksheet.Cell(b, 15).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 16).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 17).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 18).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 19).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 20).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 21).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                                worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 12).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 13).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 14).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 15).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 16).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 18).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 19).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 20).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;



                                worksheet.Cell(b, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 12).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 14).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 15).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 16).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(b, 20).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Cell(b, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;


                                worksheet.Cell(b, "A").Value = p;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                                    worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_rekening"].ToString();
                                    worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                                    worksheet.Cell(b, "C").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                                    worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "C").Style.Font.FontSize = 9;

                                    worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["hari_tagih"].ToString();
                                    worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "D").Style.Font.FontSize = 9;

                                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["plafon"].ToString();
                                    worksheet.Cell(b, "E").Value = ku_mingguan.Replace(",", ".");
                                    worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "E").Style.Font.FontSize = 9;

                                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["bakidb"].ToString();
                                    worksheet.Cell(b, "F").Value = ku_mingguan.Replace(",", ".");
                                    worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "F").Style.Font.FontSize = 9;


                                worksheet.Cell(b, "G").Value = dt2.ListDataset.Tables[0].Rows[x]["jangka"].ToString();
                                worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "G").Style.Font.FontSize = 9;

                                    worksheet.Cell(b, "H").Value = dt2.ListDataset.Tables[0].Rows[x]["terbayar"].ToString();
                                    worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "H").Style.Font.FontSize = 9;

                                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["by_cadangan"].ToString();
                                    worksheet.Cell(b, "I").Value = ku_mingguan.Replace(",", ".");
                                    worksheet.Cell(b, "I").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "I").Style.Font.FontSize = 9;

                                    worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["tunggakan"].ToString();
                                    worksheet.Cell(b, "J").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "J").Style.Font.FontSize = 9;

                                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_pokok"].ToString();
                                    worksheet.Cell(b, "K").Value = ku_mingguan.Replace(",", ".");
                                    worksheet.Cell(b, "K").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "K").Style.Font.FontSize = 9;

                                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_bunga"].ToString();
                                    worksheet.Cell(b, "L").Value = ku_mingguan.Replace(",", ".");
                                    worksheet.Cell(b, "L").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "L").Style.Font.FontSize = 9;

                                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["total_tung"].ToString();
                                    worksheet.Cell(b, "M").Value = ku_mingguan.Replace(",", ".");
                                    worksheet.Cell(b, "M").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "M").Style.Font.FontSize = 9;


                                    ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["rasio"].ToString();
                                    worksheet.Cell(b, "N").Value = ku_mingguan.Replace(",", ".");
                                    worksheet.Cell(b, "N").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "N").Style.Font.FontSize = 9;

                                
                                    worksheet.Cell(b, "O").Value = dt2.ListDataset.Tables[0].Rows[x]["tgl_due"].ToString();
                                    worksheet.Cell(b, "O").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "O").Style.Font.FontSize = 9;




                                worksheet.Cell(b, "P").Value = dt2.ListDataset.Tables[0].Rows[x]["tunggakan"].ToString();
                                worksheet.Cell(b, "P").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "P").Style.Font.FontSize = 9;

                                ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_pokok"].ToString();
                                worksheet.Cell(b, "Q").Value = ku_mingguan.Replace(",", ".");
                                worksheet.Cell(b, "Q").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "Q").Style.Font.FontSize = 9;

                                ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["tung_bunga"].ToString();
                                worksheet.Cell(b, "R").Value = ku_mingguan.Replace(",", ".");
                                worksheet.Cell(b, "R").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "R").Style.Font.FontSize = 9;

                                ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["total_tung"].ToString();
                                worksheet.Cell(b, "S").Value = ku_mingguan.Replace(",", ".");
                                worksheet.Cell(b, "S").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "S").Style.Font.FontSize = 9;


                                ku_mingguan = dt2.ListDataset.Tables[0].Rows[x]["rasio"].ToString();
                                worksheet.Cell(b, "T").Value = ku_mingguan.Replace(",", ".");
                                worksheet.Cell(b, "T").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "T").Style.Font.FontSize = 9;


                                worksheet.Cell(b, "U").Value = dt2.ListDataset.Tables[0].Rows[x]["tgl_due"].ToString();
                                worksheet.Cell(b, "U").Style.Font.FontName = "Calibri";
                                worksheet.Cell(b, "U").Style.Font.FontSize = 9;                                
                               
                              



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
                        worksheet.Cell(pakhir, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 12).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 13).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 14).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 15).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 16).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 17).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 18).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 19).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 20).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 21).Style.Border.BottomBorder = XLBorderStyleValues.Thin;



                            worksheet.Cell(pakhir, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 8).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 11).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 12).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 14).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 15).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 16).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 19).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 20).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 21).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                            worksheet.Cell(pakhir, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 10).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 11).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 12).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 13).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(pakhir, 14).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir,15).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 16).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 18).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 19).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 20).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Cell(pakhir, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                         
                            worksheet.Cell(pakhir, "D").Value = "Total";
                                worksheet.Cell(pakhir, "D").Style.Font.FontName = "Calibri";
                                worksheet.Cell(pakhir, "D").Style.Font.FontSize = 9;
                                worksheet.Cell(pakhir, "E").Style.Font.FontName = "Calibri";
                                worksheet.Cell(pakhir, "E").Style.Font.FontSize = 9;
                                worksheet.Cell(pakhir, "F").Style.Font.FontName = "Calibri";
                                worksheet.Cell(pakhir, "F").Style.Font.FontSize = 9;
                                worksheet.Cell(pakhir, "I").Style.Font.FontName = "Calibri";
                                worksheet.Cell(pakhir, "I").Style.Font.FontSize = 9;
                                worksheet.Cell(pakhir, "J").Style.Font.FontName = "Calibri";
                                worksheet.Cell(pakhir, "J").Style.Font.FontSize = 9;
                                worksheet.Cell(pakhir, "L").Style.Font.FontName = "Calibri";
                                worksheet.Cell(pakhir, "L").Style.Font.FontSize = 9;
                                worksheet.Cell(pakhir, "K").Style.Font.FontName = "Calibri";
                                worksheet.Cell(pakhir, "K").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "M").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "M").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "N").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "N").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "O").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "O").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "P").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "P").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "Q").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "Q").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "R").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "R").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "S").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "S").Style.Font.FontSize = 9;
                            worksheet.Cell(pakhir, "T").Style.Font.FontName = "Calibri";
                            worksheet.Cell(pakhir, "T").Style.Font.FontSize = 9;
                          


                            worksheet.Cell(pakhir, "E").FormulaA1 = "=SUM(E8:E" + b + ")";
                                worksheet.Cell(pakhir, "F").FormulaA1 = "=SUM(F8:F" + b + ")";
                                worksheet.Cell(pakhir, "I").FormulaA1 = "=SUM(I8:I" + b + ")";

                            worksheet.Cell(pakhir, "K").FormulaA1 = "=SUM(K8:K" + b + ")";
                            worksheet.Cell(pakhir, "L").FormulaA1 = "=SUM(L8:L" + b + ")";
                            worksheet.Cell(pakhir, "M").FormulaA1 = "=SUM(M8:M" + b + ")";
                            worksheet.Cell(pakhir, "N").FormulaA1 = "(K" + pakhir + "/f" + pakhir + ")*100";


                            worksheet.Cell(pakhir, "Q").FormulaA1 = "=SUM(Q8:Q" + b + ")";
                            worksheet.Cell(pakhir, "R").FormulaA1 = "=SUM(R8:R" + b + ")";
                            worksheet.Cell(pakhir, "S").FormulaA1 = "=SUM(S8:S" + b + ")";
                            worksheet.Cell(pakhir, "T").FormulaA1 = "(Q" + pakhir + "/f" + pakhir + ")*100";




                            worksheet.Column(5).Style.NumberFormat.Format = "_(#,##0_)";
                        worksheet.Column(6).Style.NumberFormat.Format = "_(#,##0_)";
                        worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";


                        worksheet.Column(10).Style.NumberFormat.Format = "_(#,##0_)";
                        worksheet.Column(11).Style.NumberFormat.Format = "_(#,##0_)";
                            worksheet.Column(12).Style.NumberFormat.Format = "_(#,##0_)";
                            worksheet.Column(13).Style.NumberFormat.Format = "_(#,##0_)";

                            worksheet.Column(14).Style.NumberFormat.Format = "#,##0.00";
                    
                        worksheet.Column(15).Style.NumberFormat.Format = "dd/mm/yyyy";

                            worksheet.Column(16).Style.NumberFormat.Format = "_(#,##0_)";
                            worksheet.Column(17).Style.NumberFormat.Format = "_(#,##0_)";
                            worksheet.Column(18).Style.NumberFormat.Format = "_(#,##0_)";
                            worksheet.Column(19).Style.NumberFormat.Format = "_(#,##0_)";

                            worksheet.Column(20).Style.NumberFormat.Format = "#,##0.00";

                            worksheet.Column(21).Style.NumberFormat.Format = "dd/mm/yyyy";

                            //worksheet.Column(8).Style.NumberFormat.Format = "_(#,##0_)";
                            //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";

                            //worksheet.Column(9).Style.NumberFormat.Format = "_(#,##0_)";



                            worksheet.Column(1).Width = 4;
                                worksheet.Column(2).Width = 11;
                                worksheet.Column(3).Width = 8;
                                worksheet.Column(4).Width = 5.71;
                                worksheet.Column(5).Width = 9.43;
                                worksheet.Column(6).Width = 9.71;
                                worksheet.Column(7).Width = 2.14;
                                worksheet.Column(8).Width = 2.57;
                                worksheet.Column(9).Width = 8.43;
                                worksheet.Column(10).Width = 9;
                                worksheet.Column(11).Width = 9.86;
                                worksheet.Column(12).Width = 10.14;
                                worksheet.Column(13).Width = 11.43;
                                worksheet.Column(14).Width = 6.43;
                            worksheet.Column(15).Width = 9.14;
                            worksheet.Column(16).Width = 9;
                            worksheet.Column(17).Width = 9.86;
                            worksheet.Column(18).Width = 10.14;
                            worksheet.Column(19).Width = 11.43;
                            worksheet.Column(20).Width = 6.43;
                            worksheet.Column(21).Width = 9.14;








                            worksheet.Row(1).Height = 12;
                                worksheet.Row(2).Height = 12;

                                worksheet.Row(3).Height = 12;
                                worksheet.Row(4).Height = 12;

                                worksheet.Row(5).Height = 12;
                                worksheet.Row(6).Height = 12;
                                worksheet.Row(7).Height = 36;
                                worksheet.Row(8).Height = 12;
                                worksheet.Row(9).Height = 12;
                 

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








                }


                    string report = "";
                    if (ddlproduk.SelectedValue == "3C")
                    {
                        report = "Report Tunggakan Mingguan " + xtanggal.ToString() + ".xlsx";
                    }
                    else if (ddlproduk.SelectedValue == "3B")
                    {
                        report = "Report Listtagihan Bulanan " + xtanggal.ToString() + ".xlsx";
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