<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="contact-customtab.aspx.vb" Inherits="tsma.contact_customtab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--Update End css-->
<style type="text/css">
body 
{
    overflow:hidden;
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	font-family:arial;
}
</style>
<script type="text/javascript" language="javascript" src="https://www.summasocialapp.com/fb/js/jquery-1.7.2.min.js"></script>
<script src="https://connect.facebook.net/en_US/all.js"></script>
<script>
	function SetBodyStyle()
	{
		$("body").css("overflow", "auto");
	}
    $(document).ready(function () {
        //	$('.pointer share').attr('rel','hi');
        FB.Canvas.setSize();
    });
	
	FB.init({
        appId: '481614931883782',
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        oauth: true // enable OAuth 2.0
    });
</script>
</head>
<body>
    
    <form id="form1" runat="server" >
    
   <div id="divContent" style="width:810px;" runat="server"></div>
    </form>
    <input type="hidden" id="hdnFBUserId" runat="server" name="hdnFBUserId"  />
  <input type="hidden" id="hdnTSMAUserId" runat="server" name="hdnTSMAUserId" />
  <input type="hidden" id="hdnFBPageId" runat="server" name="hdnFBPageId"  />
<input type="hidden" id="hdnFBPageName" runat="server" name="hdnFBPageName"  />
   <input type="hidden" id="hdnAppId" runat="server" name="hdnAppId"  />
   <input type="hidden" id="hdnAppName" runat="server" name="hdnAppName"  />
  <input type="hidden" id="hdnVideoTop"  name="hdnVideoTop"  />
</body>
</html>
