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


public partial class AS_Master_KodeAsuransiJournal : System.Web.UI.Page
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
    string CCFLAGKredit = "";
    string CCFLAGDebit = "";
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        pnljurnal.Visible = true;
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {

                BindGridEntity();


                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4]; 
                var1[0] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGL", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)); 
                ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";
                 
                ddlDebitGL1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitGL1.DataTextField = "kdnmGL";
                ddlDebitGL1.DataValueField = "KodeGl";
                ddlDebitGL1.DataBind();
                ddlDebitGL1.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlDebitGL2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitGL2.DataTextField = "kdnmGL";
                ddlDebitGL2.DataValueField = "KodeGl";
                ddlDebitGL2.DataBind();
                ddlDebitGL2.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditGL1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditGL1.DataTextField = "kdnmGL";
                ddlKreditGL1.DataValueField = "KodeGl";
                ddlKreditGL1.DataBind();
                ddlKreditGL1.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditGL2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditGL2.DataTextField = "kdnmGL";
                ddlKreditGL2.DataValueField = "KodeGl";
                ddlKreditGL2.DataBind();
                ddlKreditGL2.Items.Insert(0, new ListItem("Pilih Data", "-"));

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4]; 
                var2[0] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCC", typeof(string));
                var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCc asc";


                ddlDebitCC1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitCC1.DataTextField = "Nama";
                ddlDebitCC1.DataValueField = "KodeCc";
                ddlDebitCC1.DataBind();
                ddlDebitCC1.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlDebitCC2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitCC2.DataTextField = "Nama";
                ddlDebitCC2.DataValueField = "KodeCc";
                ddlDebitCC2.DataBind();
                ddlDebitCC2.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlKreditCC1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditCC1.DataTextField = "Nama";
                ddlKreditCC1.DataValueField = "KodeCc";
                ddlKreditCC1.DataBind();
                ddlKreditCC1.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditCC2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditCC2.DataTextField = "Nama";
                ddlKreditCC2.DataValueField = "KodeCc";
                ddlKreditCC2.DataBind();
                ddlKreditCC2.Items.Insert(0, new ListItem("Pilih Data", "-"));
            } 

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";


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

                    txtKodeJurnal.Enabled = false;
                    txtKodeJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdJurnal")).Text;
                    txtKetJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKetJurnal")).Text;
                    ddlDebitGL1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL")).Text;
                    ddlDebitCC1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC")).Text;
                    ddlKreditGL1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL")).Text;
                    ddlKreditCC1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC")).Text;
                    ddlDebitGL2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL1")).Text;
                    ddlDebitCC2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC1")).Text;
                    ddlKreditGL2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL1")).Text;
                    ddlKreditCC2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC1")).Text;
                    //txtDebitGL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL")).Text;
                    //txtDebitCC.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC")).Text;
                    //txtKreditGL.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL")).Text;
                    //txtKreditCC.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC")).Text; 

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlDebitGL1.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var, "constrmf").Tables[0];

                    CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    if (CCFLAGDebit == "True")
                    {
                        ddlDebitCC1.Enabled = true;
                    }
                    else
                    {
                        ddlDebitCC1.Enabled = false;
                        ddlDebitCC1.SelectedValue = "-";
                    }

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlKreditGL1.SelectedValue, typeof(string));
                    var1[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf").Tables[0];

                    CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    if (CCFLAGKredit == "True")
                    {
                        ddlKreditCC1.Enabled = true;
                    }
                    else
                    {
                        ddlKreditCC1.Enabled = false;
                        ddlKreditCC1.SelectedValue = "-";
                    }

                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                    var2[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlDebitGL2.SelectedValue, typeof(string));
                    var2[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var2, "constrmf").Tables[0];

                    CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    if (CCFLAGDebit == "True")
                    {
                        ddlDebitCC2.Enabled = true;
                    }
                    else
                    {
                        ddlDebitCC2.Enabled = false;
                        ddlDebitCC2.SelectedValue = "-";
                    }

                    DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
                    var3[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlKreditGL2.SelectedValue, typeof(string));
                    var3[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var3[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var3, "constrmf").Tables[0];

                    CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    if (CCFLAGKredit == "True")
                    {
                        ddlKreditCC2.Enabled = true;
                    }
                    else
                    {
                        ddlKreditCC2.Enabled = false;
                        ddlKreditCC2.SelectedValue = "-";
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

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 6, txtKodeJurnal.Text, typeof(string)); 
                    var1[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDGJURNAL", typeof(string));
                    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Jurnal sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 6, txtKodeJurnal.Text, typeof(string)); 
                        var[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var, "constrmf");
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
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            } 

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[15];
                var1[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 6, txtKodeJurnal.Text, typeof(string));
                var1[1] = gb.CreateSPParameter("@KetJurnal", ParameterDirection.Input, SqlDbType.VarChar, 40, txtKetJurnal.Text, typeof(string));
                var1[2] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlDebitGL1.SelectedValue, typeof(string));
                var1[3] = gb.CreateSPParameter("@DebitCC", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlDebitCC1.SelectedValue, typeof(string));
                var1[4] = gb.CreateSPParameter("@KreditGL", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlKreditGL1.SelectedValue, typeof(string));
                var1[5] = gb.CreateSPParameter("@KreditCC", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlKreditCC1.SelectedValue, typeof(string));
                var1[6] = gb.CreateSPParameter("@DebitGL1", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlDebitGL2.SelectedValue, typeof(string));
                var1[7] = gb.CreateSPParameter("@DebitCC1", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlDebitCC2.SelectedValue, typeof(string));
                var1[8] = gb.CreateSPParameter("@KreditGL1", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlKreditGL2.SelectedValue, typeof(string));
                var1[9] = gb.CreateSPParameter("@KreditCC1", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlKreditCC2.SelectedValue, typeof(string));
                var1[10] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                var1[11] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                var1[12] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var1[13] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[14] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            //if(CCFLAGDebit == "False")
            if (ddlDebitCC1.Enabled == true && ddlKreditCC1.Enabled == true)
            {
                if (ddlDebitCC1.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Debit CC');", true);
                }
                else if (ddlKreditCC1.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Kredit CC');", true);
                }
                else
                {
                    if (txtKodeJurnal.Enabled == true)
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                    }
                    else
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                    }


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
            else if (ddlDebitCC1.Enabled == false && ddlKreditCC1.Enabled == false)
            {
                if (txtKodeJurnal.Enabled == true)
                {
                    ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                }
                else
                {
                    ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                }


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
            else if (ddlDebitCC1.Enabled == true && ddlKreditCC1.Enabled == false)
            {
                if (ddlDebitCC1.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Debit CC');", true);
                }
                else
                {
                    if (txtKodeJurnal.Enabled == true)
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                    }
                    else
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                    }


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
            else if (ddlDebitCC1.Enabled == false && ddlKreditCC1.Enabled == true)
            {
                if (ddlKreditCC1.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Data Kredit CC');", true);
                }
                else
                {
                    if (txtKodeJurnal.Enabled == true)
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                    }
                    else
                    {
                        ge.ListDataset = gb.GetDataJustOutPut("As_KodeJurnal", var1, "constrmf");
                    }


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
    protected void DebitGL1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL1.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var, "constrmf").Tables[0];

        CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();


        if (CCFLAGDebit == "True")
        {
            ddlDebitCC1.Enabled = true;
        }
        else
        {
            ddlDebitCC1.Enabled = false;
            //ddlDebitCC.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddlDebitCC1.SelectedValue = "-";
        }
    }
    protected void KreditGL1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var GetKreditGL = ddlKreditGL1.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var, "constrmf").Tables[0];

        CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

        if (CCFLAGKredit == "True")
        {
            ddlKreditCC1.Enabled = true;
        }
        else
        {
            ddlKreditCC1.Enabled = false;
            //ddlKreditCC.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddlKreditCC1.SelectedValue = "-";
        }
    }
    protected void DebitGL2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL2.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var, "constrmf").Tables[0];

        CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();


        if (CCFLAGDebit == "True")
        {
            ddlDebitCC2.Enabled = true;
        }
        else
        {
            ddlDebitCC2.Enabled = false; 
            ddlDebitCC2.SelectedValue = "-";
        }
    }
    protected void KreditGL2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var GetKreditGL = ddlKreditGL2.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("As_KodeJurnal", var, "constrmf").Tables[0];

        CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

        if (CCFLAGKredit == "True")
        {
            ddlKreditCC2.Enabled = true;
        }
        else
        {
            ddlKreditCC2.Enabled = false;
            //ddlKreditCC.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddlKreditCC2.SelectedValue = "-";
        }
    }
}