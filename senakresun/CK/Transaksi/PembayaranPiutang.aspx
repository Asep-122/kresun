<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PembayaranPiutang.aspx.cs" Inherits="CL_Transaksi_PembayaranPiutang" %>
  
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Pembayaran Piutang</title>
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
                                    <li class="breadcrumb-item"><a href="PembayaranPiutang.aspx">Transaksi Pembayaran Piutang</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Transaksi Pembayaran Piutang</h4>
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

                            <h4 class="m-t-0 header-title">Transaksi Pembayaran Piutang</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="No Bukti">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Transaksi">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Pelanggan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNmLgn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namaLgn")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Keterangan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 

                                    <%--Hidden Value--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypetrns" CssClass="" runat="server" Text='<%#Eval("typetrn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltgljthtmp" CssClass="" runat="server" Text='<%#Eval("tgljthtmp")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdsplr" CssClass="" runat="server" Text='<%#Eval("kodelgn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkursstd" CssClass="" runat="server" Text='<%#Eval("KursSupp")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdbank" CssClass="" runat="server" Text='<%#Eval("kodebank")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnmbank" CssClass="" runat="server" Text='<%#Eval("namabank")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkursstdbank" CssClass="" runat="server" Text='<%#Eval("kursbank")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbankgiro" CssClass="" runat="server" Text='<%#Eval("bankgiro")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnogiro" CssClass="" runat="server" Text='<%#Eval("nogiro")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <%--<asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlhtrnsAP" CssClass="" runat="server" Text='<%#Eval("BungaOvd")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlhGL" CssClass="" runat="server" Text='<%#Eval("NoRekening")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlhCL" CssClass="" runat="server" Text='<%#Eval("SaldoAwal")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> --%>
                                   <%-- <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKeterangan" CssClass="" runat="server" Text='<%#Eval("keterangan")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> --%>
                                    <%--<asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblddlpembayaran" CssClass="" runat="server" Text='<%#Eval("DP")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypejurnal" CssClass="" runat="server" Text='<%#Eval("typejur")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamatsupp1" CssClass="" runat="server" Text='<%#Eval("alamat1")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamatsupp2" CssClass="" runat="server" Text='<%#Eval("alamat2")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamatsupp3" CssClass="" runat="server" Text='<%#Eval("alamat3")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkurssupp" CssClass="" runat="server" Text='<%#Eval("kurssuppcrc")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <%--<asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsldhtg" CssClass="" runat="server" Text='<%#Eval("TglKredit")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrmndr" CssClass="" runat="server" Text='<%#Eval("TglKredit")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> --%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamatbank1" CssClass="" runat="server" Text='<%#Eval("alamatbank1")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamatbank2" CssClass="" runat="server" Text='<%#Eval("alamatbank2")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamatbank3" CssClass="" runat="server" Text='<%#Eval("alamatbank3")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkursbank" CssClass="" runat="server" Text='<%#Eval("kursbank")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatusgiro" CssClass="" runat="server" Text='<%#Eval("statusgiro")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                   <%-- <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsldjlnbank" CssClass="" runat="server" Text='<%#Eval("saldojalan")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsldgirobank" CssClass="" runat="server" Text='<%#Eval("saldogiro")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> --%> 
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return confirm('Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                                                <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                <asp:HiddenField ID="HDNoSebelumnya" runat="server" />
                                               <div class="form-group col-md-6">
                                                  <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">No. Bukti*</p>
                                                            <%--<asp:TextBox CssClass="form-control" ID="txtBankId"  MaxLength="8" runat="server"  parsley-trigger="change" required="required" data-parsley-maxlength="1"  ></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlnobukti" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlNoBukti_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:TextBox ID="txtnobukti" CssClass="form-control" required="required" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Tanggal Transaksi</p>
                                                            <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" MaxLength="10"></asp:TextBox> 
                                                      </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Type Transaksi</p>
                                                            <asp:DropDownList ID="ddltypetrn" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlTypeTrn_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:TextBox ID="txttypetrn" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>          
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Tanggal Jatuh Tempo</p>
                                                            <asp:TextBox ID="txttgljthtmp" CssClass="form-control" required="required" runat="server" MaxLength="10"></asp:TextBox> 
                                                      </div>  
                                                      <div> 
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kode Supplier</p>
                                                            <asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>   
                                                      </div>   
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Nama Supplier</p>
                                                            <asp:TextBox ID="txtNmSup" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                      </div> 
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kurs Std</p>
                                                            <%--<asp:DropDownList ID="ddlkursstd" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList>--%>   
                                                            <asp:TextBox ID="txtkursstd" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                      </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kode Bank</p>
                                                            <asp:DropDownList ID="ddlKdBank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdBank_SelectedIndexChanged"></asp:DropDownList> </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Nama Bank</p>
                                                            <asp:TextBox ID="txtNmBank" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                      </div>  
                                                      <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kurs Std</p> 
                                                            <%--<asp:DropDownList ID="ddlkurstdbank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList>--%>   
                                                            <asp:TextBox ID="txtkursstdbank" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox> 
                                                      </div>   
                                                </div>
                                                   
                                            </div>
                                                <div class="form-group col-md-6">
                                                    <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Bank Giro</p>
                                                            <%--<asp:TextBox CssClass="form-control" ID="txtBankId"  MaxLength="8" runat="server"  parsley-trigger="change" required="required" data-parsley-maxlength="1"  ></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtBankGiro" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox> 
                                                        <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Nomor Giro</p>
                                                            <asp:TextBox ID="txtnoGiro" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                        </div> 
                                                        <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Status Giro</p>
                                                            <asp:DropDownList ID="ddlStatusGiro" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlStatusGiro_SelectedIndexChanged">
                                                                <asp:ListItem Text="Silahkan Pilih" Value="0" Selected="true" />
                                                                <asp:ListItem Text="C" Value="C" />
                                                                <asp:ListItem Text="M" Value="M"/>
                                                            </asp:DropDownList>
                                                            <asp:TextBox CssClass="form-control" ID="txtStatusGiro"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1" readonly ="true"></asp:TextBox>
                                                        </div> 
                                                         <div>
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Jumlah Transaksi AR</p>
                                                                <asp:TextBox ID="txttransAP" CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox>
                                                      </div>
                                                         <div>
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Jumlah ke G/L Rp</p>
                                                                <asp:TextBox ID="txtjmlhGL" CssClass="form-control" required="required" runat="server" MaxLength="50" ></asp:TextBox>
                                                      </div>
                                                      <div>
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Jumlah ke C/L</p>
                                                                <asp:TextBox ID="txtjmlhCL" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                      </div>
                                                      <div>
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Keterangan</p>
                                                                  <asp:TextBox ID="txtKeterangan" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                      
                                                      </div>
                                                        <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Cara Distribusi Pembayaran</p>
                                                            <asp:DropDownList ID="ddlJnsByr" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlJnsByr_SelectedIndexChanged">
                                                                <asp:ListItem Text="Silahkan Pilih" Selected="true" />
                                                                <asp:ListItem Text="2" Value="2" />
                                                                <asp:ListItem Text="3" Value="3"/>
                                                            </asp:DropDownList>
                                                            <asp:TextBox CssClass="form-control" ID="txtJnsByr"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1" readonly ="true"></asp:TextBox>
                                                        </div>
                                                        <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Type Seleksi Jurnal</p>
                                                            <asp:DropDownList ID="ddltypejur" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddltypejur_SelectedIndexChanged">
                                                                <asp:ListItem Text="Silahkan Pilih" Selected="true" />
                                                                <asp:ListItem Text="Double Line Jurnal" Value="1" />
                                                                <asp:ListItem Text="Multipe Line Jurnal" Value="2"/>
                                                                <asp:ListItem Text="Tidak Pakai Jurnal" Value="3"/>
                                                            </asp:DropDownList>
                                                            <asp:TextBox CssClass="form-control" ID="txttypeJur"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1" readonly ="true"></asp:TextBox>
                                                        </div>
                                                </div> 
                                         </div>      
                                        </div>
    
                         </asp:Panel>


                        <asp:Panel ID="PnlGrid" Visible="false" runat="server">
                         <div class="row">
                            <div class="col-md-12">
                                <div class="card-box"> 
                                    <h4 class="Page-title">4. Multiple Line Journal</h4>
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
                                                            <asp:TextBox ID="txtKT" runat="server" CssClass="form-control no-resize auto-growth required" Rows="1" ></asp:TextBox>
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
                                                            <asp:TextBox ID="jlhdebit" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhdebit")%>' OnTextChanged="XXXTxt01_TextChanged"></asp:TextBox>

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
                                                            <asp:TextBox ID="jlhKredit" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhkredit")%>' OnTextChanged="XXXTxt01_TextChanged"></asp:TextBox>

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
                 <asp:panel ID="pnltrn" Visible ="false" runat ="server">
                     <div class="row clearfix">
                            <div class="col-md-12"> 
                                <div class="card-box">  
                                    <h4 class="Page-title">3. Jenis Pembayaran Faktur</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GRTP" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRTP_RowCommand" ShowHeaderWhenEmpty ="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumber1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rownumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Faktur">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltglfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrnFaktur")%>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nomor Faktur">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltyptrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Jatuh Tempo">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgljthtmp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglJthTmp")%>'></asp:Label>
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
                                                            <asp:Label ID="lbljmlhtrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jmlhtrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bayar">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" />   
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Jumlah yang dibayar">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lbljmlhbyr" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label> 
                                                            <asp:TextBox ID="lbljmlhbyr" CssClass="form-control required" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Kurs Transaksi">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblkurstrngrtp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KursStd")%>'></asp:Label> 
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

                 <asp:Panel ID="pnltypejurnal" Visible="false" runat="server">
                        <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="card-box">  
                                                <div class="form-row"> 
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group"> 
                                                                <p class="Page-title">4. Double Line Journal</p>
                                                                <asp:DropDownList ID="ddlJurnal" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlJurnal_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:TextBox ID="txtJurnal" CssClass="form-control" runat="server" MaxLength="40" Enabled="false"></asp:TextBox>
                                                            </div>    
                                                            <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Debit GL</p>  
                                                                   <asp:TextBox ID="txtDebit1" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                            </div>
                                                            <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Debit CC</p>  
                                                                   <asp:TextBox ID="txtDebit2" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                            </div>
                                                            <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Debit KET</p>  
                                                                   <asp:TextBox ID="txtDebit3" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                            </div>  
                                                            <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit GL</p>  
                                                                   <asp:TextBox ID="txtKredit1" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                            </div>
                                                            <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit CC</p>  
                                                                   <asp:TextBox ID="txtKredit2" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                            </div>
                                                            <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit KET</p>  
                                                                   <asp:TextBox ID="txtKredit3" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                            </div>  
                                                            </div>
                                                    </div>
                                            </div>
                                        </div> 
                        </div>
                 </asp:Panel>

                 <asp:Panel ID="pnlIDS" Visible ="false" runat="server">
                     <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                            <h4 class="Page-title">1. Informasi Data Supplier</h4>  
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Alamat</p>
                                        <asp:TextBox ID="txtsupalamat1" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Kota</p>
                                        <asp:TextBox ID="txtsupalamat2" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>  
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Propinsi</p>
                                        <asp:TextBox ID="txtsupalamat3" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>  
                                    </div>
                                </div> 
                                <div class="form-group col-md-6">
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Mata Uang</p>
                                        <asp:TextBox ID="txtkursSup" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Saldo Piutang</p>
                                        <asp:TextBox ID="txtsldHtng" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Giro Mundur</p>
                                        <asp:TextBox ID="txtGrMndr" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                </div>
                            </div>
                        </div>
                     </div> 
                 </asp:Panel> 
                 <asp:Panel ID="PnlIDB" Visible ="false" runat="server">
                     <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                            <h4 class="Page-title">2. Informasi Data Bank</h4>  
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Alamat</p>
                                        <asp:TextBox ID="txtalamatbank1" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Kota</p>
                                        <asp:TextBox ID="txtalamatbank2" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>  
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Propinsi</p>
                                        <asp:TextBox ID="txtalamatbank3" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>  
                                    </div>
                                </div> 
                                <div class="form-group col-md-6">
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Mata Uang</p>
                                        <asp:TextBox ID="txtkursbank" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Saldo Berjalan</p>
                                        <asp:TextBox ID="txtsldjln" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Saldo Giro</p>
                                        <asp:TextBox ID="txtsldgr" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>   
                                    </div>
                                </div>
                            </div>
                        </div>
                     </div> 
                 </asp:Panel>
                  
                 <asp:Panel ID="PnlJournalSupplier" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="Btnnextjournal" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="Btnnextjournal_Click" Width="65px" />
                                           <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />

                                        </div>
                                </div>
                             </div>
                            </div> 
                       </asp:Panel>
                 
                 <asp:Panel ID="PnlJournalBank" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="BtnnextjournalIDB" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="BtnnextjournalIDB_Click" Width="65px" />
                                           <asp:LinkButton ID="LinkButton3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancelIDS_Click" Width="65px" />

                                        </div>
                                </div>
                             </div>
                            </div> 
                       </asp:Panel>
                 <asp:Panel ID="pnljournalUM" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="Button1" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="BtnnextjournalUM_Click" Width="65px" />
                                           <asp:LinkButton ID="LinkButton4" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancelIDback_Click" Width="65px" />

                                        </div>
                                </div>
                             </div>
                            </div> 
                       </asp:Panel>
                 <asp:Panel ID="pnlJournalJurnal" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="Button3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="BtnnextjournalJurnal_Click" Width="65px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                           <asp:LinkButton ID="LinkButton5" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancelJPF_Click" Width="65px" />

                                        </div>
                                </div>
                             </div>
                            </div> 
                       </asp:Panel>
                 <asp:Panel ID="Pnlfooter" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <div class="card-box table-responsive">
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click" />
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px" />
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
                <%--<asp:PostBackTrigger ControlID="Btnnextjournal" />--%> 
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



