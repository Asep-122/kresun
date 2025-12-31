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

public partial class AR_View_CetakKartuAngsuran : System.Web.UI.Page
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
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    public void BindGridEntity()
    {

        string[] splitdata = txtnik.Text.Split('-');
        string no_rekening = splitdata[0];
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        string nik = hdnZipCode.Value;
        var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnZipCodex.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTU", typeof(string));
        var[3] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
        //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "angske asc";


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

            //ReportDocument rprt = new ReportDocument();
            string[] splitdata = txtnik.Text.Split('-');
            string no_rekening = splitdata[0];
            string nik = hdnZipCodex.Value;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];

            var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, nik, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARTU", typeof(string));
            var[3] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf").Tables[0];

            rprt.Load(Server.MapPath("~/AE/View/Report/rptCetakKartuAngsuran.rpt"));

            rprt.SetDataSource(ge.ListDataTable);
            string jam = string.Format(DateTime.Now.ToString("HH:mm:ss", new CultureInfo("id-ID")));
            rprt.SetParameterValue("tanggal_cetak", DateTime.Now.ToString("dd-MM-yyyy"));

            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


            Response.End();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('test3!');", true);
        }
        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }
            else
            {

                rprt.Close();
                rprt.Dispose();
            }

        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string[] splitdata = txtnik.Text.Trim().Split('-');
        string nik = splitdata[0];


        if (txtnik.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nik belum di isi');", true);
        }
        else
        {
            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
            var3[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnZipCodex.Value, typeof(string));
            var3[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNASABAHFORINFOKARTU", typeof(string));
            var3[2] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnZipCode.Value, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_CETAKAN", var3, "constrmf");
            if(ge.ListDataset.Tables[0].Rows.Count == 0 )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nik ini belum melakukan Pinjaman');", true);
            }
            else
            {

                txtnorek.Text = hdnZipCodex.Value;
                txtjangka.Text = ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString(); 
                txtbagian.Text = ge.ListDataset.Tables[0].Rows[0]["KetBagian"].ToString();
                txtdebit.Text = ge.ListDataset.Tables[0].Rows[0]["JlhDebit"].ToString();
                txtNamaKaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                txtketerangan.Text = ge.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();
                txtbataspinjaman.Text = ge.ListDataset.Tables[0].Rows[0]["BatasPinjaman"].ToString();
                txtnobukti.Text = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                //txtnorek.Text = nik.Trim() ; 
                txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["BatasPinjaman"].ToString()));
                txtdebit.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhAngsuran"].ToString()));

                pnladd.Visible = true;
                pnlFilter.Visible = false;
                PnlFront.Visible = true;

                BindGridEntity();
            }
        }
    }


    [WebMethod]
    public static string GetNoKaryawan(string InputText, string SessionBranchID, string SessKdpersh)
    {
        List<string[]> listResult = new List<string[]>();

        string nik = InputText ;

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 50, nik, typeof(String));
        Params[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_getCustomer_angsuran", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);
        //if(ge.ListDataset.Tables[0].Rows.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nik ini belum melakukan Pinjaman');", true);
        //}
        //else
        //{
        //    return json;
        //}
        return json;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtkodecabang.Text = SessBranchID;
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnNext);
    }
}