using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DefaultClass;
using EntitiesClass;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class CL_View_MonitoringKHT : System.Web.UI.Page
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
    DataTable dtRP = new DataTable("Transaction");
    DataTable dtBayar = new DataTable("TransactionBayar");
    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    Global gbx = new Global();
    GlobalEntities gex = new GlobalEntities();
    Global gby = new Global();
    GlobalEntities gey = new GlobalEntities();

    Global gbs = new Global();
    GlobalEntities ges = new GlobalEntities();
    int blnadd = 0;
    string statusclosing, statusposting;
    string form = "R6";
    string kodesumber = "CL";
    string KodeKonter = "CL09";
    //string xkodejournal = "HTGAS01";
    public string nobuktidb;
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        blnadd = 0;
        //GRTP.Enabled = true;

        pnlHeader.Visible = false;
        PnlFront.Visible = false;
        //Pnlbank.Visible = true;
        pnlFilter.Visible = false;

        txtnobukti.Text = "";
        pnladd.Visible = true;
        //Pnlbank.Visible = true;
        Pnldetail.Visible = true;

        txtBankGiro.Text = "-";
        txtnoGiro.Text = "-";
        txtKeterangan.Text = "Pengecekan Kas Head Teller";
        txtKeterangan.Enabled = false;
        Pnlfooter4.Visible = true;

        ddlsaldokashead(sender, e);
        if (ddladjust.SelectedValue == "A")
        {

            txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = true;
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
        }
        else if (ddladjust.SelectedValue == "B")
        {
            //txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
            //txtsaldoakhiradjust.Text = "0";
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
        }
        else if (ddladjust.SelectedValue == "C")
        {
            //txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) - decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) - decimal.Parse(txtnilaiadjust.Text));
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void txtlogam1_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam1.Text) * 1000;
        txtlogamtotal1.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam2_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam2.Text) * 500;
        txtlogamtotal2.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam3_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam3.Text) * 200;
        txtlogamtotal3.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam4_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam4.Text) * 100;
        txtlogamtotal4.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam5_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam5.Text) * 50;
        txtlogamtotal5.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam6_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam6.Text) * 25;
        txtlogamtotal6.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam7_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam7.Text) * 10;
        txtlogamtotal7.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam8_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam8.Text) * 5;
        txtlogamtotal8.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlogam9_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlogam9.Text) * 1;
        txtlogamtotal9.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar1_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar1.Text) * 100000;
        txtlembartotal1.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar2_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar2.Text) * 50000;
        txtlembartotal2.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar3_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar3.Text) * 20000;
        txtlembartotal3.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar4_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar4.Text) * 10000;
        txtlembartotal4.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar5_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar5.Text) * 5000;
        txtlembartotal5.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar6_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar6.Text) * 2000;
        txtlembartotal6.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar7_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar7.Text) * 1000;
        txtlembartotal7.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar8_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar8.Text) * 500;
        txtlembartotal8.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void txtlembar9_TextChanged(object sender, EventArgs e)
    {

        decimal bayar, jumlahlembar, jumlahlogam;
        bayar = decimal.Parse(txtlembar9.Text) * 100;
        txtlembartotal9.Text = string.Format("{0:N2}", bayar);
        jumlahlembar = decimal.Parse(txtlembartotal1.Text) + decimal.Parse(txtlembartotal2.Text) + decimal.Parse(txtlembartotal3.Text) + decimal.Parse(txtlembartotal4.Text) + decimal.Parse(txtlembartotal5.Text) + decimal.Parse(txtlembartotal6.Text) + decimal.Parse(txtlembartotal7.Text) + decimal.Parse(txtlembartotal8.Text) + decimal.Parse(txtlembartotal9.Text);
        jumlahlogam = decimal.Parse(txtlogamtotal1.Text) + decimal.Parse(txtlogamtotal2.Text) + decimal.Parse(txtlogamtotal3.Text) + decimal.Parse(txtlogamtotal4.Text) + decimal.Parse(txtlogamtotal5.Text) + decimal.Parse(txtlogamtotal6.Text) + decimal.Parse(txtlogamtotal7.Text) + decimal.Parse(txtlogamtotal8.Text) + decimal.Parse(txtlogamtotal9.Text);
        txtlembartotal.Text = string.Format("{0:N2}", jumlahlembar);
        txtlogamtotal.Text = string.Format("{0:N2}", jumlahlogam);
        txttransAP.Text = string.Format("{0:N2}", jumlahlembar + jumlahlogam);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CultureName = CultureInfo.CurrentCulture.TextInfo.CultureName;
                CultureName = "id-ID";
                //BindGridEntity();

                //get tanggal server
                DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[0];
                ge.ListDataset = gb.GetDataJustOutPut("gettglsekarang", varT, "constrmf");
                DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
                txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
                //txttgljthtmp.Text = txttgltrn.Text;

                DateTime awaldate = DateTime.Now.AddYears(-10);
                DateTime akhirdate = DateTime.Now;
                int tahunawal = Int32.Parse(awaldate.Year.ToString());
                int tahunakhir = Int32.Parse(akhirdate.Year.ToString());
                int count = 0;
                for (int i = tahunawal; i <= tahunakhir; i++)
                {
                    ddlyear.Items.Insert(count, new ListItem(i.ToString(),i.ToString()));
                    count++;
                }
                ddlyear.SelectedIndex = count/2;

                //get tanggal server
                GetddlCabang();
                ddlsp_SelectedIndexChanged(sender, e);
                //BindGridEntity();
            }



            //GRData.UseAccessibleHeader = true;
            //GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        catch (System.Exception X)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
            gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

        }
    }
    protected void ddlsaldokashead(object sender, EventArgs e)
    {
        //var bank = ddlKdBank.SelectedValue;
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[3];
        var[0] = gb.CreateSPParameter("@kdPersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        var[2] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
        ge.ListDataset = gb.GetDataJustOutPut("CL_SALDO_KAS_HEAD", var, "constrmf");
        if (ge.ListDataset.Tables[0].Rows.Count > 0)
        {

            txtsaldoakhir.Text = string.Format("{0:N2}", decimal.Parse(ge.ListDataset.Tables[0].Rows[0]["saldo"].ToString()));



        }
        else
        {
            txtsaldoakhir.Text = "0";


        }

        if (ddladjust.SelectedValue == "A")
        {

            txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = true;
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));

            //txtsaldoakhirsystemadjust
        }
        else if (ddladjust.SelectedValue == "B")
        {
            //txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
            //txtsaldoakhiradjust.Text = "0";
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
        }
        else if (ddladjust.SelectedValue == "C")
        {
            //txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) - decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) - decimal.Parse(txtnilaiadjust.Text));
        }


    }


    protected void ddladjust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddladjust.SelectedValue == "A")
        {

            txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = true;
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
        }
        else if (ddladjust.SelectedValue == "B")
        {
            txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
            //txtsaldoakhiradjust.Text = "0";
            //txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
        }
        else if (ddladjust.SelectedValue == "C")
        {
            txtnilaiadjust.Text = "0";
            txtsaldoakhiradjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
        }
        //txtkursbank.Text = ddlkursbank.SelectedValue;
    }

    protected void txtnilaiadjust_TextChanged(object sender, EventArgs e)
    {
        if (ddladjust.SelectedValue == "B")
        {
            //txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
            //txtsaldoakhiradjust.Text = "0";
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) + decimal.Parse(txtnilaiadjust.Text));

        }
        else if (ddladjust.SelectedValue == "C")
        {
            //txtnilaiadjust.Text = "0";
            txtnilaiadjust.ReadOnly = false;
            txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) - decimal.Parse(txtnilaiadjust.Text));
            txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(txtsaldoakhir.Text) - decimal.Parse(txtnilaiadjust.Text));
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

        DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[4];
        Params[0] = gb.CreateSPParameter("@wherecond", ParameterDirection.Input, SqlDbType.VarChar, 0, WhereCond, typeof(String));
        Params[1] = gb.CreateSPParameter("@kodecabang", ParameterDirection.Input, SqlDbType.VarChar, 50, SessBranchID, typeof(string));
        Params[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        Params[3] = gb.CreateSPParameter("@state", ParameterDirection.Input, SqlDbType.VarChar, 50, "TRXTFANTARBANK", typeof(string));
        ge.ListDataset = gb.GetDataJustOutPut("CL_GetSEARCH", Params, "constrmf");

        GRData.DataSource = ge.ListDataset.Tables[0];
        GRData.DataBind();


        GRData.UseAccessibleHeader = true;
        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;



    }
    public void BindGridEntity()
    {
        DefaultClass.Global.SqlHelper[] var = new DefaultClass.Global.SqlHelper[9];
        var[0] = gb.CreateSPParameter("@form", ParameterDirection.Input, SqlDbType.VarChar, 8, form, typeof(string));
        var[1] = gb.CreateSPParameter("@STATE", ParameterDirection.Input, SqlDbType.VarChar, 50, "ListKHT", typeof(string));
        var[2] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));
        var[3] = gb.CreateSPParameter("@kdcabang", ParameterDirection.Input, SqlDbType.VarChar, 50, ddlcabang.SelectedValue, typeof(string));
        var[4] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 50, SessKdpersh, typeof(string));
        var[5] = gb.CreateSPParameter("@DteTglAwal", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtStart.Text, typeof(DateTime));
        var[6] = gb.CreateSPParameter("@DteTglAkhir", ParameterDirection.Input, SqlDbType.SmallDateTime, 50, txtEnd.Text, typeof(DateTime));
        var[7] = gb.CreateSPParameter("@bulan", ParameterDirection.Input, SqlDbType.Int, 0, ddlSP.SelectedValue, typeof(int));
        var[8] = gb.CreateSPParameter("@tahun", ParameterDirection.Input, SqlDbType.Int, 0, ddlyear.SelectedValue, typeof(int));


        ge.ListDataset = gb.GetDataJustOutPut("CL_MonitorKHT", var, "constrmf");

        ge.ListDataset.Tables[0].DefaultView.Sort = "TglTrn desc";


        GRData.DataSource = ge.ListDataset.Tables[0].DefaultView.ToTable();
        GRData.DataBind();

        GRData.UseAccessibleHeader = true;

        GRData.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ddltellerhdr_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
    }

    protected void ddlcabang_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridEntity();
    }
    protected void txtdatex_TextChanged(object sender, System.EventArgs e)
    {
        //DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        //varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrnx.Text, typeof(DateTime));
        //varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        //varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        //varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        //varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        //ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        //string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        //DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        //if (xstatus == "1")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
        //    txttgltrnx.Text = xtanggal.ToString("dd-MM-yyyy");
        //    BindGridEntity();
        //}
        //else
        //{
        //    BindGridEntity();
        //}
    }

    decimal total = 0;
    protected void GRData_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NilaiAdjust"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotal = (Label)e.Row.FindControl("lbltotal");
            lbltotal.Text = string.Format("{0:N2}", total);

        }
    }

    protected void GRData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        pnlFilter.Visible = false;
        switch (e.CommandName)
        {

            case "SET":

                try

                {

                    PnlFront.Visible = false;
                    pnladd.Visible = true;
                    Pnldetail.Visible = true;
                    pnlHeader.Visible = false;
                    blnadd = 1;

                    txtnobukti.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnobukti")).Text;

                    txttgltrn.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltglTrn")).Text;
                    txttgltrn.Enabled = false;
                    txttransAP.Enabled = false;
                    txttransAP.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblddlpembayaran")).Text));
                    txtlembartotal.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljumlahkertas")).Text));
                    txtlogamtotal.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbljumlahlogam")).Text));
                    txtlembar1.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas1")).Text));
                    txtlembartotal1.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas1")).Text));
                    txtlembar2.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas2")).Text));
                    txtlembartotal2.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas2")).Text));
                    txtlembar3.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas3")).Text));
                    txtlembartotal3.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas3")).Text));
                    txtlembar4.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas4")).Text));
                    txtlembartotal4.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas4")).Text));
                    txtlembar5.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas5")).Text));
                    txtlembartotal5.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas5")).Text));
                    txtlembar6.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas6")).Text));
                    txtlembartotal6.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas6")).Text));
                    txtlembar7.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas7")).Text));
                    txtlembartotal7.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas7")).Text));
                    txtlembar8.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas8")).Text));
                    txtlembartotal8.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas8")).Text));
                    txtlembar9.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkertas9")).Text));
                    txtlembartotal9.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotalkertas9")).Text));
                    txtlogam1.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam1")).Text));
                    txtlogamtotal1.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam1")).Text));
                    txtlogam2.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam2")).Text));
                    txtlogamtotal2.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam2")).Text));
                    txtlogam3.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam3")).Text));
                    txtlogamtotal3.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam3")).Text));
                    txtlogam4.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam4")).Text));
                    txtlogamtotal4.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam4")).Text));
                    txtlogam5.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam5")).Text));
                    txtlogamtotal5.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam5")).Text));
                    txtlogam6.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam6")).Text));
                    txtlogamtotal6.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam6")).Text));
                    txtlogam7.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam7")).Text));
                    txtlogamtotal7.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam7")).Text));
                    txtlogam8.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam8")).Text));
                    txtlogamtotal8.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam8")).Text));
                    txtlogam9.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbllogam9")).Text));
                    txtlogamtotal9.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbltotallogam9")).Text));
                    ddladjust.SelectedValue = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblkodeAdjust")).Text;
                    txtnilaiadjust.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblNilaiAdjust")).Text));
                    txtsaldoakhiradjust.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblSaldoAkhir")).Text));
                    txtsaldoakhirsystemadjust.Text = string.Format("{0:N2}", decimal.Parse(((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblSaldoAkhir")).Text));
                    ddladjust.Enabled = false;
                    txtnilaiadjust.Enabled = false;
                    txtsaldoakhiradjust.Enabled = false;
                    txtKeterangan.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblKeterangan")).Text;
                    txtKeterangan.Enabled = false;
                    txtBankGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblbankgiro")).Text;
                    txtnoGiro.Text = ((Label)GRData.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblnogiro")).Text;
                    Pnlfooter4.Visible = true;
                }

                catch (Exception X)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('" + X.Message.ToString() + "');", true);
                    gb.SaveErroLog(X.Message.ToString(), Path.GetFileName(Request.Path), SessLoginID, Server.MapPath("~/ErrorLog/" + "ErrorLog" + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy") + ".xml"));

                }

                break;
            

        }
    }
    protected void btndelete_click(object sender, EventArgs e)
    {
        try
        {


            decimal jumlahAP = decimal.Parse(txttransAP.Text);



            if (jumlahAP <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
            }

            else
            {



                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[13];

                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[2] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[3] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[6] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

                Params[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                Params[9] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[10] = gb.CreateSPParameter("@bankgiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtBankGiro.Text, typeof(string));
                Params[11] = gb.CreateSPParameter("@nogiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoGiro.Text, typeof(string));
                Params[12] = gb.CreateSPParameter("@Nobukti", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnobukti.Text, typeof(string));
                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Delete_Transfer_Head_Teller", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);

                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);

                }

                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    btnCancel.Visible = false;
                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Delete Done!','zzzzzz','bottom-center','success','1');", true);

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
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        PnlFront.Visible = true;
        pnladd.Visible = false;
        Pnldetail.Visible = false;
        pnlHeader.Visible = true;
        Pnlfooter4.Visible = false;

    }
    protected string btnreload()
    {
        return Page.ClientScript.GetPostBackEventReference(btnCancel, string.Empty, true);
    }
    protected void btnSimpan_Click(object sender, EventArgs e)
    {
        try
        {


            decimal jumlahAP = decimal.Parse(txtsaldoakhiradjust.Text);
            decimal jumlahRc = decimal.Parse(txtlembartotal.Text) + decimal.Parse(txtlogamtotal.Text);

            //btnSimpan.Enabled = false;

            if (jumlahAP <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi Masih Kosong');", true);
                //btnSimpan.Enabled = true;
            }
            else if (jumlahAP != jumlahRc)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Jumlah Transaksi tidak sama dengan jumlah recon');", true);
                //btnSimpan.Enabled = true;
            }

            else if (txtKeterangan.Text.Length > 100)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Keterangan tidak boleh lebih dari 100');", true);
                //btnSimpan.Enabled = true;
                txtKeterangan.Focus();
            }

            else
            {



                DefaultClass.Global.SqlHelper[] Params = new DefaultClass.Global.SqlHelper[53];

                Params[0] = gb.CreateSPParameter("@KodeKonter", ParameterDirection.Input, SqlDbType.VarChar, 50, KodeKonter, typeof(string));
                Params[1] = gb.CreateSPParameter("@Note", ParameterDirection.Input, SqlDbType.VarChar, 200, txtKeterangan.Text, typeof(string));
                Params[2] = gb.CreateSPParameter("@tgltrn", ParameterDirection.Input, SqlDbType.SmallDateTime, 0, txttgltrn.Text, typeof(DateTime));
                Params[3] = gb.CreateSPParameter("@jumlahtrn", ParameterDirection.Input, SqlDbType.Decimal, 0, txttransAP.Text, typeof(decimal));
                Params[4] = gb.CreateSPParameter("@Kodesumber", ParameterDirection.Input, SqlDbType.VarChar, 20, kodesumber, typeof(string));
                Params[5] = gb.CreateSPParameter("@userlogin", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(string));
                Params[6] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(string));
                Params[7] = gb.CreateSPParameter("@Kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(string));

                Params[8] = gb.CreateSPParameter("@ErrMsg", ParameterDirection.Output, SqlDbType.VarChar, 8000, ParameterDirection.Output, typeof(string));

                Params[9] = gb.CreateSPParameter("@Form", ParameterDirection.Input, SqlDbType.VarChar, 10, form, typeof(string));
                Params[10] = gb.CreateSPParameter("@bankgiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtBankGiro.Text, typeof(string));
                Params[11] = gb.CreateSPParameter("@nogiro", ParameterDirection.Input, SqlDbType.VarChar, 30, txtnoGiro.Text, typeof(string));

                Params[12] = gb.CreateSPParameter("@kertas1", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar1.Text), typeof(int));
                Params[13] = gb.CreateSPParameter("@totalkertas1", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal1.Text, typeof(Decimal));
                Params[14] = gb.CreateSPParameter("@kertas2", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar2.Text), typeof(int));
                Params[15] = gb.CreateSPParameter("@totalkertas2", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal2.Text, typeof(Decimal));
                Params[16] = gb.CreateSPParameter("@kertas3", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar3.Text), typeof(int));
                Params[17] = gb.CreateSPParameter("@totalkertas3", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal3.Text, typeof(Decimal));
                Params[18] = gb.CreateSPParameter("@kertas4", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar4.Text), typeof(int));
                Params[19] = gb.CreateSPParameter("@totalkertas4", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal4.Text, typeof(Decimal));
                Params[20] = gb.CreateSPParameter("@kertas5", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar5.Text), typeof(int));
                Params[21] = gb.CreateSPParameter("@totalkertas5", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal5.Text, typeof(Decimal));
                Params[22] = gb.CreateSPParameter("@kertas6", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar6.Text), typeof(int));
                Params[23] = gb.CreateSPParameter("@totalkertas6", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal6.Text, typeof(Decimal));
                Params[24] = gb.CreateSPParameter("@kertas7", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar7.Text), typeof(int));
                Params[25] = gb.CreateSPParameter("@totalkertas7", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal7.Text, typeof(Decimal));
                Params[26] = gb.CreateSPParameter("@kertas8", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar8.Text), typeof(int));
                Params[27] = gb.CreateSPParameter("@totalkertas8", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal8.Text, typeof(Decimal));
                Params[28] = gb.CreateSPParameter("@kertas9", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlembar9.Text), typeof(int));
                Params[29] = gb.CreateSPParameter("@totalkertas9", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal9.Text, typeof(Decimal));
                Params[30] = gb.CreateSPParameter("@totalkertas", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlembartotal.Text, typeof(Decimal));
                Params[31] = gb.CreateSPParameter("@logam1", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam1.Text), typeof(int));
                Params[32] = gb.CreateSPParameter("@totallogam1", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal1.Text, typeof(Decimal));
                Params[33] = gb.CreateSPParameter("@logam2", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam2.Text), typeof(int));
                Params[34] = gb.CreateSPParameter("@totallogam2", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal2.Text, typeof(Decimal));
                Params[35] = gb.CreateSPParameter("@logam3", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam3.Text), typeof(int));
                Params[36] = gb.CreateSPParameter("@totallogam3", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal3.Text, typeof(Decimal));
                Params[37] = gb.CreateSPParameter("@logam4", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam4.Text), typeof(int));
                Params[38] = gb.CreateSPParameter("@totallogam4", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal4.Text, typeof(Decimal));
                Params[39] = gb.CreateSPParameter("@logam5", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam5.Text), typeof(int));
                Params[40] = gb.CreateSPParameter("@totallogam5", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal5.Text, typeof(Decimal));
                Params[41] = gb.CreateSPParameter("@logam6", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam6.Text), typeof(int));
                Params[42] = gb.CreateSPParameter("@totallogam6", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal6.Text, typeof(Decimal));
                Params[43] = gb.CreateSPParameter("@logam7", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam7.Text), typeof(int));
                Params[44] = gb.CreateSPParameter("@totallogam7", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal7.Text, typeof(Decimal));
                Params[45] = gb.CreateSPParameter("@logam8", ParameterDirection.Input, SqlDbType.Int, 0, int.Parse(txtlogam8.Text), typeof(int));
                Params[46] = gb.CreateSPParameter("@totallogam8", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal8.Text, typeof(Decimal));
                Params[47] = gb.CreateSPParameter("@logam9", ParameterDirection.Input, SqlDbType.Decimal, 0, int.Parse(txtlogam9.Text), typeof(Decimal));
                Params[48] = gb.CreateSPParameter("@totallogam9", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal9.Text, typeof(Decimal));
                Params[49] = gb.CreateSPParameter("@totallogam", ParameterDirection.Input, SqlDbType.Decimal, 0, txtlogamtotal.Text, typeof(Decimal));
                Params[50] = gb.CreateSPParameter("@NilaiAdjust", ParameterDirection.Input, SqlDbType.Decimal, 0, txtnilaiadjust.Text, typeof(Decimal));
                Params[51] = gb.CreateSPParameter("@SaldoAkhir", ParameterDirection.Input, SqlDbType.Decimal, 0, txtsaldoakhiradjust.Text, typeof(Decimal));
                Params[52] = gb.CreateSPParameter("@kodeAdjust", ParameterDirection.Input, SqlDbType.VarChar, 1, ddladjust.SelectedValue, typeof(string));

                ge.ListDataset = gb.GetDataJustOutPut("CL_Trn_CL_Insert_Transfer_Head_Teller", Params, "constrmf");
                if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Closing"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Tersebut Sudah Diclosing!');", true);
                    //btnSimpan.Enabled = false;
                }
                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Selisih"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Transaksi Debit dan Kredit Selisih!');", true);
                    //btnSimpan.Enabled = false;
                }

                else if (ge.ListDataset.Tables[0].Rows[0][0].ToString().Contains("Success"))
                {
                    //btnSimpan.Visible = false;

                    nobuktidb = ge.ListDataset.Tables[0].Rows[0][1].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Save Done!','zzzzzz','bottom-center','success','1');", true);

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


    protected void txtdate_TextChanged(object sender, System.EventArgs e)
    {
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[5];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@State", ParameterDirection.Input, SqlDbType.VarChar, 40, "CHECKTGL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        varT[4] = gb.CreateSPParameter("@kodeuser", ParameterDirection.Input, SqlDbType.VarChar, 30, SessLoginID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckCabang", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SweetAlert('Date diluar fiskal Bulan ini!!');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
            ddlsaldokashead(sender, e);
        }
        else
        {
            ddlsaldokashead(sender, e);
        }
    }

    protected void ddlsp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DateTime now = DateTime.Now;




        //ddlSP.SelectedValue = SessFiskalBi;
        string nowx;
        nowx = gb.ReadOrderData();
        DateTime now = DateTime.Parse(nowx);
        int tahun;
        tahun = now.Year;
        DateTime date = new DateTime(Int32.Parse(ddlyear.SelectedValue), Int32.Parse(ddlSP.SelectedValue), 1);
        txtStart.Text = date.ToString("yyyy-MM-dd");
        DateTime xdate = new DateTime(Int32.Parse(ddlyear.SelectedValue), Int32.Parse(ddlSP.SelectedValue), 1);
        xdate = date.AddMonths(1);
        xdate = xdate.AddDays(-1);
        txtEnd.Text = xdate.ToString("yyyy-MM-dd");
        //Txtrptperiode.Text = "Periode : " + date.ToString("dd-MM-yyyy") + " s/d " + xdate.ToString("dd-MM-yyyy");

        //Txtrptperiode.Enabled = false;
        ddlSP.Enabled = true;
        BindGridEntity();

    }


    protected void GetKurs_textchanged(object sender, EventArgs e)
    {

        //get kurs SUPLLIER for dropdown and textbox
        DefaultClass.Global.SqlHelper[] varT = new DefaultClass.Global.SqlHelper[4];
        varT[0] = gb.CreateSPParameter("@Anydate", ParameterDirection.Input, SqlDbType.SmallDateTime, 40, txttgltrn.Text, typeof(DateTime));
        varT[1] = gb.CreateSPParameter("@module", ParameterDirection.Input, SqlDbType.VarChar, 40, "GL", typeof(string));
        varT[2] = gb.CreateSPParameter("@kdpersh", ParameterDirection.Input, SqlDbType.VarChar, 10, SessKdpersh, typeof(String));
        varT[3] = gb.CreateSPParameter("@KodeCabang", ParameterDirection.Input, SqlDbType.VarChar, 10, SessBranchID, typeof(String));
        ge.ListDataset = gb.GetDataJustOutPut("MccCheckTglTrn", varT, "constrmf");

        string xstatus = ge.ListDataset.Tables[0].Rows[0]["status"].ToString();
        DateTime xtanggal = DateTime.Parse(ge.ListDataset.Tables[0].Rows[0]["tanggal"].ToString());
        if (xstatus == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Alert('Date diluar fiskal Bulan ini!','Cek tanggal','top-right','success','0');", true);
            txttgltrn.Text = xtanggal.ToString("dd-MM-yyyy");
            //txttgljthtmp.Text = txttgltrn.Text;
        }
        //txttgljthtmp.Text = txttgltrn.Text;
    }

    

    protected void txtjlhbayar_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        decimal bayar, jumlahtrn;
        bayar = decimal.Parse(((TextBox)grow.FindControl("jlhdibayar")).Text);
        jumlahtrn = decimal.Parse(((Label)grow.FindControl("lbljmlhtrn")).Text);
        if (bayar > 0)
        {

            if (bayar > jumlahtrn)
            {
                ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lbljmlhtrn")).Text;
            }
            //subgridAP_Click();

        }
        else
        {

            ((TextBox)grow.FindControl("jlhdibayar")).Text = "0";
            //subgridAP_Click();
            //((TextBox)grow.FindControl("jlhdebit")).Enabled = true;
            //((TextBox)grow.FindControl("jlhkredit")).Enabled = false;
        }
        //GRTP.UseAccessibleHeader = true;
        //GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void Check_Clicked(object sender, EventArgs e)
    {
        if (blnadd == 0)
        {


            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            if (((CheckBox)grow.FindControl("CheckBox1")).Checked)
            {
                ((TextBox)grow.FindControl("jlhdibayar")).Text = ((Label)grow.FindControl("lbljmlhtrn")).Text;
                ((TextBox)grow.FindControl("jlhdibayar")).Enabled = true;
            }
            else
            {
                ((TextBox)grow.FindControl("jlhdibayar")).Text = "0";
                ((TextBox)grow.FindControl("jlhdibayar")).Enabled = false;
            }

            //subgridAP_Click();
            //GRTP.UseAccessibleHeader = true;
            //GRTP.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }


    protected void GetddlCabang()
    {
        gey.ListDataset = gby.GetDataFunction("dbo.GetCabang()", "constrmf");

        gey.ListDataset.Tables[0].DefaultView.Sort = "cabang_nama asc";

        ddlcabang.DataSource = gey.ListDataset.Tables[0].DefaultView.ToTable();
        ddlcabang.DataTextField = "cabang_nama";
        ddlcabang.DataValueField = "kode_cabang";
        ddlcabang.DataBind();
        ddlcabang.Items.Insert(0, new ListItem("ALL CABANG", ""));
    }
}
