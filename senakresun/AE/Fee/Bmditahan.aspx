<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bmditahan.aspx.cs" Inherits="mikro_Bmditahan" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>Fee BM Ditahan</title>
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
   <%-- <script src="/plugins/custombox/js/legacy.min.js"></script>--%>

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

            $('#LnkAdd, #lnkCari, #lnkSaveGR').tooltipster({
                animation: 'grow'
            });

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
                $("#GRUser").show();
            });
            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            top: 0px;
            left: 0px;
            -ms-flex: 0 0 75%;
            flex: 0 0 75%;
            max-width: 75%;
            padding-left: 15px;
            padding-right: 15px;
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
                <div class="container-fluid">
                    <!-- Form row -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                <h4 class="text-uppercase m-b-5 font-13 font-600"> <asp:Label ID="Label1" runat="server" Text="Bm Ditahan percabang"></asp:Label></h4>
                                <p class="text-muted m-b-30 font-13">
                                    <asp:LinkButton ID="lnkRefreshModal" runat="server" OnClick="lnkRefreshModal_Click" />
<asp:Panel ID="Pnlsearch" runat="server">
                        <div class="row">
                                              <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="text-uppercase m-b-5 font-13 font-600">Search By</label>
                                                                  
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">CABANG</label>
                                                                        <div class="auto-style1">
                                                                            <asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Group</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlgroup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <div class="col-3">
                                                                        <button type="button" id="btnsearch" class="btn btn-outline-custom waves-light waves-effect" runat="server" onserverclick="lnkCari_Click">Search</button>
                                                                        </div>
                                                                    </div>
                                                            </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                        </div>
                    </asp:Panel>


                                    <asp:Panel ID="pnlFront" runat="server">
                                    <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="GRUser" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRUser_RowCommand" AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="NIK">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblxCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>--%>
                                                                            <asp:Label ID="lblnik"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nik")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblxCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>--%>
                                                                            <asp:Label ID="lblkode_cabang"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodecabang")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bulan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbulan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bulan")%>'></asp:Label>
                                                                           
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tahun">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltahun" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tahun")%>'></asp:Label>
                                                                           
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaLengkap")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>    
                                                                     <asp:TemplateField HeaderText="Fungsi Jabatan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblfungsi_jabatan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaJabatan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  
                                                                     
                                                                    <asp:TemplateField HeaderText="Fee BM">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBM"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FeeMarketing", "{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>     
                                                                   <asp:TemplateField HeaderText="Bm ditahan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbmditahan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bmtertahan", "{0:N2}")%>'></asp:Label>
                                                                            <asp:Label ID="lbllock" visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lock")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  
                                                                 
                                                                   
                          
                                                                    <asp:TemplateField HeaderText="BM Ditahan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEditUser" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="TAHAN" runat="server" CommandName="GRUPEDT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class=" mdi mdi-cart"></i></asp:LinkButton>
                                                                            <%--<asp:LinkButton ID="lnkGR" runat="server" data-toggle="tooltip" data-placement="top" class="tooltips" data-original-title="Delete" CommandName="GRUPSET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class=" mdi mdi-delete-forever "></i></asp:LinkButton>--%>
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
                                                                        <label for="inputpid" class="col-3 col-form-label">Nik</label>
                                                                        <div class="col-9">
                                                                            <asp:TextBox ID="txtnik" runat="server" Enabled="false"></asp:TextBox>     
                                                                            <asp:TextBox ID="txtbulan" runat="server" visible="false"></asp:TextBox> 
                                                                            <asp:TextBox ID="txttahun" runat="server" visible="false"></asp:TextBox> 
                                                                            <asp:TextBox ID="txtcabang" runat="server" visible="false"></asp:TextBox> 
                                                                        </div>
                                                                    </div>

                                                            
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Fee BM Bulan Ini</label>
                                                                        <div class="col-9">
                                                                              <asp:TextBox ID="txtfeebmbulanini" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                                            
                                                                            <%--<asp:TextBox CssClass="form-control" ID="txtbatasawal" required="required" runat="server"></asp:TextBox>--%>
                                                                        </div>
                                                                    </div>
                                                                     <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Fee BM Ditahan Bulan Ini</label>
                                                                        <div class="col-9">
                                                                                  <asp:TextBox ID="txtbmditahan" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtbmditahanlama" Visible="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>
                                                                   
                                                                            <%--<asp:TextBox CssClass="form-control" ID="txtbatasakhir" required="required" runat="server"></asp:TextBox>--%>
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
                <asp:PostBackTrigger ControlID="btnsearch" />
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

