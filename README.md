
<img style="display: block; max-width:100%; margin: auto auto;" src="https://i.imgur.com/nohTytC.png">
<p align="CENTER" style="margin-bottom: 0in"><font size="2" style="font-size: 9pt"><span lang="en-US">v1.2</span></font></p>

<h1>Introduction</h1>

<p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Ini-&gt;Sql
is a program designed to convert .INI files to SQL Insert Queries.</span></font></font></p>
<p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">It
was designed to help people that wanted to switch to MySQL, however
converting their files was the problem.</span></font></font></p>

<h2>Features</h2>

<p style="margin-bottom: 0in"><font color="#244061"><font face="serif"><font size="2"><span lang="en-US">v1.0</span></font></font></font></p>
<ul>
	<li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Improved file handling to prevent possible application failure.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Ability
	to ignore duplicate errors upon executing your SQL Query.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Ability to skip INI files with missing columns.</span></font></font>
	</p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Automatic table column creation (in query) based off of data gathered from INI files.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Progress
	bar that updates you on the program’s status.</span></font></font></p>
</li></ul>
<p style="margin-bottom: 0in"><a name="_GoBack"></a><font color="#244061"><font face="serif"><span lang="en-US">v1.1</span></font></font></p>
<ul>
	<li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">SQL
	Queries will now be escaped to protect you from possible SQL injection
	attempts.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Improved application startup.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Improved progress bar.</span></font></font></p>
</li></ul>
<p style="margin-bottom: 0in"><font color="#244061"><font face="serif"><span lang="en-US">v1.2</span></font></font></p>
<ul>
	<li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Ability to handle INI file comments.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Improved file handling.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Ability
	to change column names.</span></font></font></p>
    <li> <img src='http://i.imgur.com/ladj6nE.gif'> </li>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Improved user input error checking; an empty table name can no longer be used.</span></font></font></p>
</li></ul>

<h1>Usage</h1>

<ol>
	<li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Click
	on the browse button.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Locate
	the folder where you have your .INI files stored.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Change
	the "table name" value to the name of your table.</span></font></font></p>
</li></ol>
<p style="margin-left: 0.5in; margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US"><b>Optional</b></span></font></font></p>
<ul>
	<li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">If
	you don’t want to ignore duplicate errors, uncheck </span></font></font><font color="#548dd4"><font face="serif"><span lang="en-US"><b>Ignore
	duplicate errors</b></span></font></font><font color="#000000"><font face="serif"><span lang="en-US">.</span></font></font></p>
	</li><li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">If
	your file doesn’t contain the player’s username, uncheck
	</span></font></font><font color="#548dd4"><font face="serif"><span lang="en-US"><b>File
	contains username</b></span></font></font><font color="#000000"><font face="serif"><span lang="en-US">
	and enter in a column name for your usernames in the textbox below.</span></font></font></p>
</li></ul>
<p style="margin-left: 1in; margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">Example:
In my table I’ve got a column named </span></font></font><font color="#548dd4"><font face="serif"><span lang="en-US"><b>usernames</b></span></font></font><font color="#000000"><font face="serif"><span lang="en-US">
and my .INI files don’t contain the user’s name but each
.INI file is named after the player it was created for. I’ll
uncheck </span></font></font><font color="#548dd4"><font face="serif"><span lang="en-US"><b>file
contains username</b></span></font></font><font color="#000000"><font face="serif"><span lang="en-US">
and in the textbox below it, I’d write </span></font></font><font color="#548dd4"><font face="serif"><span lang="en-US"><b>usernames</b></span></font></font><font color="#000000"><font face="serif"><span lang="en-US">.</span></font></font></p>
<ul>
	<li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">If
	you don’t want to skip files that have missing columns in
	them, uncheck </span></font></font><font color="#548dd4"><font face="serif"><span lang="en-US"><b>Skip
	file if it’s missing columns</b></span></font></font><font color="#000000"><font face="serif"><span lang="en-US">.</span></font></font></p>
</li></ul>
<p style="margin-left: 0.75in; margin-bottom: 0in"><font color="#244061"><font face="serif"><span lang="en-US">Added
in v1.2</span></font></font></p>
<ul>
	<li><p style="margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US">If
	you want to change your column names, you’ll have to double
	click on the column you want to rename in the column list to your
	right. </span></font></font>
	</p>
</li></ul>
<p style="margin-left: 1in; margin-bottom: 0in"><br>
</p>
<p style="margin-left: 0.5in; margin-bottom: 0in"><font color="#000000"><font face="serif"><span lang="en-US"><b>Important:
</b></span></font></font><font color="#000000"><font face="serif"><span lang="en-US">The
first .INI file in your folder must be fully correct and valid
because my program uses it as a reference.</span></font></font></p>

<h2>Screenshots</h2>

<img src='https://i.imgur.com/ikaBTM7.png'>


<img src='https://i.imgur.com/n6fUt3G.png'>
