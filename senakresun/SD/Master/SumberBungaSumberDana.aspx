<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SumberBungaSumberDana.aspx.cs" Inherits="SD_Master_BungaSumberDana" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Bunga Sumber Dana</title>
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
                        window.location.href = "SumberBungaSumberDana.aspx"
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
            }, 200);

        }

        function sweetAlertsave(btnrequest, errMsg) {
            if (btnSave.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSave.dataset.confirmed = false;
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
                        btnSave.dataset.confirmed = true;
                        btnSave.click();

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

        thead th {
            border: 1px solid #dee2e6 !important;
            background-color: #f8f9fa; /* atau warna lain sesuai keinginan */
            z-index: 2; /* agar tidak tertutup elemen lain */
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
                                        <li class="breadcrumb-item"><a href="#">Sumber Dana</a></li>
                                        <li class="breadcrumb-item"><a href="#">Master</a></li>
                                        <li class="breadcrumb-item"><a href="SumberBungaSumberDana.aspx">Bunga Sumber Dana</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Bunga Sumber Dana</h4>
                            </div>
                        </div>
                    </div>                      

                



                    <!-- Panel Edit -->
                    <asp:Panel ID="pnladd"  runat="server">                                                                              
                        <div class="row">
                            <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Bunga Sumber Dana</div>
                                <p class="text-muted font-14 m-b-30"></p>         
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label for="labelKantorBayar" class="col-form-label">Sumber Dana</label>
                                            <asp:DropDownList ID="ddlSumberDana" runat="server"  CssClass="form-control" data-style="btn-custom"></asp:DropDownList>                                                                
                                        </div>
                                        </div>
                                          <div class="form-row">
                                                    <div class="form-group col-md-6">
                                                        <label for="labelProgramPinjam" class="col-form-label">Program Pinjam</label>
                                                        <asp:DropDownList ID="ddlProgramPinjam" runat="server"  CssClass="form-control" data-style="btn-custom"></asp:DropDownList>                                                                
                                                    </div>
                                               </div>
                                           <div class="form-row">
                                                    <div class="form-group col-md-6">
                                                        <label for="labelStatusPensiun" class="col-form-label">Status Pensiun</label>
                                                        <asp:DropDownList ID="ddlStatusPensiun" runat="server"  CssClass="form-control" data-style="btn-custom">
                                                            <asp:ListItem Text="Pilih Pensiunan" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Sendiri" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Janda/Duda" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>                                                                
                                                    </div>
                                               </div>
                                            <div class="form-row">
                                                    <div class="form-group col-md-6">
                                                        <label for="labelKategori" class="col-form-label">Kategori</label>
                                                        <asp:DropDownList ID="ddlKategori" runat="server"  CssClass="form-control" data-style="btn-custom"></asp:DropDownList>                                                                
                                                    </div>
                                                </div>
                                                <div class="form-row">
                                                        <div class="form-group">
                                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                        <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Bunga" OnClick="btnAdd_Click"/>
                                                        </div>
                                                </div>                                                    
                                            </div>
                        </div>
                    </asp:Panel>      
                                        <!-- Panel Front Gridview -->
                    <asp:Panel ID="PnlFront" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box ribbon-box">
                                    <div class="col-md-6" id="idcari" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-form-label">Search By</label>
                                            <div class="form-group col-md-6">
                                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                    <asp:ListItem Value=" and NoSimulasi ">No Bank</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                                                <br>
                                                <asp:Button ID="lnkCari" class="btn btn-success waves-light text-white" runat="server" Text="Cari" OnClick="lnkCari_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <h4 class="m-t-0 header-title" id="idjudulsemua" runat="server" visible="true">List Bunga Sumber Dana</h4>
                                    <div class="form-row" id="divtotalrequest" runat="server" visible="true">

                                         
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Bank" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNamaBank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaBank")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                     </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Program Pinjam">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProgramPinjam" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProgramPinjam")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status Pensiun">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatusPensiun" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StatusPensiun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kategori">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKategori" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kategori")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                
                                                    <asp:TemplateField HeaderText="Jangka" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jangka")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                     </asp:TemplateField>                                                 
                                                    <asp:TemplateField HeaderText="JangkaTahun" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJangkaTahun" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JangkaTahun")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                     </asp:TemplateField>                                                                                         
                                                    <asp:TemplateField HeaderText="bunga flat 1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbungaflat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bungaflat1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                           
                                                    <asp:TemplateField HeaderText="bunga efektif 1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbungaefektif1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bungaefektif1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                                                                    
                                                    <asp:TemplateField HeaderText="bunga flat 2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbungaflat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bungaflat2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                           
                                                    <asp:TemplateField HeaderText="bunga efektif 2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbungaefektif2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bungaefektif2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>                                                                                              
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                 
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:HiddenField ID="VIEWSTATE" runat="server"/>
                    <asp:Panel ID="pnlGenerate" runat="server" Visible="false">
                   <div class="row">
                    <div class="col-lg-12">
                    <div class="card-box ribbon-box">
                        <div class="ribbon ribbon-custom">Generate Bunga Sumber Dana</div>
                        <p class="text-muted font-14 m-b-30"></p>  
                        <table>
                            <tr>
                                <td>Jangka Bulan</td>
                                <td><asp:TextBox ID="txtStart" runat="server" CssClass="form-control" data-style="btn-custom" text="0" TextMode="Number"></asp:TextBox> <asp:HiddenField ID="txtLast" runat="server"/></td>
                                <td>S/d</td>
                                <td><asp:TextBox ID="txtEnd" runat="server" CssClass="form-control" data-style="btn-custom" text="0" TextMode="Number"></asp:TextBox></td>
                                <td><asp:Button ID="btnGenerate" class="btn btn-success waves-light text-white" runat="server" Text="Generate" OnClick="btnGenerate_Click"/></td>
                            </tr>
                        </table>
                          </div>
                        </div>
                       </div>
                    </asp:Panel>
                    <panel ID="pnlGenerateTable" runat="server" Visible="false">
                         <div style="max-height: 450px; overflow-y: auto;">
                            <table class="table table-hover table-bordered" style="width: max-content;">
                                <thead style="position: sticky; top: 0; z-index: 1;">
                                    <tr class="table-secondary">
                                        <th rowspan="2" style="text-align:center;vertical-align:middle;">Jangka Bulan</th>
                                        <th rowspan="2" style="text-align:center;vertical-align:middle;">Kode Bank</th>
                                        <th rowspan="2" style="text-align:center;vertical-align:middle;">Program</th>
                                        <th rowspan="2" style="text-align:center;vertical-align:middle;">Status Pensiun</th>
                                        <th rowspan="2" style="text-align:center;vertical-align:middle;">Kategori</th>
                                        <th rowspan="2" style="text-align:center;vertical-align:middle;">Jangka Tahun</th>
                                        <th colspan="2" style="text-align:center;vertical-align:middle;">Sena 1</th>
                                        <th colspan="2" style="text-align:center;vertical-align:middle;">Sena 2</th>
                                    </tr>
                                    <tr>
                                        <th width="150px;">Bunga Flat</th>
                                        <th width="150px;">Bunga Efektif</th>
                                        <th width="150px;">Bunga Flat</th>
                                        <th width="150px;">Bunga Efektif</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptInputs" runat="server">
                                        <ItemTemplate>
                                            <tr style="text-align:center;">
                                                <td><%# Eval("JangkaBulan") %> <asp:HiddenField ID="JangkaBulan" runat="server" Value='<%# Eval("JangkaBulan") %>' /></td>
                                                <td><%# Eval("NamaBank") %> <asp:HiddenField ID="KodeBank" runat="server" Value='<%# Eval("KodeBank") %>' /></td>
                                                <td><%# Eval("ProgramPinjam") %> <asp:HiddenField ID="KodeProgramPinjam" runat="server" Value='<%# Eval("KodeProgramPinjam") %>' /></td>
                                                <td><%# Eval("StatusPensiun") %> <asp:HiddenField ID="KodeStatusPensiun" runat="server" Value='<%# Eval("KodeStatusPensiun") %>' /></td>
                                                <td><%# Eval("Kategori") %> <asp:HiddenField ID="KodeKategori" runat="server" Value='<%# Eval("KodeKategori") %>' /></td>
                                                <td><%# Eval("JangkaTahun") %> <asp:HiddenField ID="JangkaTahun" runat="server" Value='<%# Eval("JangkaTahun") %>' /></td>
                                                <td><asp:TextBox ID="txtBungaFlat1" runat="server" CssClass="form-control"  Style="text-align:center;" Text='<%# Eval("bungaflat1") %>' Value='<%# Eval("bungaflat1") %>' /></td>
                                                <td><asp:TextBox ID="txtBungaEfektif1" runat="server" CssClass="form-control"  Style="text-align:center;" Text='<%# Eval("bungaefektif1") %>' Value='<%# Eval("bungaefektif1") %>' /></td>
                                                <td><asp:TextBox ID="txtBungaFlat2" runat="server" CssClass="form-control"  Style="text-align:center;" Text='<%# Eval("bungaflat2") %>' Value='<%# Eval("bungaflat2") %>' /></td>
                                                <td><asp:TextBox ID="txtBungaEfektif2" runat="server" CssClass="form-control"  Style="text-align:center;" Text='<%# Eval("bungaefektif2") %>' Value='<%# Eval("bungaefektif2") %>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </panel>
                     <asp:Panel ID="pnlButtonForm" runat="server" Visible="false">
                        <div class="form-group">
                            <div class="button-list">
                                <asp:Button ID="btnSave" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnrequest_Click"/>                                
                                <asp:Button ID="btnDelete" align="right" class="btn btn-danger waves-light text-white" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="return sweetAlertdelete(this,'Apakah anda yakin ingin hapus semua data ini ?');" />
                                <asp:Button ID="btnCancel" class="btn btn-custom waves-light text-white" runat="server" Text="Cancel" Visible="false"/>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="GRData" />
                     <asp:PostBackTrigger ControlID="btnAdd" />
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

                                           
