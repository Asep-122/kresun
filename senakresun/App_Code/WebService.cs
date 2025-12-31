using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    StringBuilder sblabel = new StringBuilder();
    

   

    public WebService()
    {

        //Uncomment the following line if using designed components 
      //InitializeComponent(); 
    }

   

    [WebMethod(EnableSession = true)]
    public string ChartBranchVsSex()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("HCIS_RELVSSEX", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            dt.DefaultView.Sort = "rel_name asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }



    [WebMethod(EnableSession = true)]
    public string ChartAnggotaBaru()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Anggota", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            dt.DefaultView.Sort = "NamaCabang asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string ChartOrder()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Order_Cair", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            dt.DefaultView.Sort = "NamaCabang asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string ChartPermohonan()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Order_Produk", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string ChartPencairan()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Cair_Produk", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    //______________________________________CONFIGURATION__________________________________________________________
    [WebMethod(EnableSession = true)]
    public string ChartUserBranch()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));

             
            ge.ListDataset = gb.GetDataJustOutPut("DSH_Configuration_Branch", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch(Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string barconf()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Configuration_Aktif", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    //______________________________________Purchase Request__________________________________________________________
    [WebMethod(EnableSession = true)]
    public string ChartPurchasingPR()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Purchasing_PR", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string PiePembelianPR()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Purchasing_TOPsupp", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    //______________________________________ACCOUNT PAYABLE__________________________________________________________
    [WebMethod(EnableSession = true)]
    public string APHutang()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_AccountPayable_HutangBulan", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string APBayarHutang()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_AccountPayable_BayarBulan", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string ApTop5()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_AccountPayable_TOPsupp", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    //______________________________________ACCOUNT EMPLOYEE__________________________________________________________
    [WebMethod(EnableSession = true)]
    public string AEGaji()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_AccountEmployee_GajiBulan", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string AEPinjaman()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_AccountEmployee_Pinjaman", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string AEPembPin()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_AccountEmployee_Angsuran", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string AeTop5()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_AccountEmployee_TOPsupp", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    //______________________________________CASH LEDGER__________________________________________________________

    [WebMethod(EnableSession = true)]
    public string ClAngsuran()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_Angsuran", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string CLdenda()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_Denda", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }  
    [WebMethod(EnableSession = true)]
    public string clpkcdg()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_PakaiCadangan", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string cltmbcdg()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_TambahanCadangan", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string clsimpok()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_SimpananPokok", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string clsimang()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_SimpananAnggota", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string clpnrsmp()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_PenarikanSimpanan", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string clbtrhtg()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_BayarHutang", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string clbyrAdv()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_CashLedger_BayarAdvance", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }


    //______________________________________MIKRO__________________________________________________________
    [WebMethod(EnableSession = true)]
    public string MikroKredit()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Mikro_PmhnKredit", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string MikroAngsuran()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Mikro_Pencairan", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string MikroTop5()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Mikro_TOPsupp", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string cvsgadai1()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_GADAI_Anggota", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "NamaCabang asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string cvsgadai2()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_GADAI_Order_Cair", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "NamaCabang asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string cvsgadai3()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_GADAI_Order_Produk", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }

    [WebMethod(EnableSession = true)]
    public string cvsgadai4()
    {
        String resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {

            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_GADAI_Cair_Produk", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string cvsgadaitop5()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_Gadai_TOPsupp", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    //FA
    [WebMethod(EnableSession = true)]
    public string faaktivattp()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_FixedAsset_AktivaTetap", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string faperaktivattp()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_FixedAsset_PerawatanAktivaTetap", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string cvlabarugi()
    {
         string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_GL_LABARUGI", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
    [WebMethod(EnableSession = true)]
    public string cvneraca()
    {
        string resultJSON = "";
        String SessBranchIDx = Session["SessBranchID"].ToString();
        String SessKdpershx = Session["SessKdpersh"].ToString();
        try
        {
            DataTable dt = new DataTable();

            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[2];
            Params[0] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpershx, typeof(String));
            Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchIDx, typeof(String));


            ge.ListDataset = gb.GetDataJustOutPut("DSH_GL_NERACA", Params, "constrmf");

            dt = ge.ListDataset.Tables[0];

            //dt.DefaultView.Sort = "unitbisnis asc";
            resultJSON = gb.DataTableToJSONWithStringBuilder(dt.DefaultView.ToTable());
        }
        catch (Exception ex)
        {

            resultJSON = ex.Message.ToString();
        }

        return resultJSON;
    }
}
