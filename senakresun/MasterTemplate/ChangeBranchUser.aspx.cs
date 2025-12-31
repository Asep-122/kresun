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

public partial class MasterTemplate_ChangeBranchUser : System.Web.UI.Page
{

    public string SessLoginID
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

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                DefaultClass.Global.SqlHelper[] dlrMNC = new DefaultClass.Global.SqlHelper[2];

                dlrMNC[0] = gb.CreateSPParameter("@User", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(String));
                dlrMNC[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 20, SessKdpersh, typeof(String));

                ge.ListDataset = gb.GetDataJustOutPut("dbo.MF_GetUserBranch", dlrMNC, "constrmf");


                ge.ListDataset.Tables[0].DefaultView.Sort = "[Text] asc";

                gb.FillDDL(ge.ListDataset.Tables[0].DefaultView.ToTable(), ddlCabang, "None", "");

                ddlCabang.SelectedValue = SessBranchID;
            }
        }

        catch (Exception X)
        {
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
        }
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Session.Contents.Remove("SessBranchID");
    //    Session.Contents.Add("SessBranchID", "BGR");
    //    Request.UrlReferrer.OriginalString.ToString();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
     

        try
        {


            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
            Params[0] = gb.CreateSPParameter("@Username", ParameterDirection.Input, SqlDbType.VarChar, 20, SessLoginID, typeof(string));
            Params[1] = gb.CreateSPParameter("@BranchID", ParameterDirection.Input, SqlDbType.VarChar, 3, ddlCabang.SelectedValue, typeof(string));
            Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 3, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("MF_spInserOrUpdateSessionBranch", Params, "constrmf");
            DataTable dt = new DataTable();
            dt = ge.ListDataset.Tables[0].DefaultView.ToTable();


            if (dt.Rows[0]["statusx"].ToString() == "1")
            {
                Session.Contents.Remove("SessBranchID");                
                Session.Contents.Remove("Sessemail");
                Session.Contents.Remove("SessNik");
                Session.Contents.Remove("SessName");
                Session.Contents.Remove("Sesstgllahir");
                Session.Contents.Remove("SessIsSuperAdmin");
                Session.Contents.Remove("SessKdpersh");
                Session.Contents.Remove("SessNamaProfil");
                Session.Contents.Remove("SessAlamatProfil1");
                Session.Contents.Remove("SessAlamatProfil2");
                Session.Contents.Remove("SessKotaProfil");
                Session.Contents.Remove("SessNPWPProfil");
                Session.Contents.Remove("SessCabangAlamatProfil");
                Session.Contents.Remove("SessAlamatFakturPajakLama");
                Session.Contents.Remove("SessSystemKurs");
                Session.Contents.Remove("SessInisial");
                Session.Contents.Remove("SessFiskalBi");
                Session.Contents.Remove("SessFiskalTi");
                Session.Contents.Remove("SessTglAwalbi");
                Session.Contents.Remove("SessTglAkhirbi");
                Session.Contents.Remove("SessTutupBuku");
                Session.Contents.Remove("SessPostingLsg");
                Session.Contents.Remove("SessTutupCabang");
                Session.Contents.Remove("SessWebCompany");
                Session.Contents.Remove("SessEmailCompany");
                Session.Contents.Remove("SessIsSuperClosing");
                Session.Contents.Remove("SessKoperasiID");
                Session.Contents.Remove("SessKoperasiNama"); 

                Session.Contents.Add("SessBranchID", ddlCabang.SelectedValue);
                Session.Contents.Add("Sessemail", dt.Rows[0]["email"].ToString().Trim());
                Session.Contents.Add("SessNik", dt.Rows[0]["NIK"].ToString().Trim());
                Session.Contents.Add("SessName", dt.Rows[0]["Name"].ToString().Trim());
                Session.Contents.Add("Sesstgllahir", dt.Rows[0]["tgllahir"].ToString().Trim());               
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


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Branch','top-right','success','1');", true);

            }

            else
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Ini Pesan Error');", true);
                gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

            }

        }

        catch (Exception X)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }

    }
}