function postProcessor(postId) {
    $.getJSON("/Home/Json", function (data) {
    })

   .done(function (data) {
       var postIdSorter = [];
       $.each(data, function (key, val) {
           postIdSorter.push(key);
       });
       // place in reverse order so most recent article comes first.
       postIdSorter = postIdSorter.reverse();

       // fill out content of the html page
       $("#postTitle").text(data[postIdSorter[postId]][0]);
       $("#postContent").text(data[postIdSorter[postId]][1]);
       $("#postAuthor").text("Post by: " + data[postIdSorter[postId]][2]);

       // fill out similar article list with links.
       $("#similarArticle1")
           .attr("onclick", "postProcessor(" + postIdSorter.indexOf(String(data[postIdSorter[postId]][3])) + ")")
           .text(data[postIdSorter[postId]][4]);
       $("#similarArticle2")
           .attr("onclick", "postProcessor(" + postIdSorter.indexOf(String(data[postIdSorter[postId]][5])) + ")")
           .text(data[postIdSorter[postId]][6]);
       $("#similarArticle3")
           .attr("onclick", "postProcessor(" + postIdSorter.indexOf(String(data[postIdSorter[postId]][7])) + ")")
           .text(data[postIdSorter[postId]][8]);
   })

   .fail(function () {
       console.log("error getting json data");
   });
}

function enableDisableButtons(currentPostId, numberOfPosts) {

    if (currentPostId == 0) {
        $("#nextBtn").addClass("disabledBtn");
    } else {
        $("#nextBtn").removeClass("disabledBtn");
    }

    if (currentPostId == numberOfPosts) {
        $("#prevBtn").addClass("disabledBtn");
    } else {
        $("#prevBtn").removeClass("disabledBtn");
    }

};



// Validation functions
function validateLogin() {
    var regex = new RegExp(/^[a-zA-Z0-9_.-@@]{4,100}$/);

    $('#usernameField').blur(function () {
        var fieldContent = $("#usernameField").val();
        if (regex.test(fieldContent) == false) {
            $('#usernameFeedback').fadeIn();
        };

    });

    $('#usernameField').focus(function () {
        var fieldContent = $("#usernameField").val();
        if (regex.test(fieldContent) == false) {
            $('#usernameFeedback').fadeOut();
        }

    });

    $('#passwordField').blur(function () {
        var fieldContent = $("#passwordField").val();
        if (regex.test(fieldContent) == false) {
            $('#passwordFeedback').fadeIn();
        };

    });

    $('#passwordField').focus(function () {
        var fieldContent = $("#passwordField").val();
        if (fieldContent == "") {
            $('#passwordFeedback').fadeOut();
        }

    });
};

function validateRegistration() {

    var regex = new RegExp(/^([a-zA-Z0-9!@#$£%^&*()_+\-=\[\]{};':"\\|,.<>\/?]{4,100})+/);
    var emailRegex = new RegExp(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);

    $("#submitButton").prop("disabled", true).prop("opacity", 0.5);

    $('#usernameField').blur(function () {
        var fieldContent = $("#usernameField").val();
        if (regex.test(fieldContent) == false) {
            $('#usernameFeedback').fadeIn();
        };

    });

    $('#usernameField').focus(function () {
        var fieldContent = $("#usernameField").val();
        if (regex.test(fieldContent) == false) {
            $('#usernameFeedback').fadeOut();
        }

    });

    $('#emailField').blur(function () {
        var fieldContent = $("#emailField").val();
        if (regex.test(fieldContent) == false) {
            $('#emailFeedback').fadeIn();
        };

    });

    $('#emailField').blur(function () {
        var fieldContent = $("#emailField").val();
        if (emailRegex.test(fieldContent) == false) {
            $('#emailValidFeedback').fadeIn();
        };

    });

    $('#emailField').focus(function () {
        var fieldContent = $("#emailField").val();
        if (regex.test(fieldContent) == false || emailRegex.test(fieldContent) == false) {
            $('#emailFeedback').fadeOut();emailRegex.test(fieldContent) == false
            $('#emailValidFeedback').fadeOut();
        }

    });

    $('#passwordField').blur(function () {
        var fieldContent = $("#passwordField").val();
        if (regex.test(fieldContent) == false) {
            $('#passwordFeedback').fadeIn();
        };

    });

    $('#passwordField').focus(function () {
        var fieldContent = $("#passwordField").val();
        if (fieldContent == "") {
            $('#passwordFeedback').fadeOut();
        }

    });

    $('#passwordValidationField').blur(function () {
        var passwordField1 = $("#passwordField").val();
        var passwordField2 = $("#passwordValidationField").val();
        if (passwordField1 != passwordField2) {
            $('#passwordValidationFeedback').fadeIn();
        } else {
            $('#passwordValidationFeedback').fadeOut();
        };

    });

    $('#passwordValidationField').focus(function () {
        var passwordField1 = $("#passwordField").val();
        var passwordField2 = $("#passwordValidationField").val();
        if (passwordField1 == passwordField2 && passwordField1 != "") {
            $('#passwordFeedback').fadeOut();
            $('#passwordValidationFeedback').fadeOut();
        }

    });

    $('#passwordField').blur(function () {
        if (regex.test($("#usernameField").val()) == true) {
            $("#submitButton").prop("disabled", false).prop("opacity", 1);
            console.log("Hello");
        }

    });
}


// Testing functions
function confirmJsonRequest() {
    $.getJSON("/Home/Json", function (data) {
        console.log("success");
        console.log(data);
    })
   .done(function (data) {
       console.log("second success");
       $.each(data, function (key, val) {
           console.log(key + " " + val);
       })
   })
   .fail(function () {
       console.log("error");
   })
   .always(function () {
       console.log("complete");
   });
}