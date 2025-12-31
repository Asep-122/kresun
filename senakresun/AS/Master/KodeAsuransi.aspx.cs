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
using System.Globalization;

public partial class AS_Master_KodeAsuransi : System.Web.UI.Page
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
    private string SessSystemKurs
    {
        get { return Session["SessSystemKurs"].ToString(); }
        set { ViewState["SessSystemKurs"] = value; }
    }

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    int nosekarang = 0;
    string formatnumber = "";
    string nosebelumnya = "";
    int nonasabah = 0;
    string CRNonasabah = "";
    string updateno = "";
    string kodeawal = "";


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        txtAsuransiId.Enabled = true;
        txtAsuransiId.Focus();

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
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
                //culture
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                BindGridEntity();

                var KdCompany = SessKdpersh;
                 
                //DROPDOWNLIST SYARATBAYAR
                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
                var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORSYARATBAYAR", typeof(string));
                var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var3[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SD_KodeSupplier_Select_DDL", var3, "constrmf");
                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeSyarat asc";
                ddlSyaratBayar.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlSyaratBayar.DataTextField = "NmSyaratBayar";
                ddlSyaratBayar.DataValueField = "KodeSyarat";
                ddlSyaratBayar.DataBind();
                ddlSyaratBayar.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //DROPDOWNLIST KODESALES
                //DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[8]; 
                //var4[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKODEPEMBELI", typeof(string));
                //var4[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var4[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var4[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                //ge.ListDataset = gb.GetDataJustOutPut("AP_DDLKodeIdSupplier", var4, "constrmf");

                //ge.ListDataset.Tables[0].DefaultView.Sort = "KodePembeli asc";

                //ddlKodeSales.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                //ddlKodeSales.DataTextField = "NmPembeli";
                //ddlKodeSales.DataValueField = "KodePembeli";
                //ddlKodeSales.DataBind();
                //ddlKodeSales.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //DROPDOWNLIST KODESALES
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[4];
                var5[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORKODESTATUSPERUSAHAAN", typeof(string));
                var5[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var5[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var5[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SD_KodeSupplier_Select_DDL", var5, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodePerusahaan asc";

                ddlStatusPerusahaan.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlStatusPerusahaan.DataTextField = "NmPerusahaan";
                ddlStatusPerusahaan.DataValueField = "KodePerusahaan";
                ddlStatusPerusahaan.DataBind();
                ddlStatusPerusahaan.Items.Insert(0, new ListItem("Pilih Data", "-"));

                //DROPDOWNLIST AKUN GL
                DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[4];
                var6[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORAKUNGL", typeof(string));
                var6[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var6[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var6[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SD_KodeSupplier_Select_DDL", var6, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGl asc";

                ddlKodeAkunGL.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlKodeAkunGL.DataTextField = "namaGL";
                ddlKodeAkunGL.DataValueField = "KodeGl";
                ddlKodeAkunGL.DataBind();
                ddlKodeAkunGL.Items.Insert(0, new ListItem("Pilih Data", "-"));

                ////DROPDOWNLIST CURRENCY
                DefaultClass.Global.SqlHelper[] var7 = new DefaultClass.Global.SqlHelper[4];
                var7[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTFORCURRENCY", typeof(string));
                var7[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var7[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var7[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("SD_KodeSupplier_Select_DDL", var7, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";

                ddlCurrency.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                ddlCurrency.DataTextField = "Currency";
                ddlCurrency.DataValueField = "KodeCrc";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("Pilih Data", "-"));
                 
            }


            if (SessSystemKurs == "False")
            {
                ddlCurrency.SelectedValue = "IDR";
                ddlCurrency.Enabled = false;

            }
            else
            {
                ddlCurrency.SelectedValue = "IDR";
                ddlCurrency.Enabled = true;
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
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("[As_KodeIdAsuransi]", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAs asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    pnlHeader.Visible = false;
                    PnlFront.Visible = false;
                    pnladd.Visible = true;

                    txtAsuransiId.Enabled = false;


                    txtAsuransiId.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeAsuransi")).Text; 
                    txtNameAsuransi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNamaAsuransi")).Text;
                    txtNoRek.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoRek")).Text;
                    txtNmRek.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmrek")).Text;
                    txtAddress.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAddress")).Text;
                    txtCity.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCity")).Text;
                    txtPropinsi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblProvince")).Text;
                    txtZipCode.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodePos")).Text;
                    txtNPWP.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNPWP")).Text;
                    ddlSyaratBayar.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblSyaratBayar")).Text;
                    decimal j = decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbataskredit")).Text);
                    txtBatasKredit.Text = string.Format("{0:N2}", j);
                    txtTelp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTeleponHD")).Text;
                    txtNoFax.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoFax")).Text;
                    txtEmail.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblemail")).Text;
                    //ddlKodeSales.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodePembeli")).Text;
                    ddlStatusPerusahaan.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatusperusahaan")).Text;
                    txtMemo.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblMemo")).Text;
                    ddlKodeAkunGL.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblAkunGL")).Text;
                    ddlCurrency.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCurrency")).Text;
                    txtKontakPerson.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblHubungan")).Text;
                    txtKontakTelp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTelepon")).Text;
                    txtBagian.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblBagian")).Text;
                    txtbungaAs.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbunga")).Text;
                    var ckbx = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonaktif")).Text;
                    if (ckbx == "True")
                    {
                        cbNonAktif.Checked = true;
                    }
                    else
                    {
                        cbNonAktif.Checked = false;
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
                    txtAsuransiId.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeAsuransi")).Text;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[5];
                    var1[0] = gb.CreateSPParameter("@KodeAs", ParameterDirection.Input, SqlDbType.VarChar, 15, txtAsuransiId.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKASURANSI", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("[As_KodeIdAsuransi]", var1, "constrmf");

                    if (ge.ListDataset.Tables[0].DefaultView.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Asuransi sudah ada di transaksi tidak boleh di hapus!');", true);

                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
                        var[0] = gb.CreateSPParameter("@KodeAs", ParameterDirection.Input, SqlDbType.VarChar, 15, txtAsuransiId.Text, typeof(string));
                        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("[As_KodeIdAsuransi]", var, "constrmf");
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



                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;

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
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    //protected void ddlgroupid_onselectedchanged(object sender, EventArgs e)
    //{
    //    //DROPDOWNLIST GROUPID
    //    DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[4];
    //    //var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlJenisId.SelectedValue, typeof(string));
    //    var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
    //    var2[2] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
    //    var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

    //    ge.ListDataset = gb.GetDataJustOutPut("SD_KodeSupplier_Select_DDL", var2, "constrmf");
    //    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeGroup asc";
    //    ddlgroupid.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
    //    ddlgroupid.DataTextField = "nmGroup";
    //    ddlgroupid.DataValueField = "KodeGroup";
    //    ddlgroupid.DataBind();
    //    ddlgroupid.Items.Insert(0, new ListItem("Pilih Data", "-"));
    //}
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        var ckbx = "";
        if (cbNonAktif.Checked == true)
        {
            ckbx = "1";
        }
        else
        {
            ckbx = "0";
        }
        try
        {
            if (txtAsuransiId.Enabled == true)
            {
                STATE = "INSERT";
            }
            else
            {
                STATE = "UPDATE";
            }

            if (ddlSyaratBayar.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Syarat Bayar Masih Kosong, Isi Kembali!');", true);
            }
            else if (ddlKodeAkunGL.SelectedValue == "-")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Account Masih Kosong, Isi Kembali!');", true);
            }
            else
            {

                //var JoinValue = ddlKdWil.SelectedValue + txtCustId.Text;

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[28];
                var[0] = gb.CreateSPParameter("@KodeAs", ParameterDirection.Input, SqlDbType.VarChar, 15, txtAsuransiId.Text, typeof(string)); 
                var[1] = gb.CreateSPParameter("@NamaAs", ParameterDirection.Input, SqlDbType.VarChar, 200, txtNameAsuransi.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 200, txtAddress.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@Kota", ParameterDirection.Input, SqlDbType.VarChar, 200, txtCity.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@Propinsi", ParameterDirection.Input, SqlDbType.VarChar, 200, txtPropinsi.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@KodePos", ParameterDirection.Input, SqlDbType.VarChar, 200, txtZipCode.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@NPWP", ParameterDirection.Input, SqlDbType.VarChar, 20, txtNPWP.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@SyaratBayar", ParameterDirection.Input, SqlDbType.VarChar, 20, ddlSyaratBayar.SelectedValue, typeof(string));
                var[8] = gb.CreateSPParameter("@BatasKredit", ParameterDirection.Input, SqlDbType.Money, 30, txtBatasKredit.Text, typeof(decimal));
                var[9] = gb.CreateSPParameter("@Telepon", ParameterDirection.Input, SqlDbType.VarChar, 30, txtTelp.Text, typeof(string));
                var[10] = gb.CreateSPParameter("@NoFax", ParameterDirection.Input, SqlDbType.VarChar, 40, txtNoFax.Text, typeof(string));
                var[11] = gb.CreateSPParameter("@Email", ParameterDirection.Input, SqlDbType.VarChar, 40, txtEmail.Text, typeof(string));
                var[12] = gb.CreateSPParameter("@NoRek", ParameterDirection.Input, SqlDbType.VarChar, 20, txtNoRek.Text, typeof(string));
                var[13] = gb.CreateSPParameter("@StatusPersh", ParameterDirection.Input, SqlDbType.VarChar, 1, ddlStatusPerusahaan.SelectedValue, typeof(string));
                var[14] = gb.CreateSPParameter("@Memo", ParameterDirection.Input, SqlDbType.VarChar, 300, txtMemo.Text, typeof(string));
                var[15] = gb.CreateSPParameter("@KodeGl", ParameterDirection.Input, SqlDbType.VarChar, 40, ddlKodeAkunGL.SelectedValue, typeof(string));
                var[16] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlCurrency.SelectedValue, typeof(string));
                var[17] = gb.CreateSPParameter("@HubungDengan", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKontakPerson.Text, typeof(string));
                var[18] = gb.CreateSPParameter("@Telepon1", ParameterDirection.Input, SqlDbType.VarChar, 40, txtKontakTelp.Text, typeof(string));
                var[19] = gb.CreateSPParameter("@Jabatan", ParameterDirection.Input, SqlDbType.VarChar, 40, txtBagian.Text, typeof(string));
                var[20] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[21] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 40, SessLoginID, typeof(string));
                var[22] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.VarChar, 1, ckbx, typeof(string));
                var[23] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[24] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[25] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var[27] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNoRek.Text, typeof(string));
                var[26] = gb.CreateSPParameter("@nmrek", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNmRek.Text, typeof(string)); 
                var[27] = gb.CreateSPParameter("@bunga", ParameterDirection.Input, SqlDbType.Float, 20, txtbungaAs.Text, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("[As_KodeIdAsuransi]", var, "constrmf");

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    //btnSimpan.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully Delete this Project','top-right','success','2');", true);
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


    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

}