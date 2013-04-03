<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="left.ascx.vb" Inherits="tsma.headerleft" %>

<script language="javascript" type="text/javascript">
  	 function CreatePage() {
    window.open("https://www.facebook.com/pages/create.php?", "Page", "left=20,top=20,menubar=0,resizable=0,width=1000,height=850");
	}
	function SubMenu(obj) {
	    $('#hdnLinkval').val(obj);
	    var SaveData = $('#hdnSaveHeader').val();
	    if (SaveData == "1") {
	        $('#divsubmenu').slideDown();
	        return true;
	    }
	    else {
	        FacebookAlert();
	        return false;
	    }
	}
	function FacebookAlert() {
	    /*jConfirm('Warning: You have unsaved changes to your work you are editing.', '', 
	    function(r) {
	    $('#hdnSaveHeader').val('1');
	    return true;
	    });*/
	    //$("#spnMessage").html(mess);
	    var maskHeight = $(document).height();
	    var maskWidth = $(window).width();
	    $('#DivSaveBeforeExist').css({ 'width': maskWidth, 'height': maskHeight });
	    $("#DivSaveBeforeExist").show("slow");
	}
	function HideLinkAlert() {
	    $("#DivSaveBeforeExist").hide("slow");
	}
	function ChangedPage() {
	    //alert("test");
	    //alert($('#lnkMenu').attr('href'));
	    //alert($('#hdnLinkval').val());
	    window.parent.location = $('#hdnLinkval').val();
	    return true;
	}
	function HideLinkAlert1() {
	    $("#DivSaveBeforeExistCover").hide("slow");
	}
	function ChangedPage1() {
	    //alert("test");
	    //alert($('#lnkMenu').attr('href'));
	    //alert($('#hdnLinkval').val());
	    window.parent.location.href = 'create-cover-photo';
	    return true;
	}

	function OpenSubMenu() {
		$('#divsubmenustatic').slideDown();
	}
function OpenSubMenu2() {
    $('#divsubmenustatic2').slideDown();
}
function OpenSubMenu22(id)
{
	$('#divsubmenustatic'+ id +'').slideDown();
}

  </script>
   <input type="hidden" id="hdnLinkval" value="" />
 <div style="padding-top:15px; padding-bottom:15px;z-index:111111111">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
		<td>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
			<tr style="display:none">
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                   
                   <a href='#'  class="leftlink">Overview</a>
                    
             </td>
			</tr>
             <tr>
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                    
                   <a onclick="return OpenSubMenu();" href='javascript:;'  class="leftlink">Design</a>
                    
             </td>
			</tr>
            <tr>
                <td  colspan="2" align="left" valign="middle" style="padding-left:13px; padding-top:5px; ">
                <div id="divsubmenustatic" style="display:none;">
        
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                    <tr>
		                <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           		        <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="CreatePage();" href='javascript:;'>Setup a Page</a>
		                </td>
		            </tr>
                    <tr>
		                <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           		        <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("cover-photo")%>'>Cover Image</a>
		                </td>
		            </tr>
		            <asp:Literal ID="ltrMenu" runat="server"></asp:Literal>
                    <tr style="display:none">
		                <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           		        <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("custom-tab")%>'>Custom Tabs</a>
		                </td>
		            </tr>
					<tr id="trLegacy" runat="server" visible="false">
                    	<td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">   
                        <img src="../content/images/light_gray_arrow.png" />&nbsp;<a href="category.aspx?id=2"> Welcome Tabs</a>
                    	</td>
                    </tr>
                     
                    <tr id="trAmerifirst1" runat="server" visible="false">
                    	<td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">   
                        <img src="../content/images/light_gray_arrow.png" />&nbsp;<a href="category.aspx?id=2"> Welcome Tabs</a>
                    	</td>
                    </tr>
                    <tr id="trAmerifirst2" runat="server" visible="false">
                        <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">   
                        <img src="../content/images/light_gray_arrow.png" />&nbsp;<a href="category.aspx?id=4"> Contact Us Tabs</a>
                    	</td>
                    </tr>
                     <tr id="trBoresha" runat="server" visible="false">
                    	<td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">   
                        <img src="../content/images/light_gray_arrow.png" />&nbsp;<a href="category.aspx?id=2"> Welcome Tabs</a>
                    	</td>
                    </tr>
 	            </table>
                </div>
                 </td>
            </tr> 
            
            <tr>
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                   <a onclick="$('#divinvitesubmenu').show()" href='javascript:;'  class="leftlink">Invite</a>
             </td>
			</tr>
            

            <tr>
                <td  colspan="2" align="left" valign="middle" style="padding-left:13px; padding-top:5px; ">
                <div id="divinvitesubmenu" style="display:none;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                    <tr>
		                <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           		        <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("sweepstakes")%>'>Sweepstakes</a>
		                </td>
		            </tr>
                      <tr style="display:;">
		                <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           		        <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("sweepstake-main")%>'>Customize Sweepstakes</a>
		                </td>
		            </tr>
		            </table>
                </div>
                 </td>
            </tr>
            
            
             <tr>
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                   <a onclick="$('#divEnagageSubMenu').show()" href='javascript:;'  class="leftlink">Engage</a>
             </td>
			</tr>
            
            <tr>
                <td  colspan="2" align="left" valign="middle" style="padding-left:13px; padding-top:5px; ">
                <div id="divEnagageSubMenu" style="display:none;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                      <tr>
                        <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; vertical-align:middle; padding-left:5px;"><img border="0" src="../content/images/light_gray_arrow.png" align="absmiddle"/>&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("engage-tutorial")%>'>Quick Video Tutorial</a> </td>
                      </tr>           
                      <tr>
                        <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; vertical-align:middle; padding-left:5px;">
                        <img border="0" src="../content/images/light_gray_arrow.png" align="absmiddle"/>&nbsp; 
                        <a onclick="return SubMenu(this);" href='<%=ResolveUrl("scheduler-main")%>'>QuickPost & Scheduler</a> </td>
                      </tr>
                      <tr>
                        <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; vertical-align:middle; padding-left:5px;"><img border="0" src="../content/images/light_gray_arrow.png" align="absmiddle"/>&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("view-autopost")%>'>Autopost Admin View</a> </td>
                      </tr>                  
                      <tr>
                        <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; vertical-align:middle; padding-left:5px;"><img border="0" src="../content/images/light_gray_arrow.png" align="absmiddle"/>&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("configure-autopost")%>'>Set Daily Autopost</a> </td>
                      </tr>
    
                    </table>
                </div>
                 </td>
            </tr>
            
            
            
             <tr>
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                   <a onclick="$('#divConvertSubMenu').show()" href='javascript:;'  class="leftlink">Convert</a>
             </td>
			</tr>
            
            
              <tr>
                <td  colspan="2" align="left" valign="middle" style="padding-left:13px; padding-top:5px; ">
                <div id="divConvertSubMenu" style="display:none;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                     <tr>
                        <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; vertical-align:middle; padding-left:5px;"><img border="0" src="../content/images/light_gray_arrow.png" align="absmiddle"/>&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("lead-tutorial")%>'>Quick Video Tutorial</a> </td>
                      </tr>     
                    <tr>
		                <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           		        <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("view-all-leads")%>'> View All Leads</a>
		                </td>
		            </tr>
                    <tr>
		                <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           		        <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="return SubMenu(this);" href='<%=ResolveUrl("custom-tabs-leads-admin")%>'> Set Custom Tab Leads</a>
		                </td>
		            </tr>
                    <tr>
                        <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; vertical-align:middle; padding-left:5px;">
                        <img border="0" src="../content/images/light_gray_arrow.png" align="absmiddle"/>&nbsp; 
                        <a onclick="return SubMenu(this);" href='<%=ResolveUrl("sweepstakes-leads-admin")%>'>Set Sweepstakes Leads</a> 
                       </td>
                    </tr>
		            </table>
                </div>
                 </td>
            </tr>
            
            <tr>
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                    
                   <a onclick="return SubMenu(this);" href='<%=ResolveUrl("training")%>'  class="leftlink">Learning Center</a>
                    
             </td>
			</tr>
            
             <tr>
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                    
                   <a onclick="return SubMenu(this);" href='<%=ResolveUrl("support")%>'  class="leftlink">Support</a>
                    
             </td>
			</tr>
          
        </table> 
		<%--<asp:Repeater runat="server" ID="rptLeftMenu">
        <itemtemplate>
        	<table width="100%" border="0" cellpadding="0" cellspacing="0">
			<tr style="display:none;">
				<td width="10" align="left" valign="middle" class="leftline"><span style="line-height:22px;"><img src="../Content/images/arrow-gray.gif" width="4" height="7" style="margin-left:7px; margin-right:5px;" /></span></td>
				<td width="95%" height="40" align="left" valign="middle" class="leftline">
                    <input type="hidden" id="hdnid" runat="server" value='<%#Container.DataItem("mnu_id")%>' />
                   <a id="lnkMenu" onclick="return SubMenu(this);" href='<%#Container.DataItem("mnu_link")%>'  class="leftlink"><%#Container.DataItem("mnu_Name")%></a>
                    
             </td>
			</tr>
             <tr>
      <td  colspan="2" align="left" valign="middle" style="padding-left:13px; padding-top:5px; ">
	  <input type="hidden" id="hdnMap" value="0" />
     <div id="divsubmenu" style="display:none;">
      <asp:Repeater runat="server" ID="rptLeftSubMenu" DataSource='<%# GetSubMenu(DataBinder.Eval(Container.DataItem, "mnu_Id")) %>' >
        <itemtemplate>
         
          <table cellpadding="0" cellspacing="0" border="0" width="100%" >
		  <tr>
		  <td height="40" style="line-height:20px; font-size:12px; border-bottom:1px  solid #cccccc; padding-left:5px;">
           <input type="hidden" id="hdnpid" runat="server" value='<%#Container.DataItem("mnu_Pmnuid")%>' />
		  <img src="../content/images/light_gray_arrow.png" />&nbsp; <a onclick="return SubMenu(this);" href='<%#Container.DataItem("mnu_link")%>'><%#Container.DataItem("mnu_Name")%></a>
		  </td>
		  </tr>
		  </table>
          
        </itemtemplate>
        </asp:Repeater>
        </div>
	 </td>
    </tr>		
			</table>
          </itemtemplate>
        </asp:Repeater>--%>
		</td>
	</tr>
       <tr>
    	<td align="left" valign="top" style="padding-top:20px; padding-left:7px;">
        <a href="https://www.facebook.com/twitter/?setup=1" target="_blank"><img src="../Content/images/facebook_twitter.png" border="0" align="left" /></a>
        </td>
    </tr>       
    <%--
    <tr>
		  <td style="line-height:22px;">
		  <img src="content/images/light_gray_arrow.png" />&nbsp;<a href="<%=ResolveUrl("scheduler")%>">Schedule Post</a>
		  </td>
		  </tr>
		  <tr>
		  <td style="line-height:22px;">
		  <img src="content/images/light_gray_arrow.png" />&nbsp;<a href="<%=ResolveUrl("drafts")%>">View Drafts <asp:Literal ID="ltrdrafts" runat="server"></asp:Literal></a>
		  </td>
		  </tr>
		  <tr>
		  <td style="line-height:22px;">
		  <img src="content/images/light_gray_arrow.png" />&nbsp;<a href="<%=ResolveUrl("templates")%>">View Templates <asp:Literal ID="ltrtemplates" runat="server"></asp:Literal></a>
		  </td>
		  </tr>
		  <tr>
		  <td style="line-height:22px;">
		  <img src="content/images/light_gray_arrow.png" />&nbsp;<a href="<%=ResolveUrl("sent-messages")%>">View Sent Messages</a>
		  </td>
		  </tr>
          <tr>
		  <td style="line-height:22px;">
		  <img src="content/images/light_gray_arrow.png" />&nbsp;<a href="<%=ResolveUrl("pending-messages")%>">View Pending Messages <asp:Literal ID="ltrpending" runat="server"></asp:Literal></a>
		  </td>
		  </tr>
		 <tr>
		  <td style="line-height:22px;">
		  <img src="content/images/light_gray_arrow.png" />&nbsp;<a href="scheduler/scheduledmessage">View Sheduled Messages</a>
		  </td>
		  </tr>--%>
	
  </table>
</div>
<script language="javascript" type="text/javascript">
 //   $("#lnkPostNLeapStatic").click(function () {
//    alert('test');
//        if (hdnid.value == hdnpid.value) {
//            alert('test');
//            $("#menuPostScheduler1").slideDown("slow");
//        }
  //  });
    
//    $("#lnkPostNLeapStatic").click(function () {
//        if (hdnid.value == hdnpid.value) {
//            alert('test');
//            $("#menuPostScheduler1").slideDown("slow");
//        }
//    });
//    var url = window.location;
//    var p = /.+\/([^\/]+)/;
//    var match = p.exec(url)
//    if (match[1] == 'sidebar' || match[1] == 'custom-tab' || match[1] == 'sidebar-templates' || match[1] == 'create-sidebar' || match[1] == 'sent-messages' || match[1] == 'pending-messages' || match[1] == 'SaveAsTemplate' || match[1] == 'sweepstake' || match[1] == 'quickpost') {
//        $("#menuPostScheduler1").slideDown("slow");
//    }

 </script>

