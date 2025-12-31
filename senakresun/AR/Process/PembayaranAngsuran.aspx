<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PembayaranAngsuran.aspx.cs" Inherits="AR_Process_PembayaranAngsuran" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pembayaran Angsuran Pinjaman</title>
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

        function Simpan() {

            setTimeout(function () {
                swal(
                    {
                         title: "Data berhasil disimpan",  
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
        }

            function sweetAlertsave(btnProses,errMsg) {
            if (btnProses.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnProses.dataset.confirmed = false;
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
                        btnProses.dataset.confirmed = true;
                        btnProses.click();
                  
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

        $(document).ready(InIEvent);
        function InIEvent() { 

            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

              


            $(".dataTables_scrollHeadInner").css({ "width": "100%" });

            $(".table ").css({ "width": "100%" });

            $('form').parsley();
        }
 
    </script>
     <style type="text/css">
        .form-control {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
          
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate> 
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Kredit Mikro</a></li>
                                    <li class="breadcrumb-item"><a href="#">Process</a></li>
                                    <li class="breadcrumb-item"><a href="PembayaranAngsuran.aspx">Pembayaran Angsuran Pinjaman</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Pembayaran Angsuran Pinjaman</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
            <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                                      <div class="ribbon ribbon-custom">Filter</div>
                                        <p class="m-b-0">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-box">
                                                        <div class="form-row">
                                                                <div class="form-group col-md-6">
                                                                    <label class="col-form-label">Kode Cabang</label>
                                                                    <asp:TextBox CssClass="form-control" ID="txtkodecabang" Enabled="false" runat="server" required="required"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnZipCode" runat="server" />

                                                                    
                                                                </div>
                                                                <div class="form-group col-md-6">
                                                                     <div class="form-group">
                                                                        <label for="inputState" class="col-form-label">No.Rekening</label>
                                                                         <asp:TextBox CssClass="form-control" ID="txtnorekening" Enabled="true" runat="server" required="required" MaxLength ="15"></asp:TextBox>
                                                                   </div>  
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearch_Click"/>                                              
                                                                    </div>
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
             <asp:Panel ID="pnladd" Visible="false" runat="server">
              <div class="row">
                                        <div class="col-lg-12"> 
                                                   <div class="card-box ribbon-box">
                                                    <div class="ribbon ribbon-custom">DATA ANGSURAN NASABAH</div>
                                                       <p class="m-b-0"></p>
 
                                                    <div class="form-row">
                                                         
                                                        <div class="form-group col-md-6">
                                                            
                                                                 
                                                        <asp:HiddenField ID="HDsisapk" runat="server" />	 
                                                        <asp:HiddenField ID="HDnonasabah" runat="server" />	 
                                                        <asp:HiddenField ID="HDselisihdenda" runat="server" />
                                                        <asp:HiddenField ID="HDtgl_jwl" runat="server" />
                                                        <asp:HiddenField ID="HDnama_nasabah" runat="server" />
                                                        <asp:HiddenField ID="HDnama_staff" runat="server" />
                                                        <asp:HiddenField ID="HDKodeProduk" runat="server" />
                                                        <asp:HiddenField ID="HDNoBuktiSebelumnya" runat="server" />
                                                        <asp:HiddenField ID="HDJangka" runat="server" />
                                                            
                                                            
                                                            <div class="form-group">
                                                                <asp:label ID="Label43" runat="server" class="col-form-label">Angsuran Ke</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtangske"  Enabled="false" runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label1" runat="server" class="col-form-label">Nama Nasabah</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnamanasabah"  Enabled="false" runat="server"></asp:TextBox>
                                                            </div>  

                                                           <div class="form-group">
                                                                <asp:label ID="Label44" runat="server" class="col-form-label">Keterangan</asp:label>
																<asp:TextBox ID="txtketerangan"  CssClass="form-control"  TextMode="MultiLine" Rows="5" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label3" runat="server" class="col-form-label">No. Resi</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtnoresi"   runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                           
															
                                                         
                                                        </div> 
                                                        <div class="form-group col-md-6">
                                                            
                                                             <div class="form-group">
                                                                <asp:label ID="Label4" runat="server" class="col-form-label">Pokok</asp:label>
																<asp:TextBox ID="txtpokok"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>      
                                                           </div>

															<div class="form-group">
                                                                <asp:label ID="Label2" runat="server" class="col-form-label">Bunga</asp:label>
																<asp:TextBox CssClass="form-control" ID="txtbunga" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label45" runat="server" class="col-form-label">Denda</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txtdenda"  runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label86" runat="server" class="col-form-label">Total Angsuran</asp:label>
	                                                            <asp:TextBox CssClass="form-control" ID="txttotal" Enabled="false"  runat="server" required="required"></asp:TextBox>
                                                            </div> 

                                                            <div class="form-group">
                                                                <asp:label ID="Label74" runat="server" class="col-form-label">Cadangan Biaya Marketing</asp:label>
																<asp:TextBox ID="txtbymarketing" CssClass="form-control"    onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>

                                                             <div class="form-group">
                                                                <asp:label ID="Label5" runat="server" class="col-form-label">By. Marketing Mikro Umum</asp:label>
																<asp:TextBox ID="txtbymarketingumum" CssClass="form-control"   onkeypress="return isnumerickey(event);"   runat="server" ></asp:TextBox>     
                                                                 
                                                            </div>
                                                        </div>
													</div>
												</div>  

                                            </div>
                                         </div>        
                  
                                           
                                            <div class="hidden-print mt-4"> 
                                                <div class="float-right mb-2 "> 
                                                   
                                                     <asp:Button ID="btnProses" align="right" CssClass="btn btn-success waves-light text-white" runat="server" Text="PROCESS" OnClick="btnProses_Click" Width="100px"  OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin memproses data ini ?');"/> 
                                                     
                                                </div>
                                                
                                            </div> 
                                               
    
            </asp:Panel> 
                  
                 

            </div> 
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>  
                <asp:PostBackTrigger ControlID="btnSearch" />
                <asp:PostBackTrigger ControlID="btnProses" /> 
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
