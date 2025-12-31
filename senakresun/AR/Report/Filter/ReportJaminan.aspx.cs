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
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class AR_Report_Jaminan : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global(); 
    GlobalEntities ge = new GlobalEntities();

    public string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }

    public string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                DateTime now = DateTime.Now;
                txttgl.Text = now.ToString("yyyy-MM-dd");
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[2];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKOPERASI", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_koperasi asc";
                ddlKoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKoperasi.DataTextField = "nama_koperasi";
                ddlKoperasi.DataValueField = "kode_koperasi";
                ddlKoperasi.DataBind();
                ddlKoperasi.Items.Insert(0, new ListItem("Pilih Data", "-"));


                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "nama_cabang";
                ddlcabang.DataValueField = "kode_cabang";
                ddlcabang.DataBind();
                ddlcabang.Items.Insert(0, new ListItem("ALL Cabang", ""));
                ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlcabang.Enabled = false;

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[2];
                var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPRODUK", typeof(string));
                var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", var2, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "namaproduk asc";
                ddlProduk.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlProduk.DataTextField = "namaproduk";
                ddlProduk.DataValueField = "kode_produk";
                ddlProduk.DataBind();
                ddlProduk.Items.Insert(0, new ListItem("ALL Product", ""));
                ddlProduk.Items.Insert(0, new ListItem("Pilih Data", "-"));

                if (SessBranchID == "000" || SessBranchID == "203")
                {
                    ddlKoperasi.Enabled = true;
                    //ddlcabang.Enabled = true;
                }
                else
                {
                    ddlcabang.SelectedValue = SessBranchID;
                    ddlKoperasi.SelectedValue = SessKoperasiID;
                    //var Koperasi = SessKoperasiID;
                    //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    //var1[0] = gb.CreateSPParameter("@koperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                    //var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                    //var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", var1, "constrmf");
                    //ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                    //ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    //ddlcabang.DataTextField = "nama_cabang";
                    //ddlcabang.DataValueField = "kode_cabang";
                    //ddlcabang.DataBind();
                    //ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));
                    ddlKoperasi.Enabled = false;
                    ddlcabang.Enabled = false;
                }
            }
            if (ddlKoperasi.SelectedValue == "-" || ddlcabang.SelectedValue == "-" || ddlProduk.SelectedValue == "-")
            {
                ddlProduk.SelectedValue = "-";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnCetakAnggota_Click(object sender, EventArgs e)
    {
		ReportDocument rprt = new ReportDocument();
        try
        {
            //string asdas = hdnZipCode.va
            if(ddlProduk.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Produk Terlebih Dahulu.');", true);
            }
            else if(ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Cabang Terlebih Dahulu.');", true);
            }
            else if(ddlstatus.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Silahkan Pilih Status Jaminan Terlebih Dahulu.');", true);
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                var1[0] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlstatus.SelectedValue, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "REPORTJAMINAN", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[3] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgl.Text, typeof(string));
                var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                var1[5] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlProduk.SelectedValue, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", var1, "constrmf");
                ge.ListDataTable = gb.GetDataJustOutPut("View_Cetak_Cabang", var1, "constrmf").Tables[0];
                if (ge.ListDataTable.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Peride Pada Tanggal yang anda tidak ada History Jaminan, Silahkan Pilih Tanggal Lain.');", true);
                }
                else
                {
                    string masuktanggal = Convert.ToDateTime(txttgl.Text).ToString("dd MMMM yyyy");
                    string namacabang = ddlcabang.SelectedValue.ToString();
                    string gtes = ddlcabang.SelectedItem.Text;
                    rprt.Load(Server.MapPath("~/AR/Report/Report/ReportJaminan.rpt"));
                    rprt.SetDataSource(ge.ListDataTable);
                    rprt.SetParameterValue("koperasi", ddlKoperasi.SelectedItem.Text);
                    rprt.SetParameterValue("Periode", masuktanggal);
                    rprt.SetParameterValue("usersearch", SessLoginID);
                    rprt.SetParameterValue("Judul", "Laporan Jaminan Pinjaman");
                    rprt.SetParameterValue("cabang", gtes);
                    rprt.SetParameterValue("produk", ddlProduk.SelectedItem.Text);
                    rprt.SetParameterValue("status", ddlstatus.SelectedItem.Text);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    rprt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedFile");
                    Response.End();
                }
            }
		}
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ex.Message.ToString() + "');", true);
            }else
			{
                rprt.Close();
                rprt.Dispose();
                GC.Collect();
            }

        }
    }
    protected void ddlKoperasi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlKoperasi.SelectedValue == "-")
            {
                ddlcabang.SelectedValue = "-";
                ddlcabang.Enabled = false;
            }
            else
            {
                ddlcabang.Enabled = true;
                var Koperasi = ddlKoperasi.SelectedValue;
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                var[0] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 15, Koperasi, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARICABANG", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("View_Cetak_Cabang", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "nama_cabang";
                ddlcabang.DataValueField = "kode_cabang";
                ddlcabang.DataBind();
                ddlcabang.Items.Insert(0, new ListItem("ALL Cabang", ""));
                ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));
            }
            if (ddlKoperasi.SelectedValue == "-" || ddlcabang.SelectedValue == "-" || ddlProduk.SelectedValue == "-")
            {
                ddlProduk.SelectedValue = "-";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlKoperasi.SelectedValue == "-" || ddlcabang.SelectedValue == "-" || ddlProduk.SelectedValue == "-")
            {
                ddlProduk.SelectedValue = "-";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    [WebMethod]
    public static string GetNoNasabah(string InputText, string SessionBranchID)
    {
        List<string[]> listResult = new List<string[]>();

        string no_nasabah =  InputText;
        
        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[1] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, no_nasabah, typeof(String));
        Params[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARINASABAH", typeof(string));
        Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.View_Cetak_Cabang", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }
}