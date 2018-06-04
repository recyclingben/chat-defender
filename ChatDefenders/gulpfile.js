const gulp = require("gulp"),
      uglify = require("gulp-uglify-es").default,
      rename = require("gulp-rename");

gulp.task('copy-modules', function () {
    return gulp.src('node_modules/requirejs/require.js')
        .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('minifyJs', function () {
    
    return gulp.src(['wwwroot/js/*.js', '!wwwroot/js/*.min.js'])
        .pipe(uglify())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/js'));
});
gulp.task('watch', function () {
    gulp.watch('wwwroot/js/*.js', ['minifyJs']); 
});