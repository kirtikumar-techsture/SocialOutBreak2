<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="inner-header.ascx.vb" Inherits="tsma.inner_header1" %>
	<link id="lnkInnerTheme" href="<%=ResolveUrl("~/Content/css/apartment-style.css")%>" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="<%=ResolveUrl("~/Content/css/jquery.ui.timepicker.css?v=0.2.9")%>" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/css/jquery-ui-1.8.14.custom.css")%>" type="text/css" />
    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/css/popup.css")%>" type="text/css" />
    <script src="<%=ResolveUrl("~/Scripts/jquery-1.6.2.min.js")%>" type="text/javascript"></script>
    <script src='<%=ResolveUrl("~/Content/js/jquery.ui.timepicker.js?v=0.2.9")%>' type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js" type="text/javascript"></script>
    <script src="http://jquery-ui.googlecode.com/svn/tags/latest/external/jquery.bgiframe-2.1.2.js" type="text/javascript"></script> 
    <script src="<%=ResolveUrl("~/Content/js/pagejs/scheduler.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Content/js/popupmodel.js")%>" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var cssstyle = ''
            if (readCookie('style')) {
                cssstyle = readCookie('style');
            }
            else {
                cssstyle = 'apartment-style.css';
            }
            $('#lnkInnerTheme').attr("href", 'Content/css/' + cssstyle + '');

        });
        function readCookie(name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        }
</script>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="left" valign="top"><div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="middle" class="inheadertop"><table width="1004" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td height="166" align="left" valign="middle"><div class="inheaderimg"><table width="1004" border="0" align="center" cellpadding="0" cellspacing="0">
                  <tr>
                    <td width="600" height="164" align="left" valign="middle"><a href='<%=System.Configuration.ConfigurationManager.AppSettings("AppPath")%>'> <img src="<%=ResolveUrl("~/Content/images/header_logo.png")%>" width="423" height="82" border="0" /></a></td>
                    <td align="right" valign="middle"><table border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="12" align="left" valign="top"><img src="<%=ResolveUrl("~/Content/images/signup_left.png")%>" width="12" height="34" /></td>
                        <td align="center" valign="middle" style="background-image: url(<%=ResolveUrl("~/Content/images/signup_bg.png")%>);
                                        background-position: left top; background-repeat: repeat-x"><a href='<%=ConfigurationManager.AppSettings("AppPath")%>/SiteUserLogout.aspx' class="headertoplink">LOGOUT</a></td>
                        <td width="35" align="left" valign="top"><img src="<%=ResolveUrl("~/Content/images/signup_right.png")%>" width="12" height="34" /></td>
                      </tr>
                    </table></td>
                  </tr>
                </table></div></td>
              </tr>
              <tr>
                <td height="37" align="right" valign="middle" style="">
                
                <div style="float: left; padding-left:20px; font-family:arial; font-size:13px; color:#FFF; text-align:left">User: <font color="#febb00"><strong><%=Session("SiteUserName")%></strong></font></div>
                <div style=" float:right; padding-right:20px; font-family:arial; font-size:13px; color:#FFF">You Selected : <font color="#febb00"><strong><%=tsma.GetSetCookies.GetCookie("SelectedName")%></strong></font></div>
                </td>
              </tr>
            </table></td>
        </tr>
        </table>
    
    </div></td>
  </tr>
</table>

    