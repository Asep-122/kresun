<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <iframe id="FormFrame" name="FormFrame" frameborder="0" runat="server" style="width: 100%" allowtransparency="true" scrolling="no"></iframe>

    <script type="text/javascript">

        //document.getElementById('MainContent_FormFrame').onload = function () { // Adjust the Id accordingly
        //    resize();
        //}

        function getDocHeight(doc) {
            doc = doc || document;
            // stackoverflow.com/questions/1145850/
            var body = doc.body, html = doc.documentElement;
            var height = Math.max(body.scrollHeight, body.offsetHeight,
                html.clientHeight, html.scrollHeight, html.offsetHeight);

            return height;
        }

        function setIframeHeight(id) {

            var ifrm = document.getElementById(id);
            var doc = ifrm.contentDocument ? ifrm.contentDocument :
                ifrm.contentWindow.document;


            ifrm.style.visibility = 'hidden';
//            ifrm.style.height = "3000px"; // reset to minimal height ...
            // IE opt. for bing/msn needs a bit added or scrollbar appears

            ifrm.style.height = getDocHeight(doc) + 500 + "px";

            ifrm.style.visibility = 'visible';

        }

        document.getElementById('MainContent_FormFrame').onload = function () { // Adjust the Id accordingly
            setIframeHeight(this.id);
        }

        function cekResize() {
            setTimeout(function (e) {
                //alert(jQuery(window.frames[0].document).height());
                setIframeHeight('MainContent_FormFrame');
            }, 50);

        }


        window.onresize = function () {
            setIframeHeight('MainContent_FormFrame');
        };

        function out() {
            window.location.reload();
        }
    </script>
</asp:Content>

