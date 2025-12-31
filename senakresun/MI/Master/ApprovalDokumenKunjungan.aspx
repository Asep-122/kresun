<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalDokumenKunjungan.aspx.cs" Inherits="MI_Master_Approval_Dokumen_Kunjungan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Create Simulasi</title>
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

    <script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

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

        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

        }

        function AlertSuccess(h, t, p, i, fr) {
            console.log(fr)
            setTimeout(function () {


                Swal.fire({
                    title: h,
                    text: t,
                    icon: "success"
                }).then((result) => {
                    if (fr === 'true') {
                        window.location.href = "CreateSimulasi.aspx"
                    }
                });;
            }, 100);

        }

        function sweetAlertCancel(btnBack, errMsg) {
            if (btnBack.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnBack.dataset.confirmed = false;
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
                }).then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btnBack.dataset.confirmed = true;
                    redirect();

                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
            }
        }

        function AlertRequest(btnRequest, errMsg) {
            if (btnRequest.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnRequest.dataset.confirmed = false;
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
                }).then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btnRequest.dataset.confirmed = true;
                    btnRequest.click();

                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
            }
        }


        function redirect() {
            location.href = 'CreateMINew.aspx ';
        }

        function WarningDate(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

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

            var region = '<%=CultureName%>';


            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {


                if (txt == 44 && region == 'id-ID') {
                    return true;
                }
                if (txt == 46 && region != 'id-ID') {
                    return true;
                }
                else {
                    //alert("Input is Number only")
                    return false;
                }

            }
            return true;
        }

        //function to add commas to textboxes
        function Comma(Num) {
            console.log('<%=CultureName%>');
            var splitnya;
            var koma;

            var region = '<%=CultureName%>';

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
            }
            else {
                splitnya = ',';
                koma = '.';
            }

            Num += '';
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            x = Num.split(koma);
            x1 = x[0];
            x2 = x.length > 1 ? koma + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + splitnya + '$2');


            return x1 + x2;
        }

        function checkTextField1(field) {

            var region = '<%=CultureName%>';
            var splitnya;
            var dolarnya;
            var koma;

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
                dolarnya = '$1.';
            }
            else {
                splitnya = ',';
                koma = '.';
                dolarnya = '$1,';
            }

            if (field.value == '' || field.value == 0 || field.value == koma) {
                field.value = 0;
            }


            var nilai = field.value.split(splitnya).join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split(koma); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, dolarnya).split("").reverse().join("");
                output = parts.join(koma);
            }
            field.value = output;
        }

        function Alertfailed(h, t, p, i) {
            setTimeout(function () {
                Swal.fire({
                    title: h,
                    text: t,
                    icon: "error"
                });
            }, 100);

        }

        function sweetAlertsave(btnrequest, errMsg) {
            if (btnrequest.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnrequest.dataset.confirmed = false;
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
                }).then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btnrequest.dataset.confirmed = true;
                    btnrequest.click();

                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
            }
        }

        function sweetAlertdelete(btndelete, errMsg) {
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
                }).then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btndelete.dataset.confirmed = true;
                    btndelete.click();

                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
            }
        }
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            /* Resize();*/
            $('#txttgllahir').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak1').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak2').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtmtpensiun').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak4').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttglrealisasi').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
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
                var table = $('#GrdApp').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GrdReject').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GrdPending').dataTable({
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
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
        function kepo2(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
    </script>
    <style>
        .Spaceradio label {
            margin-left: 10px;
        }

        .aturbawah {
            margin-bottom: 0.1rem;
        }

        .aturkiri {
            margin-left: 0;
        }

        .tabdetil {
            width: 187px;
            /*right: 241px;*/
            /*//position: fixed;*/
        }

        .aturbawahpinjaman {
            margin-bottom: -45px
        }

        .texttengah {
            text-align: center;
        }

        .kiritengah {
            align-self: center;
        }

        .auto-style1 {
            left: 0px;
            top: 0px;
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
                <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>--%>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Simulasi</a></li>
                                        <li class="breadcrumb-item"><a href="#">Master</a></li>
                                        <li class="breadcrumb-item"><a href="#">Approval Dokumen Kunjungan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Approval Dokumen Kunjungan</h4>
                            </div>
                        </div>
                    </div>

                    <!-- Panel Front Gridview -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="col-md-6" id="idcari" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-form-label">Search By</label>
                                            <div class="form-group col-md-6">
                                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                    <asp:ListItem Value=" and NoSimulasi ">No Simulasi</asp:ListItem>
                                                    <asp:ListItem Value=" and Nopen ">Nopen</asp:ListItem>
                                                    <asp:ListItem Value=" and NamaDebitur ">Nama</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                                                <br>
                                                <%--<asp:Button ID="lnkCari" class="btn btn-success waves-light text-white" runat="server" Text="Cari" OnClick="lnkCari_Click" />--%>
                                            </div>
                                        </div>
                                    </div>

                                    <h4 class="m-t-0 header-title" id="idjudulsemua" runat="server" visible="true">Daftar Simulasi</h4>
                                    <div class="form-row" id="divtotalrequest" runat="server" visible="true">


                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="KodeSimulasi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodeSimulasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeSimulasi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NoSimulasi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrequest" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoSimulasi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nopen">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnopen" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NomorPensiun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaPensiun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plafond">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplafond" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Plafon", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plafond Max">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplafondmax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlafondMax", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Detail">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewDokumen" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Lihat Dokumen Kunjungan" CommandName="DOKUMEN" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-eye"></i></asp:LinkButton>
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

                    <asp:Panel ID="pnlData" Visible="false" runat="server">
                        <div class="row">

                            <div class="col-lg-12">
                                <%--<div class="col-lg-12">--%>
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Data</div>
                                    <p class="text-muted font-14 m-b-30"></p>
                                    <div class="form-row">

                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <label for="txtIdPermohonan" class="col-3 col-form-label">No Simulasi</label>
                                                <div class="col-8">
                                                    <asp:TextBox ID="txtNoSimulasi" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputStates" class="col-3 col-form-label">Plafon</label>
                                                <div class="col-8">
                                                    <asp:TextBox ID="txtPlafon" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <div class="form-group">
                                                <label for="inputStates" class="col-3 col-form-label">Nomor Pensiun</label>
                                                <div class="col-8">
                                                    <asp:TextBox ID="txtNopen" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="inputStates" class="col-3 col-form-label">Nama Nasabah</label>
                                                <div class="col-8">
                                                    <asp:TextBox ID="txtNama" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <%--</div>--%>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlApproval" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Approval Kunjungan</div>
                                    <p class="text-muted font-14 m-b-30"></p>

                                    <div class="row">
                                        <!-- KIRI: Note dan Tombol Dokumen -->
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="txtNote" class="col-form-label">Note</label>
                                                <asp:TextBox
                                                    ID="txtNote"
                                                    Visible="false"
                                                    runat="server"
                                                    CssClass="form-control"
                                                    TextMode="MultiLine"
                                                    Rows="4"
                                                    Columns="50"
                                                    onkeyup="validateNote();" />
                                                <asp:RequiredFieldValidator
                                                    ID="rfvNote"
                                                    runat="server"
                                                    ControlToValidate="txtNote"
                                                    ErrorMessage="Wajib memberikan Note"
                                                    Display="Dynamic"
                                                    ForeColor="Red"
                                                    CausesValidation="true"
                                                    ValidationGroup="ActionGroup" />
                                            </div>

                                            <div class="form-group mt-3">
                                                <asp:Button ID="btnViewDokumenNasabah" CssClass="btn btn-success waves-light text-white" runat="server" Text="Dokumen Nasabah" OnClick="btnViewDokumenNasabah_Click" />
                                                <asp:Button ID="btnViewAplikasiNasabah" CssClass="btn btn-success waves-light text-white" runat="server" Text="Aplikasi Nasabah" OnClick="btnViewAplikasiNasabah_Click" />
                                                <asp:Button ID="btnHide" CssClass="btn btn-success waves-light text-white" runat="server" Text="Hide" OnClick="btnHide_Click" />
                                            </div>
                                        </div>

                                        <!-- KANAN: Grid History -->
                                        <div class="col-md-6">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-form-label">History Approval</asp:Label>
                                            <asp:GridView ID="GRDataUploadHistory" runat="server"
                                                CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"
                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Upload By">
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUploadBy" runat="server" Text='<%# Eval("UploadBy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatusApproval" runat="server" Text='<%# Eval("StatusApprovalDesc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note Admin">
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoteAdmin" runat="server" Text='<%# Eval("NoteAdmin") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date">
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprovalDate" runat="server" Text='<%# Eval("ApprovalDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <!-- Tombol Approve/Revise/Reject/Back -->
                                    <div class="form-group mt-3">
                                        <asp:Button ID="btnApprove" Visible="false" CssClass="btn btn-success waves-light text-white" runat="server" Text="Approve" CausesValidation="true" ValidationGroup="ActionGroup" OnClick="btnApprove_Click" />
                                        <asp:Button ID="btnRevise" Visible="false" CssClass="btn btn-success waves-light text-white" runat="server" Text="Revise" CausesValidation="true" ValidationGroup="ActionGroup" OnClick="btnRevise_Click" />
                                        <asp:Button ID="btnReject" Visible="false" CssClass="btn btn-success waves-light text-white" runat="server" Text="Reject" CausesValidation="true" ValidationGroup="ActionGroup" OnClick="btnReject_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                    </div>

                                    <!-- Panel PDF Dokumen -->
                                    <asp:Panel ID="PanelPDFDokumen" CssClass="col-lg-12 mt-3" runat="server" Visible="false">
                                        <iframe id="iframePDFDokumenNasabah" runat="server" visible="false" width="100%" height="900px" style="border: none;"></iframe>
                                        <iframe id="iframePDFAplikasiNasabah" runat="server" visible="false" width="100%" height="900px" style="border: none;"></iframe>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnViewDokumenNasabah" />
                <asp:PostBackTrigger ControlID="btnViewAplikasiNasabah" />
                <%--<asp:PostBackTrigger ControlID="btnHide" />--%>
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="GRData" />
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
<script type="text/javascript">
    $(document).ready(function () {
        $('#datatable').dataTable();
    });

    function validateNote() {
        for (var i = 0; i < Page_Validators.length; i++) {
            var v = Page_Validators[i];
            if (v && (v.id.indexOf("rfvNote") !== -1 || v.id.indexOf("revNote") !== -1)) {
                ValidatorValidate(v);
            }
        }
    }
</script>
</html>

