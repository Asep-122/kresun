<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransaksiPerawatan.aspx.cs" Inherits="FA_Process_TransaksiPerawatan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <title>Transaksi Perawatan</title>
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
    <%--<a href="BudgetAccount.aspx">BudgetAccount.aspx</a>--%>

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
        function KurangDebitKredit(errMsg, kurang) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        text: "Kekurangan Debit atau kredit anda " + kurang,
                        type: "warning",
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
            }, 100);

        }
        var x = 0;
        var y = 0;
        var z = 0;
        function calc(obj) {
                var e = obj.id.toString();
                if (e == 'txtKurs') {
                    x = Number(obj.value);
                    y = Number(document.getElementById('txtjmlbiaya').value);
                } else {
                    x = Number(document.getElementById('txtKurs').value);
                    y = Number(obj.value);
                }
                z = x * y;
            document.getElementById('txtjmlrp').value = z; 
            document.getElementById('hfjmlrp').value = z;
        } 

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        
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
        function sweetAlertConfirm(btndelete, errMsg) {
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

        //function Resize() {
        //    setTimeout(function (e) {
        //        //  alert('z');
        //        window.top.cekResize();
        //        var y;
        //        y = window.parent.document.getElementById('txtScrollY').value;
        //        setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
        //    }, 50);

        //}

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
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            }); 
             

        //Resize();
           $('#txttglTransaksi').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtTglJthTempo').datepicker({
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">FA</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="TransaksiPerawatan.aspx">Transaksi Perawatan</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Transaction Perawatan Fix Asset</h4>
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

                                    <h4 class="m-t-0 header-title">Data Transaksi Perawatan</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="550px">
                                                <Columns>
                                                    <%--DISPLAY--%>  
                                                        <asp:TemplateField HeaderText="KodeAktiva" > 
                                                            <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkodeaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeAktiva")%>'></asp:Label>
                                                             </ItemTemplate>
                                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Bukti">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltgltrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Keterangan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblkdlgn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jumlah">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltypetrans" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jumlah","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jlh Debit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljlhdebit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhDebit","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Jlh Kredit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljlhkredit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhKredit","{0:N2}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left"  title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                             <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnPDF" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                            <%--<asp:LinkButton ID="BtnPDF" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>--%>
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

                    <!-- Panel Edit -->
                <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                  <div class="card-box">
                                      <div class="form-row"> 
                                          <div class="form-group col-md-6">
                                              <div class="form-group">  
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted">Kode Aktiva</p>
                                                  <div class ="form-row">
                                                      <div class="form-group col-md-11">
                                                          <asp:DropDownList ID="ddlkodeaktiva" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"  OnSelectedIndexChanged ="ddlkodeaktiva_SelectedIndexChanged"></asp:DropDownList>        
                                                      </div>
                                                  </div>
                                                      <asp:TextBox ID="txtnamaaktiva" CssClass="form-control" required="required" runat="server" MaxLength="40" Enabled="false"></asp:TextBox>
                                                  </div>
                                          <div class="form-group">
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted">No. Bukti*</p>
                                                  <asp:TextBox ID="txtnoBukti" CssClass="form-control" runat="server" MaxLength="40" Enabled="false"></asp:TextBox>
                                          </div>
                                          <div class="form-group">
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted">Type Transaksi*</p>
                                                  <asp:DropDownList ID="ddltypetrans" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlTypTrans_SelectedIndexChanged">
                                                      <%--<listitem Text "></listitem>--%>
                                                  </asp:DropDownList>
                                                  <asp:TextBox ID="txttypetrans" CssClass="form-control" required="required" runat="server" MaxLength="40" Enabled="false"></asp:TextBox>
                                          </div>
                                          <div class="form-group">
                                              <p class="mb-1 mt-4 font-weight-bold text-muted">Tanggal Transaksi</p>
                                              <asp:TextBox CssClass="form-control" ID="txttglTransaksi" required="required" TextMode="DateTime" runat="server" AutoPostBack="true" OnTextChanged ="tgltrn_textchange"></asp:TextBox>
                                          </div>
                                          <div class="form-group">
                                              <p class="mb-1 mt-4 font-weight-bold text-muted">Tanggal Mulai Susut</p>
                                              <asp:TextBox CssClass="form-control" ID="txttglmulaisusut" required="required" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox> 
                                          </div>
                                          <div class="form-group">
                                              <p class="mb-1 mt-4 font-weight-bold text-muted">Tanggal Akhir Susut</p>
                                              <asp:TextBox CssClass="form-control" ID="txttglakhirsusut" required="required" Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox> 
                                          </div>
                                          <div class="form-group">
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted">Keterangan</p>
                                                  <asp:TextBox class="form-control" id="txtketerangan" runat="server" required="required"  TextMode="Multiline"></asp:TextBox>
                                          </div>
                                          <div class="form-group" id="FileJurnal" visible="false" runat="server">
                                              <div class="form-group"> 
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted">Jurnal</p>
                                                  <asp:DropDownList ID="ddlJurnal" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlJurnal_SelectedIndexChanged"></asp:DropDownList>
                                                 <asp:TextBox ID="txtJurnal" CssClass="form-control" runat="server" MaxLength="50" Enabled="false"></asp:TextBox>
                                              </div>   
                                              <div class="row">
                                                  <div class="form-group col-md-4">
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted">Debit GL</p>  
                                                     <asp:TextBox ID="txtDebit1" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true" ></asp:TextBox>
                                                  </div>
                                                  <div class ="form-group col-md-4"> 
                                                      <p class="mb-1 mt-4 font-weight-bold text-muted">Debit CC</p>  
                                                     <asp:TextBox ID="txtDebit2" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                  </div>
                                                  <div class ="form-group col-md-4"> 
                                                      <p class="mb-1 mt-4 font-weight-bold text-muted">Debit KET</p>  
                                                     <asp:TextBox ID="txtDebit3" CssClass="form-control" runat="server" MaxLength="150" ReadOnly ="true"></asp:TextBox>
                                                  </div> 
                                               </div>
                                               <div class="row">
                                                  <div class="form-group col-md-4">
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit GL</p>  
                                                     <asp:TextBox ID="txtKredit1" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                  </div>
                                                  <div class ="form-group col-md-4"> 
                                                      <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit CC</p>  
                                                     <asp:TextBox ID="txtKredit2" CssClass="form-control" runat="server" MaxLength="40" ReadOnly ="true"></asp:TextBox>
                                                  </div>
                                                  <div class ="form-group col-md-4"> 
                                                      <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit KET</p>  
                                                     <asp:TextBox ID="txtKredit3" CssClass="form-control" runat="server" MaxLength="150" ReadOnly ="true"></asp:TextBox>
                                                 </div> 
                                               </div>   
                                              </div>
                                          </div>
                                          <div class="form-group col-md-6">
                                              <div class="form-group">
                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Umur Ekonomis</p>
                                                <div class="form-group row">
                                                    <div class="form-inline col-lg-12">    
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtnomorekonomis" CssClass="form-control" runat="server" MaxLength="40" Enabled="false">0</asp:TextBox>    
                                                        </div>
                                                        <div class="col-1">
                                                            <label for="inputState" class="col-1 col-form-label">Tahun</label>     
                                                        </div>
                                                    </div>  
                                                </div>
                                              </div>
                                              <div class="form-group">
                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Masa Manfaat</p>
                                                <div class="form-group row">
                                                    <div class="form-inline col-lg-12">    
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtmasamanfaat" CssClass="form-control" runat="server" MaxLength="40" Enabled="false">0</asp:TextBox>    
                                                        </div>
                                                        <div class="col-1">
                                                            <label for="inputState" class="col-1 col-form-label">Bulan</label>     
                                                        </div>
                                                    </div>  
                                                </div>
                                              </div>
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Harga Perolehan</p> 
                                                    <asp:TextBox ID="txthargaperolehan" CssClass="form-control" Enabled="false" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);" onkeyup="calc(this)"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Nilai Residu</p> 
                                                    <asp:TextBox ID="txtnilairesidu" CssClass="form-control" Enabled="false" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);" onkeyup="calc(this)"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Harga Pembelian</p> 
                                                    <asp:TextBox ID="txthargapembelian" CssClass="form-control" Enabled="false" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);" onkeyup="calc(this)"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Akumulasi Susut</p> 
                                                    <asp:TextBox ID="txtakumulasisusut" CssClass="form-control" Enabled="false" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);" onkeyup="calc(this)"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Penambahan Harga</p> 
                                                    <asp:TextBox ID="txtpenambahanharga" CssClass="form-control" Enabled="false" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);" onkeyup="calc(this)"></asp:TextBox>
                                            </div> 
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Pengurangan Harga</p>
                                                    <asp:TextBox ID="txtpenguranganharga" CssClass="form-control" Enabled="false" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"  ></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Mata Uang</p>
                                               <asp:DropDownList ID="ddlCurrency" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlcurrency_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                               <%--<asp:TextBox ID="txtcurrency" CssClass="form-control" required="required" runat="server" MaxLength="40"></asp:TextBox>--%>
                                               <asp:TextBox ID="txtnegara" CssClass="form-control" required="required" runat="server" MaxLength="40" Enabled ="false"></asp:TextBox>
                                            </div> 
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Kurs</p> 
                                               <asp:DropDownList ID="ddlkurs" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkurs_SelectedIndexChanged"></asp:DropDownList>
                                                    <%--<asp:DropDownList ID="ddlkurs" CssClass="form-control" required="required" runat="server" OnSelectedIndexChanged ="ddlkurs_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtKurs" CssClass="form-control" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);" onkeyup="calc(this)"></asp:TextBox>
                                            </div> 
                                            <div class="form-group">
                                                    <p class="mb-1 mt-4 font-weight-bold text-muted">Jumlah Biaya</p>
                                                    <asp:TextBox ID="txtjmlbiaya" CssClass="form-control" required="required" runat="server" MaxLength="40" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);"  AutoPostBack="true" onkeyup="javascript:this.value=Comma(this.value);"  OnTextChanged ="kurs_textchanged"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <p class="mb-1 mt-4 font-weight-bold text-muted">Jumlah Rupiah</p>
                                               <asp:TextBox ID="txtjmlrp" CssClass="form-control" required="required" runat="server" MaxLength="40" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="hfjmlrp" runat="server" />
                                            </div  >
                                          <div class="form-group">
                                              
                                              <div class="form-group col-md-1">
                                                  <p class="mb-1 mt-4 font-weight-bold text-muted" id="textnama" runat="server">Keterangan</p>
                                              </div>
                                          </div>  
                                          <div class ="form-group" >
                                              <p class="mb-1 mt-4 font-weight-bold text-muted" runat ="server" id="typejur">Type Seleksi Jurnal</p>
                                              <asp:RadioButtonList ID="rbJurnal" runat="server" 
                                                  RepeatDirection="Vertical" RepeatLayout="Table" OnSelectedIndexChanged="RadioButton1_CheckedChanged" AutoPostBack="true">
                                                  <asp:ListItem Text="Double Line Jurnal" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="Multipe Line Jurnal" Value="2"></asp:ListItem>
                                                  <asp:ListItem Text="Tidak Pakai Jurnal" Value="3" Selected="True"></asp:ListItem>
                                              </asp:RadioButtonList>
                                          </div> 
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
                
                <asp:Panel ID="PnlGrid" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h4 class="m-t-0 header-title">Fix Assets Transaction</h4>
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
                                                                <asp:TextBox ID="txtKT" runat="server" CssClass="form-control no-resize auto-growth required" Rows="1" Text='<%#DataBinder.Eval(Container.DataItem, "Description")%>' MaxLength="150"></asp:TextBox>
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
                                                                <asp:DropDownList ID="ddlcostcenter" runat="server" CssClass="show-tick form-control" Enabled="false">
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
                                                <asp:TemplateField HeaderText="Action"  >
                                                    <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndeletegrrp" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DEL" data-placement="left" data-toggle="tooltip" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to delete ? ')) {  return false; }" title="Hapus"> <i class="btn-outline-danger">delete</i></asp:LinkButton>
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
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" Enabled="false" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menyimpan data ini ?');" runat="server" Text="Save" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btndelete" class="btn btn-success waves-light text-white" Enabled="false" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menghapus data ini ?');" runat="server" Text="Delete" OnClick="btndelete_Click" Visible="false" />
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                     <%--POPUP--%>
                <asp:LinkButton ID="lnkopenGroup" runat="server" data-toggle="modal" data-target="#modalGroup"></asp:LinkButton>
                <div id="modalGroup" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" data-backdrop="static" data-keyboard="false" aria-hidden="true" style="display: none;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <h4 class="modal-title" id="myLargeModalLabel">Detail Data Supplier</h4>
                            </div>
                            <div class="modal-body">
                                <!-- Form row -->
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">  
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-row">
                                                                <div class="form-group col-md-2">
                                                                    <label class="col-form-label">Kode Supplier</label> 
                                                                </div>
                                                                <div class="form-group col-md-9">
                                                                    <asp:Label ID="lblkdsuppu" CssClass="form-control" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div> 
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-row">
                                                                <div class="form-group col-md-2">
                                                                    <label class="col-form-label">Nama Supplier</label> 
                                                                </div>
                                                                <div class="form-group col-md-9">
                                                                    <asp:Label ID="lblnmsuppu" CssClass="form-control" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div> 
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-row">
                                                                <div class="form-group col-md-2">
                                                                    <label class="col-form-label">Kode Wilayah</label> 
                                                                </div>
                                                                <div class="form-group col-md-3">
                                                                    <asp:Label ID="lblkdwilpu" CssClass="form-control" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="form-group col-md-2 text-center">
                                                                    <label class="col-form-label">Kode Group</label> 
                                                                </div>
                                                                <div class="form-group col-md-4">
                                                                    <asp:Label ID="lblkdgrouppu" CssClass="form-control" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>  
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-row">
                                                                <div class="form-group col-md-2">
                                                                    <label class="col-form-label">Address</label> 
                                                                </div>
                                                                <div class="form-group col-md-9">
                                                                    <asp:Label ID="lblalamat1pu" CssClass="form-control" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div> 
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-group">
                                                                <div class="form-row">
                                                                    <div class="form-group col-md-2">
                                                                        <label class="col-form-label">City</label>
                                                                    </div>
                                                                    <div class="form-group col-md-9">
                                                                        <asp:Label ID="lblalamat2pu" CssClass="form-control" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div> 
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-group">
                                                                <div class="form-row">
                                                                    <div class="form-group col-md-2">
                                                                        <label class="col-form-label">Province</label>
                                                                    </div>
                                                                    <div class="form-group col-md-9">
                                                                        <asp:Label ID="lblalamat3pu" CssClass="form-control" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>  
                                                    <div class="form-row">
                                                        <div class="form-group col-md-12">
                                                            <div class="form-group">
                                                                <div class="form-row">
                                                                    <div class="form-group col-md-2">
                                                                        <label class="col-form-label">Mata Uang</label>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <asp:Label ID="lblcrcpu" CssClass="form-control" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="form-group col-md-2  text-center">
                                                                        <label class="col-form-label">Saldo Jalan</label>
                                                                    </div> 
                                                                    <div class="form-group col-md-4">
                                                                        <asp:Label ID="lblsldjlnpu" CssClass="form-control" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>     
                                                </div>
                                            </div>
                                        </div> 
                                    </div>
                                </div>
                            </div>
                            <!-- end row -->
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                
                  <%-- <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rbJurnal" EventName="SelectedIndexChanged" />
                    </Triggers>--%>
                   
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ddlkodeaktiva" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="Btnnextjournal" />
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btndelete" />
                <asp:PostBackTrigger ControlID="LinkButton2" />
                <asp:PostBackTrigger ControlID="GRRP" />
                <asp:PostBackTrigger ControlID="ddlJurnal" />
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID ="rbJurnal" />
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
