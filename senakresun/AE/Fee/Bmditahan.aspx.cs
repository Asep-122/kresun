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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;

public partial class mikro_Bmditahan : System.Web.UI.Page
{
    private string SessLoginID
    {
        get { return Session["SessLoginID"].ToString(); }
        set { ViewState["SessLoginID"] = value; }
    }
    public string CultureName
    {
        get { return (string)ViewState["CultureName"]; }
        set { ViewState["CultureName"] = value; }
    }
    private Boolean SessIsSuperAdmin
    {
        get { return Boolean.Parse(Session["SessIsSuperAdmin"].ToString()); }
        set { ViewState["SessIsSuperAdmin"] = value; }
    }
    private string SessBranchID
    {
        get { return Session["SessBranchID"].ToString(); }
        set { ViewState["SessBranchID"] = value; }
    }
    public string SessIsLevel
    {
        get { return Session["SessIsLevel"].ToString(); }
        set { ViewState["SessIsLevel"] = value; }
    }
    public string SessAreaUser
    {
        get { return Session["SessAreaUser"].ToString(); }
        set { ViewState["SessAreaUser"] = value; }
    }
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";

                InsertCombo();
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
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATABYCABANG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@KodeBagian", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlgroup.SelectedValue, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeBmditahan", var, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "Nik asc";

            GRUser.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRUser.DataBind();

            GRUser.UseAccessibleHeader = true;
            GRUser.HeaderRow.TableSection = TableRowSection.TableHeader;
            string jumlah = Convert.ToString(GRUser.Rows.Count);
            if (ddlcabang.SelectedValue != "" && jumlah == "0")
            {
                string tes = ddlcabang.Text.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Pada Cabang " + ddlcabang.SelectedItem + " belum ada.');", true);
                ddlcabang.SelectedValue = "";
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        pnlFront.Visible = false;


        ddlcabang.Enabled = true;


    }

    protected void GRUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {


            case "GRUPEDT":
               
                    txtbmditahanlama.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbmditahan")).Text;
                    txtnik.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnik")).Text;
                    txtbulan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbulan")).Text;
                    txttahun.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltahun")).Text;
                    txtcabang.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkode_cabang")).Text;

                    txtfeebmbulanini.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBM")).Text;
                    //txtfeebmbulanini.Text = "500000";
                    txtbmditahan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbmditahan")).Text;

                    pnlFront.Visible = false;
                    pnlUserEdit.Visible = true;
                    Pnlsearch.Visible = false;

               
                //txtbmditahan.Text = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbmditahan")).Text;






                //string query;
                //query = "";
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = GRUser.Rows[index];
                //String constr = ConfigurationManager.ConnectionStrings["postgresmikro"].ConnectionString;
                //NpgsqlConnection con = new NpgsqlConnection(constr);
                //con.Open();

                //string plafon;
                // string reward;
                //string no_rekening;
                //string no_nasabah;
                //string nama_nasabah;
                //no_rekening = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_rekening")).Text;
                //no_nasabah = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblno_nasabah")).Text;
                //nama_nasabah = ((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama_nasabah")).Text;
                ////tgldrop = (Convert.ToDateTime((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltgl_drop")));
                //plafon = Convert.ToString(Convert.ToDecimal(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblplafon")).Text));
                //reward = Convert.ToString(Convert.ToDecimal(((Label)GRUser.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblreward")).Text));

                // //query = "SELECT * FROM aspnet_insert_gaji_cabang('" + ddlbulan.SelectedValue + "','" + ddltahun.SelectedValue + "','" + ddlcabang.SelectedValue + "'," + gaji + "," + gajisuper + "," + gajicollection + ",)";

                //query = " insert into tbl_reward_sm_bulan_bayar";
                //query = query + "  (norekening, kode_cabang, nik, plafon, reward, user_insert,tgl_insert) values ";
                //query = query + "(";
                //query = query + "'" + no_rekening + "',";
                //query = query + "'" + ddlcabang.SelectedValue + "',";
                //query = query + "'" + ddlmarketing.SelectedValue + "',";
                //query = query + "'" + plafon.Replace(",", ".") + "',";
                //query = query + "'" + reward.Replace(",", ".") + "',";
                //query = query + "'" + SessLoginID + "',";
                //query = query + "now())";
                //NpgsqlCommand command = new NpgsqlCommand(query, con);
                //command.ExecuteReader();

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Bayar Success!','You successfully save this Project','top-right','success','1');", true);
                //BindGridEntity();
                break;

        }
    }

    protected void lnkCari_Click(object sender, EventArgs e)
    {

        BindGridEntity();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
    }

    protected void btnreload(object sender, EventArgs e)
    {

            }

    protected void btnafterDelete(object sender, EventArgs e)
    {
    }

    protected void GRUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToDecimal(txtbmditahan.Text) > Convert.ToDecimal(txtfeebmbulanini.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('jumlah Bm ditahan tidak boleh lebih dari fee BM','top-right','success','1');", true);
            }
            else
            {
                string gaji;
                string gajisuper;
                string gajicollection;
                gaji = Convert.ToString(Convert.ToDecimal(txtbmditahan.Text));
                gajisuper = Convert.ToString(Convert.ToDecimal(txtfeebmbulanini.Text));
                gajicollection = Convert.ToString(Convert.ToDecimal(txtbmditahanlama.Text));

                //query = " insert into tbl_fee_bm_ditahan";
                //query = query + "  (kode_cabang, bulan, tahun,nik, feebm, bmditahan,user_insert,tgl_insert) values ";
                //query = query + "(";
                //query = query + "'" + txtcabang.Text + "',";
                //query = query + "'" + txtbulan.Text + "',";
                //query = query + "'" + txttahun.Text + "',";
                //query = query + "'" + txtnik.Text + "',";
                //query = query + "'" + gajisuper.Replace(",", ".") + "',";
                //query = query + "'" + gaji.Replace(",", ".") + "',";
                //query = query + "'" + SessLoginID + "',";
                //query = query + "now())";

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[12];
                var[0] = gb.CreateSPParameter("@KodeNik", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnik.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, txtcabang.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 40, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 40, txtbulan.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@bayarbm", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(gajisuper), typeof(decimal));
                var[5] = gb.CreateSPParameter("@bmditahan", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(gaji), typeof(decimal));
                var[6] = gb.CreateSPParameter("@bmditahanlama", ParameterDirection.Input, SqlDbType.Money, 0, decimal.Parse(gajicollection), typeof(decimal));
                var[7] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[8] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[9] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "INSERT", typeof(string));
                var[10] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[11] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 40, txttahun.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AE_FeeBmditahan", var, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Save Done!','You successfully save this Project','top-right','success','1');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                }
                pnlFront.Visible = true;
                pnlUserEdit.Visible = false;
                Pnlsearch.Visible = true;
                BindGridEntity();
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
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(lnkRefreshModal, string.Empty, true);
    }
    #region "Insertdata"
    public void InsertCombo()
    {
        try
        {
            // --- Combo Kode Cabang
            DefaultClass.Global.SqlHelper[] varB = new DefaultClass.Global.SqlHelper[3];
            varB[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varB[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
            varB[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //varB[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeBmditahan", varB, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

            ddlcabang.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlcabang.DataTextField = "nama";
            ddlcabang.DataValueField = "KodeCabang";
            ddlcabang.DataBind();
            ddlcabang.Items.Insert(0, new ListItem("Pilih Cabang", ""));

            DefaultClass.Global.SqlHelper[] varG = new DefaultClass.Global.SqlHelper[3];
            varG[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varG[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGROUP", typeof(string));
            varG[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            //varB[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("AE_FeeBmditahan", varG, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeBagian asc";

            ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgroup.DataTextField = "KetBagian";
            ddlgroup.DataValueField = "KodeBagian";
            ddlgroup.DataBind();
            ddlgroup.Items.Insert(0, new ListItem("Pilih Group", ""));
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    #endregion "Insertdata"

}