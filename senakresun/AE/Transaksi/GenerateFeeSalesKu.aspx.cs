using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AE_Transaksi_GenerategFeeSalesKu : System.Web.UI.Page
{
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
    private string SessKdpersh
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
        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetCabangReport", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
        ddlcabang.Items.Insert(0, new ListItem("All Cabang", "-"));
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
            Paramsc[0] = gb.CreateSPParameter("@kode_cabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string)); 
            Paramsc[1] = gb.CreateSPParameter("@periode_jwl", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            Paramsc[2] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@user_insert", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            Paramsc[5] = gb.CreateSPParameter("@tglclosing", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FEEGenerateKUSALES", Paramsc, "constrmf");
            string kesatu = ge.ListDataset.Tables.Count <= 3 ?  "Success" : ge.ListDataset.Tables[3].Rows[0][0].ToString() ;
            //string kedua = ge.ListDataset.Tables[6].Rows[0][0].ToString();
            if (kesatu == "Success")// || kedua == "Success")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Generate Fee Sales & KU Pada Bulan " + DateTime.Parse(txtStart.Text).ToString("MMMM") + " Tahun " + DateTime.Parse(txtStart.Text).ToString("yyyy") + " Berhasil');", true);

                txtStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ddlcabang.SelectedValue = "-";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[10].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }
            
        }
        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }
        }
    }
}