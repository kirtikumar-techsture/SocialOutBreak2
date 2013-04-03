// JavaScript Document
                    $(document).ready(function() {
						$('#txtActivationHour').timepicker({
							showMinutes: false,
							showLeadingZero: false,
							showPeriodLabels: false,
							//onHourShow: timepicker7OnHourShowCallback1,
						});
						$('#txtActivationMinute').timepicker({
							showHours: false,
							showMinutes: true,
							showLeadingZero: false,
							//onMinuteShow: timepicker7OnMinuteShowCallback1,
						});
                      
                        $("#txtActivationDate").datepicker(
							{minDate: new Date(),
							 showLeadingZero: false,
							 maxDate: null,
							 }
						);	
//                         $("#txtvideo").validate({
//						  rules: {
//							field: {
//							  required: true,
//							  url: true
//							}
//						  }
//						});
					})
					function timepicker7OnHourShowCallback1(hour) {
						var dTime = new Date();
						var hours1 = dTime.getHours();
						var minute1 = dTime.getMinutes();
						if (minute1 > 30) 
						{
							hours1 = hours1 +1;
						}
						if ((hour < hours1)) {
							return false;
						}
						return true;
					}
					function OpenCal(Id) {
                        document.getElementById(Id).focus();
                    }
					function timepicker7OnMinuteShowCallback1(hour, minute) {
						var dTime1 = new Date();
						var minute1 = dTime1.getMinutes();
						var hours1 = dTime1.getHours();
						if (minute1 > 30) 
							{
								var diff=60-minute1;
								var diff1=30-diff;
								if (minute < diff1) {
									return false;
								}
								else
								{
									return true;
								}
							}
							else
							{
								if (minute < minute1+30) {
									return false;
								}
								else
								{
									return true;
								}
							}
					}
    			 function ValidatePostNLeapData() {
/*					var date = new Date();//'Fri May 11 2012 20:15:49';//
					var mth = 0;
					if ((date.getMonth() + 1) <= 9)
					{
						mth = "0" + (date.getMonth() + 1);
					}
					else
					{
						mth = (date.getMonth() + 1);
					}
					var dateform = mth + '/' + date.getDate() + '/' + date.getFullYear();
					
					
					var currentTime = (new Date(date))
					var hours = currentTime.getHours()
					//Note: before converting into 12 hour format
					var suffix = '';
					if (hours > 11) {
						suffix += "PM";
					} else {
						suffix += "AM";
					}
					var minutes = currentTime.getMinutes()
					if (minutes < 10) {
						minutes = "0" + minutes
					}
					if (hours > 12) {
						hours -= 12;
					} else if (hours === 0) {
						hours = 12;
					}
					var time = hours + ":" + minutes + " " + suffix;
					
					var dateselected = $.trim($('#txtActivationDate').val()) + " " + $.trim($('#selActivationHour').val()) + ":" + $.trim($('#selActivationMinute').val()) +":00 " + $.trim($('#selAMPM').val());
					var currdate = dateform +" " + hours + ":" + minutes + ":00 " + suffix;
					
					alert('Selected Date: ' + dateselected);	
					alert("current date: " + currdate);
					
					if ($.trim($('#txtActivationDate').val()) == dateform)
					{
						if ($.trim($('#selAMPM').val()) != suffix)
						{
							alert('Please select a time at least 15 minutes from now 123');
						}
						else
						{
							if (dateform == $.trim($('#txtActivationDate').val())  && hours == $.trim($('#selActivationHour').val()) && suffix == $.trim($('#selAMPM').val()))
							{
								var tmdiff = minutes - $.trim($('#selActivationMinute').val());
								if (tmdiff < -12 )
								{
									alert('Thank You');	
								}
								else
								{
									alert('Please select a time at least 15 minutes from now test');
								}
							}
						}
					}
					else if ($.trim($('#txtActivationDate').val()) < dateform)
					{
						alert('Please select a time at least 15 minutes from now test abcd');
					}
					else if ($.trim($('#txtActivationDate').val()) > dateform)
					{
						alert('thanks');
					}
								
					return false;*/
					var Title = "Fill in Following Information\n";
					var fields = "";
					
					
					if ($.trim($('#txtMessage').val()) == '') {
						fields = fields + "\n-- Compose Message --";
					}
					if ($.trim($('#txtActivationDate').val()) == '') {
						fields = fields + "\n-- Activation Date --";
					}
		
					if ($.trim($('#selActivationHour').val()) == 0) {
						fields = fields + "\n-- Activation Hour --";
					}
		
					if ($.trim($('#selActivationMinute').val()) == 'Minute') {
						fields = fields + "\n-- Activation Minute --";
					}
					
                    if ($.trim($('#selAMPM').val()) == 0) {
						fields = fields + "\n-- AM/PM --";
					}
                    if ($('#ddlTimeZone').val() == "0") {
						fields = fields + "\n-- TimeZone --";
					}
					if(selectedpages()==false)
					{
						fields = fields + "\n-- Please select a business page before attempting to post --";
					}
                    selectedpageimage();
                    selectedpagesName();
					if (fields.length > 0) {
						alert(Title + fields);
						if (fields=="\n-- Please select a business page before attempting to post --"){			
						$(".Fanpagesdiv1").show("slow");
						$('#divFanPages').removeClass("divFanPagesAutoPost");
						$('#divFanPages').addClass("divFanPages");
						$(".composediv1").hide("slow");
						$(".photodiv1").hide("slow");
						$(".Linkdiv").hide("slow");
						$('#divLibCat').hide("slow");
						$('#divSchedule').hide("slow");
						}
						return false;
					}
					else {
						return true;
					}
				}
				 
                 function ValidateQuickPost() {
					//alert("test");
					var Title = "Fill in Following Information\n";
					var fields = "";
					if ($.trim($('#txtMessage').val()) == '') {
						fields = fields + "\n-- Compose Message --";
					}
                    selectedpageimage();
                    selectedpagesName();
					GetVideoId();
                    if(selectedpages()==false)
					{
						fields = fields + "\n-- Please select a business page before attempting to post --";
					}
					if (fields.length > 0) {
						alert(Title + fields);
						if (fields=="\n-- Please select a business page before attempting to post --"){			
						$(".Fanpagesdiv1").show("slow");
						$('#divFanPages').removeClass("divFanPagesAutoPost");
						$('#divFanPages').addClass("divFanPages");
						$(".composediv1").hide("slow");
						$(".photodiv1").hide("slow");
						$(".Linkdiv").hide("slow");
						$('#divLibCat').hide("slow");
						$('#divSchedule').hide("slow");
						}
						
						return false;
						
					}
					else {
					if($.trim($('#txtvideo').val()) != '' && $.trim($('#photo').val()) != '')
						{
							if(confirm('Warning: This will post two different message on your wall becuase you selected both link and picture as a quick post. are you sure you want to continue?'))
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
				
				function GetVideoId()
				{
					var obj=document.getElementById('txtvideo');
    				 var vid;
						  var results;
						  var url=DoTrim(obj.value).toLowerCase();
						  //Youtube
							url=DoTrim(obj.value);
							  results = url.match("[\\?&]v=([^&#]*)");
							  if(results != null )
							  {
								vid=results[1];
								if(vid!='')
								{
									document.getElementById('imgThumbnail').src="http://img.youtube.com/vi/"+vid+"/2.jpg";	
									document.getElementById('hdnVideoId').value=vid;
									document.getElementById('hdnUrl').value="http://img.youtube.com/vi/"+vid+"/2.jpg";	
									return true;
								}	
							  } 
				}
                
				function Pageid(_this) {
					if($(_this).attr("checked")=="checked") 
					{
						$('input[group^=pages]').each(
							function(){
								$(this).attr("checked",false)
							}
						);
						$(_this).attr("checked",true);
					}
					else
					{
						$('input[group^=page]').each(
							function(){
								$(this).attr("checked",false)
							}
						);
					}
				}
				function PageidScheduler(_this)
				{
					
				}
				function selectedpages() {
					var pages = '';
                 	var hdnPages = '';
					$('input[group^=pages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
								pages = pages + $(this).attr("pageid") + ','
							}
						}
					);
					if (pages != '') {
                        $('#hdnPageId').val(pages);
                        return true;
					}
					else
					{
						return false;
					}
                   
				}
                	
              
                function selectedpageimage() {
                    var pageImage = '';
					var hdnPages = '';
					$('input[group^=pages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
								pageImage = pageImage + $(this).attr("pageimage") + ','
							}
						}
					);
					if (pageImage != '') {
                        $('#hdnImage').val(pageImage);
						return true;
					}
					else
					{
						return false;
					}
				}
                function selectedpagesName() {
					var pages = '';
					var hdnPages = '';
					$('input[group^=pages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
								var pageid = $(this).attr("pageid")
								pages = pages + $(this).attr("pagevalue") + "<a href='javascript:RemovePage(" + pageid + ")'>&nbsp;&nbsp;<img src='../content/images/remove.gif' width='8' height='8' /></a><br/>"
							}
						}
					);
					if (pages != '') {
						$('#divHtml').show("slow");
						$('#divHtml').html("<strong>Selected Pages:</strong><br/>" + pages );
						return true;
					}
					else
					{
						$('#divHtml').hide("slow");
						$('#divHtml').html('');
						return false;
					}
				}
                
				function RemovePage(pageid)
				{
				   $('input[group^=pages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
								var pageid1 = $(this).attr('pageid');
								 if(pageid == pageid1)
								 {
								var id= $(this).attr('id');
								 $("#" + id).removeAttr("checked");
								 selectedpagesName();
								 }
							}
						}
					);	
				}
				
			 function ValidateAlbum() {
			
					var Title = "Fill in Following Information\n";
					var fields = "";
                    if(selectedpages()==false)
					{
						fields = fields + "\n-- Please select a business page before attempting to post --";
					}
					
					 if ($.trim($('#photo').val()) == '') {
						fields = fields + "\n-- Upload Photo --";
					}
                   
                    selectedpagesAccessToken();
                    selectedpageimage();
                    selectedpagesName();
					
                  
					if (fields.length > 0) {
						alert(Title + fields);
						return false;
					}
					else {
                    ShowProgress();
						return true;
					}
				}
				
                function ValidatePublish() {
					var Title = "Fill in Following Information\n";
					var fields = "";
					if(selectedpages()==false)
					{
						fields = fields + "\n-- Please select a business page before attempting to post --";
					}
					                   
                    selectedpagesAccessToken();
                    selectedpageimage();
                    selectedpagesName();
					if (fields.length > 0) {
						alert(Title + fields);
						return false;
					}
					else {
						PublishAlert();
						return true;
					}
					
				}

                function ValidateVideoUpload() {
                    var Title = "Fill in Following Information\n";
					var fields = "";
                    /*if(selectedpages()==false)
					{
						fields = fields + "\n-- Please select a business page before attempting to post --";
					}*/
                    if ($.trim($('#photo').val()) == '') {
						fields = fields + "\n-- Upload Photo/Video --";
					}
                    selectedpagesAccessToken();
                    selectedpageimage();
                    selectedpagesName();
                  
					if (fields.length > 0) {
						alert(Title + fields);
						return false;
					}
					else {
                    //ShowProgress();
						return true;
					}
				}
		 function HideProgress() {
                 parent.document.getElementById("imgLoading").style.display = 'none';
        
                }
            function ShowProgress() {
                         parent.document.getElementById("imgLoading").style.display = 'block';
      
                }

  
                function selectedpagesAccessToken() {
                    var pagesaccesstoken = '';
					var hdnPages = '';
					$('input[group^=pages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
							      pagesaccesstoken = pagesaccesstoken + $(this).attr("pageaccess_token") + ','
							}
						}
					);
				   
                    if (pagesaccesstoken != '') {
                        $('#hdnAccessToken').val(pagesaccesstoken);
                        return true;
					}
					else
					{
						return false;
					}
				}
                	 function ValidateDrafts() {
			
					var Title = "Fill in Following Information\n";
					var fields = "";
                    if ($.trim($('#txtMessage').val()) == '') {
						fields = fields + "\n-- Compose Message --";
					}
                 
					if (fields.length > 0) {
						alert(Title + fields);
						return false;
					}
					else {
						return true;
					}
				    
                    
				}

				function PublishAlert()
						{
							jConfirm('Sidebar will upload to the photo tab on your Facebook Business page', 'Publish Sidebar', 
									function(r) {
									if(r==true)
									{
										__doPostBack("btnUpload", "");
									}
									});
				
						}
				function PublishAlertCoverPhoto()
						{
							jConfirm('Cover Photo will upload to the photo tab on your Facebook Business page', 'Publish Cover Photo', 
									function(r) {
									if(r==true)
									{
										__doPostBack("btnUpload", "");
									}
									});
				
						}

/***************         Auto Post Validation  ************************************************************************/

			function AutoPostPageid(_this) {
				
				}
			function SelectedAutoPostPages() {
					var autopages = '';
                 	var autohdnPages = '';
					$('input[group^=autopostpages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
								autopages = autopages + $(this).attr("autopostpageid") + ','
							}
						}
					);
					if (autopages != '') {
						$('#hdnAutoPostPageId1').val(autopages);
                        return true;
					}
					else
					{
						return false;
					}
                   
				}
                	
              
                function SelectedAutoPostImage() {
                    var autopageImage = '';
					var autohdnPages = '';
					$('input[group^=autopostpages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
								autopageImage = autopageImage + $(this).attr("autopostpageimage") + ','
							}
						}
					);
					if (autopageImage != '') {
                        $('#hdnAutoPostImage1').val(autopageImage);
						return true;
					}
					else
					{
						return false;
					}
				}
				
				function SelectedAutoPostPagesName() {
				var autopages = '';
                var autohdnPages = '';
				$('input[group^=autopostpages]').each(
					function () {
							if ($(this).attr("checked") == 'checked') {
								var autopageid = $(this).attr("autopostpageid")
								autopages = autopages + $(this).attr("autopostpagevalue") + "<a href='javascript:RemoveAutoPostPage(" + autopageid + ")'>&nbsp;&nbsp;<img src='../content/images/remove.gif' width='8' height='8' /></a><br/>"
							}
						}
					);
					if (autopages != '') {
						$('#divAutoPostHtml').show("slow");
						$('#divAutoPostHtml').html("<strong>Selected Pages:</strong><br/>" + autopages );
						return true;
					}
					else
					{
						$('#divAutoPostHtml').hide("slow");
						$('#divAutoPostHtml').html('');
						return false;
					}
				}
			
			function SelectedAutoPostPagesAccessToken() {
                    var autopagesaccesstoken = '';
					var autohdnPages = '';
					$('input[group^=autopostpages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
							      autopagesaccesstoken = autopagesaccesstoken + $(this).attr("autopostpageaccess_token") + ','
							}
						}
					);
				   
                    if (autopagesaccesstoken != '') {
                        $('#hdnAutoPostAccessToken1').val(autopagesaccesstoken);
                        return true;
					}
					else
					{
						return false;
					}
				}
				
				function RemoveAutoPostPage(autopageid)
				{
				   $('input[group^=autopostpages]').each(
						function () {
							if ($(this).attr("checked") == 'checked') {
								var autopageid1 = $(this).attr('autopostpageid');
								 if(autopageid == autopageid1)
								 {
								var id= $(this).attr('id');
								 $("#" + id).removeAttr("checked");
								 SelectedAutoPostPagesName();
								 }
							}
						}
					);	
				
					
				}
 
 function ValidateAutoPost() {
					var Title = "Fill in Following Information\n";
					var fields = "";
					
					/*  if ((document.getElementById('rd7day').checked == false) && (document.getElementById('rd5day').checked == false) && (document.getElementById('rd1day').checked == false) )
					{
						fields = fields + "\n-- Autopost Schedule Day   --";
					}
							
					if ((document.getElementById('rd1day').checked == true) && (document.getElementById('autopostday1').checked == false) && (document.getElementById('autopostday2').checked == false) && (document.getElementById('autopostday3').checked == false) && (document.getElementById('autopostday4').checked == false) 
				&& (document.getElementById('autopostday5').checked == false) && (document.getElementById('autopostday6').checked == false)  && (document.getElementById('autopostday7').checked == false))
					{
						fields = fields + "\n-- Select Single Day for AutoPost  --";
					}*/
								
					if ($.trim($('#selAutoPostActivationHour').val()) == 0) {
						fields = fields + "\n-- Activation Hour --";
					}
					if ($.trim($('#selAutoPostActivationMinute').val()) == 'Minute') {
						fields = fields + "\n-- Activation Minute --";
					}
                    if ($.trim($('#selAutoPostAMPM').val()) == 0) {
						fields = fields + "\n-- AM/PM --";
					}
                    if ($('#ddlAutoPostTimeZone').val() == "0") {
						fields = fields + "\n-- TimeZone --";
					}
					if(SelectedAutoPostPages()==false)
					{
						fields = fields + "\n-- Please select a business page before attempting to post --";
					}
                    
                    SelectedAutoPostPagesName();
					SelectedAutoPostPagesAccessToken();
					SelectedAutoPostImage();
					if (fields.length > 0) {
						alert(Title + fields);
						if (fields=="\n-- Please select a business page before attempting to post --")
						{
						OpenAutoPostBusinessPageDiv();
						}
						return false;
					}
					else {
						return true;
					}
				}
				
				function ValidatePublishCoverPhoto() {
					var Title = "Fill in Following Information\n";
					var fields = "";
					if(selectedpages()==false)
					{
						fields = fields + "\n-- Please select a business page before attempting to post --";
					}
					                   
                    selectedpagesAccessToken();
                    selectedpageimage();
                    selectedpagesName();
					if (fields.length > 0) {
						alert(Title + fields);
						return false;
					}
					else {
						PublishAlertCoverPhoto();
						return true;
					}
				}
/*************************************** End **************************************************************************/