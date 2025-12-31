<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
 
<%@ Register Assembly="System.Web.DataVisualization,Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- App css -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />

    <script src="Javascript/jquery-1.12.0.min.js"></script>

    <!-- Bootstrap Core Js -->
    <script src="plugins/bootstrap/js/bootstrap.js"></script>

    <%--<script type="text/javascript" src="js/Chart/Chart.bundle.js"></script>--%>
    <%--<script type="text/javascript" src="js/Chart/utils.js"></script>--%>

    <%--<script type="text/javascript" src="ChartDashboard_mikro.js"></script>--%>

    <script src="assets/js/modernizr.min.js"></script>
    <script>
        function customResize() {
            //your code
            //alert("test222");
            //$("div").remove(".tooltip");
            window.top.cekResize();
        }
        function jv_openModal_root() {

            window.top.cekResize();
        }
        function onLoad() {
            window.onresize = customResize;
        }
    </script>
     <script>
         $(document).ready(function () {
             $("#myModal").modal('show');
         });
     </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="myModal" runat="server" visible="true" class="modal fade show"  style="padding-right: 100px; display: block;">
	    <div class="modal-dialog">
          <div class="card-box bg-purple" >
		        <div class="modal-content align-items-center shadow">
		            <img src="assets/images/lebaran.jpg" class="img-fluid" alt="">
           
                    <h4 class="text-uppercase m-b-5 font-13 font-600">Selamat datang, Ramadan! Selamat menunaikan ibadah puasa!</h4>
                    <div class="form-row"> 
                         <p class="mb-0 text-muted">Data Karyawan Yang berulang tahun di Bulan ini</p>
                                         
                    </div> 
                
                        <asp:Button ID="Button5"  class="btn btn-primary m-t-10 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="btnkaryawan_onclick" />
              
                    <div class="form-row"> 
                         <p class="mb-0 text-muted">Data Asset Lelang Cabang</p>
                                         
                    </div> 
                
                        <asp:Button ID="Button6"  class="btn btn-primary m-t-10 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="btnlelang_onclick" />
    
                </div>

         </div>    
	  </div>
   </div>
       
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="container-fluid">
                    <%--App Pusat--%>
                    <asp:Panel ID="Pnlapproval" runat="server" Visible="false">
                        <asp:Button ID="Button7" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Reload" OnClick="Btnupdatesaldo_onclick" />
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box">
                                    <h4 class="header-title">Manage Penutupan Anggota</h4>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="row">
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box widget-flat border-custom bg-custom text-white">
                                                    <i class="fi-tag"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="lblticketAnggotatutup" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Total tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box bg-primary widget-flat border-primary text-white">
                                                    <i class="fi-archive"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="Lblticketanggotaopen" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Pending Tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box widget-flat border-success bg-success text-white">
                                                    <i class="fi-help"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LblticketAnggotaApprove" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Closed Tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box bg-danger widget-flat border-danger text-white">
                                                    <i class="fi-delete"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LblticketAnggotaTolak" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Ajukan Ulang Tickets</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/berjangka.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">Pinjaman</h4>
                                        </div>
                                        <%--<asp:Button ID="Button3" Enabled="false" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="btnberjangka_onclick" />--%>
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblpinjamanout" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Outstanding</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblpinjamanulang" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Ajukan Ulang</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblpinjamanreal" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Realiasi</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/sukarela.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">BDM/Inventaris/Kendaraan</h4>
                                        </div>
                                        <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Check Now</button>--%>
                                        <%--<asp:Button ID="Button4" Enabled="false" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="BtnSukarela_onclick" />--%>
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblbdmout" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Outstanding</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblbdmulang" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Ajukan Ulang</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblbdmreal" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Realiasi</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/modal.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">Biaya Approval</h4>
                                        </div>
                                        <%--<asp:Button ID="Button5" Enabled="false" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="BtnModal_onclick" />--%>
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblbiayaout" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Outstanding</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblbiayaulang" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Ajukan Ulang</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblbiayareal" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Realiasi</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/hutangbank.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">Pelunasan Discount</h4>
                                        </div>
                                        <%--<asp:Button ID="Button6" Enabled="false" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="BtnHutangBank_onclick" />--%>
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblpelunasanout" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Outstanding</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblpelunasanulang" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Ajukan Ulang</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lblpelunasanreal" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Realiasi</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end row -->

                        <div class="row">
                            <div class="col-12">
                                <div class="card-box">
                                    <h4 class="header-title">Manage Aktivasi WO</h4>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="row">
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box widget-flat border-custom bg-custom text-white">
                                                    <i class="fi-tag"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LblWoTicket" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Total tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box bg-primary widget-flat border-primary text-white">
                                                    <i class="fi-archive"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LBLWOOpen" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Pending Tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box widget-flat border-success bg-success text-white">
                                                    <i class="fi-help"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LBLWOApprove" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Closed Tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box bg-danger widget-flat border-danger text-white">
                                                    <i class="fi-delete"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LblWOTolak" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Ajukan Ulang Tickets</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->
                        <div class="row">
                            <div class="col-12">
                                <div class="card-box">
                                    <h4 class="header-title">Manage Aktivasi KTP</h4>
                                    <div class="text-center mt-4 mb-4">
                                        <div class="row">
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box widget-flat border-custom bg-custom text-white">
                                                    <i class="fi-tag"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LblKTPTICKET" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Total tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box bg-primary widget-flat border-primary text-white">
                                                    <i class="fi-archive"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LBLKTPTICKETOPEN" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Pending Tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box widget-flat border-success bg-success text-white">
                                                    <i class="fi-help"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LBLKTPAPPROVE" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Closed Tickets</p>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 col-sm-3">
                                                <div class="card-box bg-danger widget-flat border-danger text-white">
                                                    <i class="fi-delete"></i>
                                                    <h3 class="m-b-10">
                                                        <asp:Label ID="LBLKTPTOLAK" runat="server" Text="0"></asp:Label></h3>
                                                    <p class="text-uppercase m-b-5 font-13 font-600">Ajukan Ulang Tickets</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                        </div>
                        <!-- end row -->
                    </asp:Panel>
                    
                    
                    <%--End App Pusat--%>
                    <%--Cabang--%>

                     <asp:Panel ID="PnlHeader1" runat="server">

                   
                               
                          
                <!-- end row -->

                        
                     </asp:Panel>

                              

                    <asp:Panel ID="pnlbank" runat="server" Visible="false">
                        
                  <div class="row">
                    <div class="col-12">
                        <div class="float-right mb-2"> 
                                                   
                                                    <div class="button-list"> 
                                                       <asp:Button ID="btnpassword" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light"  runat="server" Text=""/>
                                                        <asp:Button ID="BtnAdjusment" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light"  runat="server" Text="Adjustment Harian Pusat 10.000.000"/>
                                                   
                                                  <asp:Button ID="Button3" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server"  Text=""/>                       
                                                    </div>
                                                    </div>
                        </div>
                        <div class="col-12">
                             <div class="float-left mb-2 "> 
                                <div class="button-list"> 
                                <asp:Button ID="Button2" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Reload" OnClick="BtnupdateKas_onclick" />          
                                <asp:Button ID="BtnKasTl" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" BackColor="SteelBlue" Text="Kas TL Hari ini +/- "/>
                                 <asp:Button ID="BtnKasHT" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" BackColor="SteelBlue" Text="Kas HT Hari ini +/- "/>
                                 <asp:Button ID="BtnBank" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" BackColor="SteelBlue" Text="Bank Hari ini +/- "/>
                                <asp:Button ID="BtnCadangan" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" BackColor="SteelBlue" Text="Cadangan hari ini +/- "/>
                           
                        </div>
                             </div>
                            </div>
                       </div>
                
                     
                        <div class="text-center mt-4 mb-4">
                            <div class="row">
                                
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box widget-flat border-custom bg-custom text-white">
                                        <i class="fi-tag"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="Lblcashteller" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Cash Teller</p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-primary widget-flat border-primary text-white">
                                        <i class="fi-archive"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="lblheadteller" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Cash Head Teller</p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-warning widget-flat border-success bg-success text-white">
                                        <i class="fi-help"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="lblbank" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Bank Mandiri Cabang</p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-danger widget-flat border-danger text-white">
                                        <i class="fi-delete"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="lblcadangan" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Cadangan</p>
                                    </div>
                                </div>
                            </div>
                        </div>

       

                                        
                    
                    </asp:Panel>
                    <asp:Panel ID="PnlNotifikasi" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/berjangka.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">Simpanan Berjangka</h4>
                                        </div>
                                        <asp:Button ID="btnberjangka" Enabled="true" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="btnberjangka_onclick" />
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalbungaberjangka" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Bunga</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalpajakberjangka" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Pajak</p>
                                                    </div>
                                                </div>
                                                <div class="col-4">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalpencairanberjangka" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Pencairan</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/sukarela.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">Simpanan Sukarela</h4>
                                        </div>
                                        <%--<button type="button" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light">Check Now</button>--%>
                                        <asp:Button ID="BtnSukarela" Enabled="true" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="BtnSukarela_onclick" />
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-6">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalbungasukarela" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Bunga</p>
                                                    </div>
                                                </div>
                                                <div class="col-6">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalpajaksukarela" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Pajak</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/modal.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">Simpanan Modal</h4>
                                        </div>
                                        <asp:Button ID="BtnModal" Enabled="true" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="BtnModal_onclick" />
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-6">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalbungaModal" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Bunga</p>
                                                    </div>
                                                </div>
                                                <div class="col-6">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalpajakModal" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Pajak</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end col -->
                            <div class="col-lg-3">
                                <div class="text-center card-box">
                                    <div class="member-card pt-2 pb-2">
                                        <div class="thumb-lg member-thumb m-b-10 mx-auto">
                                            <img src="assets/images/users/hutangbank.jpg" class="rounded-circle img-thumbnail" alt="profile-image">
                                        </div>
                                        <div class="">
                                            <h4 class="m-b-5">Hutang Bank</h4>
                                        </div>
                                        <asp:Button ID="BtnHutangBank" Enabled="false" class="btn btn-primary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Check Now" OnClick="BtnHutangBank_onclick" />
                                        <div class="mt-4">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="mt-3">
                                                        <h4 class="m-b-5">
                                                            <asp:Label ID="lbltotalbungahutangbank" runat="server" Text="0"></asp:Label>
                                                        </h4>
                                                        <p class="mb-0 text-muted">Bunga</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        
                        </div>
                        <!-- end row -->
                    </asp:Panel>
                    <asp:Panel ID="pnlData" runat="server" Visible="false">
                        <asp:Button ID="Button1" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Calculate Tunggakan" OnClick="Tunggakan" />
                                 <div class="row">
                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-layers float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Tunggakan Pokok Hari ini</h6>
                            <h2 class="m-b-20">Rp. <span data-plugin="counterup"><asp:Label ID="lbltunggakanpokok" runat="server" Text="0"></asp:Label></span></h2>
                            
                        </div>
                    </div>

                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-paypal float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Tunggakan Bunga Hari ini</h6>
                            <h2 class="m-b-20">Rp. <span data-plugin="counterup"><asp:Label ID="Lbltunggakanbunga" runat="server" Text="0"></asp:Label></span></h2>                         
                        </div>
                    </div>

                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-chart float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Total Tunggakan Hari ini</h6>
                            <h2 class="m-b-20">Rp. <span data-plugin="counterup"><asp:Label ID="lbltotaltunggakan" runat="server" Text="0"></asp:Label></span></h2>
                     
                        </div>
                    </div>
                                   
                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-rocket float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Realiasi AB (Anggota Baru)</h6>
                           <h2 class="m-b-20" data-plugin="counterup"><asp:Label ID="LblrealiasasiAB" runat="server" Text="0"></asp:Label></h2>

                        </div>
                    </div>
                </div>
                <!-- end row -->
                          <asp:Button ID="Button4" class="btn btn-secondary m-t-20 btn-rounded btn-bordered waves-effect w-md waves-light" runat="server" Text="Calculate Terbayar & Realisasi" OnClick="Terbayar" />
                        <div class="row">
                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-layers float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Terbayar Pokok Hari ini</h6>
                            <h2 class="m-b-20">Rp. <span data-plugin="counterup"><asp:Label ID="Lblterbayarpokok" runat="server" Text="0"></asp:Label></span></h2>
                            
                        </div>
                    </div>

                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-paypal float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Terbayar Bunga Hari ini</h6>
                            <h2 class="m-b-20">Rp. <span data-plugin="counterup"><asp:Label ID="LblterbayarBunga" runat="server" Text="0"></asp:Label></span></h2>                         
                        </div>
                    </div>

                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-chart float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Total Terbayar Hari ini</h6>
                            <h2 class="m-b-20">Rp. <span data-plugin="counterup"><asp:Label ID="lbltotalterbayar" runat="server" Text="0"></asp:Label></span></h2>
                     
                        </div>
                    </div>

                    <div class="col-xs-12 col-md-6 col-lg-6 col-xl-3">
                        <div class="card-box tilebox-one">
                            <i class="icon-rocket float-right text-muted"></i>
                            <h6 class="text-muted text-uppercase mt-0">Realiasi AL (Anggota Lama)</h6>
                              <h2 class="m-b-20" data-plugin="counterup"><asp:Label ID="lblrealiasiAL" runat="server" Text="0"></asp:Label></h2>
                          

                        </div>
                    </div>
                </div>



                        
                        <div class="text-center mt-4 mb-4">
                            <div class="row">
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-primary widget-flat border-primary text-white">
                                        <i class="fi-tag"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="Lblanggota" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Anggota Pinjaman Baru Bulan ini</p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-primary widget-flat border-primary text-white">
                                        <i class="fi-archive"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="LblPermohonan" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Permohonan Pinjaman Bulan ini</p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-primary widget-flat border-primary text-white">
                                        <i class="fi-help"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="LblPembatalan" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Pembatalan Pinjaman Bulan ini</p>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-3">
                                    <div class="card-box bg-primary widget-flat border-primary text-white">
                                        <i class="fi-delete"></i>
                                        <h3 class="m-b-10">
                                            <asp:Label ID="lblpencairan" runat="server" Text="0"></asp:Label></h3>
                                        <p class="text-uppercase m-b-5 font-13 font-600">Pencairan Pinjaman Bulan ini</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <%--End Cabang--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>


</body>
</html>
