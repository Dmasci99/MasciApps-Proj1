/**
* Author : Daniel Masci - 200299037
* Class : Enterprise Computing
* Semester : 4
* Professor : Tom Tsiliopolous
* Purpose : Project 1 - Game Tracker
* Website Name : MasciApps-GameTracker.azurewebsites.net
*/

/**
* The following file is a Javascript file dedicated to the Game Tracker App.
* It includes all functionality for public and private Game Tracker pages.
*/

jQuery(document).ready(function ($) {

    //Show calendar when input is clicked - Games.aspx
    $('.calendar-trigger').click(function () {
        $('.GameCalendar').toggleClass('active');
    });

    //Change background color of the current week of the calendar - Games.aspx
    $('.SelectedWeek').parent('tr').addClass('SelectedWeek');

    //Allow user to close a Validator Error Message by clicking on it - Forms(Register/Login/Profile)
    $('.input-container span').click(function () {
        $(this).css('visibility', 'hidden');
    });

    //If user isn't logged in - hide edit buttons on Games.aspx
    if ($('.gametracker-page').length > 0)
        if ($('.menu.loggedin').length == 0) 
            $('.Edit').css('display', 'none');
    
    //Give user prompt when trying to delete a Team/Game
    $('.gametracker-page .Delete').click(function () {
        return confirm("Are you sure you want to delete this record?");
    });

});