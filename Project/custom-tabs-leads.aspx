<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="custom-tabs-leads.aspx.vb" Inherits="tsma.custom_tabs_leads" %>

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
          <td align="left" valign="top" class="contentbody">
           <div id="divLoading" style="position:absolute;" >
<asp:UpdateProgress ID="UpdateProgressLib" runat="Server" DisplayAfter="0">
 <ProgressTemplate> <img src="Content/images/ajax-loading.gif" style="border:0px;font-family:Tahoma;font-size:12px;" alt="Loading" /> </ProgressTemplate>
 </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="uptMain1" runat="server">
<ContentTemplate>
          
          <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="30" align="left" valign="top">
                 <div align="left" style="float:left"><h6>My Custom Tabs Leads</h6></div>
                <div align="right" style="float:right; margin-right:20px;"><a href="custom-tabs-leads-admin" class="bluetablink">Back</a></div>
                </td>
              </tr>              
              <tr>
                	<td align="center" colspan="2" style="color:#FF0000; font-weight:bold; font-size:18px" ><asp:Literal id="ltrMsg" runat="server"></asp:Literal><br/><br/></td>
              	</tr>
                <tr>
                	<td align="left" valign="top">
                    	<div id="divAutoPostFanPages" style="display:; z-index:100; width:815px; height:100px; position:; border:1px solid #CCCCCC; background-color:#f6f6f6;" title="Business Pages">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td align="left" valign="top" style="padding:20px; padding-bottom:10px; ">
                                            <asp:DropDownList ID="drpFanPages" runat="server" AutoPostBack="true">
                         </asp:DropDownList>                  
                                    </td>
                                  </tr>
                                  <tr>
                                  	<td align="left" valign="top" style="padding-left:20px">
                                    <div style="width:150px; position:relative; float:left; vertical-align:middle; padding-top:10px;" align="left">
                                    <a href="javascript:;" id="btnExport" runat="server" class="bluetablink">
                                    Export Page Leads
                                    </a>
                                    </div>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <div align="left" style="width:600px; float:left">
                                    <font color="#666666" style=" line-height:18px;" >Click the button to export your leads as a CSV file to use for general followup or in software such as Aweber, Constant Contact, or Mailchimp. </font>
                                    </div>
                                    </td>
                                  </tr>
                                </table>
                              </div>
                    </td>
                </tr>
                <tr >
                	<td align="right" height="23" colspan="2" ><strong><asp:PlaceHolder ID="phPagingTop" runat="server"></asp:PlaceHolder></strong></td>
                </tr>
              
              <tr>
                  <td align="center" colspan="2" style="border:0px solid #CFCACA;padding:1px;">
				  <asp:Gridview ID="dgrLeads" runat="server" AutoGenerateColumns="False" PagerStyle-HorizontalAlign="Right"
						PagerStyle-PrevPageText="Prev" PagerStyle-NextPageText="Next" PagerStyle-Mode="NumericPages"
						Font-Size="8pt" Font-Name="Verdana" CellSpacing="0" CellPadding="3" ShowFooter="false" PageSize="30"
						BorderWidth="0" BorderColor="#EAEAEA" AllowPaging="false" Width="100%" GridLines="None" allowsorting="true" >
                      <HeaderStyle Font-Bold="True" HorizontalAlign="left" BackColor="#BBBBBB" ForeColor="#515050" Height="30"></HeaderStyle>
                        <Columns>
						 <asp:TemplateField HeaderText="Name <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"  SortExpression="Name" ItemStyle-Height="25" ItemStyle-HorizontalAlign="Left" >
						<ItemTemplate >
						<%#Eval("Name")%>	
						</ItemTemplate>												
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Email <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>"  SortExpression="Email" ItemStyle-HorizontalAlign="Left" headerstyle-width="200">
						<ItemTemplate >
						<a href="mailto:<%#Eval("Email")%>"><%#Eval("Email")%></a>

						</ItemTemplate>												
					</asp:TemplateField>					
					<asp:TemplateField HeaderText="Phone <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>" ItemStyle-HorizontalAlign="Left" headerstyle-width="70" SortExpression="Phone" >
						<ItemTemplate >
							<%#Eval("Phone")%>	
						</ItemTemplate>												
					</asp:TemplateField>											 
					<asp:TemplateField HeaderText="Message" ItemStyle-HorizontalAlign="Left" headerstyle-width="150"  >
						<ItemTemplate >
							<%#Eval("Message")%>	
						</ItemTemplate>												
					</asp:TemplateField>	
                    <asp:TemplateField HeaderText="Status <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>" ItemStyle-HorizontalAlign="center" headerstyle-HorizontalAlign="center"  headerstyle-width="100" SortExpression="Status">
						<ItemTemplate >
							<%#Eval("Status")%>	
						</ItemTemplate>												
					</asp:TemplateField>										 
                   <asp:TemplateField HeaderText="Lead From <img src='content/images/i-sort.gif' id='imgsort' border='0' align='absmiddle'/>" ItemStyle-HorizontalAlign="center" headerstyle-HorizontalAlign="center"  headerstyle-width="100" SortExpression="LeadFrom">
						<ItemTemplate >
							<%#Eval("LeadFrom")%>	
						</ItemTemplate>												
					</asp:TemplateField>											 
					 <asp:TemplateField HeaderText="Date <img src='content/images/desc.gif' id='imgsort' border='0' align='absmiddle'/>" headerstyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" headerstyle-width="90" SortExpression="Date">
						<ItemTemplate >
							<%#Eval("Date")%>	
						</ItemTemplate>												
					</asp:TemplateField>											 
                     </Columns>
					   <AlternatingRowStyle BackColor="#F5F5F5"></AlternatingRowStyle>                  
                      </asp:Gridview>
					  <input type="hidden" runat="server" id="hdnSortexp" value="date desc">
                  </td>
                </tr>
                <tr>
                	<td align="right" height="23" colspan="2"><strong><asp:PlaceHolder ID="phPagingBottom" runat="server"></asp:PlaceHolder></strong></td>
                </tr>
            </table>
            
                </ContentTemplate>
                <Triggers>
			<asp:PostBackTrigger ControlID="btnExport" />
			</Triggers>
</asp:UpdatePanel>

			</td>
        </tr>
      </table>
    </div>
  </div>
  <uc2:inner ID="inner2" runat="server" />
</form>
</body>
</html>
<script type="text/javascript" language="javascript" src="Content/js/loading.js"></script>