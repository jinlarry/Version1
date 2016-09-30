$(function() {
    var globalinfo, globaluserid; var GobalIsCreateCleanPath = false; var  globalcolor ; var globalroutedragble = true;
	$.fn.googleMap = function(params) {
		params = $.extend( {
			zoom : 10,
			coords : [48.895651, 2.290569],
			type : "ROADMAP",
			debug : false,
			langage : "english",
			overviewMapControl: false,
			streetViewControl: false,
			scrollwheel: false,
			mapTypeControl: false
		}, params);
	
		switch(params.type) {
			case 'ROADMAP':
			case 'SATELLITE':
			case 'HYBRID':
			case 'TERRAIN':
				params.type = google.maps.MapTypeId[params.type];
				break;
			default:
				params.type = google.maps.MapTypeId.ROADMAP;
				break;
		}

		this.each(function() {

			var map = new google.maps.Map(this, {
				zoom: params.zoom,
				center: new google.maps.LatLng(params.coords[0], params.coords[1]),
				mapTypeId: params.type,
				scrollwheel: params.scrollwheel,
				streetViewControl: params.streetViewControl,
				overviewMapControl: params.overviewMapControl,
				mapTypeControl: params.mapTypeControl

			});

			$(this).data('googleMap', map);
			$(this).data('googleLang', params.langage);
			$(this).data('googleDebug', params.debug);
			$(this).data('googleMarker', new Array());
			$(this).data('googleBound', new google.maps.LatLngBounds());		 
		});

		return this;
	}
	
	$.fn.addMarker = function (params) {
	     
		params = $.extend( {
			coords : false,
			address : false,
			url : false,
			id : false,
			icon : false,
			draggable : false,
			title : "",
			text : "",
			success : function() {}
		}, params);

		this.each(function() {
			$this = $(this);

			if(!$this.data('googleMap')) {
				if($this.data('googleDebug'))
					console.error("jQuery googleMap : Unable to add a marker where there is no map !");
					
				return false;
			}

			if(!params.coords && !params.address) {
				if($this.data('googleDebug'))
					console.error("jQuery googleMap : Unable to add a marker if you don't tell us where !");
					
				return false;
			}
			 
			if(params.address && typeof params.address == "string") {
				var geocodeAsync = function($that) {
					var geocoder = new google.maps.Geocoder();
					geocoder.geocode({
						address : params.address,
						bounds : $that.data('googleBound'),
						language : $that.data('googleLang')
					}, function(results, status) {
					    
						if (status == google.maps.GeocoderStatus.OK) {
							$that.data('googleBound').extend(results[0].geometry.location);

							if(params.icon) {
								var marker = new google.maps.Marker({
									map: $this.data('googleMap'),
									position: results[0].geometry.location,
									title: params.title,
									icon: params.icon,
									draggable: params.draggable
								});
							} else {
								var marker = new google.maps.Marker({
									map: $that.data('googleMap'),
									position: results[0].geometry.location,
									title: params.title,
									draggable: params.draggable
								});
							}

							if (params.draggable) {
							    
								google.maps.event.addListener(marker, 'dragend', function() {
									var location = marker.getPosition();
								
									var coords = {};

									coords.lat = location.lat();
									coords.lon = location.lng();

									params.success(coords, $this);
								});
							}
							
							if(params.title != "" && params.text != "" && !params.url) {
								var infowindow = new google.maps.InfoWindow({
									content: "<h1>"+params.title+"</h1>"+params.text
								});

								var map = $that.data('googleMap');

								google.maps.event.addListener(marker, 'click', function() {
								     infowindow.open(map, marker);
								});
							} else if(params.url) {
								google.maps.event.addListener(marker, 'click', function() {
								     document.location = params.url;
								});
							}

							if (!params.id) {
							    
								$that.data('googleMarker').push(marker);
							} else {
								$that.data('googleMarker')[params.id] = marker;
							}

							if($that.data('googleMarker').length == 1) {
								$that.data('googleMap').setCenter(results[0].geometry.location);
								$that.data('googleMap').setZoom($that.data('googleMap').getZoom());
							} else {
								$that.data('googleMap').fitBounds($that.data('googleBound'));
							}

							var coords = {};
							coords.lat = results[0].geometry.location.lat();
							coords.lon = results[0].geometry.location.lng();

							params.success(coords, $this);

						} else {
						  
							if($this.data('googleDebug'))
								console.error("jQuery googleMap : Unable to find the place asked for the marker ("+status+")");
						}
					});
				}($this);
			} else {
			   
				$this.data('googleBound').extend(new google.maps.LatLng(params.coords[0], params.coords[1]));

        			if(params.icon) {
					var marker = new google.maps.Marker({
						map: $this.data('googleMap'),
						position: new google.maps.LatLng(params.coords[0], params.coords[1]),
						title: params.title,
						icon: params.icon,
						draggable: params.draggable
					});
				} else {
					var marker = new google.maps.Marker({
						map: $this.data('googleMap'),
						position: new google.maps.LatLng(params.coords[0], params.coords[1]),
						title: params.title,
						draggable: params.draggable
					});
				}

        			if(params.title != "" && params.text != "" && !params.url) {
          				var infowindow = new google.maps.InfoWindow({
						content: "<h1>"+params.title+"</h1>"+params.text
					});

					var map = $this.data('googleMap');

					google.maps.event.addListener(marker, 'click', function () {
					 
		        			infowindow.open(map, marker);
	        			});
				} else if(params.url) {
				    google.maps.event.addListener(marker, 'click', function () {				       
              					document.location = params.url;
        				});
				}

        			if (params.draggable) {
        			google.maps.event.addListener(marker, 'click', function () {
        			    // alert(marker.position);
        			    });
					google.maps.event.addListener(marker, 'dragend', function() {
						var location = marker.getPosition();

						var coords = {};

						coords.lat = location.lat();
						coords.lon = location.lng();

						params.success(coords, $this);
					});
				}

				if(!params.id) {
       					$this.data('googleMarker').push(marker);
        			} else {
        				$this.data('googleMarker')[params.id] = marker;
        			}

				if($this.data('googleMarker').length == 1) {
					$this.data('googleMap').setCenter(new google.maps.LatLng(params.coords[0], params.coords[1]));
					$this.data('googleMap').setZoom($this.data('googleMap').getZoom());
				} else {
					$this.data('googleMap').fitBounds($this.data('googleBound'));
				}

				params.success({
					lat: params.coords[0],
					lon: params.coords[1]
				}, $this);
			}
		});

		return this;
	}

	$.fn.removeMarker = function(id) {
		this.each(function() {
			var $this = $(this);

    			if(!$this.data('googleMap')) {
    				if($this.data('googleDebug'))
      					console.log("jQuery googleMap : Unable to delete a marker where there is no map !");
      					
      				return false;
    			}

    			var $markers = $this.data('googleMarker');

    			if(typeof $markers[id] != 'undefined') {
    				$markers[id].setMap(null);
    				
      				if($this.data('googleDebug'))
      					console.log('jQuery googleMap : marker deleted');
      					
      				return true;
    			} else {
      				if($this.data('googleDebug'))
      					console.error("jQuery googleMap : Unable to delete a marker if it not exists !");
      		
      				return false;
    			}
		});
	}

	$.fn.addWay = function (params) {   
	    params = $.extend( {
	        start : false,
	        end : false,
	        step: [],
	        route : false,
	        langage: 'english',
	        username: false,
	        Createdate: false,
	        routelogo: false,
	        route_id: false,
	        CreateUserID:false
	    }, params);
	    
	    var direction = new google.maps.DirectionsService({
	        region: "fr"
	    });

	    var way = new google.maps.DirectionsRenderer({ 
	        draggable: globalroutedragble, 
	        map: $(this).data('googleMap'),
	        panel: document.getElementById(params.route),
	        provideTripAlternatives: true,suppressMarkers : true,
	        polylineOptions: {
	            strokeColor: globalcolor,
	            strokeOpacity: 3.0,
	            strokeWeight: 5 
	        }
	    });
		
	    document.getElementById.innerHTML = "";

	    var waypoints = [];

	    for(var i in params.step) {
	        var step;
	        if(typeof params.step[i] == "object") {
	            step = new google.maps.LatLng(params.step[i][0], params.step[i][1]);
	        } else {
	            step = params.step[i];
	        }

	        waypoints.push({
	            location: step,
	            stopover: true
	        });
	    }  
 
	    if (typeof params.end != "object") {
	        var geocodeAsync = function ($that) {
	            var geocoder = new google.maps.Geocoder();

	            geocoder.geocode({
	                address: params.end,
	                bounds: $that.data('googleBound'),
	                language: params.langage
	            }, function (results, status) {

	                if (status == google.maps.GeocoderStatus.OK) {
	                    var request = {
	                        origin: params.start,
	                        destination: results[0].geometry.location,
	                        travelMode: google.maps.DirectionsTravelMode.WALKING,
	                        region: "fr",
	                        waypoints: waypoints
	                    };
	                   
	                    direction.route(request, function (response, status) {

	                        if (status == google.maps.DirectionsStatus.OK) {	                           	                           
	                            way.setDirections(response);
	           
	                            //google.maps.event.addListener(way, 'mouseover', function () {
	                            //    alert("moused over straight line!");
	                            //});
	                        } else {
	                            if ($that.data('googleDebug'))
	                                console.error("jQuery googleMap : Unable to find the place asked for the route (" + response + ")");
	                        }
	                    });

	                } else {
	                    if ($that.data('googleDebug'))
	                        console.error("jQuery googleMap : Address not found");
	                }
	            });
	        }($(this));
	    } else {
	        var request = {
	            origin: params.start,
	            destination: new google.maps.LatLng(params.end[0], params.end[1]),
	            travelMode: google.maps.DirectionsTravelMode.DRIVING,
	            region: "fr",
	            waypoints: waypoints
	        };

	        direction.route(request, function (response, status) {
	            if (status == google.maps.DirectionsStatus.OK) {
	                
	                way.setDirections(response);
	                
	                if (!GobalIsCreateCleanPath) {
	                   
	                    showSteps(response,params.username, params.Createdate, params.routelogo, params.route_id, params.CreateUserID);
	                }	               
	            } else {
	                if ($(this).data('googleDebug'))
	                    console.error("jQuery googleMap : Address not found");
	            }
	        });
	    }
		return this;
	}
    
	$.fn.GetMapCurrentPosition = function () {
	    var aa = [];
	    aa.push($this.data('googleMap').getCenter().lat());
	    aa.push($this.data('googleMap').getCenter().lng());
	    return aa;
	}

	$.fn.DrawPathWithPolyline = function (params) {
	    params  = $.extend({ start: false, end: false, step: [] }, params);
	    var flightPlanCoordinates = []; 
	    var oppo = { lat: params.start[0], lng: params.start[1] };	 
	    flightPlanCoordinates.push(oppo);
	  
	    for (var i = 0; i < params.step.length; i++) {
	         var nono =   { lat: '', lng: '' };
	         nono.lat = params.step[i][0]; nono.lng = params.step[i][1];
	         flightPlanCoordinates.push(nono);
	    }
	    var eoeo = { lat: params.end[0], lng: params.end[1] };	 
	    flightPlanCoordinates.push(eoeo);	                
	                var flightPath = new google.maps.Polyline({
	                    path: flightPlanCoordinates,
	                    strokeColor: globalcolor,
	                    strokeOpacity:4.0,
	                    strokeWeight: 5
	                });	               
	  flightPath.setMap($this.data('googleMap'));
	  google.maps.event.addListener(flightPath, 'mouseover', function () {
	                    alert("moused over straight line!");
	                });
	    return this;
	}

	$.fn.GetRoutePointList = function () {
	    var $markers = $this.data('googleMarker'); var points = new Array();
	   // alert("markers:" + $markers.length);
	    for (var i = 0; i < $markers.length; i++) {
	        if ($markers[i].title == "StartPoint") {	            
	            points.push({ PostalAddress: globalinfo, lat: $markers[i].position.lat(), lon: $markers[i].position.lng(), Type: 'STARTPOINT', Title: '', Text: '' });
	        }
	        if ($markers[i].title == "DestinationPoint") {
	            points.push({ PostalAddress: '', lat: $markers[i].position.lat(), lon: $markers[i].position.lng(), Type: 'ENDPOINT', Title: '', Text: '' });
	        }
	        if ($markers[i].title == "StepPoint") {
	            points.push({ PostalAddress: '', lat: $markers[i].position.lat(), lon: $markers[i].position.lng(), Type: 'STEPPOINT', Title: '', Text: '' });
	        }
	    }
	    return points;
	}
	 
    //set brush color of drawing route 
	$.fn.SetRouteStrokeColor = function (color) { globalcolor = color; return this; }
	$.fn.SetGobalIsCreateCleanPath = function (ttt) { GobalIsCreateCleanPath = ttt; return this; }
	$.fn.SetRoute_Dragable = function (drag) { globalroutedragble = drag; return this; }
	$.fn.SetUserID = function (userid) { globaluserid = userid; return this; }
	$.fn.CreateCleanPath = function (color,userid) {
	    globalcolor = color; globaluserid = userid;
	    var startpoint_lat = "", startpoint_lng = ""; START_POINT = ""; FINAL_lng = ""; FINAL_lat = ""; StepPoint = [];
	    var $markers = $this.data('googleMarker'); 
	    for (var i = 0; i < $markers.length; i++) {	       
	        if ($markers[i].title == "StartPoint") {
	            startpoint_lat = $markers[i].position.lat(); startpoint_lng = $markers[i].position.lng();
	            START_POINT = startpoint_lat + ',' + startpoint_lng;
	        }
	        if ($markers[i].title == "DestinationPoint") {
	            FINAL_lat = $markers[i].position.lat(); FINAL_lng = $markers[i].position.lng();
	        }
	        if ($markers[i].title == "StepPoint") {	            
	            
	            var coords = [$markers[i].position.lat(), $markers[i].position.lng()];
	            
	            StepPoint.push(coords); 
	        }
	    }
	    
	    
	    geocodeLatLng(START_POINT);//gain postal address, the process is asynchronous,so we use setTimeout function to wait result 
	    var that = this;
	    setTimeout(function () {
 
	        that.addWay({
	            start: globalinfo, // postal address for the start marker (obligatory)
	            end: [FINAL_lat, FINAL_lng], // postal address or gps coordinates for the end marker (obligatory)
	            route: 'way', // Block's ID for the route display (optional)
	            langage: 'english', // language of the route detail (optional)
	            step: StepPoint
	        });
	    }, 1000);
	    
	   
	    //return this;
	}
	 

	function geocodeLatLng(position) {
	    var input = position;
	    var latlngStr = input.split(',', 2);
	    var latlng = { lat: parseFloat(latlngStr[0]), lng: parseFloat(latlngStr[1]) };
	    var geocoder = new google.maps.Geocoder;
	    var infocontent = "";
	           geocoder.geocode({ 'location': latlng }, function (results, status)
                                                        {                   
	                                                        if (status === google.maps.GeocoderStatus.OK)
	                                                        {
	                                                            if (results[1])
	                                                            {
	                                                                infocontent = results[0].formatted_address;
	                                                                initContinued(infocontent);
	                                                            } else { infocontent= 'No results found' ;}
	                                                        } else { infocontent = 'Geocoder failed due to: ' + status; }
	                 
                                                         } 
              
               );
	}
	function initContinued(addr) {
	    globalinfo = addr;
	}
	function showSteps(directionResult, usernametext, routeCreateTime, routelogo, route_id, CreateUserID) {
	   
	    var markerArray = [];
	    var myRoute = directionResult.routes[0].legs[0]; 
	    stepDisplay = new google.maps.InfoWindow();
	   
	    var icon = "images/GoogleMap/" + routelogo;
	    
	    var marker = new google.maps.Marker({
	        position: myRoute.steps[0].start_point,	        
	        map: $this.data('googleMap'),
	        icon: icon,
	        RouteID: route_id,
	        CreateUserID: CreateUserID
	    });
	   
	    google.maps.event.addListener(marker, 'rightclick', function () {	        
	        var pp = "";
	        pp = String(globaluserid) + ',' + String(marker.RouteID);
	        if (globaluserid != CreateUserID) {
	            $.alert({
	                title: 'Message!',
	                content: "You cannot delete another person's cleaning route!",
	                confirm: function (response) {
	                    return;
	                }
	            });
	        } else {
	            $.confirm({
	                title: 'Deleting Confirm!',
	                content: 'Are you sure to delete the route?',
	                confirm: function () {
	                    CallDeleteRouteAjax(pp);
	                },
	                cancel: function () {
	                }
	            });
	        }
	        
	       
	    });

	    attachInstructionText(marker, usernametext);
	    markerArray.push(marker);
	    var pp = directionResult.routes[0].legs[directionResult.routes[0].legs.length - 1];
	    var icon = "images/GoogleMap/EndMarker.png";
	    var markerend = new google.maps.Marker({
	        position: pp.steps[pp.steps.length - 1].end_point,
	        map: $this.data('googleMap'),
	        icon: icon
	    });	    
	    attachInstructionText(markerend, "The route was claimed at " +  routeCreateTime + ".<br>"+"From " + directionResult.routes[0].legs[0].start_address + "<br> to " + pp.end_address);
	    markerArray.push(markerend);
	    google.maps.event.trigger(markerArray[0], "click");
	}

	function CallDeleteRouteAjax(data) {
	    var DeleteRouteData = {
	        RouteToDelete: data
	    }
	    $.ajax({
	        url: '/ZeroRubbish/RouteDelete',
	        type: 'POST',
	        data: DeleteRouteData,
	        dataType: 'html',
	        success: function (response) {
	           // alert(response.responseText);
	        },
	        error: function (response) {
	          //  alert(response.responseText);  
	        }
	    });
	    $.alert({
	        title: 'Message!',
	        content: 'Delete finished!',
	        confirm: function (response) {
	            window.location.reload();
	        }
	    });
 
	}
	function attachInstructionText(marker, text) {
	    google.maps.event.addListener(marker, 'click', function () {
	        // Open an info window when the marker is clicked on,
	        // containing the text of the step.
	        stepDisplay.setContent(text);
	        stepDisplay.open($this.data('googleMap'), marker);
	    });
	}

});
