//When document is ready
$(document).ready(function() {
//When button is clicked, do this
$("#MyButton").click(function(){  
//If the age is over 12, do this
    if ($('#age').val() === "over12")
{
//If we choose a, b, c, or d, output corresponding list
    if ($('input[name="ans"]:checked').val() === "a") {
    $("#result").html("<dl><dt>Café Borgia</dt><dd>-Something like Mocha preparation, this drink involves espresso, then hot chocolate, whipped cream and some orange zest at top. Instead of chocolate shavings, orange zest provides some citrus touch to the drink.</dd><dt>White Mocha</dt><dd>-A beverage with espresso, milk, and sugar.</dd></dl>");
    }
     if ($('input[name="ans"]:checked').val() === "b") {
        $("#result").html("<dl><dt>Café Au Lait</dt><dd>-One part coffee, one part steamed milk. It may be served with or without milk foam</dd><dt>Affogato</dt><dd>-Ice cream in a shot of espresso</dd></dl>");
    }
     if ($('input[name="ans"]:checked').val() === "c") {
        $("#result").html("<dl><dt>Café Romano</dt><dd>-A shot of espresso served with a wedge or twist of lemon</dd><dt>Espresso con Panna</dt><dd>-A shot of espresso topped with whipped cream</dd></dl>");
    }
     if ($('input[name="ans"]:checked').val() === "d") {
        $("#result").html("<dl><dt>Café Americano</dt><dd>-Equal parts espresso and hot water. It is similar in consistency to American drip brewed coffee.</dd><dt>Espresso</dt><dd>-A very strong, concentrated coffee made with a dark roasted bean that has been brewed using pressurized steam. One regular shot of espresso is roughly one ounce.</dd></dl>");
    }
}
//If the age is less than 12, do this
else if ($('#age').val() === "less12")
{
//If we choose a, b, c, or d, output corresponding list
    if ($('input[name="ans"]:checked').val() === "a") {
        $("#result").html("<p>You're not old enough to drink coffee. Here is a list of other drinks you may like: </p><br><ul><li>Soda</li><li>Hot Chocolate</li><li>Chocolate Milk</li></ul>");
    }
        if ($('input[name="ans"]:checked').val() === "b") {
            $("#result").html("<p>You're not old enough to drink coffee. Here is a list of other drinks you may like: </p><br><ul><li>Peanut Butter Banana Smoothie</li><li>Blueberry Smoothie</li><li>Oreo Milkshake</li></ul>");
        }
         if ($('input[name="ans"]:checked').val() === "c") {
            $("#result").html("<p>You're not old enough to drink coffee. Here is a list of other drinks you may like: </p><br><ul><li>Orange Creamsicle Smoothie</li><li>Lemonade</li><li>Strawberry Milkshake</li><li>Watermelon Smoothie</li></ul>");
        }
         if ($('input[name="ans"]:checked').val() === "d") {
            $("#result").html("<p>You're not old enough to drink coffee. Here is a list of other drinks you may like: </p><br><ul><li>Milk</li><li>Water</li><li>Vitamin Water</li><li>Apple Juice</li><li>Orange Juice</li></ul>");
        }
}
else 
//Else, error, we need to select something
{
    $("#result").html("<p> Error: You must select all items before submitting. </p>");
}
});
});