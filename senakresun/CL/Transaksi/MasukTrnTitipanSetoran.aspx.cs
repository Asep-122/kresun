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
public partial class CL_Trn_Titipan_Setoran : System.Web.UI.Page
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
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    DataTable dtmasuk = new DataTable();
    ReportDocument rprt = new ReportDocument();
    //string form = "B9";
    string kodesumber = "CL";
    string KodeKonter = "CL01";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;

            btndelete.Visible = false;
            btnSimpan.Visible = true;
            txtnobukti.Text = "";
            pnladd.Visible = true;
            //Pnlfooter4.Visible = true;
            pnltrn.Visible = true;
            hapus();

            DataTable dt = new DataTable(); // Data table creation without any rows
            GRTP.DataSource = dt;
            GRTP.DataBind();
            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
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
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                //get tanggal server
                Global.SqlHelper[] varT = new Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MMMM-yyyy");

                nobuktiid.Visible = false;

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
        Global.SqlHelper[] var = new Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
 
        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Titipan_Setoran", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti desc";

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
                    string nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    Global.SqlHelper[] vart = new Global.SqlHelper[5];
                    vart[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, nobukti, typeof(string));
                    vart[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 30, "DELETE", typeof(string));
                    vart[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    vart[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));
                    vart[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Titipan_Setoran", vart, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Anggota','top-right','success','2');", true);
                        BindGridEntity();
                        GRData.UseAccessibleHeader = true;

                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
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
    protected void btndelete_click(object sender,EventArgs e)
    {
        try
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[11];

            Params[0] = gb.CreateSPParameter("@nobukti", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobukti.Text, typeof(string));
            Params[1] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
            Params[2] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnominal.Text, typeof(decimal));
            Params[3] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, hdncustomer.Value, typeof(string));
            Params[4] = gb.CreateSPParameter("@NoRekening", ParameterDirection.Input, SqlDbType.VarChar, 30, HDNoRekening.Value, typeof(string));
            Params[5] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
            Params[6] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
            Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
            Params[9] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[10] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, KodeKonter, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR_delete_Bayar_TARIKAN", Params, "constrmf");
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                btnSimpan.Visible = false;
                nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','2');", true);
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
    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //switch (e.CommandName)
        //{
        //    case "DEL":
        //        int dgcount = GRTP.Rows.Count;


        //        ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

        //        dtRP.Columns.Clear();

        //        dtRP = (DataTable)ViewState["ListDetailRTP"];

        //        DataRow drCurrentRow = null;

        //        dtRP.Rows.Clear();

        //        int j = 1;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {

        //            if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
        //            {


        //                drCurrentRow = dtRP.NewRow();

        //                drCurrentRow["nomor"] = j;
        //                drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
        //                drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
        //                drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
        //                drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
        //                drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
        //                drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
        //                drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

        //                dtRP.Rows.Add(drCurrentRow);

        //                j++;
        //            }
        //        }

        //        if (dgcount == 1)
        //        {
        //            DataRow drdnew = dtRP.NewRow();

        //            drdnew["nomor"] = 1;
        //            drdnew["TglTrnFaktur"] = string.Empty;
        //            drdnew["NoFaktur"] = string.Empty;
        //            drdnew["TypeTrn"] = string.Empty;
        //            drdnew["TglJthTmp"] = string.Empty;
        //            drdnew["Keterangan"] = string.Empty;
        //            drdnew["JumlahTrn"] = decimal.Zero;
        //            drdnew["KursStd"] = decimal.Zero;
        //            dtRP.Rows.Add(drdnew);
        //        }


        //        ViewState["ListDetailRTP"] = dtRP;

        //        GRTP.DataSource = dtRP;
        //        GRTP.DataBind();

        //        GRTP.UseAccessibleHeader = true;
        //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        cssGridBayar();



        //        break;


        //}
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            //invoce yg dibayarkan
            decimal jlhdibayar = decimal.Parse(txtnominal.Text);
            string statustransaksi;
            
            if(ddlTypeTrn.SelectedValue.ToString() == "A")
            {
                statustransaksi = "Pemasukan";
            }
            else
            {
                statustransaksi = "Pengeluaran";
            }

            if (jlhdibayar <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nominal Transfer Masih Kosong!');", true);
                txtnominal.Focus();
            }
            else if (txtnobuktimanual.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bukti Manual '"+ statustransaksi +"' Masih Kosong!');", true);
                txtnobuktimanual.Focus();
            }
            else if (txtKeterangan.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Simpanan Pokok tidak boleh diambil!');", true);
                txtKeterangan.Focus();
            }
            else
            {
                //gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                //gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnominal.Text, typeof(decimal));
                Global.SqlHelper[] Params = new Global.SqlHelper[12];
                Params[0] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnobukti.Text, typeof(string));
                Params[1] = gb.CreateSPParameter("@TypeTrn", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlTypeTrn.SelectedValue, typeof(string));
                Params[2] = gb.CreateSPParameter("@Tgltrnbank", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[3] = gb.CreateSPParameter("@nobuktimanual", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnobuktimanual.Text, typeof(string));
                Params[4] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtKeterangan.Text, typeof(string));
                Params[5] = gb.CreateSPParameter("@JumlahTrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnominal.Text, typeof(decimal));
                Params[6] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
                Params[7] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[8] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                Params[9] =  gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                Params[11] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 4, KodeKonter, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_Titipan_Setoran", Params, "constrmf");
                if (ge.ListDataset.Tables[1].Rows[0][0].ToString().Contains("Success"))
                {
                    ambiltitipan(ge.ListDataset.Tables[0].Rows[0]["masukno"].ToString());
                    hapus();
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
    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
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
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
        }
    }
    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {

        //decimal saldoawal;
        //saldoawal = decimal.Parse(txtkelipatan.Text) * decimal.Parse(txtsaldowajib.Text);
        //txtnominal.Text = string.Format("{0:N2}", saldoawal);
    }
    decimal nominal = 0, debet = 0, kredit = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            nominal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "nominal"));
            debet += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "debit"));
            kredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "kredit"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotalnomial1 = (Label)e.Row.FindControl("lbltotaldebit");
            lbltotalnomial1.Text = nominal == 0 ? string.Format("{0:N2}", nominal) : "0";
            Label lbltotaldebet = (Label)e.Row.FindControl("lbltotaldebit");
            lbltotaldebet.Text = debet == 0 ? string.Format("{0:N2}", debet) : "0";
            Label lbltotalkredit = (Label)e.Row.FindControl("lbltotalkredit");
            lbltotalkredit.Text = kredit == 0 ? string.Format("{0:N2}", kredit) : "0";
        }
    }
    private void hapus()
    {
        txtnobukti.Text = "";
        txtnobuktimanual.Text = "";
        txttgltrn.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtnominal.Text = "";
        txtKeterangan.Text = "";
        ddlTypeTrn.SelectedValue = "-";
    }
    #region FUNGSI

    private void ambiltitipan(string nobukti)
    {
        try
        {
            int dgcount = GRTP.Rows.Count;
            string ambilnama = "";
            string ambilpath = "";


            dtmasuk.Columns.Clear();
            //dtmasuk = (DataTable)ViewState["ListDetail"];

            dtmasuk.Columns.Add(new DataColumn("nobukti", typeof(string)));
            dtmasuk.Columns.Add(new DataColumn("nobuktimanual", typeof(string)));
            dtmasuk.Columns.Add(new DataColumn("tgltransfer", typeof(string)));
            dtmasuk.Columns.Add(new DataColumn("typetransaksi", typeof(string)));
            dtmasuk.Columns.Add(new DataColumn("keterangan", typeof(string)));
            dtmasuk.Columns.Add(new DataColumn("nominal", typeof(decimal)));
            dtmasuk.Columns.Add(new DataColumn("debit", typeof(decimal)));
            dtmasuk.Columns.Add(new DataColumn("kredit", typeof(decimal)));


            if (dgcount != 0)
            {
                for (int i = 0; i < dgcount; i++)
                {
                    DataRow drsatu = dtmasuk.NewRow();
                    drsatu["nobukti"] = ((Label)GRTP.Rows[i].FindControl("lblnobukti1")).Text; ;
                    drsatu["nobuktimanual"] = ((Label)GRTP.Rows[i].FindControl("lblnobuktimanual1")).Text;
                    drsatu["tgltransfer"] = ((Label)GRTP.Rows[i].FindControl("lbltglTrn1")).Text;
                    drsatu["typetransaksi"] = ((Label)GRTP.Rows[i].FindControl("lbltypetransaksi1")).Text;
                    drsatu["keterangan"] = ((Label)GRTP.Rows[i].FindControl("lblketerangan1")).Text;
                    drsatu["nominal"] = ((Label)GRTP.Rows[i].FindControl("lblNominal1")).Text;
                    drsatu["debit"] = ((Label)GRTP.Rows[i].FindControl("lbldebit")).Text;
                    drsatu["kredit"] = ((Label)GRTP.Rows[i].FindControl("lblkredit")).Text;
                    dtmasuk.Rows.Add(drsatu);
                }
            }

            DataRow drdnew = dtmasuk.NewRow();
            drdnew["nobukti"] = nobukti;
            drdnew["nobuktimanual"] = txtnobuktimanual.Text;
            drdnew["tgltransfer"] = DateTime.Parse(txttgltrn.Text).ToString("dd MMMM yyyy");
            drdnew["typetransaksi"] = ddlTypeTrn.SelectedValue;
            drdnew["keterangan"] = txtKeterangan.Text;
            drdnew["nominal"] = decimal.Parse(txtnominal.Text);
            drdnew["debit"] = decimal.Parse(ddlTypeTrn.SelectedValue == "A" ? txtnominal.Text : "0" );
            drdnew["kredit"] = decimal.Parse(ddlTypeTrn.SelectedValue == "B" ? txtnominal.Text : "0" );


            dtmasuk.Rows.Add(drdnew);

            //ViewState["ListDetailRP"] = dtmasuk;

            GRTP.DataSource = dtmasuk;
            GRTP.DataBind();
            //int asdas = GRTP.Rows.Count;
            //string tesmasuk =  ((Label)GRTP.Rows[Convert.ToInt32(0)].FindControl("lblnobukti1")).Text;
            GRTP.UseAccessibleHeader = true;
            GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

            cssGridBayar();
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    private void cssGridBayar()
    {

        // Attribute to show the Plus Minus Button.
        GRTP.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

        //Attribute to hide column in Phone.

        TableCellCollection cells = GRTP.HeaderRow.Cells;
        cells[0].Attributes.Add("data-class", "expand");
        cells[1].Attributes.Add("data-class", "expand");
        cells[2].Attributes.Add("data-class", "expand");
        cells[3].Attributes.Add("data-class", "expand");
        cells[4].Attributes.Add("data-class", "expand");
        cells[5].Attributes.Add("data-class", "expand");
        cells[6].Attributes.Add("data-class", "expand");
        cells[7].Attributes.Add("data-class", "expand");

    }
    private void SetDetailRP()
    {
        DataRow dr = null;

        dtmasuk.Columns.Add(new DataColumn("nobukti", typeof(int)));
        dtmasuk.Columns.Add(new DataColumn("nobuktimanual", typeof(string)));
        dtmasuk.Columns.Add(new DataColumn("tgltransfer", typeof(string)));
        dtmasuk.Columns.Add(new DataColumn("typetransaksi", typeof(string)));
        dtmasuk.Columns.Add(new DataColumn("keterangan", typeof(string)));
        dtmasuk.Columns.Add(new DataColumn("nominal", typeof(decimal)));
        dtmasuk.Columns.Add(new DataColumn("debit", typeof(decimal)));
        dtmasuk.Columns.Add(new DataColumn("kredit", typeof(decimal)));

        dr = dtmasuk.NewRow();
        dr["nobukti"] = string.Empty;
        dr["nobuktimanual"] = string.Empty;
        dr["tgltransfer"] = string.Empty;
        dr["typetransaksi"] = string.Empty;
        dr["keterangan"] = string.Empty;
        dr["nominal"] = 0;
        dr["debit"] = 0;
        dr["kredit"] = 0;


        dtmasuk.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["ListDetail"] = dtmasuk;


        GRTP.DataSource = dtmasuk;
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;
        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        cssGridBayar();
    }
    #endregion
}
