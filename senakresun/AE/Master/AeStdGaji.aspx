<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AeStdGaji.aspx.cs" Inherits="AE_Master_AeStdGaji" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Master Standard Gaji </title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link href="../../../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../../../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../../../assets/js/modernizr.min.js"></script>

    <link href="../../../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="../../../plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../../../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../../../assets/js/popper.min.js"></script>
    <script src="../../../assets/js/bootstrap.min.js"></script>
    <script src="../../../assets/js/waves.js"></script>
    <script src="../../../assets/js/jquery.slimscroll.js"></script>

    <script src="../../../Javascript/jquery-ui.js"></script>

        <!-- Toastr css -->
    <link href="../../../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />
    
        <!-- Modal -->
    <link href="../../../plugins/custombox/css/custombox.min.css" rel="stylesheet" />
    
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../../../plugins/tooltipster/tooltipster.bundle.min.css" />
        <!-- DataTables -->
    <link href="../../../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="../../../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />


   
   
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
        function isnumerickey(e) {

            var region = '<%=CultureName%>';


            if (window.event)
                var txt = window.event.keyCode; // IE
            else
                var txt = e.which;

            if (txt > 31 && (txt < 48 || txt > 57)) {


                if (txt == 44 && region == 'id-ID') {
                    return true;
                }
                if (txt == 46 && region != 'id-ID') {
                    return true;
                }
                else {
                    //alert("Input is Number only")
                    return false;
                }

            }
            return true;
        }

        
        function Comma(Num) {

            console.log('<%=CultureName%>');
            var splitnya;
            var koma;

            var region = '<%=CultureName%>';

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
            }
            else {
                splitnya = ',';
                koma = '.';
            }

            Num += '';
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, ''); Num = Num.replace(splitnya, '');
            x = Num.split(koma);
            x1 = x[0];
            x2 = x.length > 1 ? koma + x[1] : '';
            var rgx = /(\d+)(\d{3})/;


            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + splitnya + '$2');


            return x1 + x2; 
        }

        function checkTextField1(field) {

            var region = '<%=CultureName%>';
            var splitnya;
            var dolarnya;
            var koma;

            if (region == 'id-ID') {
                splitnya = '.';
                koma = ',';
                dolarnya = '$1.';
            }
            else {
                splitnya = ',';
                koma = '.';
                dolarnya = '$1,';
            }

            if (field.value == '' || field.value == 0 || field.value == koma) {
                field.value = 0;
            }


            var nilai = field.value.split(splitnya).join('');
            var output = nilai;
            if (parseFloat(nilai)) {
                input = new String(nilai); // so you can perform string operations
                var parts = input.split(koma); // remove the decimal part
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, dolarnya).split("").reverse().join("");
                output = parts.join(koma);
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
                                    <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="AeGolongan.aspx">Standard Gaji</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Standard Gaji</h4>
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

                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Group Code" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Code Standar Gaji</h4>                           
                                    
                                <div class="form-group col-md-6">
                                        <label for="inputCity" class="col-form-label">Cabang</label>
                                        <asp:DropDownList ID="ddlcabangx" CssClass="form-control" required="required" runat="server" AutoPostBack="true" data-style="btn-custom" OnSelectedIndexChanged="ddlcabangx_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                            
                            <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="625px">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Kode Std Gaji">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkdstd" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeSTD")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Keterangan Std Gaji">
                                        <ItemTemplate>
                                            <asp:Label ID="lblket" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Cabang">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCabang")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Unit Bisnis">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketDept" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Ketdept")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>  
                                     <asp:TemplateField HeaderText="Bagian">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketbagian" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Ketbagian")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Jabatan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketJab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Golongan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketgol" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Ketgol")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField> 
                                     
                                    <asp:TemplateField HeaderText="Gaji Pokok">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljlhgjpkk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GajiPokok","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>  
                                      <asp:TemplateField HeaderText="Tunjangan Makan">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljlhMakan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TunjMakan","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Tunjangan Transport">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljlhtrp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TunjTransport","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Net Gaji">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljlhNetGaji" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NetGajiPokok","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Hilankan" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkodeDept" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodedept")%>'></asp:Label>
                                            <asp:Label ID="lblkodebagian" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodebagian")%>'></asp:Label>
                                            <asp:Label ID="lblKodeJab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeJab")%>'></asp:Label>
                                             <asp:Label ID="lblKodeGol" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeGol")%>'></asp:Label>
                                             <asp:Label ID="lblkodecabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kodecabang")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                             <asp:LinkButton ID="btndelete" Visible="false" runat="server" CausesValidation="True" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                                              <div class="row clearfix">
                                                <div class="col-md-6">
                                                    <p>
                                                        <b>Kode Standar Gaji</b>
                                                    </p>
                                                    <div class="form-group">
                                                        <div class="form-line">
                                                            <asp:TextBox CssClass="form-control" enabled="false"  ID="txtkdstdgaji"  runat="server" ></asp:TextBox>
                                                      </div>
                                                    </div>
                                                </div>
                                            </div> 

                                            <div class ="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="">
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Unit Bisnis</p>
                                                           <asp:DropDownList ID="ddlunitbisnis" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlunitbisnis_SelectedIndexChanged">
                                                            
                                                            </asp:DropDownList>
                                                        <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Cabang</p>
                                                            <asp:DropDownList ID="ddlcabang" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  width ="400">
                                                            
                                                            </asp:DropDownList>
                                                        </div> 
                                                    </div> 
                                                     <div class="">
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Bagian</p>
                                                      <asp:DropDownList ID="ddlbagian" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" width ="400" AutoPostBack="true"  OnSelectedIndexChanged ="ddlbagian_SelectedIndexChanged" >
                                                            
                                                            </asp:DropDownList>
                                                    </div> 
                                                    <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Jabatan</p>  
                                                       <asp:DropDownList ID="ddljabatan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" width ="400" >
                                                            
                                                            </asp:DropDownList>
                                                   </div> 
                                                     <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Golongan</p>  
                                                        <asp:DropDownList ID="ddlgolongan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" width ="400" >
                                                            
                                                            </asp:DropDownList>
                                                   </div> 
                                                </div>
                                                <div class="col-md-6">
                                                   <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Keterangan</p>  
                                                        <asp:TextBox ID="txtketerangan" CssClass="form-control"   runat="server" MaxLength="50"></asp:TextBox>
                                                   </div> 
                                                    <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Gaji Pokok</p>  
                                                        
                                                        <asp:TextBox ID="txtjlhgjpkk" CssClass="form-control"  value="0" runat="server" MaxLength="50" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="netgaji_ontextchanged"></asp:TextBox> 
                                                   </div>
                                                     <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Tunjangan Makan</p>  
                                                        <asp:TextBox ID="txttunmakan" CssClass="form-control"  value="0" runat="server" MaxLength="50" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="netgaji_ontextchanged"></asp:TextBox>
                                                         
                                                   </div>
                                                     <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Tunjangan Transport</p>  
                                                        <asp:TextBox ID="txttuntransport" CssClass="form-control"  value="0" runat="server" MaxLength="50" text="0,00"  onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);"  AutoPostBack="true" OnTextChanged="netgaji_ontextchanged"></asp:TextBox>
                                                        
                                                   </div>
                                                    <div>
                                                        <p class="mb-1 mt-4 font-weight-bold text-muted">Net Gaji</p>  
                                                        <asp:TextBox ID="txtnetgajipokok" CssClass="form-control" value="0"  Enabled="false"  runat="server" MaxLength="50"  data-parsley-maxlength="1" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" nkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                                                   </div>
                                                </div> 
                                            </div> 
                                        </div>
                                       
                                         
                                            <div class="row clearfix">
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click" />
                                                </div>
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
                  <asp:PostBackTrigger ControlID="GRData" />
                   <asp:PostBackTrigger ControlID="ddlcabangx" />
                 
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  
    
    <!-- Parsley js -->
    <script type="text/javascript" src="../../../plugins/parsleyjs/parsley.min.js"></script>

    <script src="../../../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="../../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="../../../plugins/sweet-alert/sweetalert2.min.js"></script>
    <!-- Tooltipster js -->
<script src="../../../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../../../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../../../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../../../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../../../plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="../../../assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="../../../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>



</html>

