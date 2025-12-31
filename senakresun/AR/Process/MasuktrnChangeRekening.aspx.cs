using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using DocumentFormat.OpenXml.Spreadsheet;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
public partial class GL_MasuktrnChangeRekening : System.Web.UI.Page
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

    DataTable dtRP = new DataTable("Transaction");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                //BindGridEntity();
                //DROPDOWNLIST Kode Supplier
                DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "MARKETING", typeof(string));
                var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var1[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("AR_Over_Cup", var1, "constrmf");
                 ge.ListDataset.Tables[0].DefaultView.Sort = "Text asc";

                ddlaolama.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlaolama.DataTextField = "Text";
                ddlaolama.DataValueField = "value";
                ddlaolama.DataBind();
                ddlaolama.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ddlAobaru.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlAobaru.DataTextField = "Text";
                ddlAobaru.DataValueField = "value";
                ddlAobaru.DataBind();
                ddlAobaru.Items.Insert(0, new ListItem("Pilih Data", "-"));


                panelPosting.Visible = true;
                pnlFilter.Visible = true;
                //BindGridEntity();
            }
           
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }
    protected void ddlmodule_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            BindGridEntity();

       
    }


    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] param = new DefaultClass.Global.SqlHelper[5];
        param[0] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlaolama.SelectedValue, typeof(string));
        param[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTSM", typeof(string));
        param[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        param[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        param[4] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("AR_Over_Cup", param, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "tgl_due asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    decimal debet = 0, kredit = 0;
    int datax = 0;
    decimal debetpilihxt = 0, kreditpilihxt = 0, pilihxt = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            debet += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Plafon"));
            kredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));

            //datax = int.Parse(DataBinder.Eval(e.Row.DataItem, "Plafon"));
            //CheckBox ChkBoxRows = (CheckBox)e.Row.FindControl("ckbx");
            //if (ChkBoxRows.Checked == true)
            //{
            //    pilih += 1;
            //    debetpilih += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Plafon"));
            //    kreditpilih += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));

            //}
            if (((CheckBox)e.Row.FindControl("ckbx")).Checked == true)
            {


                pilihxt += 1;
                debetpilihxt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Plafon"));
                kreditpilihxt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "bakidb"));
               

            }
            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotaldebet = (Label)e.Row.FindControl("lbltotalplafon");
            lbltotaldebet.Text = string.Format("{0:N2}", debet);
            Label lbltotalkredit = (Label)e.Row.FindControl("lbltotalsaldoakhir");
            lbltotalkredit.Text = string.Format("{0:N2}", kredit);

            //txtjumlahanggota.Text = string.Format("{0:N0}", pilih);
            //txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilih);
            //txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilih);
            txtjumlahanggota.Text = string.Format("{0:N0}", pilihxt);
            txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilihxt);
            txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilihxt);



        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
    }
    protected void btnbackheader_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = true;
      
        Pnlfooter.Visible = false;
        Response.Redirect("MasuktrnChangeRekening.aspx");

    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {

       
        if (ddlAobaru.SelectedValue == "-")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sales Baru Masih Kosong!');", true);
        }
        else if (ddlaolama.SelectedValue == "-")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Sales Lama Masih Kosong!');", true);
        }
        else
        {


                DefaultClass.Global.SqlHelper[] varz = new DefaultClass.Global.SqlHelper[5];
                varz[0] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlAobaru.SelectedValue, typeof(string));
                varz[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CEKAOWO", typeof(string));
                varz[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
                varz[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
                varz[4] = gb.CreateSPParameter("@MarketingLama", ParameterDirection.Input, SqlDbType.VarChar, 30, ddlaolama.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("AR_Over_Cup", varz, "constrmf");

                string AOBARU = ge.ListDataset.Tables[0].Rows[0]["AOBARU"].ToString();
                string AOLAMA = ge.ListDataset.Tables[0].Rows[0]["AOLAMA"].ToString();
                string XBOLEH = "0";
                if (AOBARU=="0" && AOLAMA == "0")
                {
                    XBOLEH = "1";
                }
                else if (AOBARU == "1" && AOLAMA == "0")
                {
                    XBOLEH = "0";
                }
                else if (AOBARU == "0" && AOLAMA == "1")
                {
                    XBOLEH = "0";
                }
                else if (AOBARU == "1" && AOLAMA == "1")
                {
                    XBOLEH = "0";
                }


                if (XBOLEH == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('AO WO tidak Boleh di over cup!');", true);
                }
                else
                {
                    
               


                dtRP.Columns.Clear();
                dtRP.Columns.Add(new DataColumn("No", typeof(int)));
                dtRP.Columns.Add(new DataColumn("NomorRekening", typeof(string)));
                dtRP.Columns.Add(new DataColumn("nonasabah", typeof(string)));
                dtRP.Columns.Add(new DataColumn("kodeproduk", typeof(string)));
                dtRP.Columns.Add(new DataColumn("catatan", typeof(string)));

                dtRP.Rows.Clear();
                int xi = 0;

           


                foreach (GridViewRow row in GRData.Rows)
                {
                    CheckBox ChkBoxHeader = (CheckBox)row.FindControl("ckbx");

             
                    if (ChkBoxHeader.Checked == true)
                    {

                    


                      DataRow dr1 = dtRP.NewRow();
                        xi = xi + 1;

                        dr1["No"] = xi;
                        dr1["NomorRekening"] = ((Label)row.FindControl("lblnorekening")).Text;
                        dr1["nonasabah"] = ((Label)row.FindControl("lblnonasabah")).Text;
                        dr1["kodeproduk"] = ((Label)row.FindControl("lblProduk")).Text;
                        dr1["catatan"] = ((TextBox)row.FindControl("txtcatatan")).Text;
                       dtRP.Rows.Add(dr1);




                    }
                }
               
                    
                    
                    
                    DefaultClass.Global.SqlHelper[] Paramsc = new DefaultClass.Global.SqlHelper[6];
                //Paramsc[0] = gb.CreateSPParameter("@Nosystem", ParameterDirection.Input, SqlDbType.VarChar, 50, nobukti, typeof(string));
                Paramsc[0] = gb.CreateSPParameter("@AO_OVERCUP_03", ParameterDirection.Input, SqlDbType.Structured, 0, dtRP, typeof(DataTable));
                Paramsc[1] = gb.CreateSPParameter("@Marketing", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlAobaru.SelectedValue, typeof(string));
                Paramsc[2] = gb.CreateSPParameter("@MarketingLama", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlaolama.SelectedValue, typeof(string));
                Paramsc[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Paramsc[4] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Paramsc[5] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_Over_Cup_XYZ", Paramsc, "constrmf");
                    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    {

                        txtjumlahanggota.Text = string.Format("{0:N0}", 0);
                        txtjumlahPlafon.Text = string.Format("{0:N2}", 0);
                        txtjumlahsaldoakhir.Text = string.Format("{0:N2}", 0);
                        BindGridEntity();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Over Cup Done');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);

        }
    }

    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }

  
    protected void GRRP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{


        //    DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //    Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    Params[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTNMGL", typeof(String));
        //    Params[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


        //    ge.ListDataset = gb.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", Params, "constrmf");
        //    ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlnamaGL")), "-", "-");



        //    DefaultClass.Global.SqlHelper[] xParams = new DefaultClass.Global.SqlHelper[3];
        //    xParams[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    xParams[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKDGL", typeof(String));
        //    xParams[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //    gex.ListDataset = gbx.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", xParams, "constrmf");
        //    gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlKodeGL")), "-", "-");


        //    DefaultClass.Global.SqlHelper[] xParamsCC = new DefaultClass.Global.SqlHelper[3];
        //    xParamsCC[0] = gbx.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //    xParamsCC[1] = gbx.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTGLCC", typeof(String));
        //    xParamsCC[2] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        //    gex.ListDataset = gbx.GetDataJustOutPut("dbo.ALL_JURNALPOSTING", xParamsCC, "constrmf");
        //    gex.ListDataset.Tables[0].DefaultView.Sort = "value asc";
        //    gbx.FillDDL(gex.ListDataset.Tables[0].DefaultView.ToTable(), ((DropDownList)e.Row.FindControl("ddlcostcenter")), "-", "-");




        //    ((DropDownList)e.Row.FindControl("ddlKodeGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;
        //    ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlnamaGL")).Value;

        //    ((DropDownList)e.Row.FindControl("ddlcostcenter")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlcostcenter")).Value;



        //    if (((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value != "")
        //    {
        //        ((DropDownList)e.Row.FindControl("ddlnamaGL")).SelectedValue = ((HiddenField)e.Row.FindControl("hdnddlKodeGL")).Value;

        //    }

        //    GRRP.UseAccessibleHeader = true;
        //    GRRP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //}
    }

    decimal debetpilih = 0, kreditpilih = 0, pilih = 0;
    protected void ckbx_checked(object sender, EventArgs e)
    {
        

         
        foreach (GridViewRow row in GRData.Rows)
        {

            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            ChkBoxRows.Checked = true;
            pilih += 1;
            debetpilih += decimal.Parse(((Label)row.FindControl("lblplafon")).Text);
            kreditpilih += decimal.Parse(((Label)row.FindControl("lblsaldoakhir")).Text);

        }
        txtjumlahanggota.Text = string.Format("{0:N0}", pilih);
        txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilih);
        txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilih);
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ckbx_unchecked(object sender, EventArgs e)
    {

 

        foreach (GridViewRow row in GRData.Rows)
        {

            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            ChkBoxRows.Checked = false;

        }
        txtjumlahanggota.Text = string.Format("{0:N0}", 0);
        txtjumlahPlafon.Text = string.Format("{0:N2}", 0);
        txtjumlahsaldoakhir.Text = string.Format("{0:N2}", 0);
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    decimal debetpilihx = 0, kreditpilihx = 0, pilihx = 0;
    protected void chkdtl_checked(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GRData.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("ckbx");
            if (ChkBoxRows.Checked == true)
            {

                pilihx += 1;
                debetpilihx += decimal.Parse(((Label)row.FindControl("lblplafon")).Text);
                kreditpilihx += decimal.Parse(((Label)row.FindControl("lblsaldoakhir")).Text);
            }



        }
        txtjumlahanggota.Text = string.Format("{0:N0}", pilihx);
        txtjumlahPlafon.Text = string.Format("{0:N2}", debetpilihx);
        txtjumlahsaldoakhir.Text = string.Format("{0:N2}", kreditpilihx);
        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        //GRData.UseAccessibleHeader = true;
        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


}
