/// <binding BeforeBuild='copy-modules' ProjectOpened='copy-modules' />
const gulp = require("gulp"),
      uglify = require("gulp-uglify-es").default,
      rename = require("gulp-rename"),
      pump = require("pump");

gulp.task('copy-modules', function () {
    pump([
        gulp.src('node_modules/requirejs/require.js'),
        gulp.dest('wwwroot/js')
    ]);
});

gulp.task('minifyJs', function (cb) {
    pump([
        gulp.src(['wwwroot/js/*.js', '!wwwroot/js/*.min.js']),
        uglify(),
        rename({ suffix: '.min' }),
        gulp.dest('wwwroot/js')
    ], cb);
});
gulp.task('watch', function () {
    gulp.watch('wwwroot/js/*.js', ['minifyJs']); 
});