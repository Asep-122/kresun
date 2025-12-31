<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AeJurnal.aspx.cs" Inherits="AE_Master_AeJurnal" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Account Employee Jurnal</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link href="../../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../assets/js/modernizr.min.js"></script>

    <link href="../../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../../assets/js/popper.min.js"></script>
    <script src="../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../assets/js/waves.js"></script>
    <script src="../../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../../Javascript/jquery-ui.js"></script>

        <!-- Toastr css -->
    <link href="../../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />
    
        <!-- Modal -->
    <link href="../../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />
    
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../../../plugins/tooltipster/tooltipster.bundle.min.css" />
        <!-- DataTables -->
    <link href="../../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="../../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />


   
   
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

            
            $('#txttglptkp').datepicker({
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
                                    <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="AeJurnal.aspx">Kode Jurnal</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Kode Jurnal</h4>
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
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Jurnal" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Kode Jurnal</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Kode Jurnal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdJurnal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeJurnal")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Keterangan Jurnal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetJurnal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJurnal")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>  
                                    <%--------------------------------------------------------------------------------------------------------------%>
                                    <%--Hidden--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhGajiPokok" CssClass="" runat="server" Text='<%#Eval("NoJlhGajiPokok")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <%--------------------------------------------------------------------------------------------------------------%>
                                    <%--Tunjangan--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj1" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj1")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj2" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj2")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj3" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj3")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj4" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj4")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj5" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj5")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj6" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj6")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj7" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj7")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj8" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj8")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj9" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj9")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj10" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj10")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj11" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj11")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj12" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj12")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTunj13" CssClass="" runat="server" Text='<%#Eval("NoJlhTunj13")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <%--------------------------------------------------------------------------------------------------------------%>
                                    <%--Keterangan Tunjangan--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj1" CssClass="" runat="server" Text='<%#Eval("KetTunj1")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj2" CssClass="" runat="server" Text='<%#Eval("KetTunj2")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj3" CssClass="" runat="server" Text='<%#Eval("KetTunj3")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj4" CssClass="" runat="server" Text='<%#Eval("KetTunj4")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj5" CssClass="" runat="server" Text='<%#Eval("KetTunj5")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj6" CssClass="" runat="server" Text='<%#Eval("KetTunj6")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj7" CssClass="" runat="server" Text='<%#Eval("KetTunj7")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj8" CssClass="" runat="server" Text='<%#Eval("KetTunj8")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj9" CssClass="" runat="server" Text='<%#Eval("KetTunj9")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj10" CssClass="" runat="server" Text='<%#Eval("KetTunj10")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj11" CssClass="" runat="server" Text='<%#Eval("KetTunj11")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj12" CssClass="" runat="server" Text='<%#Eval("KetTunj12")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetTunj13" CssClass="" runat="server" Text='<%#Eval("KetTunj13")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>
                                    <%--------------------------------------------------------------------------------------------------------------%>
                                    <%--POTONGAN--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhPot1" CssClass="" runat="server" Text='<%#Eval("NoJlhPot1")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhPot2" CssClass="" runat="server" Text='<%#Eval("NoJlhPot2")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhPot3" CssClass="" runat="server" Text='<%#Eval("NoJlhPot3")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhPot4" CssClass="" runat="server" Text='<%#Eval("NoJlhPot4")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhPot5" CssClass="" runat="server" Text='<%#Eval("NoJlhPot5")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhPot6" CssClass="" runat="server" Text='<%#Eval("NoJlhPot6")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhPot7" CssClass="" runat="server" Text='<%#Eval("NoJlhPot7")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <%--------------------------------------------------------------------------------------------------------------%>
                                    <%-- KET POTONGAN--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetPot1" CssClass="" runat="server" Text='<%#Eval("KetPot1")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetPot2" CssClass="" runat="server" Text='<%#Eval("KetPot2")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetPot3" CssClass="" runat="server" Text='<%#Eval("KetPot3")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetPot4" CssClass="" runat="server" Text='<%#Eval("KetPot4")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetPot5" CssClass="" runat="server" Text='<%#Eval("KetPot5")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetPot6" CssClass="" runat="server" Text='<%#Eval("KetPot6")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField> 
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblKetPot7" CssClass="" runat="server" Text='<%#Eval("KetPot7")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <%--------------------------------------------------------------------------------------------------------------%>
                                    <%--10--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTMknBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTrpBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTLembur")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTMknLbr")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTrpLbr")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTObat")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTHR")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTPphPrs")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTJKKPrs")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTHTPrs")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  

                                    <%--20--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTJKMPrs")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTIPPrs")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPMknBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPTrpBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPMknLbr")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPTrpLbr")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPObat")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPPisb")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPTHTKry")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPJKMKry")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  

                                    <%--30--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPIPKry")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPinjaman")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhAngsuran")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPditerima")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhDibayar")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoSelisihBln")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhGajiTerim")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhGajiKp")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhGajiBruto")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhBJabatan")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  

                                    <%--40--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhGajiNet")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhGajiNetTh")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPkpThn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPPPhKryTh")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPPphBKry")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPPphGab")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPPphKry")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhGBThn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhBJThn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhPtkpThn")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  

                                    <%--45--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTrpBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTrpBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTrpBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTrpBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoJlhTMknBiasa" CssClass="" runat="server" Text='<%#Eval("NoJlhTTrpBiasa")%>'></asp:Label>                           
                                        </ItemTemplate>                            
                                    </asp:TemplateField>  


                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                                              <div class="row clearfix">
                                                <div class="col-md-6">
                                                    <p>
                                                        <b>Kode Jurnal</b>
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                             <asp:TextBox CssClass="form-control" ID="txtKodeJurnal"  MaxLength="6" runat="server"  parsley-trigger="change" required="required" data-parsley-maxlength="1"  ></asp:TextBox>
                                                      </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row clearfix">                                         
                                                <div class="col-md-6">
                                                    <p>
                                                        <b>Keterangan Jurnal</b>
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox ID="txtKetJurnal" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                             </div>  
                                        </div>
                                    </div>
                                <div class="row clearfix">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class ="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="">
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Debit GL</p>
                                                        <asp:DropDownList ID="ddlDebitGL" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400"  OnSelectedIndexChanged="DebitGL_OnSelectedIndexChanged"></asp:DropDownList>                         
                                                        <div>
                                                            <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit GL</p>
                                                        <asp:DropDownList ID="ddlKreditGL" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged="KreditGL_OnSelectedIndexChanged"></asp:DropDownList>
                                                        </div> 
                                                    </div> 
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="">
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Debit CC</p>
                                                            <asp:DropDownList ID="ddlDebitCC" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList>
                                                    </div> 
                                                    <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Kredit CC</p>
                                                        <asp:DropDownList ID="ddlKreditCC" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" ></asp:DropDownList>
                                                    </div> 
                                                </div> 
                                            </div> 
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-2">
                                                <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                                <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" />
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
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  
    
    <!-- Parsley js -->
    <script type="text/javascript" src="../../../plugins/parsleyjs/parsley.min.js"></script>

    <script src="../../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="../../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="../../../plugins/sweet-alert/sweetalert2.min.js"></script>
    <!-- Tooltipster js -->
<script src="../../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../../plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="../../../assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="../../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>



</html>


