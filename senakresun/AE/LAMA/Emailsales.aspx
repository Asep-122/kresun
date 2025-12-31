<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emailsales.aspx.cs" Inherits="Emailsales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Email Karyawan</title>
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
        function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: t,
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 500,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 10); //Delete
                        }


                    }
                });
            }, 0);

        }
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
        function Pencariannull() {

            setTimeout(function () {
                swal(
                    {
                        title: "Data yang Anda masukan tidak sesuai atau tidak ada dalam Database. Silahkan cari kembali dengan kode yang berbeda.",
                        text: "Note : Coba masukan kode cabang Anda, jika tetap tidak ada. Coba laporkan ke IT Development",
                        type: "warning",
                        position: "top-end",
                        //   icon: "success",
                        showConfirmButton: true,
                        timer: 100000
                        //title: errMsg,
                        //confirmButtonClass: 'btn btn-confirm mt-2'
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


            <%--{
                $('#<%=txtketeranganJaminan.ClientID%>').prop('maxlength', 250);

            }--%>

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

                var table = $('#GRData2').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });

                var table = $('#GRData3').dataTable({
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

            $("#txtkaryawan").autocomplete({
                source: function (request, response) {
                    var list = document.getElementById("rbJnsKaryawan"); 
                    var inputs = list.getElementsByTagName("input");
                    var selected;
                    for (var i = 0; i < inputs.length; i++) {
                        if (inputs[i].checked) {
                            selected = inputs[i];
                            break;
                        }
                    };
                    //console.log(selected.value);
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Emailsales.aspx/GetNoKaryawan",
                        data: "{'InputText':'" + document.getElementById('txtkaryawan').value + "','InputText2':'" + selected.value + "','SessionBranchID':'<%=SessBranchID%>','SessionBagian':'" + document.getElementById('HDNBagian').value + "','SessKoperasiID':'<%=SessKoperasiID%>'}",
                        dataType: "json",
                        cache: false,
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[0],
                                    hdnkaryawan: item[1]
                                }
                            });

                            $("[id$=hdnkaryawan]").val('');

                            response(found);
                            if (!$.trim(obj)) {
                                document.getElementById('txtkaryawan').value = '';
                                Pencariannull();
                                //alert("Data yang Anda masukan tidak sesuai atau tidak ada dalam Database. Silahkan cari kembali, coba masukan kode cabang Anda di awal.");
                            }
                            //else {
                            //    alert("What follows is not blank: " + obj);
                            //}
                        },

                        error: function (textStatus, response) {
                            console.log("enggamasuk");
                            alert(textStatus);
                        },
                        failure: function (response) {
                            console.log("enggamasuk2");
                            alert(response.responseText);
                        }
                        
                    });
                },
                select: function (e, i) {
                    $("[id$=hdnkaryawan]").val(i.item.hdnkaryawan);
                    //console.log(selected.value + "tes");
                    //console.log(document.getElementById('hdnkaryawan').value);
                    //if ($("[id$=hdnkaryawan]").val(i.item.hdnkaryawan) == null) {

                    //    console.log("enggamasuk3");
                    //} else {

                    //    console.log("enggamasuk4");
                    //}
                },
                minLength: 3
            });

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

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
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="Emailsales.aspx">Email Karyawan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Email Karyawan</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="card-box ribbon-box" >
                                    <div class="ribbon ribbon-custom">Search Type Data</div>
                                    <p class="m-b-0">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <asp:TextBox CssClass="form-control" ID="txtkodecabang" Visible="false" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnkaryawan" runat="server" />
                                                            <asp:HiddenField ID="HDNBagian" runat="server" />
                                                            <%--<asp:HiddenField ID="HdnWilayah" runat="server" />--%>
                                                            <div class="form-group" style="margin-bottom: 0rem;">
                                                                <%--<div class="form-group col-md-12">--%>
                                                                    <label for="inputCity" class="col-form-label" style="font-weight:bold">Choice Category Karyawan</label>
                                                                    <asp:RadioButtonList ID="rbJnsKaryawan" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="Group1_CheckedChanged" AutoPostBack="true" CellPadding="10" CellSpacing="2">
                                                                        <asp:ListItem Text="&nbsp;Manager Cabang" Value="Manager" />
                                                                        <asp:ListItem Text="&nbsp;Kepala Unit" Value="KU" />
                                                                        <asp:ListItem Text="&nbsp;Sales" Value="Sales" />
                                                                    </asp:RadioButtonList>
                                                                <%--</div>--%>
                                                            </div>
                                                            <div class="form-group" runat="server" id="pnlsearckaryawan" visible="false">
                                                                <label for="inputState" class="col-form-label" style="font-weight:bold">Kode Nik Karyawan</label>
                                                                <asp:TextBox CssClass="form-control" ID="txtkaryawan" runat="server" required="required"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group" runat="server" id="pnlsearcbtn" visible="false">
                                                                <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearchx_Click" />
                                                                <label for="inputState" class="col-form-label" style="font-weight:bold">*Note : Jika Data Karyawan tidak ada di Table Data di bawah, silahkan cari di kolom Kode Nik Karyawan di atas. </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                                <div class="card-box table-responsive" runat="server" visible="false" id="pnlniklama">
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Email Karyawan" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box ribbon-box" >
                                    <div class="ribbon ribbon-custom">Data</div>
                                    <p class="m-b-0">
                                    <h1 class="m-t-0 header-title" style="font-weight:900" runat="server" visible="false" id="txtheadersales">Data Account Officer</h1>
                                    <h1 class="m-t-0 header-title" style="font-weight:900" runat="server" visible="false" id="txtheaderku">Data Head Unit</h1>
                                    <h1 class="m-t-0 header-title" style="font-weight:900" runat="server" visible="false" id="txtheadermanager">Data Manager Cabang</h1>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;"  runat="server" id="pnldatakaryawan" visible="false">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" 
                                                ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Kode Sales">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodesales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodeSales")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Marketing">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamasales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nama")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kepala Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblku" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaku")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "emailsales")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aktif">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaktif" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "aktif")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit Data" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                           <%-- <asp:LinkButton ID="btnApproveAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnupdate" runat="server" data-toggle="tooltip" data-placement="left" title="Update Data" CommandName="UPDATEX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-document-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnApproveTeller" runat="server" data-toggle="tooltip" data-placement="left" title="Lanjutkan Ke Teller" CommandName="Lanjutkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-user"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnBatalkanAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Batalkan Antrian" CommandName="Batalkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-thumbs-down"></i></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="GRData2" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"
                                                ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Kode KU">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodesales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodeku")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamasales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nama")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "emailku")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aktif">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaktif" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "aktif")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit Data" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%-- <asp:LinkButton ID="btnApproveAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnupdate" runat="server" data-toggle="tooltip" data-placement="left" title="Update Data" CommandName="UPDATEX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-document-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnApproveTeller" runat="server" data-toggle="tooltip" data-placement="left" title="Lanjutkan Ke Teller" CommandName="Lanjutkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-user"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnBatalkanAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Batalkan Antrian" CommandName="Batalkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-thumbs-down"></i></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="GRData3" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"
                                                ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Kode Manager">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodesales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodemc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamasales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nama")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "emailmc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aktif">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaktif" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "aktif")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center" Font-Bold="true"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit Data" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <%-- <asp:LinkButton ID="btnApproveAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnupdate" runat="server" data-toggle="tooltip" data-placement="left" title="Update Data" CommandName="UPDATEX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-document-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnApproveTeller" runat="server" data-toggle="tooltip" data-placement="left" title="Lanjutkan Ke Teller" CommandName="Lanjutkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-user"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnBatalkanAntrian" runat="server" data-toggle="tooltip" data-placement="left" title="Batalkan Antrian" CommandName="Batalkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-thumbs-down"></i></asp:LinkButton>--%>
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
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="form-group col-md-6" runat="server" visible="false">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Search Marketing</div>
                                        <p class="m-b-0"></p>
                                        <div class="card-box">
                                            <div class="form-group col-md-12">
                                                <asp:TextBox CssClass="form-control" ID="TextBox1" Visible="false" Enabled="false" runat="server" ></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <div class="form-group col-md-12">
                                                    <label for="inputState" class="col-form-label">Kode Nik Sales</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtnikkaryawan" Enabled="true" MaxLength="250" runat="server" ></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12" runat="server" visible="false">
                                                    <asp:Button ID="Button2" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearchx_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-md-6">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">INFORMASI DATA ANGGOTA</div>
                                        <p class="m-b-0"></p>
                                        <div class="card-box">
                                            <div class="form-group col-md-12">
                                                <asp:HiddenField ID="HDkodecabang" runat="server" />
                                                <asp:HiddenField ID="HDkodekoperasi" runat="server" />
                                                <asp:HiddenField ID="HDKodewilayah" runat="server" />
                                                <div class="form-group">
                                                    <asp:Label ID="Label151" runat="server" class="col-form-label">Kode Nik</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtkodenik" Enabled="false" runat="server" ></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label43" runat="server" class="col-form-label">Nama</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtnamasales" Enabled="false" runat="server" ></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" class="col-form-label">Email</asp:Label>
                                                    <asp:TextBox CssClass="form-control" ID="txtemail" runat="server"   MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        <div class="hidden-print mt-4">
                                            <div class="row" style="margin-left: auto;">
                                                <%--<div class="button-list">--%>
                                                    <asp:Button ID="Button1" class="btn btn-success waves-light text-white" type="button" runat="server" Text="BACK" OnClick="btnCancel_Click" Visible="false" />
                                                    <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white"   runat="server" type="button" Text="BACK" OnClick="btnCancel_Click" />
                                                    <asp:Button ID="btnSimpan" CssClass="btn btn-success waves-light text-white float-right" type="button" runat="server" OnClick="btnSimpan_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />
                                                <%--</div>--%>
                                            </div>
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
                <asp:PostBackTrigger ControlID="btnSearch" />
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="GRData2" />
                <asp:PostBackTrigger ControlID="GRData3" />
                
                <asp:PostBackTrigger ControlID="rbJnsKaryawan" />

                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
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
