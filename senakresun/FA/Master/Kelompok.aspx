<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Kelompok.aspx.cs" Inherits="FA_Master_Kelompok" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Kelompok</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
	<meta http-equiv="Content-Security-Policy" content="upgrade-insecure-requests"/> 
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link  rel="stylesheet" type="text/css" href="/plugins/bootstrap-select/css/bootstrap-select.min.css"  />

    <!-- App css -->
    <link  rel="stylesheet" type="text/css" href="/assets/css/bootstrap.min.css" />
    <link  rel="stylesheet" type="text/css" href="/assets/css/icons.css"  />
    <link  rel="stylesheet" type="text/css" href="/assets/css/style.css"/>

    <script src="/assets/js/modernizr.min.js"></script>

    <link  rel="stylesheet" type="text/css" href="/assets/css/jquery-ui.css" />

    <link  rel="stylesheet" type="text/css" href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" />

    <!-- Sweet Alert css -->
    <link  rel="stylesheet" type="text/css" href="/plugins/sweet-alert/sweetalert2.min.css"/>


    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>

    <script src="/Javascript/jquery-ui.js"></script>

        <!-- Toastr css -->
    <link rel="stylesheet" href="/plugins/jquery-toastr/jquery.toast.min.css"  />
    
        <!-- Modal -->
    <link rel="stylesheet" href="/plugins/custombox/css/custombox.min.css"  />
    
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
        <!-- DataTables -->
    <link rel="stylesheet" type="text/css" href="/plugins/datatables/dataTables.bootstrap4.min.css"  />
    <link rel="stylesheet" type="text/css" href="/plugins/datatables/buttons.bootstrap4.min.css" />
    <!-- Responsive datatable examples -->
    <link rel="stylesheet" type="text/css"  href="/plugins/datatables/responsive.bootstrap4.min.css"/>


   
   
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
            Num = Num.replace('.', ''); Num = Num.replace('.', ''); Num = Num.replace('.', '');
            Num = Num.replace('.', ''); Num = Num.replace('.', ''); Num = Num.replace('.', '');
            x = Num.split(',');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');


            return x1 + x2;

        }

        function isNumber(evt) {
          var theEvent = evt || window.event;
          var key = theEvent.keyCode || theEvent.which;
          key = String.fromCharCode(key);
          if (key.length == 0) return;
          var regex = /^[0-9.,\b]+$/;
          if (!regex.test(key)) {
              theEvent.returnValue = false;
              if (theEvent.preventDefault) theEvent.preventDefault();
          }
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
        function PeringatanKode(a) {

            setTimeout(function () {
                swal(
                    {
                        title: a,
                        type: "warning",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000
                    }
                ).catch(swal.noop)
            }, 100);
            //$("#txtjenisgol").val(""); 
        }
        function Delete(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil di hapus",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
                        type: "success",
                        position: "top-end",
                        showConfirmButton: false,
                        timer: 2000,
                        hideAfter: 2000,
                        afterHidden: function () {
                            setTimeout(function () { <%=btnreload()%>; }, 50);
                        }
                    }
                ).catch(swal.noop)
            }, 100);
            location.href = 'Kelompok.aspx ';
        }
        function Simpan(a, b, c, d) {

            setTimeout(function () {
                swal(
                    {
                        title: "Data berhasil disimpan",
                        text: a + ' ' + ':' + ' ' + b + ' ' + c + ' ' + ':' + ' ' + d,
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
            $("#txtkdKel").val("");
            $("#ddlgol").val("");
            $("#txtnama").val("");
            $("#txtmasa").val("");
            $("#txtbulan").val("");
            $("#txtSL").val("");
            $("#txtDB").val("");
        }

        function sweetAlertsave(btnSimpan, errMsg) {
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
    </script>
    <script type="text/javascript">
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 ]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
        }
        function text(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9]/.test(inp))
                return true;
            //alert("Hanya Bisa Angka Dan Huruf");
            return false;
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
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="Kelompok.aspx">Kelompok</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Master Kelompok</h4>
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
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kelompok" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Kelompok</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Kode Golongan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdGol" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeGol")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kode Kelompok">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdKel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeKel")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nama">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Masa Susut dalam Tahun">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmasa" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MasaSusut")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Masa Susut dalam Bulan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbulan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MasaSusutBulan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tarif Susut SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSL" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TarifSusutSL")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                   
                                                                                                        
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" OnClientClick="return confirm('Apakah anda yakin ingin menghapus data ini ?');" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                          <div class="card-box">
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <div class="form-group">
                                        <label class="col-form-label" id="textkelompok" runat="server">Kode Kelompok</label> 
                                        <asp:TextBox CssClass="form-control" ID="txtkdKel"  runat="server" parsley-trigger="change" required="required" MaxLength ="1" AutoPostBack="true" OnTextChanged="cekkode_change"></asp:TextBox>
                                     </div>      

                                     <div class="form-group">
                                          <label class="col-form-label" id="textgolongan" runat="server">Kode Golongan</label>
                                          <asp:DropDownList ID="ddlgol" CssClass="form-control" runat="server" data-style="btn-custom" required="required" AutoPostBack="true" ></asp:DropDownList>
                                         <%--<asp:DropDownList ID="ddlaktivaakhir" CssClass="form-control" required="required" runat="server"></asp:DropDownList>--%>
                                     </div>

                                    <div class="form-group">
                                        <label class="col-form-label">Nama</label>
                                        <asp:TextBox ID="txtnama" CssClass="form-control" required="required" runat="server" MaxLength ="60" onkeypress="return kepo(event);" AutoPostBack="true"></asp:TextBox>
                                    </div>      
                                    
                                </div> 
                                    <div class="form-group col-md-6">
                                        <div class="form-group">
                                            <label class="col-form-label">Masa Susut ( Dalam Tahun )</label>
                                            <asp:TextBox ID="txtmasa" CssClass="form-control" required="required" runat="server" onkeypress="return isnumerickey(event);" AutoPostBack="true"  OnTextChanged="txtmasa_TextChanged"></asp:TextBox>
                                         </div>    
                                        
                                         <div class="form-group">
                                            <label class="col-form-label">Masa Susut ( Dalam Bulan )</label>
                                            <asp:TextBox ID="txtbulan" CssClass="form-control" required="required" runat="server" onkeypress="return isnumerickey(event);" AutoPostBack="true" OnTextChanged="cekbulan_change"></asp:TextBox>
                                         </div>  
                                        
                                         <div class="form-group">
                                            <label class="col-form-label">Tarif Susut SL</label>
                                            <asp:TextBox ID="txtSL" CssClass="form-control" required="required" runat="server" onkeypress="return isNumber(event);" onkeyup="javascript:this.value=Comma(this.value);"  ></asp:TextBox>
                                        </div>  
                                    </div> 

                                        
                                         <div>
                                            <%--<p class="mb-1 mt-4 font-weight-bold text-muted" visible="false" >Tarif Susut DB</p>--%>
                                            <asp:TextBox ID="txtDB" CssClass="form-control" visible ="false" required="required" runat="server" onkeypress="return isNumber(event);"  onkeyup="javascript:this.value=Comma(this.value);" >0</asp:TextBox>
                                        </div>
                                    
                                        
                                </div>

                            </div> 
                        </div>     
                        </div>       
                  
                    <div class="row">
                        <div class="btn-group mb-2">
                            <div class="button-list">
                                <br>
                                <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" Width="65px" />
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