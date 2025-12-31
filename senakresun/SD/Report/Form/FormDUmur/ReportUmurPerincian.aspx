<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportUmurPerincian.aspx.cs" Inherits="SD_Report_Umur_Perincian" EnableEventValidation="false"%>
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
                                    <li class="breadcrumb-item"><a href="#">Sumber Dana</a></li>
                                    <li class="breadcrumb-item"><a href="#">Report</a></li>
                                    <li class="breadcrumb-item"><a href="Branch.aspx">Report Umur Hutang</a></li>
                                </ol>
                            </div>
                            <%--<h4 class="page-title">Report</h4>--%>
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
                                                  <asp:ListItem Value="01">Perincian Umur Hutang Yang akan jatuh Tempo</asp:ListItem>
                                                  <asp:ListItem Value="02">Perincian Umur Hutang Yang lewat jatuh Tempo</asp:ListItem>
                                                  <asp:ListItem Value="03">Rekap Umur Hutang Yang akan jatuh Tempo</asp:ListItem>
                                                  <asp:ListItem Value="04">Rekap Umur Hutang Yang lewat jatuh Tempo</asp:ListItem>
                                          </asp:DropDownList> 
                                      <%--<label for="inputCity" class="col-form-label font-weight-bold">Choice Category Report</label>
                                      <asp:radiobuttonlist id="rdbtnGender" runat="server" repeatdirection="Horizontal"  AutoPostBack="true" CellPadding="10" CellSpacing="4">
                                         <asp:listitem  Text="&nbsp; Perincian Umur Hutang Yang akan jatuh Tempo" Value="1" />
                                         <asp:listitem  Text="&nbsp; Perincian Umur Hutang Yang lewat jatuh Tempo" Value="2"/>
                                         <asp:listitem  Text="&nbsp; Rekap Umur Hutang Yang akan jatuh Tempo" Value="3" />
                                         <asp:listitem  Text="&nbsp; Rekap Umur Hutang Yang lewat jatuh Tempo" Value="4"/>
                                      </asp:radiobuttonlist>--%>
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
                                                <label for="inputState" class="col-md-3 col-form-label font-weight-bold">Tgl Umur Hutang</label>     
                                            <div class="col-4">
                                                 <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date" AutoPostBack="true" OnTextChanged="txtStart_Onchange"></asp:TextBox>     
                                            </div>
                                        </div>
                                    </div>  
                                    <div class="form-group row">
                                        <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-md-3 col-form-label font-weight-bold">Batas Interval</label>     
                                            <div class="col-2">
                                                 <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" Text="30" Enabled="false"></asp:TextBox>    
                                            </div>     
                                            <div class="col-1">
                                                 <label for="inputState" class="col-md-3 col-form-label">Hari</label>     
                                            </div>
                                        </div>
                                    </div>     
                                     <div class="form-group row">
                                        <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-3 col-form-label font-weight-bold">Kode Group</label>     
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddlgroupawal" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <label for="inputState" class="col-1 col-form-label">s/d</label>     
                                                </div>
                                                <div class="col-4">
                                                    <asp:DropDownList ID="ddlgroupakhir" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
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
                                     </div>--%>
                                     <div class="form-group row">
                                             <div class="form-check-inline col-md-12">
                                                     <label for="inputState" class="col-3 col-form-label font-weight-bold">Kode Sumber Dana</label>     
                                                <div class="col-4">
                                                    <asp:DropDownList ID="ddlsupplierawal" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <label for="inputState" class="col-1 col-form-label">s/d</label>     
                                                </div>
                                                <div class="col-4">
                                                    <asp:DropDownList ID="ddlsupplierakhir" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                                     <div class="form-group row">
                                         <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-3 col-form-label font-weight-bold">Kode Currency</label>     
                                            <div class="col-4">
                                                <asp:DropDownList ID="ddlCrc" CssClass="col-11 col-form-label" required="required" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>
                               <%--      <div class="form-group row">
                                         <div class="form-check-inline col-md-12">
                                                <label for="inputState" class="col-3 col-form-label font-weight-bold">Pilihan Laporan</label>     
                                            <div class="col-4">
                                                
                                                <asp:DropDownList ID="ddlpilihanlaporan" runat="server" AutoPostBack="true" CssClass="col-11 col-form-label" data-style="btn-custom">
                                                        <asp:ListItem Value="-">Pilih...</asp:ListItem>
                                                        <asp:ListItem Value="A">A. Sortir Laporan Menurut Kode Wilayah</asp:ListItem>
                                                        <asp:ListItem Value="B">B. Sortir Laporan Menurut Kode Group</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                     </div>--%>
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
                                    <%--<div class="form-group row">
                                         <div class="form-check-inline col-md-12">
                                            <label for="inputState" class="col-3 col-form-label font-weight-bold">Komentar</label>
                                             <div class="col-9">
                                                <asp:TextBox ID="TextBox2" CssClass="col-11 col-form-label" runat="server" TextMode="MultiLine" Rows="5" data-style="btn-custom" MaxLength="150" Enabled="false"
                                                    Text=""></asp:TextBox>
                                             </div>
                                         </div>
                                    </div>--%>
                                 <div class="form-group">
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










