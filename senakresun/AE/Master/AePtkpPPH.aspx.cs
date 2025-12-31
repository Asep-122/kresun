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


public partial class AE_Master_AePtkpPPH : System.Web.UI.Page
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
    Global gbsearch = new Global();
    GlobalEntities gesearch = new GlobalEntities();
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txt1.Enabled = true;
        txt1.Focus();
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                DateTime now = DateTime.Now; 
                BindGridEntity();
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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTYPEPTKPPPH", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_SELECT", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodePtkp asc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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

                    txt1.Enabled = false;
                    txt1.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl1")).Text;
                    txt2.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl2")).Text;
                    txt3.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl3")).Text;
                    txt4.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl4")).Text;
                    txt5.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl5")).Text;
                    txt6.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl6")).Text;
                    txt7.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl7")).Text;
                    txt8.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl8")).Text;
                    txt9.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl9")).Text;
                    txt10.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl10")).Text;
                    txt11.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl11")).Text;
                    txt12.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl12")).Text;
                    txt13.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl13")).Text;
                    txt14.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl14")).Text;
                    txt15.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl15")).Text;
                    txt16.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl16")).Text;
                    txt17.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl17")).Text;
                    txt18.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl18")).Text;
                    txt19.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl19")).Text;
                    txt20.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl20")).Text;
                    txt21.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl21")).Text;
                    txt22.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl22")).Text;
                    txt23.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl23")).Text;
                    txt24.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl24")).Text;
                    txt25.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl25")).Text;
                    txt26.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl26")).Text; 
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
                    txt1.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbl1")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@kdptkp", ParameterDirection.Input, SqlDbType.VarChar, 2, txt1.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETETYPEPTKPPPH", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_DELETE", var, "constrmf");
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
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = ""; 
        try
        {
            if (txt1.Enabled == true)
            {
                STATE = "INSERTTYPEPTKPPPH";
            }
            else
            {
                STATE = "UPDATETYPEPTKPPPH";
            }
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[28];
            var[0] = gb.CreateSPParameter("@KodePtkp", ParameterDirection.Input, SqlDbType.VarChar, 2, txt1.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@PtkpTK", ParameterDirection.Input, SqlDbType.Money,50, txt2.Text, typeof(decimal));
            var[2] = gb.CreateSPParameter("@PtkpK0", ParameterDirection.Input, SqlDbType.Money, 50, txt3.Text, typeof(decimal));
            var[3] = gb.CreateSPParameter("@PtkpK1", ParameterDirection.Input, SqlDbType.Money, 50, txt4.Text, typeof(decimal));
            var[4] = gb.CreateSPParameter("@PtkpK2", ParameterDirection.Input, SqlDbType.Money, 50, txt5.Text, typeof(decimal));
            var[5] = gb.CreateSPParameter("@PtkpK3", ParameterDirection.Input, SqlDbType.Money, 50, txt6.Text, typeof(decimal));
            var[6] = gb.CreateSPParameter("@BiayaJabatan", ParameterDirection.Input, SqlDbType.Money, 50, txt7.Text, typeof(decimal));
            var[7] = gb.CreateSPParameter("@JKKPrs", ParameterDirection.Input, SqlDbType.Decimal, 10, txt8.Text, typeof(decimal));
            var[8] = gb.CreateSPParameter("@THTPrs", ParameterDirection.Input, SqlDbType.Decimal, 10, txt9.Text, typeof(decimal));
            var[9] = gb.CreateSPParameter("@THTKry", ParameterDirection.Input, SqlDbType.Decimal, 10, txt10.Text, typeof(decimal));
            var[10] = gb.CreateSPParameter("@JKMPrs", ParameterDirection.Input, SqlDbType.Decimal, 10, txt11.Text, typeof(decimal));
            var[11] = gb.CreateSPParameter("@PkpBatas1Min", ParameterDirection.Input, SqlDbType.Money, 50, txt12.Text, typeof(decimal));
            var[12] = gb.CreateSPParameter("@PkpBatas1Max", ParameterDirection.Input, SqlDbType.Money, 50, txt13.Text, typeof(decimal));
            var[13] = gb.CreateSPParameter("@PkpBatas2Min", ParameterDirection.Input, SqlDbType.Money, 50, txt14.Text, typeof(decimal));
            var[14] = gb.CreateSPParameter("@PkpBatas2Max", ParameterDirection.Input, SqlDbType.Money, 50, txt15.Text, typeof(decimal));
            var[15] = gb.CreateSPParameter("@PkpBatas3Min", ParameterDirection.Input, SqlDbType.Money, 50, txt16.Text, typeof(decimal));
            var[16] = gb.CreateSPParameter("@PkpBatas3Max", ParameterDirection.Input, SqlDbType.Money, 50, txt17.Text, typeof(decimal));
            var[17] = gb.CreateSPParameter("@PkpBatas4Min", ParameterDirection.Input, SqlDbType.Money, 50, txt18.Text, typeof(decimal));
            var[18] = gb.CreateSPParameter("@PkpBatas4Max", ParameterDirection.Input, SqlDbType.Money, 50, txt19.Text, typeof(decimal));
            var[19] = gb.CreateSPParameter("@PkpBatas5Min", ParameterDirection.Input, SqlDbType.Money, 50, txt20.Text, typeof(decimal));
            var[20] = gb.CreateSPParameter("@PkpBatas5Max", ParameterDirection.Input, SqlDbType.Money, 50, txt21.Text, typeof(decimal));
            var[21] = gb.CreateSPParameter("@TarifBatas1", ParameterDirection.Input, SqlDbType.Decimal, 10, txt22.Text, typeof(decimal));
            var[22] = gb.CreateSPParameter("@TarifBatas2", ParameterDirection.Input, SqlDbType.Decimal, 10, txt23.Text, typeof(decimal));
            var[23] = gb.CreateSPParameter("@TarifBatas3", ParameterDirection.Input, SqlDbType.Decimal, 10, txt24.Text, typeof(decimal));
            var[24] = gb.CreateSPParameter("@TarifBatas4", ParameterDirection.Input, SqlDbType.Decimal, 10, txt25.Text, typeof(decimal));
            var[25] = gb.CreateSPParameter("@TarifBatas5", ParameterDirection.Input, SqlDbType.Decimal, 10, txt26.Text, typeof(decimal));
            var[26] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[27] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_INSERTUPDATE", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Duplicate"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Photo Copy " + txt1.Text + " already Exists!');", true);
            }
            else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
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

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
      
}