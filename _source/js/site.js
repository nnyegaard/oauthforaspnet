(function() {
	$( document ).ready(function() {
		$('#provider-list').mixItUp();


		// $('#provider-list').find('li').filter('h2').each(function() {
		// 	console.log($(this));
		// });

		// $('#provider-list li').each(function() {
		// 	console.log($(this));
		// })

		//var matching = $("li h2:contains('Facebook')")
		//console.log(matching);

    	$('#search').on('input', function() {
			var searchTerm = $('#search').val().toLowerCase();

			if ((searchTerm.length) > 0) {
				// Filter the providers
				var $matching = $();	

				$('#provider-list li').each(function() {
					//$this = $(this);
					
					if ($(this).find('h1').text().toLowerCase().match(searchTerm))
					{
						$matching = $matching.add(this);

						//console.log('match' + $(this));
					} else {
						//console.log('DOES NOT match' + $(this));
					}

					//console.log($(this).find('h1'));
				});

				console.log($matching);

				$('#provider-list').mixItUp('filter', $matching);
			} else {
				$('#provider-list').mixItUp('filter', 'all');
			}

			//$('#provider-list').mixItUp('filter', "li h1:contains('Facebook')");

			//var matching = $("li h2:contains('Facebook')")

			//console.log($('#search').val());
    	});
	});
})();
