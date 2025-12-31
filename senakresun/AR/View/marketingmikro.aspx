<%@ Page Language="C#" AutoEventWireup="true" CodeFile="marketingmikro.aspx.cs" Inherits="Report_marketingmikro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>DATA MARKETING MIKRO</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

        <!-- App icon -->
        <link rel="shortcut icon" href="../../assets/images/icon.ico"/>



 <!-- Plugins css-->
            <link rel="stylesheet" href="../../plugins/tooltipster/tooltipster.bundle.min.css" />
            <link href="../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
            <link href="../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />

            <!-- Plugins css-->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />


    <link href="../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../plugins/datatables/select.bootstrap4.min.css" rel="stylesheet" type="text/css" />



      <link href="../../plugins/bootstrap-tagsinput/css/bootstrap-tagsinput.css" rel="stylesheet" />
        <link href="../../plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" href="../../plugins/switchery/switchery.min.css" />
    

    <!-- App css -->
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../assets/js/modernizr.min.js"></script>
        <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/waves.js"></script>
    <script src="../../assets/js/jquery.slimscroll.js"></script>

  <!-- jQuery  -->
    <script src="../../Javascript/jquery-1.12.0.min.js"></script>
   
    <%--<script src="../assets/js/bootstrap.min.js"></script>--%>
  

    <script src="../../Javascript/jquery-ui.js"></script>
    <style type="text/css">
       .modalPopup
        {           
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
               .xcenter
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 160px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .ui-autocomplete {
            max-height: 500px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */
        * html .ui-autocomplete {
            height: 500px;
        }
        .ui-autocomplete-loading {
            background: white url("../../assets/images/Cube-1s-30px.gif") right center no-repeat;
        }

    </style>


  <script type="text/javascript">

      
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }


        function SweetAlert(errMsg) {

            setTimeout(function () {

                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                ).then(function () {
                    }).catch(swal.noop)
            }, 100);

        }

        $(document).ready(InIEvent);
      function InIEvent() {
              $('#datatable').DataTable();

                //Buttons examples
                var table = $('#Grdtagihan').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
          });
                       
             var table2 = $('#Grdtagihanmrk').DataTable({
                    lengthChange: false,
                    buttons: ['copy','excel', 'pdf']
          });
                          var table3 = $('#Grdtunggakan').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
          });
                          var table4 = $('#Grdtunggakanmrk').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
          });
                          var table5 = $('#Grdterbayar').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
          });
                          var table6 = $('#Grdterbayarmrk').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
                });
                //var table = $('#GRData').DataTable({
                //    lengthChange: false,
                //    buttons: ['copy', 'excel', 'pdf']
                //});
                // Key Tables

                $('#key-table').DataTable({
                    keys: true
                });

                // Responsive Datatable
                $('#responsive-datatable').DataTable();

                // Multi Selection Datatable
                $('#selection-datatable').DataTable({
                    select: {
                        style: 'multi'
                    }
          });

                   table.buttons().container()
              .appendTo('#Grdtagihan_wrapper .col-md-6:eq(0)');

                             table2.buttons().container()
              .appendTo('#Grdtagihanmrk_wrapper .col-md-6:eq(0)');

                             table3.buttons().container()
              .appendTo('#Grdtunggakan_wrapper .col-md-6:eq(0)');

                             table4.buttons().container()
              .appendTo('#Grdtunggakanmrk_wrapper .col-md-6:eq(0)');
                             table5.buttons().container()
              .appendTo('#Grdterbayar_wrapper .col-md-6:eq(0)');

                             table6.buttons().container()
              .appendTo('#Grdterbayarmrk_wrapper .col-md-6:eq(0)');

            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd, #lnkCari, #lnkSaveGR,#btnsearch').tooltipster({
                animation: 'grow'
            });
                $('.selectpicker').selectpicker();
            document.onclick = function (event) {
                // Compensate for IE<9's non-standard event model
                //
                if (event === undefined) event = window.event;
                var target = 'target' in event ? event.target : event.srcElement;
                // alert(event.clientY);
                if (target.getAttribute('tabindex') == '0') {
                    window.top.cekResize();

                    var x = event.clientX, y = event.clientY;
                    var papaWindow = parent.window;
                    setTimeout(function (e) {
                        papaWindow.scrollBy(0, y / 1.25);
                    }, 50);

                }
            };
                        $('#txtTglL').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
          });
                           $('#txtTglS').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });

        

            $(function () {
                var table = $('#Grdtagihan').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



          });
          $(function () {
                var table = $('#Grdtagihanmrk').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



          });
                $(function () {
                var table = $('#Grdtunggakan').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



          });
          $(function () {
                var table = $('#Grdtunggakanmrk').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



            });


             $(function () {
                var table = $('#Grdterbayar').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



          });
          $(function () {
                var table = $('#Grdterbayarnmrk').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
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


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
        //Shows the modal popup - the update progress
        //alert("1");
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
        popup.show();
        }
        }
        function EndRequestHandler(sender, args) {
        //Hide the modal popup - the update progress
        //alert("2");
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
        popup.hide();
        }
        }
</script>   
      
<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
<div class="xcenter">
<asp:Image ID="Image1" ImageUrl="../../assets/images/loadx.gif" AlternateText="Processing" runat="server" Width="136px"/>
</div>
</ProgressTemplate>
</asp:UpdateProgress>
<ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container-fluid">



                      <asp:Panel ID="pnlData" runat="server">
                          <h4 class="text-uppercase m-b-5 font-13 font-600"> <asp:Label ID="Lbltaspen" runat="server" Text="DATA MARKETING MIKRO / TAGIHAN"></asp:Label></h4>
                        <div class="text-center mt-4 mb-4">
                            <div class="row">
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box widget-flat border-custom bg-custom text-white">
                                        <i class="fi-tag"></i>
                                        <h3 class="m-b-10"></h3>
                                       <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A1"  href="#" runat="server" onserverclick="btntagihan_Click" >TAGIHAN</a></p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-primary widget-flat border-primary text-white">
                                        <i class="fi-archive"></i>
                                        <h3 class="m-b-10"></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A2"  href="#" runat="server" onserverclick="btntunggakan_Click" >TUNGGAKAN</a></p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-warning widget-flat border-success bg-success text-white">
                                        <i class="fi-help"></i>
                                        <h3 class="m-b-10"></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A4"  href="#" runat="server" onserverclick="btnterbayar_Click">TERBAYAR</a></p>
                                    </div>
                                </div>
                               <%-- <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-warning widget-flat border-success bg-success text-white">
                                        <i class="fi-help"></i>
                                        <h3 class="m-b-10"></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A3"  href="#" runat="server" onserverclick="btnlisttagihan_Click">LIST TAGIHAN</a></p>
                                    </div>
                                </div>--%>
                                
                            </div>
                           
                        </div>
                    </asp:Panel>



                    

               <asp:Panel ID="Pnlsearch" runat="server">
                        <div class="row">
                                              <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="text-uppercase m-b-5 font-13 font-600">Search By</label>
                                                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="2000" DynamicLayout ="true">
                                                                            <ProgressTemplate><center></center></ProgressTemplate>
                                                                   </asp:UpdateProgress>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Periode</label>
                                                                        <div class="col-9">
                                                                            <%--<asp:DropDownList ID="ddlwilayah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlcabang_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                              <div class="input-group">
                                                                                <asp:TextBox CssClass="form-control" ID="txtTglL" TextMode="DateTime" runat="server"></asp:TextBox>
                                                                                <div class="input-group-append">
                                                                                <span class="input-group-text"><i class="mdi mdi-calendar"></i></span>
                                                                                </div>
                                                                               </div> 
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">to</label>
                                                                        <div class="col-9">
                                                                            <%--<asp:DropDownList ID="ddlwilayah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlcabang_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                              <div class="input-group">
                                                                                <asp:TextBox CssClass="form-control" ID="txtTglS" TextMode="DateTime" runat="server"></asp:TextBox>
                                                                                <div class="input-group-append">
                                                                                <span class="input-group-text"><i class="mdi mdi-calendar"></i></span>
                                                                                </div>
                                                                               </div> 
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">PRODUK</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlproduk" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">CABANG</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlcabang_SelectedIndexChanged" ></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">MARKETING</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlmarketing" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">View Data</label>
                                                                        <div class="col-9">
                                                                        <asp:DropDownList ID="ddldata" runat="server" CssClass="form-control" data-style="btn-custom">
                                                                        <asp:ListItem Value="1">View Report</asp:ListItem>
                                                                        <asp:ListItem Value="2">Export Word</asp:ListItem>
                                                                        <asp:ListItem Value="3">Export PDF</asp:ListItem>
                                                                        <asp:ListItem Value="4">Export Excel</asp:ListItem>
                                                                        <asp:ListItem Value="5">Export CSV</asp:ListItem>
                                                                        </asp:DropDownList>
                                                       
                                                                        </div>
                                                                    </div>

                                                                   <div class="form-group row">
                                                                        <div class="col-3">
                                                                            <button type="button" id="btnsearch" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="lnkCari_Click">Search Rekap</button>
                                                                                                                       
                                                                        </div>
                                                                       <div class="col-3">
                                                                            <button type="button" id="Button1" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="lnkCari_Clickx">Search Detail</button>
                                                                                                                       
                                                                        </div>
                                                                      
                                                                    </div>
                                                                  
                                                            </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                        </div>
                    </asp:Panel>

    

        <asp:Panel ID="Pnltagihan" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA TAGIHAN</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">

                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdtagihan" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"
                                                                      AutoGenerateColumns="False" ShowFooter="True" onrowdatabound="Grdtagihan_RowDataBound" AllowSorting="True">
                                                                <Columns>
                                                                  <%--          <asp:TemplateField HeaderText="No">
                                                                            <EditItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                           
                                                                            </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="No.Urut">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  --%>
                                                                    <asp:TemplateField HeaderText="Marketing">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ao")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                                </FooterTemplate>
                                                                    </asp:TemplateField> 
                                                           <%--         <asp:TemplateField HeaderText="Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>           --%>                                                         
                                                                    <asp:TemplateField HeaderText="Nsb">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljlhnasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jumlah_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Plafon">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid2"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "plafon","{0:N2}")%>'></asp:Label>
                                                                        
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lblplafon" runat="server"  />
                                                                    </FooterTemplate>
                                                                  </asp:TemplateField>                                                                    
                                                                   <asp:TemplateField HeaderText="Baki Debet">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                       <FooterTemplate>
                                                                    <asp:Label ID="lblbaki" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Tung.pokok">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_pokok","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                       <FooterTemplate>
                                                                    <asp:Label ID="lbltungpokok" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Tung.Bunga">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_bunga","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lbltungbunga" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="T.Pok.jthtmp">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_pokok_jt","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     <FooterTemplate>
                                                                    <asp:Label ID="lbltungpokokjthtempo" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Cadangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lblcadangan" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rasio">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rasio","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblrasio" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>  
                                                                                                                       
                                                            </Columns>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>


        <asp:Panel ID="Pnltagihanmrk" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA TAGIHAN / Per Marketing</label>
                                                    
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdtagihanmrk" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" ShowFooter="True" onrowdatabound="Grdtagihanmrk_RowDataBound">
                                                                <Columns>
                                                               <%--    <asp:TemplateField HeaderText="No.Urut">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  --%>

                                                                    <asp:TemplateField HeaderText="No.Rekening">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                                </FooterTemplate>
                                                                    </asp:TemplateField>                                                                    
                                                                    <asp:TemplateField HeaderText="Nama Anggota">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Hari">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "hari_tagih")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Plafon">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lblplafon" runat="server" />
                                                                    </FooterTemplate>
                                                                  </asp:TemplateField>                                                                    
                                                                   <asp:TemplateField HeaderText="Baki Debet">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lblbaki" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Tunggakan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunggakan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Terbayar">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "terbayar","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Tung.Pokok">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_pokok","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                       <FooterTemplate>
                                                                    <asp:Label ID="lbltungpokok" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Tung.Bunga">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_bunga","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lbltungbunga" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total.Tung">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "total_tung","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lbltotaltung" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>  
                                                                    <asp:TemplateField HeaderText="Cadangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                    <asp:TemplateField HeaderText="Tgl.Jth.Tempo">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_due")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                            </Columns>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>

        <asp:Panel ID="Pnltunggakan" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA TUNGGAKAN</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdtunggakan" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" ShowFooter="True" onrowdatabound="Grdtunggakan_RowDataBound">
                                                                <Columns>
                                                             <%--      <asp:TemplateField HeaderText="No.Urut">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  --%>

                                                                    <asp:TemplateField HeaderText="Marketing">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ao")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                                </FooterTemplate>
                                                                    </asp:TemplateField>  
                                                                     <asp:TemplateField HeaderText="Status WO">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid201" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  
                                                           <%--        <asp:TemplateField HeaderText="Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> --%>
                                                                    
                                                                    <asp:TemplateField HeaderText="Nasabah">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jumlah_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Plafon">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lblplafon" runat="server" />
                                                                    </FooterTemplate>
                                                                  </asp:TemplateField>                                                                    
                                                                   <asp:TemplateField HeaderText="Saldo Akhir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lblbaki" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Tung.pokok">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_pokok","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbltungpokok" runat="server" />
                                                                    </FooterTemplate>

                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Tung.Bunga">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_bunga","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lbltungbunga" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Tung.PokokJthTempo">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_pokok_jt","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                      <FooterTemplate>
                                                                    <asp:Label ID="lbltungpokokjthtempo" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rasio">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rasio","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    <FooterTemplate>
                                                                    <asp:Label ID="lblrasio" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Cadangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lblcadangan" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                                                                                                                                                                                                                  
                                                            </Columns>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>
      <asp:Panel ID="Pnltunggakanmrk" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA TUNGGAKAN / Per Marketing</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdtunggakanmrk" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" ShowFooter="True" onrowdatabound="Grdtunggakanmrk_RowDataBound">
                                                                <Columns>
                                                                 <%--  <asp:TemplateField HeaderText="No.Urut">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  --%>

                                                                    <asp:TemplateField HeaderText="No.Rekening">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                                </FooterTemplate>
                                                                    </asp:TemplateField>  
                                                                    <asp:TemplateField HeaderText="Status WO">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid201" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  
                                                                    <asp:TemplateField HeaderText="Nama Nasabah">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                    
                                                                    <asp:TemplateField HeaderText="Plafon">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                           <FooterTemplate>
                                                                    <asp:Label ID="lblplafon" runat="server" />
                                                                    </FooterTemplate>
                                                                  </asp:TemplateField>                                                                    
                                                                   <asp:TemplateField HeaderText="Saldo Akhir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                       <FooterTemplate>
                                                                    <asp:Label ID="lblbaki" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="Terbayar">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "terbayar","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Tunggakan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunggakan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                              
                                                                 <asp:TemplateField HeaderText="Tung.Pokok">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_pokok","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lbltungpokok" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Tung.Bunga">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tung_bunga","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lbltungbunga" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Rasio">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rasio","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lblrasio" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField> 
                                                                    <asp:TemplateField HeaderText="cadangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lblcadangan" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>     
                                                                    <asp:TemplateField HeaderText="Tgl.Jth.Tempo">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_due")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>   
                                                            </Columns>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>

                       <asp:Panel ID="Pnlterbayar" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA TERBAYAR</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdterbayar" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" ShowFooter="True" onrowdatabound="Grdterbayar_RowDataBound">
                                                                <Columns>
                                                                  <%-- <asp:TemplateField HeaderText="No.Urut">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> --%> 

                                                                    <asp:TemplateField HeaderText="Marketing">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "marketing")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                                </FooterTemplate>
                                                                    </asp:TemplateField>  
                                                         <%--           <asp:TemplateField HeaderText="Cabang">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_cabang")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> --%>
                                                                    <asp:TemplateField HeaderText="Nasabah">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jumlah_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Plafon">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lblplafon" runat="server" />
                                                                    </FooterTemplate>
                                                                  </asp:TemplateField>                                                                    
                                                                   <asp:TemplateField HeaderText="Saldo Akhir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lblbaki" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Pokok">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pokok","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                              <FooterTemplate>
                                                                            <asp:Label ID="lbltungpokok" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Bunga">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lbltungbunga" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Denda">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "denda","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                      <FooterTemplate>
                                                                    <asp:Label ID="lbldenda" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Total">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "total","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                    <asp:Label ID="lblstotal" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cadangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lblcadangan" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>                                                                                                                                                                                              
                                                            </Columns>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>
      <asp:Panel ID="Pnlterbayarmrk" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA TERBAYAR / Per Marketing</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdterbayarmrk" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" ShowFooter="True" onrowdatabound="Grdterbayarmrk_RowDataBound">
                                                                <Columns>
                                                                  <%-- <asp:TemplateField HeaderText="No.Urut">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor_urut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  --%>

                                                                    <asp:TemplateField HeaderText="No.Rekening">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblao" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                          <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                                </FooterTemplate>
                                                                    </asp:TemplateField>                                                                    
                                                                    <asp:TemplateField HeaderText="Nama Nasabah">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="Plafon">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                           <FooterTemplate>
                                                                    <asp:Label ID="lblplafon" runat="server" />
                                                                    </FooterTemplate>
                                                                  </asp:TemplateField>                                                                    
                                                                   <asp:TemplateField HeaderText="Saldo Akhir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lblbaki" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Angs_ke">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid81" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angs_ke")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tgl_Byr">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid812" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_angs","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Pokok">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pokok","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lbltungpokok" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Bunga">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bunga","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                            <FooterTemplate>
                                                                    <asp:Label ID="lbltungbunga" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                 
                                                                     <asp:TemplateField HeaderText="Total">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "total","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                         <FooterTemplate>
                                                                    <asp:Label ID="lblstotal" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cadangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lblcadangan" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>  
                                                                    <asp:TemplateField HeaderText="Denda">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "denda","{0:N2}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                    <asp:Label ID="lbldenda" runat="server" />
                                                                    </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Tgl_Byr_denda">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblempid8122" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_byr_denda","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                            </Columns>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>
            
         </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ddlcabang" />
                <asp:PostBackTrigger ControlID="btnsearch" />
                <asp:PostBackTrigger ControlID="Button1" />
                
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>
<!-- App js -->
<script src="../../assets/js/jquery.app.js"></script>
<!-- Toastr js -->
<script src="../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>
<!-- Sweet Alert Js  -->
<script src="../../plugins/sweet-alert/sweetalert2.min.js"></script>

<!-- Parsley js -->
<script type="text/javascript" src="../../plugins/parsleyjs/parsley.min.js"></script>
  <script src="../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

<!-- Tooltipster js -->
<script src="../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../plugins/datatables/responsive.bootstrap4.min.js"></script>  



    <script src="../../plugins/tablesaw/js/tablesaw.js"></script>
        <script src="../../plugins/tablesaw/js/tablesaw-init.js"></script>
    <script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
            <!-- Buttons examples -->
        <script src="../../plugins/datatables/dataTables.buttons.min.js"></script>
        <script src="../../plugins/datatables/buttons.bootstrap4.min.js"></script>
        <script src="../../plugins/datatables/jszip.min.js"></script>
        <script src="../../plugins/datatables/pdfmake.min.js"></script>
        <script src="../../plugins/datatables/vfs_fonts.js"></script>
        <script src="../../plugins/datatables/buttons.html5.min.js"></script>
        <script src="../../plugins/datatables/buttons.print.min.js"></script>
           <!-- Key Tables -->
        <script src="../../plugins/datatables/dataTables.keyTable.min.js"></script>

</html>

