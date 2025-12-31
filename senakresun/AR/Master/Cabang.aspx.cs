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

public partial class AR_Master_Cabang : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BindGridEntity(); 
                txtalamat.Attributes.Add("maxlength", "140");
                txtpengadilan.Attributes.Add("maxlength", "90");
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
        try
        {
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
            var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTCABANG", typeof(string));
            var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("AR_CABANG", var, "constrmf");
            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang asc";

            GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
            GRData.DataBind();

            GRData.UseAccessibleHeader = true;
            GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ///pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        btnSimpan.Enabled = true;
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            STATE = "UPDATECABANG";

            btnSimpan.Enabled = false;

            DateTime now = DateTime.Now;
            tanggal = now.ToString("yyyy/MM/dd HH:MM:ss");

            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[38];
            var[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, txtKodeCabang.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@NamaCabang", ParameterDirection.Input, SqlDbType.VarChar, 50, txtNamaCabang.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@NPWP", ParameterDirection.Input, SqlDbType.VarChar, 250, txtNPWP.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@Alamat1", ParameterDirection.Input, SqlDbType.VarChar, 100, txtalamat.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@Alamat2", ParameterDirection.Input, SqlDbType.VarChar, 50, txttelp.Text, typeof(string));
            var[5] = gb.CreateSPParameter("@norek", ParameterDirection.Input, SqlDbType.VarChar, 250, txtrek.Text, typeof(string));
            var[6] = gb.CreateSPParameter("@UserLogin", ParameterDirection.Input, SqlDbType.VarChar, 10, SessLoginID, typeof(string));
            var[7] = gb.CreateSPParameter("@TglUpdateData", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, tanggal, typeof(DateTime));
            var[8] = gb.CreateSPParameter("@kepalacabang", ParameterDirection.Input, SqlDbType.VarChar, 60, txtkabag.Text, typeof(string));
            var[9] = gb.CreateSPParameter("@kepalawilayah", ParameterDirection.Input, SqlDbType.VarChar, 60, txtmanager.Text, typeof(string));
            var[10] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[11] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[12] = gb.CreateSPParameter("@tglizin", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttglizin.Text, typeof(DateTime));
            var[13] = gb.CreateSPParameter("@izin", ParameterDirection.Input, SqlDbType.VarChar, 20, txtizin.Text, typeof(string));
            var[14] = gb.CreateSPParameter("@bank", ParameterDirection.Input, SqlDbType.VarChar, 50, txtbank.Text, typeof(string));
            var[15] = gb.CreateSPParameter("@email", ParameterDirection.Input, SqlDbType.VarChar, 50, txtemail.Text, typeof(string));

            var[16] = gb.CreateSPParameter("@inisial_koperasi", ParameterDirection.Input, SqlDbType.VarChar, 10, txtinisialkoperasi.Text, typeof(string));
            var[17] = gb.CreateSPParameter("@inisial_area", ParameterDirection.Input, SqlDbType.VarChar, 10, txtinisialarea.Text, typeof(string));
            var[18] = gb.CreateSPParameter("@inisial_cabang", ParameterDirection.Input, SqlDbType.VarChar, 10, txtinisialcabang.Text, typeof(string));
            //surat
            var[19] = gb.CreateSPParameter("@PKNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 60, txtpkttd.Text, typeof(string));
            var[20] = gb.CreateSPParameter("@PKNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtpkbagian.Text, typeof(string));
            var[21] = gb.CreateSPParameter("@SPNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 60, txtsp.Text, typeof(string));
            var[22] = gb.CreateSPParameter("@SPNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtspbagian.Text, typeof(string));
            var[23] = gb.CreateSPParameter("@STUNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 60, txttugas.Text, typeof(string));
            var[24] = gb.CreateSPParameter("@STUNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txttugasbagian.Text, typeof(string));
            var[25] = gb.CreateSPParameter("@STRNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 60, txttarik.Text, typeof(string));
            var[26] = gb.CreateSPParameter("@STRNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txttarikbagian.Text, typeof(string));
            //tambahan
            var[27] = gb.CreateSPParameter("@atasnama", ParameterDirection.Input, SqlDbType.VarChar, 50, txtatasnama.Text, typeof(string));
            //surat
            var[28] = gb.CreateSPParameter("@PKMingguTtd", ParameterDirection.Input, SqlDbType.VarChar, 60, txtpkmingguan.Text, typeof(string));
            var[29] = gb.CreateSPParameter("@PKMingguBgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtpkjabatan.Text, typeof(string));
            var[30] = gb.CreateSPParameter("@STUAlamat", ParameterDirection.Input, SqlDbType.VarChar, 150, txttugasalamat.Text, typeof(string));
            var[31] = gb.CreateSPParameter("@TTKNamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 60, txtterima.Text, typeof(string));
            var[32] = gb.CreateSPParameter("@TTKNamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtterimabagian.Text, typeof(string));
            var[33] = gb.CreateSPParameter("@KANamaTtd", ParameterDirection.Input, SqlDbType.VarChar, 60, txtkartu.Text, typeof(string));
            var[34] = gb.CreateSPParameter("@KANamaBgn", ParameterDirection.Input, SqlDbType.VarChar, 30, txtkartubagian.Text, typeof(string));

            var[35] = gb.CreateSPParameter("@Pengadilan", ParameterDirection.Input, SqlDbType.VarChar, 100, txtpengadilan.Text, typeof(string));
			//tambahan note angsuran
            var[36] = gb.CreateSPParameter("@KANbMingguan", ParameterDirection.Input, SqlDbType.VarChar, 600, txtnbmingguan.Text, typeof(string));
            var[37] = gb.CreateSPParameter("@KANbBulanan", ParameterDirection.Input, SqlDbType.VarChar, 600, txtnbbulanan.Text, typeof(string));

            if (txtKodeCabang.Enabled == true)
            {
                ge.ListDataset = gb.GetDataJustOutPut("AR_CABANG", var, "constrmf");
            }
            else
            {
                ge.ListDataset = gb.GetDataJustOutPut("AR_CABANG", var, "constrmf");
            }

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
                    //pnlHeader.Visible = false;

                    txtKodeCabang.Enabled = false;
                    txtKodeCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;
                    txtNamaCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacabang")).Text;
                    //txtNPWP.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNpwp")).Text;
                    txtalamat.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblalamat")).Text;
                    txttelp.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltelp")).Text;
                    //txtrek.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnorek")).Text;
                    txtkabag.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkepalacabang")).Text;
                    txtmanager.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblmanager")).Text;
                    txtizin.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblizin")).Text;
                    txttglizin.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglizin")).Text;
                    //txtbank.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbank")).Text;

                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
                    var[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDETIL", typeof(string));
                    var[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 3, txtKodeCabang.Text, typeof(string));
                    ge.ListDataset = gb.GetDataJustOutPut("AR_CABANG", var, "constrmf");

                    txtNPWP.Text = ge.ListDataset.Tables[0].Rows[0]["npwp"].ToString();
                    txtrek.Text = ge.ListDataset.Tables[0].Rows[0]["norekeningcabang"].ToString();
                    txtbank.Text = ge.ListDataset.Tables[0].Rows[0]["bankrekeningcabang"].ToString();
                    txtatasnama.Text = ge.ListDataset.Tables[0].Rows[0]["atasnamacabang"].ToString();
                    txtemail.Text = ge.ListDataset.Tables[0].Rows[0]["email"].ToString();
                    txtinisialkoperasi.Text = ge.ListDataset.Tables[0].Rows[0]["inisial_koperasi"].ToString();
                    txtinisialarea.Text = ge.ListDataset.Tables[0].Rows[0]["inisial_area"].ToString();
                    txtinisialcabang.Text = ge.ListDataset.Tables[0].Rows[0]["inisial_cabang"].ToString();

                    txtpkttd.Text = ge.ListDataset.Tables[0].Rows[0]["PKNamaTtd"].ToString();
                    txtpkbagian.Text = ge.ListDataset.Tables[0].Rows[0]["PKNamaBgn"].ToString();
                    txtpkmingguan.Text = ge.ListDataset.Tables[0].Rows[0]["PKMingguTtd"].ToString();
                    txtpkjabatan.Text = ge.ListDataset.Tables[0].Rows[0]["PKMingguBgn"].ToString();
                    txtsp.Text = ge.ListDataset.Tables[0].Rows[0]["SPNamaTtd"].ToString();
                    txtspbagian.Text = ge.ListDataset.Tables[0].Rows[0]["SPNamaBgn"].ToString();
                    txttugas.Text = ge.ListDataset.Tables[0].Rows[0]["STUNamaTtd"].ToString();
                    txttugasbagian.Text = ge.ListDataset.Tables[0].Rows[0]["STUNamaBgn"].ToString();
                    txttugasalamat.Text = ge.ListDataset.Tables[0].Rows[0]["STUAlamat"].ToString();
                    txttarik.Text = ge.ListDataset.Tables[0].Rows[0]["STRNamaTtd"].ToString();
                    txttarikbagian.Text = ge.ListDataset.Tables[0].Rows[0]["STRNamaBgn"].ToString();
					
                    txtkartu.Text = ge.ListDataset.Tables[0].Rows[0]["KANamaTtd"].ToString();
                    txtkartubagian.Text = ge.ListDataset.Tables[0].Rows[0]["KANamaBgn"].ToString();
                    txtnbmingguan.Text = ge.ListDataset.Tables[0].Rows[0]["KANbMingguan"].ToString();
                    txtnbbulanan.Text = ge.ListDataset.Tables[0].Rows[0]["KANbBulanan"].ToString();
					
                    txtterima.Text = ge.ListDataset.Tables[0].Rows[0]["TTKNamaTtd"].ToString();
                    txtterimabagian.Text = ge.ListDataset.Tables[0].Rows[0]["TTKNamaBgn"].ToString();

                    txtpengadilan.Text = ge.ListDataset.Tables[0].Rows[0]["Pengadilan"].ToString();
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
                    txtKodeCabang.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeCabang")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, txtKodeCabang.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    ge.ListDataset = gb.GetDataJustOutPut("AR_CABANG", var1, "constrmf");

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
}