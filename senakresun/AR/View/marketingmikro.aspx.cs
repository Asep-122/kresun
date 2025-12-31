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

public partial class Report_marketingmikro : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    private Boolean SessIsSuperAdmin
    {
        get { return Boolean.Parse(Session["SessIsSuperAdmin"].ToString()); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }

    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessIsLevel
    {
        get { return Session["SessIsLevel"].ToString(); }
        set { ViewState["SessIsLevel"] = value; }

    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }

    DataTable dtBayar = new DataTable("TransactionBayar");

    //DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //BindGrddatawilayahEntity();
                
                //string query;
                //query = " SELECT * FROM aspnet_report_realisasi_mikro_kode_produk()";

                //DataTable dt = GetData(query);

                //ddlproduk.DataSource = dt;
                //ddlproduk.DataTextField = "nama_produk";
                //ddlproduk.DataValueField = "kode_produk";
                //ddlproduk.DataBind();
                //ddlproduk.Items.Insert(0, new ListItem("ALL", "ALL"));

                //ddlproduk.SelectedValue = "ALL";
                //ddlproduk.Enabled = false;
                DefaultClass.Global.SqlHelper[] Paramsi = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_Product", Paramsi, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlproduk, "None", "");


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang_X", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();
                ddlcabang.SelectedValue = SessBranchID;

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[2];
                varx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                varx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_Marketing_List", varx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";
                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "Text";
                ddlmarketing.DataValueField = "value";
                ddlmarketing.DataBind();
                ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));



                ////query = "SELECT * FROM aspnet_report_realisasi_mikro_cabang('" + SessBranchID + "')";


                ////DataTable dtx = GetData(query);

                ////ddlcabang.DataSource = dtx;
                ////ddlcabang.DataTextField = "nama_cabang";
                ////ddlcabang.DataValueField = "kode_cabang";
                ////ddlcabang.DataBind();

                //if (SessBranchID != "ALL")
                //{

                //    ddlcabang.Enabled = false;

                //    //ddlcabang.Items.Insert(0, new ListItem("-", "-"));
                //    ddlcabang.SelectedValue = SessBranchID;

                //    query= "select * from aspnet_report_marketing_mikro_kode_ref('" + SessBranchID + "')";               

                //    DataTable dx = GetData(query);
                //    ddlmarketing.DataSource = dx;
                //    ddlmarketing.DataTextField = "nama";
                //    ddlmarketing.DataValueField = "kode_ref";
                //    ddlmarketing.DataBind();
                //    ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));
                //    ddlmarketing.Enabled = true;

                //}
                //else
                //{
                //    ddlcabang.Enabled = true;
                //    ddlcabang.Items.Insert(0, new ListItem("ALL", "ALL"));
                //    ddlcabang.SelectedValue = "ALL";
                //    ddlmarketing.Enabled = false;
                //    ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));
                //}


                Lbltaspen.Text= "DATA MARKETING MIKRO / TAGIHAN";
                //Pnltagihan.Visible = true;
                txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");

                txtTglS.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtTglS.Enabled = false;


            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void btnSearch_Click()
    {
        ReportDocument rprt = new ReportDocument();
       try
        {
            //DateTime ambilawal = Convert.ToDateTime(txtStart.Text); 
            DateTime now = DateTime.Now;

            string tanggal = now.ToString("dd-MM-yyyy");


            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTTAGIHAN", typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
            if (ge.ListDataTable.Rows.Count > 0)
            {
                rprt.Load(Server.MapPath("~/AR/View/rpttagihandetail.rpt"));

                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("Userprint", SessLoginID);
                rprt.SetParameterValue("xPeriode", txtTglL.Text);
                rprt.SetParameterValue("Produk", ddlproduk.SelectedItem.Text);
                rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                Response.ClearContent();
                Response.ClearHeaders();
                //ExportFormatType formatType = ExportFormatType.NoFormat;
                Response.ContentType = "application/pdf";
                rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                Response.End();


            }
            else if (ge.ListDataTable.Rows.Count <= 0)

            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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
    }

    public void BindGridEntityTagihan()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAGIHAN", typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


        Grdtagihan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtagihan.DataBind();

        Grdtagihan.UseAccessibleHeader = true;

        Grdtagihan.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

    public void BindGridEntityTagihanMrk()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
            var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
            var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTTAGIHAN", typeof(string));
            var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


            Grdtagihanmrk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            Grdtagihanmrk.DataBind();

            Grdtagihanmrk.UseAccessibleHeader = true;

            Grdtagihanmrk.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }
    //'int total = 0;
    decimal plafon = 0, total = 0, baki = 0, tungpokok = 0, tungbunga = 0, tungpokokjthtempo = 0, Cadangan = 0, totaltung = 0, denda = 0, stotal = 0, xrasio = 0;
    protected void Grdtagihan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((DataBinder.Eval(e.Row.DataItem, "status_nasabah").ToString()) == "B")
            {
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlah_nasabah"));
                plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
                baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
                tungpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok"));
                tungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_bunga"));
                tungpokokjthtempo += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok_jt"));
                Cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));
            }
            //plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            //baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
            //tungpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok"));
            //tungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_bunga"));
            //tungpokokjthtempo += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok_jt"));
            //Cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));
        




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            
            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = string.Format("{0:N0}", decimal.Parse(total.ToString()));
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = string.Format("{0:N2}", decimal.Parse(plafon.ToString())); //plafon.ToString();

            Label lblbaki = (Label)e.Row.FindControl("lblbaki");
            lblbaki.Text = string.Format("{0:N2}", decimal.Parse(baki.ToString())); //baki.ToString();

            Label lbltungpokok = (Label)e.Row.FindControl("lbltungpokok");
            lbltungpokok.Text = string.Format("{0:N2}", decimal.Parse(tungpokok.ToString())); //tungpokok.ToString();

            Label lbltungbunga = (Label)e.Row.FindControl("lbltungbunga");
            lbltungbunga.Text = string.Format("{0:N2}", decimal.Parse(tungbunga.ToString())); //tungbunga.ToString();

            Label lbltungpokokjthtempo = (Label)e.Row.FindControl("lbltungpokokjthtempo");
            lbltungpokokjthtempo.Text = string.Format("{0:N2}", decimal.Parse(tungpokokjthtempo.ToString())); //tungpokokjthtempo.ToString();
            Label lblCadangan = (Label)e.Row.FindControl("lblCadangan");
            lblCadangan.Text = string.Format("{0:N2}", decimal.Parse(Cadangan.ToString())); //Cadangan.ToString();
            xrasio = (tungpokok / baki) * 100;
            Label lblrasio = (Label)e.Row.FindControl("lblrasio");

            lblrasio.Text = string.Format("{0:N2}", decimal.Parse(xrasio.ToString())); //xrasio.ToString("N2");

        }
    }
   
    //'int total = 0;
    
    protected void Grdtagihanmrk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
            tungpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok"));
            tungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_bunga"));
            totaltung += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_tung"));
 
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = string.Format("{0:N2}", decimal.Parse(plafon.ToString())); //plafon.ToString();

            Label lblbaki = (Label)e.Row.FindControl("lblbaki");
            lblbaki.Text = string.Format("{0:N2}", decimal.Parse(baki.ToString())); //baki.ToString();

            Label lbltungpokok = (Label)e.Row.FindControl("lbltungpokok");
            lbltungpokok.Text = string.Format("{0:N2}", decimal.Parse(tungpokok.ToString())); //tungpokok.ToString();

            Label lbltungbunga = (Label)e.Row.FindControl("lbltungbunga");
            lbltungbunga.Text = string.Format("{0:N2}", decimal.Parse(tungbunga.ToString())); //tungbunga.ToString();

            Label lbltotaltung = (Label)e.Row.FindControl("lbltotaltung");
            lbltotaltung.Text = string.Format("{0:N2}", decimal.Parse(totaltung.ToString())); //totaltung.ToString();    


        }
    }
    protected void Grdtunggakan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((DataBinder.Eval(e.Row.DataItem, "status_nasabah").ToString()) == "BLM WO")
            {
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlah_nasabah"));
                plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
                baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
                tungpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok"));
                tungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_bunga"));
                tungpokokjthtempo += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok_jt"));
                Cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));
            }






        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = string.Format("{0:N2}", decimal.Parse(total.ToString())); //total.ToString();
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = string.Format("{0:N2}", decimal.Parse(plafon.ToString())); //plafon.ToString();

            Label lblbaki = (Label)e.Row.FindControl("lblbaki");
            lblbaki.Text = string.Format("{0:N2}", decimal.Parse(baki.ToString())); //baki.ToString();

            Label lbltungpokok = (Label)e.Row.FindControl("lbltungpokok");
            lbltungpokok.Text = string.Format("{0:N2}", decimal.Parse(tungpokok.ToString())); //tungpokok.ToString();

            Label lbltungbunga = (Label)e.Row.FindControl("lbltungbunga");
            lbltungbunga.Text = string.Format("{0:N2}", decimal.Parse(tungbunga.ToString())); //tungbunga.ToString();

            Label lbltungpokokjthtempo = (Label)e.Row.FindControl("lbltungpokokjthtempo");
            lbltungpokokjthtempo.Text = string.Format("{0:N2}", decimal.Parse(tungpokokjthtempo.ToString())); //tungpokokjthtempo.ToString();
            Label lblCadangan = (Label)e.Row.FindControl("lblCadangan");
            lblCadangan.Text = string.Format("{0:N2}", decimal.Parse(Cadangan.ToString())); //Cadangan.ToString();
            xrasio = (tungpokok / baki) * 100;
            Label lblrasio = (Label)e.Row.FindControl("lblrasio");

            lblrasio.Text = string.Format("{0:N2}", decimal.Parse(xrasio.ToString())); //xrasio.ToString("N2");


        }
    }

    //'int total = 0;

    protected void Grdtunggakanmrk_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
            tungpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_pokok"));
            tungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tung_bunga"));
            Cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));





        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = plafon.ToString("N2");

            Label lblbaki = (Label)e.Row.FindControl("lblbaki");
            lblbaki.Text = baki.ToString("N2");

            Label lbltungpokok = (Label)e.Row.FindControl("lbltungpokok");
            lbltungpokok.Text = tungpokok.ToString("N2");

            Label lbltungbunga = (Label)e.Row.FindControl("lbltungbunga");
            lbltungbunga.Text = tungbunga.ToString("N2");

            Label lblcadangan = (Label)e.Row.FindControl("lblcadangan");
            lblcadangan.Text = Cadangan.ToString("N2");



        }
    }
    protected void Grdterbayar_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((DataBinder.Eval(e.Row.DataItem, "status_nasabah").ToString()) == "B")
            {
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlah_nasabah"));
                plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
                baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
                tungpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
                tungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bunga"));
                Cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));
                denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
                stotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
            }




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = string.Format("{0:N0}", decimal.Parse(total.ToString())); //total.ToString();
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = string.Format("{0:N2}", decimal.Parse(plafon.ToString())); //plafon.ToString();

            Label lblbaki = (Label)e.Row.FindControl("lblbaki");
            lblbaki.Text = string.Format("{0:N2}", decimal.Parse(baki.ToString())); //baki.ToString();

            Label lbltungpokok = (Label)e.Row.FindControl("lbltungpokok");
            lbltungpokok.Text = string.Format("{0:N2}", decimal.Parse(tungpokok.ToString())); //tungpokok.ToString();

            Label lbltungbunga = (Label)e.Row.FindControl("lbltungbunga");
            lbltungbunga.Text = string.Format("{0:N2}", decimal.Parse(tungbunga.ToString())); //tungbunga.ToString();

            Label lblCadangan = (Label)e.Row.FindControl("lblCadangan");
            lblCadangan.Text = string.Format("{0:N2}", decimal.Parse(Cadangan.ToString())); //Cadangan.ToString();

            Label lbldenda = (Label)e.Row.FindControl("lbldenda");
            lbldenda.Text = string.Format("{0:N2}", decimal.Parse(denda.ToString())); //denda.ToString();
            Label lblstotal = (Label)e.Row.FindControl("lblstotal");
            lblstotal.Text = string.Format("{0:N2}", decimal.Parse(stotal.ToString())); //stotal.ToString();

        }
    }

    //'int total = 0;

    protected void Grdterbayarmrk_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            plafon += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plafon"));
            baki += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
            tungpokok += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pokok"));
            tungbunga += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bunga"));
            denda += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "denda"));
            stotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
            Cadangan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "by_cadangan"));




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblplafon = (Label)e.Row.FindControl("lblPlafon");
            lblplafon.Text = plafon.ToString();

            Label lblbaki = (Label)e.Row.FindControl("lblbaki");
            lblbaki.Text = baki.ToString();

            Label lbltungpokok = (Label)e.Row.FindControl("lbltungpokok");
            lbltungpokok.Text = tungpokok.ToString();

            Label lbltungbunga = (Label)e.Row.FindControl("lbltungbunga");
            lbltungbunga.Text = tungbunga.ToString();

            Label lblstotal = (Label)e.Row.FindControl("lblstotal");
            lblstotal.Text = stotal.ToString();


            Label lbldenda = (Label)e.Row.FindControl("lbldenda");
            lbldenda.Text = denda.ToString();
            Label lblcadangan = (Label)e.Row.FindControl("lblcadangan");
            lblcadangan.Text = Cadangan.ToString();

        }
    }
    //public void BindGridEntityTagihanmrk()
    //{
    //    string xdate;
    //    string query;
    //    xdate = Convert.ToDateTime(txtTglL.Text).ToString("yyyy/MM/dd");


    //    if (ddlproduk.SelectedValue == "ALL")
    //    {

    //        query = "select * from aspnet_report_marketing_tagihan_mikro_b_1('" + xdate + "','" + ddlmarketing.SelectedValue + "','" + ddlcabang.SelectedValue + "','" + ddlproduk.SelectedValue + "')";
    //        DataTable dt = GetData(query);

    //        Grdtagihanmrk.DataSource = dt;
    //        Grdtagihanmrk.DataBind();
    //        Grdtagihanmrk.UseAccessibleHeader = true;
    //        Grdtagihanmrk.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    }
    //    else
    //    {
    //        query = "select * from aspnet_report_marketing_tagihan_mikro_b_2('" + xdate + "','" + ddlmarketing.SelectedValue + "','" + ddlcabang.SelectedValue + "','" + ddlproduk.SelectedValue + "')";
    //        DataTable dt = GetData(query);

    //        Grdtagihanmrk.DataSource = dt;
    //        Grdtagihanmrk.DataBind();
    //        Grdtagihanmrk.UseAccessibleHeader = true;
    //        Grdtagihanmrk.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    };







    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);


    //}

    public void BindGridEntityTunggakan()
    {
        

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TUNGGAKAN", typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


        Grdtunggakan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtunggakan.DataBind();

        Grdtunggakan.UseAccessibleHeader = true;

        Grdtunggakan.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    public void BindGridEntityTunggakanmrk()
    {


        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TUNGGAKANNASABAH", typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


        Grdtunggakanmrk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdtunggakanmrk.DataBind();

        Grdtunggakanmrk.UseAccessibleHeader = true;

        Grdtunggakanmrk.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    public void BindGridEntityterbayar()
    {
        
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);




        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);


        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TERBAYAR", typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[6] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[7] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglS.Text, typeof(DateTime));

        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


        Grdterbayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdterbayar.DataBind();

        Grdterbayar.UseAccessibleHeader = true;

        Grdterbayar.HeaderRow.TableSection = TableRowSection.TableHeader;


    }
    public void BindGridEntityterbayarmrk()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TERBAYARNASABAH", typeof(string));
        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[6] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
        var[7] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglS.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "nomor_urut ASC";


        Grdterbayarmrk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        Grdterbayarmrk.DataBind();

        Grdterbayarmrk.UseAccessibleHeader = true;

        Grdterbayarmrk.HeaderRow.TableSection = TableRowSection.TableHeader;










       ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Resize();", true);


    }

    protected void lnkCari_Clickx(object sender, EventArgs e)
    {

        try
        {
            ReportDocument rprt = new ReportDocument();

            if (ddldata.SelectedValue == "0")
            {
                //System.Threading.Thread.Sleep(5000);
                DataTable ds = new DataTable();
                ds = null;
                Grdterbayar.DataSource = ds;
                Grdterbayar.DataBind();
                Grdterbayarmrk.DataSource = ds;
                Grdterbayarmrk.DataBind();
                Grdtagihan.DataSource = ds;
                Grdtagihan.DataBind();
                Grdtagihanmrk.DataSource = ds;
                Grdtagihanmrk.DataBind();
                Grdtunggakan.DataSource = ds;
                Grdtunggakan.DataBind();
                Grdtunggakanmrk.DataSource = ds;
                Grdtunggakanmrk.DataBind();


                if (Lbltaspen.Text == "DATA MARKETING MIKRO / TAGIHAN")
                {
                    if (ddlmarketing.SelectedValue != "ALL")
                    {
                        Pnltagihan.Visible = false;
                        Pnltagihanmrk.Visible = true;
                        BindGridEntityTagihanMrk();
                    }
                    else
                    {
                        Pnltagihan.Visible = false;
                        Pnltagihanmrk.Visible = true;
                        BindGridEntityTagihanMrk();

                    }


                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TUNGGAKAN")
                {

                    if (ddlmarketing.SelectedValue != "ALL")
                    {
                        Pnltunggakan.Visible = false;
                        Pnltunggakanmrk.Visible = true;
                        BindGridEntityTunggakanmrk();


                    }
                    else
                    {
                        Pnltunggakan.Visible = false;
                        Pnltunggakanmrk.Visible = true;
                        BindGridEntityTunggakanmrk();

                    }


                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TERBAYAR")
                {

                    if (ddlmarketing.SelectedValue != "ALL")
                    {
                        Pnlterbayar.Visible = false;
                        Pnlterbayarmrk.Visible = true;
                        BindGridEntityterbayarmrk();
                    }
                    else
                    {
                        Pnlterbayar.Visible = false;
                        Pnlterbayarmrk.Visible = true;
                        BindGridEntityterbayarmrk();

                    }


                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

            }
            else
            {
                 if (Lbltaspen.Text == "DATA MARKETING MIKRO / TAGIHAN")
                {
                    //btnSearch_Click();
                    
                    try
                    {
                 

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTTAGIHAN", typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                        //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            rprt.Load(Server.MapPath("~/AR/View/rpttagihandetail.rpt"));

                            rprt.SetDataSource(ge.ListDataTable);
                            rprt.SetParameterValue("Userprint", SessLoginID);
                            rprt.SetParameterValue("xPeriode", txtTglL.Text);
                            rprt.SetParameterValue("Produk", ddlproduk.SelectedItem.Text);
                            rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                            //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                            Response.ClearContent();
                            Response.ClearHeaders();


                            ExportFormatType formatType = ExportFormatType.NoFormat;
                            switch (ddldata.SelectedItem.Value)
                            {
                                case "1":
                                    Response.ContentType = "application/pdf";
                                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                    break;
                                case "2":
                                    formatType = ExportFormatType.WordForWindows;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanword");
                                    break;
                                case "3":
                                    formatType = ExportFormatType.PortableDocFormat;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanpdf");
                                    break;
                                case "4":
                                    formatType = ExportFormatType.Excel;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanexcel");
                                    break;
                                case "5":
                                    formatType = ExportFormatType.CharacterSeparatedValues;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihancsv");
                                    break;
                            }




                            ////ExportFormatType formatType = ExportFormatType.NoFormat;
                            //Response.ContentType = "application/pdf";
                            //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            Response.End();


                        }
                        else if (ge.ListDataTable.Rows.Count <= 0)

                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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


                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TUNGGAKAN")
                {

                    try
                    {


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TUNGGAKANNASABAH", typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                        //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            rprt.Load(Server.MapPath("~/AR/View/rpttunggakandetail.rpt"));

                            rprt.SetDataSource(ge.ListDataTable);
                            rprt.SetParameterValue("Userprint", SessLoginID);
                            rprt.SetParameterValue("xPeriode", txtTglL.Text);
                            rprt.SetParameterValue("Produk", ddlproduk.SelectedItem.Text);
                            rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                            //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                            Response.ClearContent();
                            Response.ClearHeaders();
                            ////ExportFormatType formatType = ExportFormatType.NoFormat;
                            //Response.ContentType = "application/pdf";
                            //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            ExportFormatType formatType = ExportFormatType.NoFormat;
                            switch (ddldata.SelectedItem.Value)
                            {
                                case "1":
                                    Response.ContentType = "application/pdf";
                                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                    break;
                                case "2":
                                    formatType = ExportFormatType.WordForWindows;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reporttunggakan");
                                    break;
                                case "3":
                                    formatType = ExportFormatType.PortableDocFormat;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reporttunggakanpdf");
                                    break;
                                case "4":
                                    formatType = ExportFormatType.Excel;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reporttunggakanexcel");
                                    break;
                                case "5":
                                    formatType = ExportFormatType.CharacterSeparatedValues;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reporttunggakancsv");
                                    break;
                            }


                            Response.End();


                        }
                        else if (ge.ListDataTable.Rows.Count <= 0)

                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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

                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TERBAYAR")
                {

                    try
                    {


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TERBAYARNASABAH", typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[6] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[7] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglS.Text, typeof(DateTime));

                        //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            rprt.Load(Server.MapPath("~/AR/View/rptterbayardetail.rpt"));

                            rprt.SetDataSource(ge.ListDataTable);
                            rprt.SetParameterValue("Userprint", SessLoginID);
                            rprt.SetParameterValue("xPeriode", txtTglL.Text + " to " + txtTglS.Text);
                            rprt.SetParameterValue("Produk", ddlproduk.SelectedItem.Text);
                            rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                            //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                            Response.ClearContent();
                            Response.ClearHeaders();
                            ////ExportFormatType formatType = ExportFormatType.NoFormat;
                            //Response.ContentType = "application/pdf";
                            //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            ExportFormatType formatType = ExportFormatType.NoFormat;
                            switch (ddldata.SelectedItem.Value)
                            {
                                case "1":
                                    Response.ContentType = "application/pdf";
                                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                    break;
                                case "2":
                                    formatType = ExportFormatType.WordForWindows;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reportterbayarWord");
                                    break;
                                case "3":
                                    formatType = ExportFormatType.PortableDocFormat;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reportterbayarpdf");
                                    break;
                                case "4":
                                    formatType = ExportFormatType.Excel;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reportterbayarxcel");
                                    break;
                                case "5":
                                    formatType = ExportFormatType.CharacterSeparatedValues;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "reportterbayarcsv");
                                    break;
                            }



                            Response.End();


                        }
                        else if (ge.ListDataTable.Rows.Count <= 0)

                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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


                }

            }


            
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {

        try
        {
            //System.Threading.Thread.Sleep(5000);
            ReportDocument rprt = new ReportDocument();
            if (ddldata.SelectedValue == "0")
            {

                DataTable ds = new DataTable();
                ds = null;
                Grdterbayar.DataSource = ds;
                Grdterbayar.DataBind();
                Grdterbayarmrk.DataSource = ds;
                Grdterbayarmrk.DataBind();
                Grdtagihan.DataSource = ds;
                Grdtagihan.DataBind();
                Grdtagihanmrk.DataSource = ds;
                Grdtagihanmrk.DataBind();
                Grdtunggakan.DataSource = ds;
                Grdtunggakan.DataBind();
                Grdtunggakanmrk.DataSource = ds;
                Grdtunggakanmrk.DataBind();

                if (Lbltaspen.Text == "DATA MARKETING MIKRO / TAGIHAN")
                {
                    if (ddlmarketing.SelectedValue != "ALL")
                    {
                        Pnltagihan.Visible = true;
                        Pnltagihanmrk.Visible = false;
                        BindGridEntityTagihan();
                    }
                    else
                    {
                        Pnltagihan.Visible = true;
                        Pnltagihanmrk.Visible = false;
                        BindGridEntityTagihan();

                    }


                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TUNGGAKAN")
                {

                    if (ddlmarketing.SelectedValue != "ALL")
                    {
                        Pnltunggakan.Visible = true;
                        Pnltunggakanmrk.Visible = false;
                        BindGridEntityTunggakan();


                    }
                    else
                    {
                        Pnltunggakan.Visible = true;
                        Pnltunggakanmrk.Visible = false;
                        BindGridEntityTunggakan();

                    }


                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TERBAYAR")
                {

                    if (ddlmarketing.SelectedValue != "ALL")
                    {
                        Pnlterbayar.Visible = true;
                        Pnlterbayarmrk.Visible = false;
                        BindGridEntityterbayar();
                    }
                    else
                    {
                        Pnlterbayar.Visible = true;
                        Pnlterbayarmrk.Visible = false;
                        BindGridEntityterbayar();

                    }


                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


            }
            else
            {
                if (Lbltaspen.Text == "DATA MARKETING MIKRO / TAGIHAN")
                {
                    //btnSearch_Click();

                    try
                    {


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TAGIHAN", typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                        //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            rprt.Load(Server.MapPath("~/AR/View/rpttagihanRekap.rpt"));

                            rprt.SetDataSource(ge.ListDataTable);
                            rprt.SetParameterValue("Userprint", SessLoginID);
                            rprt.SetParameterValue("xPeriode", txtTglL.Text);
                            rprt.SetParameterValue("Produk", ddlproduk.SelectedItem.Text);
                            rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                            //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                            Response.ClearContent();
                            Response.ClearHeaders();
                            ////ExportFormatType formatType = ExportFormatType.NoFormat;
                            //Response.ContentType = "application/pdf";
                            //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            ExportFormatType formatType = ExportFormatType.NoFormat;
                            switch (ddldata.SelectedItem.Value)
                            {
                                case "1":
                                    Response.ContentType = "application/pdf";
                                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                    break;
                                case "2":
                                    formatType = ExportFormatType.WordForWindows;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanword");
                                    break;
                                case "3":
                                    formatType = ExportFormatType.PortableDocFormat;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanpdf");
                                    break;
                                case "4":
                                    formatType = ExportFormatType.Excel;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanexcel");
                                    break;
                                case "5":
                                    formatType = ExportFormatType.CharacterSeparatedValues;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihancsv");
                                    break;
                            }


                            Response.End();


                        }
                        else if (ge.ListDataTable.Rows.Count <= 0)

                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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


                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TUNGGAKAN")
                {

                    try
                    {


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TUNGGAKANCST", typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                        //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            rprt.Load(Server.MapPath("~/AR/View/rpttunggakanRekap.rpt"));

                            rprt.SetDataSource(ge.ListDataTable);
                            rprt.SetParameterValue("Userprint", SessLoginID);
                            rprt.SetParameterValue("xPeriode", txtTglL.Text);
                            rprt.SetParameterValue("Produk", ddlproduk.SelectedItem.Text);
                            rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                            //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                            Response.ClearContent();
                            Response.ClearHeaders();
                            ////ExportFormatType formatType = ExportFormatType.NoFormat;
                            //Response.ContentType = "application/pdf";
                            //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");

                            ExportFormatType formatType = ExportFormatType.NoFormat;
                            switch (ddldata.SelectedItem.Value)
                            {
                                case "1":
                                    Response.ContentType = "application/pdf";
                                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                    break;
                                case "2":
                                    formatType = ExportFormatType.WordForWindows;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanword");
                                    break;
                                case "3":
                                    formatType = ExportFormatType.PortableDocFormat;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanpdf");
                                    break;
                                case "4":
                                    formatType = ExportFormatType.Excel;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanexcel");
                                    break;
                                case "5":
                                    formatType = ExportFormatType.CharacterSeparatedValues;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihancsv");
                                    break;
                            }

                            Response.End();


                        }
                        else if (ge.ListDataTable.Rows.Count <= 0)

                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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


                }
                else if (Lbltaspen.Text == "DATA MARKETING MIKRO / TERBAYAR")
                {

                    try
                    {


                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                        var[0] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlproduk.SelectedValue, typeof(string));
                        var[1] = gb.CreateSPParameter("@marketing", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
                        var[2] = gb.CreateSPParameter("@periode", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "TERBAYAR", typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                        var[5] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[6] = gb.CreateSPParameter("@angs1", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglL.Text, typeof(DateTime));
                        var[7] = gb.CreateSPParameter("@angs2", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtTglS.Text, typeof(DateTime));

                        //ge.ListDataset = gb.GetDataJustOutPut("AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        ge.ListDataTable = gb.GetDataJustOutPut("dbo.AR_STD_RPT_MARKETING", var, "constrmf").Tables[0];
                        if (ge.ListDataTable.Rows.Count > 0)
                        {
                            rprt.Load(Server.MapPath("~/AR/View/rptterbayarRekap.rpt"));

                            rprt.SetDataSource(ge.ListDataTable);
                            rprt.SetParameterValue("Userprint", SessLoginID);
                            rprt.SetParameterValue("xPeriode", txtTglL.Text + " to " + txtTglS.Text);
                            rprt.SetParameterValue("Produk", ddlproduk.SelectedItem.Text);
                            rprt.SetParameterValue("Cabang", ddlcabang.SelectedItem.Text);

                            //rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(GTotal.ToString())));
                            Response.ClearContent();
                            Response.ClearHeaders();
                            ////ExportFormatType formatType = ExportFormatType.NoFormat;
                            //Response.ContentType = "application/pdf";
                            //rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                            ExportFormatType formatType = ExportFormatType.NoFormat;
                            switch (ddldata.SelectedItem.Value)
                            {
                                case "1":
                                    Response.ContentType = "application/pdf";
                                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                                    break;
                                case "2":
                                    formatType = ExportFormatType.WordForWindows;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanword");
                                    break;
                                case "3":
                                    formatType = ExportFormatType.PortableDocFormat;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanpdf");
                                    break;
                                case "4":
                                    formatType = ExportFormatType.Excel;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihanexcel");
                                    break;
                                case "5":
                                    formatType = ExportFormatType.CharacterSeparatedValues;
                                    rprt.ExportToHttpResponse(formatType, Response, true, "Reporttagihancsv");
                                    break;
                            }

                            Response.End();


                        }
                        else if (ge.ListDataTable.Rows.Count <= 0)

                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Yang anda Pilih Tidak ada')", true);
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


                }

            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btntagihan_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = true;

        //DataTable ds = new DataTable();
        //ds = null;
        //Grdtagihan.DataSource = ds;
        //Grdtagihan.DataBind();
        ////Grdtagihanmrk.DataSource = ds;
        ////Grdtagihanmrk.DataBind();
        ////Grdtagihan.SelectedIndex = -1;
        ////ddlmarketing.Enabled = false;
        
        txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
        //ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));
        txtTglS.Enabled = false;
        Lbltaspen.Text = "DATA MARKETING MIKRO / TAGIHAN";

        Pnltagihan.Visible = false;
        Pnltagihanmrk.Visible = false;
        Pnltunggakan.Visible = false;
        Pnltunggakanmrk.Visible = false;
        Pnlterbayar.Visible = false;
        Pnlterbayarmrk.Visible = false;




    }
    protected void btntunggakan_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = true;

        //DataTable ds = new DataTable();
        //ds = null;
        //Grdtunggakan.DataSource = ds;
        //Grdtunggakan.DataBind();
        //Grdtunggakanmrk.DataSource = ds;
        //Grdtunggakanmrk.DataBind();
        //  ddlmarketing.Enabled = false;
        txtTglS.Enabled = false;
        // txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
        //ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));

        Lbltaspen.Text = "DATA MARKETING MIKRO / TUNGGAKAN";

        Pnltagihan.Visible = false;
        Pnltagihanmrk.Visible = false;
        Pnltunggakan.Visible = false;
        Pnltunggakanmrk.Visible = false;
        Pnlterbayar.Visible = false;
        Pnlterbayarmrk.Visible = false;
        
    }

    protected void btnterbayar_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = true;
        txtTglS.Enabled = true;
        //DataTable ds = new DataTable();
        //ds = null;
        //Grdterbayar.DataSource = ds;
        //Grdterbayar.DataBind();
        //Grdterbayarmrk.DataSource = ds;
        //Grdterbayarmrk.DataBind();

       // ddlmarketing.Enabled = false;

      //  txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
     //   ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));

        Lbltaspen.Text = "DATA MARKETING MIKRO / TERBAYAR";

        Pnltagihan.Visible = false;
        Pnltagihanmrk.Visible = false;
        Pnltunggakan.Visible = false;
        Pnltunggakanmrk.Visible = false;
        Pnlterbayar.Visible = false;
        Pnlterbayarmrk.Visible = false;
        
    }

    protected void btnlisttagihan_Click(object sender, EventArgs e)
    {
        //Pnlsearch.Visible = true;
        txtTglS.Enabled = false;
        //DataTable ds = new DataTable();
        //ds = null;
        //Grdtagihanmrk.DataSource = ds;
        //Grdtagihanmrk.DataBind();

        txtTglL.Text = DateTime.Now.ToString("dd-MM-yyyy");
      

        Lbltaspen.Text = "DATA MARKETING MIKRO / TAGIHAN MARKETING";

        Pnltagihan.Visible = false;
        Pnltagihanmrk.Visible = false;
        Pnltunggakan.Visible = false;
        Pnltunggakanmrk.Visible = false;
        Pnlterbayar.Visible = false;
        Pnlterbayarmrk.Visible = false;
        Pnltagihanmrk.Visible = false;


    }
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {



        DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[2];
        varx[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
        varx[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlcabang.SelectedValue, typeof(string));
        //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_Marketing_List", varx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";
        ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlmarketing.DataTextField = "Text";
        ddlmarketing.DataValueField = "value";
        ddlmarketing.DataBind();
        ddlmarketing.Items.Insert(0, new ListItem("ALL", "ALL"));


    }

    private Decimal orderTotal = 0.0M;

    void OrderGridView_RowCreated(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            // Get the OrderTotalLabel Label control in the footer row.
            Label total = (Label)e.Row.FindControl("jumlah_nasabah");

            // Display the grand total of the order formatted as currency.
            if (total != null)
            {
                total.Text = orderTotal.ToString("c");
            }

        }

    }

    void OrderGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            // Get the cell that contains the item total.
            TableCell cell = e.Row.Cells[2];

            // Get the DataBoundLiteralControl control that contains the 
            // data-bound value.
            DataBoundLiteralControl boundControl = (DataBoundLiteralControl)cell.Controls[0];

            // Remove the '$' character so that the type converter works properly.
            String itemTotal = boundControl.Text.Replace("$", "");

            // Add the total for an item (row) to the order total.
            orderTotal += Convert.ToDecimal(itemTotal);

        }

    }
  

}