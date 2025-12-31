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

public partial class CF_GenerateNeraca : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    Global gb1 = new DefaultClass.Global();
    GlobalEntities ge1 = new GlobalEntities();
    Global gb2 = new DefaultClass.Global();
    GlobalEntities ge2 = new GlobalEntities();
    Global gb3 = new DefaultClass.Global();
    GlobalEntities ge3 = new GlobalEntities();
    Global gb4 = new DefaultClass.Global();
    GlobalEntities ge4 = new GlobalEntities();
    Global gb5 = new DefaultClass.Global();
    GlobalEntities ge5 = new GlobalEntities();
    Global gb6 = new DefaultClass.Global();
    GlobalEntities ge6 = new GlobalEntities();

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
                DateTime now = DateTime.Now;
                //txttglAkhirini.Text = now.ToString("yyyy-MM-dd");
                string ATahun = now.ToString("yyyy");
                string ABulan = now.ToString("MM");
                int rubahtahun = Int32.Parse(ATahun);
                int rubahbulan = Int32.Parse(ABulan);
                DateTime.DaysInMonth(rubahtahun, rubahbulan);
                String rubahhari = DateTime.DaysInMonth(rubahtahun, rubahbulan).ToString();
                string rubahtanggal = rubahhari + "/" + ABulan + "/" + ATahun;
                //txttglAkhirini.Text = rubahtanggal;
                int year = DateTime.Now.Year;
                for (int i = year - 1; i <= year + 5; i++)
                {
                    ListItem li = new ListItem(i.ToString());
                    ddltahun.Items.Add(li);
                }
                ddltahun.Items.FindByText(year.ToString()).Selected = true;
         

                //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                //var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 15, SessKdpersh, typeof(string));
                //var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
                //var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("CF_Closing", var, "constrmf");
                //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";
                //ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlcabang.DataTextField = "Nama";
                //ddlcabang.DataValueField = "KodeCabang";
                //ddlcabang.DataBind();
                //ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
                var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(string));
                var[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                //var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang", var, "constrmf");
                //ge.ListDataTable = gb.GetDataJustOutPut("GG_GetCabang", var, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "nama asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "Text";
                ddlcabang.DataValueField = "value";
                ddlcabang.DataBind();
                ddlcabang.SelectedValue = SessBranchID;
                if (SessBranchID == "000")
                {
                    ddlcabang.Enabled = true;
                }
                else
                {
                    ddlcabang.Enabled = false;
                }
                //else if (ge.ListDataset.Tables[0].Rows.Count > 1)
                //{
                //    ddlbulan_SelectedChange(ddlcabang.SelectedValue, e);
                //}
            }

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    

    protected void btntambah_click(object sender, EventArgs e)
    {
        
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcabang.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Pilih Cabang Terlebih Dahulu');", true);
                ddlcabang.Focus();
                ddlbulan.SelectedValue = "1";
                ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
             
            }
            else
            {

                DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                var2[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlbulan.SelectedValue, typeof(string));
                var2[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 15, ddltahun.SelectedValue, typeof(string));
                var2[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlcabang.SelectedValue, typeof(string));
                var2[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KALENDER", typeof(string));
                var2[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("MT_Generate", var2, "constrmf");

                if (ge.ListDataset.Tables[0].DefaultView.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert(' Cabang Belum Melakukan Closing di bulan tersebut');", true);
                }
                else
                {
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[7];

                    var1[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.Int, 0, ddlbulan.SelectedValue, typeof(string));
                    var1[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.Int, 0, ddltahun.SelectedValue, typeof(string));
                    var1[2] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                    var1[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GENERATE", typeof(string));
                    var1[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("MT_Generate", var1, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Closing Done!','You successfully Delete this Project','top-right','success','2');", true);
                        //ddlbulan.SelectedValue = "1";
                        //ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                        //txttglAwalini.Text = "";
                        //txttglAkhirini.Text = "";
                        //txttglAwaldepan.Text = "";
                        //txttglAkhirdepan.Text = "";
                        //txttglAwallalu.Text = "";
                        //txttglAkhirlalu.Text = "";
                        DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[5];
                        var5[0] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 15, ddlbulan.SelectedValue, typeof(string));
                        var5[1] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 15, ddltahun.SelectedValue, typeof(string));
                        var5[2] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlcabang.SelectedValue, typeof(string));
                        var5[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "HASILCLOSING", typeof(string));
                        var5[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                        ge.ListDataset = gb.GetDataJustOutPut("MT_Generate", var5, "constrmf");


                        if (ge.ListDataset.Tables[0].Rows.Count > 0)
                        {

                            var data1 = ge.ListDataset.Tables[0].Rows[0]["msgdata"].ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + data1.ToString() + "');", true);
                            ddlbulan.SelectedValue = "1";
                            ddltahun.SelectedValue = DateTime.Now.ToString("yyyy");
                           



                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }


                }


                 
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

}