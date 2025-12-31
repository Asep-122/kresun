using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Globalization;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DocumentFormat.OpenXml.Spreadsheet;

public partial class Emailsales : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    Global gbx = new DefaultClass.Global();
    GlobalEntities gex = new GlobalEntities();

    Global gb1 = new Global();
    GlobalEntities ge1 = new GlobalEntities();

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
    //public string sesskodebagian = "A";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                txtkodecabang.Text = SessBranchID;
                Group1_CheckedChanged(sender, e);
                GRData.UseAccessibleHeader = true;
                BindGridEntity();
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";
        }
        catch (Exception X)
        {
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void BindGridEntity()
    {
        try
        {
            string jeniskaryawan = rbJnsKaryawan.SelectedValue;
            Global.SqlHelper[] var = new Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@KodeKoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKoperasiID, typeof(string));
            var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[2] = gb.CreateSPParameter("@JnsKaryawan", ParameterDirection.Input, SqlDbType.VarChar, 15, jeniskaryawan, typeof(string));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPERBAGIAN", typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SU_Email", var, "constrmf");

            HDKodewilayah.Value = ge.ListDataset.Tables[0].Rows[0]["Kode_wilayah"].ToString();

            if (rbJnsKaryawan.SelectedValue == "Manager")
            {
                ge.ListDataset.Tables[0].DefaultView.Sort = "kodemc asc";
                GRData3.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRData3.DataBind();

                GRData3.UseAccessibleHeader = true;

                GRData3.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (rbJnsKaryawan.SelectedValue == "KU")
            {
                ge.ListDataset.Tables[0].DefaultView.Sort = "kodeku asc";
                GRData2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRData2.DataBind();

                GRData2.UseAccessibleHeader = true;

                GRData2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (rbJnsKaryawan.SelectedValue == "Sales")
            {
                ge.ListDataset.Tables[0].DefaultView.Sort = "kodesales asc";
                GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                GRData.DataBind();

                GRData.UseAccessibleHeader = true;

                GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        catch (Exception X)
        {
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void Group1_CheckedChanged(Object sender, EventArgs e)
    {
        try
        {
            if (rbJnsKaryawan.SelectedValue == "Manager")
            {
                pnlsearckaryawan.Visible = true;
                pnlsearcbtn.Visible = true;
                pnldatakaryawan.Visible = true;

                txtheadermanager.Visible = true;
                txtheadersales.Visible = false;
                txtheaderku.Visible = false;

                GRData.Visible = false;
                GRData2.Visible = false;
                GRData3.Visible = true;

                txtkaryawan.Text = "";
                HDNBagian.Value = "E";
                btnSimpan.Text = "Save";
                BindGridEntity();
            }
            else if (rbJnsKaryawan.SelectedValue == "KU")
            {
                pnlsearckaryawan.Visible = true;
                pnlsearcbtn.Visible = true;
                pnldatakaryawan.Visible = true;

                txtheadermanager.Visible = false;
                txtheadersales.Visible = false;
                txtheaderku.Visible = true;

                GRData.Visible = false;
                GRData2.Visible = true;
                GRData3.Visible = false;

                txtkaryawan.Text = "";
                HDNBagian.Value = "B";
                btnSimpan.Text = "Save";

                BindGridEntity();
            }
            else if (rbJnsKaryawan.SelectedValue == "Sales")
            {
                pnlsearckaryawan.Visible = true;
                pnlsearcbtn.Visible = true;
                pnldatakaryawan.Visible = true;

                txtheadermanager.Visible = false;
                txtheadersales.Visible = true;
                txtheaderku.Visible = false;

                GRData.Visible = true;
                GRData2.Visible = false;
                GRData3.Visible = false;

                txtkaryawan.Text = "";
                HDNBagian.Value = "A";
                btnSimpan.Text = "Save";

                BindGridEntity();
            }
            else
            {
                pnlsearckaryawan.Visible = false;
                pnlsearcbtn.Visible = false;
                pnldatakaryawan.Visible = false;
                txtheadermanager.Visible = false;
                txtheadersales.Visible = false;
                txtheaderku.Visible = false;

                GRData.Visible = false;
                GRData2.Visible = false;
                GRData3.Visible = false;

                txtkaryawan.Text = "";
                HDNBagian.Value = "";

            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReportDocument rprt = new ReportDocument();
        switch (e.CommandName)
        {
            case "SET":
                try
                {
                    if (rbJnsKaryawan.SelectedValue == "Manager")
                    {
                        txtkodenik.Text = ((Label)GRData3.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesales")).Text;
                        txtnamasales.Text = ((Label)GRData3.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasales")).Text;
                        txtemail.Text = ((Label)GRData3.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblemail")).Text;
                    }
                    else if (rbJnsKaryawan.SelectedValue == "KU")
                    {
                        txtkodenik.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesales")).Text;
                        txtnamasales.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasales")).Text;
                        txtemail.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblemail")).Text;
                    }
                    else if (rbJnsKaryawan.SelectedValue == "Sales")
                    {
                        txtkodenik.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesales")).Text;
                        txtnamasales.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamasales")).Text;
                        txtemail.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblemail")).Text;
                    }
                    btnSimpan.Visible = true;
                    btnSimpan.Text = "Update";
                    pnlFilter.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                }
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
        }
    }
    protected void btnSearchx_Click(object sender, EventArgs e)
    {
        try
        {
            string[] splitdata = txtkaryawan.Text.Split('-');
            txtkodenik.Text = splitdata[0].Trim();
            txtnamasales.Text = splitdata[1].Trim();
            pnlFilter.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            btnSimpan.Visible = true;
            btnSimpan.Text = "Save";
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnlFilter.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;

        }
        catch (Exception X)
        {
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("PermohonanPinjamanKreditMultigunaNew.aspx");
        pnladd.Visible = false;
        PnlFront.Visible = true;
        pnlFilter.Visible = true;
        btnSimpan.Visible = false;
        rbJnsKaryawan.ClearSelection();
        Group1_CheckedChanged(sender, e);


        txtkaryawan.Text = "";

        txtkaryawan.Focus();
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtemail.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Masukkan Email Anda..!!');", true);
                txtemail.Focus();
            }
            else
            {
                string jnsState = "", btn = "";
                if(HDNBagian.Value == "A")
                {
                    jnsState = "Sales";
                }
                else if(HDNBagian.Value == "B")
                {
                    jnsState = "KU";
                }
                else if(HDNBagian.Value == "E")
                {
                    jnsState = "Manager";
                }
                if(btnSimpan.Text == "Save")
                {
                    btn = "INSERTBYBAGIAN";
                }
                else if(btnSimpan.Text == "Update")
                {
                    btn = "UPDATEBYBAGIAN";
                }

                Global.SqlHelper[] var = new Global.SqlHelper[12];
                var[0] = gb.CreateSPParameter("@kodeSales", ParameterDirection.Input, SqlDbType.VarChar, 10, txtkodenik.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@nama", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamasales.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@emailsales", ParameterDirection.Input, SqlDbType.VarChar, 50, txtemail.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[4] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKoperasiID, typeof(string));
                var[5] = gb.CreateSPParameter("@Kode_Wilayah", ParameterDirection.Input, SqlDbType.VarChar, 3, HDKodewilayah.Value, typeof(string));
                var[6] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));

                var[7] = gb.CreateSPParameter("@JnsKaryawan", ParameterDirection.Input, SqlDbType.VarChar, 50, jnsState, typeof(string));
                var[8] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[9] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, btn , typeof(string));
                var[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SU_Email", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    rbJnsKaryawan.ClearSelection();
                    Group1_CheckedChanged(sender, EventArgs.Empty);
                    txtemail.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Berhasil disimpan.');", true);
                    pnlFilter.Visible = true;
                    PnlFront.Visible = true;
                    pnladd.Visible = false;

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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmailSales.aspx");
        txtkaryawan.Focus();
    }
    //Cari data karyawan
    [WebMethod]
    public static string GetNoKaryawan(string InputText, string InputText2, string SessionBranchID, string SessionBagian, string SessKoperasiID)
    {
        List<string[]> listResult = new List<string[]>();
        
        //String tes = rbjns

        DefaultClass.Global gb2 = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge2 = new EntitiesClass.GlobalEntities();

        Global.SqlHelper[] Params = new Global.SqlHelper[7];
        Params[0] = gb2.CreateSPParameter("@KodeKoperasi", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKoperasiID, typeof(String));
        Params[1] = gb2.CreateSPParameter("@kodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessionBranchID, typeof(String));
        Params[2] = gb2.CreateSPParameter("@kodeSales", ParameterDirection.Input, SqlDbType.VarChar, 50, InputText, typeof(String));
        Params[3] = gb2.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 1, SessionBagian, typeof(String));
        Params[4] = gb2.CreateSPParameter("@JnsKaryawan", ParameterDirection.Input, SqlDbType.VarChar, 15, InputText2, typeof(String));
        Params[5] = gb2.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CARIKARYAWAN", typeof(string));
        Params[6] = gb2.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge2.ListDataset = gb2.GetDataJustOutPut("dbo.SU_Email", Params, "constrmf");

        listResult = gb2.ConvertTable(ge2.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;
    }
}