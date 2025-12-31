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
public partial class mikro_petugasbulanan : System.Web.UI.Page
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
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    string bulan;
    string tahun;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                //if (SessLevelUser == "X02" || SessLevelUser == "X04" || SessLevelUser == "X07" || SessLevelUser == "X08" || SessLevelUser == "X09" || SessLevelUser == "X10")
                //{

 

                    CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                    CultureName = "id-ID";
                    //Label2.Text = CultureName;
                    Label2.Text = "";
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[3];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CABANG", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var3[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";
                ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabang.DataTextField = "nama_cabang";
                ddlcabang.DataValueField = "kode_cabang";
                ddlcabang.DataBind();

                ddlcabanglist.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabanglist.DataTextField = "nama_cabang";
                ddlcabanglist.DataValueField = "kode_cabang";
                ddlcabanglist.DataBind();



                BindGridEntity();
                    ambilTahun();
                    ambilBulan();
                //}
              
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

          //  gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
  
    public void BindGridEntity()
    {




        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, '-', typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabanglist.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_cabang asc";


        GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRUser.DataBind();




      


        GRUser.UseAccessibleHeader = true;
        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;




    }

    //private static DataTable GetDataKresun(string query)
    //{

    //    DataTable dt = new DataTable();
    //    String constr = ConfigurationManager.ConnectionStrings["postgreskresun"].ConnectionString;
    //    NpgsqlConnection con = new NpgsqlConnection(constr);
    //    con.Open();
    //    NpgsqlCommand command = new NpgsqlCommand(query, con);
    //    NpgsqlDataReader dr = command.ExecuteReader();
    //    dt.Load(dr);
    //    con.Close();
    //    return dt;


    //}

    //private static DataTable GetData(string query)
    //{

    //    DataTable dt = new DataTable();
    //    String constr = ConfigurationManager.ConnectionStrings["postgresmikro"].ConnectionString;
    //    NpgsqlConnection con = new NpgsqlConnection(constr);
    //    con.Open();
    //    NpgsqlCommand command = new NpgsqlCommand(query, con);
    //    NpgsqlDataReader dr = command.ExecuteReader();
    //    dt.Load(dr);
    //    con.Close();
    //    return dt;


    //}
    //private static DataTable GetDataGadai(string query)
    //{

    //    DataTable dt = new DataTable();
    //    String constr = ConfigurationManager.ConnectionStrings["postgresgadai"].ConnectionString;
    //    NpgsqlConnection con = new NpgsqlConnection(constr);
    //    con.Open();
    //    NpgsqlCommand command = new NpgsqlCommand(query, con);
    //    NpgsqlDataReader dr = command.ExecuteReader();
    //    dt.Load(dr);
    //    con.Close();
    //    return dt;


    //}
    //private static DataTable GetDataMikro(string query)
    //{

    //    DataTable dt = new DataTable();
    //    String constr = ConfigurationManager.ConnectionStrings["postgresmikro"].ConnectionString;
    //    NpgsqlConnection con = new NpgsqlConnection(constr);
    //    con.Open();
    //    NpgsqlCommand command = new NpgsqlCommand(query, con);
    //    NpgsqlDataReader dr = command.ExecuteReader();
    //    dt.Load(dr);
    //    con.Close();
    //    return dt;


    //}
    public void ambilTahun()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 3; i >= 0; i--)
        {
            // Now just add an entry that's the current year minus the counter
            ddlTahun.Items.Add((currentYear - i).ToString());
        }
        ddlTahun.SelectedValue = currentYear.ToString();
        if (SessBranchID != "000")
        {
            ddlTahun.Enabled = false;
        }
        else
        {
            ddlTahun.Enabled = true;
        }
    }

    public void ambilBulan()
    {
        var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        for (int i = 0; i < months.Length; i++)
        {
            ddlbulan.Items.Add(new ListItem { Text = months[i].ToString(), Value = (i + 1).ToString() });
        }
        var currentmonth = DateTime.Today.Month - 1;
        ddlbulan.SelectedValue = currentmonth.ToString();
        if (SessBranchID != "000")
        {
            ddlbulan.Enabled = false;
        }
        else
        {
            ddlbulan.Enabled = true;
        }
    }
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;
        pnlUserEdit.Visible = true;
        hdnstatus.Value = "true";
        ddlcabang.Enabled = true;        
        Pnlsearch.Visible = false;
        ddlbulan.Enabled = true;
        ddlTahun.Enabled = true;

    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        DataTable ds = new DataTable();
        ds = null;
        GRUser.DataSource = ds;
        GRUser.DataBind();

        BindGridEntity();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);


    }
    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string xid;



            switch (e.CommandName)
            {


                case "GRUPEDT":



                    ListItem lstItemc = ddlcabang.Items.FindByValue(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtcabangid")).Text);
                    if (lstItemc != null)
                    {
                        ddlcabang.SelectedValue = lstItemc.Value;
                    }




                    txtkumingguan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkumingguan")).Text;
                    txtku_bulanan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblku_bulanan")).Text;
                    txtsp_mingguan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsp_mingguan")).Text;
                    txtsp_bulanan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsp_bulanan")).Text;

                    ddlbulan.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtbulan")).Text;
                    ddlTahun.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtTahun")).Text;
                    bulan = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtbulan")).Text;
                    tahun = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtTahun")).Text;

                    ddlTahun.Enabled = false;
                    ddlbulan.Enabled = false;
                    pnlFront.Visible = false;
                    pnlUserEdit.Visible = true;
                    ddlcabang.Enabled = false;

                    Pnlsearch.Visible = false;
                    hdnstatus.Value = "false";
                    break;

                case "GRUPSET":

                    //String constr = ConfigurationManager.ConnectionStrings["postgresmikro"].ConnectionString;
                    //NpgsqlConnection con = new NpgsqlConnection(constr);

                    bulan = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtbulan")).Text;
                    tahun = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtTahun")).Text;
                    ListItem lstItemr = ddlcabang.Items.FindByValue(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtcabangid")).Text);
                    if (lstItemr != null)
                    {
                        ddlcabang.SelectedValue = lstItemr.Value;
                    }


                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                    var1[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string)); ;
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPETUGAS", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 0, bulan, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                    var1[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 0, tahun, typeof(string));



                    ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var1, "constrmf");
                    if (decimal.Parse(ge.ListDataset.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data tidak dapat di Delete sudah ada transaksi!');", true);

                    }
                    else

                    {

                        //query = "  delete FROM tbl_master_perkiraan_petugas where  kode_cabang='" + ddlcabang.SelectedValue + "' and bulan='" + bulan + "' and  tahun ='" + tahun + "'";


                        //NpgsqlCommand command = new NpgsqlCommand(query, con);
                        //command.ExecuteReader();
                        //con.Close();
                        //BindGridEntity();


                        DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[6];
                        var2[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string)); ;
                        var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETEPETUGAS", typeof(string));
                        var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var2[3] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, bulan, typeof(string));
                        var2[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                        var2[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, tahun, typeof(string));
                        ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var2, "constrmf");

                        if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','1');", true);

                        }

                    }








                    break;
            }
        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

  


    

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {




            if (hdnstatus.Value == "true")
            {


                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                var1[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string)); ;
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPETUGASSAVE", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[3] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
                var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));



                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var1, "constrmf");

                if ( decimal.Parse(ge.ListDataset.Tables[0].Rows[0][0].ToString())>0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data sudah ada, anda tidak dapat membuat data dengan Bulan dan tahun pada cabang tersebut');", true);
                }

                else
                {

    

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
                    var[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                    var[3] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[4] = gb.CreateSPParameter("@ku_mingguan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtkumingguan.Text), typeof(float));
                    var[5] = gb.CreateSPParameter("@ku_bulanan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtku_bulanan.Text), typeof(float));
                    var[6] = gb.CreateSPParameter("@sp_mingguan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtsp_mingguan.Text), typeof(float));
                    var[7] = gb.CreateSPParameter("@sp_bulanan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtsp_bulanan.Text), typeof(float));
                    var[8] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTPETUGAS", typeof(string));
                    var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','1');", true);


                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }

                    
                }
            }

            else
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];
                var[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlcabang.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbulan.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlTahun.SelectedValue, typeof(string));
                var[3] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[4] = gb.CreateSPParameter("@ku_mingguan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtkumingguan.Text), typeof(float));
                var[5] = gb.CreateSPParameter("@ku_bulanan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtku_bulanan.Text), typeof(float));
                var[6] = gb.CreateSPParameter("@sp_mingguan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtsp_mingguan.Text), typeof(float));
                var[7] = gb.CreateSPParameter("@sp_bulanan", ParameterDirection.Input, SqlDbType.Float, 0, float.Parse(txtsp_bulanan.Text), typeof(float));
                var[8] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATEPETUGAS", typeof(string));
                var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");



                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Update Done!','You successfully save this Project','top-right','success','1');", true);


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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
           //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



   
    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {

        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
 
}