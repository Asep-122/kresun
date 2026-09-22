<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MelihatKartuTransaksiMutasi.aspx.cs" Inherits="FA_View_MelihatKartuTM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Melihat Kartu Transaksi Mutasi</title>
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
                                    <li class="breadcrumb-item"><a href="#">Fixed Assets</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="MelihatKartuTransaksiMutasi.aspx">Melihat Kartu Transaksi Mutasi</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Melihat Kartu Transaksi Mutasi</h4>
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
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Melihat Kartu Transaksi Mutasi" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
       
            <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box">
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                          
															<div class="form-group">
                                                                <label class="col-form-label">Kode Group Aktiva</label>
                                                                <asp:DropDownList ID="ddlgroup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true"  OnSelectedIndexChanged ="ddlgroup_SelectedIndexChanged"  ></asp:DropDownList>
                                                            </div>
															<div class="form-group">
                                                                <label class="col-form-label">Nama Aktiva</label>
                                                                <asp:TextBox ID="txtnamaaktiva" CssClass="form-control" Enabled="false" runat="server" ></asp:TextBox>
                                                            </div>
                                                           <div class="form-group">
                                                                 <label class="col-form-label">Kode Lokasi ( Cabang )</label>
                                                                 <asp:TextBox ID="txtlokasi" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                             </div>
                                                         
															 <div class="form-group">
                                                                 <asp:label   runat="server"  class="col-form-label">Nomor Aktiva</asp:label>
																 <asp:TextBox ID="txtnomoraktiva" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                 <asp:label ID="lblumur" runat="server"  class="col-form-label">Spesifikasi</asp:label>
																 <asp:TextBox ID="txtspek" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                   
                                                            </div>
                                                            <div class="form-group">
                                                                 <asp:label ID="Label1" runat="server"   class="col-form-label">Merk</asp:label>
                                                                 <asp:TextBox ID="txtmerk" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                   
                                                            </div>
                                                            <div class="form-group">
                                                                 <asp:label ID="Label2" runat="server"  class="col-form-label">Nomor Mesin</asp:label>
                                                            <asp:TextBox ID="txtnomormesin" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                   
                                                            </div>
                                                            <div class="form-group">
                                                                 <asp:label ID="Label3" runat="server" class="col-form-label">Nomor Chasis</asp:label>
																<asp:TextBox ID="txtnomorchasis" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                   
                                                            </div>

                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                 <asp:label id="Label6" runat="server"  class="col-form-label">Golongan Aktiva</asp:label>
																 <asp:TextBox ID="txtgolonganaktiva" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox>
                                                   
                                                            </div>

                                                            <div class="form-group">
                                                                 <asp:label id="Label7" runat="server"   class="col-form-label">Kelompok Aktiva</asp:label> 
																 <asp:TextBox ID="txtkelompok" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox>
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:label id="Label8" runat="server"   Visible="false" class="col-form-label">Met. Penyusutan Akuntansi</asp:label>
                                                                <asp:TextBox ID="txtmetodeakuntansi" Visible="false" CssClass="form-control"  enabled = "false"   runat="server"  ></asp:TextBox>
																<asp:TextBox ID="txtakuntansi"  Visible="false" CssClass="form-control"  enabled = "false"   runat="server" ></asp:TextBox>
                                                        
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:label id="Label9" runat="server"  class="col-form-label">Met. Penyusutan UU</asp:label>
                                                                 <asp:TextBox ID="txtmotedeUU" CssClass="form-control"  enabled = "false"   runat="server" ></asp:TextBox>
                                                                 <asp:TextBox ID="txtuu" CssClass="form-control" enabled = "false"   runat="server" ></asp:TextBox>
                                                        
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label id="Label10" runat="server"  class="col-form-label">Aktiva Aktif</asp:label>
                                                                <asp:CheckBox ID="chkaktiva" runat="server" Enabled ="false"/>
                                                                <asp:label id="Label11" runat="server"  class="col-form-label">Boleh Disusut</asp:label>
																<asp:CheckBox ID="chkboleh" runat="server" Enabled ="false"/>
																
                                                            </div>
															<div class="form-group">
                                                                <asp:label id="Label12" runat="server"   class="col-form-label">Keterangan</asp:label>
																<asp:TextBox ID="txtketerangan" CssClass="form-control" Enabled="false"  runat="server" MaxLength="50"></asp:TextBox>
                                                        
                                                            </div>
															<div class="form-group">
                                                                <asp:label id="Label4" runat="server"   class="col-form-label">Barcode ID</asp:label>
																<asp:TextBox ID="txtbarcode" CssClass="form-control"  enabled = "false"   runat="server" MaxLength="50"></asp:TextBox>
                                                                 
                                                            </div>
															<div class="form-group">
                                                               <asp:Image ID="Image1" runat="server" Height="140px" Width="280px" />
                                                            </div>

                                                            
															  
                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>    
                
                <div class="card-box">
                                                  
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
<asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  AutoGenerateColumns="False" Width="625px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Kode Aktiva">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkdaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeAktiva")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Aktiva">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnmaktiva" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaAktiva")%>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Bukti">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnomorbukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Masa Susut (dalam Bulan)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmasasusut" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MasaSusut")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TglTrn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgltrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tglTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TypeTrn">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltypetrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jumlah">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljumlah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jumlah", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Harga Perolehan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperolehan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhPerolehan", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nilai Residu">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresidu" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JlhResidu", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Harga Akumulasi">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblakumulasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "HargaAkumulasi", "{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Mulai Susut">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmulaisusut" runat="server" Format="yyyy-mm-dd"  Text='<%#DataBinder.Eval(Container.DataItem, "tglawalSusut")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Akhir Susut">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblakhirsusut" runat="server" Format="dd-mm-yyyy"  Text='<%#DataBinder.Eval(Container.DataItem, "tglsudahSusut")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>                         
                            </div>
                        </div> 

                        </div>
                  
                                            <div class="row">
                                                <div class="btn-group mb-2">
                                                   <div class="button-list"> 
                                                         <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
                                                   </div>
                                                    </div>
                                            </div>
    
            </asp:Panel>

                 <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        
                    </div>
                </div>

        </asp:Panel>
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>   
                <asp:PostBackTrigger ControlID="btnAdd" /> 
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="ddlgroup" />
                 
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
