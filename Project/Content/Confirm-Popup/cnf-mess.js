$(document).ready(function(){
	$('.item .delete').click(function(){
		var elem = $(this).closest('.item');
		$.confirm({
			'title'		: 'Delete Confirmation',
			'message'	: 'You are about to delete this sidebar. <br />It cannot be restored at a later time! Continue?',
			'buttons'	: {
				'Yes'	: {
					'class'	: 'blue',
					'action': function(){
						__doPostBack("lnkdelete1", "");
					}
				},
				'No'	: {
					'class'	: 'gray',
					'action': function(){}	// Nothing to do in this case. You can as well omit the action property.
				}
			}
		});
	});
});