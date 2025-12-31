using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class AE_Master_AefeeNonManagement : System.Web.UI.Page
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
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                DefaultClass.Global.SqlHelper[] Paramsx = new DefaultClass.Global.SqlHelper[1];
                Paramsx[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));


                ge.ListDataset = gb.GetDataJustOutPut("dbo.AE_GetCabangReport", Paramsx, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlcabangx, "None", "");
                //ddlcabang.Items.Insert(0, new ListItem("-", "-"));

                ddlcabangx.SelectedValue = SessBranchID;

                if (SessBranchID == "000")
                {
                    ddlcabangx.Enabled = true;
                }
                else
                {
                    ddlcabangx.Enabled = false;
                }

                BindGridEntity();
            }
            //GRData.UseAccessibleHeader = true;

            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ambilcabang();
        if (ddlcabang.Items.Count == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Semua Cabang sudah di input FeeBM berdasarkan Jabatan dan Bagian nya masing-masing. Silahkan mengupadate data yang sudah ada di bawah.');", true);
        }
        else
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;
            ddlcabang.Focus();
            ddlbagian.Enabled = false;
            ddljabatan.Enabled = false;
        }
    }
    private void ambilcabang()
    {
        try
        {
            Global.SqlHelper[] var = new Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANGNONMGM", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");
            ge.ListDataset.Tables[1].DefaultView.Sort = "Kodecabang asc";

            ddlcabang.DataSource = ge.ListDataset.Tables[1].DefaultView.ToTable();
            ddlcabang.DataTextField = "kode";
            ddlcabang.DataValueField = "Kodecabang";
            ddlcabang.DataBind();
            ddlcabang.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    private void ambilbagian()
    {
        try
        {
            Global.SqlHelper[] var = new Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBAGIANNONMGM", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");
            ge.ListDataset.Tables[1].DefaultView.Sort = "kodebagian asc";

            ddlbagian.DataSource = ge.ListDataset.Tables[1].DefaultView.ToTable();
            ddlbagian.DataTextField = "kode";
            ddlbagian.DataValueField = "kodebagian";
            ddlbagian.DataBind();
            ddlbagian.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    private void ambiljabatan()
    {
        try
        {
            Global.SqlHelper[] var = new Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAB", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbagian.SelectedValue, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kodejab asc";

            ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljabatan.DataTextField = "kode";
            ddljabatan.DataValueField = "kodejab";
            ddljabatan.DataBind();
            ddljabatan.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    private void allbagiandanjabatan()
    {
        try
        {
            Global.SqlHelper[] var = new Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBAGIAN", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kodebagian asc";

            ddlbagian.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlbagian.DataTextField = "kode";
            ddlbagian.DataValueField = "kodebagian";
            ddlbagian.DataBind();
            ddlbagian.Items.Insert(0, new ListItem("Pilih Data", "-"));


            Global.SqlHelper[] var1 = new Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAB", typeof(string));
            var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[3] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbagian.SelectedValue, typeof(string));

            
            ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var1, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kodejab asc";

            ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddljabatan.DataTextField = "kode";
            ddljabatan.DataValueField = "kodejab";
            ddljabatan.DataBind();
            ddljabatan.Items.Insert(0, new ListItem("Pilih Data", "-"));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void ddlcabangx_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGridEntity();
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), System.IO.Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
        //DateTime now = DateTime.Now;

        //Txtrptperiode.Text = "Periode : "  + date.ToString("dd-MM-yyyy") +  " s/d " + xdate.ToString("dd-MM-yyyy");
    }

    public void BindGridEntity()
    {
        try
        {
            Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTALLNONMGM", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabangx.SelectedValue, typeof(string));


            ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "kodecabang asc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
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


                    if (SessBranchID != "000")
                    {
                    

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;
                    ddlcabang.Enabled = false;
                    ddlbagian.Enabled = false;

                    allbagiandanjabatan();

                    ddlcabang.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    ddlcabang.Items.Insert(0, new ListItem(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text + " - " + 
                        ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text, ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text));

                    ddlbagian.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebag")).Text;
                    Global.SqlHelper[] var1 = new Global.SqlHelper[4];
                    var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTJAB", typeof(string));
                    var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 10, ddlbagian.SelectedValue, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var1, "constrmf");
                    ge.ListDataset.Tables[0].DefaultView.Sort = "kodejab asc";

                    ddljabatan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    ddljabatan.DataTextField = "kode";
                    ddljabatan.DataValueField = "kodejab";
                    ddljabatan.DataBind();
                    ddljabatan.Items.Insert(0, new ListItem("Pilih Data", "-"));


                    ddljabatan.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejab")).Text;
                    txtfeebm.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfeeBM")).Text;
                    txtfeebmbulanan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblfeeBMbulan")).Text;
                    ddljabatan.Enabled = false;

                    if (ddlbagian.SelectedValue == "A")
                    {
                        feebulan.Visible = true;

                    }
                    else
                    {
                        feebulan.Visible = false;
                    }
                    btnSimpan.Text = "Update";

                       
                    }
                    else
                    {
                        //ddlcabangx.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Perubahan Data Hanya dapat dilakukan oleh cabang!!');", true);
                        GRData.UseAccessibleHeader = true;
                        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
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
                    string kodecabang = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecabang")).Text;
                    string bagian = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodebag")).Text;
                    string jabatan = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodejab")).Text;
                    Global.SqlHelper[] var = new Global.SqlHelper[6];
                    var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, kodecabang, typeof(string));
                    var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETENONMGM", typeof(string));
                    var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[4] = gb.CreateSPParameter("@kodejab", ParameterDirection.Input, SqlDbType.VarChar, 3, jabatan, typeof(string));
                    var[5] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 3, bagian, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");
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
                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                break;
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (btnSimpan.Text == "Save")
            {
                STATE = "INSERTNONMGM";
            }
            else
            {
                STATE = "UPDATENONMGM";
            }

            if(ddlcabang.SelectedValue == "-")
            {
                ddlcabang.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih Cabang terlebih dahulu!!');", true);
            }
            else if (ddlbagian.SelectedValue == "-")
            {
                ddlbagian.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih Bagian terlebih dahulu!!');", true);
            }
            else if (ddljabatan.SelectedValue == "-")
            {
                ddljabatan.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih Jabatan terlebih dahulu!!');", true);
            }
            else if (decimal.Parse(txtfeebm.Text) == 0)
            {
                txtfeebm.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('FeeBM tidak boleh kosong, harap isi terlebih dahulu!!');", true);
            }
            else if (ddlbagian.SelectedValue=="A" &&  decimal.Parse(txtfeebmbulanan.Text)== 0)
            {
                txtfeebmbulanan.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('FeeBM Bulanan tidak boleh kosong, harap isi terlebih dahulu!!');", true);
            }
            else
            {
                Global.SqlHelper[] var = new Global.SqlHelper[9];
                var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlcabang.SelectedValue, typeof(string));
                var[1] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                var[2] = gb.CreateSPParameter("@feebm", ParameterDirection.Input, SqlDbType.Money, 0, txtfeebm.Text, typeof(decimal));
                var[3] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[4] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[5] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[6] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbagian.SelectedValue, typeof(string));
                var[7] = gb.CreateSPParameter("@kodejab", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljabatan.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@feeBULANAN", ParameterDirection.Input, SqlDbType.Money, 0, txtfeebmbulanan.Text, typeof(decimal));

                ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnSimpan.Enabled = false;
                    btnSimpan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','1');", true);
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
    protected void Cabang_Onchange(object sender, EventArgs e)
    {
        try
        {
            if(ddlcabang.SelectedValue == "-")
            {
                ddlbagian.SelectedValue = "-";
                ddljabatan.SelectedValue = "-";

                ddlbagian.Enabled = false;
                ddljabatan.Enabled = false;
            }
            else
            {
                ambilbagian();
                ddlbagian.Enabled = true;
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void Bagian_Onchange(object sender, EventArgs e)
    {
        try
        {
            if (ddlbagian.SelectedValue == "-")
            {
                ddljabatan.SelectedValue = "-";

                ddljabatan.Enabled = false;
            }
            else
            {
                ambiljabatan();
                ddljabatan.Enabled = true;
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void Jabatan_Onchange(object sender, EventArgs e)
    {
        try
        {
            if (ddljabatan.SelectedValue != "-")
            {
                Global.SqlHelper[] var = new Global.SqlHelper[6];
                var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTTYPENONMGM", typeof(string));
                var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@kodebagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlbagian.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@kodejab", ParameterDirection.Input, SqlDbType.VarChar, 50, ddljabatan.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("Ae_Management", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Untuk Kode Jabatan " + ddljabatan.SelectedItem + " sudah pernah di input. Jika anda ingin meruabah data tersebut. Silahkan cari di data Table tampilan awal atau pilih data lain jika ingin menginput.');", true);
                    ddljabatan.SelectedValue = "-";
                }
                else
                {
                    txtfeebm.Enabled = true;
                }
            }
            else
            {
                txtfeebm.Enabled = false;
                txtfeebm.Text = "0";
            }
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
}