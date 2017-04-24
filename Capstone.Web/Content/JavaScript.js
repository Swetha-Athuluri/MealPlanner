

    $(document).ready(function () {
        var max_fields = 15; //maximum input boxes allowed
        var wrapper = $(".input_fields_wrap"); //Fields wrapper
        var add_button = $(".add_field_button"); //Add button ID
        var counter = $(".input_fields_wrap input").length-1;

        var x = 1; //initlal text box count
        $(add_button).click(function (e) { //on add input button click
            e.preventDefault();
            if (x < max_fields) { //max input box allowed
                x++; //text box increment
                counter++;
                $(wrapper).append('<div><input class="form-control form-control-lg" type="text" data-id=' + counter + ' name="QuantityMeasurementIngredient[' + counter + ']"/><a href="#" class="remove_field">Remove</a></div>'); //add input box
                $(`input[name='QuantityMeasurementIngredient[${counter}]']`).focus();
                
            }
        });

        $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
            //get the id of the one you are removing and then update the other IDs corresponding. 
            var ingredientInput = $(this).prev();
            var ingredientId = ingredientInput.attr('data-id');
            var containerDiv = $(this).parent();
            var siblings = containerDiv.nextAll();
          
            $.each(siblings, function (index, sibling) {
                var input = $(sibling).children('input');
                var existingId = input.attr('data-id');
                existingId--;
                input.attr('data-id', existingId);
                input.attr('name', 'QuantityMeasurementIngredient[' + existingId + ']');
            });
            e.preventDefault();
            containerDiv.remove();
            x--;

        })
    });
    $(document).ready(function () {
        var max_fields = 10; //maximum input boxes allowed
        var wrapper = $(".checkbox_fields_wrap"); //Fields wrapper
        var add_button = $(".add_checkbox_button"); //Add button ID
        

        
        $(add_button).click(function (e) { //on add input button click
            var numberOfExistingFields = $(".meal_recipe_input").length;

            e.preventDefault();
            if (numberOfExistingFields < max_fields) { //max input box allowed
                var lastInput = $(".meal_recipe_input").last();
                var newInput = lastInput.clone();
                lastInput.after(newInput);

                // Reset to the first item
                newInput.children("select")[0].selectedIndex = 0;
                newInput.children("select")[1].selectedIndex = 0;
                
            }
        });

        $("#modify-meal, #create-meal").on("click", ".remove_checkbox", function (e) {
            e.preventDefault();
            var recipeToRemove = $(this).parent();

            if (recipeToRemove.siblings(".meal_recipe_input").length > 0) {
                recipeToRemove.remove();
            }
        });


        $(wrapper).on("click", ".remove_checkbox", function (e) { //user click on remove text
            //get the id of the one you are removing and then update the other IDs corresponding. 
            var ingredientInput = $(this).prev();
            var ingredientId = ingredientInput.attr('data-id');
            var containerDiv = $(this).parent();
            var siblings = containerDiv.nextAll();

            $.each(siblings, function (index, sibling) {
                var input = $(sibling).children('div');
                var existingId = input.attr('data-id');
                existingId--;
                input.attr('data-id', existingId);
                input.attr('name', 'RecipeMealType[' + existingId + ']');
            });
            e.preventDefault();
            containerDiv.remove();
            x--;

        })
    });
    
  
  