using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AE_Master_AeStdGaji : System.Web.UI.Page
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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
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
        ddlunitbisnis.Focus();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

                //DROPDOWNLIST TYPE PENDIDIKAN
                DefaultClass.Global.SqlHelper[] var4xx = new DefaultClass.Global.SqlHelper[2];
                var4xx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DEPT", typeof(string));
                var4xx[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4xx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

                ddlunitbisnis.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlunitbisnis.DataTextField = "TEXT";
                ddlunitbisnis.DataValueField = "VALUE";
                ddlunitbisnis.DataBind();
                ddlunitbisnis.Items.Insert(0, new ListItem("Pilih Data", "-"));

               


              

                // DROPDOWNLIST TYPE GOLONGAN
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[2];
                var5[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETGOLONGAN", typeof(string));
                var5[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var5, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGol asc";

                ddlgolongan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgolongan.DataTextField = "kdket";
                ddlgolongan.DataValueField = "KodeGol";
                ddlgolongan.DataBind();
                ddlgolongan.Items.Insert(0, new ListItem("Pilih Data", "-"));

                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
                Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));


                ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetCabangReport", Paramsx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabangx, "None", "");
                //ddlcabang.Items.Insert(0, new ListItem("-", "-"));

                ddlcabangx.SelectedValue = SessBranchID;

                if (SessBranchID == "000")
                {
                    ddlcabangx.Enabled = true;
                }
                else
                {
                    ddlcabangx.Enabled = false;
                }

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
    protected void netgaji_ontextchanged(object sender, EventArgs e)
    {
        decimal a, b, c;
        //a = decimal.Parse(txtgjmx.Text);
        a = decimal.Parse(txtjlhgjpkk.Text);
        b = decimal.Parse(txttunmakan.Text);
        c = decimal.Parse(txttuntransport.Text);
        txtnetgajipokok.Text = string.Format("{0:N2}", a+b+c);
       
        // z = (b + c + d + f + g + h + i + j + k + x)/173;

        //txtkurslembur.Text = z.ToString();
        //txtkurslembur.Text = string.Format("{0:N2}", z);
    }

    protected void ddlunitbisnis_SelectedIndexChanged(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBAGIAN", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

        ddlbagian.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbagian.DataTextField = "kdket";
        ddlbagian.DataValueField = "KodeBagian";
        ddlbagian.DataBind();
        ddlbagian.Items.Insert(0, new ListItem("Pilih Data", "-"));

        
        //DROPDOWNLIST TYPE JABATAN
        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
        var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbagian.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

        ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljabatan.DataTextField = "kdket";
        ddljabatan.DataValueField = "KodeJab";
        ddljabatan.DataBind();
        ddljabatan.Items.Insert(0, new ListItem("Pilih Data", "-"));

        //DROPDOWNLIST TYPE KANTOR
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKANTOR", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var1[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

        ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "kdnm";
        ddlcabang.DataValueField = "KodeCabang";
        ddlcabang.DataBind();
        ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlcabang.SelectedValue = SessBranchID;
    }

    protected void ddlbagian_SelectedIndexChanged(object sender, EventArgs e)
    {

        

        //DROPDOWNLIST TYPE JABATAN
        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
        var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbagian.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

        ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljabatan.DataTextField = "kdket";
        ddljabatan.DataValueField = "KodeJab";
        ddljabatan.DataBind();
        ddljabatan.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }



    protected void ddlcabangx_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGridEntity();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), System.IO.Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        //DateTime now = DateTime.Now;

        //Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy") +  " s/d " + xdate.ToString("dd-MM-yyyy");
    }

    public void BindGridEntity()
    {
        //ddlcabang.SelectedValue
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGAJISTD", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangx.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_SELECT", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Kodecabang asc";

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

                    txtkdstdgaji.Enabled = false;
                    txtkdstdgaji.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdstd")).Text;
                    ddlunitbisnis.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeDept")).Text;
                    ddlunitbisnis_SelectedIndexChanged(sender, e);
                    ddlcabang.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    ddlbagian.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebagian")).Text;
                    //DROPDOWNLIST TYPE JABATAN
                    DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
                    var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
                    var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
                    var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbagian.SelectedValue, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

                    ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljabatan.DataTextField = "kdket";
                    ddljabatan.DataValueField = "KodeJab";
                    ddljabatan.DataBind();
                    ddljabatan.Items.Insert(0, new ListItem("Pilih Data", "-"));

                    ddljabatan.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeJab")).Text;
                    ddlgolongan.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGol")).Text;
                    //txtketGol.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblketGol")).Text;
                    //ddlkritgp.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkritgp")).Text;
                    txtketerangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblket")).Text;
                    txtjlhgjpkk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljlhgjpkk")).Text;
                    txttunmakan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljlhMakan")).Text;
                    txttuntransport.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljlhtrp")).Text;
                    txtnetgajipokok.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljlhNetGaji")).Text;
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
                    txtkdstdgaji.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdstd")).Text;
                    string kodcab = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkdstdgaji.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, kodcab, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_INSERTUPDATESTDGAJI", var, "constrmf");
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
            if (txtkdstdgaji.Text.Length == 0)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[15];
            var[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkdstdgaji.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@Ket", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@kdDept", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlunitbisnis.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbagian.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@kdGol", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgolongan.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@kdjab", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljabatan.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@gajipokok", ParameterDirection.Input, SqlDbType.Money, 50, txtjlhgjpkk.Text, typeof(decimal));
            var[7] = gb.CreateSPParameter("@tunjmakan", ParameterDirection.Input, SqlDbType.Money, 50, txttunmakan.Text, typeof(decimal));
            var[8] = gb.CreateSPParameter("@tunjtransport", ParameterDirection.Input, SqlDbType.Money, 50, txttuntransport.Text, typeof(decimal));
            var[9] = gb.CreateSPParameter("@netgaji", ParameterDirection.Input, SqlDbType.Money, 50, txtnetgajipokok.Text, typeof(decimal));
            var[10] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
            var[11] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[12] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[13] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_INSERTUPDATESTDGAJI", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
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

    protected void ddlkritGp_SelectedIndexChanged(object sender,EventArgs e)
    {
        if(ddlunitbisnis.SelectedValue == "T")
        {
            txtjlhgjpkk.Enabled = true;
        }
        else
        {
            txtjlhgjpkk.Enabled = false;
            txtjlhgjpkk.Text = "0";
        }
    }
}