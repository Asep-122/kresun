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
public partial class AE_Process_MemoKasbonApproveGm : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtrpinjaman = new DataTable("Pinjaman");
    DataTable dtrangsuran = new DataTable("Angsuran");
    DataTable dtrJurnal = new DataTable("Jurnal");
    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "B2";
    string kodesumber = "AE";
    string KodeKonter = "AE02";
    public string nobuktidb;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text;
                //txtKeterangan.Attributes.Add("maxlength", "50");


                ////KODEBANK
                //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                //var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLMST", typeof(string));
                //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var, "constrmf");

                //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBank asc";


                //ddlKdBank.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlKdBank.DataTextField = "namabank";
                //ddlKdBank.DataValueField = "KodeBank";
                //ddlKdBank.DataBind();
                //ddlKdBank.Items.Insert(0, new ListItem("Pilih Data", "-"));


                //DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[4]; 
                //var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCLJURNAL", typeof(string));
                //var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var6[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var6, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJurnal asc";
                //ddlJurnal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlJurnal.DataTextField = "NamaJurnal";
                //ddlJurnal.DataValueField = "KodeJurnal";
                //ddlJurnal.DataBind();
                //ddlJurnal.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ////KODEjurnal
                //string jurnalcode;
                //DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];
                //varx[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //varx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //varx[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ARJURNALSIMPOK", typeof(string));
                //varx[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varx, "constrmf");
                //if (ge.ListDataset.Tables[0].Rows.Count > 0)
                //{
                //    jurnalcode = ge.ListDataset.Tables[0].Rows[0]["CL_AR_SIMPOK_JURNAL"].ToString();
                //}
                //else
                //{
                //    jurnalcode = "";


                //}


                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STATUSKARY", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

                ddlsttskyn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlsttskyn.DataTextField = "TEXT";
                ddlsttskyn.DataValueField = "VALUE";
                ddlsttskyn.DataBind();



                //DROPDOWNLIST TYPE GOLONGAN
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[2];
                var5[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETGOLONGAN", typeof(string));
                var5[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var5, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGol asc";

                ddlgol.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlgol.DataTextField = "kdket";
                ddlgol.DataValueField = "KodeGol";
                ddlgol.DataBind();
                ddlgol.Items.Insert(0, new ListItem("Pilih Data", "-"));

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBAGIAN", typeof(string));
                var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

                ddlbag.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlbag.DataTextField = "kdket";
                ddlbag.DataValueField = "KodeBagian";
                ddlbag.DataBind();
                ddlbag.Items.Insert(0, new ListItem("Pilih Data", "-"));


                //DROPDOWNLIST TYPE JABATAN
                DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
                var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
                var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

                ddljab.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddljab.DataTextField = "kdket";
                ddljab.DataValueField = "KodeJab";
                ddljab.DataBind();
                ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //DROPDOWNLIST TYPE KANTOR
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKANTOR", typeof(string));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var1, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

                ddloff.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddloff.DataTextField = "kdnm";
                ddloff.DataValueField = "KodeCabang";
                ddloff.DataBind();
                //ddloff.Items.Insert(0, new ListItem("Pilih Data", "-"));
                ddloff.SelectedValue = SessBranchID;
                //if (jurnalcode!="")
                //{


                //string Jur = jurnalcode;
                ////TEXTBOX AUTO CURRENCY
                //DefaultClass.Global.SqlHelper[] varT1 = new DefaultClass.Global.SqlHelper[5];
                //varT1[0] = gb.CreateSPParameter("@kodejurnal", ParameterDirection.Input, SqlDbType.VarChar, 30, Jur, typeof(string));
                //varT1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIClJURNAL", typeof(string));
                //varT1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //varT1[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                //varT1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varT1, "constrmf");

                //string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
                //string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
                //string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
                //string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
                //string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
                //txtJurnal.Text = KetJurnal;
                //ddlJurnal.SelectedValue = jurnalcode;
                //txtDebit1.Text = DebitGL;
                //txtDebit2.Text = DebitCC;
                //txtKredit1.Text = KreditGL;
                //txtKredit2.Text = KreditCC;

                //txtkodejournal.Text = jurnalcode;
                //txtnamajurnal.Text = txtJurnal.Text;

                ////GET KETERANGAN GL
                //DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[5];
                //varGL[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, DebitGL, typeof(string));
                //varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
                //varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //varGL[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                //varGL[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varGL, "constrmf");
                //string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                //txtDebit3.Text = DCC;

                ////GET KETERANGAN GL
                //DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[5];
                //varCC[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, KreditGL, typeof(string));
                //varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
                //varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //varCC[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                //varCC[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varCC, "constrmf");
                //string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
                //txtKredit3.Text = KGL;



                ////DebitGL
                //var bank = DebitGL;
                //ddlKdBank.SelectedValue = DebitGL;
                //DefaultClass.Global.SqlHelper[] varp = new DefaultClass.Global.SqlHelper[5];
                //varp[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
                //varp[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //varp[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //varp[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
                //varp[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varp, "constrmf");
                //if (ge.ListDataset.Tables[0].Rows.Count > 0)
                //{
                //    string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
                //    string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();



                //    txtNmBank.Text = NamaBank;
                //    ddlkursbank.SelectedValue = kdcrc;
                //    txtkursbank.Text = "1";

                //}
                //else
                //{
                //    txtNmBank.Text = "";
                //    ddlkursbank.ClearSelection();
                //    ddlkursbank.SelectedValue = "IDR";
                //    txtkursbank.Text = "1";


                //}

                //ddlKdBank.Enabled = false;
                //ddlJurnal.Enabled = false;
                //ddltypejur.SelectedValue = "1";
                //ddltypejur.Enabled = false;

                //}
            }


            //pwcabang system kurs single default IDR kurs 1
            if (SessSystemKurs == "False")
            {
                ddlkursSup.Items.Insert(0, new ListItem("IDR"));
                ddlkursSup.SelectedValue = "IDR";
                ddlkursSup.Enabled = false;
                txtkursstd.Text = "1";
                txtkursstd.Enabled = false;

                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = false;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = false;
            }
            else
            {
                ddlkursSup.Items.Insert(0, new ListItem("IDR"));
                ddlkursSup.SelectedValue = "IDR";
                ddlkursSup.Enabled = true;
                txtkursstd.Text = "1";
                txtkursstd.Enabled = true;

                ddlkursbank.Items.Insert(0, new ListItem("IDR"));
                ddlkursbank.SelectedValue = "IDR";
                ddlkursbank.Enabled = true;
                txtkursbank.Text = "1";
                txtkursbank.Enabled = true;
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

    protected void btnreload_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridEntity();




            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;
        GRRP.Enabled = true;
        //pnlHeader.Visible = false;
        PnlFront.Visible = false;

        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        //pnladd.Visible = true;
        Pnlfooter1.Visible = true;
        txtBankGiro.Text = "-";
        txtnoGiro.Text = "-";
        //txtKeterangan.Text = "Terima Simpok";
        Pnlcustomer.Visible = true;
        Pnlfooter1.Visible = false;
        //Pnlfooter4.Visible = true;
        if (txtnobukti.Text == "")
        {
            btnSimpan.Visible = true;
            btnSimpan.Enabled = false;
            btndelete.Visible = false;
        }
        else
        {
            btnSimpan.Visible = false;
            btndelete.Visible = true;
        }

        btnback4.Visible = false;
        btnclose.Visible = true;
        Pnlcustomer.Visible = true;

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void ddlkursBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkursbank.Text = ddlkursbank.SelectedValue;
    }
    protected void ddlKdBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdBank", ParameterDirection.Input, SqlDbType.VarChar, 8, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICLMST", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {
            string NamaBank = ge.ListDataset.Tables[0].Rows[0]["NamaBank"].ToString();
            string kdcrc = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            


            txtNmBank.Text = NamaBank;
            ddlkursbank.SelectedValue = kdcrc;
            txtkursbank.Text = "1";

        }
        else
        {
            txtNmBank.Text = "";
            ddlkursbank.ClearSelection();
            ddlkursbank.SelectedValue = "IDR";
            txtkursbank.Text = "1";
            

        }

    
    }
    public void BindGridEntity()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAKASBONGMAPPROVEGM", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SKASBONKARYAWAN", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "tglupdatememo Desc";


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
                    pnlmemo.Visible = true;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    txtangsuran.Enabled = false;
                    txtjmlpinjaman.Enabled = false;
                    rbpotong.Enabled = false;
                   
                    txtbataspinjaman.Enabled = false;
                    txtsaldobelumlunas.Enabled = false;
                    txtangsuranperbulan.Enabled = false;
                    //pnlHeader.Visible = false;
                    blnadd = 1;
                    //GRTP.Enabled = false;
                    Pnlfooter1.Visible = false;
                    Pnlfooter4.Visible = true;
                    Pnlcustomer.Visible = false;
                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    txtkodekaryawan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;
                    hdncabang.Value = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                    var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, hdncabang.Value, typeof(string));
                    var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAKASBON", typeof(string));
                    var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_SKASBONKARYAWAN", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count > 0)
                    {
                        txtnoteverifikasi.Text= ge.ListDataset.Tables[0].Rows[0]["notemc"].ToString();
                        hdncabang.Value= ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                        txtkodekaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString(); ;
                        txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                        txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["plafonkasbon"].ToString()));
                        txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tglmasuk"].ToString()).ToString("dd-MM-yyyy");
                        txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
                        ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                        ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["bagian"].ToString();

                        //DROPDOWNLIST TYPE JABATAN
                        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[4];
                        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
                        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var4[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));
                        var4[3] = gb.CreateSPParameter("@kdbagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbag.SelectedValue, typeof(string));
                        gex.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
                        gex.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

                        ddljab.DataSource = gex.ListDataset.Tables[0].DefaultView.ToTable();
                        ddljab.DataTextField = "kdket";
                        ddljab.DataValueField = "KodeJab";
                        ddljab.DataBind();
                        ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));

                        ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                        ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["golongan"].ToString();
                        ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["statuskaryawan"].ToString();
                        txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["netgaji"].ToString()));
                        txtsaldobelumlunas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldobelumlunas"].ToString()));
                        txtlimitkasbon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["limitkasbon"].ToString()));
                        txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["angsuranperbulan"].ToString()));
                        if (ge.ListDataset.Tables[0].Rows[0]["Potgaji"].ToString() == "1")
                        {
                            rbpotong.SelectedValue = "1";
                        }
                        else
                        {
                            rbpotong.SelectedValue = "2";
                        }
                        txttgltrn.Text= DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTrn"].ToString()).ToString("dd-MM-yyyy");
                        txtjmlpinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Jlhkasbon"].ToString()));
                        txtangsuran.Text = string.Format("{0:N0}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["tenor"].ToString()));
                        txtnote.Text = ge.ListDataset.Tables[0].Rows[0]["notememo"].ToString();

                    }


                   
                    //rbpotong.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString() == "1" ? "1" : "2"; rbpotong.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString() == "0" ? "2" : ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString();
                    //txtkodekaryawan.Text = "";
                    //txtnamakaryawan.Text = "";
                    ddlkursSup.SelectedValue = "IDR";
                    txtkursstd.Text = "1";

                    txtsaldobelumlunas.Enabled = false;
                    txtangsuranperbulan.Enabled = false;
                    txtbataspinjaman.Enabled = false;
                    txttgltrn.Enabled = false;
                    rbpotong.Enabled = true;
                    //txtKeterangan.Enabled = false;
                    txttgltrn.Enabled = false;
                    txtangsuran.Enabled = false;
                    txtjmlpinjaman.Enabled = false;


                  
                    //txtjmlhCL.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));

                    ddltypejur.Enabled = false;
                    //Pnlfooter1.Visible = true;
                    btnSimpan.Visible = false;
                    btnback4.Visible = false;
                    Pnlfooter4.Visible = true;
                    btndelete.Visible = false;
                    btnclose.Visible = true;
                    btngenerate.Visible = false;

                    PnlGrid.Visible = true;
                    DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[6];
                    Paramsx[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    Paramsx[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    Paramsx[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, hdncabang.Value, typeof(string));
                    Paramsx[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAKASBONDTL", typeof(string));
                    Paramsx[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    Paramsx[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("AE_SKASBONKARYAWAN", Paramsx, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {

                        ViewState["ListDetailRP"] = ge.ListDataTable;

                        GRRP.DataSource = ge.ListDataTable;
                        GRRP.DataBind();

                        GRRP.UseAccessibleHeader = true;
                        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                        //cssGridAP();
                        //subgridGL_Click();
                    }
                    else
                    {
                        //SetDetailAP();
                        //subgridGL_Click();
                    }
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


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

                    ReportDocument rprt = new ReportDocument();
                    string nobukti;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ReportJournal", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 50, "AE", typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_Trn_AR", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/GL/Process/Report/GL.STD.RPT.010.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
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


                }

                break;
            case "SLIP":

                try
                {

                    ReportDocument rprt = new ReportDocument();
                    string nobukti;
                    string typetrn;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    typetrn = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatus")).Text;
                    if (typetrn != "5")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Pinjaman Belum di approve Pusat');", true);
                    }
                    else
                    {

                    
                    //hdnnourut.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[5];
                    Paramsc[0] = gb.CreateSPParameter("@noBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SLIPAEKASBON", typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    Paramsc[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                    Paramsc[4] = gb.CreateSPParameter("@typetrn", ParameterDirection.Input, SqlDbType.VarChar, 10, typetrn, typeof(string));

                    ge.ListDataTable = gb.GetDataJustOutPut("dbo.AE_Trn_AEreport", Paramsc, "constrmf").Tables[0];

                    if (ge.ListDataTable.Rows.Count > 0)
                    {


                        rprt.Load(Server.MapPath("~/AE/Report/CR/CLKwitansikeluar.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                     
                            //rprt.SetParameterValue("Reportname", "Cetak Pinjaman");
                        

                        Response.Buffer = false;
                        // Clear the response content and headers
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                    }

                    }
                }

                catch (Exception X)
                {

                    if (X.Message != "Thread was being aborted.")
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    }


                }

                break;

        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
            var1[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var1[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEPINJAMAN", typeof(string));
            var1[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //var1[5] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodekaryawan.Text, typeof(string));
            var1[5] = gb.CreateSPParameter("@JlhDebit", ParameterDirection.Input, SqlDbType.Money, 0, Convert.ToDecimal(txtjmlpinjaman.Text), typeof(decimal));

            ge.ListDataset = gb.GetDataJustOutPut("AE_PINJAMANKARYAWAN_Delete", var1, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                btnCancel.Visible = false;
                nobuktidb = txtnobukti.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Data Berhasil Di hapus','bottom-center','success','2');", true);

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
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
         try
         {
            decimal jlhdibayar = decimal.Parse(txtjmlpinjaman.Text);
            int Angsuran = int.Parse(txtangsuran.Text);
            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Pinjaman Masih Kosong!');", true);
            }
            else if (Angsuran <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Banyak Angsuran Masih Kosong!');", true);
            }
            else
            {
                dtRP.Columns.Clear();
                dtRP.Columns.Add(new DataColumn("tgljthtempo", typeof(DateTime)));
                dtRP.Columns.Add(new DataColumn("angske", typeof(int)));
                dtRP.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("keterangan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("bulan", typeof(int)));
                dtRP.Columns.Add(new DataColumn("tahun", typeof(int)));

                //string nomorrekening = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
                dtRP.Rows.Clear();
                for (int i = 0; i < GRRP.Rows.Count; i++)
                 
                {
                    DataRow dr = dtRP.NewRow();
                    
                    dr["tgljthtempo"] = ((Label)GRRP.Rows[i].FindControl("lbltgl")).Text;
                    dr["angske"] = int.Parse(((Label)GRRP.Rows[i].FindControl("lblangske")).Text); 
                    dr["angsuran"] = decimal.Parse(((Label)GRRP.Rows[i].FindControl("lblangsuran")).Text); 
                    dr["keterangan"] = ((Label)GRRP.Rows[i].FindControl("lblketerangan")).Text;
                    dr["bulan"] = int.Parse(((Label)GRRP.Rows[i].FindControl("lblbulan")).Text);
                    dr["tahun"] = int.Parse(((Label)GRRP.Rows[i].FindControl("lbltahun")).Text);
                    dtRP.Rows.Add(dr);
                }
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[25];
                var1[0] = gb.CreateSPParameter("@AE_Angsuran_Pinjaman", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, txtjmlpinjaman.Text, typeof(decimal));
                var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                var1[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                var1[6] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtnoteverifikasi.Text, typeof(string));
                var1[7] = gb.CreateSPParameter("@TglTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                var1[8] = gb.CreateSPParameter("@tenor", ParameterDirection.Input, SqlDbType.Int, 0, txtangsuran.Text, typeof(int));
                var1[9] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodekaryawan.Text, typeof(string));
                var1[10] = gb.CreateSPParameter("@KodeSumber", ParameterDirection.Input, SqlDbType.VarChar, 30, "AE", typeof(string));
                var1[11] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                var1[12] = gb.CreateSPParameter("@NamaKry", ParameterDirection.Input, SqlDbType.VarChar, 100, txtnamakaryawan.Text, typeof(string));
                var1[13] = gb.CreateSPParameter("@potgaji", ParameterDirection.Input, SqlDbType.VarChar, 5, rbpotong.SelectedValue, typeof(string));
                var1[14] = gb.CreateSPParameter("@tglmasuk", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgljoin.Text, typeof(DateTime));
                var1[15] = gb.CreateSPParameter("@masakerja", ParameterDirection.Input, SqlDbType.VarChar, 10, txtmasakerja.Text, typeof(string));
                var1[16] = gb.CreateSPParameter("@statuskry", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlsttskyn.SelectedValue, typeof(string));
                var1[17] = gb.CreateSPParameter("@bagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbag.SelectedValue, typeof(string));
                var1[18] = gb.CreateSPParameter("@jabatan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljab.SelectedValue, typeof(string));
                var1[19] = gb.CreateSPParameter("@golongan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlgol.SelectedValue, typeof(string));
                var1[20] = gb.CreateSPParameter("@saldoblmlunas", ParameterDirection.Input, SqlDbType.Money, 0, txtsaldobelumlunas.Text, typeof(decimal));
                var1[21] = gb.CreateSPParameter("@gajinet", ParameterDirection.Input, SqlDbType.Money, 0, txtnetgaji.Text, typeof(decimal));
                var1[22] = gb.CreateSPParameter("@Plafonpinjaman", ParameterDirection.Input, SqlDbType.Money, 0, txtbataspinjaman.Text, typeof(decimal));
                var1[23] = gb.CreateSPParameter("@limitpinjaman", ParameterDirection.Input, SqlDbType.Money, 0, txtlimitkasbon.Text, typeof(decimal));
                var1[24] = gb.CreateSPParameter("@angsuranperbulan", ParameterDirection.Input, SqlDbType.Money, 0, txtangsuranperbulan.Text, typeof(decimal));
                ge.ListDataset = gb.GetDataJustOutPut("AE_KASBONKARYAWAN_INSERT", var1, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Visible = false;
                    btnCancel.Visible = false;
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

    protected void ddlKdSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        var bank = txtkodekaryawan.Text;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 15, bank, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMST", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var, "constrmf");

        txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaLgn"].ToString();
        ddlkursSup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
        txtkursstd.Text = "1";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }

    protected void ddltypejur_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddltypejur.SelectedValue == "3")
        {
            Pnlfooter1.Visible = false;
            Pnlfooter4.Visible = true;
            if (txtnobukti.Text=="")
            {
                btnSimpan.Visible = true;
                btndelete.Visible = false;
            }
            else
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            
            btnback4.Visible = false;
            btnclose.Visible = true;
        }
        else
        {
            Pnlfooter1.Visible = true;
            Pnlfooter4.Visible = false;
            btnSimpan.Visible = false;
            btndelete.Visible = false;
            btnback4.Visible = true;
            btnclose.Visible = false;
        }
      
    }

    //protected void txtdate_TextChanged(object sender, System.EventArgs e)
    //{
    //    DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
    //    varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
    //    varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
    //    varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //    varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
    //    ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

    //    string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
    //    DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
    //    if (xstatus == "1")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
    //        txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
    //        txttgljthtmp.Text = txttgltrn.Text;
    //    }
    //    txttgljthtmp.Text = txttgltrn.Text;
    //}

    private void lnkAddRP2_Click()
    {

        int dgcount = GRRP.Rows.Count;
        if (dgcount > 0)
        {
            dtRP.Columns.Clear();

            dtRP = (DataTable)ViewState["ListDetailRP"];

            dtRP.Rows.Clear();

            for (int i = 0; i < dgcount; i++)
            {
                DataRow dr = dtRP.NewRow();

                dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
                dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
                dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
                dtRP.Rows.Add(dr);
            }

            //DataRow drdnew = dtRP.NewRow();
            //drdnew["nomor"] = dgcount + 1;
            //drdnew["KodeGL"] = string.Empty;
            //drdnew["NamaGL"] = string.Empty;
            //drdnew["Description"] = string.Empty;
            //drdnew["Costcenter"] = string.Empty;
            //drdnew["jlhdebit"] = decimal.Zero;
            //drdnew["jlhkredit"] = decimal.Zero;

            //dtRP.Rows.Add(drdnew);

            ViewState["ListDetailRP"] = dtRP;

            GRRP.DataSource = dtRP;
            GRRP.DataBind();

            GRRP.UseAccessibleHeader = true;
            GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridRP();

        }


        else
        {
            SetDetailRP();
        }


    }
    private void cssGridRP()
    {

        // Attribute to show the Plus Minus Button.
        GRRP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRRP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-hide", "phone");
        cells[4].Attributes.Add("data-hide", "phone");

    }
    private void SetDetailRP()
    {

        DataRow dr = null;

        dtRP.Columns.Add(new DataColumn("nomor", typeof(int)));
        dtRP.Columns.Add(new DataColumn("KodeGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("NamaGL", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Description", typeof(string)));
        dtRP.Columns.Add(new DataColumn("Costcenter", typeof(string)));

        dtRP.Columns.Add(new DataColumn("jlhdebit", typeof(decimal)));
        dtRP.Columns.Add(new DataColumn("jlhkredit", typeof(decimal)));


        dr = dtRP.NewRow();
        dr["nomor"] = 1;
        dr["KodeGL"] = string.Empty;
        dr["NamaGL"] = string.Empty;
        dr["Description"] = txtnote.Text; 
        dr["Costcenter"] = string.Empty;
        dr["jlhdebit"] = decimal.Zero;
        dr["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetailRP"] = dtRP;


        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridRP();
    }
    protected void lnkBT_Click(object sender, EventArgs e)
    {
        int dgcount = GRRP.Rows.Count;

        dtRP.Columns.Clear();

        dtRP = (DataTable)ViewState["ListDetailRP"];

        dtRP.Rows.Clear();

        for (int i = 0; i < dgcount; i++)
        {
            DataRow dr = dtRP.NewRow();

            dr["nomor"] = ((Label)GRRP.Rows[i].FindControl("lblNumber")).Text;
            dr["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
            dr["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).Text : (object)DBNull.Value;
            dr["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;//((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
            dr["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
            dr["jlhdebit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
            dr["jlhkredit"] = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text);
            dtRP.Rows.Add(dr);
        }

        DataRow drdnew = dtRP.NewRow();
        drdnew["nomor"] = dgcount + 1;
        drdnew["KodeGL"] = string.Empty;
        drdnew["NamaGL"] = string.Empty;
        drdnew["Description"] = string.Empty;
        drdnew["Costcenter"] = string.Empty;
        drdnew["jlhdebit"] = decimal.Zero;
        drdnew["jlhkredit"] = decimal.Zero;

        dtRP.Rows.Add(drdnew);

        ViewState["ListDetailRP"] = dtRP;

        GRRP.DataSource = dtRP;
        GRRP.DataBind();

        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
        subgridGL_Click();
        cssGridRP();
    }
    protected void GRRP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "DEL":

                int dgcount = GRRP.Rows.Count;


                ((Label)GRRP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

                dtRP.Columns.Clear();

                dtRP = (DataTable)ViewState["ListDetailRP"];

                DataRow drCurrentRow = null;

                dtRP.Rows.Clear();

                int j = 1;

                for (int i = 0; i < GRRP.Rows.Count; i++)
                {

                    if (((Label)GRRP.Rows[i].FindControl("lblNumber")).Text != "")
                    {


                        drCurrentRow = dtRP.NewRow();

                        drCurrentRow["nomor"] = j;
                        drCurrentRow["KodeGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlKodeGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["NamaGL"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlnamaGL")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["Description"] = ((TextBox)GRRP.Rows[i].FindControl("txtKT")).Text;
                        drCurrentRow["Costcenter"] = ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue != string.Empty ? ((DropDownList)GRRP.Rows[i].FindControl("ddlcostcenter")).SelectedValue : (object)DBNull.Value;
                        drCurrentRow["jlhdebit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text;
                        drCurrentRow["jlhkredit"] = ((TextBox)GRRP.Rows[i].FindControl("jlhkredit")).Text;


                        dtRP.Rows.Add(drCurrentRow);

                        j++;
                    }
                }

                if (dgcount == 1)
                {
                    DataRow drdnew = dtRP.NewRow();

                    drdnew["nomor"] = 1;
                    drdnew["KodeGL"] = string.Empty;
                    drdnew["NamaGL"] = string.Empty;
                    drdnew["Description"] = string.Empty;
                    drdnew["Costcenter"] = string.Empty;
                    drdnew["jlhdebit"] = decimal.Zero;
                    drdnew["jlhkredit"] = decimal.Zero;
                    dtRP.Rows.Add(drdnew);
                }


                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

                cssGridRP();
                subgridGL_Click();

                break;
        }
    }
    //protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
    //        Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //        Params[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_GetCoa", Params, "constrmf");
    //        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
    //        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



    //        DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[2];
    //        xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
    //        xParams[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //        gex.ListDataset = gbx.GetDataJustOutPut("dbo.GL_GetCoacode", xParams, "constrmf");
    //        gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
    //        gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");


    //        ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
    //        ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;


    //        DefaultClass.Global.SqlHelper[] xParamt = new DefaultClass.Global.SqlHelper[3];
    //        xParamt[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 10, ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value, typeof(String));
    //        xParamt[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
    //        xParamt[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
    //        ge.ListDataset = gb.GetDataJustOutPut("dbo.GL_SGetCostcenter", xParamt, "constrmf");
    //        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");

    //        if (ge.ListDataset.Tables[0].Rows.Count > 0)
    //        {
    //            //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "", "");
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataTextField = "Text";
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataValueField = "Value";
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).DataBind();

    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).Enabled = true;
    //        }
    //        else
    //        {
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).Items.Insert(0, new ListItem("-", "-"));
    //            ((DropDownList)e.Row.FindControl("ddlcostcenter")).Enabled = false;
    //        }

    //         ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;



    //        if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
    //        {
    //            ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

    //        }

    //        GRRP.UseAccessibleHeader = true;
    //        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

    //    }
    //}

    protected void ddlJurnal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Jur = ddlJurnal.SelectedValue;
        //TEXTBOX AUTO CURRENCY
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@kodejurnal", ParameterDirection.Input, SqlDbType.VarChar, 30, Jur, typeof(string));
        varT[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIClJURNAL", typeof(string));
        varT[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varT[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varT, "constrmf");

        string KetJurnal = ge.ListDataset.Tables[0].Rows[0]["KetJurnal"].ToString();
        string DebitCC = ge.ListDataset.Tables[0].Rows[0]["DebitCC"].ToString();
        string DebitGL = ge.ListDataset.Tables[0].Rows[0]["DebitGL"].ToString();
        string KreditCC = ge.ListDataset.Tables[0].Rows[0]["KreditCC"].ToString();
        string KreditGL = ge.ListDataset.Tables[0].Rows[0]["KreditGL"].ToString();
        txtJurnal.Text = KetJurnal;
        txtDebit1.Text = DebitGL;
        txtDebit2.Text = DebitCC;
        txtKredit1.Text = KreditGL;
        txtKredit2.Text = KreditCC;

        //GET KETERANGAN GL
        DefaultClass.Global.SqlHelper[] varGL = new DefaultClass.Global.SqlHelper[5];
        varGL[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, DebitGL, typeof(string));
        varGL[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
        varGL[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varGL[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varGL[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varGL, "constrmf");
        string DCC = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        txtDebit3.Text = DCC;

        //GET KETERANGAN GL
        DefaultClass.Global.SqlHelper[] varCC = new DefaultClass.Global.SqlHelper[5];
        varCC[0] = gb.CreateSPParameter("@kodegl", ParameterDirection.Input, SqlDbType.VarChar, 30, KreditGL, typeof(string));
        varCC[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIGLMST", typeof(string));
        varCC[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varCC[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varCC[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", varCC, "constrmf");
        string KGL = ge.ListDataset.Tables[0].Rows[0]["NamaGL"].ToString();
        txtKredit3.Text = KGL;
    }

    protected void ddlkursSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkursstd.Text = ddlkursSup.SelectedValue;
    }


    protected void XXXTxt01_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (decimal.Parse(((TextBox)grow.FindControl("jlhdebit")).Text) > 0)
        {
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
        }
        else
        {
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
        }
        subgridGL_Click();
        GRRP.UseAccessibleHeader = true;
        GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void XXXTxt02_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        if (decimal.Parse(((TextBox)grow.FindControl("jlhkredit")).Text) > 0)
        {
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = false;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = true;
        }
        else
        {
            ((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            ((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
        }

    }

    private void subgridGL_Click()
    {
        int dgcount = GRRP.Rows.Count;
        decimal sumjlhgl = 0;
        decimal jlhgl = 0;
        if (dgcount > 0)
        {
            for (int i = 0; i < dgcount; i++)
            {
                //DataRow dr = dtRP.NewRow();

                jlhgl = decimal.Parse(((TextBox)GRRP.Rows[i].FindControl("jlhdebit")).Text);
                sumjlhgl = sumjlhgl + jlhgl;
            }
        }
        else
        {
           sumjlhgl =0;
        }
        string formatted = string.Format("{0:N2}", sumjlhgl);
        txtjmlhGL.Text = formatted;
    }

    protected void btnnext1_Click(object sender, EventArgs e)
    {
        Pnlfooter1.Visible = false;
        //pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = false;
        Pnlcustomer.Visible = false;
        //PnlUangmukaAP.Visible = true;
        //Pnlfooter2.Visible = true;
        PnlGrid.Visible = true;
        Pnlfooter4.Visible = true;
        //Pnlfooter3.Visible = true;
        //pnltrn.Visible = true;
        btnSimpan.Visible = true;
        btnclose.Visible = false;
        if (ddltypejur.SelectedValue == "2")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine


            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            PnlGrid.Visible = true;
            pnltypejurnal.Visible = false;

            lnkAddRP2_Click();

        }
        else if (ddltypejur.SelectedValue == "1")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine
            pnltypejurnal.Visible = true;
            PnlGrid.Visible = false;

            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }



    }
    protected void btnnext2_Click(object sender, EventArgs e)
    {



        Pnlfooter1.Visible = false;
        Pnlfooter4.Visible = true;
        btnSimpan.Visible = false;
        btndelete.Visible = true;
        btnback4.Visible = false;
        btnclose.Visible = true;

        //Pnlfooter3.Visible = true;
        //pnltrn.Visible = true;



        //lnkAddRP2_Click();
        if (ddltypejur.SelectedValue == "3")
        {


            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            Pnlfooter3.Visible = false;
            Pnlfooter4.Visible = true;
        }




    }
    protected void btnback2_Click(object sender, EventArgs e)
    {



        Pnlfooter1.Visible = true;
        //pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;

 
        Pnlfooter2.Visible = false;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


    }
    protected void btnnext3_Click(object sender, EventArgs e)
    {



        Pnlfooter1.Visible = false;
        //pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = false;

        Pnlfooter2.Visible = false;

        //Pnlfooter3.Visible = false;
        //pnltrn.Visible = false;

        Pnlfooter4.Visible = true;



        Pnlfooter3.Visible = false;
        //pnltrn.Visible = false;

        if (ddltypejur.SelectedValue == "2")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine


            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            PnlGrid.Visible = true;
            pnltypejurnal.Visible = false;

            lnkAddRP2_Click();

        }
        else if (ddltypejur.SelectedValue == "1")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine
            pnltypejurnal.Visible = true;
            PnlGrid.Visible = false;

            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }

    }
    protected void btnback3_Click(object sender, EventArgs e)
    {
        Pnlfooter1.Visible = true;
        //pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;

 
        Pnlfooter2.Visible = false;


        Pnlfooter3.Visible = false;
        //pnltrn.Visible = false;


        Pnlfooter4.Visible = false;


        if (ddltypejur.SelectedValue == "2")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine


            if (txtnobukti.Text.Length >0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            PnlGrid.Visible = false;
            pnltypejurnal.Visible = false;

           // lnkAddRP2_Click();

        }
        else if (ddltypejur.SelectedValue == "1")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine
            //pnltrn.Visible = false;
            PnlGrid.Visible = false;

            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }




    }
    protected void btnback4_Click(object sender, EventArgs e)
    {



        Pnlfooter1.Visible = true;
        //pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;
        Pnlfooter2.Visible = false;

        Pnlfooter3.Visible = false;
        //pnltrn.Visible = true;


        Pnlfooter4.Visible = false;


        if (ddltypejur.SelectedValue == "2")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine


            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
            PnlGrid.Visible = false;
            pnltypejurnal.Visible = false;

            // lnkAddRP2_Click();

        }
        else if (ddltypejur.SelectedValue == "1")
        {
            //ddlJurnal Jenis Pembayaran dan MultiLine
            pnltypejurnal.Visible = false;
            PnlGrid.Visible = false;

            if (txtnobukti.Text.Length > 0)
            {
                btnSimpan.Visible = false;
                btndelete.Visible = true;
            }
        }




    }

    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        decimal bayar;
        bayar = decimal.Parse(txttransAP.Text);
        txtjmlhCL.Text = string.Format("{0:N2}", bayar);
        txtjmlhGL.Text = string.Format("{0:N2}", bayar);
    }
    protected void txtjmlpinjaman_change(object sender, EventArgs e)
    {
        try
         {
            decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            decimal batas = decimal.Parse(txtlimitkasbon.Text.ToString());
            decimal angsuran = decimal.Parse(txtangsuran.Text.ToString());
            if (pinjaman > batas)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Jumlah Pinjaman tidak Boleh Lebih dari Batas Pinjaman');", true);
                txtjmlpinjaman.Focus();
                txtjmlpinjaman.Text = "0";
            }
            else if (pinjaman != 0 && angsuran != 0 )
            {
                txtangsuranperbulan.Text = string.Format("{0:N2}",decimal.Parse(txtjmlpinjaman.Text) / decimal.Parse(txtangsuran.Text));
                btnGenerate_Click(sender, e);
            }
            else if (pinjaman <= batas)
            {
                txtangsuran.Enabled = true;
                txtangsuran.Focus();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Angsuran tidak Boleh Nol atau Kosong');", true);
            }
            
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void txtangsuran_change(object sender, EventArgs e)
    {
        try
        {
            decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            decimal batas = decimal.Parse(txtbataspinjaman.Text.ToString());
            decimal angsuran = decimal.Parse(txtangsuran.Text.ToString());

            if (txtangsuran.Text == "0" || txtangsuran.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Angsuran tidak Boleh Nol atau Kosong');", true);
                txtangsuran.Focus();
            }
            else if (pinjaman == 0 || txtjmlpinjaman.Text == "" )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Pinjaman tidak Boleh Nol atau Kosong');", true);
                txtjmlpinjaman.Focus();
            }
            else if (pinjaman > batas)
            {
                txtjmlpinjaman.Text = "0";
                txtjmlpinjaman.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Jumlah Pinjaman tidak Boleh Lebih dari Batas Pinjaman');", true);
            }
            else if (pinjaman != 0 && pinjaman != 0)
            {
                txtangsuranperbulan.Text = string.Format("{0:N2}", (pinjaman / angsuran));
                btnGenerate_Click(sender, e);
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            decimal pinjaman = decimal.Parse(txtjmlpinjaman.Text.ToString());
            if (txtnote.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Keterangan tidak Lebih Kosong');", true);
            }
            else if (txtangsuran.Text == "0" || txtangsuran.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Angsuran tidak Boleh Nol atau Kosong');", true);
                txtangsuran.Focus();
            }
            else if (pinjaman == 0 || txtjmlpinjaman.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Pinjaman tidak Boleh Nol atau Kosong');", true);
                txtjmlpinjaman.Focus();
            }
            else
            {
                Pnlfooter4.Visible = true;
                PnlGrid.Visible = true;
                DataRow dr = null;

                dtRP.Columns.Add(new DataColumn("nomor", typeof(string)));
                dtRP.Columns.Add(new DataColumn("tgljthtempo", typeof(string)));
                dtRP.Columns.Add(new DataColumn("angske", typeof(string)));
                dtRP.Columns.Add(new DataColumn("angsuran", typeof(decimal)));
                dtRP.Columns.Add(new DataColumn("keterangan", typeof(string)));
                dtRP.Columns.Add(new DataColumn("bulan", typeof(int)));
                dtRP.Columns.Add(new DataColumn("tahun", typeof(int)));

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodekaryawan.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKARYAWAN", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var, "constrmf");

                string nomorrekening = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();

                for (int i = 0; i < int.Parse(txtangsuran.Text); i++)
                {
                    dr = dtRP.NewRow();
                    string ambilbulan = Convert.ToString(Convert.ToDateTime(txttgltrn.Text).ToString("MM"));
                    string rubahtahun = Convert.ToString(Convert.ToDateTime(txttgltrn.Text).ToString("yyyy"));
                    int ambiltahun = int.Parse(rubahtahun);
                    int rubahbulan = int.Parse(ambilbulan) + (i + 0) + 1;
                    int bulanPeriode = 0; 
                    int tahunPeriode = 0;
                    string rubahtanggal = "";
                    string datatanggal;
                    DateTime tanggalrubah;
                    if (rubahbulan > 12)
                    {
                        rubahtanggal = "01" + "-" + Convert.ToString(Convert.ToInt32(rubahbulan - 12)) + "-" + Convert.ToString(Convert.ToInt32(ambiltahun + 1));
                        tanggalrubah = Convert.ToDateTime(rubahtanggal);
                        datatanggal = tanggalrubah.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        rubahtanggal = "01" + "-" + Convert.ToString(rubahbulan) + "-" + Convert.ToString(ambiltahun);
                        tanggalrubah = Convert.ToDateTime(rubahtanggal);
                        datatanggal = tanggalrubah.ToString("dd/MM/yyyy");
                    }
                    bulanPeriode = int.Parse(Convert.ToDateTime(datatanggal).ToString("MM")); 
                    tahunPeriode = int.Parse(Convert.ToDateTime(datatanggal).ToString("yyyy"));
                    dr["nomor"] = nomorrekening;
                    dr["tgljthtempo"] = datatanggal;
                    dr["angske"] = i + 1;
                    dr["angsuran"] = Convert.ToDecimal(txtangsuranperbulan.Text);
                    dr["keterangan"] = txtnote.Text;
                    dr["bulan"] = bulanPeriode;
                    dr["tahun"] = tahunPeriode;
                    dtRP.Rows.Add(dr);
                }

                ViewState["ListDetailRP"] = dtRP;

                GRRP.DataSource = dtRP;
                GRRP.DataBind();

                GRRP.UseAccessibleHeader = true;
                GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;
                //subgridGL_Click();
                cssGridRP();
                btnSimpan.Enabled = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            var kodekry = hdnkaryawan.Value;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, kodekry, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIAEMSTBARU", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var, "constrmf");
            //Boolean status = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["stl"].ToString());
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                rbpotong.Enabled = true;
                rbpotong.SelectedValue = "1";
                txtkodekaryawan.Text = kodekry;
                txtnamakaryawan.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                txtbataspinjaman.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString())); 
                txttgljoin.Text = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglTerima"].ToString()).ToString("dd-MM-yyyy");
                txtmasakerja.Text = ge.ListDataset.Tables[0].Rows[0]["masakerja"].ToString();
                ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
                ddlbag.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();
                ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
                ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
                txtnetgaji.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["JlhNetGaji"].ToString()));
                txtsaldobelumlunas.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));
                txtlimitkasbon.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Plafonkasbon"].ToString()) - decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["blmlunas"].ToString()));
                txtangsuranperbulan.Text = string.Format("{0:N2}", decimal.Parse("0"));
                pnladd.Visible = true;
                //Pnlfooter4.Visible = true;
                txtangsuran.Text = "0";
                txtjmlpinjaman.Text = "0";
                btnSimpan.Enabled = true;
                txtangsuran.Enabled = false;
                txtnote.Text = "Kasbon Karyawan";
            }
            



        
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

            if (txtmemopusatapprove.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Request Masih Kosong!');", true);
            }
            else
            {




                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, hdncabang.Value, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "APPROVE", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[5] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtmemopusatapprove.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@potgaji", ParameterDirection.Input, SqlDbType.Int, 0, rbpotong.SelectedValue, typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("AE_MEMO_KARYAWAN_KASBON_APPROVE", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Approve Done!','Approve Pengajuan Sukses','top-right','success','2');", true);
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
    protected void btnTolak_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";

             if (txtmemopusatapprove.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Catatan Request Masih Kosong!');", true);
            }
            else
            {




                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, hdncabang.Value, typeof(string));
                var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TOLAK", typeof(string));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[4] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[5] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@notememo", ParameterDirection.Input, SqlDbType.VarChar, 100, txtmemopusatapprove.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@potgaji", ParameterDirection.Input, SqlDbType.Int, 0, rbpotong.SelectedValue, typeof(int));
                ge.ListDataset = gb.GetDataJustOutPut("AE_MEMO_KARYAWAN_KASBON_APPROVE", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Tolak Done!','Tolak Pengajuan Sukses','top-right','success','2');", true);
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

    [WebMethod]
    public static string GetKaryawan(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
        Params[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));
        Params[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARINIK", typeof(string));
        Params[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_SPINJAMANKARYAWAN", Params, "constrmf");
        
        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);
        return json;
    }






}
