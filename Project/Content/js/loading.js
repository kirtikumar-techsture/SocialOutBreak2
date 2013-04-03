// ---------------------- Loading Code Start ------------------------------

addFunction(window,'load', setLoadingPosition);  
addFunction(window,'scroll', setLoadingPosition);

function addFunction(eventObject,eventFiresOn,eventFunction)
{
	if(eventObject.addEventListener) eventObject.addEventListener(eventFiresOn, eventFunction, false); else if (eventObject.attachEvent) eventObject.attachEvent('on'+ eventFiresOn, eventFunction); 
}
function setLoadingPosition()
{
	var DivLoading = document.getElementById("divLoading");
	if(DivLoading)
	{
		DivLoading.style.left = ((document.body.clientWidth - DivLoading.offsetWidth) /2) + 40 + "px";
		var top =  window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop ;
		DivLoading.style.top = (top+ 280)+"px";
	}
}
// ---------------------- Loading Code End ------------------------------