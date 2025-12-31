<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportTransaksiBukuBesar.aspx.cs" Inherits="GL_Report_ReportTransaksiBukuBesar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Report Transaction Buku Besar</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- App css -->
    <link href="../../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../../assets/js/modernizr.min.js"></script>






    <!-- Plugins css-->
    <link href="../../../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../../assets/js/modernizr.min.js"></script>

    <link href="../../../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../../../assets/js/popper.min.js"></script>
    <script src="../../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../../assets/js/waves.js"></script>
    <script src="../../../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../../../Javascript/jquery-ui.js"></script>






    <style type="text/css">
        .form-control {
        }
    </style>

       <script type="text/javascript">

           $(document).ready(InIEvent);
           function InIEvent() {
               // Default Datatable
               $('#datatable').DataTable();

               //Buttons examples
               var table = $('#GRData').DataTable({
                   "order": [], "paging": false,
                   buttons: ['copy', 'excel', 'pdf'],
                   "columnDefs": [{
                       "targets": 'no-sort',
                       "orderable": false,
                   }]
               });



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
               $('.selectpicker').selectpicker();



               $(".dataTables_scrollHeadInner").css({ "width": "100%" });

               $(".table ").css({ "width": "100%" });

               $('form').parsley();



           }


           function showSuccessMessage() {
               try {
                   swal({
                       title: "Data tidak ditemukan",
                       text: "",
                       type: "success",
                       showCancelButton: false,
                       confirmButtonColor: "#DD6B55",
                       confirmButtonText: "OK",
                       closeOnConfirm: true,
                       closeOnCancel: false
                   }, function (isConfirm) {
                       if (isConfirm) {

                      <%--  setTimeout(function () {<%= Lumpsum() %>; }, 1); --%>

                    }
                });
               }
               catch (err) {
                   console.log(err.message);
               }

           }
       </script>

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
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Report Transaction Perhari</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Report Transaction Buku Besar Perhari</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->


                    <asp:Panel ID="Panel1" Visible="true" runat="server">                                              
                                
                  <div class="row">

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Periode</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                    <div class="form-group">
                                        <label for="inputState" class="col-form-label">Tanggal Hari Ini</label>
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>     
                                    </div>
                    <%--            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">No. Reference from</label>
                                          <asp:DropDownList ID="Ddlreferenceawal" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">No. Reference to</label>
                                         <asp:DropDownList ID="Ddlreferenceakhir" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>                                   
                                </div>--%>
                               
                                 <div class="form-group">
                                        <label for="inputState" class="col-form-label">Kode Account</label>
                                          <asp:DropDownList ID="ddlaccountakhir" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>  

                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Type Journal</label>
                                          <asp:DropDownList ID="ddltypejournalawal" CssClass="form-control"  runat="server"></asp:DropDownList>
                                          <asp:DropDownList ID="ddltypejournalakhir" Visible="false" CssClass="form-control"  runat="server"></asp:DropDownList>
                                         <asp:DropDownList ID="ddlaccountawal" visible="false" CssClass="form-control"  runat="server"></asp:DropDownList>
                                               
                                                <asp:DropDownList ID="ddlsourceawal" Visible="false" CssClass="form-control"  runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlsourceakhir" Visible="false" CssClass="form-control"  runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlcostcenterawal" Visible="false" CssClass="form-control"  runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlcostcenterakhir" Visible="false" CssClass="form-control"  runat="server"></asp:DropDownList>

                                    </div>
                                     <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">User Entry</label>
                                          <asp:DropDownList ID="ddlteller" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                                                      
                                </div>

                                    <div class="form-group">
                                         <label for="inputpid" class="col-3 col-form-label">Report Type</label>
                                          <asp:DropDownList ID="ddlrpttype" CssClass="form-control"  runat="server" Enabled="false">
                                                                    <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                                    <asp:ListItem Value="A">Posting</asp:ListItem>
                                                                    <asp:ListItem Value="B">Un-Posting</asp:ListItem>
                                          </asp:DropDownList>
                                                
                                    </div>
                 
                                </div>                                                    
                            </div>
                           </div>

                                         
                    </div>
            </asp:Panel>
                    <asp:Panel ID="Panel2" Visible="true" runat="server">
               
                         <div class="button-list">
                                <asp:Button ID="btnsearchdata" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="btnsearchdata_Click"/> 
                                 <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Report" OnClick="btnSerach_Click"/>  
                       
                                </div>

                    </asp:Panel>

                    <asp:Panel ID="PnlFront" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title">Transaksi Buku Besar</h4>
                                      <div class="form-row">
                                                                
															


                                                               <div class="form-group col-md-3">
                                                                <asp:label ID="Label40" runat="server" class="col-form-label">Saldo Awal</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtsaldoAwal" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 

                                                                <div class="form-group col-md-3">
                                                                <asp:label ID="Label41" runat="server" class="col-form-label">Mutasi Debit</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtmutasidebit" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 
                                                                  <div class="form-group col-md-3">
                                                                    <asp:label ID="Label57" runat="server" class="col-form-label">Mutasi Kredit</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtmutasikredit" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 
                                                            
                                                                 <div class="form-group col-md-3">
                                                                    <asp:label ID="Label60" runat="server" class="col-form-label">Saldo Akhir</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtsaldoakhir" Enabled="false"  runat="server" >0</asp:TextBox>
                                                                </div> 



                                                            </div>
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">

                                         <asp:GridView ID="GRData" runat="server" ShowFooter="True" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRData_RowDataBound"  AutoGenerateColumns="False" Width="625px">
                                             <Columns>

                                                   <asp:TemplateField HeaderText="No">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="No Bukti">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Tgl Trn">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Keterangan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Jlh Debit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbldebit" CssClass="" runat="server" Text='<%#Eval("JlhDebit","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                        <FooterTemplate>
                                                         
                                                            <span style="display:block; padding:0; text-align: right"><asp:Label ID="lbldebit" Font-Bold="true" runat="server" Text='0'  /> </span>
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Jlh Kredit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblkredit" CssClass="" runat="server" Text='<%#Eval("JlhKredit","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                          <span style="display:block; padding:0; text-align: right"><asp:Label ID="lblkredit" Font-Bold="true" runat="server" /> </span>
                                                         
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>                                                 
                                                <asp:TemplateField HeaderText="Saldo" Visible="false">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsaldo" CssClass="" runat="server" Text='0'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Tgl Entry">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltglentrydate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglEntry","{0:dd-MM-yyyy HH:mm:ss}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="User Entry ">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbluserentry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodeuser")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                               



                                                
                                             </Columns>
                                         </asp:GridView>

                                     </div>
                                 </div>

                             </div>
                         </div>
                     </div>

                 </asp:Panel>
                    

                </div>
                <!-- end container -->

                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
                 <asp:PostBackTrigger ControlID="btnsearchdata" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>

               <!-- Parsley js -->
    <script type="text/javascript" src="../../../../plugins/parsleyjs/parsley.min.js"></script>

    <script src="../../../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="../../../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>


        <!-- Required datatable js -->
        <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="../../../../plugins/datatables/dataTables.bootstrap4.min.js"></script>
        <!-- Buttons examples -->
        <script src="../../../../plugins/datatables/dataTables.buttons.min.js"></script>
        <script src="../../../../plugins/datatables/buttons.bootstrap4.min.js"></script>
        <script src="../../../../plugins/datatables/jszip.min.js"></script>
        <script src="../../../../plugins/datatables/pdfmake.min.js"></script>
        <script src="../../../../plugins/datatables/vfs_fonts.js"></script>
        <script src="../../../../plugins/datatables/buttons.html5.min.js"></script>
        <script src="../../../../plugins/datatables/buttons.print.min.js"></script>

        <!-- App js -->
        <script src="../../../../assets/js/jquery.core.js"></script>
        <script src="../../../../assets/js/jquery.app.js"></script>
        <!-- plugin js -->
        <script src="../../../../plugins/moment/moment.js"></script>

<%--<script type="text/javascript">

        //  $(document).ready(InIEvent);
        //function InIEvent() {

        $(document).ready(function () {

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

        });
</script>--%>
</html>










