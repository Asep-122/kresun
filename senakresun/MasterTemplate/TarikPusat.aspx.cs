using DefaultClass;
using EntitiesClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CF_TarikPusat : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new DefaultClass.Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gb2 = new DefaultClass.Global();
    GlobalEntities ge2 = new GlobalEntities();
    Global gb3 = new DefaultClass.Global();
    GlobalEntities ge3 = new GlobalEntities();
    Global gb4 = new DefaultClass.Global();
    GlobalEntities ge4 = new GlobalEntities();
    Global gb5 = new DefaultClass.Global();
    GlobalEntities ge5 = new GlobalEntities();
    Global gb6 = new DefaultClass.Global();
    GlobalEntities ge6 = new GlobalEntities();

    string tanggal = "";
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
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
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
            if (!Page.IsPostBack)
            {
                DateTime now = DateTime.Now;
                //txttglAkhirini.Text = now.ToString("yyyy-MM-dd");
                string ATahun = now.ToString("yyyy");
                string ABulan = now.ToString("MM");
                int rubahtahun = Int32.Parse(ATahun);
                int rubahbulan = Int32.Parse(ABulan);
                DateTime.DaysInMonth(rubahtahun, rubahbulan);
                String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
                string rubahtanggal = rubahhari + "/" + ABulan + "/" + ATahun;
                //txttglAkhirini.Text = rubahtanggal;
                int year = DateTime.Now.Year;
                for (int i = year - 1; i <= year + 5; i++)
                {
                    ListItem li = new ListItem(i.ToString());
                    ddltahun.Items.Add(li);
                }
                ddltahun.Items.FindByText(year.ToString()).Selected = true;
                ddltahun.Enabled = false;
                ddlbulan.SelectedValue = ABulan;
                ddlbulan.Enabled = false;
                ddlcabang_SelectedChange(sender, e);
                ddlcabangx_SelectedChange(sender, e);

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlcabang_SelectedChange(object sender, EventArgs e)
    {
        try
        {
           
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 15, SessBranchID, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYCABANG", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var, "constrmf");


                ddlbulan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["FiskalBi"].ToString();
                ddltahun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["FiskalTi"].ToString();
        

            
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void ddlcabangx_SelectedChange(object sender, EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlbulan.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 15, ddltahun.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TARIKPUSAT", typeof(string));
            var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
            {
                txttgltarikpusat.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgltarikPusat"].ToString()));
                txttglAwalini.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglPelunasanAwal"].ToString()));
                txttglAkhirini.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglPelunasanAkhir"].ToString()));
                txttglAwaldepan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglPelunasanBerikutAwal"].ToString()));
                txttglAkhirdepan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglPelunasanBerikutAkhir"].ToString()));
                btnSimpan.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Tarik Pusat tidak dilakukan di bulan fiskal sekarang');", true);
                btnSimpan.Visible = false;
            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
 protected void btntambah_click(object sender, EventArgs e)
    {
        
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
           
                
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[8];

                    var1[0] = gb.CreateSPParameter("@xtglTARIK", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltarikpusat.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@tglpelunasantarikawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                    var1[2] = gb.CreateSPParameter("@tglpelunasantarikakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                    var1[3] = gb.CreateSPParameter("@tglpelunasantarikberikutawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwaldepan.Text, typeof(string));
                    var1[4] = gb.CreateSPParameter("@tglpelunasantarikberikutakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirdepan.Text, typeof(string));
                    var1[5] = gb.CreateSPParameter("@trcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[6] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50,SessLoginID, typeof(string));
                    var1[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                   
                    ge.ListDataset = gb.GetDataJustOutPut("AGENT_TARIKTP_3BULAN_002_Branch_X", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                      
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Tarik Pusat Done');", true);

                            btnSimpan.Text = "TARIK PUSAT BULAN INI";
                            ddlbulan.SelectedValue = "1";
                            ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                            txttglAwalini.Text = "";
                            txttglAkhirini.Text = "";
                            txttglAwaldepan.Text = "";
                            txttglAkhirdepan.Text = "";
                       



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
    //unclosing
    protected void btnSimpan2_Click(object sender, EventArgs e)
    {
        try
        {
            
 
                ddlbulan.SelectedValue = "1";
                ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                txttglAwalini.Text = "";
                txttglAkhirini.Text = "";
                txttglAwaldepan.Text = "";
                txttglAkhirdepan.Text = "";

          
            
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

}