using System;
using System.Collections.Generic;
using Aecial.Conversions;
/*
 * HUUUUGEE thanks to samuri25404 from Cheat Engine forums for releasing this class on
 * the Cheat Engine forums.
 * 
 * Source taken from (dead link to file for original class):
 * http://forum.cheatengine.org/viewtopic.php?t=175943
 * 
 * TODO:
 * See DetermineDispType -- Supposed to find if val is 8/16/32 bit -- with release
 * of win7 it may need 64 bit (do qwords exist!!?!)
 */

namespace Opcode
{
    public static class Converter
    {
        #region Variables
        private static List<byte> aob = new List<byte>();
        private static List<byte> opcodebytes = new List<byte>();
        private static bool TwoMnemons = false;
        private static int Mnemonic = 0;

        private static string mnemonic = "";
        private static List<Opcode.Param>[] Params;
        private static List<Opcode.Param> p1;
        private static List<Opcode.Param> p2;
        private static List<Opcode.Param> p3;

        private static int P1_Place = 0;
        private static int P2_Place = 0;
        private static int P3_Place = 0;

        private static string[] zeTokens;
        private static int Start = 0;

        private static bool TakeSecond = true;
        #endregion
        //Example of how to call: Opcode.Opcode.GetBytes("mov [eax],ecx"); 
        public static byte[] GetBytes(string Opcode, string Address)
        { //the address is only needed if you are passing in something that uses a rel32, otherwise just put in ""
            aob = new List<byte>(); //clear any residue away
            opcodebytes = new List<byte>();
            TwoMnemons = false;
            Mnemonic = 0;

            /*
             * Step 1 : Formalize the opcode
             * Step 2 : Split up the Opcode into manageable chunks
             * Step 3 : Get the prefixes (and find the spot of the actual opcode)
             * Step 4 : Identify the Opcode
             * Step 5 : Identify the parameter type, if no parameter types match the used ones, throw an error; this
             *          is used to determine which opcode is actually being used
             * Step 6 : Determine the position of the actual Mnemonic in the string, then put the mnemonic in
             * Step 7 : Determine if the opcode requires a ModRM byte, if so, find it, and same with the SIB byte
             * Step 8 : Determine if the opcode has a displacement byte(s), if so, find it (them)
             * Step 9 : Determine if the opcode has an immediate byte(s), if so, find it (them)
             * Step 10 : Return the byte[] (note that if the user wants it in hex, he/she'll have to do something like this:
             * 
             * List<byte> myaob = new List<byte>();
             * 
             * foreach (byte b in aob)
             * {
             *      myaob.Add(byte.Parse(b.ToString("X")));
             * }
             */

            //Step 1:
            string _TheOpcode = Formalize(Opcode);

            if (_TheOpcode == null) return null;

            //Step 2:
            string[] Tokens = Tokenize(_TheOpcode);

            if (Tokens == null) return null;

            //Step 3:
            InsPrefs(Tokens);

            //Step 4:
            DetermineOpcode(Tokens);

            //Step 5:
            Opcode zeOpcode = FindAbsOpcode(Tokens, Start);

            if (zeOpcode == null) return null;

            //Step 6:
            SetMnemonic(zeOpcode);

            //Perhaps delve into step 7 to see why nop is defined as db 90 5
            int fixme2 = 0;
            if (zeOpcode.mnemonic != "NOP")
            {
                //Step 7:
                ModRMnSIB(zeOpcode, Tokens, Address);

                //Step 8:
                Displacement(zeOpcode, Tokens);

                //Step 9:
                Immediate(zeOpcode, Tokens);
            }
            //Step 10:
            return aob.ToArray();
        }

        //Converts syntax to the form later methods need
        private static string Formalize(string _Opcode)
        {//Maybe this could have gone into the Tokenize method, but Idk
            //This is kinda nasty, so be careful

            //Make sure there are no two spaces touching eachother

            //Find a comma, and make there be spaces on either side of it

            _Opcode = AddSpaces(_Opcode, ','); //My AddSpaces method simply returns if there isn't a comma (in this case) in the string

            //Then, find if there's a bracket, if there is, make sure there is a closing one, if not, throw an error

            if (_Opcode.Contains("["))
            {
                if (!_Opcode.Contains("]"))
                {
                    System.Windows.Forms.MessageBox.Show("Command contains an opening bracket ([) but has no matching closing bracket (]).");
                }

                else
                {
                    _Opcode = AddSpaces(_Opcode, '[');
                    _Opcode = AddSpaces(_Opcode, ']');
                }
            }

            //Change everything to upper

            _Opcode = _Opcode.ToUpper();

            //Put spaces inbetween everything that needs it (for example :       mov [ eax + ecx * 4 ],edx )
            //Already have the brackets

            _Opcode = AddSpaces(_Opcode, '+');
            _Opcode = AddSpaces(_Opcode, '-');
            _Opcode = AddSpaces(_Opcode, '*'); //Notice no /
            _Opcode = AddSpaces(_Opcode, '\''); // '
            _Opcode = AddSpaces(_Opcode, ':');

            //Be nitpicky =) (for example, CMOVPE to CMOVP, and CMOVPO, to CMOVNP, and CMOVNGE to CMOVL, etc; Change ST to ST(0), change BYTE PTR [ to just [

            string mnemonic = _Opcode.Split(' ')[0];

            zeReplace(mnemonic, "NB", "AE");
            zeReplace(mnemonic, "NBE", "A");
            zeReplace(mnemonic, "NA", "BE");
            zeReplace(mnemonic, "NAE", "B");
            zeReplace(mnemonic, "NG", "LE");
            zeReplace(mnemonic, "NGE", "L");
            zeReplace(mnemonic, "NL", "GE");
            zeReplace(mnemonic, "NLE", "G");

            //If there are any brackets, and a displacement is inside it, make it (the displacement) the last parameter inside the brackets
            //Change the Scale to be in the middle (* whatever), the displacement to be on the end (+ whatever)
            if (_Opcode.Contains("["))
            {
                if (_Opcode.Contains("+"))
                {
                    if (_Opcode.IndexOf("+") + 2 != _Opcode.IndexOf("]"))
                    {
                        System.Windows.Forms.MessageBox.Show("Invalid token at " + _Opcode.ToCharArray()[_Opcode.IndexOf("+") + 2] + ".");
                        return null;
                    }
                }

                if (_Opcode.Contains("*"))
                {
                    if (_Opcode.IndexOf("*") + 2 != _Opcode.IndexOf("]")
                        && _Opcode.IndexOf("*") + 4 != _Opcode.IndexOf("]"))
                    {
                        System.Windows.Forms.MessageBox.Show("Invalid token at " + _Opcode.ToCharArray()[_Opcode.IndexOf("+") + 2] + ".");
                        return null;
                    }
                }
            }

            return _Opcode;
        }

        //Called from formalize to add spaces around a string
        private static string AddSpaces(string ToAddTo, char ToFind)
        {
            if (!ToAddTo.Contains(ToFind.ToString()))
                return ToAddTo;

            int Pos = ToAddTo.IndexOf(ToFind);
            string StupidVS = ToAddTo.Substring(Pos - 1, 1);

            if (StupidVS != " ") //wouldn't let me use  char[]'s for some unknown reason, so I have to do this            
                ToAddTo = ToAddTo.Insert(Pos, " ");

            if (ToAddTo.ToCharArray()[ToAddTo.Length - 1] != ToFind)
            {
                Pos = ToAddTo.IndexOf(ToFind); //Have to reget the position because the inserting has wacked it up
                StupidVS = ToAddTo.Substring(Pos + 1, 1);

                if (StupidVS != " ")
                    ToAddTo = ToAddTo = ToAddTo.Insert(Pos + 1, " ");
            }
            return ToAddTo;
        }

        //Small method called by formalize to replace things like "NGE" to "L"
        private static void zeReplace(string s, string ToReplace, string With)
        {
            if (!s.Contains(ToReplace))
                return;

            s.Replace(ToReplace, With);
        }

        //Splits string up:
        //ex) mov [ eax + 4 ],ecx (Note the spacing from formalize) would split all
        //properties (mov, [, eax, +, etc) -- each item being its own element in an array
        private static string[] Tokenize(string Opcode)
        { //This method will split up the opcode into tokens, for easier getting to parameters and opcodes
            //and will also add spaces to where spaces need to be put, and remove spaces from where they don't need to be

            string[] _ToTokenize = Opcode.Split(' ', ','); //Lists > Arrays
            List<string> ToTokenize = new List<string>(_ToTokenize);

            for (int i = 0; i < ToTokenize.Count; i++) //We loop through the chunks to check if there is a bad spot
            {
                if (ToTokenize[i] == "")
                {
                    ToTokenize.RemoveAt(i); //Lists are 0-based
                    i--; //Because it might go something like:
                    //1 : good
                    //2 : bad -remove
                    //3 : (is now 2, so doesn't get checked) bad
                }

                if (ToTokenize[i] == "byte" ||
                    ToTokenize[i] == "word" ||
                    ToTokenize[i] == "dword")
                {
                    if (ToTokenize[i + 1] != "ptr")
                    {
                        System.Windows.Forms.MessageBox.Show("Invalid token at " + (i + 1).ToString() + ". \"ptr\" expected.");
                        return null;
                    }

                    else
                    {
                        ToTokenize[i + 2] = ToTokenize[i] + " " + ToTokenize[i + 1] + " " + ToTokenize[i + 2];
                        ToTokenize.RemoveRange(i, 2);
                    }
                }
            }

            return ToTokenize.ToArray();
        }

        //Finds the mnemonic (eg 'mov') using Array.Find (incredibly fast)
        private static void DetermineOpcode(string[] Tokens)
        {
            string Opcode = Tokens[Mnemonic];
            zeTokens = new string[Tokens.Length];
            zeTokens = Tokens;
            Array.Find<Opcode>(Opcodes.ASSEMBLYLANGUAGE.ToArray(), mypredicate);
        }

        //A predicate to find the correct opcode for DetermineOpcode
        private static bool mypredicate(Opcode o)
        {
            Start++;
            if (o.mnemonic == zeTokens[0])
            {
                Start--; //I've had some issues with this, I'm not sure why though
                return true; //but this seems to fix it
            }
            return false;
        }

        //Tries to find our exact opcode -- if not throw an error and quit.
        private static Opcode FindAbsOpcode(string[] Tokens, int Start)
        {
            List<Opcode> Matches = new List<Opcode>();
            //Loop through the opcodes from start, until the parameters match
            for (int i = Start; ; i++)
            {
                if (Opcodes.ASSEMBLYLANGUAGE[i].mnemonic != Tokens[Mnemonic])
                    break;

                Matches.Add(Opcodes.ASSEMBLYLANGUAGE[i]);
            }

            GetPTypes(Tokens);

            mnemonic = Tokens[Mnemonic]; //for the predicate

            List<Opcode> GoodParameters = new List<Opcode>();
            GoodParameters = Matches.FindAll(zepredicate); //Opcodes with good parameters

            Opcode o = new Opcode();
            int checkhere = 0;
            if (GoodParameters.Count == 0) //Bleh I've got to fix it
            {
                System.Windows.Forms.MessageBox.Show("Potentially unsupported opcode (that or you ****ed up)");
                return null;
            }
            if (GoodParameters.Count == 1)
                o = GoodParameters[0];
            else
            {
                //check if all we need to determine the size

                #region determine size

                bool determine_size1 = false; //the multiple ones are if there are multiple params that need to be
                bool determine_size2 = false; //checked
                bool determine_size3 = false;

                if (p1[0] != Opcode.Param.none && //So we're not looking for something in nothing
                    IsNumeric(Tokens[P1_Place])) //it's placed before this so it'll automatically move on
                //without checking for something that doesn't exist
                {
                    determine_size1 = true;
                }

                if (p2[0] != Opcode.Param.none &&
                    IsNumeric(Tokens[P2_Place]))
                {
                    determine_size2 = true;
                }

                if (p3[0] != Opcode.Param.none &&
                    IsNumeric(Tokens[P3_Place]))
                {
                    determine_size3 = true;
                }

                if (determine_size1 || determine_size2 || determine_size3)
                {
                    //Yes, determine the size

                    int[] places = new int[3] { P1_Place, P2_Place, P3_Place };
                    bool[] bools = new bool[3] { determine_size1, determine_size2, determine_size3 };

                    for (int i = 0; i < 3; i++)
                    { //I personally think it looks nicer this way

                        bool b = bools[i];
                        if (!b) continue;

                        //loop through the matches (opcodes) and see which sizes there are, then 
                        //decide which to use:
                        //the preference order is 32 > 8 > 16

                        string[] Sizes = new string[Matches.Count];
                        List<char>[] chars = new List<char>[GoodParameters.Count];
                        int cnt = 0;

                        foreach (Opcode o_ in GoodParameters)
                        {
                            switch (i + 1) //i + 1 because i will be 0-based but the parameters in opcodes 
                            { //are 1-based
                                case 1:
                                    chars[cnt] = new List<char>(o_.p1.ToString().ToCharArray());
                                    break;

                                case 2:
                                    chars[cnt] = new List<char>(o_.p2.ToString().ToCharArray());
                                    break;

                                case 3:
                                    chars[cnt] = new List<char>(o_.p3.ToString().ToCharArray());
                                    break;
                            }

                            //get the size from the arrays and store it in the array of strings (Sizes)

                            string zeSize = String.Empty;

                            if (chars[cnt][chars[cnt].Count - 1] != '8') //this and next few lines are very confusing
                            {
                                zeSize += chars[cnt][chars[cnt].Count - 2];
                                zeSize += chars[cnt][chars[cnt].Count - 1];
                            }
                            else
                                zeSize += chars[cnt][chars[cnt].Count - 1];

                            Sizes[cnt] = zeSize;

                            cnt++;
                        }

                        bool Has32 = false;
                        int spot32 = 0;
                        int spot8 = 0;
                        cnt = 0;

                        foreach (string s in Sizes)
                        {
                            if (s == "32")
                            {
                                Has32 = true;
                                spot32 = cnt;
                            }

                            if (s == "8")
                                spot8 = cnt;

                            cnt++;
                        }

                        if (Has32) //this is the param that we'll be using
                            o = GoodParameters[spot32];
                        else
                            o = GoodParameters[spot8];
                        //there would only be 3 tops (a 32, an 8 and a 16) since all we need to do is determine the size
                        //there would have to be at least 2 (because we checked earlier), so there would HAVE to be an 8
                        //if there isn't a 32
                    }
                }
                #endregion

                else
                {
                    //Bug fix for some occassions.. may need to address root of issue
                    if (!TakeSecond)
                        o = GoodParameters[0];
                    else
                        o = GoodParameters[1];
                }
            }
            List<byte> zebytes = new List<byte>(o.Defined);
            byte _b = 0x00;

            //add the things the default way (integer-base10), then convert to hex and add to the bytes

            if (Tokens.Length < Mnemonic + 2) //if it's an opcode that only has it's opcode, like pushfd,
            {
                /*arrays are*/
                Array.Resize<string>(ref Tokens, Mnemonic + 2); //calling DetermineR_BWD with Tokens[Mnemonic+1] 
                /*0 based*/
                Tokens[Mnemonic + 1] = ""; // (which doesn't exist), would throw an error
            } //we do +2 because it will already be +1 (arrays are 0 based, so if mnemonic = 0, then it's size will be 1)
            //also, we initialize it because it's default will be null, and you can't call things with the parameter "null"

            _b = (byte)(o.Defined[o.Defined.Length - 1] + DetermineR_BWD(Tokens[Mnemonic + 1]));

            _b = byte.Parse(_b.ToString());

            zebytes[zebytes.Count - 1] = _b;

            opcodebytes = new List<byte>(zebytes); //Using the IEnumerator overload

            return o;
        }

        //This method returns a value for the OpcodeInfo's rb, rw, and rd
        //(which take a simple value and add them to the array of bytes). 
        private static int DetermineR_BWD(string parameter)
        {
            switch (parameter.ToLower())
            {
                case "al":
                case "ax":
                case "eax":
                    return 0;

                case "cl":
                case "cx":
                case "ecx":
                    return 1;

                case "dl":
                case "dx":
                case "edx":
                    return 2;

                case "bl":
                case "bx":
                case "ebx":
                    return 3;

                case "ah":
                case "sp":
                case "esp":
                    return 4;

                case "ch":
                case "bp":
                case "ebp":
                    return 5;

                case "dh":
                case "si":
                case "esi":
                    return 6;

                case "bh":
                case "di":
                case "edi":
                    return 7;

                default:
                    return 0;
            }

        }

        //Called from FindAbsOpcode to help find Opcode by setting private global variables
        //Which are used by the predicate when looping through
        private static void GetPTypes(string[] Tokens)
        {
            Params = GetParam(Tokens);

            if (Params.Length < Mnemonic + 6)
            {
                int before = Params.Length;
                Array.Resize<List<Opcode.Param>>(ref Params, 6);
                for (int i = before; i < (Mnemonic + 6); i++)
                {
                    Params[i] = new List<Opcode.Param>(); //init
                }
            }

            for (int i = 0; i < Params.Length; i++)
            {
                if (Params[i].Count == 0)
                    Params[i].Add(Opcode.Param.none); //This is so when we're using the predicate, 
                //we're not looking for things inside lists with nothing
            }

            List<string> _Tokens = new List<string>(Tokens); //IEnumerable overload
            //Lists > Arrays            

            if (_Tokens.Contains("["))
            {
                int OBracket = _Tokens.IndexOf("["); //Open bracket
                int CBracket = _Tokens.IndexOf("]");

                if (OBracket == 1)
                {
                    //opcode [ p1 ...

                    if (CBracket == 3)
                    {
                        // opcode [ p1 ] p2 p3

                        p1 = Params[2];
                        p2 = Params[4];
                        p3 = Params[5];

                        P1_Place = 2;
                        P2_Place = 4;
                        P3_Place = 5;
                    }

                    if (CBracket == 5)
                    {
                        // opcode [ p1 p2 ] p3 

                        p1 = Params[2];
                        p2 = Params[3];
                        p3 = Params[5];

                        P1_Place = 2;
                        P2_Place = 3;
                        P3_Place = 5;
                    }

                    if (CBracket == 7)
                    {
                        // opcode [ p1 p2 p3 ] (not sure if this is possible, but just in case)

                        p1 = Params[2];
                        p2 = Params[3];
                        p3 = Params[4];

                        P1_Place = 2;
                        P2_Place = 3;
                        P3_Place = 4;
                    }
                }

                if (OBracket == 3)
                {
                    // opcode p1 , [ ...

                    if (CBracket == 5)
                    {
                        // opcode p1 [ p2 ] p3 (there might not be a p3 but that's where it would be)

                        p1 = Params[1];
                        p2 = Params[3];
                        p3 = Params[5];

                        P1_Place = 1;
                        P2_Place = 3;
                        P3_Place = 5;
                    }

                    if (CBracket == 7)
                    {
                        // opcode p1 [ p2 p3 ]

                        p1 = Params[1];
                        p2 = Params[3];
                        p3 = Params[4];

                        P1_Place = 1;
                        P2_Place = 3;
                        P3_Place = 4;
                    }
                }

                if (OBracket == 5)
                {
                    //opcode p1 p2 [ p3 ]

                    p1 = Params[1];
                    p2 = Params[2];
                    p3 = Params[4];

                    P1_Place = 1;
                    P2_Place = 2;
                    P3_Place = 4;
                }
            }

            else
            {
                // opcode p1 p2 p3
                p1 = Params[1];
                p2 = Params[2];
                p3 = Params[3];

                P1_Place = 1;
                P2_Place = 2;
                P3_Place = 3;
            }

        }

        //This is a method used to determine the types of parameters the tokens are (r32 for example). 
        private static List<Opcode.Param>[] GetParam(string[] Tokens)
        {
            List<Opcode.Param>[] temp = new List<Opcode.Param>[Tokens.Length];

            //initialize the lists

            for (int i = 0; i < Tokens.Length; i++)
            {
                temp[i] = new List<Opcode.Param>();
            }

            int cnt = 0;
            string Param = String.Empty;

            foreach (string param in Tokens)
            {
                Param = param.ToLower();

                if (Param == "") temp[cnt].Add(Opcode.Param.none);

                if (Param == "al")
                {
                    if (Tokens[cnt - 1] != "[")
                    {
                        temp[cnt].Add(Opcode.Param.al);
                        temp[cnt].Add(Opcode.Param.r8);
                        temp[cnt].Add(Opcode.Param.rm8);
                    }
                }

                if (Param == "cl")
                {
                    if (Tokens[cnt - 1] != "[")
                    {
                        temp[cnt].Add(Opcode.Param.cl);
                        temp[cnt].Add(Opcode.Param.r8);
                        temp[cnt].Add(Opcode.Param.rm8);
                    }
                }

                if (Param == "ax")
                {
                    if (Tokens[cnt - 1] != "[")
                    {
                        temp[cnt].Add(Opcode.Param.ax);
                        temp[cnt].Add(Opcode.Param.r16);
                        temp[cnt].Add(Opcode.Param.rm16);
                    }
                }

                if (Param == "dx")
                {
                    if (Tokens[cnt - 1] != "[")
                    {
                        temp[cnt].Add(Opcode.Param.dx);
                        temp[cnt].Add(Opcode.Param.r16);
                        temp[cnt].Add(Opcode.Param.rm16);
                    }
                }

                if (Param == "eax")
                {
                    if (Tokens[cnt - 1] != "[")
                    {
                        temp[cnt].Add(Opcode.Param.eax);
                        temp[cnt].Add(Opcode.Param.r32);
                        temp[cnt].Add(Opcode.Param.rm32);
                    }
                    else
                    {
                        int fixme = 0;
                        temp[cnt].Add(Opcode.Param.eax);
                        temp[cnt].Add(Opcode.Param.r32);
                        temp[cnt].Add(Opcode.Param.rm32);


                        TakeSecond = true;
                    }
                }

                if (Param == "cr0" ||
                    Param == "cr1" ||
                    Param == "cr2" ||
                    Param == "cr3" ||
                    Param == "cr4" ||
                    Param == "cr5" ||
                    Param == "cr6" ||
                    Param == "cr7")
                    temp[cnt].Add(Opcode.Param.cr);


                if (Param == "dr0" ||
                    Param == "dr1" ||
                    Param == "dr2" ||
                    Param == "dr3" ||
                    Param == "dr4" ||
                    Param == "dr5" ||
                    Param == "dr6" ||
                    Param == "dr7")
                    temp[cnt].Add(Opcode.Param.dr);

                if (Param == "1")
                    temp[cnt].Add(Opcode.Param.dig1);

                if (Param == "3")
                    temp[cnt].Add(Opcode.Param.dig3);


                if (Param == "+" ||
                    Param == "-")
                    temp[cnt].Add(Opcode.Param.rel8);


                if (IsNumeric(Param))
                {
                    if (Array.IndexOf(Tokens, "[") == -1 ||
                        Array.IndexOf(Tokens, "]") < cnt)
                    {
                        temp[cnt].Add(Opcode.Param.imm8);
                        temp[cnt].Add(Opcode.Param.imm16);
                        temp[cnt].Add(Opcode.Param.imm32);

                        temp[cnt].Add(Opcode.Param.rel16);
                        temp[cnt].Add(Opcode.Param.rel32);
                    }

                    else if (Array.IndexOf(Tokens, "[") == (cnt - 1))
                    {
                        temp[cnt].Add(Opcode.Param.m32real);
                        temp[cnt].Add(Opcode.Param.m16int);
                        temp[cnt].Add(Opcode.Param.m32int);

                        temp[cnt].Add(Opcode.Param.rm8);
                        temp[cnt].Add(Opcode.Param.rm16);
                        temp[cnt].Add(Opcode.Param.rm32);
                    }

                    else
                    {
                        temp[cnt].Add(Opcode.Param.moffs8);
                        temp[cnt].Add(Opcode.Param.moffs16);
                        temp[cnt].Add(Opcode.Param.moffs32);
                    }
                }


                if (Param == "dl" ||
                    Param == "bl" ||
                    Param == "ah" ||
                    Param == "ch" ||
                    Param == "dh" ||
                    Param == "bh")
                {
                    temp[cnt].Add(Opcode.Param.rm8);
                    temp[cnt].Add(Opcode.Param.r8);
                }


                if (Param == "cx" || //ax and dx are already used
                    Param == "bx" ||
                    Param == "si" ||
                    Param == "di" ||
                    Param == "sp" ||
                    Param == "bp")
                {
                    temp[cnt].Add(Opcode.Param.rm16);
                    temp[cnt].Add(Opcode.Param.r16);
                }


                if (Param == "ecx" ||
                    Param == "edx" ||
                    Param == "ebx" ||
                    Param == "esi" ||
                    Param == "edi" ||
                    Param == "esp" ||
                    Param == "ebp")
                {
                    temp[cnt].Add(Opcode.Param.rm32);
                    temp[cnt].Add(Opcode.Param.r32);
                }

                if (Param.Contains("byte ptr ["))
                {
                    temp[cnt].Add(Opcode.Param.rm8);
                    temp[cnt].Add(Opcode.Param.m8);
                }


                if (Param.Contains("word ptr ["))
                {
                    temp[cnt].Add(Opcode.Param.rm16);
                    temp[cnt].Add(Opcode.Param.m16);

                }

                if (Param.Contains("dword ptr ["))
                {
                    temp[cnt].Add(Opcode.Param.rm32);
                    temp[cnt].Add(Opcode.Param.m32);
                    temp[cnt].Add(Opcode.Param.mmm32);
                }


                if (Param == "cs")
                {
                    temp[cnt].Add(Opcode.Param.sreg);
                    temp[cnt].Add(Opcode.Param.cs);
                }

                if (Param == "ds")
                {
                    temp[cnt].Add(Opcode.Param.sreg);
                    temp[cnt].Add(Opcode.Param.ds);
                }

                if (Param == "es")
                {
                    temp[cnt].Add(Opcode.Param.sreg);
                    temp[cnt].Add(Opcode.Param.es);
                }

                if (Param == "fs")
                {
                    temp[cnt].Add(Opcode.Param.sreg);
                    temp[cnt].Add(Opcode.Param.fs);
                }

                if (Param == "gs")
                {
                    temp[cnt].Add(Opcode.Param.sreg);
                    temp[cnt].Add(Opcode.Param.gs);
                }

                if (Param == "ss")
                {
                    temp[cnt].Add(Opcode.Param.sreg);
                    temp[cnt].Add(Opcode.Param.ss);
                }

                if (Param == "st(0)")
                    temp[cnt].Add(Opcode.Param.st0);

                if (Param == "st(1)" ||
                    Param == "st(2)" ||
                    Param == "st(3)" ||
                    Param == "st(4)" ||
                    Param == "st(5)" ||
                    Param == "st(6)" ||
                    Param == "st(7)")
                    temp[cnt].Add(Opcode.Param.sti);

                if (Param == "mm0" ||
                    Param == "mm1" ||
                    Param == "mm2" ||
                    Param == "mm3" ||
                    Param == "mm4" ||
                    Param == "mm5" ||
                    Param == "mm6" ||
                    Param == "mm7")
                {
                    temp[cnt].Add(Opcode.Param.mmm32);
                    temp[cnt].Add(Opcode.Param.mm);
                }


                if (Param == "xmm0" ||
                    Param == "xmm1" ||
                    Param == "xmm2" ||
                    Param == "xmm3" ||
                    Param == "xmm4" ||
                    Param == "xmm5" ||
                    Param == "xmm6" ||
                    Param == "xmm7")
                {
                    temp[cnt].Add(Opcode.Param.xmmm32);
                    temp[cnt].Add(Opcode.Param.xmm);
                }

                cnt++;
            }

            return temp;
        }

        private static bool IsNumeric(string s)
        {
            char[] Tokens = new char[s.Length];
            Tokens = s.ToCharArray();
            string temp = String.Empty;

            foreach (char c in Tokens)
            {
                if (!Char.IsNumber(c))
                {
                    temp = c.ToString().ToLower();
                    if (temp != "a" &&
                        temp != "b" &&
                        temp != "c" &&
                        temp != "d" &&
                        temp != "e" &&
                        temp != "f")
                        return false;
                }
            }

            return true;
        }

        //Another simple predicate, called by FindAbsOpcode, to find the correct opcode. 
        private static bool zepredicate(Opcode o)
        {
            return ((o.mnemonic == mnemonic) &&
                (p1.Contains(o.p1)) &&
                (p2.Contains(o.p2)) &&
                (p3.Contains(o.p3)));
        }

        //Determines which Instructional Prefixes are needed in the array of bytes, and adds them in. Then, it adds in the actual opcode bytes. 
        private static void InsPrefs(string[] Tokens)
        {
            switch (Tokens[0]) //Group 1:
            {
                case "LOCK":
                    aob.Add(0xF0);
                    TwoMnemons = true;
                    break;

                case "REPNE":
                case "REPNZ":
                    aob.Add(0xF2);
                    TwoMnemons = true;
                    break;

                case "REP":
                case "REPE":
                case "REPZ":
                    aob.Add(0xF3);
                    TwoMnemons = true;
                    break;
            }

            //Group 2:
            int ColonSpot = Array.IndexOf(Tokens, ":");
            if (ColonSpot != -1) //It does have a colon
            {
                switch (Tokens[ColonSpot - 1])
                {
                    case "CS":
                        aob.Add(0x2E);
                        break;

                    case "SS":
                        aob.Add(0x36);
                        break;

                    case "DS":
                        aob.Add(0x3E);
                        break;

                    case "ES":
                        aob.Add(0x26);
                        break;

                    case "FS":
                        aob.Add(0x64);
                        break;

                    case "GS":
                        aob.Add(0x65);
                        break;

                    default:
                        System.Windows.Forms.MessageBox.Show("Error encountered at " + Tokens[ColonSpot - 1] + ". Segment register expected.");
                        break;
                }
            }

            //Group 3 is included in the opcodes
            //Group 4 isn't used
        }

        //Takes the bytes from a public property that was set by FindAbsOpcode and adds those into the array of bytes. 
        private static void SetMnemonic(Opcode o)
        {
            if (TwoMnemons)
                Mnemonic = 1;
            else
                Mnemonic = 0;

            foreach (byte b in o.Defined)
            {
                aob.Add(b);
            }
        }

        //Determines if the opcode needs a ModRM byte, if so, finds it. Then determines if it needs an SIB byte, and if so, finds it. 
        private static void ModRMnSIB(Opcode o, string[] Tokens, string Address)
        {
            #region ModRM

            #region Check

            //Check if it needs a ModRM, if it doesn't, return
            //If either of the OI parameters have +rb, +rw, or +rd, or +i, then it doesn't have one

            if (o.oi1 == Opcode.OpcodeInfo.rb ||
                o.oi1 == Opcode.OpcodeInfo.rw ||
                o.oi1 == Opcode.OpcodeInfo.rd ||
                o.oi1 == Opcode.OpcodeInfo.i ||

                o.oi2 == Opcode.OpcodeInfo.rb ||
                o.oi2 == Opcode.OpcodeInfo.rw ||
                o.oi2 == Opcode.OpcodeInfo.rd ||
                o.oi2 == Opcode.OpcodeInfo.i)
                return;

            #endregion

            //Declare ModRM vars

            int RM = 0;
            int Mod = 0;
            int reg = 0;
            int _modRM = 0;
            byte ModRM = 0x00;

            #region R/M

            //Find the R/M (second parameter)
            //First, find where the second parameter is in the array

            int SecondPar = 0;
            int Bracket = Array.IndexOf(Tokens, "[");
            int SecBracket = Array.IndexOf(Tokens, "]");
            int Diff = SecBracket - Bracket;
            bool HasBracket = false;

            if (Bracket != -1) //There is a bracket in the opcode
            {
                HasBracket = true;

                if (Bracket == 1)
                {
                    if (SecBracket == 3)
                        SecondPar = Mnemonic + 4; //opcode [ p1 ] p2
                    else
                        SecondPar = Mnemonic + 3; //opcode [ p1 p2 ]
                }

                if (Bracket == 2)
                    SecondPar = Mnemonic + 3; //opcode p1 [ p2 ]
            }

            else //There is not a bracket in the opcode
            {
                HasBracket = false;
                SecondPar = Mnemonic + 2; //opcode p1 p2
            }

            //Next determine what it is

            if (HasBracket)
            {
                if (Diff == 2)
                {
                    string s = Tokens[Mnemonic + Bracket + 1];

                    if (s == "EAX") RM = 000;
                    else if (s == "ECX") RM = 001;
                    else if (s == "EDX") RM = 010;
                    else if (s == "EBX") RM = 011;
                    else if (s == "ESP") RM = 100;
                    else if (s == "EBP") RM = 101;
                    else if (s == "ESI") RM = 110;
                    else if (s == "EDI") RM = 111;
                    else
                    {
                        //32 bit displacement
                        RM = 101;
                    }
                }

                else //R/M = 100 or 101 
                {
                    //If EBP is in the brackets ( '[',']'), then it's 101, otherwise it's 100

                    List<string> zetemp = new List<string>();

                    for (int i = Bracket + 1; i < SecBracket; i++)
                    {
                        zetemp.Add(Tokens[i]);
                    }

                    if (zetemp.Contains("EBP"))
                        RM = 101;
                    else
                        RM = 100;
                }
            }

            else
            {
                //Either mod = 11, or there is a 32 bit displacement

                string s = "";

                if (Tokens.Length > Mnemonic + 1)
                    s = Tokens[Mnemonic + 1]; //Since there is no bracket, it's 1 after the mnemonic              

                if (s == "EAX" || s == "AX" || s == "AL" || s == "MM0" || s == "XMM0" || s == "ST(0)" || s == "ES" || s == "CR0" || s == "DR0") RM = 000;
                else if (s == "ECX" || s == "CX" || s == "CL" || s == "MM1" || s == "XMM1" || s == "ST(1)" || s == "CS" || s == "CR1" || s == "DR1") RM = 001;
                else if (s == "EDX" || s == "DX" || s == "DL" || s == "MM2" || s == "XMM2" || s == "ST(2)" || s == "SS" || s == "CR2" || s == "DR2") RM = 010;
                else if (s == "EBX" || s == "BX" || s == "BL" || s == "MM3" || s == "XMM3" || s == "ST(3)" || s == "DS" || s == "CR3" || s == "DR2") RM = 011;
                else if (s == "ESP" || s == "SP" || s == "AH" || s == "MM4" || s == "XMM4" || s == "ST(4)" || s == "FS" || s == "CR4" || s == "DR2") RM = 100;
                else if (s == "EBP" || s == "BP" || s == "CH" || s == "MM5" || s == "XMM5" || s == "ST(5)" || s == "GS" || s == "CR5" || s == "DR2") RM = 101;
                else if (s == "ESI" || s == "SI" || s == "DH" || s == "MM6" || s == "XMM6" || s == "ST(6)" || s == "HS" || s == "CR6" || s == "DR2") RM = 110;
                else if (s == "EDI" || s == "DI" || s == "BH" || s == "MM7" || s == "XMM7" || s == "ST(7)" || s == "IS" || s == "CR7" || s == "DR2") RM = 111;

                else
                { //32 bit displacement
                    RM = 101;
                }
            }

            #endregion

            #region Mod

            //Find the Mod (second parameter) 

            if (RM != 100)
            {
                if (HasBracket)
                {
                    if (Diff == 2) //There is one parameter in between the brackets
                        Mod = 00;

                    else if (Diff == 3)
                    {
                        int DispType = DetermineDispType(Tokens[SecondPar]);

                        if (DispType == 8)
                            Mod = 01;
                        else
                            Mod = 10;
                    }
                }

                else
                {
                    if (RM == 101)
                        Mod = 00;
                    else
                        Mod = 11;
                }
            }

            else
            {
                if (DetermineDispType(Tokens[SecondPar]) == 8)
                    Mod = 01;
                else
                    Mod = 10;
            }

            #endregion

            #region Reg/Opcode

            //Find the Reg/Opcode (first param, or /digit)

            //First check for a /digit

            int WhichDig = -1;

            switch (o.oi1)
            {
                case Opcode.OpcodeInfo.digit0:
                    WhichDig = 0;
                    break;

                case Opcode.OpcodeInfo.digit1:
                    WhichDig = 1;
                    break;

                case Opcode.OpcodeInfo.digit2:
                    WhichDig = 2;
                    break;

                case Opcode.OpcodeInfo.digit3:
                    WhichDig = 3;
                    break;

                case Opcode.OpcodeInfo.digit4:
                    WhichDig = 4;
                    break;

                case Opcode.OpcodeInfo.digit5:
                    WhichDig = 5;
                    break;

                case Opcode.OpcodeInfo.digit6:
                    WhichDig = 6;
                    break;

                case Opcode.OpcodeInfo.digit7:
                    WhichDig = 7;
                    break;

                default:

                    switch (o.oi2)
                    {
                        case Opcode.OpcodeInfo.digit0:
                            WhichDig = 0;
                            break;

                        case Opcode.OpcodeInfo.digit1:
                            WhichDig = 1;
                            break;

                        case Opcode.OpcodeInfo.digit2:
                            WhichDig = 2;
                            break;

                        case Opcode.OpcodeInfo.digit3:
                            WhichDig = 3;
                            break;

                        case Opcode.OpcodeInfo.digit4:
                            WhichDig = 4;
                            break;

                        case Opcode.OpcodeInfo.digit5:
                            WhichDig = 5;
                            break;

                        case Opcode.OpcodeInfo.digit6:
                            WhichDig = 6;
                            break;

                        case Opcode.OpcodeInfo.digit7:
                            WhichDig = 7;
                            break;
                    }
                    break;
            }

            if (WhichDig == -1)
            {
                string s = "";

                if (Tokens.Length > Mnemonic + 2) //length = 1 + last element's index, if last element's index is mnemonic
                    s = Tokens[SecondPar]; //then size = mnemonic + 1

                if (s == "EAX" || s == "AX" || s == "AL" || s == "MM0" || s == "XMM0" || s == "ST(0)" || s == "ES" || s == "CR0" || s == "DR0") reg = 000;
                if (s == "ECX" || s == "CX" || s == "CL" || s == "MM1" || s == "XMM1" || s == "ST(1)" || s == "CS" || s == "CR1" || s == "DR1") reg = 001;
                if (s == "EDX" || s == "DX" || s == "DL" || s == "MM2" || s == "XMM2" || s == "ST(2)" || s == "SS" || s == "CR2" || s == "DR2") reg = 010;
                if (s == "EBX" || s == "BX" || s == "BL" || s == "MM3" || s == "XMM3" || s == "ST(3)" || s == "DS" || s == "CR3" || s == "DR2") reg = 011;
                if (s == "ESP" || s == "SP" || s == "AH" || s == "MM4" || s == "XMM4" || s == "ST(4)" || s == "FS" || s == "CR4" || s == "DR2") reg = 100;
                if (s == "EBP" || s == "BP" || s == "CH" || s == "MM5" || s == "XMM5" || s == "ST(5)" || s == "GS" || s == "CR5" || s == "DR2") reg = 101;
                if (s == "ESI" || s == "SI" || s == "DH" || s == "MM6" || s == "XMM6" || s == "ST(6)" || s == "HS" || s == "CR6" || s == "DR2") reg = 110;
                if (s == "EDI" || s == "DI" || s == "BH" || s == "MM7" || s == "XMM7" || s == "ST(7)" || s == "IS" || s == "CR7" || s == "DR2") reg = 111;
            }

            else
            {
                switch (WhichDig)
                {
                    case 0:
                        reg = 000;
                        break;

                    case 1:
                        reg = 001;
                        break;

                    case 2:
                        reg = 010;
                        break;

                    case 3:
                        reg = 011;
                        break;

                    case 4:
                        reg = 100;
                        break;

                    case 5:
                        reg = 101;
                        break;

                    case 6:
                        reg = 110;
                        break;

                    case 7:
                        reg = 111;
                        break;
                }
            }

            #endregion

            #region ModRM

            if (Address == "")
            {

                //Put it all together and add it to the aob

                Mod = Mod * (int)(Math.Pow(10, 6));
                reg = reg * (int)(Math.Pow(10, 3));
                RM = RM * (int)(Math.Pow(10, 0)); //For consistency
                _modRM = Mod + reg + RM;
                ModRM = byte.Parse(Conversions.BinaryToHex(_modRM), System.Globalization.NumberStyles.HexNumber);
                aob.Add(ModRM);
            }

            else //we're dealing with a rel32, in that case we need to calculate,
            // thenjust put in the bytes in little endian and be done
            {
                int Destination = int.Parse(Tokens[1], System.Globalization.NumberStyles.HexNumber);
                int _Base = int.Parse(Address, System.Globalization.NumberStyles.HexNumber); //base is declared in SIB
                int Idword_form = (Destination - (_Base + 5)); //Thanks Appal
                string Sdword_form = Idword_form.ToString("X");
                char[] zechars = new char[Sdword_form.Length];
                zechars = Sdword_form.ToCharArray();
                string[] littleendian = new string[Sdword_form.Length / 2];

                for (int i = 0; i < Sdword_form.Length; i += 2)
                {
                    littleendian[i] = zechars[i].ToString();
                    littleendian[i] += zechars[i + 2];
                }

                foreach (string s in littleendian)
                {
                    aob.Add(byte.Parse(s));
                }
            }

            #endregion

            #endregion

            #region SIB

            #region Check

            //Check if it needs an SIB, if it doesn't, return

            if (!(RM == 100 && Mod != 11))
                return;

            #endregion

            //Declare SIB vars

            int SS = 0;
            int Index = 0;
            int Base = 0;
            int _SIB = 0;
            byte SIB = 0x00;

            int MultPos = Array.IndexOf(Tokens, "*"); //Find the *

            if (MultPos == -1) //There isn't a *
            {
                SS = 00;

                if (Mod != 0)
                {
                    switch (Tokens[SecBracket - 1]) //last param is the Scaled Index
                    {
                        case "EAX":
                            Index = 000;
                            break;

                        case "ECX":
                            Index = 001;
                            break;

                        case "EDX":
                            Index = 001;
                            break;

                        case "EBX":
                            Index = 011;
                            break;

                        case "EBP":
                            Index = 101;
                            break;

                        case "ESI":
                            Index = 110;
                            break;

                        case "EDI":
                            Index = 111;
                            break;
                    }
                }
                else
                {
                    switch (Tokens[Mnemonic + Bracket + 1])
                    {
                        case "EAX":
                            Index = 000;
                            break;

                        case "ECX":
                            Index = 001;
                            break;

                        case "EDX":
                            Index = 001;
                            break;

                        case "EBX":
                            Index = 011;
                            break;

                        case "EBP":
                            Index = 101;
                            break;

                        case "ESI":
                            Index = 110;
                            break;

                        case "EDI":
                            Index = 111;
                            break;
                    }
                }
            }
            else
            {
                switch (Tokens[Mnemonic + MultPos + 1])
                {
                    case "2":
                        SS = 01;
                        break;

                    case "4":
                        SS = 10;
                        break;

                    case "8":
                        SS = 11;
                        break;
                }

                switch (Tokens[Mnemonic + MultPos - 1])
                {
                    case "EAX":
                        Index = 000;
                        break;

                    case "ECX":
                        Index = 001;
                        break;

                    case "EDX":
                        Index = 010;
                        break;

                    case "EBX":
                        Index = 011;
                        break;

                    case "EBP":
                        Index = 101;
                        break;

                    case "ESI":
                        Index = 110;
                        break;

                    case "EDI":
                        Index = 111;
                        break;
                }
            }

            //Get the base (first param inside the brackets ('[' and ']'), and check if the Scaled Index is none

            switch (Tokens[Mnemonic + Bracket + 1])
            {
                case "EAX":
                    Base = 000;
                    break;

                case "ECX":
                    Base = 001;
                    break;

                case "EDX":
                    Base = 010;
                    break;

                case "EBX":
                    Base = 011;
                    break;

                case "ESP":
                    Base = 100;
                    break;

                case "EDI":
                    Base = 101; //More in the default section
                    break;

                case "ESI":
                    Base = 110;
                    break;

                default:
                    if (Mod == 0)
                    {
                        Base = 101;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Invalid parameter at " + Tokens[Mnemonic + Bracket + 1] + ".");
                    }
                    break;

            }

            if (SecBracket - Bracket == 2) //One parameter, and the automatic difference ( 1: [  2: ]  2 - 1 = 1
            {
                SS = 100; //none
            }

            #region SIB

            //Put it all together, and add it to the aob

            SS = SS * 6;
            Index = Index * 3;
            Base = Base * 1;

            _SIB = SS + Index + Base;
            SIB = byte.Parse(Conversions.BinaryToHex(_SIB), System.Globalization.NumberStyles.HexNumber);
            aob.Add(SIB);

            #endregion

            #endregion
        }

        //This determines the displacement type (8 bit, 16 bit, or 32 bit). 
        private static int DetermineDispType(string Parameter)
        {
            int Length = Parameter.TrimStart('0').Length;

            if (Length < 2)
                return 8;

            else if (Length > 2 && Length < 8)
                return 16;

            else
                return 32;
        }

        //This finds the 0, 1, 2, or 4 bytes for Displacement and adds them to the aob. 
        private static void Displacement(Opcode o, string[] Tokens)
        {
            //find out if there is a number inside the brackets 

            if (Array.IndexOf(Tokens, "[") == -1) return; //no bracket, no displacement

            bool HasDisp = false;

            for (int i = Array.IndexOf(Tokens, "["); i < Array.IndexOf(Tokens, "]"); i++)
            {
                if (IsNumeric(Tokens[i]))
                {
                    HasDisp = true;
                    break;
                }
            }

            if (!HasDisp) return;

            int Size = 0;

            switch (o.oi1)
            {
                case Opcode.OpcodeInfo.ib:
                    Size = 1;
                    break;

                case Opcode.OpcodeInfo.iw:
                    Size = 2;
                    break;

                case Opcode.OpcodeInfo.id:
                    Size = 4;
                    break;

                default:
                    switch (o.oi2)
                    {
                        case Opcode.OpcodeInfo.ib:
                            Size = 1;
                            break;

                        case Opcode.OpcodeInfo.iw:
                            Size = 2;
                            break;

                        case Opcode.OpcodeInfo.id:
                            Size = 4;
                            break;

                        default:
                            Size = 4; //default
                            break;
                    }
                    break;
            }

            //it's going to be the last parameter inside the bracket
            int SecBracket = Array.IndexOf(Tokens, "]");
            string Disp = Tokens[SecBracket - 1];
            int Length = Disp.Length;

            switch (Size)
            {
                case 1:
                    if (Length > 2)
                    {
                        System.Windows.Forms.MessageBox.Show("Incorrect attribute size encountered at " + Disp);
                        break;
                    }
                    else
                        Disp.PadLeft(2, '0');
                    break;

                case 2:
                    if (Length > 4)
                    {
                        System.Windows.Forms.MessageBox.Show("Incorrect attribute size encountered at " + Disp);
                        break;
                    }
                    else
                        Disp.PadLeft(4, '0');
                    break;

                case 4:
                    if (Length > 8)
                    {
                        System.Windows.Forms.MessageBox.Show("Incorrect attribute size encountered at " + Disp);
                        break;
                    }
                    else
                        Disp.PadLeft(8, '0');
                    break;
            }

            string[] DispTemp = new string[Length / 2];
            char[] temp = Disp.ToCharArray();
            string tmp = "";

            for (int i = 0; i < Length; i += 2)
            {
                tmp = temp[i].ToString();
                tmp += temp[i + 1];
                DispTemp[i / 2] = tmp;
            }

            Array.Reverse(DispTemp);

            //now we convert them to decimal since that's the base language for C#, and the base for ASM is hex

            int cnt = 0;

            foreach (string s in DispTemp)
            {
                string _temp = Convert.ToInt32(s, 16).ToString();
                DispTemp[cnt] = _temp;
                cnt++;
            }

            for (int i = 0; i < DispTemp.Length; i++)
            {
                aob.Add(byte.Parse(DispTemp[i]));
            }

        }

        //As with the Displacement, this finds the 0, 1, 2, or 4 bytes for Immediate, and adds them to the aob. 
        private static void Immediate(Opcode o, string[] Tokens)
        {
            int HowMany = 0;

            switch (o.p1)
            {
                case Opcode.Param.imm8:
                    HowMany = 1;
                    break;

                case Opcode.Param.imm16:
                    HowMany = 2;
                    break;

                case Opcode.Param.imm32:
                    HowMany = 4;
                    break;

                default:
                    switch (o.p2)
                    {
                        case Opcode.Param.imm8:
                            HowMany = 1;
                            break;

                        case Opcode.Param.imm16:
                            HowMany = 2;
                            break;

                        case Opcode.Param.imm32:
                            HowMany = 4;
                            break;

                        default:
                            switch (o.p3)
                            {
                                case Opcode.Param.imm8:
                                    HowMany = 1;
                                    break;

                                case Opcode.Param.imm16:
                                    HowMany = 2;
                                    break;

                                case Opcode.Param.imm32:
                                    HowMany = 4;
                                    break;
                            }
                            break;
                    }
                    break;
            }

            if (HowMany == 0) return;

            //Now we find out which spot the immediate byte will be in

            Opcode.Param p = Opcode.Param.imm8; //Just default

            switch (HowMany)
            {
                case 1:
                    p = Opcode.Param.imm8; //8 bit
                    break;

                case 2:
                    p = Opcode.Param.imm16;
                    break;

                case 4:
                    p = Opcode.Param.imm32;
                    break;
            }

            int Where = Mnemonic + 1; //This is where the params start. We will add to this depending on which param it is

            int Bracket = Array.IndexOf(Tokens, "[");

            if (Bracket != -1) //If there is a bracket in the array
            {
                if (Bracket == 1)
                {
                    if (o.p1 == p)
                        Where += 2;

                    if (o.p2 == p)
                        Where += 3;

                    if (o.p3 == p)
                        Where += 4;
                }

                if (Bracket == 2)
                {
                    if (o.p1 == p)
                        Where += 1;

                    if (o.p2 == p)
                        Where += 2;

                    if (o.p3 == p)
                        Where += 3;
                }
            }

            else //If there isn't a bracket in the array
            {
                if (o.p1 == p)
                    Where += 0;

                if (o.p2 == p)
                    Where += 1;

                if (o.p3 == p)
                    Where += 2;
            }

            //Now we know where in the array the immediate is, and what size we need to make it

            string Imm = Tokens[Where];

            switch (HowMany)
            {
                case 1:
                    if (Imm.Length > 2)
                    {
                        System.Windows.Forms.MessageBox.Show("Epic phailure : Size overload. A byte can not be larger than 0xFF");
                        return;
                    }

                    Imm = Imm.PadLeft(2, '0');
                    break;

                case 2:
                    if (Imm.Length > 4)
                    {
                        System.Windows.Forms.MessageBox.Show("Epic phailure : Size overload. A word can not be larger than 0xFFFF");
                        return;
                    }

                    Imm = Imm.PadLeft(4, '0');
                    break;

                case 4:
                    if (Imm.Length > 8)
                    {
                        System.Windows.Forms.MessageBox.Show("Epic phailure : Size overload. A doubleword can not be larger than 0xFFFFFFFF");
                        return;
                    }

                    Imm = Imm.PadLeft(8, '0');
                    break;
            }

            //So now that we have the necessary characters, split it up into an array with 2 charactesr per slot
            //then reverse that, change it into a byte array, and add that to the AOB

            string[] ImmBytesS = new string[Imm.Length / 2]; //Immediate Bytes - String
            char[] ImmArray = Imm.ToCharArray();
            string tmp = "";

            for (int i = 0; i < Imm.Length; i += 2)
            {
                tmp = ImmArray[i].ToString();
                tmp += ImmArray[i + 1];
                ImmBytesS[i / 2] = tmp;
            }

            Array.Reverse(ImmBytesS);

            for (int i = 0; i < ImmBytesS.Length; i++)
            {
                aob.Add(byte.Parse(ImmBytesS[i]));
            }
        }
    }
}