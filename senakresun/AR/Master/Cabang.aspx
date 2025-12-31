<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cabang.aspx.cs" Inherits="AR_Master_Cabang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Cabang</title>
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

        function sweetAlertConfirm(btndelete, errMsg) {
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

        function sweetAlertsave(btnSimpan, errMsg) {
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

        function redirect() {
            location.href = 'Cabang.aspx ';
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

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        function isnumerickey(e) {
            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {

                if (txt == 118) {
                    return true;
                }
                //  else {
                //alert("Input is Number only")
                return false;
                //  }

            }
            return true;
        }

        function Comma(Num) { //function to add commas to textboxes

            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');


            return x1 + x2;

        }

        function checkTextField1(field) {
            if (field.value == '' || field.value == 0 || field.value == '.') {
                field.value = 0;
            }


            var nilai = field.value.split(',').join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split("."); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
                output = parts.join(".");
            }
            field.value = output;
        }

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });


            $('#txttglizin').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('.selectpicker').selectpicker();



            $(function () {
                var table = $('#GRData').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
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
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                        <li class="breadcrumb-item"><a href="#">Master</a></li>
                                        <li class="breadcrumb-item"><a href="Cabang.aspx">Cabang</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Master Cabang</h4>
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
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Data Cabang</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="887px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Kode Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Izin Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblizin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "izin_cabang")%>'></asp:Label>
                                                           <%-- <asp:Label ID="lblNpwp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NPWP")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblbank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bankrekeningcabang")%>' Visible="false"></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Izin">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglizin" runat="server" DateFormat="dd-mm-yyyy" Text='<%# Eval("tgl_izin_cabang", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Alamat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Telpon">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltelp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "telp")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="No rekening" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnorek" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "norekeningcabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Manager">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmanager" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Manager")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kabag">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkepalacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kabag")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Visible="false"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                                <div class="row">
                                    <%-- -- DATA CABANG--%>
                                    <div class="col-lg-12">
                                        <%--<div class="col-lg-12">--%>
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA CABANG</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-row col-md-12">
                                                <div class="col-6">
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label1" runat="server" class="col-form-label col-4 font-weight-bold">Kode Cabang</asp:Label>
                                                            <div class="col-3">
                                                                <asp:TextBox CssClass="form-control" ID="txtKodeCabang" MaxLength="3" runat="server" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label2" runat="server" class="col-form-label col-4 font-weight-bold">Nama Cabang</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtNamaCabang" CssClass="form-control" required="required" runat="server" MaxLength="30"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label5" runat="server" class="col-form-label col-4 font-weight-bold">Manager Cabang</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtmanager" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="60" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label4" runat="server" class="col-form-label col-4 font-weight-bold">Kabag</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtkabag" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="60" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label3" runat="server" class="col-form-label col-4 font-weight-bold">NPWP</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtNPWP" CssClass="form-control" required="required" runat="server" MaxLength="30" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label8" runat="server" class="col-form-label col-4 font-weight-bold">No Rekening</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtrek" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" required="required" Text="-"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label10" runat="server" class="col-form-label col-4 font-weight-bold">Bank</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtbank" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="20" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label23" runat="server" class="col-form-label col-4 font-weight-bold">Atas Nama Bank</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtatasnama" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-6">
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label31" runat="server" class="col-form-label col-4 font-weight-bold">Pengadilan</asp:Label>
                                                            <div class="col-8">
                                                                <asp:TextBox ID="txtpengadilan" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label13" runat="server" class="col-form-label col-4 font-weight-bold">Email</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label6" runat="server" class="col-form-label col-4 font-weight-bold">Telpon</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txttelp" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="13"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label7" runat="server" class="col-form-label col-4 font-weight-bold">Alamat</asp:Label>
                                                            <div class="col-8">
                                                                <asp:TextBox ID="txtalamat" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label22" runat="server" class="col-form-label col-4 font-weight-bold">Inisial Koperasi</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtinisialkoperasi" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="10" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label21" runat="server" class="col-form-label col-4 font-weight-bold">Inisial Area</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtinisialarea" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="10" required="required"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-check-inline col-md-12">
                                                            <asp:Label ID="Label20" runat="server" class="col-form-label col-4 font-weight-bold">Inisial Cabang</asp:Label>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtinisialcabang" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="10" required="required" Text="-"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- </div>--%>
                                    </div>
                                    <%-- -- END DATA CABANG--%>
                                    <%-- --Data Perizinan--%>
                                    <div class="col-lg-6">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA PERIZINAN</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label9" runat="server" class="col-form-label col-4 font-weight-bold">Izin Cabang</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtizin" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label3ccc" runat="server" class="col-form-label col-4 font-weight-bold">Tanggal Izin</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox CssClass="form-control" ID="txttglizin" TextMode="DateTime" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- --End Data Perizinan--%>
                                    <%-- -- DATA PK Bulanan--%>
                                    <div class="col-lg-6">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA SURAT PK BULANAN</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label14" runat="server" class="col-form-label col-4 font-weight-bold">Nama Tanda Tangan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtpkttd" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="60" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label11" runat="server" class="col-form-label col-4 font-weight-bold">Jabatan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtpkbagian" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- -- END DATA PK Bulanan--%>
                                    <%-- -- DATA PK Mingguan--%>
                                    <div class="col-lg-6">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA SURAT PK MINGGUAN</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label24" runat="server" class="col-form-label col-4 font-weight-bold">Nama Tanda Tangan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtpkmingguan" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="60" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label25" runat="server" class="col-form-label col-4 font-weight-bold">Jabatan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtpkjabatan" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- -- END DATA PK Mingguan--%>
                                    <%-- --Data SURAT PERINGATAN--%>
                                    <div class="col-lg-6">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA SURAT PERINGATAN</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label12" runat="server" class="col-form-label col-4 font-weight-bold">Nama Tanda Tangan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtsp" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label15" runat="server" class="col-form-label col-4 font-weight-bold">Jabatan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtspbagian" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- --End Data SURAT PERINGATAN--%>
                                    <%-- --Data KARTU ANGSURAN--%>
                                    <div class="col-lg-6">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA KARTU ANGSURAN</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label27" runat="server" class="col-form-label col-4 font-weight-bold">Nama Tanda Tangan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtkartu" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label28" runat="server" class="col-form-label col-4 font-weight-bold">Jabatan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtkartubagian" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label60" runat="server" class="col-form-label col-4 font-weight-bold">Note Mingguan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtnbmingguan" CssClass="form-control" runat="server" data-style="btn-custom"  TextMode="MultiLine" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label61" runat="server" class="col-form-label col-4 font-weight-bold">Note Bulanan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtnbbulanan" CssClass="form-control" runat="server" data-style="btn-custom"  TextMode="MultiLine" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- --End Data KARTU ANGSURAN--%>
                                    <%-- --Data TANDA TERIMA--%>
                                    <div class="col-lg-6">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA TANDA TERIMA KEANGGOTAAN</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label29" runat="server" class="col-form-label col-4 font-weight-bold">Nama Tanda Tangan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtterima" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label30" runat="server" class="col-form-label col-4 font-weight-bold">Jabatan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txtterimabagian" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- --End Data TANDA TERIMA--%>
                                    <%-- --Data SURAT TUGAS--%>
                                    <div class="col-lg-6">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA SURAT TUGAS</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label16" runat="server" class="col-form-label col-4 font-weight-bold">Nama Tanda Tangan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txttugas" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label17" runat="server" class="col-form-label col-4 font-weight-bold">Jabatan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txttugasbagian" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label26" runat="server" class="col-form-label col-4 font-weight-bold">Alamat</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txttugasalamat" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="150" TextMode="MultiLine" required="required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- --End Data SURAT TUGAS--%>
                                    <%-- --Data SURAT TARIK HIDDEN--%>
                                    <div class="col-lg-6" runat="server" visible="false">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA SURAT TARIK</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label18" runat="server" class="col-form-label col-4 font-weight-bold">Nama Tanda Tangan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txttarik" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label19" runat="server" class="col-form-label col-4 font-weight-bold">Jabatan</asp:Label>
                                                        <div class="col-8">
                                                            <asp:TextBox ID="txttarikbagian" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="30" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- --End Data SURAT TARIK--%>
                                </div>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />
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
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
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