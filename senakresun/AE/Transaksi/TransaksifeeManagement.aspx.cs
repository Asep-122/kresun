using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class AE_Transaksi_TransaksifeeManagement : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
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
                ambilTahun();
                ambilBulan();
                BindGridEntity();
            }
            //GRData.UseAccessibleHeader = true;

            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 3; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
        ddlTahun.SelectedValue = currentYear.ToString();
        ddlTahun.Enabled = false;
        //if (SessBranchID != "000")
        //{
        //    ddlTahun.Enabled = false;
        //}
        //else
        //{
        //    ddlTahun.Enabled = true;
        //}
    }

    public void ambilBulan()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length; i++)
        {
            ddlbulan.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
        }
        var currentmonth = DateTime.Today.Month;
        ddlbulan.SelectedValue = currentmonth.ToString();
        ddlbulan.Enabled = false;
        //if (SessBranchID != "000")
        //{
        //    ddlbulan.Enabled = false;
        //}
        //else
        //{
        //    ddlbulan.Enabled = true;
        //}
    }

    public void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {


          
            
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlbulan.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPERSENMGM", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlTahun.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@feeBM", ParameterDirection.Input, SqlDbType.Money, 0, txtpersenfee.Text, typeof(decimal));
            ge.ListDataset = gb.GetDataJustOutPut("Ae_Persen_Management", var, "constrmf");



            BindGridEntity();


        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
             pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;

       
    }
   public void BindGridEntity()
    {
        try
        {
            Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangx.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("Ae_Persen_Management", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kodenik asc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
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

                    txtkodekaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodenik")).Text;
                    
                    txtnamakaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNama")).Text;
                    txtpersen.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblPersen")).Text;

                   
                    btnSimpan.Text = "Update";
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
                    string kodenik = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodenik")).Text;
                   
                    Global.SqlHelper[] var = new Global.SqlHelper[7];
                    var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEMGMPRS", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 30, kodenik, typeof(string));
                    var[5] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
                    var[6] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("Ae_Persen_Management", var, "constrmf");
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
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (btnSimpan.Text == "Save")
            {
                STATE = "INSERTMGM";
            }
            else
            {
                STATE = "UPDATEMGM";
            }

           
                Global.SqlHelper[] var = new Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));
                var[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@feeBM", ParameterDirection.Input, SqlDbType.Money, 0, txtpersen.Text, typeof(decimal));
                var[3] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEMGMPRS", typeof(string));
                var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[6] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbulan.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlTahun.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("Ae_Persen_Management", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Enabled = false;
                    btnSimpan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','1');", true);
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
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
}