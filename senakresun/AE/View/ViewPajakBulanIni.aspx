<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPajakBulanIni.aspx.cs" Inherits="SN_View_Berjangka_ViewPajakBulanIni" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pajak Bulan Ini</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

     <!-- Plugins css-->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />


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
                //(
                //    function () {

                //    });
                $('#datatable').DataTable();

                //Buttons examples
                //var table = $('#Grdtagihank').DataTable({
                //    lengthChange: false,
                //    buttons: ['copy', 'excel', 'pdf']
                //});
                var table = $('#GRData').DataTable({
                    "order": [],
                    "paging": false,
                    buttons: ['copy', 'excel', 'pdf'],
                    screenX: true,
                    "columnDefs": [{
                        "targets": 'no-sort',
                        "orderable": false,
                    }]
                });
             

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

                table.buttons().container().appendTo('#GRData_wrapper .col-md-6:eq(0)');
            

                $("div").remove(".tooltip");
                $.getScript("/assets/js/jquery.core.js");


                $('#LnkAdd, #lnkCari, #lnkSaveGR,#btnsearch').tooltipster({
                    animation: 'grow'
                });
                $('#LnkAdd, #lnkCari, #lnkSaveGR,#btncari').tooltipster({
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

                $('#txtTglLto').datepicker({
                    format: 'dd-mm-yyyy',
                    autoclose: true,
                    todayHighlight: true
                });

                $(function () {
                    var table = $('#Grdtagihank').dataTable({
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

                //table.buttons().container()
                //    .appendTo('#GRDataRekap .col-md-6:eq(0)');
            }
        </script>


    </head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="SciptManager1" runat="server"></asp:ScriptManager>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
            </script>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
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
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">View Pajak Bulan Ini</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Pajak Bulan Ini</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
<asp:Panel ID="pnlFilter" Visible="true" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="card-box ribbon-box">
                <div class="ribbon ribbon-custom">Filter No. Rekening</div>
                <p class="m-b-0">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <!-- Form Filter -->
                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="inputMonth" class="col-form-label">Bulan</label>
                                        <asp:DropDownList ID="ddlbulan" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlsp_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Januari</asp:ListItem>
                                            <asp:ListItem Value="2">Februari</asp:ListItem>
                                            <asp:ListItem Value="3">Maret</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">Mei</asp:ListItem>
                                            <asp:ListItem Value="6">Juni</asp:ListItem>
                                            <asp:ListItem Value="7">Juli</asp:ListItem>
                                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">Oktober</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">Desember</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label for="inputYears" class="col-form-label">Tahun</label>
                                        <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" >
                                        </asp:DropDownList>
                                    </div>

                                
                                </div>

                                 <div class="form-row">
                                    
                                    <div class="form-group col-md-4">
                                        <label for="inputState" class="col-form-label">Tanggal Abensi Awal</label>
                                         <asp:TextBox ID="txtStart" CssClass="form-control" runat="server" Enabled="false"  ></asp:TextBox>     
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputZip" class="col-form-label">Tanggal Abensi Akhir</label>
                                        <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>  
                                    </div>
                                </div>

                                <!-- Form No Rekening -->
                               <%-- <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label class="col-form-label">No Rekening</label>
                                        <asp:TextBox CssClass="form-control" ID="txtno_rekening" Enabled="true" runat="server" EnableViewState="true"></asp:TextBox>
                                        <asp:HiddenField ID="hdnZipCode" runat="server" />
                                    </div>                                                          
                                </div> --%>

                            </div> <!-- card-box -->
                        </div> <!-- col-md-12 -->
                    </div> <!-- row -->
                </p>
            </div> <!-- card-box ribbon-box -->
        </div> <!-- col-lg-12 -->
    </div> <!-- row -->
</asp:Panel>
                 <asp:Panel ID="Panel2" Visible="true" runat="server">
                        <div class="form-group">
                            <div class="button-list">
                                <asp:Button ID="btnSearch" class="btn btn-custom waves-light text-white" runat="server" Text="Search" OnClick="btnSerach_Click"  />
                                <%--<asp:Button ID="btnCancel" class="btn btn-custom waves-light text-white" runat="server" Text="Reset" OnClick="btnCancel_Click" />--%>
                             <%--<asp:Button ID="btnCancel" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />--%>
                            </div>
                        </div>
                    </asp:Panel>

            
                 <!-- Panel Header -->
               
                    <asp:Panel ID="PnlFront" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h4 class="m-t-0 header-title">View Pajak Bulan Ini</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width:100%;">
                                            <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nourut")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Namakaryawan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>

                                                       <asp:TemplateField HeaderText="NPWP/NIK">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNpWPNIK" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NpWPNIK")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ptkp")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="tarif">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltarif" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tarif")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Kerja">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkerja" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Harikerja")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Masuk">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMasuk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Harimasuk")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                            
                                                    <asp:TemplateField HeaderText="Potongan Hadir">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpotonganhadir" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Potongangaji","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Gaji">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jlhgajipotgaji","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="+Fee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltambah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "penambahanfee","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="-Fee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkurang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "penguranganfee","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Net Fee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnetfee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "netbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Reward">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Reward","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="AB 1%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblABreward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "newmembersharian","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Lembur">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllembur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "lembur","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Tunjangan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltunjangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tunjangan","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="THR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTHR" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhTTHR","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="JHT/JKK/JKM/BPJS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJHTPrs" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bayarprsh","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                        
                                                    <asp:TemplateField HeaderText="Total Bruto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbruto" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbruto","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Tarif Ter">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpersenpajak" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "persenpajak","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Pot Pajak">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPotPajak" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PotPajak","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Bebas Pajak">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnilaibebaspajak" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nilaibebaspajak","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Selisih Bebas">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblselisihbebas" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "selisihbebas","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
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

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
            
                 <asp:PostBackTrigger ControlID ="ddlbulan"/>
                 <asp:PostBackTrigger ControlID ="GRData"/>

                 <asp:PostBackTrigger ControlID="btnSearch" />

            </Triggers>
        </asp:UpdatePanel>


        </div>
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
<script src="/assets/js/jquery.core.js"></script>
<!-- plugin js -->
<script src="/plugins/moment/moment.js"></script>
</html>
