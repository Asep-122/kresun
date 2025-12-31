<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListTolakanPencairan.aspx.cs" Inherits="AR_Process_ListTolakanPencairan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>List Survey Pinjaman</title>
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
                            setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
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
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="ListTolakanPencairan.aspx">List Tolakan Pinjaman</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">List Survey Pinjaman</h4>
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
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Data List Survey Pinjaman</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="887px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No. Urut">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Kode Produk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Anggota">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Tanggal Approve">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltanggalapp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "app_date")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Alasan Tolak">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalasan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alasan_tolak")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluser" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "userid")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Action">
                                                                            <HeaderStyle CssClass="align-center" Width="10%"></HeaderStyle>
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
            <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA ANGGOTA</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDStatusDebitur" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodePos" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodeArea" runat="server" />
                                                        <asp:HiddenField ID="HDGender" runat="server" />
                                                        <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                                        <asp:HiddenField ID="HDLamaKerja" runat="server" />
                                                        <asp:HiddenField ID="HDid" runat="server" />
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label43" runat="server" class="col-form-label">No. Anggota</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnoanggota"  Enabled="false" runat="server" ></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">No.Rekening Tabungan</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnorektabungan"   runat="server" ></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Anggota</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah"  Enabled="false" runat="server" ></asp:TextBox>
                                                            </div> 

                                                           <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Alamat</asp:label>
																<asp:TextBox ID="txtalamat"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

															<div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtkelurahan" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Kecamatan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkecamatan" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Kota</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkota" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                           
															
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahir" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox>
															</div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Tanggal Lahir</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txttgllahir"  Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox> 
                                                            </div> 
                                                          
                                                            <div class="form-group">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">No. KTP</asp:label> 
                                                                <asp:TextBox ID="txtnoidentitas" Enabled="false" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server"></asp:TextBox>
                                                           </div> 
                                                            
                                                           <div class="form-group">
                                                                    <asp:label id="Label22" runat="server"  class="col-form-label">Masa Berakhir KTP</asp:label>
                                                                    <asp:TextBox ID="txtmasaberakhirKTP" Enabled="false" CssClass="form-control" runat="server"  TextMode="DateTime"></asp:TextBox>
                                                           </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">NPWP</asp:label>
																  <asp:TextBox ID="txtNPWP" CssClass="form-control" Enabled="false"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                           
                                                            </div>  
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">Agama</asp:label> 
                                                                <asp:TextBox ID="txtAgama" CssClass="form-control" Enabled="false" runat="server"  ></asp:TextBox>
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txthp" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                            </div> 


                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                           
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnCancel" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />
                                                        


                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNext" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext_Click" Width="65px" />


                                                    </div>
                                            </div> 
                                               
    
            </asp:Panel> 
                 

            <asp:Panel ID="pnlPekerjaan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PEKERJAAN ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Kode Instansi</asp:label>
																<asp:TextBox ID="txtkdinstansi" CssClass="form-control" Enabled= "false"  runat="server" ></asp:TextBox> 
															 </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label8" runat="server" class="col-form-label">Pekerjaan</asp:label>
																<asp:TextBox ID="txtPekerjaan" CssClass="form-control" Enabled= "false"  runat="server" ></asp:TextBox> 
															</div>
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label47" runat="server" class="col-form-label">Nama Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtnamaPerusahaan"  CssClass="form-control" Enabled= "false"   runat="server"  ></asp:TextBox> 
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label12" runat="server" class="col-form-label">Alamat</asp:label>
																<asp:TextBox ID="txtalamatperusahaan"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

															<div class="form-group">
                                                                <asp:label ID="Label14" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtkelurahanperusahaan" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label15" runat="server" class="col-form-label">Kecamatan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkecamatanperusahaan" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label16" runat="server" class="col-form-label">Kota</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkotapekerjaan" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label17" runat="server" class="col-form-label">Kode Pos</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkodepospekerjaan" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                           
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            

                                                            <div class="form-group">
                                                                 <asp:label id="Label9" runat="server"  class="col-form-label">Bidang Usaha Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtBidangUsaha"  CssClass="form-control" Enabled= "false"   runat="server"   ></asp:TextBox>
                                                            </div>   

                                                              <div class="form-group">
                                                                <asp:label ID="Label58" runat="server" class="col-form-label">Telepon</asp:label>
																<asp:TextBox ID="txtteleponpekerjaan" CssClass="form-control" Enabled= "false" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                 
                                                              </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label59" runat="server" class="col-form-label">Fax</asp:label>
																<asp:TextBox ID="txtFaxPekerjaan" CssClass="form-control"  Enabled= "false" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                    
                                                              </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label49" runat="server" class="col-form-label">Lama Usaha</asp:label>
																<asp:TextBox ID="txtlamabekerja" CssClass="form-control"  Enabled= "false" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label51" runat="server" class="col-form-label">Kepemilikan Tempat Usaha</asp:label>
																 <asp:TextBox ID="txtkepemilikantempatusaha"  Enabled= "false"  CssClass="form-control" runat="server"   ></asp:TextBox> 
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label87" runat="server" class="col-form-label">Alamat Surat</asp:label>
																 <asp:TextBox ID="txtalamatsurat"  Enabled= "false"  CssClass="form-control" runat="server"   ></asp:TextBox> 
                                                                 
                                                            </div> 

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPekerjaan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPekerjaan_Click" />
                                                        


                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPekerjaan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPekerjaan_Click" Width="65px" />


                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 
            <asp:Panel ID="pnlTambahan" Visible="false" runat="server">
                <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA TAMBAHAN ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">Status dalam keluarga</asp:label>
																<asp:TextBox ID="txtstatusdalamkeluarga" Enabled="false"  CssClass="form-control"    runat="server"  ></asp:TextBox> 
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label18" runat="server" class="col-form-label">Kondisi Sehat</asp:label>
																<asp:TextBox ID="txtkondisisakit" Enabled="false"  CssClass="form-control"    runat="server"  ></asp:TextBox> 
                                                            </div>
                                                             
                                                     
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                                 <div class="form-group">
                                                                 <asp:label id="Label19" runat="server" class="col-form-label">Sakit yang pernah di derita (Opname)</asp:label>
                                                                 <asp:TextBox ID="txtsakit" Enabled="false"  CssClass="form-control"    runat="server"  ></asp:TextBox> 
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label20" runat="server" class="col-form-label">Tahun sakit yg diderita (Opname)</asp:label>
																<asp:TextBox ID="txttahunsakit" Enabled="false"  CssClass="form-control" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

															 

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackTambahan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackTambahan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextTambahan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextTambahan_Click" Width="65px" />

                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 
            <asp:Panel ID="pnlPendapatan" Visible="false" runat="server">
                <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENDAPATAN ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label21" runat="server" class="col-form-label">Pendapatan Bersih Usaha</asp:label>
																<asp:TextBox ID="txtpendapatanbersih" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control" runat="server" MaxLength="16" ></asp:TextBox>  
                                                   
                                                             </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label23" runat="server" class="col-form-label">Pendapatan Selain Usaha</asp:label>
																<asp:TextBox ID="txtpendapatanselainusaha" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"   ></asp:TextBox> 

															</div>

                                                            <div class="form-group">
                                                                 <asp:label id="lbltotalpendapatan" runat="server" class="col-form-label">Total Pendapatan</asp:label>
                                                                 <asp:TextBox ID="txttotalpendapatan" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  ></asp:TextBox> 
                                                            </div>   
                                                             
                                                     
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                 <asp:label id="Label24" runat="server" class="col-form-label">Pengeluaran Rumah Tangga</asp:label>
                                                                 <asp:TextBox ID="txtpengeluaranrumahtangga" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server"  ></asp:TextBox>   
                                                            
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label25" runat="server" class="col-form-label">Tagihan PLN, Listrik, PDAM, Telepon</asp:label>
																<asp:TextBox ID="TextBox2" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  CssClass="form-control" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label27" runat="server" class="col-form-label">Pinjaman / Kewajiban Lainnya</asp:label>
																<asp:TextBox ID="TextBox4" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Rekomendasi Angsuran</asp:label>
																<asp:TextBox ID="txtangsuran" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"    ></asp:TextBox>      
                                                           </div>

                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="lbltotalPengeluaran" runat="server" class="col-form-label"></asp:label>
																<asp:TextBox ID="TextBox6" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

															 

                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="lblsisapenghasilan" runat="server" class="col-form-label"></asp:label>
																<asp:TextBox ID="TextBox7" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" ></asp:TextBox>      
                                                                
                                                           </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPendapatan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPendapatan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPendapatan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPendapatan_Click" Width="65px" />

                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 

                 <asp:Panel ID="pnlLaba" Visible="false" runat="server">
                 <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA LABA ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label26" runat="server" class="col-form-label">Pendapatan Usaha</asp:label>
																<asp:TextBox ID="txtpendapatanusaha" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"   onblur="checkTextField1(this);" CssClass="form-control"  runat="server" MaxLength="16" ></asp:TextBox>  
                                                   
                                                             </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label29" runat="server" class="col-form-label">Harga Pokok</asp:label>
																<asp:TextBox ID="txthargapokok" Enabled="false"   onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  ></asp:TextBox> 

															</div>

                                                            <div class="form-group">
                                                                 <asp:label id="lbllabakotor" runat="server" class="col-form-label">Laba Kotor</asp:label>
                                                                 <asp:TextBox ID="txtlabakotor" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control"    runat="server"  ></asp:TextBox> 
                                                            </div>   
                                                             
                                                     
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                 <asp:label id="Label32" runat="server" class="col-form-label">Biaya Pegawai</asp:label>
                                                                 <asp:TextBox ID="txtbiayapegawai" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server"  ></asp:TextBox>        
                                                            
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label33" runat="server" class="col-form-label">Biaya Lain-lain</asp:label>
																<asp:TextBox ID="txtbiayalain" Enabled="false"  onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);"  CssClass="form-control" runat="server" data-style="btn-custom"></asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="lbltotalbiaya" runat="server" class="col-form-label">Total Biaya</asp:label>
																<asp:TextBox ID="txttotalbiaya" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>
                                                             
                                                              <div class="form-group">
                                                                <asp:label ID="lbllabasebelumpajak" runat="server" class="col-form-label">Laba Bersih Sebelum Pajak</asp:label>
																<asp:TextBox ID="txtlababersihsebelumpajak" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom"   ></asp:TextBox>      
                                                           </div>

                                                            
                                                           <div class="form-group">
                                                                <asp:label ID="Label36" runat="server" class="col-form-label">Pajak Penghasilan</asp:label>
																<asp:TextBox ID="txtpajakpenghasilan" Enabled="false"  Required ="required" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div> 

                                                           <div class="form-group">
                                                                <asp:label ID="lbllababersih" runat="server" class="col-form-label">Laba Bersih Setelah Pajak</asp:label>
																<asp:TextBox ID="txtlababersihsetelahpajak" Enabled="false" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);" onblur="checkTextField1(this);" CssClass="form-control" runat="server" ></asp:TextBox>      
                                                                
                                                           </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackLaba" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackLaba_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextLaba" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextLaba_Click" Width="65px" />

                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 

            <asp:Panel ID="pnlPengajuan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENGAJUAN KREDIT ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                       <asp:HiddenField ID="HDJenisPinjaman" runat="server"/> 
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">No. Rekening Lama</asp:label>
																<asp:TextBox ID="txtnoreklama" Enabled="false"  CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label34" runat="server" class="col-form-label">Jenis Kredit</asp:label>
																<asp:TextBox ID="txtjeniskredit" Enabled="false"  CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label81" runat="server" class="col-form-label">Jenis Penggunaan</asp:label>
																<asp:TextBox ID="txtjenispenggunaan" Enabled="false"  CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label35" runat="server" class="col-form-label">Tanggal Realisasi</asp:label>
                                                                 <asp:TextBox CssClass="form-control" Enabled="false"  ID="txttglrealisasi" TextMode="DateTime" runat="server"></asp:TextBox>
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label37" runat="server" class="col-form-label">Jenis Pinjaman</asp:label>
																<asp:TextBox ID="txtjenispinjaman" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label55" runat="server" class="col-form-label">Program Pinjaman</asp:label>
																<asp:TextBox ID="txtprogrampinjaman" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label56" runat="server" class="col-form-label">Sumber Dana</asp:label>
																<asp:TextBox ID="txtsumberdana" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>

															<div class="form-group">
                                                                <asp:label ID="Label38" runat="server" class="col-form-label">Plafon</asp:label>
																<asp:TextBox CssClass="form-control" Enabled ="false" ID="txtplafon" runat="server" ></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label39" runat="server" class="col-form-label">Jangka Waktu</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtjangka"  runat="server"  ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label40" runat="server" class="col-form-label">Bunga</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtbunga"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label41" runat="server" class="col-form-label">Persen Provisi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtProvisi"  runat="server" ></asp:TextBox>
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label57" runat="server" class="col-form-label">Nominal Provisi</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtnominalprovisi"    runat="server" ></asp:TextBox>
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label60" runat="server" class="col-form-label">Angsuran Perbulan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtangsuranperbulan"    runat="server" ></asp:TextBox>
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label61" runat="server" class="col-form-label">Admin Pencairan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtadminPencairan"    runat="server" ></asp:TextBox>     
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label62" runat="server" class="col-form-label">Admin Angsuran</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Enabled ="false" ID="txtadminangsuran"   runat="server" ></asp:TextBox>     
                                                            </div> 
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label63" runat="server" class="col-form-label">Sifat</asp:label>
																<asp:TextBox ID="txtsifat" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label64" runat="server" class="col-form-label">AO</asp:label>
																<asp:TextBox ID="txtAO" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label65" runat="server" class="col-form-label">Ref Non AO</asp:label>
																<asp:TextBox ID="txtNonAO" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label67" runat="server" class="col-form-label">No Tabungan Lain</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txttablain" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                           
                                                             
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <asp:label ID="Label66" runat="server" class="col-form-label">Sektor Ekonomi</asp:label>
																<asp:TextBox ID="txtsektorekonomi" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label68" runat="server" class="col-form-label">Golongan Debitur</asp:label>
																<asp:TextBox ID="txtgolongandebitur" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                 <asp:label id="Label42" runat="server"  class="col-form-label">No KTP Penjamin</asp:label>
                                                                 <asp:TextBox ID="txtktppenjamin"  Enabled ="false" CssClass="form-control" onkeypress="return isnumerickey(event);"    runat="server"   ></asp:TextBox>
                                                            </div>   

                                                              <div class="form-group">
                                                                <asp:label ID="Label48" runat="server" class="col-form-label">Nama Penjamin</asp:label>
																<asp:TextBox ID="TextBox13" Enabled ="false" CssClass="form-control"    runat="server" ></asp:TextBox>
                                                                 
                                                              </div>

                                                              <div class="form-group">
                                                                <asp:label ID="Label50" runat="server" class="col-form-label">Golongan Penjamin</asp:label>
																<asp:TextBox ID="txtgolonganpenjamin" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label52" runat="server" class="col-form-label">Pendapatan/Bulan</asp:label>
																<asp:TextBox ID="txtpendapatanperbulan" Enabled ="false" CssClass="form-control"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label53" runat="server" class="col-form-label">Fisik Jaminan</asp:label>
																<asp:TextBox ID="txtfisikjaminan" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>
                                                             
                                                            <div class="form-group">
                                                                <asp:label ID="Label69" runat="server" class="col-form-label">Jaminan Tambahan</asp:label>
																<asp:TextBox ID="TextBox1" CssClass="form-control"  Enabled ="false"   runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label70" runat="server" class="col-form-label">Biaya Cadangan</asp:label>
																<asp:TextBox ID="TextBox3" CssClass="form-control" Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label71" runat="server" class="col-form-label">Nilai Taksasi</asp:label>
																<asp:TextBox ID="TextBox5" CssClass="form-control" Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">Jatuh Tempo</asp:label>
																<asp:TextBox ID="txtjttempo" CssClass="form-control"  Enabled="false"    runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label73" runat="server" class="col-form-label">Angsuran Dimuka</asp:label>
																<asp:TextBox ID="txtangsurandimuka" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>  

                                                            <div class="form-group">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Biaya Materai</asp:label>
																<asp:TextBox ID="txtbymaterai" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label75" runat="server" class="col-form-label">Biaya Asuransi</asp:label>
																<asp:TextBox ID="txtbyasuransi" CssClass="form-control" Enabled="false"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label76" runat="server" class="col-form-label">Biaya Pelunasan</asp:label>
																<asp:TextBox ID="txtbypelunasan" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                              <div class="form-group">
                                                                <asp:label ID="Label77" runat="server" class="col-form-label">Biaya Notaris</asp:label>
																<asp:TextBox ID="txtbynotaris" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label78" runat="server" class="col-form-label">Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox14" CssClass="form-control" Enabled="false"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label79" runat="server" class="col-form-label">Nominal Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox15" CssClass="form-control"  Enabled="false"     onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" class="col-form-label">Jenis Blokir</asp:label>
																<asp:TextBox ID="txtjenisblokir" CssClass="form-control" Enabled ="false" runat="server" ></asp:TextBox> 
															 </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            
    
                </span></span> 
    
            </asp:Panel> 

            <asp:Panel ID="pnlKeanggotaan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA KEANGGOTAAN ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                          
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label85" runat="server" class="col-form-label">Simpanan Pokok</asp:label>
																<asp:TextBox ID="txtsimpok" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
															 </div>
															
															 
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label89" runat="server" class="col-form-label">Simpanan Wajib</asp:label>
																<asp:TextBox ID="txtsimwa" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
                                                             </div>

                                                           

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                       <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPengajuan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPengajuan_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNextPengajuan" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextPengajuan_Click" Width="65px" />

                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 
             <asp:Panel ID="pnlJaminan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box"> 
                                                    <div class="ribbon ribbon-custom">UPLOAD BUKTI JAMINAN</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                          
                                                        <div class="form-group col-md-6">
                                                           
                                                           
                                                            <div>
                                                                 
                                                                  <div class="form-group">
                                                                <asp:label ID="Label88" runat="server" class="col-form-label">Keterangan Jaminan</asp:label>
																<asp:TextBox ID="txtketeranganJaminan" enabled="false" CssClass="form-control"  TextMode="MultiLine" Rows="6" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                            
                                                            </div> 
                                                            </div>

                                                          
															
															 
                                                         
                                                        </div>

                                                        <div class="form-group col-md-6">
                                                           
                                                           
                                                            <div>
                                                                  <asp:Image ID="imgjaminan" runat="server" Height="300px" Width="600px" />
                                                            </div>

                                                          
															
															 
                                                         
                                                        </div> 

													</div>
												</div>  

                                            </div>
                                         </div>         
                </span></span> 
    
            </asp:Panel> 
            <asp:Panel ID="pnlUsaha" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA KRITERIA USAHA ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                       <asp:HiddenField ID="HiddenField1" runat="server"/> 
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label82" runat="server" class="col-form-label">Kondisi Usaha Saat Ini</asp:label>
																<asp:TextBox ID="txtkondisiusaha" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox> 
															 </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label83" runat="server" class="col-form-label">Karakter Anggota</asp:label>
																<asp:TextBox ID="txtKarakterAnggota" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox> 
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label84" runat="server" class="col-form-label">Info Anggota dari Pihak ke 3</asp:label>
																<asp:TextBox ID="txtinfo" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox> 
                                                            </div>
                                                              
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label103" runat="server" class="col-form-label">Kondisi Pesaing</asp:label>
																<asp:TextBox ID="txtkondisipesaing" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox> 
                                                             </div>

                                                             <div class="form-group">
                                                                 <asp:label id="Label104" runat="server"  class="col-form-label">Resiko Usaha</asp:label>
                                                                 <asp:TextBox ID="txtresikousaha" CssClass="form-control" Enabled="false"  runat="server" ></asp:TextBox> 
                                                             </div>   

                                                              <div class="form-group">
                                                                <asp:label ID="Label105" runat="server" class="col-form-label">Upaya Mengatasi Resiko Usaha</asp:label>
																<asp:TextBox ID="txtatasiresiko" CssClass="form-control"  Enabled="false"  runat="server" ></asp:TextBox>
                                                                 
                                                              </div>


                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackUsaha" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackUsaha_Click" />
                                                        

                                                    </div> 
                                                 <div class="float-right mb-2 "> 
                                                   
                                                    <div class="button-list"> 
                                                   
                                                     <asp:Button ID="btnNextTolakan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="TOLAK" OnClick="btnNextTolakan_Click" />
                                                   
                                                    </div>
                                                    </div>
                                            </div> 
    
                </span></span> 
    
            </asp:Panel> 

            <asp:Panel ID="pnlAlasanTolak" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box"> 
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                          
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label86" runat="server" class="col-form-label">Alasan Tolak</asp:label>
																<asp:TextBox ID="txtalasantolak" enable="false" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server" >-</asp:TextBox> 
															 </div>
															
															 
                                                         
                                                        </div> 


													</div>
												</div>  

                                            </div>
                                         </div>        
                                        <div class="hidden-print mt-4"> 
                                                <div class="float-right mb-2 "> 
                                                   
                                                <div class="button-list"> 
                                                   
                                                    <asp:Button ID="BtnBackTolakan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="TOLAK" OnClick="BtnBackTolakan_Click" />
                                                   
                                                </div>
                                                </div>
                                        </div> 
                </span></span> 
    
            </asp:Panel> 
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnNext" /> 
                <asp:PostBackTrigger ControlID="btnSetuju" />
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="btnNext" /> 
                <asp:PostBackTrigger ControlID="btnNextLaba" />
                <asp:PostBackTrigger ControlID="btnNextPekerjaan" />
                <asp:PostBackTrigger ControlID="btnNextPendapatan" />
                <asp:PostBackTrigger ControlID="btnjaminan" />  
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