# GoogleTakeout

If you want to transfer all your photos and videos from Google Photos you will get a lot of .zip files and a lot of different files.
This tool will allow you from the unzipped folders create one folder with all the pictures and videos with original dates.

## Directions
Download all zip files of your photos using google takeout.
With [7-zip tool](https://www.7-zip.org/download.html) unzip all folders to one folder.

Download GoogleTakeout tool, save the path to this tool.
Open terminal (on windows in search put "terminal"). 

```
cd 'path to googletakeout tool'
Usage: .\GoogleTakeout.exe [options...]

Options:
  -s, --source <String>         Path to the folder where you unzipped all zip files. (Required)
  -d, --destination <String>    Path to destination folder. (Required)

Commands:
  help       Display help.
  version    Display version.
```
