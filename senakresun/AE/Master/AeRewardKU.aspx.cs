using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class AE_Master_AeRewardKU : System.Web.UI.Page
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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            if (!Page.IsPostBack)
            {
                BindGridEntity();
                txtbatasbawah.Text = "0";
                txtbatasatas.Text = "0";
                txtpersentase.Text = "0";
            }
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKU", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeReward", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "batasbawahsm asc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;
                    txtbatasbawah.Enabled = false;
                    txtbatasatas.Enabled = false;
                    txtbatasbawah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbatasbawah")).Text;
                    txtbatasatas.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbatasatas")).Text;
                    txtpersentase.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpersentase")).Text;
                    btnSimpan.Text = "Update";
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
            case "SETDEL":
                try
                {
                    string atas = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbatasatas")).Text;
                    string bawah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbatasbawah")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                    var[0] = gb.CreateSPParameter("@batasawal", ParameterDirection.Input, SqlDbType.Int, 0, bawah, typeof(string));
                    var[1] = gb.CreateSPParameter("@batasakhir", ParameterDirection.Input, SqlDbType.Int, 0, atas, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEKU", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_FeeReward", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
        }
    }
    protected void batasbawah_OnChange(object sender, EventArgs e)
    {
        try
        {
            string ambil = txtbatasatas.Text;
            if(ambil != "0")
            {
                if (int.Parse(txtbatasbawah.Text) >= int.Parse(txtbatasatas.Text))
                {
                    txtbatasbawah.Text = "0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Batas Bawah tidak boleh Sama atau Melebihi Batas Atas!!');", true);
                }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void batasatas_OnChange(object sender, EventArgs e)
    {
        try
        {
            if (decimal.Parse(txtbatasatas.Text) <= decimal.Parse(txtbatasbawah.Text))
            {
                txtbatasatas.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Batas Atas tidak boleh Sama atau Lebih Kecil dari Batas bawah!!');", true);
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (decimal.Parse(txtbatasatas.Text) <= decimal.Parse(txtbatasbawah.Text))
            {
                txtbatasatas.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Batas Atas tidak boleh Sama atau Lebih Kecil dari Batas bawah!!');", true);
            }
            else if (decimal.Parse(txtbatasbawah.Text) >= decimal.Parse(txtbatasatas.Text))
            {
                txtbatasbawah.Text = "0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Batas Bawah tidak boleh Sama atau Melebihi Batas Atas!!');", true);
            }
            else
            {
                if (btnSimpan.Text == "Save")
                {
                    STATE = "INSERTKU";
                }
                else
                {
                    STATE = "UPDATEKU";
                }

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 100, SessLoginID, typeof(string));
                var[3] = gb.CreateSPParameter("@batasawal", ParameterDirection.Input, SqlDbType.Int, 0, txtbatasbawah.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@batasakhir", ParameterDirection.Input, SqlDbType.Int, 0, txtbatasatas.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@persentase", ParameterDirection.Input, SqlDbType.Money, 0, txtpersentase.Text, typeof(decimal));
                var[6] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 100, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_FeeReward", var, "constrmf"); 
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
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
}