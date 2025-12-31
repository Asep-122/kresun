<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datafollowupmarket.aspx.cs" Inherits="Report_datafollowupmarket" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>DATA BAHAN TASPEN/ASABRI</title>
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
                      .overlay
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

         .auto-style1 {
             position: relative;
             width: 100%;
             min-height: 1px;
             top: -20px;
             left: 0px;
             -ms-flex: 0 0 75%;
             flex: 0 0 75%;
             max-width: 75%;
             padding-left: 15px;
             padding-right: 15px;
         }

    </style>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script> 
    
  <script type="text/javascript">  
       function initialize() {
var lat = document.getElementById('TextBox1').value;
var lon = document.getElementById('TextBox2').value;
var myLatlng = new google.maps.LatLng(lat, lon) // This is used to center the map to show our markers
var mapOptions = {
center: myLatlng,
zoom: 6,
mapTypeId: google.maps.MapTypeId.ROADMAP,
marker: true
};
var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
var marker = new google.maps.Marker({
position: myLatlng
});
marker.setMap(map);
}
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
    <%--  function showProgress() {
var updateProgress = $get("<%= UpdateProgress.ClientID %>");
updateProgress.style.display = "block";
}--%>

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
          //      var table = $('#Grdtagihan').DataTable({
          //          lengthChange: false,
          //          buttons: ['copy', 'excel', 'pdf']
          //});


          //                var table3 = $('#Grdtunggakan').DataTable({
          //          lengthChange: false,
          //          buttons: ['copy', 'excel', 'pdf']
          //});

          //                var table5 = $('#Grdterbayar').DataTable({
          //          lengthChange: false,
          //          buttons: ['copy', 'excel', 'pdf']
          //});

          var table6 = $('#grdFollowUpTaspen').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
          });
          
          var table7 = $('#grdAsabriFollowUp').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
          });

          var table5 = $('#GrdReport').DataTable({
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

              //     table.buttons().container()
              //.appendTo('#Grdtagihan_wrapper .col-md-6:eq(0)');

             
              //               table3.buttons().container()
              //.appendTo('#Grdtunggakan_wrapper .col-md-6:eq(0)');

           
                             table5.buttons().container()
              .appendTo('#GrdReport_wrapper .col-md-6:eq(0)');
          
                             table6.buttons().container()
              .appendTo('#grdFollowUpTaspen_wrapper .col-md-6:eq(0)');

                             table7.buttons().container()
              .appendTo('#grdAsabriFollowUp_wrapper .col-md-6:eq(0)');
            
          
            $("div").remove(".tooltip");
            $.getScript("../../assets/js/jquery.core.js");

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
             

           $(function () {
                var table = $('#GrdReport').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



          });

          $(function () {
                var table = $('#grdFollowUpTaspen').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true
                });



          });
          
          $(function () {
                var table = $('#grdAsabriFollowUp').dataTable({
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

</script>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="container-fluid">

                    <asp:Panel ID="pnlData" runat="server">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="card-box">
                                    <h4 class="text-uppercase m-b-5 font-13 font-600"> <asp:Label ID="Lbltaspen" runat="server" Text="REPORT DETAIL KUNJUNGAN"></asp:Label></h4>
                                        <div class="text-center mt-4 mb-4">
                                            <div class="row">
                                                                <div class="col-xs-6 col-sm-6">
                                                                    <div  class="card-box widget-flat border-custom bg-custom text-white">
                                                                        <i class="fi-tag"></i>
                                                                         <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A2"  href="#" runat="server" onserverclick="btndetail_Click"  >LAPORAN DETAIL KUNJUNGAN</a></p>
                                           
                                                                     </div>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-6">
                                                                    <div  class="card-box widget-flat border-custom bg-custom text-white">
                                                                        <i class="fi-help"></i>
                                                                            <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A3"  href="#" runat="server" onserverclick="btnrekap_Click">LAPORAN REKAP KUNJUNGAN</a></p>
                                                                    </div>
                                                                </div>
                             
                                            </div>
                                         </div>
                                    </div>
                            </div>
                        </div>
                    </asp:Panel>

               

                    <asp:Panel ID="Pnlsearch" runat="server">
                         <div class="card m-b-10 text-white btn-custom text-xs-center">
                        <div class="text-center mt-2 mb-2">
                                <asp:Label ID="Lbljudul"  runat="server" Text="REPORT DETAIL KUNJUNGAN"></asp:Label>
                        </div>
                        </div>
                        <div class="row">
                          
                            
                              <div class="col-lg-6">
                                                <div class="card-box">
                                                    <%--<div class="form-row">--%>
                                                        <%--<div class="form-group col-md-6">--%>
                                                            <%--<div class="form-group">--%>
                                                                <label class="text-uppercase m-b-5 font-13 font-600">Search By</label>
                                                                   <%--<form action="#">--%>
                                                                    
                                                                    <div class="form-group row">
                                                                         <label for="inputpid" class="col-3 col-form-label">Area SENA</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlProduk" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style=btn-custom></asp:DropDownList>
                                                                            <asp:DropDownList ID="ddlAreaSena" CssClass="selectpicker show-tick" runat="server" data-style=btn-custom AutoPostBack="true" OnSelectedIndexChanged="ddlAreaSena_SelectedIndexChanged"></asp:DropDownList>
                                                                            <asp:DropDownList ID="ddlCabangSena" Visible="false" CssClass="selectpicker show-tick" runat="server" data-style=btn-custom  AutoPostBack="true" ></asp:DropDownList>
                                                                        </div>
                                                                    </div>                                                                   
                                                                    
                                                                    <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Marketing</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlMarketing" CssClass="selectpicker show-tick" runat="server" data-style=btn-custom></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                    <label for="inputpid" class="col-3 col-form-label">Status</label>
                                                                        <div class="col-9">
                                                                    <asp:DropDownList ID="ddlstatusfollow" CssClass="selectpicker show-tick" runat="server" data-style=btn-custom >                                       
                                                                    <asp:ListItem Value="0">Belum dikunjungi</asp:ListItem>
                                                                    <asp:ListItem Value="1">Sudah dikunjungi</asp:ListItem>
                                                                    </asp:DropDownList>       
                                                                            </div>
                                                                    </div>
                                                                   <div class="form-group row">
                                                                        <div class="col-3">
                                                                              <asp:Button ID="btnSearchFollowUp" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="btnSearchFollowUp_Click" OnClientClick="showProgress()"/>   
                                                                            <%--<button type="button" id="btnSearchFollowUp" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="btnSearchFollowUp_Click">Search</button>--%>
                                                                        </div>
                                                                    </div>
                                                                      <%--</form>--%>
                                                            <%--</div>--%>
                                                    <%--</div>--%>
                                                    
                                                <%--</div>--%>
                                            </div>
                                        </div>

                              <div class="col-lg-6">

                        <div class="card-box">
                            <h4 class="header-title m-t-0">Filter Status Data yang sudah dikunjungi</h4>
                            <%--<form action="#">--%>
                                <div class="form-group row">
                                                                        <label for="inputpid" class="col-3 col-form-label">Bulan</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddlbulan" CssClass="selectpicker show-tick" runat="server" data-style=btn-custom>
                                                                                <asp:ListItem Value="01">Bulan January</asp:ListItem>
                                                                                <asp:ListItem Value="02">Bulan February</asp:ListItem>
                                                                                <asp:ListItem Value="03">Bulan March</asp:ListItem>
                                                                                <asp:ListItem Value="04">Bulan April</asp:ListItem>
                                                                                <asp:ListItem Value="05">Bulan May</asp:ListItem>
                                                                                <asp:ListItem Value="06">Bulan June</asp:ListItem>
                                                                                <asp:ListItem Value="07">Bulan July</asp:ListItem>
                                                                                <asp:ListItem Value="08">Bulan Agustus</asp:ListItem>
                                                                                <asp:ListItem Value="09">Bulan September</asp:ListItem>
                                                                                <asp:ListItem Value="10">Bulan October</asp:ListItem>
                                                                                <asp:ListItem Value="11">Bulan November</asp:ListItem>
                                                                                <asp:ListItem Value="12">Bulan December</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                         <label for="inputpid" class="col-3 col-form-label">Tahun</label>
                                                                        <div class="col-9">
                                                                            <asp:DropDownList ID="ddltahun" CssClass="selectpicker show-tick" runat="server" data-style=btn-custom>
                                                                                <asp:ListItem Value="2025">Tahun 2025</asp:ListItem>
                                                                                <asp:ListItem Value="2026">Tahun 2026</asp:ListItem>
                                                                                <asp:ListItem Value="2027">Tahun 2027</asp:ListItem>
																				 <asp:ListItem Value="2028">Tahun 2028</asp:ListItem>
																			
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>                                                                   

                                

                  <%--                  <div class="form-group text-right m-b-0">
                                        <button type="button" id="btnproses" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="btnproses_Click">Get DATA</button>

                                    </div>--%>

                            <%--</form>--%>
                        </div> <!-- end card-box -->
                    </div>
                        </div>
                    </asp:Panel>

    

        



                    <asp:Panel ID="Pnlrekap" Visible="false" runat="server">
                        
        <%--<asp:HiddenField ID="hdnNotas" runat="server" />--%>
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">REPORT REKAPITULASI AO</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="GrdReport" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"
                                                                      AutoGenerateColumns="False"  AllowSorting="True" >
                                                                <Columns>
                                                                           
                                                                 <asp:TemplateField HeaderText="NAMA AO">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamasales" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namasales")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="KETERANGAN">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keterangan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MINGGU I">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblminggu1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "minggu1")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="MINGGU II">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblminggu2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "minggu2")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="MINGGU III">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblminggu3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "minggu3")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="MINGGU IV">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblminggu4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "minggu4")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="MINGGU V">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblminggu5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "minggu5")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="REKAP">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblrekap" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "rekap")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>



                                                              
                                                                    
                                                        
                                                                 
                                                                </Columns>
                                                               
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>



        <asp:Panel ID="pnlFollowUpTaspen" Visible="false" runat="server">
                        
        <%--<asp:HiddenField ID="hdnNotas" runat="server" />--%>
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">REPORT DETAIL KUNJUNGAN</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="grdFollowUpTaspen" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"
                                                                      AutoGenerateColumns="False"  AllowSorting="True" OnRowCommand="grdFollowUpTaspen_RowCommand">
                                                                <Columns>
                                                                        <asp:TemplateField HeaderText="+">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                        <asp:LinkButton ID="LnkView2" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</asp:LinkButton> 
                                                                        <asp:Label ID="lblnouruttaspen" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                        <asp:Label ID="lbllat" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lat")%>'></asp:Label>
                                                                        <asp:Label ID="lbllon" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lon")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>  
                                                                    <asp:TemplateField HeaderText="Data">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldata" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "IDN")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Marketing">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmarketix" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namamarketing")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                              
                                                                    
                                                                    <asp:TemplateField HeaderText="Tgl.Getdata">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltglgetdatax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglgetdata","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                    
                                                                     
                                                                    
                                                                   <asp:TemplateField HeaderText="Notas">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotasx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notas")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>    
                                                                     <asp:TemplateField HeaderText="Tanggal Kunjungan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltglx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglkunjungan","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamaxsss" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Alamat">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalamatkunjunganx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamatkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No Hp">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltelponkunjunganx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "telponkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>      
                                                                 <asp:TemplateField HeaderText="Status SK OnHand">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblskonhandx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "skonhand")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>                                                        
                                                                 <asp:TemplateField HeaderText="Status SK To">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblskontox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "skonto")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                  <asp:TemplateField HeaderText="Tempat Tanggal Lahir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltgllahirkunjunganx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahirkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                 <asp:TemplateField HeaderText="Usia">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusiakunjunganx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usiakunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="Gaji">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgajikunjunganx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gajikunjungan","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>   
                                                                <asp:TemplateField HeaderText="Kantor Bayar">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkantorbayarkunjunganx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kantorbayarkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpinjamanlamax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pinjamanlama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Plafon Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblplafonlamax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafonlama","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="JW Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljangkalamax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkalama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Angsuran Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljangkalamasx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuranlama","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Keterangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblKeteranganx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notekunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Plafon Pinjaman Sena">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblplafonsenax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafonsena","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="JW Pinjaman Sena">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljangkasenax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkasena")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Angsuran Pinjaman Sena">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblangsuransenax" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuransena","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                            
                                                                <asp:TemplateField HeaderText="Kemampuan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkemampuanx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kemampuan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="keinginan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkeinginanx" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keinginan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Prospek">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblprospekxs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "prospek")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                 
                                                                </Columns>
                                                               
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>     

        <asp:Panel ID="pnlAsabriFollowUp" Visible="false" runat="server">
                        
        <%--<asp:HiddenField ID="hdnNoUrutAsabri" runat="server" />--%>
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">REPORT DETAIL KUNJUNGAN ASABRI</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="grdAsabriFollowUp" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"
                                                                      AutoGenerateColumns="False"  AllowSorting="True" OnRowCommand="grdAsabriFollowUp_RowCommand">
                                                          
                                                                <Columns>
                                                                      <asp:TemplateField HeaderText="+">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>    
                                                                        <asp:LinkButton ID="LnkView1" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</asp:LinkButton> 
                                                                        <asp:Label ID="lblnourutasabri" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                        
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>                                                                         
                                                                       
                                                                    <asp:TemplateField HeaderText="Marketing">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblmarketiasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namamarketing")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                              
                                                                      <asp:TemplateField HeaderText="Tgl.Getdata">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltglgetdataasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglgetdata","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                   
                                                                  
                                                                   <asp:TemplateField HeaderText="Notas">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotasasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopens")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                       
                                                                         <asp:TemplateField HeaderText="Tanggal Kunjungan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltglasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglkunjungan","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamaeasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "namakunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Alamat">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalamatkunjunganasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamatkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No Hp">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltelponkunjunganasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "telponkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>      
                                                                 <asp:TemplateField HeaderText="Status SK OnHand">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblskonhandasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "skonhand")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>                                                        
                                                                 <asp:TemplateField HeaderText="Status SK To">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblskontoasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "skonto")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                  <asp:TemplateField HeaderText="Tempat Tanggal Lahir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltgllahirkunjunganasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahirkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                 <asp:TemplateField HeaderText="Usia">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusiakunjunganasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usiakunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="Gaji">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgajikunjunganasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gajikunjungan","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>   
                                                                <asp:TemplateField HeaderText="Kantor Bayar">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkantorbayarkunjunganasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kantorbayarkunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpinjamanlamaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pinjamanlama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Plafon Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblplafonlamaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafonlama","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="JW Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljangkalamaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkalama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Angsuran Pinjaman Lama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblangsuranlamaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuranlama","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Keterangan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblKeteranganasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notekunjungan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Plafon Pinjaman Sena">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblplafonsenaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafonsena","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="JW Pinjaman Sena">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljangkasenaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangkasena")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Angsuran Pinjaman Sena">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblangsuransenaasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsuransena","{0:n}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                            
                                                                <asp:TemplateField HeaderText="Kemampuan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkemampuanasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kemampuan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="keinginan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkeinginanasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "keinginan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Prospek">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblprospekasabri" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "prospek")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                </asp:TemplateField>
                                                                                                                                                                                                                                                   
                                                            </Columns>
                                                                
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>

         
   <asp:Panel ID="pnlformattaspen" Visible="false" runat="server">
                    
                            
                        <asp:LinkButton ID="LinkButton4" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembalitaspen_Click" />        
                                       <div class="ribbon ribbon-custom"></div>
                            <p class="text-muted font-14 m-b-30">
                            </p>
                        <div class="row">

                                 <div class="col-lg-6">

                         <div class="card-box">
                                 <h4 class="m-t-q header-title">Data Pensiun</h4>

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Notas</label>
                                          <asp:TextBox ID="txtnotas" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Jenis Pensiun</label>
                                         <asp:TextBox ID="Txtjenis" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                                   

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Nama Pensiunan</label>
                                          <asp:TextBox ID="txtnamapensiun" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tgl Lahir Pensiunan</label>
                                          <asp:TextBox ID="txttgllahir" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                   
                                </div>
                                 

                             <div class="form-row">
                                    
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Kode Pangkat</label>
                                         <asp:TextBox ID="txtkodepangkat" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                  <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Ket Jiwa</label>
                                          <asp:TextBox ID="txtjiwa" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Stop</label>
                                         <asp:TextBox ID="txtstop" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Kode Cabang</label>
                                          <asp:TextBox ID="txtcabang" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Telepon</label>
                                         <asp:TextBox ID="txttelepon" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>


                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Kode Bayar</label>
                                          <asp:TextBox ID="txtkodebyr" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Nama Bayar</label>
                                         <asp:TextBox ID="txtnamabyr" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                             
                                                                  
                                    
                                    
                                    
                                </div>

                                 </div>

                

                            <div class="col-lg-6">

                        <div class="card-box">
                             <h4 class="m-t-0 header-title">Pendapatan</h4>
                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Pendapatan Pokok</label>
                                          <asp:TextBox ID="txtpenpok" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tunjangan Istri</label>
                                         <asp:TextBox ID="txttistri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tunjangan Anak</label>
                                          <asp:TextBox ID="txttanak" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tunjangan Pajak</label>
                                         <asp:TextBox ID="txttpajak" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tunjangan Beras</label>
                                          <asp:TextBox ID="Txttberas" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Penyesuaian</label>
                                         <asp:TextBox ID="txtpenyesuaian" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                           
                           <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Pembulatan</label>
                                          <asp:TextBox ID="txttbulat" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Kotor</label>
                                         <asp:TextBox ID="TxtKotor" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Bersih</label>
                                          <asp:TextBox ID="TxtBersih" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Rapel</label>
                                         <asp:TextBox ID="TxtRapel" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                             
                           
                        </div> <!-- end card-box -->
                    </div>


                            <div class="col-lg-6">

                        <div class="card-box">
                             <h4 class="m-t-0 header-title">Penerima</h4>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Nama Penerima</label>
                                          <asp:TextBox ID="Txtpenerima" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tanggal Lahir Penerima</label>
                                         <asp:TextBox ID="Txttgllahirpenerima" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                     
                            <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Alamat Penerima</label>
                                            <asp:TextBox ID="Txtalamatpenerima" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>                                                       
                            </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Dati 4</label>
                                          <asp:TextBox ID="Txtdati4" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Kota</label>
                                         <asp:TextBox ID="txtkota" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                         
                  
                        </div> <!-- end card-box -->
                    </div>

                            <div class="col-lg-6">

                        <div class="card-box">
                             <h4 class="m-t-0 header-title">Lain-Lain</h4>
                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">No.Urut.Dapem</label>
                                          <asp:TextBox ID="txtnourutdapem" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tmt.Pensiun</label>
                                         <asp:TextBox ID="Txttmppensiun" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                           <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">No.Skep</label>
                                          <asp:TextBox ID="TxtNoskep" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="inputState" class="col-form-label">Tanggal Skep</label>
                                         <asp:TextBox ID="TxttglSkep" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   <div class="form-group col-md-5">
                                        <label for="inputState" class="col-form-label">Penerbit Skep</label>
                                         <asp:TextBox ID="Txtpenerbitskep" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Norek</label>
                                          <asp:TextBox ID="Txtnorek" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                   <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Npwp</label>
                                          <asp:TextBox ID="txtnpwp" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                            
                                   
                                </div>
                         
                         
                  
                        </div> <!-- end card-box -->
                    </div>

          
              
                    </div>
  
   
   </asp:Panel>

    <asp:Panel ID="pnlformatasabri" Visible="false" runat="server">
                    <asp:LinkButton ID="LinkButton5" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembaliasbri_Click" /> 
                            
                                
                                       <div class="ribbon ribbon-custom"></div>
                            <p class="text-muted font-14 m-b-30">
                            </p>
                        <div class="row">

                                 <div class="col-lg-6">

                         <div class="card-box">
                                 <h4 class="m-t-q header-title">Data asabri</h4>
                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Nopens</label>
                                          <asp:TextBox ID="txtnopensasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Nrp</label>
                                         <asp:TextBox ID="txtnrpasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                                    
  

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Nama Pensiunan</label>
                                          <asp:TextBox ID="txtnamaasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tgl Lahir Pensiunan</label>
                                         <asp:TextBox ID="txttgllahirasabri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Kode Jiwa</label>
                                          <asp:TextBox ID="txtkdjiwaasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Usia</label>
                                         <asp:TextBox ID="txtusiaasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                               
                               <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <label for="inputCity" class="col-form-label">kd_jby</label>
                                          <asp:TextBox ID="txtkd_jbyasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="inputState" class="col-form-label">Kode Pangkat</label>
                                         <asp:TextBox ID="txtpangkatasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Nama Mitra</label>
                                         <asp:TextBox ID="Txtmitraasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>

                                   
                                </div>

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Kode Cabang</label>
                                          <asp:TextBox ID="txtkodecabangasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Nama Cabang</label>
                                         <asp:TextBox ID="txtnamacabangasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                                   
                              <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">Alamat</label>
                                            <asp:TextBox ID="txtalamatasbri" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>                                                       
                            </div>
                            <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <label for="inputCity" class="col-form-label">Rt</label>
                                          <asp:TextBox ID="txtrtasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="inputState" class="col-form-label">RW</label>
                                         <asp:TextBox ID="txtrwasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Kelurahan</label>
                                         <asp:TextBox ID="txtkelurahanasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Kecamatan</label>
                                          <asp:TextBox ID="txtkecamatanasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Provinsi</label>
                                         <asp:TextBox ID="txtprovinsiasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                                    
                                </div>

                                 </div>

                

                            <div class="col-lg-6">

                        <div class="card-box">
                             <h4 class="m-t-0 header-title">Pendapatan</h4>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Pendapatan Pokok</label>
                                          <asp:TextBox ID="txtpendapatanpokokasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tunjangan Istri</label>
                                         <asp:TextBox ID="txttunjistriabsri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                             <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tunjangan Anak</label>
                                          <asp:TextBox ID="txttunjanakasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Tunjangan Beras</label>
                                         <asp:TextBox ID="txttunberasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                            
                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Tunjangan Lain</label>
                                          <asp:TextBox ID="txttunjlainasbri" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Jumlah Bruto</label>
                                         <asp:TextBox ID="txtbrutoasbri" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Potongan pph21</label>
                                          <asp:TextBox ID="txtpot_pph21" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Potongan Askes</label>
                                         <asp:TextBox ID="txtpot_askes" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>

                            <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Potongan Hutang</label>
                                          <asp:TextBox ID="txtpot_hutang" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Potongan lain</label>
                                         <asp:TextBox ID="txtpot_lain" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                          
                            <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Jumlah Potong</label>
                                          <asp:TextBox ID="txtjml_potong" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Jumlah Netto</label>
                                         <asp:TextBox ID="txtjlm_netto" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Pembulatan</label>
                                         <asp:TextBox ID="txtpembulatan" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
                          
                                   <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Bank Rekening</label>
                                          <asp:TextBox ID="txtbank_rekening" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="inputState" class="col-form-label">Jenis Rekening</label>
                                         <asp:TextBox ID="txtjenis_rekening" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                   
                                </div>
           
                            <div class="form-group">
                                            <label for="inputpid" class="col-3 col-form-label">No Rekening</label>
                                            <asp:TextBox ID="txtno_rekening" CssClass="form-control" runat="server"></asp:TextBox>                                                       
                            </div>
                            
                           </div> <!-- end card-box -->
                    </div>


                            

                            
          
              
                    </div>
  
   
   </asp:Panel>
            
      

     
<asp:Panel ID="PnlPeta" Visible="false" runat="server">
                                                            
                <div class="row">
                    <div class="col-12">
                        <div class="card-box">
                            <h4 class="m-t-0 header-title">PETA</h4>
                            <p class="text-muted m-b-30 font-14">
                               Detail Peta
                            </p>
                                                      
                            

                            <div class="row">
                                <div class="col-12">
                                      <div>  
                                <asp:TextBox ID="TextBox1" Enabled="false" runat="server"></asp:TextBox>
                                <asp:TextBox ID="TextBox2" Enabled="false" runat="server"></asp:TextBox>
      
    </div>  
        <div>  
            <asp:Button ID="Button3" CssClass="btn btn-outline-primary" runat="server" Text="Pencarian" OnClientClick="initialize();return false;"  />
            
        </div>  
    <div id="map_canvas" style="width:100%; height:500px; border: 5px solid #5E5454;">  
    </div>  
                                    
                                </div>

                            </div>
                            <!-- end row -->

                        </div> <!-- end card-box -->
                    </div><!-- end col -->
                </div>
                <!-- end row -->


        </asp:Panel>
                    



            
         </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger  ControlID="ddlAreaSena" />
                <%--<asp:AsyncPostBackTrigger ControlID="ddlcabangprodukAsrn" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="btnsearch" />--%>
                 <asp:PostBackTrigger ControlID="btnSearchFollowUp" />
                
            </Triggers>
        </asp:UpdatePanel>

<%--<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
<ProgressTemplate>
<div class="overlay">
<div style=" z-index: 1000; margin: 300px auto;padding: 10px;width: 160px;background-color: White;border-radius: 10px;filter: alpha(opacity=100);opacity: 1;-moz-opacity: 1;">
<asp:Image ID="Image1" ImageUrl="../../assets/images/loadx.gif" AlternateText="Processing" runat="server" Width="136px"/>
</div>
</div>
</ProgressTemplate>
</asp:UpdateProgress>--%>

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

