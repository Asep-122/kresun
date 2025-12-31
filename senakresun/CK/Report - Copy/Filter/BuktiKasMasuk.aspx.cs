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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.Services;
 

public partial class CL_Report_Filter_BuktiKasMasuk : System.Web.UI.Page
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
    private string SessNamaProfil
    {
        get { return Session["SessNamaProfil"].ToString(); }
        set { ViewState["SessNamaProfil"] = value; }
    }
    private string SessAlamatProfil1
    {
        get { return Session["SessAlamatProfil1"].ToString(); }
        set { ViewState["SessAlamatProfil1"] = value; }
    }
    private string SessAlamatProfil2
    {
        get { return Session["SessAlamatProfil2"].ToString(); }
        set { ViewState["SessAlamatProfil2"] = value; }
    }
    private string SessKotaProfil
    {
        get { return Session["SessKotaProfil"].ToString(); }
        set { ViewState["SessKotaProfil"] = value; }
    }
     
    private string SessNegaraProfil
    {
        get { return Session["SessNegaraProfil"].ToString(); }
        set { ViewState["SessNegaraProfil"] = value; }
    }

    private string SessTeleponProfil
    {
        get { return Session["SessTeleponProfil"].ToString(); }
        set { ViewState["SessTeleponProfil"] = value; }
    }
    private string SessFaxProfil
    {
        get { return Session["SessFaxProfil"].ToString(); }
        set { ViewState["SessFaxProfil"] = value; }
    }

    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnSearch);

                BindGridEntity();

            } 
        }
        catch (System.Exception X)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
     
    public void BindGridEntity()
    {
        ddlnobuktiAwal.Enabled = false;
        ddlnobuktiAkhir.Enabled = false;
        ddlkdcrc.Enabled = false;
        txtsetor.Enabled = false;
        txtkassa.Enabled = false;
        txtbuku.Enabled = false;
        txtsetuju.Enabled = false;
        txtketahui.Enabled = false;
    }
    public void btnSearch_Click(object sender, EventArgs e)
    {
        string totalval = ""; var title = ""; var state = "";
        string ddl = "";

        state = "REPORTKASBANK";
        if (ddljeniskas.SelectedValue == "1")
        {
            title = "BUKTI KAS MASUK";
            ddl = "1";
        }
        else if (ddljeniskas.SelectedValue == "2")
        {
            title = "BUKTI KAS KELUAR";
            ddl = "2";
        } 
        try
        {
            //DateTime ambilawal = Convert.ToDateTime(txtStart.Text); 
            DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[7];
            Paramsc[0] = gb.CreateSPParameter("@KodeGolonganawal", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnobuktiAwal.SelectedValue, typeof(string));
            Paramsc[1] = gb.CreateSPParameter("@KodeGolonganakhir", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnobuktiAkhir.SelectedValue, typeof(string));
            Paramsc[2] = gb.CreateSPParameter("@kdCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlkdcrc.SelectedValue, typeof(string));
            Paramsc[3] = gb.CreateSPParameter("@ddl", ParameterDirection.Input, SqlDbType.VarChar, 1, ddl, typeof(string));
            Paramsc[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, state, typeof(string));
            Paramsc[5] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            Paramsc[6] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("dbo.CL_ReportNKmasuk", Paramsc, "constrmf").Tables[0];

            float GTotal = 0f;
            for (int i = 0; i < ge.ListDataTable.Rows.Count; i++)
            { 
                String total = ge.ListDataTable.Rows[i]["JumlahTrn"].ToString();  
                GTotal += Convert.ToSingle(total);
            }
            string formatted = string.Format("{0:N2}", GTotal);
            totalval = GTotal.ToString();
            
            if (ge.ListDataTable.Rows.Count > 0)
            {
                ReportDocument rprt = new ReportDocument();

                //var path = Server.MapPath("~/CL/Report/Report/CL/CL.STD.RPT.002.rpt");
                rprt.Load(Server.MapPath("~/CL/Report/Report/CL/CL.STD.RPT.002.rpt"));
                rprt.SetDataSource(ge.ListDataTable);
                rprt.SetParameterValue("Reportname", title);
                rprt.SetParameterValue("NamaPT", SessNamaProfil);
                rprt.SetParameterValue("Alamat1", SessAlamatProfil1);
                rprt.SetParameterValue("Alamat2", SessAlamatProfil2);
                rprt.SetParameterValue("Alamat3", SessKotaProfil);
                //rprt.SetParameterValue("Crc", ddlkdcrc.SelectedValue);
                rprt.SetParameterValue("Mengetahui", txtketahui.Text);
                rprt.SetParameterValue("Disetujui", txtsetuju.Text);
                rprt.SetParameterValue("Keuangan", txtbuku.Text);
                rprt.SetParameterValue("Kassa", txtkassa.Text);
                rprt.SetParameterValue("Disetor", txtsetor.Text);
                rprt.SetParameterValue("Tot", formatted);
                rprt.SetParameterValue("Jumlah", gb.Terbilang(decimal.Parse(formatted.ToString().Replace("-",""))));
                Response.ClearContent();
                Response.ClearHeaders();
                ExportFormatType formatType = ExportFormatType.NoFormat;
                switch (DdlReport.SelectedItem.Value)
                {
                    case "1":
                        Response.ContentType = "application/pdf";
                        rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                        break;
                    case "2":
                        formatType = ExportFormatType.WordForWindows;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldoword");
                        break;
                    case "3":
                        formatType = ExportFormatType.PortableDocFormat;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldopdf");
                        break;
                    case "4":
                        formatType = ExportFormatType.Excel;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldoexcel");
                        break;
                    case "5":
                        formatType = ExportFormatType.CharacterSeparatedValues;
                        rprt.ExportToHttpResponse(formatType, Response, true, "Daftarakunsaldocsv");
                        break;
                }

                Response.Flush();
                Response.Close();
                Response.End();
                    


            }
            else if (ge.ListDataTable.Rows.Count <= 0)

            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Pada Rentang " + "" + "" + ddlnobuktiAwal.SelectedValue + "" + "" + "  s/d  " + "" + "" + ddlnobuktiAkhir.SelectedValue + " Yang anda Pilih Tidak ada')", true);
            }




        }

        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }

        }
    }
    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;

        //string nowx;
        //nowx = gb.ReadOrderData();
        //DateTime now = DateTime.Parse(nowx);
        //int tahun;
        //tahun = now.Year;
        //DateTime date = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        //DateTime awaltahun = Convert.ToDateTime(date);
        //int ambilawaltahun = Convert.ToDateTime(date).Year;
        ////ddlStart.Text = awaltahun.ToShortDateString();

        //// txtEnd.Text = awaltahun.ToString();
        //DateTime xdate = new DateTime(now.Year, Int32.Parse(ddlSP.SelectedValue), 1);
        //int ambilakhirtahun = Convert.ToDateTime(xdate).Year;
        //xdate = date.AddMonths(1);
        //xdate = xdate.AddDays(-1);
        ////txtEnd.Text = xdate.ToString("dd/MM/yyyy");
        ////txtEnd.Text = ambilakhirtahun.ToString("yyyy");
        //Txtrptperiode.Text = "Periode : " + ddlSP.SelectedItem + ' ' + ddlStart.SelectedValue;

        //txtrpttitle.Text = "Report Daftar Penyusutan Per-Group";

    }

    protected void JenisKas_SelectedIndexChanged(object sender,EventArgs e)
    {

        var state = "";
        string ddl = "";
        if (ddljeniskas.SelectedValue == "1")
        {
            state = "SELECTNOBUKTI";
            ddl = "1";
        }
        else if (ddljeniskas.SelectedValue == "2")
        {
            state = "SELECTNOBUKTI";
            ddl = "2";
        }

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@ddl", ParameterDirection.Input, SqlDbType.VarChar, 40, ddl, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, state, typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Input, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ReportNKmasuk", var, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";
        ddlnobuktiAwal.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlnobuktiAwal.DataTextField = "notrn";
        ddlnobuktiAwal.DataValueField = "nobukti";
        ddlnobuktiAwal.DataBind();
        ddlnobuktiAwal.Items.Insert(0, new ListItem("Pilih Data", "-"));


        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@ddl", ParameterDirection.Input, SqlDbType.VarChar, 40, ddl, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, state, typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Input, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_ReportNKmasuk", var1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "nobukti asc";
        ddlnobuktiAkhir.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlnobuktiAkhir.DataTextField = "notrn";
        ddlnobuktiAkhir.DataValueField = "nobukti";
        ddlnobuktiAkhir.DataBind();
        ddlnobuktiAkhir.Items.Insert(0, new ListItem("Pilih Data", "-"));


        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
        var2[0] = gb.CreateSPParameter("@kdCrc", ParameterDirection.Input, SqlDbType.VarChar, 40, "-", typeof(string));
        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTKDCRC", typeof(string));
        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Input, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("CL_ReportNKmasuk", var2, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";
        ddlkdcrc.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlkdcrc.DataTextField = "kdnm";
        ddlkdcrc.DataValueField = "KodeCrc";
        ddlkdcrc.DataBind();
        ddlkdcrc.Items.Insert(0, new ListItem("Pilih Data", "-"));

        if (SessSystemKurs == "False")
        {
            ddlkdcrc.Items.Insert(0, new ListItem("IDR"));
            ddlkdcrc.Enabled = false; 

        }
        else
        {
            ddlkdcrc.Enabled = true; 
        }
        ddlnobuktiAwal.Enabled = true;
        ddlnobuktiAkhir.Enabled = true;  

        DefaultClass.Global.SqlHelper[] vargetdata = new DefaultClass.Global.SqlHelper[4];
        vargetdata[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETDATAREPORT", typeof(string));
        vargetdata[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        vargetdata[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        vargetdata[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[CL_ReportNKmasuk]", vargetdata, "constrmf");
        txtsetor.Text = ge.ListDataset.Tables[0].Rows[0]["CL_SETOROLEH"].ToString();
        txtkassa.Text = ge.ListDataset.Tables[0].Rows[0]["CL_KASSA"].ToString();
        txtbuku.Text = ge.ListDataset.Tables[0].Rows[0]["CL_BAGIANPEMBUKUAN"].ToString();
        txtsetuju.Text = ge.ListDataset.Tables[0].Rows[0]["CL_DISETUJUIOLEH"].ToString();
        txtketahui.Text = ge.ListDataset.Tables[0].Rows[0]["CL_MENGETAHUI"].ToString();
    }
}