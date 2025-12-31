<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCardAR.aspx.cs" Inherits="AR_View_ViewCardAR" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Melihat Kartu Supplier</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <!-- Plugins css-->
    <link href="/plugins/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />

    <!-- App css -->
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="/assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/assets/js/modernizr.min.js"></script>

    <link href="/assets/css/jquery-ui.css" rel="stylesheet" />

    <link href="/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- Sweet Alert css -->
    <link href="/plugins/sweet-alert/sweetalert2.min.css" rel="stylesheet" type="text/css" />


    <!-- jQuery  -->
    <script src="/Javascript/jquery-1.12.0.min.js"></script>
    <script src="/assets/js/popper.min.js"></script>
    <script src="/assets/js/bootstrap.min.js"></script>
    <script src="/assets/js/waves.js"></script>
    <script src="/assets/js/jquery.slimscroll.js"></script>

    <script src="/Javascript/jquery-ui.js"></script>

        <!-- Toastr css -->
    <link href="/plugins/jquery-toastr/jquery.toast.min.css" rel="stylesheet" />
    
        <!-- Modal -->
    <link href="/plugins/custombox/css/custombox.min.css" rel="stylesheet" />
    
    <!-- Tooltipster css -->
    <link rel="stylesheet" href="/plugins/tooltipster/tooltipster.bundle.min.css" />
        <!-- DataTables -->
    <link href="/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <!-- Responsive datatable examples -->
    <link href="/plugins/datatables/responsive.bootstrap4.min.css" rel="stylesheet" type="text/css" />

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
                background: white url("/assets/images/Cube-1s-30px.gif") right center no-repeat;
            }
        </style>

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

        
        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

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

       

        $(document).ready(InIEvent);
        function InIEvent() { 

            $("div").remove(".tooltip");
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

            
                $("#txtkodesupplier").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "ViewCardAR.aspx/GetKodeLgn",
                            data: "{'InputText':'" + document.getElementById('txtkodesupplier').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                             dataType: "json",
                            success: function (data) {
                                obj = JSON.parse(stringToArray(data.d));
                                console.log(obj);
                                var found = $.map(obj, function (item) {
                                    return {
                                        label: item[1],  
                                        hdnZipCode: item[0]
                                    }
                                });

                                $("[id$=hdnZipCode]").val('');

                                response(found);
                            },

                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("[id$=hdnZipCode]").val(i.item.hdnZipCode);
                    },
                    minLength: 3
                });
          

            
            }
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
             $(function () {
                var table = $('#GRTabungan').dataTable({
                    "order": [],
                    "columnDefs": [{ "targets": 'no-sort', "orderable": false }, { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }],
                    ordering: [[0, 'asc']],
                    "scrollX": true,
                    retrieve: true                     
                });
            });
             $(function () {
                var table = $('#GrdPinjaman').dataTable({
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
                                    <li class="breadcrumb-item"><a href="#">Home</a></li>
                                    <li class="breadcrumb-item"><a href="#">Account Receivable</a></li>
                                    <li class="breadcrumb-item"><a href="#">View</a></li>
                                    <li class="breadcrumb-item"><a href="MelihatKartuSupplier.aspx">Melihat Kartu AR</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">View Card Anggota</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
               <asp:Panel ID="pnlFilter" Visible="true" runat="server">
                     <div class="row">
                        <div class="col-lg-6">
                            <div class="card-box ribbon-box">
                              <div class="ribbon ribbon-custom">Filter</div>
                                <p class="text-muted font-14 m-b-30">
                                  <div class="form-row">
                                    <div class="form-group col-md-3">
                                         <label for="inputState" class="col-form-label">Kode Anggota</label>
                                         <asp:HiddenField ID="hdnZipCode" runat="server" />
                                     </div>
                                     <div class="form-group col-md-12">
                                         <asp:TextBox CssClass="form-control" ID="txtkodesupplier" Enabled="true" runat="server" required="required" ></asp:TextBox>
                                         <span id="lblsupplier" visible="false" class="text-muted" style="font-size: 10px"></span>
                                     </div>  
                                     <div class="form-group col-md-6">
                                        <label for="inputPassword2" class="sr-only">Search</label>
                                        <asp:Button ID="btnSearch" class="btn btn-success waves-light text-white" runat="server" Text="Search"  OnClick="btnSearch_Click"/>                                              
                                     </div>
                                 </div>
                            </div> 
                        </div> 
                    </div>

                 </asp:Panel>
                 <!-- Panel Header -->
          <asp:Panel ID="Panel1" Visible="false" runat="server">
            <div class="row">

                  <div class="col-lg-6">
                     <div class="col-lg-12">
                          <div class="card-box ribbon-box">
                                <div class="ribbon ribbon-custom">Data Anggota</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                                                        
                                         <div class="form-group">
                                                    <asp:label id="Label1" runat="server"   class="col-form-label">Nama Anggota</asp:label>
                                                    <asp:TextBox ID="txtnamasupplier"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                            </div>                                        
                                            <div class="form-group">
                                                <asp:label id="Label2" runat="server"  class="col-form-label">Kode CRC</asp:label>
                                                <asp:TextBox ID="txtkodecrc"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                            </div>
                                            <div class="form-group">
                                                <asp:label id="Label11" runat="server"  class="col-form-label">Sales</asp:label>
                                                <asp:TextBox ID="txtnamasales"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                            </div>
                                            <div class="Tanggal Daftar">
                                                <asp:label id="Label16" runat="server"  class="col-form-label">Tgl Daftar</asp:label>
                                                <asp:TextBox ID="txttgldaftar"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"></asp:TextBox>   
                                            </div>                                    
                                            <div class="form-group">
                                                        <asp:label id="Label4" runat="server"   class="col-form-label">Tgl Pinjaman Akhir</asp:label>
                                                        <asp:TextBox ID="txtTglPinjamanAhr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                                </div>
                                            <div class="form-group">
                                                        <asp:label id="Label7" runat="server"   class="col-form-label">Pokok Pinjaman</asp:label>
                                                 </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label5" runat="server"   class="col-form-label">JPokok Tahun</asp:label>
                                                        <asp:TextBox ID="txtJPokokThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label3" runat="server"   class="col-form-label">JPokok Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJPokokAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label6" runat="server"   class="col-form-label">JPokok Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJPokokBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                 </div>

                                                    


                                                <div class="form-group">
                                                        <asp:label id="Label8" runat="server"   class="col-form-label">Bunga Pinjaman</asp:label>
                                                 </div>
                                                <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label9" runat="server"   class="col-form-label">JBunga Tahun</asp:label>
                                                        <asp:TextBox ID="TxtJBungaThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label10" runat="server"   class="col-form-label">JBunga Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJBungaAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label12" runat="server"   class="col-form-label">JBunga Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJBungaBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div> 
                                                 </div> 
                                                <div class="form-group">
                                                        <asp:label id="Label13" runat="server"   class="col-form-label">Total Pinjaman</asp:label>
                                                 </div>
                                                    <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label14" runat="server"   class="col-form-label">JPinjaman Tahun</asp:label>
                                                        <asp:TextBox ID="txtJPinjamanThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label15" runat="server"   class="col-form-label">JPinjaman Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJPinjamanAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label17" runat="server"   class="col-form-label">JPinjaman Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJPinjamanBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                            </div>

                                                        
                                                    <div class="form-group">
                                                        <asp:label id="Label71" runat="server"   class="col-form-label">Bayar Pinjaman</asp:label>
                                                 </div>
                                                <div class="form-group">
                                                        <asp:label id="Label18" runat="server"   class="col-form-label">Tgl Bayar Akhir</asp:label>
                                                        <asp:TextBox ID="txtTglBayarPinjamanAhr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                 <div class="form-group">
                                                        <asp:label id="Label19" runat="server"   class="col-form-label">Bayar Pokok Pinjaman</asp:label>
                                                 </div>
                                                        <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label20" runat="server"   class="col-form-label">JBayar Pokok Tahun</asp:label>
                                                        <asp:TextBox ID="txtJBayarPokokThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label21" runat="server"   class="col-form-label">JBayar Pokok Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJBayarPokokAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label22" runat="server"   class="col-form-label">JBayar Pokok Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJBayarPokokBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div> 
                                                 </div>


                                                <div class="form-group">
                                                        <asp:label id="Label23" runat="server"   class="col-form-label">Bayar Bunga Pinjaman</asp:label>
                                                 </div>
                                                    <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label24" runat="server"   class="col-form-label">JBayar Bunga Tahun</asp:label>
                                                        <asp:TextBox ID="txtJBayarBungaThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label25" runat="server"   class="col-form-label">JBayar Bunga Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJBayarBungaAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label26" runat="server"   class="col-form-label">JBayar Bunga Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJBayarBungaBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div> 
                                                        </div> 
                                    
                                                <div class="form-group">
                                                        <asp:label id="Label27" runat="server"   class="col-form-label">Total Bayar Pinjaman</asp:label>
                                                 </div>
                                                         <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label28" runat="server"   class="col-form-label">JBayar Tahun</asp:label>
                                                        <asp:TextBox ID="TxtJBayarPinjamanThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label29" runat="server"   class="col-form-label">JBayar Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="TxtJBayarPinjamanAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label30" runat="server"   class="col-form-label">JBayar Bulan Ini</asp:label>
                                                        <asp:TextBox ID="TxtJBayarPinjamanBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div> 
                                              </div> 
                                    
                                                <div class="form-group">
                                                        <asp:label id="Label79" runat="server"   class="col-form-label">Saldo Anggota</asp:label>
                                                 </div>

                                                <div class="form-row">                                    
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label33" runat="server"   class="col-form-label">Saldo Awal Pokok</asp:label>
                                                        <asp:TextBox ID="txtSaldoAwalPokok"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label34" runat="server"   class="col-form-label">Saldo Jalan Pokok</asp:label>
                                                        <asp:TextBox ID="txtSaldoJalanPokok"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label31" runat="server"   class="col-form-label">Saldo Awal Bunga</asp:label>
                                                        <asp:TextBox ID="txtSaldoAwalBunga"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                    </div>

                                                       <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label32" runat="server"   class="col-form-label">Saldo Jalan Bunga</asp:label>
                                                        <asp:TextBox ID="txtSaldoJalanBunga"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label35" runat="server"   class="col-form-label">Saldo Awal Pinjaman</asp:label>
                                                        <asp:TextBox ID="txtSaldoAwalPinjaman"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label36" runat="server"   class="col-form-label">Saldo Jalan Pinjaman</asp:label>
                                                        <asp:TextBox ID="txtSaldoJalanPinjaman"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                           </div>  
                                    



                                    </div>
                                
                        </div>                                                    
                     </div>



                <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Cadangan & Denda & Simpanan Pokok</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                                                 <div class="form-row">   
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label39" runat="server"   class="col-form-label">JCadangan Tahun</asp:label>
                                                        <asp:TextBox ID="txtJCadanganThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label40" runat="server"   class="col-form-label">JCadangan Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJCadanganakm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label41" runat="server"   class="col-form-label">JCadangan Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJCadanganBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  
                                                     </div>


                                                <div class="form-group">
                                                        <asp:label id="Label42" runat="server"   class="col-form-label">Distribusi Cadangan</asp:label>
                                                 </div>
                                                     
                                                    <div class="form-group">
                                                        <asp:label id="Label37" runat="server"   class="col-form-label">Tgl Dst Cadangan</asp:label>
                                                        <asp:TextBox ID="txtTglDstCadanganAhr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"  ></asp:TextBox>   
                                                </div>
                                                     <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label43" runat="server"   class="col-form-label">JDistribusi Cadangan Tahun</asp:label>
                                                        <asp:TextBox ID="txtJDstCadanganThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label44" runat="server"   class="col-form-label">JDistribusi Cadangan Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJDstCadanganAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label45" runat="server"   class="col-form-label">JDistribusi Cadangan Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJDstCadanganBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  
                                                         </div>  
                                                         
                                                <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label47" runat="server"   class="col-form-label">Saldo Awal Cadangan</asp:label>
                                                        <asp:TextBox ID="txtSaldoAwalCadangan"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label48" runat="server"   class="col-form-label">Saldo Jalan Cadangan</asp:label>
                                                        <asp:TextBox ID="txtSaldoJalanCadangan"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                    </div>  
                                                <div class="form-group">
                                                        <asp:label id="Label80" runat="server"   class="col-form-label">Denda Anggota</asp:label>
                                                 </div>

                                    
                                                <div class="form-group">
                                                        <asp:label id="Label57" runat="server"   class="col-form-label">Tgl Denda Akhir</asp:label>
                                                        <asp:TextBox ID="txtTglDendaahr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                                </div>

                                                <div class="form-row">
                                                
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label38" runat="server"   class="col-form-label">JDenda Tahun</asp:label>
                                                        <asp:TextBox ID="txtJDendaThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label46" runat="server"   class="col-form-label">JDenda Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJDendaAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label49" runat="server"   class="col-form-label">JDenda Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJDendaBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  
                                                    </div>  


                                                <div class="form-group">
                                                        <asp:label id="Label50" runat="server"   class="col-form-label">Bayar Denda</asp:label>
                                                 </div>
                                                    <div class="form-group">
                                                        <asp:label id="Label51" runat="server"   class="col-form-label">Tgl Bayar Denda</asp:label>
                                                        <asp:TextBox ID="txtTglBayarDendaAhr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"  ></asp:TextBox>   
                                                </div>
                                                    <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label52" runat="server"   class="col-form-label">JBayar Denda Tahun</asp:label>
                                                        <asp:TextBox ID="TxtJBayarDendaThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label53" runat="server"   class="col-form-label">JBayar Denda Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJBayarDendaAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label54" runat="server"   class="col-form-label">JBayar Denda Bulan Ini</asp:label>
                                                        <asp:TextBox ID="TxtJBayarDendaBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div> 
                                                        </div>  
                                    
                                                <div class="form-group">
                                                        <asp:label id="Label64" runat="server"   class="col-form-label">Batal Denda</asp:label>
                                                 </div>
                                                    <div class="form-group">
                                                        <asp:label id="Label70" runat="server"   class="col-form-label">Tgl Batal Denda</asp:label>
                                                        <asp:TextBox ID="txtTglBatalDendaAhr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"  ></asp:TextBox>   
                                                </div>
                                                        <div class="form-row">
                                                    <div class="form-group col-md-4">
                                                        <asp:label id="Label58" runat="server"   class="col-form-label">JBatal Denda Tahun</asp:label>
                                                        <asp:TextBox ID="TxtJBatalDendaThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label59" runat="server"   class="col-form-label">JBatal Denda Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="TxtJBatalDendaAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label60" runat="server"   class="col-form-label">JBatal Denda Bulan Ini</asp:label>
                                                        <asp:TextBox ID="TxtJBatalDendaBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                            </div>  
                                                <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label55" runat="server"   class="col-form-label">Saldo Awal Denda</asp:label>
                                                        <asp:TextBox ID="txtSaldoAwalDenda"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label56" runat="server"   class="col-form-label">Saldo Jalan Denda</asp:label>
                                                        <asp:TextBox ID="txtSaldoJalanDenda"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  
                                                    </div> 
                                    
                                                <div class="form-group">
                                                        <asp:label id="Label81" runat="server"   class="col-form-label">Simpanan Pokok</asp:label>
                                                 </div>

                                    
                                                <div class="form-group">
                                                        <asp:label id="Label61" runat="server"   class="col-form-label">Tgl Simpanan Pokok</asp:label>
                                                        <asp:TextBox ID="txtTglsimpokAhr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" ></asp:TextBox>   
                                                </div>
                                                <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label62" runat="server"   class="col-form-label">Simpanan Pokok</asp:label>
                                                        <asp:TextBox ID="txtsimpananpokok"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label63" runat="server"   class="col-form-label">Simpanan Wajib</asp:label>
                                                        <asp:TextBox ID="txtsimpananwajib"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  
                                                    </div>                                                 
                                                <div class="form-group">
                                                        <asp:label id="Label65" runat="server"   class="col-form-label">Setoran Anggota</asp:label>
                                                 </div>
                                                    <div class="form-group">
                                                        <asp:label id="Label66" runat="server"   class="col-form-label">Tgl Setoran</asp:label>
                                                        <asp:TextBox ID="txtTglSetoranahr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"  ></asp:TextBox>   
                                                </div>
                                                <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label67" runat="server"   class="col-form-label">JSetoran Tahun</asp:label>
                                                        <asp:TextBox ID="txtJSetoranThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label68" runat="server"   class="col-form-label">JSetoran Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="TxtJSetoranAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label69" runat="server"   class="col-form-label">JSetoran Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJSetoranBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>   
                                                     </div> 
                                                <div class="form-group">
                                                        <asp:label id="Label72" runat="server"   class="col-form-label">Tarikan Anggota</asp:label>
                                                 </div>
                                                    <div class="form-group">
                                                        <asp:label id="Label75" runat="server"   class="col-form-label">Tgl Tarikan</asp:label>
                                                        <asp:TextBox ID="txtTglTarikanAhr"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom"  ></asp:TextBox>   
                                                </div>
                                                     <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label76" runat="server"   class="col-form-label">JTarikan Tahun</asp:label>
                                                        <asp:TextBox ID="TxtJTarikanThn"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label77" runat="server"   class="col-form-label">JTarikan Bulan Lalu</asp:label>
                                                        <asp:TextBox ID="txtJTarikanAkm"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>     
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label78" runat="server"   class="col-form-label">JTarikan Bulan Ini</asp:label>
                                                        <asp:TextBox ID="txtJTarikanBln"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  
                                                         </div> 
                                            <div class="form-row">
                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label73" runat="server"   class="col-form-label">Saldo Awal Setoran</asp:label>
                                                        <asp:TextBox ID="txtSaldoAwalSetoran"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>

                                                <div class="form-group col-md-4">
                                                        <asp:label id="Label74" runat="server"   class="col-form-label">Saldo Jalan Setoran</asp:label>
                                                        <asp:TextBox ID="txtSaldoJalanSetoran"  CssClass="form-control" Enabled="false" runat="server" data-style="btn-custom" onblur="checkTextField1(this);" ></asp:TextBox>   
                                                </div>  
                                                 </div> 
                                                 

                                                 

                                                 
   
                                </div>                                                    
                            </div>
                           </div>
      
                   </div>
                </asp:Panel>

                 <asp:Panel ID="Pnltagihan" Visible="True" runat="server">
                        
        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card-box table-responsive">
                                                     <label class="text-uppercase m-b-5 font-13 font-600">REKENING PINJAMAN</label>
                                                    <div class="text-center mt-4 mb-4">
                                                    
<div class="form-row">
                                        <div class="form-group" style="width: 98%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GrdPinjaman" runat="server"  AutoGenerateColumns="false" CssClass="table table-bordered table-bordered dt-responsive nowrap"  ShowHeaderWhenEmpty ="True" AllowSorting="True" PageSize="50">
                                                
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="NO. Rekening">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltgl_drop" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "tgl_drop","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Produk">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkode_produk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kode_produk")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                      
                                                <asp:TemplateField HeaderText="Pokok">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblttlpokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ttlpokok","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                    <asp:Label ID="lbltotaldebet" runat="server"  />
                                                         </FooterTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bunga">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblttlbunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ttlbunga","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <asp:Label ID="lbltotalkredit" runat="server"  />
                                                         </FooterTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Angs Pokok">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblangspokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angspokok","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                   
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Angs Bunga">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblangsbunga" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "angsbunga","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                   
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="bakidb">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbakidb" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bakidb","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                   
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status Pelunasan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus_pelunasan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status_pelunasan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                   
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sales">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkd_staff" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "kd_staff")%>'></asp:Label>
                                                        </ItemTemplate>
                                                   
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblby_cadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "by_cadangan","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>                                                          
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Mutasi Cadangan">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmutasi_cadangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mutasi_cadangan","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>                                                          
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Denda">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldenda" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "denda","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>                                                          
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Mutasi Denda">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmutasidenda" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mutasidenda","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>                                                          
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>

                                    </div>

                                                </div>
                                            </div>
                                            </div>
                                        </div>
        
        
        </asp:Panel>

                 

                 <asp:panel ID="Panel4" Visible ="true" runat ="server">
                     <div class="row clearfix">
                            <div class="auto-style1"> 
                                <div class="card-box">  
                                    <h4 class="Page-title">Rekening Simpanan Anggota</h4>
                                    <div class="form-row">
                                        <div class="form-group" style="width: 100%;">
                                            <%--<div class="card-box table-responsive">--%>
                                            <asp:GridView ID="GRTabungan" runat="server" ShowFooter="True" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable"  ShowHeaderWhenEmpty ="True" AllowSorting="True" PageSize="50">
                                                
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="Nomor Rekening">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_rekening")%>'></asp:Label>
                                                        </ItemTemplate>
                                                         
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tgl Daftar">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDateAdded" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DateAdded","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                                                           
                                                <asp:TemplateField HeaderText="Saldo Awal Pokok">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsaldo_awal_pokok" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "saldo_awal_pokok","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Saldo Awal Wajib">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkredit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "saldo_awal_wajib","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>    
                                                     <asp:TemplateField HeaderText="Mutasi Debet bln">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmutasi_debet_bln" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mutasi_debet_bln","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Mutasi Kredit Bln">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmutasi_kredit_bln" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mutasi_kredit_bln","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Saldo Akhir Bulan Lalu">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSaldo_Akhir_Akm" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Saldo_Akhir_Akm","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Simpanan Anggota">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsaldo_tabungan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "saldo_tabungan","{0:N2}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                      
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>

                                    </div>
                                </div>
                         </div>
                     </div>
                 </asp:panel>


                  <!-- Panel Filter date -->
               <asp:Panel ID="Panel3" Visible="false" runat="server">
                     <div class="row">
                        <div class="col-lg-12">
                            <div class="card-box ribbon-box">
                               <div class="ribbon ribbon-custom">Filter Date</div>
                                 <p class="m-b-0">
                                     <div class="row">
                                         <div class="col-md-12">
                                             <div class="card-box">
                                                 <div class="form-row">
                                                      <div class="form-group col-md-4">
                                                         <label for="inputState" class="col-form-label">This Month From</label>
                                                          <asp:TextBox ID="txtStart" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>     
                                                     </div>
                                                     <div class="form-group col-md-4">
                                                         <label for="inputZip" class="col-form-label">This Month To</label>
                                                         <asp:TextBox ID="txtEnd" CssClass="form-control" runat="server"  Type="date"></asp:TextBox>  
                                                     </div>
                                                  </div>
                                                     <div class="form-group"> 
                                                             <asp:Button ID="btnDetilSupplier" class="btn btn-success waves-light text-white" runat="server" Text="Detail Anggota"  OnClick="btnDetilSupplier_Click" /> 
                                                     </div>  
                                             </div>
                                           </div>
                                    </div> 
                            </div> 
                        </div> 
                    </div>

                 </asp:Panel>
                 <!-- Panel Front -->



       <asp:Panel ID="PnlFront" runat="server" Visible="false">
            <div class="row">
                    <div class="col-md-12">
                        <div class="card-box">

                            <h4 class="m-t-0 header-title">Melihat Kartu Anggota</h4>                           
                                     <div class="form-row">
                                  <div class="form-group" style="width: 100%;">

                                 <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" OnRowCommand="GRData_RowCommand" OnRowDataBound="GRData_RowDataBound" ShowHeaderWhenEmpty ="True" AutoGenerateColumns="False" Width="625px" ShowFooter="True">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Tgl.Faktur">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljenisgol" runat="server"  Text='<%# Eval("TglTrnFaktur", "{0:dd-MM-yyyy}") %>' ></asp:Label>
                                         </ItemTemplate>
                                        <FooterTemplate>
                                         <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                         </FooterTemplate>
                                        <HeaderStyle Height="5px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nomor Faktur">
                                        <ItemTemplate>
                                            <asp:Label ID="Lblnofaktur" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoFaktur")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="5px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TypeTrn")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nomor Bukti">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tgl. Transaksi">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltgltransaksi" runat="server" DateFormat="dd-mm-yyyy" Text='<%# Eval("TglTrn", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tgl Jth Tmp">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltgljthtempo" runat="server" DateFormat="dd-mm-yyyy" Text='<%# Eval("TglJthTmp", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jumlah Cair">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlcair" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JumlahTrn","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                          <asp:Label ID="lbltotalcair" runat="server"  />
                                          </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>

                                  <%--  <asp:TemplateField HeaderText="Jumlah Giro">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmlgiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "JumlahTrn")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Jlh Cadangan">
                                        <ItemTemplate>
                                            <asp:Label ID="lbljmluangmnuka" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jumlahumk","{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="lbltotalumk" runat="server"  />
                                          </FooterTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Keterangan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblketerangan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Keterangan")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Bank Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbankcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BankGiro")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Giro">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgiro" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Nogiro")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Height="30px" CssClass="align-center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <%--<asp:BoundField DataField="JumlahTrn" HeaderText="Class" FooterText="Total" />
                                    <asp:BoundField DataField="JumlahTrn" HeaderText="Students" />--%>
                                </Columns>
                            </asp:GridView>                           
                        </div>
                    </div>
                </div>
            </div>
        </div>

        </asp:Panel>
                 <!-- Panel Edit -->
              
                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
                 <Triggers>
                   <asp:PostBackTrigger ControlID="txtkodesupplier" />
                     <asp:PostBackTrigger ControlID="btnSearch" />
                   <asp:PostBackTrigger ControlID="btnDetilSupplier" /> 
                </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  <script>
      function deletealert(ctl, event) {
    // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
          var defaultAction = $(ctl).prop("href");
          var number=document.getElementById("txtjenisgol").value;
    // CANCEL DEFAULT LINK BEHAVIOUR
    event.preventDefault();
    swal({
        
                     title: "Data berhasil disimpan",
                     text: 'Nomer Anggota anda :' + number ,
        type: "success",
        position: "top-end",
               //   icon: "success",
                  showConfirmButton: false,
                  timer: 1500
        //showCancelButton: true,
        //confirmButtonColor: "#DD6B55",
        //confirmButtonText: "Yes, delete it!",
        //cancelButtonText: "No, cancel plx!",
        //closeOnConfirm: false,
        //closeOnCancel: false
    },
    function () {
      // if (isConfirm) {
        //    swal({ title: "Deleted!", text: "Your imaginary file has been deleted.", type: "success", confirmButtonText: "OK!", closeOnConfirm: false },
           // function (isConfirm) {
                // RESUME THE DEFAULT LINK ACTION
                window.location.href = defaultAction;
                return true;
            //});
       // } else {
        //    swal("Cancelled", "Your imaginary file is safe :)", "error");
         //   return false;
       // }
    });
}
      //function myFunction(ctl, event) {
      //            Swal.fire({
      //              position: "top-end",
      //              icon: "success",
      //                title: "Data berhasil disimpan",
      //                text: 'Nomer nasabah anda :' + document.getElementById("#txtjenisgol").value(),
      //              showConfirmButton: false,
      //              timer: 1500
      //            });
      //          }
      </script>
    
    <!-- Parsley js -->
    <script type="text/javascript" src="/plugins/parsleyjs/parsley.min.js"></script>

    <script src="/plugins/bootstrap-select/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <!-- Sweet Alert Js  -->
    <script src="/plugins/sweet-alert/sweetalert2.min.js"></script>
    <!-- Tooltipster js -->
<script src="/plugins/tooltipster/tooltipster.bundle.min.js"></script>

<!-- Required datatable js -->
<script src="/plugins/datatables/jquery.dataTables.min.js"></script>
<script src="/plugins/datatables/dataTables.bootstrap4.min.js"></script>

<!-- Responsive examples -->
<script src="/plugins/datatables/dataTables.responsive.min.js"></script>
<script src="/plugins/datatables/responsive.bootstrap4.min.js"></script>

<!-- App js -->
<script src="/assets/js/jquery.app.js"></script>



<!-- Toastr js -->
<script src="/plugins/jquery-toastr/jquery.toast.min.js" type="text/javascript"></script>



</html>
