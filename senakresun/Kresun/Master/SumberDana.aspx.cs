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

public partial class Kresun_Master_SumberDana : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    public string State = "", kodeproduk = "", kodesumberdana = "";
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
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("SumberDana.aspx");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("SumberDana.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        HDState.Value = "INSERT";
    }
    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.AbsoluteUri);

    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        kodeproduk = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeproduk")).Text;
        kodesumberdana = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodesumberdana")).Text;
        switch (e.CommandName)
        {

            case "SET":

                try
                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;



                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[1] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, kodeproduk, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumberdana, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_Sumberdana", var, "constrmf");
                     

                    txtkdproduk.Text     = ge.ListDataset.Tables[0].Rows[0]["kodeproduk"].ToString();
                    txtkdsumberdana.Text = ge.ListDataset.Tables[0].Rows[0]["kodesumberdana"].ToString();
                    txtnmsumberdana.Text = ge.ListDataset.Tables[0].Rows[0]["namasumberdana"].ToString(); 
                    txtminplafondpinjaman.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["minplafondpinjaman"].ToString()).Replace("Rp","");
                    txtmaxplafondpinjaman.Text = gb.ToRupiah(ge.ListDataset.Tables[0].Rows[0]["maxplafondpinjaman"].ToString()).Replace("Rp", "");
                    txtminusia.Text            = ge.ListDataset.Tables[0].Rows[0]["usiaminimalpinjaman"].ToString();
                    txtalamatsumberdana.Text   = ge.ListDataset.Tables[0].Rows[0]["alamatsumberdana"].ToString();
                    txtmaksusia.Text = ge.ListDataset.Tables[0].Rows[0]["usiamaxpinjaman"].ToString();
                    txtnosk.Text     = ge.ListDataset.Tables[0].Rows[0]["nosksumberdana"].ToString();
                    txttglsk.Text    = ge.ListDataset.Tables[0].Rows[0]["tglsksumberdana"].ToString();
                    txtbtsusiajanda.Text = ge.ListDataset.Tables[0].Rows[0]["batasusiajanda"].ToString();
                    txtbtsusiaduda.Text  = ge.ListDataset.Tables[0].Rows[0]["batasusiaduda"].ToString();
                    txtsukubunga.Text    = ge.ListDataset.Tables[0].Rows[0]["sukubunga"].ToString();


                    HDState.Value = "UPDATE";
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
                    

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                    var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var[1] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, kodeproduk, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 50, kodesumberdana, typeof(string));
                    var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("KP_Sumberdana", var, "constrmf"); 

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
    protected void btnSimpan_Click(object sender, EventArgs e)
    { 
        try
        {
            

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[17];
            var[0] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));
            var[1] = gb.CreateSPParameter("@kodeproduk", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkdproduk.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@kodesumberdana", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkdsumberdana.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var[4] = gb.CreateSPParameter("@namasumberdana", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnmsumberdana.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@minplafondpinjaman", ParameterDirection.Input, SqlDbType.Money, 50, txtminplafondpinjaman.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@maxplafondpinjaman", ParameterDirection.Input, SqlDbType.Money, 20, txtmaxplafondpinjaman.Text, typeof(string));
            var[7] = gb.CreateSPParameter("@usiaminimalpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, txtminusia.Text, typeof(string));
            var[8] = gb.CreateSPParameter("@alamatsumberdana", ParameterDirection.Input, SqlDbType.VarChar, 20, txtalamatsumberdana.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@usiamaxpinjaman", ParameterDirection.Input, SqlDbType.VarChar, 50, txtmaksusia.Text, typeof(string));
            var[10] = gb.CreateSPParameter("@nosksumberdana", ParameterDirection.Input, SqlDbType.VarChar, 20, txtnosk.Text, typeof(string));
            var[11] = gb.CreateSPParameter("@tglsksumberdana", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txttglsk.Text, typeof(string));
            var[12] = gb.CreateSPParameter("@batasusiajanda", ParameterDirection.Input, SqlDbType.VarChar, 20, txtbtsusiajanda.Text, typeof(string));
            var[13] = gb.CreateSPParameter("@batasusiaduda", ParameterDirection.Input, SqlDbType.VarChar, 50, txtbtsusiaduda.Text, typeof(string));
            var[14] = gb.CreateSPParameter("@sukubunga", ParameterDirection.Input, SqlDbType.Decimal, 50, txtsukubunga.Text, typeof(string));
            var[15] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, HDState.Value, typeof(string));
            var[16] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("KP_Sumberdana", var, "constrmf");

            btnSimpan.Enabled = false;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BindGridEntity();


            }
            GRData.UseAccessibleHeader = true;

            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

            CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
            CultureName = "id-ID";



        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }


    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[2];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("KP_Sumberdana", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "kodecabang asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }
    
}