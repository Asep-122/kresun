using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;

public partial class Kresun_View_CetakPernyataanKesehatan : System.Web.UI.Page
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

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakPernyataanKesehatan.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CetakPernyataanKesehatan.aspx");
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTVIEWAPPROVALNEW", typeof(string));
        var[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_rekening desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HDRekening.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
        HDPlafon.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text.Replace(".", "");
        HDNasabah.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text.Replace(".", "");
        HDSumberdana.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsumberdana")).Text;
        HDInstansi.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblinstansi")).Text;

        pnladd.Visible = true;
        PnlFront.Visible = false;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {


            if (HDNasabah.Value.Substring(0, 3) == SessBranchID)
            {
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
                var1[0] = gb.CreateSPParameter("@no_rekening", ParameterDirection.Input, SqlDbType.VarChar, 20, HDRekening.Value, typeof(string));
                //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKSKPEMBIAYAANSENDIRI", typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CETAKSKPEMBIAYAANSENDIRINEW", typeof(string));

                ge.ListDataTable = gb.GetDataJustOutPut("KP_VIEWCETAKAN", var1, "constrmf").Tables[0];

                rprt.Load(Server.MapPath("~/Kresun/Report/SuratPernyataanKesehatan.rpt"));

                //string sumberDana = HDSumberdana.Value;

                //if (sumberDana == "Pembiayaan Sendiri")
                //{
                //    rprt.Load(Server.MapPath("~/Kresun/Report/SuratPernyataanPerbedaanIdentitas.rpt"));
                //}
                //else if (sumberDana == "BPRS HIK")
                //{
                //    rprt.Load(Server.MapPath("~/Kresun/Report/SuratPernyataanPerbedaanIdentitasHIK.rpt"));
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tidak Tersedia Untuk Sumber Dana Ini');", true);
                //}


                rprt.SetDataSource(ge.ListDataTable);


                rprt.SetParameterValue("terbilangplafon", gb.Terbilang(decimal.Parse(HDPlafon.Value)));
                rprt.SetParameterValue("terbilangangsuran", gb.Terbilang(decimal.Parse(ge.ListDataTable.Rows[0][12].ToString())));
                // rprt.SetParameterValue("jaminan", txtjaminan.Text);


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
        }
    }
}