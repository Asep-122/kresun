using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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

public partial class AE_Slip_CetakSlipfee : System.Web.UI.Page
{
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
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
                ambilBulan();
                ambilTahun();
                bindgridview();
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 4; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddltahun.Items.Add((currentYear - i).ToString());
        }
        ddltahun.SelectedValue = currentYear.ToString();

    }

    public void ambilBulan()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length; i++)
        {
            ddlSP.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
        }
        var currentmonth = DateTime.Today.Month;
        ddlSP.SelectedValue = currentmonth.ToString();

    }
    private void bindgridview()
    {
       
        DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
        Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));


        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetCabangReport", Paramsx, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabang, "None", "");
        ddlcabang.SelectedValue = SessBranchID;

        if (SessBranchID == "000"){
            ddlcabang.Enabled = true;
        }
        else
        {
            ddlcabang.Enabled = false;
        }

        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETBAGIAN", typeof(string));
        var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var2[2] = gb.CreateSPParameter("@kdgol", ParameterDirection.Input, SqlDbType.VarChar, 50, "MK", typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AE_GAJI_SELECT", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

        ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlgroup.DataTextField = "kdket";
        ddlgroup.DataValueField = "KodeBagian";
        ddlgroup.DataBind();
        ddlgroup.Items.Insert(0, new ListItem("Pilih Data", "-"));

    }
    decimal totalBM = 0, totalPL = 0, totalKMK = 0, totalTunggakan = 0,
    totalSMTerbaik = 0, totalReward = 0, totalRewardBM = 0,totalNewMembers = 0,
    totalNewMembersharian = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalbm"));
            totalPL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "plreward"));
            totalKMK += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "KMK"));
            totalTunggakan += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TP"));
            totalSMTerbaik += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SMTERBAIK"));
            totalReward += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalreward"));
            totalRewardBM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalrewardbm"));

            totalNewMembers += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "newmembers"));
            totalNewMembersharian += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "newmembersharian"));
            }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalBM = (Label)e.Row.FindControl("lbltotalBM");
            lbltotalBM.Text = string.Format("{0:N2}", totalBM);

            Label lbltotalPL = (Label)e.Row.FindControl("lbltotalPL");
            lbltotalPL.Text = string.Format("{0:N2}", totalPL);

            Label lbltotalKMK = (Label)e.Row.FindControl("lbltotalKMK");
            lbltotalKMK.Text = string.Format("{0:N2}", totalKMK);

            Label lbltotalTunggakan = (Label)e.Row.FindControl("lbltotalTunggakan");
            lbltotalTunggakan.Text = string.Format("{0:N2}", totalTunggakan);

            Label lbltotalSMTerbaik = (Label)e.Row.FindControl("lbltotalSMTerbaik");
            lbltotalSMTerbaik.Text = string.Format("{0:N2}", totalSMTerbaik);


            Label lbltotalReward = (Label)e.Row.FindControl("lbltotalReward");
            lbltotalReward.Text = string.Format("{0:N2}", totalReward);

            Label lbltotalRewardBM = (Label)e.Row.FindControl("lbltotalRewardBM");
            lbltotalRewardBM.Text = string.Format("{0:N2}", totalRewardBM);

            Label lbltotalNEWMEMBERSReward = (Label)e.Row.FindControl("lbltotalNEWMEMBERSReward");
            lbltotalNEWMEMBERSReward.Text = string.Format("{0:N2}", totalNewMembers);

                Label lbltotalNEWMEMBERSHarian = (Label)e.Row.FindControl("lbltotalNEWMEMBERSharian");
                lbltotalNEWMEMBERSHarian.Text = string.Format("{0:N2}", totalNewMembersharian);

                totalBM = 0; totalPL = 0; totalKMK = 0; totalTunggakan = 0;
            totalSMTerbaik = 0; totalReward = 0; totalRewardBM = 0;
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

         
            }
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        //ReportDocument rprt = new ReportDocument();
        try
        {

            PNLMKT.Visible = true;
            DefaultClass.Global.SqlHelper[] Paramsx4 = new DefaultClass.Global.SqlHelper[7];
            Paramsx4[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlSP.SelectedValue, typeof(string));
            Paramsx4[1] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsx4[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Paramsx4[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATAFEEREWARDSLIP", typeof(string));
            Paramsx4[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Paramsx4[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddltahun.SelectedValue, typeof(string));
            Paramsx4[6] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlgroup.SelectedValue, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("AE_SP_REWARD", Paramsx4, "constrmf").Tables[0];

            if (ge.ListDataTable.Rows.Count > 0)
            {

                ViewState["ListDetailRP"] = ge.ListDataTable;

                GRData.DataSource = ge.ListDataTable;
                GRData.DataBind();

                GRData.UseAccessibleHeader = true;
                GRData.HeaderRow.TableSection = TableRowSection.TableHeader;


            }
            else
            {

            }

            //GRData.Enabled=false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('test3!');", true);



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

    //protected void btnSimpanx_Click(object sender, EventArgs e)
    //{
    //    ReportDocument rprt = new ReportDocument();
    //    try
    //    {
    //        string tanggaljth = "";
    //        string jumlahangsuran = "";
    //        string angsuran = "";
    //        string jlhangsuran = "";


    //        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

    //        var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnikawal.SelectedValue, typeof(string));
    //        var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGAJI", typeof(string));
    //        var[3] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSP.SelectedValue, typeof(string));
    //        var[4] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 20, ddltahun.SelectedValue, typeof(string));

    //        ge.ListDataTable = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf").Tables[0];
    //        ge.ListDataset = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf");
    //        if(ge.ListDataset.Tables[0].Rows.Count == 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:KosongAlert('Data Gaji Pada Bulan ', '"+ ddlnikawal.SelectedItem +"');", true);
    //        }
    //        else 
    //        {
    //            string rubahangsuran = "0";

    //            rprt.Load(Server.MapPath("~/AE/View/Report/rptSlipGaji.rpt"));
    //            rprt.SetDataSource(ge.ListDataTable);
    //            rprt.SetParameterValue("tanggal_cetak", DateTime.Now.ToString("dd-MM-yyyy"));
    //            rprt.SetParameterValue("jlhangsuran", rubahangsuran);
    //            rprt.SetParameterValue("period", ddlSP.SelectedValue);

    //            Response.ClearContent();
    //            Response.ClearHeaders();

    //            Response.ContentType = "application/pdf";
    //            rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


    //            Response.End();


    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('test3!');", true);
    //        }


    //    }
    //    catch (Exception X)
    //    {
    //        if (X.Message != "Thread was being aborted.")
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
    //        }
    //        else
    //        {

    //            rprt.Close();
    //            rprt.Dispose();
    //        }
    //    }


    //}

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {

           
            case "SLIP":

                try
                {

                   
                    string nobukti;
                    string nik;
                    nobukti = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;
                    nik = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoanggota")).Text;
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];

                    var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, nik, typeof(string));
                    var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFEEREWARD", typeof(string));
                    var[3] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSP.SelectedValue, typeof(string));
                    var[4] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 20, ddltahun.SelectedValue, typeof(string));
                    var[5] = gb.CreateSPParameter("@NoBukti", ParameterDirection.Input, SqlDbType.VarChar, 30, nobukti, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf").Tables[0];
                    ge.ListDataset = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:KosongAlert('Data Fee dan Gaji Kosong Pada Bulan ', '" + ddlSP.SelectedItem + "');", true);
                    }
                    else
                    {
                        string rubahangsuran = "0";

                        rprt.Load(Server.MapPath("~/AE/View/Report/rptSlipFeeReward.rpt"));
                        rprt.SetDataSource(ge.ListDataTable);
                        rprt.SetParameterValue("tanggal_cetak", DateTime.Now.ToString("dd-MM-yyyy"));
                        //rprt.SetParameterValue("jlhangsuran", rubahangsuran);
                        //rprt.SetParameterValue("period", ddlSP.SelectedValue);

                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");


                        Response.End();


                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('test3!');", true);
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


}