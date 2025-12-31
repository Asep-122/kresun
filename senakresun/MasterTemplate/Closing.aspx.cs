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

public partial class CF_CLosing : System.Web.UI.Page
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

                //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                //var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                //var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
                //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";
                //ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlcabang.DataTextField = "Nama";
                //ddlcabang.DataValueField = "KodeCabang";
                //ddlcabang.DataBind();
                //ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang", var, "constrmf");
                //ge.ListDataTable = gb.GetDataJustOutPut("GG_GetCabang", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();
                //ddlcabang_SelectedChange(ddlcabang.SelectedValue, e);
                if (ge.ListDataset.Tables[0].Rows.Count <= 1)
                {
                    ddlcabang_SelectedChange(ddlcabang.SelectedValue, e);
                    ddlbulan_SelectedChange(ddlcabang.SelectedValue, e);
                }
                else
                {
                    ddlbulan_SelectedChange(ddlcabang.SelectedValue, e);
                }
                //else if (ge.ListDataset.Tables[0].Rows.Count > 1)
                //{
                //    ddlbulan_SelectedChange(ddlcabang.SelectedValue, e);
                //}
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
            if (ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih Dahulu');", true);
                ddlcabang.Focus();
                ddlbulan.SelectedValue = "1";
                ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                txttglAwalini.Text = "";
                txttglAkhirini.Text = "";
                txttglAwaldepan.Text = "";
                txttglAkhirdepan.Text = "";
                txttglAwallalu.Text = "";
                txttglAkhirlalu.Text = "";
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlcabang.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYCABANG", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var, "constrmf");
                ddlbulan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["FiskalBi"].ToString();
                ddltahun.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["FiskalTi"].ToString();

                int rubahtahun = Int32.Parse(ddltahun.SelectedValue);
                int rubahbulan = Int32.Parse(ddlbulan.SelectedValue);
                DateTime.DaysInMonth(rubahtahun, rubahbulan);
                String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
                string rubahtanggal = rubahhari + "/" + ddlbulan.SelectedValue + "/" + ddltahun.SelectedValue;
                string rubahMM = Convert.ToDateTime(rubahtanggal).ToString("MM");
                txttglAwalini.Text = "01" + "/" + rubahMM + "/" + ddltahun.SelectedValue;
                txttglAkhirini.Text = rubahhari + "/" + rubahMM + "/" + ddltahun.SelectedValue;

                if (ddlbulan.SelectedValue == "12")
                {
                    int rubahtahun1 = Int32.Parse(ddltahun.SelectedValue) + 1;
                    int rubahbulan1 = 01;
                    String rubahhari1 = DateTime.DaysInMonth(rubahtahun1, rubahbulan1).ToString();
                    string rubahtanggal1 = rubahhari1 + "/" + rubahbulan1 + "/" + rubahtahun1;
                    string rubahMM1 = Convert.ToDateTime(rubahtanggal1).ToString("MM");
                    txttglAwaldepan.Text = "01" + "/" + "01" + "/" + rubahtahun1;
                    txttglAkhirdepan.Text = rubahhari1 + "/" + rubahMM1 + "/" + rubahtahun1;

                    int rubahtahun2 = Int32.Parse(ddltahun.SelectedValue);
                    int rubahbulan2 = Int32.Parse(ddlbulan.SelectedValue) - 1;
                    String rubahhari2 = DateTime.DaysInMonth(rubahtahun2, rubahbulan2).ToString();
                    string rubahtanggal2 = rubahhari2 + "/" + rubahbulan2 + "/" + rubahtahun2;
                    string rubahMM2 = Convert.ToDateTime(rubahtanggal2).ToString("MM");
                    txttglAwallalu.Text = "01" + "/" + rubahMM2 + "/" + rubahtahun2;
                    txttglAkhirlalu.Text = rubahhari2 + "/" + rubahMM2 + "/" + rubahtahun2;
                }
                else if(int.Parse(ddlbulan.SelectedValue) < 12)
                {
                    int rubahtahun1 = Int32.Parse(ddltahun.SelectedValue);
                    int rubahbulan1 = Int32.Parse(ddlbulan.SelectedValue) + 1;
                    String rubahhari1 = DateTime.DaysInMonth(rubahtahun1, rubahbulan1).ToString();
                    string rubahtanggal1 = rubahhari1 + "/" + rubahbulan1 + "/" + rubahtahun1;
                    string rubahMM1 = Convert.ToDateTime(rubahtanggal1).ToString("MM");
                    txttglAwaldepan.Text = "01" + "/" + rubahMM1 + "/" + rubahtahun1;
                    txttglAkhirdepan.Text = rubahhari1 + "/" + rubahMM1 + "/" + rubahtahun1;
                    if(ddlbulan.SelectedValue == "1")
                    {
                        int rubahtahun2 = Int32.Parse(ddltahun.SelectedValue) - 1;
                        int rubahbulan2 = 12;
                        String rubahhari2 = DateTime.DaysInMonth(rubahtahun2, rubahbulan2).ToString();
                        string rubahtanggal2 = rubahhari2 + "/" + rubahbulan2 + "/" + rubahtahun2;
                        string rubahMM2 = Convert.ToDateTime(rubahtanggal2).ToString("MM");
                        txttglAwallalu.Text = "01" + "/" + rubahMM2 + "/" + rubahtahun2;
                        txttglAkhirlalu.Text = rubahhari2 + "/" + rubahMM2 + "/" + rubahtahun2;
                    }
                    else
                    {
                        int rubahtahun2 = Int32.Parse(ddltahun.SelectedValue);
                        int rubahbulan2 = Int32.Parse(ddlbulan.SelectedValue) - 1;
                        String rubahhari2 = DateTime.DaysInMonth(rubahtahun2, rubahbulan2).ToString();
                        string rubahtanggal2 = rubahhari2 + "/" + rubahbulan2 + "/" + rubahtahun2;
                        string rubahMM2 = Convert.ToDateTime(rubahtanggal2).ToString("MM");
                        txttglAwallalu.Text = "01" + "/" + rubahMM2 + "/" + rubahtahun2;
                        txttglAkhirlalu.Text = rubahhari2 + "/" + rubahMM2 + "/" + rubahtahun2;
                    }
                }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlbulan_SelectedChange(object sender, EventArgs e)
    {
        try
        {
            if(ddlcabang.SelectedValue == "-" )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih Dahulu');", true);
                ddlcabang.Focus();
            }
            else
            {
                int rubahtahun = Int32.Parse(ddltahun.SelectedValue);
                int rubahbulan = Int32.Parse(ddlbulan.SelectedValue);
                DateTime.DaysInMonth(rubahtahun, rubahbulan);
                String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
                string rubahtanggal = rubahhari + "/" + ddlbulan.SelectedValue + "/" + ddltahun.SelectedValue;
                txttglAwalini.Text = "01" + "/" + ddlbulan.SelectedValue + "/" + ddltahun.SelectedValue;
                txttglAkhirini.Text = rubahtanggal;

                if (ddlbulan.SelectedValue == "12")
                {
                    int rubahtahun1 = Int32.Parse(ddltahun.SelectedValue) + 1;
                    int rubahbulan1 = 01;
                    String rubahhari1 = DateTime.DaysInMonth(rubahtahun1, rubahbulan1).ToString();
                    string rubahtanggal1 = rubahhari1 + "/" + rubahbulan1  + "/" + rubahtahun1;
                    string rubahMM = Convert.ToDateTime(rubahtanggal1).ToString("MM");
                    txttglAwaldepan.Text = "01" + "/" + "01" + "/" + rubahtahun1;
                    txttglAkhirdepan.Text = rubahhari1 + "/" + rubahMM + "/" + rubahtahun1;

                    int rubahtahun2 = Int32.Parse(ddltahun.SelectedValue);
                    int rubahbulan2 = Int32.Parse(ddlbulan.SelectedValue) - 1;
                    String rubahhari2 = DateTime.DaysInMonth(rubahtahun2, rubahbulan2).ToString();
                    string rubahtanggal2 = rubahhari2 + "/" + rubahbulan2 + "/" + rubahtahun2;
                    string rubahMM1 = Convert.ToDateTime(rubahtanggal2).ToString("MM");
                    txttglAwallalu.Text = "01" + "/" + rubahMM1 + "/" + rubahtahun2;
                    txttglAkhirlalu.Text = rubahhari2 + "/" + rubahMM1 + "/" + rubahtahun2;
                }
                else
                {
                    int rubahtahun1 = Int32.Parse(ddltahun.SelectedValue);
                    int rubahbulan1 = Int32.Parse(ddlbulan.SelectedValue) + 1;
                    String rubahhari1 = DateTime.DaysInMonth(rubahtahun1, rubahbulan1).ToString();
                    string rubahtanggal1 = rubahhari1 + "/" + rubahbulan1 + "/" + rubahtahun1;
                    string rubahMM = Convert.ToDateTime(rubahtanggal1).ToString("MM");
                    txttglAwaldepan.Text = "01" + "/" + rubahMM + "/" + rubahtahun1;
                    txttglAkhirdepan.Text = rubahhari1 + "/" + rubahMM + "/" + rubahtahun1;


                    int rubahtahun2;
                    int rubahbulan2;
                    if (ddlbulan.SelectedValue == "1")
                    {
                        rubahbulan2 = 12;

                        rubahtahun2 = Int32.Parse(ddltahun.SelectedValue) - 1;
                    }
                    else
                    {
                        rubahtahun2 = Int32.Parse(ddltahun.SelectedValue);
                        rubahbulan2 = Int32.Parse(ddlbulan.SelectedValue) - 1;
                    }
                    String rubahhari2 = DateTime.DaysInMonth(rubahtahun2, rubahbulan2).ToString();
                    string rubahtanggal2 = rubahhari2 + "/" + rubahbulan2 + "/" + rubahtahun2;
                    txttglAwallalu.Text = "01" + "/" + rubahbulan2 + "/" + rubahtahun2;
                    txttglAkhirlalu.Text = rubahtanggal2;
                }
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddltahun_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih Dahulu');", true);
                ddlcabang.Focus();
            }
            else
            {
                int rubahtahun = Int32.Parse(ddltahun.SelectedValue);
                int rubahbulan = Int32.Parse(ddlbulan.SelectedValue);
                DateTime.DaysInMonth(rubahtahun, rubahbulan);
                String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
                string rubahtanggal = rubahhari + "/" + ddlbulan.SelectedValue + "/" + ddltahun.SelectedValue;
                txttglAwalini.Text = "01" + "/" + ddlbulan.SelectedValue + "/" + ddltahun.SelectedValue;
                txttglAkhirini.Text = rubahtanggal;

                if (ddlbulan.SelectedValue == "12")
                {
                    int rubahtahun1 = Int32.Parse(ddltahun.SelectedValue) + 1;
                    int rubahbulan1 = 01;
                    String rubahhari1 = DateTime.DaysInMonth(rubahtahun1, rubahbulan1).ToString();
                    string rubahtanggal1 = rubahhari1 + "/" + rubahbulan1 + "/" + rubahtahun1;
                    string rubahMM = Convert.ToDateTime(rubahtanggal1).ToString("MM");
                    txttglAwaldepan.Text = "01" + "/" + "01" + "/" + rubahtahun1;
                    txttglAkhirdepan.Text = rubahhari1 + "/" + rubahMM + "/" + rubahtahun1;

                    int rubahtahun2 = Int32.Parse(ddltahun.SelectedValue);
                    int rubahbulan2 = Int32.Parse(ddlbulan.SelectedValue) - 1;
                    String rubahhari2 = DateTime.DaysInMonth(rubahtahun2, rubahbulan2).ToString();
                    string rubahtanggal2 = rubahhari2 + "/" + rubahbulan2 + "/" + rubahtahun2;
                    string rubahMM1 = Convert.ToDateTime(rubahtanggal2).ToString("MM");
                    txttglAwallalu.Text = "01" + "/" + rubahMM1 + "/" + rubahtahun2;
                    txttglAkhirlalu.Text = rubahhari2 + "/" + rubahMM1 + "/" + rubahtahun2;
                }
                else
                {
                    int rubahtahun1 = Int32.Parse(ddltahun.SelectedValue);
                    int rubahbulan1 = Int32.Parse(ddlbulan.SelectedValue) + 1;
                    String rubahhari1 = DateTime.DaysInMonth(rubahtahun1, rubahbulan1).ToString();
                    string rubahtanggal1 = rubahhari1 + "/" + rubahbulan1 + "/" + rubahtahun1;
                    string rubahMM = Convert.ToDateTime(rubahtanggal1).ToString("MM");
                    txttglAwaldepan.Text = "01" + "/" + rubahMM + "/" + rubahtahun1;
                    txttglAkhirdepan.Text = rubahhari1 + "/" + rubahMM + "/" + rubahtahun1;

                    int rubahtahun2 = Int32.Parse(ddltahun.SelectedValue) - 1;
                    int rubahbulan2 = 12;
                    String rubahhari2 = DateTime.DaysInMonth(rubahtahun2, rubahbulan2).ToString();
                    string rubahtanggal2 = rubahhari2 + "/" + rubahbulan2 + "/" + rubahtahun2;
                    txttglAwallalu.Text = "01" + "/" + "01" + "/" + rubahtahun2;
                    txttglAkhirlalu.Text = rubahtanggal2;
                }
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
            btnSimpan.Enabled = false;
            if (ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih Dahulu');", true);
                ddlcabang.Focus();
                ddlbulan.SelectedValue = "1";
                ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                txttglAwalini.Text = "";
                txttglAkhirini.Text = "";
                txttglAwaldepan.Text = "";
                txttglAkhirdepan.Text = "";
                txttglAwallalu.Text = "";
                txttglAkhirlalu.Text = "";
                btnSimpan.Enabled = true;
            }
            else
            {

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                var2[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlbulan.SelectedValue, typeof(string));
                var2[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 15, ddltahun.SelectedValue, typeof(string));
                var2[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlcabang.SelectedValue, typeof(string));
                var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PETUGAS", typeof(string));
                var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var2, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Petugas Bahan belum di input di bulan tersebut');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {
                    decimal nilaikasteller = 0;
                    //check kas teller harus 0
                    DefaultClass.Global.SqlHelper[] varc1 = new DefaultClass.Global.SqlHelper[7];

                    varc1[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                    varc1[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                    varc1[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                    varc1[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varc1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    varc1[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKTELLER", typeof(string));
                    varc1[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc1, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data Kas Teller tidak ditemukan');", true);
                        btnSimpan.Enabled = true;
                    }

                    else
                    {

                        nilaikasteller = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                        if (nilaikasteller != 0)
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Saldo Kas teller Masih Ada Belum Bisa Closing');", true);
                            btnSimpan.Enabled = true;


                        }

                        else
                        {

                            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[13];

                            var1[0] = gb.CreateSPParameter("@FiskalBi", ParameterDirection.Input, SqlDbType.Int, 0, ddlbulan.SelectedValue, typeof(string));
                            var1[1] = gb.CreateSPParameter("@FiskalTi", ParameterDirection.Input, SqlDbType.Int, 0, ddltahun.SelectedValue, typeof(string));
                            var1[2] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                            var1[3] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                            var1[4] = gb.CreateSPParameter("@tglblawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                            var1[5] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                            var1[6] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                            var1[7] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            var1[8] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                            var1[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CLOSING", typeof(string));
                            var1[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            var1[11] = gb.CreateSPParameter("@tglbdawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwaldepan.Text, typeof(string));
                            var1[12] = gb.CreateSPParameter("@tglbdakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirdepan.Text, typeof(string));

                            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var1, "constrmf");
                            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                            {


                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selamat Closing Bulanan Done');", true);

                                btnSimpan.Enabled = true;
                                ddlbulan.SelectedValue = "1";
                                ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                                txttglAwalini.Text = "";
                                txttglAkhirini.Text = "";
                                txttglAwaldepan.Text = "";
                                txttglAkhirdepan.Text = "";
                                txttglAwallalu.Text = "";
                                txttglAkhirlalu.Text = "";



                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                                btnSimpan.Enabled = true;
                            }





                        }

                    }









                }




            }





        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            btnSimpan.Enabled = true;
        }


    }
   
    protected void btncheckSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            btnSimpan.Enabled = false;
            if (ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih Dahulu');", true);
                ddlcabang.Focus();
                ddlbulan.SelectedValue = "1";
                ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                txttglAwalini.Text = "";
                txttglAkhirini.Text = "";
                txttglAwaldepan.Text = "";
                txttglAkhirdepan.Text = "";
                txttglAwallalu.Text = "";
                txttglAkhirlalu.Text = "";
                btnSimpan.Enabled = true;
            }
            else
            {

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                var2[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlbulan.SelectedValue, typeof(string));
                var2[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 15, ddltahun.SelectedValue, typeof(string));
                var2[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlcabang.SelectedValue, typeof(string));
                var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "PETUGAS", typeof(string));
                var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var2, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Petugas Bahan belum di input di bulan tersebut');", true);
                    btnSimpan.Enabled = true;
                }
                else
                {
                    decimal nilaikasteller = 0;
                    //check kas teller harus 0
                    DefaultClass.Global.SqlHelper[] varc1 = new DefaultClass.Global.SqlHelper[7];
                    
                    varc1[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                    varc1[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                    varc1[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                    varc1[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varc1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    varc1[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKTELLER", typeof(string));
                    varc1[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
          
                    ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc1, "constrmf");
                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data Kas Teller tidak ditemukan');", true);
                        btnSimpan.Enabled = true;
                    }

                    else
                    {

                        nilaikasteller = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                        if (nilaikasteller != 0)
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Saldo Kas teller Masih Ada Belum Bisa Closing');", true);
                            btnSimpan.Enabled = true;


                        }

                        else
                        {

                            string statusdata = "0";
                            //check kas teller harus 0
                            DefaultClass.Global.SqlHelper[] varc2 = new DefaultClass.Global.SqlHelper[7];

                            varc2[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                            varc2[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                            varc2[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                            varc2[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            varc2[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                            varc2[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKTUNGGAKAN", typeof(string));
                            varc2[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc2, "constrmf");
                            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data tidak ditemukan');", true);
                                btnSimpan.Enabled = true;
                            }

                            else
                            {

                                statusdata = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();


                                if (statusdata == "0")
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cek Kembali report tunggakan dengan piutang dibayarkan berbeda');", true);
                                    btnSimpan.Enabled = true;


                                }

                                else
                                {

                                    string statusdatax = "0";
                                    //check kas teller harus 0
                                    DefaultClass.Global.SqlHelper[] varc3 = new DefaultClass.Global.SqlHelper[7];

                                    varc3[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                                    varc3[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                                    varc3[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                                    varc3[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                    varc3[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                                    varc3[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKSIMPOK", typeof(string));
                                    varc3[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                    ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc3, "constrmf");
                                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data tidak ditemukan');", true);
                                        btnSimpan.Enabled = true;
                                    }

                                    else
                                    {

                                        statusdatax = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();


                                        if (statusdatax == "0")
                                        {

                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cek Kembali Simpok Neraca berbeda dengan List anggota');", true);
                                            btnSimpan.Enabled = true;


                                        }

                                        else
                                        {


                                            string statusdatax1 = "0";
                                            //check kas teller harus 0
                                            DefaultClass.Global.SqlHelper[] varc4 = new DefaultClass.Global.SqlHelper[7];

                                            varc4[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                                            varc4[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                                            varc4[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                                            varc4[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                            varc4[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                                            varc4[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKSIMWA", typeof(string));
                                            varc4[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                                            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc4, "constrmf");
                                            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data tidak ditemukan');", true);
                                                btnSimpan.Enabled = true;
                                            }

                                            else
                                            {

                                                statusdatax1 = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();


                                                if (statusdatax1 == "0")
                                                {

                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cek Kembali Simwa Neraca berbeda dengan List anggota');", true);
                                                    btnSimpan.Enabled = true;


                                                }

                                                else
                                                {


                                                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[13];

                                                    var1[0] = gb.CreateSPParameter("@FiskalBi", ParameterDirection.Input, SqlDbType.Int, 0, ddlbulan.SelectedValue, typeof(string));
                                                    var1[1] = gb.CreateSPParameter("@FiskalTi", ParameterDirection.Input, SqlDbType.Int, 0, ddltahun.SelectedValue, typeof(string));
                                                    var1[2] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                                                    var1[3] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                                                    var1[4] = gb.CreateSPParameter("@tglblawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                                                    var1[5] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                                                    var1[6] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                                                    var1[7] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                                                    var1[8] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                                                    var1[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CLOSING", typeof(string));
                                                    var1[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                                    var1[11] = gb.CreateSPParameter("@tglbdawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwaldepan.Text, typeof(string));
                                                    var1[12] = gb.CreateSPParameter("@tglbdakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirdepan.Text, typeof(string));

                                                    ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var1, "constrmf");
                                                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                                                    {


                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selamat Closing Bulanan Done');", true);

                                                        btnSimpan.Enabled = true;
                                                        ddlbulan.SelectedValue = "1";
                                                        ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                                                        txttglAwalini.Text = "";
                                                        txttglAkhirini.Text = "";
                                                        txttglAwaldepan.Text = "";
                                                        txttglAkhirdepan.Text = "";
                                                        txttglAwallalu.Text = "";
                                                        txttglAkhirlalu.Text = "";



                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                                                        btnSimpan.Enabled = true;
                                                    }




                                                }

                                            }










                                        }

                                    }









                                }

                            }





                        }

                    }



                          





                }




            }





        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            btnSimpan.Enabled = true;
        }


    }
    //unclosing
    protected void btnSimpan2_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih Dahulu');", true);
                ddlcabang.Focus();
                ddlbulan.SelectedValue = "1";
                ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                txttglAwalini.Text = "";
                txttglAkhirini.Text = "";
                txttglAwaldepan.Text = "";
                txttglAkhirdepan.Text = "";
                txttglAwallalu.Text = "";
                txttglAkhirlalu.Text = "";
            }
            else
            {
                if(ddlbulan.SelectedValue == "1")
                {
                    ddlbulan.SelectedValue = "12";
                    ddltahun.SelectedValue = Convert.ToString(int.Parse(ddltahun.SelectedValue) - 1);
                }
                else
                {
                    ddlbulan.SelectedValue = Convert.ToString(int.Parse(ddlbulan.SelectedValue) - 1);
                }
                ddlcabang_SelectedChange(ddlcabang.SelectedValue, e);
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[13];

                var1[0] = gb.CreateSPParameter("@FiskalBi", ParameterDirection.Input, SqlDbType.Int, 0, ddlbulan.SelectedValue, typeof(string));
                var1[1] = gb.CreateSPParameter("@FiskalTi", ParameterDirection.Input, SqlDbType.Int, 0, ddltahun.SelectedValue, typeof(string));
                var1[2] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
                var1[3] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
                var1[4] = gb.CreateSPParameter("@tglblawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
                var1[5] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirlalu.Text, typeof(string));
                var1[6] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                var1[7] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[8] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                var1[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UNCLOSING", typeof(string));
                var1[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[11] = gb.CreateSPParameter("@tglbdawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwaldepan.Text, typeof(string));
                var1[12] = gb.CreateSPParameter("@tglbdakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirdepan.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Un-Closing Done di bulan tersebut');", true);
                   
                    ddlbulan.SelectedValue = "1";
                    ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                    txttglAwalini.Text = "";
                    txttglAkhirini.Text = "";
                    txttglAwaldepan.Text = "";
                    txttglAkhirdepan.Text = "";
                    txttglAwallalu.Text = "";
                    txttglAkhirlalu.Text = "";
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