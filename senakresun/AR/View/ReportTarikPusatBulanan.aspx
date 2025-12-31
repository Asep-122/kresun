<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportTarikPusatBulanan.aspx.cs" Inherits="AR_Report_ReportTarikPusatBulanan" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Report Tarik Pusat Bulanan</title>
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
                                        <li class="breadcrumb-item"><a href="#">Account Receivable</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report</a></li>
                                        <li class="breadcrumb-item"><a href="#">Report Tarik Pusat Bulanan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Report Tarik Pusat Bulanan</h4>
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
                                        <label for="inputState" class="col-form-label">Tanggal Tarik Pusat</label>
                                         <asp:TextBox ID="txttglhariini" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>     
                                    </div>
                                   
                               
                                

                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Pilih Report</label>
                                            <asp:DropDownList ID="ddltypereport" CssClass="form-control"  runat="server">
                                                                    <asp:ListItem Value="A">CABANG</asp:ListItem>
                                                                    <asp:ListItem Value="B">KOPERASI</asp:ListItem>
                                                                  
                                          </asp:DropDownList>
                                    </div>

                                      <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Cabang</label>
                                          <asp:DropDownList ID="ddlcabang" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                                                      
                                     <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Koperasi</label>
                                          <asp:DropDownList ID="ddlkoperasi" CssClass="form-control"  runat="server"></asp:DropDownList>
                                    </div>
                                
                                </div>


                 
                                </div>                                                    
                            </div>
                           </div>

                                         
                    </div>
            </asp:Panel>
               <asp:Panel ID="Panel2" Visible="true" runat="server">
               
                         <div class="button-list">
                                <asp:Button ID="btnsearchdata" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="btnsearchdata_Click"/> 
                                
                       
                                </div>

                    </asp:Panel>

                    <asp:Panel ID="PnlFront" runat="server">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title">Transaksi Tarik Pusat</h4>
                                      
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
                                                    <asp:TemplateField HeaderText="Cabang">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblcabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-left"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No Journal">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnojurnal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No Anggota">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "noanggota")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="No Rekening">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "norekening")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Drop">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgldrop" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Tgldrop","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Tgl Due">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgldue" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglDue","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Tgl Pelunasan">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgllunas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglPelunasan","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                              

                                                   <asp:TemplateField HeaderText="Simpanan Pokok">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsimpok" CssClass="" runat="server" Text='<%#Eval("simpananpokok","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                        <FooterTemplate>
                                                         
                                                            <span style="display:block; padding:0; text-align: right"><asp:Label ID="lbldebit" Font-Bold="true" runat="server" Text='0'  /> </span>
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Simpanan Wajib">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblsimwa" CssClass="" runat="server" Text='<%#Eval("simpananwajib","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                          <span style="display:block; padding:0; text-align: right"><asp:Label ID="lblkredit" Font-Bold="true" runat="server" /> </span>
                                                         
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-right"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
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










