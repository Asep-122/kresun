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

public partial class AR_Master_Company : System.Web.UI.Page
{
    String ID = "";
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    //public string fotopath = "";
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
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Company.aspx");
    //}


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                aktifadd();
            }
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }

    public void aktifadd()
    {
        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@Id", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSESSIONAKTIF", typeof(string));
        Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("GS_COMPANY", Params, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "IsSuperAdmin desc";

        DataTable dt = new DataTable();

        dt = ge.ListDataset.Tables[0].DefaultView.ToTable();
        string a = dt.Rows[0]["IsSuperAdmin"].ToString();
        if (a == "True")
        {
            string b = null;
            pnlHeader.Visible = true;
            BindGridEntity(b);
        }
        else if((a != "True"))
        {
            BindGridEntity(SessKdpersh);
            pnlHeader.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNotification('bg-red', 'Invalid Password', 'top', 'right', 'animated fadeInRight', 'animated fadeOutRight')", true);
        }
    }
    public void BindGridEntity(string kdpers)
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kd_persh", ParameterDirection.Input, SqlDbType.VarChar, 30, kdpers, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCOMPANY", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("GS_COMPANY", var, "constrmf");

        //ge.ListDataset.Tables[0].DefaultView.Sort = "ID asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (HFID.Value == "")
            {
                STATE = "INSERT"; 
            }
            else
            {
                STATE = "UPDATE";
            } 

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[15];
            var[0] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 20, HFID.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@NamaCompany", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamacompany.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@AlamatCompany1", ParameterDirection.Input, SqlDbType.VarChar, 250, txtalamat1.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@AlamatCompany2", ParameterDirection.Input, SqlDbType.VarChar, 50, txtalamat2.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@KotaCompany", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkota.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@NegaraCompany", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnegara.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@TeleponCompany", ParameterDirection.Input, SqlDbType.VarChar, 50, txttlp.Text, typeof(string));
            var[7] = gb.CreateSPParameter("@FaxCompany", ParameterDirection.Input, SqlDbType.VarChar, 50, txtfax.Text, typeof(string));
            var[8] = gb.CreateSPParameter("@Email", ParameterDirection.Input, SqlDbType.VarChar, 50, txtemail.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@WebSIte", ParameterDirection.Input, SqlDbType.VarChar, 250, txtweb.Text, typeof(string));
            var[10] = gb.CreateSPParameter("@MottoCompany", ParameterDirection.Input, SqlDbType.VarChar, 50, txtmoto.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@DirLogoGambar", ParameterDirection.Input, SqlDbType.VarChar, 200, txtpath.Text, typeof(string));
            var[12] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[13] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[14] = gb.CreateSPParameter("@NPWP", ParameterDirection.Input, SqlDbType.VarChar, 250, txtnpwp.Text, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("GS_COMPANY", var, "constrmf");
             
            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);


            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }

        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;

                    ID = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;
                    txtnamacompany.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacompany")).Text;
                    txtalamat1.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat1")).Text;
                    txtalamat2.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat2")).Text;
                    txtkota.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkota")).Text;
                    txtnegara.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnegara")).Text;
                    txttlp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltlp")).Text;
                    txtfax.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfax")).Text;
                    txtemail.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblemail")).Text;
                    txtweb.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblweb")).Text;
                    txtmoto.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblmotto")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@kd_persh", ParameterDirection.Input, SqlDbType.VarChar, 30, ID, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTLOGO", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("GS_COMPANY", var, "constrmf");
                    txtnpwp.Text = ge.ListDataset.Tables[0].Rows[0]["NPWP"].ToString();
                    Image1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["DirLogoGambar"].ToString() + "?r=" + DateTime.Now.Ticks.ToString();
                    txtpath.Text = ge.ListDataset.Tables[0].Rows[0]["DirLogoGambar"].ToString();
                    HFID.Value = ID;
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "SETDEL":

                try
                {
                    //txtKodeCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text; 
                    //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[10];
                    //var1[0] = gb.CreateSPParameter("@KodeGolongan", ParameterDirection.Input, SqlDbType.VarChar, 20, "-", typeof(string));
                    //var1[1] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkdKel.Text, typeof(string));
                    //var1[2] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 250, "-", typeof(string));
                    //var1[3] = gb.CreateSPParameter("@MasaSusut", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                    //var1[4] = gb.CreateSPParameter("@SL", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                    //var1[5] = gb.CreateSPParameter("@DB", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));
                    //var1[6] = gb.CreateSPParameter("@User", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    //var1[7] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYIDKELOMPOK", typeof(string));
                    //var1[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    //var1[9] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Int, 10, "0", typeof(string));

                    //ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var1, "constrmf");

                    //GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    //if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kelompok sudah ada pada transaksi tidak dapat dihapus!');", true);

                    //}
                    //else
                    //{

                    ID = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblid")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@ID", ParameterDirection.Input, SqlDbType.VarChar, 20, ID, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("GS_COMPANY", var1, "constrmf");

                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully Delete this Project','top-right','success','2');", true);

                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }
                }
                //}

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

        }
    }

    protected void UploadFile(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("/Files/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

        //Display the Picture in Image control.
        Image1.ImageUrl = "/Files/" + Path.GetFileName(FileUpload1.FileName);

        string fotopath = folderPath + Path.GetFileName(FileUpload1.FileName);
        txtpath.Text = Image1.ImageUrl;
    }
}