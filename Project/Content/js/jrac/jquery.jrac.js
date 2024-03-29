/* jQuery Resize And Crop (jrac)
 * A jQuery 1.4+ and jQueryUi 1.8+ plugin under GNU GENERAL PUBLIC LICENSE version 2 lisense.
 * Copyright (c) 2011 Cedric Gampert - cgampert@gmail.com
 */

(function($) {

    $.fn.jrac = function(options) {

        // Default settings
        var settings = {
            'crop_width': 200,
            'crop_height': 100,
            // The two following properties define the crop position (relative to the
            // image).
            //'crop_x': 0,
            //'crop_y': 0,
            'crop_resize': true,
            'crop_aspect_ratio': null,
            'image_width': null,
            'image_height': null,
            'zoom_min': 100,
            'zoom_max': 3000,
            'viewport_resize': true,
            // The two following properties allow to position the content (negative
            // value allowed). It can be use to focus the viewport on the cropped
            // part of the image.
            'viewport_content_left': 0,
            'viewport_content_top': 0,
            // Submit here a callback function (context is the viewport).
            'viewport_onload': null
        };

        // Apply the resize and crop tools to each images
        return this.each(function() {
            if (!$(this).is('img')) {
				return;
            }

            // Read options
            if (options) {
                $.extend(settings, options);
            }
            // Prepare image
            var $image = $(this).hide().css('position', 'absolute').wrap('<div class="jrac_container"><div class="jrac_viewport"></div></div>');
            // Get viewport and container references
          
		    var $viewport = $image.parent();
            var $container = $viewport.parent();

            // Add a waiting on load input image
            var $loading = $('<div class="jrac_loading" />');
            $viewport.append($loading);

            // The following procedure hold business intend to be run once the image
            // is loaded (load event).
            var image_load_handler = function() {

                // Add some custom properties to $image
                $.extend($image, {
                    scale_proportion_locked: true,
                    originalWidth: $image.width(),
                    originalHeight: $image.height()
                });

                // Set given optional image size
                $image.width(settings.image_width).height(settings.image_height);

                // Set the viewport content position for the image
                $image.css({ 'left': settings.viewport_content_left, 'top': settings.viewport_content_top });

                // Create the zoom widget which permit to resize the image
                //var $zoom_widget = $('<div class="jrac_zoom_slider"><div class="ui-slider-handle"></div></div>') original code
                var $zoom_widget = $('<div class="jrac_zoom_slider" style="display:"><div class="ui-slider-handle"></div></div>')
        .width($viewport.width())
        .slider({
            value: $image.width(),
            min: settings.zoom_min,
            max: settings.zoom_max,
            start: function(event, ui) {
                $.extend($zoom_widget, {
                    on_start_width_value: ui.value,
                    on_start_height_value: $image.height()
                })
            },
            slide: function(event, ui) {
                var height = Math.round($zoom_widget.on_start_height_value * ui.value / $zoom_widget.on_start_width_value);
                $image.height(height);
				 document.getElementById('tmpD').innerHTML='Current Size: ' + ui.value + ' x ' + height;
                $image.width(ui.value);
                $viewport.observator.notify('image_height', height);
                $viewport.observator.notify('image_width', ui.value);
            }
        });
                $container.append($zoom_widget);

                // Make the viewport resizeable
                if (settings.viewport_resize) {
                    $viewport.resizable({
                        resize: function(event, ui) {
                            $zoom_widget.width(ui.size.width);
                        }
                    });
                }

                // Enable the image draggable interaction
                $image.draggable({
                    drag: function(event, ui) {
                        if (ui.position.left != ui.originalPosition.left) {
                            $viewport.observator.notify('crop_x', $viewport.observator.crop_position_x());
                        }
                        if (ui.position.top != ui.originalPosition.top) {
                            $viewport.observator.notify('crop_y', $viewport.observator.crop_position_y());
                        }
                    }
                });

                // Build the crop element
                var $crop = $('<div class="jrac_crop"><div class="jrac_crop_drag_handler"></div></div>')
        .css({
            'width': settings.crop_width,
            'height': settings.crop_height,
            'left': settings.crop_x + settings.viewport_content_left,
            'top': settings.crop_y + settings.viewport_content_top
        }).draggable({
            containment: $viewport,
            handle: 'div.jrac_crop_drag_handler',
            drag: function(event, ui) {
                if (ui.position.left != ui.originalPosition.left) {
                    $viewport.observator.notify('crop_x', $viewport.observator.crop_position_x());
                }
                if (ui.position.top != ui.originalPosition.top) {
                    $viewport.observator.notify('crop_y', $viewport.observator.crop_position_y());
                }
            }

        });
                //alert($crop.left());
                if (settings.crop_resize) {
                    $crop.resizable({
                        containment: $viewport,
                        aspectRatio: settings.crop_aspect_ratio,
                        resize: function(event, ui) {
                            if (ui.size.width != ui.originalSize.width) {
                                $viewport.observator.notify('crop_width', $crop.width());
                            }
                            if (ui.size.height != ui.originalSize.height) {
                                $viewport.observator.notify('crop_height', $crop.height());
                            }
                        }
                    })
                }
                $viewport.append($crop);

                // Extend viewport witch usefull objects as it will be exposed to user
                // functions interface
                $.extend($viewport, {
                    $container: $container,
                    $image: $image,
                    $crop: $crop,
                    // Let me introduce the following Terminator's friend which handle the
                    // creation of the viewport events.
                    observator: {
                        items: {},
                        // Register an event with a given element
                        register: function(event_name, element, onevent_callback) {
                            if (event_name && element) {
                                this.items[event_name] = {
                                    element: element,
                                    callback: onevent_callback
                                };
                            }
                        },
                        // Unregister an event
                        unregister: function(event_name) {
                            delete this.items[event_name];
                        },
                        // Trigger an event and optionally supply a value
                        notify: function(event_name, value) {
                            if (this.items[event_name]) {
                                var element = this.items[event_name].element;
                                var onevent_callback = this.items[event_name].callback;
                                element.trigger(event_name, [$viewport, value]);
                                if ($.isFunction(onevent_callback)) {
                                    onevent_callback.call($viewport, event_name, element, value);
                                }
                            }
                            $image.trigger('viewport_events', [$viewport]);
                        },
                        notify_all: function() {
                            this.notify('crop_x', this.crop_position_x());
                            this.notify('crop_y', this.crop_position_y());
                            this.notify('crop_width', $crop.width());
                            this.notify('crop_height', $crop.height());
                            this.notify('image_width', $image.width());
                            this.notify('image_height', $image.height());
                        },
                        // Return crop x position relative to $image
                        crop_position_x: function() {
                            $('#hdnX').val($crop.position().left - $image.position().left);
                            return $crop.position().left - $image.position().left;
                        },
                        // Return crop y position relative to $image
                        crop_position_y: function() {
                            $('#hdnY').val($crop.position().top - $image.position().top);
                            return $crop.position().top - $image.position().top;
                        },
                        // Does the crop is completely inside the image?
                        crop_consistent: function() {
                            return this.crop_position_x() >= 0 && this.crop_position_y() >= 0
              && this.crop_position_x() + $crop.width() <= $image.width()
              && this.crop_position_y() + $crop.height() <= $image.height();
                        },
                        // Set a property (which his name is one of the event) with a given
                        // value then notify this operation
                        set_property: function(that, value) {
                            value = parseInt(value);
                            if (isNaN(value)) {
                                return;
                            }
                            switch (that) {
                                case 'crop_x':
                                    //$('#hdnX').val(value + $image.position().left);
                                    $crop.css('left', value + $image.position().left);
                                    break;
                                case 'crop_y':
                                    //$('#hdnY').val(value + $image.position().top);
                                    $crop.css('top', value + $image.position().top);
                                    break;
                                case 'crop_width':
                                    $crop.width(value);
                                    break;
                                case 'crop_height':
                                    $crop.height(value);
                                    break;
                                case 'image_width':
                                   	if ($image.scale_proportion_locked) {
                                        var image_height = Math.round($image.height() * value / $image.width());
                                        $image.height(image_height);
                                        this.notify('image_height', image_height);
                                    }
                                    $image.width(value);
                                    $zoom_widget.slider('value', value);
                                    break;
                                case 'image_height':
                                    if ($image.scale_proportion_locked) {
                                        var image_width = Math.round($image.width() * value / $image.height());
                                        $image.width(image_width);
                                        this.notify('image_width', image_width);
                                        $zoom_widget.slider('value', image_width);
                                    }
                                    $image.height(value);
                                    break;
                            }
                            this.notify(that, value);
                        }
                    }
                });
                //alert($crop.position().left - $image.position().left);
                //                var test = $crop.css('left', value + $image.position().left);
                //                alert(test);
				
				
				
				//alert($viewport.$image.originalWidth);
			    //alert($viewport.$image.originalHeight);
                //alert($('#txtCropX').val());
                //alert($('#txtCropY').val());
                //alert($crop.width());
                //alert($crop.height());
                
				
				
				// Hide the loading notice
                $loading.hide();

                // Finally display the image
                $image.show();

                // Trigger the viewport_onload callback
                if ($.isFunction(settings.viewport_onload)) {
                    settings.viewport_onload.call($viewport);
                    $viewport.observator.notify_all();
                }
            };

            // When an image is using an src image "data" URL scheme then it appear 
            // that the image load event never get fired. Then fire directly
            // image_load_handler() in that case.
            var src = $image.attr('src');
            if (/^data:image/.test(src)) {
                image_load_handler();
            }
            else {
                src = src + (src.search(/\?/) < 0 ? '?' : '&') + 'jracrandom=' + (new Date()).getTime();
                $('<img>').attr('src', src).load(image_load_handler);
            }
        });
    };
})(jQuery);
