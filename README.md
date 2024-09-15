# ParseCSV2FixedWidth
### This should (I think) run on Linux as well as Windows 10/11.
The program converts a CSV file to a new fixed-width flat file for processing inside MVS3.8j.

It is a console app that takes two arguments/flags.

First arg = /path/to/inputfile.

Second arg = the delimiter (',').
 
Check source code for the variable to change according to your needs.

### To-do: 

Make it autoset the fixed-width according to the longest record in the file (because I am lazy but this could be excessive reads and slow operation).