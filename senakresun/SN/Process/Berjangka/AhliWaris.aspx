<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AhliWaris.aspx.cs" Inherits="PN_AhliWaris" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Ahli Waris</title>
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


    <style type="text/css">
        .modalPopup {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .xcenter {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 160px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

        .ui-autocomplete {
            max-height: 500px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */
        * html .ui-autocomplete {
            height: 500px;
        }

        .ui-autocomplete-loading {
            background: white url("/assets/images/Cube-1s-30px.gif") right center no-repeat;
        }
    </style>

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
                            setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
                        }

                        if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
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

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        }

        //numeric 1
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

        //numeric 2
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

        <%--function CheckFileExistence() {

            var filePath = document.getElementById('<%= this.fileupload.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto belum di pilih!");


                return false;
            }
        }--%>
        //numeric 3
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
        var s = 0
        var v = 0
        var x = 0;
        var y = 0;
        var z = 0;
        function calc(obj) {
            var e = obj.id.toString();
            if (e == 'txttransAP') {
                x = Number(obj.value);
                y = Number(document.getElementById('txtkursstd').value);
                v = Number(document.getElementById('txtkursstdbank').value);
            }
            else if (e == '') {
                x = Number(document.getElementById('txttransAP').value);
                y = Number(obj.value);
            }
            z = x * y;
            s = z / v;
            document.getElementById('txtjmlhGL').value = z;
            document.getElementById('hfjmlrp').value = z;
            document.getElementById('txtjmlhCL').value = s;
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

        function cancelalert(errMsg) {

            swal({
                title: errMsg,
                // text: 'Your message has been succesfully sent',
                confirmButtonClass: 'btn btn-confirm mt-2',
                type: 'warning'

            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    alert("CEKDATA");
                    setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                });
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
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $('#Txttglsetoran').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgltrn').datepicker({
                format: 'dd-mm-yyyy',
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
                var tableX = $('#GRRP').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var tableX = $('#GRTP').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var tableX = $('#Grduangmuka').dataTable({
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

            $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "AhliWaris.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[2],
                                    hdncustomer: item[1],
                                    hdnidpermohonan: item[0]
                                }
                            });

                            $("[id$=hdncustomer]").val('');
                            $("[id$=hdnidpermohonan]").val('');

                            response(found);
                        },

                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hdncustomer]").val(i.item.hdncustomer);
                    $("[id$=hdnidpermohonan]").val(i.item.hdnidpermohonan);
                },
                minLength: 3
            });
        }

        var validFiles = ["png", "jpg", "jpeg"];
        function CheckExt1(obj) {
            var source = obj.value;
            var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
            for (var i = 0; i < validFiles.length; i++) {
                if (validFiles[i] == ext)
                    break;
            }
            if (i >= validFiles.length) {
                alert("Jenis Gambar Harus ber Extention :\n\n" + validFiles.join(", "));
                $(".fileuploadktpwaris1").val = "";
                obj.value = "";
                //fileuploadktp.FileName
                return false;
            }
        }
        function CheckExt1(obj) {
            var validFiles = ["png", "jpg", "jpeg", "pdf"];
            var maxFileSize = 2097152; // 4MB -> 4 * 1024 * 1024
            var fileUpload = $('#fileuploadktpwaris1');
            var source = obj.value;
            var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
            for (var i = 0; i < validFiles.length; i++) {
                if (validFiles[i] == ext)
                    break;
            }
            if (fileUpload.val() == '') {
                return false;
            }
            else {
                if (fileUpload[0].files[0].size < maxFileSize) {
                    if (i >= validFiles.length) {
                        alert("Jenis File Harus ber Extention :\n\n" + validFiles.join(", "));
                        $(".fileuploadktpwaris1").val = "";
                        obj.value = "";
                        //fileuploadktp.FileName
                        return false;
                    } else {
                        $('#btnUploadktp1').prop('disabled', false);
                        $('#lbl_uploadMessage').text('')
                        return true;
                    }
                } else {
                    //$('#fileuploadktpwaris1').empty;
                    document.getElementById("fileuploadktpwaris1").value = '';
                    $('#lbl_uploadMessage').text('Ukuran File Tidak boleh lebih dari 2MB !')
                    return false;
                }
            }
        }
        function CheckExt2(obj) {
            var validFiles = ["png", "jpg", "jpeg", "pdf"];
            var maxFileSize = 2097152; // 4MB -> 4 * 1024 * 1024
            var fileUpload = $('#fileuploadktpwaris2');
            var source = obj.value;
            var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
            for (var i = 0; i < validFiles.length; i++) {
                if (validFiles[i] == ext)
                    break;
            }
            if (fileUpload.val() == '') {
                return false;
            }
            else {
                if (fileUpload[0].files[0].size < maxFileSize) {
                    if (i >= validFiles.length) {
                        alert("Jenis File Harus ber Extention :\n\n" + validFiles.join(", "));
                        $(".fileuploadktpwaris2").val = "";
                        obj.value = "";
                        //fileuploadktp.FileName
                        return false;
                    } else {
                        $('#btnUploadktp2').prop('disabled', false);
                        $('#lbl_uploadMessage2').text('')
                        return true;
                    }
                } else {
                    //$('#fileuploadktpwaris1').empty;
                    document.getElementById("fileuploadktpwaris2").value = '';
                    $('#lbl_uploadMessage2').text('Ukuran Gambar Tidak boleh lebih dari 2MB !')
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                //Shows the modal popup - the update progress
                //alert("1");
                var popup = $find('<%= modalPopup.ClientID %>');
                if (popup != null) {
                    popup.show();
                }
            }


            function EndRequestHandler(sender, args) {
                //Hide the modal popup - the update progress
                //alert("2");
                var popup = $find('<%= modalPopup.ClientID %>');
                if (popup != null) {
                    popup.hide();
                }
            }
        </script>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <div class="xcenter">
                    <asp:Image ID="Image1" ImageUrl="/assets/images/loadx.gif" AlternateText="Processing" runat="server" Width="136px" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
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
                                        <li class="breadcrumb-item"><a href="#">Simpanan Nasabah</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="AhliWaris.aspx">Ahli Waris</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Ahli Waris</h4>
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
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Change Ahli Waris" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">Ahli Waris</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No Permohonan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Permohonan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglpermohonan","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Nasabah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamaNasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="KTP Ahli Waris 1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblktpwaris1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoKTPAhliWaris1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Ahli Waris 1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamawaris1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaAhliWaris1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="KTP Ahli Waris 2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblktpwaris2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoKTPAhliWaris2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Ahli Waris 2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamawaris2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaAhliWaris2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btndelete" runat="server" Visible="false" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin merubah data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cabang" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_cabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Billyed" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobilyed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobilyed")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Setoran" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobuktisetoran")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBayar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bayar")%>'></asp:Label>
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
                    <!-- Panel Edit -->

                    <asp:Panel ID="Pnlcustomer" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">Pencarian Nasabah</div>
                                            <p class="text-muted font-14 m-b-30"></p>
                                            <div class="form-group col-md-12">
                                                <label class="col-form-label">Entry Data Nasabah</label>
                                                <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnladd" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="col-lg-6" >
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Entry Data</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoRekening" runat="server" />
                                                <div class="form-group">
                                                    <asp:HiddenField ID="hdncustomer" runat="server" />
                                                    <asp:HiddenField ID="hdnidpermohonan" runat="server" />
                                                    <label for="inputpid" class="col-3 col-form-label">No. Permohonan</label>
                                                    <asp:TextBox ID="txtnobukti" CssClass="form-control " runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Change</label>
                                                    <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <asp:DropDownList ID="ddlpajak" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlKoperasi" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlcabang" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Diri Nasabah</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:HiddenField ID="HDNoSebelumnya" runat="server" />
                                                <asp:HiddenField ID="HDState" runat="server" />
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" runat="server" class="col-form-label">Nomor Nasabah</asp:Label>
                                                    <asp:TextBox ID="txtnonasabah" Enabled="false" CssClass="form-control" MaxLength="30" onkeypress="return isnumerickey(event);" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label80" runat="server" class="col-form-label">Nomor Identitas KTP (NIK)</asp:Label>
                                                    <asp:TextBox ID="txtidentitas" CssClass="form-control" MaxLength="16" onkeypress="return isnumerickey(event);" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" class="col-form-label">Nama Lengkap</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtnamanasabah"  MaxLength="70" runat="server" Text="-"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label7" runat="server" class="col-form-label">Alamat </asp:Label>
                                                    <asp:TextBox ID="txtalamat" CssClass="form-control" Text="-" MaxLength="100" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-row" >
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label23" runat="server" class="col-form-label">Kabupaten</asp:Label>
                                                        <asp:TextBox ID="txtkabupaten"  CssClass="form-control" Text="-" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <asp:Label ID="Label25" runat="server" class="col-form-label">RT</asp:Label>
                                                        <asp:TextBox ID="txtrtktp"  CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);" MaxLength="3" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <asp:Label ID="Label26" runat="server" class="col-form-label">RW</asp:Label>
                                                        <asp:TextBox ID="txtrwktp"  CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);" MaxLength="3" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-row" >
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label27" runat="server" class="col-form-label">Kelurahan</asp:Label>
                                                        <asp:TextBox ID="txtkelurahanktp"  CssClass="form-control" Text="-" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label28" runat="server" class="col-form-label">Kecamatan</asp:Label>
                                                        <asp:TextBox ID="txtkecamatanktp"  CssClass="form-control" Text="-" MaxLength="50" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-row"  >
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label29" runat="server" class="col-form-label">Provinsi</asp:Label>
                                                        <asp:DropDownList ID="ddlprovinsiktp" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <asp:Label ID="Label30" runat="server" class="col-form-label">Kode Pos</asp:Label>
                                                        <asp:TextBox ID="txtkodeposktp" CssClass="form-control" Text="0" MaxLength="6" onkeypress="return isnumerickey(event);" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Ahli Waris lama</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" class="col-form-label">Nama Ahli Waris I</asp:Label>
                                                    <asp:TextBox ID="txtnamawaris11" Enabled="true" CssClass="form-control" MaxLength="100" Text="-" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label4" runat="server" class="col-form-label">No KTP Ahli Waris I</asp:Label>
                                                    <asp:TextBox ID="txtktpwaris11" Enabled="true" CssClass="form-control" MaxLength="16" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label5" runat="server" class="col-form-label">Path File KTP Waris 1</asp:Label>
                                                    <asp:Button ID="btnUploadktp11" Text="Upload" runat="server" OnClick="UploadFileKTPwaris1" Visible="false" />
                                                    <asp:Button ID="btndeletewaris11" Text="Batal Upload" runat="server" OnClick="clearFileInputField1" Visible="false" />
                                                    <asp:TextBox ID="txtpathktpwaris11" CssClass="form-control" Enabled="false" runat="server" MaxLength="400"></asp:TextBox>
                                                    <asp:Image ID="imgCroppedwaris11" runat="server" Visible="false" />
                                                    <asp:Label ID="lbl_uploadMessage11" runat="server" Style="color: red;" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label6" runat="server" class="col-form-label">Alamat Ahli Waris I </asp:Label>
                                                    <asp:TextBox ID="txtalamatwaris11" CssClass="form-control" Text="-" MaxLength="100" runat="server"></asp:TextBox>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label8" runat="server" class="col-form-label">Nama Ahli Waris II</asp:Label>
                                                        <asp:TextBox ID="txtnamawaris22" Enabled="true" CssClass="form-control" MaxLength="50" Text="-" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label9" runat="server" class="col-form-label">No KTP Ahli Waris II</asp:Label>
                                                        <asp:TextBox ID="txtktpwaris22" Enabled="true" CssClass="form-control" MaxLength="16" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label10" runat="server" class="col-form-label">Path File KTP Waris 2</asp:Label>
                                                        <asp:Button ID="btnUploadktp22" Text="Upload" runat="server" OnClick="UploadFileKTPwaris2" Visible="false" />
                                                        <asp:Button ID="btndeletewaris22" Text="Batal Upload" runat="server" OnClick="clearFileInputField2" Visible="false" />
                                                        <asp:TextBox ID="txtpathktpwaris22" CssClass="form-control" Enabled="false" runat="server" MaxLength="400"></asp:TextBox>
                                                        <asp:Image ID="imgCroppedwaris22" runat="server" Visible="false" />
                                                        <asp:Label ID="lbl_uploadMessage22" runat="server" Style="color: red;" />
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label11" runat="server" class="col-form-label">Alamat Ahli Waris II </asp:Label>
                                                        <asp:TextBox ID="txtalamatwaris22" CssClass="form-control" Text="-" MaxLength="100" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Ahli Waris Baru</div>
                                                <p class="text-muted font-14 m-b-30"></p>
                                                <asp:HiddenField ID="HiddenField5" runat="server" />
                                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                                <asp:HiddenField ID="HiddenField7" runat="server" />
                                                <div class="form-group">
                                                    <asp:Label ID="Label18" runat="server" class="col-form-label">Nama Ahli Waris I</asp:Label>
                                                    <asp:TextBox ID="txtnamawaris1" Enabled="true" CssClass="form-control" MaxLength="100" Text="-" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label19" runat="server" class="col-form-label">No KTP Ahli Waris I</asp:Label>
                                                    <asp:TextBox ID="txtktpwaris1" Enabled="true" CssClass="form-control" MaxLength="16" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:FileUpload ID="fileuploadktpwaris1" runat="server" onchange="CheckExt1(this)" />
                                                    <asp:Button ID="btnUploadktp1" Text="Upload" runat="server" OnClick="UploadFileKTPwaris1" Visible="false" />
                                                    <asp:Button ID="btndeletewaris1" Text="Batal Upload" runat="server" OnClick="clearFileInputField1" Visible="false" />
                                                    <asp:TextBox ID="txtpathktpwaris1" CssClass="form-control" Enabled="false" runat="server" MaxLength="400"></asp:TextBox>
                                                    <asp:Image ID="imgCroppedwaris1" runat="server" Visible="false" />
                                                    <asp:Label ID="lbl_uploadMessage" runat="server" Style="color: red;" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label20" runat="server" class="col-form-label">Alamat Ahli Waris I </asp:Label>
                                                    <asp:TextBox ID="txtalamatwaris1" CssClass="form-control" Text="-" MaxLength="100" runat="server"></asp:TextBox>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label22" runat="server" class="col-form-label">Nama Ahli Waris II</asp:Label>
                                                        <asp:TextBox ID="txtnamawaris2" Enabled="true" CssClass="form-control" MaxLength="50" Text="-" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label24" runat="server" class="col-form-label">No KTP Ahli Waris II</asp:Label>
                                                        <asp:TextBox ID="txtktpwaris2" Enabled="true" CssClass="form-control" MaxLength="16" Text="0" runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:FileUpload ID="fileuploadktpwaris2" runat="server" onchange="CheckExt2(this)" />
                                                        <asp:Button ID="btnUploadktp2" Text="Upload" runat="server" OnClick="UploadFileKTPwaris2" Visible="false" />
                                                        <asp:Button ID="btndeletewaris2" Text="Batal Upload" runat="server" OnClick="clearFileInputField2" Visible="false" />
                                                        <asp:TextBox ID="txtpathktpwaris2" CssClass="form-control" Enabled="false" runat="server" MaxLength="400"></asp:TextBox>
                                                        <asp:Image ID="imgCroppedwaris2" runat="server" Visible="false" />
                                                        <asp:Label ID="lbl_uploadMessage2" runat="server" Style="color: red;" />
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label31" runat="server" class="col-form-label">Alamat Ahli Waris II </asp:Label>
                                                        <asp:TextBox ID="txtalamatwaris2" CssClass="form-control" Text="-" MaxLength="100" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                    </asp:Panel>

                    <asp:Panel ID="Pnlfooter4" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <div class="card-box table-responsive">
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Delete" OnClick="btndelete_click" Enabled="true" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnCancel_Click" Text="Close" Width="65px" />
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
                <%--           <asp:PostBackTrigger ControlID="btnNext2" />--%>
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="btnUploadktp1" />
                <asp:PostBackTrigger ControlID="btnUploadktp2" />
                <asp:PostBackTrigger ControlID="btndeletewaris1" />
                <asp:PostBackTrigger ControlID="btndeletewaris2" />
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



