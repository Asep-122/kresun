<%@ Page Language="C#" enableEventValidation="false" AutoEventWireup="true" CodeFile="RegistrasiDataPelengkap.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="FA_Master_RegistrasiDataPelengkap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Registrasi Data Pelengkap</title>
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





        function checkTextAreaMaxLength(textBox, e, length)
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


        function redirect() {
		  location.href = 'RegistrasiDataPelengkap.aspx ';
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
        var scrollPosition = 0;
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            $(window).scroll(function (event) {
                scrollPosition = $(window).scrollTop();
            });


             $('#txttgllahir').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttgllahirasuransi').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttglterbitidentitas').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txtberlakuidentitas').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttgllahirpasangan').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttglmulaimenempati').datepicker({
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
                                    <li class="breadcrumb-item"><a href="#">Kredit Pensiun</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="RegistrasiNasabah.aspx">Data Pelengkap Nasabah</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Data Pelengkap Nasabah</h4>
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
                            <%--<asp:Button ID="btnAdd" Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="Add Registrasi Nasabah" OnClick="btnAdd_Click" />--%>
                        </div>
                    </div>
                   </div>
                </asp:Panel>

                   <asp:Panel ID="Panel1"  runat="server">
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
                 <!-- Panel Front -->
        <asp:Panel ID="PnlFront" runat="server">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box ribbon-box">
                            <h4 class="m-t-0 header-title">Data Nasabah</h4>                           
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
                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nama_nasabah")%>'></asp:Label>
                                              
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
                                    <asp:TemplateField HeaderText="Tanggal Bergabung">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltglTrn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "register_date","{0:dd-MM-yyyy}")%>'></asp:Label>
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


                 <asp:Panel ID="pnldataanggota" Visible="false"  runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">Data Anggota</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">

                                                            <div class="form-group col-md-3">
                                                                <asp:label ID="Label119" runat="server" class="col-form-label">No Anggota</asp:label>
																<asp:TextBox ID="txtnoanggota" enabled="false" CssClass="form-control" MaxLength="40"   runat="server"></asp:TextBox> 
															 
                                                            
                                                            </div>

                                                           
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label28" runat="server" class="col-form-label">Nama Lengkap</asp:label>
																<asp:TextBox CssClass="form-control" enabled="false" ID="txtnamaanggota" MaxLength="70"   runat="server"  ></asp:TextBox>

                                                            </div>
															
															
                                                            <div class="form-group col-md-3">
                                                                <asp:label ID="Label86" runat="server" class="col-form-label">Tanggal Bergabung</asp:label>
                                                                  <asp:TextBox CssClass="form-control" enabled="false" ID="txttanggalbergabung"  TextMode="DateTime" runat="server"  ></asp:TextBox>
                                                                  
                                                               
                                                            
                                                            </div> 
                                                            </div>
                                                               

                                            </div>
                                          
                                         </div>        
                                          
                                        
                                         
                                               
    
            </asp:Panel>
                 <!-- Panel Edit -->

                 <asp:Panel ID="Pnl1" Visible="false" runat="server">
                      
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA TEMPAT TINGGAL & DATA PASANGAN</div>
                                                       <p class="m-b-0"></p>
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Tempat Tinggal</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                      <div class="form-group">
                                                                <asp:label ID="Label104" runat="server" class="col-form-label">Status Rumah</asp:label>
																<asp:DropDownList ID="ddlstatusrumah" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"></asp:DropDownList>
                                                                 <%--<asp:DropDownList ID="DropDownList1" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatusrumah_SelectedIndexChanged"  ></asp:DropDownList>--%>
                                                             </div>
                                                             <div class="form-group">
                                                                <asp:label ID="Label105" runat="server" class="col-form-label">Alamat Rumah Tinggal</asp:label>
																<asp:TextBox ID="txtalamatrumahtinggal1" CssClass="form-control" Text="-" MaxLength="100" TextMode="MultiLine"  runat="server" ></asp:TextBox>
                                                              
                                                             </div>
															
														
                                                          <div class="form-row">
                                                               <div class="form-group col-md-6">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Kabupaten Tempat Tinggal</asp:label>
																<asp:TextBox ID="txtkabupatenpt" Enabled="true" CssClass="form-control" Text="-" MaxLength="30"  runat="server" ></asp:TextBox>
                                                              
															</div> 
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label106" runat="server" class="col-form-label">RT</asp:label>
																<asp:TextBox ID="txtrttp" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);" MaxLength="3"  runat="server" ></asp:TextBox>
															  
                                                            </div>
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label113" runat="server" class="col-form-label">RW</asp:label>
																<asp:TextBox ID="txtrwtpt" CssClass="form-control" Text="0" onkeypress="return isnumerickey(event);"  MaxLength="3"   runat="server" ></asp:TextBox>
															 
                                                            </div>
                                                        </div>

                                                        <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label114" runat="server" class="col-form-label">Kelurahan Tempat Tinggal</asp:label>
																<asp:TextBox ID="txtkelurahantpt" Enabled="true" CssClass="form-control" Text="-" MaxLength="30"  runat="server" ></asp:TextBox>
                                                              
															</div> 
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label115" runat="server" class="col-form-label">Kecamatan Tempat Tinggal</asp:label>
																<asp:TextBox ID="txtkecamatantpt" Enabled="true" CssClass="form-control" Text="-" MaxLength="30"  runat="server" ></asp:TextBox>
															   
                                                            </div>
                                                         </div>
                                                        <div class="form-row">
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label116" runat="server" class="col-form-label">Provinsi Tempat Tinggal</asp:label>
																<asp:DropDownList ID="ddlprovinsitpt" CssClass="selectpicker show-tick"  runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                               

															</div>

															<div class="form-group col-md-6">
                                                                <asp:label ID="Label117" runat="server" class="col-form-label">Kode Pos Tempat Tinggal</asp:label>
																<asp:TextBox ID="txtkodepostpt" CssClass="form-control" MaxLength="6" Text="0" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
															   
                                                            </div>
                                                        </div>


                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">Data Pasangan</div>
                                                <p class="text-muted font-14 m-b-30"></p>

                                                   <div class="form-group">
                                                                <asp:label ID="Label10" runat="server" class="col-form-label">Status dalam keluarga</asp:label>
																<asp:DropDownList ID="ddlstatuskawinupload" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlstatuskawin_SelectedIndexChanged" ></asp:DropDownList>
                                                            </div>
                                                                <div class="form-group">
                                                                <asp:label ID="Label107" runat="server" class="col-form-label">No.Identitas Pasangan</asp:label>
																<asp:TextBox ID="txtidpasangan" CssClass="form-control"   MaxLength="16"  onkeypress="return isnumerickey(event);"    runat="server" AutoPostBack="true"  OnTextChanged="txtidpasangan_TextChanged"></asp:TextBox>
															   
                                                             </div>
                                                             	<div class="form-group">
                                                                <asp:label ID="Label108" runat="server" class="col-form-label">Nama Pasangan</asp:label>
																<asp:TextBox ID="txtnamapasangan" CssClass="form-control"  MaxLength="70"  runat="server" ></asp:TextBox>
															     
                                                            </div>
                                                            
															<div class="form-group">
                                                                <asp:label ID="Label109" runat="server" class="col-form-label">Tempat Lahir Pasangan</asp:label>
																<asp:TextBox ID="txttempatlahirpasangan" CssClass="form-control"  MaxLength="30"  runat="server" ></asp:TextBox>
															</div>

                                                             <div class="form-group">
                                                                 <asp:label id="Label110" runat="server"  class="col-form-label">Tanggal Lahir Pasangan</asp:label>
                                                                 <asp:TextBox ID="txttgllahirpasangan"  CssClass="form-control" runat="server"  TextMode="DateTime" ></asp:TextBox>
                                                            </div>  
                                                        

                                                       

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                    
                                     
                                                   </div>  

                                            </div>
                                         </div>        
                             
                    
                    </asp:Panel>


                 <asp:Panel ID="pnlPekerjaan" Visible="false" runat="server">
                      
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA PEKERJAAN NASABAH & DATA PENDAPATAN / SUMBER DANA NASABAH</div>
                                                       <p class="m-b-0"></p>
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA PEKERJAAN NASABAH</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                     
                                                      <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Pekerjaan</asp:label>
																<asp:DropDownList ID="ddlPekerjaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                            
                                                            </div>
															
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label46" runat="server" class="col-form-label">Nama Pekerjaan</asp:label>
																<asp:TextBox ID="txtPekerjaan"  MaxLength="70" Text="-" CssClass="form-control"  runat="server" ></asp:TextBox>
                                                            
                                                               
                                                            
															</div>
                                                      </div>    

                                                 <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                 <asp:label id="Label47" runat="server" class="col-form-label">Jabatan</asp:label>
                                                                 <asp:TextBox ID="txtjabatan"  MaxLength="50"  Text="-" CssClass="form-control"  runat="server"  ></asp:TextBox>
                                                                        
                                                            </div>  

                                                          

                                                           <div class="form-group col-md-6">
                                                                <asp:label ID="Label49" runat="server" class="col-form-label">Lama Bekerja</asp:label>
																<asp:DropDownList ID="ddlLamaBekerja" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                                                             
                                                            </div> 
                                                             </div>  
                                                            <div class="form-group">
                                                                <asp:label ID="Label51" runat="server" class="col-form-label">Kepemilikan Tempat Usaha</asp:label>
																<asp:DropDownList ID="ddlkepemilikantempatusaha" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlkepemilikantempatusaha_SelectedIndexChanged"  ></asp:DropDownList>
                                                                <asp:TextBox ID="txtkepemilikantempatusaha" Text="-" MaxLength="50" CssClass="form-control" runat="server" required="required"  ></asp:TextBox>
                                                                                                                             
                                                            </div> 


                                                               <div class="form-row">
                                                            <div class="form-group col-md-6">
                                                                 <asp:label id="Label48" runat="server"  class="col-form-label">Nama Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtNamaPerusahaan"  Text="-" MaxLength="100" CssClass="form-control"  runat="server"   ></asp:TextBox>
                                                                                        
                                                            </div>   

                                                            <div class="form-group col-md-6">
                                                                 <asp:label id="Label43" runat="server"  class="col-form-label">Bidang Usaha Perusahaan</asp:label>
                                                                 <asp:TextBox ID="txtBidangUsaha"  Text="-" MaxLength="100" CssClass="form-control"  runat="server"   ></asp:TextBox>
                                                         </div>   
                                                                   </div>   


                                                                <div class="form-group">
                                                                <asp:label ID="Label50" runat="server" class="col-form-label">Alamat Perusahaan</asp:label>
																<asp:TextBox ID="txtalamatperusahaan1" Text="-"  MaxLength="150" TextMode="MultiLine" CssClass="form-control"  runat="server"   ></asp:TextBox>
                                                                  
                                                               
                                                              </div>

                                                              <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label53" runat="server" class="col-form-label">Kabupaten</asp:label>
                                                                 <asp:TextBox ID="txtkabupatenpekerjaan" Text="-" MaxLength="50" CssClass="form-control"  runat="server" ></asp:TextBox>
                                                                                                          
                                                            </div>  
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label54" runat="server" class="col-form-label">Kelurahan</asp:label>
                                                                 <asp:TextBox ID="txtkelurahanpekerjaan" Text="-" MaxLength="50" CssClass="form-control"  runat="server" ></asp:TextBox>
                                                                           
                                                            </div>  
                                                             <div class="form-group col-md-4">
                                                                <asp:label ID="Label55" runat="server" class="col-form-label">Kecamatan</asp:label>
                                                                 <asp:TextBox ID="txtkecamatanpekerjaan" Text="-" MaxLength="50" CssClass="form-control"  runat="server" ></asp:TextBox>
                                                                                
                                                            </div>  
                                                            </div> 

                                                  <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                          
                                                                <asp:label ID="Label56" runat="server" class="col-form-label">Provinsi</asp:label>
																<asp:DropDownList ID="ddlprovinsipekerjaan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  ></asp:DropDownList>
                                                                                
															</div>
                                                            
                                                              <div class="form-group col-md-4">
                                                                <asp:label ID="Label57" runat="server" class="col-form-label">Kode Pos</asp:label>
																<asp:TextBox ID="txtkodepospekerjaan" MaxLength="6" Text="0" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                 
													                              
                                                             </div>
                                                      </div>
                                                              <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label58" runat="server" class="col-form-label">Telepon</asp:label>
																<asp:TextBox ID="txtteleponpekerjaan" MaxLength="13" Text="0" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                                                            
                                                              </div>
                                                              <div class="form-group col-md-4">
                                                                <asp:label ID="Label59" runat="server" class="col-form-label">Fax</asp:label>
																<asp:TextBox ID="txtFaxPekerjaan" Text="0" MaxLength="10" CssClass="form-control" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
                                                    
                                                              </div>
                                                             </div>





                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA PENDAPATAN / SUMBER DANA NASABAH</div>
                                                <p class="text-muted font-14 m-b-30"></p>

                                                     <div class="form-group">
                                                                <asp:label ID="Label60" runat="server" class="col-form-label">Penghasilan Gaji Kotor Per Tahun</asp:label>
																<asp:DropDownList ID="ddlgajikotor" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                                                                    </div>
															
															 <div class="form-group">
                                                                <asp:label ID="Label61" runat="server" class="col-form-label">Sumber Penghasilan</asp:label>
																<asp:DropDownList ID="ddlsumberpenghasilan" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"  AutoPostBack="true" OnSelectedIndexChanged ="ddlsumberpenghasilan_SelectedIndexChanged"    ></asp:DropDownList>
                                                                 <asp:TextBox ID="txtsumberpenghasilan" MaxLength="50" Text="-" CssClass="form-control" runat="server" required="required" ></asp:TextBox>
                                                                                   
                                                             </div>

                                                <div class="form-group">
                                                                <asp:label ID="Label62" runat="server" class="col-form-label">Transaksi Per Hari</asp:label>
																<asp:DropDownList ID="ddltransaksiperhari" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                        </div>
                                                         
                                                       

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                    
                                     
                                                   </div>  

                                            </div>
                                         </div>        
                             
                    
                    </asp:Panel>
             

            
                 <!-- Panel Edit -->
             
                 <asp:Panel ID="pnlPihakYgDapatDiHubungi" Visible="false" runat="server">
                      
            <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">INFORMASI DATA PIHAK LAIN YANG DAPAT DI HUBUNGI / DATA LAIN NASABAH</div>
                                                       <p class="m-b-0"></p>
                                 
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA PIHAK LAIN YANG DAPAT DI HUBUNGI</div>
                                                 <p class="text-muted font-14 m-b-30"></p>
                                                     
                                                      <div class="form-row">

                                                            <div class="form-group col-md-6">
                                                                <asp:label ID="Label69" runat="server" class="col-form-label">Nama</asp:label>
                                                                 <asp:TextBox ID="txtnamapihaklain" Text="-" MaxLength="70" CssClass="form-control"  runat="server" ></asp:TextBox> 
                                                                
                                                            
                                                             </div>  
															
															<div class="form-group col-md-6">
                                                                <asp:label ID="Label70" runat="server" class="col-form-label">Hubungan</asp:label>
																<asp:DropDownList ID="ddlHubunganPihakLain" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                               

                                                             </div>
                                                             




                                                            
                                                      </div>    









                                                        <div class="form-group">
                                                                <asp:label ID="Label71" runat="server" class="col-form-label">Alamat </asp:label>
                                                                 <asp:TextBox ID="txtalamatPihakLain" MaxLength='150' Text="-" onkeyDown="checkTextAreaMaxLength(this,event,'150');" CssClass="form-control"  runat="server" TextMode ="MultiLine" Columns="55" ></asp:TextBox> 
                                                            
                                                            
                                                              </div> 




                                                 <div class="form-row">
                                                            <div class="form-group col-md-3">
                                                                <asp:label ID="Label72" runat="server" class="col-form-label">RT</asp:label>
                                                                 <asp:TextBox ID="txtRTPihakLain" Text="-" CssClass="form-control" MaxLength="3" runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                             
                                                             </div>  

                                                            
                                                             <div class="form-group col-md-3">
                                                                <asp:label ID="Label73" runat="server" class="col-form-label">RW</asp:label>
                                                                 <asp:TextBox ID="txtRWPihakLain" Text="-" CssClass="form-control"  MaxLength="3" runat="server" onkeypress="return isnumerickey(event);"  ></asp:TextBox> 
                                                            
                                                             </div>
                                                      <div class="form-group col-md-4">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Kelurahan</asp:label>
                                                                 <asp:TextBox ID="txtKelurahanPihakLain" Text="-" MaxLength="50" CssClass="form-control"  runat="server" ></asp:TextBox> 
                                                              
                                                            </div>  


                                                             </div>  
                                                         
                                                
                                                 <div class="form-row">
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label75" runat="server" class="col-form-label">Kecamatan</asp:label>
                                                                 <asp:TextBox ID="txtKecamatanPihakLain"  Text="-"  MaxLength="50" CssClass="form-control"  runat="server" ></asp:TextBox> 
                                                            
                                                            </div>  
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label76" runat="server" class="col-form-label">Kota</asp:label>
                                                                 <asp:TextBox ID="txtKotaPihakLain" Text="-"  MaxLength="50" CssClass="form-control"  runat="server" ></asp:TextBox> 
                                                             
                                                            </div>  
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label77" runat="server" class="col-form-label">Provinsi</asp:label>
                                                                <asp:DropDownList ID="ddlProvinsiPihakLain"  CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                          
                                                            </div>  


                                                     </div> 

                                                 <div class="form-row">
                                                        <div class="form-group col-md-4">
                                                                <asp:label ID="Label78" runat="server" class="col-form-label">Kode Pos</asp:label>
                                                                 <asp:TextBox ID="txtKodePosPihakLain" Text="0"  MaxLength="6" CssClass="form-control"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                               
                                                            </div>  
                                                            <div class="form-group col-md-4">
                                                                <asp:label ID="Label79" runat="server" class="col-form-label">No.Telepon</asp:label>
                                                                 <asp:TextBox ID="txtTlpPihakLain" Text="0"  MaxLength="13" CssClass="form-control"  runat="server" onkeypress="return isnumerickey(event);" ></asp:TextBox> 
                                                             
                                                            </div>  


                                                      </div> 


                                            </div>
                                        </div>

                                        
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="col-lg-12">
                                            <div class="card-box ribbon-box">
                                                <div class="ribbon ribbon-custom">DATA LAIN NASABAH</div>
                                                <p class="text-muted font-14 m-b-30"></p>

                                                   <div class="form-row">
															
											              <div class="form-group col-md-4">
                                                            <asp:label ID="Label64" runat="server" class="col-form-label">Tujuan Pembukaan Rekening</asp:label>
											                <asp:DropDownList ID="ddlTujuanbukaRekening" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddlTujuanbukaRekening_SelectedIndexChanged"  ></asp:DropDownList>
                                                            <asp:TextBox ID="txttujuanbukarekening" MaxLength="100" Text="-" CssClass="form-control" runat="server" required="required"  ></asp:TextBox>
                                                                  
                                                        </div> 
                                                        
                                                          <div class="form-group col-md-4">
                                                            <asp:label ID="Label63" runat="server" class="col-form-label">Dalam Hal Bertindak</asp:label>
											                <asp:DropDownList ID="ddldalamhalbertindak" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged ="ddldalamhalbertindak_SelectedIndexChanged"   ></asp:DropDownList>
                                                                   <asp:TextBox ID="txtdalamhalbertindak" Text="-" MaxLength="100" CssClass="form-control" runat="server" required="required"  ></asp:TextBox>
                                                                      
                                                        </div>
                                                             
                                                       
                                                    </div>

                                                <div class="form-group">
                                                                <asp:label ID="Label66" runat="server" class="col-form-label">Nama Ibu Kandung</asp:label>
                                                                 <asp:TextBox ID="txtnamaibukandung" Text="-" MaxLength="70" CssClass="form-control"  runat="server" ></asp:TextBox> 
                                                       
                                                          
                                                         </div>  

                                                      
                                                         
                                                        <div class="form-group">
                                                            <asp:label ID="Label68" runat="server" class="col-form-label">Kena Pajak</asp:label>
											                <asp:DropDownList ID="ddlKenaPajak" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" ></asp:DropDownList>
                                                           
                                                        </div>
                                                         
                                                            <div class="form-group">
                                                                <asp:label ID="Label87" runat="server" class="col-form-label">Alamat Surat</asp:label>
																<asp:DropDownList ID="ddlAlamatSurat" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom"   ></asp:DropDownList>
                                                              

                                                                 
                                                            </div> 
                                                              <div class="form-group">
                                                                <asp:label ID="Label83" runat="server" class="col-form-label">Jumlah Anak</asp:label>
																<asp:TextBox ID="txtjumlahanak" CssClass="form-control" Text="0" MaxLength="2" onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>
															  
                                                            
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

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers> 
                <%--<asp:PostBackTrigger ControlID="btnAdd" />--%>   
                
                <asp:PostBackTrigger ControlID="GRData" />
    
                 

              

                 
                 
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
