<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="vision-sweepstake.aspx.vb" Inherits="tsma.vision_sweepstake" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css"> img#share_button {cursor: pointer;} </style>

	<script src="../content/js/jquery-1.6.2.min.js" type="text/javascript"></script>
	<script src="../content/js/popupmodel.js" type="text/javascript"></script>
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
<script type="text/javascript">
    $(document).ready(function () {
					//alert("page   " + $('#hdnFBPageName').val());
  				    //alert("page url  " + $('#hdnURL').val());
					//alert("page Id  " + $('#hdnFBPageId').val());
	var fbpagename=$('#hdnFBPageName').val();
	var fburl=$('#hdnURL').val();
	//FB.Canvas.setAutoResize();
	//so.techsturedevelopment.com

	       
			
			 //https://www.summasocialapp.com/
				 $('#share_button').live('click', function (e) {
                    e.preventDefault();
                    FB.ui(
                    {
                  		    method: 'feed',
							name: fbpagename,
                            link: fburl,
							//name: 'Summa Social',
                            //link: 'https://www.summasocialapp.com',
                            picture: 'http://www.summasocialapp.com/Content/images/vision-sweepstake-images/facebook_visionproducts_left_img1.jpg',
                            caption: '',
                            description: 'Sweepstakes: The entire Vision Products Super food Skin Care range of specialty cleansers,exfoliant.moisturisers,luxury soaps and body lotions worth over $NZD1200.00!',
                            message: ''
							//actions: [
								//	{ name: 'share', link: 'http://www.google.com' }
								  //]
                    });
	                });
    });
	
	function PopupCenter(pageURL,title,w,h) 
		{
			var left = (screen.width/2)-(w/2);
			var top = (screen.height/2)-(h/2);
			if(navigator.appName=='Microsoft Internet Explorer')
			{
			newwin =	window.open (pageURL);
			}
			else
			{
			newwin = window.open (pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, width='+w+', height='+h+', top='+top+', left='+left);
			}
			newwin.focus();
		} 
</script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js" type="text/javascript"></script>

    
	
    <div id="divForm" style="width:100%; height:100%; text-align:center; background-image:url(../Content/facebookalert/images/popup_bg.png); position:absolute; z-index:999999999; text-align:center;  display:none;">
			    <div id="popup_content" style="padding-top:200px; padding-left:250px; text-align:left">
    <table width="230" height="260" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td bgcolor="#395992" style="border:5px solid #a5e0ff; padding:2px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="right" valign="top"><a href="javascript:;" onclick="$('#divForm').hide();"><img src="../Content/images/popup_close.gif" width="13" height="13" /></a></td>
          </tr>
          <tr>
            <td align="center" valign="top"><table width="177" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td align="left" valign="top" style="font-family:Arial; font-size:13px; color:#FFF; font-weight:bold; line-height:17px;">Enter your contact<br />
                  information to sign up.</td>
              </tr>
              <tr>
                <td align="left" valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td align="left" valign="top"><label>
                  <input name="txtFullName" type="text" id="txtFullName" style="background-image:url(../Content/images/popup_input_bg.jpg); width:172px; height:34px; border:0px; padding-left:5px; padding-right:5px; font-size:13px; font-family:Arial; color:#676969;" value="*Full Name" />
                </label></td>
              </tr>
              <tr>
                <td align="left" valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td align="left" valign="top"><label>
                  <input name="txtEmail" type="text" id="txtEmail" style="background-image:url(../Content/images/popup_input_bg.jpg); width:172px; height:34px; border:0px; padding-left:5px; padding-right:5px; font-size:13px; font-family:Arial; color:#676969;" value="*Email Address" />
                </label></td>
              </tr>
              <tr>
                <td align="left" valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="30" align="left" valign="top" style="background-image:url(../Content/images/popup_rediyo_button.jpg); background-repeat:no-repeat;"><label>
                      <input type="checkbox" name="chkOverOld" id="chkOverOld" />
                    </label></td>
                    <td align="left" valign="top" style="font-family:Arial; font-size:13px; color:#FFF; font-weight:bold; line-height:17px;">I am Over <br />
                      18 years old.</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td align="left" valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td align="left" valign="top"><input type="image" src="../Content/images/popup_enter_button.jpg" width="177" height="37" id="btnSubmit" /></td>
              </tr>
              <tr>
                <td align="left" valign="top">&nbsp;</td>
              </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
    </table>
	</div>
	</div>

	<div id="divMessage" style="width:100%; height:100%; text-align:center; background-image:url(../Content/facebookalert/images/popup_bg.png); position:absolute; text-align:center;  display:none;">
			    <div id="popup_content" style="padding-top:200px; padding-left:250px; text-align:left;">
    <table width="232" height="260" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="center" valign="top" style="background-image:url(../Content/images/Popup_bg.jpg); background-repeat:no-repeat; width:230px; height:307px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="30" align="right" valign="top"><a href="javascript:;" onclick="$('#divMessage').hide();"><img src="../Content/images/popup_close1.gif" width="13" height="13" hspace="4" vspace="4" /></a></td>
          </tr>
          <tr>
            <td align="center"><img src="../Content/images/popup_thank_you.jpg" width="183" height="49" /></td>
          </tr>
          <tr>
            <td align="center" height="80"><img src ="../Content/images/sharebtn.png" id ="share_button"></td>
          </tr>
          <tr>
            <td height="40" align="center" style="color:#32579b; font-weight:bold;">VIEW MY BUSINESS</td>
          </tr>
          <tr>
            <td align="center" style="color:#da1c0a;"><a id="linkpage" runat="server" target="_blank" >Click Here</a></td>
          </tr>
          </table></td>
        </tr>
      </table>
	</div>
    </div>

<div id="divUnLiked" runat="server" Visible="false">
<table id="Home" style="display:;" width="810" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="346" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_left_img1.jpg); background-position:top; background-repeat:no-repeat; width:346px; height:381px; "></td>
          </tr>
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_left_img2.jpg); background-position:top; background-repeat:no-repeat; width:346px; height:335px; "></td>
          </tr>
        </table></td>
        <td width="464" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_right_like_img1.jpg); background-position:top; background-repeat:no-repeat; width:464px; height:217px;">&nbsp;</td>
          </tr>
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_right_img2.jpg); background-position:top; background-repeat:no-repeat; width:464px; height:499px;">&nbsp;</td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="94" valign="top" style=" padding:28px; padding-top:20px; font-family:arial; font-size:12px; color:#000; text-transform:uppercase; line-height:18px;">*COMPETITION RUNS UNTIL THE 2ND OF DECEMBER 2012.<br />
    WINNER WILL BE SELECTED ON THE 4TH OF DECEMBER 2012.<br/>
    <font color="#5784d2"><a style="cursor:pointer;" onclick="PopupCenter('../Generic_Sweepstakes_Rules_and_Privacy_Policy.pdf', 'Privacy Policy','800','700')">OFFICIAL RULES & PRIVACY POLICY</a></font>. THIS CONTEST IS NOT ASSOCIATED WITH FACEBOOK.</td>
  </tr>
</table>
</div>


<div id="divliked" runat="server" Visible="">
<table id="Thanks"  width="810" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="346" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_left_img1.jpg); background-position:top; background-repeat:no-repeat; width:346px; height:381px; "></td>
          </tr>
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_left_img2.jpg); background-position:top; background-repeat:no-repeat; width:346px; height:335px; "></td>
          </tr>
        </table></td>
        <td width="464" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_right_img1.jpg); background-position:top; background-repeat:no-repeat; width:464px; height:217px;"><div style="padding-top:110px; padding-left:33px;"><a href="javascript:;" onclick="ShowForm();" ><img src="../Content/images/vision-sweepstake-images/facebook_transparent_img.png" width="199" height="52" border="0" align="top" style="cursor:pointer;"/></a></div></td>
          </tr>
          <tr>
            <td align="left" valign="top" style="background-image:url(../Content/images/vision-sweepstake-images/facebook_visionproducts_right_img2.jpg); background-position:top; background-repeat:no-repeat; width:464px; height:499px;">&nbsp;</td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="94" valign="top" style=" padding:28px; padding-top:20px; font-family:arial; font-size:12px; color:#000; text-transform:uppercase; line-height:18px;">*COMPETITION RUNS UNTIL THE 2ND OF DECEMBER 2012.<br />
    WINNER WILL BE SELECTED ON THE 4TH OF DECEMBER 2012.<br/>
    <font color="#5784d2"><a style="cursor:pointer;" onclick="PopupCenter('../Generic_Sweepstakes_Rules_and_Privacy_Policy.pdf', 'Privacy Policy','800','700')">OFFICIAL RULES & PRIVACY POLICY</a></font>. THIS CONTEST IS NOT ASSOCIATED WITH FACEBOOK.</td>
  </tr>
</table>
</div>

  <input type="hidden" id="hdnFBUserId" runat="server" name="hdnFBUserId"  />
  <input type="hidden" id="hdnTSMAUserId" runat="server" name="hdnTSMAUserId" />
  <input type="hidden" id="hdnFBPageId" runat="server" name="hdnFBPageId"  />
  <input type="hidden" id="hdnFBPageName" runat="server" name="hdnFBPageName"  />
   <input type="hidden" id="hdnFBApplicationId" runat="server" name="hdnFBPageName"  />
   <input type="hidden" id="hdnSweepStakeAppId" runat="server" name="hdnSweepStakeAppId"  />
      <input type="hidden" id="hdnSweepStakeAppName" runat="server" name="hdnSweepStakeAppName"  />
   <input type="hidden" id="hdnURL" runat="server" name="hdnURL"  />
      
   
</body>
</html>
<script language="javascript">
function ShowForm()
{
	$('#divForm').show();
	 //showDivPopup('divForm');
}
function Validate()
{
var fields
fields = "";
if (DoTrim(document.getElementById('txtFullName').value).length == 0 || DoTrim(document.getElementById('txtFullName').value)=='*Full Name' ) {
	fields = "\n-- Full Name --";
}

var re = new RegExp();
re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
var sinput;
sinput = "";
if (DoTrim(document.getElementById('txtEmail').value).length == 0 || DoTrim(document.getElementById('txtEmail').value)=='*Email Address' ) {
	fields = fields + "\n-- Email Address --";
}
else {
	sinput = DoTrim(document.getElementById('txtEmail').value);
	if (!re.test(sinput)) {
		fields = fields + "\n-- Invalid Email Address --";
		document.getElementById('txtEmail').value == "";
	}
}
if (document.getElementById('chkOverOld').checked == false) {
	fields = fields + "\n-- Confirm Your Age --";
}
if (fields != "") {
	fields = "Please fill in the following details:\n--------------------------------" + fields;
	alert(fields);
	return false;
}
else {
	return true;
}
}
function DoTrim(strComp) {
ltrim = /^\s+/
rtrim = /\s+$/
strComp = strComp.replace(ltrim, '');
strComp = strComp.replace(rtrim, '');
return strComp;
}
$(document).ready(function () {
    $("#txtFullName").focus(function () {
        $("#txtFullName").val($("#txtFullName").val().replace('*Full Name', ''));
    });

    $("#txtFullName").blur(function () {
        if ($("#txtFullName").val() == '') {
            $("#txtFullName").val('*Full Name');
        }
    });

    $("#txtEmail").focus(function () {
        $("#txtEmail").val($("#txtEmail").val().replace('*Email Address', ''));
    });

    $("#txtEmail").blur(function () {
        if ($("#txtEmail").val() == '') {
            $("#txtEmail").val('*Email Address');
        }
    });

    $("#btnSubmit").click(function () {
        if (Validate()) {
           // alert('ajax/ajax-sweepstak-contest.aspx?nm='+ $("#txtFullName").val() +'&em='+ $("#txtEmail").val() +'&fbuid='+ $("#hdnFBUserId").val() +'&tsmauid='+ $("#hdnTSMAUserId").val() +'&fbpid='+ $("#count").val() +'');
            $.ajax({ url: '../ajax/ajax-sweepstak-contest.aspx?nm=' + $("#txtFullName").val() + '&appid=' + $("#hdnSweepStakeAppId").val() + '&appname=' + $("#hdnSweepStakeAppName").val() + '&em=' + $("#txtEmail").val() + '&fbuid=' + $("#hdnFBUserId").val() + '&tsmauid=' + $("#hdnTSMAUserId").val() + '&fbpid=' + $("#hdnFBPageId").val() + '&swid=3&fbpagename=' + $("#hdnFBPageName").val(), success: function (result) {
                //alert(result);
                //hideDivPopup('divForm');
                //showDivPopup('divMessage');
				$('#divForm').hide();
				$('#divMessage').show();
                
            }
            });
        }
        else {
            return false;
        }
    });
});
</script>