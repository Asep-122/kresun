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

public partial class AR_Master_MasterJournal : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
 
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

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }


    protected void btnBack_ServerClick(object sender, EventArgs e)
    {

        Response.Redirect("MasterJournal.aspx");

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.AbsoluteUri);

    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterJournal.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BindGridEntity();

                ddlstatus.Items.Add("PILIH DATA");
                ddlstatus.Items.Add("AKTIF");
                ddlstatus.Items.Add("TIDAK AKTIF");

                insertcombo();
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
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kodejournal asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "", createuser = "", createddate = "", updateuser = "", updatedate = "", kreditbiayaadmin = "",
               kreditmaterai = "", kredittitipan = "";
        try
        {
            if (txtkodejurnal.Enabled == true)
            {
                STATE = "INSERT";
                createuser = SessLoginID;
                createddate = DateTime.Now.ToString();
                updatedate = "";
                updateuser = "";

            }
            else
            {
                STATE = "UPDATE";
                createuser = "";
                createddate = "";
                updatedate = DateTime.Now.ToString();
                updateuser = SessLoginID;
            }
             

            string[] splitdata1 = ddlkreditbiayaadmin.Text.Split('-');
            kreditbiayaadmin = splitdata1[0]; 

            string[] splitdata2 = ddlkreditmaterai.Text.Split('-');
            kreditmaterai = splitdata2[0];

            string[] splitdata3 = ddlkredittitipan.Text.Split('-');
            kredittitipan = splitdata3[0];

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[16];
            var[0] = gb.CreateSPParameter("@kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodejurnal.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@namajournal", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnamajournal.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@type", ParameterDirection.Input, SqlDbType.VarChar, 50, ddltype.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@kredit1", ParameterDirection.Input, SqlDbType.VarChar, 50, kreditbiayaadmin, typeof(string));
            var[5] = gb.CreateSPParameter("@kredit2", ParameterDirection.Input, SqlDbType.VarChar, 50, kreditmaterai, typeof(string));
            var[6] = gb.CreateSPParameter("@kredit3", ParameterDirection.Input, SqlDbType.VarChar, 50, kredittitipan, typeof(string)); 
            var[7] = gb.CreateSPParameter("@kredit4", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkredit4.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@kredit5", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkredit5.SelectedValue, typeof(string));
            var[9] = gb.CreateSPParameter("@debit1", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldebit1.SelectedValue, typeof(string));
            var[10] = gb.CreateSPParameter("@debit2", ParameterDirection.Input, SqlDbType.VarChar, 50, ddldebit2.SelectedValue, typeof(string));
            var[11] = gb.CreateSPParameter("@usercreate", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[12] = gb.CreateSPParameter("@tglcreate", ParameterDirection.Input, SqlDbType.VarChar, 50, updatedate, typeof(string));
            var[13] = gb.CreateSPParameter("@statusaktif", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedIndex, typeof(string));
            var[14] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[15] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", var, "constrmf");



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

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {
                    string status = "";

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;

                    txtkodejurnal.Enabled = false; 
                    status = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    txtkodejurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdjournal")).Text;
                    txtnamajournal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmjournal")).Text;
                    ddltype.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltype")).Text;
                    ddlkreditbiayaadmin.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkreditbiayaadmin")).Text;
                    ddlkreditmaterai.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkreditmaterai")).Text;
                    ddlkredittitipan.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkredittitipan")).Text;
                    ddlkredit4.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkredit4")).Text;
                    ddlkredit5.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkredit5")).Text;
                    ddldebit1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldebit1")).Text;
                    ddldebit2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldebit2")).Text;


                    if (status == "AKTIF")
                    {
                        ddlstatus.SelectedValue = "AKTIF";
                    }
                    else
                    {
                        ddlstatus.SelectedValue = "TIDAK AKTIF";

                    }

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


                    txtkodejurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdjournal")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@kodejournal", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodejurnal.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", var1, "constrmf");

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
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedValue == "A1")
        {
            ddlkredit5.Visible = false;
            lblkredit5.Visible = false;
            ddldebit2.Visible = false;
            lbldebit2.Visible = false;

            ddlkreditbiayaadmin.Visible = true;
            lblkredit1.Visible = true;
            ddlkreditmaterai.Visible = true;
            lblkredit2.Visible = true;
            ddlkredittitipan.Visible = true;
            lblkredit3.Visible = true;
            ddlkredit4.Visible = true;
            lblkredit4.Visible = true;
        }
        else if (ddltype.SelectedValue == "A2")
        {
            ddlkredit5.Visible = false;
            lblkredit5.Visible = false;

            ddldebit1.Visible = true;
            lbldebit1.Visible = true;
            ddldebit2.Visible = true;
            lbldebit2.Visible = true;
            ddlkreditbiayaadmin.Visible = true;
            lblkredit1.Visible = true;
            ddlkreditmaterai.Visible = true;
            lblkredit2.Visible = true;
            ddlkredittitipan.Visible = true;
            lblkredit3.Visible = true;
            ddlkredit4.Visible = true;
            lblkredit4.Visible = true;

        }
        else if (ddltype.SelectedValue == "A3")
        {
            ddlkredit5.Visible = false;
            lblkredit5.Visible = false;
            ddldebit2.Visible = false;
            lbldebit2.Visible = false;
            ddlkreditmaterai.Visible = false;
            lblkredit2.Visible = false;
            ddlkredittitipan.Visible = false;
            lblkredit3.Visible = false;
            ddlkredit4.Visible = false;
            lblkredit4.Visible = false;


            ddlkreditbiayaadmin.Visible = true;
            lblkredit1.Visible = true;
            ddldebit1.Visible = true;
            lbldebit1.Visible = true;

        }
        else if (ddltype.SelectedValue == "A4")
        {
            ddlkredit5.Visible = false;
            lblkredit5.Visible = false;
            ddldebit2.Visible = false;
            lbldebit2.Visible = false;
            ddlkredittitipan.Visible = false;
            lblkredit3.Visible = false;
            ddlkredit4.Visible = false;
            lblkredit4.Visible = false;


            ddlkreditbiayaadmin.Visible = true;
            lblkredit1.Visible = true;
            ddldebit1.Visible = true;
            lbldebit1.Visible = true;
            ddlkreditmaterai.Visible = true;
            lblkredit2.Visible = true;

        }

    }
    public void insertcombo()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEJURNAL", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";
          
        ddlkreditbiayaadmin.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkreditbiayaadmin.DataTextField = "kdjurnal";
        ddlkreditbiayaadmin.DataValueField = "KodeGl";
        ddlkreditbiayaadmin.DataBind();
        ddlkreditbiayaadmin.Items.Insert(0, new ListItem("Pilih Data", "-"));


        ddlkreditmaterai.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkreditmaterai.DataTextField = "kdjurnal";
        ddlkreditmaterai.DataValueField = "KodeGl";
        ddlkreditmaterai.DataBind();
        ddlkreditmaterai.Items.Insert(0, new ListItem("Pilih Data", "-"));


        ddlkredittitipan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkredittitipan.DataTextField = "kdjurnal";
        ddlkredittitipan.DataValueField = "KodeGl";
        ddlkredittitipan.DataBind();
        ddlkredittitipan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ddlkredit4.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkredit4.DataTextField = "kdjurnal";
        ddlkredit4.DataValueField = "KodeGl";
        ddlkredit4.DataBind();
        ddlkredit4.Items.Insert(0, new ListItem("Pilih Data", "-"));


        ddlkredit5.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkredit5.DataTextField = "kdjurnal";
        ddlkredit5.DataValueField = "KodeGl";
        ddlkredit5.DataBind();
        ddlkredit5.Items.Insert(0, new ListItem("Pilih Data", "-"));

        ddldebit1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddldebit1.DataTextField = "kdjurnal";
        ddldebit1.DataValueField = "KodeGl";
        ddldebit1.DataBind();
        ddldebit1.Items.Insert(0, new ListItem("Pilih Data", "-"));


        ddldebit2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddldebit2.DataTextField = "kdjurnal";
        ddldebit2.DataValueField = "KodeGl";
        ddldebit2.DataBind();
        ddldebit2.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTYPE", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_MASTER_JOURNAL", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "typetrn asc";

        ddltype.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltype.DataTextField = "typejrnl";
        ddltype.DataValueField = "typetrn";
        ddltype.DataBind();
        ddltype.Items.Insert(0, new ListItem("Pilih Data", "-"));
    }
}