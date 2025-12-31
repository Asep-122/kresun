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


public partial class CL_View_ViewHutangSupplier : System.Web.UI.Page
{
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
    //private string SessLoginID
    //{
    //    get { return Session["SessLoginID"].ToString(); }
    //    set { ViewState["SessLoginID"] = value; }
    //
    //}
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
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            { 

                DateTime now = DateTime.Now;
                txtStart.Text = now.ToString("yyyy-MM-dd");
                txtEnd.Text = now.ToString("yyyy-MM-dd");
                BindGridEntity();

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, "-", typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";
                //ddlkdbank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlkdbank.DataTextField = "kdnmbank";
                //ddlkdbank.DataValueField = "KodeBank";
                //ddlkdbank.DataBind();
                //ddlkdbank.Items.Insert(0, new ListItem("Pilih Data", "-"));
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
        //DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        //Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        //Paramsarea[1] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdbank.SelectedValue, typeof(String));
        //Paramsarea[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //Paramsarea[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //Paramsarea[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWCARDCASHBANK", typeof(string));
        //ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", Paramsarea, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        //GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //GRData.DataBind();


        //GRData.UseAccessibleHeader = true;

        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {

        //if (ddlView.SelectedValue == "1")
        //{

        //    DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        //    Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        //    Paramsarea[1] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkdbank.SelectedValue, typeof(String));
        //    Paramsarea[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        //    Paramsarea[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        //    Paramsarea[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "VIEWCARDCASHBANK", typeof(string));
        //    ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", Paramsarea, "constrmf");

        //    ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        //    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        //    GRData.DataBind();
        //    //GRData.HeaderRow.Disposed = 1;
        //    GRData.UseAccessibleHeader = true;

        //    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        //}
        //else if (ddlView.SelectedValue == "2")
        //{
        //}
        //else if(ddlView.SelectedValue == "3")
        //{

        //}
    }


    [WebMethod]
    public static string GetTempat(string InputText, string SessKdpershx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_getTempat", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

    protected void ddlNamaBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////var bank = ddlkdbank.SelectedValue;
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        //var[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        //var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNAMABANK", typeof(string));
        //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        //ge.ListDataset = gb.GetDataJustOutPut("CL_ViewkasBank", var, "constrmf");

        //string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
        //string Alamat1 = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
        //string Alamat2 = ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString();
        //string Alamat3 = ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString();
        //string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
        //string NOREK = ge.ListDataset.Tables[0].Rows[0]["NoRekening"].ToString();
        //string btskrdt = ge.ListDataset.Tables[0].Rows[0]["BatasKredit"].ToString();

        //string SaldoBerjalan = ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString();
        //string saldoGiro = ge.ListDataset.Tables[0].Rows[0]["SaldoAwalGr"].ToString();
        ////txtnmBank.Text = NamaBank;
        ////txtalamat1.Text = Alamat1;
        ////txtalamat2.Text = Alamat2;
        ////txtalamat3.Text = Alamat3;
        ////txtnorek.Text = NOREK;
        ////txtbtskrdit.Text = btskrdt;
        ////txtsaldogiro.Text = saldoGiro;
        ////txtsaldojln.Text = SaldoBerjalan;
        ////txtkurs1.Text = kdcrc;
    }

    protected void ddlView_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlView.SelectedValue == "1")
        //{
        //    panel2.Visible = false;

        //}
        //else if (ddlView.SelectedValue == "2")
        //{
        //    panel2.Visible = true;
        //}
        //else
        //{
        //    panel2.Visible = false;
        //}
    }
}