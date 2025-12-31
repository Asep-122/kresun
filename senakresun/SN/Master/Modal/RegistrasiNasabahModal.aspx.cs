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
using System.Web.Script.Serialization;
using System.Web.Services;
using DocumentFormat.OpenXml.Drawing.Charts;

public partial class FA_Master_RegistrasiNasabahModal: System.Web.UI.Page
{
    String no_nasabah = "", nosebelumnya = "", updateno = "", CRNonasabah = "", STATE = "", kdpersh = "",
           formatnumber = "", msg1 = "", msg2 = "", msg3 = "", msg4 = "", msg5 = "", msg6 = "", msg7 = "", namafileKTP = "", namafileKK = "",
           namafileNPWP = "", namafileLainLain1 = "", namafileLainLain2 = "", namafilektppasangan = "";
    int counter_nonasabah = 0;
    long nosekarang = 0, nonasabah = 0;
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

    public string fotopathktp = "", tanggal = "", fotopathnpwp = "";
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

    public string SessKoperasiID
    {
        get { return Session["SessKoperasiID"].ToString(); }
        set { ViewState["SessKoperasiID"] = value; }
    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnBack, string.Empty, true);
    }

    protected string btnafterDelete()
    {
        return Page.ClientScript.GetPostBackEventReference(LinkButton1, string.Empty, true);
    }

    protected void txtidentitas_TextChanged(object sender, EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[4];
        //var[0] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamanasabah.Text, typeof(string));
        //var[1] = gb.CreateSPParameter("@nama_ibu_kandung", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnamaibukandung.Text, typeof(string));
        //var[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTP", typeof(string));
        //var[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        
        //ge.ListDataset = gb.GetDataJustOutPut("AR_NASABAH", var, "constrmf");

        //if (ge.ListDataset.Tables[0].DefaultView.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Nasabah sudah terdaftar');", true);

        //}

    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
       
    }
 

   
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RegistrasiNasabahModal.aspx");
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegistrasiNasabahModal.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
               
                BindGridEntity();

                //InsertCombo();
                //txttgllahirasuransi.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
    protected void lnkCari_Click(object sender, EventArgs e)
    {

        if (txtSearch.Text == "")
        {
            BindGridEntity();
        }
        else
        {
            BindGridEntityx();
        }
        

    }
    public void BindGridEntityx()
    {


        string sign = txtSearch.Text.Trim().Contains("%") ? " like " : " = ";

        var WhereCond = string.Format(" {0} ", txtSearch.Text.Trim() != "" ? ddlSearch.SelectedValue + sign + "'" + txtSearch.Text + "'" : "");

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKoperasiID, typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("SM_GetNasabah", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }

    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("SM_NASABAH", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "no_nasabah desc";

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
                HDState.Value = e.CommandName;

                try

                {

                    var datax = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("LblCabangdaftar")).Text;
                  
                    if (datax == SessBranchID)
                    {



                        PnlFront.Visible = false;
                        pnladd.Visible = true;
                        pnlHeader.Visible = false;
                        pnlFilter.Visible = false;


                        txtanggota.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        txtnamanasabah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                        ddljenistrn.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltype")).Text;
                        txtidentitas.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblidentitas")).Text;
                        txtNPWP.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNpwp")).Text;
                        ddlstatusanggota.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatusanggota")).Text;
                        txtanggota.Enabled = false;
                        txtnamanasabah.Enabled = false;
                        txtidentitas.Enabled = false;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota hanya bisa diedit pada cabang yang mendaftarkan');", true);

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

                    var datax = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("LblCabangdaftar")).Text;

                    if (datax == SessBranchID)
                    {



                        PnlFront.Visible = false;
                        pnladd.Visible = true;
                        pnlHeader.Visible = false;
                        pnlFilter.Visible = false;
                        btndelete.Visible = true;
                        btnNexIdentitas.Visible = false;

                        txtanggota.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnonasabah")).Text;
                        txtnamanasabah.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnama")).Text;
                        ddljenistrn.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltype")).Text;
                        txtidentitas.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblidentitas")).Text;
                        txtNPWP.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNpwp")).Text;
                        ddlstatusanggota.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblstatusanggota")).Text;
                        txtanggota.Enabled = false;
                        txtnamanasabah.Enabled = false;
                        txtidentitas.Enabled = false;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Anggota hanya bisa diedit pada cabang yang mendaftarkan');", true);

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



    protected void btnAdd_Click(object sender, EventArgs e)
    {

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        pnladd.Visible = true;
        pnlFilter.Visible=false;
        pnladd.Visible = true;
        Pnlcustomer.Visible = true;

        //DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[2];
        //var1[0] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTKONTERNASABAH", typeof(string));
        //var1[1] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

        //ge.ListDataset = gb.GetDataJustOutPut("GETCOUNTER", var1, "constrmf");

        //nosekarang = long.Parse(ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString());
        //formatnumber = ge.ListDataset.Tables[0].Rows[0]["NoAwal"].ToString();
        //nosebelumnya = ge.ListDataset.Tables[0].Rows[0]["NoSekarang"].ToString();

        //nonasabah = nosekarang + 1;

        //CRNonasabah = SessBranchID + String.Format("{0:" + formatnumber + "}", nonasabah);

        //updateno = String.Format("{0:" + formatnumber + "}", nonasabah);

        //HDNoNasabah.Value = CRNonasabah;
        //HDNoSebelumnya.Value = nosebelumnya;

        //txtkodepetugas.Text = SessLoginID;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {

        try
        {
                if (HDState.Value == "SET")
                {
                    STATE = "UPDATE";
                }
                else
                {
                    STATE = "INSERT";
                    HDNoNasabah.Value = "-";
                }

            int btnblock = 0;
            if (STATE == "INSERT")
            {
                DefaultClass.Global.SqlHelper[] vart4 = new DefaultClass.Global.SqlHelper[5];
                vart4[0] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitas.Text, typeof(string));
                vart4[1] = gb.CreateSPParameter("@kodekoperasi", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKoperasiID, typeof(string));
                vart4[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                vart4[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                vart4[4] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "CEKKTP", typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SM_NASABAH", vart4, "constrmf");

                if (ge.ListDataset.Tables[0].Rows.Count > 0)

                {
                    btnblock = 1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Info Id Ktp sudah terdapat pada anggota yg telah didaftarkan');", true);

                }
                else
                {
                   
                    btnblock = 0;
                }

            }
           


            if (btnblock == 0)
            {

            
            //INSERT INTO ARNASABAH
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[11];

                var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 20, txtanggota.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamanasabah.Text, typeof(string));
                var[2] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitas.Text, typeof(string));
                var[3] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNPWP.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@type", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenistrn.SelectedValue, typeof(string));
                var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
                var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                var[7] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
                var[8] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
                var[9] = gb.CreateSPParameter("@Userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
                var[10] = gb.CreateSPParameter("@statusanggota", ParameterDirection.Input, SqlDbType.VarChar, 5, ddlstatusanggota.SelectedValue, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("SM_NASABAH", var, "constrmf");

                msg1 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();






            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                        btnNexIdentitas.Visible = false;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);

              }
          

               

                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
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

    protected void btndelete_Click(object sender, EventArgs e)
    {

        try
        {
           
            string STATE = "DELETE";
   

            //INSERT INTO ARNASABAH
            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[10];

            var[0] = gb.CreateSPParameter("@no_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 20, txtanggota.Text, typeof(string));
            var[1] = gb.CreateSPParameter("@nama_nasabah", ParameterDirection.Input, SqlDbType.VarChar, 80, txtnamanasabah.Text, typeof(string));
            var[2] = gb.CreateSPParameter("@no_identitas", ParameterDirection.Input, SqlDbType.VarChar, 30, txtidentitas.Text, typeof(string));
            var[3] = gb.CreateSPParameter("@no_npwp", ParameterDirection.Input, SqlDbType.VarChar, 30, txtNPWP.Text, typeof(string));
            var[4] = gb.CreateSPParameter("@type", ParameterDirection.Input, SqlDbType.VarChar, 10, ddljenistrn.SelectedValue, typeof(string));
            var[5] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, STATE, typeof(string));
            var[6] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[7] = gb.CreateSPParameter("@Kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 250, SessBranchID, typeof(string));
            var[8] = gb.CreateSPParameter("@KdPersh", ParameterDirection.Input, SqlDbType.VarChar, 250, SessKdpersh, typeof(string));
            var[9] = gb.CreateSPParameter("@Userlogin", ParameterDirection.Input, SqlDbType.VarChar, 250, SessLoginID, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("SM_NASABAH", var, "constrmf");

            msg1 = ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success").ToString();






            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {

                btnNexIdentitas.Visible = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','You successfully save this Project','top-right','success','2');", true);

            }




            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));


            }





        }
        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }


    }


    public void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {



            txtanggota.Text = HDNoNasabah.Value;
            txtnamanasabah.Text = HDNama.Value;
            txtidentitas.Text = HDKtp.Value;


            //txtkodelgn.Text = kodelgn;
            //txtNmSup.Text = norekening;

            //txtsimpananPokok.Text = string.Format("{0:N0}", decimal.Parse("10000"));
            //txtsimpananwajib.Text = string.Format("{0:N0}", decimal.Parse("10000"));
            //txtbayar.Text = string.Format("{0:N0}", decimal.Parse("20000"));


        }

        catch (Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString().Replace("'", "") + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }








    [WebMethod]
    public static string GetCustomer(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        // empResult.Add(KDBRG);
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        //Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(String));
        Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
        Params[2] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.AR_getCustomer_Aktif_SM", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;

    }


}