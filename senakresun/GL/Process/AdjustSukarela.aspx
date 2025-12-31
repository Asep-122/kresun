<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdjustSukarela.aspx.cs" Inherits="GL_Process_AdjustSukarela" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Create Adjust Bulanan</title>
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
        function sweetAlertConfirm(btnedit, errMsg) {
            if (btnedit.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnedit.dataset.confirmed = false;
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
                        btnedit.dataset.confirmed = true;
                        btnedit.click();
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
            }
        }

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

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

        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

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


        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#datatable').DataTable();
            //Buttons examples
            var table = $('#GRData').DataTable({
                "order": [], "paging": false,
                //buttons: ['copy', 'excel', 'pdf'],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });
            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });
            
            Resize();
            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            table.buttons().container()
                .appendTo('#GRData_wrapper .col-md-6:eq(0)');
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
                                        <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Create Bunga & Pajak Sukarela</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Create Bunga & Pajak Sukarela</h4>
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
                    <asp:Panel ID="pnlHeader" runat="server" Visible ="false">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h3 class="m-t-0 header-title bold">Data Bunga & Pajak Sukarela</h3>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 95%;" >
                                            <%--<asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="550px">--%>
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RowNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="align-content-center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKodeGl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodecabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"  CssClass="align-content-center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Cabang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnamaGl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCabang")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" CssClass="align-content-center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bunga">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblBudgetTi" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Bunga", "{0:N2}")%>'></asp:Label>--%>
                                                              
                                                            <%--<div class="form-group">--%>
                                                                <div class="form-line">
                                                                    <asp:TextBox ID="txtbunga"  onkeyup="javascript:this.value=Comma(this.value);"  CssClass="form-control required" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "Bunga", "{0:N0}")%>'  ></asp:TextBox>
                    
                                                                </div>
                                                            <%--</div>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pajak">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblPajak" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pajak", "{0:N2}")%>'></asp:Label>--%>
                                                              
                                                            <%--<div class="form-group">--%>
                                                                <div class="form-line">
                                                                    <asp:TextBox ID="txtPajak"  onkeyup="javascript:this.value=Comma(this.value);" CssClass="form-control required" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "Pajak", "{0:N0}")%>'  ></asp:TextBox>
                    
                                                                </div>
                                                            <%--</div>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                            <%--<asp:LinkButton  ID="btnedit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="UPD" data-placement="left" data-toggle="tooltip" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to update data ? ')) {  return false; }" title="Update"> <i class="btn-outline-danger">Update</i></asp:LinkButton>--%> 
                                            <asp:LinkButton ID="btnedit" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="UPDATE" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin mengupdate data ini ?');" CommandName="UPD" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi ">Update</i></asp:LinkButton>
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
                                <div class="row clearfix">
                                    <div class="col-md-6">
                                        <p>
                                            <b>kode Cabang</b>
                                        </p>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:TextBox CssClass="form-control" ID="ddlkodegl" MaxLength="100" runat="server"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-6">
                                        <p>
                                            <b>Nama Cabang</b>
                                        </p>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:TextBox CssClass="form-control" ID="txtnamaGl" MaxLength="100" runat="server"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row clearfix">
                                    <div class="col-md-6">
                                        <p>
                                            <b>Adjust Harian</b>
                                        </p>
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:TextBox ID="Txttotal" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" Text="0" CssClass="form-control required" runat="server"></asp:TextBox>
                                                <%--    <asp:TextBox ID="TextBox1" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" ></asp:TextBox>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row clearfix">
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click" />
                                        <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="btn-group mb-2">
                                <div class="button-list">
                                    <br>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
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
<!-- Buttons examples -->
<script src="/plugins/datatables/dataTables.buttons.min.js"></script>
<script src="/plugins/datatables/buttons.bootstrap4.min.js"></script>
<script src="/plugins/datatables/jszip.min.js"></script>
<script src="/plugins/datatables/pdfmake.min.js"></script>
<script src="/plugins/datatables/vfs_fonts.js"></script>
<script src="/plugins/datatables/buttons.html5.min.js"></script>
<script src="/plugins/datatables/buttons.print.min.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.core.js"></script>
<script src="/assets/js/jquery.app.js"></script>
<!-- plugin js -->
<script src="/plugins/moment/moment.js"></script>
<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
</html>
