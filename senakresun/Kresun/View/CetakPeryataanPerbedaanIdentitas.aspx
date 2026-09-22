<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CetakPeryataanPerbedaanIdentitas.aspx.cs" Inherits="Kresun_View_CetakPeryataanPerbedaanIdentitas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cetak Surat Peryataan Perbedaan Identitas</title>
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
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1").split("").reverse().join("");
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
             
            $('#txttglrealisasi').datepicker({
                format: 'dd-MM-yyyy',
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
                                    <li class="breadcrumb-item"><a href="#">Kresun</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="CetakPeryataanPerbedaanIdentitas.aspx">Cetak Pernyataan Beda Identitas</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Cetak Pernyataan Perbedaan Identitas</h4>
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
                 <!-- Panel Front --> 
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box ribbon-box">
                            <h4 class="m-t-0 header-title">Data Surat Peryataan Perbedaan Identitas</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  AutoGenerateColumns="False" Width="887px"  OnRowCommand="GRData_RowCommand" >
                                <Columns>
                                       <asp:TemplateField HeaderText="No Nasabah">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="No Rekening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Nama Nasabah" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblnamanasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                                                         
                                    <asp:TemplateField HeaderText="Plafon">
                                        <ItemTemplate>
                                            <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafond")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jangka">
                                        <ItemTemplate>
                                            <asp:Label ID="lblhp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                         </ItemTemplate>
                                        
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tanggal Pencairan">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltglpengajuan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_pengajuan")%>'></asp:Label>
                                        </ItemTemplate>
                                        
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>        
                                        <asp:TemplateField HeaderText="Tanggal Jatuh Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltgljatuhtempo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TglJatuhTempo")%>'></asp:Label>
                                        </ItemTemplate>
                                        
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    </asp:TemplateField>   
                                        <asp:TemplateField Visible="true" HeaderText="Sumber Dana">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsumberdana" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sumberdana")%>'></asp:Label>
                                        </ItemTemplate>
    
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>  
                                        <asp:TemplateField Visible="false" HeaderText="Instansi">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinstansi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Instansi")%>'></asp:Label>
                                            </ItemTemplate>
    
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 



                                       <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-magnify"></i></asp:LinkButton>
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
          <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENANDATANGAN</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         <asp:HiddenField ID="HDRekening" runat="server" />	 
                                                        <asp:HiddenField ID="HDPlafon" runat="server" />	 
                                                        <asp:HiddenField ID="HDNasabah" runat="server" />
                                                        <asp:HiddenField ID="HDSumberdana" runat="server" />	 
                                                        <asp:HiddenField ID="HDInstansi" runat="server" />	 

                                                        <!--
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Jaminan</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtjaminan"   runat="server" required="required"></asp:TextBox>
                                                            </div>
															
															
                                                            
                                                        </div>
                                                            -->
                            
                                                          


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="row">
                                                <div class="btn-group mb-2">
                                                   <div class="button-list">
                                                   
                                                     
                                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="CETAK PENYERTAAN PERBEDAAN IDENTITAS" OnClick="btnSimpan_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />
                                                       
                                                       <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                                  
                                                   </div>
                                                    </div>
                                            </div>
    
            </asp:Panel>
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                <asp:PostBackTrigger ControlID="GRData" /> 
            
                 
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
