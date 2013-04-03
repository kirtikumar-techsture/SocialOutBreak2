jQuery(document).ready(function() {

	$(".video").click(function() {
				var offset = $('.video').offset();
		var x_pos = offset.left;
		var y_pos = offset.top;
		//alert('left' + x_pos);
		
		$('#hdnVideoTop').val(y_pos);
		//alert('top hidd' + $('#hdnVideoTop').val());	
		$.fancybox({
			'top'			: y_pos,
			'left'			: x_pos,
			'padding'		: 0,
			'autoScale'		: false,
			'transitionIn'	: 'none',
			'transitionOut'	: 'none',
			'title'			: this.title,
			'width'			: 400,
			'height'		: 295,
			'href'			: this.href.replace(new RegExp("watch\\?v=", "i"), 'v/'),
			'type'			: 'swf',
			'swf'			: {
			'wmode'				: 'transparent',
			'allowfullscreen'	: 'true'
			}
		});

		return false;
	});



});
