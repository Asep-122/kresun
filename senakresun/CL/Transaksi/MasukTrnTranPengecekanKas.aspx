<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="MasukTrnTranPengecekanKas.aspx.cs" Inherits="CL_Process_MasukTrnPembulatanKas" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Pembayaran Hutang</title>
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
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
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
                    btnSimpan.disabled = true;
                    return true;
                  
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

            {
                $('#<%=txtKeterangan.ClientID%>').prop('maxlength', 50);
                
            }
            
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
                var tableX = $('#GRRP').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var tableX = $('#GRTP').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                        var tableX = $('#Grduangmuka').dataTable({
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
                                    <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">Transaksi Pengecekan Kas Head Teller</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Transaksi Pengecekan Kas Head Teller</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="false"  runat="server">
                       <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-form-label">Search By</label>
                                                               
                                                                    <div class="form-group col-md-6">
                                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                                            <asp:ListItem Value=" and nobukti ">No. Bukti</asp:ListItem>                                                                              
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                                                                        <asp:Button ID="lnkCari" class="btn btn-success waves-light text-white" runat="server" Text="Cari" OnClick="lnkCari_Click" />
                                                                    </div>
                                                                    
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>

            </asp:Panel>
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
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Transaksi Pengecekan Fisik Kas Head Teller</h4>   
                            

                            <div class="form-row" runat="server" visible="false">
                                    <div class="form-group col-md-4">                                
                                    <asp:label ID="Label1" runat="server" class="col-form-label">Tgl Transaksi</asp:label>
                                    <asp:TextBox ID="txttgltrnx"  CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdatex_TextChanged"></asp:TextBox>      
                                                       
                                   
                                    </div>
                                    <div class="form-group col-md-4">
                                    <asp:label ID="Label44" runat="server" class="col-form-label">User Teller</asp:label>
                                    <asp:DropDownList ID="ddltellerhdr" CssClass="selectpicker show-tick" data-style="btn-success" runat="server" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddltellerhdr_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>
                                    </div>

                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData"  ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No Bukti">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL" />
                                        </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>


       
                                        <asp:TemplateField HeaderText="Tanggal Transaksi">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                   

                                    <asp:TemplateField HeaderText="Jumlah Kertas">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljumlahkertas" CssClass="" runat="server" Text='<%#Eval("totalkertas","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jumlah Logam">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljumlahlogam" CssClass="" runat="server" Text='<%#Eval("totallogam","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Kertas + Logam ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblddlpembayaranx" CssClass="" runat="server" Text='<%#Eval("JumlahTrn","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>    
                                   </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Adjust">
                                        <ItemTemplate>
                                            <asp:Label ID="lblddlpembayaran" CssClass="" runat="server" Text='<%#Eval("NilaiAdjust","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>    
                                        <FooterTemplate>
                                        <asp:Label ID="lbltotal" runat="server"  />
                                        </FooterTemplate>
                                  </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Ket Adjust">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobuktix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ketadjust")%>'></asp:Label>
                                         </ItemTemplate>
                                       
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>

                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="User Entry ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluserentry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodeuser")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Entry date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltglentrydate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglEntry","{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                    

                                        
                                       <asp:TemplateField HeaderText="nogiro" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnogiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nogiro")%>'></asp:Label>
                                                <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                                <asp:Label ID="lblHapusFlag" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "HapusFlag")%>'></asp:Label>
                                                <asp:Label ID="lblTutupBukuFlag" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TutupBukuFlag")%>'></asp:Label>
                                                <asp:Label ID="lblbankgiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bankgiro")%>'></asp:Label>
                                                <asp:Label ID="lblkertas1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas1")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas1" CssClass="" runat="server" Text='<%#Eval("totalkertas1","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas2")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas2" CssClass="" runat="server" Text='<%#Eval("totalkertas2","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas3")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas3" CssClass="" runat="server" Text='<%#Eval("totalkertas3","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas4")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas4" CssClass="" runat="server" Text='<%#Eval("totalkertas4","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas5")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas5" CssClass="" runat="server" Text='<%#Eval("totalkertas5","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas6")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas6" CssClass="" runat="server" Text='<%#Eval("totalkertas6","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas7")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas7" CssClass="" runat="server" Text='<%#Eval("totalkertas7","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas8")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas8" CssClass="" runat="server" Text='<%#Eval("totalkertas8","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblkertas9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kertas9")%>'></asp:Label>
                                                <asp:Label ID="lbltotalkertas9" CssClass="" runat="server" Text='<%#Eval("totalkertas9","{0:N2}")%>'></asp:Label> 
                                            
                                                <asp:Label ID="lbllogam1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam1")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam1" CssClass="" runat="server" Text='<%#Eval("totallogam1","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam2")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam2" CssClass="" runat="server" Text='<%#Eval("totallogam2","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam3")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam3" CssClass="" runat="server" Text='<%#Eval("totallogam3","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam4")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam4" CssClass="" runat="server" Text='<%#Eval("totallogam4","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam5")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam5" CssClass="" runat="server" Text='<%#Eval("totallogam5","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam6")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam6" CssClass="" runat="server" Text='<%#Eval("totallogam6","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam7")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam7" CssClass="" runat="server" Text='<%#Eval("totallogam7","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam8")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam8" CssClass="" runat="server" Text='<%#Eval("totallogam8","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lbllogam9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "logam9")%>'></asp:Label>
                                                <asp:Label ID="lbltotallogam9" CssClass="" runat="server" Text='<%#Eval("totallogam9","{0:N2}")%>'></asp:Label> 
                                                
                                                <asp:Label ID="lblkodeAdjust" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodeAdjust")%>'></asp:Label>
                                                <asp:Label ID="lblNilaiAdjust" CssClass="" runat="server" Text='<%#Eval("NilaiAdjust","{0:N2}")%>'></asp:Label> 
                                                <asp:Label ID="lblSaldoAkhir" CssClass="" runat="server" Text='<%#Eval("SaldoAkhir","{0:N2}")%>'></asp:Label> 

                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                    
                                    
                                    
                                   
                                 
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" title="Delete" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="BtnSlip" Visible="false" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="BtnPDF" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
																			<asp:LinkButton ID="BtnReport" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="REPORT" CommandName="REPORT" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                      
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
                       <div class="row">


                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Entry Data</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                      
                                                

                                    <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                     <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>        
                                    <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoSebelumnya" runat="server" />
                                                <asp:HiddenField ID="HDNlasttransaksiap" runat="server" />
                                                <asp:HiddenField ID="HDNlasttransaksicl" runat="server" />
                                    </div>
                       
                             
                                      <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Transaksi</label>
                                     <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>      
                                    </div>
                                  <%--   <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Jth tempo</label>
                                     <asp:TextBox ID="txttgljthtmp" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>        
                                    </div>--%>

                                    <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Keterangan</label>
                                    <asp:TextBox CssClass="form-control" ID="txtKeterangan" MaxLength="50"  required="required"  runat="server" TextMode="MultiLine"></asp:TextBox>
                                        
                                    </div>

                                
                             

                     
                                            
                                                                      
                                </div>                                                    
                            </div>
                           </div>
                                   
                                              

                    
                   
                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                   <div class="form-row" runat="server" visible="false">
              
                                         <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Bank Giro</label>
                                       <asp:TextBox ID="txtBankGiro" CssClass="form-control"  runat="server" MaxLength="30"></asp:TextBox>    
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">No Giro</label>
                                         <asp:TextBox ID="txtnoGiro" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                             
                                    </div>
                      
                                </div>
                                     <%--<div class="form-row">
              
                                         <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Jumlah Rekon Transaksi</label>
                                        <asp:TextBox ID="txttransAP" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>      
                                    </div>


                                      
                                    
                                    
                                      </div>--%>

                                         <div class="form-row">
                             
                                                  <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Saldo Akhir</label>
                                        <asp:TextBox ID="txtsaldoakhir" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>      
                                    </div>
                                            
                                     </div>
                                                   
                                    <div class="form-row">
                                     <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Adjust</label>
                                         <asp:DropDownList ID="ddladjust" runat="server" CssClass="selectpicker show-tick" data-style="btn-success" AutoPostBack="true"  OnSelectedIndexChanged ="ddladjust_SelectedIndexChanged">
                                                                            <asp:ListItem Value="A">Tidak Ada Adjust</asp:ListItem>   
                                                                            <asp:ListItem Value="B">+</asp:ListItem> 
                                                                            <asp:ListItem Value="C">-</asp:ListItem> 
                                                                        </asp:DropDownList>
                                    </div>

                                                  <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Nilai Adjust</label>
                                        <%--<asp:TextBox ID="txtnilaiadjust" MaxLength="3" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);"onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"></asp:TextBox>--%>      
                                        <asp:TextBox ID="txtnilaiadjust" MaxLength="5" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True" AutoPostBack="true" OnTextChanged="txtnilaiadjust_TextChanged"></asp:TextBox> 
                                    </div>
                                     <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Saldo Akhir sth Adjust</label>
                                        <asp:TextBox ID="txtsaldoakhiradjust" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>      
                                    </div>
                                            
                                     </div>


                                </div>                                                    
                            </div>
                           </div>


                           
                        </div>      
                                   
                </div>
              </div>
              </asp:Panel>

            

                 <asp:Panel ID="Pnldetail" Visible="false" runat="server">
              <div class="row clearfix">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <div class="row">

                            <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Uang Kertas</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                       
                               
                                   <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar1"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar1_TextChanged"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 100.000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal1"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar2"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar2_TextChanged" TabIndex="1"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 50.000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal2" Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                           <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar3"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar3_TextChanged" TabIndex="2"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 20.000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal3"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                   <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar4"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar4_TextChanged" TabIndex="3"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 10.000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal4"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar5"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar5_TextChanged" TabIndex="4"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 5.000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal5" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar6"  Text="0" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar6_TextChanged" TabIndex="5"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 2.000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal6"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar7"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar7_TextChanged" TabIndex="6"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 1.000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal7"  Text="0" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar8"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar8_TextChanged" TabIndex="7"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 500</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal8" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembar9"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlembar9_TextChanged" TabIndex="8"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Lembar Rp. 100</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal9"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>

                                     <div class="form-row">                                      
                                       <div class="form-group col-md-8">
                                                <label for="inputState" class="col-form-label">Jumlah Uang Kertas</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlembartotal"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>


                                                              
                                </div>                                                    
                            </div>
                           </div>

                                               
                         <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Uang Logam</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                       
                               
                                   <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam1"  Text="0"  type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam1_TextChanged" TabIndex="10"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 1000</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal1" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam2" Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam2_TextChanged" TabIndex="10"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 500</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal2" Text="0" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                           <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam3"  Text="0" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam3_TextChanged" TabIndex="11"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 200</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal3"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                   <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam4"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam4_TextChanged" TabIndex="12"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 100</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal4"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam5"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam5_TextChanged" TabIndex="13"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 50</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal5"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam6"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam6_TextChanged" TabIndex="14"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 25</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal6"  Text="0" onblur="checkTextField1(this);"  type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam7"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam7_TextChanged" TabIndex="15"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 10</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal7"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam8"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam8_TextChanged" TabIndex="16"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 5</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal8"  Text="0" onblur="checkTextField1(this);"  type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>
                                   
                                     <div class="form-row">
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogam9"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="True" MaxLength="50" AutoPostBack="true" OnTextChanged="txtlogam9_TextChanged" TabIndex="16"></asp:TextBox>     
                                        </div>
                                       <div class="form-group col-md-4">
                                                <label for="inputState" class="col-form-label">Keping Rp. 1</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal9"  Text="0" onblur="checkTextField1(this);"  type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>

                                     <div class="form-row">                                      
                                       <div class="form-group col-md-8">
                                                <label for="inputState" class="col-form-label">Jumlah Uang Logam</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtlogamtotal"  Text="0"  onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>     
                                        </div>
                                    </div>


                                                              
                                </div>                                                    
                            </div>
                           </div>

                           
                        </div>      
                                   
                </div>
              </div>
              </asp:Panel>

                 <asp:Panel ID="Pnlfooter4" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">

                                  <div class="form-row">
              
                                         <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Jumlah Rekon Transaksi</label>
                                        <asp:TextBox ID="txttransAP" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>      
                                        </div>

                                        <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Jumlah Saldo Akhir System Setelah Adjust</label>
                                        <asp:TextBox ID="txtsaldoakhirsystemadjust" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="True"></asp:TextBox>      
                                        </div>
                                      
                                    
                                    
                                      </div>


                                <div class="card-box table-responsive">
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Delete" OnClick="btndelete_click" Enabled="true"/>
                                    <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click"  Width="65px" />
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
                           <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="GRData" /> 
                 <%--<asp:PostBackTrigger ControlID="ddlteller" />--%> 
                 
                <asp:PostBackTrigger ControlID="lnkCari" />
                 <%--<asp:PostBackTrigger ControlID="GRTP" />--%>
             
                 
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



