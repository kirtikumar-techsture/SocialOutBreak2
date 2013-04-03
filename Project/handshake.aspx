<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="handshake.aspx.vb" Inherits="tsma.handshake" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="Scripts/jquery-1.6.2.min.js"  type="text/javascript"></script>
<head runat="server">
    <title>Total Social Media Application</title>
    <script language="javascript" type="text/javascript" src="Content/js/pagejs/handshake.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="hidden" id="hdnId" runat="server" value="" />
   
     <div id="diverr" style="display:none; font:'Courier New', Courier, monospace; font-style:normal; color:#FF0000">Facebook is experiencing problems. Error in connecting to Facebook, Please Try Again...</div>
    </div>
    </form>
</body>
</html>
