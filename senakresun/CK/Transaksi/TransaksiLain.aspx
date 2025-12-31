<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransaksiLain.aspx.cs" Inherits="CL_Transaksi_TransaksiLain" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <title>Transaction Journal</title>
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
    <%--<a href="BudgetAccount.aspx">BudgetAccount.aspx</a>--%>

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

        var x = 0;
        var y = 0;
        var z = 0;
        function calc(obj) {
                var e = obj.id.toString();
                if (e == 'txtKurs') {
                    x = Number(obj.value);
                    y = Number(document.getElementById('txtjmltrans').value);
                } else {
                    x = Number(document.getElementById('txtKurs').value);
                    y = Number(obj.value);
                }
                z = x * y;
            document.getElementById('txtjmlrp').value = z; 
            document.getElementById('hfjmlrp').value = z;
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

        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

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
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

        Resize();
           $('#txttgltrn').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });  
            $('#txttgljthtempo').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttglentry').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('.selectpicker').selectpicker(); 

            // $('.selectpicker').selectpicker('refresh');

            // alert('');

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

            });
             
            $("#rbJurnal").change(function () { 
                var coba = document.getElementsByName("rbJurnal"); 
                //var coba = $(this).val() 
                for (var x = 0; x < coba.length; x++) {
                    if (coba[x].checked) {
                        var values = coba[x].value
                        //alert(coba[x].value);
                        if (values == '1') {
                            $('#FileJurnal').show();    
                            $ansPanel.show();
                            $('#pnlJournal').hide();
                            $('#Pnlfooter').show();
                        }else if (values == '2')
                        { 
                            $('#FileJurnal').hide();  
                            $('#pnlJournal').show();
                            document.all("Pnlfooter").style.display = "none";
                            $('#Pnlfooter').hide();
                        } else { 
                            $('#FileJurnal').hide();  
                            $('#pnlJournal').hide();
                            $('#Pnlfooter').show(); 
                        }
                    }
                } 
            }); 
            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley(); 
            
        }

        

        $("#txtDate").autocomplete({
            source: function (request, response) {

                alert('');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "TransaksiGLS.aspx/GetTempat",
                    data: "{'InputText':'" + document.getElementById('txtKodeGL').value + "','<%=SessKdpersh%>'}",
                    dataType: "json",
                    success: function (data) {
                        obj = JSON.parse(stringToArray(data.d));
                        console.log(obj);
                        var found = $.map(obj, function (item) {
                            return {
                                label: item[1],
                                hdnddlKodeGL: item[0]
                            }
                        });

                        $("[id$=hdnddlKodeGL]").val('');

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
                $("[id$=hdnddlKodeGL]").val(i.item.hdnddlKodeGL);
            },
            minLength: 3
        });



    </script>
    <style type="text/css">
        .form-control {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="TransactionAccountPayable.aspx">Transaction Cash Ledger</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Transaction Cash Ledger</h4>
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
                                    <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Transaction" OnClick="btnAdd_Click" />
                                    
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Panel Front -->
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <h4 class="m-t-0 header-title">Data Transaksi Lain - Lain</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="550px">
                                                <Columns> 
                                     <asp:TemplateField HeaderText="No Bukti">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Transaksi">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kode Bank">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKdBank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeBank")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type Transaksi">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypetrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Keterangan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeterangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                        <%--HIDDEN--%>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltgljthtmp" CssClass="" runat="server" Text='<%#Eval("TglJthTmp")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbankgiro" CssClass="" runat="server" Text='<%#Eval("BankGiro")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoGiro" CssClass="" runat="server" Text='<%#Eval("NoGiro")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatusgiro" CssClass="" runat="server" Text='<%#Eval("Statusgiro")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkurs" CssClass="" runat="server" Text='<%#Eval("kursstd")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljmlhRP" CssClass="" runat="server" Text='<%#Eval("JumlahBayar")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljmlhtrn" CssClass="" runat="server" Text='<%#Eval("JumlahTrn")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAlamat1" CssClass="" runat="server" Text='<%#Eval("Alamat1")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAlamat2" CssClass="" runat="server" Text='<%#Eval("Alamat2")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAlamat3" CssClass="" runat="server" Text='<%#Eval("Alamat3")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltxtkurs" CssClass="" runat="server" Text='<%#Eval("kodeCrc")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsaldoawal" CssClass="" runat="server" Text='<%#Eval("SaldoJalan")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsaldogiro" CssClass="" runat="server" Text='<%#Eval("SaldoAwalGr")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypejurnal" CssClass="" runat="server" Text='<%#Eval("FlagKodeJurnal")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkodejurnal" CssClass="" runat="server" Text='<%#Eval("kodejurnal")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField> 
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltgladd" CssClass="" runat="server" Text='<%#Eval("DateAdded")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpic" CssClass="" runat="server" Text='<%#Eval("AddedByFK")%>'></asp:Label>                           
                                            </ItemTemplate>                            
                                        </asp:TemplateField>
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

                    <asp:Panel ID="pnlIDB" Visible ="false" runat="server">
                     <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                            <h4 class="m-t-0 header-title">Informasi Data Bank</h4>  
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Address</p>
                                        <asp:TextBox CssClass="form-control" ID="txtAlamat1"  MaxLength="50" runat="server"   required="required" data-parsley-maxlength="1" ></asp:TextBox>
                                        
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">City</p>
                                        <asp:TextBox CssClass="form-control" ID="txtAlamat2"  MaxLength="50" runat="server"   required="required" data-parsley-maxlength="1" ></asp:TextBox>
                                        
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Province</p>
                                        <asp:TextBox CssClass="form-control" ID="txtAlamat3"  MaxLength="50" runat="server"   required="required" data-parsley-maxlength="1" ></asp:TextBox>
                                    </div> 
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Mata Uang</p>
                                        <asp:TextBox CssClass="form-control" ID="txtkurs1"  MaxLength="3" runat="server"   required="required" data-parsley-maxlength="1" ></asp:TextBox>
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Saldo Berjalan</p>
                                        <asp:TextBox CssClass="form-control" ID="txtsaldojln"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1"></asp:TextBox>
                                    </div>
                                    <div class="">
                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Saldo Giro</p>
                                        <asp:TextBox CssClass="form-control" ID="txtsaldogiro"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1" ></asp:TextBox>
                                    </div>
                                    <div class ="form-group" >
                                     <p class="mb-1 mt-4 font-weight-bold text-muted">Type Seleksi Jurnal</p>
                                         <asp:RadioButtonList ID="rbJurnal" runat="server" 
                                             RepeatDirection="Vertical" RepeatLayout="Table" OnSelectedIndexChanged="RadioButton1_CheckedChanged" AutoPostBack="true">
                                             <asp:ListItem Text="Double Line Jurnal" Value="1"></asp:ListItem>
                                             <asp:ListItem Text="Multipe Line Jurnal" Value="2"></asp:ListItem>
                                             <asp:ListItem Text="Tidak Pakai Jurnal" Value="3" Selected="True"></asp:ListItem>
                                         </asp:RadioButtonList>
                                     </div>
                                    <div class="form-group" id="FileJurnal" runat="server" visible ="false">
                                                            <div class="form-group"> 
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Jurnal</p>
                                                                <asp:DropDownList ID="ddlJurnal" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlJurnal_SelectedIndexChanged"></asp:DropDownList>
                                                               <asp:TextBox ID="txtJurnal" CssClass="form-control" runat="server" MaxLength="40" Enabled="false"></asp:TextBox>
                                                            </div>   
                                                            <div class="row">
                                                                <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Debit GL</p>  
                                                                   <asp:TextBox ID="txtDebit1" CssClass="form-control" runat="server" MaxLength="40" Width="188px" ></asp:TextBox>
                                                                </div>
                                                                <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Debit CC</p>  
                                                                   <asp:TextBox ID="txtDebit2" CssClass="form-control" runat="server" MaxLength="40" Width="188px" ></asp:TextBox>
                                                                </div>
                                                                <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Debit KET</p>  
                                                                   <asp:TextBox ID="txtDebit3" CssClass="form-control" runat="server" MaxLength="40" Width="188px" ></asp:TextBox>
                                                                </div>
                                                            </div> 
                                                            <div class="row">
                                                                <div class="form-group">
                                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit GL</p>  
                                                                   <asp:TextBox ID="txtKredit1" CssClass="form-control" runat="server" MaxLength="40" Width="188px" ></asp:TextBox>
                                                                </div>
                                                                <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit CC</p>  
                                                                   <asp:TextBox ID="txtKredit2" CssClass="form-control" runat="server" MaxLength="40" Width="188px" ></asp:TextBox>
                                                                </div>
                                                                <div class ="form-group"> 
                                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit KET</p>  
                                                                   <asp:TextBox ID="txtKredit3" CssClass="form-control" runat="server" MaxLength="40" Width="188px" ></asp:TextBox>
                                                               </div>
                                                            </div>    
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
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kode Bank</p>
                                                            <asp:DropDownList ID="ddlkdBank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlNamaBank_SelectedIndexChanged"></asp:DropDownList>
                                                          </div>
                                                    <div>
                                                           <p class="mb-1 mt-4 font-weight-bold text-muted">Nama Bank</p>
                                                           <asp:TextBox ID="txtnmBank" CssClass="form-control" required="required"  MaxLength="50" runat="server"></asp:TextBox>
                                                    </div> 
                                                   <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">No. Bukti</p>
                                                            <asp:DropDownList ID="ddlnoBukti" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlNoBukti_SelectedIndexChanged" ></asp:DropDownList>
                                                            <asp:TextBox CssClass="form-control" ID="txtNoBukti"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                                        
                                                   </div>
                                                   <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Type Transaksi</p>
                                                            <asp:DropDownList ID="ddlTypeTrn" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddltYPEtRN_SelectedIndexChanged" ></asp:DropDownList>
                                                            <asp:TextBox CssClass="form-control" ID="txtTypeTrn"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                                   </div>
                                                   <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Tgl Transaksi</p>
                                                            <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" MaxLength="10"></asp:TextBox> 
                                                   </div>
                                                   <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Tgl Jatuh Tempo</p>
                                                            <asp:TextBox ID="txttgljthtempo" CssClass="form-control" required="required" runat="server" MaxLength="10"></asp:TextBox> 
                                                  </div>
                                                   <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Bank Giro</p> 
                                                             <asp:TextBox CssClass="form-control" ID="txtbankgiro"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                                    </div> 
                                                   <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Nomor Giro</p> 
                                                            <asp:TextBox CssClass="form-control" ID="txtnogiro"  MaxLength="10" runat="server"   required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                                   </div> 
                                                </div> 
                                                <div class="form-group col-md-6">
                                                    <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Tanggal Entry</p>
                                                             <asp:TextBox CssClass="form-control" ID="txttgl"   runat="server" readonly = "true"></asp:TextBox>
                                                    </div> 
                                                    <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Entry Oleh</p>
                                                            <asp:TextBox CssClass="form-control" ID="txtpic"   runat="server" readonly = "true"></asp:TextBox>
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
                                                    <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kurs</p>
                                                            <asp:TextBox CssClass="form-control" ID="txtKurs"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1" onkeyup="calc(this)" ></asp:TextBox>
                                                    </div> 
                                                    <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Jumlah Transaksi</p>
                                                            <asp:TextBox CssClass="form-control" ID="txtjmltrans"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1" onkeyup="calc(this)" ></asp:TextBox>
                                                    </div> 
                                                    <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Jumlah Rupiah</p>
                                                            <asp:TextBox CssClass="form-control" ID="txtjmlrp"  MaxLength="30" runat="server"   required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                                            <asp:HiddenField ID="hfjmlrp" runat="server" />
                                                    </div> 
                                                    <div class="">
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Keterangan</p>
                                                            <asp:TextBox CssClass="form-control" ID="txtKeterangan"  MaxLength="50" runat="server"   required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                                    </div> 
                                                </div>
                                            </div>
                                         </div> 
                                    </div> 
                    </asp:Panel>
                    <asp:Panel ID="PnlJournal" Visible="false" runat="server">
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
                    <asp:Panel ID="PnlJournalGRRP" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                          <asp:Button ID="BtnnextjournalGRRP" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="BtnnextjournalGRRP_Click" Width="65px" />
                                           <asp:LinkButton ID="LinkButton3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancelGRRP_Click" Width="65px" />

                                        </div>
                                </div>
                             </div>
                            </div> 
                       </asp:Panel>

                        <asp:Panel ID="PnlGrid" Visible="false" runat="server">
                         <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">

                                    <h4 class="m-t-0 header-title">Cash Ledger Transaction</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                         <div class="card-box table-responsive">
                                            <asp:Button ID="Button1" class="btn btn-success waves-light text-white" runat="server" Text="Add Detail" OnClick="lnkBT_Click" />
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
                                                            <asp:TextBox ID="txtKT" runat="server" CssClass="form-control no-resize auto-growth required" Rows="1" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>'>></asp:TextBox>
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
            <%--</ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="GRData" />
                 <asp:AsyncPostBackTrigger ControlID ="ddlkdBank" />
            </Triggers>
        </asp:UpdatePanel>--%>
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
