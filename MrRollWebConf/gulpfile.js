/// <binding BeforeBuild='default' ProjectOpened='default' />
var gulp = require('gulp');
var cleanCSS = require('gulp-clean-css');
var clean = require('gulp-clean');

var cssmin = require('gulp-cssmin');
var rename = require('gulp-rename');


gulp.task('after-npm-run', function () {
    // https://www.codeproject.com/Articles/1165004/How-to-Use-Gulp-with-ASP-NET-Core-MVC
    gulp.src("node_modules/bootstrap/dist/css/**.*").pipe(gulp.dest('wwwroot/lib/bootstrap/dist/css/'));
    gulp.src("node_modules/bootstrap/dist/fonts/**.*").pipe(gulp.dest('wwwroot/lib/bootstrap/dist/fonts/'));
    gulp.src("node_modules/bootstrap/dist/js/**.*").pipe(gulp.dest('wwwroot/lib/bootstrap/dist/js/'));
    gulp.src("node_modules/bootstrap/js/**.*").pipe(gulp.dest('wwwroot/lib/bootstrap/js/'));
    gulp.src("node_modules/bootstrap/scss/**.*").pipe(gulp.dest('wwwroot/lib/bootstrap/scss/'));

    gulp.src("node_modules/jquery/dist/**.*").pipe(gulp.dest('wwwroot/lib/jquery/dist'));
    gulp.src("node_modules/jqueryui/**.*").pipe(gulp.dest('wwwroot/lib/jqueryui/'));
    gulp.src("node_modules/jqueryui/external/**.*").pipe(gulp.dest('wwwroot/lib/jqueryui/external/'));
    gulp.src("node_modules/jqueryui/images/**.*").pipe(gulp.dest('wwwroot/lib/jqueryui/images/'));
});


gulp.task('mimify', function () {
    gulp.src('wwwroot/css/*.css')
        .pipe(cssmin())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/css/'));
});

gulp.task('clean-css', function () {
    return gulp.src('wwwroot/css/site.min.css', { read: false })
        .pipe(clean());
});
