<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PencairanPinjamanAntrianAdmin.aspx.cs" Inherits="AR_Process_PencairanPinjamanAntrianAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Proses Antrian Admin</title>
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


       <style type="text/css">
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
    </style>
   
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

        
            function sweetAlertsave(btnSimpanTolakan,errMsg) {
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
             {
                $('#<%=txtalasantolak.ClientID%>').prop('maxlength', 150);
                
            }
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
            <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="PencairanPinjamanAntrian.aspx">Proses Antrian Admin</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Proses Antrian Admin</h4>
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

                            <h4 class="m-t-0 header-title">Data Pencairan Pinjaman Antrian</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                     <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="550px">
                                     
                                    <Columns>
                                       <asp:TemplateField HeaderText="No. Urut" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Tanggal Pengajuan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgldrop","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="No KTP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKTP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_identitas")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Rekening">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Tipe">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltipe" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tipeperpanjangan")%>'></asp:Label>
                                                <asp:Label ID="lblnopk" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_spk")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status Antrian">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatusantrian" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "statusantrian")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="No. Anggota">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Register" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegister" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Idpermohonan")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nama Anggota">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamanasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                      
                                       <asp:TemplateField visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblalamat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Permohonan Plafon">
                                            <ItemTemplate>
                                                <asp:Label ID="lblplafon" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plafon", "{0:N2}")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jangka">
                                            <ItemTemplate>
                                                <asp:Label ID="lbljangka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jangka")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lama hari tidak Cair">
                                            <ItemTemplate>
                                                <asp:Label ID="lblselisih" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "selisih")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                           
                                        <asp:TemplateField HeaderText="Action">
                                                                            <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Lanjutkan Teller" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnupdate" runat="server" data-toggle="tooltip" data-placement="left" title="Update Data" CommandName="UPDATEX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                      </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Cetak">
                                                                            <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                           
                                                                                            <asp:LinkButton ID="BtnBukti" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Cetak Bukti" CommandName="Bukti" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="BtnKwPinjaman" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Kwitansi Pinjaman" CommandName="Kwitansipinjaman" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="BtnAngsuran" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Angsuran Pinjaman" CommandName="Angsuranpinjaman" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Perjanjian Kredit" CommandName="PerjanjianKredit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Tanda Terima Anggota" CommandName="TandaTerimaAnggota" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Surat Tanda Terima" CommandName="TandaTerimaJaminan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                                                
                                                                            
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
                                                    <div class="ribbon ribbon-custom">DATA ANGGOTA</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDRegister" runat="server" />
                                                        <asp:HiddenField ID="HDStatusDebitur" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodePos" runat="server" />	 
                                                        <asp:HiddenField ID="HDKodeArea" runat="server" />
                                                        <asp:HiddenField ID="HDGender" runat="server" />
                                                        <asp:HiddenField ID="HDno_rekening" runat="server" />
                                                        <asp:HiddenField ID="HDLamaKerja" runat="server" />
                                                        <asp:HiddenField ID="HDNoBuktiSekarang" runat="server" />
                                                        <asp:HiddenField ID="HDNoBuktiSebelumnya" runat="server" />
                                                            
                                                            
                                                        <asp:HiddenField ID="HiddenField2" runat="server" />	 
                                                        <asp:HiddenField ID="HDNoSebelumnya" runat="server" />	 
                                                        <asp:HiddenField ID="HDid" runat="server" />
                                                        <asp:HiddenField ID="HDnopk" runat="server" /> 
                                                        <asp:HiddenField ID="HDnopksekarang" runat="server" /> 
                                                        <asp:HiddenField ID="HDNoPKSebelumnya" runat="server" />	

                                                            
                                                        <asp:HiddenField ID="HDHubunganKeluarga" runat="server" />	 
                                                        <asp:HiddenField ID="HDAlamatKeluarga" runat="server" />	 
                                                        <asp:HiddenField ID="HDKelurahanKeluarga" runat="server" />
                                                        <asp:HiddenField ID="HDKecamatanKeluarga" runat="server" /> 
                                                        <asp:HiddenField ID="HDKotaKeluarga" runat="server" />	
                                                        <asp:HiddenField ID="HDNamaKeluarga" runat="server" />	
                                                             
                                                        <asp:HiddenField ID="HDKodeproduk" runat="server" />	
                                                            
                                                            
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label43" runat="server" class="col-form-label">No. Anggota</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnoanggota"  Enabled="false" runat="server" required="required"></asp:TextBox>
                                                           <asp:TextBox CssClass="form-control" ID="txtnorektabungan" Visible="false"   runat="server" required="required"></asp:TextBox>
                                                                </div>
                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Anggota</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah"  Enabled="false" runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                            
                                                                        <asp:label ID="Label91" runat="server" class="col-form-label">Tanggal Bergabung</asp:label>
																        <asp:TextBox ID="txttglbergabung" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
												
                                                                </div>
                                                          
                                                        <div class="form-group col-md-4">
                                                           
                                                            
                                                                <asp:label ID="Label118" runat="server" class="col-form-label">Simpanan Pokok</asp:label>
																<asp:TextBox ID="txtsimpok" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
												
															
															 
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            
                                                      
                                                                <asp:label ID="Label126" runat="server" class="col-form-label">Simpanan Wajib</asp:label>
																<asp:TextBox ID="txtsimwa" CssClass="form-control"  Enabled="false" runat="server" ></asp:TextBox> 
                                                  

                                                           

                                                        </div>


													</div>
                                                  
                                                            <div class="form-row">
                                                                 <div class="form-group col-md-4">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">Pendidikan</asp:label>
																<asp:DropDownList ID="ddlpendidikanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                            </div>
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txthpktp" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);"  required="required" runat="server" ></asp:TextBox>
                                                            </div> 
                                                               <div class="form-group col-md-4">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">Email</asp:label>
																<asp:TextBox ID="txtemailsktp" Enabled="false" CssClass="form-control" MaxLength="100"   runat="server"  ></asp:TextBox>
                                                                 
                                                            
                                                            </div>
                                                            </div>
                                                            

                                                           <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Alamat</asp:label>
																<asp:TextBox ID="txtalamatktp"  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                                <asp:TextBox CssClass="form-control" ID="txtrtktp" visible="false"  runat="server"></asp:TextBox>
                                                                <asp:TextBox CssClass="form-control" ID="txtrwktp" visible="false"  runat="server"></asp:TextBox>
                                                           </div>
                                                             <div class="form-row">
                                                          <div class="form-group col-md-4">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtkelurahanktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Kecamatan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkecamatanktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Kota</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtkotaktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 
                                                                  
                                                            </div>
															
                                                            <div class="form-row">
                                                             
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label125" runat="server" class="col-form-label">Provinsi</asp:label>
																<asp:DropDownList ID="ddlpropinsiktp" enabled="false" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                            </div>
                                                                 <div class="form-group col-md-4">
                                                                <asp:label ID="Label132" runat="server" class="col-form-label">kode Pos</asp:label>
																 <asp:TextBox CssClass="form-control" ID="txtkodeposktp" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div>

                                                                </div>
                                                           
															
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">No. KTP</asp:label> 
                                                                <asp:TextBox ID="txtnoidentitasktp" Enabled="false" CssClass="form-control" MaxLength="16" onkeypress="return isnumerickey(event);"  required="required" runat="server"></asp:TextBox>
                                                           </div> 
                                                           
                                                          

                                                        <div class="form-group">
                                                        <!-- Simple card -->
                                                        <div class="card m-b-30">
                                                        <asp:Image ID="imgktp" runat="server" class="card-img-top img-fluid"   alt="Card image cap" />
                                                        </div>
                                                        </div>

                                                        
                                                            <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label127" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljenkelktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahirktp" CssClass="form-control" Enabled="false" required="required" runat="server" ></asp:TextBox>
															</div>

                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Tanggal Lahir</asp:label> 
                                                                <asp:TextBox CssClass="form-control" ID="txttgllahirktp"  Enabled="false" TextMode="DateTime" runat="server"></asp:TextBox> 
                                                            </div> 
                                                            </div>
                                                             <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">Agama</asp:label>
																<asp:DropDownList ID="ddlAgamaktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                           
                                                            </div>  

                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label128" runat="server" class="col-form-label">Status Pernikahan</asp:label>
																<asp:DropDownList ID="ddlstatusnikahktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>

                                                             </div>
                                                                  <div class="form-group col-md-4">
                                                                <asp:label ID="Label129" runat="server" class="col-form-label">Pekerjaan</asp:label>
																<asp:DropDownList ID="ddlpekerjaanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div>
                                                            </div>
                                                              <div class="form-row">
                                                                    <div class="form-group col-md-4">
                                                                    <asp:label id="Label130" runat="server"  class="col-form-label">Kewarganegaraan</asp:label>
                                                                      <asp:DropDownList ID="ddlkewarganegaraanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"></asp:DropDownList>
                                                           </div> 
                                                            <div class="form-group col-md-4">
                                                                    <asp:label id="Label22" runat="server"  class="col-form-label">Masa Berakhir KTP</asp:label>
                                                                    <asp:TextBox ID="txtmasaberakhirKTP" Enabled="false" CssClass="form-control" runat="server"  TextMode="DateTime"></asp:TextBox>
                                                           </div> 

                                                        </div> 
                                                            
                                                           
                                                            

                                                           

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                           
                                      
    
            </asp:Panel> 
                     
                       
          <asp:Panel ID="pnlPengajuan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">Detail Pencairan</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                       <form class="form-horizontal" role="form">
                                                            <asp:DropDownList ID="ddlmajuhari" visible="false" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                <asp:ListItem Value="0">0 Hari</asp:ListItem>
                                                                <asp:ListItem Value="1">1 Hari</asp:ListItem>
                                                                <asp:ListItem Value="2">2 Hari</asp:ListItem>
                                                                <asp:ListItem Value="3">3 Hari</asp:ListItem>
                                                                <asp:ListItem Value="4">4 Hari</asp:ListItem>
                                                                <asp:ListItem Value="5">5 Hari</asp:ListItem>
                                                                <asp:ListItem Value="6">6 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="7">7 Hari</asp:ListItem>
                                                                </asp:DropDownList>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">No Anggota</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblNoAnggota" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label" for="example-email">Nama Anggota</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblNama" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                    
                                                </div>
                                            </div>
                                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">No Rek. Pinjaman</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblrekpinjaman" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">No PK Kredit</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblpkkredit" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">KTP Penjamin</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblktppenjamin" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Nama Penjamin</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblnamapenjamin" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Nama Ibu Kandung</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblnamaibukandung" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                                <asp:label ID="Label89" runat="server" class="col-form-label">Jenis Permohonan</asp:label>
																
                                                                <asp:DropDownList ID="ddlpengajuan" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                    <asp:ListItem Value="B">Pinjaman Baru/Lama</asp:ListItem>
                                                                    <asp:ListItem Value="X">Perpanjangan</asp:ListItem>  
                                                                    </asp:DropDownList>
                                             </div>

                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Rekening Perpanjangan</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblrekeningperpanjangan" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Jaminan Surat</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbljaminansurat" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Agunan</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblagunan" multiline="true" rows="2" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Kondisi Agunan</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblkondisiagunan" multiline="true" rows="2" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Taksasi</label>
                                                <div class="col-8">
                                                    <asp:label ID="Lbltaksasi"  Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                    
                                            
                                                  <div class="form-group row">
                                                <label class="col-3 col-form-label">Account Officer</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblao" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Tanggal Realisasi</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbltglRealisasi" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                               <div class="form-group row">
                                                <label class="col-3 col-form-label">Jangka Waktu</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbljangkawaktu" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                              <div class="form-group row">
                                                <label class="col-3 col-form-label">Jatuh Tempo</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbljthtempo" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Bunga</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblbunga" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                             <div class="form-group row">
                                                <label class="col-3 col-form-label">Maju Hari Tagih</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblmajuhari" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>     
                                           
                                         <div class="form-group row">
                                                <label class="col-3 col-form-label">cadangan sebelumnya</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblcadangansebelumnya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                   
                                        </form>
                                                           
															
                                                         
                                                        </div>

                                                          <div class="form-group col-md-6">
                                                            <form class="form-horizontal" role="form">
                                                                <div class="form-group row">
                                                                <label class="col-3 col-form-label">Jenis Kredit</label>
                                                                <div class="col-8">
                                                                <asp:label ID="lbljeniskredit" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                                </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                <asp:label ID="lblangsuran" class="col-4 col-form-label" runat="server"></asp:label>
                                                                <div class="col-8">
                                                                <asp:label ID="lblangsuranbulan" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                                </div>
                                                                </div>

                                                                 <div class="form-group row">
                                                <label class="col-4 col-form-label">Besar Pinjaman</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblbesarpinjaman" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                             <div class="form-group row">
                                                <label class="col-3 col-form-label" >Potong / Biaya</label>
                                               <%-- <div class="col-10">
                                                    <asp:label ID="lblpotongbiaya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>--%>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Administrasi 3%</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbladministrasi" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                               <div class="form-group row">
                                                <label class="col-3 col-form-label">Materai</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblmaterai" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                             <div class="form-group row">
                                                <label class="col-4 col-form-label">Cadangan Angsuran</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblPotonganangsuran" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                             <div class="form-group row">
                                                <label class="col-4 col-form-label">Uang Muka</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbluangmuka" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Anggota</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblAnggota" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Total Potong /Biaya </label>
                                                <div class="col-8">
                                                    <asp:label ID="lblTotalPotongbiaya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Yang di terima Anggota </label>
                                                <div class="col-8">
                                                    <asp:label ID="lblygditerimaanggota" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Pengembalian Biaya Cadangan </label>
                                                <div class="col-8">
                                                    <asp:label ID="lblkembalicadangan" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                                            </form>
                                                       
                                                            </div>
                                                           
															
                                                         
                                                        </div>
                                                        


													</div>
												</div>  

              </div>
                                         </div>        
                  
                                <div class="hidden-print mt-4">
                                    <div class="row">
                                    <div class="col-md-12">
                                    <div class="card-box">
                                    <div class="card-box table-responsive">
                                    <asp:Button ID="btnCancel" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnlanjutkan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="LANJUTKAN KE TELLER" OnClick="btnlanjutkan_Click" />
                                    <asp:Button ID="btnTolak" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BATALKAN ANTRIAN" OnClick="btnSimpanTolakan_Click" />
                                    </div>
                                    </div>
                                    </div>
                                    </div> 
                                </div>            
        
            </asp:Panel> 



        <asp:Panel ID="Pnlupdatedata" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">Update Data Jaminan dan Anggota</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                       <form class="form-horizontal" role="form">
                                                
                                       
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">No Rek. Pinjaman</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblrekening" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                           <div class="form-group row">
                                                <label class="col-3 col-form-label">Jaminan Surat</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbljaminansuratx" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                                <div class="form-group row">
                                                                <asp:label ID="Label141" runat="server" class="col-3 col-form-label">Detail Jaminan</asp:label>
													<div class="col-8">		
                                                    <asp:TextBox ID="txtketeranganJaminan"  CssClass="form-control"  TextMode="MultiLine" Rows="7" runat="server" data-style="btn-custom" MaxLength="250" ></asp:TextBox>          
                                                    </div>
                                                    </div>
                                             <div class="form-group row">
                                                <label class="col-3 col-form-label">No Anggota</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblanggotax" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label" for="example-email">Nama Anggota</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblnamaanggota" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                    
                                                </div>
                                            </div>

                                                            <div class="form-group row">
                                                                <asp:label ID="Label10" runat="server" class="col-3 col-form-label">No.HP</asp:label>
																<div class="col-8">
                                                                <asp:TextBox ID="txtnohpx" CssClass="form-control" Enabled="true" onkeypress="return isnumerickey(event);"  required="required" runat="server" ></asp:TextBox>
                                                                </div>
                                                                </div> 
                                                               <div class="form-group row">
                                                                <asp:label ID="Label12" runat="server" class="col-3 col-form-label">Email</asp:label>
																<div class="col-8">
                                                                   <asp:TextBox ID="txtemailx" Enabled="true" CssClass="form-control" MaxLength="100"   runat="server"  ></asp:TextBox>
                                                                </div>
                                                            </div>

                                   
                                        </form>
                                                           
															
                                                         
                                                        </div>

                                                          
                                                           
															
                                                         
                                                        </div>
                                                        


													</div>
												</div>  

              </div>
                                         </div>        
                  
                                <div class="hidden-print mt-4">
                                    <div class="row">
                                    <div class="col-md-12">
                                    <div class="card-box">
                                    <div class="card-box table-responsive">
                                    <asp:Button ID="Button1" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />
                                    <asp:Button ID="Button2" align="right" class="btn btn-success waves-light text-white" runat="server" Text="Update Data" OnClick="btnupdate_Click" />
                                    
                                    </div>
                                    </div>
                                    </div>
                                    </div> 
                                </div>            
        
            </asp:Panel>
       
                 
             

            <asp:Panel ID="pnlAlasanTolak" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box"> 
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                          
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label86" runat="server" class="col-form-label">Alasan Pembatalan</asp:label>
																<asp:TextBox ID="txtalasantolak" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server" >-</asp:TextBox> 
															 </div>
															
															 
                                                         
                                                        </div> 


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackTolak" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackTolak_Click" />
                                                        

                                                 </div> 
                                                 <div class="float-right mb-2 "> 
                                                   
                                                        <div class="button-list"> 
                                                   
                                                          <asp:Button ID="btnSimpanTolakan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="PROSES PEMBATALAN" OnClick="btnSimpanTolakan_Click"   OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menolak data ini ?');"/> 
                                                     
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
              
                <asp:PostBackTrigger ControlID="btnTolak" />
                <asp:PostBackTrigger ControlID="btnSimpanTolakan" />
                <%--<asp:PostBackTrigger ControlID="btnSetuju" />--%>
                <asp:PostBackTrigger ControlID="GRData" /> 
                    
             
                 
                <asp:PostBackTrigger ControlID="btnCancel" />
               

                 
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
