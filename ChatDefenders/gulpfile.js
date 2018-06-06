﻿/// <binding ProjectOpened='watch' />
const   gulp = require("gulp"),
        uglifyJs = require("gulp-uglify-es").default,
        uglifyCss = require("gulp-uglifycss"),
        rename = require("gulp-rename"),
        sass = require("gulp-sass");

// Copies only needed files from the node_modules folder.
// This  should  be updated  when new modules are  added.
gulp.task('copy:modules', function () {
    return gulp.src('node_modules/requirejs/require.js')
        .pipe(gulp.dest('wwwroot/js'));
});

// Minifies all .js files, excluding all that have already
// been minified to avoid an endless loop.
gulp.task('minify:js', function () {
    return gulp.src(['wwwroot/js/*.js', '!wwwroot/js/*.min.js'])
        .pipe(uglifyJs())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('compile:sass', function () {
    return gulp.src('wwwroot/css/*.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('minify:js', function () {
    // minify all js files and add '.min' suffix
    return gulp.src(['wwwroot/css/*.css', '!wwwroot/css/*.min.css'])
        .pipe(uglifyCss())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/css'));
});

// watch all needed tasks
gulp.task('watch', function () {
    gulp.watch('wwwroot/js/*.js', ['minify:js']);

    gulp.watch('wwwroot/css/*.scss', ['compile:sass'])
    gulp.watch('wwwroot/css/*.css', ['minify:css']);
});