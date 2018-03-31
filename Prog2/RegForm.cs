// Program 3
// CIS 199-01/-75
// Due: 4/5/2016
// By: Richard Patrick
// Description: This solution uses Solution 3 to find out the earliest 
// time and day to register but instead of if/else logic for the times, 
// it uses parallel arrays and range matching to assign the correct times. 
// This solution uses a method to contain the loop used for the range matching

// This application calculates the earliest registration date
// and time for an undergraduate student given their credit hours
// and last name.
// Decisions based on UofL Fall/Summer 2016 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }
        // Precondition: Valid last name and hours have been entered 
        // and the arrays containg the ranges for letters and times are matched up
        // Postcondition: Time associated with entered last name is returned 
        private string CalcTime(char lastNameLetter, char[] letterRange, string[] time)
        {
            int index;                                                // Declare counter variable to go through array
            index = letterRange.Length - 1;                           // Assign index to equal length of the array minus 1
            while (index >= 0 && lastNameLetter < letterRange[index]) // While index is greater than or equal to postion 0 
                                                                      // and char is less than the letter in position index
                --index;                                              // Take 1 from index
                return time[index];                                   // Return the time assciated with the entered last name
        } 
        // Precondition: The button to find registration time is clicked
        // Postconditon: The last name and credit hours have been validated
        // and the time and day the user is allowed to register is displayed in a label
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const float SENIOR_HOURS = 90;    // Min hours for Senior
            const float JUNIOR_HOURS = 60;    // Min hours for Junior
            const float SOPHOMORE_HOURS = 30; // Min hours for Soph.

            const string DAY1 = "March 30";  // 1st day of registration
            const string DAY2 = "March 31";  // 2nd day of registration
            const string DAY3 = "April 1";   // 3rd day of registration
            const string DAY4 = "April 4";   // 4th day of registration
            const string DAY5 = "April 5";   // 5th day of registration
            const string DAY6 = "April 6";   // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            float creditHours;        // Entered credit hours
             
          
            if (float.TryParse(creditHrTxt.Text, out creditHours) && creditHours >= 0) // Valid hours
            {
                lastNameStr = lastNameTxt.Text;
                if (lastNameStr.Length > 0) // Empty string?
                {
                    lastNameStr = lastNameStr.ToUpper(); // Ensure upper case
                    lastNameLetterCh = lastNameStr[0];   // First char of last name

                    if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                    {
                        // Juniors and Seniors share same schedule but different days
                        if (creditHours >= JUNIOR_HOURS)
                        {
                            if (creditHours >= SENIOR_HOURS)
                                dateStr = DAY1;
                            else // Must be juniors
                                dateStr = DAY2;

                            char[] letterLowRange = { 'A', 'E', 'J', 'P', 'T' };         // Low end of letter range
                            string[] times = { TIME5, TIME1, TIME2, TIME3, TIME4 };      // Times associated with letter range
                            timeStr = CalcTime( lastNameLetterCh,letterLowRange, times); // Pass arrays and char to method parameter and assign to timeStr                                                                 
                        }
                        // Sophomores and Freshmen
                        else // Must be soph/fresh
                        {
                            if (creditHours >= SOPHOMORE_HOURS)
                            {
                                // E-Q on one day
                                if ((lastNameLetterCh >= 'E') && // >= E and
                                    (lastNameLetterCh <= 'Q'))   // <= Q
                                    dateStr = DAY3;
                                else // All other letters on next day
                                    dateStr = DAY4;
                            }
                            else // must be freshman
                            {
                                // E-Q on one day
                                if ((lastNameLetterCh >= 'E') && // >= E and
                                    (lastNameLetterCh <= 'Q'))   // <= Q
                                    dateStr = DAY5;
                                else // All other letters on next day
                                    dateStr = DAY6;
                            }
                            char[] underLetterLowRange = { 'A', 'C', 'E', 'G', 'J', 'M', 'P', 'R', 'T', 'W' }; // Low end of letter range for underclassmen
                            string[] underTimes = { TIME4, TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2, TIME3}; // Times asscoiated with letter range for underclassmen
                            timeStr = CalcTime(lastNameLetterCh, underLetterLowRange, underTimes); // Pass arrays and char to method parameter and assign to timeStr
                        }
                        // Output results
                        dateTimeLbl.Text = dateStr + " at " + timeStr;
                    }
                    else // First char not a letter
                        MessageBox.Show("Enter valid last name!");
                }
                else // Empty textbox
                    MessageBox.Show("Enter a last name!");
            }
            else // Can't parse credit hours
                MessageBox.Show("Please enter valid credit hours earned!");
        }
    }
}
