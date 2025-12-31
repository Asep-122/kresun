<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewNotifikasikasbon.aspx.cs" Inherits="AE_Process_ViewNotifikasikasbon" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>View Notifikasi Kasbon</title>
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
                        if (fr == '1') {
                           
                        }

                         if (fr == '2') {
                            //
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
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

<%--            {
                $('#<%=txtKeterangan.ClientID%>').prop('maxlength', 150);
                
            }--%>
            
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
            $('.selectpicker').selectpicker();


            $(function () {
                var table = $('#GPBayar').dataTable({
                    "order": [],
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true

                });
                var tableX = $('#GPTagihan').dataTable({
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

               $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "MasukTrnPencairanBerjangka.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                         dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[2],
                                    hdncustomer: item[0],
                                    HDNoRekening : item[1]
                                }
                            });

                            $("[id$=hdncustomer]").val('');
                            $("[id$=HDNoRekening]").val('');

                            response(found);
                        },

                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hdncustomer]").val(i.item.hdncustomer);
                    $("[id$=HDNoRekening]").val(i.item.HDNoRekening);
                },
                minLength: 3
            });
        }
 
    </script>
    </head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="ViewNotifikasikasbon.aspx">View Notifikasi Kasbon</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Notifikasi Kasbon</h4>
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
               
                 <!-- Panel Front -->
                 
                 <!-- Panel Edit -->

                 
                 

                 <asp:Panel ID="Pnltagihan" runat="server">
                     <div class="row clearfix">
                         <div class="col-md-12">
                             <div class="card-box">
                                 <h4 class="Page-title">Data Tagihan Kasbon yang Jatuh Tempo di bulan ini</h4>
                                 <div class="form-row">
                                     <div class="form-group" style="width: 100%;">
                                         <%--<div class="card-box table-responsive">--%>
                                         <asp:GridView ID="GPTagihan" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True">
                                             <Columns>
                                                 <asp:TemplateField HeaderText="No">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNumber1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Nama Cabang">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>                                                 
                                                 <asp:TemplateField HeaderText="Nik Karyawan">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnikKaryawan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodenik")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama Karyawan">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnamaKaryawan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakry")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Rekening">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_rekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>                                                  
                                                 <asp:TemplateField HeaderText="Tgl Jth Tempo">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgl_awal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>                                                
                                                 <asp:TemplateField HeaderText="Angs_ke">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblangske" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angske")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Jumlah">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbljumlah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhangsuran","{0:N0}")%>'></asp:Label>
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
                 
                 
                 <asp:Panel ID="PnlBayar" runat="server">
                     <div class="row clearfix">
                         <div class="col-md-12">
                             <div class="card-box">
                                 <h4 class="Page-title">Data Pembayaran Kasbon di bulan ini</h4>
                                 <div class="form-row">
                                     <div class="form-group" style="width: 100%;">
                                         <%--<div class="card-box table-responsive">--%>
                                         <asp:GridView ID="GPBayar" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True">
                                             <Columns>
                                                 <asp:TemplateField HeaderText="No">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNumber1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Nama Cabang">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "cabang_nama")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>                                                 
                                                 <asp:TemplateField HeaderText="Nik Karyawan">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnikKaryawan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodenik")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama Karyawan">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblnamaKaryawan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakry")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Rekening">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_rekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>                                                  
                                                 <asp:TemplateField HeaderText="Tgl Jth Tempo">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgl_jthtempo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>                                                
                                                 <asp:TemplateField HeaderText="Angs_ke">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblangske" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angske")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Jumlah">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbljumlah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhangsuran","{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No. Bayar">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblno_bayar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_tagihan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>                                                  
                                                 <asp:TemplateField HeaderText="Tgl bayar">
                                                     <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbltgl_bayar" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglbayar","{0:dd-MM-yyyy}")%>'></asp:Label>
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

                 

            



         



                 
                 
                 

                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
               
                <%--<asp:PostBackTrigger ControlID="GRData" />--%>
             
             
                 
           
                 <%--<asp:PostBackTrigger ControlID="GRRP" />--%>

                 
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



