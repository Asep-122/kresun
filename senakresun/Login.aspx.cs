using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.IO;
using System.Web.Security;
using System.Web.UI;


public partial class Login : System.Web.UI.Page
{

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    //public string LDAPDomain
    //{
    //    get { return (string)ViewState["LDAPDomain"]; }
    //    set { ViewState["LDAPDomain"] = value; }
    //}

    public string fileName
    {
        get { return (string)ViewState["fileName"]; }
        set { ViewState["fileName"] = value; }
    }

    public string fileNameX
    {
        get { return (string)ViewState["fileNameX"]; }
        set { ViewState["fileNameX"] = value; }
    }

    public DataTable TblError
    {
        get { return (DataTable)ViewState["TblError"]; }
        set { ViewState["TblError"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!this.IsPostBack)
            {

                //lblJudul.Text = "Mega Finance System".ToUpper();

                //LDAPDomain = ConfigurationManager.AppSettings["DomainLDAP"].ToString();


                /*-------------------------------------------------------------------For Error Log generate file-----------------------*/

                TblError = gb.ErroLogTemplate();

                fileName = "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml";

                if (!File.Exists(Server.MapPath("~/ErrorLog/" + fileName)))
                {
                    TblError.WriteXml(Server.MapPath("~/ErrorLog/" + fileName));
                }


                fileNameX = "UserLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml";

                if (!File.Exists(Server.MapPath("~/UserLog/" + fileNameX)))
                {
                    TblError.WriteXml(Server.MapPath("~/UserLog/" + fileNameX));
                }

                /*-------------------------------------------------------------------For Error Log generate file-----------------------*/


            }
        }
        catch (System.Exception X)
        {
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), txtUserName.Text, Server.MapPath("~/ErrorLog/" + fileName));
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool remember = false;
        string password;
        string tutupcabang;


        try
        {

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[1];
            Params[0] = gb.CreateSPParameter("@UserName", ParameterDirection.Input, SqlDbType.VarChar, 30, txtUserName.Text, typeof(String));

            ge.ListDataset = gb.GetDataJustOutPut("MF_SPLoginMenu", Params, "constrmf");

            ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCabang desc";

            DataTable dt = new DataTable();

            dt = ge.ListDataset.Tables[0].DefaultView.ToTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["IsSuperAdmin"].ToString() == "True")
                {
                    password = gb.Decrypt(dt.Rows[0]["Password"].ToString().Trim());
                    tutupcabang = (dt.Rows[0]["TutupCabang"].ToString().Trim());
                    if (txtPassword.Text == password)
                    {
                        if (password == "")
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('please change password');", true);
                            Response.Redirect("~/MasterTemplate/Changepassword.aspx", false);
                        }
                        else
                        {
                            Session.Contents.Add("SessLoginID", txtUserName.Text.ToUpper());
                            Session.Contents.Add("Sesspassword", txtPassword.Text);
                            Session.Contents.Add("Sessemail", dt.Rows[0]["email"].ToString().Trim());
                            Session.Contents.Add("SessNik", dt.Rows[0]["NIK"].ToString().Trim());
                            Session.Contents.Add("SessName", dt.Rows[0]["Name"].ToString().Trim());
                            Session.Contents.Add("Sesstgllahir", dt.Rows[0]["tgllahir"].ToString().Trim());
                            Session.Contents.Add("SessBranchID", dt.Rows[0]["KodeCabang"].ToString().Trim());
                            Session.Contents.Add("SessIsSuperAdmin", dt.Rows[0]["IsSuperAdmin"].ToString().Trim());
                            Session.Contents.Add("SessKdpersh", ge.ListDataset.Tables[0].Rows[0]["kdpersh"].ToString().Trim());
                            Session.Contents.Add("SessNamaProfil", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString().Trim());
                            Session.Contents.Add("SessAlamatProfil1", ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString().Trim());
                            Session.Contents.Add("SessAlamatProfil2", ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString().Trim());
                            Session.Contents.Add("SessKotaProfil", ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString().Trim());
                            Session.Contents.Add("SessNPWPProfil", ge.ListDataset.Tables[0].Rows[0]["NPWP"].ToString().Trim());
                            Session.Contents.Add("SessCabangAlamatProfil", ge.ListDataset.Tables[0].Rows[0]["CabangAlamat"].ToString().Trim());
                            Session.Contents.Add("SessAlamatFakturPajakLama", ge.ListDataset.Tables[0].Rows[0]["AlamatFakturPajakLama"].ToString().Trim());
                            Session.Contents.Add("SessSystemKurs", ge.ListDataset.Tables[0].Rows[0]["SystemKurs"].ToString().Trim());
                            Session.Contents.Add("SessInisial", ge.ListDataset.Tables[0].Rows[0]["inisial"].ToString().Trim());
                            Session.Contents.Add("SessFiskalBi", ge.ListDataset.Tables[0].Rows[0]["FiskalBi"].ToString().Trim());
                            Session.Contents.Add("SessFiskalTi", ge.ListDataset.Tables[0].Rows[0]["FiskalTi"].ToString().Trim());
                            Session.Contents.Add("SessTglAwalbi", ge.ListDataset.Tables[0].Rows[0]["TglAwalbi"].ToString().Trim());
                            Session.Contents.Add("SessTglAkhirbi", ge.ListDataset.Tables[0].Rows[0]["TglAkhirbi"].ToString().Trim());
                            Session.Contents.Add("SessTutupBuku", ge.ListDataset.Tables[0].Rows[0]["TutupBuku"].ToString().Trim());
                            Session.Contents.Add("SessPostingLsg", ge.ListDataset.Tables[0].Rows[0]["PostingLsg"].ToString().Trim());
                            Session.Contents.Add("SessTutupCabang", ge.ListDataset.Tables[0].Rows[0]["TutupCabang"].ToString().Trim());
                            Session.Contents.Add("SessWebCompany", ge.ListDataset.Tables[0].Rows[0]["webCompany"].ToString().Trim());
                            Session.Contents.Add("SessEmailCompany", ge.ListDataset.Tables[0].Rows[0]["EmailCompany"].ToString().Trim());
                            Session.Contents.Add("SessIsSuperClosing", ge.ListDataset.Tables[0].Rows[0]["IsSuperClosing"].ToString().Trim());
                            Session.Contents.Add("SessKoperasiID", dt.Rows[0]["kode_koperasi"].ToString().Trim());
                            Session.Contents.Add("SessKoperasiNama", dt.Rows[0]["nama_koperasi"].ToString().Trim());
                            Session.Contents.Add("Sesstglupdate", dt.Rows[0]["tglupdate"].ToString().Trim());
                            //Response.Redirect("Branch.aspx");

                    

                                if (tutupcabang == "True")
                                {
                                    if ((dt.Rows[0]["IsSuperClosing"].ToString().Trim()) == "True")
                                    {
                                        FormsAuthentication.RedirectFromLoginPage(dt.Rows[0]["UserName"].ToString().Trim(), createPersistentCookie: remember);
                                    }
                                    else
                                    {
                                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNotification('bg-red', 'Cabang ditutup', 'top', 'right', 'animated fadeInRight', 'animated fadeOutRight')", true);
                                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang Tutup');", true);
                                    }

                                }
                                else
                                {
                                    FormsAuthentication.RedirectFromLoginPage(dt.Rows[0]["UserName"].ToString().Trim(), createPersistentCookie: remember);
                                }

                                if (password == "123456")
                                {
                                    Response.Redirect("~/MasterTemplate/Changepasswordspc.aspx");
                                }



                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Invalid Password');", true);
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNotification('bg-red', 'Invalid Password', 'top', 'right', 'animated fadeInRight', 'animated fadeOutRight')", true);

                    }
                }

                else
                {


                    password = gb.Decrypt(dt.Rows[0]["Password"].ToString().Trim());
                    tutupcabang = (dt.Rows[0]["TutupCabang"].ToString().Trim());
                    if (txtPassword.Text == password)
                    {

                        Session.Contents.Add("SessLoginID", txtUserName.Text.ToUpper());
                        Session.Contents.Add("Sesspassword", txtPassword.Text);
                        Session.Contents.Add("Sessemail", dt.Rows[0]["email"].ToString().Trim());
                        Session.Contents.Add("SessNik", dt.Rows[0]["NIK"].ToString().Trim());
                        Session.Contents.Add("SessName", dt.Rows[0]["Name"].ToString().Trim());
                        Session.Contents.Add("SessName", dt.Rows[0]["Name"].ToString().Trim());
                        Session.Contents.Add("Sesstgllahir", dt.Rows[0]["tgllahir"].ToString().Trim());
                        Session.Contents.Add("SessBranchID", dt.Rows[0]["KodeCabang"].ToString().Trim());
                        Session.Contents.Add("SessIsSuperAdmin", dt.Rows[0]["IsSuperAdmin"].ToString().Trim());
                        Session.Contents.Add("SessKdpersh", ge.ListDataset.Tables[0].Rows[0]["kdpersh"].ToString().Trim());
                        Session.Contents.Add("SessNamaProfil", ge.ListDataset.Tables[0].Rows[0]["NamaCabang"].ToString().Trim());
                        Session.Contents.Add("SessAlamatProfil1", ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString().Trim());
                        Session.Contents.Add("SessAlamatProfil2", ge.ListDataset.Tables[0].Rows[0]["Alamat2"].ToString().Trim());
                        Session.Contents.Add("SessKotaProfil", ge.ListDataset.Tables[0].Rows[0]["Alamat3"].ToString().Trim());
                        Session.Contents.Add("SessNPWPProfil", ge.ListDataset.Tables[0].Rows[0]["NPWP"].ToString().Trim());
                        Session.Contents.Add("SessCabangAlamatProfil", ge.ListDataset.Tables[0].Rows[0]["CabangAlamat"].ToString().Trim());
                        Session.Contents.Add("SessAlamatFakturPajakLama", ge.ListDataset.Tables[0].Rows[0]["AlamatFakturPajakLama"].ToString().Trim());
                        Session.Contents.Add("SessSystemKurs", ge.ListDataset.Tables[0].Rows[0]["SystemKurs"].ToString().Trim());
                        Session.Contents.Add("SessInisial", ge.ListDataset.Tables[0].Rows[0]["inisial"].ToString().Trim());
                        Session.Contents.Add("SessFiskalBi", ge.ListDataset.Tables[0].Rows[0]["FiskalBi"].ToString().Trim());
                        Session.Contents.Add("SessFiskalTi", ge.ListDataset.Tables[0].Rows[0]["FiskalTi"].ToString().Trim());
                        Session.Contents.Add("SessTglAwalbi", ge.ListDataset.Tables[0].Rows[0]["TglAwalbi"].ToString().Trim());
                        Session.Contents.Add("SessTglAkhirbi", ge.ListDataset.Tables[0].Rows[0]["TglAkhirbi"].ToString().Trim());
                        Session.Contents.Add("SessTutupBuku", ge.ListDataset.Tables[0].Rows[0]["TutupBuku"].ToString().Trim());
                        Session.Contents.Add("SessPostingLsg", ge.ListDataset.Tables[0].Rows[0]["PostingLsg"].ToString().Trim());
                        Session.Contents.Add("SessTutupCabang", ge.ListDataset.Tables[0].Rows[0]["TutupCabang"].ToString().Trim());
                        Session.Contents.Add("SessWebCompany", ge.ListDataset.Tables[0].Rows[0]["webCompany"].ToString().Trim());
                        Session.Contents.Add("SessEmailCompany", ge.ListDataset.Tables[0].Rows[0]["EmailCompany"].ToString().Trim());
                        Session.Contents.Add("SessIsSuperClosing", ge.ListDataset.Tables[0].Rows[0]["IsSuperClosing"].ToString().Trim());
                        Session.Contents.Add("SessKoperasiID", dt.Rows[0]["kode_koperasi"].ToString().Trim());
                        Session.Contents.Add("SessKoperasiNama", dt.Rows[0]["nama_koperasi"].ToString().Trim());
                        Session.Contents.Add("Sesstglupdate", dt.Rows[0]["tglupdate"].ToString().Trim());


               
                      

                        if (tutupcabang == "True")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Cabang ditutup');", true);
                            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNotification('bg-red', 'Cabang ditutup', 'top', 'right', 'animated fadeInRight', 'animated fadeOutRight')", true);
                        }
                        else
                        {
                            FormsAuthentication.RedirectFromLoginPage(dt.Rows[0]["Name"].ToString().Trim(), createPersistentCookie: remember);
                            gb.SaveUserLog("Login Masuk ", Path.GetFileName(Request.Path), txtUserName.Text.ToUpper(), Server.MapPath("~/UserLog/" + "UserLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        }

                        if (password == "123456")
                        {
                            Response.Redirect("~/MasterTemplate/Changepasswordspc.aspx");
                        }
                        //Response.Redirect("Branch.aspx");
                    }

                    else
                    {

                        // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNotification('bg-red', 'Invalid Password', 'top', 'right', 'animated fadeInRight', 'animated fadeOutRight')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Invalid Password');", true);
                        //gb.SaveUserLog("Login Invalid Password", Path.GetFileName(Request.Path), txtUserName.Text.ToUpper(), Server.MapPath("~/UserLog/" + "UserLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    }

                }
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Invalid user name');", true);

            }




        }
        catch (System.Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {
                gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), txtUserName.Text, Server.MapPath("~/ErrorLog/" + fileName));
            }
            else
            {
                //rprt.Close();
                //rprt.Dispose();
            }

            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showNotification('bg-red', '" + X.Message.ToString() + "', 'top', 'right', 'animated fadeInRight', 'animated fadeOutRight')", true);
            
        }

    }
}