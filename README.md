# Serko_SE_Test_1<br>
Serko Expense New Feature<br>
<b>Scenario<br></b>
Serko Expense has a new requirement to import data from text received via email.<br>
The data will either be:<br>
-Embedded as ‘islands’ of XML-like content<br>
-Marked up using XML style opening and closing tags<br>

REST service that:<br>
• Accepts a block of text<br>
• Extracts the relevant data<br>
• Calculate the GST and total excluding GST based on the extracted <total> (it includes GST)<br>
• Makes the extracted and calculated data available to the service’s client<br>
