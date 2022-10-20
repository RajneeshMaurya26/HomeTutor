
    $(document).ready(function () {
        var marks = 0;
        $("#btnsave").click(function () {
            var spOpen, spClose, msg, txt, x, status = true;
            spOpen = "<span class='err'>";
            spClose = "</span>";
            $(".err").remove(); //to remove all line msg...
            // To validate name...
            txt = $("#name").val().trim();
            if (txt.length == 0) {
                msg = "Please Enter Your Name";
                status = false;
                $("#name").after(spOpen + msg + spClose);
            }
            else if (txt.length < 2) {
                msg = "Please Enter Your Valid Name";
                status = false;
                $("#name").after(spOpen + msg + spClose);
            }
            // To validate Address
            txt = $("#address").val().trim();
            if (txt.length == 0) {
                msg = "Please Enter Your Address";
                status = false;
                $("#address").after(spOpen + msg + spClose);
            }
            else if (txt.length < 3) {
                msg = "Please Enter Your Valid Address";
                status = false;
                $("#address").after(spOpen + msg + spClose);
            }
            // To Validate Subject DDL..
            txt = $("#subjects").val().trim();
            if (txt.length == 0) {
                msg = "Please Select Subject";
                status = false;
                $("#subjects").after(spOpen + msg + spClose);
            }
            // To Validate City DDL..
            txt = $("#city1").val().trim();
            if (txt.length == 0) {
                msg = "Please Select City";
                status = false;
                $("#city1").after(spOpen + msg + spClose);
            }
            // To Validate Qualification DDL..
            txt = $("#qualification").val().trim();
            if (txt.length == 0) {
                msg = "Please Select Your Qualification";
                status = false;
                $("#qualification").after(spOpen + msg + spClose);
            }
            // To Validate Experience DDL..
            txt = $("#exp").val().trim();
            if (txt.length == 0) {
                msg = "Please Select Your Experience Year.";
                status = false;
                $("#exp").after(spOpen + msg + spClose);
            }
            //to Validate mob no..
            txt = $("#number").val().trim();
            if (txt.length == 0) {
                msg = "Please Enter Mobile number";
                status = false;
                $("#number").after(spOpen + msg + spClose);
            }
            else if (txt.length == 10 || txt.length == 12) {
                var first = txt.length == 12 ? txt.charAt(2) : txt.charAt(0);
                if (txt.length == 12) {
                    if (txt.substr(0, 2) != '91') {
                        msg = "Invalid Mobile Number";
                        status = false;
                        $("#number").after(spOpen + msg + spClose);
                    }
                }
                // To Check first Digit
                if (status == true)
                    if (first < 6 || first > 9) {
                        msg = "Invalid Indian Mobile Number";
                        status = false;
                        $("#number").after(spOpen + msg + spClose);
                    }
            }
            else {
                msg = "Invalid Mobile Number";
                status = false;
                $("#number").after(spOpen + msg + spClose);
            }

            // To validate email
            var em = $("#email").val().trim();
            if (em.length == 0) {
                msg = "<span class='err'>Please enter email id.</span>";
                status = false;
            }
            else {
                // Check for
                var s = em.indexOf('@');
                if (s <= 0 || (em.length - s) < 5) {
                    msg = "<span class='err'>Invalid email id.</span>";
                    status = false;

                }
                // Check for .
                s = em.indexOf('.');
                if (s <= 0 || (em.length - s) < 3) {
                    msg = "<span class='err'>Invalid email id.</span>";
                    status = false;
                }
            }
            if (msg.length > 0)
                $("#email").after(msg);
            // To validate file Upload
            var myfile = $("#photo");
            if (myfile.val().length > 1)
            {
                var fname = myfile.val();
                fname = fname.substr(fname.lastIndexOf("\\") + 1);
                var ext = fname.substr(fname.lastIndexOf(".") + 1).toUpperCase();
                if (ext == "JPG" || ext == "PNG" || ext == "JPEG") {
                    // To get file size in bytes..
                    var byteSize = myfile[0].files[0].size;
                    var kSize = byteSize / 1024;
                    if (kSize > 2048) {
                        msg = "File Size To Large";
                        status = false;
                        $("#photo").after(spOpen + msg + spClose);
                    }
                }
                else {
                    msg = "Invalid Image type (Choose like jpg, png, jpeg)";
                    status = false;
                    $("#photo").after(spOpen + msg + spClose);
                }
            }
            else {
                msg = "Please Select an Image";
                status = false;
                $("#photo").after(spOpen + msg + spClose);
            }


            

            // To check password && confirm password
            var p = $("#pass").val().trim();
            if (p.length == 0)
            {
                msg = "Please Enter Password.";
                status = false;
                $("#pass").after(spOpen + msg + spClose);
            }
            else if (marks < 100)
            {
                status = false;
            }

            //confirm password
            var cp = $("#cpass").val().trim();
            if (cp.length == 0) {
                msg = "Please re-enter your password.";
                status = false;
                $("#cpass").after(spOpen + msg + spClose);
            }
            else if (p != cp) {
                msg = "Password and confirm password must be same.";
                status = false;
                $("#cpass").after(spOpen + msg + spClose);
            }
            // To validate password Strength
            $("#pass").keyup(function () {
                var ps = $(this).val().trim();
                $(".ps").remove();
                var upper = false, lower = false, digit = false, special = false, res = "";
                marks = 0;
                $(this).css("border-bottom", "1px solid red");
                if (ps.length >= 8) {
                    marks = 20;
                    for (x = 0; x < ps.length; x++) {
                        if (ps.charCodeAt(x) >= 65 && ps.charCodeAt(x) <= 90) {
                            marks = marks + 20;
                            upper = true;
                            break;
                        }
                    }
                    for (x = 0; x < ps.length; x++) {
                        if (ps.charCodeAt(x) >= 97 && ps.charCodeAt(x) <= 122) {
                            marks = marks + 20;
                            lower = true;
                            break;
                        }
                    }
                    for (x = 0; x < ps.length; x++) {
                        if (ps.charCodeAt(x) >= 48 && ps.charCodeAt(x) <= 57) {
                            marks = marks + 20;
                            digit = true;
                            break;
                        }
                    }
                    for (x = 0; x < ps.length; x++) {
                        if ((ps.charCodeAt(x) >= 58 && ps.charCodeAt(x) <= 64) || (ps.charCodeAt(x) >= 91 && ps.charCodeAt(x) <= 96)) {
                            marks = marks + 20;
                            special = true;
                            break;
                        }
                    }
                }
                else if (ps.length == 0) {
                    res = "Please enter password.";
                    $("#sppass").text(res);
                }
                else {
                    res = "Password must have minimum 8 characters.";
                    $("#sppass").text(res);
                }
                if (marks < 100 && marks > 0) {
                    res = "Password must contain ";
                    res += upper == false ? "an Uppercase character " : "";
                    res += lower == false ? "a Lowercase character " : "";
                    res += digit == false ? "a Digit " : "";
                    res += special == false ? "a Special symbol." : "";
                    $("#sppass").text(res);
                }
                else if (marks >= 100) {
                    $("#sppass").text("");
                }

            });
            return status;
            if (status == true)
            {
                $("#form").submit();
            }
            

        });

    });
