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
public partial class GL_Process_BeginningBalance : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

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
                //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
                //Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

                //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa02", Params, "constrmf");

                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlkodegl, "None", "");
                //ddlkodegl.Items.Insert(0, new ListItem("-", "-"));

                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("GL_GRIDAccountBeginning", Params, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();
  



    }

    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;

        ddlkodegl.Enabled = true;
        Txttahun.Enabled = true;
        ddlkodegl.Focus();
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
    
                    ddlkodegl.Enabled = false;
                    txtnamaGl.Enabled = false;

                    ddlkodegl.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGl")).Text;
                    txtnamaGl.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamaGl")).Text;
                    
                    Txttahun.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltahun")).Text;
                    Txttotal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi")).Text;

                    
                    //Double value;
                    //if (Double.TryParse(Txttotal.Text, out value))
                    //    Txttotal.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txttotal.Text = String.Empty;

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
                    ddlkodegl.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGl")).Text;
                    Txttahun.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltahun")).Text;
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
                    Params[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodegl.Text, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, Txttahun.Text, typeof(string));

                    Params[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));

                    Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("GL_DELETEBudgetAccount", Params, "constrmf");



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

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            // string total;
            //Convert.ToString(Convert.ToDecimal(txtkumingguan.Text));

            //total = Convert.ToString(Convert.ToDecimal(Txttotal.Text));
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];
            Params[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodegl.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[2] = gb.CreateSPParameter("@BudgetTi", ParameterDirection.Input, SqlDbType.Decimal, 0, Txttotal.Text, typeof(decimal));
            Params[3] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, Txttahun.Text, typeof(string));
            Params[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            Params[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
            if (ddlkodegl.Enabled == true)
            {
               // ge.ListDataset = gb.GetDataJustOutPut("GL_INSERTGlAccountBudget", Params, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_UPDATEGlGlAccountBeginning", Params, "constrmf");
            }


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);


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


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    private static int ConvertToInt(string number)
    {
        number = number.Replace(",", "").Replace(".", "");
        return int.Parse(number);
    }

    private static decimal ConvertToDecimal(string number)
    {
        var ns = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        ns.NumberDecimalSeparator = ".";

        return decimal.Parse(number, ns);
    }
    

}