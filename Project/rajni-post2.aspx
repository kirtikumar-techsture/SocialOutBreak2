<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rajni-post2.aspx.vb" Inherits="tsma.rajni_post2" %>
<%@ Register src="left.ascx" tagname="left" tagprefix="uc3" %>
<%@ Register src="inner-header.ascx" tagname="inner1" tagprefix="uc1" %>
<%@ Register src="inner-footer.ascx" tagname="inner" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Total Social Media Application</title>
<link href="Content/css/inner.css" rel="stylesheet" type="text/css" />
<script src="content/js/jquery-1.7.2.min.js" type="text/javascript"></script>
<script src="content/js/scheduler-new.js" type="text/javascript"></script>
</head>
<body>
<form id="frm" runat="server">
  <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
  <div id="innerpagepagemain"  >
    <uc1:inner1 ID="inner1" runat="server" />
    <div id="centermain">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="170" align="left" valign="top" class="leftbg"><uc3:left ID="left1" runat="server" />
          </td>
          <td align="left" valign="top" class="contentbody">
		  <!-- Quick Posting Start here -->
		  <table cellpadding="0" cellspacing="0" border="0" width="100%">
              <tr>
                <td height="50"><h6>Facebook Postings Master</h6></td>
              </tr>
              <tr>
                <td valign="bottom">
				<!-- Top Menu Start -->
				<div style="float:left;">
				<span class="compose_msg" id="StatusMessage">
				<a href="javascript:;" class="graylink" onclick="QuickPost('1');">
				<img src="../Content/images/compose_message.gif" width="16" height="15" align="absmiddle" />
				&nbsp;<strong>Compose Message</strong></a>
				</span> 
				&nbsp;&nbsp;&nbsp;<font color="#7d869d">|</font>&nbsp;&nbsp;&nbsp;
				<span class="photo_video">
				<a href="javascript:;" class="graylink" onclick="QuickPost('2');">
				<img src="../Content/images/add_photo_video.gif" width="16" height="14" align="absmiddle" />
				&nbsp;<strong>Add Photo &nbsp;</strong></a>
				</span>
				<font color="#7d869d">|</font>&nbsp;&nbsp;&nbsp;
				<span class="link_video">
				<a href="javascript:;" class="graylink" onclick="QuickPost('3');">
				<img src="../Content/images/video_icon1.gif" align="absmiddle" />&nbsp;
				<strong>Add Link&nbsp;</strong></a></span>
				&nbsp;&nbsp;<font color="#7d869d">|</font>&nbsp;&nbsp;&nbsp; 
				<span class="fans_showhide1">
				<a href="javascript:;" class="graylink" onclick="QuickPost('4');">
				<img src="../Content/images/selectfanpage_icon1.gif" align="absmiddle" />
				&nbsp;&nbsp;<strong>Select Business Pages</strong></a></span>
				</div>
				<div align="right" style="padding-bottom:5px;"><a href="javascript:;" class="bluetablink" onclick="ClearPost();" >Clear Post</a></div>
				<!-- Top Menu End -->
				<input type="hidden" id="hdnselectedPages" runat="server" name="hdnselectedPages" value="" />
				<input type="hidden" id="hdnSelectedPagesName" runat="server" name="hdnSelectedPagesName" value="" />
				<input type="hidden" id="hdnSelectedPagesImage" runat="server" name="hdnSelectedPagesImage" value="" />
				<input type="hidden" id="hdnselectedPagesAccessToken" runat="server" name="hdnselectedPagesAccessToken" value="" />
				</td>
              </tr>
			  <tr>
			  <td>
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
				  <tr>
					<td align="left" valign="top" class="popupbordertop" id="topimage" style="padding-left:70px;">
					<img src="../Content/images/popup_border_top.gif" width="15" height="6" /></td>
				  </tr>
				  <tr>
					<td align="left" valign="top" class="popupborder" style="padding:10px;">
						<!-- Message Start -->
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
						  <tr>
						  <td>
						  Enter Message
						  </td>
						  </tr>
						<tr>
						  <td align="left" valign="top" style="padding:5px; font-family:arial;border:1px solid #999999;">
						  <textarea style="width: 765px; border:0px" id="txtMessage" runat="server" cols="80" rows="4" name="txtMessage" class="menu_showhide12" ></textarea></td>
						</tr>
						<tr id="dvLoading" style="display:none;">
							<td align="left" valign="bottom" style="padding-left:5px; padding-top:5px;">
							<img src="content/images/loading-fb.gif" />
							</td>
						  </tr>
					  </table>
					  <!-- Message End -->
					  <!-- Photo Start -->
					  <div id="tblPhoto" style="display:none;">
					  <table width="100%" border="0" cellspacing="0" cellpadding="0">
						  <tr>
						  <td style="padding-top:10px;">
						  Select Photo<br />
						  </td>
						  </tr>
						  <tr>
							<td align="left" style="padding:5px; font-family:arial;border:1px solid #999999;">							
							<strong>Select an image file on your computer.</strong><br />
							<ajaxToolkit:AsyncFileUpload runat="server" OnClientUploadError="uploadError" OnClientUploadComplete="uploadComplete" ID="AsyncFileUpload1" AllowedFileTypes="jpg,jpeg,gif,png" Width="400px" UploaderStyle="Modern" UploadingBackColor="#CCFFFF" ThrobberID="myThrobber">
							</ajaxToolkit:AsyncFileUpload><br />
							<asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="content/images/loading-fb.gif"/></asp:Label>
							  &nbsp;&nbsp;<a href="javascript:;" id="lnkFileRemove" style="display:none; padding-left:5px;" onclick="RemoveFileUploadImage();">Remove</a>
							  <div style="padding-left:10px;"><img id="imgLoading" src="Content/images/uploading.gif" style="display:none" /></div></td>
							<td align="left" style="width:150px;" valign="middle">
							<img width="66" height="66" id="imgPhoto" style="margin:5px;" src="Content/images/no_img.jpg" runat="server"/>
							  <asp:Label ID="lblVideoText" runat="server"></asp:Label>
							  <input type="hidden" id="hdnFBUserId" runat="server" />
							  <input type="hidden" id="hdnImageUrl" runat="server" />
							</td>
						  </tr>
					  </table>
					  </div>
					  <!-- Photo End -->
					 <!-- Video Link Start -->
					 <div id="tblVideo" style="display:none;">
					 <table width="100%" border="0" cellspacing="0" cellpadding="0">
					 <tr>
						  <td style="padding-top:10px;">
						  Select Youtube URL<br />
						  </td>
						  </tr>
						  <tr>
							<td align="left" style="padding:5px; font-family:arial;border:1px solid #999999;">							
							<strong>Enter Youtube URL.</strong><br />
							  <input type="text" id="txtvideo" runat="server" width="720" style="width:320px; " onchange="CheckVideo();" onkeydown="return FocusOnEnter(event)"/>
							  (e.g: www.youtube.com/watch?v=yoGYjtCo350)&nbsp;&nbsp;
							  <input type="hidden" id="hdnVideoId" value="" />
							  <input type="hidden" id="hdnVideoImage" value="" />
							 </td>
							<td align="left" style="width:150px;" valign="middle">
							<img width="66" height="66" id="imgThumbnail" style="margin:5px;" src="Content/images/no_img.jpg" runat="server"/>
							</td>
						  </tr>
						</table>
						</div>
						<!-- Video Link End -->											  
					 <!-- Business Pages Start -->
					 <div id="tbBusinessPage" style="display:none;">
					  <table width="100%" border="0" cellspacing="0" cellpadding="0">
						  <tr>
						  <td style="padding-top:10px;">
						  Select Business Page<br />
						  </td>
						  </tr>
						  <tr>
							<td align="left" style="font-family:arial;">
							<div style="overflow:auto; padding:10px; height:115px; margin-top:5px;border:1px solid #999999;">
							<asp:PlaceHolder ID="plcData" runat="server">
								<asp:DataList ID="dstFanPages" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
								  <ItemTemplate>
									<table width="252" border="0" cellspacing="0" cellpadding="0">
									  <tr>
										<td colspan="2" height="4"></td>
									  </tr>
									  <tr>
										<td width="48" align="left" valign="middle" style="background-color:#f6f6f6; " ><table border="0" cellspacing="0" cellpadding="0">
											<tr>
											  <td align="center" valign="middle"><img src='<%#Eval("picture")%>' width="40" style=" border: 7px solid #f6f6f6" height="40" align="absmiddle" group="pageimg" pageid='<%#Eval("Id")%>' /> </td>
											  <td align="center" valign="middle">
											  <table border="0" width="170" cellspacing="0" cellpadding="0">
												  <tr>
													<td width="25" align="left" valign="middle" style="text-align:center; vertical-align:middle">
													<input class="checkboxpadding" type="checkbox" id='chkPage_<%#Eval("Id")%>' name="chkPage" pageid='<%#Eval("Id")%>' group="pages" onclick='PageidScheduler(this);selectedpagesName();'  pageaccess_token='<%#Eval("access_token")%>' pagevalue='<%#Eval("name")%>' pageimage='<%#Eval("picture")%>' /></td>
													<td align="left" width="150" valign="middle"><%#Eval("name")%>
													  <input type="hidden" id='hdnPageId_<%#Eval("Id")%>' value='<%#Eval("Id")%>' />
													  <input type="hidden" id='hdnPageName_<%#Eval("Id")%>' value='<%#Eval("name")%>' />
													  <input type="hidden" id='hdnAccessToken_<%#Eval("Id")%>' value='<%#Eval("access_token")%>' />
													  <input type="hidden" id='hdnImage_<%#Eval("Id")%>' value='<%#Eval("picture")%>' />
													</td>
												  </tr>
												</table></td>
											</tr>
										  </table></td>
										<td width="4" align="left"></td>
									  </tr>
									</table>
								  </ItemTemplate>
								</asp:DataList>
							  </asp:PlaceHolder>
							  <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false"> <strong style="color:#990066"> You have no business pages.</strong><br />
								<br />
								<a href="javascript:CreatePage();">Click here</a> to create business page. </asp:PlaceHolder>
							</div>
							</td>
						  </tr>
						</table>
						</div>
					 <!-- Business Pages End -->
					 </td>
				  </tr>
				</table>				  
			  </td>
			  </tr>
			  <tr>
			  <td class="popupinnerbg" style="border:1px solid #b4bbcd;  border-top:0px; ">
			  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="30%" height="31" align="left" valign="middle"></td>
                      <td width="70%" align="right" valign="middle" style="padding-right:5px;">
					  <a href="javascript:;" class="bluetablink" runat="server" onclick="return ValidateDrafts();" id="lnkSaveDraft">
					  <span id="spnSaveToDraft">Save as Draft</span>
					  </a>&nbsp;&nbsp;
					  <a href="javascript:;" class="bluetablink" onClick="SelectLibCat();" id="lnkSaveToMyLib">Save in My Library</a>&nbsp;&nbsp;
					  <a href="javascript:;" runat="server" class="bluetablink" onclick="return validateQuickPost();" id="lnkPost"><span id="spnQuickPost">Quick Post</span></a>&nbsp;&nbsp;
					  <a href="javascript:;" class="bluetablink" onClick="SelectScheduleDiv();" id="lnkScheduleDiv">Schedule Post</a>
					  <div id="divLibCat" style="display:none; height:auto;" class="boxClass">
                          <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td align="right" valign="top" style="height:10px; padding-right:5px;">
							  <a href="javascript:;" class="lib_close" onclick="SelectLibCat();"><strong>x</strong></a></td>
                            </tr>
                            <tr>
                              <td align="left" valign="top" style="padding-left:5px;padding-right:5px;">
							  <span id="spnLibFolders" />
							  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <asp:Repeater id="rptLibUserCatList" runat="server">
                                      <itemtemplate>
                                        <tr>
                                          <td height="18" onmouseover="FolderOver(this,0);" style=" cursor:pointer;border:1px solid #ffffff; padding:5px;" onmouseout="FolderOver(this,1);">
										  <a href="javascript:;" onclick="return ValidateLib()" id="lnkLibCatSel"><%#Eval("Title").replace("{%Selection%}", strSelectionType)%></a>
										  </td>
                                        </tr>
                                      </itemtemplate>
                                    </asp:Repeater>
                                    <tr id="trNew">
                                      <td height="18" onmouseover="FolderOver(this,0);" style=" cursor:pointer;border:1px solid #ffffff; padding:5px;" onmouseout="FolderOver(this,1);" onClick="CreateNewLibCat();">Create New</td>
                                    </tr>
                                    <tr id="trCreateNew" style="display:none">
                                      <td height="18" onmouseover="FolderOver(this,0);" style=" cursor:pointer;border:1px solid #ffffff; padding:5px;" onmouseout="FolderOver(this,1);" onClick="CreateNewLibCat();">
									  <input type="text" id="txtNewLibCat" maxlength="20" runat="server" width="130" style="width:110px; height:15px;" />&nbsp;<img src="content/images/save-ico.png" style="cursor:pointer;" id="ibtnFolderSave" onclick="saveMyLibrary()" border="0" align="absmiddle" />                                        
                                      </td>
                                    </tr>
                                </table></td>
                            </tr>
                          </table>
                        </div>
                        <div id="divSchedule" width="350" align="left" valign="top"  style="display:none; z-index:100; position:absolute; border:1px solid #cccccc; padding:10px; background-color:#f7f7f7; width:350px;">
                          <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td align="right" valign="top"><a href="javascript:;" class="fan_close"><img src='Content/images/delete_icon.png' width="12" height="12" hspace="7" vspace="7" border="0"/></a></td>
                            </tr>
                            <tr>
                              <td align="left" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td width="170"><input type="text" class="input" id="txtActivationDate" name="txtActivationDate"
                                                                                    runat="server" size="30" style="border:1px solid #CCC; font-family:Arial; font-size:12px; color:#666; height:20px; padding:2px; width:160px;"
                                                                                    maxlength="25">
                                          </td>
                                          <td>&nbsp; <img src="content/images/calender_icon.png" onClick="OpenCal('txtActivationDate');" width="22" height="23" align="absmiddle" style="cursor: pointer;"/>&nbsp;&nbsp;&nbsp;(mm/dd/yyyy)&nbsp;<font color="#FF0000">*</font></td>
                                        </tr>
                                      </table></td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td align="left" valign="top"><label>
                                            <select id="selActivationHour" name="selActivationHour" runat="server" style="border:1px solid #CCC; font-family:Arial; font-size:12px; color:#666; height:24px; padding:2px; width:85px;">
                                              <option value="0">Hour</option>
                                              <option value="1">1</option>
                                              <option value="2">2</option>
                                              <option value="3">3</option>
                                              <option value="4">4</option>
                                              <option value="5">5</option>
                                              <option value="6">6</option>
                                              <option value="7">7</option>
                                              <option value="8">8</option>
                                              <option value="9">9</option>
                                              <option value="10">10</option>
                                              <option value="11">11</option>
                                              <option value="12">12</option>
                                            </select>
                                            &nbsp;<font color="#FF0000">*</font> </label></td>
                                          <td align="left" valign="top"><select id="selActivationMinute" name="selActivationMinute" style="border:1px solid #CCC; font-family:Arial; font-size:12px; color:#666; height:24px; padding:2px; width:85px;" runat="server">
                                              <option value="Minute">Minute</option>
                                              <option value="00">00</option>
                                              <option value="15">15</option>
                                              <option value="30">30</option>
                                              <option value="45">45</option>
                                            </select>
                                            &nbsp;<font color="#FF0000">*</font> </td>
                                          <td align="left" valign="top"><select id="selAMPM" name="selAMPM" runat="server" style="border:1px solid #CCC; font-family:Arial; font-size:12px; color:#666; height:24px; padding:2px; width:85px;">
                                              <option value="0">AM/PM</option>
                                              <option value="AM">AM</option>
                                              <option value="PM">PM</option>
                                            </select>
                                            &nbsp;<font color="#FF0000">*</font> </td>
                                        </tr>
                                      </table></td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top"><asp:DropDownList  ID="ddlTimeZone" runat="server" style="border:1px solid #CCC; font-family:Arial; font-size:12px; color:#666; height:24px; padding:2px; width:291px;">
                                        <asp:ListItem Value="0"> Select a TimeZone </asp:ListItem>
                                        <asp:ListItem Value="Eastern Standard Time">Eastern Timezone (UTC -5.00)</asp:ListItem>
                                        <asp:ListItem Value="Central Standard Time">Central Timezone (UTC -6.00)</asp:ListItem>
                                        <asp:ListItem Value="Mountain Standard Time">Mountain Timezone (UTC -7.00)</asp:ListItem>
                                        <asp:ListItem Value="Pacific Standard Time">Pacific Timezone (UTC -8.00)</asp:ListItem>
                                        <asp:ListItem value="Dateline Standard Time">(UTC-12:00) International Date Line West</asp:ListItem>
                                        <asp:ListItem value="Samoa Standard Time">(UTC-11:00) Midway Island, Samoa</asp:ListItem>
                                        <asp:ListItem value="Hawaiian Standard Time">(UTC-10:00) Hawaii</asp:ListItem>
                                        <asp:ListItem value="Alaskan Standard Time">(UTC-09:00) Alaska</asp:ListItem>
                                        <asp:ListItem value="Pacific Standard Time (Mexico)">(UTC-08:00) Tijuana, Baja California</asp:ListItem>
                                        <asp:ListItem value="US Mountain Standard Time">(UTC-07:00) Arizona</asp:ListItem>
                                        <asp:ListItem value="Mountain Standard Time (Mexico)">(UTC-07:00) Chihuahua, La Paz, Mazatlan</asp:ListItem>
                                        <asp:ListItem value="Central America Standard Time">(UTC-06:00) Central America</asp:ListItem>
                                        <asp:ListItem value="Central Standard Time (Mexico)">(UTC-06:00) Guadalajara, Mexico City, Monterrey</asp:ListItem>
                                        <asp:ListItem value="Canada Central Standard Time">(UTC-06:00) Saskatchewan</asp:ListItem>
                                        <asp:ListItem value="SA Pacific Standard Time">(UTC-05:00) Bogota, Lima, Quito</asp:ListItem>
                                        <asp:ListItem value="US Eastern Standard Time">(UTC-05:00) Indiana (East)</asp:ListItem>
                                        <asp:ListItem value="Venezuela Standard Time">(UTC-04:30) Caracas</asp:ListItem>
                                        <asp:ListItem value="Paraguay Standard Time">(UTC-04:00) Asuncion</asp:ListItem>
                                        <asp:ListItem value="Atlantic Standard Time">(UTC-04:00) Atlantic Time (Canada)</asp:ListItem>
                                        <asp:ListItem value="SA Western Standard Time">(UTC-04:00) Georgetown, La Paz, San Juan</asp:ListItem>
                                        <asp:ListItem value="Central Brazilian Standard Time">(UTC-04:00) Manaus</asp:ListItem>
                                        <asp:ListItem value="Pacific SA Standard Time">(UTC-04:00) Santiago</asp:ListItem>
                                        <asp:ListItem value="Newfoundland Standard Time">(UTC-03:30) Newfoundland</asp:ListItem>
                                        <asp:ListItem value="E. South America Standard Time">(UTC-03:00) Brasilia</asp:ListItem>
                                        <asp:ListItem value="Argentina Standard Time">(UTC-03:00) Buenos Aires</asp:ListItem>
                                        <asp:ListItem value="SA Eastern Standard Time">(UTC-03:00) Cayenne</asp:ListItem>
                                        <asp:ListItem value="Greenland Standard Time">(UTC-03:00) Greenland</asp:ListItem>
                                        <asp:ListItem value="Montevideo Standard Time">(UTC-03:00) Montevideo</asp:ListItem>
                                        <asp:ListItem value="Mid-Atlantic Standard Time">(UTC-02:00) Mid-Atlantic</asp:ListItem>
                                        <asp:ListItem value="Azores Standard Time">(UTC-01:00) Azores</asp:ListItem>
                                        <asp:ListItem value="Cape Verde Standard Time">(UTC-01:00) Cape Verde Is.</asp:ListItem>
                                        <asp:ListItem value="Morocco Standard Time">(UTC) Casablanca</asp:ListItem>
                                        <asp:ListItem value="UTC">(UTC) Coordinated Universal Time</asp:ListItem>
                                        <asp:ListItem value="GMT Standard Time">(UTC) Dublin, Edinburgh, Lisbon, London</asp:ListItem>
                                        <asp:ListItem value="Greenwich Standard Time">(UTC) Monrovia, Reykjavik</asp:ListItem>
                                        <asp:ListItem value="W. Europe Standard Time">(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna</asp:ListItem>
                                        <asp:ListItem value="Central Europe Standard Time">(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague</asp:ListItem>
                                        <asp:ListItem value="Romance Standard Time">(UTC+01:00) Brussels, Copenhagen, Madrid, Paris</asp:ListItem>
                                        <asp:ListItem value="Central European Standard Time">(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb</asp:ListItem>
                                        <asp:ListItem value="W. Central Africa Standard Time">(UTC+01:00) West Central Africa</asp:ListItem>
                                        <asp:ListItem value="Jordan Standard Time">(UTC+02:00) Amman</asp:ListItem>
                                        <asp:ListItem value="GTB Standard Time">(UTC+02:00) Athens, Bucharest, Istanbul</asp:ListItem>
                                        <asp:ListItem value="Middle East Standard Time">(UTC+02:00) Beirut</asp:ListItem>
                                        <asp:ListItem value="Egypt Standard Time">(UTC+02:00) Cairo</asp:ListItem>
                                        <asp:ListItem value="South Africa Standard Time">(UTC+02:00) Harare, Pretoria</asp:ListItem>
                                        <asp:ListItem value="FLE Standard Time">(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius</asp:ListItem>
                                        <asp:ListItem value="Israel Standard Time">(UTC+02:00) Jerusalem</asp:ListItem>
                                        <asp:ListItem value="E. Europe Standard Time">(UTC+02:00) Minsk</asp:ListItem>
                                        <asp:ListItem value="Namibia Standard Time">(UTC+02:00) Windhoek</asp:ListItem>
                                        <asp:ListItem value="Arabic Standard Time">(UTC+03:00) Baghdad</asp:ListItem>
                                        <asp:ListItem value="Arab Standard Time">(UTC+03:00) Kuwait, Riyadh</asp:ListItem>
                                        <asp:ListItem value="Russian Standard Time">(UTC+03:00) Moscow, St. Petersburg, Volgograd</asp:ListItem>
                                        <asp:ListItem value="E. Africa Standard Time">(UTC+03:00) Nairobi</asp:ListItem>
                                        <asp:ListItem value="Georgian Standard Time">(UTC+03:00) Tbilisi</asp:ListItem>
                                        <asp:ListItem value="Iran Standard Time">(UTC+03:30) Tehran</asp:ListItem>
                                        <asp:ListItem value="Arabian Standard Time">(UTC+04:00) Abu Dhabi, Muscat</asp:ListItem>
                                        <asp:ListItem value="Azerbaijan Standard Time">(UTC+04:00) Baku</asp:ListItem>
                                        <asp:ListItem value="Mauritius Standard Time">(UTC+04:00) Port Louis</asp:ListItem>
                                        <asp:ListItem value="Caucasus Standard Time">(UTC+04:00) Yerevan</asp:ListItem>
                                        <asp:ListItem value="Afghanistan Standard Time">(UTC+04:30) Kabul</asp:ListItem>
                                        <asp:ListItem value="Ekaterinburg Standard Time">(UTC+05:00) Ekaterinburg</asp:ListItem>
                                        <asp:ListItem value="Pakistan Standard Time">(UTC+05:00) Islamabad, Karachi</asp:ListItem>
                                        <asp:ListItem value="West Asia Standard Time">(UTC+05:00) Tashkent</asp:ListItem>
                                        <asp:ListItem value="India Standard Time">(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi</asp:ListItem>
                                        <asp:ListItem value="Sri Lanka Standard Time">(UTC+05:30) Sri Jayawardenepura</asp:ListItem>
                                        <asp:ListItem value="Nepal Standard Time">(UTC+05:45) Kathmandu</asp:ListItem>
                                        <asp:ListItem value="N. Central Asia Standard Time">(UTC+06:00) Almaty, Novosibirsk</asp:ListItem>
                                        <asp:ListItem value="Central Asia Standard Time">(UTC+06:00) Astana, Dhaka</asp:ListItem>
                                        <asp:ListItem value="Myanmar Standard Time">(UTC+06:30) Yangon (Rangoon)</asp:ListItem>
                                        <asp:ListItem value="SE Asia Standard Time">(UTC+07:00) Bangkok, Hanoi, Jakarta</asp:ListItem>
                                        <asp:ListItem value="North Asia Standard Time">(UTC+07:00) Krasnoyarsk</asp:ListItem>
                                        <asp:ListItem value="China Standard Time">(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi</asp:ListItem>
                                        <asp:ListItem value="North Asia East Standard Time">(UTC+08:00) Irkutsk, Ulaan Bataar</asp:ListItem>
                                        <asp:ListItem value="Singapore Standard Time">(UTC+08:00) Kuala Lumpur, Singapore</asp:ListItem>
                                        <asp:ListItem value="W. Australia Standard Time">(UTC+08:00) Perth</asp:ListItem>
                                        <asp:ListItem value="Taipei Standard Time">(UTC+08:00) Taipei</asp:ListItem>
                                        <asp:ListItem value="Tokyo Standard Time">(UTC+09:00) Osaka, Sapporo, Tokyo</asp:ListItem>
                                        <asp:ListItem value="Korea Standard Time">(UTC+09:00) Seoul</asp:ListItem>
                                        <asp:ListItem value="Yakutsk Standard Time">(UTC+09:00) Yakutsk</asp:ListItem>
                                        <asp:ListItem value="Cen. Australia Standard Time">(UTC+09:30) Adelaide</asp:ListItem>
                                        <asp:ListItem value="AUS Central Standard Time">(UTC+09:30) Darwin</asp:ListItem>
                                        <asp:ListItem value="E. Australia Standard Time">(UTC+10:00) Brisbane</asp:ListItem>
                                        <asp:ListItem value="AUS Eastern Standard Time">(UTC+10:00) Canberra, Melbourne, Sydney</asp:ListItem>
                                        <asp:ListItem value="West Pacific Standard Time">(UTC+10:00) Guam, Port Moresby</asp:ListItem>
                                        <asp:ListItem value="Tasmania Standard Time">(UTC+10:00) Hobart</asp:ListItem>
                                        <asp:ListItem value="Vladivostok Standard Time">(UTC+10:00) Vladivostok</asp:ListItem>
                                        <asp:ListItem value="Central Pacific Standard Time">(UTC+11:00) Magadan, Solomon Is., New Caledonia</asp:ListItem>
                                        <asp:ListItem value="New Zealand Standard Time">(UTC+12:00) Auckland, Wellington</asp:ListItem>
                                        <asp:ListItem value="Fiji Standard Time">(UTC+12:00) Fiji, Marshall Is.</asp:ListItem>
                                        <asp:ListItem value="Kamchatka Standard Time">(UTC+12:00) Petropavlovsk-Kamchatsky</asp:ListItem>
                                        <asp:ListItem value="Tonga Standard Time">(UTC+13:00) Nuku&#39;alofa</asp:ListItem>
                                      </asp:DropDownList>
                                      &nbsp;<font color="#FF0000">*</font> </td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top">&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top"><a href="javascript:;" class="bluetablink" runat="server" onclick="return ValidatePostNLeapData();" id="lnkScheduledPost">Add Schedule</a> &nbsp;&nbsp; <a href="javascript:;" class="bluetablink" onclick="ClearSchedule();" id="lnkCancel">Reset</a>
                                      <input type="hidden" runat="server" id="hdnMakeDateString" />
                                      <asp:Label ID="lblTimeZone" runat="server" Visible="false"></asp:Label>
                                      <input type="hidden" runat="server" id="hdnTimeZone" /></td>
                                  </tr>
                                </table></td>
                            </tr>
                          </table>
                        </div>
					  </td>                      
                    </tr>
                  </table>
			  </tr>
			  </tr>
            </table>
			<!-- Quick Posting Ends here -->
			</td>
        </tr>
      </table>
    </div>
  </div>
  <uc2:inner ID="inner2" runat="server" />
</form>
</body>
</html>
<style>
.boxClass{
-moz-box-shadow: 1px 1px 1px 1px #bbbbbb;
-webkit-box-shadow: 1px 1px 1px 1px #bbbbbb;
box-shadow: 1px 1px 1px 1px #bbbbbb;
z-index:100; position:absolute; background-color:#FFFFFF; padding-top:0px; width:160px;
padding-bottom:10px;
}
</style>