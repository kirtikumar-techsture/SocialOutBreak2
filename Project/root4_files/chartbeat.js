/*(function(){function d(){this.a=window._sf_async_config||{};this.J=this.l(this.O);this.z=0;this.B()}function i(){this.e=[];this.b=[{},{},{},{}];this.f=0;this.u(window,"onscroll");this.u(document.body,"onkeydown");this.u(document.body,"onmousemove");this.G()}d.prototype.l=function(a){var b=this;return function(){a.apply(b,arguments)}};d.prototype.w=function(){var a,b,c;a="";for(c=0;c<16;c++){b=Math.floor(Math.random()*36).toString(36);a+=b}return a};d.prototype.S=function(){for(var a=document.getElementsByTagName("script"),
b=0;b<a.length;b++)if(a[b].src.match(/(chartbeat|chartbeatdev|chartbeat_raw).js/)){var c=a[b];break}if(c){a=this.Q(c.src.split("?")[1]);this.d("uid",a.uid);this.d("domain",a.domain)}};d.prototype.Q=function(a){var b={};if(a){if(a.charAt(0)=="?")a=a.substring(1);a=a.replace("+"," ");a=a.split(/[&;]/g);for(var c=0;c<a.length;c++){var f=a[c].split("=");b[decodeURIComponent(f[0])]=decodeURIComponent(f[1])}}return b};d.prototype.K=function(){var a=e.n(),b=a.pathname+(a.search||"");b=b.replace(/PHPSESSID=[^&]+/,
"");var c=/&utm_[^=]+=[^&]+/ig;if(a=c.exec(a.search))b=b.replace(c,"");c=/\?utm_[^=]+=[^&]+(.*)/i;if(a=c.exec(b))b=b.replace(c,a[1]!=""?"?"+a[1]:"");return b};d.prototype.d=function(a,b){this.a[a]=this.a[a]||b};d.prototype.B=function(){if(!g.j("_SUPERFLY_nosample")){this.g=this.q=0;this.H=e.getTime();this.t=true;this.k=null;this.S();var a=e.n();this.d("pingServer","");this.d("title",document.title);this.d("domain",a.host);this.d("path",this.K());this.P=this.w();this.A=a.host;this.A=
this.m(this.A);this.a.domain=this.m(this.a.domain);this.c=g.j("_chartbeat2");this.D=0;if(!this.c)if(this.c=g.j("_chartbeat"))g.remove("_chartbeat");else{this.D=1;this.c=this.w()}g.create("_chartbeat2",this.c,30);if(this.M)this.s();else{this.M=true;this.h=new i;if(window._sf_async_config)this.s();else{a=this.l(this.s);if(typeof window.addEventListener!="undefined")window.addEventListener("load",a,false);else if(typeof document.addEventListener!="undefined")document.addEventListener("load",a,false);
else typeof window.attachEvent!="undefined"&&window.attachEvent("onload",a)}}}};d.prototype.V=function(a){this.a.path=a;window.clearInterval(this.p);this.B()};d.prototype.I=function(a){this.k=a};d.prototype.s=function(){var a=window._sf_startpt,b=window._sf_endpt;if(e.r(a))this.F=e.r(b)?b-a:e.getTime()-a;this.p=window.setInterval(this.l(this.C),15E3);this.C()};d.prototype.T=function(a){var b=new Image(1,1);b.onerror=this.J;b.src=a};d.prototype.O=function(){this.z++;if(this.z<3){this.t=true;this.v()}else{clearInterval(this.p);
g.create("_SUPERFLY_nosample",1,0.0070)}};d.prototype.v=function(){var a=this.g;this.g=a=a?Math.min(a*2,16):1};d.prototype.C=function(){var a=this.h.N();this.h.G();if(this.q<this.g&&!a)this.q++;else{if(a)this.g=0;else this.v();this.q=0;this.R();e.getTime()-this.H>72E5&&window.clearInterval(this.p)}};d.prototype.m=function(a){return a.replace(/^www\./,"")};d.prototype.L=function(){var a=window,b=document.body,c=document.documentElement;if(e.r(a.pageYOffset))return a.pageYOffset;else if(b&&b.scrollTop)return b.scrollTop;
else if(c&&c.scrollTop)return c.scrollTop;return 0};d.prototype.R=function(){var a=this.L(),b=0,c=0,f=0;if(this.h.o("onkeydown"))c=1;else if(this.h.o("onmousemove")||this.h.o("onscroll"))b=1;else f=1;var h=this.a,j="",l="",k=e.n();if(this.t){this.t=false;j=document.referrer||"";j=(j.indexOf("://"+k.host+"/")!=-1?"&v=":"&r=")+encodeURIComponent(j);l="&i="+e.i(h.title.slice(0,100))}var m=this.F?"&b="+this.F:"",n=this.k?"&A="+this.k:"",o=h.sections?"&g0="+encodeURIComponent(h.sections):"",p=h.authors?
"&g1="+encodeURIComponent(h.authors):"";this.T((k.protocol||"http:")+"//"+h.pingServer+"/ping?h="+'127.0.0.1'+"&p="+e.i(h.path)+"&u="+this.c+"&d="+e.i(this.m(k.host))+"&g="+h.uid+o+p+"&n="+this.D+"&c="+Math.round((e.getTime()-this.H)/600)/100+"&x="+a+"&y="+(document.body.scrollHeight||0)+"&w="+(window.innerHeight||document.body.offsetHeight||0)+"&j="+Math.round((this.g+2)*15E3/1E3)+"&R="+b+"&W="+c+"&I="+f+j+m+n+"&t="+this.P+l+"&_")};i.prototype.N=function(){for(var a=0;a<this.e.length;a++)if(this.b[this.f][this.e[a]])return true;
return false};i.prototype.u=function(a,b){var c=a[b]||function(){},f=this;this.e.push(b);a[b]=function(){c.apply(this,arguments);f.U(b)}};i.prototype.U=function(a){this.b[this.f][a]++};i.prototype.o=function(a){for(var b=0,c=0;c<this.b.length;c++)b+=this.b[c][a]||0;return b};i.prototype.G=function(){this.f=(this.f+1)%this.b.length;for(var a=0;a<this.e.length;a++)this.b[this.f][this.e[a]]=0};var g={};g.j=function(a){a=a+"=";for(var b=document.cookie.split(";"),c=0;c<b.length;c++){for(var f=b[c];f.charAt(0)==
" ";)f=f.substring(1,f.length);if(f.indexOf(a)==0)return f.substring(a.length,f.length)}return null};g.create=function(a,b,c){var f=new Date;f.setTime(e.getTime()+c*864E5);document.cookie=a+"="+b+("; expires="+f.toGMTString())+"; path=/"};g.remove=function(a){g.j(a)&&g.create(a,"",-1)};var e={};e.r=function(a){return typeof a=="number"};e.getTime=function(){return(new Date).getTime()};e.i=function(a){return encodeURIComponent(a)};e.n=function(){return window.location};window.pSUPERFLY=new d;d.prototype.virtualPage=
d.prototype.V;d.prototype.activity=d.prototype.I})();
*/