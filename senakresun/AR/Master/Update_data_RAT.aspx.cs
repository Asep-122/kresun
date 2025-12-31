using DefaultClass;
using DocumentFormat.OpenXml.Bibliography;
using EntitiesClass;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AR_Master_Update_data_RAT : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();
    DataTable dtRP = new DataTable("Transaction");
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
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessNamaProfil
    {
        get { return Session["SessNamaProfil"].ToString(); }
        set { ViewState["SessNamaProfil"] = value; }
    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 2, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));
        var[2] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlbulan.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, ddltahun.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlmarketing.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlstatus.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "DEFAULT", typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_List_Anggota_RAT", var, "constrmf");


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    public void BindGridEntityx()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];
        var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 2, "01", typeof(string));
        var[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));
        var[2] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.VarChar, 2, ddlbulan.SelectedValue, typeof(string));
        var[3] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.VarChar, 4, ddltahun.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kodeao", ParameterDirection.Input, SqlDbType.VarChar, 30,ddlmarketing.SelectedValue, typeof(string));
        var[5] = gb.CreateSPParameter("@status", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlstatus.SelectedValue, typeof(string));
        var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 20, "FILTER", typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("AR_List_Anggota_RAT", var, "constrmf");

        // ge.ListDataset.Tables[0].DefaultView.Sort = "nama_nasabah desc";

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!Page.IsPostBack)
            {
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
                Params[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 2, "01", typeof(String));
                Params[1] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 10, SessLoginID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GG_GetCabang", Params, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";

                ddlkoperasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlkoperasi.DataTextField = "text";
                ddlkoperasi.DataValueField = "value";
                ddlkoperasi.DataBind();

                ddlkoperasi.SelectedValue = SessBranchID;
             
                // hdncustomer.Value = "ALL";

                // COMBO MASTER MO
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                var1[0] = gb.CreateSPParameter("KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
                var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_REPORT_KODEMARKETING", var1, "constrmf");

                ddlmarketing.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlmarketing.DataTextField = "namaAO";
                ddlmarketing.DataValueField = "NIK";
                ddlmarketing.DataBind();
                //ddlmarketing.Items.Insert(0, new ListItem("ALL AO", "ALL"));

                //BindGridEntity();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }}



    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SIMPAN":

                string no_nasabah = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoanggota")).Text;
                string nope = ((TextBox)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtnope")).Text;
                string talhir = ((TextBox)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txttalhir")).Text;

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 20, no_nasabah, typeof(string));
                var[1] = gb.CreateSPParameter("@no_hp", ParameterDirection.Input, SqlDbType.VarChar, 16, nope, typeof(string));
                var[2] = gb.CreateSPParameter("@tanggal_lahir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, talhir, typeof(DateTime));
                var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
             
                ge.ListDataset = gb.GetDataJustOutPut("AR_UPDATE_ANGGOTA_RAT", var, "constrmf");


                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sukses menyimpan perubahan');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                }

                //BindGridEntity();
                break;

        }
    }

 
    protected void lnkCari_Click(object sender, EventArgs e)
    {
        panelPosting.Visible = true;
        BindGridEntity();
    }


    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {


           



                    dtRP.Columns.Clear();
                    dtRP.Columns.Add(new DataColumn("No", typeof(int)));
                    dtRP.Columns.Add(new DataColumn("NoAnggota", typeof(string)));
                    dtRP.Columns.Add(new DataColumn("TglLahir", typeof(DateTime)));
                    dtRP.Columns.Add(new DataColumn("NohP", typeof(string)));


                    dtRP.Rows.Clear();
                    int xi = 0;




                    foreach (GridViewRow row in GRData.Rows)
                    {


                        //DateTime TglLahir = DateTime.Parse(((TextBox)row.FindControl("txttalhir")).Text);

                            DataRow dr1 = dtRP.NewRow();
                            xi = xi + 1;

                            dr1["No"] = xi;
                            dr1["NoAnggota"] = ((Label)row.FindControl("lblnoanggota")).Text;
                            dr1["TglLahir"] ="2024-01-01";
                            dr1["NohP"] = ((TextBox)row.FindControl("txtnope")).Text;
                            dtRP.Rows.Add(dr1);




                       
                    }




                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[3];
                    //Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                    Paramsc[0] = gb.CreateSPParameter("@AR_Update_Anggota", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                    Paramsc[1] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    Paramsc[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_UPDATE_ANGGOTA_RAT", Paramsc, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {


                            BindGridEntity();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Update Data Sukses');", true);
                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                      

                    }



        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }

}