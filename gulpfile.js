var gulp = require('gulp'),
	concat = require('gulp-concat'),
	minifyCss = require('gulp-minify-css'),
	sass = require('gulp-sass'),
	replace = require('gulp-replace'),
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
		//.pipe(minifyCss())
		.pipe(gulp.dest('css'))
		;

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
  // place code for your default task here
  gulp.start(['css', 'fonts']);
});