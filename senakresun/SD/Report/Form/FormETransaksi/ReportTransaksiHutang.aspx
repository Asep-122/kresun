<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportTransaksiHutang.aspx.cs" Inherits="SD_Report_Transaksi_Hutang" EnableEventValidation="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
         <title>Report Daftar Assets</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
        <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
        <meta content="Coderthemes" name="author" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />

        <!-- App css -->
        <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
        <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />

        <script src="/assets/js/modernizr.min.js"></script>
    
        <style type="text/css">
        .form-control {
        }
    </style>
    </head>
     <body>
          <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container-fluid">

                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Sumber Dana Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Report</a></li>
                                    <li class="breadcrumb-item"><a href="Branch.aspx">Report Transaksi Hutang</a></li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            <%--Category Panel--%>
                    <asp:Panel ID="pnlCategory" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-lg-7">
                            <div class="card-box ribbon-box">
                              <div class="ribbon ribbon-custom">Category Report :</div>
                                <p class="m-b-0">                
                                 <div class="form-group row">
                                     <div class="form-group col-md-12">
                                         <label for="inputCity" class="col-form-label font-weight-bold" >Choice Category Report</label>
                                          <asp:DropDownList ID="ddljenisreport" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddljenis_SelectedIndexChanged">
                                                  <asp:ListItem Value="-">Pilih Salah Satu Category Report....</asp:ListItem>
                                                  <asp:ListItem Value="A">Transaksi Hutang Berdasarkan Transaksi Saldo Awal</asp:ListItem>
                                                  <asp:ListItem Value="C">Transaksi Hutang Berdasarkan Transaksi Beli Kredit</asp:ListItem>
                                                  <asp:ListItem Value="D">Transaksi Hutang Berdasarkan Transaksi Nota Kredit</asp:ListItem>
                                                  <asp:ListItem Value="E">Transaksi Hutang Berdasarkan Transaksi Pinalty Keterlambatan</asp:ListItem>
                                                  <asp:ListItem Value="F">Transaksi Hutang Berdasarkan Transaksi PPN Masukan</asp:ListItem>
                                                  <asp:ListItem Value="H">Transaksi Hutang Berdasarkan Transaksi Ongkos Angkut</asp:ListItem>
                                                  <asp:ListItem Value="J">Transaksi Hutang Berdasarkan Transaksi Adjustment</asp:ListItem>
                                                  <asp:ListItem Value="O">Transaksi Hutang Berdasarkan Transaksi Nota Debit</asp:ListItem>
                                                  <asp:ListItem Value="P">Transaksi Hutang Berdasarkan Transaksi Discount</asp:ListItem>
                                                  <asp:ListItem Value="Q">Transaksi Hutang Berdasarkan Transaksi Retur Pembelian</asp:ListItem>
                                                  <asp:ListItem Value="S">Transaksi Hutang Berdasarkan Transaksi Retur PPN Masukan</asp:ListItem>
                                                  <asp:ListItem Value="All">Transaksi Hutang Berdasarkan Semua Type Transaksi</asp:ListItem>
                                                 <%-- -<asp:ListItem Value="13">Transaksi Hutang Berdasarkan Semua Transaksi Pembelian Dari Purchasing</asp:ListItem>
                                                  <asp:ListItem Value="14">Transaksi Hutang Berdasarkan Semua Transaksi Retur Dari Purchasing</asp:ListItem>
                                                  <asp:ListItem Value="M">Transaksi Hutang Berdasarkan Semua Transaksi Pembayaran Dari Cheque (Cek) Control</asp:ListItem>
                                                  <asp:ListItem Value="K">Transaksi Hutang Berdasarkan Semua Transaksi Yang Sudah Lunas</asp:ListItem>--%>
                                                  <%--<asp:ListItem Value="17">Transaksi Hutang Berdasarkan Semua Transaksi Yang Belum Lunas</asp:ListItem> SAMA DENGAN BELI KREDIT--%>
                                                  <asp:ListItem Value="U">Transaksi Hutang Berdasarkan Transaksi Mutasi Uang Muka</asp:ListItem>
                                                  <asp:ListItem Value="B">Transaksi Hutang Berdasarkan Transaksi Beli Cash</asp:ListItem>
                                          </asp:DropDownList>
                                     </div>
                                 </div>
                            </div> 
                        </div> 
                    </div>

                 </asp:Panel>
            <%--Data Filter--%>
            <asp:Panel ID="pnlFilter" Visible="false" runat="server">                                              
                                
                  <div class="row">
                           <div class="col-lg-7">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter Data</div>
                                    <p class="text-muted font-14 m-b-30"></p>   
                                    <div class="form-group row">
                                        <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-md-3 col-form-label font-weight-bold">Tanggal Faktur</label>     
                                            <div class="col-4">
                                                 <asp:TextBox ID="txtStartFaktur" CssClass="form-control" runat="server"  Type="date" AutoPostBack="true"></asp:TextBox>     
                                            </div>
                                            <div class="col-1">
                                                <label for="inputState" class="col-1 col-form-label">s/d</label>     
                                            </div>   
                                            <div class="col-4">
                                                 <asp:TextBox ID="txtEndFaktur" CssClass="form-control" runat="server"  Type="date" AutoPostBack="true"></asp:TextBox>     
                                            </div>
                                        </div>
                                    </div>   
                                    <div class="form-group row">
                                        <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-md-3 col-form-label font-weight-bold">Tanggal Transaksi</label>     
                                            <div class="col-4">
                                                 <asp:TextBox ID="txtStartTransaksi" CssClass="form-control" runat="server"  Type="date" AutoPostBack="true" OnTextChanged="txtStart_Onchange"></asp:TextBox>     
                                            </div>
                                            <div class="col-1">
                                                <label for="inputState" class="col-1 col-form-label">s/d</label>     
                                            </div>   
                                            <div class="col-4">
                                                 <asp:TextBox ID="txtEndTransaksi" CssClass="form-control" runat="server"  Type="date" AutoPostBack="true" OnTextChanged="txtEnd_Onchange"></asp:TextBox>     
                                            </div>
                                        </div>
                                    </div>     
                                     <%--<div class="form-group row">
                                        <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-3 col-form-label font-weight-bold">Kode Wilayah</label>     
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddlwilayahawal" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-1">
                                                <label for="inputState" class="col-1 col-form-label">s/d</label>     
                                            </div>
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddlwilayahakhir" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>   --%>
                                     <div class="form-group row">
                                        <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-3 col-form-label font-weight-bold">Kode Sumber Dana</label>     
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddllanggananawal" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="col-1">
                                                <label for="inputState" class="col-1 col-form-label">s/d</label>     
                                            </div>
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddllanggananakhir" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                     <div class="form-group row">
                                             <div class="form-check-inline col-md-12">
                                                     <label for="inputState" class="col-3 col-form-label font-weight-bold">Kode Pembeli</label>     
                                                <div class="col-4">
                                                    <asp:DropDownList ID="ddlpembeliawal" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <label for="inputState" class="col-1 col-form-label">s/d</label>     
                                                </div>
                                                <div class="col-4">
                                                    <asp:DropDownList ID="ddlpembeliakhir" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                     <div class="form-group row">
                                         <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-3 col-form-label font-weight-bold">Type Transaksi</label>     
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddlTypeTransaksi" CssClass="col-11 col-form-label" required="required" runat="server" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                     <div class="form-group row">
                                         <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-3 col-form-label font-weight-bold">Kode Currency</label>     
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddlcrc" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="form-group row">
                                         <div class="form-check-inline col-md-12">
                                            <label for="inputState" class="col-3 col-form-label font-weight-bold">Keterangan Laporan</label>
                                             <div class="col-9">
                                                <asp:TextBox ID="txtrpttitle" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                             </div>
                                         </div>
                                    </div>
                                    <div class="form-group row">
                                         <div class="form-check-inline col-md-12">
                                            <label for="inputState" class="col-3 col-form-label font-weight-bold">Keterangan Periode</label>
                                             <div class="col-9">
                                                <asp:TextBox ID="txtperiode" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                             </div>
                                         </div>
                                    </div>
                                 <div class="form-group pull-right">
                                    <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white right" runat="server" Text="Process" OnClick="btnSerach_Click"/>  
                                 </div>
                            </div>
                          </div>
                       </div>              
                    </div>
            </asp:Panel>
                    

                    <asp:Panel ID="Panel1" Visible="true" runat="server">
               
                        

                    </asp:Panel>



            </div> <!-- end container -->
      
   <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="btnSearch" />
                 <asp:PostBackTrigger ControlID="ddljenisreport" />
                <%-- <asp:PostBackTrigger ControlID="txtgolongan" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>

        <script src="/assets/js/jquery.min.js"></script>
        <script src="/assets/js/popper.min.js"></script>
        <script src="/assets/js/bootstrap.min.js"></script>
        <script src="/assets/js/waves.js"></script>
        <script src="/assets/js/jquery.slimscroll.js"></script>

        <!-- App js -->
        <script src="/assets/js/jquery.core.js"></script>
        <script src="/assets/js/jquery.app.js"></script>

   
    <script src="/plugins/sweet-alert/sweetalert2.min.js"></script>
    <%--<script type="text/javascript">
          var sessionTimeoutWarning = "System.Configuration.ConfigurationSettings.AppSettings["SessionWarning"].ToString() ";
        var sessionTimeout = "Session.Timeout";

        var sTimeout = parseInt(sessionTimeoutWarning) * 60 * 1000;
        setTimeout('SessionWarning()', sTimeout);

        function SessionWarning()
        {
            var message = "Your session will expire in another " +
                (parseInt(sessionTimeout) - parseInt(sessionTimeoutWarning)) +
                " mins! Please Save the data before the session expires";
            alert(message);
        }
        </script>--%>
      <script type="text/javascript">

      //  $(document).ready(InIEvent);
        //function InIEvent() {

          $(document).ready(function() {

                // Default Datatable
                $('#datatable').DataTable();

                //Buttons examples
                var table = $('#GRData').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
                });
                //var table = $('#GRData').DataTable({
                //    lengthChange: false,
                //    buttons: ['copy', 'excel', 'pdf']
                //});
                // Key Tables

                $('#key-table').DataTable({
                    keys: true
                });

                // Responsive Datatable
                $('#responsive-datatable').DataTable();

                // Multi Selection Datatable
                $('#selection-datatable').DataTable({
                    select: {
                        style: 'multi'
                    }
                });
                   table.buttons().container()
                    .appendTo('#GRData_wrapper .col-md-6:eq(0)');
     
            } );
        </script>

</html>










