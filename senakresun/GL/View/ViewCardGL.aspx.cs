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



public partial class GL_View_ViewCardGL : System.Web.UI.Page
{
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
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
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@Kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, txtKodepos.Text, typeof(String));
        Paramsarea[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        Paramsarea[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        Paramsarea[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_CardGL", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[5];
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@Kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnZipCode.Value, typeof(String));
        Paramsarea[2] = gb.CreateSPParameter("@Tglawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
        Paramsarea[3] = gb.CreateSPParameter("@Tglakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
        Paramsarea[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_CardGL", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();
        //GRData.HeaderRow.Disposed = 1;
        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        Params[2] = gb.CreateSPParameter("@Kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnZipCode.Value, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_CardGL_Saldo", Params, "constrmf");



         if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            Txtsaldoawal.Text = ge.ListDataset.Tables[0].Rows[0][1].ToString();
            txtsaldobulanini.Text = ge.ListDataset.Tables[0].Rows[0][2].ToString();
            txtsaldoakhir.Text = ge.ListDataset.Tables[0].Rows[0][3].ToString();
        }

        else
        {
            Txtsaldoawal.Text = "0";
            txtsaldobulanini.Text = "0";
            txtsaldoakhir.Text ="0";
        }

        Double value;
        if (Double.TryParse(Txtsaldoawal.Text, out value))
            Txtsaldoawal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            Txtsaldoawal.Text = String.Empty;

        if (Double.TryParse(txtsaldobulanini.Text, out value))
            txtsaldobulanini.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtsaldobulanini.Text = String.Empty;

        if (Double.TryParse(txtsaldoakhir.Text, out value))
            txtsaldoakhir.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
        else
            txtsaldoakhir.Text = String.Empty;



    }


    [WebMethod]
    public static string GetTempat(string InputText,string SessKdpershx,string SessBranchIDx)
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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_getTempat", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }





}