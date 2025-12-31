<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master3.aspx.cs" Inherits="MasterTemplate_Master3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Highdmin - Responsive Bootstrap 4 Admin Dashboard</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../plugins/tooltipster/tooltipster.bundle.min.css" />

    <!-- Plugins css-->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <!-- App css -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />

    <!-- Modal -->
    <link href="../plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Toastr css -->
    <link href="../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="../assets/js/modernizr.min.js"></script>

    <!-- DataTables -->
    <link href="../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive datatable examples -->
    <link href="../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Multi Item Selection examples -->
    <link href="../../plugins/datatables/select.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- jQuery  -->
    <script src="../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../assets/js/popper.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/waves.js"></script>
    <script src="../assets/js/jquery.slimscroll.js"></script>

    <!-- Modal-Effect -->
    <script src="../plugins/custombox/js/custombox.min.js"></script>
    <script src="../plugins/custombox/js/legacy.min.js"></script>
   <script src="../assets/js/modernizr.min.js">
    <script type="text/javascript">
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }

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
<%--                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }

                        if (fr == '2') {
                            setTimeout(function () { $('#modalGroup').modal('hide'); <%=btnremodal()%>; }, 10);
                        }
                    }--%>
                });
            }, 0);

        }

        function SweetAlert(errMsg) {

          <%--  setTimeout(function () {

                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).then(function () {
                    setTimeout(function () { <%=btnremodal()%>; }, 50);
                    }).catch(swal.noop)
            }, 100);--%>

        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd, #lnkCari, #lnkSaveGR').tooltipster({
                animation: 'grow'
            });

            //$('#modalGroup').on('shown.bs.modal', function () {
            //    $('#modalGroup').animate({ scrollTop: 0 }, 'slow');
            //});

            $('.selectpicker').selectpicker();

            $(function () {
                var table = $('#GRUser').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });

                var tableGroup = $('#DGGroup').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    lengthMenu: [
                        [-1],
                        ['All']
                    ]
                });

                $("#DGGroup_filter").hide();

            });

            //$(".dataTables_scrollHeadInner").css({ "width": "100%" });

            //$(".table ").css({ "width": "100%" });

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
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Highdmin</a></li>
                                    <li class="breadcrumb-item"><a href="#">Components</a></li>
                                    <li class="breadcrumb-item"><a href="#">Tables</a></li>
                                    <li class="breadcrumb-item active">Datatable</li>
                                </ol>
                            </div>
                            <h4 class="page-title">Datatable</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" runat="server">
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
                            <h4 class="m-t-0 header-title">Header</h4>
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light waves-effect" type="button" runat="server" Text="Add Cost Center" />
                            <asp:Button ID="btnSimpan" class="btn btn-success waves-light waves-effect" type="button" runat="server" Text="Save Cost Center" Visible="false" />
                        </div>
                    </div>
                </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="pnlFront" runat="server">
            <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                            <h4 class="m-t-0 header-title">Buttons example</h4>
                            <p class="text-muted font-14 m-b-30">
                                The Buttons extension for DataTables provides a common set of options, API methods and styling to display buttons on a page that will interact with a DataTable. The core library provides the based framework upon which plug-ins can built.
                            </p>
                            
                   <%--         <table id="datatable-buttons" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                
<asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="550px">
                                <Columns>

                                    <asp:TemplateField HeaderText="Cost Center ID">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodeCc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCc")%>'></asp:Label>
                                          </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Center Name">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamaCC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCC")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Center Description">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetCC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetCC")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <HeaderStyle Height="30px" CssClass="align-center no-sort"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="material-icons">mode_edit</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Delete">
                                        <HeaderStyle Height="30px" CssClass="align-center no-sort"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btndelete" runat="server" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="material-icons">delete_forever</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>                            
                                </Columns>
                            </asp:GridView>

                                
                            </table>--%>
                        </div>
                    </div>
                </div>
        </asp:Panel>
                 <!-- Panel Edit -->
                 <asp:Panel ID="pnlEdit" Visible="false" runat="server">
            <%--<div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA COST CENTER</div>
                                    <p class="text-muted m-b-30 font-13">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-group">
                                                        <label class="col-form-label">NIK Konsumen</label>
                                                        <asp:TextBox CssClass="form-control" ID="txtNIK" onkeypress="return isnumerickey(event);" MaxLength="16" runat="server" data-parsley-type="number" parsley-trigger="change" required="required" data-parsley-maxlength="16" data-parsley-error-message="Huruf angka Max 16 Digit"></asp:TextBox>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <label class="col-form-label">Nama Konsumen</label>
                                                            <asp:TextBox CssClass="form-control" ID="txtNK" runat="server" parsley-trigger="change" required="required" data-parsley-pattern="/^[a-zA-Z\s]+$/" data-parsley-error-message="Huruf tanpa angka dan tanda baca"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label class="col-form-label">Status Perkawinan</label>
                                                            <asp:DropDownList ID="ddlSP" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlSP_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Single</asp:ListItem>
                                                                <asp:ListItem Value="2">Menikah</asp:ListItem>
                                                                <asp:ListItem Value="3">Janda / Duda</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="pnlMenikah" Visible="false" runat="server">
                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <label class="col-form-label">Nama Pasangan</label>
                                                                <asp:TextBox CssClass="form-control" ID="txtNP" runat="server" parsley-trigger="change" required="required" data-parsley-pattern="/^[a-zA-Z\s]+$/" data-parsley-error-message="Huruf tanpa angka dan tanda baca"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label class="col-form-label">NIK KTP Pasangan</label>
                                                                <asp:TextBox CssClass="form-control" ID="txtNIKNP" onkeypress="return isnumerickey(event);" MaxLength="16" runat="server" data-parsley-type="number" parsley-trigger="change" required="required" data-parsley-maxlength="16" data-parsley-error-message="Huruf angka Max 16 Digit"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <label class="col-form-label">Status Konsumen</label>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" CssClass="selectpicker show-tick" data-style="btn-custom">
                                                                <asp:ListItem Value="1">Umum</asp:ListItem>
                                                                <asp:ListItem Value="2">RO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label class="col-form-label">History NPP</label>
                                                            <asp:TextBox CssClass="form-control" ID="txtHNPP" Enabled="false" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </p>
                                </div>
                            </div>
                        </div>--%>
                     </asp:Panel>
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>

        </asp:UpdatePanel>
    </form>
</body>
     <!-- jQuery  -->
        <script src="../assets/js/jquery.min.js"></script>
        <script src="../assets/js/popper.min.js"></script>
        <script src="../assets/js/bootstrap.min.js"></script>
        <script src="../assets/js/waves.js"></script>
        <script src="../assets/js/jquery.slimscroll.js"></script>

        <!-- Required datatable js -->
        <script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>
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

        <!-- Responsive examples -->
        <script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
        <script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>

        <!-- Selection table -->
        <script src="../../plugins/datatables/dataTables.select.min.js"></script>

        <!-- App js -->
        <script src="../assets/js/jquery.core.js"></script>
        <script src="../assets/js/jquery.app.js"></script>

        <script type="text/javascript">
            $(document).ready(function() {

                // Default Datatable
                $('#datatable').DataTable();

                //Buttons examples
                var table = $('#datatable-buttons').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
                });

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
                        .appendTo('#datatable-buttons_wrapper .col-md-6:eq(0)');
            } );

        </script>
</html>
