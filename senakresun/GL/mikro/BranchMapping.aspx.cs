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

public partial class MasterTemplate_BranchMapping : System.Web.UI.Page
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



    public string area;
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
                ddlSearch.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlSearch.DataTextField = "nama_area";
                ddlSearch.DataValueField = "kode_area";
                ddlSearch.DataBind();
                ddlarea.DataTextField = "nama_area";
                ddlarea.DataValueField = "kode_area";
                ddlarea.DataBind();
                ddlarea.Enabled = false;
                BindGridEntity();

                DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
                varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "AREA", typeof(string));
                varx1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx1[2] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlarea.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", varx1, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "kode_nik asc";
                ddlrmcrm.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlrmcrm.DataTextField = "nama_karyawan";
                ddlrmcrm.DataValueField = "kode_nik";
                ddlrmcrm.DataBind();


             
                
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, '-', typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTRMCRM", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSearch.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_area asc";


        GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRUser.DataBind();







        GRUser.UseAccessibleHeader = true;
        GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;





    }
   
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindGridEntity();
        ddlarea.SelectedValue = ddlSearch.SelectedValue;
        string query;

        DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
        varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRMCRM", typeof(string));
        varx1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varx1[2] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlarea.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", varx1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_nik asc";
        ddlrmcrm.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlrmcrm.DataTextField = "nama_karyawan";
        ddlrmcrm.DataValueField = "kode_nik";
        ddlrmcrm.DataBind();

      
        ddlrmcrm.Enabled = true;

    }

 
    protected void LnkAdd_Click(object sender, EventArgs e)
    {

        ddlarea.SelectedValue = ddlSearch.SelectedValue;
        string query;
        DefaultClass.Global.SqlHelper[] varx1 = new DefaultClass.Global.SqlHelper[3];
        varx1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTRMCRM", typeof(string));
        varx1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        varx1[2] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlarea.SelectedValue, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", varx1, "constrmf");
        ge.ListDataset.Tables[0].DefaultView.Sort = "kode_nik asc";
        ddlrmcrm.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        ddlrmcrm.DataTextField = "nama_karyawan";
        ddlrmcrm.DataValueField = "kode_nik";
        ddlrmcrm.DataBind();
        ddlrmcrm.Enabled = true;
        //ddlrmcrm.Items.Insert(0, new ListItem("Pilih RM/CRM", "ALL"));

        //query = "   SELECT kode_cabang, cabang_nama,0 as checkbranch FROM tbl_master_perkiraan_mapping x ";
        //query = query + " where x.kode_area = '" + ddlSearch.SelectedValue + "' and x.id_mapping_rm = 0 ";
        //query = query + " order by cabang_nama asc";
        //DataTable dt = GetData(query);



        //DGGroup.DataSource = dt;
        //DGGroup.DataBind();



        //DGGroup.UseAccessibleHeader = true;
        //DGGroup.HeaderRow.TableSection = TableRowSection.TableHeader;

        DGGroup.Visible = false;     
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);

    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string query;
        switch (e.CommandName)
        {

            case "GRUPEDT":


                string ambilddlid = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblrmid")).Text;
                string ambilddlid1 = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                hdnmapping.Value = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                ddlnourut.SelectedValue= ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNO")).Text;



                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                var[0] = gb.CreateSPParameter("@id_mapping_rm", ParameterDirection.Input, SqlDbType.VarChar,30, ambilddlid1, typeof(string));
                var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTDATARMCRM", typeof(string));
                var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[3] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSearch.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "cabang_nama asc";


                DGGroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                DGGroup.DataBind();



                DGGroup.UseAccessibleHeader = true;
                DGGroup.HeaderRow.TableSection = TableRowSection.TableHeader;


                

                ddlarea.Enabled = false;


                ddlrmcrm.Enabled = false;
                ddlrmcrm.SelectedValue = ambilddlid;

                DGGroup.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopup();", true);
              break;
            case "GRUPSET":

                hdnmapping.Value = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;

                DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[5];
                varx[0] = gb.CreateSPParameter("@id_mapping_rm", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnmapping.Value, typeof(string));
                varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MAPPINGRMCRM", typeof(string));
                varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                varx[3] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSearch.SelectedValue, typeof(string));
                varx[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", varx, "constrmf");



                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Data sudah Berhasil di hapus!','Data sudah Berhasil di hapus','top-right','success','1');", true);


                }

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }


                break;

        }
    }

 

    public void BindGridEntityGroup()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@id_mapping_rm", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnmapping.Value, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "LISTDATARMCRM", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSearch.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "cabang_nama asc";


        DGGroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        DGGroup.DataBind();



        DGGroup.UseAccessibleHeader = true;
        DGGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void GRUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

     

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        //area = ddlSearch.SelectedValue;
        //Response.Redirect(Request.Url.AbsoluteUri);
        //ddlSearch.SelectedValue = area;
        //pnlFront.Visible = false;
        //lblmsg.Text = "Item successfully added to category dropdown ";
        //lblmsg.CssClass = "alert alert-success pull-right";
        //string strJsSuccess = new StringBuilder
        //("$('#modalGroup').modal('hide');").ToString();
        //ScriptManager.RegisterClientScriptBlock
        //(this, this.GetType(), "Hide", strJsSuccess, true);

        //("$('#modalGroup').modal('hide');").ToString();

    }

    protected string btnreload()
    {
        
        
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
        
    }

    //function simpan
    protected void lnkSaveGR_Click(object sender, EventArgs e)
    {
        try
        {
            //string State = "";
            string query = "";
            if (ddlrmcrm.SelectedValue == "ALL")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anda belum memilih RM/CRM tersebut');", true);
            }
            else
            {

       


            if (ddlrmcrm.Enabled == true) 
            {
                    //State = "INSERT";

                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[5];
                    varx[0] = gb.CreateSPParameter("@kode_nik", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlrmcrm.SelectedValue, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERTRMCRM", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[3] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlarea.SelectedValue, typeof(string));
                    varx[4] = gb.CreateSPParameter("@urut", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnourut.SelectedValue, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", varx, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Sukses!','Data sudah Berhasil di Save','top-right','success','1');", true);


                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }


                     BindGridEntity();
                  
                }
                else
            {



                    DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[5];
                    varx[0] = gb.CreateSPParameter("@id_mapping_rm", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnmapping.Value, typeof(string));
                    varx[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATERMCRM", typeof(string));
                    varx[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    varx[3] = gb.CreateSPParameter("@kodearea", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlarea.SelectedValue, typeof(string));
                    varx[4] = gb.CreateSPParameter("@urut", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlnourut.SelectedValue, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", varx, "constrmf");



                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Update Sukses!','Data sudah Berhasil di Save','top-right','success','1');", true);

                        for (int i = 0; i < DGGroup.Rows.Count; i++)
                        {
                            //DataRow dr = dtRP.NewRow();
                            if (((CheckBox)DGGroup.Rows[i].FindControl("chklist")).Checked == true)
                            {



                                DefaultClass.Global.SqlHelper[] vars = new DefaultClass.Global.SqlHelper[4];
                                vars[0] = gb.CreateSPParameter("@id_mapping_rm", ParameterDirection.Input, SqlDbType.VarChar, 30, hdnmapping.Value, typeof(string));
                                vars[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "UPDATECABANGRMCRM", typeof(string));
                                vars[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                                vars[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ((Label)DGGroup.Rows[i].FindControl("lblkodecabang")).Text, typeof(string));


                                ge.ListDataset = gb.GetDataJustOutPut("GL_BAHAN_CABANG", vars, "constrmf");



                                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Update Sukses!','Data sudah Berhasil di Save','top-right','success','1');", true);


                                }

                                else
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                                }





                            }
                        }
                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                    }







            

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Berhasil Di Mapping');", true);
                    //BindGridEntity();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save Mapping Cabang','top-right','success','1');", true);
                }

            }


        }

        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            //gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    protected void lnkRefreshModal_Click(object sender, EventArgs e)
    {

        BindGridEntity();
        BindGridEntityGroup();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);

    }



    protected string btnremodal()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
   
}