<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Kategori.aspx.cs" Inherits="GL_Master_Kategori" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Kategori General Ledger</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
     <!-- Tooltipster css -->
    <link rel="stylesheet" href="../../plugins/tooltipster/tooltipster.bundle.min.css" />

    <!-- Plugins css-->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <!-- App css -->
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <!-- Modal -->
    <link href="../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Toastr css -->
    <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="../../assets/js/modernizr.min.js"></script>

    <!-- DataTables -->
    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive datatable examples -->
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>

    <!-- Modal-Effect -->
    <script src="../../plugins/custombox/js/custombox.min.js"></script>
    <script src="../../plugins/custombox/js/legacy.min.js"></script>
   
    <script type="text/javascript">
            function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: t,
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 2000,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }
                       if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50);
                        }
                    }
                });
            }, 0);

        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

        }
        
        function sweetAlertConfirm(btndelete,errMsg) {
        if (btndelete.dataset.confirmed) {
            // The action was already confirmed by the user, proceed with server event
            btndelete.dataset.confirmed = false;
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
                    btndelete.dataset.confirmed = true;
                    btndelete.click();
                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
        }
        }    

           function cancelalert(errMsg) {
               swal({
                   title: errMsg,
                   // text: 'Your message has been succesfully sent',
                   confirmButtonClass: 'btn btn-confirm mt-2',
                   type: 'warning'

               })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                });
        }

function sweetAlertsave(btnSimpan,errMsg) {
        if (btnSimpan.dataset.confirmed) {
            // The action was already confirmed by the user, proceed with server event
            btnSimpan.dataset.confirmed = false;
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
                    btnSimpan.dataset.confirmed = true;
                    btnSimpan.click();
                  
                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
        }
        } 


        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('.selectpicker').selectpicker();

            $(function () {
                var table = $('#GRData').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });

            });

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="CostCenter.aspx">Kategori Code</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Master Kategori</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="false" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                            <h4 class="m-t-0 header-title">Filter</h4>
                            <p class="text-muted font-14 m-b-30">
                                KeyTable provides Excel like cell navigation on any table. Events (focus, blur, action etc) can be assigned to individual cells, columns, rows or all cells.
                            </p>

                            
                        </div>
                    </div>
                </div>
                </asp:Panel>
                 <!-- Panel Header -->
               <asp:Panel ID="pnlHeader" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kategori" OnClick="btnAdd_Click" />
                            <asp:LinkButton ID="lnkRefreshModal" Visible="false" runat="server" OnClick="lnkRefreshModal_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Cost Center</h4>                           
                                     <div class="form-row">
                                    <div class="form-group" style="width: 100%;">
                                    <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="true" OnRowCommand="GRData_RowCommand"
                                            AutoGenerateColumns="False">
                                    <Columns>

                                    <asp:TemplateField HeaderText="Kategori ID">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeKat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeKat")%>'></asp:Label>
                                            <%--  <asp:Label ID="lblZonaID" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                            <asp:Label ID="lblStatus" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "active_status_id")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kategori Name">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetKategori" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetKategori")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Awal Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoAwalGrup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoAwalGrup")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Akhir Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoAhirGrup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoAhirGrup")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btndelete" runat="server" data-toggle="tooltip" data-placement="left" title="Delete" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
                                                                                 
                                        <%--<asp:LinkButton ID="btndg" runat="server" CommandName="DEL" data-toggle="tooltip" data-placement="right" title="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to delete ? ')) { return false; }"> <i class="material-icons">delete_forever</i></asp:LinkButton>--%>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>
                                                                
                                                            </asp:GridView>
                                                        </div>
                                                    </div> 

                        </div>
                    </div>
                </div>
        </asp:Panel>
                 <!-- Panel Edit -->
            <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                              <div class="row clearfix">
                                                <div class="col-md-6">
                                                    <p>
                                                        <b>Kategori ID</b>
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox CssClass="form-control" parsley-trigger="change" required="required" data-parsley-pattern="/^[a-zA-Z\s]+$/" data-parsley-error-message="Huruf tanpa angka dan tanda baca" ID="txtKodeKat"  runat="server" MaxLength="1"></asp:TextBox>
                                                      </div>
                                                    </div>
                                                </div>
                                            </div>
                                                      <div class="row clearfix">                                         
                                                <div class="col-md-6">
                                                    <p>
                                                        <b>Kategori Name</b>
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtKetKategori" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                             </div>
                                            <div class="row clearfix">                                         
                                                <div class="col-md-6">
                                                    <p>
                                                        <b>No Awal Group</b>
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="form-line">                                                      
                                                           <asp:DropDownList ID="ddlNoAwalGrup" CssClass="form-control" required="required" runat="server"></asp:DropDownList>    
                                                        </div>
                                                    </div>
                                                </div>
                                             </div>
                                               <div class="row clearfix">                                         
                                                <div class="col-md-6">
                                                    <p>
                                                        <b>No Akhir Group</b>
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlNoAhirGrup" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                             </div>

                                            <div class="row clearfix">
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
            </asp:Panel>
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                 <asp:PostBackTrigger ControlID="btnCancel" />
                 
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>
<!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>
</html>
