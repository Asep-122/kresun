<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update_data_RAT.aspx.cs" Inherits="AR_Master_Update_data_RAT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Info Data Anggota</title>
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
                        
                          if (fr == '2') {
                            setTimeout(function () { <%//=btnafterDelete()%>; }, 50); //Delete
                          }
                    }
                });
            }, 0);

        }
        function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

        }


         function CheckFileExistence() {

            var filePath = document.getElementById('<%//= this.fileuploadktp.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto KTP belum di pilih!");


                return false;
            }
        }

      
        function sweetAlertCancel(btnBack,errMsg) {
            if (btnBack.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnBack.dataset.confirmed = false;
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
                        btnBack.dataset.confirmed = true;
                        redirect();
                  
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
                }
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
                  
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
                }
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

           
   
            Resize();
             $('#txttgllahir').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttgllahirasuransi').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('.mydate').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txtberlakuidentitas').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttgllahirpasangan').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttglmulaimenempati').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            
            $('.selectpicker').selectpicker();

            $(function () {
                var $gv = $("table[id$=GRData]");
                var $rows = $("> tbody > tr:not(:has(th, table))", $gv);
                var $inputs = $(".mydate", $rows);

                $inputs.datepicker();
            });

            $('#datatable').DataTable();

            //Buttons examples
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
            <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="Update_data_rat.aspx">Update Data Anggota</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Update Data Anggota RAT</h4>
                        </div>
                    </div>
                </div>
                
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter"  runat="server">
                       <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <%--<label class="col-form-label">Search By</label>--%>
                                                                <div class="col-md-12 d-flex">
                                                                    <div class="form-group col-md-6">
                                                                         <asp:label ID="Label44" runat="server" class="col-form-label">Cabang</asp:label>
                                                                          <asp:DropDownList ID="ddlkoperasi" CssClass="form-control" runat="server" data-style="btn-custom" Enabled="false"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-md-4">
                                                                        <asp:label ID="Label1" runat="server" class="col-form-label">Bulan</asp:label>
                                                                        <asp:DropDownList ID="ddlbulan" Enabled="false" runat="server" CssClass="selectpicker show-tick">
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
                                                                            <asp:ListItem Value="12" Selected="True">Desember</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-md-3">
                                                                        <asp:Label ID="Label2" runat="server" class="col-form-label">Tahun</asp:Label>
                                                                        <asp:DropDownList ID="ddltahun" Enabled="false" runat="server" CssClass="selectpicker show-tick">
                                                                            <asp:ListItem Value="2024" Selected="True">2024</asp:ListItem>
                                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                            <asp:ListItem Value="2026">2026</asp:ListItem>
                                                                            <asp:ListItem Value="2027">2027</asp:ListItem>
                                                                            <asp:ListItem Value="2028">2028</asp:ListItem>
                                                                            <asp:ListItem Value="2029">2029</asp:ListItem>
                                                                            <asp:ListItem Value="20230">2030</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                     </div>

                                                                    <div class="form-group col-md-4">
                                                                         <asp:Label ID="Label3" runat="server" class="col-form-label">Marketing</asp:Label>
                                                                       
                                                                            <asp:DropDownList ID="ddlmarketing" runat="server" CssClass="form-control" data-style="btn-custom">
                                                                            </asp:DropDownList>
                                                                           


                                                                    </div>

                                                                    <div class="form-group col-md-4">
                                                                        <asp:Label ID="Label4" runat="server" class="col-form-label">Status Anggota</asp:Label>
                                                                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" data-style="btn-custom">
                                                                            <asp:ListItem Value="1">Non Aktif</asp:ListItem>
                                                                            <asp:ListItem Value="2">Aktif</asp:ListItem>
                                                                            <asp:ListItem Value="3">Pengurus Koperasi</asp:ListItem>
                                                                            <asp:ListItem Value="4">Peserta RAT</asp:ListItem>
                                                                            <asp:ListItem Value="5">Anggota Simpanan</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                     <div class="col-md-4 p-1" style="height: 55px">
                                                                        <%--<asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>--%>
                                                                        <asp:Button ID="lnkCari" class="btn btn-success waves-light text-white mt-3" runat="server" Text="Cari" OnClick="lnkCari_Click" />
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>

            </asp:Panel>
       
        

                  <asp:Panel ID="PnlFront" runat="server">
                     <div class="row">
                         <div class="col-md-12 p-3">
                             <div class="card-box">

                                 <h4 class="m-t-0 header-title">Data Anggota</h4>
                                 <div class="form-group col-md-4">
                               
                                </div>
                                 
                                 <div class="form-row">
                                     <div class="form-group" style="width: 100%;">

                                         <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="887px" OnRowCommand="GRData_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="No. Anggota">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_anggota")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Nama Anggota">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnamaanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="No. Identitas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnoktp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_identitas")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Kode AO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_ref")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Alamat">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamat" runat="server" Width="70%" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tanggal Lahir">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lbltalhir" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_lahir")%>'></asp:Label>--%>
                                            <asp:TextBox ID="txttalhir" Enabled="false" runat="server" CssClass="mydate form-control"  Width="200" Text='<%#DataBinder.Eval(Container.DataItem, "tanggal_lahir", "{0:dd-MM-yyyy}")%>' ></asp:TextBox>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. HP">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblnohp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_hp")%>'></asp:Label>--%>
                                            <asp:TextBox ID="txtnope" runat="server" CssClass="form-control" Width="200" Text='<%#DataBinder.Eval(Container.DataItem, "no_hp")%>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                   
                                <%--    <asp:TemplateField HeaderText="Action">
                                        <HeaderStyle CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                           
                                            <asp:Button ID="btnSimpan" runat="server" CssClass="btn btn-success waves-light text-white" UseSubmitBehavior="false" data-toggle="tooltip" data-placement="left" title="Simpan perubahan" CommandName="SIMPAN" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="SIMPAN" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>

                                     </div>
                                 </div>

                             </div>
                         </div>
                     </div>

        </asp:Panel>


                 <!-- Panel Edit -->
           
        
            
                 <!-- Panel Edit -->
             

             
             

             
                 <asp:Panel ID="panelPosting" Visible="false" runat="server">
                               <div class="row">
                                <div class="col-md-12">
                                    <div class="card-box">
                                            <div class="card-box table-responsive">
                                                <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Update data ini ?');" runat="server" Text="Update Data" OnClick="btnSimpan_Click" />
                                                <%--<asp:LinkButton ID="LinkButton1" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnbackheader_Click" Width="65px" />--%>
                                </div>
                                </div>
                             </div>
                             </div>

                       
                       </asp:Panel>


                     

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                <%--<asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="btnCancel" />   --%>
<%--                 <asp:PostBackTrigger ControlID="btnSimpan" />--%>
              <%--<asp:PostBackTrigger ControlID="GRData" />--%>           
                 <asp:PostBackTrigger ControlID="lnkCari" /> 
                     
                 
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
<script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>
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
<script src="../../assets/js/jquery.core.js"></script>
<!-- plugin js -->
<script src="../../plugins/moment/moment.js"></script>
</html>
