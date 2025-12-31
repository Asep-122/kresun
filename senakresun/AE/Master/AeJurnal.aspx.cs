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
using System.Globalization;

public partial class AE_Master_AeJurnal : System.Web.UI.Page
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

    Global gbsearch = new Global();
    GlobalEntities gesearch = new GlobalEntities();
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtKodeJurnal.Enabled = true;
        txtKodeJurnal.Focus();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
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
        //GET ALL VALUE
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJURNAL", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[INV_SELECT]", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kodejurnal asc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
        var1[0] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGL", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("INV_SELECT", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";


        ddlDebitGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlDebitGL.DataTextField = "kdnmGL";
        ddlDebitGL.DataValueField = "KodeGl";
        ddlDebitGL.DataBind();
        ddlDebitGL.Items.Insert(0, new ListItem("Pilih Data", "-"));


        ddlKreditGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKreditGL.DataTextField = "kdnmGL";
        ddlKreditGL.DataValueField = "KodeGl";
        ddlKreditGL.DataBind();
        ddlKreditGL.Items.Insert(0, new ListItem("Pilih Data", "-"));

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
        var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCC", typeof(string));
        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[3] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("INV_SELECT", var2, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCc asc";


        ddlDebitCC.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlDebitCC.DataTextField = "Nama";
        ddlDebitCC.DataValueField = "KodeCc";
        ddlDebitCC.DataBind();
        ddlDebitCC.Items.Insert(0, new ListItem("Pilih Data", "-"));


        ddlKreditCC.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlKreditCC.DataTextField = "Nama";
        ddlKreditCC.DataValueField = "KodeCc";
        ddlKreditCC.DataBind();
        ddlKreditCC.Items.Insert(0, new ListItem("Pilih Data", "-"));
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

                    txtKodeJurnal.Enabled = false;
                    txtKodeJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdJurnal")).Text;
                    txtKetJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKetJurnal")).Text;
                    ddlKreditGL.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL")).Text;
                    ddlKreditCC.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC")).Text;
                    ddlDebitGL.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL")).Text;
                    ddlDebitCC.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlDebitGL.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("INV_SELECT", var, "constrmf").Tables[0];

                    var CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    if (CCFLAGDebit == "True")
                    {
                        ddlDebitCC.Enabled = true;
                    }
                    else
                    {
                        ddlDebitCC.Enabled = false;
                        ddlDebitCC.SelectedValue = "-";
                    }

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlKreditGL.SelectedValue, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("INV_SELECT", var1, "constrmf").Tables[0];

                    var CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    if (CCFLAGKredit == "True")
                    {
                        ddlKreditCC.Enabled = true;
                    }
                    else
                    {
                        ddlKreditCC.Enabled = false;
                        ddlKreditCC.SelectedValue = "-";
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
                    txtKodeJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdJurnal")).Text;

                    //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                    //var1[0] = gb.CreateSPParameter("@KdGdg", ParameterDirection.Input, SqlDbType.VarChar, 3, txtkdcctr.Text, typeof(string)); 
                    //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCOSTCENTERINMST", typeof(string));
                    //var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //var1[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    //ge.ListDataset = gb.GetDataJustOutPut("[INV_SELECT]", var1, "constrmf");

                    //GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    //if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Cost Center sudah ada pada transaksi tidak dapat dihapus!');", true);

                    //}
                    //else
                    //{

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, txtKodeJurnal.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEJURNAL", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("[INV_CUD]", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                    //    }
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
            if (txtKodeJurnal.Enabled == true)
            {
                STATE = "INSERTJURNAL";
            }
            else
            {
                STATE = "UPDATEJURNAL";
            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gbsearch.CreateSPParameter("@KdJurnal", ParameterDirection.Input, SqlDbType.VarChar, 6, txtKodeJurnal.Text, typeof(string));
            var[1] = gbsearch.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
            var[2] = gbsearch.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 40, SessBranchID, typeof(string));
            var[3] = gbsearch.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJURNALBYID", typeof(string));
            var[4] = gbsearch.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[11];
            var1[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 6, txtKodeJurnal.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@KetJurnal", ParameterDirection.Input, SqlDbType.VarChar, 40, txtKetJurnal.Text, typeof(string));
            var1[2] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlDebitGL.SelectedValue, typeof(string));
            var1[3] = gb.CreateSPParameter("@DebitCC", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlDebitCC.SelectedValue, typeof(string));
            var1[4] = gb.CreateSPParameter("@KreditGL", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlKreditGL.SelectedValue, typeof(string));
            var1[5] = gb.CreateSPParameter("@KreditCC", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlKreditCC.SelectedValue, typeof(string));
            var1[6] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
            var1[7] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
            var1[8] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var1[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[10] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


            if (STATE == "UPDATEJURNAL")
            {
                if (ddlDebitCC.Enabled == true && ddlKreditCC.Enabled == true)
                {
                    if (ddlDebitCC.SelectedValue == "-")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Debit CC');", true);
                    }
                    else if (ddlKreditCC.SelectedValue == "-")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Kredit CC');", true);
                    }
                    else
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }
                    }
                }
                else if (ddlDebitCC.Enabled == false && ddlKreditCC.Enabled == false)
                {
                    ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
                else if (ddlDebitCC.Enabled == true && ddlKreditCC.Enabled == false)
                {
                    if (ddlDebitCC.SelectedValue == "-")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Debit CC');", true);
                    }
                    else
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }
                    }
                }
                else if (ddlDebitCC.Enabled == false && ddlKreditCC.Enabled == true)
                {
                    if (ddlKreditCC.SelectedValue == "-")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Kredit CC');", true);
                    }
                    else
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }
                    }
                }
            }
            else
            {
                gesearch.ListDataset = gbsearch.GetDataJustOutPut("[INV_SELECT]", var, "constrmf");
                if (gesearch.ListDataset.Tables[0].Rows.Count == 0)
                {
                    if (ddlDebitCC.Enabled == true && ddlKreditCC.Enabled == true)
                    {
                        if (ddlDebitCC.SelectedValue == "-")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Debit CC');", true);
                        }
                        else if (ddlKreditCC.SelectedValue == "-")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Kredit CC');", true);
                        }
                        else
                        {
                            ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                            }
                        }
                    }
                    else if (ddlDebitCC.Enabled == false && ddlKreditCC.Enabled == false)
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                            gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }
                    }
                    else if (ddlDebitCC.Enabled == true && ddlKreditCC.Enabled == false)
                    {
                        if (ddlDebitCC.SelectedValue == "-")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Debit CC');", true);
                        }
                        else
                        {
                            ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                            }
                        }
                    }
                    else if (ddlDebitCC.Enabled == false && ddlKreditCC.Enabled == true)
                    {
                        if (ddlKreditCC.SelectedValue == "-")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Kredit CC');", true);
                        }
                        else
                        {
                            ge.ListDataset = gb.GetDataJustOutPut("INV_CUD", var1, "constrmf");
                            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Jurnal " + txtKodeJurnal.Text + " already created, Please check this Code Again!');", true);

                }
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

    protected void DebitGL_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("INV_SELECT", var, "constrmf").Tables[0];

        var CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();


        if (CCFLAGDebit == "True")
        {
            ddlDebitCC.Enabled = true;
        }
        else
        {
            ddlDebitCC.Enabled = false;
            ddlDebitCC.SelectedValue = "-";
        }
    }
    protected void KreditGL_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var GetKreditGL = ddlKreditGL.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@KdCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("INV_SELECT", var, "constrmf").Tables[0];

        var CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

        if (CCFLAGKredit == "True")
        {
            ddlKreditCC.Enabled = true;
        }
        else
        {
            ddlKreditCC.Enabled = false;
            //ddlKreditCC.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddlKreditCC.SelectedValue = "-";
        }
    }

}