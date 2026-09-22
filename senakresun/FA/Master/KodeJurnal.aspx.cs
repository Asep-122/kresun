using DefaultClass;
using DocumentFormat.OpenXml.EMMA;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FA_Master_KodeJurnal : System.Web.UI.Page
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
    Global gb1 = new Global();
    GlobalEntities ge1 = new GlobalEntities();

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
                listdropdown();
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtkdJurnal.Enabled = true;
        txtkdJurnal.Focus();
    }

    public void listdropdown()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORGL", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";

            ddlDebitGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlDebitGL.DataTextField = "namaGL";
            ddlDebitGL.DataValueField = "KodeGl";
            ddlDebitGL.DataBind();
            ddlDebitGL.Items.Insert(0, new ListItem("Pilih Data", ""));


            ddlKreditGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlKreditGL.DataTextField = "namaGL";
            ddlKreditGL.DataValueField = "KodeGl";
            ddlKreditGL.DataBind();
            ddlKreditGL.Items.Insert(0, new ListItem("Pilih Data", ""));


            DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
            var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var2[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCC", typeof(string));
            var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var2, "constrmf");

            //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCc asc";
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGL asc";


            ddlDebitCC.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlDebitCC.DataTextField = "Nama";
            //ddlDebitCC.DataValueField = "KodeCc";
            ddlDebitCC.DataValueField = "KodeGL";
            ddlDebitCC.DataBind();
            ddlDebitCC.Items.Insert(0, new ListItem("Pilih Data", "-"));


            ddlKreditCC.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlKreditCC.DataTextField = "Nama";
            //ddlKreditCC.DataValueField = "KodeCc";
            ddlKreditCC.DataValueField = "KodeGL"; 
            ddlKreditCC.DataBind();
            ddlKreditCC.Items.Insert(0, new ListItem("Pilih Data", "-"));
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
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));
            var[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 8, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";


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
    protected void cekkode_change(object sender, EventArgs e)
    {
        try
        {
            //string convertjurnal = "FA" + txtkdJurnal.Text;
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkdJurnal.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKODEJURNAL", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Kode Jurnal yang anda masukkan sudah ada..!!');", true);
                txtkdJurnal.Text = "";
                txtkdJurnal.Focus();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void ceknama_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@Nama", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnama.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKNAMAJURNAL", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Nama Jurnal yang anda masukkan sudah ada..!!');", true);
                txtnama.Text = "";
                txtnama.Focus();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void debitgl_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlDebitGL.SelectedValue, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKDEBITGLATAUCC", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf");

            bool ccFlag = (bool)ge.ListDataset.Tables[0].Rows[0]["CCFlag"];

            //if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "False")
            if (!ccFlag)
            {
                ddlDebitCC.Enabled = false;
                //ddlDebitCC.Items.Insert(0, new ListItem("-", "-"));
                ddlDebitCC.SelectedValue = "-";
                //string tesmasuk = ddlDebitCC.SelectedValue;
            }
            //else if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "True")
            else if (ccFlag)
            {
                ddlDebitCC.Enabled = true;
                //ddlDebitCC.Items.RemoveAt(0);
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void kreditgl_change(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlKreditGL.SelectedValue, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKDEBITGLATAUCC", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "False")
            {
                ddlKreditCC.Enabled = false;
                //ddlKreditCC.Items.Insert(0, new ListItem("-", "-"));
                ddlKreditCC.SelectedValue = "-";

            }
            else if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "True")
            {
                ddlKreditCC.Enabled = true;
                //ddlKreditCC.Items.RemoveAt(0);
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void debitgl(string debitgl)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 50, debitgl, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKDEBITGLATAUCC", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "False")
            {
                ddlDebitCC.Enabled = false;
                //ddlDebitCC.Items.Insert(0, new ListItem("-", "-"));
                //ddlDebitCC.SelectedValue = debitgl;
                ddlDebitCC.SelectedValue = "-";

            }
            else if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "True")
            {
                ddlDebitCC.Enabled = true;
                //ddlDebitCC.Items.RemoveAt(0);
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void kreditgl(string kreditgl)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 50, kreditgl, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKDEBITGLATAUCC", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "False")
            {
                ddlKreditCC.Enabled = false;
                //ddlKreditCC.Items.Insert(0, new ListItem("-", "-"));
                //ddlKreditCC.SelectedValue = kreditgl;
                ddlKreditCC.SelectedValue = "-";
            }
            else if (ge.ListDataset.Tables[0].Rows[0]["CCFlag"].ToString() == "True")
            {
                ddlKreditCC.Enabled = true;
                //ddlKreditCC.Items.RemoveAt(0);
            }
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
                    //loaddata();

                    txtkdJurnal.Enabled = false;
                    string masuk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC")).Text;
                    string ammbil = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdJurnal")).Text;
                    txtkdJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdJurnal")).Text;
                    txtnama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                    ddlDebitGL.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL")).Text;
                    ddlDebitCC.SelectedValue = masuk != "-" ? masuk : "-";
                    ddlKreditGL.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL")).Text;
                    ddlKreditCC.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC")).Text != "-" ? ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC")).Text : "-";
                    debitgl(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbldebitgl")).Text);
                    kreditgl(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkreditgl")).Text);
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
                    txtkdJurnal.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdJurnal")).Text;
                    //string convertjurnal = "FA" + txtkdJurnal.Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkdJurnal.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDJURNAL", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf");
                    ge.ListDataTable = gb.GetDataJustOutPut("FA_KodeJurnal", var1, "constrmf").Tables[0];

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataTable.Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Jurnal sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {
                        //string convertjurnal = "FA" + txtkdJurnal.Text;
                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkdJurnal.Text, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("FA_KodeJurnal", var, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully save this Project','top-right','success','2');", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Delete('" + textkode.InnerText + "','" + txtkdJurnal.Text + "','Nama Jurnal','" + txtnama.Text + "');", true);
                            //Response.Redirect("KodeJurnal.aspx");
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
    private void loaddata()
    {
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
            if (txtkdJurnal.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }
            if (ddlKreditCC.Enabled == true && ddlKreditCC.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Anda Belum Memilih Kredit CC');", true);
            }
            else if (ddlDebitCC.Enabled == true && ddlDebitCC.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Anda Belum Memilih Debit CC');", true);
            }
            else
            {
                //string convertjurnal = "FA" + txtkdJurnal.Text;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb1.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkdJurnal.Text, typeof(string));
            var[1] = gb1.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL.SelectedValue, typeof(string));
            var[2] = gb1.CreateSPParameter("@DebitCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC.SelectedValue, typeof(string));
            var[3] = gb1.CreateSPParameter("@KreditGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL.SelectedValue, typeof(string));
            var[4] = gb1.CreateSPParameter("@KreditCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC.SelectedValue, typeof(string));
            var[5] = gb1.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
            var[6] = gb1.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            var[7] = gb1.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));
            var[8] = gb1.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[9] = gb1.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[10] = gb1.CreateSPParameter("@Nama", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnama.Text, typeof(string));

            if (txtkdJurnal.Enabled == true)
            {
                ge1.ListDataset = gb1.GetDataJustOutPut("FA_KodeJurnal", var, "constrmf");
            }
            else
            {
                ge1.ListDataset = gb1.GetDataJustOutPut("FA_KodeJurnal", var, "constrmf");
            }

            if (ge1.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Simpan('" + textkode.InnerText + "','" + convertjurnal + "','Nama Jurnal','" + txtnama.Text + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
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
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

}