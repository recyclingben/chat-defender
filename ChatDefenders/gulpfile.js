/// <binding ProjectOpened='watch' />
const gulp = require("gulp"),
        uglifyJs = require("gulp-uglify-es").default,
        uglifyCss = require("gulp-uglifycss"),
        rename = require("gulp-rename");

gulp.task('copy-modules', function () {
    return gulp.src('node_modules/requirejs/require.js')
        .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('minifyJs', function () {
    // takes all .js files that weren't already minified
    return gulp.src(['wwwroot/js/*.js', '!wwwroot/js/*.min.js'])
        .pipe(uglifyJs())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/js'));
});
gulp.task('minifyCss', function () {
    // minify all js files and add '.min' suffix
    return gulp.src(['wwwroot/css/*.css', '!wwwroot/css/*.min.css'])
        .pipe(uglifyCss())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/css'));
});

// watch all needed tasks
gulp.task('watch', function () {
    gulp.watch('wwwroot/js/*.js', ['minifyJs']);
    gulp.watch('wwwroot/css/*.css', ['minifyCss']);
});