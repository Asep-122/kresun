<%@ Page Language="C#" AutoEventWireup="true" CodeFile="approvekaryawanmutasi.aspx.cs" Inherits="mikro_approvekaryawanmutasi" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>Fee Cabang</title>
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
         <link href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Responsive datatable examples -->
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>

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
            function Comma(Num) {

            console.log('<%=CultureName%>');
            var splitnya;
            var koma;

            var region = '<%=CultureName%>';

            if (region == 'en-ID') {
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


        function isnumerickey(e) {

            var region = '<%=CultureName%>';


            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {


                if (txt == 44 && region == 'en-ID') {
                    return true;
                }
                if (txt == 46 && region != 'en-ID') {
                    return true;
                }
                else {
                    //alert("Input is Number only")
                    return false;
                }

            }
            return true;
        }

        
        function checkTextField1(field) {

            var region = '<%=CultureName%>';
            var splitnya;
            var dolarnya;
            var koma;

            if (region == 'en-ID') {
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
                         $('#Txttglmasuk').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
                         $('#txttglresign').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('.selectpicker').selectpicker();

            $(function () {
                var table = $('#GRUser').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
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
                    <!-- Form row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                <h4 class="text-uppercase m-b-5 font-13 font-600">
                                    <asp:Label ID="Label1" runat="server" Text="Approve Mutasi Karyawan Cabang"></asp:Label></h4>
                                <p class="text-muted m-b-30 font-13">
                                    <asp:LinkButton ID="lnkRefreshModal" runat="server" OnClick="lnkRefreshModal_Click" />



                                    <asp:Panel ID="pnlFront" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">

                                                    <div class="form-row">
                                                        <div class="col-sm-12">
                                                            <button type="button" visible="false" id="btnsearch" class="btn btn-custom waves-light waves-effect" href="#" runat="server" onserverclick="LnkAdd_Click">Add Mutasi Karyawan </button>
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
                                                                    <asp:TemplateField HeaderText="NIK">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnik" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodenik")%>'></asp:Label>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakaryawan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="status">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblketflag" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ketflag")%>'></asp:Label>
                                                                            <asp:Label ID="lblflag" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "aktif")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Kode cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namacabang")%>'></asp:Label>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tipe Mutasi">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltipemutasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tipemutasi")%>'></asp:Label>
                                                                            <asp:Label ID="lblkettipemutasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kettipemutasi")%>'></asp:Label>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Tanggal Insert">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltglinsert" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAdded","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="User Insert">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblxCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>--%>
                                                                            <asp:Label ID="lbluserx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AddedByFK")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <%--<asp:LinkButton ID="lnkEditUser" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Edit" runat="server" CommandName="GRUPEDT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                                            <asp:LinkButton ID="lnkGR" runat="server" data-toggle="tooltip" data-placement="top" class="tooltips" data-original-title="Approve" CommandName="GRUPSET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class=" fa fa-edit "></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" data-toggle="tooltip" data-placement="top" class="tooltips" data-original-title="View" CommandName="GRUPSETVIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class=" fa fa-edit "></i></asp:LinkButton>
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



                                    <asp:Panel ID="pnlFilter" Visible="false" runat="server">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Add Mutasi Karyawan </h4>

                                                    <div class="form-group row">

                                                        <label for="inputCity" class="col-form-label">Nik</label>
                                                        <asp:DropDownList ID="ddlnik" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="form-group row">

                                                        <label for="inputCity" class="col-form-label">Tipe Mutasi</label>
                                                        <asp:DropDownList ID="ddltipemutasi" runat="server" CssClass="form-control" data-style="btn-custom">
                                                            <asp:ListItem Selected="True" Value="">Pilih Tipe Mutasi</asp:ListItem>
                                                            <asp:ListItem Value="A1">Pergantian Atasan Dan Cabang</asp:ListItem>
                                                            <asp:ListItem Value="A2">PerUbahan Sallary</asp:ListItem>
                                                            <asp:ListItem Value="A3">Pergantian Atasan</asp:ListItem>
                                                            <asp:ListItem Value="A4">Pergantian Jenis Karyawan</asp:ListItem>
                                                            <asp:ListItem Value="A4">Pergantian PTKP</asp:ListItem>
                                                            <asp:ListItem Value="A4">Pergantian PTKP</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="form-group row">

                                                        <label for="inputCity" class="col-form-label">Nama Karyawan</label>
                                                        <asp:TextBox ID="txtnama" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group row">

                                                        <label for="inputCity" class="col-form-label">Catatan Mutasi</label>
                                                        <asp:TextBox ID="txtcatatan" TextMode="MultiLine" Enabled="true" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>

                                                    <div class="col-9">
                                                        <asp:Button ID="btnproses" CssClass="btn btn-outline-primary" runat="server" Text="Proses" OnClick="btnproses_Click" />
                                                        <asp:LinkButton ID="btnbatal" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnCancelmutasi_Click" />
                                                    </div>





                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                    </asp:Panel>


                                    <asp:Panel ID="Pnlmutasicabang" Visible="false" runat="server">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Mutasi Cabang Lama</h4>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Cabang</label>
                                                        <asp:DropDownList ID="ddlcabang" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Bos Id</label>
                                                        <asp:DropDownList ID="ddlbosid" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>


                                                </div>
                                                <!-- end card-box -->
                                            </div>


                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Mutasi Cabang Baru </h4>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Cabang</label>
                                                        <asp:DropDownList ID="ddlcabangbaru" runat="server" CssClass="form-control" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlcabangbaru_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Bos Id</label>
                                                        <asp:DropDownList ID="ddlbosidbaru" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlstatuskry" Visible="false" runat="server">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Status Karyawan Lama</h4>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Status Karyawan</label>
                                                        <asp:DropDownList ID="ddljeniskary" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                            <asp:ListItem Value="KP">Karyawan Percobaan</asp:ListItem>
                                                            <asp:ListItem Value="KK">Karyawan Kontrak</asp:ListItem>
                                                            <asp:ListItem Value="KT">Karyawan Tetap</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Status Karyawan Baru </h4>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Status Karyawan</label>
                                                        <asp:DropDownList ID="ddljeniskarybaru" runat="server" CssClass="form-control" data-style="btn-custom">
                                                            <asp:ListItem Value="KP">Karyawan Percobaan</asp:ListItem>
                                                            <asp:ListItem Value="KK">Karyawan Kontrak</asp:ListItem>
                                                            <asp:ListItem Value="KT">Karyawan Tetap</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Pnlptkp" Visible="false" runat="server">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data PTKP Lama</h4>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Ptkp Karyawan</label>
                                                        <asp:DropDownList ID="ddlptkp" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                            <asp:ListItem Value="TK">Tidak Kawin</asp:ListItem>
                                                            <asp:ListItem Value="K0">Kawin tanpa anak</asp:ListItem>
                                                            <asp:ListItem Value="K1">Kawin 1 anak</asp:ListItem>
                                                            <asp:ListItem Value="K2">Kawin 2 anak</asp:ListItem>
                                                            <asp:ListItem Value="K3">Kawin 3 anak</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data PTKP Baru </h4>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Ptkp Karyawan</label>
                                                        <asp:DropDownList ID="ddlptkpbaru" runat="server" CssClass="form-control" data-style="btn-custom">
                                                            <asp:ListItem Value="TK">Tidak Kawin</asp:ListItem>
                                                            <asp:ListItem Value="K0">Kawin tanpa anak</asp:ListItem>
                                                            <asp:ListItem Value="K1">Kawin 1 anak</asp:ListItem>
                                                            <asp:ListItem Value="K2">Kawin 2 anak</asp:ListItem>
                                                            <asp:ListItem Value="K3">Kawin 3 anak</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Pnljabatan" Visible="false" runat="server">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Jabatan Lama</h4>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Kode Jabatan</label>
                                                        <asp:DropDownList ID="ddlkodejabatan" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Group</label>
                                                        <asp:DropDownList ID="ddlgroup" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Bos Id</label>
                                                        <asp:DropDownList ID="ddlbosidjabatan" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>



                                                </div>

                                            </div>


                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Jabatan Baru </h4>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Kode Jabatan</label>
                                                        <asp:DropDownList ID="ddlkodejabatanbaru" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Group</label>
                                                        <asp:DropDownList ID="ddlgroupbaru" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Bos Id</label>
                                                        <asp:DropDownList ID="ddlbosidjabatanbaru" runat="server" CssClass="form-control" data-style="btn-custom">
                                                        </asp:DropDownList>
                                                    </div>


                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                    </asp:Panel>

                                    <asp:Panel ID="Pnlgaji" Visible="false" runat="server">

                                        <div class="row">


                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Gaji Lama</h4>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Gaji Pokok</label>
                                                        <asp:TextBox ID="txtgajipokok" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Tunjangan Makan</label>
                                                        <asp:TextBox ID="txtjunganmakan" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Tunjangan Transport</label>
                                                        <asp:TextBox ID="txttunjangantransport" ReadOnly="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    </div>


                                                </div>
                                                <!-- end card-box -->
                                            </div>



                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Gaji Baru </h4>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Gaji Pokok</label>
                                                        <asp:TextBox ID="txtgajipokokBaru" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Tunjangan Makan</label>
                                                        <asp:TextBox ID="txtjunganmakanBaru" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Tunjangan Transport</label>
                                                        <asp:TextBox ID="txttunjangantransportBaru" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                    </div>


                                                </div>
                                                <!-- end card-box -->
                                            </div>



                                        </div>


                                    </asp:Panel>

                                    <asp:Panel ID="Pnlresign" Visible="false" runat="server">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Karyawan</h4>

                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Tanggal Masuk</label>
                                                        <asp:TextBox ID="Txttglmasuk" Enabled="false" runat="server" CssClass="form-control" TextMode="DateTime"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Group</label>
                                                        <asp:TextBox ID="Txtgroup" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtkodegroup" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Jabatan</label>
                                                        <asp:TextBox ID="TxtJabatan" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="txtkodejabatan" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Kode Staf</label>
                                                        <asp:TextBox ID="txtkodestaf" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Bos_id</label>
                                                        <asp:TextBox ID="txtbosid" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-lg-6">

                                                <div class="card-box">
                                                    <h4 class="m-t-q header-title">Data Resign</h4>
                                                    <div class="form-group row">
                                                        <label for="inputCity" class="col-form-label">Tanggal Efektif Resing</label>
                                                        <asp:TextBox ID="txttglresign" runat="server" CssClass="form-control" Type="date" TextMode="DateTime"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <!-- end card-box -->
                                            </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlfooter" Visible="false" runat="server">

                                        <div class="row">

                                            <div class="col-lg-6">
                                                <div class="col-9">
                                                    <asp:Button ID="btnSimpan" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnCancel_Click" />
                                                </div>

                                            </div>

                                        </div>
                                    </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <!-- end row -->


                    <!-- /.modal-dialog -->
                </div>
                <!-- /.modal -->

                </div>
            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnproses" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="lnkRefreshModal" />
                <asp:PostBackTrigger ControlID="GRUser" />

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
     <script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

<!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>
</html>

