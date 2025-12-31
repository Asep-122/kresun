<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransactionGeneralLedger.aspx.cs" Inherits="GL_Process_TransactionGeneralLedger" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

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
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };

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

      

        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

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
                $('#<%=txtnote.ClientID%>').prop('maxlength', 50);
                
            }
            
        Resize();
            $('#txtTglL').datepicker({
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

                //var tableX = $('#GRRP').dataTable({
                //    "order": [],
                //    "columnDefs": [{
                //        "targets": 'no-sort',
                //        "orderable": false,
                //    }],
                //    ordering: [[0, 'asc']],
                //    "scrollX": true,
                //    retrieve: true

                //});

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
<%--        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                        <li class="breadcrumb-item"><a href="#">General Ledger</a></li>
                                        <li class="breadcrumb-item"><a href="#">Process</a></li>
                                        <li class="breadcrumb-item"><a href="Branch.aspx">Transaction Journal Manual</a></li>
                                    </ol>
                                </div>
                                <h4 class="page-title">Transaction Journal Manual</h4>
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
                                     <div class="form-row" runat="server" visible="false">
                                             <div class="form-group col-md-4">                                
                                                <asp:label ID="Label1" runat="server" class="col-form-label">Tgl Entry</asp:label>
                                                 <asp:TextBox ID="txttgltrnx" CssClass="form-control" required="required" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdatex_TextChanged"></asp:TextBox>      
                                             </div>
                                      </div>
                                    <h4 class="m-t-0 header-title">Data Transaction Journal Manual</h4>
                               
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="550px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Reference system">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                             
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Reference Manual">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnomanual" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBuktiKns")%>'></asp:Label>
                                                             
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date Transaction">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgltrn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type Trn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkettypetrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetType")%>'></asp:Label>
                                                             <asp:Label  Visible="false" ID="lbltypetrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Currency">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodecrc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecrc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kurs">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKurs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kursstd", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "total", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Note")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" Visible="false" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                                                <div class="card-box">
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">

                                                            <div class="form-row">
                                                            <div class="form-group">
                                                                <label class="col-form-label">No. Reference</label>
                                                                <asp:TextBox CssClass="form-control" ID="txtnobukti" MaxLength="20" runat="server" ></asp:TextBox>
                                                                 <asp:TextBox Visible="false" CssClass="form-control" ID="hdnnourut" MaxLength="30" runat="server" data-parsley-type="string" parsley-trigger="change" required="required" data-parsley-maxlength="30" data-parsley-error-message="Huruf  Max 30 Digit"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-md-4">
                                                                 <label class="col-form-label">Type Trn</label>
                                                                <asp:DropDownList ID="ddltypetrn" CssClass="form-control" required="required" runat="server"></asp:DropDownList>
                                                            
                                                            </div>
                                                            <div class="form-group col-md-4">
                                                                <label class="col-form-label">Date Transaction</label>
                                                                                                                                    
                                                                    <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtTglL" TextMode="DateTime" runat="server" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                                    
                                                                
                                                            </div>
                                                                 </div>
                                                          

                                                        </div>
                                                        <div class="form-group col-md-6">

                                                              <div class="form-group">
                                                                <label class="col-form-label">Note</label>
                                                                 <asp:TextBox CssClass="form-control" ID="txtnote" MaxLength="50" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                 <asp:DropDownList ID="ddlcurrency" Visible="false" AutoPostBack="true" CssClass="form-control" required="required" runat="server" OnSelectedIndexChanged="ddlcurrency_SelectedIndexChanged"></asp:DropDownList>
                                                                 <asp:TextBox ID="Txtkurs" Visible="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" AutoPostBack="true" runat="server"></asp:TextBox>
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

                                    <h4 class="m-t-0 header-title">Journal Transaction</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                         <div class="card-box table-responsive">
                                            <asp:Button ID="Button1" class="btn btn-success waves-light text-white" runat="server" Text="Add Detail" OnClick="lnkBT_Click" />
                                        </div>                                               
                                            
                                            
                                            
                                            <asp:GridView ID="GRRP" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRRP_RowCommand" OnRowDataBound="GRRP_RowDataBound" ShowHeaderWhenEmpty="True" AllowPaging="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Account code">
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
                                            <asp:TemplateField HeaderText="Account Name">
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
                                            <asp:TemplateField HeaderText="Description" Visible="false">
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
                                            <asp:TemplateField HeaderText="cost center" Visible="false">
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
                                            <asp:TemplateField HeaderText="Debit">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="jlhdebit" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhdebit", "{0:N2}")%>' OnTextChanged="XXXTxt01_TextChanged"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="jlhkredit" onkeyup="javascript:this.value=Comma(this.value);" AutoPostBack="true"  onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" CssClass="form-control required" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jlhkredit", "{0:N2}")%>' OnTextChanged="XXXTxt02_TextChanged"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete2" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DEL" data-placement="left" data-toggle="tooltip" OnClientClick="if (!ConfirmDeleteMessage('Are you sure you want to delete ? ')) {  return false; }" title="Hapus"> <i class="btn-outline-danger">delete</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                                <PagerSettings PageButtonCount="50" />
                                    </asp:GridView>

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
                                        
                                              <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                            <asp:LinkButton ID="LinkButton2" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />

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
                                              <asp:Button ID="Btnnextjournal" CssClass="btn btn-success waves-light text-white" runat="server" Text="Next" OnClick="Btnnextjournal_Click" Width="65px" />
                                      
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px" />
                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>



<%--                       <asp:Panel ID="PnlReport" Visible="false" runat="server">
                           <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                        <div class="card-box table-responsive">
                                      <asp:LinkButton ID="LinkButton3" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader2_Click" Width="65px" />
                             
                                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                             
                                </div>
                                </div>
                             </div>
                             </div>
                       
                       </asp:Panel>--%>

                
                        
                   
<%--            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
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
