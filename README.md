[center][img]http://i1270.photobucket.com/albums/jj602/gtakilleriv/ini2sql.png[/img][/center]

Hello everybody!

I'm pretty sure some of you wanted to switch to MySQL but converting the accounts to SQL was your biggest problem.
I had that same problem but luckly I knew how to program so I made a program in C++ that'll convert my .ini files to SQL format and it worked perfectly fine, but it wasn't dynamic.
So I've decided to create this app to help all those people out there that want to switch to MySQL to enjoy all of the features MySQL has to offer.

[size=14pt][color=orange]What's this?[/color][/size]

It's a .ini to MySQL converter that'll convert all of your .ini files to multiple MySQL INSERT queries.

[size=14pt][color=orange]There's already one![/color][/size]

Yes, I'm fully aware that there is one that is made by [url=http://forum.sa-mp.com/member.php?u=79895]gamer931215[/url] found [url=http://forum.sa-mp.com/showthread.php?t=245893]here[/url].

Why didn't I use it you might say?

Because it always crashed/froze when I loaded huge amount of files to it! And sometimes it didn't even work at all.

[size=14pt][color=orange]What do I bring to the table?[/color][/size]

My program can take huge amounts of files and convert them(the whole purpose of me creating it), and not only that; I also validate the files(of course it's not gonna remove all the invalid files, but should exclude most!).

[size=14pt][color=orange]Okay, I ran out of questions! Just show me the features.[/color][/size]

1)An option to ignore duplicate errors upon executing the INSERT query.
2)Allow you to skip files if their columns don't match the one that I've stored(from the first account file).
3)You can tell the program if your .ini file has the player's username in one of the enteris or not. If there isn't, you can choose a column name for it and it'll get the name of the player automatically for you assuming that the .ini file is named after the player.
4)A popup  telling you that the program has done converting.
5)A progress bar that'll show you the progress.

And I've still got some more ideas in mind that I'll be adding soon.

Note: I have only tested this on a .ini file that was created using [url=http://forum.sa-mp.com/showthread.php?t=175565]y_ini[/url], but I'm sure that it'll work for other systems like dini, because I take care of the spacing.

[size=14pt][color=orange]Screenshots[/color][/size]

[img]http://i1270.photobucket.com/albums/jj602/gtakilleriv/12-1.png[/img]

[img]http://i1270.photobucket.com/albums/jj602/gtakilleriv/13-1.png[/img]

Converting 48,796 accounts(excluding the invalid ones).

[img]http://i1270.photobucket.com/albums/jj602/gtakilleriv/14.png[/img]

Took me about 33.5 seconds(I'm not using an SSD).

Speed will depend on how good your PC is. Especially the HDD.

[size=14pt][color=orange]How to use?[/color][/size]

1)Click on "Browse" and then choose the directory where you have your accounts stored.
2)Change the "Table name" to your table name.
3)Hit "Generate".
4)Once it's done, go back to the directory where you placed the program. You'll see a file named Query.sql. Open it up and you'll see the generated queries that you can use.

[b]Optional[/b]
If you don't want to ignore duplicate errors upon executing your MySQL query, untick "Ignore duplicate errors".
If your file doesn't contain the player's username, then untick "File contains username" and enter in a column name for your usernames.

[size=14pt][color=orange]Bugs[/color][/size]

None currently.
I'm not the best programmer in the world so there might be a few bugs here and there ;)

[size=14pt][color=orange]Download[/color][/size]

[url=https://github.com/GtakillerIV/IniToSql]Github[/url]
