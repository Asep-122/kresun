using ClosedXML.Excel;
using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Report_viewreportkonsolidasiBerjalanCabang: System.Web.UI.Page
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




                ambilTahun();
                ambilBulan();

                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                //txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                ddlbulan.SelectedValue = int.Parse(xtanggal.ToString("MM")).ToString();
                ddlTahun.SelectedValue = xtanggal.ToString("yyyy");

                //ddlbulan.Enabled = false;
                //ddlTahun.Enabled = false;

                ddlcabang.Enabled = true;
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
        var currentYear = DateTime.Today.Year + 1;
        for (int i = 5; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
        ddlTahun.SelectedValue = currentYear.ToString();
        //if (SessBranchID != "000")
        //{
        //    ddlTahun.Enabled = false;
        //}
        //else
        //{
            ddlTahun.Enabled = true;
        //}
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
        //if (SessBranchID != "000")
        //{
        //    ddlbulan.Enabled = false;
        //}
        //else
        //{
            ddlbulan.Enabled = true;
        //}
    }


    
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {

          if (DdlReport.SelectedValue == "4")
            {

                int column = 1;
                string query = "";
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

                //var TypeReport = ddltypereport.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@typereport", ParameterDirection.Input, SqlDbType.VarChar, 1, "3", typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                var[6] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                var[7] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG_BERJALAN", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    //ds.Tables[0].TableName = "tbl_master";
                    XLWorkbook wb = new XLWorkbook();
                    string name = "";
                    //name=dt.Rows[i][0].;
                  
                        name = "Cabang";

                   

                    string hurufx = "";
                    int abjadlanjutx = 65;
                    int abjadawalx = 64;
                    string strAlphaawalx = "";
                    string strAlphalanjutx = "";
                    string alpha1x = "";
                    string alpha2x = "";

                    var worksheet = wb.Worksheets.Add(name);
                    worksheet.RowHeight = 13;

                    int bulanlalu = 0;
                    int bulanlalu2 = 0;

                    if (ddlbulan.SelectedValue == "1")
                    {
                        bulanlalu = 12;
                        bulanlalu2 = 11;

                    }
                    else
                    {
                        bulanlalu = int.Parse(ddlbulan.SelectedValue) - 1;
                        //bulanlalu2 = int.Parse(ddlbulan.SelectedValue) - 2;


                    }


                    for (int i = 0; i < ge.ListDataset.Tables[0].Rows.Count; i++)
                    {


                        if (i == 0)
                        {



                            for (int x1x = 1; x1x < (ge.ListDataset.Tables[0].Rows.Count + 2); x1x++)
                            {

                                if (abjadlanjutx == 90)
                                {
                                    abjadlanjutx = 65;
                                    abjadawalx = abjadawalx + 1;
                                }
                                if (abjadawalx == 64)
                                {
                                    strAlphaawalx = "";
                                    abjadlanjutx = abjadlanjutx + 1;
                                }
                                else
                                {
                                    if (strAlphaawalx == "")
                                    {
                                        strAlphaawalx = ((char)abjadawalx).ToString();
                                    }
                                    else
                                    {
                                        abjadlanjutx = abjadlanjutx + 1;
                                        strAlphaawalx = ((char)abjadawalx).ToString();

                                    }

                                }

                                strAlphalanjutx = strAlphaawalx + ((char)abjadlanjutx).ToString();




                            }
                            hurufx = strAlphalanjutx;
                            if ((abjadlanjutx - 1) < 65)
                            {
                                alpha2x = "Z";

                            }
                            else
                            {
                                alpha2x = strAlphaawalx + ((char)(abjadlanjutx - 1)).ToString();
                            }

                            if ((abjadlanjutx - 2) < 65)
                            {
                                alpha1x = "Z";

                            }
                            else
                            {
                                alpha1x = strAlphaawalx + ((char)(abjadlanjutx - 2)).ToString();
                            }


                            //alpha1x = strAlphaawalx + ((char)(abjadlanjutx - 3)).ToString();
                            //alpha2x = strAlphaawalx + ((char)(abjadlanjutx - 2)).ToString();

                        }
                        string xnama;
                        string ku_mingguan;
                        string noperkiraan;
                        string groupx;

                        string namakoperasi = ge.ListDataset.Tables[0].Rows[i]["nama_koperasi"].ToString().ToUpper();
                        string nama_area = ge.ListDataset.Tables[0].Rows[i]["nama_area"].ToString().ToUpper();
                        string cabang_nama = ge.ListDataset.Tables[0].Rows[i]["cabang_nama"].ToString().ToUpper();

                        string xxxx;
                        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[9];
                        varx[0] = gb.CreateSPParameter("@typereport", ParameterDirection.Input, SqlDbType.VarChar, 1, DdlReport.SelectedValue, typeof(string));
                        varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RPTKONSOLALL", typeof(string));
                        varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varx[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ge.ListDataset.Tables[0].Rows[i][0].ToString(), typeof(string));
                        varx[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                        varx[6] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                        varx[7] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                        varx[8] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));

                        dt2.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG_BERJALAN", varx, "constrmf");
                        if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                        {



                            abjadlanjut = abjadlanjut + 1;
                            column = column + 1;

                            //x1 = x1 + 1;
                            ////Add DataTable as Worksheet.
                            
                                worksheet.Cell("A1").Value = ddlcabang.SelectedItem.Text + " - " + ddlbulan.SelectedItem.Text + " - " + ddlTahun.SelectedValue;

                           

                            worksheet.Cell("A1").Style.Font.FontSize = 14;
                            worksheet.Cell("A1").Style.Font.FontName = "ARIAL";
                            worksheet.Cell("A1").Style.Font.Bold = true;

                            worksheet.Cell("A2").Value = "AKTIVA";
                            worksheet.Cell("A2").Style.Font.FontName = "ARIAL";
                            worksheet.Cell("A2").Style.Font.FontSize = 14;
                            worksheet.Cell("A2").Style.Font.Bold = true;


                            //worksheet.Cell("A3").Value = "AKTIVA";
                            //worksheet.Cell("A3").Style.Font.FontName = "ARIAL";
                            //worksheet.Cell("A3").Style.Font.FontSize = 12;
                            //worksheet.Cell("A3").Style.Font.Bold = true;



                            worksheet.Cell(3, column).Value = cabang_nama;
                            worksheet.Cell(3, column).Style.Font.FontName = "ARIAL";
                            worksheet.Cell(3, column).Style.Font.Italic = true;
                            worksheet.Cell(3, column).Style.Font.FontSize = 10;


                            int b;
                            int p;
                            b = 4;
                            p = 165;

                            if (abjadlanjut > 90)
                            {
                                abjadlanjut = 65;
                                abjadawal = abjadawal + 1;
                            }
                            if (abjadawal == 64)
                            {
                                strAlphaawal = "";
                            }
                            else
                            {
                                strAlphaawal = ((char)abjadawal).ToString();
                            }

                            strAlphalanjut = strAlphaawal + ((char)abjadlanjut).ToString();
                            huruf = strAlphalanjut;
                            huruf2 = strAlphaawal + ((char)(abjadlanjut - 1)).ToString();
                            //if (huruf == "Y")
                            //{
                            //    xxxx = "";
                            //}





                            //if (abjadlanjutx == 90)
                            //{
                            //    abjadlanjutx = 65;
                            //    abjadawalx = abjadawalx + 1;
                            //}
                            //if (abjadawalx == 64)
                            //{
                            //    strAlphaawalx = "";
                            //}
                            //else
                            //{
                            //    strAlphaawalx = ((char)abjadawalx).ToString();
                            //}

                            //strAlphalanjutx = strAlphaawalx + ((char)abjadlanjutx).ToString();


                            //abjadlanjutx = abjadlanjutx + 1;



                            for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                            {

                                noperkiraan = dt2.ListDataset.Tables[0].Rows[x]["perkiraan_bahan"].ToString();
                                if (noperkiraan == "810000")
                                {
                                    b = 240;
                                }
                                if (noperkiraan == "311200")
                                {
                                    b = b + 1;
                                    p = p + 1;
                                }
                                if (noperkiraan == "311111")
                                {
                                    p = p + 1;
                                }

                                if (b == 48 || b == 90 || p == 166 || b == 128 || p == 204)
                                {
                                    if (p == 166 || p == 204)
                                    {

                                        worksheet.Cell(p, column).Value = cabang_nama;
                                        worksheet.Cell(p, column).Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(p, column).Style.Font.Italic = true;
                                        worksheet.Cell(p, column).Style.Font.FontSize = 10;


                                    }
                                    else
                                    {
                                        worksheet.Cell(b, column).Value = cabang_nama;
                                        worksheet.Cell(b, column).Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, column).Style.Font.Italic = true;
                                        worksheet.Cell(b, column).Style.Font.FontSize = 10;




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

                                    worksheet.Cell(p, "A").Value = "PROFIT  &  LOSS  MONTHLY";
                                    worksheet.Cell(p, "A").Style.Font.FontSize = 12;
                                    worksheet.Cell(p, "A").Style.Font.Bold = true;
                                    worksheet.Cell(p, "A").Style.Font.FontName = "ARIAL";

                                }

                                else if (b == 240)
                                {
                                    b = b + 1;
                                    worksheet.Cell(b, "A").Value = "DATA - DATA PENUNJANG";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 14;
                                    worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "ARIAL";

                                    b = b + 2;
                                    worksheet.Cell(b, column).Value = cabang_nama;
                                    worksheet.Cell(b, column).Style.Font.FontName = "ARIAL";
                                    worksheet.Cell(b, column).Style.Font.Italic = true;
                                    worksheet.Cell(b, column).Style.Font.FontSize = 10;



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

                                    groupx = dt2.ListDataset.Tables[0].Rows[x]["groupx"].ToString();
                                    if (groupx == "C")
                                    {
                                        p = p + 1;
                                        if (noperkiraan != "311900" && noperkiraan != "311901" && noperkiraan != "311903" && noperkiraan != "311904" && noperkiraan != "311906")
                                        {
                                            worksheet.Cell(p, "A").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_bahan"].ToString();
                                            worksheet.Cell(p, "A").Style.Font.FontName = "Calibri";
                                            worksheet.Cell(p, "A").Style.Font.FontSize = 11;
                                            if (dt2.ListDataset.Tables[0].Rows[x]["type_font"].ToString() == "B")
                                            {
                                                worksheet.Cell(p, "A").Style.Font.Bold = true;
                                            }

                                            xnama = dt2.ListDataset.Tables[0].Rows[x]["kategori"].ToString();

                                            if (xnama == "M" || xnama == "U")
                                            {
                                                //String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));

                                                if ((b >= 89 && b <= 240))
                                                {

                                                    ku_mingguan = Convert.ToString(Convert.ToDecimal(dt2.ListDataset.Tables[0].Rows[x]["saldo_akhir_bi"].ToString()));
                                                }
                                                else
                                                {
                                                    ku_mingguan = Convert.ToString(Convert.ToDecimal(dt2.ListDataset.Tables[0].Rows[x]["saldo_akhir"].ToString()));
                                                }
                                                //if (dt2.ListDataset.Tables[0].Rows[x]["saldobulanlalu"].ToString() == null)
                                                //{

                                                //}


                                                
                         


                                                //ku_mingguan =string.Format("{0:N2}", decimal.Parse(dt2.ListDataset.Tables[0].Rows[x]["saldobulanlalu"].ToString()));


                                                //if (column > 2)
                                                //{
                                                //    deltax = "="  +huruf + b.ToString()+ "-" + huruf2 + b.ToString();
                                                //    worksheet.Cell(p, column).FormulaA1 = deltax;
                                                //}
                                                //else
                                                //{ 

                                     
                                              
                                                worksheet.Cell(p, column).Value = ku_mingguan.Replace(",", ".");

                                                //}




                                                //worksheet.Cell(b, hurufx).FormulaA1 = "=SUM(B" + b.ToString() + ":" + alpha2x + b.ToString() + ")";
                                                //worksheet.Cell(p, hurufx).FormulaA1 = "=SUM(B" + p.ToString() + ":" + alpha2x + p.ToString() + ")";


                                                if (b == 21)
                                                {

                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "5:" + huruf + "19)";

                                                }
                                                else if (b == 28)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "24:" + huruf + "27)";
                                                }
                                                else if (b == 36)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "31:" + huruf + "35)";
                                                }
                                                else if (b == 42)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "39:" + huruf + "41)";
                                                }
                                                else if (b == 44)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "21+" + huruf + "28+" + huruf + "36+" + huruf + "42";
                                                }
                                                else if (b == 64)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "50:" + huruf + "62)";
                                                }
                                                else if (b == 73)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "67:" + huruf + "72)";
                                                }
                                                else if (b == 86)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "76:" + huruf + "85)";
                                                }
                                                else if (b == 88)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "64+" + huruf + "73+" + huruf + "86"; //"=B64+B73+B86";
                                                }
                                                else if (b == 97)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "93:" + huruf + "96)";//"=SUM(B93:B96)";

                                                }
                                                else if (b == 105)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "100:" + huruf + "104)";//"=SUM(B100:B104)";

                                                }
                                                else if (b == 106)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "97-" + huruf + "105"; //"=B97-B105";

                                                }
                                                else if (b == 114)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "110:" + huruf + "113)";//"=SUM(B110:B113)";

                                                }
                                                else if (b == 123)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "117:" + huruf + "122)";// "=SUM(B117:B122)";

                                                }
                                                else if (b == 125)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "114-" + huruf + "123"; // "=B114-B123";

                                                }
                                                else if (b == 126)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "106+" + huruf + "125"; // "=B106+B125";

                                                }
                                                else if (b == 146)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "130:" + huruf + "145)";// "=SUM(B130:B145)";

                                                }
                                                else if (b == 148)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "126-" + huruf + "146"; // "=B126-B146";

                                                }
                                                else if (b == 154)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "151:" + huruf + "153)";// "=SUM(B151:B153)";

                                                }
                                                else if (b == 155)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "148-" + huruf + "154"; // "=B148-B154";

                                                }
                                                else if (b == 159)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "157:" + huruf + "158)";//"=SUM(B157:B158)";

                                                }
                                                else if (b == 160)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "155+" + huruf + "159"; //"=B155+B159";

                                                }
                                                else if (b == 162)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "160+" + huruf + "161"; // "=B160+B161";
                                                }
                                                else if (b == 164)
                                                {
                                                    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "162-" + huruf + "163"; //"=B162-B163";
                                                }



                                                if (p == 173)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "169:" + huruf + "172)";// "=SUM(B169:B172)";
                                                }
                                                else if (p == 181)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "176:" + huruf + "180)";// "=SUM(B176:B180)";
                                                }
                                                else if (p == 182)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "173-" + huruf + "181"; //"=B173-B181";
                                                }
                                                else if (p == 190)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "186:" + huruf + "189)";// "=SUM(B186:B189)";
                                                }
                                                else if (p == 199)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "193:" + huruf + "198)";// "=SUM(B193:B198)";
                                                }
                                                else if (p == 201)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "190-" + huruf + "199"; //"=B190-B199";
                                                }
                                                else if (p == 202)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "182+" + huruf + "201"; //"=B182+B201";
                                                }
                                                else if (p == 222)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "206:" + huruf + "221)";//"=SUM(B206:B221)";
                                                }
                                                else if (p == 224)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "202-" + huruf + "222"; //"=B182+B201";"=B202-B222";
                                                }
                                                else if (p == 230)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "227:" + huruf + "229)";//"=SUM(B227:B229)";
                                                }
                                                else if (p == 231)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "224-" + huruf + "230"; // "=B224-B230";
                                                }
                                                else if (p == 235)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=ABS(" + huruf + "234)-ABS(" + huruf + "233)"; // "=B231+B235";
                                                    //worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "233:" + huruf + "234)";//"=SUM(B233:B234)";
                                                }
                                                else if (p == 236)
                                                {
                                                    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "231-" + huruf + "235"; // "=B231+B235";
                                                }





                                            }

                                        }





                                    }






                                    worksheet.Cell(b, "A").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_bahan"].ToString();
                                    worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 11;
                                    xnama = dt2.ListDataset.Tables[0].Rows[x]["kategori"].ToString();

                                    if (xnama == "M" || xnama == "U")
                                    {
                                        //String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));

                                        ku_mingguan = Convert.ToString(Convert.ToDecimal(dt2.ListDataset.Tables[0].Rows[x]["saldo_akhir"].ToString()));

                                        worksheet.Cell(b, column).Value = ku_mingguan.Replace(",", ".");

                                        //deltax = "=B" + b.ToString() + "-" + alpha1x + b.ToString();
                                        // worksheet.Cell(b, hurufx).FormulaA1 = deltax;
                                        // deltax = "=" + alpha2x + p.ToString() + "-" + alpha1x + p.ToString();
                                        // worksheet.Cell(p, hurufx).FormulaA1 = deltax;
                                        //if (b == 259)
                                        //{
                                        //    // = B256 / (B13 + B14) * 100
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=" + hurufx + "256/(" + hurufx + "13+" + hurufx + "14) * 100";


                                        //}
                                        //else if (b == 262)
                                        //{

                                        //    // worksheet.Cell(b, "B").FormulaA1 = "=-B16/(B13+B14)*100";
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=-" + hurufx + "16/(" + hurufx + "13+" + hurufx + "14) * 100";


                                        //}
                                        //else if (b == 265)
                                        //{
                                        //    //=B259-B262
                                        //    //worksheet.Cell(b, "B").FormulaA1 = "=B187-B190";
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=" + hurufx + "259-" + hurufx + "262";

                                        //}
                                        //else if (b == 268)
                                        //{
                                        //    //=B265*(B13+B14)/((B13+B14)+B16)
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=" + hurufx + "265*(" + hurufx + "13+" + hurufx + "14)/((" + hurufx + "13+" + hurufx + "14)+" + hurufx + "16)";
                                        //    //worksheet.Cell(b, "B").FormulaA1 = "=B193*(B13+B14)/((B13+B14)+B16)";


                                        //}
                                        //else
                                        //{
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=SUM(B" + b.ToString() + ":" + alpha2x + b.ToString() + ")";
                                        //}


                                        if (b == 21)
                                        {

                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "5:" + huruf + "19)";

                                        }
                                        else if (b == 28)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "24:" + huruf + "27)";
                                        }
                                        else if (b == 36)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "31:" + huruf + "35)";
                                        }
                                        else if (b == 42)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "39:" + huruf + "41)";
                                        }
                                        else if (b == 44)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "21+" + huruf + "28+" + huruf + "36+" + huruf + "42";
                                        }
                                        else if (b == 64)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "50:" + huruf + "62)";
                                        }
                                        else if (b == 73)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "67:" + huruf + "72)";
                                        }
                                        else if (b == 86)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "76:" + huruf + "85)";
                                        }
                                        else if (b == 88)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "64+" + huruf + "73+" + huruf + "86"; //"=B64+B73+B86";
                                        }
                                        else if (b == 97)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "93:" + huruf + "96)";//"=SUM(B93:B96)";

                                        }
                                        else if (b == 105)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "100:" + huruf + "104)";//"=SUM(B100:B104)";

                                        }
                                        else if (b == 106)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "97-" + huruf + "105"; //"=B97-B105";

                                        }
                                        else if (b == 114)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "110:" + huruf + "113)";//"=SUM(B110:B113)";

                                        }
                                        else if (b == 123)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "117:" + huruf + "122)";// "=SUM(B117:B122)";

                                        }
                                        else if (b == 125)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "114-" + huruf + "123"; // "=B114-B123";

                                        }
                                        else if (b == 126)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "106+" + huruf + "125"; // "=B106+B125";

                                        }
                                        else if (b == 146)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "130:" + huruf + "145)";// "=SUM(B130:B145)";

                                        }
                                        else if (b == 148)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "126-" + huruf + "146"; // "=B126-B146";

                                        }
                                        else if (b == 154)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "151:" + huruf + "153)";// "=SUM(B151:B153)";

                                        }
                                        else if (b == 155)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "148-" + huruf + "154"; // "=B148-B154";

                                        }
                                        else if (b == 159)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "157:" + huruf + "158)";//"=SUM(B157:B158)";

                                        }
                                        else if (b == 160)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "155+" + huruf + "159"; //"=B155+B159";

                                        }
                                        else if (b == 162)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "160+" + huruf + "161"; // "=B160+B161";
                                        }
                                        else if (b == 164)
                                        {
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "162-" + huruf + "163"; //"=B162-B163";
                                        }
                                        else if (b == 259)
                                        {
                                            // = B256 / (B13 + B14) * 100
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "256/(" + huruf + "13+" + huruf + "14) * 100";


                                        }
                                        else if (b == 262)
                                        {

                                            // worksheet.Cell(b, "B").FormulaA1 = "=-B16/(B13+B14)*100";
                                            worksheet.Cell(b, huruf).FormulaA1 = "=-" + huruf + "16/(" + huruf + "13+" + huruf + "14) * 100";


                                        }
                                        else if (b == 265)
                                        {
                                            //=B259-B262
                                            //worksheet.Cell(b, "B").FormulaA1 = "=B187-B190";
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "259-" + huruf + "262";

                                        }
                                        else if (b == 268)
                                        {
                                            //=B265*(B13+B14)/((B13+B14)+B16)
                                            worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "265*(" + huruf + "13+" + huruf + "14)/((" + huruf + "13+" + huruf + "14)+" + huruf + "16)";
                                            //worksheet.Cell(b, "B").FormulaA1 = "=B193*(B13+B14)/((B13+B14)+B16)";


                                        }
                                        else if (b == 84)
                                        {
                                            //=B265*(B13+B14)/((B13+B14)+B16)
                                            worksheet.Cell(b, "B").FormulaA1 = "=B164";
                                            //worksheet.Cell(b, "B").FormulaA1 = "=B193*(B13+B14)/((B13+B14)+B16)";


                                        }



                                    }

                                }


                                b = b + 1;
                                worksheet.Column(1).Width = 61.86;
                                worksheet.Column(column).Width = 20.29;
                                worksheet.Column(column).Style.NumberFormat.Format = "_(#,##0_)";
                                worksheet.Cell(259, column).Style.NumberFormat.Format = "0.00";
                                worksheet.Cell(262, column).Style.NumberFormat.Format = "0.00";
                                worksheet.Cell(265, column).Style.NumberFormat.Format = "0.00";
                                worksheet.Cell(268, column).Style.NumberFormat.Format = "0.00";



                            }








                        }








                    }

                    worksheet.Column(1).Width = 61.86;
                    worksheet.Column(column + 1).Width = 20.29;
                    worksheet.Column(column + 1).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Cell(259, column + 1).Style.NumberFormat.Format = "0.00";
                    worksheet.Cell(262, column + 1).Style.NumberFormat.Format = "0.00";
                    worksheet.Cell(265, column + 1).Style.NumberFormat.Format = "0.00";
                    worksheet.Cell(268, column + 1).Style.NumberFormat.Format = "0.00";
                    //sheet.Cells[1, 1], sheet.Cells[3, 3]

                  
                    worksheet.Range(worksheet.Cell(20, 2), worksheet.Cell(20, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(27, 2), worksheet.Cell(27, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(35, 2), worksheet.Cell(35, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(41, 2), worksheet.Cell(41, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(44, 2), worksheet.Cell(44, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(63, 2), worksheet.Cell(63, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(72, 2), worksheet.Cell(72, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(85, 2), worksheet.Cell(85, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(88, 2), worksheet.Cell(88, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(97, 2), worksheet.Cell(97, column )).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(105, 2), worksheet.Cell(105, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(114, 2), worksheet.Cell(114, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(123, 2), worksheet.Cell(123, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(126, 2), worksheet.Cell(126, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(146, 2), worksheet.Cell(146, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(154, 2), worksheet.Cell(154, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(159, 2), worksheet.Cell(159, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(162, 2), worksheet.Cell(162, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(164, 2), worksheet.Cell(164, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(173, 2), worksheet.Cell(173, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(181, 2), worksheet.Cell(181, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(190, 2), worksheet.Cell(190, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(199, 2), worksheet.Cell(199, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(222, 2), worksheet.Cell(222, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(230, 2), worksheet.Cell(230, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(235, 2), worksheet.Cell(235, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(239, 2), worksheet.Cell(239, column)).Style.Border.TopBorder = XLBorderStyleValues.Thin;



                    worksheet.Column(1).Width = 60;
                    worksheet.Column(2).Width = 40;
                    worksheet.Column(3).Width = 15;
                    worksheet.Column(4).Width = 15;
                    worksheet.Column(5).Width = 15;
                    worksheet.Column(6).Width = 15;
                    worksheet.Column(7).Width = 15;
                    worksheet.Column(8).Width = 15;
                    worksheet.Column(9).Width = 15;
                    worksheet.Column(10).Width = 15;
                    worksheet.Column(11).Width = 15;
                    worksheet.Column(12).Width = 15;
                    worksheet.Column(13).Width = 15;
                    worksheet.Column(14).Width = 15;
                    worksheet.Column(15).Width = 15;
                    worksheet.Column(16).Width = 15;
                    worksheet.Column(17).Width = 15;
                    worksheet.Column(18).Width = 15;
                    worksheet.Column(19).Width = 15;
                    worksheet.Column(20).Width = 15;
                    worksheet.Column(21).Width = 15;




                    worksheet.Row(1).Height = 16.5;
                    worksheet.Row(2).Height = 14.25;

                    worksheet.Row(3).Height = 12.75;
                    worksheet.Row(4).Height = 13.5;
                    worksheet.Row(5).Height = 13.5;
                    worksheet.Row(6).Height = 13.5;
                    worksheet.Row(7).Height = 13.5;
                    worksheet.Row(8).Height = 13.5;
                    worksheet.Row(9).Height = 13.5;
                    worksheet.Row(10).Height = 13.5;
                    worksheet.Row(11).Height = 13.5;
                    worksheet.Row(12).Height = 13.5;
                    worksheet.Row(13).Height = 13.5;
                    worksheet.Row(14).Height = 13.5;
                    worksheet.Row(15).Height = 13.5;
                    worksheet.Row(16).Height = 13.5;
                    worksheet.Row(17).Height = 13.5;
                    worksheet.Row(18).Height = 13.5;
                    worksheet.Row(19).Height = 13.5;
                    worksheet.Row(20).Height = 11.25;
                    worksheet.Row(21).Height = 13.5;
                    worksheet.Row(22).Height = 10.5;
                    worksheet.Row(23).Height = 13.5;
                    worksheet.Row(24).Height = 13.5;
                    worksheet.Row(25).Height = 13.5;
                    worksheet.Row(26).Height = 13.5;
                    worksheet.Row(27).Height = 11.25;
                    worksheet.Row(28).Height = 13.5;
                    worksheet.Row(29).Height = 10.5;
                    worksheet.Row(30).Height = 13.5;
                    worksheet.Row(31).Height = 13.5;
                    worksheet.Row(32).Height = 13.5;
                    worksheet.Row(33).Height = 13.5;
                    worksheet.Row(34).Height = 13.5;
                    worksheet.Row(35).Height = 12;
                    worksheet.Row(36).Height = 13.5;
                    worksheet.Row(37).Height = 11.25;
                    worksheet.Row(38).Height = 13.5;
                    worksheet.Row(39).Height = 13.5;
                    worksheet.Row(40).Height = 13.5;
                    worksheet.Row(41).Height = 11.25;
                    worksheet.Row(42).Height = 13.5;
                    worksheet.Row(43).Height = 12;
                    worksheet.Row(44).Height = 13.5;
                    worksheet.Row(45).Height = 11.25;
                    worksheet.Row(46).Height = 11.25;


                    worksheet.Row(47).Height = 15.75;//passwiva
                    worksheet.Row(48).Height = 13.5;
                    worksheet.Row(49).Height = 13.5;
                    worksheet.Row(50).Height = 13.5;
                    worksheet.Row(51).Height = 13.5;
                    worksheet.Row(52).Height = 13.5;
                    worksheet.Row(53).Height = 13.5;
                    worksheet.Row(54).Height = 13.5;
                    worksheet.Row(55).Height = 13.5;
                    worksheet.Row(56).Height = 13.5;
                    worksheet.Row(57).Height = 13.5;
                    worksheet.Row(58).Height = 13.5;
                    worksheet.Row(59).Height = 13.5;
                    worksheet.Row(60).Height = 13.5;

                    worksheet.Row(61).Height = 13.5;
                    worksheet.Row(62).Height = 13.5;
                    worksheet.Row(63).Height = 11.25;
                    worksheet.Row(64).Height = 13.5;
                    worksheet.Row(65).Height = 11.25;
                    worksheet.Row(66).Height = 13.5;
                    worksheet.Row(67).Height = 13.5;
                    worksheet.Row(68).Height = 13.5;
                    worksheet.Row(69).Height = 13.5;
                    worksheet.Row(70).Height = 13.5;
                    worksheet.Row(71).Height = 13.5;
                    worksheet.Row(72).Height = 11.25;
                    worksheet.Row(73).Height = 13.5;
                    worksheet.Row(74).Height = 10.5;
                    worksheet.Row(75).Height = 13.5;
                    worksheet.Row(76).Height = 13.5;
                    worksheet.Row(77).Height = 13.5;
                    worksheet.Row(78).Height = 13.5;
                    worksheet.Row(79).Height = 13.5;
                    worksheet.Row(80).Height = 13.5;
                    worksheet.Row(81).Height = 13.5;
                    worksheet.Row(82).Height = 13.5;
                    worksheet.Row(83).Height = 13.5;
                    worksheet.Row(84).Height = 13.5;
                    worksheet.Row(85).Height = 12;
                    worksheet.Row(86).Height = 13.5;
                    worksheet.Row(87).Height = 10.5;
                    worksheet.Row(88).Height = 13.5;









                    worksheet.Row(89).Height = 15.75;
                    worksheet.Row(90).Height = 15;
                    worksheet.Row(91).Height = 15;
                    worksheet.Row(92).Height = 15;
                    worksheet.Row(93).Height = 15;
                    worksheet.Row(94).Height = 15;
                    worksheet.Row(95).Height = 15;
                    worksheet.Row(96).Height = 15;
                    worksheet.Row(97).Height = 15;
                    worksheet.Row(98).Height = 15;
                    worksheet.Row(99).Height = 15;
                    worksheet.Row(100).Height = 15;

                    worksheet.Row(101).Height = 15;
                    worksheet.Row(102).Height = 15;
                    worksheet.Row(103).Height = 15;
                    worksheet.Row(104).Height = 15;
                    worksheet.Row(105).Height = 15;
                    worksheet.Row(106).Height = 15;
                    worksheet.Row(107).Height = 15;
                    worksheet.Row(108).Height = 15;
                    worksheet.Row(109).Height = 15;
                    worksheet.Row(110).Height = 15;
                    worksheet.Row(111).Height = 15;
                    worksheet.Row(112).Height = 15;
                    worksheet.Row(113).Height = 15;
                    worksheet.Row(114).Height = 15;
                    worksheet.Row(115).Height = 15;
                    worksheet.Row(116).Height = 15;
                    worksheet.Row(117).Height = 15;
                    worksheet.Row(118).Height = 15;
                    worksheet.Row(119).Height = 15;
                    worksheet.Row(120).Height = 15;
                    worksheet.Row(121).Height = 15;
                    worksheet.Row(122).Height = 15;
                    worksheet.Row(123).Height = 15;
                    worksheet.Row(124).Height = 15;
                    worksheet.Row(125).Height = 15;
                    worksheet.Row(126).Height = 15;
                    worksheet.Row(127).Height = 15;
                    worksheet.Row(128).Height = 15;
                    worksheet.Row(129).Height = 15;
                    worksheet.Row(130).Height = 15;
                    worksheet.Row(131).Height = 15;
                    worksheet.Row(132).Height = 15;
                    worksheet.Row(133).Height = 15;
                    worksheet.Row(134).Height = 15;
                    worksheet.Row(135).Height = 15;
                    worksheet.Row(136).Height = 15;
                    worksheet.Row(137).Height = 15;
                    worksheet.Row(138).Height = 15;
                    worksheet.Row(139).Height = 15;
                    worksheet.Row(140).Height = 15;
                    worksheet.Row(141).Height = 15;
                    worksheet.Row(142).Height = 15;
                    worksheet.Row(143).Height = 15;
                    worksheet.Row(144).Height = 15;
                    worksheet.Row(145).Height = 15;
                    worksheet.Row(146).Height = 15;
                    worksheet.Row(147).Height = 15;
                    worksheet.Row(148).Height = 15;
                    worksheet.Row(149).Height = 15;
                    worksheet.Row(150).Height = 15;
                    worksheet.Row(151).Height = 15;
                    worksheet.Row(152).Height = 15;
                    worksheet.Row(153).Height = 15;
                    worksheet.Row(154).Height = 15;
                    worksheet.Row(155).Height = 15;
                    worksheet.Row(156).Height = 15;
                    worksheet.Row(157).Height = 15;
                    worksheet.Row(158).Height = 15;
                    worksheet.Row(159).Height = 15;
                    worksheet.Row(160).Height = 15;

                    worksheet.Row(161).Height = 15;
                    worksheet.Row(162).Height = 15;
                    worksheet.Row(163).Height = 15;
                    worksheet.Row(164).Height = 15;
                    worksheet.Row(165).Height = 15.75;
                    worksheet.Row(166).Height = 15;
                    worksheet.Row(167).Height = 15;
                    worksheet.Row(168).Height = 15;
                    worksheet.Row(169).Height = 15;
                    worksheet.Row(170).Height = 15;
                    worksheet.Row(171).Height = 15;
                    worksheet.Row(172).Height = 15;
                    worksheet.Row(173).Height = 15;
                    worksheet.Row(174).Height = 15;
                    worksheet.Row(175).Height = 15;
                    worksheet.Row(176).Height = 15;
                    worksheet.Row(177).Height = 15;
                    worksheet.Row(178).Height = 15;
                    worksheet.Row(179).Height = 15;
                    worksheet.Row(180).Height = 15;
                    worksheet.Row(181).Height = 15;
                    worksheet.Row(182).Height = 15;
                    worksheet.Row(183).Height = 15;
                    worksheet.Row(184).Height = 15;
                    worksheet.Row(185).Height = 15;
                    worksheet.Row(186).Height = 15;
                    worksheet.Row(187).Height = 15;
                    worksheet.Row(188).Height = 15;
                    worksheet.Row(189).Height = 15;
                    worksheet.Row(190).Height = 15;
                    worksheet.Row(191).Height = 15;
                    worksheet.Row(192).Height = 15;
                    worksheet.Row(193).Height = 15;
                    worksheet.Row(194).Height = 15;
                    worksheet.Row(195).Height = 15;
                    worksheet.Row(196).Height = 15;
                    worksheet.Row(197).Height = 15;
                    worksheet.Row(198).Height = 15;
                    worksheet.Row(199).Height = 15;
                    worksheet.Row(200).Height = 15;




                    worksheet.Row(201).Height = 15;
                    worksheet.Row(202).Height = 15;
                    worksheet.Row(203).Height = 15;
                    worksheet.Row(204).Height = 15;
                    worksheet.Row(205).Height = 15;
                    worksheet.Row(206).Height = 15;
                    worksheet.Row(207).Height = 15;
                    worksheet.Row(208).Height = 15;
                    worksheet.Row(209).Height = 15;
                    worksheet.Row(210).Height = 15;
                    worksheet.Row(211).Height = 15;
                    worksheet.Row(212).Height = 15;
                    worksheet.Row(213).Height = 15;
                    worksheet.Row(214).Height = 15;
                    worksheet.Row(215).Height = 15;
                    worksheet.Row(216).Height = 15;
                    worksheet.Row(217).Height = 15;
                    worksheet.Row(218).Height = 15;
                    worksheet.Row(219).Height = 15;
                    worksheet.Row(220).Height = 15;
                    worksheet.Row(221).Height = 15;
                    worksheet.Row(222).Height = 15;
                    worksheet.Row(223).Height = 15;
                    worksheet.Row(224).Height = 15;
                    worksheet.Row(225).Height = 15;
                    worksheet.Row(226).Height = 15;
                    worksheet.Row(227).Height = 15;
                    worksheet.Row(228).Height = 15;
                    worksheet.Row(229).Height = 15;
                    worksheet.Row(230).Height = 15;
                    worksheet.Row(231).Height = 15;
                    worksheet.Row(232).Height = 15;
                    worksheet.Row(233).Height = 15;
                    worksheet.Row(234).Height = 15;
                    worksheet.Row(235).Height = 15;
                    worksheet.Row(236).Height = 15;
                    worksheet.Row(237).Height = 15;
                    worksheet.Row(238).Height = 15;
                    worksheet.Row(239).Height = 15;
                    worksheet.Row(240).Height = 15;
                    worksheet.Row(241).Height = 18;
                    worksheet.Row(242).Height = 15;
                    worksheet.Row(243).Height = 15;
                    worksheet.Row(244).Height = 15;
                    worksheet.Row(245).Height = 15;
                    worksheet.Row(246).Height = 15;
                    worksheet.Row(247).Height = 15;
                    worksheet.Row(248).Height = 15;
                    worksheet.Row(249).Height = 15;
                    worksheet.Row(250).Height = 15;
                    worksheet.Row(251).Height = 15;
                    worksheet.Row(252).Height = 15;
                    worksheet.Row(253).Height = 15;
                    worksheet.Row(254).Height = 15;
                    worksheet.Row(255).Height = 15;
                    worksheet.Row(256).Height = 15;
                    worksheet.Row(257).Height = 15;
                    worksheet.Row(258).Height = 15;
                    worksheet.Row(259).Height = 15;
                    worksheet.Row(260).Height = 15;

                    worksheet.Row(261).Height = 15;
                    worksheet.Row(262).Height = 15;
                    worksheet.Row(263).Height = 15;
                    worksheet.Row(264).Height = 15;
                    worksheet.Row(265).Height = 15;
                    worksheet.Row(266).Height = 15;
                    worksheet.Row(267).Height = 15;
                    worksheet.Row(268).Height = 15;
                    worksheet.Cell(2, "A").Style.Font.FontName = "Arial";
                    worksheet.Cell(2, "A").Style.Font.FontSize = 12;

                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.Margins.Top = 0.314961;
                    worksheet.PageSetup.Margins.Left = 0.11811;
                    worksheet.PageSetup.Margins.Right = 0.11811;
                    worksheet.PageSetup.Margins.Bottom = 0.314961;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;

                    string report = "";

                    report = "Report Berjalan This Month Cabang " + ddlbulan.SelectedValue + "-" + ddlTahun.SelectedValue + "-" + ddlcabang.SelectedItem.Text + ".xlsx";


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


            else if (DdlReport.SelectedValue == "5")
            {

                int column = 1;
                string query = "";
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

                //var TypeReport = ddltypereport.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@typereport", ParameterDirection.Input, SqlDbType.VarChar, 1, "", typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                var[6] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                var[7] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG_BERJALAN", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    //ds.Tables[0].TableName = "tbl_master";
                    XLWorkbook wb = new XLWorkbook();
                    string name = "";
                    //name=dt.Rows[i][0].;
                    name = "CABANG";

                    string hurufx = "";
                    int abjadlanjutx = 65;
                    int abjadawalx = 64;
                    string strAlphaawalx = "";
                    string strAlphalanjutx = "";
                    string alpha1x = "";
                    string alpha2x = "";

                    var worksheet = wb.Worksheets.Add(name);
                    worksheet.RowHeight = 13;

                    int bulanlalu = 0;
                    int bulanlalu2 = 0;

                    if (ddlbulan.SelectedValue == "1")
                    {
                        bulanlalu = 12;
                        bulanlalu2 = 11;

                    }
                    else
                    {
                        bulanlalu = int.Parse(ddlbulan.SelectedValue) - 1;
                        //bulanlalu2 = int.Parse(ddlbulan.SelectedValue) - 2;


                    }


                    for (int i = 0; i < ge.ListDataset.Tables[0].Rows.Count; i++)
                    {


                        if (i == 0)
                        {



                            for (int x1x = 1; x1x < (ge.ListDataset.Tables[0].Rows.Count + 2); x1x++)
                            {

                                if (abjadlanjutx == 90)
                                {
                                    abjadlanjutx = 65;
                                    abjadawalx = abjadawalx + 1;
                                }
                                if (abjadawalx == 64)
                                {
                                    strAlphaawalx = "";
                                    abjadlanjutx = abjadlanjutx + 1;
                                }
                                else
                                {
                                    if (strAlphaawalx == "")
                                    {
                                        strAlphaawalx = ((char)abjadawalx).ToString();
                                    }
                                    else
                                    {
                                        abjadlanjutx = abjadlanjutx + 1;
                                        strAlphaawalx = ((char)abjadawalx).ToString();

                                    }

                                }

                                strAlphalanjutx = strAlphaawalx + ((char)abjadlanjutx).ToString();




                            }
                            hurufx = strAlphalanjutx;
                            if ((abjadlanjutx - 1) < 65)
                            {
                                alpha2x = "Z";

                            }
                            else
                            {
                                alpha2x = strAlphaawalx + ((char)(abjadlanjutx - 1)).ToString();
                            }

                            if ((abjadlanjutx - 2) < 65)
                            {
                                alpha1x = "Z";

                            }
                            else
                            {
                                alpha1x = strAlphaawalx + ((char)(abjadlanjutx - 2)).ToString();
                            }


                            //alpha1x = strAlphaawalx + ((char)(abjadlanjutx - 3)).ToString();
                            //alpha2x = strAlphaawalx + ((char)(abjadlanjutx - 2)).ToString();

                        }
                        string xnama;
                        string ku_mingguan;
                        string noperkiraan;
                        string groupx;

                        string namakoperasi = ge.ListDataset.Tables[0].Rows[i]["nama_koperasi"].ToString().ToUpper();
                        string nama_area = ge.ListDataset.Tables[0].Rows[i]["nama_area"].ToString().ToUpper();
                        string cabang_nama = ge.ListDataset.Tables[0].Rows[i]["cabang_nama"].ToString().ToUpper();

                        string xxxx;
                        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[9];
                        varx[0] = gb.CreateSPParameter("@typereport", ParameterDirection.Input, SqlDbType.VarChar, 1, DdlReport.SelectedValue, typeof(string));
                        varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RPTKONSOLALL", typeof(string));
                        varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        varx[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ge.ListDataset.Tables[0].Rows[i][0].ToString(), typeof(string));
                        varx[5] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                        varx[6] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, "", typeof(string));
                        varx[7] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                        varx[8] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));

                        dt2.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG_BERJALAN", varx, "constrmf");
                        if (dt2.ListDataset.Tables[0].Rows.Count > 0)
                        {

                      

                            abjadlanjut = abjadlanjut + 1;
                            column = column + 1;
                            worksheet.Cell("A1").Value = namakoperasi + " - " + ddlbulan.SelectedItem.Text + " - " + ddlTahun.SelectedValue;
                            //x1 = x1 + 1;
                            ////Add DataTable as Worksheet.
                            

                            worksheet.Cell("A1").Style.Font.FontSize = 14;
                            worksheet.Cell("A1").Style.Font.FontName = "ARIAL";
                            worksheet.Cell("A1").Style.Font.Bold = true;

                            worksheet.Cell("A2").Value = "AKTIVA";
                            worksheet.Cell("A2").Style.Font.FontName = "ARIAL";
                            worksheet.Cell("A2").Style.Font.FontSize = 14;
                            worksheet.Cell("A2").Style.Font.Bold = true;


                            //worksheet.Cell("A3").Value = "AKTIVA";
                            //worksheet.Cell("A3").Style.Font.FontName = "ARIAL";
                            //worksheet.Cell("A3").Style.Font.FontSize = 12;
                            //worksheet.Cell("A3").Style.Font.Bold = true;



                            worksheet.Cell(3, column).Value = cabang_nama;
                            worksheet.Cell(3, column).Style.Font.FontName = "ARIAL";
                            worksheet.Cell(3, column).Style.Font.Italic = true;
                            worksheet.Cell(3, column).Style.Font.FontSize = 10;


                            int b;
                            int p;
                            b = 4;
                            p = 425;

                            if (abjadlanjut > 90)
                            {
                                abjadlanjut = 65;
                                abjadawal = abjadawal + 1;
                            }
                            if (abjadawal == 64)
                            {
                                strAlphaawal = "";
                            }
                            else
                            {
                                strAlphaawal = ((char)abjadawal).ToString();
                            }

                            strAlphalanjut = strAlphaawal + ((char)abjadlanjut).ToString();
                            huruf = strAlphalanjut;
                            huruf2 = strAlphaawal + ((char)(abjadlanjut - 1)).ToString();
                            //if (huruf == "Y")
                            //{
                            //    xxxx = "";
                            //}





                            //if (abjadlanjutx == 90)
                            //{
                            //    abjadlanjutx = 65;
                            //    abjadawalx = abjadawalx + 1;
                            //}
                            //if (abjadawalx == 64)
                            //{
                            //    strAlphaawalx = "";
                            //}
                            //else
                            //{
                            //    strAlphaawalx = ((char)abjadawalx).ToString();
                            //}

                            //strAlphalanjutx = strAlphaawalx + ((char)abjadlanjutx).ToString();


                            //abjadlanjutx = abjadlanjutx + 1;



                            for (int x = 0; x <dt2.ListDataset.Tables[0].Rows.Count; x++)
                            {

                                noperkiraan = dt2.ListDataset.Tables[0].Rows[x]["perkiraan_bahan"].ToString();
                                if (noperkiraan == "810000")
                                {
                                    b = 618;
                                }
                                if (noperkiraan == "311200")
                                {
                                    b = b + 1;
                                    p = p + 1;
                                }
                                if (noperkiraan == "311111")
                                {
                                    p = p + 1;
                                }

                                if (b == 51 || b == 90 || p == 230 || b == 190 || p == 228)
                                {
                                    if (p == 166 || p == 204)
                                    {

                                        worksheet.Cell(p, column).Value = cabang_nama;
                                        worksheet.Cell(p, column).Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(p, column).Style.Font.Italic = true;
                                        worksheet.Cell(p, column).Style.Font.FontSize = 10;


                                    }
                                    else
                                    {
                                        worksheet.Cell(b, column).Value = cabang_nama;
                                        worksheet.Cell(b, column).Style.Font.FontName = "ARIAL";
                                        worksheet.Cell(b, column).Style.Font.Italic = true;
                                        worksheet.Cell(b, column).Style.Font.FontSize = 10;




                                    }





                                }


                                if (b == 89)
                                {
                                    worksheet.Cell(b, "A").Value = "PASIVA";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 12;
                                    worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "ARIAL";


                                }

                                else if (b == 229)
                                {
                                    worksheet.Cell(b, "A").Value = "PROFIT  &  LOSS  CUMULATIVE";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 12;
                                    worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "ARIAL";

                                    worksheet.Cell(p, "A").Value = "PROFIT  &  LOSS  MONTHLY";
                                    worksheet.Cell(p, "A").Style.Font.FontSize = 12;
                                    worksheet.Cell(p, "A").Style.Font.Bold = true;
                                    worksheet.Cell(p, "A").Style.Font.FontName = "ARIAL";

                                }

                                else if (b == 618)
                                {
                                    b = b + 1;
                                    worksheet.Cell(b, "A").Value = "DATA - DATA PENUNJANG";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 14;
                                    worksheet.Cell(b, "A").Style.Font.Bold = true;
                                    worksheet.Cell(b, "A").Style.Font.FontName = "ARIAL";

                                    b = b + 2;
                                    worksheet.Cell(b, column).Value = cabang_nama;
                                    worksheet.Cell(b, column).Style.Font.FontName = "ARIAL";
                                    worksheet.Cell(b, column).Style.Font.Italic = true;
                                    worksheet.Cell(b, column).Style.Font.FontSize = 10;



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

                                    groupx = dt2.ListDataset.Tables[0].Rows[x]["groupx"].ToString();
                                    if (groupx == "C")
                                    {
                                        p = p + 1;
                                        if (noperkiraan != "311900" && noperkiraan != "311901" && noperkiraan != "311903" && noperkiraan != "311904" && noperkiraan != "311906")
                                        {
                                            worksheet.Cell(p, "A").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_bahan"].ToString();
                                            worksheet.Cell(p, "A").Style.Font.FontName = "Calibri";
                                            worksheet.Cell(p, "A").Style.Font.FontSize = 11;
                                            if (dt2.ListDataset.Tables[0].Rows[x]["type_font"].ToString() == "B")
                                            {
                                                worksheet.Cell(p, "A").Style.Font.Bold = true;
                                            }

                                            xnama = dt2.ListDataset.Tables[0].Rows[x]["kategori"].ToString();

                                            if (xnama == "M" || xnama == "U")
                                            {
                                                //String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));

                                                if ((p >= 428 && p <= 617))
                                                {

                                                    ku_mingguan = Convert.ToString(Convert.ToDecimal(dt2.ListDataset.Tables[0].Rows[x]["saldo_akhir_bi"].ToString()));
                                                }
                                                else
                                                {
                                                    ku_mingguan = Convert.ToString(Convert.ToDecimal(dt2.ListDataset.Tables[0].Rows[x]["saldo_akhir"].ToString()));
                                                }
                                                //if (dt2.ListDataset.Tables[0].Rows[x]["saldobulanlalu"].ToString() == null)
                                                //{

                                                //}
                                            
                                              

                                                //ku_mingguan =string.Format("{0:N2}", decimal.Parse(dt2.ListDataset.Tables[0].Rows[x]["saldobulanlalu"].ToString()));


                                                //if (column > 2)
                                                //{
                                                //    deltax = "="  +huruf + b.ToString()+ "-" + huruf2 + b.ToString();
                                                //    worksheet.Cell(p, column).FormulaA1 = deltax;
                                                //}
                                                //else
                                                //{ 
                                                worksheet.Cell(p, column).Value = ku_mingguan.Replace(",", ".");
                                                //}



                                              
                                                    worksheet.Cell(448, huruf).FormulaA1 = "=B429+B432+B433+B447";
                                                    worksheet.Cell(461, huruf).FormulaA1 = "=B451+B454+B458+B459";
                                                    worksheet.Cell(462, huruf).FormulaA1 = "=B448-B461";
                                                    worksheet.Cell(474, huruf).FormulaA1 = "=B466+B469+B470+B473";
                                                worksheet.Cell(491, huruf).FormulaA1 = "=B477+B480+B481+B486+B487+B490";
                                                worksheet.Cell(493, huruf).FormulaA1 = "=B474-B491";
                                                worksheet.Cell(494, huruf).FormulaA1 = "=B462+B493";
                                                worksheet.Cell(597, huruf).FormulaA1 = "=B498+B510+B511+B542+B543+B551+B552+B560+B561+B572+B573+B580+B582+B594+B595";
                                                worksheet.Cell(599, huruf).FormulaA1 = "=B494-B597";
                                                worksheet.Cell(608, huruf).FormulaA1 = "=B602+B605+B606";
                                                worksheet.Cell(609, huruf).FormulaA1 = "=B599-B608";
                                                worksheet.Cell(615, huruf).FormulaA1 = "=B611+B614";
                                                worksheet.Cell(616, huruf).FormulaA1 = "=B609-B615";

                                                //= B477 + B480 + B481 + B486 + B487 + B490

                                                //= B466 + B469 + B470 + B473
                                                // = B448 - B461
                                                //= B451 + B454 + B458 + B459


                                                //worksheet.Cell(b, hurufx).FormulaA1 = "=SUM(B" + b.ToString() + ":" + alpha2x + b.ToString() + ")";
                                                //worksheet.Cell(p, hurufx).FormulaA1 = "=SUM(B" + p.ToString() + ":" + alpha2x + p.ToString() + ")";


                                                //if (b == 21)
                                                //{

                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "5:" + huruf + "19)";

                                                //}
                                                //else if (b == 28)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "24:" + huruf + "27)";
                                                //}
                                                //else if (b == 36)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "31:" + huruf + "35)";
                                                //}
                                                //else if (b == 42)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "39:" + huruf + "41)";
                                                //}
                                                //else if (b == 44)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "21+" + huruf + "28+" + huruf + "36+" + huruf + "42";
                                                //}
                                                //else if (b == 64)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "50:" + huruf + "62)";
                                                //}
                                                //else if (b == 73)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "67:" + huruf + "72)";
                                                //}
                                                //else if (b == 86)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "76:" + huruf + "85)";
                                                //}
                                                //else if (b == 88)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "64+" + huruf + "73+" + huruf + "86"; //"=B64+B73+B86";
                                                //}
                                                //else if (b == 97)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "93:" + huruf + "96)";//"=SUM(B93:B96)";

                                                //}
                                                //else if (b == 105)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "100:" + huruf + "104)";//"=SUM(B100:B104)";

                                                //}
                                                //else if (b == 106)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "97-" + huruf + "105"; //"=B97-B105";

                                                //}
                                                //else if (b == 114)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "110:" + huruf + "113)";//"=SUM(B110:B113)";

                                                //}
                                                //else if (b == 123)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "117:" + huruf + "122)";// "=SUM(B117:B122)";

                                                //}
                                                //else if (b == 125)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "114-" + huruf + "123"; // "=B114-B123";

                                                //}
                                                //else if (b == 126)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "106+" + huruf + "125"; // "=B106+B125";

                                                //}
                                                //else if (b == 146)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "130:" + huruf + "145)";// "=SUM(B130:B145)";

                                                //}
                                                //else if (b == 148)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "126-" + huruf + "146"; // "=B126-B146";

                                                //}
                                                //else if (b == 154)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "151:" + huruf + "153)";// "=SUM(B151:B153)";

                                                //}
                                                //else if (b == 155)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "148-" + huruf + "154"; // "=B148-B154";

                                                //}
                                                //else if (b == 159)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "157:" + huruf + "158)";//"=SUM(B157:B158)";

                                                //}
                                                //else if (b == 160)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "155+" + huruf + "159"; //"=B155+B159";

                                                //}
                                                //else if (b == 162)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "160+" + huruf + "161"; // "=B160+B161";
                                                //}
                                                //else if (b == 164)
                                                //{
                                                //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "162-" + huruf + "163"; //"=B162-B163";
                                                //}



                                                //if (p == 173)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "169:" + huruf + "172)";// "=SUM(B169:B172)";
                                                //}
                                                //else if (p == 181)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "176:" + huruf + "180)";// "=SUM(B176:B180)";
                                                //}
                                                //else if (p == 182)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "173-" + huruf + "181"; //"=B173-B181";
                                                //}
                                                //else if (p == 190)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "186:" + huruf + "189)";// "=SUM(B186:B189)";
                                                //}
                                                //else if (p == 199)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "193:" + huruf + "198)";// "=SUM(B193:B198)";
                                                //}
                                                //else if (p == 201)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "190-" + huruf + "199"; //"=B190-B199";
                                                //}
                                                //else if (p == 202)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "182+" + huruf + "201"; //"=B182+B201";
                                                //}
                                                //else if (p == 222)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "206:" + huruf + "221)";//"=SUM(B206:B221)";
                                                //}
                                                //else if (p == 224)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "202-" + huruf + "222"; //"=B182+B201";"=B202-B222";
                                                //}
                                                //else if (p == 230)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "227:" + huruf + "229)";//"=SUM(B227:B229)";
                                                //}
                                                //else if (p == 231)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "224-" + huruf + "230"; // "=B224-B230";
                                                //}
                                                //else if (p == 235)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=SUM(" + huruf + "233:" + huruf + "234)";//"=SUM(B233:B234)";
                                                //}
                                                //else if (p == 236)
                                                //{
                                                //    worksheet.Cell(p, huruf).FormulaA1 = "=" + huruf + "231-" + huruf + "235"; // "=B231+B235";
                                                //}





                                            }

                                        }





                                    }






                                    worksheet.Cell(b, "A").Value = dt2.ListDataset.Tables[0].Rows[x]["nama_bahan"].ToString();
                                    worksheet.Cell(b, "A").Style.Font.FontName = "Calibri";
                                    worksheet.Cell(b, "A").Style.Font.FontSize = 11;
                                    xnama = dt2.ListDataset.Tables[0].Rows[x]["kategori"].ToString();

                                    if (xnama == "M" || xnama == "U")
                                    {
                                        //String.Format("{0:#,###0}", Convert.ToInt32(LblBahan.Text));

                                        ku_mingguan = Convert.ToString(Convert.ToDecimal(dt2.ListDataset.Tables[0].Rows[x]["saldo_akhir"].ToString()));


                                        worksheet.Cell(b, column).Value = ku_mingguan.Replace(",", ".");

                                        //deltax = "=B" + b.ToString() + "-" + alpha1x + b.ToString();
                                        // worksheet.Cell(b, hurufx).FormulaA1 = deltax;
                                        // deltax = "=" + alpha2x + p.ToString() + "-" + alpha1x + p.ToString();
                                        // worksheet.Cell(p, hurufx).FormulaA1 = deltax;
                                        //if (b == 637)
                                        //{
                                        //    // = B256 / (B13 + B14) * 100
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=" + hurufx + "634/(" + hurufx + "29+" + hurufx + "30) * 100";


                                        //}
                                        //else if (b == 262)
                                        //{

                                        //    // worksheet.Cell(b, "B").FormulaA1 = "=-B16/(B13+B14)*100";
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=-" + hurufx + "16/(" + hurufx + "13+" + hurufx + "14) * 100";


                                        //}
                                        //else if (b == 265)
                                        //{
                                        //    //=B259-B262
                                        //    //worksheet.Cell(b, "B").FormulaA1 = "=B187-B190";
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=" + hurufx + "259-" + hurufx + "262";

                                        //}
                                        //else if (b == 268)
                                        //{
                                        //    //=B265*(B13+B14)/((B13+B14)+B16)
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=" + hurufx + "265*(" + hurufx + "13+" + hurufx + "14)/((" + hurufx + "13+" + hurufx + "14)+" + hurufx + "16)";
                                        //    //worksheet.Cell(b, "B").FormulaA1 = "=B193*(B13+B14)/((B13+B14)+B16)";


                                        //}
                                        //else
                                        //{
                                        //    worksheet.Cell(b, hurufx).FormulaA1 = "=SUM(B" + b.ToString() + ":" + alpha2x + b.ToString() + ")";
                                        //}


                                        //if (b == 21)
                                        //{

                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "5:" + huruf + "19)";

                                        //}
                                        //else if (b == 28)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "24:" + huruf + "27)";
                                        //}
                                        //else if (b == 36)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "31:" + huruf + "35)";
                                        //}
                                        //else if (b == 42)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "39:" + huruf + "41)";
                                        //}
                                        //else if (b == 44)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "21+" + huruf + "28+" + huruf + "36+" + huruf + "42";
                                        //}
                                        //else if (b == 64)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "50:" + huruf + "62)";
                                        //}
                                        //else if (b == 73)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "67:" + huruf + "72)";
                                        //}
                                        //else if (b == 86)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "76:" + huruf + "85)";
                                        //}
                                        //else if (b == 88)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "64+" + huruf + "73+" + huruf + "86"; //"=B64+B73+B86";
                                        //}
                                        //else if (b == 97)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "93:" + huruf + "96)";//"=SUM(B93:B96)";

                                        //}
                                        //else if (b == 105)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "100:" + huruf + "104)";//"=SUM(B100:B104)";

                                        //}
                                        //else if (b == 106)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "97-" + huruf + "105"; //"=B97-B105";

                                        //}
                                        //else if (b == 114)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "110:" + huruf + "113)";//"=SUM(B110:B113)";

                                        //}
                                        //else if (b == 123)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "117:" + huruf + "122)";// "=SUM(B117:B122)";

                                        //}
                                        //else if (b == 125)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "114-" + huruf + "123"; // "=B114-B123";

                                        //}
                                        //else if (b == 126)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "106+" + huruf + "125"; // "=B106+B125";

                                        //}
                                        //else if (b == 146)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "130:" + huruf + "145)";// "=SUM(B130:B145)";

                                        //}
                                        //else if (b == 148)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "126-" + huruf + "146"; // "=B126-B146";

                                        //}
                                        //else if (b == 154)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "151:" + huruf + "153)";// "=SUM(B151:B153)";

                                        //}
                                        //else if (b == 155)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "148-" + huruf + "154"; // "=B148-B154";

                                        //}
                                        //else if (b == 159)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=SUM(" + huruf + "157:" + huruf + "158)";//"=SUM(B157:B158)";

                                        //}
                                        //else if (b == 160)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "155+" + huruf + "159"; //"=B155+B159";

                                        //}
                                        //else if (b == 162)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "160+" + huruf + "161"; // "=B160+B161";
                                        //}
                                        //else if (b == 164)
                                        //{
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "162-" + huruf + "163"; //"=B162-B163";
                                        //}
                                        //else if (b == 259)
                                        //{
                                        //    // = B256 / (B13 + B14) * 100
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "256/(" + huruf + "13+" + huruf + "14) * 100";


                                        //}
                                        //else if (b == 262)
                                        //{

                                        //    // worksheet.Cell(b, "B").FormulaA1 = "=-B16/(B13+B14)*100";
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=-" + huruf + "16/(" + huruf + "13+" + huruf + "14) * 100";


                                        //}
                                        //else if (b == 265)
                                        //{
                                        //    //=B259-B262
                                        //    //worksheet.Cell(b, "B").FormulaA1 = "=B187-B190";
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "259-" + huruf + "262";

                                        //}
                                        //else if (b == 268)
                                        //{
                                        //    //=B265*(B13+B14)/((B13+B14)+B16)
                                        //    worksheet.Cell(b, huruf).FormulaA1 = "=" + huruf + "265*(" + huruf + "13+" + huruf + "14)/((" + huruf + "13+" + huruf + "14)+" + huruf + "16)";
                                        //    //worksheet.Cell(b, "B").FormulaA1 = "=B193*(B13+B14)/((B13+B14)+B16)";


                                        //}
                                        //else if (b == 84)
                                        //{
                                        //    //=B265*(B13+B14)/((B13+B14)+B16)
                                        //    worksheet.Cell(b, "B").FormulaA1 = "=B164";
                                        //    //worksheet.Cell(b, "B").FormulaA1 = "=B193*(B13+B14)/((B13+B14)+B16)";


                                        //}



                                    }

                                }


                                b = b + 1;
                                worksheet.Column(1).Width = 61.86;
                                worksheet.Column(column).Width = 20.29;
                                worksheet.Column(column).Style.NumberFormat.Format = "_(#,##0_)";
                                worksheet.Cell(637, column).Style.NumberFormat.Format = "0.00";
                                worksheet.Cell(640, column).Style.NumberFormat.Format = "0.00";
                                worksheet.Cell(643, column).Style.NumberFormat.Format = "0.00";
                                worksheet.Cell(646, column).Style.NumberFormat.Format = "0.00";



                            }








                        }








                    }

                    worksheet.Column(1).Width = 61.86;
                    worksheet.Column(column + 1).Width = 20.29;
                    worksheet.Column(column + 1).Style.NumberFormat.Format = "_(#,##0_)";
                    worksheet.Cell(259, column + 1).Style.NumberFormat.Format = "0.00";
                    worksheet.Cell(262, column + 1).Style.NumberFormat.Format = "0.00";
                    worksheet.Cell(265, column + 1).Style.NumberFormat.Format = "0.00";
                    worksheet.Cell(268, column + 1).Style.NumberFormat.Format = "0.00";
                    //sheet.Cells[1, 1], sheet.Cells[3, 3]

                    //worksheet.Cell(3, hurufx).Value = "Total";
                    //worksheet.Cell(48, hurufx).Value = "Total";
                    //worksheet.Cell(90, hurufx).Value = "Total";
                    //worksheet.Cell(128, hurufx).Value = "Total";
                    //worksheet.Cell(166, hurufx).Value = "Total";
                    //worksheet.Cell(204, hurufx).Value = "Total";
                    //worksheet.Cell(243, hurufx).Value = "Total";

                    //worksheet.Cell(21, hurufx).FormulaA1 = "=SUM(" + hurufx + "5:" + hurufx + "19)";
                    //worksheet.Cell(28, hurufx).FormulaA1 = "=SUM(" + hurufx + "24:" + hurufx + "27)";
                    //worksheet.Cell(36, hurufx).FormulaA1 = "=SUM(" + hurufx + "31:" + hurufx + "35)";
                    //worksheet.Cell(42, hurufx).FormulaA1 = "=SUM(" + hurufx + "39:" + hurufx + "41)";
                    //worksheet.Cell(44, hurufx).FormulaA1 = "=" + hurufx + "21+" + hurufx + "28+" + hurufx + "36+" + hurufx + "42";
                    //worksheet.Cell(64, hurufx).FormulaA1 = "=SUM(" + hurufx + "50:" + hurufx + "62)";
                    //worksheet.Cell(73, hurufx).FormulaA1 = "=SUM(" + hurufx + "67:" + hurufx + "72)";
                    //worksheet.Cell(86, hurufx).FormulaA1 = "=SUM(" + hurufx + "76:" + hurufx + "85)";
                    //worksheet.Cell(88, hurufx).FormulaA1 = "=" + hurufx + "64+" + hurufx + "73+" + hurufx + "86"; //"=B64+B73+B86";
                    //worksheet.Cell(97, hurufx).FormulaA1 = "=SUM(" + hurufx + "93:" + hurufx + "96)";//"=SUM(B93:B96)";
                    //worksheet.Cell(105, hurufx).FormulaA1 = "=SUM(" + hurufx + "100:" + hurufx + "104)";//"=SUM(B100:B104)";
                    //worksheet.Cell(106, hurufx).FormulaA1 = "=" + hurufx + "97-" + hurufx + "105"; //"=B97-B105";
                    //worksheet.Cell(173, hurufx).FormulaA1 = "=SUM(" + hurufx + "169:" + hurufx + "172)";// "=SUM(B169:B172)";

                    //worksheet.Cell(181, hurufx).FormulaA1 = "=SUM(" + hurufx + "176:" + hurufx + "180)";// "=SUM(B176:B180)";

                    //worksheet.Cell(182, hurufx).FormulaA1 = "=" + hurufx + "173-" + hurufx + "181"; //"=B173-B181";
                    //worksheet.Cell(114, hurufx).FormulaA1 = "=SUM(" + hurufx + "110:" + hurufx + "113)";//"=SUM(B110:B113)";
                    //worksheet.Cell(190, hurufx).FormulaA1 = "=SUM(" + hurufx + "186:" + hurufx + "189)";// "=SUM(B186:B189)";
                    //worksheet.Cell(123, hurufx).FormulaA1 = "=SUM(" + hurufx + "117:" + hurufx + "122)";// "=SUM(B117:B122)";
                    //worksheet.Cell(199, hurufx).FormulaA1 = "=SUM(" + hurufx + "193:" + hurufx + "198)";// "=SUM(B193:B198)";
                    //worksheet.Cell(125, hurufx).FormulaA1 = "=" + hurufx + "114-" + hurufx + "123"; // "=B114-B123";
                    //worksheet.Cell(201, hurufx).FormulaA1 = "=" + hurufx + "190-" + hurufx + "199"; //"=B190-B199";
                    //worksheet.Cell(126, hurufx).FormulaA1 = "=" + hurufx + "106+" + hurufx + "125"; // "=B106+B125";
                    //worksheet.Cell(202, hurufx).FormulaA1 = "=" + hurufx + "182+" + hurufx + "201"; //"=B182+B201";
                    //worksheet.Cell(146, hurufx).FormulaA1 = "=SUM(" + hurufx + "130:" + hurufx + "145)";// "=SUM(B130:B145)";
                    //worksheet.Cell(222, hurufx).FormulaA1 = "=SUM(" + hurufx + "206:" + hurufx + "221)";//"=SUM(B206:B221)";
                    //worksheet.Cell(148, hurufx).FormulaA1 = "=" + hurufx + "126-" + hurufx + "146"; // "=B126-B146";
                    //worksheet.Cell(224, hurufx).FormulaA1 = "=" + hurufx + "202-" + hurufx + "222"; //"=B182+B201";"=B202-B222";
                    //worksheet.Cell(154, hurufx).FormulaA1 = "=SUM(" + hurufx + "151:" + hurufx + "153)";// "=SUM(B151:B153)";
                    //worksheet.Cell(230, hurufx).FormulaA1 = "=SUM(" + hurufx + "227:" + hurufx + "229)";//"=SUM(B227:B229)";
                    //worksheet.Cell(155, hurufx).FormulaA1 = "=" + hurufx + "148-" + hurufx + "154"; // "=B148-B154";
                    //worksheet.Cell(231, hurufx).FormulaA1 = "=" + hurufx + "224-" + hurufx + "230"; // "=B224-B230";
                    //worksheet.Cell(159, hurufx).FormulaA1 = "=SUM(" + hurufx + "157:" + hurufx + "158)";//"=SUM(B157:B158)";
                    //worksheet.Cell(235, hurufx).FormulaA1 = "=SUM(" + hurufx + "233:" + hurufx + "234)";//"=SUM(B233:B234)";
                    //worksheet.Cell(160, hurufx).FormulaA1 = "=" + hurufx + "155+" + hurufx + "159"; //"=B155+B159";
                    //worksheet.Cell(236, hurufx).FormulaA1 = "=" + hurufx + "231-" + hurufx + "235"; // "=B231+B235";
                    //worksheet.Cell(162, hurufx).FormulaA1 = "=" + hurufx + "160+" + hurufx + "161"; // "=B160+B161";
                    ////worksheet.Cell(164, hurufx).FormulaA1 = "=" + hurufx + "162-" + hurufx + "163"; //"=B162-B163";
                    //worksheet.Range(worksheet.Cell(20, 2), worksheet.Cell(20, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(27, 2), worksheet.Cell(27, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(35, 2), worksheet.Cell(35, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(41, 2), worksheet.Cell(41, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(44, 2), worksheet.Cell(44, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(63, 2), worksheet.Cell(63, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(72, 2), worksheet.Cell(72, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(85, 2), worksheet.Cell(85, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(88, 2), worksheet.Cell(88, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(97, 2), worksheet.Cell(97, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(105, 2), worksheet.Cell(105, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(114, 2), worksheet.Cell(114, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(123, 2), worksheet.Cell(123, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(126, 2), worksheet.Cell(126, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(146, 2), worksheet.Cell(146, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(154, 2), worksheet.Cell(154, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(159, 2), worksheet.Cell(159, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(162, 2), worksheet.Cell(162, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(164, 2), worksheet.Cell(164, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(173, 2), worksheet.Cell(173, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(181, 2), worksheet.Cell(181, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(190, 2), worksheet.Cell(190, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(199, 2), worksheet.Cell(199, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(222, 2), worksheet.Cell(222, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(230, 2), worksheet.Cell(230, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(235, 2), worksheet.Cell(235, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    //worksheet.Range(worksheet.Cell(239, 2), worksheet.Cell(239, column + 1)).Style.Border.TopBorder = XLBorderStyleValues.Thin;



                    worksheet.Column(1).Width = 50;
                    worksheet.Column(2).Width = 15;
                    worksheet.Column(3).Width = 15;
                    worksheet.Column(4).Width = 15;
                    worksheet.Column(5).Width = 15;
                    worksheet.Column(6).Width = 15;
                    worksheet.Column(7).Width = 15;
                    worksheet.Column(8).Width = 15;
                    worksheet.Column(9).Width = 15;
                    worksheet.Column(10).Width = 15;
                    worksheet.Column(11).Width = 15;
                    worksheet.Column(12).Width = 15;
                    worksheet.Column(13).Width = 15;
                    worksheet.Column(14).Width = 15;
                    worksheet.Column(15).Width = 15;
                    worksheet.Column(16).Width = 15;
                    worksheet.Column(17).Width = 15;
                    worksheet.Column(18).Width = 15;
                    worksheet.Column(19).Width = 15;
                    worksheet.Column(20).Width = 15;
                    worksheet.Column(21).Width = 15;




                    worksheet.Row(1).Height = 16.5;
                    worksheet.Row(2).Height = 14.25;

                    worksheet.Row(3).Height = 12.75;
                    worksheet.Row(4).Height = 13.5;
                    worksheet.Row(5).Height = 13.5;
                    worksheet.Row(6).Height = 13.5;
                    worksheet.Row(7).Height = 13.5;
                    worksheet.Row(8).Height = 13.5;
                    worksheet.Row(9).Height = 13.5;
                    worksheet.Row(10).Height = 13.5;
                    worksheet.Row(11).Height = 13.5;
                    worksheet.Row(12).Height = 13.5;
                    worksheet.Row(13).Height = 13.5;
                    worksheet.Row(14).Height = 13.5;
                    worksheet.Row(15).Height = 13.5;
                    worksheet.Row(16).Height = 13.5;
                    worksheet.Row(17).Height = 13.5;
                    worksheet.Row(18).Height = 13.5;
                    worksheet.Row(19).Height = 13.5;
                    worksheet.Row(20).Height = 11.25;
                    worksheet.Row(21).Height = 13.5;
                    worksheet.Row(22).Height = 10.5;
                    worksheet.Row(23).Height = 13.5;
                    worksheet.Row(24).Height = 13.5;
                    worksheet.Row(25).Height = 13.5;
                    worksheet.Row(26).Height = 13.5;
                    worksheet.Row(27).Height = 11.25;
                    worksheet.Row(28).Height = 13.5;
                    worksheet.Row(29).Height = 10.5;
                    worksheet.Row(30).Height = 13.5;
                    worksheet.Row(31).Height = 13.5;
                    worksheet.Row(32).Height = 13.5;
                    worksheet.Row(33).Height = 13.5;
                    worksheet.Row(34).Height = 13.5;
                    worksheet.Row(35).Height = 12;
                    worksheet.Row(36).Height = 13.5;
                    worksheet.Row(37).Height = 11.25;
                    worksheet.Row(38).Height = 13.5;
                    worksheet.Row(39).Height = 13.5;
                    worksheet.Row(40).Height = 13.5;
                    worksheet.Row(41).Height = 11.25;
                    worksheet.Row(42).Height = 13.5;
                    worksheet.Row(43).Height = 12;
                    worksheet.Row(44).Height = 13.5;
                    worksheet.Row(45).Height = 11.25;
                    worksheet.Row(46).Height = 11.25;


                    worksheet.Row(47).Height = 15.75;//passwiva
                    worksheet.Row(48).Height = 13.5;
                    worksheet.Row(49).Height = 13.5;
                    worksheet.Row(50).Height = 13.5;
                    worksheet.Row(51).Height = 13.5;
                    worksheet.Row(52).Height = 13.5;
                    worksheet.Row(53).Height = 13.5;
                    worksheet.Row(54).Height = 13.5;
                    worksheet.Row(55).Height = 13.5;
                    worksheet.Row(56).Height = 13.5;
                    worksheet.Row(57).Height = 13.5;
                    worksheet.Row(58).Height = 13.5;
                    worksheet.Row(59).Height = 13.5;
                    worksheet.Row(60).Height = 13.5;

                    worksheet.Row(61).Height = 13.5;
                    worksheet.Row(62).Height = 13.5;
                    worksheet.Row(63).Height = 11.25;
                    worksheet.Row(64).Height = 13.5;
                    worksheet.Row(65).Height = 11.25;
                    worksheet.Row(66).Height = 13.5;
                    worksheet.Row(67).Height = 13.5;
                    worksheet.Row(68).Height = 13.5;
                    worksheet.Row(69).Height = 13.5;
                    worksheet.Row(70).Height = 13.5;
                    worksheet.Row(71).Height = 13.5;
                    worksheet.Row(72).Height = 11.25;
                    worksheet.Row(73).Height = 13.5;
                    worksheet.Row(74).Height = 10.5;
                    worksheet.Row(75).Height = 13.5;
                    worksheet.Row(76).Height = 13.5;
                    worksheet.Row(77).Height = 13.5;
                    worksheet.Row(78).Height = 13.5;
                    worksheet.Row(79).Height = 13.5;
                    worksheet.Row(80).Height = 13.5;
                    worksheet.Row(81).Height = 13.5;
                    worksheet.Row(82).Height = 13.5;
                    worksheet.Row(83).Height = 13.5;
                    worksheet.Row(84).Height = 13.5;
                    worksheet.Row(85).Height = 12;
                    worksheet.Row(86).Height = 13.5;
                    worksheet.Row(87).Height = 10.5;
                    worksheet.Row(88).Height = 13.5;









                    worksheet.Row(89).Height = 15.75;
                    worksheet.Row(90).Height = 15;
                    worksheet.Row(91).Height = 15;
                    worksheet.Row(92).Height = 15;
                    worksheet.Row(93).Height = 15;
                    worksheet.Row(94).Height = 15;
                    worksheet.Row(95).Height = 15;
                    worksheet.Row(96).Height = 15;
                    worksheet.Row(97).Height = 15;
                    worksheet.Row(98).Height = 15;
                    worksheet.Row(99).Height = 15;
                    worksheet.Row(100).Height = 15;

                    worksheet.Row(101).Height = 15;
                    worksheet.Row(102).Height = 15;
                    worksheet.Row(103).Height = 15;
                    worksheet.Row(104).Height = 15;
                    worksheet.Row(105).Height = 15;
                    worksheet.Row(106).Height = 15;
                    worksheet.Row(107).Height = 15;
                    worksheet.Row(108).Height = 15;
                    worksheet.Row(109).Height = 15;
                    worksheet.Row(110).Height = 15;
                    worksheet.Row(111).Height = 15;
                    worksheet.Row(112).Height = 15;
                    worksheet.Row(113).Height = 15;
                    worksheet.Row(114).Height = 15;
                    worksheet.Row(115).Height = 15;
                    worksheet.Row(116).Height = 15;
                    worksheet.Row(117).Height = 15;
                    worksheet.Row(118).Height = 15;
                    worksheet.Row(119).Height = 15;
                    worksheet.Row(120).Height = 15;
                    worksheet.Row(121).Height = 15;
                    worksheet.Row(122).Height = 15;
                    worksheet.Row(123).Height = 15;
                    worksheet.Row(124).Height = 15;
                    worksheet.Row(125).Height = 15;
                    worksheet.Row(126).Height = 15;
                    worksheet.Row(127).Height = 15;
                    worksheet.Row(128).Height = 15;
                    worksheet.Row(129).Height = 15;
                    worksheet.Row(130).Height = 15;
                    worksheet.Row(131).Height = 15;
                    worksheet.Row(132).Height = 15;
                    worksheet.Row(133).Height = 15;
                    worksheet.Row(134).Height = 15;
                    worksheet.Row(135).Height = 15;
                    worksheet.Row(136).Height = 15;
                    worksheet.Row(137).Height = 15;
                    worksheet.Row(138).Height = 15;
                    worksheet.Row(139).Height = 15;
                    worksheet.Row(140).Height = 15;
                    worksheet.Row(141).Height = 15;
                    worksheet.Row(142).Height = 15;
                    worksheet.Row(143).Height = 15;
                    worksheet.Row(144).Height = 15;
                    worksheet.Row(145).Height = 15;
                    worksheet.Row(146).Height = 15;
                    worksheet.Row(147).Height = 15;
                    worksheet.Row(148).Height = 15;
                    worksheet.Row(149).Height = 15;
                    worksheet.Row(150).Height = 15;
                    worksheet.Row(151).Height = 15;
                    worksheet.Row(152).Height = 15;
                    worksheet.Row(153).Height = 15;
                    worksheet.Row(154).Height = 15;
                    worksheet.Row(155).Height = 15;
                    worksheet.Row(156).Height = 15;
                    worksheet.Row(157).Height = 15;
                    worksheet.Row(158).Height = 15;
                    worksheet.Row(159).Height = 15;
                    worksheet.Row(160).Height = 15;

                    worksheet.Row(161).Height = 15;
                    worksheet.Row(162).Height = 15;
                    worksheet.Row(163).Height = 15;
                    worksheet.Row(164).Height = 15;
                    worksheet.Row(165).Height = 15.75;
                    worksheet.Row(166).Height = 15;
                    worksheet.Row(167).Height = 15;
                    worksheet.Row(168).Height = 15;
                    worksheet.Row(169).Height = 15;
                    worksheet.Row(170).Height = 15;
                    worksheet.Row(171).Height = 15;
                    worksheet.Row(172).Height = 15;
                    worksheet.Row(173).Height = 15;
                    worksheet.Row(174).Height = 15;
                    worksheet.Row(175).Height = 15;
                    worksheet.Row(176).Height = 15;
                    worksheet.Row(177).Height = 15;
                    worksheet.Row(178).Height = 15;
                    worksheet.Row(179).Height = 15;
                    worksheet.Row(180).Height = 15;
                    worksheet.Row(181).Height = 15;
                    worksheet.Row(182).Height = 15;
                    worksheet.Row(183).Height = 15;
                    worksheet.Row(184).Height = 15;
                    worksheet.Row(185).Height = 15;
                    worksheet.Row(186).Height = 15;
                    worksheet.Row(187).Height = 15;
                    worksheet.Row(188).Height = 15;
                    worksheet.Row(189).Height = 15;
                    worksheet.Row(190).Height = 15;
                    worksheet.Row(191).Height = 15;
                    worksheet.Row(192).Height = 15;
                    worksheet.Row(193).Height = 15;
                    worksheet.Row(194).Height = 15;
                    worksheet.Row(195).Height = 15;
                    worksheet.Row(196).Height = 15;
                    worksheet.Row(197).Height = 15;
                    worksheet.Row(198).Height = 15;
                    worksheet.Row(199).Height = 15;
                    worksheet.Row(200).Height = 15;




                    worksheet.Row(201).Height = 15;
                    worksheet.Row(202).Height = 15;
                    worksheet.Row(203).Height = 15;
                    worksheet.Row(204).Height = 15;
                    worksheet.Row(205).Height = 15;
                    worksheet.Row(206).Height = 15;
                    worksheet.Row(207).Height = 15;
                    worksheet.Row(208).Height = 15;
                    worksheet.Row(209).Height = 15;
                    worksheet.Row(210).Height = 15;
                    worksheet.Row(211).Height = 15;
                    worksheet.Row(212).Height = 15;
                    worksheet.Row(213).Height = 15;
                    worksheet.Row(214).Height = 15;
                    worksheet.Row(215).Height = 15;
                    worksheet.Row(216).Height = 15;
                    worksheet.Row(217).Height = 15;
                    worksheet.Row(218).Height = 15;
                    worksheet.Row(219).Height = 15;
                    worksheet.Row(220).Height = 15;
                    worksheet.Row(221).Height = 15;
                    worksheet.Row(222).Height = 15;
                    worksheet.Row(223).Height = 15;
                    worksheet.Row(224).Height = 15;
                    worksheet.Row(225).Height = 15;
                    worksheet.Row(226).Height = 15;
                    worksheet.Row(227).Height = 15;
                    worksheet.Row(228).Height = 15;
                    worksheet.Row(229).Height = 15;
                    worksheet.Row(230).Height = 15;
                    worksheet.Row(231).Height = 15;
                    worksheet.Row(232).Height = 15;
                    worksheet.Row(233).Height = 15;
                    worksheet.Row(234).Height = 15;
                    worksheet.Row(235).Height = 15;
                    worksheet.Row(236).Height = 15;
                    worksheet.Row(237).Height = 15;
                    worksheet.Row(238).Height = 15;
                    worksheet.Row(239).Height = 15;
                    worksheet.Row(240).Height = 15;
                    worksheet.Row(241).Height = 18;
                    worksheet.Row(242).Height = 15;
                    worksheet.Row(243).Height = 15;
                    worksheet.Row(244).Height = 15;
                    worksheet.Row(245).Height = 15;
                    worksheet.Row(246).Height = 15;
                    worksheet.Row(247).Height = 15;
                    worksheet.Row(248).Height = 15;
                    worksheet.Row(249).Height = 15;
                    worksheet.Row(250).Height = 15;
                    worksheet.Row(251).Height = 15;
                    worksheet.Row(252).Height = 15;
                    worksheet.Row(253).Height = 15;
                    worksheet.Row(254).Height = 15;
                    worksheet.Row(255).Height = 15;
                    worksheet.Row(256).Height = 15;
                    worksheet.Row(257).Height = 15;
                    worksheet.Row(258).Height = 15;
                    worksheet.Row(259).Height = 15;
                    worksheet.Row(260).Height = 15;

                    worksheet.Row(261).Height = 15;
                    worksheet.Row(262).Height = 15;
                    worksheet.Row(263).Height = 15;
                    worksheet.Row(264).Height = 15;
                    worksheet.Row(265).Height = 15;
                    worksheet.Row(266).Height = 15;
                    worksheet.Row(267).Height = 15;
                    worksheet.Row(268).Height = 15;
                    worksheet.Cell(2, "A").Style.Font.FontName = "Arial";
                    worksheet.Cell(2, "A").Style.Font.FontSize = 12;

                    worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    worksheet.PageSetup.Margins.Top = 0.314961;
                    worksheet.PageSetup.Margins.Left = 0.11811;
                    worksheet.PageSetup.Margins.Right = 0.11811;
                    worksheet.PageSetup.Margins.Bottom = 0.314961;

                    worksheet.PageSetup.Margins.Header = 0;
                    worksheet.PageSetup.Margins.Footer = 0;

                    string report = "";

                    report = "Report Berjalan This Month Cabang " + ddlbulan.SelectedValue + "-" + ddlTahun.SelectedValue + "-" + ddlcabang.SelectedItem.Text + ".xlsx";

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