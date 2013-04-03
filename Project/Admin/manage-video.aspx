<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="manage-video.aspx.vb" Inherits="tsma.manage_video" validaterequest="false"%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"   Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="left.ascx" TagName="left" TagPrefix="uc2" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>Total Social Media Application</title>
<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
-->
</style>
<%--<link href="../Content/css/admin-style.css" rel="stylesheet" type="text/css" />--%>
<link href="../Content/css/style-admin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript"  language="javascript" >

//    function selectVideoType(objRadio) {
//        if (objRadio.value == "1") {
//            document.getElementById("trUploadVideo").style.display = "";
//            document.getElementById("trEmbedVideo").style.display = "none";
//        }
//        else if (objRadio.value == "2") {
//            document.getElementById("trUploadVideo").style.display = "none";
//            document.getElementById("trEmbedVideo").style.display = "";
//        }
//    }

    function ValidateDetails() {
        var fields = "";

        if (DoTrim(document.getElementById("txtTitle").value).length == 0) {
            fields = fields + "\n-- Title --";
        }
        if (document.getElementById("cmbCatagory").value == 0) {
            fields = fields + "\n-- Select Category --";
        }

        if (document.getElementById('rdoUpload').checked == true) {
            if (DoTrim(document.getElementById("flVideo").value).length == 0 && document.getElementById("btnSave").value == 'Save') {
                fields = fields + "\n-- Upload Video --";
            }
        }
        else if (document.getElementById('rdoEmbed').checked == true) {
            if (DoTrim(document.getElementById("txtEmbedCode").value).length == 0) {
                fields = fields + "\n-- Embed Code --";
            }
        }

        if (fields != "") {
            fields = "Please fill in the following details:\n--------------------------------\n" + fields;
            alert(fields);
            return false;
        }
        else {
            return true;
        }
    }

    function SelectType(objlb) {
        if (objlb.value == "1") {
		for(i = 0; i < document.forms[0].elements.length; i++) 
					{
						elm = document.forms[0].elements[i];
						if(elm.id.indexOf("chkIndustry")>=0)
						{
							if (elm.type == "checkbox" ) 
							{
								elm.checked = false;
							}
						}
					}	
            document.getElementById("chkIndAll").checked = false;
			document.getElementById("trcomp").style.display = "";
            document.getElementById("trind").style.display = "none";
            document.getElementById("trind").value = "0"
            document.getElementById("drpIndustry").value = "0"
			
			
        }
        else if (objlb.value == "2") {
			for(i = 0; i < document.forms[0].elements.length; i++) 
						{
							elm = document.forms[0].elements[i];
							if(elm.id.indexOf("chkCompany")>=0)
							{
								if (elm.type == "checkbox" ) 
								{
									elm.checked = false;
								}
							}
				}		
			document.getElementById("chkAll").checked = false;
            document.getElementById("trind").style.display = "";
            document.getElementById("trcomp").style.display = "none";
            document.getElementById("trcomp").value = "0"
            document.getElementById("drpCompany").value = "0"
			
			
			
        }
        else if (objlb.value == "3") {
            document.getElementById("trind").style.display = "none";
            document.getElementById("trcomp").style.display = "none";
            document.getElementById("trcomp").value = "0"
            document.getElementById("trind").value = "0"
            document.getElementById("drpCompany").value = "0"
            document.getElementById("drpIndustry").value = "0"
        }
     }
	 
	 function checkallcomp(obj)
			{			
				if (document.getElementById(obj.id).checked==true)
				{
					for(i = 0; i < document.forms[0].elements.length; i++) 
					{
						elm = document.forms[0].elements[i];
						if(elm.id.indexOf("chkCompany")>=0)
						{
							if (elm.type == "checkbox" ) 
							{
								elm.checked = true;
							}
						}
					}		
				}				
				else
				{
					for(i = 0; i < document.forms[0].elements.length; i++) 
					{
						elm = document.forms[0].elements[i];
						if(elm.id.indexOf("chkCompany")>=0)
						{
							if (elm.type == "checkbox" ) 
							{
								elm.checked = false;
							}
						}
					}		
				}
			}


			function checkallind(obj)
			{			
				if (document.getElementById(obj.id).checked==true)
				{
					for(i = 0; i < document.forms[0].elements.length; i++) 
					{
						elm = document.forms[0].elements[i];
						if(elm.id.indexOf("chkIndustry")>=0)
						{
							if (elm.type == "checkbox" ) 
							{
								elm.checked = true;
							}
						}
					}		
				}				
				else
				{
					for(i = 0; i < document.forms[0].elements.length; i++) 
					{
						elm = document.forms[0].elements[i];
						if(elm.id.indexOf("chkIndustry")>=0)
						{
							if (elm.type == "checkbox" ) 
							{
								elm.checked = false;
							}
						}
					}		
				}
			}
		</script>
</head>
<body>
<iframe width="174"  height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js" src="calender/ipopeng.htm" scrolling="no" frameborder="0" style="visibility:visible; z-index:999; position:absolute; top:-500px;"></iframe> 
<form id="frm" name="frm" runat="server">
    <asp:ScriptManager id="objScriptManager" runat="server">
    </asp:ScriptManager>

<table width="1004" height="100%" border="0" cellpadding="0" cellspacing="0" align="center">
  <tr>
    <td height="10" valign="top"><uc1:header ID="Header1" runat="server" /></td>
  </tr>
  <tr>
   <td align="left" valign="top" bgcolor="#DAE0F3" style="background-repeat:repeat-x;">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="165" align="left" valign="top" style="padding:9px; "><uc2:left ID="Left1" runat="server" /></td>
        <td align="left" valign="top" style="padding:9px; padding-left:0px; padding-bottom:0px "><table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td align="left" valign="top" bgcolor="#FFFFFF" class="imgborder" style="padding:16px ">
				  
			 <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
            <tr>
              <td align="left" valign="top" class="tdborder1" style="padding:10px">

			<!--<asp:UpdatePanel ID="pnlMain1" runat="server"><ContentTemplate>-->
          <%--   <div id="divLoading" style="position:absolute;" >
					<asp:UpdateProgress ID="objUpdateProgress" runat="Server" DisplayAfter="0">
					<ProgressTemplate>
						<img src="../Content/adminimages/popuploading.png" style="border:0px;font-family:Tahoma;font-size:12px;" alt="Loading" />					</ProgressTemplate>
					</asp:UpdateProgress>
					</div>		--%>
<%--<asp:UpdatePanel ID="pnlMain1" runat="server"><ContentTemplate> --%>
			  <div style="height:100%;width:100%" >
               <div id="div1" style="position:absolute;" >
			<%--<asp:UpdateProgress ID="UpdateProgress1" runat="Server" DisplayAfter="0">
			<ProgressTemplate>
				<div class="loadingbg"><img src="../images/ajax-loader.gif" width="32" height="32" vspace="22" /><div class="loadingtext">Loading...</div></div>
			</ProgressTemplate>
			</asp:UpdateProgress>--%>
			</div>
			  <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                      	<tr valign="middle" align="center" bgcolor="#ffffff">
                                                            <td align="left" valign="middle" class="title">Add New Video</td>
                                                         </tr>
                                                        <tr valign="middle" align="center" bgcolor="#ffffff">
                                                            <td height="20" class="arial1b" style="color: red; font-weight: bold;"><asp:Literal ID="ltrMsg" runat="server"></asp:Literal></td>
                                                         </tr>
                                                        <tr>
                                                            <td align="center" class="tdborder">
                                                                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                                                                <tr >
                                                                    <td width="20%" height="25" align="right" >Select Company / Industry</td>
                                                                    <td height="25" align="left" valign="middle">
                                                                    <input type="radio" id="rdoComp" checked  runat="Server" name="CompInd" value="1" onChange="SelectType(this);"  />
                                                                        Company &nbsp;&nbsp;
                                                                        <input type="radio" id="rdoInd" runat="Server" name="CompInd" value="2" onChange="SelectType(this);" />
                                                                        Industry
                                                                   </td>
                                                                  </tr>
                                                                  <tr id="trcomp" runat="server">
                                                                    <td width="20%" height="25" align="right" >Company:&nbsp;<font color="#ff0000">*</font></td>
                                                                    <td height="25" align="left" valign="middle">
                                                                    <div style="float:left; padding-left:8px;">
                                                                    <input type="checkbox" id="chkAll" onClick="checkallcomp(this);" />&nbsp;<strong>Select All Company</strong></div><br/><br/>
                                                                    <div id="divCountry" runat="server"  style="vertical-align:top; overflow:auto; overflow -x:hidden;" align="left">
                                                                      <asp:DataList ItemStyle-Width="25%" ID="chkCompany" RepeatDirection="Vertical" RepeatColumns="4" runat="server" 
                                                                      autogeneratecolumns="False" Font-Size="8pt" Font-Name="Verdana" CellSpacing="0" CellPadding="0" ShowFooter="false" BorderWidth="0" BorderColor="#EAEAEA" Width="100%" GridLines="None">
                                                                            <itemstyle Height="21" BackColor="#FFFFFF" HorizontalAlign="left"   ></itemstyle>
                                                                            <itemtemplate>
                                                                              <table border="0" width="98%" cellspacing="0" height="100%" cellpadding="1" bgcolor="#f6f6f6">
                                                                                <tr>
                                                                                  <td width="25" align="center" style="padding-left:5px;">
                                                                                  <input type="checkbox" id="chkSelected" comid='<%#Eval("c_Id")%>' runat="server"></td>
                                                                                  <td style="padding-left:5px;" ><%#Container.DataItem("c_Name")%></td>
                                                                                </tr>
                                                                              </table>
                                                                            </itemtemplate>
                                                                        </asp:DataList>
                                                                   </div>
                                                                    <%--<select name="drpCompany" runat="server" class="input" id="drpCompany">
                                                                    <option value="0" selected >-- Select Company --</option>
                                                                    </select>--%>
                                                                   
                                                                   </td>
                                                                  </tr>
                                                                   <tr id="trind" runat="server" style=" display:none">
                                                                    <td width="20%" height="25" align="right" >Industry:&nbsp; <font color="#ff0000">*</font></td>
                                                                    <td height="25" align="left" valign="middle">
                                                                    <div style="float:left; padding-left:8px;"><input type="checkbox" id="chkIndAll" onClick="checkallind(this);" />&nbsp;<strong>Select All Industry</strong></div><br/><br/>
                                                                    <div id="divIndustry" runat="server"  style="vertical-align:top; overflow:auto; overflow -x:hidden;" align="left">
                                                                       <asp:DataList ItemStyle-Width="25%" ID="chkIndustry" RepeatDirection="Vertical" RepeatColumns="4" runat="server" 
                                                                      autogeneratecolumns="False" Font-Size="8pt" Font-Name="Verdana" CellSpacing="0" CellPadding="0" ShowFooter="false" BorderWidth="0" BorderColor="#EAEAEA" Width="100%" GridLines="None">
                                                                            <itemstyle Height="21" BackColor="#FFFFFF" HorizontalAlign="left"   ></itemstyle>
                                                                            <itemtemplate>
                                                                              <table border="0" width="98%" cellspacing="0" height="100%" cellpadding="1" bgcolor="#f6f6f6">
                                                                                <tr>
                                                                                  <td width="25" align="center" style="padding-left:5px;">
                                                                                  <input type="checkbox" id="chkSelected" indid='<%#Eval("i_Id")%>' runat="server"></td>
                                                                                  <td style="padding-left:5px;" ><%#Container.DataItem("i_Name")%></td>
                                                                                </tr>
                                                                              </table>
                                                                            </itemtemplate>
                                                                        </asp:DataList>
                                                                   </div>
                                                                    <%--<select name="drpIndustry" runat="server" class="input" id="drpIndustry">
                                                                    <option value="0" selected>-- Select Industry --</option>
                                                                    </select>--%>
                                                                    
                                                                   </td>
                                                                  </tr>
                                                                    <tr>
                                                                        <td width="37%" height="25" align="right">
                                                                            Catagory:&nbsp;</td>
                                                                        <td>
                                                                            <select name="cmbCatagory" class="input" id="cmbCatagory" runat="server" style="width:175px">
                                                                            </select>   <font color="#ff0000">*</font>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="37%" height="25" align="right">
                                                                            Title:&nbsp;</td>
                                                                        <td width="63%" height="25" align="left">
                                                                            <input name="txtTitle" runat="server" type="text" class="input" id="txtTitle" size="48"
                                                                                maxlength="100">   <font color="#ff0000">*</font>
                                                                            </td>
                                                                    </tr>
																	<tr >
                                                                       <%-- <td height="25" align="right">
                                                                            Video Type:&nbsp;</td>--%>
                                                                        <%--<td height="25" align="left">
                                                                            <input type="radio" id="rdoUpload" runat="Server"  value="1" name="video" onClick="selectVideoType(this);" />
                                                                            Upload Video&nbsp;&nbsp;
                                                                            <input type="radio" id="rdoEmbed" checked runat="Server" value="2" name="video" onClick="selectVideoType(this);" />
                                                                            Embed Video </td>--%>
                                                                    </tr> 
                                                                   <%-- <tr id="trUploadVideo" runat="server" style="display:none;">
                                                                        <td align="right" valign="top">
                                                                            Upload Video:&nbsp;</td>
                                                                        <td align="left" valign="top"> 
																			<input type="file" id="flVideo" class="input" runat="server" >   <font color="#ff0000">*</font>
																		</td>
                                                                    </tr>--%>
																	<tr id="trEmbedVideo" runat="server" >
                                                                      <td align="right" valign="top" >Embed Video:&nbsp;</td>
                                                                      <td align="left" valign="top"> 
																		<textarea id="txtEmbedCode" runat="server" class="input" style="width:280px;height:120px " ></textarea>   <font color="#ff0000">*</font></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="25" align="right">
                                                                            Status:&nbsp;</td>
                                                                        <td height="25" align="left">
                                                                            <input type="radio" id="rdoActive"  runat="Server" name="ActiveInactive" value="1" />
                                                                            Active &nbsp;&nbsp;
                                                                            <input type="radio" id="rdoInactive" runat="Server" name="ActiveInactive" value="0" />
                                                                            Inactive
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="25" align="right">&nbsp;</td>
                                                                        <td height="30" align="left">
                                                                            <input runat="server" type="submit" class="AdminFormButton" id="btnSave" value="Save" OnClick="return ValidateDetails();"/>&nbsp;&nbsp;
                                                                            <input runat="server" type="submit" class="AdminFormButton" id="btnReset" value="Reset" />
																		</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" height="25">&nbsp;
                                                                </td>
                                                        </tr>
                                                        <asp:Panel ID="pnldata" runat="server">
                                                            <tr>
                                                                <td height="25" valign="middle" class="title" colspan="2">
                                                                    <span id="spnpagetitle1" runat="server">List of Videos</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="25" colspan="2" align="right" valign="top">
                                                                    <a href="manage-video.aspx"><b> Add New Video</b></a></td>
                                                            </tr>
                                                            <tr>
                	                                            <td align="right" height="23" ><strong><asp:PlaceHolder ID="phPagingTop" runat="server"></asp:PlaceHolder></strong></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2" style="border: 1px solid #E0E0E0; padding: 1px;">
                                                                   <asp:GridView ID="dgrVideo" runat="server" AutoGenerateColumns="False" PagerStyle-HorizontalAlign="Right"
                                                                        Font-Size="8pt" Font-Name="Verdana" CellSpacing="0" CellPadding="5" ShowFooter="false"
                                                                        PageSize="30" BorderWidth="0" BorderColor="#EAEAEA" AllowPaging="true"  Width="100%"
                                                                        GridLines="None" AllowSorting="true" >
																		     <HeaderStyle Font-Bold="True"  HorizontalAlign="left"  CssClass="GridTop" ForeColor="#FFFFFF" Height="25"></HeaderStyle>
																		   <AlternatingrowStyle BackColor="#F7F7F9"></AlternatingrowStyle>                                                                    
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Title" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%#DataBinder.Eval(Container.DataItem, "vd_Title")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Catagory" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <%#DataBinder.Eval(Container.DataItem, "vcat_Name")%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="Video" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                                                                                HeaderStyle-Width="100">
                                                                               <ItemTemplate>
																					<span id="spnVideoEmbed" runat="server" width="100" height="100" style="width:100px; height:100px;"><%#DataBinder.Eval(Container.DataItem, "vd_EMbededCode")%></span>
                                                                                </ItemTemplate>

                                                                            </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                                                                                HeaderStyle-Width="50">
                                                                                <ItemTemplate>
                                                                                   <asp:ImageButton OnCommand="ChangeStatus" Runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "vd_isActive")%>' CommandName=<%#DataBinder.Eval(Container.DataItem, "vd_id")%>    BorderWidth=0  ImageUrl='<%# Container.DataItem("ImageSrc")%>' ID="Imagebutton2" ></asp:Imagebutton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                                                                                HeaderStyle-Width="50">
                                                                                <ItemTemplate>
                                                                                    <a href='manage-video.aspx?id=<%#Container.DataItem("vd_id") %>'  Title="Edit" ><img src="../Content/images/ico-edit.gif" border="0"></a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="50" HeaderStyle-HorizontalAlign="center"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Button  OnCommand="DeleteVideoById" CssClass="AdminFormButton" runat="server" ID="imgDel1" onclientclick="return confirm('Are you sure want to delete this video?');" Text="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "vd_Id")%>' ToolTip="Delete" CommandName='<%#DataBinder.Eval(Container.DataItem, "vd_Id")%>'></asp:Button>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                                                                                                  
                                                                        </Columns>
                                                                        <AlternatingRowStyle BackColor="#F5F5F5"></AlternatingRowStyle>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                	                                            <td align="right" height="23" colspan="2"><strong><asp:PlaceHolder ID="phPagingBottom" runat="server"></asp:PlaceHolder></strong></td>
                                                            </tr>
                                                           <tr >
                                                                <td colspan="2" height="25" align="left" valign="middle">
                                                                    Note:
                                                                   <img src="../Content/images/ico-active.gif" align="absmiddle">&nbsp;denotes enabled video and&nbsp;
                                                                    <img src="../Content/images/ico-inactive.gif" align="absmiddle">
                                                                    denotes disabled video.
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                      
                                                    </table>
			  </div>
			</ContentTemplate>
			<Triggers>
			<%--<asp:PostBackTrigger ControlID="btnDownload" />--%>
			</Triggers>
			</asp:UpdatePanel>
            
						  </td>
            </tr>
          </table>
			  </td>
            </tr>
        </table></td>
      </tr>
      <tr align="center" valign="middle">
        <td colspan="2" valign="top" height="25"><uc3:footer ID="Footer1" runat="server" /></td>
        </tr>
    </table>
    </td>
  </tr>
</table>
</form>
</body>
</html>