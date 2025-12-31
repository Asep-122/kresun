<%@ Page Language="C#" EnableEventValidation="false" CodeBehind="MutasiCadangan.aspx.cs" AutoEventWireup="true" CodeFile="MutasiCadangan.aspx.cs" Inherits="AR_View_MutasiCandangan" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>View Mutasi Cadangan</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!-- App icon -->
    <link rel="shortcut icon" href="/assets/images/icon.ico" />

    <!-- Plugins css-->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/select.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <link href="/plugins/bootstrap-tagsinput/css/bootstrap-tagsinput.css" rel="stylesheet" />
    <link href="/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/plugins/switchery/switchery.min.css" />

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/assets/js/modernizr.min.js"></script>


    <link href="/assets/css/jquery-ui.css" rel="stylesheet" />
    <link href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>

    <script src="/Javascript/jquery-ui.js"></script>


   
   
    <script type="text/javascript">


        function checkTextField1(field) {
            if (field.value == '' || field.value == 0 || field.value == '.') {
                field.value = 0;
            }


            var nilai = field.value.split(',').join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split("."); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1").split("").reverse().join("");
                output = parts.join(".");
            }
            field.value = output;
        }


        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

        function Simpan() {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
                        type: "success",
                        position: "top-end",
                        //   icon: "success",
                        showConfirmButton: false,
                        timer: 2000
                        //title: errMsg,
                        //confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).catch(swal.noop)
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



        $(document).ready(InIEvent);
        function InIEvent() {

            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });
            $('#txttgltagih').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtStart').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtEnd').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
			    var table = $('#GRData').DataTable({
                "order": [], "paging": false,
                buttons: ['copy', 'excel', 'pdf'],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }]
            });

            $("#txtno_anggota").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "MutasiCadangan.aspx/GetNoNasabah",
                        data: "{'InputText':'" + document.getElementById('txtno_anggota').value + "','SessionBranchID':'<%=SessBranchID%>'}",
                                dataType: "json",
                                success: function (data) {
                                    obj = JSON.parse(stringToArray(data.d));
                                    console.log(obj);
                                    var found = $.map(obj, function (item) {
                                        return {
                                            label: item[0],
                                            hdnZipCode: item[1]
                                        }
                                    });

                                    $("[id$=hdnZipCode]").val('');

                                    response(found);
                                },

                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    alert("masuk1");
                                },
                                failure: function (response) {
                                    alert("masuk 2");
                                }
                            });
                },
                select: function (e, i) {
                    $("[id$=hdnZipCode]").val(i.item.hdnZipCode);
                },
                minLength: 3
            });

			table.buttons().container()
                .appendTo('#GRData_wrapper .col-md-6:eq(0)');
            $('.selectpicker').selectpicker();
            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }

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
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="MutasiCadangan.aspx">View Mutasi Cadangan</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Mutasi Cadangan</h4>
                        </div>
                    </div>
                </div>
                    <!-- Panel Category -->
                 <asp:Panel ID="pnlCategory" Visible="false" runat="server">
                     <div class="row">
                         <div class="col-lg-6">
                             <%--<div class="col-lg-12">--%>
                                 <div class="card-box ribbon-box">
                                     <div class="ribbon ribbon-custom">Category Report :</div>
                                     <p class="m-b-0">
                                         <div class="form-group row">
                                             <div class="form-group col-md-12">
                                                 <label for="inputCity" class="col-form-label">Choice Category Mutasi</label>
                                                 <asp:RadioButtonList ID="rdbtnjenis" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="Group1_CheckedChanged" AutoPostBack="true" CellPadding="10" CellSpacing="2">
                                                     <asp:ListItem Text="&nbsp;Berdasarkan Anggota" Value="0" />
                                                     <asp:ListItem Text="&nbsp;All Anggota" Value="1" />
                                                 </asp:RadioButtonList>
                                                 <asp:HiddenField ID="hdnZipCode" runat="server" />
                                                 <asp:HiddenField ID="hdnCabangCode" runat="server" />
                                             </div>
                                         </div>
                                 </div>
                             <%--</div>--%>
                         </div>
                     </div>
                 </asp:Panel>
                 <!-- Panel Filter -->
                 <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                     <div class="row">
                         <div class="col-lg-6">
                             <div class="card-box ribbon-box">
                                 <div class="ribbon ribbon-custom">Filter Anggota</div>
                                 <p class="m-b-0">
                                     <div class="row">
                                         <div class="col-md-12">
                                             <div class="card-box">
                                                 <div class="form-row">
                                                     <div class="form-row">
                                                         <div class="form-group col-md-4">
                                                             <label for="inputCity" class="col-form-label">Periode</label>
                                                             <asp:DropDownList ID="ddlbulan" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlbulan_SelectedIndexChanged">
                                                                 <asp:ListItem Value="01">Januari</asp:ListItem>
                                                                 <asp:ListItem Value="02">Februari</asp:ListItem>
                                                                 <asp:ListItem Value="03">March</asp:ListItem>
                                                                 <asp:ListItem Value="04">April</asp:ListItem>
                                                                 <asp:ListItem Value="05">May</asp:ListItem>
                                                                 <asp:ListItem Value="06">June</asp:ListItem>
                                                                 <asp:ListItem Value="07">July</asp:ListItem>
                                                                 <asp:ListItem Value="08">Agustus</asp:ListItem>
                                                                 <asp:ListItem Value="09">September</asp:ListItem>
                                                                 <asp:ListItem Value="10">October</asp:ListItem>
                                                                 <asp:ListItem Value="11">November</asp:ListItem>
                                                                 <asp:ListItem Value="12">December</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="form-group col-md-4">
                                                             <label for="inputState" class="col-form-label">This Month From</label>
                                                             <asp:TextBox ID="txtStart" CssClass="form-control" runat="server" TextMode="DateTime" OnTextChanged="txtdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                         </div>
                                                         <div class="form-group col-md-4">
                                                             <label for="inputZip" class="col-form-label">This Month To</label>
                                                             <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server" TextMode="DateTime" OnTextChanged="txtdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                         </div>
                                                     </div>
                                                     <div class="form-row">
                                                         <div class="form-group col-md-12" runat=server visible=false>
                                                             <label class="col-form-label">Kode Cabang</label>
                                                             <asp:TextBox CssClass="form-control" ID="txtkodecabang" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                         </div>
                                                         <div class="form-group col-md-12">
                                                             <label class="col-form-label">No Anggota</label>
                                                             <asp:TextBox CssClass="form-control" ID="txtno_anggota" Enabled="true" runat="server"></asp:TextBox>
                                                         </div>
                                                          <%-- <div class="form-group col-md-12">
                                                             <div class="form-group">
                                                                 <label for="inputState" class="col-form-label">Type Cadangan</label>
                                                                 <asp:DropDownList CssClass="form-control" ID="ddltype" Enabled="true" runat="server" required="required" MaxLength="200">
                                                                     <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                                                     <asp:ListItem Value="U">Masuk</asp:ListItem>
                                                                     <asp:ListItem Value="T">Keluar</asp:ListItem>
                                                                 </asp:DropDownList>
                                                             </div>
                                                             <div class="form-group">
                                                               <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                             </div>
                                                         </div>--%>
                                                     </div>
                                                 </div>
                                             </div>
                                         </div>
                                     </div>
                             </div>
                         </div>
                         <div class="col-lg-6">
                             <div class="col-lg-12">
                                 <div class="card-box ribbon-box">
                                     <div class="ribbon ribbon-custom">Setting</div>
                                     <p class="text-muted font-14 m-b-30"></p>
                                     <div class="form-group">
                                         <label for="inputpid" class="col-form-label">Report Periode</label>
                                         <asp:TextBox ID="Txtrptperiode" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>
                                         <asp:TextBox ID="txtrpttitle" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                     </div>
                                     <div class="form-group">
                                         <label for="inputpid" class="col-3 col-form-label">Type Cadangan</label>
                                         <asp:DropDownList ID="ddltype" CssClass="form-control" runat="server">
                                             <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                             <asp:ListItem Value="U">Masuk</asp:ListItem>
                                             <asp:ListItem Value="T">Keluar</asp:ListItem>
                                         </asp:DropDownList>
                                     </div>
                                 </div>
                             </div>
                         </div>
                     </div>
                 </asp:Panel>
                    <asp:Panel ID="Panel2" Visible="true" runat="server">
                        <div class="form-group">
                            <div class="button-list">
                                <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnCancel" class="btn btn-custom waves-light text-white" runat="server" Text="Reset" OnClick="btnCancel_Click" />
                             <%--<asp:Button ID="btnCancel" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />--%>
                            </div>
                        </div>
                    </asp:Panel>
              
                 <!-- Panel Front -->
                 <asp:Panel ID="PnlFront" runat="server" Visible="false">
                     <div class="row">
                         <div class="col-md-12">
                             <div class="card-box  ribbon-box">
                                 <div class="ribbon ribbon-custom">DATA MUTASI</div>
                                 <p class="m-b-0"></p>
                                    <div class="form-row">
                                        <div class="form-group col-md-3">
                                            <asp:Label ID="Label41" runat="server" class="col-form-label">Mutasi Debit</asp:Label>
                                            <asp:TextBox CssClass="form-control" ID="txtmutasidebit" Enabled="false" runat="server">0</asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <asp:Label ID="Label57" runat="server" class="col-form-label">Mutasi Kredit</asp:Label>
                                            <asp:TextBox CssClass="form-control" ID="txtmutasikredit" Enabled="false" runat="server">0</asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <asp:Label ID="Label60" runat="server" class="col-form-label">Saldo Akhir</asp:Label>
                                            <asp:TextBox CssClass="form-control" ID="txtsaldoakhir" Enabled="false" runat="server">0</asp:TextBox>
                                        </div>
                                    </div>
                                 <div class="form-row">
                                     <div class="form-group" style="width: 100%;">
                                         <asp:GridView ID="GRData" runat="server" ShowFooter="True" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowDataBound="GRData_RowDataBound" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="887px">
                                             <Columns>
                                                 <asp:TemplateField HeaderText="No" >
                                                     <ItemTemplate>
                                                         <%# Container.DataItemIndex + 1 %>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Tanggal Transaksi">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblNpwp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglTrn", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No Bukti">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Kode Ao">
                                                     <ItemTemplate>
                                                           <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Kodesales")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No Anggota">
                                                     <ItemTemplate>
                                                           <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nama Anggota">
                                                     <ItemTemplate>
                                                           <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Kode Cabang" Visible="false">
                                                     <ItemTemplate>
                                                           <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Keterangan">
                                                     <ItemTemplate>
                                                           <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Debit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lbldebit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "debit", "{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                        <FooterTemplate>
                                                            <span style="display: block; padding: 0; text-align: right">
                                                                <asp:Label ID="lbldebit" Font-Bold="true" runat="server" />
                                                            </span>
                                                        </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Kredit">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblkredit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kredit", "{0:N0}")%>'></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterTemplate>
                                                         <span style="display: block; padding: 0; text-align: right">
                                                             <asp:Label ID="lblkredit" Font-Bold="true" runat="server" />
                                                         </span>
                                                     </FooterTemplate>
                                                     <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                                     <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                 </asp:TemplateField>
                                             </Columns>
                                         </asp:GridView>
                                     </div>
                                 </div>

                             </div>
                         </div>
                     </div>
                     <div class="hidden-print mt-4">
                         <div class="float-left mb-2">
                             <%--<asp:Button ID="btnCancel" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />--%>
                         </div>
                     </div>
                 </asp:Panel>
                 <!-- Panel Edit -->
            </div> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>  
                <asp:PostBackTrigger ControlID="btnSearch" /> 
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  
  
<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="/plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>
<script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>
<script src="/plugins/tablesaw/js/tablesaw.js"></script>
<script src="/plugins/tablesaw/js/tablesaw-init.js"></script>
<script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<!-- Buttons examples -->
<script src="/plugins/datatables/dataTables.buttons.min.js"></script>
<script src="/plugins/datatables/buttons.bootstrap4.min.js"></script>
<script src="/plugins/datatables/jszip.min.js"></script>
<script src="/plugins/datatables/pdfmake.min.js"></script>
<script src="/plugins/datatables/vfs_fonts.js"></script>
<script src="/plugins/datatables/buttons.html5.min.js"></script>
<script src="/plugins/datatables/buttons.print.min.js"></script>
<!-- Key Tables -->
<script src="/plugins/datatables/dataTables.keyTable.min.js"></script>


</html>
