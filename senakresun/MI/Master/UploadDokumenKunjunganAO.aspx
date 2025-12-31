<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadDokumenKunjunganAO.aspx.cs" Inherits="MI_Master_UploadDokumenKunjunganAO" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pencairan Kredit</title>
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
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }

        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };

        function IsValid(args) {
            if (args.value.length == 0) {
                return false;
            }
            else {
                return true;
            }
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

        function Simpan() {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
                        type: "success",
                        position: "top-end",
                        //   icon: "success",
                        showConfirmButton: false,
                        timer: 2000
                        //title: errMsg,
                        //confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);
        }
        function sweetAlerttolak(btnSimpanTolakan, errMsg) {
            if (btnSimpanTolakan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSimpanTolakan.dataset.confirmed = false;
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
                        btnSimpanTolakan.dataset.confirmed = true;
                        btnSimpanTolakan.click();

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
                        btnSimpan.disabled = true;
                        return true;

                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
            }
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

        function Comma(Num) { //function to add commas to textboxes

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


        $(document).ready(InIEvent);
        function InIEvent() {

            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahirpasangan').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('#txttglrealisasi').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgltagih').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtglskpensiun').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtmtpensiun').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
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
            $('#txttgllahirpp').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiraw').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('#txttgltransfer').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('.selectpicker').selectpicker();

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }

    </script>
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

                <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>--%>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Kredit Pensiun</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="#">Uplaod Dokument Kunjungan Marketing</a></li>
                                        <%--<li class="breadcrumb-item"><a href="UploadDokumenKunjunganAO.aspx">Uplaod Dokument Kunjungan Marketing</a></li>--%>
                                        <%--<li class="breadcrumb-item">
                                            <asp:LinkButton 
                                                ID="breadCrumbUploadDokumen" 
                                                runat="server" 
                                                CssClass="breadcrumb-link"
                                                OnClick="breadCrumbUploadDokumen_Click">
                                                Upload Dokument Kunjungan Marketing
                                            </asp:LinkButton>
                                        </li>--%>
                                    </ol>
                                </div>
                                <h4 class="page-title">Uplaod Dokument Kunjungan Marketing</h4>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="pnlData" Visible="true" runat="server">
                        <div class="row">

                            <div class="col-lg-12">
                                <div class="col-lg-12">
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
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlUploadDokument" Visible="true" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Upload Dokument</div>
                                    <p class="text-muted font-14 m-b-30"></p>

                                    <div class="row">
                                        <!-- KIRI: Input File -->
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="LabelPdf" runat="server" CssClass="col-form-label">File PDF Dokumen Nasabah</asp:Label>
                                                <asp:FileUpload ID="fileuploadpdf_dokumen_nasabah" runat="server" accept=".pdf,application/pdf" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvPdf1" runat="server" ControlToValidate="fileuploadpdf_dokumen_nasabah" ErrorMessage="Wajib Melampirkan Dokumen Nasabah" Display="Dynamic" ForeColor="Red" ValidationGroup="UploadGroup" />
                                                <asp:TextBox ID="txtPdfDokumenNasabah" CssClass="form-control mt-2" Enabled="false" runat="server" MaxLength="400">-</asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="LabelPdf2" runat="server" CssClass="col-form-label">File PDF Dokumen Aplikasi</asp:Label>
                                                <asp:FileUpload ID="fileuploadpdf_dokumen_aplikasi" runat="server" accept=".pdf,application/pdf" CssClass="form-control" />
                                                <asp:TextBox ID="txtPdfDokumenApliaksi" CssClass="form-control mt-2" Enabled="false" runat="server" MaxLength="400">-</asp:TextBox>
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
                                                            <asp:Label ID="lblUploadBy" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UploadBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatusApproval" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StatusApprovalDesc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note Admin">
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoteAdmin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoteAdmin")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<asp:Label ID="lblplafond" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Plafon", "{0:N2}")%>'></asp:Label>--%>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action Date">
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprovalDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ApprovalDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <!-- Tombol -->
                                    <div class="form-group mt-3">
                                        <asp:Button ID="btnUpload" CssClass="btn btn-custom waves-light text-white" runat="server" Text="Upload" ValidationGroup="UploadGroup" OnClick="btnUpload_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-custom waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>



                </div>
                <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
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

