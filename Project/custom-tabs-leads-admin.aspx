<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="custom-tabs-leads-admin.aspx.vb" Inherits="tsma.custom_tabs_leads_admin" %>

<%@ Register src="left.ascx" tagname="left" tagprefix="uc3" %>
<%@ Register src="inner-header.ascx" tagname="inner1" tagprefix="uc1" %>
<%@ Register src="inner-footer.ascx" tagname="inner" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Total Social Media Application</title>
<script src="<%=ResolveUrl("~/Scripts/jquery-1.6.2.min.js") %>" type="text/javascript"></script>
<script type="text/javascript" src="Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
<link rel="stylesheet" type="text/css" href="Content/js/fancybox/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
    <script type="text/javascript" src="Content/js/fancybox/fancybox/video.js"></script>
<script type="text/javascript">
	function DoTrim(strComp) {
        ltrim = /^\s+/
        rtrim = /\s+$/
        strComp = strComp.replace(ltrim, '');
        strComp = strComp.replace(rtrim, '');
        return strComp;
    }
	var id = "";
    function ValidateEmail(obj)
	{
		//alert('2222' + obj.id);
		//alert('333' + obj.id.replace('rptPages_LinkButton1_',''));	
		
		id = obj.id.replace('rptPages_LinkButton1_','')
		var fields = "";
			if (DoTrim(document.getElementById('rptPages_txtEmail_' + id).value).length == 0) {
				fields = fields + "\n-- Email Address --";
			}
			else {
				var re = new RegExp();
				re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
				if (!re.test(document.getElementById('rptPages_txtEmail_' + id).value)) {
					fields = fields + "\n-- Invalid Email Address --";
				}
			}
			if (fields != "") {
				fields = "Please fill in the following details:\n--------------------------------\n" + fields;
				alert(fields);
				//args.IsValid = false
				return false;
			}
			else {
				//args.IsValid = true;
				return true;
			}
		//alert('abc' + $('#rptPages_txtEmail_' + id).val());//DoTrim(document.getElementById('').value).length);
		//alert('abc' + DoTrim(document.getElementById('rptPages_txtEmail_' + id).value).length);//DoTrim(document.getElementById('').value).length);
	}
</script>
<script type="text/javascript">
	var quantityLists = new Array();
	function ValidateQuantity(sender, args) {
		if(id != "")
		{
			var fields = "";
			if (DoTrim(document.getElementById('rptPages_txtEmail_' + id).value).length == 0) {
				fields = fields + "\n-- Email Address --";
			}
			else {
				var re = new RegExp();
				re = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
				if (!re.test(document.getElementById('rptPages_txtEmail_' + id).value)) {
					fields = fields + "\n-- Invalid Email Address --";
				}
			}
			if (fields != "") {
				fields = "Please fill in the following details:\n--------------------------------\n" + fields;
				alert(fields);
				args.IsValid = false
				//return false;
			}
			else {
				args.IsValid = true;
				//return true;
			}
		}
		
		/*for(i = 0; i < quantityLists.length; i++) {
			alert('test' + quantityLists[i].id);
			if(quantityLists[i].selectedIndex > 0) {
			 args.IsValid = true;
			return;
			}
		}
		args.IsValid = false;*/
	}
</script>
</head>
<body>
<form id="form1" runat="server">
  <asp:ScriptManager ID="objScriptManager" runat="server"></asp:ScriptManager>
  <div id="innerpagepagemain">
    <uc1:inner1 ID="inner1" runat="server" />
    <div id="centermain">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td align="left" valign="top" ><table width="974" border="0" align="left" cellpadding="0" cellspacing="0">
              <tr>
                <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td width="172" align="left" valign="top" class="leftbg"><uc3:left ID="left1" runat="server" />
                            </td>
                            <td align="left" valign="top" class="contentbody"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                
                                  <td align="left" valign="top" style="padding-bottom:10px;"> <span style="float:left;font-family: Tahoma, Geneva, sans-serifthaoma;font-size: 16px;color: #181818;margin:0px;font-weight:bold;line-height:18px;">Set Notification For Custom Tabs</span>
                                    <div style="float:right;" > 
           	<a id="btnVideo" runat="server" class="video">
            <img id="imgVideo" src="../Content/images/watch_video_tutorial_red.jpg" runat="server"  style="width:213px;height:36px"/></a>
               </div>
                
                                  </td>
                                </tr>
                                 <tr>
                                  <td align="center" height="20" valign="middle" style="color:#FF0000; font-weight:bold"><asp:Literal id="ltrMsg" runat="server"></asp:Literal>
                                   <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
				                           <strong style="color:#990066">  You have no business pages.</strong><br /><br /> <a href="javascript:CreatePage();">Click here</a> to create business page. 
		                           </asp:PlaceHolder> 
                                  </td>
                                </tr>
                                <asp:PlaceHolder id="plcData" runat="server">
                                <tr>
                                  <td align="center" valign="top" style="border: 1px solid #E9E9E9;">
                                    <div id="divLoading" style="position:absolute; margin-left:350px; margin-top:100px;" >
                                    <asp:UpdateProgress ID="UpdateProgressLib" runat="Server" DisplayAfter="0">
                                     <ProgressTemplate> <img src="Content/images/ajax-loading.gif" style="border:0px;font-family:Tahoma;font-size:12px;" alt="Loading" /> </ProgressTemplate>
                                     </asp:UpdateProgress>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanelDiv4" runat="server" UpdateMode="Conditional">
                                      <ContentTemplate>
                                  <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                      <asp:Repeater ID="rptPages" runat="server">
                                        <headertemplate>
                                          <tr  style="font-weight:bold;background-color:#DCE6F1;"  align="Center"  Font-Bold="true" >
                                            <td align="left" style="color:#000000;" height="30" >Page Name</td>
                                            <td align="left" style="color:#000000;" >Date + (PST)</td>
                                            <td align="center" width="70" style="color:#000000;" >Published by</td>
                                            <td align="center" width="60" style="color:#000000;" >Notification Status</td>
                                            <td align="center" width="70" style="color:#000000;" >Notification Email</td>
                                            <td align="center" width="30" style="color:#000000;" >#Leads</td>                                            
                                            <td align="center" width="80" style="color:#000000;" >View Leads</td>
                                            <td align="center" width="30" style="color:#000000;" >Save</td>
                                          </tr>
                                        </headertemplate>
                                        <itemtemplate>
                                          <tr align="left" valign="middle" onMouseOver="javascript:this.style.backgroundColor='#ECECEC'"  onMouseOut="javascript:this.style.backgroundColor='#FFFFFF'"  >
                                            <td align="left" height="35" width="120"><%#Eval("PageName")%></td>
                                            <td align="center" ><%#Eval("PublishedDate")%></td>
                                            <td align="center" ><%#Eval("PublishedBy")%></td>
                                            <td align="center" width="100" >
											<asp:PlaceHolder ID="pnlAutoPostOff" runat="server" Visible='<%#Eval("IsNotification")=0%>'>
                                            <asp:LinkButton id="lnkOff" runat="server" CssClass="bluetablink" OnCommand="TurnOnnOffAutoPost" CommandName='<%#Eval("Pageid")%>' ToolTip="Click here to turn On"><img src='<%=ResolveUrl("Content/images/Off_bullate.png") %>' width="10" height="10" hspace="3" border="0" />Off</asp:LinkButton>
                                            </asp:PlaceHolder> 
                              				<asp:PlaceHolder ID="pnlAutoPostOn" runat="server"  Visible='<%#Eval("IsNotification")=1%>'>
                                            <asp:LinkButton id="lnkOn" runat="server" CssClass="bluetablink" OnCommand="TurnOnnOffAutoPost" CommandName='<%#Eval("Pageid")%>' ToolTip="Click here to turn Off"><img src='<%=ResolveUrl("Content/images/On_bullate.png") %>' width="10" height="10" hspace="3" border="0" />On</asp:LinkButton>
                                            </asp:PlaceHolder> 
                                           <%-- <asp:PlaceHolder ID="pnlAutoPostNull" runat="server"  Visible='<%#Eval("IsNotification")=-1%>'>
                                            --
                                            </asp:PlaceHolder>--%>
											</td>
                                            <td align="center" width="100" >
                                            <asp:PlaceHolder ID="PlaceHolder7" runat="server" >
                                            <input type="text" runat="server" id="txtEmail" width="100" />
                                           
                                            </asp:PlaceHolder> 
                                            <%--<asp:PlaceHolder ID="PlaceHolder7" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <input type="text" runat="server" id="txtEmail" width="100" />
                                           
                                            </asp:PlaceHolder> 
                              				<asp:PlaceHolder ID="PlaceHolder8" runat="server"  Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder>--%> 
                                            </td>
                                            <td align="center">
                                            <asp:PlaceHolder id="PlaceHolder2" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <%#Eval("Leads")%>
                                            </asp:PlaceHolder>
                                             <asp:PlaceHolder id="PlaceHolder3" runat="server" Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder>
                                            
                                            </td>
                                            <td align="center" width="220">
                                             <asp:PlaceHolder id="plcView" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <a href="custom-tabs-leads?lid=<%#Eval("Pageid")%>" class="bluetablink">View Leads</a>
                                            </asp:PlaceHolder>
                                             <asp:PlaceHolder id="PlaceHolder1" runat="server" Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder>
                                         
                                            </td>
                                            <td align="center">
                                            <asp:PlaceHolder ID="PlaceHolder4" runat="server" >
                                            <asp:LinkButton id="LinkButton1" runat="server" CssClass="bluetablink" OnCommand="SaveNotificationEmail" OnClientClick="return ValidateEmail(this);" CommandName='<%#Eval("Pageid")%>' ToolTip="Click here to Save Notification Email">Save</asp:LinkButton>
                                            </asp:PlaceHolder> 
                                            <%--<asp:PlaceHolder ID="PlaceHolder4" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <asp:LinkButton id="LinkButton1" runat="server" CssClass="bluetablink" OnCommand="SaveNotificationEmail" OnClientClick="ValidateEmail(this);" CommandName='<%#Eval("Pageid")%>' ToolTip="Click here to Save Notification Email">Save</asp:LinkButton>
                                            </asp:PlaceHolder> 
                                            <asp:PlaceHolder ID="PlaceHolder6" runat="server"  Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder>--%>
                                            
                                            </td>
                                            
                                          </tr>
                                          <input type="hidden" runat="server" id="hdnPageId" />
                                        </itemtemplate>
                                         <alternatingitemtemplate>
                                          <tr align="left" valign="middle" style="background-color:#F7F7F9;" onMouseOver="javascript:this.style.backgroundColor='#ECECEC'" onMouseOut="javascript:this.style.backgroundColor='#F7F7F9'">
                                            <td align="left" height="35" width="120"><%#Eval("PageName")%></td>
                                            <td align="center" >
                                            
                                            <%#Eval("PublishedDate")%></td>
                                            <td align="center" ><%#Eval("PublishedBy")%></td>
                                            <td align="center" width="100" >
											<asp:PlaceHolder ID="pnlAutoPostOff" runat="server" Visible='<%#Eval("IsNotification")=0%>'>
                                            <asp:LinkButton id="lnkOff" runat="server" CssClass="bluetablink" OnCommand="TurnOnnOffAutoPost" CommandName='<%#Eval("Pageid")%>' ToolTip="Click here to turn On"><img src='<%=ResolveUrl("Content/images/Off_bullate.png") %>' width="10" height="10" hspace="3" border="0" />Off</asp:LinkButton>
                                            </asp:PlaceHolder> 
                              				<asp:PlaceHolder ID="pnlAutoPostOn" runat="server"  Visible='<%#Eval("IsNotification")=1%>'>
                                            <asp:LinkButton id="lnkOn" runat="server" CssClass="bluetablink" OnCommand="TurnOnnOffAutoPost" CommandName='<%#Eval("Pageid")%>' ToolTip="Click here to turn Off"><img src='<%=ResolveUrl("Content/images/On_bullate.png") %>' width="10" height="10" hspace="3" border="0" />On</asp:LinkButton>
                                            </asp:PlaceHolder> 
                                            <%--<asp:PlaceHolder ID="pnlAutoPostNull" runat="server"  Visible='<%#Eval("IsNotification")=-1%>'>
                                            --
                                            </asp:PlaceHolder>--%>
											</td>
                                            <td align="center"  width="100">
                                            <asp:PlaceHolder ID="PlaceHolder7" runat="server">
                                            <input type="text" runat="server" id="txtEmail" width="100"/>
                          
                                            </asp:PlaceHolder> 
                                            <%--<asp:PlaceHolder ID="PlaceHolder7" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <input type="text" runat="server" id="txtEmail" width="100"/>
                                                                                      
                                            </asp:PlaceHolder> 
                              				<asp:PlaceHolder ID="PlaceHolder8" runat="server"  Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder> --%>
                                           
                                            </td>
                                            <td align="center">
                                            <asp:PlaceHolder id="PlaceHolder2" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <%#Eval("Leads")%>
                                            </asp:PlaceHolder>
                                             <asp:PlaceHolder id="PlaceHolder3" runat="server" Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder>
                                            </td>
                                            <td align="center" width="220">
                                              <asp:PlaceHolder id="plcView" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <a href="custom-tabs-leads?lid=<%#Eval("Pageid")%>" class="bluetablink">View Leads</a>
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder id="PlaceHolder1" runat="server" Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder>
                                         
                                            </td>
                                            <td align="center">
                                            <asp:PlaceHolder ID="PlaceHolder4" runat="server" >
                                            <asp:LinkButton id="LinkButton1" runat="server" CssClass="bluetablink" OnCommand="SaveNotificationEmail" CommandName='<%#Eval("Pageid")%>' OnClientClick="return ValidateEmail(this);" ToolTip="Click here to Save Notification Email">Save</asp:LinkButton>
                                            </asp:PlaceHolder> 
                                           <%-- <asp:PlaceHolder ID="PlaceHolder4" runat="server" Visible='<%#IIF(Eval("Leads")<>0,"true","false")%>'>
                                            <asp:LinkButton id="LinkButton1" runat="server" CssClass="bluetablink" OnCommand="SaveNotificationEmail" CommandName='<%#Eval("Pageid")%>' OnClientClick="ValidateEmail(this);" ToolTip="Click here to Save Notification Email">Save</asp:LinkButton>
                                            </asp:PlaceHolder> 
                                            <asp:PlaceHolder ID="PlaceHolder6" runat="server"  Visible='<%#IIF(Eval("Leads")=0,"true","false")%>'>
                                            --
                                            </asp:PlaceHolder>--%>
                                            </td>
                                          </tr>
                                          <input type="hidden" runat="server" id="hdnPageId" />
                                          
                                        </alternatingitemtemplate>
                                        
                                      </asp:Repeater>
                                      <%--<asp:CustomValidator runat="server" ID="customValidator" ClientValidationFunction="ValidateQuantity" Text="" />--%>
                                    </table>
                                     </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                                </tr>
                                </asp:PlaceHolder>
                              </table>
                             
                        </table></td>
                    </tr>
                  </table></td>
              </tr>
            </table></td>
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
