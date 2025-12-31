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
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Drawing.Diagrams;

public partial class AE_Report_ReportTagihanPM : System.Web.UI.Page
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
                //ddlsp_SelectedIndexChanged(sender, e);
                BindGridEntity();
               
               
                
                txtEnd.Text = now.ToString("dd-MM-yyyy");
                
                txtEnd.Enabled = true;
                
            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), System.IO.Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetCabangReport", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
        ddlcabang.Items.Insert(0, new ListItem("ALL CABANG", "ALL"));
        ddlcabang.SelectedValue = SessBranchID;

        if (SessBranchID == "000")
        {
            ddlcabang.Enabled = true;
        }
        else
        {
            ddlcabang.Enabled = false;
        }
    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {
                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[7];
                    varx[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                    varx[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAGIHAN", typeof(string));
                    varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[4] = gb.CreateSPParameter("@tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 10, txtEnd.Text, typeof(DateTime));
                    varx[5] = gb.CreateSPParameter("@statusbyr", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlstatusByr.SelectedValue, typeof(string));
                    varx[6] = gb.CreateSPParameter("@Potgaji", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypedata.SelectedValue, typeof(string));
                    dt2.ListDataset = gb.GetDataJustOutPut("AE_STD_PM_XLS", varx, "constrmf");
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
                name = "Tagihan_PM_" + ddlcabang.SelectedValue + "_" + ddlstatusByr.SelectedValue;

                //DateTime now = DateTime.Parse(txttglhariini.Text);
                XLWorkbook wb = new XLWorkbook();
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




                worksheet.Cell("A1").Value = "LAPORAN TAGIHAN PINJAMAN MANAGEMENT";
                worksheet.Cell("A1").Style.Font.FontSize = 11;
                worksheet.Cell("A1").Style.Font.FontName = "Calibri";
                worksheet.Cell("A1").Style.Font.Bold = true;
                worksheet.Cell("A2").Value = "STATUS";
                worksheet.Cell("A2").Style.Font.FontName = "Calibri";
                worksheet.Cell("A2").Style.Font.FontSize = 11;
                worksheet.Cell("A2").Style.Font.Bold = true;
                worksheet.Cell("A3").Value = "Koperasi";
                worksheet.Cell("A3").Style.Font.FontName = "Calibri";
                worksheet.Cell("A3").Style.Font.FontSize = 11;
                worksheet.Cell("A3").Style.Font.Bold = true;
                worksheet.Cell("A4").Value = "Area";
                worksheet.Cell("A4").Style.Font.FontName = "Calibri";
                worksheet.Cell("A4").Style.Font.FontSize = 11;
                worksheet.Cell("A4").Style.Font.Bold = true;
                worksheet.Cell("A5").Value = "Cabang";
                worksheet.Cell("A5").Style.Font.FontName = "Calibri";
                worksheet.Cell("A5").Style.Font.FontSize = 11;
                worksheet.Cell("A5").Style.Font.Bold = true;

                worksheet.Cell("A6").Value = "Jatuh Tempo";
                worksheet.Cell("A6").Style.Font.FontName = "Calibri";
                worksheet.Cell("A6").Style.Font.FontSize = 11;
                worksheet.Cell("A6").Style.Font.Bold = true;

                worksheet.Cell("B2").Value = "'" + ddlstatusByr.SelectedItem.Text ;

                if (ddlcabang.SelectedValue == "ALL")
                {
                    worksheet.Cell("B2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B2").Style.Font.FontSize = 11;
                    worksheet.Cell("B2").Style.Font.Bold = true;
                    worksheet.Cell("B3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B3").Style.Font.FontSize = 11;
                    worksheet.Cell("B3").Style.Font.Bold = true;
                    worksheet.Cell("B3").Value = "ALL";
                    worksheet.Cell("B4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B4").Style.Font.FontSize = 11;
                    worksheet.Cell("B4").Value = "ALL";
                    worksheet.Cell("B4").Style.Font.Bold = true;
                    worksheet.Cell("B5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B5").Style.Font.FontSize = 11;
                    worksheet.Cell("B5").Value = "ALL";
                    worksheet.Cell("B5").Style.Font.Bold = true;
                    worksheet.Cell("B6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B6").Style.Font.FontSize = 11;
                    worksheet.Cell("B6").Value = txtEnd.Text;
                    worksheet.Cell("B6").Style.Font.Bold = true;
                }
                else
                {
                    worksheet.Cell("B2").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B2").Style.Font.FontSize = 11;
                    worksheet.Cell("B2").Style.Font.Bold = true;
                    worksheet.Cell("B3").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B3").Style.Font.FontSize = 11;
                    worksheet.Cell("B3").Style.Font.Bold = true;
                    worksheet.Cell("B3").Value = dt2.ListDataset.Tables[0].Rows[0]["nama_koperasi"].ToString();
                    worksheet.Cell("B4").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B4").Style.Font.FontSize = 11;
                    worksheet.Cell("B4").Value = dt2.ListDataset.Tables[0].Rows[0]["nama_area"].ToString();
                    worksheet.Cell("B4").Style.Font.Bold = true;
                    worksheet.Cell("B5").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B5").Style.Font.FontSize = 11;
                    worksheet.Cell("B5").Value = dt2.ListDataset.Tables[0].Rows[0]["cabang_nama"].ToString();
                    worksheet.Cell("B5").Style.Font.Bold = true;
                    worksheet.Cell("B6").Style.Font.FontName = "Calibri";
                    worksheet.Cell("B6").Style.Font.FontSize = 11;
                    worksheet.Cell("B6").Value = txtEnd.Text;
                    worksheet.Cell("B6").Style.Font.Bold = true;
                }

                

                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Border.TopBorder = XLBorderStyleValues.Thin;


                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Alignment.WrapText = true;

                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Font.FontName = "Calibri";
                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Font.FontSize = 11;
                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 16)).Style.Fill.BackgroundColor = XLColor.PaleAqua;




                worksheet.Column(1).Width = 9.89;
                worksheet.Column(2).Width = 12.33;
                worksheet.Column(3).Width = 22.89;
                worksheet.Column(4).Width = 4.33;
                worksheet.Column(5).Width = 17.11;
                worksheet.Column(6).Width = 18.67;
                worksheet.Column(7).Width = 17.11;
                worksheet.Column(8).Width = 17.11;
                worksheet.Column(9).Width = 17.11;
                worksheet.Column(10).Width = 17.11;
                worksheet.Column(11).Width = 0;
                worksheet.Column(12).Width = 6.89;
                worksheet.Column(13).Width = 17.11;
                worksheet.Column(14).Width = 9.89;
                worksheet.Column(15).Width = 17.11;
                worksheet.Column(16).Width = 9.78;
                worksheet.Column(11).Hide();


                worksheet.Range(worksheet.Cell(8, 1), worksheet.Cell(8, 1)).Value = "NO.";
                worksheet.Range(worksheet.Cell(8, 2), worksheet.Cell(8, 2)).Value = "Tgl Tagihan";
                worksheet.Range(worksheet.Cell(8, 3), worksheet.Cell(8, 3)).Value = "No. Tagihan";
                worksheet.Range(worksheet.Cell(8, 4), worksheet.Cell(8, 4)).Value = "Type";
                worksheet.Range(worksheet.Cell(8, 5), worksheet.Cell(8, 5)).Value = "Cabang";
                worksheet.Range(worksheet.Cell(8, 6), worksheet.Cell(8, 6)).Value = "Kode Nik";
                worksheet.Range(worksheet.Cell(8, 7), worksheet.Cell(8, 7)).Value = "Nama Karyawan";
                worksheet.Range(worksheet.Cell(8, 8), worksheet.Cell(8, 8)).Value = "Bagian";
                worksheet.Range(worksheet.Cell(8, 9), worksheet.Cell(8, 9)).Value = "Jabatan";
                worksheet.Range(worksheet.Cell(8, 10), worksheet.Cell(8, 10)).Value = "Golongan";
                worksheet.Range(worksheet.Cell(8, 11), worksheet.Cell(8, 11)).Value = "";
                worksheet.Range(worksheet.Cell(8, 12), worksheet.Cell(8, 12)).Value = "Angs Ke";
                worksheet.Range(worksheet.Cell(8, 13), worksheet.Cell(8, 13)).Value = "Jumlah Angsuran";
                worksheet.Range(worksheet.Cell(8, 14), worksheet.Cell(8, 14)).Value = "Status";
                worksheet.Range(worksheet.Cell(8, 15), worksheet.Cell(8, 15)).Value = "No Bayar";
                worksheet.Range(worksheet.Cell(8, 16), worksheet.Cell(8, 16)).Value = "Tgl Byr";


               

                int b;
                int p;
                b = 8;
                p = 0;


                        for (int x = 0; x < dt2.ListDataset.Tables[0].Rows.Count; x++)
                        {


                            b = b + 1;
                            p = p + 1;


                            worksheet.Cell(b, "A").Value = p;
                            worksheet.Cell(b, "B").Value = dt2.ListDataset.Tables[0].Rows[x]["TglTrn"].ToString();
                            worksheet.Cell(b, "C").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NoBukti"].ToString(); ;
                            worksheet.Cell(b, "D").Value = dt2.ListDataset.Tables[0].Rows[x]["typetrn"].ToString();
                            worksheet.Cell(b, "E").Value = dt2.ListDataset.Tables[0].Rows[x]["cabang_nama"].ToString();
                            worksheet.Cell(b, "F").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KodeNik"].ToString();
                            worksheet.Cell(b, "G").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["NamaKry"].ToString();
                            worksheet.Cell(b, "H").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KetBagian"].ToString();
                            worksheet.Cell(b, "I").Value = "'" + dt2.ListDataset.Tables[0].Rows[x]["KetJab"].ToString();
                            worksheet.Cell(b, "J").Value = dt2.ListDataset.Tables[0].Rows[x]["ketgolongan"].ToString();
                            worksheet.Cell(b, "K").Value = "";
                            worksheet.Cell(b, "L").Value = dt2.ListDataset.Tables[0].Rows[x]["angske"].ToString();
                            worksheet.Cell(b, "M").Value = dt2.ListDataset.Tables[0].Rows[x]["JlhAngsuran"].ToString().Replace(",", ".");
                            worksheet.Cell(b, "N").Value = dt2.ListDataset.Tables[0].Rows[x]["statuslunas"].ToString();
                            worksheet.Cell(b, "O").Value = dt2.ListDataset.Tables[0].Rows[x]["no_tagihan"].ToString();
                            worksheet.Cell(b, "P").Value = dt2.ListDataset.Tables[0].Rows[x]["tglbayar"].ToString();
                        
                        }

                        

                        //worksheet.Range(worksheet.Cell(13, 13), worksheet.Cell(b, 13)).Style.NumberFormat.Format = "_(#,##0_)";
                        worksheet.Column(2).Style.NumberFormat.Format = "dd/mm/yyyy";
                        worksheet.Column(16).Style.NumberFormat.Format = "dd/mm/yyyy";
                        worksheet.Column(13).Style.NumberFormat.Format = "_(#,##0_)";








                worksheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                worksheet.PageSetup.Margins.Top = 0.11811;
                worksheet.PageSetup.Margins.Left = 0.314961;
                worksheet.PageSetup.Margins.Right = 0.314961;
                worksheet.PageSetup.Margins.Bottom = 0.11811;

                worksheet.PageSetup.Margins.Header = 0;
                worksheet.PageSetup.Margins.Footer = 0;





                int pakhir = 0;
                pakhir = b + 1;
                worksheet.Cell(pakhir, "C").Value = "Total";



                worksheet.Cell(pakhir, "M").FormulaA1 = "=SUM(M8:M" + b + ")";
                
                worksheet.Column(13).Style.NumberFormat.Format = "_(#,##0_)";
                

                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 16)).Style.Font.FontName = "Calibri";
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 16)).Style.Font.FontSize = 11;
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 16)).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(pakhir, 1), worksheet.Cell(pakhir, 16)).Style.Fill.BackgroundColor = XLColor.PaleAqua;



                string report = "";
                //if (ddlproduk.SelectedValue == "3C")
                //{
                report = "Tagihan_PM_" + ddlcabang.SelectedItem.Text + "_" + ddlstatusByr.SelectedItem.Text + ".xlsx";
                //}
                //else if (ddlproduk.SelectedValue == "3B")
                //{
                //    report = "Report Listtagihan Bulanan " + xtanggal.ToString() + ".xlsx";
                //}



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

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak ditemukan!');", true);
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
 




}