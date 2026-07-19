# asciibannermanager
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-05-07 | numbworks | Created. |
| 2026-06-28 | numbworks | Last update. |

## Introduction

The `AsciiBannerManager` class is responsible for providing a custom ASCII banner for the CLI application in which it's implemented.

## Overview

The vision behind the `AsciiBannerManager` class is to provide an ASCII banner that fits the current terminal width, preventing horizontal scrolling when the CLI application displays it and preserving the banner's intended visual impact.

To achieve the goal, is important to:

1. Develop an understanding of the most common terminal‑width values;
2. Provide both a standard banner and a fallback banner for very small consoles;
3. Enable the class to make decisions according to the current terminal width.

Concerning point (1), I run the following command on the smallest Linux devices in my collection: 

```sh
stty size
```

The command returned the following data:

| Device             | Output (stty) | 
|--------------------|---------------|
| Motorola G86 Power | 33 x 61       | 
| Hackberry Pi       | 33 x 70       |
| Asus CM30          | 35 x 157      |
| Thinkpad x250      | 36 x 150      |

Concerning point (2), I established that for larger terminals I would provide a standard banner based on the concept of **figlet**, which is commonly described as "_a computer program that generates text banners, in a variety of typefaces, composed of letters made up of conglomerations of smaller ASCII characters_" (source: Wikipedia).

Implementing the whole figlet logic is out of the scope of this class. The class hardcodes an ASCII banner created by a third-party figlet and add some custom logic around it. The ASCII banner commonly returned by the `AsciiBannerManager` class can be generated using the following figlets and the "banner3-D" style:
 
- http://www.network-science.de/ascii/ 
- https://www.askapache.com/online-tools/figlet-ascii/

Smaller terminal instead receive a mini banner that displays the application name and the version number within three lines of asterisks.

Concerning point (3), I verified that in the figlet world, to have your banner render within a terminal width of **70 columns** when using "banner3-D", the name of the CLI application (used in the banner) must not exceed **six letters**. 

Once I had collected all this information, I was able to define the requirements for my `AsciiBannerManager` and implement it in all the programming languages I use for developing CLI applications.

## Examples of Standard Banner

```
*****************************************************************
'##::: ##:'##:::::'##::'######:::::'###::::'##::::'##::'######:::
 ###:: ##: ##:'##: ##:'##... ##:::'## ##::: ##:::: ##:'##... ##::
 ####: ##: ##: ##: ##: ##:::..:::'##:. ##:: ##:::: ##: ##:::..:::
 ## ## ##: ##: ##: ##: ##:::::::'##:::. ##: ##:::: ##: ##::'####:
 ##. ####: ##: ##: ##: ##::::::: #########:. ##:: ##:: ##::: ##::
 ##:. ###: ##: ##: ##: ##::: ##: ##.... ##::. ## ##::: ##::: ##::
 ##::. ##:. ###. ###::. ######:: ##:::: ##:::. ###::::. ######:::
..::::..:::...::...::::......:::..:::::..:::::...::::::......::::
**********************************************Version: 2.0.0*****
```

```
*****************************************************
'##::: ##:'##:::::'##:'########::'######::'########::
 ###:: ##: ##:'##: ##:..... ##::'##... ##: ##.... ##:
 ####: ##: ##: ##: ##::::: ##::: ##:::..:: ##:::: ##:
 ## ## ##: ##: ##: ##:::: ##::::. ######:: ##:::: ##:
 ##. ####: ##: ##: ##::: ##::::::..... ##: ##:::: ##:
 ##:. ###: ##: ##: ##:: ##::::::'##::: ##: ##:::: ##:
 ##::. ##:. ###. ###:: ########:. ######:: ########::
..::::..:::...::...:::........:::......:::........:::
********************************Version: 2.0.0*******
```

## Examples of Mini Banner

```
*****************
* NWCAVG v2.0.0 *
*****************
```

```
****************
* NWZSD v2.0.0 *
****************
```

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)