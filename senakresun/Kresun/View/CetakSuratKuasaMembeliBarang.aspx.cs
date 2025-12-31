using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Kresun_View_CetakSuratKuasaMembeliBarang : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    //public string BaseUrl
    //{
    //    get { return Session["baseUrl"].ToString(); }
    //    set { Session["baseUrl"] = value; }
    //}
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
        if (Session["SessLoginID"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (!Page.IsPostBack)
        {
            BindGridEntity();
        }

        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnViewKuasa);
        scriptManager.RegisterPostBackControl(this.btnViewPernyataanDanKuasa);

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakSuratKuasaMembeliBarang.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakSuratKuasaMembeliBarang.aspx");
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 50, "KUASA_PEMBELIAN_BARANG_LIST", typeof(string));
        var[1] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KPKuasaMembeliBarang", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        string noNasabah = ((Label)GRData.Rows[rowIndex].FindControl("lblnonasabah")).Text;
        string kantorBayar = e.CommandName;
        HDNasabah.Value = noNasabah;
        HDKantorBayar.Value = kantorBayar;

        switch (kantorBayar)
        {
            case "A001":
                // Aksi untuk BPRS HIK
                pnlKuasa.Visible = false;
                pnlPernyataanDanKuasa.Visible = true;
                PnlFront.Visible = false;
                clearpnlKuasa();
                break;

            case "A002":
                // Aksi untuk Pembiayaan Sendiri
                pnlKuasa.Visible = true;
                pnlPernyataanDanKuasa.Visible = false;
                PnlFront.Visible = false;
                clearpnlPernyataanDanKuasa();
                break;

            default:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor bayar tidak sesuai!');", true);
                break;
        }
    }

    private void clearpnlKuasa()
    {
        txtkabis.Text = "";
        //txtNamaBarang.Text = "";
        //txtJenisBarang.Text = "";
        //txtJumlah.Text = "";
        //txtAlamatSuppplier.Text = "";
        //txtHarga.Text = "";
    }
    private void clearpnlPernyataanDanKuasa()
    {
        txtKeperluan.Text = "";
    }

    protected void btnViewKuasa_Click(object sender, EventArgs e)
    {
        
        try
        {
            ReportDocument rprt = new ReportDocument();

            if (HDNasabah.Value.Substring(0, 3) == SessBranchID && HDKantorBayar.Value.ToString() == "A002")
            {
                DefaultClass.Global.SqlHelper[] paramns = new DefaultClass.Global.SqlHelper[4];
                paramns[0] = gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 100, "KUASA_PEMBELIAN_BARANG_PREVIEW", typeof(string));
                paramns[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNasabah.Value, typeof(string));
                paramns[2] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                paramns[3] = gb.CreateSPParameter("@principal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkabis.Text, typeof(string));

                ge.ListDataTable = gb.GetDataJustOutPut("KPKuasaMembeliBarang", paramns, "constrmf").Tables[0];

                var path = Server.MapPath("~/Kresun/Report/Report/KUASA/KuasaMembeliBarang.rpt");
                if (!System.IO.File.Exists(path))
                {
                    throw new FileNotFoundException("Report file not found", path);
                }
                rprt.Load(path);

                rprt.SetDataSource(ge.ListDataTable);
                    
                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "application/pdf";
                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "KUASA_PEMBELIAN_BARANG");

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor bayar tidak sesuai!');", true);
            }

            rprt.Close();
            rprt.Dispose();

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }
    }

    protected void btnViewPernyataanDanKuasa_Click(object sender, EventArgs e)
    {

        try
        {
            ReportDocument rprt2 = new ReportDocument();
            DataTable rptData = new DataTable();

            if (HDNasabah.Value.Substring(0, 3) == SessBranchID && HDKantorBayar.Value.ToString() == "A001")
            {
                DefaultClass.Global.SqlHelper[] paramns = new DefaultClass.Global.SqlHelper[4];
                paramns[0] = gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 100, "PERNYATAAN_KUASA_PEMBELIAN_BARANG_HIKS_CIBITUNG", typeof(string));
                paramns[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNasabah.Value, typeof(string));
                paramns[2] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                paramns[3] = gb.CreateSPParameter("@keperluan", ParameterDirection.Input, SqlDbType.VarChar, 255, txtKeperluan.Text, typeof(string));

                rptData = gb.GetDataJustOutPut("KPKuasaMembeliBarang", paramns, "constrmf").Tables[0];

                var path = Server.MapPath("~/Kresun/Report/Report/KUASA/SuratPernyataanKuasaPembelianBarang.rpt");
                if (!System.IO.File.Exists(path))
                {
                    throw new FileNotFoundException("Report file not found", path);
                }
                rprt2.Load(path);

                rprt2.SetDataSource(rptData);

                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "application/pdf";
                rprt2.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "PERNYATAAN_KUASA_PEMBELIAN_BARANG_HIKS_CIBITUNG");

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor bayar tidak sesuai!');", true);
            }

            rprt2.Close();
            rprt2.Dispose();

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
        }
    }

    /* Second Approach
    protected void btnViewKuasa_Click(object sender, EventArgs e)
    {
        if (HDNasabah.Value.Substring(0, 3) == SessBranchID && HDKantorBayar.Value == "A002")
        {
            DefaultClass.Global.SqlHelper[] paramns = new DefaultClass.Global.SqlHelper[4];
            paramns[0] = gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 100, "KUASA_PEMBELIAN_BARANG_PREVIEW", typeof(string));
            paramns[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNasabah.Value, typeof(string));
            paramns[2] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
            paramns[3] = gb.CreateSPParameter("@principal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkabis.Text, typeof(string));

            var dt = gb.GetDataJustOutPut("KPKuasaMembeliBarang", paramns, "constrmf").Tables[0];

            ExportReportToPdf("~/Kresun/Report/Report/KUASA/KuasaMembeliBarang.rpt", dt, "KuasaMembeliBarang");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor bayar tidak sesuai!');", true);
        }
    }

    protected void btnViewPernyataanDanKuasa_Click(object sender, EventArgs e)
    {
        if (HDNasabah.Value.Substring(0, 3) == SessBranchID && HDKantorBayar.Value == "A001")
        {
            DefaultClass.Global.SqlHelper[] paramns = new DefaultClass.Global.SqlHelper[4];
            paramns[0] = gb.CreateSPParameter("@action", ParameterDirection.Input, SqlDbType.VarChar, 100, "PERNYATAAN_KUASA_PEMBELIAN_BARANG_HIKS_CIBITUNG", typeof(string));
            paramns[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, HDNasabah.Value, typeof(string));
            paramns[2] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
            paramns[3] = gb.CreateSPParameter("@keperluan", ParameterDirection.Input, SqlDbType.VarChar, 255, txtKeperluan.Text, typeof(string));

            var dt = gb.GetDataJustOutPut("KPKuasaMembeliBarang", paramns, "constrmf").Tables[0];

            ExportReportToPdf("~/Kresun/Report/Report/KUASA/SuratPernyataanKuasaPembelianBarang2.rpt", dt, "SuratPernyataanKuasa");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kantor bayar tidak sesuai!');", true);
        }
    }
    private void ExportReportToPdf(
    string reportPath,
    DataTable data,
    string fileName
)
    {
        try
        {
            using (ReportDocument rprt = new ReportDocument())
            {
                // Pastikan file report ada
                string rptPath = Server.MapPath(reportPath);
                if (!System.IO.File.Exists(rptPath))
                    throw new FileNotFoundException("Report file not found.", rptPath);

                // Load dan isi data
                rprt.Load(rptPath);
                rprt.SetDataSource(data);

                // Export ke Stream (tanpa cast)
                using (System.IO.Stream stream = rprt.ExportToStream(ExportFormatType.PortableDocFormat))
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    stream.CopyTo(ms); // Copy dari stream CR ke MemoryStream
                    byte[] bytes = ms.ToArray();

                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", $"inline;filename={fileName}.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript",
                $"javascript:SweetAlert('Error: {ex.Message}');", true);
        }
    }
    */
}