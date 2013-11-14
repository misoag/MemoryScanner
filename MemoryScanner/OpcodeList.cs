using System;
using System.Collections.Generic;

namespace Opcode
{
    internal class Opcode
    {
        public Opcode()
        { }
        public Opcode(string Mnemonic, byte[] defined, OpcodeInfo OI1, OpcodeInfo OI2, Param P1, Param P2, Param P3)
        {
            mnemonic = Mnemonic;
            Defined = defined;
            oi1 = OI1;
            oi2 = OI2;
            p1 = P1;
            p2 = P2;
            p3 = P3;
        }

        public string mnemonic = "";
        public byte[] Defined; //Pre-defined bytes in the opcode
        public OpcodeInfo oi1 = OpcodeInfo.none;
        public OpcodeInfo oi2 = OpcodeInfo.none;
        public Param p1 = Param.none;
        public Param p2 = Param.none;
        public Param p3 = Param.none;

        public enum OpcodeInfo
        {
            none,
            digit0, digit1, digit2, digit3, digit4, digit5, digit6, digit7, // /digit
            r, // /r
            cb, cw, cd, cp,
            ib, iw, id,
            rb, rw, rd,
            i
        }

        public enum Param
        {
            none,
            al, ax, eax,
            dx,
            dig1, dig3,
            cr, dr,
            rel8,
            rel16, rel32,
            ptr1616, ptr1632, //ptr 16:16, ptr16:32
            r8, r16, r32,
            imm8, imm16, imm32,
            rm8, rm16, rm32,
            m, m8, m16, m32, m64, m80, m128,
            m1616, m1632, //m16:16, m16:32
            m16n16, m16n32, m32n32, //m16&16, m16&32, m32&32
            moffs8, moffs16, moffs32,
            sreg,
            m32real,
            m16int, m32int,
            st0, sti, //st(0), st(i)
            mm,
            mmm32, //mm-m32
            xmm, xmmm32,
            cs, ds, es, fs, gs, ss,
            cl
        }
    }

    internal static class Opcodes
    {
        #region Opcodes    

        public static List<Opcode> ASSEMBLYLANGUAGE = new List<Opcode>(new Opcode[] { //This uses the IEnumerable overload for creating a new list, by
            //creating an array to put in; I like lists a lot more than arrays, though it's quite hard to directly initialize a list, it's easier to
            //initialize it indirectly like this
            new Opcode("AAA", new byte[] { 0x37 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("AAD", new byte[] { 0xD5, 0x0A }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("AAD", new byte[] { 0xD5 }, Opcode.OpcodeInfo.ib, Opcode.OpcodeInfo.none, Opcode.Param.imm8, Opcode.Param.none, Opcode.Param.none),

            new Opcode("AAM", new byte[] { 0xD4, 0x0A }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("AAM", new byte[] { 0xD4 }, Opcode.OpcodeInfo.ib, Opcode.OpcodeInfo.none, Opcode.Param.imm8, Opcode.Param.none, Opcode.Param.none),

            new Opcode("AAS", new byte[] { 0x3F }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("ADC", new byte[] { 0x14 }, Opcode.OpcodeInfo.ib, Opcode.OpcodeInfo.none, Opcode.Param.al, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x66, 0x15 }, Opcode.OpcodeInfo.iw, Opcode.OpcodeInfo.none, Opcode.Param.ax, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x66, 0x15 }, Opcode.OpcodeInfo.id, Opcode.OpcodeInfo.none, Opcode.Param.eax, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x80 }, Opcode.OpcodeInfo.digit2, Opcode.OpcodeInfo.ib, Opcode.Param.rm8, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x66, 0x81 }, Opcode.OpcodeInfo.digit2, Opcode.OpcodeInfo.iw, Opcode.Param.rm16, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x81 }, Opcode.OpcodeInfo.digit2, Opcode.OpcodeInfo.id, Opcode.Param.rm32, Opcode.Param.imm32, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x66, 0x83 }, Opcode.OpcodeInfo.digit2, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x83 }, Opcode.OpcodeInfo.digit2, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x10 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm8, Opcode.Param.r8, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x66, 0x11 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x11 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x12 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r8, Opcode.Param.rm8, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x66, 0x13 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("ADC", new byte[] { 0x13 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("ADD", new byte[] { 0x04 }, Opcode.OpcodeInfo.ib, Opcode.OpcodeInfo.none, Opcode.Param.al, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x66, 0x05 }, Opcode.OpcodeInfo.iw, Opcode.OpcodeInfo.none, Opcode.Param.ax, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x05 }, Opcode.OpcodeInfo.id, Opcode.OpcodeInfo.none, Opcode.Param.eax, Opcode.Param.imm32, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x80 }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.ib, Opcode.Param.rm8, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x66, 0x81 }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.iw, Opcode.Param.rm16, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x81 }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm32, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x66, 0x83 }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x83 }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x00 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm8, Opcode.Param.r8, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x66, 0x01 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x01 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x02 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r8, Opcode.Param.rm8, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x66, 0x03 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("ADD", new byte[] { 0x03 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("AND", new byte[] { 0x24 }, Opcode.OpcodeInfo.ib, Opcode.OpcodeInfo.none, Opcode.Param.al, Opcode.Param.imm8, Opcode.Param.none), 
            new Opcode("AND", new byte[] { 0x66, 0x25 }, Opcode.OpcodeInfo.iw, Opcode.OpcodeInfo.none, Opcode.Param.ax, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x25 }, Opcode.OpcodeInfo.id, Opcode.OpcodeInfo.none, Opcode.Param.eax, Opcode.Param.imm32, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x80 }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.ib, Opcode.Param.rm8, Opcode.Param.imm8, Opcode.Param.none), 
            new Opcode("AND", new byte[] { 0x66, 0x81 }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.iw, Opcode.Param.rm16, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x81 }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.id, Opcode.Param.rm32, Opcode.Param.imm32, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x83 }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x83 }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x20 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm8, Opcode.Param.r8, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x66, 0x21 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x21 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x22 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r8, Opcode.Param.rm8, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x66, 0x23 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("AND", new byte[] { 0x23 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("ARPL", new byte[] { 0x63}, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),

            new Opcode("BOUND", new byte[] { 0x66, 0x62 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.m16n16, Opcode.Param.none),
            new Opcode("BOUND", new byte[] { 0x62 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.m32n32, Opcode.Param.none),

            new Opcode("BSF", new byte[] { 0x66, 0x0F, 0xBC }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("BSF", new byte[] { 0x0F, 0XBC }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("BSR", new byte[] { 0x66, 0x0F, 0xBD }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("BSR", new byte[] { 0x0F, 0xBD }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("BSWAP", new byte[] { 0x0F, 0xC8 }, Opcode.OpcodeInfo.rd, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.none, Opcode.Param.none),
            
            new Opcode("BT", new byte[] { 0x66, 0x0F, 0xA3 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("BT", new byte[] { 0x0F, 0xA3 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("BT", new byte[] { 0x66, 0x0F, 0xBA }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("BT", new byte[] { 0x0F, 0xBA }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),

            new Opcode("BTC", new byte[] { 0x66, 0x0F, 0xBB }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("BTC", new byte[] { 0x0F, 0xBB }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("BTC", new byte[] { 0x66, 0x0F, 0xBA }, Opcode.OpcodeInfo.digit7, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("BTC", new byte[] { 0x0F, 0xBA }, Opcode.OpcodeInfo.digit7, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),

            new Opcode("BTR", new byte[] { 0x66, 0x0F, 0xB3 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("BTR", new byte[] { 0x0F, 0xB3 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("BTR", new byte[] { 0x66, 0x0F, 0xBA }, Opcode.OpcodeInfo.digit6, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("BTR", new byte[] { 0x0F, 0xBA }, Opcode.OpcodeInfo.digit6, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),

            new Opcode("BTS", new byte[] { 0x66, 0x0F, 0xAB }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("BTS", new byte[] { 0x0F, 0xAB }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("BTS", new byte[] { 0x66, 0x0F, 0xBA }, Opcode.OpcodeInfo.digit5, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("BTS", new byte[] { 0x0F, 0xBA }, Opcode.OpcodeInfo.digit5, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),

            new Opcode("CALL", new byte[] { 0x66, 0xE8 }, Opcode.OpcodeInfo.cw, Opcode.OpcodeInfo.none, Opcode.Param.rel16, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CALL", new byte[] { 0xE8 }, Opcode.OpcodeInfo.cd, Opcode.OpcodeInfo.none, Opcode.Param.rel32, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CALL", new byte[] { 0x66, 0xFF }, Opcode.OpcodeInfo.digit2, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CALL", new byte[] { 0xFF }, Opcode.OpcodeInfo.digit2, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CALL", new byte[] { 0x9A }, Opcode.OpcodeInfo.cd, Opcode.OpcodeInfo.none, Opcode.Param.ptr1616, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CALL", new byte[] { 0x9A }, Opcode.OpcodeInfo.cp, Opcode.OpcodeInfo.none, Opcode.Param.ptr1632, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CALL", new byte[] { 0xFF }, Opcode.OpcodeInfo.digit3, Opcode.OpcodeInfo.none, Opcode.Param.m1616, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CALL", new byte[] { 0xFF }, Opcode.OpcodeInfo.digit3, Opcode.OpcodeInfo.none, Opcode.Param.m1632, Opcode.Param.none, Opcode.Param.none),

            new Opcode("CBW", new byte[] { 0x98 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CWDE", new byte[] { 0x98 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("CLC", new byte[] { 0xF8 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CLD", new byte[] { 0xFC }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CLI", new byte[] { 0xFA }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CLTS", new byte[] { 0x0F, 0x06 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("CMC", new byte[] { 0xF5 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("CMOVA", new byte[] { 0x66, 0x0F, 0x47 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVA", new byte[] { 0x0F, 0x47 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),
            new Opcode("CMOVAE", new byte[] { 0x66, 0x0F, 0x43 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVAE", new byte[] { 0x0F, 0X43 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVB", new byte[] { 0x66, 0x0F, 0x42 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVB", new byte[] { 0x0F, 0x42 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),
            new Opcode("CMOVBE", new byte[] { 0x66, 0x0F, 0x46 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVBE", new byte[] { 0x0F, 0x46 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVC", new byte[] { 0x66, 0x0F, 0x42 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVC", new byte[] { 0x0F, 0x42 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVE", new byte[] { 0x66, 0x0F, 0x44 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVE", new byte[] { 0x0F, 0x44 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVG", new byte[] { 0x66, 0x0F, 0x4F }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVG", new byte[] { 0x0F, 0x4F }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVGE", new byte[] { 0x66, 0x0F, 0x4D }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVGE", new byte[] { 0x0F, 0x4D }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVL", new byte[] { 0x66, 0x0F, 0x4C }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVL", new byte[] { 0x0F, 0x4C }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),
            new Opcode("CMOVLE", new byte[] { 0x66, 0x0F, 0x4E }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVLE", new byte[] { 0x0F, 0x4E }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),
            
            new Opcode("CMOVNO", new byte[] { 0x66, 0x0F, 0x41 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVNO", new byte[] { 0x0F, 0x41 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVNP", new byte[] { 0x66, 0x0F, 0x4B }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVNP", new byte[] { 0x0F, 0x4B }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVNS", new byte[] { 0x66, 0x0F, 0x49 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVNS", new byte[] { 0x0F, 0x49 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVNZ", new byte[] { 0x66, 0x0F, 0x45 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVNZ", new byte[] { 0x0F, 0x45 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVO", new byte[] { 0x66, 0x0F, 0x40 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVO", new byte[] { 0x0F, 0x40 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVP", new byte[] { 0x66, 0x0F, 0x4A }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),
            new Opcode("CMOVP", new byte[] { 0x0F, 0x4A }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVS", new byte[] { 0x66, 0x0F, 0x48 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVS", new byte[] { 0x0F, 0x48 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMOVZ", new byte[] { 0x66, 0x0F, 0x44 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMOVZ", new byte[] { 0x0F, 0x44 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMP", new byte[] { 0x3C }, Opcode.OpcodeInfo.ib, Opcode.OpcodeInfo.none, Opcode.Param.al, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x66, 0x3D }, Opcode.OpcodeInfo.iw, Opcode.OpcodeInfo.none, Opcode.Param.ax, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x3D }, Opcode.OpcodeInfo.id, Opcode.OpcodeInfo.none, Opcode.Param.eax, Opcode.Param.imm32, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x80 }, Opcode.OpcodeInfo.digit7, Opcode.OpcodeInfo.ib, Opcode.Param.rm8, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x66, 0x81 }, Opcode.OpcodeInfo.digit7, Opcode.OpcodeInfo.iw, Opcode.Param.rm16, Opcode.Param.imm16, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x81 }, Opcode.OpcodeInfo.digit7, Opcode.OpcodeInfo.id, Opcode.Param.rm32, Opcode.Param.imm32, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x66, 0x83 }, Opcode.OpcodeInfo.digit7, Opcode.OpcodeInfo.ib, Opcode.Param.rm16, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x83 }, Opcode.OpcodeInfo.digit7, Opcode.OpcodeInfo.ib, Opcode.Param.rm32, Opcode.Param.imm8, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x38 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm8, Opcode.Param.r8, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x66, 0x39 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x39 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x3A }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r8, Opcode.Param.rm8, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x66, 0x3B }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.rm16, Opcode.Param.none),
            new Opcode("CMP", new byte[] { 0x3B }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.rm32, Opcode.Param.none),

            new Opcode("CMPS", new byte[] { 0xA6 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.m8, Opcode.Param.m8, Opcode.Param.none),
            new Opcode("CMPS", new byte[] { 0x66, 0xA7 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.m16, Opcode.Param.m16, Opcode.Param.none),
            new Opcode("CMPS", new byte[] { 0xA7 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.m32, Opcode.Param.m32, Opcode.Param.none),

            new Opcode("CMPSB", new byte[] { 0xA6 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CMPSW", new byte[] { 0xA7 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CMPSD", new byte[] { 0xA7 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("CMPXCHG", new byte[] { 0x0F, 0xB0 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm8, Opcode.Param.r8, Opcode.Param.none),
            new Opcode("CMPXCHG", new byte[] { 0x66, 0x0F, 0xB1 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.r16, Opcode.Param.none),
            new Opcode("CMPXCHG", new byte[] { 0x0F, 0xB1 }, Opcode.OpcodeInfo.r, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.r32, Opcode.Param.none),

            new Opcode("CMPXCHG8B", new byte[] { 0x0F, 0xC7 }, Opcode.OpcodeInfo.digit1, Opcode.OpcodeInfo.none, Opcode.Param.m64, Opcode.Param.none, Opcode.Param.none), //This one was weird--it said that there was an OI param of m64, but that doesn't exist, digit1 works fine though

            new Opcode("CPUID", new byte[] { 0x0F, 0xA2 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("CWD", new byte[] { 0x99 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("CDQ", new byte[] { 0x99 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("DAA", new byte[] { 0x27 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("DAS", new byte[] { 0x2F }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("DEC", new byte[] { 0xFE }, Opcode.OpcodeInfo.digit1, Opcode.OpcodeInfo.none, Opcode.Param.rm8, Opcode.Param.none, Opcode.Param.none),
            new Opcode("DEC", new byte[] { 0x66, 0xFF }, Opcode.OpcodeInfo.digit1, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.none, Opcode.Param.none),
            new Opcode("DEC", new byte[] { 0xFF }, Opcode.OpcodeInfo.digit1, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.none, Opcode.Param.none),
            new Opcode("DEC", new byte[] { 0x66, 0x48 }, Opcode.OpcodeInfo.rw, Opcode.OpcodeInfo.none, Opcode.Param.r16, Opcode.Param.none, Opcode.Param.none),
            new Opcode("DEC", new byte[] { 0x48 }, Opcode.OpcodeInfo.rd, Opcode.OpcodeInfo.none, Opcode.Param.r32, Opcode.Param.none, Opcode.Param.none),

            new Opcode("DIV", new byte[] { 0xF6 }, Opcode.OpcodeInfo.digit6, Opcode.OpcodeInfo.none, Opcode.Param.rm8, Opcode.Param.none, Opcode.Param.none),
            new Opcode("DIV", new byte[] { 0x66, 0xF7 }, Opcode.OpcodeInfo.digit6, Opcode.OpcodeInfo.none, Opcode.Param.rm16, Opcode.Param.none, Opcode.Param.none),
            new Opcode("DIV", new byte[] { 0xF7 }, Opcode.OpcodeInfo.digit6, Opcode.OpcodeInfo.none, Opcode.Param.rm32, Opcode.Param.none, Opcode.Param.none),

            new Opcode("EMMS", new byte[] { 0x0F, 0x77 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("ENTER", new byte[] { 0x66, 0xC8 }, Opcode.OpcodeInfo.iw, Opcode.OpcodeInfo.ib, Opcode.Param.imm16, Opcode.Param.imm8, Opcode.Param.none),

            new Opcode("F2XM1", new byte[] { 0xD9, 0xF0 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("FABS", new byte[] { 0xD9, 0xE1 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("FADD", new byte[] { 0xD8 }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.none, Opcode.Param.m32real, Opcode.Param.none, Opcode.Param.none),
            new Opcode("FADD", new byte[] { 0xD8, 0xC0 }, Opcode.OpcodeInfo.i, Opcode.OpcodeInfo.none, Opcode.Param.st0, Opcode.Param.sti, Opcode.Param.none),
            new Opcode("FADD", new byte[] { 0xDC, 0xC0 }, Opcode.OpcodeInfo.i, Opcode.OpcodeInfo.none, Opcode.Param.sti, Opcode.Param.st0, Opcode.Param.none),
            new Opcode("FADDP", new byte[] { 0xDE, 0xC0 }, Opcode.OpcodeInfo.i, Opcode.OpcodeInfo.none, Opcode.Param.sti, Opcode.Param.st0, Opcode.Param.none),
            new Opcode("FADDP", new byte[] { 0xDE, 0xC1 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            new Opcode("FIADD", new byte[] { 0xDA }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.none, Opcode.Param.m32int, Opcode.Param.none, Opcode.Param.none),
            new Opcode("FIADD", new byte[] { 0xDE }, Opcode.OpcodeInfo.digit0, Opcode.OpcodeInfo.none, Opcode.Param.m16int, Opcode.Param.none, Opcode.Param.none),

            new Opcode("FBLD", new byte[] { 0xDF }, Opcode.OpcodeInfo.digit4, Opcode.OpcodeInfo.none, Opcode.Param.m80, Opcode.Param.none, Opcode.Param.none),
            new Opcode("FBSTP", new byte[] { 0xDF }, Opcode.OpcodeInfo.digit6, Opcode.OpcodeInfo.none, Opcode.Param.m80, Opcode.Param.none, Opcode.Param.none),
            
            new Opcode("FCHS", new byte[] { 0xD9, 0xE0 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),

            new Opcode("FCLEX", new byte[] { 0x9B, 0xDB, 0xE2 }, Opcode.OpcodeInfo.none, Opcode.OpcodeInfo.none, Opcode.Param.none, Opcode.Param.none, Opcode.Param.none),
            
            new Opcode("FCMOVB",new Byte[2] {0xDA,0xc0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),

            new Opcode("FCMOVBE",new Byte[2] {0xDA,0xd0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCMOVE",new Byte[2] {0xDA,0xc8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCMOVNB",new Byte[2] {0xDB,0xc0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCMOVNBE",new Byte[2] {0xDB,0xd0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCMOVNE",new Byte[2] {0xDB,0xc8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCMOVNU",new Byte[2] {0xDB,0xd8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCMOVU",new Byte[2] {0xDA,0xd8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),

            new Opcode("FCOM",new Byte[1] {0xd8},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FCOM",new Byte[1] {0xdc},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FCOM",new Byte[2] {0xd8,0xd0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FCOM",new Byte[2] {0xd8,0xd1},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FCOMP",new Byte[1] {0xd8},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FCOMP",new Byte[1] {0xdc},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FCOMP",new Byte[2] {0xd8,0xd8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FCOMP",new Byte[2] {0xd8,0xd9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FCOMI",new Byte[2] {0xdb,0xf0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCOMIP",new Byte[2] {0xdf,0xf0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FCOMPP",new Byte[2] {0xde,0xd9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FCOMPP",new Byte[2] {0xde,0xd9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FCOS",new Byte[2] {0xD9,0xff},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FDECSTP",new Byte[2] {0xd9,0xf6},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FDIV",new Byte[1] {0xd8},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FDIV",new Byte[1] {0xdc},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FDIV",new Byte[2] {0xd8,0xf0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FDIV",new Byte[2] {0xdc,0xf8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),

            new Opcode("FDIVP",new Byte[2] {0xde,0xf8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FDIVP",new Byte[2] {0xde,0xf9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FDIVR",new Byte[1] {0xd8},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FDIVR",new Byte[1] {0xdc},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FDIVR",new Byte[2] {0xd8,0xf8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FDIVR",new Byte[2] {0xdc,0xf0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FDIVRP",new Byte[2] {0xde,0xf0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FDIVRP",new Byte[2] {0xde,0xf1},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FFREE",new Byte[2] {0xdd,0xc0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FIADD",new Byte[1] {0xDA},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FIADD",new Byte[1] {0xDE},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FICOM",new Byte[1] {0xda},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FICOM",new Byte[1] {0xde},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FICOMP",new Byte[1] {0xda},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FICOMP",new Byte[1] {0xde},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FIDIV",new Byte[1] {0xda},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FIDIV",new Byte[1] {0xde},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FIDIVR",new Byte[1] {0xda},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FIDIVR",new Byte[1] {0xde},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FILD",new Byte[1] {0xdf},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FILD",new Byte[1] {0xdb},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FILD",new Byte[1] {0xdf},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FIMUL",new Byte[1] {0xda},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FIMUL",new Byte[1] {0xde},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FINCSTP",new Byte[2] {0xd9,0xf7},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FINIT",new Byte[3] {0x9b,0xdb,0xe3},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FIST",new Byte[1] {0xdf},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FIST",new Byte[1] {0xdb},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FISTP",new Byte[1] {0xdf},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FISTP",new Byte[1] {0xdb},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FISTP",new Byte[1] {0xdf},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FISUB",new Byte[1] {0xda},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FISUB",new Byte[1] {0xde},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FISUBR",new Byte[1] {0xda},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FISUBR",new Byte[1] {0xde},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FLD",new Byte[1] {0xd9},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLD",new Byte[1] {0xdd},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLD",new Byte[1] {0xdb},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.m80,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLD",new Byte[2] {0xd9,0xc0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FLD1",new Byte[2] {0xd9,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDCW",new Byte[1] {0xd9},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDENV",new Byte[1] {0xd9},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDL2E",new Byte[2] {0xd9,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDL2T",new Byte[2] {0xd9,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDLG2",new Byte[2] {0xd9,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDLN2",new Byte[2] {0xd9,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDPI",new Byte[2] {0xd9,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FLDZ",new Byte[2] {0xd9,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FMUL",new Byte[1] {0xd8},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FMUL",new Byte[1] {0xdc},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FMUL",new Byte[2] {0xd8,0xC8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FMUL",new Byte[2] {0xdc,0xC8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FMULP",new Byte[2] {0xde,0xC8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FMULP",new Byte[2] {0xde,0xc9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FNINIT",new Byte[2] {0xdb,0xe3},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FNLEX",new Byte[2] {0xDb,0xe2},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FNOP",new Byte[2] {0xd6,0xd0},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FNSAVE",new Byte[1] {0xdd},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FNSTCW",new Byte[1] {0xd9},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FNSTENV",new Byte[1] {0xd9},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FNSTSW",new Byte[1] {0xdd},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FNSTSW",new Byte[3] {0x9b,0xdf,0xdf},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FPATAN",new Byte[2] {0xd9,0xf3},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FPREM",new Byte[2] {0xd9,0xf8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FPREM1",new Byte[2] {0xd9,0xf5},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FPTAN",new Byte[2] {0xd9,0xf2},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FRNDINT",new Byte[2] {0xd9,0xfc},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FRSTOR",new Byte[1] {0xdd},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FSAVE",new Byte[2] {0x9b,0xdd},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FSCalE",new Byte[2] {0xd9,0xfd},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FSIN",new Byte[2] {0xd9,0xfe},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSINCOS",new Byte[2] {0xd9,0xfb},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSQRT",new Byte[2] {0xd9,0xfa},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FST",new Byte[1] {0xd9},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FST",new Byte[1] {0xdd},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FST",new Byte[2] {0xdd,0xd0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSTCW",new Byte[2] {0x9b,0xd9},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSTENV",new Byte[2] {0x9b,0xd9},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSTP",new Byte[1] {0xd9},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSTP",new Byte[1] {0xdd},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSTP",new Byte[1] {0xdb},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m80,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSTP",new Byte[2] {0xdd,0xd8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FSTSW",new Byte[2] {0x9b,0xdd},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSTSW",new Byte[3] {0x9b,0xdf,0xe0},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FSUB",new Byte[1] {0xd8},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSUB",new Byte[1] {0xdc},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSUB",new Byte[2] {0xd8,0xe0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FSUB",new Byte[2] {0xdc,0xe8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FSUBP",new Byte[2] {0xde,0xe8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FSUBP",new Byte[2] {0xde,0xe9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSUBPR",new Byte[2] {0xde,0xe0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FSUBPR",new Byte[2] {0xde,0xe1},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSUBR",new Byte[1] {0xd8},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSUBR",new Byte[1] {0xdc},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FSUBR",new Byte[2] {0xd8,0xe8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FSUBR",new Byte[2] {0xdc,0xe0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),

            new Opcode("FTST",new Byte[2] {0xd9,0xe4},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FUCOM",new Byte[2] {0xdd,0xe0},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FUCOM",new Byte[2] {0xdd,0xe1},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FUCOMI",new Byte[2] {0xdb,0xe8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FUCOMIP",new Byte[2] {0xdf,0xe8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.st0,Opcode.Param.none),
            new Opcode("FUCOMP",new Byte[2] {0xdd,0xe8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FUCOMP",new Byte[2] {0xdd,0xe9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FUCOMPP",new Byte[2] {0xda,0xe9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FWAIT",new Byte[1] {0x9b},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),

            new Opcode("FXAM",new Byte[2] {0xd9,0xe5},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("FXCH",new Byte[2] {0xd9,0xc8},Opcode.OpcodeInfo.i,Opcode.OpcodeInfo.none,Opcode.Param.st0,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FXCH",new Byte[2] {0xd9,0xc9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("FXRSTOR",new Byte[2] {0x0f,0xae},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("FXSAVE",new Byte[2] {0x0f,0xae},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("FXTRACT",new Byte[2] {0xd9,0xf4},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("FYL2X",new Byte[2] {0xd9,0xf1},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("FYL2XPI",new Byte[2] {0xd9,0xf9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("HLT",new Byte[1] {0xf4},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("IDIV",new Byte[1] {0xf6},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("IDIV",new Byte[2] {0x66,0xf7},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("IDIV",new Byte[1] {0xf7},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("IMUL",new Byte[1] {0xf6},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("IMUL",new Byte[2] {0x66,0xf7},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("IMUL",new Byte[1] {0xf7},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("IMUL",new Byte[3] {0x66,0x0f,0xaf},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("IMUL",new Byte[2] {0x0f,0xaf},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            new Opcode("IMUL",new Byte[2] {0x66,0x6b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.ib,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.imm8),
            new Opcode("IMUL",new Byte[1] {0x6b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.ib,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.imm8),
            new Opcode("IMUL",new Byte[2] {0x66,0x6b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.ib,Opcode.Param.r16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("IMUL",new Byte[1] {0x6b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.ib,Opcode.Param.r32,Opcode.Param.none,Opcode.Param.imm8),
            new Opcode("IMUL",new Byte[2] {0x66,0x69},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.iw,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.imm16),
            new Opcode("IMUL",new Byte[1] {0x69},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.id,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.imm32),
            new Opcode("IMUL",new Byte[2] {0x66,0x69},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.iw,Opcode.Param.r16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("IMUL",new Byte[1] {0x69},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.id,Opcode.Param.r32,Opcode.Param.imm32,Opcode.Param.none),
            
            new Opcode("IN",new Byte[1] {0xe4},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("IN",new Byte[2] {0x66,0xe5},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("IN",new Byte[1] {0xe5},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("IN",new Byte[1] {0xec},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.dx,Opcode.Param.none),
            new Opcode("IN",new Byte[2] {0x66,0xed},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.dx,Opcode.Param.none),
            new Opcode("IN",new Byte[1] {0xed},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.dx,Opcode.Param.none),
            
            new Opcode("INC",new Byte[2] {0x66,0x40},Opcode.OpcodeInfo.rw,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INC",new Byte[1] {0x40},Opcode.OpcodeInfo.rd,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INC",new Byte[1] {0xfe},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INC",new Byte[2] {0x66,0xff},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INC",new Byte[1] {0xff},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("INSB",new Byte[1] {0x6c},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INSD",new Byte[1] {0x6d},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INSW",new Byte[2] {0x66,0x6d},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("INT",new Byte[1] {0xcc},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.dig3,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INT",new Byte[1] {0xcd},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.imm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("INTO",new Byte[1] {0xce},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INVD",new Byte[2] {0x0f,0x08},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("INVLPG",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("IRET",new Byte[2] {0x66,0xce},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("IRETD",new Byte[1] {0xcf},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JA",new Byte[1] {0x77},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JA",new Byte[2] {0x0f,0x87},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JAE",new Byte[1] {0x73},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JAE",new Byte[2] {0x0f,0x83},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JB",new Byte[1] {0x72},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JB",new Byte[2] {0x0f,0x82},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JBE",new Byte[1] {0x76},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),            
            new Opcode("JBE",new Byte[2] {0x0f,0x86},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JC",new Byte[1] {0x72},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JC",new Byte[2] {0x0f,0x82},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JCXZ",new Byte[2] {0x66,0xe3},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JE",new Byte[1] {0x74},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JE",new Byte[2] {0x0f,0x84},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JECXZ",new Byte[1] {0xe3},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JG",new Byte[1] {0x7f},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JG",new Byte[2] {0x0f,0x8f},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JGE",new Byte[1] {0x7d},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JGE",new Byte[2] {0x0f,0x8d},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JL",new Byte[1] {0x7c},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JL",new Byte[2] {0x0f,0x8c},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JLE",new Byte[1] {0x7e},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JLE",new Byte[2] {0x0f,0x8e},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JMP",new Byte[1] {0xeb},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JMP",new Byte[1] {0xe9},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JMP",new Byte[1] {0xff},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JNC",new Byte[1] {0x73},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JNC",new Byte[2] {0x0f,0x83},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JNE",new Byte[1] {0x75},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JNE",new Byte[2] {0x0f,0x85},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JNO",new Byte[1] {0x71},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JNO",new Byte[2] {0x0f,0x81},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JNP",new Byte[1] {0x7b},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JNP",new Byte[2] {0x0f,0x8b},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JNS",new Byte[1] {0x79},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JNS",new Byte[2] {0x0f,0x89},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JNZ",new Byte[1] {0x75},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JNZ",new Byte[2] {0x0f,0x85},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),

            new Opcode("JO",new Byte[1] {0x70},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JO",new Byte[2] {0x0f,0x80},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JP",new Byte[1] {0x7a},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JP",new Byte[2] {0x0f,0x8a},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JS",new Byte[1] {0x78},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JS",new Byte[2] {0x0f,0x88},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("JZ",new Byte[1] {0x74},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("JZ",new Byte[2] {0x0f,0x84},Opcode.OpcodeInfo.cd,Opcode.OpcodeInfo.none,Opcode.Param.rel32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LAHF",new Byte[1] {0x9f},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LAR",new Byte[3] {0x66,0x0f,0x02},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("LAR",new Byte[2] {0x0f,0x02},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),

            new Opcode("LDMXCSR",new Byte[2] {0x0f,0xae},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LDS",new Byte[2] {0x66,0xc5},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.m16,Opcode.Param.none),
            new Opcode("LDS",new Byte[1] {0xc5},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.m32,Opcode.Param.none),
            
            new Opcode("LEA",new Byte[2] {0x66,0x8d},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.m16,Opcode.Param.none),
            new Opcode("LEA",new Byte[1] {0x8d},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.m32,Opcode.Param.none),
            
            new Opcode("LEAVE",new Byte[1] {0xc9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LES",new Byte[2] {0x66,0xc4},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("LES",new Byte[1] {0xc4},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            
            new Opcode("LFENCE",new Byte[3] {0x0f,0xae,0xe8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LFS",new Byte[3] {0x66,0x0f,0xb4},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.m16,Opcode.Param.none),
            new Opcode("LFS",new Byte[2] {0x0f,0xb4},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.m32,Opcode.Param.none),
            
            new Opcode("LGDT",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LGDT",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
           
            new Opcode("LGS",new Byte[3] {0x66,0x0f,0xb5},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.m16,Opcode.Param.none),
            new Opcode("LGS",new Byte[2] {0x0f,0xb5},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.m32,Opcode.Param.none),
            
            new Opcode("LIDT",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LIDT",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LLDT",new Byte[2] {0x0f,0x00},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LMSW",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LODSB",new Byte[1] {0xac},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LODSD",new Byte[1] {0xad},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LODSW",new Byte[2] {0x66,0xad},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LOOP",new Byte[1] {0xe2},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LOOPE",new Byte[2] {0x66,0xe1},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LOOPNE",new Byte[2] {0x66,0xe0},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LOOPNZ",new Byte[1] {0xe0},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("LOOPZ",new Byte[1] {0xe1},Opcode.OpcodeInfo.cb,Opcode.OpcodeInfo.none,Opcode.Param.rel8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("LSL",new Byte[3] {0x66,0x0f,0x03},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("LSL",new Byte[2] {0x0f,0x03},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            new Opcode("LSS",new Byte[3] {0x66,0x0f,0xb2},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.m16,Opcode.Param.none),
            new Opcode("LSS",new Byte[2] {0x0f,0xb2},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.m32,Opcode.Param.none),
            new Opcode("LTR",new Byte[2] {0x0f,0x00},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("MASKMOVDQU",new Byte[3] {0x66,0x0f,0xf7},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.mm,Opcode.Param.none),
            new Opcode("MASKMOVQ",new Byte[2] {0x0f,0xf7},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.mm,Opcode.Param.mm,Opcode.Param.none),
            
            new Opcode("MFENCE",new Byte[3] {0x0f,0xae,0xf0},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("MINSS",new Byte[3] {0xf3,0x0f,0x5d},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmmm32,Opcode.Param.none),
            
            new Opcode("MOV",new Byte[1] {0xa0},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.moffs8,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x66,0xa1},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.moffs16,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0xa1},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.moffs32,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0xa2},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.moffs8,Opcode.Param.al,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x66,0xa3},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.moffs16,Opcode.Param.ax,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0xa3},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.moffs32,Opcode.Param.eax,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0x88},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x66,0x89},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0x8b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0x89},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0x8a},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r8,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x66,0x8b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0x8c},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.sreg,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0x8e},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.sreg,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0xb0},Opcode.OpcodeInfo.rb,Opcode.OpcodeInfo.none,Opcode.Param.r8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x66,0xb8},Opcode.OpcodeInfo.rw,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0xb8},Opcode.OpcodeInfo.rd,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0xc6},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x66,0xc7},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("MOV",new Byte[1] {0xc7},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x0f,0x22},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.cr,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x0f,0x20},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.cr,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x0f,0x21},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.dr,Opcode.Param.none),
            new Opcode("MOV",new Byte[2] {0x0f,0x23},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.dr,Opcode.Param.r32,Opcode.Param.none),
           
            new Opcode("MOVD",new Byte[2] {0x0f,0x6e},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.mm,Opcode.Param.rm32,Opcode.Param.none),
            new Opcode("MOVD",new Byte[2] {0x0f,0x7e},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.mm,Opcode.Param.none),
            new Opcode("MOVD",new Byte[3] {0x66,0x0f,0x6e},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.rm32,Opcode.Param.none),
            new Opcode("MOVD",new Byte[3] {0x66,0x0f,0x7e},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.xmm,Opcode.Param.none),
            
            new Opcode("MOVDQ2Q",new Byte[3] {0xf2,0x0f,0xd6},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.mm,Opcode.Param.xmm,Opcode.Param.none),
            
            new Opcode("MOVHLPS",new Byte[2] {0x0f,0x12},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVHPD",new Byte[3] {0x66,0x0f,0x16},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.m64,Opcode.Param.none),
            new Opcode("MOVHPD",new Byte[3] {0x66,0x0f,0x17},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVHPS",new Byte[2] {0x0f,0x16},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.m64,Opcode.Param.none),
            new Opcode("MOVHPS",new Byte[2] {0x0f,0x17},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVLHPS",new Byte[2] {0x0f,0x16},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmm,Opcode.Param.none),
            
            new Opcode("MOVLPD",new Byte[3] {0x66,0x0f,0x12},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.m64,Opcode.Param.none),
            new Opcode("MOVLPD",new Byte[3] {0x66,0x0f,0x13},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVLPS",new Byte[2] {0x0f,0x12},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.m64,Opcode.Param.none),
            new Opcode("MOVLPS",new Byte[2] {0x0f,0x13},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.xmm,Opcode.Param.none),
            
            new Opcode("MOVMSKPD",new Byte[3] {0x66,0x0f,0x50},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVMSKPS",new Byte[2] {0x0f,0x50},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVNTDQ",new Byte[3] {0x66,0x0f,0xe7},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m128,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVNTI",new Byte[2] {0x0f,0xc3},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("MOVNTPD",new Byte[3] {0x66,0x0f,0x2b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m128,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVNTPS",new Byte[2] {0x0f,0x2b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m128,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVNTQ",new Byte[2] {0x0f,0xe7},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.m64,Opcode.Param.mm,Opcode.Param.none),            

            new Opcode("MOVQ2DQ",new Byte[3] {0x66,0x0f,0xd6},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.mm,Opcode.Param.none),
            
            new Opcode("MOVSB",new Byte[1] {0xa4},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("MOVSD",new Byte[1] {0xa5},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("MOVSS",new Byte[3] {0xf3,0x0f,0x10},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmmm32,Opcode.Param.none),
            new Opcode("MOVSS",new Byte[3] {0xf3,0x0f,0x11},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmmm32,Opcode.Param.xmm,Opcode.Param.none),
            new Opcode("MOVSW",new Byte[2] {0x66,0xa5},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("MOVSX",new Byte[3] {0x66,0x0f,0xbe},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("MOVSX",new Byte[2] {0x0f,0xbe},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("MOVSX",new Byte[2] {0x0f,0xbf},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm16,Opcode.Param.none),
            
            new Opcode("MOVZX",new Byte[3] {0x66,0x0f,0xb6},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("MOVZX",new Byte[2] {0x0f,0xb6},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("MOVZX",new Byte[2] {0x0f,0xb7},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm16,Opcode.Param.none),
            
            new Opcode("MUL",new Byte[1] {0xf6},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("MUL",new Byte[2] {0x66,0xf7},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("MUL",new Byte[1] {0xf7},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
           
            new Opcode("MULSS",new Byte[3] {0xf3,0x0f,0x59},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmmm32,Opcode.Param.none),
            
            new Opcode("NEG",new Byte[1] {0xf6},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("NEG",new Byte[2] {0x66,0xf7},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("NEG",new Byte[1] {0xf7},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("NOP",new Byte[1] {0x90},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("NOT",new Byte[1] {0xf6},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("NOT",new Byte[2] {0x66,0xf7},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("NOT",new Byte[1] {0xf7},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),

            new Opcode("OR",new Byte[1] {0x0c},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("OR",new Byte[2] {0x66,0x0d},Opcode.OpcodeInfo.iw,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x0d},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x80},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("OR",new Byte[2] {0x66,0x80},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.iw,Opcode.Param.rm16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x81},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.id,Opcode.Param.rm32,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("OR",new Byte[2] {0x66,0x83},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x83},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x08},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("OR",new Byte[2] {0x66,0x09},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x09},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x0a},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r8,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("OR",new Byte[2] {0x66,0x0b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("OR",new Byte[1] {0x0b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            
            new Opcode("OUT",new Byte[1] {0xe6},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.imm8,Opcode.Param.al,Opcode.Param.none),
            new Opcode("OUT",new Byte[2] {0x66,0xe7},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.imm8,Opcode.Param.ax,Opcode.Param.none),
            new Opcode("OUT",new Byte[1] {0xe7},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.imm8,Opcode.Param.eax,Opcode.Param.none),
            new Opcode("OUT",new Byte[1] {0xee},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.dx,Opcode.Param.al,Opcode.Param.none),
            new Opcode("OUT",new Byte[2] {0x66,0xef},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.dx,Opcode.Param.ax,Opcode.Param.none),
            new Opcode("OUT",new Byte[1] {0xef},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.dx,Opcode.Param.eax,Opcode.Param.none),
            
            new Opcode("OUTSB",new Byte[1] {0x6e},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("OUTSD",new Byte[1] {0x6f},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("OUTSW",new Byte[2] {0x66,0x6f},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("PAUSE",new Byte[2] {0xf3,0x90},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("POP",new Byte[2] {0x66,0x58},Opcode.OpcodeInfo.rw,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[1] {0x58},Opcode.OpcodeInfo.rd,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[2] {0x66,0x8f},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[1] {0x8f},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[1] {0x1f},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.ds,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[1] {0x07},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.es,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[1] {0x17},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.ss,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[2] {0x0f,0xa1},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.fs,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POP",new Byte[2] {0x0f,0xa9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.gs,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("POPA",new Byte[2] {0x66,0x61},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POPAD",new Byte[1] {0x61},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("POPF",new Byte[2] {0x66,0x9d},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("POPFD",new Byte[1] {0x9d},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("PREFETCH0",new Byte[2] {0x0f,0x18},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.m8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PREFETCH1",new Byte[2] {0x0f,0x18},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.m8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PREFETCH2",new Byte[2] {0x0f,0x18},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PREFETCHA",new Byte[2] {0x0f,0x18},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("PSLLD",new Byte[2] {0x0f,0x72},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSLLD",new Byte[3] {0x66,0x0f,0x72},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSLLDQ",new Byte[3] {0x66,0x0f,0x73},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSLLQ",new Byte[2] {0x0f,0x73},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSLLQ",new Byte[3] {0x66,0x0f,0x73},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSLLW",new Byte[2] {0x0f,0x71},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSLLW",new Byte[3] {0x66,0x0f,0x71},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSQRTSS",new Byte[3] {0xf3,0x0f,0x52},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmmm32,Opcode.Param.none),
            
            new Opcode("PSRAD",new Byte[2] {0x0f,0x72},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSRAD",new Byte[3] {0x66,0x0f,0x72},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSRAW",new Byte[2] {0x0f,0x71},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSRAW",new Byte[3] {0x66,0x0f,0x71},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSRLD",new Byte[2] {0x0f,0x72},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSRLD",new Byte[3] {0x66,0x0f,0x72},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSRLDQ",new Byte[3] {0x66,0x0f,0x73},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSRLQ",new Byte[2] {0x0f,0x73},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSRLQ",new Byte[3] {0x66,0x0f,0x73},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("PSRLW",new Byte[2] {0x0f,0x71},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.mm,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("PSRLW",new Byte[3] {0x66,0x0f,0x71},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.xmm,Opcode.Param.imm8,Opcode.Param.none),            
            
            new Opcode("PUSH",new Byte[2] {0x66,0x50},Opcode.OpcodeInfo.rw,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0x50},Opcode.OpcodeInfo.rd,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[2] {0x66,0xff},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0xff},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0x6a},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.imm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[2] {0x66,0x68},Opcode.OpcodeInfo.iw,Opcode.OpcodeInfo.none,Opcode.Param.imm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0x68},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.imm32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0x0e},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.cs,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0x16},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.ss,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0x1e},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.ds,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[1] {0x06},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.es,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[2] {0x0f,0xa0},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.fs,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSH",new Byte[2] {0x0f,0xa8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.gs,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("PUSHA",new Byte[2] {0x66,0x60},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSHAD",new Byte[1] {0x60},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
           
            new Opcode("PUSHF",new Byte[2] {0x66,0x9c},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("PUSHFD",new Byte[1] {0x9c},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("RCL",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("RCL",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("RCL",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("RCL",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("RCL",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("RCL",new Byte[2] {0x66,0xc1},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("RCL",new Byte[1] {0xd1},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("RCL",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("RCL",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit2,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("RCR",new Byte[1] {0xd1},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("RCR",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("RCR",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("RCR",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("RCR",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("RCR",new Byte[2] {0x66,0xc1},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("RCR",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("RCR",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("RCR",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("RDMSR",new Byte[2] {0x0f,0x32},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("RDPMC",new Byte[2] {0x0f,0x33},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("RDTSC",new Byte[2] {0x0f,0x31},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("RET",new Byte[1] {0xc3},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("RET",new Byte[1] {0xcb},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("RET",new Byte[1] {0xc2},Opcode.OpcodeInfo.iw,Opcode.OpcodeInfo.none,Opcode.Param.imm16,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("ROL",new Byte[1] {0xd1},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("ROL",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("ROL",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("ROL",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("ROL",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("ROL",new Byte[2] {0x66,0xc1},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("ROL",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("ROL",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("ROL",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("ROR",new Byte[1] {0xd1},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("ROR",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("ROR",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("ROR",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("ROR",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("ROR",new Byte[2] {0x66,0xc1},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("ROR",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("ROR",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("ROR",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("RSM",new Byte[2] {0x0f,0xaa},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SAHF",new Byte[1] {0x9e},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SAL",new Byte[1] {0xd1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SAL",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SAL",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SAL",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SAL",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SAL",new Byte[2] {0x66,0xc1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SAL",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SAL",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SAL",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("SAR",new Byte[1] {0xd1},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SAR",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SAR",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SAR",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SAR",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SAR",new Byte[2] {0x66,0xc1},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SAR",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SAR",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SAR",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit7,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("SBB",new Byte[1] {0x1c},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SBB",new Byte[2] {0x66,0x1d},Opcode.OpcodeInfo.iw,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x1d},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x80},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SBB",new Byte[2] {0x66,0x80},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.iw,Opcode.Param.rm16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x81},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.id,Opcode.Param.rm32,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("SBB",new Byte[2] {0x66,0x83},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x83},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x18},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("SBB",new Byte[2] {0x66,0x19},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x19},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x1a},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r8,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("SBB",new Byte[2] {0x66,0x1b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("SBB",new Byte[1] {0x1b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            
            new Opcode("SCASB",new Byte[1] {0xae},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SCASD",new Byte[1] {0xaf},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SCASW",new Byte[2] {0x66,0xaf},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETA",new Byte[2] {0x0f,0x97},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETAE",new Byte[2] {0x0f,0x93},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETB",new Byte[2] {0x0f,0x92},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETBE",new Byte[2] {0x0f,0x96},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETC",new Byte[2] {0x0f,0x92},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETE",new Byte[2] {0x0f,0x94},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETG",new Byte[2] {0x0f,0x9f},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETGE",new Byte[2] {0x0f,0x9d},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETL",new Byte[2] {0x0f,0x9c},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETLE",new Byte[2] {0x0f,0x9e},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETNC",new Byte[2] {0x0f,0x93},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETNE",new Byte[2] {0x0f,0x95},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),

            new Opcode("SETNO",new Byte[2] {0x0f,0x91},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),            
            new Opcode("SETNP",new Byte[2] {0x0f,0x9b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETNS",new Byte[2] {0x0f,0x99},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),            
            new Opcode("SETNZ",new Byte[2] {0x0f,0x95},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETO",new Byte[2] {0x0f,0x90},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETP",new Byte[2] {0x0f,0x9a},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETPE",new Byte[2] {0x0f,0x9a},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETPO",new Byte[2] {0x0f,0x9b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SETS",new Byte[2] {0x0f,0x98},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SETZ",new Byte[2] {0x0f,0x94},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SFENCE",new Byte[3] {0x0f,0xae,0xf8},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SGDT",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SHL",new Byte[1] {0xd1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SHL",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SHL",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SHL",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SHL",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SHL",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SHL",new Byte[2] {0x66,0xc1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SHL",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SHL",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("SHLD",new Byte[3] {0x66,0x0f,0xa4},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.imm8),
            new Opcode("SHLD",new Byte[3] {0x66,0x0f,0xa5},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.cl),
            new Opcode("SHLD",new Byte[2] {0x0f,0xa4},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.imm8),
            new Opcode("SHLD",new Byte[2] {0x0f,0xa5},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.cl),
            
            new Opcode("SHR",new Byte[1] {0xd0},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SHR",new Byte[1] {0xd2},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SHR",new Byte[1] {0xc0},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SHR",new Byte[2] {0x66,0xd1},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.dig1,Opcode.Param.none),
            new Opcode("SHR",new Byte[2] {0x66,0xd3},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SHR",new Byte[1] {0xd3},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.cl,Opcode.Param.none),
            new Opcode("SHR",new Byte[1] {0xc1},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            
            new Opcode("SHRD",new Byte[2] {0x0f,0xac},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.imm8),
            new Opcode("SHRD",new Byte[2] {0x0f,0xac},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.cl),
            
            new Opcode("SIDT",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SLDT",new Byte[2] {0x0f,0x00},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SMSW",new Byte[2] {0x0f,0x01},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SQRTSS",new Byte[3] {0xf2,0x0f,0x51},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmmm32,Opcode.Param.none),
            
            new Opcode("STC",new Byte[1] {0xf9},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("STD",new Byte[1] {0xfd},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("STI",new Byte[1] {0xfb},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("STMXCSR",new Byte[2] {0x0f,0xae},Opcode.OpcodeInfo.digit3,Opcode.OpcodeInfo.none,Opcode.Param.m32,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("STOSB",new Byte[1] {0xaa},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("STOSD",new Byte[1] {0xab},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("STOSW",new Byte[2] {0x66,0xab},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("STR",new Byte[2] {0x0f,0x00},Opcode.OpcodeInfo.digit1,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("SUB",new Byte[1] {0x2c},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SUB",new Byte[2] {0x66,0x2d},Opcode.OpcodeInfo.iw,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x2d},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x80},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SUB",new Byte[2] {0x66,0x80},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.iw,Opcode.Param.rm16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x81},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.id,Opcode.Param.rm32,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("SUB",new Byte[2] {0x66,0x83},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x83},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x28},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("SUB",new Byte[2] {0x66,0x29},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x29},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x2a},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r8,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("SUB",new Byte[2] {0x66,0x2b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("SUB",new Byte[1] {0x2b},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            
            new Opcode("SUBSS",new Byte[3] {0xf2,0x0f,0x5c},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmmm32,Opcode.Param.none),
            
            new Opcode("SYSENTER",new Byte[2] {0x0f,0x34},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            new Opcode("SYSEXIT",new Byte[2] {0x0f,0x35},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("TEST",new Byte[1] {0xa8},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("TEST",new Byte[2] {0x66,0xa9},Opcode.OpcodeInfo.iw,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("TEST",new Byte[1] {0xa9},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("TEST",new Byte[1] {0xf6},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("TEST",new Byte[2] {0x66,0xf7},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.iw,Opcode.Param.rm16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("TEST",new Byte[1] {0xf7},Opcode.OpcodeInfo.digit0,Opcode.OpcodeInfo.id,Opcode.Param.rm32,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("TEST",new Byte[1] {0x84},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("TEST",new Byte[2] {0x66,0x85},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("TEST",new Byte[1] {0x85},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            
            new Opcode("UCOMISS",new Byte[2] {0x0f,0x2e},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.xmm,Opcode.Param.xmmm32,Opcode.Param.none),
            
            new Opcode("UD2",new Byte[2] {0x0f,0x0b},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("VERR",new Byte[2] {0x0f,0x00},Opcode.OpcodeInfo.digit4,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            new Opcode("VERW",new Byte[2] {0x0f,0x00},Opcode.OpcodeInfo.digit5,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("WAIT",new Byte[1] {0x9b},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("WBINVD",new Byte[2] {0x0f,0x09},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("WRMSR",new Byte[2] {0x0f,0x30},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("XADD",new Byte[2] {0x0f,0xc0},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("XADD",new Byte[3] {0x66,0x0f,0xc1},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("XADD",new Byte[2] {0x0f,0xc1},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            
            new Opcode("XCHG",new Byte[2] {0x66,0x90},Opcode.OpcodeInfo.rw,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("XCHG",new Byte[1] {0x90},Opcode.OpcodeInfo.rd,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("XCHG",new Byte[2] {0x66,0x90},Opcode.OpcodeInfo.rw,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.ax,Opcode.Param.none),
            new Opcode("XCHG",new Byte[1] {0x90},Opcode.OpcodeInfo.rd,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.eax,Opcode.Param.none),
            new Opcode("XCHG",new Byte[1] {0x86},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("XCHG",new Byte[1] {0x86},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r8,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("XCHG",new Byte[2] {0x66,0x87},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("XCHG",new Byte[2] {0x66,0x87},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("XCHG",new Byte[1] {0x87},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("XCHG",new Byte[1] {0x87},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
            
            new Opcode("XLATB",new Byte[1] {0xd7},Opcode.OpcodeInfo.none,Opcode.OpcodeInfo.none,Opcode.Param.none,Opcode.Param.none,Opcode.Param.none),
            
            new Opcode("XOR",new Byte[1] {0x34},Opcode.OpcodeInfo.ib,Opcode.OpcodeInfo.none,Opcode.Param.al,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("XOR",new Byte[2] {0x66,0x35},Opcode.OpcodeInfo.iw,Opcode.OpcodeInfo.none,Opcode.Param.ax,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x35},Opcode.OpcodeInfo.id,Opcode.OpcodeInfo.none,Opcode.Param.eax,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x80},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.rm8,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("XOR",new Byte[2] {0x66,0x80},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.iw,Opcode.Param.rm16,Opcode.Param.imm16,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x81},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.id,Opcode.Param.rm32,Opcode.Param.imm32,Opcode.Param.none),
            new Opcode("XOR",new Byte[2] {0x66,0x83},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.rm16,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x83},Opcode.OpcodeInfo.digit6,Opcode.OpcodeInfo.ib,Opcode.Param.rm32,Opcode.Param.imm8,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x30},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm8,Opcode.Param.r8,Opcode.Param.none),
            new Opcode("XOR",new Byte[2] {0x66,0x31},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm16,Opcode.Param.r16,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x31},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.rm32,Opcode.Param.r32,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x32},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r8,Opcode.Param.rm8,Opcode.Param.none),
            new Opcode("XOR",new Byte[2] {0x66,0x33},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r16,Opcode.Param.rm16,Opcode.Param.none),
            new Opcode("XOR",new Byte[1] {0x33},Opcode.OpcodeInfo.r,Opcode.OpcodeInfo.none,Opcode.Param.r32,Opcode.Param.rm32,Opcode.Param.none),
           });

        #endregion

    }
}