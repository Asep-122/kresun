<%@ Page Language="C#"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="PencairanPinjaman.aspx.cs" Inherits="AR_Process_PencairanPinjaman" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pencairan Pinjaman</title>
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
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };
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
                        btnSetuju.disabled = true;
                        return true;
                  
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
                }
        }

        function sweetAlertbatal(btnBatal, errMsg) {
            if (btnBatal.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnBatal.dataset.confirmed = false;
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
                        btnBatal.dataset.confirmed = true;
                        btnBatal.click();

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
                                    <li class="breadcrumb-item"><a href="PencairanPinjaman.aspx">Pencairan Pinjaman</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Pencairan Pinjaman</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
              <asp:Panel ID="pnlFilter"  runat="server" visible = "False">
                       <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-form-label">Search By</label>
                                                               
                                                                    <div class="form-group col-md-6">
                                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                                            <asp:ListItem Value=" and no_nasabah ">No. Anggota</asp:ListItem>    
                                                                            <asp:ListItem Value=" and nama ">Nama Anggota</asp:ListItem> 
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                                                                        <asp:Button ID="lnkCari" class="btn btn-success waves-light text-white" runat="server" Text="Cari" OnClick="lnkCari_Click" />
                                                                    </div>
                                                                    
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>

            </asp:Panel>
                  
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">
                             <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Reload Data" OnClick="btnAdd_Click" />
                            <h4 class="m-t-0 header-title">Data Pencairan Pinjaman</h4>    
                             <asp:TextBox CssClass="form-control" ID="txtnonasabahxt" visible="false" MaxLength = "250" runat="server" required="required"></asp:TextBox>
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                      <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowCommand="GRData_RowCommand" AutoGenerateColumns="False" Width="550px" OnSelectedIndexChanged="GRData_SelectedIndexChanged">
                                           
                                         <Columns>
                                        <asp:TemplateField HeaderText="No. Urut">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Kode Produk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamacabang" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Debitur">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldebitur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_debitur")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Tipe">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltipe" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tipeperpanjangan")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Tgl Realisasi">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRealisasi" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_drop","{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="No. Rekening">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnorekening" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening1")%>'></asp:Label>
                                             </ItemTemplate>
                                            <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No. PK">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnopk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "pkredit")%>'></asp:Label>
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

                                             <asp:TemplateField HeaderText="AO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAO2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kd_reff2")%>'></asp:Label>
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
																<asp:DropDownList ID="ddlPenggunaan" visible="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                <asp:TextBox CssClass="form-control" ID="txttaksasi" Visible="false" runat="server" required="required"></asp:TextBox>
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
																<asp:TextBox ID="txthpktp" CssClass="form-control" Enabled="false" onkeypress="return isnumerickey(event);"  runat="server" ></asp:TextBox>
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
                                                         
                                                            </div>
                                                            <div class="form-row">
                                                                <div class="form-group col-md-4">
                                                                    <asp:label ID="Label158" runat="server" class="col-form-label">Pekerjaan</asp:label>
																    <asp:DropDownList ID="ddlpekerjaanktp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>

                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label159" runat="server" class="col-form-label">Sektor Usaha</asp:label>
																<asp:DropDownList ID="ddlSektorUsahaKtp" Enabled="false" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>

                                                             </div>
                                                                  <div class="form-group col-md-4">
                                                                        <asp:label ID="Label160" runat="server" class="col-form-label">Jenis Usaha</asp:label>
																        <asp:TextBox ID="txtjenisusahaKtp" Enabled="false" CssClass="form-control" Text="" MaxLength="50"  runat="server"  ></asp:TextBox>
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
                                                             
                                                            <div class="form-group">
                                                                <asp:label ID="Label12" runat="server" class="col-form-label">No. Anggota CBS</asp:label>
																<asp:TextBox ID="txtanggotacbs" Enabled="false" CssClass="form-control" MaxLength="30" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                                                                           
                                                            </div>
                                                            <div class="form-group" runat="server" visible="false">
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">No. Rekening CBS</asp:label>
																<asp:TextBox ID="txtrekeningcbs" CssClass="form-control" MaxLength="20" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                                                                           
                                                            </div>
                                                           
                                                            

                                                           

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                               
                                               
    
            </asp:Panel> 
          
               
            <asp:Panel ID="pnlUsaha" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENGAJUAN KREDIT ANGGOTA</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         

                                                       <asp:HiddenField ID="HDJenisPinjaman" runat="server"/> 
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label89" runat="server" class="col-form-label">Jenis Permohonan</asp:label>
																
                                                                <asp:DropDownList ID="ddlpengajuan" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                    <asp:ListItem Value="B">Anggota Baru</asp:ListItem>
                                                                    <asp:ListItem Value="X">Anggota Lama</asp:ListItem>  
                                                                    </asp:DropDownList>
                                             </div>

                                                            <div class="form-group">
                                                                <label class="col-3 col-form-label">Rekening Perpanjangan</label>
                                                                <div class="col-8">
                                                                    <asp:label ID="lblrekeningperpanjangan" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label8" Visible="true" runat="server" class="col-form-label">No. Rekening</asp:label>
																<asp:TextBox ID="txtnorekening" Enabled="false"   CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>
                                                            

                                                            <div class="form-group">
                                                                <asp:label ID="Label9" Visible="true" runat="server" class="col-form-label">No. PK Kredit</asp:label>
																<asp:TextBox ID="txtpkredit" Enabled="false"   CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label30" Visible="false" runat="server" class="col-form-label">No. Rekening Lama</asp:label>
																<asp:TextBox ID="txtnoreklama" Enabled="false"  Visible="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label34" runat="server" class="col-form-label">Jenis Kredit</asp:label>
																<asp:TextBox ID="txtjeniskredit" Enabled="false"  CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label35" runat="server" class="col-form-label">Tanggal Realisasi</asp:label>
                                                                 <asp:TextBox CssClass="form-control" Enabled="false"  ID="txttglrealisasi" TextMode="DateTime" runat="server"></asp:TextBox>
                                                            </div>   

                                                            <div class="form-group">
                                                                <label runat="server" class="col-form-label">Jenis Tagihan</label>
                                                                <%--<asp:DropDownList ID="ddlbycad" Required="False" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlbycad_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                <asp:DropDownList ID="ddljenistagihan" enabled="false" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" >
                                                                <asp:ListItem Value="0">System Lama</asp:ListItem>
                                                                <asp:ListItem Value="1">Tagihan Dengan Collection</asp:ListItem>                                                               
                                                                <asp:ListItem Value="2">Bayar Di Cabang</asp:ListItem>   
                                                                <asp:ListItem Value="3">Bayar Di Cabang</asp:ListItem>    
                                                                </asp:DropDownList>
                                                            
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label37" Visible="false" runat="server" class="col-form-label">Jenis Pinjaman</asp:label>
																<asp:TextBox ID="txtjenispinjaman" Visible="false" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label55" Visible="false" runat="server" class="col-form-label">Program Pinjaman</asp:label>
																<asp:TextBox ID="txtprogrampinjaman" Visible="false" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label56" runat="server" Visible="false" class="col-form-label">Sumber Dana</asp:label>
																<asp:TextBox ID="txtsumberdana" Visible="false" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
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
                                                                <asp:label ID="Label65" runat="server" Visible="false" class="col-form-label">Ref Non AO</asp:label>
																<asp:TextBox ID="txtNonAO" Enabled ="false" Visible="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label67" runat="server" Visible="false" class="col-form-label">No Tabungan Lain</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txttablain" Enabled="false"  runat="server" ></asp:TextBox>
                                                            </div> 
                                                           <asp:label ID="Label103" runat="server" class="col-form-label">Maju Hari Tagih</asp:label>
                                                                <asp:DropDownList ID="ddlmajuhari" Enabled="false" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                <asp:ListItem Value="0">0 Hari</asp:ListItem>
                                                                <asp:ListItem Value="1" >1 Hari</asp:ListItem>
                                                                <asp:ListItem Value="2">2 Hari</asp:ListItem>
                                                                <asp:ListItem Value="3">3 Hari</asp:ListItem>
                                                                <asp:ListItem Value="4">4 Hari</asp:ListItem>
                                                                <asp:ListItem Value="5">5 Hari</asp:ListItem>
                                                                <asp:ListItem Value="6">6 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="7">7 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="8">8 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="9">9 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="10">10 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="11">11 Hari</asp:ListItem>
                                                                <asp:ListItem Value="12">12 Hari</asp:ListItem>
                                                                <asp:ListItem Value="13">13 Hari</asp:ListItem>
                                                                <asp:ListItem Value="14">14 Hari</asp:ListItem>
                                                                <asp:ListItem Value="15">15 Hari</asp:ListItem>
                                                                <asp:ListItem Value="16">16 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="17">17 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="18">18 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="19">19 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="20">20 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="21">21 Hari</asp:ListItem>
                                                                <asp:ListItem Value="22">22 Hari</asp:ListItem>
                                                                <asp:ListItem Value="23">23 Hari</asp:ListItem>
                                                                <asp:ListItem Value="24">24 Hari</asp:ListItem>
                                                                <asp:ListItem Value="25">25 Hari</asp:ListItem>
                                                                <asp:ListItem Value="26">26 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="27">27 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="28">28 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="29">29 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="30">30 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="31">31 Hari</asp:ListItem> 
                                                                <asp:ListItem Value="32">32 Hari</asp:ListItem> 
                                                                </asp:DropDownList>
                                                            
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                             <div class="form-group">
                                                                <asp:label ID="Label66" runat="server" Visible="false" class="col-form-label">Sektor Ekonomi</asp:label>
																<asp:TextBox ID="txtsektorekonomi" Visible="false" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>
                                                             

                                                            <div class="form-group">
                                                                <asp:label ID="Label70" runat="server" class="col-form-label">Biaya Cadangan</asp:label>
																<asp:TextBox ID="TextBox3" CssClass="form-control" Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                             <div class="form-group">
                                                                <asp:label ID="Label71" runat="server" Visible="false" class="col-form-label">Nilai Taksasi</asp:label>
																<asp:TextBox ID="TextBox5" CssClass="form-control" Visible="false" Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">Jatuh Tempo</asp:label>
																<asp:TextBox ID="txtjttempo" CssClass="form-control"  Enabled="false"    runat="server" ></asp:TextBox>
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label73" runat="server" Visible="false" class="col-form-label">Angsuran Dimuka</asp:label>
																<asp:TextBox ID="txtangsurandimuka" Visible="false" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>  

                                                            <div class="form-group">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Biaya Materai</asp:label>
																<asp:TextBox ID="txtbymaterai" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
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
                                                                <asp:label ID="Label61" runat="server" Visible="false" class="col-form-label">Admin Pencairan</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Visible="false" Enabled ="false" ID="txtadminPencairan"    runat="server" ></asp:TextBox>     
                                                            </div> 
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label62" runat="server" Visible="false" class="col-form-label">Admin Angsuran</asp:label>
	                                                            <asp:TextBox CssClass="form-control" Visible="false" Enabled ="false" ID="txtadminangsuran"   runat="server" ></asp:TextBox>     
                                                            </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label7" runat="server"  class="col-form-label">Biaya Keanggotaan</asp:label>
																<asp:TextBox ID="txtanggota"  CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label14" runat="server"  class="col-form-label">Simpanan Wajib Perpanjangan</asp:label>
																<asp:TextBox ID="txtsimpananwajib"  CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>
                                
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label63" runat="server" Visible="false" class="col-form-label">Sifat</asp:label>
																<asp:TextBox ID="txtsifat" Enabled ="false" Visible="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															 </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label64" runat="server" class="col-form-label">AO</asp:label>
																<asp:TextBox ID="txtAO" Enabled ="false" CssClass="form-control"  runat="server" ></asp:TextBox> 
															  </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label75" runat="server" Visible="false" class="col-form-label">Biaya Asuransi</asp:label>
																<asp:TextBox ID="txtbyasuransi" Visible="false" CssClass="form-control" Enabled="false"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label76" runat="server" Visible="false" class="col-form-label">Biaya Pelunasan</asp:label>
																<asp:TextBox ID="txtbypelunasan" Visible="false" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                              <div class="form-group">
                                                                <asp:label ID="Label77" runat="server" Visible="false" class="col-form-label">Biaya Notaris</asp:label>
																<asp:TextBox ID="txtbynotaris" Visible="false" CssClass="form-control"  Enabled="false"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 
                                                             <div class="form-group">
                                                                <asp:label ID="Label78" runat="server" Visible="false" class="col-form-label">Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox14" Visible="false" CssClass="form-control" Enabled="false"      onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 
                                                            <div class="form-group">
                                                                <asp:label ID="Label79" runat="server" Visible="false" class="col-form-label">Nominal Blokir Angs</asp:label>
																<asp:TextBox ID="TextBox15" Visible="false" CssClass="form-control"  Enabled="false"     onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" Visible="false" class="col-form-label">Jenis Blokir</asp:label>
																<asp:TextBox ID="txtjenisblokir" Visible="false" CssClass="form-control" Enabled ="false" runat="server" ></asp:TextBox> 
															 </div>

                                                        </div>


													</div>
												</div>  

                                            </div>
                                         </div>        
                  
       
    
            </asp:Panel> 

<%--<asp:Panel ID="pnlPengajuanxxx" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">Detail Pencairan</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                       <form class="form-horizontal" role="form">
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
                                                <label class="col-3 col-form-label">Type Pinjaman</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbltipe" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">No Rekening Lama</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblrekeninglama" Visible="true" runat="server" class="col-form-label"></asp:label>
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
                                                                <asp:label ID="lblangsuran" class="col-3 col-form-label" runat="server"></asp:label>
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
                                                <div class="col-10">
                                                    <asp:label ID="lblpotongbiaya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
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
                                                <label class="col-3 col-form-label">Cadangan Angsuran</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblPotonganangsuran" Visible="true" runat="server" class="col-form-label"></asp:label>
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
                                                            </form>
                                                       
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

                                                        <asp:Button ID="btnSetuju"  align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="SAVE" OnClick="btnSetuju_Click" Width="65px" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menyetujui data ini ?');"/>
                                                    </div>
                                            </div>            
    
                </span></span> 
                               
                                               
    
       </asp:Panel>--%>

                 <asp:Panel ID="pnlPengajuan" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">Detail Pencairan</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                       <form class="form-horizontal" role="form">
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
                                                <label class="col-3 col-form-label">Jenis Permohonan</label>
                                                <div class="col-8">
                                                    <%--<asp:label ID="lbltipe" Visible="true" runat="server" class="col-form-label"></asp:label>--%>
                                                     <asp:DropDownList ID="ddljenispermohonan" Enable="false" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" >
                                                                                    <asp:ListItem Value="A">Normal</asp:ListItem>
                                                                                    <asp:ListItem Value="B">UKM</asp:ListItem> 
                                                                        </asp:DropDownList>
                                                
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Kelompok Agunan</label>
                                                <div class="col-8">
                                                    <%--<asp:label ID="lbltipe" Visible="true" runat="server" class="col-form-label"></asp:label>--%>
                                                     <asp:DropDownList ID="ddlkelompokAgunan" Enabled="false" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" >
                                                                                    <asp:ListItem Value="A">SHM</asp:ListItem>
                                                                                    <asp:ListItem Value="B">GIRIK (AJB)</asp:ListItem>  
                                                                                    <asp:ListItem Value="C">BPKB</asp:ListItem>  
                                                                                    <asp:ListItem Value="D">TABUNGAN/DEPOSITO</asp:ListItem>
                                                                                    <asp:ListItem Value="E">ELEKTRONIK</asp:ListItem>
                                                                                    <asp:ListItem Value="F">LOGAM MULIA</asp:ListItem>
                                                                                    <asp:ListItem Value="G">INVENTORY</asp:ListItem>
                                                                                    <asp:ListItem Value="Z">Agunan</asp:ListItem>
                                                                        </asp:DropDownList>
                                                
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Tujuan Pinjaman</label>
                                                <div class="col-8">
                                                    <%--<asp:label ID="lbltipe" Visible="true" runat="server" class="col-form-label"></asp:label>--%>
                                                      <asp:DropDownList ID="ddltujuanpinjaman" Enabled="false" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" >
                                                                                    <asp:ListItem Value="A">Modal Usaha</asp:ListItem>
                                                                                    <asp:ListItem Value="B">Investasi</asp:ListItem> 
                                                                                    <asp:ListItem Value="C">Konsumtif</asp:ListItem>  
                                                                                    <asp:ListItem Value="Z">Lainnya</asp:ListItem>  
                                                                        </asp:DropDownList>
                                                
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Type Pinjaman</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbltipe" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">No Rekening Lama</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblrekeninglama" Visible="true" runat="server" class="col-form-label"></asp:label>
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
                                            <div class="form-group row" runat="server" id="discountmingguan">
                                                <label class="col-3 col-form-label">Discount Angsuran</label>
                                                <div class="col-8">
                                                    <asp:label ID="txtdiscountangsuran" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                          
                                             <div class="form-group row">
                                                <label class="col-3 col-form-label">Saldo Simpanan Pokok</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblsaldosimpok" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                             <div class="form-group row">
                                                <label class="col-3 col-form-label">Saldo Simpanan Wajib</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblsaldoSimwa" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                              <div class="form-group row">
                                                <label class="col-3 col-form-label">Type Biaya Keanggotaan</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblnamaTypebiaya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                    <asp:label ID="LblTypebiaya" Visible="false" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                             <div class="form-group row">
                                                <label class="col-3 col-form-label">Maju Hari Tagih</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblmajuhari" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                             <div class="form-group row">
                                                <label class="col-3 col-form-label">Tgl Hari Tagih</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblTglhariTagih" Visible="true" runat="server" class="col-form-label"></asp:label>
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
                                                                <asp:label ID="lblangsuran" class="col-3 col-form-label" runat="server"></asp:label>
                                                                <div class="col-8">
                                                                <asp:label ID="lblangsuranbulan" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                                </div>
                                                                </div>

                                             <div class="form-group row">
                                                <label class="col-4 col-form-label">Besar Pinjaman</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblbesarpinjaman" Visible="true" Font-Bold="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                             <div class="form-group row">
                                                <label class="col-4 col-form-label">cadangan Sebelumnya</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblcadangansebelumnya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                           <div class="form-group row">
                                                <label class="col-4 col-form-label">No Rekening Cadangan</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblrekeningCadangan" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                            <label class="col-4 col-form-label">Jenis Biaya Cadangan</label>
                                            <div class="col-8">
                                                        <asp:DropDownList ID="ddlbycad" runat="server" CssClass="selectpicker show-tick" data-style="btn-custom" Enabled="false">
                                                        <asp:ListItem Value="0">Tanpa Cadangan</asp:ListItem>
                                                        <asp:ListItem Value="1">Dengan Cadangan Potong Cadangan Sebelumnya</asp:ListItem>   
                                                        <asp:ListItem Value="2">Dengan Cadangan Tanpa Potong Cadangan Sebelumnya</asp:ListItem>  
                                                        </asp:DropDownList>
                                                </div>
                                            
                                            </div> 

                                             <div class="form-group row">
                                                <label class="col-4 col-form-label" >Potong / Biaya</label>
                                               <%-- <div class="col-10">
                                                    <asp:label ID="lblpotongbiaya" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>--%>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Biaya Administrasi 3%</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbladministrasi" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                               <div class="form-group row">
                                                <label class="col-4 col-form-label">Biaya Materai</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblmaterai" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                             <div class="form-group row">
                                                <label class="col-4 col-form-label">Biaya Cadangan</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblPotonganangsuran" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                       
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Biaya Uang Muka</label>
                                                <div class="col-8">
                                                    <asp:label ID="lbluangmuka" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Biaya Keanggotaan</label>
                                                <div class="col-8">
                                                    <asp:label ID="LblAnggota" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Simpanan Wajib Perpanjangan</label>
                                                <div class="col-8">
                                                    <asp:label ID="lblsimpananwajib" Visible="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Total Potong /Biaya </label>
                                                <div class="col-8">
                                                    <asp:label ID="lblTotalPotongbiaya" Visible="true" Font-Bold="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-4 col-form-label">Yang di terima Anggota </label>
                                                <div class="col-8">
                                                    <asp:label ID="lblygditerimaanggota" Visible="true" Font-Bold="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                            
                                          <div class="form-group row">
                                                <label class="col-4 col-form-label">Pengembalian Biaya Cadangan </label>
                                                <div class="col-8">
                                                    <asp:label ID="lblkembalicadangan" Visible="true" Font-Bold="true" runat="server" class="col-form-label"></asp:label>
                                                </div>
                                            </div>
                                                            </form>
                                                       
                                                            </div>
                                                           
															
                                                         
                                                        </div>
                                                        


													</div>
												</div>  

              </div>

                        <div class="hidden-print mt-4">
                                             <div class="row">
                                   
                                    <div class="button-list">
                                        
                                   <asp:Button ID="btnSetuju" CssClass="btn btn-success waves-light text-white" runat="server" Text="SAVE" OnClick="btnSetuju_Click"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menyetujui data ini ?');"/>
                                      <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnBatal"  CssClass="btn bg-danger waves-light text-white" runat="server" Text="BATALKAN ANTRIAN TELLER" OnClick="btnBatal_Click" OnClientClick="return sweetAlertbatal(this,'Apakah anda yakin ingin batalkan Antrian data ini ?');"/>
                                      
                                    </div>
                                    </div> 




                                       

                                            </div>    
                                 
                                            
    

                     
                </span></span> 
                               
                                               
    
       </asp:Panel>

            <asp:Panel ID="pnlPengajuanx" Visible="false" runat="server">
              <div class="clearfix">                         
                <div class="row">
                    <div class="col-12">
                        <div class="card-box">
                            <h4 class="m-t-0 header-title">Detail Pencairan</h4>
                          
                            <div class="row">
                                <div class="col-12">
                                    <div class="p-20">
                                     
                                        
                                    </div>
                                </div>

                            </div>
                            <!-- end row -->

                        </div> <!-- end card-box -->
                    </div><!-- end col -->
                </div>
                <!-- end row -->


                     
               </div>        
                  
                          
    
            </asp:Panel> 
            
                     
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>  
                <asp:PostBackTrigger ControlID="btnSetuju" />
                <asp:PostBackTrigger ControlID="GRData" /> 

                 
                   <%--<asp:PostBackTrigger ControlID="btnBackUsaha" />--%>
                <%--<asp:PostBackTrigger ControlID="btnBackPekerjaan" />--%>
                <%--<asp:PostBackTrigger ControlID="btnBackTambahan" />--%>
                <%--<asp:PostBackTrigger ControlID="btnBackPendapatan" />--%> 
                 
                <asp:PostBackTrigger ControlID="btnCancel" />
                <%--<asp:PostBackTrigger ControlID="btnNext" />--%> 
                <%--<asp:PostBackTrigger ControlID="btnNextLaba" />--%>
          <%--      <asp:PostBackTrigger ControlID="btnNextPekerjaan" />
                <asp:PostBackTrigger ControlID="btnNextPendapatan" />  --%>
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

