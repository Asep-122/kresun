<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPersetujuanPinjaman.aspx.cs" Inherits="Kresun_Process_EditPersetujuanPinjaman" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Edit Verifikasi Permohonan Kredit</title>
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
                        if (fr == '1') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                        }
                    }
                });
            }, 0);

        }
          function checkTextAreaMaxLength(textBox,e, length)
        {

                var mLen = textBox["MaxLength"];
                if(null==mLen)
                    mLen=length;

                var maxLength = parseInt(mLen);
                if(!checkSpecialKeys(e))
                {
                 if(textBox.value.length > maxLength-1)
                 {
                    if(window.event)//IE
                      e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                 }
            }   
        }
        function checkSpecialKeys(e)
        {
            if(e.keyCode !=8 && e.keyCode!=46 && e.keyCode!=37 && e.keyCode!=38 && e.keyCode!=39 && e.keyCode!=40)
                return false;
            else
                return true;
        }

          function sweetAlertsave(btnSetuju,errMsg) {
            if (btnSetuju.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSetuju.dataset.confirmed = false;
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
                        btnSetuju.dataset.confirmed = true;
                        btnSetuju.click();
                  
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
                }
        }

        function sweetAlerttolak(btnSimpanTolakan,errMsg) {
            if (btnSimpanTolakan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSimpanTolakan.dataset.confirmed = false;
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
                        btnSimpanTolakan.dataset.confirmed = true;
                        btnSimpanTolakan.click();
                  
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

        function Comma(Num) { //function to add commas to textboxes
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
            <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Pensiun</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="EditVerifPermohonan.aspx">Edit Verifikasi Permohonan Kredit</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Edit Verifikasi Permohonan Kredit</h4>
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
        <asp:Panel ID="PnlFront" Visible="false" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data Verifikasi Permohonan Kredit</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="887px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID Permohonan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id_permohonan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="No Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kode Produk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblkdproduk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Nama Nasabah">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Usia">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusia" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "usia")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Permohonan Plafon">
                                            <ItemTemplate>
                                                <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "permohonan_plafon", "{0:N2}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jangka">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           
                                        <asp:TemplateField HeaderText="Action">
                                                                            <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
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
             <asp:Panel ID="pnl1" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA NASABAH</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDid" runat="server" />	 
                                                        <asp:HiddenField ID="HiddenField2" runat="server" />	 
                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                                        <asp:HiddenField ID="HiddenField5" runat="server" />
                                                        <asp:HiddenField ID="HiddenField6" runat="server" />
                                                        <asp:HiddenField ID="HiddenField7" runat="server" />
                                                            
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label15" runat="server" class="col-form-label">No. Nasabah</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnonasabah" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div>
                                                            
                                                            
                                                            <div class="form-group"> 
                                                                <asp:label ID="Label38" runat="server" class="col-form-label">No. Identitas</asp:label>
																<asp:TextBox ID="txtidentitas"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>  
                                                            <div class="form-group"> 
                                                                <asp:label ID="Label59" runat="server" class="col-form-label">Nama Nasabah</asp:label>
																<asp:TextBox ID="txtnamanasabah"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>   
                                                            <div class="form-group"> 
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">Nama Ibu Kandung</asp:label>
																<asp:TextBox ID="txtnamaibukandung"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>   
                                                            <div class="form-group"> 
                                                                <asp:label ID="Label85" runat="server" class="col-form-label">Nama Pasangan</asp:label>
																<asp:TextBox ID="txtnamapasangan"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>  
                                                           
                                                        <div class="form-row">
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label53" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:TextBox ID="txtjenkel"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>   
                                                           
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label id="Label32" runat="server"  class="col-form-label">Tempat Lahir</asp:label>
                                                                <asp:TextBox ID="txtempatlahir" Enabled="false" CssClass="form-control" runat="server"  TextMode="DateTime"></asp:TextBox>
                                                           </div> 
															
                                                              <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label33" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txttgllahir"   Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                           
                                                            
                                                              <div class="form-group">
                                                                <asp:label ID="Label26" runat="server" class="col-form-label">NPWP</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnpwp"  Enabled="false" runat="server" ></asp:TextBox>
                                                            </div> 
                                                        </div>

                                                            <div class="form-group"> 
                                                                  <asp:label ID="Label34" runat="server" class="col-form-label">Alamat</asp:label>
																  <asp:TextBox CssClass="form-control" ID="txtalamat" TextMode="MultiLine"  onkeyDown="checkTextAreaMaxLength(this,event,'150');" Rows="3" Enabled="true"  runat="server" ></asp:TextBox>
                                                            </div>  
                                                    </div>
  
                                                        <div class="form-group col-md-6"> 
                                                            
                                                        <div class="form-row">
                                                              
                                                              <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label35" runat="server" class="col-form-label">Kelurahan</asp:label> 
                                                                <asp:TextBox ID="txtkelurahan" CssClass="form-control" MaxLength="50" Enabled="true" runat="server"  ></asp:TextBox>
                                                            </div>   
                                                              <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label36" runat="server" class="col-form-label">Kecamatan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkecamatan" MaxLength="50" Enabled="true"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                              <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label37" runat="server" class="col-form-label">Kota</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkota" MaxLength="50" Enabled="true"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                             <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label57" runat="server" class="col-form-label">Kode Pos</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkodepos" MaxLength="6" Enabled="true" onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                            </div> 

                                                        </div>
                                                             <div class="form-group"> 
                                                                <asp:label ID="Label16" runat="server" class="col-form-label">Kode Produk</asp:label>
																<asp:TextBox ID="txtkdproduk" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox>
															</div>
                                                             <div class="form-group"> 
                                                                <asp:label ID="Label17" runat="server" class="col-form-label">Program Pinjaman</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtprogrampinjaman"  Enabled="false" runat="server" ></asp:TextBox>
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label29" runat="server" class="col-form-label">Tanggal Pengajuan</asp:label>
																<asp:TextBox ID="txtrealisasi"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>
                                                         
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label55" runat="server" class="col-form-label">Tanggal Lahir Asuransi</asp:label>
																<asp:TextBox ID="txttgllahirasuransi"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>   

                                                             <div class="form-group">
                                                                <asp:label ID="Label39" runat="server" class="col-form-label">Kode Referensi</asp:label>
																<asp:TextBox ID="txtAO"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>  
                                                             <div class="form-group">
                                                                <asp:label ID="Label40" runat="server" class="col-form-label">Referensi Non Marketing</asp:label>
																<asp:TextBox ID="txtnonAO"  CssClass="form-control" Enabled="false"  runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>  

                                                            
                                                        </div>

													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                           
                                            
                                        <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                     <asp:Button ID="btnBack" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack_Click" Width="100px"  />
                                                   
                                                         

                                                    </div>
                                              
                                                <div class="float-right mb-2 "> 
                                                    
                                                  
                                                    <asp:Button ID="btnNext" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext_Click" Width="100px"  />
                                                    
                                                    </div>
                                            </div> 
                  
                                           
    
            </asp:Panel> 
            
      
                      <!-- Panel Edit -->
        
                 <!-- Panel Edit -->
             <asp:Panel ID="pnl2" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENSIUN</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDStatusDebitur" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodePos" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodeArea" runat="server" />
                                                        <asp:HiddenField ID="HDGender" runat="server" />
                                                        <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                        <asp:HiddenField ID="HDNoSimulasi" runat="server" />
                                                        <asp:HiddenField ID="HDNoMemo" runat="server" />
                                                            
                                                         

                                                           <div class="form-group">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">Status Pensiun</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtstspens"   onkeypress="return isnumerickey(event);"  Enabled="false" runat="server"  ></asp:TextBox>
                                                                 
                                                           </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">No. KTP</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtktp"   onkeypress="return isnumerickey(event);"  Enabled="false" runat="server"  ></asp:TextBox>
                                                                 
                                                            </div>
                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">Nama Pensiun</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamapensiun" Enabled="false"  runat="server" ></asp:TextBox>
                                                                 
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label43" runat="server" class="col-form-label">NIP</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnip"   onkeypress="return isnumerickey(event);"  runat="server" Enabled="false"  ></asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Nopen</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnopen"   onkeypress="return isnumerickey(event);"  runat="server"   Enabled="false"></asp:TextBox>
                                                                 
                                                            </div> 

                                                       
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label54" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtjeniskelamin"  runat="server"   Enabled="false"></asp:TextBox>
                                                                
                                                           </div>
                                                             <div class="form-group">
                                                                <asp:label id="Label22" runat="server"  class="col-form-label">Status Pernikahan</asp:label>
                                                               <asp:TextBox CssClass="form-control" ID="txtstatunikah"  runat="server"   Enabled="false"></asp:TextBox>
                                                                 
                                                            </div> 
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                          
                                                            

                                                            <div class="form-group">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Kode Jiwa</asp:label>
																<asp:DropDownList ID="ddlkodejiwa" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txttgllahirpensiun" Enabled="false"   runat="server" ></asp:TextBox>
                                                                
                                                            
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Tanggal Pensiun / TMT</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtpensiun"  Enabled="false"  TextMode="DateTime" runat="server" ></asp:TextBox>
                                                                  
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Tanggal Menikah</asp:label>
																<asp:TextBox ID="txtmenikah" Enabled="false"  CssClass="form-control" TextMode="DateTime"  runat="server" ></asp:TextBox>
															</div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label8" runat="server" class="col-form-label">Tanggal Lahir anak ke 1</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txtanak1"  Enabled="false"  TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                 
                                                            </div> 
                                                          
                                                              <div class="form-group">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">Tanggal Lahir anak ke 2</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txtanak2" Enabled="false"   TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                     </div> 
                                                          
                                                           

                                                               <div class="form-group">
                                                                <asp:label ID="Label12" runat="server" class="col-form-label">Tanggal Lahir anak ke 3</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txtanak3"  Enabled="false"  TextMode="DateTime" runat="server"></asp:TextBox> 
                                                                  
                                                            </div> 
                                                          
                                                           


                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                   
            </asp:Panel> 

            
                 <!-- Panel Edit -->
             <asp:Panel ID="pnlSK" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA SK PENSIUN</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDKodeJiwa" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodeProduk" runat="server" />	 
                                                        <asp:HiddenField ID="HDJenisPensiun" runat="server" />
                                                        <asp:HiddenField ID="HDSumberDana" runat="server" />
                                                        <asp:HiddenField ID="HDBunga" runat="server" />
                                                        <asp:HiddenField ID="HDTglJt" runat="server" />
                                                        <asp:HiddenField ID="HDMemo" runat="server" />
                                                        <asp:HiddenField ID="HDAO" runat="server" />
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label99" runat="server" class="col-form-label">NO SK</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnosk" Enabled="false"    runat="server"  ></asp:TextBox>
                                                                  
                                                            </div>


                                                            <div class="form-group">
                                                                <asp:label ID="Label100" runat="server" class="col-form-label">Nama Sesuai SK</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamasesuaisk"  Enabled="false"    runat="server"  ></asp:TextBox>
                                                                 
                                                            </div>
                                                            
                                                        
                                                           
                                                      
                                                        </div> 
                                                        
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <asp:label ID="Label101" runat="server" class="col-form-label">Tanggal SK Pensiun</asp:label>
                                                                <asp:TextBox CssClass="form-control" ID="txttglsk" Enabled="false"   TextMode="DateTime" runat="server"></asp:TextBox> 
                                                             
                                                            </div>
                                                           <div class="form-group">
                                                                <asp:label ID="Label102" runat="server" class="col-form-label">No. Dapem</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnodapem"  Enabled="false"    runat="server" ></asp:TextBox>
                                                               
                                                            </div> 
                                                           
                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                           
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBack2" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack2_Click" />
                                                        


                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNext2" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext2_Click" Width="65px" />


                                                    </div>
                                                
                                            </div> 
                                               
    
            </asp:Panel> 
                 
            <asp:Panel ID="pnl5" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENERIMA PENSIUN</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              <div class="form-group">
                                                                <asp:label ID="Label97" runat="server" class="col-form-label">Penerima Pensiun</asp:label>
																<asp:TextBox ID="txtpenerimapensiun" Enabled="false" CssClass="form-control"     runat="server" ></asp:TextBox> 
                                                                 
                                                            </div>

                                                          
															
															<div class="form-group">
                                                                <asp:label ID="Label88" runat="server" class="col-form-label">Nama</asp:label>
																<asp:TextBox ID="txtPekerjaan" Enabled="false" CssClass="form-control"     runat="server" ></asp:TextBox> 
                                                                 
															</div>
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label89" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox1" Enabled="false" TextMode="DateTime"  runat="server" ></asp:TextBox>
                                                                 
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label90" runat="server" class="col-form-label">Status Perkawinan</asp:label>
																<asp:TextBox ID="txtstatuskwn" Enabled="false" CssClass="form-control"     runat="server" ></asp:TextBox> 
                                                                  
                                                            </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label91" runat="server" class="col-form-label">Alamat </asp:label>
																<asp:TextBox ID="TextBox2"  CssClass="form-control" Enabled="false"  onkeyDown="checkTextAreaMaxLength(this,event,'150');"  TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                
                                                            </div>
														
                                                         
                                                        </div>
                                                        
                                                        <div class="form-group col-md-6">
                                                            
                                                           	<div class="form-group">
                                                                <asp:label ID="Label92" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox CssClass="form-control" ID="TextBox3" Enabled="false"   runat="server" ></asp:TextBox>
                                                                
                                                            </div> 
                                                           

                                                            <div class="form-group">
                                                                <asp:label ID="Label95" runat="server" class="col-form-label">Kecamatan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="TextBox4" Enabled="false"   runat="server" ></asp:TextBox>
                                                                
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label96" runat="server" class="col-form-label">Kota</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkotapekerjaan"  Enabled="false"   runat="server" ></asp:TextBox>
                                                                  
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label98" runat="server" class="col-form-label">Kode Pos</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkodepospekerjaan" Enabled="false"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                  
                                                            </div> 

                                                              <div class="form-group">
                                                                <asp:label ID="Label103" runat="server" class="col-form-label">Telepon</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txttlp" Enabled="false"    runat="server" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                                                
                                                            </div> 
                                                        </div>

													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnback5" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnback5_Click" />
                                                        


                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnnext5" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnnext5_Click" Width="65px" />


                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 
                 
                  <asp:Panel ID="pnl3" Visible="false" runat="server">
                <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA GAJI NASABAH</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           <div class="form-group">
                                                                <asp:label ID="Label50" Font-Bold="true" runat="server" class="col-form-label">DATA PENGHASILAN DEBITUR</asp:label>
														    </div>  
                                                            <div class="form-row">
                                                             <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label21" runat="server" class="col-form-label">Gaji Pokok</asp:label>
																<asp:TextBox ID="txtgapok"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server" MaxLength="12"    AutoPostBack="true"  OnTextChanged="txtgapok_TextChanged">0</asp:TextBox>  
                                                                
                                                             </div>
															
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label23" runat="server" class="col-form-label">Tunjangan istri / suami</asp:label>
																<asp:TextBox ID="txttistri"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server" AutoPostBack="true"  OnTextChanged="txttistri_TextChanged">0</asp:TextBox>  
                                                                
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label9" runat="server" class="col-form-label">TKD</asp:label>
																<asp:TextBox ID="txttkd"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttkd_TextChanged">0</asp:TextBox>  
                                                                                                                                                                                                                                                                                              
															</div>                                                                                                                                                                                                                     
                                                                                         
                                                            <div class="form-group col-md-4">                                                                                                                                                                                               
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">TPP</asp:label>                                                                                                                                          
																<asp:TextBox ID="txttpp"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txttpp_TextChanged">0</asp:TextBox>  
                                                                                                                                                                                                                                                                                       
															</div>                                                                                                                                                                                                                     
                                                                                           
                                                            <div class="form-group col-md-4">                                                                                                                                                                                             
                                                                <asp:label ID="Label87" runat="server" class="col-form-label">Tunjangan Beras</asp:label>                                                                                                                              
																<asp:TextBox ID="txttberas"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txttberas_TextChanged">0</asp:TextBox> 
                                                                 
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label18" runat="server" class="col-form-label">Tunjangan cacat</asp:label>
																<asp:TextBox ID="txttcacat"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12" onblur="checkTextField1(this);" CssClass="form-control"    runat="server" AutoPostBack="true"  OnTextChanged="txttcacat_TextChanged">0</asp:TextBox> 
                                                                                                                                                                                                         
															</div>                                                                                                                                                                                                                     
                                                                                        
                                                            <div class="form-group col-md-4">                                                                                                                                                                                                
                                                                <asp:label ID="Label19" runat="server" class="col-form-label">Pembulatan</asp:label>                                                                                                                                   
																<asp:TextBox ID="txtpembulatan"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txtpembulatan_TextChanged">0</asp:TextBox> 
                                                                   
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label20" runat="server" class="col-form-label">TPM</asp:label>
																<asp:TextBox ID="txttpm"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttpm_TextChanged">0</asp:TextBox> 
                                                                                                                                                                                                                                                                                
															</div>                                                                                                                                                                                                                     
                                                                                         
                                                            <div class="form-group col-md-4">                                                                                                                                                                                               
                                                                <asp:label ID="Label49" runat="server" class="col-form-label">Tunjangan Khusus</asp:label>                                                                                                                             
																<asp:TextBox ID="txttkhusus"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttkhusus_TextChanged">0</asp:TextBox> 
                                                                 
															</div>
                                                                
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label51" runat="server" class="col-form-label">Tunjangan Kematian</asp:label>
																<asp:TextBox ID="txttkematian"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  AutoPostBack="true"  OnTextChanged="txttkematian_TextChanged">0</asp:TextBox> 
                                                                                                                                                                                                                                               
															</div>                                                                                                                                                                                                      
                                                                                                                                                                                                                                                                        
                                                            <div class="form-group col-md-4">                                                                                                                                                                           
                                                                <asp:label ID="Label58" runat="server" class="col-form-label">Tunjangan pajak</asp:label>                                                                                                               
																<asp:TextBox ID="txttpajak"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   AutoPostBack="true"  OnTextChanged="txttpajak_TextChanged">0</asp:TextBox> 
                                                                
															</div>
                                                           
                                                             </div>
                                                            
                                                        <div class="form-group">
                                                            <asp:label ID="lblGajiKotor" runat="server" class="col-form-label">Gaji Kotor</asp:label>
															<asp:TextBox ID="txtgajikotor" Enabled ="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  ></asp:TextBox> 

														</div>
                                                        </div> 
                                                            
                                                        <div class="form-group col-md-6">
                                                             <div class="form-group">
                                                                <asp:label ID="Label52" Font-Bold="true" runat="server" class="col-form-label">DATA POTONGAN PENGHASILAN DEBITUR</asp:label>
														    </div>  
                                                    <div class="form-row">
                                                            <div class="form-group col-md-4"> 
                                                                 <asp:label id="Label24" runat="server" class="col-form-label">Potongan Askes</asp:label>
                                                                 <asp:TextBox ID="txtpotaskes" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control" runat="server"  AutoPostBack="true"  OnTextChanged="txtpotaskes_TextChanged">0</asp:TextBox>    
                                                                 
                                                            </div>   
                                                        
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label25" runat="server" class="col-form-label">Potongan Assos</asp:label>
																<asp:TextBox ID="txtpotassos" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);"  CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtpotassos_TextChanged">0</asp:TextBox>        
                                                                 
                                                           </div>                                                                                                                                                                                                                                
                                                                                      
                                                            <div class="form-group col-md-4">                                                                                                                                                                                                            
                                                                <asp:label ID="Label27" runat="server" class="col-form-label">Potongan Pajak</asp:label>                                                                                                                                         
																<asp:TextBox ID="txtpotpajak" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" AutoPostBack="true"  OnTextChanged="txtpotpajak_TextChanged">0</asp:TextBox>      
                                                                 
                                                           </div>                                                                                                                                                                                                                           
                                                                                                                                                                                                                                                                                            
                                                            <div class="form-group col-md-4">                                                                                                                                                                                               
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Potongan Lain</asp:label>                                                                                                                                     
																<asp:TextBox ID="txtpotlain" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtpotlain_TextChanged">0</asp:TextBox>       
                                                                
                                                           </div>
                                                        
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label93" runat="server" class="col-form-label">Sewa Rumah</asp:label>
																<asp:TextBox ID="txtpotsewarmh" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"  AutoPostBack="true"  OnTextChanged="txtpotsewarmh_TextChanged">0</asp:TextBox>       
                                                                
                                                           </div>
                                                        
                                                            <div class="form-group col-md-4"> 
                                                                <asp:label ID="Label94" runat="server" class="col-form-label">Potongan kasda</asp:label>
																<asp:TextBox ID="txtpotkasda" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" MaxLength="12"  onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"   AutoPostBack="true"  OnTextChanged="txtpotkasda_TextChanged">0</asp:TextBox>     
                                                                </div>
                                                            </div>
                                                           <div class="form-group">
                                                                <asp:label ID="lbltotalPengeluaran" runat="server" class="col-form-label">Total Potongan</asp:label>
																<asp:TextBox ID="txttotal"  Enabled ="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" >0</asp:TextBox>      
                                                           </div>
                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="lblsisapenghasilan" runat="server" class="col-form-label">Gaji Bersih</asp:label>
																<asp:TextBox ID="txtgajibersih" Enabled ="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" >0</asp:TextBox>      
                                                                
                                                           </div>

                                                           
                                                            </div>  

													</div>
												</div>  

                                            </div>
                                         </div>        
                                        
                  
    
                </span></span> 

                      
                                             <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                     <asp:Button ID="btnBack3" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack3_Click" Width="100px"  />
                                                   
                                                         

                                                    </div>
                                              
                                                <div class="float-right mb-2 "> 
                                                    
                                                    <asp:Button ID="btnNext3" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext3_Click" Width="100px"  />
                                                   
                                                   <%-- <asp:Button ID="btnTolak" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="TOLAK" OnClick="btnTolak_Click" Width="100px" />
                                                    <asp:Button ID="btnSimpan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="SETUJU" OnClick="btnSimpan_Click" Width="100px" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');"  />
                                                    --%>
                                                    </div>
                                            </div> 
    
            </asp:Panel> 
            
            <asp:Panel ID="pnl4" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENGAJUAN KREDIT NASABAH</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                       <asp:HiddenField ID="HDJenisPinjaman" runat="server"/> 
                                                        <div class="form-group col-md-6">
                                                           
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Kode Produk</asp:label>
																<asp:TextBox ID="txtkodeproduk" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox>
															
                                                            </div>
                                                             

                                                            <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Program Pinjaman</asp:label>
																<asp:TextBox ID="txtprogpinj" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox>
															
                                                             </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">Sumber Dana</asp:label>
																<asp:DropDownList ID="ddlsumberdana" Enabled="false"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                               </div>
                                                            
                                                           <div class="form-row">
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label31" runat="server" class="col-form-label">Permohonan Kredit</asp:label>
																    <asp:TextBox CssClass="form-control" ID="txtplafon" Enabled="false"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  ></asp:TextBox> 
                                                           
                                                                </div> 
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label41" runat="server" class="col-form-label">Persetujuan Bendahara</asp:label>
																    <asp:TextBox CssClass="form-control" ID="txtbendahara" Enabled ="false"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox> 
                                                                </div> 
                                                            </div>
                                                            
                                                           <div class="form-row">
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label42" runat="server" class="col-form-label">Jangka Waktu (Bulan)</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtjangka" Enabled="false" onkeypress="return isnumerickey(event);" runat="server"   ></asp:TextBox>
                                                                </div> 
                                                            
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label47" runat="server" class="col-form-label">Persen Gaji</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtpersengaji" Enabled="false" runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" ></asp:TextBox>
                                                                     
                                                                </div>

                                                                 <div class="form-group col-md-4">
                                                                    <asp:label ID="Label76" runat="server" class="col-form-label">Usia</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtusia"  Enabled="false" runat="server"  ></asp:TextBox>
                                                                </div> 
                                                            </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label48" runat="server" class="col-form-label">Status Take Over</asp:label>
                                                                 
                                                                 <asp:TextBox CssClass="form-control" ID="txtTO" Enabled="false"    runat="server"></asp:TextBox>
																 
                                                             </div> 
                                                              <div class="form-group">
                                                                <asp:label ID="Label81" runat="server" class="col-form-label">Jenis Penggunaan</asp:label>
																<asp:DropDownList ID="ddlPenggunaan" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList> 
                                                           
                                                             </div>
                                                            
                                                              <div class="form-group">
                                                                 <asp:label id="Label56" runat="server" class="col-form-label">Tanggal Realisasi</asp:label>
                                                                 <asp:TextBox CssClass="form-control" ID="txttglrealisasi" Enabled="false"  TextMode="DateTime" runat="server"  ></asp:TextBox>
                                                                   
                                                              </div>   

                                                            <div class="form-group">
                                                                 <asp:label id="Label66" runat="server" class="col-form-label">Tanggal Jatuh Tempo</asp:label>
                                                                 <asp:TextBox CssClass="form-control" ID="txttgljt" Enabled="false"     runat="server"></asp:TextBox>
                                                            </div>  
                                                            
                                                           <div class="form-row">
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label60" runat="server" class="col-form-label">Persen Provisi</asp:label>
	                                                                <asp:TextBox CssClass="form-control" ID="txtProvisi"  Enabled="false" runat="server" ></asp:TextBox>
                                                                </div> 
                                                               
                                                              <%--  <div class="form-group col-md-4">
                                                                    <asp:label ID="Label110" Visible="false" runat="server" class="col-form-label">Persen Perhitungan</asp:label>
	                                                                <asp:TextBox CssClass="form-control"  Visible="false" ID="txtpersenperhitungan"  Enabled="false" runat="server" ></asp:TextBox>
                                                                     
                                                                </div> --%>
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label67" Font-Bold="true" runat="server" class="col-form-label">JURU BAYAR</asp:label>
														    </div>  

                                                              <div class="form-group">
                                                                <asp:label ID="Label68" runat="server" class="col-form-label">Kantor Bayar</asp:label>
																<asp:TextBox CssClass="form-control"   ID="txtktrbyr"  Enabled="false" runat="server" ></asp:TextBox>
                                                                     
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label69" runat="server" class="col-form-label">Bank Rekening Nasabah</asp:label>
																<asp:DropDownList ID="ddlBankNasabah" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>
                                                             <div class="form-group">
                                                                    <asp:label ID="Label70" runat="server" class="col-form-label">No. Rekening Bank Nasabah</asp:label>
	                                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtrekeningbankdeb"    runat="server" ></asp:TextBox>
                                                                
                                                                </div> 
                                                             <div class="form-group">
                                                                    <asp:label ID="Label71" runat="server" class="col-form-label">Atas Nama Rekening Bank Nasabah</asp:label>
	                                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtrekeningnamadeb"    runat="server" ></asp:TextBox>
                                                             </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label61" runat="server" class="col-form-label">jaminan</asp:label>
																<asp:TextBox ID="txtjaminan"  CssClass="form-control" Enabled="false"  onkeyDown="checkTextAreaMaxLength(this,event,'200');"  TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                 
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                                
                                                            
                                                            
                                                           <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label62" runat="server" class="col-form-label">Angsuran Dimuka</asp:label>
                                                             	<asp:TextBox CssClass="form-control" ID="txtangsurandimuka" Enabled="false" runat="server"   onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  ></asp:TextBox>
                                                                 
                                                            </div> 

                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label63" runat="server" class="col-form-label">Biaya Angsuran Dimuka</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbyangsurandimuka" Enabled="false" onkeypress="return isnumerickey(event);"  runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                               
                                                              <div class="form-group col-md-4">
                                                                <asp:label ID="Label108" runat="server" class="col-form-label">Biaya Take Over</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbyto" Enabled="false"  onkeypress="return isnumerickey(event);"  runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                            </div>

                                                              <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label64" runat="server" class="col-form-label">Angsuran Perbulan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);" ID="txtangsuranperbulan"   runat="server" ></asp:TextBox>
                                                                
                                                            </div> 
                                                            
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label65" runat="server" class="col-form-label">Baserate</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbaserate" Enabled="false"  runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">Rate Asuransi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbgc"  Enabled="false"  runat="server" ></asp:TextBox>
                                                                
                                                            </div> 
                                                            </div>
                                                            
                                                           <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label73" runat="server" class="col-form-label">Spread</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtspread" Enabled="false"  runat="server" ></asp:TextBox>
                                                                  
                                                            </div> 
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label111" runat="server" class="col-form-label">Bunga</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtbungas" Enabled="false"   runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                               <div class="form-group col-md-4">
                                                                <asp:label ID="Label114" runat="server" class="col-form-label">Panjar</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false"  onkeypress="return isnumerickey(event);" ID="txtbypanjar"   runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                            </div>

                                                            
                                                           <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label112" runat="server" class="col-form-label">Admin Asuransi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false"  ID="txtadmiasuransi" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                
                                                            </div> 
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label113" runat="server" class="col-form-label">Admin Provisi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false"  ID="txtadmprovisi" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                   
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label115" runat="server" class="col-form-label">Admin Pencairan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmpencairan" onkeypress="return isnumerickey(event);"    runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label116" runat="server" class="col-form-label">Admin Angsuran</asp:label>
	                                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtadmangsuran"  onkeypress="return isnumerickey(event);"  runat="server" ></asp:TextBox>
                                                                     
                                                                </div> 
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label74" runat="server" class="col-form-label">Taksasi</asp:label>
																    <asp:TextBox CssClass="form-control" Enabled="false" ID="txttaksasi"   runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>
                                                                     
                                                                </div> 
                                                               <div class="form-group col-md-4">
                                                                    <asp:label ID="Label75" runat="server" class="col-form-label">Angsuran Blokir</asp:label>
                                                                    
																    <asp:TextBox CssClass="form-control" ID="txtangsblokir" Enabled="false"  runat="server"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"></asp:TextBox>
                                                                   
                                                                </div>  
                                                            </div>
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label77" Font-Bold="true" runat="server" class="col-form-label">DATA USAHA DEBITUR</asp:label>
														    </div>  
                                                            
                                                           <div class="form-row">
                                                                
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label78" runat="server" class="col-form-label">Jenis Usaha</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox43"   runat="server" ></asp:TextBox>
                                                                
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label117" runat="server" class="col-form-label">Lokasi Usaha</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox44"    runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label118" runat="server" class="col-form-label">Jumlah Tenaga Kerja</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox45" onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                              
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label119" runat="server" class="col-form-label">Pendapatan Usaha</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox46"   onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                                
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label120" runat="server" class="col-form-label">Biaya Tenaga Kerja</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox47"   onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                                <div class="form-group col-md-4">
                                                                <asp:label ID="Label121" runat="server" class="col-form-label">Biaya Produksi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox48"   onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                                
                                                            </div>  
                                                            </div>

                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label79" Font-Bold="true" runat="server" class="col-form-label">DATA MARKETING</asp:label>
														    </div>  

                                                              <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" class="col-form-label">AO</asp:label>
																  <asp:TextBox CssClass="form-control" Enabled="false" ID="TxtAo1"   onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                                 <%--<asp:RequiredFieldValidator id="reqFavoriteColor" ErrorMessage="AO tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlAO" Runat="server" />--%>

                                                             </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label82" runat="server" class="col-form-label">Ref Non AO</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txtnonao1" Enabled="false"   onkeypress="return isnumerickey(event);" runat="server" ></asp:TextBox>
                                                                 
                                                             </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label83" runat="server" class="col-form-label">Alasan Peminjaman</asp:label>
																<asp:TextBox ID="txtalasanpeminjaman"  CssClass="form-control" Enabled="false"   onkeyDown="checkTextAreaMaxLength(this,event,'150');"  TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label84" runat="server" class="col-form-label">Status SK</asp:label>
																<asp:TextBox ID="txtstatussk" Enabled="false"  CssClass="form-control"    runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                  
                                                             </div>

                                                             <div>
                                                              
                                                                <%--<asp:Button ID="btnUploadktp" Text="Upload" runat="server" OnClick="UploadFileSK" OnClientClick="return CheckFileExistenceNPWP()" />--%>
                                                                <hr />
                                                                    <asp:TextBox Enabled="false"  ID="txtpathsk" CssClass="form-control"    runat="server" ></asp:TextBox>
                                                                     
                                                                <hr /> 
                                                            </div>

                                                          

                                                        </div>


													</div> 
                                                </div>
													</div> 
                                                 </div>
                                            
                                      
                                         <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                     <asp:Button ID="btnBack4" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBack4_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                     
                                                    <asp:Button ID="btnSimpan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="SETUJU" OnClick="btnSimpan_Click" Width="100px" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" />
                                                     
                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel>  
             
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>   
                <%--<asp:PostBackTrigger ControlID="btnTolak" />--%> 
                <asp:PostBackTrigger ControlID="GRData" />   
                <asp:PostBackTrigger ControlID="btnNext" />   
                <asp:PostBackTrigger ControlID="btnNext3" />  
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