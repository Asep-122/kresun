using DefaultClass;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class AR_Master_Update_manager_cab : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DROPDOWNLIST CABANG
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("[AR_UPDATE_CABANG]", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

            ddlCabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlCabang.DataTextField = "TEXT";
            ddlCabang.DataValueField = "VALUE";
            ddlCabang.DataBind();
            ddlCabang.Items.Insert(0, new ListItem("Pilih Data", "-"));


            ddlCabang.Enabled = true;

      
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNama.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama tidak boleh kosong');", true);
                return;
            }
            else if (txtHP.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nama tidak boleh kosong');", true);
                return;
            }

            else
            {


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATE", typeof(string));
                var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlCabang.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@ManagerCabang", ParameterDirection.Input, SqlDbType.VarChar, 80, txtNama.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@NoHP", ParameterDirection.Input, SqlDbType.VarChar, 50, txtHP.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("[AR_UPDATE_CABANG]", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Udpdate Data HP dan Nama Manager Sukses');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    protected void btnPencairan_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtRekening.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Rekening tidak boleh kosong');", true);
                return;
            }
            

            else
            {


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEREK", typeof(string));
                var[1] = gb.CreateSPParameter("@Norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, TxtRekening.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("[AR_UPDATE_CABANG]", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Udpdate Rekening Pencairan Sukses');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    // gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            // gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void ddlCabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList comboBox = (DropDownList)sender;

        var pilihan = comboBox.SelectedValue;

        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
        var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYKODE", typeof(string));
        var3[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, pilihan , typeof(string));
        var3[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[AR_UPDATE_CABANG]", var3, "constrmf");

        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
           
            txtNama.Text = ge.ListDataset.Tables[0].Rows[0]["kepala_cabang"].ToString();
            txtHP.Text = ge.ListDataset.Tables[0].Rows[0]["datatransfer"].ToString();
        }

    }
}