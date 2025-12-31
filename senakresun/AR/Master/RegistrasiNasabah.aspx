<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrasiNasabah.aspx.cs" Inherits="FA_Master_RegistrasiNasabah" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Registrasi Nasabah</title>
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

         function CheckFileExistenceNPWP() {

            var filePath = document.getElementById('<%= this.fileuploadnpwp.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto NPWP belum di pilih!");


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
        function CheckFileExistenceKK() {

            var filePath = document.getElementById('<%= this.fileuploadKK.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Foto KK belum di pilih!");


                return false;
            }
        }
        
        function CheckFileExistencelainlain() {

            var filePath = document.getElementById('<%= this.fileuploadlainlain1.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File buku nikah belum di pilih!");


                return false;
            }
        }
        
        function CheckFileExistencelainlain2() {

            var filePath = document.getElementById('<%= this.fileuploadlainlain2.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Surat Domisili belum di pilih!");


                return false;
            }
        }

        
        function CheckFileExistencektppasangan() {

            var filePath = document.getElementById('<%= this.fileuploadktppasangan.ClientID %>').value;

            if (filePath.length < 1) {

                alert("File Lain lain 2 belum di pilih!");


                return false;
            }
        }
        function redirect() {
		  location.href = 'RegistrasiNasabah.aspx ';
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

            {
                $('#<%=txtalamatsesuaiktp1.ClientID%>').prop('maxlength', 150);
                
            }
             {
                $('#<%=txtalamatrumahtinggal1.ClientID%>').prop('maxlength', 150);
                
            }
                      {
                $('#<%=txtalamatperusahaan1.ClientID%>').prop('maxlength', 150);
                
            }

            {
                $('#<%=txtalamatPihakLain.ClientID%>').prop('maxlength', 150);
                
            }
   
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
                                    <li class="breadcrumb-item"><a href="RegistrasiNasabah.aspx">Registrasi Nasabah</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Registrasi Nasabah</h4>
                        </div>
                    </div>
                </div>
                 <asp:Panel ID="pnlHeader" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Registrasi Nasabah" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter"  runat="server">
                       <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box">
                                                    <div class="form-row">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-form-label">Search By</label>
                                                               
                                                                    <div class="form-group col-md-6">
                                                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="selectpicker show-tick" data-style="btn-success">
                                                                            <asp:ListItem Value=" and no_nasabah ">Nasabah ID</asp:ListItem>
                                                                            <asp:ListItem Value=" and nama_nasabah ">Nasabah Name</asp:ListItem>
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
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box ribbon-box">
                            <h4 class="m-t-0 header-title">Data Registrasi Nasabah</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"  OnRowCommand="GRData_RowCommand"  AutoGenerateColumns="False" Width="887px">
                                <Columns>
                                     <asp:TemplateField HeaderText="No Nasabah">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnonasabah" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_nasabah")%>'></asp:Label>
                                         </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Nama Nasabah">
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
                                    <asp:TemplateField HeaderText="Alamat Rumah Tinggal">
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
                                                    <div class="ribbon ribbon-custom">DATA DIRI NASABAH</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                        <asp:HiddenField ID="HDNoNasabah" runat="server" />	 
                                                        <asp:HiddenField ID="HDNoSebelumnya" runat="server" />	 
                                                        <asp:HiddenField ID="HDState" runat="server" />
                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Lengkap</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah" MaxLength="70"   runat="server" Text="-" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" SetFocusOnError="True" ControlToValidate="txtnamanasabah"  ErrorMessage="Nama Lengkap tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label" Visible="false">Nama Panggilan</asp:label>
																<asp:TextBox ID="txtnamapanggilan" Visible="false" CssClass="form-control" MaxLength="40"  Text="-" runat="server"></asp:TextBox> 
															    <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapanggilan"  ErrorMessage="Nama panggilan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            <div class="form-row">
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Jenis Kelamin</asp:label>
																<asp:DropDownList ID="ddljenkel" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="reqFavoriteColor" ErrorMessage="Jenis kelamin tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddljenkel" Runat="server" />

                                                             </div>
															
															<div class="form-group col-md-4">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">Tempat Lahir</asp:label>
																<asp:TextBox ID="txttempatlahir" CssClass="form-control" Text="-" MaxLength="50"  runat="server"  ></asp:TextBox>
															</div>
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label3ccc" runat="server" class="col-form-label">Tanggal Lahir</asp:label>
                                                                  <asp:TextBox CssClass="form-control" ID="txttgllahir"  TextMode="DateTime" runat="server"  ></asp:TextBox>
                                                                  
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" SetFocusOnError="True" ControlToValidate="txttgllahir"  ErrorMessage="Tanggal lahir tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 
                                                            </div>
                                                           <div class="form-row">
                                                             <div class="form-group col-md-5">
                                                                <asp:label ID="Label13" runat="server" class="col-form-label">Agama</asp:label>
																<asp:DropDownList ID="ddlAgama" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                  <asp:RequiredFieldValidator id="RequiredFieldValidator29" ErrorMessage="Agama tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlAgama" Runat="server" />

                                                            </div>  

                                                             <div class="form-group col-md-5">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">Status Pernikahan</asp:label>
																<asp:DropDownList ID="ddlstatusnikah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator4" ErrorMessage="Status pernikahan tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlstatusnikah" Runat="server" />

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
                                                                        <asp:TextBox ID="txtkewarganegaraan" MaxLength="100" CssClass="form-control"  runat="server" ></asp:TextBox>
                                                           
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                                  
                                                          </div>

                                                          

                                                            <div class="form-group">
                                                                <asp:label ID="Label31" runat="server" class="col-form-label">No.HP</asp:label>
																<asp:TextBox ID="txthp" CssClass="form-control" MaxLength="13" Text="0"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" SetFocusOnError="True" ControlToValidate="txthp"  ErrorMessage="No. HP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                                                                                        
                                                            <div class="form-group">
                                                                <asp:label ID="Label8" Font-Bold="true" runat="server" class="col-form-label">DATA KTP PRIBADI</asp:label>
														    </div>                                        
                                                            <div class="form-group">
                                                                <asp:label ID="Label7" runat="server" class="col-form-label">Alamat Rumah Sesuai KTP</asp:label>
																<asp:TextBox ID="txtalamatsesuaiktp1"   CssClass="form-control" Text="-" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" SetFocusOnError="True" ControlToValidate="txtalamatsesuaiktp1"  ErrorMessage="Alamat Rumah Sesuai KTP (1) tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>

                                                            <div class="form-row">
                                                                 <div class="form-group col-md-6">
                                                                <asp:label ID="Label23" runat="server" class="col-form-label">Kabupaten</asp:label>
																<asp:TextBox ID="txtkabupaten" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" SetFocusOnError="True" ControlToValidate="txtkabupaten"  ErrorMessage="Kabupaten tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            
                                                            
															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label25" runat="server" class="col-form-label">RT KTP</asp:label>
																<asp:TextBox ID="txtrtktp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"    runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" SetFocusOnError="True" ControlToValidate="txtrtktp"  ErrorMessage="RT KTP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>

															    <div class="form-group col-md-3">
                                                                <asp:label ID="Label26" runat="server" class="col-form-label">RW KTP</asp:label>
																<asp:TextBox ID="txtrwktp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server" SetFocusOnError="True" ControlToValidate="txtrwktp"  ErrorMessage="RW KTP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            </div>
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label27" runat="server" class="col-form-label">Kelurahan KTP</asp:label>
																<asp:TextBox ID="txtkelurahanktp" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" SetFocusOnError="True" ControlToValidate="txtkelurahanktp"  ErrorMessage="Kelurahan KTP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Kecamatan KTP</asp:label>
																<asp:TextBox ID="txtkecamatanktp" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator15" runat="server" SetFocusOnError="True" ControlToValidate="txtkecamatanktp"  ErrorMessage="Kecamatan KTP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
															</div>
                                                            </div>
                                                            <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label29" runat="server" class="col-form-label">Provinsi KTP</asp:label>
																<asp:DropDownList ID="ddlprovinsiktp" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator16" ErrorMessage="Provinsi KTP tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlprovinsiktp" Runat="server" />

															</div>
                                                            
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label30" runat="server" class="col-form-label">Kode Pos KTP</asp:label>
																<asp:TextBox ID="txtkodeposktp" CssClass="form-control" Text="0" MaxLength="6"  onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server" SetFocusOnError="True" ControlToValidate="txtkodeposktp"  ErrorMessage="Kodepos KTP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>

                                                            </div>
															
														
                                                            
                                                        </div>





                                                        <div class="form-group col-md-6">
                                                            
															
                                                            <asp:label ID="Label9" Font-Bold="true" runat="server" class="col-form-label">DATA TEMPAT TINGGAL</asp:label>
                                                             <div class="form-group">
                                                                <asp:label ID="Label39" runat="server" class="col-form-label">Status Rumah</asp:label>
																<asp:DropDownList ID="ddlstatusrumah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"    ></asp:DropDownList>
                                                                 <%--<asp:DropDownList ID="DropDownList1" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"  ></asp:DropDownList>--%>
                                                             </div>
                                                             <div class="form-group">
                                                                <asp:label ID="Label37" runat="server" class="col-form-label">Alamat Rumah Tinggal</asp:label>
																<asp:TextBox ID="txtalamatrumahtinggal1" CssClass="form-control" Text="-" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator18" runat="server" SetFocusOnError="True" ControlToValidate="txtalamatrumahtinggal1"  ErrorMessage="Alamat Rumah Tinggal (1) tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>
															
															<%--<div class="form-group">
                                                                <asp:label ID="Label41" runat="server" class="col-form-label">Alamat Rumah Tinggal 2</asp:label>
																<asp:TextBox ID="txtalamatrumahtinggal2" CssClass="form-control" MaxLength="100"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator19" runat="server" SetFocusOnError="True" ControlToValidate="txtalamatrumahtinggal2"  ErrorMessage="Alamat Rumah Tinggal (2) tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>--%>
                                                            
															<%--<div class="form-group">
                                                                <asp:label ID="Label42" runat="server" class="col-form-label">Alamat Rumah Tinggal 3</asp:label>
																<asp:TextBox ID="txtalamatrumahtinggal3" CssClass="form-control" MaxLength="100"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator20" runat="server" SetFocusOnError="True" ControlToValidate="txtalamatrumahtinggal3"  ErrorMessage="Alamat Rumah Tinggal (3) tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> --%>
                                                            

															<div class="form-group">
                                                                <asp:label ID="Label32" runat="server" class="col-form-label">RT</asp:label>
																<asp:TextBox ID="txtrt" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);" MaxLength="3"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator21" runat="server" SetFocusOnError="True" ControlToValidate="txtrt"  ErrorMessage="RT tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
															<div class="form-group">
                                                                <asp:label ID="Label33" runat="server" class="col-form-label">RW</asp:label>
																<asp:TextBox ID="txtrw" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator22" runat="server" SetFocusOnError="True" ControlToValidate="txtrw"  ErrorMessage="RW tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            
															<div class="form-group">
                                                                <asp:label ID="Label34" runat="server" class="col-form-label">Kelurahan</asp:label>
																<asp:TextBox ID="txtkelurahan" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator23" runat="server" SetFocusOnError="True" ControlToValidate="txtkelurahan"  ErrorMessage="Kelurahan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
															</div> 
															<div class="form-group">
                                                                <asp:label ID="Label35" runat="server" class="col-form-label">Kecamatan</asp:label>
																<asp:TextBox ID="txtkecamatan" CssClass="form-control" Text="-" MaxLength="50"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator24" runat="server" SetFocusOnError="True" ControlToValidate="txtkecamatan"  ErrorMessage="Kecamatan tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
															<div class="form-group">
                                                                <asp:label ID="Label36" runat="server" class="col-form-label">Provinsi</asp:label>
																<asp:DropDownList ID="ddlprovinsi" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator25" ErrorMessage="Provinsi tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlprovinsi" Runat="server" />

															</div>
															<div class="form-group">
                                                                <asp:label ID="Label38" runat="server" class="col-form-label">Kode Pos</asp:label>
																<asp:TextBox ID="txtkodepos" CssClass="form-control" MaxLength="6" Text="0" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator26" runat="server" SetFocusOnError="True" ControlToValidate="txtkodepos"  ErrorMessage="Kodepos tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>

                                                         

                                                        </div>


													</div>
												</div>  

                                            </div>
                                          
                                         </div>        
                                         <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    <asp:Button ID="btnBack" class="btn btn-success waves-light text-white" runat="server" Text="Back" OnClientClick="return sweetAlertCancel(this,'Apakah anda yakin akan meninggalkan halaman ini ?');"/> 
                                                   

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNext" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNext_Click" Width="65px" />
                                                   
                                                    </div>
                                            </div> 
                                        
                                         
                                               
    
            </asp:Panel> 

            
                 <!-- Panel Edit -->
            <asp:Panel ID="pnlPekerjaan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PEKERJAAN NASABAH</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                              
                                                             <div class="form-group">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Pekerjaan</asp:label>
																<asp:DropDownList ID="ddlPekerjaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            </div>
															
															<div class="form-group">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Nama Pekerjaan</asp:label>
																<asp:TextBox ID="txtPekerjaan"  MaxLength="70" CssClass="form-control"  runat="server" text="-"></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
															</div>
                                                             
                                                            <div class="form-group">
                                                                 <asp:label id="Label47" runat="server" class="col-form-label">Jabatan</asp:label>
                                                                 <asp:TextBox ID="txtjabatan"  MaxLength="50"  CssClass="form-control"  runat="server" text="-"  ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal
                                                            </div>  

                                                            <div class="form-group">
                                                                 <asp:label id="Label48" runat="server"  class="col-form-label">Nama Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtNamaPerusahaan"  MaxLength="100" CssClass="form-control"  runat="server" text="-"  ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal
                                                            </div>   

                                                            <div class="form-group">
                                                                 <asp:label id="Label43" runat="server"  class="col-form-label">Bidang Usaha Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtBidangUsaha"  MaxLength="100" CssClass="form-control"  runat="server" text="-"  ></asp:TextBox>
                                                            </div>   

                                                            <div class="form-group">
                                                                <asp:label ID="Label49" runat="server" class="col-form-label">Lama Usaha/Bekerja</asp:label>
																<asp:DropDownList ID="ddlLamaBekerja" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator46" ErrorMessage="Agama tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlAgama" Runat="server" />

                                                                 
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label51" runat="server" class="col-form-label">Kepemilikan Tempat Usaha</asp:label>
																<asp:DropDownList ID="ddlkepemilikantempatusaha" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                <asp:TextBox ID="txtkepemilikantempatusaha" MaxLength="50" CssClass="form-control" runat="server"  text="-"  ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika tidak perlu</span>
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label87" runat="server" class="col-form-label">Alamat Surat</asp:label>
																<asp:DropDownList ID="ddlAlamatSurat" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                 
                                                            </div> 
                                                            
                                                           <%-- <div class="form-group">
                                                                 <asp:label id="Label88" runat="server"  class="col-form-label">NIP</asp:label>
                                                                 <asp:TextBox ID="txtNIP"  CssClass="form-control" MaxLength="16"  runat="server" text="-"  ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika tidak memiliki NIP</span>
                                                            </div> --%>  
                                                            
                                                          <%--  <div class="form-group">
                                                                 <asp:label id="Label89" runat="server"  class="col-form-label">No.Pensiun</asp:label>
                                                                 <asp:TextBox ID="txtnopensiun"  CssClass="form-control" MaxLength="16"  runat="server" text="-"  ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika tidak memiliki No.Pensiun</span>
                                                            </div> --%>  
                                                           
                                                         
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                              <div class="form-group">
                                                                <asp:label ID="Label50" runat="server" class="col-form-label">Alamat Perusahaan</asp:label>
																<asp:TextBox ID="txtalamatperusahaan1"  MaxLength="100" CssClass="form-control"  runat="server" text="-" TextMode="MultiLine"></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                              </div>

                                                            <%--<div class="form-group">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">Alamat Perusahaan 2</asp:label>
																<asp:TextBox ID="txtalamatperusahaan2" MaxLength="100" CssClass="form-control"  runat="server"  text="-"  ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>--%>

                                                           
                                                             <%--<div class="form-group">
                                                                <asp:label ID="Label52" runat="server" class="col-form-label">Alamat Perusahaan 3</asp:label>
                                                                 <asp:TextBox ID="txtalamatperusahaan3" MaxLength="100" CssClass="form-control"  runat="server" text="-"></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>  --%>
                                                             <div class="form-group">
                                                                <asp:label ID="Label53" runat="server" class="col-form-label">Kabupaten</asp:label>
                                                                 <asp:TextBox ID="txtkabupatenpekerjaan" MaxLength="50" CssClass="form-control"  runat="server" text="-" ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>  
                                                             <div class="form-group">
                                                                <asp:label ID="Label54" runat="server" class="col-form-label">Kelurahan</asp:label>
                                                                 <asp:TextBox ID="txtkelurahanpekerjaan" MaxLength="50" CssClass="form-control"  runat="server" text="-" ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>  
                                                             <div class="form-group">
                                                                <asp:label ID="Label55" runat="server" class="col-form-label">Kecamatan</asp:label>
                                                                 <asp:TextBox ID="txtkecamatanpekerjaan" MaxLength="50" CssClass="form-control"  runat="server" text="-" ></asp:TextBox>
                                                                 <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
                                                            </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label56" runat="server" class="col-form-label">Provinsi</asp:label>
																<asp:DropDownList ID="ddlprovinsipekerjaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                <span class="font-14 text-muted">isi "-" jika bukan pekerja Formal</span>
															</div>
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label57" runat="server" class="col-form-label">Kode Pos</asp:label>
																<asp:TextBox ID="txtkodepospekerjaan" MaxLength="6" CssClass="form-control" onkeypress="return isnumerickey(event);" text="0"   runat="server" ></asp:TextBox>
															    <span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>
                                                             </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label58" runat="server" class="col-form-label">Telepon</asp:label>
																<asp:TextBox ID="txtteleponpekerjaan" MaxLength="13" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server" text="0" ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>
                                                              </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label59" runat="server" class="col-form-label">Fax</asp:label>
																<asp:TextBox ID="txtFaxPekerjaan" MaxLength="10" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server" text="0" ></asp:TextBox>
                                                                <span class="font-14 text-muted">isi "0" jika bukan pekerja Formal</span>
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
     
    
            </asp:Panel> 

            <asp:Panel ID="PnlPendapatan" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PENDAPATAN / SUMBER DANA NASABAH</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label60" runat="server" class="col-form-label">Penghasilan Gaji Kotor Per Tahun</asp:label>
																<asp:DropDownList ID="ddlgajikotor" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                             </div>
															
															 <div class="form-group">
                                                                <asp:label ID="Label61" runat="server" class="col-form-label">Sumber Penghasilan</asp:label>
																<asp:DropDownList ID="ddlsumberpenghasilan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlsumberpenghasilan_SelectedIndexChanged"  ></asp:DropDownList>
                                                                <asp:TextBox ID="txtsumberpenghasilan" MaxLength="50" CssClass="form-control" runat="server"   ></asp:TextBox>
                                                                
                                                             </div>
                                                          </div>
                                                        <div class="form-group col-md-6">
                                                              <div class="form-group">
                                                                <asp:label ID="Label62" runat="server" class="col-form-label">Transaksi Per Hari</asp:label>
																<asp:DropDownList ID="ddltransaksiperhari" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
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
    
            </asp:Panel> 
            <asp:Panel ID="pnlLainnya" Visible="false" runat="server">
            <div class="clearfix">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                            <div class="card-box ribbon-box">
                                            <div class="ribbon ribbon-custom">DATA LAIN NASABAH</div>
                                                <p class="m-b-0"></p>
                                            <div class="form-row">
                                                         
                                                <div class="form-group col-md-6"> 
															
											            <div class="form-group">
                                                            <asp:label ID="Label64" runat="server" class="col-form-label">Tujuan Pembukaan Rekening</asp:label>
											                <asp:DropDownList ID="ddlTujuanbukaRekening" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlTujuanbukaRekening_SelectedIndexChanged"  ></asp:DropDownList>
                                                            <asp:TextBox ID="txttujuanbukarekening" MaxLength="100" CssClass="form-control" runat="server"   ></asp:TextBox>
                                                                
                                                        </div>
                                                        
                                                        <div class="form-group">
                                                            <asp:label ID="Label63" runat="server" class="col-form-label">Dalam Hal Bertindak</asp:label>
											                <asp:DropDownList ID="ddldalamhalbertindak" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddldalamhalbertindak_SelectedIndexChanged"  ></asp:DropDownList>
                                                            <asp:TextBox ID="txtdalamhalbertindak" MaxLength="100" CssClass="form-control" runat="server"   ></asp:TextBox>
                                                                
                                                        </div>
                                                             
                                                        <div class="form-group">
                                                            <asp:label ID="Label65" runat="server" class="col-form-label">Nama AO</asp:label>
											                <asp:DropDownList ID="ddlAO" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                
                                                        <div class="form-group col-md-6">

                                                         <div class="form-group">
                                                                <asp:label ID="Label66" runat="server" class="col-form-label">Nama Ibu Kandung</asp:label>
                                                                 <asp:TextBox ID="txtnamaibukandung" MaxLength="70" CssClass="form-control"  runat="server" ></asp:TextBox> 
                                                             <asp:RequiredFieldValidator id="RequiredFieldValidator34" runat="server" SetFocusOnError="True" ControlToValidate="txtnamaibukandung"  ErrorMessage="Nama ibu kandung tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                          
                                                         </div>  

                                                           <div class="form-group">
                                                                <asp:label ID="Label83" runat="server" class="col-form-label">Jumlah Anak</asp:label>
																<asp:TextBox ID="txtjumlahanak" CssClass="form-control" MaxLength="2" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator27" runat="server" SetFocusOnError="True" ControlToValidate="txtjumlahanak"  ErrorMessage="Jumlah anak tidak boleh kosong (isi '0' jika tidak memiliki anak)"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>


                                                         <div class="form-group">
                                                                <asp:label ID="Label67" runat="server" class="col-form-label">Kode Petugas</asp:label>
                                                                 <asp:TextBox ID="txtkodepetugas" CssClass="form-control" MaxLength="100"  runat="server" Enabled ="false"></asp:TextBox> 
                                                         </div>  
                                                         
                                                        <div class="form-group">
                                                            <asp:label ID="Label68" runat="server" class="col-form-label">Kena Pajak</asp:label>
											                <asp:DropDownList ID="ddlKenaPajak" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                        </div>
                                                         
                                                         
                                                    </div>  


								            </div>
							            </div>  

                                    </div>
                                    </div>        
                  
                                    <div class="hidden-print mt-4">
                                        <div class="float-left mb-2"> 
                                                   
                                                    
                                                <asp:Button ID="btnBackLain" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackLain_Click" />
                                                    
                                            </div>
                                        <div class="float-right mb-2 "> 
                                                   
                                                <asp:Button ID="btnNextLain" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNextLain_Click" Width="65px" />
                                                   

                                            </div>
                                    </div>  
    
            </asp:Panel> 

            <asp:Panel ID="pnlPihakYgDapatDiHubungi" Visible="false" runat="server">
              <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA PIHAK LAIN YANG DAPAT DI HUBUNGI</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                           
                                                             <div class="form-group">
                                                                <asp:label ID="Label69" runat="server" class="col-form-label">Nama</asp:label>
                                                                 <asp:TextBox ID="txtnamapihaklain" MaxLength="70" CssClass="form-control"  runat="server" Text="-" ></asp:TextBox> 
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator35" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapihaklain"  ErrorMessage="Nama pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>  
															
															 <div class="form-group">
                                                                <asp:label ID="Label70" runat="server" class="col-form-label">Hubungan</asp:label>
																<asp:DropDownList ID="ddlHubunganPihakLain" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator36" ErrorMessage="Hubungan dengan pihak lain tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlHubunganPihakLain" Runat="server" />

                                                             </div>
                                                              <div class="form-group">
                                                                <asp:label ID="Label71" runat="server" class="col-form-label">Alamat </asp:label>
                                                                 <asp:TextBox ID="txtalamatPihakLain" MaxLength="250" CssClass="form-control" Text="-" runat="server" TextMode ="MultiLine" Columns="55" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator37" runat="server" SetFocusOnError="True" ControlToValidate="txtalamatPihakLain"  ErrorMessage="Alamat pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                              </div> 
                                                            
                                                             
                                                             <div class="form-group">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">RT</asp:label>
                                                                 <asp:TextBox ID="txtRTPihakLain" CssClass="form-control" MaxLength="3" Text="0" runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator38" runat="server" SetFocusOnError="True" ControlToValidate="txtRTPihakLain"  ErrorMessage="RT pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>  

                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label73" runat="server" class="col-form-label">RW</asp:label>
                                                                 <asp:TextBox ID="txtRWPihakLain" CssClass="form-control"  MaxLength="3" Text="0" runat="server" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator39" runat="server" SetFocusOnError="True" ControlToValidate="txtRWPihakLain"  ErrorMessage="RW pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>


                                                          </div>

                                                            <div class="form-group col-md-6">

                                                           
                                                            <div class="form-group">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Kelurahan</asp:label>
                                                                 <asp:TextBox ID="txtKelurahanPihakLain" MaxLength="50" CssClass="form-control" Text="-"  runat="server" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator40" runat="server" SetFocusOnError="True" ControlToValidate="txtKelurahanPihakLain"  ErrorMessage="Kelurahan pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label75" runat="server" class="col-form-label">Kecamatan</asp:label>
                                                                 <asp:TextBox ID="txtKecamatanPihakLain" MaxLength="50" CssClass="form-control" Text="-"  runat="server" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator41" runat="server" SetFocusOnError="True" ControlToValidate="txtKecamatanPihakLain"  ErrorMessage="Kecamatan pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label76" runat="server" class="col-form-label">Kota</asp:label>
                                                                 <asp:TextBox ID="txtKotaPihakLain" MaxLength="50" CssClass="form-control" Text="-"  runat="server" ></asp:TextBox> 
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator42" runat="server" SetFocusOnError="True" ControlToValidate="txtKotaPihakLain"  ErrorMessage="Kota pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label77" runat="server" class="col-form-label">Provinsi</asp:label>
                                                                <asp:DropDownList ID="ddlProvinsiPihakLain"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator43" ErrorMessage="Provinsi dengan pihak lain tidak boleh kosong"  ForeColor="Red" SetFocusOnError="True" InitialValue="-" ControlToValidate="ddlProvinsiPihakLain" Runat="server" />

                                                            </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label78" runat="server" class="col-form-label">Kode Pos</asp:label>
                                                                 <asp:TextBox ID="txtKodePosPihakLain" MaxLength="6" CssClass="form-control" Text="0"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                 <asp:RequiredFieldValidator id="RequiredFieldValidator44" runat="server" SetFocusOnError="True" ControlToValidate="txtKodePosPihakLain"  ErrorMessage="kodepos pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label79" runat="server" class="col-form-label">No.Telepon</asp:label>
                                                                 <asp:TextBox ID="txtTlpPihakLain" MaxLength="13" CssClass="form-control"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator45" runat="server" SetFocusOnError="True" ControlToValidate="txtTlpPihakLain"  ErrorMessage="No. Tlp pihak lain tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>  
                                                         
                                                         
                                                        </div>  



													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackPihakLain" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackPihakLain_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                        <asp:Button ID="btnSimpan" align="right" class="btn btn-success waves-light text-white" runat="server" Text="SIMPAN" OnClick="btnSimpan_Click"    OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');"/> 
                                                   

                                                    </div>
                                            </div> 
     
            </asp:Panel> 

                    <asp:Panel ID="pnlIdentitas" Visible="false" runat="server">
                    <div class="clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA UPLOAD NASABAH DAN PASANGAN</div>
                                                       <p class="m-b-0"></p>
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label80" runat="server" class="col-form-label">Nomor Identitas KTP (NIK)</asp:label>
																<asp:TextBox ID="txtidentitas" CssClass="form-control"  MaxLength="16" onkeypress="return isnumerickey(event);"     runat="server" Width="219px" AutoPostBack="true" OnTextChanged="txtidentitas_TextChanged"></asp:TextBox>
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator28" runat="server" SetFocusOnError="True" ControlToValidate="txtidentitas"  ErrorMessage="No. KTP tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label20" runat="server" class="col-form-label">Kota Terbit Identitas</asp:label>
																<asp:TextBox ID="txtkotaidentitas" CssClass="form-control" MaxLength="100"  runat="server" ></asp:TextBox>
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
                                                                <asp:FileUpload ID="fileuploadktp" runat="server"/> 
                                                                <asp:Button ID="btnUploadktp" Text="Upload" runat="server" OnClick="UploadFileKTP" OnClientClick="return CheckFileExistence()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtpathktp" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                     
                                                                <hr /> 
                                                            </div>

                                                               <div class="form-group">
                                                                <asp:label ID="Label16" runat="server" class="col-form-label">No.Identitas Pasangan</asp:label>
																<asp:TextBox ID="txtidpasangan" CssClass="form-control"   MaxLength="16"  onkeypress="return isnumerickey(event);"    runat="server" AutoPostBack="true"  OnTextChanged="txtidpasangan_TextChanged"></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator30" runat="server" SetFocusOnError="True" ControlToValidate="txtidpasangan"  ErrorMessage="No. KTP Pasangan tidak boleh kosong (isi '0' jika tidak belum menikah)"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                             </div>

															<div class="form-group">
                                                                <asp:label ID="Label17" runat="server" class="col-form-label">Nama Pasangan</asp:label>
																<asp:TextBox ID="txtnamapasangan" CssClass="form-control"  MaxLength="70"  runat="server" ></asp:TextBox>
															    <asp:RequiredFieldValidator id="RequiredFieldValidator31" runat="server" SetFocusOnError="True" ControlToValidate="txtnamapasangan"  ErrorMessage="Nama Pasangan tidak boleh kosong (isi '-' jika tidak belum menikah)"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div>
                                                            
															<div class="form-group">
                                                                <asp:label ID="Label18" runat="server" class="col-form-label">Tempat Lahir Pasangan</asp:label>
																<asp:TextBox ID="txttempatlahirpasangan" CssClass="form-control"  MaxLength="50"  runat="server" ></asp:TextBox>
															</div>

                                                             <div class="form-group">
                                                                 <asp:label id="Label19" runat="server"  class="col-form-label">Tanggal Lahir Pasangan</asp:label>
                                                                 <asp:TextBox ID="txttgllahirpasangan"  CssClass="form-control" runat="server"  TextMode="DateTime" ></asp:TextBox>
                                                            </div>  
                                                            
                                                         
                                                            <div>
                                                                <asp:FileUpload ID="fileuploadktppasangan" runat="server"/> 
                                                                <asp:Button ID="btnuploadktppasangan" Text="Upload" runat="server" OnClick="UploadFilektppasangan" OnClientClick="return CheckFileExistencektppasangan()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtppathktppasangan" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                <hr />
                                                               
                                                            </div>
														



                                                          </div>

                                                         <div class="form-group col-md-6">
    
                                                             
                                                            <div class="form-group">
                                                                <asp:label ID="Label11" runat="server" class="col-form-label">Nomer NPWP</asp:label>
																 <asp:TextBox ID="txtNPWP" CssClass="form-control" MaxLength="15"  runat="server" Width="219px" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                 <%--<asp:RequiredFieldValidator id="RequiredFieldValidator32" runat="server" SetFocusOnError="True" ControlToValidate="txtNPWP"  ErrorMessage="NPWP tidak boleh kosong (isi '0' jika tidak memiliki NPWP)"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                            
                                                            </div> 

                                                            <div>
                                                                <asp:FileUpload ID="fileuploadnpwp" runat="server"/> 
                                                                <asp:Button ID="btnUploadnpwp" Text="Upload" runat="server" OnClick="UploadFileNPWP" OnClientClick="return CheckFileExistenceNPWP()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtpathnpwp" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                <hr /> 
                                                            </div>

                                                            <div class="form-group">
                                                                <asp:label ID="Label6" runat="server" class="col-form-label">Nomer Kartu Keluarga (KK)</asp:label>
																<asp:TextBox ID="txtnomerkk" CssClass="form-control"  MaxLength="16"  runat="server" Width="219px" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                                <asp:RequiredFieldValidator id="RequiredFieldValidator33" runat="server" SetFocusOnError="True" ControlToValidate="txtnomerkk"  ErrorMessage="No.KK tidak boleh kosong"  ForeColor="Red"></asp:RequiredFieldValidator>
                                                            
                                                            </div> 

                                                            <div>
                                                                <asp:FileUpload ID="fileuploadKK" runat="server"/> 
                                                                <asp:Button ID="btnUploadKK" Text="Upload" runat="server" OnClick="UploadFileKK" OnClientClick="return CheckFileExistenceKK()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtpathKK" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400"></asp:TextBox>
                                                                <hr /> 
                                                            </div>

                                                           <div class="form-group">
                                                                <asp:label ID="Label81" runat="server" class="col-form-label">Buku Nikah</asp:label>
																 <asp:TextBox ID="txtlainlain1" CssClass="form-control"   MaxLength="60" runat="server" Width="219px"   AutoPostBack="true"  OnTextChanged="txtlainlain1_TextChanged"  >-</asp:TextBox> 
                                                           
                                                            </div> 
                                                         
                                                            <div>
                                                                <asp:FileUpload ID="fileuploadlainlain1" runat="server"/> 
                                                                <asp:Button ID="btnlainlain1" Text="Upload" runat="server" OnClick="UploadFilelainlain" OnClientClick="return CheckFileExistencelainlain()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtpathlainlain1" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                                                                <hr />
                                                                <%--<asp:Image ID="imagelainlain" runat="server" Height="140px" Width="280px" />--%>
                                                            </div>

                                                              <div class="form-group">
                                                                <asp:label ID="Label82" runat="server" class="col-form-label">Surat Domisili</asp:label>
																 <asp:TextBox ID="txtlainlain2" CssClass="form-control"   MaxLength="60" runat="server" Width="219px"   AutoPostBack="true"  OnTextChanged="txtlainlain2_TextChanged" >-</asp:TextBox> 
                                                           
                                                            </div> 
                                                         
                                                            <div>
                                                                <asp:FileUpload ID="fileuploadlainlain2" runat="server"/> 
                                                                <asp:Button ID="btnlainlain2" Text="Upload" runat="server" OnClick="UploadFilelainlain2" OnClientClick="return CheckFileExistencelainlain2()" />
                                                                <hr />
                                                                    <asp:TextBox ID="txtpathlainlain2" CssClass="form-control" enabled = "false"   runat="server" MaxLength="400">-</asp:TextBox>
                                                                <hr /> 
                                                            </div>
                                                         
                                                        </div>  



													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                            <div class="hidden-print mt-4">
                                                <div class="float-left mb-2"> 
                                                   
                                                    
                                                        <asp:Button ID="btnBackIdentitas" align="right" class="btn btn-success waves-light text-white" runat="server" Text="BACK" OnClick="btnBackIdentitas_Click" />
                                                        

                                                    </div>
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnNexIdentitas" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="NEXT" OnClick="btnNexIdentitas_Click" Width="65px" />

                                                    </div>
                                            </div> 
     
    
            </asp:Panel> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                <asp:PostBackTrigger ControlID="btnAdd" />   
                <asp:PostBackTrigger ControlID="btnNext" />   
                <asp:PostBackTrigger ControlID="GRData" />
                <asp:PostBackTrigger ControlID="txtnamapanggilan"/>  
                <asp:PostBackTrigger ControlID="txtnamanasabah"/>      
                  
                <asp:PostBackTrigger ControlID="btnUploadktp" />
                <asp:PostBackTrigger ControlID="btnUploadKK" />
                <asp:PostBackTrigger ControlID="btnUploadnpwp" />
                <asp:PostBackTrigger ControlID="btnuploadktppasangan" />
                <asp:PostBackTrigger ControlID="btnlainlain1" />
                <asp:PostBackTrigger ControlID="btnlainlain2" /> 
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
