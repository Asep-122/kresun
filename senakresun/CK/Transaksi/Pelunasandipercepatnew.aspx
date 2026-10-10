<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pelunasandipercepatnew.aspx.cs" Inherits="CL_Pelunasandipercepatnew" %> 
<%--<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Pelunasan Dipercepat</title>
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


   
       <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

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

        function Alertfailed(h, t, p, i) {
            setTimeout(function () {
                Swal.fire({
                    title: h,
                    text: t,
                    icon: "error"
                });
            }, 100);

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
                        window.location.href = "Pelunasandipercepatnew.aspx"
                    }
                });;
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
                        url: "Pelunasandipercepatnew.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                         dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[2],
                                    hdncustomer: item[0],
                                    HDNoRekening: item[1] 
                                }
                            });

                            $("[id$=hdncustomer]").val('');
                            $("[id$=HDNoRekening]").val('');
                            $("[id$=HDNKodeCabang]").val('');

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
                    $("[id$=HDNKodeCabang]").val(i.item.HDNKodeCabang);
                },
                minLength: 3
            });
        }
 
    </script>
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            -ms-flex: 0 0 100%;
            flex: 0 0 100%;
            max-width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="TakeOver.aspx">Transaksi Pelunasan Dipercepat</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Transaksi Pelunasan Dipercepat</h4>
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

                            <h4 class="m-t-0 header-title">Transaksi Pelunasan Dipercepat</h4>                           
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
                                    <asp:TemplateField HeaderText="No Rekening">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Tanggal Transaksi">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglJthTmp","{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>     
                                        <asp:TemplateField HeaderText="Nama Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnmnasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaNasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>       
                                        <asp:TemplateField HeaderText="Jumlah Piutang">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljlhtrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JmlPiutang","{0:N2}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Jumlah Pinalty" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbljlhto" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BiayaPenalti","{0:N2}")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Pelunasan">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltekor" CssClass=""  runat="server" Text='<%#Eval("JlhPelunasan","{0:N2}")%>'></asp:Label> 
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
                                    

                                        <asp:TemplateField HeaderText="kurs" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkurs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KursStd")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                      <asp:TemplateField HeaderText="kodebank" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkdbank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodebank")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                       <asp:TemplateField HeaderText="kodecrc" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkdcrc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCrc")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                       <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdsplr" CssClass="" runat="server" Text='<%#Eval("kodelgn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="HapusFlag" Visible="false">
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
                                            <asp:Label ID="lblkdbank" CssClass="" runat="server" Text='<%#Eval("kodebank")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkodecabang" CssClass="" runat="server" Text='<%#Eval("kodecabangf")%>'></asp:Label>                           
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
                                    </asp:TemplateField> --%>
                                    
                                   
                                 
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                            <%--<asp:LinkButton ID="BtnSlip" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="BtnPDF" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Pusat" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="BtnPDF1" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Cabang" CommandName="PDF1" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>--%>
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

                 <asp:Panel ID="Pnlcustomer" Visible="false" runat="server">
              <div class="row clearfix">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <div class="row">
                     
                            <div class="col-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Pencarian Customer</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                 <div class="form-group col-md-12">
                                        <label class="col-form-label">Entry Data Customer</label>
                                        <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server" required="required"></asp:TextBox>
                                 </div>     
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click" />                                              
                                     </div>
                                                                                        
                                </div>                                                    
                            </div>
               
                       </div>
                    </div>
                </div>
                </asp:Panel>
            <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row clearfix">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <div class="row">                          

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Entry Data</div>
                                    <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                    <asp:HiddenField ID="HDNoRekening" runat="server" />
                                       <asp:HiddenField ID="HDNKodeCabang" runat="server" />
                                       <asp:HiddenField ID="HDSelisihPlus" runat="server" />
                                       <asp:HiddenField ID="HDSelisihMinus" runat="server" />
                                       <asp:HiddenField ID="HDBUnga" runat="server" />
                                       <asp:HiddenField ID="HDPokok" runat="server" />
                                       <asp:HiddenField ID="HDFee" runat="server" />
                                       <asp:HiddenField ID="HDtotal" runat="server" />
                                       <asp:HiddenField ID="HDAO" runat="server" />
                                       <asp:HiddenField ID="HDTglDue" runat="server" />
                                      
                                                

                                    <div class="form-group">
                                        <asp:HiddenField ID="hdncustomer" runat="server" />
                                    <label for="inputpid" class="col-3 col-form-label">No. Bukti*</label>
                                     <asp:TextBox ID="txtnobukti" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>        
                                    </div> 
                                      <div class="form-group">
                                    <label for="inputpid" class="col-9 col-form-label">Tanggal Transaksi</label>
                                     <asp:TextBox ID="txttgltrn" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>      
                                    </div>
                                  <%--    <div class="form-group">
                                        <label for="inputCity" class="col-form-label">Jumlah Setoran</label>
                                        <asp:TextBox ID="txtbayar" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ></asp:TextBox>      
                                    </div>--%>
                                     
                                    <div class="form-group">
                                    <label for="inputCity" class="col-form-label">Kode Bank</label>
                                    <asp:DropDownList ID="ddlKdBank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdBank_SelectedIndexChanged"></asp:DropDownList> 
                                 </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Nama Bank</label>
                                    <asp:TextBox ID="txtNmBank" Enabled="false" CssClass="form-control"  runat="server" MaxLength="50"></asp:TextBox>
                                                                                              
                                </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Currency</label>
                                    <asp:DropDownList ID="ddlkursbank" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkursBank_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:TextBox ID="txtkursbank" CssClass="form-control" runat="server" MaxLength="50" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                                                              
                                </div>

                                </div>                                                    
                            </div>
                           </div>

                           

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Data Customer</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                             
                                <div class="form-group">
                                    <label for="inputCity" class="col-form-label">Kode Customer</label>
                                    <%--<asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtkodelgn" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox> 
                                </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Nama Nasabah</label>
                                    <asp:TextBox ID="txtNmSup" Enabled="false" CssClass="form-control"  runat="server" MaxLength="50"></asp:TextBox>
                                  </div>
                                 <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">No Rekening</label>
                                    <asp:TextBox ID="txtnorekening" Enabled="false" CssClass="form-control"  runat="server" MaxLength="50"></asp:TextBox>
                                  </div>  
                                   
                                    
                                    <div class="form-row">
                                     <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Total Piutang</label>
                                        <asp:TextBox ID="txttotalpiutang" Visible="true" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>      
                                    </div>
                                        
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Pinalty</label>
                                        <asp:DropDownList ID="ddlpinalty" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlpinalty_SelectedIndexChanged" >
                                                       
                                                        <asp:ListItem Text="0 * Angsuran" Value="0" />
                                                        <asp:ListItem Text="1 * Angsuran" Value="1" /> 
                                                        <asp:ListItem Text="2 * Angsuran" Value="2" /> 
                                                        <asp:ListItem Text="3 * Angsuran" Value="3" /> 
                                                        <asp:ListItem Text="4 * Angsuran" Value="4" /> 
                                                        <asp:ListItem Text="5 * Angsuran" Value="5" /> 
                                                    </asp:DropDownList> 
                                        <asp:TextBox ID="Txtangsuran" Visible="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox> 
                                        <asp:TextBox ID="txtbiayapinalty" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server"  OnTextChanged ="ontextchanged_txtpenalty" AutoPostBack="true"></asp:TextBox>      
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Jumlah Pelunasan</label>
                                        <asp:TextBox ID="txtJlhpelunasan" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" ></asp:TextBox>      
                                    </div>
                                    
                                       </div>
                                            
                                    

                                </div>                                                    
                            </div>
                           </div>

                           
               </div>      
                                   
                </div>
              </div>
              </asp:Panel>
                 
              <asp:panel ID="pnltrn" Visible ="false" runat ="server">
                     <div class="row clearfix">
                            <div class="auto-style1"> 
                                <div class="card-box">  
                                    <h4 class="Page-title">Transaksi Piutang Customer</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GRTP" runat="server" ShowFooter="True" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  OnRowDataBound="GRTP_RowDataBound" ShowHeaderWhenEmpty ="True" AllowSorting="True" PageSize="50">
                                                
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="Nomor Rekening">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Jatuh Tempo">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgljthtmp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_jwl","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Produk">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    
                                                    
                                                    <asp:TemplateField HeaderText="Angs-ke">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblangs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angs_ke")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                 
                                            
                                                    <asp:TemplateField HeaderText="Pokok">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pokok","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                    <asp:Label ID="lbltotalPokok" runat="server"  />
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bunga">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <asp:Label ID="lbltotalBunga" runat="server"  />
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Angsuran">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAngsuran" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuran","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lbltotalAngsuran" runat="server"  />
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FeeChanneling">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfeechanneling" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feechannelling","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                            <asp:Label ID="lbltotalfeechanneling" runat="server"  />
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
											
                                                </Columns>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>

                                    </div>
                                </div>
                         </div>
                     </div>
                 </asp:panel>   
                 <asp:Panel ID="Pnlfooter4" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <div class="card-box table-responsive">
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btndelete" CausesValidation="TRUE" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" CssClass="btn btn-success waves-light text-white" runat="server" Text="Delete" OnClick="btndelete_click" Enabled="true"/>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnCancel_Click" Text="Close" Width="65px" UseSubmitBehavior="false" CausesValidation="false"/>
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
                <asp:PostBackTrigger ControlID="txttgltrn" />
                <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="GRData" />  
                <asp:PostBackTrigger ControlID="ddlkdbank" />
                <asp:PostBackTrigger ControlID="ddlpinalty" />  
                 <%--<asp:PostBackTrigger ControlID="GRRP" />--%>

                 
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




