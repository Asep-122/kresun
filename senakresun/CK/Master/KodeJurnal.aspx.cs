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

public partial class CL_Master_KodeJurnal : System.Web.UI.Page
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


                ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var1, "constrmf");

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

                ddlDebitGL3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitGL3.DataTextField = "namaGL";
                ddlDebitGL3.DataValueField = "KodeGl";
                ddlDebitGL3.DataBind();
                ddlDebitGL3.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditGL3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditGL3.DataTextField = "namaGL";
                ddlKreditGL3.DataValueField = "KodeGl";
                ddlKreditGL3.DataBind();
                ddlKreditGL3.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlDebitGL4.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitGL4.DataTextField = "namaGL";
                ddlDebitGL4.DataValueField = "KodeGl";
                ddlDebitGL4.DataBind();
                ddlDebitGL4.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditGL4.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditGL4.DataTextField = "namaGL";
                ddlKreditGL4.DataValueField = "KodeGl";
                ddlKreditGL4.DataBind();
                ddlKreditGL4.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlDebitGL5.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitGL5.DataTextField = "namaGL";
                ddlDebitGL5.DataValueField = "KodeGl";
                ddlDebitGL5.DataBind();
                ddlDebitGL5.Items.Insert(0, new ListItem("Pilih Data", "-"));


                ddlKreditGL5.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditGL5.DataTextField = "namaGL";
                ddlKreditGL5.DataValueField = "KodeGl";
                ddlKreditGL5.DataBind();
                ddlKreditGL5.Items.Insert(0, new ListItem("Pilih Data", "-"));




                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4]; 
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCC", typeof(string));
                var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 8, SessKdpersh, typeof(string));
                var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));



                ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var2, "constrmf");

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


                ddlDebitCC3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitCC3.DataTextField = "Nama";
                ddlDebitCC3.DataValueField = "KodeCc";
                ddlDebitCC3.DataBind();
                ddlDebitCC3.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlKreditCC3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditCC3.DataTextField = "Nama";
                ddlKreditCC3.DataValueField = "KodeCc";
                ddlKreditCC3.DataBind();
                ddlKreditCC3.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlDebitCC4.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitCC4.DataTextField = "Nama";
                ddlDebitCC4.DataValueField = "KodeCc";
                ddlDebitCC4.DataBind();
                ddlDebitCC4.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlKreditCC4.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditCC4.DataTextField = "Nama";
                ddlKreditCC4.DataValueField = "KodeCc";
                ddlKreditCC4.DataBind();
                ddlKreditCC4.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlDebitCC5.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlDebitCC5.DataTextField = "Nama";
                ddlDebitCC5.DataValueField = "KodeCc";
                ddlDebitCC5.DataBind();
                ddlDebitCC5.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlKreditCC5.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKreditCC5.DataTextField = "Nama";
                ddlKreditCC5.DataValueField = "KodeCc";
                ddlKreditCC5.DataBind();
                ddlKreditCC5.Items.Insert(0, new ListItem("Pilih Data", "-"));

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
    protected void ddlKodeSumber_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (DDLKdSumber.SelectedValue == "1")
        {
            txtketJurnal.Text = "Account Payable";
        }
        else if (DDLKdSumber.SelectedValue == "2")
        {
            txtketJurnal.Text = "Account Receivable";
        }
        else if (DDLKdSumber.SelectedValue == "3")
        {
            txtketJurnal.Text = "Cash Ledger";
        }
        else 
        {
            txtketJurnal.Text = "";
        }
     
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLTYPEJURNAL", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@sumber", ParameterDirection.Input, SqlDbType.VarChar, 8, DDLKdSumber.SelectedItem.Text, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TypeJournal asc";
        ddljournal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljournal.DataTextField = "Nama";
        ddljournal.DataValueField = "TypeJournal";
        ddljournal.DataBind();
        ddljournal.Items.Insert(0, new ListItem("Pilih Data", "-"));
       

    }
        public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf");

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
                    DDLKdSumber.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdSumber")).Text;
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                    var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLTYPEJURNAL", typeof(string));
                    var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2[2] = gb.CreateSPParameter("@sumber", ParameterDirection.Input, SqlDbType.VarChar, 8, DDLKdSumber.SelectedItem.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var2, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "TypeJournal asc";
                    ddljournal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljournal.DataTextField = "Nama";
                    ddljournal.DataValueField = "TypeJournal";
                    ddljournal.DataBind();
                    ddljournal.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddljournal.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltype")).Text;

                    ddlDebitGL1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL1")).Text;
                    ddlDebitCC1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC1")).Text;
                    ddlKreditGL1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL1")).Text;
                    ddlKreditCC1.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC1")).Text;


                    ddlDebitGL2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL2")).Text;
                    ddlDebitCC2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC2")).Text;
                    ddlKreditGL2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL2")).Text;
                    ddlKreditCC2.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC2")).Text;

                    ddlDebitGL3.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL3")).Text;
                    ddlDebitCC3.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC3")).Text;
                    ddlKreditGL3.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL3")).Text;
                    ddlKreditCC3.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC3")).Text;

                    ddlDebitGL4.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL4")).Text;
                    ddlDebitCC4.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC4")).Text;
                    ddlKreditGL4.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL4")).Text;
                    ddlKreditCC4.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC4")).Text;

                    ddlDebitGL5.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitGL5")).Text;
                    ddlDebitCC5.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblDebitCC5")).Text;
                    ddlKreditGL5.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditGL5")).Text;
                    ddlKreditCC5.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKreditCC5")).Text;


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
                    DDLKdSumber.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdSumber")).Text;
                    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                    var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLTYPEJURNAL", typeof(string));
                    var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var2[2] = gb.CreateSPParameter("@sumber", ParameterDirection.Input, SqlDbType.VarChar, 8, DDLKdSumber.SelectedItem.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var2, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "TypeJournal asc";
                    ddljournal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljournal.DataTextField = "Nama";
                    ddljournal.DataValueField = "TypeJournal";
                    ddljournal.DataBind();
                    ddljournal.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddljournal.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltype")).Text;



                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, txtkdJurnal.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[3] = gb.CreateSPParameter("@sumber", ParameterDirection.Input, SqlDbType.VarChar, 2, DDLKdSumber.SelectedItem.Text, typeof(string));
                    var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDJURNAL", typeof(string));
                    
                     
                    ge.ListDataset = gb.GetDataJustOutPut("CL_CekJournal", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Journal sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                         DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[6];
                        varx[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, txtkdJurnal.Text, typeof(string));
                        varx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                        varx[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        varx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        varx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        varx[5] = gb.CreateSPParameter("@typejurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljournal.SelectedValue, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", varx, "constrmf");
                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
                            Response.Redirect("KodeJurnal.aspx");
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
                DefaultClass.Global.SqlHelper[] varCek = new DefaultClass.Global.SqlHelper[4];
                varCek[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, txtkdJurnal.Text, typeof(string));
                varCek[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                varCek[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                varCek[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50,"CEKJURNAL", typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("CL_CekJournal", varCek, "constrmf");

                var KodeJurnal = ge.ListDataset.Tables[0].Rows[0][0].ToString();


                if (KodeJurnal != "KOSONG")

                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Journal " + txtkdJurnal.Text + " sudah ada, silahkan cek kembali');", true);
                }
                else
                {
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[29];
                    var[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, txtkdJurnal.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@sumber", ParameterDirection.Input, SqlDbType.VarChar, 2, DDLKdSumber.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@typejurnal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljournal.SelectedValue, typeof(string));
                    var[3] = gb.CreateSPParameter("@KetJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketJurnal.Text, typeof(string));
                    var[4] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL1.SelectedValue, typeof(string));
                    var[5] = gb.CreateSPParameter("@DebitCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC1.SelectedValue, typeof(string));
                    var[6] = gb.CreateSPParameter("@KreditGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL1.SelectedValue, typeof(string));
                    var[7] = gb.CreateSPParameter("@KreditCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC1.SelectedValue, typeof(string));
                    var[8] = gb.CreateSPParameter("@DebitGL2", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL2.SelectedValue, typeof(string));
                    var[9] = gb.CreateSPParameter("@DebitCC2", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC2.SelectedValue, typeof(string));
                    var[10] = gb.CreateSPParameter("@KreditGL2", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL2.SelectedValue, typeof(string));
                    var[11] = gb.CreateSPParameter("@KreditCC2", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC2.SelectedValue, typeof(string));
                    var[12] = gb.CreateSPParameter("@DebitGL3", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL3.SelectedValue, typeof(string));
                    var[13] = gb.CreateSPParameter("@DebitCC3", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC3.SelectedValue, typeof(string));
                    var[14] = gb.CreateSPParameter("@KreditGL3", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL3.SelectedValue, typeof(string));
                    var[15] = gb.CreateSPParameter("@KreditCC3", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC3.SelectedValue, typeof(string));
                    var[16] = gb.CreateSPParameter("@DebitGL4", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL4.SelectedValue, typeof(string));
                    var[17] = gb.CreateSPParameter("@DebitCC4", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC4.SelectedValue, typeof(string));
                    var[18] = gb.CreateSPParameter("@KreditGL4", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL4.SelectedValue, typeof(string));
                    var[19] = gb.CreateSPParameter("@KreditCC4", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC4.SelectedValue, typeof(string));
                    var[20] = gb.CreateSPParameter("@DebitGL5", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL5.SelectedValue, typeof(string));
                    var[21] = gb.CreateSPParameter("@DebitCC5", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC5.SelectedValue, typeof(string));
                    var[22] = gb.CreateSPParameter("@KreditGL5", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL5.SelectedValue, typeof(string));
                    var[23] = gb.CreateSPParameter("@KreditCC5", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC5.SelectedValue, typeof(string));

                    var[24] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                    var[25] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
                    var[26] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));
                    var[27] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                    var[28] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf");
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
            }

            else
            {


              

                    STATE = "UPDATE";
            


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[29];
                var[0] = gb.CreateSPParameter("@KodeJurnal", ParameterDirection.Input, SqlDbType.VarChar, 8, txtkdJurnal.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@sumber", ParameterDirection.Input, SqlDbType.VarChar, 2, DDLKdSumber.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@typejurnal", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljournal.SelectedValue, typeof(string));
                var[3] = gb.CreateSPParameter("@KetJurnal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketJurnal.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL1.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@DebitCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC1.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@KreditGL", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL1.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@KreditCC", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC1.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@DebitGL2", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL2.SelectedValue, typeof(string));
                var[9] = gb.CreateSPParameter("@DebitCC2", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC2.SelectedValue, typeof(string));
                var[10] = gb.CreateSPParameter("@KreditGL2", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL2.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@KreditCC2", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC2.SelectedValue, typeof(string));
                var[12] = gb.CreateSPParameter("@DebitGL3", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL3.SelectedValue, typeof(string));
                var[13] = gb.CreateSPParameter("@DebitCC3", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC3.SelectedValue, typeof(string));
                var[14] = gb.CreateSPParameter("@KreditGL3", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL3.SelectedValue, typeof(string));
                var[15] = gb.CreateSPParameter("@KreditCC3", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC3.SelectedValue, typeof(string));
                var[16] = gb.CreateSPParameter("@DebitGL4", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL4.SelectedValue, typeof(string));
                var[17] = gb.CreateSPParameter("@DebitCC4", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC4.SelectedValue, typeof(string));
                var[18] = gb.CreateSPParameter("@KreditGL4", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL4.SelectedValue, typeof(string));
                var[19] = gb.CreateSPParameter("@KreditCC4", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC4.SelectedValue, typeof(string));
                var[20] = gb.CreateSPParameter("@DebitGL5", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlDebitGL5.SelectedValue, typeof(string));
                var[21] = gb.CreateSPParameter("@DebitCC5", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlDebitCC5.SelectedValue, typeof(string));
                var[22] = gb.CreateSPParameter("@KreditGL5", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlKreditGL5.SelectedValue, typeof(string));
                var[23] = gb.CreateSPParameter("@KreditCC5", ParameterDirection.Input, SqlDbType.VarChar, 6, ddlKreditCC5.SelectedValue, typeof(string));

                var[24] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                var[25] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
                var[26] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 8, SessBranchID, typeof(string));
                var[27] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[28] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf");
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

    protected void DebitGL1_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        var getdebitgl = ddlDebitGL1.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf").Tables[0];

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
    protected void KreditGL1_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        var GetKreditGL = ddlKreditGL1.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePrsh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("AP_KodeJurnal", var, "constrmf").Tables[0];

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

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf").Tables[0];

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
        var[1] = gb.CreateSPParameter("@KodePrsh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("AP_KodeJurnal", var, "constrmf").Tables[0];

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

    protected void DebitGL3_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL3.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf").Tables[0];

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
            ddlDebitCC3.Enabled = true;
        }
        else
        {
            ddlDebitCC3.Enabled = false;
            //ddlDebitCC.Items.Insert(0, new ListItem("Pilih Data", "-"));
            ddlDebitCC3.SelectedValue = "-";
        }
    }
    protected void KreditGL3_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var GetKreditGL = ddlKreditGL3.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePrsh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("AP_KodeJurnal", var, "constrmf").Tables[0];

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
            ddlKreditCC3.Enabled = true;
        }
        else
        {
            ddlKreditCC3.Enabled = false;
            ddlKreditCC3.SelectedValue = "-";
        }
    }
    protected void DebitGL4_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL4.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf").Tables[0];

        //var CCFLAGDebit = ge.ListDataTable.Rows[0]["CCFLAG"].ToString();
        var CCFLAGDebit = "False";
        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf").Tables[0];
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
            ddlDebitCC4.Enabled = true;
        }
        else
        {
            ddlDebitCC4.Enabled = false;
            ddlDebitCC4.SelectedValue = "-";
        }
    }
    protected void KreditGL4_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var GetKreditGL = ddlKreditGL4.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePrsh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("AP_KodeJurnal", var, "constrmf").Tables[0];

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
            ddlKreditCC4.Enabled = true;
        }
        else
        {
            ddlKreditCC4.Enabled = false;
            ddlKreditCC4.SelectedValue = "-";
        }
    }
    protected void DebitGL5_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var getdebitgl = ddlDebitGL5.SelectedValue;
       
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, getdebitgl, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var CCFLAGDebit = "False";
        ge.ListDataTable = gb.GetDataJustOutPut("CL_KodeJurnal", var, "constrmf").Tables[0];
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
            ddlDebitCC5.Enabled = true;
        }
        else
        {
            ddlDebitCC5.Enabled = false;
            ddlDebitCC5.SelectedValue = "-";
        }
    }
    protected void KreditGL5_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var GetKreditGL = ddlKreditGL5.SelectedValue;

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@DebitGL", ParameterDirection.Input, SqlDbType.VarChar, 10, GetKreditGL, typeof(string));
        var[1] = gb.CreateSPParameter("@KodePrsh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCCFLAG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataTable = gb.GetDataJustOutPut("AP_KodeJurnal", var, "constrmf").Tables[0];
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
            ddlKreditCC5.Enabled = true;
        }
        else
        {
            ddlKreditCC5.Enabled = false;
            ddlKreditCC5.SelectedValue = "-";
        }

 
           

        
    



    }
}