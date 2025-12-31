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

public partial class AE_Slip_Gaji : System.Web.UI.Page
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
                //DateTime now = DateTime.Now;
                //string ATahun = now.ToString("yyyy");
                //string ABulan = now.ToString("MM");
                //int rubahtahun = Int32.Parse(ATahun);
                //int rubahbulan = Int32.Parse(ABulan);
                //DateTime.DaysInMonth(rubahtahun, rubahbulan);
                //String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
                //string rubahtanggal = rubahhari + "/" + ABulan + "/" + ATahun;
                //int year = DateTime.Now.Year;
                //for (int i = year - 10; i <= year + 10; i++)
                //{
                //    ListItem li = new ListItem(i.ToString());
                //    ddltahun.Items.Add(li);
                //}
                //ddltahun.Items.FindByText(year.ToString()).Selected = true;
                bindgridview();
                ambilTahun();
                ambilBulan();
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
        DefaultClass.Global.SqlHelper[] Paramsx1 = new DefaultClass.Global.SqlHelper[4];
        Paramsx1[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Paramsx1[1] = gb.CreateSPParameter("@Kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        Paramsx1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "SELECTNIK", typeof(string));
        Paramsx1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GAJI_SELECT", Paramsx1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";
        gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnikawal, "None", "");
        //gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlnikakhir, "None", "");
        ddlnikawal.Items.Insert(0, new ListItem("Pilih NIK Karyawan", "-"));
        //ddlnikakhir.Items.Insert(0, new ListItem("-", "-"));
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        try
        {
            string tanggaljth = "";
            string jumlahangsuran = "";
            string angsuran = "";
            string jlhangsuran = "";
            
            
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];

            var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnikawal.SelectedValue, typeof(string));
            var[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGAJI", typeof(string));
            var[3] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSP.SelectedValue, typeof(string));
            var[4] = gb.CreateSPParameter("@Tahun", ParameterDirection.Input, SqlDbType.VarChar, 20, ddltahun.SelectedValue, typeof(string));

            ge.ListDataTable = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf").Tables[0];
            //ge.ListDataset = gb.GetDataJustOutPut("AE_CETAKAN", var, "constrmf");
            if(ge.ListDataTable.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:KosongAlert('Data Gaji Pada Bulan ', '"+ ddlnikawal.SelectedItem +"');", true);
            }
            else 
            {
                string rubahangsuran = "0";
               
                rprt.Load(Server.MapPath("~/AE/View/Report/rptSlipGaji.rpt"));
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("tanggal_cetak", DateTime.Now.ToString("dd-MM-yyyy"));
                rprt.SetParameterValue("jlhangsuran", rubahangsuran);
                rprt.SetParameterValue("period", ddlSP.SelectedValue);

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


    }

}