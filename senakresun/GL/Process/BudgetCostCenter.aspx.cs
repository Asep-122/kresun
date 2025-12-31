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
public partial class GL_Process_BudgetCostCenter : System.Web.UI.Page
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
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
                Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa02", Params, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlkodegl, "None", "");
                ddlkodegl.Items.Insert(0, new ListItem("-", "-"));


                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[2];
                Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                Paramsx[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_costcenter", Paramsx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcostcenter, "None", "");

                ddlcostcenter.Items.Insert(0, new ListItem("-", "-"));

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
        Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("GL_GRIDcostbudget", Params, "constrmf");

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
        ddlcostcenter.Enabled = true;
        Txttahun.Enabled = true;
        ddlcostcenter.Focus();
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
                    ddlcostcenter.Enabled = false;
                    Txttahun.Enabled = false;

                    ddlcostcenter.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeCC")).Text;
                    ddlkodegl.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGl")).Text;
                    ddldistribusi.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDBudgetTi")).Text;
                    Txttahun.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltahun")).Text;
                    Txttotal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi")).Text;
                    Txt01.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi01")).Text;
                    Txt02.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi02")).Text;
                    Txt03.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi03")).Text;
                    Txt04.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi04")).Text;
                    Txt05.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi05")).Text;
                    Txt06.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi06")).Text;
                    Txt07.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi07")).Text;
                    Txt08.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi08")).Text;
                    Txt09.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi09")).Text;
                    Txt10.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi10")).Text;
                    Txt11.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi11")).Text;
                    Txt12.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBudgetTi12")).Text;

                    //Double value;
                    //if (Double.TryParse(Txt01.Text, out value))                 
                    //Txt01.Text = String.Format({ 0:N2}, value);
                    //else
                    //    Txt01.Text = String.Empty;

                    //if (Double.TryParse(Txt02.Text, out value))
                    //    Txt02.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt02.Text = String.Empty;

                    //if (Double.TryParse(Txt03.Text, out value))
                    //    Txt03.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt03.Text = String.Empty;

                    //if (Double.TryParse(Txt04.Text, out value))
                    //    Txt04.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt04.Text = String.Empty;


                    //if (Double.TryParse(Txt05.Text, out value))
                    //    Txt05.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt05.Text = String.Empty;

                    //if (Double.TryParse(Txt06.Text, out value))
                    //    Txt06.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt06.Text = String.Empty;

                    //if (Double.TryParse(Txt07.Text, out value))
                    //    Txt07.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt07.Text = String.Empty;

                    //if (Double.TryParse(Txt08.Text, out value))
                    //    Txt08.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt08.Text = String.Empty;

                    //if (Double.TryParse(Txt09.Text, out value))
                    //    Txt09.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt09.Text = String.Empty;


                    //if (Double.TryParse(Txt10.Text, out value))
                    //    Txt10.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt10.Text = String.Empty;


                    //if (Double.TryParse(Txt11.Text, out value))
                    //    Txt11.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt11.Text = String.Empty;


                    //if (Double.TryParse(Txt12.Text, out value))
                    //    Txt12.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
                    //else
                    //    Txt12.Text = String.Empty;



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
                    ddlcostcenter.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeCC")).Text;
                    Txttahun.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltahun")).Text;
                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];
                    Params[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodegl.SelectedValue, typeof(string));
                    Params[1] = gb.CreateSPParameter("@Accountcode", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlcostcenter.SelectedValue, typeof(string));
                    Params[2] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, Txttahun.Text, typeof(string));
                    Params[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                    Params[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("GL_DELETEBudgetcostcenter", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("dataada"))
                    {
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:cancelalert('Budget Cost Center sudah ada transaksi!');", true);

                    }


                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
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


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[21];
            Params[0] = gb.CreateSPParameter("@KodeCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlcostcenter.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodegl.SelectedValue, typeof(string));
            Params[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[3] = gb.CreateSPParameter("@DBudgetTi", ParameterDirection.Input, SqlDbType.VarChar, 1, ddldistribusi.SelectedValue, typeof(string));
            Params[4] = gb.CreateSPParameter("@BudgetTi", ParameterDirection.Input, SqlDbType.Decimal, 0, Txttotal.Text, typeof(decimal));
            Params[5] = gb.CreateSPParameter("@BudgetTi01", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt01.Text, typeof(decimal));
            Params[6] = gb.CreateSPParameter("@BudgetTi02", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt02.Text, typeof(decimal));
            Params[7] = gb.CreateSPParameter("@BudgetTi03", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt03.Text, typeof(decimal));
            Params[8] = gb.CreateSPParameter("@BudgetTi04", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt04.Text, typeof(decimal));
            Params[9] = gb.CreateSPParameter("@BudgetTi05", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt05.Text, typeof(decimal));
            Params[10] = gb.CreateSPParameter("@BudgetTi06", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt06.Text, typeof(decimal));
            Params[11] = gb.CreateSPParameter("@BudgetTi07", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt07.Text, typeof(decimal));
            Params[12] = gb.CreateSPParameter("@BudgetTi08", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt08.Text, typeof(decimal));
            Params[13] = gb.CreateSPParameter("@BudgetTi09", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt09.Text, typeof(decimal));
            Params[14] = gb.CreateSPParameter("@BudgetTi10", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt10.Text, typeof(decimal));
            Params[15] = gb.CreateSPParameter("@BudgetTi11", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt11.Text, typeof(decimal));
            Params[16] = gb.CreateSPParameter("@BudgetTi12", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt12.Text, typeof(decimal));
            Params[17] = gb.CreateSPParameter("@USERUPD", ParameterDirection.Input, SqlDbType.VarChar, 15, SessLoginID, typeof(string));
            Params[18] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, Txttahun.Text, typeof(string));
            Params[19] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            Params[20] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
            if (ddlcostcenter.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_INSERTGlCCBudget", Params, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("GL_UPDATEGlCCBudget", Params, "constrmf");
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
    protected void ddldistribusi_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddldistribusi.SelectedValue == "F")
        {
            decimal xi;
            xi = decimal.Parse(Txttotal.Text) / 12;
            Txt01.Text = string.Format("{0:N2}", xi);
            Txt02.Text = string.Format("{0:N2}", xi);
            Txt03.Text = string.Format("{0:N2}", xi);
            Txt04.Text = string.Format("{0:N2}", xi);
            Txt05.Text = string.Format("{0:N2}", xi);
            Txt06.Text = string.Format("{0:N2}", xi);
            Txt07.Text = string.Format("{0:N2}", xi);
            Txt08.Text = string.Format("{0:N2}", xi);
            Txt09.Text = string.Format("{0:N2}", xi);
            Txt10.Text = string.Format("{0:N2}", xi);
            Txt11.Text = string.Format("{0:N2}", xi);
            Txt12.Text = string.Format("{0:N2}", xi);


        }

        else
        {
            Txt01.Text = "0";
            Txt02.Text = "0";
            Txt03.Text = "0";
            Txt04.Text = "0";
            Txt05.Text = "0";
            Txt06.Text = "0";
            Txt07.Text = "0";
            Txt08.Text = "0";
            Txt09.Text = "0";
            Txt10.Text = "0";
            Txt11.Text = "0";
            Txt12.Text = "0";



        }


    }



    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        decimal xi;
        xi = decimal.Parse(Txt01.Text) + decimal.Parse(Txt02.Text) + decimal.Parse(Txt03.Text) + decimal.Parse(Txt04.Text) + decimal.Parse(Txt05.Text) + decimal.Parse(Txt06.Text) + decimal.Parse(Txt07.Text) + decimal.Parse(Txt08.Text) + decimal.Parse(Txt09.Text) + decimal.Parse(Txt10.Text) + decimal.Parse(Txt11.Text) + decimal.Parse(Txt12.Text);
        Txttotal.Text = string.Format("{0:N2}", xi);
        // Txttotal.Text ="0";

    }

}