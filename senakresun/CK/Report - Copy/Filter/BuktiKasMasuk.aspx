<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuktiKasMasuk.aspx.cs" Inherits="CL_Report_Filter_BuktiKasMasuk" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
         <title>Report Daftar Keluar Masuk Kas</title>
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
                                    <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Report</a></li>
                                    <li class="breadcrumb-item"><a href="Branch.aspx">Report Bukti Masuk & Masuk</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Filter Data Report Bukti Masuk & Keluar</h4>
                        </div>
                    </div>
                </div>


            <asp:Panel ID="pnlFilter" Visible="true" runat="server">                                              
                                
                  <div class="row">
                      
                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter Report</div>
                                    <p class="text-muted font-14 m-b-30"></p>    
                                    <div class="form-row">
                                      <div class="form-group col-md-12">  
                                                    <label class="col-form-label">Bukti Kas</label> 
                                                    <asp:DropDownList ID="ddljeniskas" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400"  OnTextChanged ="JenisKas_SelectedIndexChanged">
                                                        <asp:ListItem Text="Silahkan Pilih" Selected="true" />
                                                        <asp:ListItem Text="Bukti Kas Bank Terima Pembayaran Piutang" Value="1" />
                                                        <asp:ListItem Text="Bukti Kas Bank Keluar Pembayaran Hutang" Value="2"/>  
                                                    </asp:DropDownList> 
                                          </div>
                                    <div class="form-row col-md-12">
                                        <div class="form-group col-md-2"> 
                                            <label for="inputCity" class="col-form-label" width="300" >Nobukti</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <asp:DropDownList ID="ddlnobuktiAwal" runat="server"  required="required" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" ></asp:DropDownList>        
                                        </div>
                                        <div class="form-group col-md-0">
                                            <label for="inputCity" class="col-form-label">S/d</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <asp:DropDownList ID="ddlnobuktiAkhir" runat="server" required="required" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" ></asp:DropDownList> 
                                        </div> 
                                     </div>  
                                     <div class="form-group col-md-6">
                                        <div>
                                            <label for="inputState" class="col-form-label">Kode Currency</label>
                                            <asp:DropDownList ID="ddlkdcrc" runat="server"  required="required" AutoPostBack="true" width ="400"  CssClass="form-control" data-style="btn-custom" ></asp:DropDownList>  
                                        </div>
                                        <div>
                                            <label for="inputZip" class="col-form-label">Disetor Oleh</label>
                                            <asp:TextBox ID="txtsetor" CssClass="form-control" runat="server"></asp:TextBox>  
                                        </div>
                                        <div>
                                             <label for="inputCity" class="col-form-label">Kassa</label> 
                                            <asp:TextBox ID="txtkassa" CssClass="form-control" runat="server" required="required"></asp:TextBox>  
                                        </div>
                                        <div>
                                                <label for="inputCity" class="col-form-label">Bagian Pembukuan</label>
                                                  <asp:TextBox ID="txtbuku" CssClass="form-control" required="required" runat="server"></asp:TextBox>
                                        </div> 
                                        <div>
                                                <label for="inputCity" class="col-form-label">Disetujui Oleh</label>
                                                  <asp:TextBox ID="txtsetuju" CssClass="form-control" required="required" runat="server"></asp:TextBox>
                                        </div> 
                                        <div>
                                                <label for="inputCity" class="col-form-label">Mengetahui</label>
                                                  <asp:TextBox ID="txtketahui" CssClass="form-control" required="required" runat="server"></asp:TextBox>
                                        </div> 
                                      </div> 
                                        </div>
                                </div>                                                    
                            </div>
                               </div>
                            <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Setting</div>
                                        <p class="text-muted font-14 m-b-30"></p>                  
                                            <div class="form-row">
                                                
                                            <div class="form-group col-md-6">
                                                <div class="form-group col-md-8">
                                                <label for="inputCity" class="col-form-label">Setting Report</label>
                                                    <asp:DropDownList ID="DdlReport" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">View Report</asp:ListItem>
                                                        <asp:ListItem Value="2">Export Word</asp:ListItem>
                                                        <asp:ListItem Value="3">Export PDF</asp:ListItem>
                                                        <asp:ListItem Value="4">Export Excel</asp:ListItem>
                                                        <asp:ListItem Value="5">Export CSV</asp:ListItem>
                                                    </asp:DropDownList> 
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>     
                    </div>
            </asp:Panel>
                    

                    <asp:Panel ID="Panel1" Visible="true" runat="server">
               
                        <div class="form-group">
                        <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Process" OnClick="btnSearch_Click"/>  
                        </div>

                    </asp:Panel>



            </div> <!-- end container -->
      
   <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                 <asp:PostBackTrigger ControlID="btnSearch" />
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

   
    <script src="../../../plugins/sweet-alert/sweetalert2.min.js"></script>
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










