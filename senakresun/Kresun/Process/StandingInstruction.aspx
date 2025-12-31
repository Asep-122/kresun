<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" enableEventValidation="false" CodeFile="StandingInstruction.aspx.cs" Inherits="KP_Process_StandingInstruction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Pencairan Kredit</title>
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
     <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css?family=Roboto&display=swap" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

   
    <script src="https://cdn.quilljs.com/1.3.6/quill.js"></script>
    <script type="text/javascript">

        var quill;

        document.addEventListener("DOMContentLoaded", function () {
            quill = new Quill('#editor', { theme: 'snow' });
        });



          function jv_openModal_root() {
            setTimeout(function () { $('#lnkopenGroup').click(); $("div").remove(".tooltip"); window.top.cekResize(); }, 200);
        }
    
        history.pushState(null, null, location.href);
        window.onpopstate = function () {
            history.go(1);
        };

        function IsValid( args ) {
        if( args.value.length == 0 ) {
            return false;
        }
        else {
            return true;
        }
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
        function sweetAlerttolak(btnSimpanTolakan, errMsg) {
            if (btnSimpanTolakan.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnSimpanTolakan.dataset.confirmed = false;
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
                        btnSimpanTolakan.dataset.confirmed = true;
                        btnSimpanTolakan.click();

                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
            }
        }

        function sweetAlertdelete(btndelete, errMsg) {
            if (btndelete.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btndelete.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: errMsg,
                    confirmButtonClass: 'btn btn-confirm',
                    cancelButtonClass: 'btn btn-confirm',
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

          function sweetAlertsave(btnSimpan,errMsg) {
            if (btnSimpan.dataset.confirmed) {
                btnSimpan.dataset.confirmed = false;

                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: errMsg,
                    confirmButtonClass: 'btn btn-confirm',
                    cancelButtonClass: 'btn btn-confirm',
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

        function AlertSuccess(h, t, p, i, fr) {
            console.log(fr)
            setTimeout(function () {


                Swal.fire({
                    title: h,
                    text: t,
                    icon: "success"
                }).then((result) => {
                    if (fr === 'true') {
                        window.location.href = "StandingInstruction.aspx"
                    }
                });;
            }, 100);

        }

        function Alertfailed(h, t, p, i) {
            setTimeout(function () {
                Swal.fire({
                    title: h,
                    text: t,
                    icon: "error"
                });
            }, 200);

        }

        function SweetAlert(errMsg) {

            setTimeout(function () {
                swal(
                    {
                        title: errMsg,
                        confirmButtonClass: 'btn btn-confirm'
                    }
                ).catch(swal.noop)
            }, 100);

        }
  

        function stringToArray(items) {
            return items.replace(/'/g, '"');
        } // "

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

        function Alert(h, t, p, i, fr) {
            setTimeout(function () {
                $.toast({
                    heading: h,
                    text: t,
                    position: p,
                    loaderBg: '#5ba035',
                    icon: i,
                    hideAfter: 500,
                    stack: 1,
                    afterHidden: function () {
                        if (fr == '1') {
                            setTimeout(function () { <%=btnafterDelete()%>; }, 100); //Delete
                        }


                    }
                });
            }, 0);

              }
        function Comma(Num) { //function to add commas to textboxes

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
      
       
        $(document).ready(InIEvent);
        function InIEvent() { 

            $("div").remove(".tooltip");


            $('#LnkAdd').tooltipster({
                animation: 'grow'
            });

              <%--  {
                $('#<%=txtketeranganJaminan.ClientID%>').prop('maxlength', 250);
                
            }--%>

            $("[id$='txtNoRekening']").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "StandingInstruction.aspx/GetNoRekening",
                        data: "{'InputText':'" + document.getElementById('txtNoRekening').value +"'}",
                        dataType: "json",
                        success: function (data) {
                            obj = JSON.parse(stringToArray(data.d));
                       

                            var found = $.map(obj, function (item) {
                                console.log(item)
                                return {
                                    label: item[0],
                                    txtnorekening: item[7],
                                    txtnoktp: item[2], 
                                    txtnama: item[1],
                                    txtnopen: item[3],
                                    txtnopinjam: item[4],
                                    txtnopk: item[5],
                                    txtterimabersih: item[6],
                                    txtbankrekening: item[8],
                                    txtsetoran: item[9],
                                    txtidpermohonan: item[10]

                                    
                                }
                            });

                            //$("[id$=txtno_bukti]").val(''); //hdnZipCode2

                            response(found);
                        },

                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("masuk1");
                        },
                        failure: function (response) {
                            alert("masuk 2");
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=txtNoRekening]").val(i.item.txtnorekening);
                    $("[id$=NoRekeningHiden]").val(i.item.txtnorekening);
                    $("[id$=txtNoRekening2]").val(i.item.txtnorekening);
                    $("[id$=txtNoKtp]").val(i.item.txtnoktp);
                    $("[id$=txtNama]").val(i.item.txtnama);
                    $("[id$=txtNoPensiun]").val(i.item.txtnopen);
                    $("[id$=txtNoPinjaman]").val(i.item.txtnopinjam);
                    $("[id$=txtNoPk]").val(i.item.txtnopk);
                    $("[id$=txtBankRekening]").val(i.item.txtbankrekening);
                    $("[id$=txtNamaBank]").val(i.item.txtnama);
                    $("[id$=txtTerimaBersih]").val(i.item.txtsetoran);
                    var txt = $("[id$=txtTerimaBersih]");
                    txt.val(Comma(txt.val()));
                    $("[id$=txtIdPermohonan]").val(i.item.txtidpermohonan);
                    console.log(i);//hdnZipCode3 hdnZipCode4
                },
                minLength: 3
            });
            $('#txttglptkp').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                todayHighlight: true
            });
              $('#txttgllahirpasangan').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

              $('#txttglrealisasi').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
              });
            $('#txttgltagih').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtglskpensiun').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txtmtpensiun').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahir').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak1').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiranak2').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahirpp').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            $('#txttgllahiraw').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });

            $('#txttgltransfer').datepicker({
                format: 'dd-MM-yyyy',
                autoclose: true,
                todayHighlight: true
            });
            
            $('.selectpicker').selectpicker();


            $(function () {


            });



            $(".table ").css({ "width": "100%" });

            $('form').parsley();


            var Font = Quill.import('formats/font');
            Font.whitelist = ['sans-serif', 'serif', 'monospace', 'roboto', 'arial', 'times-new-roman', 'comic-sans'];
            Quill.register(Font, true);


            var quill = new Quill('#editor', {
                theme: 'snow',
                modules: {
                    toolbar: [
                        [{ 'font': Font.whitelist }, { 'size': [] }],
                        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
                        ['bold', 'italic', 'underline', 'strike'],
                        [{ 'color': [] }, { 'background': [] }],
                        [{ 'script': 'sub' }, { 'script': 'super' }],
                        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                        [{ 'indent': '-1' }, { 'indent': '+1' }],
                        [{ 'direction': 'rtl' }],
                        [{ 'align': [] }],
                        ['link', 'image', 'video', 'formula'],
                        ['clean']  // remove formatting
                    ]
                }
            });


            function submitContent() {
                var content = quill.root.innerHTML;
                alert(content);
            }
        }
 
    </script>
     <style type="text/css">
        .form-control {
        }

        .ql-snow .ql-picker.ql-font .ql-picker-label[data-value="roboto"]::before,
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value="roboto"]::before {
  content: 'Roboto';
  font-family: 'Roboto', sans-serif;
}
.ql-font-roboto {
  font-family: 'Roboto', sans-serif;
}

.ql-snow .ql-picker.ql-font .ql-picker-label[data-value="arial"]::before,
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value="arial"]::before {
  content: 'Arial';
  font-family: Arial, sans-serif;
}
.ql-font-arial {
  font-family: Arial, sans-serif;
}

.ql-snow .ql-picker.ql-font .ql-picker-label[data-value="times-new-roman"]::before,
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value="times-new-roman"]::before {
  content: 'Times New Roman';
  font-family: "Times New Roman", serif;
}
.ql-font-times-new-roman {
  font-family: "Times New Roman", serif;
}

.ql-snow .ql-picker.ql-font .ql-picker-label[data-value="comic-sans"]::before,
.ql-snow .ql-picker.ql-font .ql-picker-item[data-value="comic-sans"]::before {
  content: 'Comic Sans';
  font-family: "Comic Sans MS", cursive, sans-serif;
}
.ql-font-comic-sans {
  font-family: "Comic Sans MS", cursive, sans-serif;
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
                                    <li class="breadcrumb-item"><a href="StandingInstruction.aspx">StandingInstruction</a></li>
                                </ol>
                            </div>
                            <h4 class="page-title"></h4>
                        </div>
                    </div>
                </div>

                 <!-- Panel Header -->
               <asp:Panel ID="pnlHeader" runat="server">
                 <div class="row">
                    <div class="col-12">
                        <div class="card-box table-responsive">
                           <!--  <h4 class="m-t-0 header-title">Header</h4>-->
                            <asp:Button ID="btnAdd" class="btn btn-success waves-light text-white" runat="server" Text="Add Kode Transaksi" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                   </div>
                </asp:Panel>

  <asp:Panel ID="PnlFront" runat="server">
      <div class="row">
              <div class="col-md-12">
                  <div class="card-box">
                      <h4 class="m-t-0 header-title">  <asp:TextBox ID="txtfocus" CssClass="form-control"  readonly="true" text="Standing Instruction" runat="server" ></asp:TextBox> </h4>   
                      
                               <div class="form-row">
                            <div class="form-group" style="width: 100%;">



                                <asp:GridView ID="GRData" runat="server" CssClass="table table-bordered table-striped table-hover js-basic-example dataTable" ShowHeaderWhenEmpty="True"   AutoGenerateColumns="False" OnRowCommand="GRData_RowCommand" >
                              <Columns>
                                    <asp:TemplateField HeaderText="No Bukti">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnobukti" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nobukti")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="Id Permohonan" Visible="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lblidpermohonan" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "idpermohonan")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                 <asp:TemplateField HeaderText="No Rekening">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnorekening" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "norekening")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                 
                                  <asp:TemplateField HeaderText="Nama">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnama" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "NamaNasabah")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                    
                                  <asp:TemplateField HeaderText="No Ktp" Visible ="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnoktp" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "no_identitas")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                  
                                  <asp:TemplateField HeaderText="No Pinjaman" Visible="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnopinjaman" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopinjaman")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                  
                                  <asp:TemplateField HeaderText="No pK" Visible="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnopk" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopk")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="No Pensiun" Visible="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnopensiun" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nopensiun")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                  
                                  <asp:TemplateField HeaderText="Bank" Visible="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lblbankrekening" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BankRekeningtrf")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Terima Bersih">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterimabersih" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "terimabersihrp", "{0:N2}")%>'></asp:Label>
                                     </ItemTemplate>
                                    <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Jenis Instruction">
                                      <ItemTemplate>
                                          <asp:Label ID="lbljenisinstruction" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "jenisinstruction")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Koperasi Asal">
                                      <ItemTemplate>
                                          <asp:Label ID="lblkoperasiasal" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "koperasiasal")%>'></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Nilai">
                                      <ItemTemplate>
                                          <asp:Label ID="lblnilai" Visible="true" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "nilai", "{0:N2}")%>' onkeyup="javascript:this.value=Comma(this.value);"></asp:Label>
                                       </ItemTemplate>
                                      <HeaderStyle Height="30px" CssClass="text-center"></HeaderStyle>
                                      <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  </asp:TemplateField>                                                                       
                                  <asp:TemplateField HeaderText="Action">
                                                  <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                  <ItemTemplate>
                                                      <asp:LinkButton ID="btnedit" runat="server" data-toggle="tooltip" data-placement="left" title="Process Verifikasi" CommandName="SET" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                      <asp:LinkButton ID="BtnPDF" Visible="true" runat="server" CausesValidation="True" UseSubmitBehavior="False" data-toggle="tooltip" data-placement="left" title="PDF" CommandName="PDF" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="mdi mdi-printer-3d"></i></asp:LinkButton>
                                                      <asp:LinkButton ID="btnApproveAntrian" Visible="true" runat="server" data-toggle="tooltip" data-placement="left" title="Approve Antrian" CommandName="Approve" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin Approve dan menentukan data ini ?');" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-export"></i></asp:LinkButton>
                                                      <asp:LinkButton ID="BtnBatalkanAntrian" Visible="false" runat="server" data-toggle="tooltip" data-placement="left" title="Batalkan Antrian" CommandName="Batalkan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="dripicons-thumbs-down"></i></asp:LinkButton>
                                                      <asp:LinkButton ID="btnDelete" runat="server" data-toggle="tooltip" data-placement="left" title="Delete" CommandName="SETDELETE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <i class="fa fa-trash"></i></asp:LinkButton>

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

  <asp:Panel ID="pnlData" Visible="false" runat="server">                                                                     
         <div class="row">

                  <div class="col-lg-12">
                    <div class="col-lg-12">
                       <div class="card-box ribbon-box">
                           <div class="ribbon ribbon-custom">Data</div>
                           <p class="text-muted font-14 m-b-30"></p>   
                           <div class="form-row">
                               <div class="form-group col-md-6">
                                <div class="form-group">
                                        <label for="txtNoRekening" class="col-3 col-form-label">No Rekening</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNoRekening" CssClass="col-11 col-form-label" runat="server" ></asp:TextBox>
                                        <asp:HiddenField ID="NoRekeningHiden" runat="server" />
                                        <asp:HiddenField ID="txtIdPermohonan" runat="server" />
                                        <asp:HiddenField ID="txtNoBukti" runat="server" />
                                    </div>
                                </div>                                
                                   <div class="form-group">
                                        <label for="txtTo" class="col-3 col-form-label">Nama</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNama" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                        <label for="txtNoPinjaman" class="col-3 col-form-label">No Pinjaman</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNoPinjaman" CssClass="col-11 col-form-label"  runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                </div>                               
                               <div class="form-group col-md-6">
                               <div class="form-group">
                                     <label for="txtNoKtp" class="col-3 col-form-label">No Ktp</label>     
                                 <div class="col-8">
                                     <asp:TextBox ID="txtNoKtp" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                 </div>
                               </div>   
                                <div class="form-group">
                                        <label for="txtNoPensiun" class="col-3 col-form-label">No Pensiun</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNoPensiun" CssClass="col-11 col-form-label"  runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>                                
                                   <div class="form-group">
                                        <label for="txtNoPk" class="col-3 col-form-label">No Pk</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNoPk" CssClass="col-11 col-form-label"  runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                             </div>
                            </div>                           
                           <div class="ribbon ribbon-custom">Data Rekening</div>
                           <p class="text-muted font-14 m-b-30"></p>   
                           <div class="form-row">
                               <div class="form-group col-md-6">
                                <div class="form-group">
                                        <label for="txtNoRekening2" class="col-3 col-form-label">No Rekening</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNoRekening2" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>                                
                                   <div class="form-group">
                                        <label for="txtNamaBank" class="col-3 col-form-label">Nama Bank</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNamaBank" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                </div>                               
                               <div class="form-group col-md-6">
                               <div class="form-group">
                                     <label for="txtBankRekening" class="col-3 col-form-label">Bank Rekening</label>     
                                 <div class="col-8">
                                     <asp:TextBox ID="txtBankRekening" CssClass="col-11 col-form-label" runat="server" Enabled="false"></asp:TextBox>
                                 </div>
                               </div>                                 
                                 <div class="form-group">
                                     <label for="txtTerimaBersih" class="col-3 col-form-label">Terima Bersih</label>     
                                 <div class="col-8">
                                     <asp:TextBox ID="txtTerimaBersih" CssClass="col-11 col-form-label" runat="server" Enabled="false" Text="0"  onkeypress="return isnumerickey(event);" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                                 </div>
                               </div>   
                             </div>
                            </div>
                       </div>
                   </div>
                  </div>              
           </div>
   </asp:Panel>
   <asp:Panel ID="pnlUploadDokument" Visible="false" runat="server">
    <div class="row">
      <div class="col-lg-12">
            <div class="col-lg-12">
                <div class="card-box ribbon-box">
                    <div class="ribbon ribbon-custom">Jenis Instruction</div>
                    <p class="text-muted font-14 m-b-30"></p>    
                           <div class="form-row">
                               <div class="form-group col-md-10">
                                    <div class="form-group">
                                           <label for="inputState" class="col-11 col-form-label">Jenis Instruction</label>     
                                       <div class="col-4">
                                           <asp:DropDownList runat="server" ID="ddlJnsInstruction" CssClass="form-control selectpicker" data-style="btn-custom" AutoPostBack="true" OnSelectedIndexChanged="ddlJnsInstruction_SelectedIndexChanged">
                                               <asp:ListItem Text="Pilih Instruction" Value="-"></asp:ListItem>
                                               <asp:ListItem Text="Dantal" Value="Dantal"></asp:ListItem>
                                               <asp:ListItem Text="Panjar" Value="Panjar"></asp:ListItem>
                                           </asp:DropDownList>
                                       </div>
                                   </div>
                                <div class="form-group" runat="server" id="divKoperasi" visible="false"> 
                                        <label for="txtKoperasi" class="col-3 col-form-label">Koperasi</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtKoperasi" CssClass="col-6 col-form-label" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                        <label for="txtNilai" class="col-3 col-form-label">Nilai</label>     
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNilai" CssClass="col-6 col-form-label" runat="server" Text="0" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return isnumerickey(event);"></asp:TextBox>
                                    </div>
                                </div>

                                </div>
                            </div>
                        <div class="form-group">
                            <asp:Button ID="btnSimpan" class="btn btn-success waves-light text-white right" runat="server" Text="Process" OnClientClick="return sweetAlertsave(this,'Apakah anda yakin ingin menyimpan data ini ?');" OnClick="btnSimpan_Click" />
                            <asp:Button ID="btnDelete" class="btn btn-success waves-light text-white right" runat="server" Text="Delete" OnClientClick="return sweetAlertdelete(this,'Apakah anda yakin ingin hapus semua data ini ?');" OnClick="btnDelete_Click" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-success waves-light text-white" runat="server" Text="Back" OnClick="btnCancel_Click"  Width="65px" />
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
