<%@ Page Language="C#" enableEventValidation="false" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="datamarketing.aspx.cs" Inherits="Report_datamarketing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-------------------------------------------- Default Start ------------------------------------------------------>

    <meta charset="utf-8" />
    <title>DATA TASPEN/ASABRI</title>
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

         .auto-style3 {
             position: relative;
             width: 100%;
             min-height: 1px;
             -ms-flex: 0 0 83.333333%;
             flex: 0 0 83.333333%;
             max-width: 83.333333%;
             left: 0px;
             top: -10px;
             height: 40px;
             padding-left: 15px;
             padding-right: 15px;
         }

    </style>

  <script type="text/javascript">  
        function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
<%--            function showProgress() {
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
                var table = $('#Grdtagihan').DataTable({
                    lengthChange: false
          });


                          var table3 = $('#Grdtunggakan').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf']
          });

                          var table5 = $('#Grdasabri').DataTable({
                    lengthChange: false
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

             
                             table3.buttons().container()
              .appendTo('#Grdtunggakan_wrapper .col-md-6:eq(0)');

           
                             table5.buttons().container()
              .appendTo('#Grdasabri_wrapper .col-md-6:eq(0)');

            

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
                var table = $('#Grdasabri').dataTable({
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
                                    <h4 class="text-uppercase m-b-5 font-13 font-600"> <asp:Label ID="Lbltaspen" runat="server" Text="DATA TASPEN/ASABRI"></asp:Label></h4>
                                        <div class="text-center mt-4 mb-4">
                                            <div class="row">
                                                                <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box widget-flat border-pink bg-custom text-white">
                                                                        <i class="fi-archive"></i>
                                                                        <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A1"  href="#" runat="server" onserverclick="btntagihan_Click" >DATA SYSTEM</a></p>
                                                                    </div>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box widget-flat border-pink bg-custom text-white">
                                                                        <i class="fi-briefcase"></i>
                                                                         <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A3"  href="#" runat="server" onserverclick="btnmanual_Click" >MANUAL</a></p>
                                                                                                                
                                                                     </div>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-4">
                                                                    <div  class="card-box widget-flat border-pink bg-custom text-white">
                                                                        <i class="fi-tag"></i>
                                                                         <p class="text-uppercase m-b-5 font-13 font-600"><a class="text-white" id="A2"  href="#" runat="server" onserverclick="btntunggakan_Click" >GET DATA</a></p>
                                           
                                                                     </div>
                                                                </div>
                                                  
                             
                                            </div>
                                         </div>
                                    </div>
                            </div>
                        </div>
                    </asp:Panel>

        <asp:Panel ID="Pnlsearch" runat="server" >

              <div class="card m-b-10 text-white bg-custom text-xs-center">
                        <div class="text-center mt-2 mb-2">
                                <asp:Label ID="Lbljudul"  runat="server" Text="DATA"></asp:Label>
                        </div>
                    </div>
                    
                             <div class="row">
                                  
                    
                    <div class="col-lg-6">

                        <div class="card-box">
                            <h4 class="header-title m-t-0">Search By</h4>
                            <form action="#">
                                <div class="form-group row">
                                   <label for="inputpid" class="col-3 col-form-label">Pilih DATA</label>
                                   <div class="col-7">
                                    <asp:DropDownList ID="ddlbahan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlbahan_SelectedIndexChanged">
                                       <asp:ListItem>SELECT</asp:ListItem>
                                       <asp:ListItem>TASPEN</asp:ListItem>
                                       <asp:ListItem>ASABRI</asp:ListItem>
                                    </asp:DropDownList>    
                                       </div>
                                </div>
 
                                <div class="form-group row">
                                   <label for="inputpid" class="col-3 col-form-label">Area</label>
                                   <div class="col-7">
                                    <asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlcabang_SelectedIndexChanged"></asp:DropDownList>                                   
                                </div>
                                   </div>
                                <div class="form-group row">
                                    
                                    <asp:Label ID="lblcabang" runat="server" class="col-3 col-form-label">Cabang</asp:Label>
                                    <div class="col-7">
                                    <asp:DropDownList ID="ddlcabangprodukAsrn" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlcabangprodukAsrn_SelectedIndexChanged"></asp:DropDownList>
                                     </div>
                                </div>
                             
                               

                             

                            </form>
                        </div> <!-- end card-box -->
                    </div>

                             <div class="col-lg-6">

                        <div class="card-box">

                            <h4 class="header-title m-t-2">Data yang diambil Hari Ini</h4>
                            <form action="#">
                                <div class="form-group">
                                   <label for="inputpid" class="col-3 col-form-label">Outstanding</label>
                                   <asp:Label ID="LblOutstanding" runat="server" Text="0"></asp:Label>                                  
                                </div>

                                <div class="form-group">
                                     <label for="inputpid" class="col-3 col-form-label">DATA</label>
                                     <asp:Label ID="LblBahan" runat="server" Text="0"></asp:Label>
                                     
                                </div>
                                <div class="form-group">
                                    <label for="inputpid" class="col-3 col-form-label">Minimal</label>
                                        <asp:Label ID="Lblminimal" runat="server" Text="0"></asp:Label>                                                            
                                </div>
                                

                                    <div class="form-group text-right m-b-0">
                                        <button type="button" id="btnproses" class="btn btn-outline-custom waves-light waves-effect" href="#" runat="server" onserverclick="btnproses_Click">Get DATA</button>
                                        <%--<asp:Button ID="btnproses" class="btn btn-dark waves-light text-white" href="#" runat="server" Text="Get DATA" onserverclick="btnproses_Click" OnClientClick="showProgress()"/>--%>
                                    </div>

                            </form>
                        </div> <!-- end card-box -->
                    </div>

                                 <div class="col-lg-6">

                        <div class="card-box">

                            <h4 class="header-title m-t-2">Search Data</h4>

                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <asp:Label ID="Label1" runat="server" class="col-3 col-form-label">Kantor Bayar</asp:Label>
                                    
                                      
                                    <asp:DropDownList ID="ddlKantor" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                           
                                </div>
                               
                               
                                <div class="form-group col-md-6">
                                     <asp:Label ID="Label6" runat="server" class="col-3 col-form-label">Jenis Usia</asp:Label>
                                         <asp:DropDownList ID="ddljenisusia" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" >
                                                                <asp:ListItem Value="ALL">All Jenis Usia</asp:ListItem>
                                                                <asp:ListItem Value="1">Reguler</asp:ListItem>
                                                                <asp:ListItem Value="2">Platinium</asp:ListItem>
                                                               
                                                            </asp:DropDownList>     
                                    </div>
                            </div>

                             <div class="form-row">
                                     <div class="form-group col-md-6">
                                    <asp:Label ID="Label2" runat="server" class="col-3 col-form-label">Jenis Pensiun</asp:Label>
                                    <asp:DropDownList ID="ddljenispensiun" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>                                   
                                </div>

                                     <div class="form-group col-md-6">
                                    <asp:Label ID="Label3" runat="server" class="col-3 col-form-label">Jenis Jiwa</asp:Label>
                                     <asp:DropDownList ID="ddljenisjiwa" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                     
                                </div>
                                </div>

                          
                        </div> <!-- end card-box -->
                    </div>


                                 <div class="col-lg-6">

                        <div class="card-box">

                            <h4 class="header-title m-t-2">Search Data</h4>
                           
                               
                               
                                <div class="form-row">
                                <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Alamat</label>
                                         <asp:TextBox ID="txtalamat" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>

                                <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Kecamatan</label>
                                         <asp:TextBox ID="txtnmdati4" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                                 <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Kelurahan</label>
                                         <asp:TextBox ID="txtkotasearch" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                                     </div>



                                    <div class="form-group text-right m-b-0">                                    
                                    <asp:Button ID="btnsearch" class="btn-custom" runat="server" Text="Search" OnClick="lnkCari_Click"  />
                                    </div>
                                      
                               

                          
                        </div> <!-- end card-box -->
                    </div>
                        </div>

                    </asp:Panel>    

        <asp:Panel ID="Pnltagihan" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA TASPEN</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdtagihan" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"
                                                                      AutoGenerateColumns="False"  AllowSorting="True" OnRowCommand="Grdtagihan_RowCommand">
                                                                <Columns>
                                                                        <asp:TemplateField HeaderText="+">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                        <asp:LinkButton ID="Lnkakanfollwo" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Get</asp:LinkButton>
                                                                        <asp:LinkButton ID="LnkView" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</asp:LinkButton> 
                                                                        <asp:Label ID="lblnouruttaspen" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                 
                                                                 <asp:TemplateField HeaderText="Tanggal Lahir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Usia">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Alamat">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dati 4">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldati4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "dati4")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Kota">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kota")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Notas">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notas")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama Pensiun">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>   
                                                                    <asp:TemplateField HeaderText="Nama Penerima" Visible="false">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnamapenerima" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_penerima")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gaji">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
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

        <asp:Panel ID="Pnltunggakan" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">GET DATA</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdtunggakan" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" OnRowCommand="Grdtunggakan_RowCommand" >
                                                               
                                                                <Columns>
                                                                        <asp:TemplateField HeaderText="+">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                        <asp:LinkButton ID="Lnkakanfollwo" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Delete</asp:LinkButton>
                                                                        <asp:Label ID="lblnouruttaspenx" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                        <asp:LinkButton ID="LnkView2" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</asp:LinkButton> 
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="produk">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnourutproduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "produk")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                   
                                         
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tanggal Lahir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Usia">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Alamat">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Notas">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "notas")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>    
                                                                    <asp:TemplateField HeaderText="Gaji">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
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

        <asp:Panel ID="Pnlasabari" Visible="false" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">DATA ASABRI</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    <div class="form-row">
                                                        <div class="form-group" style="width: 100%;">
                                                            <asp:GridView ID="Grdasabri" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="true"
                                                                AutoGenerateColumns="False" OnRowCommand="Grdasabri_RowCommand" >
                                                                <Columns>
                                                                      <asp:TemplateField HeaderText="+">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                        <asp:LinkButton ID="Lnkakanfollwox" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="Follow Up" runat="server" CommandName="APPROVE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Get</asp:LinkButton>
                                                                        <asp:Label ID="lblnourutasabri" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                                        <asp:LinkButton ID="LnkView2" data-placement="top" data-toggle="tooltip" class="tooltips" data-original-title="View data" runat="server" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">View</asp:LinkButton> 
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>                                                                           
                                                                              <asp:TemplateField HeaderText="Tanggal Lahir">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltgllahirpsn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgllahir","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Usia">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                                                        </ItemTemplate>                                                                   
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Alamat">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Kelurahan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkelurahan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kelurahan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Kecamatan">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblkecamatan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kecamatan")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Notas">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnotas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopens")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nama">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>    
                                                                    <asp:TemplateField HeaderText="Gaji">
                                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "gaji")%>'></asp:Label>
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
                    
                        <asp:LinkButton ID="LinkButton3" Visible="false" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembalitaspenget_Click" />        
   
                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembalitaspen_Click" />        
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
                    <asp:LinkButton ID="LinkButton2" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembaliasbri_Click" /> 
                            
                     <asp:LinkButton ID="LinkButton4" Visible="false" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembaliasbriget_Click" /> 
                           
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
            

    <asp:Panel ID="PnlManual" Visible="false" runat="server">
                                                            
                <div class="row">
                    <div class="col-12">
                        <div class="card-box">
                            <h4 class="m-t-0 header-title">Entry Data manual</h4>
                            <p class="text-muted m-b-30 font-14">
                                Isi data Notas,Nama dan Alamat dibawah ini 
                            </p>

                            <div class="row">
                                <div class="col-12">
                                    <div class="p-20">
                                        <form class="form-horizontal" role="form">
                                            <div class="form-group row">
                                                <label class="col-2 col-form-label">Data</label>
                                                <div class="col-10">
                                                  <asp:DropDownList ID="ddldatamanual" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddldatamanual_SelectedIndexChanged" >
                                                            <asp:ListItem>PILIH DATA</asp:ListItem>
                                                            <asp:ListItem>TASPEN</asp:ListItem>
                                                            <asp:ListItem>ASABRI</asp:ListItem>
                                                  </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-2 col-form-label">Cabang</label>
                                                <div class="col-10">
                                                  <asp:DropDownList ID="ddlcabangmanual" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom">
                                                   </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-2 col-form-label">Notas</label>
                                                <div class="col-10">
                                                    <asp:TextBox ID="txtnotasmanual"  CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-2 col-form-label">Nama</label>
                                                <div class="col-10">
                                                    <asp:TextBox ID="Txtnamamanual"  CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-2 col-form-label">Alamat</label>
                                                <div class="col-10">
                                                    <asp:TextBox ID="txtalamatmanual" TextMode="MultiLine" CssClass="form-control" runat="server" Rows="3"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group mb-0 justify-content-end row">
                                                <div class="col-9">
                                                    <asp:Button ID="btnkembali" CssClass="btn btn-outline-primary" runat="server" Text="Simpan" OnClick="btnSimpan_manual_Click" />
                                                    <%--<asp:LinkButton ID="lnkbtn" CssClass="btn btn-outline-primary" runat="server" Text="Kembali" OnClick="btnKembali_Click" />--%>
                                                </div>
                                            </div>


                                        </form>
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


              
                <asp:PostBackTrigger ControlID="ddlbahan" />
            
           
         
                
                
            </Triggers>
        </asp:UpdatePanel>
<%--<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
<ProgressTemplate>
<div class="overlay">
<div style=" z-index: 1000; margin: 300px auto;padding: 10px;width: 160px;background-color: White;border-radius: 10px;filter: alpha(opacity=100);opacity: 1;-moz-opacity: 1;">
<asp:Image ID="Image1" ImageUrl="/assets/images/loadx.gif" AlternateText="Processing" runat="server" Width="136px"/>
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

