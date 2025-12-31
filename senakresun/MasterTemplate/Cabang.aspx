<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cabang.aspx.cs" Inherits="AR_Master_Cabang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Branch Company</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />    
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="../assets/js/modernizr.min.js"></script>

    <link href="../assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="../plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="../Javascript/jquery-1.12.0.min.js"></script>
    <script src="../assets/js/popper.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/js/waves.js"></script>
    <script src="../assets/js/jquery.slimscroll.js"></script>

    <script src="../Javascript/jquery-ui.js"></script>

        <!-- Toastr css -->
    <link href="../plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />
    
        <!-- Modal -->
    <link href="../plugins/custombox/css/custombox.min.css" rel="stylesheet" />
    
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="../plugins/tooltipster/tooltipster.bundle.min.css" />
        <!-- DataTables -->
    <link href="../plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="../plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="../plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />


   
   
    <script type="text/javascript">
        function Alert(h, t, p, i, fr) {
            //alert("TEST1");
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
            location.href = 'Default.aspx ';
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
        function WarningAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
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
    <script type="text/javascript">
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9 .,]/.test(inp))
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
                                    <li class="breadcrumb-item"><a href="#">Configuration</a></li>
                                    <li class="breadcrumb-item"><a href="Cabang.aspx">Branch Company</a></li>
                                </ol>
                            </div>
                            <%--<h4 class="page-title">Master Cabang</h4>--%>
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
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Cabang" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">
                            <h4 class="m-t-0 header-title">Data Cabang</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">
                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="887px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Kode Cabang">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkodeCabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeCabang")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Cabang">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaCabang")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NPWP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNpwp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NPWP")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alamat 1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alamat 2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat2")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alamat 3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alamat3")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField> 
                                     
                                        <asp:TemplateField HeaderText="Kepala Cabang">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkepalacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kepala_cabang")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kepala Wilayah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkepalawilayah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kepala_wilayah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                                                                                        
                                        <asp:TemplateField HeaderText="Action">
                                           <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
                                           <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                           <ItemTemplate>
                                               <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" OnClientClick="return confirm('Apakah anda yakin ingin menghapus data ini ?');" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDEL" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                  <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Detail Cabang</div>
                         <br />
                         <br />
                       <div class="form-row">
                   <div class="form-group col-md-6">
                    <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label1" runat="server" class="col-md-4 col-form-label font-weight-bold">Kode Cabang</asp:label>
					 	  <div class="col-4">
                            <asp:TextBox CssClass="form-control" ID="txtKodeCabang" AutoPostBack="true" MaxLength="3" runat="server" required="required" onkeypress="return kepo(event)" OnTextChanged="cekkodecabang"></asp:TextBox>
                          </div>
                        </div>
					</div>							
					  <div class="form-group">
                      <div class="form-check-inline col-md-12">
                         <asp:label ID="Label3" runat="server" class="col-md-4 col-form-label font-weight-bold">NPWP</asp:label>
					 	  <div class="col-7">
						 <asp:TextBox ID="txtNPWP" CssClass="form-control" required="required" runat="server" MaxLength="50" onkeypress="return isnumerickey(event);"></asp:TextBox>
					  </div>
                     </div>
					</div>						
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                         <asp:label id="Label29" runat="server"  class="col-md-4 col-form-label font-weight-bold">NIK Kepala Cabang</asp:label>
                         <div class="col-7">
                          <asp:TextBox ID="txtnikkepalacabang"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="100"></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label id="Label30" runat="server"  class="col-md-4 col-form-label font-weight-bold">NIK Kepala Wilayah</asp:label>
                          <div class="col-7">
                         <asp:TextBox ID="txtnikkepalawilayah"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="100" ></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label id="Label31" runat="server"  class="col-md-4 col-form-label font-weight-bold">NIK Juru Taksir</asp:label>
                          <div class="col-7">
                         <asp:TextBox ID="txtnikjurutaksir"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="100"></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                           <asp:label id="Label7" runat="server"   class="col-md-4 col-form-label font-weight-bold">Alamat 1</asp:label>
                           <div class="col-8">
                         <asp:TextBox ID="txtalamat1"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50"></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                           <asp:label id="Label6" runat="server"  class="col-md-4 col-form-label font-weight-bold">Alamat 2</asp:label>
					       <div class="col-8">
                         <asp:TextBox ID="txtalamat2"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50"></asp:TextBox>      
                      </div>
                     </div>
					</div> 
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                           <asp:label id="Label9" runat="server"  class="col-md-4 col-form-label font-weight-bold">Alamat 3</asp:label>
                           <div class="col-8">
                          <asp:TextBox ID="txtalamat3"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50"></asp:TextBox>   
                      </div>
                     </div>
					</div> 
                    </div>
                    <div class="form-group col-md-6">										
					  <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label2" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama Cabang</asp:label>
					 	  <div class="col-7">
						  <asp:TextBox ID="txtNamaCabang" CssClass="form-control" required="required" runat="server" MaxLength="250" ></asp:TextBox>
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label id="Label10" runat="server"  class="col-md-4 col-form-label font-weight-bold">Alamat Faktur Pajak</asp:label>
                          <div class="col-8">
                         <asp:TextBox ID="txtfakturpajak"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50"  ></asp:TextBox>   
                      </div>
                     </div>
					</div>						
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                         <asp:label id="Label4" runat="server"  class="col-md-4 col-form-label font-weight-bold">Kepala Cabang</asp:label>
                         <div class="col-7">
                          <asp:TextBox ID="txtkepalacabang"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="150"  ></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label id="Label5" runat="server"  class="col-md-4 col-form-label font-weight-bold">Kepala Wilayah</asp:label>
                          <div class="col-7">
                         <asp:TextBox ID="txtkepalawilayah"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="150"></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label id="Label32" runat="server"  class="col-md-4 col-form-label font-weight-bold">Juru Taksir</asp:label>
                          <div class="col-7">
                         <asp:TextBox ID="txtjurutaksir"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="150" ></asp:TextBox>   
                      </div>
                     </div>
					</div>	
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label id="Label8" runat="server"  class="col-md-4 col-form-label font-weight-bold">Email</asp:label>
                          <div class="col-7">
                         <asp:TextBox ID="txtemail"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="50"></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label id="Label11" runat="server"  class="col-md-4 col-form-label font-weight-bold">Inisial</asp:label>
                          <div class="col-8">
                         <asp:TextBox ID="txtinisial"  CssClass="form-control" runat="server" data-style="btn-custom" MaxLength="10"  onkeypress="return kepo(event)"></asp:TextBox>   
                      </div>
                     </div>
					</div>
                      <div class="form-group">
                          <asp:CheckBox id="chkcabang" runat="server"  Checked="false" class="col-md-4 col-form-label font-weight-bold" Text="&nbsp;&nbsp; Aktif"></asp:CheckBox> 
                      </div>
                   </div>
				  </div>  
                  </div>
                 </div>
                </div>        
              </asp:Panel>
                 
                 <%--//form 2--%>
              <asp:Panel ID="pnladd2" Visible="false" runat="server">
              <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                  <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Detail System Cabang</div>
                         <br />
                         <br />
                       <div class="form-row">
                   <div class="form-group col-md-6">
                    <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label13" runat="server" class="col-md-4 col-form-label font-weight-bold">Fiskal Bulan Ini</asp:label>
					 	  <div class="col-5">
						     <asp:DropDownList ID="ddlfiskalbulanini" AutoPostBack="true" CssClass="form-control" required="required"  runat="server" OnSelectedIndexChanged="ddlfiskalbulanini_change" >
                                  <asp:ListItem Value="">Pilih Bulan Fiskal Bulan</asp:ListItem>
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
                        </div>
					</div>										
					  <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label14" runat="server" class="col-md-4 col-form-label font-weight-bold">Fiskal Tahun Ini</asp:label>
					 	  <div class="col-5">
						     <asp:DropDownList ID="ddlfiskaltahunini" AutoPostBack="true" CssClass="form-control" required="required"  runat="server" OnSelectedIndexChanged="ddlfiskaltahunini_change" >
                                  <%--<asp:ListItem Value="">Pilih Bulan Fiskal Tahun</asp:ListItem>
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
                                  <asp:ListItem Value="12">Desember</asp:ListItem>--%>
                              </asp:DropDownList>
                            </div>
                     </div>
					</div>								
					  <div class="form-group">
                      <div class="form-check-inline col-md-12">
                         <asp:label ID="Label15" runat="server" class="col-md-4 col-form-label font-weight-bold">Tutup Buku</asp:label>
					 	  <div class="col-5">
						 <asp:DropDownList ID="ddltutupbuku" AutoPostBack="true" CssClass="form-control" required="required"  runat="server" MaxLength="30" >
                             <asp:ListItem Value="">Pilih Jenis Tutup Buku</asp:ListItem>
                             <asp:ListItem Value="B">Bulanan</asp:ListItem>
                             <asp:ListItem Value="T">Tahunan</asp:ListItem>
                         </asp:DropDownList>
                       </div>
                     </div>
					</div>						
                     <div class="form-group">
                      <div class="form-check-inline col-md-12">
                         <asp:label ID="Label16" runat="server" class="col-md-4 col-form-label font-weight-bold">Posting</asp:label>
					 	  <div class="col-5">
						 <asp:DropDownList ID="ddlposting" AutoPostBack="true" CssClass="form-control" required="required"  runat="server" MaxLength="30" >
                             <asp:ListItem Value="">Pilih Jenis Posting</asp:ListItem>
                             <asp:ListItem Value="0">Sementara</asp:ListItem>
                             <asp:ListItem Value="1">Langsung</asp:ListItem>
                         </asp:DropDownList>
                       </div>
                     </div>
					</div>
                    <div class="form-group">
                      <div class="form-check-inline col-md-12">
                         <asp:label ID="Label17" runat="server" class="col-md-4 col-form-label font-weight-bold">Tutup Cabang</asp:label>
					 	  <div class="col-5">
						 <asp:DropDownList ID="ddltutupcabang" AutoPostBack="true" CssClass="form-control" required="required"  runat="server" MaxLength="30" >
                             <asp:ListItem Value="">Pilih Jenis Tutup Cabang</asp:ListItem>
                             <asp:ListItem Value="0">Tidak</asp:ListItem>
                             <asp:ListItem Value="1">Ya</asp:ListItem>
                         </asp:DropDownList>
                       </div>
                     </div>
					</div>
                      <div class="form-group">
                      <div class="form-check-inline col-md-12">
                         <asp:label ID="Label18" runat="server" class="col-md-4 col-form-label font-weight-bold">System Kurs</asp:label>
					 	  <div class="col-5">
						 <asp:DropDownList ID="ddlsystemkurs" AutoPostBack="true" CssClass="form-control" required="required"  runat="server" MaxLength="30" >
                             <asp:ListItem Value="">Pilih Jenis Tutup Buku</asp:ListItem>
                             <asp:ListItem Value="0">Single</asp:ListItem>
                             <asp:ListItem Value="1">Multi</asp:ListItem>
                         </asp:DropDownList>
                       </div>
                     </div>
					</div>
                       </div>
                       <div class="form-group col-md-6">
                        <div class="form-group">
                          <div class="form-check-inline col-md-12">
                              <asp:label ID="Label12" runat="server" class="col-md-6 col-form-label font-weight-bold">Kode Gl Bulan Berjalan</asp:label>
					     	  <div class="col-4">
                                <asp:TextBox CssClass="form-control" ID="txtkodegl"   runat="server" required="required" MaxLength="10" onkeypress="return isnumerickey(event);"></asp:TextBox>
                              </div>
                            </div>
					    </div>
                      <div class="form-group" hidden="hidden">
                        <div class="form-check-inline col-md-12">
                           <asp:label id="Label25" runat="server"  class="col-md-4 col-form-label font-weight-bold">Tanggal Awal Tahun Fiskal</asp:label>
					       <div class="col-8">
                                <asp:TextBox ID="txttanggalawalfiskal"  CssClass="form-control" runat="server" data-style="btn-custom"></asp:TextBox>      
                          </div>
                         </div>
					  </div>
                      <div class="form-group" hidden="hidden">
                        <div class="form-check-inline col-md-12">
                           <asp:label id="Label26" runat="server"  class="col-md-4 col-form-label font-weight-bold">Tanggal Akhir Tahus Fiskal</asp:label>
					       <div class="col-8">
                                <asp:TextBox ID="txttanggalakhirfiskal"  CssClass="form-control" runat="server" data-style="btn-custom"></asp:TextBox>      
                          </div>
                         </div>
					  </div>
                      <div class="form-group" hidden="hidden">
                        <div class="form-check-inline col-md-12">
                           <asp:label id="Label27" runat="server"  class="col-md-4 col-form-label font-weight-bold">Tanggal Awal Bulan Bi</asp:label>
					       <div class="col-8">
                                <asp:TextBox ID="txttanggalawalbi"  CssClass="form-control" runat="server" data-style="btn-custom"></asp:TextBox>      
                          </div>
                         </div>
					  </div>
                      <div class="form-group" hidden="hidden">
                        <div class="form-check-inline col-md-12">
                           <asp:label id="Label28" runat="server"  class="col-md-4 col-form-label font-weight-bold">Tanggal Akhir Bulan Bi</asp:label>
					       <div class="col-8">
                                <asp:TextBox ID="txttanggalakhirbi"  CssClass="form-control" runat="server" data-style="btn-custom"></asp:TextBox>      
                          </div>
                         </div>
					  </div>  
                   </div>
				  </div>  
                  </div>
                 </div>
                </div>        
              </asp:Panel>
                 <%--end form 2--%>
                 <%--//form 3--%>
              <asp:Panel ID="pnladd3" Visible="false" runat="server">
              <div class="row clearfix">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6"> 
                  <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Detail General Ledger Modul Application</div>
                         <br />
                         <br />
                       <div class="form-row">
                   <div class="form-group col-md-12">
                    <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label19" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama Tanda Tangan</asp:label>
					 	  <div class="col-4">
                            <asp:TextBox CssClass="form-control" ID="txtttd"  MaxLength="50" runat="server" ></asp:TextBox>
                          </div>
                        </div>
					</div>										
					  <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label20" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama Bagian</asp:label>
					 	  <div class="col-7">
						  <asp:TextBox ID="txtnamabagian" CssClass="form-control"  runat="server" MaxLength="50"></asp:TextBox>
                      </div>
                     </div>
					</div>										
					  <div class="form-group">
                        <asp:CheckBox id="chkGl" runat="server"  Checked="false" class="col-md-4 col-form-label font-weight-bold" Text="&nbsp;&nbsp; Penomoran Otomatis"></asp:CheckBox> 
                      </div>
                    </div>
				  </div>  
                  </div>
                 </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6"> 
                  <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Detail Cash Ledger Modul Application</div>
                         <br />
                         <br />
                       <div class="form-row">
                   <div class="form-group col-md-12">
                    <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label21" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama Tanda Tangan</asp:label>
					 	  <div class="col-4">
                            <asp:TextBox CssClass="form-control" ID="txtttdCl"  MaxLength="50" runat="server" ></asp:TextBox>
                          </div>
                        </div>
					</div>										
					  <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label22" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama Bagian</asp:label>
					 	  <div class="col-7">
						  <asp:TextBox ID="txtnamabagianCl" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                      </div>
                     </div>
					</div>										
					  <div class="form-group">
                        <asp:CheckBox id="chkCl" runat="server"  Checked="false" class="col-md-4 col-form-label font-weight-bold" Text="&nbsp;&nbsp; Penomoran Otomatis"></asp:CheckBox> 
                      </div>
                    </div>
				  </div>  
                  </div>
                 </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6"> 
                  <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Detail Account Payable Modul Application</div>
                         <br />
                         <br />
                       <div class="form-row">
                   <div class="form-group col-md-12">
                    <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label23" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama Tanda Tangan</asp:label>
					 	  <div class="col-4">
                            <asp:TextBox CssClass="form-control" ID="txtttdAp"  MaxLength="50" runat="server" required="required"></asp:TextBox>
                          </div>
                        </div>
					</div>										
					  <div class="form-group">
                      <div class="form-check-inline col-md-12">
                          <asp:label ID="Label24" runat="server" class="col-md-4 col-form-label font-weight-bold">Nama Bagian</asp:label>
					 	  <div class="col-7">
						  <asp:TextBox ID="txtnamabagianAP" CssClass="form-control" required="required" runat="server" MaxLength="50"></asp:TextBox>
                      </div>
                     </div>
					</div>										
					  <div class="form-group">
                        <asp:CheckBox id="chkAp" runat="server"  Checked="false" class="col-md-4 col-form-label font-weight-bold" Text="&nbsp;&nbsp; Penomoran Otomatis"></asp:CheckBox> 
                      </div>
                    </div>
				  </div>  
                  </div>
                 </div>
                </div>
              </asp:Panel>
                 <%--start button--%>
                <div class="hidden-print mt-4">
                   <div class="float-left mb-2"> 
                      <asp:Button ID="btnBack" Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnBack_Click" />
                      <asp:Button ID="btnCancel"  Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="Cancel"  OnClick="btnCancel_Click"/>
                  </div>
                  <div class="float-right mb-2 "> 
                      <asp:Button ID="btnNext" Visible="false" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext_Click" Width="65px" />
                      <asp:Button ID="btnSimpan" Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClick="btnSimpan_Click"  OnClientClick="return confirm('Apakah anda yakin ingin menyimpan data ini ?');" /> 
                    </div>
                </div> 
                <%--<div class="row">
                    <div class="btn-group mb-2">
                       <div class="button-list">
                           </div>
                        </div>
                </div>--%>
                 <%--end button--%>
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnNext" />
                <asp:PostBackTrigger ControlID="ddlfiskalbulanini" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  
    
    <!-- Parsley js -->
    <script type="text/javascript" src="../plugins/parsleyjs/parsley.min.js"></script>

    <script src="../plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="../plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="../plugins/sweet-alert/sweetalert2.min.js"></script>
    <!-- Tooltipster js -->
<script src="../plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="../plugins/datatables/jquery.dataTables.min.js"></script>
<script src="../plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="../plugins/datatables/dataTables.responsive.min.js"></script>
<script src="../plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="../assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="../plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>



</html>