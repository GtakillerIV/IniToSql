<p><center><img src="http://i1270.photobucket.com/albums/jj602/gtakilleriv/ini2sql.png" alt="ini2sql.png" /></center></p>

<p>Hello everybody!</p>

<p>I'm pretty sure some of you wanted to switch to MySQL but converting the accounts to SQL was your biggest problem.<br />
I had that same problem but luckly I knew how to program so I made a program in C++ that'll convert my .ini files to SQL format and it worked perfectly fine, but it wasn't dynamic.<br />
So I've decided to create this app to help all those people out there that want to switch to MySQL to enjoy all of the features MySQL has to offer.</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">What's this?</span></span></p>

<p>It's a .ini to MySQL converter that'll convert all of your .ini files to multiple MySQL INSERT queries.</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">There's already one!</span></span></p>

<p>Yes, I'm fully aware that there is one that is made by <a href="http://forum.sa-mp.com/member.php?u=79895">gamer931215</a> found <a href="http://forum.sa-mp.com/showthread.php?t=245893">here</a>.</p>

<p>Why didn't I use it you might say?</p>

<p>Because it always crashed/froze when I loaded huge amount of files to it! And sometimes it didn't even work at all.</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">What do I bring to the table?</span></span></p>

<p>My program can take huge amounts of files and convert them(the whole purpose of me creating it), and not only that; I also validate the files(of course it's not gonna remove all the invalid files, but should exclude most!).</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">Okay, I ran out of questions! Just show me the features.</span></span></p>

<p>1)An option to ignore duplicate errors upon executing the INSERT query.<br />
2)Allow you to skip files if their columns don't match the one that I've stored(from the first account file).<br />
3)You can tell the program if your .ini file has the player's username in one of the enteris or not. If there isn't, you can choose a column name for it and it'll get the name of the player automatically for you assuming that the .ini file is named after the player.<br />
4)A popup  telling you that the program has done converting.<br />
5)A progress bar that'll show you the progress.</p>

<p>And I've still got some more ideas in mind that I'll be adding soon.</p>

<p>Note: I have only tested this on a .ini file that was created using <a href="http://forum.sa-mp.com/showthread.php?t=175565">y_ini</a>, but I'm sure that it'll work for other systems like dini, because I take care of the spacing.</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">Screenshots</span></span></p>

<p><img src="http://i1270.photobucket.com/albums/jj602/gtakilleriv/12-1.png" alt="12-1.png" /></p>

<p><img src="http://i1270.photobucket.com/albums/jj602/gtakilleriv/13-1.png" alt="13-1.png" /></p>

<p>Converting 48,796 accounts(excluding the invalid ones).</p>

<p><img src="http://i1270.photobucket.com/albums/jj602/gtakilleriv/14.png" alt="14.png" /></p>

<p>Took me about 33.5 seconds(I'm not using an SSD).</p>

<p>Speed will depend on how good your PC is. Especially the HDD.</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">How to use?</span></span></p>

<p>1)Click on "Browse" and then choose the directory where you have your accounts stored.<br />
2)Change the "Table name" to your table name.<br />
3)Hit "Generate".<br />
4)Once it's done, go back to the directory where you placed the program. You'll see a file named Query.sql. Open it up and you'll see the generated queries that you can use.</p>

<p><strong>Optional</strong><br />
If you don't want to ignore duplicate errors upon executing your MySQL query, untick "Ignore duplicate errors".<br />
If your file doesn't contain the player's username, then untick "File contains username" and enter in a column name for your usernames.</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">Bugs</span></span></p>

<p>None currently.<br />
I'm not the best programmer in the world so there might be a few bugs here and there ;)</p>

<p><span style="font-size: 14ptpx;"><span style="color: orange;">Download</span></span></p>

<p><a href="https://github.com/GtakillerIV/IniToSql">Github</a></p>
