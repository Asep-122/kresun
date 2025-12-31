<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasukTrnAngsuranTagihanCadangan.aspx.cs" Inherits="CL_Process_MasukTrnAngsuranTagihanCadangan" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transaksi Pembayaran Angsuran</title>
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
             function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };

        function focusOnNext(e, nextControl) {
            //alert(e.keyCode);
            if (e.keyCode == 13) {
                $("." + nextControl).focus();
                return false;
            }
            
        }
         function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: '<%=nobuktidb%>',
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 2000,
                    stack: 1,
                    afterHidden: function () {
                     <%--   if (fr == '1') {
                            setTimeout(function () { <%=btnreload()%>; }, 50);    //Save dan Edit
                        }--%>

                         if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
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
        }

        


          //numeric 1
        function Comma(Num) {

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

        //numeric 2
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


          //numeric 3
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
        var s = 0
        var v = 0
        var x = 0;
        var y = 0;
        var z = 0;
        function calc(obj) {
                var e = obj.id.toString();
            if (e == 'txttransAP') {
                x = Number(obj.value);
                y = Number(document.getElementById('txtkursstd').value);  
                v = Number(document.getElementById('txtkursstdbank').value); 
            }
            else if(e == '')
            {
                x = Number(document.getElementById('txttransAP').value);
                y = Number(obj.value); 
            } 
            z = x * y; 
            s = z / v;
            document.getElementById('txtjmlhGL').value = z; 
            document.getElementById('hfjmlrp').value = z;
            document.getElementById('txtjmlhCL').value = s;
        }
           function sweetAlertConfirm(btndelete,errMsg) {
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

        function MoveFocusOnF2(event) {
            switch (event.keyCode) {
                case 112:
                    document.getElementById("txtcustomerX").focus();
                    break;

                case 113:
                    document.getElementById("txtcustomer").focus();
                    break;            
                default:
                //nothing
            }
        }

       
        function cancelalert(errMsg) {

            swal({
                title: errMsg,
                // text: 'Your message has been succesfully sent',
                confirmButtonClass: 'btn btn-confirm mt-2',
                type: 'warning'

            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    alert("CEKDATA");
                    setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                });
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
        $(document).ready(InIEvent);
        function InIEvent() {
            $("div").remove(".tooltip");
            $.getScript("../assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });


            
            $('#txttgljthtmp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
             $('#txttgltrn').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });  
            $('.selectpicker').selectpicker();



            $('form').parsley();

               $("#txtcustomer").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "MasukTrnAngsuranTagihan.aspx/GetCustomer",
                        data: "{'InputText':'" + document.getElementById('txtcustomer').value + "','SessKdpershx':'<%=SessKdpersh%>','SessBranchIDx':'<%=SessBranchID%>'}",
                         dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                            console.log(obj);
                            var found = $.map(obj, function (item) {
                                return {
                                    label: item[2],
                                    hdncustomer: item[0],
                                    HDNoRekening: item[1],
                                    hdnkodeproduk: item[3]
                                }
                            });

                            $("[id$=hdncustomer]").val('');
                            $("[id$=HDNoRekening]").val('');
                            $("[id$=hdnkodeproduk]").val('');

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
                    $("[id$=hdncustomer]").val(i.item.hdncustomer);
                    $("[id$=HDNoRekening]").val(i.item.HDNoRekening);
                    $("[id$=hdnkodeproduk]").val(i.item.hdnkodeproduk);
                },
                minLength: 3
            });
        }
 
    </script>
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            -ms-flex: 0 0 100%;
            flex: 0 0 100%;
            max-width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
        </style>
</head>
<body onkeydown="MoveFocusOnF2(event);">
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
                                    <li class="breadcrumb-item"><a href="#">Cash Ledger</a></li>
                                    <li class="breadcrumb-item"><a href="#">Master</a></li>
                                    <li class="breadcrumb-item"><a href="PembayaranHutang.aspx">Transaksi Pembayaran Angsuran Anggota</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title">Transaksi Pembayaran Angsuran Anggota</h4>
                        </div>
                    </div>
                </div>
                 <!-- Panel Filter -->
       
                 <!-- Panel Header -->
               
                 <!-- Panel Front -->
                 
                 <!-- Panel Edit -->

            

            <asp:Panel ID="pnladd" runat="server">
              <div class="row clearfix">
                 <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       <div class="row">                          
                            <asp:HiddenField ID="HDNoNasabah" runat="server" />
                                    <asp:HiddenField ID="HDNoRekening" runat="server" />
                                    <asp:HiddenField ID="hdncustomer" runat="server" />
                                       <asp:HiddenField ID="hdnkodeproduk" runat="server" />
                                      <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="hdnjangka" runat="server" />
                                        <asp:HiddenField ID="hdnnama" runat="server" />



                          
                                

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Entry Data</div>
                                   <p class="text-muted font-14 m-b-30"></p>  
                                                            
                                     <div class="form-group">
                                        <label class="col-form-label">Entry Rekening ERP / tombol Keyboard F1 </label>
                                        <asp:TextBox CssClass="form-control" ID="txtcustomerX" runat="server" Font-Size="30px" TabIndex="0" AutoPostBack="true" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
 
                                 </div>   
                                
                                </div>                                                    
                            </div>
                           </div>

                                 

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Untuk Cek Data</div>
                                   <p class="text-muted font-14 m-b-30"></p>  
                                                            
                                      <div class="form-group">
                                        <label class="col-form-label">Cek Data / tombol Keyboard F2 </label>
                                        <asp:TextBox CssClass="form-control" ID="txtcustomer" runat="server" ></asp:TextBox>
 
                                     </div>    
                                
                                </div>                                                    
                            </div>
                           </div>


                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Entry Data</div>
                                   <p class="text-muted font-14 m-b-30"></p>  
                                                            
                                    <div class="form-row">
                                    <div class="form-group col-md-4">
                                    <label for="inputCity" class="col-form-label">Tgl Transaksi</label>
                                     <asp:TextBox ID="txttgltrn" CssClass="form-control" runat="server" MaxLength="10"  AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                    <label for="inputCity" class="col-form-label">Angsuran Ke</label>
                                    <asp:TextBox ID="txtangsuranke" onkeyup="javascript:this.value=Comma(this.value);" Text="1" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" CssClass="form-control" TabIndex="1" ></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                    <label for="inputCity" class="col-form-label">s/d Angsuran Ke</label>
                                    <asp:TextBox ID="txtangsuransd" Text="2" onkeyup="javascript:this.value=Comma(this.value);" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" onkeydown="return focusOnNext(event, 'jsTxt2')"  type="text"  required="required" placeholder="" class="form-control autonumber" runat="server" AutoPostBack="true" OnTextChanged="txtangssd_TextChanged" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                                    </div>
                                    </div>

                                    <div class="form-row">
                                    
                                    <div class="form-group col-md-4">
                                        
                                    <label for="inputpid" class="col-form-label">No. Bukti Manual</label>
                                     <asp:TextBox ID="txtnobuktimanual" CssClass="jsTxt2" runat="server" MaxLength="8" TabIndex="3" onkeydown="return focusOnNext(event, 'jsTxt4')" ></asp:TextBox>   
                                            
                                     <asp:TextBox ID="txtnobukti" Visible="false" CssClass="form-control" runat="server" MaxLength="50" ReadOnly = "true"></asp:TextBox>

                                    </div>
                                     <div class="form-group col-md-4" runat="server" visible="false">                                   
                                        <label for="inputCity" class="col-form-label">Bayar Denda</label>
                                        <asp:TextBox ID="txtdendabyr" onkeydown="return focusOnNext(event, 'jsTxt4')"  onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" AutoPostBack="true" OnTextChanged="txtjlhbayar_TextChanged" CssClass="jsTxt3" TabIndex="4"></asp:TextBox>
                                        
                                    </div>
                                    </div>
                                   <div class="form-group">
                                        <label for="inputCity" class="col-form-label">Jumlah Bayar Angsuran</label>
                                        <asp:TextBox ID="txtbayar" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);FocusNext(this);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>      
                                    </div>
                                     <div class="form-group">
                                    <asp:Button ID="btnSimpan" Width="100" class="btn btn-success waves-light text-white" runat="server" Text="Save" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Menyimpan data ini ?');" OnClick="btnSimpan_Click" BackColor="#66FF33" CssClass="jsTxt4" TabIndex="9" />
                                    </div>
                                </div>                                                    
                            </div>
                           </div>

                           <div class="col-lg-6">
                             <div class="col-lg-12">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Data Anggota</div>
                                    <p class="text-muted font-14 m-b-30"></p>                                                                           
                             

                                   
                                  
                                      <div class="form-row">

                                           <div class="form-group col-md-4">
                                    <label for="inputCity" class="col-form-label">No Rekening</label>
                                    <%--<asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtrekening" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox> 
                                </div>
                                     <div class="form-group col-md-6">
                                    <label for="inputCity" class="col-form-label">Kode Bank</label>
                                   <asp:DropDownList ID="ddlkdBank" runat="server" CssClass="selectpicker show-tick"  data-style="btn-custom">
                                                        <asp:ListItem Value="110141">110141 - Kas Teller</asp:ListItem>
                                                        <asp:ListItem Value="120241">120241 - Mandiri Tabungan Cabang</asp:ListItem>
                                                       
                                                </asp:DropDownList>        
                                         
                                     
                                     </div>


                                </div>
                                    <div class="form-row">
                                    
                      

                                <div class="form-group col-md-4">
                                    <label for="inputCity" class="col-form-label">Kode Anggota</label>
                                    <%--<asp:DropDownList ID="ddlkdsup" CssClass="selectpicker show-tick" runat="server" data-style="btn-custom" AutoPostBack="true" width ="400" OnSelectedIndexChanged ="ddlKdSup_SelectedIndexChanged"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtkodelgn" Enabled="false" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox> 
                                </div>
                                 <div class="form-group col-md-6">
                                    <label for="inputpid" class="col-3 col-form-label">Nama</label>
                                    <asp:TextBox ID="txtNmSup" Enabled="false" CssClass="form-control"  runat="server"></asp:TextBox>
                                                                                              
                                </div>

                                          </div>
                                    <div class="form-row">
                                    <div class="form-group col-md-4">                                    
                                        <label for="inputCity" class="col-form-label">Rekening CBS</label>
                                        <asp:TextBox ID="TxtRekeningCBS" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox> 
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Produk</label>
                                            <asp:TextBox ID="txtproduk" Enabled="false" CssClass="form-control"  runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtjangka" visible="false" CssClass="form-control"  runat="server"></asp:TextBox>
                                    </div>
                                   
                                                          
                                        
                                  <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">AO</label>
                                        <asp:TextBox ID="Txtkodeao" Enabled="false" CssClass="form-control"  runat="server"></asp:TextBox>
                                    </div>
                                   
                                        
                                    
                                    </div>
                                    

                                        
                                    <div class="form-row">
                                    <div class="form-group col-md-4">                                    
                                        <label for="inputCity" class="col-form-label">Pokok</label>
                                        <asp:TextBox ID="txtpokok" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>
                                        <asp:TextBox ID="txtpokokangs" Visible="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Bunga</label>
                                        <asp:TextBox ID="txtbunga" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>   
                                        <asp:TextBox ID="txtbungaangs" Visible="false" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>  
                                    </div>
                                   
                                      <div class="form-group col-md-4" runat="server" visible="false">
                                        <label for="inputCity" class="col-form-label">Denda</label>
                                        <asp:TextBox ID="txtdenda" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>      
                                    </div>
                                   
                                        
                                  <div class="form-group col-md-4">
                                        <label for="inputCity" class="col-form-label">Cadangan</label>
                                        <asp:TextBox ID="txtcadangan" onkeyup="javascript:this.value=Comma(this.value);" Text="0" onblur="checkTextField1(this);" onkeypress="return isnumerickey(event);" type="text" required="required" placeholder="" class="form-control autonumber" runat="server" readonly="true"></asp:TextBox>      
                                    </div>
                                   
                                        
                                    
                                    </div>
                                    
                                    

                                               
                                    

                               



            

                                    

                               

                                 


                                                              
                                </div>                                                    
                            </div>
                           </div>

                           
               </div>      
                                   
                </div>
              </div>
              </asp:Panel>

                 
                 

                 
            </div> 

        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />


                 
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



