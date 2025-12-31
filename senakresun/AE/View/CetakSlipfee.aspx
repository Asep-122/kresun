<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CetakSlipfee.aspx.cs" Inherits="AE_Slip_CetakSlipfee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cetak Slip Fee dan Reward</title>
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
            location.href = 'CetakSlipfee.aspx ';
        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        showConfirmButton: false,
                        timer: 3000
                    }
                ).catch(swal.noop)
            }, 100);

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
        function KosongAlert(errMsg, namaid) {
            //var e = document.getElementById("ddlSP").text;
            ////var bulan = e.text;
            ////var rubah = ddlViewBy.selectedOptions[bulan].text
            //console.log(e);
            //console.log(bulan);
            //var f = document.getElementById("ddltahun");
            //var tahun = f.items;
            var selObj = document.getElementById("ddlSP");
            var bulan = selObj.options[selObj.selectedIndex].text;
            console.log(bulan);
            var selObj1 = document.getElementById("ddltahun");
            var tahun = selObj1.options[selObj1.selectedIndex].text;
            console.log(tahun);

            setTimeout(function () {
                swal(
                    {
                        title: errMsg + ' ' + bulan + ' ' + tahun ,
                        text: namaid + ' ' +  'Belum ada.',
                        type: 'warning',
                        showConfirmButton: false,
                        timer: 3500
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
            if (/[a-zA-Z0-9 ]/.test(inp))
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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>   <!-- delete  -->
             <div class="container-fluid">
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="btn-group pull-right">
                                <ol class="breadcrumb hide-phone p-0 m-0">
                                    <li class="breadcrumb-item"><a href="#">Smart</a></li>
                                    <li class="breadcrumb-item"><a href="#">Account Employee</a></li>
                                    <li class="breadcrumb-item"><a href="CetakSlipGaji.aspx">Cetak Slip Fee Reward</a></li>
                                </ol>
                            </div>
                            <%--<h4 class="page-title">Master Kode Currency</h4>--%>
                        </div>
                    </div>
                </div>
                 <!-- Panel Edit -->
            <asp:Panel ID="pnladd" runat="server">
              <div class="row clearfix">
                  <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7 center-page"> 
                     <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Data</div>
                         <br />
                               <div class="title text-center mb-3">
                                    <h3>Cetak Slip Fee Reward</h3>
                               </div>
                         <br />
                       <div class="form-row">
                         <div class="form-group  col-md-12">
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                    <asp:label ID="Label1" runat="server" class="col-md-3 col-form-label font-weight-bold">Cabang</asp:label>
                                   <div class="col-6">
					                <asp:DropDownList CssClass="form-control" ID="ddlcabang" runat="server" required="required" onkeypress="return kepo(event)" MaxLength="30"></asp:DropDownList>
                                   </div>
                                   <%--<asp:HiddenField ID="HDNamakry" runat="server" />--%>
                                   <%--<div class="col-1">
					                 <asp:label ID="Label2" runat="server" class="col-md-1 font-weight-bold">s/d</asp:label>
                                   </div>
                                   <div class="col-4">
					                <asp:DropDownList CssClass="form-control" ID="ddlnikakhir" runat="server" required="required" onkeypress="return kepo(event)" MaxLength="30"></asp:DropDownList>
                                   </div>--%>
                                </div>
                            </div>
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                 <label  runat="server" class="col-md-3 col-form-label font-weight-bold">Periode</label>
                                   <div class="col-6">
                                    <asp:DropDownList ID="ddlSP" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" >
                                            <%--<asp:ListItem Value="1">Januari</asp:ListItem>
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
                                            <asp:ListItem Value="12">December</asp:ListItem>--%>
                                    </asp:DropDownList>  
                                       </div>
                                </div>
                            </div>
                           <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                 <label  runat="server" class="col-md-3 col-form-label font-weight-bold">Tahun</label>
                                   <div class="col-6">
                                    <asp:DropDownList ID="ddltahun" runat="server" AutoPostBack="true" CssClass="form-control" data-style="btn-custom" ></asp:DropDownList>  
                                  </div>
                                </div>
                            </div>
                         
                         <div class="form-group  ">
                               <div class="form-check-inline col-md-12">
                                 <label  runat="server" class="col-md-3 col-form-label font-weight-bold">Group Karyawan</label>
                                   <div class="col-6">
                                    <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control" data-style="btn-custom" >
                                                                              
                                    </asp:DropDownList>  
                                       </div>
                                </div>
                            </div>    
                         </div>
                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 row justify-content-center">
                        <div class="btn-group mb-2">
                            <div class="button-list">
                                <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white" runat="server" Text="Proses" OnClick="btnSimpan_Click"  OnClientClick="btnSimpan_Click();" />
                                &nbsp&nbsp&nbsp&nbsp&nbsp
                                <asp:Button ID="btnCancel" class="btn btn-success waves-light text-white" runat="server" Text="Back" onclientclick='redirect()' /> 
                            </div>
                        </div>
                  </div>
                           </div>
                        </div>

                      
                         
					   </div>
                       </div>
               
            </asp:Panel>  
                 <asp:Panel ID="PNLMKT" Visible="false" runat="server">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="card-box"> 
                                     <h4 class="Page-title">Data Karyawan Cabang</h4>
                                     

                                     <div class="form-row">
                                         <div class="form-group" style="width: 100%;">
                                            <asp:GridView ID="GRData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True" OnRowDataBound="GRData_RowDataBound" OnRowCommand="GRData_RowCommand" ShowFooter="True">
                                                <Columns>
                                                   
                                                     <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nomor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                     <asp:TemplateField HeaderText="nobukti" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnobukti" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NoBukti")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="NIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnoanggota" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KodeNik")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Nama Karyawan">
                                                        <HeaderStyle CssClass="align-Left" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnama" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaKry")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltxttotal" runat="server" Text="TOTAL"/>
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Kode Bagian" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodebagian" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "bagian")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Bagian" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketbagian" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ketbagian")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kode Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblkodejab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Jabatan")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Jabatan" Visible="false">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblketjab" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KetJab")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="AVERAGE & KPI">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="txtaverage" Enabled="false"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalaverage", "{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Fee Marketing %">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpsnfee" Enabled="false"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "feemrk", "{0:N2}")%>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="Total BM">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBM" Enabled="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                   <asp:TemplateField HeaderText="Reward P&L">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtpl" Enabled="false"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "plreward", "{0:N0}")%>' ></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalPL" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="NEW MEMBERS REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblNEWMEMBERSReward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "newmembers","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalNEWMEMBERSReward" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                     <asp:TemplateField HeaderText="Reward K M K">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtKMK" Enabled="false"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "KMK", "{0:N0}")%>' ></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalKMK" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                   <asp:TemplateField HeaderText="Tunggakan Pokok (TP)">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtTP" Enabled="false"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TP", "{0:N0}")%>' ></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                        <asp:Label ID="lbltotalTunggakan" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="SM/KU TERBAIK">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="txtSMTErbaik" Enabled="false"   runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTERBAIK", "{0:N0}")%>' ></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                        <asp:Label ID="lbltotalSMTERBAIK" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                 
                                                     <asp:TemplateField HeaderText="TOTAL REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblReward"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalreward","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                           <FooterTemplate>
                                                        <asp:Label ID="lbltotalReward" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                      <asp:TemplateField HeaderText="TOTAL BM + REWARD">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblBMReward" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "totalrewardbm","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalRewardBM" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                    <asp:TemplateField HeaderText="New Members Harian">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblNEWMEMBERSharian" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "newmembersharian","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalNEWMEMBERSharian" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                              <asp:TemplateField HeaderText="Pengurangan Fee">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblpenguranganfee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "penguranganfee","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalpenguranganfee" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 

                                                              <asp:TemplateField HeaderText="Penambahan Fee">
                                                        <HeaderStyle CssClass="align-center" ForeColor="Black" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblpenambahanfee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "penambahanfee","{0:N0}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                            <FooterTemplate>
                                                        <asp:Label ID="lbltotalpenambahanfee" runat="server"  />
                                                        </FooterTemplate>
                                                    </asp:TemplateField> 
                                                    
                                                    <asp:TemplateField HeaderText="Action">
                                                        <HeaderStyle CssClass="align-center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                           
                                                            <asp:LinkButton ID="BtnSlip" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="Slip" CommandName="SLIP" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                           
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
        <!-- end wrapper -->
        </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnSimpan" />
                <asp:PostBackTrigger ControlID="GRData" />
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