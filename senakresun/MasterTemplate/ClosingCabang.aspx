<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClosingCabang.aspx.cs" Inherits="CF_ClosingCabang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Tutup Buku Bulan</title>
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

                        if (fr == '2') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 50); //Delete
                        }
                    }
                });
            }, 0);

        }

        function redirect() {
            location.href = 'Closing.aspx ';
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type : 'success',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

        }

        function simpan(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'success',
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);
            $('#txtnobukti').value("");
        }
        function WarningAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 3000
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


        function sweetAlertsave(btnSimpan, errMsg) {
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
            $.getScript("/assets/js/jquery.core.js");

            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });
            $('.selectpicker').selectpicker();

            
            
        }
        

        $('form').parsley();

    </script>
    <script type="text/javascript">
        function kepo(evt) {
            var inp = String.fromCharCode(evt.keyCode);
            if (/[a-zA-Z0-9]/.test(inp))
                return true;
            alert("Hanya Bisa Angka Dan Huruf");
            return false;
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
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                <!-- delete  -->
                <%--<div class="container-fluid">--%>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="btn-group pull-right">
                                    <ol class="breadcrumb hide-phone p-0 m-0">
                                        <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                        <li class="breadcrumb-item"><a href="#">Configuration</a></li>
                                        <li class="breadcrumb-item"><a href="Closing.aspx">Master Clossing Cabang</a></li>
                                    </ol>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Panel Edit -->
                    <asp:Panel ID="PanelSystem" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 center-page">
                                
                        <div class="card-box">
                            <h4 class="text-dark header-title m-t-0">Process Pengecekan Data</h4>
                        
                            <ul class="sortable-list taskList list-unstyled" id="inprogress">
                                <li id="task9">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                        <asp:CheckBox ID="chkkasteller" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Saldo Akhir Pada Kas Teller 
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="BtnKasTeller" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="LblKasTeller" runat="server" Text="Saldo AKhir Kas Teller "></asp:Label> </p>
                                    </div>
                                </li>
                                <li class="task-info" id="task10">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chkpetugasbahan" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Petugas Bahan
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="btnpetugas" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="lblpetugas" runat="server" Text="Check Petugas Bahan"></asp:Label> </p>
                                       
                                    </div>
                                </li>
                                      <li class="task-info" id="task11">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="ChkPiutanganggota" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Piutang Anggota dikurang piutang dibayarkan sama dengan saldo akhir di tunggakan
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="BtnPiutang" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="Lblpiutanganggota" runat="server" Text="Check Piutang Anggota"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblPiutangdibayarkan" runat="server" Text="Check Piutang Dibayarkan "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblPiutangpengurangan" runat="server" Text="Check Pengurangan Piutang Anggota dengan dibayarkan "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsaldoakhirtgk" runat="server" Text="Check Saldo Akhir Tunggakan "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblselisih" runat="server" Text="Selisih "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsolusi" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>
                                <li class="task-info" id="task12">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chksimpok"  Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Total Anggota List sama dengan saldo akhir Simpanan Pokok di neraca
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                           <asp:Button ID="BtnSimpok" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="Lblanggota" runat="server" Text="Check Total Anggota"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblanggotasimpok" runat="server" Text="Check Total Simpanan Pokok Anggota"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="Lblanggotaneraca" runat="server" Text="Check Total Anggota Simpanan Pokok Neraca "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblanggotasimpokneraca" runat="server" Text="Check Saldo Akhir Simpanan Pokok Neraca"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblselisihsimpok" runat="server" Text="Selisih "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsolusisimpok" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>
                                <li class="task-info" id="task13">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chksimwa" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Total Simpanan Wajib Anggota List sama dengan saldo akhir Simpanan Wajib di neraca
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="btnsimwa" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="lblanggotasimwa" runat="server" Text="Check Total Simpanan Wajib Anggota"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblanggotasimwaneraca" runat="server" Text="Check Saldo Akhir Simpanan Wajib "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblselisihsimwa" runat="server" Text="Selisih "></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsolusisimwa" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>
                             
                                    <li class="task-info" id="task14">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chkmodal" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Saldo Akhir Hutang Bunga Modal Penyertaan
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="btnmodal" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="lblbungamodal" runat="server" Text="Check Total Saldo Akhir"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="LblSolusimodal" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>
                                   <li class="task-info" id="task15">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chksimpananjakpen" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Saldo Akhir Hutang Bunga Simpanan Berjangka Jangka Pendek
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="btnsimpananjakpen" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="lblsimpananjakpen" runat="server" Text="Check Total Saldo Akhir"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsolusisimpananjakpen" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>
                                      <li class="task-info" id="task16">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chksimpananjakpanjang" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Saldo Akhir Hutang Bunga Simpanan Berjangka Jangka Panjang
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="btnsimpananjakpanjang" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="lblsimpananjakpanjang" runat="server" Text="Check Total Saldo Akhir"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsolusisimpananjakpanjang" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>

                                <li class="task-info" id="task17">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chkSimpananSukarela" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Saldo Akhir Hutang Bunga Simpanan Sukarela
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="btnSimpananSukarela" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="lblSimpananSukarela" runat="server" Text="Check Total Saldo Akhir"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsolusiSimpananSukarela" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>
                                  <li class="task-info" id="task18">
                                    <div class="checkbox checkbox-custom checkbox-single pull-right">
                                         <asp:CheckBox ID="chkbungabank" Checked="false" runat="server" />
                                        <label></label>
                                    </div>
                                    Pengecekan Saldo Akhir Hutang Bunga Bank Jangka Pendek
                                    <div class="clearfix"></div>
                                    <div class="mt-3">
                                        <p class="pull-right mb-0 mt-2">
                                            <asp:Button ID="btnbungabank" Enabled="false" runat="server" Text="Progress" class="btn btn-success btn-sm waves-effect waves-light"/>
                                        </p>
                                        <p class="mb-0"><asp:Label ID="lblbungaBank" runat="server" Text="Check Total Saldo Akhir"></asp:Label> </p>
                                        <p class="mb-0"><asp:Label ID="lblsolusibungaBank" runat="server" Text="Solusi "></asp:Label> </p>
                                    </div>
                                </li>

                            </ul>
                            <button id="btnnextclosing" runat="server" visible="false"  type="button" class="btn btn-custom btn-block mt-3 waves-effect waves-light" onserverclick="btntambah_click">Lanjutkan Proses Closing Bulan ini</button>
                            <button id="btninfo" runat="server"  type="button" class="btn btn-custom btn-block mt-3 waves-effect waves-light">Perbaiki data terlebih dahulu baru bisa melakukan proses closing bulan ini</button>   
                           
                                
                        </div>
                  



                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnladd" Visible="false" runat="server">
                        <div class="row clearfix">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 center-page">
                                <div class="card-box ribbon-box">
                                    <div class="ribbon ribbon-custom">Data</div>
                                    <br />
                                    <div class="title text-center mb-3">
                                        <h3>Tutup Buku Bulan</h3>
                                    </div>
                                    <br />
                                    <div class="form-row">
                                        <div class="form-group  col-md-12">
                                            <div class="form-group  ">
                                                <div class="form-check-inline col-md-12">
                                                    <asp:Label ID="Label4" runat="server" class="col-md-4 col-form-label font-weight-bold">Cabang</asp:Label>
                                                    <div class="col-6">
                                                        <asp:DropDownList ID="ddlcabang" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged="ddlcabang_SelectedChange"></asp:DropDownList>  
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group  ">
                                                <div class="form-check-inline col-md-12">
                                                    <asp:Label ID="Label2" runat="server" class="col-md-4 col-form-label font-weight-bold">Bulan</asp:Label>
                                                    <div class="col-4">
                                                        <asp:DropDownList ID="ddlbulan" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged ="ddlbulan_SelectedChange">
                                                            <asp:ListItem Value="1">Januari</asp:ListItem>
                                                            <asp:ListItem Value="2">Februari</asp:ListItem>
                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                            <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group  ">
                                                <div class="form-check-inline col-md-12">
                                                    <asp:Label ID="Label3" runat="server" class="col-md-4 col-form-label font-weight-bold">Tahun</asp:Label>
                                                    <div class="col-4">
                                                        <asp:DropDownList ID="ddltahun" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" OnSelectedIndexChanged ="ddltahun_SelectedIndexChanged"></asp:DropDownList>  
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label6" runat="server" class="col-md-6 col-form-label font-weight-bold">Tgl Awal Fiskal Bulan Ini</asp:Label>
                                                        <div class="col-6">
                                                            <asp:TextBox ID="txttglAwalini" CssClass="form-control" runat="server" data-style="btn-custom" Type="Text" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label1" runat="server" class="col-md-6 col-form-label font-weight-bold">Tgl Akhir Fiskal Bulan Ini</asp:Label>
                                                        <div class="col-6">
                                                            <asp:TextBox ID="txttglAkhirini" CssClass="form-control" runat="server" data-style="btn-custom" Type="Text" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label5" runat="server" class="col-md-6 col-form-label font-weight-bold">Tgl Awal Fiskal Bulan Depan</asp:Label>
                                                        <div class="col-6">
                                                            <asp:TextBox ID="txttglAwaldepan" CssClass="form-control" runat="server" data-style="btn-custom" Type="Text" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label7" runat="server" class="col-md-6 col-form-label font-weight-bold">Tgl Akhir Fiskal Bulan Depan</asp:Label>
                                                        <div class="col-6">
                                                            <asp:TextBox ID="txttglAkhirdepan" CssClass="form-control" runat="server" data-style="btn-custom" Type="Text" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label8" runat="server" class="col-md-6 col-form-label font-weight-bold">Tgl Awal Fiskal Bulan lalu</asp:Label>
                                                        <div class="col-6">
                                                            <asp:TextBox ID="txttglAwallalu" CssClass="form-control" runat="server" data-style="btn-custom" Type="Text" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <div class="form-check-inline col-md-12">
                                                        <asp:Label ID="Label9" runat="server" class="col-md-6 col-form-label font-weight-bold">Tgl Akhir Fiskal Bulan lalu</asp:Label>
                                                        <div class="col-6">
                                                            <asp:TextBox ID="txttglAkhirlalu" CssClass="form-control" runat="server" data-style="btn-custom" Type="Text" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row justify-content-center">
                                            <div class="btn-group mb-2">
                                                <div class="button-list">
                                                    <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="CLOSING BULAN INI" OnClick="btnSimpan_Click" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Closing Bulan ini ?');" />
                                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                                    <asp:Button ID="btnCancel" Visible="false" class="btn btn-success waves-light text-white" runat="server" Text="BACK TO BULAN LALU" OnClick="btnSimpan2_Click" OnClientClick="btnSimpan2_Click();"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </asp:Panel>
                    <!-- end wrapper -->
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                 <asp:PostBackTrigger ControlID="btnnextclosing" />

                
                
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
  
    
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