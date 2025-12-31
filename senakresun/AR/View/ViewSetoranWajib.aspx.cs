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
public partial class CL_Process_ViewSetoranWajib: System.Web.UI.Page
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
    string form = "B8";
    string kodesumber = "CL";
    string KodeKonter = "MK08";
    public string nobuktidb;
    
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
                //BindGridEntity();






            }


         


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

   
   
   

    decimal total = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", total);

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

    protected void GRTP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //switch (e.CommandName)
        //{
        //    case "DEL":
        //        int dgcount = GRRP.Rows.Count;


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

    decimal debet = 0, kredit = 0;
    protected void GRTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            debet += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "mutasidebet"));
            kredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "mutasikredit"));
       
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldebet = (Label)e.Row.FindControl("lbltotaldebet");
            lbltotaldebet.Text = string.Format("{0:N2}", debet);
            Label lbltotalkredit = (Label)e.Row.FindControl("lbltotalkredit");
            lbltotalkredit.Text = string.Format("{0:N2}", kredit);
        }
    }


 

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }



   


    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            var kodelgn = hdncustomer.Value;
        string norekening = HDNoRekening.Value;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 15, kodelgn, typeof(string));
        var[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIARMSTTBG", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)

            
        {
            txtkodelgn.Text = kodelgn;
            txtNmSup.Text = ge.ListDataset.Tables[0].Rows[0]["nama_nasabah"].ToString();

            txtSaldoPokok.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_pokok"].ToString()));
            txtsaldowajib.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_awal_wajib"].ToString()));

            txtmutasidebet.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["mutasi_debet_bln"].ToString()));
            txtmutasikredit.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["mutasi_kredit_bln"].ToString()));

            txtsaldobulanlalu.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["Saldo_Akhir_Akm"].ToString()));
            txtsaldotabungan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo_tabungan"].ToString()));

            txtnorekening.Text= ge.ListDataset.Tables[0].Rows[0]["no_rekening"].ToString();
                //btnSimpan.Visible = true;

                txttglregister.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["register_date"].ToString()));
                Txtuser.Text = ge.ListDataset.Tables[0].Rows[0]["Register_User"].ToString();
                Txtkodesales.Text = ge.ListDataset.Tables[0].Rows[0]["kode_ref"].ToString();

                if (ge.ListDataset.Tables[0].Rows[0]["tgl_penutupan"].ToString()!="")
                {
                    txttglpenutupan.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_penutupan"].ToString()));
                   
                }
                else
                {
                    txttglpenutupan.Text = "";
                  

                }
                if (ge.ListDataset.Tables[0].Rows[0]["tgl_wo"].ToString() != "")
                {
                    txttglwo.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tgl_wo"].ToString()));
                   
                }
                else
                {
                  
                    txttglwo.Text = "";
                    
                  

                }

                txtstatuswo.Text = ge.ListDataset.Tables[0].Rows[0]["status_wo"].ToString();
                txtstatustutup.Text = ge.ListDataset.Tables[0].Rows[0]["flag_tutup"].ToString();
                txtKeterangan.Text = ge.ListDataset.Tables[0].Rows[0]["alasan_keluar"].ToString();
                //btnSimpan.Enabled = true;
            }

        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak ditemukan!');", true);
            txtkodelgn.Text = "";
            txtNmSup.Text = "";


            txtSaldoPokok.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtsaldowajib.Text = string.Format("{0:N2}", decimal.Parse("0"));

            txtmutasidebet.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtmutasikredit.Text = string.Format("{0:N2}", decimal.Parse("0"));

            txtsaldobulanlalu.Text = string.Format("{0:N2}", decimal.Parse("0"));
            txtsaldotabungan.Text = string.Format("{0:N2}", decimal.Parse("0"));

            txtnorekening.Text = "";

                txttglregister.Text = "";
                Txtuser.Text = "";
                Txtkodesales.Text = "";

                txttglpenutupan.Text = "";
                txtstatustutup.Text = "";
                txttglwo.Text = "";
                txtstatuswo.Text = "";
                txtKeterangan.Text = "";




                //btnSimpan.Enabled = false;
            }


     
        string customer = hdncustomer.Value;
        //string norekening = HDNoRekening.Value;
        pnltrn.Visible = true;

        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
        var1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, customer, typeof(string));
        var1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
        var1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

        var1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTRANSAKSITABUNGAN", typeof(string));
        var1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", var1, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc";


        GRTP.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRTP.DataBind();

        GRTP.UseAccessibleHeader = true;

        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;


         

            DefaultClass.Global.SqlHelper[] vart1 = new DefaultClass.Global.SqlHelper[6];
            vart1[0] = gb.CreateSPParameter("@Kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, customer, typeof(string));
            vart1[1] = gb.CreateSPParameter("@norekening", ParameterDirection.Input, SqlDbType.VarChar, 30, norekening, typeof(string));
            vart1[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
            vart1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(String));

            vart1[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASICDG", typeof(string));
            vart1[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_AR", vart1, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_drop Desc";


            GRDCADANGAN.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDCADANGAN.DataBind();

            GRDCADANGAN.UseAccessibleHeader = true;

            GRDCADANGAN.HeaderRow.TableSection = TableRowSection.TableHeader;

            txtmasukcadangn.Text = string.Format("{0:N0}", lblby_cadangan); 
            txtmutasicadangan.Text = string.Format("{0:N0}", lblmutasi_cadangan) ;
            txtsaldocadangan.Text = string.Format("{0:N0}", lblSaldo);
   

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    decimal lblby_cadangan = 0, lblmutasi_cadangan = 0,lblSaldo = 0;
    protected void GRDCADANGAN_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lblby_cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));
            lblmutasi_cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "mutasi_cadangan"));

            lblSaldo += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lblby_cadanganx = (Label)e.Row.FindControl("lblby_cadanganx");
            lblby_cadanganx.Text = string.Format("{0:N0}", lblby_cadangan);

            Label lblmutasi_cadanganx = (Label)e.Row.FindControl("lblmutasi_cadanganx");
            lblmutasi_cadanganx.Text = string.Format("{0:N0}", lblmutasi_cadangan);

            Label lblsaldox = (Label)e.Row.FindControl("lblsaldox");
            lblsaldox.Text = string.Format("{0:N0}", lblSaldo);


        }
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

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_getCustomer_tabungan_VIEW", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }






}
