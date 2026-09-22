<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CreateDeviasi.aspx.cs" Inherits="KP_Process_CreateDeviasi" %>

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
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto&display=swap" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


    <script src="https://cdn.quilljs.com/1.3.6/quill.js"></script>
    <script type="text/javascript">

        var quill;

        document.addEventListener("DOMContentLoaded", function () {
            quill = new Quill('#editor', { theme: 'snow' });
        });



        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 500);
        }

        history.pushState(null, null, location.href);
/*        window.onpopstate = function () {
            history.go(1);
        };
*/
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
                    confirmButtonClass: 'btn btn-confirm',
                    cancelButtonClass: 'btn btn-confirm',
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
                var myEditor = document.querySelector('#editor')
                var html = myEditor.children[0].innerHTML;

                document.getElementById('<%= hdnEditorContent.ClientID %>').value = (html);

                console.log("tester " + html);
                btnSimpan.dataset.confirmed = false;

                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: errMsg,
                    confirmButtonClass: 'btn btn-confirm',
                    cancelButtonClass: 'btn btn-confirm',
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

        function AlertSuccess(h, t, p, i, fr) {
            console.log(fr)
            setTimeout(function () {


                Swal.fire({
                    title: h,
                    text: t,
                    icon: "success"
                }).then((result) => {
                    if (fr === 'true') {
                        window.location.href = "CreateDeviasi.aspx"
                    }
                });;
            }, 100);

        }

        function Alertfailed(h, t, p, i) {
            setTimeout(function () {
                Swal.fire({
                    title: h,
                    text: t,
                    icon: "error"
                });
            }, 200);

        }

        function sweetAlertApproveGrid(btnSimpan, errMsg) {
            if (btnSimpan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                var myEditor = document.querySelector('#editor')
                var html = myEditor.children[0].innerHTML;
                // simpan ke hidden field ASP.NET
                document.getElementById('<%= hdnEditorContent.ClientID %>').value = (html);

                console.log("tester " + html);
                btnSimpan.dataset.confirmed = false;

                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: errMsg,
                    confirmButtonClass: 'btn btn-confirm',
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
                        confirmButtonClass: 'btn btn-confirm'
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
                            setTimeout(function () { <%=btnafterDelete()%>; }, 100); //Delete
                        }


                    }
                });
            }, 0);

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


            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

              <%--  {
                $('#<%=txtketeranganJaminan.ClientID%>').prop('maxlength', 250);
                
            }--%>
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



            $(".table ").css({ "width": "100%" });

            $('form').parsley();


            var Font = Quill.import('formats/font');
            Font.whitelist = ['sans-serif', 'serif', 'monospace', 'roboto', 'arial', 'times-new-roman', 'comic-sans'];
            Quill.register(Font, true);


            var quill = new Quill('#editor', {
                theme: 'snow',
                modules: {
                    toolbar: [
                        [{ 'font': Font.whitelist }, { 'size': [] }],
                        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
                        ['bold', 'italic', 'underline', 'strike'],
                        [{ 'color': [] }, { 'background': [] }],
                        [{ 'script': 'sub' }, { 'script': 'super' }],
                        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                        [{ 'indent': '-1' }, { 'indent': '+1' }],
                        [{ 'direction': 'rtl' }],
                        [{ 'align': [] }],
                        ['link', 'image', 'video', 'formula'],
                        ['clean']  // remove formatting
                    ]
                }
            });


            function submitContent() {
                var content = quill.root.innerHTML;
                alert(content);
            }
        }

    </script>
    <style type="text/css">
        .form-control {
        }

        .ql-snow .ql-picker.ql-font .ql-picker-label[data-value="roboto"]::before,
        .ql-snow .ql-picker.ql-font .ql-picker-item[data-value="roboto"]::before {
            content: 'Roboto';
            font-family: 'Roboto', sans-serif;
        }

        .ql-font-roboto {
            font-family: 'Roboto', sans-serif;
        }

        .ql-snow .ql-picker.ql-font .ql-picker-label[data-value="arial"]::before,
        .ql-snow .ql-picker.ql-font .ql-picker-item[data-value="arial"]::before {
            content: 'Arial';
            font-family: Arial, sans-serif;
        }

        .ql-font-arial {
            font-family: Arial, sans-serif;
        }

        .ql-snow .ql-picker.ql-font .ql-picker-label[data-value="times-new-roman"]::before,
        .ql-snow .ql-picker.ql-font .ql-picker-item[data-value="times-new-roman"]::before {
            content: 'Times New Roman';
            font-family: "Times New Roman", serif;
        }

        .ql-font-times-new-roman {
            font-family: "Times New Roman", serif;
        }

        .ql-snow .ql-picker.ql-font .ql-picker-label[data-value="comic-sans"]::before,
        .ql-snow .ql-picker.ql-font .ql-picker-item[data-value="comic-sans"]::before {
            content: 'Comic Sans';
            font-family: "Comic Sans MS", cursive, sans-serif;
        }

        .ql-font-comic-sans {
            font-family: "Comic Sans MS", cursive, sans-serif;
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
                                        <li class="breadcrumb-item"><a href="#">Kredit Pensiun</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="Deviasi.aspx">Disposisi</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title"></h4>
                            </div>
                        </div>
                    </div>

                    <!-- Panel Header -->
                    <asp:Panel ID="pnlHeader" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Transaksi" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">
                                        <asp:TextBox ID="txtfocus" CssClass="form-control" ReadOnly="true" Text="Disposisi" runat="server"></asp:TextBox>
                                    </h4>

                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">



                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnRowCommand="GRData_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No Bukti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Nama Debitur">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamadebitur" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaDebitur")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>                             
                                                    <asp:TemplateField HeaderText="No Pensiun">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnopensiun" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoPensiun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblto" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "[To]")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltitle" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dept">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldept" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purpose">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpurpose" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Purpose")%>'></asp:Label>
                                                            <asp:Label ID="lblreason" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Reason")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Perihal">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperihal" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Perihal")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Editor" Visible ="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbleditor" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Editor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="KodeCabang" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodecabang" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Process Verifikasi" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnPDF" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnApproveAntrian" Visible="true" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Approve dan menentukan data ini ?');" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnBatalkanAntrian" Visible="false" runat="server" data-toggle="tooltip" data-placement="left" title="Batalkan Antrian" CommandName="Batalkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-thumbs-down"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnDelete" runat="server" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDELETE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-trash"></i></asp:LinkButton>

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
                                <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Data</div>
                                        <p class="text-muted font-14 m-b-30"></p>
                                        <div class="form-row">
                                            <div class="form-group col-md-6">
                                                <div class="form-group">
                                                    <label for="txtNoBukti" class="col-3 col-form-label">No Bukti</label>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="txtNoBukti" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtTo" class="col-3 col-form-label">To</label>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="txtTo" CssClass="col-11 col-form-label" runat="server" Text="Giga Group" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtTitle" class="col-3 col-form-label">Title</label>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="txtTitle" CssClass="col-11 col-form-label" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="form-group col-md-6">
                                                <div class="form-group">
                                                    <label for="ddlPurpose" class="col-3 col-form-label">Purpose</label>
                                                    <div class="col-10">
                                                        <%--<asp:TextBox ID="txtPurpose" CssClass="col-11 col-form-label" runat="server"></asp:TextBox>--%>
                                                        <asp:DropDownList runat="server" ID="ddlPurpose" CssClass="form-control selectpicker" data-style="btn-custom" AutoPostBack="true">
                                                            <asp:ListItem Text="Pencairan" Value="Pencairan"></asp:ListItem>
                                                            <asp:ListItem Text="Dana Talangan" Value="Dana Talangan"></asp:ListItem>
                                                            <asp:ListItem Text="Deviasi" Value="Deviasi"></asp:ListItem>
                                                            <asp:ListItem Text="Panjar" Value="Panjar"></asp:ListItem>
                                                            <asp:ListItem Text="Lain - lain" Value="Lain - lain"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div ID="divReason" runat="server" class="form-group" visible="false">
                                                    <label for="txtReason" class="col-3 col-form-label">Reason</label>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="txtReason" CssClass="col-11 col-form-label" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group form-row">
                                                    <div class="col-md-4">
                                                        <div class="form-group ">
                                                            <label class="col-4 col-form-label">Dept</label>
                                                            <div class="col-14">
                                                                <asp:DropDownList runat="server" ID="ddlDept"
                                                                    CssClass="form-control selectpicker"
                                                                    data-style="btn-custom">
                                                                    <asp:ListItem Text="Bisnis" Value="Bisnis"></asp:ListItem>
                                                                    <asp:ListItem Text="Operational" Value="Operational"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-3 col-form-label">Perihal</label>
                                                            <div class="col-12">
                                                                <asp:TextBox ID="txtPrihal"
                                                                    CssClass="form-control"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                 <div class="form-group form-row">
                                                     <div class="col-md-4">
                                                         <div class="form-group ">
                                                             <label class="col-8 col-form-label">Nama Debitur</label>
                                                             <div class="col-14">
                                                                     <asp:TextBox ID="txtNamaDebitur"
                                                                     CssClass="form-control"
                                                                     runat="server"></asp:TextBox>

                                                             </div>
                                                         </div>
                                                     </div>

                                                     <div class="col-md-6">
                                                         <div class="form-group">
                                                             <label class="col-8 col-form-label">No. Pensiun / NIP</label>
                                                             <div class="col-12">
                                                                 <asp:TextBox ID="txtNoPen"
                                                                     CssClass="form-control"
                                                                     runat="server"></asp:TextBox>
                                                             </div>
                                                         </div>
                                                     </div>

                                                 </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlUploadDokument" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-12">
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">Editor</div>
                                        <p class="text-muted font-14 m-b-30"></p>
                                        <div class="form-row">
                                            <div class="form-group col-md-10">
                                                <div class="form-group">
                                                    <div id="editor" style="height: 300px;"></div>
                                                    <asp:HiddenField ID="hdnEditorContent" runat="server" />

                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white right" runat="server" Text="Process" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                            <asp:Button ID="btnDelete" class="btn btn-success waves-light text-white right" runat="server" Text="Delete" OnClientClick="return sweetAlertdelete(this,'Apakah anda yakin ingin hapus semua data ini ?');" OnClick="btnDelete_Click" />
                                            <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
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


</html>
