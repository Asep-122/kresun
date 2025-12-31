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
public partial class SN_Process_Modal_UnlockCetakSurat : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A2";
    string kodesumber = "CL";
    string KodeKonter = "CL02";
    public string nobuktidb;


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                //BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttglentry.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text; 
                Pnlfooter1.Visible = true;
                btnCancel.Visible = false;



                ////DROPDOWNLIST Kode Supplier
                //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4]; 
                //var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKANTORBAYAR", typeof(string));
                //var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_As_View", var1, "constrmf");

                //ge.ListDataset.Tables[0].DefaultView.Sort = "KantorBayar asc";

                //ddlkantorbayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlkantorbayar.DataTextField = "KantorBayar";
                //ddlkantorbayar.DataValueField = "KantorBayar";
                //ddlkantorbayar.DataBind();
                //ddlkantorbayar.Items.Insert(0, new ListItem("Pilih Data", "-"));
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }





    public void btnSerach_Click(object sender, EventArgs e)
    {
        var kodelgn = hdncustomer.Value;
        string norekening = HDNoRekening.Value;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@nobillyed", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SM_UNLOCK_SURAT", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {


            
            txtkodenasabah.Text = ge.ListDataset.Tables[0].Rows[0]["no_nasabah"].ToString();
            txtNamaNasabah.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
            txtnorekening.Text = ge.ListDataset.Tables[0].Rows[0]["Idpermohonan"].ToString();
            txtnobilyed.Text = ge.ListDataset.Tables[0].Rows[0]["nobilyed"].ToString();
            txtcatatanlama.Text = ge.ListDataset.Tables[0].Rows[0]["catatanunlock"].ToString();
            txtupdate.Text = ge.ListDataset.Tables[0].Rows[0]["userunlock"].ToString();
            if ((ge.ListDataset.Tables[0].Rows[0]["tglunlock"].ToString()).Length != 0)
            {
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglunlock"].ToString());
                txttglentry.Text = xtanggal.ToString("yyyy-MM-dd");
            }
           
        }

    }


    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }


    [WebMethod]
    public static string GetBukti(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.SM_getCustomer_Unlock_Surat", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[9];

        Params[0] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnorekening.Text, typeof(string));
        Params[1] = gb.CreateSPParameter("@nobillyed", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobilyed.Text, typeof(string));
        Params[2] = gb.CreateSPParameter("@nonasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodenasabah.Text, typeof(string));
        Params[3] = gb.CreateSPParameter("@catatanunlock", ParameterDirection.Input, SqlDbType.VarChar, 250, txtKeterangan.Text, typeof(string));
        Params[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATE", typeof(string));
        Params[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        Params[6] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        Params[7] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Params[8] = gb.CreateSPParameter("@addbyfk", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SM_UNLOCK_SURAT", Params, "constrmf");
        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success")) 
        {
            btnSimpan.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);

            txtKeterangan.Text = "";

            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }


    }
}