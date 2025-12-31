using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Kresun_View_CetakPK : System.Web.UI.Page
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

    private class CetakAction
    {
        public Action<object, GridViewCommandEventArgs, string, string> Handler { get; set; }
        public string ReportPath { get; set; }
        public string CategoryPk { get; set; }
    }

    private Dictionary<string, CetakAction> _cetakActions;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        _cetakActions = new Dictionary<string, CetakAction>();

        try
        {
            DefaultClass.Global.SqlHelper[] prm = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("KP_GET_MAPPING_CETAKAN_PK", prm, "constrmf");

            if (ge.ListDataset != null && ge.ListDataset.Tables.Count > 0)
            {
                DataTable dt = ge.ListDataset.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string kodeBank = row["KodeBank"].ToString();
                    string categoryPk = row["CategoryPK"].ToString();
                    string reportPath = row["ReportPath"].ToString();

                    Action<object, GridViewCommandEventArgs, string, string> handler = CetakPerjanjianKredit_Generic;

                    // kalau ada pengecualian (custom handler)
                    //switch (kodeBank)
                    //{
                    //    case "A002":
                    //        handler = CetakPerjanjianKredit_Umum;
                    //        break;
                    //}
                    switch (categoryPk)
                    {
                        case "PK_UMUM":
                            handler = CetakPerjanjianKredit_Umum;
                            break;
                    }

                    _cetakActions[kodeBank] = new CetakAction
                    {
                        Handler = handler,
                        ReportPath = reportPath,
                        CategoryPk = categoryPk
                    };
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridEntity();
        }

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.GRData);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakPK.aspx");
    }

    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVIEWAPPROVAL", typeof(string));
            var[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop desc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();
            GRData_Draw_DataTable();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }

    }

    private void GRData_Draw_DataTable()
    {
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string kodeBank = e.CommandName.Trim();
            if (_cetakActions.ContainsKey(kodeBank))
            {
                var action = _cetakActions[kodeBank];
                string reportPath = Server.MapPath(action.ReportPath);

                if (!System.IO.File.Exists(reportPath))
                    throw new FileNotFoundException("Report file not found", reportPath);

                action.Handler(sender, e, reportPath, action.CategoryPk);
            }
            else
            {
                throw new NotImplementedException("Belum ada handler untuk kode: " + kodeBank);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message + "');", true);
            GRData_Draw_DataTable();
        }
    }

    private void CetakPerjanjianKredit_Generic(object sender, GridViewCommandEventArgs e, string reportPath, string kategoriPk)
    {
        CetakPerjanjianKredit(
            e,
            reportPath,
            action: kategoriPk + "_PREVIEW",
            kategoriPk: kategoriPk,
            exportFileName: kategoriPk + "_" + e.CommandName,
            includeNoRekening: true
        );
    }

    private void CetakPerjanjianKredit(
        GridViewCommandEventArgs e,
        string reportPath,
        string action,
        string kategoriPk,
        string exportFileName,
        bool includeNoRekening = false
    )
    {
        string kodeBank = e.CommandName;
        string noRekening = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
        string noNasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;

        using (ReportDocument rprt = new ReportDocument())
        {
            if (noNasabah.Substring(0, 3) == SessBranchID)
            {
                var paramList = new List<DefaultClass.Global.SqlHelper>
                {
                    gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, action, typeof(string)),
                    gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, noNasabah, typeof(string)),
                    gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string)),
                    gb.CreateSPParameter("@kode_bank", ParameterDirection.Input, SqlDbType.VarChar, 5, kodeBank, typeof(string)),
                    gb.CreateSPParameter("@kategori_pk", ParameterDirection.Input, SqlDbType.VarChar, 50, kategoriPk, typeof(string))
                };

                if (includeNoRekening)
                {
                    paramList.Add(gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, noRekening, typeof(string)));
                }

                ge.ListDataTable = gb.GetDataJustOutPut("KP_CETAK_PERJANJIAN_KREDIT", paramList.ToArray(), "constrmf").Tables[0];

                rprt.Load(reportPath);
                rprt.SetDataSource(ge.ListDataTable);

                // Export ke stream
                ReportHelper.ExportPdfInline(rprt, exportFileName, Response);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor bayar tidak sesuai!');", true);
            }

            rprt.Close();
            //rprt.Dispose();
        }
    }

    private void CetakPerjanjianKredit_Umum(object sender, GridViewCommandEventArgs e, string reportPath, string kategoriPk)
    {

        ReportDocument rprt = new ReportDocument();

        noRekening = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
        plafon = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text.Replace(".", "");
        tgljt = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgljatuhtempo")).Text;
        noNasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;

        if (noNasabah.Substring(0, 3) == SessBranchID)
        {
            ge.ListDataset = gb.GetDataFunction("dbo.GetDayName()", "constrmf");

            namahari = ge.ListDataset.Tables[0].Rows[0]["hari"].ToString();
            today = ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString();

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
            var1[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, noRekening, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKPKKRESUN", typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var1, "constrmf").Tables[0];

            rprt.Load(reportPath);

            rprt.SetDataSource(ge.ListDataTable);

            tgl_realisasi = DateTime.Parse(ge.ListDataTable.Rows[0]["tgl_drop"].ToString());
            tglDrop = tgl_realisasi.ToString("dd/MM/yyyy");

            tgl_jt = DateTime.Parse(ge.ListDataTable.Rows[0]["tgl_due"].ToString());
            tglJatuhTempo = tgl_realisasi.ToString("dd/MM/yyyy");

            rprt.SetParameterValue("hari", namahari);
            rprt.SetParameterValue("terbilangplafon", gb.Terbilang(decimal.Parse(plafon)));
            rprt.SetParameterValue("terbilangbunga", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][3].ToString())));
            rprt.SetParameterValue("terbilangprovisi", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][18].ToString())));
            rprt.SetParameterValue("terbilangasuransi", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][19].ToString())));
            rprt.SetParameterValue("terbilangjangka", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][4].ToString())));
            rprt.SetParameterValue("terbilangangsuran", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][7].ToString())));
            rprt.SetParameterValue("terbilangdenda", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][9].ToString())));

            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

            Response.End();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Rekening tidak terdapat di cabang anda!');", true);
            GRData_Draw_DataTable();
        }
    }
}