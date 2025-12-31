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

public partial class AS_Master_KodeClJurnalAsuransi : System.Web.UI.Page
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pnlheaderx.Visible = true;
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtkdJurnal.Enabled = true;
        txtkdJurnal.Focus();
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

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 8, SessKdpersh, typeof(string));
                var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));
                var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGL", typeof(string));
                var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";


                ddlDebitGL1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitGL1.DataTextField = "namaGL";
                ddlDebitGL1.DataValueField = "KodeGl";
                ddlDebitGL1.DataBind();
                ddlDebitGL1.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditGL1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditGL1.DataTextField = "namaGL";
                ddlKreditGL1.DataValueField = "KodeGl";
                ddlKreditGL1.DataBind();
                ddlKreditGL1.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlDebitGL2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitGL2.DataTextField = "namaGL";
                ddlDebitGL2.DataValueField = "KodeGl";
                ddlDebitGL2.DataBind();
                ddlDebitGL2.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditGL2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditGL2.DataTextField = "namaGL";
                ddlKreditGL2.DataValueField = "KodeGl";
                ddlKreditGL2.DataBind();
                ddlKreditGL2.Items.Insert(0, new ListItem("Pilih Data", "-"));
                  

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCC", typeof(string));
                var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 8, SessKdpersh, typeof(string));
                var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));



                ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCc asc";


                ddlDebitCC1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitCC1.DataTextField = "Nama";
                ddlDebitCC1.DataValueField = "KodeCc";
                ddlDebitCC1.DataBind();
                ddlDebitCC1.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditCC1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditCC1.DataTextField = "Nama";
                ddlKreditCC1.DataValueField = "KodeCc";
                ddlKreditCC1.DataBind();
                ddlKreditCC1.Items.Insert(0, new ListItem("Pilih Data", "-"));



                ddlDebitCC2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitCC2.DataTextField = "Nama";
                ddlDebitCC2.DataValueField = "KodeCc";
                ddlDebitCC2.DataBind();
                ddlDebitCC2.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlKreditCC2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditCC2.DataTextField = "Nama";
                ddlKreditCC2.DataValueField = "KodeCc";
                ddlKreditCC2.DataBind();
                ddlKreditCC2.Items.Insert(0, new ListItem("Pilih Data", "-"));




                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKODEBANK", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var3[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var3[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var3, "constrmf");
                ddlkodebank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkodebank.DataTextField = "kdnm";
                ddlkodebank.DataValueField = "kodebank";
                ddlkodebank.DataBind();
                ddlkodebank.Items.Insert(0, new ListItem("Pilih Data", "-"));
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

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf");

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
                    Pnlheaderx.Visible = true;
                    //loaddata();

                    txtkdJurnal.Enabled = false;
                    txtkdJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdJurnal")).Text;
                    txtketJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKetJurnal")).Text; 
                    var kodebank = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdSumber")).Text;
                    ddlkodebank.SelectedValue = kodebank;
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                    var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KODEBANK", typeof(string));
                    var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodebank.SelectedValue, typeof(string));
                    var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var2[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var2, "constrmf");  
                    txtnamabank.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();

                    txttypejurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltype")).Text;

                    ddlDebitGL1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL1")).Text;
                    ddlKreditGL1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL1")).Text;
                    ddlDebitGL2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL2")).Text;
                    ddlKreditGL2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL2")).Text;



                    var getdebitgl = ddlDebitGL1.SelectedValue;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
                    var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf").Tables[0];

                    //var CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
                    var CCFLAGDebit = "False";
                    if (ge.ListDataTable.Rows.Count != 0)
                    {
                        CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    }
                    else
                    {
                        CCFLAGDebit = "False";
                    }

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


                    var GetKreditGL = ddlKreditGL1.SelectedValue;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var1, "constrmf").Tables[0];

                    // var CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
                    var CCFLAGKredit = "False";
                    if (ge.ListDataTable.Rows.Count != 0)
                    {
                        CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    }
                    else
                    {
                        CCFLAGKredit = "False";
                    }
                    if (CCFLAGKredit == "True")
                    {
                        ddlKreditCC1.Enabled = true;
                    }
                    else
                    {
                        ddlKreditCC1.Enabled = false;
                        ddlKreditCC1.SelectedValue = "-";
                    }

                    var getdebitgl1 = ddlDebitGL2.SelectedValue;

                    DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[5];
                    var3[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl1, typeof(string));
                    var3[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var3[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf").Tables[0];

                    //var CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
                    var CCFLAGDebit1 = "False";
                    if (ge.ListDataTable.Rows.Count != 0)
                    {
                        CCFLAGDebit1 = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    }
                    else
                    {
                        CCFLAGDebit1 = "False";
                    }

                    if (CCFLAGDebit1 == "True")
                    {
                        ddlDebitCC2.Enabled = true;
                    }
                    else
                    {
                        ddlDebitCC2.Enabled = false;
                        ddlDebitCC2.SelectedValue = "-";
                    }

                    var GetKreditGL1 = ddlKreditGL2.SelectedValue;

                    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[5];
                    var4[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL1, typeof(string));
                    var4[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                    var4[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
                    var4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var4[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf").Tables[0];

                    //var CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
                    var CCFLAGKredit1 = "False";
                    if (ge.ListDataTable.Rows.Count != 0)
                    {
                        CCFLAGKredit1 = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

                    }
                    else
                    {
                        CCFLAGKredit1 = "False";
                    }
                    if (CCFLAGKredit1 == "True")
                    {
                        ddlKreditCC2.Enabled = true;
                    }
                    else
                    {
                        ddlKreditCC2.Enabled = false;
                        ddlKreditCC2.SelectedValue = "-";
                    }




                    ddlDebitCC1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC1")).Text;
                    ddlKreditCC1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC1")).Text; 
                    ddlDebitCC2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC2")).Text;
                    ddlKreditCC2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC2")).Text;


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
        Response.Redirect("KodeJurnal.aspx");
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
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[17];
            var[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, txtkdJurnal.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlkodebank.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@typejurnal", ParameterDirection.Input, SqlDbType.VarChar, 10, txttypejurnal.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@KetJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketJurnal.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL1.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@DebitCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC1.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@KreditGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL1.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@KreditCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC1.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@DebitGL2", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL2.SelectedValue, typeof(string));
            var[9] = gb.CreateSPParameter("@DebitCC2", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC2.SelectedValue, typeof(string));
            var[10] = gb.CreateSPParameter("@KreditGL2", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL2.SelectedValue, typeof(string));
            var[11] = gb.CreateSPParameter("@KreditCC2", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC2.SelectedValue, typeof(string)); 
            var[12] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
            var[13] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
            var[14] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));
            var[15] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATE", typeof(string));
            var[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf");
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


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void DebitGL1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL1.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf").Tables[0];

        //var CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
        var CCFLAGDebit = "False";
        if (ge.ListDataTable.Rows.Count != 0)
        {
            CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

        }
        else
        {
            CCFLAGDebit = "False";
        }

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
        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf").Tables[0];

        // var CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
        var CCFLAGKredit = "False";
        if (ge.ListDataTable.Rows.Count != 0)
        {
            CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

        }
        else
        {
            CCFLAGKredit = "False";
        }
        if (CCFLAGKredit == "True")
        {
            ddlKreditCC1.Enabled = true;
        }
        else
        {
            ddlKreditCC1.Enabled = false;
            ddlKreditCC1.SelectedValue = "-";
        }
    }
    protected void DebitGL2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL2.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf").Tables[0];

        //var CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
        var CCFLAGDebit = "False";
        if (ge.ListDataTable.Rows.Count != 0)
        {
            CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

        }
        else
        {
            CCFLAGDebit = "False";
        }

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
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var, "constrmf").Tables[0];

        //var CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
        var CCFLAGKredit = "False";
        if (ge.ListDataTable.Rows.Count != 0)
        {
            CCFLAGKredit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();

        }
        else
        {
            CCFLAGKredit = "False";
        }
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
    protected void ddlkode_onselectedindexchanged(object sender, EventArgs args)
    { 
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KODEBANK", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kodebank", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlkodebank.SelectedValue, typeof(string));
        var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var2[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnalAsuransi", var2, "constrmf"); 
        txtnamabank.Text = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
    }
}