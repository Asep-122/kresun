<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewKasBank.aspx.cs" Inherits="CL_View_ViewKasBank" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>View Pencairan Pinjaman</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

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
             function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
         function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: '<%=nobuktidb%>',
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 2000,
                    stack: 1,
                    afterHidden: function () {
                        <%--if (fr == '1') {
                            ////setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
                        }--%>

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
            else if(e == '')
            {
                x = Number(document.getElementById('txttransAP').value);
                y = Number(obj.value); 
            } 
            z = x * y; 
            s = z / v;
            document.getElementById('txtjmlhGL').value = z; 
            document.getElementById('hfjmlrp').value = z;
            document.getElementById('txtjmlhCL').value = s;
        }
           function sweetAlertConfirm(btndelete,errMsg) {
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


function sweetAlertsave(btnSimpan,errMsg) {
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
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });


            
            $('#txttgljthtmp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttgltrn').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
             });  
            $('#txttgltrnx').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('.selectpicker').selectpicker();


            $(function () {
                var table = $('#GRDTL1').dataTable({
                    "order": [],
                    "paging": false,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GRDTL2').dataTable({
                    "order": [],
                    "paging": false,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GrdProvisi').dataTable({
                    "order": [],
                    "paging": false,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GrdMaterai').dataTable({
                    "order": [],
                    "paging": false,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GRDBANK').dataTable({
                    "order": [],
                    "paging": false,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });


                var table = $('#Grdcdgtl2').dataTable({
                    "order": [],
                    "paging": false,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                
                var table = $('#GRDHT').dataTable({
                    "order": [],
                    "paging": false,
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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">View Kas & Bank Hari ini</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Cadangan, Kas & Bank Hari ini</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            
                 <!-- Panel Header -->
               
                 <!-- Panel Front -->
                 <asp:Panel ID="pnlFilter"  runat="server">
                       


                         <div class="row">
                    <div class="col-sm-12">
                        <!-- meta -->
                        <div class="profile-user-box card-box bg-custom">
                            <div class="row">
                                <div class="col-sm-6">
                                    <%--<span class="pull-left mr-3"><img src="assets/images/users/avatar-1.jpg" alt="" class="thumb-lg rounded-circle"></span>--%>
                                    <div class="media-body text-white">
                                        <h4 class="mt-1 mb-1 font-18">Cadangan,Kas & Bank</h4>
                                        <p class="font-13 text-light"><asp:Label ID="lblnamakoperasi" Font-Bold="true" runat="server" Text=""></asp:Label></p>
                                        <p class="text-light mb-0"><asp:Label ID="lblcabang" Font-Bold="true" runat="server" Text=""></asp:Label></p>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    
                                    <div class="text-right">
                                        <button type="button" class="btn btn-light waves-effect">
                                            <i class="mdi mdi-account-settings-variant mr-1"></i>   <asp:TextBox ID="txttgltrnx" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdatex_TextChanged"></asp:TextBox>      
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--/ meta -->
                    </div>
                </div>
                <!-- end row -->


                <div class="row">
                     <!-- end col -->

                    <div class="col-md-4">
                        <div class="text-center card-box">

                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                                <div class="">
                                    <h4 class="m-b-5">KAS TELLER</h4>
                                </div>

                                

                                <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Message Now</button>--%>

                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalTL" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahantl" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpengurangantl" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakirtl" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                                <div class="">
                                    <h4 class="m-b-5">KAS TELLER 1</h4>
                                </div>

                                

                                <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Message Now</button>--%>

                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalTL1d" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahantl1d" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpengurangantld" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakirtl1d" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                               

     

                                     <div class="">
                                    <h4 class="m-b-5">KAS TELLER 2</h4>
                                </div>

                                

                                
                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalTL2d" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahantl2d" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpengurangantl2d" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakirtl2d" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                                                           
                               




                                  


                            </div>


                        </div>

                    </div> <!-- end col -->
                           <div class="col-md-4">
                        <div class="text-center card-box">

                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                                <div class="">
                                    <h4 class="m-b-5">KAS HEAD TELLER</h4>
                                </div>

                                

                                <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Message Now</button>--%>

                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalht" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahanht" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenguranganht" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakirht" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    
                                    </div>
                                </div>

                            </div>


                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                                <div class="">
                                    <h4 class="m-b-5">KAS HEAD TELLER + KAS TELLER 1</h4>
                                </div>

                                

                                <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Message Now</button>--%>

                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalTlHT" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahanTlHT" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenguranganTlHT" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                          <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="LblsaldoakhirTlHT" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>



                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                           <div class="col-md-4">
                        <div class="text-center card-box">

                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                                <div class="text-dark">
                                    <h4 class="m-b-5">BANK CABANG</h4>
                                </div>

                                

                                <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Message Now</button>--%>

                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalbank" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahanbank" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenguranganbank" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakirbank" Font-Bold="true" runat="server" Text="0"></asp:Label></h4></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>

                </div>
                <!-- end row -->


                     



                     <div class="row">
                    <div class="col-sm-12">
                        <!-- meta -->
                        <div class="profile-user-box card-box bg-custom">
                            <div class="row">
                                <div class="col-sm-6">
                                    <%--<span class="pull-left mr-3"><img src="assets/images/users/avatar-1.jpg" alt="" class="thumb-lg rounded-circle"></span>--%>
                                    <div class="media-body text-white">
                                        <h4 class="mt-1 mb-1 font-18">Rekap Transaction</h4>
                                      </div>
                                </div>

                                <div class="col-sm-6">
                                    
                                    <div class="text-right">
                                        <asp:Button ID="lnkCari" class="btn btn-success waves-light text-white" runat="server" Text="View Rekap" OnClick="lnkCari_Click"/>
                                        <%--<asp:Button ID="Button1" class="btn btn-success waves-light text-white" runat="server" Text="View Rekap" OnClick="lnkCari_Click" />--%>

                                       
                                    </div>
                                </div>
                            </div>




                 
                            
                        </div>
                        <!--/ meta -->
                    </div>
                </div>


            </asp:Panel>

                       


                   <asp:Panel ID="Pnldata" Visible="false" runat="server">
                      
             
                             <div class="row">
                     <!-- end col -->

                    <div class="col-md-4">
                        <div class="text-center card-box">

                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                                <div class="">
                                    <h4 class="m-b-5">KAS TELLER 1</h4>
                                </div>

                                

                                <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Message Now</button>--%>

                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawaltl1" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahantl1" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpengurangantl1" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakhirtl1" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                               <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Teller 1</h4>
                                 
                        
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                         
                                         <asp:GridView ID="GRDTL1" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDTL1_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grouping">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ket")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penambahan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdbtl1" CssClass="" runat="server" Text='<%#Eval("penambahan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotaldbtl1" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pengurangan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdkrl1" CssClass="" runat="server" Text='<%#Eval("pengurangan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>         
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotalkrtl1" runat="server" Text="TOTAL" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                   
                                        
                    
                                    
                                    
                                   
                                 
                                    
                                </Columns>
                                            </asp:GridView>

                                     </div>
                                 </div>

                             </div>

           <div class="row">
                    <div class="col-sm-12">
                        <!-- meta -->
                        <div class="profile-user-box card-box bg-custom">
                            
                        </div>
                        <!--/ meta -->
                    </div>
                </div>

                                     <div class="">
                                    <h4 class="m-b-5">KAS TELLER 2</h4>
                                </div>

                                

                                
                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawaltl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahantl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpengurangantl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakhirtl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                               <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Teller 2</h4>
                                 
                        
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                         
                                         <asp:GridView ID="GRDTL2" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDTL2_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grouping">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ket")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penambahan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdbtl1" CssClass="" runat="server" Text='<%#Eval("penambahan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotaldbtl1" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pengurangan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdkrl1" CssClass="" runat="server" Text='<%#Eval("pengurangan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>         
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotalkrtl1" runat="server" Text="TOTAL" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                   
                                        
                    
                                    
                                    
                                   
                                 
                                    
                                </Columns>
                                            </asp:GridView>

                                     </div>
                                 </div>

                             </div>
                                
                               




                                  


                            </div>

                        </div>

                    </div> <!-- end col -->
                           <div class="col-md-4">
                        <div class="text-center card-box">

                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                            

                                <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Head Teller </h4>
                                 
                        
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                         
                                         <asp:GridView ID="GRDHT" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDHT_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grouping">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ket")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penambahan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdbtl1" CssClass="" runat="server" Text='<%#Eval("penambahan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotaldbtl1" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pengurangan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdkrl1" CssClass="" runat="server" Text='<%#Eval("pengurangan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>         
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotalkrtl1" runat="server" Text="TOTAL" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                   
                                        
                    
                                    
                                    
                                   
                                 
                                    
                                </Columns>
                                            </asp:GridView>

                                     </div>
                                 </div>
                                 

                             </div>


                                <div class="row">
                    <div class="col-sm-12">
                        <!-- meta -->
                        <div class="profile-user-box card-box bg-custom">
                            
                        </div>
                        <!--/ meta -->
                    </div>
                </div>
                                    

           

                                     <div class="">
                                    <h4 class="m-b-5">Cadangan/Jaminan Angsuran</h4>
                                </div>

                                

                                
                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalcdgtl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahancdgtl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpengurangancdgtl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakhircdgtl2" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>     
                                

                               <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Cadangan/Jaminan Angsuran</h4>
                                 
                        
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                         
                                         <asp:GridView ID="Grdcdgtl2" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="Grdcdgtl2_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grouping">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ket")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penambahan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdbtl1" CssClass="" runat="server" Text='<%#Eval("penambahan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotaldbtl1" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pengurangan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdkrl1" CssClass="" runat="server" Text='<%#Eval("pengurangan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>         
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotalkrtl1" runat="server" Text="TOTAL" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                   
                                        
                    
                                    
                                    
                                   
                                 
                                    
                                </Columns>
                                            </asp:GridView>

                                     </div>
                                 </div>

                             </div>
                                
                               


                            </div>

                        </div>

                    </div>
                       
                                 
                                 
                                 
                                 
                                 
                                 <div class="col-md-4">
                        <div class="text-center card-box">

                            <div class="member-card pt-2 pb-2">
                                <%--<div class="thumb-lg member-thumb m-b-10 mx-auto">
                                    <img src="assets/images/users/avatar-3.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                </div>--%>

                                                 

                                <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Bank Cabang</h4>                                 
                        
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                         
                                         <asp:GridView ID="GRDBANK" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRDBK_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grouping">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ket")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penambahan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdbtl1" CssClass="" runat="server" Text='<%#Eval("penambahan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotaldbtl1" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pengurangan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdkrl1" CssClass="" runat="server" Text='<%#Eval("pengurangan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>         
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotalkrtl1" runat="server" Text="TOTAL" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                   
                                        
                    
                                    
                                    
                                   
                                 
                                    
                                </Columns>
                                            </asp:GridView>

                                     </div>
                                 </div>

                             </div>

                                <div class="row">
                    <div class="col-sm-12">
                        <!-- meta -->
                        <div class="profile-user-box card-box bg-custom">
                            
                        </div>
                        <!--/ meta -->
                    </div>
                </div>

                                         <div class="">
                                    <h4 class="m-b-5">Persediaan Materai</h4>
                                </div>

                                

                                
                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalmaterai" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahanmaterai" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenguranganmaterai" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakhirmaterai" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                               <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Persediaan Materai</h4>
                                 
                        
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                         
                                         <asp:GridView ID="GrdMaterai" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GrdMaterai_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grouping">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ket")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penambahan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdbtl1" CssClass="" runat="server" Text='<%#Eval("penambahan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotaldbtl1" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pengurangan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdkrl1" CssClass="" runat="server" Text='<%#Eval("pengurangan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>         
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotalkrtl1" runat="server" Text="TOTAL" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                   
                                        
                    
                                    
                                    
                                   
                                 
                                    
                                </Columns>
                                            </asp:GridView>

                                     </div>
                                 </div>

                             </div>
                                
                               <div class="row">
                    <div class="col-sm-12">
                        <!-- meta -->
                        <div class="profile-user-box card-box bg-custom">
                            
                        </div>
                        <!--/ meta -->
                    </div>
                </div>

                                
                                         <div class="">
                                    <h4 class="m-b-5">Pendapatan Provisi Anggota</h4>
                                </div>

                                

                                
                                <div class="mt-4">
                                    <div class="row">
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoawalprovisi" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Awal</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenambahanprovisi" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Penambahan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblpenguranganprovisi" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Pengurangan</p>
                                            </div>
                                        </div>
                                        <div class="col-3">
                                            <div class="mt-3">
                                                <h4 class="m-b-5"><asp:Label ID="lblsaldoakhirprovisi" Font-Bold="true" runat="server" Text="0"></asp:Label></h4>
                                                <p class="mb-0 text-muted">Saldo Akhir</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>                                
                               <div class="card-box">
                                 <h4 class="m-t-0 header-title">Transaksi Pendapatan Provisi Anggota</h4>
                                 
                        
                                 <div class="form-row">
                                     <div class="form-group" style="width: 95%;">
                                         
                                         <asp:GridView ID="GrdProvisi" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GrdProvisi_RowDataBound" AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grouping">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ket")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penambahan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdbtl1" CssClass="" runat="server" Text='<%#Eval("penambahan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotaldbtl1" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pengurangan">
                                        <ItemTemplate>
                                            <asp:Label ID="Lbdkrl1" CssClass="" runat="server" Text='<%#Eval("pengurangan","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>         
                                         <FooterTemplate>
                                                         <asp:Label ID="lbltotalkrtl1" runat="server" Text="TOTAL" />
                                         </FooterTemplate>
                                    </asp:TemplateField>

                                   
                                        
                    
                                    
                                    
                                   
                                 
                                    
                                </Columns>
                                            </asp:GridView>

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
               
                <%--<asp:PostBackTrigger ControlID="btnCancel" />--%>
                         <%--<asp:PostBackTrigger ControlID="ddlteller" />--%>
              
                 <asp:PostBackTrigger ControlID="txttgltrnx" />
               <asp:PostBackTrigger ControlID="lnkCari" />
                 
                     
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



