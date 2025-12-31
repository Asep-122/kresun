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
using DocumentFormat.OpenXml.EMMA;
using System.Security.AccessControl;

public partial class CL_View_ViewKasBankLama: System.Web.UI.Page
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
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
    }
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "A2";
    string kodesumber = "MK";
    string KodeKonter = "AP03";
    public string nobuktidb;
    string no_bukti;
    decimal tambahteller = 0;
    decimal kurangteller = 0;
    decimal saldoawal = 0;
    decimal saldoawalTL2 = 0;
    decimal saldoawalHT = 0;
    decimal saldoawalBanK = 0;
    decimal saldoawalcdgtl1 = 0;
    decimal saldoawalcdgtl2 = 0;
    decimal saldoawalmaterai = 0;
    decimal saldoawalprovisi = 0;
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {

        Pnldata.Visible = true;
        

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
               

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
              
               
               txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");



                btnSaldoawal(sender, e);
                lblsaldoawaltl1.Text = string.Format("{0:N0}", saldoawal);

                BindGridEntityTL1TRANSAKSI();

                lblpenambahantl1.Text = string.Format("{0:N0}", totaldbtl1);
                lblpengurangantl1.Text = string.Format("{0:N0}", totalkrtl1);

                lblsaldoakhirtl1.Text = string.Format("{0:N0}", saldoawal + (totaldbtl1 - totalkrtl1));




                btnSaldoawalTL2(sender, e);

                lblsaldoawaltl2.Text = string.Format("{0:N0}", saldoawalTL2);

                BindGridEntityTL2TRANSAKSI();

                lblpenambahantl2.Text = string.Format("{0:N0}", totaldbtl2);
                lblpengurangantl2.Text = string.Format("{0:N0}", totalkrtl2);

                lblsaldoakhirtl2.Text = string.Format("{0:N0}", saldoawalTL2 + (totaldbtl2 - totalkrtl2));



                lblsaldoawalTL.Text = string.Format("{0:N0}", saldoawal + saldoawalTL2);
                lblpenambahantl.Text = string.Format("{0:N0}", totaldbtl1 + totaldbtl2);
                lblpengurangantl.Text = string.Format("{0:N0}", totalkrtl1 + totalkrtl2);
                lblsaldoakirtl.Text = string.Format("{0:N0}", (saldoawal + (totaldbtl1 - totalkrtl1)) + saldoawalTL2 + (totaldbtl2 - totalkrtl2));



                btnSaldoawalHT(sender, e);

                lblsaldoawalht.Text = string.Format("{0:N0}", saldoawalHT);

                BindGridEntityHTTRANSAKSI();

                lblpenambahanht.Text = string.Format("{0:N0}", totaldbht);
                lblpenguranganht.Text = string.Format("{0:N0}", totalkrht);

                lblsaldoakirht.Text = string.Format("{0:N0}", saldoawalHT + (totaldbht - totalkrht));




                btnSaldoawalBANK(sender, e);

                lblsaldoawalbank.Text = string.Format("{0:N0}", saldoawalBanK);

                BindGridEntityBKTRANSAKSI();

                lblpenambahanbank.Text = string.Format("{0:N0}", totaldbbank);
                lblpenguranganbank.Text = string.Format("{0:N0}", totalkrbank);

                lblsaldoakirbank.Text = string.Format("{0:N0}", saldoawalBanK + (totaldbbank - totalkrbank));











                btnSaldoawalcdgtl2(sender, e);

                lblsaldoawalcdgtl2.Text = string.Format("{0:N0}", saldoawalcdgtl2);

                BindGridEntityCDGTL2TRANSAKSI();

                lblpenambahancdgtl2.Text = string.Format("{0:N0}", totaldbcdgtl2);
                lblpengurangancdgtl2.Text = string.Format("{0:N0}", totalkrtcdgl2);

                lblsaldoakhircdgtl2.Text = string.Format("{0:N0}", saldoawalcdgtl2 + (totaldbcdgtl2 - totalkrtcdgl2));




                btnSaldoawalmaterai(sender, e);

                lblsaldoawalmaterai.Text = string.Format("{0:N0}", saldoawalmaterai);

                BindGridEntityMATERAITRANSAKSI();

                lblpenambahanmaterai.Text = string.Format("{0:N0}", totaldbmaterai);
                lblpenguranganmaterai.Text = string.Format("{0:N0}", totalkrtmaterai);

                lblsaldoakhirmaterai.Text = string.Format("{0:N0}", saldoawalmaterai + (totaldbmaterai - totalkrtmaterai));




                btnSaldoawalprovisi(sender, e);

                lblsaldoawalprovisi.Text = string.Format("{0:N0}", saldoawalprovisi);

                BindGridEntityPROVISITRANSAKSI();

                lblpenambahanprovisi.Text = string.Format("{0:N0}", totaldbprovisi);
                lblpenguranganprovisi.Text = string.Format("{0:N0}", totalkrtprovisi);

                lblsaldoakhirprovisi.Text = string.Format("{0:N0}", saldoawalprovisi + (totaldbprovisi - totalkrtprovisi));


                lblsaldoawalTlHT.Text = string.Format("{0:N0}", saldoawal + saldoawalHT);
                lblpenambahanTlHT.Text = string.Format("{0:N0}", totaldbht + totaldbtl1);
                lblpenguranganTlHT.Text = string.Format("{0:N0}", totalkrht + totalkrtl1);
                LblsaldoakhirTlHT.Text = string.Format("{0:N0}", (saldoawalHT + (totaldbht - totalkrht)) + (saldoawal + (totaldbtl1 - totalkrtl1)));

                lblsaldoawalTL1d.Text = string.Format("{0:N0}", saldoawal);
                lblpenambahantl1d.Text = string.Format("{0:N0}", totaldbtl1);
                lblpengurangantld.Text = string.Format("{0:N0}", totalkrtl1);
                lblsaldoakirtl1d.Text = string.Format("{0:N0}", (saldoawal + (totaldbtl1 - totalkrtl1)));

                lblsaldoawalTL2d.Text = string.Format("{0:N0}", saldoawalTL2);
                lblpenambahantl2d.Text = string.Format("{0:N0}", totaldbtl2);
                lblpengurangantl2d.Text = string.Format("{0:N0}", totalkrtl2);
                lblsaldoakirtl2d.Text = string.Format("{0:N0}", (saldoawalTL2 + (totaldbtl2 - totalkrtl2)));






                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

            }


          
            


        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    protected void btnSaldoawal(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALTL1", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

         
            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawal = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());
                
            

            }

            else
            {
                saldoawal = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }

    protected void btnSaldoawalTL2(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALTL2", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawalTL2 = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());



            }

            else
            {
                saldoawalTL2 = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }

    protected void btnSaldoawalHT(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALHT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawalHT = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());



            }

            else
            {
                saldoawalHT = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }

    protected void btnSaldoawalBANK(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALBK", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawalBanK = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());



            }

            else
            {
                saldoawalBanK = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }




    protected void btnSaldoawalcdgtl2(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALCDGTL2", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawalcdgtl2 = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());



            }

            else
            {
                saldoawalcdgtl2 = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }

    protected void btnSaldoawalmaterai(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALMATERAI", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawalmaterai = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());



            }

            else
            {
                saldoawalmaterai = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }


    protected void btnSaldoawalprovisi(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();

        try
        {



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSALDOAWALPROVISI", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");


            if (ge.ListDataset.Tables[0].Rows.Count > 0)
            {
                saldoawalprovisi = decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldokemarin"].ToString());



            }

            else
            {
                saldoawalprovisi = 0;
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

                //rprt.Close();
                //rprt.Dispose();
            }

        }



    }



    decimal totaldbtl1 = 0, totalkrtl1 = 0;
    protected void GRDTL1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totaldbtl1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "penambahan"));
            totalkrtl1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengurangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldbtl1 = (Label)e.Row.FindControl("lbltotaldbtl1");
            lbltotaldbtl1.Text = string.Format("{0:N2}", totaldbtl1);

            Label lbltotalkrtl1 = (Label)e.Row.FindControl("lbltotalkrtl1");
            lbltotalkrtl1.Text = string.Format("{0:N2}", totalkrtl1);

        }
    }

    public void BindGridEntityTL1TRANSAKSI()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASIKASTL1", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn ASC";


            GRDTL1.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDTL1.DataBind();

            GRDTL1.UseAccessibleHeader = true;

            GRDTL1.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    decimal totaldbtl2 = 0, totalkrtl2 = 0;
    protected void GRDTL2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totaldbtl2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "penambahan"));
            totalkrtl2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengurangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldbtl1 = (Label)e.Row.FindControl("lbltotaldbtl1");
            lbltotaldbtl1.Text = string.Format("{0:N2}", totaldbtl2);

            Label lbltotalkrtl1 = (Label)e.Row.FindControl("lbltotalkrtl1");
            lbltotalkrtl1.Text = string.Format("{0:N2}", totalkrtl2);

        }
    }

    public void BindGridEntityTL2TRANSAKSI()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASIKASTL2", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn ASC";


            GRDTL2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDTL2.DataBind();

            GRDTL2.UseAccessibleHeader = true;

            GRDTL2.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }




    decimal totaldbht = 0, totalkrht = 0;
    protected void GRDHT_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totaldbht += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "penambahan"));
            totalkrht += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengurangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldbtl1 = (Label)e.Row.FindControl("lbltotaldbtl1");
            lbltotaldbtl1.Text = string.Format("{0:N2}", totaldbht);

            Label lbltotalkrtl1 = (Label)e.Row.FindControl("lbltotalkrtl1");
            lbltotalkrtl1.Text = string.Format("{0:N2}", totalkrht);

        }
    }

    public void BindGridEntityHTTRANSAKSI()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASIKASHT", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn ASC";


            GRDHT.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDHT.DataBind();

            GRDHT.UseAccessibleHeader = true;

            GRDHT.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }


    decimal totaldbbank = 0, totalkrbank = 0;
    protected void GRDBK_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totaldbbank += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "penambahan"));
            totalkrbank += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengurangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldbtl1 = (Label)e.Row.FindControl("lbltotaldbtl1");
            lbltotaldbtl1.Text = string.Format("{0:N2}", totaldbbank);

            Label lbltotalkrtl1 = (Label)e.Row.FindControl("lbltotalkrtl1");
            lbltotalkrtl1.Text = string.Format("{0:N2}", totalkrbank);

        }
    }

    public void BindGridEntityBKTRANSAKSI()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASIKASBK", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn ASC";


            GRDBANK.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRDBANK.DataBind();

            GRDBANK.UseAccessibleHeader = true;

            GRDBANK.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }




   
    decimal totaldbcdgtl2 = 0, totalkrtcdgl2 = 0;
    protected void Grdcdgtl2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totaldbcdgtl2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "penambahan"));
            totalkrtcdgl2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengurangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldbtl1 = (Label)e.Row.FindControl("lbltotaldbtl1");
            lbltotaldbtl1.Text = string.Format("{0:N2}", totaldbcdgtl2);

            Label lbltotalkrtl1 = (Label)e.Row.FindControl("lbltotalkrtl1");
            lbltotalkrtl1.Text = string.Format("{0:N2}", totalkrtcdgl2);

        }
    }

    public void BindGridEntityCDGTL2TRANSAKSI()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASICDGTL2", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn ASC";


            Grdcdgtl2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            Grdcdgtl2.DataBind();

            Grdcdgtl2.UseAccessibleHeader = true;

            Grdcdgtl2.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    
    
    decimal totaldbmaterai = 0, totalkrtmaterai = 0;
    protected void GrdMaterai_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totaldbmaterai += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "penambahan"));
            totalkrtmaterai += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengurangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldbtl1 = (Label)e.Row.FindControl("lbltotaldbtl1");
            lbltotaldbtl1.Text = string.Format("{0:N2}", totaldbmaterai);

            Label lbltotalkrtl1 = (Label)e.Row.FindControl("lbltotalkrtl1");
            lbltotalkrtl1.Text = string.Format("{0:N2}", totalkrtmaterai);

        }
    }

    public void BindGridEntityMATERAITRANSAKSI()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASIMATERAI", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn ASC";


            GrdMaterai.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GrdMaterai.DataBind();

            GrdMaterai.UseAccessibleHeader = true;

            GrdMaterai.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    decimal totaldbprovisi = 0, totalkrtprovisi = 0;
    protected void GrdProvisi_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totaldbprovisi += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "penambahan"));
            totalkrtprovisi += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pengurangan"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldbtl1 = (Label)e.Row.FindControl("lbltotaldbtl1");
            lbltotaldbtl1.Text = string.Format("{0:N2}", totaldbprovisi);

            Label lbltotalkrtl1 = (Label)e.Row.FindControl("lbltotalkrtl1");
            lbltotalkrtl1.Text = string.Format("{0:N2}", totalkrtprovisi);

        }
    }

    public void BindGridEntityPROVISITRANSAKSI()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTMUTASIPROVISI", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 30, "", typeof(string));
            var[5] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrnx.Text, typeof(DateTime));
            ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_KAS_HEAD_TELLER_001", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "TypeTrn ASC";


            GrdProvisi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GrdProvisi.DataBind();

            GrdProvisi.UseAccessibleHeader = true;

            GrdProvisi.HeaderRow.TableSection = TableRowSection.TableHeader;

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

   
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {



        btnSaldoawal(sender, e);

        lblsaldoawaltl1.Text = string.Format("{0:N0}", saldoawal);

        BindGridEntityTL1TRANSAKSI();

        lblpenambahantl1.Text = string.Format("{0:N0}", totaldbtl1);
        lblpengurangantl1.Text = string.Format("{0:N0}", totalkrtl1);

        lblsaldoakhirtl1.Text = string.Format("{0:N0}", saldoawal + (totaldbtl1 - totalkrtl1));


        btnSaldoawalTL2(sender, e);

        lblsaldoawaltl2.Text = string.Format("{0:N0}", saldoawalTL2);

        BindGridEntityTL2TRANSAKSI();

        lblpenambahantl2.Text = string.Format("{0:N0}", totaldbtl2);
        lblpengurangantl2.Text = string.Format("{0:N0}", totalkrtl2);

        lblsaldoakhirtl2.Text = string.Format("{0:N0}", saldoawalTL2 + (totaldbtl2 - totalkrtl2));

        lblsaldoawalTL.Text = string.Format("{0:N0}", saldoawal + saldoawalTL2);
        lblpenambahantl.Text = string.Format("{0:N0}", totaldbtl1 + totaldbtl2);
        lblpengurangantl.Text = string.Format("{0:N0}", totalkrtl1 + totalkrtl2);
        lblsaldoakirtl.Text = string.Format("{0:N0}", (saldoawal + (totaldbtl1 - totalkrtl1)) + saldoawalTL2 + (totaldbtl2 - totalkrtl2));



        btnSaldoawalHT(sender, e);

        lblsaldoawalht.Text = string.Format("{0:N0}", saldoawalHT);

        BindGridEntityHTTRANSAKSI();

        lblpenambahanht.Text = string.Format("{0:N0}", totaldbht);
        lblpenguranganht.Text = string.Format("{0:N0}", totalkrht);

        lblsaldoakirht.Text = string.Format("{0:N0}", saldoawalHT + (totaldbht - totalkrht));


        btnSaldoawalBANK(sender, e);

        lblsaldoawalbank.Text = string.Format("{0:N0}", saldoawalBanK);

        BindGridEntityBKTRANSAKSI();

        lblpenambahanbank.Text = string.Format("{0:N0}", totaldbbank);
        lblpenguranganbank.Text = string.Format("{0:N0}", totalkrbank);

        lblsaldoakirbank.Text = string.Format("{0:N0}", saldoawalBanK + (totaldbbank - totalkrbank));



        btnSaldoawalcdgtl2(sender, e);

        lblsaldoawalcdgtl2.Text = string.Format("{0:N0}", saldoawalcdgtl2);

        BindGridEntityCDGTL2TRANSAKSI();

        lblpenambahancdgtl2.Text = string.Format("{0:N0}", totaldbcdgtl2);
        lblpengurangancdgtl2.Text = string.Format("{0:N0}", totalkrtcdgl2);

        lblsaldoakhircdgtl2.Text = string.Format("{0:N0}", saldoawalcdgtl2 + (totaldbcdgtl2 - totalkrtcdgl2));



        

        btnSaldoawalmaterai(sender, e);

        lblsaldoawalmaterai.Text = string.Format("{0:N0}", saldoawalmaterai);

        BindGridEntityMATERAITRANSAKSI();

        lblpenambahanmaterai.Text = string.Format("{0:N0}", totaldbmaterai);
        lblpenguranganmaterai.Text = string.Format("{0:N0}", totalkrtmaterai);

        lblsaldoakhirmaterai.Text = string.Format("{0:N0}", saldoawalmaterai + (totaldbmaterai - totalkrtmaterai));

        btnSaldoawalprovisi(sender, e);

        lblsaldoawalprovisi.Text = string.Format("{0:N0}", saldoawalprovisi);

        BindGridEntityPROVISITRANSAKSI();

        lblpenambahanprovisi.Text = string.Format("{0:N0}", totaldbprovisi);
        lblpenguranganprovisi.Text = string.Format("{0:N0}", totalkrtprovisi);

        lblsaldoakhirprovisi.Text = string.Format("{0:N0}", saldoawalprovisi + (totaldbprovisi - totalkrtprovisi));


        lblsaldoawalTlHT.Text = string.Format("{0:N0}", saldoawal + saldoawalHT);
        lblpenambahanTlHT.Text = string.Format("{0:N0}", totaldbht + totaldbtl1);
        lblpenguranganTlHT.Text = string.Format("{0:N0}", totalkrht + totalkrtl1);
        LblsaldoakhirTlHT.Text = string.Format("{0:N0}", (saldoawalHT + (totaldbht - totalkrht)) + (saldoawal + (totaldbtl1 - totalkrtl1)));

        lblsaldoawalTL1d.Text = string.Format("{0:N0}", saldoawal);
        lblpenambahantl1d.Text = string.Format("{0:N0}", totaldbtl1);
        lblpengurangantld.Text = string.Format("{0:N0}", totalkrtl1);
        lblsaldoakirtl1d.Text = string.Format("{0:N0}", (saldoawal + (totaldbtl1 - totalkrtl1)));

        lblsaldoawalTL2d.Text = string.Format("{0:N0}", saldoawalTL2);
        lblpenambahantl2d.Text = string.Format("{0:N0}", totaldbtl2);
        lblpengurangantl2d.Text = string.Format("{0:N0}", totalkrtl2);
        lblsaldoakirtl2d.Text = string.Format("{0:N0}", (saldoawalTL2 + (totaldbtl2 - totalkrtl2)));


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }

 
   
   


}
