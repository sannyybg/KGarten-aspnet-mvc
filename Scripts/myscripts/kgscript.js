$(function () {
    $('.lessonaddbtn').click(function () {
        var lessonid = $(this).attr('id');

        $.post("/Student/AddLessonToTable", { "id": lessonid }, function (data) {

            $('#addlessoninfo').html(data.Message);

        });

    });
});