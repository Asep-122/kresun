<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportDaftarStatementCustomer.aspx.cs" Inherits="KP_Report_ReportDaftarStatementCustomer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
         <title>Report Statement Customer</title>
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
                                    <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Report</a></li>
                                    <li class="breadcrumb-item"><a href="Branch.aspx">Report Statement Customer</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Report Statement Customer</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="true" runat="server">                                              
                                
                  <div class="row">

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Periode</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                        <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Periode</label>
                                          <asp:DropDownList ID="ddlSP" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">Januari</asp:ListItem>
                                                        <asp:ListItem Value="2">Februari</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>        
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">This Month From</label>
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server" Type="date"></asp:TextBox>     
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">This Month To</label>
                                        <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>  
                                    </div>
                                </div>
                                        <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Customer Code from</label>
                                          <asp:DropDownList ID="ddlaccountawal" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Customer Code to</label>
                                         <asp:DropDownList ID="ddlaccountakhir" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div>
                                   
                                </div>
                                    <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Product</label>
                                            <asp:DropDownList ID="ddlproduct" runat="server"  CssClass="form-control" data-style="btn-custom" >
                                            </asp:DropDownList>                                                     
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
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Setting Report</label>
                                                     <asp:DropDownList ID="DdlReport" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">View Report</asp:ListItem>
                                                        <asp:ListItem Value="2">Export Word</asp:ListItem>
                                                        <asp:ListItem Value="3">Export PDF</asp:ListItem>
                                                        <asp:ListItem Value="4">Export Excel</asp:ListItem>
                                                        <asp:ListItem Value="5">Export CSV</asp:ListItem>
                                                </asp:DropDownList> 
                                    </div>
                                    <div class="form-group col-md-8">
                                        <label for="inputState" class="col-form-label">Report Title</label>
                                           <asp:TextBox ID="txtrpttitle" CssClass="form-control" runat="server" ></asp:TextBox> 
                                    </div>
                                   
                                </div>
                                <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Report Periode</label>
                                            <asp:TextBox ID="Txtrptperiode" CssClass="form-control" runat="server"></asp:TextBox>                                                       
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">Date Kurs</label>
                                        <asp:TextBox ID="DateKurs" CssClass="form-control" runat="server"  Type="date"></asp:TextBox> 
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="inputCity" class="col-form-label">Currency Code</label>
                                          <asp:DropDownList ID="ddlCurrency" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="inputState" class="col-form-label">Kurs</label>
                                        <asp:TextBox ID="Txtkurs" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>

                                </div>                                
                                </div>                                                    
                            </div>
                          </div>              
                    </div>
            </asp:Panel>
                    

                    <asp:Panel ID="Panel1" Visible="true" runat="server">
               
                        <div class="form-group">
                        <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Process" OnClick="btnSerach_Click"/>  
                        </div>

                    </asp:Panel>

            </div> <!-- end container -->
      
   <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="btnSearch" />
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










