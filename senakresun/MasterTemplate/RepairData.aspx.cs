using DefaultClass;
using DocumentFormat.OpenXml.Spreadsheet;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AE_Transaksi_RepairData : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessKdpersh
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
    GlobalEntities gx = new GlobalEntities();
    GlobalEntities gx1 = new GlobalEntities();
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime now = DateTime.Now;
           
        Pnlfooter.Visible = true;
        PnlFrontx.Visible = false;
       
            PnlChangePassword.Visible = false;
            PnlPengajuan.Visible = true;
          
      
            pnlHeader.Visible = false;
            //ddlstdgaji_view_SelectedIndexChanged(sender, e);
            //ddlstd_gaji_SelectedIndexChanged(sender, e);

            ddltypememo.Enabled = true;
           
            btnSimpan.Visible = true;
           
            txtnobukti.Text = "";
            txtkodeuser.Text = "";

     
   
        
       
       
            txttglpengajuan.Text = now.ToString("dd-MM-yyyy");
          
        
       
          
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {

        //txttglout.Text = txttgljoin.Text;

    }
   
  
    protected void btnbackheader_Click(object sender, EventArgs e)
    {

        //pnladd.Visible = true;
        //pnl1.Visible = true; 
        Pnlfooter.Visible = false;

        PnlFrontx.Visible = true;
        
    }
    protected void btnback1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
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
            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                DateTime now = DateTime.Now;
                BindGridEntity();
                BindGridEntitydata();
              
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
        try
        {

        
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglRepair desc";


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
    public void BindGridEntitydata()
    {



        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

        ddlkodecabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkodecabang.DataTextField = "TEXT";
        ddlkodecabang.DataValueField = "VALUE";
        ddlkodecabang.DataBind();
        ddlkodecabang.Items.Insert(0, new ListItem("Pilih Cabang", "-"));

        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "USER", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";
        ddluser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddluser.DataTextField = "TEXT";
        ddluser.DataValueField = "VALUE";
        ddluser.DataBind();
        ddluser.Items.Insert(0, new ListItem("Pilih User Repair", "-"));

        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[2];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "JENIS", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";
        ddltypememo.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddltypememo.DataTextField = "TEXT";
        ddltypememo.DataValueField = "VALUE";
        ddltypememo.DataBind();
        ddltypememo.Items.Insert(0, new ListItem("Pilih Jenis Repair", "-"));

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

    protected void ResetPassword(object sender, EventArgs e)
    {
       
        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RESETPASSWORD", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodeuser.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue , typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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

    protected void ClearTeller(object sender, EventArgs e)
    {

        try
        {

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CLEARTELLER", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, Txtrekeningteller.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "1", typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
            {

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[6];
                var2[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var2[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
                var2[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CLEARTELLER", typeof(string));
                var2[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, Txtrekeningteller.Text, typeof(string));
                var2[5] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "2", typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
                ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var2, "constrmf");
                if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
                {



                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[12];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CLEARTELLER", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, Txtrekeningteller.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
                var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
                var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "3", typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cek Rekening di permohonan dan posisi harus di teller!');", true);



                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Rekening tidak ditemukan di master kredit!');", true);



            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

 
    
    protected void REQUESTDATA(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REQUESTDATA", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, "RQSDATA", typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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

    protected void EXECUTEQUERY(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "EXECUTEQUERY", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, "EXTQUERY", typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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
    protected void CHECKAGENT(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKAGENT", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, "CHKAGENT", typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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
    protected void TANYAJAWAB(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TANYAJAWAB", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, "TYJWB", typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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
    protected void DELETEJOURNAL(object sender, EventArgs e)
    {

        try
        {

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEJOURNAL", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobuktiJournal.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "1", typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");
            int xsf =int.Parse(ge.ListDataset.Tables[0].Rows[0]["banyak"].ToString());
            if (xsf > 0)
            {



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[12];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEJOURNAL", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobuktiJournal.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
                var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
                var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "2", typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }



            }
               

          
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No bukti tidak ditemukan di Transaksi Journal!');", true);



            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }
  
    
    protected void RESETBUNGAJANGKA(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[12];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RESETBUNGAJANGKA", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrekeningBerjangka.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "2", typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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

    protected void RUNNINGBUNGAJANGKA(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[12];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RUNNINGBUNGAJANGKA", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrekeningberjangkarunning.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "1", typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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

    protected void CADANGANREK(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[14];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CADANGANREK", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, TxtRekeningCadangan.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "2", typeof(string));
            var[12] = gb.CreateSPParameter("@nilai1", ParameterDirection.Input, SqlDbType.Decimal, 0, txtcadangan.Text, typeof(decimal));
            var[13] = gb.CreateSPParameter("@nilai2", ParameterDirection.Input, SqlDbType.Decimal, 0, txtmutasicadangan.Text, typeof(decimal));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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
    protected void SALDOTP(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[14];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SALDOTP", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtanggotatp.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "2", typeof(string));
            var[12] = gb.CreateSPParameter("@nilai1", ParameterDirection.Input, SqlDbType.Decimal, 0, txtsaldoakhir.Text, typeof(decimal));
            var[13] = gb.CreateSPParameter("@nilai2", ParameterDirection.Input, SqlDbType.Decimal, 0, "0", typeof(decimal));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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

    protected void REGISTERDATE(object sender, EventArgs e)
    {

        try
        {


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[15];
            var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REGISTERDATE", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggotard.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
            var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
            var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "2", typeof(string));
            var[12] = gb.CreateSPParameter("@nilai1", ParameterDirection.Input, SqlDbType.Decimal, 0, "0", typeof(decimal));
            var[13] = gb.CreateSPParameter("@nilai2", ParameterDirection.Input, SqlDbType.Decimal, 0, "0", typeof(decimal));
            var[14] = gb.CreateSPParameter("@tgl1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglmasukanggota.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
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


    protected void BUKTIKODEUSER(object sender, EventArgs e)
    {

        try
        {

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BUKTIKODEUSER", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobuktikodeuser.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "1", typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");
            int xsf = int.Parse(ge.ListDataset.Tables[0].Rows[0]["banyak"].ToString());
            if (xsf > 0)
            {



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[13];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "BUKTIKODEUSER", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobuktikodeuser.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@koderepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltypememo.SelectedValue, typeof(string));
                var[6] = gb.CreateSPParameter("@UserRepair", ParameterDirection.Input, SqlDbType.VarChar, 10, ddluser.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@Kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlpermintaanUser.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@memorepair", ParameterDirection.Input, SqlDbType.VarChar, 300, Txtcatatan.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@TglRepair", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglpengajuan.Text, typeof(DateTime));
                var[10] = gb.CreateSPParameter("@namacabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkodecabang.SelectedItem.Text, typeof(string));
                var[11] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "2", typeof(string));
                var[12] = gb.CreateSPParameter("@kode2", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlkodeuserjournal.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }



            }



            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No bukti tidak ditemukan di Transaksi Journal!');", true);



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
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

            if (ddltypememo.SelectedValue == "A01")
            {



                if (txtkodeuser.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Login Masih Kosong!');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
        
                else
                {
                    ResetPassword(sender, e);

                }

            }

            else if (ddltypememo.SelectedValue == "A02")
            {
                if (Txtrekeningteller.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Rekening Masih Kosong!');", true);
                }                
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    ClearTeller(sender, e);

                }
            }
            else if (ddltypememo.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anda Belum Memilih Jenis Repair!');", true);
            }
            else if (ddltypememo.SelectedValue == "A03")
            { 
            
            
            
            }
            else if (ddltypememo.SelectedValue == "A04")
            {

                if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    REQUESTDATA(sender, e);

                }

            }
            else if (ddltypememo.SelectedValue == "A05")
            {

                if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    EXECUTEQUERY(sender, e);

                }

            }
            else if (ddltypememo.SelectedValue == "A06")
            {
                if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    CHECKAGENT(sender, e);

                }


            }
            else if (ddltypememo.SelectedValue == "A07")
            {
                if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    CHECKAGENT(sender, e);

                }


            }
            else if (ddltypememo.SelectedValue == "A08")
            {
                if (txtnobuktiJournal.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nobukti Masih Kosong!');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    DELETEJOURNAL(sender, e);

                }


            }
            else if (ddltypememo.SelectedValue == "A09")
            {
                if (ddlrekeningBerjangka.SelectedValue== "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Rekening Belum dipilih!');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    RESETBUNGAJANGKA(sender, e);

                }


            }
            else if (ddltypememo.SelectedValue == "A10")
            {

                if (ddlrekeningberjangkarunning.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Rekening Belum dipilih!');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    RUNNINGBUNGAJANGKA(sender, e);

                }

            }

            else if (ddltypememo.SelectedValue == "A11")
            {

                if (TxtRekeningCadangan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Rekening belum dimasukkan');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    CADANGANREK(sender, e);

                }

            }

            else if (ddltypememo.SelectedValue == "A12")
            {

                if (txtanggotatp.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota belum dimasukkan');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    SALDOTP(sender, e);

                }

            }

            else if (ddltypememo.SelectedValue == "A13")
            {

                if (txtnoanggotard.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota belum dimasukkan');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    REGISTERDATE(sender, e);

                }

            }

            else if (ddltypememo.SelectedValue == "A14")
            {

                if (txtnobuktikodeuser.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Bukti Journal belum dimasukkan');", true);
                }
                else if (Txtcatatan.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Masih Kosong!');", true);
                }
                else if (ddluser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('User Yang Mengerjakan Masih Kosong!');", true);
                }
                else if (ddlpermintaanUser.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Permintaan User Masih Kosong!');", true);
                }
                else if (ddlkodecabang.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Masih Kosong!');", true);
                }
                else
                {
                    BUKTIKODEUSER(sender, e);

                }

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

    protected void SaldoCadangan_ontextchanged(object sender, EventArgs e)
    {
        decimal a, b, c;
        a = decimal.Parse(txtcadangan.Text);
        b = decimal.Parse(txtmutasicadangan.Text);
        c = a-b;
        txtsaldocadangan.Text = string.Format("{0:N2}", c);

    }
    protected void btnSearch_Cadangan_Click(object sender, EventArgs e)
    {
        try
        {
            //var search = "";
            DateTime now;
            string gajistd = "";
            string gol = "";
            string bag = "";
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CADANGANREK", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, TxtRekeningCadangan.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "1", typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {


                //data karyawan


                txtnamaanggota.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();
                
                txtsaldocadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldocadangan"].ToString()));
                txtcadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["by_cadangan"].ToString()));
                txtmutasicadangan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["mutasi_cadangan"].ToString()));
             

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Rekening tidak ditemukan!');", true);
                txtnamaanggota.Text = "";
                TxtRekeningCadangan.Text = "";

                txtsaldocadangan.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtcadangan.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtmutasicadangan.Text = string.Format("{0:N2}", decimal.Parse("0"));

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }


    protected void btnSearch_saldo_TP_Click(object sender, EventArgs e)
    {
        try
        {
            //var search = "";
            DateTime now;
            string gajistd = "";
            string gol = "";
            string bag = "";
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SALDOTP", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtanggotatp.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "1", typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {


                //data karyawan
                int tutup = int.Parse(ge.ListDataset.Tables[0].Rows[0]["flag_tutup"].ToString());
                int salah = int.Parse(ge.ListDataset.Tables[0].Rows[0]["flagsalah"].ToString());
                

                

                txtnamatp.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

                txtsaldoakhir.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhircbs"].ToString()));
               
                if (salah == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya Anggota Tarik TP yg bisa diupdate Saldo Akhir!');", true);
                    txtnamatp.Text = "";
                    txtanggotatp.Text = "";

                    txtsaldoakhir.Text = string.Format("{0:N2}", decimal.Parse("0"));

                }
                else if (tutup == 1)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Hanya Anggota Tarik TP yg bisa diupdate Saldo Akhir!');", true);
                    txtnamatp.Text = "";
                    txtanggotatp.Text = "";

                    txtsaldoakhir.Text = string.Format("{0:N2}", decimal.Parse("0"));
                }


            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota tidak ditemukan!');", true);
                txtnamatp.Text = "";
                txtanggotatp.Text = "";

                txtsaldoakhir.Text = string.Format("{0:N2}", decimal.Parse("0"));
                

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }


    protected void btnSearch_Rd_Anggota_Click(object sender, EventArgs e)
    {
        try
        {
            //var search = "";
            DateTime now;
            string gajistd = "";
            string gol = "";
            string bag = "";
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REGISTERDATE", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@kode", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoanggotard.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 2, "1", typeof(string));
            //ge.ListDataset = gb.GetDataJustOutPut("GetMasterKredit", var4, "constrmf").Tables[0];
            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");

            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {




                txtnamard.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();


                txttglmasukanggota.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
              


            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('No Anggota tidak ditemukan!');", true);
                txtnamard.Text = "";
                


            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    
    protected void ddlType_Memo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypememo.SelectedValue == "A01")
        {
            PnlChangePassword.Visible = true;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;

        }
        else if (ddltypememo.SelectedValue == "A02")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = true;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A03")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A04")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A05")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A06")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A07")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A08")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = true;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A09")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = true;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
            //DROPDOWNLIST TYPE PENDIDIKAN
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RESETBUNGAJANGKA", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 10, "1", typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";
            ddlrekeningBerjangka.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlrekeningBerjangka.DataTextField = "TEXT";
            ddlrekeningBerjangka.DataValueField = "VALUE";
            ddlrekeningBerjangka.DataBind();
            ddlrekeningBerjangka.Items.Insert(0, new ListItem("Pilih Jenis Rekening", "-"));


        }

        else if (ddltypememo.SelectedValue == "A10")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;

            PnlRunningBungaJangka.Visible = true;

            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
            //DROPDOWNLIST TYPE PENDIDIKAN
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlkodecabang.SelectedValue, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "RESETBUNGAJANGKA", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[4] = gb.CreateSPParameter("@urutx", ParameterDirection.Input, SqlDbType.VarChar, 10, "1", typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("DEV_REPAIR", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";
            ddlrekeningberjangkarunning.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlrekeningberjangkarunning.DataTextField = "TEXT";
            ddlrekeningberjangkarunning.DataValueField = "VALUE";
            ddlrekeningberjangkarunning.DataBind();
            ddlrekeningberjangkarunning.Items.Insert(0, new ListItem("Pilih Jenis Rekening", "-"));
        }

        else if (ddltypememo.SelectedValue == "A11")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = true;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A12")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = true;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A13")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = true;
            PnlnobuktikodeUser.Visible = false;
        }
        else if (ddltypememo.SelectedValue == "A14")
        {
            PnlChangePassword.Visible = false;
            PnlClearTeller.Visible = false;
            PnlDeleteJournal.Visible = false;
            PnlResetBungaJangka.Visible = false;
            PnlRunningBungaJangka.Visible = false;
            PnlUpdateCadangan.Visible = false;
            PnlUpdateSaldoTP.Visible = false;
            PnlRegisterDate.Visible = false;
            PnlnobuktikodeUser.Visible = true;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

 


    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GET_NIK_KARYAWAN", Params, "constrmf");
        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }

}




