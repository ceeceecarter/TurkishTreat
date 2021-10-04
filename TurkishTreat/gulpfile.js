var gulp = require("gulp");
var uglify = require("gulp-uglify");
var concat = require("gulp-concat");
var rename = require("gulp-rename");
var cleanCss = require("gulp-clean-css");

//minify JavaScript
function minify() {
    return gulp.src(["wwwroot/js/**/*.js"])
        .pipe(uglify())
        .pipe(concat("turkishtreat.min.js"))
        .pipe(gulp.dest("wwwroot/dist/"));
}

//minify CSS
function styles() {
    return gulp.src(["wwwroot/css/**/*.css"])
        .pipe(rename("turkishtreat.min.css"))
        .pipe(cleanCss())
        .pipe(gulp.dest("wwwroot/dist/"));
}

exports.minify = minify;
exports.styles = styles;

exports.default = gulp.parallel(minify, styles);