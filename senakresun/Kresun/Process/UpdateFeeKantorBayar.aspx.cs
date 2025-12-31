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


public partial class SD_UPDATE_PROVISI : System.Web.UI.Page
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void txtpsn_TextChanged(object sender, EventArgs e) 
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(txtpsn.Text, "  ^ [0-9]"))
        {
            txtpsn.Text = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
            if (!Page.IsPostBack)
            {
                listdropdown();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void listdropdown()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKANTOR", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_FEEKANTORBAYAR", var1, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KantorBayar ASC";

            ddlKantorBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlKantorBayar.DataTextField = "Kantor";
            ddlKantorBayar.DataValueField = "KantorBayar";
            ddlKantorBayar.DataBind();
            ddlKantorBayar.Items.Insert(0, new ListItem("Pilih Sumber Dana", ""));
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    //protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    switch (e.CommandName)
    //    {

    //        case "SET":

    //            try

    //            {

    //                PnlFront.Visible = false;
    //                pnladd.Visible = true;
    //                pnlHeader.Visible = false;

    //                txtkodekantor.Enabled = false;
    //                txtkodekantor.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebayar")).Text;
    //                txtnamabayar.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamabayar")).Text;
    //                txtnorekening.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorekening")).Text;
    //                txtatasnama.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblatasnama")).Text;
    //                ddlnoaccount.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoaccount")).Text;

    //            }

    //            catch (Exception X)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //            }

    //            break;
    //        case "SETDEL":

    //            try
    //            {
    //                txtkodekantor.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebayar")).Text;

    //                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
    //                var1[0] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodekantor.Text, typeof(string));
    //                var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //                var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKANTORBAYAR", typeof(string));
    //                var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //                ge.ListDataTable = gb.GetDataJustOutPut("SD_PROVISI", var1, "constrmf").Tables[0];
    //                if (ge.ListDataTable.Rows.Count != 0)
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Data ini Tidak Bisa Di Hapus, Silahkan Hubungi Admin.!!');", true);
    //                }
    //                else
    //                {
    //                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
    //                    var[0] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodekantor.Text, typeof(string));
    //                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
    //                    var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //                    ge.ListDataset = gb.GetDataJustOutPut("SD_PROVISI", var, "constrmf");
    //                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
    //                    {

    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);

    //                    }

    //                    else
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
    //                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //                    }
    //                }
    //            }

    //            catch (Exception X)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

    //            }

    //            break;

    //    }
    //}
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("UpdateFeeKantorBayar.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateFeeKantorBayar.aspx");
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSearch_OnChange(object sender, EventArgs e)
    {
        try
        {
            if(ddlKantorBayar.SelectedValue == "")
            {
                txtpsn.Text = "";
                txttgl.Text = "";
                txtupdate.Text = "";
            }
            //else
            //{
            //    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            //    var1[0] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlsumberdana.SelectedValue, typeof(string));
            //    var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //    var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
            //    var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //    ge.ListDataset = gb.GetDataJustOutPut("SD_PROVISI", var1, "constrmf");
            //    if (ge.ListDataset.Tables[0].Rows.Count < 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Data tidak ada, harap hubungi Admin');", true);
            //    }

            //    else
            //    {
            //        txtpsn.Text = ge.ListDataset.Tables[0].Rows[0]["PsnProvisi"].ToString();
            //        txttgl.Text = ge.ListDataset.Tables[0].Rows[0]["DateModified"].ToString() == "" ? 
            //                (Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["DateAdded"].ToString())).ToString("dd MMMM yyyy") 
            //                : ge.ListDataset.Tables[0].Rows[0]["DateModified"].ToString();
            //        txtupdate.Text = ge.ListDataset.Tables[0].Rows[0]["LastUserFK"].ToString() == ""?
            //                ge.ListDataset.Tables[0].Rows[0]["AddedByFK"].ToString()  : ge.ListDataset.Tables[0].Rows[0]["LastUserFK"].ToString();
            //    }
            //}
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlKantorBayar.SelectedValue == "")
            {
                txtpsn.Text = "";
                txttgl.Text = "";
                txtupdate.Text = "";
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                var1[0] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlKantorBayar.SelectedValue, typeof(string));
                var1[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
                var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("KP_FEEKANTORBAYAR", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:PeringatanKode('Data tidak ada, harap hubungi Admin');", true);
                }

                else
                {
                    string asd = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["DateAdded"].ToString()).ToString("yyyy-MM-dd");
                    txtpsn.Text = ge.ListDataset.Tables[0].Rows[0]["PsnKomisi"].ToString();
                    txttgl.Text = ge.ListDataset.Tables[0].Rows[0]["DateModified"].ToString() == "" ?
                            Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["DateAdded"].ToString()).ToString("dd MMMM yyyy")
                            : Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["DateModified"].ToString()).ToString("dd MMMM yyyy");
                    txtupdate.Text = ge.ListDataset.Tables[0].Rows[0]["AddedByFK"].ToString(); //== "" ?
                           // ge.ListDataset.Tables[0].Rows[0]["AddedByFK"].ToString() : ge.ListDataset.Tables[0].Rows[0]["LastUserFK"].ToString();
                }
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
            var[0] = gb.CreateSPParameter("@KantorBayar", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKantorBayar.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@PsnKomisi", ParameterDirection.Input, SqlDbType.Money, 0, txtpsn.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[3] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, txtupdate.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@DateModified", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
            var[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
            var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("KP_FEEKANTORBAYAR", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ddlKantorBayar.SelectedValue = "";
                txtpsn.Text = "";
                txttgl.Text = "";
                txtupdate.Text = "";
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

}