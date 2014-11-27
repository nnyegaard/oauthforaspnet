var gulp = require('gulp'),
	concat = require('gulp-concat'),
	minifyCss = require('gulp-minify-css'),
	uglify = require('gulp-uglify'),
	sass = require('gulp-sass'),
	replace = require('gulp-replace'),
	size = require('gulp-size'),
	streamqueue = require('streamqueue')
	;

// Do them stylesheets
gulp.task('css', function() {
	var appStyles = gulp.src('_source/sass/index.scss')
		.pipe(sass());

	var libraryStyles = gulp.src([
		'bower_components/materialize/bin/materialize.css',
		'bower_components/pygments/css/default.css'
		]);

	return streamqueue({ objectMode: true }, libraryStyles, appStyles)
		.pipe(concat('styles.css'))
		.pipe(replace('../font/roboto/', '../fonts/'))
		.pipe(replace('../font/material-design-icons/', '../fonts/'))
		.pipe(size({ title: "css before minification" }))
		//.pipe(minifyCss())
		.pipe(size({ title: "css after minfication" }))
		.pipe(gulp.dest('css'))
		;

});

// Javascript
gulp.task('js', function() {
	return gulp.src([
		'bower_components/jquery/dist/jquery.js',
		'bower_components/mixitup/build/jquery.mixitup.min.js',
		'bower_components/materialize/bin/materialize.js',
		'_source/js/site.js'
		])
		.pipe(concat('scripts.js'))
		.pipe(size({ title: "js before minification" }))
		//.pipe(uglify())
		.pipe(size({ title: "js after minification" }))
		.pipe(gulp.dest('js'));
});

// Move the fonts across...
gulp.task('fonts', function() {
	return gulp.src([
		'bower_components/materialize/font/material-design-icons/*',
		'bower_components/materialize/font/roboto/*'
		])
		.pipe(gulp.dest('fonts'));
});

// Default task
gulp.task('default', function() {
  gulp.start(['css', 'js', 'fonts']);
});