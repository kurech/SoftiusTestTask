function DisplayFields() {
    var studentcount = document.getElementById('studentCount');

    if (document.getElementById('messages') != null) {
        document.getElementById('messages').innerHTML = '';
    }
    var div = document.createElement("div");
    div.id = "messages";
    div.style.display = "flex";
    div.style.justifyContent = "center";
    div.style.maxWidth = "100% !important";
    div.style.flexWrap = "wrap";
    studentcount.insertAdjacentElement("afterend", div);

    for (var i = 0; i < studentcount.value; i++) {
        var input = document.createElement('input');
        input.type = 'number';
        input.id = 'maxMessageByStudent' + i;
        input.classList = "messagevalue";

        div.insertAdjacentElement("beforeend", input);
    }

    document.getElementById('btnForSolving').style.display = 'flex';
    document.getElementById('result').innerHTML = '';
};

function TaskSolving() {
    var studentcount = document.getElementById('studentCount').value;
    const messagesValue = [];
    for (var i = 0; i < studentcount; i++) {
        messagesValue.push(document.getElementById(`maxMessageByStudent${i}`).value);
    }

    $.ajax({
        type: "POST",
        url: `/api/Calculating/Solving/${studentcount}`,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(messagesValue),
        success: function (result) {
            console.log(document.getElementById('result'));
            document.getElementById('result').innerHTML = result;
        }
    });
}