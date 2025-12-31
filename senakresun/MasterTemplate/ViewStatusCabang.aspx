<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStatusCabang.aspx.cs" Inherits="View_status_cabang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>View Status Cabang</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/assets/js/modernizr.min.js"></script>

    <link href="/assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>

    <script src="/Javascript/jquery-ui.js"></script>

    <!-- Toastr css -->
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Modal -->
    <link href="/plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
    <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
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
                            setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
                        }

                        if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                        }
                    }
                });
            }, 0);

        }

        function redirect() {
            location.href = 'ViewStatusCabang.aspx ';
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'success',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }

        function simpan(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);
            $('#txtnobukti').value("");
        }
        function WarningAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        showConfirmButton: true,
                        timer: 10000
                    }
                ).catch(swal.noop)
            }, 100);

        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('.selectpicker').selectpicker();


            $(function () {
                var table = $('#GRDD').dataTable({
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
    <script type="text/javascript">
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9]/.test(inp))
                return true;
            alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
    </script>

    <script src="/Javascript/jquery-ui.js"></script>
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
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Configuration</a></li>
                                        <li class="breadcrumb-item"><a href="ViewStatusCabang.aspx">View Status Cabang</a></li>
                                    </ol>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Edit -->
                    <asp:Panel ID="pnladd" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 center-page">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Filter</div>
                                    <br />
                                    <div class="title text-center mb-3">
                                        <h3>Search Status Cabang</h3>
                                    </div>
                                    <br />
                                    <div class="form-row">
                                        <div class="form-group  col-md-12">
                                            <div class="form-group" runat="server" visible="false">
                                                <div class="form-check-inline col-md-12">
                                                    <asp:Label ID="Label2" runat="server" class="col-md-4 col-form-label font-weight-bold">Koperasi</asp:Label>
                                                    <div class="col-6">
                                                        <asp:DropDownList ID="ddlKoperasi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlKoperasi_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group  ">
                                                <div class="form-check-inline col-md-12">
                                                    <asp:Label ID="Label1" runat="server" class="col-md-4 col-form-label font-weight-bold">Cabang</asp:Label>
                                                    <div class="col-6">
                                                        <asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group  ">
                                                <div class="form-check-inline col-md-12">
                                                    <label for="inputCity" class="col-md-4 col-form-label font-weight-bold">Bulan</label>
                                                    <div class="col-6">
                                                        <asp:DropDownList ID="ddlbulan" runat="server" CssClass="form-control" data-style="btn-custom">
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
                                                </div>
                                            </div>
                                            <div class="form-group ">
                                                <div class="form-check-inline col-md-12">
                                                    <asp:Label ID="Label6" runat="server" class="col-md-4 col-form-label font-weight-bold">Tahun</asp:Label>
                                                    <div class="col-4">
                                                        <asp:DropDownList ID="ddltahun" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row justify-content-center">
                                            <div class="btn-group mb-2">
                                                <div class="button-list">
                                                    <asp:Button ID="btnsearch" class="btn btn-success waves-light text-white" runat="server" Text="Proses" OnClick="btnSimpan_Click" OnClientClick="btnSimpan_Click();" />
                                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                                    <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClientClick='redirect()' />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlgrid" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom" runat="server" id="titlegrid"></div>
                                    <br />
                                    <br />
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRDD" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="false" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Justify" HeaderText="Kode Cabang">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Justify" HeaderText="Nama Cabang">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" Height="30px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="FiskalBi">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljenis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FiskalBi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="FiskalTi">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FiskalTi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Tanggal Awal Bi">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglAwalbi","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Tanggal Akhir Bi">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglAkhirbi","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle ></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Status Closing">
                                                        <HeaderStyle CssClass="text-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsimpanan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_closing")%>'></asp:Label>
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
                    <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsearch" />
                <asp:PostBackTrigger ControlID="GRDD" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Sweet Alert Js  -->
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>
<!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
</html>