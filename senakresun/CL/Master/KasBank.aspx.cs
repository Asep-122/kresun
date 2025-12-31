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

public partial class CL_Master_KasBank : System.Web.UI.Page
{
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }

    }
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
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        ddlBankId.Enabled = true;
        ddlBankId.Focus();
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
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4]; 
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORBANK", typeof(string));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var1, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";

                ddlBankId.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlBankId.DataTextField = "GL";
                ddlBankId.DataValueField = "KodeGl";
                ddlBankId.DataBind();
                ddlBankId.Items.Insert(0, new ListItem("Pilih Data", "-"));


                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
                var2[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCY", typeof(string));
                var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var2, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";

                ddlCrc.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlCrc.DataTextField = "Currency";
                ddlCrc.DataValueField = "KodeCrc";
                ddlCrc.DataBind();
                ddlCrc.Items.Insert(0, new ListItem("Pilih Data", "-"));

                if (SessSystemKurs == "False")
                {
                    ddlCrc.SelectedValue = "IDR";
                    ddlCrc.Enabled = false;
                 
                }
                else
                {
                    ddlCrc.SelectedValue = "USD";
                    ddlCrc.Enabled = false;
                }

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

    public void ddlNamaBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bankID = ddlBankId.SelectedValue;
        if(bankID != "-")
        { 
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, bankID, typeof(string)); 
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORNAMABANK", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var1, "constrmf");
         
            string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaGl"].ToString();
            txtnmBank.Text = NamaBank; 
        }
        else
        {
            txtnmBank.Text = bankID;
        }

    }


    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4]; 
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


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
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                    var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORBANKLIST", typeof(string));
                    var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var1, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";

                    ddlBankId.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddlBankId.DataTextField = "GL";
                    ddlBankId.DataValueField = "KodeBank";
                    ddlBankId.DataBind();
                    ddlBankId.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddlBankId.Enabled = false;
                    ddlBankId.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdBank")).Text;
                    txtnmBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNmBank")).Text;
                    ddlCrc.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdCrc")).Text;
                    txtalamat.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalmt")).Text;
                    txtCity.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcity")).Text;
                    txtPropinsi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblpropinsi")).Text;
                    txtzipcode.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblzipcode")).Text;
                    txtContPers.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblConPer")).Text;
                    txtContNo.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltelp")).Text;
                    txtbtskredit.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbtskredit")).Text;
                    ddlTypeRek.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTypeRek")).Text;
                    txtRekBank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoRek")).Text;
                    txtBungaBiasa.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbunga")).Text;
                    txtBungaovd.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbungaovd")).Text;

                    txtSaldoAB.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldoawalbul")).Text;
                    txtSaldoAT.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsaldoakhirtah")).Text;

                    //var param1 = txtSaldoAB.Text.ToString().Replace(",0000","").Replace(".0000","");
                    //var param2 = txtSaldoAT.Text.ToString().Replace(",0000", "").Replace(".0000", "");
                    //Int64 x, y; 
                    //x= Int64.Parse(param1);
                    //y = Int64.Parse(param2); 
                    //string formatted1 = string.Format(CultureInfo.InvariantCulture, "{0:N0}", x);
                    //string formatted2 = string.Format(CultureInfo.InvariantCulture, "{0:N0}", y); 
                    //txtSaldoAB.Text = Convert.ToString(formatted1);
                    //txtSaldoAT.Text = Convert.ToString(formatted2);


                    txtMemo.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblmemo")).Text;
                    txttglkredit.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglkredit")).Text;
                    var ckbx = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonaktif")).Text;
                    if(ckbx == "True")
                    {
                        CheckBox1.Checked = true;
                    }
                    else
                    {
                        CheckBox1.Checked = false;
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
                    //ddlBankId.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdBank")).Text;
                    var kdbank = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdBank")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, kdbank, typeof(string)); 
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDBANK", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string)); 
                    var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var1, "constrmf");

                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Bank sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {
                         DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, kdbank, typeof(string));  
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
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
        Response.Redirect("KasBank.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        var ckbx = "";

        if (CheckBox1.Checked == true)
        {
            ckbx = "1";
        }
        else
        {
            ckbx = "0";
        }

        if (ddlCrc.SelectedValue == "-")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Currency');", true);
        }
        else
        {
            try
            {
                if (ddlBankId.Enabled == true)
                {
                    STATE = "INSERT";
                    DefaultClass.Global.SqlHelper[] varcek = new DefaultClass.Global.SqlHelper[5];
                    varcek[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlBankId.SelectedValue, typeof(string));
                    varcek[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
                    varcek[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    varcek[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKIDBANK", typeof(string));
                    varcek[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", varcek, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count == 0)
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[25];
                        var[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlBankId.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@NamaBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnmBank.Text, typeof(string));
                        var[2] = gb.CreateSPParameter("@NamaPendek", ParameterDirection.Input, SqlDbType.VarChar, 50, txtContPers.Text, typeof(string));
                        var[3] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamat.Text, typeof(string));
                        var[4] = gb.CreateSPParameter("@Alamat2", ParameterDirection.Input, SqlDbType.VarChar, 50, txtCity.Text, typeof(string));
                        var[5] = gb.CreateSPParameter("@Alamat3", ParameterDirection.Input, SqlDbType.VarChar, 50, txtPropinsi.Text, typeof(string));
                        var[6] = gb.CreateSPParameter("@Telepon", ParameterDirection.Input, SqlDbType.VarChar, 30, txtContNo.Text, typeof(string));
                        var[7] = gb.CreateSPParameter("@KodePos", ParameterDirection.Input, SqlDbType.VarChar, 30, txtzipcode.Text, typeof(string));
                        var[8] = gb.CreateSPParameter("@NoFax", ParameterDirection.Input, SqlDbType.VarChar, 15, "-", typeof(string));
                        var[9] = gb.CreateSPParameter("@BatasKredit", ParameterDirection.Input, SqlDbType.Decimal, 30, txtbtskredit.Text, typeof(decimal));
                        var[10] = gb.CreateSPParameter("@TypeRekening", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlTypeRek.SelectedValue, typeof(string));
                        var[11] = gb.CreateSPParameter("@BungaBiasa", ParameterDirection.Input, SqlDbType.Decimal, 30, txtBungaBiasa.Text, typeof(decimal));
                        var[12] = gb.CreateSPParameter("@BungaOvd", ParameterDirection.Input, SqlDbType.Decimal, 30, txtBungaovd.Text, typeof(decimal));
                        var[13] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlCrc.SelectedValue, typeof(string));
                        var[14] = gb.CreateSPParameter("@SaldoAwal", ParameterDirection.Input, SqlDbType.Decimal, 30, txtSaldoAB.Text, typeof(decimal));
                        var[15] = gb.CreateSPParameter("@SaldoAwalTi", ParameterDirection.Input, SqlDbType.Decimal, 30, txtSaldoAT.Text, typeof(decimal));
                        var[16] = gb.CreateSPParameter("@Memo", ParameterDirection.Input, SqlDbType.VarChar, 50, txtMemo.Text, typeof(string));
                        var[17] = gb.CreateSPParameter("@TglKredit", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglkredit.Text, typeof(DateTime));
                        var[18] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        var[19] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
                        var[20] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 20, txtRekBank.Text, typeof(string));
                        var[21] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.VarChar, 20, ckbx, typeof(string));
                        var[22] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                        var[23] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[24] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                        if (ddlBankId.Enabled == true)
                        {
                            ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
                        }
                        else
                        {
                            ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
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
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Kas Bank " + ddlBankId.SelectedValue + " sudah ada, silahkan cek kembali');", true);

                    }
                }
                else
                {
                    STATE = "UPDATE";


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[25];
                        var[0] = gb.CreateSPParameter("@BankID", ParameterDirection.Input, SqlDbType.VarChar, 8, ddlBankId.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@NamaBank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnmBank.Text, typeof(string));
                        var[2] = gb.CreateSPParameter("@NamaPendek", ParameterDirection.Input, SqlDbType.VarChar, 50, txtContPers.Text, typeof(string));
                        var[3] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamat.Text, typeof(string));
                        var[4] = gb.CreateSPParameter("@Alamat2", ParameterDirection.Input, SqlDbType.VarChar, 50, txtCity.Text, typeof(string));
                        var[5] = gb.CreateSPParameter("@Alamat3", ParameterDirection.Input, SqlDbType.VarChar, 50, txtPropinsi.Text, typeof(string));
                        var[6] = gb.CreateSPParameter("@Telepon", ParameterDirection.Input, SqlDbType.VarChar, 30, txtContNo.Text, typeof(string));
                        var[7] = gb.CreateSPParameter("@KodePos", ParameterDirection.Input, SqlDbType.VarChar, 30, txtzipcode.Text, typeof(string));
                        var[8] = gb.CreateSPParameter("@NoFax", ParameterDirection.Input, SqlDbType.VarChar, 15, "-", typeof(string));
                        var[9] = gb.CreateSPParameter("@BatasKredit", ParameterDirection.Input, SqlDbType.Decimal, 30, txtbtskredit.Text, typeof(decimal));
                        var[10] = gb.CreateSPParameter("@TypeRekening", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlTypeRek.SelectedValue, typeof(string));
                        var[11] = gb.CreateSPParameter("@BungaBiasa", ParameterDirection.Input, SqlDbType.Decimal, 30, txtBungaBiasa.Text, typeof(decimal));
                        var[12] = gb.CreateSPParameter("@BungaOvd", ParameterDirection.Input, SqlDbType.Decimal, 30, txtBungaovd.Text, typeof(decimal));
                        var[13] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlCrc.SelectedValue, typeof(string));
                        var[14] = gb.CreateSPParameter("@SaldoAwal", ParameterDirection.Input, SqlDbType.Decimal, 30, txtSaldoAB.Text, typeof(decimal));
                        var[15] = gb.CreateSPParameter("@SaldoAwalTi", ParameterDirection.Input, SqlDbType.Decimal, 30, txtSaldoAT.Text, typeof(decimal));
                        var[16] = gb.CreateSPParameter("@Memo", ParameterDirection.Input, SqlDbType.VarChar, 50, txtMemo.Text, typeof(string));
                        var[17] = gb.CreateSPParameter("@TglKredit", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglkredit.Text, typeof(DateTime));
                        var[18] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                        var[19] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
                        var[20] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 20, txtRekBank.Text, typeof(string));
                        var[21] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.VarChar, 20, ckbx, typeof(string));
                        var[22] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                        var[23] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[24] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                        if (ddlBankId.Enabled == true)
                        {
                            ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
                        }
                        else
                        {
                            ge.ListDataset = gb.GetDataJustOutPut("CL_kasBank", var, "constrmf");
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

               
            }

            catch (Exception X)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }
        }
    }


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

}