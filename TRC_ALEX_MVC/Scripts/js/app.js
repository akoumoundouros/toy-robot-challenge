class HomePage {
    constructor() {
        this.txtAreaMaxSize = 29;
        this.line = 0;
        this.lineCount = 0;
        let line = this.line;
        let step = this.step;
        let loadInputFile = this.loadInputFile;
        $(() => {
            homePage.refreshBoard();
            $("#step").click(function () {
                step();
            });
            $("#reset").click(function () {
                homePage.line = 0;
                $.get("/home/reset", function () {
                    homePage.refreshBoard();
                });
            });
            $(".file-names").click(e => loadInputFile(e.currentTarget));
        });
    }
    loadInputFile(e) {
        let name = e.innerHTML;
        $(".file-names").removeClass("list-group-item-primary");
        $(e).addClass(" list-group-item-primary");
        $.get("/input?id=" + name, function (response) {
            $("#commands").val(response);
            this.lineCount = response.split("\n").length > this.txtAreaMaxSize ? this.txtAreaMaxSize : response.split("\n").length;
            $("#commands").prop("rows", this.lineCount + 1);
        });
    }
    step() {
        var cmd = homePage.nextCommand();
        let e = document.getElementById('commands');
        homePage.selectTextareaLine(e, homePage.line);
        if (cmd != undefined) {
            $.get("/home/step?cmd=" + cmd, function (response) {
                homePage.refreshBoard();
            });
        }
    }
    refreshBoard() {
        $.get("/home/play", function (response) {
            $("#board").html(response);
        });
    }
    nextCommand() {
        let txt = $("#commands").val().toString();
        var txtLine = txt.split("\n")[this.line];
        console.log(this.line + ":" + txtLine);
        this.line++;
        return txtLine;
    }
    /// Source from: https://stackoverflow.com/questions/37155642/in-javascript-how-to-highlight-single-line-of-text-in-textarea
    selectTextareaLine(tarea, lineNum) {
        lineNum--; // array starts at 0
        var lines = tarea.value.split("\n");
        // calculate start/end
        var startPos = 0, endPos = tarea.value.length;
        for (var x = 0; x < lines.length; x++) {
            if (x == lineNum) {
                break;
            }
            startPos += (lines[x].length + 1);
        }
        var endPos = lines[lineNum].length + startPos;
        // do selection
        // Chrome / Firefox
        if (typeof (tarea.selectionStart) != "undefined") {
            tarea.focus();
            tarea.selectionStart = startPos;
            tarea.selectionEnd = endPos;
            return true;
        }
        // IE
        //if (document.selection && document.selection.createRange) {
        //    tarea.focus();
        //    tarea.select();
        //    var range = document.selection.createRange();
        //    range.collapse(true);
        //    range.moveEnd("character", endPos);
        //    range.moveStart("character", startPos);
        //    range.select();
        //    return true;
        //}
        return false;
    }
}
let homePage = new HomePage();
//# sourceMappingURL=app.js.map