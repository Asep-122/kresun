<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_manager_cab.aspx.cs" Inherits="AR_Master_Update_manager_cab" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Update Manager Cabang</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../assets/js/modernizr.min.js"></script>

    <link href="../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../Javascript/jquery-ui.js"></script>

        <!-- Toastr css -->
    <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />
  
        <!-- Modal -->
    <link href="../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />
    
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../../plugins/tooltipster/tooltipster.bundle.min.css" />
        <!-- DataTables -->
    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    
    <style>
        div.swal2-shown:not(.swal2-no-backdrop):not(.swal2-toast-shown) {
            overflow-y: visible !important;
            height: 800px
        }
    </style>

        <script type="text/javascript">
            function jv_openModal_root() {
                setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
            }
            history.pushState(null, null, location.href);
            window.onpopstate = function () {
                history.go(1);
            };
         

            function SweetAlert(errMsg) {

                setTimeout(function () {

                    swal(
                        {
                            title: errMsg,
                            confirmButtonClass: 'btn btn-confirm mt-2'
                        }
                    ).then(function () {
                    }).catch(swal.noop)
                }, 100);

            }

            function sweetAlertConfirm(btnDelete, errMsg) {
                if (btnDelete.dataset.confirmed) {
                    // The action was already confirmed by the user, proceed with server event
                    btnDelete.dataset.confirmed = false;
                    return true;
                } else {
                    // Ask the user to confirm/cancel the action
                    event.preventDefault();
                    swal({
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2',
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonText: "Yes",
                        cancelButtonText: "No",
                    })
                        .then(function () {
                            // Set data-confirmed attribute to indicate that the action was confirmed
                            btnDelete.dataset.confirmed = true;
                            btnDelete.click();
                        }).catch(function (reason) {
                            // The action was canceled by the user
                            return false
                        });
                }
            } 

            $(document).ready(InIEvent);
            function InIEvent() {
                $("div").remove(".tooltip");
                $.getScript("../assets/js/jquery.core.js");

                $('#LnkAdd').tooltipster({
                    animation: 'grow'
                });

                $('#txtTalhir').datepicker({
                    format: 'dd-mm-yyyy',
                    autoclose: true,
                    todayHighlight: true
                });
                
                $('.selectpicker').selectpicker();

                function BindDataTable() {
                    var table = $('#GRData').dataTable({
                        "order": [],
                        "paging": false,
                        "columnDefs": [{
                            "targets": 'no-sort',
                            "orderable": false,
                        }],
                        ordering: [[0, 'asc']],
                        "scrollX": true,
                        retrieve: true

                    });

                    var tableX = $('#GRDT').dataTable({
                        "order": [],
                        "columnDefs": [{
                            "targets": 'no-sort',
                            "orderable": false,
                        }],
                        ordering: [[0, 'asc']],
                        "scrollX": true,
                        retrieve: true

                    });
                }

                $(function () {
                    BindDataTable();

                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(BindDataTable);
                });


                $(".dataTables_scrollHeadInner").css({ "width": "100%" });

                $(".table ").css({ "width": "100%" });

                $('form').parsley();

            }

        </script>

    </head>
<body>               
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>   <!-- delete  -->
            <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>--%>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Utility</a></li>
                                    <li class="breadcrumb-item"><a href="#">Update Data</a></li>
                                    <li class="breadcrumb-item"><a href="Update_manager_cab.aspx">Updata Data</a></li>
                                </ol>
                            </div>
                           
                        </div>
                    </div>
                </div>
                 <!-- Panel Header -->
                 <asp:Panel ID="pnlHeader" runat="server">
                     
                 </asp:Panel>

                 <asp:Panel ID="pnlResult" runat="server" CssClass="rounded">
                      <div class="row clearfix">
                           <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                               <div class="row">
                                   <div class="col-12">
                                     <div class="card-box ribbon-box">
                                         <div class="ribbon ribbon-custom">EDIT SIGNATURE MANAGER</div>
                                          <p class="text-muted font-14 m-b-30"></p>
                                          <div class="col-md-12 p-3 mt-2">
                                           
                                             <asp:Panel ID="pnlInput" runat="server" CssClass="rounded">
                                                 
                                                     <div class="form-group row">
                                                         <asp:HiddenField ID="nourut" runat="server" />
                                                         <label class="col-2 col-form-label">Cabang</label>
                                                         <div class="col-sm-4">
                                                             <asp:DropDownList ID="ddlCabang" runat="server" AutoPostBack="true" CssClass="ddl selectpicker show-tick" data-style="btn-success" OnSelectedIndexChanged="ddlCabang_SelectedIndexChanged">
                                                                
                                                             </asp:DropDownList>
                                                         </div>
                                                     </div>
                                                    <div class="form-group">
                                                    <label for="inputpid" class="col-form-label">Nama Manager</label>
                                                    <asp:TextBox ID="txtNama" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                     <div class="form-group">
                                                    <label for="inputpid" class="col-form-label">Nohp</label>
                                                     <asp:TextBox ID="txtHP" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                   <div class="form-group">
                                                   
                                                      <asp:Button ID="btnSave" class="btn btn-success waves-light text-white" runat="server" Text="Update Data" OnClick="btnSave_Click" />
                                                    </div>
                                                 
                                                 
                                                    
                                                 
                                             </asp:Panel>
                                         </div>
                                     </div>
                                    </div>
                                   <div class="col-12">
                                     <div class="card-box ribbon-box">
                                         <div class="ribbon ribbon-custom">Update Pencairan Uang Muka</div>
                                          <p class="text-muted font-14 m-b-30"></p>
                                          <div class="col-md-12 p-3 mt-2">
                                           
                                             <asp:Panel ID="Panel1" runat="server" CssClass="rounded">
                                                   <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                    <label for="inputpid" class="col-form-label">Data Rekening</label>
                                                    <asp:TextBox ID="TxtRekening" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                 
                                                   <div class="form-group col-md-4">
                                                   
                                                      <asp:Button ID="BTNCAIR" class="btn btn-success waves-light text-white" runat="server" Text="Update pencairan" OnClick="btnPencairan_Click" />
                                                    </div>
                                                 
                                                 
                                                    
                                                 
                                             </asp:Panel>
                                         </div>
                                     </div>
                                    </div>
                               </div>
                            </div>
                          </div>
                 </asp:Panel>


                 
                 
            </div> 
            
        <!-- end wrapper -->
        </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="BTNCAIR" />
               <%-- <asp:PostBackTrigger ControlID="btnUpdate" />--%>
               <%-- <asp:PostBackTrigger ControlID="btnCancel" />--%>

               <%--<asp:PostBackTrigger ControlID="GRDT" />--%>
               
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  <!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>
<script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>

<script src="../../plugins/tablesaw/js/tablesaw.js"></script>
<script src="../../plugins/tablesaw/js/tablesaw-init.js"></script>
<script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<!-- Buttons examples -->
<script src="../../plugins/datatables/dataTables.buttons.min.js"></script>
<script src="../../plugins/datatables/buttons.bootstrap4.min.js"></script>
<script src="../../plugins/datatables/jszip.min.js"></script>
<script src="../../plugins/datatables/pdfmake.min.js"></script>
<script src="../../plugins/datatables/vfs_fonts.js"></script>
<script src="../../plugins/datatables/buttons.html5.min.js"></script>
<script src="../../plugins/datatables/buttons.print.min.js"></script>
<!-- Key Tables -->
<script src="../../plugins/datatables/dataTables.keyTable.min.js"></script>
<script src="../../assets/js/jquery.core.js"></script>
<!-- plugin js -->
<script src="../../plugins/moment/moment.js"></script>
</html>
