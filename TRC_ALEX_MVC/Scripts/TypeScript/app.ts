
class HomePage {

    private txtAreaMaxSize: number = 29;
    private line: number = 0;
    private lineCount: number = 0;
    /// Run on page load and init events
    constructor() {
        let line: number = this.line;
        let step: Function = this.step;
        let loadInputFile: Function = this.loadInputFile;
        
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

            $(".file-names").click(e => loadInputFile(e.currentTarget))
        });
    }
    /// Loads the valid commands from the selected file into the command list
    loadInputFile(e:HTMLUListElement) {
        let name = e.innerHTML;
        $(".file-names").removeClass("list-group-item-primary");
        $(e).addClass(" list-group-item-primary");
        $.get("/input?id=" + name, function (response) {
            $("#commands").val(response);
            this.lineCount = response.split("\n").length > this.txtAreaMaxSize ? this.txtAreaMaxSize : response.split("\n").length;
            $("#commands").prop("rows", this.lineCount + 1);
        });
    }
    /// Runs the next command
    step() {
        var cmd = homePage.nextCommand();
        let e: HTMLTextAreaElement = document.getElementById('commands') as HTMLTextAreaElement;
        homePage.selectTextareaLine(e, homePage.line);
        if (cmd != undefined) {
            $.get("/home/step?cmd=" + cmd, function (response) {
                homePage.refreshBoard();
            });
        }
    }
    /// Used to reload the board in its current state
    refreshBoard() {
        $.get("/home/play", function (response) {
            $("#board").html(response);
        });
    }
    /// Gets the next command from the list
    nextCommand() {
        let txt: string = $("#commands").val().toString();
        var txtLine = txt.split("\n")[this.line];
        console.log(this.line + ":" + txtLine);
        this.line++;
        return txtLine;
    }
    /// Highlights the current line in the command list textarea
    /// Source from: https://stackoverflow.com/questions/37155642/in-javascript-how-to-highlight-single-line-of-text-in-textarea
    selectTextareaLine(tarea: HTMLTextAreaElement, lineNum: number) {
        lineNum--; // array starts at 0
        var lines = tarea.value.split("\n");

        // calculate start/end
        var startPos = 0,
            endPos = tarea.value.length;
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

let homePage: HomePage = new HomePage();