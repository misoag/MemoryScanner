using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AecialEngine
{
    static class Entry
    {
        /// TODO:
        /// [80%] Windows control editor
        ///     [ ] View & select children handles from parent
        ///     [ ] Select parent from child
        ///     [ ] Option to apply all changes to parent instead
        ///     [ ] Ignore option
        ///     [ ] More common messages?
        ///     [ ] Outline target in red
        ///     [ ] Select EnableWindow type (0-9)
        ///     [ ] Menus & classes
        /// [ ] Hex editor
        /// [-5%] Memory View & all of the awful things that come with it (see CE for unlisted)
        ///     [ ] Process Debugging? Kind of meaningless
        ///     [ ] Assembly Scripting
        /// [X] Save & load tables
        /// [ ] Lua Scripting
        /// [X] View memory pages and information about them
        /// [X] Show process icons in select process window
        /// [X] Arrange process list by time since execution
        /// [ ] Determine if address is static in found list
        /// [X] Use VirtualQueryEx to find readable pages of memory
        /// [X] Read large chunks of memory (found via VQEx) at once and evaluate each chunk
        /// [X] Ignore read-only memory (with an option to scan it if the user wishes)
        /// [80%] Optimize scan methods 
        ///     [X] Multithread scannning/processing
        ///     [X] Optimize float/double scans
        /// [X] Irreg scan - read all memory pages, create a new list. Start comparing stuff.
        /// [X] Fast scan: Only check memory that is aligned on memory locations (so only scans addresses dividable by 2 or 4) 
        /// [??] Limit Windows API calls to bare minimum during scan
        /// [ ] Other scan types
        ///     [ ] Binary scan type
        ///     [X] Byte scan type
        ///     [X] Float scan type
        ///     [X] Double scan type
        ///     [ ] Text scan type
        ///     [ ] Array of bytes scan type
        ///     [ ] Scan all types (byte through double)
        /// [X] Freeze memory
        /// [X] Edit memory at specified location
        /// [X] Directly edit data in the user address list
        /// [ ] Advanced DLL injector window with stealth, multinject, and autoinject
        /// [80%] Get similar # of addresses as CE
        /// [ ] DirectX Hooking


        //Create instance of memory scanner so it may be accessed as a variable by other stuff
        public static MemoryEditor MemoryEditor;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MemoryEditor = new MemoryEditor();
            //Run instance
            Application.Run(MemoryEditor);
        }
    }
}