<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMasukTrnCairGadai.aspx.cs" Inherits="CL_View_ViewMasukTrnCairGadai" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
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

               $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ViewMasukTrnAP.aspx/GetBukti",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>','tglawal':'" + document.getElementById('txtStart').value + "','tglakhir':'" + document.getElementById('txtEnd').value + "'}",
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
                                    <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">View Transaksi Lain-Lain</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Transaksi Pencairan</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            
                 <!-- Panel Header -->
               
                 <!-- Panel Front -->
       <%-- <asp:Panel ID="Pnlcustomer" Visible="true" runat="server">
              <div class="row clearfix">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <div class="row">
                     
                            <div class="col-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Pencarian Nobukti</div>
                                    <p class="text-muted font-14 m-b-30"></p>  
                                <div class="form-row">
                                    <div class="form-group">
                                <label for="inputState" class="col-form-label">Start Date</label>
                                <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date" ></asp:TextBox>
                                </div>
                            <div class="form-group">
                                <label for="inputZip" class="col-form-label">End Date</label>
                                <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server" Type="date"></asp:TextBox>                                        
                            </div>
                            </div>
                                 <div class="form-group col-md-12">
                                        <label class="col-form-label">Entry Data</label>
                                        <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server" required="required"></asp:TextBox>
                                 </div>     
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click" />                                              
                                        <asp:HiddenField ID="hdncustomer" runat="server" />
                                        <asp:HiddenField ID="HDNoRekening" runat="server" />
                                     </div>
                                                                                        
                                </div>                                                    
                            </div>
               
                       </div>
                    </div>
                </div>
                </asp:Panel>--%>
                 <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Pencairan Pinjaman</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                      <asp:GridView ID="GRData" ShowFooter="True" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" AutoGenerateColumns="False">
                                       <Columns>
                                        <asp:TemplateField HeaderText="No. Urut">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rownumber")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Produk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tgl Cair">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltglcair" runat="server" DateFormat="dd-mm-yyyy" Text='<%# Eval("tgl_drop", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                         <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                         </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           
                                           <asp:TemplateField visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblno_bukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_bukti_pencairan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Rekening">
                                            <ItemTemplate>
                                                <asp:Label ID="lblno_rekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Nama Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodesales")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Jangka">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pokok">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pokok", "{0:N2}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <FooterTemplate>
                                          <asp:Label ID="lbltotalpokok" runat="server"  />
                                          </FooterTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bunga">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MutasiBunga", "{0:N2}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <FooterTemplate>
                                          <asp:Label ID="lbltotalbunga" runat="server"  />
                                          </FooterTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                      <%--  <asp:TemplateField HeaderText="Cadangan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan", "{0:N2}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <FooterTemplate>
                                          <asp:Label ID="lbltotalcadangan" runat="server"  />
                                          </FooterTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>--%>

                                        
                                           
                                        <asp:TemplateField HeaderText="Action">
                                                                            <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnedit" runat="server" Visible="false" data-toggle="tooltip" data-placement="left" title="View" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="BtnSlip" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="BtnPDF" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
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
                                    <div class="ribbon ribbon-custom">Information Data</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                
                                    <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">No. Rekening*</label>
                                     <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>        
                                    <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoSebelumnya" runat="server" />
                                                <asp:HiddenField ID="HDNlasttransaksiap" runat="server" />
                                                <asp:HiddenField ID="HDNlasttransaksicl" runat="server" />
                                    </div>

                                      
                                    <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">No. Rekening Lama*</label>
                                     <asp:TextBox ID="txtnofaktur" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>        
                                     </div>

                       
                             <div class="form-row">
                                      <div class="form-group col-md-4">
                                    <label for="inputpid" class="col-form-label">Tanggal Pencairan</label>
                                     <asp:TextBox ID="txttgltrn" CssClass="form-control"  runat="server" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>      
                                    </div>
                                     
                            </div>

                                          
                                     <div class="form-row">
              
                                         <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Jumlah Pokok</label>
                                        <asp:TextBox ID="txttransAP" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text"  placeholder="" class="form-control autonumber" runat="server" Enabled="False"></asp:TextBox>      
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Jumlah Bunga</label>
                                         <asp:TextBox ID="txtjmlhGL" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text"  placeholder="" class="form-control autonumber" runat="server" Enabled="False"></asp:TextBox>
                                         <asp:HiddenField ID="hfjmlrp" runat="server" />     
                                    </div>
                                    <div class="form-group col-md-4" >
                                        <label for="inputZip"  class="col-form-label">Jumlah Cadangan</label>
                                        <asp:TextBox ID="txtjmlhCL" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text"  placeholder="" class="form-control autonumber" runat="server" Enabled="False"></asp:TextBox>  
                                    </div>
                                </div>
                                            
                                   
                                                
                                </div>                                                    
                            </div>
                           </div>
                                   
                          <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Information Data</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                       
                               
                                   
                                <div class="form-group">
                                    <label for="inputCity" class="col-form-label">Kode Customer</label>
                                    <asp:TextBox ID="ddlKdBank" Enabled="false" CssClass="form-control"  runat="server" MaxLength="50"></asp:TextBox> 
                                </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Nama </label>
                                    <asp:TextBox ID="txtNmBank" Enabled="false" CssClass="form-control"  runat="server" MaxLength="50"></asp:TextBox>
                                                                                              
                                </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Currency</label>
                                    <asp:DropDownList ID="ddlkursbank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  enabled="false"></asp:DropDownList>
                                    <asp:TextBox ID="txtkursbank" CssClass="form-control"  runat="server" MaxLength="50" onkeypress="return isnumerickey(event);" enabled="false"></asp:TextBox>
                                                                                            
                                </div>

                     
                                              
                                               
                                 


                                                              
                                </div>                                                    
                            </div>
                           </div>

        
            

                           
                        </div>      
                                   
                </div>
              </div>
              </asp:Panel>

             <asp:panel ID="Pnlcl" visible="false" runat ="server">
                     <div class="row clearfix">
                            <div class="col-md-12"> 
                                <div class="card-box">  
                                    <h4 class="Page-title">Mutasi Transaksi Account Receivable</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GCL" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty ="True">
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumber1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rownumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Nomor Bukti">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoBukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Nomor Faktur">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn","{0:dd-MM-yyyy}")%>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Tgl JthTempo">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglJthTmp","{0:dd-MM-yyyy}")%>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Type Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltyptrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Keterangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketJPF" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jumlah Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljmlhtrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JumlahTrn","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                   </asp:TemplateField>
                                               
                                                </Columns>
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>

                                    </div>
                                </div>
                         </div>
                     </div>
                 </asp:panel>
                 <asp:panel ID="pnlar" visible="false"  runat ="server">
                     <div class="row clearfix">
                            <div class="col-md-12"> 
                                <div class="card-box">  
                                    <h4 class="Page-title">Mutasi Transaksi Cash Ledger</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GrdCL" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty ="True">
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumber1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rownumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Nomor Bukti">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoBukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Nomor Faktur">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn","{0:dd-MM-yyyy}")%>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Tgl JthTempo">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglJthTmp","{0:dd-MM-yyyy}")%>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Type Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltyptrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Keterangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketJPF" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jumlah Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljmlhtrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JumlahTrn","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                   </asp:TemplateField>
                                               
                                                </Columns>
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>

                                    </div>
                                </div>
                         </div>
                     </div>
                 </asp:panel>
                 <asp:panel ID="pnlGL" visible="false" runat ="server">
                     <div class="row clearfix">
                            <div class="col-md-12"> 
                                <div class="card-box">  
                                    <h4 class="Page-title">Mutasi Transaksi General Ledger</h4>
                                    <div class="form-group col-md-4">
                                        &nbsp;</div>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GGL" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  ShowHeaderWhenEmpty ="True">
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumber22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account Code">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglfaktur22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodegl")%>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account Name">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namagl")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Keterangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltyptrn22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost Center">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgljthtmp22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "costcenter")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debit">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketJPF22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhdebit","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kredit">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljmlhtrn22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhkredit","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>

                                    </div>
                                </div>
                         </div>
                     </div>
                 </asp:panel>




                 
                 
                  
                 <asp:Panel ID="Pnlfooter1" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <%--<asp:Button ID="btnNext1" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="btnnext1_Click" Width="65px" />--%>
                                           <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />

                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>
              </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                     
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



