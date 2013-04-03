<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="test-image-generate.aspx.vb" Inherits="tsma.test_image_generate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
<!-- REMEMBER ME CHECKBOX -->

<script language="JavaScript" type="text/javascript">
<!-- Cookie script - Scott Andrew -->
<!-- Popup script, Copyright 2005, Sandeep Gangadharan --> 
<!-- For more free scripts go to http://www.sivamdesign.com/scripts/ -->
<!--
function newCookie(name,value,days) {
 var days = 1;   // the number at the left reflects the number of days for the cookie to last
                 // modify it according to your needs
 if (days) {
   var date = new Date();
   date.setTime(date.getTime()+(days*24*60*60*1000));
   var expires = "; expires="+date.toGMTString(); }
   else var expires = "";
   document.cookie = name+"="+value+expires+"; path=/"; }

function readCookie(name) {

   var nameSG = name + "=";
   var nuller = '';
  if (document.cookie.indexOf(nameSG) == -1)
    return nuller;

   var ca = document.cookie.split(';');
  for(var i=0; i<ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0)==' ') c = c.substring(1,c.length);
  if (c.indexOf(nameSG) == 0) return c.substring(nameSG.length,c.length); }
    return null; }

function eraseCookie(name) {
  newCookie(name,"",1); }

function toMem(a) {
alert(a);
    newCookie('UserName', document.form1.txtName.value);     // add a new cookie as shown at left for every
    newCookie('Password', document.form1.txtPwd.value);   // field you wish to have the script remember
}

function delMem(a) {
  eraseCookie('UserName');   // make sure to add the eraseCookie function for every field
  eraseCookie('Password');

   document.form1.txtName.value = '';   // add a line for every field
   document.form1.txtPwd.value = ''; }
//-->
</script>

</head>
<body>
    <form id="form1" name="form1" runat="server" action=" " method="post" onSubmit="if (this.checker.checked) toMem(this)">
     <asp:ScriptManager ID="objScriptManager" runat="server"></asp:ScriptManager>
        
        <table border="0" cellpadding="0" cellspacing="0">
        <tr>
        	<td align="left">
            Name
            </td>
                    	<td align="left">
                        <input type="text" id="txtName" name="txtName" runat="server" />
            </td>
        </tr>
        <tr>
        	<td align="left">
            Password
            </td>
                    	<td align="left">
                                                <input type="text" id="txtPwd" name="txtPwd"  runat="server"/>
            </td>
        </tr>
        <tr>
        	<td align="left">
            Remember Me?
            </td>
                    	<td align="left">
                                                <input type="checkbox" id="checker" name="checker"  runat="server" />
            </td>
        </tr>
        <tr>
        	<td align="left">&nbsp;
            
            </td>
                    	<td align="left">
                                                <input type="submit" id="btnSubmit"  runat="server"/>&nbsp;&nbsp;
                                                <input type="submit" id="btnReset"  runat="server"/>&nbsp;&nbsp;
                                                <input type="submit" id="btnErase"  runat="server" onclick="delMem(this)" value="Delete Information" />&nbsp;&nbsp;                                                
            </td>
        </tr>
 
        </table>
  <script type="text/javascript" language="javascript">
		<!--
		document.form1.txtName.value = readCookie("UserName");  // Change the names of the fields at right to match the ones in your form.
		document.form1.txtPwd.value = readCookie("Password");
		//-->
		</script>
         Local Time: <asp:Label ID="lblLocalTime" runat="server" Text=""></asp:Label>
        <br /><br />
        Converted Time:  <asp:Label ID="lblTimeZone" runat="server"></asp:Label>  <br /><br />
        Re-Converted in origanal Time:  <asp:Label ID="lblReOrgTimeZone" runat="server"></asp:Label>  <br /><br />
        <div id="divSchedule" width="350" align="left" valign="top"  style="display:; z-index:100; position:absolute; border:1px solid #eaeaea; padding:10px; background-color:#FFFFFF; width:350px;">
                          <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td align="right" valign="top"><a href="javascript:;" class="fan_close"><img src='<%=ResolveUrl("Content/images/delete_icon.png")%>' width="12" height="12" hspace="7" vspace="7" border="0"/></a></td>
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
                                          <td>&nbsp; <img src="<%=ResolveUrl("~/content/images/calender_icon.png")%>" onClick="OpenCal('txtActivationDate');" width="22" height="23" align="absmiddle" style="cursor: pointer;"/>&nbsp;&nbsp;&nbsp;(mm/dd/yyyy)&nbsp;<font color="#FF0000">*</font></td>
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
                                    <td align="left" valign="top"><a href="javascript:;" class="bluetablink" runat="server" onclick="return ValidatePostNLeapData();" id="lnkScheduledPost">Add Schedule</a> &nbsp;&nbsp; <a href="javascript:;" class="bluetablink" onclick="ClearSchedule();" id="lnkCancel">Cancel</a>
                                      <input type="hidden" runat="server" id="hdnMakeDateString" />
                                     
                                      <input type="hidden" runat="server" id="hdnTimeZone" /></td>
                                  </tr>
                                </table></td>
                            </tr>
                          </table>
                        </div>
        <div align="left" style="padding-top:250px;">
        <asp:DropDownList ID="ddlTimeZone1" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlTimeZone1_SelectedIndexChanged"
            AppendDataBoundItems="true" >
            <asp:ListItem Text="Select a TimeZone" Value="Default value" />
        </asp:DropDownList>
        <br /><br />    
        Local Time: <asp:Label ID="lblLocalTime1" runat="server" Text=""></asp:Label>
        <br /><br />
        Converted Time: <asp:Label ID="lblTimeZone1" runat="server" Text=""></asp:Label>            
   		 </div>

         

         <div class="Fanpagesdiv1" style="z-index: 1000;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td align="left" valign="top" class="popupbordertop" style="padding-left:490px;"><img src="../Content/images/popup_border_top.gif" width="15" height="6" /></td>
                      </tr>
                      <tr>
                        <td align="left" valign="top" class="popupborder" style="border-bottom:0px;"><div style="height:150px; overflow:auto; overflow-x:hidden;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td align="left" valign="top" style="padding:20px;">
                                <asp:DataList ID="dstFanPages" runat="server" RepeatColumns="3">
                                    <ItemTemplate>
                                      <table width="230" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td colspan="2" height="4"></td>
                                        </tr>
                                        <tr>
                                          <td width="48" align="left" valign="middle" style="background-color:#f6f6f6; " ><table border="0" cellspacing="0" cellpadding="0">
                                              <tr>
                                                <td align="center" valign="middle"><img src='<%#Eval("picture")%>' width="40" style=" border: 7px solid #f6f6f6" height="40" align="absmiddle" group="pageimg"pageid='<%#Eval("Id")%>' /> </td>
                                                <td align="center" valign="middle"><table border="0" width="170" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                      <td width="25" align="left" valign="middle" ><input class="checkboxpadding" type="checkbox" id="chkPage" name="chkPage" runat="server" pageid='<%#Eval("Id")%>' group="pages" onclick='Pageid(this);selectedpagesName();'  pageaccess_token='<%#Eval("access_token")%>' pagevalue='<%#Eval("name")%>' pageimage='<%#Eval("picture")%>' /></td>
                                                      <td align="left" width="150" valign="middle"><%#Eval("name")%>
                                                        <input type="hidden" id="hdnPageId" runat="server" value='<%#Eval("Id")%>' />
                                                        <input type="hidden" id="hdnPageName" runat="server" value='<%#Eval("name")%>' />
                                                        <input type="hidden" id="hdnAccessToken" runat="server" value='<%#Eval("access_token")%>' />
                                                        <input type="hidden" id="hdnImage" runat="server" value='<%#Eval("picture")%>' />
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
                                </td>
                              </tr>
                            </table>
                          </div></td>
                      </tr>
                      <tr>
                        <td>
                         <a href="javascript:;" class="bluetablink" runat="server" onclick="return ValidateAutoPost();" id="A1">
                              post fan page
                              </a>
                        </td>
                      </tr>
                    </table>
                  </div>


         
         
         
         
         <div id="divAutoPostFanPages" title="Business Pages">
                                                              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td align="left" valign="top" style="padding:20px;">
                                <asp:DataList ID="dstAutoPostFanPages" runat="server" RepeatColumns="3">
                                    <ItemTemplate>
                                      <table width="230" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td colspan="2" height="4"></td>
                                        </tr>
                                        <tr>
                                          <td width="48" align="left" valign="middle" style="background-color:#f6f6f6; " ><table border="0" cellspacing="0" cellpadding="0">
                                              <tr>
                                                <td align="center" valign="middle"><img src='<%#Eval("picture")%>' width="40" style=" border: 7px solid #f6f6f6" height="40" align="absmiddle" group="pageimg" pageid='<%#Eval("Id")%>' /> </td>
                                                <td align="center" valign="middle"><table border="0" width="170" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                      <td width="25" align="left" valign="middle" >
                                                      <input class="checkboxpadding" type="checkbox" id="AutoPostchkPage" name="AutoPostchkPage" runat="server" AutoPostpageid='<%#Eval("Id")%>' group="AutoPostpages" onclick='AutoPostPageid(this);SelectedAutoPostPagesName();'  AutoPostpageaccess_token='<%#Eval("access_token")%>' AutoPostpagevalue='<%#Eval("name")%>' AutoPostpageimage='<%#Eval("picture")%>' /></td>
                                                      <td align="left" width="150" valign="middle"><%#Eval("name")%>
                                                        <input type="hidden" id="hdnAutoPostPageId" runat="server" value='<%#Eval("Id")%>' />
                                                        <input type="hidden" id="hdnAutoPostPageName" runat="server" value='<%#Eval("name")%>' />
                                                        <input type="hidden" id="hdnAutoPostAccessToken" runat="server" value='<%#Eval("access_token")%>' />
                                                        <input type="hidden" id="hdnAutoPostImage" runat="server" value='<%#Eval("picture")%>' />
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
                                </td>
                              </tr>
                              <tr>
                              <td>
                               <a href="javascript:;" class="bluetablink" runat="server" onclick="return ValidateAutoPost();" id="lnkAutoPost">
                              post fan page
                              </a>
                              </td>
                              </tr>
                            </table>
                        </div>
    </form>
</body>
</html>
