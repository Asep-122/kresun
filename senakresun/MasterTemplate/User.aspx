<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="MasterTemplate_User" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />


    <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />

    <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/assets/js/modernizr.min.js"></script>

    <link href="/assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Modal -->
    <link href="/plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Toastr css -->
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="/assets/js/modernizr.min.js"></script>

    <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive datatable examples -->
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>
    <script src="/Javascript/jquery-ui.js"></script>

    <!-- Modal-Effect -->
    <script src="/plugins/custombox/js/custombox.min.js"></script>
    <script src="/plugins/custombox/js/legacy.min.js"></script>

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
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }

                        if (fr == '2') {
                            setTimeout(function () { $('#modalGroup').modal('hide'); <%=btnremodal()%>; }, 10);
                        }
                    }
                });
            }, 0);

        }

        function redirect() {
            location.href = 'User.aspx ';
        }
        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type : 'success',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }
        function WarningAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 4000
                    }
                ).catch(swal.noop)
            }, 100);

        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

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
                <div class="container-fluid">
                    <!-- Form row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Master User Setting</div>
                                <p class="text-muted m-b-30 font-13">
                                    <asp:LinkButton ID="lnkRefreshModal" runat="server" OnClick="lnkRefreshModal_Click" />
                                    <asp:Panel ID="pnlFront" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-form-label">Search By</label>
                                                                <div class="form-row">
                                                                    <div class="form-group col-md-6">
                                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                                            <asp:ListItem Value=" and UserName ">Employee ID</asp:ListItem>
                                                                            <asp:ListItem Value=" and Name ">Employee Name</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                        <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="col-sm-12">
                                                            <div class="text-center m-t-30">
                                                                <asp:LinkButton runat="server" ID="lnkCari" CssClass="btn btn-icon waves-effect waves-light btn-purple" title="Cari" OnClick="lnkCari_Click"> <i class="mdi mdi-account-search"></i></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="LnkAdd" CssClass="btn btn-icon waves-effect waves-light btn-purple" title="Tambah User Lokal" OnClick="LnkAdd_Click"> <i class="mdi mdi-group"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="GRUser" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" OnRowDataBound="GRUser_RowDataBound" OnRowCommand="GRUser_RowCommand" ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee ID">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnPassword" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Password")%>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="hdnIslocal" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "IsLocal")%>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="hdnIsSuperAdmin" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "IsSuperAdmin")%>'></asp:HiddenField>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Employee Name">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Employee Branch">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBranch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BranchID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "email")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Is Active">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkIsActive" Enabled="false" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "IsActive") == DBNull.Value ? false:DataBinder.Eval(Container.DataItem, "IsActive")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Setting User">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEditUser" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Edit" runat="server" CommandName="GRUPEDT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkGR" runat="server" data-toggle="tooltip" data-placement="top" class="tooltips" data-original-title="Group Menu" CommandName="GRUPSET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-group"></i></asp:LinkButton>
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
                                    <asp:Panel ID="pnlUserEdit" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="card-box">
                                                    <%--  <h4 class="header-title m-t-0 m-b-20">List Menu</h4>--%>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="card-box">
                                                                <div class="form-horizontal">
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">User Name</label>
                                                                        <div class="col-9">
                                                                            <asp:TextBox CssClass="form-control" ID="txtUsername" required="required" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Name</label>
                                                                        <div class="col-9">
                                                                            <asp:TextBox CssClass="form-control" ID="txtName" required="required" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Branch</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlbranch" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Email</label>
                                                                        <div class="col-9">
                                                                            <asp:TextBox CssClass="form-control" ID="txtMail" required="required" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Password</label>
                                                                        <div class="col-9">
                                                                            <asp:TextBox CssClass="form-control" ID="txtPass1" TextMode="Password" required="required" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Confirm Password</label>
                                                                        <div class="col-9">
                                                                            <asp:TextBox CssClass="form-control" ID="txtpassc" TextMode="Password" required="required" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row justify-content-end">
                                                                        <div class=" col-9">
                                                                            <div class="checkbox checkbox-primary">
                                                                                <asp:CheckBox ID="chkIsActive" runat="server" />
                                                                                <label for="chkIsActive">
                                                                                    Is Active
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group mb-0 justify-content-end row">
                                                                        <div class="col-9">
                                                                            <asp:Button ID="btnSimpan" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                                                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnCancel_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                            <!-- end col -->
                                        </div>
                                    </asp:Panel>
                                </p>
                            </div>
                        </div>
                    </div>
                    <!-- end row -->

                    <asp:LinkButton ID="lnkopenGroup" runat="server" data-toggle="modal" data-target="#modalGroup"></asp:LinkButton>
                    <div id="modalGroup" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" data-backdrop="static" data-keyboard="false" aria-hidden="true" style="display: none;">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"  id="btnclose" runat="server" onclick="redirect()">×</button>
                                    <h4 class="modal-title" id="myLargeModalLabel">Master User Setting</h4>
                                </div>
                                <div class="modal-body">
                                    <!-- Form row -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <div class="form-group">
                                                                    <label class="col-form-label">Employee ID</label>
                                                                    <asp:Label ID="lblEMPID" CssClass="form-control" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <div class="form-group">
                                                                    <label class="col-form-label">Employee Name</label>
                                                                    <asp:Label ID="lblEMPNAME" CssClass="form-control" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-row">
                                                            <div class="col-sm-12">
                                                                <div class="text-center m-t-30">
                                                                    <asp:LinkButton runat="server" ID="lnkSaveGR" CssClass="btn btn-icon waves-effect waves-light btn-purple" title="Simpan" OnClick="lnkSaveGR_Click"> <i class="mdi mdi-content-save-all"></i></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                            <div class="form-group" style="width: 100%;">
                                                                <asp:GridView ID="DGGroup" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="true"
                                                                    AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIdGroup" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IdGroup")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No">
                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rowNumber")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Group Name">
                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGroupName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GroupName")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Is Group">
                                                                            <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chklist" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "isgroup") == DBNull.Value ? false:DataBinder.Eval(Container.DataItem, "isgroup")%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end row -->
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
                <!-- /.modal -->

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkCari" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="lnkSaveGR" />
                <asp:PostBackTrigger ControlID="lnkRefreshModal" />
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>
<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>
<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
    <script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>
    <script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>
</html>

