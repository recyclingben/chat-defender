/// <binding BeforeBuild='copy-modules' ProjectOpened='copy-modules' />
var gulp = require("gulp");

gulp.task('copy-modules', function () {
    gulp.src('node_modules/requirejs/require.js')
        .pipe(gulp.dest('wwwroot/js'));
});