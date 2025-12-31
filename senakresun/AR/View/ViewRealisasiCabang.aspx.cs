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
using System.Net;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DocumentFormat.OpenXml.EMMA;
using System.Activities.Statements;
using ClosedXML.Excel;

public partial class AR_View_ViewRealisasiCabang : System.Web.UI.Page
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
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();
    GlobalEntities dt2 = new GlobalEntities();
    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A2";
    string kodesumber = "MK";
    string KodeKonter = "AP03";
    public string nobuktidb;
    string no_bukti;
    decimal tambahteller = 0;
    decimal kurangteller = 0;
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // btncetak_Click(sender, e);
        try
        {

            if (!Page.IsPostBack)
            {


                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";


                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());


                //txtStart.Text = xtanggal.ToString("dd-MM-yyyy");
                txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
                txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
                // DefaultClass.Global.SqlHelper[] Paramx = new DefaultClass.Global.SqlHelper[2];
                // Paramx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AREA", typeof(string));
                // Paramx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                // //Paramx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //// Paramx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                // ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Realisasi_AREA", Paramx, "constrmf");
                // //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                // gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddluser, "None", "");



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
                var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var[3] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, "", typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_Login_TT", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();




                //ambilTahun();
                //ambilBulan();

                DefaultClass.Global.SqlHelper[] var1= new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", var1, "constrmf");
                //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                //txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                ddlbulan.SelectedValue = int.Parse(xtanggal.ToString("MM")).ToString();
                //ddlTahun.SelectedValue = xtanggal.ToString("yyyy");

                //ddlbulan.Enabled = false;
                //ddlTahun.Enabled = false;

                ddlcabang.Enabled = true;

            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    decimal Plafon = 0, Potongan = 0, diterimaAnggota = 0, cadangan = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            Potongan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "potongan"));
            diterimaAnggota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jlhditerimaanggota"));
            cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalplafon = (Label)e.Row.FindControl("lbltotalplafon");
            lbltotalplafon.Text = string.Format("{0:N2}", Plafon);
            Label lbltotalPotongan = (Label)e.Row.FindControl("lbltotalPotongan");
            lbltotalPotongan.Text = string.Format("{0:N2}", Potongan);
            Label lbltotalditerimaanggota = (Label)e.Row.FindControl("lbltotalditerimaanggota");
            lbltotalditerimaanggota.Text = string.Format("{0:N2}", diterimaAnggota);
            Label lbltotalcadangan = (Label)e.Row.FindControl("lbltotalcadangan");
            lbltotalcadangan.Text = string.Format("{0:N2}", cadangan);
        }
    }

    public void btncetak_Click(object sender, EventArgs e)
    {


        //BindGridEntity();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);



    }


    public void btnSerach_Click(object sender, EventArgs e)
    {


        //ReportDocument rprt = new ReportDocument();
        try
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




            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RLSCABANG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            var[6] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            var[7] = gb.CreateSPParameter("@KodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));

            //var[7] = gb.CreateSPParameter("@kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
            dt2.ListDataset = gb.GetDataJustOutPut("AR_Realisasi_AREA", var, "constrmf");
            dt2.ListDataset.Tables[0].DefaultView.Sort = "no_anggota ASC";





            //dt2.ListDataset = gb.GetDataJustOutPut("dbo.GL_STD_RPT_003_Bulanan", Paramsc, "constrmf");
            if (dt2.ListDataset.Tables[0].Rows.Count > 0)
            {



                string name;
                //name=dt.Rows[i][0].;
                name = "Anggota tidak aktif";


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


                worksheet.Cell("A1").Value = "LAPORAN TRANSAKSI ANGGOTA TIDAK AKTIF PERIODE TERTENTU";
                worksheet.Cell("A1").Style.Font.FontSize = 9;
                worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                worksheet.Cell("A2").Value = "Periode";
                worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                worksheet.Cell("A2").Style.Font.FontSize = 9;
                worksheet.Cell("A3").Value = "Cabang";
                worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                worksheet.Cell("A3").Style.Font.FontSize = 9;


                worksheet.Cell("C2").Value = "'Periode : " + DateTime.Parse(txtStart.Text).ToString("dd-MM-yyyy") + " s/d " + DateTime.Parse(txtEnd.Text).ToString("dd-MM-yyyy");

                worksheet.Cell("C2").Style.Font.FontName = "Calibri";
                worksheet.Cell("C2").Style.Font.FontSize = 9;
                worksheet.Cell("C3").Value = ddlcabang.SelectedItem.Text;
                worksheet.Cell("C3").Style.Font.FontName = "Calibri";
                worksheet.Cell("C3").Style.Font.FontSize = 9;


                worksheet.Cell("A7").Value = "No.";
                worksheet.Cell("A7").Style.Font.FontName = "Calibri";
                worksheet.Cell("A7").Style.Font.FontSize = 9;

                worksheet.Cell("B7").Value = "No.Anggota";
                worksheet.Cell("B7").Style.Font.FontName = "Calibri";
                worksheet.Cell("B7").Style.Font.FontSize = 9;


                worksheet.Cell("C7").Value = "No. KTP";
                worksheet.Cell("C7").Style.Font.FontName = "Calibri";
                worksheet.Cell("C7").Style.Font.FontSize = 9;

                worksheet.Cell("D7").Value = "Nama Anggota";
                worksheet.Cell("D7").Style.Font.FontName = "Calibri";
                worksheet.Cell("D7").Style.Font.FontSize = 9;

                worksheet.Cell("E7").Value = "Alamat";
                worksheet.Cell("E7").Style.Font.FontName = "Calibri";
                worksheet.Cell("E7").Style.Font.FontSize = 9;

                worksheet.Cell("F7").Value = "kabupaten";
                worksheet.Cell("F7").Style.Font.FontName = "Calibri";
                worksheet.Cell("F7").Style.Font.FontSize = 9;

                worksheet.Cell("G7").Value = "Kecamatan";
                worksheet.Cell("G7").Style.Font.FontName = "Calibri";
                worksheet.Cell("G7").Style.Font.FontSize = 9;

                worksheet.Cell("H7").Value = "Kelurahan";
                worksheet.Cell("H7").Style.Font.FontName = "Calibri";
                worksheet.Cell("H7").Style.Font.FontSize = 9;

                worksheet.Cell("I7").Value = "kodepos";
                worksheet.Cell("I7").Style.Font.FontName = "Calibri";
                worksheet.Cell("I7").Style.Font.FontSize = 9;

                worksheet.Cell("J7").Value = "RT";
                worksheet.Cell("J7").Style.Font.FontName = "Calibri";
                worksheet.Cell("J7").Style.Font.FontSize = 9;


                worksheet.Cell("K7").Value = "RW";
                worksheet.Cell("K7").Style.Font.FontName = "Calibri";
                worksheet.Cell("K7").Style.Font.FontSize = 9;


                worksheet.Cell("L7").Value = "No HP";
                worksheet.Cell("L7").Style.Font.FontName = "Calibri";
                worksheet.Cell("L7").Style.Font.FontSize = 9;

                worksheet.Cell("M7").Value = "kode AO";
                worksheet.Cell("M7").Style.Font.FontName = "Calibri";
                worksheet.Cell("M7").Style.Font.FontSize = 9;

                worksheet.Cell("N7").Value = "Status Wo";
                worksheet.Cell("N7").Style.Font.FontName = "Calibri";
                worksheet.Cell("N7").Style.Font.FontSize = 9;

                worksheet.Cell("O7").Value = "Non Aktif Flag";
                worksheet.Cell("O7").Style.Font.FontName = "Calibri";
                worksheet.Cell("O7").Style.Font.FontSize = 9;

                worksheet.Cell("P7").Value = "Tarik Pusat";
                worksheet.Cell("P7").Style.Font.FontName = "Calibri";
                worksheet.Cell("P7").Style.Font.FontSize = 9;

                worksheet.Cell("Q7").Value = "Penutupan";
                worksheet.Cell("Q7").Style.Font.FontName = "Calibri";
                worksheet.Cell("Q7").Style.Font.FontSize = 9;

                worksheet.Cell("R7").Value = "Alasan Penutupan";
                worksheet.Cell("R7").Style.Font.FontName = "Calibri";
                worksheet.Cell("R7").Style.Font.FontSize = 9;

                worksheet.Cell("S7").Value = "Register Date";
                worksheet.Cell("S7").Style.Font.FontName = "Calibri";
                worksheet.Cell("S7").Style.Font.FontSize = 9;




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
                    worksheet.Cell(b, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(b, 19).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                    worksheet.Cell(b, "A").Value = p;
                    worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "A").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "B").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_anggota"].ToString();
                    worksheet.Cell(b, "B").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "B").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_identitas"].ToString();
                    worksheet.Cell(b, "C").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "C").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "D").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["nama_nasabah"].ToString();
                    worksheet.Cell(b, "D").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "D").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "E").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["alamat"].ToString();
                    worksheet.Cell(b, "E").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "E").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "F").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["kabupaten"].ToString();
                    worksheet.Cell(b, "F").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "F").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "G").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["kecamatan_ktp"].ToString();
                    worksheet.Cell(b, "G").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "G").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "H").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["kelurahan_ktp"].ToString();
                    worksheet.Cell(b, "H").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "H").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "I").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["kodepos"].ToString();
                    worksheet.Cell(b, "I").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "I").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "J").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["RT_ktp"].ToString();
                    worksheet.Cell(b, "J").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "J").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "K").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["RW_ktp"].ToString();
                    worksheet.Cell(b, "K").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "K").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "L").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["no_hp"].ToString();
                    worksheet.Cell(b, "L").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "L").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "M").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["kode_ref"].ToString();
                    worksheet.Cell(b, "M").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "M").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "N").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["status_wo"].ToString();
                    worksheet.Cell(b, "N").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "N").Style.Font.FontSize = 9;

                    worksheet.Cell(b, "O").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NonAktifFlag"].ToString();
                    worksheet.Cell(b, "O").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "O").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "P").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["flagsalah"].ToString();
                    worksheet.Cell(b, "P").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "P").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "Q").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["flag_tutup"].ToString();
                    worksheet.Cell(b, "Q").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "Q").Style.Font.FontSize = 9;



                    worksheet.Cell(b, "R").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["alasan_keluar"].ToString();
                    worksheet.Cell(b, "R").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "R").Style.Font.FontSize = 9;


                    worksheet.Cell(b, "S").Value = DateTime.Parse(dt2.ListDataset.Tables[0].Rows[x]["register_date"].ToString()).ToString("dd-MM-yyyy");
                    worksheet.Cell(b, "S").Style.Font.FontName = "Calibri";
                    worksheet.Cell(b, "S").Style.Font.FontSize = 9;




                    worksheet.Row(b).Height = 12;




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
                worksheet.Cell(pakhir, 15).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 16).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(pakhir, 18).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                worksheet.Cell(pakhir, 19).Style.Border.RightBorder = XLBorderStyleValues.Thin;


               



                worksheet.Column(1).Width = 4;
                worksheet.Column(2).Width = 19.43;
                worksheet.Column(3).Width = 10.57;
                worksheet.Column(4).Width = 10.86;
                worksheet.Column(5).Width = 10.86;
                worksheet.Column(6).Width = 25.14;
                worksheet.Column(7).Width = 15.14;
                worksheet.Column(8).Width = 15.14;
                worksheet.Column(9).Width = 15.57;
                worksheet.Column(10).Width = 10.57;







                worksheet.Row(1).Height = 12;
                worksheet.Row(2).Height = 12;

                worksheet.Row(3).Height = 12;
                worksheet.Row(4).Height = 12;

                worksheet.Row(5).Height = 12;
                worksheet.Row(6).Height = 12;
                worksheet.Row(7).Height = 12;
                worksheet.Row(8).Height = 12;
                worksheet.Row(9).Height = 12;
                worksheet.Row(10).Height = 12;

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

            report = "Report Anggota Tidak Aktif.xlsx";

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

            }

        }

    }



    public void BindGridEntity()
    {
        DateTime periodeDate = new DateTime(2024, 1, 1);
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RLSCABANG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
            //var[5] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, periodeDate, typeof(DateTime));
            var[5] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            var[6] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            var[7] = gb.CreateSPParameter("@KodeArea", ParameterDirection.Input, SqlDbType.VarChar, 30,  "", typeof(string));

            //var[7] = gb.CreateSPParameter("@kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlAO.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_Realisasi_AREA", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "register_date desc";
            GRDataList.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDataList.DataBind();

            GRDataList.UseAccessibleHeader = true;
            GRDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
            buttonprint.Visible = true;
            //}
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void ddlbulan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(now.Year, Int32.Parse(ddlbulan.SelectedValue), 1);
        txtStart.Text = date.ToString("dd-MM-yyyy");
        DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlbulan.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("dd-MM-yyyy");
        txtStart.Enabled = true;
        txtEnd.Enabled = true;
        ddlbulan.Enabled = true;
    }
    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        if (DateTime.Parse(txtStart.Text) > DateTime.Parse(txtEnd.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Awal tidak boleh lebih dari Tanggal Akhir');", true);

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

            txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
            txtStart.Focus();
        }
        else if (DateTime.Parse(txtEnd.Text) < DateTime.Parse(txtEnd.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tanggal Akhir tidak boleh kurang dari Tanggal Awal');", true);

            DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
            DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

            txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
            txtEnd.Focus();
        }
        //else
        //{
        //    BindGridEntity();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        //}
    }

    public void ambiltanggal()
    {

        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
        ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());

        txtStart.Text = xtanggal.ToString("dd/MM/yyyy");
        txtEnd.Text = xtanggal.ToString("dd/MM/yyyy");
    }
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGridEntity();
        //\ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
}
