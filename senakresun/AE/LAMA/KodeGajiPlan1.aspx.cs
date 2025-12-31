using DefaultClass;
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

public partial class AE_Master_KodeGajiPlan1 : System.Web.UI.Page
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
    int btnadd;

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
      
        //Panel2.Visible = true;
        PnlFront.Visible = true;
        Pnlfooter.Visible = true;
        PnlFrontx.Visible = false;
        PnlFront.Visible = true;
        //pnladd.Visible = true;

        pnlHeader.Visible = false;
        //btnsearch.Visible = true;
        //txtsearch.Visible = true;
        //btndelete.Visible = false;
        txtnik.Text = "";
        txtnmkyn.Text = "";
        btnadd = 1;
        //ddllvl.Text = "";
        ddloff.SelectedValue = SessBranchID;
        ddlbag.SelectedValue = "-";
        ddlgen.SelectedValue = "L";
      
        ddljab.SelectedValue = "-";

        ddlsttskyn.SelectedValue = "-";
        hdndata.Text = "1";
        //txtptgSPSI.Text = "";
        //ddlabsensi.SelectedValue = "-";
        //ddlcaraHtglbr.SelectedValue = "";
        //txtkurslembur.Text = "0,00";
        //txtmemo.Text = "-"; 

        //chkstatuswo.

        txtnik.ReadOnly = false;
        txtnik.Focus();

        //btndelete.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }


    protected void btnSearch_Click(string search)
    {
        //var search = "";
        DateTime now;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, search, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");

        txtnik.Text = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString();
        txtnmkyn.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
      

        //now = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["TglMasukJst"].ToString());
        ddlmarketing.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["marketing"].ToString();
              ddloff.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["kodecabang"].ToString();
       
        ddlgen.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["JenisKel"].ToString();
        ddljab.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["Jabatan"].ToString();
        //ddlgol.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
        ddlsttskyn.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["StatusKry"].ToString();
           
      
        ddlunitbisnis.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeDept"].ToString();
        //txtptgSPSI.Text = ge.ListDataset.Tables[0].Rows[0]["JlhPot1"].ToString();
        //ddlabsensi.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["LemburEntry"].ToString();
        //ddlcaraHtglbr.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["HitungLembur"].ToString();
        //txtkurslembur.Text = ge.ListDataset.Tables[0].Rows[0]["KursLembur"].ToString();
        //txtmemo.Text = ge.ListDataset.Tables[0].Rows[0]["Memo"].ToString();
       

        var bagian = ge.ListDataset.Tables[0].Rows[0]["KodeBagian"].ToString();



        var nnaktif = ge.ListDataset.Tables[0].Rows[0]["NonAktifFlag"].ToString();
      
        var bosid = ge.ListDataset.Tables[0].Rows[0]["bos_id"].ToString();

        if (nnaktif == "True")
        {
            cbxnonaktf.Checked = false;
        }
        else
        {
            cbxnonaktf.Checked = true;
        }

        var statuswo = ge.ListDataset.Tables[0].Rows[0]["status_wo"].ToString();

        if (statuswo == "1")
        {
            chkstatuswo.Checked = true;
        }
        else
        {
            chkstatuswo.Checked = false;
        }

        




        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBAGIAN", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

        ddlbag.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbag.DataTextField = "kdket";
        ddlbag.DataValueField = "KodeBagian";
        ddlbag.DataBind();
        ddlbag.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddlbag.SelectedValue = bagian;

        if (ddlmarketing.SelectedValue == "2")
        {
            DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[4];
            varx[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBOSID", typeof(string));
            varx[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddloff.SelectedValue, typeof(string));
            varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            varx[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", varx, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

            ddlbosid.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlbosid.DataTextField = "TEXT";
            ddlbosid.DataValueField = "VALUE";
            ddlbosid.DataBind();
            ddlbosid.Items.Insert(0, new ListItem("Pilih Data", "-"));
            divbos.Visible = true;
            ddlbosid.SelectedValue = bosid;
        }
        else
        {
            divbos.Visible = false;
        }

    }
    protected void btnbackheader_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.AbsoluteUri);
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
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGRID", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntitydata()
    {



       

        //DROPDOWNLIST TYPE PENDIDIKAN
        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
        var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "STATUSKARY", typeof(string));
        var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var3, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

        ddlsttskyn.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlsttskyn.DataTextField = "TEXT";
        ddlsttskyn.DataValueField = "VALUE";
        ddlsttskyn.DataBind();
        ddlsttskyn.Items.Insert(0, new ListItem("Pilih Data", "-"));

        

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
        //ddlunitbisnis.Items.Insert(0, new ListItem("Pilih Data", "-"));

        


        //DROPDOWNLIST TYPE KANTOR
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKANTOR", typeof(string));
        var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

        ddloff.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddloff.DataTextField = "kdnm";
        ddloff.DataValueField = "KodeCabang";
        ddloff.DataBind();
        ddloff.Items.Insert(0, new ListItem("Pilih Data", "-"));
        ddloff.SelectedValue = SessBranchID;
        ddloff.Enabled = false;
        //DROPDOWNLIST TYPE BAGIAN


      


        //DROPDOWNLIST TYPE JABATAN
        DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[2];
        var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETJABATAN", typeof(string));
        var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var4, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeJab asc";

        ddljab.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddljab.DataTextField = "kdket";
        ddljab.DataValueField = "KodeJab";
        ddljab.DataBind();
        ddljab.Items.Insert(0, new ListItem("Pilih Data", "-"));



        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBAGIAN", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

        ddlbag.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbag.DataTextField = "kdket";
        ddlbag.DataValueField = "KodeBagian";
        ddlbag.DataBind();
        ddlbag.Items.Insert(0, new ListItem("Pilih Data", "-"));





    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {
                    PnlFrontx.Visible = false;
                    PnlFront.Visible = true;
                    //pnladd.Visible = true;
                    Pnlfooter.Visible = true;
                    pnlHeader.Visible = false;
                  

                    btnSearch_Click(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNik")).Text);
                    txtnik.ReadOnly = true;
                    Pnlfooter.Visible = true;
                    hdndata.Text = "0";
                    //btndelete.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


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
                    string nikx = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNik")).Text;

                    DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[3];
                    x[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, nikx, typeof(string));
                    x[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKSALES", typeof(string));
                    x[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", x, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        btnDelete_Click(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNik")).Text, "-", ddlmarketing.SelectedValue);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data sudah ada pada transaksi tidak dapat dihapus!');", true);

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
        string XSTATE = "";
        try
        {
            var ckbx = "";
            var ckbx1 = "";
            var ckbt = "";
            if (cbxnonaktf.Checked == true)
            {
                ckbx = "0";
            }
            else
            {
                ckbx = "1";
            }

            if (chkstatuswo.Checked == true)
            {
                ckbt = "1";
            }
            else
            {
                ckbt = "0";
            }
            //if (cbxpph.Checked == true)
            //{
            //    ckbx1 = "1";
            //}
            //else
            //{
            //    ckbx1 = "0";
            //}
            if (txtnik.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode AO Masih kosong, cek kembali');", true);
            }
            if (ddlunitbisnis.SelectedValue=="-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Unit Bisnis Masih Kosong!');", true);
            }
              else if(ddlbag.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bagian Masih Kosong!');", true);
            }
            else if (ddljab.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jabatan Masih Kosong!');", true);
            }
            else if (ddlsttskyn.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Status Karyawan Masih Kosong!');", true);
            }
            else
            {


              

            if (hdndata.Text == "1")
            {
                      STATE = "INSERT";
                    // txttglout.Text = txttgljoin.Text;



                    string nikx = txtnik.Text;

                    DefaultClass.Global.SqlHelper[] x = new DefaultClass.Global.SqlHelper[5];
                    x[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, nikx, typeof(string));
                    x[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                    x[2] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
                    x[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                    x[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", x, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                    {
                        XSTATE = "0";

                    }
                    else
                    {
                        XSTATE = "1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode AO sudah ada cek kembali!');", true);

                    }


                }
            else
                {
                STATE = "UPDATE";
                    XSTATE = "0";

                }
                if  (XSTATE == "0")
                {

                
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[58];
            var[0] = gb.CreateSPParameter("@txtnik", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnik.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@txtnmkyn", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnmkyn.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@txtnpwpkyn", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var[3] = gb.CreateSPParameter("@txtnobpjs", ParameterDirection.Input, SqlDbType.VarChar, 15, "0", typeof(string));
            var[4] = gb.CreateSPParameter("@txttglbpjs", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, DateTime.Now, typeof(DateTime));
            var[5] = gb.CreateSPParameter("@ddlpddk", ParameterDirection.Input, SqlDbType.VarChar, 12, "S1", typeof(string));
            var[6] = gb.CreateSPParameter("@ddltngkrj", ParameterDirection.Input, SqlDbType.VarChar, 10, "WNI", typeof(string));
            var[7] = gb.CreateSPParameter("@ddloff", ParameterDirection.Input, SqlDbType.VarChar, 10, ddloff.SelectedValue, typeof(string));
            var[8] = gb.CreateSPParameter("@ddlbag", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbag.SelectedValue, typeof(string));
            var[9] = gb.CreateSPParameter("@ddlgen", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlgen.SelectedValue, typeof(string));
            var[10] = gb.CreateSPParameter("@ddlsttptkp", ParameterDirection.Input, SqlDbType.VarChar, 40, "K/0", typeof(string));
            var[11] = gb.CreateSPParameter("@ddljab", ParameterDirection.Input, SqlDbType.VarChar, 18, ddljab.SelectedValue, typeof(string));
            var[12] = gb.CreateSPParameter("@ddlgol", ParameterDirection.Input, SqlDbType.VarChar, 5, "00001", typeof(string));
            var[13] = gb.CreateSPParameter("@ddlsttskyn", ParameterDirection.Input, SqlDbType.VarChar, 12, ddlsttskyn.SelectedValue, typeof(string));
            var[14] = gb.CreateSPParameter("@ddlsysbyr", ParameterDirection.Input, SqlDbType.VarChar, 20, "Bulanan", typeof(string));
            var[15] = gb.CreateSPParameter("@ddlkdmkn", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
            var[16] = gb.CreateSPParameter("@ddlkdtrp", ParameterDirection.Input, SqlDbType.VarChar, 5, "-", typeof(string));
            var[17] = gb.CreateSPParameter("@txttgljoin", ParameterDirection.Input, SqlDbType.SmallDateTime, 5, DateTime.Now, typeof(DateTime));
            var[18] = gb.CreateSPParameter("@txttglout", ParameterDirection.Input, SqlDbType.SmallDateTime, 5, DateTime.Now, typeof(DateTime));
            var[19] = gb.CreateSPParameter("@ddlbyr", ParameterDirection.Input, SqlDbType.VarChar, 40, "C", typeof(string));
            var[20] = gb.CreateSPParameter("@txtnmbank", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            var[21] = gb.CreateSPParameter("@ddlrekpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, "1111111111", typeof(string));
            var[22] = gb.CreateSPParameter("@txtnmbakpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            var[23] = gb.CreateSPParameter("@txtcbgbank", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
            var[24] = gb.CreateSPParameter("@txtrekkyn", ParameterDirection.Input, SqlDbType.VarChar, 20, "0", typeof(string));
            var[25] = gb.CreateSPParameter("@txtplafonpnjm", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[26] = gb.CreateSPParameter("@txtplafonct", ParameterDirection.Input, SqlDbType.Float, 5, "0", typeof(string));
            var[27] = gb.CreateSPParameter("@txttunjplaf", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[28] = gb.CreateSPParameter("@txtgapok", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[29] = gb.CreateSPParameter("@txttutep", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[30] = gb.CreateSPParameter("@txttujab", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[31] = gb.CreateSPParameter("@txttuabdi", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[32] = gb.CreateSPParameter("@txttutrp", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[33] = gb.CreateSPParameter("@txttunj1", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[34] = gb.CreateSPParameter("@txttunj2", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[35] = gb.CreateSPParameter("@txttunj3", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[36] = gb.CreateSPParameter("@txttunj4", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[37] = gb.CreateSPParameter("@txttunj5", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[38] = gb.CreateSPParameter("@txttunjhrRaya", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[39] = gb.CreateSPParameter("@txtmemo", ParameterDirection.Input, SqlDbType.VarChar, 150, "-", typeof(string));
            var[40] = gb.CreateSPParameter("@txtptgpjk", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[41] = gb.CreateSPParameter("@KetTunj1", ParameterDirection.Input, SqlDbType.VarChar, 50, "R", typeof(string));
            var[42] = gb.CreateSPParameter("@KetTunj2", ParameterDirection.Input, SqlDbType.VarChar, 50, "R", typeof(string));
            var[43] = gb.CreateSPParameter("@KetTunj3", ParameterDirection.Input, SqlDbType.VarChar, 50, "R", typeof(string));
            var[44] = gb.CreateSPParameter("@KetTunj4", ParameterDirection.Input, SqlDbType.VarChar, 50, "R", typeof(string));
            var[45] = gb.CreateSPParameter("@KetTunj5", ParameterDirection.Input, SqlDbType.VarChar, 50, "R", typeof(string));
            var[46] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.VarChar, 50, ckbx, typeof(string));
            var[47] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 5, SessKdpersh, typeof(string));
            var[48] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 5, SessBranchID, typeof(string));
            var[49] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[50] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[51] = gb.CreateSPParameter("@kodedept", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));
            var[52] = gb.CreateSPParameter("@BiayaRek", ParameterDirection.Input, SqlDbType.Decimal, 50, "0", typeof(decimal));
            var[53] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
            var[54] = gb.CreateSPParameter("@pembeli", ParameterDirection.Input, SqlDbType.VarChar, 50, "1", typeof(string));
            var[55] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlmarketing.SelectedValue, typeof(string));
                var[56] = gb.CreateSPParameter("@bos_id", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbosid.SelectedValue, typeof(string));
                    var[57] = gb.CreateSPParameter("@NonAktifWO", ParameterDirection.Input, SqlDbType.VarChar, 50, ckbt, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_INSERT", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Duplicate"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Gaji " + txtnik.Text + " already Exists!');", true);
                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Save this Project','top-right','success','2');", true);
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
    protected void ddlmarketing_OnChange(object sender, EventArgs e)
    {
        try
        {
            //string asdas = ddloff.SelectedValue;
            if (ddlmarketing.SelectedValue == "2")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBOSID", typeof(string));
                var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddloff.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "TEXT asc";

                ddlbosid.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlbosid.DataTextField = "TEXT";
                ddlbosid.DataValueField = "VALUE";
                ddlbosid.DataBind();
                ddlbosid.Items.Insert(0, new ListItem("Pilih Data", "-"));

                divbos.Visible = true;
            }
            else
            {
                divbos.Visible = false;
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
   
    protected void btnDelete_Click(string nik, string pembeli, string marketing)
    {
        try
        {
            var del = nik;
        var pembelix = pembeli;
        var marketingx = marketing;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kdnik", ParameterDirection.Input, SqlDbType.VarChar, 50, del, typeof(string));
        var[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[2] = gb.CreateSPParameter("@pembeli", ParameterDirection.Input, SqlDbType.VarChar, 50, pembelix, typeof(string));
        var[3] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, marketingx, typeof(string));
        var[4] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_DELETE", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Ada"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningData('Data sudah ada pada transaksi tidak dapat dihapus!');", true);
        }
        else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);
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


 

    protected void ddlunitbisnis_SelectedIndexChanged(object sender, EventArgs e)
    {

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBAGIAN", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlunitbisnis.SelectedValue, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

        ddlbag.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlbag.DataTextField = "kdket";
        ddlbag.DataValueField = "KodeBagian";
        ddlbag.DataBind();
        ddlbag.Items.Insert(0, new ListItem("Pilih Data", "-"));


    }
   
}




