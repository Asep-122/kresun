<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransaksiGL.aspx.cs" Inherits="GL_Process_TransaksiGL" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <meta http-equiv='cache-control' content='no-cache' />
    <meta http-equiv='expires' content='0' />
    <meta http-equiv='pragma' content='no-cache' />
    <title></title>
  <!-- Plugins css-->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../assets/js/modernizr.min.js"></script>

    <link href="../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../Javascript/jquery-ui.js"></script>

    <!-- Toastr css -->
    <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Modal -->
    <link href="../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../../plugins/tooltipster/tooltipster.bundle.min.css" />
    <!-- DataTables -->
    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function showSuccessMessage() {
            try {
                swal({
                    title: "Data Berhasil dimasukan",
                    text: "",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: true,
                    closeOnCancel: false
                }, function (isConfirm) {
                    if (isConfirm) {

                      <%--  setTimeout(function () {<%= Lumpsum() %>; }, 1); --%>

                        }
                    });
            }
            catch (err) {
                console.log(err.message);
            }

        }

        function showSuccessMessagetoAPVFinish() {
            try {
                swal({
                    title: "Data Berhasil dimasukan",
                    text: "",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: true,
                    closeOnCancel: false
                }, function (isConfirm) {
                    if (isConfirm) {

                        window.location.href = 'SPJKhusus.aspx';

                    }
                });
            }
            catch (err) {
                console.log(err.message);
            }

        }

        function showSuccessMessagetoAPV() {
            try {
                swal({
                    title: "Data Berhasil dimasukan",
                    text: "",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "OK",
                    closeOnConfirm: true,
                    closeOnCancel: false
                }, function (isConfirm) {
                    if (isConfirm) {

                      <%--  setTimeout(function () {<%= LumpsumAPV() %>; }, 1);--%>

                        }
                    });
            }
            catch (err) {
                console.log(err.message);
            }

        }

        function ConfirmDeleteMessage(strMessage) {

            if (window.confirm(strMessage)) {

                return true;
            }
            else {

                return false;
            }

        }

        function SumAll() {


            var f = document.getElementById("GRLumpsumDetail");

            var total = 0;


            for (var j = 0; j < f.rows.length - 2; j++) {

                var rowIndex;

                rowIndex = j.toString();

                document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value);
                document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value);
                document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value);


                var txtQty = document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value;
                var txtHarga = document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value;


                var jjp = $('#GRLumpsumDetail_ddlJP_' + rowIndex).find('option:selected').val();

                if (jjp == 'USKU' || jjp == 'USMN') {

                    if (txtQty >= 11) {
                        txtQty = 10;
                    }
                }


                document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(parseFloat(txtQty) * parseFloat(txtHarga.replace(/,/g, '')));

                if (jjp == 'USKU' || jjp == 'USMN') {

                    if (parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value) >= 11) {

                        var caltem = Comma(parseFloat(txtQty) * parseFloat(txtHarga.replace(/,/g, '')));


                        var QTY2 = parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value);

                        if (parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value) > 30) {
                            QTY2 = 30;
                        }

                        txtQty = QTY2 - 10
                        txtHarga = document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value;

                        var txtHarga2 = Comma(txtQty * parseFloat(txtHarga.replace(/,/g, '')) * 75 / 100);

                        //alert(txtQty);
                        //alert(txtHarga);
                        //alert(txtHarga2);

                        document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(parseFloat(txtHarga2.replace(/,/g, '')) + parseFloat(caltem.replace(/,/g, '')));

                    }

                }

                total = parseFloat(total) + parseFloat(document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value.replace(/,/g, ''));

            }

            document.getElementById('GRLumpsumDetail_txttotal').innerHTML = Comma(total);

        }

        function SumS(rowIndex, jjp) {



            document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value);
            document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value);
            document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value);


            var txtQty = document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value;
            var txtHarga = document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value;


            if (jjp == 'USKU' || jjp == 'USMN') {

                if (txtQty >= 11) {
                    txtQty = 10;
                }
            }


            document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(parseFloat(txtQty) * parseFloat(txtHarga.replace(/,/g, '')));


            if (jjp == 'USKU' || jjp == 'USMN') {

                if (parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value) >= 11) {

                    var caltem = Comma(parseFloat(txtQty) * parseFloat(txtHarga.replace(/,/g, '')));


                    var QTY2 = parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value);

                    if (parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value) > 30) {
                        QTY2 = 30;
                    }

                    txtQty = QTY2 - 10
                    txtHarga = document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value;

                    var txtHarga2 = Comma(txtQty * parseFloat(txtHarga.replace(/,/g, '')) * 75 / 100);

                    //alert(txtQty);
                    //alert(txtHarga);
                    //alert(txtHarga2);

                    document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(parseFloat(txtHarga2.replace(/,/g, '')) + parseFloat(caltem.replace(/,/g, '')));

                }

            }

            SumAll();

        }


        function Sum(row) {

            var rowData = row.parentNode.parentNode.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;

            document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value);
            document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value);
            document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value);


            var txtQty = document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value;
            var txtHarga = document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value;

            var jjp = $('#GRLumpsumDetail_ddlJP_' + rowIndex).find('option:selected').val();

            if (jjp == 'USKU' || jjp == 'USMN') {

                if (txtQty >= 11) {
                    txtQty = 10;
                }
            }


            document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(parseFloat(txtQty) * parseFloat(txtHarga.replace(/,/g, '')));

            if (jjp == 'USKU' || jjp == 'USMN') {

                if (parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value) >= 11) {

                    var caltem = Comma(parseFloat(txtQty) * parseFloat(txtHarga.replace(/,/g, '')));


                    var QTY2 = parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value);

                    if (parseFloat(document.getElementById('GRLumpsumDetail_txtQty_' + rowIndex).value) > 30) {
                        QTY2 = 30;
                    }

                    txtQty = QTY2 - 10
                    txtHarga = document.getElementById('GRLumpsumDetail_txtHarga_' + rowIndex).value;

                    var txtHarga2 = Comma(txtQty * parseFloat(txtHarga.replace(/,/g, '')) * 75 / 100);

                    //alert(txtQty);
                    //alert(txtHarga);
                    //alert(txtHarga2);

                    document.getElementById('GRLumpsumDetail_txtSubTotal_' + rowIndex).value = Comma(parseFloat(txtHarga2.replace(/,/g, '')) + parseFloat(caltem.replace(/,/g, '')));

                }

            }

            SumAll();

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


        function isnumerickey(e) {
            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {

                if (txt == 46) {
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

            Sum(field)
        }


        var nmButton;

        $(document).ready(function () {
            $("input[type='submit']").on("click", function () {
                nmButton = $(this).attr('id');
                //alert(nmButton);
                // return false;
            });
        });

        window.onbeforeunload = function () {

            $("<span id='spproses'>Loading....</span>").insertAfter("#" + nmButton);


            $("#" + nmButton).hide();
            //console.log(nmButton);
        }
        window.onunload = function () {


            $('span[id^="spproses"]').remove();

            $("#" + nmButton).show();

            //console.log("ok sudah");
        }

        function EmployeeAPV(EmpID, EmpName) {

          <%--  var  = ;
            var  = '<%= txtNama.ClientID %>';--%>

            var to = '../UserControler/UCEmployeeAllBranch.aspx?EmpID=' + EmpID + '&EmpName=' + EmpName + '';
            window.open(to, 'Employee List', 'height=800,width=800,toolbar=no, status=no,menubar=no,scrollbars=no');

            return true;


        }

        $(document).ready(InIEvent);
        function InIEvent() {

            autosize($('textarea.auto-growth'));

            $('#txtStartdate').bootstrapMaterialDatePicker({ weekStart: 1, time: false, clearButton: true, format: 'DD-MMM-YYYY', });
            $('#txtEndDate').bootstrapMaterialDatePicker({ weekStart: 1, time: false, clearButton: true, format: 'DD-MMM-YYYY', });

            $('#txttglB').bootstrapMaterialDatePicker({ weekStart: 1, time: false, clearButton: true, format: 'DD-MMM-YYYY', });
            $('#txtwaktuB').bootstrapMaterialDatePicker({
                format: 'HH:mm',
                clearButton: true,
                date: false
            });

            $('#txttglP').bootstrapMaterialDatePicker({ weekStart: 1, time: false, clearButton: true, format: 'DD-MMM-YYYY', });
            $('#txtwaktuP').bootstrapMaterialDatePicker({
                format: 'HH:mm',
                clearButton: true,
                date: false
            });

            $.getScript("../js/admin2.js");
            $.getScript("../js/pages/ui/tooltips-popovers.js");


            $('#form1').validate({

                highlight: function (input) {
                    $(input).parents('.form-line').addClass('error');
                },
                unhighlight: function (input) {
                    $(input).parents('.form-line').removeClass('error');
                },
                errorPlacement: function (error, element) {
                    $(element).parents('.form-group').append(error);
                }
            });


            $(function () {
                var table = $('#GRData').dataTable({
                    "order": [[0, 'asc']],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    "scrollX": true,
                    retrieve: true
                });
            });

            


            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $("div").remove(".tooltip");

        }
    </script>


</head>
<body class="theme-red">
    <!-- Page Loader -->
    <div class="page-loader-wrapper">
        <div class="loader">
            <div class="preloader">
                <div class="spinner-layer pl-red">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>
            </div>
            <p>Please wait...</p>
        </div>
    </div>
    <!-- #END# Page Loader -->
    <form id="form1" runat="server">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">
                        <div class="header">
                            <h2>FILTER CONDITION
                            </h2>
                        </div>
                        <div class="body">
                            <div class="form-horizontal">
                                <div class="row clearfix">
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-5 form-control-label">
                                        <label>Search by</label>
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-8 col-xs-7">
                                        <div class="form-group">
                                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-7">
                                                <div class="form-group">

                                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="show-tick form-control">
                                                        <asp:ListItem Value="spjd.NoSPJ">No Reference</asp:ListItem>
                                                        <asp:ListItem Value="NIKPelaksana">Delindex</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-7">
                                                <div class="form-group">
                                                    <div class="form-line">
                                                        <asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Cari" runat="server"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-5 form-control-label">
                                        <label>Periode Create</label>
                                    </div>
                                    <div class="col-lg-10 col-md-10 col-sm-8 col-xs-7">
                                        <div class="form-group">
                                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-7">
                                                <div class="form-group">

                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="show-tick form-control">
                                                        <asp:ListItem Value="01">January</asp:ListItem>
                                                        <asp:ListItem Value="02">February</asp:ListItem>
                                                        <asp:ListItem Value="03">March</asp:ListItem>
                                                        <asp:ListItem Value="04">April</asp:ListItem>
                                                        <asp:ListItem Value="05">May</asp:ListItem>
                                                        <asp:ListItem Value="06">June</asp:ListItem>
                                                        <asp:ListItem Value="07">July</asp:ListItem>
                                                        <asp:ListItem Value="08">August</asp:ListItem>
                                                        <asp:ListItem Value="09">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-8 col-xs-7">
                                                <div class="form-group">

                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="show-tick form-control">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-offset-2 col-md-offset-2 col-sm-offset-4 col-xs-offset-5">
                                        <asp:Button ID="btnSearch" class="btn btn-primary m-t-15 waves-effect" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">
                        <div class="header">
                            <h2>Query Table
                            </h2>
                        </div>
                        <div class="body">
                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="true" OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="No Reference">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Trn">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTgl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn", "{0:dd-MMM-yyyy}")%>'></asp:Label>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TypeTrn">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ketTypeTrn")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrency" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCrc")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Kurs">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKurs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KursStd")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jumlah")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Note">
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblnote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "note")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderStyle Height="30px" CssClass="align-center no-sort"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="EDT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="material-icons">mode_edit</i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkView" runat="server" data-toggle="tooltip" data-placement="left" title="View" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="material-icons">view_comfy</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="block-header" style="position: fixed; z-index: 1000; bottom: 0; right: 0px; padding: 4px 8px; overflow: hidden;">
                <h2>
                    <button runat="server" id="btnAdd" class="btn bg-green btn-circle-lg waves-effect waves-circle waves-float" onserverclick="btnAdd_ServerClick">
                        <i class="material-icons">add_circle</i>
                    </button>
                </h2>
            </div>
        </asp:Panel>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlSPJ" Visible="false" runat="server">
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="card">
                                <div class="header">
                                    <h2>Diberikan Kepada
                                    </h2>
                                </div>
                                <div class="body">
                                    <div class="row clearfix">
                                        <div class="col-md-3">
                                            <p>
                                                <b>No bukti</b>
                                            </p>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <asp:TextBox ID="txtNobukti" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <p>
                                                <b>Date trn</b>
                                            </p>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <div class="form-line">
                                                        <asp:TextBox ID="txtDate" runat="server" class="datepicker form-control"></asp:TextBox>
                                                        &nbsp;</div>
                                                  </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <p>
                                                <b>Type Transaction</b>
                                            </p>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <asp:DropDownList ID="ddltypetrn" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <p>
                                                <b>Currency</b>
                                            </p>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <asp:DropDownList ID="ddlcurrency" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <p>
                                                <b>Kurs</b>
                                            </p>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <asp:TextBox ID="txtkurs" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" ></asp:TextBox>
                                                 </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <p>
                                                <b>Note</b>
                                            </p>
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <asp:TextBox CssClass="form-control" ID="txtnote"  MaxLength="100" runat="server" TextMode="MultiLine" Width="301px"  ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="card">
                                <div class="header">
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                            <div class="card">
                                <div class="header">
                                </div>
                                <div class="body">
                                    <asp:GridView ID="GRRP" runat="server" AutoGenerateColumns="False" CssClass="footable" OnRowCommand="GRRP_RowCommand" OnRowDataBound="GRRP_RowDataBound" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account code">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlKodeGL" runat="server" CssClass="show-tick form-control">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnddlKodeGL" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "KodeGL")%>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account Name">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlnamaGL" runat="server" CssClass="show-tick form-control">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnddlnamaGL" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "namagl")%>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtKT" runat="server" CssClass="form-control no-resize auto-growth required" Rows="1" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>' TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="cost center">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">



                                                            <asp:DropDownList ID="ddlcostcenter" runat="server" CssClass="show-tick form-control">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnddlcostcenter" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "costcenter")%>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                            <asp:TemplateField HeaderText="Debit">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                               <asp:TextBox ID="jlhdebit" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" ></asp:TextBox>
                                                                                                
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                               <asp:TextBox ID="jlhkredit" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" ></asp:TextBox>
                                                                                                
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle CssClass="align-center" ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DEL" data-placement="left" data-toggle="tooltip" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to delete ? ')) {  return false; }" title="Hapus"> <i class="material-icons">delete_forever</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="row clearfix align-right">
                                        <p>
                                        </p>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:LinkButton ID="lnkAddRP" runat="server" CssClass="btn bg-green btn-circle-lg waves-effect waves-circle waves-float" OnClick="lnkAddRP_Click"> <i class="material-icons">add_to_queue</i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row clearfix align-right">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:LinkButton ID="btnCancel" class="btn btn-primary m-t-15 waves-effect" runat="server" Text="CANCEL" OnClick="btnCancel_Click"></asp:LinkButton>
                               
                                <asp:Button ID="btnNext" class="btn btn-primary m-t-15 waves-effect" runat="server" Text="NEXT" OnClick="btnNext_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnNext" />
             </Triggers>
        </asp:UpdatePanel>
    </form>

</body>
    
<!-- Parsley js -->
<script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>

<script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>
<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
</html>
