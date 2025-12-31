using DefaultClass;
using EntitiesClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GS_Kode_Currency : System.Web.UI.Page
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
    private string SessKdpersh
    {
        get { return Session["SessKdpersh"].ToString(); }
        set { ViewState["SessKdpersh"] = value; }
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
    protected void lnkKeluar_Click(object sender, EventArgs e)
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!Page.IsPostBack)
            {
                DateTime now = DateTime.Now;
                txttglAwal.Text = now.ToString("yyyy-MM-dd");
                txttglAkhir.Text = now.ToString("yyyy-MM-dd");
                BindGridEntity();
            }
            GRData2.UseAccessibleHeader = true;

            GRData2.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void btntambah_click(object sender, EventArgs e)
    {
        
    }

    public void detailperiode()
    {
        //GRData.DataSource = GRData.EmptyDataText;
        //GRData.DataBind();
        //GRData.UseAccessibleHeader = true;

        //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;
        List<string> rows = new List<string>(
        new string[] { "Row 1", "Row 2", "Row 3", "Row 4", "Row 5", "Row 6", "Row 7", "Row 8", "Row 9", "Row 10", "Row 11", "Row 12" });

        GRData.DataSource = rows;
        GRData.DataBind();

        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        int years = 12;

        DataTable dtCurrentTable = new DataTable();
        DataRow drCurrentRow = dtCurrentTable.NewRow();
        for (int j = 0; j < years; j++)
        {
            int i = j + 1;
            int z = 10;
            int hitung = (int)Math.Floor(Math.Log10(i)) + 1;
            int hitung2 = (int)Math.Floor(Math.Log10(z)) + 1;
            //TextBox ambikhitung = new TextBox(i); 
            int defaultint = 0;

            string convertmonth = CultureInfo.CreateSpecificCulture("id-ID").DateTimeFormat.GetMonthName(i);
            int days = DateTime.DaysInMonth(tahun, i);
            int awaltanggal = 1;
            int ambil = Convert.ToInt32(DateTime.Now.Month.ToString(i.ToString()));
            var currentYear = DateTime.Now.Year;

            var dateToCompare = new DateTime(currentYear, i, awaltanggal).ToString("dd/MM/yyyy");
            var lastdate = new DateTime(currentYear, i, days).ToString("dd/MM/yyyy");

            drCurrentRow = dtCurrentTable.NewRow();
            if ((int)Math.Floor(Math.Log10(i)) + 1 < 2)
            {
                string satudigit = "0" + i;
                ((TextBox)GRData.Rows[j].FindControl("lblambilperiode")).Text = satudigit.ToString();
            }
            else
            {
                ((TextBox)GRData.Rows[j].FindControl("lblambilperiode")).Text = i.ToString();
            }
            ((TextBox)GRData.Rows[j].FindControl("lblperiode")).Text = convertmonth.ToString();
            ((TextBox)GRData.Rows[j].FindControl("lblstart")).Text = dateToCompare.ToString();
            ((TextBox)GRData.Rows[j].FindControl("lblakhir")).Text = lastdate.ToString();
            ((TextBox)GRData.Rows[j].FindControl("lblkursstd")).Text = defaultint.ToString();
            ((TextBox)GRData.Rows[j].FindControl("lblkurspajak")).Text = defaultint.ToString();
        }
    }
    public void BindGridEntity()
    {

        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


        ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";


        GRData2.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData2.DataBind();
        GRData2.UseAccessibleHeader = true;

        GRData2.HeaderRow.TableSection = TableRowSection.TableHeader;

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //detailperiode();
        pnldepan.Visible = false;
        pnlHeader.Visible = false;
        //PnlFront.Visible = true;
        pnladd.Visible = true;
        txtkodecrc.Enabled = true;

    }

    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        string STATE = "";
        try
        {
            if (txtkodecrc.Enabled == true)
            {
                STATE = "INSERTCRC";
                //txtkodecrc.Enabled = false;
            }
            else
            {
                STATE = "INSERTPERIODE";
            }
            //int a = txtNoAwal.Text.Length;
            DateTime now = DateTime.Now;
            tanggal = now.ToString("yyyy/MM/dd HH:MM:ss");
            decimal ambilkurs = 0.00m;

            DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
            var1[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
            var1[1] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
            var1[2] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "CEKCRC", typeof(string));
            var1[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
            ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", var1, "constrmf").Tables[0];

            if( STATE == "INSERTCRC")
            {
                if (ge.ListDataTable.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Kode Currency Yang Anda Masukkan sudah Ada Di Database');", true);
                    pnldepan.Visible = false;
                    pnlHeader.Visible = false;
                    txtkodecrc.Text = "";
                }
                else
                {
                    DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[15];
                    var[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
                    var[1] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.Bit, 1, chkaktif.Checked, typeof(Boolean));
                    var[2] = gb.CreateSPParameter("@PK", ParameterDirection.Input, SqlDbType.BigInt, 1, "1", typeof(string));
                    var[3] = gb.CreateSPParameter("@AddedByFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[4] = gb.CreateSPParameter("@DateAdded", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tanggal, typeof(DateTime));
                    var[5] = gb.CreateSPParameter("@DateModified", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tanggal, typeof(DateTime));
                    var[6] = gb.CreateSPParameter("@LastUserFK", ParameterDirection.Input, SqlDbType.VarChar, 50, SessLoginID, typeof(string));
                    var[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                    var[8] = gb.CreateSPParameter("@NamaCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnama.Text, typeof(string));
                    var[9] = gb.CreateSPParameter("@Symbol", ParameterDirection.Input, SqlDbType.VarChar, 50, txtsymbol.Text, typeof(string));
                    var[10] = gb.CreateSPParameter("@Memo", ParameterDirection.Input, SqlDbType.VarChar, 50, txtmemo.Text, typeof(string));
                    var[11] = gb.CreateSPParameter("@TglTb", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, tanggal, typeof(DateTime));
                    var[12] = gb.CreateSPParameter("@KursTb", ParameterDirection.Input, SqlDbType.Money, 50, ambilkurs, typeof(decimal));
                    var[13] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "INSERTCRC", typeof(string));
                    var[14] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                    //if (txtkodecrc.Enabled == true)
                    //{
                    //    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");
                    //}
                    //else
                    //{
                    //    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");
                    //}

                    //if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','You successfully save this Project','top-right','success','2');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                    //    gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                    //}
                    
                        //DefaultClass.Global.SqlHelper[] var2 = new DefaultClass.Global.SqlHelper[3];
                        //var2[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
                        //var2[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "CEKPERIODE", typeof(string));
                        //var2[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        //ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", var2, "constrmf").Tables[0];
                        //if(ge.ListDataTable.Rows.Count <= 0)
                        //{
                            DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[8];
                            var3[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
                            //var3[1] = gb.CreateSPParameter("@Periode", ParameterDirection.Input, SqlDbType.BigInt, 0, Convert.ToInt64(ddlSP.SelectedValue), typeof(int));
                            var3[1] = gb.CreateSPParameter("@TglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, Convert.ToDateTime(txttglAwal.Text), typeof(DateTime));
                            var3[2] = gb.CreateSPParameter("@TglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, Convert.ToDateTime(txttglAkhir.Text), typeof(DateTime));
                            var3[3] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkursstd.Text, typeof(string));
                            var3[4] = gb.CreateSPParameter("@KursPajak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkurspajak.Text, typeof(string));
                            var3[5] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                            var3[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "INSERTPERIODE", typeof(string));
                            var3[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                            //ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf").Tables[0];
                            if (txtkodecrc.Enabled == true)
                            {
                                ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf");
                                ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");
                            }
                            else
                            {
                                ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf");
                                ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");
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
                        //}
                        //else if (ge.ListDataTable.Rows.Count > 0)
                        //{
                        //    DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[8];
                        //    var3[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
                        //    var3[1] = gb.CreateSPParameter("@Periode", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlSP.SelectedValue, typeof(string));
                        //    var3[2] = gb.CreateSPParameter("@TglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, Convert.ToDateTime(txttglAwal.Text), typeof(DateTime));
                        //    var3[3] = gb.CreateSPParameter("@TglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, Convert.ToDateTime(txttglAkhir.Text), typeof(DateTime));
                        //    var3[4] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkursstd.Text, typeof(string));
                        //    var3[5] = gb.CreateSPParameter("@KursPajak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkurspajak.Text, typeof(string));
                        //    var3[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "INSERTPERIODE", typeof(string));
                        //    var3[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                        //    //ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf").Tables[0];
                        //    if (txtkodecrc.Enabled == true)
                        //    {
                        //        ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf");
                        //    }
                        //    else
                        //    {
                        //        ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf");
                        //    }

                        //    if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tersimpan');", true);
                        //    }
                        //    else
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + ge.ListDataset.Tables[0].Rows[0][0].ToString() + "');", true);
                        //        gb.SaveErroLog(ge.ListDataset.Tables[0].Rows[0][0].ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));
                        //    }
                            
                        //}
                    //}
                }
            }
            else if (STATE == "INSERTPERIODE")
            {
                DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[8];
                var[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
                var[1] = gb.CreateSPParameter("@NonAktifFlag", ParameterDirection.Input, SqlDbType.Bit, 1, chkaktif.Checked, typeof(Boolean));
                var[2] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var[3] = gb.CreateSPParameter("@NamaCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtnama.Text, typeof(string));
                var[4] = gb.CreateSPParameter("@Symbol", ParameterDirection.Input, SqlDbType.VarChar, 50, txtsymbol.Text, typeof(string));
                var[5] = gb.CreateSPParameter("@Memo", ParameterDirection.Input, SqlDbType.VarChar, 50, txtmemo.Text, typeof(string));
                var[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "UPDATECRC", typeof(string));
                var[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                DefaultClass.Global.SqlHelper[] var3 = new DefaultClass.Global.SqlHelper[8];
                var3[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
                //var3[1] = gb.CreateSPParameter("@Periode", ParameterDirection.Input, SqlDbType.BigInt, 0, Convert.ToInt64(ddlSP.SelectedValue), typeof(int));
                var3[1] = gb.CreateSPParameter("@TglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, Convert.ToDateTime(txttglAwal.Text), typeof(DateTime));
                var3[2] = gb.CreateSPParameter("@TglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, Convert.ToDateTime(txttglAkhir.Text), typeof(DateTime));
                var3[3] = gb.CreateSPParameter("@KursStd", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkursstd.Text, typeof(string));
                var3[4] = gb.CreateSPParameter("@KursPajak", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkurspajak.Text, typeof(string));
                var3[5] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                var3[6] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "INSERTPERIODE", typeof(string));
                var3[7] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf").Tables[0];
                if (txtkodecrc.Enabled == true)
                {
                    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf");
                    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");
                }
                else
                {
                    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var3, "constrmf");
                    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");
                }

            }


            if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Data Tersimpan');", true);
               // pnladd.Visible = false;
               
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

    public void rubahedit()
    {
        DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
        var1[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, txtkodecrc.Text, typeof(string));
        var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "SELECTALL", typeof(string));
        var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", var1, "constrmf").Tables[0];
        ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var1, "constrmf");
        
        //((TextBox)GRData.Rows[j].FindControl("lblambilperiode")).Text = i.ToString();
        //((TextBox)GRData.Rows[j].FindControl("lblperiode")).Text = convertmonth.ToString();
        //((TextBox)GRData.Rows[j].FindControl("lblstart")).Text = dateToCompare.ToString();
        //((TextBox)GRData.Rows[j].FindControl("lblakhir")).Text = lastdate.ToString();
        //((TextBox)GRData.Rows[j].FindControl("lblkursstd")).Text = defaultint.ToString();
        //((TextBox)GRData.Rows[j].FindControl("lblkurspajak")).Text = defaultint.ToString();
        
    }
    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "SET":

                try
                {
                    txtkodecrc.Enabled = false;
                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[4];
                    var1[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 50, ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrc")).Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 100, "CEKCRC", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                    var1[3] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50,SessKdpersh, typeof(string));
                    ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", var1, "constrmf").Tables[0];
                    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var1, "constrmf").DefaultViewManager.DataSet;
                    //rubahedit();
                    pnlFilter.Visible = false;
                    pnlHeader.Visible = false; ;
                    pnldepan.Visible = false;
                    pnladd.Visible = true;
                    txtkodecrc.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrc")).Text;
                    txtnama.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacrc")).Text;
                    txtsymbol.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblsymbol")).Text;
                    txtmemo.Text = ge.ListDataset.Tables[0].Rows[0]["Memo"].ToString();

                    //ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var, "constrmf");

                    //ge.ListDataset.Tables[0].DefaultView.Sort = "KodeCrc asc";


                    //GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
                    //GRData.DataBind();
                    //GRData.UseAccessibleHeader = true;

                    //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

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


                    txtkodecrc.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodekonter")).Text;

                    DefaultClass.Global.SqlHelper[] var1 = new DefaultClass.Global.SqlHelper[3];
                    var1[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 20, txtkodecrc.Text, typeof(string));
                    var1[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "DELETE", typeof(string));
                    var1[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));


                    ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", var1, "constrmf");

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
                //}

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "tambah":

                try
                {
                    
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            case "GRUPPREVIEW":
                lblkodecurrency.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrc")).Text;
                lblnamacurrency.Text = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnamacrc")).Text;
                string kode = ((Label)GRData2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodecrc")).Text;
                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
                Params[0] = gb.CreateSPParameter("@KodeCrc", ParameterDirection.Input, SqlDbType.VarChar, 20, kode, typeof(string));
                Params[1] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
                Params[2] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 50, "SELECTPERIODE", typeof(String));
                Params[3] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
                //Params[4] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 20, SessBranchID, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("GS_CURRENCY", Params, "constrmf");
                ge.ListDataTable = gb.GetDataJustOutPut("GS_CURRENCY", Params, "constrmf").Tables[0];

                if(ge.ListDataTable.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Data Masih Kosong, Silahkan Tambah Data dengan memilih Tombol Tambah data di Colomn List data','Terimakasih');", true);
                }
                else
                {
                    GRData.DataSource = ge.ListDataset.Tables[0];
                    GRData.DataBind();

                    GRData.UseAccessibleHeader = true;
                    GRData.HeaderRow.TableSection = TableRowSection.TableHeader;


                    pnldepan.Visible = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:jv_openModal_root();", true);
                }

                break;
        }
    }

    public void OnChange_TglAkhir(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txttglAkhir.Text) < Convert.ToDateTime(txttglAwal.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:WarningAlert('Tanggal Akhir tidak boleh kurang dari Tanggal Awal!!');", true);
            txttglAkhir.Text = txttglAwal.Text;
        }
    }
    public void OnChange_TglAwal(object sender, EventArgs e)
    {
        txttglAkhir.Text = txttglAwal.Text;
    }
}

//extract the TextBox values  
//DateTime a = new DateTime(ge.ListDataset.Tables[0].Rows[i - 1]["TglAwal"]);
//string coba = Convert.ToDateTime(ge.ListDataset.Tables[0].Rows[i - 1]["TglAwal"]).ToString("dd/MM/yyyy");
//string masuk = Convert.ToDateTime(coba).ToString("dd/MM/yyyy");
//string lagi = masuk.ToString("dd/MM/yyyy");
//1.,.0000 1,0000
//decimal qwe = Convert.ToDecimal( ge.ListDataset.Tables[0].Rows[i - 1]["KursStd"]);
//string asdas = qwe.ToString().Replace(",",".");
//string xasx = qwe.ToString();
//TextBox klj = (TextBox)FindControl(asdas);
//TextBox box2 = (TextBox)FindControl(Convert.ToDecimal(ge.ListDataset.Tables[0].Rows[i - 1]["KursStd"]));
////TextBox box3 = (TextBox)ge.ListDataset.Tables[0].Rows[i - 1]["TglAkhir"];
//TextBox box4 = (TextBox)FindControl(Convert.ToString(ge.ListDataset.Tables[0].Rows[i - 1]["KursStd"]));
//TextBox box5 = (TextBox)ge.ListDataset.Tables[0].Rows[i - 1]["KursPajak"];