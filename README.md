Memory Editor written in C#

================

A Memory Editor is a powerful hacking tool capable of searching for and modifying running process memory of another application. More specifically, they are used to hack simple values held in memory in games, such as health, gold, etc. As of right now, most data types can be searched for: byte, short, int, long, float, double. There are games with custom structures that pack things together in such a way where binary scans are necessary (mostly seen in emulators), so this is on the TODO list to be implemented, as well as strings.

The tool's function can be broken into the following steps: 1) Open a target process (OpenProcess() in Kernel32.dll) 2) Preform the initial scan based on the 'data type' and 'scan type'. a) Read the memory one page at a time. (VirtualQueryEx() & ReadProcessMemory() in Kernel32.dll) b) Search for matches based on the search criteria and save them to a memory-mapped file 3) Preform additional scans to filter out memory addresses we do not care for. 4) Add the address to a 'Table', where they can then modify the value arbitrarily. (WriteProcessMemory in Kernel32.dll)

Another more challenging feature (yet to be implemented) would be to determine the static portions of code that modify data in the heap, which would also allow the user to modify the code that runs the game at run-time (NOT on disk). Of course this editing would have to be done in x86 assembly, but would allow complex editing of game sub-routines.

=================

With this said, I've already made an effort to rebuild this project from the ground up, as I originally started this project my Sophomore year of highschool. This project has been an incredible learning process, as no other project of mine has required as much research into the inner workings of a computer as this one. I have already fixed most of my bad coding practices that I used when I started this project, however some structural flaws still remain, which I'd like to iron out.

One is example of this is my usage of memory mapped files. This was a solution to a large problem: if I need to scan another programs memory and save the information I find, my program will ALWAYS use more memory than the target program to store the information if I do a scan for 'Unknown Values'. This means the Memory Editor will run out of memory far before the target program does, which is enirely possible if the target uses meomry inefficiently. To solve this issue, I decided to store the scans in memory mapped files so that the scan results were saved to disk rather than in RAM. This however brings about another issue, which is that the files need to save both an address and value, and in a 32-bit application that can be up to 2 gigs for EACH. The problem is further compounded by the need to save the results of the first scan, last scan, and current scan, meaning that the worst case scenario would involve saving 12 gigs of scan data, which is rather absurd. The solution to this would involve redesigning this process, though I haven't looked into alternate solutions yet.

Another improvement I wish to make is to the interface -- this tool can be very difficult to learn, so I intend to make it visually as simple as humanly possible.
