<a href="MasukTrnAktivasiKTP.aspx">MasukTrnAktivasiKTP.aspx</a><%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasukTrnAdvanceSupplier.aspx.cs" Inherits="CL_Process_MasukTrnAdvanceSupplier" %> 
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

            {
                $('#<%=txtKeterangan.ClientID%>').prop('maxlength', 150);
                
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
                                    <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">Transaksi Pembayaran Hutang</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Transaksi Advance Supplier</h4>
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

                            <h4 class="m-t-0 header-title">Transaksi ADVANCE ACCOUNT PAYABLE</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px" OnSelectedIndexChanged="GRData_SelectedIndexChanged">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No Bukti">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Faktur">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Tanggal Transaksi">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Supplier">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNmLgn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaLgn")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>                                     
                                    <asp:TemplateField HeaderText="Jumlah">
                                        <ItemTemplate>
                                            <asp:Label ID="lblddlpembayaran" CssClass="" runat="server" Text='<%#Eval("JumlahTrn","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>                            
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
                                    

                                        <asp:TemplateField HeaderText="Keterangan" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                      <asp:TemplateField HeaderText="bankgiro" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbankgiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bankgiro")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                       <asp:TemplateField HeaderText="nogiro" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnogiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nogiro")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="HapusFlag" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHapusFlag" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "HapusFlag")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="TutupBukuFlag" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTutupBukuFlag" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TutupBukuFlag")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                    
                    
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdsplr" CssClass="" runat="server" Text='<%#Eval("kodelgn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdbank" CssClass="" runat="server" Text='<%#Eval("kodebank")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypejurnal" CssClass="" runat="server" Text='<%#Eval("FlagKodeJurnal")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                 <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkodejurnal" CssClass="" runat="server" Text='<%#Eval("KodeJurnal")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    
                                   
                                 
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
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
                                     <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Jth tempo</label>
                                     <asp:TextBox ID="txttgljthtmp" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>        
                                    </div>

                                

                                
                             

                     
                                            
                                                                      
                                </div>                                                    
                            </div>
                           </div>
                                   
                                               <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Cash Bank</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                       
                               
                                   
                                <div class="form-group">
                                    <label for="inputCity" class="col-form-label">Kode Bank</label>
                                    <asp:DropDownList ID="ddlKdBank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdBank_SelectedIndexChanged"></asp:DropDownList> 
                                 </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Nama Bank</label>
                                    <asp:TextBox ID="txtNmBank" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                                                              
                                </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Currency</label>
                                    <asp:DropDownList ID="ddlkursbank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkursBank_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:TextBox ID="txtkursbank" CssClass="form-control" required="required" runat="server" MaxLength="50" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                                                              
                                </div>


                                                              
                                </div>                                                    
                            </div>
                           </div>

                                               <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Supplier</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                                                 
                                <div class="form-group">
                                    <label for="inputCity" class="col-form-label">Kode Supplier</label>
                                    <asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>
                                 </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Nama Supplier</label>
                                    <asp:TextBox ID="txtNmSup" Enabled="false" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                                                              
                                </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Currency</label>
                                   <asp:DropDownList ID="ddlkursSup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkursSup_SelectedIndexChanged"></asp:DropDownList>
                                                             <asp:TextBox ID="txtkursstd" CssClass="form-control" required="required" runat="server" MaxLength="50" onkeypress="return isnumerickey(event);"></asp:TextBox>                                                                                              
                                </div>


                                                              
                                </div>                                                    
                            </div>
                           </div>

                    
                   
                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="form-row">
              
                                         <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Bank Giro</label>
                                       <asp:TextBox ID="txtBankGiro" CssClass="form-control" required="required" runat="server" MaxLength="30"></asp:TextBox>    
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">No Giro</label>
                                         <asp:TextBox ID="txtnoGiro" CssClass="form-control" required="required" runat="server" MaxLength="10"></asp:TextBox>
                                             
                                    </div>
                      
                                </div>
                                     <div class="form-row">
              
                                         <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Jumlah Advance</label>
                                        <asp:TextBox ID="txttransAP" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="true" AutoPostBack="true" OnTextChanged ="txtjlhbayar_TextChanged"></asp:TextBox>      
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Jumlah ke G/L Rp</label>
                                         <asp:TextBox ID="txtjmlhGL" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="false"></asp:TextBox>
                                         <asp:HiddenField ID="hfjmlrp" runat="server" />     
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">Jumlah ke C/L</label>
                                        <asp:TextBox ID="txtjmlhCL" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" Enabled="false"></asp:TextBox>  
                                    </div>
                                </div>
                                            
                                    <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Keterangan</label>
                                    <asp:TextBox CssClass="form-control" ID="txtKeterangan" MaxLength="150"  required="required"  runat="server" TextMode="MultiLine"></asp:TextBox>
                                        
                                    </div>
                                                   <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Type Seleksi Jurnal</label>
                                         <asp:DropDownList ID="ddltypejur" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" required="required" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddltypejur_SelectedIndexChanged">
                                                                <asp:ListItem Value="-">Silahkan Pilih</asp:ListItem>
                                                                <asp:ListItem Value="1">Double Line Jurnal</asp:ListItem>
                                                                <asp:ListItem Value="2">Multipe Line Jurnal</asp:ListItem>
                                                                <asp:ListItem Value="3">Tidak Pakai Jurnal</asp:ListItem>
                                                            </asp:DropDownList>     
                                    </div>



                                </div>                                                    
                            </div>
                           </div>


                           
                        </div>      
                                   
                </div>
              </div>
              </asp:Panel>

                 
                 
                        <asp:Panel ID="PnlGrid" Visible="false" runat="server">
                         <div class="row">
                            <div class="col-md-12">
                                <div class="card-box"> 
                                    <h4 class="Page-title">Multiple Line Journal</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                         <div class="card-box table-responsive">
                                            <asp:Button ID="Button2" class="btn btn-success waves-light text-white" runat="server" Text="Add Detail" OnClick="lnkBT_Click" />
                                        </div> 
                                            <asp:GridView ID="GRRP" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRRP_RowCommand" OnRowDataBound="GRRP_RowDataBound" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Kode GL">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlKodeGL" AutoPostBack="true" runat="server" CssClass="show-tick form-control" OnSelectedIndexChanged="ddlKodeGL_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:HiddenField ID="hdnddlKodeGL" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "KodeGL")%>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="+">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:DropDownList ID="ddlnamaGL" AutoPostBack="true" runat="server" CssClass="show-tick form-control" OnSelectedIndexChanged="ddlnamaGL_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:HiddenField ID="hdnddlnamaGL" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "namagl")%>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Keterangan">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtKT" runat="server" CssClass="form-control no-resize auto-growth required" Rows="1" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Kode Cost Center">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
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
                                            <asp:TemplateField HeaderText="Jumlah Debit">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                           <asp:TextBox CssClass="form-control" ID="jlhdebit" Text='<%#DataBinder.Eval(Container.DataItem, "jlhdebit","{0:N2}")%>' MaxLength="30" runat="server"  onkeypress="return isnumerickey(event);" required="required" onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged ="XXXTxt01_TextChanged" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Jumlah Kredit">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox CssClass="form-control" ID="jlhKredit" Text='<%#DataBinder.Eval(Container.DataItem, "jlhkredit","{0:N2}")%>' MaxLength="30" runat="server"  onkeypress="return isnumerickey(event);" required="required" onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);" OnTextChanged ="XXXTxt02_TextChanged" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DEL" data-placement="left" data-toggle="tooltip" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to delete ? ')) {  return false; }" title="Hapus"> <i class="btn-outline-danger">delete</i></asp:LinkButton>
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
                 

                 <asp:Panel ID="pnltypejurnal" Visible="false" runat="server">
                        <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="card-box">  
                                                <div class="form-row"> 
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group"> 
                                                                <p class="Page-title">4. Double Line Journal</p>
                                                                <asp:DropDownList ID="ddlJurnal" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlJurnal_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:TextBox ID="txtJurnal" CssClass="form-control" runat="server"  Enabled="false"></asp:TextBox>
                                                            </div>    

                 
                                                            </div>
                                                </div>
                                            </div>
                                        </div> 
                        </div>

                     <div class="row">

                         <%--debit--%>
                         <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Debit GL</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                        <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Account Debit</label>
                                        <asp:TextBox ID="txtDebit1" CssClass="form-control" runat="server"  ReadOnly ="true"></asp:TextBox>       
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">Nama Account debit</label>
                                        <asp:TextBox ID="txtDebit3" CssClass="form-control" runat="server"  ReadOnly ="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Cost Center</label>
                                        <asp:TextBox ID="txtDebit2" CssClass="form-control" runat="server"  ReadOnly ="true"></asp:TextBox>     
                                    </div>
                                    
                                </div>
                                        
                                    
                                                                      
                                </div>                                                    
                            </div>
                           </div>

                         <%--kredit--%>
                         <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Kredit GL</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                        <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Account Kredit</label>
                                        <asp:TextBox ID="txtKredit1" CssClass="form-control" runat="server"  ReadOnly ="true"></asp:TextBox>     
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">Nama Account Kredit</label>
                                        <asp:TextBox ID="txtKredit3" CssClass="form-control" runat="server"  ReadOnly ="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Cost Center</label>
                                         <asp:TextBox ID="txtKredit2" CssClass="form-control" runat="server"  ReadOnly ="true"></asp:TextBox>   
                                    </div>
                                    
                                </div>
                                        
                                    
                         
                                </div>                                                    
                            </div>
                           </div>

                     </div>
                 </asp:Panel>
                 
                  
                 <asp:Panel ID="Pnlfooter1" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="btnNext1" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="btnnext1_Click" Width="65px" />
                                           <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />

                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>
                 <asp:Panel ID="Pnlfooter2" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="btnNext2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="btnnext2_Click" Width="65px" />
                                           <asp:LinkButton ID="btnback2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnback2_Click" Width="65px" />

                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>
                 <asp:Panel ID="Pnlfooter3" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="btnnext3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="btnnext3_Click" Width="65px" />
                                           <asp:LinkButton ID="btnback3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnback3_Click" Width="65px" />

                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>
                  
                 <asp:Panel ID="Pnlfooter4" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <div class="card-box table-responsive">
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Delete" OnClick="btndelete_click" Enabled="true"/>
                                    <asp:Button ID="btnback4" CssClass="btn btn-success waves-light text-white" runat="server" Text="back" OnClick="btnback4_Click"  Width="65px" />
                                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnCancel_Click" Text="close" Width="65px" />
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
                 <asp:PostBackTrigger ControlID="btnNext2" />
                <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="GRData" />  
                 <asp:PostBackTrigger ControlID="GRRP" />
                 <asp:PostBackTrigger ControlID="btnback3" />
                <asp:PostBackTrigger ControlID="btnback2" />
                 <asp:PostBackTrigger ControlID="btnNext1" />
                 <asp:PostBackTrigger ControlID="btnNext3" />
                <asp:PostBackTrigger ControlID="btnback4" />
                 
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



