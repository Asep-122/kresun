<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KodeCurrency.aspx.cs" Inherits="GS_Kode_Currency" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Kode Currency</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />

    <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/assets/js/modernizr.min.js"></script>

    <link href="/assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Modal -->
    <link href="/plugins/custombox/css/custombox.min.css" rel="stylesheet" />

    <!-- Toastr css -->
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="/assets/js/modernizr.min.js"></script>

    <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- Responsive datatable examples -->
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>
    <script src="/Javascript/jquery-ui.js"></script>

    <!-- Modal-Effect -->
    <script src="/plugins/custombox/js/custombox.min.js"></script>
    <script src="/plugins/custombox/js/legacy.min.js"></script>
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
    
   
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

        function redirect() {
            location.href = 'KodeCurrency.aspx ';
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'success',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }
        function WarningAlert(errMsg, error) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        text: error,
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 4000
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
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd,#lnkGR').tooltipster({
                animation: 'grow'
            });


            //$('#txttglAwal').datepicker({
            //    format: 'dd/MM/yyyy',
            //    autoclose: true,
            //    todayHighlight: true
            //});
            //$('#txttglAkhir').datepicker({
            //    format: 'dd/MM/yyyy',
            //    autoclose: true,
            //    todayHighlight: true
            //});

            $('.selectpicker').selectpicker();

            
            
        }
        $(function () {
            //var table = $('#GRData').dataTable({
            //    "order": [],
            //    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
            //    { responsivePriority: 2, targets: -1 }],
            //    ordering: [[0, 'asc']],
            //    "scrollX": true,
            //    retrieve: true
            //});
            //var table = $('#GRData2').dataTable({
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
                                    <li class="breadcrumb-item"><a href="#">General Setup</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="KodeCurrency.aspx">Kode Currency</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Master Kode Currency</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="false"  runat="server">
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
               <asp:Panel ID="pnlHeader" runat="server" >
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Currency" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
       <asp:Panel ID="pnldepan" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">
                            <h4 class="m-t-0 header-title">Data Currency</h4>                           
                                     <div class="form-row center-page">
                                  <div class="form-group row center-page" style="width: 100%;">
                                     <asp:GridView ID="GRData2" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="887px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Kode Crc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkodecrc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCrc")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Crc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamacrc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCrc")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Symbol">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsymbol" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Symbol")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="List Data">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditCrc" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Tambah Data" runat="server" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkGR" runat="server" data-toggle="tooltip" data-placement="top" class="tooltips" data-original-title="Preview" CommandName="GRUPPREVIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-group"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
            <asp:Panel ID="pnladd" runat="server" Visible="false">
              <div class="row clearfix">
                  <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 center-page"> 
                     <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Data</div>
                         <br />
                               <div class="title text-center mb-3">
                                    <h2>Master Kode Currency</h2>
                               </div>
                         <br />
                       <div class="form-row">
                         <div class="form-group  col-md-6">
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                    <asp:label ID="Label1" runat="server" class="col-md-4 col-form-label font-weight-bold">Kode</asp:label>
                                   <div class="col-4">
					                <asp:TextBox CssClass="form-control" ID="txtkodecrc"  MaxLength="3" runat="server" required="required" Enabled="false"></asp:TextBox>
                                   </div>
                                </div>
                            </div>
				           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                   <asp:label ID="Label2" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama</asp:label>
                                   <div class="col-6">
							            <asp:TextBox ID="txtnama" CssClass="form-control" required="required" runat="server" MaxLength="20"></asp:TextBox>
                                   </div>
                                </div>
                            </div>
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                <asp:label ID="Label3" runat="server" class="col-md-4 col-form-label font-weight-bold">Simbol</asp:label>
                                   <div class="col-6">
							            <asp:TextBox ID="txtsymbol" CssClass="form-control" required="required" runat="server" MaxLength="3"></asp:TextBox>
                                   </div>
                                </div>
                            </div>
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                <asp:label ID="Label4" runat="server" class="col-md-4 col-form-label font-weight-bold">Memo</asp:label>
                                   <div class="col-8">
                                     <asp:TextBox ID="txtmemo" CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50"></asp:TextBox>
                                   </div>
                                </div>
                            </div>           
                            <%--<div class="form-group row">--%>
                                <div class="form-group col-md-12 font-weight-bold">
                                 <asp:CheckBox id="chkaktif" Text="&nbsp; Non Aktif" Checked="true" runat="server" repeatdirection="Horizontal" CssClass="font-weight-bold" AutoPostBack="true" CellPadding="10" CellSpacing="2">
                                    <%--<asp:ListItem   Value="true"/>--%>
                                 </asp:CheckBox>
                                </div>
                            <%--</div>--%>
                          </div>
                           <div class="form-group  col-md-6" >
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                <asp:label ID="Label7" runat="server" class="col-md-4 col-form-label font-weight-bold">Tanggal Awal</asp:label>
                                   <div class="col-6">
                                       <asp:TextBox ID="txttglAwal"  CssClass="form-control" runat="server" data-style="btn-custom" Type="date"  OnTextChanged="OnChange_TglAwal" AutoPostBack="true"></asp:TextBox> 
                                   </div>
                                </div>
                            </div>
                           <div class="form-group ">
                               <div class="form-check-inline col-md-12">
                                <asp:label ID="Label6" runat="server" class="col-md-4 col-form-label font-weight-bold">Tanggal Akhir</asp:label>
                                   <div class="col-6">
							        <asp:TextBox ID="txttglAkhir"  CssClass="form-control" runat="server" data-style="btn-custom" Type="date" OnTextChanged="OnChange_TglAkhir" AutoPostBack="true"></asp:TextBox>
                                   </div>
                                </div>
                            </div>
                           <div class="form-group ">
                               <div class="form-check-inline col-md-12">
                                <asp:label ID="Label5" runat="server" class="col-md-4 col-form-label font-weight-bold">Kurs Std</asp:label>
                                   <div class="col-5">
							        <asp:TextBox ID="txtkursstd"  CssClass="form-control" runat="server" data-style="btn-custom" Type="number" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                                   </div>
                                </div>
                            </div>
                           <div class="form-group ">
                               <div class="form-check-inline col-md-12">
                                <asp:label ID="Label8" runat="server" class="col-md-4 col-form-label font-weight-bold">Kurs Pajak</asp:label>
                                   <div class="col-5">
							        <asp:TextBox ID="txtkurspajak"  CssClass="form-control" runat="server" data-style="btn-custom" Type="number"  AutoPostBack="true" MaxLength="10"></asp:TextBox>
                                   </div>
                                </div>
                            </div>
                         </div>
                           
                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row justify-content-center">
                        <div class="btn-group mb-2">
                            <div class="button-list">
                                <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click" OnClientClick="btnSimpan_Click();"  />
                                &nbsp&nbsp&nbsp&nbsp&nbsp
                                <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back"  OnClick="btnCancel_Click"  OnClientClick="redirect();"/> 
                            </div>
                        </div>
                  </div>
                           </div>
                        </div>
                         
					   </div>
                       </div>
               
            </asp:Panel>
                 <%-- <asp:Panel ID="PnlFront" runat="server" Visible="false">
                    <div class="row">
                    <div class="col-md-10 center-page">
                     <div class="card-box ribbon-box center-page">
                           <div class="ribbon ribbon-custom">Periode</div>   
                         <br />
                         <div class="title text-center mb-3">
                                    <h2>Setting Data Kurs</h2>
                               </div>
                         <br />
                                     <div class="form-row center-page">
                                         
                                  <div class="form-row center-page justify-content-center" style="width: 100%;">
                                     
                                                          
                                </div>
                            </div> 

                        </div>
            </div>
                    </div>
        </asp:Panel>--%>
          <asp:LinkButton ID="lnkopenGroup" runat="server" data-toggle="modal" data-target="#modalGroup"></asp:LinkButton>
                    <div id="modalGroup" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" data-backdrop="static" data-keyboard="false" aria-hidden="true" style="display: none;">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnclose" runat="server" onclick="redirect()">×</button>
                                    <h4 class="modal-title" id="myLargeModalLabel">Detail Data Currency</h4>
                                </div>
                                <div class="modal-body">
                                    <!-- Form row -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <div class="form-group">
                                                                    <label class="col-form-label">Kode Currency</label>
                                                                    <asp:TextBox ID="lblkodecurrency" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <div class="form-group">
                                                                    <label class="col-form-label">Nama Currency</label>
                                                                    <asp:TextBox ID="lblnamacurrency" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <div class="text-center m-t-30">
                                                                    <asp:Button runat="server" ID="lnkKeluar" CssClass="btn btn-icon waves-effect waves-light btn-purple" title="Keluar" Text="Exit Preview"  OnClick="lnkKeluar_Click" ></asp:Button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                            <div class="form-group" style="width: 100%;">
                                                                <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap justify-content-center"  ShowHeaderWhenEmpty="True"   AutoGenerateColumns="false"  >
                                    <Columns>
                                        <asp:TemplateField  HeaderText="Tanggal Awal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblambilperiode" runat="server" Width="100px" CssClass="text-center" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "TglAwal", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanggal Akhir">
                                            <ItemTemplate>
                                                <asp:Label ID="lblambilperiode" runat="server" Width="100px" CssClass="text-center" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "TglAkhir", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kurs Std">
                                            <ItemTemplate>
                                                <asp:Label ID="lblambilperiode" runat="server" Width="100px" CssClass="text-center" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "KursStd", "{0:N2}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kurs Pajak">
                                            <ItemTemplate>
                                                <asp:Label ID="lblambilperiode" runat="server" Width="100px" CssClass="text-center" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "KursPajak", "{0:N2}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Kurs Pajak">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblkurspajak" runat="server" Width="120px" CssClass="text-center" MinValue="0" Type="number"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                           <HeaderStyle CssClass="align-center"></HeaderStyle>
                                           <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                           <ItemTemplate>
                                               <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SETPERIODE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                             </ItemTemplate>
                                      </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
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
           </div>    
            
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="GRdata2" />
                <asp:PostBackTrigger ControlID="GRdata" />
                <asp:PostBackTrigger ControlID="lnkKeluar" />
               <%-- <asp:PostBackTrigger ControlID="lnkEditCrc" />--%>
                <asp:PostBackTrigger ControlID="btnCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  <script>
      function btnSimpan_Click() {
    // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
          //var defaultAction = $(ctl).prop("href");
          var number = document.getElementById("txtkodecrc").value;
    // CANCEL DEFAULT LINK BEHAVIOUR
    //event.preventDefault();
    swal({
        
                     title: "Data berhasil disimpan",
                     text: 'Kode Curreny anda :' + number ,
        type: "success",
        position: "top-end",
               //   icon: "success",
                  showConfirmButton: false,
                  timer: 1500
        //showCancelButton: true,
        //confirmButtonColor: "#DD6B55",
        //confirmButtonText: "Yes, delete it!",
        //cancelButtonText: "No, cancel plx!",
        //closeOnConfirm: false,
        //closeOnCancel: false
    },
    function () {
      // if (isConfirm) {
        //    swal({ title: "Deleted!", text: "Your imaginary file has been deleted.", type: "success", confirmButtonText: "OK!", closeOnConfirm: false },
           // function (isConfirm) {
                // RESUME THE DEFAULT LINK ACTION
               // window.location.href = defaultAction;
                return true;
            //});
       // } else {
        //    swal("Cancelled", "Your imaginary file is safe :)", "error");
         //   return false;
       // }
    });
}
      //function myFunction(ctl, event) {
      //            Swal.fire({
      //              position: "top-end",
      //              icon: "success",
      //                title: "Data berhasil disimpan",
      //                text: 'Nomer nasabah anda :' + document.getElementById("#txtjenisgol").value(),
      //              showConfirmButton: false,
      //              timer: 1500
      //            });
      //          }
      </script>
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