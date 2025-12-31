<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateMiNonCapital.aspx.cs" Inherits="MI_Master_CreateMiNonCapital" %>
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

    <script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

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

     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

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

        function AlertSuccess(h, t, p, i, fr) {
            console.log(fr)
            setTimeout(function () {


                Swal.fire({
                    title: h,
                    text: t,
                    icon: "success"
                }).then((result) => {
                    if (fr === 'true') {
                        window.location.href = "CreateMiNonCapital.aspx"
                    }
                });;
            }, 100);

        }


         <%--function CheckFileExistence() {

            var filePath = document.getElementById('<%= this.fileuploadktp.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto KTP belum di pilih!");


                return false;
            }
        }--%>

        function sweetAlertCancel(btnBack, errMsg) {
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
            location.href = 'CreateMINew.aspx ';
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
        function Alertfailed(h, t, p, i) {
            setTimeout(function () {
                Swal.fire({
                    title: h,
                    text: t,
                    icon: "error"
                });
            }, 100);

        }

        function sweetAlertsave(btnrequest, errMsg) {
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
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });


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
            $('#txtmtpensiun').datepicker({
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
                                <h4 class="page-title">Simulasi Kredit</h4>
                            </div>
                        </div>
                    </div>
                       

                    <asp:Panel ID="pnlHeader" runat="server">
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box table-responsive">
                                    <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Simulasi" OnClick="btnAdd_Click" />
                                    
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                


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
                                    
                                    <h4 class="m-t-0 header-title" id="idjudulsemua" runat="server" visible="true">Daftar Simulasi</h4>
                                    <div class="form-row" id="divtotalrequest" runat="server" visible="true">

                                         
                                        <div class="form-group" style="width: 100%;">
                                                                                        <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NoSimulasi" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrequest" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoSimulasi")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tgl Simulasi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglsimulasix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglSimulasi","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nopen">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnopen" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NomorPensiun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaPensiun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plafond">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplafond" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Plafon", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Plafond Max">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplafondmax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlafondMax", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                           
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                              <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:Button>                                                                                                                      
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="DEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="BtnNasabah" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Report Data Nasabah" CommandName="RptDataNasabah" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="BtnDebitur" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Report Data Debitur" CommandName="RptDataDebitur" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="BtnSimulasi" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Report Simulasi" CommandName="RptDataSimulasiNonCapital" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>                 
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
                                
                  <div class="row">



                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA PRODUCT & SUMBERDANA</div>
                                    <p class="text-muted font-14 m-b-30"></p>         
                                  <div class ="form-row">
                                 <div class="form-group col-md-6">
                                       <asp:label ID="Label14" runat="server" class="col-form-label">No Simulasi</asp:label>
                                 
                                    <asp:TextBox ID="txtnosimulasi" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>                                                       
                                </div>
                                <div class="form-group col-md-6">
                                     <asp:label ID="Label15" runat="server" class="col-form-label">Tanggal Simulasi</asp:label>
                      
                                    <asp:TextBox ID="txttglsimulasi" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>                                                       
                                </div>

                                       </div>
                                                                
                                </div>                                                    
                            </div>
                        

                           

                                          
                            

                    </div>
            </asp:Panel>


                    <asp:Panel ID="Pnlsimulasi" visible="false" runat="server">
                      
                                    <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA SIMULASI</div>
                                                       <p class="m-b-0"></p>
                                 
                                                                 <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">INPUT</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                     

                                                            <div class ="form-row">
                                                                <div class="form-group col-md-6">
                                                                    <asp:label ID="Label165" runat="server" class="col-form-label">Kantor Bayar</asp:label>
                                                                   
                                                                    <asp:DropDownList ID="ddlKantorBayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlkantor_SelectedIndexChanged" ></asp:DropDownList>
                                                                    
                                                                     <asp:TextBox ID="txtkodesimulasi" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="form-group col-md-6">
                                                                    <asp:label ID="Label1" runat="server" class="col-form-label">Sumber Dana</asp:label>
                                                                  
                                                                       <asp:DropDownList ID="ddlsumberdana" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>
                                                            </div>
                      

                             


                                                             <div class ="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label2" runat="server" class="col-form-label">ProgramPinjaman</asp:label>
                                                                       
                                                                            <asp:DropDownList ID="ddlprogrampinjaman" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack ="true" OnSelectedIndexChanged="ddlprogrampinjaman_SelectedIndexChanged" ></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                          <asp:label ID="Label3" runat="server" class="col-form-label">Produk</asp:label>
                                                                         <asp:DropDownList ID="ddlproduk" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlkantorbayar_SelectedIndexChanged" ></asp:DropDownList>
                                                                       
                                                                    </div>
                                                                </div>

                                                          

                                                            <div class ="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label4" runat="server" class="col-form-label">Kategori</asp:label>
                                                                     <asp:DropDownList ID="ddlkategori" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" OnSelectedIndexChanged="ddlkategori_SelectedIndexChanged" AutoPostBack ="true" ></asp:DropDownList>
                                                                            <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label5" runat="server" class="col-form-label">Instansi</asp:label>
                                                                        <asp:DropDownList ID="ddlinstansi" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" Enabled="false" ></asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                           
                                                   <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label9" runat="server" class="col-form-label">Nomor Pensiun</asp:label>
                                                                      <asp:TextBox ID="txtnopen" CssClass="form-control" MaxLength="15" onkeypress="return kepo2(event);" runat="server" AutoPostBack="true" OnTextChanged="txtnopen_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-8">
                                                                         <asp:label ID="Label10" runat="server" class="col-form-label">Nama Pensiunan</asp:label>
                                                                       <asp:TextBox ID="txtnamapensiun" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                        


                                                   

                                                         


                                                 <div class ="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label6" runat="server" class="col-form-label">Kode Jiwa</asp:label>
                                                                     <asp:DropDownList ID="ddlkodejiwa" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                            <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label7" runat="server" class="col-form-label">Status Pernikahan</asp:label>
                                                                       <asp:DropDownList ID="ddlstatusnikah"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                                            <asp:ListItem Value="">Pilih Status</asp:ListItem>
                                                                            <asp:ListItem Value="0">Tidak Kawin</asp:ListItem>
                                                                            <asp:ListItem Value="1">Kawin</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label8" runat="server" class="col-form-label">Pensiunan</asp:label>
                                                                            <asp:DropDownList ID="ddlpensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                                            <asp:ListItem Value="">Pilih Pensiun</asp:ListItem>
                                                                            <asp:ListItem Value="1">Sendiri</asp:ListItem>
                                                                            <asp:ListItem Value="2">Janda</asp:ListItem>
                                                                            <asp:ListItem Value="3">Duda</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                   

                                                            <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label11" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
                                                                    <asp:TextBox ID="txttgllahir" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged="txtusia_OnChange"></asp:TextBox>
                                                                            <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                        
                                                                         <asp:label ID="Label12" runat="server" class="col-form-label">Tanggal Lahir Anak 1</asp:label>
                                                                       <asp:TextBox ID="txttgllahiranak1" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged="txtusia_anak1_OnChange" ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label13" runat="server" class="col-form-label">Tanggal Lahir Anak 2</asp:label>
                                                                          <asp:TextBox ID="txttgllahiranak2" CssClass="form-control" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged="txtusia_anak2_OnChange" ></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class ="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label18" runat="server" class="col-form-label">Jangka Waktu Bulan</asp:label>
                                                                       
                                                                          <asp:DropDownList ID="ddljangkawaktu" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddljangkawaktu_SelectedIndexChanged" ></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                          <asp:label ID="Label19" runat="server" Enabled="false" class="col-form-label">Jangka Waktu Tahun</asp:label>
                                                                         <asp:TextBox ID="txtjangkatahun" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                </div>
                                                                       
                                                                   
                                                                </div>


                                                                <div class ="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label16" runat="server" class="col-form-label">TMT Pensiun</asp:label>
                                                                       
                                                                           <asp:TextBox CssClass="form-control" ID="txtmtpensiun"   TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                          <asp:label ID="Label17" runat="server" class="col-form-label">Gaji Sesuai Display</asp:label>
                                                                         <asp:TextBox ID="txtgajidisplay" CssClass="form-control" AutoPostBack ="true"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" OnTextChanged="txtgajidisplay_TextChanged" ></asp:TextBox> 
                                                                </div>
                                                                       
                                                               
                                                                </div>
                                                                <div class ="form-row" style="display:none">
                                                                    <div class="form-group col-md-6">
                                                                          <asp:label ID="lblgajipokok" runat="server" class="col-form-label">Gaji Pokok</asp:label>
                                                                         <asp:TextBox ID="txtgajipokok" CssClass="form-control" AutoPostBack ="true"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                          <asp:label ID="Lbltunjangananak" runat="server" class="col-form-label">Tunjangan Anak</asp:label>
                                                                         <asp:TextBox ID="txttunjangananak" CssClass="form-control" AutoPostBack ="true"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"></asp:TextBox> 
                                                                    </div>
                                                                       
                                                               
                                                                </div>
                                                   

                                                             <div class ="form-row">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label20" runat="server" class="col-form-label">Plafon</asp:label>
                                                                       
                                                                         <asp:TextBox ID="txtplafon" CssClass="form-control"  runat="server" text="0,00" AutoPostBack ="true"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" OnTextChanged="txtplafon_TextChanged" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-6">
                                                                          <asp:label ID="Label21" runat="server" Enabled="false" class="col-form-label">Pelunasan</asp:label>
                                                                          <asp:TextBox ID="txtpelunasan" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" AutoPostBack = "true" OnTextChanged="txtpelunasan_TextChanged" ></asp:TextBox> 
                                                                </div>
                                                                       
                                                                  
                                                                </div>
                                                            
                                                  
                                                    
                                                      



                                                            <div class ="form-row">
                                                                  <div class="form-group col-md-4">
                                                                         <asp:label ID="Label24" runat="server" class="col-form-label">Angsuran Dimuka</asp:label>
                                                                           <asp:DropDownList ID="ddlangsurandimuka" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack ="true" OnSelectedIndexChanged="ddlangsurandimuka_SelectedIndexChanged"> 
                                                                           </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label22" runat="server" class="col-form-label">Materai</asp:label>
                                                                          <asp:DropDownList ID="ddlmaterai" CssClass="form-control" runat="server" data-style="btn-custom" OnSelectedIndexChanged="ddlmaterai_SelectedIndexChanged"  AutoPostBack ="true"> </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label23" runat="server" class="col-form-label">Provisi</asp:label>
                                                                       <asp:DropDownList ID="ddlprovisi" CssClass="form-control" runat="server" data-style="btn-custom" OnSelectedIndexChanged="ddlprovisi_SelectedIndexChanged" AutoPostBack ="true"> </asp:DropDownList>
                                                                    </div>
                                                                  
                                                                </div>
                                                
                                                           

                                                        <div class ="form-row">
                                                                <div class="col-md-6">
                                                                    <p class="form-control">Mutasi Kantor Bayar</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-6">
                                                                     
                                                                            <asp:DropDownList ID="ddlmutasikantorbayar" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                                            <asp:ListItem Value="">Pilih Mutasi Kantor Bayar</asp:ListItem>
                                                                            <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                                                            <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                                                                          
                                                                            </asp:DropDownList>
                                                                      
                                                                </div>
                                                            </div>

                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">OUTPUT</div>
                                                <p class="text-muted font-14 m-b-30"></p>


                                                <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label25" BackColor="#cccccc" runat="server" class="form-control">Nama</asp:label>
                                                                       <asp:TextBox ID="txtnama" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label26" BackColor="#cccccc" runat="server" class="form-control">Status Pinjaman</asp:label>
                                                                       <asp:TextBox ID="txtstatuspinjaman" Enabled="false" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                         <div class ="form-row">
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label30" BackColor="#cccccc" runat="server" class="form-control">Usia Pinjaman</asp:label>
                                                                    <asp:TextBox ID="txtusiapinjaman" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                            <%--<asp:TextBox ID="TextBox6" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Tunjangan Makan" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label31" BackColor="#cccccc" runat="server" class="form-control">Usia Asuransi</asp:label>
                                                                       <asp:TextBox ID="txtusiaasuransi" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>  
                                                               <div class="form-group col-md-3">
                                                                         <asp:label ID="Label28" BackColor="#cccccc" runat="server" class="form-control">Usia Anak 1</asp:label>
                                                                       <asp:TextBox ID="txtusiaanak1" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                         <asp:label ID="Label29" BackColor="#cccccc" runat="server" class="form-control">Usia Anak 2</asp:label>
                                                                          <asp:TextBox ID="txtusiaanak2" Enabled="false" CssClass="form-control" TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label27" BackColor="#cccccc" runat="server" class="form-control">Plafond Max</asp:label>
                                                                       <asp:TextBox ID="txtplafonmax" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-8">
                                                                         <asp:label ID="Label32" BackColor="#cccccc" runat="server" class="form-control">Jangka Waktu Max (Bulan)</asp:label>
                                                                        <asp:TextBox ID="txtjangkawaktumax" Enabled="false" CssClass="form-control"  runat="server" text="0,00"   onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                </div>
                                            
                                                            <div class ="form-row">
                                                                    <div class="form-group col-md-4">
                                                                         <asp:label ID="Label33"  BackColor="#cccccc" runat="server" class="form-control">Rate Asuransi %</asp:label>
                                                                       <asp:TextBox ID="txtrateasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="form-group col-md-8">
                                                                         <asp:label ID="Label34" BackColor="#cccccc" runat="server" class="form-control">Rate Bunga Pinjaman %</asp:label>
                                                                        <asp:TextBox ID="txtratebungapinjaman" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                    </div>
                                                                </div>

                                                  

                                                    <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angsuran Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangsuran" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtpsnangsuran" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Asuransi Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtpersenasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Total Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txttotal" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txttotalpersen" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                    <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Sisa Gaji Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtsisagaji" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtsisagajipersen" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                   <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Provisi Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtprovisi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtpersenprovisi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">%</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Keanggotaan Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtkeanggotaan" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>
                                                 <div class ="form-row" style="display:none">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Materai Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtmaterai" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtmateraipcs" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">Pcs</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angsuran Blokir Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangsuranblokir" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangsuranblokirkali" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">kali</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angs. Dimuka angs Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangsurandimuka" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangsurandimukakali" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">kali</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>

                                                        <div class ="form-row" style="display:none;">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Angs. Dimuka asuransi Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtangdimukaasuransi" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                                <div class="col-md-2">
                                                                      <asp:TextBox ID="txtangdimukaasuransikali" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               <div class="col-md-2">
                                                                    <p class="form-control">kali</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                            </div>


                                                  <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Biaya Admin Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtbiayaadmin" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                  <div class ="form-row" style="display:none">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Pelunasan/Take Over Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtpelunasanRp" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                 <div class ="form-row">
                                                                <div class="col-md-4">
                                                                    <p class="form-control">Terima Bersih Rp</p>
                                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" required="required" runat="server" MaxLength="20" Enabled ="false" Text="Gaji Pokok/Bln" OnTextChanged="TextBox6_TextChanged" ></asp:TextBox>--%> 
                                                                </div>
                                                                <div class="col-md-4">
                                                                      <asp:TextBox ID="txtterimabersihrp" Enabled="false" CssClass="form-control"  runat="server" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                </div>
                                                               
                                                            </div>

                                                    <%--    <div class ="form-row">
                                                                <div class="col-md-6">
                                                                   <asp:Label ID="lblnopenbaru" runat="server" class="col-form-label " Font-Bold="true" ForeColor="Red" Font-Italic="true" Visible="true"></asp:Label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <asp:Label ID="lblnopenlama" runat="server" class="col-form-label " Font-Bold="true" ForeColor="Red" Font-Italic="true" Visible="true"></asp:Label>
                                                                </div>
                                                            </div>
                                                     --%>

                                                            


                                                         
                                         
                                                     
                                                         



                                          

                                                       

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                    
                                     
                                                   </div>  

                                            </div>
                                         </div>        
                             
                    <div class="hidden-print mt-4" id="idsimpanvisible" runat="server" visible="false">
                                <div class="float-left mb-2">
                                        <asp:Button ID="btnrequest" align="right" class="btn btn-success waves-light text-white"  runat="server" Text="Save"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnrequest_Click" />
                                         <asp:Button ID="btndelete" align="right" class="btn btn-success waves-light text-white"  runat="server" Text="Delete"  OnClientClick="return sweetAlertdelete(this,'Apakah anda yakin ingin hapus Data Simulasi ?');" OnClick="btndelete_Click" />                                
                                </div>
                                <div class="float-right mb-2 ">
                                    <%--<asp:Button ID="btnsimpan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="SIMPAN" OnClick="btnSimpan1_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />--%>
                                    <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="btn btn-outline-primary" runat="server" OnClick="btnCancel_Click" />

                                
                                    
                                </div>
                            </div>
                    </asp:Panel>
        
               
                    

                    
                    <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="txtnopen"/>
                <asp:PostBackTrigger ControlID="GRData" />
                     <asp:PostBackTrigger ControlID="btnAdd" />
                <%-- <asp:PostBackTrigger ControlID="ddlkodeproduk"/>
                <asp:PostBackTrigger ControlID="ddlstatustakeover"/>
                <asp:PostBackTrigger ControlID="ddlpensiun"/>
                <asp:PostBackTrigger ControlID="ddlstatussk"/>
                <asp:PostBackTrigger ControlID="ddlstatusnikah"/>
                <asp:PostBackTrigger ControlID="ddlKodeJiwa"/>
                <asp:PostBackTrigger ControlID="ddljenisdeviasi"/>
                <asp:PostBackTrigger ControlID="ddljangkawaktu"/>--%>
             
                
                
              
 
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

                                           
