<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="left.ascx.vb" Inherits="tsma.uleft" %>
 <div style="padding:15px; padding-top:5px;">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td width="10" align="left" valign="middle" class="leftline"><img src="<%=ResolveUrl("../content/images/leftarrow.gif")%>" width="5" height="9" /></td>
      <td width="95%" height="40" align="left" valign="left" class="leftline"><a href='<%=ConfigurationManager.AppSettings("AppPath")%>friendlist' class="leftlink">My Facebook Friends</a></td>
    </tr>
    <tr>
      <td width="10" align="left" valign="middle" class="leftline"><img src="<%=ResolveUrl("../content/images/leftarrow.gif")%>" width="5" height="9" /></td>
      <td height="40" align="left" valign="left" class="leftline"><a href='<%=ConfigurationManager.AppSettings("AppPath")%>fanpages' class="leftlink">My Facebook Fan Pages</a></td>
    </tr>
    <tr>
      <td width="10" align="left" valign="middle" class="leftline"><img src="<%=ResolveUrl("../content/images/leftarrow.gif")%>" width="5" height="9" /></td>
      <td height="40" align="left" valign="middle" class="leftline"><input type="hidden" id="hdnMap" value="0" />
		   <a href="javascript:;" id="lnkPostNLeap" class="leftlink">Post N Leap Scheduler</a>
	 </td>
    </tr>
	<tr id="menuPostScheduler" style="display:none;">
      <td height="40" colspan="2" align="left" valign="middle" class="leftline" style="padding-left:10px;">
	  <input type="hidden" id="hdnMap" value="0" />        
          <table cellpadding="2" cellspacing="1" border="0" width="100%">
		  <tr>
		  <td style="line-height:22px;">
		  <img src='<%=ResolveUrl("../content/images/light_gray_arrow.png")%>' />&nbsp;<a href='<%=ResolveUrl("../quickpost") %>'>Quick Post (Post Immediately)</a>
		  </td>
		  </tr>
          <tr>
		  <td style="line-height:22px;">
		  <img src='<%=ResolveUrl("../content/images/light_gray_arrow.png")%>' />&nbsp;<a href="<%=ResolveUrl("../scheduler")%>">Schedule Post</a>
		  </td>
		  </tr>
		  <tr>
		  <td style="line-height:22px;">
		  <img src='<%=ResolveUrl("../content/images/light_gray_arrow.png")%>' />&nbsp;<a href="<%=ResolveUrl("../drafts")%>">View Draft <asp:Literal ID="ltrdrafts" runat="server"></asp:Literal></a>
		  </td>
		  </tr>
		  <tr>
		  <td style="line-height:22px;">
		  <img src='<%=ResolveUrl("../content/images/light_gray_arrow.png")%>' />&nbsp;<a href="<%=ResolveUrl("../templates")%>">View Templates</a>
		  </td>
		  </tr>
		  <tr>
		  <td style="line-height:22px;">
		  <img src='<%=ResolveUrl("../content/images/light_gray_arrow.png")%>' />&nbsp;<a href="<%=ResolveUrl("../sent-messages")%>">View Sent Messages</a>
		  </td>
		  </tr>
          <tr>
		  <td style="line-height:22px;">
		  <img src='<%=ResolveUrl("../content/images/light_gray_arrow.png")%>' />&nbsp;<a href="<%=ResolveUrl("../pending-messages")%>">View Pending Messages</a>
		  </td>
		  </tr>
		  <%--<tr>
		  <td style="line-height:22px;">
		  <img src="content/images/light_gray_arrow.png" />&nbsp;<a href="scheduler/scheduledmessage">View Sheduled Messages</a>
		  </td>
		  </tr>--%>
		  </table>
        
	 </td>
    </tr>		
  </table>
</div>
<script language="javascript">
    $("#lnkPostNLeap").click(function () {
        $("#menuPostScheduler").slideDown("slow");
    });
    var url = window.location;
    var p = /.+\/([^\/]+)/;
    var match = p.exec(url)
    if (match[1] == 'create-post.aspx' || match[1] == 'scheduler.aspx' || match[1] == 'drafts.aspx' || match[1] == 'sent-messages.aspx' || match[1] == 'pending-messages.aspx' || match[1] == 'SaveAsTemplate' || match[1] == 'templates.aspx' || match[1] == 'quickpost.aspx') {
        $("#menuPostScheduler").slideDown("slow");
    }
 </script>