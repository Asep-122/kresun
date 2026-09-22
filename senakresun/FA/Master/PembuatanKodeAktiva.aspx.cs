using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FA_Master_PembuatanAktiva : System.Web.UI.Page
{
    public int counter = 0, counternambah = 0;
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    public string fotopath = "";
    string STATE = "", aktivachecked = "", bolehdisusutchecked = "", kodeaktiva = "";
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                //DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[5];
                //var2[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "GETKODEAKTIVA", typeof(string));
                //var2[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var2[2] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //var2[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                //var2[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var2, "constrmf");
                //txtkodegroup.Text = ge.ListDataset.Tables[0].Rows[0]["GetNoBukti"].ToString();

                BindGridEntity();
                datadropdown();
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

    public void datadropdown()
    {
        try
        {
            DefaultClass.Global.SqlHelper[] var4 = new DefaultClass.Global.SqlHelper[3];
            var4[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var4[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var4[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_Group", var4, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "kode_group asc";
            ddlgroup.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlgroup.DataTextField = "groups";
            ddlgroup.DataValueField = "kode_group";
            ddlgroup.DataBind();
            ddlgroup.Items.Insert(0, new ListItem("Pilih Data", "-"));


            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYKATIVA", typeof(string));
            var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var1[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("FA_LOKASI", var1, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "value asc";
            ddllokasi.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddllokasi.DataTextField = "Text";
            ddllokasi.DataValueField = "value";
            ddllokasi.DataBind();
            ddllokasi.Items.Insert(0, new ListItem("Pilih Data", ""));

            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[4];
            var3[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            var3[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var3[2] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Float, 50, "0", typeof(string));
            var3[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var3, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeKel asc";
            ddlkelompok.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            ddlkelompok.DataTextField = "kelompok";
            ddlkelompok.DataValueField = "KodeKel";
            ddlkelompok.DataBind();
            ddlkelompok.Items.Insert(0, new ListItem("Pilih Data", ""));
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[5];
        var[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[4] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeAktiva asc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            pnlHeader.Visible = false;
            PnlFront.Visible = false;
            pnladd.Visible = true;

            ddlgroup.Enabled = true;
            ddlgroup.Focus();

            //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
            //var[0] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 20, "FA_IDBARCODE", typeof(string));
            //var[1] = gb.CreateSPParameter("@counter_number", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
            //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ////var[4] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

            //ge.ListDataset = gb.GetDataJustOutPut("FA_COUNTER", var, "constrmf");

            //if (ge.ListDataset.Tables[0].Rows.Count == 0)
            //    throw new Exception("Barcode Data Not Found!");

            //counter = int.Parse(ge.ListDataset.Tables[0].Rows[0]["counter_number"].ToString());
            //string formatnumber = ge.ListDataset.Tables[0].Rows[0]["format_number"].ToString();
            //counternambah = counter + 1;
            ////txtkodegroup.MaxLength = 5;

            /**
                | Format     | Arti                                                | Contoh jika `counternambah = 7` | Hasil       |
                | :--------- | :-------------------------------------------------- | :------------------------------ | :---------- |
                | `"D4"`     | Digit tetap 4 (pad kiri dengan 0)                   | `7`                             | `0007`      |
                | `"D6"`     | Digit tetap 6                                       | `7`                             | `000007`    |
                | `"0000"`   | Sama seperti `"D4"` tapi manual                     | `7`                             | `0007`      |
                | `"000000"` | Enam digit                                          | `7`                             | `000007`    |
                | `"N0"`     | Angka dengan format ribuan tanpa desimal            | `12345`                         | `12,345`    |
                | `"N2"`     | Angka dengan 2 desimal                              | `12345.6`                       | `12,345.60` |
                | `"000"`    | Minimal 3 digit, tambahkan nol di depan jika kurang | `45`                            | `045`       |

             */
            //string autoId = string.Format("{0}{1:" + formatnumber + "}", DateTime.Now.AddMinutes(55).ToString("yyyyMMddHHmm"), counternambah);

            //txtbarcode.Text = autoId;

        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }


    protected void ddlkelompok_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlkelompok.SelectedValue == "")
            {
                txtakuntansi.Text = "";
                txtuu.Text = "";
                txtmotedeUU.Text = "";
                txtmetodeakuntansi.Text = "";
                txtgolaktiva.Text = "";
                txtmasasusut.Text = "";
                txtmasasusutbulan.Text = "";
            }
            else
            {
                DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[5];
                var5[0] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkelompok.SelectedValue, typeof(string));
                var5[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                var5[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var5[3] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Float, 50, "0", typeof(string));
                var5[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                //var5[11] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var5, "constrmf");

                ge.ListDataset.Tables[0].DefaultView.Sort = "KodeKel asc";


                ddlkelompok.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                txtakuntansi.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutDB"].ToString();
                txtuu.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutSL"].ToString();
                txtmotedeUU.Text = "SL";
                txtmetodeakuntansi.Text = "DB";
                txtgolaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                txtmasasusut.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusut"].ToString();
                txtmasasusutbulan.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusutBulan"].ToString();
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlkelompok.SelectedValue == "")
            {
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Kelompok masih Kosong!');", true);
            }
            else if (ddlgroup.SelectedValue == "-")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Group masih Kosong!');", true);
            }
            else if ( txtketerangan.Text == "")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Keterangan masih Kosong!');", true);
            }
            else if (ddllokasi.SelectedValue == "")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Lokasi masih Kosong!');", true);
            }
            else
            {
                if (txtkodegroup.Text.Length==0)
                {
                    STATE = "INSERT";
                                      
                }
                else
                {
                    STATE = "UPDATE";
                    kodeaktiva = txtkodegroup.Text;
                }

                if (chkaktiva.Checked)
                {
                    aktivachecked = "1";
                }
                else
                {
                    aktivachecked = "0";
                }

                if (chkboleh.Checked)
                {
                    bolehdisusutchecked = "1";
                }
                else
                {
                    bolehdisusutchecked = "0";
                }

                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[30];

                var[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkodegroup.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@NamaAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaaktiva.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlgroup.SelectedValue, typeof(string));
                var[3] = gb.CreateSPParameter("@KodeLokasi", ParameterDirection.Input, SqlDbType.VarChar, 6, ddllokasi.SelectedValue, typeof(string));
                var[4] = gb.CreateSPParameter("@NoAktiva", ParameterDirection.Input, SqlDbType.VarChar, 16, txtnomoraktiva.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@Spesifikasi", ParameterDirection.Input, SqlDbType.VarChar, 16, txtspek.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@Merek", ParameterDirection.Input, SqlDbType.VarChar, 16, txtmerk.Text, typeof(string));
                var[7] = gb.CreateSPParameter("@NoMesin", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnomormesin.Text, typeof(string));
                var[8] = gb.CreateSPParameter("@NoChasis", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnomorchasis.Text, typeof(string));
                var[9] = gb.CreateSPParameter("@KodeGol", ParameterDirection.Input, SqlDbType.VarChar, 50, txtgolaktiva.Text, typeof(string));
                var[10] = gb.CreateSPParameter("@KodeKel", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkelompok.SelectedValue, typeof(string));
                var[11] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[12] = gb.CreateSPParameter("@NoPerkiraanGL", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                var[13] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, txtketerangan.Text, typeof(string));
                var[14] = gb.CreateSPParameter("@MetodeSusutAkt", ParameterDirection.Input, SqlDbType.VarChar, 50, txtmetodeakuntansi.Text, typeof(string));
                var[15] = gb.CreateSPParameter("@MetodeSusutUU", ParameterDirection.Input, SqlDbType.VarChar, 50, txtmotedeUU.Text, typeof(string));
                var[16] = gb.CreateSPParameter("@TarifSusutAkt", ParameterDirection.Input, SqlDbType.Float, 0, Convert.ToDouble("0"), typeof(float));
                var[17] = gb.CreateSPParameter("@TarifSusutUU", ParameterDirection.Input, SqlDbType.Float, 0, Convert.ToDouble(txtuu.Text == string.Empty ? "0" : txtuu.Text), typeof(float));
                var[18] = gb.CreateSPParameter("@JlhRawat", ParameterDirection.Input, SqlDbType.Float, 10, Convert.ToDouble("0"), typeof(float));
                var[19] = gb.CreateSPParameter("@JlhRawatSBlnLalu", ParameterDirection.Input, SqlDbType.Float, 10, Convert.ToDouble("0"), typeof(float));
                var[20] = gb.CreateSPParameter("@AktivaAktif", ParameterDirection.Input, SqlDbType.VarChar, 50, aktivachecked, typeof(string));
                var[21] = gb.CreateSPParameter("@BolehSusut", ParameterDirection.Input, SqlDbType.VarChar, 50, bolehdisusutchecked, typeof(string));
                var[22] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                var[23] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[24] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[25] = gb.CreateSPParameter("@path", ParameterDirection.Input, SqlDbType.VarChar, 50, txtpath.Text, typeof(string));
                var[26] = gb.CreateSPParameter("@barcode", ParameterDirection.Input, SqlDbType.VarChar, 100, txtbarcode.Text, typeof(string));
                var[27] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[28] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                var[29] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var, "constrmf");
              

                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AlertSuccess('Save Done!','You successfully save this Project','bottom-center','success','true');", true);

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

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try
                {

                    aktivachecked = "";
                    bolehdisusutchecked = "";
                    if (chkaktiva.Checked)
                    {
                        aktivachecked = "1";
                    }
                    else
                    {
                        aktivachecked = "0";
                    }

                    if (chkboleh.Checked)
                    {
                        bolehdisusutchecked = "1";
                    }
                    else
                    {
                        bolehdisusutchecked = "0";
                    }


                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    pnlHeader.Visible = false;

                    txtkodegroup.Enabled = false;
                    //ddlgroup.Visible = false;


                    txtkodegroup.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdaktiva")).Text;
                    txtnamaaktiva.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnmaktiva")).Text;
                    ddllokasi.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdlokasi")).Text;
                    txtnomoraktiva.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnoaktiva")).Text;
                    txtspek.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblSpesifikasi")).Text;
                    txtmerk.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblMerek")).Text;
                    txtnomormesin.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoMesin")).Text;
                    txtnomorchasis.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNoChasis")).Text;
                    txtgolaktiva.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeGol")).Text;
                    ddlkelompok.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKodeKel")).Text;
                    txtketerangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    ddlgroup.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdgroup")).Text;
                    //txtmetodeakuntansi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblMetodeSusutAkt")).Text;
                    //txtmotedeUU.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblMetodeSusutUU")).Text; 
                    //txtakuntansi.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTarifSusutAkt")).Text;
                    //txtuu.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblTarifSusutUU")).Text;
                    //txtkodegroup.MaxLength = 8;


                    DefaultClass.Global.SqlHelper[] var5 = new DefaultClass.Global.SqlHelper[5];
                    var5[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodegroup.Text, typeof(string));
                    var5[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var5[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var5[3] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var5[4] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var5, "constrmf");

                    //txtakuntansi.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutDB"].ToString();
                    //txtuu.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutSL"].ToString();
                    //txtmotedeUU.Text = "SL";
                    //txtmetodeakuntansi.Text = "DB";
                    //txtgolaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                    //txtmasasusut.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusut"].ToString();
                    //txtmasasusutbulan.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusutBulan"].ToString();
                    chkaktiva.Checked = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["AktivaAktif"].ToString());
                    chkboleh.Checked = Convert.ToBoolean(ge.ListDataset.Tables[0].Rows[0]["BolehSusut"].ToString());
                    txtbarcode.Text = ge.ListDataset.Tables[0].Rows[0]["barcode_id"].ToString();
                    Image1.ImageUrl = ge.ListDataset.Tables[0].Rows[0]["filepathphoto"].ToString();
                    txtpath.Text = ge.ListDataset.Tables[0].Rows[0]["filepathphoto"].ToString();

                    DefaultClass.Global.SqlHelper[] var6 = new DefaultClass.Global.SqlHelper[5];
                    var6[0] = gb.CreateSPParameter("@KodeKelompok", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlkelompok.SelectedValue, typeof(string));
                    var6[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTBYID", typeof(string));
                    var6[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var6[3] = gb.CreateSPParameter("@Bulan", ParameterDirection.Input, SqlDbType.Float, 50, "0", typeof(string));
                    var6[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    //var5[11] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_KELOMPOK", var6, "constrmf");

                    ge.ListDataset.Tables[0].DefaultView.Sort = "KodeKel asc";

                    txtakuntansi.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutDB"].ToString();
                    txtuu.Text = ge.ListDataset.Tables[0].Rows[0]["TarifSusutSL"].ToString();
                    txtmotedeUU.Text = "SL";
                    txtmetodeakuntansi.Text = "DB";
                    txtgolaktiva.Text = ge.ListDataset.Tables[0].Rows[0]["KodeGol"].ToString();
                    txtmasasusut.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusut"].ToString();
                    txtmasasusutbulan.Text = ge.ListDataset.Tables[0].Rows[0]["MasaSusutBulan"].ToString();
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


                    string kodeaktiva = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkdaktiva")).Text;


                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[30];

                    var1[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, kodeaktiva, typeof(string));
                    var1[1] = gb.CreateSPParameter("@NamaAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[2] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[3] = gb.CreateSPParameter("@KodeLokasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[4] = gb.CreateSPParameter("@NoAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[5] = gb.CreateSPParameter("@Spesifikasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[6] = gb.CreateSPParameter("@Merek", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[7] = gb.CreateSPParameter("@NoMesin", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[8] = gb.CreateSPParameter("@NoChasis", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[9] = gb.CreateSPParameter("@KodeGol", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[10] = gb.CreateSPParameter("@KodeKel", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[11] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[12] = gb.CreateSPParameter("@NoPerkiraanGL", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[13] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[14] = gb.CreateSPParameter("@MetodeSusutAkt", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[15] = gb.CreateSPParameter("@MetodeSusutUU", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[16] = gb.CreateSPParameter("@TarifSusutAkt", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                    var1[17] = gb.CreateSPParameter("@TarifSusutUU", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                    var1[18] = gb.CreateSPParameter("@JlhRawat", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                    var1[19] = gb.CreateSPParameter("@JlhRawatSBlnLalu", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                    var1[20] = gb.CreateSPParameter("@AktivaAktif", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[21] = gb.CreateSPParameter("@BolehSusut", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[22] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                    var1[23] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CHECKTRXFATRN", typeof(string));
                    var1[24] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[25] = gb.CreateSPParameter("@path", ParameterDirection.Input, SqlDbType.VarChar, 50, txtpath.Text, typeof(string));
                    var1[26] = gb.CreateSPParameter("@barcode", ParameterDirection.Input, SqlDbType.VarChar, 100, txtbarcode.Text, typeof(string));
                    var1[27] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var1[28] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                    var1[29] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var1, "constrmf");
                    GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();

                    if (ge.ListDataset.Tables[0].Rows.Count != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Aktiva sudah ada pada transaksi tidak dapat dihapus!');", true);

                    }
                    else
                    {

                        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[30];

                        var[0] = gb.CreateSPParameter("@KodeAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, kodeaktiva, typeof(string));
                        var[1] = gb.CreateSPParameter("@NamaAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[2] = gb.CreateSPParameter("@KodeGroup", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[3] = gb.CreateSPParameter("@KodeLokasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[4] = gb.CreateSPParameter("@NoAktiva", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[5] = gb.CreateSPParameter("@Spesifikasi", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[6] = gb.CreateSPParameter("@Merek", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[7] = gb.CreateSPParameter("@NoMesin", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[8] = gb.CreateSPParameter("@NoChasis", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[9] = gb.CreateSPParameter("@KodeGol", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[10] = gb.CreateSPParameter("@KodeKel", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[11] = gb.CreateSPParameter("@KodeUser", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[12] = gb.CreateSPParameter("@NoPerkiraanGL", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[13] = gb.CreateSPParameter("@Keterangan", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[14] = gb.CreateSPParameter("@MetodeSusutAkt", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[15] = gb.CreateSPParameter("@MetodeSusutUU", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[16] = gb.CreateSPParameter("@TarifSusutAkt", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                        var[17] = gb.CreateSPParameter("@TarifSusutUU", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                        var[18] = gb.CreateSPParameter("@JlhRawat", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                        var[19] = gb.CreateSPParameter("@JlhRawatSBlnLalu", ParameterDirection.Input, SqlDbType.Float, 10, "0", typeof(string));
                        var[20] = gb.CreateSPParameter("@AktivaAktif", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[21] = gb.CreateSPParameter("@BolehSusut", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[22] = gb.CreateSPParameter("@user", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[23] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                        var[24] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        var[25] = gb.CreateSPParameter("@path", ParameterDirection.Input, SqlDbType.VarChar, 50, "-", typeof(string));
                        var[26] = gb.CreateSPParameter("@barcode", ParameterDirection.Input, SqlDbType.VarChar, 100, txtbarcode.Text, typeof(string));
                        var[27] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                        var[28] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                        var[29] = gb.CreateSPParameter("@GetNoBukti", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                        ge.ListDataset = gb.GetDataJustOutPut("FA_PembuatanAktiva", var, "constrmf");
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
    protected void UploadFile(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/Files/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

        //Display the Picture in Image control.
        Image1.ImageUrl = "~/Files/" + Path.GetFileName(FileUpload1.FileName);

        fotopath = folderPath + Path.GetFileName(FileUpload1.FileName);
        txtpath.Text = Image1.ImageUrl;
    }
}