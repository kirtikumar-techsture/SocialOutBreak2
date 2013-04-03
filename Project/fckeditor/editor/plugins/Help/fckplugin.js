// Our method which is called during initialization of the toolbar.
function Help()
{
}

// Disable button toggling.
Help.prototype.GetState = function()
{
	return FCK_TRISTATE_OFF;
}

// Our method which is called on button click.
Help.prototype.Execute = function()
{
	window.open('../../admin/help/quick-reference.aspx');
}

// Register the command.
FCKCommands.RegisterCommand('Help', new Help());

// Add the button.
var item = new FCKToolbarButton('Help', 'Help');
item.IconPath = FCKPlugins.Items['Help'].Path + 'Help.gif';
FCKToolbarItems.RegisterItem('Help', item);