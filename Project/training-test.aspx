<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="training-test.aspx.vb" Inherits="tsma.training_test" %>

<%@ Register src="inner-footer.ascx" tagname="inner" tagprefix="uc2" %>
<%@ Register src="left.ascx" tagname="left" tagprefix="uc3" %>
<%@ Register src="inner-header.ascx" tagname="inner" tagprefix="uc1" %>
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
    <uc1:inner ID="inner1" runat="server" />
    <div id="centermain">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="170" align="left" valign="top" class="leftbg"><uc3:left ID="left1" runat="server" />
          </td>
          <td align="left" valign="top" class="contentbody"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="30" align="left" valign="top"><h6>Your Online Training Center</h6></td>
              </tr>
              <tr>
                <td>Helping professionals launch smart Social Media Marketing <br />
                  so your business goals are accomplished</td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td>
             <asp:UpdatePanel runat="server" id="UpdatePanel">
                        <ContentTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                       
                      <td width="581" align="left" valign="top">
                      <div id="divVideo">
                        
                          <div id="divVideoTitle" style=" font-family:Arial, Helvetica, sans-serif; font-weight:bold; padding-bottom:10px; font-size:12px; "><span id="spanTitle" runat="server"></span></div>
                          <div style="width:581px; height:370px; overflow:hidden;"><span id="spnVideoEmbed" runat="server" /></div>
                           
                        </div>
                        </td>
                 
                      <td width="20" align="left" valign="top">&nbsp;</td>
                      <td align="left" valign="top" class="blueboxbg" style="padding:0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td  width=100% align="left" valign="middle" class="blueboxtitle" >Facebook</td>
                          </tr>
                          <tr>
                            <td align="left" valign="top" style="padding-left:15px; padding-right:5px;">
                            <div style="height:330px; overflow:auto; overflow-x:hidden">
                                <asp:Repeater ID="rptCategory" runat="server" >
                                          <itemtemplate>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0"  >
                                              <tr>
                                                <td height="30">
                                                <div class="subtitle" style="height:25px; padding-top:10px; border-bottom:2px solid #FFF">
                                                <span class="arial14"; style="text-transform: capitalize; color:#000"><strong><%#Eval("vcat_Name")%></strong></span>
                                                </div>
                                                </td>
                                              </tr>
                                              <tr>
                                                <td  style="padding-bottom:5px;padding-top:10px;" width="100%">
                                                <asp:DataList ID="dlstVideos" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" ItemStyle-Width="100%" DataSource='<%# BindAllVideos(Container.dataItem("vcat_id")) %>' >
                                                    <itemtemplate>
                                                      <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                          <td width="100%" valign="top" style="height:20px;">
                                                          <asp:LinkButton  OnCommand="imgVideoThumb_Click" CommandArgument='<%# Container.DataItem("vd_Id") %>' id="lnkLibCatSel" runat="server"><%# Container.DataItem("vd_title") %></asp:LinkButton>
                                                          </td>
                                                        </tr>
                                                        
                                                      </table>
                                                    </itemtemplate>
                                                </asp:DataList>
                                                </td>
                                              </tr>
                                            </table>
                                          </itemtemplate>
                                      </asp:Repeater>
                            </div></td>
                          </tr>
                        </table></td>
                    </tr>
                  </table>
                 </ContentTemplate>
                            </asp:UpdatePanel>
                  </td>
              </tr>
              <tr>
                <td>&nbsp;</td>
              </tr>
            </table></td>
        </tr>
      </table>
    </div>
  </div>
  <uc2:inner ID="inner2" runat="server" />
</form>
</body>
</html>
