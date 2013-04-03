// JavaScript Document
var objdivision;
			
	function showDivPopup(divId,windowHeight, windowWidth)
	{
		
		
		var centerWidth = (window.screen.width - windowWidth) / 2;
		var centerHeight = (window.screen.height - windowHeight) / 2;
	
		var objCategoryDiv=document.getElementById(divId);
		objdivision=divId;
		objCategoryDiv.style.top=centerHeight + "px";
		objCategoryDiv.style.left=centerWidth + "px";
		//document.getElementById("divBackGround").style.display = "";
		objCategoryDiv.style.display="";
		objCategoryDiv.style.zIndex=102;
		setLoadingPositionOfDiv();
	}		
	function ShowDivPopup(divId)
	{
		hideSelectBoxes();		
		objdivision=divId;		
		var divisionloading = document.getElementById(objdivision);
	//	document.getElementById("divBackGround").style.display = "";		
		divisionloading.style.zIndex=100;
		divisionloading.style.display='';
		setLoadingPositionOfDiv();
		window.setTimeout("setLoadingPositionOfDiv();",5);
		return false;
	}		

	function hideDivPopup(divId)
	{
		showSelectBoxes();
		document.getElementById(divId).style.display="none";
	//	document.getElementById("divBackGround").style.display = "none";
		
	}
	
	// ---------------------- Center Code Start ------------------------------
	addDIVFunction(window,'scroll', setLoadingPositionOfDiv);
	
	function addDIVFunction(eventObject,eventFiresOn,eventFunction)
	{
	if(eventObject.addEventListener) eventObject.addEventListener(eventFiresOn, eventFunction, false); else if (eventObject.attachEvent) eventObject.attachEvent('on'+ eventFiresOn, eventFunction); 
	}
	function setLoadingPositionOfDiv()
	{
		if (objdivision)
		{
			var divisionloading = document.getElementById(objdivision);
			divisionloading.style.left = ((document.body.clientWidth - divisionloading.offsetWidth) /2) + "px";

			var top =  window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop ;
			divisionloading.style.top = (top + (document.body.clientHeight - divisionloading.offsetHeight) /2) +  "px";			
			var DivBack = document.getElementById("divBackGround");
			DivBack.style.left ="0px"
			DivBack.style.width = document.body.clientWidth + "px";
			DivBack.style.height = document.body.clientHeight + "px";
			DivBack.style.top =top + "px";		
		}	
	
	}
	// ---------------------- Center Code End ------------------------------
	
	function showSelectBoxes(){
	/*var selects = document.getElementsByTagName("select");
	for (i = 0; i != selects.length; i++) {
		selects[i].style.visibility = "visible";
	}*/
}

// ---------------------------------------------------

function hideSelectBoxes(){
/*	var selects = document.getElementsByTagName("select");
	for (i = 0; i != selects.length; i++) {
		selects[i].style.visibility = "hidden";
	}*/
}