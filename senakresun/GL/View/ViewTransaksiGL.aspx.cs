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


public partial class GL_View_ViewTransaksiGL : System.Web.UI.Page
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
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[3];
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnZipCode.Value, typeof(String));
        Paramsarea[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_GL_Trn", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
   
    public void btnSerach_Click(object sender, EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] Paramsarea = new DefaultClass.Global.SqlHelper[3];
        Paramsarea[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        Paramsarea[1] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnZipCode.Value, typeof(String));
        Paramsarea[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_GL_Trn", Paramsarea, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();
        //GRData.HeaderRow.Disposed = 1;
        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 30, SessKdpersh, typeof(String));
        Params[1] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnZipCode.Value, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_View_GL_Trn_dtl", Params, "constrmf");



         if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            //Txtsaldoawal.Text = ge.ListDataset.Tables[0].Rows[0][1].ToString();
            //txtsaldobulanini.Text = ge.ListDataset.Tables[0].Rows[0][2].ToString();
            //txtsaldoakhir.Text = ge.ListDataset.Tables[0].Rows[0][3].ToString();
            
            Txtdatetrn.Text = ge.ListDataset.Tables[0].Rows[0][0].ToString();
            txttype.Text = ge.ListDataset.Tables[0].Rows[0][1].ToString();
            txtsumber.Text = ge.ListDataset.Tables[0].Rows[0][2].ToString();
            TxtNote.Text = ge.ListDataset.Tables[0].Rows[0][3].ToString();
            Txtuserentry.Text = ge.ListDataset.Tables[0].Rows[0][4].ToString();
            Txtdateentry.Text = ge.ListDataset.Tables[0].Rows[0][5].ToString();
        }
        else
        {
            Txtdatetrn.Text = String.Empty;
            txttype.Text = String.Empty;
            txtsumber.Text = String.Empty;
            TxtNote.Text = String.Empty;
            Txtuserentry.Text = String.Empty;
            Txtdateentry.Text = String.Empty;
        }
    }

    
    [WebMethod]
    public static string GetTempat(string InputText,string SessKdpershx, string SessBranchIDx, string SessPilihData)
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
        if (SessPilihData == "1")
        {
            ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_getTempat_GL_Trnsm", Params, "constrmf");
        }
       else
        {
            ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_getTempat_GL_Trn", Params, "constrmf");
        }

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

   




}