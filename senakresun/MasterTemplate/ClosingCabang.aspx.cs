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

public partial class CF_ClosingCabang : System.Web.UI.Page
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

                btnSimpan_check_Click(sender, e);

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
        PanelSystem.Visible = false;
        pnladd.Visible = true;
        btnSimpan.Enabled = true;
    }
    protected void btnSimpan_check_Click(object sender, EventArgs e)
    {
        try
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
                    btnpetugas.Text = "Not Completed";
                    lblpetugas.Text = "Petugas Bahan belum di input di bulan tersebut";
                    chkpetugasbahan.Checked = false;
                  
                    
                }
                else
                {
                    btnpetugas.Text = "Completed";
                    lblpetugas.Text = "Petugas Bahan Sudah Diinput";
                chkpetugasbahan.Enabled = true;
                    chkpetugasbahan.Checked = true;

                }
               
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
                        BtnKasTeller.Text = "Not Completed";
                        LblKasTeller.Text = "Data Kas Teller tidak ditemukan";
                        chkkasteller.Checked = false;
         
                    }

                    else
                    {

                        nilaikasteller = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                        if (nilaikasteller != 0)
                        {
                                
                                BtnKasTeller.Text = "Not Completed";
                                LblKasTeller.Text = "Saldo Kas teller Masih Ada Rp. " + string.Format("{0:N2}", nilaikasteller);
                                chkkasteller.Checked = false;    

                        }

                        else
                        {


                            BtnKasTeller.Text = "Completed";
                            LblKasTeller.Text = "Saldo Kas teller Rp. " + string.Format("{0:N2}", nilaikasteller);
                            chkkasteller.Checked = true;



                        }

                    }








            string statusdata = "0";
            decimal pinjamananggota = 0;
            decimal pinjamanBayar = 0;
            decimal bakidb = 0;
            decimal pengurangan = 0;
            decimal selisih = 0;
            string solusi = "0";
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data tidak ditemukan');", true);
                //btnSimpan.Enabled = true;
                BtnPiutang.Text = "Not Completed";
                Lblpiutanganggota.Text = "Data tidak ditemukan";
                lblPiutangdibayarkan.Text = "";
                lblPiutangpengurangan.Text = "";
                lblsaldoakhirtgk.Text = "";
                lblselisih.Text = "";
                lblsolusi.Text = "";
                ChkPiutanganggota.Checked = false;
            }

            else
            {

                statusdata = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();
                pinjamananggota = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pinjamananggota"].ToString());
                pinjamanBayar = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pinjamanBayar"].ToString());
                bakidb = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bakidb"].ToString());
                pengurangan = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["pengurangan"].ToString());
                selisih = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["selisih"].ToString());
                solusi = ge.ListDataset.Tables[0].Rows[0]["solusi"].ToString();
                if (statusdata == "0")
                {

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cek Kembali report tunggakan dengan piutang dibayarkan berbeda');", true);
                    //btnSimpan.Enabled = true;


                    BtnPiutang.Text = "Not Completed";
                    Lblpiutanganggota.Text = "Piutang Anggota Rp. " + string.Format("{0:N2}", pinjamananggota);
                    lblPiutangdibayarkan.Text = "Piutang dibayarkan Rp. " + string.Format("{0:N2}", pinjamanBayar);
                    lblPiutangpengurangan.Text = "Piutang Anggota dikurang Piutang dibayarkan Rp. " + string.Format("{0:N2}", pengurangan);
                    lblsaldoakhirtgk.Text = "Saldo Akhir Tunggakan Rp. " + string.Format("{0:N2}", bakidb);
                    lblselisih.Text = "Selisih Rp. " + string.Format("{0:N2}", selisih);
                    if (solusi=="1")
                    {
                        lblsolusi.Text = " Neraca Lebih Dari SPSM Lakukan Adjusment Pada Journal Otomatis pilih 143-Tambah Beban Keanggota pada Piutang Angota Masukan nilai selisih";
                    }
                    else if(solusi == "2")
                    {
                        lblsolusi.Text = " Neraca Kurang Dari SPSM Lakukan Adjusment Pada Journal Otomatis pilih 144-Kurang Beban Kenggota pada Piutang Angota Masukan nilai selisih";
                    }
                    else
                    {
                        lblsolusi.Text = "";
                    }
                   
                    ChkPiutanganggota.Checked = false;


                }

                else
                {

                    BtnPiutang.Text = "Completed";
                    Lblpiutanganggota.Text = "Piutang Anggota Rp. " + string.Format("{0:N2}", pinjamananggota);
                    lblPiutangdibayarkan.Text = "Piutang dibayarkan Rp. " + string.Format("{0:N2}", pinjamanBayar);
                    lblPiutangpengurangan.Text = "Piutang Anggota dikurang Piutang dibayarkan Rp. " + string.Format("{0:N2}", pengurangan);
                    lblsaldoakhirtgk.Text = "Saldo Akhir Tunggakan Rp. " + string.Format("{0:N2}", bakidb);
                    lblselisih.Text = "Selisih Rp. " + string.Format("{0:N2}", selisih);
                    lblsolusi.Text = "Done";
                    ChkPiutanganggota.Checked = true;


                }

            }




            string statusdatax = "0";
            int Listanggotasimpok = 0;
            int neracasimpok = 0;
            decimal anggotasimpok = 0;
            decimal anggotasimpokneraca = 0;
            decimal selisihsimpok = 0;
            string solusisimpok = "0";
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data tidak ditemukan');", true);
                //btnSimpan.Enabled = true;
                BtnSimpok.Text = "Not Completed";
                Lblanggota.Text = "Data tidak ditemukan";
                lblanggotasimpok.Text = "";
                Lblanggotaneraca.Text = "";
                lblanggotasimpokneraca.Text = "";
                lblselisihsimpok.Text = "";
                lblsolusisimpok.Text = "";
                chksimpok.Checked = false;
            }

            else
            {

                statusdatax = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();


                Listanggotasimpok = int.Parse(ge.ListDataset.Tables[0].Rows[0]["AnggotaSimpanan"].ToString());
                neracasimpok = int.Parse(ge.ListDataset.Tables[0].Rows[0]["simpokneraca"].ToString());
                anggotasimpok = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["AnggotaSimpanannilai"].ToString());
                anggotasimpokneraca = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["simpokneracanilai"].ToString());
                selisihsimpok = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Selisihsimpok"].ToString());
                solusisimpok = ge.ListDataset.Tables[0].Rows[0]["solusisimpok"].ToString();


                if (statusdatax == "0")
                {

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cek Kembali Simpok Neraca berbeda dengan List anggota');", true);
                    //btnSimpan.Enabled = true;

                    BtnSimpok.Text = "Not Completed";
                    Lblanggota.Text = "Total Anggota List " + string.Format("{0:N0}", Listanggotasimpok);
                    lblanggotasimpok.Text = "Total Simpanan Pokok Anggota List Rp. " + string.Format("{0:N0}", anggotasimpok);
                    Lblanggotaneraca.Text = "Total Anggota Neraca " + string.Format("{0:N0}", neracasimpok);
                    lblanggotasimpokneraca.Text = "Total Simpanan Pokok Anggota Neraca Rp. " + string.Format("{0:N0}", anggotasimpokneraca);
                    lblselisihsimpok.Text = "Selisih Rp. " + string.Format("{0:N0}", selisihsimpok);
                    
                    if (solusisimpok == "1")
                    {
                        lblsolusisimpok.Text = "Total Simpanan Pokok Pada List Anggota Lebih Dari Saldo Akhir Simpanan Pokok Pada Neraca Lakukan Adjusment Pada Journal Otomatis 150-Tambah Simpok dan Kurang Beban Keanggotaan masukkan selisih simpok";
                    }
                    else if (solusisimpok == "2")
                    {
                        lblsolusisimpok.Text = " Total Simpanan Pokok Pada List Anggota Kurang Dari Saldo Akhir Simpanan Pokok Pada Neraca Lakukan Adjusment Pada Journal Otomatis 151-Kurang Simpok dan tambah Beban Keanggotaan masukkan selisih simpok";
                    }
                    else
                    {
                        lblsolusisimpok.Text = "";
                    }

                    chksimpok.Checked = false;





                }

                else
                {


                    BtnSimpok.Text = "Completed";
                    Lblanggota.Text = "Total Anggota List " + string.Format("{0:N0}", Listanggotasimpok);
                    lblanggotasimpok.Text = "Total Simpanan Pokok Anggota List Rp. " + string.Format("{0:N0}", anggotasimpok);
                    Lblanggotaneraca.Text = "Total Anggota Neraca " + string.Format("{0:N0}", neracasimpok);
                    lblanggotasimpokneraca.Text = "Total Simpanan Pokok Anggota Neraca Rp. " + string.Format("{0:N0}", anggotasimpokneraca);
                    lblselisihsimpok.Text = "Selisih Rp. " + string.Format("{0:N0}", selisihsimpok);
                    lblsolusisimpok.Text = "";
                    chksimpok.Checked = true;




                }

            }







            string statusdatax1 = "0";
            decimal anggotasimwa = 0;
            decimal anggotasimwaneraca = 0;
            decimal selisihsimwa = 0;
            string solusisimwa = "0";

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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Data tidak ditemukan');", true);
                //btnSimpan.Enabled = true;
                btnsimwa.Text = "Not Completed";
                lblanggotasimwa.Text = "Data tidak ditemukan";
                lblanggotasimwaneraca.Text = "";
                lblselisihsimwa.Text = "";
                lblsolusisimwa.Text = "";
                chksimwa.Checked = false;
            }

            else
            {

                statusdatax1 = ge.ListDataset.Tables[0].Rows[0]["data"].ToString();
               
                anggotasimwa = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Jumlahanggotasimwa"].ToString());
                anggotasimwaneraca = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["simwaneraca"].ToString());
                selisihsimwa = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Selisihsimwa"].ToString());
                solusisimwa = ge.ListDataset.Tables[0].Rows[0]["solusisimwa"].ToString();

                if (statusdatax1 == "0")
                {

                    
                    btnsimwa.Text = "Not Completed";
                    lblanggotasimwa.Text = "Total Simpanan Wajib Anggota List Rp. " + string.Format("{0:N0}", anggotasimwa);
                    lblanggotasimwaneraca.Text = "Total Simpanan Wajib Anggota Neraca Rp. " + string.Format("{0:N0}", anggotasimwaneraca);
                    lblselisihsimwa.Text = "Selisih Rp. " + string.Format("{0:N0}", selisihsimwa);

                    if (solusisimwa == "1")
                    {
                        lblsolusisimwa.Text = "Total Simpanan Wajib Pada List Anggota Lebih Dari Saldo Akhir Simpanan Wajib Pada Neraca Lakukan Adjusment Pada Journal Otomatis 160-Tambah Simwa dan Kurang Beban Keanggotaan masukkan selisih simwa";
                    }
                    else if (solusisimwa == "2")
                    {
                        lblsolusisimwa.Text = " Total Simpanan Wajib Pada List Anggota Kurang Dari Saldo Akhir Simpanan Wajib Pada Neraca Lakukan Adjusment Pada Journal Otomatis 161-Kurang Simwa dan tambah Beban Keanggotaan masukkan selisih simwa";
                    }
                    else
                    {
                        lblsolusisimwa.Text = "";
                    }

                    chksimwa.Checked = false;





                }

                else
                {
                    btnsimwa.Text = "Completed";
                    lblanggotasimwa.Text = "Total Simpanan Wajib Anggota List Rp. " + string.Format("{0:N0}", anggotasimwa);
                    lblanggotasimwaneraca.Text = "Total Simpanan Wajib Anggota Neraca Rp. " + string.Format("{0:N0}", anggotasimwaneraca);
                    lblselisihsimwa.Text = "Selisih Rp. " + string.Format("{0:N0}", selisihsimwa);
                    lblsolusisimwa.Text = "";
                    chksimwa.Checked = true;

                }

            }




            decimal nilaimodal = 0;
            //check kas teller harus 0
            DefaultClass.Global.SqlHelper[] varc12 = new DefaultClass.Global.SqlHelper[7];

            varc12[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
            varc12[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
            varc12[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
            varc12[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varc12[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            varc12[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKMODAL", typeof(string));
            varc12[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc12, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                btnmodal.Text = "Not Completed";
                lblbungamodal.Text = "Data Modal tidak ditemukan";
                chkmodal.Checked = false;
                LblSolusimodal.Text = "";

            }

            else
            {

                nilaimodal = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                if (nilaimodal != 0)
                {

                    btnmodal.Text = "Not Completed";
                    lblbungamodal.Text = "Saldo Akhir Masih Ada Rp. " + string.Format("{0:N5}", nilaimodal);
                    chkmodal.Checked = false;
                    LblSolusimodal.Text = "Lakukan Pembulatan Jika Kurang dari 100 Rupiah";
                }

                else
                {


                    btnmodal.Text = "Completed";
                    lblbungamodal.Text = "Saldo Akhir Rp. " + string.Format("{0:N2}", nilaimodal);
                    chkmodal.Checked = true;
                    LblSolusimodal.Text = "";



                }

            }



            decimal nilaisimpananjakpen = 0;
            //check kas teller harus 0
            DefaultClass.Global.SqlHelper[] varc13 = new DefaultClass.Global.SqlHelper[7];

            varc13[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
            varc13[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
            varc13[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
            varc13[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varc13[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            varc13[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKJAKPEN", typeof(string));
            varc13[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc13, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                btnsimpananjakpen.Text = "Not Completed";
                lblsimpananjakpen.Text = "Data Simpanan Jangka Pendek tidak ditemukan";
                chksimpananjakpen.Checked = false;
                lblsolusisimpananjakpen.Text = "";

            }

            else
            {

                nilaisimpananjakpen = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                if (nilaisimpananjakpen != 0)
                {

                    btnsimpananjakpen.Text = "Not Completed";
                    lblsimpananjakpen.Text = "Saldo Akhir Masih Ada Rp. " + string.Format("{0:N5}", nilaisimpananjakpen);
                    chksimpananjakpen.Checked = false;
                    lblsolusisimpananjakpen.Text = "Lakukan Pembulatan Jika Kurang dari 100 Rupiah";
                }

                else
                {


                    btnsimpananjakpen.Text = "Completed";
                    lblsimpananjakpen.Text = "Saldo Akhir Rp. " + string.Format("{0:N2}", nilaisimpananjakpen);
                    chksimpananjakpen.Checked = true;
                    lblsolusisimpananjakpen.Text = "";



                }

            }



            decimal nilaisimpananjakpanjang = 0;
            //check kas teller harus 0
            DefaultClass.Global.SqlHelper[] varc14 = new DefaultClass.Global.SqlHelper[7];

            varc14[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
            varc14[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
            varc14[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
            varc14[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varc14[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            varc14[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKJAKPANJ", typeof(string));
            varc14[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc14, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                btnsimpananjakpanjang.Text = "Not Completed";
                lblsimpananjakpanjang.Text = "Data Simpanan Jangka Panjang tidak ditemukan";
                chksimpananjakpanjang.Checked = false;
                lblsolusisimpananjakpanjang.Text = "";

            }

            else
            {

                nilaisimpananjakpanjang = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                if (nilaisimpananjakpanjang != 0)
                {

                    btnsimpananjakpanjang.Text = "Not Completed";
                    lblsimpananjakpanjang.Text = "Saldo Akhir Masih Ada Rp. " + string.Format("{0:N5}", nilaisimpananjakpanjang);
                    chksimpananjakpanjang.Checked = false;
                    lblsolusisimpananjakpanjang.Text = "Lakukan Pembulatan Jika Kurang dari 100 Rupiah";
                }

                else
                {


                    btnsimpananjakpanjang.Text = "Completed";
                    lblsimpananjakpanjang.Text = "Saldo Akhir Rp. " + string.Format("{0:N2}", nilaisimpananjakpanjang);
                    chksimpananjakpanjang.Checked = true;
                    lblsolusisimpananjakpanjang.Text = "";



                }

            }

            decimal nilaiSukarela = 0;
            //check kas teller harus 0
            DefaultClass.Global.SqlHelper[] varc15 = new DefaultClass.Global.SqlHelper[7];

            varc15[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
            varc15[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
            varc15[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
            varc15[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varc15[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            varc15[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKSUKARELA", typeof(string));
            varc15[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc15, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                btnSimpananSukarela.Text = "Not Completed";
                lblSimpananSukarela.Text = "Data Simpanan Sukarela tidak ditemukan";
                chkSimpananSukarela.Checked = false;
                lblsolusiSimpananSukarela.Text = "";

            }

            else
            {

                nilaiSukarela = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                if (nilaiSukarela != 0)
                {

                    btnSimpananSukarela.Text = "Not Completed";
                    lblSimpananSukarela.Text = "Saldo Akhir Masih Ada Rp. " + string.Format("{0:N5}", nilaiSukarela);
                    chkSimpananSukarela.Checked = false;
                    lblsolusiSimpananSukarela.Text = "Lakukan Pembulatan Jika Kurang dari 100 Rupiah";
                }

                else
                {


                    btnSimpananSukarela.Text = "Completed";
                    lblSimpananSukarela.Text = "Saldo Akhir Rp. " + string.Format("{0:N2}", nilaiSukarela);
                    chkSimpananSukarela.Checked = true;
                    lblsolusiSimpananSukarela.Text = "";



                }

            }





            decimal nilaibungabank = 0;
            //check kas teller harus 0
            DefaultClass.Global.SqlHelper[] varc16 = new DefaultClass.Global.SqlHelper[7];

            varc16[0] = gb.CreateSPParameter("@tglbiawal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwalini.Text, typeof(string));
            varc16[1] = gb.CreateSPParameter("@tglbiakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAkhirini.Text, typeof(string));
            varc16[2] = gb.CreateSPParameter("@tglblakhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglAwallalu.Text, typeof(string));
            varc16[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varc16[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            varc16[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKBUNGABANK", typeof(string));
            varc16[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", varc16, "constrmf");
            if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
            {
                btnbungabank.Text = "Not Completed";
                lblbungaBank.Text = "Data Hutang Bunga Bank tidak ditemukan";
                chkbungabank.Checked = false;
                lblsolusibungaBank.Text = "";

            }

            else
            {

                nilaibungabank = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString());


                if (nilaibungabank != 0)
                {

                    btnbungabank.Text = "Not Completed";
                    lblbungaBank.Text = "Saldo Akhir Masih Ada Rp. " + string.Format("{0:N5}", nilaibungabank);
                    chkbungabank.Checked = false;
                    lblsolusibungaBank.Text = "Lakukan Pembulatan Jika Kurang dari 100 Rupiah";
                }

                else
                {


                    btnbungabank.Text = "Completed";
                    lblbungaBank.Text = "Saldo Akhir Rp. " + string.Format("{0:N2}", nilaibungabank);
                    chkbungabank.Checked = true;
                    lblsolusibungaBank.Text = "";



                }

            }



            if (chkkasteller.Checked == true && chkpetugasbahan.Checked == true && ChkPiutanganggota.Checked == true && chksimpok.Checked == true && chksimwa.Checked == true && chkmodal.Checked == true && chksimpananjakpen.Checked == true && chksimpananjakpanjang.Checked == true && chkSimpananSukarela.Checked == true && chkbungabank.Checked == true)
            {
                btnnextclosing.Visible = true;
                btninfo.Visible = false;
            }
            else
            {
                btnnextclosing.Visible = false;
                btninfo.Visible = true;

            }




        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            btnSimpan.Enabled = true;
        }


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


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selamat Closing Bulanan ini Done');", true);

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