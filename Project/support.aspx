<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="support.aspx.vb" Inherits="tsma.support" %>
<%@ Register src="left.ascx" tagname="left" tagprefix="uc3" %>
<%@ Register src="inner-header.ascx" tagname="inner1" tagprefix="uc1" %>
<%@ Register src="inner-footer.ascx" tagname="inner" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Total Social Media Application</title>
<link href="Content/css/inner.css" rel="stylesheet" type="text/css" />
<script src="<%=ResolveUrl("~/Scripts/jquery-1.6.2.min.js") %>" type="text/javascript"></script>
</head>
<body>
<form id="frm" runat="server">
 <asp:ScriptManager ID="objScriptManager" runat="server"></asp:ScriptManager>
  <div id="innerpagepagemain"  >
    <uc1:inner1 ID="inner1" runat="server" />
    <div id="centermain">
                      <table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td width="170" align="left" valign="top" class="leftbg">
                            <uc3:left ID="left1" runat="server" />
                            </td>
                            <td id="tdSupport" runat="server" align="left" valign="top" class="contentbody">
                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td width="146" height="125" align="left" valign="top">
                                <a href="http://www.totalsocialmediasupport.com" target="_blank">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="30" align="left" valign="middle" class="blueboxtitle" ><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td width="32"><img src="Content/images/icon_faq.png" width="32" border="0" height="30" /></td>
                                        <td class="blueboxtitle" style=" padding-bottom:5px;">FAQs</td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top" class="blueboxbg" style="padding:20px; padding-left:30px;">FAQ's Contents <br />
                                      <br />                                    </td>
                                  </tr>
                                </table>
                                </a></td>
                                <td width="31" align="left" valign="top">&nbsp;</td>
                                <td width="303"  align="left" valign="top">
                                <a href="http://www.facebook.com/totalsocialmediacorp" target="_blank">
                                <table width="101%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td align="left" valign="middle" class="blueboxtitle" >
                                    
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td width="32"><img src="Content/images/icon_communities.png" width="33" height="29" border="0" /></td>
                                        <td class="blueboxtitle" style="padding-top:5px; padding-bottom:5px;">Communities</td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top" class="blueboxbg" style="padding:20px; padding-left:30px;">Share tips and solutions 
                                      with other Virtual Social 
                                      Media Agency users</td>
                                  </tr>
                                </table>
                                </a></td>
                                <td width="20" align="left" valign="top">&nbsp;</td>
                                <td width="348" align="left" valign="top"><a href="http://www.totalsocialmediasupport.com" target="_blank">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td align="left" valign="middle" class="blueboxtitle" ><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td width="26"><img src="Content/images/icon_contact.png" width="26" height="31" border="0"/></td>
                                        <td class="blueboxtitle" style="padding-top:5px; padding-bottom:5px;">Contact</td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top" class="blueboxbg" style="padding:20px; padding-left:30px;">Connect with us when you 
                                      can't find what you're looking 
                                      for or give us some feedback</td>
                                  </tr>
                                </table>
                                </a>
                                </td>
                                </tr>
                              </table>
                              </td>
                              <td id="tdVisalus" runat="server" align="left" valign="top" class="contentbody" visible="false">
                              <table width="50%" align="center" border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td align="left" valign="top"><a href="https://www.facebook.com/groups/528398507174288/" target="_blank">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td align="left" valign="middle" class="blueboxtitle" ><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td width="26"><img src="Content/images/icon_contact.png" width="26" height="31" border="0"/></td>
                                        <td class="blueboxtitle" style="padding-top:5px; padding-bottom:5px;">Contact</td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top" class="blueboxbg" style="padding:20px; padding-left:30px;">Click Here to connect with us when you can't find what you're looking for or give us some feedback</td>
                                  </tr>
                                </table>
                                </a>
                                </td>
                                </tr>
                              </table>
                              </td>
                          </tr>
                        </table>
                     </div>
  </div>
  <uc2:inner ID="inner2" runat="server" />
 </form>
</body>
</html>

