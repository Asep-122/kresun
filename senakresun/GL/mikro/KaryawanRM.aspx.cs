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
public partial class mikro_KaryawanRM : System.Web.UI.Page
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



                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AREA", typeof(string));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var1, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";
                ddlarea.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlarea.DataTextField = "nama_area";
                ddlarea.DataValueField = "kode_area";
                ddlarea.DataBind();
                ddlcabanglist.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlcabanglist.DataTextField = "nama_area";
                ddlcabanglist.DataValueField = "kode_area";
                ddlcabanglist.DataBind();
                BindGridEntity();

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
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "KARYAWANRM", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabanglist.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";


        GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRUser.DataBind();







        GRUser.UseAccessibleHeader = true;
        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;










    }


    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;
        pnlUserEdit.Visible = true;
        hdnstatus.Value = "true";
        ddlarea.Enabled = true;        
        Pnlsearch.Visible = false;
        

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
        string xid;



        switch (e.CommandName)
        {
              
         
            case "GRUPEDT":
                string query;
                query = "";
         
           
                ListItem lstItemc = ddlarea.Items.FindByValue(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtcabangid")).Text);
                if (lstItemc != null)
                {
                    ddlarea.SelectedValue = lstItemc.Value;
                }




                hdnnik.Value = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtnik")).Text;

                ddljabatan.SelectedValue = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtjabatan")).Text;

                txtkaryawan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtnama")).Text;


                pnlFront.Visible = false;
                pnlUserEdit.Visible = true;
                Pnlsearch.Visible = false;
                hdnstatus.Value = "false";
                break;

            case "GRUPSET":
                //String constr = ConfigurationManager.ConnectionStrings["postgresmikro"].ConnectionString;
                //NpgsqlConnection con = new NpgsqlConnection(constr);
                hdnnik.Value = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtnik")).Text;
                //bulan = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtbulan")).Text;
                //tahun = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtTahun")).Text;
                ListItem lstItemr = ddlarea.Items.FindByValue(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtcabangid")).Text);
                if (lstItemr != null)
                {
                    ddlarea.SelectedValue = lstItemr.Value;
                }


                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[6];
                var1[0] = gb.CreateSPParameter("@kode_nik", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnnik.Value, typeof(string)); ;
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRM", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[3] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 0, bulan, typeof(string));
                var1[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));
                var1[5] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 0, tahun, typeof(string));

          
                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var1, "constrmf");



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
                    var2[0] = gb.CreateSPParameter("@kode_nik", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnnik.Value, typeof(string)); ;
                    var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETERM", typeof(string));
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


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {




            if (hdnstatus.Value == "true")
            {


               



                   
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlarea.SelectedValue, typeof(string));
                    var[1] = gb.CreateSPParameter("@kode_jabatan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddljabatan.SelectedValue, typeof(string));
                    var[2] = gb.CreateSPParameter("@nama_karyawan", ParameterDirection.Input, SqlDbType.VarChar, 60, txtkaryawan.Text, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTRM", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


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

            else
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[6];
                var[0] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlarea.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@kode_jabatan", ParameterDirection.Input, SqlDbType.VarChar, 30, ddljabatan.SelectedValue, typeof(string));
                var[2] = gb.CreateSPParameter("@nama_karyawan", ParameterDirection.Input, SqlDbType.VarChar, 60, txtkaryawan.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATERM", typeof(string));
                var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[5] = gb.CreateSPParameter("@kode_nik", ParameterDirection.Input, SqlDbType.VarChar, 60, hdnnik.Value, typeof(string));

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