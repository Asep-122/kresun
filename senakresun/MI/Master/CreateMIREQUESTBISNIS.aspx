<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CreateMIREQUESTBISNIS.aspx.cs" Inherits="MI_CreateMIREQUESTBISNIS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Create Request MI</title>
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
        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

        }


         <%--function CheckFileExistence() {

            var filePath = document.getElementById('<%= this.fileuploadktp.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto KTP belum di pilih!");


                return false;
            }
        }--%>

        function sweetAlertCancel(btnBack,errMsg) {
            if (btnBack.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnBack.dataset.confirmed = false;
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
                        btnBack.dataset.confirmed = true;
                        redirect();
                  
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
                }
        }

        function sweetAlertsave(btnrequest,errMsg) {
            if (btnrequest.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnrequest.dataset.confirmed = false;
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
                        btnrequest.dataset.confirmed = true;
                        btnrequest.click();
                  
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
                }
        }
        function AlertRequest(btnRequest, errMsg) {
            if (btnRequest.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnRequest.dataset.confirmed = false;
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
                        btnRequest.dataset.confirmed = true;
                        btnRequest.click();

                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
            }
        }
        
      
        function redirect() {
		  location.href = 'CreateMI.aspx ';
		}

        function WarningDate(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

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
        //function checkTextField1(field) {
        //    if (field.value == '' || field.value == 0 || field.value == '.') {
        //        field.value = 0;
        //    }


        //    var nilai = field.value.split(',').join('');
        //    var output = nilai;
        //    if (parseFloat(nilai)) {
        //        input = new String(nilai); // so you can perform string operations
        //        var parts = input.split("."); // remove the decimal part
        //        parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
        //        output = parts.join(".");
        //    }
        //    field.value = output;
        //}

        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

               {
                $('#<%=txtnote.ClientID%>').prop('maxlength', 150);
                
            }
   
            Resize();
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
            $('#txttgllahiranak3').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak4').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttglsimulasi').datepicker({
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
                var table = $('#GrdApp').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GrdReject').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var table = $('#GrdPending').dataTable({
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
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
        function kepo2(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
    </script>
    <style>
        .Spaceradio label
        {
           margin-left: 10px;
        }
        .aturbawah
        {
            margin-bottom: 0.1rem;
        }
        .aturkiri
        {
            margin-left: 0;
        }
        .tabdetil
        {
            width: 187px;
            /*right: 241px;*/
            /*//position: fixed;*/
        }
        .aturbawahpinjaman
        {
           margin-bottom: -45px
        }
        .texttengah
        {
            text-align: center;
        }
        .kiritengah
        {
            align-self: center;

        }
        .auto-style1 {
            left: 0px;
            top: 0px;
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Memo Internal</a></li>
                                        <li class="breadcrumb-item"><a href="#">Master</a></li>
                                        <li class="breadcrumb-item"><a href="CreateMI.aspx">Simulai & Request</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Memo Internal - Request Bisnis</h4>
                            </div>
                        </div>
                    </div>
                       <asp:Panel ID="pnlData" runat="server" Visible="false">
                        
                            <div class="text-center mt-4 mb-4">
                                <div class="row">
                                    <div class="col-xs-6 col-sm-3">
                                        <div class="card-box widget-flat border-custom bg-custom text-white">
                                            <i class="fi-tag"></i>
                                            <h3 class="m-b-10"><asp:Label ID="idtotalreq" runat="server" Text="0"></asp:Label></h3>
                                            <p class="text-uppercase m-b-5 font-13 font-600">Normal</p>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3">
                                        <div class="card-box bg-primary widget-flat border-primary text-white">
                                            <i class="fi-archive"></i>
                                            <h3 class="m-b-10"><asp:Label ID="idtotalmemo" runat="server" Text="0"></asp:Label></h3>
                                            <p class="text-uppercase m-b-5 font-13 font-600">Request Memo</p>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3">
                                        <div class="card-box bg-warning widget-flat border-success bg-success text-white">
                                            <i class="fi-help"></i>
                                            <h3 class="m-b-10"><asp:Label ID="idtotalapp" runat="server" Text="0"></asp:Label></h3>
                                            <p class="text-uppercase m-b-5 font-13 font-600">MEMO APPROVE</p>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3">
                                        <div class="card-box bg-danger widget-flat border-danger text-white">
                                            <i class="fi-delete"></i>
                                            <h3 class="m-b-10">
                                                <asp:Label ID="idtotalreject" runat="server" Text="0"></asp:Label></h3>
                                            <p class="text-uppercase m-b-5 font-13 font-600">MEMO&nbsp; TOLAK</p>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        
                    </asp:Panel>

               <%--     <asp:Panel ID="pnlHeader" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Simulasi" OnClick="btnAdd_Click" />
                                    
                                </div>
                            </div>
                        </div>
                    </asp:Panel>--%>

                


                    <!-- Panel Front Gridview -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box ribbon-box">
                                    <div class="col-md-6" id="idcari" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-form-label">Search By</label>
                                            <div class="form-group col-md-6">
                                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                    <asp:ListItem Value=" and NoSimulasi ">No Simulasi</asp:ListItem>
                                                     <asp:ListItem Value=" and Nopen ">Nopen</asp:ListItem>
                                                      <asp:ListItem Value=" and NamaDebitur ">Nama</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                                                <br>
                                                <asp:Button ID="lnkCari" class="btn btn-success waves-light text-white" runat="server" Text="Cari" OnClick="lnkCari_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <h4 class="m-t-0 header-title" id="idjudulsemua" runat="server" visible="true">Daftar Request Bisnis</h4>
                                    <div class="form-row" id="divtotalrequest" runat="server" visible="true">

                                         
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Memo Internal" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoMemoInternal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoMemoInternal")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NoSimulasi" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrequest" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoSimulasi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    
                                                            <asp:TemplateField HeaderText="Tgl Memo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglsimulasix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglmemo","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltypememo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namamemo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamaStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Request User">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrequestuser" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "requestuser")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="User Network">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluserapprovalNetwork" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "userapprovalNetwork")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status Network">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatusapprovalnetwork" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusapprovalnetwork")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Network">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tglapprovalnetwork" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglapprovalnetwork","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note Network">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcatatannetwork" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatannetwork")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                                 
                                                    <asp:TemplateField HeaderText="User operational">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluserapprovaloperational" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "userapprovaloperational")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status operational">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatusapprovaloperational" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusapprovaloperational")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Operational">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tglapprovaloperational" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglapprovaloperational","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note Operational">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcatatanoperational" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatanoperational")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="User Bisnis">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluserapprovalbisnis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "userapprovalbisnis")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status Bisnis">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatusapprovalbisnis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusapprovalbisnis")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Bisnis">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tglapprovalBisnis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglapprovalbisnis","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note Bisnis">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcatatanbisnis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatanbisnis")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                     <%--<asp:TemplateField HeaderText="User GM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluserapprovalGM" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "userapprovalGM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status GM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatusapprovalGM" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusapprovalGM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl GM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tglapprovalGM" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglapprovalGM","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note GM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcatatanGM" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "catatanGM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Permohonan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpermohonan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Permohonan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>--%>
                                           
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                              <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="APPROVAL" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:Button>
                                                           
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                               <%--     <asp:TemplateField HeaderText="">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                              <asp:Button ID="btnDelete" class="btn btn-success waves-light text-white" runat="server" OnClientClick="return confirm('Apakah anda yakin ingin mencetak data ini ?');"  Text="Report" CommandName="DEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:Button>
                                                           
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
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
                                
                  <div class="row">



                      <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA PRODUCT & SUMBERDANA</div>
                                    <p class="text-muted font-14 m-b-30"></p>    
                                   <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">No Memo Internal</label>
                                    <asp:TextBox ID="txtmemointernal" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>                                                       
                                </div> 
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">No Simulasi</label>
                                    <asp:TextBox ID="txtnosimulasi" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>                                                       
                                </div>
                                <div class="form-group">
                                    <label for="inputpid" class="col-6 col-form-label">Tanggal Memo Internal</label>
                                    <asp:TextBox ID="txttglsimulasi" Enabled="false" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>                                                       
                                </div>


                                                                
                                </div>                                                    
                            </div>
                          </div>

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <%--<div class="ribbon ribbon-custom">Periode</div>--%>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                        <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputpid" class="col-6 col-form-label">Sumber Dana</label>
                                         <asp:DropDownList ID="ddlsumberdana" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlsumberdana_SelectedIndexChanged"></asp:DropDownList>                                           

                                       </div>
                                    <div class="form-group col-md-6">
                                     <label for="inputCity" class="col-form-label">Kode Produk</label>
                                        <asp:DropDownList ID="ddlkodeproduk" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlkodeproduk_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    </div>
                                    
                                </div>
                                        
                                    <div class="form-group">
                                         <label for="inputState" class="col-form-label">Status Take Over</label>
                                         <asp:DropDownList ID="ddlstatustakeover" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlsumberdana_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Selected="True">-</asp:ListItem>
                                                        <asp:ListItem Value="1">Take Over</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>    
                                    
                                                     
                                </div>                                                    
                            </div>
                           </div>

                                         
                    </div>
            </asp:Panel>
                    

                    <%--Panel pemohon--%>
                    
                    <asp:Panel ID="pnldatapemohon" Visible="false" runat="server">
                        <div class="row ">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                  <%--DATA PENSIUN PEMINJAM--%>
                                <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">--%>
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">DATA PENSIUN PEMINJAM</div>
                                        <p class="m-b-0"></p>
                                        <div class="form-row">
                                            <div class="form-group col-md-12">
                                                <asp:HiddenField ID="hdnusiaanak" runat="server" />
                                                <asp:HiddenField ID="hdnpengajuanbulan" runat="server" />
                                                <asp:HiddenField ID="hdnpengajuanhari" runat="server" />
                                                <asp:HiddenField ID="hdntempotahun" runat="server" />
                                                <asp:HiddenField ID="hdntempobulan" runat="server" />
                                                <asp:HiddenField ID="hdntempohari" runat="server" />
                                                <asp:HiddenField ID="hdnusiaasuransi" runat="server" />
                                                <asp:HiddenField ID="hdnjwmax" runat="server" />
                                                <asp:HiddenField ID="hdntglhariini" runat="server" />
                                                <asp:HiddenField ID="hdntahunjw" runat="server" />
                                                <asp:HiddenField ID="hdngajitunjanak" runat="server" />
                                                <asp:HiddenField ID="hdnjumlahanak" runat="server" />
                                                <asp:HiddenField ID="hdngajinol" runat="server" />
                                                <asp:HiddenField ID="hdntglpk" runat="server" />
                                                <asp:HiddenField ID="hdntglsimulasiDB" runat="server" />
                                                <div class="form-row aturbawah aturkiri">
                                                    <asp:Label ID="Label2" runat="server" class="col-form-label tabdetil">Nomor Pensiun</asp:Label>
                                                    <asp:TextBox ID="txtnopen" CssClass="form-control" MaxLength="12" onkeypress="return kepo2(event);" runat="server" Width="219px" AutoPostBack="true" OnTextChanged="txtnopen_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lbltaspenasabri" runat="server" class="col-form-label tabdetil" Font-Bold="true" ForeColor="Red" Font-Italic="true" Visible="false"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True" ControlToValidate="txtnopen" ErrorMessage="Nopen tidak boleh kosong" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-row aturkiri">
                                                    <asp:Label ID="lblnopenbaru" runat="server" class="col-form-label " Font-Bold="true" ForeColor="Red" Font-Italic="true" Visible="true"></asp:Label>
                                                    <asp:Label ID="lblnopenlama" runat="server" class="col-form-label " Font-Bold="true" ForeColor="Red" Font-Italic="true" Visible="true"></asp:Label></div>
                                                &nbsp
                                                <div class="form-row aturbawah aturkiri">
                                                    <asp:Label ID="Label9" runat="server" class="col-form-label tabdetil">Nama Pensiunan</asp:Label>
                                                    <asp:TextBox ID="txtnamapensiun" CssClass="form-control" MaxLength="100" runat="server" Width="219px" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapensiun" ErrorMessage="Nama tidak boleh kosong" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                &nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label12" runat="server" class="col-form-label tabdetil">Kode Jiwa</asp:Label>
                                                <%--<asp:TextBox ID="txtkodejiwa" CssClass="form-control" MaxLength="16" runat="server" Width="219px"></asp:TextBox>--%>
                                                <div style="width: 227px;" tabindex="2">
                                                    <asp:DropDownList ID="ddlkodejiwa" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlKodeJiwa_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                                &nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label5" runat="server" class="col-form-label tabdetil">Status</asp:Label>
                                                <div style="width: 39%;" tabindex="3">
                                                    <asp:DropDownList ID="ddlstatusnikah"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlstatusnikah_OnChange">
                                                        <asp:ListItem Value="">Pilih Status</asp:ListItem>
                                                        <asp:ListItem Value="0">Tidak Kawin</asp:ListItem>
                                                        <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                                &nbsp
                                            <div class="form-row aturbawah aturkiri"  >
                                                <asp:Label ID="Label55" runat="server" class="col-form-label tabdetil">Pensiunan</asp:Label>
                                                <div style="width: 39%;" tabindex="4">
                                                    <asp:DropDownList ID="ddlpensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlpensiun_OnChange">
                                                        <asp:ListItem Value="">Pilih Pensiun</asp:ListItem>
                                                        <asp:ListItem Value="1">Sendiri</asp:ListItem>
                                                        <asp:ListItem Value="2">Janda</asp:ListItem>
                                                        <asp:ListItem Value="3">Duda</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                                &nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label1" runat="server" class="col-form-label tabdetil">Status SK</asp:Label>
                                                <div style="width: 39%;" tabindex="5">
                                                    <asp:DropDownList ID="ddlstatussk"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlstatussk_OnChange">
                                                        <asp:ListItem Value="">Pilih Status</asp:ListItem>
                                                      <asp:ListItem Value="0">Belum</asp:ListItem>
                                                        <asp:ListItem Value="1">Sudah Diterima</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                                &nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label3ccc" runat="server" class="col-form-label  tabdetil">Tanggal Lahir</asp:Label>
                                                <asp:TextBox ID="txttgllahir" CssClass="form-control" TextMode="DateTime" runat="server" Width="219px" AutoPostBack="true" OnTextChanged="tgllahir_OnChange" TabIndex="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="True" ControlToValidate="txttgllahir" ErrorMessage="Tanggal lahir tidak boleh kosong" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            </div>
                                            <%-- <div class="form-group col-md-12">
                                               BEKAS USIA PENGAJUAN
                                            </div>--%>
                                            <%--<div class="form-group col-md-12">
                                                bekas usiajatuh tempo
                                            </div>--%>
                                        </div>
                                    </div>
                                <%--</div>--%>
                                <%-- DATA GAJI--%>
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA GAJI</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-12">
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                            <div class="form-row aturkiri">
                                                <asp:Label ID="Label63" runat="server" class="col-form-label tabdetil">Gaji Bersih Display</asp:Label>
                                                <asp:TextBox ID="txtgajibersihdisplay" CssClass="form-control" MaxLength="20" runat="server" Width="219px" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"  OnTextChanged="gajidisplay_TextChanged" TabIndex="7">0</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" SetFocusOnError="True" ControlToValidate="txtgajibersihdisplay" ErrorMessage="Gaji Berdih tidak boleh kosong" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            &nbsp
                                            <div class="form-row  aturkiri">
                                                <asp:Label ID="Label64" runat="server" class="col-form-label tabdetil" Width="">Gaji Pokok</asp:Label>
                                                <asp:TextBox ID="txtgajipokok" CssClass="form-control" MaxLength="20" runat="server" Width="219px" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged="gajipokok_OnChange" TabIndex="8">0</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" SetFocusOnError="True" ControlToValidate="txtgajipokok" ErrorMessage="Gaji Pokok tidak boleh kosong (Isi angka 0 Jika tidak dibutuhkan)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-row  aturkiri">
                                                <asp:Label ID="Label14" runat="server" class="col-form-label tabdetil" Width="">Tunjangan Anak</asp:Label>
                                                <asp:TextBox ID="txttunjangananak" CssClass="form-control" MaxLength="20" runat="server" Width="219px" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged="txttunjangananak_OnChange" TabIndex="9">0</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--PENGURANGAN TUNJANGAN ISTRI ATAU ANAK--%>
                                    <div class="card-box ribbon-box">
                                        <div class="ribbon ribbon-custom">PENGURANGAN TUNJANGAN ISTRI ATAU ANAK</div>
                                        <p class="m-b-0"></p>
                                        <div class="form-row">
                                            <div class="form-group col-md-12">
                                                <div class="form-row aturbawah aturkiri">
                                                    <asp:Label ID="Label87" runat="server" class="col-form-label  tabdetil">Gaji Pokok</asp:Label>
                                                    <asp:TextBox ID="txtPLgajipokok"  CssClass="form-control" runat="server" Width="45px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                    <asp:TextBox ID="txtPLgajipokok1" OnTextChanged="txtPLgajipokok1_OnChange" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                    <asp:TextBox ID="txtPlgajipokok2" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>
                                                </div>
                                                &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label88" runat="server" class="col-form-label  tabdetil">Tunjangan Anak</asp:Label>
                                                    <asp:TextBox ID="txtPLtunjanak"  OnTextChanged="txtPLtunjanak1_OnChange" CssClass="form-control" runat="server" Width="45px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                    <asp:TextBox ID="txtPLtunjanak1" OnTextChanged="txtPLtunjanak1_OnChange" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                    <asp:TextBox ID="txtPLtunjanak2" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>
                                            </div>
                                                &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label89" runat="server" class="col-form-label  tabdetil">Tunjangan Istri</asp:Label>
                                                <%--<div style="width:45px;">
                                                    <asp:DropDownList ID="txtPLtunjistri" CssClass="form-control" Enabled="true" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="provisi_OnChange">
                                                        <asp:ListItem Value="0"  Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtPLtunjistri"  OnTextChanged="txtPLtinjistri1_OnChange" CssClass="form-control" runat="server" Width="45px" Enabled="false" MaxLength="2" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                <%--</div>--%>
                                                    <asp:TextBox ID="txtPLtunjistri1" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                    <asp:TextBox ID="txtPLtunjistri2" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>
                                            </div>
                                                &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label100" runat="server" class="col-form-label  tabdetil">Tunjangan Beras</asp:Label>
                                                    <asp:TextBox ID="txtPLtunjberas"  OnTextChanged="txtPLtunjberas1_OnChange" CssClass="form-control" runat="server" Width="45px" MaxLength="2" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                    <asp:TextBox ID="txtPLtunjberas1" OnTextChanged="txtPLtunjberas1_OnChange" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>&nbsp
                                                    <asp:TextBox ID="txtPLtunjberas2" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);">0</asp:TextBox>
                                            </div>
                                                &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label3" runat="server" class="col-form-label  tabdetil">Total Gaji Dikurang Tunjangan</asp:Label>
                                                <div runat="server" style="width: 205px;"></div>
                                                <asp:TextBox ID="txtPLterimabersih" CssClass="form-control" runat="server" Width="160px" Enabled="false" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged="PLterimabersih_OnChange">0</asp:TextBox>
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <%--DATA Anak PEMINJAM--%>
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA ANAK PEMINJAM</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-12">
                                            <div class="form-row aturbawah aturkiri" id="divanak1" runat="server" >
                                                <asp:Label ID="Label56" runat="server" class="col-form-label  tabdetil">Tanggal Lahir Anak 1</asp:Label>
                                                <asp:TextBox ID="txttgllahiranak1" CssClass="form-control" Enabled="false" TextMode="DateTime" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="tgllahiranak1_OnChange" TabIndex="10"></asp:TextBox>&nbsp&nbsp
                                                    <asp:TextBox ID="txttahunanak1" CssClass="form-control" Enabled="false" runat="server" Width="62px">0</asp:TextBox>
                                                <asp:Label ID="Label4" runat="server" class="col-form-label ">Tahun</asp:Label>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri" id="divanak2" runat="server">
                                                <asp:Label ID="Label20" runat="server" class="col-form-label  tabdetil">Tanggal Lahir Anak 2</asp:Label>
                                                <asp:TextBox ID="txttgllahiranak2" CssClass="form-control" Enabled="false" TextMode="DateTime" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="tgllahiranak2_OnChange" TabIndex="11"></asp:TextBox>&nbsp&nbsp
                                                <asp:TextBox ID="txttahunanak2" CssClass="form-control" Enabled="false" runat="server" Width="62px">0</asp:TextBox>
                                                <asp:Label ID="Label65" runat="server" class="col-form-label  ">Tahun</asp:Label>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri" id="divanak3" runat="server" visible="false">
                                                <asp:Label ID="Label39" runat="server" class="col-form-label  tabdetil">Tanggal Lahir Anak 3</asp:Label>
                                                <asp:TextBox ID="txttgllahiranak3" CssClass="form-control" Enabled="false" TextMode="DateTime" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="tgllahiranak3_OnChange" TabIndex="12"></asp:TextBox>&nbsp&nbsp
                                                    <asp:TextBox ID="txttahunanak3" CssClass="form-control" Enabled="false" runat="server" Width="62px">0</asp:TextBox>
                                                <asp:Label ID="Label66" runat="server" class="col-form-label  ">Tahun</asp:Label>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri" id="divanak4" runat="server" visible="false">
                                                <asp:Label ID="Label54" runat="server" class="col-form-label  tabdetil">Tanggal Lahir Anak 4</asp:Label>
                                                <asp:TextBox ID="txttgllahiranak4" CssClass="form-control" Enabled="false" TextMode="DateTime" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="tgllahiranak4_OnChange" TabIndex="13"></asp:TextBox>&nbsp&nbsp
                                                    <asp:TextBox ID="txttahunanak4" CssClass="form-control" Enabled="false" runat="server" Width="62px">0</asp:TextBox>
                                                <asp:Label ID="Label67" runat="server" class="col-form-label  ">Tahun</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--      Detail Pinjaman--%>
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Detail Pinjaman</div>
                                    <p class="m-b-0"></p>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="form-row">
                                            <div class="form-group col-md-3">
                                                <label for="inputCity" class="col-form-label">Jangka Waktu</label>
                                                <%--<div class="col-md-12">--%>
                                                    <asp:DropDownList ID="ddljangkawaktu" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddljangkawaktu_OnChange" TabIndex="14">
                                                    </asp:DropDownList>
                                               <%-- </div>--%>
                                            </div>&nbsp&nbsp&nbsp
                                            <div class="form-group col-md-2">
                                                <label for="inputState" class="col-form-label">JW Max</label>
                                                <asp:TextBox ID="txtjwmax" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" required="required" CssClass="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>&nbsp&nbsp&nbsp
                                            <div class="form-group col-md-2">
                                                <label for="inputState" class="col-form-label">JW Tahun</label>
                                                <asp:TextBox ID="txtjwtahun" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" required="required" CssClass="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-group col-md-3">
                                                <label for="inputState" class="col-form-label">Bunga Asuransi</label>
                                                <asp:TextBox ID="txtbungaasuransi" Enabled="false" Text="0" required="required" CssClass="form-control"  runat="server"></asp:TextBox>
                                            </div>&nbsp&nbsp&nbsp
                                            <div class="form-group col-md-3">
                                                <label for="inputZip" class="col-form-label">Usia Asuransi</label>
                                                <asp:TextBox ID="txtusiaasuransi" CssClass="form-control" Enabled="false" MaxLength="15" runat="server" onkeypress="return isnumerickey(event);" AutoPostBack="true" Text="0"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-row ">
                                            <div class="form-group col-md-4">
                                                <asp:Label ID="Label11" runat="server" class="col-form-label tabdetil" Width="">Plafond</asp:Label>
                                                <asp:TextBox ID="txtPlafond" CssClass="form-control" MaxLength="15" runat="server" Width="100%" onkeypress="return isnumerickey(event);" Text="0"  onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged="plafond_OnChange" TabIndex="15" ></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:Label ID="Label57" runat="server" class="col-form-label tabdetil" Width="">Plafond Max</asp:Label>
                                                <asp:TextBox ID="txtplafondmax" CssClass="form-control" MaxLength="15" Enabled="false" runat="server" Width="100%" onkeypress="return isnumerickey(event);" Text="0"  onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                                            </div>
                                            <%--<div class="form-group col-md-4">
                                                <asp:Label ID="Label16" runat="server" class="col-form-label tabdetil" Width="">Sisa Gaji</asp:Label>
                                                <asp:TextBox ID="txtsisagaji" CssClass="form-control" MaxLength="15" Enabled="false" runat="server" Width="100%" onkeypress="return isnumerickey(event);" Text="0"  onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                                            </div>--%>
                                        </div>
                                        <div class="form-row ">
                                            <div class="form-group col-md-4 kiritengah">
                                                <asp:Label ID="Label21" runat="server" class="col-form-label tabdetil" Visible="false">Gaji Tunj.Anak</asp:Label>
                                                <asp:Label ID="Label16" runat="server" class="col-form-label tabdetil" Width="">Sisa Gaji</asp:Label>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtgajitunjanak" CssClass="form-control col-4 " Enabled="false" Visible="false" MaxLength="15" runat="server" Width="100%" onkeypress="return isnumerickey(event);" Text="0" onblur="checkTextField1(this);" AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                                                <asp:TextBox ID="txtsisagaji" CssClass="form-control" MaxLength="15" Enabled="false" runat="server" Width="100%" onkeypress="return isnumerickey(event);" Text="0" onblur="checkTextField1(this);" AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-row ">
                                            <div class="form-group col-md-4">
                                                <asp:Label ID="Label58" runat="server" class="col-form-label ">Bunga</asp:Label>
                                                <asp:TextBox ID="txtbunga" CssClass="form-control" MaxLength="10" runat="server" Width="100%" Enabled="false" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" Text="0" AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>&nbsp&nbsp&nbsp
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:Label ID="Label59" runat="server" class="col-form-label  justify-content-center">Angsuran</asp:Label>
                                                <asp:TextBox ID="txtangsuran" CssClass="form-control"  Enabled="false" runat="server" Width="100%" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" Text="0"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged="angsuran_OnChange"></asp:TextBox>&nbsp&nbsp&nbsp
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:Label ID="Label60" runat="server" class="col-form-label tabdetil" Width="">Angsuran Max</asp:Label>
                                                <asp:TextBox ID="txtangsuranmax" CssClass="form-control" Enabled="false" MaxLength="15" runat="server" Width="100%" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" Text="0" AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-group col-md-4 kiritengah">
                                                <asp:Label ID="Label62" runat="server" class="col-form-label tabdetil" >Pelunasan Take Over</asp:Label>
                                            </div>
                                            <div class="form-group col-md-4"> 
                                                <asp:TextBox ID="txtpelunasantakeover" CssClass="form-control col-4" MaxLength="15" runat="server" Width="100%" Text="0" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged="PelunasanTOP_OnChange"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Informasi Data--%>
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">INFORMASI DATA</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-12">
                                            <div class="col-md-12" style="text-align: center; margin-top: -17px;">
                                                <label for="inputCity" class="col-form-label font-weight-bold " runat="server" style="font-size: medium;">Usia Pengajuan</label>
                                            </div>
                                            <div class="form-row" style="text-align: center;">
                                                <div class="form-group col-md-4 justify-content-center">
                                                    <label for="inputCity" class="col-form-label">Tahun</label>
                                                    <asp:TextBox ID="txttahunpengajuan" CssClass="form-control" Enabled="false" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="inputState" class="col-form-label">Bulan</label>
                                                    <asp:TextBox ID="txtbulanpengajuan" CssClass="form-control" Enabled="false" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="inputZip" class="col-form-label">Hari</label>
                                                    <asp:TextBox ID="txtharipengajuan" CssClass="form-control" Enabled="false" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12" style="text-align: center; margin-top: -17px;">
                                                <label for="inputCity" class="col-form-label font-weight-bold" runat="server" style="font-size: medium;">Usia Jatuh Tempo</label>
                                            </div>
                                            <div class="form-row" style="text-align: center;">
                                                <div class="form-group col-md-4 justify-content-center">
                                                    <label for="inputCity" class="col-form-label" style="text-align: center;">Tahun</label>
                                                    <asp:TextBox ID="txttahuntempo" CssClass="form-control" Enabled="false" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="inputState" class="col-form-label">Bulan</label>
                                                    <asp:TextBox ID="txtbulantempo" CssClass="form-control" Enabled="false" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label for="inputZip" class="col-form-label">Hari</label>
                                                    <asp:TextBox ID="txtharitempo" CssClass="form-control" Enabled="false" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12" style="text-align: center; margin-top: -17px;">
                                                <label for="inputCity" class="col-form-label font-weight-bold" runat="server" style="font-size: medium;">Keterangan</label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtketerangan1" CssClass="form-control" MaxLength="150" runat="server" Width="100%" onkeypress="return kepo(event);" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                &nbsp&nbsp                                                                               
                                                    <asp:TextBox ID="txtketerangan2" CssClass="form-control" MaxLength="150" runat="server" Width="100%" onkeypress="return kepo(event);" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                &nbsp&nbsp                                                                               
                                                    <asp:TextBox ID="txtketerangan3" CssClass="form-control" MaxLength="150" runat="server" Width="100%" onkeypress="return kepo(event);" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" id="divdataanak" runat="server">
                            </div>  --%>
                            <%--<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">--%>
                                <%--PENGURANGAN TUNJANGAN ISTRI ATAU ANAK--%>
                                <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                </div>
                            </div>--%>
                            <%--<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            </div>--%>
                            <%--      Detail Pinjaman--%>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Deviasi</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                            <asp:HiddenField ID="HiddenField5" runat="server" />
                                            <asp:HiddenField ID="HiddenField6" runat="server" />
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label15" runat="server" class="col-form-label tabdetil">Jenis Deviasi</asp:Label>
                                                <div style="width: 147px;">
                                                    <asp:DropDownList ID="ddljenisdeviasi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddljenisdeviasi_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label92" runat="server" class="col-form-label tabdetil"> Deviasi Asuransi</asp:Label>
                                                <div style="width: 57px;" Visible="false" runat="server">
                                                    <asp:DropDownList ID="txttittleasuransi" CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="asuransi_OnChange" >
                                                        <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:TextBox ID="txtdeviasiasuransi" CssClass="form-control" MaxLength="15" runat="server" Width="100px"  Enabled="false" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="txtdeviasiasuransi_SelectedIndexChanged"></asp:TextBox>
                                                <asp:Label ID="Label8" runat="server" class="col-form-label">%</asp:Label>
                                                <asp:TextBox ID="txtasuransi" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false" Visible="false" onkeypress="return isnumerickey(event);" AutoPostBack="true"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server" SetFocusOnError="True" ControlToValidate="txtasuransi" ErrorMessage="Asuransi tidak boleh kosong (Isi angka 0 Jika tidak dibutuhkan)" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri " runat="server" visible="false">
                                                <asp:Label ID="Label51" runat="server" class="col-form-label tabdetil" Width="">Deviasi Keanggotaan</asp:Label>
                                                <%--<asp:TextBox ID="txttittlebunga" CssClass="form-control" runat="server" Width="45px" Enabled="false" AutoPostBack="true">0</asp:TextBox>&nbsp--%>
                                                <asp:TextBox ID="txtdeviasikeanggotaan" CssClass="form-control" MaxLength="15" runat="server" Width="219px"  Enabled="false" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" SetFocusOnError="True" ControlToValidate="txtbunga" ErrorMessage="Bunga tidak boleh kosong (Isi angka 0 Jika tidak dibutuhkan)" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri ">
                                                <asp:Label ID="Label7" runat="server" class="col-form-label tabdetil" Width="">Deviasi Bunga</asp:Label>
                                                <div style="width: 57px;" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlbungadeviasi" CssClass="form-control" runat="server" Enabled="false" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="bungadeviasi_OnChange" >
                                                        <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:TextBox ID="txtbungadeviasi" CssClass="form-control" Enabled="false" MaxLength="5" runat="server" Width="100px"  onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="txtbungadeviasi_OnChange"></asp:TextBox>
                                                <asp:Label ID="Label17" runat="server" class="col-form-label">%</asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:HiddenField ID="HDNProvisi" runat="server"/>
                                                <asp:Label ID="Label41" runat="server" class="col-form-label tabdetil">Deviasi Provisi</asp:Label>
                                                <div style="width: 57px;" runat="server" visible="false">
                                                    <asp:DropDownList ID="txttittleprovisi" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="provisi_OnChange">
                                                        <asp:ListItem Value="0">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:TextBox ID="txttittleprovisi2" CssClass="form-control" runat="server" Width="100px" Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="txttittleprovisi2_OnChange"></asp:TextBox>&nbsp
                                                <asp:Label ID="Label13" runat="server" class="col-form-label">%</asp:Label>
                                                <asp:TextBox ID="txtprovisi" CssClass="form-control" MaxLength="15" runat="server" Width="180px" Visible="false"  Enabled="false" onkeypress="return isnumerickey(event);"  ></asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" SetFocusOnError="True" ControlToValidate="txtprovisi" ErrorMessage="Provisi tidak boleh kosong  (Isi angka 0 Jika tidak dibutuhkan)" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label44" runat="server" class="col-form-label tabdetil">Deviasi Angsuran Diblokir</asp:Label>
                                                <div style="width: 57px;">
                                                    <asp:DropDownList ID="txttittlediblokir" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="angsblokir_OnChange">
                                                        <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <%--<asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>&nbsp&nbsp
                                                <%--<asp:TextBox ID="txttittlediblokir" CssClass="form-control" runat="server" Width="45px" Enabled="false" AutoPostBack="true">0</asp:TextBox>&nbsp--%>
                                                <asp:TextBox ID="txtangsblokir" CssClass="form-control" MaxLength="15" runat="server" Width="219px"  Enabled="false" onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" SetFocusOnError="True" ControlToValidate="txtangsblokir" ErrorMessage="Angs. Blokir tidak boleh kosong  (Isi angka 0 Jika tidak dibutuhkan)" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            &nbsp&nbsp&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label52" runat="server" class="col-form-label tabdetil">Deviasi Angsuran Dimuka</asp:Label>
                                                <div style="width: 57px;">
                                                    <asp:DropDownList ID="txttittledimuka" CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="angsdimuka_OnChange">
                                                        <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>&nbsp&nbsp
                                                <%--<asp:TextBox ID="txttittledimuka" CssClass="form-control" runat="server" Width="45px" Enabled="false" AutoPostBack="true">0</asp:TextBox>&nbsp--%>
                                                <asp:TextBox ID="txtangsdimuka" CssClass="form-control" MaxLength="15" runat="server" Width="219px" onkeypress="return isnumerickey(event);" Enabled="false" AutoPostBack="true" ></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" SetFocusOnError="True" ControlToValidate="txtangsdimuka" ErrorMessage="Angs. dimuka tidak boleh kosong  (Isi angka 0 Jika tidak dibutuhkan)" ForeColor="Red"></asp:RequiredFieldValidator--%>
                                                <%--</div> --%>
                                                <%--</div> --%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>

                    
                    <asp:Panel ID="Pnldepan" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server">
                                <div class="card-box ">
                                    
                                    <div class="form-row" >

                                        <div class="form-group col-md-6" style="justify-content:flex-start;" runat="server" >
                                          
                                                <asp:Button ID="btnbackcalculate" CausesValidation="False" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack_Click"/>
                                                <asp:Button ID="btnNextpage2" align="right"  class="btn btn-success waves-light text-white" runat="server" OnClick="btnnextpage2_Click" Text="NEXT"/>
                                                <asp:Button ID="btncalculate" align="right"  class="btn btn-success waves-light text-white" runat="server" Text="CALCULATE" OnClick="btnCalculate_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Melihat Hasil Simulasi ?');" />
                                         </div>                                                                              
                                    </div>
                               
                                </div>
                            </div>      
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    

                       <asp:Panel ID="Pnlhasil" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server" id="divhasilsimulasi">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Report Hasil Simulasi</div>
                                    <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <asp:HiddenField ID="HiddenField7" runat="server" />
                                            <asp:HiddenField ID="HiddenField8" runat="server" />
                                            <asp:HiddenField ID="HiddenField9" runat="server" />
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label70" runat="server" class="col-form-label tabdetil" Width=""> Nama Debitur</asp:Label>
                                                <asp:TextBox ID="txtRnamadebitur" CssClass="form-control" MaxLength="100" runat="server" Width="219px" Enabled="false" onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri ">
                                                <asp:Label ID="Label71" runat="server" class="col-form-label tabdetil" Width="">Instansi</asp:Label>
                                                <asp:TextBox ID="txtRinstansi" CssClass="form-control" Enabled ="false" MaxLength="15" runat="server" Width="219px" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label72" runat="server" class="col-form-label tabdetil">Takeover</asp:Label>
                                                <asp:TextBox ID="txtRto" CssClass="form-control" MaxLength="15" runat="server" Width="219px" Enabled="false" onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label73" runat="server" class="col-form-label tabdetil">Deviasi</asp:Label>
                                                <asp:TextBox ID="txtRdeviasi" CssClass="form-control" MaxLength="15" runat="server" Width="180px" Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label69" runat="server" class="col-form-label tabdetil">Plafond</asp:Label>
                                                <asp:TextBox ID="txtRplafond" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri" runat="server" visible="false">
                                                <asp:Label ID="Label75" runat="server" class="col-form-label tabdetil">Potongan</asp:Label>
                                                <asp:TextBox ID="txtRpotongan" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label76" runat="server" class="col-form-label tabdetil">Asuransi</asp:Label>
                                                <asp:TextBox ID="txtRtitleasuransi" CssClass="form-control" MaxLength="15" runat="server" Width="89px"  Enabled="false"  onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                <asp:Label ID="Label18" runat="server" class="col-form-label">%</asp:Label>
                                                <asp:TextBox ID="txtRasuransi" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);"  ></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label74" runat="server" class="col-form-label tabdetil">Provisi</asp:Label>
                                                <asp:TextBox ID="txtRtittleprovisi" CssClass="form-control" MaxLength="15" runat="server" Width="89px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true"></asp:TextBox>
                                                <asp:Label ID="Label19" runat="server" class="col-form-label">%</asp:Label>
                                                <asp:TextBox ID="txtRprovisi" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label77" runat="server" class="col-form-label tabdetil">Keanggotaan</asp:Label>
                                                <asp:TextBox ID="txtRkeanggotaan" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label78" runat="server" class="col-form-label tabdetil" >Angs.Blokir</asp:Label>
                                                <div style="width: 57px;" runat="server" >
                                                    <asp:DropDownList ID="ddlangsblokir" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  Enabled="false">
                                                        <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>&nbsp
                                                <asp:TextBox ID="txtRangsblokir" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp     
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label79" runat="server" class="col-form-label tabdetil">Angs.Dimuka</asp:Label>
                                                <div style="width: 57px;"  runat="server" >
                                                    <asp:DropDownList ID="ddlangsdimuka" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  Enabled="false"  >
                                                        <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>&nbsp
                                                <asp:TextBox ID="txtRangsdimuka" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp     
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label80" runat="server" class="col-form-label tabdetil">Pelunasan Take Over</asp:Label>
                                                <asp:TextBox ID="txtRpelunasanto" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp     
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label83" runat="server" class="col-form-label tabdetil">Terima Bersih</asp:Label>
                                                <asp:TextBox ID="txtterimabersih" CssClass="form-control" MaxLength="15" runat="server" Width="180px"  Enabled="false"  onkeypress="return isnumerickey(event);" AutoPostBack="true" ></asp:TextBox>
                                            </div>&nbsp     
                                            <div class="form-row aturbawah aturkiri">
                                                <asp:Label ID="Label86" runat="server" class="col-form-label tabdetil">Catatan</asp:Label>
                                                <div class="form-group col-8">
                                                    <asp:TextBox ID="txtcatatan1" CssClass="form-control" MaxLength="150" runat="server" Width="100%" onkeypress="return kepo(event);" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                    &nbsp&nbsp                                                                               
                                                    <asp:TextBox ID="txtcatatan2" CssClass="form-control" MaxLength="150" runat="server" Width="100%" onkeypress="return kepo(event);" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                    &nbsp&nbsp                                                                               
                                                    <asp:TextBox ID="txtcatatan3" CssClass="form-control" MaxLength="150" runat="server" Width="100%" onkeypress="return kepo(event);" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>           
                           
                                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" runat="server">

                                    <div class="form-row" >
                                     <div class="form-group">
                                                <label class="form-group col-md-6">Catatan Memo</label>
                                                <asp:TextBox CssClass="auto-style2" ID="txtnote" MaxLength="150"  runat="server" TextMode="MultiLine" Height="145px" Width="595px"></asp:TextBox>
                                    </div>
                                    </div>

                                   <div class="form-row" >

                                        <div class="form-group col-md-6" style="justify-content:flex-start;" runat="server" >
                                          
                                              <asp:Button ID="BtnApprove" align="right"  class="btn btn-success waves-light text-white" runat="server" Text="APPROVE" OnClick="btnApprove_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Approve?');" />
                                            <asp:Button ID="BtnTolak" align="right"  class="btn btn-success waves-light text-white" runat="server" Text="TOLAK" OnClick="BtnTolak_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Tolak?');" /> 
                                             <asp:Button ID="btnback" CausesValidation="False" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBacksave_Click" />
                                        </div>                                                                              
                                    </div>
                                     </div>

                                                       
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="divsimpan" runat="server" visible="false">
                                <div class="hidden-print mt-4">
                                    <div class="float-left mb-2">
                                       
                                    </div>
                                    <div class="float-right mb-2 ">
                                        <%--id="idreqvisible" runat="server" visible="false" CausesValidation="False"--%>
                                        <asp:Button ID="btnrequest" align="right" class="btn btn-success waves-light text-white"  runat="server" Text="Save" OnClick="btnRequest_Click" OnClientClick="return AlertRequest(this,'Apakah anda yakin ingin Melakukan Memo Internal ?');" />
                                    </div>
                                </div>
                            </div>
                            <div class="hidden-print mt-4" id="idsimpanvisible" runat="server" visible="false">
                                <div class="float-left mb-2">
                                    <asp:Button ID="btnBackIdentitas" align="right" class="btn btn-success waves-light text-white" runat="server" Text="Cancel" OnClick="btnBack_Click" />
                                </div>
                                <div class="float-right mb-2 ">
                                    <%--<asp:Button ID="btnsimpan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="SIMPAN" OnClick="btnSimpan1_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />--%>
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-outline-primary" runat="server" OnClick="btnBacksave_Click" Visible="FALSE" />
                                </div>
                            </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    
                    <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="txtnopen"/>
                <asp:PostBackTrigger ControlID="GRData" />
                     <%--<asp:PostBackTrigger ControlID="btnAdd" />--%>
                 <asp:PostBackTrigger ControlID="ddlkodeproduk"/>
                <asp:PostBackTrigger ControlID="ddlstatustakeover"/>
                <asp:PostBackTrigger ControlID="ddlpensiun"/>
                <asp:PostBackTrigger ControlID="ddlstatussk"/>
                <asp:PostBackTrigger ControlID="ddlstatusnikah"/>
                <asp:PostBackTrigger ControlID="ddlKodeJiwa"/>
                <asp:PostBackTrigger ControlID="ddljenisdeviasi"/>
                <asp:PostBackTrigger ControlID="ddljangkawaktu"/>
             
                
                
              
 
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
<script type="text/javascript">
    $(document).ready(function () {
        $('#datatable').dataTable();
    });

    
</script>

</html>

                                           