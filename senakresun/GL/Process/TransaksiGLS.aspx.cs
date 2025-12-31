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

public partial class GL_Process_TransaksiGLS : System.Web.UI.Page
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
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }


    Reports rpt = new Reports();


    DataTable dtRP = new DataTable("RencanaPerjalanan");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
                Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

                //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa02", Params, "constrmf");

                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlkodegl, "None", "");
                //ddlkodegl.Items.Insert(0, new ListItem("-", "-"));


                //DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
                //Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                //ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_costcenter", Paramsx, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcostcenter, "None", "");
                
                //ddlcostcenter.Items.Insert(0, new ListItem("-", "-"));
               
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;


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

    [WebMethod]
    public static string GetTempat(string InputText, string SessKdpershx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_getTempat", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];

        Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("GL_GRIDTransaksiView", Params, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


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
        SetDetailRP();

    }
    private void SetDetailRP()
    {

        DataRow dr = null;

        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));

        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


        dr = dtRP.NewRow();
        dr["nomor"] = 1;
        dr["KodeGL"] = string.Empty;
        dr["NamaGL"] = string.Empty;
        dr["Description"] = string.Empty;
        dr["Costcenter"] = string.Empty;
        dr["jlhdebit"] = decimal.Zero;
        dr["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtRP;


        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();




    }


    private void cssGridRP()
    {

        // Attribute to show the Plus Minus Button.
        GRRP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRRP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");

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
    
                  

                    //Double value;
                    //if (Double.TryParse(Txt01.Text, out value))
                    //    Txt01.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:N2}", value);
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
                    //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
                    //Params[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodegl.SelectedValue, typeof(string));
                    //Params[1] = gb.CreateSPParameter("@Accountcode", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlcostcenter.SelectedValue, typeof(string));
                    //Params[2] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, Txttahun.Text, typeof(string));

                    //Params[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    //Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //ge.ListDataset = gb.GetDataJustOutPut("[GL_DELETEBudgetcostcenter", Params, "constrmf");



                    //if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    //{

                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);



                    //}

                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    //    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    //}

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


            //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[20];
            //Params[0] = gb.CreateSPParameter("@KodeCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlcostcenter.SelectedValue, typeof(string));
            //Params[1] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodegl.SelectedValue, typeof(string));
            //Params[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            //Params[3] = gb.CreateSPParameter("@DBudgetTi", ParameterDirection.Input, SqlDbType.VarChar, 1, ddldistribusi.SelectedValue, typeof(string));
            //Params[4] = gb.CreateSPParameter("@BudgetTi", ParameterDirection.Input, SqlDbType.Decimal, 0, Txttotal.Text, typeof(decimal));
            //Params[5] = gb.CreateSPParameter("@BudgetTi01", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt01.Text, typeof(decimal));
            //Params[6] = gb.CreateSPParameter("@BudgetTi02", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt02.Text, typeof(decimal));
            //Params[7] = gb.CreateSPParameter("@BudgetTi03", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt03.Text, typeof(decimal));
            //Params[8] = gb.CreateSPParameter("@BudgetTi04", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt04.Text, typeof(decimal));
            //Params[9] = gb.CreateSPParameter("@BudgetTi05", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt05.Text, typeof(decimal));
            //Params[10] = gb.CreateSPParameter("@BudgetTi06", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt06.Text, typeof(decimal));
            //Params[11] = gb.CreateSPParameter("@BudgetTi07", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt07.Text, typeof(decimal));
            //Params[12] = gb.CreateSPParameter("@BudgetTi08", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt08.Text, typeof(decimal));
            //Params[13] = gb.CreateSPParameter("@BudgetTi09", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt09.Text, typeof(decimal));
            //Params[14] = gb.CreateSPParameter("@BudgetTi10", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt10.Text, typeof(decimal));
            //Params[15] = gb.CreateSPParameter("@BudgetTi11", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt11.Text, typeof(decimal));
            //Params[16] = gb.CreateSPParameter("@BudgetTi12", ParameterDirection.Input, SqlDbType.Decimal, 0, Txt12.Text, typeof(decimal));
            //Params[17] = gb.CreateSPParameter("@USERUPD", ParameterDirection.Input, SqlDbType.VarChar, 15, SessLoginID, typeof(string));
            //Params[18] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, Txttahun.Text, typeof(string));
            //Params[19] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
            //if (ddlcostcenter.Enabled == true)
            //{
            //    ge.ListDataset = gb.GetDataJustOutPut("GL_INSERTGlCCBudget", Params, "constrmf");
            //}
            //else
            //{
            //    ge.ListDataset = gb.GetDataJustOutPut("GL_UPDATEGlCCBudget", Params, "constrmf");
            //}


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
    //protected void ddldistribusi_SelectedIndexChanged(object sender, EventArgs e)
    //{


    //    if (ddldistribusi.SelectedValue == "F")
    //    {
    //        decimal xi;
    //        xi = decimal.Parse(Txttotal.Text) / 12;
    //        Txt01.Text = string.Format("{0:###,###,###}", xi);
    //        Txt02.Text = string.Format("{0:###,###,###}", xi);
    //        Txt03.Text = string.Format("{0:###,###,###}", xi);
    //        Txt04.Text = string.Format("{0:###,###,###}", xi);
    //        Txt05.Text = string.Format("{0:###,###,###}", xi);
    //        Txt06.Text = string.Format("{0:###,###,###}", xi);
    //        Txt07.Text = string.Format("{0:###,###,###}", xi);
    //        Txt08.Text = string.Format("{0:###,###,###}", xi);
    //        Txt09.Text = string.Format("{0:###,###,###}", xi);
    //        Txt10.Text = string.Format("{0:###,###,###}", xi);
    //        Txt11.Text = string.Format("{0:###,###,###}", xi);
    //        Txt12.Text = string.Format("{0:###,###,###}", xi);


    //    }

    //    else
    //    {
    //        Txt01.Text = "0";
    //        Txt02.Text = "0";
    //        Txt03.Text = "0";
    //        Txt04.Text = "0";
    //        Txt05.Text = "0";
    //        Txt06.Text = "0";
    //        Txt07.Text = "0";
    //        Txt08.Text = "0";
    //        Txt09.Text = "0";
    //        Txt10.Text = "0";
    //        Txt11.Text = "0";
    //        Txt12.Text = "0";



    //    }


    //}



    //protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    //{
    //    decimal xi;
    //    xi = decimal.Parse(Txt01.Text) + decimal.Parse(Txt02.Text) + decimal.Parse(Txt03.Text) + decimal.Parse(Txt04.Text) + decimal.Parse(Txt05.Text) + decimal.Parse(Txt06.Text) + decimal.Parse(Txt07.Text) + decimal.Parse(Txt08.Text) + decimal.Parse(Txt09.Text) + decimal.Parse(Txt10.Text) + decimal.Parse(Txt11.Text) + decimal.Parse(Txt12.Text);
    //    Txttotal.Text = string.Format("{0:###,###,###}", xi);
    //    // Txttotal.Text ="0";

    //}

    protected void lnkAddRP_Click(object sender, EventArgs e)
    {
        int dgcount = GRRP.Rows.Count;

        dtRP.Columns.Clear();

        dtRP = (DataTable)ViewState["ListDetailRP"];

        dtRP.Rows.Clear();

        for (int i = 0; i < dgcount; i++)
        {
            DataRow dr = dtRP.NewRow();

            dr["nomor"] = 1;
            dr["KodeGL"] = string.Empty;
            dr["NamaGL"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Costcenter"] = string.Empty;
            dr["jlhdebit"] = decimal.Zero;
            dr["jlhkredit"] = decimal.Zero;


            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
            dr["KodeGL"] = ((TextBox)GRRP.Rows[i].FindControl("txtKodeGL")).Text;
            dr["NamaGL"] = ((TextBox)GRRP.Rows[i].FindControl("hdnddlnamaGL")).Text;// ((DropDownList)GRRP.Rows[i].FindControl("hdnddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("hdnddlnamaGL")).SelectedValue : (object)DBNull.Value;
            dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
            dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("hdnddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("hdnddlnamaGL")).SelectedValue : (object)DBNull.Value;
            dr["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
            dr["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;


            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();

        drdnew["nomor"] = 1;
        drdnew["KodeGL"] = string.Empty;
        drdnew["NamaGL"] = string.Empty;
        drdnew["Description"] = string.Empty;
        drdnew["Costcenter"] = string.Empty;
        drdnew["jlhdebit"] = decimal.Zero;
        drdnew["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();


    }


    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                int dgcount = GRRP.Rows.Count;


                ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                        drCurrentRow["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                        drCurrentRow["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;


                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = string.Empty;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }


                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();


                break;
        }
    }

    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[0];

            //ge.ListDataset = gb.GetDataJustOutPut("dbo.HCIS_SP_JustBranch", Params, "constrhrms");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "[Text] asc";

            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlBranch")), "none", "");
            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlBranchtujuan")), "none", "");

            //((DropDownList)e.Row.FindControl("ddlBranch")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnBranch")).Value;
            //((DropDownList)e.Row.FindControl("ddlBranchtujuan")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlBranchtujuan")).Value;

            //((DropDownList)e.Row.FindControl("ddlSaranaB")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlddlSaranaB")).Value;

            //((TextBox)e.Row.FindControl("txtwaktuB")).Attributes.Add("readonly", "true");


            //if (((DropDownList)e.Row.FindControl("ddlSaranaB")).SelectedValue == "3")
            //{
            //    ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Remove("readonly");
            //    ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Add("required", "required");
            //    ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Remove("readonly");
            //    ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Add("required", "required");
            //}

            //else
            //{
            //    ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Add("readonly", "true");
            //    ((TextBox)e.Row.FindControl("txtBandaraB")).Attributes.Remove("required");
            //    ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Add("readonly", "true");
            //    ((TextBox)e.Row.FindControl("txtBandaraT")).Attributes.Remove("required");
            //}

            //if (SessBranchID != "WPI" & (((HiddenField)e.Row.FindControl("hdnBranch")).Value != "" || ((HiddenField)e.Row.FindControl("hdnddlBranchtujuan")).Value != ""))
            //{
            //    ((DropDownList)e.Row.FindControl("ddlBranch")).SelectedValue = SessBranchID;
            //    ((DropDownList)e.Row.FindControl("ddlBranch")).Enabled = false;

            //    ((DropDownList)e.Row.FindControl("ddlBranchtujuan")).SelectedValue = SessBranchID;
            //    ((DropDownList)e.Row.FindControl("ddlBranchtujuan")).Enabled = false;
            //}

        }
    }

}