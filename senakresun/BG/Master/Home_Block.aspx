<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home_Block.aspx.cs" Inherits="Home_Block" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>eData Kopnus</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta content="A fully featured admin theme which can be used to build CRM, CMS, etc." name="description" />
    <meta content="Coderthemes" name="author" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

            <!-- App favicon -->
        <link rel="shortcut icon" href="../../assets/images/icon.ico"/>

        <!-- App css -->
        <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="../../assets/css/icons.css" rel="stylesheet" type="text/css" />
        <link href="../../assets/css/style.css" rel="stylesheet" type="text/css" />

        <script src="../../assets/js/modernizr.min.js"></script>



    
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
            background: white url("../assets/images/Cube-1s-30px.gif") right center no-repeat;
        }
        .auto-style11 {
            height: 455px;
            width: 1374px;
        }
        </style>


    <script type="text/javascript">



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

       </script>

</head>
<body>


      <div class="limiter">
        <div class="">
            <div class="wrap-login100">
                                
                <form id="sign_in" class="login100-form validate-form" runat="server">

                    <div class="text-center">
                                 <h2 class="login100-form-title p-b-34"> <span><img src="../../assets/images/logox.png" alt="" height="26"/></span></h2>
                    
                                 <svg id="Layer_1" class="svg-computer" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 424.2 424.2">
                                     <style>
                                         .st0 {
                                             fill: none;
                                             stroke: #02c0ce;
                                             stroke-width: 5;
                                             stroke-linecap: round;
                                             stroke-linejoin: round;
                                             stroke-miterlimit: 10;
                                         }
                                     </style>
                                     <g id="Layer_2">
                                         <path class="st0" d="M339.7 289h-323c-2.8 0-5-2.2-5-5V55.5c0-2.8 2.2-5 5-5h323c2.8 0 5 2.2 5 5V284c0 2.7-2.2 5-5 5z"/>
                                         <path class="st0" d="M26.1 64.9h304.6v189.6H26.1zM137.9 288.5l-3.2 33.5h92.6l-4.4-33M56.1 332.6h244.5l24.3 41.1H34.5zM340.7 373.7s-.6-29.8 35.9-30.2c36.5-.4 35.9 30.2 35.9 30.2h-71.8z"/>
                                         <path class="st0" d="M114.2 82.8v153.3h147V82.8zM261.2 91.1h-147"/>
                                         <path class="st0" d="M124.5 105.7h61.8v38.7h-61.8zM196.6 170.2H249v51.7h-52.4zM196.6 105.7H249M196.6 118.6H249M196.6 131.5H249M196.6 144.4H249M124.5 157.3H249M124.5 170.2h62.2M124.5 183.2h62.2M124.5 196.1h62.2M124.5 209h62.2M124.5 221.9h62.2"/>
                                     </g>
                                 </svg>
 
                                 <h4 class="text-danger">Anda tidak mempunyai Hak Akses</h4>
                                 <p class="text-muted">hubungi administrator untuk mendapatkan hak akses.</p>
                             </div>


                </form>
                <div class="login100-more" style="background-image: url('Login/images/bg-01.png');"></div>

            </div>
        </div>
    </div>








        <script src="../../assets/js/jquery.min.js"></script>
        <script src="../../assets/js/popper.min.js"></script>
        <script src="../../assets/js/bootstrap.min.js"></script>
        <script src="../../assets/js/waves.js"></script>
        <script src="../../assets/js/jquery.slimscroll.js"></script>

        <!-- App js -->
        <script src="../../assets/js/jquery.core.js"></script>
        <script src="../../assets/js/jquery.app.js"></script>
</body>
</html>
