<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrasiNasabahInternal.aspx.cs" Inherits="FA_Master_RegistrasiNasabahInternal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Registrasi Anggota</title>
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


   <script>
        function simpanalert(ctl, event) { //simpanalert cek OnClientClick dan fungsi di backend bisa juga bisa buat alert delete update atau insert. selebihnya hanya Allah SWT yang berkehendak
            var defaultAction = $(ctl).prop("href");
            var ambilnasabah=document.getElementById("txtjenisgol").value; //txtjenisgol di ganti ke id nasabah
            event.preventDefault();
            swal({
                title: "Data berhasil disimpan",
                text: 'Nomor nasabah anda :' + ambilnasabah ,
                type: "success", //type warning
                position: "top-end", //posisi center
                showConfirmButton: false, // kalo mau ada button yes or no bisa di true
                timer: 1500 //bisa coba atur lama disini
            },
                function () {
                window.location.href = defaultAction;
                return true;
                });
        }
   
    </script>
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
                        
                          if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                          }
                    }
                });
            }, 0);

        } function Resize() {
            setTimeout(function (e) {
                //  alert('z');
                window.top.cekResize();
                var y;
                y = window.parent.document.getElementById('txtScrollY').value;
                setTimeout(function (e) { window.parent.scroll(0, y); }, 50);
            }, 50);

        }


         function CheckFileExistence() {

            var filePath = document.getElementById('<%= this.fileuploadktp.ClientID %>').value;

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
                        btnSimpan.disabled = true;
                        return true;

                  
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

       
       
        
       
        function redirect() {
            location.href = 'RegistrasiNasabahBlock.aspx ';
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
             $('#txttglterbitidentitas').datepicker({
                format: 'dd-MM-yyyy',
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
            
            {
            $('#<%=txtjenisusaha.ClientID%>').prop('maxlength', 50);
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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="RegistrasiNasabah.aspx">Registrasi Anggota Internal</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Registrasi Anggota Internal</h4>
                        </div>
                    </div>
                </div>
                 <asp:Panel ID="pnlHeader" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Registrasi Anggota Internal" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter"  runat="server" Visible="false">
                       <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-form-label">Search By</label>
                                                               
                                                                    <div class="form-group col-md-6">
                                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                                            <asp:ListItem Value=" and x.no_nasabah_cbs ">Anggota CBS</asp:ListItem>
                                                                            <asp:ListItem Value=" and x.no_nasabah ">Anggota ID</asp:ListItem>
                                                                            <asp:ListItem Value=" and x.nama_nasabah ">Anggota Name</asp:ListItem>
                                                                            <asp:ListItem Value=" and x.no_identitas ">Anggota KTP</asp:ListItem>
                                                                            <asp:ListItem Value=" and x.no_ktp_pasangan ">Anggota KTP Pasangan</asp:ListItem>
                                                                            <asp:ListItem Value=" and x.ktp_penjamin ">Anggota KTP Penjamin</asp:ListItem>
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
                 <!-- Panel Header -->
               
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server" Visible="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box ribbon-box">
                            <h4 class="m-t-0 header-title">Data Registrasi Anggota Internal</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="887px">
                                <Columns>
                                     <asp:TemplateField HeaderText="No Anggota">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Anggota CBS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnonasabahCBS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah_cbs")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Nama Anggota">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamat1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. Identitas">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamat2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_identitas")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="No. Identitas Pasangan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpasangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_ktp_pasangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Identitas Penjamin">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpasanganpenjamin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ktp_penjamin")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alamat KTP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblalamat3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "alamat_rumah_ktp1")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="No. HP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnohp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_hp")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>      
                                      <asp:TemplateField HeaderText="AO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_AO")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus_Nasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_Nasabah")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>   
                                    
                                                                                                        
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderStyle CssClass="text-center" Width="10%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Edit" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                            <%--<asp:LinkButton ID="btndelete" runat="server" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="DELX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>--%>
                                            <asp:LinkButton ID="btndelete" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Delete" OnClientClick="return sweetAlertConfirm(this,'Apakah anda yakin ingin menghapus data ini ?');" CommandName="DELX" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-close-circle"></i></asp:LinkButton>
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
                       <div class="row">                          
                               <asp:HiddenField ID="HDNoNasabah" runat="server" />	 
                                                        <asp:HiddenField ID="HDNoSebelumnya" runat="server" />	 
                                                        <asp:HiddenField ID="HDState" runat="server" />
                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA DIRI ANGGOTA</div> 
                                                        <p class="text-muted font-14 m-b-30"></p>  
                                                        <div class="form-group" runat="server" visible="false">
                                                                <asp:label ID="Label8" runat="server" class="col-form-label">No.Anggota CBS</asp:label>
																<asp:TextBox ID="txtanggotacbs" CssClass="form-control" MaxLength="15" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                                                                           
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Lengkap</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah" MaxLength="70"   runat="server" Text="" ></asp:TextBox>
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label" Visible="false">Nama Panggilan</asp:label>
																<asp:TextBox ID="txtnamapanggilan" Visible="false" CssClass="form-control" MaxLength="40"  Text="-" runat="server"></asp:TextBox> 
															                                                               
                                                            </div>
                                                            <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljenkel" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                          </div>
															
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahir" CssClass="form-control" Text="" MaxLength="30"  runat="server"  ></asp:TextBox>
															</div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label3ccc" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
                                                                  <asp:TextBox CssClass="form-control" ID="txttgllahir"  TextMode="DateTime" runat="server"  ></asp:TextBox>
                                                                                  
                                                            </div> 
                                                            </div>
                                                           <div class="form-row">
                                                             <div class="form-group col-md-5">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">Agama</asp:label>
																<asp:DropDownList ID="ddlAgama" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                           
                                                            </div>  

                                                             <div class="form-group col-md-5">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Status Pernikahan</asp:label>
																<asp:DropDownList ID="ddlstatusnikah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>

                                                             </div>
                                                               
                                                            </div>
                                                              

                                                            <div class="form-row">
                                                                <div class="form-group col-md-5">
                                                                    <asp:label ID="Label45" runat="server" class="col-form-label">Pekerjaan</asp:label>
																    <asp:DropDownList ID="ddlPekerjaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                </div>

                                                             <div class="form-group col-md-5">
                                                                <asp:label ID="Label16" runat="server" class="col-form-label">Sektor Usaha</asp:label>
																<asp:DropDownList ID="ddlSektorUsaha" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>

                                                             </div>
                                                                  <div class="form-group col-md-5">
                                                                        <asp:label ID="Label17" runat="server" class="col-form-label">Jenis Usaha</asp:label>
																        <asp:TextBox ID="txtjenisusaha" CssClass="form-control" Text="" MaxLength="50"  runat="server"  ></asp:TextBox>
                                                                </div>
                                                            </div>


                                                             <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label14" runat="server" class="col-form-label">Pendidikan</asp:label>
																<asp:DropDownList ID="ddlpendidikan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                            </div>   

                                                             <div class="form-group col-md-6">
                                                                <asp:label ID="Label14xxx" runat="server" class="col-form-label">Kewarganegaraan</asp:label>
                                                                <div class="form-row">
                                                                    <div class="form-group col-md-6">
                                                                        <asp:DropDownList ID="ddlkewarganegaraan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlkewarganegaraan_SelectedIndexChanged" ></asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-5">
                                                                        <asp:TextBox ID="txtkewarganegaraan" MaxLength="30" CssClass="form-control"  runat="server" ></asp:TextBox>
                                                           
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                                  
                                                          </div>

                                                          
                                                             <div class="form-row">
                                                            <div class="form-group col-md-5">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txthp" CssClass="form-control" MaxLength="13" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-group col-md-5">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">Email</asp:label>
																<asp:TextBox ID="TxtEmail" CssClass="form-control" MaxLength="40" Text="0"   runat="server" ></asp:TextBox>
                                                                             
                                                            </div>
                                                               </div>                                                         
               
                                                            
                                 
                                                


                                  
                                </div>                                                    
                            </div>
                           </div>

                           

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">DATA KTP PRIBADI</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                           <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Alamat Rumah Sesuai KTP</asp:label>
																<asp:TextBox ID="txtalamatsesuaiktp1"   CssClass="form-control" Text="" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                               
                                                            
                                                            </div>

                                                            <div class="form-row">
                                                                 <div class="form-group col-md-6">
                                                                <asp:label ID="Label23" runat="server" class="col-form-label">Kabupaten</asp:label>
																<asp:TextBox ID="txtkabupaten" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
														                  
                                                            </div>
                                                            
                                                            
															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label25" runat="server" class="col-form-label">RT KTP</asp:label>
																<asp:TextBox ID="txtrtktp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"    runat="server" ></asp:TextBox>
											                             
                                                            </div>

															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label26" runat="server" class="col-form-label">RW KTP</asp:label>
																<asp:TextBox ID="txtrwktp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
												             </div>
                                                            </div>
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label27" runat="server" class="col-form-label">Kelurahan KTP</asp:label>
																<asp:TextBox ID="txtkelurahanktp" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
															                                            
                                                            </div> 

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Kecamatan KTP</asp:label>
																<asp:TextBox ID="txtkecamatanktp" CssClass="form-control" Text="" MaxLength="30"  runat="server" ></asp:TextBox>
                                                                 
															</div>
                                                            </div>
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label29" runat="server" class="col-form-label">Provinsi KTP</asp:label>
																<asp:DropDownList ID="ddlprovinsiktp" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                            </div>
                                                            
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">Kode Pos KTP</asp:label>
																<asp:TextBox ID="txtkodeposktp" CssClass="form-control" Text="0" MaxLength="6"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
											                </div>

                                                            </div>
															
														
                  
                                                              
                                </div>                                                    
                            </div>
                           </div>

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Upload KTP PRIBADI</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                        
                                      <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" class="col-form-label">Nomor Identitas KTP (NIK)</asp:label>
																<asp:TextBox ID="txtidentitas" CssClass="form-control"  MaxLength="16" onkeypress="return isnumerickey(event);"     runat="server" Width="219px" ></asp:TextBox>
                                                                              
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label20" runat="server" class="col-form-label">Kota Terbit Identitas</asp:label>
																<asp:TextBox ID="txtkotaidentitas" CssClass="form-control" MaxLength="30"  runat="server" ></asp:TextBox>
															</div> 

                                                            <div class="form-group">
                                                                 <asp:label id="Label21" runat="server"  class="col-form-label">Tanggal Terbit Identitas</asp:label>
                                                                <asp:TextBox ID="txttglterbitidentitas"  CssClass="form-control" runat="server"  TextMode="DateTime"  > </asp:TextBox>
                                                            </div>   
                                                          
                                    
                                    
                                    
                                                    <div class="form-group">
                                                                 <asp:label id="Label22" runat="server" Visible="false"  class="col-form-label">Berlaku sampai dengan</asp:label>
                                                                 <asp:TextBox ID="txtberlakuidentitas" Visible="false" CssClass="form-control" runat="server"  TextMode="DateTime"  ></asp:TextBox>
                                                            </div>   

                                                            <div>
                                                                <asp:FileUpload ID="fileuploadktp" Visible="false" runat="server"/> 
                                                                <asp:Button ID="btnUploadktp" Visible="false" Text="Upload" runat="server" OnClick="UploadFileKTP" OnClientClick="return CheckFileExistence()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtpathktp" Visible="false" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                     
                                                                <hr /> 
                                                            </div>

                                                           
                  
                                                              
                                </div>                                                    
                            </div>
                           </div>

                             <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Information Data</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                             
                                    <div class="form-group">
                                                            <asp:label ID="Label65" runat="server" class="col-form-label">Nama AO</asp:label>
											                <asp:DropDownList ID="ddlAO" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                        </div>

                                                     

                                                    <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">Add Petugas</asp:label>
																<asp:TextBox ID="txtkodepetugas" Enabled="false" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
															  
                                                            
                                                            </div> 

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">Add Date Entry</asp:label>
																<asp:TextBox ID="txtaddentry" Enabled="false" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
      
                                                            
															</div>
                                                            </div>
                                                    <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label12" runat="server" class="col-form-label">Update Petugas</asp:label>
																<asp:TextBox ID="txtpetugasupdate" Enabled="false" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
															  
                                                            
                                                            </div> 

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label15" runat="server" class="col-form-label">Update Date Entry</asp:label>
																<asp:TextBox ID="txtupdateentry" Enabled="false" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
      
                                                            
															</div>
                                                            </div>
                                                              
                                </div>                                                    
                            </div>
                           </div>

                           
               </div>      
                                   
                </div>
              </div>
              </asp:Panel>

             
                 <asp:Panel ID="Pnlfooter4" Visible="false" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <div class="card-box table-responsive">
                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success waves-light text-white" OnClick="btnBack_Click" Text="close" Width="65px" />
                               </div>
                            </div>
                        </div>
                    </div> 
                     </asp:Panel>
            
                 <!-- Panel Edit -->
             

             
             

             

                     

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="btnCancel" />   
                 <asp:PostBackTrigger ControlID="btnSimpan" />   
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="txtnamapanggilan"/>  
                <asp:PostBackTrigger ControlID="txtnamanasabah"/>      
                  
                <asp:PostBackTrigger ControlID="btnUploadktp" />
               
                 <asp:PostBackTrigger ControlID="lnkCari" /> 
                 

                 
                 
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
