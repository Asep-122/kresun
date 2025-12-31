using CrystalDecisions.CrystalReports.Engine;
using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Kresun_View_CetakTandaTerimaKredit : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    //string noRekening = "", plafon = "", tgljt = "", namahari = "", today = "", tglDrop = "", tglJatuhTempo = "", noNasabah = "";
    //DateTime tgl_realisasi, tgl_jt;

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
        public Action<object, EventArgs, string, string> Handler { get; set; }
        public string ReportPath { get; set; }
        public string CategoryTandaTerima { get; set; }
    }

    private Dictionary<string, CetakAction> _cetakActions;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        _cetakActions = new Dictionary<string, CetakAction>();

        try
        {
            DefaultClass.Global.SqlHelper[] prm = new DefaultClass.Global.SqlHelper[0];
            ge.ListDataset = gb.GetDataJustOutPut("KP_GET_MAPPING_CETAKAN_KWITANSI", prm, "constrmf");

            if (ge.ListDataset != null && ge.ListDataset.Tables.Count > 0)
            {
                DataTable dt = ge.ListDataset.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string kodeBank = row["KodeBank"].ToString();
                    string categoryTandaTerima = row["Kategori"].ToString();
                    string reportPath = row["ReportPath"].ToString();

                    Action<object, EventArgs, string, string> handler = CategoryTandaTerima_Generic;

                    /* kalau ada pengecualian (custom handler)
                    switch (kodeBank)
                    {
                        case "A002":
                            handler = CategoryTandaTerima_PembiayaanSendiri;
                            break;
                    } */

                    _cetakActions[kodeBank] = new CetakAction
                    {
                        Handler = handler,
                        ReportPath = reportPath,
                        CategoryTandaTerima = categoryTandaTerima
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
        scriptManager.RegisterPostBackControl(this.btnSimpan);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakTandaTerimaKredit.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakTandaTerimaKredit.aspx");
    }

    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETDATATANDATERIMA", typeof(string));
        var[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "tanggal_pengajuan desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData_Draw_DataTable();

    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HDNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
        HDRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
        HDNKodeBank.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebank")).Text;
        pnladd.Visible = true; 
        PnlFront.Visible = false; 
    }


    private void CategoryTandaTerima_Generic(object sender, EventArgs e, string reportPath, string kategoriTandaTerima)
    {
        CetakCategoryTandaTerima(
            e,
            reportPath,
            action: kategoriTandaTerima + "_PREVIEW",
            categoryTandaTerima: kategoriTandaTerima,
            exportFileName: kategoriTandaTerima,
            includeNoRekening: true
        );
    }


    private void CetakCategoryTandaTerima(
        EventArgs e,
        string reportPath,
        string action,
        string categoryTandaTerima,
        string exportFileName,
        bool includeNoRekening = false
    )
    {
        string noRekening = HDRekening.Value;
        string noNasabah = HDNasabah.Value;

        using (ReportDocument rprt = new ReportDocument())
        {
            if (noNasabah.Substring(0, 3) == SessBranchID)
            {
                var paramList = new List<DefaultClass.Global.SqlHelper>
                {
                    gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, action, typeof(string)),
                    gb.CreateSPParameter("@kategori", ParameterDirection.Input, SqlDbType.VarChar, 50, categoryTandaTerima, typeof(string)),
                    gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, noRekening, typeof(string)),
                    gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, noNasabah, typeof(string)),
                    gb.CreateSPParameter("@session_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string)),
                    gb.CreateSPParameter("@session_branch_id", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)),
                    gb.CreateSPParameter("@manager_cabang", ParameterDirection.Input, SqlDbType.VarChar, 150, txtpejabat1.Text, typeof(string)),
                    gb.CreateSPParameter("@admin_area", ParameterDirection.Input, SqlDbType.VarChar, 150, txtpejabat2.Text, typeof(string)),
                    gb.CreateSPParameter("@head_area", ParameterDirection.Input, SqlDbType.VarChar, 150, txtpejabat3.Text, typeof(string))
                };

                ge.ListDataTable = gb.GetDataJustOutPut("KP_CETAK_KWITANSI_TANDA_TERIMA_PENCAIRAN_PEMBIAYAAN", paramList.ToArray(), "constrmf").Tables[0];

                rprt.Load(reportPath);
                rprt.SetDataSource(ge.ListDataTable);

                Response.ClearContent();
                Response.ClearHeaders();

                ReportHelper.ExportPdfInline(rprt, exportFileName, Response);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor bayar tidak sesuai!');", true);
            }
        }
    }

    protected void btnSimpan_Click(object sender, EventArgs e) {
        try
        {
            string kodeBank = HDNKodeBank.Value;
            if (_cetakActions.ContainsKey(kodeBank))
            {
                var action = _cetakActions[kodeBank];
                string reportPath = Server.MapPath(action.ReportPath);

                if (!System.IO.File.Exists(reportPath))
                    throw new FileNotFoundException("Report file not found", reportPath);

                action.Handler(sender, e, reportPath, action.CategoryTandaTerima);
            }
            else
            {
                throw new NotImplementedException("Belum ada handler untuk kode: " + kodeBank);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message + "');", true);
        }
        finally
        {
            GRData_Draw_DataTable();
        }
    }

    private void GRData_Draw_DataTable()
    {
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}