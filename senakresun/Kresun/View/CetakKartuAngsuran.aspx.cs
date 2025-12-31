using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;

public partial class Kresun_View_CetakKartuAngsuran : System.Web.UI.Page
{

    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

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
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    public void BindGridEntity()
    {

        string[] splitdata = txtno_rekening.Text.Split('-');
        string no_rekening = splitdata[0];
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];

        var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKKARTUANGSURAN", typeof(string));
        //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "angs_ke asc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            string[] splitdata = txtno_rekening.Text.Split('-');
            string no_rekening = splitdata[0];

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
            var[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("KP_CETAK_KARTU_ANGSURAN", var, "constrmf").Tables[0];

            rprt.Load(Server.MapPath("~/Kresun/Report/KartuPinjaman/KP_KARTU_ANGSURAN.rpt"));

            rprt.SetDataSource(ge.ListDataTable);

            Response.ClearContent();
            Response.ClearHeaders();

            //Response.ContentType = "application/pdf";
            //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
            //Response.Flush();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            string fileName = "KP_KARTU_ANGSURAN_" + no_rekening;
            ReportHelper.ExportPdfInline(rprt, fileName, Response);
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }

        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string[] splitdata = txtno_rekening.Text.Trim().Split('-');
        string no_rekening = splitdata[0];

        if (txtno_rekening.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah belum di isi');", true);

        }
        else if (txtkodecabang.Text != txtno_rekening.Text.Substring(0, 3))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No. Nasabah yang di input tidak terdapat di cabang Anda');", true);

        }
        else
        {

            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
            var3[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 50, no_rekening, typeof(string));
            var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNASABAHFORINFOKARTU", typeof(string));
            //var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var3, "constrmf");

            txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["jangka"].ToString(); 
            txtAo.Text = ge.ListDataset.Tables[0].Rows[0]["MO"].ToString();
            txtplafon.Text = ge.ListDataset.Tables[0].Rows[0]["plafon"].ToString();
            txtNamaNasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtkelurahan.Text = ge.ListDataset.Tables[0].Rows[0]["kabupaten"].ToString();
            txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["alamat_rumah_tinggal1"].ToString();
            txtjenispinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["kode_produk"].ToString();
            txtnorek.Text = no_rekening.Trim();

            pnladd.Visible = true;
            pnlFilter.Visible = false;
            PnlFront.Visible = true;

            BindGridEntity();
        }
    }

    [WebMethod]
    public static string GetNoNasabah(string InputText, string SessionBranchID)
    {
        List<string[]> listResult = new List<string[]>();

        string no_nasabah = SessionBranchID + "" + InputText;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 30, InputText, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.Kp_GetNoRekeningForKartu", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtkodecabang.Text = SessBranchID;
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnNext);
    }
}