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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DocumentFormat.OpenXml.Bibliography;

public partial class OE_Process_TransaksiBiayabayarmukaAP : System.Web.UI.Page
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
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    DataTable dtBayar = new DataTable("TransactionBayar");

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "X3";
    string kodesumber = "CL";
    string KodeKonter = "CL16";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindGridEntity();


    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

            if (txtcatatanpusat.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Pusat Masih Kosong!');", true);
            }
            else
            {




                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, hdncabang.Value, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVE", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[5] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtcatatanpusat.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("OE_Biaya_BDM_PUSAT", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','Approve Pusat Pengajuan Sukses','top-right','success','2');", true);
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
    protected void btnTolak_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

            if (txtcatatanpusat.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Request Masih Kosong!');", true);
            }
            else
            {




                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, hdncabang.Value, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOLAK", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[5] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtcatatanpusat.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("OE_Biaya_BDM_PUSAT", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Tolak Done!','Tolak Pusat Pengajuan Sukses','top-right','success','2');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Selisih dengan Gaji Max');", true);
                //}

            }


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
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
                //pnlFilter.Visible = true;
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
               

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");

                ddljenis.SelectedValue = "S";

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenis.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Kodegroup asc";

                ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgroup.DataTextField = "Namagroup";
                ddlgroup.DataValueField = "Kodegroup";
                ddlgroup.DataBind();

                ddlgroup.Items.Insert(0, new ListItem("Pilih Group Biaya", "-"));
                ddlgroup.Enabled=true;

                //DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                ////var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTKARYAWAN", typeof(string));
                //varx1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //varx1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", varx1, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";

                //ddlKodenik.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlKodenik.DataTextField = "NamaKry";
                //ddlKodenik.DataValueField = "KodeNik";
                //ddlKodenik.DataBind();

                //ddlKodenik.Items.Insert(0, new ListItem("Pilih Karyawan", "-"));

                //ddltypememo.SelectedValue = "D1";
                txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglawalsusut.Text = xtanggal.ToString("dd-MM-yyyy");
                txttglakhirsusut.Text = xtanggal.ToString("dd-MM-yyyy");
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


    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        try
        {


            DateTime originalDate = DateTime.Parse(txttglawalsusut.Text);
            DateTime tgltrn = DateTime.Parse(txttgltrn.Text);
            decimal total = decimal.Parse(txtbayar.Text);
            decimal penyusutan = 0;


            if (originalDate < tgltrn)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('tgl penyusutan tidak boleh kurang dari tgl transakti!!');", true);
                var datax3 = "31-12-" + tgltrn.ToString("yyyy");
                DateTime originalakhirbulanx1 = DateTime.Parse(datax3);
                int monthsx1 = (originalakhirbulanx1.Year * 12 + originalakhirbulanx1.Month) - (tgltrn.Year * 12 + tgltrn.Month);
                monthsx1 = monthsx1 + 1;
                ddlmasasusut.SelectedValue = monthsx1.ToString();

                DateTime x3 = tgltrn.AddMonths(monthsx1 - 1);
                txttglakhirsusut.Text = x3.ToString("dd-MM-yyyy");
                txttglawalsusut.Text = tgltrn.ToString("dd-MM-yyyy");

                penyusutan = total / monthsx1;

                txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);
            }

            else
            {



                DateTime dateAfter7Days = originalDate;


                DateTime originalDatex = originalDate;

                var data1 = "31-12-" + tgltrn.ToString("yyyy");
                DateTime originalakhirbulan = DateTime.Parse(data1);
                int months = (originalakhirbulan.Year * 12 + originalakhirbulan.Month) - (dateAfter7Days.Year * 12 + dateAfter7Days.Month);
                months = months + 1;
                if (months <= 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masa Susut tidak boleh lebih dari 12 Bulan!!');", true);



                    var datax2 = "31-12-" + tgltrn.ToString("yyyy");
                    DateTime originalakhirbulanx = DateTime.Parse(datax2);
                    int monthsx = (originalakhirbulanx.Year * 12 + originalakhirbulanx.Month) - (tgltrn.Year * 12 + tgltrn.Month);
                    monthsx = monthsx + 1;
                    ddlmasasusut.SelectedValue = monthsx.ToString();

                    DateTime x2 = tgltrn.AddMonths(monthsx - 1);
                    txttglakhirsusut.Text = x2.ToString("dd-MM-yyyy");
                    txttglawalsusut.Text = tgltrn.ToString("dd-MM-yyyy");

                    penyusutan = total / monthsx;

                    txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);

                }
                else
                {
                    ddlmasasusut.SelectedValue = months.ToString();

                    DateTime x1 = dateAfter7Days.AddMonths(months - 1);
                    txttglakhirsusut.Text = x1.ToString("dd-MM-yyyy");
                    txttglawalsusut.Text = originalDate.ToString("dd-MM-yyyy");


                    penyusutan = total / months;

                    txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);

                }


            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    protected void ddlmasasusut_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {


            DateTime originalDate = DateTime.Parse(txttglawalsusut.Text);
            DateTime tgltrn = DateTime.Parse(txttgltrn.Text);
            decimal total = decimal.Parse(txtbayar.Text);
            decimal penyusutan = 0;


            if (originalDate< tgltrn)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('tgl penyusutan tidak boleh kurang dari tgl transakti!!');", true);
                var datax3 = "31-12-" + tgltrn.ToString("yyyy");
                DateTime originalakhirbulanx1 = DateTime.Parse(datax3);
                int monthsx1 = (originalakhirbulanx1.Year * 12 + originalakhirbulanx1.Month) - (tgltrn.Year * 12 + tgltrn.Month);
                monthsx1 = monthsx1 + 1;
                ddlmasasusut.SelectedValue = monthsx1.ToString();

                DateTime x3 = tgltrn.AddMonths(monthsx1 - 1);
                txttglakhirsusut.Text = x3.ToString("dd-MM-yyyy");
                txttglawalsusut.Text = tgltrn.ToString("dd-MM-yyyy");

                penyusutan = total / monthsx1;

                txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);
            }

            else
            {

           

            DateTime dateAfter7Days = originalDate;

 
            DateTime originalDatex = originalDate;

            var data1= "31-12-" + tgltrn.ToString("yyyy");
            DateTime originalakhirbulan = DateTime.Parse(data1);
            int months = (originalakhirbulan.Year * 12 + originalakhirbulan.Month) - (dateAfter7Days.Year * 12 + dateAfter7Days.Month);
            months = months + 1;
            if (months <=0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masa Susut tidak boleh lebih dari 12 Bulan!!');", true);


               
                var datax2 = "31-12-" + tgltrn.ToString("yyyy");
                DateTime originalakhirbulanx = DateTime.Parse(datax2);
                int monthsx = (originalakhirbulanx.Year * 12 + originalakhirbulanx.Month) - (tgltrn.Year * 12 + tgltrn.Month);
                monthsx = monthsx + 1;
                ddlmasasusut.SelectedValue = monthsx.ToString();

                DateTime x2 = tgltrn.AddMonths(monthsx - 1);
                txttglakhirsusut.Text = x2.ToString("dd-MM-yyyy");
                txttglawalsusut.Text = tgltrn.ToString("dd-MM-yyyy");

                    penyusutan = total / monthsx;

                    txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);

                }
            else
            {
                ddlmasasusut.SelectedValue = months.ToString();

                DateTime x1 = dateAfter7Days.AddMonths(months-1);
                txttglakhirsusut.Text= x1.ToString("dd-MM-yyyy");
                txttglawalsusut.Text = originalDate.ToString("dd-MM-yyyy");


                    penyusutan = total / months;

                    txtnilaisusut.Text = string.Format("{0:N2}", penyusutan);

                }


            }


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    protected void ddljenis_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.VarChar, 1, ddljenis.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "Kodegroup asc";

            ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgroup.DataTextField = "Namagroup";
            ddlgroup.DataValueField = "Kodegroup";
            ddlgroup.DataBind();


            ddlgroup.Items.Insert(0, new ListItem("Pilih Group Biaya", "-"));


            ddlgroup.Enabled = true;



        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }

    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            var bank = ddlgroup.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kodegroup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroup.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGROUP", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {

            txtcatatangroupbiaya.Text = ge.ListDataset.Tables[0].Rows[0]["catatan"].ToString();

        }
        else
        {
            txtcatatangroupbiaya.Text = "";
        }

        //if (ddlgroup.SelectedValue== "A0004" || ddlgroup.SelectedValue == "T0004")
        //{
        //    ddlKodenik.SelectedValue = "-";
        //    txtnamakaryawan.Text = "-";
        //    ddlKodenik.Enabled = true;
        //    txtnamakaryawan.Enabled = false;

        //}
        //else
        //{
        //    ddlKodenik.SelectedValue = "-";
        //    txtnamakaryawan.Text = "-";
        //    ddlKodenik.Enabled = false;
        //        txtnamakaryawan.Enabled = false;



        //    }





            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[3];
            //var[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            vart1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTBIAYAGROUP", typeof(string));
            vart1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vart1[2] = gb.CreateSPParameter("@kodegroup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroup.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", vart1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kodebiaya asc";

            ddlgroupbiaya.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgroupbiaya.DataTextField = "Namagroup";
            ddlgroupbiaya.DataValueField = "kodebiaya";
            ddlgroupbiaya.DataBind();

            ddlgroupbiaya.Items.Insert(0, new ListItem("Pilih Biaya", "-"));

            ddlgroupbiaya.Enabled = true;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    protected void ddlgroupbiaya_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            var bank = ddlgroup.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@kodegroup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroup.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGROUPBIAYA", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kodebiaya", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroupbiaya.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {

                txtcatatandetailbiaya.Text = ge.ListDataset.Tables[0].Rows[0]["catatan"].ToString();
                txtnote.Text = ge.ListDataset.Tables[0].Rows[0]["Namagroup"].ToString();

            }
            else
            {
                txtcatatandetailbiaya.Text = "";
                txtnote.Text = "-";
            }


            txtbayar.ReadOnly = false;


        
            DefaultClass.Global.SqlHelper[] vars1 = new DefaultClass.Global.SqlHelper[5];
            vars1[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            vars1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAKMBIAYA", typeof(string));
            vars1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            vars1[3] = gb.CreateSPParameter("@kodebiaya", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroupbiaya.SelectedValue, typeof(string));
            vars1[4] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenis.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", vars1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {

                txtakumulasibiaya.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["akumulasibiaya"].ToString()));
               

            }
            else
            {
                txtakumulasibiaya.Text = "0";
            }



        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }

    }


    //protected void ddlkodenik_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        var bank = ddlgroup.SelectedValue;
    //        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
    //        var[0] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKodenik.SelectedValue, typeof(string));
    //        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKRY", typeof(string));
    //        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
    //        if (ge.ListDataset.Tables[0].Rows.Count > 0)
    //        {

    //            txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
    //            txtnote.Text = ddlgroupbiaya.SelectedItem.Text + ' ' + txtnamakaryawan.Text;
    //            if (txtnote.Text.Length > 50)
    //            {
    //                txtnote.Text = txtnote.Text.Substring(0, 49);
    //            }
                
    //        }
    //        else
    //        {
    //            txtnamakaryawan.Text = "";
    //            txtnote.Text = ddlgroupbiaya.SelectedItem.Text;

    //        }


    //        DefaultClass.Global.SqlHelper[] vars1 = new DefaultClass.Global.SqlHelper[6];
    //        vars1[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //        vars1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTAKMKRY", typeof(string));
    //        vars1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //        vars1[3] = gb.CreateSPParameter("@kodebiaya", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroupbiaya.SelectedValue, typeof(string));
    //        vars1[4] = gb.CreateSPParameter("@kodenik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlKodenik.SelectedValue, typeof(string));
    //        vars1[5] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenis.SelectedValue, typeof(string));
    //        gex.ListDataset = gb.GetDataJustOutPut("OEbiaya", vars1, "constrmf");
    //        if (gex.ListDataset.Tables[0].Rows.Count > 0)
    //        {

    //            txtakumulasikry.Text = string.Format("{0:N2}", decimal.Parse(gex.ListDataset.Tables[0].Rows[0]["akumulasibiayakryx"].ToString()));


    //        }
    //        else
    //        {
    //            txtakumulasikry.Text = "0";
    //        }




    //    }
    //    catch (System.Exception X)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
    //    }

    //}
    

    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAAPPROVALBDMAP", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            //var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("OEbiaya", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "no_memo desc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrnx.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
            BindGridEntity();
        }
        else
        {
            BindGridEntity();
        }
    }
    public void BindGridEntityx()
    {
        try
        {
            var WhereCond = "";
            string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";
            WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[6];
            Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SEARCHBY", typeof(string));
            Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[4] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, "D1", typeof(string));
            Params[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AR_GetSEARCH", Params, "constrmf");

            GRData.DataSource = ge.ListDataset.Tables[0];
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
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text == "")
        {
            BindGridEntity();
        }
        else
        {
            BindGridEntityx();
        }
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        pnlFilter.Visible = false;
        switch (e.CommandName)
        {
            
            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
      
                    pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                     
                   
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;                    
                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttglawalsusut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglMulaiSusut")).Text;
                    txttglakhirsusut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTglAkhirSusut")).Text;

                    txtnilaisusut.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnilaisusut")).Text));
                    //ddlgroup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodegroup")).Text;
                    ddlmasasusut.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblmasamanfaat")).Text;

                    txtakumulasibiaya.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblakumulasibiaya")).Text));
                    //txtakumulasikry.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblakumulasikaryawan")).Text));
                    txtcatatangroupbiaya.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblcatatangroup")).Text;
                    txtcatatandetailbiaya.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("catatanbiayagroup")).Text;

                    txtbayar.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtnote.Text= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnotememo")).Text;
                    ddljenis.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljenis")).Text;
                    hdncabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;


                    ddlgroup.Items.Insert(0, new ListItem(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblgroupnama")).Text, ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodegroup")).Text));

                    ddlgroup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodegroup")).Text;

                    txtnote.Enabled = false;
                    txttgltrn.Enabled = false;
                    txttglawalsusut.Enabled = false;
                    txtbayar.ReadOnly = true;
                    btnSimpan.Visible = true;
                    ddlgroup.Enabled = false;
                    ddlgroupbiaya.Enabled = false;
                    //txtnamakaryawan.Enabled = false;
                    //ddlKodenik.Enabled = false;
                    ddljenis.Enabled = false;


                    ddlgroupbiaya.Items.Insert(0, new ListItem(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBiayanama")).Text, ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeBiaya")).Text));

                    ddlgroupbiaya.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeBiaya")).Text;



                    //if (ddlgroup.SelectedValue == "A0004")
                    //{

                    //    ddlKodenik.Items.Insert(0, new ListItem(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodenik")).Text + '-' + ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamakaryawan")).Text, ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodenik")).Text));
                    //    ddlKodenik.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodenik")).Text;

                    //    txtnamakaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamakaryawan")).Text;

                    //}
                    //else
                    //{

                    //    ddlKodenik.Items.Insert(0, new ListItem("Pilih Karyawan", "-"));

                    //    ddlKodenik.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodenik")).Text;

                    //    txtnamakaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamakaryawan")).Text;


                    //}
       


                    btndelete.Visible = true;
                    Pnlfooter4.Visible = true;
                   



                  



                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

              
                break;
            case "PDF":

                try
                {

                    //ReportDocument rprt = new ReportDocument();
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    var xstatus= ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;

                    if (xstatus != "5")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Biaya Belum di bayar Teller!');", true);
                    }
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    else
                    { 
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, "OE", typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.OEbiaya", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        //Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        Response.End();

                    }
                    }

                }

                catch (Exception X)
                {

                    if (X.Message != "Thread was being aborted.")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }
                    else
                    {

                        rprt.Close();
                        rprt.Dispose();
                        GC.Collect();



                    }


                }

                break;
            case "SLIP":

                try
                {

                    //ReportDocument rprt = new ReportDocument();
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    typetrn = "B";

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAR", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_CL_report", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CashAR.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        if (typetrn == "B")
                        {
                            rprt.SetParameterValue("Reportname", "Terima Cash Bank");
                        }
                        else
                        {
                            rprt.SetParameterValue("Reportname", "Keluar Cash Bank");
                        }

                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    }


                }

                catch (Exception X)
                {

                    if (X.Message != "Thread was being aborted.")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }
                    else
                    {

                        rprt.Close();
                        rprt.Dispose();
                    }


                }

                break;

        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("OE_Biaya_BDM_DELETE", var, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btndelete.Visible = false;

                nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
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
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
        {

            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtbayar.Text);

            btnSimpan.Enabled = false;

            DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
            varz[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
            varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
            varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
            varz[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
            ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglCabang", varz, "constrmf");

            string xsf = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
            string xnotifikasi = ge.ListDataset.Tables[0].Rows[0]["notif"].ToString();

            if (xsf != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + xnotifikasi + "');", true);
                btnSimpan.Enabled = true;
            }
            else
            {

                if (jlhdibayar <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong!');", true);
                    btnSimpan.Enabled = true;
                }
                if (ddlgroup.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Group Masih Kosong!');", true);
                }
                if (ddlgroupbiaya.SelectedValue == "-")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Group Biaya Masih Kosong!');", true);
                }
 
                else
                {


                    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[19];


                    Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                    Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                    Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtbayar.Text, typeof(decimal));
                    Params[3] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                    Params[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                    Params[5] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Params[6] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Params[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Params[8] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                    Params[9] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 150, txtnote.Text, typeof(string));
                    Params[10] = gb.CreateSPParameter("@kodegroup", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroup.SelectedValue, typeof(string));
                    Params[11] = gb.CreateSPParameter("@akumulasibiaya", ParameterDirection.Input, SqlDbType.Decimal, 0, txtakumulasibiaya.Text, typeof(decimal));
                    Params[12] = gb.CreateSPParameter("@kodebiaya", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgroupbiaya.SelectedValue, typeof(string));
                    Params[13] = gb.CreateSPParameter("@jenis", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenis.SelectedValue, typeof(string));
                    Params[14] = gb.CreateSPParameter("@TglMulaiSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0,txttglawalsusut.Text, typeof(DateTime));
                    Params[15] = gb.CreateSPParameter("@TglAkhirSusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglakhirsusut.Text, typeof(DateTime));
                    Params[16] = gb.CreateSPParameter("@Tglsusut", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglawalsusut.Text, typeof(DateTime));
                    Params[17] = gb.CreateSPParameter("@masasusut", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(ddlmasasusut.SelectedValue), typeof(int));
                    Params[18] = gb.CreateSPParameter("@nilaisusut", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnilaisusut.Text, typeof(decimal));
                    ge.ListDataset = gb.GetDataJustOutPut("OE_Biaya_BDM_SAVE", Params, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                        btnSimpan.Enabled = true;
                    }
                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);
                        btnSimpan.Enabled = true;
                    }

                    else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        btnSimpan.Visible = false;

                        nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }
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

  
  
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!','Cek tanggal');", true);
        
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
       
        }
        ddlmasasusut_SelectedIndexChanged(sender, e);

    }


}
