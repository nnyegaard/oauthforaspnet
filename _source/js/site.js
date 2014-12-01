(function() {
	$( document ).ready(function() {
		$('#provider-list').mixItUp();

    	$('#search').on('input', function() {
			var searchTerm = $('#search').val().toLowerCase();

			if ((searchTerm.length) > 0) {
				// Filter the providers
				var $matching = $();	

				$('#provider-list li').each(function() {
					//$this = $(this);
					
					if ($(this).find('.provider-name').text().toLowerCase().match(searchTerm))
					{
						$matching = $matching.add(this);
					} 
				});

				$('#provider-list').mixItUp('filter', $matching);
			} else {
				$('#provider-list').mixItUp('filter', 'all');
			}
    	});
	});
})();
