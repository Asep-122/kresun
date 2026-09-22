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
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
public partial class Kresun_View_CetakAnalisaDanPenawaranKredit : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    string noRekening = "", plafon = "", tgljt = "", namahari = "", today = "", tglDrop = "", tglJatuhTempo = "", noNasabah = "";
    DateTime tgl_realisasi, tgl_jt;


    public string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }

    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridEntity();
        }

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnSimpan);

        ScriptManager a = ScriptManager.GetCurrent(this.Page);
        a.RegisterPostBackControl(this.btncetak);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakAnalisaDanPenawaranKredit.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakAnalisaDanPenawaranKredit.aspx");
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETDATANALISA", typeof(string));
        var[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tanggal_pengajuan desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HDRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
        HDNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
        pnladd.Visible = true;
        pnlnasabah.Visible = true;
        PnlFront.Visible = false;

        TextBox1.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnip")).Text;
        TextBox2.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
    }

    /**
    protected void btncetak_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string today = dateTime.ToString("dd/MM/yyyy");

            if (HDNasabah.Value.Substring(0, 3) == SessBranchID)
            {
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
                var1[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 20, HDRekening.Value, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPENAWARANKRDT", typeof(string));

                ge.ListDataTable = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var1, "constrmf").Tables[0];

                rprt.Load(Server.MapPath("~/Kresun/Report/CetakPenawaranKredit.rpt"));

                rprt.SetDataSource(ge.ListDataTable);

                rprt.SetParameterValue("pejabat1", txtpejabat1.Text);
                rprt.SetParameterValue("user", SessLoginID);
                rprt.SetParameterValue("terbilang", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][3].ToString())));

                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "application/pdf";
                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                Response.End();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Rekening tidak terdapat di cabang anda!');", true);
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
                rprt.Dispose();
                rprt.Close();
            }
            //UpdateFlag(HDRekening.Value, "2");
        }
    } */

    protected void btncetak_Click(object sender, EventArgs e)
    {
        string _noRekening = HDRekening.Value;

        string reportPath = Server.MapPath("~/Kresun/Report/AnalisaKredit/KP_PERSETUJUAN_PEMBERIAN_KREDIT.rpt");
        using (ReportDocument rprt = new ReportDocument())
        {
            var paramList2 = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAK_PERSETUJUAN_PEMBERIAN_KREDIT", typeof(string)),
                gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, _noRekening, typeof(string)),
                gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
                gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)),
                gb.CreateSPParameter("@pejabat_1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpejabat1.Text, typeof(string)),
                gb.CreateSPParameter("@pejabat_2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpejabat2.Text, typeof(string))
            };
            ge.ListDataTable = gb.GetDataJustOutPut("KP_CETAK_ANALISA_PERSETUJUAN_PEMBERIAN_KREDIT", paramList2.ToArray(), "constrmf").Tables[0];

            if (!System.IO.File.Exists(reportPath))
                throw new FileNotFoundException("Report file not found", reportPath);

            rprt.Load(reportPath);

            rprt.SetDataSource(ge.ListDataTable);

            Response.ClearContent();
            Response.ClearHeaders();
            ReportHelper.ExportPdfInline(rprt, "PERSETUJUAN_KREDIT_" + _noRekening, Response);
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string _noRekening = HDRekening.Value;


        string reportPath = string.Empty;
        using (ReportDocument rprt = new ReportDocument())
        {
            var paramList = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAK_ANALISA", typeof(string)),
                gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, _noRekening, typeof(string)),
                gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
                gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)),
                gb.CreateSPParameter("@pejabat_1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpejabat1.Text, typeof(string)),
                gb.CreateSPParameter("@pejabat_2", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpejabat2.Text, typeof(string))
            };
            ge.ListDataTable = gb.GetDataJustOutPut("KP_CETAK_ANALISA_PERSETUJUAN_PEMBERIAN_KREDIT", paramList.ToArray(), "constrmf").Tables[0];

            if (ge.ListDataTable.Rows[0]["sumberdana"].ToString() == "A001")
            {
                reportPath = Server.MapPath("~/Kresun/Report/AnalisaKredit/KP_ANALISA_DAN_PERSETUJUAN_PEMBIAYAAN.rpt");
            }            
            else if (ge.ListDataTable.Rows[0]["Jenis_produk"].ToString() == "5")
            {
                reportPath = Server.MapPath("~/Kresun/Report/AnalisaKredit/KP_ANALISA_DAN_PERSETUJUAN_PEMBIAYAAN_KONVEN_THT.rpt");
            }
            else
            {
                reportPath = Server.MapPath("~/Kresun/Report/AnalisaKredit/KP_ANALISA_DAN_PERSETUJUAN_PEMBIAYAAN_KONVEN.rpt");
            }

            if (!System.IO.File.Exists(reportPath))
                throw new FileNotFoundException("Report file not found", reportPath);
            rprt.Load(reportPath);

            rprt.SetDataSource(ge.ListDataTable);



            Response.ClearContent();
            Response.ClearHeaders();
            ReportHelper.ExportPdfInline(rprt, "ANALISA_" + _noRekening, Response);
        }
    }
    /**
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {

            DateTime dateTime = DateTime.UtcNow.Date;
            string today = dateTime.ToString("dd/MM/yyyy");

            if (HDNasabah.Value.Substring(0, 3) == SessBranchID)
            {
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
                var1[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 20, HDRekening.Value, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKANALISAKREDIT", typeof(string));

                ge.ListDataTable = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var1, "constrmf").Tables[0];

                rprt.Load(Server.MapPath("~/Kresun/Report/CetakAnalisaPersetujuanKreditSendiri.rpt"));

                rprt.SetDataSource(ge.ListDataTable);

                rprt.SetParameterValue("today", today);
                rprt.SetParameterValue("pejabat1", txtpejabat1.Text);
                rprt.SetParameterValue("pejabat2", txtpejabat2.Text);
                rprt.SetParameterValue("userlogin", SessLoginID);

                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "application/pdf";
                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Rekening tidak terdapat di cabang anda!');", true);
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
                rprt.Dispose();
                rprt.Close();
            }
            ///UpdateFlag(HDRekening.Value, "1");
        }
    }*/

    public void UpdateFlag(string noRekening, string Flag)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] varSTATE = new DefaultClass.Global.SqlHelper[4];
            varSTATE[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 250, noRekening, typeof(string));
            varSTATE[1] = gb.CreateSPParameter("@flagupdate", ParameterDirection.Input, SqlDbType.VarChar, 250, Flag, typeof(string));
            varSTATE[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 250, "UPDATESTATUS", typeof(string));
            varSTATE[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_VERIFIKASI_PINJAMAN", varSTATE, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Rekening tidak terdapat di cabang anda!');", true);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    /** Example Merged Version (sub report)
    private void CetakCategoryTandaTerima(string _noRekening)
    {
        DataTable dataSourceAnalisa = new DataTable();
        DataTable dataSourcePersetujuanKredit = new DataTable();

        string reportPath = Server.MapPath("~/Kresun/Report/AnalisaKredit/KP_ANALISA_DAN_PERSETUJUAN_PEMBIAYAAN.rpt");
        using (ReportDocument rprt = new ReportDocument())
        {
            var paramList = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAK_ANALISA", typeof(string)),
                gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, _noRekening, typeof(string)),
                gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
                gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string))
            };
            dataSourceAnalisa = gb.GetDataJustOutPut("KP_CETAK_ANALISA_PERSETUJUAN_PEMBERIAN_KREDIT", paramList.ToArray(), "constrmf").Tables[0];
            dataSourceAnalisa.TableName = "KP_CETAK_ANALISA";

            var paramList2 = new List<DefaultClass.Global.SqlHelper>
            {
                gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAK_PERSETUJUAN_PEMBERIAN_KREDIT", typeof(string)),
                gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, _noRekening, typeof(string)),
                gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
                gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string))
            };
            dataSourcePersetujuanKredit = gb.GetDataJustOutPut("KP_CETAK_ANALISA_PERSETUJUAN_PEMBERIAN_KREDIT", paramList2.ToArray(), "constrmf").Tables[0];
            dataSourcePersetujuanKredit.TableName = "KP_CETAK_PERSETUJUAN_KREDIT";

            if (!System.IO.File.Exists(reportPath))
                throw new FileNotFoundException("Report file not found", reportPath);

            // Load report utama
            rprt.Load(reportPath);

            // Binding main report (Analisa)
            rprt.SetDataSource(dataSourceAnalisa);

            // Binding subreport (Persetujuan) dengan nama filenya
            rprt.Subreports["KP_PERSETUJUAN_PEMBERIAN_KREDIT"].SetDataSource(dataSourcePersetujuanKredit);

            // Export ke PDF
            Response.ClearContent();
            Response.ClearHeaders();
            ReportHelper.ExportPdfInline(rprt, "ANALISA_PERSETUJUAN_KREDIT_" + _noRekening, Response);
        }
    } */
}