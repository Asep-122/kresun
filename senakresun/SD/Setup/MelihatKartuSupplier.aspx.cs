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
using System.Web.Script.Serialization;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class SD_View_MelihatKartuSupplier : System.Web.UI.Page
{
    Global gb = new DefaultClass.Global();
    GlobalEntities ge = new GlobalEntities();

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
    public string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
    }
    private string SessFiskalBi
    {
        get { return Session["SessFiskalBi"].ToString(); }
        set { ViewState["SessFiskalBi"] = value; }
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
                string nowx;
                nowx = gb.ReadOrderData();
                DateTime now = DateTime.Parse(nowx);
                int tahun;
                tahun = now.Year;
                DateTime date = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
                txtStart.Text = date.ToString("yyyy-MM-dd");
                DateTime xdate = new DateTime(now.Year, Int32.Parse(SessFiskalBi), 1);
                xdate = date.AddMonths(1);
                xdate = xdate.AddDays(-1);
                txtEnd.Text = xdate.ToString("yyyy-MM-dd");


            }
        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    public void btnSearch_Click(object sender, EventArgs e)
    {
        string[] splitdata = txtkodesupplier.Text.Split('-');
        string nonasabah = splitdata[0]; 

        if (txtkodesupplier.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Supplier belum di isi');", true);

        }
        else
        {
            try
            {
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
                Params[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnZipCode.Value, typeof(string));
                Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATA", typeof(string));
                Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                Params[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
                Params[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_KodeIdSupplier", Params, "constrmf");
                ge.ListDataTable = gb.GetDataJustOutPut("dbo.SD_KodeIdSupplier", Params, "constrmf").Tables[0];
                if (ge.ListDataTable.Rows.Count > 0)
                {
                    txtnamasupplier.Text = ge.ListDataset.Tables[0].Rows[0]["NamaLgn"].ToString();
                    txtalamat.Text = ge.ListDataset.Tables[0].Rows[0]["Alamat1"].ToString();
                    txtsaldoawal.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoAwal"].ToString())); 
                    txtsyaratbayar.Text = ge.ListDataset.Tables[0].Rows[0]["SyaratBayar"].ToString();
                    txtkodecrc.Text = ge.ListDataset.Tables[0].Rows[0]["KodeCrc"].ToString();
                    txtsaldoberjalan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString()));
                    //txtsaldogiro.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoGiro"].ToString()));
                    txtsaldouangmuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoUmkAwal"].ToString()));
                    txtsaldonetto.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString()));
                    txtbataskredit.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["BatasKredit"].ToString()));
                    txtoverkredit.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString())); 

                    Panel1.Visible = true;
                    Panel3.Visible = true;
                }
                else if (ge.ListDataTable.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Supplier yang di input tidak terdapat di cabang Anda');", true);
                    txtkodesupplier.Text = "";
                }
            }
            catch(Exception X)
            {
                if (X.Message != "Thread was being aborted.")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                }
            } 
        }


    }

    public void btnCariKode(object sender, EventArgs e)
    {
        if (txtkodesupplier.MaxLength == 3)
        {
            DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[5];
            Params[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnZipCode.Value, typeof(string));
            Params[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
            Params[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            Params[3] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            Params[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_KodeIdSupplier", Params, "constrmf");
             
            txtkodesupplier.Text = ge.ListDataset.Tables[0].Rows[0]["KodeBank"].ToString();
            txtkodesupplier.Attributes.Add("autocomplete", "off");
             
        }
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //switch (e.CommandName)
        //{
        //    case "DEL":
        //        int dgcount = GRRP.Rows.Count;


        //        ((Label)GRTP.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNumber")).Text = "";

        //        dtRP.Columns.Clear();

        //        dtRP = (DataTable)ViewState["ListDetailRTP"];

        //        DataRow drCurrentRow = null;

        //        dtRP.Rows.Clear();

        //        int j = 1;

        //        for (int i = 0; i < GRTP.Rows.Count; i++)
        //        {

        //            if (((Label)GRTP.Rows[i].FindControl("lblNumber")).Text != "")
        //            {


        //                drCurrentRow = dtRP.NewRow();

        //                drCurrentRow["nomor"] = j;
        //                drCurrentRow["TglTrnFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lbltglfaktur")).Text;
        //                drCurrentRow["NoFaktur"] = ((TextBox)GRTP.Rows[i].FindControl("lblnofaktur")).Text;
        //                drCurrentRow["TypeTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbltyptrn")).Text;
        //                drCurrentRow["TglJthTmp"] = ((TextBox)GRTP.Rows[i].FindControl("lbltgljthtmp")).Text;
        //                drCurrentRow["Keterangan"] = ((TextBox)GRTP.Rows[i].FindControl("lblketJPF")).Text;
        //                drCurrentRow["JumlahTrn"] = ((TextBox)GRTP.Rows[i].FindControl("lbljmlhtrn")).Text;
        //                drCurrentRow["KursStd"] = ((TextBox)GRTP.Rows[i].FindControl("lblkurstrngrtp")).Text;

        //                dtRP.Rows.Add(drCurrentRow);

        //                j++;
        //            }
        //        }

        //        if (dgcount == 1)
        //        {
        //            DataRow drdnew = dtRP.NewRow();

        //            drdnew["nomor"] = 1;
        //            drdnew["TglTrnFaktur"] = string.Empty;
        //            drdnew["NoFaktur"] = string.Empty;
        //            drdnew["TypeTrn"] = string.Empty;
        //            drdnew["TglJthTmp"] = string.Empty;
        //            drdnew["Keterangan"] = string.Empty;
        //            drdnew["JumlahTrn"] = decimal.Zero;
        //            drdnew["KursStd"] = decimal.Zero;
        //            dtRP.Rows.Add(drdnew);
        //        }


        //        ViewState["ListDetailRTP"] = dtRP;

        //        GRTP.DataSource = dtRP;
        //        GRTP.DataBind();

        //        GRTP.UseAccessibleHeader = true;
        //        GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        cssGridBayar();



        //        break;


        //}
    }

    decimal cair = 0, uangmuka = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            cair += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "JumlahTrn"));
            uangmuka += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "jumlahumk"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalcair = (Label)e.Row.FindControl("lbltotalcair");
            lbltotalcair.Text = string.Format("{0:N2}", cair);
            Label lbltotalumk = (Label)e.Row.FindControl("lbltotalumk");
            lbltotalumk.Text = string.Format("{0:N2}", uangmuka);
        }
    }


    protected void btnDetilSupplier_Click(object sender, EventArgs e)
    {
        try
        {

         
            DefaultClass.Global.SqlHelper[] varx = new DefaultClass.Global.SqlHelper[5];
            varx[0] = gb.CreateSPParameter("@kodelgn", ParameterDirection.Input, SqlDbType.VarChar, 15, hdnZipCode.Value, typeof(string));
            varx[1] = gb.CreateSPParameter("@TgltrnAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtStart.Text, typeof(DateTime));
            varx[2] = gb.CreateSPParameter("@TgltrnAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txtEnd.Text, typeof(DateTime));
            varx[3] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            varx[4] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("sdLihatKartu", varx, "constrmf");
            if (ge.ListDataset.Tables[0].Rows.Count > 0)


            {
                txtsaldoawal1.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoAwal"].ToString()));
                txtsaldoawalum.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoUmkAwal"].ToString()));
                txtsaldoberjalan.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoJalan"].ToString()));
                txtsaldouangmuka.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["SaldoUmk"].ToString()));
                Txtsaldoumakhir.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoumkakhir"].ToString()));
                txtsaldonetto.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldoakhir"].ToString()));
                txtbataskredit.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["bataskredit"].ToString()));
                txtoverkredit.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["sisakredit"].ToString()));

            }
            else
            {
                txtsaldoawal1.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtsaldoawalum.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtsaldoberjalan.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtsaldouangmuka.Text = string.Format("{0:N2}", decimal.Parse("0"));
                Txtsaldoumakhir.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtsaldonetto.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtbataskredit.Text = string.Format("{0:N2}", decimal.Parse("0"));
                txtoverkredit.Text = string.Format("{0:N2}", decimal.Parse("0"));
            }



            DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[7];

            var[0] = gb.CreateSPParameter("@KodeLgn", ParameterDirection.Input, SqlDbType.VarChar, 50, hdnZipCode.Value, typeof(string));
            var[1] = gb.CreateSPParameter("@TglFakturAwal", ParameterDirection.Input, SqlDbType.VarChar, 50, txtStart.Text, typeof(String));
            var[2] = gb.CreateSPParameter("@TglFakturAkhir", ParameterDirection.Input, SqlDbType.VarChar, 50, txtEnd.Text, typeof(String));
            var[3] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTDATATRNNEW", typeof(string));
            var[4] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            var[5] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
            var[6] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));

            ge.ListDataset = gb.GetDataJustOutPut("SD_KodeIdSupplier", var, "constrmf");
            ge.ListDataTable = gb.GetDataJustOutPut("SD_KodeIdSupplier", var, "constrmf").Tables[0];

        ge.ListDataset.Tables[0].DefaultView.Sort = "NoBukti asc"; 

        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (txtStart.Text == null || txtEnd == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Pilih Tanggal..!');", true);
            }
            else if (ge.ListDataTable.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Pada Form Filter Date Tidak ada, Silahkan Pilih Tanggal Kembali');", true);
            }
            else
            {
                PnlFront.Visible = true;
            }
        }
        catch (Exception X)
        {
            if (X.Message != "Thread was being aborted.")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            }
        }
    }


    [WebMethod]
    public static string GetKodeLgn(string InputText, string SessKdpershx, string SessBranchIDx)
    {
        List<string[]> listResult = new List<string[]>();

        string kodesupplier = InputText;
         
        DefaultClass.Global gb = new DefaultClass.Global();
        EntitiesClass.GlobalEntities ge = new EntitiesClass.GlobalEntities();

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[3];
        Params[0] = gb.CreateSPParameter("@WhereCond", ParameterDirection.Input, SqlDbType.VarChar, 0, InputText, typeof(string));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchIDx, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpershx, typeof(string));

        ge.ListDataset = gb.GetDataJustOutPut("dbo.SD_getsupplier", Params, "constrmf");

        listResult = gb.ConvertTable(ge.ListDataset.Tables[0]);

        var jsonSerialiser = new JavaScriptSerializer();
        var json = jsonSerialiser.Serialize(listResult);

        return json;
    }
}
