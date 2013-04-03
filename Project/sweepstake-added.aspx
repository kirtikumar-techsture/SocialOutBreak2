<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sweepstake-added.aspx.vb" Inherits="tsma.sweepstake_added" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Amerifirst Sweepstake</title>
    <style type="text/css" media="screen">
	body {
	width:820px;
	overflow:hidden;
	margin:0; padding:0; border:0;
	}
    #divBackGround 
    {
        position:absolute;
        background-color:#ffffff;
		filter:alpha(opacity=70);
        opacity: 0.70;
        -moz-opacity:0.70;
    }
</style>
<script src="../content/js/jquery-1.6.2.min.js" type="text/javascript"></script>
	<script src="../content/js/popupmodel.js" type="text/javascript"></script>
</head>
<body>
<script src='https://connect.facebook.net/en_US/all.js'></script>
<div id="fb-root"></div>
<script>

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=400989213301355";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));
    
</script>
<script>
    //so.techsturedevelopment.com
    window.fbAsyncInit = function () {
        FB.init({
            //appId: '343180435727440', //So.tsmaapplication.com,
            appId: '400989213301355', //summasocialapp.com 
            //appId: '281207455306383', //uswebsocial.com 
            //appId: '322568194479566', //meadowcreeksocial.com 
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true // parse XFBML
        });
    };

    (function () {
        var e = document.createElement('script');
        e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
        e.async = true;
        document.getElementById('fb-root').appendChild(e);
    } ());
</script>
    <form id="form1" runat="server">
 <div id="divUnLiked" runat="server" Visible="">
<table width="810" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="137" align="left" valign="top" bgcolor="#FFFFFF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="left" valign="top" style="background-image:url(../Content/images/amerifirst-sweepstake-images/facebook_amerifirst_sweepstakes_header_logo.jpg); height:137px; width:570px; background-position:top; background-repeat:no-repeat">&nbsp;</td>
        <td align="left" valign="top" style="background-image:url(../Content/images/amerifirst-sweepstake-images/facebook_amerifirst_sweepstakes_header_like_us.jpg); height:137px; width:240px; background-position:top; background-repeat:no-repeat">&nbsp;</td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td align="left" valign="top" style="background-image:url(../Content/images/amerifirst-sweepstake-images/facebook_amerifirst_sweepstakes_center_img.jpg); height:558px; width:810px; background-position:top; background-repeat:no-repeat">&nbsp;</td>
  </tr>
  <tr>
    <td height="133" align="center" valign="top" bgcolor="#FFFFFF">&nbsp;</td>
  </tr>
</table>
</div>
    </form>
</body>
</html>
