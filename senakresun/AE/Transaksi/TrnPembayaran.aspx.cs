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
public partial class AE_Process_Pembayaran : System.Web.UI.Page
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
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A2";
    string kodesumber = "CL";
    string KodeKonter = "CL02";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        
        btndelete.Visible = false;
        btnSimpan.Visible = true;
        txtnobukti.Text = "";
        pnladd.Visible = true;
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
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                //get tanggal server
                DateTime xtanggal = DateTime.Now;
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text;

                //get tanggal server

                //KODE LKaryawan
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
                var3[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAPINJAMAN", typeof(string));
                var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";
                ddlkaryawan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkaryawan.DataTextField = "KodeNm";
                ddlkaryawan.DataValueField = "KodeNik";
                ddlkaryawan.DataBind();
                ddlkaryawan.Items.Insert(0, new ListItem("Pilih Data", "-"));

            }


            //pwcabang system kurs single default IDR kurs 1
            if (SessSystemKurs == "False")
            {
                ddlkursSup.Items.Insert(0, new ListItem("IDR"));
                ddlkursSup.SelectedValue = "IDR";
                ddlkursSup.Enabled = false;
                txtkursstd.Text = "1";
                txtkursstd.Enabled = false;

            }
            else
            {
                ddlkursSup.Items.Insert(0, new ListItem("IDR"));
                ddlkursSup.SelectedValue = "IDR";
                ddlkursSup.Enabled = true;
                txtkursstd.Text = "1";
                txtkursstd.Enabled = true;

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

    protected void ddlkursBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPEMBAYARAN", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeNik asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    string nik = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;
                    DefaultClass.Global.SqlHelper[] varC = new DefaultClass.Global.SqlHelper[6];
                    varC[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    varC[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    varC[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKDELETE", typeof(string));
                    varC[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varC[4] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, nik, typeof(string));
                    varC[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", varC, "constrmf");
                    string ceklunas = ge.ListDataset.Tables[0].Rows[0]["StatusLunas"].ToString();
                    if (ceklunas == "False")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:peringatan('Anda Sudah Mempunyai Peminjaman Yang Belum Lunas, Pinjaman tidak boleh lebih dari 1 ..!!');", true);
                    }
                    else if(ceklunas == "True")
                    {
                        PnlFront.Visible = false;
                        pnladd.Visible = true;

                        pnlHeader.Visible = false;
                        blnadd = 1;

                        DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[6];
                        var3[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var3[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var3[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNIKNOBUKTI", typeof(string));
                        var3[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var3[4] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, nik, typeof(string));
                        var3[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var3, "constrmf");
                        ddlkaryawan.Items.Insert(0, new ListItem(ge.ListDataset.Tables[0].Rows[0]["ddl"].ToString(), ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString()));
                        ddlkaryawan.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeNik"].ToString();
                        ddlkaryawan.Enabled = false;
                        txttgltrn.Text = (Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglJthtempo"].ToString())).ToString("dd-MM-yyyy");
                        txtnamakar.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
                        txttgljthtmp.Text = (Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglJthtempo"].ToString())).ToString("dd-MM-yyyy");
                        txtnobukti.Text = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
                        txtangsuran.Text = string.Format("{0:N2}", Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["JlhAngsuran"].ToString()));
                        txtbagian.Text = ge.ListDataset.Tables[0].Rows[0]["KetBagian"].ToString();
                        txtrekening.Text = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
                        txtangske.Text = ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString();
                        txtsisaangske.Text = "0";
                        decimal sisaangske = Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["sisaangske"].ToString());
                        decimal jmlangs = Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["JlhAngsuran"].ToString());
                        txtsisajmlangs.Text = "0";
                        txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["Keterangan"].ToString();
                        string type = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString();
                        if (type == "True")
                        {
                            txttypepembayaran.Text = "Potong Gaji";
                            Pnlfooter4.Visible = true;
                            btndelete.Visible = false;
                            btnSimpan.Visible = true;
                        }
                        else if (type == "False")
                        {
                            txttypepembayaran.Text = "Pembayaran Cash";
                        }
                        txtKeterangan.Enabled = false;
                        txtkursstd.Enabled = false;
                        ddlkursSup.Enabled = false;
                        btndelete.Visible = true;
                        btnSimpan.Visible = false;
                    }
                   
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

              
                break;
        }
    }
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] varC = new DefaultClass.Global.SqlHelper[4];
            varC[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkaryawan.SelectedValue, typeof(string));
            varC[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            varC[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEPEMBAYARAN", typeof(string));
            varC[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_IUPINJAMANKARYAWAN", varC, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                btnback4.Visible = false;
                nobuktidb = txtnobukti.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','Data Berhasil Dihapus','bottom-center','success','2');", true);
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
        return Page.ClientScript.GetPostBackEventReference(btnback4, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[7];
            
            Params[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlkaryawan.SelectedValue, typeof(string));
            Params[1] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 15, txtnobukti.Text, typeof(string));
            Params[2] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKeterangan.Text, typeof(decimal));
            Params[3] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Money, 0, Convert.ToDecimal(txtsisajmlangs.Text), typeof(decimal));
            Params[4] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 15, SessLoginID, typeof(string));
            Params[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPEMBAYARAN", typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_IUPINJAMANKARYAWAN", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);
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

    protected void ddlkaryawan_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var bank = ddlkaryawan.SelectedValue;
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlkaryawan.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYNIK", typeof(string));
            var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_SPINJAMANKARYAWAN", var, "constrmf");
            txtnamakar.Text = ge.ListDataset.Tables[0].Rows[0]["NamaKry"].ToString();
            txttgljthtmp.Text = (Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[0]["TglJthtempo"].ToString())).ToString("dd-MM-yyyy");
            txtnobukti.Text = ge.ListDataset.Tables[0].Rows[0]["NoBukti"].ToString();
            txtangsuran.Text = string.Format("{0:N2}", Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["JlhAngsuran"].ToString()));
            txtbagian.Text = ge.ListDataset.Tables[0].Rows[0]["KetBagian"].ToString();
            txtrekening.Text = ge.ListDataset.Tables[0].Rows[0]["RekBank"].ToString();
            txtangske.Text = ge.ListDataset.Tables[0].Rows[0]["Angsuran"].ToString();
            txtsisaangske.Text = ge.ListDataset.Tables[0].Rows[0]["sisaangske"].ToString();
            decimal sisaangske = Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["sisaangske"].ToString());
            decimal jmlangs = Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[0]["JlhAngsuran"].ToString());
            txtsisajmlangs.Text = string.Format("{0:N2}", jmlangs * sisaangske);
            //ddlkursSup.SelectedValue = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
            string type = ge.ListDataset.Tables[0].Rows[0]["TypePembayaran"].ToString();
            if (type == "True")
            {
                txttypepembayaran.Text = "Potong Gaji";
                Pnlfooter4.Visible = true;
                btndelete.Visible = false;
                btnSimpan.Visible = true;
            }
            else if(type == "False")
            {
                txttypepembayaran.Text = "Pembayaran Cash";
            }

            txtkursstd.Text = "1";

            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void GetKurs_textchanged(object sender, EventArgs e)
    {

        //get kurs SUPLLIER for dropdown and textbox
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
            //txttgljthtmp.Text = txttgltrn.Text;
        }
        //txttgljthtmp.Text = txttgltrn.Text;
    }
    protected void ddlkursSup_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkursstd.Text = ddlkursSup.SelectedValue;
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
}
