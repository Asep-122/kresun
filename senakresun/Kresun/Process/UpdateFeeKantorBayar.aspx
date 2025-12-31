<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateFeeKantorBayar.aspx.cs" Inherits="SD_UPDATE_PROVISI" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Update Fee Kantor Bayar</title>
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

		
        function isnumerickey1(e) {
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


            if (window.event){
                var txt = window.event.keyCode; // IE
				console.log(txt +'windows');
				//console.log(txt + 'pertama');
				//alert(txt + 'pertama');
				}
            else{
                var txt = e.which;
				console.log(txt+'ellsewindows');
				//console.log(txt + 'kedua');|| txt == 188
				//alert(txt + 'kedua');
				}
            if (txt > 31 && (txt < 48 || txt > 57 )) {
				console.log(txt+'if1');
				//alert(txt + '1');
				//console.log(txt + '1');
                if (txt == 44 && region == 'id-ID') {
					console.log(txt+'if2');
				//alert(txt + '2');
				//console.log(txt + '2');
                    return true;
                }
                else if ((txt == 46 || txt == 188)&& region != 'id-ID') {
					console.log(txt+'if3');
				//alert(txt + '3');
				//console.log(txt + '3');
                    return true;
                }
                else if ((txt == 188)&& region == 'id-ID') {
					console.log(txt+'if4');
				//alert(txt + '3');
				//console.log(txt + '3');
                    return true;
                }
                else {
                    //alert("Input is Number only")
				//alert(txt + '4');
				//console.log(txt + '4');
					console.log(txt+'if5');
				e.target.value = e.target.value.replace(/[^\d]/g,'');
                    return false;
                }
				console.log(txt);

				alert(txt);
            }
            return true;
        }
function numberMobile(e){
    e.target.value = e.target.value.replace(/[^\d]/g,'');
    return false;
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

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            
            $('#txttgl').datepicker({
                format: 'dd mmmm yyyy',
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

            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }
        function PeringatanKode(a) {

            setTimeout(function () {
                swal(
                    {
                        title: a,
                        type: "warning",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000
                    }
                ).catch(swal.noop)
            }, 100);
            //$("#txtjenisgol").val(""); 
        }
        function Delete(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil di hapus",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
                        type: "success",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000,
                        hideAfter: 2000,
                        afterHidden: function () {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }
                    }
                ).catch(swal.noop)
            }, 100);
            location.href = 'JenisGolongan.aspx ';
        }
        function Simpan(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
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
            $("#txtkodegroup").val("");
            $("#txtketerangan").val("");
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
    </script>
    <script type="text/javascript">
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
        function kepo1(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[0-9,.]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
        function kepo2(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
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
                                        <li class="breadcrumb-item"><a href="#">Sumber Dana</a></li>
                                        <li class="breadcrumb-item"><a href="#">Transaksi</a></li>
                                        <li class="breadcrumb-item"><a href="UpdateFeeKantorBayar.aspx">Update Fee Kantor Bayar</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Update Fee Kantor Bayar</h4>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Filter -->
                    <asp:Panel ID="pnlFilter" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="card-box">
                                    <div>
                                        <div class="col-md-4">
                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kantor Bayar</p>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlKantorBayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" required="required" OnSelectedIndexChanged="btnSearch_OnChange"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="button-list col-md-12">
                                        <br>
                                        <asp:Button ID="btnsearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Panel Edit -->
                    <asp:Panel ID="pnladd" runat="server">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">ENTRY DATA</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class=" col-md-12">
                                                    <div class="form-check-inline col-md-12">
                                                        <span id="Label1" class=" mb-1 mt-4 col-md-7 col-form-label font-weight-bold">Persen Komisi</span>
                                                        <asp:TextBox CssClass="mb-1 mt-4 form-control" ID="txtpsn" MaxLength="10" runat="server" required="required" onkeyup="return isnumerickey(event); "    onblur="checkTextField1(this);"  AutoPostBack="true"  ></asp:TextBox>
                                                        &nbsp<span id="Label3" class=" mb-1 mt-4  col-form-label font-weight-bold">%</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">INFORMASI</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class=" col-md-12">
                                                    <div class="form-check-inline col-md-12">
                                                        <span class=" mb-1 mt-4 col-md-4 col-form-label font-weight-bold" id="P1" runat="server">Update Oleh</span>
                                                        <asp:TextBox ID="txtupdate" CssClass="mb-1 mt-4  form-control col-6"  runat="server" Enabled="false" MaxLength="100" onkeypress="return kepo(event);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-check-inline col-md-12">
                                                        <span class=" mb-1 mt-4 col-md-4 col-form-label font-weight-bold">Tgl Update</span>
                                                        <asp:TextBox ID="txttgl" CssClass="mb-1 mt-4 form-control col-4"  TextMode="DateTime" runat="server" Enabled="false" MaxLength="50" onkeypress="return kepo1(event);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="row">
                                    <div class="btn-group mb-2">
                                        <div class="col-md-12">
                                            <div class="button-list">
                                                <br>
                                                <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />
                                                <asp:LinkButton ID="btnCancel" Visible="false" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
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

